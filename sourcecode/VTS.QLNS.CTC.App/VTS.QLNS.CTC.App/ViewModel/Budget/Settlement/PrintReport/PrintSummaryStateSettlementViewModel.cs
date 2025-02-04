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
using VTS.QLNS.CTC.App.View.Budget.Settlement.Explanation;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Explanation;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport
{
    public class PrintSummaryStateSettlementViewModel : ViewModelBase
    {
        private IExportService _exportService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private ILog _logger;
        private IMapper _mapper;
        private INsDonViService _donViService;
        private INsMucLucNganSachService _mucLucNganSachService;
        private INsQtChungTuService _chungTuService;
        private INsQtChungTuChiTietService _chungTuChiTietService;
        private INsQtChungTuChiTietGiaiThichService _chungTuChiTietGiaiThichService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private INsPhongBanService _phongBanService;
        private VerbalExplanationViewModel VerbalExplanationViewModel;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private List<NsMucLucNganSach> _listMucLucNganSach;
        private List<ReportQtChungTuChiTietQuery> _reportQtChungTuChiTiets;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private bool _isInitReport;
        private bool _checkAllAgencies;

        public override string Name => "In báo cáo quyết toán ngân sách nhà nước - Tổng hợp quý";
        public override string Title => "In báo cáo quyết toán ngân sách nhà nước - Tổng hợp quý";
        public override string Description => "In báo cáo quyết toán ngân sách nhà nước - Tổng hợp quý";
        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintSummaryStateSettlement);

        private List<ComboboxItem> _quarterMonths;
        public List<ComboboxItem> QuarterMonths
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
                if (SetProperty(ref _quarterMonthSelected, value))
                {
                    if (!_isInitReport)
                        LoadAgencies();
                }
            }
        }

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
                    totalCount = Agencies != null ? Agencies.Where(x => x.IsFilter).Count() : 0;
                    totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                }
                return string.Format(SELECTED_AGENCY_COUNT_STR, totalSelected, totalCount);
            }
        }
        private bool _isSelectedAllAgency;
        public bool IsSelectedAllAgency
        {
            get => Agencies.Count > 0 && Agencies.Where(x => x.IsFilter).All(x => x.Selected);
            set
            {
                SetProperty(ref _isSelectedAllAgency, value);
                _checkAllAgencies = true;
                foreach (AgencyModel item in Agencies)
                {
                    item.Selected = _isSelectedAllAgency;
                }
                _checkAllAgencies = false;
                LoadBudgetIndexes();
                OnPropertyChanged(nameof(SelectedAgencyCount));
            }
        }
        #endregion

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
                int totalCount = BudgetIndexes != null ? BudgetIndexes.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => BudgetIndexes.Count > 0 && BudgetIndexes.Where(x => x.IsFilter).All(x => x.IsSelected);
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

        public bool IsExportEnable
        {
            get
            {
                if (_budgetIndexes != null)
                    return _budgetIndexes.Where(x => x.IsSelected).Count() > 0 && (_isCoverSheet || _isData || _isVerbalExplanation);
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

        private bool _isCoverSheet;
        public bool IsCoverSheet
        {
            get => _isCoverSheet;
            set
            {
                SetProperty(ref _isCoverSheet, value);
                OnPropertyChanged(nameof(IsExportEnable));
            }
        }

        private bool _isData;
        public bool IsData
        {
            get => _isData;
            set
            {
                SetProperty(ref _isData, value);
                OnPropertyChanged(nameof(IsExportEnable));
            }
        }

        private bool _isVerbalExplanation;
        public bool IsVerbalExplanation
        {
            get => _isVerbalExplanation;
            set
            {
                SetProperty(ref _isVerbalExplanation, value);
                OnPropertyChanged(nameof(IsExportEnable));
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

        private List<ComboboxItem> _khoi;
        public List<ComboboxItem> Khoi
        {
            get => _khoi;
            set => SetProperty(ref _khoi, value);
        }

        private ComboboxItem _selectedKhoi;
        public ComboboxItem SelectedKhoi
        {
            get => _selectedKhoi;
            set
            {
                if (SetProperty(ref _selectedKhoi, value))
                {
                    if (!_isInitReport)
                        LoadAgencies();
                }
            }
        }

        private List<ComboboxItem> _bQuanLy;
        public List<ComboboxItem> BQuanLy
        {
            get => _bQuanLy;
            set => SetProperty(ref _bQuanLy, value);
        }

        private ComboboxItem _selectedBQuanLy;
        public ComboboxItem SelectedBQuanLy
        {
            get => _selectedBQuanLy;
            set
            {
                if (SetProperty(ref _selectedBQuanLy, value))
                {
                    if (!_isInitReport)
                        LoadBudgetIndexes();
                }
            }
        }

        private bool _isSummary;
        public bool IsSummary
        {
            get => _isSummary;
            set
            {
                if (SetProperty(ref _isSummary, value))
                    LoadAgencies();
            }
        }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintSummaryStateSettlementViewModel(INsMucLucNganSachService mucLucNganSachService,
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            INsQtChungTuService chungTuService,
            INsQtChungTuChiTietService chungTuChiTietService,
            INsQtChungTuChiTietGiaiThichService chungTuChiTietGiaiThichService,
            IExportService exportService,
            INsDonViService donViService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            INsPhongBanService phongBanService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            VerbalExplanationViewModel verbalExplanationViewModel)
        {
            _mucLucNganSachService = mucLucNganSachService;
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _chungTuChiTietGiaiThichService = chungTuChiTietGiaiThichService;
            _exportService = exportService;
            _donViService = donViService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _phongBanService = phongBanService;
            VerbalExplanationViewModel = verbalExplanationViewModel;
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
            VerbalExplanationCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            _listMucLucNganSach = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
            _isInitReport = true;
            IsSummary = false;
            ResetCondition();
            LoadTieuDe();
            LoadQuarterMonths();
            LoadDanhMuc();
            LoadChiTietToi();
            LoadKhoi();
            LoadBQuanLy();
            LoadAgencies();
            _isInitReport = false;
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_NHANUOC_TONGHOP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }

        private void ResetCondition()
        {
            _quarterMonthSelected = new ComboboxItem();
            _selectedKhoi = new ComboboxItem();
            _selectedBQuanLy = new ComboboxItem();
            _budgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
            _searchAgencyText = string.Empty;
            _searchBudgetIndexText = string.Empty;

            OnPropertyChanged(nameof(BudgetIndexes));
        }

        /// <summary>
        /// Tạo data cho combobox qúy
        /// </summary>
        private void LoadQuarterMonths()
        {
            _quarterMonths = new List<ComboboxItem>();
            _quarterMonths.Add(new ComboboxItem("Quý I", "3"));
            _quarterMonths.Add(new ComboboxItem("Quý II", "6"));
            _quarterMonths.Add(new ComboboxItem("Quý III", "9"));
            _quarterMonths.Add(new ComboboxItem("Quý IV", "12"));
            for (int i = 1; i <= 12; i++)
            {
                _quarterMonths.Add(new ComboboxItem("Tháng " + i, i.ToString()));
            }
            QuarterMonthSelected = _quarterMonths.First();
        }

        private void LoadKhoi()
        {
            _khoi = new List<ComboboxItem>
            {
                new ComboboxItem ("Tất cả", string.Empty),
                new ComboboxItem ("Doanh nghiệp", "1"),
                new ComboboxItem ("Dự toán", "2"),
                new ComboboxItem ("Bệnh viện tự chủ", "3"),
            };
            SelectedKhoi = _khoi.First();
        }

        private void LoadBQuanLy()
        {
            var predicate = PredicateBuilder.True<DmBQuanLy>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            List<DmBQuanLy> listPhongBan = _phongBanService.FindByCondition(predicate);
            _bQuanLy = _mapper.Map<List<ComboboxItem>>(listPhongBan);
            if (_bQuanLy.Count() > 0)
            {
                _bQuanLy.Insert(0, new ComboboxItem("Tất cả", string.Empty));
                SelectedBQuanLy = _bQuanLy.First();
            }
        }

        private void LoadAgencies()
        {
            if (SelectedKhoi != null && QuarterMonthSelected != null)
            {
                string quarterMonthId = String.Empty;
                int quarterMonthType = 0;
                quarterMonthId = QuarterMonthSelected.ValueItem;
                if (QuarterMonthSelected.DisplayItem.Contains("Quý"))
                    quarterMonthType = (int)QuarterMonth.QUARTER;
                else quarterMonthType = (int)QuarterMonth.MONTH;
                List<DonVi> agencies = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
                List<DonVi> listDonViReport = new List<DonVi>();
                if (!IsSummary)
                {
                    string loaiQuyetToan = string.Join(",", new List<string> { SettlementType.STATE_BUDGET, SettlementType.FOREX_BUDGET, SettlementType.EXPENSE_BUDGET });
                    listDonViReport = _donViService.FindByQtTongHopQuy(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget.ToString(), _sessionInfo.Budget,
                                            quarterMonthId, SelectedKhoi.ValueItem, loaiQuyetToan, _sessionInfo.Principal).ToList();
                }
                _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(agencies.Where(x => x.Loai == LoaiDonVi.ROOT || (x.Loai != LoaiDonVi.ROOT && listDonViReport.Select(x => x.IIDMaDonVi).Contains(x.IIDMaDonVi))));
                _listAgency = CollectionViewSource.GetDefaultView(_agencies);
                _listAgency.Filter = ListAgencyFilter;
                foreach (var model in Agencies)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(AgencyModel.Selected) && !_checkAllAgencies)
                        {
                            OnPropertyChanged(nameof(SelectedAgencyCount));
                            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                            OnPropertyChanged(nameof(IsSelectedAllAgency));
                            LoadBudgetIndexes();
                        }
                    };
                }
                _budgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();

                OnPropertyChanged(nameof(BudgetIndexes));
                OnPropertyChanged(nameof(Agencies));
                OnPropertyChanged(nameof(IsSelectedAllAgency));
                OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                OnPropertyChanged(nameof(SelectedAgencyCount));
                OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                OnPropertyChanged(nameof(IsExportEnable));
            }
        }

        private bool ListAgencyFilter(object obj)
        {
            bool result = true;
            var item = (AgencyModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchAgencyText))
                result = result && item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadBudgetIndexes()
        {
            if (SelectedBQuanLy != null)
            {
                string quarterMonthId = String.Empty;
                int quarterMonthType = 0;
                quarterMonthId = QuarterMonthSelected.ValueItem;
                if (QuarterMonthSelected.DisplayItem.Contains("Quý"))
                    quarterMonthType = (int)QuarterMonth.QUARTER;
                else quarterMonthType = (int)QuarterMonth.MONTH;
                string agencyIds = string.Empty;
                if (!IsSummary)
                {
                    if (Agencies.Where(x => x.Selected).Any(x => x.Loai == LoaiDonVi.ROOT))
                        agencyIds = string.Join(",", Agencies.Select(x => x.Id));
                    else agencyIds = string.Join(",", Agencies.Where(x => x.Selected && x.Loai != LoaiDonVi.ROOT).Select(x => x.Id));
                }
                else agencyIds = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
                List<LNSQuery> lnsQueries = _mucLucNganSachService.FindBySettlementMonth(_sessionInfo.YearOfWork, _sessionInfo.Budget,
                    agencyIds, quarterMonthId, quarterMonthType);

                List<NsMucLucNganSach> listMucLuc = new List<NsMucLucNganSach>();
                List<string> lns = new List<string>();
                foreach (var item in lnsQueries)
                {
                    lns.Add(item.LNS);
                }

                listMucLuc = lns.OrderBy<string, string>((Func<string, string>)(x => x)).Select<string, NsMucLucNganSach>((Func<string, NsMucLucNganSach>)(x => new NsMucLucNganSach()
                {
                    Lns = x,
                    XauNoiMa = x,
                    MoTa = _listMucLucNganSach.FirstOrDefault<NsMucLucNganSach>((Func<NsMucLucNganSach, bool>)(m => m.XauNoiMa == x))?.MoTa,
                    ChiTietToi = _listMucLucNganSach.FirstOrDefault<NsMucLucNganSach>((Func<NsMucLucNganSach, bool>)(m => m.XauNoiMa == x))?.ChiTietToi,
                    IdPhongBan = _listMucLucNganSach.FirstOrDefault<NsMucLucNganSach>((Func<NsMucLucNganSach, bool>)(m => m.XauNoiMa == x))?.IdPhongBan,
                })).Where<NsMucLucNganSach>((Func<NsMucLucNganSach, bool>)(x => !string.IsNullOrEmpty(x.MoTa) && !x.Lns.StartsWith("1"))).ToList();

                List<string> listBQuanLy = SelectedBQuanLy.ValueItem.Split(",").Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                if (listBQuanLy.Count > 0)
                    BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMucLuc.Where(x => listBQuanLy.Contains(x.IdPhongBan)));
                else BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMucLuc);

                _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
                _listBudgetIndex.Filter = ListBudgetIndexFilter;
                foreach (var model in BudgetIndexes)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                        {
                            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                            OnPropertyChanged(nameof(IsExportEnable));
                            OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                        }
                    };
                }
                OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                OnPropertyChanged(nameof(IsExportEnable));
                OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
            }
        }

        private bool ListBudgetIndexFilter(object obj)
        {
            bool result = true;
            var item = (NsMuclucNgansachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchBudgetIndexText))
                result = item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE).OrderBy(n => n.SGiaTri)
                .ToList();
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

        private void OnExportFile(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();

                    if (IsCoverSheet)
                    {
                        var dataResult = OnExportCoverSheet();
                        if (dataResult != null)
                            results.Add(dataResult);
                    }

                    if (IsData || IsVerbalExplanation)
                    {
                        _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_NHANUOC_TONGHOP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_CHITIET_QS_NGHIHUU_KEHOACH);
                        string fileNamePrefix;
                        string fileNameWithoutExtension;

                        int dvt = Convert.ToInt32(SelectedUnit.ValueItem);
                        string quarterMonth = String.Empty;
                        int quarterMonthType = 0;
                        string quarterMonthBefore = string.Empty;

                        quarterMonth = QuarterMonthSelected.ValueItem;
                        if (QuarterMonthSelected.DisplayItem.Contains("Quý"))
                        {
                            quarterMonthType = (int)QuarterMonth.QUARTER;
                            quarterMonthBefore = string.Join(",", Enumerable.Range(0, Convert.ToInt32(quarterMonth)).Where(x => x % 3 == 0).ToList());
                        }
                        else
                        {
                            quarterMonthType = (int)QuarterMonth.MONTH;
                            quarterMonthBefore = string.Join(",", Enumerable.Range(0, Convert.ToInt32(quarterMonth)).ToList());
                        }
                        ReportSettlementCriteria condition = new ReportSettlementCriteria
                        {
                            YearOfWork = _sessionInfo.YearOfWork,
                            YearOfBudget = _sessionInfo.YearOfBudget,
                            BudgetSource = _sessionInfo.Budget,
                            QuarterMonthType = quarterMonthType,
                            QuarterMonth = quarterMonth,
                            QuarterMonthBefore = quarterMonthBefore,
                            VoucherDate = DateTime.Now,
                            LNS = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns).ToArray()),
                            SettlementType = SettlementType.STATE_BUDGET,
                            Dvt = dvt
                        };

                        foreach (var agency in Agencies.Where(x => x.Selected))
                        {
                            if (agency.Loai == LoaiDonVi.ROOT)
                            {
                                condition.EstimateAgencyId = agency.Id;
                                if (IsSummary)
                                    condition.AgencyId = agency.Id;
                                else
                                    condition.AgencyId = string.Join(",", Agencies.Where(x => x.Selected && x.Loai != LoaiDonVi.ROOT).Select(x => x.Id));
                            }
                            else
                            {
                                condition.EstimateAgencyId = agency.Id;
                                condition.AgencyId = agency.Id;
                            }
                            _reportQtChungTuChiTiets = _chungTuChiTietService.FindForReportQuarterlySummary(condition);
                            RptQuyetToanChungTu rptChungTu = new RptQuyetToanChungTu();

                            rptChungTu.Cap1 = _cap1;
                            rptChungTu.Cap2 = _sessionInfo.TenDonVi;
                            rptChungTu.TieuDe1 = Title1;
                            rptChungTu.TieuDe2 = Title2;
                            rptChungTu.TieuDe3 = Title3;
                            foreach (var item in _reportQtChungTuChiTiets.Where(x => !x.IsHangCha))
                            {
                                CalculateParent(item, item);
                                rptChungTu.TongChiTieu += item.ChiTieu;
                                rptChungTu.TongTuChi += item.TuChi;
                                rptChungTu.TongTuChi2 += item.TuChi2;
                                rptChungTu.TongThucChi += item.ThucChi;
                            }
                            _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => x.HasData).ToList();
                            switch (SelectedInToiMuc.ValueItem)
                            {
                                case nameof(MLNSFiled.NG):
                                    _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG):
                                    _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG1):
                                    _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG2):
                                    _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                                    break;
                            }
                            FormatDisplay();
                            rptChungTu.Items = _reportQtChungTuChiTiets;
                            rptChungTu.ThoiGian = QuarterMonthSelected.DisplayItem + " năm " + _sessionInfo.YearOfWork;
                            rptChungTu.ThangQuy = quarterMonthType == (int)QuarterMonth.QUARTER ? "quý" : "tháng";
                            rptChungTu.H2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem);
                            rptChungTu.TienTuChi = StringUtils.NumberToText(rptChungTu.TongTuChi * dvt);
                            rptChungTu.Ngay = DateUtils.FormatDateReport(ReportDate);
                            rptChungTu.DiaDiem = _diaDiem;
                            rptChungTu.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                            rptChungTu.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                            rptChungTu.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                            rptChungTu.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                            rptChungTu.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                            rptChungTu.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                            rptChungTu.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                            rptChungTu.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                            rptChungTu.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

                            if (IsData)
                            {
                                Dictionary<string, object> data = new Dictionary<string, object>();
                                FormatNumber formatNumber = new FormatNumber(dvt, exportType);
                                data.Add("FormatNumber", formatNumber);
                                foreach (var prop in rptChungTu.GetType().GetProperties())
                                {
                                    data.Add(prop.Name, prop.GetValue(rptChungTu));
                                }

                                var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                                List<int> hideColumns = ExportExcelHelper<SettlementVoucher>.HideColumn(chiTietToi);

                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP);
                                fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP.Split(".").First() + "_" + agency.AgencyName + "_" + DateTime.Now.Millisecond;
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data, hideColumns);
                                results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                            }
                            if (IsVerbalExplanation)
                            {
                                SettlementVoucherDetailExplainCriteria explainCondition = new SettlementVoucherDetailExplainCriteria
                                {
                                    VoucherId = Guid.Empty,
                                    ExplainId = "NN_" + _sessionInfo.YearOfWork + "_" + _sessionInfo.IdDonVi + "_" + quarterMonth + "_" + quarterMonthType,
                                    AgencyId = _sessionInfo.IdDonVi,
                                    YearOfWork = _sessionInfo.YearOfWork
                                };
                                NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindByCondition(explainCondition);
                                RptQuyetToanGiaiThichLoi rptGiaiThichLoi = new RptQuyetToanGiaiThichLoi();
                                rptGiaiThichLoi.Tien = rptChungTu.TongTuChi;
                                rptGiaiThichLoi.TienTuChi = StringUtils.NumberToText(rptChungTu.TongTuChi * Convert.ToInt32(SelectedUnit.ValueItem));
                                rptGiaiThichLoi.MoTaTinhHinh = chungTuChiTietGiaiThich == null ? string.Empty : chungTuChiTietGiaiThich.SMoTaTinhHinh;
                                rptGiaiThichLoi.MoTaKienNghi = chungTuChiTietGiaiThich == null ? string.Empty : chungTuChiTietGiaiThich.SMoTaKienNghi;
                                rptGiaiThichLoi.Ngay = StringUtils.CreateDateTimeString();
                                rptGiaiThichLoi.DiaDiem = _diaDiem;
                                rptGiaiThichLoi.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                                rptGiaiThichLoi.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                                rptGiaiThichLoi.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                                rptGiaiThichLoi.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                                rptGiaiThichLoi.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                                rptGiaiThichLoi.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                                rptGiaiThichLoi.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                                rptGiaiThichLoi.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                                rptGiaiThichLoi.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;
                                rptGiaiThichLoi.ChucDanh4 = _dmChuKy != null ? _dmChuKy.ChucDanh4MoTa : string.Empty;
                                rptGiaiThichLoi.ChucDanh5 = _dmChuKy != null ? _dmChuKy.ChucDanh5MoTa : string.Empty;
                                rptGiaiThichLoi.ChucDanh6 = _dmChuKy != null ? _dmChuKy.ChucDanh6MoTa : string.Empty;
                                rptGiaiThichLoi.ThuaLenh4 = _dmChuKy != null ? _dmChuKy.ThuaLenh4MoTa : string.Empty;
                                rptGiaiThichLoi.ThuaLenh5 = _dmChuKy != null ? _dmChuKy.ThuaLenh5MoTa : string.Empty;
                                rptGiaiThichLoi.ThuaLenh6 = _dmChuKy != null ? _dmChuKy.ThuaLenh6MoTa : string.Empty;
                                rptGiaiThichLoi.Ten4 = _dmChuKy != null ? _dmChuKy.Ten4MoTa : string.Empty;
                                rptGiaiThichLoi.Ten5 = _dmChuKy != null ? _dmChuKy.Ten5MoTa : string.Empty;
                                rptGiaiThichLoi.Ten6 = _dmChuKy != null ? _dmChuKy.Ten6MoTa : string.Empty;
                                if (QuarterMonthSelected.DisplayItem.Contains("Quý"))
                                {
                                    rptGiaiThichLoi.LoaiThangQuy = "Quý";
                                }
                                else
                                {
                                    rptGiaiThichLoi.LoaiThangQuy = "Tháng";
                                }

                                Dictionary<string, object> data = new Dictionary<string, object>();
                                FormatNumber formatNumber = new FormatNumber(dvt, exportType);
                                data.Add("FormatNumber", formatNumber);
                                foreach (var prop in rptGiaiThichLoi.GetType().GetProperties())
                                {
                                    data.Add(prop.Name, prop.GetValue(rptGiaiThichLoi));
                                }

                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP_GIAITHICH_LOI);
                                fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP_GIAITHICH_LOI.Split(".").First() + "_" + agency.AgencyName + "_" + DateTime.Now.Millisecond;
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
                                results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                            }
                        }
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

        private ExportResult OnExportCoverSheet()
        {
            RptQuyetToanToBia rptToBia = new RptQuyetToanToBia
            {
                Cap1 = _cap1,
                Cap2 = _sessionInfo.TenDonVi,
                TieuDe = "Tổng hợp quý - Ngân sách nhà nước",
                ThoiGian = string.Format("{0} năm {1}", QuarterMonthSelected.DisplayItem, _sessionInfo.YearOfWork),
                Ngay = DateUtils.FormatDateReport(ReportDate),
                DiaDiem = _diaDiem
            };
            Dictionary<string, object> data = new Dictionary<string, object>();
            foreach (var prop in rptToBia.GetType().GetProperties())
            {
                data.Add(prop.Name, prop.GetValue(rptToBia));
            }
            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_TOBIA);
            string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_TOBIA.Split(".").First() + "_" + QuarterMonthSelected.DisplayItem;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        private void FormatDisplay()
        {
            foreach (var item in _reportQtChungTuChiTiets.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
            {
                var parent = _reportQtChungTuChiTiets.Where(x => x.IIdMlns == item.IIdMlnsCha).LastOrDefault();
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

        private void CalculateParent(ReportQtChungTuChiTietQuery currentItem, ReportQtChungTuChiTietQuery selfItem)
        {
            var parentItem = _reportQtChungTuChiTiets.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.ChiTieu += selfItem.ChiTieu;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.TuChi2 += selfItem.TuChi2;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.ThucChi += selfItem.ThucChi;
            CalculateParent(parentItem, selfItem);
        }

        private void OnOpenVerbalExplanationDialog()
        {
            string quarterMonth = String.Empty;
            int quarterMonthType = 0;
            quarterMonth = QuarterMonthSelected.ValueItem;
            if (QuarterMonthSelected.DisplayItem.Contains("Quý"))
                quarterMonthType = (int)QuarterMonth.QUARTER;
            else quarterMonthType = (int)QuarterMonth.MONTH;

            VerbalExplanationViewModel.SettlementVoucher = null;
            VerbalExplanationViewModel.ExplainId = "NN_" + _sessionInfo.YearOfWork + "_" + _sessionInfo.IdDonVi + "_" + quarterMonth + "_" + quarterMonthType;
            VerbalExplanationViewModel.QuarterMonth = Convert.ToInt32(quarterMonth);
            VerbalExplanationViewModel.QuarterMonthType = quarterMonthType;
            VerbalExplanationViewModel.AgencyId = _sessionInfo.IdDonVi;
            VerbalExplanationViewModel.Init();
            var view = new VerbalExplanation { DataContext = VerbalExplanationViewModel };
            view.ShowDialog();
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_NHANUOC_TONGHOP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_QUYETTOAN_NHANUOC_TONGHOP;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.HasAddedSign4 = IsVerbalExplanation;
            DmChuKyDialogViewModel.HasAddedSign5 = IsVerbalExplanation;
            DmChuKyDialogViewModel.HasAddedSign6 = IsVerbalExplanation;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
