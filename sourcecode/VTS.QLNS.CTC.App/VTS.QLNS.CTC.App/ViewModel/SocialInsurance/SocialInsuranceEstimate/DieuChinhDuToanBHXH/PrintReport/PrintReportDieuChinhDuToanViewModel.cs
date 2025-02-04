using AutoMapper;
using ControlzEx.Standard;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH.PrintReport
{
    public class PrintReportDieuChinhDuToanViewModel : ViewModelBase
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private ICollectionView _listAgency;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly IBhDtcDcdToanChiService _bhDtcDcdToanChiService;
        private readonly IBhDtcDcdToanChiChiTietService _bhDtcDcdToanChiChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhMucLoaiChiService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly ILog _logger;
        private readonly IDanhMucService _danhMucService;
        private readonly IBhBaoCaoGhiChuService _bhGhiChuService;

        #endregion

        #region Property
        private SessionInfo _sessionInfo;
        private string _diaDiem;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private string _cap1;
        private DmChuKy _dmChuKy;
        private string _typeChuky;
        string templateFileName;
        string fileNamePrefix;
        private bool _checkAllAgencies;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        public override string Name => "BÁO CÁO ĐIỀU CHỈNH DỰ TOÁN - THEO DƠN VỊ";
        public override string Title => "BÁO CÁO ĐIỀU CHỈNH DỰ TOÁN - THEO DƠN VỊ";
        public override string Description => "Báo cáo điều chỉnh dự toán - theo đơn vị" + _sessionService.Current.YearOfWork;
        public override Type ContentType => typeof(PrintReportDieuChinhDuToanChiTiet);
        public static DtDcDtCheckPrintType dtDcDtCheckPrintType { get; set; }
        public string TitleFirst;
        public string TitleSecond;

        private string _txtTitleFirst;
        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set
            {
                SetProperty(ref _txtTitleFirst, value);
            }
        }
        private string _txtTitleSecond;

        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }
        private string _txtTitleThird;

        public string TxtTitleThird
        {
            get => _txtTitleThird;
            set
            {

                SetProperty(ref _txtTitleThird, value);
            }
        }
        public bool IsShowInToi => ItemsLoaiBaoCao != null && SelectedLoaiBaoCao.ValueItem == AdjustSummaryReportType.AgencyDetail.ToString();

        #region list agency
        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
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
            get => Agencies != null && Agencies.Where(x => x.IsFilter).All(x => x.Selected);
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

        public bool IsExportEnable => Agencies != null && Agencies.Any(x => x.Selected);

        public bool IsSummary { get; set; }

        public bool IsLoaiKCB { get; set; }
        public Visibility IsTitleType => IsSummary ? Visibility.Visible : Visibility.Collapsed;
        public bool IsEnabled => !IsSummary;
        public Visibility IsVisibilityLoaiKCB => IsLoaiKCB ? Visibility.Visible : Visibility.Collapsed;

        private ObservableCollection<ComboboxItem> _paperPrintTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> PaperPrintTypes
        {
            get => _paperPrintTypes;
            set => SetProperty(ref _paperPrintTypes, value);
        }

        private bool _isInTheoTongHop;
        public bool IsInTheoTongHop
        {
            get => _isInTheoTongHop;
            set
            {
                SetProperty(ref _isInTheoTongHop, value);
                LoadDonVi();

            }
        }

        private ComboboxItem _selectedDanhMucLoaiChi;
        public ComboboxItem SelectedDanhMucLoaiChi
        {
            get => _selectedDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selectedDanhMucLoaiChi, value);
                if (_selectedDanhMucLoaiChi != null)
                {
                    LoadDonVi();
                    if (_selectedLoaiBaoCao != null)
                    {
                        LoadTypeChuKy(_selectedDanhMucLoaiChi.HiddenValue);
                    }

                }
            }
        }
        private ObservableCollection<ComboboxItem> _itemsDanhMucLoaiChi;
        public ObservableCollection<ComboboxItem> ItemsDanhMucLoaiChi
        {
            get => _itemsDanhMucLoaiChi;
            set => SetProperty(ref _itemsDanhMucLoaiChi, value);

        }

        private List<ComboboxItem> _itemsLoaiBaoCao;
        public List<ComboboxItem> ItemsLoaiBaoCao
        {
            get => _itemsLoaiBaoCao;
            set => SetProperty(ref _itemsLoaiBaoCao, value);
        }

        private ComboboxItem _selectedLoaiBaoCao;
        public ComboboxItem SelectedLoaiBaoCao
        {
            get => _selectedLoaiBaoCao;
            set
            {
                SetProperty(ref _selectedLoaiBaoCao, value);
                OnPropertyChanged(nameof(IsShowInToi));
                if (_selectedLoaiBaoCao != null)
                {
                    LoadDonVi();
                    LoadTypeChuKy(_selectedDanhMucLoaiChi.HiddenValue);
                }
            }
        }

        private List<ComboboxItem> _itemsInToi;
        public List<ComboboxItem> ItemsInToi
        {
            get => _itemsInToi;
            set => SetProperty(ref _itemsInToi, value);
        }

        private ComboboxItem _selectedInToi;
        public ComboboxItem SelectedInToi
        {
            get => _selectedInToi;
            set
            {
                SetProperty(ref _selectedInToi, value);
            }
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

        private ObservableCollection<ComboboxItem> _catUnitTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> CatUnitTypes
        {
            get => _catUnitTypes;
            set => SetProperty(ref _catUnitTypes, value);
        }

        private ComboboxItem _catUnitTypeSelected;

        public ComboboxItem CatUnitTypeSelected
        {
            get => _catUnitTypeSelected;
            set => SetProperty(ref _catUnitTypeSelected, value);
        }

        public IEnumerable<DonVi> ListUnit { get; set; }
        public bool IsShowInTheoTongHop { get; set; }

        #endregion

        #region RelayCommand
        private BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel { get; set; }
        public RelayCommand NoteCommand { get; }
        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        #endregion

        #region Constructor
        public PrintReportDieuChinhDuToanViewModel(ISessionService sessionService,
            IMapper mapper,
            INsDonViService nsDonViService,
            IBhDtcDcdToanChiService bhDtcDcdToanChiService,
            IBhDtcDcdToanChiChiTietService bhDtcDcdToanChiChiTietService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            IDmChuKyService dmChuKyService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            ILog log,
            IDanhMucService danhMucService,
            IExportService exportService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IBhBaoCaoGhiChuService bhBaoCaoGhiChuService,
            BhBaoCaoGhiChuDialogViewModel bhBaoCaoGhiChuDialogViewModel
            )
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _nsDonViService = nsDonViService;
            _bhDtcDcdToanChiService = bhDtcDcdToanChiService;
            _bhDtcDcdToanChiChiTietService = bhDtcDcdToanChiChiTietService;
            _bhMucLoaiChiService = bhDanhMucLoaiChiService;
            _dmChuKyService = dmChuKyService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _logger = log;
            _danhMucService = danhMucService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _exportService = exportService;
            _bhGhiChuService = bhBaoCaoGhiChuService;
            BhBaoCaoGhiChuDialogViewModel = bhBaoCaoGhiChuDialogViewModel;
            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportPdfActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
        }
        #endregion

        #region Add chu ky
        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TxtTitleFirst = chuKy.TieuDe1MoTa;
                TxtTitleSecond = chuKy.TieuDe2MoTa;
                TxtTitleThird = chuKy.TieuDe3MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
        #endregion

        #region Init
        public override void Init()
        {
            _sessionInfo = _sessionService.Current;

            InitReportDefaultDate();
            Clear();
            LoadDanhMucLoaiChi();
            LoadLoaiBaoCao();
            LoadInToi();
            LoadTitleFirst();
            LoadCatUnitTypes();
            LoadDiaDiem();
            LoadKieuGiayIn();
            LoadDonVi();
            LoadTypeChuKy(_selectedDanhMucLoaiChi.HiddenValue);
        }
        #endregion Note

        #region Note
        private void OnNoteCommand()
        {
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();
            BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_BHXH_DT_DCDT_CHIBHXH_TONGHOP_CHITIETDONVI,
                                                                            TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPQL_TONGHOP_CHITIETDONVI,
                                                                            TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPKCB_QUANY_TONGHOP_CHITIETDONVI,
                                                                            TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPKCB_TS_TONGHOP_CHITIETDONVI,
                                                                            TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPCSSK_HSSV_NLD_TONGHOP_CHITIETDONVI,
                                                                            TypeChuKy.RPT_BHXH_DT_DCDT_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_CHITIETDONVI,
                                                                            TypeChuKy.RPT_BHXH_DT_DCDT_BHTN_TONGHOP_CHITIETDONVI,
                                                                            TypeChuKy.RPT_BHXH_DT_DCDT_MSTTBYT_TONGHOP_CHITIETDONVI };
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(Agencies);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuky;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = true;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = false;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
        }
        #endregion

        #region Export
        private void OnExport(ExportType exportType)
        {
            if (SelectedLoaiBaoCao.ValueItem == AdjustSummaryReportType.AgencyDetail.ToString())
            {
                OnPrintReportDieuChinhDuToanChiTiet(exportType);
            }
            else
            {
                OnPrintReportDieuChinhDuToanTongHop(exportType);
            }
        }

        private void LoadTypeChuKy(string sLNSForDanhMuc)
        {
            if (SelectedLoaiBaoCao.ValueItem == AdjustSummaryReportType.AgencyDetail.ToString())
            {
                switch (sLNSForDanhMuc.Trim())
                {
                    case LNSValue.LNS_9010001_9010002:
                    case LNSValue.LNS_901_9010001_9010002:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIBHXH_CHITIET;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoBHXH;
                        break;
                    case LNSValue.LNS_9010003:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPQL_CHITIET;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoKPQL;
                        break;

                    case LNSValue.LNS_9010004_9010005:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPKCB_QUANY_CHITIET;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoKCBQY;
                        break;

                    case LNSValue.LNS_9010006_9010007:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPKCB_TS_CHITIET;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoKCBTS;
                        break;

                    case LNSValue.LNS_9050001_9050002:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPCSSK_HSSV_NLD_CHITIET;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoCSSKHSSV;
                        break;
                    case LNSValue.LNS_9010008:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHITNKDQ_KCBBHYT_QUANNHAN_CHITIET;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoTNKCBBHYTQN;
                        break;
                    case LNSValue.LNS_9010009:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_MSTTBYT_CHITIET;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoHTBHTN;
                        break;
                    case LNSValue.LNS_9010010:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_BHTN_CHITIET;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoMSTTBYT;
                        break;
                    default:
                        _typeChuky = string.Empty;
                        break;

                }
            }

            else
            {
                switch (sLNSForDanhMuc.Trim())
                {
                    case LNSValue.LNS_9010001_9010002:
                    case LNSValue.LNS_901_9010001_9010002:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIBHXH_TONGHOP_CHITIETDONVI;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoBHXH;
                        break;
                    case LNSValue.LNS_9010003:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPQL_TONGHOP_CHITIETDONVI;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoKPQL;
                        break;

                    case LNSValue.LNS_9010004_9010005:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPKCB_QUANY_TONGHOP_CHITIETDONVI;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoKCBQY;
                        break;

                    case LNSValue.LNS_9010006_9010007:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPKCB_TS_TONGHOP_CHITIETDONVI;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoKCBTS;
                        break;

                    case LNSValue.LNS_9050001_9050002:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPCSSK_HSSV_NLD_TONGHOP_CHITIETDONVI;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoCSSKHSSV;
                        break;
                    case LNSValue.LNS_9010008:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_CHITIETDONVI;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoTNKCBBHYTQN;
                        break;
                    case LNSValue.LNS_9010009:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_BHTN_TONGHOP_CHITIETDONVI;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoHTBHTN;
                        break;
                    case LNSValue.LNS_9010010:
                        _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_MSTTBYT_TONGHOP_CHITIETDONVI;
                        TitleFirst = EstimateTitlePrint.Title1BaoCao;
                        TitleSecond = EstimateTitlePrint.Title2BaoCaoMSTTBYT;
                        break;
                    default:
                        _typeChuky = string.Empty;
                        break;

                }
            }


            LoadTitleFirst();
        }


        private string GetFileTemplate(string sLNSForDanhMuc)
        {
            string sFileName = string.Empty;
            if (SelectedLoaiBaoCao.ValueItem == AdjustSummaryReportType.AgencyDetail.ToString())
            {
                switch (sLNSForDanhMuc.Trim())
                {
                    case LNSValue.LNS_9010001_9010002:
                    case LNSValue.LNS_901_9010001_9010002:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_CHIBHXH_CHITIET;
                        break;
                    case LNSValue.LNS_9010003:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_CHIKPQL_CHITIET;
                        break;
                    case LNSValue.LNS_9010004_9010005:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_CHIKPKCB_QUANY_CHITIET;

                        break;
                    case LNSValue.LNS_9010006_9010007:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_CHIKPKCB_TS_CHITIET;
                        break;
                    case LNSValue.LNS_9050001_9050002:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_CHIKPCSSK_HSSV_NLD_CHITIET;
                        break;
                    case LNSValue.LNS_9010008:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_CHITNKDQ_KCBBHYT_QUANNHAN_CHITIET;
                        break;
                    case LNSValue.LNS_9010009:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_BHTN_CHITIET;
                        break;
                    case LNSValue.LNS_9010010:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_MSTTBYT_CHITIET;
                        break;
                    default:
                        sFileName = string.Empty;
                        break;

                }
            }
            else
            {
                switch (sLNSForDanhMuc.Trim())
                {
                    case LNSValue.LNS_9010001_9010002:
                    case LNSValue.LNS_901_9010001_9010002:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_CHIBHXH_TONGHOP_CHITIETDONVI;
                        break;
                    case LNSValue.LNS_9010003:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_CHIKPQL_TONGHOP_CHITIETDONVI;
                        break;
                    case LNSValue.LNS_9010004_9010005:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_CHIKPKCB_QUANY_TONGHOP_CHITIETDONVI;
                        break;
                    case LNSValue.LNS_9010006_9010007:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_CHIKPKCB_TS_TONGHOP_CHITIETDONVI;
                        break;
                    case LNSValue.LNS_9050001_9050002:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_CHIKPCSSK_HSSV_NLD_TONGHOP_CHITIETDONVI;
                        break;
                    case LNSValue.LNS_9010008:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_CHITIETDONVI;
                        break;
                    case LNSValue.LNS_9010009:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_BHTN_TONGHOP_CHITIETDONVI;
                        break;
                    case LNSValue.LNS_9010010:
                        sFileName = ExportFileName.RPT_BHXH_DT_DCDT_MSTTBYT_TONGHOP_CHITIETDONVI;
                        break;
                    default:
                        sFileName = string.Empty;
                        break;
                }
            }
            return sFileName;
        }

        private string GetLevelTitle(DmChuKy dmChuKy, int level)
        {
            if (dmChuKy == null) return string.Empty;
            var loaiDVBanHanh = dmChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty;
            var danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

            return loaiDVBanHanh switch
            {
                LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => string.Empty,
                LoaiDonViBanHanh.TUY_CHINH => dmChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
        }

        private void LoadDonVi()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                var yearOfWork = _sessionInfo.YearOfWork;

                List<DonVi> lstDonVis = new List<DonVi>();
                if (IsShowInTheoTongHop)
                {
                    lstDonVis = _bhDtcDcdToanChiService.FindByDonViForNamLamViec(yearOfWork, SelectedDanhMucLoaiChi.Id).ToList();
                    if (!IsInTheoTongHop)
                        lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                    else
                        lstDonVis = lstDonVis.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();
                }
                else
                {
                    lstDonVis = _bhDtcDcdToanChiService.FindByDonViForNamLamViec(yearOfWork, SelectedDanhMucLoaiChi.Id).ToList();
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

        private bool ListAgencyFilter(object obj)
        {
            bool result = true;
            var item = (AgencyModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchAgencyText))
                result = item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void OnPrintReportDieuChinhDuToanChiTiet(ExportType exportType)
        {

            BackgroundWorkerHelper.Run((s, e) =>
            {
                var currentDonVi = GetNsDonViOfCurrentUser();
                var yearOfWork = _sessionService.Current.YearOfWork;
                string sCap1 = GetLevelTitle(_dmChuKy, 1);
                string sCap2 = GetLevelTitle(_dmChuKy, 2);
                List<ExportResult> results = new List<ExportResult>();
                var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.Id).ToList();
                IsLoading = true;
                string sFileName = string.Empty;
                int dvt = Convert.ToInt32(CatUnitTypeSelected.ValueItem);
                foreach (var item in lstIdDonVi)
                {
                    DonVi donViChild = _nsDonViService.FindByIdDonVi(item, yearOfWork);
                    BhDtcDcdToanChiModel chungTu = new BhDtcDcdToanChiModel();
                    var searchCondition = new BhDtcDcdToanChiChiTietCriteria
                    {
                        LNS = SelectedDanhMucLoaiChi.HiddenValue,
                        NamLamViec = _sessionInfo.YearOfWork,
                        IdDonVi = item,
                        ILoaiDanhMucChi = SelectedDanhMucLoaiChi.Id,
                        NgayChungTu = DateTime.Now,
                        DonViTinh = dvt,
                        LoaiChungTu = donViChild.Loai == LoaiDonVi.ROOT ? 1 : 0,
                        MaLoaiChi = SelectedDanhMucLoaiChi.ValueItem
                    };

                    var dtcDcdToanChiModelsSummary = _bhDtcDcdToanChiChiTietService.GetDataForAgency(searchCondition).ToList();
                    //var lstDtcDcdToanChiModelsSummary = _mapper.Map<ObservableCollection<BhDtcDcdToanChiChiTietModel>>(dtcDcdToanChiModelsSummary).ToList();

                    CalculateDataDuToan(dtcDcdToanChiModelsSummary);

                    CalculateDataChiTiet(dtcDcdToanChiModelsSummary);

                    dtcDcdToanChiModelsSummary.ForEach(x =>
                    {
                        x.BHangCha = x.IsHangCha;
                        if (x.BHangChaDuToan.HasValue)
                        {
                            x.FTienUocThucHienCaNam = x.FTienThucHien06ThangDauNam + x.FTienUocThucHien06ThangCuoiNam;
                            x.FTienSoSanhTang = (x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao) > 0 ? x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao : 0;
                            x.FTienSoSanhGiam = (x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam) > 0 ? x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam : 0;
                        }
                        else
                        {
                            x.FTienSoSanhTang = 0;
                            x.FTienSoSanhGiam = 0;
                        }
                        //x.BHangCha = x.IsHangCha;
                        //x.FTienUocThucHienCaNam = x.FTienThucHien06ThangDauNam + x.FTienUocThucHien06ThangCuoiNam;
                        //x.FTienSoSanhTang = (!string.IsNullOrEmpty(x.SDuToanChiTietToi) || string.IsNullOrEmpty(x.SM)) ? ((x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao) > 0 ? x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao : 0) : 0;
                        //x.FTienSoSanhGiam = (!string.IsNullOrEmpty(x.SDuToanChiTietToi) || string.IsNullOrEmpty(x.SM)) ? ((x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam) > 0 ? x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam : 0) : 0;
                        //if (!string.IsNullOrEmpty(x.STM))
                        //{
                        //    x.FTienSoSanhTang = 0;
                        //    x.FTienSoSanhGiam = 0;
                        //}
                    });

                    sFileName = GetFileTemplate(_selectedDanhMucLoaiChi.HiddenValue);

                    if (SelectedInToi.ValueItem == "0")
                    {
                        dtcDcdToanChiModelsSummary = dtcDcdToanChiModelsSummary.Where(x => x.BHangChaDuToan.HasValue).ToList();
                        dtcDcdToanChiModelsSummary.ForEach(x =>
                        {
                            x.BHangCha = x.BHangChaDuToan.Value;
                            x.IsHangCha = x.BHangChaDuToan.Value;
                        });

                    }

                    var SumFTienDuToanDuocGiao = dtcDcdToanChiModelsSummary.Where(x => x.BHangChaDuToan.HasValue && !x.BHangChaDuToan.Value).Sum(x => x.FTienDuToanDuocGiao);
                    var SumFTienThucHien06ThangDauNam = dtcDcdToanChiModelsSummary.Where(x => x.BHangChaDuToan.HasValue && !x.BHangChaDuToan.Value).Sum(x => x.FTienThucHien06ThangDauNam);
                    var SumFTienUocThucHien06ThangCuoiNam = dtcDcdToanChiModelsSummary.Where(x => x.BHangChaDuToan.HasValue && !x.BHangChaDuToan.Value).Sum(x => x.FTienUocThucHien06ThangCuoiNam);
                    var SumFTienUocThucHienCaNam = dtcDcdToanChiModelsSummary.Where(x => x.BHangChaDuToan.HasValue && !x.BHangChaDuToan.Value).Sum(x => x.FTienUocThucHienCaNam);
                    //var SumFTienSoSanhTang = dtcDcdToanChiModelsSummary.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTienSoSanhTang);
                    //var SumFTienSoSanhGiam = dtcDcdToanChiModelsSummary.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTienSoSanhGiam);

                    var SumFTienSoSanhTang = dtcDcdToanChiModelsSummary.Select(x => x.FTienSoSanhTang).FirstOrDefault();
                    var SumFTienSoSanhGiam = dtcDcdToanChiModelsSummary.Select(x => x.FTienSoSanhGiam).FirstOrDefault();
                    var TongSoTien = SumFTienUocThucHienCaNam;

                    dtcDcdToanChiModelsSummary = dtcDcdToanChiModelsSummary.Where(x => x.IsDataNotNull).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();

                    FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond + " " + _sessionInfo.YearOfWork);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                    data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                    data.Add("DonVi", donViChild.TenDonVi);
                    data.Add("SumFTienDuToanDuocGiao", SumFTienDuToanDuocGiao);
                    data.Add("SumFTienThucHien06ThangDauNam", SumFTienThucHien06ThangDauNam);
                    data.Add("SumFTienUocThucHien06ThangCuoiNam", SumFTienUocThucHien06ThangCuoiNam);
                    data.Add("SumFTienUocThucHienCaNam", SumFTienUocThucHienCaNam);
                    data.Add("SumFTienSoSanhTang", SumFTienSoSanhTang);
                    data.Add("SumFTienSoSanhGiam", SumFTienSoSanhGiam);
                    data.Add("ListData", dtcDcdToanChiModelsSummary);
                    data.Add("SKTML", dtcDcdToanChiModelsSummary);
                    data.Add("TongSoTien", TongSoTien != 0 ? StringUtils.NumberToText((double)TongSoTien, true) : string.Empty);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    AddChuKy(data, _typeChuky);
                    data.Add("ThoiGian", _diaDiem + "," + DateUtils.FormatDateReport(ReportDate));
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork, item);
                    templateFileName = string.Empty;
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(sFileName));
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.ConvertVN(fileNamePrefix + "_" + donViChild.TenDonVi);
                    var xlsFile = _exportService.Export<BhDtcDcdToanChiChiTietQuery, BhDmMucLucNganSach>(templateFileName, data);
                    results.Add(new ExportResult("DỰ TOÁN ĐIỀU CHỈNH " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                }
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

        private void OnPrintReportDieuChinhDuToanTongHop(ExportType exportType)
        {

            BackgroundWorkerHelper.Run((s, e) =>
            {
                var currentDonVi = GetNsDonViOfCurrentUser();
                var yearOfWork = _sessionService.Current.YearOfWork;
                string sCap1 = GetLevelTitle(_dmChuKy, 1);
                string sCap2 = GetLevelTitle(_dmChuKy, 2);
                List<ExportResult> results = new List<ExportResult>();
                var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.Id).ToList();
                IsLoading = true;
                string sFileName = string.Empty;
                int dvt = Convert.ToInt32(CatUnitTypeSelected.ValueItem);
                var lstReport = new List<BhDtcDcdToanChiChiTietModel>();
                var lstReportQuery = new List<BhDtcDcdToanChiChiTietQuery>();
                if (SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9010001_9010002 || SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002)
                {
                    var searchCondition = new BhDtcDcdToanChiChiTietCriteria
                    {
                        LNS = SelectedDanhMucLoaiChi.HiddenValue,
                        NamLamViec = _sessionInfo.YearOfWork,
                        IdDonVi = string.Join(",", lstIdDonVi),
                        ILoaiDanhMucChi = SelectedDanhMucLoaiChi.Id,
                        NgayChungTu = DateTime.Now,
                        ILoaiTongHop = IsInTheoTongHop ? 1 : 0,
                        MaLoaiChi = SelectedDanhMucLoaiChi.ValueItem,
                        DonViTinh = dvt,
                    };
                    var dtcDcdToanChiModelsSummary = _bhDtcDcdToanChiChiTietService.GetDataAggregateForAgency(searchCondition).ToList();
                    //lstReport = _mapper.Map<ObservableCollection<BhDtcDcdToanChiChiTietModel>>(dtcDcdToanChiModelsSummary).ToList();

                    CalculateData(dtcDcdToanChiModelsSummary);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    sFileName = GetFileTemplate(_selectedDanhMucLoaiChi.HiddenValue);


                    var SumFTienDuToanDuocGiao = dtcDcdToanChiModelsSummary.Where(x => !x.BHangCha).Sum(x => x.FTienDuToanDuocGiao);
                    var SumFTienThucHien06ThangDauNam = dtcDcdToanChiModelsSummary.Where(x => !x.BHangCha).Sum(x => x.FTienThucHien06ThangDauNam);
                    var SumFTienUocThucHien06ThangCuoiNam = dtcDcdToanChiModelsSummary.Where(x => !x.BHangCha).Sum(x => x.FTienUocThucHien06ThangCuoiNam);
                    var SumFTienUocThucHienCaNam = dtcDcdToanChiModelsSummary.Where(x => !x.BHangCha).Sum(x => x.FTienUocThucHienCaNam);
                    var SumFTienSoSanhTang = (SumFTienUocThucHienCaNam - SumFTienDuToanDuocGiao) > 0 ? SumFTienUocThucHienCaNam - SumFTienDuToanDuocGiao : 0;
                    var SumFTienSoSanhGiam = (SumFTienDuToanDuocGiao - SumFTienUocThucHienCaNam) > 0 ? SumFTienDuToanDuocGiao - SumFTienUocThucHienCaNam : 0;
                    var TongSoTien = SumFTienUocThucHienCaNam;

                    FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond + " " + _sessionInfo.YearOfWork);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                    data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                    data.Add("SumFTienDuToanDuocGiao", SumFTienDuToanDuocGiao);
                    data.Add("SumFTienThucHien06ThangDauNam", SumFTienThucHien06ThangDauNam);
                    data.Add("SumFTienUocThucHien06ThangCuoiNam", SumFTienUocThucHien06ThangCuoiNam);
                    data.Add("SumFTienUocThucHienCaNam", SumFTienUocThucHienCaNam);
                    data.Add("SumFTienSoSanhTang", SumFTienSoSanhTang);
                    data.Add("SumFTienSoSanhGiam", SumFTienSoSanhGiam);
                    data.Add("ListData", dtcDcdToanChiModelsSummary.Where(x => x.IsDataNotNull).ToList());
                    data.Add("SKTML", dtcDcdToanChiModelsSummary);
                    data.Add("TongSoTien", TongSoTien != 0 ? StringUtils.NumberToText((double)TongSoTien, true) : string.Empty);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    AddChuKy(data, _typeChuky);
                    data.Add("ThoiGian", _diaDiem + "," + DateUtils.FormatDateReport(ReportDate));
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                    templateFileName = string.Empty;
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(sFileName));
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.ConvertVN(fileNamePrefix);
                    var xlsFile = _exportService.Export<BhDtcDcdToanChiChiTietQuery, BhDmMucLucNganSach, BhDtcDcdToanChiChiTiet>(templateFileName, data);
                    results.Add(new ExportResult("DỰ TOÁN ĐIỀU CHỈNH " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = results;
                }
                else
                {
                    if (!IsInTheoTongHop)
                    {
                        var searchCondition = new BhDtcDcdToanChiChiTietCriteria
                        {
                            LNS = SelectedDanhMucLoaiChi.HiddenValue,
                            NamLamViec = _sessionInfo.YearOfWork,
                            IdDonVi = string.Join(",", lstIdDonVi),
                            ILoaiDanhMucChi = SelectedDanhMucLoaiChi.Id,
                            NgayChungTu = DateTime.Now,
                            DonViTinh = dvt,
                        };

                        var dtcDcdToanChiModelsSummary = _bhDtcDcdToanChiChiTietService.GetDataAggregateForAgencyKPQLKCBQYKCBTSKPK(searchCondition).ToList();
                        var lstDtcDcdToanChiModelsSummary = _mapper.Map<ObservableCollection<BhDtcDcdToanChiChiTietModel>>(dtcDcdToanChiModelsSummary).ToList();
                        var lstNotDonVi = dtcDcdToanChiModelsSummary.Where(x => x.Type == 0).ToList();
                        CalculateDataChiTiet(lstNotDonVi);

                        lstReportQuery = dtcDcdToanChiModelsSummary.Where(x => x.IsDataNotNull).ToList();
                    }
                    else
                    {
                        string sMaDonVoi = lstIdDonVi.FirstOrDefault();
                        var sSTongHop = _bhDtcDcdToanChiService.FindByCondition(x => x.IID_MaDonVi == sMaDonVoi && x.IID_LoaiCap == SelectedDanhMucLoaiChi.Id && x.INamLamViec == yearOfWork).FirstOrDefault();
                        if (sSTongHop.STongHop != null)
                        {
                            var lstTongHop = sSTongHop.STongHop.Split(",");
                            var lstChungTu = _bhDtcDcdToanChiService.FindByCondition(x => x.IID_LoaiCap == SelectedDanhMucLoaiChi.Id && x.INamLamViec == yearOfWork && x.BDaTongHop);
                            var lsdMaDonVi = lstChungTu.Select(x => x.IID_MaDonVi).Distinct().ToList();
                            lstIdDonVi = new List<string>();
                            lstIdDonVi = lsdMaDonVi;
                        }

                        var searchCondition = new BhDtcDcdToanChiChiTietCriteria
                        {
                            LNS = SelectedDanhMucLoaiChi.HiddenValue,
                            NamLamViec = _sessionInfo.YearOfWork,
                            IdDonVi = string.Join(",", lstIdDonVi),
                            ILoaiDanhMucChi = SelectedDanhMucLoaiChi.Id,
                            NgayChungTu = DateTime.Now,
                            DonViTinh = dvt,
                        };
                        var dtcDcdToanChiModelsSummary = _bhDtcDcdToanChiChiTietService.GetDataAggregateForAgencyKPQLKCBQYKCBTSKPK(searchCondition).ToList();
                        var lstNotDonVi = dtcDcdToanChiModelsSummary.Where(x => x.Type == 0).ToList();
                        CalculateDataChiTiet(lstNotDonVi);
                        //var lstDtcDcdToanChiModelsSummary = _mapper.Map<ObservableCollection<BhDtcDcdToanChiChiTietModel>>(dtcDcdToanChiModelsSummary).ToList();
                        dtcDcdToanChiModelsSummary.ForEach(x =>
                        {
                            x.FTienUocThucHienCaNam = x.FTienThucHien06ThangDauNam + x.FTienUocThucHien06ThangCuoiNam;
                            x.FTienSoSanhTang = x.FTienUocThucHienCaNam > x.FTienDuToanDuocGiao ? x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao : 0;
                            x.FTienSoSanhTang = x.FTienDuToanDuocGiao > x.FTienUocThucHienCaNam ? x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam : 0;
                            x.BHangCha = x.IsHangCha;

                        });

                        lstReportQuery = dtcDcdToanChiModelsSummary.Where(x => x.IsDataNotNull).ToList();
                    }

                    // Thêm dấu + và thụt đầu dòng cho những đơn vị con trừ những đơn vị 9010004, 9010006, 9010008, 9010009 đã có
                    List<string> lstLNS = new List<string>();
                    lstLNS.Add(LNSValue.LNS_9010004);
                    lstLNS.Add(LNSValue.LNS_9010006);
                    lstLNS.Add(LNSValue.LNS_9010008);
                    lstLNS.Add(LNSValue.LNS_9010009);

                    if (!lstLNS.Contains(SelectedDanhMucLoaiChi.HiddenValue))
                    {
                        if (SelectedDanhMucLoaiChi.HiddenValue.Equals("9010010")
                        || SelectedDanhMucLoaiChi.HiddenValue.Equals("9010003"))
                        {
                            lstReportQuery.ForEach(x =>
                            {
                                x.IsHangCha = x.BHangCha;
                                if (!x.BHangCha)
                                {
                                    x.SNoiDung = "  " + x.SNoiDung;
                                }
                            });

                        } else
                        {
                            lstReportQuery.ForEach(x =>
                            {
                                x.IsHangCha = x.BHangCha;
                                if (!x.BHangCha)
                                {
                                    x.SNoiDung = "  + " + x.SNoiDung;
                                }
                            });
                        }

                    }

                    Dictionary<string, object> data = new Dictionary<string, object>();

                    sFileName = GetFileTemplate(_selectedDanhMucLoaiChi.HiddenValue);

                    var SumFTienDuToanDuocGiao = lstReportQuery.Select(x => x.FTienDuToanDuocGiao).FirstOrDefault();
                    var SumFTienThucHien06ThangDauNam = lstReportQuery.Select(x => x.FTienThucHien06ThangDauNam).FirstOrDefault();
                    var SumFTienUocThucHien06ThangCuoiNam = lstReportQuery.Select(x => x.FTienUocThucHien06ThangCuoiNam).FirstOrDefault();
                    var SumFTienUocThucHienCaNam = lstReportQuery.Select(x => x.FTienUocThucHienCaNam).FirstOrDefault();
                    var SumFTienSoSanhTang = lstReportQuery.Select(x => x.FTienSoSanhTang).FirstOrDefault();
                    var SumFTienSoSanhGiam = lstReportQuery.Select(x => x.FTienSoSanhGiam).FirstOrDefault();
                    var TongSoTien = SumFTienUocThucHienCaNam;

                    FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond + " " + _sessionInfo.YearOfWork);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                    data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                    data.Add("SumFTienDuToanDuocGiao", SumFTienDuToanDuocGiao);
                    data.Add("SumFTienThucHien06ThangDauNam", SumFTienThucHien06ThangDauNam);
                    data.Add("SumFTienUocThucHien06ThangCuoiNam", SumFTienUocThucHien06ThangCuoiNam);
                    data.Add("SumFTienUocThucHienCaNam", SumFTienUocThucHienCaNam);
                    data.Add("SumFTienSoSanhTang", SumFTienSoSanhTang);
                    data.Add("SumFTienSoSanhGiam", SumFTienSoSanhGiam);
                    data.Add("ListData", lstReportQuery);
                    data.Add("SKTML", lstReportQuery);
                    data.Add("TongSoTien", TongSoTien != 0 ? StringUtils.NumberToText((double)TongSoTien, true) : string.Empty);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    AddChuKy(data, _typeChuky);
                    data.Add("ThoiGian", _diaDiem + "," + DateUtils.FormatDateReport(ReportDate));
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                    templateFileName = string.Empty;
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(sFileName));
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.ConvertVN(fileNamePrefix);
                    var xlsFile = _exportService.Export<BhDtcDcdToanChiChiTietQuery, BhDmMucLucNganSach>(templateFileName, data);
                    results.Add(new ExportResult("DỰ TOÁN ĐIỀU CHỈNH " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = results;
                }

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

        //private List<BhDtcDcdToanChiChiTietModel> CalculateRowSummary(string iIdDonVi, List<BhDtcDcdToanChiChiTietModel> lstReport, int dvt)
        //{
        //    var searchCondition = new BhDtcDcdToanChiChiTietCriteria
        //    {
        //        LNS = SelectedDanhMucLoaiChi.HiddenValue,
        //        NamLamViec = _sessionInfo.YearOfWork,
        //        IdDonVi = iIdDonVi,
        //        ILoaiDanhMucChi = SelectedDanhMucLoaiChi.Id,
        //        ILoaiTongHop = !IsInTheoTongHop ? 0 : 1,
        //        NgayChungTu = DateTime.Now,
        //        DonViTinh = dvt,
        //    };

        //    var dtcDcdToanChiModelsSummary = _bhDtcDcdToanChiChiTietService.GetDataAggregateForAgency(searchCondition).ToList();

        //    dtcDcdToanChiModelsSummary.ForEach(x =>
        //    {
        //        if (string.IsNullOrEmpty(x.SM))
        //        {
        //            x.IsHangCha = true;
        //        }
        //    });

        //    var lstDtcDcdToanChiModelsSummary = _mapper.Map<ObservableCollection<BhDtcDcdToanChiChiTietModel>>(dtcDcdToanChiModelsSummary).ToList();

        //    CalculateData(lstDtcDcdToanChiModelsSummary);
        //    if (SelectedDanhMucLoaiChi.HiddenValue != LNSValue.LNS_9010003)
        //    {
        //        lstDtcDcdToanChiModelsSummary.ForAll(x =>
        //        {
        //            x.SM = string.Empty;
        //            x.STM = string.Empty;
        //        });
        //    }
        //    else
        //    {
        //        FormatDisplay(lstDtcDcdToanChiModelsSummary);
        //    }

        //    lstDtcDcdToanChiModelsSummary.ForEach(x =>
        //    {
        //        x.FTienSoSanhTang = x.FTienUocThucHienCaNam > x.FTienDuToanDuocGiao ? x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao : 0;
        //        x.FTienSoSanhGiam = x.FTienUocThucHienCaNam < x.FTienDuToanDuocGiao ? x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam : 0;
        //    });

        //    lstReport.ForEach(x =>
        //    {
        //        x.FTienSoSanhTang = x.FTienUocThucHienCaNam > x.FTienDuToanDuocGiao ? x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao : 0;
        //        x.FTienSoSanhGiam = x.FTienUocThucHienCaNam < x.FTienDuToanDuocGiao ? x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam : 0;
        //    });
        //    //lstDtcDcdToanChiModelsSummary = lstDtcDcdToanChiModelsSummary.Where(x => x.IsDataNotNull).ToList();

        //    lstReport = lstReport.Union(lstDtcDcdToanChiModelsSummary).ToList();

        //    return lstReport;
        //}

        private void FormatDisplay(List<BhDtcDcdToanChiChiTietModel> lstDtcDcdToanChiModelsSummary)
        {
            foreach (var item in lstDtcDcdToanChiModelsSummary.Where(x => !string.IsNullOrEmpty(x.STM)))
            {
                var parent = lstDtcDcdToanChiModelsSummary.FirstOrDefault(x => x.IID_MucLucNganSach == item.IdParent);
                if (parent != null && parent.SM != string.Empty)
                {
                    item.SM = string.Empty;

                }
            }
            foreach (var item in lstDtcDcdToanChiModelsSummary.Where(x => !string.IsNullOrEmpty(x.STTM)))
            {
                var parent = lstDtcDcdToanChiModelsSummary.FirstOrDefault(x => x.IID_MucLucNganSach == item.IdParent);
                if (parent != null && parent.STM != string.Empty)
                {
                    item.STM = string.Empty;
                    item.SM = string.Empty;
                }
            }
        }

        private string GetTemplate(string input)
        {
            if (SelectedKieuGiayIn.ValueItem == "1")
                input = input + "_Doc";
            return Path.Combine(ExportPrefix.PATH_BH_DT_DCDT, input + FileExtensionFormats.Xlsx);
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser; ;
        }

        private void CalculateData(List<BhDtcDcdToanChiChiTietQuery> lstDtcDcdToanChiModelsSummary)
        {
            lstDtcDcdToanChiModelsSummary.Where(x => x.BHangChaDuToan != null && x.BHangChaDuToan.Value && string.IsNullOrEmpty(x.STenDonVi))
               .ForAll(x =>
               {
                   x.FTienDuToanDuocGiao = 0;
                   x.FTienThucHien06ThangDauNam = 0;
                   x.FTienUocThucHien06ThangCuoiNam = 0;
                   x.FTienUocThucHienCaNam = 0;
                   x.FTienSoSanhTang = 0;
                   x.FTienSoSanhGiam = 0;
               });


            var temp = lstDtcDcdToanChiModelsSummary.Where(x => x.BHangChaDuToan != null && !x.BHangChaDuToan.Value && string.IsNullOrEmpty(x.STenDonVi)).ToList();
            var dictByMlns = lstDtcDcdToanChiModelsSummary.Where(x => string.IsNullOrEmpty(x.STenDonVi)).GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }

            lstDtcDcdToanChiModelsSummary.ForEach(x =>
            {
                x.FTienUocThucHienCaNam = x.FTienThucHien06ThangDauNam + x.FTienUocThucHien06ThangCuoiNam;
                x.FTienSoSanhTang = x.FTienUocThucHienCaNam > x.FTienDuToanDuocGiao ? x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao : 0;
                x.FTienSoSanhGiam = x.FTienUocThucHienCaNam < x.FTienDuToanDuocGiao ? x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam : 0;
            });
        }

        private void CalculateDataChiTiet(List<BhDtcDcdToanChiChiTietQuery> lstDtcDcdToanChiModelsSummary)
        {
            lstDtcDcdToanChiModelsSummary.Where(x => x.IsHangCha)
               .ForAll(x =>
               {
                   //x.FTienDuToanDuocGiao = 0;
                   x.FTienThucHien06ThangDauNam = 0;
                   x.FTienUocThucHien06ThangCuoiNam = 0;
                   x.FTienUocThucHienCaNam = 0;
                   x.FTienSoSanhTang = 0;
                   x.FTienSoSanhGiam = 0;
               });


            var temp = lstDtcDcdToanChiModelsSummary.Where(x => !x.IsHangCha).ToList();
            var dictByMlns = lstDtcDcdToanChiModelsSummary.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParentChiTiet(item.IdParent, item, dictByMlns);
            }
            lstDtcDcdToanChiModelsSummary.ForEach(x =>
            {
                x.FTienSoSanhTang = x.FTienUocThucHienCaNam > x.FTienDuToanDuocGiao ? x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao : 0;
                x.FTienSoSanhGiam = x.FTienUocThucHienCaNam < x.FTienDuToanDuocGiao ? x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam : 0;
                x.BHangCha = x.IsHangCha;
            });
        }

        private void CalculateDataDuToan(List<BhDtcDcdToanChiChiTietQuery> lstDtcDcdToanChiModelsSummary)
        {
            lstDtcDcdToanChiModelsSummary.Where(x => x.BHangChaDuToan.HasValue && x.BHangChaDuToan.Value)
               .ForAll(x =>
               {
                   x.FTienDuToanDuocGiao = 0;
               });


            var temp = lstDtcDcdToanChiModelsSummary.Where(x => x.BHangChaDuToan.HasValue && !x.BHangChaDuToan.Value).ToList();
            var dictByMlns = lstDtcDcdToanChiModelsSummary.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParentDuToan(item.IdParent, item, dictByMlns);
            }
        }

        private void CalculateParentDuToan(Guid idParent, BhDtcDcdToanChiChiTietQuery item, Dictionary<Guid, BhDtcDcdToanChiChiTietQuery> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            model.FTienDuToanDuocGiao += item.FTienDuToanDuocGiao;
            CalculateParentDuToan(model.IdParent, item, dictByMlns);
        }

        private void CalculateParentChiTiet(Guid idParent, BhDtcDcdToanChiChiTietQuery item, Dictionary<Guid, BhDtcDcdToanChiChiTietQuery> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            //model.FTienDuToanDuocGiao += item.FTienDuToanDuocGiao;
            model.FTienThucHien06ThangDauNam += item.FTienThucHien06ThangDauNam.GetValueOrDefault(0);
            model.FTienUocThucHien06ThangCuoiNam += item.FTienUocThucHien06ThangCuoiNam.GetValueOrDefault(0);
            model.FTienUocThucHienCaNam += item.FTienUocThucHienCaNam.GetValueOrDefault(0);
            model.FTienSoSanhTang += item.FTienSoSanhTang.GetValueOrDefault(0);
            model.FTienSoSanhGiam += item.FTienSoSanhGiam.GetValueOrDefault(0);

            CalculateParentChiTiet(model.IdParent, item, dictByMlns);
        }

        private void CalculateParent(Guid idParent, BhDtcDcdToanChiChiTietQuery item, Dictionary<Guid, BhDtcDcdToanChiChiTietQuery> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            model.FTienDuToanDuocGiao += item.FTienDuToanDuocGiao;
            model.FTienThucHien06ThangDauNam += item.FTienThucHien06ThangDauNam;
            model.FTienUocThucHien06ThangCuoiNam += item.FTienUocThucHien06ThangCuoiNam;
            model.FTienUocThucHienCaNam += item.FTienUocThucHienCaNam;
            model.FTienSoSanhTang += item.FTienSoSanhTang;
            model.FTienSoSanhGiam += item.FTienSoSanhGiam;

            CalculateParent(model.IdParent, item, dictByMlns);

        }

        private void LoadTitleFirst()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                TxtTitleFirst = _dmChuKy.TieuDe1MoTa;
            else
                TxtTitleFirst = TitleFirst;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                TxtTitleSecond = _dmChuKy.TieuDe2MoTa;
            else
                TxtTitleSecond = TitleSecond;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                TxtTitleThird = _dmChuKy.TieuDe3MoTa;
        }

        private void AddChuKy(Dictionary<string, object> data, string typeChuky)
        {
            //add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
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
        }
        #endregion

        #region Load data

        private void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(0);
        }

        private void LoadDiaDiem()
        {
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).FirstOrDefault(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM);
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
        }

        private void LoadCatUnitTypes()
        {
            _catUnitTypes = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH));
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            List<DanhMuc> data = _danhMucService.FindByCondition(predicate).OrderBy(x => x.SGiaTri).ToList();
            _catUnitTypes = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            if (data.Count == 0)
            {
                _catUnitTypes.Insert(0, new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            _catUnitTypeSelected = _catUnitTypes.FirstOrDefault();
        }

        private void LoadDanhMucLoaiChi()
        {
            try
            {
                ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
                IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi = _bhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
                if (listDanhMucLoaiChi != null)
                {
                    ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                    {
                        DisplayItem = n.STenDanhMucLoaiChi,
                        ValueItem = n.SMaLoaiChi,
                        HiddenValue = n.SLNS,
                        Id = n.Id
                    }));
                    SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.ElementAt(0);
                }

                OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }

        }

        private void LoadLoaiBaoCao()
        {
            _itemsLoaiBaoCao = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Chi tiết đơn vị", ValueItem = AdjustSummaryReportType.AgencyDetail.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp - chi tiết đơn vị", ValueItem = AdjustSummaryReportType.AgencySummary.ToString() }
            };
            _selectedLoaiBaoCao = _itemsLoaiBaoCao.First();
        }

        private void LoadInToi()
        {
            _itemsInToi = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "M", ValueItem = "0" },
                new ComboboxItem { DisplayItem = "TNG", ValueItem = "1" }
            };
            _selectedInToi = _itemsInToi.First();
        }


        private void Clear()
        {
            _listAgency = null;
        }

        #endregion
    }
}
