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
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport
{
    public class PrintSummaryYearSettlementViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IExportService _exportService;
        private INsDonViService _donViService;
        private INsMucLucNganSachService _mucLucNganSachService;
        private IMapper _mapper;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private INsQtChungTuChiTietService _chungTuChiTietService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private readonly INsNguoiDungLnsService _nsNguoiDungLNSService;
        private ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private List<NsMucLucNganSach> _listMucLucNganSach;
        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintSummaryYearSettlement);

        private List<MucLucNganSachCheckDataQuery> _listMucLucNganSachHasData;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private List<PrintYearSummarySettlementQuery> _reportData2;
        private List<ReportQtTongHopNamQuery> _reportData;
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private bool _isHasData;

        public override string Name => "Tổng hợp quyết toán năm - Đơn vị";
        public override string Title => "Tổng hợp quyết toán năm - Đơn vị";
        public override string Description => "Chọn in tổng hợp đơn vị hoặc chi tiết từng đơn vị";
        private Guid IdMlnsRootLastYear = Guid.Parse("00000000-0000-0000-0000-000000000001");
        private Guid IdMlnsRootCurrentYear = Guid.Parse("00000000-0000-0000-0000-000000000002");

        private SummaryReportDataType _dataType;
        public SummaryReportDataType DataType
        {
            get => _dataType;
            set
            {
                SetProperty(ref _dataType, value);
                LoadBudgetIndexes();
                LoadAgencies();
            }
        }

        private SummaryYearReportType _reportType;
        public SummaryYearReportType ReportType
        {
            get => _reportType;
            set => SetProperty(ref _reportType, value);
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
                foreach (NsMuclucNgansachModel item in BudgetIndexes)
                {
                    item.IsSelected = _isSelectAllBudgetIndex;
                }
            }
        }
        #endregion

        #region list agency
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

        public string SelectedAgencyCount
        {
            get
            {
                int totalCount = 0;
                int totalSelected = 0;
                if (_agencies != null)
                {
                    totalCount = Agencies != null ? Agencies.Count : 0;
                    totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                }
                return string.Format(SELECTED_AGENCY_COUNT_STR, totalSelected, totalCount);
            }
        }
        private bool _isSelectAllAgency;
        public bool IsSelectAllAgency
        {
            get => Agencies.Count() > 0 && Agencies.All(x => x.Selected);
            set
            {
                SetProperty(ref _isSelectAllAgency, value);
                foreach (AgencyModel item in Agencies)
                {
                    item.Selected = _isSelectAllAgency;
                }
            }
        }
        #endregion

        public bool IsExportEnable
        {
            get
            {
                if (_budgetIndexes != null)
                    return _budgetIndexes.Where(x => x.IsSelected).Count() > 0;
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

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set
            {
                SetProperty(ref _years, value);
                LoadBudgetIndexes();
                LoadAgencies();
            }
        }

        private ComboboxItem _selectedYear;
        public ComboboxItem SelectedYear
        {
            get => _selectedYear;
            set
            {
                SetProperty(ref _selectedYear, value);
                LoadBudgetIndexes();
                LoadAgencies();
            }
        }

        private ObservableCollection<ComboboxItem> _cbbTypeYearBudgets;
        public ObservableCollection<ComboboxItem> CbbTypeYearBudgets
        {
            get => _cbbTypeYearBudgets;
            set => SetProperty(ref _cbbTypeYearBudgets, value);
        }

        private ComboboxItem _cbbTypeYearBudgetSelected;
        public ComboboxItem CbbTypeYearBudgetSelected
        {
            get => _cbbTypeYearBudgetSelected;
            set
            {
                SetProperty(ref _cbbTypeYearBudgetSelected, value);
                LoadBudgetIndexes();
            }
        }



        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintSummaryYearSettlementViewModel(ISessionService sessionService,
            IExportService exportService,
            INsDonViService donViService,
            INsMucLucNganSachService mucLucNganSachService,
            IMapper mapper,
            INsQtChungTuChiTietService chungTuChiTietService,
            IDanhMucService danhMucService,
            INsNguoiDungLnsService nsNguoiDungLNSService,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            ILog logger)
        {
            _sessionService = sessionService;
            _exportService = exportService;
            _donViService = donViService;
            _mucLucNganSachService = mucLucNganSachService;
            _mapper = mapper;
            _chungTuChiTietService = chungTuChiTietService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _nsNguoiDungLNSService = nsNguoiDungLNSService;
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
            _dataType = SummaryReportDataType.SelfPay;
            _reportType = SummaryYearReportType.SummaryLNS;
            _listMucLucNganSach = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
            _listMucLucNganSachHasData = _mucLucNganSachService.FindMlnsEstimateSettlementByYearOfBudget(_sessionInfo.YearOfWork).ToList();
            LoadTieuDe();
            LoadBudgetIndexes();
            LoadAgencies();
            LoadDanhMuc();
            LoadChiTietToi();
            LoadQuater();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_NAM_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;

            var years = _danhMucService.FindByType(TypeDanhMuc.NS_NamNganSach);

            _years = new List<ComboboxItem>();
            foreach (var year in years)
            {
                _years.Add(new ComboboxItem()
                {
                    DisplayItem = year.STen.Split("-")[1].Trim(),
                    ValueItem = year.IIDMaDanhMuc
                });
            }
            _years.Add(new ComboboxItem()
            {
                DisplayItem = "Tất cả",
                ValueItem = string.Join(",", years.Select(c => c.IIDMaDanhMuc))
            });
            SelectedYear = _years.Last();
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
            List<LNSQuery> lnsQueries = _mucLucNganSachService.FindBySummaryYearSettlement(_sessionInfo.YearOfWork, _sessionInfo.Budget, (int)DataType, _sessionInfo.Principal).ToList();
            var lstMlnsEstimateSettlement = _listMucLucNganSachHasData;
            if (CbbTypeYearBudgetSelected != null)
            {
                var typeYearBudget = CbbTypeYearBudgetSelected.ValueItem;
                List<int> lstYearBudget = new List<int>();
                if (typeYearBudget == "2") // Nam truoc
                {
                    lstYearBudget.Add(1);
                    lstYearBudget.Add(3);
                    lstYearBudget.Add(4);
                    lstMlnsEstimateSettlement = lstMlnsEstimateSettlement.Where(x => lstYearBudget.Contains(x.INamNganSach)).ToList();
                }
                else if (typeYearBudget == "3") // nam nay
                {
                    lstYearBudget = new List<int>() { 2 };
                    lstMlnsEstimateSettlement = lstMlnsEstimateSettlement.Where(x => lstYearBudget.Contains(x.INamNganSach)).ToList();
                }
            }
            lnsQueries = lnsQueries.Where(x => lstMlnsEstimateSettlement.Select(s => s.LNS).Distinct().Contains(x.LNS) && listLNSNguoiDung.Contains(x.LNS)).ToList();

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
                        LoadAgencies();
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

        private void LoadAgencies()
        {
            List<DonVi> _listDonVi = _donViService.FindBySettlement(_sessionInfo.YearOfWork, _sessionInfo.Budget, (int)DataType, string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns))).ToList();
            var idsDonViQuanLy = _sessionService.Current.IdsDonViQuanLy.Split(",");
            if (!_listDonVi.Any(x => idsDonViQuanLy.Contains(x.IIDMaDonVi) && x.Loai == LoaiDonVi.ROOT))
            {
                _listDonVi = _listDonVi.Where(x => idsDonViQuanLy.Contains(x.IIDMaDonVi)).ToList();
            }

            Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(_listDonVi);
            _listAgency = CollectionViewSource.GetDefaultView(Agencies);
            _listAgency.Filter = ListAgencyFilter;
            foreach (var model in Agencies)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(AgencyModel.Selected))
                    {
                        OnPropertyChanged(nameof(SelectedAgencyCount));
                        OnPropertyChanged(nameof(IsExportEnable));
                        OnPropertyChanged(nameof(IsSelectAllAgency));
                    }
                };
            }
            OnPropertyChanged(nameof(SelectedAgencyCount));
            OnPropertyChanged(nameof(IsExportEnable));
            OnPropertyChanged(nameof(IsSelectAllAgency));
        }

        private bool ListAgencyFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchAgencyText))
            {
                return true;
            }
            return obj is AgencyModel item && item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
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

        private void LoadQuater()
        {
            CbbTypeYearBudgets = new ObservableCollection<ComboboxItem>();
            CbbTypeYearBudgets.Add(new ComboboxItem { ValueItem = "1", DisplayItem = "Tổng hợp" });
            CbbTypeYearBudgets.Add(new ComboboxItem { ValueItem = "2", DisplayItem = "Năm trước chuyển sang" });
            CbbTypeYearBudgets.Add(new ComboboxItem { ValueItem = "3", DisplayItem = "Năm nay" });

            _cbbTypeYearBudgetSelected = CbbTypeYearBudgets.FirstOrDefault(n => n.ValueItem == "1");

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

        private bool ListBudgetIndexFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchBudgetIndexText))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
        }

        private void OnExportFile(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    ReportSettlementCriteria condition = new ReportSettlementCriteria
                    {
                        YearOfWork = _sessionInfo.YearOfWork,
                        LNS = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns)),
                        DataType = (int)DataType,
                        Dvt = Convert.ToInt32(SelectedUnit.ValueItem),
                        YearOfBudget = Convert.ToInt32(CbbTypeYearBudgetSelected.ValueItem),
                    };

                    if (ReportType == SummaryYearReportType.AgencyDetail)
                    {
                        foreach (var agency in Agencies.Where(x => x.Selected))
                        {
                            condition.AgencyId = agency.Id;
                            //var exportResult = ProcessExport(condition, agency.AgencyName, exportType);
                            var exportResult = ProcessExportUpdate(condition, agency.AgencyName, exportType);
                            if (exportResult != null)
                                results.Add(exportResult);
                        }
                    }
                    if (ReportType == SummaryYearReportType.SummaryLNS || ReportType == SummaryYearReportType.SummaryAgency)
                    {
                        condition.AgencyId = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
                        //var exportResult = ProcessExport(condition, "Tổng hợp", exportType);
                        var exportResult = ProcessExportUpdate(condition, "Tổng hợp", exportType);
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

        private ExportResult ProcessExport(ReportSettlementCriteria condition, string agencyName, ExportType exportType)
        {
            try
            {
                //string printType = ReportType == SummaryYearReportType.SummaryAgency || ReportType == SummaryYearReportType.AgencyDetail ? "1" : "0,1";
                //var items = _chungTuChiTietService
                //    .FindApprovalSettlementYear(condition.YearOfWork, SelectedYear.ValueItem, condition.LNS, condition.AgencyId, condition.DataType, condition.BudgetSource, int.Parse(SelectedUnit.ValueItem), printType)
                //    .Select(c => new PrintYearSummarySettlementQuery(c)).ToList();


                List<ReportQtTongHopNamQuery> temp = new List<ReportQtTongHopNamQuery>();
                _reportData = _chungTuChiTietService.FindForSummaryYearSettlementReport(condition);
                foreach (var item in _reportData)
                {
                    if (!temp.Any(x => x.XauNoiMa == item.XauNoiMa))
                    {
                        temp.Add(new ReportQtTongHopNamQuery
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
                            MLNS_Id = item.MLNS_Id,
                            MLNS_Id_Parent = item.MLNS_Id_Parent,
                            MoTa = item.MoTa,
                            TenDonVi = item.TenDonVi,
                            ChiTieuNamNay = _reportData.Where(x => x.XauNoiMa == item.XauNoiMa).Sum(x => x.ChiTieuNamNay),
                            ChiTieuNamSau = _reportData.Where(x => x.XauNoiMa == item.XauNoiMa).Sum(x => x.ChiTieuNamSau),
                            QuyetToan = _reportData.Where(x => x.XauNoiMa == item.XauNoiMa).Sum(x => x.QuyetToan),
                            IsHangCha = _reportData.Where(x => x.XauNoiMa == item.XauNoiMa).Max(x => x.IsHangCha)
                        });
                    }
                }
                _reportData = temp;

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_NAM_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();


                RptQuyetToanNamDonVi report = new RptQuyetToanNamDonVi();
                foreach (var item in _reportData.Where(x => !x.IsHangCha || x.HasData))
                {
                    CalculateParent(item, item);
                    report.TongChiTieuNamNay += item.ChiTieuNamNay == null ? 0 : item.ChiTieuNamNay.Value;
                    report.TongChiTieuNamSau += item.ChiTieuNamSau == null ? 0 : item.ChiTieuNamSau.Value;
                    report.TongQuyetToan += item.QuyetToan == null ? 0 : item.QuyetToan.Value;
                }

                report.TongConNamNay += (report.TongChiTieuNamNay - report.TongChiTieuNamSau);
                if (report.TongChiTieuNamNay > report.TongQuyetToan)
                    report.TongThua += report.TongChiTieuNamNay - report.TongQuyetToan;
                if (report.TongChiTieuNamNay < report.TongQuyetToan)
                    report.TongThieu += report.TongChiTieuNamNay - report.TongQuyetToan;
                report.TongTiLe += report.TongConNamNay != 0 ? report.TongQuyetToan * 100 / report.TongConNamNay : 0;



                //RptSummaryYearSettlementYear report = new RptSummaryYearSettlementYear();

                if (ReportType == SummaryYearReportType.SummaryLNS)
                {
                    //_reportData = ProcessSummaryData(false, items);
                    _reportData = ProcessSummaryData(false);
                }
                else if (ReportType == SummaryYearReportType.SummaryAgency)
                {
                    _reportData = ProcessAgencySummaryData();
                }

                switch (SelectedInToiMuc.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        _reportData = _reportData.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                        _reportData.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        _reportData = _reportData.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                        _reportData.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        _reportData = _reportData.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                        _reportData.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        _reportData = _reportData.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                        _reportData.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                        break;
                }


                FormatDisplay();
                report.DonVi = agencyName;
                report.Items = _reportData;
                report.Cap1 = _cap1;
                report.Cap2 = _sessionInfo.TenDonVi;
                report.TieuDe1 = Title1;
                report.TieuDe2 = Title2;
                report.TieuDe3 = Title3;
                report.Ngay = DateUtils.FormatDateReport(ReportDate);
                report.DiaDiem = _diaDiem;
                report.h2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem);
                report.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                report.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                report.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                report.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                report.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                report.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                report.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                report.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                report.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(Convert.ToInt32(SelectedUnit.ValueItem), exportType);
                data.Add("FormatNumber", formatNumber);
                foreach (var prop in report.GetType().GetProperties())
                {
                    data.Add(prop.Name, prop.GetValue(report));
                }

                var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                List<int> hideColumns = ExportExcelHelper<SettlementVoucher>.HideColumn(chiTietToi);

                string fileName = ExportFileName.RPT_NS_QUYETTOAN_NAM_LNS;
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, fileName);
                string fileNamePrefix = string.Format("{0}_{1}", fileName.Split(".").First(), agencyName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQtTongHopNamQuery>(templateFileName, data, hideColumns);
                return new ExportResult(agencyName, fileNameWithoutExtension, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private ExportResult ProcessExportUpdate(ReportSettlementCriteria condition, string agencyName, ExportType exportType)
        {
            try
            {

                List<ReportQtTongHopNamQuery> temp = new List<ReportQtTongHopNamQuery>();
                _reportData = _chungTuChiTietService.FindForSummaryYearSettlementReportUpdate(condition);

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_NAM_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();


                RptQuyetToanNamDonVi report = new RptQuyetToanNamDonVi();
                foreach (var item in _reportData.Where(x => !x.IsHangCha))
                {
                    CalculateParent(item, item);
                    report.TongChiTieuNamNay += item.ChiTieuNamNay == null ? 0 : item.ChiTieuNamNay.Value;
                    report.TongChiTieuNamSau += item.ChiTieuNamSau == null ? 0 : item.ChiTieuNamSau.Value;
                    report.TongQuyetToan += item.QuyetToan == null ? 0 : item.QuyetToan.Value;
                }

                report.TongConNamNay += (report.TongChiTieuNamNay - report.TongChiTieuNamSau);
                if (report.TongChiTieuNamNay > report.TongQuyetToan)
                    report.TongThua += report.TongChiTieuNamNay - report.TongQuyetToan;
                if (report.TongChiTieuNamNay < report.TongQuyetToan)
                    report.TongThieu += report.TongChiTieuNamNay - report.TongQuyetToan;
                report.TongTiLe += report.TongConNamNay != 0 ? report.TongQuyetToan * 100 / report.TongConNamNay : 0;

                if (ReportType == SummaryYearReportType.SummaryAgency)
                {
                    _reportData = ProcessAgencySummaryData();
                }
                else
                {
                    _reportData = ProcessSummaryData(false);
                }

                _reportData = _reportData.Where(x => x.HasData).ToList();

                switch (SelectedInToiMuc.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        _reportData = _reportData.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                        _reportData.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        _reportData = _reportData.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                        _reportData.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x =>  x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        _reportData = _reportData.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                        _reportData.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        _reportData = _reportData.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                        _reportData.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                        break;
                }


                FormatDisplay();
                _reportData.ForEach(x =>
                {
                    if (x.MLNS_Id == IdMlnsRootCurrentYear && x.Level != 5)
                    {
                        x.MLNS_Id_Parent = Guid.Empty;
                    }

                    if ((x.MLNS_Id_Parent.IsNullOrEmpty()) && x.INamNganSach == (int)YearBubgetType.Currentyear && x.Level != 3)
                    {
                        x.MLNS_Id_Parent = IdMlnsRootCurrentYear;
                    }

                    if (x.MLNS_Id == IdMlnsRootLastYear && x.Level != 2)
                    {
                        x.MLNS_Id_Parent = Guid.Empty;
                    }
                    if (x.MLNS_Id_Parent.IsNullOrEmpty() && x.INamNganSach == (int)YearBubgetType.LastYear && x.Level != 0)
                    {
                        x.MLNS_Id_Parent = IdMlnsRootLastYear;
                    }
                });

                _reportData.Select(item =>
                {
                    var parent = _reportData.FirstOrDefault(x => item.MLNS_Id == x.MLNS_Id_Parent);
                    var sumChitieuNamNay = _reportData.Where(x => item.MLNS_Id == x.MLNS_Id_Parent).Sum(x => x.ChiTieuNamNay);
                    var sumChitieuNamSau = _reportData.Where(x => item.MLNS_Id == x.MLNS_Id_Parent).Sum(x => x.ChiTieuNamSau);
                    if (parent != null)
                    {                       
                        parent.ChiTieuNamNayCustom = parent.ChiTieuNamNay == 0 ? item.ChiTieuNamNay - sumChitieuNamNay : parent.ChiTieuNamNay;
                        parent.ChiTieuNamSauCustom = parent.ChiTieuNamSau == 0 ? item.ChiTieuNamSau - sumChitieuNamSau : parent.ChiTieuNamSau;
                        parent.IsHiddenValue = parent.ChiTieuNamNay == 0 && !string.IsNullOrEmpty(parent.TNG) ? true : false;
                    }
                    return item;
                }).ToList();


                report.DonVi = _cbbTypeYearBudgetSelected != null ? _cbbTypeYearBudgetSelected.DisplayItem : string.Empty;
                report.Items = _reportData;
                report.Count = _reportData.Count;
                report.Cap1 = _cap1;
                report.Cap2 = _sessionInfo.TenDonVi;
                report.TieuDe1 = Title1;
                report.TieuDe2 = Title2;
                report.TieuDe3 = Title3;
                report.Ngay = DateUtils.FormatDateReport(ReportDate);
                report.DiaDiem = _diaDiem;
                report.h2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem);
                report.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                report.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                report.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                report.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                report.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                report.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                report.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                report.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                report.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(Convert.ToInt32(SelectedUnit.ValueItem), exportType);
                data.Add("FormatNumber", formatNumber);
                foreach (var prop in report.GetType().GetProperties())
                {
                    data.Add(prop.Name, prop.GetValue(report));
                }

                var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                List<int> hideColumns = ExportExcelHelper<SettlementVoucherNew>.HideColumn(chiTietToi);

                string fileName = string.Empty;
                if (_reportData.IsEmpty())
                {
                    fileName = ExportFileName.RPT_NS_QUYETTOAN_NAM_LNS_UPDATE1_EMPTY;
                }
                else
                {
                    fileName = ExportFileName.RPT_NS_QUYETTOAN_NAM_LNS_UPDATE1;

                }
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, fileName);
                string fileNamePrefix = string.Format("{0}_{1}", fileName.Split(".").First(), agencyName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQtTongHopNamQuery>(templateFileName, data, hideColumns, true);
                return new ExportResult(agencyName, fileNameWithoutExtension, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }


        private List<ReportQtTongHopNamQuery> ProcessSummaryData(bool isSetHangCha)
        {
            List<ReportQtTongHopNamQuery> result = new List<ReportQtTongHopNamQuery>();
            result = _reportData.GroupBy(g => new { g.MLNS_Id, g.MLNS_Id_Parent, g.LNS, g.L, g.K, g.M, g.TM, g.TTM, g.NG, g.TNG, g.TNG1, g.TNG2, g.TNG3, g.XauNoiMa, g.MoTa, g.Level, g.INamNganSach }).Select(x => new ReportQtTongHopNamQuery
            {
                MLNS_Id = x.Key.MLNS_Id,
                MLNS_Id_Parent = x.Key.MLNS_Id_Parent.HasValue ? x.Key.MLNS_Id_Parent.Value : Guid.Empty,
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
                Level = x.Key.Level,
                //IsHangCha = isSetHangCha ? true : x.Key.IsHangCha,
                IsHangCha = isSetHangCha ? true : (_reportData.Any(m => m.MLNS_Id == x.Key.MLNS_Id && m.MLNS_Id_Parent == x.Key.MLNS_Id_Parent
                                                    && m.XauNoiMa == x.Key.XauNoiMa && m.Level == x.Key.Level && m.INamNganSach == x.Key.INamNganSach && m.IsHangCha)),
                ChiTieuNamNay = x.Sum(rpt => rpt.ChiTieuNamNay),
                ChiTieuNamSau = x.Sum(rpt => rpt.ChiTieuNamSau),
                QuyetToan = x.Sum(rpt => rpt.QuyetToan),
                INamNganSach = x.Key.INamNganSach
            }).ToList();
            return result;

        }

        private List<PrintYearSummarySettlementQuery> ProcessSummaryData2(bool isSetHangCha, List<PrintYearSummarySettlementQuery> items)
        {
            List<PrintYearSummarySettlementQuery> result = new List<PrintYearSummarySettlementQuery>();
            result = items.GroupBy(g => new { g.MLNS_Id, g.MLNS_Id_Parent, g.LNS, g.L, g.K, g.M, g.TM, g.TTM, g.NG, g.TNG, g.TNG1, g.TNG2, g.TNG3, g.XauNoiMa, g.MoTa, g.IsHangCha }).Select(x => new PrintYearSummarySettlementQuery
            {
                MLNS_Id = x.Key.MLNS_Id,
                MLNS_Id_Parent = x.Key.MLNS_Id_Parent.HasValue ? x.Key.MLNS_Id_Parent.Value : Guid.Empty,
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
                DuToanNganSach = x.Sum(rpt => rpt.DuToanNganSach),
                SoDeNghiQuyetToan = x.Sum(rpt => rpt.SoDeNghiQuyetToan),
                SoChuyenNamSau = x.Sum(c => c.SoChuyenNamSau)
            }).ToList();
            return result;
        }

        private void FormatDisplay()
        {
            foreach (var item in _reportData.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
            {
                var parent = _reportData.Where(x => x.MLNS_Id == item.MLNS_Id_Parent).LastOrDefault();
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

        private List<ReportQtTongHopNamQuery> ProcessAgencySummaryData()
        {
            List<ReportQtTongHopNamQuery> result = new List<ReportQtTongHopNamQuery>();
            result = ProcessSummaryData(false);
            List<string> xauNoiMas = result.Select(x => $"{x.XauNoiMa}{x.INamNganSach}").ToList();
            foreach (var xauNoiMa in xauNoiMas)
            {
                List<ReportQtTongHopNamQuery> childrens = _reportData.Where(x => x.XauNoiMa == xauNoiMa && !string.IsNullOrEmpty(x.TenDonVi)).ToList();
                if (childrens.Count > 0)
                {
                    int index = result.IndexOf(result.Where(x => $"{x.XauNoiMa}{x.INamNganSach}" == xauNoiMa).FirstOrDefault());
                    foreach (var child in childrens)
                    {
                        index++;
                        ReportQtTongHopNamQuery rpt = new ReportQtTongHopNamQuery()
                        {
                            MLNS_Id = Guid.NewGuid(),
                            MLNS_Id_Parent = child.MLNS_Id,
                            MoTa = child.TenDonVi,
                            ChiTieuNamNay = child.ChiTieuNamNay,
                            ChiTieuNamSau = child.ChiTieuNamSau,
                            QuyetToan = child.QuyetToan,
                            IsHangCha = false
                        };

                        result.Insert(index, rpt);
                    }
                }
            }
            return result;
        }

        private void CalculateParent(ReportQtTongHopNamQuery currentItem, ReportQtTongHopNamQuery selfItem)
        {
            var parentItem = _reportData.Where(x => x.MLNS_Id != null && currentItem.MLNS_Id_Parent != null && x.MLNS_Id == currentItem.MLNS_Id_Parent && x.Level == currentItem.Level).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.ChiTieuNamNay += selfItem.ChiTieuNamNay;
            parentItem.ChiTieuNamSau += selfItem.ChiTieuNamSau;
            parentItem.QuyetToan += selfItem.QuyetToan;
            CalculateParent(parentItem, selfItem);

        }

        private List<PrintYearSummarySettlementQuery> ProcessAgencySummaryData2(List<PrintYearSummarySettlementQuery> items)
        {
            List<PrintYearSummarySettlementQuery> result = new List<PrintYearSummarySettlementQuery>();
            result = ProcessSummaryData2(true, items);
            List<string> xauNoiMas = result.Select(x => x.XauNoiMa).ToList();
            foreach (var xauNoiMa in xauNoiMas)
            {
                List<PrintYearSummarySettlementQuery> childrens = _reportData2.Where(x => x.XauNoiMa == xauNoiMa && !string.IsNullOrEmpty(x.TenDonVi)).ToList();
                if (childrens.Count > 0)
                {
                    int index = result.IndexOf(result.Where(x => x.XauNoiMa == xauNoiMa).FirstOrDefault());
                    foreach (var child in childrens)
                    {
                        index++;
                        PrintYearSummarySettlementQuery rpt = new PrintYearSummarySettlementQuery()
                        {
                            MLNS_Id = Guid.Empty,
                            MLNS_Id_Parent = Guid.Empty,
                            MoTa = child.TenDonVi,
                            IsHangCha = false,
                            DuToanNganSach = child.DuToanNganSach,
                            SoDeNghiQuyetToan = child.SoDeNghiQuyetToan,
                            SoChuyenNamSau = child.SoChuyenNamSau
                        };
                        result.Insert(index, rpt);
                    }
                }
            }
            return result;
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_NAM_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_QUYETTOAN_NAM_LNS;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private List<FindApprovalSettlementYearQuery> FillParentMlnsData(List<FindApprovalSettlementYearQuery> items)
        {
            var xnms = items.Select(c => c.XauNoiMa);
            var mlns = _mucLucNganSachService
                .FindByCondition(c => c.NamLamViec == _sessionInfo.YearOfWork && c.BHangCha)
                .Where(d => xnms.Any(xn => xn.StartsWith(d.XauNoiMa) || xn.Equals(d.XauNoiMa)))
                .Select(e => new FindApprovalSettlementYearQuery()
                {
                    DuToanSoBaoCao = 0,
                    XauNoiMa = e.XauNoiMa,
                    DuToanSoXetDuyet = 0,
                    QuyetToanSoBaoCao = 0,
                    QuyetToanSoXetDuyet = 0,
                    XetDuyetDuToanConDuChuyenNamSau = 0,
                    XetDuyetDuToanConDuTongSo = 0,
                    DuToanSoChenhLech = 0,
                    QuyetToanChenhLech = 0,
                    XetDuyetDuToanConDuBiHuy = 0,
                    IsHangCha = e.BHangCha,
                    K = e.K,
                    L = e.L,
                    LNS = e.Lns,
                    M = e.M,
                    MLNS_Id = e.MlnsId,
                    MLNS_Id_Parent = e.MlnsIdParent,
                    MoTa = e.MoTa,
                    NG = e.Ng,
                    TM = e.Tm,
                    TNG = e.Tng,
                    TNG1 = e.Tng1,
                    TNG2 = e.Tng2,
                    TNG3 = e.Tng3,
                    TTM = e.Ttm
                }).OrderBy(c => c.XauNoiMa).ToList();
            foreach (var mln in mlns)
            {
                mln.DuToanSoBaoCao = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.DuToanSoBaoCao);
                mln.DuToanSoXetDuyet = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.DuToanSoXetDuyet);
                mln.QuyetToanSoBaoCao = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.QuyetToanSoBaoCao);
                mln.QuyetToanSoXetDuyet = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.QuyetToanSoXetDuyet);
                mln.XetDuyetDuToanConDuChuyenNamSau = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.XetDuyetDuToanConDuChuyenNamSau);
                mln.XetDuyetDuToanConDuTongSo = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.XetDuyetDuToanConDuTongSo);
                mln.DuToanSoChenhLech = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.DuToanSoChenhLech);
                mln.QuyetToanChenhLech = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.QuyetToanChenhLech);
                mln.XetDuyetDuToanConDuBiHuy = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.XetDuyetDuToanConDuBiHuy);
            }
            return mlns;
        }



    }
}
