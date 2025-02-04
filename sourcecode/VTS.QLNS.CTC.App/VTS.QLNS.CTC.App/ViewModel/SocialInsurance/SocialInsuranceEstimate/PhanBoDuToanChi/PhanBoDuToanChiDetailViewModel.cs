using AutoMapper;
using log4net;
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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi
{
    public class PhanBoDuToanChiDetailViewModel : DetailViewModelBase<BhPbdtcBHXHModel, BhPbdtcBHXHChiTietModel>
    {
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IPbdtcBHXHService _pbdtcBHXHService;
        private readonly IPbdtcBHXHChiTietService _pbdtcBHXHChiTietService;
        private readonly IPbdtcMapBHXHService _pbdtcMapBHXHService;
        private readonly INdtctgBHXHService _ndtctgBHXHService;
        private readonly INdtctgBHXHChiTietService _ndtctgBHXHChiTietService;
        private readonly IBhKhcCheDoBhXhService _bhKeHoachChiService;
        private readonly IBhKhcCheDoBhXhChiTietService _bhKhcCheDoBhXhChiTietService;
        private readonly IBhKhcKService _bhKhcKService;
        private readonly IBhKhcKcbService _bhKhcKcbService;
        private readonly IBhKhcKinhphiQuanlyService _bhKhcKinhphiQuanlyService;
        private readonly IBhDtcDcdToanChiChiTietService _bhDtcDcdToanChiChiTietService;
        private readonly IBhDtcDcdToanChiService _bhDtcDcdToanChiService;
        private readonly IBhDtCtctKPQLService _bhDtCtctKPQLService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _budgetCatalogItemsView;
        private ICollectionView ItemsView;
        private readonly INsDonViService _nsDonViService;
        private readonly IDanhMucService _danhMucService;
        private readonly ILog _logger;
        private DanhMucNganhService _danhMucNganhService;
        private EstimationVoucherDetailCriteria _searchCondition;
        private bool _isNamLuyKe;
        private bool _isShowQuyetDinh;
        private List<BhDmMucLucNganSach> _listMLNS;
        private SessionInfo _sessionInfo;
        private List<DanhMuc> _listDanhMucNganh;
        private ICollection<BhPbdtcBHXHChiTietModel> _filterResult = new HashSet<BhPbdtcBHXHChiTietModel>();
        private List<Tuple<string, Guid?>> _filterResultWithSQD = new List<Tuple<string, Guid?>>();
        private string xnmConcatenation = "";
        private Dictionary<string, List<string>> _dicDonViNganh = new Dictionary<string, List<string>>();
        public HashSet<string> _filterXauNoiMa = new HashSet<string>();
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public override System.Type ContentType => typeof(PhanBoDuToanChiDetail);
        public bool IsSaveData => Items.Any(x => x.IsModified || x.IsDeleted);
        //public bool IsDieuChinh => (BudgetType.ADDITIONAL.Equals((BudgetType)Model.ILoaiDotNhanPhanBo) || BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR.Equals((BudgetType)Model.ILoaiDotNhanPhanBo));
        public bool IsDeleteAll;
        public bool IsTypeLuyKe => _isNamLuyKe;
        public bool IsShowQuyetDinh => _isShowQuyetDinh;
        public bool HasLastDivisionEstimateVoucher { get; set; }
        public bool IsShowKhcPlanButton => Model.ILoaiDotNhanPhanBo == (int)EstimateTypeNum.YEAR;
        public bool IsShowAdjustButton => !IsShowKhcPlanButton;
        public int NamLamViec { get; set; }
        public bool IsPropretyChange;
        private DivisionEstimateDetailPropertyHelper _detailHelper = new DivisionEstimateDetailPropertyHelper();
        public DivisionEstimateDetailPropertyHelper DetailHelper
        {
            get => _detailHelper;
            set => SetProperty(ref _detailHelper, value);
        }

        private DivisionEstimateDetailPropertyHelper _detailTotal;

        public DivisionEstimateDetailPropertyHelper DetailTotal
        {
            get => _detailTotal;
            set => SetProperty(ref _detailTotal, value);
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
                }
            }
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
                OnPropertyChanged(nameof(IsEnablePhanBoAll));
            }
        }

        private Visibility _isShowColDieuChinh;
        public Visibility IsShowColDieuChinh
        {

            get => _isShowColDieuChinh;
            set
            {
                SetProperty(ref _isShowColDieuChinh, value);

            }
        }

        private Visibility _isShowCol;
        public Visibility IsShowCol
        {

            get => _isShowCol;
            set
            {
                SetProperty(ref _isShowCol, value);

            }
        }
        public bool FirstTimePhanBo { get; set; }


        public bool IsEnablePhanBoAll => SelectedAgency != null;
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
                }
            }
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private DivisionColumnVisibility _columnVisibility;
        public DivisionColumnVisibility ColumnVisibility
        {
            get => _columnVisibility;
            set => SetProperty(ref _columnVisibility, value);
        }

        private ObservableCollection<DanhMucNganhModel> _NNganhItems;
        public ObservableCollection<DanhMucNganhModel> NNganhItems
        {
            get => _NNganhItems;
            set => SetProperty(ref _NNganhItems, value);
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
                }
            }
        }

        private ObservableCollection<DanhMucNganhModel> _cNganhItems;
        public ObservableCollection<DanhMucNganhModel> CNganhItems
        {
            get => _cNganhItems;
            set => SetProperty(ref _cNganhItems, value);
        }

        private DanhMucNganhModel _selectedCNganh;
        public DanhMucNganhModel SelectedCNganh
        {
            get => _selectedCNganh;
            set
            {
                if (SetProperty(ref _selectedCNganh, value))
                {
                    BeForeRefresh();
                    ItemsView.Refresh();
                }
            }
        }

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set
            {
                if (SetProperty(ref _sNoiDungSearch, value))
                {
                    BeForeRefresh();
                    SearchTextFilter();
                    ItemsView.Refresh();
                    //_budgetCatalogItemsView.Refresh();
                }
            }
        }

        private ObservableCollection<BhPbdtcBHXHChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhPbdtcBHXHChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhPbdtcBHXHChiTietModel _selectedPopupItem;
        public BhPbdtcBHXHChiTietModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SNoiDungSearch = _selectedPopupItem?.SNoiDung;
                OnPropertyChanged(nameof(SNoiDungSearch));
                IsPopupOpen = false;
            }
        }

        private ObservableCollection<BhPbdtcBHXHChiTietModel> _dataSearch;
        public ObservableCollection<BhPbdtcBHXHChiTietModel> DataSearch
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

        public bool IsAnotherUserCreate { get; set; }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        private bool _isAdjusted;
        public bool IsAdjusted
        {
            get => _isAdjusted;
            set => SetProperty(ref _isAdjusted, value);
        }
        public bool IsFillDataDauNam { get; set; }
        public RelayCommand GetKhcPlanDataCommand { get; }
        public RelayCommand GetDataAdjustCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand FillDataDauNamCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand OnOpenPhanboChiTietKPQL { get; }
        public PrintPhanBoTuToanChiTheoDonViViewModel PrintPhanBoTuToanChiTheoDonViViewModel { get; set; }
        public TongHopThuChiViewModel TongHopThuChiViewModel { get; set; }
        public PhanBoDuToanChiTietChiKPQLViewModel PhanBoDuToanChiTietChiKPQLViewModel { get; set; }
        public PrintPhuLucGiaoDuToanDuToanChiViewModel PrintPhuLucGiaoDuToanDuToanChiViewModel { get; set; }
        public PrintChiTietDuToanChiKPQLViewModel PrintChiTietDuToanChiKPQLViewModel { get; set; }
        public PhanBoDuToanChiDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IPbdtcBHXHService pbdtcBHXHService,
            IPbdtcBHXHChiTietService pbdtcBHXHChiTietService,
            IPbdtcMapBHXHService pbdtcMapBHXHService,
            INdtctgBHXHService ndtctgBHXHService,
            INdtctgBHXHChiTietService ndtctgBHXHChiTietService,
            INsDonViService nsDonViService,
            IDanhMucService danhMucService,
            ILog logger,
            PrintPhanBoTuToanChiTheoDonViViewModel printPhanBoTuToanChiTheoDonViViewModel,
            TongHopThuChiViewModel tongHopThuChiViewModel,
            DanhMucNganhService danhMucNganhService,
            IBhKhcCheDoBhXhService bhKhKeHoachChiService,
            IBhKhcCheDoBhXhChiTietService bhKhcCheDoBhXhChiTietService,
            IBhKhcKService BhKhcKService,
            IBhKhcKChiTietService BhKhcKChiTietService,
            IBhKhcKcbService bhKhcKcbService,
            IBhKhcKcbChiTietService bhKhcKcbChiTietService,
            IBhKhcKinhphiQuanlyService bhKhcKinhphiQuanlyService,
            IBhKhcKinhphiQuanlyChiTietService bhKhcKinhphiQuanlyChiTietService,
            IBhDtcDcdToanChiChiTietService bhDtcDcdToanChiChiTietService,
            IBhDtcDcdToanChiService bhDtcDcdToanChiService,
            IBhDtCtctKPQLService bhDtCtctKPQLService,
            PhanBoDuToanChiTietChiKPQLViewModel phanBoDuToanChiTietChiKPQLViewModel,
            PrintPhuLucGiaoDuToanDuToanChiViewModel printPhuLucGiaoDuToanDuToanChiViewModel,
            PrintChiTietDuToanChiKPQLViewModel printChiTietDuToanChiKPQLViewModel
            ) : base(danhMucService, sessionService)
        {
            _mapper = mapper;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _pbdtcBHXHService = pbdtcBHXHService;
            _pbdtcBHXHChiTietService = pbdtcBHXHChiTietService;
            _pbdtcMapBHXHService = pbdtcMapBHXHService;
            _ndtctgBHXHService = ndtctgBHXHService;
            _ndtctgBHXHChiTietService = ndtctgBHXHChiTietService;
            _bhKeHoachChiService = bhKhKeHoachChiService;
            _bhKhcCheDoBhXhChiTietService = bhKhcCheDoBhXhChiTietService;
            _bhKhcKService = BhKhcKService;
            _bhKhcKcbService = bhKhcKcbService;
            _bhKhcKinhphiQuanlyService = bhKhcKinhphiQuanlyService;
            _bhDtcDcdToanChiChiTietService = bhDtcDcdToanChiChiTietService;
            _bhDtcDcdToanChiService = bhDtcDcdToanChiService;
            _bhDtCtctKPQLService = bhDtCtctKPQLService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _danhMucService = danhMucService;
            _danhMucNganhService = danhMucNganhService;
            _logger = logger;
            PhanBoDuToanChiTietChiKPQLViewModel = phanBoDuToanChiTietChiKPQLViewModel;
            PrintPhanBoTuToanChiTheoDonViViewModel = printPhanBoTuToanChiTheoDonViViewModel;
            PrintPhuLucGiaoDuToanDuToanChiViewModel = printPhuLucGiaoDuToanDuToanChiViewModel;
            PrintChiTietDuToanChiKPQLViewModel = printChiTietDuToanChiKPQLViewModel;
            TongHopThuChiViewModel = tongHopThuChiViewModel;
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            PrintCommand = new RelayCommand(OnPrint);
            PrintReportCommand = new RelayCommand(OnPrintForAgency);
            SearchCommand = new RelayCommand(obj =>
            {
                BeForeRefresh();
                OnSearch();
            });
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            GetKhcPlanDataCommand = new RelayCommand(obj => GetPlanData());
            GetDataAdjustCommand = new RelayCommand(obj => GetAdjustData());
            OnOpenPhanboChiTietKPQL = new RelayCommand(obj => OnOpenDTPBCTKPQL());
        }

        private void OnOpenDTPBCTKPQL()
        {
            PhanBoDuToanChiTietChiKPQLViewModel.BhPbdtcBHXHModel = Model;
            PhanBoDuToanChiTietChiKPQLViewModel.IIDChungTuChiTiet = Guid.Empty;
            PhanBoDuToanChiTietChiKPQLViewModel.Init();
            var view = new PhanBoDuToanChiTietChiKPQL()
            {
                DataContext = PhanBoDuToanChiTietChiKPQLViewModel
            };
            view.ShowDialog();
        }

        private void OnPrint(object param)
        {
            var divisionPrintType = (SocialInsuranceDivisionEstimatePrintType)((int)param);
            TongHopThuChiViewModel.ReportNameTypeValue = (int)divisionPrintType;
            TongHopThuChiViewModel.ReportTypeValue = divisionPrintType;
            TongHopThuChiViewModel.Init();
            var view = new TongHopThuChi
            {
                DataContext = TongHopThuChiViewModel
            };
            DialogHost.Show(view, SystemConstants.DETAIL_DIALOG, null, null);
        }

        private void OnPrintForAgency(object obj)
        {
            try
            {
                var printType = (SocialInsuranceDivisionEstimatePrintType)((int)obj);
                if ((int)printType == (int)SocialInsuranceDivisionEstimatePrintType.TARGET_AGENCY)
                {
                    PrintPhanBoTuToanChiTheoDonViViewModel.Init();
                    var view = new PrintPhanBoTuToanChiTheoDonVi
                    {
                        DataContext = PrintPhanBoTuToanChiTheoDonViViewModel
                    };
                    DialogHost.Show(view, SystemConstants.DETAIL_DIALOG, null, null);
                }

                if ((int)printType == (int)SocialInsuranceDivisionEstimatePrintType.DELIVER)
                {
                    PrintPhuLucGiaoDuToanDuToanChiViewModel.Init();
                    var view = new PrintPhuLucGiaoDuToanDuToanChi
                    {
                        DataContext = PrintPhuLucGiaoDuToanDuToanChiViewModel
                    };
                    DialogHost.Show(view, SystemConstants.DETAIL_DIALOG, null, null);
                }
                if ((int)printType == (int)SocialInsuranceDivisionEstimatePrintType.DU_TOAN_CHITIET_KPQL)
                {
                    PrintChiTietDuToanChiKPQLViewModel.Init();
                    var view = new PrintChiTietDuToanChiKPQL
                    {
                        DataContext = PrintChiTietDuToanChiKPQLViewModel
                    };
                    DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
                }
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        public override void Init()
        {
            base.Init();
            IsFillDataDauNam = true;
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            ResetConditionSearch();
            LoadDotNhan();
            LoadControlVisibility();
            LoadTypeDisplay();
            LoadAgencies();
            IsPropretyChange = true;
            LoadData();
            IsPropretyChange = false;

            OnClearSearch(false);
        }

        private void LoadControlVisibility()
        {
            string lns = Model.SLNS;
            //_listMLNS = _mlnsService.FindByListLnsDonVi(lns, _sessionService.Current.YearOfWork).ToList();
            _columnVisibility = new DivisionColumnVisibility();
            _columnVisibility.IsDisplayTuChiDieuChinh = _isAdjusted;
            _columnVisibility.IsDisplayHienVatDieuChinh = _isAdjusted;

            DetailHelper.VisibilityBudgetTypeAdjusted = _isAdjusted ? Visibility.Visible : Visibility.Collapsed;
            DetailHelper.VisibilityBudgetTypeNoneAdjusted = !_isAdjusted ? Visibility.Visible : Visibility.Collapsed;

            IsShowColDieuChinh = _isAdjusted ? Visibility.Visible : Visibility.Hidden;
            IsShowCol = _isAdjusted ? Visibility.Hidden : Visibility.Visible;

            OnPropertyChanged(nameof(IsShowColDieuChinh));
            OnPropertyChanged(nameof(IsShowCol));
            OnPropertyChanged(nameof(ColumnVisibility));
        }

        private void ResetConditionSearch()
        {
            DetailTotal = new DivisionEstimateDetailPropertyHelper();
            DetailFilter = new EstimationDetailCriteria();
            _listDanhMucNganh = new List<DanhMuc>();
            _dicDonViNganh = new Dictionary<string, List<string>>();
            Items = new ObservableCollection<BhPbdtcBHXHChiTietModel>();
            _isAdjusted = false;
            if (BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDotNhanPhanBo))
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
            SelectedAgency = null;
            BeForeRefresh();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var listDonViQuanLy = _sessionService.Current.IdsDonViQuanLy.Split(StringUtils.COMMA).ToList();
                var listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).Where(x => x.Loai.Equals(LoaiDonVi.ROOT)).Select(y => y.IIDMaDonVi).ToList();
                var listIntersect = listDonViQuanLy.Intersect(listDonVi);

                int iNamLamViec = _sessionInfo.YearOfWork;
                List<string> ListLNS = new List<string>();
                ListLNS = Model.SLNS.Split(",").ToList();
                if (ListLNS.Contains(LNSValue.LNS_9010001) || ListLNS.Contains(LNSValue.LNS_9010002))
                {
                    Model.SLNS = Model.SLNS + "," + LNSValue.LNS_901;
                }

                if (ListLNS.Contains(LNSValue.LNS_9010004_9010005) || ListLNS.Contains(LNSValue.LNS_9010003)
                    || ListLNS.Contains(LNSValue.LNS_9010006_9010007) || ListLNS.Contains(LNSValue.LNS_9010008) ||
                    ListLNS.Contains(LNSValue.LNS_9010009) || ListLNS.Contains(LNSValue.LNS_9010010))
                {
                    Model.SLNS = Model.SLNS + "," + LNSValue.LNS_9;
                }
                string sIdDonVi = Model.SID_MaDonVi;

                List<BhPbdtcBHXHChiTietQuery> listDataQuery = new List<BhPbdtcBHXHChiTietQuery>();

                listDataQuery = _pbdtcBHXHChiTietService.FindChungTuChiTiet(Model.Id, Model.SLNS, sIdDonVi, iNamLamViec, _sessionInfo.Principal,Model.ILoaiDotNhanPhanBo).ToList();
                var predicate = PredicateBuilder.True<BhPbdtcBHXHChiTiet>();
                predicate = predicate.And(x => x.IID_DTC_PhanBoDuToanChi == Model.Id);
                predicate = predicate.And(x => x.SMaLoaiChi == MaLoaiChiBHXH.SMAKCBQYDV);
                var chungTuChiTietExist = _pbdtcBHXHChiTietService.FindByCondition(predicate);

                //if (Model.ILoaiDotNhanPhanBo == 2 && ListLNS.Contains(LNSValue.LNS_9010004_9010005))
                //{
                //    var lstPhanBoDtBhxhQuanNhan = _pbdtcBHXHChiTietService.FindGiaTriDieuChinhThuBHXHChangeRequest(sIdDonVi, iNamLamViec).ToList();
                //    var lstData = listDataQuery.Where(x => !x.BHangCha && x.Type == (int)BaoHiemDuToanTypeEnum.Type.HANG_CON && x.SLNS == LNSValue.LNS_9010004_9010005).ToList();
                //    if (lstPhanBoDtBhxhQuanNhan.Count > 0)
                //    {
                //        foreach (var item in lstData)
                //        {
                //            if (item.SLNS == LNSValue.LNS_9010004_9010005)
                //            {
                //                var chungtu = lstPhanBoDtBhxhQuanNhan.Where(x => x.IID_MaDonVi == item.IID_MaDonVi && x.SXauNoiMa == item.SXauNoiMa).FirstOrDefault();
                //                if (chungtu != null)
                //                {
                //                    item.FTienTuChi = chungtu.FTienTuChi.GetValueOrDefault(0);
                //                }
                //            }
                //        }
                //    }
                //}

                Items = _mapper.Map<ObservableCollection<BhPbdtcBHXHChiTietModel>>(listDataQuery);
                DataPopupSearchItems = _mapper.Map<ObservableCollection<BhPbdtcBHXHChiTietModel>>(listDataQuery);
                ItemsView = CollectionViewSource.GetDefaultView(Items);
                ItemsView.Filter = ItemsViewFilter;


                foreach (var bhDtctgBHXHChiTietModel in Items)
                {
                    bhDtctgBHXHChiTietModel.IsFilter = true;
                    if (!bhDtctgBHXHChiTietModel.BHangCha)
                    {
                        bhDtctgBHXHChiTietModel.PropertyChanged += OnchangeDetail_PropertyChanged;
                    }
                }
                CalculateData();
                CalculateRemainRow();

                foreach (var item in Items.Where(x => x.SXauNoiMa == "9010004" && x.Type != (int)BaoHiemDuToanTypeEnum.Type.SO_CHUA_PHAN_BO).ToList())
                {
                    if (item.FTienTuChi != null && !chungTuChiTietExist.Any(item => item.FTienTuChi == item.FTienTuChi && item.SXauNoiMa == item.SXauNoiMa && item.IID_MaDonVi == item.IID_MaDonVi))
                    {
                        item.IsModified = true;
                        OnPropertyChanged(nameof(IsSaveData));
                    }

                    var chungTuChiTietEdit = chungTuChiTietExist.Where(x => x.SXauNoiMa == item.SXauNoiMa && x.IID_MaDonVi == item.IID_MaDonVi).FirstOrDefault();
                    if (chungTuChiTietEdit != null)
                    {
                        item.FTienTuChi = chungTuChiTietEdit.FTienTuChi;
                        item.IsModified = false;
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnchangeDetail_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IsPropretyChange = true;
            if (IsPropretyChange)
            {
                foreach (var item in Items.Where(x => !x.BHangCha))
                {
                    item.PropertyChanged -= OnchangeDetail_PropertyChanged;
                }
                var objSender = (BhPbdtcBHXHChiTietModel)sender;

                if (args.PropertyName.Equals(nameof(BhPbdtcBHXHChiTietModel.FTienTuChi)))
                {
                    objSender.IsModified = true;
                    OnPropertyChanged(nameof(IsSaveData));
                    objSender.FTienTuChiSauDieuChinh = (objSender.FTienTuChi ?? 0) + (objSender.FTienTuChiTruocDieuChinh ?? 0);
                    //item.FTienHienVatSauDieuChinh = (item.FTienHienVat ?? 0) + (item.FTienHienVatTruocDieuChinh ?? 0);
                    CalculateData();
                    if (!IsAdjusted)
                    {
                        //UpdateSoChuaPhanBo();
                    }
                    OnPropertyChanged(nameof(IsSaveData));
                }
                foreach (var item in Items.Where(x => !x.BHangCha))
                {
                    item.PropertyChanged += OnchangeDetail_PropertyChanged;
                }

            }

            CalculateRemainRow();

            IsPropretyChange = false;
        }
        private void CalculateData()
        {

            Items.Where(x => x.BHangCha && x.Type != (int)BaoHiemDuToanTypeEnum.Type.SO_CHUA_PHAN_BO)
                .ForAll(x =>
                {
                    //x.FTienHienVat = 0;
                    x.FTienTuChi = 0;
                    //x.FTienHienVatTruocDieuChinh = 0;
                    x.FTienTuChiTruocDieuChinh = 0;
                    //x.FTienTuChiSauDieuChinh = 0;
                    //x.FTienHienVatSauDieuChinh = 0;
                });
            var dictByMlns = Items.Where(x => !x.IsRemainRow).GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            var temp = Items.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (var item in temp)
            {

                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }
            UpdateTotal();
        }

        private void CalculateParent(Guid? idParent, BhPbdtcBHXHChiTietModel item, Dictionary<Guid?, BhPbdtcBHXHChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            if (!model.IsRemainRow || model.Type != 2)
            {
                //Trước điều chỉnh
                model.FTienTuChiTruocDieuChinh = (model.FTienTuChiTruocDieuChinh == null ? 0 : model.FTienTuChiTruocDieuChinh) + (item.FTienTuChiTruocDieuChinh == null ? 0 : item.FTienTuChiTruocDieuChinh);
                //model.FTienHienVatTruocDieuChinh = (model.FTienHienVatTruocDieuChinh == null ? 0 : model.FTienHienVatTruocDieuChinh) + (item.FTienHienVatTruocDieuChinh == null ? 0 : item.FTienHienVatTruocDieuChinh);

                //model.FTienHienVat = (model.FTienHienVat == null ? 0 : model.FTienHienVat) + (item.FTienHienVat == null ? 0 : item.FTienHienVat);
                model.FTienTuChi = (model.FTienTuChi ?? 0) + (item.FTienTuChi ?? 0);

                //Sau điều chinh
                model.FTienTuChiSauDieuChinh = (model.FTienTuChiTruocDieuChinh ?? 0) + (model.FTienTuChi ?? 0);
                // model.FTienHienVatSauDieuChinh = (model.FTienHienVatTruocDieuChinh ?? 0) + (model.FTienHienVat ?? 0);

                CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
            }

        }

        private void UpdateSoChuaPhanBo()
        {

            var bhPbdtcChiTiet = (BhPbdtcBHXHChiTietModel)SelectedItem;
            if (bhPbdtcChiTiet != null)
            {

                var lstCtChiTietNhanPb = _ndtctgBHXHChiTietService.FindByCondition(bhPbdtcChiTiet.IID_DTC_DuToanChiTrenGiao).Where(x => x.IID_MucLucNganSach == bhPbdtcChiTiet.IID_MLNS).ToList();
                Double? fTuChiNhanPhanBo = lstCtChiTietNhanPb?.Select(x => x.FTienTuChi).Sum();
                //Double? fHienVatNhanPhanBo = lstCtChiTietNhanPb?.Select(x => x.FTienHienVat).Sum();

                var predicate = PredicateBuilder.True<BhPbdtcBHXHChiTiet>();
                predicate = predicate.And(x => x.IID_DTC_DuToanChiTrenGiao == bhPbdtcChiTiet.IID_DTC_DuToanChiTrenGiao);
                predicate = predicate.And(x => x.IID_MucLucNganSach == bhPbdtcChiTiet.IID_MLNS);
                predicate = predicate.And(x => x.IID_DTC_PhanBoDuToanChi != Model.Id);
                var lstCtChiTietDaPhanBo = _pbdtcBHXHChiTietService.FindByCondition(predicate);

                Double? fTuChiDaPhanBo = lstCtChiTietDaPhanBo?.Select(x => x.FTienTuChi).Sum();
                //Double? fHienVatDaPhanBo = lstCtChiTietDaPhanBo?.Select(x => x.FTienHienVat).Sum();

                Double? fTuChiPhanBo = Items.Where(x => x.IID_MLNS == SelectedItem.IID_MLNS && x.IID_DTC_DuToanChiTrenGiao == SelectedItem.IID_DTC_DuToanChiTrenGiao && !x.BHangCha && !x.IsDeleted).Select(x => x.FTienTuChi).Sum();

                Items.Where(x => x.Type == (int)BaoHiemDuToanTypeEnum.Type.SO_CHUA_PHAN_BO && x.IID_MLNS == SelectedItem.IID_MLNS && x.IID_DTC_DuToanChiTrenGiao == SelectedItem.IID_DTC_DuToanChiTrenGiao)
                .Select(x =>
                {
                    x.FTienTuChi = (fTuChiNhanPhanBo ?? 0) - (fTuChiDaPhanBo ?? 0) - (fTuChiPhanBo ?? 0);

                    return x;
                }).ToList();

            }
        }

        private void UpdateTotal()
        {
            Model.FTongTienHienVat = 0;
            Model.FTongTienTuChi = 0;
            Model.FTongTienTuChiTruocDieuChinh = 0;
            Model.FTongTienTuChiSauDieuChinh = 0;
            Model.FTongTienHienVatTruocDieuChinh = 0;
            Model.FTongTienHienVatSauDieuChinh = 0;
            Model.FTongTienHienVat = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienHienVat);
            Model.FTongTienTuChi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienTuChi);

            Model.FTongTienTuChiTruocDieuChinh = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienTuChiTruocDieuChinh);
            Model.FTongTienHienVatTruocDieuChinh = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienHienVatTruocDieuChinh);
            Model.FTongTienTuChiSauDieuChinh = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienTuChiSauDieuChinh);
            Model.FTongTienHienVatSauDieuChinh = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienHienVatSauDieuChinh);


        }

        private void CalculateRemainRow()
        {
            var lstRemainRows = Items.Where(x => x.IsRemainRow);
            foreach (var item in lstRemainRows)
            {
                if (Items.Any(y => y.BHangCha && y.SXauNoiMa.Equals(item.SXauNoiMa) && !y.IsRemainRow))
                {
                    var items = Items.Where(y => !y.IsRemainRow && !y.BHangCha && y.SXauNoiMa.Equals(item.SXauNoiMa)).Sum(s => s.FTienTuChi.GetValueOrDefault());
                    item.FTienTuChi = item.FTienTuChiTruocDieuChinh.GetValueOrDefault() - items;
                }
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
            Agencies = _mapper.Map<ObservableCollection<ComboboxItem>>(listNsDonVi);
        }

        private void LoadDotNhan()
        {
            List<BhPbdtcBHXH> chungTus = _pbdtcBHXHService.FindDotNhanByChungTuPhanBo(Model.Id).ToList();
            List<ComboboxItem> cbxChungTus = new List<ComboboxItem>();
            foreach (var chungTu in chungTus)
            {
                cbxChungTus.Add(new ComboboxItem { ValueItem = chungTu.Id.ToString(), DisplayItem = chungTu.SSoQuyetDinh });
            }
            CbxNhanPhanBos = new ObservableCollection<ComboboxItem>(cbxChungTus);
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
                    Items.Where(x => x.IsFilter && !x.BHangCha).ForAll(x => x.IsDeleted = true);
                    OnSave();
                }
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(BhPbdtcBHXHChiTietModel.FTienHienVat)
                || args.PropertyName == nameof(BhPbdtcBHXHChiTietModel.FTienTuChi))
            {
                BhPbdtcBHXHChiTietModel item = (BhPbdtcBHXHChiTietModel)sender;
                item.FTienTuChiSauDieuChinh = (item.FTienTuChiTruocDieuChinh ?? 0) + (item.FTienTuChi ?? 0);
                item.FTienHienVatSauDieuChinh = (item.FTienHienVatTruocDieuChinh ?? 0) + (item.FTienHienVat ?? 0);
                CalculateData();
                if (_isAdjusted)
                {
                    UpdateSoChuaPhanBo();
                }
                item.IsModified = true;
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        //protected override void OnAdd()
        //{
        //    if (_isNamLuyKe || (Model.ILoaiDotNhanPhanBo != null && !BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDotNhanPhanBo.Value)))
        //    {
        //        MessageBox.Show("Thêm mới không khả dụng. Hãy thao tác với các bản ghi đang hiển thị trên lưới", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Information);
        //        return;
        //    }

        //    if (SelectedItem != null)
        //    {
        //        int currentRow = Items.IndexOf(SelectedItem);
        //        int targetRow = Items.ToList().FindIndex(currentRow, x => x.IsEditable);
        //        if (targetRow > -1)
        //        {
        //            BhPbdtcBHXHChiTietModel sourceItem = Items.ElementAt(targetRow);
        //            BhPbdtcBHXHChiTietModel targetItem = CloneRow(sourceItem);
        //            OnPropertyChanged(nameof(targetItem));
        //            targetItem.PropertyChanged += DetailModel_PropertyChanged;
        //            Items.Insert(targetRow + 1, targetItem);
        //            OnPropertyChanged(nameof(Items));
        //            OnPropertyChanged(nameof(IsSaveData));
        //            OnPropertyChanged(nameof(IsDeleteAll));
        //        }
        //    }
        //}

        //private BhPbdtcBHXHChiTietModel CloneRow(BhPbdtcBHXHChiTietModel sourceItem)
        //{
        //    BhPbdtcBHXHChiTietModel targetItem = ObjectCopier.Clone(sourceItem);

        //    targetItem.Id = Guid.Empty;
        //    targetItem.IID_DTC_PhanBoDuToanChiTiet = null;
        //    targetItem.BEmty = true;
        //    targetItem.FTienTuChi = 0;
        //    targetItem.FTienTuChiTruocDieuChinh = 0;
        //    targetItem.FTienTuChiSauDieuChinh = 0;
        //    targetItem.FTienHienVat = 0;
        //    targetItem.FTienHienVatSauDieuChinh = 0;
        //    targetItem.FTienHienVatTruocDieuChinh = 0;
        //    targetItem.CbxDonVi = Agencies;
        //    targetItem.CbxNhanPhanBos = CbxNhanPhanBos;

        //    return targetItem;
        //}

        public override void OnSave()
        {
            if (!IsSaveData)
            {
                return;
            }

            var lstDataAdd = Items.Where(x => !x.BHangCha && string.IsNullOrEmpty(x.IID_DTC_PhanBoDuToanChiTiet.ToString()) && x.IsModified).ToList();
            var lstDataUpdate = Items.Where(x => !x.BHangCha && !string.IsNullOrEmpty(x.IID_DTC_PhanBoDuToanChiTiet.ToString()) && x.IsModified).ToList();
            var lstDataDelete = Items.Where(x => !x.BHangCha && x.IsDeleted && !string.IsNullOrEmpty(x.IID_DTC_PhanBoDuToanChiTiet.ToString())).ToList();

            var addItemList = new List<BhPbdtcBHXHChiTiet>();
            if (lstDataAdd.Count() > 0)
            {
                _mapper.Map(lstDataAdd, addItemList);
                addItemList = lstDataAdd.Select(x => new BhPbdtcBHXHChiTiet
                {
                    Id = Guid.NewGuid(),
                    DNgayTao = DateTime.Now,
                    DNgaySua = DateTime.Now,
                    SNguoiTao = _sessionInfo.Principal,
                    IID_DTC_DuToanChiTrenGiao = x.IID_DTC_DuToanChiTrenGiao,
                    IID_DTC_PhanBoDuToanChi = Model.Id,
                    IID_MaDonVi = x.IID_MaDonVi,
                    IID_MucLucNganSach = x.IID_MLNS.Value,
                    SLNS = x.SLNS,
                    SM = x.SM,
                    SNG = x.SNG,
                    SNoiDung = x.SNoiDung,
                    STM = x.STM,
                    STTM = x.STTM,
                    SXauNoiMa = x.SXauNoiMa,
                    INamLamViec = _sessionInfo.YearOfWork,
                    FTienHienVat = x.FTienHienVat,
                    FTienTuChi = x.FTienTuChi,
                    FTongTien = (x.FTienTuChi ?? 0) + (x.FTienHienVat ?? 0),
                    SMaLoaiChi = x.SMaLoaiChi,
                }).ToList();

                _pbdtcBHXHChiTietService.AddRange(addItemList);
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }
            if (lstDataUpdate.Count() > 0)
            {
                //_mapper.Map(lstDataUpdate, addItemList);
                addItemList = lstDataUpdate.Select(x => new BhPbdtcBHXHChiTiet
                {
                    Id = x.IID_DTC_PhanBoDuToanChiTiet ?? x.IID_DTC_PhanBoDuToanChiTiet.Value,
                    DNgayTao = DateTime.Now,
                    DNgaySua = DateTime.Now,
                    SNguoiTao = x.SNguoiTao,
                    SNguoiSua = _sessionInfo.Principal,
                    IID_DTC_DuToanChiTrenGiao = x.IID_DTC_DuToanChiTrenGiao,
                    IID_DTC_PhanBoDuToanChi = Model.Id,
                    IID_MaDonVi = x.IID_MaDonVi,
                    IID_MucLucNganSach = Items.Any(y => y.BHangCha && y.SXauNoiMa.Equals(x.SXauNoiMa)) ? Items.FirstOrDefault(y => y.BHangCha && y.SXauNoiMa.Equals(x.SXauNoiMa)).IID_MLNS.Value : Guid.Empty,
                    SLNS = x.SLNS,
                    SM = x.SM,
                    SNG = x.SNG,
                    SNoiDung = x.SNoiDung,
                    STM = x.STM,
                    STTM = x.STTM,
                    SXauNoiMa = x.SXauNoiMa,
                    INamLamViec = _sessionInfo.YearOfWork,
                    FTienHienVat = x.FTienHienVat,
                    FTienTuChi = x.FTienTuChi,
                    FTongTien = (x.FTienTuChi ?? 0) + (x.FTienHienVat ?? 0),
                    SMaLoaiChi = x.SMaLoaiChi,
                }).ToList();

                foreach (var item in addItemList)
                {
                    _pbdtcBHXHChiTietService.Update(item);
                }
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            if (lstDataDelete.Count > 0)
            {
                addItemList = lstDataDelete.Select(x => new BhPbdtcBHXHChiTiet
                {
                    Id = x.IID_DTC_PhanBoDuToanChiTiet ?? x.IID_DTC_PhanBoDuToanChiTiet.Value,
                }).ToList();

                foreach (var item in addItemList)
                {
                    _pbdtcBHXHChiTietService.Delete(item);
                }
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            //Update phân bổ dự toán chi
            var chungTuPhanBoModel = _pbdtcBHXHService.FindById(Model.Id);
            var predicate_chitiet = PredicateBuilder.True<BhPbdtcBHXHChiTiet>();
            predicate_chitiet = predicate_chitiet.And(x => x.IID_DTC_PhanBoDuToanChi == Model.Id);
            var lstChungTuChiTiet = _pbdtcBHXHChiTietService.FindByCondition(predicate_chitiet).ToList();

            chungTuPhanBoModel.FTongTienTuChi = lstChungTuChiTiet?.Select(x => x.FTienTuChi).Sum();
            chungTuPhanBoModel.FTongTienHienVat = lstChungTuChiTiet?.Select(x => x.FTienHienVat).Sum();
            chungTuPhanBoModel.FTongTien = lstChungTuChiTiet?.Select(x => x.FTongTien).Sum();

            //List<string> sMaDonVi = new List<string>();
            //sMaDonVi = lstChungTuChiTiet?.Select(x => x.IID_MaDonVi).Distinct().ToList();
            //chungTuPhanBoModel.SID_MaDonVi = string.Join(",", sMaDonVi);

            BhPbdtcBHXH chungtu = new BhPbdtcBHXH();
            chungtu = _mapper.Map(chungTuPhanBoModel, chungtu);
            _pbdtcBHXHService.Update(chungtu);

            OnPropertyChanged(nameof(IsSaveData));
            OnRefresh();
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            SavedAction?.Invoke(null);
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        #region search
        public void BeForeRefresh()
        {
            _filterResult = Items.Where(item => AllocationDetailItemsFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
            if (!string.IsNullOrEmpty(TypeDisplaysSelected) && TypeDisplaysSelected == TypeDisplay.DA_NHAN_DUTOAN)
            {
                var lstXauNoiMa = Items.Where(x => !x.IsHangCha && !x.IID_DTC_DuToanChiTrenGiao.IsNullOrEmpty()).Select(s => s.SXauNoiMa).Distinct().ToList();
                if (lstXauNoiMa.IsEmpty())
                    _filterXauNoiMa = new HashSet<string>();
                else
                {
                    var lstXc = StringUtils.GetListKyHieuParent(lstXauNoiMa);
                    _filterXauNoiMa = new HashSet<string>(StringUtils.GetListKyHieuParent(lstXauNoiMa));

                }
            }

        }


        private bool AllocationDetailItemsFilter(object obj)
        {
            //if (string.IsNullOrWhiteSpace(_searchLNS))
            //{
            //    return true;
            //}
            //return obj is BhPbdtcBHXHChiTietModel item && (item.SNoiDung.StartsWith(_searchLNS, StringComparison.OrdinalIgnoreCase) || item.SNoiDung.StartsWith(_searchLNS, StringComparison.OrdinalIgnoreCase));
            bool result = true;
            var item = (BhPbdtcBHXHChiTietModel)obj;
            if (!string.IsNullOrEmpty(SelectedLNS))
                result = result && item.SLNS.ToLower().StartsWith(SelectedLNS.Trim().ToLower());

            if (!string.IsNullOrEmpty(TypeDisplaysSelected))
            {
                if (TypeDisplaysSelected == TypeDisplay.CO_DU_LIEU)
                    result = result && (!NumberUtils.DoubleIsNullOrZero(item.FTienTuChi) && !item.BHangCha && !item.IsDeleted);
                else if (TypeDisplaysSelected == TypeDisplay.DA_NHAN_DUTOAN)
                    result = result && (!NumberUtils.DoubleIsNullOrZero(item.FTienTuChi) || (!item.IID_DTC_DuToanChiTrenGiao.IsNullOrEmpty()) && !item.IsDeleted);
            }

            if (SelectedAgency != null)
            {
                result = result && ((!string.IsNullOrEmpty(item.IID_MaDonVi) && item.IID_MaDonVi.StartsWith(SelectedAgency.ValueItem)));
            }

            if (!string.IsNullOrEmpty(DetailFilter.L))
                result = result && item.SL.ToLower().StartsWith(DetailFilter.L.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.K))
                result = result && item.SK.ToLower().StartsWith(DetailFilter.K.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.SM.ToLower().StartsWith(DetailFilter.M.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.STM.ToLower().StartsWith(DetailFilter.TM.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.STTM.ToLower().StartsWith(DetailFilter.TTM.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.SNG.ToLower().StartsWith(DetailFilter.NG.Trim().ToLower());

            item.IsFilter = result;
            return result;
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhPbdtcBHXHChiTietModel)obj;
            result = AllocationDetailItemsFilter(item);
            if (!result && item.BHangCha && !string.IsNullOrEmpty(TypeDisplaysSelected) && TypeDisplaysSelected == TypeDisplay.DA_NHAN_DUTOAN)
            {
                result = _filterXauNoiMa.Any(x => x.Equals(item.SXauNoiMa));
            }

            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = result && DataSearch.Any(x => x.IID_MLNS.Equals(item.IID_MLNS));
            }

            if (result)
                item.IsFilter = result;
            return result;
        }

        private void OnSearch()
        {
            SearchTextFilter();
        }

        private void OnClearSearch(object obj)
        {
            SelectedAgency = null;
            SNoiDungSearch = string.Empty;
            if (!(obj is bool temp))
            {
                ItemsView.Refresh();
            }
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhPbdtcBHXHChiTietModel> results = new List<BhPbdtcBHXHChiTietModel>();

                List<string> lstSXaNoiMaChildSearch = DataPopupSearchItems.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.BHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                List<string> lstSXaNoiMaParentSearch = DataPopupSearchItems.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.BHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
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
                DataSearch = new ObservableCollection<BhPbdtcBHXHChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhPbdtcBHXHChiTietModel>();
            }
            ItemsView.Refresh();
        }

        private List<BhPbdtcBHXHChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhPbdtcBHXHChiTietModel> result = new List<BhPbdtcBHXHChiTietModel>();
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

        private void GetListChild(List<BhPbdtcBHXHChiTietModel> lstInput, List<BhPbdtcBHXHChiTietModel> results)
        {
            var itemChild = DataPopupSearchItems.Where(x => lstInput.Select(x => x.IID_MLNS).Distinct().Contains(x.IID_MLNS_Cha)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => DataPopupSearchItems.Select(y => y.IID_MLNS_Cha).Distinct().Contains(x.IID_MLNS)))
                {
                    GetListChild(new List<BhPbdtcBHXHChiTietModel>() { item }, results);
                }
            }
        }
        #endregion


        private void GetPlanData()
        {
            var chiCheDo = (_bhKeHoachChiService.FindAggregateVoucher(NamLamViec)?.STongHop ?? "").Split(',').ToList();

            var chiKhac = (_bhKhcKService.FindAggregateVoucher(NamLamViec)).ToList();
            List<string> sChikhacSoChungTu = new List<string>();
            foreach (var item in chiKhac)
            {
                var sCTCon = item.STongHop.Split(',').ToList();
                sChikhacSoChungTu.AddRange(sCTCon);
            }

            var chiKCB = (_bhKhcKcbService.FindAggregateVoucher(NamLamViec)?.STongHop ?? "").Split(',').ToList();
            var chiKPQL = (_bhKhcKinhphiQuanlyService.FindAggregateVoucher(NamLamViec)?.STongHop ?? "").Split(',').ToList();
            var lstVoucher = chiCheDo.Concat(sChikhacSoChungTu).Concat(chiKCB).Concat(chiKPQL).Distinct().ToList();
            var vouchers = string.Join(",", lstVoucher);
            if (!string.IsNullOrEmpty(vouchers))
            {
                List<BhKhcCheDoBhXhChiTietQuery> planData = _bhKhcCheDoBhXhChiTietService.GetPlanData(NamLamViec, vouchers, Model.SLNS).ToList();
                var planIsHadData = planData.Where(x => x.IsHadData).ToList();
                if (planIsHadData.Count() > 0)
                {
                    //CalculateDataGetPlan(planData);
                    var itemFilter = Items.Where(x => !x.BHangCha && x.Type == (int)BaoHiemDuToanTypeEnum.Type.HANG_CON && x.SXauNoiMa != "9010004");
                    planIsHadData = planData.Where(x => x.IsHadData).ToList();
                    foreach (var itemPBDTC in itemFilter)
                    {
                        foreach (var item in planIsHadData)
                        {
                            // var sSXauNoiMaSub = itemPBDTC.SXauNoiMa.Substring(1, 20);
                            if (item.SXauNoiMa == itemPBDTC.SXauNoiMa && item.IIdMaDonVi == itemPBDTC.IID_MaDonVi)
                            {
                                itemPBDTC.FTienTuChi = 0;
                                itemPBDTC.FTienTuChi = item.fTienKeHoachThucHienNamNay.GetValueOrDefault(0);
                                if (itemPBDTC.IsEmptyPlanData)
                                    itemPBDTC.IsModified = false;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBoxHelper.Info(Resources.MsgPlanData);
            }
        }
        private void GetAdjustData()
        {
            //var dieuChinh = (_bhKeHoachChiService.FindAggregateVoucher(NamLamViec)?.STongHop ?? "").Split(',').ToList();

            var dieuChinhAllLoaiChi = (_bhDtcDcdToanChiService.FindAggregateAdjustVoucher(_sessionInfo.YearOfWork, Model.IIDLoaiDanhMucChi, Model.SMaLoaiChi)).ToList();
            dieuChinhAllLoaiChi = dieuChinhAllLoaiChi.Where(x => !string.IsNullOrEmpty(x.STongHop)).ToList();
            List<string> sSoChungTu = new List<string>();
            foreach (var item in dieuChinhAllLoaiChi)
            {
                var sCTCon = item.STongHop.Split(',').ToList();
                sSoChungTu.AddRange(sCTCon);
            }

            var vouchers = string.Join(",", sSoChungTu);
            if (!string.IsNullOrEmpty(vouchers))
            {
                List<BhDtcDcdToanChiChiTietQuery> planData = _bhDtcDcdToanChiChiTietService.GetAdjustData(NamLamViec, vouchers, Model.SLNS, Model.SMaLoaiChi, Model.SID_MaDonVi).ToList();
                planData = planData.Where(x => x.SXauNoiMa != LNSValue.LNS_9010004_9010005).ToList();
                if (planData.Count() > 0)
                {
                    //CalculateDataGetPlan(planData);
                    var itemFilter = Items.Where(x => !x.BHangCha && x.Type == (int)BaoHiemDuToanTypeEnum.Type.HANG_CON);

                    foreach (var itemPBDTC in itemFilter)
                    {
                        foreach (var item in planData)
                        {

                            if (item.SXauNoiMa == itemPBDTC.SXauNoiMa && item.IIdMaDonVi == itemPBDTC.IID_MaDonVi)
                            {
                                itemPBDTC.FTienTuChi = 0;
                                itemPBDTC.FTienTuChi = item.FTienTangGiam.GetValueOrDefault(0);
                                if (itemPBDTC.IsEmptyPlanData)
                                    itemPBDTC.IsModified = false;
                            }
                        }
                    }

                }
            }
            else
            {
                MessageBoxHelper.Info(Resources.MessageNoAdjustData);
            }
        }

        private void CalculateDataGetPlan(List<BhKhcCheDoBhXhChiTietQuery> lstGetPlan)
        {

            lstGetPlan.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    x.fTienKeHoachThucHienNamNay = 0;
                });

            var temp = lstGetPlan.Where(x => !x.BHangCha).ToList();
            var dictByMlns = lstGetPlan.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParentGetPlan(item.IID_MLNS_Cha, item, dictByMlns);
            }
        }

        private void CalculateParentGetPlan(Guid idParent, BhKhcCheDoBhXhChiTietQuery item, Dictionary<Guid, BhKhcCheDoBhXhChiTietQuery> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.fTienKeHoachThucHienNamNay = model.fTienKeHoachThucHienNamNay.GetValueOrDefault(0) + item.fTienKeHoachThucHienNamNay.GetValueOrDefault(0);

            CalculateParentGetPlan(model.IID_MLNS_Cha, item, dictByMlns);
        }
    }

}
