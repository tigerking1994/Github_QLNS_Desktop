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
    public class PrintArmyRegularViewModel : ViewModelBase
    {
        private readonly INsQsChungTuChiTietService _chungTuChiTietService;
        private readonly IExportService _exportService;
        private readonly INsDonViService _donViService;
        private readonly IDanhMucService _danhMucService;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly IDmChuKyService _dmChuKyService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private IMapper _mapper;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private string _diaDiem;
        private DmChuKy _dmChuKy;

        public override string Title => "Báo cáo quân số thường xuyên";
        public override string Description => "Chọn thông số để in chi tiết đơn vị theo tháng";
        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintArmyRegular);

        private ReportArmy _reportType;
        public ReportArmy ReportType
        {
            get => _reportType;
            set => SetProperty(ref _reportType, value);
        }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _monthFirst;
        public ComboboxItem MonthFirst
        {
            get => _monthFirst;
            set
            {
                SetProperty(ref _monthFirst, value);
                if (_monthFirst != null)
                    LoadAgencies();
            }
        }

        private ComboboxItem _monthSecond;
        public ComboboxItem MonthSecond
        {
            get => _monthSecond;
            set
            {
                SetProperty(ref _monthSecond, value);
                if (_monthSecond != null)
                    LoadAgencies();
            }
        }

        private ComboboxItem _monthThird;
        public ComboboxItem MonthThird
        {
            get => _monthThird;
            set
            {
                SetProperty(ref _monthThird, value);
                if (_monthThird != null)
                    LoadAgencies();
            }
        }

        private ComboboxItem _monthFourth;
        public ComboboxItem MonthFourth
        {
            get => _monthFourth;
            set
            {
                SetProperty(ref _monthFourth, value);
                if (_monthFourth != null)
                    LoadAgencies();
            }
        }

        private ObservableCollection<CheckBoxItem> _agencies;
        public ObservableCollection<CheckBoxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
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

        public bool IsExportEnable
        {
            get
            {
                if (_agencies != null)
                    return _agencies.Where(x => x.IsChecked).Count() > 0;
                return false;
            }
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

        public PrintArmyRegularViewModel(
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
            LoadTieuDe();
            LoadMonths();
            LoadAgencies();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_THUONGXUYEN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
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
            _months = new List<ComboboxItem>
            {
                new ComboboxItem("Đầu năm", "0")
                {
                    HiddenValue = "0"
                }
            };
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem("Tháng " + i, i.ToString())
                {
                    HiddenValue = i.ToString()
                };
                _months.Add(month);
            }

            ComboboxItem quater;
            for (int i = 1; i <= 4; i++)
            {

                switch (i)
                {
                    case 1:
                        quater = new ComboboxItem("Quý I", "13")
                        {
                            HiddenValue = "1,2,3"
                        };
                        _months.Add(quater);
                        break;
                    case 2:
                        quater = new ComboboxItem("Quý II", "14")
                        {
                            HiddenValue = "4,5,6"
                        };
                        _months.Add(quater);
                        break;
                    case 3:
                        quater = new ComboboxItem("Quý III", "15")
                        {
                            HiddenValue = "7,8,9"
                        };
                        _months.Add(quater);
                        break;
                    case 4:
                        quater = new ComboboxItem("Quý IV", "16")
                        {
                            HiddenValue = "10,11,12"
                        };
                        _months.Add(quater);
                        break;

                }
            }

            _monthFirst = _months.First();
            _monthSecond = _months.First();
            _monthThird = _months.First();
            _monthFourth = _months.First();
        }

        /// <summary>
        /// Convert quater to month
        /// </summary>
        public string ConvertQuaterMonth(string quater)
        {
            if (Months.Select(s => s.ValueItem).Contains(quater))
            {
                var sQuater = Months.FirstOrDefault(s => s.ValueItem == quater);
                return sQuater.HiddenValue;
            }
            return string.Empty;
        }


        /// <summary>
        /// Load dữ liệu đơn vị
        /// </summary>
        private void LoadAgencies()
        {
            string month1 = ConvertQuaterMonth(MonthFirst.ValueItem);
            string month2 = ConvertQuaterMonth(MonthSecond.ValueItem);
            string month3 = ConvertQuaterMonth(MonthThird.ValueItem);
            string month4 = ConvertQuaterMonth(MonthFourth.ValueItem);
            string monthStr = string.Format("{0},{1},{2},{3}", month1, month2, month3, month4);
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
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_THUONGXUYEN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                int month1 = Convert.ToInt32(MonthFirst.ValueItem);
                int month2 = Convert.ToInt32(MonthSecond.ValueItem);
                int month3 = Convert.ToInt32(MonthThird.ValueItem);
                int month4 = Convert.ToInt32(MonthFourth.ValueItem);
                List<int> listMonth = new List<int> { month1, month2, month3, month4 };
                List<ReportQuanSoThuongXuyenQuery> listQuanSo = new List<ReportQuanSoThuongXuyenQuery>();
                listQuanSo = _chungTuChiTietService.FindForRegular(month1, month2, month3, month4, _sessionInfo.YearOfWork, agencyId);
                List<ReportQuanSoThuongXuyenQuery> results = new List<ReportQuanSoThuongXuyenQuery>();
                var agencyIdList = agencyId.Split(",").ToList();
                foreach (var agency in agencyIdList)
                {
                    ReportQuanSoThuongXuyenQuery item = new ReportQuanSoThuongXuyenQuery();
                    item.TenDonVi = listQuanSo.First(x => x.Id_DonVi.Equals(agency)).TenDonVi;
                    item.MoTa = listQuanSo.First(x => x.Id_DonVi.Equals(agency)).MoTa;
                    for (int i = 0; i < listMonth.Count; i++)
                    {
                        var month = listMonth[i];
                        if (month <= 12)
                        {                        
                            if(i == 0)
                            {
                                item.RSQ1 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RSQ);
                                item.RQNCN1 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RQNCN);
                                item.RCNVHD1 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RCNVHD);
                                item.RHSQCS1 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RHSQCS);
                            }
                            if (i == 1)
                            {
                                item.RSQ2 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RSQ);
                                item.RQNCN2 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RQNCN);
                                item.RCNVHD2 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RCNVHD);
                                item.RHSQCS2 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RHSQCS);
                            }
                            if (i == 2)
                            {
                                item.RSQ3 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RSQ);
                                item.RQNCN3 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RQNCN);
                                item.RCNVHD3 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RCNVHD);
                                item.RHSQCS3 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RHSQCS);
                            }
                            if (i == 3)
                            {
                                item.RSQ4 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RSQ);
                                item.RQNCN4 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RQNCN);
                                item.RCNVHD4 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RCNVHD);
                                item.RHSQCS4 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && x.iThangQuy == month).Sum(x => x.RHSQCS);
                            }

                        }
                        else
                        {
                            var listThangQuy = new List<int>();
                            if (month == 13)
                            {
                                listThangQuy = new List<int>();
                                listThangQuy.Add(1);
                                listThangQuy.Add(2);
                                listThangQuy.Add(3);
                            }
                            if (month == 14)
                            {
                                listThangQuy = new List<int>();
                                listThangQuy.Add(4);
                                listThangQuy.Add(5);
                                listThangQuy.Add(6);
                            }
                            if (month == 15)
                            {
                                listThangQuy = new List<int>();
                                listThangQuy.Add(7);
                                listThangQuy.Add(8);
                                listThangQuy.Add(9);
                            }
                            if (month == 16)
                            {
                                listThangQuy = new List<int>();
                                listThangQuy.Add(10);
                                listThangQuy.Add(11);
                                listThangQuy.Add(12);
                            }
                            if(i == 0)
                            {
                                item.RSQ1 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RSQ);
                                item.RQNCN1 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RQNCN);
                                item.RCNVHD1 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RCNVHD);
                                item.RHSQCS1 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RHSQCS);
                            }
                            if (i == 1)
                            {
                                item.RSQ2 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RSQ);
                                item.RQNCN2 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RQNCN);
                                item.RCNVHD2 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RCNVHD);
                                item.RHSQCS2 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RHSQCS);
                            }
                            if (i == 2)
                            {
                                item.RSQ3 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RSQ);
                                item.RQNCN3 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RQNCN);
                                item.RCNVHD3 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RCNVHD);
                                item.RHSQCS3 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RHSQCS);
                            }
                            if (i == 3)
                            {
                                item.RSQ4 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RSQ);
                                item.RQNCN4 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RQNCN);
                                item.RCNVHD4 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RCNVHD);
                                item.RHSQCS4 = listQuanSo.Where(x => x.Id_DonVi.Equals(agency) && listThangQuy.Contains(x.iThangQuy)).Sum(x => x.RHSQCS);
                            }
                        }
                    }
                    results.Add(item);

                }

                var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();

                RptQuanSoThuongXuyen rptQuanSo = new RptQuanSoThuongXuyen();
                rptQuanSo.Items = results;
                rptQuanSo.TieuDe = ReportTitle;
                rptQuanSo.TieuDe2 = ReportTitle2;
                rptQuanSo.TieuDe3 = ReportTitle3;
                rptQuanSo.Cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
                rptQuanSo.Cap2 = _sessionInfo.TenDonVi;
                rptQuanSo.TenDonVi = agencyName;
                rptQuanSo.Thang = "Tháng";
                rptQuanSo.Thang1 = getTitleThang(month1);
                rptQuanSo.Thang2 = getTitleThang(month2);
                rptQuanSo.Thang3 = getTitleThang(month3);
                rptQuanSo.Thang4 = getTitleThang(month4);
                rptQuanSo.iNam = _sessionInfo.YearOfWork;
                rptQuanSo.TenPhuLuc = "QS002";
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

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUANSO_THUONGXUYEN);
                string fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_NS_QUANSO_THUONGXUYEN.Split(".").First(), agencyName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQuanSoThuongXuyenQuery>(templateFileName, data);
                return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private string getTitleThang(int month)
        {
            if(month == 0)
            {
                return "Đầu năm";
            }
            if (month == 13)
            {
                return "Quý I";
            }
            if (month == 14)
            {
                return "Quý II";
            }
            if (month == 15)
            {
                return "Quý III";
            }
            if (month == 16)
            {
                return "Quý IV";
            }
            return "Tháng " + month;
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUANSO_THUONGXUYEN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_QUANSO_THUONGXUYEN;
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
