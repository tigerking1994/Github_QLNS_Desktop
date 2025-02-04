using AutoMapper;
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
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport
{
    public class PrintEstimateSettlementViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private readonly IMapper _mapper;
        private readonly INsDonViService _donViService;
        private readonly INsQtChungTuChiTietService _chungTuChiTietService;
        private readonly INsQtChungTuService _chungTuService;
        private readonly INsNguoiDungLnsService _nsNguoiDungLNSService;
        private List<ReportQtDuToanQuyetToanQuery> _reportQuyetToanDuToans;
        private List<ReportQtDuToanQuyetToanThangQuery> _reportQuyetToanDuToanThangs;
        private INsMucLucNganSachService _mucLucNganSachService;
        private List<ReportQtDuToanQuyetToanQuyQuery> _reportQuyetToanDuToanQuys;
        private List<ReportQtDuToanQuyetToanTongThangQuery> _reportQuyetToanDuToanTongThangs;
        private IExportService _exportService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private List<NsMucLucNganSach> _listMucLucNganSach;
        private ILog _logger;
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private string _quarterMonth;
        private int _quarterMonthType;
        private string _quarterMonthBefore;

        public override string Name => "SO SÁNH SỐ QUYẾT TOÁN - DỰ TOÁN";
        public override string Title => "So sánh số quyết toán - dự toán";
        public override string Description => "In báo cáo so sánh số quyết toán - dự toán - Theo tháng quý";
        public override Type ContentType => typeof(PrintEstimateSettlement);

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
                LoadAgencies();
            }
        }

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
            get => !BudgetIndexes.Any() ? false : BudgetIndexes.Count > 0 && BudgetIndexes.All(x => x.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                foreach (NsMuclucNgansachModel item in BudgetIndexes)
                {
                    item.IsSelected = _isSelectAllBudgetIndex;
                }
            }
        }

        private ObservableCollection<ComboboxItem> _expenseTypes = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ExpenseTypes
        {
            get => _expenseTypes;
            set => SetProperty(ref _expenseTypes, value);
        }

        private ComboboxItem _expenseTypeSelected;
        public ComboboxItem ExpenseTypeSelected
        {
            get => _expenseTypeSelected;
            set
            {
                SetProperty(ref _expenseTypeSelected, value);
                LoadBudgetIndexes();
            }
        }

        public string LabelSelectedCountDonVi
        {
            get
            {
                var totalCount = Agencies.Count;
                var totalSelected = Agencies.Count(item => item.Selected);
                return $"ĐƠN VỊ ({totalSelected}/{totalCount})";
            }
        }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value))
                {
                    _listAgency.Refresh();
                }
            }
        }

        private bool _selectAllDonVi;
        public bool SelectAllDonVi
        {
            get => Agencies.Count > 0 && Agencies.All(item => item.Selected);
            set
            {
                SetProperty(ref _selectAllDonVi, value);
                foreach (var item in Agencies) item.Selected = _selectAllDonVi;
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

        private ReportEstimateSettlement _reportType;
        public ReportEstimateSettlement ReportType
        {
            get => _reportType;
            set => SetProperty(ref _reportType, value);
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

        public PrintEstimateSettlementViewModel(INsDonViService donViService,
            ISessionService sessionService,
            IMapper mapper,
            INsQtChungTuChiTietService chungTuChiTietService,
            IExportService exportService,
            INsMucLucNganSachService mucLucNganSachService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            INsNguoiDungLnsService nsNguoiDungLNSService,
            ILog logger,
            INsQtChungTuService chungTuService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _donViService = donViService;
            _sessionService = sessionService;
            _mapper = mapper;
            _mucLucNganSachService = mucLucNganSachService;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _nsNguoiDungLNSService = nsNguoiDungLNSService;
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
            _sessionInfo = _sessionService.Current;
            _reportType = ReportEstimateSettlement.Agency;
            _listMucLucNganSach = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
            InitReportDefaultDate();
            LoadTieuDe();
            // LoadAgencies();
            LoadMonthsAndQuarters();
            LoadExpenseTypes();
            LoadDanhMuc();
            LoadChiTietToi();
            LoadBudgetIndexes();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_DUTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }

        private void ProcessMonth()
        {
            if (MonthAndQuartersSelected.DisplayItem.Contains("Quý"))
            {
                _quarterMonthType = (int)QuarterMonth.QUARTER;
                _quarterMonth = string.Join(",", Enumerable.Range(Convert.ToInt32(MonthAndQuartersSelected.ValueItem) - 2, 3).ToList());
                _quarterMonthBefore = string.Join(",", Enumerable.Range(0, Convert.ToInt32(MonthAndQuartersSelected.ValueItem) - 2).ToList());
            }
            else
            {
                _quarterMonthType = (int)QuarterMonth.MONTH;
                _quarterMonth = MonthAndQuartersSelected.ValueItem;
                _quarterMonthBefore = string.Join(",", Enumerable.Range(0, Convert.ToInt32(MonthAndQuartersSelected.ValueItem)).ToList());
            }
        }

        private void LoadAgencies()
        {
            List<DonVi> agencies = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();

            var idsDonViQuanLy = _sessionService.Current.IdsDonViQuanLy.Split(",");
            if (!agencies.Any(x => idsDonViQuanLy.Contains(x.IIDMaDonVi) && x.Loai == LoaiDonVi.ROOT))
            {
                agencies = agencies.Where(x => idsDonViQuanLy.Contains(x.IIDMaDonVi)).ToList();
            }

            List<DonVi> listDonViReport = _donViService.FindByEstimateSettlement(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, DateTime.Now, Convert.ToInt32(MonthAndQuartersSelected?.ValueItem), _quarterMonthType).ToList();
            Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(agencies.Where(x => x.Loai == LoaiDonVi.ROOT || (x.Loai != LoaiDonVi.ROOT && listDonViReport.Select(x => x.IIDMaDonVi).Contains(x.IIDMaDonVi))));
            // Filter
            _listAgency = CollectionViewSource.GetDefaultView(Agencies);
            _listAgency.Filter = obj => string.IsNullOrWhiteSpace(_searchDonVi)
                                       || (obj is CheckBoxItem item && item.DisplayItem.ToLower().Contains(_searchDonVi.ToLower()));

            foreach (var org in Agencies)
            {
                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountDonVi));
                    OnPropertyChanged(nameof(SelectAllDonVi));
                    OnPropertyChanged(nameof(IsExportEnable));
                    LoadBudgetIndexes();
                };
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

        private void LoadExpenseTypes()
        {
            var expenseTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Tổng hợp", ValueItem = "" },
                new ComboboxItem { DisplayItem = "Quốc phòng", ValueItem = "1" },
                new ComboboxItem { DisplayItem = "Lương, phụ cấp, trợ cấp, tiền ăn", ValueItem = "101" },
                new ComboboxItem { DisplayItem = "Nghiệp vụ", ValueItem = "102" },
                new ComboboxItem { DisplayItem = "Quốc phòng khác", ValueItem = "109" },
                new ComboboxItem { DisplayItem = "Nhà nước", ValueItem = "2" },
                new ComboboxItem { DisplayItem = "Đặc biệt", ValueItem = "3" },
                new ComboboxItem { DisplayItem = "Khác", ValueItem = "4" }
            };

            ExpenseTypes = new ObservableCollection<ComboboxItem>(expenseTypes);
            _expenseTypeSelected = ExpenseTypes.ElementAt(0);
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

        private void OnExportFile(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();

                    ReportSettlementCriteria condition = new ReportSettlementCriteria()
                    {
                        YearOfWork = _sessionInfo.YearOfWork,
                        YearOfBudget = _sessionInfo.YearOfBudget,
                        BudgetSource = _sessionInfo.Budget,
                        LNS = string.Join(StringUtils.COMMA, BudgetIndexes.Where(x => x.IsSelected).Select(s => s.Lns)),
                        QuarterMonthType = _quarterMonthType,
                        QuarterMonth = _quarterMonth,
                        QuarterMonthBefore = _quarterMonthBefore,
                        //VoucherDate = ReportDate.GetValueOrDefault(),
                        VoucherDate = new DateTime(_sessionInfo.YearOfWork, int.Parse(MonthAndQuartersSelected.ValueItem), DateTime.DaysInMonth(_sessionInfo.YearOfWork, int.Parse(MonthAndQuartersSelected.ValueItem))),
                        Dvt = Convert.ToInt32(SelectedUnit.ValueItem)
                    };

                    if (ReportType == ReportEstimateSettlement.Agency || ReportType == ReportEstimateSettlement.AgencyMonth || ReportType == ReportEstimateSettlement.AgencyQuater)
                    {
                        foreach (var agency in Agencies.Where(x => x.Selected))
                        {
                            bool isRoot = false;
                            if (agency.Loai == LoaiDonVi.ROOT)
                            {
                                var hasSTongHop = _chungTuService.HasSTongHop(
                                new SettlementVoucherCriteria
                                {
                                    SettlementType = SettlementType.DEFENSE_BUDGET,
                                    YearOfWork = _sessionInfo.YearOfWork,
                                    YearOfBudget = _sessionInfo.YearOfBudget,
                                    BudgetSource = _sessionInfo.Budget,
                                    QuarterMonth = Convert.ToInt32(_quarterMonth.Split(",").Last()),
                                    QuarterMonthType = Convert.ToInt32(_quarterMonthType),
                                    AgencyId = agency.Id
                                });
                                isRoot = true;
                                condition.EstimateAgencyId = agency.Id;
                                if (hasSTongHop)
                                {
                                    condition.AgencyId = string.Join(",", Agencies.Where(x => x.Loai != LoaiDonVi.ROOT).Select(x => x.Id));
                                }
                                else
                                {
                                    condition.AgencyId = agency.Id;
                                }
                            }
                            else
                            {
                                condition.EstimateAgencyId = agency.Id;
                                condition.AgencyId = agency.Id;
                            }
                            ExportResult exportResult = null;
                            if (ReportType == ReportEstimateSettlement.Agency)
                                exportResult = ProcessExport(condition, isRoot, exportType);
                            else if (ReportType == ReportEstimateSettlement.AgencyMonth)
                            {
                                condition.QuarterMonth = string.Join(",", Enumerable.Range(1, Convert.ToInt32(MonthAndQuartersSelected.ValueItem)).ToList());
                                exportResult = ProcessExportThang(condition, isRoot, exportType);
                            }
                            else if (ReportType == ReportEstimateSettlement.AgencyQuater)
                            {
                                condition.QuarterMonth = string.Join(",", Enumerable.Range(1, Convert.ToInt32(MonthAndQuartersSelected.ValueItem)).ToList());
                                exportResult = ProcessExportQuy(condition, isRoot, exportType);
                            }

                            if (exportResult != null)
                                results.Add(exportResult);
                        }
                    }
                    else if (ReportType == ReportEstimateSettlement.AgencySummary || ReportType == ReportEstimateSettlement.Summary || ReportType == ReportEstimateSettlement.AgencySummaryMonth)
                    {
                        ExportResult exportResult = null;
                        if (Agencies.Any(x => x.Selected && x.Loai == LoaiDonVi.ROOT))
                            condition.AgencyId = string.Join(",", Agencies.Where(x => x.Loai != LoaiDonVi.ROOT).Select(x => x.Id).ToArray());
                        else
                            condition.AgencyId = string.Join(",", Agencies.Where(x => x.Selected && x.Loai != LoaiDonVi.ROOT).Select(x => x.Id).ToArray());
                        condition.EstimateAgencyId = condition.AgencyId;
                        exportResult = null;
                        if (ReportType == ReportEstimateSettlement.AgencySummaryMonth)
                        {
                            condition.QuarterMonth = string.Join(",", Enumerable.Range(1, Convert.ToInt32(MonthAndQuartersSelected.ValueItem)).ToList());
                            exportResult = ProcessExportSummaryMonth(condition);
                        }
                        else exportResult = ProcessExport(condition);
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
            }
        }

        private ExportResult ProcessExport(ReportSettlementCriteria condition, bool isRoot = false, ExportType exportType = ExportType.PDF)
        {
            try
            {
                _reportQuyetToanDuToans = _chungTuChiTietService.FindForEstimateSettlementReportClone(condition);

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_DUTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                RptQuyetToanDuToan rptQtDt = new RptQuyetToanDuToan();
                rptQtDt.Cap1 = _cap1;
                rptQtDt.Cap2 = _sessionInfo.TenDonVi;
                rptQtDt.TieuDe1 = string.Format(Title1, ExpenseTypeSelected.DisplayItem);
                rptQtDt.TieuDe2 = Title2;
                rptQtDt.TieuDe3 = Title3;

                foreach (var item in _reportQuyetToanDuToans.Where(x => !x.IsHangCha || x.ChiTieuOrigin != 0))
                {
                    CalculateParent(item, item);
                    rptQtDt.TongSoNgay += item.SoNgay;
                    rptQtDt.TongDuToan += item.ChiTieu;
                    rptQtDt.TongTrongKy += item.TuChi;
                    rptQtDt.TongKyTruoc += item.TuChi2;
                    rptQtDt.TongKyNay += item.DenKyNay ?? 0;
                    //rptQtDt.TongKyNay += rptQtDt.TongTrongKy + rptQtDt.TongKyTruoc;
                }

                rptQtDt.TongSoConLai = rptQtDt.TongDuToan - rptQtDt.TongKyNay;
                rptQtDt.TongTiLe = rptQtDt.TongDuToan != 0 ? rptQtDt.TongKyNay * 100 / rptQtDt.TongDuToan : 0;

                _reportQuyetToanDuToans = _reportQuyetToanDuToans.Where(x => x.HasData).ToList();
                if (ReportType == ReportEstimateSettlement.Summary)
                {
                    _reportQuyetToanDuToans = ProcessSummaryData(false);
                }
                else if (ReportType == ReportEstimateSettlement.AgencySummary)
                {
                    _reportQuyetToanDuToans = ProcessAgencySummaryData();
                }
                switch (SelectedInToiMuc.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        _reportQuyetToanDuToans = _reportQuyetToanDuToans.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                        _reportQuyetToanDuToans.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        _reportQuyetToanDuToans = _reportQuyetToanDuToans.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                        _reportQuyetToanDuToans.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        _reportQuyetToanDuToans = _reportQuyetToanDuToans.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                        _reportQuyetToanDuToans.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        _reportQuyetToanDuToans = _reportQuyetToanDuToans.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                        _reportQuyetToanDuToans.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                        break;
                }
                FormatDisplay();
                rptQtDt.Items = _reportQuyetToanDuToans;
                rptQtDt.ThoiGian = MonthAndQuartersSelected.DisplayItem + " năm " + _sessionInfo.YearOfWork;
                if (ReportType == ReportEstimateSettlement.Agency)
                    rptQtDt.DonVi = Agencies.Where(x => (isRoot && x.Id == condition.EstimateAgencyId) || (!isRoot && x.Id == condition.AgencyId)).Select(x => x.AgencyName).First().Split("-").Last();
                else rptQtDt.DonVi = "(Tổng hợp)";
                rptQtDt.H2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem);
                rptQtDt.TienTuChi = StringUtils.NumberToText(rptQtDt.TongTrongKy * Convert.ToInt32(SelectedUnit.ValueItem));
                rptQtDt.Ngay = DateUtils.FormatDateReport(ReportDate);
                rptQtDt.DiaDiem = _diaDiem;
                rptQtDt.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                rptQtDt.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                rptQtDt.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                rptQtDt.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                rptQtDt.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                rptQtDt.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                rptQtDt.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                rptQtDt.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                rptQtDt.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(Convert.ToInt32(SelectedUnit.ValueItem), exportType);
                data.Add("FormatNumber", formatNumber);
                foreach (var prop in rptQtDt.GetType().GetProperties())
                {
                    data.Add(prop.Name, prop.GetValue(rptQtDt));
                }

                var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                List<int> hideColumns = ExportExcelHelper<SettlementVoucher>.HideColumn(chiTietToi);

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_DUTOAN);
                string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_DUTOAN.Split(".").First() + "_" + rptQtDt.DonVi + "_" + DateTime.Now.Millisecond;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQtDuToanQuyetToanQuery>(templateFileName, data, hideColumns);
                return new ExportResult(rptQtDt.DonVi, fileNameWithoutExtension, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private ExportResult ProcessExportThang(ReportSettlementCriteria condition, bool isRoot = false, ExportType exportType = ExportType.PDF)
        {
            try
            {
                _reportQuyetToanDuToanThangs = _chungTuChiTietService.FindForEstimateSettlementMonthReportClone(condition);

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_DUTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                RptQuyetToanDuToan rptQtDt = new RptQuyetToanDuToan();
                rptQtDt.Cap1 = _cap1;
                rptQtDt.Cap2 = _sessionInfo.TenDonVi;
                rptQtDt.TieuDe1 = string.Format(Title1, ExpenseTypeSelected.DisplayItem);
                rptQtDt.TieuDe2 = Title2;
                rptQtDt.TieuDe3 = Title3;
                foreach (var item in _reportQuyetToanDuToanThangs.Where(x => !x.IsHangCha || x.ChiTieuOrigin != 0))
                {
                    CalculateParentThang(item, item);
                    rptQtDt.TongDuToan += item.ChiTieu;
                    rptQtDt.TongQuyetToan += item.TuChi;
                    rptQtDt.TongThang1 += item.Thang1;
                    rptQtDt.TongThang2 += item.Thang2;
                    rptQtDt.TongThang3 += item.Thang3;
                    rptQtDt.TongThang4 += item.Thang4;
                    rptQtDt.TongThang5 += item.Thang5;
                    rptQtDt.TongThang6 += item.Thang6;
                    rptQtDt.TongThang7 += item.Thang7;
                    rptQtDt.TongThang8 += item.Thang8;
                    rptQtDt.TongThang9 += item.Thang9;
                    rptQtDt.TongThang10 += item.Thang10;
                    rptQtDt.TongThang11 += item.Thang11;
                    rptQtDt.TongThang12 += item.Thang12;
                }
                rptQtDt.TongSoConLai = rptQtDt.TongDuToan - rptQtDt.TongQuyetToan;
                rptQtDt.TongTiLe = rptQtDt.TongDuToan != 0 ? rptQtDt.TongQuyetToan * 100 / rptQtDt.TongDuToan : 0;

                _reportQuyetToanDuToanThangs = _reportQuyetToanDuToanThangs.Where(x => x.HasData).ToList();
                switch (SelectedInToiMuc.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        _reportQuyetToanDuToanThangs = _reportQuyetToanDuToanThangs.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                        _reportQuyetToanDuToanThangs.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        _reportQuyetToanDuToanThangs = _reportQuyetToanDuToanThangs.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                        _reportQuyetToanDuToanThangs.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        _reportQuyetToanDuToanThangs = _reportQuyetToanDuToanThangs.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                        _reportQuyetToanDuToanThangs.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        _reportQuyetToanDuToanThangs = _reportQuyetToanDuToanThangs.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                        _reportQuyetToanDuToanThangs.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                        break;
                }
                FormatDisplayThang();
                rptQtDt.ItemMonths = _reportQuyetToanDuToanThangs;
                rptQtDt.ThoiGian = MonthAndQuartersSelected.DisplayItem + " năm " + _sessionInfo.YearOfWork;
                rptQtDt.DonVi = Agencies.Where(x => (isRoot && x.Id == condition.EstimateAgencyId) || (!isRoot && x.Id == condition.AgencyId)).Select(x => x.AgencyName).First().Split("-").Last();

                //if (ReportType == ReportEstimateSettlement.Agency)
                //    rptQtDt.DonVi = Agencies.Where(x => (isRoot && x.Id == condition.EstimateAgencyId) || (!isRoot && x.Id == condition.AgencyId)).Select(x => x.AgencyName).First().Split("-").Last();
                //else rptQtDt.DonVi = "(Tổng hợp)";

                rptQtDt.H2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem);
                //rptQtDt.TienTuChi = StringUtils.NumberToText(rptQtDt.TongQuyetToan * Convert.ToInt32(SelectedUnit.ValueItem));

                double tienTuChi = 0;
                var listThang = condition.QuarterMonth.Split(StringUtils.COMMA).ToList();
                listThang.ForEach(x =>
                {
                    tienTuChi += Convert.ToDouble(rptQtDt.GetType().GetProperty("TongThang" + x).GetValue(rptQtDt));
                });

                rptQtDt.TienTuChi = StringUtils.NumberToText(tienTuChi * Convert.ToInt32(SelectedUnit.ValueItem));
                rptQtDt.Ngay = DateUtils.FormatDateReport(ReportDate);
                rptQtDt.DiaDiem = _diaDiem;
                rptQtDt.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                rptQtDt.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                rptQtDt.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                rptQtDt.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                rptQtDt.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                rptQtDt.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                rptQtDt.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                rptQtDt.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                rptQtDt.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(Convert.ToInt32(SelectedUnit.ValueItem), exportType);
                FormatNumber formatNumberDecimalPlace = new FormatNumber(2);
                data.Add("FormatNumber", formatNumber);
                data.Add("FormatNumberDecimalPlace", formatNumberDecimalPlace);
                foreach (var prop in rptQtDt.GetType().GetProperties())
                {
                    data.Add(prop.Name, prop.GetValue(rptQtDt));
                }

                var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                List<int> hideColumns = ExportExcelHelper<SettlementVoucher>.HideColumn(chiTietToi);

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_DUTOAN_THANG);
                string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_DUTOAN_THANG.Split(".").First() + "_" + rptQtDt.DonVi + "_" + DateTime.Now.Millisecond;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQtDuToanQuyetToanThangQuery>(templateFileName, data, hideColumns);
                return new ExportResult(rptQtDt.DonVi, fileNameWithoutExtension, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private List<string> GetListLNSByUser()
        {
            var predicate = PredicateBuilder.True<NsNguoiDungLns>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.SMaNguoiDung == _sessionService.Current.Principal);
            List<NsNguoiDungLns> listNguoiDungDonVi = _nsNguoiDungLNSService.FindAll(predicate).ToList();
            return listNguoiDungDonVi.Select(x => x.SLns).ToList();
        }

        private void LoadBudgetIndexes()
        {
            List<string> listLNSNguoiDung = GetListLNSByUser();
            //List<LNSQuery> lnsQueries = _mucLucNganSachService.FindBySettlementMonth(_sessionInfo.YearOfWork, _sessionInfo.Budget, 1).ToList();
            if (MonthAndQuartersSelected == null) return;
            string quarterMonthId = String.Empty;
            int quarterMonthType = 0;
            quarterMonthId = MonthAndQuartersSelected.ValueItem;
            if (MonthAndQuartersSelected.DisplayItem.Contains("Quý"))
                quarterMonthType = (int)QuarterMonth.QUARTER;
            else quarterMonthType = (int)QuarterMonth.MONTH;
            List<LNSQuery> lnsQueries = _mucLucNganSachService.FindBySettlementEstimateMonth(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, string.Join(",", _agencies.Where(x => x.Selected).Select(x => x.Id).ToArray()), quarterMonthId, quarterMonthType);

            lnsQueries = lnsQueries.Where(x => listLNSNguoiDung.Contains(x.LNS)).ToList();

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

            BudgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
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
                        OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                        OnPropertyChanged(nameof(IsExportEnable));
                        OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                    }
                };
            }
            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
            OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
            OnPropertyChanged(nameof(IsExportEnable));
        }

        private bool ListBudgetIndexFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchBudgetIndexText))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
        }

        private ExportResult ProcessExportQuy(ReportSettlementCriteria condition, bool isRoot = false, ExportType exportType = ExportType.PDF)
        {
            try
            {
                _reportQuyetToanDuToanQuys = _chungTuChiTietService.FindForEstimateSettlementQuarterReportClone(condition);

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_DUTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                RptQuyetToanDuToan rptQtDt = new RptQuyetToanDuToan();
                rptQtDt.Cap1 = _cap1;
                rptQtDt.Cap2 = _sessionInfo.TenDonVi;
                rptQtDt.TieuDe1 = string.Format(Title1, ExpenseTypeSelected.DisplayItem);
                rptQtDt.TieuDe3 = Title3;
                rptQtDt.TieuDe2 = Title2;
                foreach (var item in _reportQuyetToanDuToanQuys.Where(x => !x.IsHangCha || x.ChiTieuOrigin != 0))
                {
                    CalculateParentQuy(item, item);
                    rptQtDt.TongDuToan += item.ChiTieu;
                    rptQtDt.TongQuyetToan += item.TuChi;
                    rptQtDt.TongQuy1 += item.Quy1;
                    rptQtDt.TongQuy2 += item.Quy2;
                    rptQtDt.TongQuy3 += item.Quy3;
                    rptQtDt.TongQuy4 += item.Quy4;
                }

                rptQtDt.TongSoConLai = rptQtDt.TongDuToan - rptQtDt.TongQuyetToan;
                rptQtDt.TongTiLe = rptQtDt.TongDuToan != 0 ? rptQtDt.TongQuyetToan * 100 / rptQtDt.TongDuToan : 0;

                _reportQuyetToanDuToanQuys = _reportQuyetToanDuToanQuys.Where(x => x.HasData).ToList();
                switch (SelectedInToiMuc.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        _reportQuyetToanDuToanQuys = _reportQuyetToanDuToanQuys.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                        _reportQuyetToanDuToanQuys.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        _reportQuyetToanDuToanQuys = _reportQuyetToanDuToanQuys.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                        _reportQuyetToanDuToanQuys.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        _reportQuyetToanDuToanQuys = _reportQuyetToanDuToanQuys.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                        _reportQuyetToanDuToanQuys.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        _reportQuyetToanDuToanQuys = _reportQuyetToanDuToanQuys.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                        _reportQuyetToanDuToanQuys.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                        break;
                }
                FormatDisplayQuy();
                rptQtDt.ItemQuarters = _reportQuyetToanDuToanQuys;
                rptQtDt.ThoiGian = MonthAndQuartersSelected.DisplayItem + " năm " + _sessionInfo.YearOfWork;
                rptQtDt.DonVi = Agencies.Where(x => (isRoot && x.Id == condition.EstimateAgencyId) || (!isRoot && x.Id == condition.AgencyId)).Select(x => x.AgencyName).First().Split("-").Last();

                //if (ReportType == ReportEstimateSettlement.Agency)
                //    rptQtDt.DonVi = Agencies.Where(x => (isRoot && x.Id == condition.EstimateAgencyId) || (!isRoot && x.Id == condition.AgencyId)).Select(x => x.AgencyName).First().Split("-").Last();
                //else rptQtDt.DonVi = "(Tổng hợp)";

                rptQtDt.H2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem);
                rptQtDt.TienTuChi = StringUtils.NumberToText(rptQtDt.TongQuyetToan * Convert.ToInt32(SelectedUnit.ValueItem));
                rptQtDt.Ngay = DateUtils.FormatDateReport(ReportDate);
                rptQtDt.DiaDiem = _diaDiem;
                rptQtDt.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                rptQtDt.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                rptQtDt.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                rptQtDt.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                rptQtDt.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                rptQtDt.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                rptQtDt.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                rptQtDt.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                rptQtDt.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(Convert.ToInt32(SelectedUnit.ValueItem), exportType);
                data.Add("FormatNumber", formatNumber);
                foreach (var prop in rptQtDt.GetType().GetProperties())
                {
                    data.Add(prop.Name, prop.GetValue(rptQtDt));
                }

                var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                List<int> hideColumns = ExportExcelHelper<SettlementVoucher>.HideColumn(chiTietToi);

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_DUTOAN_QUY);
                string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_DUTOAN_QUY.Split(".").First() + "_" + rptQtDt.DonVi + "_" + DateTime.Now.Millisecond;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQtDuToanQuyetToanQuyQuery>(templateFileName, data, hideColumns);
                return new ExportResult(rptQtDt.DonVi, fileNameWithoutExtension, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private ExportResult ProcessExportSummaryMonth(ReportSettlementCriteria condition, ExportType exportType = ExportType.PDF)
        {
            try
            {
                _reportQuyetToanDuToanTongThangs = _chungTuChiTietService.FindForEstimateSettlementSummaryMonthReport(condition);

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_DUTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                RptQuyetToanDuToan rptQtDt = new RptQuyetToanDuToan();
                rptQtDt.Cap1 = _cap1;
                rptQtDt.Cap2 = _sessionInfo.TenDonVi;
                rptQtDt.TieuDe1 = string.Format(Title1, ExpenseTypeSelected.DisplayItem);
                rptQtDt.TieuDe2 = Title2;
                rptQtDt.TieuDe3 = Title3;
                foreach (var item in _reportQuyetToanDuToanTongThangs)
                {
                    rptQtDt.TongDuToan += item.ChiTieu;
                    rptQtDt.TongThang1 += item.Thang1;
                    rptQtDt.TongThang2 += item.Thang2;
                    rptQtDt.TongThang3 += item.Thang3;
                    rptQtDt.TongThang4 += item.Thang4;
                    rptQtDt.TongThang5 += item.Thang5;
                    rptQtDt.TongThang6 += item.Thang6;
                    rptQtDt.TongThang7 += item.Thang7;
                    rptQtDt.TongThang8 += item.Thang8;
                    rptQtDt.TongThang9 += item.Thang9;
                    rptQtDt.TongThang10 += item.Thang10;
                    rptQtDt.TongThang11 += item.Thang11;
                    rptQtDt.TongThang12 += item.Thang12;
                    rptQtDt.TongQuyetToan = rptQtDt.TongThang1 + rptQtDt.TongThang2 + rptQtDt.TongThang3 + rptQtDt.TongThang4 + rptQtDt.TongThang5 + rptQtDt.TongThang6
                        + rptQtDt.TongThang7 + rptQtDt.TongThang8 + rptQtDt.TongThang9 + rptQtDt.TongThang10 + rptQtDt.TongThang11 + rptQtDt.TongThang12;
                }
                rptQtDt.TongSoConLai = rptQtDt.TongDuToan - rptQtDt.TongQuyetToan;
                rptQtDt.TongTiLe = rptQtDt.TongDuToan != 0 ? rptQtDt.TongQuyetToan * 100 / rptQtDt.TongDuToan : 0;

                rptQtDt.ItemSummaryMonths = _reportQuyetToanDuToanTongThangs;
                rptQtDt.ThoiGian = MonthAndQuartersSelected.DisplayItem + " năm " + _sessionInfo.YearOfWork;
                rptQtDt.H2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem);
                rptQtDt.TienTuChi = StringUtils.NumberToText(rptQtDt.TongQuyetToan * Convert.ToInt32(SelectedUnit.ValueItem));
                rptQtDt.Ngay = DateUtils.FormatDateReport(ReportDate);
                rptQtDt.DiaDiem = _diaDiem;
                rptQtDt.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                rptQtDt.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                rptQtDt.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                rptQtDt.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                rptQtDt.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                rptQtDt.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                rptQtDt.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                rptQtDt.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                rptQtDt.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(Convert.ToInt32(SelectedUnit.ValueItem), exportType);
                data.Add("FormatNumber", formatNumber);
                foreach (var prop in rptQtDt.GetType().GetProperties())
                {
                    data.Add(prop.Name, prop.GetValue(rptQtDt));
                }

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_DUTOAN_TONGHOP_THANG);
                string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_DUTOAN_TONGHOP_THANG.Split(".").First() + "_" + rptQtDt.DonVi + "_" + DateTime.Now.Millisecond;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQtDuToanQuyetToanTongThangQuery>(templateFileName, data);
                return new ExportResult(rptQtDt.DonVi, fileNameWithoutExtension, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private List<ReportQtDuToanQuyetToanQuery> ProcessSummaryData(bool isSetHangCha)
        {
            List<ReportQtDuToanQuyetToanQuery> result = new List<ReportQtDuToanQuyetToanQuery>();
            result = _reportQuyetToanDuToans.GroupBy(g => new { g.MLNS_Id, g.MLNS_Id_Parent, g.LNS, g.L, g.K, g.M, g.TM, g.TTM, g.NG, g.TNG, g.TNG1, g.TNG2, g.TNG3, g.XauNoiMa, g.MoTa, g.IsHangCha }).Select(x => new ReportQtDuToanQuyetToanQuery
            {
                MLNS_Id = x.Key.MLNS_Id,
                MLNS_Id_Parent = x.Key.MLNS_Id_Parent.GetValueOrDefault(),
                LNS = x.Key.LNS,
                L = x.Key.L,
                K = x.Key.K,
                M = x.Key.M,
                TM = x.Key.TM,
                TTM = x.Key.TTM,
                NG = x.Key.NG,
                TNG = x.Key.TNG,
                TNG1 = x.Key.TNG1,
                TNG2 = x.Key.TNG2,
                TNG3 = x.Key.TNG3,
                XauNoiMa = x.Key.XauNoiMa,
                MoTa = x.Key.MoTa,
                IsHangCha = isSetHangCha ? true : x.Key.IsHangCha,
                SoNgay = x.Sum(rpt => rpt.SoNgay),
                SoNguoi = x.Sum(rpt => rpt.SoNguoi),
                ChiTieu = x.Sum(rpt => rpt.ChiTieu),
                TuChi = x.Sum(rpt => rpt.TuChi),
                TuChi2 = x.Sum(rpt => rpt.TuChi2),
            }).ToList();
            return result;
        }

        private void FormatDisplay()
        {
            foreach (var item in _reportQuyetToanDuToans.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
            {
                var parent = _reportQuyetToanDuToans.Where(x => x.MLNS_Id == item.MLNS_Id_Parent).LastOrDefault();
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
        }

        private void FormatDisplayThang()
        {
            foreach (var item in _reportQuyetToanDuToanThangs.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
            {
                var parent = _reportQuyetToanDuToanThangs.Where(x => x.MLNS_Id == item.MLNS_Id_Parent).LastOrDefault();
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
        }

        private void FormatDisplayQuy()
        {
            foreach (var item in _reportQuyetToanDuToanQuys.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
            {
                var parent = _reportQuyetToanDuToanQuys.Where(x => x.MLNS_Id == item.MLNS_Id_Parent).LastOrDefault();
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
        }

        private List<ReportQtDuToanQuyetToanQuery> ProcessAgencySummaryData()
        {
            List<ReportQtDuToanQuyetToanQuery> result = new List<ReportQtDuToanQuyetToanQuery>();
            result = ProcessSummaryData(true);
            foreach (var item in result.ToList())
            {
                List<ReportQtDuToanQuyetToanQuery> childrens = _reportQuyetToanDuToans.Where(x => x.XauNoiMa == item.XauNoiMa && !x.IsHangCha && !string.IsNullOrEmpty(x.TenDonVi)).ToList();
                if (childrens.Count > 0)
                {
                    int index = result.IndexOf(result.Where(x => x.XauNoiMa == item.XauNoiMa).FirstOrDefault());
                    foreach (var child in childrens)
                    {
                        index++;
                        ReportQtDuToanQuyetToanQuery rpt = new ReportQtDuToanQuyetToanQuery()
                        {
                            MLNS_Id = Guid.Empty,
                            MLNS_Id_Parent = item.MLNS_Id,
                            MoTa = child.TenDonVi,
                            SoNguoi = child.SoNguoi,
                            SoNgay = child.SoNgay,
                            ChiTieu = child.ChiTieu,
                            TuChi = child.TuChi,
                            TuChi2 = child.TuChi2,
                            IsHangCha = false
                        };
                        result.Insert(index, rpt);
                    }
                }
            }
            return result;
        }

        private void CalculateParent(ReportQtDuToanQuyetToanQuery currentItem, ReportQtDuToanQuyetToanQuery selfItem)
        {
            var parentItem = _reportQuyetToanDuToans.Where(x => x.MLNS_Id != Guid.Empty && currentItem.MLNS_Id_Parent != Guid.Empty && x.MLNS_Id == currentItem.MLNS_Id_Parent).FirstOrDefault();
            if (parentItem == null) return;
            if (selfItem.ChiTieuOrigin != 0)
                parentItem.ChiTieu += selfItem.ChiTieu;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.TuChi2 += selfItem.TuChi2;
            CalculateParent(parentItem, selfItem);
        }

        private void CalculateParentThang(ReportQtDuToanQuyetToanThangQuery currentItem, ReportQtDuToanQuyetToanThangQuery selfItem)
        {
            var parentItem = _reportQuyetToanDuToanThangs.Where(x => x.MLNS_Id != Guid.Empty && currentItem.MLNS_Id_Parent != Guid.Empty && x.MLNS_Id == currentItem.MLNS_Id_Parent).FirstOrDefault();
            if (parentItem == null) return;
            if (selfItem.ChiTieuOrigin != 0)
                parentItem.ChiTieu += selfItem.ChiTieu;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.Thang1 += selfItem.Thang1;
            parentItem.Thang2 += selfItem.Thang2;
            parentItem.Thang3 += selfItem.Thang3;
            parentItem.Thang4 += selfItem.Thang4;
            parentItem.Thang5 += selfItem.Thang5;
            parentItem.Thang6 += selfItem.Thang6;
            parentItem.Thang7 += selfItem.Thang7;
            parentItem.Thang8 += selfItem.Thang8;
            parentItem.Thang9 += selfItem.Thang9;
            parentItem.Thang10 += selfItem.Thang10;
            parentItem.Thang11 += selfItem.Thang11;
            parentItem.Thang12 += selfItem.Thang12;
            CalculateParentThang(parentItem, selfItem);
        }

        private void CalculateParentQuy(ReportQtDuToanQuyetToanQuyQuery currentItem, ReportQtDuToanQuyetToanQuyQuery selfItem)
        {
            var parentItem = _reportQuyetToanDuToanQuys.Where(x => x.MLNS_Id != Guid.Empty && currentItem.MLNS_Id_Parent != Guid.Empty && x.MLNS_Id == currentItem.MLNS_Id_Parent).FirstOrDefault();
            if (parentItem == null) return;
            if (selfItem.ChiTieuOrigin != 0)
                parentItem.ChiTieu += selfItem.ChiTieu;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.Quy1 += selfItem.Quy1;
            parentItem.Quy2 += selfItem.Quy2;
            parentItem.Quy3 += selfItem.Quy3;
            parentItem.Quy4 += selfItem.Quy4;
            CalculateParentQuy(parentItem, selfItem);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_DUTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_QUYETTOAN_DUTOAN;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}