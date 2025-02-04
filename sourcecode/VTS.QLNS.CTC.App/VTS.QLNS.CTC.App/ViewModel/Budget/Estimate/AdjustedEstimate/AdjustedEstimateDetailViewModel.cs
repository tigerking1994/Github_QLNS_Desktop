using AutoMapper;
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
using VTS.QLNS.CTC.App.View.Budget.Estimate.AdjustedEstimate;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.AdjustedEstimate
{
    public class AdjustedEstimateDetailViewModel : DetailViewModelBase<DcChungTuModel, DcChungTuChiTietModel>
    {
        private readonly IMapper _mapper;
        private readonly INsDcChungTuChiTietService _chungTuChiTietService;
        private readonly INsDcChungTuService _chungTuService;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsMucLucNganSachService _mlnsService;
        private readonly INsDonViService _donViService;
        private DanhMucNganhService _danhMucNganhService;
        private ICollectionView _budgetCatalogItemsView;
        private ICollectionView _chungTuChiTietItemsView;
        private EstimationVoucherDetailCriteria _searchCondition;
        private SessionInfo _sessionInfo;
        private List<NsMucLucNganSach> _listMLNS;
        private ICollection<DcChungTuChiTietModel> _filterResult = new HashSet<DcChungTuChiTietModel>();
        private string xnmConcatenation = "";
        private List<DanhMuc> _listDanhMucNganh;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateVoucherEvent;
        public override Type ContentType => typeof(AdjustedEstimateDetail);
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        public bool IsDeleteAll => Items.Any(item => !item.IsModified && item.HasData);
        public bool IsTongHopDonVi => Model.IIdMaDonVi == _sessionInfo.IdDonVi;
        public bool IsTongHop => !string.IsNullOrEmpty(Model.STongHop);
        public bool IsShowAgencyFilter => IsTongHop && _selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI;
        public int NamLamViec { get; set; }

        public bool AfterImport { get; set; } = false;

        private bool _isShowColumnDonVi;
        public bool IsShowColumnDonVi
        {
            get => _isShowColumnDonVi;
            set => SetProperty(ref _isShowColumnDonVi, value);
        }

        private string _duKienQuyetToanDauNamTitle;
        public string DuKienQuyetToanDauNamTitle
        {
            get => _duKienQuyetToanDauNamTitle;
            set => SetProperty(ref _duKienQuyetToanDauNamTitle, value);
        }

        private string _duKienQuyetToanCuoiNamTitle;
        public string DuKienQuyetToanCuoiNamTitle
        {
            get => _duKienQuyetToanCuoiNamTitle;
            set => SetProperty(ref _duKienQuyetToanCuoiNamTitle, value);
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _budgetCatalogItemsView.Refresh();
                    if (!IsChecked) CalculateData();
                }
            }
        }

        private ObservableCollection<DcChungTuChiTietModel> _budgetCatalogItems;
        public ObservableCollection<DcChungTuChiTietModel> BudgetCatalogItems
        {
            get => _budgetCatalogItems;
            set => SetProperty(ref _budgetCatalogItems, value);
        }

        private DcChungTuChiTietModel _selectedBudgetCatalog;
        public DcChungTuChiTietModel SelectedBudgetCatalog
        {
            get => _selectedBudgetCatalog;
            set
            {
                if (SetProperty(ref _selectedBudgetCatalog, value))
                {
                    if (_selectedBudgetCatalog != null)
                        SelectedLNS = _selectedBudgetCatalog.SLns;
                    BeForeRefresh();
                    _chungTuChiTietItemsView.Refresh();
                    if (!IsChecked) CalculateData();
                }
            }
        }

        public bool IsChecked { get; set; } = false;

        private string _selectedLNS;
        public string SelectedLNS
        {
            get => _selectedLNS;
            set => SetProperty(ref _selectedLNS, value);
        }

        private EstimationDetailCriteria _detailFilter;
        public EstimationDetailCriteria DetailFilter
        {
            get => _detailFilter;
            set => SetProperty(ref _detailFilter, value);
        }

        private ObservableCollection<ComboboxItem> _typeDisplays;
        public ObservableCollection<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private string _typeDisplaysSelected;
        public string TypeDisplaysSelected
        {
            get => _typeDisplaysSelected;
            set
            {
                if (SetProperty(ref _typeDisplaysSelected, value) && _chungTuChiTietItemsView != null)
                {
                    BeForeRefresh();
                    _chungTuChiTietItemsView?.Refresh();
                    if (!IsChecked) CalculateData();
                }
            }
        }

        private int _loaiChungTu;
        public int LoaiChungTu
        {
            get => _loaiChungTu;
            set => SetProperty(ref _loaiChungTu, value);
        }

        private DivisionColumnVisibility _columnVisibility;
        public DivisionColumnVisibility ColumnVisibility
        {
            get => _columnVisibility;
            set => SetProperty(ref _columnVisibility, value);
        }

        private AdjustedEstimateDetailTotalModel _detailTotal;

        public AdjustedEstimateDetailTotalModel DetailTotal
        {
            get => _detailTotal;
            set => SetProperty(ref _detailTotal, value);
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

        private ComboboxItem _selectedAgency;
        public ComboboxItem SelectedAgency
        {
            get => _selectedAgency;
            set
            {
                SetProperty(ref _selectedAgency, value);
                BeForeRefresh();
                _chungTuChiTietItemsView?.Refresh();
                if (!IsChecked) CalculateData();
            }
        }

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand SaveDataCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand CopyCommand { get; }
        public RelayCommand TransferCommand { get; }

        public AdjustedEstimateDetailViewModel(
            IMapper mapper,
            INsDcChungTuService chungTuService,
            INsDcChungTuChiTietService chungTuChiTietService,
            ISessionService sessionService,
            IDanhMucService danhMucService,
            INsMucLucNganSachService mlnsService,
            DanhMucNganhService danhMucNganhService,
            INsDonViService donViService) : base(danhMucService, sessionService)
        {
            _mapper = mapper;
            _chungTuService = chungTuService;
            _danhMucService = danhMucService;
            _chungTuChiTietService = chungTuChiTietService;
            _sessionService = sessionService;
            _mlnsService = mlnsService;
            _danhMucNganhService = danhMucNganhService;
            _donViService = donViService;

            SearchCommand = new RelayCommand(obj => { 
                BeForeRefresh();
                _chungTuChiTietItemsView.Refresh();
                if (!IsChecked) CalculateData();
            });
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            CopyCommand = new RelayCommand(obj => OnCopy(Convert.ToBoolean(obj)));
            TransferCommand = new RelayCommand(obj => OnTransfer(Convert.ToInt32(obj)));
        }

        public override void Init()
        {
            IsChecked = true;
            base.Init();
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            _selectedTypeShowAgency = null;
            _isShowColumnDonVi = false;
            ResetConditionSearch();
            LoadControlVisibility();
            LoadTypeDisplay();
            if (!string.IsNullOrEmpty(Model.STongHop))
                LoadComboboxTypeShow();
            if (VoucherType.NSBD_Key.Equals(Model.ILoaiChungTu.ToString()))
                LoadNganhByDonVi();
            if (Model.ILoaiDuKien == (int)EstimateSettlementType.SIX_MONTH)
            {
                _duKienQuyetToanDauNamTitle = "QT 6 tháng đầu năm";
                _duKienQuyetToanCuoiNamTitle = "Dự kiến QT 6 tháng cuối năm";
            }
            else
            {
                _duKienQuyetToanDauNamTitle = "QT 9 tháng đầu năm";
                _duKienQuyetToanCuoiNamTitle = "Dự kiến QT 3 tháng cuối năm";
            }
            OnPropertyChanged(nameof(DuKienQuyetToanDauNamTitle));
            OnPropertyChanged(nameof(DuKienQuyetToanCuoiNamTitle));
            LoadData();
            IsChecked = false;
        }

        private void ResetConditionSearch()
        {
            _searchLNS = string.Empty;
            _selectedLNS = string.Empty;
            xnmConcatenation = string.Empty;
            _filterResult = new HashSet<DcChungTuChiTietModel>();
            DetailFilter = new EstimationDetailCriteria();
            _listDanhMucNganh = new List<DanhMuc>();
            _searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Model.Id,
                LNS = Model.SDslns,
                YearOfWork = Model.INamLamViec,
                YearOfBudget = Model.INamNganSach,
                BudgetSource = Model.IIdMaNguonNganSach,
                IdDonVi = Model.IIdMaDonVi,
                LoaiDuKien = Model.ILoaiDuKien,
                LoaiChungTu = Model.ILoaiChungTu,
                VoucherDate = Model.DNgayChungTu,
                UserName = _sessionInfo.Principal
            };
            OnPropertyChanged(nameof(SearchLNS));
            OnPropertyChanged(nameof(SelectedLNS));
        }

        private void LoadComboboxTypeShow()
        {
            TypeShowAgency = new ObservableCollection<ComboboxItem>();
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgency = TypeShowAgency.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedTypeShowAgency));
        }

        private void LoadControlVisibility()
        {
            string lns = Model.SDslns;
            _listMLNS = _mlnsService.FindByListLnsDonVi(lns, _sessionInfo.YearOfWork).ToList();
            _columnVisibility = new DivisionColumnVisibility();
            _columnVisibility.IsDisplayTuChi = _listMLNS.Any(x => x.BTuChi);
            _columnVisibility.IsDisplayTuChiDieuChinh = _listMLNS.Any(x => x.BTuChi);
            _columnVisibility.IsDisplayHienVat = _listMLNS.Any(x => x.BHienVat);
            _columnVisibility.IsDisplayHienVatDieuChinh = _listMLNS.Any(x => x.BHienVat);
            _columnVisibility.IsDisplayDuPhong = _listMLNS.Any(x => x.BDuPhong);
            _columnVisibility.IsDisplayHangMua = _listMLNS.Any(x => x.BHangMua);
            _columnVisibility.IsDisplayHangMuaDieuChinh = _listMLNS.Any(x => x.BHangMua);
            _columnVisibility.IsDisplayHangNhap = _listMLNS.Any(x => x.BHangNhap);
            _columnVisibility.IsDisplayHangNhapDieuChinh = _listMLNS.Any(x => x.BHangNhap);
            _columnVisibility.IsDisplayPhanCap = _listMLNS.Any(x => x.BPhanCap);
            _columnVisibility.IsDisplayPhanCapDieuChinh = _listMLNS.Any(x => x.BPhanCap);
            OnPropertyChanged(nameof(ColumnVisibility));
        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Đã nhập dữ liệu" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Hiển thị tất cả" });
            TypeDisplaysSelected = TypeDisplay.TAT_CA;
        }

        private void OnResetFilter()
        {
            DetailFilter = new EstimationDetailCriteria();
            SelectedLNS = string.Empty;
            SelectedAgency = null;
            if (_chungTuChiTietItemsView != null)
            {
                BeForeRefresh();
                _chungTuChiTietItemsView.Refresh();
                if (!IsChecked) CalculateData();
            }
        }

        private void LoadLNSIndexCondition()
        {
            List<DcChungTuChiTietModel> listLNS = Items.Where(x => string.IsNullOrEmpty(x.SL) &&
                string.IsNullOrEmpty(x.SK) &&
                string.IsNullOrEmpty(x.SM) &&
                string.IsNullOrEmpty(x.STm) &&
                string.IsNullOrEmpty(x.STtm) &&
                string.IsNullOrEmpty(x.SNg) &&
                string.IsNullOrEmpty(x.STng)).ToList();
            listLNS.Insert(0, new DcChungTuChiTietModel
            {
                SLns = string.Empty,
                SMoTa = "-- TẤT CẢ --"
            });
            BudgetCatalogItems = new ObservableCollection<DcChungTuChiTietModel>(listLNS);
            _budgetCatalogItemsView = CollectionViewSource.GetDefaultView(BudgetCatalogItems);
            _budgetCatalogItemsView.Filter = BudgetCatalogItemsFilter;
        }

        private bool BudgetCatalogItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is DcChungTuChiTietModel item && (item.SLns.StartsWith(_searchLNS, StringComparison.OrdinalIgnoreCase) || item.SMoTa.StartsWith(_searchLNS, StringComparison.OrdinalIgnoreCase));
        }

        private void LoadNganhByDonVi()
        {
            List<DonVi> listNsDonVi = new List<DonVi>();
            listNsDonVi = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
            listNsDonVi = listNsDonVi.Where(x => x.BCoNSNganh && (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT)).ToList();
            var authenticationInfo = _mapper.Map<AuthenticationInfo>(_sessionInfo);
            _listDanhMucNganh = _danhMucNganhService.FindAll(authenticationInfo).Where(x => x.SGiaTri.Split(",").Any(y => listNsDonVi.Select(y => y.IIDMaDonVi).Contains(y))).ToList();
        }

        private void LoadData()
        {
            List<NsDcChungTuChiTietQuery> listChungTuChiTiet = new List<NsDcChungTuChiTietQuery>();
            Items = new ObservableCollection<DcChungTuChiTietModel>();
            DetailTotal = new AdjustedEstimateDetailTotalModel();
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                if (IsTongHop)
                {
                    List<string> soChungTus = Model.STongHop.Split(",").ToList();
                    var predicate = PredicateBuilder.True<NsDcChungTu>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && x.INamNganSach == _sessionInfo.YearOfBudget
                                                        && x.IIdMaNguonNganSach == _sessionInfo.Budget);
                    predicate = predicate.And(x => soChungTus.Contains(x.SSoChungTu));
                    List<NsDcChungTu> listChungTu = _chungTuService.FindByCondition(predicate).ToList();
                    if (Model.BTatCaDonVi.GetValueOrDefault())
                        _searchCondition.IdDonVi = GetAllDonVi();
                    else _searchCondition.IdDonVi = string.Join(",", listChungTu.Select(x => x.IIdMaDonVi).ToList());

                    if (SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
                    {
                        string donViIds = string.Join(",", listChungTu.Select(x => x.IIdMaDonVi));
                        LoadDonVi(donViIds);
                        var dictAgencies = Agencies.ToDictionary(x => x.ValueItem, x => x.DisplayItem);
                        List<NsDcChungTuChiTietQuery> listChungTuChiTietParent = new List<NsDcChungTuChiTietQuery>();
                        List<NsDcChungTuChiTietQuery> listChungTuChiTietChildren = new List<NsDcChungTuChiTietQuery>();
                        foreach (var chungTu in listChungTu)
                        {
                            var searchCondition = new EstimationVoucherDetailCriteria
                            {
                                VoucherId = chungTu.Id,
                                LNS = chungTu.SDslns,
                                YearOfWork = chungTu.INamLamViec,
                                YearOfBudget = chungTu.INamNganSach,
                                BudgetSource = chungTu.IIdMaNguonNganSach,
                                IdDonVi = chungTu.IIdMaDonVi,
                                LoaiDuKien = chungTu.ILoaiDuKien,
                                LoaiChungTu = chungTu.ILoaiChungTu,
                                VoucherDate = chungTu.DNgayChungTu,
                                UserName = _sessionInfo.Principal
                            };
                            List<NsDcChungTuChiTietQuery> listQuery = _chungTuChiTietService.FindByCondition(searchCondition).ToList();
                            listChungTuChiTietParent.AddRange(listQuery.Where(x => x.BHangCha));
                            listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.BHangCha && x.HasDataSummary).Select(x =>
                            {
                                x.STenDonVi = dictAgencies.ContainsKey(chungTu.IIdMaDonVi) ? dictAgencies[chungTu.IIdMaDonVi] : null;
                                return x;
                            }));
                        }
                        List<string> listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                        listChungTuChiTietParent = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x => x.First()).Distinct().ToList();
                        listChungTuChiTiet.AddRange(listChungTuChiTietParent);
                        listChungTuChiTiet.AddRange(listChungTuChiTietChildren);
                        listChungTuChiTiet = listChungTuChiTiet.OrderBy(x => x.SXauNoiMa).ToList();

                    }
                    else
                    {
                        _searchCondition.IdDonVi = _sessionInfo.IdDonVi;
                        listChungTuChiTiet = _chungTuChiTietService.FindByConditionTongSo(_searchCondition).ToList();
                    }

                }
                else
                    listChungTuChiTiet = _chungTuChiTietService.FindByCondition(_searchCondition).ToList();
                if (_listDanhMucNganh != null && _listDanhMucNganh.Count > 0)
                {
                    var listXauNoiMa = StringUtils.GetListXauNoiMaParent(listChungTuChiTiet.Where(x => !x.BHangCha && _listDanhMucNganh.Select(x => x.IIDMaDanhMuc).Contains(x.SNg)).Select(x => x.SXauNoiMa).ToList());
                    listChungTuChiTiet = listChungTuChiTiet.Where(x => listXauNoiMa.Contains(x.SXauNoiMa)).ToList();
                }
            }, (s, e) =>
            {
                IsLoading = false;
                Items = _mapper.Map<ObservableCollection<DcChungTuChiTietModel>>(listChungTuChiTiet);
                LoadLNSIndexCondition();
                // Using collection view
                _chungTuChiTietItemsView = CollectionViewSource.GetDefaultView(Items);
                _chungTuChiTietItemsView.Filter = ItemsViewFilter;

                if (Items != null)
                {
                    SelectedItem = Items.Where(x => !x.IsHangCha).FirstOrDefault();
                }
                foreach (var model in Items)
                {
                    if (model.IsEditable)
                    {
                        model.PropertyChanged += DetailModel_PropertyChanged;
                    }
                }

                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
                OnPropertyChanged(nameof(Items));
                CalculateData();
                OnInitDataFirst(AfterImport);
            });
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (DcChungTuChiTietModel)obj;
            result = ChungTuChiTietItemsViewFilter(item);
            if (!result && (item.IsHangCha || (!item.IsHangCha && ((HasInputData(item) && (TypeDisplaysSelected == TypeDisplay.TAT_CA || TypeDisplaysSelected == TypeDisplay.CO_DU_LIEU))))))
            {
                if (string.IsNullOrEmpty(item.SL))
                    result = xnmConcatenation.StartsWith(item.SLns);
                else if (SelectedAgency != null)
                {
                    result = xnmConcatenation.Contains(item.SXauNoiMa) && ((!string.IsNullOrEmpty(item.IIdMaDonVi) && Equals(item.IIdMaDonVi, SelectedAgency.ValueItem)) || string.IsNullOrEmpty(item.IIdMaDonVi));
                }
                else
                {
                    result = xnmConcatenation.Contains(item.SXauNoiMa);
                }
            }
            if (result)
                item.IsFilter = result;
            return result;
        }

        private bool ChungTuChiTietItemsViewFilter(DcChungTuChiTietModel item)
        {
            bool result = true;
            if (SelectedAgency != null)
                result = result && ((!string.IsNullOrEmpty(item.IIdMaDonVi) && item.IIdMaDonVi.Equals(SelectedAgency.ValueItem)) || string.IsNullOrEmpty(item.IIdMaDonVi));
            if (!string.IsNullOrEmpty(SelectedLNS))
                result = result && item.SLns.ToLower().StartsWith(SelectedLNS.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.L))
                result = result && item.SL.ToLower().StartsWith(DetailFilter.L.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.K))
                result = result && item.SK.ToLower().StartsWith(DetailFilter.K.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.SM.ToLower().StartsWith(DetailFilter.M.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.STm.ToLower().StartsWith(DetailFilter.TM.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.STtm.ToLower().StartsWith(DetailFilter.TTM.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.SNg.ToLower().StartsWith(DetailFilter.NG.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TNG))
                result = result && item.STng.ToLower().StartsWith(DetailFilter.TNG.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TNG1))
                result = result && item.STng1.ToLower().StartsWith(DetailFilter.TNG1.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TNG2))
                result = result && item.STng2.ToLower().StartsWith(DetailFilter.TNG2.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TNG3))
                result = result && item.STng3.ToLower().StartsWith(DetailFilter.TNG3.ToLower().Trim());

            if (!string.IsNullOrEmpty(TypeDisplaysSelected))
            {
                if (TypeDisplaysSelected == TypeDisplay.CO_DU_LIEU)
                    result = result && (HasInputData(item) || (item.IsModified && (item.IIdDcchungTu == Guid.Empty || item.IIdDcchungTu == null) && !item.IsDeleted));
            }
            item.IsFilter = result;
            return result;
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => ChungTuChiTietItemsViewFilter(item)).Where(item => !item.IsHangCha || (item.IsHangCha && HasInputData(item))).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        private bool HasInputData(DcChungTuChiTietModel item)
        {
            bool hasDataDuKienDauNam = item.FDuKienQtDauNam != 0;
            bool hasDataDuKienCuoiNam = item.FDuKienQtCuoiNam != 0;
            bool hasDataDuToanNganSachNam = item.FDuToanNganSachNam != 0;
            bool hasDataDuToanChuyenNamSau = item.FDuToanChuyenNamSau != 0;
            return hasDataDuKienDauNam || hasDataDuKienCuoiNam || hasDataDuToanNganSachNam || hasDataDuToanChuyenNamSau;
        }

        protected override void OnRefresh()
        {
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

        private void CalculateData()
        {
            IsChecked = true;
            Items.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FDuToanNganSachNam = 0;
                    x.FDuKienQtDauNam = 0;
                    x.FDuKienQtCuoiNam = 0;
                    x.FDuToanChuyenNamSau = 0;
                    //x.FTang = 0;
                    //x.FGiam = 0;
                    return x;
                }).ToList();

            //Items.Where(x => !x.IsHangCha)
            //    .ForAll(x =>
            //    {
            //        x.FTang = x.FTongCong - x.FDuToanConLai;
            //        x.FGiam = x.FDuToanConLai - x.FTongCong;
            //    });

            var listItem = Items.Where(x => x.IsEditable && x.IsFilter && (x.FDuToanNganSachNam != 0 || x.FDuKienQtDauNam != 0 || x.FDuKienQtCuoiNam != 0 || x.FDuToanChuyenNamSau != 0));
            DetailTotal = new AdjustedEstimateDetailTotalModel();

            foreach (var item in listItem)
            {
                CalculateParent(item, item);
            }
            CalculateTotal();
            IsChecked = false;
        }

        private void CalculateParent(DcChungTuChiTietModel currentItem, DcChungTuChiTietModel seftItem)
        {
            var parrentItem = Items.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FDuToanNganSachNam += seftItem.FDuToanNganSachNam;
            parrentItem.FDuKienQtDauNam += seftItem.FDuKienQtDauNam;
            parrentItem.FDuKienQtCuoiNam += seftItem.FDuKienQtCuoiNam;
            parrentItem.FDuToanChuyenNamSau += seftItem.FDuToanChuyenNamSau;
            //parrentItem.FTang += seftItem.FTang;
            //parrentItem.FGiam += seftItem.FGiam;
            CalculateParent(parrentItem, seftItem);
        }

        private void CalculateTotal()
        {
            DetailTotal = new AdjustedEstimateDetailTotalModel();
            Items.Where(x => x.IIdMlnsCha.IsNullOrEmpty())
                .ForAll(item =>
                {
                    DetailTotal.FTongDuToanNganSachNam += item.FDuToanNganSachNam;
                    DetailTotal.FTongDuKienQuyetToanDauNam += item.FDuKienQtDauNam;
                    DetailTotal.FTongDuKienQuyetToanCuoiNam += item.FDuKienQtCuoiNam;
                    DetailTotal.FTongTongCong += item.FTongCong;
                    DetailTotal.FTongDuToanChuyenNamSau += item.FDuToanChuyenNamSau;
                    DetailTotal.FTongDuToanConLai += item.FDuToanConLai;
                    DetailTotal.FTongGiam = DetailTotal.FTongDuToanConLai > DetailTotal.FTongTongCong ? DetailTotal.FTongDuToanConLai - DetailTotal.FTongTongCong : 0;
                    DetailTotal.FTongTang = DetailTotal.FTongDuToanConLai < DetailTotal.FTongTongCong ? DetailTotal.FTongTongCong - DetailTotal.FTongDuToanConLai : 0;
                });
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if ((args.PropertyName == nameof(DcChungTuChiTietModel.FDuKienQtDauNam)
                || args.PropertyName == nameof(DcChungTuChiTietModel.FDuKienQtCuoiNam)
                || args.PropertyName == nameof(DcChungTuChiTietModel.FDuToanNganSachNam)
                || args.PropertyName == nameof(DcChungTuChiTietModel.FDuToanChuyenNamSau)) && !IsChecked)
            {
                DcChungTuChiTietModel item = (DcChungTuChiTietModel)sender;
                item.IsModified = true;
                IsChecked = true;
                CalculateData();
                IsChecked = false;
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            } else if (args.PropertyName == nameof(DcChungTuChiTietModel.SGhiChu))
            {
                DcChungTuChiTietModel item = sender as DcChungTuChiTietModel;
                item.IsModified = true;
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        public void OnInitDataFirst(bool afterImport = false)
        {
            var data = _chungTuChiTietService.FindByChungTuID(Model.Id).ToList();
            if (!data.Any())
            {
                List<DcChungTuChiTietModel> listChungTuChiTietAdd = Items.Where(x => !x.IsHangCha && HasInputData(x) && x.IIdDcchungTu.IsNullOrEmpty()).ToList();
                if (listChungTuChiTietAdd.Count > 0)
                {
                    listChungTuChiTietAdd = listChungTuChiTietAdd.Select(x =>
                    {
                        x.Id = Guid.NewGuid();
                        x.IIdDcchungTu = Model.Id;
                        x.INamNganSach = Model.INamNganSach;
                        x.INamLamViec = Model.INamLamViec;
                        x.IIdMaNguonNganSach = Model.IIdMaNguonNganSach;
                        x.SNguoiTao = _sessionInfo.Principal;
                        x.IIdMaDonVi = Model.IIdMaDonVi;
                        x.DNgayTao = DateTime.Now;
                        return x;
                    }).ToList();

                    List<NsDcChungTuChiTiet> listChungTuChiTiets = _mapper.Map<List<NsDcChungTuChiTiet>>(listChungTuChiTietAdd);
                    _chungTuChiTietService.AddRange(listChungTuChiTiets);
                }
                // Cập nhật thông tin chứng từ
                UpdateChungTu();

                // Refresh dữ liệu ở màn index
                UpdateVoucherEvent?.Invoke(Model, new EventArgs());
            }
            else if (afterImport)
            {
                List<DcChungTuChiTietModel> listChungTuChiTietAdd = Items.Where(x => !x.IsHangCha && HasInputData(x) && x.IIdDcchungTu.IsNullOrEmpty()).ToList();
                List<DcChungTuChiTietModel> listChungTuChiTietUpdate = Items.Where(x => !x.IsHangCha && HasInputData(x) && !x.IIdDcchungTu.IsNullOrEmpty()).ToList();

                List<NsDcChungTuChiTiet> listUpdate = _mapper.Map<List<NsDcChungTuChiTiet>>(listChungTuChiTietUpdate).Select(x =>
                {
                    x.SNguoiSua = _sessionInfo.Principal;
                    x.DNgaySua = DateTime.Now;
                    return x;
                }).ToList();

                foreach (var item in listChungTuChiTietUpdate)
                {
                    NsDcChungTuChiTiet chungTuChiTiet = _chungTuChiTietService.FindById(item.Id);
                    if (chungTuChiTiet != null)
                    {
                        chungTuChiTiet.SNguoiSua = _sessionInfo.Principal;
                        chungTuChiTiet.DNgaySua = DateTime.Now;
                        _mapper.Map(item, chungTuChiTiet);
                        _chungTuChiTietService.Update(chungTuChiTiet);
                    }
                }

                listChungTuChiTietAdd = listChungTuChiTietAdd.Select(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.IIdDcchungTu = Model.Id;
                    x.INamNganSach = Model.INamNganSach;
                    x.INamLamViec = Model.INamLamViec;
                    x.IIdMaNguonNganSach = Model.IIdMaNguonNganSach;
                    x.SNguoiTao = _sessionInfo.Principal;
                    x.IIdMaDonVi = Model.IIdMaDonVi;
                    x.DNgayTao = DateTime.Now;
                    return x;
                }).ToList();

                List<NsDcChungTuChiTiet> listChungTuChiTiets = _mapper.Map<List<NsDcChungTuChiTiet>>(listChungTuChiTietAdd);
                _chungTuChiTietService.AddRange(listChungTuChiTiets);
                _chungTuChiTietService.UpdateRange(listUpdate);
                // Cập nhật thông tin chứng từ
                UpdateChungTu();

                // Refresh dữ liệu ở màn index
                UpdateVoucherEvent?.Invoke(Model, new EventArgs());
            }
        }

        public void OnSaveData(bool popMessage = true)
        {
            List<DcChungTuChiTietModel> listChungTuChiTietAdd = Items.Where(x => !x.IsHangCha && HasInputData(x) && x.IIdDcchungTu.IsNullOrEmpty()).ToList();
            List<DcChungTuChiTietModel> listChungTuChiTietUpdate = Items.Where(x => !x.IsHangCha && x.IsModified && !x.IsDeleted && !x.IIdDcchungTu.IsNullOrEmpty()).ToList();
            List<DcChungTuChiTietModel> listChungTuChiTietDelete = Items.Where(x => !x.IsHangCha && x.IsModified && (x.IsDeleted || !HasInputData(x)) && x.IIdDcchungTu != null).ToList();
            List<DcChungTuChiTietModel> listChungTu = new List<DcChungTuChiTietModel>();
            // Thêm mới chứng từ chi tiết
            if (listChungTuChiTietAdd.Count > 0)
            {
                listChungTuChiTietAdd = listChungTuChiTietAdd.Select(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.IIdDcchungTu = Model.Id;
                    x.INamNganSach = Model.INamNganSach;
                    x.INamLamViec = Model.INamLamViec;
                    x.IIdMaNguonNganSach = Model.IIdMaNguonNganSach;
                    x.SNguoiTao = _sessionInfo.Principal;
                    x.IIdMaDonVi = Model.IIdMaDonVi;
                    x.DNgayTao = DateTime.Now;
                    return x;
                }).ToList();

                List<NsDcChungTuChiTiet> listChungTuChiTiets = _mapper.Map<List<NsDcChungTuChiTiet>>(listChungTuChiTietAdd);
                _chungTuChiTietService.AddRange(listChungTuChiTiets);
            }

            // Cập nhật chứng từ chi tiết
            if (listChungTuChiTietUpdate.Count > 0)
            {
                List<NsDcChungTuChiTiet> listAdd = new List<NsDcChungTuChiTiet>();
                foreach (var item in listChungTuChiTietUpdate)
                {
                    NsDcChungTuChiTiet chungTuChiTiet = _chungTuChiTietService.FindById(item.Id);
                    if (chungTuChiTiet != null)
                    {
                        chungTuChiTiet.SNguoiSua = _sessionInfo.Principal;
                        chungTuChiTiet.DNgaySua = DateTime.Now;
                        _mapper.Map(item, chungTuChiTiet);
                        _chungTuChiTietService.Update(chungTuChiTiet);
                    }
                    else
                    {
                        chungTuChiTiet = _mapper.Map<NsDcChungTuChiTiet>(item);
                        chungTuChiTiet.IIdMaDonVi = Model.IIdMaDonVi;
                        chungTuChiTiet.SNguoiTao = _sessionInfo.Principal;
                        chungTuChiTiet.DNgayTao = DateTime.Now;
                        listAdd.Add(chungTuChiTiet);
                    }
                }
                if (listAdd.Count > 0)
                    _chungTuChiTietService.AddRange(listAdd);

                if (IsTongHop && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    List<Guid> mlnsIds = listChungTuChiTietUpdate.Select(x => x.IIdMlns).Distinct().ToList();
                    //cập nhật chứng từ chi tiết của chứng từ tổng hợp
                    foreach (var mlnsId in mlnsIds)
                    {
                        NsDcChungTuChiTiet tongHopChiTiet = _chungTuChiTietService.FindByChungTuIdAndMlnsId(Model.Id, mlnsId);
                        if (tongHopChiTiet != null)
                        {
                            var details = Items.Where(x => x.IIdMlns == mlnsId).ToList();
                            tongHopChiTiet.FDuKienQtDauNam = details.Sum(x => x.FDuKienQtDauNam);
                            tongHopChiTiet.FDuKienQtCuoiNam = details.Sum(x => x.FDuKienQtCuoiNam);
                            tongHopChiTiet.FDuToanNganSachNam = details.Sum(x => x.FDuToanNganSachNam);
                            tongHopChiTiet.FDuToanChuyenNamSau = details.Sum(x => x.FDuToanChuyenNamSau);
                            _chungTuChiTietService.Update(tongHopChiTiet);
                        }
                    }

                    //cập nhật thông tin chứng từ con
                    List<Guid> chungTuIds = listChungTuChiTietUpdate.Where(x => x.IIdDcchungTu.HasValue).Select(x => x.IIdDcchungTu.Value).Distinct().ToList();
                    foreach (var chungTuId in chungTuIds)
                    {
                        NsDcChungTu chungTu = _chungTuService.FindById(chungTuId);
                        var details = Items.Where(x => x.IIdDcchungTu == chungTuId).ToList();
                        chungTu.FDieuChinh = details.Sum(x => x.FTongCong);
                        _chungTuService.Update(chungTu);
                    }
                }
            }

            // Xóa chứng từ chi tiết
            _chungTuChiTietService.DeleteByIds(listChungTuChiTietDelete.Select(x => x.Id.ToString()));
            foreach (var item in listChungTuChiTietDelete)
            {
                item.FDuKienQtDauNam = 0;
                item.FDuKienQtCuoiNam = 0;
                item.FDuToanNganSachNam = 0;
                item.FDuToanChuyenNamSau = 0;
                item.SGhiChu = string.Empty;
                item.IsDeleted = false;
                item.IsModified = false;
                item.IIdDcchungTu = Guid.Empty;
            }
            // Reset value has changed
            Items.Select(x => { x.IsModified = false; x.IsDeleted = false; return x; }).ToList();

            //Cập nhật thông tin chứng từ
            UpdateChungTu();

            if (popMessage)
            {
                MessageBoxHelper.Info(Resources.MsgSaveDone);
            }

            //refresh dữ liệu ở màn index
            UpdateVoucherEvent?.Invoke(Model, new EventArgs());
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha)
            {
                SelectedItem.IsModified = true;
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                if (!IsChecked) CalculateData();
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            var result = MessageBoxHelper.Confirm(Resources.DeleteAllChungTuChiTiet);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                if (Items != null)
                {
                    Items.Where(x => x.IsFilter && !x.IsHangCha).ForAll(x => x.IsDeleted = true);
                    OnSave();
                }
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void UpdateChungTu()
        {
            NsDcChungTu chungTu = _chungTuService.FindById(Model.Id);
            var childs = Items.Where(x => !x.IsHangCha && (x.FDuKienQtDauNam != 0 || x.FDuKienQtCuoiNam != 0 || x.FDuToanNganSachNam != 0 || x.FDuToanChuyenNamSau != 0)).ToList();
            chungTu.FDieuChinh = childs.Sum(x => x.FTongCong);
            _chungTuService.Update(chungTu);
            Model.FDieuChinh = chungTu.FDieuChinh;
        }

        protected override void OnLockUnLock()
        {
            base.OnLockUnLock();
            string message = Model.BKhoa ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = Model.BKhoa ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult result = MessageBoxHelper.Confirm(message);
            if (result == MessageBoxResult.Yes)
            {
                _chungTuService.LockOrUnLock(Model.Id, !Model.BKhoa);
                Model.BKhoa = !Model.BKhoa;
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
                MessageBoxHelper.Info(msgDone);
            }
        }

        public override void OnClose(object obj)
        {
            base.OnClose(obj);
            Window window = obj as Window;
            window.Close();
        }

        private void OnCopy(bool isCopyAll)
        {
            var items = Items.Where(x => !x.IsHangCha && (x.FDuToanNganSachNam != 0 || x.FDuToanConLai != 0 || x.FDuKienQtDauNam != 0 || x.FDuToanChuyenNamSau != 0)).ToList();
            IsChecked = true;
            foreach (var item in items)
            {
                if (isCopyAll || (!isCopyAll && item.FDuKienQtCuoiNam == 0))
                {
                    item.FDuKienQtCuoiNam = item.FDuToanConLai - item.FDuKienQtDauNam;
                    item.IsModified = true;
                }
            }
            IsChecked = false;
            CalculateData();
        }

        private void OnTransfer(int type)
        {
            if (type == 1)
            {
                var condition = new EstimationVoucherDetailCriteria
                {
                    VoucherId = Model.Id,
                    LNS = Model.SDslns,
                    YearOfWork = Model.INamLamViec,
                    YearOfBudget = Model.INamNganSach,
                    BudgetSource = Model.IIdMaNguonNganSach,
                    IdDonVi = _sessionInfo.IdDonVi,
                    LoaiDuKien = Model.ILoaiDuKien,
                    LoaiChungTu = Model.ILoaiChungTu,
                    VoucherDate = Model.DNgayChungTu,
                    UserName = _sessionInfo.Principal
                };
                var listChungTuChiTiet = _chungTuChiTietService.FindDuToanByCondition(_searchCondition).GroupBy(x => x.SXauNoiMa).ToDictionary(x => x.Key, x => x.Sum(y => y.FDuToanNganSachNam));
                IsChecked = true;
                foreach (var item in Items)
                {
                    if (listChungTuChiTiet.ContainsKey(item.SXauNoiMa))
                    {
                        item.FDuToanNganSachNam = listChungTuChiTiet[item.SXauNoiMa] ?? 0;
                        item.IsModified = true;
                    }
                }
                OnPropertyChanged(nameof(IsSaveData));
                IsChecked = false;
                CalculateData();
            }
            else if (type == 2)
            {
                if (!string.IsNullOrEmpty(Model.STongHop))
                {
                    List<string> soChungTus = Model.STongHop.Split(",").ToList();
                    var predicate = PredicateBuilder.True<NsDcChungTu>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && x.INamNganSach == _sessionInfo.YearOfBudget
                                                        && x.IIdMaNguonNganSach == _sessionInfo.Budget);
                    predicate = predicate.And(x => soChungTus.Contains(x.SSoChungTu));
                    List<NsDcChungTu> listChungTu = _chungTuService.FindByCondition(predicate).ToList();
                    var listChungTuChiTiet = _chungTuChiTietService.FindByCondition(x => listChungTu.Select(t => t.Id).Contains(x.IIdDcchungTu)).GroupBy(x => x.SXauNoiMa).ToDictionary(x => x.Key, x => x.Sum(y => y.FDuToanNganSachNam));
                    IsChecked = true;
                    foreach (var item in Items)
                    {
                        if (listChungTuChiTiet.ContainsKey(item.SXauNoiMa))
                        {
                            item.FDuToanNganSachNam = listChungTuChiTiet[item.SXauNoiMa] ?? 0;
                            item.IsModified = true;
                        }
                    }
                    OnPropertyChanged(nameof(IsSaveData));
                    IsChecked = false;
                    CalculateData();
                }
            }
        }

        private string GetAllDonVi()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => LoaiDonVi.NOI_BO.Equals(x.Loai));
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            // remove 999 hard code
            predicate = predicate.And(x => !x.IIDMaDonVi.Equals("999"));
            var listDonVi = _donViService.FindByCondition(predicate).ToList();
            return string.Join(",", listDonVi.Select(x => x.IIDMaDonVi));
        }

        private void LoadDonVi(string donViIds)
        {
            var listDonVi = _donViService.FindByListIdDonVi(donViIds, _sessionInfo.YearOfWork);
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            OnPropertyChanged(nameof(Agencies));
        }
    }
}
