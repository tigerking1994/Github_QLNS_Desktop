using AutoMapper;
using log4net;
using Microsoft.SqlServer.Diagnostics.STrace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.Report
{
    public class PrintPhuLucDuToanThuViewModel : ViewModelBase
    {
        private IExportService _exportService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private ILog _logger;
        private IMapper _mapper;
        private INsDonViService _donViService;
        private IDttBHXHPhanBoService _dttBHXHService;
        private IPbdttmBHYTService _dttmBHYTService;
        private IPbdtcBHXHService _dtcBHXHService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private IBhBaoCaoGhiChuService _bhGhiChuService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private IDttBHXHPhanBoChiTietService _phanBoDTTService;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private bool _checkAllAgencies;
        public bool IsQuanLyDonViCha;
        public bool IsShowDatePeople { get; set; }
        public string TieuDeBaoCao { get; set; }
        public string name { get; set; }
        private string _typeChuky;
        private string ReportName
        {
            get
            {
                return name = "Báo cáo dự toán thu BHXH, BHYT, BHTN";
            }
        }

        public override string Name => ReportName;
        public override string Title => ReportName;
        public override string Description => ReportName;
        public bool IsEnableCheckBox1Page => _selectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString();

        private List<ComboboxItem> _reportTypes;
        public List<ComboboxItem> ReportTypes
        {
            get => _reportTypes;
            set => SetProperty(ref _reportTypes, value);
        }

        private List<ComboboxItem> _reportBaoCaoTypes;
        public List<ComboboxItem> ReportBaoCaoTypes
        {
            get => _reportBaoCaoTypes;
            set => SetProperty(ref _reportBaoCaoTypes, value);
        }

        private bool _isMillionRound;
        public bool IsMillionRound
        {
            get => _isMillionRound;
            set => SetProperty(ref _isMillionRound, value);
        }

        private ComboboxItem _selectedReportType;
        public ComboboxItem SelectedReportType
        {
            get => _selectedReportType;
            set
            {
                SetProperty(ref _selectedReportType, value);
                OnPropertyChanged(nameof(IsEnableCheckBox1Page));
            }
        }
        private ComboboxItem _selectedBaoCaoType;
        public ComboboxItem SelectedBaoCaoType
        {
            get => _selectedBaoCaoType;
            set
            {
                SetProperty(ref _selectedBaoCaoType, value);
                //OnPropertyChanged(nameof(IsEnableCheckBox1Page));
            }
        }
        private ObservableCollection<ComboboxItem> _cbxEstimateReportType;
        public ObservableCollection<ComboboxItem> CbxEstimateReportType
        {
            get => _cbxEstimateReportType;
            set => SetProperty(ref _cbxEstimateReportType, value);
        }

        private ComboboxItem _cbxEstimateReportTypeSelected;
        public ComboboxItem CbxEstimateReportTypeSelected
        {
            get => _cbxEstimateReportTypeSelected;
            set
            {
                SetProperty(ref _cbxEstimateReportTypeSelected, value);
                if (_cbxEstimateReportTypeSelected != null)
                {
                    LoadDataDot();
                    LoadTypeChuKy();
                }
            }
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

        private bool _isEnableType;
        public bool IsEnableType
        {
            get => _isEnableType;
            set => SetProperty(ref _isEnableType, value);
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
         
        private LoaiDTTThu _inTheo;
        public LoaiDTTThu InTheo
        {
            get => _inTheo;
            set
            {
                SetProperty(ref _inTheo, value);
                LoadTypeChuKy();
                LoadTieuDe();
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
            set => SetProperty(ref _isSummaryAgency, value);
        }

        public bool InMotToChecked { get; set; }

        private bool _inLuyKeChecked;
        public bool InLuyKeChecked
        {
            get => _inLuyKeChecked;
            set
            {
                SetProperty(ref _inLuyKeChecked, value);
                LoadAgencies();
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
                LoadTieuDe();
                LoadAgencies();
            }
        }

        private bool _isInTheoChungTu;
        public bool IsInTheoChungTu
        {
            get => _isInTheoChungTu;
            set
            {
                if (SetProperty(ref _isInTheoChungTu, value))
                {
                    LoadAgencies();
                    LoadDataDot();
                }
            }
        }

        private ComboboxItem _selectedKieuGiayIn;

        public ComboboxItem SelectedKieuGiayIn
        {
            get => _selectedKieuGiayIn;
            set => SetProperty(ref _selectedKieuGiayIn, value);
        }

        private ObservableCollection<ComboboxItem> _itemsKieuGiayIn = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ItemsKieuGiayIn
        {
            get => _itemsKieuGiayIn;
            set => SetProperty(ref _itemsKieuGiayIn, value);
        }
        private string SMaBaoCao { get; set; }
        private BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel { get; set; }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand DataInterpretationCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand NoteCommand { get; }

        public PrintPhuLucDuToanThuViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            INsDonViService donViService,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IDttBHXHPhanBoService iDttBHXHPhanBoService,
            IPbdttmBHYTService iPbdttmBHYTService,
            IPbdtcBHXHService iPbdtcBHXHService,
            IDanhMucService iDanhMucService,
            IDttBHXHPhanBoChiTietService iDttBHXHPhanBoChiTietService,
            IBhBaoCaoGhiChuService bhBaoCaoGhiChuService,
            BhBaoCaoGhiChuDialogViewModel bhBaoCaoGhiChuDialogViewModel)
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _exportService = exportService;
            _donViService = donViService;
            _dmChuKyService = dmChuKyService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _dttBHXHService = iDttBHXHPhanBoService;
            _danhMucService = iDanhMucService;
            _dttmBHYTService = iPbdttmBHYTService;
            _dtcBHXHService = iPbdtcBHXHService;
            _phanBoDTTService = iDttBHXHPhanBoChiTietService;
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

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            _agencies = new ObservableCollection<AgencyModel>();
            IsSummary = false;
            IsSummaryAgency = false;
            LoadEstimateType();
            LoadDataDot();
            ResetCondition();
            LoadTieuDe();
            LoadReportType();
            LoadLoaiBaoCao();
            LoadAgencies();
            LoadTypeChuKy();
            LoadDanhMuc();
            LoadKieuGiayIn();
            InTheo = LoaiDTTThu.BHXH;
        }

        private void LoadAgencies()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                var lstIdDonVi = GetListIdDonVi();
                if (lstIdDonVi != null)
                {
                    IsLoading = true;
                    List<DonVi> agencies = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
                    agencies = agencies.Where(x => lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    e.Result = agencies;
                }
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
                        }
                    };
                }
                OnPropertyChanged(nameof(Agencies));
                OnPropertyChanged(nameof(IsSelectedAllAgency));
                OnPropertyChanged(nameof(SelectedAgencyCount));
                IsLoading = false;
            });
        }

        private List<string> GetListIdDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            List<string> lstIdDonVi = new List<string>();
            if (DataDotSelected != null)
            {
                List<BhDtPhanBoChungTu> lstDTTDuocXem = new List<BhDtPhanBoChungTu>();
                if (IsInTheoChungTu && !InLuyKeChecked)
                {
                    lstDTTDuocXem = _dttBHXHService.FindBySoChungTu(DataDotSelected.DisplayItemOption2, yearOfWork).ToList();
                }
                else if (InLuyKeChecked)
                {
                    var soQuyetDinh = GetSoQuyetDinhDenLuyKe();
                    var ngayQuyetDinh = GetNgayQuyetDinhDenLuyKe();
                    var lstQuery = _dttBHXHService.FindBySoQuyetDinhLuyKe(soQuyetDinh, ngayQuyetDinh, yearOfWork).ToList();
                    lstDTTDuocXem = _mapper.Map<List<BhDtPhanBoChungTu>>(lstQuery);
                }
                else if (!IsInTheoChungTu && !InLuyKeChecked)
                {
                    lstDTTDuocXem = _dttBHXHService.FindBySoQuyetDinh(DataDotSelected.ValueItem, yearOfWork).ToList();
                }

                if (lstDTTDuocXem != null)
                {
                    foreach (var ct in lstDTTDuocXem)
                    {
                        lstIdDonVi = ct.SDsidMaDonVi.Split(",").Distinct().ToList();
                    }
                }
            }
            return lstIdDonVi;
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
            _searchAgencyText = string.Empty;
            _searchBudgetIndexText = string.Empty;
        }

        private void LoadTieuDe()
        {
            LoadTypeChuKy();
        }

        private void LoadReportType()
        {
            _reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencyDetail.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() }
            };
            _selectedReportType = _reportTypes.First();
        }

        private void LoadLoaiBaoCao()
        {
            _reportBaoCaoTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencyDetail.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị theo khối", ValueItem = SummaryLNSReportType.AgencySummaryBlock.ToString() }
            };
            _selectedBaoCaoType = _reportBaoCaoTypes.First();
        }

        private string GetTemplate()
        {
            string input = "";
            input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_DU_TOAN_TONG_HOP_THU_CHI);
            return Path.Combine(ExportPrefix.PATH_BH_DTT, input + FileExtensionFormats.Xlsx);
        }
        private void OnConfigSign()
        {
            LoadTypeChuKy();
            DmChuKyModel chuKyModel = new DmChuKyModel();

            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void OnNoteCommand()
        {
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();
            BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHXH, TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHTN, TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHYT_QUAN_NHAN, TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHYT_NLD };
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(Agencies);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = SMaBaoCao;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = false;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = true;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
        }

        private void OnExportFile(ExportType exportType)
        {
            if (Agencies.Where(item => item.Selected).Count() <= 0)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }
            // Tong hop theo don vi
            if (_selectedBaoCaoType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
            {
                if (InTheo == LoaiDTTThu.BHXH || InTheo == LoaiDTTThu.BHTN)
                {
                    OnPrintReportKhtDuToanThuBHXH(exportType, false);
                }
                else if (InTheo == LoaiDTTThu.BHYT_QN || InTheo == LoaiDTTThu.BHYT_NLD)
                {
                    OnPrintReportKhtDuToanThuBHYT(exportType, false);
                }
            }
            // Tong hop theo khoi don vi
            else
            {
                if (InTheo == LoaiDTTThu.BHXH || InTheo == LoaiDTTThu.BHTN)
                {
                    OnPrintReportKhtDuToanThuBHXH(exportType, true);
                }
                else if (InTheo == LoaiDTTThu.BHYT_QN || InTheo == LoaiDTTThu.BHYT_NLD)
                {
                    OnPrintReportKhtDuToanThuBHYT(exportType, true);
                }
            }
        }


        public void OnPrintReportKhtDuToanThuBHXH(ExportType exportType, bool IsKhoi)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.Id.ToString()).ToList());

                    string soQuyetDinh = "";
                    string ngayQuyetDinh = "";
                    if (InLuyKeChecked)
                    {
                        soQuyetDinh = GetSoQuyetDinhDenLuyKe();
                        ngayQuyetDinh = GetNgayQuyetDinhDenLuyKe();
                    }
                    else
                    {
                        soQuyetDinh = DataDotSelected.ValueItem;
                        ngayQuyetDinh = DataDotSelected.HiddenValue;
                    }

                    var listData = _phanBoDTTService.ExportKhtDuToanBHXH(yearOfWork, selectedUnits, BhxhMLNS.KHOI_DU_TOAN
                        , BhxhMLNS.KHOI_HACH_TOAN, soQuyetDinh, ngayQuyetDinh, donViTinh, IsMillionRound, IsKhoi).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    double? tongSoTienBHXH = 0;
                    double? tongSoTienBHTN = 0;
                    if (!IsKhoi)
                        listData = listData.OrderBy(x => x.IdDonVi).ToList();

                   
                    foreach (var item in listData)
                    {
                        item.BhxhNldDongDuToan = Math.Round(item.BhxhNldDongDuToan.GetValueOrDefault());
                        item.BhxhNsddDongDuToan = Math.Round(item.BhxhNsddDongDuToan.GetValueOrDefault());
                        item.BHXHTongCongDuToan = item.BhxhNldDongDuToan + item.BhxhNsddDongDuToan;
                        item.BhxhNldDongHachToan = Math.Round(item.BhxhNldDongHachToan.GetValueOrDefault());
                        item.BhxhNsddDongHachToan = Math.Round(item.BhxhNsddDongHachToan.GetValueOrDefault());
                        item.BHXHTongCongHachToan = item.BhxhNldDongHachToan + item.BhxhNsddDongHachToan;

                        item.BhtnNldDongDuToan= Math.Round(item.BhtnNldDongDuToan.GetValueOrDefault());
                        item.BhtnNsddDongDuToan = Math.Round(item.BhtnNsddDongDuToan.GetValueOrDefault());
                        item.BhtnNldDongHachToan= Math.Round(item.BhtnNldDongHachToan.GetValueOrDefault());
                        item.BhtnNsddDongHachToan = Math.Round(item.BhtnNsddDongHachToan.GetValueOrDefault());

                        item.BHTNTongCongDuToan = item.BhtnNldDongDuToan + item.BhtnNsddDongDuToan;
                        item.BHTNTongCongHachToan = item.BhtnNldDongHachToan + item.BhtnNsddDongHachToan;

                    }
                    
                    int stt = 1;
                    foreach (var i in listData)
                    {
                        if (string.IsNullOrEmpty(i.IdDonVi))
                        {
                            stt = 1;
                            i.STT = null;
                            i.SNumber = i.STenDonVi;
                            i.IsParent = true;
                            i.BHangCha = true;
                        }
                        else
                        {
                            i.STT = stt++;
                            i.SNumber = i.STT.ToString();
                            i.IsParent = false;
                        }
                    }
                    
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("Donvi", (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("TongSoTien", 0);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Nam", yearOfWork);

                    AddChuKy(data, _typeChuky);
                    AddNote(data, _typeChuky);
                    //BHXH
                    if (InTheo == LoaiDTTThu.BHXH)
                    {
                        listData = listData.Where(x => x.BhxhNldDongDuToan.GetValueOrDefault() != 0 || x.BhxhNsddDongDuToan.GetValueOrDefault() != 0
                            || x.BhxhNldDongHachToan.GetValueOrDefault() != 0 || x.BhxhNsddDongHachToan.GetValueOrDefault() != 0).ToList();
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHT_DU_TOAN_THU_BHXH));
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("TieuDe3", Title3);
                        data.Add("ListData", listData);
                        AddEmptyItems(listData);
                        if (!IsKhoi)
                        {
                            data.Add("TotalBHXHNldDongDuToan", listData.Sum(x => x.BhxhNldDongDuToan));
                            data.Add("TotalBHXHNsddDongDuToan", listData.Sum(x => x.BhxhNsddDongDuToan));
                            data.Add("TotalBHXHNldDongHachToan", listData.Sum(x => x.BhxhNldDongHachToan));
                            data.Add("TotalBHXHNsddDongHachToan", listData.Sum(x => x.BhxhNsddDongHachToan));
                            data.Add("TotalBHXHDuToan", listData.Sum(x => x.BHXHTongCongDuToan));
                            data.Add("TotalBHXHHachToan", listData.Sum(x => x.BHXHTongCongHachToan));
                            data.Add("TotalBHXH", listData?.Sum(x => x.BHXHTongCong));
                        }
                        else
                        {
                            data.Add("TotalBHXHNldDongDuToan", listData.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhxhNldDongDuToan));
                            data.Add("TotalBHXHNsddDongDuToan", listData.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhxhNsddDongDuToan));
                            data.Add("TotalBHXHNldDongHachToan", listData.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhxhNldDongHachToan));
                            data.Add("TotalBHXHNsddDongHachToan", listData.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhxhNsddDongHachToan));
                            data.Add("TotalBHXHDuToan", listData.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BHXHTongCongDuToan));
                            data.Add("TotalBHXHHachToan", listData.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BHXHTongCongHachToan));
                            data.Add("TotalBHXH", listData.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BHXHTongCong));
                        }
                    }
                    //BHTN
                    if (InTheo == LoaiDTTThu.BHTN)
                    {
                        listData = listData.Where(x => x.BhtnNldDongDuToan.GetValueOrDefault() != 0 || x.BhtnNsddDongDuToan.GetValueOrDefault() != 0
                            || x.BhtnNldDongHachToan.GetValueOrDefault() != 0 || x.BhtnNsddDongHachToan.GetValueOrDefault() != 0).ToList();
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHT_DU_TOAN_THU_BHTN));
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        data.Add("ListData", listData);
                        AddEmptyItems(listData);
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("TieuDe3", Title3);
                        if (!IsKhoi)
                        {
                            data.Add("TotalBHTNNldDongDuToan", listData.Sum(x => x.BhtnNldDongDuToan));
                            data.Add("TotalBHTNNsddDongDuToan", listData.Sum(x => x.BhtnNsddDongDuToan));
                            data.Add("TotalBHTNNldDongHachToan", listData.Sum(x => x.BhtnNldDongHachToan));
                            data.Add("TotalBHTNNsddDongHachToan", listData.Sum(x => x.BhtnNsddDongHachToan));
                            data.Add("TotalBHTNDuToan", listData.Sum(x => x.BHTNTongCongDuToan));
                            data.Add("TotalBHTNHachToan", listData.Sum(x => x.BHTNTongCongHachToan));
                            data.Add("TotalBHTN", listData.Sum(x => x.BhtnTongCong));
                        }
                        else
                        {
                            data.Add("TotalBHTNNldDongDuToan", listData.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhtnNldDongDuToan));
                            data.Add("TotalBHTNNsddDongDuToan", listData.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhtnNsddDongDuToan));
                            data.Add("TotalBHTNNldDongHachToan", listData.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhtnNldDongHachToan));
                            data.Add("TotalBHTNNsddDongHachToan", listData.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhtnNsddDongHachToan));
                            data.Add("TotalBHTNDuToan", listData.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BHTNTongCongDuToan));
                            data.Add("TotalBHTNHachToan", listData.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BHTNTongCongHachToan));
                            data.Add("TotalBHTN", listData.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhtnTongCong));
                        }
                    }

                    if (listData.Count > 0)
                    {
                        if (!IsKhoi)
                        {
                            tongSoTienBHXH = listData?.Sum(x => x.BHXHTongCong);
                            tongSoTienBHTN = listData?.Sum(x => x.BhtnTongCong);
                        }
                        else
                        {
                            tongSoTienBHXH = listData?.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BHXHTongCong);
                            tongSoTienBHTN = listData?.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhtnTongCong);
                        }
                    }

                    data.Add("TienBangChu", InTheo == LoaiDTTThu.BHXH ? (StringUtils.NumberToText((tongSoTienBHXH != null ? (tongSoTienBHXH.Value * donViTinh) : 0), true))
                                                    : (StringUtils.NumberToText((tongSoTienBHTN != null ? (tongSoTienBHTN.Value * donViTinh) : 0), true)));
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<ReportKhtDuToanBHXHQuery>(templateFileName, data);
                    exportResults.Add(new ExportResult("DỰ TOÁN THU BHXH, BHTN NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = exportResults;
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

        public void OnPrintReportKhtDuToanThuBHYT(ExportType exportType, bool IsKhoi)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.Id.ToString()).ToList());
                    List<ReportKhtDuToanBHXHQuery> listData = new List<ReportKhtDuToanBHXHQuery>();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();

                    double? tongSoTienBHYT_QN = 0;
                    double? tongSoTienBHYT_NLD = 0;
                    string soQuyetDinh = "";
                    string ngayQuyetDinh = "";
                    if (InLuyKeChecked)
                    {
                        soQuyetDinh = GetSoQuyetDinhDenLuyKe();
                        ngayQuyetDinh = GetNgayQuyetDinhDenLuyKe();
                    }
                    else
                    {
                        soQuyetDinh = DataDotSelected.ValueItem;
                        ngayQuyetDinh = DataDotSelected.HiddenValue;
                    }


                    //BHYT - Quan nhan
                    if (InTheo == LoaiDTTThu.BHYT_QN)
                    {
                        listData = _phanBoDTTService.ExportKhtDuToanBHYT(yearOfWork, selectedUnits, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN
                            , BhxhLoaiSM.QUAN_NHAN, soQuyetDinh, ngayQuyetDinh, donViTinh, IsMillionRound, IsKhoi).ToList();
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHT_DU_TOAN_THU_BHYT_QUAN_NHAN));
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        listData = listData.Where(x => x.BHYTTongCongDuToan.GetValueOrDefault() != 0 || x.BHYTTongCongHachToan.GetValueOrDefault() != 0).ToList();

                        foreach (var item in listData)
                        {
                            item.BhytNldDongDuToan = Math.Round(item.BhytNldDongDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.BhytNsddDongDuToan = Math.Round(item.BhytNsddDongDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.BhytNldDongHachToan = Math.Round(item.BhytNldDongHachToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.BhytNsddDongHachToan = Math.Round(item.BhytNsddDongHachToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);

                            item.BHYTTongCongDuToan = item.BhytNldDongDuToan + item.BhytNsddDongDuToan;
                            item.BHYTTongCongHachToan = item.BhytNldDongHachToan + item.BhytNsddDongHachToan;
                        }

                        // Sum 
                        if (listData.Count > 0)
                        {
                            if (!IsKhoi)
                            {
                                tongSoTienBHYT_QN = listData?.Sum(x => x.BhytTongCong);
                                tongSoTienBHYT_NLD = listData?.Sum(x => x.BhytTongCong);
                            }
                            else
                            {
                                tongSoTienBHYT_QN = listData?.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhytTongCong);
                                tongSoTienBHYT_NLD = listData?.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhytTongCong);
                            }
                        }

                        if (!IsKhoi)
                        {

                            listData = listData.OrderBy(x => x.IdDonVi).ToList();
                            data.Add("BHYTTongDuToan", listData.Sum(x => x.BHYTTongCongDuToan));
                            data.Add("BHYTTongHachToan", listData.Sum(x => x.BHYTTongCongHachToan));
                            data.Add("BHYTTongCong", listData.Sum(x => x.BhytTongCong));
                            data.Add("TienBangChu", StringUtils.NumberToText((tongSoTienBHYT_QN != null ? (tongSoTienBHYT_QN.Value * donViTinh) : 0), true));
                        }
                        else
                        {
                            var tongTien = listData.Where(x => !string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhytTongCong);
                            data.Add("BHYTTongDuToan", listData.Where(x => !string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BHYTTongCongDuToan));
                            data.Add("BHYTTongHachToan", listData.Where(x => !string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BHYTTongCongHachToan));
                            data.Add("BHYTTongCong", listData.Where(x => !string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhytTongCong));
                            data.Add("TienBangChu", StringUtils.NumberToText((tongSoTienBHYT_QN != null ? (tongSoTienBHYT_QN.Value * donViTinh) : 0), true));

                        }
                        int stt = 1;
                        foreach (var i in listData)
                        {
                            if (string.IsNullOrEmpty(i.IdDonVi))
                            {
                                stt = 1;
                                i.STT = null;
                                i.SNumber = i.STenDonVi;
                                i.IsParent = true;
                                i.BHangCha = true;
                            }
                            else
                            {
                                i.STT = stt++;
                                i.SNumber = i.STT.ToString();
                                i.IsParent = false;
                            }
                        }
                        AddEmptyItems(listData);
                        data.Add("ListData", listData);
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("TieuDe3", Title3);
                    }
                    //BHYT - Nguoi Lao Dong
                    if (InTheo == LoaiDTTThu.BHYT_NLD)
                    {
                        listData = _phanBoDTTService.ExportKhtDuToanBHYT(yearOfWork, selectedUnits, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN
                            , BhxhLoaiSM.NGUOI_LAO_DONG, soQuyetDinh, ngayQuyetDinh, donViTinh, IsMillionRound, IsKhoi).ToList();
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHT_DU_TOAN_THU_BHYT_NLD));
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        listData = listData.Where(x => x.BhytNldDongDuToan.GetValueOrDefault() != 0 || x.BhytNsddDongDuToan.GetValueOrDefault() != 0
                            || x.BhytNldDongHachToan.GetValueOrDefault() != 0 || x.BhytNsddDongHachToan.GetValueOrDefault() != 0).ToList();

                        if (listData.Any())
                        {
                            foreach (var item in listData)
                            {
                                item.BhytNldDongDuToan = Math.Round(item.BhytNldDongDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.BhytNsddDongDuToan = Math.Round(item.BhytNsddDongDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.BHYTTongCongDuToan = item.BhytNldDongDuToan + item.BhytNsddDongDuToan;
                                item.BhytNldDongHachToan = Math.Round(item.BhytNldDongHachToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.BhytNsddDongHachToan = Math.Round(item.BhytNsddDongHachToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.BHYTTongCongHachToan = item.BhytNldDongHachToan + item.BhytNsddDongHachToan;
                            }
                        }

                        // Sum 
                        if (listData.Count > 0)
                        {
                            if (!IsKhoi)
                            {
                                tongSoTienBHYT_QN = listData?.Sum(x => x.BhytTongCong);
                                tongSoTienBHYT_NLD = listData?.Sum(x => x.BhytTongCong);
                            }
                            else
                            {
                                tongSoTienBHYT_QN = listData?.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhytTongCong);
                                tongSoTienBHYT_NLD = listData?.Where(x => string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhytTongCong);
                            }
                        }

                        if (!IsKhoi)
                        {
                            listData = listData.OrderBy(x => x.IdDonVi).ToList();
                            data.Add("TotalBhytNldDongDuToan", listData.Sum(x => x.BhytNldDongDuToan));
                            data.Add("TotalBhytNsddDongDuToan", listData.Sum(x => x.BhytNsddDongDuToan));
                            data.Add("TotalBHYTTongCongDuToan", listData.Sum(x => x.BHYTTongCongDuToan));
                            data.Add("TotalBhytNldDongHachToan", listData.Sum(x => x.BhytNldDongHachToan));
                            data.Add("TotalBhytNsddDongHachToan", listData.Sum(x => x.BhytNsddDongHachToan));
                            data.Add("BHYTTongCongHachToan", listData.Sum(x => x.BHYTTongCongHachToan));
                            data.Add("TotalBHYT", listData.Sum(x => x.BhytTongCong));
                            data.Add("TienBangChu", StringUtils.NumberToText((tongSoTienBHYT_NLD != null ? (tongSoTienBHYT_NLD.Value * donViTinh) : 0), true));
                        }
                        else
                        {
                            var tongTien = listData.Where(x => !string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhytTongCong);
                            data.Add("TotalBhytNldDongDuToan", listData.Where(x => !string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhytNldDongDuToan));
                            data.Add("TotalBhytNsddDongDuToan", listData.Where(x => !string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhytNsddDongDuToan));
                            data.Add("TotalBHYTTongCongDuToan", listData.Where(x => !string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BHYTTongCongDuToan));
                            data.Add("TotalBhytNldDongHachToan", listData.Where(x => !string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhytNldDongHachToan));
                            data.Add("TotalBhytNsddDongHachToan", listData.Where(x => !string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhytNsddDongHachToan));
                            data.Add("BHYTTongCongHachToan", listData.Where(x => !string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BHYTTongCongHachToan));
                            data.Add("TotalBHYT", listData.Where(x => !string.IsNullOrEmpty(x.IdDonVi)).Sum(x => x.BhytTongCong));
                            data.Add("TienBangChu", StringUtils.NumberToText((tongSoTienBHYT_NLD != null ? (tongSoTienBHYT_NLD.Value * donViTinh) : 0), true));
                        }

                        int stt = 1;
                        foreach (var i in listData)
                        {
                            if (string.IsNullOrEmpty(i.IdDonVi))
                            {
                                stt = 1;
                                i.STT = null;
                                i.SNumber = i.STenDonVi;
                                i.IsParent = true;
                                i.BHangCha = true;
                            }
                            else
                            {
                                i.STT = stt++;
                                i.SNumber = i.STT.ToString();
                                i.IsParent = false;
                            }
                        }
                        AddEmptyItems(listData);
                        data.Add("ListData", listData);
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("TieuDe3", Title3);
                    }

                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Nam", yearOfWork);
                    AddChuKy(data, _typeChuky);
                    AddNote(data, _typeChuky);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<ReportKhtDuToanBHXHQuery>(templateFileName, data);
                    exportResults.Add(new ExportResult("DỰ TOÁN THU BHYT NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    e.Result = exportResults;
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

        public void AddNote(Dictionary<string, object> data, string idType, string idMaDonVi = null)
        {
            try
            {
                BhCauHinhBaoCao bhGhiChu;
                if (string.IsNullOrEmpty(idMaDonVi))
                {
                    bhGhiChu = _bhGhiChuService.FindByCondition(x => x.SMaBaoCao.Equals(idType) && x.INamLamViec == _sessionInfo.YearOfWork && x.ILoaiBaoCao.Equals((int)NoteTypeBhxh.AgencySummary)).FirstOrDefault();
                }
                else
                {
                    bhGhiChu = _bhGhiChuService.FindByCondition(x => x.SMaBaoCao.Equals(idType) && x.INamLamViec == _sessionInfo.YearOfWork && x.ILoaiBaoCao.Equals((int)NoteTypeBhxh.AgencyDetail) && x.IIdMaDonVi.Equals(idMaDonVi)).FirstOrDefault();
                }

                data.Add("GhiChu", bhGhiChu == null ? string.Empty : bhGhiChu.SGhiChu);
                if (bhGhiChu != null && !string.IsNullOrEmpty(bhGhiChu.SGhiChu))
                {
                    data.Add("ShowNote", true);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        private void LoadTypeChuKy()
        {
            if (InTheo == LoaiDTTThu.BHXH && (CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.YEAR).ToString() || CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ALL).ToString()))
            {
                SMaBaoCao = TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHXH;
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHXH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHXH;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.DTT_BHXH;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_3;
            }
            else if (InTheo == LoaiDTTThu.BHTN && (CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.YEAR).ToString() || CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ALL).ToString()))
            {
                SMaBaoCao = TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHTN;
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHTN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHTN;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.DTT_BHTN;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_3;
            }
            else if (InTheo == LoaiDTTThu.BHYT_QN && (CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.YEAR).ToString() || CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ALL).ToString()))
            {
                SMaBaoCao = TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHYT_QUAN_NHAN;
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHYT_QUAN_NHAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHYT_QUAN_NHAN;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.DTT_BHYT_QN;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_3;
            }
            else if (InTheo == LoaiDTTThu.BHYT_NLD && (CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.YEAR).ToString() || CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ALL).ToString()))
            {
                SMaBaoCao = TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHYT_NLD;
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHYT_NLD) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHYT_NLD;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.DTT_BHYT_NLD;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_3;
            }
            //Loại bổ sung
            else if (InTheo == LoaiDTTThu.BHXH && CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ADDITIONAL).ToString())
            {
                Title1 = DefaultDTTReportTitle.GDTT_BS_BHXH_TITLE_1;
                Title2 = DefaultDTTReportTitle.GDTT_BS_TITLE_2;
            }
            else if (InTheo == LoaiDTTThu.BHTN && CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ADDITIONAL).ToString())
            {
                Title1 = DefaultDTTReportTitle.GDTT_BS_BHTN_TITLE_1;
                Title2 = DefaultDTTReportTitle.GDTT_BS_TITLE_2;
            }
            else if (InTheo == LoaiDTTThu.BHYT_QN && CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ADDITIONAL).ToString())
            {
                Title1 = DefaultDTTReportTitle.GDTT_BS_BHYT_QN_TITLE_1;
                Title2 = DefaultDTTReportTitle.GDTT_BS_TITLE_2;
            }
            else if (InTheo == LoaiDTTThu.BHYT_NLD && CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ADDITIONAL).ToString())
            {
                Title1 = DefaultDTTReportTitle.GDTT_BS_BHYT_NLD_TITLE_1;
                Title2 = DefaultDTTReportTitle.GDTT_BS_TITLE_2;
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

        private void LoadDataDot(Guid? id = null)
        {
            DataDot = new ObservableCollection<ComboboxItem>();
            List<BhDuToanThuChiQuery> lstSoQuyetDinh = new List<BhDuToanThuChiQuery>();
            var lstChungTu = _dttBHXHService.GetSoQuyetDinhDTT(_sessionInfo.YearOfWork, IsInTheoChungTu).ToList();
            if (CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.YEAR).ToString())
            {
                lstSoQuyetDinh = lstChungTu.Where(x => x.ILoaiDuToan == (int)EstimateTypeNum.YEAR).OrderByDescending(x => x.DNgayQuyetDinh).ToList();
            }
            else if (CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ADDITIONAL).ToString())
            {
                lstSoQuyetDinh = lstChungTu.Where(x => x.ILoaiDuToan == (int)EstimateTypeNum.ADDITIONAL).OrderByDescending(x => x.DNgayQuyetDinh).ToList();
            }
            else
            {
                lstSoQuyetDinh = lstChungTu.OrderByDescending(x => x.DNgayQuyetDinh).ToList();
            }

            if (lstSoQuyetDinh != null)
            {
                if (IsInTheoChungTu)
                {
                    foreach (var qd in lstSoQuyetDinh)
                    {
                        string mota = "";

                        mota += qd.SNgayQuyetDinh;
                        mota += " ";
                        mota += qd.SSoQuyetDinh;

                        DataDot.Add(new ComboboxItem()
                        {
                            ValueItem = qd.SSoQuyetDinh,
                            DisplayItem = string.Format("{0} - {1}\n{2}", qd.SSoQuyetDinh, qd.SSoChungTu, mota),
                            HiddenValue = qd.SNgayQuyetDinh,
                            DisplayItemOption2 = qd.SSoChungTu
                        });
                    }
                }
                else
                {
                    foreach (var qd in lstSoQuyetDinh)
                    {
                        string mota = "";

                        mota += qd.SNgayQuyetDinh;
                        mota += " ";
                        mota += qd.SSoQuyetDinh;

                        DataDot.Add(new ComboboxItem()
                        {
                            ValueItem = qd.SSoQuyetDinh,
                            DisplayItem = string.Format("{0}\n{1}", qd.SSoQuyetDinh, mota),
                            HiddenValue = qd.SNgayQuyetDinh
                        });
                    }
                }
            }

            if (DataDot != null && DataDot.Count > 0)
            {
                if (id != null)
                {
                    DataDotSelected = DataDot.FirstOrDefault(c => c.Id == id.Value);

                }
                else
                {
                    DataDotSelected = DataDot.FirstOrDefault();
                }
            }
        }

        private DateTime? ConvertToDateTime(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            if (DateTime.TryParse(input, out DateTime result))
            {
                return result.AddDays(1);
            }

            return null;
        }

        private string GetSoQuyetDinhDenLuyKe()
        {
            string soQDs = "";
            if (DataDotSelected != null)
            {
                var ngayQD = DataDotSelected.HiddenValue;

                if (!string.IsNullOrEmpty(ngayQD))
                {
                    var dNgayQD = ConvertToDateTime(ngayQD);
                    var lstSqdDTT = _dttBHXHService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Select(x => x.SSoQuyetDinh).ToList();
                    var lstSQD = lstSqdDTT.Distinct().ToList();
                    soQDs = string.Join(",", lstSQD);
                }
            }
            return soQDs;
        }

        private string GetNgayQuyetDinhDenLuyKe()
        {
            string ngayQDs = "";
            if (DataDotSelected != null)
            {
                var ngayQD = DataDotSelected.HiddenValue;

                if (!string.IsNullOrEmpty(ngayQD))
                {
                    var dNgayQD = ConvertToDateTime(ngayQD);
                    var lstSqdDTT = _dttBHXHService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Select(x => x.DNgayQuyetDinh?.ToString("MM/dd/yyyy")).ToList();
                    var lstSQD = lstSqdDTT.Distinct().ToList();
                    ngayQDs = string.Join(",", lstSQD);
                }
            }
            return ngayQDs;
        }

        public string GetTemplate(string input)
        {
            if (SelectedKieuGiayIn.ValueItem == "1")
                input = input + "_Doc";
            return Path.Combine(ExportPrefix.PATH_BH_KHT, input + FileExtensionFormats.Xlsx);
        }

        public virtual void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(0);
        }

        private void AddEmptyItems(List<ReportKhtDuToanBHXHQuery> listData)
        {
            if (listData.Count < DefaultConst.BHXH_10_Rows)
            {
                var rowRemain = DefaultConst.BHXH_10_Rows - listData.Count;
                for (int i = 0; i < rowRemain; i++)
                {
                    listData.Add(new ReportKhtDuToanBHXHQuery());
                }
            }
        }

        private void LoadEstimateType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = EstimateType.EstimateTypeName[EstimateTypeNum.YEAR], ValueItem = ((int)EstimateTypeNum.YEAR).ToString()},
                new ComboboxItem {DisplayItem = EstimateType.EstimateTypeName[EstimateTypeNum.ADDITIONAL], ValueItem = ((int)EstimateTypeNum.ADDITIONAL).ToString()},
                new ComboboxItem {DisplayItem = EstimateType.EstimateTypeName[EstimateTypeNum.ALL], ValueItem = ((int)EstimateTypeNum.ALL).ToString()}
            };

            CbxEstimateReportType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            _cbxEstimateReportTypeSelected = CbxEstimateReportType.FirstOrDefault();
        }
    }
}
