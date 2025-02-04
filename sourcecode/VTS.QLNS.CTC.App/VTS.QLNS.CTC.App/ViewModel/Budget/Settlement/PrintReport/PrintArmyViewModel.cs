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
    public class PrintArmyViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IMapper _mapper;
        private readonly INsQsChungTuChiTietService _chungTuChiTietService;
        private readonly INsQsChungTuService _chungTuService;
        private readonly IExportService _exportService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private List<ComboboxItem> _months;
        private List<ComboboxItem> _quarters;
        private List<ComboboxItem> _years;
        private ICollectionView _listAgency;
        private DmChuKy _dmChuKy;
        private string _diaDiem;
        private string _title;
        private string _cap2;

        public override string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public override string Description => "Chọn thông số để in chi tiết đơn vị hoặc tổng hợp theo tháng, quý, năm";
        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintArmy);

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
                var totalSelected = Agencies.Count(item => item.Selected);
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
            get => Agencies.All(item => item.Selected);
            set
            {
                SetProperty(ref _selectAllAgency, value);
                foreach (var item in Agencies) item.Selected = _selectAllAgency;
            }
        }

        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        public bool IsExportEnable
        {
            get
            {
                if (_agencies != null)
                    return _agencies.Where(x => x.Selected).Count() > 0;
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

        public PrintArmyViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            INsQsChungTuChiTietService chungTuChiTietService,
            INsQsChungTuService chungTuService,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            IDanhMucService danhMucService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            ILog logger)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _chungTuChiTietService = chungTuChiTietService;
            _chungTuService = chungTuService;
            _exportService = exportService;
            _dmChuKyService = dmChuKyService;
            _logger = logger;
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
            _title = string.Format("In báo cáo quân số năm {0}", _sessionInfo.YearOfWork);
            _periodType = QuarterMonth.MONTH;
            _reportType = ReportArmy.Army;
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
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            Title1 = _dmChuKy.TieuDe1MoTa;
            Title2 = _dmChuKy.TieuDe2MoTa;
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
            _quarters.Add(new ComboboxItem("Quý I", "3"));
            _quarters.Add(new ComboboxItem("Quý II", "6"));
            _quarters.Add(new ComboboxItem("Quý III", "9"));
            _quarters.Add(new ComboboxItem("Quý IV", "12"));
        }

        /// <summary>
        /// Tạo dữ liệu combobox năm
        /// </summary>
        private void LoadYears()
        {
            _years = new List<ComboboxItem>();
            _years.Add(new ComboboxItem("Năm " + _sessionInfo.YearOfWork, string.Join(",", Enumerable.Range(1, 12).ToArray())));
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
            SelectedPeriod = Periods.FirstOrDefault();
        }

        private void LoadPaperType()
        {
            _paperTypes = new List<ComboboxItem>()
            {
                new ComboboxItem("1. Tách CC & VCQP", ""),
                new ComboboxItem("2. Gộp CC & VCQP", "_2")
            };
            _selectedPaper = _paperTypes.First();
        }

        private void LoadAgencies()
        {
            string month = string.Empty;
            if (PeriodType == QuarterMonth.MONTH)
                month = SelectedPeriod.ValueItem;
            else if (PeriodType == QuarterMonth.QUARTER)
                month = string.Join(",", Enumerable.Range(Convert.ToInt32(SelectedPeriod.ValueItem) - 2, 3).ToList());
            else
                month = string.Join(",", Enumerable.Range(1, 12).ToList());
            List<DonVi> listDonViHeThong = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
            List<DonVi> listDonViQuanSo = _donViService.FindByQuanSo(_sessionInfo.YearOfWork, month).Where(x => x.Loai != LoaiDonVi.ROOT).ToList();

            if (ReportType == ReportArmy.Army)
                listDonViQuanSo.AddRange(listDonViHeThong.Where(x => x.Loai == LoaiDonVi.ROOT));
            Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonViQuanSo.OrderBy(x => x.IIDMaDonVi));

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
                    if (ReportType == ReportArmy.Army)
                    {
                        foreach (var agency in Agencies.Where(x => x.Selected))
                        {
                            ExportResult exportResult = ProcessExportAgency(agency);
                            if (exportResult != null)
                                results.Add(exportResult);
                        }
                    }
                    else if (ReportType == ReportArmy.AgencyDetail)
                    {
                        var exportResult = ProcessExportDetail();
                        if (exportResult != null)
                            results.Add(exportResult);
                    }
                    else
                    {
                        var exportResult = ProcessExportSummary();
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
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                MessageBox.Show(Resources.ErrorExportReport, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private ExportResult ProcessExportAgency(AgencyModel agency)
        {
            try
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                string idDonVi = string.Empty;
                string tenDonVi = string.Empty;

                var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
                predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == _sessionInfo.YearOfWork && x.ITrangThai == StatusType.ACTIVE && x.Loai == LoaiDonVi.NOI_BO);
                bool isDvCap4 = _donViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;

                if (agency.Loai == LoaiDonVi.ROOT && !isDvCap4)
                    idDonVi = string.Join(",", Agencies.Where(x => x.Loai != LoaiDonVi.ROOT).Select(x => x.Id));
                else
                {
                    idDonVi = agency.Id;
                    tenDonVi = agency.AgencyName;
                }
                int month = 0;
                string monthStr = string.Empty;
                string monthStrPrev = string.Empty;
                List<int> months = new List<int>();
                var listQuanSo = new List<ReportQuanSoDonViQuery>();
                var listQuanSoThangQuyTruoc = new List<ReportQuanSoDonViQuery>();


                if (PeriodType == QuarterMonth.MONTH)
                {
                    month = Convert.ToInt32(SelectedPeriod.ValueItem);
                    monthStr = SelectedPeriod.ValueItem;
                }
                else if (PeriodType == QuarterMonth.QUARTER)
                {
                    var listQuanSoNamTruoc = _chungTuChiTietService.FindForAgencyReport(_sessionInfo.YearOfWork - 1, idDonVi, "12");
                    if (Convert.ToInt32(SelectedPeriod.ValueItem) == 3)
                    {
                        monthStrPrev = "12";
                        if (listQuanSoNamTruoc.Where(t => t.HasData).Count() == 0)
                        { 
                            listQuanSoNamTruoc =  _chungTuChiTietService.FindForAgencyReport(_sessionInfo.YearOfWork, idDonVi, "0");
                            monthStrPrev = "0";
                        }
                        listQuanSoThangQuyTruoc = listQuanSoNamTruoc.Where(x => x.XauNoiMa == "700").ToList();
                    }
                    else
                    {
                        monthStrPrev = (Convert.ToInt32(SelectedPeriod.ValueItem)) switch
                        {
                            6 => "3",
                            9 => "6",
                            12 => "9",
                            _ => ""
                        };
                        listQuanSoThangQuyTruoc = _chungTuChiTietService.FindForAgencyReport(_sessionInfo.YearOfWork, idDonVi, monthStrPrev).Where(x => x.XauNoiMa == "700").ToList();
                    }

                    months = Enumerable.Range(Convert.ToInt32(SelectedPeriod.ValueItem) - 2, 3).ToList();
                    monthStr = string.Join(",", months);
                    month = months.First();
                }
                else if (PeriodType == QuarterMonth.YEAR)
                {
                    months = _chungTuService.FindMonthOfArmy(_sessionInfo.YearOfWork).Where(x => x != 0).ToList();
                    monthStr = string.Join(",", months);
                    month = months.First();
                }

                listQuanSo = _chungTuChiTietService.FindForAgencyReport(_sessionInfo.YearOfWork, idDonVi, monthStr);
                RptQuanSoDonVi rptQuanSo = new RptQuanSoDonVi();
                if (PeriodType == QuarterMonth.QUARTER)
                {
                    rptQuanSo.Truoc = listQuanSoThangQuyTruoc;
                }
                else if (PeriodType == QuarterMonth.YEAR)
                {
                    rptQuanSo.Truoc = _chungTuChiTietService.FindForAgencyReport(_sessionInfo.YearOfWork, idDonVi, "1").Where(x => x.XauNoiMa.StartsWith("1")).ToList();
                }
                else
                {
                    rptQuanSo.Truoc = listQuanSo.Where(x => x.XauNoiMa.StartsWith("1")).ToList();
                }
                rptQuanSo.Tang = listQuanSo.Where(x => x.XauNoiMa.StartsWith("2") && !x.BHangCha).ToList();
                rptQuanSo.Giam = listQuanSo.Where(x => x.XauNoiMa.StartsWith("3") && !x.BHangCha).ToList();
                rptQuanSo.BS1 = listQuanSo.Where(x => x.XauNoiMa == "500").ToList();
                rptQuanSo.BS2 = listQuanSo.Where(x => x.XauNoiMa == "600").ToList();
                rptQuanSo.Thang = new List<ReportQuanSoDonViQuery>();
                if (PeriodType == QuarterMonth.QUARTER || PeriodType == QuarterMonth.YEAR)
                {
                    foreach (var item in months)
                    {
                        List<ReportQuanSoDonViQuery> listQuanSoThang = _chungTuChiTietService.FindForAgencyReport(_sessionInfo.YearOfWork, idDonVi, item.ToString());
                        ReportQuanSoDonViQuery row = listQuanSoThang.Where(x => x.XauNoiMa == "700").FirstOrDefault();
                        if (row == null)
                            row = new ReportQuanSoDonViQuery();
                        row.Thang = item;
                        rptQuanSo.Thang.Add(row);
                    }
                    if (PeriodType == QuarterMonth.YEAR)
                    {
                        int count = months.Count;
                        ReportQuanSoDonViQuery nam = new ReportQuanSoDonViQuery();
                        nam.RThieuUy = rptQuanSo.Thang.Sum(x => x.RThieuUy) > 12 ? rptQuanSo.Thang.Sum(x => x.RThieuUy) / 12 : rptQuanSo.Thang.Sum(x => x.RThieuUy) / count;
                        nam.RTrungUy = rptQuanSo.Thang.Sum(x => x.RTrungUy) > 12 ? rptQuanSo.Thang.Sum(x => x.RTrungUy) / 12 : rptQuanSo.Thang.Sum(x => x.RTrungUy) / count;
                        nam.RThuongUy = rptQuanSo.Thang.Sum(x => x.RThuongUy) > 12 ? rptQuanSo.Thang.Sum(x => x.RThuongUy) / 12 : rptQuanSo.Thang.Sum(x => x.RThuongUy) / count;
                        nam.RDaiUy = rptQuanSo.Thang.Sum(x => x.RDaiUy) > 12 ? rptQuanSo.Thang.Sum(x => x.RDaiUy) / 12 : rptQuanSo.Thang.Sum(x => x.RDaiUy) / count;
                        nam.RThieuTa = rptQuanSo.Thang.Sum(x => x.RThieuTa) > 12 ? rptQuanSo.Thang.Sum(x => x.RThieuTa) / 12 : rptQuanSo.Thang.Sum(x => x.RThieuTa) / count;
                        nam.RThuongTa = rptQuanSo.Thang.Sum(x => x.RThuongTa) > 12 ? rptQuanSo.Thang.Sum(x => x.RThuongTa) / 12 : rptQuanSo.Thang.Sum(x => x.RThuongTa) / count;
                        nam.RTrungTa = rptQuanSo.Thang.Sum(x => x.RTrungTa) > 12 ? rptQuanSo.Thang.Sum(x => x.RTrungTa) / 12 : rptQuanSo.Thang.Sum(x => x.RTrungTa) / count;
                        nam.RDaiTa = rptQuanSo.Thang.Sum(x => x.RDaiTa) > 12 ? rptQuanSo.Thang.Sum(x => x.RDaiTa) / 12 : rptQuanSo.Thang.Sum(x => x.RDaiTa) / count;
                        nam.RTuong = rptQuanSo.Thang.Sum(x => x.RTuong) > 12 ? rptQuanSo.Thang.Sum(x => x.RTuong) / 12 : rptQuanSo.Thang.Sum(x => x.RTuong) / count;
                        nam.RBinhNhi = rptQuanSo.Thang.Sum(x => x.RBinhNhi) > 12 ? rptQuanSo.Thang.Sum(x => x.RBinhNhi) / 12 : rptQuanSo.Thang.Sum(x => x.RBinhNhi) / count;
                        nam.RBinhNhat = rptQuanSo.Thang.Sum(x => x.RBinhNhat) > 12 ? rptQuanSo.Thang.Sum(x => x.RBinhNhat) / 12 : rptQuanSo.Thang.Sum(x => x.RBinhNhat) / count;
                        nam.RHaSi = rptQuanSo.Thang.Sum(x => x.RHaSi) > 12 ? rptQuanSo.Thang.Sum(x => x.RHaSi) / 12 : rptQuanSo.Thang.Sum(x => x.RHaSi) / count;
                        nam.RTrungSi = rptQuanSo.Thang.Sum(x => x.RTrungSi) > 12 ? rptQuanSo.Thang.Sum(x => x.RTrungSi) / 12 : rptQuanSo.Thang.Sum(x => x.RTrungSi) / count;
                        nam.RThuongSi = rptQuanSo.Thang.Sum(x => x.RThuongSi) > 12 ? rptQuanSo.Thang.Sum(x => x.RThuongSi) / 12 : rptQuanSo.Thang.Sum(x => x.RThuongSi) / count;
                        nam.RThuongTaQNCN = rptQuanSo.Thang.Sum(x => x.RThuongTaQNCN) > 12 ? rptQuanSo.Thang.Sum(x => x.RThuongTaQNCN) / 12 : rptQuanSo.Thang.Sum(x => x.RThuongTaQNCN) / count;
                        nam.RTrungTaQNCN = rptQuanSo.Thang.Sum(x => x.RTrungTaQNCN) > 12 ? rptQuanSo.Thang.Sum(x => x.RTrungTaQNCN) / 12 : rptQuanSo.Thang.Sum(x => x.RTrungTaQNCN) / count;
                        nam.RThieuTaQNCN = rptQuanSo.Thang.Sum(x => x.RThieuTaQNCN) > 12 ? rptQuanSo.Thang.Sum(x => x.RThieuTaQNCN) / 12 : rptQuanSo.Thang.Sum(x => x.RThieuTaQNCN) / count;
                        nam.RDaiUyQNCN = rptQuanSo.Thang.Sum(x => x.RDaiUyQNCN) > 12 ? rptQuanSo.Thang.Sum(x => x.RDaiUyQNCN) / 12 : rptQuanSo.Thang.Sum(x => x.RDaiUyQNCN) / count;
                        nam.RThuongUyQNCN = rptQuanSo.Thang.Sum(x => x.RThuongUyQNCN) > 12 ? rptQuanSo.Thang.Sum(x => x.RThuongUyQNCN) / 12 : rptQuanSo.Thang.Sum(x => x.RThuongUyQNCN) / count;
                        nam.RTrungUyQNCN = rptQuanSo.Thang.Sum(x => x.RTrungUyQNCN) > 12 ? rptQuanSo.Thang.Sum(x => x.RTrungUyQNCN) / 12 : rptQuanSo.Thang.Sum(x => x.RTrungUyQNCN) / count;
                        nam.RThieuUyQNCN = rptQuanSo.Thang.Sum(x => x.RThieuUyQNCN) > 12 ? rptQuanSo.Thang.Sum(x => x.RThieuUyQNCN) / 12 : rptQuanSo.Thang.Sum(x => x.RThieuUyQNCN) / count;
                        nam.RVcqp = rptQuanSo.Thang.Sum(x => x.RVcqp) > 12 ? rptQuanSo.Thang.Sum(x => x.RVcqp) / 12 : rptQuanSo.Thang.Sum(x => x.RVcqp) / count;
                        nam.RCnvqp = rptQuanSo.Thang.Sum(x => x.RCnvqp) > 12 ? rptQuanSo.Thang.Sum(x => x.RCnvqp) / 12 : rptQuanSo.Thang.Sum(x => x.RCnvqp) / count;
                        nam.RLdhd = rptQuanSo.Thang.Sum(x => x.RLdhd) > 12 ? rptQuanSo.Thang.Sum(x => x.RLdhd) / 12 : rptQuanSo.Thang.Sum(x => x.RLdhd) / count;
                        nam.RCcqp = rptQuanSo.Thang.Sum(x => x.RCcqp) > 12 ? rptQuanSo.Thang.Sum(x => x.RCcqp) / 12 : rptQuanSo.Thang.Sum(x => x.RCcqp) / count;
                        rptQuanSo.Nam = new List<ReportQuanSoDonViQuery> { nam };
                    }
                }

                rptQuanSo.Cap2 = _cap2;
                rptQuanSo.Cap3 = _sessionInfo.TenDonVi;
                rptQuanSo.TieuDeChung = PeriodType != QuarterMonth.YEAR ? string.Format("{0} năm {1}", SelectedPeriod.DisplayItem, _sessionInfo.YearOfWork) : SelectedPeriod.DisplayItem;
                rptQuanSo.TenDonVi = tenDonVi;
                rptQuanSo.Ngay = DateUtils.FormatDateReport(ReportDate);
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
                rptQuanSo.TieuDe1 = Title1;
                rptQuanSo.TieuDe2 = Title2;
                rptQuanSo.TieuDe3 = Title3;
                //rptQuanSo.TieuDe1 = _dmChuKy != null ? _dmChuKy.TieuDe1 : string.Empty;
                //rptQuanSo.TieuDe2 = _dmChuKy != null ? _dmChuKy.TieuDe2 : string.Empty;

                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var prop in rptQuanSo.GetType().GetProperties())
                {
                    data.Add(prop.Name, prop.GetValue(rptQuanSo));
                }

                string fileName = string.Empty;
                if (PeriodType == QuarterMonth.MONTH)
                {
                    switch (SelectedPaper.ValueItem)
                    {
                        case "":
                            fileName = ExportFileName.RPT_NS_QUANSO_DONVI;
                            break;
                        case "_2":
                            fileName = ExportFileName.RPT_NS_QUANSO_DONVI_2;
                            break;
                    }
                }
                else if (PeriodType == QuarterMonth.QUARTER)
                {
                    switch (SelectedPaper.ValueItem)
                    {
                        case "":
                            fileName = ExportFileName.RPT_NS_QUANSO_DONVI_QUY;
                            break;
                        case "_2":
                            fileName = ExportFileName.RPT_NS_QUANSO_DONVI_QUY_2;
                            break;
                    }
                }
                else
                {
                    switch (SelectedPaper.ValueItem)
                    {
                        case "":
                            fileName = ExportFileName.RPT_NS_QUANSO_DONVI_NAM;
                            break;
                        case "_2":
                            fileName = ExportFileName.RPT_NS_QUANSO_DONVI_NAM_2;
                            break;
                    }
                }

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, fileName);
                string fileNamePrefix = string.Format("{0}_{1}", fileName.Split(".").First(), tenDonVi) + "_" + DateTime.Now.Millisecond;
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

        private ExportResult ProcessExportDetail()
        {
            try
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                string idDonVi = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
                int month = 0;
                string monthStr = string.Empty;
                string monthStrPrev = string.Empty;
                List<int> months = new List<int>();
                var listQuanSo = new List<ReportQuanSoDonViQuery>();
                var listQuanSoThangQuyTruoc = new List<ReportQuanSoDonViQuery>();
                if (PeriodType == QuarterMonth.MONTH)
                {
                    month = Convert.ToInt32(SelectedPeriod.ValueItem);
                    monthStr = SelectedPeriod.ValueItem;
                }
                else if (PeriodType == QuarterMonth.QUARTER)
                {
                    var listQuanSoNamTruoc = _chungTuChiTietService.FindForAgencyDetailReport(_sessionInfo.YearOfWork - 1, idDonVi, "10,11,12");
                    if (Convert.ToInt32(SelectedPeriod.ValueItem) == 3)
                    {
                        //listQuanSoNamTruoc.Any(x => !string.IsNullOrEmpty(x.IdMaDonVi) && x.HasData)
                        monthStrPrev = "10,11,12";
                        listQuanSoThangQuyTruoc = listQuanSoNamTruoc.Where(x => x.XauNoiMa == "700").ToList();
                    }
                    else
                    {
                        monthStrPrev = (Convert.ToInt32(SelectedPeriod.ValueItem)) switch
                        {
                            6 => "1,2,3",
                            9 => "4,5,6",
                            12 => "7,8,9",
                            _ => ""
                        };
                        listQuanSoThangQuyTruoc = _chungTuChiTietService.FindForAgencyDetailReport(_sessionInfo.YearOfWork, idDonVi, monthStrPrev).Where(x => x.XauNoiMa == "700").ToList();
                    }

                    months = Enumerable.Range(Convert.ToInt32(SelectedPeriod.ValueItem) - 2, 3).ToList();
                    monthStr = string.Join(",", months);
                    month = months.First();
                }
                else if (PeriodType == QuarterMonth.YEAR)
                {
                    months = _chungTuService.FindMonthOfArmy(_sessionInfo.YearOfWork).Where(x => x != 0).ToList();
                    monthStr = string.Join(",", months);
                    month = months.First();
                }

                listQuanSo = _chungTuChiTietService.FindForAgencyDetailReport(_sessionInfo.YearOfWork, idDonVi, monthStr);
                listQuanSo = listQuanSo.Where(x => string.IsNullOrEmpty(x.IdMaDonVi) || (!string.IsNullOrEmpty(x.IdMaDonVi) && x.HasData)).ToList();
                RptQuanSoDonVi rptQuanSo = new RptQuanSoDonVi();
                rptQuanSo.Truoc = new List<ReportQuanSoDonViQuery>();
                rptQuanSo.BS1 = listQuanSo.Where(x => x.XauNoiMa == "500").ToList();
                rptQuanSo.BS2 = listQuanSo.Where(x => x.XauNoiMa == "600").ToList();
                List<ReportQuanSoDonViQuery> listTruoc = new List<ReportQuanSoDonViQuery>();

                if (PeriodType == QuarterMonth.QUARTER)
                {
                    listTruoc = listQuanSoThangQuyTruoc.Where(x => string.IsNullOrEmpty(x.IdMaDonVi) || (!string.IsNullOrEmpty(x.IdMaDonVi) && x.HasData)).ToList();
                }
                else if (PeriodType == QuarterMonth.YEAR)
                {
                    listTruoc = _chungTuChiTietService.FindForAgencyDetailReport(_sessionInfo.YearOfWork, idDonVi, "1").Where(x => x.XauNoiMa.StartsWith("1") && !string.IsNullOrEmpty(x.IdMaDonVi) && x.HasData).ToList();
                }
                else
                {
                    listTruoc = listQuanSo.Where(x => x.XauNoiMa.StartsWith("1") && !string.IsNullOrEmpty(x.IdMaDonVi)).ToList();
                }

                ReportQuanSoDonViQuery truoc = listQuanSo.Where(x => x.XauNoiMa.StartsWith("1") && string.IsNullOrEmpty(x.IdMaDonVi)).First();


                ProcessItem(ref truoc, listTruoc);
                rptQuanSo.Truoc.Add(truoc);

                rptQuanSo.TangCha = new List<ReportQuanSoDonViQuery>();
                List<ReportQuanSoDonViQuery> listTangCha = listQuanSo.Where(x => x.XauNoiMa.StartsWith("2") && x.BHangCha && !string.IsNullOrEmpty(x.IdMaDonVi)).ToList();
                ReportQuanSoDonViQuery tangCha = listQuanSo.Where(x => x.XauNoiMa.StartsWith("2") && string.IsNullOrEmpty(x.IdMaDonVi)).First();
                ProcessItem(ref tangCha, listTangCha);
                rptQuanSo.TangCha.Add(tangCha);

                rptQuanSo.Tang = new List<ReportQuanSoDonViQuery>();
                List<ReportQuanSoDonViQuery> listTang = listQuanSo.Where(x => x.XauNoiMa.StartsWith("2") && !x.BHangCha && string.IsNullOrEmpty(x.IdMaDonVi)).ToList();
                for (int i = 0; i < listTang.Count; i++)
                {
                    var item = listTang[i];
                    var listTangCon = listQuanSo.Where(x => x.XauNoiMa == item.XauNoiMa && !string.IsNullOrEmpty(x.IdMaDonVi)).ToList();
                    ProcessItem(ref item, listTangCon);
                    item.BHangCha = true;
                    rptQuanSo.Tang.Add(item);
                    foreach (var con in listTangCon)
                    {
                        con.MoTa = "  " + con.TenDonVi;
                        rptQuanSo.Tang.Add(con);
                    }
                }

                rptQuanSo.GiamCha = new List<ReportQuanSoDonViQuery>();
                List<ReportQuanSoDonViQuery> listGiamCha = listQuanSo.Where(x => x.XauNoiMa.StartsWith("3") && x.BHangCha && !string.IsNullOrEmpty(x.IdMaDonVi)).ToList();
                ReportQuanSoDonViQuery giamCha = listQuanSo.Where(x => x.XauNoiMa.StartsWith("3") && string.IsNullOrEmpty(x.IdMaDonVi)).First();
                ProcessItem(ref giamCha, listGiamCha);
                rptQuanSo.GiamCha.Add(giamCha);

                rptQuanSo.Giam = new List<ReportQuanSoDonViQuery>();
                List<ReportQuanSoDonViQuery> listGiam = listQuanSo.Where(x => x.XauNoiMa.StartsWith("3") && !x.BHangCha && string.IsNullOrEmpty(x.IdMaDonVi)).ToList();
                for (int i = 0; i < listGiam.Count; i++)
                {
                    var item = listGiam[i];
                    var listGiamCon = listQuanSo.Where(x => x.XauNoiMa == item.XauNoiMa && !string.IsNullOrEmpty(x.IdMaDonVi)).ToList();
                    ProcessItem(ref item, listGiamCon);
                    item.BHangCha = true;
                    rptQuanSo.Giam.Add(item);
                    foreach (var con in listGiamCon)
                    {
                        con.MoTa = "  " + con.TenDonVi;
                        rptQuanSo.Giam.Add(con);
                    }
                }

                rptQuanSo.Cap2 = _cap2;
                rptQuanSo.Cap3 = _sessionInfo.TenDonVi;
                rptQuanSo.TieuDe1 = Title1;
                rptQuanSo.TieuDe2 = Title2;
                rptQuanSo.TieuDe3 = Title3;
                rptQuanSo.TieuDeChung = PeriodType != QuarterMonth.YEAR ? string.Format("{0} năm {1}", SelectedPeriod.DisplayItem, _sessionInfo.YearOfWork) : SelectedPeriod.DisplayItem;
                rptQuanSo.TenDonVi = "";
                rptQuanSo.Ngay = DateUtils.FormatDateReport(ReportDate);
                rptQuanSo.DiaDiem = _diaDiem;
                if (PeriodType == QuarterMonth.MONTH)
                    rptQuanSo.ThangQuy = "Tháng";
                else if (PeriodType == QuarterMonth.QUARTER)
                    rptQuanSo.ThangQuy = "Quý";
                else rptQuanSo.ThangQuy = "Năm";
                rptQuanSo.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                rptQuanSo.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                rptQuanSo.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                rptQuanSo.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                rptQuanSo.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                rptQuanSo.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                rptQuanSo.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                rptQuanSo.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                rptQuanSo.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var prop in rptQuanSo.GetType().GetProperties())
                {
                    data.Add(prop.Name, prop.GetValue(rptQuanSo));
                }

                string fileName = string.Empty;
                switch (SelectedPaper.ValueItem)
                {
                    case "":
                        fileName = ExportFileName.RPT_NS_QUANSO_DONVI_CHITIET;
                        //fileName = ExportFileName.RPT_NS_QUANSO_DONVI;
                        break;
                    case "_2":
                        fileName = ExportFileName.RPT_NS_QUANSO_DONVI_CHITIET_2;
                        break;
                }
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, fileName);
                string fileNamePrefix = string.Format("{0}_{1}", fileName.Split(".").First(), DateTime.Now.Millisecond);
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

        private void ProcessItem(ref ReportQuanSoDonViQuery item, List<ReportQuanSoDonViQuery> data)
        {
            item.RThieuUy = data.Sum(x => x.RThieuUy);
            item.RTrungUy = data.Sum(x => x.RTrungUy);
            item.RThuongUy = data.Sum(x => x.RThuongUy);
            item.RDaiUy = data.Sum(x => x.RDaiUy);
            item.RThieuTa = data.Sum(x => x.RThieuTa);
            item.RThuongTa = data.Sum(x => x.RThuongTa);
            item.RTrungTa = data.Sum(x => x.RTrungTa);
            item.RDaiTa = data.Sum(x => x.RDaiTa);
            item.RTuong = data.Sum(x => x.RTuong);
            item.RBinhNhi = data.Sum(x => x.RBinhNhi);
            item.RBinhNhat = data.Sum(x => x.RBinhNhat);
            item.RHaSi = data.Sum(x => x.RHaSi);
            item.RTrungSi = data.Sum(x => x.RTrungSi);
            item.RThuongSi = data.Sum(x => x.RThuongSi);
            item.RThuongTaQNCN = data.Sum(x => x.RThuongTaQNCN);
            item.RTrungTaQNCN = data.Sum(x => x.RTrungTaQNCN);
            item.RThieuTaQNCN = data.Sum(x => x.RThieuTaQNCN);
            item.RDaiUyQNCN = data.Sum(x => x.RDaiUyQNCN);
            item.RThuongUyQNCN = data.Sum(x => x.RThuongUyQNCN);
            item.RTrungUyQNCN = data.Sum(x => x.RTrungUyQNCN);
            item.RThieuUyQNCN = data.Sum(x => x.RThieuUyQNCN);
            item.RVcqp = data.Sum(x => x.RVcqp);
            item.RCnvqp = data.Sum(x => x.RCnvqp);
            item.RLdhd = data.Sum(x => x.RLdhd);
            item.RCcqp = data.Sum(x => x.RCcqp);
        }

        private ExportResult ProcessExportSummary()
        {
            try
            {
                string agencyId = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id).ToArray());
                string month = string.Empty;
                if (PeriodType == QuarterMonth.MONTH)
                    month = SelectedPeriod.ValueItem;
                else if (PeriodType == QuarterMonth.QUARTER)
                    month = string.Join(",", Enumerable.Range(Convert.ToInt32(SelectedPeriod.ValueItem) - 2, 3).ToList());
                else
                    month = string.Join(",", Enumerable.Range(1, 12).ToList());
                List<ReportQuanSoTongHopQuery> listQuanSo = _chungTuChiTietService.FindForSummaryReport(_sessionInfo.YearOfWork, agencyId, month);
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                RptQuanSoTongHop rptQuanSo = new RptQuanSoTongHop();
                rptQuanSo.Items = listQuanSo;
                rptQuanSo.Cap2 = _cap2;
                rptQuanSo.Cap3 = _sessionInfo.TenDonVi;
                rptQuanSo.TieuDe1 = Title1;
                rptQuanSo.TieuDe2 = Title2;
                rptQuanSo.TieuDe3 = Title3;
                rptQuanSo.TieuDeChung = PeriodType != QuarterMonth.YEAR ? string.Format("{0} năm {1}", SelectedPeriod.DisplayItem, _sessionInfo.YearOfWork) : SelectedPeriod.DisplayItem;
                rptQuanSo.Ngay = DateUtils.FormatDateReport(ReportDate);
                rptQuanSo.DiaDiem = _diaDiem;
                rptQuanSo.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                rptQuanSo.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                rptQuanSo.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                rptQuanSo.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                rptQuanSo.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                rptQuanSo.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                rptQuanSo.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                rptQuanSo.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                rptQuanSo.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var prop in rptQuanSo.GetType().GetProperties())
                {
                    data.Add(prop.Name, prop.GetValue(rptQuanSo));
                }

                string fileName = string.Empty;
                if (ReportType == ReportArmy.Summary)
                    fileName = SelectedPaper.ValueItem switch
                    {
                        "" => ExportFileName.RPT_NS_QUANSO_DONVI_TONGHOP,
                        _ => ExportFileName.RPT_NS_QUANSO_DONVI_TONGHOP_2
                    };
                else
                {
                    switch (SelectedPaper.ValueItem)
                    {
                        case "":
                            fileName = ExportFileName.RPT_NS_QUANSO_DONVI_TONGHOP_LIENTHAM;
                            break;
                        case "_2":
                            fileName = ExportFileName.RPT_NS_QUANSO_DONVI_TONGHOP_LIENTHAM_2;
                            break;
                    }
                }
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, fileName);
                string fileNamePrefix = fileName.Split(".").First();
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

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_QUANSO_DONVI;
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
