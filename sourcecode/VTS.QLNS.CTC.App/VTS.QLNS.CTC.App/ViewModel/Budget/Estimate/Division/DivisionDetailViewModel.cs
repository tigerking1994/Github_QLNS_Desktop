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
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.Settlement.GetDataLuong;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.GetDataLuong;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division
{
    public class DivisionDetailViewModel : DetailViewModelBase<DtChungTuModel, DtChungTuChiTietModel>
    {
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsDcChungTuChiTietService _dcChungTuChiTietService;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDcChungTuService _dcChungTuService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly INsDonViService _nsDonViService;
        private readonly ILbChungTuService _chungTuService;
        private readonly INsDtChungTuCanCuService _dtChungTuCanCuService;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly INsMucLucNganSachService _mlnsService;
        private readonly ICauHinhMLNSService _cauHinhMLNSService;
        private readonly INsDtNhanPhanBoMapService _dtChungTuMapService;
        private DanhMucNganhService _danhMucNganhService;
        private ICollectionView _budgetCatalogItemsView;
        private ICollectionView _budgetCatalogItemsMirrorView;
        private ICollectionView _chungTuChiTietItemsView;
        private ICollectionView _chungTuChiTietItemsImportView;
        private ICollectionView _chungTuChiTietItemsInputView;
        private IMapper _mapper;
        private EstimationVoucherDetailCriteria _searchCondition;
        private List<DtChungTuChiTietModel> _listCompare;
        private List<NsDtChungTuChiTietQuery> _listChungTuChiTiet;
        private List<NsDtChungTuChiTietQuery> _listChungTuChiTietDuLieuNhap;
        private List<NsDtChungTuChiTietQuery> _listChungTuChiTietDynamic;
        private bool _isCompareData;
        private List<NsMucLucNganSach> _listMLNS;
        private SessionInfo _sessionInfo;
        private List<DanhMuc> _listDanhMucNganh;
        private ICollection<DtChungTuChiTietModel> _filterResult = new HashSet<DtChungTuChiTietModel>();
        private string xnmConcatenation = "";
        private string xnmConcatenationMirror = "";
        private bool _isFillData;
        private List<Guid> _listGuidTyp2;
        private GetAdjustedEstimateViewModel GetAdjustedEstimateViewModel;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateVoucherEvent;
        public override Type ContentType => typeof(View.Budget.Estimate.Division.DivisionDetail);
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted) || HasAdjData;
        public bool IsSaveInputData => ItemInputs.Any(item => item.IsModified);
        public bool IsDuToanDauNam => (Model.ILoaiDuToan.HasValue && BudgetType.YEAR.Equals((BudgetType)Model.ILoaiDuToan.Value));
        public bool IsDieuChinh => (Model.ILoaiDuToan.HasValue && (BudgetType.ADDITIONAL.Equals((BudgetType)Model.ILoaiDuToan.Value) || BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR.Equals((BudgetType)Model.ILoaiDuToan.Value)));
        public bool IsDeleteAll => Items.Any(item => !item.IsModified && item.HasData);
        public int NamLamViec { get; set; }

        private bool _hasAdjData;
        public bool HasAdjData
        {
            get => _hasAdjData;
            set
            {
                SetProperty(ref _hasAdjData, value);
            }
        }

        private ObservableCollection<DtChungTuChiTietModel> _itemImports;
        public ObservableCollection<DtChungTuChiTietModel> ItemImports
        {
            get => _itemImports;
            set => SetProperty(ref _itemImports, value);
        }

        private DtChungTuChiTietModel _selectedItemImport;
        public DtChungTuChiTietModel SelectedItemImport
        {
            get => _selectedItemImport;
            set => SetProperty(ref _selectedItemImport, value);
        }

        public bool HasImportData => _itemImports.Count > 0;

        private ObservableCollection<DtChungTuChiTietModel> _itemInputs;
        public ObservableCollection<DtChungTuChiTietModel> ItemInputs
        {
            get => _itemInputs;
            set => SetProperty(ref _itemInputs, value);
        }

        private DtChungTuChiTietModel _selectedItemInput;
        public DtChungTuChiTietModel SelectedItemInput
        {
            get => _selectedItemInput;
            set => SetProperty(ref _selectedItemInput, value);
        }

        private Visibility _visibilityNSBD;
        public Visibility VisibilityNSBD
        {
            get => _visibilityNSBD;
            set => SetProperty(ref _visibilityNSBD, value);
        }

        private Visibility _visibilityNSSD;
        public Visibility VisibilityNSSD
        {
            get => _visibilityNSSD;
            set => SetProperty(ref _visibilityNSSD, value);
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
                    CalculateData();
                }
            }
        }

        private string _searchLNSMirror;
        public string SearchLNSMirror
        {
            get => _searchLNSMirror;
            set
            {
                if (SetProperty(ref _searchLNSMirror, value))
                {
                    _budgetCatalogItemsMirrorView.Refresh();
                    CalculateDataInput();
                    CalculateDataImport();
                }
            }
        }

        private ObservableCollection<DtChungTuChiTietModel> _budgetCatalogItems;
        public ObservableCollection<DtChungTuChiTietModel> BudgetCatalogItems
        {
            get => _budgetCatalogItems;
            set => SetProperty(ref _budgetCatalogItems, value);
        }

        private DtChungTuChiTietModel _selectedBudgetCatalog;
        public DtChungTuChiTietModel SelectedBudgetCatalog
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
                }
                CalculateTotal();
                IsOpenLnsPopup = false;
                IsOpenLnsPopupMinor = false;
            }
        }

        List<string> MucLucNganSach { get; set; }

        private ObservableCollection<DtChungTuChiTietModel> _budgetCatalogItemsMirror;
        public ObservableCollection<DtChungTuChiTietModel> BudgetCatalogItemsMirror
        {
            get => _budgetCatalogItemsMirror;
            set => SetProperty(ref _budgetCatalogItemsMirror, value);
        }

        private DtChungTuChiTietModel _selectedBudgetCatalogMirror;
        public DtChungTuChiTietModel SelectedBudgetCatalogMirror
        {
            get => _selectedBudgetCatalogMirror;
            set
            {
                if (SetProperty(ref _selectedBudgetCatalogMirror, value))
                {
                    if (_selectedBudgetCatalogMirror != null)
                        SelectedLNSMirror = _selectedBudgetCatalogMirror.SLns;
                    BeForeRefreshMirror();
                    if (_chungTuChiTietItemsImportView != null)
                    {
                        _chungTuChiTietItemsImportView.Refresh();
                    }
                    else if (_chungTuChiTietItemsInputView != null)
                    {
                        _chungTuChiTietItemsInputView.Refresh();
                    }
                }
                CalculateTotal();
                IsOpenLnsPopupMirror = false;
            }
        }

        private bool _isOpenLnsPopupMirror;
        public bool IsOpenLnsPopupMirror
        {
            get => _isOpenLnsPopupMirror;
            set
            {
                SetProperty(ref _isOpenLnsPopupMirror, value);
            }
        }

        public bool IsInit { get; set; } = false;

        private bool _isOpenLnsPopupMinor;
        public bool IsOpenLnsPopupMinor
        {
            get => _isOpenLnsPopupMinor;
            set
            {
                SetProperty(ref _isOpenLnsPopupMinor, value);
            }
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

        private string _selectedLNS;
        public string SelectedLNS
        {
            get => _selectedLNS;
            set => SetProperty(ref _selectedLNS, value);
        }

        private string _selectedLNSMirror;
        public string SelectedLNSMirror
        {
            get => _selectedLNSMirror;
            set => SetProperty(ref _selectedLNSMirror, value);
        }

        private EstimationDetailCriteria _detailFilter;
        public EstimationDetailCriteria DetailFilter
        {
            get => _detailFilter;
            set => SetProperty(ref _detailFilter, value);
        }

        private EstimationDetailCriteria _detailFilterMirror;
        public EstimationDetailCriteria DetailFilterMirror
        {
            get => _detailFilterMirror;
            set => SetProperty(ref _detailFilterMirror, value);
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
                    _chungTuChiTietItemsView.Refresh();
                    CalculateData();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _typeDisplaysMirror;
        public ObservableCollection<ComboboxItem> TypeDisplaysMirror
        {
            get => _typeDisplaysMirror;
            set => SetProperty(ref _typeDisplaysMirror, value);
        }

        private string _typeDisplaysSelectedMirror;
        public string TypeDisplaysSelectedMirror
        {
            get => _typeDisplaysSelectedMirror;
            set
            {
                if (SetProperty(ref _typeDisplaysSelectedMirror, value))
                {
                    if (_chungTuChiTietItemsInputView != null)
                    {
                        BeForeRefreshMirror();
                        _chungTuChiTietItemsInputView.Refresh();
                        CalculateDataInput();
                    }
                    else if (_chungTuChiTietItemsImportView != null)
                    {
                        BeForeRefreshMirror();
                        _chungTuChiTietItemsImportView.Refresh();
                        CalculateDataInput();
                    }
                }
            }
        }

        public List<DtChungTuChiTietModel> ListParentFilter;

        private MLNSColumnDisplay _columnDisplayImport;
        public MLNSColumnDisplay ColumnDisplayImport
        {
            get => _columnDisplayImport;
            set => SetProperty(ref _columnDisplayImport, value);
        }

        private bool _isShowDataImport;
        public bool IsShowDataImport
        {
            get => _isShowDataImport;
            set
            {
                SetProperty(ref _isShowDataImport, value);
                OnPropertyChanged(nameof(IsShowData));
            }
        }
        public bool IsShowData => IsShowDataImport || IsShowDataInput;
        public bool IsShowAdjustedEstimate => (Model.ILoaiDuToan.Equals((int)BudgetType.ADDITIONAL) || Model.ILoaiDuToan.Equals((int)BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR)) && Model.ILoai == 0;
        public bool IsEnableAdjEtm => !Model.BKhoa;
        private bool _isOpenDataSourcePopup;
        public bool IsOpenDataSourcePopup
        {
            get => _isOpenDataSourcePopup;
            set => SetProperty(ref _isOpenDataSourcePopup, value);
        }

        private DivisionColumnVisibility _columnVisibility;
        public DivisionColumnVisibility ColumnVisibility
        {
            get => _columnVisibility;
            set => SetProperty(ref _columnVisibility, value);
        }

        private ObservableCollection<NsMuclucNgansachModel> _configMLNSItems;
        public ObservableCollection<NsMuclucNgansachModel> ConfigMLNSItems
        {
            get => _configMLNSItems;
            set => SetProperty(ref _configMLNSItems, value);
        }

        private bool _isOpenConfigMLNS;
        public bool IsOpenConfigMLNS
        {
            get => _isOpenConfigMLNS;
            set => SetProperty(ref _isOpenConfigMLNS, value);
        }

        private List<ComboboxItem> _chiTietTois;
        public List<ComboboxItem> ChiTietTois
        {
            get => _chiTietTois;
            set => SetProperty(ref _chiTietTois, value);
        }

        private MLNSColumnDisplay _columnDisplayInput;
        public MLNSColumnDisplay ColumnDisplayInput
        {
            get => _columnDisplayInput;
            set => SetProperty(ref _columnDisplayInput, value);
        }

        private bool _isShowDataInput;
        public bool IsShowDataInput
        {
            get => _isShowDataInput;
            set
            {
                SetProperty(ref _isShowDataInput, value);
                OnPropertyChanged(nameof(IsShowData));
            }
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private int _loaiChungTu;
        public int LoaiChungTu
        {
            get => _loaiChungTu;
            set => SetProperty(ref _loaiChungTu, value);
        }

        private DivisionTotalModel _divisionTotal;
        public DivisionTotalModel DivisionTotal
        {
            get => _divisionTotal;
            set => SetProperty(ref _divisionTotal, value);
        }

        public DivisionDetailCanCuViewModel DivisionDetailCanCuViewModel { get; }
        public PrintReportReceiveDivisionViewModel PrintReportReceiveDivisionViewModel { get; set; }
        public PrintReportGeneralReceiveDivisionViewModel PrintReportGeneralReceiveDivisionViewModel { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand SearchCommandMirror { get; }
        public RelayCommand FillDataDauNamCommand { get; }
        public RelayCommand FillDataPhanBoNganhCommand { get; }
        public RelayCommand FillDataDieuChinhCommand { get; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand ResetFilterCommandMirror { get; set; }
        public RelayCommand ItemImportSelectionCommand { get; }
        public RelayCommand ShowImportDataCommand { get; }
        public RelayCommand MapImportDataCommand { get; }
        public RelayCommand MapAllImportDataCommand { get; }
        public RelayCommand DataSourceCommand { get; }
        public RelayCommand ConfigMLNSCommand { get; }
        public RelayCommand ShowInputDataCommand { get; }
        public RelayCommand ItemInputSelectionCommand { get; }
        public RelayCommand MapInputDataCommand { get; }
        public RelayCommand DeleteInputDataCommand { get; }
        public RelayCommand SaveInputDataCommand { get; }
        public RelayCommand GetAdjustedEstimateCommand { get; }
        public RelayCommand BtnPrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand CloseCommand { get; }

        public DivisionDetailViewModel(
            IMapper mapper,
            ILog logger,
            ISessionService sessionService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsDcChungTuChiTietService dcChungTuChiTietService,
            INsDtChungTuService dtChungTuService,
            INsDcChungTuService dcChungTuService,
            ISktSoLieuService sktSoLieuService,
            INsDonViService nsDonViService,
            ILbChungTuService chungTuService,
            INsDtChungTuCanCuService dtChungTuCanCuService,
            INsMucLucNganSachService mlnsService,
            ICauHinhMLNSService cauHinhMLNSService,
            IDanhMucService danhMucService,
            INsDtNhanPhanBoMapService dtNhanPhanBoMapService,
            DanhMucNganhService danhMucNganhService,
            PrintReportReceiveDivisionViewModel printReportReceiveDivisionViewModel,
            PrintReportGeneralReceiveDivisionViewModel printReportGeneralReceiveDivisionViewModel,
            DivisionDetailCanCuViewModel divisionDetailCanCuViewModel,
            GetAdjustedEstimateViewModel getAdjustedEstimateViewModel) : base(danhMucService, sessionService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _dcChungTuChiTietService = dcChungTuChiTietService;
            _dtChungTuService = dtChungTuService;
            _dcChungTuService = dcChungTuService;
            _sktSoLieuService = sktSoLieuService;
            _nsDonViService = nsDonViService;
            _chungTuService = chungTuService;
            _dtChungTuCanCuService = dtChungTuCanCuService;
            _mlnsService = mlnsService;
            _cauHinhMLNSService = cauHinhMLNSService;
            _dtChungTuMapService = dtNhanPhanBoMapService;
            _danhMucNganhService = danhMucNganhService;

            PrintReportReceiveDivisionViewModel = printReportReceiveDivisionViewModel;
            PrintReportGeneralReceiveDivisionViewModel = printReportGeneralReceiveDivisionViewModel;
            DivisionDetailCanCuViewModel = divisionDetailCanCuViewModel;
            DivisionDetailCanCuViewModel.ParentPage = this;
            GetAdjustedEstimateViewModel = getAdjustedEstimateViewModel;

            SearchCommand = new RelayCommand(obj =>
            {
                _isCompareData = false;
                BeForeRefresh();
                _chungTuChiTietItemsView.Refresh();
                CalculateData();
                CalculateTotal();
                if (IsShowDataImport)
                    _chungTuChiTietItemsImportView.Refresh();
                if (IsShowDataInput)
                    _chungTuChiTietItemsInputView.Refresh();
            });

            SearchCommandMirror = new RelayCommand(obj =>
            {
                _isCompareData = false;
                BeForeRefresh();
                _chungTuChiTietItemsView.Refresh();
                CalculateData();
                CalculateTotal();
                if (IsShowDataImport)
                    _chungTuChiTietItemsImportView.Refresh();
                if (IsShowDataInput)
                    _chungTuChiTietItemsInputView.Refresh();
            });

            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            ResetFilterCommandMirror = new RelayCommand(obj => OnResetFilterMirror());
            FillDataDauNamCommand = new RelayCommand(obj => FillDataDauNam());
            FillDataPhanBoNganhCommand = new RelayCommand(obj => FillDataPhanBoNganh());
            FillDataDieuChinhCommand = new RelayCommand(obj => FillDataDieuChinh());
            ItemImportSelectionCommand = new RelayCommand(obj => OnSelectItem(ref _selectedItemImport));
            ShowImportDataCommand = new RelayCommand(obj => OnShowImportData());
            MapImportDataCommand = new RelayCommand(obj => OnMapImportData());
            MapAllImportDataCommand = new RelayCommand(obj => OnMapAllImportData());
            DataSourceCommand = new RelayCommand(obj => IsOpenDataSourcePopup = true);
            ConfigMLNSCommand = new RelayCommand(obj => IsOpenConfigMLNS = true);
            ShowInputDataCommand = new RelayCommand(obj => OnShowInputData());
            ItemInputSelectionCommand = new RelayCommand(obj => OnSelectItem(ref _selectedItemInput));
            MapInputDataCommand = new RelayCommand(obj => OnMapInputData());
            DeleteInputDataCommand = new RelayCommand(obj => OnDeleteInputData());
            SaveInputDataCommand = new RelayCommand(obj => OnSaveInputData());
            GetAdjustedEstimateCommand = new RelayCommand(obj => OnShowAdjustedEstimateVoucher());
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            BtnPrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
        }

        public override void Init()
        {
            base.Init();
            IsInit = false;
            HasAdjData = false;
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            VisibilityNSSD = Visibility.Visible;
            VisibilityNSBD = Visibility.Collapsed;
            ResetConditionSearch();
            if (Model.ILoaiChungTu.HasValue && VoucherType.NSBD_Key.Equals(Model.ILoaiChungTu.ToString()))
            {
                VisibilityNSSD = Visibility.Collapsed;
                VisibilityNSBD = Visibility.Visible;
                LoadNganhByDonVi();
            }

            LoadControlVisibility();
            LoadTypeDisplay();
            LoadTypeDisplayMirror();
            LoadData();
            LoadImportData();
            LoadConfigMLNS();
            LoadInputData();
        }

        private void OpenPrintDialog(object param)
        {
            var divisionPrintType = (DivisionPrintType)((int)param);
            object content;
            if (SelectedItem == null)
                return;
            switch (divisionPrintType)
            {
                case DivisionPrintType.BUDGET_PERIOD:
                case DivisionPrintType.BUDGET_ACCUMULATION:
                case DivisionPrintType.BUDGET_SPECIALIZED:
                    PrintReportReceiveDivisionViewModel.DivisionPrintType = divisionPrintType;
                    PrintReportReceiveDivisionViewModel.DivisionModel = Model;
                    PrintReportReceiveDivisionViewModel.LoaiChungTu = LoaiChungTu;
                    PrintReportReceiveDivisionViewModel.Init();
                    content = new PrintReportReceiveDivision
                    {
                        DataContext = PrintReportReceiveDivisionViewModel
                    };
                    break;
                case DivisionPrintType.SYNTHESIS_BUDGET_SELF:
                case DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS:
                case DivisionPrintType.SYNTHESIS_BUDGET_COMMON:
                    PrintReportGeneralReceiveDivisionViewModel.DivisionPrintType = divisionPrintType;
                    PrintReportGeneralReceiveDivisionViewModel.DivisionModel = Model;
                    PrintReportGeneralReceiveDivisionViewModel.LoaiChungTu = LoaiChungTu;
                    PrintReportGeneralReceiveDivisionViewModel.Init();
                    content = new PrintReportGeneralReceiveDivision
                    {
                        DataContext = PrintReportGeneralReceiveDivisionViewModel
                    };
                    break;
                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, "DivisionDetailDialog");
            }
        }

        private void LoadControlVisibility()
        {
            _listMLNS = _mlnsService.FindByListLnsDonVi(Model.SDslns, _sessionService.Current.YearOfWork).ToList();
            _columnVisibility = new DivisionColumnVisibility();
            _columnVisibility.IsDisplayTuChi = _listMLNS.Any(x => x.BTuChi);
            _columnVisibility.IsDisplayHienVat = _listMLNS.Any(x => x.BHienVat);
            _columnVisibility.IsDisplayDuPhong = _listMLNS.Any(x => x.BDuPhong);
            _columnVisibility.IsDisplayHangMua = _listMLNS.Any(x => x.BHangMua);
            _columnVisibility.IsDisplayHangNhap = _listMLNS.Any(x => x.BHangNhap);
            _columnVisibility.IsDisplayPhanCap = _listMLNS.Any(x => x.BPhanCap);
            OnPropertyChanged(nameof(ColumnVisibility));
        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Có dữ liệu" },
                new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Tất cả" }
            };
            TypeDisplaysSelected = TypeDisplay.TAT_CA;
        }

        private void LoadTypeDisplayMirror()
        {
            TypeDisplaysMirror = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Có dữ liệu" },
                new ComboboxItem { ValueItem = TypeDisplay.DU_LIEU_CHUA_CHUYEN_DOI, DisplayItem = "Dữ liệu chưa chuyển đổi" },
                new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Tất cả" }
            };
            TypeDisplaysSelectedMirror = TypeDisplay.TAT_CA;
        }

        private void LoadConfigMLNS()
        {
            AuthenticationInfo authenticationInfo = _mapper.Map<AuthenticationInfo>(_sessionService.Current);
            List<NsMucLucNganSach> listMLNS = _cauHinhMLNSService.FindAll(authenticationInfo).ToList();
            //List<string> listLNSOfModel = Model.SDslns.Split(",").ToList();
            var mlnsSetting = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(this.ChiTietToi));
            //listMLNS = listMLNS.Where(x => listLNSOfModel.Contains(x.Lns)).Select(x => { x.ChiTietToi = this.ChiTietToi; return x; }).ToList();
            _configMLNSItems = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMLNS);
            _configMLNSItems.ForAll(x =>
            {
                x.MLNSReportSetting = mlnsSetting;
                if (mlnsSetting.Count > 0)
                    x.SelectedMLNSReportSetting = mlnsSetting.Last().ValueItem;
            });
            OnPropertyChanged(nameof(_configMLNSItems));
            foreach (var item in ConfigMLNSItems)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(NsMuclucNgansachModel.SelectedMLNSReportSetting))
                    {
                        LoadInputData();
                    }
                };
            }
        }

        private void LoadNganhByDonVi()
        {
            List<DonVi> listNsDonVi = new List<DonVi>();
            listNsDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();
            listNsDonVi = listNsDonVi.Where(x => x.BCoNSNganh && (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT)).ToList();
            var authenticationInfo = _mapper.Map<AuthenticationInfo>(_sessionService.Current);
            _listDanhMucNganh = _danhMucNganhService.FindAll(authenticationInfo).Where(x => x.SGiaTri.Split(",").Any(y => listNsDonVi.Select(y => y.IIDMaDonVi).Contains(y))).ToList();
        }

        private void LoadInputData()
        {
            if (!HasImportData)
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    MucLucNganSach = _mlnsService.FindAll(_sessionService.Current.YearOfWork).Where(n => n.BHangChaDuToan.HasValue && !n.BHangChaDuToan.Value).Select(n => n.XauNoiMa).ToList();
                    _searchCondition.IDuLieuNhan = false;
                    _listChungTuChiTietDuLieuNhap = _dtChungTuChiTietService.FindByCondition(_searchCondition, "sp_dt_chungtu_chitiet_dulieunhap").ToList();
                    if (_listDanhMucNganh != null && _listDanhMucNganh.Count > 0)
                        _listChungTuChiTietDuLieuNhap = _listChungTuChiTietDuLieuNhap.Where(x => x.BHangCha || (!x.BHangCha && _listDanhMucNganh.Select(x => x.IIDMaDanhMuc).Contains(x.SNg))).ToList();
                    ProcessDynamicMLNS();
                }, (s, e) =>
                {
                    _itemInputs = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(_listChungTuChiTietDynamic);
                    OnPropertyChanged(nameof(ItemInputs));
                    CalculateDataInput();
                    _chungTuChiTietItemsInputView = CollectionViewSource.GetDefaultView(ItemInputs);
                    _chungTuChiTietItemsInputView.Filter = ItemsViewFilterMirror;
                });
            }
        }

        private void ResetConditionSearch()
        {
            _isShowDataImport = false;
            _isShowDataInput = false;
            _searchLNS = string.Empty;
            _selectedLNS = string.Empty;
            _searchLNSMirror = string.Empty;
            _selectedLNSMirror = string.Empty;
            xnmConcatenation = string.Empty;
            xnmConcatenationMirror = string.Empty;
            _filterResult = new HashSet<DtChungTuChiTietModel>();
            _listDanhMucNganh = new List<DanhMuc>();
            _itemImports = new ObservableCollection<DtChungTuChiTietModel>();
            _itemInputs = new ObservableCollection<DtChungTuChiTietModel>();
            Items = new ObservableCollection<DtChungTuChiTietModel>();
            DetailFilter = new EstimationDetailCriteria();
            DetailFilterMirror = new EstimationDetailCriteria();
            _searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Model.Id,
                LNS = Model.SDslns,
                YearOfWork = Model.INamLamViec,
                YearOfBudget = Model.INamNganSach,
                BudgetSource = Model.IIdMaNguonNganSach,
                VoucherDate = Model.DNgayChungTu,
                UserName = _sessionService.Current.Principal
            };
            OnPropertyChanged(nameof(ItemImports));
            OnPropertyChanged(nameof(ItemInputs));
            OnPropertyChanged(nameof(SearchLNS));
            OnPropertyChanged(nameof(SelectedLNS));
        }

        private void LoadLNSIndexCondition()
        {
            List<DtChungTuChiTietModel> listLNS = Items.Where(x => string.IsNullOrEmpty(x.SL) &&
                string.IsNullOrEmpty(x.SK) &&
                string.IsNullOrEmpty(x.SM) &&
                string.IsNullOrEmpty(x.STm) &&
                string.IsNullOrEmpty(x.STtm) &&
                string.IsNullOrEmpty(x.SNg) &&
                string.IsNullOrEmpty(x.STng)).ToList();
            listLNS.Insert(0, new DtChungTuChiTietModel
            {
                SLns = string.Empty,
                SMoTa = "-- TẤT CẢ --"
            });
            BudgetCatalogItems = new ObservableCollection<DtChungTuChiTietModel>(listLNS);
            _budgetCatalogItemsView = CollectionViewSource.GetDefaultView(BudgetCatalogItems);
            _budgetCatalogItemsView.Filter = BudgetCatalogItemsFilter;
        }

        private void LoadLNSIndexConditionMirror()
        {
            if (ItemInputs.Any())
            {
                List<DtChungTuChiTietModel> listLNS = ItemInputs.Where(x => string.IsNullOrEmpty(x.SL) &&
                string.IsNullOrEmpty(x.SK) &&
                string.IsNullOrEmpty(x.SM) &&
                string.IsNullOrEmpty(x.STm) &&
                string.IsNullOrEmpty(x.STtm) &&
                string.IsNullOrEmpty(x.SNg) &&
                string.IsNullOrEmpty(x.STng)).ToList();
                listLNS.Insert(0, new DtChungTuChiTietModel
                {
                    SLns = string.Empty,
                    SMoTa = "-- TẤT CẢ --"
                });
                BudgetCatalogItemsMirror = new ObservableCollection<DtChungTuChiTietModel>(listLNS);
                _budgetCatalogItemsMirrorView = CollectionViewSource.GetDefaultView(BudgetCatalogItemsMirror);
                _budgetCatalogItemsMirrorView.Filter = BudgetCatalogItemsMirrorFilter;
            }
            else
            {
                List<DtChungTuChiTietModel> listLNS = ItemImports.Where(x => string.IsNullOrEmpty(x.SL) &&
                string.IsNullOrEmpty(x.SK) &&
                string.IsNullOrEmpty(x.SM) &&
                string.IsNullOrEmpty(x.STm) &&
                string.IsNullOrEmpty(x.STtm) &&
                string.IsNullOrEmpty(x.SNg) &&
                string.IsNullOrEmpty(x.STng)).ToList();
                listLNS.Insert(0, new DtChungTuChiTietModel
                {
                    SLns = string.Empty,
                    SMoTa = "-- TẤT CẢ --"
                });
                BudgetCatalogItemsMirror = new ObservableCollection<DtChungTuChiTietModel>(listLNS);
                _budgetCatalogItemsMirrorView = CollectionViewSource.GetDefaultView(BudgetCatalogItemsMirror);
                _budgetCatalogItemsMirrorView.Filter = BudgetCatalogItemsMirrorFilter;
            }

        }

        public override void LoadData(params object[] args)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                _isCompareData = false;
                _searchCondition.IDuLieuNhan = null;
                _listChungTuChiTiet = _dtChungTuChiTietService.FindByCondition(_searchCondition).ToList();
                if (_listDanhMucNganh != null && _listDanhMucNganh.Count > 0)
                {
                    var listXauNoiMa = StringUtils.GetListXauNoiMaParent(_listChungTuChiTiet.Where(x => !x.BHangCha && _listDanhMucNganh.Select(x => x.IIDMaDanhMuc).Contains(x.SNg)).Select(x => x.SXauNoiMa).ToList());
                    _listChungTuChiTiet = _listChungTuChiTiet.Where(x => listXauNoiMa.Contains(x.SXauNoiMa)).ToList();
                }
            }, (s, e) =>
            {
                Items = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(_listChungTuChiTiet);
                // Using collection view
                _chungTuChiTietItemsView = CollectionViewSource.GetDefaultView(Items);
                _chungTuChiTietItemsView.Filter = ItemsViewFilter;

                SettingEditable();
                CalculateData();

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
                IsLoading = false;
                LoadLNSIndexCondition();
                LoadLNSIndexConditionMirror();
            });
        }

        private void LoadImportData()
        {
            // Xử lý khi có dữ liệu nhận (dữ liệu từ import)
            _searchCondition.IDuLieuNhan = true;
            IEnumerable<NsDtChungTuChiTietQuery> _chungTuChiTietDuLieuNhan = _dtChungTuChiTietService.FindByCondition(_searchCondition, "sp_dt_chungtu_chitiet_dulieunhan");
            if (_listDanhMucNganh != null && _listDanhMucNganh.Count > 0)
                _chungTuChiTietDuLieuNhan = _chungTuChiTietDuLieuNhan.Where(x => x.BHangCha || (!x.BHangCha && _listDanhMucNganh.Select(x => x.IIDMaDanhMuc).Contains(x.SNg))).ToList();
            _itemImports = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(_chungTuChiTietDuLieuNhan);
            OnPropertyChanged(nameof(ItemImports));
            OnPropertyChanged(nameof(HasImportData));
            if (ItemImports.Count > 0)
            {
                MucLucNganSach = _mlnsService.FindAll(_sessionService.Current.YearOfWork).Where(n => n.BHangChaDuToan.HasValue && !n.BHangChaDuToan.Value).Select(n => n.XauNoiMa).ToList();
                CalculateDataImport();
                SetColumnImportVisibility();
                _chungTuChiTietItemsImportView = CollectionViewSource.GetDefaultView(ItemImports);
                _chungTuChiTietItemsImportView.Filter = ItemsViewFilterMirror;
            }
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FRutKBNN = 0;
                    x.FHienVat = 0;
                    x.FHangNhap = 0;
                    x.FHangMua = 0;
                    x.FPhanCap = 0;
                    x.FDuPhong = 0;
                    return x;
                }).ToList();

            foreach (var item in Items.Where(x => x.IsEditable && x.IsFilter && (x.FTuChi != 0 || x.FRutKBNN != 0 || x.FHienVat != 0 || x.FHangNhap != 0 || x.FHangMua != 0 || x.FDuPhong != 0 || x.FPhanCap != 0)))
            {
                CalculateParent(item, item);
            }

            CalculateTotal();
        }

        private void SettingEditable()
        {
            foreach (var item in Items.Where(x => !x.IsHangCha))
            {
                var mlns = _listMLNS.Where(x => x.Lns == item.SLns).FirstOrDefault();
                if (mlns != null)
                {
                    item.IsEditTuChi = mlns.BTuChi;
                    item.IsEditHienVat = mlns.BHienVat;
                    item.IsEditHangNhap = mlns.BHangNhap;
                    item.IsEditHangMua = mlns.BHangMua;
                    item.IsEditDuPhong = mlns.BDuPhong;
                    item.IsEditPhanCap = mlns.BPhanCap;
                }
            }
        }

        private void CalculateParent(DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem)
        {
            var parrentItem = Items.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FTuChi += seftItem.FTuChi;
            parrentItem.FRutKBNN += seftItem.FRutKBNN;
            parrentItem.FHienVat += seftItem.FHienVat;
            parrentItem.FHangNhap += seftItem.FHangNhap;
            parrentItem.FHangMua += seftItem.FHangMua;
            parrentItem.FPhanCap += seftItem.FPhanCap;
            parrentItem.FDuPhong += seftItem.FDuPhong;
            CalculateParent(parrentItem, seftItem);
        }

        private bool BudgetCatalogItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is DtChungTuChiTietModel item && (item.SLns.StartsWith(_searchLNS, StringComparison.Ordinal) || item.SMoTa.StartsWith(_searchLNS, StringComparison.Ordinal));
        }

        private bool BudgetCatalogItemsMirrorFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNSMirror))
            {
                return true;
            }
            return obj is DtChungTuChiTietModel item && (item.SLns.StartsWith(_searchLNSMirror, StringComparison.Ordinal) || item.SMoTa.StartsWith(_searchLNSMirror, StringComparison.Ordinal));
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (DtChungTuChiTietModel)obj;
            result = ChungTuChiTietItemsViewFilter(item);
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

        private bool ItemsViewFilterMirror(object obj)
        {
            bool result = true;
            var item = (DtChungTuChiTietModel)obj;
            result = ChungTuChiTietItemsViewMirrorFilter(item);
            if (!result && item.IsHangCha)
            {
                if (string.IsNullOrEmpty(item.SL))
                    result = xnmConcatenationMirror.StartsWith(item.SLns);
                else result = xnmConcatenationMirror.Contains(item.SXauNoiMa);
            }
            if (result)
                item.IsFilter = result;
            return result;
        }

        private bool ChungTuChiTietItemsViewMirrorFilter(object obj)
        {
            bool result = true;
            var item = (DtChungTuChiTietModel)obj;
            if (!string.IsNullOrEmpty(SelectedLNSMirror))
                result = result && item.SLns.ToLower().StartsWith(SelectedLNSMirror.ToLower());
            if (!string.IsNullOrEmpty(DetailFilterMirror.L))
                result = result && item.SL.ToLower().StartsWith(DetailFilterMirror.L.ToLower());
            if (!string.IsNullOrEmpty(DetailFilterMirror.K))
                result = result && item.SK.ToLower().StartsWith(DetailFilterMirror.K.ToLower());
            if (!string.IsNullOrEmpty(DetailFilterMirror.M))
                result = result && item.SM.ToLower().StartsWith(DetailFilterMirror.M.ToLower());
            if (!string.IsNullOrEmpty(DetailFilterMirror.TM))
                result = result && item.STm.ToLower().StartsWith(DetailFilterMirror.TM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilterMirror.TTM))
                result = result && item.STtm.ToLower().StartsWith(DetailFilterMirror.TTM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilterMirror.NG))
                result = result && item.SNg.ToLower().StartsWith(DetailFilterMirror.NG.ToLower());
            if (!string.IsNullOrEmpty(DetailFilterMirror.TNG))
                result = result && item.STng.ToLower().StartsWith(DetailFilterMirror.TNG.ToLower());
            if (!string.IsNullOrEmpty(DetailFilterMirror.TNG1))
                result = result && item.STng1.ToLower().StartsWith(DetailFilterMirror.TNG1.ToLower());
            if (!string.IsNullOrEmpty(DetailFilterMirror.TNG2))
                result = result && item.STng2.ToLower().StartsWith(DetailFilterMirror.TNG2.ToLower());
            if (!string.IsNullOrEmpty(DetailFilterMirror.TNG3))
                result = result && item.STng3.ToLower().StartsWith(DetailFilterMirror.TNG3.ToLower());

            if (!string.IsNullOrEmpty(TypeDisplaysSelectedMirror))
            {
                bool hasInputData = item.FTuChi != 0 || item.FRutKBNN != 0 || item.FHienVat != 0 || item.FDuPhong != 0 || item.FHangNhap != 0 || item.FHangMua != 0 || item.FPhanCap != 0;
                if (TypeDisplaysSelectedMirror == TypeDisplay.DA_NHAP)
                    result = result && (hasInputData || (item.IsModified && (item.IIdDtchungTu == Guid.Empty || item.IIdDtchungTu == null) && !item.IsDeleted));
                else if (TypeDisplaysSelectedMirror == TypeDisplay.CO_DU_LIEU)
                    result = result && (hasInputData || (item.IsModified && (item.IIdDtchungTu == Guid.Empty || item.IIdDtchungTu == null) && !item.IsDeleted));
                else if (TypeDisplaysSelectedMirror == TypeDisplay.DU_LIEU_CHUA_CHUYEN_DOI)
                    result = result && !item.IsHangCha && item.IDuLieuNhan == 1 && !MucLucNganSach.Contains(item.SXauNoiMa) && !Items.Where(x => x.HasData).Select(y => y.SXauNoiMa).Contains(item.SXauNoiMa);
            }

            if (_isCompareData)
            {
                result = result && _listCompare.Any(x => x.SXauNoiMa == item.SXauNoiMa);
            }

            item.IsFilter = result;
            return result;
        }

        private bool ChungTuChiTietItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (DtChungTuChiTietModel)obj;
            if (!string.IsNullOrEmpty(SelectedLNS))
                result = result && item.SLns.ToLower().StartsWith(SelectedLNS.ToLower());
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

            if (!string.IsNullOrEmpty(TypeDisplaysSelected))
            {
                bool hasInputData = item.FTuChi != 0 || item.FRutKBNN != 0 || item.FHienVat != 0 || item.FDuPhong != 0 || item.FHangNhap != 0 || item.FHangMua != 0 || item.FPhanCap != 0;
                if (TypeDisplaysSelected == TypeDisplay.DA_NHAP)
                    result = result && (hasInputData || (item.IsModified && (item.IIdDtchungTu == Guid.Empty || item.IIdDtchungTu == null) && !item.IsDeleted));
                else if (TypeDisplaysSelected == TypeDisplay.CO_DU_LIEU)
                    result = result && (hasInputData || (item.IsModified && (item.IIdDtchungTu == Guid.Empty || item.IIdDtchungTu == null) && !item.IsDeleted));
            }

            if (_isCompareData)
            {
                result = result && _listCompare.Any(x => x.SXauNoiMa == item.SXauNoiMa);
            }

            item.IsFilter = result;
            return result;
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => ChungTuChiTietItemsViewFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
            CalculateData();
        }

        private void BeForeRefreshMirror()
        {
            if (IsShowDataInput)
            {
                _filterResult = ItemInputs.Where(item => ChungTuChiTietItemsViewMirrorFilter(item)).Where(item => !item.IsHangCha).ToList();
                xnmConcatenationMirror = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
                CalculateDataInput();
            }
            else if (IsShowDataImport)
            {
                _filterResult = ItemImports.Where(item => ChungTuChiTietItemsViewMirrorFilter(item)).Where(item => !item.IsHangCha).ToList();
                xnmConcatenationMirror = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
                CalculateDataImport();
            }

        }

        protected override bool CanDelete(object obj)
        {
            return !Model.BKhoa;
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                CalculateData();
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        protected override void OnLockUnLock()
        {
            if (Model.BKhoa)
            {
                //chỉ có đơn vị cha mới được mở khóa chứng từ
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBox.Show(Resources.MsgRoleUnlock, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                ////kiểm tra chứng từ đã được phân bổ chưa, nếu đã được phân bổ thì không cho mở khóa
                //List<NsDtNhanPhanBoMap> dtNhanPhanBoMaps = _dtChungTuMapService.FindByIdNhanDuToan(Model.Id).ToList();
                //if (dtNhanPhanBoMaps.Count() > 0)
                //{
                //    MessageBox.Show(Resources.AlertUnlockDivisionVoucher, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                //    return;
                //}
            }
            else
            {
                //chỉ có người tạo chứng từ mới được khóa chứng từ
                if (Model.SNguoiTao != _sessionInfo.Principal)
                {
                    MessageBox.Show(string.Format(Resources.MsgRoleLock, SelectedItem.SNguoiTao), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            var msgConfirm = Model.BKhoa ? Resources.LockChungTu : Resources.UnlockChungTu;
            var msgDone = Model.BKhoa ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            var result = MessageBox.Show(msgConfirm, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var rs = _dtChungTuService.LockOrUnLock(Model.Id, !Model.BKhoa);
                if (rs == DBContextSaveChangeState.SUCCESS)
                {
                    Model.BKhoa = !Model.BKhoa;
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IsDeleteAll));
                }
                MessageBox.Show(msgDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                // refresh dữ liệu ở màn index
                DataChangedEventHandler handler = UpdateVoucherEvent;
                if (handler != null)
                {
                    handler(Model, new EventArgs());
                }
            }
        }

        protected override void OnAdd()
        {
            if (SelectedItem != null)
            {
                int currentRow = Items.IndexOf(SelectedItem);
                int targetRow = Items.ToList().FindIndex(currentRow, x => x.IsEditable);
                if (targetRow > -1)
                {
                    DtChungTuChiTietModel sourceItem = Items.ElementAt(targetRow);
                    DtChungTuChiTietModel targetItem = ObjectCopier.Clone(sourceItem);

                    targetItem.Id = Guid.NewGuid();
                    targetItem.IIdDtchungTu = null;
                    targetItem.FTuChi = 0;
                    targetItem.FRutKBNN = 0;
                    targetItem.FHienVat = 0;
                    targetItem.FHangNhap = 0;
                    targetItem.FHangMua = 0;
                    targetItem.FPhanCap = 0;
                    targetItem.FDuPhong = 0;
                    targetItem.SGhiChu = null;
                    targetItem.IsModified = true;
                    targetItem.PropertyChanged += DetailModel_PropertyChanged;

                    Items.Insert(targetRow + 1, targetItem);
                    OnPropertyChanged(nameof(Items));
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IsDeleteAll));
                }
            }
        }

        public override void OnSave()
        {
            var session = _sessionService.Current;
            List<DtChungTuChiTietModel> listChungTuChiTietAdd = Items.Where(x => x.IsEditable && x.IsModified && x.IIdDtchungTu == null).ToList();
            listChungTuChiTietAdd = listChungTuChiTietAdd.Where(n => n.FTonKho != 0 || n.FTuChi != 0 || n.FRutKBNN != 0 | n.FDuPhong != 0 || n.FHangMua != 0 || n.FHangNhap != 0 || n.FPhanCap != 0 || n.FHienVat != 0).ToList();
            List<DtChungTuChiTietModel> listChungTuChiTietUpdate = Items.Where(x => x.IsEditable && x.IsModified && x.IIdDtchungTu != null).ToList();
            List<DtChungTuChiTietModel> listChungTuChiTietDelete = Items.Where(x => x.IsDeleted && x.IIdDtchungTu != null).ToList();

            // Thêm mới chứng từ chi tiết
            if (listChungTuChiTietAdd.Count > 0)
            {
                listChungTuChiTietAdd = listChungTuChiTietAdd.Select(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.IIdDtchungTu = Model.Id;
                    x.IIdMaDonVi = session.IdDonVi;
                    x.STenDonVi = session.TenDonVi;
                    x.INamNganSach = Model.INamNganSach;
                    x.INamLamViec = Model.INamLamViec;
                    x.IIdMaNguonNganSach = Model.IIdMaNguonNganSach;
                    x.IPhanCap = NSDuToan.IPHANCAP_NHAN_PHANBO;
                    x.SNguoiTao = session.Principal;
                    x.DNgayTao = DateTime.Now;
                    return x;
                }).ToList();

                List<NsDtChungTuChiTiet> listChungTuChiTiets = _mapper.Map<List<NsDtChungTuChiTiet>>(listChungTuChiTietAdd);
                _dtChungTuChiTietService.AddRange(listChungTuChiTiets);
            }

            // Cập nhật chứng từ chi tiết
            if (listChungTuChiTietUpdate.Count > 0)
            {
                foreach (var item in listChungTuChiTietUpdate.Where(n => n.FTonKho != 0 || n.FTuChi != 0 || n.FRutKBNN != 0 || n.FDuPhong != 0 || n.FHangMua != 0 || n.FHangNhap != 0 || n.FPhanCap != 0 || n.FHienVat != 0))
                //foreach (var item in listChungTuChiTietUpdate)
                {
                    NsDtChungTuChiTiet chungTuChiTiet = _dtChungTuChiTietService.FindById(item.Id);
                    _mapper.Map(item, chungTuChiTiet);
                    _dtChungTuChiTietService.Update(chungTuChiTiet);
                }
                _dtChungTuChiTietService.DeleteByIds(listChungTuChiTietUpdate.Where(n => n.FTonKho == 0 && n.FTuChi == 0 && n.FRutKBNN == 0 && n.FDuPhong == 0 && n.FHangMua == 0 && n.FHangNhap == 0 && n.FPhanCap == 0 && n.FHienVat == 0).Select(x => x.Id.ToString()));
            }

            // Xóa chứng từ chi tiết
            if (listChungTuChiTietDelete.Count > 0)
            {
                foreach (var item in listChungTuChiTietDelete)
                {
                    _dtChungTuChiTietService.Delete(item.Id);

                    ResetItemValue(item);

                    // Reset flag
                    item.IsModified = false;
                    item.IsDeleted = false;
                    item.IIdDtchungTu = null;
                }
            }

            if (DivisionDetailCanCuViewModel.Items != null)
            {
                var listCanCu = DivisionDetailCanCuViewModel.Items.Where(x => x.IsSelected).ToList();
                if (listCanCu != null && listCanCu.Count > 0)
                {
                    // Update chứng từ căn cứ
                    _dtChungTuCanCuService.Update(Model.Id, listCanCu.Select(x => x.Id).ToList(), session.Principal);
                }
            }
            // Refresh state form
            Items.Select(x => { x.IsModified = false; x.IsDeleted = false; return x; }).ToList();

            CalculateData();

            //Cập nhật thông tin chứng từ
            UpdateChungTu();

            MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            //refresh dữ liệu ở màn index
            DataChangedEventHandler handler = UpdateVoucherEvent;
            if (handler != null)
            {
                handler(Model, new EventArgs());
            }
            HasAdjData = false;
            OnPropertyChanged(nameof(IsSaveData));
        }

        protected override void OnRefresh()
        {
            if (IsSaveData)
            {
                var result = MessageBox.Show(Resources.ConfirmReloadData, Resources.ConfirmTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
                if (result == MessageBoxResult.Cancel)
                    return;
                else if (result == MessageBoxResult.Yes)
                    OnSave();
            }
            LoadData();
        }

        private void OnResetFilter()
        {
            DetailFilter = new EstimationDetailCriteria();
            SelectedLNS = string.Empty;
            if (_chungTuChiTietItemsView != null)
            {
                BeForeRefresh();
                _chungTuChiTietItemsView.Refresh();
                CalculateData();
            }
        }

        private void OnResetFilterMirror()
        {
            DetailFilterMirror = new EstimationDetailCriteria();
            SelectedLNSMirror = string.Empty;
            if (_chungTuChiTietItemsImportView != null)
            {
                BeForeRefreshMirror();
                _chungTuChiTietItemsImportView.Refresh();
                CalculateDataImport();
            }
            if (_chungTuChiTietItemsInputView != null)
            {
                BeForeRefreshMirror();
                _chungTuChiTietItemsInputView.Refresh();
                CalculateDataInput();
            }
        }


        private void CalculateTotal()
        {
            DivisionTotal = new DivisionTotalModel();
            var listChildren = Items.Where(x => x.IsEditable && x.IsFilter).ToList();
            foreach (var item in listChildren)
            {
                DivisionTotal.FTongTuChi += item.FTuChi;
                DivisionTotal.FTongRutKBNN += item.FRutKBNN;
                DivisionTotal.FTongHienVat += item.FHienVat;
                DivisionTotal.FTongHangNhap += item.FHangNhap;
                DivisionTotal.FTongHangMua += item.FHangMua;
                DivisionTotal.FTongPhanCap += item.FPhanCap;
                DivisionTotal.FTongDuPhong += item.FDuPhong;
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(DtChungTuChiTietModel.FTuChi)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FRutKBNN)
                || args.PropertyName == nameof(DtChungTuChiTietModel.SGhiChu)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FHienVat)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FHangNhap)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FHangMua)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FPhanCap)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FDuPhong))
            {
                DtChungTuChiTietModel item = (DtChungTuChiTietModel)sender;
                item.IsModified = true;
                if ((args.PropertyName == nameof(DtChungTuChiTietModel.FTuChi)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FRutKBNN)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FHienVat)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FHangNhap)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FHangMua)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FPhanCap)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FDuPhong)) && !IsInit)
                {
                    if (!_isFillData)
                    {
                        IsInit = true;
                        CalculateData();
                        IsInit = false;
                    }
                }
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void FillDataDauNam()
        {
            if (!Model.ILoaiDuToan.HasValue || !BudgetType.YEAR.Equals((BudgetType)Model.ILoaiDuToan.Value))
            {
                return;
            }

            if (!Model.ILoaiChungTu.HasValue)
            {
                return;
            }

            var listDonViPlanBeginYearQuery = _nsDonViService.FindPlanBeginYearByConditon(_sessionService.Current.YearOfWork,
                _sessionService.Current.YearOfBudget,
                _sessionService.Current.Budget, Model.ILoaiChungTu.Value.ToString(), 0, _sessionService.Current.Principal);

            var listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).FirstOrDefault(n => n.Loai.Equals(LoaiDonVi.ROOT));

            var searchPredicate = new EstimationVoucherDetailCriteria
            {
                YearOfWork = Model.INamLamViec,
                YearOfBudget = Model.INamNganSach,
                BudgetSource = Model.IIdMaNguonNganSach,
                ILoai = 3,
                LoaiChungTu = Model.ILoaiChungTu.Value,
                IdDonVi = string.Join(StringUtils.COMMA, listDonViPlanBeginYearQuery.Select(x => x.Id_DonVi))
            };

            IEnumerable<SktSoLieuChiTietMLNSBudget> listDataDetail = _sktSoLieuService.FindForFillBudget(searchPredicate, "sp_skt_so_lieu_chi_tiet_mlns_for_fill_budget").Where(m => m.IdDonVi == listDonVi.IIDMaDonVi).ToList();
            if (!listDataDetail.Any())
            {
                MessageBox.Show(string.Format(Resources.YearPlanSumaryHasNoData), Resources.ConfirmTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dataGroupByMlns = listDataDetail
                .GroupBy(skt => skt.MlnsId.ToString())
                .ToDictionary(g => g.Key, g => g.ToList());
            _isFillData = true;
            Items.Where(item => item.IsEditable).ForAll(item =>
            {
                var listDataByMlns = dataGroupByMlns.GetValueOrDefault(item.IIdMlns.ToString(), new List<SktSoLieuChiTietMLNSBudget>());
                item.FTuChi = listDataByMlns.Sum(x => x.TuChi);
                item.FRutKBNN = listDataByMlns.Sum(x => x.RutKBNN);
                item.FHienVat = listDataByMlns.Sum(x => x.HienVat);
                item.FHangNhap = listDataByMlns.Sum(x => x.HangNhap);
                item.FHangMua = listDataByMlns.Sum(x => x.HangMua);
                item.FPhanCap = listDataByMlns.Sum(x => x.PhanCap);
                item.FDuPhong = listDataByMlns.Sum(x => x.DuPhong.HasValue ? x.DuPhong.Value : 0);
            });
            _isFillData = false;
            CalculateData();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            BeForeRefresh();
            _chungTuChiTietItemsView.Refresh();
        }

        private void FillDataPhanBoNganh()
        {
            DivisionDetailCanCuViewModel.Model = this.Model;
            DivisionDetailCanCuViewModel.Init();
            DivisionDetailCanCuViewModel.SavedAction = OnSavePhanBoNganh;
            DivisionDetailCanCuViewModel.ShowDialogHost("DivisionDetailDialog");
        }

        private void OnSavePhanBoNganh(object obj)
        {
            try
            {
                if (obj != null)
                {
                    var items = (List<LbChungTuCanCuModel>)obj;
                    int namLamViec = _sessionService.Current.YearOfWork;
                    string idDonVi = _sessionService.Current.IdDonVi;
                    string idsChungTu = String.Join(",", items.Select(x => x.Id.ToString()));
                    int loaiChungTu = Model.ILoaiChungTu.Value;
                    var data = _chungTuService.GetCanCuDuToanData(namLamViec, idsChungTu, loaiChungTu, idDonVi).ToList();

                    // Mapper data to mlns
                    foreach (var item in Items)
                    {
                        var val = data.FirstOrDefault(x => item.SXauNoiMa.Equals(x.XauNoiMa));
                        if (val != null)
                        {
                            // NSSD
                            item.FTuChi = val.TuChiTaiNganh + val.TuChiTaiDonVi;
                            item.FDuPhong = val.DuPhong;

                            // NSBD
                            item.FHangNhap = val.HangNhap;
                            item.FHangMua = val.HangMua;
                            item.FPhanCap = val.PhanCap;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw;
            }
        }

        private void FillDataDieuChinh()
        {
            EstimationVoucherCriteria condition = new EstimationVoucherCriteria
            {
                YearOfWork = _sessionInfo.YearOfWork,
                YearOfBudget = _sessionInfo.YearOfBudget,
                BudgetSource = _sessionInfo.Budget,
                UserName = _sessionInfo.Principal,
                VoucherTypes = Model.ILoaiChungTu.GetValueOrDefault().ToString()
            };
            var listChungTuDieuChinhTongHop = _dcChungTuService.FindByCondition(condition).Where(x => !string.IsNullOrEmpty(x.STongHop)).ToList();
            _isFillData = true;
            if (listChungTuDieuChinhTongHop.Count > 0)
            {
                foreach (var chungTu in listChungTuDieuChinhTongHop)
                {
                    var listLNS = chungTu.SDslns.Split(",").ToList();
                    if (listLNS.Any(x => Model.SDslns.Split(",").Contains(x)))
                    {
                        EstimationVoucherDetailCriteria conditionDetail = new EstimationVoucherDetailCriteria
                        {
                            VoucherId = chungTu.Id,
                            LNS = chungTu.SDslns,
                            YearOfWork = chungTu.INamLamViec,
                            YearOfBudget = chungTu.INamNganSach,
                            BudgetSource = chungTu.IIdMaNguonNganSach,
                            LoaiDuKien = chungTu.ILoaiDuKien,
                            LoaiChungTu = chungTu.ILoaiChungTu,
                            VoucherDate = chungTu.DNgayChungTu,
                            UserName = _sessionInfo.Principal
                        };
                        List<string> soChungTus = chungTu.STongHop.Split(",").ToList();
                        var predicate = PredicateBuilder.True<NsDcChungTu>();
                        predicate = predicate.And(x => soChungTus.Contains(x.SSoChungTu));
                        List<NsDcChungTu> chungTus = _dcChungTuService.FindByCondition(predicate).ToList();
                        conditionDetail.IdDonVi = string.Join(",", chungTus.Select(x => x.IIdMaDonVi).ToList());
                        var listChungTuDieuChinhChiTiet = _dcChungTuChiTietService.FindByCondition(conditionDetail).ToList();
                        foreach (var dieuChinh in listChungTuDieuChinhChiTiet)
                        {
                            var item = Items.Where(x => x.SXauNoiMa == dieuChinh.SXauNoiMa && x.IIdMaDonVi == dieuChinh.IIdMaDonVi).FirstOrDefault();
                            if (item != null)
                            {
                                var value = dieuChinh.FDuKienQtDauNam.Value + dieuChinh.FDuKienQtCuoiNam.Value - dieuChinh.FDuToanNganSachNam.Value;
                                if ((ColumnVisibility.IsDisplayTuChi || ColumnVisibility.IsDisplayTuChiDieuChinh) && item.IsEditTuChi)
                                    item.FTuChi = value;
                                else if (item.SLns == "1040200" && (ColumnVisibility.IsDisplayHangNhap || ColumnVisibility.IsDisplayHangNhapDieuChinh) && item.IsEditHangNhap)
                                    item.FHangNhap = value;
                                else if (item.SLns == "1040300" && (ColumnVisibility.IsDisplayHangMua || ColumnVisibility.IsDisplayHangMuaDieuChinh) && item.IsEditHangMua)
                                    item.FHangMua = value;
                            }
                        }
                    }
                }
            }
            else MessageBoxHelper.Info(Resources.MsgNoDataAdjustedEstimate);
            _isFillData = false;
            CalculateData();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
        }

        #region xử lý động mục lục ngân sách
        private void ProcessDynamicMLNS()
        {
            _listChungTuChiTietDynamic = new List<NsDtChungTuChiTietQuery>();

            //setting visibility/collapse MLNS columns
            List<string> chiTietTois = new List<string>();
            chiTietTois = _configMLNSItems.Where(x => !string.IsNullOrEmpty(x.SelectedMLNSReportSetting)).Select(x => x.SelectedMLNSReportSetting).Distinct().ToList();
            ColumnDisplayInput = DynamicMLNS.SettingColumnVisibility(chiTietTois);

            foreach (var item in _listChungTuChiTietDuLieuNhap.Where(x => string.IsNullOrEmpty(x.SL)))
            {
                var config = _configMLNSItems.Where(x => x.Lns == item.SLns).FirstOrDefault();
                if (config != null)
                {
                    item.SChiTietToi = config.SelectedMLNSReportSetting;
                    _listChungTuChiTietDynamic.AddRange(GetChildItems(_listChungTuChiTietDuLieuNhap.Where(x => x.SLns == item.SLns).ToList(), item));
                }
                else _listChungTuChiTietDynamic.Add(item);
            }
            _listChungTuChiTietDynamic = _listChungTuChiTietDynamic.OrderBy(x => x.SXauNoiMa).ToList();
        }

        public List<NsDtChungTuChiTietQuery> GetChildItems(List<NsDtChungTuChiTietQuery> data, NsDtChungTuChiTietQuery root)
        {
            var mlns = _listMLNS.Where(x => x.Lns == root.SLns).FirstOrDefault();
            List<NsDtChungTuChiTietQuery> childrens = new List<NsDtChungTuChiTietQuery>();
            switch (root.SChiTietToi)
            {
                case nameof(MLNSFiled.NG):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.SNg) && string.IsNullOrEmpty(x.STng)).Select(x => { SettingProperty(false, mlns, ref x); return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.SNg) && string.IsNullOrEmpty(x.STng)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
                case nameof(MLNSFiled.TNG):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.STng) && string.IsNullOrEmpty(x.STng1)).Select(x => { SettingProperty(false, mlns, ref x); return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.STng) && string.IsNullOrEmpty(x.STng1)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
                case nameof(MLNSFiled.TNG1):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.STng1) && string.IsNullOrEmpty(x.STng2)).Select(x => { SettingProperty(false, mlns, ref x); return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.STng1) && string.IsNullOrEmpty(x.STng2)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
                case nameof(MLNSFiled.TNG2):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.STng2) && string.IsNullOrEmpty(x.STng3)).Select(x => { SettingProperty(false, mlns, ref x); return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.STng2) && string.IsNullOrEmpty(x.STng3)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
                case nameof(MLNSFiled.TNG3):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.STng3)).Select(x => { SettingProperty(false, mlns, ref x); return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.STng3)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
            }
            return childrens;
        }

        private void SettingProperty(bool isHangCha, NsMucLucNganSach mlns, ref NsDtChungTuChiTietQuery model)
        {
            model.BHangCha = isHangCha;
            model.IsEditTuChi = mlns.BTuChi;
            model.IsEditHienVat = mlns.BHienVat;
            model.IsEditHangNhap = mlns.BHangNhap;
            model.IsEditHangMua = mlns.BHangMua;
            model.IsEditDuPhong = mlns.BDuPhong;
            model.IsEditPhanCap = mlns.BPhanCap;
        }
        #endregion

        private void CalculateDataImport()
        {
            ItemImports.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FRutKBNN = 0;
                    x.FHienVat = 0;
                    x.FHangNhap = 0;
                    x.FHangMua = 0;
                    x.FPhanCap = 0;
                    x.FDuPhong = 0;
                    return x;
                }).ToList();

            foreach (var item in ItemImports.Where(x => (x.FTuChi != 0 || x.FRutKBNN != 0 || x.FHienVat != 0 || x.FHangNhap != 0 || x.FHangMua != 0 || x.FPhanCap != 0)))
            {
                CalculateParentImport(item, item);
            }

            CalculateTotal();
        }

        private void CalculateParentImport(DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem)
        {
            var parrentItem = ItemImports.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FTuChi += seftItem.FTuChi;
            parrentItem.FRutKBNN += seftItem.FRutKBNN;
            parrentItem.FHienVat += seftItem.FHienVat;
            parrentItem.FHangNhap += seftItem.FHangNhap;
            parrentItem.FHangMua += seftItem.FHangMua;
            parrentItem.FPhanCap += seftItem.FPhanCap;
            parrentItem.FDuPhong += seftItem.FDuPhong;
            CalculateParentImport(parrentItem, seftItem);
        }

        private void SetColumnImportVisibility()
        {
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.SType == TypeDanhMuc.DM_CAUHINH);
            predicate = predicate.And(x => x.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI);
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            var columnSetting = _danhMucNganhService.FindByCondition(predicate).FirstOrDefault();

            List<string> mlnsColumns = new List<string> { "LNS", "L", "K", "M", "TM", "TTM", "NG", "TNG", "TNG1", "TNG2", "TNG3" };

            int columnIndexMax = 0;

            if (columnSetting != null && columnSetting.SGiaTri.Equals(mlnsColumns[3]))
            {
                columnIndexMax = (int)MLNSFiled.M;
            }
            else if (columnSetting != null && columnSetting.SGiaTri.Equals(mlnsColumns[4]))
            {
                columnIndexMax = (int)MLNSFiled.TM;
            }
            else if (columnSetting != null && columnSetting.SGiaTri.Equals(mlnsColumns[5]))
            {
                columnIndexMax = (int)MLNSFiled.TTM;
            }
            else if (columnSetting != null && columnSetting.SGiaTri.Equals(mlnsColumns[6]))
            {
                columnIndexMax = (int)MLNSFiled.NG;
            }
            else if (columnSetting != null && columnSetting.SGiaTri.Equals(mlnsColumns[7]))
            {
                columnIndexMax = (int)MLNSFiled.TNG;
            }
            else if (columnSetting != null && columnSetting.SGiaTri.Equals(mlnsColumns[8]))
            {
                columnIndexMax = (int)MLNSFiled.TNG1;
            }
            else if (columnSetting != null && columnSetting.SGiaTri.Equals(mlnsColumns[9]))
            {
                columnIndexMax = (int)MLNSFiled.TNG2;
            }
            else if (columnSetting != null && columnSetting.SGiaTri.Equals(mlnsColumns[10]))
            {
                columnIndexMax = (int)MLNSFiled.TNG3;
            }

            _columnDisplayImport = new MLNSColumnDisplay();

            /*
            int columnIndexMax = 0;
            if (_itemImports.Any(x => !string.IsNullOrEmpty(x.STng3)))
                columnIndexMax = (int)MLNSFiled.TNG3;
            else if (_itemImports.Any(x => !string.IsNullOrEmpty(x.STng2)))
                columnIndexMax = (int)MLNSFiled.TNG2;
            else if (_itemImports.Any(x => !string.IsNullOrEmpty(x.STng1)))
                columnIndexMax = (int)MLNSFiled.TNG1;
            else if (_itemImports.Any(x => !string.IsNullOrEmpty(x.STng)))
                columnIndexMax = (int)MLNSFiled.TNG;
            else if (_itemImports.Any(x => !string.IsNullOrEmpty(x.SNg)))
                columnIndexMax = (int)MLNSFiled.NG;
            else if (_itemImports.Any(x => !string.IsNullOrEmpty(x.STtm)))
                columnIndexMax = (int)MLNSFiled.TTM;
            else if (_itemImports.Any(x => !string.IsNullOrEmpty(x.STm)))
                columnIndexMax = (int)MLNSFiled.TM;
            else if (_itemImports.Any(x => !string.IsNullOrEmpty(x.SM)))
                columnIndexMax = (int)MLNSFiled.M;
            */

            foreach (var prop in _columnDisplayImport.GetType().GetProperties())
            {
                if (prop.GetCustomAttributes(true).Length > 0)
                {
                    MLNSColumnAttribute attribute = prop.GetCustomAttributes(true).First() as MLNSColumnAttribute;
                    if (attribute.ColumnIndex > columnIndexMax)
                        prop.SetValue(_columnDisplayImport, Visibility.Collapsed);
                }
            }
            OnPropertyChanged(nameof(ColumnDisplayImport));
        }

        private void OnSelectItem(ref DtChungTuChiTietModel selectedItem)
        {
            if (selectedItem != null)
            {
                _isCompareData = true;
                _listCompare = new List<DtChungTuChiTietModel>();
                _listCompare.AddRange(GetItemParentSelected(selectedItem));
                _listCompare.Add(selectedItem);

                string LNS = selectedItem.SLns;
                int selectedInputColumnIndex = DynamicMLNS.GetColumnIndex(selectedItem);
                int selectedColumnIndex = DynamicMLNS.GetColumnIndexByChiTietToi(this.ChiTietToi);

                if (selectedInputColumnIndex <= selectedColumnIndex && !selectedItem.IsHangCha)
                {
                    ItemInputs.Select(x => { x.IsMapData = false; return x; }).ToList();
                    selectedItem.IsMapData = true;
                }
                _listCompare.AddRange(GetItemChildrenSelected(selectedItem));
                BeForeRefresh();
                _chungTuChiTietItemsView.Refresh();
                CalculateData();
            }
        }

        public List<DtChungTuChiTietModel> GetItemParentSelected(DtChungTuChiTietModel child)
        {
            List<DtChungTuChiTietModel> listParent = Items.Where(n => n.IIdMlns == child.IIdMlnsCha).ToList();
            foreach (DtChungTuChiTietModel item in listParent)
            {
                _listCompare.AddRange(GetItemParentSelected(item));
            }
            return (listParent != null && listParent.Count > 0) ? listParent : new List<DtChungTuChiTietModel>();
        }

        public List<DtChungTuChiTietModel> GetItemChildrenSelected(DtChungTuChiTietModel parent)
        {
            List<DtChungTuChiTietModel> listChildren = Items.Where(n => n.IIdMlnsCha == parent.IIdMlns).ToList();
            foreach (DtChungTuChiTietModel item in listChildren)
            {
                _listCompare.AddRange(GetItemChildrenSelected(item));
            }
            return (listChildren != null && listChildren.Count > 0) ? listChildren : new List<DtChungTuChiTietModel>();
        }

        private void OnMapImportData()
        {
            string message = "Đ/c có muốn sử dụng dữ liệu đã chọn không?";
            var result = MessageBox.Show(message, Resources.NotifiTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            _isFillData = true;
            if (result == MessageBoxResult.Yes)
            {
                SetDataImport(SelectedItemImport);
            }
            _isFillData = false;
        }

        private void OnMapAllImportData()
        {
            string message = "Đ/c có muốn sử dụng tất cả dữ liệu import không?";
            var result = MessageBox.Show(message, Resources.NotifiTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _isFillData = true;
                foreach (var item in ItemImports.Where(x => !x.IsHangCha))
                {
                    SetDataImport(item);
                }
                _isFillData = false;
            }
        }

        private void SetDataImport(DtChungTuChiTietModel dataImport)
        {
            DtChungTuChiTietModel model = Items.Where(x => x.SXauNoiMa == dataImport.SXauNoiMa && !x.IsHangCha).FirstOrDefault();
            if (model != null)
            {
                model.FTuChi = dataImport.FTuChi;
                model.FRutKBNN = dataImport.FRutKBNN;
                model.FHienVat = dataImport.FHienVat;
                model.FHangMua = dataImport.FHangMua;
                model.FHangNhap = dataImport.FHangNhap;
                model.FPhanCap = dataImport.FPhanCap;
                model.FDuPhong = dataImport.FDuPhong;
                model.FTonKho = dataImport.FTonKho;
            }
            else
            {
                model = Items.Where(x => x.IIdMlns == dataImport.IIdMlnsCha && !x.IsHangCha).FirstOrDefault();
                if (model != null)
                {
                    DtChungTuChiTietModel ctImport = ItemImports.Where(x => x.IIdMlns == model.IIdMlns && !x.IsHangCha).FirstOrDefault();
                    if (ctImport != null)
                    {
                        model.FHienVat = ctImport.FHienVat;
                        model.FHangMua = ctImport.FHangMua;
                        model.FHangNhap = ctImport.FHangNhap;
                        model.FPhanCap = ctImport.FPhanCap;
                        model.FDuPhong = ctImport.FDuPhong;
                        model.FTonKho = dataImport.FTonKho;
                    }
                }
            }
        }

        private void OnShowImportData()
        {
            IsShowDataImport = !IsShowDataImport;
            if (!IsShowDataImport)
            {
                _isCompareData = false;
                _listCompare = new List<DtChungTuChiTietModel>();
                BeForeRefreshMirror();
                _chungTuChiTietItemsView.Refresh();
                CalculateData();
            }
        }

        private void OnShowInputData()
        {
            IsShowDataInput = !IsShowDataInput;
            if (!IsShowDataInput)
            {
                _isCompareData = false;
                _listCompare = new List<DtChungTuChiTietModel>();
                BeForeRefreshMirror();
                _chungTuChiTietItemsView.Refresh();
                CalculateData();
            }
        }

        public override void OnClose(object obj)
        {
            base.OnClose(obj);
        }

        public override void Dispose()
        {
            base.Dispose();
            if (DivisionDetailCanCuViewModel != null)
            {
                DivisionDetailCanCuViewModel.Dispose();
            }
        }

        private void OnMapInputData()
        {
            string message = "Đ/c có muốn lấy dữ liệu nhập không?";
            var result = MessageBox.Show(message, Resources.NotifiTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SetDataInput();
            }
        }

        private void OnDeleteInputData()
        {
            string message = "Đ/c có muốn xóa dữ liệu nhập không?";
            var result = MessageBox.Show(message, Resources.NotifiTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var item = ItemInputs.Where(x => x.IIdMlns == _selectedItemInput.IIdMlns).First();
                item.FTuChi = 0;
                item.FRutKBNN = 0;
                item.FHienVat = 0;
                item.FHangNhap = 0;
                item.FHangMua = 0;
                item.FPhanCap = 0;
                item.FDuPhong = 0;
                item.IsModified = true;
                OnPropertyChanged(nameof(IsSaveInputData));
                CalculateDataInput();
            }
        }

        private void SetDataInput()
        {
            var item = ItemInputs.Where(x => x.IIdMlns == _selectedItemInput.IIdMlns).First();
            DtChungTuChiTietModel model = Items.Where(x => x.SXauNoiMa == item.SXauNoiMa).FirstOrDefault();
            if (model != null)
            {
                item.FTuChi = model.FTuChi;
                item.FRutKBNN = model.FRutKBNN;
                item.FHienVat = model.FHienVat;
                item.FHangMua = model.FHangMua;
                item.FHangNhap = model.FHangNhap;
                item.FPhanCap = model.FPhanCap;
                item.FDuPhong = model.FDuPhong;
                item.IsModified = true;
                OnPropertyChanged(nameof(IsSaveInputData));
                CalculateDataInput();
            }
        }

        private void OnSaveInputData()
        {
            var session = _sessionService.Current;
            List<DtChungTuChiTietModel> listChungTuChiTietAdd = ItemInputs.Where(x => !x.IsHangCha && (x.FTuChi != 0 || x.FHienVat != 0 || x.FHangNhap != 0 || x.FHangMua != 0 || x.FDuPhong != 0 || x.FPhanCap != 0)).ToList();

            //Xóa chứng từ chi tiết dữ liệu nhập
            _dtChungTuChiTietService.DeleteInputData(Model.Id);

            // Thêm mới chứng từ chi tiết dữ liệu nhập
            if (listChungTuChiTietAdd.Count > 0)
            {
                listChungTuChiTietAdd = listChungTuChiTietAdd.Select(x =>
                {
                    x.Id = Guid.Empty;
                    x.IIdDtchungTu = Model.Id;
                    x.IIdMaDonVi = session.IdDonVi;
                    x.STenDonVi = session.TenDonVi;
                    x.INamNganSach = Model.INamNganSach;
                    x.INamLamViec = Model.INamLamViec;
                    x.IIdMaNguonNganSach = Model.IIdMaNguonNganSach;
                    x.IPhanCap = NSDuToan.IPHANCAP_NHAN_PHANBO;
                    x.IDuLieuNhan = (int)DtDuLieuNhan.Input;
                    return x;
                }).ToList();

                List<NsDtChungTuChiTiet> listChungTuChiTiets = _mapper.Map<List<NsDtChungTuChiTiet>>(listChungTuChiTietAdd);
                _dtChungTuChiTietService.AddRange(listChungTuChiTiets);
            }
            MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);

            // Refresh state form
            ItemInputs.Select(x => { x.IsModified = false; return x; }).ToList();
            OnPropertyChanged(nameof(IsSaveInputData));
        }

        private void CalculateDataInput()
        {
            ItemInputs.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FRutKBNN = 0;
                    x.FHienVat = 0;
                    x.FHangNhap = 0;
                    x.FHangMua = 0;
                    x.FPhanCap = 0;
                    x.FDuPhong = 0;
                    return x;
                }).ToList();
            foreach (var item in ItemInputs.Where(x => (x.FTuChi != 0 || x.FHienVat != 0 || x.FHangNhap != 0 || x.FHangMua != 0 || x.FPhanCap != 0 || x.FDuPhong != 0)))
            {
                CalculateParentInput(item, item);
            }
        }

        private void CalculateParentInput(DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem)
        {
            var parrentItem = ItemInputs.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FTuChi += seftItem.FTuChi;
            parrentItem.FRutKBNN += seftItem.FRutKBNN;
            parrentItem.FHienVat += seftItem.FHienVat;
            parrentItem.FHangNhap += seftItem.FHangNhap;
            parrentItem.FHangMua += seftItem.FHangMua;
            parrentItem.FPhanCap += seftItem.FPhanCap;
            parrentItem.FDuPhong += seftItem.FDuPhong;
            CalculateParentInput(parrentItem, seftItem);
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
                    Items.Where(x => x.IsFilter && !x.IsHangCha).ForAll(x => x.IsDeleted = true);
                    OnSave();
                }
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private DtChungTuChiTietModel ResetItemValue(DtChungTuChiTietModel item)
        {
            item.FTuChi = 0;
            item.FRutKBNN = 0;
            item.FHienVat = 0;
            item.FHangNhap = 0;
            item.FHangMua = 0;
            item.FPhanCap = 0;
            item.FDuPhong = 0;
            item.FTonKho = 0;
            item.SGhiChu = string.Empty;
            return item;
        }

        private void UpdateChungTu()
        {
            NsDtChungTu chungTu = _dtChungTuService.FindById(Model.Id);
            var itemsHasData = Items.Where(x => x.HasData);
            chungTu.FTongTuChi = itemsHasData.Sum(x => x.FTuChi);
            chungTu.FTongRutKBNN = itemsHasData.Sum(x => x.FRutKBNN);
            chungTu.FTongHienVat = itemsHasData.Sum(x => x.FHienVat);
            chungTu.FTongHangNhap = itemsHasData.Sum(x => x.FHangNhap);
            chungTu.FTongHangMua = itemsHasData.Sum(x => x.FHangMua);
            chungTu.FTongPhanCap = itemsHasData.Sum(x => x.FPhanCap);
            chungTu.FTongDuPhong = itemsHasData.Sum(x => x.FDuPhong);

            //Update thông tin chứng từ điều chỉnh
            
            if (_listGuidTyp2 != null)
            {
                var lstAdjChosen = _listGuidTyp2.Distinct();
                var selectedDcChungTu = GetAdjustedEstimateViewModel.SelectedDcChungTu;
                var adjSaving = selectedDcChungTu.Where(x => lstAdjChosen.Contains(x.Id)).ToList();
                if (adjSaving.Any())
                {
                    chungTu.IIDChungTuDieuChinh = string.Join(",", adjSaving.Select(x => x.Id.ToString()).ToList());
                    chungTu.SSoChungTuDieuChinh = string.Join(",", adjSaving.Select(x => x.SSoChungTu.ToString()).ToList());
                }
            }
            _dtChungTuService.Update(chungTu);

            Model.FTongTuChi = chungTu.FTongTuChi;
            Model.FTongRutKBNN = chungTu.FTongRutKBNN;
            Model.FTongHienVat = chungTu.FTongHienVat;
            Model.FTongHangNhap = chungTu.FTongHangNhap;
            Model.FTongHangMua = chungTu.FTongHangMua;
            Model.FTongPhanCap = chungTu.FTongPhanCap;
            Model.FTongDuPhong = chungTu.FTongDuPhong;
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
            UpdateVoucherEvent?.Invoke(Model, new EventArgs());
        }

        private void OnShowAdjustedEstimateVoucher()
        {
            GetAdjustedEstimateViewModel.Name = "Chọn dữ liệu điều chỉnh";
            GetAdjustedEstimateViewModel.Description = "Chọn dữ liệu điều chỉnh";
            GetAdjustedEstimateViewModel.ILoaiChungTu = Model.ILoaiChungTu;
            GetAdjustedEstimateViewModel.EstimateVoucher = Model;
            GetAdjustedEstimateViewModel.Init();
            GetAdjustedEstimateViewModel.SavedAction = obj =>
            {
                var dataDieuChinh = (List<DcChungTuChiTietModel>)obj;
                GetAdjustedEstimateData(dataDieuChinh);
            };
            var addView = new GetAdjustedEstimate() { DataContext = GetAdjustedEstimateViewModel };
            DialogHost.Show(addView, "DivisionDetailDialog");
        }

        private void GetAdjustedEstimateData(List<DcChungTuChiTietModel> dataDieuChinh)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                _listGuidTyp2 = new List<Guid>();
                if (dataDieuChinh.Any())
                {
                    HasAdjData = true;
                    OnPropertyChanged(nameof(IsSaveData));
                    var dieuChinhLan2 = dataDieuChinh.Where(x => x.ILoaiDuKien == (int)EstimateSettlementType.NINE_MONTH);
                    var dieuChinhLan1n2 = dataDieuChinh.Where(x => x.ILoaiDuKien == (int)EstimateSettlementType.SIX_MONTH && dieuChinhLan2.Select(s => s.SLns).Distinct().Contains(x.SLns)).Select(x => x.IIdDcchungTu).Distinct().ToList();
                    if (dieuChinhLan1n2.Any())
                        dataDieuChinh.RemoveAll(r => dieuChinhLan1n2.Contains(r.IIdDcchungTu));

                    var itemFilter = Items.Where(x => !x.IsHangCha && dataDieuChinh.Select(a => a.SXauNoiMa).Contains(x.SXauNoiMa)).ToList();
                    Parallel.ForEach(itemFilter, item =>
                    {
                        DcChungTuChiTietModel dataMap = new DcChungTuChiTietModel();
                        var ltsDataMap = dataDieuChinh.Where(x => x.SXauNoiMa.Equals(item.SXauNoiMa)).ToList();
                        dataMap = ltsDataMap.FirstOrDefault();
                        _listGuidTyp2.Add(dataMap.IIdDcchungTu.GetValueOrDefault());

                        if (Model.ILoaiChungTu == int.Parse(VoucherType.NSSD_Key) && dataMap != null)
                        {
                            if (dataMap.FTang != 0)
                                item.FTuChi = dataMap.FTang;
                            else if (dataMap.FGiam != 0)
                                item.FTuChi = 0 - dataMap.FGiam;
                            
                        }
                        else if (Model.ILoaiChungTu == int.Parse(VoucherType.NSBD_Key) && dataMap != null)
                        {
                            var mlnsCha = _listMLNS.FirstOrDefault(x => x.XauNoiMa == item.SLns);
                            if (dataMap.FTang != 0)
                            {
                                if (mlnsCha.BHangNhap == true && item.SLns == "1040200")
                                    item.FHangNhap = dataMap.FTang;
                                else if (mlnsCha.BHangMua == true && item.SLns == "1040300")
                                    item.FHangMua = dataMap.FTang;
                                else
                                    item.FTuChi = dataMap.FTang;
                            }
                            if (dataMap.FGiam != 0)
                            {
                                if (mlnsCha.BHangNhap == true && item.SLns == "1040200")
                                    item.FHangNhap = 0 - dataMap.FGiam;
                                else if (mlnsCha.BHangMua == true && item.SLns == "1040300")
                                    item.FHangMua = 0 - dataMap.FGiam;
                                else
                                    item.FTuChi = 0 - dataMap.FGiam;
                            }
                        }
                    });
                }
                CalculateData();
            }, (s, e) =>
            {
                IsLoading = false;
            });
        }
    }
}
