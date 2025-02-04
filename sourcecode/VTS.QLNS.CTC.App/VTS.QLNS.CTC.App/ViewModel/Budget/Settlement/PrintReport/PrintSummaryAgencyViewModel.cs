using AutoMapper;
using ControlzEx.Standard;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Data;
using System.Xml.Linq;
using System.Xml.Schema;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;


namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport
{
    public class PrintSummaryAgencyViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IExportService _exportService;
        private INsDonViService _donViService;
        private INsMucLucNganSachService _mucLucNganSachService;
        private INsQtChungTuChiTietService _chungTuChiTietService;
        private IMapper _mapper;
        private ICollectionView _listBudgetIndex;
        private ICollectionView _listAgencyIndex;
        private ICollectionView _listPage;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private const string SELECTED_PAGE_COUNT_STR = "Chọn trang ({0}/{1})";
        private const string SELECTED_AGENCY_INDEX_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private bool _isCheckLNS;
        private bool _isCheckAgency;
        private Dictionary<int, List<string>> _dicAgency;
        private List<QuyetToanTongHopDonVi> _exportData;
        private QuyetToanTongHopDonViTotal _total;
        private List<QuyetToanDynamicValue> _totalListDynamic = new List<QuyetToanDynamicValue>();
        private List<DonVi> _listDonVi;
        private List<string> _listDonViSelected;
        private string _quarterMonth;
        private int _quarterMonthType;
        private string _quarterMonthBefore;
        private int _iNumListValue;
        private const string Header = "TRONG ĐÓ";
        private List<NsQtChungTuChiTiet> _lstMaDonViHashData = new List<NsQtChungTuChiTiet>();
        private List<DonVi> _lstMaDonViHasData = new List<DonVi>();
        private List<NsMucLucNganSach> _listMucLucNganSach;
        public override string Name => "Tổng hợp quyết toán - Đơn vị";
        public override string Title => "Tổng hợp quyết toán - Đơn vị";
        public override string Description => "MLNS hàng dọc - Đơn vị hàng ngang";
        public override Type ContentType => typeof(PrintSummaryAgency);

        private List<ComboboxItem> _monthAndQuarters;
        public List<ComboboxItem> MonthAndQuarters
        {
            get => _monthAndQuarters;
            set => SetProperty(ref _monthAndQuarters, value);
        }

        private ComboboxItem _monthAndQuartersSelected;
        public ComboboxItem MonthAndQuartersSelected
        {
            get => _monthAndQuartersSelected;
            set
            {
                SetProperty(ref _monthAndQuartersSelected, value);
                ProcessMonth();
                LoadBudgetIndexes();
                LoadAgencyHashData();
            }
        }

        private List<CheckBoxItem> _pages;
        public List<CheckBoxItem> Pages
        {
            get => _pages;
            set => SetProperty(ref _pages, value);
        }

        private string _searchPageText;
        public string SearchPageText
        {
            set
            {
                if (SetProperty(ref _searchPageText, value))
                {
                    _listPage.Refresh();
                }
            }
        }

        public bool InMotToChecked { get; set; }
        private bool _isAccumulated;
        public bool IsAccumulated
        {
            get => _isAccumulated;
            set
            {
                SetProperty(ref _isAccumulated, value);
                ProcessMonth();
                LoadAgencyHashData();
            }
        }

        public string SelectedPageCount
        {
            get
            {
                int totalCount = Pages != null ? Pages.Count : 0;
                int totalSelected = Pages != null ? Pages.Count(item => item.IsChecked) : 0;
                return string.Format(SELECTED_PAGE_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllPage;
        public bool IsSelectAllPage
        {
            get => Pages.Count() > 0 && Pages.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _isSelectAllPage, value);
                foreach (CheckBoxItem item in Pages)
                {
                    item.IsChecked = _isSelectAllPage;
                }
            }
        }

        private SummaryAgencyReportType _reportType;
        public SummaryAgencyReportType ReportType
        {
            get => _reportType;
            set
            {
                SetProperty(ref _reportType, value);
                LoadAgencyHashData();
                LoadBudgetIndexes();
            }
        }

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
                int totalCount = BudgetIndexes != null ? BudgetIndexes.Count : 0;
                int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => BudgetIndexes.Count() > 0 && BudgetIndexes.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                _isCheckLNS = true;
                foreach (NsMuclucNgansachModel item in BudgetIndexes)
                {
                    item.IsSelected = _isSelectAllBudgetIndex;
                }
                OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                LoadPages();
                _isCheckLNS = false;
            }
        }
        #endregion

        #region list Agency
        private ObservableCollection<DonViModel> _agencyIndexes;
        public ObservableCollection<DonViModel> AgencyIndexes
        {
            get => _agencyIndexes;
            set => SetProperty(ref _agencyIndexes, value);
        }

        private string _searchAgencyIndexText;
        public string SearchAgencyIndexText
        {
            set
            {
                if (SetProperty(ref _searchAgencyIndexText, value))
                {
                    _listAgencyIndex.Refresh();
                }
            }
        }

        public string SelectedAgencyIndexCount
        {
            get
            {
                int totalCount = AgencyIndexes != null ? AgencyIndexes.Count : 0;
                int totalSelected = AgencyIndexes != null ? AgencyIndexes.Count(item => item.IsSelected) : 0;
                LoadBudgetIndexes();
                return string.Format(SELECTED_AGENCY_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllAgencyIndex;
        public bool IsSelectAllAgencyIndex
        {
            get => AgencyIndexes.Count() > 0 && AgencyIndexes.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllAgencyIndex, value);
                _isCheckAgency = true;
                foreach (DonViModel item in AgencyIndexes)
                {
                    item.IsSelected = _isSelectAllAgencyIndex;
                }
                OnPropertyChanged(nameof(SelectedAgencyIndexCount));
                _isCheckAgency = false;
            }
        }
        #endregion

        public bool IsExportEnable
        {
            get
            {
                if (Pages != null)
                    return Pages.Where(x => x.IsChecked).Count() > 0;
                return false;
            }
        }

        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
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

        private ObservableCollection<ComboboxItem> _dataInToiMuc;
        public ObservableCollection<ComboboxItem> DataInToiMuc
        {
            get => _dataInToiMuc;
            set => SetProperty(ref _dataInToiMuc, value);
        }

        private ComboboxItem _selectedInToiMuc;
        public ComboboxItem SelectedInToiMuc
        {
            get => _selectedInToiMuc;
            set => SetProperty(ref _selectedInToiMuc, value);
        }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintSummaryAgencyViewModel(ISessionService sessionService,
            IExportService exportService,
            INsDonViService donViService,
            INsMucLucNganSachService mucLucNganSachService,
            INsQtChungTuChiTietService chungTuChiTietService,
            IMapper mapper,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _sessionService = sessionService;
            _exportService = exportService;
            _donViService = donViService;
            _mucLucNganSachService = mucLucNganSachService;
            _chungTuChiTietService = chungTuChiTietService;
            _mapper = mapper;
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
                if (InMotToChecked)
                {
                    OnExportFileOnePaper(ExportType.PDF_ONE_PAPER);

                }
                else
                {
                    OnExportFile(ExportType.PDF);
                }
            });
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            base.Init();
            _reportType = SummaryAgencyReportType.A4;
            _sessionInfo = _sessionService.Current;
            _pages = new List<CheckBoxItem>();
            _budgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
            _listMucLucNganSach = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
            InitReportDefaultDate();
            LoadTieuDe();
            LoadDanhMuc();
            LoadChiTietToi();
            LoadMonthsAndQuarters();
            LoadAgencyIndexes();
            LoadBudgetIndexes();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_TONGHOP_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
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

        private void LoadChiTietToi()
        {
            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                string chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                DataInToiMuc = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi));
                _selectedInToiMuc = DataInToiMuc.FirstOrDefault(n => n.DisplayItem == "TNG");
            }
        }

        private void LoadMonthsAndQuarters()
        {
            _monthAndQuarters = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                _monthAndQuarters.Add(new ComboboxItem("Tháng " + i, i.ToString()));
            }
            _monthAndQuarters.Add(new ComboboxItem("Quý I", "3"));
            _monthAndQuarters.Add(new ComboboxItem("Quý II", "6"));
            _monthAndQuarters.Add(new ComboboxItem("Quý III", "9"));
            _monthAndQuarters.Add(new ComboboxItem("Quý IV", "12"));
            MonthAndQuartersSelected = _monthAndQuarters.First();
        }

        private void ProcessMonth()
        {
            if (MonthAndQuartersSelected.DisplayItem.Contains("Quý"))
            {
                if (IsAccumulated)
                {
                    _quarterMonthType = (int)QuarterMonth.QUARTER;
                    _quarterMonth = string.Join(",", Enumerable.Range(0, Convert.ToInt32(MonthAndQuartersSelected.ValueItem) + 1).ToList());
                    _quarterMonthBefore = string.Join(",", Enumerable.Range(0, Convert.ToInt32(MonthAndQuartersSelected.ValueItem) - 2).ToList());
                }
                else
                {
                    _quarterMonthType = (int)QuarterMonth.QUARTER;
                    _quarterMonth = string.Join(",", Enumerable.Range(Convert.ToInt32(MonthAndQuartersSelected.ValueItem) - 2, 3).ToList());
                    _quarterMonthBefore = string.Join(",", Enumerable.Range(0, Convert.ToInt32(MonthAndQuartersSelected.ValueItem) - 2).ToList());
                }

            }
            else
            {
                if (IsAccumulated)
                {
                    _quarterMonthType = (int)QuarterMonth.MONTH;
                    _quarterMonth = string.Join(",", Enumerable.Range(0, Convert.ToInt32(MonthAndQuartersSelected.ValueItem) + 1).ToList());
                    _quarterMonthBefore = string.Join(",", Enumerable.Range(0, Convert.ToInt32(MonthAndQuartersSelected.ValueItem)).ToList());
                }
                else
                {
                    _quarterMonthType = (int)QuarterMonth.MONTH;
                    _quarterMonth = MonthAndQuartersSelected.ValueItem;
                    _quarterMonthBefore = string.Join(",", Enumerable.Range(0, Convert.ToInt32(MonthAndQuartersSelected.ValueItem)).ToList());
                }
            }
        }

        private void LoadAgencyHashData()
        {
            LoadAgencies();

            List<DonVi> listDonViPBDuToanReport = _donViService.FindByEstimateSettlement(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, DateTime.Now, Convert.ToInt32(MonthAndQuartersSelected?.ValueItem), _quarterMonthType).ToList();
            List<DonVi> agencies = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.NOI_BO).ToList();

            agencies = agencies.Where(x => _lstMaDonViHasData.Select(x => x.IIDMaDonVi).Contains(x.IIDMaDonVi)).ToList();

            foreach (var item in listDonViPBDuToanReport)
            {
                if (!agencies.Select(x => x.IIDMaDonVi).Contains(item.IIDMaDonVi))
                {
                    agencies.Add(item);
                }
            }

            AgencyIndexes = _mapper.Map<ObservableCollection<DonViModel>>(agencies);
            _listAgencyIndex = CollectionViewSource.GetDefaultView(AgencyIndexes);
            _listAgencyIndex.Filter = ListAgencyIndexFilter;
            //var quarterMonthValue = _quarterMonth.Split(StringUtils.COMMA).ToList();
            //var dataFilters = _lstMaDonViHashData.Where(x => quarterMonthValue.Contains(x.IThangQuy.ToString())).ToList();
            foreach (var model in AgencyIndexes)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(DonViModel.IsSelected))
                    {
                        if (!_isCheckAgency)
                        {
                            OnPropertyChanged(nameof(SelectedAgencyIndexCount));
                            OnPropertyChanged(nameof(IsExportEnable));
                            OnPropertyChanged(nameof(IsSelectAllAgencyIndex));
                        }
                    }
                };
            }
            OnPropertyChanged(nameof(SelectedAgencyIndexCount));
            OnPropertyChanged(nameof(IsExportEnable));
            OnPropertyChanged(nameof(IsSelectAllAgencyIndex));

        }

        private void LoadAgencies()
        {
            bool hasDuToan = false;
            if (ReportType == SummaryAgencyReportType.EstimateSettlement)
                hasDuToan = true;
            DateTime voucherDate = new DateTime(_sessionInfo.YearOfWork, int.Parse(MonthAndQuartersSelected.ValueItem), DateTime.DaysInMonth(_sessionInfo.YearOfWork, int.Parse(MonthAndQuartersSelected.ValueItem)));
            _lstMaDonViHasData = _donViService.FindBySummaryAgencySettlement(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, null, _quarterMonth, voucherDate, hasDuToan).Where(n => n.Loai != LoaiDonVi.ROOT).ToList();

        }

        private void LoadBudgetIndexes()
        {
            bool hasDuToan = false;
            var listIdMaDonVi = !AgencyIndexes.IsEmpty() ? AgencyIndexes.Where(w => w.IsSelected).Select(w => w.IIDMaDonVi).ToList() : new List<string>();
            _pages = new List<CheckBoxItem>();
            OnPropertyChanged(nameof(Pages));
            if (ReportType == SummaryAgencyReportType.EstimateSettlement)
                hasDuToan = true;
            DateTime voucherDate = new DateTime(_sessionInfo.YearOfWork, int.Parse(MonthAndQuartersSelected.ValueItem), DateTime.DaysInMonth(_sessionInfo.YearOfWork, int.Parse(MonthAndQuartersSelected.ValueItem)));

            var lnsQueries = _mucLucNganSachService.FindBySettlementEstimateMonth(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, string.Join(",", listIdMaDonVi), MonthAndQuartersSelected?.ValueItem, _quarterMonthType);
            List<NsMucLucNganSach> listMucLuc = new List<NsMucLucNganSach>();
            List<string> lns = new List<string>();
            foreach (var item in lnsQueries)
            {
                lns.Add(item.LNS1);
                lns.Add(item.LNS3);
                lns.Add(item.LNS);
            }
            listMucLuc = lns.Distinct().OrderBy<string, string>((Func<string, string>)(x => x)).Select<string, NsMucLucNganSach>((Func<string, NsMucLucNganSach>)(x =>
            {
                var mlns = _listMucLucNganSach.FirstOrDefault<NsMucLucNganSach>((Func<NsMucLucNganSach, bool>)(m => m.XauNoiMa == x));
                return new NsMucLucNganSach()
                {
                    Lns = x,
                    XauNoiMa = x,
                    MoTa = mlns == null ? string.Empty : mlns.MoTa,
                    MlnsId = mlns == null ? Guid.Empty : (Guid)mlns.MlnsId,
                    MlnsIdParent = mlns == null ? Guid.Empty : (mlns.MlnsIdParent == null ? Guid.Empty : (Guid)mlns.MlnsIdParent)
                };
            })).Where<NsMucLucNganSach>((Func<NsMucLucNganSach, bool>)(x => !string.IsNullOrEmpty(x.MoTa))).ToList();

            /*List<NsMucLucNganSach> listLNS = _mucLucNganSachService.FindBySummaryAgencySettlement(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, _quarterMonth, voucherDate, hasDuToan, _sessionInfo.Principal, string.Join(",", listIdMaDonVi)).ToList();*/
            BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMucLuc);

            _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
            _listBudgetIndex.Filter = ListBudgetIndexFilter;
            foreach (var model in BudgetIndexes)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                    {
                        foreach (NsMuclucNgansachModel item in BudgetIndexes.Where(x => !x.BHangCha))
                        {
                            if (item.MlnsIdParent == model.MlnsId)
                                item.IsSelected = model.IsSelected;
                        }
                        if (!_isCheckLNS)
                        {
                            LoadPages();
                            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                            OnPropertyChanged(nameof(IsExportEnable));
                            OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                        }
                    }
                };
            }
            OnPropertyChanged(nameof(SelectedPageCount));
            OnPropertyChanged(nameof(IsSelectAllPage));
            OnPropertyChanged(nameof(IsExportEnable));
            OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
        }

        private void LoadAgencyIndexes()
        {
            LoadAgencies();
            List<DonVi> listDonViPBDuToanReport = _donViService.FindByEstimateSettlement(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, DateTime.Now, Convert.ToInt32(MonthAndQuartersSelected?.ValueItem), _quarterMonthType).ToList();
            List<DonVi> agencies = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.NOI_BO).ToList();
            var predicate = PredicateBuilder.True<NsQtChungTuChiTiet>();
            predicate = predicate.And(x => x.INamLamViec.Equals(_sessionInfo.YearOfWork));
            predicate = predicate.And(x => x.INamNganSach.Equals(_sessionInfo.YearOfBudget));
            predicate = predicate.And(x => x.IIdMaNguonNganSach.Equals(_sessionInfo.Budget));
            var datadonvi = _chungTuChiTietService.FindByCondition(predicate);
            _lstMaDonViHashData = datadonvi.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi)).GroupBy(g => new { g.IIdMaDonVi, g.IThangQuy, g.IThangQuyLoai }).Select(x => x.First()).ToList();

            //agencies = agencies.Where(x => _lstMaDonViHashData.Select(s => s.IIdMaDonVi).Contains(x.IIDMaDonVi)).ToList();
            agencies = agencies.Where(x => _lstMaDonViHasData.Select(s => s.IIDMaDonVi).Contains(x.IIDMaDonVi)).ToList();

            foreach (var item in listDonViPBDuToanReport)
            {
                if (!agencies.Select(x => x.IIDMaDonVi).Contains(item.IIDMaDonVi))
                {
                    agencies.Add(item);
                }
            }

            AgencyIndexes = _mapper.Map<ObservableCollection<DonViModel>>(agencies.OrderBy(x => x.IIDMaDonVi));
            _listAgencyIndex = CollectionViewSource.GetDefaultView(AgencyIndexes);
            _listAgencyIndex.Filter = ListAgencyIndexFilter;
            foreach (var model in AgencyIndexes)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(DonViModel.IsSelected))
                    {
                        if (!_isCheckAgency)
                        {
                            OnPropertyChanged(nameof(SelectedAgencyIndexCount));
                            OnPropertyChanged(nameof(IsExportEnable));
                            OnPropertyChanged(nameof(IsSelectAllAgencyIndex));
                        }
                    }
                };
            }

            OnPropertyChanged(nameof(IsExportEnable));
            OnPropertyChanged(nameof(IsSelectAllAgencyIndex));
        }

        private bool ListBudgetIndexFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchBudgetIndexText))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
        }

        private bool ListAgencyIndexFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchAgencyIndexText))
            {
                return true;
            }
            return obj is DonViModel item && item.TenDonViDisplay.ToLower().Contains(_searchAgencyIndexText!.ToLower());
        }

        private void LoadPages()
        {
            bool hasDuToan = false;
            if (ReportType == SummaryAgencyReportType.EstimateSettlement)
                hasDuToan = true;
            DateTime voucherDate = new DateTime(_sessionInfo.YearOfWork, int.Parse(MonthAndQuartersSelected.ValueItem), DateTime.DaysInMonth(_sessionInfo.YearOfWork, int.Parse(MonthAndQuartersSelected.ValueItem)));
            string lns = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns));
            _listDonVi = _mapper.Map<List<DonVi>>(AgencyIndexes.Where(item => item.IsSelected).ToList());

            /*_listDonVi = _donViService.FindBySummaryAgencySettlement(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, _quarterMonth, voucherDate, hasDuToan).Where(n => n.Loai != LoaiDonVi.ROOT)
                .Where(r=>_listDonViSelected.Contains(r.IIDMaDonVi)).ToList()*/

            var idsDonViQuanLy = _sessionService.Current.IdsDonViQuanLy;
            if (!_listDonVi.Any(x => idsDonViQuanLy.Contains(x.IIDMaDonVi) && x.Loai == LoaiDonVi.ROOT))
            {
                _listDonVi = _listDonVi.Where(x => idsDonViQuanLy.Contains(x.IIDMaDonVi)).ToList();
            }

            int firstPageCol = 0;
            int otherPageCol = 0;
            if (ReportType == SummaryAgencyReportType.A4)
            {
                firstPageCol = 3;
                otherPageCol = 6;
            }
            else if (ReportType == SummaryAgencyReportType.A3)
            {
                firstPageCol = 7;
                otherPageCol = 10;
            }
            else
            {
                firstPageCol = 3;
                otherPageCol = 3;
            }
            int countAgency = _listDonVi.Count();
            int pageCount = 0;
            _pages = new List<CheckBoxItem>();
            _dicAgency = new Dictionary<int, List<string>>();
            if (countAgency > 0)
            {
                pageCount++;
                _pages.Add(new CheckBoxItem { ValueItem = pageCount.ToString(), DisplayItem = "Tờ " + pageCount });
                List<string> listDonViId = new List<string>();
                int count = 0;
                for (int i = count; i < firstPageCol; i++)
                {
                    if (i < _listDonVi.Count)
                    {
                        listDonViId.Add(_listDonVi[i].IIDMaDonVi);
                        count++;
                    }
                    else break;
                }
                _dicAgency.Add(pageCount, listDonViId);

                if (countAgency > firstPageCol)
                {
                    countAgency = countAgency - firstPageCol;
                    while (countAgency >= otherPageCol)
                    {
                        listDonViId = new List<string>();
                        pageCount++;
                        _pages.Add(new CheckBoxItem { ValueItem = pageCount.ToString(), DisplayItem = "Tờ " + pageCount });
                        //for (int i = count; i < otherPageCol * pageCount; i++)
                        //{
                        //    if (i < _listDonVi.Count)
                        //    {
                        //        listDonViId.Add(_listDonVi[i].IIDMaDonVi);
                        //        count++;
                        //    }
                        //    else break;
                        //}
                        for (int i = 0; i < otherPageCol; i++)
                        {
                            if (count < _listDonVi.Count)
                            {
                                listDonViId.Add(_listDonVi[count].IIDMaDonVi);
                                count++;
                            }
                            else break;
                        }
                        _dicAgency.Add(pageCount, listDonViId);
                        countAgency = countAgency - otherPageCol;
                    }
                    if (countAgency != 0 && countAgency < otherPageCol)
                    {
                        listDonViId = new List<string>();
                        pageCount++;
                        _pages.Add(new CheckBoxItem { ValueItem = pageCount.ToString(), DisplayItem = "Tờ " + pageCount });
                        for (int i = count; count < _listDonVi.Count; i++)
                        {
                            if (i < _listDonVi.Count)
                            {
                                listDonViId.Add(_listDonVi[i].IIDMaDonVi);
                                count++;
                            }
                            else break;
                        }
                        _dicAgency.Add(pageCount, listDonViId);
                    }
                }
            }
            foreach (var model in Pages)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        OnPropertyChanged(nameof(SelectedPageCount));
                        OnPropertyChanged(nameof(IsSelectAllPage));
                        OnPropertyChanged(nameof(IsExportEnable));
                    }
                };
            }
            OnPropertyChanged(nameof(Pages));
            OnPropertyChanged(nameof(SelectedPageCount));
            OnPropertyChanged(nameof(IsSelectAllPage));
            OnPropertyChanged(nameof(IsExportEnable));
        }

        private void OnExportFile(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                int dvt = Convert.ToInt32(SelectedUnit.ValueItem);
                var agencies = AgencyIndexes.Any(x => x.IsSelected) ? AgencyIndexes.Where(x => x.IsSelected).Select(s => s.IIDMaDonVi).ToList() : new List<string>
                {
                    _sessionInfo.IdDonVi
                };

                ReportSettlementCriteria condition = new ReportSettlementCriteria
                {
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    BudgetSource = _sessionInfo.Budget,
                    AgencyId = string.Join(StringUtils.COMMA, agencies),
                    EstimateAgencyId = string.Join(StringUtils.COMMA, agencies),
                    QuarterMonthType = _quarterMonthType,
                    QuarterMonth = _quarterMonth,
                    QuarterMonthBefore = _quarterMonthBefore,
                    LNS = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns)),
                    VoucherDate = new DateTime(_sessionInfo.YearOfWork, int.Parse(MonthAndQuartersSelected.ValueItem), DateTime.DaysInMonth(_sessionInfo.YearOfWork, int.Parse(MonthAndQuartersSelected.ValueItem))),
                    Dvt = dvt,
                    IsAccumulated = IsAccumulated ? 1 : 0
                };
                List<ReportQuyetToanTongHopDonViQuery> reportData = _chungTuChiTietService.FindForSummaryAgencyReport(condition);
                string exportFilePrefixName = string.Empty;
                string fileName = string.Empty;
                string templateName = string.Empty;
                if (exportType == ExportType.EXCEL)
                {
                    templateName = Path.Combine(GetPath(ExportFileName.RPT_NS_QUYETTOAN_TONGHOP_DONVI_EXCEL));
                    exportFilePrefixName = ExportFileName.RPT_NS_QUYETTOAN_TONGHOP_DONVI_EXCEL.Split(".").First();
                    fileName = StringUtils.CreateExportFileName(exportFilePrefixName);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data = ExportPage(NSConstants.DEFAULT_INDEX, reportData, dvt, exportType);
                    List<int> hideColumns = ExportExcelHelper<QuyetToanTongHopDonVi>.HideColumn(SelectedInToiMuc.ValueItem);
                    var xlsFile = _exportService.Export<QuyetToanTongHopDonVi, QuyetToanTongHopDonViHeader, QuyetToanTongHopDonViTotal, QuyetToanTongHopDonViHeaderDynamic, QuyetToanDynamicValue, HeaderReportDivisionCurrentBatch>(templateName, data, hideColumns);
                    results.Add(new ExportResult(fileName, fileName, null, xlsFile));
                }
                else
                {
                    foreach (var page in Pages.Where(x => x.IsChecked))
                    {


                        if (int.Parse(page.ValueItem) == 1)
                        {
                            templateName = Path.Combine(GetPath(ExportFileName.RPT_NS_QUYETTOAN_TONGHOP_DONVI));
                            exportFilePrefixName = ExportFileName.RPT_NS_QUYETTOAN_TONGHOP_DONVI.Split(".").First();
                            fileName = StringUtils.CreateExportFileName(exportFilePrefixName);
                        }
                        else
                        {
                            templateName = Path.Combine(GetPath(ExportFileName.RPT_NS_QUYETTOAN_TONGHOP_DONVI_TRANG));
                            exportFilePrefixName = ExportFileName.RPT_NS_QUYETTOAN_TONGHOP_DONVI_TRANG.Split(".").First() + "_" + page.ValueItem;
                            fileName = StringUtils.CreateExportFileName(exportFilePrefixName);
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data = ExportPage(int.Parse(page.ValueItem), reportData, dvt, exportType);
                        List<int> hideColumns = ExportExcelHelper<QuyetToanTongHopDonVi>.HideColumn(SelectedInToiMuc.ValueItem);
                        var xlsFile = _exportService.Export<QuyetToanTongHopDonVi, QuyetToanTongHopDonViHeader, QuyetToanTongHopDonViTotal, QuyetToanTongHopDonViHeaderDynamic, QuyetToanDynamicValue, HeaderReportDivisionCurrentBatch>(templateName, data, hideColumns);
                        results.Add(new ExportResult(fileName, fileName, null, xlsFile));
                    }
                }

                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    if (result != null && result.Count > 0)
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


        private void OnExportFileOnePaper(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                int dvt = Convert.ToInt32(SelectedUnit.ValueItem);
                var agencies = AgencyIndexes.Any(x => x.IsSelected) ? AgencyIndexes.Where(x => x.IsSelected).Select(s => s.IIDMaDonVi).ToList() : new List<string>
                {
                    _sessionInfo.IdDonVi
                };
                ReportSettlementCriteria condition = new ReportSettlementCriteria
                {
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    BudgetSource = _sessionInfo.Budget,
                    AgencyId = string.Join(StringUtils.COMMA, agencies),
                    EstimateAgencyId = string.Join(StringUtils.COMMA, agencies),
                    QuarterMonthType = _quarterMonthType,
                    QuarterMonth = _quarterMonth,
                    QuarterMonthBefore = _quarterMonthBefore,
                    LNS = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns)),
                    VoucherDate = new DateTime(_sessionInfo.YearOfWork, int.Parse(MonthAndQuartersSelected.ValueItem), DateTime.DaysInMonth(_sessionInfo.YearOfWork, int.Parse(MonthAndQuartersSelected.ValueItem))),
                    Dvt = dvt
                };
                List<ReportQuyetToanTongHopDonViQuery> reportData = _chungTuChiTietService.FindForSummaryAgencyReport(condition);
                foreach (var page in Pages.Where(x => x.IsChecked))
                {
                    string exportFilePrefixName = string.Empty;
                    string fileName = string.Empty;
                    string templateName = string.Empty;
                    if (int.Parse(page.ValueItem) == 1)
                    {
                        templateName = Path.Combine(GetPath(ExportFileName.RPT_NS_QUYETTOAN_TONGHOP_DONVI_ONEPAPER));
                        exportFilePrefixName = ExportFileName.RPT_NS_QUYETTOAN_TONGHOP_DONVI_ONEPAPER.Split(".").First();
                        fileName = StringUtils.CreateExportFileName(exportFilePrefixName);
                    }
                    else
                    {
                        templateName = Path.Combine(GetPath(ExportFileName.RPT_NS_QUYETTOAN_TONGHOP_DONVI_TRANG_ONEPAPER));
                        exportFilePrefixName = ExportFileName.RPT_NS_QUYETTOAN_TONGHOP_DONVI_TRANG_ONEPAPER.Split(".").First() + "_" + page.ValueItem;
                        fileName = StringUtils.CreateExportFileName(exportFilePrefixName);
                    }
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data = ExportPage(int.Parse(page.ValueItem), reportData, dvt, exportType);
                    List<int> hideColumns = ExportExcelHelper<QuyetToanTongHopDonVi>.HideColumn(SelectedInToiMuc.ValueItem);
                    var xlsFile = _exportService.Export<QuyetToanTongHopDonVi, QuyetToanTongHopDonViHeader, QuyetToanTongHopDonViTotal>(templateName, data, hideColumns);
                    results.Add(new ExportResult(fileName, fileName, null, xlsFile));
                }
                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    if (result != null && result.Count > 0)
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


        public string GetPath(string input)
        {
            if (ReportType == SummaryAgencyReportType.A4)
                input = input + "_A4";
            else if (ReportType == SummaryAgencyReportType.A3)
                input = input + "_A3";
            else input = input + "_DuToan_A3";
            return Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, input + FileExtensionFormats.Xlsx);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_TONGHOP_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_QUYETTOAN_TONGHOP_DONVI;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private Dictionary<string, object> ExportPage(int page, List<ReportQuyetToanTongHopDonViQuery> reportData, int dvt, ExportType exportType = ExportType.PDF)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            List<string> listDonViByPage = _dicAgency[page];
            List<string> listDonViByPageExcel = _listDonVi.Select(x => x.IIDMaDonVi).ToList();
            List<string> listXauNoiMaDonVi = new List<string>();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_TONGHOP_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            listXauNoiMaDonVi = reportData.Where(x => !string.IsNullOrEmpty(x.IdMaDonVi)).Select(x => { string xauNoiMaDonVi = string.Join(StringUtils.DELIMITER, x.XauNoiMa, x.IdMaDonVi); return xauNoiMaDonVi; }).Distinct().ToList();
            _exportData = new List<QuyetToanTongHopDonVi>();

            foreach (var item in reportData.Where(x => string.IsNullOrEmpty(x.IdMaDonVi)))
            {
                var dataDuToan = new QuyetToanTongHopDonVi
                {
                    LNS = item.LNS,
                    L = item.L,
                    K = item.K,
                    M = item.M,
                    TM = item.TM,
                    TTM = item.TTM,
                    NG = item.NG,
                    TNG = item.TNG,
                    TNG1 = item.TNG1,
                    TNG2 = item.TNG2,
                    TNG3 = item.TNG3,
                    XauNoiMa = item.XauNoiMa,
                    MlnsId = item.MlnsId,
                    MlnsIdParent = item.MlnsIdCha,
                    MoTa = item.MoTa,
                    IsHangCha = item.IsHangCha,
                    BHangChaDuToan = item.BHangChaDuToan,
                    BHangChaQuyetToan = item.BHangChaQuyetToan
                };
                _exportData.Add(dataDuToan);
            }
            if (exportType == ExportType.EXCEL)
            {
                HandlerDataExcel(reportData, listDonViByPageExcel, listXauNoiMaDonVi);
            }
            else
            {
                _iNumListValue = NSConstants.ZERO;
                foreach (var xauNoiMa in reportData.Where(x => !string.IsNullOrEmpty(x.IdMaDonVi)).Select(x => x.XauNoiMa).Distinct())
                {
                    var xnm = reportData.Where(x => !string.IsNullOrEmpty(x.IdMaDonVi) && x.XauNoiMa == xauNoiMa).FirstOrDefault();

                    if (xnm != null)
                    {
                        foreach (var xauNoiMaDonVi in listXauNoiMaDonVi.Where(x => x.Split(StringUtils.DELIMITER).First() == xauNoiMa))
                        {
                            var reportDataByXauNoiMa = reportData.Where(x => string.Join(StringUtils.DELIMITER, x.XauNoiMa, x.IdMaDonVi) == xauNoiMaDonVi).FirstOrDefault();
                            if (reportDataByXauNoiMa != null)
                            {
                                var index = listDonViByPage.IndexOf(reportDataByXauNoiMa.IdMaDonVi);
                                QuyetToanTongHopDonVi dataQuyetToan = new QuyetToanTongHopDonVi();
                                bool isCreate = true;
                                if (_exportData.Any(x => x.XauNoiMa == reportDataByXauNoiMa.XauNoiMa && x.IsHangCha == reportDataByXauNoiMa.IsHangCha))
                                {
                                    isCreate = false;
                                    dataQuyetToan = _exportData.Where(x => x.XauNoiMa == reportDataByXauNoiMa.XauNoiMa && x.IsHangCha == reportDataByXauNoiMa.IsHangCha).First();
                                }
                                else dataQuyetToan = new QuyetToanTongHopDonVi
                                {
                                    XauNoiMa = xnm.XauNoiMa,
                                    MlnsId = xnm.MlnsId,
                                    MlnsIdParent = xnm.MlnsIdCha,
                                    LNS = xnm.LNS,
                                    L = xnm.L,
                                    K = xnm.K,
                                    M = xnm.M,
                                    TM = xnm.TM,
                                    TTM = xnm.TTM,
                                    NG = xnm.NG,
                                    TNG = xnm.TNG,
                                    TNG1 = xnm.TNG1,
                                    TNG2 = xnm.TNG2,
                                    TNG3 = xnm.TNG3,
                                    IsHangCha = xnm.IsHangCha,
                                    BHangChaDuToan = xnm.BHangChaDuToan,
                                    BHangChaQuyetToan = xnm.BHangChaQuyetToan,
                                    MoTa = reportDataByXauNoiMa.MoTa,
                                    MaDonVi = reportDataByXauNoiMa.IdMaDonVi,
                                    DuToan = xnm.DuToan,
                                    QuyetToan = xnm.QuyetToan,
                                    TrongKy = xnm.TrongKy
                                };
                                double quyetToanDonVi = reportDataByXauNoiMa.QuyetToanDonVi;
                                double duToanDonVi = reportDataByXauNoiMa.DuToanDonVi;

                                switch (index)
                                {
                                    case 0:
                                        dataQuyetToan.QuyetToan1 = quyetToanDonVi;
                                        dataQuyetToan.DuToan1 = duToanDonVi;
                                        break;
                                    case 1:
                                        dataQuyetToan.QuyetToan2 = quyetToanDonVi;
                                        dataQuyetToan.DuToan2 = duToanDonVi;
                                        break;
                                    case 2:
                                        dataQuyetToan.QuyetToan3 = quyetToanDonVi;
                                        dataQuyetToan.DuToan3 = duToanDonVi;
                                        break;
                                    case 3:
                                        dataQuyetToan.QuyetToan4 = quyetToanDonVi;
                                        dataQuyetToan.DuToan4 = duToanDonVi;
                                        break;
                                    case 4:
                                        dataQuyetToan.QuyetToan5 = quyetToanDonVi;
                                        dataQuyetToan.DuToan5 = duToanDonVi;
                                        break;
                                    case 5:
                                        dataQuyetToan.QuyetToan6 = quyetToanDonVi;
                                        dataQuyetToan.DuToan6 = duToanDonVi;
                                        break;
                                    case 6:
                                        dataQuyetToan.QuyetToan7 = quyetToanDonVi;
                                        dataQuyetToan.DuToan7 = duToanDonVi;
                                        break;
                                    case 7:
                                        dataQuyetToan.QuyetToan8 = quyetToanDonVi;
                                        dataQuyetToan.DuToan8 = duToanDonVi;
                                        break;
                                    case 8:
                                        dataQuyetToan.QuyetToan9 = quyetToanDonVi;
                                        dataQuyetToan.DuToan9 = duToanDonVi;
                                        break;
                                    case 9:
                                        dataQuyetToan.QuyetToan10 = quyetToanDonVi;
                                        dataQuyetToan.DuToan10 = duToanDonVi;
                                        break;
                                }
                                if (isCreate)
                                    _exportData.Add(dataQuyetToan);
                            }
                        }
                    }
                }
            }
            _exportData = _exportData.OrderBy(x => x.XauNoiMa).ToList();
            CalculateDataLNS();
            CalculateTotal(_exportData);
            switch (SelectedInToiMuc.ValueItem)
            {
                case nameof(MLNSFiled.NG):
                    _exportData = _exportData.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                    _exportData.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                    break;
                case nameof(MLNSFiled.TNG):
                    _exportData = _exportData.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                    _exportData.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                    break;
                case nameof(MLNSFiled.TNG1):
                    _exportData = _exportData.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                    _exportData.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                    break;
                case nameof(MLNSFiled.TNG2):
                    _exportData = _exportData.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                    _exportData.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                    break;
            }
            if (page == 1)
                _exportData = _exportData.Where(x => x.HasAllData).ToList();
            else
            {                
                _exportData = _exportData.Where(x => x.HasAllData).ToList();
                /*
                if (ReportType == SummaryAgencyReportType.EstimateSettlement)
                    _exportData = _exportData.Where(x => x.HasDataQuyetToan || x.HasDataDuToan).ToList();
                else _exportData = _exportData.Where(x => x.HasDataQuyetToan).ToList();
                */
            }
            // các item có loại L khác null
            // nếu có bố khác null và bố k có cả L và K -> show L và K của item
            // nếu có bố khác null và bố có cả L và K ->  ẩn L và K của item
            foreach (var item in _exportData.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
            {
                var parent = _exportData.Where(x => x.MlnsId == item.MlnsIdParent).LastOrDefault();
                if (parent != null)
                {
                    if (!string.IsNullOrEmpty(parent.L) && !string.IsNullOrEmpty(parent.K))
                    {
                        item.L = string.Empty;
                        item.K = string.Empty;
                        item.LNS = string.Empty;
                    }
                    else
                    {
                        item.LNS = item.L + StringUtils.DIVISION + item.K;
                    }

                    if (!string.IsNullOrEmpty(parent.M))
                        item.M = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TM))
                        item.TM = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TTM))
                        item.TTM = string.Empty;
                    if (!string.IsNullOrEmpty(parent.NG))
                        item.NG = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TNG))
                        item.TNG = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TNG1))
                        item.TNG1 = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TNG2))
                        item.TNG2 = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TNG3))
                        item.TNG3 = string.Empty;
                }
            }
            QuyetToanTongHopDonViHeader header = new QuyetToanTongHopDonViHeader();
            List<QuyetToanTongHopDonViHeaderDynamic> listHeader2 = new List<QuyetToanTongHopDonViHeaderDynamic>();
            List<QuyetToanTongHopDonViHeaderDynamic> ListHeaderChild = new List<QuyetToanTongHopDonViHeaderDynamic>();
            if (exportType == ExportType.EXCEL)
            {
                if (ReportType == SummaryAgencyReportType.EstimateSettlement)
                {
                    int colStart = 18;
                    for (int i = 0; i < (_iNumListValue + 1) / 3; i++)
                    {
                        var columnStartName = GetExcelColumnName(colStart);
                        var columnEndName = GetExcelColumnName(colStart + 2);
                        string mergerange = $"{columnStartName}7:{columnEndName}7";
                        //Header 2
                        listHeader2.Add(new QuyetToanTongHopDonViHeaderDynamic { Header = listDonViByPageExcel.Count > 0 && !string.IsNullOrEmpty(listDonViByPageExcel[i]) ? _listDonVi.Where(x => x.IIDMaDonVi == listDonViByPageExcel[i]).First().MaTenDonVi : string.Empty, STT = i + 1, MergeRange = mergerange });
                        listHeader2.Add(new QuyetToanTongHopDonViHeaderDynamic());
                        listHeader2.Add(new QuyetToanTongHopDonViHeaderDynamic());
                        //Header 3
                        ListHeaderChild.Add(new QuyetToanTongHopDonViHeaderDynamic { Header = "Dự toán" });
                        ListHeaderChild.Add(new QuyetToanTongHopDonViHeaderDynamic { Header = "Quyết toán" });
                        ListHeaderChild.Add(new QuyetToanTongHopDonViHeaderDynamic { Header = "Chênh lệch" });
                        //colStart += 3;
                    }
                }
                else
                {
                    for (int i = 0; i < _iNumListValue; i++)
                    {
                        listHeader2.Add(new QuyetToanTongHopDonViHeaderDynamic { Header = listDonViByPageExcel.Count > 0 && !string.IsNullOrEmpty(listDonViByPageExcel[i]) ? _listDonVi.Where(x => x.IIDMaDonVi == listDonViByPageExcel[i]).First().MaTenDonVi : string.Empty });
                    }
                }

            }
            else
            {
                header = new QuyetToanTongHopDonViHeader
                {
                    Header1 = listDonViByPage.Count > 0 && !string.IsNullOrEmpty(listDonViByPage[0]) ? _listDonVi.Where(x => x.IIDMaDonVi == listDonViByPage[0]).First().MaTenDonVi : string.Empty,
                    Header2 = listDonViByPage.Count > 1 && !string.IsNullOrEmpty(listDonViByPage[1]) ? _listDonVi.Where(x => x.IIDMaDonVi == listDonViByPage[1]).First().MaTenDonVi : string.Empty,
                    Header3 = listDonViByPage.Count > 2 && !string.IsNullOrEmpty(listDonViByPage[2]) ? _listDonVi.Where(x => x.IIDMaDonVi == listDonViByPage[2]).First().MaTenDonVi : string.Empty,
                    Header4 = listDonViByPage.Count > 3 && !string.IsNullOrEmpty(listDonViByPage[3]) ? _listDonVi.Where(x => x.IIDMaDonVi == listDonViByPage[3]).First().MaTenDonVi : string.Empty,
                    Header5 = listDonViByPage.Count > 4 && !string.IsNullOrEmpty(listDonViByPage[4]) ? _listDonVi.Where(x => x.IIDMaDonVi == listDonViByPage[4]).First().MaTenDonVi : string.Empty,
                    Header6 = listDonViByPage.Count > 5 && !string.IsNullOrEmpty(listDonViByPage[5]) ? _listDonVi.Where(x => x.IIDMaDonVi == listDonViByPage[5]).First().MaTenDonVi : string.Empty,
                    Header7 = listDonViByPage.Count > 6 && !string.IsNullOrEmpty(listDonViByPage[6]) ? _listDonVi.Where(x => x.IIDMaDonVi == listDonViByPage[6]).First().MaTenDonVi : string.Empty,
                    Header8 = listDonViByPage.Count > 7 && !string.IsNullOrEmpty(listDonViByPage[7]) ? _listDonVi.Where(x => x.IIDMaDonVi == listDonViByPage[7]).First().MaTenDonVi : string.Empty,
                    Header9 = listDonViByPage.Count > 8 && !string.IsNullOrEmpty(listDonViByPage[8]) ? _listDonVi.Where(x => x.IIDMaDonVi == listDonViByPage[8]).First().MaTenDonVi : string.Empty,
                    Header10 = listDonViByPage.Count > 9 && !string.IsNullOrEmpty(listDonViByPage[9]) ? _listDonVi.Where(x => x.IIDMaDonVi == listDonViByPage[9]).First().MaTenDonVi : string.Empty,
                };
            }
            _exportData.Select(x =>
            {
                if (x.MlnsIdParent is null)
                {
                    x.MlnsIdParent = Guid.Empty;
                }
                return x;
            }).ToList();

            int columnStart = 18;
            var ColNameStart = GetExcelColumnName(columnStart);
            int columnEnd = columnStart + (_iNumListValue == 0 ? 0 : _iNumListValue - 1);
            var ColNameEnd = GetExcelColumnName(columnEnd);
            var mergeRange = string.Empty;
            if (ReportType == SummaryAgencyReportType.EstimateSettlement)
            {
                mergeRange = string.Format("{0}6:{1}6", ColNameStart, ColNameEnd);

            }
            else
            {
                mergeRange = string.Format("{0}7:{1}7", ColNameStart, ColNameEnd);

            }
            var lstColHeader = new List<HeaderReportDivisionCurrentBatch>();
            lstColHeader.Insert(0, new HeaderReportDivisionCurrentBatch { SSoQuyetDinh = Header, MergeRange = mergeRange, STT = 1 });
            for (var iNum = 0; iNum < _iNumListValue - 1; iNum++)
            {
                lstColHeader.Add(new HeaderReportDivisionCurrentBatch());
            }
            List<QuyetToanTongHopDonViHeader> headers = new List<QuyetToanTongHopDonViHeader> { header };
            List<QuyetToanTongHopDonViTotal> totals = new List<QuyetToanTongHopDonViTotal> { _total };
            FormatNumber formatNumber = new FormatNumber(dvt, exportType);
            data.Add("FormatNumber", formatNumber);
            data.Add("Cap1", _cap1);
            data.Add("Cap2", _sessionInfo.TenDonVi);
            data.Add("Headers", headers);
            data.Add("TieuDe1", Title1);
            data.Add("TieuDe2", Title2);
            data.Add("TieuDe3", Title3);
            data.Add("dvt", SelectedUnit.DisplayItem);
            data.Add("to", string.Format("{0}/{1}", page, Pages.Count));
            data.Add("Items", _exportData);
            data.Add("Total", totals);
            data.Add("Tien", StringUtils.NumberToText(totals.First().TongTrongKy * dvt));
            data.Add("DiaDiem", _diaDiem);
            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
            data.Add("ListValueTotal", listHeader2);
            data.Add("ListHeader2", listHeader2);
            data.Add("ListHeader", lstColHeader);
            data.Add("ListHeaderChild", ListHeaderChild);
            data.Add("Count", 10000);
            return data;
        }

        private void HandlerDataExcel(List<ReportQuyetToanTongHopDonViQuery> reportData, List<string> listDonViByPageExcel, List<string> listXauNoiMaDonVi)
        {
            foreach (var xauNoiMa in reportData.Where(x => !string.IsNullOrEmpty(x.IdMaDonVi)).Select(x => x.XauNoiMa).Distinct())
            {
                var xnm = reportData.Where(x => !string.IsNullOrEmpty(x.IdMaDonVi) && x.XauNoiMa == xauNoiMa).FirstOrDefault();

                if (xnm != null)
                {
                    foreach (var xauNoiMaDonVi in listXauNoiMaDonVi.Where(x => x.Split(StringUtils.DELIMITER).First() == xauNoiMa))
                    {
                        var reportDataByXauNoiMa = reportData.Where(x => string.Join(StringUtils.DELIMITER, x.XauNoiMa, x.IdMaDonVi) == xauNoiMaDonVi).FirstOrDefault();
                        if (reportDataByXauNoiMa != null)
                        {
                            var index = listDonViByPageExcel.IndexOf(reportDataByXauNoiMa.IdMaDonVi);
                            QuyetToanTongHopDonVi dataQuyetToan = new QuyetToanTongHopDonVi();
                            bool isCreate = true;
                            if (_exportData.Any(x => x.XauNoiMa == reportDataByXauNoiMa.XauNoiMa && x.IsHangCha == reportDataByXauNoiMa.IsHangCha))
                            {
                                isCreate = false;
                                dataQuyetToan = _exportData.Where(x => x.XauNoiMa == reportDataByXauNoiMa.XauNoiMa && x.IsHangCha == reportDataByXauNoiMa.IsHangCha).First();
                            }
                            else dataQuyetToan = new QuyetToanTongHopDonVi
                            {
                                XauNoiMa = xnm.XauNoiMa,
                                MlnsId = xnm.MlnsId,
                                MlnsIdParent = xnm.MlnsIdCha,
                                LNS = xnm.LNS,
                                L = xnm.L,
                                K = xnm.K,
                                M = xnm.M,
                                TM = xnm.TM,
                                TTM = xnm.TTM,
                                NG = xnm.NG,
                                TNG = xnm.TNG,
                                TNG1 = xnm.TNG1,
                                TNG2 = xnm.TNG2,
                                TNG3 = xnm.TNG3,
                                IsHangCha = xnm.IsHangCha,
                                BHangChaDuToan = xnm.BHangChaDuToan,
                                BHangChaQuyetToan = xnm.BHangChaQuyetToan,
                                MoTa = reportDataByXauNoiMa.MoTa,
                                MaDonVi = reportDataByXauNoiMa.IdMaDonVi,
                                DuToan = xnm.DuToan,
                                QuyetToan = xnm.QuyetToan,
                                TrongKy = xnm.TrongKy
                            };
                            double quyetToanDonVi = reportDataByXauNoiMa.QuyetToanDonVi;
                            double duToanDonVi = reportDataByXauNoiMa.DuToanDonVi;
                            if (index != NSConstants.NOT_FOUND_INDEX)
                            {
                                if (ReportType == SummaryAgencyReportType.EstimateSettlement)
                                {
                                    index *= 3; // *3 vi bao cao nay co 3 value Động
                                }
                                dataQuyetToan.IndexAgency = index;
                                var iNum = dataQuyetToan.LstValue.Count;
                                if (dataQuyetToan.LstValue.Any() && iNum > index)
                                {
                                    if (ReportType == SummaryAgencyReportType.EstimateSettlement) 
                                    {

                                        dataQuyetToan.LstValue.RemoveAt(index + 2);
                                        dataQuyetToan.LstValue.RemoveAt(index + 1);
                                        dataQuyetToan.LstValue.RemoveAt(index);

                                        dataQuyetToan.LstValue.Insert(index, new QuyetToanDynamicValue { QuyetToanValue = quyetToanDonVi, DuToanValue = duToanDonVi, FvalCommon = duToanDonVi });
                                        dataQuyetToan.LstValue.Insert(index + 1, new QuyetToanDynamicValue { QuyetToanValue = quyetToanDonVi, DuToanValue = duToanDonVi, FvalCommon = quyetToanDonVi });
                                        dataQuyetToan.LstValue.Insert(index + 2, new QuyetToanDynamicValue { QuyetToanValue = quyetToanDonVi, DuToanValue = duToanDonVi, FvalCommon = duToanDonVi - quyetToanDonVi });
                                        dataQuyetToan.IndexAgency = index + 2;
                                    }
                                    else
                                    {
                                        dataQuyetToan.LstValue.RemoveAt(index);
                                        dataQuyetToan.LstValue.Insert(index, new QuyetToanDynamicValue { QuyetToanValue = quyetToanDonVi, DuToanValue = duToanDonVi });

                                    }

                                }
                                else
                                {
                                    if (index != 0)
                                    {
                                        for (int i = 0; i < index - iNum; i++)
                                        {
                                            dataQuyetToan.LstValue.Add(new QuyetToanDynamicValue());
                                        }
                                    }
                                    if (ReportType == SummaryAgencyReportType.EstimateSettlement)
                                    {
                                        dataQuyetToan.LstValue.Insert(index, new QuyetToanDynamicValue { QuyetToanValue = quyetToanDonVi, DuToanValue = duToanDonVi, FvalCommon = duToanDonVi });
                                        dataQuyetToan.LstValue.Insert(index + 1, new QuyetToanDynamicValue { QuyetToanValue = quyetToanDonVi, DuToanValue = duToanDonVi, FvalCommon = quyetToanDonVi });
                                        dataQuyetToan.LstValue.Insert(index + 2, new QuyetToanDynamicValue { QuyetToanValue = quyetToanDonVi, DuToanValue = duToanDonVi, FvalCommon = duToanDonVi - quyetToanDonVi });

                                    }
                                    else
                                    {
                                        dataQuyetToan.LstValue.Insert(index, new QuyetToanDynamicValue { QuyetToanValue = quyetToanDonVi, DuToanValue = duToanDonVi });

                                    }
                                }
                            }
                            if (isCreate)
                                _exportData.Add(dataQuyetToan);
                        }
                    }
                }
            }

            _iNumListValue = _exportData.Max(x => x.LstValue.Count);
            _exportData.ForEach(x =>
            {
                var iCount = x.LstValue.Count;
                if (iCount < _iNumListValue)
                {
                    for (int i = 0; i < _iNumListValue - iCount; i++)
                    {
                        x.LstValue.Add(new QuyetToanDynamicValue());
                    }
                }
            });
        }

        private void CalculateDataLNS()
        {
            foreach (var item in _exportData.Where(x => !x.IsHangCha || (x.IsHangCha && (!x.BHangChaDuToan || !x.BHangChaQuyetToan))))
            {
                var data = _exportData.Where(x => x.XauNoiMa == item.XauNoiMa).ToList();
                if (data.Count > 0)
                {
                    item.DuToan = data.First().DuToan;
                    item.QuyetToan = data.First().QuyetToan;
                    item.TrongKy = data.First().TrongKy;
                    item.QuyetToan1 = data.Sum(x => x.QuyetToan1);
                    item.QuyetToan2 = data.Sum(x => x.QuyetToan2);
                    item.QuyetToan3 = data.Sum(x => x.QuyetToan3);
                    item.QuyetToan4 = data.Sum(x => x.QuyetToan4);
                    item.QuyetToan5 = data.Sum(x => x.QuyetToan5);
                    item.QuyetToan6 = data.Sum(x => x.QuyetToan6);
                    item.QuyetToan7 = data.Sum(x => x.QuyetToan7);
                    item.QuyetToan8 = data.Sum(x => x.QuyetToan8);
                    item.QuyetToan9 = data.Sum(x => x.QuyetToan9);
                    item.QuyetToan10 = data.Sum(x => x.QuyetToan10);
                    item.DuToan1 = data.Sum(x => x.DuToan1);
                    item.DuToan2 = data.Sum(x => x.DuToan2);
                    item.DuToan3 = data.Sum(x => x.DuToan3);
                    item.DuToan4 = data.Sum(x => x.DuToan4);
                    item.DuToan5 = data.Sum(x => x.DuToan5);
                    item.DuToan6 = data.Sum(x => x.DuToan6);
                    item.DuToan7 = data.Sum(x => x.DuToan7);
                    item.DuToan8 = data.Sum(x => x.DuToan8);
                    item.DuToan9 = data.Sum(x => x.DuToan9);
                    item.DuToan10 = data.Sum(x => x.DuToan10);
                    //for (int iNum = 0; iNum < _iNumListValue; iNum++)
                    //{
                    //    item.LstValue[iNum].QuyetToanValue = data.Sum(x => x.LstValue[iNum].QuyetToanValue);
                    //    item.LstValue[iNum].DuToanValue = data.Sum(x => x.LstValue[iNum].DuToanValue);
                    //}
                }
            }
            _exportData.Where(x => x.IsHangCha && x.BHangChaDuToan && x.BHangChaQuyetToan).Select(x =>
            {
                x.DuToan = x.DuToanOrigin != 0 ? x.DuToanOrigin : 0;
                x.QuyetToan = 0;
                x.TrongKy = 0;
                x.QuyetToan1 = 0;
                x.QuyetToan2 = 0;
                x.QuyetToan3 = 0;
                x.QuyetToan4 = 0;
                x.QuyetToan5 = 0;
                x.QuyetToan6 = 0;
                x.QuyetToan7 = 0;
                x.QuyetToan8 = 0;
                x.QuyetToan9 = 0;
                x.QuyetToan10 = 0;
                x.DuToan1 = 0;
                x.DuToan2 = 0;
                x.DuToan3 = 0;
                x.DuToan4 = 0;
                x.DuToan5 = 0;
                x.DuToan6 = 0;
                x.DuToan7 = 0;
                x.DuToan8 = 0;
                x.DuToan9 = 0;
                x.DuToan10 = 0;
                x.LstValue = SetListValueQuyetToanParent();
                return x;
            }).ToList();
            foreach (var item in _exportData.Where(x => !x.IsHangCha || x.DuToanOrigin != 0))
            {
                CalculateParent(item, item);
            }
        }

        private List<QuyetToanDynamicValue> SetListValueQuyetToanParent()
        {
            List<QuyetToanDynamicValue> results = new List<QuyetToanDynamicValue>();
            for (int i = 0; i < _iNumListValue; i++)
            {
                results.Add(new QuyetToanDynamicValue());
            }
            return results;
        }

        private string GetExcelColumnName(int columnNumber)
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

        private void CalculateParent(QuyetToanTongHopDonVi currentItem, QuyetToanTongHopDonVi selfItem)
        {
            var parentItem = _exportData.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            if (selfItem.DuToanOrigin != 0)
                parentItem.DuToan += selfItem.DuToan;
            parentItem.QuyetToan += selfItem.QuyetToan;
            parentItem.TrongKy += selfItem.TrongKy;
            parentItem.QuyetToan1 += selfItem.QuyetToan1;
            parentItem.QuyetToan2 += selfItem.QuyetToan2;
            parentItem.QuyetToan3 += selfItem.QuyetToan3;
            parentItem.QuyetToan4 += selfItem.QuyetToan4;
            parentItem.QuyetToan5 += selfItem.QuyetToan5;
            parentItem.QuyetToan6 += selfItem.QuyetToan6;
            parentItem.QuyetToan7 += selfItem.QuyetToan7;
            parentItem.QuyetToan8 += selfItem.QuyetToan8;
            parentItem.QuyetToan9 += selfItem.QuyetToan9;
            parentItem.QuyetToan10 += selfItem.QuyetToan10;
            parentItem.DuToan1 += selfItem.DuToan1;
            parentItem.DuToan2 += selfItem.DuToan2;
            parentItem.DuToan3 += selfItem.DuToan3;
            parentItem.DuToan4 += selfItem.DuToan4;
            parentItem.DuToan5 += selfItem.DuToan5;
            parentItem.DuToan6 += selfItem.DuToan6;
            parentItem.DuToan7 += selfItem.DuToan7;
            parentItem.DuToan8 += selfItem.DuToan8;
            parentItem.DuToan9 += selfItem.DuToan9;
            parentItem.DuToan10 += selfItem.DuToan10;
            //for (int i = 0; i < _iNumListValue; i++)
            //{
            //    parentItem.LstValue[i].QuyetToanValue += selfItem.LstValue[i].QuyetToanValue;
            //    parentItem.LstValue[i].DuToanValue += selfItem.LstValue[i].DuToanValue;
            //}
            CalculateParent(parentItem, selfItem);
        }

        private void CalculateTotal(List<QuyetToanTongHopDonVi> exportData)
        {
            _total = new QuyetToanTongHopDonViTotal();
            for (int i = 0; i < _iNumListValue; i++)
            {
                _totalListDynamic.Add(new QuyetToanDynamicValue());
            }
            foreach (var item in exportData.Where(x => !string.IsNullOrEmpty(x.MaDonVi) && (!x.IsHangCha || (x.IsHangCha && (!x.BHangChaDuToan || !x.BHangChaQuyetToan)))))
            {
                _total.TongQuyetToan1 += item.QuyetToan1;
                _total.TongQuyetToan2 += item.QuyetToan2;
                _total.TongQuyetToan3 += item.QuyetToan3;
                _total.TongQuyetToan4 += item.QuyetToan4;
                _total.TongQuyetToan5 += item.QuyetToan5;
                _total.TongQuyetToan6 += item.QuyetToan6;
                _total.TongQuyetToan7 += item.QuyetToan7;
                _total.TongQuyetToan8 += item.QuyetToan8;
                _total.TongQuyetToan9 += item.QuyetToan9;
                _total.TongQuyetToan10 += item.QuyetToan10;
                _total.TongDuToan1 += item.DuToan1;
                _total.TongDuToan2 += item.DuToan2;
                _total.TongDuToan3 += item.DuToan3;
                _total.TongDuToan4 += item.DuToan4;
                _total.TongDuToan5 += item.DuToan5;
                _total.TongDuToan6 += item.DuToan6;
                _total.TongDuToan7 += item.DuToan7;
                _total.TongDuToan8 += item.DuToan8;
                _total.TongDuToan9 += item.DuToan9;
                _total.TongDuToan10 += item.DuToan10;
                for (int i = 0; i < _iNumListValue; i++)
                {
                    //if (_totalListDynamic.Any())
                    //{
                    //    _totalListDynamic[i].QuyetToanValue += item.LstValue[i].QuyetToanValue;
                    //    _totalListDynamic[i].DuToanValue += item.LstValue[i].DuToanValue;
                    //}
                }

            }
            _total.TongDuToan = exportData.Where(x => x.MlnsIdParent == null || x.MlnsIdParent.Equals(Guid.Empty)).Sum(x => x.DuToan);
            _total.TongQuyetToan = exportData.Where(x => x.MlnsIdParent == null || x.MlnsIdParent.Equals(Guid.Empty)).Sum(x => x.QuyetToan);
            _total.TongTrongKy = exportData.Where(x => x.MlnsIdParent == null || x.MlnsIdParent.Equals(Guid.Empty)).Sum(x => x.TrongKy);
        }
    }
}
