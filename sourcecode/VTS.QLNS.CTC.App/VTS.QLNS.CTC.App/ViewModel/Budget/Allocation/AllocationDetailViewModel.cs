using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Allocation.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Allocation
{
    public class AllocationDetailViewModel : DetailViewModelBase<AllocationModel, AllocationDetailModel>
    {
        private ICpChungTuService _chungTuService;
        private INsDtChungTuService _dtChungTuService;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private ICpChungTuChiTietService _chungTuChiTietService;
        private INsDtChungTuChiTietService _dtChungTuChiTietService;
        private INsDonViService _donViService;
        private INsNguoiDungDonViService _nguoiDungDonViService;
        private ICollectionView _dataDetailAllocationFilter;
        private ICollectionView _budgetCatalogFilter;
        private INsMucLucNganSachService _mucLucNganSachService;
        private IDanhMucService _danhMucService;
        private readonly ILog _logger;
        private AllocationDetailCriteria _searchCondition;
        private List<CpChungTuChiTietQuery> _listChungTuChiTiet;
        private List<string> _xauNoiMaDonViFilter;
        private SessionInfo _sessionInfo;
        private bool _isCapPhatToanDonVi;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        public override Type ContentType => typeof(View.Budget.Allocation.AllocationDetail);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        public string CapPhatTitle => Model != null && Model.LoaiCap == LoaiCap.CAP_THU.ToString() ? "Cấp thu" : "Cấp phát";
        public bool IsDelete => _selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI ? false : (SelectedItem != null ? SelectedItem.HasData : false);
        public bool IsDeleteAll => Items.Any(item => !item.IsModified && item.HasData);
        public bool IsReadOnlyGrid => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI) ? true : false;
        public bool IsTongHop => !string.IsNullOrEmpty(Model.DSSoChungTuTongHop) ? true : false;
        public Visibility VisibleColAgency => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI) ?
            Visibility.Collapsed : Visibility.Visible;

        public Visibility VisibleColumnAgency => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI)
            || ParentPage?.FuncCode == NSFunctionCode.BUDGET_ALLOCATION_RECEIVE ?
            Visibility.Collapsed : Visibility.Visible;

        public Visibility VisibleVoucherNo => !string.IsNullOrEmpty(Model.DSSoChungTuTongHop) && VisibleColAgency == Visibility.Visible ? Visibility.Visible : Visibility.Collapsed;

        public bool ReadOnlyCapPhat => IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI ? true : false;
        public bool ReadOnlyDeNghi => IsTongHop ? true : false;
        public bool IsEditByRole => Model.UserCreator == _sessionInfo.Principal;

        private string xnmConcatenation = "";
        private ICollection<AllocationDetailModel> _filterResult = new HashSet<AllocationDetailModel>();

        //Xác định lần đầu tiên tạo mới
        public bool IsCreate;

        public int NamLamViec { get; set; }

        private bool _isSummaryVoucher;
        public bool IsSummaryVoucher
        {
            get => _isSummaryVoucher;
            set
            {
                SetProperty(ref _isSummaryVoucher, value);
                OnPropertyChanged(nameof(ReadOnlyCapPhat));
                OnPropertyChanged(nameof(ReadOnlyDeNghi));
            }
        }

        private bool _isOpenRefresh;
        public bool IsOpenRefresh
        {
            get => _isOpenRefresh;
            set => SetProperty(ref _isOpenRefresh, value);
        }

        private ObservableCollection<ComboboxItem> _typeDisplays;
        public ObservableCollection<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private string _selectedTypeDisplays;
        public string SelectedTypeDisplays
        {
            get => _selectedTypeDisplays;
            set
            {
                if (SetProperty(ref _selectedTypeDisplays, value) && _dataDetailAllocationFilter != null)
                {
                    if (_selectedTypeDisplays == TypeDisplay.TAT_CA)
                    {
                        LoadData();
                        OnPropertyChanged(nameof(ReadOnlyCapPhat));
                        OnPropertyChanged(nameof(ReadOnlyDeNghi));
                        OnPropertyChanged(nameof(IsDeleteAll));
                        OnPropertyChanged(nameof(IsShowTypeAgency));
                        OnPropertyChanged(nameof(VisibleColAgency));
                        OnPropertyChanged(nameof(VisibleVoucherNo));
                    }
                    BeForeRefresh();
                    OnRefeshFilter();
                }
            }
        }

        private ObservableCollection<AllocationModel> _dataAllocationSummary;
        public ObservableCollection<AllocationModel> DataAllocationSummary
        {
            get => _dataAllocationSummary;
            set => SetProperty(ref _dataAllocationSummary, value);
        }

        private string _selectedDonVi;
        public string SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                if (SetProperty(ref _selectedDonVi, value))
                {
                    OnRefeshFilter();
                    BeForeRefresh();
                }
            }
        }

        private ObservableCollection<CheckBoxItem> _dataDonVi;
        public ObservableCollection<CheckBoxItem> DataDonVi
        {
            get => _dataDonVi;
            set => SetProperty(ref _dataDonVi, value);
        }

        private AllocationDetailFilterModel _detailFilter;
        public AllocationDetailFilterModel DetailFilter
        {
            get => _detailFilter;
            set => SetProperty(ref _detailFilter, value);
        }

        private string _selectedLNS;
        public string SelectedLNS
        {
            get => _selectedLNS;
            set => SetProperty(ref _selectedLNS, value);
        }

        private bool _isOpenLnsPopup;
        public bool IsOpenLnsPopup
        {
            get => _isOpenLnsPopup;
            set
            {
                SetProperty(ref _isOpenLnsPopup, value);
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _budgetCatalogFilter.Refresh();
                }
            }
        }

        public Visibility ShowTotal => Items.Count > 0 ? Visibility.Visible : Visibility.Hidden;

        private ObservableCollection<NsMuclucNgansachModel> _budgetCatalogItems;
        public ObservableCollection<NsMuclucNgansachModel> BudgetCatalogItems
        {
            get => _budgetCatalogItems;
            set => SetProperty(ref _budgetCatalogItems, value);
        }

        private NsMuclucNgansachModel _selectedBudgetCatalog;
        public NsMuclucNgansachModel SelectedBudgetCatalog
        {
            get => _selectedBudgetCatalog;
            set
            {
                if (SetProperty(ref _selectedBudgetCatalog, value))
                {
                    BeForeRefresh();
                    OnRefeshFilter();
                }
                if (_selectedBudgetCatalog != null)
                    SelectedLNS = _selectedBudgetCatalog.Lns;
                IsOpenLnsPopup = false;
            }
        }

        private ObservableCollection<AllocationModel> _dataAllocation;
        public ObservableCollection<AllocationModel> DataAllocation
        {
            get => _dataAllocation;
            set => SetProperty(ref _dataAllocation, value);
        }

        private bool _isShowTypeAgency;
        public bool IsShowTypeAgency
        {
            get => _isShowTypeAgency;
            set => SetProperty(ref _isShowTypeAgency, value);
        }

        private ObservableCollection<ComboboxItem> _typeShowAgency;
        public ObservableCollection<ComboboxItem> TypeShowAgency
        {
            get => _typeShowAgency;
            set => SetProperty(ref _typeShowAgency, value);
        }

        private ComboboxItem _selectedTypeShowAgency;
        public ComboboxItem SelectedTypeShowAgency
        {
            get => _selectedTypeShowAgency;
            set
            {
                if (SetProperty(ref _selectedTypeShowAgency, value))
                {
                    if (IsShowTypeAgency && _selectedTypeShowAgency != null)
                    {
                        LoadDonVi();
                    }
                    OnPropertyChanged(nameof(ReadOnlyCapPhat));
                    OnPropertyChanged(nameof(ReadOnlyDeNghi));
                    BeForeRefresh();
                    LoadData();
                    OnRefeshFilter();
                }
            }
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RefreshAllDataCommand { get; }
        public RelayCommand AutoFillDataCommand { get; }
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand PrintActionCommand { get; }

        public PrintAllocationNoticeViewModel PrintAllocationNoticeViewModel { get; }
        public PrintAllocationDonViViewModel PrintAllocationDonViViewModel { get; }
        public PrintAllocationRequestViewModel PrintAllocationRequestViewModel { get; }
        public PrintAllocationTypeViewModel PrintAllocationTypeViewModel { get; }
        public AllocationDetailViewModel(ICpChungTuService cpChungTuService,
            ICpChungTuChiTietService chungTuChiTietService,
            INsDtChungTuService dtChungTuService,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            ILog logger,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsMucLucNganSachService mucLucNganSachService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IDanhMucService danhMucService,
            PrintAllocationDonViViewModel printAllocationDonViViewModel,
            PrintAllocationRequestViewModel printAllocationRequestViewModel,
            PrintAllocationNoticeViewModel printAllocationNoticeViewModel,
            PrintAllocationTypeViewModel printAllocationTypeViewModel
            )
        {
            _mapper = mapper;
            _chungTuService = cpChungTuService;
            _dtChungTuService = dtChungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _sessionService = sessionService;
            _donViService = nsDonViService;
            _mucLucNganSachService = mucLucNganSachService;
            _logger = logger;
            _nguoiDungDonViService = nsNguoiDungDonViService;
            _danhMucService = danhMucService;
            PrintAllocationNoticeViewModel = printAllocationNoticeViewModel;
            PrintAllocationDonViViewModel = printAllocationDonViViewModel;
            PrintAllocationRequestViewModel = printAllocationRequestViewModel;
            PrintAllocationTypeViewModel = printAllocationTypeViewModel;

            SaveCommand = new RelayCommand(obj => OnSaveData());
            SearchCommand = new RelayCommand(obj =>
            {
                BeForeRefresh();
                OnRefeshFilter();
            });
            RefreshAllDataCommand = new RelayCommand(obj => OnRefreshAllData());
            AutoFillDataCommand = new RelayCommand(obj => OnAutoFillData());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
        }

        public override void Init()
        {
            try
            {
                MarginRequirement = new System.Windows.Thickness(10);
                _sessionInfo = _sessionService.Current;
                NamLamViec = _sessionService.Current.YearOfWork;
                ResetSearchCondition();
                LoadSettingCapPhat();
                IsSummaryVoucher = false;
                IsShowTypeAgency = false;
                if (!string.IsNullOrEmpty(Model.DSSoChungTuTongHop))
                {
                    IsShowTypeAgency = true;
                    IsSummaryVoucher = true;
                    LoadComboboxTypeShow();
                }
                LoadTypeDisplay();
                LoadDonVi();
                LoadBudgetIndexCondition();
                if (!IsEditByRole)
                    MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.UserCreator));

                LoadData();

                OnPropertyChanged(nameof(ReadOnlyCapPhat));
                OnPropertyChanged(nameof(ReadOnlyDeNghi));
                OnPropertyChanged(nameof(IsDeleteAll));
                OnPropertyChanged(nameof(IsShowTypeAgency));
                OnPropertyChanged(nameof(VisibleColAgency));
                OnPropertyChanged(nameof(VisibleVoucherNo));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadSettingCapPhat()
        {
            DanhMuc dmCapPhatToanDonVi = _danhMucService.FindByCode(MaDanhMuc.CAP_PHAT_TOAN_DON_VI);
            if (dmCapPhatToanDonVi != null)
                bool.TryParse(dmCapPhatToanDonVi.SGiaTri, out _isCapPhatToanDonVi);
            else _isCapPhatToanDonVi = false;
        }

        private void ResetSearchCondition()
        {
            _selectedTypeShowAgency = null;
            DetailFilter = new AllocationDetailFilterModel();
            Items = new ObservableCollection<AllocationDetailModel>();
        }

        private void OnRemoveFilter()
        {
            DetailFilter = new AllocationDetailFilterModel();
            SelectedLNS = string.Empty;
            BeForeRefresh();
            OnRefeshFilter();
        }

        private void LoadComboboxTypeShow()
        {
            TypeShowAgency = new ObservableCollection<ComboboxItem>();
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgency = TypeShowAgency.FirstOrDefault();
        }



        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Có dữ liệu" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.DA_NHAP, DisplayItem = "Có cấp phát" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.DA_NHAP_SO_DU_TOAN, DisplayItem = "Có dự toán" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.DA_NHAP_CON_LAI, DisplayItem = "Có còn lại" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Hiển thị tất cả" });
            SelectedTypeDisplays = TypeDisplay.DA_NHAP_SO_DU_TOAN;
        }

        private void LoadDonVi()
        {
            string listIdDonVi = string.Empty;
            DataDonVi = new ObservableCollection<CheckBoxItem>();
            if (IsTongHop && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
            {
                var predicate = PredicateBuilder.True<NsCpChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                List<string> listSoChungTu = Model.DSSoChungTuTongHop.Split(",").ToList();
                predicate = predicate.And(x => listSoChungTu.Contains(x.SSoChungTu));
                List<NsCpChungTu> listChungTu = _chungTuService.FindByCondition(predicate).ToList();
                listIdDonVi = string.Join(",", listChungTu.Select(x => x.SDsidMaDonVi));
            }
            else
                listIdDonVi = Model.IdDonVi;
            IEnumerable<DonVi> listDonVi = _donViService.FindByListIdDonVi(listIdDonVi, _sessionInfo.YearOfWork).OrderBy(x => x.IIDMaDonVi);
            DataDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonVi);
        }

        private void LoadBudgetIndexCondition()
        {
            List<NsMuclucNgansachModel> listBudgetCatalog = new List<NsMuclucNgansachModel>();
            BudgetIndexForBudgetCriteria searchCondition = new BudgetIndexForBudgetCriteria
            {
                LNS = Model.Lns,
                YearOfWork = _sessionInfo.YearOfWork,
                GenerateAgencyId = _sessionInfo.IdDonVi
            };
            List<NsMucLucNganSach> listMucLucNganSach = _mucLucNganSachService.FindByDefenseBudget(searchCondition);
            listMucLucNganSach = listMucLucNganSach.GroupBy(n => n.XauNoiMa).Select(n => n.First()).ToList();
            listBudgetCatalog = _mapper.Map<List<NsMuclucNgansachModel>>(listMucLucNganSach);
            listBudgetCatalog.Insert(0, new NsMuclucNgansachModel(string.Empty, "-- TẤT CẢ --"));
            BudgetCatalogItems = new ObservableCollection<NsMuclucNgansachModel>(listBudgetCatalog);
            _budgetCatalogFilter = CollectionViewSource.GetDefaultView(BudgetCatalogItems);
            _budgetCatalogFilter.Filter = OnBudgetCatalogFilter;
        }

        private Expression<Func<NguoiDungDonVi, bool>> CreatePredicateUser()
        {
            var session = _sessionInfo;
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => x.IIDMaNguoiDung == _sessionInfo.Principal);
            return predicate;
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsDelete));
        }

        private List<NsCpChungTu> GetListCpChungTu(string soChungTus)
        {
            if (!string.IsNullOrEmpty(soChungTus))
            {
                List<string> listSoChungTu = soChungTus.Split(",").ToList();
                var predicate = PredicateBuilder.True<NsCpChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicate = predicate.And(x => listSoChungTu.Contains(x.SSoChungTu));
                return _chungTuService.FindByCondition(predicate).ToList();
            }
            else
            {
                return new List<NsCpChungTu>();
            }
        }

        private void CalculateParentSummary(ref List<CpChungTuChiTietQuery> listDetail, CpChungTuChiTietQuery currentItem, CpChungTuChiTietQuery selfItem)
        {
            var parentItem = listDetail.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.DeNghiDonVi += selfItem.DeNghiDonVi;
            parentItem.DuToan += selfItem.DuToan;
            parentItem.DaCap += selfItem.DaCap;
            CalculateParentSummary(ref listDetail, parentItem, selfItem);
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                base.LoadData(args);
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    if (Model != null)
                    {
                        _searchCondition = new AllocationDetailCriteria
                        {
                            VoucherId = Model.Id.ToString(),
                            //LNS = String.Join(",", Model.Lns.Split(",").Skip(PreviousLNS).Take(NextLNS - PreviousLNS)),
                            LNS = Model.Lns,
                            YearOfWork = _sessionInfo.YearOfWork,
                            YearOfBudget = _sessionInfo.YearOfBudget,
                            Type = Model.ILoai,
                            BudgetSource = _sessionInfo.Budget,
                            AgencyId = Model.IdDonVi,
                            VoucherDate = Model.NgayChungTu,
                            UserName = _sessionInfo.Principal,
                            PhanCap = DynamicMLNS.FormatLevel(Model.ITypeMoTa),
                            //PhanCap = "NG",
                            DonViTongHop = string.Empty,
                            IsCapPhatToanDonVi = _isCapPhatToanDonVi
                        };
                    }
                    if (!IsEditByRole)
                    {
                        if (!_sessionInfo.IsQuanLyDonViCha)
                            _searchCondition.AgencyId = _sessionInfo.IdsDonViQuanLy;
                    }

                    if (IsTongHop)
                    {
                        List<string> voucherNos = Model.DSSoChungTuTongHop.Split(",").ToList();
                        List<NsCpChungTu> listChungTu = _chungTuService.FindByAggregateVoucher(voucherNos, _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget).ToList();
                        _listChungTuChiTiet = new List<CpChungTuChiTietQuery>();
                        List<CpChungTuChiTietQuery> listChungTuChiTietParent = new List<CpChungTuChiTietQuery>();
                        List<CpChungTuChiTietQuery> listChungTuChiTietChildren = new List<CpChungTuChiTietQuery>();
                        foreach (var chungTu in listChungTu)
                        {
                            var searchCondition = new AllocationDetailCriteria
                            {
                                VoucherId = chungTu.Id.ToString(),
                                LNS = chungTu.SDslns,
                                YearOfWork = _sessionInfo.YearOfWork,
                                YearOfBudget = _sessionInfo.YearOfBudget,
                                BudgetSource = _sessionInfo.Budget,
                                AgencyId = chungTu.SDsidMaDonVi,
                                VoucherDate = chungTu.DNgayChungTu,
                                UserName = _sessionInfo.Principal,
                                PhanCap = DynamicMLNS.FormatLevel(chungTu.ITypeMoTa),
                                IsCapPhatToanDonVi = _isCapPhatToanDonVi
                            };
                            var listQuery = _chungTuChiTietService.FindChungTuChiTietByCondition(searchCondition, false);
                            listChungTuChiTietParent.AddRange(listQuery.Where(x => x.BHangCha));
                            if (SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI)
                                listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.BHangCha));
                            else listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.BHangCha && x.HasDataSummary));
                        }
                        List<string> listXauNoiMa = listChungTuChiTietChildren.Select(x => x.XauNoiMa).Distinct().ToList();
                        listChungTuChiTietParent = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.StartsWith(x.XauNoiMa))).GroupBy(x => x.XauNoiMa).Select(x => x.First()).Distinct().ToList();
                        _listChungTuChiTiet.AddRange(listChungTuChiTietParent);
                        if (SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI)
                        {
                            DateTime dateTime = Model.NgayQuyetDinh != null ? Model.NgayQuyetDinh.Value : Model.NgayChungTu.Value;
                            var listChungTuChiTietNhanPhanBo = _dtChungTuChiTietService.FindAll(x => x.IDuLieuNhan == 0
                                                    && x.INamLamViec == _sessionService.Current.YearOfWork
                                                    && x.IIdMaNguonNganSach == _sessionService.Current.Budget
                                                    && x.INamNganSach == _sessionService.Current.YearOfBudget).ToList();
                            var listChungTuNhanPhanBo = _dtChungTuService.FindByCondition(x =>
                                                    x.ILoai == SoChungTuType.ReceiveEstimate
                                                    && x.INamLamViec == _sessionService.Current.YearOfWork
                                                    && x.IIdMaNguonNganSach == _sessionService.Current.Budget
                                                    && (x.DNgayQuyetDinh.HasValue && x.DNgayQuyetDinh.Value.Date <= dateTime) || (!x.DNgayQuyetDinh.HasValue && x.DNgayChungTu.HasValue && x.DNgayChungTu.Value.Date <= dateTime)
                                                    && x.INamNganSach == _sessionService.Current.YearOfBudget).ToList();

                            var lstChiTiet = from x in listChungTuChiTietNhanPhanBo
                                             join y in listChungTuNhanPhanBo on x.IIdDtchungTu equals y.Id
                                             select x;
                            var duToanChiTiet = lstChiTiet.GroupBy(x => x.IIdMlns).Select(x => new
                            {
                                MlnsId = x.First().IIdMlns,
                                MlnsIdParent = x.First().IIdMlnsCha,
                                DuToan = x.Sum(y => y.FTuChi)
                            }).ToList();

                            var listChungTuChiTietChildrenGroup = listChungTuChiTietChildren.GroupBy(x => x.MlnsId)
                            .Select(x => new CpChungTuChiTietQuery
                            {
                                MlnsId = x.First().MlnsId,
                                MlnsIdParent = x.First().MlnsIdParent,
                                XauNoiMa = x.First().XauNoiMa,
                                Lns = x.First().Lns,
                                L = x.First().L,
                                K = x.First().K,
                                M = x.First().M,
                                Tm = x.First().Tm,
                                Ttm = x.First().Ttm,
                                Ng = x.First().Ng,
                                Tng = x.First().Tng,
                                MoTa = x.First().MoTa,
                                DeNghiDonVi = x.Sum(n => n.DeNghiDonVi),
                                TuChi = x.Sum(n => n.TuChi),
                            }).ToList();

                            var item = from x in listChungTuChiTietChildrenGroup
                                       join y in duToanChiTiet on x.MlnsId equals y.MlnsId
                                       select new CpChungTuChiTietQuery
                                       {
                                           MlnsId = x.MlnsId,
                                           MlnsIdParent = x.MlnsIdParent,
                                           XauNoiMa = x.XauNoiMa,
                                           Lns = x.Lns,
                                           L = x.L,
                                           K = x.K,
                                           M = x.M,
                                           Tm = x.Tm,
                                           Ttm = x.Ttm,
                                           Ng = x.Ng,
                                           Tng = x.Tng,
                                           MoTa = x.MoTa,
                                           DeNghiDonVi = x.DeNghiDonVi,
                                           TuChi = x.TuChi,
                                           DuToan = y.DuToan
                                       };


                            _listChungTuChiTiet.AddRange(item);
                        }
                        else _listChungTuChiTiet.AddRange(listChungTuChiTietChildren);
                        _listChungTuChiTiet = _listChungTuChiTiet.OrderBy(x => x.XauNoiMa).ToList();
                    }
                    else
                        _listChungTuChiTiet = _chungTuChiTietService.FindChungTuChiTietByCondition(_searchCondition, SelectedTypeDisplays == TypeDisplay.TAT_CA).ToList();

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        DonVi donvi0 = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
                        //Check user
                        var predicate = CreatePredicateUser();
                        List<NguoiDungDonVi> listNguoiDungDonVi = _nguoiDungDonViService.FindAll(predicate).ToList();

                        if (listNguoiDungDonVi != null && listNguoiDungDonVi.Count > 0 && donvi0 != null)
                        {
                            NguoiDungDonVi itemNguoiDungDonVi = listNguoiDungDonVi.Where(n => n.IIdMaDonVi == donvi0.IIDMaDonVi).FirstOrDefault();
                            if (itemNguoiDungDonVi == null)
                            {
                                List<string> listIdDonViUser = listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList();
                                _listChungTuChiTiet = _listChungTuChiTiet.Where(x => x.BHangCha || (!x.BHangCha && listIdDonViUser.Contains(x.IdDonVi))).ToList();
                            }
                        }

                        var data = _mapper.Map<List<AllocationDetailModel>>(_listChungTuChiTiet);
                        FormatDetailExport(Model, data);

                        Items = _mapper.Map<ObservableCollection<AllocationDetailModel>>(data);
                        Items.Select(n => n.IsCreate = IsCreate).ToList();

                        OnCalculateData();

                        double fSoCapPhat = 0;
                        fSoCapPhat = _chungTuService.FindById(Model.Id).SoCapPhat == null ? 0 : _chungTuService.FindById(Model.Id).SoCapPhat.Value;
                        if (IsCreate)
                        {
                            //Items.Select(n => { n.DeNghiDonVi = n.DuToan * (fSoCapPhat / 100); n.TuChi = n.DuToan * (fSoCapPhat / 100); return n; }).ToList();    
                            //Items.Where(n => !n.IsHangCha).Select(n => { n.IsModified = true; return n; }).ToList();
                            foreach (var item in Items)
                            {
                                if (item.DuToan * (fSoCapPhat / 100) != item.DeNghiDonVi)
                                {
                                    item.DeNghiDonVi = item.DuToan * (fSoCapPhat / 100);
                                    item.TuChi = item.DuToan * (fSoCapPhat / 100);

                                    if (!item.IsHangCha)
                                    {
                                        item.IsModified = true;
                                    }
                                }
                            }
                        }

                        _dataDetailAllocationFilter = CollectionViewSource.GetDefaultView(Items);
                        _dataDetailAllocationFilter.Filter = ItemsViewFilter;
                        if (Items != null && Items.Count > 0)
                        {
                            SelectedItem = Items.FirstOrDefault();
                        }

                        Items.Where(x => !x.IsHangCha).ForAll(y =>
                        {
                            y.PropertyChanged += DetailModel_PropertyChanged;
                        });

                        ColumnDisplay = DynamicMLNS.SettingColumnVisibilityByString(Model.ITypeMoTa);
                        OnPropertyChanged(nameof(IsReadOnlyGrid));
                        OnPropertyChanged(nameof(Items));
                        OnPropertyChanged(nameof(IsSaveData));
                        OnPropertyChanged(nameof(IsDeleteAll));
                        OnPropertyChanged(nameof(VisibleColAgency));
                        OnPropertyChanged(nameof(VisibleVoucherNo));
                        OnPropertyChanged(nameof(ShowTotal));
                        OnRefeshFilter();
                    }
                    else
                        _logger.Error(e.Error.Message);
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private List<NguoiDungDonVi> GetListNguoiDungDonVi()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.IIDMaNguoiDung.Equals(_sessionInfo.Principal));
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            List<NguoiDungDonVi> nsDungDonVis = _nguoiDungDonViService.FindAll(predicate).ToList();
            return nsDungDonVis;
        }

        private void CountParentChildReverse(List<AllocationDetailModel> listChild, AllocationDetailModel parent)
        {
            if (parent == null) return;
            parent.TuChi += listChild.Sum(n => n.TuChi);
            parent.DeNghiDonVi += listChild.Sum(n => n.DeNghiDonVi);
            parent.DaCap += listChild.Sum(n => n.DaCap);
            parent.DuToan += listChild.Sum(n => n.DuToan);
        }

        private void OnCalculateData()
        {
            Items.Where(x => x.IsHangCha).ForAll(x =>
            {
                x.DuToan = 0;
                x.DaCap = 0;
                x.TuChi = 0;
                x.DeNghiDonVi = 0;
            });

            var temps = Items.Where(x => x.IsFilter && !x.IsHangCha && !x.IsDeleted && (x.DuToan != 0 || x.DaCap != 0 || x.ConLai != 0 || x.TuChi != 0 || x.DeNghiDonVi != 0))
                .Where(x => x.MlnsIdParent.HasValue)
                .GroupBy(n => new { n.MlnsIdParent, n.IdDonVi }).ToList();
            var listParent = new List<AllocationDetailModel>();
            var dictItems = Items.GroupBy(x => x.MlnsId).ToDictionary(x => x.Key, x => x.FirstOrDefault());

            void CalculateReverse(List<AllocationDetailModel> items)
            {
                var temps = items.GroupBy(n => new { n.MlnsIdParent, n.IdDonVi }).ToList();
                var listParent = new List<AllocationDetailModel>();
                foreach (var temp in temps)
                {
                    _ = dictItems.TryGetValue(temp.Key.MlnsIdParent ?? Guid.Empty, out var parent);
                    if (parent != null)
                    {
                        CountParentChildReverse(temp.ToList(), parent);
                        listParent.Add(parent);
                    }

                }
                if (listParent.Count > 0)
                {
                    CalculateReverse(listParent);
                }
            }

            foreach (var temp in temps)
            {
                _ = dictItems.TryGetValue(temp.Key.MlnsIdParent ?? Guid.Empty, out var parent);
                if (parent != null)
                {
                    CountParentChildReverse(temp.ToList(), parent);
                    listParent.Add(parent);
                }

            }
            if (listParent.Count > 0)
            {
                CalculateReverse(listParent);
            }

            OnCalculateTotal();
        }

        
        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.TuChi = 0; x.DaCap = 0; x.DuToan = 0; x.DeNghiDonVi = 0;
                    return x;
                }).ToList();
            var temp = Items.Where(x => x.IsFilter && !x.IsHangCha && !x.IsDeleted && (x.DuToan != 0 || x.DaCap != 0 || x.ConLai != 0 || x.TuChi != 0 || x.DeNghiDonVi != 0)).ToList();
            foreach (var item in temp)
            {
                CalculateParent(item, item);
            }
            CalculateTotal();
        }

        private void OnRefeshFilter()
        {
            if (!string.IsNullOrEmpty(SelectedDonVi))
            {
                List<AllocationDetailModel> dataDonViFilter = Items.Where(n => n.IdDonVi == SelectedDonVi).ToList();
                _xauNoiMaDonViFilter = StringUtils.GetListXauNoiMaParent(dataDonViFilter.Select(n => n.XauNoiMa).ToList());
            }
            _dataDetailAllocationFilter.Refresh();
            OnCalculateData();
        }

        private void CalculateParent(AllocationDetailModel currentItem, AllocationDetailModel selfItem)
        {
            var parentItem = Items.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.DeNghiDonVi += selfItem.DeNghiDonVi;
            parentItem.DuToan += selfItem.DuToan;
            parentItem.DaCap += selfItem.DaCap;
            CalculateParent(parentItem, selfItem);
        }

        private void OnSaveData()
        {
            try
            {
                List<AllocationDetailModel> allocationDetailsAdd = Items.Where(x => x.IsModified && (x.IdChungTu == Guid.Empty || x.IdChungTu == null) && !x.IsDeleted).ToList();
                List<AllocationDetailModel> allocationDetailsUpdate = Items.Where(x => x.IsModified && x.IdChungTu != Guid.Empty && x.IdChungTu != null && !x.IsDeleted).ToList();
                List<AllocationDetailModel> allocationDetailsDelete = Items.Where(x => x.IsDeleted && x.IdChungTu != Guid.Empty && x.IdChungTu != null).ToList();
                List<DonVi> listDonVi = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();

                // Thêm mới chứng từ chi tiết
                if (allocationDetailsAdd.Count > 0)
                {
                    allocationDetailsAdd = allocationDetailsAdd.Select(x =>
                    {
                        x.Id = Guid.NewGuid();
                        x.IdChungTu = Model.Id;
                        x.ILoai = Model.ILoai;
                        x.ITrangThai = 1;
                        x.NamLamViec = Model.NamLamViec;
                        x.NamNganSach = Model.NamNganSach;
                        x.IsModified = false;
                        x.DateCreated = DateTime.Now;
                        x.NguonNganSach = Model.NguonNganSach.HasValue ? Model.NguonNganSach.Value : 0;
                        x.UserCreator = _sessionInfo.Principal;
                        return x;
                    }).ToList();
                    List<NsCpChungTuChiTiet> listChungTuChiTiets = new List<NsCpChungTuChiTiet>();
                    listChungTuChiTiets = _mapper.Map<List<NsCpChungTuChiTiet>>(allocationDetailsAdd);
                    _chungTuChiTietService.AddRange(listChungTuChiTiets);
                    Items.Where(x => x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
                }

                // Cập nhật chứng từ chi tiết
                if (allocationDetailsUpdate.Count > 0)
                {
                    foreach (var item in allocationDetailsUpdate.Where(n => n.TuChi != 0 || n.DeNghiDonVi != 0))
                    {
                        item.IsModified = false;
                        NsCpChungTuChiTiet chungTuChiTiet = _chungTuChiTietService.Find(item.Id);
                        _mapper.Map(item, chungTuChiTiet);
                        chungTuChiTiet.DNgaySua = DateTime.Now;
                        chungTuChiTiet.SNguoiSua = _sessionInfo.Principal;
                        _chungTuChiTietService.Update(chungTuChiTiet);
                    }
                    foreach (var item in allocationDetailsUpdate.Where(n => n.TuChi == 0 && n.DeNghiDonVi == 0))
                    {
                        _chungTuChiTietService.Delete(item.Id);
                        ResetVoucherDetailData(item);

                        // Reset flag
                        item.IsModified = false;
                        item.IsDeleted = false;
                        item.IdChungTu = Guid.Empty;
                    }
                }

                // Delete
                if (allocationDetailsDelete.Count > 0)
                {
                    foreach (var item in allocationDetailsDelete)
                    {
                        _chungTuChiTietService.Delete(item.Id);
                        ResetVoucherDetailData(item);

                        // Reset flag
                        item.IsModified = false;
                        item.IsDeleted = false;
                        item.IdChungTu = Guid.Empty;
                    }
                }

                IsCreate = false;

                if (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    List<NsCpChungTu> listChungTu = GetListCpChungTu(Model.DSSoChungTuTongHop);
                    if (listChungTu != null && listChungTu.Count > 0)
                    {
                        foreach (var item in listChungTu)
                        {
                            _chungTuService.UpdateTotalCPChungTu(item.Id.ToString(), _sessionInfo.Principal);
                        }
                    }

                    List<NsCpChungTuChiTiet> listDetailChild = GetChildDetail(listChungTu);

                    var predicate = PredicateBuilder.True<NsCpChungTuChiTiet>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                    predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                    predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                    predicate = predicate.And(x => x.IIdCtcapPhat.HasValue && x.IIdCtcapPhat == Model.Id);
                    List<NsCpChungTuChiTiet> listDetailSummary = _chungTuChiTietService.FindByCondition(predicate).ToList();

                    if (listDetailChild != null && listDetailChild.Count > 0 && listDetailSummary != null && listDetailSummary.Count > 0)
                    {
                        foreach (var item in listDetailSummary)
                        {
                            item.FTuChi = listDetailChild.Where(n => n.IIdMlns == item.IIdMlns).Sum(n => n.FTuChi);
                        }
                    }
                    foreach (var item in listDetailSummary)
                    {
                        _chungTuChiTietService.Update(item);
                    }
                }

                _chungTuService.UpdateTotalCPChungTu(Model.Id.ToString(), _sessionInfo.Principal);
                LoadData();
                MessageBoxHelper.Info(Resources.MsgSaveDone);
                SavedAction?.Invoke(null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private AllocationDetailModel ResetVoucherDetailData(AllocationDetailModel voucherDetail)
        {
            voucherDetail.DaCap = 0;
            voucherDetail.DuToan = 0;
            voucherDetail.TuChi = 0;
            voucherDetail.DeNghiDonVi = 0;
            voucherDetail.GhiChu = string.Empty;
            return voucherDetail;
        }

        private List<NsCpChungTuChiTiet> GetChildDetail(List<NsCpChungTu> listChungTu)
        {
            var predicate = PredicateBuilder.True<NsCpChungTuChiTiet>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.IIdCtcapPhat.HasValue && listChungTu.Select(n => n.Id).ToList().Contains(x.IIdCtcapPhat.Value));
            List<NsCpChungTuChiTiet> listDetail = _chungTuChiTietService.FindByCondition(predicate).ToList();
            return listDetail;
        }

        protected override void OnLockUnLock()
        {
            try
            {
                if (Model == null)
                    return;
                if (!IsEditByRole)
                {
                    MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.UserCreator));
                    return;
                }

                if (Model.IsLocked)
                {
                    List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                    {
                        MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                        return;
                    }
                    if (Model.BDaTongHop.GetValueOrDefault())
                    {
                        MessageBoxHelper.Warning(Resources.AlertUnlockAggregatedVoucher);
                        return;
                    }
                }
                else
                {
                    if (Model.UserCreator != _sessionInfo.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, Model.UserCreator));
                        return;
                    }
                }

                string msgConfirm = string.Format(Model.IsLocked ? Resources.MsgUnLock : Resources.MsgLock, Environment.NewLine, Environment.NewLine);
                string msgDone = Model.IsLocked ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                MessageBoxResult dialogResult = MessageBoxHelper.Confirm(msgConfirm);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    _chungTuService.LockOrUnLock(Model.Id, !Model.IsLocked);
                    Model.IsLocked = !Model.IsLocked;
                    OnPropertyChanged(nameof(IsSaveData));
                    MessageBoxHelper.Info(msgDone);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd()
        {
            if (!IsEditByRole)
            {
                MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.UserCreator));
                return;
            }
            if (SelectedItem != null)
            {
                int currentRow = Items.IndexOf(SelectedItem);
                int targetRow = Items.ToList().FindIndex(currentRow, x => x.IsEditable);
                if (targetRow > -1)
                {
                    AllocationDetailModel sourceItem = Items.ElementAt(targetRow);
                    AllocationDetailModel targetItem = ObjectCopier.Clone(sourceItem);

                    targetItem.Id = Guid.NewGuid();
                    targetItem.IdChungTu = null;
                    targetItem.IdDonVi = SelectedItem.IdDonVi;
                    targetItem.TuChi = 0;
                    targetItem.HienVat = 0;
                    targetItem.GhiChu = null;
                    targetItem.IsModified = true;
                    targetItem.PropertyChanged += DetailModel_PropertyChanged;

                    Items.Insert(targetRow + 1, targetItem);
                    OnPropertyChanged(nameof(Items));
                    OnPropertyChanged(nameof(IsSaveData));
                }
            }
        }

        protected override void OnDelete()
        {
            if (!IsEditByRole)
            {
                MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.UserCreator));
                return;
            }
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha && !Model.IsLocked)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                OnCalculateData();
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        protected override void OnRefresh()
        {
            IsOpenRefresh = !IsOpenRefresh;
        }

        private void OnRefreshAllData()
        {
            try
            {
                if (IsSaveData)
                {
                    var result = MessageBoxHelper.ConfirmCancel(Resources.ConfirmReloadData);
                    if (result == MessageBoxResult.Cancel)
                        return;
                    else if (result == MessageBoxResult.Yes)
                        OnSaveData();
                }
                IsCreate = false;
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnAutoFillData()
        {
            if (!IsEditByRole)
            {
                MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.UserCreator));
                return;
            }
            IsLoading = true;
            foreach (AllocationDetailModel item in Items.Where(x => !x.IsHangCha))
            {
                if (item.ConLai != 0)
                {
                    item.TuChi = item.ConLai;
                    if (item.DeNghiDonVi == 0)
                        item.DeNghiDonVi = item.TuChi;
                    item.IsModified = true;
                }
            }
            IsLoading = false;
            OnCalculateData();
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void OnCloseWindow()
        {
            DataChangedEventHandler handler = ClosePopup;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void CalculateTotal()
        {
            Model.TongCapPhat = 0;
            Model.TongDuToan = 0;
            Model.TongConLai = 0;
            Model.TongDaCap = 0;
            Model.TongDonViDeNghi = 0;
            var listMlnsId = Items.Select(n => n.MlnsId).ToList();
            List<AllocationDetailModel> listChildren = Items.Where(x => x.IsHangCha && !x.IsDeleted && x.IsFilter && (!x.MlnsIdParent.HasValue || !listMlnsId.Contains((Guid)x.MlnsIdParent))).ToList();
            //List<AllocationDetailModel> listChildren = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();

            if (listChildren.Count > 1)
            {
                foreach (AllocationDetailModel item in listChildren)
                {
                    Model.TongCapPhat += item.TuChi;
                    Model.TongDuToan += item.DuToan;
                    Model.TongConLai += item.ConLai;
                    Model.TongDaCap += item.DaCap;
                    Model.TongDonViDeNghi += item.DeNghiDonVi;
                }
            }
            else
            {
                var child = Items.FirstOrDefault(x => x.IsHangCha && !x.IsDeleted && x.IsFilter);
                if (child != null)
                {
                    Model.TongCapPhat = child.TuChi;
                    Model.TongDuToan = child.DuToan;
                    Model.TongConLai = child.ConLai;
                    Model.TongDaCap = child.DaCap;
                    Model.TongDonViDeNghi = child.DeNghiDonVi;
                }

            }
        }

        private void OnCalculateTotal()
        {
            Model.TongCapPhat = 0;
            Model.TongDuToan = 0;
            Model.TongConLai = 0;
            Model.TongDaCap = 0;
            Model.TongDonViDeNghi = 0;
            var listMlnsId = Items.Select(n => n.MlnsId).ToList();
            List<AllocationDetailModel> listChildren = Items.Where(x => x.IsHangCha && !x.IsDeleted && x.IsFilter && (!x.MlnsIdParent.HasValue || !listMlnsId.Contains((Guid)x.MlnsIdParent))).ToList();

            if (listChildren.Count > 1)
            {
                foreach (AllocationDetailModel item in listChildren)
                {
                    Model.TongCapPhat += item.TuChi;
                    Model.TongDuToan += item.DuToan;
                    Model.TongConLai += item.ConLai;
                    Model.TongDaCap += item.DaCap;
                    Model.TongDonViDeNghi += item.DeNghiDonVi;
                }
            }
            else
            {
                var child = Items.FirstOrDefault(x => x.IsHangCha && !x.IsDeleted && x.IsFilter);
                if (child != null)
                {
                    Model.TongCapPhat = child.TuChi;
                    Model.TongDuToan = child.DuToan;
                    Model.TongConLai = child.ConLai;
                    Model.TongDaCap = child.DaCap;
                    Model.TongDonViDeNghi = child.DeNghiDonVi;
                }

            }
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => AllocationDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.XauNoiMa).ToHashSet());
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (AllocationDetailModel)obj;
            result = AllocationDetailFilter(item);
            if (!result && item.IsHangCha)
            {
                if (string.IsNullOrEmpty(item.L))
                    result = xnmConcatenation.StartsWith(item.Lns);
                else result = xnmConcatenation.Contains(item.XauNoiMa);
            }
            if (result)
                item.IsFilter = result;
            return result;
        }


        private bool AllocationDetailFilter(object obj)
        {
            bool result = true;
            var item = (AllocationDetailModel)obj;
            if (!string.IsNullOrEmpty(SelectedLNS))
                result = result && item.Lns.ToLower().StartsWith(SelectedLNS.Trim().ToLower());

            if (!string.IsNullOrEmpty(SelectedTypeDisplays))
            {
                if (SelectedTypeDisplays == TypeDisplay.DA_NHAP)
                    result = result && (item.TuChi != 0 || (item.IsModified && (item.IdChungTu == Guid.Empty || item.IdChungTu == null) && !item.IsDeleted));
                else if (SelectedTypeDisplays == TypeDisplay.DA_NHAP_SO_DU_TOAN)
                    result = result && (item.DuToan != 0 || (item.IsModified && (item.IdChungTu == Guid.Empty || item.IdChungTu == null) && !item.IsDeleted));
                else if (SelectedTypeDisplays == TypeDisplay.DA_NHAP_CON_LAI)
                    result = result && (item.ConLai != 0 || (item.IsModified && (item.IdChungTu == Guid.Empty || item.IdChungTu == null) && !item.IsDeleted));
                else if (SelectedTypeDisplays == TypeDisplay.CO_DU_LIEU)
                    result = result && (item.DuToan != 0 || item.DaCap != 0 || item.ConLai != 0 || item.TuChi != 0 || item.DeNghiDonVi != 0
                        || (item.IsModified && (item.IdChungTu == Guid.Empty || item.IdChungTu == null) && !item.IsDeleted));
            }

            if (!string.IsNullOrEmpty(SelectedDonVi))
            {
                result = result && ((!string.IsNullOrEmpty(item.IdDonVi) && item.IdDonVi.ToLower().StartsWith(SelectedDonVi.ToLower()))
                    || (item.IsHangCha && _xauNoiMaDonViFilter.Contains(item.XauNoiMa)));
            }

            if (!string.IsNullOrEmpty(DetailFilter.L))
                result = result && item.L.ToLower().StartsWith(DetailFilter.L.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.K))
                result = result && item.K.ToLower().StartsWith(DetailFilter.K.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.M.ToLower().StartsWith(DetailFilter.M.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.Tm.ToLower().StartsWith(DetailFilter.TM.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.Ttm.ToLower().StartsWith(DetailFilter.TTM.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.Ng.ToLower().StartsWith(DetailFilter.NG.Trim().ToLower());

            item.IsFilter = result;
            return result;
        }

        private void FormatDetailExport(AllocationModel itemAllocation, List<AllocationDetailModel> listAllocationDetail)
        {
            List<CpChungTuChiTietQuery> result = new List<CpChungTuChiTietQuery>();
            string chiTietToi = DynamicMLNS.FormatLevel(itemAllocation.ITypeMoTa);
            switch (chiTietToi)
            {
                case "NG":
                    listAllocationDetail = listAllocationDetail.Where(n =>
                    (!string.IsNullOrEmpty(n.Ng) && string.IsNullOrEmpty(n.Tng)
                                                    && string.IsNullOrEmpty(n.TNG1) && string.IsNullOrEmpty(n.TNG2) && string.IsNullOrEmpty(n.TNG3))
                    || (string.IsNullOrEmpty(n.Ng) && !string.IsNullOrEmpty(n.Ttm))
                    || string.IsNullOrEmpty(n.L) || string.IsNullOrEmpty(n.K) || string.IsNullOrEmpty(n.M)
                    || string.IsNullOrEmpty(n.Tm) || string.IsNullOrEmpty(n.Ttm)).ToList();
                    listAllocationDetail.Where(n => !string.IsNullOrEmpty(n.Ng)).Select(n => { n.IsHangCha = false; return n; }).ToList();
                    listAllocationDetail.Where(n => string.IsNullOrEmpty(n.Ng)).Select(n => { n.IsHangCha = true; return n; }).ToList();
                    break;
                case "M":
                    listAllocationDetail = listAllocationDetail.Where(n =>
                    (!string.IsNullOrEmpty(n.M)
                    && string.IsNullOrEmpty(n.Tm) && string.IsNullOrEmpty(n.Ttm) && string.IsNullOrEmpty(n.Ng)
                    && string.IsNullOrEmpty(n.Tng) && string.IsNullOrEmpty(n.TNG1) && string.IsNullOrEmpty(n.TNG2) && string.IsNullOrEmpty(n.TNG3))
                    || string.IsNullOrEmpty(n.L) || string.IsNullOrEmpty(n.K) || string.IsNullOrEmpty(n.M)).ToList();
                    listAllocationDetail.Where(n => !string.IsNullOrEmpty(n.M)).Select(n => { n.IsHangCha = false; return n; }).ToList();
                    listAllocationDetail.Where(n => string.IsNullOrEmpty(n.M)).Select(n => { n.IsHangCha = true; return n; }).ToList();
                    break;
                case "TM":
                    listAllocationDetail = listAllocationDetail.Where(n =>
                    (!string.IsNullOrEmpty(n.Tm)
                    && string.IsNullOrEmpty(n.Ttm) && string.IsNullOrEmpty(n.Ng)
                    && string.IsNullOrEmpty(n.Tng) && string.IsNullOrEmpty(n.TNG1) && string.IsNullOrEmpty(n.TNG2) && string.IsNullOrEmpty(n.TNG3))
                    || string.IsNullOrEmpty(n.L) || string.IsNullOrEmpty(n.K) || string.IsNullOrEmpty(n.M)
                    || string.IsNullOrEmpty(n.Tm)).ToList();
                    listAllocationDetail.Where(n => !string.IsNullOrEmpty(n.Tm)).Select(n => { n.IsHangCha = false; return n; }).ToList();
                    listAllocationDetail.Where(n => string.IsNullOrEmpty(n.Tm)).Select(n => { n.IsHangCha = true; return n; }).ToList();
                    break;
                default:
                    break;
            }
            listAllocationDetail = listAllocationDetail.OrderBy(x => x.XauNoiMa).ToList();
        }

        private bool OnBudgetCatalogFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.Lns.ToLower().Contains(_searchLNS!.ToLower());
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            if (!IsEditByRole)
            {
                MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.UserCreator));
                return;
            }
            if (Model.IsLocked)
            {
                return;
            }
            var result = MessageBoxHelper.Confirm(Resources.DeleteAllChungTuChiTiet);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                List<AllocationDetailModel> allocationDetailDelete = Items.Where(x => x.IdChungTu != Guid.Empty && x.IdChungTu != null && x.IsFilter).ToList();
                if (allocationDetailDelete.Count > 0)
                {
                    foreach (var item in allocationDetailDelete)
                    {
                        _chungTuChiTietService.Delete(item.Id);
                    }
                }
                if (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    List<NsCpChungTu> listChungTu = GetListCpChungTu(Model.DSSoChungTuTongHop);
                    foreach (var item in listChungTu)
                    {
                        _chungTuService.UpdateTotalCPChungTu(item.Id.ToString(), _sessionInfo.Principal);
                    }
                }
                else
                {
                    _chungTuService.UpdateTotalCPChungTu(Model.Id.ToString(), _sessionInfo.Principal);
                }
                LoadData();
                MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (!IsLoading && (args.PropertyName == nameof(AllocationDetailModel.TuChi) ||
                args.PropertyName == nameof(AllocationDetailModel.DeNghiDonVi) ||
                args.PropertyName == nameof(AllocationDetailModel.GhiChu) ||
                args.PropertyName == nameof(AllocationDetailModel.IdDonVi)))
            {
                AllocationDetailModel item = Items.Where(x => x.Id == ((AllocationDetailModel)sender).Id).First();
                item.IsModified = true;
                if (args.PropertyName == nameof(AllocationDetailModel.TuChi) || args.PropertyName == nameof(AllocationDetailModel.DeNghiDonVi))
                {
                    OnCalculateData();
                }
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }


        private void OpenPrintDialog(object param)
        {
            try
            {
                int dialogType = (int)param;
                switch (dialogType)
                {
                    case (int)AllocationPrintType.PRINT_AllOCATION_NOTICE:
                        PrintAllocationNoticeViewModel.Init();
                        var view1 = new PrintAllocationNotice
                        {
                            DataContext = PrintAllocationNoticeViewModel
                        };
                        DialogHost.Show(view1, "AllocationDetailWindow", null, null);
                        break;
                    case (int)AllocationPrintType.PRINT_ALLOCATION_DONVI:
                        PrintAllocationDonViViewModel.Init();
                        var view2 = new PrintAllocationDonVi
                        {
                            DataContext = PrintAllocationDonViViewModel
                        };
                        DialogHost.Show(view2, "AllocationDetailWindow", null, null);
                        break;
                    case (int)AllocationPrintType.PRINT_ALLOCATION_REQUEST:
                        PrintAllocationRequestViewModel.Init();
                        var view3 = new PrintAllocationRequest
                        {
                            DataContext = PrintAllocationRequestViewModel
                        };
                        DialogHost.Show(view3, "AllocationDetailWindow", null, null);
                        break;
                    case (int)AllocationPrintType.PRINT_ALLOCATION_TYPE:
                        PrintAllocationTypeViewModel.Init();
                        var view4 = new PrintAllocationType
                        {
                            DataContext = PrintAllocationTypeViewModel
                        };
                        DialogHost.Show(view4, "AllocationDetailWindow", null, null);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
