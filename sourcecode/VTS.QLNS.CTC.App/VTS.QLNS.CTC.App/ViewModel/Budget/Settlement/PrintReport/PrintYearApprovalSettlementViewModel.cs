using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
    public class PrintYearApprovalSettlementViewModel : ViewModelBase
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
        private ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private List<NsMucLucNganSach> _listMucLucNganSach;
        private int _type;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private List<FindApprovalSettlementYearQuery> _reportData;
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintYearApprovalSettlement);

        public override string Name => "Xét duyệt quyết toán năm (HD 8063)";
        public override string Title => "Tổng hợp xét duyệt quyết toán năm (HD 8063)";
        public override string Description => "Chọn in tổng hợp đơn vị hoặc chi tiết từng đơn vị";

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

        public PrintYearApprovalSettlementViewModel(ISessionService sessionService,
            IExportService exportService,
            INsDonViService donViService,
            INsMucLucNganSachService mucLucNganSachService,
            IMapper mapper,
            INsQtChungTuChiTietService chungTuChiTietService,
            IDanhMucService danhMucService,
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
            _type = _donViService.CountILoaiByNamLamViec(_sessionInfo.YearOfWork, LoaiDonVi.NOI_BO) > 0 ? 1 : 0;
            LoadTieuDe();
            LoadBudgetIndexes();
            LoadAgencies();
            LoadDanhMuc();
            LoadChiTietToi();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOANNAM_XETDUYET) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;

            var years = _danhMucService.FindByType(TypeDanhMuc.NS_NamNganSach);
            var allYear = new StringBuilder();
            _years = new List<ComboboxItem>();
            foreach (var year in years)
            {
                allYear.Append(year.IIDMaDanhMuc);
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

            OnPropertyChanged(nameof(Years));
            OnPropertyChanged(nameof(SelectedYear));
            OnPropertyChanged(nameof(SelectedUnit));

        }

        private void LoadBudgetIndexes()
        {
            List<LNSQuery> lnsQueries = _mucLucNganSachService.FindBySummaryYearSettlement(SelectedYear.ValueItem, _sessionInfo.YearOfWork, _sessionInfo.Budget, (int)DataType, _type.ToString()).ToList();
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
                var mlns = _listMucLucNganSach.FirstOrDefault(m => m.XauNoiMa == x);
                return new NsMucLucNganSach()
                {
                    Lns = x,
                    XauNoiMa = x,
                    MoTa = mlns == null ? string.Empty : mlns.MoTa,
                    MlnsId = mlns == null ? Guid.Empty : (Guid)mlns.MlnsId,
                    MlnsIdParent = mlns == null ? Guid.Empty : (mlns.MlnsIdParent == null ? Guid.Empty : (Guid)mlns.MlnsIdParent)
                };
            })).Where(x => !string.IsNullOrEmpty(x.MoTa)).ToList();

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
            string lns = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns));
            List<DonVi> _listDonVi = _donViService.FindBySettlement(_sessionInfo.YearOfWork, _sessionInfo.Budget, (int)DataType, lns, _type.ToString()).ToList();
            RemoveAgencyParent(ref _listDonVi);
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
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE && x.INamLamViec == _sessionInfo.YearOfWork)
                .OrderBy(n => n.SGiaTri).ToList();
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
                _selectedInToiMuc = DataInToiMuc != null ? DataInToiMuc[0] : null;
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
                        Dvt = Convert.ToInt32(SelectedUnit.ValueItem)
                    };

                    if (ReportType == SummaryYearReportType.AgencyDetail)
                    {
                        foreach (var agency in Agencies.Where(x => x.Selected))
                        {
                            condition.AgencyId = agency.Id;
                            var exportResult = ProcessExport(condition, agency.AgencyName, exportType);
                            if (exportResult != null)
                                results.Add(exportResult);
                        }
                    }
                    if (ReportType == SummaryYearReportType.SummaryLNS || ReportType == SummaryYearReportType.SummaryAgency)
                    {
                        condition.AgencyId = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
                        var exportResult = ProcessExport(condition, "Tổng hợp", exportType);
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

                var temp = new List<FindApprovalSettlementYearQuery>();
                var items = _chungTuChiTietService.FindApprovalSettlementYear(condition.YearOfWork, SelectedYear.ValueItem, condition.LNS, condition.AgencyId, 1, condition.BudgetSource, int.Parse(SelectedUnit.ValueItem), "1").ToList();
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_NAM_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                var report = new RptFindApprovalSettlementYear();

                if (ReportType == SummaryYearReportType.SummaryLNS || ReportType == SummaryYearReportType.AgencyDetail)
                {
                    temp = ProcessSummaryData(false, items);
                    _reportData = FillParentData(temp);
                }
                else if (ReportType == SummaryYearReportType.SummaryAgency)
                {
                    var unitlns = FillUnitData(items);
                    var parents = FillParentData(items);
                    _reportData = GroupByUnit(parents, unitlns);
                    RemoveUnitNull(ref _reportData);
                }
                else
                {
                    _reportData = items;
                }

                FormatDisplay();
                FormatHeader(ref report);
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


                string fileName = ExportFileName.RPT_NS_QUYETTOAN_TONGHOP_8063;
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, fileName);
                string fileNamePrefix = string.Format("{0}_{1}", fileName.Split(".").First(), agencyName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<FindApprovalSettlementYearQuery>(templateFileName, data);
                return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private List<FindApprovalSettlementYearQuery> ProcessSummaryData(bool isSetHangCha, List<FindApprovalSettlementYearQuery> items)
        {
            var result = items.GroupBy(g => new { g.MLNS_Id, g.MLNS_Id_Parent, g.LNS, g.L, g.K, g.M, g.TM, g.TTM, g.NG, g.TNG, g.TNG1, g.TNG2, g.TNG3, g.XauNoiMa, g.MoTa, g.IsHangCha }).Select(x => new FindApprovalSettlementYearQuery
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
                DuToanSoBaoCao = x.Sum(rpt => rpt.DuToanSoBaoCao.Value),
                DuToanSoXetDuyet = x.Sum(rpt => rpt.DuToanSoXetDuyet.Value),
                QuyetToanSoBaoCao = x.Sum(rpt => rpt.QuyetToanSoBaoCao.Value),
                QuyetToanSoXetDuyet = x.Sum(rpt => rpt.QuyetToanSoXetDuyet.Value),
                XetDuyetDuToanConDuChuyenNamSau = x.Sum(rpt => rpt.XetDuyetDuToanConDuChuyenNamSau.Value),
            }).ToList();
            foreach (var res in result)
            {
                res.DuToanSoXetDuyet = res.DuToanSoBaoCao;
                res.XetDuyetDuToanConDuTongSo = res.DuToanSoXetDuyet - res.QuyetToanSoXetDuyet;
                res.XetDuyetDuToanConDuChuyenNamSau = res.XetDuyetDuToanConDuChuyenNamSau;
                res.XetDuyetDuToanConDuBiHuy = res.XetDuyetDuToanConDuTongSo - res.XetDuyetDuToanConDuChuyenNamSau;
            }
            return result;
        }

        private void FormatDisplay()
        {
            foreach (var item in _reportData.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
            {
                var parent = _reportData.Where(x => x.MLNS_Id == item.MLNS_Id_Parent).LastOrDefault();
                if (parent != null)
                {
                    item.L = string.Empty;
                    item.K = string.Empty;
                    item.LNS = string.Empty;
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

        private List<FindApprovalSettlementYearQuery> ProcessAgencySummaryData(List<FindApprovalSettlementYearQuery> items)
        {
            var donvis = items.Select(c => c.IdDonVi);
            List<FindApprovalSettlementYearQuery> result = new List<FindApprovalSettlementYearQuery>();

            result = ProcessSummaryData(true, items);
            List<string> xauNoiMas = result.Select(x => x.XauNoiMa).ToList();
            foreach (var xauNoiMa in xauNoiMas)
            {
                List<FindApprovalSettlementYearQuery> childrens = items.Where(x => x.XauNoiMa == xauNoiMa && !string.IsNullOrEmpty(x.TenDonVi)).ToList();
                if (childrens.Count > 0)
                {
                    int index = result.IndexOf(result.Where(x => x.XauNoiMa == xauNoiMa).FirstOrDefault());
                    foreach (var child in childrens)
                    {
                        index++;
                        FindApprovalSettlementYearQuery rpt = new FindApprovalSettlementYearQuery()
                        {
                            MLNS_Id = Guid.Empty,
                            MLNS_Id_Parent = Guid.Empty,
                            MoTa = child.TenDonVi,
                            QuyetToanChenhLech = child.QuyetToanChenhLech,
                            QuyetToanSoBaoCao = child.QuyetToanSoBaoCao,
                            QuyetToanSoXetDuyet = child.QuyetToanSoXetDuyet,
                            DuToanSoBaoCao = child.DuToanSoBaoCao,
                            DuToanSoXetDuyet = child.DuToanSoXetDuyet,
                            XetDuyetDuToanConDuBiHuy = child.XetDuyetDuToanConDuBiHuy,
                            XetDuyetDuToanConDuChuyenNamSau = child.DuToanSoXetDuyet,
                            XetDuyetDuToanConDuTongSo = child.XetDuyetDuToanConDuTongSo,
                            IsHangCha = false
                        };
                        result.Insert(index, rpt);
                    }
                }
            }
            return result;
        }

        private void CalculateParent(FindApprovalSettlementYearQuery currentItem, FindApprovalSettlementYearQuery selfItem)
        {
            var parentItem = _reportData.Where(x => x.MLNS_Id != null && currentItem.MLNS_Id_Parent != null && x.MLNS_Id == currentItem.MLNS_Id_Parent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.DuToanSoXetDuyet += selfItem.DuToanSoXetDuyet.Value;
            parentItem.DuToanSoBaoCao += selfItem.DuToanSoBaoCao.Value;
            parentItem.DuToanSoXetDuyet += selfItem.DuToanSoXetDuyet.Value;
            parentItem.QuyetToanSoBaoCao += selfItem.QuyetToanSoBaoCao.Value;
            parentItem.QuyetToanSoXetDuyet += selfItem.QuyetToanSoXetDuyet.Value;
            CalculateParent(parentItem, selfItem);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOANNAM_XETDUYET) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_QUYETTOANNAM_XETDUYET;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private List<FindApprovalSettlementYearQuery> FillParentData(List<FindApprovalSettlementYearQuery> items)
        {
            var xnms = items.Select(c => c.XauNoiMa);
            var mlns = _mucLucNganSachService
                .FindByCondition(c => c.NamLamViec == _sessionInfo.YearOfWork)
                .Where(d => xnms.Any(xn => xn.StartsWith(d.XauNoiMa) || xn.Equals(d.XauNoiMa)))
                .Select(e => new FindApprovalSettlementYearQuery()
                {
                    QuyetToanSoBaoCao = 0,
                    XauNoiMa = e.XauNoiMa,
                    DuToanSoBaoCao = 0,
                    DuToanSoXetDuyet = 0,
                    IsHangCha = e.BHangCha,
                    QuyetToanSoXetDuyet = 0,
                    QuyetToanChenhLech = 0,
                    XetDuyetDuToanConDuTongSo = 0,
                    XetDuyetDuToanConDuChuyenNamSau = 0,
                    XetDuyetDuToanConDuBiHuy = 0,
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
                mln.DuToanSoChenhLech = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.DuToanSoChenhLech);
                mln.QuyetToanSoBaoCao = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.QuyetToanSoBaoCao);
                mln.QuyetToanSoXetDuyet = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.QuyetToanSoXetDuyet);
                mln.QuyetToanChenhLech = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.QuyetToanChenhLech);
                mln.XetDuyetDuToanConDuTongSo = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.XetDuyetDuToanConDuTongSo);
                mln.XetDuyetDuToanConDuChuyenNamSau = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.XetDuyetDuToanConDuChuyenNamSau);
                mln.XetDuyetDuToanConDuBiHuy = items.Where(c => c.XauNoiMa.StartsWith(mln.XauNoiMa)).Sum(c => c.XetDuyetDuToanConDuBiHuy);
            }
            return mlns;
        }

        private List<FindApprovalSettlementYearQuery> FillUnitData(List<FindApprovalSettlementYearQuery> items)
        {
            var result = new List<FindApprovalSettlementYearQuery>();
            var ids = items.Select(c => c.IdDonVi).Distinct();
            var xmns = items.Select(c => c.XauNoiMa).Distinct();
            foreach (var id in ids)
            {
                foreach (var xmn in xmns)
                {
                    var unit = items.FirstOrDefault(c => c.IdDonVi.Equals(id)).TenDonVi;
                    result.Add(new FindApprovalSettlementYearQuery()
                    {
                        XauNoiMa = xmn,
                        MoTa = unit,
                        TenDonVi = unit,
                        XetDuyetDuToanConDuBiHuy = items.Where(c => c.IdDonVi == id && c.XauNoiMa == xmn).Sum(c => c.XetDuyetDuToanConDuBiHuy),
                        XetDuyetDuToanConDuChuyenNamSau = items.Where(c => c.IdDonVi == id && c.XauNoiMa == xmn).Sum(c => c.XetDuyetDuToanConDuChuyenNamSau),
                        XetDuyetDuToanConDuTongSo = items.Where(c => c.IdDonVi == id && c.XauNoiMa == xmn).Sum(c => c.XetDuyetDuToanConDuTongSo),
                        DuToanSoBaoCao = items.Where(c => c.IdDonVi == id && c.XauNoiMa == xmn).Sum(c => c.DuToanSoBaoCao),
                        DuToanSoXetDuyet = items.Where(c => c.IdDonVi == id && c.XauNoiMa == xmn).Sum(c => c.DuToanSoXetDuyet),
                        QuyetToanSoXetDuyet = items.Where(c => c.IdDonVi == id && c.XauNoiMa == xmn).Sum(c => c.QuyetToanSoXetDuyet),
                        QuyetToanChenhLech = items.Where(c => c.IdDonVi == id && c.XauNoiMa == xmn).Sum(c => c.QuyetToanChenhLech),
                        QuyetToanSoBaoCao = items.Where(c => c.IdDonVi == id && c.XauNoiMa == xmn).Sum(c => c.QuyetToanSoBaoCao),
                        DuToanSoChenhLech = items.Where(c => c.IdDonVi == id && c.XauNoiMa == xmn).Sum(c => c.DuToanSoChenhLech)
                    });
                }
            }
            result.OrderBy(c => c.XauNoiMa);
            return result;
        }

        private List<FindApprovalSettlementYearQuery> GroupByUnit(List<FindApprovalSettlementYearQuery> parent, List<FindApprovalSettlementYearQuery> units)
        {
            var result = new List<FindApprovalSettlementYearQuery>();
            foreach (var p in parent)
            {
                result.Add(p);
                var child = units.Where(c => c.XauNoiMa.Equals(p.XauNoiMa));
                if (child.Any())
                {
                    result.AddRange(child);
                }
            }

            return result;
        }
        private void FormatHeader(ref RptFindApprovalSettlementYear rpt)
        {
            var sum = new FindApprovalSettlementYearQuery()
            {
                MoTa = "B. NĂM NAY",
                XetDuyetDuToanConDuBiHuy = 0,
                XetDuyetDuToanConDuChuyenNamSau = 0,
                XetDuyetDuToanConDuTongSo = 0,
                DuToanSoBaoCao = 0,
                QuyetToanChenhLech = 0,
                QuyetToanSoBaoCao = 0,
                QuyetToanSoXetDuyet = 0,
                DuToanSoChenhLech = 0,
                DuToanSoXetDuyet = 0,
                IsHangCha = true
            };
            foreach (var item in _reportData)
            {
                if (item.XauNoiMa.Length == 1)
                {
                    item.MoTa = item.MoTa.ToUpper();
                }
                if (ReportType == SummaryYearReportType.SummaryLNS || ReportType == SummaryYearReportType.AgencyDetail)
                {
                    if (!string.IsNullOrEmpty(item.TTM))
                    {
                        item.IsHangCha = false;

                        sum.DuToanSoXetDuyet += item.DuToanSoXetDuyet;
                        sum.DuToanSoChenhLech += item.DuToanSoChenhLech;
                        sum.DuToanSoBaoCao += item.DuToanSoBaoCao;

                        sum.QuyetToanChenhLech += item.QuyetToanChenhLech;
                        sum.QuyetToanSoXetDuyet += item.QuyetToanSoXetDuyet;
                        sum.QuyetToanSoBaoCao += item.QuyetToanSoBaoCao;

                        sum.XetDuyetDuToanConDuBiHuy += item.XetDuyetDuToanConDuBiHuy;
                        sum.XetDuyetDuToanConDuChuyenNamSau += item.XetDuyetDuToanConDuChuyenNamSau;
                        sum.XetDuyetDuToanConDuTongSo += item.XetDuyetDuToanConDuTongSo;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(item.NG))
                    {
                        item.IsHangCha = true;
                    }
                }



            }

            //_reportData.Insert(0, sum);
            rpt.SDuToanSoXetDuyet = sum.DuToanSoXetDuyet.Value;
            rpt.SDuToanSoChenhLech = sum.DuToanSoChenhLech.Value;
            rpt.SDuToanSoBaoCao = sum.DuToanSoBaoCao.Value;

            rpt.SQuyetToanChenhLech = sum.QuyetToanChenhLech.Value;
            rpt.SQuyetToanSoXetDuyet = sum.QuyetToanSoXetDuyet.Value;
            rpt.SQuyetToanSoBaoCao = sum.QuyetToanSoBaoCao.Value;

            rpt.SXetDuyetDuToanConDuBiHuy = sum.XetDuyetDuToanConDuBiHuy.Value;
            rpt.SXetDuyetDuToanConDuChuyenNamSau = sum.XetDuyetDuToanConDuChuyenNamSau.Value;
            rpt.SXetDuyetDuToanConDuTongSo = sum.XetDuyetDuToanConDuTongSo.Value;
        }

        private void RemoveAgencyParent(ref List<DonVi> items)
        {
            //TODO: find parent logic here.
            var filtered = items.Where(C => C.Loai.Equals("1"));
            if (filtered.Any())
            {
                items.RemoveAll(c => c.Loai.Equals("0"));
                return;
            }
            return;
        }

        private void RemoveUnitNull(ref List<FindApprovalSettlementYearQuery> items)
        {
            items.RemoveAll(c => (c.DuToanSoBaoCao.Value + c.DuToanSoChenhLech.Value + c.DuToanSoXetDuyet
            + c.XetDuyetDuToanConDuBiHuy + c.XetDuyetDuToanConDuChuyenNamSau + c.XetDuyetDuToanConDuTongSo
            + c.QuyetToanChenhLech + c.QuyetToanSoBaoCao + c.QuyetToanSoXetDuyet) == 0);

        }
    }
}
