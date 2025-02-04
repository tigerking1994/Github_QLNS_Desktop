using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport
{
    public class PrintArmyAverageViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IMapper _mapper;
        private readonly INsQsChungTuChiTietService _chungTuChiTietService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private string _cap1;
        private DmChuKy _dmChuKy;
        private string _title;
        private string _diaDiem;

        public override string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public override string Description => "Chọn thông số để in chi tiết đơn vị hoặc tổng hợp theo tháng, quý, năm";
        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintArmyAverage);

        private ReportArmy _reportType;
        public ReportArmy ReportType
        {
            get => _reportType;
            set
            {
                SetProperty(ref _reportType, value);
                LoadAgencies();
            }
        }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _monthFrom;
        public ComboboxItem MonthFrom
        {
            get => _monthFrom;
            set
            {
                SetProperty(ref _monthFrom, value);
                if (_monthFrom != null)
                    LoadAgencies();
            }
        }

        private ComboboxItem _monthTo;
        public ComboboxItem MonthTo
        {
            get => _monthTo;
            set
            {
                SetProperty(ref _monthTo, value);
                if (_monthTo != null)
                    LoadAgencies();
            }
        }

        private List<ComboboxItem> _paperTypes;
        public List<ComboboxItem> PaperTypes
        {
            get => _paperTypes;
            set => SetProperty(ref _paperTypes, value);
        }

        private ComboboxItem _selectedPaper;
        public ComboboxItem SelectedPaper
        {
            get => _selectedPaper;
            set => SetProperty(ref _selectedPaper, value);
        }

        public string LabelSelectedCountAgency
        {
            get
            {
                var totalCount = Agencies.Count;
                var totalSelected = Agencies.Count(item => item.IsChecked);
                return $"ĐƠN VỊ ({totalSelected}/{totalCount})";
            }
        }

        private string _searchAgency;
        public string SearchAgency
        {
            get => _searchAgency;
            set
            {
                if (SetProperty(ref _searchAgency, value))
                {
                    _listAgency.Refresh();
                }
            }
        }

        private bool _selectAllAgency;
        public bool SelectAllAgency
        {
            get => Agencies.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllAgency, value);
                foreach (var item in Agencies) item.IsChecked = _selectAllAgency;
            }
        }

        private ObservableCollection<CheckBoxItem> _agencies;
        public ObservableCollection<CheckBoxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        public bool IsExportEnable
        {
            get
            {
                if (_agencies != null)
                    return _agencies.Where(x => x.IsChecked).Count() > 0;
                return false;
            }
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

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintArmyAverageViewModel(ISessionService sessionService,
           INsDonViService donViService,
           IMapper mapper,
           INsQsChungTuChiTietService chungTuChiTietService,
           IExportService exportService,
           IDanhMucService danhMucService,
           IDmChuKyService dmChuKyService,
           ILog logger,
           DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _chungTuChiTietService = chungTuChiTietService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ExportCommand = new RelayCommand(obj => IsOpenExportPopup = true);
            ExportExcelCommand = new RelayCommand(obj => OnExportFile((int)ExportType.EXCEL));
            ExportPDFCommand = new RelayCommand(obj =>
            {
                OnExportFile((int)ExportType.PDF);
            });
            PrintCommand = new RelayCommand(obj =>
            {
                OnExportFile((int)ExportType.PDF);
            });
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            _title = string.Format("In báo cáo quân số bình quân các tháng năm {0}", _sessionInfo.YearOfWork);
            _reportType = ReportArmy.AgencyDetail;
            LoadTieuDe();
            LoadPaperType();
            LoadMonths();
            LoadDanhMuc();
            LoadAgencies();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_BINHQUAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }

        /// <summary>
        /// Tạo dữ liệu combobox tháng
        /// </summary>
        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            _months.Add(new ComboboxItem("Đầu năm", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem("Tháng " + i, i.ToString());
                _months.Add(month);
            }
            _months.Add(new ComboboxItem("Quý I", "1,2,3"));
            _months.Add(new ComboboxItem("Quý II", "4,5,6"));
            _months.Add(new ComboboxItem("Quý III", "7,8,9"));
            _months.Add(new ComboboxItem("Quý IV", "10,11,12"));

            _monthFrom = _months[0];
            _monthTo = _months.Where(x => x.ValueItem == DateTime.Now.Month.ToString()).FirstOrDefault();
        }

        private void LoadPaperType()
        {
            _paperTypes = new List<ComboboxItem>()
            {
                new ComboboxItem("1. Giấy A4", "1"),
                new ComboboxItem("2. Giấy A3", "2")
            };
            _selectedPaper = _paperTypes.First();
        }

        private void LoadAgencies()
        {
            int monthFrom = int.Parse(MonthFrom.ValueItem.Split(",").First());
            int monthTo = int.Parse(MonthTo.ValueItem.Split(",").Last());
            string monthStr = string.Join(",", Enumerable.Range(monthFrom <= monthTo ? monthFrom : monthTo, (Math.Abs(monthFrom - monthTo) + 1)));

            List<DonVi> listDonVi = _donViService.FindByQuanSo(_sessionInfo.YearOfWork, monthStr).ToList();

            Agencies = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonVi);
            // Filter
            _listAgency = CollectionViewSource.GetDefaultView(Agencies);
            _listAgency.Filter = obj => string.IsNullOrWhiteSpace(_searchAgency)
                                       || (obj is CheckBoxItem item && item.DisplayItem.ToLower().Contains(_searchAgency.ToLower()));

            foreach (var org in Agencies)
            {
                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountAgency));
                    OnPropertyChanged(nameof(SelectAllAgency));
                    OnPropertyChanged(nameof(IsExportEnable));
                };
            }
            OnPropertyChanged(nameof(LabelSelectedCountAgency));
            OnPropertyChanged(nameof(SelectAllAgency));
            OnPropertyChanged(nameof(IsExportEnable));
        }

        private void LoadDanhMuc()
        {
            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void OnExportFile(int exportType)
        {
            try
            {

                if (int.TryParse(MonthFrom.ValueItem, out int monthFrom) && int.TryParse(MonthTo.ValueItem, out int monthTo))
                {
                    if (monthFrom > monthTo)
                    {
                        MessageBox.Show(Resources.MsgErrorPeriodMonth, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    BackgroundWorkerHelper.Run((s, e) =>
                    {
                        IsLoading = true;
                        List<ExportResult> results = new List<ExportResult>();
                        if (ReportType == ReportArmy.AgencyDetail)
                        {
                            foreach (var agency in Agencies.Where(x => x.IsChecked))
                            {
                                var exportResult = ProcessReport(agency.ValueItem, agency.DisplayItem, true);
                                if (exportResult != null)
                                    results.Add(exportResult);
                            }
                        }
                        else
                        {
                            var exportResult = ProcessReport(string.Join(",", Agencies.Where(x => x.IsChecked).Select(x => x.ValueItem).ToArray()), "Tổng hợp", true);
                            if (exportResult != null)
                                results.Add(exportResult);
                        }
                        e.Result = results;
                    }, (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            var result = (List<ExportResult>)e.Result;
                            if (result != null)
                            {
                                _exportService.Open(result, exportType == (int)ExportType.PDF ? ExportType.PDF : ExportType.EXCEL);
                            }
                        }
                        else
                        {
                            _logger.Error(e.Error.Message);
                        }
                        IsLoading = false;
                    });

                }
                else if (int.TryParse(MonthFrom.ValueItem, out int monthFroms) ^ int.TryParse(MonthTo.ValueItem, out int monthTos))
                {
                    MessageBox.Show(Resources.MsgErrorPeriodDiff, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    int monthFromss = int.Parse(MonthFrom.ValueItem.Split(",").First());
                    int monthToss = int.Parse(MonthTo.ValueItem.Split(",").Last());
                    if (monthFromss > monthToss)
                    {
                        MessageBox.Show(Resources.MsgErrorPeriodQuarter, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    BackgroundWorkerHelper.Run((s, e) =>
                    {
                        IsLoading = true;
                        List<ExportResult> results = new List<ExportResult>();
                        if (ReportType == ReportArmy.AgencyDetail)
                        {
                            foreach (var agency in Agencies.Where(x => x.IsChecked))
                            {
                                var exportResult = ProcessReport(agency.ValueItem, agency.DisplayItem, false);
                                if (exportResult != null)
                                    results.Add(exportResult);
                            }
                        }
                        else
                        {
                            var exportResult = ProcessReport(string.Join(",", Agencies.Where(x => x.IsChecked).Select(x => x.ValueItem).ToArray()), "Tổng hợp", false);
                            if (exportResult != null)
                                results.Add(exportResult);
                        }
                        e.Result = results;
                    }, (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            var result = (List<ExportResult>)e.Result;
                            if (result != null)
                            {
                                _exportService.Open(result, exportType == (int)ExportType.PDF ? ExportType.PDF : ExportType.EXCEL);
                            }
                        }
                        else
                        {
                            _logger.Error(e.Error.Message);
                        }
                        IsLoading = false;
                    });
                }


            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                MessageBox.Show(Resources.ErrorExportReport, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private ExportResult ProcessReport(string agencyId, string agencyName, bool isThang)
        {
            try
            {
                List<ReportQuanSoTongHopQuery> listQuanSo = new List<ReportQuanSoTongHopQuery>();
                int monthFrom = int.Parse(MonthFrom.ValueItem.Split(",").First());
                int monthTo = int.Parse(MonthTo.ValueItem.Split(",").Last());
                string monthStr = string.Join(",", Enumerable.Range(monthFrom <= monthTo ? monthFrom : monthTo, (Math.Abs(monthFrom - monthTo) + 1)));
                var listQuanSoQuy = new List<ReportQuanSoTongHopQuery>();
                if (!isThang)
                {
                    int soQuy = monthStr.Split(",").Length / 3;
                    listQuanSo = _chungTuChiTietService.FindForAverage(_sessionInfo.YearOfWork, agencyId, monthStr, ReportType, soQuy);
                    listQuanSo.Where(x => new List<string> { "Tháng 1", "Tháng 2", "Tháng 3" }.Contains(x.MoTa)).Select(x => x.MoTa = "Quý I").ToList();
                    listQuanSo.Where(x => new List<string> { "Tháng 4", "Tháng 5", "Tháng 6" }.Contains(x.MoTa)).Select(x => x.MoTa = "Quý II").ToList();
                    listQuanSo.Where(x => new List<string> { "Tháng 7", "Tháng 8", "Tháng 9" }.Contains(x.MoTa)).Select(x => x.MoTa = "Quý III").ToList();
                    listQuanSo.Where(x => new List<string> { "Tháng 10", "Tháng 11", "Tháng 12" }.Contains(x.MoTa)).Select(x => x.MoTa = "Quý IV").ToList();

                    listQuanSo = listQuanSo.GroupBy(x => x.MoTa).Select(x => new ReportQuanSoTongHopQuery
                    {
                        MoTa = x.Key,
                        RBinhNhat = x.Sum(y => y.RBinhNhat),
                        RBinhNhi = x.Sum(y => y.RBinhNhi),
                        RCcqp = x.Sum(y => y.RCcqp),
                        RCnvqp = x.Sum(y => y.RCnvqp),
                        RDaiTa = x.Sum(y => y.RDaiTa),
                        RDaiUy = x.Sum(y => y.RDaiUy),
                        RHaSi = x.Sum(y => y.RHaSi),
                        RLdhd = x.Sum(y => y.RLdhd),
                        RThuongTaQNCN = x.Sum(y => y.RThuongTaQNCN),
                        RTrungTaQNCN = x.Sum(y => y.RTrungTaQNCN),
                        RThieuTaQNCN = x.Sum(y => y.RThieuTaQNCN),
                        RDaiUyQNCN = x.Sum(y => y.RDaiUyQNCN),
                        RThuongUyQNCN = x.Sum(y => y.RThuongUyQNCN),
                        RTrungUyQNCN = x.Sum(y => y.RTrungUyQNCN),
                        RThieuUyQNCN = x.Sum(y => y.RThieuUyQNCN),
                        RThieuTa = x.Sum(y => y.RThieuTa),
                        RThieuUy = x.Sum(y => y.RThieuUy),
                        RThuongSi = x.Sum(y => y.RThuongSi),
                        RThuongTa = x.Sum(y => y.RThuongTa),
                        RThuongUy = x.Sum(y => y.RThuongUy),
                        RTrungSi = x.Sum(y => y.RTrungSi),
                        RTrungTa = x.Sum(y => y.RTrungTa),
                        RTrungUy = x.Sum(y => y.RTrungUy),
                        RTsq = x.Sum(y => y.RTsq),
                        RTuong = x.Sum(y => y.RTuong),
                        RVcqp = x.Sum(y => y.RVcqp),
                        Id_DonVi = x.Select(y => y.Id_DonVi).First(),
                        TenDonVi = x.Select(y => y.TenDonVi).First(),
                    }).ToList();
                }
                else
                {
                    int soThang = monthStr.Split(",").Length;
                    listQuanSo = _chungTuChiTietService.FindForAverage(_sessionInfo.YearOfWork, agencyId, monthStr, ReportType, soThang);
                }

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_BINHQUAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                ReportQuanSoTongHopQuery sumQuanSo = new ReportQuanSoTongHopQuery()
                {
                    RThieuUy = listQuanSo.Sum(s => s.RFThieuUy),
                    RTrungUy = listQuanSo.Sum(s => s.RFTrungUy),
                    RThuongUy = listQuanSo.Sum(s => s.RFThuongUy),
                    RDaiUy = listQuanSo.Sum(s => s.RFDaiUy),
                    RThieuTa = listQuanSo.Sum(s => s.RFThieuTa),
                    RTrungTa = listQuanSo.Sum(s => s.RFTrungTa),
                    RThuongTa = listQuanSo.Sum(s => s.RFThuongTa),
                    RDaiTa = listQuanSo.Sum(s => s.RFDaiTa),
                    RTuong = listQuanSo.Sum(s => s.RFTuong),
                    RTsq = listQuanSo.Sum(s => s.RFTsq),
                    RBinhNhi = listQuanSo.Sum(s => s.RFBinhNhi),
                    RBinhNhat = listQuanSo.Sum(s => s.RFBinhNhat),
                    RHaSi = listQuanSo.Sum(s => s.RFHaSi),
                    RTrungSi = listQuanSo.Sum(s => s.RFTrungSi),
                    RThuongSi = listQuanSo.Sum(s => s.RFThuongSi),
                    RThuongTaQNCN = listQuanSo.Sum(y => y.RThuongTaQNCN),
                    RTrungTaQNCN = listQuanSo.Sum(y => y.RTrungTaQNCN),
                    RThieuTaQNCN = listQuanSo.Sum(y => y.RThieuTaQNCN),
                    RDaiUyQNCN = listQuanSo.Sum(y => y.RDaiUyQNCN),
                    RThuongUyQNCN = listQuanSo.Sum(y => y.RThuongUyQNCN),
                    RTrungUyQNCN = listQuanSo.Sum(y => y.RTrungUyQNCN),
                    RThieuUyQNCN = listQuanSo.Sum(y => y.RThieuUyQNCN),
                    RVcqp = listQuanSo.Sum(s => s.RFVcqp),
                    RCnvqp = listQuanSo.Sum(s => s.RFCnvqp),
                    RLdhd = listQuanSo.Sum(s => s.RFLdhd),
                };

                RptQuanSoTongHop rptQuanSo = new RptQuanSoTongHop();
                rptQuanSo.SumTotalItems.Add(sumQuanSo);
                rptQuanSo.Items = listQuanSo;
                rptQuanSo.Cap1 = _cap1;
                rptQuanSo.Cap2 = _sessionInfo.TenDonVi;
                rptQuanSo.TieuDe1 = Title1;
                rptQuanSo.TieuDe3 = Title3;
                rptQuanSo.TieuDe2 = Title2;
                string strFrom = string.Empty;
                string strTo = string.Empty;

                var dictTemp = _months.ToDictionary(x => x.ValueItem, x => x.DisplayItem);
                if (dictTemp.ContainsKey(MonthFrom.ValueItem))
                {
                    strFrom = dictTemp[MonthFrom.ValueItem].Replace("Quý", "quý");
                }
                if (dictTemp.ContainsKey(MonthTo.ValueItem))
                {
                    strTo = dictTemp[MonthTo.ValueItem].Replace("Quý", "quý");
                }

                rptQuanSo.TieuDeChung = string.Format("Từ {0} đến {1} năm {2}", strFrom, strTo, _sessionInfo.YearOfWork);
                rptQuanSo.TenDonVi = agencyName;
                rptQuanSo.Ngay = DateUtils.FormatDateReport(ReportDate);
                rptQuanSo.DiaDiem = _diaDiem;
                rptQuanSo.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                rptQuanSo.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                rptQuanSo.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                rptQuanSo.ChucDanh4 = _dmChuKy != null ? _dmChuKy.ChucDanh4MoTa : string.Empty;
                rptQuanSo.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                rptQuanSo.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                rptQuanSo.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                rptQuanSo.ThuaLenh4 = _dmChuKy != null ? _dmChuKy.ThuaLenh4MoTa : string.Empty;
                rptQuanSo.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                rptQuanSo.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                rptQuanSo.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;
                rptQuanSo.Ten4 = _dmChuKy != null ? _dmChuKy.Ten4MoTa : string.Empty;

                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var prop in rptQuanSo.GetType().GetProperties())
                {
                    data.Add(prop.Name, prop.GetValue(rptQuanSo));
                }

                string templateFileName = getPath();
                string fileNamePrefix = string.Format("{0}_{1}", SelectedPaper.ValueItem.Split(".").First(), agencyName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQuanSoTongHopQuery>(templateFileName, data);
                return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private string getPath()
        {
            string path;
            if (SelectedPaper.ValueItem.Equals("1"))
            {
                path = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUANSO_BINHQUAN_A4);
                if (ReportType == ReportArmy.Summary)
                {
                    path = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUANSO_BINHQUAN_TONGHOP_A4);
                }
            }
            else
            {
                path = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUANSO_BINHQUAN_A3);
                if (ReportType == ReportArmy.Summary)
                {
                    path = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUANSO_BINHQUAN_TONGHOP_A3);
                }
            }
            return path;
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_BINHQUAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_QUANSO_BINHQUAN;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.HasAddedSign4 = true;
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
