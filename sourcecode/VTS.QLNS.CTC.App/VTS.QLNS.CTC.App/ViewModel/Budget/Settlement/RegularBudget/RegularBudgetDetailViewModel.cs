using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Settlement.Explanation;
using VTS.QLNS.CTC.App.View.Budget.Settlement.GetDataLuong;
using VTS.QLNS.CTC.App.View.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Explanation;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.GetDataLuong;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.RegularBudget
{
    public class RegularBudgetDetailViewModel : DetailViewModelBase<SettlementVoucherModel, SettlementVoucherDetailModel>
    {
        private INsQtChungTuChiTietService _chungTuChiTietService;
        private INsQtChungTuService _chungTuService;
        private ISessionService _sessionService;
        private ITlQtChungTuService _iTlQtChungTuService;
        private INsQtChungTuChiTietGiaiThichService _chungTuChiTietGiaiThichService;
        private INsMucLucNganSachService _iNsMucLucNganSachService;
        private INsDonViService _donViService;
        private IMapper _mapper;
        private INsNguoiDungDonViService _nguoiDungDonViService;
        private SettlementVoucherDetailSearch _searchCondition;
        private ICollectionView _budgetCatalogItemsView;
        private ICollectionView _settlementVoucherDetailsView;
        private VerbalExplanationViewModel VerbalExplanationViewModel;
        private DataInterpretationViewModel DataInterpretationViewModel;
        private PrintSettlementVoucherViewModel PrintSettlementVoucherViewModel;
        private PrintCommunicateSettlementLNSViewModel PrintCommunicateSettlementLNSViewModel;
        private PrintSummaryLNSViewModel PrintSummaryLNSViewModel;
        private GetDataQtThuongXuyenLuongViewModel GetDataQtThuongXuyenLuongViewModel;
        private ICollection<SettlementVoucherDetailModel> _filterResult = new HashSet<SettlementVoucherDetailModel>();
        private string xnmConcatenation = "";
        private SessionInfo _sessionInfo;
        public bool FirstInit { get; set; }

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateSettlementVoucherEvent;
        public bool IsCalculating { get; set; } = false;
        public override Type ContentType => typeof(View.Budget.Settlement.RegularBudget.RegularBudgetDetail);
        public bool IsSaveData => _settlementVoucherDetails.Any(item => item.IsModified || item.IsDeleted);
        public bool IsDelete => _selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI ? false : (SelectedItem != null ? SelectedItem.HasData : false);
        public bool IsDeleteAll => _selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI ? false : _settlementVoucherDetails.Any(item => !item.IsModified && item.HasData);
        public bool IsGetDataLuong => !Model.BKhoa;
        public int NamLamViec { get; set; }
        public Guid IdChungTuLuong { get; set; }
        public bool IsTongHop => !string.IsNullOrEmpty(Model.STongHop) ? true : false;
        public bool IsShowAgencyFilter => IsTongHop && _selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI;
        public bool IsEditByRole => Model.SNguoiTao == _sessionInfo.Principal;
        public bool IsReadonlyDeNghi => _sessionService.Current.IsQuanLyDonViCha && IsTongHop && _selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI;

        private bool _isShowColumnDonVi;
        public bool IsShowColumnDonVi
        {
            get => _isShowColumnDonVi;
            set => SetProperty(ref _isShowColumnDonVi, value);
        }

        private ObservableCollection<SettlementVoucherDetailModel> _settlementVoucherDetails;
        public ObservableCollection<SettlementVoucherDetailModel> SettlementVoucherDetails
        {
            get => _settlementVoucherDetails;
            set => SetProperty(ref _settlementVoucherDetails, value);
        }

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
                if (SetProperty(ref _selectedBudgetCatalog, value) && value != null)
                {
                    DetailFilter.Lns = _selectedBudgetCatalog.Lns;
                    BeForeRefresh();
                    _settlementVoucherDetailsView.Refresh();
                    if (!IsCalculating) CalculateData();
                }
                IsOpenLnsPopup = false;
            }
        }

        private SettlementVoucherTotalModel _settlementVocherTotal;
        public SettlementVoucherTotalModel SettlementVoucherTotal
        {
            get => _settlementVocherTotal;
            set => SetProperty(ref _settlementVocherTotal, value);
        }

        private bool _isOpenLnsPopup;
        public bool IsOpenLnsPopup
        {
            get => _isOpenLnsPopup;
            set => SetProperty(ref _isOpenLnsPopup, value);
        }

        private ObservableCollection<ComboboxItem> _displayConditions;
        public ObservableCollection<ComboboxItem> DisplayConditions
        {
            get => _displayConditions;
            set => SetProperty(ref _displayConditions, value);
        }

        private ComboboxItem _selectedDisplayCondition;
        public ComboboxItem SelectedDisplayCondition
        {
            get => _selectedDisplayCondition;
            set
            {
                SetProperty(ref _selectedDisplayCondition, value);
                BeForeRefresh();
                _settlementVoucherDetailsView.Refresh();
                if (!IsCalculating) CalculateData();
            }
        }

        private NsMuclucNgansachModel _detailFilter;
        public NsMuclucNgansachModel DetailFilter
        {
            get => _detailFilter;
            set => SetProperty(ref _detailFilter, value);
        }

        private bool _isOpenExplainPopup;
        public bool IsOpenExplainPopup
        {
            get => _isOpenExplainPopup;
            set => SetProperty(ref _isOpenExplainPopup, value);
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
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
                    if (_selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
                        _isShowColumnDonVi = true;
                    else _isShowColumnDonVi = false;
                    LoadData();
                    OnPropertyChanged(nameof(IsShowColumnDonVi));
                    OnPropertyChanged(nameof(IsShowAgencyFilter));
                }
            }
        }

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private ComboboxItem _selectedAgency;
        public ComboboxItem SelectedAgency
        {
            get => _selectedAgency;
            set
            {
                SetProperty(ref _selectedAgency, value);
                BeForeRefresh();
                _settlementVoucherDetailsView.Refresh();
                if (!IsCalculating) CalculateData();
            }
        }

        public RelayCommand SaveDataCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetSearchCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ExplainCommand { get; }
        public RelayCommand DataInterpretationCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand GetDataCommand { get; }

        public RegularBudgetDetailViewModel(INsQtChungTuChiTietService chungTuChiTietService,
            INsQtChungTuService chungTuService,
            IMapper mapper,
            ISessionService sessionService,
            ITlQtChungTuService iTlQtChungTuService,
            INsQtChungTuChiTietGiaiThichService chungTuChiTietGiaiThichService,
            INsMucLucNganSachService iNsMucLucNganSachService,
            IDanhMucService danhMucService,
            INsDonViService donViService,
            INsNguoiDungDonViService nguoiDungDonViService,
            VerbalExplanationViewModel verbalExplanationViewModel,
            DataInterpretationViewModel dataInterpretationiewModel,
            PrintSettlementVoucherViewModel printSettlementVoucherViewModel,
            PrintCommunicateSettlementLNSViewModel printCommunicateSettlementLNSViewModel,
            GetDataQtThuongXuyenLuongViewModel getDataQtThuongXuyenLuongViewModel,
            PrintSummaryLNSViewModel printSummaryLNSViewModel) : base(danhMucService, sessionService)
        {
            _chungTuChiTietService = chungTuChiTietService;
            _chungTuService = chungTuService;
            _mapper = mapper;
            _sessionService = sessionService;
            _iTlQtChungTuService = iTlQtChungTuService;
            _chungTuChiTietGiaiThichService = chungTuChiTietGiaiThichService;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;
            _donViService = donViService;
            _nguoiDungDonViService = nguoiDungDonViService;
            VerbalExplanationViewModel = verbalExplanationViewModel;
            DataInterpretationViewModel = dataInterpretationiewModel;
            PrintSettlementVoucherViewModel = printSettlementVoucherViewModel;
            PrintCommunicateSettlementLNSViewModel = printCommunicateSettlementLNSViewModel;
            PrintSummaryLNSViewModel = printSummaryLNSViewModel;
            GetDataQtThuongXuyenLuongViewModel = getDataQtThuongXuyenLuongViewModel;

            SaveDataCommand = new RelayCommand(obj =>
            {
                if (!Model.BKhoa)
                    OnSaveData();
            });
            SearchCommand = new RelayCommand(obj =>
            {
                BeForeRefresh();
                _settlementVoucherDetailsView.Refresh();
                if (!IsCalculating) CalculateData();
            });
            ResetSearchCommand = new RelayCommand(obj => OnResetData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            ExplainCommand = new RelayCommand(obj => IsOpenExplainPopup = true);
            DataInterpretationCommand = new RelayCommand(obj => OnOpenDataInterpretationDialog());
            VerbalExplanationCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());
            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            GetDataCommand = new RelayCommand(obj => OnGetDataLuong());
        }

        public override void Init()
        {
            base.Init();
            IsCalculating = true;
            NamLamViec = _sessionService.Current.YearOfWork;
            DetailFilter = new NsMuclucNgansachModel();
            _sessionInfo = _sessionService.Current;
            _selectedTypeShowAgency = null;
            _isShowColumnDonVi = false;
            _selectedDisplayCondition = null;
            if (!string.IsNullOrEmpty(Model.STongHop))
            {
                LoadComboboxTypeShow();
            }
            //LoadBudgetIndexCondition();
            LoadDisplayCondition();
            LoadCondition();
            LoadData();
            LoadBudgetIndexCondition();
            IsCalculating = false;
        }

        private void LoadComboboxTypeShow()
        {
            TypeShowAgency = new ObservableCollection<ComboboxItem>();
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgency = TypeShowAgency.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedTypeShowAgency));
        }

        private void LoadCondition()
        {
            _searchCondition = new SettlementVoucherDetailSearch
            {
                VoucherId = Model.Id,
                LNS = Model.SDslns,
                YearOfWork = _sessionInfo.YearOfWork,
                YearOfBudget = _sessionInfo.YearOfBudget,
                Type = Model.SLoai,
                BudgetSource = _sessionInfo.Budget,
                AgencyId = Model.IIdMaDonVi,
                VoucherDate = Model.DNgayChungTu,
                UserName = _sessionInfo.Principal,
                QuarterMonth = Model.IThangQuy.ToString(),
            };
        }

        /// <summary>
        /// Load dữ liệu lên grid
        /// </summary>
        public override void LoadData(params object[] args)
        {
            List<QtChungTuChiTietQuery> listChungTuChiTietQuery = new List<QtChungTuChiTietQuery>();
            if (IsTongHop && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
            {
                List<string> voucherNos = Model.STongHop.Split(",").ToList();
                List<NsQtChungTu> listChungTu = _chungTuService.FindByAggregateVoucher(voucherNos, _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, Model.SLoai).ToList();
                List<QtChungTuChiTietQuery> listChungTuChiTietParent = new List<QtChungTuChiTietQuery>();
                List<QtChungTuChiTietQuery> listChungTuChiTietChildren = new List<QtChungTuChiTietQuery>();
                foreach (var chungTu in listChungTu)
                {
                    var searchCondition = new SettlementVoucherDetailSearch
                    {
                        VoucherId = chungTu.Id,
                        LNS = chungTu.SDslns,
                        YearOfWork = _sessionInfo.YearOfWork,
                        YearOfBudget = _sessionInfo.YearOfBudget,
                        Type = chungTu.SLoai,
                        BudgetSource = _sessionInfo.Budget,
                        AgencyId = chungTu.IIdMaDonVi,
                        VoucherDate = chungTu.DNgayChungTu.Value,
                        UserName = _sessionInfo.Principal,
                        QuarterMonth = chungTu.IThangQuy.ToString(),
                    };
                    List<QtChungTuChiTietQuery> listQuery = _chungTuChiTietService.FindByCondition(searchCondition);
                    listChungTuChiTietParent.AddRange(listQuery.Where(x => x.BHangChaQuyetToan));
                    listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.BHangChaQuyetToan && x.HasTenDonVi));
                }
                List<string> listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                listChungTuChiTietParent = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x => x.First()).Distinct().ToList();
                listChungTuChiTietQuery.AddRange(listChungTuChiTietParent);
                listChungTuChiTietQuery.AddRange(listChungTuChiTietChildren);
                listChungTuChiTietQuery = listChungTuChiTietQuery.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IIdMaDonVi).ToList();
                string agencyIds = string.Join(",", listChungTu.Select(x => x.IIdMaDonVi));
                LoadAgencies(agencyIds);
            }
            else
            {
                listChungTuChiTietQuery = _chungTuChiTietService.FindByCondition(_searchCondition);
            }
            _settlementVoucherDetails = _mapper.Map<ObservableCollection<SettlementVoucherDetailModel>>(listChungTuChiTietQuery);
            _settlementVoucherDetailsView = CollectionViewSource.GetDefaultView(_settlementVoucherDetails);
            _settlementVoucherDetailsView.Filter = ItemsViewFilter;
            foreach (var model in _settlementVoucherDetails)
            {
                if (!model.IsHangCha)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(SettlementVoucherDetailModel.FTuChiDeNghi) || args.PropertyName == nameof(SettlementVoucherDetailModel.FTuChiPheDuyet)
                            || args.PropertyName == nameof(SettlementVoucherDetailModel.FSoNgay) || args.PropertyName == nameof(SettlementVoucherDetailModel.FSoNguoi)
                            || args.PropertyName == nameof(SettlementVoucherDetailModel.FSoLuot) || args.PropertyName == nameof(SettlementVoucherDetailModel.SGhiChu) 
                            || args.PropertyName == nameof(SettlementVoucherDetailModel.FDeNghiChuyenNamSau) || args.PropertyName == nameof(SettlementVoucherDetailModel.FChuyenNamSauDaCap))
                        {
                            SettlementVoucherDetailModel item = (SettlementVoucherDetailModel)sender;
                            item.IsModified = true;
                            if (args.PropertyName == nameof(SettlementVoucherDetailModel.FTuChiDeNghi) || args.PropertyName == nameof(SettlementVoucherDetailModel.FTuChiPheDuyet)
                                || args.PropertyName == nameof(SettlementVoucherDetailModel.FSoNgay) || args.PropertyName == nameof(SettlementVoucherDetailModel.FSoNguoi)
                                || args.PropertyName == nameof(SettlementVoucherDetailModel.FSoLuot) || args.PropertyName == nameof(SettlementVoucherDetailModel.FDeNghiChuyenNamSau)
                                || args.PropertyName == nameof(SettlementVoucherDetailModel.FChuyenNamSauDaCap))
                            {
                                if (!IsCalculating)
                                {
                                    /*
                                    var parent = SettlementVoucherDetails.Where(x => x.IIdMlns == model.IIdMlnsCha).FirstOrDefault();
                                    if (parent != null && parent.FDuToan != 0 && model.FDuToan == 0)
                                        model.IsCalculateConLai = false;
                                    */
                                    CalculateData();
                                }
                            }
                            OnPropertyChanged(nameof(IsSaveData));
                            OnPropertyChanged(nameof(IsDeleteAll));
                        }
                    };
                }
            }
            if (_settlementVoucherDetails != null)
                SelectedItem = _settlementVoucherDetails.Where(x => !x.IsHangCha).FirstOrDefault();
            else SelectedItem = new SettlementVoucherDetailModel();
            CalculateData();
            OnPropertyChanged(nameof(SettlementVoucherDetails));
            if (FirstInit)
            {
                FirstInit = false;
                DataInterpretationViewModel.SettlementVoucher = Model;
                DataInterpretationViewModel.SettlementVoucherDetails = _settlementVoucherDetails.ToList();
                DataInterpretationViewModel.Init();
                DataInterpretationViewModel.OnSaveAndUpdateWithoutMessage();
            }
        }

        private void LoadAgencies(string agencyIds)
        {
            var listDonVi = _donViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            OnPropertyChanged(nameof(Agencies));
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (SettlementVoucherDetailModel)obj;
            result = VoucherDetailFilter(item);
            if (!result && item.IsHangCha)
            {
                if (string.IsNullOrEmpty(item.SL))
                    result = xnmConcatenation.StartsWith(item.SLns);
                else result = xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (result)
                item.IsFilter = result;
            return result;
        }

        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            var item = (SettlementVoucherDetailModel)obj;
            if (!string.IsNullOrEmpty(DetailFilter.Lns))
                result = result && item.SLns.ToLower().StartsWith(DetailFilter.Lns.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.L))
                result = result && item.SL.ToLower().StartsWith(DetailFilter.L.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.K))
                result = result && item.SK.ToLower().StartsWith(DetailFilter.K.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.SM.ToLower().StartsWith(DetailFilter.M.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.STm.ToLower().StartsWith(DetailFilter.TM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.STtm.ToLower().StartsWith(DetailFilter.TTM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.SNg.ToLower().StartsWith(DetailFilter.NG.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG))
                result = result && item.STng.ToLower().StartsWith(DetailFilter.TNG.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG1))
                result = result && item.STng1.ToLower().StartsWith(DetailFilter.TNG1.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG2))
                result = result && item.STng2.ToLower().StartsWith(DetailFilter.TNG2.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG3))
                result = result && item.STng3.ToLower().StartsWith(DetailFilter.TNG3.ToLower());
            if (SelectedDisplayCondition != null)
            {
                if (SelectedDisplayCondition.ValueItem == SettlementVoucherDetailDisplay.HAVE_ESTIMATE.ToString())
                    result = result && item.FDuToan != 0;
                else if (SelectedDisplayCondition.ValueItem == SettlementVoucherDetailDisplay.HAVE_ESTIMATE_EXISTENCE.ToString())
                    result = result && item.FConLai != 0;
                else if (SelectedDisplayCondition.ValueItem == SettlementVoucherDetailDisplay.HAVE_ESTIMATE_SETTLEMENT_EXISTENCE.ToString())
                    result = result && (item.FDuToan != 0 || item.FTuChiDeNghi != 0 || item.FTuChiPheDuyet != 0);
                else if (SelectedDisplayCondition.ValueItem == SettlementVoucherDetailDisplay.ENTERED_THE_SETTLEMENT.ToString())
                    result = result && (item.FTuChiDeNghi != 0 || item.FTuChiPheDuyet != 0 || item.FSoNgay != 0 || item.FSoNguoi != 0 || item.FSoLuot != 0);
            }
            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIdMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;
        }

        private void BeForeRefresh()
        {
            _filterResult = _settlementVoucherDetails.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        /// <summary>
        /// Load dữ liệu cho phần tìm kiếm bằng LNS
        /// </summary>
        private void LoadBudgetIndexCondition()
        {
            List<NsMuclucNgansachModel> listBudgetIndex = new List<NsMuclucNgansachModel>();
            var listLNS = _settlementVoucherDetails.Where(m => string.IsNullOrEmpty(m.SL)).Select(n => new NsMuclucNgansachModel()
            {
                Lns = n.SLns,
                MoTa = n.SMoTa
            }).ToList();
            listBudgetIndex.Add(new NsMuclucNgansachModel(string.Empty, "-- TẤT CẢ --"));
            listBudgetIndex.AddRange(listLNS);
            //listBudgetIndex.Add(new NsMuclucNgansachModel("1010000", "KP lương, phụ cấp, tiền ăn"));
            BudgetCatalogItems = new ObservableCollection<NsMuclucNgansachModel>(listBudgetIndex);
            _budgetCatalogItemsView = CollectionViewSource.GetDefaultView(BudgetCatalogItems);
            _budgetCatalogItemsView.Filter = ListBudgetIndexFilter;
        }

        private bool ListBudgetIndexFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(DetailFilter.Lns))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.Lns.ToLower().StartsWith(DetailFilter.Lns!.ToLower());
        }

        /// <summary>
        /// Load dữ liệu cho phần điều kiện hiển thị
        /// </summary>
        private void LoadDisplayCondition()
        {
            DisplayConditions = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem("Hiển thị tất cả", SettlementVoucherDetailDisplay.ALL.ToString()),
                new ComboboxItem("Có số liệu dự toán", SettlementVoucherDetailDisplay.HAVE_ESTIMATE.ToString()),
                new ComboboxItem("Có số liệu dự toán còn lại", SettlementVoucherDetailDisplay.HAVE_ESTIMATE_EXISTENCE.ToString()),
                new ComboboxItem("Có số liệu dự toán và quyết toán", SettlementVoucherDetailDisplay.HAVE_ESTIMATE_SETTLEMENT_EXISTENCE.ToString()),
                new ComboboxItem("Đã nhập quyết toán đợt này", SettlementVoucherDetailDisplay.ENTERED_THE_SETTLEMENT.ToString())
            };
        }

        /// <summary>
        /// Reset các điều kiện tìm kiếm
        /// </summary>
        private void OnResetData()
        {
            DetailFilter = new NsMuclucNgansachModel();
            BeForeRefresh();
            _settlementVoucherDetailsView.Refresh();
            if (!IsCalculating) CalculateData();
        }

        /// <summary>
        /// mở dialog confirm khóa chứng từ
        /// </summary>
        protected override void OnLockUnLock()
        {
            if (!IsEditByRole)
            {
                MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.SNguoiTao));
                return;
            }
            string message = Model.BKhoa ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = Model.BKhoa ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult result = MessageBoxHelper.Confirm(message);
            if (result == MessageBoxResult.Yes)
            {
                _chungTuService.LockOrUnlock(Model.Id, !Model.BKhoa);
                Model.BKhoa = !Model.BKhoa;
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
                MessageBoxHelper.Info(msgDone);
            }
        }

        /// <summary>
        /// Tính toán lại dữ liệu trên lưới
        /// </summary>
        /// <param name="models"></param>
        private void CalculateData()
        {
            IsCalculating = true;
            // Reset value parrent
            _settlementVoucherDetails.Where(x => x.IsHangCha)
                .Select(x => { x.FDuToan = x.FDuToanOrigin != 0 ? x.FDuToanOrigin : 0; x.FDaQuyetToan = 0; x.FTuChiDeNghi = 0; x.FTuChiPheDuyet = 0; x.FSoNgay = 0; x.FSoNguoi = 0; x.FSoLuot = 0; x.FDeNghiChuyenNamSau = 0; x.FChuyenNamSauDaCap = 0; return x; }).ToList();
            // Caculate value child
            var listChild = _settlementVoucherDetails.Where(x => x.IsFilter && (x.FDuToanOrigin != 0 || (x.IsEditable && (x.FDaQuyetToan != 0 || x.FTuChiPheDuyet != 0 || x.FTuChiDeNghi != 0 || x.FSoLuot != 0 || x.FSoNgay != 0 || x.FSoNguoi != 0 || x.FDeNghiChuyenNamSau != 0 || x.FChuyenNamSauDaCap.GetValueOrDefault() != 0 || x.FChuyenNamSauChuaCap.GetValueOrDefault() != 0))));
            foreach (var item in listChild)
            {
                CalculateParent(item, item);
            }
            // Caculate total summary
            CalculateSummary();
            IsCalculating = false;
        }

        /// <summary>
        /// Tính toán item cha theo item con
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tuChi"></param>
        /// <param name="soNgay"></param>
        /// <param name="soNguoi"></param>
        /// <param name="soLuot"></param>
        /// <param name="models"></param>
        private void CalculateParent(SettlementVoucherDetailModel currentItem, SettlementVoucherDetailModel selfItem)
        {
            var parentItem = _settlementVoucherDetails.FirstOrDefault(x => currentItem.IIdMlnsCha != null && x.IIdMlns == currentItem.IIdMlnsCha);
            if (parentItem == null) return;
            if (selfItem.FDuToanOrigin != 0)
                parentItem.FDuToan += selfItem.FDuToan;
            /*
            if (parentItem.FDuToan != 0 && currentItem.FDuToan == 0)
            {
                currentItem.IsCalculateConLai = false;
                OnPropertyChanged(nameof(currentItem.FConLai));
            }
            */

            parentItem.FDaQuyetToan += selfItem.FDaQuyetToan;
            parentItem.FTuChiDeNghi += selfItem.FTuChiDeNghi;
            parentItem.FTuChiPheDuyet += selfItem.FTuChiPheDuyet;
            parentItem.FSoLuot += selfItem.FSoLuot;
            parentItem.FDeNghiChuyenNamSau += selfItem.FDeNghiChuyenNamSau;
            parentItem.FChuyenNamSauDaCap += selfItem.FChuyenNamSauDaCap.GetValueOrDefault();
            CalculateParent(parentItem, selfItem);
        }

        /// <summary>
        /// update dữ liệu tính tổng
        /// </summary>
        private void CalculateSummary()
        {
            SettlementVoucherTotal = new SettlementVoucherTotalModel();
            foreach (var item in _settlementVoucherDetails.Where(x => x.IsFilter && string.IsNullOrEmpty(x.IIdMlnsCha)))
            {
                SettlementVoucherTotal.FTongDuToan += item.FDuToan;
                SettlementVoucherTotal.FTongDaQuyetToan += item.FDaQuyetToan;
                SettlementVoucherTotal.FTongConLai += item.FConLai;
                SettlementVoucherTotal.FTongTuChiDeNghi += item.FTuChiDeNghi;
                SettlementVoucherTotal.FTongTuChiPheDuyet += item.FTuChiPheDuyet;
                SettlementVoucherTotal.FTongSoLuot += item.FSoLuot;
                SettlementVoucherTotal.FTongSoNgay += item.FSoNgay;
                SettlementVoucherTotal.FTongSoNguoi += 0;
                //SettlementVoucherTotal.FTongSoNguoi += item.FSoNguoi;
                SettlementVoucherTotal.FTongDeNghiChuyenNamSau += item.FDeNghiChuyenNamSau;
                SettlementVoucherTotal.FTongChuyenNamSauDaCap += item.FChuyenNamSauDaCap.GetValueOrDefault();
                SettlementVoucherTotal.FTongChuyenNamSauChuaCap += item.FChuyenNamSauChuaCap.GetValueOrDefault();
                SettlementVoucherTotal.FTongTienAn += item.FTienAn.GetValueOrDefault();
            }
        }

        protected override void OnDelete()
        {
            if (!IsEditByRole)
            {
                MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.SNguoiTao));
                return;
            }
            if (!Model.BKhoa)
            {
                if (SelectedItem != null && !SelectedItem.IsHangCha)
                {
                    SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                    if (!IsCalculating) CalculateData();
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IsDeleteAll));
                }
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            if (IsSaveData)
            {
                var result = MessageBoxHelper.ConfirmCancel(Resources.ConfirmReloadData);
                if (result == MessageBoxResult.Cancel)
                    return;
                else if (result == MessageBoxResult.Yes)
                    OnSaveData();
            }
            LoadData();
        }

        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        private void OnSaveData()
        {
            List<SettlementVoucherDetailModel> voucherDetails = new List<SettlementVoucherDetailModel>();
            List<SettlementVoucherDetailModel> voucherDetailsAdd = _settlementVoucherDetails.Where(x => x.IsModified && !x.IsDeleted && x.IIdQtchungTu == Guid.Empty).ToList();
            voucherDetailsAdd = voucherDetailsAdd.Where(n => n.FTuChiDeNghi != 0 || n.FTuChiPheDuyet != 0 || n.FDuToan != 0 || n.FDaQuyetToan != 0 || n.FDeNghiChuyenNamSau != 0 || n.FChuyenNamSauDaCap != 0).ToList();
            List<SettlementVoucherDetailModel> voucherDetailsUpdate = _settlementVoucherDetails.Where(x => x.IsModified && !x.IsDeleted && x.IIdQtchungTu != Guid.Empty).ToList();
            List<SettlementVoucherDetailModel> voucherDetailsDelete = _settlementVoucherDetails.Where(x => x.IsDeleted).ToList();
            List<SettlementVoucherModel> vouchers = new List<SettlementVoucherModel>();

            //thêm mới chứng từ chi tiết
            if (voucherDetailsAdd.Count > 0)
            {
                voucherDetailsAdd = voucherDetailsAdd.Select(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.IIdQtchungTu = Model.Id;
                    x.IThangQuyLoai = Model.IThangQuyLoai;
                    x.IThangQuy = Convert.ToInt32(Model.IThangQuy);
                    x.IIdMaDonVi = Model.IIdMaDonVi;
                    x.STenDonVi = Model.STenDonVi;
                    x.INamNganSach = Model.INamNganSach;
                    x.INamLamViec = Model.INamLamViec;
                    x.IIdMaNguonNganSach = Model.IIdMaNguonNganSach;
                    return x;
                }).ToList();
                List<NsQtChungTuChiTiet> listChungTuChiTiets = new List<NsQtChungTuChiTiet>();
                listChungTuChiTiets = _mapper.Map<List<NsQtChungTuChiTiet>>(voucherDetailsAdd);
                _chungTuChiTietService.AddRange(listChungTuChiTiets);
                _settlementVoucherDetails.Where(x => x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            // cập nhật chứng từ chi tiết
            if (voucherDetailsUpdate.Count > 0)
            {
                // xóa các chứng từ cập nhật mà các cột đều bằng 0
                foreach (var item in voucherDetailsUpdate.Where(n => n.FTuChiDeNghi == 0 && n.FTuChiPheDuyet == 0 && n.FDuToan == 0 && n.FDaQuyetToan == 0 && n.FDeNghiChuyenNamSau == 0 && n.FChuyenNamSauDaCap == 0))
                {
                    _chungTuChiTietService.Delete(item.Id);

                    ResetVoucherDetailData(item);

                    // reset flag
                    item.IsModified = false;
                    item.IsDeleted = false;
                    item.IIdQtchungTu = Guid.Empty;
                }
                voucherDetailsUpdate = voucherDetailsUpdate.Where(n => n.FTuChiDeNghi != 0 || n.FTuChiPheDuyet != 0 || n.FDuToan != 0 || n.FDaQuyetToan != 0).ToList();
                foreach (var item in voucherDetailsUpdate)
                {
                    NsQtChungTuChiTiet chungTuChiTiet = _chungTuChiTietService.FindById(item.Id);
                    _mapper.Map(item, chungTuChiTiet);
                    _chungTuChiTietService.Update(chungTuChiTiet);

                    // Reset flag
                    item.IsModified = false;
                }

                if (IsTongHop && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    List<string> mlnsIds = voucherDetailsUpdate.Select(x => x.IIdMlns).Distinct().ToList();
                    //cập nhật chứng từ chi tiết của chứng từ tổng hợp
                    foreach (var mlnsId in mlnsIds)
                    {
                        NsQtChungTuChiTiet tongHopChiTiet = _chungTuChiTietService.FindByChungTuIdAndMlnsId(Model.Id, mlnsId);
                        if (tongHopChiTiet != null)
                        {
                            var details = _settlementVoucherDetails.Where(x => x.IIdMlns == mlnsId).ToList();
                            tongHopChiTiet.FTuChiDeNghi = details.Sum(x => x.FTuChiDeNghi);
                            tongHopChiTiet.FTuChiPheDuyet = details.Sum(x => x.FTuChiPheDuyet);
                            tongHopChiTiet.FSoNgay = details.Sum(x => x.FSoNgay);
                            tongHopChiTiet.FSoNguoi = details.Sum(x => x.FSoNguoi);
                            tongHopChiTiet.FSoLuot = details.Sum(x => x.FSoLuot);
                            _chungTuChiTietService.Update(tongHopChiTiet);
                        }
                    }

                    //cập nhật thông tin chứng từ con
                    List<Guid> chungTuIds = voucherDetailsUpdate.Select(x => x.IIdQtchungTu).Distinct().ToList();
                    foreach (var chungTuId in chungTuIds)
                    {
                        NsQtChungTu chungTu = _chungTuService.FindById(chungTuId);
                        var details = _settlementVoucherDetails.Where(x => x.IIdQtchungTu == chungTuId).ToList();
                        chungTu.FTongTuChiDeNghi = details.Sum(x => x.FTuChiDeNghi);
                        chungTu.FTongTuChiPheDuyet = details.Sum(x => x.FTuChiPheDuyet);
                        _chungTuService.Update(chungTu);
                        vouchers.Add(_mapper.Map<SettlementVoucherModel>(chungTu));
                    }
                }
                if (!IsCalculating) CalculateData();
            }

            //xóa chứng từ chi tiết
            if (voucherDetailsDelete.Count > 0)
            {
                foreach (var item in voucherDetailsDelete)
                {
                    _chungTuChiTietService.Delete(item.Id);

                    ResetVoucherDetailData(item);

                    // Reset flag
                    item.IsModified = false;
                    item.IsDeleted = false;
                    item.IIdQtchungTu = Guid.Empty;
                }
                if (!IsCalculating) CalculateData();
            }

            //cập nhật thông tin chứng từ
            UpdateSettlementVoucher();

            //tạo hoặc cập nhật thông tin chứng từ giải thích số
            var condition = new SettlementVoucherDetailExplainCriteria
            {
                VoucherId = Model.Id,
                ExplainId = Model.Id.ToString(),
                AgencyId = Model.IIdMaDonVi,
                YearOfWork = _sessionInfo.YearOfWork
            };
            var chungTuChiTietGiaiThichExist = _chungTuChiTietGiaiThichService.FindByCondition(condition);
            if (chungTuChiTietGiaiThichExist != null)
            {
                var chungTuNew = _mapper.Map<SettlementVoucherDetailExplainModel>(chungTuChiTietGiaiThichExist);
                chungTuNew.SNguoiSua = _sessionInfo.Principal;
                chungTuNew.DNgaySua = DateTime.Now;

                chungTuNew.FLuongSiQuanQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000", "1");
                chungTuNew.FPhuCapSiQuanQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6100", "1");

                chungTuNew.FLuongQncnQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000", "2");
                chungTuNew.FPhuCapQncnQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6100", "2");

                chungTuNew.FLuongCnvqpQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000", "3.1,3.2,3.3");
                chungTuNew.FPhuCapCnvqpQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6100", "3.1,3.2,3.3");

                chungTuNew.FLuongHdQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000", "4");
                chungTuNew.FPhuCapHdQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6100", "4");
                _chungTuChiTietGiaiThichService.Update(_mapper.Map<NsQtChungTuChiTietGiaiThich>(chungTuNew));
            }
            else
            {
                var chungTuNew = new SettlementVoucherDetailExplainModel();
                chungTuNew.Id = Guid.NewGuid();
                chungTuNew.IIdQtchungTu = Model.Id;
                chungTuNew.IIdMaDonVi = Model.IIdMaDonVi;
                chungTuNew.IIdGiaiThich = Model.Id.ToString();
                chungTuNew.INamLamViec = _sessionInfo.YearOfWork;
                chungTuNew.IThangQuy = Convert.ToInt32(Model.IThangQuy);
                chungTuNew.IThangQuyLoai = Model.IThangQuyLoai;
                chungTuNew.SNguoiTao = _sessionInfo.Principal;
                chungTuNew.DNgayTao = DateTime.Now;

                chungTuNew.FLuongSiQuanQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000", "1");
                chungTuNew.FPhuCapSiQuanQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6100", "1");

                chungTuNew.FLuongQncnQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000", "2");
                chungTuNew.FPhuCapQncnQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6100", "2");

                chungTuNew.FLuongCnvqpQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000", "3.1,3.2,3.3");
                chungTuNew.FPhuCapCnvqpQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6100", "3.1,3.2,3.3");

                chungTuNew.FLuongHdQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000", "4");
                chungTuNew.FPhuCapHdQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6100", "4");

                _chungTuChiTietGiaiThichService.Add(_mapper.Map<NsQtChungTuChiTietGiaiThich>(chungTuNew));
            }

            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));

            MessageBoxHelper.Info(Resources.MsgSaveDone);

            //refresh dữ liệu ở màn index
            DataChangedEventHandler handler = UpdateSettlementVoucherEvent;
            if (handler != null)
            {
                if (!string.IsNullOrEmpty(Model.STongHop))
                {
                    vouchers.Add(Model);
                    handler(vouchers, new EventArgs());
                }
                else handler(Model, new EventArgs());
            }
        }

        private double GetDataFromSettlementVoucherDetails(string concatenateCode, string code)
        {
            var listCode = code.Split(StringUtils.COMMA).ToList();
            var mlns = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork)
                                                .Where(n => listCode.Contains(n.SMaCB) && n.XauNoiMa.StartsWith(concatenateCode));

            var detail = from ss in SettlementVoucherDetails
                         join ml in mlns on ss.IIdMlns equals ml.MlnsId.ToString()
                         select ss;

            var detailVoucher = _mapper.Map<List<SettlementVoucherDetailModel>>(detail);

            return detailVoucher.Where(x => x.BHangCha == false).Sum(x => x.FTuChiPheDuyet);
        }

        private SettlementVoucherDetailModel ResetVoucherDetailData(SettlementVoucherDetailModel voucherDetail)
        {
            voucherDetail.FTuChiDeNghi = 0;
            voucherDetail.FTuChiPheDuyet = 0;
            voucherDetail.FSoNgay = 0;
            voucherDetail.FSoNguoi = 0;
            voucherDetail.FSoLuot = 0;
            voucherDetail.SGhiChu = string.Empty;
            return voucherDetail;
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        private void OnOpenDataInterpretationDialog()
        {
            DataInterpretationViewModel.SettlementVoucher = Model;
            DataInterpretationViewModel.SettlementVoucherDetails = _settlementVoucherDetails.ToList();
            DataInterpretationViewModel.Init();
            var view = new DataInterpretation { DataContext = DataInterpretationViewModel };
            view.ShowDialog();
        }

        private void OnOpenVerbalExplanationDialog()
        {
            VerbalExplanationViewModel.SettlementVoucher = Model;
            VerbalExplanationViewModel.Init();
            var view = new VerbalExplanation { DataContext = VerbalExplanationViewModel };
            view.ShowDialog();
        }

        /// <summary>
        /// Mở màn hình in
        /// </summary>
        /// <param name="param"></param>
        private void OpenPrintDialog(object param)
        {
            int dialogType = (int)param;
            switch (dialogType)
            {
                case (int)SettlementPrintType.PRINT_COMMUNICATE_SETTLEMENT_LNS:
                    PrintCommunicateSettlementLNSViewModel.SettlementVoucher = Model;
                    PrintCommunicateSettlementLNSViewModel.Init();
                    var view1 = new PrintCommunicateSettlementLNS { DataContext = PrintCommunicateSettlementLNSViewModel };
                    //show the dialog
                    DialogHost.Show(view1, SettlementScreen.REGULAR_BUDGET_DETAIL_DIALOG, null, null);
                    break;
                case (int)SettlementPrintType.PRINT_REGULARLY_SETTLEMENT:
                    //PrintSummaryRegularSettlementViewModel.Init();
                    //var view2 = new PrintSummaryRegularSettlement { DataContext = PrintSummaryRegularSettlementViewModel };
                    ////show the dialog
                    //DialogHost.Show(view2, "RegularBudgetDetailDialog", null, null);
                    //break;
                    PrintSummaryLNSViewModel.SettlementTypeValue = SettlementType.REGULAR_BUDGET;
                    PrintSummaryLNSViewModel.TieuDeBaoCao = Name;
                    PrintSummaryLNSViewModel.IsShowDatePeople = false;
                    PrintSummaryLNSViewModel.Init();
                    var view2 = new PrintSummaryLNS { DataContext = PrintSummaryLNSViewModel };
                    //show the dialog
                    DialogHost.Show(view2, SettlementScreen.REGULAR_BUDGET_DETAIL_DIALOG, null, null);
                    break;
            }
        }

        private void OnGetDataLuong()
        {
            if (!IsEditByRole)
            {
                MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.SNguoiTao));
                return;
            }

            GetDataQtThuongXuyenLuongViewModel.Name = "Lấy dữ liệu quyết toán thường xuyên lương";
            GetDataQtThuongXuyenLuongViewModel.Description = "Lấy dữ liệu quyết toán thường xuyên lương";
            GetDataQtThuongXuyenLuongViewModel.NsQtChungTuModel = Model;
            GetDataQtThuongXuyenLuongViewModel.Init();
            GetDataQtThuongXuyenLuongViewModel.SavedAction = obj =>
            {
                //this.LoadData();
                var lstCtLuong = (List<SettlementVoucherDetailModel>)obj;
                if (lstCtLuong != null && lstCtLuong.Count > 0)
                {
                    AddDataFromLuong(lstCtLuong);
                    IdChungTuLuong = lstCtLuong.FirstOrDefault().IIdQtchungTu;
                    var objQtLuong = _iTlQtChungTuService.FindById(IdChungTuLuong);
                    if (objQtLuong != null)
                    {
                        objQtLuong.BNganSachNhanDuLieu = true;
                        _iTlQtChungTuService.Update(objQtLuong);
                    }
                }

            };
            var addView = new GetDataQtThuongXuyenLuong() { DataContext = GetDataQtThuongXuyenLuongViewModel };
            DialogHost.Show(addView, "RegularBudgetDetailDialog");
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsDelete));
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            if (!IsEditByRole)
            {
                MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.SNguoiTao));
                return;
            }
            var result = MessageBoxHelper.Confirm(Resources.DeleteAllChungTuChiTiet);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                if (SettlementVoucherDetails != null)
                {
                    SettlementVoucherDetails.Where(x => x.IsFilter && !x.IsHangCha).ForAll(x => x.IsDeleted = true);
                    OnSaveData();
                }
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void UpdateSettlementVoucher()
        {
            NsQtChungTu chungTu = _chungTuService.FindById(Model.Id);
            var childs = SettlementVoucherDetails.Where(x => x.HasData);
            chungTu.FTongTuChiDeNghi = childs.Sum(x => x.FTuChiDeNghi);
            chungTu.FTongTuChiPheDuyet = childs.Sum(x => x.FTuChiPheDuyet);
            Model.FTongTuChiDeNghi = chungTu.FTongTuChiDeNghi;
            Model.FTongTuChiPheDuyet = chungTu.FTongTuChiPheDuyet;
            _chungTuService.Update(chungTu);
        }

        public void AddDataFromLuong(List<SettlementVoucherDetailModel> lstDataLuong)
        {
            IsCalculating = true;
            foreach (var it in _settlementVoucherDetails)
            {
                var itLuong = lstDataLuong.FirstOrDefault(x => x.IIdMlns.Equals(it.IIdMlns) && !x.IsHangCha
                    && (x.FSoNgay != 0 || x.FSoNguoi != 0 || x.FSoLuot != 0 || x.FTuChiDeNghi != 0 || x.FTuChiPheDuyet != 0 || it.FSoNgay != 0 || it.FSoNguoi != 0 || x.FSoLuot != 0 || it.FTuChiDeNghi != 0 || it.FTuChiPheDuyet != 0));
                if (itLuong != null)
                {
                    it.FSoNgay = itLuong.FSoNgay;
                    it.FSoNguoi = itLuong.FSoNguoi;
                    it.FSoLuot = itLuong.FSoLuot;
                    it.FTuChiDeNghi = itLuong.FTuChiDeNghi;
                    it.FTuChiPheDuyet = itLuong.FTuChiPheDuyet;
                }
            }
            CalculateData();
            IsCalculating = false;
            OnPropertyChanged(nameof(SettlementVoucherDetails));
        }
    }
}
