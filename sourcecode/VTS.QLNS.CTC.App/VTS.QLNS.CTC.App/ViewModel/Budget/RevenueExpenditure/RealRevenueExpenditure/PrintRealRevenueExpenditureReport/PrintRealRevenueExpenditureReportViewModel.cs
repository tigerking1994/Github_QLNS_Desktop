using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.RealRevenueExpenditure.PrintRealRevenueExpenditureReport
{
    public class PrintRealRevenueExpenditureReportViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly ITTnDanhMucLoaiHinhService _tTnDanhMucLoaiHinhService;
        private readonly ITnQtChungTuChiTietService _tnQtChungTuChiTietService;
        private readonly ITnQtChungTuHD4554Service _tnQtChungTuHD4554Service;
        private readonly ITnQtChungTuChiTietHD4554Service _tnQtChungTuChiTietHD4554Service;
        private readonly ITnQtChungTuService _tnQtChungTuService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private readonly ILog _logger;
        private List<string> _months = new List<string>();
        private List<string> _quaters = new List<string>();
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private INsMucLucNganSachService _mucLucNganSachService;
        private List<TnQtChungTuChiTietHD4554Query> _lstdata;
        private string _cap1;
        private string _diaDiem;
        private string _typeChuKy;
        private DmChuKy _dmChuKy;

        public override string Name => "BÁO CÁO - THU NỘP NGÂN SÁCH";
        public override string Description => "(báo cáo - thu nộp ngân sách)";

        public RealRevenueExpenditureType RealRevenueExpenditureTypes { get; set; }

        private ObservableCollection<TnQtChungTuChiTietModel> _itemsTnQt;
        public ObservableCollection<TnQtChungTuChiTietModel> ItemsTnQt
        {
            get => _itemsTnQt;
            set => SetProperty(ref _itemsTnQt, value);
        }

        public double TotalTongSoThu { get; set; }

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
                    OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                }
            }
        }

        public string SelectedBudgetIndexCount
        {
            get
            {
                int totalCount = BudgetIndexes != null ? BudgetIndexes.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
                return string.Format("CHỌN LNS {0}/{1}", totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => BudgetIndexes.Where(x => x.IsFilter).All(x => x.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                foreach (NsMuclucNgansachModel item in BudgetIndexes.Where(x => x.IsFilter))
                {
                    item.IsSelected = _isSelectAllBudgetIndex;
                }
            }
        }
        #endregion

        #region list đơn vị  
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

        private bool _isSelectAllAgency;
        public bool IsSelectAllAgency
        {
            get => _isSelectAllAgency;
            set
            {
                SetProperty(ref _isSelectAllAgency, value);
                foreach (AgencyModel item in Agencies)
                {
                    item.Selected = _isSelectAllAgency;
                }

                if (SetProperty(ref _isSelectAllAgency, value))
                {
                    string lstDonVi = string.Join(",", Agencies.Where(item => item.Selected).Select(x => x.Id));
                    LoadBudgetIndexes(lstDonVi);
                }
            }
        }

        public string SelectedAgencyCount
        {
            get
            {
                int totalCount = Agencies != null ? Agencies.Count : 0;
                int totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                string lstDonVi = string.Join(",", Agencies.Where(item => item.Selected).Select(x => x.Id));
                LoadBudgetIndexes(lstDonVi);
                return string.Format("CHỌN ĐƠN VỊ {0}/{1}", totalSelected, totalCount);

            }
        }
        #endregion

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

        private ObservableCollection<ComboboxItem> _quarterMonths = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> QuarterMonths
        {
            get => _quarterMonths;
            set => SetProperty(ref _quarterMonths, value);
        }

        private ComboboxItem _quarterMonthSelected;
        public ComboboxItem QuarterMonthSelected
        {
            get => _quarterMonthSelected;
            set
            {
                SetProperty(ref _quarterMonthSelected, value);
                LoadTimeOptions();
                OnPropertyChanged(nameof(TimeOptionSelected));
            }
        }

        private ObservableCollection<ComboboxItem> _timeOptions = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> TimeOptions
        {
            get => _timeOptions;
            set => SetProperty(ref _timeOptions, value);
        }

        private ComboboxItem _timeOptionSelected;
        public ComboboxItem TimeOptionSelected
        {
            get => _timeOptionSelected;
            set
            {
                if (SetProperty(ref _timeOptionSelected, value))
                    LoadAgencies();
            }
        }

        private string _txtTitleFirst;
        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set => SetProperty(ref _txtTitleFirst, value);
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

        private TimeOptionTypes _inTheo;
        public TimeOptionTypes InTheo
        {
            get => _inTheo;
            set
            {
                SetProperty(ref _inTheo, value);
                LoadTimeOptions();
                LoadAgencies();
                LoadBudgetIndexes();
                LoadTieuDe();
                OnPropertyChanged(nameof(TimeOptionSelected));
                //LoadQuarterMonths();
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

        private bool _inMotToChecked;
        public bool InMotToChecked
        {
            get => _inMotToChecked;
            set => SetProperty(ref _inMotToChecked, value);
        }

        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public PrintRealRevenueExpenditureReportViewModel(IMapper mapper,
            ISessionService sessionService,
            INsDonViService donViService,
            ITTnDanhMucLoaiHinhService tnDanhMucLoaiHinhService,
            ITnQtChungTuChiTietService tnQtChungTuChiTietService,
            ITnQtChungTuService tnQtChungTuService,
            IExportService exportService,
            INsMucLucNganSachService nsMucLucNganSachService,
            ITnQtChungTuHD4554Service tnQtChungTuHD4554Service,
            ITnQtChungTuChiTietHD4554Service tnQtChungTuChiTietHD4554Service,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            ILog logger)
        {

            _mapper = mapper;
            _sessionService = sessionService;
            _donViService = donViService;
            _tTnDanhMucLoaiHinhService = tnDanhMucLoaiHinhService;
            _tnQtChungTuChiTietService = tnQtChungTuChiTietService;
            _tnQtChungTuService = tnQtChungTuService;
            _exportService = exportService;
            _logger = logger;
            _mucLucNganSachService = nsMucLucNganSachService;
            _tnQtChungTuHD4554Service = tnQtChungTuHD4554Service;
            _tnQtChungTuChiTietHD4554Service = tnQtChungTuChiTietHD4554Service;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            PrintBrowserCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            InTheo = TimeOptionTypes.Months;
            InitReportDefaultDate();
            //LoadQuarterMonths();
            LoadCatUnitTypes();
            LoadAgencies();
            LoadBudgetIndexes();
            //LoadDataLNS();
            LoadTitle();
            LoadKieuGiayIn();
            LoadDanhMuc();
            LoadTieuDe();
        }

        private void OnConfigSign()
        {
            if (RealRevenueExpenditureTypes.Equals(RealRevenueExpenditureType.REAL_BUDGET_NATIONAL_DEFENSE_RESULT))
            {
                if (InTheo == TimeOptionTypes.Year)
                {
                    _typeChuKy = TypeChuKy.RPT_NS_THUNOP_NGANSACH_QUOCPHONG_NAM;
                }
                else if (InTheo == TimeOptionTypes.Quarter)
                {
                    _typeChuKy = TypeChuKy.RPT_NS_THUNOP_NGANSACH_QUOCPHONG_QUY;
                }
                else
                {
                    _typeChuKy = TypeChuKy.RPT_NS_THUNOP_NGANSACH_QUOCPHONG_THANG;
                }
            }
            else
            {
                if (InTheo == TimeOptionTypes.Year)
                {
                    _typeChuKy = TypeChuKy.RPT_NS_THUNOP_NGANSACH_NHANHUOC_NAM;
                }
                else if (InTheo == TimeOptionTypes.Quarter)
                {
                    _typeChuKy = TypeChuKy.RPT_NS_THUNOP_NGANSACH_NHANHUOC_QUY;
                }
                else
                {
                    _typeChuKy = TypeChuKy.RPT_NS_THUNOP_NGANSACH_NHANHUOC_THANG;
                }
            }

            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuKy;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }


        private void LoadDanhMuc()
        {
            DanhMuc danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            DanhMuc danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        public virtual void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 Ngang", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A3 Ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(0);
        }


        private void LoadTitle()
        {
            if (RealRevenueExpenditureTypes.Equals(RealRevenueExpenditureType.REAL_BUDGET_NATIONAL_DEFENSE_RESULT))
            {
                _txtTitleFirst = string.Format("BÁO CÁO CÁC KHOẢN THU NỘP BỘ QUỐC PHÒNG NĂM {0}", _sessionService.Current.YearOfWork);
                _txtTitleSecond = string.Format("Kèm theo Công văn số...../CTC-KHNS ngày...tháng....năm {0} của Cục Tài chính", _sessionService.Current.YearOfWork);
            }
            else if (RealRevenueExpenditureTypes.Equals(RealRevenueExpenditureType.REAL_BUDGET_STATE_RESULT))
            {
                _txtTitleFirst = string.Format("BÁO CÁO CÁC KHOẢN THU NỘP NGÂN SÁCH NHÀ NƯỚC NĂM {0}", _sessionService.Current.YearOfWork);
                _txtTitleSecond = string.Format("Kèm theo Công văn số...../CTC-KHNS ngày...tháng....năm {0} của Cục Tài chính", _sessionService.Current.YearOfWork);
            }
        }

        //private void LoadQuarterMonths()
        //{
        //    int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        //    int[] quaters = { 3, 6, 9, 12 };
        //    List<ComboboxItem> expenseTypes = new List<ComboboxItem>();

        //    if (InTheo.Equals(TimeOptionTypes.Months))
        //    {
        //        foreach (var item in months)
        //        {
        //            expenseTypes.Add(new ComboboxItem { DisplayItem = "Tháng " + item, ValueItem = item.ToString() });
        //        }
        //    }

        //    if (InTheo.Equals(TimeOptionTypes.Quarter))
        //    {
        //        foreach (var item in quaters)
        //        {
        //            if (item.Equals(3))
        //            {
        //                ComboboxItem quater = new ComboboxItem("Quý I ", item.ToString());
        //                expenseTypes.Add(quater);
        //            }
        //            else if (item.Equals(6))
        //            {
        //                ComboboxItem quater = new ComboboxItem("Quý II ", item.ToString());
        //                expenseTypes.Add(quater);
        //            }
        //            else if (item.Equals(9))
        //            {
        //                ComboboxItem quater = new ComboboxItem("Quý III ", item.ToString());
        //                expenseTypes.Add(quater);
        //            }
        //            else if (item.Equals(12))
        //            {
        //                ComboboxItem quater = new ComboboxItem("Quý IV ", item.ToString());
        //                expenseTypes.Add(quater);
        //            }
        //        }
        //    }

        //    if (InTheo.Equals(TimeOptionTypes.Year))
        //    {
        //        expenseTypes.Add(new ComboboxItem { DisplayItem = "Tháng", ValueItem = RevenueExpenditureType.REAL_REVENUE_REPORT_MONTH_KEY });
        //        expenseTypes.Add(new ComboboxItem { DisplayItem = "Quý", ValueItem = RevenueExpenditureType.REAL_REVENUE_REPORT_QUATER_KEY });
        //    }

        //    QuarterMonths = new ObservableCollection<ComboboxItem>(expenseTypes);

        //    if (QuarterMonths.Count() > 0)
        //        _quarterMonthSelected = expenseTypes.ElementAt(0);

        //    LoadTimeOptions();
        //}

        private void LoadTimeOptions()
        {
            List<ComboboxItem> timeOptionsTypes = new List<ComboboxItem>();

            if (RealRevenueExpenditureTypes.Equals(RealRevenueExpenditureType.REAL_BUDGET_NATIONAL_DEFENSE_RESULT))
            {
                if (InTheo.Equals(TimeOptionTypes.Year))
                {
                    timeOptionsTypes = new List<ComboboxItem>()
                {
                    new ComboboxItem {DisplayItem = "Cả năm (tháng)", ValueItem = "0"},
                    new ComboboxItem {DisplayItem = "Cả năm (quý)", ValueItem = "1"}
                };
                }
                else if (InTheo.Equals(TimeOptionTypes.Months))
                {
                    for (int month = 1; month <= 12; month++)
                    {
                        ComboboxItem cbxMonth = new ComboboxItem { DisplayItem = "Tháng " + month.ToString(), ValueItem = month.ToString() };
                        timeOptionsTypes.Add(cbxMonth);
                    }
                }
                else if (InTheo.Equals(TimeOptionTypes.Quarter))
                {
                    int item = 0;
                    for (int quater = 3; quater <= 12; quater += 3)
                    {
                        item += 1;
                        ComboboxItem cbxQuater = new ComboboxItem
                        {
                            DisplayItem = "Quý " + (quater - (quater - item)).ToString(),
                            ValueItem = quater.ToString()
                        };
                        timeOptionsTypes.Add(cbxQuater);
                    }
                }
            }
            else if (RealRevenueExpenditureTypes.Equals(RealRevenueExpenditureType.REAL_BUDGET_STATE_RESULT))
            {
                if (InTheo.Equals(TimeOptionTypes.Year))
                {
                    timeOptionsTypes = new List<ComboboxItem>()
                {
                    new ComboboxItem {DisplayItem = "Cả năm (tháng)", ValueItem = "0"},
                    new ComboboxItem {DisplayItem = "Cả năm (quý)", ValueItem = "1"}
                };
                }
                else if (InTheo.Equals(TimeOptionTypes.Months))
                {
                    for (int month = 1; month <= 12; month++)
                    {
                        ComboboxItem cbxMonth = new ComboboxItem { DisplayItem = "Tháng " + month.ToString(), ValueItem = month.ToString() };
                        timeOptionsTypes.Add(cbxMonth);
                    }
                }
                else if (InTheo.Equals(TimeOptionTypes.Quarter))
                {
                    int item = 0;
                    for (int quater = 3; quater <= 12; quater += 3)
                    {
                        if (quater.Equals(3))
                        {
                            ComboboxItem cbxQuater = new ComboboxItem("Quý I ", quater.ToString());
                            timeOptionsTypes.Add(cbxQuater);
                        }
                        else if (quater.Equals(6))
                        {
                            ComboboxItem cbxQuater = new ComboboxItem("Quý II ", quater.ToString());
                            timeOptionsTypes.Add(cbxQuater);
                        }
                        else if (quater.Equals(9))
                        {
                            ComboboxItem cbxQuater = new ComboboxItem("Quý III ", quater.ToString());
                            timeOptionsTypes.Add(cbxQuater);
                        }
                        else if (quater.Equals(12))
                        {
                            ComboboxItem cbxQuater = new ComboboxItem("Quý IV ", quater.ToString());
                            timeOptionsTypes.Add(cbxQuater);
                        }
                    }
                }
            }

            if (timeOptionsTypes.Count() > 0)
            {
                _timeOptions = new ObservableCollection<ComboboxItem>(timeOptionsTypes);
                _timeOptionSelected = TimeOptions.ElementAt(0);
                OnPropertyChanged(nameof(TimeOptionSelected));

            }
            else
            {
                _timeOptions = new ObservableCollection<ComboboxItem>(timeOptionsTypes);
                //OnPropertyChanged(nameof(TimeOptionSelected));
            }

            OnPropertyChanged(nameof(TimeOptions));

        }

        private void LoadCatUnitTypes()
        {
            _catUnitTypes = new ObservableCollection<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE && x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh.Count == 0)
                _catUnitTypes.Add(new ComboboxItem("Đồng", "1"));
            foreach (DanhMuc dvt in listDonViTinh)
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = dvt.STen;
                cb.ValueItem = dvt.SGiaTri;
                cb.Type = dvt.SMoTa;
                _catUnitTypes.Add(new ComboboxItem(dvt.STen, dvt.SGiaTri));
            }

            OnPropertyChanged(nameof(CatUnitTypes));
            _catUnitTypeSelected = CatUnitTypes.ElementAt(0);

            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;

        }

        private void OnExport(ExportType exportType)
        {
            if (RealRevenueExpenditureTypes.Equals(RealRevenueExpenditureType.REAL_BUDGET_NATIONAL_DEFENSE_RESULT))
            {
                if (exportType == ExportType.EXCEL && InMotToChecked)
                    OnExportBaoCaoQuocPhongOnePage(exportType);
                else
                    OnExportBaoCaoQuocPhong(exportType);
            }
            else
            {
                if (exportType == ExportType.EXCEL && InMotToChecked)
                    OnExportBaoCaoNganSachNhaNuocOnePage2(exportType);

                else
                    OnExportBaoCaoNganSachNhaNuoc(exportType);

            }

        }

        private void OnExportBaoCaoQuocPhongOnePage(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    List<ExportResult> exportResults = new List<ExportResult>();
                    int dvt = Convert.ToInt32(_catUnitTypeSelected.ValueItem);
                    List<AgencyModel> lstIdDonVi = Agencies.Where(item => item.Selected).ToList();
                    //List<List<AgencyModel>> listDonViSplits = SplitList(lstIdDonVi, size).ToList();

                    List<NsMuclucNgansachModel> lstSLNS = BudgetIndexes.Where(x => x.IsSelected).ToList();
                    int sizeSLNS = lstSLNS.Count;
                    List<List<NsMuclucNgansachModel>> lstSLNSplits = SplitList(lstSLNS, sizeSLNS).ToList();

                    var predicate = PredicateBuilder.True<TnQtChungTuHD4554>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork && x.INamNganSach == _sessionService.Current.YearOfBudget
                                                    && x.INguonNganSach == _sessionService.Current.Budget);
                    predicate = predicate.And(x => lstIdDonVi.Select(x => x.IIDMaDonVi).Contains(x.IIdMaDonVi));
                    var allChungTu = _tnQtChungTuHD4554Service.FindByCondition(predicate).ToList();
                    GetDataExport(allChungTu, lstIdDonVi, dvt, lstSLNS);

                    HandlerDataExportOnePage(lstSLNSplits, sizeSLNS, dvt, exportType, exportResults, lstSLNS.Count(), lstIdDonVi);
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
                        if (result != null)
                        {
                            //if (InMotToChecked)
                            //    exportType = ExportType.PDF_ONE_PAPER;
                            //if (InMotToChecked)
                            //    {
                            //    if (exportType == ExportType.EXCEL)
                            //    {
                            //        exportType = ExportType.EXCEL_ONE_PAPER;
                            //    }
                            //    else
                            //    {
                            //        exportType = ExportType.PDF_ONE_PAPER;
                            //    }
                            //}
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
                System.Windows.Forms.MessageBox.Show(ex.Message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        public void OnExportBaoCaoNganSachNhaNuocOnePage(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    List<ExportResult> exportResults = new List<ExportResult>();
                    List<List<NsMuclucNgansachModel>> lstSLNSplits = new List<List<NsMuclucNgansachModel>>();
                    int dvt = Convert.ToInt32(_catUnitTypeSelected.ValueItem);
                    List<AgencyModel> lstIdDonVi = Agencies.Where(item => item.Selected).ToList();

                    List<NsMuclucNgansachModel> lstSLNS = BudgetIndexes.Where(x => x.IsSelected).ToList();
                    int sizeSLNS = lstSLNS.Count;
                    /// Dia phuong
                    var lstSLNSofNSSNDP = lstSLNS.Where(x => x.Lns.StartsWith(RevenueExpenditureType.MLNS_DP)).ToList();
                    int sizeSLNSDP = SelectedKieuGiayIn.ValueItem == "2" ? lstSLNSofNSSNDP.Count : 10;
                    // Bo Quoc Phong
                    var lstSLNSofNSSNBQP = lstSLNS.Where(x => x.Lns.StartsWith(RevenueExpenditureType.MLNS_BQP)).ToList();
                    int sizeSLNSBQP = SelectedKieuGiayIn.ValueItem == "2" ? lstSLNSofNSSNBQP.Count : 10;

                    List<List<NsMuclucNgansachModel>> lstSLNSplitsDP = SplitList(lstSLNSofNSSNDP, sizeSLNSDP).ToList();
                    List<List<NsMuclucNgansachModel>> lstSLNSplitsBQP = SplitList(lstSLNSofNSSNBQP, sizeSLNSBQP).ToList();

                    if (lstSLNSofNSSNBQP.Count > 0)
                    {
                        NsMuclucNgansachModel nsTongSoBQP = new NsMuclucNgansachModel();
                        nsTongSoBQP.MoTa = "Tổng số";
                        nsTongSoBQP.STenDonVi = "ALL";
                        lstSLNS.Insert(lstSLNSofNSSNDP.Count, nsTongSoBQP);
                    }

                    lstSLNSplits = SplitList(lstSLNS, lstSLNS.Count).ToList();

                    var predicate = PredicateBuilder.True<TnQtChungTuHD4554>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork && x.INamNganSach == _sessionService.Current.YearOfBudget
                                                    && x.INguonNganSach == _sessionService.Current.Budget);
                    predicate = predicate.And(x => lstIdDonVi.Select(x => x.IIDMaDonVi).Contains(x.IIdMaDonVi));
                    var allChungTu = _tnQtChungTuHD4554Service.FindByCondition(predicate).ToList();
                    GetDataExportCaoNganSachNhaNuoc(allChungTu, lstIdDonVi, dvt, lstSLNS);

                    //HandlerDataExport(lstSLNSplits, sizeSLNS, dvt, exportType, exportResults, lstSLNS.Count(), lstIdDonVi);
                    HandlerDataExportCaoNganSachNhaNuocOnePage(lstSLNSplits, sizeSLNS, dvt, exportType, exportResults, lstSLNS.Count(), lstIdDonVi, lstSLNSofNSSNBQP.Count, lstSLNSofNSSNBQP, lstSLNSofNSSNDP.Count);
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
                        if (result != null)
                        {
                            //if (InMotToChecked)
                            //    exportType = ExportType.PDF_ONE_PAPER;
                            //if (InMotToChecked)
                            //    {
                            //    if (exportType == ExportType.EXCEL)
                            //    {
                            //        exportType = ExportType.EXCEL_ONE_PAPER;
                            //    }
                            //    else
                            //    {
                            //        exportType = ExportType.PDF_ONE_PAPER;
                            //    }
                            //}
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
                System.Windows.Forms.MessageBox.Show(ex.Message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        public void OnExportBaoCaoNganSachNhaNuocOnePage2(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    List<ExportResult> exportResults = new List<ExportResult>();
                    List<List<NsMuclucNgansachModel>> lstSLNSplits = new List<List<NsMuclucNgansachModel>>();
                    int dvt = Convert.ToInt32(_catUnitTypeSelected.ValueItem);
                    List<AgencyModel> lstIdDonVi = Agencies.Where(item => item.Selected).ToList();

                    List<NsMuclucNgansachModel> lstSLNS = BudgetIndexes.Where(x => x.IsSelected).ToList();
                    int sizeSLNS = lstSLNS.Count;
                    /// Dia phuong
                    var lstSLNSofNSSNDP = lstSLNS.Where(x => x.Lns.StartsWith(RevenueExpenditureType.MLNS_DP)).ToList();
                    int sizeSLNSDP = SelectedKieuGiayIn.ValueItem == "2" ? lstSLNSofNSSNDP.Count : 10;
                    // Bo Quoc Phong
                    var lstSLNSofNSSNBQP = lstSLNS.Where(x => x.Lns.StartsWith(RevenueExpenditureType.MLNS_BQP)).ToList();
                    int sizeSLNSBQP = SelectedKieuGiayIn.ValueItem == "2" ? lstSLNSofNSSNBQP.Count : 10;

                    List<List<NsMuclucNgansachModel>> lstSLNSplitsDP = SplitList(lstSLNSofNSSNDP, sizeSLNSDP).ToList();
                    List<List<NsMuclucNgansachModel>> lstSLNSplitsBQP = SplitList(lstSLNSofNSSNBQP, sizeSLNSBQP).ToList();

                    if (lstSLNSofNSSNDP.Count > 0)
                    {
                        NsMuclucNgansachModel nsTongSoDP = new NsMuclucNgansachModel();
                        nsTongSoDP.Lns = RevenueExpenditureType.MLNS_DP;
                        nsTongSoDP.MoTa = "Tổng số";
                        nsTongSoDP.STenDonVi = "ALL1";
                        lstSLNS.Insert(0, nsTongSoDP);
                    }
                    if (lstSLNSofNSSNBQP.Count > 0)
                    {
                        NsMuclucNgansachModel nsTongSoBQP = new NsMuclucNgansachModel();
                        nsTongSoBQP.Lns = RevenueExpenditureType.MLNS_BQP;
                        nsTongSoBQP.MoTa = "Tổng số";
                        nsTongSoBQP.STenDonVi = "ALL";
                        lstSLNS.Insert(lstSLNSofNSSNDP.Count + 1, nsTongSoBQP);
                    }

                    lstSLNSplits = SplitList(lstSLNS, lstSLNS.Count).ToList();

                    var predicate = PredicateBuilder.True<TnQtChungTuHD4554>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork && x.INamNganSach == _sessionService.Current.YearOfBudget
                                                    && x.INguonNganSach == _sessionService.Current.Budget);
                    predicate = predicate.And(x => lstIdDonVi.Select(x => x.IIDMaDonVi).Contains(x.IIdMaDonVi));
                    var allChungTu = _tnQtChungTuHD4554Service.FindByCondition(predicate).ToList();
                    GetDataExportCaoNganSachNhaNuoc(allChungTu, lstIdDonVi, dvt, lstSLNS);

                    //HandlerDataExport(lstSLNSplits, sizeSLNS, dvt, exportType, exportResults, lstSLNS.Count(), lstIdDonVi);
                    HandlerDataExportCaoNganSachNhaNuocOnePage2(lstSLNSplits, dvt, exportType, exportResults, lstIdDonVi, lstSLNSofNSSNBQP.Count, lstSLNSofNSSNBQP, lstSLNSofNSSNDP.Count);
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
                        if (result != null)
                        {
                            //if (InMotToChecked)
                            //    exportType = ExportType.PDF_ONE_PAPER;
                            //if (InMotToChecked)
                            //    {
                            //    if (exportType == ExportType.EXCEL)
                            //    {
                            //        exportType = ExportType.EXCEL_ONE_PAPER;
                            //    }
                            //    else
                            //    {
                            //        exportType = ExportType.PDF_ONE_PAPER;
                            //    }
                            //}
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
                System.Windows.Forms.MessageBox.Show(ex.Message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        public void OnExportBaoCaoNganSachNhaNuoc(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    List<ExportResult> exportResults = new List<ExportResult>();
                    List<List<NsMuclucNgansachModel>> lstSLNSplits = new List<List<NsMuclucNgansachModel>>();
                    int dvt = Convert.ToInt32(_catUnitTypeSelected.ValueItem);
                    List<AgencyModel> lstIdDonVi = Agencies.Where(item => item.Selected).ToList();
                    //int size = SelectedKieuGiayIn.ValueItem == "2" ? lstIdDonVi.Count : 7;
                    //List<List<AgencyModel>> listDonViSplits = SplitList(lstIdDonVi, size).ToList();

                    List<NsMuclucNgansachModel> lstSLNS = BudgetIndexes.Where(x => x.IsSelected).ToList();
                    int sizeSLNS = SelectedKieuGiayIn.ValueItem == "2" ? lstSLNS.Count : 6;
                    /// Dia phuong
                    var lstSLNSofNSSNDP = lstSLNS.Where(x => x.Lns.StartsWith(RevenueExpenditureType.MLNS_DP)).ToList();
                    int sizeSLNSDP = 10;
                    // Bo Quoc Phong
                    var lstSLNSofNSSNBQP = lstSLNS.Where(x => x.Lns.StartsWith(RevenueExpenditureType.MLNS_BQP)).ToList();
                    int sizeSLNSBQP = 10;

                    List<List<NsMuclucNgansachModel>> lstSLNSplitsDP = SplitList(lstSLNSofNSSNDP, sizeSLNSDP).ToList();
                    List<List<NsMuclucNgansachModel>> lstSLNSplitsBQP = SplitList(lstSLNSofNSSNBQP, sizeSLNSBQP).ToList();
                    lstSLNSplits.AddRange(lstSLNSplitsDP);
                    lstSLNSplits.AddRange(lstSLNSplitsBQP);

                    var predicate = PredicateBuilder.True<TnQtChungTuHD4554>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork && x.INamNganSach == _sessionService.Current.YearOfBudget
                                                    && x.INguonNganSach == _sessionService.Current.Budget);
                    predicate = predicate.And(x => lstIdDonVi.Select(x => x.IIDMaDonVi).Contains(x.IIdMaDonVi));
                    var allChungTu = _tnQtChungTuHD4554Service.FindByCondition(predicate).ToList();
                    GetDataExportCaoNganSachNhaNuoc(allChungTu, lstIdDonVi, dvt, lstSLNS);

                    //HandlerDataExport(lstSLNSplits, sizeSLNS, dvt, exportType, exportResults, lstSLNS.Count(), lstIdDonVi);
                    HandlerDataExportCaoNganSachNhaNuoc(lstSLNSplits, sizeSLNS, dvt, exportType, exportResults, lstSLNS.Count(), lstIdDonVi, lstSLNSofNSSNBQP.Count, lstSLNSofNSSNBQP);
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
                        if (result != null)
                        {
                            //if (InMotToChecked)
                            //    exportType = ExportType.PDF_ONE_PAPER;
                            //if (InMotToChecked)
                            //    {
                            //    if (exportType == ExportType.EXCEL)
                            //    {
                            //        exportType = ExportType.EXCEL_ONE_PAPER;
                            //    }
                            //    else
                            //    {
                            //        exportType = ExportType.PDF_ONE_PAPER;
                            //    }
                            //}
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
                System.Windows.Forms.MessageBox.Show(ex.Message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        public void OnExportBaoCaoQuocPhong(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    List<ExportResult> exportResults = new List<ExportResult>();
                    int dvt = Convert.ToInt32(_catUnitTypeSelected.ValueItem);
                    List<AgencyModel> lstIdDonVi = Agencies.Where(item => item.Selected).ToList();
                    int size = 7;
                    List<List<AgencyModel>> listDonViSplits = SplitList(lstIdDonVi, size).ToList();

                    List<NsMuclucNgansachModel> lstSLNS = BudgetIndexes.Where(x => x.IsSelected).ToList();
                    int sizeSLNS = 6;
                    List<List<NsMuclucNgansachModel>> lstSLNSplits = SplitList(lstSLNS, sizeSLNS).ToList();

                    var predicate = PredicateBuilder.True<TnQtChungTuHD4554>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork && x.INamNganSach == _sessionService.Current.YearOfBudget
                                                    && x.INguonNganSach == _sessionService.Current.Budget);
                    predicate = predicate.And(x => lstIdDonVi.Select(x => x.IIDMaDonVi).Contains(x.IIdMaDonVi));
                    var allChungTu = _tnQtChungTuHD4554Service.FindByCondition(predicate).ToList();
                    GetDataExport(allChungTu, lstIdDonVi, dvt, lstSLNS);

                    HandlerDataExportBaoCaoQuocPhong(lstSLNSplits, sizeSLNS, dvt, exportType, exportResults, lstSLNS.Count(), lstIdDonVi);
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
                        if (result != null)
                        {
                            if (InMotToChecked)
                                exportType = ExportType.PDF_ONE_PAPER;
                            //if (InMotToChecked)
                            //    {
                            //    if (exportType == ExportType.EXCEL)
                            //    {
                            //        exportType = ExportType.EXCEL_ONE_PAPER;
                            //    }
                            //    else
                            //    {
                            //        exportType = ExportType.PDF_ONE_PAPER;
                            //    }
                            //}
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
                System.Windows.Forms.MessageBox.Show(ex.Message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void HandlerDataExportBaoCaoQuocPhong(List<List<NsMuclucNgansachModel>> listSLNSplits, int size, int dvt, ExportType exportType, List<ExportResult> exportResults, int countSLNS, List<AgencyModel> lstDonVi)
        {
            int donViTinh = 1;
            int count = 1;
            string sCap1 = GetLevelTitle(_dmChuKy, 1);
            string sCap2 = GetLevelTitle(_dmChuKy, 2);
            foreach (var sLNSPage in listSLNSplits)
            {
                List<ReportTnQtChungTuChiTietHD4554Query> results = new List<ReportTnQtChungTuChiTietHD4554Query>();
                List<HeaderReportTnQtChungTuChiTietHD4554> headers = new List<HeaderReportTnQtChungTuChiTietHD4554>();
                List<HeaderReportTnQtChungTuChiTietHD4554> headersSTT = new List<HeaderReportTnQtChungTuChiTietHD4554>();
                var itemsClone = _lstdata.Clone();
                List<TnQtChungTuChiTietHD4554Query> itemsPage = new List<TnQtChungTuChiTietHD4554Query>();
                int inumber = 1;
                // Tờ 1 lấy hết dữ liệu (chỉ cần cột tổng cộng có dữ liệu)
                if (listSLNSplits.IndexOf(sLNSPage) == 0)
                {
                    itemsPage = itemsClone.Where(x => !string.IsNullOrEmpty(x.SLNS)).ToList();

                }
                else
                {
                    itemsPage = itemsClone.Where(x => !string.IsNullOrEmpty(x.SLNS) && sLNSPage.Select(x => x.Lns).Contains(x.SLNS)).ToList();
                    if (!CheckDonVi(lstDonVi, itemsPage))
                    {
                        itemsPage = OnAddDonVi(itemsPage, lstDonVi, sLNSPage);
                    }
                }

                if (sLNSPage.Count < size)
                {
                    int countEmpty = size - sLNSPage.Count;
                    for (int j = 0; j < countEmpty; j++)
                    {
                        NsMuclucNgansachModel emptyCb = new NsMuclucNgansachModel();
                        sLNSPage.Add(emptyCb);
                    }
                }

                if (listSLNSplits.IndexOf(sLNSPage) == 0)
                {
                    HeaderReportTnQtChungTuChiTietHD4554 hdSTT = new HeaderReportTnQtChungTuChiTietHD4554();
                    hdSTT.STT = string.Format("(1)=(2)+..+({0})", countSLNS + 1);
                    headersSTT.Add(hdSTT);
                    for (int i = 0; i < sLNSPage.Count; i++)
                    {
                        hdSTT = new HeaderReportTnQtChungTuChiTietHD4554();
                        hdSTT.STT = string.Format("({0})", i + 2);
                        headersSTT.Add(hdSTT);
                    }
                }
                else
                {
                    HeaderReportTnQtChungTuChiTietHD4554 hdSTT = new HeaderReportTnQtChungTuChiTietHD4554();
                    for (int i = 0; i < countSLNS; i++)
                    {
                        hdSTT = new HeaderReportTnQtChungTuChiTietHD4554();
                        hdSTT.STT = string.Format("({0})", i + 8);
                        headersSTT.Add(hdSTT);
                    }
                }

                foreach (NsMuclucNgansachModel tenSLNS in sLNSPage)
                {
                    HeaderReportTnQtChungTuChiTietHD4554 hd = new HeaderReportTnQtChungTuChiTietHD4554();
                    hd.TenDonVi = tenSLNS.MoTa;
                    headers.Add(hd);
                }
                var itemsResult = itemsPage.GroupBy(x => x.IIdMaDonVi).Select(s => new ReportTnQtChungTuChiTietHD4554Query()
                {
                    IIDMaDonVi = s.FirstOrDefault().IIdMaDonVi,
                    STenDonVi = s.FirstOrDefault().STenDonVi,
                    FTongSoTien = s.Sum(x => x.FSoTien),
                    LstGiaTri = ParseDataGroup(s.ToList(), sLNSPage)
                }).ToList();
                results = itemsResult.OrderBy(x => x.IIDMaDonVi).ToList();

                foreach (var item in results.Where(x => x.HasData))
                {
                    item.STT = inumber;
                    inumber++;
                }
                List<ReportTnQtChungTuChiTietHD4554Query> resultsTotal = new List<ReportTnQtChungTuChiTietHD4554Query>();
                ReportTnQtChungTuChiTietHD4554Query total = new ReportTnQtChungTuChiTietHD4554Query();
                total.LstTong = new List<ReportChildTnQtChungTuChiTietHD4554Query>();
                foreach (NsMuclucNgansachModel tenSLNS in sLNSPage)
                {
                    ReportChildTnQtChungTuChiTietHD4554Query giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
                    if (!string.IsNullOrEmpty(tenSLNS.Lns))
                    {
                        giaTri.FSoTien = _lstdata.Where(x => x.SLNS == tenSLNS.Lns).Sum(x => x.FSoTien);
                        total.LstTong.Add(giaTri);
                    }
                    else
                    {
                        total.LstTong.Add(giaTri);
                    }
                }
                total.FTongSoTien = total.LstTong.Sum(x => x.FSoTien);
                resultsTotal.Add(total);
                var tongSoTien = _lstdata.Where(x => !string.IsNullOrEmpty(x.SLNS)).Sum(x => x.FSoTien);
                Double? tongCong = results.Sum(x => x.FTongSoTien);
                string header1 = "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem;
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "TieuDe1", _txtTitleFirst + " "  + _sessionService.Current.YearOfWork},
                        { "ListData", results.Where(x=>x.HasData).ToList()},
                        { "ListTotal", resultsTotal },
                        { "Headers", headers },
                        { "HeadersSTT", headersSTT },
                        { "FormatNumber", formatNumber },
                        { "TieuDe2", _txtTitleSecond },
                        { "TieuDe3", _txtTitleThird },
                        { "Cap1",!string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1 },
                        { "Cap2",!string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionService.Current.TenDonVi },
                        { "Ngay", DateUtils.FormatDateReport(ReportDate) },
                        { "DiaDiem", _diaDiem },
                        { "Count", 1000000 },
                        {"TongCong",tongCong },
                        { "Header1", string.Format(header1, listSLNSplits.IndexOf(sLNSPage), listSLNSplits.Count) },
                        { "TienBangChu", StringUtils.NumberToText(tongSoTien.Value * dvt) }
                    };
                //AddChuKy(data);

                //List<GhiChu> ghiChu = GetGhiChu();
                if (InTheo == TimeOptionTypes.Months)
                {
                    data.Add("LabelTime", string.Format("Tháng {0} - năm {1}", TimeOptionSelected.ValueItem, _sessionService.Current.YearOfWork));
                }
                else if (InTheo == TimeOptionTypes.Quarter)
                {
                    data.Add("LabelTime", string.Format("{0} - năm {1}", TimeOptionSelected.DisplayItem, _sessionService.Current.YearOfWork));
                }
                else
                {
                    data.Add("LabelTime", string.Empty);
                }

                List<int> hideColumns = new List<int>();
                if (count == 1)
                {
                    hideColumns.Add(10);
                    hideColumns.Add(11);
                    hideColumns.Add(12);
                    hideColumns.Add(13);
                    hideColumns.Add(14);
                    hideColumns.Add(15);
                    data.Add("MergeRange", "D8:I8");
                    data.Add("To", string.Format("Tờ số {0}", count));
                }
                else
                {
                    hideColumns.Add(9);
                    hideColumns.Add(10);
                    hideColumns.Add(11);
                    hideColumns.Add(12);
                    hideColumns.Add(13);
                    hideColumns.Add(14);
                    data.Add("MergeRange", "C1:H1");
                    data.Add("To", string.Format("Tờ số {0}", count));
                }

                string templateFileName = GetPath(count == 1 ? RevenueExpenditureType.RPT_THUNOP_QUOCPHONG : RevenueExpenditureType.RPT_THUNOP_QUOCPHONG_TO2);
                string fileNamePrefix = count == 1 ? RevenueExpenditureType.RPT_THUNOP_QUOCPHONG + "_To1" : RevenueExpenditureType.RPT_THUNOP_QUOCPHONG_TO2;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportTnQtChungTuChiTietHD4554Query, HeaderReportTnQtChungTuChiTietHD4554>(templateFileName, data, hideColumns);
                exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                count++;
            }
        }

        private string GetLevelTitle(DmChuKy dmChuKy, int level)
        {
            if (dmChuKy is null) return string.Empty;
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

        private void HandlerDataExportOnePage(List<List<NsMuclucNgansachModel>> listSLNSplits, int size, int dvt, ExportType exportType, List<ExportResult> exportResults, int countSLNS, List<AgencyModel> lstDonVi)
        {
            int donViTinh = 1;
            int columnStartPage1 = 4;
            int columnEndPage1 = columnStartPage1 + (countSLNS > 1 ? countSLNS - 1 : 0);
            var ColNameStartPage1 = GetExcelColumnName(columnStartPage1);
            var ColNameEndPage1 = GetExcelColumnName(columnEndPage1);
            string sCap1 = GetLevelTitle(_dmChuKy, 1);
            string sCap2 = GetLevelTitle(_dmChuKy, 2);
            foreach (var sLNSPage in listSLNSplits)
            {
                List<ReportTnQtChungTuChiTietHD4554Query> results = new List<ReportTnQtChungTuChiTietHD4554Query>();
                List<HeaderReportTnQtChungTuChiTietHD4554> headers = new List<HeaderReportTnQtChungTuChiTietHD4554>();
                List<HeaderReportTnQtChungTuChiTietHD4554> headersSTT = new List<HeaderReportTnQtChungTuChiTietHD4554>();
                var itemsClone = _lstdata.Clone();
                List<TnQtChungTuChiTietHD4554Query> itemsPage = new List<TnQtChungTuChiTietHD4554Query>();
                int inumber = 1;
                // Tờ 1 lấy hết dữ liệu (chỉ cần cột tổng cộng có dữ liệu)
                if (listSLNSplits.IndexOf(sLNSPage) == 0)
                {
                    itemsPage = itemsClone.Where(x => !string.IsNullOrEmpty(x.SLNS)).ToList();

                }
                else
                {
                    itemsPage = itemsClone.Where(x => !string.IsNullOrEmpty(x.SLNS) && sLNSPage.Select(x => x.Lns).Contains(x.SLNS)).ToList();
                    if (!CheckDonVi(lstDonVi, itemsPage))
                    {
                        itemsPage = OnAddDonVi(itemsPage, lstDonVi, sLNSPage);
                    }
                }

                if (sLNSPage.Count < size)
                {
                    int countEmpty = size - sLNSPage.Count;
                    for (int j = 0; j < countEmpty; j++)
                    {
                        NsMuclucNgansachModel emptyCb = new NsMuclucNgansachModel();
                        sLNSPage.Add(emptyCb);
                    }
                }

                if (listSLNSplits.IndexOf(sLNSPage) == 0)
                {
                    HeaderReportTnQtChungTuChiTietHD4554 hdSTT = new HeaderReportTnQtChungTuChiTietHD4554();
                    hdSTT.STT = string.Format("(1)=(2)+..+({0})", countSLNS + 1);
                    headersSTT.Add(hdSTT);
                    for (int i = 0; i < sLNSPage.Count; i++)
                    {
                        hdSTT = new HeaderReportTnQtChungTuChiTietHD4554();
                        hdSTT.STT = string.Format("({0})", i + 2);
                        headersSTT.Add(hdSTT);
                    }
                }
                else
                {
                    HeaderReportTnQtChungTuChiTietHD4554 hdSTT = new HeaderReportTnQtChungTuChiTietHD4554();
                    hdSTT.STT = string.Format("'(1)=(2)+..+({0})", countSLNS + 7);
                    headersSTT.Add(hdSTT);
                    for (int i = 0; i < countSLNS; i++)
                    {
                        hdSTT = new HeaderReportTnQtChungTuChiTietHD4554();
                        hdSTT.STT = string.Format("({0})", i + 8);
                        headersSTT.Add(hdSTT);
                    }
                }

                foreach (var tenSLNS in sLNSPage.Select((value, index) => new { index, value }))
                {
                    HeaderReportTnQtChungTuChiTietHD4554 hd = new HeaderReportTnQtChungTuChiTietHD4554();
                    if (tenSLNS.index == NSConstants.ZERO)
                    {
                        hd.STT = "1";
                        hd.TenDonVi = tenSLNS.value.MoTa;
                        hd.SMoTa = "Loại hình hoạt động";
                        hd.MergeRange = string.Format("D8:{0}8", ColNameEndPage1);
                        headers.Add(hd);
                    }
                    else
                    {
                        hd.TenDonVi = tenSLNS.value.MoTa;
                        headers.Add(hd);
                    }
                }
                var itemsResult = itemsPage.GroupBy(x => x.IIdMaDonVi).Select(s => new ReportTnQtChungTuChiTietHD4554Query()
                {
                    IIDMaDonVi = s.FirstOrDefault().IIdMaDonVi,
                    STenDonVi = s.FirstOrDefault().STenDonVi,
                    FTongSoTien = s.Sum(x => x.FSoTien),
                    LstGiaTri = ParseDataGroup(s.ToList(), sLNSPage)
                }).ToList();
                results = itemsResult.OrderBy(x => x.IIDMaDonVi).ToList();

                foreach (var item in results)
                {
                    item.STT = inumber;
                    inumber++;
                }
                List<ReportTnQtChungTuChiTietHD4554Query> resultsTotal = new List<ReportTnQtChungTuChiTietHD4554Query>();
                ReportTnQtChungTuChiTietHD4554Query total = new ReportTnQtChungTuChiTietHD4554Query();
                total.LstTong = new List<ReportChildTnQtChungTuChiTietHD4554Query>();
                foreach (NsMuclucNgansachModel tenSLNS in sLNSPage)
                {
                    ReportChildTnQtChungTuChiTietHD4554Query giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
                    if (!string.IsNullOrEmpty(tenSLNS.Lns))
                    {
                        giaTri.FSoTien = _lstdata.Where(x => x.SLNS == tenSLNS.Lns).Sum(x => x.FSoTien);
                        total.LstTong.Add(giaTri);
                    }
                    else
                    {
                        total.LstTong.Add(giaTri);
                    }
                }
                total.FTongSoTien = total.LstTong.Sum(x => x.FSoTien);
                resultsTotal.Add(total);
                var tongSoTien = _lstdata.Where(x => !string.IsNullOrEmpty(x.SLNS)).Sum(x => x.FSoTien);
                Double? tongCong = results.Sum(x => x.FTongSoTien);
                string header1 = "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem;
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "TieuDe1", _txtTitleFirst + " "  + _sessionService.Current.YearOfWork},
                        { "ListData", results },
                        { "ListTotal", resultsTotal },
                        { "Headers", headers },
                        { "HeadersSTT", headersSTT },
                        { "FormatNumber", formatNumber },
                        { "TieuDe2", _txtTitleSecond },
                        { "TieuDe3", _txtTitleThird },
                        { "Cap1",!string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1 },
                        { "Cap2",!string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionService.Current.TenDonVi },
                        { "Ngay", DateUtils.FormatDateReport(ReportDate) },
                        { "DiaDiem", _diaDiem },
                        { "Count", 1000000 },
                        {"TongCong",tongCong },
                        { "Header1", string.Format(header1, listSLNSplits.IndexOf(sLNSPage), listSLNSplits.Count) },
                        { "TienBangChu", StringUtils.NumberToText(tongSoTien.Value * dvt) }
                    };
                //AddChuKy(data);

                //List<GhiChu> ghiChu = GetGhiChu();
                if (InTheo == TimeOptionTypes.Months)
                {
                    data.Add("LabelTime", string.Format("Tháng {0} - năm {1}", TimeOptionSelected.ValueItem, _sessionService.Current.YearOfWork));
                }
                else if (InTheo == TimeOptionTypes.Quarter)
                {
                    data.Add("LabelTime", string.Format("{0} - năm {1}", TimeOptionSelected.DisplayItem, _sessionService.Current.YearOfWork));
                }
                else
                {
                    data.Add("LabelTime", string.Empty);
                }

                List<int> hideColumns = new List<int>();

                if (listSLNSplits.IndexOf(sLNSPage) != 0)
                {
                    hideColumns.Add(3);
                }

                string templateFileName = GetPath(RevenueExpenditureType.RPT_THUNOP_QUOCPHONG_ONEPAGE);
                string fileNamePrefix = RevenueExpenditureType.RPT_THUNOP_QUOCPHONG_ONEPAGE;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportTnQtChungTuChiTietHD4554Query, HeaderReportTnQtChungTuChiTietHD4554>(templateFileName, data, hideColumns);
                exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
            }
        }

        private static string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }

        private bool CheckDonVi(List<AgencyModel> lstDonVi, List<TnQtChungTuChiTietHD4554Query> itemsPage)
        {
            var lstDonViOfItemspage = itemsPage.Select(x => x.IIdMaDonVi).Distinct().ToList();
            var lstIDDonVi = lstDonVi.Select(x => x.IIDMaDonVi).ToList();
            return lstIDDonVi.Count == lstDonViOfItemspage.Count;
        }

        private List<TnQtChungTuChiTietHD4554Query> OnAddDonVi(List<TnQtChungTuChiTietHD4554Query> itemsPage, List<AgencyModel> lstDonVi, List<NsMuclucNgansachModel> sLNSPage)
        {
            var lstDonViOfItemspage = itemsPage.Select(x => x.IIdMaDonVi).Distinct().ToList();
            var lstIDDonVi = lstDonVi.Select(x => x.IIDMaDonVi).ToList();
            var lstExcept = lstIDDonVi.Except(lstDonViOfItemspage);
            var lstDonViExcept = lstDonVi.Where(x => lstExcept.Contains(x.IIDMaDonVi)).ToList();
            foreach (var dv in lstDonViExcept)
            {
                foreach (var item in sLNSPage.Where(x => x.STenDonVi != "ALL"))
                {
                    TnQtChungTuChiTietHD4554Query chungTuChiTiet = new TnQtChungTuChiTietHD4554Query();
                    chungTuChiTiet.IIdMaDonVi = dv.IIDMaDonVi;
                    chungTuChiTiet.STenDonVi = dv.TenDonVi;
                    chungTuChiTiet.FSoTien = 0;
                    chungTuChiTiet.SLNS = item.Lns;
                    chungTuChiTiet.SL = item.L;
                    chungTuChiTiet.SK = item.K;
                    chungTuChiTiet.SM = item.M;
                    chungTuChiTiet.SNG = item.NG;
                    chungTuChiTiet.STM = item.TM;
                    chungTuChiTiet.STTM = item.TTM;

                    itemsPage.Add(chungTuChiTiet);
                }
            }

            return itemsPage;
        }

        private void HandlerDataExportCaoNganSachNhaNuocOnePage(List<List<NsMuclucNgansachModel>> listSLNSplits, int size, int dvt, ExportType exportType, List<ExportResult> exportResults, int countSLNS, List<AgencyModel> lstIdDonVi, int countSLNSNopBQP = 0, List<NsMuclucNgansachModel> lstSLSNNopBQP = null, int countSLNSNopDP = 0)
        {
            int Count = 1;
            int donViTinh = 1;
            string sCap1 = GetLevelTitle(_dmChuKy, 1);
            string sCap2 = GetLevelTitle(_dmChuKy, 2);
            foreach (var sLNSPage in listSLNSplits)
            {
                List<ReportTnQtChungTuChiTietHD4554Query> results = new List<ReportTnQtChungTuChiTietHD4554Query>();
                List<ReportTnQtChungTuChiTietHD4554Query> lstResults = new List<ReportTnQtChungTuChiTietHD4554Query>();
                List<HeaderReportTnQtChungTuChiTietHD4554> headers = new List<HeaderReportTnQtChungTuChiTietHD4554>();
                List<HeaderReportTnQtChungTuChiTietHD4554> headersSTT = new List<HeaderReportTnQtChungTuChiTietHD4554>();
                List<int> hideCollum = new List<int>();
                var itemsClone = _lstdata.Clone();
                List<TnQtChungTuChiTietHD4554Query> itemsPage = new List<TnQtChungTuChiTietHD4554Query>();
                int inumberSTT = 1;

                // Tờ 1 lấy hết dữ liệu (chỉ cần cột tổng cộng có dữ liệu)
                if (listSLNSplits.IndexOf(sLNSPage) == 0)
                {
                    itemsPage = itemsClone.Where(x => !string.IsNullOrEmpty(x.SLNS)).ToList();
                    if (!CheckDonVi(lstIdDonVi, itemsPage))
                    {
                        itemsPage = OnAddDonVi(itemsPage, lstIdDonVi, sLNSPage);
                    }

                }

                if (listSLNSplits.IndexOf(sLNSPage) == 0)
                {
                    HeaderReportTnQtChungTuChiTietHD4554 hdSTT = new HeaderReportTnQtChungTuChiTietHD4554();
                    if (countSLNSNopDP > 0)
                    {
                        // STT Tong Phan Thu nhap
                        hdSTT.STT = string.Format("(1)=(2)+..+({0})", countSLNSNopDP + 3);
                        headersSTT.Add(hdSTT);

                        // STT Tông số
                        hdSTT = new HeaderReportTnQtChungTuChiTietHD4554
                        {
                            STT = string.Format("(2)=(3)+..+({0})", countSLNSNopDP + 2)
                        };
                        headersSTT.Add(hdSTT);

                    }
                    else
                    {
                        // STT Tong Phan Thu nhap
                        hdSTT.STT = string.Format("(1)=({0})", countSLNSNopDP + 2);
                        headersSTT.Add(hdSTT);
                        // STT Tông số
                        hdSTT = new HeaderReportTnQtChungTuChiTietHD4554
                        {
                            STT = string.Format("(2)=(3)+..+({0})", countSLNSNopDP + 2)
                        };
                        headersSTT.Add(hdSTT);

                        hideCollum.Add(4);
                    }

                    headersSTT = AddHeaderSLNSOnePageNganSachNhaNuoc(countSLNSNopBQP, countSLNSNopDP, sLNSPage, headersSTT, hdSTT);


                    // Add ten cot muc luc ngan sach
                    AddHeaderOnePageNganSachNhaNuoc(sLNSPage, headers, countSLNSNopDP, countSLNSNopBQP);
                }

                results = GetResultsOnePageNganSachNhaNuoc(sLNSPage, itemsPage, Count, lstSLSNNopBQP, lstIdDonVi);


                foreach (var item in results.Where(x => !x.BHangCha))
                {
                    item.STT = inumberSTT;
                    inumberSTT++;
                }

                List<ReportTnQtChungTuChiTietHD4554Query> resultsTotal = new List<ReportTnQtChungTuChiTietHD4554Query>();
                ReportTnQtChungTuChiTietHD4554Query total = new ReportTnQtChungTuChiTietHD4554Query();
                total.LstTong = new List<ReportChildTnQtChungTuChiTietHD4554Query>();
                foreach (NsMuclucNgansachModel tenSLNS in sLNSPage)
                {
                    ReportChildTnQtChungTuChiTietHD4554Query giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
                    if (!string.IsNullOrEmpty(tenSLNS.Lns))
                    {
                        giaTri.FSoTien = _lstdata.Where(x => x.SLNS == tenSLNS.Lns).Sum(x => x.FSoTien);
                        total.LstTong.Add(giaTri);
                    }
                    else
                    {
                        total.LstTong.Add(giaTri);
                    }
                }
                total.FTongSoTien = total.LstTong.Sum(x => x.FSoTien);
                resultsTotal.Add(total);
                var tongSoTien = _lstdata.Where(x => !string.IsNullOrEmpty(x.SLNS)).Sum(x => x.FSoTien);

                double? tongPhanThuNop = results.Where(x => !x.BHangCha).Sum(x => x.FTongSoTien);

                string header1 = "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem;
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "TieuDe1", _txtTitleFirst + " "  + _sessionService.Current.YearOfWork},
                        { "ListData", results.Where(x => !x.BHangCha) },
                        { "ListTotal", resultsTotal },
                        { "Headers", headers },
                        { "HeadersSTT", headersSTT },
                        { "FormatNumber", formatNumber },
                        { "TieuDe2", _txtTitleSecond },
                        { "TieuDe3", _txtTitleThird },
                        { "Cap1",!string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1 },
                        { "Cap2",!string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionService.Current.TenDonVi },
                        { "Ngay", DateUtils.FormatDateReport(ReportDate) },
                        { "DiaDiem", _diaDiem },
                        { "TongPhanThuNop", tongPhanThuNop },
                        { "Count", 1000000 },
                        { "Header1", string.Format(header1, listSLNSplits.IndexOf(sLNSPage), listSLNSplits.Count) },
                        { "TienBangChu", StringUtils.NumberToText(tongSoTien.Value * dvt) }
                    };

                if (InTheo == TimeOptionTypes.Months)
                {
                    data.Add("LabelTime", string.Format("Tháng {0} - năm {1}", TimeOptionSelected.ValueItem, _sessionService.Current.YearOfWork));
                }
                else if (InTheo == TimeOptionTypes.Quarter)
                {
                    data.Add("LabelTime", string.Format("{0} - năm {1}", TimeOptionSelected.DisplayItem, _sessionService.Current.YearOfWork));
                }
                else
                {
                    data.Add("LabelTime", string.Empty);
                }
                string templateFileName = GetPath(RevenueExpenditureType.RPT_THUNOP_NGANSACH_NHANUOC_ONEPAGE);
                string fileNamePrefix = RevenueExpenditureType.RPT_THUNOP_NGANSACH_NHANUOC_ONEPAGE;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportTnQtChungTuChiTietHD4554Query, HeaderReportTnQtChungTuChiTietHD4554>(templateFileName, data, hideCollum);
                exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                Count++;
            }
        }

        private void HandlerDataExportCaoNganSachNhaNuocOnePage2(List<List<NsMuclucNgansachModel>> listSLNSplits, int dvt, ExportType exportType, List<ExportResult> exportResults, List<AgencyModel> lstIdDonVi, int countSLNSNopBQP = 0, List<NsMuclucNgansachModel> lstSLSNNopBQP = null, int countSLNSNopDP = 0)
        {
            int Count = 1;
            int donViTinh = 1;
            string sCap1 = GetLevelTitle(_dmChuKy, 1);
            string sCap2 = GetLevelTitle(_dmChuKy, 2);
            int colStart = 4;
            foreach (var sLNSPage in listSLNSplits)
            {
                List<ReportTnQtChungTuChiTietHD4554Query> results = new List<ReportTnQtChungTuChiTietHD4554Query>();
                List<ReportTnQtChungTuChiTietHD4554Query> lstResults = new List<ReportTnQtChungTuChiTietHD4554Query>();
                List<HeaderReportTnQtChungTuChiTietHD4554> headers = new List<HeaderReportTnQtChungTuChiTietHD4554>();
                List<HeaderReportTnQtChungTuChiTietHD4554> headersLstGiaTri = new List<HeaderReportTnQtChungTuChiTietHD4554>();
                List<HeaderReportTnQtChungTuChiTietHD4554> headersSTT = new List<HeaderReportTnQtChungTuChiTietHD4554>();
                List<int> hideCollum = new List<int>();
                var itemsClone = _lstdata.Clone();
                List<TnQtChungTuChiTietHD4554Query> itemsPage = new List<TnQtChungTuChiTietHD4554Query>();
                int inumberSTT = 1;

                // Tờ 1 lấy hết dữ liệu (chỉ cần cột tổng cộng có dữ liệu)
                if (listSLNSplits.IndexOf(sLNSPage) == 0)
                {
                    itemsPage = itemsClone.Where(x => !string.IsNullOrEmpty(x.SLNS)).ToList();
                    if (!CheckDonVi(lstIdDonVi, itemsPage))
                    {
                        itemsPage = OnAddDonVi(itemsPage, lstIdDonVi, sLNSPage);
                    }

                }

                // Add ten cot muc luc ngan sach
                AddHeaderOnePageNganSachNhaNuoc2(sLNSPage, headers, headersLstGiaTri, countSLNSNopDP, countSLNSNopBQP);
                results = GetResultsOnePageNganSachNhaNuoc2(sLNSPage, itemsPage, Count, lstSLSNNopBQP, lstIdDonVi);

                foreach (var item in results.Where(x => !x.BHangCha))
                {
                    item.STT = inumberSTT;
                    inumberSTT++;
                }

                List<ReportTnQtChungTuChiTietHD4554Query> resultsTotal = new List<ReportTnQtChungTuChiTietHD4554Query>();
                ReportTnQtChungTuChiTietHD4554Query total = new ReportTnQtChungTuChiTietHD4554Query();
                total.LstTong = new List<ReportChildTnQtChungTuChiTietHD4554Query>();
                ReportChildTnQtChungTuChiTietHD4554Query giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
                total.LstTong.Add(giaTri);
                if (countSLNSNopDP == 0)
                {
                    giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
                    total.LstTong.Add(giaTri);
                }
                foreach (NsMuclucNgansachModel tenSLNS in sLNSPage)
                {
                    giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
                    total.LstTong.Add(giaTri);

                }
                total.FTongSoTien = total.LstTong.Sum(x => x.FSoTien);
                resultsTotal.Add(total);
                var tongSoTien = _lstdata.Where(x => !string.IsNullOrEmpty(x.SLNS)).Sum(x => x.FSoTien);

                double? tongPhanThuNop = results.Where(x => !x.BHangCha).Sum(x => x.FTongSoTien);

                string header1 = "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem;
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "TieuDe1", _txtTitleFirst + " "  + _sessionService.Current.YearOfWork},
                        { "ListData", results.Where(x => !x.BHangCha) },
                        { "ListTotal", resultsTotal },
                        { "Headers", headers },
                        { "HeadersLstGiaTriMerge","Nộp NSNN tại BQP"},
                        { "HeadersMerge","Nộp NSNN qua địa phương"},
                        { "HeadersLstGiaTri", headersLstGiaTri },
                        { "HeadersSTT", headersSTT },
                        { "FormatNumber", formatNumber },
                        { "TieuDe2", _txtTitleSecond },
                        { "TieuDe3", _txtTitleThird },
                        { "Cap1",!string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1 },
                        { "Cap2",!string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionService.Current.TenDonVi },
                        { "Ngay", DateUtils.FormatDateReport(ReportDate) },
                        { "DiaDiem", _diaDiem },
                        { "TongPhanThuNop", tongPhanThuNop },
                        { "Count", 1000000 },
                        { "Header1", string.Format(header1, listSLNSplits.IndexOf(sLNSPage), listSLNSplits.Count) },
                        { "TienBangChu", StringUtils.NumberToText(tongSoTien.Value * dvt) }
                    };

                if (InTheo == TimeOptionTypes.Months)
                {
                    data.Add("LabelTime", string.Format("Tháng {0} - năm {1}", TimeOptionSelected.ValueItem, _sessionService.Current.YearOfWork));
                }
                else if (InTheo == TimeOptionTypes.Quarter)
                {
                    data.Add("LabelTime", string.Format("{0} - năm {1}", TimeOptionSelected.DisplayItem, _sessionService.Current.YearOfWork));
                }
                else
                {
                    data.Add("LabelTime", string.Empty);
                }
                string templateFileName = GetPath(RevenueExpenditureType.RPT_THUNOP_NGANSACH_NHANUOC_ONEPAGE);
                string fileNamePrefix = RevenueExpenditureType.RPT_THUNOP_NGANSACH_NHANUOC_ONEPAGE;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                if (countSLNSNopDP == 0)
                {
                    hideCollum.Add(colStart);
                    hideCollum.Add(colStart + 1);
                }
                else
                {
                    hideCollum.Add(countSLNSNopDP + colStart + 1);
                    if (countSLNSNopBQP == 0)
                    {
                        hideCollum.Add(countSLNSNopDP + colStart + 2);
                    }
                }
                FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportTnQtChungTuChiTietHD4554Query, HeaderReportTnQtChungTuChiTietHD4554>(templateFileName, data, hideCollum);
                exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                Count++;
            }
        }

        private static List<HeaderReportTnQtChungTuChiTietHD4554> AddHeaderSLNSOnePageNganSachNhaNuoc(int countSLNSNopBQP, int countSLNSNopDP, List<NsMuclucNgansachModel> sLNSPage, List<HeaderReportTnQtChungTuChiTietHD4554> headersSTT, HeaderReportTnQtChungTuChiTietHD4554 hdSTT)
        {
            foreach (var item in sLNSPage.Select((value, index) => new { index, value }))
            {
                int offset = countSLNSNopDP == 0 ? 2 : 3;
                int currentIndex = item.index + offset;
                if (item.index == countSLNSNopDP && countSLNSNopBQP > 0)
                {
                    string sttValue = countSLNSNopBQP == 1
                        ? string.Format("({0})=({1})", currentIndex, currentIndex + 1)
                        : string.Format("({0})=({1})+..+({2})", currentIndex, currentIndex + 1, sLNSPage.Count + offset - 1);
                    hdSTT = new HeaderReportTnQtChungTuChiTietHD4554 { STT = sttValue };
                }
                else
                {
                    hdSTT = new HeaderReportTnQtChungTuChiTietHD4554
                    {
                        STT = string.Format("({0})", currentIndex)
                    };
                }
                headersSTT.Add(hdSTT);
            }

            // chua toi uu
            //foreach (var item in sLNSPage.Select((value, index) => new { index, value }))
            //{
            //    if (countSLNSNopDP == 0)
            //    {
            //        if (item.index == countSLNSNopDP)
            //        {
            //            if (countSLNSNopBQP == 1)
            //            {
            //                hdSTT = new HeaderReportTnQtChungTuChiTietHD4554
            //                {
            //                    STT = string.Format("({0})=({1})", countSLNSNopDP + 2, item.index + 3)
            //                };
            //                headersSTT.Add(hdSTT);
            //            }
            //            else
            //            {
            //                hdSTT = new HeaderReportTnQtChungTuChiTietHD4554
            //                {
            //                    STT = string.Format("({0})=({1})+..+({2})", countSLNSNopDP + 2, item.index + 3, sLNSPage.Count + 2)
            //                };
            //                headersSTT.Add(hdSTT);
            //            }
            //        }
            //        else
            //        {
            //            hdSTT = new HeaderReportTnQtChungTuChiTietHD4554
            //            {
            //                STT = string.Format("({0})", item.index + 2)
            //            };
            //            headersSTT.Add(hdSTT);
            //        }
            //    }
            //    else
            //    {
            //        if (item.index == countSLNSNopDP && countSLNSNopBQP > 0)
            //        {

            //            if (countSLNSNopBQP == 1)
            //            {
            //                hdSTT = new HeaderReportTnQtChungTuChiTietHD4554
            //                {
            //                    STT = string.Format("({0})=({1})", countSLNSNopDP + 3, item.index + 4)
            //                };
            //                headersSTT.Add(hdSTT);
            //            }
            //            else
            //            {
            //                hdSTT = new HeaderReportTnQtChungTuChiTietHD4554
            //                {
            //                    STT = string.Format("({0})=({1})+..+({2})", countSLNSNopDP + 3, item.index + 4, sLNSPage.Count + 2)
            //                };
            //                headersSTT.Add(hdSTT);
            //            }

            //        }
            //        else
            //        {
            //            hdSTT = new HeaderReportTnQtChungTuChiTietHD4554
            //            {
            //                STT = string.Format("({0})", item.index + 3)
            //            };
            //            headersSTT.Add(hdSTT);
            //        }
            //    }

            //}

            return headersSTT;
        }

        private void HandlerDataExportCaoNganSachNhaNuoc(List<List<NsMuclucNgansachModel>> listSLNSplits, int size, int dvt, ExportType exportType, List<ExportResult> exportResults, int countSLNS, List<AgencyModel> lstIdDonVi, int countSLNSNopBQP = 0, List<NsMuclucNgansachModel> lstSLSNNopBQP = null)
        {
            int Count = 1;
            int donViTinh = 1;
            string sCap1 = GetLevelTitle(_dmChuKy, 1);
            string sCap2 = GetLevelTitle(_dmChuKy, 2);
            foreach (var sLNSPage in listSLNSplits)
            {
                List<ReportTnQtChungTuChiTietHD4554Query> results = new List<ReportTnQtChungTuChiTietHD4554Query>();
                List<ReportTnQtChungTuChiTietHD4554Query> lstResults = new List<ReportTnQtChungTuChiTietHD4554Query>();
                List<ReportTnQtChungTuChiTietHD4554Query> resultsDuToan = new List<ReportTnQtChungTuChiTietHD4554Query>();
                List<ReportTnQtChungTuChiTietHD4554Query> resultsHachToan = new List<ReportTnQtChungTuChiTietHD4554Query>();
                List<HeaderReportTnQtChungTuChiTietHD4554> headers = new List<HeaderReportTnQtChungTuChiTietHD4554>();
                List<HeaderReportTnQtChungTuChiTietHD4554> headersSTT = new List<HeaderReportTnQtChungTuChiTietHD4554>();
                string sNameFile = string.Empty;
                string sNamFileExport = string.Empty;
                var itemsClone = _lstdata.Clone();
                List<TnQtChungTuChiTietHD4554Query> itemsPage = new List<TnQtChungTuChiTietHD4554Query>();
                int inumberSTT = 1;
                // Tờ 1 lấy hết dữ liệu (chỉ cần cột tổng cộng có dữ liệu)
                if (listSLNSplits.IndexOf(sLNSPage) == 0)
                {
                    itemsPage = itemsClone.Where(x => !string.IsNullOrEmpty(x.SLNS)).ToList();
                    if (!CheckDonVi(lstIdDonVi, itemsPage))
                    {
                        itemsPage = OnAddDonVi(itemsPage, lstIdDonVi, sLNSPage);
                    }

                }
                else
                {
                    itemsPage = itemsClone.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi) && sLNSPage.Select(x => x.Lns).Contains(x.SLNS)).ToList();
                    if (!CheckDonVi(lstIdDonVi, itemsPage))
                    {
                        itemsPage = OnAddDonVi(itemsPage, lstIdDonVi, sLNSPage);
                    }
                }

                if (sLNSPage.Count < size)
                {
                    int countEmpty = size - sLNSPage.Count;
                    for (int j = 0; j < countEmpty; j++)
                    {
                        NsMuclucNgansachModel emptyCb = new NsMuclucNgansachModel();
                        sLNSPage.Add(emptyCb);
                    }
                }
                if (listSLNSplits.IndexOf(sLNSPage) == 0)
                {
                    HeaderReportTnQtChungTuChiTietHD4554 hdSTT = new HeaderReportTnQtChungTuChiTietHD4554();
                    // check số tờ
                    if (listSLNSplits.Count > 1)
                        hdSTT.STT = string.Format("(1)=(2)+..+(12)");
                    else hdSTT.STT = string.Format("(1)=(2)+..+({0})", countSLNS + 1);

                    headersSTT.Add(hdSTT);

                    hdSTT = new HeaderReportTnQtChungTuChiTietHD4554();
                    hdSTT.STT = string.Format("(2)=(3)+..+({0})", sLNSPage.Count() + 2);
                    headersSTT.Add(hdSTT);

                    for (int i = 0; i < sLNSPage.Count(); i++)
                    {
                        hdSTT = new HeaderReportTnQtChungTuChiTietHD4554
                        {
                            STT = string.Format("({0})", i + 3)
                        };
                        headersSTT.Add(hdSTT);
                    }

                    // Add ten cot muc luc ngan sach
                    AddHeader(sLNSPage, headers);
                }
                else
                {
                    HeaderReportTnQtChungTuChiTietHD4554 hdSTT = new HeaderReportTnQtChungTuChiTietHD4554();
                    hdSTT.STT = string.Format("'(1)=(2)+..+({0})", countSLNS + 12);
                    headersSTT.Add(hdSTT);
                    hdSTT = new HeaderReportTnQtChungTuChiTietHD4554();
                    hdSTT.STT = string.Format("(12)=(13)+..+({0})", countSLNSNopBQP + 12);
                    headersSTT.Add(hdSTT);

                    for (int i = 0; i < sLNSPage.Count; i++)
                    {
                        hdSTT = new HeaderReportTnQtChungTuChiTietHD4554();
                        if (Count > 2)
                            hdSTT.STT = string.Format("({0})", i + 23);
                        else
                            hdSTT.STT = string.Format("({0})", i + 13);
                        headersSTT.Add(hdSTT);
                    }
                    AddHeader(sLNSPage, headers);
                }

                results = GetResults(sLNSPage, itemsPage, Count, lstSLSNNopBQP, lstIdDonVi);

                // Set STT
                //foreach (var item in results)
                //{
                //    var bKhoiHachToan = lstIdDonVi.Where(x => item.IIDMaDonVi.Contains(x.IIDMaDonVi) && x.Loai == "2").FirstOrDefault();
                //    if (bKhoiHachToan != null)
                //    {
                //        item.STT = inumberKhoiHachToan;
                //        item.STenDonVi = "       " + item.STenDonVi;
                //        inumberKhoiHachToan++;
                //        resultsHachToan.Add(item);
                //    }
                //    else
                //    {
                //        item.STT = inumberKhoiDuToan;
                //        item.STenDonVi = "       " + item.STenDonVi;
                //        inumberKhoiDuToan++;
                //        resultsDuToan.Add(item);
                //    }
                //}

                //lstResults = GetListDonViTheoKhoi(resultsDuToan, resultsHachToan, sLNSPage, lstIdDonVi, Count);
                foreach (var item in results.Where(x => !x.BHangCha && x.HasData))
                {
                    item.STT = inumberSTT;
                    inumberSTT++;
                }

                List<ReportTnQtChungTuChiTietHD4554Query> resultsTotal = new List<ReportTnQtChungTuChiTietHD4554Query>();
                ReportTnQtChungTuChiTietHD4554Query total = new ReportTnQtChungTuChiTietHD4554Query();
                total.LstTong = new List<ReportChildTnQtChungTuChiTietHD4554Query>();
                foreach (NsMuclucNgansachModel tenSLNS in sLNSPage)
                {
                    ReportChildTnQtChungTuChiTietHD4554Query giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
                    if (!string.IsNullOrEmpty(tenSLNS.Lns))
                    {
                        giaTri.FSoTien = _lstdata.Where(x => x.SLNS == tenSLNS.Lns).Sum(x => x.FSoTien);
                        total.LstTong.Add(giaTri);
                    }
                    else
                    {
                        total.LstTong.Add(giaTri);
                    }
                }
                total.FTongSoTien = total.LstTong.Sum(x => x.FSoTien);
                resultsTotal.Add(total);
                var tongSoTien = _lstdata.Where(x => !string.IsNullOrEmpty(x.SLNS)).Sum(x => x.FSoTien);

                double? tongPhanThuNop = results.Where(x => !x.BHangCha).Sum(x => x.FTongSoTien);
                //double?  tongSo = results[0].LstTongTien[0].FSoTien;

                string header1 = "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem;
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "TieuDe1", _txtTitleFirst + " "  + _sessionService.Current.YearOfWork},
                        { "ListData", results.Where(x => !x.BHangCha && x.HasData) },
                        { "ListTotal", resultsTotal },
                        { "Headers", headers },
                        { "HeadersSTT", headersSTT },
                        { "FormatNumber", formatNumber },
                        { "TieuDe2", _txtTitleSecond },
                        { "TieuDe3", _txtTitleThird },
                        { "Cap1",!string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1 },
                        { "Cap2",!string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionService.Current.TenDonVi },
                        { "Ngay", DateUtils.FormatDateReport(ReportDate) },
                        { "DiaDiem", _diaDiem },
                        { "TongPhanThuNop", tongPhanThuNop },
                        { "TongSo", 0 },
                        { "Count", 1000000},
                        { "To",string.Format("Tờ số {0}",Count)},
                        { "Header1", string.Format(header1, listSLNSplits.IndexOf(sLNSPage), listSLNSplits.Count) },
                        { "TienBangChu", StringUtils.NumberToText(tongSoTien.Value * dvt) }
                    };

                if (InTheo == TimeOptionTypes.Months)
                {
                    data.Add("LabelTime", string.Format("Tháng {0} - năm {1}", TimeOptionSelected.ValueItem, _sessionService.Current.YearOfWork));
                }
                else if (InTheo == TimeOptionTypes.Quarter)
                {
                    data.Add("LabelTime", string.Format("{0} - năm {1}", TimeOptionSelected.DisplayItem, _sessionService.Current.YearOfWork));
                }
                else
                {
                    data.Add("LabelTime", string.Empty);
                }

                List<int> hideColumns = new List<int>();
                if (Count == 1)
                {
                    data.Add("MergeRange", "D8:M8");
                    sNameFile = RevenueExpenditureType.RPT_THUNOP_NGANSACH_NHANUOC;
                    sNamFileExport = RevenueExpenditureType.RPT_THUNOP_NGANSACH_NHANUOC + "_To1";

                }
                else if (Count == 2)
                {
                    data.Add("MergeRange", "C1:M1");
                    sNameFile = RevenueExpenditureType.RPT_THUNOP_NGANSACH_NHANUOC_TO2;
                    sNamFileExport = RevenueExpenditureType.RPT_THUNOP_NGANSACH_NHANUOC_TO2;
                }
                else
                {
                    data.Add("MergeRange", "D1:L1");
                    sNameFile = RevenueExpenditureType.RPT_THUNOP_NGANSACH_NHANUOC_TO3;
                    sNamFileExport = RevenueExpenditureType.RPT_THUNOP_NGANSACH_NHANUOC_TO3;
                }

                if (listSLNSplits.IndexOf(sLNSPage) != 0)
                {
                    data.Add("HeadersMeger", "Nộp NSNN tại BQP");
                }
                else
                {
                    data.Add("HeadersMeger", "Nộp NSNN qua địa phương");
                }
                // An cot thua trong excel
                List<int> colHide = new List<int>();
                if (Count == 1)
                {
                    colHide = new List<int>()
                    {
                        14,15,16,17,18,19,20,21,22
                    };
                }
                else if (Count == 2)
                {
                    colHide = new List<int>()
                    {
                        14,15,16,17,18,19,20,21,22,23
                    };
                }
                else
                {
                    colHide = new List<int>()
                    {
                        13,14,15,16,17,18,19,20,21,22,23
                    };
                }
                hideColumns.AddRange(colHide);

                string templateFileName = GetPath(sNameFile);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(sNamFileExport);
                FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportTnQtChungTuChiTietHD4554Query, HeaderReportTnQtChungTuChiTietHD4554>(templateFileName, data, hideColumns);
                exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                Count++;
            }
        }

        private List<ReportTnQtChungTuChiTietHD4554Query> GetListDonViTheoKhoi(List<ReportTnQtChungTuChiTietHD4554Query> resultsDuToan, List<ReportTnQtChungTuChiTietHD4554Query> resultsHachToan, List<NsMuclucNgansachModel> sLNSPage, List<AgencyModel> lstDonvi, int Count = 1)
        {
            List<AgencyModel> lstDonViDuToan = lstDonvi.Where(x => x.Loai != "2").ToList();
            List<AgencyModel> lstDonViHachToan = lstDonvi.Where(x => x.Loai == "2").ToList();
            if (resultsDuToan.Any())
            {
                ReportTnQtChungTuChiTietHD4554Query item = new ReportTnQtChungTuChiTietHD4554Query();
                item.STenDonVi = RevenueExpenditureType.DONVI_DUTOAN;
                item.BHangCha = true;
                item.FTongSoTien = resultsDuToan.Sum(x => x.FTongSoTien);
                if (Count == 1)
                {
                    item.LstTongTien = SumLstTongTien(resultsDuToan, sLNSPage, lstDonViDuToan);
                }
                else
                {
                    item.LstTongTien = SumLstTongTienPage2(resultsDuToan, sLNSPage, lstDonViDuToan);
                }

                item.LstGiaTri = SumLstGiaTri(resultsDuToan, sLNSPage, lstDonViDuToan);
                resultsDuToan.Insert(0, item);
            }
            if (resultsHachToan.Any())
            {
                ReportTnQtChungTuChiTietHD4554Query item = new ReportTnQtChungTuChiTietHD4554Query();
                item.STenDonVi = RevenueExpenditureType.DONVI_HACHTOAN;
                item.BHangCha = true;
                item.FTongSoTien = resultsHachToan.Sum(x => x.FTongSoTien);
                if (Count == 1)
                {
                    item.LstTongTien = SumLstTongTien(resultsDuToan, sLNSPage, lstDonViHachToan);
                }
                else
                {
                    item.LstTongTien = SumLstTongTienPage2(resultsDuToan, sLNSPage, lstDonViHachToan);
                }
                item.LstGiaTri = SumLstGiaTri(resultsDuToan, sLNSPage, lstDonViHachToan);
                resultsHachToan.Insert(0, item);
            }

            return resultsDuToan.Union(resultsHachToan).ToList();
        }

        private List<ReportChildTnQtChungTuChiTietHD4554Query> SumLstTongTien(List<ReportTnQtChungTuChiTietHD4554Query> resultsDuToan, List<NsMuclucNgansachModel> sLNSPage, List<AgencyModel> lstDonvi)
        {

            List<ReportChildTnQtChungTuChiTietHD4554Query> LstTong = new List<ReportChildTnQtChungTuChiTietHD4554Query>();
            List<ReportChildTnQtChungTuChiTietHD4554Query> LstTongTemp = new List<ReportChildTnQtChungTuChiTietHD4554Query>();
            ReportChildTnQtChungTuChiTietHD4554Query giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();

            foreach (NsMuclucNgansachModel tenSLNS in sLNSPage)
            {
                giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
                if (!string.IsNullOrEmpty(tenSLNS.Lns))
                {
                    giaTri.FSoTien = _lstdata.Where(x => x.SLNS == tenSLNS.Lns && lstDonvi.Select(y => y.IIDMaDonVi).Contains(x.IIdMaDonVi)).Sum(x => x.FSoTien);
                    LstTongTemp.Add(giaTri);
                }
                else
                {
                    LstTongTemp.Add(giaTri);
                }
            }

            if (LstTongTemp != null)
            {
                giaTri.FSoTien = LstTongTemp.Sum(x => x.FSoTien);
                LstTong.Add(giaTri);
            }
            else
            {
                giaTri.FSoTien = 0;
                LstTong.Add(giaTri);
            }

            return LstTong;

        }

        private List<ReportChildTnQtChungTuChiTietHD4554Query> SumLstTongTienPage2(List<ReportTnQtChungTuChiTietHD4554Query> resultsDuToan, List<NsMuclucNgansachModel> sLNSPage, List<AgencyModel> lstDonvi)
        {

            List<ReportChildTnQtChungTuChiTietHD4554Query> LstTong = new List<ReportChildTnQtChungTuChiTietHD4554Query>();
            List<ReportChildTnQtChungTuChiTietHD4554Query> LstTongTemp = new List<ReportChildTnQtChungTuChiTietHD4554Query>();
            ReportChildTnQtChungTuChiTietHD4554Query giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
            foreach (var item in resultsDuToan)
            {
                foreach (var itemTongTien in item.LstTongTien)
                {
                    giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
                    giaTri.FSoTien = itemTongTien.FSoTien;
                    LstTongTemp.Add(giaTri);
                }
            }

            if (LstTongTemp != null)
            {
                giaTri.FSoTien = LstTongTemp.Sum(x => x.FSoTien);
                LstTong.Add(giaTri);
            }
            else
            {
                giaTri.FSoTien = 0;
                LstTong.Add(giaTri);
            }

            return LstTong;

        }

        private List<ReportChildTnQtChungTuChiTietHD4554Query> SumLstGiaTri(List<ReportTnQtChungTuChiTietHD4554Query> resultsDuToan, List<NsMuclucNgansachModel> sLNSPage, List<AgencyModel> lstDonvi)
        {

            List<ReportChildTnQtChungTuChiTietHD4554Query> LstGiaTri = new List<ReportChildTnQtChungTuChiTietHD4554Query>();
            foreach (NsMuclucNgansachModel tenSLNS in sLNSPage)
            {
                ReportChildTnQtChungTuChiTietHD4554Query giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
                if (!string.IsNullOrEmpty(tenSLNS.Lns))
                {
                    giaTri.FSoTien = _lstdata.Where(x => x.SLNS == tenSLNS.Lns && lstDonvi.Select(y => y.IIDMaDonVi).Contains(x.IIdMaDonVi)).Sum(x => x.FSoTien);
                    LstGiaTri.Add(giaTri);
                }
                else
                {
                    LstGiaTri.Add(giaTri);
                }
            }
            return LstGiaTri;

        }

        private List<ReportTnQtChungTuChiTietHD4554Query> GetResults(List<NsMuclucNgansachModel> sLNSPage, List<TnQtChungTuChiTietHD4554Query> itemsPage, int Count = 1, List<NsMuclucNgansachModel> lstSLSNNopBQP = null, List<AgencyModel> lstIdDonVi = null)
        {
            List<ReportTnQtChungTuChiTietHD4554Query> results;
            var itemsResult = itemsPage.GroupBy(x => x.IIdMaDonVi).Select(s => new ReportTnQtChungTuChiTietHD4554Query()
            {
                IIDMaDonVi = s.FirstOrDefault().IIdMaDonVi,
                STenDonVi = s.FirstOrDefault().STenDonVi,
                FTongSoTien = s.Sum(x => x.FSoTien),
                LstGiaTri = ParseDataGroup(s.ToList(), sLNSPage),
                LstTongTien = ParseDataGroupTong(s.ToList(), sLNSPage, Count, lstSLSNNopBQP, lstIdDonVi),
            }).ToList();
            results = itemsResult.OrderBy(x => x.IIDMaDonVi).ToList();
            return results;
        }

        private List<ReportTnQtChungTuChiTietHD4554Query> GetResultsOnePageNganSachNhaNuoc(List<NsMuclucNgansachModel> sLNSPage, List<TnQtChungTuChiTietHD4554Query> itemsPage, int Count = 1, List<NsMuclucNgansachModel> lstSLSNNopBQP = null, List<AgencyModel> lstIdDonVi = null)
        {
            List<ReportTnQtChungTuChiTietHD4554Query> results;
            var itemsResult = itemsPage.GroupBy(x => x.IIdMaDonVi).Select(s => new ReportTnQtChungTuChiTietHD4554Query()
            {
                IIDMaDonVi = s.FirstOrDefault().IIdMaDonVi,
                STenDonVi = s.FirstOrDefault().STenDonVi,
                FTongSoTien = s.Sum(x => x.FSoTien),
                LstGiaTri = ParseDataGroupNganSachNhaNuocOnePage(s.ToList(), sLNSPage, itemsPage),
                LstTongTien = ParseDataGroupTongNganSachNhaNuocOnePage(s.ToList(), sLNSPage, Count, lstSLSNNopBQP, lstIdDonVi),
            }).ToList();
            results = itemsResult.OrderBy(x => x.IIDMaDonVi).ToList();
            return results;
        }

        private List<ReportTnQtChungTuChiTietHD4554Query> GetResultsOnePageNganSachNhaNuoc2(List<NsMuclucNgansachModel> sLNSPage, List<TnQtChungTuChiTietHD4554Query> itemsPage, int Count = 1, List<NsMuclucNgansachModel> lstSLSNNopBQP = null, List<AgencyModel> lstIdDonVi = null)
        {
            List<ReportTnQtChungTuChiTietHD4554Query> results;
            var itemsResult = itemsPage.GroupBy(x => x.IIdMaDonVi).Select(s => new ReportTnQtChungTuChiTietHD4554Query()
            {
                IIDMaDonVi = s.FirstOrDefault().IIdMaDonVi,
                STenDonVi = s.FirstOrDefault().STenDonVi,
                FTongSoTien = s.Sum(x => x.FSoTien),
                LstGiaTri = ParseDataGroupNganSachNhaNuocOnePage2(s.ToList(), sLNSPage, itemsPage, true),
                LstTongTien = ParseDataGroupNganSachNhaNuocOnePage2(s.ToList(), sLNSPage, itemsPage, false),
            }).ToList();
            results = itemsResult.OrderBy(x => x.IIDMaDonVi).ToList();
            return results;
        }

        private static void AddHeaderOnePageNganSachNhaNuoc2(List<NsMuclucNgansachModel> sLNSPage, List<HeaderReportTnQtChungTuChiTietHD4554> headers, List<HeaderReportTnQtChungTuChiTietHD4554> headersLstGiaTri, int countNSNopDP = 0, int countNSBQP = 0)
        {
            int columnStartPage1 = 4;
            int columnStartNSNopDPDefault = 2;
            int columnStartNSNopBQPDefault = 2;
            int columnEndPageNSNopDP = columnStartPage1 + (countNSNopDP > 1 ? countNSNopDP : 0);
            int columnEndPagecountNSBQP = columnStartPage1 + (countNSBQP > 1 ? countNSBQP : 0);
            string ColNameEndPageNSNopDP = string.Empty;
            string ColNameEndPagecountNSBQP = string.Empty;

            if (countNSNopDP == 0)
            {
                if (countNSBQP == 1)
                {
                    // +2 vi cot tong cong  NSBQP
                    ColNameEndPagecountNSBQP = GetExcelColumnName(columnEndPagecountNSBQP + columnStartNSNopDPDefault + columnStartNSNopBQPDefault);
                }
                else
                {
                    // +1 vi cot tong cong  NSBQP
                    ColNameEndPagecountNSBQP = GetExcelColumnName(columnEndPagecountNSBQP + columnStartNSNopDPDefault + 1);
                }


                ColNameEndPageNSNopDP = GetExcelColumnName(columnEndPageNSNopDP);
            }
            else
            {
                if (countNSBQP >= 2)
                    ColNameEndPagecountNSBQP = GetExcelColumnName(columnEndPagecountNSBQP + 3);
                else
                    ColNameEndPagecountNSBQP = GetExcelColumnName(columnEndPagecountNSBQP + columnStartPage1);

                if (countNSNopDP == 1)
                    // +1 vi cot tong cong  NSNopDP
                    ColNameEndPageNSNopDP = GetExcelColumnName(columnEndPageNSNopDP + 1);
                else
                    ColNameEndPageNSNopDP = GetExcelColumnName(columnEndPageNSNopDP);
            }


            HeaderReportTnQtChungTuChiTietHD4554 hd = new HeaderReportTnQtChungTuChiTietHD4554();

            foreach (var item in sLNSPage.Where(x => x.Lns.StartsWith(RevenueExpenditureType.MLNS_DP)).OrderBy(x => x.Lns).Select((value, index) => new { index, value }))
            {
                hd = new HeaderReportTnQtChungTuChiTietHD4554();
                if (item.value.STenDonVi != "ALL1")
                {
                    if (item.index == 1)
                    {
                        hd.SMoTa = "Nộp NSNN qua địa phương";
                        hd.MergeRange = string.Format("D8:{0}8", ColNameEndPageNSNopDP);
                        hd.STT = "1";
                    }
                    hd.TenDonVi = item.value.MoTa;
                    headers.Add(hd);
                }
            }

            foreach (var item in sLNSPage.Where(x => x.Lns.StartsWith(RevenueExpenditureType.MLNS_BQP)).OrderBy(x => x.Lns).Select((value, index) => new { index, value }))
            {
                hd = new HeaderReportTnQtChungTuChiTietHD4554();
                if (item.value.STenDonVi != "ALL")
                {
                    if (item.index == 1)
                    {
                        hd.SMoTa = "Nộp NSNN tại BQP";
                        hd.MergeRange = string.Format("G8:{0}8", ColNameEndPagecountNSBQP);
                        hd.STT = "1";
                    }

                    hd.TenDonVi = item.value.MoTa;
                    headersLstGiaTri.Add(hd);
                }
            }
        }


        private static void AddHeaderOnePageNganSachNhaNuoc(List<NsMuclucNgansachModel> sLNSPage, List<HeaderReportTnQtChungTuChiTietHD4554> headers, int countNSNopDP = 0, int countNSBQP = 0)
        {
            int columnStartPage1 = 4;
            int columnEndPageNSNopDP = columnStartPage1 + (countNSNopDP > 1 ? countNSNopDP : 0);
            int columnEndPagecountNSBQP = columnStartPage1 + (countNSBQP > 1 ? countNSBQP : 0);
            string ColNameEndPageNSNopDP = string.Empty;
            string ColNameEndPagecountNSBQP = string.Empty;

            if (countNSNopDP == 0)
            {
                if (countNSBQP >= 2)
                    ColNameEndPagecountNSBQP = GetExcelColumnName(columnEndPagecountNSBQP);
                else
                    ColNameEndPagecountNSBQP = GetExcelColumnName(columnEndPagecountNSBQP + 1);

                ColNameEndPageNSNopDP = GetExcelColumnName(columnEndPageNSNopDP);
            }
            else
            {
                if (countNSBQP >= 2)
                    ColNameEndPagecountNSBQP = GetExcelColumnName(columnEndPagecountNSBQP);
                else
                    ColNameEndPagecountNSBQP = GetExcelColumnName(columnEndPagecountNSBQP + 1);

                if (countNSNopDP == 1)
                    ColNameEndPageNSNopDP = GetExcelColumnName(columnEndPageNSNopDP + 1);
                else
                    ColNameEndPageNSNopDP = GetExcelColumnName(columnEndPageNSNopDP);
            }


            HeaderReportTnQtChungTuChiTietHD4554 hd = new HeaderReportTnQtChungTuChiTietHD4554();
            hd.TenDonVi = "Tổng số";
            hd.SMoTa = "Nộp NSNN qua địa phương";
            hd.MergeRange = string.Format("D6:{0}6", ColNameEndPageNSNopDP);
            hd.STT = "1";
            headers.Add(hd);

            foreach (var item in sLNSPage.Select((value, index) => new { index, value }))
            {
                hd = new HeaderReportTnQtChungTuChiTietHD4554();
                if (item.index == countNSNopDP)
                {
                    hd.TenDonVi = item.value.MoTa;
                    hd.SMoTa = "Nộp NSNN tại BQP";
                    hd.STT = "1";
                    hd.MergeRange = string.Format("D6:{0}6", ColNameEndPagecountNSBQP);
                    headers.Add(hd);
                }
                else
                {
                    hd.TenDonVi = item.value.MoTa;
                    headers.Add(hd);
                }
            }
        }

        private static void AddHeader(List<NsMuclucNgansachModel> sLNSPage, List<HeaderReportTnQtChungTuChiTietHD4554> headers)
        {

            HeaderReportTnQtChungTuChiTietHD4554 hd = new HeaderReportTnQtChungTuChiTietHD4554();
            hd.TenDonVi = "Tổng số";
            headers.Add(hd);

            foreach (NsMuclucNgansachModel tenSLNS in sLNSPage)
            {
                hd = new HeaderReportTnQtChungTuChiTietHD4554();
                hd.TenDonVi = tenSLNS.MoTa;
                headers.Add(hd);
            }
        }

        private string GetPath(string input)
        {
            if (SelectedKieuGiayIn.ValueItem == "2")
                input = input + "_A3_Ngang";
            else
                input = input + "_A4_Ngang";
            return Path.Combine(ExportPrefix.PATH_TL_THUNOP_NGANSACH, input + FileExtensionFormats.Xlsx);
        }

        private void AddChuKy(Dictionary<string, object> data)
        {
            data.Add("Diadiem", string.Format("{0}, ngày {1} tháng {2} năm {3}", _diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
        }

        private List<ReportChildTnQtChungTuChiTietHD4554Query> ParseDataGroupTong(List<TnQtChungTuChiTietHD4554Query> tnQtChungTuChiTietHD4554Queries, List<NsMuclucNgansachModel> sLNSPage, int Count = 1, List<NsMuclucNgansachModel> lstSLSNNopBQP = null, List<AgencyModel> lstIdDonVi = null)
        {
            List<ReportChildTnQtChungTuChiTietHD4554Query> outPut = new List<ReportChildTnQtChungTuChiTietHD4554Query>();
            if (Count == 1)
            {
                var itemsResult = tnQtChungTuChiTietHD4554Queries.Where(x => sLNSPage.Select(l => l.Lns).Contains(x.SLNS)).GroupBy(x => x.IIdMaDonVi).Select(s => new ReportChildTnQtChungTuChiTietHD4554Query()
                {
                    IIDMaDonVi = s.FirstOrDefault().IIdMaDonVi,
                    STenDonVi = s.FirstOrDefault().STenDonVi,
                    FSoTien = s.Sum(x => x.FSoTien)
                }).ToList();
                outPut = itemsResult.OrderBy(x => x.IIDMaDonVi).ToList();
            }
            else
            {
                var lstDataClone = _lstdata.Clone();
                List<TnQtChungTuChiTietHD4554Query> itemsPage = new List<TnQtChungTuChiTietHD4554Query>();
                itemsPage = lstDataClone.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi) && tnQtChungTuChiTietHD4554Queries.Select(x => x.IIdMaDonVi).Distinct().Contains(x.IIdMaDonVi) && lstSLSNNopBQP.Select(x => x.Lns).Contains(x.SLNS)).ToList();

                var itemsResult = itemsPage.Where(x => lstSLSNNopBQP.Select(l => l.Lns).Contains(x.SLNS)).GroupBy(x => x.IIdMaDonVi).Select(s => new ReportChildTnQtChungTuChiTietHD4554Query()
                {
                    IIDMaDonVi = s.FirstOrDefault().IIdMaDonVi,
                    STenDonVi = s.FirstOrDefault().STenDonVi,
                    FSoTien = s.Sum(x => x.FSoTien)
                }).ToList();
                outPut = itemsResult.OrderBy(x => x.IIDMaDonVi).ToList();
            }

            return outPut;
        }

        private List<ReportChildTnQtChungTuChiTietHD4554Query> ParseDataGroup(List<TnQtChungTuChiTietHD4554Query> tnQtChungTuChiTietHD4554Queries, List<NsMuclucNgansachModel> sLNSPage)
        {
            List<ReportChildTnQtChungTuChiTietHD4554Query> outPut = new List<ReportChildTnQtChungTuChiTietHD4554Query>();
            if (tnQtChungTuChiTietHD4554Queries.IsEmpty() && sLNSPage.IsEmpty()) return outPut;
            foreach (NsMuclucNgansachModel item in sLNSPage)
            {
                ReportChildTnQtChungTuChiTietHD4554Query giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
                if (!string.IsNullOrEmpty(item.Lns))
                {
                    TnQtChungTuChiTietHD4554Query gtSLNS = tnQtChungTuChiTietHD4554Queries.FirstOrDefault(x => item.Lns.Equals(x.SLNS));
                    if (gtSLNS is null) giaTri.FSoTien = 0;
                    else giaTri.FSoTien = gtSLNS.FSoTien;
                    outPut.Add(giaTri);
                }
                else
                {
                    outPut.Add(giaTri);
                }
            }
            return outPut;
        }

        // Cot tong Nộp NSNN qua địa phương
        private List<ReportChildTnQtChungTuChiTietHD4554Query> ParseDataGroupTongNganSachNhaNuocOnePage(List<TnQtChungTuChiTietHD4554Query> tnQtChungTuChiTietHD4554Queries, List<NsMuclucNgansachModel> sLNSPage, int Count = 1, List<NsMuclucNgansachModel> lstSLSNNopBQP = null, List<AgencyModel> lstIdDonVi = null)
        {
            List<ReportChildTnQtChungTuChiTietHD4554Query> outPut = new List<ReportChildTnQtChungTuChiTietHD4554Query>();

            var itemsResult = tnQtChungTuChiTietHD4554Queries.Where(x => x.SLNS.StartsWith(RevenueExpenditureType.MLNS_DP) && sLNSPage.Select(l => l.Lns).Contains(x.SLNS)).GroupBy(x => x.IIdMaDonVi).Select(s => new ReportChildTnQtChungTuChiTietHD4554Query()
            {
                IIDMaDonVi = s.FirstOrDefault().IIdMaDonVi,
                STenDonVi = s.FirstOrDefault().STenDonVi,
                FSoTien = s.Sum(x => x.FSoTien)
            }).ToList();

            if (itemsResult.Count > 0)
            {
                return outPut = itemsResult.OrderBy(x => x.IIDMaDonVi).ToList();
            }
            else
            {
                ReportChildTnQtChungTuChiTietHD4554Query reportChildTnQtChungTuChiTietHD4554Query = new ReportChildTnQtChungTuChiTietHD4554Query();
                outPut.Add(reportChildTnQtChungTuChiTietHD4554Query);
            }
            return outPut;
        }

        private List<ReportChildTnQtChungTuChiTietHD4554Query> ParseDataGroupTongNganSachNhaNuocOnePage2(List<TnQtChungTuChiTietHD4554Query> tnQtChungTuChiTietHD4554Queries, List<NsMuclucNgansachModel> sLNSPage, int Count = 1, List<NsMuclucNgansachModel> lstSLSNNopBQP = null, List<AgencyModel> lstIdDonVi = null)
        {
            List<ReportChildTnQtChungTuChiTietHD4554Query> outPut = new List<ReportChildTnQtChungTuChiTietHD4554Query>();

            var itemsResult = tnQtChungTuChiTietHD4554Queries.Where(x => x.SLNS.StartsWith(RevenueExpenditureType.MLNS_DP) && sLNSPage.Select(l => l.Lns).Contains(x.SLNS)).GroupBy(x => x.IIdMaDonVi).Select(s => new ReportChildTnQtChungTuChiTietHD4554Query()
            {
                IIDMaDonVi = s.FirstOrDefault().IIdMaDonVi,
                STenDonVi = s.FirstOrDefault().STenDonVi,
                FSoTien = s.Sum(x => x.FSoTien)
            }).ToList();

            if (itemsResult.Count > 0)
            {
                return outPut = itemsResult.OrderBy(x => x.IIDMaDonVi).ToList();
            }
            else
            {
                ReportChildTnQtChungTuChiTietHD4554Query reportChildTnQtChungTuChiTietHD4554Query = new ReportChildTnQtChungTuChiTietHD4554Query();
                outPut.Add(reportChildTnQtChungTuChiTietHD4554Query);
            }
            return outPut;
        }

        private List<ReportChildTnQtChungTuChiTietHD4554Query> ParseDataGroupNganSachNhaNuocOnePage2(List<TnQtChungTuChiTietHD4554Query> tnQtChungTuChiTietHD4554Queries, List<NsMuclucNgansachModel> sLNSPage, List<TnQtChungTuChiTietHD4554Query> itemsPage, bool IsLstGiaTri = true)
        {
            List<ReportChildTnQtChungTuChiTietHD4554Query> outPut = new List<ReportChildTnQtChungTuChiTietHD4554Query>();
            if (tnQtChungTuChiTietHD4554Queries.IsEmpty() && sLNSPage.IsEmpty()) return outPut;
            if (IsLstGiaTri)
            {
                foreach (NsMuclucNgansachModel item in sLNSPage.Where(x => x.Lns.StartsWith(RevenueExpenditureType.MLNS_BQP)).ToList())
                {
                    ReportChildTnQtChungTuChiTietHD4554Query giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
                    if (item.STenDonVi is null || !item.STenDonVi.Equals("ALL"))
                    {
                        if (!string.IsNullOrEmpty(item.Lns))
                        {
                            TnQtChungTuChiTietHD4554Query gtSLNS = tnQtChungTuChiTietHD4554Queries.FirstOrDefault(x => item.Lns.Equals(x.SLNS));
                            if (gtSLNS is null) giaTri.FSoTien = 0;
                            else giaTri.FSoTien = gtSLNS.FSoTien;
                            outPut.Add(giaTri);
                        }

                        else
                        {
                            giaTri.FSoTien = 0;
                            outPut.Add(giaTri);
                        }
                    }

                }
            }
            else
            {
                foreach (NsMuclucNgansachModel item in sLNSPage.Where(x => x.Lns.StartsWith(RevenueExpenditureType.MLNS_DP)).ToList())
                {
                    ReportChildTnQtChungTuChiTietHD4554Query giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
                    if (item.STenDonVi is null || !item.STenDonVi.Equals("ALL1"))
                    {
                        if (!string.IsNullOrEmpty(item.Lns))
                        {
                            TnQtChungTuChiTietHD4554Query gtSLNS = tnQtChungTuChiTietHD4554Queries.FirstOrDefault(x => item.Lns.Equals(x.SLNS));
                            if (gtSLNS is null) giaTri.FSoTien = 0;
                            else giaTri.FSoTien = gtSLNS.FSoTien;
                            outPut.Add(giaTri);
                        }
                        else
                        {
                            giaTri.FSoTien = 0;
                            outPut.Add(giaTri);
                        }
                    }

                }
            }
            return outPut;
        }

        private List<ReportChildTnQtChungTuChiTietHD4554Query> ParseDataGroupNganSachNhaNuocOnePage(List<TnQtChungTuChiTietHD4554Query> tnQtChungTuChiTietHD4554Queries, List<NsMuclucNgansachModel> sLNSPage, List<TnQtChungTuChiTietHD4554Query> itemsPage)
        {
            List<ReportChildTnQtChungTuChiTietHD4554Query> outPut = new List<ReportChildTnQtChungTuChiTietHD4554Query>();
            if (tnQtChungTuChiTietHD4554Queries.IsEmpty() && sLNSPage.IsEmpty()) return outPut;
            foreach (NsMuclucNgansachModel item in sLNSPage)
            {
                ReportChildTnQtChungTuChiTietHD4554Query giaTri = new ReportChildTnQtChungTuChiTietHD4554Query();
                if (!string.IsNullOrEmpty(item.Lns))
                {
                    TnQtChungTuChiTietHD4554Query gtSLNS = tnQtChungTuChiTietHD4554Queries.FirstOrDefault(x => item.Lns.Equals(x.SLNS));
                    if (gtSLNS is null) giaTri.FSoTien = 0;
                    else giaTri.FSoTien = gtSLNS.FSoTien;
                    outPut.Add(giaTri);
                }
                else if (item.STenDonVi == "ALL")
                {
                    var itemClone = _lstdata.Where(x => x.SLNS.StartsWith(RevenueExpenditureType.MLNS_BQP)).Clone();
                    var itemChild = itemClone.Where(x => tnQtChungTuChiTietHD4554Queries.Select(i => i.IIdMaDonVi).Contains(x.IIdMaDonVi)).GroupBy(x => x.IIdMaDonVi).Select(s => new ReportChildTnQtChungTuChiTietHD4554Query
                    {
                        IIDMaDonVi = s.FirstOrDefault().IIdMaDonVi,
                        STenDonVi = s.FirstOrDefault().STenDonVi,
                        FSoTien = s.Sum(x => x.FSoTien)
                    });

                    if (itemChild is null) giaTri.FSoTien = 0;
                    else giaTri.FSoTien = itemChild.Sum(x => x.FSoTien);
                    outPut.Add(giaTri);
                }
                else
                {
                    giaTri.FSoTien = 0;
                    outPut.Add(giaTri);
                }
            }
            return outPut;
        }

        private void GetDataExport(List<TnQtChungTuHD4554> allChungTu, List<AgencyModel> lstIdDonVi, int dvt, List<NsMuclucNgansachModel> lstSLNSSelected)
        {
            #region warning
            List<string> lstThang = new List<string>();
            int iThangQuyLoai = 0;
            switch (InTheo)
            {
                case TimeOptionTypes.Months:
                    iThangQuyLoai = 0;
                    lstThang.Add(TimeOptionSelected.ValueItem);
                    break;
                case TimeOptionTypes.Quarter:
                    iThangQuyLoai = 1;
                    lstThang.Add(TimeOptionSelected.ValueItem);
                    break;
                case TimeOptionTypes.Year:
                    if (TimeOptionSelected.ValueItem == "0")
                        iThangQuyLoai = 0;
                    else
                        iThangQuyLoai = 1;
                    lstThang = GetListThang();
                    break;
                default:
                    lstThang = new List<string>();
                    break;
            }
            #endregion

            List<TnQtChungTuChiTietHD4554Query> lstData = new List<TnQtChungTuChiTietHD4554Query>();
            var idChungtus = string.Join(",", allChungTu.Select(x => x.Id));
            var lstDonvis = string.Join(",", lstIdDonVi.Select(x => x.IIDMaDonVi));
            var lstSLNS = string.Join(",", lstSLNSSelected.Select(x => x.Lns));
            List<string> slns = new List<string>();
            allChungTu.ForEach(x =>
            {
                slns.AddRange(x.SDSLNS.Split(","));
            });
            var sLnss = new HashSet<string>(slns);
            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherIds = idChungtus,
                LNS = string.Join(",", sLnss.Select(x => x)),
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                BudgetSource = _sessionService.Current.Budget,
                IdDonVi = lstDonvis,
                UserName = _sessionService.Current.Principal,
                DonViTinh = dvt,
                IThangQuyLoai = iThangQuyLoai,
                LstThangQuy = string.Join(",", lstThang.Select(x => x)),
            };

            var lns = _budgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns).ToList();
            _lstdata = _tnQtChungTuChiTietHD4554Service.FindAllNSDCChungTuByCondition(searchCondition);
            _lstdata = _lstdata.Where(x => lns.Contains(x.SLNS)).ToList();
            //lstData.AddRange(data);

        }

        private void GetDataExportCaoNganSachNhaNuoc(List<TnQtChungTuHD4554> allChungTu, List<AgencyModel> lstIdDonVi, int dvt, List<NsMuclucNgansachModel> lstSLNSSelected)
        {
            #region warning
            List<string> lstThang = new List<string>();
            int iThangQuyLoai = 0;
            switch (InTheo)
            {
                case TimeOptionTypes.Months:
                    iThangQuyLoai = 0;
                    lstThang.Add(TimeOptionSelected.ValueItem);
                    break;
                case TimeOptionTypes.Quarter:
                    iThangQuyLoai = 1;
                    lstThang.Add(TimeOptionSelected.ValueItem);
                    break;
                case TimeOptionTypes.Year:
                    if (TimeOptionSelected.ValueItem == "0")
                        iThangQuyLoai = 0;
                    else
                        iThangQuyLoai = 1;
                    lstThang = GetListThang();
                    break;
                default:
                    lstThang = new List<string>();
                    break;
            }
            #endregion

            List<TnQtChungTuChiTietHD4554Query> lstData = new List<TnQtChungTuChiTietHD4554Query>();
            var idChungtus = string.Join(",", allChungTu.Select(x => x.Id));
            var lstDonvis = string.Join(",", lstIdDonVi.Select(x => x.IIDMaDonVi));
            var lstSLNS = string.Join(",", lstSLNSSelected.Select(x => x.Lns));
            List<string> slns = new List<string>();
            allChungTu.ForEach(x =>
            {
                slns.AddRange(x.SDSLNS.Split(","));
            });
            var sLnss = new HashSet<string>(slns);
            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherIds = idChungtus,
                LNS = string.Join(",", sLnss.Select(x => x)),
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                BudgetSource = _sessionService.Current.Budget,
                IdDonVi = lstDonvis,
                UserName = _sessionService.Current.Principal,
                DonViTinh = dvt,
                IThangQuyLoai = iThangQuyLoai,
                LstThangQuy = string.Join(",", lstThang.Select(x => x)),
            };

            var lns = _budgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns).ToList();
            _lstdata = _tnQtChungTuChiTietHD4554Service.FindAllNSDCChungTuByCondition(searchCondition);
            _lstdata = _lstdata.Where(x => lns.Contains(x.SLNS)).ToList();
            //lstData.AddRange(data);

        }


        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        private List<string> GetListThang()
        {
            int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int[] quaters = { 3, 6, 9, 12 };
            var list = new List<string>();
            if (_timeOptionSelected.ValueItem == "0")
            {
                foreach (var item in months)
                {
                    list.Add(item.ToString());
                }
            }

            if (_timeOptionSelected.ValueItem == "1")
            {
                foreach (var item in quaters)
                {
                    list.Add(item.ToString());
                }
            }

            return list;
        }

        private void LoadAgencies()
        {
            try
            {
                int yearOfWork = _sessionService.Current.YearOfWork;
                int iNamNganSach = _sessionService.Current.YearOfBudget;
                int iNguonNganSach = _sessionService.Current.Budget;
                List<DonVi> listDonVi = _donViService.FindByNamLamViec(yearOfWork).ToList();
                List<TnQtChungTuChiTietHD4554> lstDonViTheoThang = new List<TnQtChungTuChiTietHD4554>();
                List<string> lstDonViEsxit = new List<string>();
                var condition = PredicateBuilder.True<TnQtChungTuChiTietHD4554>();
                condition = condition.And(x => x.INamLamViec == yearOfWork);
                if (RealRevenueExpenditureTypes.Equals(RealRevenueExpenditureType.REAL_BUDGET_NATIONAL_DEFENSE_RESULT))
                {
                    condition = condition.And(x => x.SLNS.StartsWith("802"));
                }
                else
                {
                    condition = condition.And(x => x.SLNS.StartsWith("801"));
                }

                if (InTheo == TimeOptionTypes.Months)
                {
                    condition = condition.And(x => x.IThangQuyLoai == (int)TimeOptionTypes.Months);
                    condition = condition.And(x => x.IThangQuy == int.Parse(TimeOptionSelected.ValueItem));
                    condition = condition.And(x => x.INamNganSach == iNamNganSach);
                    condition = condition.And(x => x.INguonNganSach == iNguonNganSach);
                    condition = condition.And(x => x.FSoTien.GetValueOrDefault(0) != 0);
                    lstDonViTheoThang = _tnQtChungTuChiTietHD4554Service.FindByCondition(condition).ToList();
                    lstDonViEsxit = lstDonViTheoThang.Select(x => x.IIdMaDonVi).ToList();
                }

                if (InTheo == TimeOptionTypes.Quarter)
                {
                    condition = condition.And(x => x.IThangQuyLoai == (int)TimeOptionTypes.Quarter);
                    condition = condition.And(x => x.IThangQuy == int.Parse(TimeOptionSelected.ValueItem));
                    condition = condition.And(x => x.INamNganSach == iNamNganSach);
                    condition = condition.And(x => x.INguonNganSach == iNguonNganSach);
                    condition = condition.And(x => x.FSoTien.GetValueOrDefault(0) != 0);
                    lstDonViTheoThang = _tnQtChungTuChiTietHD4554Service.FindByCondition(condition).ToList();
                    lstDonViEsxit = lstDonViTheoThang.Select(x => x.IIdMaDonVi).ToList();
                }

                if (InTheo == TimeOptionTypes.Year)
                {
                    int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                    int[] quaters = { 3, 6, 9, 12 };
                    if (TimeOptionSelected?.ValueItem == "0")
                    {
                        condition = condition.And(x => x.IThangQuyLoai == (int)TimeOptionTypes.Months);
                        condition = condition.And(x => x.INamNganSach == iNamNganSach);
                        condition = condition.And(x => x.INguonNganSach == iNguonNganSach);
                        condition = condition.And(x => x.FSoTien.GetValueOrDefault(0) != 0);
                        lstDonViTheoThang = _tnQtChungTuChiTietHD4554Service.FindByCondition(condition).ToList();
                        lstDonViTheoThang = lstDonViTheoThang.Where(x => months.Contains(x.IThangQuy.Value)).ToList();
                        lstDonViEsxit = lstDonViTheoThang.Select(x => x.IIdMaDonVi).ToList();
                    }
                    else
                    {
                        condition = condition.And(x => x.IThangQuyLoai == (int)TimeOptionTypes.Quarter);
                        condition = condition.And(x => x.INamNganSach == iNamNganSach);
                        condition = condition.And(x => x.INguonNganSach == iNguonNganSach);
                        condition = condition.And(x => x.FSoTien.GetValueOrDefault(0) != 0);
                        lstDonViTheoThang = _tnQtChungTuChiTietHD4554Service.FindByCondition(condition).ToList();
                        lstDonViTheoThang = lstDonViTheoThang.Where(x => quaters.Contains(x.IThangQuy.Value)).ToList();
                        lstDonViEsxit = lstDonViTheoThang.Select(x => x.IIdMaDonVi).ToList();
                    }
                }

                if (lstDonViEsxit.Count > 0)
                    listDonVi = listDonVi.Where(x => lstDonViEsxit.Contains(x.IIDMaDonVi)).ToList();
                else
                    listDonVi = new List<DonVi>();
                Agencies = new ObservableCollection<AgencyModel>();
                Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
                _listAgency = CollectionViewSource.GetDefaultView(Agencies);
                _listAgency.Filter = ListAgencyFilter;
                foreach (var model in Agencies)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(AgencyModel.Selected))
                        {
                            OnPropertyChanged(nameof(SelectedAgencyCount));
                            OnPropertyChanged(nameof(IsSelectAllAgency));
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListAgencyFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchAgencyText))
            {
                return true;
            }
            return obj is AgencyModel item && item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
        }

        private void LoadBudgetIndexes(string sMaDonVi = null)
        {
            try
            {
                var yearOfWork = _sessionService.Current.YearOfWork;
                var yearOfBudget = _sessionService.Current.YearOfBudget;
                var budget = _sessionService.Current.Budget;
                string sLNS = string.Empty;
                if (RealRevenueExpenditureTypes.Equals(RealRevenueExpenditureType.REAL_BUDGET_STATE_RESULT))
                {
                    sLNS = "801";
                }
                else
                {
                    sLNS = "802";
                }
                var listMucLucNganSach = _mucLucNganSachService.FindByMLNS(yearOfWork, sLNS).Where(x => x.ITrangThai == StatusType.ACTIVE);
                if (!string.IsNullOrEmpty(sMaDonVi))
                {
                    List<TnQtChungTuChiTietHD4554> lstSLNSTheoThang = new List<TnQtChungTuChiTietHD4554>();
                    List<string> lstSLNSEsxit = new List<string>();
                    List<string> lstMaDonVi = new List<string>();
                    lstMaDonVi = sMaDonVi.Split(",").ToList();

                    var condition = PredicateBuilder.True<TnQtChungTuChiTietHD4554>();
                    condition = condition.And(x => x.INamLamViec == yearOfWork);
                    if (InTheo == TimeOptionTypes.Months)
                    {
                        condition = condition.And(x => x.IThangQuyLoai == (int)TimeOptionTypes.Months);
                        condition = condition.And(x => x.IThangQuy == int.Parse(TimeOptionSelected.ValueItem));
                        condition = condition.And(x => x.INamNganSach == yearOfBudget);
                        condition = condition.And(x => x.INguonNganSach == budget);
                        condition = condition.And(x => x.FSoTien.GetValueOrDefault(0) != 0);
                        lstSLNSTheoThang = _tnQtChungTuChiTietHD4554Service.FindByCondition(condition).ToList();
                        lstSLNSTheoThang = lstSLNSTheoThang.Where(x => lstMaDonVi.Contains(x.IIdMaDonVi)).ToList();
                        lstSLNSEsxit = lstSLNSTheoThang.Select(x => x.SLNS).Distinct().ToList();
                    }

                    if (InTheo == TimeOptionTypes.Quarter)
                    {
                        condition = condition.And(x => x.IThangQuyLoai == (int)TimeOptionTypes.Quarter);
                        condition = condition.And(x => x.IThangQuy == int.Parse(TimeOptionSelected.ValueItem));
                        condition = condition.And(x => x.INamNganSach == yearOfBudget);
                        condition = condition.And(x => x.INguonNganSach == budget);
                        condition = condition.And(x => x.FSoTien.GetValueOrDefault(0) != 0);
                        lstSLNSTheoThang = _tnQtChungTuChiTietHD4554Service.FindByCondition(condition).ToList();
                        lstSLNSTheoThang = lstSLNSTheoThang.Where(x => lstMaDonVi.Contains(x.IIdMaDonVi)).ToList();
                        lstSLNSEsxit = lstSLNSTheoThang.Select(x => x.SLNS).Distinct().ToList();
                    }

                    if (InTheo == TimeOptionTypes.Year)
                    {
                        int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                        int[] quaters = { 3, 6, 9, 12 };
                        if (TimeOptionSelected.ValueItem == "0")
                        {
                            condition = condition.And(x => x.IThangQuyLoai == (int)TimeOptionTypes.Months);
                            condition = condition.And(x => x.INamNganSach == yearOfBudget);
                            condition = condition.And(x => x.INguonNganSach == budget);
                            condition = condition.And(x => x.FSoTien.GetValueOrDefault(0) != 0);
                            lstSLNSTheoThang = _tnQtChungTuChiTietHD4554Service.FindByCondition(condition).ToList();
                            lstSLNSTheoThang = lstSLNSTheoThang.Where(x => months.Contains(x.IThangQuy.Value)).ToList();
                            lstSLNSTheoThang = lstSLNSTheoThang.Where(x => lstMaDonVi.Contains(x.IIdMaDonVi)).ToList();
                            lstSLNSEsxit = lstSLNSTheoThang.Select(x => x.SLNS).Distinct().ToList();
                        }
                        else
                        {
                            condition = condition.And(x => x.IThangQuyLoai == (int)TimeOptionTypes.Quarter);
                            condition = condition.And(x => x.INamNganSach == yearOfBudget);
                            condition = condition.And(x => x.INguonNganSach == budget);
                            condition = condition.And(x => x.FSoTien.GetValueOrDefault(0) != 0);
                            lstSLNSTheoThang = _tnQtChungTuChiTietHD4554Service.FindByCondition(condition).ToList();
                            lstSLNSTheoThang = lstSLNSTheoThang.Where(x => quaters.Contains(x.IThangQuy.Value)).ToList();
                            lstSLNSTheoThang = lstSLNSTheoThang.Where(x => lstMaDonVi.Contains(x.IIdMaDonVi)).ToList();
                            lstSLNSEsxit = lstSLNSTheoThang.Select(x => x.SLNS).Distinct().ToList();
                        }
                    }

                    if (lstSLNSEsxit.Count > 0)
                        listMucLucNganSach = listMucLucNganSach.Where(x => lstSLNSEsxit.Contains(x.Lns)).ToList();
                    else
                        listMucLucNganSach = new List<NsMucLucNganSach>();
                }
                else
                {
                    listMucLucNganSach = new List<NsMucLucNganSach>();
                }
                BudgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
                BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMucLucNganSach);

                _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
                _listBudgetIndex.Filter = ListBudgetIndexFilter;
                foreach (var model in BudgetIndexes)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                        {
                            foreach (var item in BudgetIndexes)
                            {
                                if (item.MlnsIdParent == model.MlnsId)
                                {
                                    if (item.IsHitTestVisible)
                                        item.IsSelected = model.IsSelected;
                                }
                            }
                            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                            OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                        }
                    };
                }

                OnPropertyChanged(nameof(BudgetIndexes));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListBudgetIndexFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchBudgetIndexText))
            {
                return true;
            }
            return obj is TnDanhMucLoaiHinhModel item && item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
        }

        private void CalculateData()
        {
            ItemsTnQt.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.TongSoThu = 0;
                    x.QtKhauHaoTscđ = 0;
                    x.QtTienLuong = 0;
                    x.QtQtnskhac = 0;
                    x.ChiPhiKhac = 0;
                    x.ThueGtgt = 0;
                    x.ThueTndn = 0;
                    x.ThueTndnBqp = 0;
                    x.PhiLePhi = 0;
                    x.NsnnKhac = 0;
                    x.NsnnKhacBqp = 0;
                    x.PpNopNsqp = 0;
                    x.PpBoSungKinhPhi = 0;
                    x.PpTrichCacQuy = 0;
                    return x;
                }).ToList();

            foreach (var item in ItemsTnQt)
            {
                CalculateParent(item, item);
            }

            CalculateTotal();
        }

        private void CalculateParent(TnQtChungTuChiTietModel currentItem, TnQtChungTuChiTietModel seftItem)
        {
            var parrentItem = ItemsTnQt.Where(x => x.IdMaLoaiHinh == currentItem.IdMaLoaiHinhCha).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.TongSoThu += seftItem.TongSoThu;
            parrentItem.QtKhauHaoTscđ += seftItem.QtKhauHaoTscđ;
            parrentItem.QtTienLuong += seftItem.QtTienLuong;
            parrentItem.QtQtnskhac += seftItem.QtQtnskhac;
            parrentItem.ChiPhiKhac += seftItem.ChiPhiKhac;
            parrentItem.ThueGtgt += seftItem.ThueGtgt;
            parrentItem.ThueTndn += seftItem.ThueTndn;
            parrentItem.ThueTndnBqp += seftItem.ThueTndnBqp;
            parrentItem.PhiLePhi += seftItem.PhiLePhi;
            parrentItem.NsnnKhac += seftItem.NsnnKhac;
            parrentItem.NsnnKhacBqp += seftItem.NsnnKhacBqp;
            parrentItem.PpNopNsqp += seftItem.PpNopNsqp;
            parrentItem.PpBoSungKinhPhi += seftItem.PpBoSungKinhPhi;
            parrentItem.PpTrichCacQuy += seftItem.PpTrichCacQuy;
            CalculateParent(parrentItem, seftItem);
        }

        private void CalculateTotal()
        {
            TotalTongSoThu = 0;

            var listChildren = ItemsTnQt.Where(x => !x.IsHangCha && x.TongSoThu > 0).ToList();
            foreach (var item in listChildren)
            {
                TotalTongSoThu += (item.TongSoThu != null ? item.TongSoThu.Value : 0);
            }
        }

        private void LoadMonths()
        {
            for (int month = 1; month <= 12; month++)
            {
                _months.Add(month.ToString());
            }
        }

        private void LoadQuater()
        {
            for (int quater = 3; quater <= 12; quater += 3)
            {
                _quaters.Add(quater.ToString());
            }
        }

        private Dictionary<string, object> HandleData(EstimationVoucherDetailCriteria searchCondition)
        {
            List<TnQtChungTuChiTietReportQuery> _chungTuChiTiet = _tnQtChungTuChiTietService.FindByRealRevenueExpenditureReportCondition(searchCondition).ToList();

            if (_quarterMonthSelected.ValueItem.Equals(RevenueExpenditureType.REAL_REVENUE_REPORT_QUATER_MONTH_KEY))
            {
                _chungTuChiTiet = _chungTuChiTiet.GroupBy(g => new { g.IdMaLoaiHinh, g.IdMaLoaiHinhCha, g.Lns, g.BLaHangCha, g.Noidung }).Select(x => new TnQtChungTuChiTietReportQuery
                {
                    IdMaLoaiHinh = x.Key.IdMaLoaiHinh,
                    IdMaLoaiHinhCha = x.Key.IdMaLoaiHinhCha,
                    Lns = x.Key.Lns,
                    BLaHangCha = x.Key.BLaHangCha,
                    Noidung = x.Key.Noidung,
                    TongSoThu = x.Sum(rpt => rpt.TongSoThu),
                    TongSoChiPhi = x.Sum(rpt => rpt.TongSoChiPhi),
                    QtKhauHaoTscđ = x.Sum(rpt => rpt.QtKhauHaoTscđ),
                    QtTienLuong = x.Sum(rpt => rpt.QtTienLuong),
                    QtQtnskhac = x.Sum(rpt => rpt.QtQtnskhac),
                    QtTongSoQtns = x.Sum(rpt => rpt.QtTongSoQtns),
                    ChiPhiKhac = x.Sum(rpt => rpt.ChiPhiKhac),
                    TongnopNsnn = x.Sum(rpt => rpt.TongnopNsnn),
                    ThueGtgt = x.Sum(rpt => rpt.ThueGtgt),
                    ThueTndn = x.Sum(rpt => rpt.ThueTndn),
                    ThueTndnBqp = x.Sum(rpt => rpt.ThueTndnBqp),
                    PhiLePhi = x.Sum(rpt => rpt.PhiLePhi),
                    NsnnKhac = x.Sum(rpt => rpt.NsnnKhac),
                    NsnnKhacBqp = x.Sum(rpt => rpt.NsnnKhacBqp),
                    ChenhLech = x.Sum(rpt => rpt.ChenhLech),
                    PpNopNsqp = x.Sum(rpt => rpt.PpNopNsqp),
                    PpBoSungKinhPhi = x.Sum(rpt => rpt.PpBoSungKinhPhi),
                    PpTrichCacQuy = x.Sum(rpt => rpt.PpTrichCacQuy),
                    PpSoChuaPhanPhoi = x.Sum(rpt => rpt.PpSoChuaPhanPhoi)
                }).ToList();
            }

            ItemsTnQt = _mapper.Map<ObservableCollection<TnQtChungTuChiTietModel>>(_chungTuChiTiet);

            CalculateData();

            List<TnQtChungTuChiTietModel> chungTuReport = ItemsTnQt.ToList().Where(x => x.TongSoThu > 0 || x.TongSoChiPhi > 0 || x.QtKhauHaoTscđ > 0 || x.QtTienLuong > 0 ||
                                                x.QtQtnskhac > 0 || x.QtTongSoQtns > 0 || x.ChiPhiKhac > 0 || x.TongnopNsnn > 0 ||
                                                x.ThueGtgt > 0 || x.ThueTndn > 0 || x.ThueTndnBqp > 0 || x.PhiLePhi > 0 || x.NsnnKhac > 0 ||
                                                x.NsnnKhacBqp > 0 || x.ChenhLech > 0 || x.PpNopNsqp > 0 || x.PpBoSungKinhPhi > 0 ||
                                                x.PpTrichCacQuy > 0 || x.PpSoChuaPhanPhoi > 0).ToList();

            LoadMonths();
            LoadQuater();

            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("Cap1", "BỘ QUỐC PHÒNG");
            data.Add("Cap2", "CỤC TÀI CHÍNH");
            data.Add("TieuDe1", _txtTitleFirst);
            data.Add("TieuDe2", _txtTitleSecond);
            data.Add("Header1", _catUnitTypeSelected.DisplayItem);
            data.Add("Items", chungTuReport);

            return data;
        }

        public static string GetValueSelected(ObservableCollection<NsMuclucNgansachModel> data)
        {
            if (data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsSelected == true).Select(n => n.Lns).ToList());
            }
            return string.Empty;
        }


        private class GhiChu
        {
            public string Content { get; set; }
            public string SGhiChu => Content;
        }

        private void LoadTieuDe()
        {

            if (RealRevenueExpenditureTypes.Equals(RealRevenueExpenditureType.REAL_BUDGET_NATIONAL_DEFENSE_RESULT))
            {
                if (InTheo == TimeOptionTypes.Year)
                {
                    _typeChuKy = TypeChuKy.RPT_NS_THUNOP_NGANSACH_QUOCPHONG_NAM;
                }
                else if (InTheo == TimeOptionTypes.Quarter)
                {
                    _typeChuKy = TypeChuKy.RPT_NS_THUNOP_NGANSACH_QUOCPHONG_QUY;
                }
                else
                {
                    _typeChuKy = TypeChuKy.RPT_NS_THUNOP_NGANSACH_QUOCPHONG_THANG;
                }
            }
            else
            {
                if (InTheo == TimeOptionTypes.Year)
                {
                    _typeChuKy = TypeChuKy.RPT_NS_THUNOP_NGANSACH_NHANHUOC_NAM;
                }
                else if (InTheo == TimeOptionTypes.Quarter)
                {
                    _typeChuKy = TypeChuKy.RPT_NS_THUNOP_NGANSACH_NHANHUOC_QUY;
                }
                else
                {
                    _typeChuKy = TypeChuKy.RPT_NS_THUNOP_NGANSACH_NHANHUOC_THANG;
                }
            }
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                _txtTitleFirst = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                _txtTitleSecond = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                _txtTitleThird = _dmChuKy.TieuDe3MoTa;
            OnPropertyChanged(nameof(TxtTitleFirst));
            OnPropertyChanged(nameof(TxtTitleSecond));
            OnPropertyChanged(nameof(TxtTitleThird));
        }
    }
}
