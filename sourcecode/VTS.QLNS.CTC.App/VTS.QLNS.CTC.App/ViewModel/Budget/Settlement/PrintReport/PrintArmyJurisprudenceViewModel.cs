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
    public class PrintArmyJurisprudenceViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;

        private ICollectionView _listAgency;
        private readonly INsDonViService _donViService;
        private readonly IMapper _mapper;
        private readonly INsQsChungTuChiTietService _chungTuChiTietService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private List<ComboboxItem> _months;
        private List<ComboboxItem> _quarters;
        private string _cap1;
        private DmChuKy _dmChuKy;
        private string _diaDiem;
        private string _title;

        public override string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public override string Description => "Chọn thông số để in chi tiết đơn vị hoặc tổng hợp";
        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintArmyJurisprudence);

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
            set => SetProperty(ref _reportType, value);
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

        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
        }

        private string _reportTitle;
        public string ReportTitle
        {
            get => _reportTitle;
            set => SetProperty(ref _reportTitle, value);
        }
        private string _reportTitle2;
        public string ReportTitle2
        {
            get => _reportTitle2;
            set => SetProperty(ref _reportTitle2, value);
        }
        private string _reportTitle3;
        public string ReportTitle3
        {
            get => _reportTitle3;
            set => SetProperty(ref _reportTitle3, value);
        }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintArmyJurisprudenceViewModel(
            ISessionService sessionService,
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
            _title = string.Format("In báo cáo quân số liên thẩm năm {0}", _sessionInfo.YearOfWork);
            LoadTieuDe();
            LoadMonths();
            LoadQuarters();
            LoadPeriod();
            LoadAgencies();
            LoadDanhMuc();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_LIENTHAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                ReportTitle = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                ReportTitle2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                ReportTitle3 = _dmChuKy.TieuDe3MoTa;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
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
            SelectedPeriod = Periods.Where(x => x.ValueItem.Contains(DateTime.Now.Month.ToString())).FirstOrDefault();
        }

        private void LoadAgencies()
        {
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
            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_LIENTHAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
        }

        private void OnExportFile(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    if (ReportType == ReportArmy.AgencyDetail)
                    {
                        foreach (var agency in Agencies.Where(x => x.IsChecked))
                        {
                            var exportResult = ProcessReport(agency.ValueItem, agency.DisplayItem);
                            if (exportResult != null)
                                results.Add(exportResult);
                        }
                    }
                    else
                    {
                        var exportResult = ProcessReport(string.Join(",", Agencies.Where(x => x.IsChecked).Select(x => x.ValueItem).ToArray()), "Tổng hợp");
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

        private ExportResult ProcessReport(string agencyId, string agencyName)
        {
            try
            {
                int month = 1;
                if (PeriodType == QuarterMonth.MONTH)
                    month = Convert.ToInt32(SelectedPeriod.ValueItem);
                else
                    month = Convert.ToInt32(SelectedPeriod.ValueItem.Split(",").Last());
                List<ReportQuanSoLienThamQuery> listQuanSo = new List<ReportQuanSoLienThamQuery>();
                listQuanSo = _chungTuChiTietService.FindForJurisprudence(month, _sessionInfo.YearOfWork, agencyId);
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_LIENTHAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                RptQuanSoLienTham rptQuanSo = new RptQuanSoLienTham();
                rptQuanSo.Items = listQuanSo;
                rptQuanSo.Cap1 = _cap1;
                rptQuanSo.Cap2 = _sessionInfo.TenDonVi;
                rptQuanSo.TieuDe = ReportTitle;
                rptQuanSo.TieuDe2 = ReportTitle2;
                rptQuanSo.TieuDe3 = ReportTitle3;
                rptQuanSo.TenDonVi = agencyName;
                rptQuanSo.Nam = string.Format("{0} - {1}", SelectedPeriod.DisplayItem, _sessionInfo.YearOfWork);
                rptQuanSo.TenPhuLuc = "QS05";
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

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUANSO_LIENTHAM);
                string fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_NS_QUANSO_LIENTHAM.Split(".").First(), agencyName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQuanSoLienThamQuery>(templateFileName, data);
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
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_LIENTHAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_QUANSO_LIENTHAM;
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
