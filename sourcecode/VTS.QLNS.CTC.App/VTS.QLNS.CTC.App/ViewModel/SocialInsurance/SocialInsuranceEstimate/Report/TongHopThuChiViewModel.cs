using AutoMapper;
using ControlzEx.Standard;
using log4net;
using System;
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
    public class TongHopThuChiViewModel : ViewModelBase
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
        private IDttBHXHPhanBoChiTietService _dtChungTuChiTietService;
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
        public static SocialInsuranceDivisionEstimatePrintType ReportTypeValue { get; set; }
        public int ReportNameTypeValue;
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
                switch (ReportNameTypeValue)
                {
                    case (int)SocialInsuranceDivisionEstimatePrintType.COVER_SHEET:
                        name = "Báo cáo giao dự toán - Đơn vị";
                        break;
                    case (int)SocialInsuranceDivisionEstimatePrintType.DU_TOAN_THU_CHI_TONG_HOP:
                        name = "Tổng hợp dự toán thu, chi BHXH, BHYT, BHTN";
                        break;
                }
                return name;
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

        public bool IsEnableLoaiThu => SocialInsuranceDivisionEstimatePrintType.COVER_SHEET.Equals(ReportTypeValue);

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

        private TCPrintType _inTheo;
        public TCPrintType InTheo
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
        public bool InLuyKeChecked { get; set; }

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

        public TongHopThuChiViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            INsDonViService donViService,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IDttBHXHPhanBoService iDttBHXHPhanBoService,
            IDttBHXHPhanBoChiTietService iDttBHXHPhanBoChiTietService,
            IPbdttmBHYTService iPbdttmBHYTService,
            IPbdtcBHXHService iPbdtcBHXHService,
            IDanhMucService iDanhMucService,
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
            _dtChungTuChiTietService = iDttBHXHPhanBoChiTietService;
            _danhMucService = iDanhMucService;
            _dttmBHYTService = iPbdttmBHYTService;
            _dtcBHXHService = iPbdtcBHXHService;
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
            LoadAgencies();
            LoadTypeChuKy();
            LoadDanhMuc();
            InTheo = TCPrintType.All;
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
                lstIdDonVi = _dttBHXHService.GetDonViDttDttnDtc(yearOfWork, DataDotSelected.ValueItem).Distinct().ToList();
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

        private void LoadReportType()
        {
            _reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() },
                new ComboboxItem { DisplayItem = "Chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencyDetail.ToString() }
            };
            _selectedReportType = _reportTypes.First();
        }

        private string GetTemplate()
        {
            string input = "";
            if (ReportNameTypeValue == (int)SocialInsuranceDivisionEstimatePrintType.COVER_SHEET)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_DU_TOAN_TONG_HOP_THU_CHI);
            }
            else if (ReportNameTypeValue == (int)SocialInsuranceDivisionEstimatePrintType.DU_TOAN_THU_CHI_TONG_HOP)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_DU_TOAN_TONG_HOP_THU_CHI_BHXH_BHYT_BHTN);
            }
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
            if (ReportNameTypeValue == (int)SocialInsuranceDivisionEstimatePrintType.COVER_SHEET)
            {
                BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.GIAO_DU_TOAN_TONG_HOP_THU_CHI };
            }
            else
            {
                BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.DU_TOAN_TONG_HOP_THU_CHI };
            }

            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(Agencies);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuky;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = true;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = false;
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

            if (ReportNameTypeValue == (int)SocialInsuranceDivisionEstimatePrintType.DU_TOAN_THU_CHI_TONG_HOP)
            {
                ExportTongHopDuToanThuChi(exportType);
            }
            else if (ReportNameTypeValue == (int)SocialInsuranceDivisionEstimatePrintType.COVER_SHEET && SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
            {
                ExportTongHopThuChiDonVi(exportType);
            }
            else if (ReportNameTypeValue == (int)SocialInsuranceDivisionEstimatePrintType.COVER_SHEET && SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString())
            {
                ExportTongHopThuChiDonViTongHop(exportType);
            }
        }

        private void ExportTongHopThuChiDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    List<ExportResult> results = new List<ExportResult>();
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

                    foreach (var dv in lstIdDonVi)
                    {
                        var donvi = _donViService.FindByMaDonViAndNamLamViec(dv.Id, yearOfWork);
                        var lstDataThu = _dttBHXHService.ExportTongHopThuDonVi(yearOfWork, dv.Id, soQuyetDinh, donViTinh, ngayQuyetDinh, IsMillionRound).ToList();
                        var lstDataChi = _dttBHXHService.ExportTongHopChiDonVi(yearOfWork, dv.Id, soQuyetDinh, donViTinh, ngayQuyetDinh, IsMillionRound).ToList();

                        foreach (var item in lstDataThu)
                        {
                            item.FNLD = Math.Round(item.FNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.FNSD = Math.Round(item.FNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.FTongSo = item.FNLD + item.FNSD;
                        }

                        foreach (var item in lstDataChi)
                        {
                            item.FDuToan = Math.Round(item.FDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.FHachToan = Math.Round(item.FHachToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.FTongSoChi = item.FDuToan+ item.FHachToan;
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("ListDataThu", lstDataThu);
                        data.Add("ListDataChi", lstDataChi);
                        data.Add("YearOfWork", yearOfWork);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DiaDiem", string.Empty);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                        data.Add("DonVi", donvi.TenDonVi);
                        data.Add("MenhGia", SelectedUnit != null ? SelectedUnit.DisplayItem : "");
                        data.Add("Title1", Title1);
                        data.Add("Title2", Title2);
                        data.Add("Title3", Title3);
                        //Tính tổng cộng
                        data.Add("TongSoThu", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FTongSo));
                        data.Add("TongNLD", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FNLD));
                        data.Add("TongNSD", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FNSD));
                        data.Add("TienBangChuThu", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FTongSo) * donViTinh);
                        data.Add("TienBangChuChi", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FTongSoChi) * donViTinh);
                        data.Add("TongSoChi", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FTongSoChi));
                        data.Add("TongDuToan", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FDuToan));
                        data.Add("TongHachToan", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FHachToan));

                        if (InTheo == TCPrintType.Chi)
                        {
                            data.Add("ShowChi", true);
                        }
                        else if (InTheo == TCPrintType.Thu)
                        {
                            data.Add("ShowThu", true);
                        }
                        else
                        {
                            data.Add("ShowThu", true);
                            data.Add("ShowChi", true);
                        }
                        AddChuKy(data, _typeChuky);
                        AddNote(data, _typeChuky, dv.IIDMaDonVi);
                        string templateFileName;
                        templateFileName = GetTemplate();
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<BhDuToanTongHopThuChiQuery>(templateFileName, data);
                        results.Add(new ExportResult("BÁO CÁO DỰ TOÁN TỔNG HỢP THU CHI " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (InMotToChecked)
                        {
                            if (exportType == ExportType.PDF)
                            {
                                exportType = ExportType.PDF_ONE_PAPER;
                            }
                            else
                            {
                                exportType = ExportType.EXCEL_ONE_PAPER;
                            }
                        }
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

        private void ExportTongHopThuChiDonViTongHop(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    if (lstIdDonVi != null)
                    {
                        var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.Id.ToString()).ToList());
                        List<ExportResult> results = new List<ExportResult>();
                        string soQuyetDinh = "";
                        string ngayQuyetDinh = "";
                        var menhGia = SelectedUnit != null ? SelectedUnit.DisplayItem : "";
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
                        var lstDataThu = _dttBHXHService.ExportTongHopThuDonVi(yearOfWork, selectedUnits, soQuyetDinh, donViTinh, ngayQuyetDinh, IsMillionRound).ToList();
                        var lstDataChi = _dttBHXHService.ExportTongHopChiDonVi(yearOfWork, selectedUnits, soQuyetDinh, donViTinh, ngayQuyetDinh, IsMillionRound).ToList();

                        if (lstDataThu.Any())
                        {
                            foreach (var item in lstDataThu)
                            {
                                item.FNLD = Math.Round(item.FNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.FNSD = Math.Round(item.FNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.FTongSo = item.FNLD + item.FNSD;
                            }
                        }

                        if (lstDataChi.Any())
                        {
                            foreach (var item in lstDataChi)
                            {
                                item.FDuToan = Math.Round(item.FDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.FHachToan = Math.Round(item.FHachToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.FTongSoChi = item.FDuToan + item.FHachToan;
                            }
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("ListDataThu", lstDataThu);
                        data.Add("ListDataChi", lstDataChi);
                        data.Add("YearOfWork", yearOfWork);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DiaDiem", string.Empty);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("DonViTinh", "Đơn vị tính: " + menhGia);
                        data.Add("DonVi", _sessionInfo.TenDonVi);
                        data.Add("MenhGia", menhGia);
                        data.Add("Title1", Title1);
                        data.Add("Title2", Title2);
                        data.Add("Title3", Title3);
                        //Tính tổng cộng
                        data.Add("TongSoThu", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FTongSo));
                        data.Add("TienBangChuThu", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FTongSo) * donViTinh);
                        data.Add("TongNLD", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FNLD));
                        data.Add("TongNSD", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FNSD));

                        data.Add("TongSoChi", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FTongSoChi));
                        data.Add("TienBangChuChi", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FTongSoChi) * donViTinh);
                        data.Add("TongDuToan", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FDuToan));
                        data.Add("TongHachToan", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FHachToan));

                        if (InTheo == TCPrintType.Chi)
                        {
                            data.Add("ShowChi", true);
                        }
                        else if (InTheo == TCPrintType.Thu)
                        {
                            data.Add("ShowThu", true);
                        }
                        else
                        {
                            data.Add("ShowThu", true);
                            data.Add("ShowChi", true);
                        }
                        AddChuKy(data, _typeChuky);
                        AddNote(data, _typeChuky);
                        string templateFileName;
                        templateFileName = GetTemplate();
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<BhDuToanTongHopThuChiQuery>(templateFileName, data);
                        results.Add(new ExportResult("BÁO CÁO DỰ TOÁN TỔNG HỢP THU CHI " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ExportTongHopDuToanThuChi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    bool isTongHop = true;
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        isTongHop = false;
                    }
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    var lstMaDonVi = string.Join(",", Agencies.Any(x => x.Selected) ? Agencies.Where(item => item.Selected).Select(x => x.IIDMaDonVi) : new List<string>());
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);

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

                    if (isTongHop)
                    {
                        exportResults.Add(ExportResultReport(donViTinh, exportType, lstMaDonVi, isTongHop, soQuyetDinh, ngayQuyetDinh));
                    }
                    else
                    {
                        foreach (var item in Agencies.Where(x => x.Selected))
                        {
                            exportResults.Add(ExportResultReport(donViTinh, exportType, item.IIDMaDonVi, isTongHop, soQuyetDinh, ngayQuyetDinh, item.TenDonVi));
                        }

                    }

                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (InMotToChecked)
                        {
                            if (exportType == ExportType.PDF)
                            {
                                exportType = ExportType.PDF_ONE_PAPER;
                            }
                            else
                            {
                                exportType = ExportType.EXCEL_ONE_PAPER;
                            }
                        }
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

        private ExportResult ExportResultReport(int donViTinh, ExportType exportType, string lstDonVi, bool isTongHop, string soQuyetDinh, string ngayQuyetDinh, string tenDonVi = "")
        {
            try
            {
                IsLoading = true;
                string templateFileName = "", fileNamePrefix = "";
                var yearOfWork = _sessionInfo.YearOfWork;
                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                CurrencyToText currencyToText = new CurrencyToText();

                //BHYT - Quan nhan
                var listData = _dtChungTuChiTietService.ExportTongHopDuToanThuChi(yearOfWork, lstDonVi, soQuyetDinh, ngayQuyetDinh, donViTinh, IsMillionRound).ToList();
                foreach (var item in listData)
                {
                    item.FDuToan= Math.Round(item.FDuToan, MidpointRounding.AwayFromZero);
                    item.FHachToan = Math.Round(item.FHachToan, MidpointRounding.AwayFromZero);
                }
                templateFileName = GetTemplate();
                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                CalculateDataExport(listData);
                data.Add("ListData", listData);
                data.Add("TieuDe1", Title1);
                data.Add("TieuDe2", Title2);
                data.Add("TieuDe3", Title3);
                data.Add("TongCongThu", listData.Where(x => new List<string>() { "A" }.Contains(x.STT)).Sum(x => x.FSoTien));
                data.Add("TongCongChi", listData.Where(x => new List<string>() { "B" }.Contains(x.STT)).Sum(x => x.FSoTien));
                data.Add("TongCongThuChu", listData.Where(x => new List<string>() { "A" }.Contains(x.STT)).Sum(x => x.FSoTien) * donViTinh);
                data.Add("TongCongChiChu", listData.Where(x => new List<string>() { "B" }.Contains(x.STT)).Sum(x => x.FSoTien) * donViTinh);
                data.Add("TongCong", listData.Where(x => new List<string>() { "A" }.Contains(x.STT)).Sum(x => x.FSoTien)
                    + listData.Where(x => new List<string>() { "B" }.Contains(x.STT)).Sum(x => x.FSoTien));
                data.Add("CurrencyToText", currencyToText);
                data.Add("FormatNumber", formatNumber);
                data.Add("Cap1", _sessionInfo.TenDonVi);
                data.Add("DonVi", string.IsNullOrEmpty(tenDonVi) ? "" : "Đơn vị: " + tenDonVi);
                data.Add("h1", "");
                data.Add("h2", "");
                data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                data.Add("DiaDiem", _diaDiem);
                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                data.Add("YearOfWork", yearOfWork);
                AddChuKy(data, _typeChuky);
                if (isTongHop)
                {
                    AddNote(data, _typeChuky);
                }
                else
                {
                    AddNote(data, _typeChuky, lstDonVi);
                }

                string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                var xlsFile = _exportService.Export<BhReportQttBHXHChiTietQuery>(templateFileName, data);
                return new ExportResult("DỰ TOÁN TỔNG HỢP THU CHI BẢO HIỂM XÃ HỘI NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw;
            }
        }

        private void CalculateDataExport(IEnumerable<BhReportQttBHXHChiTietQuery> lstData)
        {
            if (lstData.IsEmpty())
                return;
            var lstParentRecal = lstData.Where(x => lstData.Where(w => !w.IIdParent.IsNullOrEmpty())
                .Select(x => x.IIdParent).Distinct().Contains(x.IIdChungTu)).OrderByDescending(o => o.ILevel).ToList();
            foreach (var item in lstParentRecal)
            {
                item.FSoTien = lstData.Where(x => x.IIdParent.Equals(item.IIdChungTu)).Sum(s => s.FSoTien);
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
            if (ReportNameTypeValue == (int)SocialInsuranceDivisionEstimatePrintType.COVER_SHEET)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.GIAO_DU_TOAN_TONG_HOP_THU_CHI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.GIAO_DU_TOAN_TONG_HOP_THU_CHI;
            }
            else
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.DU_TOAN_TONG_HOP_THU_CHI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.DU_TOAN_TONG_HOP_THU_CHI;
            }

            //Bao cao du toan tong hop thu chi
            if (ReportNameTypeValue == (int)SocialInsuranceDivisionEstimatePrintType.DU_TOAN_THU_CHI_TONG_HOP)
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.DTT_TH_TIEU_DE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultDTTReportTitle.DTT_BS_TIEU_DE_3;
            }
            //Bao cao giao du toan
            else if (InTheo == TCPrintType.All && (CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.YEAR).ToString() || CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ALL).ToString()) && ReportNameTypeValue == (int)SocialInsuranceDivisionEstimatePrintType.COVER_SHEET)
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_3;
            }
            else if (InTheo == TCPrintType.Thu && (CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.YEAR).ToString() || CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ALL).ToString()) && ReportNameTypeValue == (int)SocialInsuranceDivisionEstimatePrintType.COVER_SHEET)
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.DTT_THU_TIEU_DE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_3;
            }
            else if (InTheo == TCPrintType.Chi && (CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.YEAR).ToString() || CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ALL).ToString()) && ReportNameTypeValue == (int)SocialInsuranceDivisionEstimatePrintType.COVER_SHEET)
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.DTT_CHI_TIEU_DE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_3;
            }
            //Loại bổ sung
            else if (InTheo == TCPrintType.All && CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ADDITIONAL).ToString() && ReportNameTypeValue == (int)SocialInsuranceDivisionEstimatePrintType.COVER_SHEET)
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.DTT_BS_TIEU_DE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultDTTReportTitle.DTT_BS_TIEU_DE_3;
            }
            else if (InTheo == TCPrintType.Thu && CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ADDITIONAL).ToString() && ReportNameTypeValue == (int)SocialInsuranceDivisionEstimatePrintType.COVER_SHEET)
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.DTT_BS_THU_TIEU_DE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultDTTReportTitle.DTT_BS_TIEU_DE_3;
            }
            else if (InTheo == TCPrintType.Chi && CbxEstimateReportTypeSelected.ValueItem == ((int)EstimateTypeNum.ADDITIONAL).ToString() && ReportNameTypeValue == (int)SocialInsuranceDivisionEstimatePrintType.COVER_SHEET)
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.DTT_BS_CHI_TIEU_DE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultDTTReportTitle.DTT_BS_TIEU_DE_3;
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
            var lstChungTu = _dttBHXHService.GetSoQuyetDinh(_sessionInfo.YearOfWork).ToList();

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
                    var lstSqdDTTM = _dttmBHYTService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Select(x => x.SSoQuyetDinh).ToList();
                    var lstSqdDTC = _dtcBHXHService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Select(x => x.SSoQuyetDinh).ToList();
                    var lstSQD = lstSqdDTT.Concat(lstSqdDTTM).Concat(lstSqdDTC).Distinct().ToList();
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
                    var lstSqdDTTM = _dttmBHYTService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Select(x => x.DNgayQuyetDinh.ToString("MM/dd/yyyy")).ToList();
                    var lstSqdDTC = _dtcBHXHService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Select(x => x.DNgayQuyetDinh?.ToString("MM/dd/yyyy")).ToList();
                    var lstSQD = lstSqdDTT.Concat(lstSqdDTTM).Concat(lstSqdDTC).Distinct().ToList();
                    ngayQDs = string.Join(",", lstSQD);
                }
            }
            return ngayQDs;
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
