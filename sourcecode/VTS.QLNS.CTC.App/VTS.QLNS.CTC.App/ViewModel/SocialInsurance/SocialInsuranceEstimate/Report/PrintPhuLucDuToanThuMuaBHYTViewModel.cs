using AutoMapper;
using log4net;
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
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.Report
{
    public class PrintPhuLucDuToanThuMuaBHYTViewModel : ViewModelBase
    {
        private IExportService _exportService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private ILog _logger;
        private IMapper _mapper;
        private INsDonViService _donViService;
        private IPbdttmBHYTService _dttmBHYTService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private IPbdttmBHYTChiTietService _phanBoDTTMService;
        private IBhBaoCaoGhiChuService _bhGhiChuService;
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
                return name = "Báo cáo dự toán thu mua BHYT";
            }
        }

        public override string Name => ReportName;
        public override string Title => ReportName;
        public override string Description => ReportName;

        private bool _isMillionRound;
        public bool IsMillionRound
        {
            get => _isMillionRound;
            set => SetProperty(ref _isMillionRound, value);
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

        private LoaiDTTM _inTheo;
        public LoaiDTTM InTheo
        {
            get => _inTheo;
            set
            {
                SetProperty(ref _inTheo, value);
                LoadTypeChuKy();
                LoadTieuDe();
                LoadAgencies();
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
                    LoadDataDot();
                    LoadAgencies();
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
        public RelayCommand NoteCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand DataInterpretationCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintPhuLucDuToanThuMuaBHYTViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            INsDonViService donViService,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IPbdttmBHYTService iPbdttmBHYTService,
            IDanhMucService iDanhMucService,
            IPbdttmBHYTChiTietService iPbdttmBHYTChiTietService,
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
            _danhMucService = iDanhMucService;
            _dttmBHYTService = iPbdttmBHYTService;
            _phanBoDTTMService = iPbdttmBHYTChiTietService;
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
            LoadDataDot();
            ResetCondition();
            LoadTieuDe();
            LoadAgencies();
            LoadTypeChuKy();
            LoadDanhMuc();
            LoadKieuGiayIn();
            InTheo = LoaiDTTM.BHYT_TN;
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
                List<BhPbdttmBHYT> lstDTTDuocXem = new List<BhPbdttmBHYT>();
                if (IsInTheoChungTu && !InLuyKeChecked)
                {
                    lstDTTDuocXem = _dttmBHYTService.FindBySoChungTu(DataDotSelected.DisplayItemOption2, yearOfWork).ToList();
                }
                else if (InLuyKeChecked)
                {
                    var soQuyetDinh = GetSoQuyetDinhDenLuyKe();
                    var ngayQuyetDinh = GetNgayQuyetDinhDenLuyKe();
                    var lstQuery = _dttmBHYTService.FindBySoQuyetDinhLuyKe(soQuyetDinh, ngayQuyetDinh, yearOfWork).ToList();
                    lstDTTDuocXem = _mapper.Map<List<BhPbdttmBHYT>>(lstQuery);
                }
                else if (!IsInTheoChungTu && !InLuyKeChecked)
                {
                    lstDTTDuocXem = _dttmBHYTService.FindBySoQuyetDinh(DataDotSelected.ValueItem, yearOfWork).ToList();
                }

                if (lstDTTDuocXem != null)
                {
                    foreach (var ct in lstDTTDuocXem)
                    {
                        var unitIDs = ct.SDS_IDMaDonVi.Split(",").Distinct().ToList();
                        if (unitIDs.Any())
                            lstIdDonVi.AddRange(unitIDs);
                    }
                }

                if (InTheo == LoaiDTTM.BHYT_HSSV && lstIdDonVi.Any())
                {
                    var lstDvHSSV = _phanBoDTTMService.FindByXauNoiMaVaSoQuyetDinh(DataDotSelected.ValueItem, BhxhMLNS.BHYT_HSSV.Split(",").ToList(), yearOfWork, true).Select(x => x.IID_MaDonVi).ToList();
                    var lstResult = lstIdDonVi.Intersect(lstDvHSSV).ToList();
                    lstIdDonVi = lstResult;
                }
                else if (InTheo == LoaiDTTM.BHYT_TN && lstIdDonVi.Any())
                {
                    var lstDvHSSV = _phanBoDTTMService.FindByXauNoiMaVaSoQuyetDinh(DataDotSelected.ValueItem, BhxhMLNS.BHYT_HSSV.Split(",").ToList(), yearOfWork, false).Select(x => x.IID_MaDonVi).ToList();
                    var lstResult = lstIdDonVi.Intersect(lstDvHSSV).ToList();
                    lstIdDonVi = lstResult;
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
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
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

        private void OnExportFile(ExportType exportType)
        {
            if (Agencies.Where(item => item.Selected).Count() <= 0)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }
            else
            {
                OnPrintReportKhtmDuToanThuBhyt(exportType);
            }
        }

        public void OnPrintReportKhtmDuToanThuBhyt(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = "";
                    string fileNamePrefix;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.Id.ToString()).ToList());
                    List<ReportKhtmDuToanBHYTQuery> lstBHYT = new List<ReportKhtmDuToanBHYTQuery>();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();

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

                    //BHYT than nhan
                    if (InTheo == LoaiDTTM.BHYT_TN)
                    {
                        lstBHYT = _phanBoDTTMService.ExportDuToanThuBhytThanNhan(yearOfWork, selectedUnits, BhxhMLNS.THU_MUA_BHYT_TNQN, BhxhMLNS.THU_MUA_BHYT_CNVQP
                            , BhxhMLNS.SM_DU_TOAN, BhxhMLNS.SM_HACH_TOAN, soQuyetDinh, ngayQuyetDinh, donViTinh, IsMillionRound).Where(x => x.HasData).ToList();
                        //lstBHYT = lstBHYT.OrderBy(x => x.IdDonVi).ToList();
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHTM_Du_Toan_Thu_BHYT_Than_Nhan));
                        data.Add("TongTNQNDuToan", lstBHYT.Sum(x => x.TNQNDuToan));
                        data.Add("TongTNCNVQPDuToan", lstBHYT.Sum(x => x.TNCNVQPDuToan));
                        data.Add("TongCongDuToan", lstBHYT.Sum(x => x.TongDuToan));
                        data.Add("TongTNQNHachToan", lstBHYT.Sum(x => x.TNQNHachToan));
                        data.Add("TongTNCNVQPHachToan", lstBHYT.Sum(x => x.TNCNVQPHachToan));
                        data.Add("TongCongHachToan", lstBHYT.Sum(x => x.TongHachToan));
                        data.Add("TongTNQN", lstBHYT.Sum(x => x.TNQNDuToan) + lstBHYT.Sum(x => x.TNQNHachToan));
                        data.Add("TongTNCNVQP", lstBHYT.Sum(x => x.TNCNVQPDuToan) + lstBHYT.Sum(x => x.TNCNVQPHachToan));
                        data.Add("TongCong", lstBHYT.Sum(x => x.TongCongThanNhan));
                    }
                    //BHYT HSSV
                    else
                    {
                        lstBHYT = _phanBoDTTMService.ExportDuToanThuBhytHSSV(yearOfWork, selectedUnits, BhxhMLNS.SLNS_HSSV, BhxhMLNS.SLNS_LUU_HS, BhxhMLNS.SLNS_HVQS
                            , BhxhMLNS.SLNS_SQ_DU_BI, soQuyetDinh, ngayQuyetDinh, donViTinh, IsMillionRound).Where(x => x.HasData).ToList();
                        //lstBHYT = lstBHYT.OrderBy(x => x.IdDonVi).ToList();
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHTM_Du_Toan_Thu_BHYT_HSSV));
                        data.Add("TongHSSV", lstBHYT.Sum(x => x.HSSV));
                        data.Add("TongLuuHS", lstBHYT.Sum(x => x.LuuHS));
                        data.Add("TongHSSVLuuHS", lstBHYT.Sum(x => x.TongHSSV));
                        data.Add("TongHVQS", lstBHYT.Sum(x => x.HVQS));
                        data.Add("TongSQDuBi", lstBHYT.Sum(x => x.SQDuBi));
                        data.Add("TongCong", lstBHYT.Sum(x => x.TongCongHSSV));
                    }
                    AddEmptyItems(lstBHYT);
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", lstBHYT);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("Nam", yearOfWork);
                    AddChuKy(data, _typeChuky);
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                    int stt = 1;
                    foreach (var i in lstBHYT.Where(x => x.HasData))
                    {
                        i.STT = stt++;
                    }

                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<ReportKhtmDuToanBHYTQuery>(templateFileName, data);
                    results.Add(new ExportResult("DỰ TOÁN THU MUA BHYT " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        private void LoadTypeChuKy()
        {
            if (InTheo == LoaiDTTM.BHYT_TN)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_BHYT_KHTM_THAN_NHAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.RPT_BHYT_KHTM_THAN_NHAN;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.BHYT_TN_TIEU_DE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.BHYT_TN_TIEU_DE_2;
            }
            else
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_BHYT_KHTM_HSSV) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.RPT_BHYT_KHTM_HSSV;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.BHYT_HSSV_TIEU_DE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.BHYT_HSSV_TIEU_DE_2;
            }
        }
        private void OnNoteCommand()
        {
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();
            BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_BHYT_KHTM_THAN_NHAN, TypeChuKy.RPT_BHYT_KHTM_HSSV };
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(Agencies);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuky;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = false;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = true;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
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
            var lstSoQuyet = _dttmBHYTService.GetSoQuyetDinhDTTM(_sessionInfo.YearOfWork, IsInTheoChungTu).OrderByDescending(x => x.DNgayQuyetDinh).ToList();

            if (lstSoQuyet != null)
            {
                if (IsInTheoChungTu)
                {
                    foreach (var qd in lstSoQuyet)
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
                    foreach (var qd in lstSoQuyet)
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
                    var lstSqdDTT = _dttmBHYTService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Select(x => x.SSoQuyetDinh).ToList();
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
                    var lstSqdDTT = _dttmBHYTService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Select(x => x.DNgayQuyetDinh.ToString("MM/dd/yyyy")).ToList();
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
            return Path.Combine(ExportPrefix.PATH_BH_KHTM, input + FileExtensionFormats.Xlsx);
        }

        public virtual void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(1);
        }

        private void AddEmptyItems(List<ReportKhtmDuToanBHYTQuery> listData)
        {
            if (listData.Count < DefaultConst.BHXH_10_Rows)
            {
                var rowRemain = DefaultConst.BHXH_10_Rows - listData.Count;
                for (int i = 0; i < rowRemain; i++)
                {
                    listData.Add(new ReportKhtmDuToanBHYTQuery());
                }
            }
        }
    }
}
