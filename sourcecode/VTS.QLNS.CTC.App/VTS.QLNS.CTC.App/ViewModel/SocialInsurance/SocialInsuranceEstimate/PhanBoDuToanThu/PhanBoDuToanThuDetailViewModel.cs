using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu
{
    public class PhanBoDuToanThuDetailViewModel : DetailViewModelBase<BhDtPhanBoChungTuModel, BhDtPhanBoChungTuChiTietModel>
    {
        private readonly IDttBHXHPhanBoChiTietService _dttChungTuChiTietService;
        private readonly IDttBHXHPhanBoService _dtChungTuService;
        private readonly IBhDttNhanPhanBoMapService _dtChungTuMapService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _budgetCatalogItemsView;
        private ICollectionView ItemsView;
        private readonly INsDonViService _nsDonViService;
        private readonly IBhDmMucLucNganSachService _mlnsService;
        private readonly IDanhMucService _danhMucService;
        private readonly ILog _logger;
        private readonly IDttBHXHChiTietService _dttBHXHChiTietService;
        private readonly IKhtBHXHService _khtService;
        private readonly IKhtBHXHChiTietService _khtChiTietService;
        private readonly IBhDcDuToanThuChiTietService _dcDttService;
        private bool _isNamLuyKe;
        private bool _isShowQuyetDinh;
        private List<BhDmMucLucNganSach> _listMLNS;
        private SessionInfo _sessionInfo;
        private ICollection<BhDtPhanBoChungTuChiTietModel> _filterResult = new HashSet<BhDtPhanBoChungTuChiTietModel>();
        private List<Tuple<string, Guid?>> _filterResultWithSQD = new List<Tuple<string, Guid?>>();
        private string _xnmConcatenation = "";
        private Dictionary<string, List<string>> _dicDonViNganh = new Dictionary<string, List<string>>();
        private string xnmConcatenation = "";

        public HashSet<string> _filterXauNoiMaDTTBHXH = new HashSet<string>();
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateVoucherEvent;
        public bool IsSaveData;
        public bool IsDuToanDauNam => (Model.ILoaiDuToan.HasValue);
        public bool IsDieuChinh => (Model.ILoaiDuToan.HasValue);
        public bool IsDeleteAll => Items.Any(item => !item.IsModified && !item.IsConLai && !item.IsPhanBo && item.HasData);
        public bool IsTypeLuyKe => _isNamLuyKe;
        public bool IsShowQuyetDinh => _isShowQuyetDinh;
        public bool HasLastDivisionEstimateVoucher { get; set; }
        public bool IsShowPlanButton => Model.ILoaiDuToan == (int)EstimateTypeNum.YEAR;
        public bool IsShowAggregateAdjustButtonPBDTT => Model.ILoaiDuToan == (int)EstimateTypeNum.ADDITIONAL;
        public int NamLamViec { get; set; }
        public Dictionary<string, List<BhDtPhanBoChungTu>> DicNhanPhanBo = new Dictionary<string, List<BhDtPhanBoChungTu>>();
        public Dictionary<string, List<BhDtPhanBoChungTuChiTiet>> DicDtChungTuChiTiet = new Dictionary<string, List<BhDtPhanBoChungTuChiTiet>>();

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _budgetCatalogItemsView.Refresh();
                }
            }
        }

        private ObservableCollection<BhDtPhanBoChungTuChiTietModel> _budgetCatalogItems;
        public ObservableCollection<BhDtPhanBoChungTuChiTietModel> BudgetCatalogItems
        {
            get => _budgetCatalogItems;
            set => SetProperty(ref _budgetCatalogItems, value);
        }

        private BhDtPhanBoChungTuChiTietModel _selectedBudgetCatalog;
        public BhDtPhanBoChungTuChiTietModel SelectedBudgetCatalog
        {
            get => _selectedBudgetCatalog;
            set
            {
                if (SetProperty(ref _selectedBudgetCatalog, value))
                {
                    if (_selectedBudgetCatalog != null)
                        SelectedLNS = _selectedBudgetCatalog.SLns;
                    BeForeRefresh();
                    ItemsView.Refresh();
                }
                CalculateTotalParent();
                IsOpenLnsPopup = false;
            }
        }

        private bool _isOpenLnsPopup;
        public bool IsOpenLnsPopup
        {
            get => _isOpenLnsPopup;
            set => SetProperty(ref _isOpenLnsPopup, value);
        }

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set
            {
                if (SetProperty(ref _sNoiDungSearch, value))
                {
                    SearchTextFilter();
                    ItemsView.Refresh();
                    //_budgetCatalogItemsView.Refresh();
                }
            }
        }

        private ObservableCollection<BhDtPhanBoChungTuChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhDtPhanBoChungTuChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhDtPhanBoChungTuChiTietModel _selectedPopupItem;
        public BhDtPhanBoChungTuChiTietModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SNoiDungSearch = _selectedPopupItem?.SMoTa;
                OnPropertyChanged(nameof(SNoiDungSearch));
                IsPopupOpen = false;
            }
        }

        private ObservableCollection<BhDtPhanBoChungTuChiTietModel> _dataSearch;
        public ObservableCollection<BhDtPhanBoChungTuChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

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

        private ObservableCollection<ComboboxItem> _agencies;
        public ObservableCollection<ComboboxItem> Agencies
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
                ItemsView.Refresh();
                CalculateTotalParent();
                OnPropertyChanged(nameof(IsEnablePhanBoAll));
            }
        }

        public Visibility ShowColNSSD { get; set; }
        public bool FirstTimePhanBo { get; set; }

        private DivisionEstimateDetailPropertyHelper _detailHelper = new DivisionEstimateDetailPropertyHelper();
        public DivisionEstimateDetailPropertyHelper DetailHelper
        {
            get => _detailHelper;
            set => SetProperty(ref _detailHelper, value);
        }
        public bool IsEnablePhanBoAll => SelectedAgency != null;

        public IEnumerable<BhDtPhanBoChungTuModel> DtChungTuModelNhanPhanBos { get; set; }
        public IEnumerable<BhDtPhanBoChungTuModel> DtChungTuModelPhanBos { get; set; }
        private ObservableCollection<ComboboxItem> _cbxNhanPhanBos;
        public ObservableCollection<ComboboxItem> CbxNhanPhanBos
        {
            get => _cbxNhanPhanBos;
            set => SetProperty(ref _cbxNhanPhanBos, value);
        }
        private ObservableCollection<ComboboxItem> _typeDisplays;
        public ObservableCollection<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private string _typeDisplaysselected;
        public string TypeDisplaysSelected
        {
            get => _typeDisplaysselected;
            set
            {
                if (SetProperty(ref _typeDisplaysselected, value) && ItemsView != null)
                {
                    OnRefresh();
                    BeForeRefresh();
                    ItemsView.Refresh();
                    CalculateTotalParent();
                }
            }
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private DttDivisionColumnVisibility _columnVisibility;
        public DttDivisionColumnVisibility ColumnVisibility
        {
            get => _columnVisibility;
            set => SetProperty(ref _columnVisibility, value);
        }

        private DanhMucNganhModel _selectedNNganh;
        public DanhMucNganhModel SelectedNNganh
        {
            get => _selectedNNganh;
            set
            {
                if (SetProperty(ref _selectedNNganh, value))
                {
                    BeForeRefresh();
                    ItemsView.Refresh();
                    CalculateTotalParent();
                }
            }
        }

        private bool _isAdjusted;
        public bool IsAdjusted
        {
            get => _isAdjusted;
            set => SetProperty(ref _isAdjusted, value);
        }
        public bool IsFillDataDauNam { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand SaveDataCommand { get; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand FillDataDieuChinhCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand PhanBoConLaiCommand { get; }
        public RelayCommand GetPlanDataCommand { get; }
        public RelayCommand GetAggregateAdjustDataCommand { get; }
        public TongHopThuChiViewModel TongHopThuChiViewModel { get; }
        public SocialInsuranceDivisionEstimatePrintSheetViewModel SocialInsuranceDivisionEstimatePrintSheetViewModel { get; set; }
        public PrintPhuLucGiaoDuToanDuToanThuViewModel PrintPhuLucGiaoDuToanDuToanThuViewModel { get; set; }
        public PrintPhuLucDuToanThuViewModel PrintPhuLucDuToanThuViewModel { get; }

        public PhanBoDuToanThuDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IDttBHXHPhanBoChiTietService dtChungTuChiTietService,
            IDttBHXHPhanBoService dtChungTuService,
            IBhDttNhanPhanBoMapService dtChungTuMapService,
            INsDonViService nsDonViService,
            IBhDmMucLucNganSachService mlnsService,
            IDttBHXHChiTietService dttBHXHChiTietService,
            IKhtBHXHService iKhtBHXHService,
            IKhtBHXHChiTietService iKhtBHXHChiTietService,
            IBhDcDuToanThuChiTietService iBhDcDuToanThuChiTietService,
            ILog logger,
            TongHopThuChiViewModel tongHopThuChiViewModel,
            SocialInsuranceDivisionEstimatePrintSheetViewModel socialInsuranceDivisionEstimatePrintSheetViewModel,
            PrintPhuLucDuToanThuViewModel printPhuLucDuToanThuViewModel,
            PrintPhuLucGiaoDuToanDuToanThuViewModel printPhuLucGiaoDuToanDuToanThuViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _dttChungTuChiTietService = dtChungTuChiTietService;
            _dtChungTuService = dtChungTuService;
            _dtChungTuMapService = dtChungTuMapService;
            _nsDonViService = nsDonViService;
            _mlnsService = mlnsService;
            _logger = logger;
            _dttBHXHChiTietService = dttBHXHChiTietService;
            _khtService = iKhtBHXHService;
            _khtChiTietService = iKhtBHXHChiTietService;
            _dcDttService = iBhDcDuToanThuChiTietService;
            TongHopThuChiViewModel = tongHopThuChiViewModel;
            SocialInsuranceDivisionEstimatePrintSheetViewModel = socialInsuranceDivisionEstimatePrintSheetViewModel;
            PrintPhuLucDuToanThuViewModel = printPhuLucDuToanThuViewModel;
            SearchCommand = new RelayCommand(obj => { BeForeRefresh(); ItemsView.Refresh(); });
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            PhanBoConLaiCommand = new RelayCommand(obj => OnPhanBoConLai());
            GetPlanDataCommand = new RelayCommand(obj => GetPlanData());
            GetAggregateAdjustDataCommand = new RelayCommand(obj => GetAggregateAdjustData());
            PrintPhuLucGiaoDuToanDuToanThuViewModel = printPhuLucGiaoDuToanDuToanThuViewModel;
        }

        public override void Init()
        {
            base.Init();
            IsFillDataDauNam = true;
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            ResetConditionSearch();
            LoadDotNhan();
            LoadControlVisibility();
            LoadTypeDisplay();
            LoadAgencies();
            LoadData();
            CheckLastDivisionEstimateVoucher();
        }

        private void LoadControlVisibility()
        {
            _columnVisibility = new DttDivisionColumnVisibility();
            _columnVisibility.IsDisplayBhxhDieuChinh = _isAdjusted;

            DetailHelper.VisibilityBudgetTypeAdjusted = _isAdjusted ? Visibility.Visible : Visibility.Collapsed;
            DetailHelper.VisibilityBudgetTypeNoneAdjusted = !_isAdjusted ? Visibility.Visible : Visibility.Collapsed;
            OnPropertyChanged(nameof(ColumnVisibility));
        }

        private void ResetConditionSearch()
        {
            DetailFilter = new EstimationDetailCriteria();
            _dicDonViNganh = new Dictionary<string, List<string>>();
            Items = new ObservableCollection<BhDtPhanBoChungTuChiTietModel>();
            _isAdjusted = false;
            if (EstimateTypeNum.ADJUSTED.Equals((EstimateTypeNum)Model.ILoaiDuToan.Value))
                _isAdjusted = true;
        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Tất cả" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.DA_NHAN_DUTOAN, DisplayItem = "Đã nhận dự toán" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Đã nhập dữ liệu" });
            TypeDisplaysSelected = TypeDisplay.DA_NHAN_DUTOAN;
        }

        private void OnResetFilter()
        {
            DetailFilter = new EstimationDetailCriteria();
            SelectedLNS = string.Empty;
            if (ItemsView != null)
            {
                BeForeRefresh();
                ItemsView.Refresh();
                CalculateTotalParent();
            }
        }

        private void LoadLNSIndexCondition()
        {
            List<BhDtPhanBoChungTuChiTietModel> listLNS = Items.Where(x => string.IsNullOrEmpty(x.SL) &&
                string.IsNullOrEmpty(x.SK) &&
                string.IsNullOrEmpty(x.SM) &&
                string.IsNullOrEmpty(x.STm) &&
                string.IsNullOrEmpty(x.STtm) &&
                string.IsNullOrEmpty(x.SNg) &&
                string.IsNullOrEmpty(x.STng) &&
                !x.IsConLai).ToList();
            listLNS.Insert(0, new BhDtPhanBoChungTuChiTietModel
            {
                SLns = string.Empty,
                SMoTa = "-- TẤT CẢ --"
            });
            BudgetCatalogItems = new ObservableCollection<BhDtPhanBoChungTuChiTietModel>(listLNS);
            _budgetCatalogItemsView = CollectionViewSource.GetDefaultView(BudgetCatalogItems);
            _budgetCatalogItemsView.Filter = BudgetCatalogItemsFilter;
        }

        private void LoadData()
        {
            var listDonViQuanLy = _sessionService.Current.IdsDonViQuanLy.Split(StringUtils.COMMA).ToList();
            var listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).Where(x => x.Loai.Equals(LoaiDonVi.ROOT)).Select(y => y.IIDMaDonVi).ToList();
            var listIntersect = listDonViQuanLy.Intersect(listDonVi);

            int iNamLamViec = _sessionInfo.YearOfWork;
            string sLNS = Model.SDslns;
            string sIdDonVi = Model.SDsidMaDonVi;

            List<BhDtPhanBoChungTuChiTietQuery> listDataQuery = new List<BhDtPhanBoChungTuChiTietQuery>();

            if (Model != null && Model.ILoaiDuToan == (int)EstimateTypeNum.ADJUSTED)
            {
                listDataQuery = _dttChungTuChiTietService.FindChungTuChiTietDieuChinh(Model.Id, sLNS, iNamLamViec, _sessionInfo.Principal).ToList();
            }
            else
            {
                listDataQuery = _dttChungTuChiTietService.FindChungTuChiTiet(Model.Id, sLNS, sIdDonVi, iNamLamViec, _sessionInfo.Principal).ToList();
            }

            Items = _mapper.Map<ObservableCollection<BhDtPhanBoChungTuChiTietModel>>(listDataQuery);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhDtPhanBoChungTuChiTietModel>>(listDataQuery);
            BeForeRefresh();
            ItemsView = CollectionViewSource.GetDefaultView(Items);
            ItemsView.Filter = ItemsViewFilter;
            foreach (var item in Items)
            {
                item.IsFilter = true;
                if (!item.BHangCha)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        BhDtPhanBoChungTuChiTietModel item = (BhDtPhanBoChungTuChiTietModel)sender;
                        if (args.PropertyName == nameof(SelectedItem.FBHXHNLD) || args.PropertyName == nameof(SelectedItem.FBHXHNSD) ||
                            args.PropertyName == nameof(SelectedItem.FBHYTNLD) || args.PropertyName == nameof(SelectedItem.FBHYTNSD) ||
                            args.PropertyName == nameof(SelectedItem.FBHTNNLD) || args.PropertyName == nameof(SelectedItem.FBHTNNSD) ||
                            args.PropertyName == nameof(SelectedItem.SGhiChu))
                        {
                            item.IsModified = true;
                            item.FBHXHNLDSauDieuChinh = (item.FBHXHNLD ?? 0) + (item.FBHXHNLDTruocDieuChinh ?? 0);
                            item.FBHXHNSDSauDieuChinh = (item.FBHXHNSD ?? 0) + (item.FBHXHNSDTruocDieuChinh ?? 0);
                            item.FBHYTNLDSauDieuChinh = (item.FBHYTNLD ?? 0) + (item.FBHYTNLDTruocDieuChinh ?? 0);
                            item.FBHYTNSDSauDieuChinh = (item.FBHYTNSD ?? 0) + (item.FBHYTNSDTruocDieuChinh ?? 0);
                            item.FBHTNNLDSauDieuChinh = (item.FBHTNNLD ?? 0) + (item.FBHTNNLDTruocDieuChinh ?? 0);
                            item.FBHTNNSDSauDieuChinh = (item.FBHTNNSD ?? 0) + (item.FBHTNNSDTruocDieuChinh ?? 0);

                            CalculateData();
                            if (!IsAdjusted)
                            {
                                CalculateRemainRow();
                            }
                        }
                        OnPropertyChanged(nameof(IsSaveData));
                    };
                }
            }
            CalculateData();
            CalculateRemainRow();
        }
        private void CalculateRemainRow()
        {
            var lstRemainRows = Items.Where(x => x.IsRemainRow);
            foreach (var item in lstRemainRows)
            {
                if (Items.Any(y => y.BHangCha && y.SXauNoiMa.Equals(item.SXauNoiMa) && y.IIDCTDuToanNhan.Equals(item.IIDCTDuToanNhan) && !y.IsRemainRow))
                {
                    var itemBHXHNLD = Items.Where(y => !y.IsRemainRow && !y.BHangCha && y.SXauNoiMa.Equals(item.SXauNoiMa) && y.IIDCTDuToanNhan.Equals(item.IIDCTDuToanNhan)).Sum(s => s.FBHXHNLD.GetValueOrDefault());
                    var itemBHXHNSD = Items.Where(y => !y.IsRemainRow && !y.BHangCha && y.SXauNoiMa.Equals(item.SXauNoiMa) && y.IIDCTDuToanNhan.Equals(item.IIDCTDuToanNhan)).Sum(s => s.FBHXHNSD.GetValueOrDefault());
                    var itemBHYTNLD = Items.Where(y => !y.IsRemainRow && !y.BHangCha && y.SXauNoiMa.Equals(item.SXauNoiMa) && y.IIDCTDuToanNhan.Equals(item.IIDCTDuToanNhan)).Sum(s => s.FBHYTNLD.GetValueOrDefault());
                    var itemBHYTNSD = Items.Where(y => !y.IsRemainRow && !y.BHangCha && y.SXauNoiMa.Equals(item.SXauNoiMa) && y.IIDCTDuToanNhan.Equals(item.IIDCTDuToanNhan)).Sum(s => s.FBHYTNSD.GetValueOrDefault());
                    var itemBHTNNLD = Items.Where(y => !y.IsRemainRow && !y.BHangCha && y.SXauNoiMa.Equals(item.SXauNoiMa) && y.IIDCTDuToanNhan.Equals(item.IIDCTDuToanNhan)).Sum(s => s.FBHTNNLD.GetValueOrDefault());
                    var itemBHTNNSD = Items.Where(y => !y.IsRemainRow && !y.BHangCha && y.SXauNoiMa.Equals(item.SXauNoiMa) && y.IIDCTDuToanNhan.Equals(item.IIDCTDuToanNhan)).Sum(s => s.FBHTNNSD.GetValueOrDefault());

                    item.FBHXHNLD = item.FBHXHNLDTruocDieuChinh.GetValueOrDefault() - itemBHXHNLD;
                    item.FBHXHNSD = item.FBHXHNSDTruocDieuChinh.GetValueOrDefault() - itemBHXHNSD;
                    item.FBHYTNLD = item.FBHYTNLDTruocDieuChinh.GetValueOrDefault() - itemBHYTNLD;
                    item.FBHYTNSD = item.FBHYTNSDTruocDieuChinh.GetValueOrDefault() - itemBHYTNSD;
                    item.FBHTNNLD = item.FBHTNNLDTruocDieuChinh.GetValueOrDefault() - itemBHTNNLD;
                    item.FBHTNNSD = item.FBHTNNSDTruocDieuChinh.GetValueOrDefault() - itemBHTNNSD;
                }
            }
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhDtPhanBoChungTuChiTietModel)obj;
            result = ChungTuChiTietItemsViewFilter(item);
            if (!result && item.BHangCha && !string.IsNullOrEmpty(TypeDisplaysSelected) && TypeDisplaysSelected == TypeDisplay.DA_NHAN_DUTOAN)
            {
                result = _filterXauNoiMaDTTBHXH.Any(x => x.Equals(item.SXauNoiMa));
            }

            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = result && DataSearch.Any(x => x.IIdMlns.Equals(item.IIdMlns));
            }

            if (result)
                item.IsFilter = result;
            return result;
        }

        private void CalculateData()
        {
            Items.Where(x => x.BHangCha && x.IRowType != (int)BaoHiemDuToanTypeEnum.RowType.SO_CHUA_PHAN_BO && x.IRowType != (int)BaoHiemDuToanTypeEnum.RowType.SO_NHAN_PB)
                .ForAll(x =>
                {
                    x.FBHXHNLD = 0;
                    x.FBHXHNSD = 0;
                    x.FBHYTNLD = 0;
                    x.FBHYTNSD = 0;
                    x.FBHTNNLD = 0;
                    x.FBHTNNSD = 0;
                    x.FBHXHNLDTruocDieuChinh = 0;
                    x.FBHXHNSDTruocDieuChinh = 0;
                    x.FBHYTNLDTruocDieuChinh = 0;
                    x.FBHYTNSDTruocDieuChinh = 0;
                    x.FBHTNNLDTruocDieuChinh = 0;
                    x.FBHTNNSDTruocDieuChinh = 0;
                    x.FBHXHNLDSauDieuChinh = 0;
                    x.FBHXHNSDSauDieuChinh = 0;
                    x.FBHYTNLDSauDieuChinh = 0;
                    x.FBHYTNSDSauDieuChinh = 0;
                    x.FBHTNNLDSauDieuChinh = 0;
                    x.FBHTNNSDSauDieuChinh = 0;
                });
            var dictByMlns = Items.GroupBy(x => x.IIdMlns).ToDictionary(x => x.Key, x => x.First());
            var temp = Items.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (var item in temp)
            {

                CalculateParent(item.IIdMlnsCha, item, dictByMlns);
            }

            UpdateTotal();
        }

        private void CalculateParent(Guid? idParent, BhDtPhanBoChungTuChiTietModel item, Dictionary<Guid?, BhDtPhanBoChungTuChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            //Trước điều chỉnh
            model.FBHXHNLDTruocDieuChinh += item.FBHXHNLDTruocDieuChinh.GetValueOrDefault();
            model.FBHXHNSDTruocDieuChinh += item.FBHXHNSDTruocDieuChinh.GetValueOrDefault();
            model.FBHYTNLDTruocDieuChinh += item.FBHYTNLDTruocDieuChinh.GetValueOrDefault();
            model.FBHYTNSDTruocDieuChinh += item.FBHYTNSDTruocDieuChinh.GetValueOrDefault();
            model.FBHTNNLDTruocDieuChinh += item.FBHTNNLDTruocDieuChinh.GetValueOrDefault();
            model.FBHTNNSDTruocDieuChinh += item.FBHTNNSDTruocDieuChinh.GetValueOrDefault();

            model.FBHXHNLD += item.FBHXHNLD.GetValueOrDefault();
            model.FBHXHNSD += item.FBHXHNSD.GetValueOrDefault();
            model.FBHYTNLD += item.FBHYTNLD.GetValueOrDefault();
            model.FBHYTNSD += item.FBHYTNSD.GetValueOrDefault();
            model.FBHTNNLD += item.FBHTNNLD.GetValueOrDefault();
            model.FBHTNNSD += item.FBHTNNSD.GetValueOrDefault();

            //Sau điều chinh
            model.FBHXHNLDSauDieuChinh += model.FBHXHNLD.GetValueOrDefault();
            model.FBHXHNSDSauDieuChinh += model.FBHXHNSD.GetValueOrDefault();
            model.FBHYTNLDSauDieuChinh += model.FBHYTNLD.GetValueOrDefault();
            model.FBHYTNSDSauDieuChinh += model.FBHYTNSD.GetValueOrDefault();
            model.FBHTNNLDSauDieuChinh += model.FBHTNNLD.GetValueOrDefault();
            model.FBHTNNSDSauDieuChinh += model.FBHTNNSD.GetValueOrDefault();


            CalculateParent(model.IIdMlnsCha, item, dictByMlns);
        }

        private void UpdateTotal()
        {
            Model.FThuBHXHNLD = 0;
            Model.FThuBHXHNSD = 0;
            Model.FTongBHXH = 0;
            Model.FThuBHYTNLD = 0;
            Model.FThuBHYTNSD = 0;
            Model.FTongBHYT = 0;
            Model.FThuBHTNNLD = 0;
            Model.FThuBHTNNSD = 0;
            Model.FTongBHTN = 0;
            Model.FTongDuToan = 0;

            var root = Items.Where(x => !x.BHangCha && !x.IsDeleted).ToList();
            if (root.Count > 0)
            {
                Model.FThuBHXHNLD = root.Sum(x => x.FBHXHNLD);
                Model.FThuBHXHNSD = root.Sum(x => x.FBHXHNSD);
                Model.FTongBHXH = root.Sum(x => x.FThuBHXH);

                Model.FThuBHYTNLD = root.Sum(x => x.FBHYTNLD);
                Model.FThuBHYTNSD = root.Sum(x => x.FBHYTNSD);
                Model.FTongBHYT = root.Sum(x => x.FThuBHYT);

                Model.FThuBHTNNLD = root.Sum(x => x.FBHTNNLD);
                Model.FThuBHTNNSD = root.Sum(x => x.FBHTNNSD);
                Model.FTongBHTN = root.Sum(x => x.FThuBHTN);
                Model.FTongDuToan = Model.FTongBHXH + Model.FTongBHYT + Model.FTongBHTN;
            }
        }

        private void LoadAgencies()
        {
            List<DonVi> listNsDonVi = new List<DonVi>();
            listNsDonVi = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Join(StringUtils.COMMA, new string[] { LoaiDonVi.NOI_BO, LoaiDonVi.ROOT })).ToList();

            if (listNsDonVi.Any(x => x.Loai == LoaiDonVi.ROOT))
            {
                var predicate = PredicateBuilder.True<DonVi>();
                predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                predicate = predicate.And(x => x.Loai == SoChungTuType.EstimateDivision.ToString());
                predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);

                listNsDonVi = _nsDonViService.FindByCondition(predicate).ToList();
            }

            // remove 999 hard code
            listNsDonVi = listNsDonVi.Where(x => !x.IIDMaDonVi.Equals("999")).ToList();

            if (Model.ILoaiDuToan.HasValue && !BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDuToan.Value))
            {
                var listIdDonVi = string.IsNullOrEmpty(Model.SDsidMaDonVi) ? new List<string>() : Model.SDsidMaDonVi.Split(",").ToList();
                listNsDonVi = listNsDonVi.Where(x => listIdDonVi.Contains(x.IIDMaDonVi)).ToList();
            }

            Agencies = _mapper.Map<ObservableCollection<ComboboxItem>>(listNsDonVi);
        }

        private void LoadDotNhan()
        {
            List<BhDtPhanBoChungTu> chungTus = _dtChungTuService.FindDotNhanByChungTuPhanBo(Model.Id).ToList();
            List<ComboboxItem> cbxChungTus = new List<ComboboxItem>();
            foreach (var chungTu in chungTus)
            {
                cbxChungTus.Add(new ComboboxItem { ValueItem = chungTu.Id.ToString(), DisplayItem = chungTu.SSoQuyetDinh });
            }
            CbxNhanPhanBos = new ObservableCollection<ComboboxItem>(cbxChungTus);
        }

        private bool BudgetCatalogItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is BhDtPhanBoChungTuChiTietModel item && (item.SLns.StartsWith(_searchLNS, StringComparison.OrdinalIgnoreCase) || item.SMoTa.StartsWith(_searchLNS, StringComparison.OrdinalIgnoreCase));
        }

        private bool ChungTuChiTietItemsViewFilter(BhDtPhanBoChungTuChiTietModel item)
        {
            bool result = true;

            if (!string.IsNullOrEmpty(TypeDisplaysSelected))
            {
                if (TypeDisplaysSelected == TypeDisplay.CO_DU_LIEU)
                    result = result && item.HasAllocationData && item.SXauNoiMa != BhxhMLNS.KHT_BHXH_BHYT_BHTN;
                else if (TypeDisplaysSelected == TypeDisplay.DA_NHAN_DUTOAN)
                    result = result && (item.HasAllocationData || !item.IIDCTDuToanNhan.IsNullOrEmpty()) && item.SXauNoiMa != BhxhMLNS.KHT_BHXH_BHYT_BHTN;
            }

            if (SelectedAgency != null)
            {
                result = result && ((!string.IsNullOrEmpty(item.IIdMaDonVi) && item.IIdMaDonVi.StartsWith(SelectedAgency.ValueItem)));
            }

            item.IsFilter = result;
            return result;
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => ChungTuChiTietItemsViewFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
            if (!string.IsNullOrEmpty(TypeDisplaysSelected) && TypeDisplaysSelected == TypeDisplay.DA_NHAN_DUTOAN)
            {
                var lstXauNoiMa = Items.Where(x => !x.IsHangCha && !x.IIDCTDuToanNhan.IsNullOrEmpty()).Select(s => s.SXauNoiMa).Distinct().ToList();
                if (lstXauNoiMa.IsEmpty())
                    _filterXauNoiMaDTTBHXH = new HashSet<string>();
                else
                {
                    var lstXc = StringUtils.GetListKyHieuParent(lstXauNoiMa);
                    _filterXauNoiMaDTTBHXH = new HashSet<string>(StringUtils.GetListKyHieuParent(lstXauNoiMa));
                }
            }
        }

        protected override void OnLockUnLock()
        {
            string msgConfirm = Model.BKhoa ? Resources.UnlockMultiChungTu : Resources.LockMultiChungTu;
            string msgDone = Model.BKhoa ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            var result = MessageBox.Show(msgConfirm, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _dtChungTuService.LockOrUnLock(Model.Id, !Model.BKhoa);
                Model.BKhoa = !Model.BKhoa;
                OnPropertyChanged(nameof(Model.BKhoa));
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
                MessageBox.Show(msgDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            // refresh dữ liệu ở màn index
            DataChangedEventHandler handler = UpdateVoucherEvent;

            if (handler != null)
            {
                handler(Model, new EventArgs());
            }
        }

        protected override void OnAdd()
        {
            if (_isNamLuyKe || (Model.ILoaiDuToan.HasValue && !BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDuToan.Value)))
            {
                MessageBox.Show("Thêm mới không khả dụng. Hãy thao tác với các bản ghi đang hiển thị trên lưới", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (SelectedItem != null)
            {
                int currentRow = Items.IndexOf(SelectedItem);
                int targetRow = Items.ToList().FindIndex(currentRow, x => x.IsEditable);
                if (targetRow > -1)
                {
                    BhDtPhanBoChungTuChiTietModel sourceItem = Items.ElementAt(targetRow);
                    BhDtPhanBoChungTuChiTietModel targetItem = CloneRow(sourceItem);
                    OnPropertyChanged(nameof(targetItem));
                    targetItem.PropertyChanged += DetailModel_PropertyChanged;
                    Items.Insert(targetRow + 1, targetItem);

                    OnPropertyChanged(nameof(Items));
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IsDeleteAll));
                }
            }
        }

        private BhDtPhanBoChungTuChiTietModel CloneRow(BhDtPhanBoChungTuChiTietModel sourceItem)
        {
            BhDtPhanBoChungTuChiTietModel targetItem = ObjectCopier.Clone(sourceItem);

            targetItem.Id = Guid.Empty;
            targetItem.IIdDtchungTu = null;
            targetItem.FBHXHNLD = 0;
            targetItem.FBHXHNSD = 0;
            targetItem.FThuBHXH = 0;
            targetItem.FBHYTNLD = 0;
            targetItem.FBHYTNSD = 0;
            targetItem.FThuBHYT = 0;
            targetItem.FBHTNNLD = 0;
            targetItem.FBHTNNSD = 0;
            targetItem.FThuBHTN = 0;
            targetItem.SGhiChu = string.Empty;
            targetItem.IsModified = true;
            targetItem.IIdMaDonVi = string.Empty;
            targetItem.STenDonVi = string.Empty;
            targetItem.SDotPhanBoTruoc = string.Empty;
            targetItem.IIDCTDuToanNhan = sourceItem.IIDCTDuToanNhan;
            targetItem.BEmpty = true;
            targetItem.CbxDonVi = Agencies;
            targetItem.CbxNhanPhanBos = CbxNhanPhanBos;
            return targetItem;
        }

        protected override void OnRefresh()
        {
            if (IsSaveData)
            {
                var result = MessageBox.Show(Resources.ConfirmReloadData, Resources.ConfirmTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
                if (result == MessageBoxResult.Cancel)
                    return;
                else if (result == MessageBoxResult.Yes)
                    OnSaveData();
            }
            LoadData();
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(BhDtPhanBoChungTuChiTietModel.FBHXHNLD)
                || args.PropertyName == nameof(BhDtPhanBoChungTuChiTietModel.FBHXHNSD)
                || args.PropertyName == nameof(BhDtPhanBoChungTuChiTietModel.FBHYTNLD)
                || args.PropertyName == nameof(BhDtPhanBoChungTuChiTietModel.FBHYTNSD)
                || args.PropertyName == nameof(BhDtPhanBoChungTuChiTietModel.FBHTNNLD)
                || args.PropertyName == nameof(BhDtPhanBoChungTuChiTietModel.FBHTNNSD)
                || args.PropertyName == nameof(BhDtPhanBoChungTuChiTietModel.SGhiChu)
                || args.PropertyName == nameof(BhDtPhanBoChungTuChiTietModel.IIDCTDuToanNhan))
            {
                BhDtPhanBoChungTuChiTietModel item = (BhDtPhanBoChungTuChiTietModel)sender;
                item.FBHXHNLDSauDieuChinh = (item.FBHXHNLDTruocDieuChinh ?? 0) + (item.FBHXHNLD ?? 0);
                item.FBHXHNSDSauDieuChinh = (item.FBHXHNSDTruocDieuChinh ?? 0) + (item.FBHXHNSD ?? 0);
                item.FBHYTNLDSauDieuChinh = (item.FBHYTNLDTruocDieuChinh ?? 0) + (item.FBHYTNLD ?? 0);
                item.FBHYTNSDSauDieuChinh = (item.FBHYTNSDTruocDieuChinh ?? 0) + (item.FBHYTNSD ?? 0);
                item.FBHTNNLDSauDieuChinh = (item.FBHTNNLDTruocDieuChinh ?? 0) + (item.FBHTNNLD ?? 0);
                item.FBHTNNSDSauDieuChinh = (item.FBHTNNSDTruocDieuChinh ?? 0) + (item.FBHTNNSD ?? 0);
                CalculateData();
                if (_isAdjusted)
                {
                    UpdateSoChuaPhanBo();
                }

                item.IsModified = true;
                IsSaveData = true;
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void UpdateSoChuaPhanBo()
        {
            var bhPbdttChiTiet = (BhDtPhanBoChungTuChiTietModel)SelectedItem;
            if(bhPbdttChiTiet != null)
            {
                DttBHXHChiTietCriteria searchCondition = new DttBHXHChiTietCriteria();

                var lstCtChiTietNhanPb = _dttBHXHChiTietService.FindByIdDTT(bhPbdttChiTiet.IIDCTDuToanNhan).Where(x => x.IIdMlns == bhPbdttChiTiet.IIdMlns).ToList();

                var fBHXHNLDNhanPhanBo = lstCtChiTietNhanPb?.Select(x => x.FThuBHXHNguoiLaoDong).Sum();
                var fBHXHNSDNhanPhanBo = lstCtChiTietNhanPb?.Select(x => x.FThuBHXHNguoiSuDungLaoDong).Sum();
                var fBHYTNLDNhanPhanBo = lstCtChiTietNhanPb?.Select(x => x.FThuBHYTNguoiLaoDong).Sum();
                var fBHYTNSDNhanPhanBo = lstCtChiTietNhanPb?.Select(x => x.FThuBHYTNguoiSuDungLaoDong).Sum();
                var fBHTNNLDNhanPhanBo = lstCtChiTietNhanPb?.Select(x => x.FThuBHTNNguoiLaoDong).Sum();
                var fBHTNNSDNhanPhanBo = lstCtChiTietNhanPb?.Select(x => x.FThuBHTNNguoiSuDungLaoDong).Sum();

                var predicate = PredicateBuilder.True<BhDtPhanBoChungTuChiTiet>();
                predicate = predicate.And(x => x.IIdCtduToanNhan == bhPbdttChiTiet.IIDCTDuToanNhan);
                predicate = predicate.And(x => x.IIdMlns == bhPbdttChiTiet.IIdMlns);
                predicate = predicate.And(x => x.IIdDttBHXH != Model.Id);
                var lstCtChiTietDaPhanBo = _dttChungTuChiTietService.FindByCondition(predicate);

                var fThuBHXHNLDDaPhanBo = lstCtChiTietDaPhanBo?.Select(x => x.FBHXHNLD).Sum();
                var fThuBHXHNSDDaPhanBo = lstCtChiTietDaPhanBo?.Select(x => x.FBHXHNSD).Sum();
                var fThuBHYTNLDDaPhanBo = lstCtChiTietDaPhanBo?.Select(x => x.FBHYTNLD).Sum();
                var fThuBHYTNSDDaPhanBo = lstCtChiTietDaPhanBo?.Select(x => x.FBHYTNSD).Sum();
                var fThuBHTNNLDDaPhanBo = lstCtChiTietDaPhanBo?.Select(x => x.FBHTNNLD).Sum();
                var fThuBHTNNSDDaPhanBo = lstCtChiTietDaPhanBo?.Select(x => x.FBHTNNSD).Sum();


                var fThuBHXHNLDPhanBo = Items.Where(x => x.IIdMlns == SelectedItem.IIdMlns && x.IIDCTDuToanNhan == SelectedItem.IIDCTDuToanNhan && !x.BHangCha && !x.IsDeleted).Select(x => x.FBHXHNLD).Sum();
                var fThuBHXHNSDPhanBo = Items.Where(x => x.IIdMlns == SelectedItem.IIdMlns && x.IIDCTDuToanNhan == SelectedItem.IIDCTDuToanNhan && !x.BHangCha && !x.IsDeleted).Select(x => x.FBHXHNSD).Sum();
                var fThuBHYTNLDPhanBo = Items.Where(x => x.IIdMlns == SelectedItem.IIdMlns && x.IIDCTDuToanNhan == SelectedItem.IIDCTDuToanNhan && !x.BHangCha && !x.IsDeleted).Select(x => x.FBHYTNLD).Sum();
                var fThuBHYTNSDPhanBo = Items.Where(x => x.IIdMlns == SelectedItem.IIdMlns && x.IIDCTDuToanNhan == SelectedItem.IIDCTDuToanNhan && !x.BHangCha && !x.IsDeleted).Select(x => x.FBHYTNSD).Sum();
                var fThuBHTNNLDPhanBo = Items.Where(x => x.IIdMlns == SelectedItem.IIdMlns && x.IIDCTDuToanNhan == SelectedItem.IIDCTDuToanNhan && !x.BHangCha && !x.IsDeleted).Select(x => x.FBHTNNLD).Sum();
                var fThuBHTNNSDPhanBo = Items.Where(x => x.IIdMlns == SelectedItem.IIdMlns && x.IIDCTDuToanNhan == SelectedItem.IIDCTDuToanNhan && !x.BHangCha && !x.IsDeleted).Select(x => x.FBHTNNSD).Sum();

                Items.Where(x => x.IRowType == (int)BaoHiemDuToanTypeEnum.RowType.SO_CHUA_PHAN_BO && x.SMoTa == "Số chưa phân bổ" && x.IIdMlns == SelectedItem.IIdMlns && x.IIDCTDuToanNhan == SelectedItem.IIDCTDuToanNhan)
                    .Select(x =>
                    {
                        x.FBHXHNLD = (fBHXHNLDNhanPhanBo ?? 0) - (fThuBHXHNLDDaPhanBo ?? 0) - (fThuBHXHNLDPhanBo ?? 0);
                        x.FBHXHNSD = (fBHXHNSDNhanPhanBo ?? 0) - (fThuBHXHNSDDaPhanBo ?? 0) - (fThuBHXHNSDPhanBo ?? 0);
                        x.FBHYTNLD = (fBHYTNLDNhanPhanBo ?? 0) - (fThuBHYTNLDDaPhanBo ?? 0) - (fThuBHYTNLDPhanBo ?? 0);
                        x.FBHYTNSD = (fBHYTNSDNhanPhanBo ?? 0) - (fThuBHYTNSDDaPhanBo ?? 0) - (fThuBHYTNSDPhanBo ?? 0);
                        x.FBHTNNLD = (fBHTNNLDNhanPhanBo ?? 0) - (fThuBHTNNLDDaPhanBo ?? 0) - (fThuBHTNNLDPhanBo ?? 0);
                        x.FBHTNNSD = (fBHTNNSDNhanPhanBo ?? 0) - (fThuBHTNNSDDaPhanBo ?? 0) - (fThuBHTNNSDPhanBo ?? 0);
                        return x;
                    }).ToList();
            }
        }

        private void OnSaveData()
        {
            var listChungTuChiTietAdd = Items.Where(x => !x.BHangCha && x.IsModified && x.Id == Guid.Empty).ToList();
            var listChungTuChiTietUpdate = Items.Where(x => !x.BHangCha && x.IsModified && x.Id != Guid.Empty).ToList();
            var listChungTuChiTietDelete = Items.Where(x => !x.BHangCha && x.IsDeleted && x.Id != Guid.Empty).ToList();

            if (IsAdjusted)
            {
                var listSDsidMaDonVi = Items.Where(x => x.HasData && !string.IsNullOrEmpty(x.IIdMaDonVi)).Select(n => n.IIdMaDonVi).Distinct();
                Model.SDsidMaDonVi = string.Join(",", listSDsidMaDonVi);
                BhDtPhanBoChungTu entity = _dtChungTuService.FindById(Model.Id);
                _mapper.Map(Model, entity);
                entity.DNgaySua = DateTime.Now;
                entity.SNguoiSua = _sessionService.Current.Principal;
                _dtChungTuService.Update(entity);
            }

            // Thêm mới chứng từ chi tiết
            var addItemList = new List<BhDtPhanBoChungTuChiTiet>();
            if (listChungTuChiTietAdd.Count > 0)
            {
                var duplicateDvSqd = Items.Where(x => x.IRowType == (int)DuToanRowType.RowChiTiet && listChungTuChiTietAdd.Select(y => y.IIdMlns).Contains(x.IIdMlns))
                                            .GroupBy(x => new { x.IIdMaDonVi, x.IIDCTDuToanNhan, x.IIdMlns })
                                            .Where(x => x.Count() > 1)
                                            .Select(x => x.Key).ToList();
                if (duplicateDvSqd.Count > 0)
                {
                    MessageBox.Show(Resources.AlertDuplicateDonViAndSoQuyetDinh, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var addItemsQuery = new List<BhDtPhanBoChungTuChiTietQuery>();
                _mapper.Map(addItemsQuery, addItemList);
                addItemList = listChungTuChiTietAdd.Select(x => new BhDtPhanBoChungTuChiTiet
                {
                    Id = Guid.NewGuid(),
                    DNgayTao = DateTime.Now,
                    DNgaySua = DateTime.Now,
                    SNguoiTao = _sessionInfo.Principal,
                    IIdCtduToanNhan = x.IIDCTDuToanNhan,
                    IIdDttBHXH = Model.Id,
                    IIdMaDonVi = x.IIdMaDonVi,
                    IIdMlns = x.IIdMlns ?? x.IIdMlns.Value,
                    SXauNoiMa = x.SXauNoiMa,
                    SLns = x.SLns,
                    SM = x.SM,
                    SNg = x.SNg,
                    SMoTa = x.SMoTa,
                    STm = x.STm,
                    STtm = x.STtm,
                    INamLamViec = _sessionService.Current.YearOfWork,
                    FBHXHNLD = x.FBHXHNLD,
                    FBHXHNSD = x.FBHXHNSD,
                    FThuBHXH = (x.FBHXHNLD ?? 0) + (x.FBHXHNSD ?? 0),
                    FBHYTNLD = x.FBHYTNLD,
                    FBHYTNSD = x.FBHYTNSD,
                    FThuBHYT = (x.FBHYTNLD ?? 0) + (x.FBHYTNSD ?? 0),
                    FBHTNNLD = x.FBHTNNLD,
                    FBHTNNSD = x.FBHTNNSD,
                    FThuBHTN = (x.FBHTNNLD ?? 0) + (x.FBHTNNSD ?? 0),
                    FTongCong = (x.FThuBHXH ?? 0) + (x.FThuBHYT ?? 0) + (x.FThuBHTN ?? 0)
                }).ToList();
                _dttChungTuChiTietService.AddRange(addItemList);
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            // Cập nhật chứng từ chi tiết
            if (listChungTuChiTietUpdate.Count > 0)
            {
                var updateItemsQuery = new List<BhDtPhanBoChungTuChiTietQuery>();
                _mapper.Map(updateItemsQuery, addItemList);
                addItemList = listChungTuChiTietUpdate.Select(x => new BhDtPhanBoChungTuChiTiet
                {
                    Id = x.Id,
                    DNgayTao = DateTime.Now,
                    DNgaySua = DateTime.Now,
                    SNguoiTao = _sessionInfo.Principal,
                    IIdCtduToanNhan = x.IIDCTDuToanNhan,
                    IIdDttBHXH = Model.Id,
                    IIdMaDonVi = x.IIdMaDonVi,
                    IIdMlns = x.IIdMlns ?? x.IIdMlns.Value,
                    SLns = x.SLns,
                    SM = x.SM,
                    SNg = x.SNg,
                    SMoTa = x.SMoTa,
                    STm = x.STm,
                    STtm = x.STtm,
                    SXauNoiMa = x.SXauNoiMa,
                    INamLamViec = _sessionService.Current.YearOfWork,
                    FBHXHNLD = x.FBHXHNLD,
                    FBHXHNSD = x.FBHXHNSD,
                    FThuBHXH = x.FThuBHXH,
                    FBHYTNLD = x.FBHYTNLD,
                    FBHYTNSD = x.FBHYTNSD,
                    FThuBHYT = x.FThuBHYT,
                    FBHTNNLD = x.FBHTNNLD,
                    FBHTNNSD = x.FBHTNNSD,
                    FThuBHTN = x.FThuBHTN,
                    FTongCong = (x.FThuBHXH ?? 0) + (x.FThuBHYT ?? 0) + (x.FThuBHTN ?? 0)
                }).ToList();

                foreach (var item in addItemList)
                {
                    _dttChungTuChiTietService.Update(item);
                }
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }


            Items.Select(x => { x.IsModified = false; x.IsDeleted = false; return x; }).ToList();

            //Cập nhật thông tin chứng từ
            UpdateChungTu();
            OnRefresh();
            MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);

            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));

            //refresh dữ liệu ở màn index
            DataChangedEventHandler handler = UpdateVoucherEvent;

            if (handler != null)
            {
                handler(Model, new EventArgs());
            }
        }


        protected override bool CanDelete(object obj)
        {
            return !Model.BKhoa;
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.BHangCha)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                CalculateData();
                if (!_isAdjusted)
                {
                    UpdateSoChuaPhanBo();
                }
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void CountParentChild(List<BhDtPhanBoChungTuChiTietModel> listChild, BhDtPhanBoChungTuChiTietModel parent)
        {
            if (parent == null) return;
            parent.FBHXHNLD = listChild.Sum(n => n.FBHXHNLD);
            parent.FBHXHNSD = listChild.Sum(n => n.FBHXHNSD);
            parent.FThuBHXH = listChild.Sum(n => n.FThuBHXH);
            parent.FBHYTNLD = listChild.Sum(n => n.FBHYTNLD);
            parent.FBHYTNSD = listChild.Sum(n => n.FBHYTNSD);
            parent.FThuBHYT = listChild.Sum(n => n.FThuBHYT);
            parent.FBHTNNLD = listChild.Sum(n => n.FBHTNNLD);
            parent.FBHTNNSD = listChild.Sum(n => n.FBHTNNSD);
            parent.FThuBHTN = listChild.Sum(n => n.FThuBHTN);
            if (_isAdjusted)
            {
                //TODO điều chỉnh
            }
        }

        private void CalculateReverse(List<BhDtPhanBoChungTuChiTietModel> items)
        {
            var temps = items.GroupBy(n => n.IIdMlnsCha).ToList();
            var listParent = new List<BhDtPhanBoChungTuChiTietModel>();
            foreach (var temp in temps)
            {
                //var listChild = temp.ToList();
                var listChild = Items.Where(x => x.IIdMlnsCha == temp.First().IIdMlnsCha && x.IRowType != (int)BaoHiemDuToanTypeEnum.RowType.SO_CHUA_PHAN_BO).ToList();
                if (listChild.Count > 0)
                {
                    var parent = Items.FirstOrDefault(x => x.IIdMlns == listChild.First().IIdMlnsCha);
                    if (parent != null)
                    {
                        CountParentChild(listChild, parent);
                        listParent.Add(parent);
                    }
                }
            }
            if (listParent.Count > 0)
            {
                CalculateReverse(listParent);
            }
        }

        public void OnPhanBoConLai()
        {
            FirstTimePhanBo = true;
            foreach (var conLai in Items)
            {
                if (conLai.IRowType == (int)BaoHiemDuToanTypeEnum.RowType.SO_CHUA_PHAN_BO && conLai.SMoTa == "Số chưa phân bổ" && (conLai.FBHXHNLD != 0 || conLai.FBHXHNSD != 0
                    || conLai.FBHYTNLD != 0 || conLai.FBHYTNSD != 0 || conLai.FBHTNNLD != 0 || conLai.FBHTNNSD != 0))
                {
                    var dv =
                        Items.FirstOrDefault(x => x.IsFilter && x.IIdMlnsCha == conLai.IIdMlnsCha && x.IIdMlns == conLai.IIdMlns && x.IIdMaDonVi.Equals(_selectedAgency.ValueItem) && conLai.IIDCTDuToanNhan.Equals(x.IIDCTDuToanNhan));
                    if (dv != null)
                    {
                        dv.FBHXHNLD += conLai.FBHXHNLD;
                        dv.FBHXHNSD += conLai.FBHXHNSD;
                        dv.FBHYTNLD += conLai.FBHYTNLD;
                        dv.FBHYTNSD += conLai.FBHYTNSD;
                        dv.FBHTNNLD += conLai.FBHTNNLD;
                        dv.FBHTNNSD += conLai.FBHTNNSD;
                        dv.IsModified = true;
                    }

                    conLai.FBHXHNLD = 0;
                    conLai.FBHXHNSD = 0;
                    conLai.FBHYTNLD = 0;
                    conLai.FBHYTNSD = 0;
                    conLai.FBHTNNLD = 0;
                    conLai.FBHTNNSD = 0;
                }
            }
            CalculateTotalParent();
            FirstTimePhanBo = false;
        }

        private void CalculateTotalParent()
        {
            // Reset value parent
            Items.Where(x => x.IsHangCha && x.IsFilter && x.IRowType == (int)BaoHiemDuToanTypeEnum.RowType.HANG_CHA).ForAll(x => ResetItemValue(x));

            var temps = Items.Where(x => x.IsEditable && x.IsFilter && HasInputData(x) && x.IRowType == (int)BaoHiemDuToanTypeEnum.RowType.HANG_CON).GroupBy(n => n.IIdMlnsCha).ToList();

            var listParent = new List<BhDtPhanBoChungTuChiTietModel>();
            foreach (var temp in temps)
            {
                var listChild = temp.ToList();
                var parent = Items.FirstOrDefault(x => x.IIdMlns == listChild.First().IIdMlnsCha);
                if (parent != null)
                {
                    CountParentChild(listChild, parent);
                    listParent.Add(parent);
                }
            }
            if (listParent.Count > 0)
            {
                CalculateReverse(listParent);
            }

            if (!_sessionService.Current.IdsDonViQuanLy.Split(StringUtils.COMMA).Any(x => _nsDonViService.FindByIdDonVi(x, _sessionService.Current.YearOfWork)?.Loai.Equals(LoaiDonVi.ROOT) ?? false))
            {
                var temps2 = Items.Where(x => x.IRowType == (int)DuToanRowType.RowChiTiet || x.IRowType == (int)DuToanRowType.RowNhanPhanBoOrTong).GroupBy(n => new { n.IIdMlnsCha, n.SXauNoiMa }).ToList();

                foreach (var temp in temps2)
                {
                    var listChild = temp.ToList();
                    CountParentChild(listChild.Skip(1).ToList(), listChild.First());
                }
            }
        }

        private BhDtPhanBoChungTuChiTietModel ResetItemValue(BhDtPhanBoChungTuChiTietModel item)
        {
            item.FBHXHNLD = 0;
            item.FBHXHNSD = 0;
            item.FThuBHXH = 0;

            item.FBHYTNLD = 0;
            item.FBHYTNSD = 0;
            item.FThuBHYT = 0;

            item.FBHTNNLD = 0;
            item.FBHTNNSD = 0;
            item.FThuBHTN = 0;

            item.SGhiChu = string.Empty;
            return item;
        }

        /// <summary>
        /// open screen print
        /// </summary>
        /// <param name="param"></param>
        private void OpenPrintDialog(object param)
        {
            var divisionPrintType = (SocialInsuranceDivisionEstimatePrintType)((int)param);
            object view = null;
            switch (divisionPrintType)
            {
                case SocialInsuranceDivisionEstimatePrintType.COVER_SHEET:
                case SocialInsuranceDivisionEstimatePrintType.DU_TOAN_THU_CHI_TONG_HOP:
                    TongHopThuChiViewModel.ReportNameTypeValue = (int)divisionPrintType;
                    TongHopThuChiViewModel.ReportTypeValue = divisionPrintType;
                    TongHopThuChiViewModel.Init();
                    view = new TongHopThuChi
                    {
                        DataContext = TongHopThuChiViewModel
                    };
                    break;
                case SocialInsuranceDivisionEstimatePrintType.TARGET_AGENCY:
                    SocialInsuranceDivisionEstimatePrintSheetViewModel.Models = new ObservableCollection<DtChungTuModel>();
                    SocialInsuranceDivisionEstimatePrintSheetViewModel.Init();
                    view = new PhanBoDuToanThuPrintReport
                    {
                        DataContext = SocialInsuranceDivisionEstimatePrintSheetViewModel
                    };
                    break;
                case SocialInsuranceDivisionEstimatePrintType.APPENDIX:
                    PrintPhuLucDuToanThuViewModel.Init();
                    view = new PrintPhuLucDuToanThu
                    {
                        DataContext = PrintPhuLucDuToanThuViewModel
                    };
                    break;
                case SocialInsuranceDivisionEstimatePrintType.DELIVER:
                    PrintPhuLucGiaoDuToanDuToanThuViewModel.Init();
                    view = new PrintPhuLucGiaoDuToanDuToanThu
                    {
                        DataContext = PrintPhuLucGiaoDuToanDuToanThuViewModel
                    };
                    break;
                default:
                    view = null;
                    break;
            }
            DialogHost.Show(view, "PhanBoDuToanThuDetaillDialog", null, null);
        }

        private bool HasInputData(BhDtPhanBoChungTuChiTietModel item)
        {
            return item.HasData;
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            var result = MessageBox.Show(Resources.DeleteAllChungTuChiTiet, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                if (Items != null)
                {
                    Items.Where(x => x.IsFilter && !x.IsHangCha && !x.IsPhanBo && !x.IsConLai).ForAll(x => x.IsDeleted = true);
                    OnSaveData();
                }
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void UpdateChungTu()
        {
            BhDtPhanBoChungTu chungTu = _dtChungTuService.FindById(Model.Id);
            var childs = Items.Where(x => x.HasData).ToList();

            chungTu.FThuBHXHNLD = childs.Sum(x => x.FBHXHNLD);
            chungTu.FThuBHXHNSD = childs.Sum(x => x.FBHXHNSD);
            chungTu.FTongBHXH = childs.Sum(x => x.FThuBHXH);
            chungTu.FThuBHYTNLD = childs.Sum(x => x.FBHYTNLD);
            chungTu.FThuBHYTNSD = childs.Sum(x => x.FBHYTNSD);
            chungTu.FTongBHYT = childs.Sum(x => x.FThuBHYT);
            chungTu.FThuBHTNNLD = childs.Sum(x => x.FBHTNNLD);
            chungTu.FThuBHTNNSD = childs.Sum(x => x.FBHTNNSD);
            chungTu.FTongBHTN = childs.Sum(x => x.FThuBHTN);
            chungTu.FTongDuToan = childs.Sum(x => x.FTongCong);

            _dtChungTuService.Update(chungTu);

            Model.FThuBHXHNLD = chungTu.FThuBHXHNLD;
            Model.FThuBHXHNSD = chungTu.FThuBHXHNSD;
            Model.FTongBHXH = chungTu.FTongBHXH;
            Model.FThuBHYTNLD = chungTu.FThuBHYTNLD;
            Model.FThuBHYTNSD = chungTu.FThuBHYTNSD;
            Model.FTongBHYT = chungTu.FTongBHYT;
            Model.FThuBHTNNLD = chungTu.FThuBHTNNLD;
            Model.FThuBHTNNSD = chungTu.FThuBHTNNSD;
            Model.FTongBHTN = chungTu.FTongBHTN;
            Model.FTongDuToan = chungTu.FTongDuToan;
        }

        //Kiểm tra chứng từ phân bổ có phải là cuối cùng không
        private void CheckLastDivisionEstimateVoucher()
        {
            var predicate = PredicateBuilder.True<BhDtPhanBoChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.Id != Model.Id);

            var listChungTu = _dtChungTuService.FindByCondition(predicate).ToList();
            listChungTu = listChungTu.Where(x => x.IIdDotNhan.Split(",").Intersect(Model.IIdDotNhan.Split(",").ToList()).Any()).ToList();

            if (listChungTu.Count > 0)
            {
                var maxDate = listChungTu.Select(x => { return x.DNgayQuyetDinh.HasValue ? x.DNgayQuyetDinh.Value.Date : x.DNgayChungTu.Value.Date; }).Max(x => x);
                var modelDate = Model.DNgayQuyetDinh.HasValue ? Model.DNgayQuyetDinh.Value.Date : Model.DNgayChungTu.Value.Date;
                if (listChungTu.Any() && modelDate < maxDate)
                    MessageBoxHelper.Info(string.Format(Resources.AlertUpdateDivisionEstimate, Model.SSoChungTu));
                else
                {
                    List<BhDttNhanPhanBoMap> dtNhanPhanBoMaps = _dtChungTuMapService.FindByIdNhanDuToan(Model.Id).ToList();
                    if (dtNhanPhanBoMaps.Count() > 0)
                        MessageBoxHelper.Info(Resources.AlertDivisionEstimateAdjusted);
                }
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }


        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhDtPhanBoChungTuChiTietModel> results = new List<BhDtPhanBoChungTuChiTietModel>();

                List<string> lstSXaNoiMaChildSearch = DataPopupSearchItems.Where(x => x.SMoTa.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                List<string> lstSXaNoiMaParentSearch = DataPopupSearchItems.Where(x => x.SMoTa.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                if (!lstSXaNoiMaChildSearch.IsEmpty())
                {
                    lstParents.AddRange(StringUtils.GetListKyHieuParent(lstSXaNoiMaChildSearch));
                    if (lstParents.Any(x => x.Count() >= 3))
                    {
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
                    }
                    results = DataPopupSearchItems.Where(x => lstParents.Contains(x.SXauNoiMa)).ToList();
                }
                if (!lstSXaNoiMaParentSearch.IsEmpty())
                {
                    if (results.IsEmpty())
                        results = GetDataParent(lstSXaNoiMaParentSearch);
                    else
                        results.AddRange(GetDataParent(lstSXaNoiMaParentSearch.Where(x => !lstParents.Contains(x)).ToList()));
                }
                DataSearch = new ObservableCollection<BhDtPhanBoChungTuChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhDtPhanBoChungTuChiTietModel>();
            }
            ItemsView.Refresh();
        }

        private List<BhDtPhanBoChungTuChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhDtPhanBoChungTuChiTietModel> result = new List<BhDtPhanBoChungTuChiTietModel>();
            List<string> lstParent = StringUtils.GetListKyHieuParent(lstInput);
            if (!lstParent.IsEmpty() && lstParent.Any(x => x.Count() >= 3))
            {
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
            }
            var lstData = DataPopupSearchItems.Where(x => lstParent.Contains(x.SXauNoiMa)).ToList();
            result.AddRange(lstData);
            GetListChild(lstData.Where(x => lstInput.Contains(x.SXauNoiMa)).ToList(), result);
            return result;
        }

        private void GetListChild(List<BhDtPhanBoChungTuChiTietModel> lstInput, List<BhDtPhanBoChungTuChiTietModel> results)
        {
            var itemChild = DataPopupSearchItems.Where(x => lstInput.Select(x => x.IIdMlns).Distinct().Contains(x.IIdMlnsCha ?? Guid.Empty)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => DataPopupSearchItems.Select(y => y.IIdMlnsCha).Distinct().Contains(x.IIdMlns)))
                {
                    GetListChild(new List<BhDtPhanBoChungTuChiTietModel>() { item }, results);
                }
            }
        }


        private void GetPlanData()
        {
            var vouchers = _khtService.FindAggregateVoucher(NamLamViec);
            if (vouchers != null )
            {
                var planData = _khtChiTietService.GetPlanData(NamLamViec, vouchers.STongHop);
                if (planData != null)
                {
                    var itemFilter = Items.Where(x => !x.IsHangCha && x.IRowType == (int)BaoHiemDuToanTypeEnum.RowType.HANG_CON && planData.Select(x => x.IIdMaDonVi).Contains(x.IIdMaDonVi));
                    Parallel.ForEach(itemFilter, itemPB =>
                    {
                        itemPB.FBHXHNLD = planData.Where(x => x.XauNoiMa == itemPB.SXauNoiMa && x.IIdMaDonVi == itemPB.IIdMaDonVi)
                            .Select(x => x.FThuBHXHNguoiLaoDong.GetValueOrDefault()).FirstOrDefault();
                        itemPB.FBHXHNSD = planData.Where(x => x.XauNoiMa == itemPB.SXauNoiMa && x.IIdMaDonVi == itemPB.IIdMaDonVi)
                            .Select(x => x.FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault()).FirstOrDefault();
                        itemPB.FBHYTNLD = planData.Where(x => x.XauNoiMa == itemPB.SXauNoiMa && x.IIdMaDonVi == itemPB.IIdMaDonVi)
                            .Select(x => x.FThuBHYTNguoiLaoDong.GetValueOrDefault()).FirstOrDefault();
                        itemPB.FBHYTNSD = planData.Where(x => x.XauNoiMa == itemPB.SXauNoiMa && x.IIdMaDonVi == itemPB.IIdMaDonVi)
                            .Select(x => x.FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault()).FirstOrDefault();
                        itemPB.FBHTNNLD = planData.Where(x => x.XauNoiMa == itemPB.SXauNoiMa && x.IIdMaDonVi == itemPB.IIdMaDonVi)
                            .Select(x => x.FThuBHTNNguoiLaoDong.GetValueOrDefault()).FirstOrDefault();
                        itemPB.FBHTNNSD = planData.Where(x => x.XauNoiMa == itemPB.SXauNoiMa && x.IIdMaDonVi == itemPB.IIdMaDonVi)
                            .Select(x => x.FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault()).FirstOrDefault();

                        if (itemPB.IsEmptyPlanData)
                            itemPB.IsModified = false;
                    });
                }
                else
                {
                    MessageBoxHelper.Info(Resources.MsgPlanData);
                }
            }
        }

        private void GetAggregateAdjustData()
        {
            var donVi = Model.SDsidMaDonVi;
            var adjustData = _dcDttService.GetUnitAggregateAdjustData(NamLamViec, donVi).ToList();
            if (adjustData != null)
            {
                var itemFilter = Items.Where(x => !x.IsHangCha);
                Parallel.ForEach(itemFilter, item =>
                {
                    item.FBHXHNLD = adjustData.Where(x => x.SXauNoiMa == item.SXauNoiMa && x.IIdMaDonVi == item.IIdMaDonVi).Select(x => x.FThuBHXHNLDTang.GetValueOrDefault() > 0 ? x.FThuBHXHNLDTang.GetValueOrDefault() : x.FThuBHXHNLDGiam.GetValueOrDefault()).FirstOrDefault();
                    item.FBHXHNSD = adjustData.Where(x => x.SXauNoiMa == item.SXauNoiMa && x.IIdMaDonVi == item.IIdMaDonVi).Select(x => x.FThuBHXHNSDTang.GetValueOrDefault() > 0 ? x.FThuBHXHNSDTang.GetValueOrDefault() : x.FThuBHXHNSDGiam.GetValueOrDefault()).FirstOrDefault();
                    item.FBHYTNLD = adjustData.Where(x => x.SXauNoiMa == item.SXauNoiMa && x.IIdMaDonVi == item.IIdMaDonVi).Select(x => x.FThuBHYTNLDTang.GetValueOrDefault() > 0 ? x.FThuBHYTNLDTang.GetValueOrDefault() : x.FThuBHYTNLDGiam.GetValueOrDefault()).FirstOrDefault();
                    item.FBHYTNSD = adjustData.Where(x => x.SXauNoiMa == item.SXauNoiMa && x.IIdMaDonVi == item.IIdMaDonVi).Select(x => x.FThuBHYTNSDTang.GetValueOrDefault() > 0 ? x.FThuBHYTNSDTang.GetValueOrDefault() : x.FThuBHYTNSDGiam.GetValueOrDefault()).FirstOrDefault();
                    item.FBHTNNLD = adjustData.Where(x => x.SXauNoiMa == item.SXauNoiMa && x.IIdMaDonVi == item.IIdMaDonVi).Select(x => x.FThuBHTNNLDTang.GetValueOrDefault() > 0 ? x.FThuBHTNNLDTang.GetValueOrDefault() : x.FThuBHTNNLDGiam.GetValueOrDefault()).FirstOrDefault();
                    item.FBHTNNSD = adjustData.Where(x => x.SXauNoiMa == item.SXauNoiMa && x.IIdMaDonVi == item.IIdMaDonVi).Select(x => x.FThuBHTNNSDTang.GetValueOrDefault() > 0 ? x.FThuBHTNNSDTang.GetValueOrDefault() : x.FThuBHTNNSDGiam.GetValueOrDefault()).FirstOrDefault();
                });
            }
        }
    }
}
