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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.PrintReport
{
    public class PrintChiTieuKinhPhiViewModel : ViewModelBase
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
        private readonly IBhDmThamDinhQuyetToanService _bhDmThamDinhQuyetToanService;
        private readonly IBhThamDinhQuyetToanService _bhThamDinhQuyetToanService;
        private readonly IBhThamDinhQuyetToanChiTietService _bhThamDinhQuyetToanChiTietService;
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
            (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_MUA_SAM_TTBYT => "In báo cáo chỉ tiêu và quyết toán chi kinh phí mua sắm TTB y tế",
            (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_HSSV_NLD => "In báo cáo chỉ tiêu và quyết toán chi kinh phí CSSK ban đầu HSSV và NLĐ",
            (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_QUAN_Y_DON_VI => "In báo cáo chỉ tiêu và quyết toán chi kinh phí KCB tại quân y đơn vị",
            (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_CHI_CHE_DO_BHXH => "In báo cáo quyết toán chi các chế độ BHXH",
            _ => ""
        };

        public override Type ContentType => typeof(PrintChiTieuKinhPhi);

        public override string Name => SettlementName;
        public override string Title => SettlementName;
        public override string Description => SettlementName;
        public bool IsEnableCheckBoxSummary => _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString();

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
                OnPropertyChanged(nameof(IsEnabledExplanation));
                LoadTypeChuKy();
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

        public bool IsShowLoaiBaoCao => SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_HSSV_NLD;

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

        public bool IsExportEnable
        {
            get
            {
                if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHXH
                    || SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHTN
                    || SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHYT_QUAN_NHAN
                    || SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_BHYT_NLD
                    || SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_CHI_TONG_HOP
                    || SettlementTypeValue == (int)QttType.QUYET_TOAN_TONG_HOP_NAM)
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

        public PrintChiTieuKinhPhiViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IQttBHXHService chungTuService,
            IQttBHXHChiTietService chungTuChiTietService,
            IQttBHXHChiTietGiaiThichService iQttBHXHChiTietGiaiThichService,
            IExportService exportService,
            INsDonViService donViService,
            IBhDmThamDinhQuyetToanService bhDmThamDinhQuyetToanService,
            IBhThamDinhQuyetToanService bhThamDinhQuyetToanService,
            IBhThamDinhQuyetToanChiTietService bhThamDinhQuyetToanChiTietService,
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
            _bhDmThamDinhQuyetToanService = bhDmThamDinhQuyetToanService;
            _bhThamDinhQuyetToanService = bhThamDinhQuyetToanService;
            _bhThamDinhQuyetToanChiTietService = bhThamDinhQuyetToanChiTietService;
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
            LoadReportType();
            LoadTypeChuKy();
            LoadQuarterYears();
            LoadDanhMuc();
            LoadAgencies();
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
                    else if (IsSummaryAgency || (!IsSummaryAgency && !IsSummary))
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
            /*var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected?.ValueItem ?? "3");
            var selectedQuarterType = Int32.Parse(QuarterMonthQTTSelected?.HiddenValue ?? "1");
            List<BhQttBHXHQuery> listChungTuDuocXem = new List<BhQttBHXHQuery>();
            if (SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY || SettlementTypeValue == (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM)
            {
                if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                {
                    listChungTuDuocXem = _chungTuService.FindChungTuDonVi(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType).ToList();
                }
                else
                {
                    if (IsSummary)
                    {
                        listChungTuDuocXem = _chungTuService.FindChungTuDonViTongHop(yearOfWork, BhxhLoaiChungTu.BhxhChungTuTongHop, _sessionService.Current.Principal, selectedQuarter, selectedQuarterType).ToList();
                    }
                    else if (IsSummaryAgency && !IsSummary)
                    {
                        listChungTuDuocXem = _chungTuService.FindChungTuDonVi(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType).ToList();
                    }
                    else if (!IsSummaryAgency && !IsSummary)
                    {
                        listChungTuDuocXem = _chungTuService.FindChungTuDonVi(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType).ToList();
                    }
                    //else
                    //{
                    //    listChungTuDuocXem = _chungTuService.FindChungTuDonViTongHop(yearOfWork, BhxhLoaiChungTu.BhxhChungTuTongHop, _sessionService.Current.Principal, selectedQuarter).ToList();
                    //}
                }
            }
            else
            {
                listChungTuDuocXem = _chungTuService.FindAllChungTuDonVi(yearOfWork, selectedQuarter).ToList();
            }*/

            List<BhThamDinhQuyetToan> listChungTuDuocXem = new List<BhThamDinhQuyetToan>();
            listChungTuDuocXem = _bhThamDinhQuyetToanService.FindUnitVoucher(yearOfWork).ToList();
            var lstIdDonVi = listChungTuDuocXem.Select(x => x.IID_MaDonVi).Distinct().ToList();
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


        private void LoadReportType()
        {
            _reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = LoaiBaoCao.BaoHiemThamDinhQuyetToanChiCsskHssv, ValueItem = LoaiBaoCao.BaoHiemThamDinhQuyetToanChiCsskHssv },
                new ComboboxItem { DisplayItem = LoaiBaoCao.BaoHiemThamDinhQuyetToanChiCsskNld, ValueItem = LoaiBaoCao.BaoHiemThamDinhQuyetToanChiCsskNld }
            };
            _selectedReportType = _reportTypes.First();
        }

        private void LoadQuarterYears()
        {
            _quarterMonths = new List<ComboboxItem>();
            _quarterMonths.Add(new ComboboxItem("Quý I", "3"));
            _quarterMonths.Add(new ComboboxItem("Quý II", "6"));
            _quarterMonths.Add(new ComboboxItem("Quý III", "9"));
            _quarterMonths.Add(new ComboboxItem("Quý IV", "12"));

            QuarterMonthQTTSelected = _quarterMonths.First();
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
            string input = SettlementTypeValue switch
            {
                (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_MUA_SAM_TTBYT => Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THAM_DINH_QUYET_TOAN_CHI_MUA_SAM_TTBYT),
                (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_HSSV_NLD => Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THAM_DINH_QUYET_TOAN_CHI_CSSK_HSSV_NLD),
                (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_QUAN_Y_DON_VI => Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THAM_DINH_QUYET_TOAN_CHI_KCB_QYDV),
                (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_CHI_CHE_DO_BHXH => Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THAM_DINH_QUYET_TOAN_CHI_CHE_DO_BHXH),
                _ => ""
            };
            return Path.Combine(ExportPrefix.PATH_BH_THAMDINHQUYETTOAN, input + FileExtensionFormats.Xlsx);
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
            if (_typeChuky == TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY)
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
            DmChuKyDialogViewModel.SavedAction = obj => LoadTypeChuKy();
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


            switch (SettlementTypeValue)
            {
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_MUA_SAM_TTBYT:
                    ExportQuyetToanKinhPhiTTBYT(exportType);
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_HSSV_NLD:
                    ExportQuyetToanKinhPhiKCBHSSVNLD(exportType);
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_QUAN_Y_DON_VI:
                    ExportQuyetToanKinhPhiKCBQuanYDonVi(exportType);
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_CHI_CHE_DO_BHXH:
                    ExportQuyetToanKinhPhiCheDoBHXH(exportType);
                    break;
                default:
                    break;
            }
        }

        private void ExportQuyetToanKinhPhiKCBHSSVNLD(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.IIDMaDonVi).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    var lstData = new List<BhThamDinhQuyetToanChiKCBHSSVNLDQuery>();
                    if (SelectedReportType.ValueItem == LoaiBaoCao.BaoHiemThamDinhQuyetToanChiCsskHssv)
                        lstData = _bhThamDinhQuyetToanChiTietService.GetChiKinhPhiCSSKHSSVNLD(_sessionService.Current.YearOfWork, string.Join(",", lstIdDonVi), 1, donViTinh).ToList();
                    else if (SelectedReportType.ValueItem == LoaiBaoCao.BaoHiemThamDinhQuyetToanChiCsskNld)
                        lstData = _bhThamDinhQuyetToanChiTietService.GetChiKinhPhiCSSKHSSVNLD(_sessionService.Current.YearOfWork, string.Join(",", lstIdDonVi), 2, donViTinh).ToList();

                    //CalculateData(lstData);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    var tongSoTien = lstData.Sum(x => x.QuyetToan);

                    if (lstData.Count < DefaultConst.BHXH_10_Rows)
                    {
                        var rowRemain = DefaultConst.BHXH_10_Rows - lstData.Count;
                        for (int i = 0; i < rowRemain; i++)
                        {
                            lstData.Add(new BhThamDinhQuyetToanChiKCBHSSVNLDQuery());
                        }
                    }
                    int stt = 1;
                    foreach (var i in lstData.Where(x => x.HasData.GetValueOrDefault()))
                    {
                        i.STT = stt++;
                    }

                    data.Add("ListData", lstData);
                    data.Add("TongSoTien", tongSoTien);
                    data.Add("TongCongBangChu", StringUtils.NumberToText(tongSoTien * donViTinh) ?? "");
                    data.Add("TongDuToan", lstData.Sum(x => x.TongDuToan));
                    data.Add("TongDuToanNamTruoc", lstData.Sum(x => x.DuToanNamTruoc));
                    data.Add("TongDuToanNamNay", lstData.Sum(x => x.DuToanNamNay));
                    data.Add("TongThua", lstData.Sum(x => x.Thua));
                    data.Add("TongThieu", lstData.Sum(x => x.Thieu));
                    data.Add("TongQuyetToan", lstData.Sum(x => x.QuyetToan));
                    data.Add("YearWork", yearOfWork);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Year", yearOfWork);
                    data.Add("Quarter", QuarterMonthQTTSelected.DisplayItem);
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("DonViCap2", _sessionInfo.TenDonVi);
                    data.Add("DonViCap1", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    //Tính tổng cộng
                    AddChuKy(data, _typeChuky);
                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhThamDinhQuyetToanChiKCBHSSVNLDQuery>(templateFileName, data);
                    results.Add(new ExportResult("BÁO CÁO THẨM ĐỊNH QUYẾT TOÁN NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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

        private void ExportQuyetToanKinhPhiKCBQuanYDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.IIDMaDonVi).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    var lstData = _bhThamDinhQuyetToanChiTietService.GetChiKinhPhiKCBQuanYDonVi(_sessionService.Current.YearOfWork, string.Join(",", lstIdDonVi), donViTinh).ToList();
                    //CalculateData(lstData);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    var tongSoTien = lstData.Sum(x => x.QuyetToan);

                    if (lstData.Count < DefaultConst.BHXH_10_Rows)
                    {
                        var rowRemain = DefaultConst.BHXH_10_Rows - lstData.Count;
                        for (int i = 0; i < rowRemain; i++)
                        {
                            lstData.Add(new BhThamDinhQuyetToanChiKCBQuanYDonViQuery());
                        }
                    }
                    int stt = 1;
                    foreach (var i in lstData.Where(x => x.HasData.GetValueOrDefault()))
                    {
                        i.STT = stt++;
                    }

                    data.Add("ListData", lstData);
                    data.Add("TongSoTien", tongSoTien);
                    data.Add("TongCongBangChu", StringUtils.NumberToText(tongSoTien * donViTinh) ?? "");
                    data.Add("TongDuToan", lstData.Sum(x => x.TongDuToan));
                    data.Add("TongDuToanNamTruoc", lstData.Sum(x => x.DuToanNamTruoc));
                    data.Add("TongDuToanNamNay", lstData.Sum(x => x.DuToanNamNay));
                    data.Add("TongThua", lstData.Sum(x => x.Thua));
                    data.Add("TongThieu", lstData.Sum(x => x.Thieu));
                    data.Add("TongQuyetToan", lstData.Sum(x => x.QuyetToan));
                    data.Add("YearWork", yearOfWork);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Year", yearOfWork);
                    data.Add("Quarter", QuarterMonthQTTSelected.DisplayItem);
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("DonViCap2", _sessionInfo.TenDonVi);
                    data.Add("DonViCap1", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    //Tính tổng cộng
                    AddChuKy(data, _typeChuky);
                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhThamDinhQuyetToanChiKCBQuanYDonViQuery>(templateFileName, data);
                    results.Add(new ExportResult("BÁO CÁO THẨM ĐỊNH QUYẾT TOÁN NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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

        private void ExportQuyetToanKinhPhiCheDoBHXH(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.IIDMaDonVi).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    var lstData = _bhThamDinhQuyetToanChiTietService.GetChiKinhPhiCheDoBHXH(_sessionService.Current.YearOfWork, string.Join(",", lstIdDonVi), donViTinh).ToList();
                    //CalculateData(lstData);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    var tongSoTien = lstData.Where(x => x.TenDonVi.StartsWith("A") || x.TenDonVi.StartsWith("B")).Sum(x => x.Cong);
                    data.Add("ListData", lstData);
                    data.Add("TongCong", tongSoTien);
                    data.Add("TongCongBangChu", StringUtils.NumberToText(tongSoTien * donViTinh) ?? "");
                    data.Add("TongOmDau", lstData.Where(x => x.TenDonVi.StartsWith("A") || x.TenDonVi.StartsWith("B")).Sum(x => x.OmDau));
                    data.Add("TongThaiSan", lstData.Where(x => x.TenDonVi.StartsWith("A") || x.TenDonVi.StartsWith("B")).Sum(x => x.ThaiSan));
                    data.Add("TongPhucVien", lstData.Where(x => x.TenDonVi.StartsWith("A") || x.TenDonVi.StartsWith("B")).Sum(x => x.PhucVien));
                    data.Add("TongHuuTri", lstData.Where(x => x.TenDonVi.StartsWith("A") || x.TenDonVi.StartsWith("B")).Sum(x => x.HuuTri));
                    data.Add("TongTuTuat", lstData.Where(x => x.TenDonVi.StartsWith("A") || x.TenDonVi.StartsWith("B")).Sum(x => x.TuTuat));
                    data.Add("TongTNLDBNN", lstData.Where(x => x.TenDonVi.StartsWith("A") || x.TenDonVi.StartsWith("B")).Sum(x => x.TNLDBNN));
                    data.Add("TongXuatNgu", lstData.Where(x => x.TenDonVi.StartsWith("A") || x.TenDonVi.StartsWith("B")).Sum(x => x.XuatNgu));
                    data.Add("TongThoiViec", lstData.Where(x => x.TenDonVi.StartsWith("A") || x.TenDonVi.StartsWith("B")).Sum(x => x.ThoiViec));

                    data.Add("YearWork", yearOfWork);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Year", yearOfWork);
                    data.Add("Quarter", QuarterMonthQTTSelected.DisplayItem);
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("DonViCap2", _sessionInfo.TenDonVi);
                    data.Add("DonViCap1", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    //Tính tổng cộng
                    AddChuKy(data, _typeChuky);
                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhThamDinhQuyetToanChiCheDoBHXHQuery>(templateFileName, data);
                    results.Add(new ExportResult("BÁO CÁO THẨM ĐỊNH QUYẾT TOÁN NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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

        private void ExportQuyetToanKinhPhiTTBYT(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.IIDMaDonVi).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    var lstData = _bhThamDinhQuyetToanChiTietService.GetChiKinhPhiTTBYT(_sessionService.Current.YearOfWork, string.Join(",", lstIdDonVi), donViTinh).ToList();
                    //CalculateData(lstData);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    var tongSoTien = lstData.Sum(x => x.QuyetToan);
                    ExtensionMethods.CheckPassElementOrGetDefault10Element(lstData);
                    int stt = 1;
                    foreach (var i in lstData.Where(x => x.HasData.GetValueOrDefault()))
                    {
                        i.STT = stt++;
                    }

                    data.Add("ListData", lstData);
                    data.Add("TongSoTien", tongSoTien);
                    data.Add("TongCongBangChu", StringUtils.NumberToText(tongSoTien * donViTinh) ?? "");
                    data.Add("TongDuToan", lstData.Sum(x => x.TongDuToan));
                    data.Add("TongDuToanNamTruoc", lstData.Sum(x => x.DuToanNamTruoc));
                    data.Add("TongDuToanNamNay", lstData.Sum(x => x.DuToanNamNay));
                    data.Add("TongThua", lstData.Sum(x => x.Thua));
                    data.Add("TongThieu", lstData.Sum(x => x.Thieu));
                    data.Add("TongQuyetToan", lstData.Sum(x => x.QuyetToan));
                    data.Add("YearWork", yearOfWork);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Year", yearOfWork);
                    data.Add("Quarter", QuarterMonthQTTSelected.DisplayItem);
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("DonViCap2", _sessionInfo.TenDonVi);
                    data.Add("DonViCap1", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    //Tính tổng cộng
                    AddChuKy(data, _typeChuky);
                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhThamDinhQuyetToanChiTTBYTQuery>(templateFileName, data);
                    results.Add(new ExportResult("BÁO CÁO THẨM ĐỊNH QUYẾT TOÁN NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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


        private void CalculateParent(IEnumerable<BhThamDinhQuyetToanChiTietModel> listChild, IEnumerable<BhThamDinhQuyetToanChiTietModel> listOrigin)
        {
            if (listChild == null || !listChild.Any())
                return;
            var temp = listChild.GroupBy(x => x.IMaCha);
            var listParent = temp.Select(x =>
            {
                var parent = listOrigin.FirstOrDefault(y => !y.IsDeleted && y.IsFilter && y.IMa == x.Key);
                if (parent == null)
                    return null;
                parent.FSoBaoCao = x.Sum(x => x.FSoBaoCao);
                parent.FSoThamDinh = x.Sum(x => x.FSoThamDinh);
                parent.FQuanNhan = x.Sum(x => x.FQuanNhan);
                parent.FCNVLDHD = x.Sum(x => x.FCNVLDHD);
                return parent;
            }).Where(x => x != null).ToList();

            CalculateParent(listParent, listOrigin);
        }

        private void CalculateData(List<BhQttBHXHChiTietQuery> lstChungTuChiTiet)
        {
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
        }

        private void LoadTypeChuKy()
        {
            switch (SettlementTypeValue)
            {
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_MUA_SAM_TTBYT:
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.BH_THAM_DINH_QUYET_TOAN_CHI_MUA_SAM_TTBYT) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    _typeChuky = TypeChuKy.BH_THAM_DINH_QUYET_TOAN_CHI_MUA_SAM_TTBYT;
                    Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : "Phụ lục X";
                    Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : $"CHỈ TIÊU VÀ QUYẾT TOÁN CHI MUA SẮM TRANG THIẾT BỊ Y TẾ NĂM {_sessionInfo.YearOfWork}";
                    Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : $"(Kèm theo quyết định số: ......../QĐ-BQP ngày ...../...../...... của Bộ trưởng Bộ Quốc phòng)";
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_HSSV_NLD:
                    if (SelectedReportType.ValueItem == LoaiBaoCao.BaoHiemThamDinhQuyetToanChiCsskHssv)
                    {
                        _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.BH_THAM_DINH_QUYET_TOAN_CHI_CSSK_HSSV) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                        _typeChuky = TypeChuKy.BH_THAM_DINH_QUYET_TOAN_CHI_CSSK_HSSV;
                        Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : "Phụ lục XIII";
                        Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : $"CHỈ TIÊU VÀ QUYẾT TOÁN CHI KP CSSK BAN ĐẦU HỌC SINH, SINH VIÊN NĂM {_sessionInfo.YearOfWork}";
                        Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : $"(Kèm theo quyết định số: ......../QĐ-BQP ngày ...../...../...... của Bộ trưởng Bộ Quốc phòng)";
                    }
                    else if (SelectedReportType.ValueItem == LoaiBaoCao.BaoHiemThamDinhQuyetToanChiCsskNld)
                    {
                        _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.BH_THAM_DINH_QUYET_TOAN_CHI_CSSK_NLD) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                        _typeChuky = TypeChuKy.BH_THAM_DINH_QUYET_TOAN_CHI_CSSK_NLD;
                        Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : "Phụ lục IX";
                        Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : $"CHỈ TIÊU VÀ QUYẾT TOÁN CHI KP CSSK BAN ĐẦU NGƯỜI LAO ĐỘNG NĂM {_sessionInfo.YearOfWork}";
                        Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : $"(Kèm theo quyết định số: ......../QĐ-BQP ngày ...../...../...... của Bộ trưởng Bộ Quốc phòng)";
                    }
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_QUAN_Y_DON_VI:
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.BH_THAM_DINH_QUYET_TOAN_CHI_KCB_QYDV) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    _typeChuky = TypeChuKy.BH_THAM_DINH_QUYET_TOAN_CHI_KCB_QYDV;
                    Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : "Phụ lục XII";
                    Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : $"CHỈ TIÊU VÀ QUYẾT TOÁN CHI KHÁM CHỮA BỆNH TẠI QUÂN Y ĐƠN VỊ NĂM {_sessionInfo.YearOfWork}";
                    Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : $"(Kèm theo quyết định số: ......../QĐ-BQP ngày ...../...../...... của Bộ trưởng Bộ Quốc phòng)";
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_CHI_CHE_DO_BHXH:
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.BH_THAM_DINH_QUYET_TOAN_CHI_CHE_DO_BHXH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    _typeChuky = TypeChuKy.BH_THAM_DINH_QUYET_TOAN_CHI_CHE_DO_BHXH;
                    Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : "Phụ lục VIII";
                    Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : $"QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BẢO HIỂM XÃ HỘI NĂM {_sessionInfo.YearOfWork}";
                    Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : $"(Kèm theo quyết định số: ......../QĐ-BQP ngày ...../...../...... của Bộ trưởng Bộ Quốc phòng)";
                    break;
                default:
                    break;
            }
        }
    }
}
