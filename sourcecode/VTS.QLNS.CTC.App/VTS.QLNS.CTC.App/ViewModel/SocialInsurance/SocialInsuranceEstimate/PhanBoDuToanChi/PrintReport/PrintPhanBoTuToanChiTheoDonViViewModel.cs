using AutoMapper;
using log4net;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi.PrintReport
{
    public class PrintPhanBoTuToanChiTheoDonViViewModel : ReportViewModelBase<BhPbdtcBHXHModel, BhPbdtcBHXHChiTietModel, BhPbdtcBHXHChiTietModel>
    {
        #region Interface
        private IExportService _exportService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private ILog _logger;
        private IMapper _mapper;
        private INsDonViService _donViService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private readonly IPbdtcBHXHService _pbdtcBHXHService;
        private readonly IPbdtcBHXHChiTietService _pbdtcBHXHChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhMucLoaiChiService;
        private readonly IBhBaoCaoGhiChuService _bhGhiChuService;

        #endregion

        #region Property
        List<BhPbdtcBHXH> listChungTu;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private bool _checkAllAgencies;
        public bool IsQuanLyDonViCha;
        public string TitleFirst1;
        public string TitleFirst2;
        public string TitleFirst3;
        public bool IsShowDatePeople { get; set; }
        public string TieuDeBaoCao { get; set; }
        public string name { get; set; }
        private string _typeChuky;

        private bool _isMillionRound;
        public bool IsMillionRound
        {
            get => _isMillionRound;
            set => SetProperty(ref _isMillionRound, value);
        }

        private string ReportName
        {
            get
            {
                return name = "Thông báo phân bổ dự toán chi - Đơn vị";
            }
        }

        private ObservableCollection<ComboboxItem> _typeDotPhanBo = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> TypeDotPhanBo
        {
            get => _typeDotPhanBo;
            set => SetProperty(ref _typeDotPhanBo, value);
        }

        private ComboboxItem _selectDotPhanBo;
        public ComboboxItem SelectDotPhanBo
        {
            get => _selectDotPhanBo;
            set
            {
                SetProperty(ref _selectDotPhanBo, value);
                if (_selectDotPhanBo != null)
                {
                    if (_selectedLoaiChi != null)
                    {
                        LoadDataDot();
                        LoadAgencies();
                        LoadTieuDe();
                    }

                }
            }
        }

        public override string Name => ReportName;
        public override string Title => ReportName;
        public override string Description => ReportName;
        public bool IsExportEnable => Agencies != null && Agencies.Any(x => x.Selected);

        private List<ComboboxItem> _reportTypes;
        public List<ComboboxItem> ReportTypes
        {
            get => _reportTypes;
            set => SetProperty(ref _reportTypes, value);
        }

        private ComboboxItem _selectedReportType;
        public ComboboxItem SelectedReportType
        {
            get => _selectedReportType;
            set
            {
                SetProperty(ref _selectedReportType, value);
                //OnPropertyChanged(nameof(IsEnableCheckBox1Page));
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiChi;
        public ObservableCollection<ComboboxItem> ItemsLoaiChi
        {
            get => _itemsLoaiChi;
            set => SetProperty(ref _itemsLoaiChi, value);

        }

        private ComboboxItem _selectedLoaiChi;
        public ComboboxItem SelectedLoaiChi
        {
            get => _selectedLoaiChi;
            set
            {
                SetProperty(ref _selectedLoaiChi, value);
                if (_selectedLoaiChi != null)
                {
                    if (_selectedLoaiChi != null)
                    {
                        _dataDotSelected = null;
                        LoadDataDot();
                        LoadAgencies();

                    }
                    LoadKieuGiayIn();
                    LoadTieuDe();
                    // LoadTypeChuKy(_selectedDanhMucLoaiChi.HiddenValue);
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataLoaiBaoCao;
        public ObservableCollection<ComboboxItem> DataLoaiBaoCao
        {
            get => _dataLoaiBaoCao;
            set => SetProperty(ref _dataLoaiBaoCao, value);
        }

        private ComboboxItem _selectedLoaiBaoCao;
        public ComboboxItem SelectedLoaiBaoCao
        {
            get => _selectedLoaiBaoCao;
            set => SetProperty(ref _selectedLoaiBaoCao, value);
        }

        #region list agency
        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set
            {
                SetProperty(ref _agencies, value);
            }
        }

        private string _searchAgencyText;
        public string SearchAgencyText
        {
            get => _searchAgencyText;
            set
            {
                if (SetProperty(ref _searchAgencyText, value))
                {
                    _listAgency.Refresh();
                }
            }
        }

        public string SelectedAgencyCount
        {
            get
            {
                int totalCount = 0;
                int totalSelected = 0;
                if (_agencies != null)
                {
                    totalCount = Agencies != null ? Agencies.Where(x => x.IsFilter).Count() : 0;
                    totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                }
                return string.Format(SELECTED_AGENCY_COUNT_STR, totalSelected, totalCount);
            }
        }
        private bool _isSelectedAllAgency;
        public bool IsSelectedAllAgency
        {
            get => Agencies.Count > 0 && Agencies.Where(x => x.IsFilter).All(x => x.Selected);
            set
            {
                SetProperty(ref _isSelectedAllAgency, value);
                _checkAllAgencies = true;
                foreach (AgencyModel item in Agencies)
                {
                    item.Selected = _isSelectedAllAgency;
                    OnPropertyChanged(nameof(IsExportEnable));
                }
                _checkAllAgencies = false;
                OnPropertyChanged(nameof(SelectedAgencyCount));
            }
        }
        #endregion

        #region list LNS
        private ObservableCollection<NsMuclucNgansachModel> _budgetIndexes;
        public ObservableCollection<NsMuclucNgansachModel> BudgetIndexes
        {
            get => _budgetIndexes;
            set => SetProperty(ref _budgetIndexes, value);
        }

        private string _searchBudgetIndexText;
        public string SearchBudgetIndexText
        {
            set
            {
                if (SetProperty(ref _searchBudgetIndexText, value))
                {
                    _listBudgetIndex.Refresh();
                }
            }
        }

        public string SelectedBudgetIndexCount
        {
            get
            {
                int totalCount = BudgetIndexes != null ? BudgetIndexes.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => BudgetIndexes.Count > 0 && BudgetIndexes.Where(x => x.IsFilter).All(x => x.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                foreach (NsMuclucNgansachModel item in BudgetIndexes)
                {
                    item.IsSelected = _isSelectAllBudgetIndex;
                }
            }
        }
        #endregion

        private bool _isEnableLoaiThu;
        public bool IsEnableLoaiThu
        {
            get => _isEnableLoaiThu;
            set => SetProperty(ref _isEnableLoaiThu, value);
        }

        private bool _isEnableReportType;
        public bool IsEnableReportType
        {
            get => _isEnableReportType;
            set => SetProperty(ref _isEnableReportType, value);
        }

        private bool _isEnableInTheo;
        public bool IsEnableInTheo
        {
            get => _isEnableInTheo;
            set => SetProperty(ref _isEnableInTheo, value);
        }

        private string _title1;
        public string Title1
        {
            get => _title1;
            set => SetProperty(ref _title1, value);
        }

        private string _title2;
        public string Title2
        {
            get => _title2;
            set => SetProperty(ref _title2, value);
        }

        private string _title3;
        public string Title3
        {
            get => _title3;
            set => SetProperty(ref _title3, value);
        }

        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
        }

        private bool _isDatePeople;
        public bool IsDatePeople
        {
            get => _isDatePeople;
            set
            {
                SetProperty(ref _isDatePeople, value);
            }
        }

        private List<ComboboxItem> _units;
        public List<ComboboxItem> Units
        {
            get => _units;
            set => SetProperty(ref _units, value);
        }

        private ComboboxItem _selectedUnit;
        public ComboboxItem SelectedUnit
        {
            get => _selectedUnit;
            set => SetProperty(ref _selectedUnit, value);
        }

        private bool _isSummary;
        public bool IsSummary
        {
            get => _isSummary;
            set => SetProperty(ref _isSummary, value);
        }

        private bool _isSummaryAgency;
        public bool IsSummaryAgency
        {
            get => _isSummaryAgency;
            set
            {
                SetProperty(ref _isSummaryAgency, value);

            }
        }

        private bool _inTheoDotNgayChecked;
        public bool InTheoDotNgayChecked
        {
            get => _inTheoDotNgayChecked;
            set
            {
                SetProperty(ref _inTheoDotNgayChecked, value);
                if (_selectedLoaiChi != null)
                {
                    LoadDataDot();
                    LoadAgencies();
                }
            }
        }
        private bool _inLuyKeChecked;
        public bool InLuyKeChecked
        {
            get => _inLuyKeChecked;
            set
            {
                SetProperty(ref _inLuyKeChecked, value);
                if (_selectedLoaiChi != null)
                {
                    LoadDataDot();
                    LoadAgencies();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataDot = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ComboboxItem _dataDotSelected;
        public ComboboxItem DataDotSelected
        {
            get => _dataDotSelected;
            set
            {
                SetProperty(ref _dataDotSelected, value);
                if (_dataDotSelected != null)
                {
                    LoadAgencies();
                }

                LoadTieuDe();
            }
        }

        private void LoadTieuDe()
        {
            LoadTypeChuKy();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            else Title1 = TitleFirst1;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            else Title2 = TitleFirst2;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
            else Title3 = TitleFirst3;
        }
        private ObservableCollection<ComboboxItem> _itemsKieuGiayIn = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ItemsKieuGiayIn
        {
            get => _itemsKieuGiayIn;
            set => SetProperty(ref _itemsKieuGiayIn, value);
        }

        private ComboboxItem _selectedKieuGiayIn;

        public ComboboxItem SelectedKieuGiayIn
        {
            get => _selectedKieuGiayIn;
            set => SetProperty(ref _selectedKieuGiayIn, value);
        }
        private BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel { get; set; }
        #endregion

        #region RelayCommand
        public RelayCommand NoteCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand DataInterpretationCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        #endregion

        #region Contructor 
        public PrintPhanBoTuToanChiTheoDonViViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            INsDonViService donViService,
            IDmChuKyService dmChuKyService,
            IDanhMucService danhMucService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IPbdtcBHXHService pbdtcBHXHService,
            IPbdtcBHXHChiTietService pbdtcBHXHChiTietService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            IBhBaoCaoGhiChuService bhBaoCaoGhiChuService,
            BhBaoCaoGhiChuDialogViewModel bhBaoCaoGhiChuDialogViewModel
            )
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _exportService = exportService;
            _donViService = donViService;
            _dmChuKyService = dmChuKyService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _bhMucLoaiChiService = bhDanhMucLoaiChiService;
            _danhMucService = danhMucService;
            _pbdtcBHXHService = pbdtcBHXHService;
            _pbdtcBHXHChiTietService = pbdtcBHXHChiTietService;
            _bhGhiChuService = bhBaoCaoGhiChuService;
            BhBaoCaoGhiChuDialogViewModel = bhBaoCaoGhiChuDialogViewModel;
            ExportCommand = new RelayCommand(obj => IsOpenExportPopup = true);
            ExportExcelCommand = new RelayCommand(obj => OnExportFile((int)ExportType.EXCEL));
            ExportPDFCommand = new RelayCommand(obj =>
            {
                OnExportFile(ExportType.PDF);
            });
            PrintCommand = new RelayCommand(obj =>
            {
                OnExportFile(ExportType.PDF);
            });
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
        }
        #endregion

        #region Init
        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            _agencies = new ObservableCollection<AgencyModel>();
            IsSummary = false;
            IsSummaryAgency = false;
            //ResetCondition();
            LoadDotPhanBo();
            LoadDanhMucLoaiChi();
            LoadLoaiBaoCao();
            LoadKieuGiayIn();
            LoadTieuDe();
            LoadTypeChuKy();
            LoadDanhMuc();
        }
        #endregion

        #region Load data
        private void LoadLoaiBaoCao()
        {
            DataLoaiBaoCao = new ObservableCollection<ComboboxItem>();
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_DON_VI, DisplayItem = LoaiBaoCao.TONG_HOP_DON_VI });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_DON_VI_THEO_KHOI, DisplayItem = LoaiBaoCao.TONG_HOP_DON_VI_THEO_KHOI });
            SelectedLoaiBaoCao = DataLoaiBaoCao.ElementAt(1); ;
            OnPropertyChanged(nameof(SelectedLoaiBaoCao));
        }

        public void LoadDotPhanBo()
        {
            var typeReport = new List<ComboboxItem>
            {
                 new ComboboxItem {DisplayItem = "Tất cả", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đầu năm", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Bổ sung", ValueItem = "2"},

            };

            TypeDotPhanBo = new ObservableCollection<ComboboxItem>(typeReport);
            SelectDotPhanBo = TypeDotPhanBo.ElementAt(1);
        }

        private void LoadTypeChuKy()
        {
            if (_selectDotPhanBo.ValueItem == DotPhanBo.Dau_Nam || _selectDotPhanBo.ValueItem == DotPhanBo.Tat_Ca)
            {
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010001_9010002 || SelectedLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_CHIBHXH_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToan;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoDuToan;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010003)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_CHIKPQL_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToanKHCKPQL;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoDuToanKHCKPQL;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010004_9010005)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_CHIKPCBQY_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToanKHCKCBQY;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoDuToanKHCKCBQY;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010006_9010007)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_CHIKPCBTS_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToanKHCKTS;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoDuToanKHCKTS;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }

                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_CHIKPCSSK_HSSV_NLS_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToanKHCKHSSVLNLD;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoDuToanKHCKHSSVNLD;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010008)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToanKHCKHSSVLNLD;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoDuToanKHCKHSSVNLD;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010009)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_CHI_MSTTBYT_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToanKHCKHSSVLNLD;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoDuToanKHCKHSSVNLD;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010010)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_CHI_BHTN_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToanKHCKHSSVLNLD;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoDuToanKHCKHSSVNLD;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }
            }
            else
            {
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010001_9010002 || SelectedLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_BS_CHIBHXH_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToan;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoBSDuToan;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010003)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_BS_CHIKPQL_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToanKHCKPQL;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoBSDuToanKHCKPQL;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010004_9010005)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_BS_CHIKPCBQY_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToanKHCKCBQY;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoBSDuToanKHCKCBQY;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010006_9010007)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_BS_CHIKPCBTS_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToanKHCKTS;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoBSDuToanKHCKTS;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }

                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_BS_CHIKPCSSK_HSSV_NLS_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToanKHCKHSSVLNLD;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoBSDuToanKHCKHSSVNLD;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010008)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_BS_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToanKHCKHSSVLNLD;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoBSDuToanKHCKHSSVNLD;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010009)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_BS_CHI_BHTN_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToanKHCKHSSVLNLD;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoBSDuToanKHCKHSSVNLD;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010010)
                {
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_PBC_BS_CHI_MSTTBYT_TONGHOP_DONVI_PHULUC;
                    TitleFirst1 = PlanKHCTitle.Title1BaoCaoDuToanKHCKHSSVLNLD;
                    TitleFirst2 = PlanKHCTitle.Title2BaoCaoBSDuToanKHCKHSSVNLD;
                    TitleFirst3 = PlanKHCTitle.Title3BaoCaoDuToan;
                }
            }
        }

        private void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(0);
            if (SelectedLoaiChi != null)
            {
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010001_9010002 || SelectedLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002)
                {
                    SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(1);
                }
            }
        }

        private void LoadDanhMucLoaiChi()
        {
            try
            {
                ItemsLoaiChi = new ObservableCollection<ComboboxItem>();
                IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi = _bhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);

                if (listDanhMucLoaiChi != null)
                {
                    listDanhMucLoaiChi = listDanhMucLoaiChi.Where(x => x.SLNS != LNSValue.LNS_9010009 && x.SLNS != LNSValue.LNS_9010008
                                                                && x.SLNS != LNSValue.LNS_9010010).ToList().OrderBy(x => x.SMaLoaiChi);


                    ItemsLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                    {
                        DisplayItem = n.STenDanhMucLoaiChi,
                        ValueItem = n.SMaLoaiChi,
                        HiddenValue = n.SLNS,
                        Id = n.Id
                    }));
                    SelectedLoaiChi = ItemsLoaiChi.ElementAt(0);
                }

                OnPropertyChanged(nameof(ItemsLoaiChi));
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }

        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(x => x.SGiaTri)
                .ToList();
            if (listDonViTinh.Count == 0)
                _units.Add(new ComboboxItem("Đồng", "1"));
            foreach (var dvt in listDonViTinh)
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = dvt.STen;
                cb.ValueItem = dvt.SGiaTri;
                cb.Type = dvt.SMoTa;
                _units.Add(new ComboboxItem(dvt.STen, dvt.SGiaTri));
            }
            OnPropertyChanged(nameof(Units));
            _selectedUnit = Units.ElementAt(0);

            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void LoadAgencies()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                var yearOfWork = _sessionInfo.YearOfWork;
                List<DonVi> lstDonVis = new List<DonVi>();

                if (SelectedLoaiChi != null)
                {
                    List<Guid> lstIdChungTu = GetListChungTuReport();
                    int dotNhan = int.Parse(SelectDotPhanBo.ValueItem);
                    if (lstIdChungTu != null && lstIdChungTu.Count > 0)
                    {
                        if (SelectDotPhanBo != null)
                        {
                            lstDonVis = _pbdtcBHXHService.FindByDonViForNamLamViecNormal(yearOfWork, SelectedLoaiChi.Id, SelectedLoaiChi.ValueItem, string.Join(",", lstIdChungTu), dotNhan).ToList();
                        }
                    }

                    lstDonVis = lstDonVis.OrderBy(x => x.IIDMaDonVi).ToList();
                    lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                }

                e.Result = lstDonVis.OrderBy(x => x.IIDMaDonVi).ToList();
            }, (s, e) =>
            {
                if (e.Result != null)
                {
                    List<DonVi> agencies = (List<DonVi>)e.Result;
                    _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(agencies);
                }
                else
                    _agencies = new ObservableCollection<AgencyModel>();
                _listAgency = CollectionViewSource.GetDefaultView(_agencies);
                _listAgency.Filter = ListAgencyFilter;
                foreach (var model in Agencies)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(AgencyModel.Selected) && !_checkAllAgencies)
                        {
                            OnPropertyChanged(nameof(SelectedAgencyCount));
                            OnPropertyChanged(nameof(IsSelectedAllAgency));
                            OnPropertyChanged(nameof(IsExportEnable));
                        }
                    };
                }

                OnPropertyChanged(nameof(Agencies));
                OnPropertyChanged(nameof(IsSelectedAllAgency));
                OnPropertyChanged(nameof(SelectedAgencyCount));
                OnPropertyChanged(nameof(IsExportEnable));
                IsLoading = false;
            });
        }

        private List<Guid> GetListChungTuReport()
        {
            if (DataDotSelected != null && InTheoDotNgayChecked)
            {
                Guid.TryParse(DataDotSelected.ValueItem, out Guid guidSelected);
                return new List<Guid>() { guidSelected };
            }
            List<Guid> result = new List<Guid>();
            List<BhPbdtcBHXHModel> lstChungTu = new List<BhPbdtcBHXHModel>();
            DateTime ngayLuyKe = DateTime.MinValue;
            if (DataDotSelected == null) return new List<Guid>();
            if (DataDotSelected.HiddenValue.Equals("1"))
            {
                lstChungTu = Models.Where(n => !string.IsNullOrEmpty(n.SSoQuyetDinh) && n.SSoQuyetDinh.Equals(DataDotSelected.ValueItem)).ToList();
                if (lstChungTu.Count > 0)
                {
                    ngayLuyKe = lstChungTu.FirstOrDefault().DNgayQuyetDinh.GetValueOrDefault().Date;
                }
            }
            else
            {
                var ngayChungTu = DataDotSelected.ValueItem;
                lstChungTu = Models.Where(n => string.IsNullOrEmpty(n.SSoQuyetDinh) && n.DNgayChungTu.HasValue && n.DNgayChungTu.Value.ToString("dd/MM/yyyy").Equals(ngayChungTu)).ToList();
                if (lstChungTu.Count > 0)
                {
                    ngayLuyKe = lstChungTu.FirstOrDefault().DNgayChungTu.GetValueOrDefault().Date;
                }
            }

            result.AddRange(lstChungTu.Select(x => x.Id));

            // ds chung tu luy ke
            if (InLuyKeChecked)
            {
                var predicate = PredicateBuilder.True<BhPbdtcBHXH>();
                predicate = predicate.And(x => x.INamChungTu == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.SMaLoaiChi == SelectedLoaiChi.ValueItem);
                predicate = predicate.And(x => (x.DNgayQuyetDinh == null && x.DNgayChungTu != null
                                                && x.DNgayChungTu.Value.Date <= ngayLuyKe)
                                                || (x.DNgayQuyetDinh != null && x.DNgayQuyetDinh.Value.Date <= ngayLuyKe));
                if (SelectDotPhanBo != null && SelectDotPhanBo.ValueItem != "0")
                {
                    predicate = predicate.And(x => x.ILoaiDotNhanPhanBo == int.Parse(SelectDotPhanBo.ValueItem));
                }
                var lstCtLuyKe = _pbdtcBHXHService.FindByCondition(predicate).ToList();
                if (lstCtLuyKe.Count > 0)
                {
                    result.AddRange(lstCtLuyKe.Select(x => x.Id));
                }
            }

            return result;
        }

        private bool ListAgencyFilter(object obj)
        {
            bool result = true;
            var item = (AgencyModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchAgencyText))
                result = item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
            item.IsFilter = result;
            return result;
        }


        private void ResetCondition()
        {
            InLuyKeChecked = false;
            InTheoDotNgayChecked = false;
        }

        private void LoadDataDot()
        {
            listChungTu = new List<BhPbdtcBHXH>();
            List<string> lstSoQuyetDinh = new List<string>();
            List<DateTime> lstNgayChungTu = new List<DateTime>();
            var yearOfWork = _sessionInfo.YearOfWork;
            int dotPhanBo;
            string sMaLoaiChi;
            dotPhanBo = SelectDotPhanBo.ValueItem == "0" ? 0 : int.Parse(SelectDotPhanBo.ValueItem);

            if (SelectedLoaiChi != null)
            {
                sMaLoaiChi = SelectedLoaiChi.ValueItem;
                listChungTu = _pbdtcBHXHService.FindByConditionLoaiChi(yearOfWork, dotPhanBo, sMaLoaiChi).ToList();
            }
            Models = _mapper.Map<ObservableCollection<BhPbdtcBHXHModel>>(listChungTu);
            if (InTheoDotNgayChecked)
            {
                DataDot = new ObservableCollection<ComboboxItem>();
                if (listChungTu != null)
                {
                    foreach (var chungTu in Models)
                    {
                        string mota = chungTu.DNgayQuyetDinh.HasValue ? chungTu.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty;
                        mota += StringUtils.SPACE;
                        mota += chungTu.SMoTa;

                        if (!string.IsNullOrEmpty(chungTu.SSoQuyetDinh))
                        {
                            DataDot.Add(new ComboboxItem()
                            {
                                ValueItem = chungTu.Id.ToString(),
                                DisplayItem = string.Format("{0} - {1}\n{2}", chungTu.SSoQuyetDinh, chungTu.SSoChungTu, mota),
                                HiddenValue = "1",
                                Type = chungTu.ILoaiDotNhanPhanBo.ToString(),
                            });
                        }
                        else
                        {
                            DataDot.Add(new ComboboxItem
                            {
                                ValueItem = chungTu.Id.ToString(),
                                DisplayItem = string.Format("{0} - {1}\n{2}", chungTu.SSoQuyetDinh, chungTu.SSoChungTu, mota),
                                HiddenValue = "2",
                                Type = chungTu.ILoaiDotNhanPhanBo.ToString(),
                            });
                        }
                    }
                }
            }
            else
            {
                DataDot = new ObservableCollection<ComboboxItem>();

                foreach (var chungTu in Models)
                {
                    if (!string.IsNullOrEmpty(chungTu.SSoQuyetDinh))
                    {
                        if (!lstSoQuyetDinh.Contains(chungTu.SSoQuyetDinh))
                        {
                            lstSoQuyetDinh.Add(chungTu.SSoQuyetDinh);
                            var lstDotBySoQuyetDinh = Models.Where(x => !string.IsNullOrEmpty(x.SSoQuyetDinh) && x.SSoQuyetDinh.Equals(chungTu.SSoQuyetDinh));
                            var firstList = lstDotBySoQuyetDinh.FirstOrDefault();
                            string mota = firstList.DNgayQuyetDinh.HasValue ? firstList.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty;
                            mota += StringUtils.SPACE;
                            mota += firstList.SMoTa;
                            DataDot.Add(new ComboboxItem
                            {
                                ValueItem = chungTu.SSoQuyetDinh,
                                DisplayItem = string.Format("{0}\n{1}", chungTu.SSoQuyetDinh, mota),
                                HiddenValue = "1",
                                Type = chungTu.ILoaiDotNhanPhanBo.ToString()
                            });
                        }
                    }
                    else
                    {
                        var ngayChungTu = chungTu.DNgayChungTu.GetValueOrDefault().Date;
                        if (!lstNgayChungTu.Contains(ngayChungTu))
                        {
                            lstNgayChungTu.Add(chungTu.DNgayChungTu.GetValueOrDefault().Date);
                            var lstDotByNgayChungTu = Models.Where(x => x.DNgayChungTu.GetValueOrDefault().Date.Equals(ngayChungTu) && string.IsNullOrEmpty(x.SSoQuyetDinh));
                            string mota = "";
                            foreach (var dt in lstDotByNgayChungTu)
                            {
                                if (!string.IsNullOrEmpty(mota))
                                {
                                    mota += "\n";
                                }
                                mota += (dt.DNgayChungTu.HasValue
                                    ? dt.DNgayChungTu.Value.ToString("dd/MM/yyyy")
                                    : string.Empty) + " " + dt.SMoTa;
                            }
                            var lstSoChungTuMota = string.Join(",", lstDotByNgayChungTu.Select(x => x.SSoChungTu));
                            DataDot.Add(new ComboboxItem
                            {
                                ValueItem = ngayChungTu.ToString("dd/MM/yyyy"),
                                DisplayItem = string.Format("{0}\n{1}", lstSoChungTuMota, mota),
                                HiddenValue = "2",
                                Type = chungTu.ILoaiDotNhanPhanBo.ToString()
                            });
                        }
                    }
                }
            }

            var ordered = DataDot.OrderByDescending(c => DateTime.Parse(c.DisplayItem.Split('\n')[1].Split(" ")[0])).ToList();
            DataDot = new ObservableCollection<ComboboxItem>(ordered);
            if (DataDot != null && DataDot.Count > 0)
            {
                DataDotSelected = DataDot.FirstOrDefault();
            }
        }

        private void OnConfigSign()
        {
            LoadTypeChuKy();
            DmChuKyModel chuKyModel = new DmChuKyModel();

            if (_dmChuKy == null || _dmChuKy.Id.IsNullOrEmpty())
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            if (_typeChuky == TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY)
            {
                DmChuKyDialogViewModel.HasAddedSign4 = true;
                DmChuKyDialogViewModel.HasAddedSign5 = true;
                DmChuKyDialogViewModel.HasAddedSign6 = true;
            }
            else
            {
                DmChuKyDialogViewModel.HasAddedSign4 = false;
                DmChuKyDialogViewModel.HasAddedSign5 = false;
                DmChuKyDialogViewModel.HasAddedSign6 = false;
            }
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void OnNoteCommand()
        {
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();
            if (_selectDotPhanBo.ValueItem == DotPhanBo.Dau_Nam || _selectDotPhanBo.ValueItem == DotPhanBo.Tat_Ca)
            {

                BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_BHXH_DT_PBC_CHIBHXH_TONGHOP_DONVI_PHULUC
                                                                            , TypeChuKy.RPT_BHXH_DT_PBC_CHIKPQL_TONGHOP_DONVI_PHULUC
                                                                            , TypeChuKy.RPT_BHXH_DT_PBC_CHIKPCBQY_TONGHOP_DONVI_PHULUC
                                                                            , TypeChuKy.RPT_BHXH_DT_PBC_CHIKPCBTS_TONGHOP_DONVI_PHULUC
                                                                            , TypeChuKy.RPT_BHXH_DT_PBC_CHIKPCSSK_HSSV_NLS_TONGHOP_DONVI_PHULUC
                                                                            , TypeChuKy.RPT_BHXH_DT_PBC_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_DONVI_PHULUC
                                                                            , TypeChuKy.RPT_BHXH_DT_PBC_CHI_BHTN_TONGHOP_DONVI_PHULUC
                                                                            , TypeChuKy.RPT_BHXH_DT_PBC_CHI_MSTTBYT_TONGHOP_DONVI_PHULUC};
            }
            else
            {

                BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_BHXH_DT_PBC_BS_CHIBHXH_TONGHOP_DONVI_PHULUC
                                                                            , TypeChuKy.RPT_BHXH_DT_PBC_BS_CHIKPQL_TONGHOP_DONVI_PHULUC
                                                                            , TypeChuKy.RPT_BHXH_DT_PBC_BS_CHIKPCBQY_TONGHOP_DONVI_PHULUC
                                                                            , TypeChuKy.RPT_BHXH_DT_PBC_BS_CHIKPCBTS_TONGHOP_DONVI_PHULUC
                                                                            , TypeChuKy.RPT_BHXH_DT_PBC_BS_CHIKPCSSK_HSSV_NLS_TONGHOP_DONVI_PHULUC
                                                                            , TypeChuKy.RPT_BHXH_DT_PBC_BS_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_DONVI_PHULUC
                                                                            , TypeChuKy.RPT_BHXH_DT_PBC_BS_CHI_BHTN_TONGHOP_DONVI_PHULUC
                                                                            , TypeChuKy.RPT_BHXH_DT_PBC_BS_CHI_MSTTBYT_TONGHOP_DONVI_PHULUC};
            }
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(Agencies);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuky;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = false;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = true;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
        }

        #endregion

        #region export
        private void OnExportFile(ExportType pDF)
        {
            if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010001_9010002 || SelectedLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002)
            {
                OnPrintReportKhcTongHopTheoDonVi(pDF);
            }
            else
            {
                OnPrintReportKPQLKCBKHACTongHopTheoDonVi(pDF);
            }
        }

        private void OnPrintReportKPQLKCBKHACTongHopTheoDonVi(ExportType exportType)
        {
            try
            {

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    List<ExportResult> results = new List<ExportResult>();
                    IsLoading = true;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    string templateFileName;
                    string fileNamePrefix;
                    var h1 = SelectedUnit.ValueItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    double? TongSoTien = 0;
                    var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.Id.ToString()).ToList());
                    string soQuyetDinh = DataDotSelected.DisplayItemOption2;
                    string ngayQuyetDinh = DataDotSelected.HiddenValue;
                    Guid? IDLoaiChi = SelectedLoaiChi.Id;
                    string sMaLoaiChi = SelectedLoaiChi.ValueItem;
                    List<Guid> lstIdChungTu = GetListChungTuReport();
                    if (lstIdChungTu.Count <= 0) return;
                    List<BhPbdtcBHXHChiBHXHReportQuery> lstBHXH = new List<BhPbdtcBHXHChiBHXHReportQuery>();
                    bool IsTongHopDonViKhoi = false;
                    int dotNhan = int.Parse(_selectDotPhanBo.ValueItem);
                    if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_DON_VI)
                    {
                        lstBHXH = _pbdtcBHXHChiTietService.GetListDataPhanBoLoaiChiKPQLKCBKHAC(yearOfWork, selectedUnits, IDLoaiChi, sMaLoaiChi, string.Join(",", lstIdChungTu), donViTinh, IsTongHopDonViKhoi, dotNhan, IsMillionRound).ToList();
                        TongSoTien = lstBHXH.Sum(x => x.FCong);
                    }
                    else
                    {
                        IsTongHopDonViKhoi = true;
                        lstBHXH = _pbdtcBHXHChiTietService.GetListDataPhanBoLoaiChiKPQLKCBKHAC(yearOfWork, selectedUnits, IDLoaiChi, sMaLoaiChi, string.Join(",", lstIdChungTu), donViTinh, IsTongHopDonViKhoi, dotNhan, IsMillionRound).ToList();
                        TongSoTien = lstBHXH.Where(x => x.IsHangCha).Sum(x => x.FCong);
                    }


                    ExtensionMethods.CheckPassElementOrGetDefault10Element(lstBHXH);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", $"{Title2} {_sessionInfo.YearOfWork}");
                    data.Add("TieuDe3", Title3);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", lstBHXH);
                    data.Add("DonVi", _sessionInfo.TenDonVi);
                    data.Add("TongTien", TongSoTien);
                    data.Add("CONG", TongSoTien);

                    data.Add("TongSoTien", TongSoTien != 0 ? StringUtils.NumberToText((double)TongSoTien * donViTinh, true) : string.Empty);
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                    string sFileName = GetFileName();
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(sFileName));

                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<BhPbdtcBHXHChiBHXHReportQuery, DonVi>(templateFileName, data);
                    results.Add(new ExportResult("KẾ HOẠCH CHI BHXH NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string GetFileName()
        {
            string sFileName = string.Empty;
            if (_selectDotPhanBo.ValueItem == DotPhanBo.Dau_Nam || _selectDotPhanBo.ValueItem == DotPhanBo.Tat_Ca)
            {
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010001_9010002 || SelectedLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_CHIBHXH_TONGHOP_DONVI_PHULUC;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010003)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_CHIKPQL_TONGHOP_DONVI_PHULUC;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010004_9010005)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_CHIKPCBQY_TONGHOP_DONVI_PHULUC;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010006_9010007)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_CHIKPCBTS_TONGHOP_DONVI_PHULUC;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_CHIKPCSSK_HSSV_NLS_TONGHOP_DONVI_PHULUC;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010008)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_DONVI_PHULUC;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010009)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_CHI_BHTN_TONGHOP_DONVI_PHULUC;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010010)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_CHI_MSTTBYT_TONGHOP_DONVI_PHULUC;
                }
            }
            else
            {
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010001_9010002 || SelectedLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_BS_CHIBHXH_TONGHOP_DONVI_PHULUC;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010003)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_BS_CHIKPQL_TONGHOP_DONVI_PHULUC;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010004_9010005)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_BS_CHIKPCBQY_TONGHOP_DONVI_PHULUC;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010006_9010007)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_BS_CHIKPCBTS_TONGHOP_DONVI_PHULUC;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_BS_CHIKPCSSK_HSSV_NLS_TONGHOP_DONVI_PHULUC;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010008)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_BS_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_DONVI_PHULUC;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010009)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_BS_CHI_BHTN_TONGHOP_DONVI_PHULUC;
                }
                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010010)
                {
                    sFileName = ExportFileName.RPT_BHXH_DT_PBC_BS_CHI_MSTTBYT_TONGHOP_DONVI_PHULUC;
                }
            }
            return sFileName;
        }

        private void OnPrintReportKhcTongHopTheoDonVi(ExportType exportType)
        {
            try
            {

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    List<ExportResult> results = new List<ExportResult>();
                    IsLoading = true;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    string templateFileName;
                    string fileNamePrefix;
                    var h1 = SelectedUnit.ValueItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    int stt = 1;

                    // sum khoi du toan
                    double? sumTroCapOmDauDT = 0;
                    double? sumTroCapThaiSanDT = 0;
                    double? sumTroCapTLLĐNNDT = 0;
                    double? sumTroCapHuuTriDT = 0;
                    double? sumTroCapPhucVienDT = 0;
                    double? sumTroCapXuatNguDT = 0;
                    double? sumTroCapThoiViecDT = 0;
                    double? sumTroCapTuTuatDT = 0;

                    //  sum khoi hach toan
                    double? sumTroCapOmDauHT = 0;
                    double? sumTroCapThaiSanHT = 0;
                    double? sumTroCapTLLĐNNHT = 0;
                    double? sumTroCapHuuTriHT = 0;
                    double? sumTroCapPhucVienHT = 0;
                    double? sumTroCapXuatNguHT = 0;
                    double? sumTroCapThoiViecHT = 0;
                    double? sumTroCapTuTuatHT = 0;
                    List<Guid> lstIdChungTu = GetListChungTuReport();
                    if (lstIdChungTu.Count <= 0) return;
                    var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.Id.ToString()).ToList());

                    string soQuyetDinh = DataDotSelected.DisplayItemOption2;
                    string ngayQuyetDinh = DataDotSelected.HiddenValue;
                    Guid? IDLoaiChi = SelectedLoaiChi.Id;
                    string sMaLoaiChi = SelectedLoaiChi.ValueItem;
                    bool IsTongHopDonViKhoi = false;
                    int dotNhan = int.Parse(_selectDotPhanBo.ValueItem);
                    List<BhPbdtcBHXHChiBHXHReportQuery> lstBHXH = new List<BhPbdtcBHXHChiBHXHReportQuery>();
                    if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_DON_VI)
                    {
                        IsTongHopDonViKhoi = false;
                        lstBHXH = _pbdtcBHXHChiTietService.GetListDataPhanBoLoaiChiBHXH(yearOfWork, selectedUnits, IDLoaiChi, sMaLoaiChi, string.Join(",", lstIdChungTu), donViTinh, IsTongHopDonViKhoi, dotNhan, IsMillionRound).ToList();
                    }
                    else
                    {
                        IsTongHopDonViKhoi = true;
                        lstBHXH = _pbdtcBHXHChiTietService.GetListDataPhanBoLoaiChiBHXH(yearOfWork, selectedUnits, IDLoaiChi, sMaLoaiChi, string.Join(",", lstIdChungTu), donViTinh, IsTongHopDonViKhoi, dotNhan, IsMillionRound).ToList();
                    }

                    if (lstBHXH.Count > 0 && SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_DON_VI)
                    {
                        // sum khoi du toan
                        var listKhoiDuToan = lstBHXH.Where(x => x.STenDonVi == KhcCheckHachToanAnDuToan.KhoiDuToan).ToList();
                        sumTroCapOmDauDT = listKhoiDuToan.Sum(x => x.FTienTroCapOmDau);
                        sumTroCapThaiSanDT = listKhoiDuToan.Sum(x => x.FTienTroCapThaiSan);
                        sumTroCapTLLĐNNDT = listKhoiDuToan.Sum(x => x.FTienTroCapTaiNan);
                        sumTroCapHuuTriDT = listKhoiDuToan.Sum(x => x.FTienTroCapHuuTri);
                        sumTroCapPhucVienDT = listKhoiDuToan.Sum(x => x.FTienTroCapPhucVien);
                        sumTroCapXuatNguDT = listKhoiDuToan.Sum(x => x.FTienTroCapXuatNgu);
                        sumTroCapThoiViecDT = listKhoiDuToan.Sum(x => x.FTienTroCapThoiViec);
                        sumTroCapTuTuatDT = listKhoiDuToan.Sum(x => x.FTienTroCapTuTuat);

                        // sum khoi hach toan
                        var listKhoiHachToan = lstBHXH.Where(x => x.STenDonVi == KhcCheckHachToanAnDuToan.KhoiHoachToan).ToList();
                        sumTroCapOmDauHT = listKhoiHachToan.Sum(x => x.FTienTroCapOmDau);
                        sumTroCapThaiSanHT = listKhoiHachToan.Sum(x => x.FTienTroCapThaiSan);
                        sumTroCapTLLĐNNHT = listKhoiHachToan.Sum(x => x.FTienTroCapTaiNan);
                        sumTroCapHuuTriHT = listKhoiHachToan.Sum(x => x.FTienTroCapHuuTri);
                        sumTroCapPhucVienHT = listKhoiHachToan.Sum(x => x.FTienTroCapPhucVien);
                        sumTroCapXuatNguHT = listKhoiHachToan.Sum(x => x.FTienTroCapXuatNgu);
                        sumTroCapThoiViecHT = listKhoiHachToan.Sum(x => x.FTienTroCapThoiViec);
                        sumTroCapTuTuatHT = listKhoiHachToan.Sum(x => x.FTienTroCapTuTuat);
                    }

                    if (lstBHXH.Count > 0 && SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_DON_VI_THEO_KHOI)
                    {
                        // sum khoi du toan
                        var listKhoiDuToan = lstBHXH.Where(x => x.STenDonVi == KhcCheckHachToanAnDuToan.KhoiDuToan && x.RowNumber == 0).ToList();
                        sumTroCapOmDauDT = listKhoiDuToan.Sum(x => x.FTienTroCapOmDau);
                        sumTroCapThaiSanDT = listKhoiDuToan.Sum(x => x.FTienTroCapThaiSan);
                        sumTroCapTLLĐNNDT = listKhoiDuToan.Sum(x => x.FTienTroCapTaiNan);
                        sumTroCapHuuTriDT = listKhoiDuToan.Sum(x => x.FTienTroCapHuuTri);
                        sumTroCapPhucVienDT = listKhoiDuToan.Sum(x => x.FTienTroCapPhucVien);
                        sumTroCapXuatNguDT = listKhoiDuToan.Sum(x => x.FTienTroCapXuatNgu);
                        sumTroCapThoiViecDT = listKhoiDuToan.Sum(x => x.FTienTroCapThoiViec);
                        sumTroCapTuTuatDT = listKhoiDuToan.Sum(x => x.FTienTroCapTuTuat);

                        // sum khoi hach toan
                        var listKhoiHachToan = lstBHXH.Where(x => x.STenDonVi == KhcCheckHachToanAnDuToan.KhoiHoachToan && x.RowNumber == 0).ToList();
                        sumTroCapOmDauHT = listKhoiHachToan.Sum(x => x.FTienTroCapOmDau);
                        sumTroCapThaiSanHT = listKhoiHachToan.Sum(x => x.FTienTroCapThaiSan);
                        sumTroCapTLLĐNNHT = listKhoiHachToan.Sum(x => x.FTienTroCapTaiNan);
                        sumTroCapHuuTriHT = listKhoiHachToan.Sum(x => x.FTienTroCapHuuTri);
                        sumTroCapPhucVienHT = listKhoiHachToan.Sum(x => x.FTienTroCapPhucVien);
                        sumTroCapXuatNguHT = listKhoiHachToan.Sum(x => x.FTienTroCapXuatNgu);
                        sumTroCapThoiViecHT = listKhoiHachToan.Sum(x => x.FTienTroCapThoiViec);
                        sumTroCapTuTuatHT = listKhoiHachToan.Sum(x => x.FTienTroCapTuTuat);
                    }


                    var TongSoTienDT = (sumTroCapOmDauDT + sumTroCapThaiSanDT + sumTroCapTLLĐNNDT
                    + sumTroCapHuuTriDT + sumTroCapXuatNguDT + sumTroCapThoiViecDT
                    + sumTroCapTuTuatDT + sumTroCapPhucVienDT);

                    var TongSoTienHT = (sumTroCapOmDauHT + sumTroCapThaiSanHT + sumTroCapTLLĐNNHT
                    + sumTroCapHuuTriHT + sumTroCapXuatNguHT + sumTroCapThoiViecHT
                    + sumTroCapTuTuatHT + sumTroCapPhucVienHT);

                    var TongSoTien = TongSoTienDT + TongSoTienHT;
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);


                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2 + _sessionInfo.YearOfWork);
                    data.Add("TieuDe3", Title3);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", lstBHXH);

                    data.Add("sumTroCapOmDauDT", sumTroCapOmDauDT);
                    data.Add("sumTroCapThaiSanDT", sumTroCapThaiSanDT);
                    data.Add("sumTroCapTLLĐNNDT", sumTroCapTLLĐNNDT);
                    data.Add("sumTroCapHuuTriDT", sumTroCapHuuTriDT);
                    data.Add("sumTroCapPhucVienDT", sumTroCapPhucVienDT);
                    data.Add("sumTroCapXuatNguDT", sumTroCapXuatNguDT);
                    data.Add("sumTroCapThoiViecDT", sumTroCapThoiViecDT);
                    data.Add("sumTroCapTuTuatDT", sumTroCapTuTuatDT);

                    data.Add("sumTroCapOmDauHT", sumTroCapOmDauHT);
                    data.Add("sumTroCapThaiSanHT", sumTroCapThaiSanHT);
                    data.Add("sumTroCapTLLĐNNHT", sumTroCapTLLĐNNHT);
                    data.Add("sumTroCapHuuTriHT", sumTroCapHuuTriHT);
                    data.Add("sumTroCapPhucVienHT", sumTroCapPhucVienHT);
                    data.Add("sumTroCapXuatNguHT", sumTroCapXuatNguHT);
                    data.Add("sumTroCapThoiViecHT", sumTroCapThoiViecHT);
                    data.Add("sumTroCapTuTuatHT", sumTroCapTuTuatHT);
                    // Tổng cộng
                    data.Add("sumTongCongTroCapOmDau", sumTroCapOmDauDT + sumTroCapOmDauHT);
                    data.Add("sumTongCongTroCapThaiSan", sumTroCapThaiSanDT + sumTroCapThaiSanHT);
                    data.Add("sumTongCongTroCapTLLĐNN", sumTroCapTLLĐNNDT + sumTroCapTLLĐNNHT);
                    data.Add("sumTongCongTroCapHuuTri", sumTroCapHuuTriDT + sumTroCapHuuTriHT);
                    data.Add("sumTongCongTroCapPhucVien", sumTroCapPhucVienDT + sumTroCapPhucVienHT);
                    data.Add("sumTongCongTroCapXuatNgu", sumTroCapXuatNguDT + sumTroCapXuatNguHT);
                    data.Add("sumTongCongTroCapThoiViec", sumTroCapThoiViecDT + sumTroCapThoiViecHT);
                    data.Add("sumTongCongTroCapTuTuat", sumTroCapTuTuatDT + sumTroCapTuTuatHT);
                    data.Add("TongSoTienDT", TongSoTienDT);
                    data.Add("TongSoTienHT", TongSoTienHT);
                    data.Add("DonVi", _sessionInfo.TenDonVi);
                    data.Add("TongTien", TongSoTien);

                    data.Add("TongSoTien", TongSoTien != 0 ? StringUtils.NumberToText((double)TongSoTien * donViTinh, true) : string.Empty);
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                    string sFileName = GetFileName();
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(sFileName));

                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<BhPbdtcBHXHChiBHXHReportQuery, DonVi>(templateFileName, data);
                    results.Add(new ExportResult("KẾ HOẠCH CHI BHXH NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string GetTemplate(string input)
        {

            if (SelectedKieuGiayIn.ValueItem == "1")
                input = input + "_Doc";
            return Path.Combine(ExportPrefix.PATH_BH_DT_DTCPBCL, input + FileExtensionFormats.Xlsx);
        }

        //private string GetTemplateQLKPKCBK(string input)
        //{
        //    if (SelectedKieuGiayIn.ValueItem == "1")
        //        input = input + "_Doc";
        //    return Path.Combine(ExportPrefix.PATH_BH_DT_DTCPBCL, input + FileExtensionFormats.Xlsx);
        //}

        //private void GetDataPhanBoDTTheoDonVi(ExportType exportType)
        //{
        //    try
        //    {
        //        BackgroundWorkerHelper.Run((s, e) =>
        //        {
        //            IsLoading = true;
        //            int yearOfWork = _sessionService.Current.YearOfWork;
        //            int donViTinh = int.Parse(SelectedUnit.ValueItem);
        //            var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
        //            if (lstIdDonVi != null)
        //            {
        //                var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.Id.ToString()).ToList());
        //                List<ExportResult> results = new List<ExportResult>();
        //                string soQuyetDinh = DataDotSelected.ValueItem;
        //                string ngayQuyetDinh = DataDotSelected.HiddenValue;
        //                Guid? IDLoaiChi = SelectedLoaiChi.Id;
        //                string sMaLoaiChi = SelectedLoaiChi.ValueItem;
        //                List<BhPbdtcBHXHChiBHXHReportQuery> lstBHXH = new List<BhPbdtcBHXHChiBHXHReportQuery>();
        //                if (SelectedLoaiChi.HiddenValue == LNSValue.LNS_9010001_9010002)
        //                {
        //                    lstBHXH = _pbdtcBHXHChiTietService.GetListDataPhanBoLoaiChiBHXH(yearOfWork, selectedUnits, IDLoaiChi, sMaLoaiChi, ngayQuyetDinh, soQuyetDinh, donViTinh).ToList();
        //                }
        //                else
        //                {
        //                    lstBHXH = _pbdtcBHXHChiTietService.GetListDataPhanBoLoaiChiKPQLKCBKHAC(yearOfWork, selectedUnits, IDLoaiChi, sMaLoaiChi, ngayQuyetDinh, soQuyetDinh, donViTinh).ToList();
        //                }


        //                Dictionary<string, object> data = new Dictionary<string, object>();
        //                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

        //                data.Add("YearOfWork", yearOfWork);
        //                data.Add("FormatNumber", formatNumber);
        //                data.Add("DiaDiem", string.Empty);
        //                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
        //                data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
        //                data.Add("DonVi", _sessionInfo.TenDonVi);
        //                data.Add("Title1", Title1);
        //                data.Add("Title2", Title2);
        //                data.Add("Title3", Title3);
        //                //Tính tổng cộng

        //                AddChuKy(data, _typeChuky);
        //                string templateFileName;
        //                //templateFileName = GetTemplate();
        //                templateFileName = string.Empty;
        //                string fileNamePrefix;
        //                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
        //                string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
        //                var xlsFile = _exportService.Export<BhPbdtcBHXHChiBHXHReportQuery>(templateFileName, data);
        //                results.Add(new ExportResult("BÁO CÁO DỰ TOÁN TỔNG HỢP THU CHI " + _sessionInfo.YearOfWork, filename, null, xlsFile));
        //                e.Result = results;
        //            }

        //        }, (s, e) =>
        //        {
        //            if (e.Error == null)
        //            {
        //                var result = (List<ExportResult>)e.Result;
        //                _exportService.Open(result, exportType);
        //            }
        //            else
        //            {
        //                _logger.Error(e.Error.Message);
        //            }
        //            IsLoading = false;
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message, ex);
        //    }
        //}

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh1MoTa);
            data.Add("ChucDanh1", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh1MoTa);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy == null ? string.Empty : dmChyKy.Ten1MoTa);

            data.Add("ThuaLenh2", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh2MoTa);
            data.Add("ChucDanh2", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh2MoTa);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy == null ? string.Empty : dmChyKy.Ten2MoTa);

            data.Add("ThuaLenh3", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh3MoTa);
            data.Add("ChucDanh3", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh3MoTa);
            data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten3", dmChyKy == null ? string.Empty : dmChyKy.Ten3MoTa);

            data.Add("ThuaLenh4", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh4MoTa);
            data.Add("ChucDanh4", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh4MoTa);
            data.Add("GhiChuKy4", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten4", dmChyKy == null ? string.Empty : dmChyKy.Ten4MoTa);

            data.Add("ThuaLenh5", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh5MoTa);
            data.Add("ChucDanh5", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh5MoTa);
            data.Add("GhiChuKy5", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten5", dmChyKy == null ? string.Empty : dmChyKy.Ten5MoTa);

            data.Add("ThuaLenh6", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh6MoTa);
            data.Add("ChucDanh6", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh6MoTa);
            data.Add("GhiChuKy6", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten6", dmChyKy == null ? string.Empty : dmChyKy.Ten6MoTa);
        }
        #endregion
    }
}
