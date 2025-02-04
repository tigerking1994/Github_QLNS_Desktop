using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.App.Helper;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.UserFunction;
using System.IO;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.Report
{
    public class PrintPhuLucGiaoDuToanDuToanThuViewModel : ViewModelBase
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
                return name = "Phương án giao dự toán thu BHXH, BHYT, BHTN";
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
                //LoadTypeChuKy();
                //LoadTieuDe();
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
        public PrintPhuLucGiaoDuToanDuToanThuViewModel(
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
            LoadAgencies();
            LoadTypeChuKy();
            LoadDanhMuc();
            LoadKieuGiayIn();

        }

        private void OnExportFile(ExportType exportType)
        {
            if (Agencies.Where(item => item.Selected).Count() <= 0)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }

            OnPrintReportKhtDuToanThuBHXHBHYT(exportType);
        }

        public virtual void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A3 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(0);
        }

        private void OnPrintReportKhtDuToanThuBHXHBHYT(ExportType exportType)
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

                    var lstData = _phanBoDTTService.ExportKhtDuToanBHXHBHYTBHTN(yearOfWork, selectedUnits, soQuyetDinh, ngayQuyetDinh, donViTinh, IsMillionRound).ToList();
                    var khoiDuToan = lstData.Where(x => x.IKhoi == (int)IKhoi.KHOIDUTOAN).ToList();
                    var khoiHachToan = lstData.Where(x => x.IKhoi == (int)IKhoi.KHOIHACHTOAN).ToList();

                    if (khoiDuToan.Count > 0)
                    {
                        var firstDuToan = khoiDuToan.Where(x => x.Type == 1).FirstOrDefault();
                        if (firstDuToan != null)
                        {
                            firstDuToan.FTienBHXHNLD = khoiDuToan.Where(x => x.Type == 2).Sum(x => x.FTienBHXHNLD);
                            firstDuToan.FTienBHXHNSDLDD = khoiDuToan.Where(x => x.Type == 2).Sum(x => x.FTienBHXHNSDLDD);
                            firstDuToan.FTienBHTNNLD = khoiDuToan.Where(x => x.Type == 2).Sum(x => x.FTienBHTNNLD);
                            firstDuToan.FTienBHTNNSDLDD = khoiDuToan.Where(x => x.Type == 2).Sum(x => x.FTienBHTNNSDLDD);
                            firstDuToan.FTienBHYTNLDNLD = khoiDuToan.Where(x => x.Type == 2).Sum(x => x.FTienBHYTNLDNLD);
                            firstDuToan.FTienBHYTNSDLDD = khoiDuToan.Where(x => x.Type == 2).Sum(x => x.FTienBHYTNSDLDD);
                            firstDuToan.FTienBHYTQNNLD = khoiDuToan.Where(x => x.Type == 2).Sum(x => x.FTienBHYTQNNLD);
                            firstDuToan.FTienBHYTQNNSDNLD = khoiDuToan.Where(x => x.Type == 2).Sum(x => x.FTienBHYTQNNSDNLD);
                            firstDuToan.FTienBHYTTNQN = khoiDuToan.Where(x => x.Type == 2).Sum(x => x.FTienBHYTTNQN);
                            firstDuToan.FTienBHYTTNCNCNVQP = khoiDuToan.Where(x => x.Type == 2).Sum(x => x.FTienBHYTTNCNCNVQP);
                        }
                    }
                    if (khoiHachToan.Count > 0)
                    {
                        var firstHachToann = khoiHachToan.Where(x => x.Type == 1).FirstOrDefault();
                        if (firstHachToann != null)
                        {
                            firstHachToann.FTienBHXHNLD = khoiHachToan.Where(x => x.Type == 2).Sum(x => x.FTienBHXHNLD);
                            firstHachToann.FTienBHXHNSDLDD = khoiHachToan.Where(x => x.Type == 2).Sum(x => x.FTienBHXHNSDLDD);
                            firstHachToann.FTienBHTNNLD = khoiHachToan.Where(x => x.Type == 2).Sum(x => x.FTienBHTNNLD);
                            firstHachToann.FTienBHTNNSDLDD = khoiHachToan.Where(x => x.Type == 2).Sum(x => x.FTienBHTNNSDLDD);
                            firstHachToann.FTienBHYTNLDNLD = khoiHachToan.Where(x => x.Type == 2).Sum(x => x.FTienBHYTNLDNLD);
                            firstHachToann.FTienBHYTNSDLDD = khoiHachToan.Where(x => x.Type == 2).Sum(x => x.FTienBHYTNSDLDD);
                            firstHachToann.FTienBHYTQNNLD = khoiHachToan.Where(x => x.Type == 2).Sum(x => x.FTienBHYTQNNLD);
                            firstHachToann.FTienBHYTQNNSDNLD = khoiHachToan.Where(x => x.Type == 2).Sum(x => x.FTienBHYTQNNSDNLD);
                            firstHachToann.FTienBHYTTNQN = khoiHachToan.Where(x => x.Type == 2).Sum(x => x.FTienBHYTTNQN);
                            firstHachToann.FTienBHYTTNCNCNVQP = khoiHachToan.Where(x => x.Type == 2).Sum(x => x.FTienBHYTTNCNCNVQP);
                        }
                    }

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListDataThu", lstData.Where(x => x.Type != 0 && x.HasData).ToList());
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("Donvi", (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("TongSoTien", 0);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("ThoiGian", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Nam", yearOfWork);
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("TotalFTienBHXHNLD", lstData.Where(x => x.Type == 2).Sum(x => x.FTienBHXHNLD));
                    data.Add("TotalFTienBHXHNSDLDD", lstData.Where(x => x.Type == 2).Sum(x => x.FTienBHXHNSDLDD));
                    data.Add("TotalFTienCongBHXH", lstData.Where(x => x.Type == 2).Sum(x => x.FTienCongBHXH));
                    data.Add("TotalFTienBHTNNLD", lstData.Where(x => x.Type == 2).Sum(x => x.FTienBHTNNLD));
                    data.Add("TotalFTienBHTNNSDLDD", lstData.Where(x => x.Type == 2).Sum(x => x.FTienBHTNNSDLDD));
                    data.Add("TotalFTienCongBHTN", lstData.Where(x => x.Type == 2).Sum(x => x.FTienCongBHTN));
                    data.Add("TotaFTienBHYTNLDNLD", lstData.Where(x => x.Type == 2).Sum(x => x.FTienBHYTNLDNLD));
                    data.Add("TotalFTienBHYTNSDLDD", lstData.Where(x => x.Type == 2).Sum(x => x.FTienBHYTNSDLDD));
                    data.Add("TotalFTienCongBHYTNLD", lstData.Where(x => x.Type == 2).Sum(x => x.FTienCongBHYTNLD));
                    data.Add("TotalFTienBHYTQNNLD", lstData.Where(x => x.Type == 2).Sum(x => x.FTienBHYTQNNLD));
                    data.Add("TotalFTienBHYTQNNSDNLD", lstData.Where(x => x.Type == 2).Sum(x => x.FTienBHYTQNNSDNLD));
                    data.Add("TotalFTienCongBHYTQN", lstData.Where(x => x.Type == 2).Sum(x => x.FTienCongBHYTQN));
                    data.Add("TotalFTienBHYTTNQN", lstData.Where(x => x.Type == 2).Sum(x => x.FTienBHYTTNQN));
                    data.Add("TotalFTienBHYTTNCNCNVQP", lstData.Where(x => x.Type == 2).Sum(x => x.FTienBHYTTNCNCNVQP));
                    data.Add("TotalFTienCongBHYTTN", lstData.Where(x => x.Type == 2).Sum(x => x.FTienCongBHYTTN));
                    data.Add("TotalFTienCongALL", lstData.Where(x => x.Type == 2).Sum(x => x.FTienCongALL));
                    data.Add("TienBangChu", lstData.Where(x => x.Type == 2).Sum(x => x.FTienCongALL) * donViTinh);
                    AddChuKy(data, _typeChuky);
                    AddNote(data, _typeChuky);
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BHXH_DU_TOAN_THU_BHXH_BHYT_BHTN));
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<ReportKhtDuToanBHXHBHYTBHTNQuery>(templateFileName, data);
                    exportResults.Add(new ExportResult("PHUONG AN GIAO DỰ TOÁN THU BHXH, BHYT, BHTN NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        public string GetTemplate(string input)
        {
            if (SelectedKieuGiayIn.ValueItem == "1")
                input = input + "_A4";
            else
                input = input + "_A3";
            return Path.Combine(ExportPrefix.PATH_BH_DTT, input + FileExtensionFormats.Xlsx);
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

        private void LoadDataDot(Guid? id = null)
        {
            DataDot = new ObservableCollection<ComboboxItem>();
            List<BhDuToanThuChiQuery> lstSoQuyetDinh = new List<BhDuToanThuChiQuery>();
            var lstChungTu = _dttBHXHService.GetSoQuyetDinhDTTBHXHBHYTBHTN(_sessionInfo.YearOfWork).ToList();
            if (lstChungTu != null)
            {
                lstChungTu = lstChungTu.Where(x => x.ILoaiDuToan == (int)EstimateTypeNum.YEAR).ToList();
                lstSoQuyetDinh = lstChungTu.OrderByDescending(x => x.DNgayQuyetDinh).ToList();
            }
            //if (CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.YEAR).ToString())
            //{
            //    lstSoQuyetDinh = lstChungTu.Where(x => x.ILoaiDuToan == (int)EstimateTypeNum.YEAR).OrderByDescending(x => x.DNgayQuyetDinh).ToList();
            //}
            //else if (CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ADDITIONAL).ToString())
            //{
            //    lstSoQuyetDinh = lstChungTu.Where(x => x.ILoaiDuToan == (int)EstimateTypeNum.ADDITIONAL).OrderByDescending(x => x.DNgayQuyetDinh).ToList();
            //}
            //else
            //{
            //    lstSoQuyetDinh = lstChungTu.OrderByDescending(x => x.DNgayQuyetDinh).ToList();
            //}

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

        private void ResetCondition()
        {
            _searchAgencyText = string.Empty;
            _searchBudgetIndexText = string.Empty;
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

        private bool ListAgencyFilter(object obj)
        {
            bool result = true;
            var item = (AgencyModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchAgencyText))
                result = item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private List<string> GetListIdDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            List<string> lstIdDonVi = new List<string>();
            List<string> lstIdDonViDTTM = new List<string>();
            if (DataDotSelected != null)
            {
                List<BhDtPhanBoChungTu> lstDTTDuocXem = new List<BhDtPhanBoChungTu>();
                List<BhPbdttmBHYT> lstDTTMDuocXem = new List<BhPbdttmBHYT>();

                if (InLuyKeChecked)
                {
                    var soQuyetDinh = GetSoQuyetDinhDenLuyKe();
                    var ngayQuyetDinh = GetNgayQuyetDinhDenLuyKe();
                    var lstQuery = _dttBHXHService.FindBySoQuyetDinhLuyKe(soQuyetDinh, ngayQuyetDinh, yearOfWork).ToList();
                    var lstQueryDttmBhyt = _dttBHXHService.FindBySoQuyetDinhLuyKeDttmBHYT(soQuyetDinh, ngayQuyetDinh, yearOfWork).ToList();
                    lstDTTDuocXem = _mapper.Map<List<BhDtPhanBoChungTu>>(lstQuery);
                    lstDTTMDuocXem = _mapper.Map<List<BhPbdttmBHYT>>(lstQueryDttmBhyt);
                }
                else
                {
                    lstDTTDuocXem = _dttBHXHService.FindBySoQuyetDinh(DataDotSelected.ValueItem, yearOfWork).ToList();
                    lstDTTMDuocXem = _dttmBHYTService.FindBySoQuyetDinh(DataDotSelected.ValueItem, yearOfWork).ToList();
                }

                if (lstDTTDuocXem != null)
                {
                    foreach (var ct in lstDTTDuocXem)
                    {
                        lstIdDonVi = ct.SDsidMaDonVi.Split(",").Distinct().ToList();
                    }
                }

                if (lstDTTMDuocXem != null)
                {
                    foreach (var ct in lstDTTMDuocXem)
                    {
                        lstIdDonViDTTM = ct.SDS_IDMaDonVi.Split(",").Distinct().ToList();
                    }
                }
                lstIdDonVi = lstIdDonVi.Concat(lstIdDonViDTTM).Distinct().ToList();
            }
            return lstIdDonVi;
        }

        private string GetNgayQuyetDinhDenLuyKe()
        {
            string ngayQDs = "";
            if (DataDotSelected != null)
            {
                var ngayQD = DataDotSelected.HiddenValue;
                List<string> lstNgayDTT = new List<string>();
                List<string> lstNgayTTMBHYT = new List<string>();
                if (!string.IsNullOrEmpty(ngayQD))
                {
                    var dNgayQD = ConvertToDateTime(ngayQD);
                    var lstSqdDTT = _dttBHXHService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Select(x => x.DNgayQuyetDinh?.ToString("MM/dd/yyyy")).ToList();
                    lstNgayDTT = lstSqdDTT.Distinct().ToList();
                    var lstNgayDTTM = _dttmBHYTService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Select(x => x.DNgayQuyetDinh.ToString("MM/dd/yyyy")).ToList();
                    lstNgayTTMBHYT = lstNgayDTTM.Distinct().ToList();
                    ngayQDs = string.Join(",", lstNgayDTT.Concat(lstNgayTTMBHYT).Distinct().ToList());
                }
            }
            return ngayQDs;
        }

        private string GetSoQuyetDinhDenLuyKe()
        {
            string soQDs = "";
            if (DataDotSelected != null)
            {
                var ngayQD = DataDotSelected.HiddenValue;
                List<string> lstSQDTT = new List<string>();
                List<string> lstSQDTTMBHYT = new List<string>();
                if (!string.IsNullOrEmpty(ngayQD))
                {
                    var dNgayQD = ConvertToDateTime(ngayQD);
                    var lstSqdDTT = _dttBHXHService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Select(x => x.SSoQuyetDinh).ToList();
                    lstSQDTT = lstSqdDTT.Distinct().ToList();
                    var lstSqdDttmbhyt = _dttmBHYTService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Select(x => x.SSoQuyetDinh).ToList();
                    lstSQDTTMBHYT = lstSqdDttmbhyt.Distinct().ToList();
                    soQDs = string.Join(",", lstSQDTT.Concat(lstSQDTTMBHYT).Distinct().ToList());
                }
            }
            return soQDs;
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

        private void LoadTypeChuKy()
        {
            SMaBaoCao = TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHXH_BHYT_BHTN;
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHXH_BHYT_BHTN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            _typeChuky = TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHXH_BHYT_BHTN;
            Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_1;
            Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.DTT_PAG_TIEU_DE_2;
            Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultDTTReportTitle.DTT_PAG_TIEU_DE_3;
        }

        private void LoadTieuDe()
        {
            LoadTypeChuKy();
        }

        private void OnNoteCommand()
        {
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();
            BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHXH_BHYT_BHTN };
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(Agencies);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = SMaBaoCao;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = false;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = true;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
        }
    }
}
