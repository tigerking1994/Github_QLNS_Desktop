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
    public class PrintArmyUpDownViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly INsQsChungTuChiTietService _chungTuChiTietService;
        private readonly IExportService _exportService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private List<ComboboxItem> _months;
        private List<ComboboxItem> _quarters;
        private List<ComboboxItem> _years;
        private ICollectionView _listAgency;
        private DmChuKy _dmChuKy;
        private string _diaDiem;
        private string _cap2;

        private string _title;
        public override string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public override string Description => "Chọn thông số để in chi tiết đơn vị hoặc tổng hợp theo tháng, quý, năm";
        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintArmyUpDown);

        private List<ComboboxItem> _periods;
        public List<ComboboxItem> Periods
        {
            get => _periods;
            set => SetProperty(ref _periods, value);
        }

        private QuarterMonth _periodType;
        public QuarterMonth PeriodType
        {
            get => _periodType;
            set
            {
                SetProperty(ref _periodType, value);
                LoadPeriod();
            }
        }

        private string _periodHint;
        public string PeriodHint
        {
            get => _periodHint;
            set => SetProperty(ref _periodHint, value);
        }

        private ComboboxItem _selectedPeriod;
        public ComboboxItem SelectedPeriod
        {
            get => _selectedPeriod;
            set
            {
                SetProperty(ref _selectedPeriod, value);
                if (_selectedPeriod != null)
                    LoadAgencies();
            }
        }

        private ReportArmy _reportType;
        public ReportArmy ReportType
        {
            get => _reportType;
            set
            {
                SetProperty(ref _reportType, value);
                LoadAgencies();
                LoadTieuDe();
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

        public PrintArmyUpDownViewModel(ISessionService sessionService,
            INsDonViService donViService,
            ILog logger,
            IMapper mapper,
            INsQsChungTuChiTietService chungTuChiTietService,
            IDmChuKyService dmChuKyService,
            IDanhMucService danhMucService,
            IExportService exportService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _logger = logger;
            _mapper = mapper;
            _chungTuChiTietService = chungTuChiTietService;
            _exportService = exportService;
            _dmChuKyService = dmChuKyService;
            _danhMucService = danhMucService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

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
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            _title = string.Format("In báo cáo tăng giảm quân số năm {0}", _sessionInfo.YearOfWork);
            _periodType = QuarterMonth.MONTH;
            _reportType = ReportArmy.AgencyDetail;
            LoadTieuDe();
            LoadMonths();
            LoadQuarters();
            LoadYears();
            LoadPeriod();
            LoadPaperType();
            LoadDanhMuc();
        }

        private void LoadTieuDe()
        {
            if (ReportType == ReportArmy.SummaryAgency)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_TANGGIAM_TONGHOP_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            }
            else
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_TANGGIAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            }
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap2 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
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
        }

        /// <summary>
        /// Tạo dữ liệu combobox quý
        /// </summary>
        private void LoadQuarters()
        {
            _quarters = new List<ComboboxItem>();
            _quarters.Add(new ComboboxItem("Quý I", "1,2,3"));
            _quarters.Add(new ComboboxItem("Quý II", "4,5,6"));
            _quarters.Add(new ComboboxItem("Quý III", "7,8,9"));
            _quarters.Add(new ComboboxItem("Quý IV", "10,11,12"));
        }

        /// <summary>
        /// Tạo dữ liệu combobox năm
        /// </summary>
        private void LoadYears()
        {
            _years = new List<ComboboxItem>();
            _years.Add(new ComboboxItem("Năm " + _sessionInfo.YearOfWork, "0,1,2,3,4,5,6,7,8,9,10,11,12"));
        }

        private void LoadPeriod()
        {
            if (PeriodType == QuarterMonth.MONTH)
            {
                Periods = _months;
                PeriodHint = "Chọn tháng";
            }
            else if (PeriodType == QuarterMonth.QUARTER)
            {
                Periods = _quarters;
                PeriodHint = "Chọn quý";
            }
            else
            {
                Periods = _years;
                PeriodHint = "Chọn năm";
            }
            SelectedPeriod = Periods.Where(x => x.ValueItem.Contains(DateTime.Now.Month.ToString())).FirstOrDefault();
        }

        private void LoadPaperType()
        {
            _paperTypes = new List<ComboboxItem>()
            {
                new ComboboxItem("Mặc định", "")
            };
            _selectedPaper = _paperTypes.First();
        }

        private void LoadAgencies()
        {
            //string months = "";

            //if (PeriodType == QuarterMonth.QUARTER)
            //{
            //    months = (SelectedPeriod.ValueItem) switch
            //    {
            //        "1,2,3" => "0",
            //        "4,5,6" => "3",
            //        "7,8,9" => "6",
            //        "10,11,12" => "9",
            //        _ => ""
            //    };
            //}
            //else if (PeriodType == QuarterMonth.YEAR)
            //{
            //    months = "1,12";
            //}
            //else
            //{
            //    months = SelectedPeriod.ValueItem;
            //}

            List<DonVi> listDonVi = _donViService.FindByQuanSo(_sessionInfo.YearOfWork, SelectedPeriod.ValueItem).ToList();

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
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void OnExportFile(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string agencyId = string.Empty;
                    string agencyName = string.Empty;
                    if (ReportType == ReportArmy.AgencyDetail)
                    {
                        foreach (var agency in Agencies.Where(x => x.IsChecked))
                        {
                            var exportResult = ProcessExport(agency.ValueItem, agency.DisplayItem.Split("-").Last());
                            if (exportResult != null)
                                results.Add(exportResult);
                        }
                    }
                    else if (ReportType == ReportArmy.Summary)
                    {
                        agencyId = string.Join(",", Agencies.Where(x => x.IsChecked).Select(x => x.ValueItem).ToArray());
                        var exportResult = ProcessExport(agencyId, "Tổng hợp");
                        if (exportResult != null)
                            results.Add(exportResult);
                    }
                    else
                    {
                        agencyId = string.Join(",", Agencies.Where(x => x.IsChecked).Select(x => x.ValueItem).ToArray());
                        var exportResult = ProcessExportDetail(agencyId, "Tổng hợp");
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
                            _exportService.Open(result, exportType);
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch
            {
                MessageBox.Show(Resources.ErrorExportReport, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public string NgayTieuDe(QuarterMonth periodType) => periodType switch
        {
            QuarterMonth.YEAR => $"Năm {_sessionInfo.YearOfWork}",
            QuarterMonth.QUARTER => $"Quý {RomanNumber.ToRoman(int.Parse(SelectedPeriod.ValueItem.Split(",").LastOrDefault()) / 3)} năm {_sessionInfo.YearOfWork}",
            _ => $"Tháng {(SelectedPeriod.ValueItem == "0" ? "đầu" : SelectedPeriod.ValueItem)} năm {_sessionInfo.YearOfWork}",
        };



        private ExportResult ProcessExportDetail(string agencyId, string agencyName)
        {
            try
            {
                List<ReportQuanSoDonViQuery> listQuanSoTruoc;
                List<ReportQuanSoDonViQuery> listQuanSoSau;
                if (PeriodType == QuarterMonth.QUARTER)
                {
                    var (monthStrPrev, monthStrNext) = (SelectedPeriod.ValueItem) switch
                    {
                        "1,2,3" => ("0", "3"),
                        "4,5,6" => ("3", "6"),
                        "7,8,9" => ("6", "9"),
                        "10,11,12" => ("9", "12"),
                        _ => ("", "")
                    };
                    listQuanSoTruoc = _chungTuChiTietService.FindForAgencyDetailReport(_sessionInfo.YearOfWork, agencyId, monthStrPrev).Where(x => x.XauNoiMa == "700").ToList();
                    listQuanSoSau = _chungTuChiTietService.FindForAgencyDetailReport(_sessionInfo.YearOfWork, agencyId, monthStrNext).Where(x => x.XauNoiMa == "700").ToList();
                    listQuanSoTruoc.Select(n => n.XauNoiMa = "100").ToList();
                }
                else if (PeriodType == QuarterMonth.YEAR)
                {
                    listQuanSoTruoc = _chungTuChiTietService.FindForAgencyDetailReport(_sessionInfo.YearOfWork, agencyId, "1").Where(x => x.XauNoiMa == "100").ToList();
                    listQuanSoSau = _chungTuChiTietService.FindForAgencyDetailReport(_sessionInfo.YearOfWork, agencyId, "12").Where(x => x.XauNoiMa == "700").ToList();
                }
                else
                {
                    var listQuanSoTemp = _chungTuChiTietService.FindForAgencyDetailReport(_sessionInfo.YearOfWork, agencyId, SelectedPeriod.ValueItem).ToList();
                    listQuanSoTruoc = listQuanSoTemp.Where(x => x.XauNoiMa == "100").ToList();
                    listQuanSoSau = listQuanSoTemp.Where(x => x.XauNoiMa == "700").ToList();
                }

                var listQuanSo = listQuanSoTruoc.ToList();
                listQuanSo.AddRange(listQuanSoSau);

                foreach (var item in Agencies.Where(x => x.IsChecked))
                {
                    if (!listQuanSo.Any(x => x.XauNoiMa == "100" && x.IdMaDonVi == item.ValueItem))
                    {
                        listQuanSo.Add(new ReportQuanSoDonViQuery()
                        {
                            XauNoiMa = "100",
                            IdMaDonVi = item.ValueItem,
                            TenDonVi = item.DisplayItem,
                            BHangCha = true
                        });
                    }
                    if (!listQuanSo.Any(x => x.XauNoiMa == "700" && x.IdMaDonVi == item.ValueItem))
                    {
                        listQuanSo.Add(new ReportQuanSoDonViQuery()
                        {
                            XauNoiMa = "700",
                            IdMaDonVi = item.ValueItem,
                            TenDonVi = item.DisplayItem,
                            BHangCha = true
                        });
                    }
                }

                listQuanSo = listQuanSo.OrderBy(x => x.IdMaDonVi).ThenBy(x => x.XauNoiMa).ToList();

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_TANGGIAM_TONGHOP_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                RptQuanSoDonVi rptQuanSo = new RptQuanSoDonVi();
                rptQuanSo.TangGiam = listQuanSo.Where(x => (x.XauNoiMa == "100" || x.XauNoiMa == "700") && x.BHangCha && !string.IsNullOrEmpty(x.IdMaDonVi)).OrderBy(x => x.IdMaDonVi).ThenBy(x => x.XauNoiMa).ToList();
                rptQuanSo.TangGiam = rptQuanSo.TangGiam.Select(x =>
                {
                    if (x.XauNoiMa == "100")
                    {
                        x.MergeRange = "A7:A8";
                        if (PeriodType == QuarterMonth.MONTH)
                            x.MoTa = "QT tháng trước";
                        else if (PeriodType == QuarterMonth.QUARTER)
                            x.MoTa = "QT quý trước";
                        else x.MoTa = "Quân số 01/01";
                    }
                    else
                    {
                        x.MergeRange = "A7:A7";
                        if (PeriodType == QuarterMonth.MONTH)
                            x.MoTa = "QT tháng này";
                        else if (PeriodType == QuarterMonth.QUARTER)
                            x.MoTa = "QT quý này";
                        else x.MoTa = "Quân số 31/12";
                    }
                    return x;
                }).ToList();
                rptQuanSo.Cap2 = _cap2;
                rptQuanSo.Cap3 = _sessionInfo.TenDonVi;
                rptQuanSo.TieuDe1 = Title1;
                rptQuanSo.TieuDe3 = Title3;
                //rptQuanSo.TieuDe2 = PeriodType != QuarterMonth.YEAR ? SelectedPeriod.DisplayItem + " năm " + _sessionInfo.YearOfWork : SelectedPeriod.DisplayItem;
                rptQuanSo.TieuDe2 = Title2;
                rptQuanSo.TenDonVi = string.Format("Đơn vị: {0}", agencyName);
                rptQuanSo.Ngay = DateUtils.FormatDateReport(ReportDate);
                rptQuanSo.NgayTieuDe = NgayTieuDe(PeriodType);
                rptQuanSo.DiaDiem = _diaDiem;
                if (PeriodType == QuarterMonth.MONTH)
                    rptQuanSo.ThangQuy = "Tháng";
                else if (PeriodType == QuarterMonth.QUARTER)
                    rptQuanSo.ThangQuy = "Quý";
                else rptQuanSo.ThangQuy = "Năm";
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

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUANSO_TANGGIAM_TONGHOP_DONVI);
                string filePath = string.Format(ExportFileName.RPT_NS_QUANSO_TANGGIAM_TONGHOP_DONVI, SelectedPaper.ValueItem);
                string fileNamePrefix = string.Format("{0}_{1}", filePath.Split(".").First(), agencyName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQuanSoDonViQuery>(templateFileName, data);
                return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private ExportResult ProcessExport(string agencyId, string agencyName)
        {
            try
            {
                var listQuanSo = _chungTuChiTietService.FindForAgencyDetailReport(_sessionInfo.YearOfWork, agencyId, SelectedPeriod.ValueItem);
                listQuanSo = listQuanSo.GroupBy(x => x.XauNoiMa).Select(x => new ReportQuanSoDonViQuery()
                {
                    XauNoiMa = x.First().XauNoiMa,
                    MoTa = x.First().MoTa,
                    BHangCha = x.First().BHangCha,
                    RThieuUy = x.Sum(y => y.RThieuUy),
                    RTrungUy = x.Sum(y => y.RTrungUy),
                    RThuongUy = x.Sum(y => y.RThuongUy),
                    RDaiUy = x.Sum(y => y.RDaiUy),
                    RThieuTa = x.Sum(y => y.RThieuTa),
                    RTrungTa = x.Sum(y => y.RTrungTa),
                    RThuongTa = x.Sum(y => y.RThuongTa),
                    RDaiTa = x.Sum(y => y.RDaiTa),
                    RTuong = x.Sum(y => y.RTuong),
                    RTsq = x.Sum(y => y.RTsq),
                    RBinhNhi = x.Sum(y => y.RBinhNhi),
                    RBinhNhat = x.Sum(y => y.RBinhNhat),
                    RHaSi = x.Sum(y => y.RHaSi),
                    RTrungSi = x.Sum(y => y.RTrungSi),
                    RThuongSi = x.Sum(y => y.RThuongSi),
                    RThuongTaQNCN = x.Sum(y => y.RThuongTaQNCN),
                    RTrungTaQNCN = x.Sum(y => y.RTrungTaQNCN),
                    RThieuTaQNCN = x.Sum(y => y.RThieuTaQNCN),
                    RDaiUyQNCN = x.Sum(y => y.RDaiUyQNCN),
                    RThuongUyQNCN = x.Sum(y => y.RThuongUyQNCN),
                    RTrungUyQNCN = x.Sum(y => y.RTrungUyQNCN),
                    RThieuUyQNCN = x.Sum(y => y.RThieuUyQNCN),
                    RVcqp = x.Sum(y => y.RVcqp),
                    RCnvqp = x.Sum(y => y.RCnvqp),
                    RLdhd = x.Sum(y => y.RLdhd),
                    RCcqp = x.Sum(y => y.RCcqp),
                    IdMaDonVi = x.First().IdMaDonVi,
                    TenDonVi = x.First().TenDonVi
                }).ToList();
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_TANGGIAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                RptQuanSoDonVi rptQuanSo = new RptQuanSoDonVi();
                rptQuanSo.Tang = listQuanSo.Where(x => x.XauNoiMa.StartsWith("2") && !x.BHangCha).ToList();
                rptQuanSo.Giam = listQuanSo.Where(x => x.XauNoiMa.StartsWith("3") && !x.BHangCha).ToList();

                rptQuanSo.Cap2 = _cap2;
                rptQuanSo.Cap3 = _sessionInfo.TenDonVi;
                rptQuanSo.TieuDe1 = Title1;
                rptQuanSo.TieuDe3 = Title3;
                //rptQuanSo.TieuDe2 = PeriodType != QuarterMonth.YEAR ? SelectedPeriod.DisplayItem + " năm " + _sessionInfo.YearOfWork : SelectedPeriod.DisplayItem;
                rptQuanSo.TieuDe2 = Title2;
                rptQuanSo.TenDonVi = string.Format("Đơn vị: {0}", agencyName);
                rptQuanSo.Ngay = DateUtils.FormatDateReport(ReportDate);
                rptQuanSo.NgayTieuDe = NgayTieuDe(PeriodType);
                rptQuanSo.DiaDiem = _diaDiem;
                if (PeriodType == QuarterMonth.MONTH)
                    rptQuanSo.ThangQuy = "Tháng";
                else if (PeriodType == QuarterMonth.QUARTER)
                    rptQuanSo.ThangQuy = "Quý";
                else rptQuanSo.ThangQuy = "Năm";
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

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUANSO_TANGGIAM);
                string filePath = string.Format(ExportFileName.RPT_NS_QUANSO_TANGGIAM, SelectedPaper.ValueItem);
                string fileNamePrefix = string.Format("{0}_{1}", filePath.Split(".").First(), agencyName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQuanSoDonViQuery>(templateFileName, data);
                return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            if (ReportType == ReportArmy.SummaryAgency)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_TANGGIAM_TONGHOP_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                if (_dmChuKy == null)
                    chuKyModel.IdType = TypeChuKy.RPT_NS_QUANSO_TANGGIAM_TONGHOP_DONVI;
                else
                    chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
                DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
                DmChuKyDialogViewModel.HasAddedSign4 = true;
                DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
                DmChuKyDialogViewModel.Init();
                DmChuKyDialogViewModel.ShowDialog();
            }
            else
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_TANGGIAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                if (_dmChuKy == null)
                    chuKyModel.IdType = TypeChuKy.RPT_NS_QUANSO_TANGGIAM;
                else
                    chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
                DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
                DmChuKyDialogViewModel.HasAddedSign4 = true;
                DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
                DmChuKyDialogViewModel.Init();
                DmChuKyDialogViewModel.ShowDialog();
            }


        }
    }
}
