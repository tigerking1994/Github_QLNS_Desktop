using AutoMapper;
using ControlzEx.Standard;
using log4net;
using MaterialDesignColors.ColorManipulation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Windows.Data;
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
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.PrintReport
{
    public class PrintQuyetToanThuViewModel : ViewModelBase
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
        private IQttBHXHService _chungTuService;
        private IQttBHXHChiTietService _chungTuChiTietService;
        private IQttBHXHChiTietGiaiThichService _explainService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private bool _checkAllAgencies;
        private string _quarterMonth;
        private int _quarterMonthType;
        private string _quarterMonthBefore;
        public BhQttBHXHModel SettlementVoucher;
        public List<BhQttBHXHChiTietModel> SettlementVoucherDetails;
        public bool IsShowAll { get; set; }
        public bool IsShowDatePeople { get; set; }
        public string TieuDeBaoCao { get; set; }
        public int SettlementTypeValue;
        private string _typeChuky;
        private string SettlementName => SettlementTypeValue switch
        {
            (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY => "In báo cáo thu nộp BHYT, BHXH, BHTN",
            (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM => "In báo cáo thu nộp BHYT, BHXH, BHTN năm",
            (int)QttType.QUYET_TOAN_THU_BHXH => "In bản quyết toán thu BHXH",
            (int)QttType.QUYET_TOAN_THU_BHTN => "In bản quyết toán thu BHTN",
            (int)QttType.QUYET_TOAN_THU_BHYT_QUAN_NHAN => "In bản quyết toán thu BHYT Quân nhân",
            (int)QttType.QUYET_TOAN_THU_BHYT_NLD => "In bản quyết toán thu BHYT NLĐ",
            (int)QttType.QUYET_TOAN_THU_CHI_TONG_HOP => "Báo cáo tổng hợp quyết toán thu, chi BHXH, BHTN, BHYT năm",
            (int)QttType.QUYET_TOAN_TONG_HOP_NAM => "In thông báo phê duyệt quyết toán thu, chi BHXH, BHYT, BHTN",
            _ => ""
        };

        public override string Name => SettlementName;
        public override string Title => SettlementName;
        public override string Description => SettlementName;
        public bool IsEnableCheckBoxSummary => _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() || _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryDetail.ToString();
        public bool IsEnableCheckBoxSummaryQuarter => (_selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() || _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryDetail.ToString()) && SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY;
        public bool IsEnableCheckBoxSummaryYear => (_selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() || _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryDetail.ToString()) && SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM;
        private List<ComboboxItem> _quarterMonths;
        public List<ComboboxItem> QuarterMonths
        {
            get => _quarterMonths;
            set => SetProperty(ref _quarterMonths, value);
        }

        private ComboboxItem _quarterMonthSelected;
        public ComboboxItem QuarterMonthQTTSelected
        {
            get => _quarterMonthSelected;
            set
            {
                SetProperty(ref _quarterMonthSelected, value);
                LoadAgencies();
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
                OnPropertyChanged(nameof(IsEnableCheckBoxSummaryQuarter));
                OnPropertyChanged(nameof(IsEnableCheckBoxSummaryYear));
                OnPropertyChanged(nameof(IsEnabledExplanation));
                LoadAgencies();
            }
        }

        #region list agency
        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set
            {
                SetProperty(ref _agencies, value);
                OnPropertyChanged(nameof(IsExportEnable));
            }
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

        private bool _isEnableLoaiThu;
        public bool IsEnableLoaiThu
        {
            get => _isEnableLoaiThu;
            set => SetProperty(ref _isEnableLoaiThu, value);
        }

        private bool _isEnableReportType;
        public bool IsEnableReportType
        {
            get => _isEnableReportType;
            set => SetProperty(ref _isEnableReportType, value);
        }

        private bool _isEnableReportTypeYear;
        public bool IsEnableReportTypeYear
        {
            get => _isEnableReportTypeYear;
            set => SetProperty(ref _isEnableReportTypeYear, value);
        }

        private bool _isEnableInTheo;
        public bool IsEnableInTheo
        {
            get => _isEnableInTheo;
            set => SetProperty(ref _isEnableInTheo, value);
        }

        public bool IsEnableByMonthQuarter => SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY;
        public bool IsEnableByYear => SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM;

        public bool IsExportEnable
        {
            get
            {
                if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHXH
                    || SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHTN
                    || SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHYT_QUAN_NHAN
                    || SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHYT_NLD
                    || SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_CHI_TONG_HOP
                    || SettlementTypeValue == (int)QttType.QUYET_TOAN_TONG_HOP_NAM
                    || IsDataExplanation || IsVerbalExplanation)
                {
                    return true;
                }
                else if (IsSummary && IsSummaryAgency)
                {
                    return false;
                }
                else
                {
                    return _isData;
                }
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

        private bool _isInLuyKe;
        public bool IsInLuyKe
        {
            get => _isInLuyKe;
            set
            {
                SetProperty(ref _isInLuyKe, value);
            }
        }

        private LoaiThu _loaiThu;
        public LoaiThu LoaiThu
        {
            get => _loaiThu;
            set
            {
                SetProperty(ref _loaiThu, value);
                LoadTypeChuKy();
                LoadTieuDe();
            }
        }

        private bool _isDataExplanation;
        public bool IsDataExplanation
        {
            get => _isDataExplanation;
            set
            {
                SetProperty(ref _isDataExplanation, value);
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

        private bool _isSummary;
        public bool IsSummary
        {
            get => _isSummary;
            set
            {
                if (SetProperty(ref _isSummary, value))
                {
                    OnPropertyChanged(nameof(IsEnabledExplanation));
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
                    LoadAgencies();
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

        public PrintQuyetToanThuViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IQttBHXHService chungTuService,
            IQttBHXHChiTietService chungTuChiTietService,
            IQttBHXHChiTietGiaiThichService iQttBHXHChiTietGiaiThichService,
            IExportService exportService,
            INsDonViService donViService,
            INsNguoiDungDonViService nguoiDungDonViService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _explainService = iQttBHXHChiTietGiaiThichService;
            _exportService = exportService;
            _donViService = donViService;
            _nguoiDungDonViService = nguoiDungDonViService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
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
            //DataInterpretationCommand = new RelayCommand(obj => OnOpenDataInterpretationDialog());
            //VerbalExplanationCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            _agencies = new ObservableCollection<AgencyModel>();
            IsSummary = false;
            IsSummaryAgency = false;
            IsDataInterpretation = false;
            ResetCondition();
            LoadTieuDe();
            LoadReportType();
            LoadQuarterYears();
            LoadDanhMuc();
            LoadAgencies();
            LoadTypeChuKy();
            LoaiThu = LoaiThu.All;
            IsShowAll = _sessionInfo.YearOfBudget == 1 || _sessionInfo.YearOfBudget == 4;
        }

        private void LoadAgencies()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    var lstIdDonVi = GetListIdDonVi();
                    IsLoading = true;
                    List<DonVi> agencies = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        agencies = agencies.Where(x => x.Loai != LoaiDonVi.ROOT && lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    }
                    else if (IsEnableCheckBoxSummary && (IsSummaryAgency || (!IsSummaryAgency && !IsSummary)))
                    {
                        agencies = agencies.Where(x => lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    }
                    else
                    {
                        agencies = agencies.Where(x => x.Loai == LoaiDonVi.ROOT && lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    }
                    e.Result = agencies;
                }, (s, e) =>
                {
                    if (e.Result != null)
                    {
                        List<DonVi> agencies = (List<DonVi>)e.Result;
                        _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(agencies);
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
                                OnPropertyChanged(nameof(IsSelectedAllAgency));
                                OnPropertyChanged(nameof(IsExportEnable));
                            }
                        };
                    }
                    OnPropertyChanged(nameof(Agencies));
                    OnPropertyChanged(nameof(IsSelectedAllAgency));
                    OnPropertyChanged(nameof(SelectedAgencyCount));
                    OnPropertyChanged(nameof(IsExportEnable));
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                _agencies = new ObservableCollection<AgencyModel>();
                _listAgency = CollectionViewSource.GetDefaultView(_agencies);
            }
        }

        private List<string> GetListIdDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected?.ValueItem ?? "3");
            var selectedQuarterType = Int32.Parse(QuarterMonthQTTSelected?.HiddenValue ?? "1");
            //List<BhQttBHXHQuery> listChungTuDuocXem = new List<BhQttBHXHQuery>();
            var lstIdDonVi = new List<string>();
            if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY)
            {
                if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                {
                    lstIdDonVi = _chungTuChiTietService.FindChiTietDonViThangQuy(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType);
                    //listChungTuDuocXem = _chungTuService.FindChungTuDonViThangQuy(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType).ToList();
                }
                else
                {
                    if (IsSummary)
                    {
                        lstIdDonVi = _chungTuChiTietService.FindChiTietDonViTongHopThangQuy(yearOfWork, BhxhLoaiChungTu.BhxhChungTuTongHop, _sessionService.Current.Principal, selectedQuarter, selectedQuarterType);
                        //listChungTuDuocXem = _chungTuService.FindChungTuDonViTongHopThangQuy(yearOfWork, BhxhLoaiChungTu.BhxhChungTuTongHop, _sessionService.Current.Principal, selectedQuarter, selectedQuarterType).ToList();
                    }
                    else if (IsSummaryAgency && !IsSummary)
                    {
                        lstIdDonVi = _chungTuChiTietService.FindChiTietDonViThangQuy(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType);
                        //listChungTuDuocXem = _chungTuService.FindChungTuDonViThangQuy(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType).ToList();
                    }
                    else if (!IsSummaryAgency && !IsSummary)
                    {
                        lstIdDonVi = _chungTuChiTietService.FindChiTietDonViThangQuy(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType);
                        //listChungTuDuocXem = _chungTuService.FindChungTuDonViThangQuy(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType).ToList();
                    }
                }
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM)
            {
                if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                {
                    lstIdDonVi = _chungTuChiTietService.FindChiTietDonVi(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType);
                    //listChungTuDuocXem = _chungTuService.FindChungTuDonVi(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType).ToList();
                }
                else
                {
                    if (IsSummary)
                    {
                        lstIdDonVi = _chungTuChiTietService.FindChiTietDonViTongHop(yearOfWork, BhxhLoaiChungTu.BhxhChungTuTongHop, _sessionService.Current.Principal, selectedQuarter, selectedQuarterType);
                        //listChungTuDuocXem = _chungTuService.FindChungTuDonViTongHop(yearOfWork, BhxhLoaiChungTu.BhxhChungTuTongHop, _sessionService.Current.Principal, selectedQuarter, selectedQuarterType).ToList();
                    }
                    else if (IsSummaryAgency && !IsSummary)
                    {
                        lstIdDonVi = _chungTuChiTietService.FindChiTietDonVi(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType);
                        //listChungTuDuocXem = _chungTuService.FindChungTuDonVi(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType).ToList();
                    }
                    else if (!IsSummaryAgency && !IsSummary)
                    {
                        lstIdDonVi = _chungTuChiTietService.FindChiTietDonVi(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType);
                        //listChungTuDuocXem = _chungTuService.FindChungTuDonVi(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType).ToList();
                    }
                }
            }
            else
            {
                lstIdDonVi = _chungTuChiTietService.FindAllDonVi(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType);
                //listChungTuDuocXem = _chungTuService.FindAllChungTuDonVi(yearOfWork, selectedQuarter).ToList();
            }
            //var lstIdDonVi = listChungTuDuocXem.Select(x => x.IIDMaDonVi).Distinct().ToList();
            return lstIdDonVi;
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

        private void ResetCondition()
        {
            _quarterMonthSelected = new ComboboxItem();
            _searchAgencyText = string.Empty;
            _searchBudgetIndexText = string.Empty;
        }

        private void LoadTieuDe()
        {
            LoadTypeChuKy();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
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
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencySummaryDetail.ToString() }
            };
            _selectedReportType = _reportTypes.First();
        }

        private void LoadQuarterYears()
        {
            if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY)
            {
                _quarterMonths = new List<ComboboxItem>();
                _quarterMonths.Add(new ComboboxItem { DisplayItem = "Quý I", ValueItem = "3", HiddenValue = "1" });
                _quarterMonths.Add(new ComboboxItem { DisplayItem = "Quý II", ValueItem = "6", HiddenValue = "1" });
                _quarterMonths.Add(new ComboboxItem { DisplayItem = "Quý III", ValueItem = "9", HiddenValue = "1" });
                _quarterMonths.Add(new ComboboxItem { DisplayItem = "Quý IV", ValueItem = "12", HiddenValue = "1" });
                for (int i = 1; i <= 12; i++)
                {
                    _quarterMonths.Add(new ComboboxItem { DisplayItem = "Tháng " + i, ValueItem = i.ToString(), HiddenValue = "0" });
                }
                QuarterMonthQTTSelected = _quarterMonths.First();
            }
            else
            {
                var currYear = _sessionService.Current.YearOfWork;
                var voucherYears = _chungTuService.GetVoucherYears(currYear);
                _quarterMonths = new List<ComboboxItem>();
                _quarterMonths.Add(new ComboboxItem { DisplayItem = "Năm " + currYear, ValueItem = currYear.ToString(), HiddenValue = "2" });
                if (_quarterMonths.Count > 0)
                {
                    _quarterMonths = _quarterMonths.OrderByDescending(x => x.ValueItem).ToList();
                    QuarterMonthQTTSelected = _quarterMonths?.FirstOrDefault() ?? null;
                }
                else
                {
                    MessageBoxHelper.Warning(Resources.AlertEmptyData);
                    return;
                }
            }

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

        private string GetTemplate()
        {
            string input = "";
            // In quyết toán thu BHXH, BHYT, BHTN theo quý
            if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY && LoaiThu == LoaiThu.All && IsData)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_NOP_ALL_QUY);
            }
            // In quyết toán thu BHXH theo quý
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY && LoaiThu == LoaiThu.BHXH && IsData)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_NOP_BHXH_QUY);
            }
            // In quyết toán thu BHYT theo quý
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY && LoaiThu == LoaiThu.BHYT && IsData)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_NOP_BHYT_QUY);
            }
            // In quyết toán thu BHTN theo quý
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY && LoaiThu == LoaiThu.BHTN && IsData)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_NOP_BHTN_QUY);
            }
            // In quyết toán thu BHXH, BHYT, BHTN theo năm
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM && IsData)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM);
            }
            // In quyết toán thu BHXH
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHXH)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_BHXH);
            }
            // In quyết toán thu BHTN
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHTN)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_BHTN);
            }
            // In quyết toán thu BHYT quân nhân
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHYT_QUAN_NHAN)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_BHYT_QUAN_NHAN);
            }
            // In quyết toán thu mua BHYT người lao đông
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHYT_NLD)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_BHYT_NLD);
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_TONG_HOP_NAM)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_TONG_HOP_NAM);
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_CHI_TONG_HOP)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_TONG_HOP_THU_CHI);
            }
            return Path.Combine(ExportPrefix.PATH_BH_QTT, input + FileExtensionFormats.Xlsx);
        }
        private void OnConfigSign()
        {
            LoadTypeChuKy();
            DmChuKyModel chuKyModel = new DmChuKyModel();

            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            if (_typeChuky == TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY || _typeChuky == TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_QUY || _typeChuky == TypeChuKy.QUYET_TOAN_THU_NOP_BHYT_QUY || _typeChuky == TypeChuKy.QUYET_TOAN_THU_NOP_BHTN_QUY)
            {
                DmChuKyDialogViewModel.HasAddedSign4 = true;
                DmChuKyDialogViewModel.HasAddedSign5 = true;
                DmChuKyDialogViewModel.HasAddedSign6 = true;
            }
            else
            {
                DmChuKyDialogViewModel.HasAddedSign4 = false;
                DmChuKyDialogViewModel.HasAddedSign5 = false;
                DmChuKyDialogViewModel.HasAddedSign6 = false;
            }
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void OnExportFile(ExportType exportType)
        {
            if (Agencies.Where(item => item.Selected).Count() <= 0)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }

            if (QuarterMonthQTTSelected == null)
            {
                MessageBoxHelper.Warning(Resources.ErrorQuarterEmpty);
                return;
            }

            if ((SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY && (IsData || IsVerbalExplanation || IsDataExplanation)
                && (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString()
                || (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && IsSummary)))
                || (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY
                && SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString()))
            {
                ExportQuyetToanThuNopBhxhBhytBhtnQuy(exportType);
            }
            else if ((SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY && (IsData || IsVerbalExplanation || IsDataExplanation)
                && (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString()
                || (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && IsSummaryAgency && !IsSummary)
                || (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && !IsSummaryAgency && !IsSummary)))
                || (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY
                && SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString()))
            {
                ExportQuyetToanThuNopBhxhBhytBhtnTongHopDonViQuy(exportType);
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM && (IsData || IsVerbalExplanation || IsDataExplanation)
                && (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString()
                || (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && IsSummary)))
            {
                ExportQuyetToanThuNopBhxhBhytBhtnNam(exportType);
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM && (IsData || IsVerbalExplanation || IsDataExplanation)
                && (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString()
                || (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && IsSummaryAgency && !IsSummary)
                || (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && !IsSummaryAgency && !IsSummary)))
            {
                ExportQuyetToanThuNopBhxhBhytBhtnTongHopDonViNam(exportType);
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHXH || SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHTN)
            {
                ExportQuyetToanThuBhxhBhtn(exportType);
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_TONG_HOP_NAM)
            {
                ExportQuyetToanTongHopNamBhxhBhytBhtn(exportType);
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_CHI_TONG_HOP)
            {
                ExportQuyetToanTongHopThuChi(exportType);
            }
            else if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryDetail.ToString())
            {
                if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM)
                {
                    if (IsSummary)
                        ExportQuyetToanThuNopBhxhBhytBhtnNam(exportType);
                    else
                        ExportQuyetToanThuNopBhxhBhytBhtnTongHopDonViNam(exportType);
                }
                else
                {
                    ExportQuyetToanThuNopBhxhBhytBhtnTongHopDonViQuy(exportType);
                }
            }
            else
            {
                ExportQuyetToanThuBHYT(exportType);
            }

        }

        private void ExportQuyetToanThuNopBhxhBhytBhtnQuy(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected.ValueItem);
                    var selectedQuarterType = Int32.Parse(QuarterMonthQTTSelected.HiddenValue);

                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    foreach (var dv in lstIdDonVi)
                    {
                        var hasMonthlyData = _chungTuService.HasMonthlyVouchers(yearOfWork, selectedQuarter, selectedQuarterType, IsInLuyKe, dv.Id);
                        bool isTongHop = true;
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString() && !IsSummary)
                        {
                            isTongHop = false;
                        }
                        List<BhQttBHXHChiTietQuery> lstData = new List<BhQttBHXHChiTietQuery>();
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString() ||
                        (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && !IsSummary) ||
                        (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && !IsSummaryAgency && !IsSummary))
                        {
                            isTongHop = false;
                            if (hasMonthlyData)
                            {
                                lstData = _chungTuChiTietService.ExportQuyetToanThuNopBhxhBhytBhtnThang(yearOfWork, dv.Id, donViTinh, selectedQuarter, 0, IsInLuyKe).ToList();
                            }
                            else
                            {
                                lstData = _chungTuChiTietService.ExportQuyetToanThuNopBhxhBhytBhtnQuy(yearOfWork, dv.Id, donViTinh, isTongHop, selectedQuarter, selectedQuarterType, IsInLuyKe).ToList();
                            }
                        }
                        else
                        {
                            if (hasMonthlyData)
                            {
                                lstData = _chungTuChiTietService.ExportQTTNopBhxhBhytBhtnTongHopThang(yearOfWork, dv.Id, donViTinh, selectedQuarter, 0, IsInLuyKe).ToList();
                            }
                            else
                            {
                                lstData = _chungTuChiTietService.ExportQTTNopBhxhBhytBhtnTongHopQuy(yearOfWork, dv.Id, donViTinh, isTongHop, selectedQuarter, selectedQuarterType, IsInLuyKe).ToList();
                            }
                        }
                        if (lstData.Any())
                        {
                            lstData.RemoveAt(0);

                            // Quý
                            if (selectedQuarterType == 1)
                            {
                                if (IsInLuyKe && hasMonthlyData)
                                {
                                    var numOfMonthVoucher = _chungTuService.GetNumOfMonthlyVoucher(yearOfWork, dv.Id, IsInLuyKe, selectedQuarter, 1);
                                    foreach (var item in lstData.Where(x => x.HasDataToPrint && !x.BHangCha))
                                    {
                                        item.IQSBQNam = (int?)Math.Round(Convert.ToDouble(item.IQSBQNam) / numOfMonthVoucher, MidpointRounding.AwayFromZero);
                                    }
                                }
                                else if (IsInLuyKe && !hasMonthlyData)
                                {
                                    var divideQuarter = selectedQuarter / 3;
                                    foreach (var item in lstData.Where(x => x.HasDataToPrint && !x.BHangCha))
                                    {
                                        item.IQSBQNam = (int?)Math.Round(Convert.ToDouble(item.IQSBQNam) / divideQuarter, MidpointRounding.AwayFromZero);
                                    }
                                }
                                else if (!IsInLuyKe && hasMonthlyData)
                                {
                                    var numOfMonthVoucher = _chungTuService.GetNumOfMonthlyVoucher(yearOfWork, dv.Id, IsInLuyKe, selectedQuarter, 1);
                                    foreach (var item in lstData.Where(x => x.HasDataToPrint && !x.BHangCha))
                                    {
                                        item.IQSBQNam = (int?)Math.Round(Convert.ToDouble(item.IQSBQNam) / numOfMonthVoucher, MidpointRounding.AwayFromZero);
                                    }
                                }
                                else if (!IsInLuyKe && !hasMonthlyData)
                                {
                                    foreach (var item in lstData.Where(x => x.HasDataToPrint && !x.BHangCha))
                                    {
                                        item.IQSBQNam = (int?)Math.Round(Convert.ToDouble(item.IQSBQNam));
                                    }
                                }
                            }
                            // Thang
                            else
                            {
                                if (IsInLuyKe)
                                {
                                    foreach (var item in lstData.Where(x => x.HasDataToPrint && !x.BHangCha))
                                    {
                                        item.IQSBQNam = (int?)Math.Round(Convert.ToDouble(item.IQSBQNam) / selectedQuarter);
                                    }
                                }
                            }
                        }

                        if (IsData)
                        {
                            CalculateData(lstData);
                            lstData.ForEach(item =>
                            {
                                foreach (var prop in item.GetType().GetProperties())
                                {
                                    if (prop.Name.StartsWith("F")
                                    && prop.PropertyType.IsGenericType
                                    && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                                    && prop.PropertyType.GetGenericArguments()[0].Name == "Double")
                                    {
                                        if (prop.CanWrite) prop.SetValue(item, Math.Round(Convert.ToDouble(prop.GetValue(item, null))));
                                    }
                                    else if (prop.Name.StartsWith("F") && prop.PropertyType.Name == "Double")
                                    {
                                        if (prop.CanWrite) prop.SetValue(item, Math.Round(Convert.ToDouble(prop.GetValue(item, null))));
                                    }
                                }
                            });
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                            data.Add("ListData", lstData.Where(x => x.HasDataToPrint));
                            data.Add("YearWork", yearOfWork);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("DiaDiem", string.Empty);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Year", lstData.First().INamLamViec);
                            data.Add("Quarter", QuarterMonthQTTSelected.DisplayItem);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            data.Add("DonViIn", dv.TenDonVi);
                            data.Add("DonVi", _sessionInfo.TenDonVi);
                            data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                            data.Add("Title1", Title1);
                            data.Add("Title2", Title2);
                            data.Add("Title3", Title3);
                            if (isTongHop)
                                data.Add("IsAggregate", true);
                            else
                                data.Add("IsAggregate", false);
                            //Tính tổng cộng
                            data.Add("TongQSBQNam", lstData.Where(n => !n.BHangCha).Sum(x => x.IQSBQNam));
                            data.Add("TongLuongChinh", lstData.Where(n => !n.BHangCha).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVu", lstData.Where(n => !n.BHangCha).Sum(x => x.FPhuCapChucVu));
                            data.Add("TongPCTNNghe", lstData.Where(n => !n.BHangCha).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhung", lstData.Where(n => !n.BHangCha).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOm", lstData.Where(n => !n.BHangCha).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBL", lstData.Where(n => !n.BHangCha).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNam", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongQuyTienLuongNam));
                            data.Add("TongFThuBHXHNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHXHNLD));
                            data.Add("TongFThuBHXHNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXH", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHXH));
                            data.Add("TongFThuBHYTNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHYTNLD));
                            data.Add("TongFThuBHYTNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYT", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHYT));
                            data.Add("TongFThuBHTNNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHTNNLD));
                            data.Add("TongFThuBHTNNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTN", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHTN));
                            data.Add("FTongCong", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongCong));
                            data.Add("TongDaQuyetToan", lstData.Where(n => !n.BHangCha).Sum(x => x.FDaQuyetToan));
                            data.Add("TongNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongNLD));
                            data.Add("TongNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongNSD));
                            AddChuKy(data, _typeChuky);
                            string templateFileName;
                            templateFileName = GetTemplate();
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                            var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery>(templateFileName, data);
                            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN THU NỘP BHXH, BHYT, BHTN QUÝ " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }
                        if (IsVerbalExplanation)
                        {
                            var loaiThu = LoaiThu == LoaiThu.All ? "ALL" : LoaiThu == LoaiThu.BHXH ? CollectTypeDisplay.BHXH : LoaiThu == LoaiThu.BHYT ? CollectTypeDisplay.BHYT : CollectTypeDisplay.BHTN;
                            var dataGTBL = _explainService.ExportGiaiThichBangLoi(yearOfWork, selectedQuarter, selectedQuarterType, dv.Id, loaiThu);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                            data.Add("YearWork", yearOfWork);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("DiaDiem", string.Empty);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Quarter", QuarterMonthQTTSelected.DisplayItem);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            data.Add("DonVi", _sessionInfo.TenDonVi);
                            data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                            data.Add("Title1", Title1);
                            data.Add("Title2", Title2);
                            data.Add("Title3", Title3);

                            if (LoaiThu == LoaiThu.All)
                            {
                                data.Add("TongCong", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongCong));
                                data.Add("LoaiThu", "BHXH, BHYT, BHTN");
                            }
                            else if (LoaiThu == LoaiThu.BHXH)
                            {
                                data.Add("TongCong", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHXH));
                                data.Add("LoaiThu", "BHXH");
                            }
                            else if (LoaiThu == LoaiThu.BHYT)
                            {
                                data.Add("TongCong", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHYT));
                                data.Add("LoaiThu", "BHYT");
                            }
                            else if (LoaiThu == LoaiThu.BHTN)
                            {
                                data.Add("TongCong", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHTN));
                                data.Add("LoaiThu", "BHTN");
                            }

                            data.Add("FPhaiNopTrongQuyNam", dataGTBL?.FPhaiNopTrongQuyNam ?? 0);
                            data.Add("FTruyThuQuyNamTruoc", dataGTBL?.FTruyThuQuyNamTruoc ?? 0);
                            data.Add("FPhaiNopQuyNamTruoc", dataGTBL?.FPhaiNopQuyNamTruoc ?? 0);
                            data.Add("FPhaiNop", dataGTBL != null ? dataGTBL.FPhaiNopTrongQuyNam.GetValueOrDefault() + dataGTBL.FTruyThuQuyNamTruoc.GetValueOrDefault() + dataGTBL.FPhaiNopQuyNamTruoc.GetValueOrDefault() : 0);
                            data.Add("FDaNopTrongQuyNam", dataGTBL?.FDaNopTrongQuyNam ?? 0);
                            data.Add("FConPhaiNopTiep", dataGTBL?.FConPhaiNopTiep ?? 0);
                            data.Add("SKienNghi", dataGTBL?.SKienNghi ?? null);
                            AddChuKy(data, _typeChuky);
                            var templateFileName = Path.Combine(Path.Combine(ExportPrefix.PATH_BH_QTT, ExportFileName.RPT_BH_GIAI_THICH_LOI_QUY));
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + dv.AgencyName);
                            var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery>(templateFileName, data);
                            results.Add(new ExportResult("GIẢI THÍCH BẰNG LỜI QUÝ " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }

                        if (IsDataExplanation)
                        {
                            List<BhQttBHXHChiTietGiaiThichQuery> dataGTSL = new List<BhQttBHXHChiTietGiaiThichQuery>();
                            var hasMonthlyExplains = _explainService.HasMonthlyExplains(yearOfWork, selectedQuarter, selectedQuarterType, IsInLuyKe, dv.Id);
                            if (!hasMonthlyExplains && (selectedQuarterType == 0 || selectedQuarterType == 1))
                            {
                                dataGTSL = _explainService.ExportGiaiThichSoLieuThangQuy(yearOfWork, selectedQuarter, selectedQuarterType, dv.Id, donViTinh, IsInLuyKe).ToList();
                            }
                            else if (hasMonthlyExplains && selectedQuarterType == 1)
                            {
                                dataGTSL = _explainService.ExportGiaiThichSoLieuThang(yearOfWork, selectedQuarter, selectedQuarterType, dv.Id, donViTinh, IsInLuyKe).ToList();
                            }
                            CalculateDataExplain(dataGTSL);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                            data.Add("ListGTSL", dataGTSL.Where(x => x.HasDataToPrint));
                            data.Add("YearWork", yearOfWork);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("DiaDiem", string.Empty);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Quarter", QuarterMonthQTTSelected.DisplayItem);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            data.Add("DonVi", _sessionInfo.TenDonVi);
                            data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                            data.Add("DonViIn", dv.TenDonVi);
                            data.Add("Title1", Title1);
                            data.Add("Year", lstData.First().INamLamViec);
                            data.Add("TongLuongChinh", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVu", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FPCChucVu));
                            data.Add("TongPCTNNghe", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhung", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOm", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBL", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNam", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTongQuyLuong));
                            data.Add("TongFThuBHXHNLD", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHXHNLD));
                            data.Add("TongFThuBHXHNSD", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXH", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHXHTongCong));
                            data.Add("TongFThuBHYTNLD", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHYTNLD));
                            data.Add("TongFThuBHYTNSD", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYT", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHYTTongCong));
                            data.Add("TongFThuBHTNNLD", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHTNNLD));
                            data.Add("TongFThuBHTNNSD", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTN", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHTNTongCong));
                            data.Add("FTongCong", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTongTruyThuBHXH));
                            if (isTongHop)
                                data.Add("IsAggregate", true);
                            else
                                data.Add("IsAggregate", false);
                            if (LoaiThu == LoaiThu.BHXH)
                                data.Add("FTongCongChu", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHXHTongCong));
                            else if (LoaiThu == LoaiThu.BHYT)
                                data.Add("FTongCongChu", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHYTTongCong));
                            else if (LoaiThu == LoaiThu.BHTN)
                                data.Add("FTongCongChu", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHTNTongCong));
                            else
                                data.Add("FTongCongChu", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTongTruyThuBHXH));
                            AddChuKy(data, _typeChuky);
                            var templateFileName = Path.Combine(Path.Combine(ExportPrefix.PATH_BH_QTT, ExportFileName.RPT_BH_GIAI_SO_LIEU_THANG_QUY));
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + dv.AgencyName);
                            var xlsFile = _exportService.Export<BhQttBHXHChiTietGiaiThichQuery>(templateFileName, data);
                            
                            if (LoaiThu == LoaiThu.BHXH)
                            {
                                xlsFile.SetColHidden(12, true);
                                xlsFile.SetColHidden(13, true);
                                xlsFile.SetColHidden(14, true);
                                xlsFile.SetColHidden(15, true);
                                xlsFile.SetColHidden(16, true);
                                xlsFile.SetColHidden(17, true);
                                xlsFile.SetColHidden(18, true);
                            }
                            else if (LoaiThu == LoaiThu.BHYT)
                            {
                                xlsFile.SetColHidden(9, true);
                                xlsFile.SetColHidden(10, true);
                                xlsFile.SetColHidden(11, true);
                                xlsFile.SetColHidden(15, true);
                                xlsFile.SetColHidden(16, true);
                                xlsFile.SetColHidden(17, true);
                                xlsFile.SetColHidden(18, true);
                            }
                            else if (LoaiThu == LoaiThu.BHTN)
                            {
                                xlsFile.SetColHidden(9, true);
                                xlsFile.SetColHidden(10, true);
                                xlsFile.SetColHidden(11, true);
                                xlsFile.SetColHidden(12, true);
                                xlsFile.SetColHidden(13, true);
                                xlsFile.SetColHidden(14, true);
                                xlsFile.SetColHidden(18, true);
                            }
                            
                            results.Add(new ExportResult("GIẢI THÍCH SỐ LIỆU QUÝ " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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

        private void ExportQuyetToanThuNopBhxhBhytBhtnTongHopDonViQuy(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected.ValueItem);
                    var selectedQuarterType = Int32.Parse(QuarterMonthQTTSelected.HiddenValue);
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    if (lstIdDonVi != null)
                    {
                        var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.Id.ToString()).ToList());
                        var hasMonthlyData = _chungTuService.HasMonthlyVouchers(yearOfWork, selectedQuarter, selectedQuarterType, IsInLuyKe, selectedUnits);
                        List<ExportResult> results = new List<ExportResult>();
                        List<BhQttBHXHChiTietQuery> lstData = new List<BhQttBHXHChiTietQuery>();
                        if (_selectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryDetail.ToString())
                        {
                            if (hasMonthlyData)
                            {
                                lstData = _chungTuChiTietService.ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViThang(yearOfWork, selectedUnits, donViTinh, selectedQuarter, 0, IsSummary, IsInLuyKe).ToList();
                            }
                            else
                            {
                                lstData = _chungTuChiTietService.ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViQuy(yearOfWork, selectedUnits, donViTinh, selectedQuarter, selectedQuarterType, IsInLuyKe, IsSummary).ToList();
                            }
                        }
                        else
                        {
                            if (hasMonthlyData)
                            {
                                lstData = _chungTuChiTietService.ExportQTTNopBhxhBhytBhtnTongHopDonViThang(yearOfWork, selectedUnits, donViTinh, selectedQuarter, 0, IsInLuyKe).ToList();
                            }
                            else
                            {
                                lstData = _chungTuChiTietService.ExportQTTNopBhxhBhytBhtnTongHopDonViQuy(yearOfWork, selectedUnits, donViTinh, selectedQuarter, selectedQuarterType, IsInLuyKe).ToList();
                            }
                        }
                        if (lstData.Any())
                        {
                            lstData.RemoveAt(0);

                            // Quý
                            if (selectedQuarterType == 1)
                            {
                                if (IsInLuyKe && hasMonthlyData)
                                {
                                    var numOfMonthVoucher = _chungTuService.GetNumOfMonthlyVoucher(yearOfWork, selectedUnits, IsInLuyKe, selectedQuarter, 1);
                                    foreach (var item in lstData.Where(x => x.HasDataToPrint && !x.BHangCha))
                                    {
                                        item.IQSBQNam = (int?)Math.Round(Convert.ToDouble(item.IQSBQNam) / numOfMonthVoucher, MidpointRounding.AwayFromZero);
                                    }
                                }
                                else if (IsInLuyKe && !hasMonthlyData)
                                {
                                    var divideQuarter = selectedQuarter / 3;
                                    foreach (var item in lstData.Where(x => x.HasDataToPrint && !x.BHangCha))
                                    {
                                        item.IQSBQNam = (int?)Math.Round(Convert.ToDouble(item.IQSBQNam) / divideQuarter, MidpointRounding.AwayFromZero);
                                    }
                                }
                                else if (!IsInLuyKe && hasMonthlyData)
                                {
                                    var numOfMonthVoucher = _chungTuService.GetNumOfMonthlyVoucher(yearOfWork, selectedUnits, IsInLuyKe, selectedQuarter, 1);
                                    foreach (var item in lstData.Where(x => x.HasDataToPrint && !x.BHangCha))
                                    {
                                        item.IQSBQNam = (int?)Math.Round(Convert.ToDouble(item.IQSBQNam) / numOfMonthVoucher, MidpointRounding.AwayFromZero);
                                    }
                                }
                                else if (!IsInLuyKe && !hasMonthlyData)
                                {
                                    foreach (var item in lstData.Where(x => x.HasDataToPrint && !x.BHangCha))
                                    {
                                        item.IQSBQNam = (int?)Math.Round(Convert.ToDouble(item.IQSBQNam));
                                    }
                                }
                            }
                            // Thang
                            else
                            {
                                if (IsInLuyKe)
                                {
                                    foreach (var item in lstData.Where(x => x.HasDataToPrint && !x.BHangCha))
                                    {
                                        item.IQSBQNam = (int?)Math.Round(Convert.ToDouble(item.IQSBQNam) / selectedQuarter);
                                    }
                                }
                            }
                        }

                        if (IsData)
                        {
                            CalculateData(lstData);
                            if ((_selectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryDetail.ToString() && selectedQuarterType == 0)
                            || (_selectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryDetail.ToString() && !IsSummary && selectedQuarterType == 1))
                            {
                                foreach (var item in lstData)
                                {
                                    item.BHangCha = !item.IIDMLNS.Equals(Guid.Empty) ? true : item.BHangCha;
                                }
                            }

                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                            data.Add("ListData", lstData.Where(x => x.HasDataToPrint));
                            data.Add("YearWork", yearOfWork);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("DiaDiem", string.Empty);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Year", lstData.Any() ? lstData.First().INamLamViec : yearOfWork);
                            data.Add("Quarter", QuarterMonthQTTSelected.DisplayItem);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            data.Add("DonVi", _sessionInfo.TenDonVi);
                            data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                            data.Add("Title1", Title1);
                            data.Add("Title2", Title2);
                            data.Add("Title3", Title3);
                            data.Add("DonViIn", _sessionInfo.TenDonVi);
                            data.Add("IsAggregate", true);
                            //Tính tổng cộng
                            if (IsSummary)
                            {
                                data.Add("TongQSBQNam", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.IQSBQNam));
                                data.Add("TongLuongChinh", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FLuongChinh));
                                data.Add("TongPhuCapChucVu", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FPhuCapChucVu));
                                data.Add("TongPCTNNghe", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FPCTNNghe));
                                data.Add("TongFPCTNVuotKhung", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FPCTNVuotKhung));
                                data.Add("TongFNghiOm", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FNghiOm));
                                data.Add("TongFHSBL", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FHSBL));
                                data.Add("TongFTongQuyTienLuongNam", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FTongQuyTienLuongNam));
                                data.Add("TongFThuBHXHNLD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FThuBHXHNLD));
                                data.Add("TongFThuBHXHNSD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FThuBHXHNSD));
                                data.Add("TongFTongSoPhaiThuBHXH", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FTongSoPhaiThuBHXH));
                                data.Add("TongFThuBHYTNLD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FThuBHYTNLD));
                                data.Add("TongFThuBHYTNSD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FThuBHYTNSD));
                                data.Add("TongFTongSoPhaiThuBHYT", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FTongSoPhaiThuBHYT));
                                data.Add("TongFThuBHTNNLD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FThuBHTNNLD));
                                data.Add("TongFThuBHTNNSD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FThuBHTNNSD));
                                data.Add("TongFTongSoPhaiThuBHTN", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FTongSoPhaiThuBHTN));
                                data.Add("FTongCong", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FTongCong));
                                data.Add("TongDaQuyetToan", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FDaQuyetToan));
                                data.Add("TongNLD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FTongNLD));
                                data.Add("TongNSD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FTongNSD));
                            }
                            else
                            {
                                data.Add("TongQSBQNam", lstData.Where(n => !n.BHangCha).Sum(x => x.IQSBQNam));
                                data.Add("TongLuongChinh", lstData.Where(n => !n.BHangCha).Sum(x => x.FLuongChinh));
                                data.Add("TongPhuCapChucVu", lstData.Where(n => !n.BHangCha).Sum(x => x.FPhuCapChucVu));
                                data.Add("TongPCTNNghe", lstData.Where(n => !n.BHangCha).Sum(x => x.FPCTNNghe));
                                data.Add("TongFPCTNVuotKhung", lstData.Where(n => !n.BHangCha).Sum(x => x.FPCTNVuotKhung));
                                data.Add("TongFNghiOm", lstData.Where(n => !n.BHangCha).Sum(x => x.FNghiOm));
                                data.Add("TongFHSBL", lstData.Where(n => !n.BHangCha).Sum(x => x.FHSBL));
                                data.Add("TongFTongQuyTienLuongNam", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongQuyTienLuongNam));
                                data.Add("TongFThuBHXHNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHXHNLD));
                                data.Add("TongFThuBHXHNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHXHNSD));
                                data.Add("TongFTongSoPhaiThuBHXH", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHXH));
                                data.Add("TongFThuBHYTNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHYTNLD));
                                data.Add("TongFThuBHYTNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHYTNSD));
                                data.Add("TongFTongSoPhaiThuBHYT", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHYT));
                                data.Add("TongFThuBHTNNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHTNNLD));
                                data.Add("TongFThuBHTNNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHTNNSD));
                                data.Add("TongFTongSoPhaiThuBHTN", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHTN));
                                data.Add("FTongCong", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongCong));
                                data.Add("TongDaQuyetToan", lstData.Where(n => !n.BHangCha).Sum(x => x.FDaQuyetToan));
                                data.Add("TongNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongNLD));
                                data.Add("TongNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongNSD));
                            }
                            //Tính tổng cộng Quân nhân
                            data.Add("TongQSBQNamQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.IQSBQNam));
                            data.Add("TongLuongChinhQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVuQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FPhuCapChucVu));
                            data.Add("TongPCTNNgheQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhungQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOmQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBLQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNamQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongQuyTienLuongNam));
                            data.Add("TongFThuBHXHNLDQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHXHNLD));
                            data.Add("TongFThuBHXHNSDQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXHQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHXH));
                            data.Add("TongFThuBHYTNLDQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHYTNLD));
                            data.Add("TongFThuBHYTNSDQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYTQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHYT));
                            data.Add("TongFThuBHTNNLDQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHTNNLD));
                            data.Add("TongFThuBHTNNSDQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTNQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHTN));
                            data.Add("FTongCongQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHXH) +
                                lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHYT) +
                                lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHTN));
                            //Tính tổng cộng Người lao động
                            data.Add("TongQSBQNamNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.IQSBQNam));
                            data.Add("TongLuongChinhNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVuNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FPhuCapChucVu));
                            data.Add("TongPCTNNgheNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhungNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOmNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBLNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNamNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongQuyTienLuongNam));
                            data.Add("TongFThuBHXHNLDNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHXHNLD));
                            data.Add("TongFThuBHXHNSDNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXHNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHXH));
                            data.Add("TongFThuBHYTNLDNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHYTNLD));
                            data.Add("TongFThuBHYTNSDNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYTNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHYT));
                            data.Add("TongFThuBHTNNLDNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHTNNLD));
                            data.Add("TongFThuBHTNNSDNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTNNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHTN));
                            data.Add("FTongCongNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHXH) +
                                lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHYT) +
                                lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHTN));
                            AddChuKy(data, _typeChuky);
                            string templateFileName;
                            templateFileName = GetTemplate();
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                            var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery>(templateFileName, data);
                            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN THU NỘP BHXH, BHYT, BHTN QUÝ " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }
                        if (IsVerbalExplanation)
                        {
                            var loaiThu = LoaiThu == LoaiThu.All ? "ALL" : LoaiThu == LoaiThu.BHXH ? CollectTypeDisplay.BHXH : LoaiThu == LoaiThu.BHYT ? CollectTypeDisplay.BHYT : CollectTypeDisplay.BHTN;
                            var dataGTBL = _explainService.ExportGiaiThichBangLoiTongHopDonVi(yearOfWork, selectedQuarter, selectedQuarterType, selectedUnits, loaiThu);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                            data.Add("YearWork", yearOfWork);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("DiaDiem", string.Empty);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Quarter", QuarterMonthQTTSelected.DisplayItem);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            data.Add("DonVi", _sessionInfo.TenDonVi);
                            data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                            data.Add("Title1", Title1);
                            data.Add("Title2", Title2);
                            data.Add("Title3", Title3);

                            if (LoaiThu == LoaiThu.All)
                            {
                                data.Add("TongCong", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongCong));
                                data.Add("LoaiThu", "BHXH, BHYT, BHTN");
                            }
                            else if (LoaiThu == LoaiThu.BHXH)
                            {
                                data.Add("TongCong", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHXH));
                                data.Add("LoaiThu", "BHXH");
                            }
                            else if (LoaiThu == LoaiThu.BHYT)
                            {
                                data.Add("TongCong", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHYT));
                                data.Add("LoaiThu", "BHYT");
                            }
                            else if (LoaiThu == LoaiThu.BHTN)
                            {
                                data.Add("TongCong", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHTN));
                                data.Add("LoaiThu", "BHTN");
                            }

                            data.Add("FPhaiNopTrongQuyNam", dataGTBL?.FPhaiNopTrongQuyNam ?? 0);
                            data.Add("FTruyThuQuyNamTruoc", dataGTBL?.FTruyThuQuyNamTruoc ?? 0);
                            data.Add("FPhaiNopQuyNamTruoc", dataGTBL?.FPhaiNopQuyNamTruoc ?? 0);
                            data.Add("FPhaiNop", dataGTBL != null ? dataGTBL.FPhaiNopTrongQuyNam.GetValueOrDefault() + dataGTBL.FTruyThuQuyNamTruoc.GetValueOrDefault() + dataGTBL.FPhaiNopQuyNamTruoc.GetValueOrDefault() : 0);
                            data.Add("FDaNopTrongQuyNam", dataGTBL?.FDaNopTrongQuyNam ?? 0);
                            data.Add("FConPhaiNopTiep", dataGTBL?.FConPhaiNopTiep ?? 0);
                            data.Add("SKienNghi", dataGTBL?.SKienNghi ?? null);
                            AddChuKy(data, _typeChuky);
                            string templateFileName;
                            templateFileName = Path.Combine(Path.Combine(ExportPrefix.PATH_BH_QTT, ExportFileName.RPT_BH_GIAI_THICH_LOI_QUY));
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                            var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery>(templateFileName, data);
                            results.Add(new ExportResult("GIẢI THÍCH BẰNG LỜI QUÝ " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }

                        if (IsDataExplanation)
                        {
                            List<BhQttBHXHChiTietGiaiThichQuery> dataGTSL = new List<BhQttBHXHChiTietGiaiThichQuery>();
                            var hasMonthlyExplains = _explainService.HasMonthlyExplains(yearOfWork, selectedQuarter, selectedQuarterType, IsInLuyKe, selectedUnits);
                            if (!hasMonthlyExplains && (selectedQuarterType == 0 || selectedQuarterType == 1))
                            {
                                dataGTSL = _explainService.ExportGiaiThichSoLieuThangQuy(yearOfWork, selectedQuarter, selectedQuarterType, selectedUnits, donViTinh, IsInLuyKe).ToList();
                            }
                            else if (hasMonthlyExplains && selectedQuarterType == 1)
                            {
                                dataGTSL = _explainService.ExportGiaiThichSoLieuThang(yearOfWork, selectedQuarter, selectedQuarterType, selectedUnits, donViTinh, IsInLuyKe).ToList();
                            }
                            CalculateDataExplain(dataGTSL);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                            data.Add("ListGTSL", dataGTSL.Where(x => x.HasDataToPrint));
                            data.Add("YearWork", yearOfWork);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("DiaDiem", string.Empty);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Quarter", QuarterMonthQTTSelected.DisplayItem);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            data.Add("DonVi", _sessionInfo.TenDonVi);
                            data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                            data.Add("DonViIn", _sessionInfo.TenDonVi);
                            data.Add("Title1", Title1);
                            data.Add("Year", lstData.First().INamLamViec);
                            data.Add("TongLuongChinh", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVu", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FPCChucVu));
                            data.Add("TongPCTNNghe", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhung", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOm", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBL", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNam", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTongQuyLuong));
                            data.Add("TongFThuBHXHNLD", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHXHNLD));
                            data.Add("TongFThuBHXHNSD", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXH", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHXHTongCong));
                            data.Add("TongFThuBHYTNLD", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHYTNLD));
                            data.Add("TongFThuBHYTNSD", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYT", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHYTTongCong));
                            data.Add("TongFThuBHTNNLD", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHTNNLD));
                            data.Add("TongFThuBHTNNSD", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTN", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHTNTongCong));
                            data.Add("FTongCong", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTongTruyThuBHXH));
                            data.Add("IsAggregate", true);
                            if (LoaiThu == LoaiThu.BHXH)
                                data.Add("FTongCongChu", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHXHTongCong));
                            else if (LoaiThu == LoaiThu.BHYT)
                                data.Add("FTongCongChu", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHYTTongCong));
                            else if (LoaiThu == LoaiThu.BHTN)
                                data.Add("FTongCongChu", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBHTNTongCong));
                            else
                                data.Add("FTongCongChu", dataGTSL.Where(n => !n.BHangCha).Sum(x => x.FTongTruyThuBHXH));
                            AddChuKy(data, _typeChuky);
                            var templateFileName = Path.Combine(Path.Combine(ExportPrefix.PATH_BH_QTT, ExportFileName.RPT_BH_GIAI_SO_LIEU_THANG_QUY));
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                            var xlsFile = _exportService.Export<BhQttBHXHChiTietGiaiThichQuery>(templateFileName, data);

                            if (LoaiThu == LoaiThu.BHXH)
                            {
                                xlsFile.SetColHidden(12, true);
                                xlsFile.SetColHidden(13, true);
                                xlsFile.SetColHidden(14, true);
                                xlsFile.SetColHidden(15, true);
                                xlsFile.SetColHidden(16, true);
                                xlsFile.SetColHidden(17, true);
                                xlsFile.SetColHidden(18, true);
                            }
                            else if (LoaiThu == LoaiThu.BHYT)
                            {
                                xlsFile.SetColHidden(9, true);
                                xlsFile.SetColHidden(10, true);
                                xlsFile.SetColHidden(11, true);
                                xlsFile.SetColHidden(15, true);
                                xlsFile.SetColHidden(16, true);
                                xlsFile.SetColHidden(17, true);
                                xlsFile.SetColHidden(18, true);
                            }
                            else if (LoaiThu == LoaiThu.BHTN)
                            {
                                xlsFile.SetColHidden(9, true);
                                xlsFile.SetColHidden(10, true);
                                xlsFile.SetColHidden(11, true);
                                xlsFile.SetColHidden(12, true);
                                xlsFile.SetColHidden(13, true);
                                xlsFile.SetColHidden(14, true);
                                xlsFile.SetColHidden(18, true);
                            }

                            results.Add(new ExportResult("GIẢI THÍCH SỐ LIỆU QUÝ " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }

                        e.Result = results;
                    }
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

        private void ExportQuyetToanThuNopBhxhBhytBhtnNam(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    foreach (var dv in lstIdDonVi)
                    {
                        bool isTongHop = true;
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                        {
                            isTongHop = false;
                        }
                        List<BhQttBHXHChiTietQuery> lstData = new List<BhQttBHXHChiTietQuery>();
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString() ||
                        (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && !IsSummary))
                        {
                            lstData = _chungTuChiTietService.ExportQuyetToanThuNopBhxhBhytBhtnNam(yearOfWork, dv.Id, donViTinh, isTongHop, selectedQuarter).ToList();
                        }
                        else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM &&
                                IsSummary && SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryDetail.ToString())
                        {
                            lstData = _chungTuChiTietService.ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViNam(yearOfWork, dv.Id, donViTinh, selectedQuarter, IsSummary).ToList();
                        }
                        else
                        {
                            lstData = _chungTuChiTietService.ExportQuyetToanThuNopTongHopNam(yearOfWork, dv.Id, donViTinh, isTongHop, selectedQuarter).ToList();
                        }
                        if (lstData.Any())
                            lstData.RemoveAt(0);

                        if (IsData)
                        {
                            CalculateData(lstData);
                            lstData.ForEach(item =>
                            {
                                foreach (var prop in item.GetType().GetProperties())
                                {
                                    if (prop.Name.StartsWith("F")
                                    && prop.PropertyType.IsGenericType
                                    && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                                    && prop.PropertyType.GetGenericArguments()[0].Name == "Double")
                                    {
                                        if (prop.CanWrite) prop.SetValue(item, Math.Round(Convert.ToDouble(prop.GetValue(item, null))));
                                    }
                                    else if (prop.Name.StartsWith("F") && prop.PropertyType.Name == "Double")
                                    {
                                        if (prop.CanWrite) prop.SetValue(item, Math.Round(Convert.ToDouble(prop.GetValue(item, null))));
                                    }
                                }
                            });
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                            data.Add("ListData", lstData.Where(x => x.HasDataToPrint));
                            data.Add("YearWork", yearOfWork);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("DiaDiem", string.Empty);
                            data.Add("DonViIn", dv.TenDonVi);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Year", lstData.Any() ? lstData.First().INamLamViec : yearOfWork);
                            data.Add("Quarter", QuarterMonthQTTSelected.DisplayItem);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            data.Add("DonVi", _sessionInfo.TenDonVi);
                            data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                            data.Add("Title1", Title1);
                            data.Add("Title2", Title2);
                            data.Add("Title3", Title3);
                            data.Add("TongQSBQNam", lstData.Where(n => !n.BHangCha).Sum(x => x.IQSBQNam));
                            data.Add("TongLuongChinh", lstData.Where(n => !n.BHangCha).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVu", lstData.Where(n => !n.BHangCha).Sum(x => x.FPhuCapChucVu));
                            data.Add("TongPCTNNghe", lstData.Where(n => !n.BHangCha).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhung", lstData.Where(n => !n.BHangCha).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOm", lstData.Where(n => !n.BHangCha).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBL", lstData.Where(n => !n.BHangCha).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNam", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongQuyTienLuongNam));
                            data.Add("TongFThuBHXHNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHXHNLD));
                            data.Add("TongFThuBHXHNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXH", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHXH));
                            data.Add("TongFThuBHYTNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHYTNLD));
                            data.Add("TongFThuBHYTNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYT", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHYT));
                            data.Add("TongFThuBHTNNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHTNNLD));
                            data.Add("TongFThuBHTNNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTN", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHTN));
                            data.Add("FTongCong", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHXH) +
                                lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHYT) +
                                lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHTN));

                            //Tính tổng cộng Quân nhân
                            data.Add("TongQSBQNamQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.IQSBQNam));
                            data.Add("TongLuongChinhQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVuQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FPhuCapChucVu));
                            data.Add("TongPCTNNgheQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhungQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOmQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBLQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNamQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongQuyTienLuongNam));
                            data.Add("TongFThuBHXHNLDQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHXHNLD));
                            data.Add("TongFThuBHXHNSDQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXHQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHXH));
                            data.Add("TongFThuBHYTNLDQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHYTNLD));
                            data.Add("TongFThuBHYTNSDQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYTQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHYT));
                            data.Add("TongFThuBHTNNLDQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHTNNLD));
                            data.Add("TongFThuBHTNNSDQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTNQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHTN));
                            data.Add("FTongCongQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHXH) +
                                lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHYT) +
                                lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHTN));
                            //Tính tổng cộng Người lao động
                            data.Add("TongQSBQNamNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.IQSBQNam));
                            data.Add("TongLuongChinhNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVuNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FPhuCapChucVu));
                            data.Add("TongPCTNNgheNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhungNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOmNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBLNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNamNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongQuyTienLuongNam));
                            data.Add("TongFThuBHXHNLDNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHXHNLD));
                            data.Add("TongFThuBHXHNSDNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXHNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHXH));
                            data.Add("TongFThuBHYTNLDNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHYTNLD));
                            data.Add("TongFThuBHYTNSDNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYTNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHYT));
                            data.Add("TongFThuBHTNNLDNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHTNNLD));
                            data.Add("TongFThuBHTNNSDNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTNNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHTN));
                            data.Add("FTongCongNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHXH)
                                + lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHYT)
                                + lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHTN));
                            var dataTruyThu = lstData.Where(n => n.SXauNoiMa == BhxhLoaiSM.TRUY_THU_DU_TOAN || n.SXauNoiMa == BhxhLoaiSM.TRUY_THU_HACH_TOAN);
                            data.Add("TongTruyThuLuongChinh", dataTruyThu?.Sum(x => x.FLuongChinh) ?? 0);
                            data.Add("TongTruyThuPCCV", dataTruyThu?.Sum(x => x.FPhuCapChucVu) ?? 0);
                            data.Add("TongTruyThuPCTN", dataTruyThu?.Sum(x => x.FPCTNNghe) ?? 0);
                            data.Add("TongTruyThuPCTNVK", dataTruyThu?.Sum(x => x.FPCTNVuotKhung) ?? 0);
                            data.Add("TongTruyThuNghiOm", dataTruyThu?.Sum(x => x.FNghiOm) ?? 0);
                            data.Add("TongTruyThuHSBL", dataTruyThu?.Sum(x => x.FHSBL) ?? 0);
                            data.Add("TongTruyThuQuyLuong", dataTruyThu?.Sum(x => x.FTongQuyTienLuongNam) ?? 0);
                            data.Add("TongTruyThuBHXHNLD", dataTruyThu?.Sum(x => x.FThuBHXHNLD) ?? 0);
                            data.Add("TongTruyThuBHXHNSD", dataTruyThu?.Sum(x => x.FThuBHXHNSD) ?? 0);
                            data.Add("TongTruyThuBHXHTong", dataTruyThu?.Sum(x => x.FTongSoPhaiThuBHXH) ?? 0);
                            data.Add("TongTruyThuBHYTNLD", dataTruyThu?.Sum(x => x.FThuBHYTNLD) ?? 0);
                            data.Add("TongTruyThuBHYTNSD", dataTruyThu?.Sum(x => x.FThuBHYTNSD) ?? 0);
                            data.Add("TongTruyThuBHYTTong", dataTruyThu?.Sum(x => x.FTongSoPhaiThuBHYT) ?? 0);
                            data.Add("TongTruyThuBHTNNLD", dataTruyThu?.Sum(x => x.FThuBHTNNLD) ?? 0);
                            data.Add("TongTruyThuBHTNNSD", dataTruyThu?.Sum(x => x.FThuBHTNNSD) ?? 0);
                            data.Add("TongTruyThuBHTNTong", dataTruyThu?.Sum(x => x.FTongSoPhaiThuBHTN) ?? 0);
                            data.Add("TongTruyThu", (dataTruyThu?.Sum(x => x.FTongSoPhaiThuBHXH) ?? 0)
                                + (dataTruyThu?.Sum(x => x.FTongSoPhaiThuBHYT) ?? 0)
                                + (dataTruyThu?.Sum(x => x.FTongSoPhaiThuBHTN) ?? 0));

                            AddChuKy(data, _typeChuky);
                            string templateFileName;
                            templateFileName = GetTemplate();
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + dv.TenDonVi);
                            var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery, BhQttBHXHChiTietGiaiThichQuery>(templateFileName, data);
                            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN THU NỘP BHXH, BHYT, BHTN NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }
                        if (IsDataExplanation)
                        {
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            var lstGiaiThichTruyThu = _explainService.ExportGiaiThichTruyThu(yearOfWork, dv.Id).ToList();
                            CalculateDataTruyThu(lstGiaiThichTruyThu);
                            var lstGiaiThichSosanh = _explainService.ExportGiaiThichTongHopSoSanh(yearOfWork, dv.Id)
                            .Where(x => x.ILoaiGiaiThich == (int)ExplainType.GIAITHICH_TONGHOP_SOSANH).OrderBy(n => n.ISTT).ToList();
                            var lstGiaiThichGiamDong = _explainService.ExportGiaiThichGiamDong(yearOfWork, dv.Id)
                            .Where(x => x.ILoaiGiaiThich == (int)ExplainType.GIAITHICH_GIAMDONG).OrderBy(n => n.ISTT).ToList();
                            data.Add("YearWork", yearOfWork);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("DiaDiem", string.Empty);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Quarter", QuarterMonthQTTSelected.DisplayItem);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            data.Add("DonVi", dv.TenDonVi);
                            data.Add("Title1", Title1);
                            data.Add("Title2", Title2);
                            data.Add("Title3", Title3);
                            data.Add("ListTruyThu", lstGiaiThichTruyThu);
                            data.Add("ListSoSanh", lstGiaiThichSosanh);
                            data.Add("ListGiamDong", lstGiaiThichGiamDong);
                            //Tính tổng cộng
                            data.Add("FTruyThuBhxhNldDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhxhNldDT));
                            data.Add("FTruyThuBhxhNsdDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhxhNsdDT));
                            data.Add("FTruyThuBhxhTongCongDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhxhTongCongDT));
                            data.Add("FTruyThuBhytNldDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhytNldDT));
                            data.Add("FTruyThuBhytNsdDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhytNsdDT));
                            data.Add("FTruyThuBhytTongCongDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhytTongCongDT));
                            data.Add("FTruyThuBhtnNldDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhtnNldDT));
                            data.Add("FTruyThuBhtnNsdDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhtnNsdDT));
                            data.Add("FTruyThuBhtnTongCongDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhtnTongCongDT));
                            data.Add("FTruyThuBhxhNldHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhxhNldHT));
                            data.Add("FTruyThuBhxhNsdHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhxhNsdHT));
                            data.Add("FTruyThuBhxhTongCongHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhxhTongCongHT));
                            data.Add("FTruyThuBhytNldHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhytNldHT));
                            data.Add("FTruyThuBhytNsdHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhytNsdHT));
                            data.Add("FTruyThuBhytTongCongHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhytTongCongHT));
                            data.Add("FTruyThuBhtnNldHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhtnNldHT));
                            data.Add("FTruyThuBhtnNsdHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhtnNsdHT));
                            data.Add("FTruyThuBhtnTongCongHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhtnTongCongHT));
                            data.Add("FTongCongTruyThuBHXHTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTongCongTruyThuBHXH));
                            data.Add("FTongCongTruyThuBHYTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTongCongTruyThuBHYT));
                            data.Add("FTongCongTruyThuBHTNTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTongCongTruyThuBHTN));
                            data.Add("FTongTruyThuTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTongTruyThu));
                            data.Add("FTongSoPhaiThuNop", lstGiaiThichSosanh.Sum(x => x.FSoPhaiThuNop));
                            data.Add("FTongSoDaNopTrongNam", lstGiaiThichSosanh.Sum(x => x.FSoDaNopTrongNam));
                            data.Add("FTongSoDaNopSau3112", lstGiaiThichSosanh.Sum(x => x.FSoDaNopSau3112));
                            data.Add("FTongSoDaNop", lstGiaiThichSosanh.Sum(x => x.FTongSoDaNop));
                            data.Add("FTongSoConPhaiNop", lstGiaiThichSosanh.Sum(x => x.FSoConPhaiNop));
                            data.Add("ITongQuanSo", lstGiaiThichGiamDong.Sum(x => x.IQuanSo));
                            data.Add("FTongQuyTienLuongCanCu", lstGiaiThichGiamDong.Sum(x => x.FQuyTienLuongCanCu));
                            data.Add("FTongSoTienGiamDong", lstGiaiThichGiamDong.Sum(x => x.FSoTienGiamDong));
                            AddChuKy(data, _typeChuky);
                            string templateFileName;
                            templateFileName = Path.Combine(Path.Combine(ExportPrefix.PATH_BH_QTT, ExportFileName.RPT_BH_GIAI_THICH_SO_LIEU_NAM));
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + dv.TenDonVi);
                            var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery, BhQttBHXHChiTietGiaiThichQuery>(templateFileName, data);
                            results.Add(new ExportResult("GIẢI THÍCH SỐ LIỆU NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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

        private void ExportQuyetToanThuNopBhxhBhytBhtnTongHopDonViNam(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    if (lstIdDonVi != null)
                    {
                        var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.Id.ToString()).ToList());
                        List<BhQttBHXHChiTietQuery> lstData = new List<BhQttBHXHChiTietQuery>();
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryDetail.ToString())
                        {
                            lstData = _chungTuChiTietService.ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViNam(yearOfWork, selectedUnits, donViTinh, selectedQuarter).ToList();
                        }
                        else
                        {
                            lstData = _chungTuChiTietService.ExportQTTNopBhxhBhytBhtnTongHopDonViNam(yearOfWork, selectedUnits, donViTinh, selectedQuarter).ToList();
                        }
                        if (lstData.Any())
                            lstData.RemoveAt(0);

                        if (IsData)
                        {
                            CalculateData(lstData);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                            data.Add("ListData", lstData.Where(x => x.HasDataToPrint));
                            data.Add("YearWork", yearOfWork);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("DiaDiem", string.Empty);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Year", lstData.Any() ? lstData.First().INamLamViec : yearOfWork);
                            data.Add("Quarter", QuarterMonthQTTSelected.DisplayItem);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            data.Add("DonVi", _sessionInfo.TenDonVi);
                            data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                            data.Add("DonViIn", _sessionInfo.TenDonVi);
                            data.Add("Title1", Title1);
                            data.Add("Title2", Title2);
                            data.Add("Title3", Title3);
                            data.Add("TongQSBQNam", lstData.Where(n => !n.BHangCha).Sum(x => x.IQSBQNam));
                            data.Add("TongLuongChinh", lstData.Where(n => !n.BHangCha).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVu", lstData.Where(n => !n.BHangCha).Sum(x => x.FPhuCapChucVu));
                            data.Add("TongPCTNNghe", lstData.Where(n => !n.BHangCha).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhung", lstData.Where(n => !n.BHangCha).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOm", lstData.Where(n => !n.BHangCha).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBL", lstData.Where(n => !n.BHangCha).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNam", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongQuyTienLuongNam));
                            data.Add("TongFThuBHXHNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHXHNLD));
                            data.Add("TongFThuBHXHNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXH", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHXH));
                            data.Add("TongFThuBHYTNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHYTNLD));
                            data.Add("TongFThuBHYTNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYT", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHYT));
                            data.Add("TongFThuBHTNNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHTNNLD));
                            data.Add("TongFThuBHTNNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTN", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHTN));
                            data.Add("FTongCong", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHXH) +
                                lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHYT) +
                                lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHTN));
                            //Tính tổng cộng Quân nhân
                            data.Add("TongQSBQNamQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.IQSBQNam));
                            data.Add("TongLuongChinhQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVuQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FPhuCapChucVu));
                            data.Add("TongPCTNNgheQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhungQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOmQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBLQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNamQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongQuyTienLuongNam));
                            data.Add("TongFThuBHXHNLDQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHXHNLD));
                            data.Add("TongFThuBHXHNSDQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXHQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHXH));
                            data.Add("TongFThuBHYTNLDQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHYTNLD));
                            data.Add("TongFThuBHYTNSDQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYTQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHYT));
                            data.Add("TongFThuBHTNNLDQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHTNNLD));
                            data.Add("TongFThuBHTNNSDQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTNQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHTN));
                            data.Add("FTongCongQuanNhan", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHXH) +
                                lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHYT) +
                                lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHTN));
                            //Tính tổng cộng Người lao động
                            data.Add("TongQSBQNamNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.IQSBQNam));
                            data.Add("TongLuongChinhNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVuNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FPhuCapChucVu));
                            data.Add("TongPCTNNgheNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhungNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOmNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBLNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNamNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongQuyTienLuongNam));
                            data.Add("TongFThuBHXHNLDNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHXHNLD));
                            data.Add("TongFThuBHXHNSDNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXHNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHXH));
                            data.Add("TongFThuBHYTNLDNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHYTNLD));
                            data.Add("TongFThuBHYTNSDNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYTNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHYT));
                            data.Add("TongFThuBHTNNLDNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHTNNLD));
                            data.Add("TongFThuBHTNNSDNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTNNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHTN));
                            data.Add("FTongCongNLD", lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHXH)
                                + lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHYT)
                                + lstData.Where(n => !n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHTN));

                            var dataTruyThu = lstData.Where(n => n.SXauNoiMa == BhxhLoaiSM.TRUY_THU_DU_TOAN || n.SXauNoiMa == BhxhLoaiSM.TRUY_THU_HACH_TOAN);
                            data.Add("TongTruyThuLuongChinh", dataTruyThu?.Sum(x => x.FLuongChinh) ?? 0);
                            data.Add("TongTruyThuPCCV", dataTruyThu?.Sum(x => x.FPhuCapChucVu) ?? 0);
                            data.Add("TongTruyThuPCTN", dataTruyThu?.Sum(x => x.FPCTNNghe) ?? 0);
                            data.Add("TongTruyThuPCTNVK", dataTruyThu?.Sum(x => x.FPCTNVuotKhung) ?? 0);
                            data.Add("TongTruyThuNghiOm", dataTruyThu?.Sum(x => x.FNghiOm) ?? 0);
                            data.Add("TongTruyThuHSBL", dataTruyThu?.Sum(x => x.FHSBL) ?? 0);
                            data.Add("TongTruyThuQuyLuong", dataTruyThu?.Sum(x => x.FTongQuyTienLuongNam) ?? 0);
                            data.Add("TongTruyThuBHXHNLD", dataTruyThu?.Sum(x => x.FThuBHXHNLD) ?? 0);
                            data.Add("TongTruyThuBHXHNSD", dataTruyThu?.Sum(x => x.FThuBHXHNSD) ?? 0);
                            data.Add("TongTruyThuBHXHTong", dataTruyThu?.Sum(x => x.FTongSoPhaiThuBHXH) ?? 0);
                            data.Add("TongTruyThuBHYTNLD", dataTruyThu?.Sum(x => x.FThuBHYTNLD) ?? 0);
                            data.Add("TongTruyThuBHYTNSD", dataTruyThu?.Sum(x => x.FThuBHYTNSD) ?? 0);
                            data.Add("TongTruyThuBHYTTong", dataTruyThu?.Sum(x => x.FTongSoPhaiThuBHYT) ?? 0);
                            data.Add("TongTruyThuBHTNNLD", dataTruyThu?.Sum(x => x.FThuBHTNNLD) ?? 0);
                            data.Add("TongTruyThuBHTNNSD", dataTruyThu?.Sum(x => x.FThuBHTNNSD) ?? 0);
                            data.Add("TongTruyThuBHTNTong", dataTruyThu?.Sum(x => x.FTongSoPhaiThuBHTN) ?? 0);
                            data.Add("TongTruyThu", (dataTruyThu?.Sum(x => x.FTongSoPhaiThuBHXH) ?? 0)
                                + (dataTruyThu?.Sum(x => x.FTongSoPhaiThuBHYT) ?? 0)
                                + (dataTruyThu?.Sum(x => x.FTongSoPhaiThuBHTN) ?? 0));

                            AddChuKy(data, _typeChuky);
                            string templateFileName;
                            templateFileName = GetTemplate();
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                            var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery, BhQttBHXHChiTietGiaiThichQuery>(templateFileName, data);
                            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN THU NỘP BHXH, BHYT, BHTN NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }
                        if (IsDataExplanation)
                        {
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            var lstGiaiThichTruyThu = _explainService.ExportGiaiThichTruyThuTongHopDonVi(yearOfWork, selectedUnits).ToList();
                            CalculateDataTruyThu(lstGiaiThichTruyThu);
                            var lstGiaiThichSosanh = _explainService.ExportGiaiThichTongHopSoSanhDonVi(yearOfWork, selectedUnits)
                            .Where(x => x.ILoaiGiaiThich == (int)ExplainType.GIAITHICH_TONGHOP_SOSANH).OrderBy(n => n.ISTT).ToList();
                            var lstGiaiThichGiamDong = _explainService.ExportGiaiThichGiamDongTongHopDonVi(yearOfWork, selectedUnits)
                            .Where(x => x.ILoaiGiaiThich == (int)ExplainType.GIAITHICH_GIAMDONG).OrderBy(n => n.ISTT).ToList();
                            data.Add("YearWork", yearOfWork);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("DiaDiem", string.Empty);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Quarter", QuarterMonthQTTSelected.DisplayItem);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            data.Add("DonVi", _sessionInfo.TenDonVi);
                            data.Add("Title1", Title1);
                            data.Add("Title2", Title2);
                            data.Add("Title3", Title3);
                            data.Add("ListTruyThu", lstGiaiThichTruyThu);
                            data.Add("ListSoSanh", lstGiaiThichSosanh);
                            data.Add("ListGiamDong", lstGiaiThichGiamDong);
                            //Tính tổng cộng
                            data.Add("FTruyThuBhxhNldDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhxhNldDT));
                            data.Add("FTruyThuBhxhNsdDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhxhNsdDT));
                            data.Add("FTruyThuBhxhTongCongDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhxhTongCongDT));
                            data.Add("FTruyThuBhytNldDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhytNldDT));
                            data.Add("FTruyThuBhytNsdDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhytNsdDT));
                            data.Add("FTruyThuBhytTongCongDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhytTongCongDT));
                            data.Add("FTruyThuBhtnNldDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhtnNldDT));
                            data.Add("FTruyThuBhtnNsdDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhtnNsdDT));
                            data.Add("FTruyThuBhtnTongCongDTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhtnTongCongDT));
                            data.Add("FTruyThuBhxhNldHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhxhNldHT));
                            data.Add("FTruyThuBhxhNsdHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhxhNsdHT));
                            data.Add("FTruyThuBhxhTongCongHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhxhTongCongHT));
                            data.Add("FTruyThuBhytNldHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhytNldHT));
                            data.Add("FTruyThuBhytNsdHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhytNsdHT));
                            data.Add("FTruyThuBhytTongCongHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhytTongCongHT));
                            data.Add("FTruyThuBhtnNldHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhtnNldHT));
                            data.Add("FTruyThuBhtnNsdHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhtnNsdHT));
                            data.Add("FTruyThuBhtnTongCongHTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTruyThuBhtnTongCongHT));
                            data.Add("FTongCongTruyThuBHXHTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTongCongTruyThuBHXH));
                            data.Add("FTongCongTruyThuBHYTTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTongCongTruyThuBHYT));
                            data.Add("FTongCongTruyThuBHTNTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTongCongTruyThuBHTN));
                            data.Add("FTongTruyThuTotal", lstGiaiThichTruyThu.Where(n => !n.BHangCha).Sum(x => x.FTongTruyThu));
                            data.Add("FTongSoPhaiThuNop", lstGiaiThichSosanh.Sum(x => x.FSoPhaiThuNop));
                            data.Add("FTongSoDaNopTrongNam", lstGiaiThichSosanh.Sum(x => x.FSoDaNopTrongNam));
                            data.Add("FTongSoDaNopSau3112", lstGiaiThichSosanh.Sum(x => x.FSoDaNopSau3112));
                            data.Add("FTongSoDaNop", lstGiaiThichSosanh.Sum(x => x.FTongSoDaNop));
                            data.Add("FTongSoConPhaiNop", lstGiaiThichSosanh.Sum(x => x.FSoConPhaiNop));
                            data.Add("ITongQuanSo", lstGiaiThichGiamDong.Sum(x => x.IQuanSo));
                            data.Add("FTongQuyTienLuongCanCu", lstGiaiThichGiamDong.Sum(x => x.FQuyTienLuongCanCu));
                            data.Add("FTongSoTienGiamDong", lstGiaiThichGiamDong.Sum(x => x.FSoTienGiamDong));
                            AddChuKy(data, _typeChuky);
                            string templateFileName;
                            templateFileName = Path.Combine(Path.Combine(ExportPrefix.PATH_BH_QTT, ExportFileName.RPT_BH_GIAI_THICH_SO_LIEU_NAM));
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                            var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery, BhQttBHXHChiTietGiaiThichQuery>(templateFileName, data);
                            results.Add(new ExportResult("GIẢI THÍCH SỐ LIỆU NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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

        public void ExportQuyetToanThuBhxhBhtn(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    bool isTongHop = true;
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        isTongHop = false;
                    }
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";
                    var lstDonViSelected = Agencies.Where(item => item.Selected).ToList();
                    var lstSelectedUnitID = string.Join(",", lstDonViSelected.Select(x => x.Id.ToString()).ToList());
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected.ValueItem);
                    var listData = _chungTuChiTietService.ExportQuyetToanThuBhxhBhtn(selectedQuarter, lstSelectedUnitID, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN,
                        donViTinh, isTongHop).ToList();

                    listData.ForEach(item =>
                    {
                        foreach (var prop in item.GetType().GetProperties())
                        {
                            if (prop.Name.StartsWith("F")
                            && prop.PropertyType.IsGenericType
                            && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                            && prop.PropertyType.GetGenericArguments()[0].Name == "Double")
                            {
                                if (prop.CanWrite) prop.SetValue(item, Math.Round(Convert.ToDouble(prop.GetValue(item, null))));
                            }
                            else if (prop.Name.StartsWith("F") && prop.PropertyType.Name == "Double")
                            {
                                if (prop.CanWrite) prop.SetValue(item, Math.Round(Convert.ToDouble(prop.GetValue(item, null))));
                            }
                        }
                    });

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);

                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("TongSoTien", 0);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Year", selectedQuarter);
                    AddChuKy(data, _typeChuky);
                    //BHXH
                    if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHXH)
                    {
                        templateFileName = GetTemplate();
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                        data.Add("Title1", Title1);
                        data.Add("Title2", Title2);
                        data.Add("Title3", Title3);
                        data.Add("TotalBHXHNldDongDuToan", listData.Sum(x => x.FBhxhNldDongDuToan));
                        data.Add("TotalBHXHNsddDongDuToan", listData.Sum(x => x.FBhxhNsddDongDuToan));
                        data.Add("TotalBHXHNldDongHachToan", listData.Sum(x => x.FBhxhNldDongHachToan));
                        data.Add("TotalBHXHNsddDongHachToan", listData.Sum(x => x.FBhxhNsddDongHachToan));
                        data.Add("TotalBHXHDuToan", listData.Sum(x => x.FBHXHTongCongDuToan));
                        data.Add("TotalBHXHHachToan", listData.Sum(x => x.FBHXHTongCongHachToan));
                        data.Add("TotalBHXH", listData.Sum(x => x.FBHXHTongCong));
                    }
                    //BHTN
                    if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHTN)
                    {
                        templateFileName = GetTemplate();
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                        data.Add("Title1", Title1);
                        data.Add("Title2", Title2);
                        data.Add("Title3", Title3);
                        data.Add("TotalBHTNNldDongDuToan", listData.Sum(x => x.FBhtnNldDongDuToan));
                        data.Add("TotalBHTNNsddDongDuToan", listData.Sum(x => x.FBhtnNsddDongDuToan));
                        data.Add("TotalBHTNNldDongHachToan", listData.Sum(x => x.FBhtnNldDongHachToan));
                        data.Add("TotalBHTNNsddDongHachToan", listData.Sum(x => x.FBhtnNsddDongHachToan));
                        data.Add("TotalBHTNDuToan", listData.Sum(x => x.FBHTNTongCongDuToan));
                        data.Add("TotalBHTNHachToan", listData.Sum(x => x.FBHTNTongCongHachToan));
                        data.Add("TotalBHTN", listData.Sum(x => x.FBHTNTongCong));
                    }

                    int stt = 1;
                    foreach (var i in listData)
                    {
                        i.STT = stt++;
                    }
                    data.Add("ListData", listData.Where(x => x.HasDataToPrint));
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery>(templateFileName, data);
                    exportResults.Add(new ExportResult("QUYẾT TOÁN THU BẢO HIỂM THẤT NGHIỆP NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = exportResults;
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

        public void ExportQuyetToanThuBHYT(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    bool isTongHop = true;
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        isTongHop = false;
                    }
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";

                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    List<BhQttBHXHChiTietQuery> listData = new List<BhQttBHXHChiTietQuery>();
                    var lstDonViSelected = Agencies.Where(item => item.Selected).ToList();
                    var lstSelectedUnitID = string.Join(",", lstDonViSelected.Select(x => x.Id.ToString()).ToList());
                    var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected.ValueItem);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    //BHYT - Quan nhan
                    if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHYT_QUAN_NHAN)
                    {
                        listData = _chungTuChiTietService.ExportQuyetToanThuBHYT(selectedQuarter, lstSelectedUnitID, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN,
                            donViTinh, isTongHop, BhxhLoaiSM.QUAN_NHAN).ToList();
                        templateFileName = GetTemplate();
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        data.Add("ListData", listData.Where(x => x.HasDataToPrint));
                        data.Add("Title1", Title1);
                        data.Add("Title2", Title2);
                        data.Add("Title3", Title3);
                        data.Add("BHYTTongDuToan", listData.Sum(x => x.FBHYTTongCongDuToan));
                        data.Add("BHYTTongHachToan", listData.Sum(x => x.FBHYTTongCongHachToan));
                        data.Add("BHYTTongCong", listData.Sum(x => x.FBhytTongCong));
                    }
                    //BHYT - Nguoi Lao Dong
                    if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHYT_NLD)
                    {
                        listData = _chungTuChiTietService.ExportQuyetToanThuBHYT(selectedQuarter, lstSelectedUnitID, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN,
                            donViTinh, isTongHop, BhxhLoaiSM.NGUOI_LAO_DONG).ToList();
                        templateFileName = GetTemplate();
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                        data.Add("ListData", listData);
                        data.Add("Title1", Title1);
                        data.Add("Title2", Title2);
                        data.Add("Title3", Title3);
                        data.Add("TotalBhytNldDongDuToan", listData.Sum(x => x.FBhytNldDongDuToan));
                        data.Add("TotalBhytNsddDongDuToan", listData.Sum(x => x.FBhytNsddDongDuToan));
                        data.Add("TotalBHYTTongCongDuToan", listData.Sum(x => x.FBHYTTongCongDuToan));
                        data.Add("TotalBhytNldDongHachToan", listData.Sum(x => x.FBhytNldDongHachToan));
                        data.Add("TotalBhytNsddDongHachToan", listData.Sum(x => x.FBhytNsddDongHachToan));
                        data.Add("BHYTTongCongHachToan", listData.Sum(x => x.FBHYTTongCongHachToan));
                        data.Add("TotalBHYT", listData.Sum(x => x.FBhytTongCong));
                    }

                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Year", selectedQuarter);
                    AddChuKy(data, _typeChuky);
                    int stt = 1;
                    foreach (var i in listData)
                    {
                        i.STT = stt++;
                    }

                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery>(templateFileName, data);
                    exportResults.Add(new ExportResult("QUYẾT TOÁN THU BẢO HIỂM Y TẾ NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = exportResults;
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

        public void ExportQuyetToanTongHopNamBhxhBhytBhtn(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    bool isTongHop = true;
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        isTongHop = false;
                    }
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    var lstMaDonVi = string.Join(",", Agencies.Any(x => x.Selected) ? Agencies.Where(item => item.Selected).Select(x => x.IIDMaDonVi) : new List<string>());
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    if (isTongHop)
                    {
                        exportResults.Add(ExportResultReport(donViTinh, exportType, lstMaDonVi));
                    }
                    else
                    {
                        foreach (var item in Agencies.Where(x => x.Selected))
                        {
                            exportResults.Add(ExportResultReport(donViTinh, exportType, item.IIDMaDonVi));
                        }

                    }

                    e.Result = exportResults;
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

        private ExportResult ExportResultReport(int donViTinh, ExportType exportType, string lstDonVi)
        {
            try
            {
                string templateFileName = "", fileNamePrefix = "";
                templateFileName = GetTemplate();
                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                CurrencyToText currencyToText = new CurrencyToText();
                data.Add("currencyToText", currencyToText);
                data.Add("FormatNumber", formatNumber);

                data.Add("Cap1", _sessionInfo.TenDonVi);
                data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                data.Add("DiaDiem", _diaDiem);
                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                data.Add("Year", DateTime.Now.Year);
                AddChuKy(data, _typeChuky);
                var lstDataThu = _chungTuChiTietService.ExportQTTBhxhBhytBhtnTongHopDonViNam(_sessionInfo.YearOfWork, lstDonVi, donViTinh, (int)TypeSettlement.THU);
                var lstDataChi = _chungTuChiTietService.ExportQTTBhxhBhytBhtnTongHopDonViNam(_sessionInfo.YearOfWork, lstDonVi, donViTinh, (int)TypeSettlement.CHI);
                CalculateDataExport(lstDataThu);
                CalculateDataExport(lstDataChi);
                data.Add("TieuDe1", Title1);
                data.Add("TieuDe2", Title2);
                data.Add("TieuDe3", Title3);
                data.Add("TongSoTienChi", lstDataChi.Where(x => x.ILevel.Equals(1)).Sum(x => x.FTongSo));
                data.Add("TongSoTienThu", lstDataThu.Where(x => x.ILevel.Equals(1)).Sum(x => x.FTongSo));
                data.Add("TongDuToanChi", lstDataChi.Where(x => x.ILevel.Equals(1)).Sum(x => x.FDuToan));
                data.Add("TongDuToanThu", lstDataThu.Where(x => x.ILevel.Equals(1)).Sum(x => x.FDuToan));
                data.Add("TongHachToanChi", lstDataChi.Where(x => x.ILevel.Equals(1)).Sum(x => x.FHachToan));
                data.Add("TongHachToanThu", lstDataThu.Where(x => x.ILevel.Equals(1)).Sum(x => x.FHachToan));
                data.Add("ListDataThu", lstDataThu);
                data.Add("ListDataChi", lstDataChi);
                string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                var xlsFile = _exportService.Export<BhReportQttBHXHChiTietQuery>(templateFileName, data);
                return new ExportResult("QUYẾT TOÁN TỔNG HỢP NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw;
            }

        }

        private void CalculateDataExport(IEnumerable<BhReportQttBHXHChiTietQuery> lstData)
        {
            if (lstData.IsEmpty()) return;
            var lstParentRecal = lstData.Where(x => lstData.Where(w => !w.IIdParent.IsNullOrEmpty()).Select(x => x.IIdParent).Distinct().Contains(x.IIdChungTu)).OrderByDescending(o => o.ILevel).ToList();
            foreach (var item in lstParentRecal)
            {
                item.FDuToan = lstData.Where(x => x.IIdParent.Equals(item.IIdChungTu)).Sum(s => s.FDuToan);
                item.FHachToan = lstData.Where(x => x.IIdParent.Equals(item.IIdChungTu)).Sum(s => s.FHachToan);
            }
            if (lstData.Any(x => x.IKinhPhiKCB == 3))
            {
                var itemKCB3 = lstData.FirstOrDefault(x => x.IKinhPhiKCB == 3);
                var itemKCB2 = lstData.FirstOrDefault(x => x.IKinhPhiKCB == 2);
                var itemKCB1 = lstData.FirstOrDefault(x => x.IKinhPhiKCB == 1);
                itemKCB3.FDuToan = itemKCB1.FDuToan - itemKCB2.FDuToan;
                itemKCB3.FHachToan = itemKCB1.FHachToan - itemKCB2.FHachToan;
            }
        }

        public void ExportQuyetToanTongHopThuChi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    bool isTongHop = true;
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        isTongHop = false;
                    }
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";

                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var lstDonViSelected = Agencies.Where(item => item.Selected).ToList();
                    var lstSelectedUnitID = string.Join(",", lstDonViSelected.Select(x => x.Id).ToList());
                    var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected.ValueItem);
                    List<BhQttBHXHChiTietQuery> listData = new List<BhQttBHXHChiTietQuery>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();

                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        foreach (var item in lstDonViSelected)
                        {
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            listData = _chungTuChiTietService.ExportTongHopQuyetToanThuChi(selectedQuarter, item.Id, donViTinh, isTongHop).OrderBy(x => x.SNoiDung).ToList();
                            templateFileName = GetTemplate();
                            listData.ForEach(x => x.BHangCha = new List<string>() { "I", "II" }.Contains(x.STTDisplay));

                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            data.Add("ListData", listData);
                            data.Add("Title1", Title1);
                            data.Add("Title2", Title2);
                            data.Add("Title3", Title3);
                            data.Add("TongCong", listData.Where(x => new List<string>() { "I", "II" }.Contains(x.STTDisplay)).Sum(x => x.FSoTien));
                            data.Add("TongCongThu", listData.Where(x => new List<string>() { "I" }.Contains(x.STTDisplay)).Sum(x => x.FSoTien));
                            data.Add("TongCongChi", listData.Where(x => new List<string>() { "II" }.Contains(x.STTDisplay)).Sum(x => x.FSoTien));
                            data.Add("CurrencyToText", currencyToText);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("Cap1", _sessionInfo.TenDonVi);
                            data.Add("h1", "");
                            data.Add("h2", "");
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            data.Add("DiaDiem", _diaDiem);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Year", selectedQuarter);
                            AddChuKy(data, _typeChuky);
                            int stt = 1;
                            foreach (var i in listData)
                            {
                                i.STT = stt++;
                            }

                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                            var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery>(templateFileName, data);
                            exportResults.Add(new ExportResult("QUYẾT TOÁN THU BẢO HIỂM Y TẾ NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }
                    }
                    else
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        listData = _chungTuChiTietService.ExportTongHopQuyetToanThuChi(selectedQuarter, lstSelectedUnitID, donViTinh, isTongHop).OrderBy(x => x.SNoiDung).ToList();
                        templateFileName = GetTemplate();
                        listData.ForEach(x => x.BHangCha = new List<string>() { "I", "II" }.Contains(x.STTDisplay));

                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        data.Add("ListData", listData);
                        data.Add("Title1", Title1);
                        data.Add("Title2", Title2);
                        data.Add("Title3", Title3);
                        data.Add("TongCong", listData.Where(x => new List<string>() { "I", "II" }.Contains(x.STTDisplay)).Sum(x => x.FSoTien));
                        data.Add("TongCongThu", listData.Where(x => new List<string>() { "I" }.Contains(x.STTDisplay)).Sum(x => x.FSoTien));
                        data.Add("TongCongChi", listData.Where(x => new List<string>() { "II" }.Contains(x.STTDisplay)).Sum(x => x.FSoTien));
                        data.Add("CurrencyToText", currencyToText);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", _sessionInfo.TenDonVi);
                        data.Add("h1", "");
                        data.Add("h2", "");
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Year", selectedQuarter);
                        AddChuKy(data, _typeChuky);
                        int stt = 1;
                        foreach (var i in listData)
                        {
                            i.STT = stt++;
                        }

                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                        var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery>(templateFileName, data);
                        exportResults.Add(new ExportResult("QUYẾT TOÁN THU BẢO HIỂM Y TẾ NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    }



                    e.Result = exportResults;
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

        private void CalculateData(List<BhQttBHXHChiTietQuery> lstChungTuChiTiet)
        {
            foreach (var item in lstChungTuChiTiet)
            {
                item.FLuongChinh = Math.Round(item.FLuongChinh.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FPhuCapChucVu = Math.Round(item.FPhuCapChucVu.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FPCTNNghe = Math.Round(item.FPCTNNghe.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FPCTNVuotKhung = Math.Round(item.FPCTNVuotKhung.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FNghiOm = Math.Round(item.FNghiOm.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FHSBL = Math.Round(item.FHSBL.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongQuyTienLuongNam = Math.Round(item.FTongQuyTienLuongNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FDuToan = Math.Round(item.FDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FDaQuyetToan = Math.Round(item.FDaQuyetToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FConLai = Math.Round(item.FConLai.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FThuBHXHNLD = Math.Round(item.FThuBHXHNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FThuBHXHNSD = Math.Round(item.FThuBHXHNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongSoPhaiThuBHXH = Math.Round(item.FTongSoPhaiThuBHXH.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FThuBHYTNLD = Math.Round(item.FThuBHYTNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FThuBHYTNSD = Math.Round(item.FThuBHYTNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongSoPhaiThuBHYT = Math.Round(item.FTongSoPhaiThuBHYT.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FThuBHTNNLD = Math.Round(item.FThuBHTNNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FThuBHTNNSD = Math.Round(item.FThuBHTNNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongSoPhaiThuBHTN = Math.Round(item.FTongSoPhaiThuBHTN.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongNLD = Math.Round(item.FTongNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongNSD = Math.Round(item.FTongNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongCong = Math.Round(item.FTongCong.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            }

            lstChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.IQSBQNam = 0;
                    x.FLuongChinh = 0;
                    x.FPhuCapChucVu = 0;
                    x.FPCTNNghe = 0;
                    x.FPCTNVuotKhung = 0;
                    x.FNghiOm = 0;
                    x.FHSBL = 0;
                    x.FTongQuyTienLuongNam = 0;
                    x.FDuToan = 0;
                    x.FDaQuyetToan = 0;
                    x.FConLai = 0;
                    x.FThuBHXHNLD = 0;
                    x.FThuBHXHNSD = 0;
                    x.FTongSoPhaiThuBHXH = 0;
                    x.FThuBHYTNLD = 0;
                    x.FThuBHYTNSD = 0;
                    x.FTongSoPhaiThuBHYT = 0;
                    x.FThuBHTNNLD = 0;
                    x.FThuBHTNNSD = 0;
                    x.FTongSoPhaiThuBHTN = 0;
                    x.FTongNLD = 0;
                    x.FTongNSD = 0;
                    x.FTongCong = 0;
                    return x;
                }).ToList();
            var temp = lstChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParent(item.IIDMLNSCha, item, lstChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, BhQttBHXHChiTietQuery item, List<BhQttBHXHChiTietQuery> lstChungTuChiTiet)
        {
            var dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IIDMLNS).ToDictionary(x => x.Key, x => x.First());
            if (idParent == null || !dictByMlns.ContainsKey(idParent.Value))
            {
                return;
            }
            var model = dictByMlns[idParent.Value];
            model.IQSBQNam = (model.IQSBQNam ?? 0) + (item.IQSBQNam ?? 0);
            model.FLuongChinh = (model.FLuongChinh ?? 0) + (item.FLuongChinh ?? 0);
            model.FPhuCapChucVu = (model.FPhuCapChucVu ?? 0) + (item.FPhuCapChucVu ?? 0);
            model.FPCTNNghe = (model.FPCTNNghe ?? 0) + (item.FPCTNNghe ?? 0);
            model.FPCTNVuotKhung = (model.FPCTNVuotKhung ?? 0) + (item.FPCTNVuotKhung ?? 0);
            model.FNghiOm = (model.FNghiOm ?? 0) + (item.FNghiOm ?? 0);
            model.FHSBL = (model.FHSBL ?? 0) + (item.FHSBL ?? 0);
            model.FTongQuyTienLuongNam = (model.FTongQuyTienLuongNam ?? 0) + (item.FTongQuyTienLuongNam ?? 0);
            model.FDuToan = (model.FDuToan ?? 0) + (item.FDuToan ?? 0);
            model.FDaQuyetToan = (model.FDaQuyetToan ?? 0) + (item.FDaQuyetToan ?? 0);
            model.FConLai = (model.FConLai ?? 0) + (item.FConLai ?? 0);
            model.FThuBHXHNLD = (model.FThuBHXHNLD ?? 0) + (item.FThuBHXHNLD ?? 0);
            model.FThuBHXHNSD = (model.FThuBHXHNSD ?? 0) + (item.FThuBHXHNSD ?? 0);
            model.FTongSoPhaiThuBHXH = (model.FTongSoPhaiThuBHXH ?? 0) + (item.FTongSoPhaiThuBHXH ?? 0);
            model.FThuBHYTNLD = (model.FThuBHYTNLD ?? 0) + (item.FThuBHYTNLD ?? 0);
            model.FThuBHYTNSD = (model.FThuBHYTNSD ?? 0) + (item.FThuBHYTNSD ?? 0);
            model.FTongSoPhaiThuBHYT = (model.FTongSoPhaiThuBHYT ?? 0) + (item.FTongSoPhaiThuBHYT ?? 0);
            model.FThuBHTNNLD = (model.FThuBHTNNLD ?? 0) + (item.FThuBHTNNLD ?? 0);
            model.FThuBHTNNSD = (model.FThuBHTNNSD ?? 0) + (item.FThuBHTNNSD ?? 0);
            model.FTongSoPhaiThuBHTN = (model.FTongSoPhaiThuBHTN ?? 0) + (item.FTongSoPhaiThuBHTN ?? 0);
            model.FTongNLD = (model.FTongNLD ?? 0) + (item.FTongNLD ?? 0);
            model.FTongNSD = (model.FTongNSD ?? 0) + (item.FTongNSD ?? 0);
            model.FTongCong = (model.FTongCong ?? 0) + (item.FTongCong ?? 0);
            CalculateParent(model.IIDMLNSCha, item, lstChungTuChiTiet);
        }

        private void CalculateDataExplain(List<BhQttBHXHChiTietGiaiThichQuery> lstChungTuChiTiet)
        {
            foreach (var item in lstChungTuChiTiet)
            {
                item.FLuongChinh = Math.Round(item.FLuongChinh.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FPCChucVu = Math.Round(item.FPCChucVu.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FPCTNNghe = Math.Round(item.FPCTNNghe.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FPCTNVuotKhung = Math.Round(item.FPCTNVuotKhung.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FNghiOm = Math.Round(item.FNghiOm.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FHSBL = Math.Round(item.FHSBL.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongQuyLuong = Math.Round(item.FTongQuyLuong.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTruyThuBHXHNLD = Math.Round(item.FTruyThuBHXHNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTruyThuBHXHNSD = Math.Round(item.FTruyThuBHXHNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTruyThuBHXHTongCong = Math.Round(item.FTruyThuBHXHTongCong.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTruyThuBHYTNLD = Math.Round(item.FTruyThuBHYTNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTruyThuBHYTNSD = Math.Round(item.FTruyThuBHYTNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTruyThuBHYTTongCong = Math.Round(item.FTruyThuBHYTTongCong.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTruyThuBHTNNLD = Math.Round(item.FTruyThuBHTNNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTruyThuBHTNNSD = Math.Round(item.FTruyThuBHTNNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTruyThuBHTNTongCong = Math.Round(item.FTruyThuBHTNTongCong.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongTruyThuBHXH = Math.Round(item.FTongTruyThuBHXH.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            }

            lstChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.FLuongChinh = 0;
                    x.FPCChucVu = 0;
                    x.FPCTNNghe = 0;
                    x.FPCTNVuotKhung = 0;
                    x.FNghiOm = 0;
                    x.FHSBL = 0;
                    x.FTongQuyLuong = 0;
                    x.FTruyThuBHXHNLD = 0;
                    x.FTruyThuBHXHNSD = 0;
                    x.FTruyThuBHXHTongCong = 0;
                    x.FTruyThuBHYTNLD = 0;
                    x.FTruyThuBHYTNSD = 0;
                    x.FTruyThuBHYTTongCong = 0;
                    x.FTruyThuBHTNNLD = 0;
                    x.FTruyThuBHTNNSD = 0;
                    x.FTruyThuBHTNTongCong = 0;
                    x.FTongTruyThuBHXH = 0;
                    return x;
                }).ToList();
            var temp = lstChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParentDataExplain(item.IIDMLNSCha, item, lstChungTuChiTiet);
            }
        }

        private void CalculateParentDataExplain(Guid? idParent, BhQttBHXHChiTietGiaiThichQuery item, List<BhQttBHXHChiTietGiaiThichQuery> lstChungTuChiTiet)
        {
            var dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IIDMLNS).ToDictionary(x => x.Key, x => x.First());
            if (idParent == null || !dictByMlns.ContainsKey(idParent.Value))
            {
                return;
            }
            var model = dictByMlns[idParent.Value];
            model.FLuongChinh += item.FLuongChinh.GetValueOrDefault();
            model.FPCChucVu += item.FPCChucVu.GetValueOrDefault();
            model.FPCTNNghe += item.FPCTNNghe.GetValueOrDefault();
            model.FPCTNVuotKhung += item.FPCTNVuotKhung.GetValueOrDefault();
            model.FNghiOm += item.FNghiOm.GetValueOrDefault();
            model.FHSBL += item.FHSBL.GetValueOrDefault();
            model.FTongQuyLuong += item.FTongQuyLuong.GetValueOrDefault();
            model.FTruyThuBHXHNLD += item.FTruyThuBHXHNLD.GetValueOrDefault();
            model.FTruyThuBHXHNSD += item.FTruyThuBHXHNSD.GetValueOrDefault();
            model.FTruyThuBHXHTongCong += item.FTruyThuBHXHTongCong.GetValueOrDefault();
            model.FTruyThuBHYTNLD += item.FTruyThuBHYTNLD.GetValueOrDefault();
            model.FTruyThuBHYTNSD += item.FTruyThuBHYTNSD.GetValueOrDefault();
            model.FTruyThuBHYTTongCong += item.FTruyThuBHYTTongCong.GetValueOrDefault();
            model.FTruyThuBHTNNLD += item.FTruyThuBHTNNLD.GetValueOrDefault();
            model.FTruyThuBHTNNSD += item.FTruyThuBHTNNSD.GetValueOrDefault();
            model.FTruyThuBHTNTongCong += item.FTruyThuBHTNTongCong.GetValueOrDefault();
            model.FTongTruyThuBHXH += item.FTongTruyThuBHXH.GetValueOrDefault();

            CalculateParentDataExplain(model.IIDMLNSCha, item, lstChungTuChiTiet);
        }

        private void CalculateDataTruyThu(List<BhQttBHXHChiTietGiaiThichQuery> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.FTruyThuBhxhNldDT = 0;
                    x.FTruyThuBhxhNsdDT = 0;
                    x.FTruyThuBhxhTongCongDT = 0;
                    x.FTruyThuBhytNldDT = 0;
                    x.FTruyThuBhytNsdDT = 0;
                    x.FTruyThuBhytTongCongDT = 0;
                    x.FTruyThuBhtnNldDT = 0;
                    x.FTruyThuBhtnNsdDT = 0;
                    x.FTruyThuBhtnTongCongDT = 0;
                    x.FTruyThuBhxhNldHT = 0;
                    x.FTruyThuBhxhNsdHT = 0;
                    x.FTruyThuBhxhTongCongHT = 0;
                    x.FTruyThuBhytNldHT = 0;
                    x.FTruyThuBhytNsdHT = 0;
                    x.FTruyThuBhytTongCongHT = 0;
                    x.FTruyThuBhtnNldHT = 0;
                    x.FTruyThuBhtnNsdHT = 0;
                    x.FTruyThuBhtnTongCongHT = 0;
                    x.FTongCongTruyThuBHXH = 0;
                    x.FTongCongTruyThuBHYT = 0;
                    x.FTongCongTruyThuBHTN = 0;
                    x.FTongTruyThu = 0;
                    return x;
                }).ToList();
            var temp = lstChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParentTruyThu(item.IIDMLNSCha, item, lstChungTuChiTiet);
            }
        }

        private void CalculateParentTruyThu(Guid? idParent, BhQttBHXHChiTietGiaiThichQuery item, List<BhQttBHXHChiTietGiaiThichQuery> lstChungTuChiTiet)
        {
            var dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IIDMLNS).ToDictionary(x => x.Key, x => x.First());
            if (idParent == null || !dictByMlns.ContainsKey(idParent.Value))
            {
                return;
            }
            var model = dictByMlns[idParent.Value];
            model.FTruyThuBhxhNldDT = (model.FTruyThuBhxhNldDT.GetValueOrDefault()) + (item.FTruyThuBhxhNldDT.GetValueOrDefault());
            model.FTruyThuBhxhNsdDT = (model.FTruyThuBhxhNsdDT.GetValueOrDefault()) + (item.FTruyThuBhxhNsdDT.GetValueOrDefault());
            model.FTruyThuBhxhTongCongDT = (model.FTruyThuBhxhTongCongDT.GetValueOrDefault()) + (item.FTruyThuBhxhTongCongDT.GetValueOrDefault());
            model.FTruyThuBhytNldDT = (model.FTruyThuBhytNldDT.GetValueOrDefault()) + (item.FTruyThuBhytNldDT.GetValueOrDefault());
            model.FTruyThuBhytNsdDT = (model.FTruyThuBhytNsdDT.GetValueOrDefault()) + (item.FTruyThuBhytNsdDT.GetValueOrDefault());
            model.FTruyThuBhytTongCongDT = (model.FTruyThuBhytTongCongDT.GetValueOrDefault()) + (item.FTruyThuBhytTongCongDT.GetValueOrDefault());
            model.FTruyThuBhtnNldDT = (model.FTruyThuBhtnNldDT.GetValueOrDefault()) + (item.FTruyThuBhtnNldDT.GetValueOrDefault());
            model.FTruyThuBhtnNsdDT = (model.FTruyThuBhtnNsdDT.GetValueOrDefault()) + (item.FTruyThuBhtnNsdDT.GetValueOrDefault());
            model.FTruyThuBhtnTongCongDT = (model.FTruyThuBhtnTongCongDT.GetValueOrDefault()) + (item.FTruyThuBhtnTongCongDT.GetValueOrDefault());
            model.FTruyThuBhxhNldHT = (model.FTruyThuBhxhNldHT.GetValueOrDefault()) + (item.FTruyThuBhxhNldHT.GetValueOrDefault());
            model.FTruyThuBhxhNsdHT = (model.FTruyThuBhxhNsdHT.GetValueOrDefault()) + (item.FTruyThuBhxhNsdHT.GetValueOrDefault());
            model.FTruyThuBhxhTongCongHT = (model.FTruyThuBhxhTongCongHT.GetValueOrDefault()) + (item.FTruyThuBhxhTongCongHT.GetValueOrDefault());
            model.FTruyThuBhytNldHT = (model.FTruyThuBhytNldHT.GetValueOrDefault()) + (item.FTruyThuBhytNldHT.GetValueOrDefault());
            model.FTruyThuBhytNsdHT = (model.FTruyThuBhytNsdHT.GetValueOrDefault()) + (item.FTruyThuBhytNsdHT.GetValueOrDefault());
            model.FTruyThuBhytTongCongHT = (model.FTruyThuBhytTongCongHT.GetValueOrDefault()) + (item.FTruyThuBhytTongCongHT.GetValueOrDefault());
            model.FTruyThuBhtnNldHT = (model.FTruyThuBhtnNldHT.GetValueOrDefault()) + (item.FTruyThuBhtnNldHT.GetValueOrDefault());
            model.FTruyThuBhtnNsdHT = (model.FTruyThuBhtnNsdHT.GetValueOrDefault()) + (item.FTruyThuBhtnNsdHT.GetValueOrDefault());
            model.FTruyThuBhtnTongCongHT = (model.FTruyThuBhtnTongCongHT.GetValueOrDefault()) + (item.FTruyThuBhtnTongCongHT.GetValueOrDefault());
            model.FTongCongTruyThuBHXH = (model.FTongCongTruyThuBHXH.GetValueOrDefault()) + (item.FTongCongTruyThuBHXH.GetValueOrDefault());
            model.FTongCongTruyThuBHYT = (model.FTongCongTruyThuBHYT.GetValueOrDefault()) + (item.FTongCongTruyThuBHYT.GetValueOrDefault());
            model.FTongCongTruyThuBHTN = (model.FTongCongTruyThuBHTN.GetValueOrDefault()) + (item.FTongCongTruyThuBHTN.GetValueOrDefault());
            model.FTongTruyThu = (model.FTongTruyThu.GetValueOrDefault()) + (item.FTongTruyThu.GetValueOrDefault());
        }

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh1MoTa);
            data.Add("ChucDanh1", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh1MoTa);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy == null ? string.Empty : dmChyKy.Ten1MoTa);

            data.Add("ThuaLenh2", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh2MoTa);
            data.Add("ChucDanh2", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh2MoTa);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy == null ? string.Empty : dmChyKy.Ten2MoTa);

            data.Add("ThuaLenh3", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh3MoTa);
            data.Add("ChucDanh3", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh3MoTa);
            data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten3", dmChyKy == null ? string.Empty : dmChyKy.Ten3MoTa);

            data.Add("ThuaLenh4", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh4MoTa);
            data.Add("ChucDanh4", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh4MoTa);
            data.Add("GhiChuKy4", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten4", dmChyKy == null ? string.Empty : dmChyKy.Ten4MoTa);

            data.Add("ThuaLenh5", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh5MoTa);
            data.Add("ChucDanh5", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh5MoTa);
            data.Add("GhiChuKy5", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten5", dmChyKy == null ? string.Empty : dmChyKy.Ten5MoTa);

            data.Add("ThuaLenh6", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh6MoTa);
            data.Add("ChucDanh6", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh6MoTa);
            data.Add("GhiChuKy6", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten6", dmChyKy == null ? string.Empty : dmChyKy.Ten6MoTa);
            if (dmChyKy != null && (!dmChyKy.ThuaLenh4MoTa.IsEmpty() || !dmChyKy.ChucDanh4MoTa.IsEmpty() || !dmChyKy.Ten4MoTa.IsEmpty()))
            {
                data.Add("Co6ChuKy", true);
            }
        }

        private void LoadTypeChuKy()
        {
            if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY && LoaiThu == LoaiThu.All)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTT_QUY_ALL;
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY && LoaiThu == LoaiThu.BHXH)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_QUY) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_QUY;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTT_QUY_BHXH;
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY && LoaiThu == LoaiThu.BHYT)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_NOP_BHYT_QUY) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_NOP_BHYT_QUY;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTT_QUY_BHYT;
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY && LoaiThu == LoaiThu.BHTN)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_NOP_BHTN_QUY) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_NOP_BHTN_QUY;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTT_QUY_BHTN;
            }

            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTT_NAM;
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHXH)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_BHXH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_BHXH;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTT_PHU_LUC_BHXH;
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHTN)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_BHTN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_BHTN;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTT_PHU_LUC_BHTN;
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHYT_QUAN_NHAN)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_BHYT_QUAN_NHAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_BHYT_QUAN_NHAN;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTT_PHU_LUC_BHYT_QN;
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHYT_NLD)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_BHYT_NLD) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_BHYT_NLD;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTT_PHU_LUC_BHYT_NLD;
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_TONG_HOP_NAM)
            {
                _typeChuky = TypeChuKy.QUYET_TOAN_TONG_HOP_NAM;
                _title1 = $"Thông báo phê duyệt quyết toán thu, chi BHXH, BHYT, BHTN năm {DateTime.Now.Year}";
                _title2 = $"(Kèm theo quyết định số: ......../QĐ-BQP ngày ...../...../........)";
            }
            else if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_CHI_TONG_HOP)
            {
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_CHI_TONG_HOP;
                _title1 = $"Tổng hợp quyết toán thu, chi BHXH, BHYT, BHTN năm {DateTime.Now.Year}";
                _title2 = $"(Kèm theo quyết định số: ......../QĐ-BQP ngày ...../...../...... của Bộ trưởng Bộ Quốc phòng)";
            }
        }
    }
}
