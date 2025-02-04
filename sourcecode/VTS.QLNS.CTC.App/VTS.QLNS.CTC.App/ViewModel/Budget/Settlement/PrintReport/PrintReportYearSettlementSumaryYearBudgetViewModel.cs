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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Budget.Settlement.Explanation;
using VTS.QLNS.CTC.App.View.Budget.Settlement.PrintReport;
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
    public class PrintReportYearSettlementSumaryYearBudgetViewModel : ViewModelBase
    {
        private IExportService _exportService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private ILog _logger;
        private IMapper _mapper;
        private INsDonViService _donViService;
        private INsNguoiDungDonViService _nguoiDungDonViService;
        private INsMucLucNganSachService _mucLucNganSachService;
        private INsQtChungTuService _chungTuService;
        private INsQtChungTuChiTietService _chungTuChiTietService;
        private INsQtChungTuChiTietGiaiThichService _chungTuChiTietGiaiThichService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private INsPhongBanService _phongBanService;
        private VerbalExplanationViewModel VerbalExplanationViewModel;
        private DataInterpretationViewModel DataInterpretationViewModel;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private List<ReportQtChungTuChiTietQuery> _reportQtChungTuChiTiets;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private bool _isInitReport;
        private bool _checkAllAgencies;
        private string _quarterMonth;
        private int _quarterMonthType;
        private string _quarterMonthBefore;
        public SettlementVoucherModel SettlementVoucher;
        public List<SettlementVoucherDetailModel> SettlementVoucherDetails;
        public bool IsShowDataNumberButton => SettlementTypeValue == SettlementType.REGULAR_BUDGET;
        public bool IsShowAll { get; set; }
        public bool IsShowDatePeople { get; set; }
        public string TieuDeBaoCao { get; set; }

        private string SettlementName => "Quyết toán năm - Tổng hợp năm ngân sách";


        public override string Name => "In báo cáo " + SettlementName;
        public override string Title => "In báo cáo " + SettlementName;
        public override string Description => "In báo cáo " + SettlementName;
        public override Type ContentType => typeof(PrintReportYearSettlementSumaryYearBudget);

        public string SettlementTypeValue;
        public bool IsEnableCheckBoxSummary => _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString();

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
                    ProcessMonth();
                    if (!_isInitReport)
                        LoadAgencies();
                }
            }
        }

        private List<ComboboxItem> _reportTypes;
        public List<ComboboxItem> ReportTypes
        {
            get => _reportTypes;
            set => SetProperty(ref _reportTypes, value);
        }

        private ComboboxItem _selectedReportType;
        public ComboboxItem SelectedReportType
        {
            get => _selectedReportType;
            set
            {
                SetProperty(ref _selectedReportType, value);
                OnPropertyChanged(nameof(IsEnableCheckBoxSummary));
                OnPropertyChanged(nameof(IsEnabledExplanation));
                LoadAgencies();
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

        public bool IsEnabledExplanation => (IsEnableCheckBoxSummary && IsSummary) || (IsEnableCheckBoxSummary && IsSummaryAgency);

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

        public bool IsExportEnable => _budgetIndexes != null && _budgetIndexes.Where(x => x.IsSelected).Count() > 0 && (_isCoverSheet || _isData || _isVerbalExplanation || _isDataInterpretation || _isCheckAll);
        //public bool IsExportEnable => _budgetIndexes != null && _budgetIndexes.Where(x => x.IsSelected).Count() > 0 && (_isCoverSheet || _isData || _isVerbalExplanation || _isDataInterpretation );

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

        private bool _isDatePeople;
        public bool IsDatePeople
        {
            get => _isDatePeople;
            set
            {
                SetProperty(ref _isDatePeople, value);
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

        private bool _isDataInterpretation;
        public bool IsDataInterpretation
        {
            get => _isDataInterpretation;
            set
            {
                SetProperty(ref _isDataInterpretation, value);
                OnPropertyChanged(nameof(IsExportEnable));
            }
        }
        private bool _isCheckAll;
        public bool IsCheckAll
        {
            get => _isCheckAll;
            set
            {
                SetProperty(ref _isCheckAll, value);
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
                {
                    OnPropertyChanged(nameof(IsEnabledExplanation));
                    if (!_isInitReport)
                        LoadAgencies();
                }
            }
        }

        private bool _isSummaryAgency;
        public bool IsSummaryAgency
        {
            get => _isSummaryAgency;
            set
            {
                if (SetProperty(ref _isSummaryAgency, value))
                {
                    OnPropertyChanged(nameof(IsEnabledExplanation));
                }
            }
        }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand DataInterpretationCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintReportYearSettlementSumaryYearBudgetViewModel(INsMucLucNganSachService mucLucNganSachService,
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            INsQtChungTuService chungTuService,
            INsQtChungTuChiTietService chungTuChiTietService,
            INsQtChungTuChiTietGiaiThichService chungTuChiTietGiaiThichService,
            IExportService exportService,
            INsDonViService donViService,
            INsNguoiDungDonViService nguoiDungDonViService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            INsPhongBanService phongBanService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            DataInterpretationViewModel dataInterpretationViewModel,
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
            _nguoiDungDonViService = nguoiDungDonViService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _phongBanService = phongBanService;
            DataInterpretationViewModel = dataInterpretationViewModel;
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
            DataInterpretationCommand = new RelayCommand(obj => OnOpenDataInterpretationDialog());
            VerbalExplanationCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            _isInitReport = true;
            _agencies = new ObservableCollection<AgencyModel>();
            _budgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
            IsSummary = false;
            IsSummaryAgency = false;
            IsDataInterpretation = false;
            ResetCondition();
            LoadTieuDe();
            LoadReportType();
            LoadQuarterMonths();
            LoadDanhMuc();
            LoadChiTietToi();
            LoadKhoi();
            LoadBQuanLy();
            LoadAgencies();
            _isInitReport = false;
            IsShowAll = _sessionInfo.YearOfBudget == 1 || _sessionInfo.YearOfBudget == 4;
        }

        private void LoadTieuDe()
        {
            var typeChuKy = SettlementTypeValue switch
            {
                SettlementType.REGULAR_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP,
                SettlementType.DEFENSE_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP,
                SettlementType.STATE_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_NHANUOC_TONGHOP,
                SettlementType.FOREX_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_NGOAIHOI_TONGHOP,
                SettlementType.EXPENSE_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_KINHPHIKHAC_TONGHOP,
                _ => TypeChuKy.RPT_NS_QUYETTOAN_TATCA_TONGHOP
            };
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }

        private void LoadReportType()
        {
            _reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencyDetail.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() }
            };
            _selectedReportType = _reportTypes.First();
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

        private void ProcessMonth()
        {
            if (QuarterMonthSelected.DisplayItem.Contains("Quý"))
            {
                _quarterMonthType = (int)QuarterMonth.QUARTER;
                _quarterMonth = string.Join(",", Enumerable.Range(Convert.ToInt32(QuarterMonthSelected.ValueItem) - 2, 3).ToList());
                _quarterMonthBefore = string.Join(",", Enumerable.Range(0, Convert.ToInt32(QuarterMonthSelected.ValueItem) - 2).ToList());
            }
            else
            {
                _quarterMonthType = (int)QuarterMonth.MONTH;
                _quarterMonth = QuarterMonthSelected.ValueItem;
                _quarterMonthBefore = string.Join(",", Enumerable.Range(0, Convert.ToInt32(QuarterMonthSelected.ValueItem)).ToList());
            }
        }

        private void LoadAgencies()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                if (SelectedKhoi != null && QuarterMonthSelected != null)
                {
                    List<DonVi> agencies = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
                    List<DonVi> listDonViReport = new List<DonVi>();
                    listDonViReport = _donViService.FindByQtTongHopQuy(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget.ToString(), _sessionInfo.Budget,
                                            string.Format("{0},{1}", _quarterMonthBefore, _quarterMonth), SelectedKhoi.ValueItem, SettlementTypeValue, _sessionInfo.Principal).ToList();
                    if (!(IsEnableCheckBoxSummary && IsSummary))
                        listDonViReport = listDonViReport.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                    else listDonViReport = listDonViReport.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();
                    e.Result = listDonViReport;
                }
            }, (s, e) =>
            {
                if (e.Result != null)
                {
                    List<DonVi> listDonViReport = (List<DonVi>)e.Result;
                    _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonViReport);
                }
                else
                    _agencies = new ObservableCollection<AgencyModel>();
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
                IsLoading = false;
            });
        }

        private bool ListAgencyFilter(object obj)
        {
            bool result = true;
            var item = (AgencyModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchAgencyText))
                result = item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadBudgetIndexes()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                if (SelectedBQuanLy != null)
                {
                    string agencyIds = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
                    List<NsMucLucNganSach> listMLNS = _mucLucNganSachService.FindByQtTongHopQuy(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget.ToString(), _sessionInfo.Budget,
                        agencyIds, string.Format("{0},{1}", _quarterMonthBefore, _quarterMonth), SettlementTypeValue, _sessionInfo.Principal).ToList();
                    e.Result = listMLNS;
                }
            }, (s, e) =>
            {
                List<NsMucLucNganSach> listMLNS = (List<NsMucLucNganSach>)e.Result;
                if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                    _budgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMLNS.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem));
                else _budgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMLNS);
                OnPropertyChanged(nameof(BudgetIndexes));

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
                            OnPropertyChanged(nameof(IsExportEnable));
                            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                            OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                        }
                    };
                }
                OnPropertyChanged(nameof(IsExportEnable));
                OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                IsLoading = false;
            });

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
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(x => x.SGiaTri)
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
                DataInToiMuc = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi, false));
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
                    if (IsCoverSheet)
                    {
                        var dataResult = OnExportCoverSheet();
                        if (dataResult != null)
                            results.Add(dataResult);
                    }

                    if (IsData || IsVerbalExplanation || IsDataInterpretation)
                    {
                        _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                        int dvt = Convert.ToInt32(SelectedUnit.ValueItem);

                        ReportSettlementCriteria condition = new ReportSettlementCriteria
                        {
                            YearOfWork = _sessionInfo.YearOfWork,
                            YearOfBudget = _sessionInfo.YearOfBudget,
                            BudgetSource = _sessionInfo.Budget,
                            QuarterMonthType = _quarterMonthType,
                            QuarterMonth = _quarterMonth,
                            QuarterMonthBefore = _quarterMonthBefore,
                            VoucherDate = new DateTime(_sessionInfo.YearOfWork, int.Parse(QuarterMonthSelected.ValueItem), DateTime.DaysInMonth(_sessionInfo.YearOfWork, int.Parse(QuarterMonthSelected.ValueItem))),
                            LNS = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns).ToArray()),
                            Dvt = dvt
                        };
                        if (IsCheckAll)
                        {
                            ReportSettlementCriteria condition2year = new ReportSettlementCriteria
                            {
                                YearOfWork = _sessionInfo.YearOfWork,
                                YearOfBudget = 1,
                                BudgetSource = _sessionInfo.Budget,
                                QuarterMonthType = _quarterMonthType,
                                QuarterMonth = _quarterMonth,
                                QuarterMonthBefore = _quarterMonthBefore,
                                VoucherDate = new DateTime(_sessionInfo.YearOfWork, int.Parse(QuarterMonthSelected.ValueItem), DateTime.DaysInMonth(_sessionInfo.YearOfWork, int.Parse(QuarterMonthSelected.ValueItem))),
                                LNS = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns).ToArray()),
                                Dvt = dvt
                            };
                            condition2year.AgencyId = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
                            condition2year.EstimateAgencyId = condition2year.AgencyId;
                            results.AddRange(ProcessFile(null, condition2year, dvt, exportType));
                        }
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                        {
                            foreach (var agency in Agencies.Where(x => x.Selected))
                            {
                                condition.EstimateAgencyId = agency.Id;
                                condition.AgencyId = agency.Id;
                                results.AddRange(ProcessFile(agency, condition, dvt, exportType));
                            }
                        }
                        else
                        {
                            condition.AgencyId = string.Join(StringUtils.COMMA, Agencies.Where(x => x.Selected).Select(x => x.Id));
                            condition.EstimateAgencyId = condition.AgencyId;
                            results.AddRange(ProcessFile(null, condition, dvt, exportType));


                            /*
                            DonVi donVi = _donViService.FindAll().FirstOrDefault(n => n.NamLamViec == _sessionInfo.YearOfWork && n.IIDMaDonVi == condition.AgencyId);
                            if (donVi != null && !string.IsNullOrEmpty(donVi.Khoi))
                            {
                                results.AddRange(ProcessFile(null, condition, dvt, exportType));
                            }
                            else
                            {
                                /*
                                NsQtChungTu chungTuTongHop = _chungTuService.FindByCondition(n => n.INamLamViec == _sessionInfo.YearOfWork && n.IIdMaDonVi == condition.AgencyId).FirstOrDefault();
                                if (chungTuTongHop == null || string.IsNullOrEmpty(chungTuTongHop.STongHop))
                                {
                                    results.AddRange(ProcessFile(null, condition, dvt, exportType));
                                }
                                else
                                {
                                    List<NsQtChungTu> chungTuTongHopList = _chungTuService.FindByCondition(n => n.INamLamViec == _sessionInfo.YearOfWork && chungTuTongHop.STongHop.Contains(n.SSoChungTu)).ToList();
                                    condition.AgencyId = string.Join(",", chungTuTongHopList.Select(x => x.IIdMaDonVi));
                                    results.AddRange(ProcessFile(null, condition, dvt, exportType));
                                }
                                **********
                                var listChungTuTongHop = _chungTuService.FindByCondition(n => n.INamLamViec == _sessionInfo.YearOfWork
                                                                                           && n.INamNganSach == _sessionInfo.YearOfBudget
                                                                                           && n.IIdMaNguonNganSach == _sessionInfo.Budget
                                                                                           && n.IIdMaDonVi == condition.AgencyId).Where(x => x.SLoai == SettlementTypeValue).ToList();
                                if (listChungTuTongHop == null || listChungTuTongHop.Count() == 0)
                                {
                                    results.AddRange(ProcessFile(null, condition, dvt, exportType));
                                } else
                                {
                                    List<NsQtChungTu> listChungTuCon = _chungTuService.FindByCondition(n => n.INamLamViec == _sessionInfo.YearOfWork
                                                                                                         && n.INamNganSach == _sessionInfo.YearOfBudget
                                                                                                         && n.IIdMaNguonNganSach == _sessionInfo.Budget
                                                && listChungTuTongHop.Any(x => !string.IsNullOrEmpty(x.STongHop) && x.STongHop.Contains(n.SSoChungTu) && x.SLoai == n.SLoai)).ToList();
                                    condition.AgencyId = string.Join(StringUtils.COMMA, listChungTuCon.Select(x => x.IIdMaDonVi).Distinct());
                                    results.AddRange(ProcessFile(null, condition, dvt, exportType));
                                }
                            }
                            */
                        }
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (result.Count() == 0)
                            MessageBoxHelper.Info(Resources.AlertEmptyReport);
                        else _exportService.Open(result, exportType);
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

        private ExportResult OnExportDataInterpretation(string IdDonVi, string quarterMonth, int quarterMonthType, string agencyName, string sLoai, int dvt)
        {
            //SettlementVoucherDetailExplainCriteria condition = new SettlementVoucherDetailExplainCriteria
            //{
            //    VoucherId = Guid.Empty,
            //    ExplainId = "TX_" + _sessionInfo.YearOfWork + "_" + _sessionInfo.IdDonVi + "_" + quarterMonth + "_" + quarterMonthType,
            //    AgencyId = _sessionInfo.IdDonVi,
            //    YearOfWork = _sessionInfo.YearOfWork
            //};
            //NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindByCondition(condition);
            //Lấy iid của đơn vị tổng hợp
            int iTongHop = 0;
            string ExplainId = string.Empty;
            if (string.IsNullOrEmpty(IdDonVi))
            {
                var predicate = PredicateBuilder.True<DonVi>();
                predicate = predicate.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
                predicate = predicate.And(x => x.Loai == LoaiDonVi.ROOT);
                var donvi = _donViService.FindAll(predicate).ToList();
                IdDonVi = donvi == null ? String.Empty : donvi.FirstOrDefault().IIDMaDonVi;
                iTongHop = 1;
                if (SettlementTypeValue != SettlementType.REGULAR_BUDGET)
                {
                    ExplainId = "TX_" + _sessionInfo.YearOfWork + StringUtils.UNDERSCORE + _sessionInfo.IdDonVi + StringUtils.UNDERSCORE + quarterMonth + StringUtils.UNDERSCORE + quarterMonthType;
                }
                else
                {
                    ExplainId = "NV_" + _sessionInfo.YearOfWork + StringUtils.UNDERSCORE + _sessionInfo.IdDonVi + StringUtils.UNDERSCORE + quarterMonth + StringUtils.UNDERSCORE + quarterMonthType;
                }

            }
            IEnumerable<NsQtChungTuChiTietGiaiThich> lstchungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.GetDataChungTuGiaiTichBangSo(sLoai, IdDonVi, Convert.ToInt16(quarterMonth), quarterMonthType
                                                                                                                , _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, iTongHop, ExplainId);

            RptQuyetToanGiaiThichSo rptGiaiThichSo = _mapper.Map<RptQuyetToanGiaiThichSo>(lstchungTuChiTietGiaiThich.FirstOrDefault());

            if (rptGiaiThichSo == null)
            {
                rptGiaiThichSo = new RptQuyetToanGiaiThichSo();
                rptGiaiThichSo.FLuongSiQuanQt = 0;
                rptGiaiThichSo.FPhuCapSiQuanQt = 0;

                rptGiaiThichSo.FLuongQncnQt = 0;
                rptGiaiThichSo.FPhuCapQncnQt = 0;

                rptGiaiThichSo.FLuongCnvqpQt = 0;
                rptGiaiThichSo.FPhuCapCnvqpQt = 0;
                rptGiaiThichSo.FLuongHdQt = 0;
                rptGiaiThichSo.FPhuCapHdQt = 0;
                rptGiaiThichSo.DonVi = agencyName;
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var prop in rptGiaiThichSo.GetType().GetProperties())
                {
                    data.Add(prop.Name, prop.GetValue(rptGiaiThichSo));
                }
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP_GIAITHICH_SO);
                string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP_GIAITHICH_SO.Split(".").First();
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
                return new ExportResult(IdDonVi + " - " + agencyName, fileNameWithoutExtension, null, xlsFile);
            }
            else
            {
                foreach (var item in rptGiaiThichSo.GetType().GetProperties())
                {
                    if (item.PropertyType.Name == "Double" && !item.Name.StartsWith("FNgay") && !item.Name.StartsWith("FRaQuan"))
                    {
                        item.SetValue(rptGiaiThichSo, Convert.ToDouble(item.GetValue(rptGiaiThichSo)) / dvt, null);
                    }
                }

                /*
                rptGiaiThichSo.FLuongSiQuanQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000-6001-10-00");
                rptGiaiThichSo.FPhuCapSiQuanQt = SumDataFromSettelemtVoucherDetails(new List<string> { "1010000-010-011-6100-6101-10-00", "1010000-010-011-6100-6107-10-00", "1010000-010-011-6100-6124-10-00" });

                rptGiaiThichSo.FLuongQncnQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000-6001-20-00");
                rptGiaiThichSo.FPhuCapQncnQt = SumDataFromSettelemtVoucherDetails(new List<string> { "1010000-010-011-6100-6101-20-00", "1010000-010-011-6100-6107-20-00", "1010000-010-011-6100-6124-20-00" });

                rptGiaiThichSo.FLuongCnvqpQt = SumDataFromSettelemtVoucherDetails(new List<string> { "1010000-010-011-6000-6001-30-00", "1010000-010-011-6000-6001-40-00", "1010000-010-011-6000-6001-70-00" });
                rptGiaiThichSo.FPhuCapCnvqpQt = SumDataFromSettelemtVoucherDetails(new List<string> {
                "1010000-010-011-6100-6101-30-00", "1010000-010-011-6100-6101-40-00", "1010000-010-011-6100-6101-70-00",
                "1010000-010-011-6100-6107-30-00", "1010000-010-011-6100-6124-30-00" });
                rptGiaiThichSo.FLuongHdQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000-6001-90-00");
                rptGiaiThichSo.FPhuCapHdQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6100-6101-90-00");
                */

                rptGiaiThichSo.DonVi = agencyName;
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var prop in rptGiaiThichSo.GetType().GetProperties())
                {
                    data.Add(prop.Name, prop.GetValue(rptGiaiThichSo));
                }
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP_GIAITHICH_SO);
                string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP_GIAITHICH_SO.Split(".").First();
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
                return new ExportResult(IdDonVi + " - " + agencyName, fileNameWithoutExtension, null, xlsFile);
            }
        }

        /// <summary>
        /// tính lương từ chứng từ chi tiết
        /// </summary>
        /// <param name="concatenateCode"></param>Z
        /// <returns></returns>
        private double GetDataFromSettlementVoucherDetails(string concatenateCode, string code)
        {
            var listCode = code.Split(StringUtils.COMMA).ToList();
            var mlns = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork)
                                                .Where(n => listCode.Contains(n.SMaCB) && n.XauNoiMa.StartsWith(concatenateCode));

            var detail = from ss in _reportQtChungTuChiTiets
                         join ml in mlns on ss.IIdMlns equals ml.MlnsId
                         select ss;

            var detailVoucher = _mapper.Map<List<ReportQtChungTuChiTietQuery>>(detail);

            return detailVoucher.Where(x => x.IsHangCha == false).Sum(x => x.TuChi);
        }

        /// <summary>
        /// tính phụ cấp từ chứng từ chi tiết
        /// </summary>
        /// <param name="concatenateCodes"></param>
        /// <returns></returns>
        private double SumDataFromSettelemtVoucherDetails(List<string> concatenateCodes)
        {
            double sum = 0;
            foreach (var item in _reportQtChungTuChiTiets.Where(x => concatenateCodes.Contains(x.XauNoiMa)).ToList())
            {
                sum += item.TuChi;
            }
            return sum;
        }

        private List<ExportResult> ProcessFile(AgencyModel agency, ReportSettlementCriteria condition, int dvt, ExportType exportType)
        {
            List<ExportResult> results = new List<ExportResult>();

            _reportQtChungTuChiTiets = !IsCheckAll ? _chungTuChiTietService.FindForReportQuarterlySummary(condition) : _chungTuChiTietService.FindForReportQuarterlySummary2Year(condition);

            RptQuyetToanChungTu rptChungTu = new RptQuyetToanChungTu();
            rptChungTu.Cap1 = _cap1;
            rptChungTu.Cap2 = _sessionInfo.TenDonVi;
            rptChungTu.TieuDe1 = Title1;
            rptChungTu.TieuDe2 = Title2;
            rptChungTu.TieuDe3 = Title3;
            if (IsCheckAll)
            {
                rptChungTu.NamNganSach = "Năm trước chuyển sang";
            }
            else if (_sessionInfo.YearOfBudget == 1)
            {
                rptChungTu.NamNganSach = "Năm trước đã cấp";
            }
            else if (_sessionInfo.YearOfBudget == 2)
            {
                rptChungTu.NamNganSach = "Năm nay";
            }
            else if (_sessionInfo.YearOfBudget == 4)
            {
                rptChungTu.NamNganSach = "Năm trước chưa cấp";
            }
            foreach (var item in _reportQtChungTuChiTiets.Where(x => !x.IsHangCha || x.ChiTieuOrigin != 0))
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
                    _reportQtChungTuChiTiets.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                    break;
                case nameof(MLNSFiled.TNG):
                    _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                    _reportQtChungTuChiTiets.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                    break;
                case nameof(MLNSFiled.TNG1):
                    _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                    _reportQtChungTuChiTiets.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                    break;
                case nameof(MLNSFiled.TNG2):
                    _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                    _reportQtChungTuChiTiets.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                    break;
            }
            FormatDisplay();
            // dữ liệu lấy không tính đến mục có LNS
            rptChungTu.Items = _reportQtChungTuChiTiets;
            rptChungTu.TongSoNgay = _reportQtChungTuChiTiets.Sum(x => x.SoNgay);
            rptChungTu.TongSoNguoi = 0;
            //rptChungTu.TongSoNguoi = _reportQtChungTuChiTiets.Sum(x => x.SoNguoi);
            rptChungTu.TienTuChi = StringUtils.NumberToText(rptChungTu.TongTuChi * dvt);
            rptChungTu.DonVi = agency != null ? agency.AgencyName.Split("-")[1] : "";
            rptChungTu.ThoiGian = QuarterMonthSelected.DisplayItem + " năm " + _sessionInfo.YearOfWork;
            rptChungTu.ThangQuy = condition.QuarterMonthType == (int)QuarterMonth.QUARTER ? "quý" : "tháng";
            rptChungTu.MauSo = condition.QuarterMonthType == (int)QuarterMonth.QUARTER ? "02Q/BCQT" : "01Q/BCQT";
            rptChungTu.H2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem);
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

            string agencyName = agency != null ? agency.AgencyName : "Tổng hợp";

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
                string templateFileName = String.Empty;
                string fileNamePrefix = String.Empty;
                templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP_SONGUOI_SONGAY);
                fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP_SONGUOI_SONGAY.Split(StringUtils.DOT).First() + StringUtils.UNDERSCORE + agencyName + StringUtils.UNDERSCORE + DateTime.Now.Millisecond;
                if (!IsDatePeople && (SettlementTypeValue == SettlementType.DEFENSE_BUDGET || SettlementTypeValue != SettlementType.REGULAR_BUDGET))
                {
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP);
                    fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP.Split(StringUtils.DOT).First() + StringUtils.UNDERSCORE + agencyName + StringUtils.UNDERSCORE + DateTime.Now.Millisecond;
                    hideColumns.Add(15);          // Ẩn cột Số người          
                    hideColumns.Add(16);          // Ẩn cột Số ngày
                }


                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data, hideColumns);
                results.Add(new ExportResult(agencyName, fileNameWithoutExtension, null, xlsFile));
            }
            string quarterMonth = String.Empty;
            int quarterMonthType = 0;
            quarterMonth = QuarterMonthSelected.ValueItem;
            if (QuarterMonthSelected.DisplayItem.Contains("Quý"))
                quarterMonthType = (int)QuarterMonth.QUARTER;
            else quarterMonthType = (int)QuarterMonth.MONTH;

            //13-10-2022 loại báo cáo tổng hợp vẫn in ra trang tờ giải thích bằng lời và bằng số
            if (IsVerbalExplanation)
            {

                //SettlementVoucherDetailExplainCriteria explainCondition = new SettlementVoucherDetailExplainCriteria
                //{
                //    VoucherId = Guid.Empty,
                //    ExplainId = "NV_" + _sessionInfo.YearOfWork + "_" + _sessionInfo.IdDonVi + "_" + quarterMonth + "_" + quarterMonthType,
                //    AgencyId = _sessionInfo.IdDonVi,
                //    YearOfWork = _sessionInfo.YearOfWork
                //};
                //NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindByCondition(explainCondition);
                int iTongHop = 0;
                string ExplainId = string.Empty;
                string idMaDonVi = agency == null ? string.Empty : agency.Id;
                if (string.IsNullOrEmpty(idMaDonVi))
                {
                    var predicate = PredicateBuilder.True<DonVi>();
                    predicate = predicate.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
                    predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
                    predicate = predicate.And(x => x.Loai == LoaiDonVi.ROOT);
                    var donvi = _donViService.FindAll(predicate).ToList();
                    idMaDonVi = donvi == null ? String.Empty : donvi.FirstOrDefault().IIDMaDonVi;
                    iTongHop = 1;
                    if (SettlementTypeValue == SettlementType.REGULAR_BUDGET)
                    {
                        ExplainId = "TX_" + _sessionInfo.YearOfWork + "_" + _sessionInfo.IdDonVi + "_" + quarterMonth + "_" + quarterMonthType;
                    }
                    else
                    {
                        ExplainId = "NV_" + _sessionInfo.YearOfWork + "_" + _sessionInfo.IdDonVi + "_" + quarterMonth + "_" + quarterMonthType;
                    }
                }
                IEnumerable<NsQtChungTuChiTietGiaiThich> chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.GetDataChungTuGiaiTichBangLoi(SettlementTypeValue, idMaDonVi, Convert.ToInt16(quarterMonth), quarterMonthType
                                                                                                                , _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, iTongHop, ExplainId);
                RptQuyetToanGiaiThichLoi rptGiaiThichLoi = new RptQuyetToanGiaiThichLoi();
                rptGiaiThichLoi.Tien = rptChungTu.TongTuChi;
                rptGiaiThichLoi.TienTuChi = StringUtils.NumberToText(rptChungTu.TongTuChi * dvt);
                rptGiaiThichLoi.MoTaTinhHinh = chungTuChiTietGiaiThich.Count() == 0 ? string.Empty : chungTuChiTietGiaiThich.FirstOrDefault().SMoTaTinhHinh;
                rptGiaiThichLoi.MoTaKienNghi = chungTuChiTietGiaiThich.Count() == 0 ? string.Empty : chungTuChiTietGiaiThich.FirstOrDefault().SMoTaKienNghi;
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

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP_GIAITHICH_LOI);
                string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP_GIAITHICH_LOI.Split(".").First() + "_" + agencyName + "_" + DateTime.Now.Millisecond;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
                results.Add(new ExportResult(agencyName, fileNameWithoutExtension, null, xlsFile));
            }
            if (IsDataInterpretation)
            {
                //agency = null-> in theo đon vị tổng hợp
                string idMaDonVi = agency == null ? string.Empty : agency.Id;

                results.Add(OnExportDataInterpretation(idMaDonVi, quarterMonth, quarterMonthType, agencyName, SettlementTypeValue, dvt));
            }
            return results;
        }

        private ExportResult OnExportCoverSheet()
        {
            RptQuyetToanToBia rptToBia = new RptQuyetToanToBia
            {
                Cap1 = _cap1,
                Cap2 = _sessionInfo.TenDonVi,
                //TieuDe = "Tổng hợp quý - Ngân sách nghiệp vụ, quốc phòng khác",
                TieuDe = "Tổng hợp quý - " + TieuDeBaoCao,
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

        private void CalculateParent(ReportQtChungTuChiTietQuery currentItem, ReportQtChungTuChiTietQuery selfItem)
        {
            var parentItem = _reportQtChungTuChiTiets.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parentItem == null) return;
            if (selfItem.ChiTieuOrigin != 0)
                parentItem.ChiTieu += selfItem.ChiTieu;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.TuChi2 += selfItem.TuChi2;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.ThucChi += selfItem.ThucChi;
            CalculateParent(parentItem, selfItem);
        }

        private void OnOpenDataInterpretationDialog()
        {
            if (SettlementVoucher == null)
            {
                MessageBoxHelper.Warning(Resources.AlertVoucherSummaryEmptySelected);
                return;
            }
            DataInterpretationViewModel.SettlementVoucher = SettlementVoucher;
            var chungTuChiTiets = _chungTuChiTietService.FindByCondition(x => x.IIdQtchungTu == SettlementVoucher.Id).ToList();
            DataInterpretationViewModel.SettlementVoucherDetails = _mapper.Map<List<SettlementVoucherDetailModel>>(chungTuChiTiets);
            DataInterpretationViewModel.Init();
            var view = new DataInterpretation { DataContext = DataInterpretationViewModel };
            view.ShowDialog();
        }

        private void OnOpenDataInterpretationDialogOld()
        {
            string quarterMonth = QuarterMonthSelected.ValueItem;
            int quarterMonthType = 0;
            if (QuarterMonthSelected.DisplayItem.Contains("Quý"))
                quarterMonthType = (int)QuarterMonth.QUARTER;
            else
                quarterMonthType = (int)QuarterMonth.MONTH;
            DataInterpretationViewModel.SettlementVoucher = null;
            DataInterpretationViewModel.SettlementVoucherDetails = null;
            DataInterpretationViewModel.ExplainId = "TX_" + _sessionInfo.YearOfWork + "_" + _sessionInfo.IdDonVi + "_" + quarterMonth + "_" + quarterMonthType;
            DataInterpretationViewModel.QuarterMonth = Convert.ToInt32(quarterMonth);
            DataInterpretationViewModel.QuarterMonthType = quarterMonthType;
            DataInterpretationViewModel.AgencyId = _sessionInfo.IdDonVi;
            DataInterpretationViewModel.Init();
            var view = new DataInterpretation { DataContext = DataInterpretationViewModel };
            view.ShowDialog();
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
            if (SettlementTypeValue != SettlementType.REGULAR_BUDGET)
            {
                VerbalExplanationViewModel.ExplainId = "NV_" + _sessionInfo.YearOfWork + "_" + _sessionInfo.IdDonVi + "_" + quarterMonth + "_" + quarterMonthType;
            }
            else
            {
                VerbalExplanationViewModel.ExplainId = "TX_" + _sessionInfo.YearOfWork + "_" + _sessionInfo.IdDonVi + "_" + quarterMonth + "_" + quarterMonthType;
            }

            VerbalExplanationViewModel.QuarterMonth = Convert.ToInt32(quarterMonth);
            VerbalExplanationViewModel.QuarterMonthType = quarterMonthType;
            VerbalExplanationViewModel.AgencyId = _sessionInfo.IdDonVi;
            VerbalExplanationViewModel.Init();
            var view = new VerbalExplanation { DataContext = VerbalExplanationViewModel };
            view.ShowDialog();
        }

        /// <summary>
        /// Action when checkbox select all is selected
        /// </summary>
        /// <param name="select">true/false</param>
        /// <param name="models">items source of data grid</param>
        private static void SelectAll(bool select, IEnumerable<NsMuclucNgansachModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }

        private void FormatDisplay()
        {
            foreach (var item in _reportQtChungTuChiTiets.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
            {
                var parent = _reportQtChungTuChiTiets.Where(x => x.IIdMlns == item.IIdMlnsCha).LastOrDefault();
                if (parent != null && item.M != string.Empty)
                {
                    if (!string.IsNullOrEmpty(parent.L) && !string.IsNullOrEmpty(parent.K))
                    {
                        item.L = string.Empty;
                        item.K = string.Empty;
                        item.LNS = string.Empty;
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

        private void OnConfigSign()
        {
            var typeChuKy = SettlementTypeValue switch
            {
                SettlementType.REGULAR_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP,
                SettlementType.DEFENSE_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP,
                SettlementType.STATE_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_NHANUOC_TONGHOP,
                SettlementType.FOREX_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_NGOAIHOI_TONGHOP,
                SettlementType.EXPENSE_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_KINHPHIKHAC_TONGHOP,
                _ => TypeChuKy.RPT_NS_QUYETTOAN_TATCA_TONGHOP
            };

            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = typeChuKy;
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
