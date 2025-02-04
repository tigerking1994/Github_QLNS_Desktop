using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.CanCu;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Plan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.App.Model.PlanBeginYearModel;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan
{
    public class PlanBeginYearDetailViewModel : DetailViewModelBase<PlanBeginYearModel, SktSoLieuChiTietMLNSModel>
    {
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly ISktSoLieuChiTietCanCuDataService _sktSoLieuChiTietCanCuDataService;
        private readonly ISktSoLieuChiTietCanCuService _sktSoLieuChiTietCanCuService;
        private readonly ISktMucLucService _sktMucLucService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly ICauHinhCanCuService _iCauHinhCanCuService;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ISktSoLieuChungTuService _sktChungTuService;
        private readonly INsDtChungTuService _iDtChungTuService;
        private readonly INsQtChungTuService _iQtChungTuService;
        private readonly ISktChungTuService _iSktChungTuService;
        private readonly ISoLieuChiTietPhanCapService _soLieuChiTietPhanCapService;

        private ICollectionView _searchPopupView;
        private readonly IMapper _mapper;
        private ICollectionView _dataDetailFilter;
        private ICollectionView _dataMuclucFilter;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungLnsService _nsNguoiDungLNSService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private List<SktSoLieuChiTietMlnsQuery> _dataDetailPlanBeginYear = new List<SktSoLieuChiTietMlnsQuery>();
        private List<SktSoLieuChiTietMlnsQuery> _dataDetailPlanBeginYearDynamic;
        private ICollectionView _budgetCatalogFilter;

        private int _countLoop;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        public event DataChangedEventHandler RefeshIndexWindow;
        public bool LoadComboboxDone { get; set; } = false;

        public override Type ContentType => typeof(PlanBeginYearDetail);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;

        public bool IsSaveData => (Items != null && Items.Any(item => item.IsModified || item.IsDeleted || item.IsUpdateCanCu || (LstCanCu != null && LstCanCu.Where(n => !n.IsSaved).Count() > 0)) && !IsLocked);
        public bool IsEnableButtonDelete => SelectedItem != null && !SelectedItem.IsHangCha && !IsReadOnlyTable;
        public bool IsEnableButtonCanCu => !IsReadOnlyTable;
        public PrintReportChiTietDuToanDonViViewModel PrintReportChiTietDuToanDonViViewModel { get; }
        public PlanBeginYearDetailChildViewModel PlanBeginYearDetailChildViewModel { get; }
        public PrintReportCompareDemandCheckViewModel PrintReportCompareDemandCheckViewModel { get; }
        public TongHopCanCuDuToanDauNamViewModel TongHopCanCuDuToanDauNamViewModel { get; }

        public string HeaderQuyetToan => "Quyết toán " + (_sessionService.Current.YearOfWork - 2);
        public string HeaderDuToan => "Dự toán " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderThucHien => "Ước thực hiện " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderChiTiet => "Chi tiết " + _sessionService.Current.YearOfWork;
        public string HeaderMucMuc => "Số kiểm tra " + _sessionService.Current.YearOfWork;
        public bool IsDeleteAll => Items.Any(item => !item.IsModified && item.HasData);

        private ObservableCollection<SktMucLucDuToanDauNamModel> _dataMucLuc;
        public ObservableCollection<SktMucLucDuToanDauNamModel> DataMucLuc
        {
            get => _dataMucLuc;
            set => SetProperty(ref _dataMucLuc, value);
        }

        private SktMucLucDuToanDauNamModel _selectedMucLuc;
        public SktMucLucDuToanDauNamModel SelectedMucLuc
        {
            get => _selectedMucLuc;
            set
            {
                if (SetProperty(ref _selectedMucLuc, value) && _dataDetailFilter != null && LoadComboboxDone)
                {
                    GetListParentFilter();
                    _dataDetailFilter.Refresh();
                    CalculateData();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _typeShowAgency;
        public ObservableCollection<ComboboxItem> TypeShowAgency
        {
            get => _typeShowAgency;
            set => SetProperty(ref _typeShowAgency, value);
        }

        private ComboboxItem _selectedTypeShowAgency;
        public ComboboxItem SelectedTypeShowAgency
        {
            get => _selectedTypeShowAgency;
            set
            {
                if (SetProperty(ref _selectedTypeShowAgency, value) && _selectedTypeShowAgency != null)
                {
                    LoadData();
                    LoadCanCu();
                    CalculateData();
                    OnPropertyChanged(nameof(IsShowTypeAgencyDetail));
                }
            }
        }

        private ObservableCollection<ComboboxItem> _typeDisplays;
        public ObservableCollection<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private string _selectedTypeDisplays;
        public string SelectedTypeDisplays
        {
            get => _selectedTypeDisplays;
            set
            {
                if (SetProperty(ref _selectedTypeDisplays, value) && _dataDetailFilter != null && LoadComboboxDone)
                {
                    GetListParentFilter();
                    _dataDetailFilter.Refresh();
                    CalculateData();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _typeDisplaysMucLuc;
        public ObservableCollection<ComboboxItem> TypeDisplaysMucLuc
        {
            get => _typeDisplaysMucLuc;
            set => SetProperty(ref _typeDisplaysMucLuc, value);
        }

        private string _selectedTypeDisplaysMucLuc;
        public string SelectedTypeDisplaysMucLuc
        {
            get => _selectedTypeDisplaysMucLuc;
            set
            {
                if (SetProperty(ref _selectedTypeDisplaysMucLuc, value) && _dataMuclucFilter != null)
                {
                    if (_dataMuclucFilter != null && _selectedTypeDisplaysMucLuc != null)

                    {
                        GetListParentSktSearch();
                        _dataMuclucFilter.Refresh();
                        UpdateSelectedSktSearch();
                    }
                    if (DataMucLuc != null && DataMucLuc.Count > 0)
                    {
                        SelectedMucLuc = DataMucLuc.FirstOrDefault();
                    }
                }
            }
        }

        private void GetListParentSktSearch()
        {
            ListParentSktSearch = !string.IsNullOrEmpty(SktSKyHieuSearch) ? StringUtils.GetListKyHieuParent(new List<string> { SktSKyHieuSearch }) : new List<string>();
            List<string> listChildSktSearch = DataMucLuc.Where(n => n.KyHieu != null && SktSKyHieuSearch != null && n.KyHieu.StartsWith(SktSKyHieuSearch))?.Select(n => n.KyHieu).ToList();
            if (listChildSktSearch != null && ListParentSktSearch != null)
            {
                ListParentSktSearch.AddRange(listChildSktSearch);
            }
        }

        private void UpdateSelectedSktSearch()
        {
            if (DataMucLuc != null && DataMucLuc.Count > 0)
            {
                SelectedMucLuc = string.IsNullOrEmpty(SktSKyHieuSearch)
                    ? DataMucLuc.FirstOrDefault()
                    : DataMucLuc.FirstOrDefault(n => n.KyHieu == SktSKyHieuSearch && n.IsFilter);
                OnPropertyChanged(nameof(SelectedMucLuc));
            }
        }

        private bool _isReadOnlyTable;
        public bool IsReadOnlyTable
        {
            get => _isReadOnlyTable;
            set => SetProperty(ref _isReadOnlyTable, value);
        }

        public bool IsShowTypeAgency => Model.Id_DonVi == _sessionService.Current.IdDonVi && !string.IsNullOrWhiteSpace(Model.DSSoChungTuTongHop);
        public bool IsShowTypeAgencyDetail => IsShowTypeAgency && (SelectedTypeShowAgency?.ValueItem == TypeDisplay.CHITIET_DONVI);

        private bool _isLocked;
        public bool IsLocked
        {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }

        private ObservableCollection<ComboboxItem> _loaiNganSach;
        public ObservableCollection<ComboboxItem> LoaiNganSach
        {
            get => _loaiNganSach;
            set => SetProperty(ref _loaiNganSach, value);
        }

        private ComboboxItem _selectedLoaiNganSach;
        public ComboboxItem SelectedLoaiNganSach
        {
            get => _selectedLoaiNganSach;
            set
            {
                if (SetProperty(ref _selectedLoaiNganSach, value) && _dataDetailFilter != null
                    && LoaiChungTu != null && LoaiChungTu == VoucherType.NSBD_Key && LoadComboboxDone)
                {
                    GetListParentFilter();
                    _dataDetailFilter.Refresh();
                    CalculateData();
                }
            }
        }

        private string _loaiChungTu;
        public string LoaiChungTu
        {
            get => _loaiChungTu;
            set => SetProperty(ref _loaiChungTu, value);
        }

        private ObservableCollection<CauHinhCanCuModel> _lstCanCu;
        public ObservableCollection<CauHinhCanCuModel> LstCanCu
        {
            get => _lstCanCu;
            set => SetProperty(ref _lstCanCu, value);
        }

        private ObservableCollection<CauHinhCanCuModel> _lstCanCuInit;
        public ObservableCollection<CauHinhCanCuModel> LstCanCuInit
        {
            get => _lstCanCuInit;
            set => SetProperty(ref _lstCanCuInit, value);
        }

        private List<NguoiDungDonVi> _listNguoiDungDonVi;
        public List<NguoiDungDonVi> ListNguoiDungDonVi
        {
            get => _listNguoiDungDonVi;
            set => SetProperty(ref _listNguoiDungDonVi, value);
        }

        private string _popupSearchText;
        public string PopupSearchText
        {
            set
            {
                SetProperty(ref _popupSearchText, value);
                _searchPopupView?.Refresh();
            }
            get => _popupSearchText;
        }

        private ObservableCollection<SktMucLucModel> _sktMucLucModelItems;
        public ObservableCollection<SktMucLucModel> SktMucLucModelItems
        {
            get => _sktMucLucModelItems;
            set => SetProperty(ref _sktMucLucModelItems, value);
        }

        private List<string> _listParentSktSearch;
        public List<string> ListParentSktSearch
        {
            get => _listParentSktSearch;
            set => SetProperty(ref _listParentSktSearch, value);
        }

        private string _sktSKyHieuSearch;
        public string SktSKyHieuSearch
        {
            get => _sktSKyHieuSearch;
            set => SetProperty(ref _sktSKyHieuSearch, value);
        }

        private SktMucLucModel _selectedPopupItem;
        public SktMucLucModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SktSKyHieuSearch = _selectedPopupItem?.SKyHieu;
                OnPropertyChanged(nameof(SktSKyHieuSearch));
                IsPopupOpen = false;
            }
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        public Visibility VisibleNganSachSuDung => LoaiChungTu == VoucherType.NSSD_Key ? Visibility.Visible : Visibility.Collapsed;
        public Visibility VisibleNganSachDamBao => LoaiChungTu == VoucherType.NSBD_Key ? Visibility.Visible : Visibility.Collapsed;

        public List<SktSoLieuChiTietMLNSModel> ListParentFilter;
        public List<SktSoLieuChiTietMLNSModel> ListChildFilter;
        public List<SktMucLucDuToanDauNamModel> ListMucLucSelected;

        public Visibility TC1Visible { get; set; }
        public Visibility TC2Visible { get; set; }
        public Visibility TC3Visible { get; set; }
        public Visibility TC4Visible { get; set; }
        public Visibility TC5Visible { get; set; }

        public Visibility HangNhap1Visible { get; set; }
        public Visibility HangMua1Visible { get; set; }
        public Visibility PhanCap1Visible { get; set; }

        public Visibility HangNhap2Visible { get; set; }
        public Visibility HangMua2Visible { get; set; }
        public Visibility PhanCap2Visible { get; set; }

        public Visibility HangNhap3Visible { get; set; }
        public Visibility HangMua3Visible { get; set; }
        public Visibility PhanCap3Visible { get; set; }

        public Visibility HangNhap4Visible { get; set; }
        public Visibility HangMua4Visible { get; set; }
        public Visibility PhanCap4Visible { get; set; }

        public Visibility HangNhap5Visible { get; set; }
        public Visibility HangMua5Visible { get; set; }
        public Visibility PhanCap5Visible { get; set; }

        public bool IsReadOnlyX1 { get; set; }
        public bool IsReadOnlyX2 { get; set; }
        public bool IsReadOnlyX3 { get; set; }
        public bool IsReadOnlyX4 { get; set; }
        public bool IsReadOnlyX5 { get; set; }

        public string TC1 { get; set; }
        public string TC2 { get; set; }
        public string TC3 { get; set; }
        public string TC4 { get; set; }
        public string TC5 { get; set; }

        public string HangNhap1 { get; set; }
        public string HangMua1 { get; set; }
        public string PhanCap1 { get; set; }

        public string HangNhap2 { get; set; }
        public string HangMua2 { get; set; }
        public string PhanCap2 { get; set; }

        public string HangNhap3 { get; set; }
        public string HangMua3 { get; set; }
        public string PhanCap3 { get; set; }

        public string HangNhap4 { get; set; }
        public string HangMua4 { get; set; }
        public string PhanCap4 { get; set; }

        public string HangNhap5 { get; set; }
        public string HangMua5 { get; set; }
        public string PhanCap5 { get; set; }
        public bool IsInit { get; set; }
        public bool IsGetCanCu { get; set; } = false;
        public bool IsCanCu { get; set; }

        public int IndexCheck { get; set; }
        public Dictionary<string, string> DictionaryCanCu => new Dictionary<string, string>()
        {
            [TypeCanCu.SETTLEMENT] = "1",
            [TypeCanCu.ESTIMATE] = "2",
            [TypeCanCu.ALLOCATION] = "3",
            [TypeCanCu.DEMAND] = "4",
            [TypeCanCu.CHECK_NUMBER] = "5",
        };

        public Dictionary<string, int> DictionaryLoaiCanCu => new Dictionary<string, int>()
        {
            [TypeCanCu.ESTIMATE] = 1,
            [TypeCanCu.SETTLEMENT] = 2,
            [TypeCanCu.ALLOCATION] = 3,
            [TypeCanCu.DEMAND] = 4,
            [TypeCanCu.CHECK_NUMBER] = 5,
        };

        private AllocationDetailFilterModel _mLNSDetailFilter;
        public AllocationDetailFilterModel MLNSDetailFilter
        {
            get => _mLNSDetailFilter;
            set => SetProperty(ref _mLNSDetailFilter, value);
        }

        private string _selectedLNS;
        public string SelectedLNS
        {
            get => _selectedLNS;
            set => SetProperty(ref _selectedLNS, value);
        }

        private bool _isOpenLnsPopup;
        public bool IsOpenLnsPopup
        {
            get => _isOpenLnsPopup;
            set
            {
                SetProperty(ref _isOpenLnsPopup, value);
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _budgetCatalogFilter.Refresh();
                }
            }
        }

        private ObservableCollection<NsMuclucNgansachModel> _budgetCatalogItems;
        public ObservableCollection<NsMuclucNgansachModel> BudgetCatalogItems
        {
            get => _budgetCatalogItems;
            set => SetProperty(ref _budgetCatalogItems, value);
        }

        private NsMuclucNgansachModel _selectedBudgetCatalog;
        public NsMuclucNgansachModel SelectedBudgetCatalog
        {
            get => _selectedBudgetCatalog;
            set
            {
                if (SetProperty(ref _selectedBudgetCatalog, value))
                {
                    OnSearch();
                }
                if (_selectedBudgetCatalog != null)
                    SelectedLNS = _selectedBudgetCatalog.Lns;
                IsOpenLnsPopup = false;
            }
        }

        public int NamLamViec { get; set; }

        public RelayCommand SaveDataCommand { get; }
        public RelayCommand SelectionChangedMucLucCommand { get; }
        public RelayCommand RefreshMucLucDataCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand ClosingWindowCommand { get; }
        public RelayCommand ShowPopupReportChiTietCommand { get; }

        public RelayCommand ShowPopupChildCommand { get; }
        public RelayCommand ShowPopupReportCompareCommand { get; }
        public RelayCommand CanCuCommand { get; }
        public RelayCommand SearchSktCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand CopyDuToanNamTruocCommand { get; }
        public RelayCommand GetDataUocThucHienCommand { get; }
        public PlanBeginYearDetailViewModel(ISktSoLieuService sktSoLieuService,
           ISktMucLucService sktMucLucService,
           ISktSoLieuChungTuService sktChungTuService,
           IMapper mapper,
           ISessionService sessionService,
           IDanhMucService danhMucService,
           INsDonViService nsDonViService,
           INsMucLucNganSachService nsMucLucNganSachService,
           ICauHinhCanCuService iCauHinhCanCuService,
           ISktSoLieuChiTietCanCuDataService sktSoLieuChiTietCanCuDataService,
           ISktChungTuChiTietService sktChungTuChiTietService,
           ISktSoLieuChiTietCanCuService sktSoLieuChiTietCanCuService,
           ISktChungTuService iSktChungTuService,
           INsNguoiDungLnsService nsNguoiDungLNSService,
           INsDtChungTuService iDtChungTuService,
           INsQtChungTuService iQtChungTuService,
           INsNguoiDungDonViService nsNguoiDungDonViService,
           ISoLieuChiTietPhanCapService soLieuChiTietPhanCapService,
           ILog logger,
           PrintReportChiTietDuToanDonViViewModel printReportChiTietDuToanDonViViewModel,
           PrintReportCompareDemandCheckViewModel printReportCompareDemandCheckViewModel,
           TongHopCanCuDuToanDauNamViewModel tongHopCanCuDuToanDauNamViewModel,
           PlanBeginYearDetailChildViewModel planBeginYearDetailChildViewModel) : base(danhMucService, sessionService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _sktSoLieuService = sktSoLieuService;
            _sktMucLucService = sktMucLucService;
            _danhMucService = danhMucService;
            _iDtChungTuService = iDtChungTuService;
            _iQtChungTuService = iQtChungTuService;
            _nsDonViService = nsDonViService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _iCauHinhCanCuService = iCauHinhCanCuService;
            _sktSoLieuChiTietCanCuDataService = sktSoLieuChiTietCanCuDataService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _sktSoLieuChiTietCanCuService = sktSoLieuChiTietCanCuService;
            _sktChungTuService = sktChungTuService;
            _iSktChungTuService = iSktChungTuService;
            _nsNguoiDungLNSService = nsNguoiDungLNSService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _soLieuChiTietPhanCapService = soLieuChiTietPhanCapService;
            SaveDataCommand = new RelayCommand(obj => OnSave());
            RefreshMucLucDataCommand = new RelayCommand(obj => OnRefreshMucLucData());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            ClosingWindowCommand = new RelayCommand(obj => OnRefeshIndexWindow());
            ShowPopupReportChiTietCommand = new RelayCommand(obj => OnShowPopupReportChiTiet(obj));
            ShowPopupChildCommand = new RelayCommand(o => OnShowPopupChild());
            ShowPopupReportCompareCommand = new RelayCommand(obj => OnShowPopupReportCompare(obj));
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SearchCommand = new RelayCommand(_ => OnSearch());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            SearchSktCommand = new RelayCommand(obj =>
            {
                OnRefeshFilterSkt();
            });
            PrintReportChiTietDuToanDonViViewModel = printReportChiTietDuToanDonViViewModel;
            PlanBeginYearDetailChildViewModel = planBeginYearDetailChildViewModel;
            PrintReportCompareDemandCheckViewModel = printReportCompareDemandCheckViewModel;
            TongHopCanCuDuToanDauNamViewModel = tongHopCanCuDuToanDauNamViewModel;
            CanCuCommand = new RelayCommand(obj => OnCanCu());
            CopyDuToanNamTruocCommand = new RelayCommand(obj => CopyDuToanNamTruocIntoUocThucHien());
            GetDataUocThucHienCommand = new RelayCommand(obj => GetDataUocThucHien(obj));
        }

        private void OnSearch()
        {
            GetListParentFilter();
            _dataDetailFilter.Refresh();
            CalculateData();
        }

        private void LoadBudgetIndexCondition()
        {
            List<NsMuclucNgansachModel> listBudgetCatalog = new List<NsMuclucNgansachModel>();
            BudgetIndexForBudgetCriteria searchCondition = new BudgetIndexForBudgetCriteria
            {
                LNS = string.Join(",", Model != null ? Model.DsLNS : string.Empty),
                YearOfWork = _sessionService.Current.YearOfWork,
                GenerateAgencyId = _sessionService.Current.IdDonVi
            };
            List<NsMucLucNganSach> listMucLucNganSach = _nsMucLucNganSachService.FindByDefenseBudget(searchCondition);
            listMucLucNganSach = listMucLucNganSach.GroupBy(n => n.XauNoiMa).Select(n => n.First()).ToList();
            listBudgetCatalog = _mapper.Map<List<NsMuclucNgansachModel>>(listMucLucNganSach);
            listBudgetCatalog.Insert(0, new NsMuclucNgansachModel(string.Empty, "-- TẤT CẢ --"));
            BudgetCatalogItems = new ObservableCollection<NsMuclucNgansachModel>(listBudgetCatalog);
            _budgetCatalogFilter = CollectionViewSource.GetDefaultView(BudgetCatalogItems);
            _budgetCatalogFilter.Filter = OnBudgetCatalogFilter;
        }

        private bool OnBudgetCatalogFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.Lns.ToLower().Contains(_searchLNS!.ToLower());
        }

        private void OnRemoveFilter()
        {
            MLNSDetailFilter.L = string.Empty;
            MLNSDetailFilter.K = string.Empty;
            MLNSDetailFilter.M = string.Empty;
            MLNSDetailFilter.TM = string.Empty;
            MLNSDetailFilter.TTM = string.Empty;
            MLNSDetailFilter.NG = string.Empty;
            MLNSDetailFilter.TNG = string.Empty;
            MLNSDetailFilter.TNG1 = string.Empty;
            MLNSDetailFilter.TNG2 = string.Empty;
            MLNSDetailFilter.TNG3 = string.Empty;
            SelectedLNS = string.Empty;
            OnSearch();
        }

        private void OnClearSearch(object obj)
        {
            SktSKyHieuSearch = string.Empty;
            OnPropertyChanged(nameof(SktSKyHieuSearch));
            GetListParentSktSearch();
            _dataMuclucFilter.Refresh();
            SelectedMucLuc = (DataMucLuc != null && DataMucLuc.Count > 0) ? DataMucLuc.FirstOrDefault() : null;
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            MessageBoxResult result = MessageBoxHelper.Confirm(Resources.DeleteAllChungTuChiTiet);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                Guid chungTuId = AddChungTu();
                // _sktSoLieuService.DeleteByVoucherId(chungTuId);
                List<SktSoLieuChiTietMLNSModel> listDelete = Items.Where(x => x.IdDb != Guid.Empty && x.IdDb != null && x.IsFilter).ToList();
                if (listDelete.Count > 0)
                {
                    foreach (SktSoLieuChiTietMLNSModel item in listDelete)
                    {
                        _sktSoLieuService.Delete(item.Id);
                    }
                }

                _sktChungTuService.UpdateTotalChungTu(Model.Id_DonVi, Model.Loai,
                    int.Parse(LoaiChungTu), _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, Model.ILoaiNguonNganSach ?? 0, _sessionService.Current.Budget);
                LoadData();
                MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void OnRefreshMucLucData()
        {
            if (IsSaveData)
            {
                string message = Resources.MsgConfirmEdit;
                MessageBoxResult confirm = MessageBoxHelper.ConfirmCancel(message);

                if (confirm == MessageBoxResult.Cancel) return;
                else if (confirm == MessageBoxResult.Yes) OnSave();

                LoadDataMucLuc();
                LoadData();
            }
            else
            {
                IsInit = true;
                LoadDataMucLuc();
                LoadData();
                CalculateMucLuc();
                IsInit = false;
            }
        }

        private void OnCanCu()
        {
            TongHopCanCuDuToanDauNamViewModel.SktChungTuModel = Model;
            TongHopCanCuDuToanDauNamViewModel.CauHinhCanCuTemp = LstCanCu;
            TongHopCanCuDuToanDauNamViewModel.ListIdNhomNganhSelected = null;
            TongHopCanCuDuToanDauNamViewModel.ListIdMlnsSelected = null;
            TongHopCanCuDuToanDauNamViewModel.LoaiChungTu = LoaiChungTu;
            TongHopCanCuDuToanDauNamViewModel.Init();
            TongHopCanCuDuToanDauNamViewModel.SavedAction = obj =>
            {
                //LoadData();
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    LoadCanCu((ObservableCollection<CauHinhCanCuModel>)obj);
                }, (s, e) =>
                {
                    IsLoading = false;
                });
            };

            TongHopCanCuDuToanDauNamViewModel.ShowDialogHost("PlanBeginYearDetailDialog");
        }

        private void RefeshMucLucHandler(NSDialogResult result)
        {
            if (result == NSDialogResult.Cancel) return;
            if (result == NSDialogResult.Yes)
            {
                OnSave();
            }
            LoadDataMucLuc();
            LoadData();
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsEnableButtonDelete));
        }

        protected override void OnRefresh()
        {
            if (IsSaveData)
            {
                string message = Resources.MsgConfirmEdit;
                //var messageBox = new NSMessageBoxViewModel(message, Resources.ConfirmTitle, NSMessageBoxButtons.YesNoCancel, RefeshDataHandler);
                //DialogHost.Show(messageBox.Content, "PlanBeginYearDetailDialog");
                MessageBoxResult confirm = MessageBoxHelper.ConfirmCancel(message);
                if (confirm == MessageBoxResult.Cancel) return;
                else if (confirm == MessageBoxResult.Yes) OnSave();
                LoadData();
                LoadCanCu();
                CalculateData();
            }
            else
            {
                IsInit = true;
                LoadData();
                LoadCanCu();
                CalculateData();
                IsInit = false;
            }
        }

        private void RefeshDataHandler(NSDialogResult result)
        {
            if (result == NSDialogResult.Cancel) return;
            if (result == NSDialogResult.Yes)
            {
                OnSave();
            }
            LoadData();
        }

        public override void OnSave()
        {
            try
            {
                List<SktSoLieuChiTietMLNSModel> listAdd = Items.Where(x => x.IsModified && (x.IdDb == Guid.Empty || x.IdDb == null) && !x.IsDeleted).ToList();
                List<SktSoLieuChiTietMLNSModel> listUpdate = Items.Where(x => x.IsModified && x.IdDb != Guid.Empty && x.IdDb != null && !x.IsDeleted).ToList();
                List<SktSoLieuChiTietMLNSModel> listDelete = Items.Where(x => x.IsDeleted && x.IdDb != Guid.Empty && x.IdDb != null).ToList();

                //chung tu
                Guid chungTuId = AddChungTu();

                if (LoaiChungTu == VoucherType.NSSD_Key)
                {
                    if (listAdd.Count > 0)
                    {
                        List<NsDtdauNamChungTuChiTiet> listChiTiet = new List<NsDtdauNamChungTuChiTiet>();
                        listChiTiet = _mapper.Map<List<NsDtdauNamChungTuChiTiet>>(listAdd.Where(n => n.UocThucHien != 0 || n.ChiTiet != 0 || n.MucTienPhanBo != 0));
                        listChiTiet = listChiTiet.Select(x =>
                        {
                            x.ILoaiChungTu = LoaiChungTu;
                            x.INamNganSach = _sessionService.Current.YearOfBudget;
                            x.IIdMaNguonNganSach = _sessionService.Current.Budget;
                            x.INamLamViec = _sessionService.Current.YearOfWork;
                            x.ILoai = 3;
                            x.DNgayTao = DateTime.Now;
                            x.SNguoiTao = _sessionService.Current.Principal;
                            x.IIdMaDonVi = Model.Id_DonVi;
                            x.STenDonVi = Model.TenDonVi;
                            x.IIdCtdtdauNam = chungTuId;
                            return x;
                        }).ToList();
                        _sktSoLieuService.AddRange(listChiTiet);
                        listAdd.Select(n => { n.IdDb = n.Id; return n; }).ToList();

                        foreach (SktSoLieuChiTietMLNSModel add in listAdd)
                        {
                            //Update chứng từ chi tiết phân cấp
                            NsDtdauNamPhanCap phancapchitiet = _soLieuChiTietPhanCapService.FindAll().Where(x => x.IIdCtdtDauNam == chungTuId && x.sXauNoiMaGoc == add.XauNoiMa).FirstOrDefault();
                            if (phancapchitiet != null)
                            {
                                NsDtdauNamPhanCap chungtu_phancap = _mapper.Map<NsDtdauNamPhanCap>(phancapchitiet);
                                chungtu_phancap.IIdCtdtdauNamChiTiet = add.Id;
                                _soLieuChiTietPhanCapService.Update(chungtu_phancap);
                            }
                        }
                    }
                    if (listUpdate.Count > 0)
                    {
                        foreach (SktSoLieuChiTietMLNSModel item in listUpdate.Where(n => n.UocThucHien != 0 || n.ChiTiet != 0 || n.MucTienPhanBo != 0))
                        {
                            item.IsModified = false;
                            NsDtdauNamChungTuChiTiet solieuChiTiet = _sktSoLieuService.Find(item.Id);
                            if (solieuChiTiet != null)
                            {
                                solieuChiTiet.FUocThucHien = item.UocThucHien;
                                solieuChiTiet.FMucTienPhanBo = item.MucTienPhanBo;
                                solieuChiTiet.FTuChi = item.ChiTiet;
                                solieuChiTiet.FHangNhap = item.HangNhap;
                                solieuChiTiet.FHangMua = item.HangMua;
                                solieuChiTiet.FPhanCap = item.PhanCap;
                                solieuChiTiet.FChuaPhanCap = item.ChuaPhanCap;
                                solieuChiTiet.DNgaySua = DateTime.Now;
                                solieuChiTiet.SNguoiSua = _sessionService.Current.Principal;
                                _sktSoLieuService.Update(solieuChiTiet);

                                if (item.PhanCap == 0)
                                {
                                    List<NsDtdauNamPhanCap> listChiTiet = new List<NsDtdauNamPhanCap>();
                                    listChiTiet = _soLieuChiTietPhanCapService.FindAll().Where(x => x.IIdCtdtdauNamChiTiet == item.Id && x.INamLamViec == _sessionService.Current.YearOfWork).ToList();
                                    foreach (NsDtdauNamPhanCap child in listChiTiet)
                                    {
                                        child.FTuChi = 0;
                                        child.IIdCtdtDauNam = Model.Id;
                                        _soLieuChiTietPhanCapService.Update(child);
                                    }
                                }
                            }
                        }
                        foreach (SktSoLieuChiTietMLNSModel item in listUpdate.Where(n => n.UocThucHien == 0 && n.ChiTiet == 0 && n.MucTienPhanBo == 0))
                        {
                            _sktSoLieuService.Delete(item.Id);
                        }
                    }
                }
                else
                {
                    if (listAdd.Count > 0)
                    {
                        List<NsDtdauNamChungTuChiTiet> listChiTiet = new List<NsDtdauNamChungTuChiTiet>();
                        listChiTiet = _mapper.Map<List<NsDtdauNamChungTuChiTiet>>(listAdd.Where(n => n.HangNhap != 0 || n.HangMua != 0 || n.PhanCap != 0 || n.ChuaPhanCap != 0 || n.UocThucHien != 0 || n.MucTienPhanBo != 0));
                        listChiTiet = listChiTiet.Select(x =>
                        {
                            x.ILoaiChungTu = LoaiChungTu;
                            x.INamNganSach = _sessionService.Current.YearOfBudget;
                            x.IIdMaNguonNganSach = _sessionService.Current.Budget;
                            x.INamLamViec = _sessionService.Current.YearOfWork;
                            x.ILoai = 3;
                            x.DNgayTao = DateTime.Now;
                            x.SNguoiTao = _sessionService.Current.Principal;
                            x.IIdMaDonVi = Model.Id_DonVi;
                            x.STenDonVi = Model.TenDonVi;
                            x.IIdCtdtdauNam = chungTuId;
                            return x;
                        }).ToList();
                        _sktSoLieuService.AddRange(listChiTiet);
                        listAdd.Select(n => { n.IdDb = n.Id; return n; }).ToList();

                        foreach (SktSoLieuChiTietMLNSModel add in listAdd.Where(n => n.HangNhap != 0 || n.HangMua != 0 || n.PhanCap != 0 || n.ChuaPhanCap != 0 || n.UocThucHien != 0 || n.MucTienPhanBo != 0))
                        {
                            //Update chứng từ chi tiết phân cấp
                            NsDtdauNamPhanCap phancapchitiet = _soLieuChiTietPhanCapService.FindAll().Where(x => x.IIdCtdtDauNam == chungTuId && x.sXauNoiMaGoc == add.XauNoiMa).FirstOrDefault();
                            if (phancapchitiet != null)
                            {
                                NsDtdauNamPhanCap chungtu_phancap = _mapper.Map<NsDtdauNamPhanCap>(phancapchitiet);
                                chungtu_phancap.IIdCtdtdauNamChiTiet = add.Id;
                                _soLieuChiTietPhanCapService.Update(chungtu_phancap);
                            }
                        }
                    }
                    if (listUpdate.Count > 0)
                    {
                        foreach (SktSoLieuChiTietMLNSModel item in listUpdate.Where(n => n.HangNhap != 0 || n.HangMua != 0 || n.PhanCap != 0 || n.ChuaPhanCap != 0 || n.UocThucHien != 0 || n.MucTienPhanBo != 0))
                        {
                            item.IsModified = false;
                            NsDtdauNamChungTuChiTiet solieuChiTiet = _sktSoLieuService.Find(item.Id);
                            if (solieuChiTiet != null)
                            {
                                solieuChiTiet.FUocThucHien = item.UocThucHien;
                                solieuChiTiet.FMucTienPhanBo = item.MucTienPhanBo;
                                solieuChiTiet.FTuChi = item.ChiTiet;
                                solieuChiTiet.FHangNhap = item.HangNhap;
                                solieuChiTiet.FHangMua = item.HangMua;
                                solieuChiTiet.FPhanCap = item.PhanCap;
                                solieuChiTiet.FChuaPhanCap = item.ChuaPhanCap;
                                solieuChiTiet.DNgaySua = DateTime.Now;
                                solieuChiTiet.SNguoiSua = _sessionService.Current.Principal;
                                _sktSoLieuService.Update(solieuChiTiet);

                                if (item.PhanCap == 0)
                                {
                                    List<NsDtdauNamPhanCap> listChiTiet = new List<NsDtdauNamPhanCap>();
                                    listChiTiet = _soLieuChiTietPhanCapService.FindAll().Where(x => x.IIdCtdtdauNamChiTiet == item.Id && x.INamLamViec == _sessionService.Current.YearOfWork).ToList();
                                    foreach (NsDtdauNamPhanCap child in listChiTiet)
                                    {
                                        child.FTuChi = 0;
                                        child.IIdCtdtDauNam = Model.Id;
                                        _soLieuChiTietPhanCapService.Update(child);
                                    }
                                }
                            }
                        }
                        foreach (SktSoLieuChiTietMLNSModel item in listUpdate.Where(n => n.HangNhap == 0 && n.HangMua == 0 && n.PhanCap == 0 && n.ChuaPhanCap == 0 && n.UocThucHien == 0 && n.MucTienPhanBo == 0))
                        {
                            _sktSoLieuService.Delete(item.Id);
                        }
                    }

                }

                if (listDelete.Count > 0)
                {
                    foreach (SktSoLieuChiTietMLNSModel item in listDelete)
                    {
                        _sktSoLieuService.Delete(item.Id);
                    }
                }

                // lưu dữ liệu căn cứ nhập tay
                SaveCanCu();

                // cập nhật tổng số liệu chứng từ
                _sktChungTuService.UpdateTotalChungTu(Model.Id_DonVi, Model.Loai, int.Parse(LoaiChungTu),
                    _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, Model.ILoaiNguonNganSach ?? 0, Model.Id.ToString());

                OnPropertyChanged(nameof(IsSaveData));
                MessageBoxHelper.Info(Resources.MsgSaveDone);
                RefeshIndexWindow?.Invoke(this, new EventArgs());
                ReFreshDataItems();
                OnPropertyChanged(nameof(IsDeleteAll));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private Guid AddChungTu()
        {
            System.Linq.Expressions.Expression<Func<NsDtdauNamChungTu, bool>> predicate = PredicateBuilder.True<NsDtdauNamChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIdMaDonVi == Model.Id_DonVi);
            predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.Id == Model.Id);
            NsDtdauNamChungTu chungTu = _sktChungTuService.FindByCondition(predicate).FirstOrDefault();
            if (chungTu == null)
            {
                NsDtdauNamChungTu entity = new NsDtdauNamChungTu();
                entity.IIdMaDonVi = Model.Id_DonVi;
                entity.INamLamViec = _sessionService.Current.YearOfWork;
                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                entity.BKhoa = false;
                entity.ILoaiChungTu = int.Parse(LoaiChungTu);
                entity.INamNganSach = _sessionService.Current.YearOfBudget;
                entity.IIdMaNguonNganSach = _sessionService.Current.Budget;
                _sktChungTuService.Add(entity);
                return entity.Id;
            }
            else
            {
                return chungTu.Id;
            }
        }

        public void SaveCanCu()
        {
            List<SktSoLieuChiTietMLNSModel> detailsCanCu = Items.Where(x => !x.IsDeleted && !x.IsHangCha && x.IsUpdateCanCu).ToList();
            if (LstCanCu != null)
            {
                List<NsDtdauNamChungTuChungTuCanCu> listCanCuChungTus = new List<NsDtdauNamChungTuChungTuCanCu>();
                List<NsDtdauNamChungTuChiTietCanCu> listCanCuChiTiet = new List<NsDtdauNamChungTuChiTietCanCu>();

                // xoa can cu chung tu 
                System.Linq.Expressions.Expression<Func<NsDtdauNamChungTuChungTuCanCu, bool>> predicate = PredicateBuilder.True<NsDtdauNamChungTuChungTuCanCu>();
                predicate = predicate.And(x => x.IIdMaDonVi == Model.Id_DonVi);
                predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
                List<NsDtdauNamChungTuChungTuCanCu> sktcanCanCuChungTus = _sktSoLieuChiTietCanCuService.FindByCondition(predicate).ToList();
                //_sktSoLieuChiTietCanCuService.RemoveRange(sktcanCanCuChungTus);
                foreach (NsDtdauNamChungTuChungTuCanCu item in sktcanCanCuChungTus)
                {
                    _sktSoLieuChiTietCanCuService.Delete(item.Id);
                }
                int count = 0;
                foreach (CauHinhCanCuModel cc in LstCanCu)
                {
                    //xoa can cu chung tu chi tiet
                    System.Linq.Expressions.Expression<Func<NsDtdauNamChungTuChiTietCanCu, bool>> predicateCT = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
                    predicateCT = predicateCT.And(x => x.IIdCanCu == cc.Id);
                    predicateCT = predicateCT.And(x => x.IID_CTDTDauNam == Model.Id);
                    predicateCT = predicateCT.And(x => x.IIdMaDonVi == Model.Id_DonVi);
                    predicateCT = predicateCT.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
                    List<NsDtdauNamChungTuChiTietCanCu> sktcanCanCus = _sktSoLieuChiTietCanCuDataService.FindByCondition(predicateCT).ToList();

                    foreach (NsDtdauNamChungTuChiTietCanCu item in sktcanCanCus)
                    {
                        _sktSoLieuChiTietCanCuDataService.Delete(item.Id);
                    }

                    if (cc.LstIdChungTuCanCu != null && cc.LstIdChungTuCanCu.Count > 0)
                    {
                        if (cc.IThietLap != 3)
                        {
                            foreach (Guid idChungTu in cc.LstIdChungTuCanCu)
                            {
                                NsDtdauNamChungTuChungTuCanCu canCuChungTu = new NsDtdauNamChungTuChungTuCanCu();
                                canCuChungTu.IIdCtcanCu = idChungTu;
                                canCuChungTu.IIdCanCu = cc.Id;
                                canCuChungTu.ILoaiChungTu = int.Parse(LoaiChungTu);
                                canCuChungTu.IIdMaDonVi = Model.Id_DonVi;
                                canCuChungTu.INamLamViec = _sessionService.Current.YearOfWork;
                                listCanCuChungTus.Add(canCuChungTu);
                            }
                        }
                        else
                        {
                            NsDtdauNamChungTuChungTuCanCu canCuChungTu = new NsDtdauNamChungTuChungTuCanCu();
                            canCuChungTu.IIdCtcanCu = cc.IdChungTuCanCuLuyKe;
                            canCuChungTu.IIdCanCu = cc.Id;
                            canCuChungTu.ILoaiChungTu = int.Parse(LoaiChungTu);
                            canCuChungTu.IIdMaDonVi = Model.Id_DonVi;
                            canCuChungTu.INamLamViec = _sessionService.Current.YearOfWork;
                            listCanCuChungTus.Add(canCuChungTu);
                        }

                        foreach (SktSoLieuChiTietMLNSModel item in detailsCanCu)
                        {
                            NsDtdauNamChungTuChiTietCanCu canCuChungTu = new NsDtdauNamChungTuChiTietCanCu();
                            canCuChungTu = _mapper.Map<NsDtdauNamChungTuChiTietCanCu>(item);
                            canCuChungTu.Id = Guid.Empty;
                            canCuChungTu.IID_CTDTDauNam = Model.Id;
                            canCuChungTu.IIdCanCu = cc.Id;
                            canCuChungTu.ILoaiChungTu = int.Parse(LoaiChungTu);
                            canCuChungTu.INamNganSach = _sessionService.Current.YearOfBudget;
                            canCuChungTu.IIdMaNguonNganSach = _sessionService.Current.Budget;
                            canCuChungTu.INamLamViec = _sessionService.Current.YearOfWork;
                            //canCuChungTu.ILoai = 3;
                            canCuChungTu.DNgayTao = DateTime.Now;
                            canCuChungTu.SNguoiTao = _sessionService.Current.Principal;
                            canCuChungTu.IIdMaDonVi = Model.Id_DonVi;
                            canCuChungTu.STenDonVi = Model.TenDonVi;
                            canCuChungTu.FTuChi = 0;
                            canCuChungTu.FHienVat = 0;
                            canCuChungTu.FHangNhap = 0;
                            canCuChungTu.FHangMua = 0;
                            canCuChungTu.FPhanCap = 0;
                            canCuChungTu.FChuaPhanCap = 0;

                            if (DictionaryCanCu.TryGetValue(cc.IIDMaChucNang, out string position))
                            {
                                if (item.GetType().GetProperty($"X{position}").GetValue(item, null) is SktSoLieuChiTietCanCuDetail canCu)
                                {
                                    canCuChungTu.FTuChi = canCu.TuChi;
                                    canCuChungTu.FHangNhap = canCu.HangNhap;
                                    canCuChungTu.FHangMua = canCu.HangMua;
                                    canCuChungTu.FPhanCap = canCu.PhanCap;
                                }
                            }

                            if (canCuChungTu.HasValue) listCanCuChiTiet.Add(canCuChungTu);
                        }
                    }
                    count++;
                }
                LstCanCu.Select(n => { n.IsSaved = true; return n; }).ToList();
                _sktSoLieuChiTietCanCuService.AddRange(listCanCuChungTus);
                _sktSoLieuChiTietCanCuDataService.AddRange(listCanCuChiTiet);
            }
            else if (_lstCanCuInit != null && _lstCanCuInit.Count > 0)
            {
                List<NsDtdauNamChungTuChiTietCanCu> listCanCus = new List<NsDtdauNamChungTuChiTietCanCu>();
                int count = 0;
                foreach (CauHinhCanCuModel cc in _lstCanCuInit)
                {
                    foreach (SktSoLieuChiTietMLNSModel item in detailsCanCu)
                    {
                        System.Linq.Expressions.Expression<Func<NsDtdauNamChungTuChiTietCanCu, bool>> predicateCT = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
                        predicateCT = predicateCT.And(x => x.IID_CTDTDauNam == Model.Id);
                        predicateCT = predicateCT.And(x => x.IIdCanCu == cc.Id);
                        predicateCT = predicateCT.And(x => x.IIdMaDonVi == Model.Id_DonVi);
                        predicateCT = predicateCT.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
                        predicateCT = predicateCT.And(x => x.SXauNoiMa == item.XauNoiMa);

                        List<NsDtdauNamChungTuChiTietCanCu> sktcanCanCus = _sktSoLieuChiTietCanCuDataService.FindByCondition(predicateCT).ToList();
                        //_sktSoLieuChiTietCanCuDataService.RemoveRange(sktcanCanCus);
                        foreach (NsDtdauNamChungTuChiTietCanCu itemDelete in sktcanCanCus)
                        {
                            _sktSoLieuChiTietCanCuDataService.Delete(itemDelete.Id);
                        }

                        NsDtdauNamChungTuChiTietCanCu canCuChungTu = new NsDtdauNamChungTuChiTietCanCu();
                        canCuChungTu = _mapper.Map<NsDtdauNamChungTuChiTietCanCu>(item);
                        canCuChungTu.Id = Guid.Empty;
                        canCuChungTu.IIdCanCu = cc.Id;
                        canCuChungTu.IID_CTDTDauNam = Model.Id;
                        canCuChungTu.ILoaiChungTu = int.Parse(LoaiChungTu);
                        canCuChungTu.INamNganSach = _sessionService.Current.YearOfBudget;
                        canCuChungTu.IIdMaNguonNganSach = _sessionService.Current.Budget;
                        canCuChungTu.INamLamViec = _sessionService.Current.YearOfWork;
                        //canCuChungTu.ILoai = 3;
                        canCuChungTu.DNgayTao = DateTime.Now;
                        canCuChungTu.SNguoiTao = _sessionService.Current.Principal;
                        canCuChungTu.IIdMaDonVi = Model.Id_DonVi;
                        canCuChungTu.STenDonVi = Model.TenDonVi;
                        canCuChungTu.FTuChi = 0;
                        canCuChungTu.FHienVat = 0;
                        canCuChungTu.FHangNhap = 0;
                        canCuChungTu.FHangMua = 0;
                        canCuChungTu.FPhanCap = 0;
                        canCuChungTu.FChuaPhanCap = 0;

                        if (DictionaryCanCu.TryGetValue(cc.IIDMaChucNang, out string position))
                        {
                            if (item.GetType().GetProperty($"X{position}").GetValue(item, null) is SktSoLieuChiTietCanCuDetail canCu)
                            {
                                canCuChungTu.FTuChi = canCu.TuChi;
                                canCuChungTu.FHangNhap = canCu.HangNhap;
                                canCuChungTu.FHangMua = canCu.HangMua;
                                canCuChungTu.FPhanCap = canCu.PhanCap;
                            }
                        }

                        if (canCuChungTu.HasValue) listCanCus.Add(canCuChungTu);
                    }

                    count++;
                }
                _sktSoLieuChiTietCanCuDataService.AddRange(listCanCus);
            }
            if (detailsCanCu != null && detailsCanCu.Count > 0)
            {
                detailsCanCu.Select(n => { n.IsUpdateCanCu = false; return n; }).ToList();
            }
            if (Items != null && Items.Count > 0)
            {
                Items.Where(n => n.IsHangCha).Select(n => { n.IsUpdateCanCu = false; return n; }).ToList();
            }
        }

        private void ReFreshDataItems()
        {
            Items.Where(x => x.IsModified).Select(x =>
            {
                x.IsModified = false;
                return x;
            }).ToList();
            Items.Where(x => x.IsDeleted).Select(x =>
            {
                x.MucTienPhanBo = 0;
                x.ChiTiet = 0;
                x.UocThucHien = 0;
                x.HangMua = 0;
                x.HangNhap = 0;
                x.PhanCap = 0;
                x.ChuaPhanCap = 0;
                x.IdDb = null;
                x.IsModified = false;
                x.IsDeleted = false;
                return x;
            }).ToList();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha && !IsReadOnlyTable)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                CalculateData();
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private void CreateRow(int indexRow)
        {
            SktSoLieuChiTietMLNSModel itemNewRow = new SktSoLieuChiTietMLNSModel();
            itemNewRow.MlnsId = SelectedItem.MlnsId;
            itemNewRow.MlnsIdParent = SelectedItem.MlnsIdParent;
            itemNewRow.XauNoiMa = SelectedItem.XauNoiMa;
            itemNewRow.LNS = SelectedItem.LNS;
            itemNewRow.L = SelectedItem.L;
            itemNewRow.K = SelectedItem.K;
            itemNewRow.M = SelectedItem.M;
            itemNewRow.TM = SelectedItem.TM;
            itemNewRow.TTM = SelectedItem.TTM;
            itemNewRow.NG = SelectedItem.NG;
            itemNewRow.TNG = SelectedItem.TNG;
            itemNewRow.MoTa = SelectedItem.MoTa;
            itemNewRow.Chuong = SelectedItem.Chuong;
            itemNewRow.IsHangCha = false;
            itemNewRow.IdDonVi = Model.Id_DonVi;
            itemNewRow.TenDonVi = Model.TenDonVi;
            itemNewRow.SKT_KyHieu = SelectedItem.SKT_KyHieu;
            itemNewRow.IsModified = true;
            itemNewRow.PropertyChanged += DetailModel_PropertyChanged;
            Items.Insert(indexRow, itemNewRow);
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private List<NsNguoiDungLns> GetListLNSByUser()
        {
            System.Linq.Expressions.Expression<Func<NsNguoiDungLns, bool>> predicate = PredicateBuilder.True<NsNguoiDungLns>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.SMaNguoiDung == _sessionService.Current.Principal);
            List<NsNguoiDungLns> listNguoiDungDonVi = _nsNguoiDungLNSService.FindAll(predicate).ToList();
            return listNguoiDungDonVi;
        }

        private string GetIdChungTuChild(string idDonVi)
        {
            System.Linq.Expressions.Expression<Func<NsDtdauNamChungTu, bool>> predicateSummary = PredicateBuilder.True<NsDtdauNamChungTu>();
            predicateSummary = predicateSummary.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicateSummary = predicateSummary.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicateSummary = predicateSummary.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicateSummary = predicateSummary.And(x => x.IIdMaDonVi == idDonVi);
            predicateSummary = predicateSummary.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
            NsDtdauNamChungTu chungTu = new NsDtdauNamChungTu();
            chungTu = _sktChungTuService.FindByCondition(predicateSummary).FirstOrDefault();
            List<string> listChungTu = new List<string>();
            if (chungTu != null && !string.IsNullOrEmpty(chungTu.SDSSoChungTuTongHop))
            {
                listChungTu = chungTu.SDSSoChungTuTongHop.Split(",").ToList();
            }
            if (chungTu != null)
            {
                if (listChungTu != null && listChungTu.Count > 0)
                {
                    System.Linq.Expressions.Expression<Func<NsDtdauNamChungTu, bool>> predicate = PredicateBuilder.True<NsDtdauNamChungTu>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                    predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
                    predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                    predicate = predicate.And(x => listChungTu.Contains(x.SSoChungTu));
                    predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
                    List<NsDtdauNamChungTu> chungTuChild = _sktChungTuService.FindByCondition(predicate).ToList();
                    if (chungTuChild != null && chungTuChild.Count > 0)
                    {
                        return string.Join(",", chungTuChild.Select(n => n.Id.ToString()).ToList());
                    }
                }
            }
            return string.Empty;
        }

        private string GetIdMaDonViChungTuChild(string idDonVi)
        {
            System.Linq.Expressions.Expression<Func<NsDtdauNamChungTu, bool>> predicateSummary = PredicateBuilder.True<NsDtdauNamChungTu>();
            predicateSummary = predicateSummary.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicateSummary = predicateSummary.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicateSummary = predicateSummary.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicateSummary = predicateSummary.And(x => x.IIdMaDonVi == idDonVi);
            predicateSummary = predicateSummary.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
            NsDtdauNamChungTu chungTu = new NsDtdauNamChungTu();
            chungTu = _sktChungTuService.FindByCondition(predicateSummary).FirstOrDefault();
            List<string> listChungTu = new List<string>();
            if (chungTu != null && !string.IsNullOrEmpty(chungTu.SDSSoChungTuTongHop))
            {
                listChungTu = chungTu.SDSSoChungTuTongHop.Split(",").ToList();
            }
            if (chungTu != null)
            {
                if (listChungTu != null && listChungTu.Count > 0)
                {
                    System.Linq.Expressions.Expression<Func<NsDtdauNamChungTu, bool>> predicate = PredicateBuilder.True<NsDtdauNamChungTu>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                    predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
                    predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                    predicate = predicate.And(x => listChungTu.Contains(x.SSoChungTu));
                    predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
                    List<NsDtdauNamChungTu> chungTuChild = _sktChungTuService.FindByCondition(predicate).ToList();
                    if (chungTuChild != null && chungTuChild.Count > 0)
                    {
                        return string.Join(",", chungTuChild.Select(n => n.IIdMaDonVi).ToList());
                    }
                }
            }
            return string.Empty;
        }

        private List<NsDtdauNamChungTu> GetListChungTuBySoChungTu(string soChungTu)
        {
            List<string> arr = soChungTu.Split(",").ToList();
            System.Linq.Expressions.Expression<Func<NsDtdauNamChungTu, bool>> predicate = PredicateBuilder.True<NsDtdauNamChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => arr.Contains(x.SSoChungTu));
            List<NsDtdauNamChungTu> chungTus = _sktChungTuService.FindByCondition(predicate).ToList();
            return chungTus == null ? new List<NsDtdauNamChungTu>() : chungTus;
        }

        public void LoadData()
        {
            if (Model == null) return;

            if (Model.Loai == LoaiDonVi.ROOT)
            {
                if (IsShowTypeAgency && SelectedTypeShowAgency?.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    _dataDetailPlanBeginYear = _sktSoLieuService.FindByConditionDonVi0ChiTietDonVi(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                                                _sessionService.Current.Budget, 1, 0, Model.Id_DonVi, LoaiChungTu, GetIdChungTuChild(Model.Id_DonVi), Model.DsLNS).ToList();
                }
                else
                {
                    _dataDetailPlanBeginYear = _sktSoLieuService.FindByConditionDonVi0(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                                              _sessionService.Current.Budget, 0, 0, Model.Id_DonVi, LoaiChungTu, Model.Id.ToString(), Model.DsLNS).ToList();
                }
            }
            else
            {
                List<string> listParentLNS = StringUtils.GetListXauNoiMaParent(Model.DsLNS.Split(",").ToList());
                _dataDetailPlanBeginYear = _sktSoLieuService.FindByCondition(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                _sessionService.Current.Budget, 3, 1, Model.Id_DonVi, LoaiChungTu, string.Join(",", listParentLNS), Model.Id.ToString()).ToList();
            }

            if (LoaiChungTu == VoucherType.NSBD_Key)
            {
                List<DanhMuc> listDanhMuc = new List<DanhMuc>();
                if (IsShowTypeAgency)
                {
                    if (string.IsNullOrEmpty(Model.DSSoChungTuTongHop))
                    {
                        listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => n.SGiaTri.Split(",").Contains(Model.Id_DonVi)).ToList();
                    }
                    else
                    {
                        List<NsDtdauNamChungTu> listChungTuCon = GetListChungTuBySoChungTu(Model.DSSoChungTuTongHop);
                        if (listChungTuCon.Any())
                        {
                            List<string> listDonVi = listChungTuCon.Select(n => n.IIdMaDonVi).ToList();
                            listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => n.SGiaTri.Split(",").Any(m => listDonVi.Contains(m))).ToList();
                        }
                        else
                        {
                            _dataDetailPlanBeginYear = new List<SktSoLieuChiTietMlnsQuery>();
                        }
                    }
                }
                else
                {
                    listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => n.SGiaTri.Split(",").Contains(Model.Id_DonVi)).ToList();
                }
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    List<SktSoLieuChiTietMlnsQuery> listChild = _dataDetailPlanBeginYear.Where(n => listDanhMuc.Select(m => m.IIDMaDanhMuc).ToList().Contains(n.NG)).ToList();
                    List<string> listParentXauNoiMa = StringUtils.GetListXauNoiMaParent(listChild.Select(n => n.XauNoiMa).ToList());
                    _dataDetailPlanBeginYear = _dataDetailPlanBeginYear.Where(n => listParentXauNoiMa.Contains(n.XauNoiMa)).ToList();
                }
                else
                {
                    _dataDetailPlanBeginYear = new List<SktSoLieuChiTietMlnsQuery>();
                }
            }
            // Check map
            List<SktSoLieuChiTietMlnsQuery> listHasMap = _dataDetailPlanBeginYear.Where(n => !string.IsNullOrEmpty(n.SKT_KyHieu)).ToList();
            HashSet<string> listXauNoiMaMap = StringUtils.GetXauNoiMaParentOptimize(listHasMap.Select(n => n.XauNoiMa));
            _dataDetailPlanBeginYear = _dataDetailPlanBeginYear.Where(n => listXauNoiMaMap.Contains(n.XauNoiMa)).ToList();

            if (!_sessionService.Current.IsQuanLyDonViCha)
            {
                List<NsNguoiDungLns> listLNSNguoiDung = GetListLNSByUser();
                HashSet<string> listParentLNS = StringUtils.GetXauNoiMaParentOptimize(listLNSNguoiDung.Select(n => n.SLns));
                _dataDetailPlanBeginYear = _dataDetailPlanBeginYear.Where(n => listParentLNS.Contains(n.LNS)).ToList();

                //Filter SKT
                //List<string> listKyHieuSkt = _dataDetailPlanBeginYear.Where(n => !n.BHangCha && !string.IsNullOrEmpty(n.SKT_KyHieu)).Select(n => n.SKT_KyHieu).ToList();
                //listKyHieuSkt = StringUtils.GetListKyHieuParent(listKyHieuSkt);
                //_dataMucLuc.Where(n => !string.IsNullOrEmpty(n.KyHieu) && !listKyHieuSkt.Contains(n.KyHieu)).Select(n => { n.IsVisibilily = false; return n; }).ToList();
                GetListParentSktSearch();
                _dataMuclucFilter.Refresh();
                UpdateSelectedSktSearch();
            }
            HashSet<string> listXauNoiMaChild = StringUtils.GetXauNoiMaParentOptimize(_dataDetailPlanBeginYear.Where(n => !n.BHangChaDuToan).Select(n => n.XauNoiMa));
            _dataDetailPlanBeginYear = _dataDetailPlanBeginYear.Where(n => listXauNoiMaChild.Contains(n.XauNoiMa)).ToList();
            //ProcessDynamicMLNS();
            _dataDetailPlanBeginYear = _dataDetailPlanBeginYear.OrderBy(n => n.XauNoiMa).ToList();
            Items = _mapper.Map<ObservableCollection<SktSoLieuChiTietMLNSModel>>(_dataDetailPlanBeginYear);

            //GetDuToanNamTruoc();

            _dataDetailFilter = CollectionViewSource.GetDefaultView(Items);
            _dataDetailFilter.Filter = DetailFilter;
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
            foreach (SktSoLieuChiTietMLNSModel model in Items)
            {
                if (!model.IsHangCha)
                {
                    model.PropertyChanged += DetailModel_PropertyChanged;

                    for (int i = 1; i <= 5; i++)
                    {
                        if (model.GetType().GetProperty($"X{i}").GetValue(model, null) is SktSoLieuChiTietCanCuDetail canCu)
                        {
                            canCu.PropertyChanged += (sender, args) =>
                            {
                                if (!IsInit)
                                {
                                    model.IsUpdateCanCu = true;
                                    model.IsModified = true;
                                    if (!IsCanCu) CalculateData();
                                    OnPropertyChanged(nameof(IsSaveData));
                                }
                            };
                        }
                    }
                }
            }
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
        }

        private void ProcessDynamicMLNS()
        {
            _dataDetailPlanBeginYearDynamic = new List<SktSoLieuChiTietMlnsQuery>();

            List<string> chiTietTois = _dataDetailPlanBeginYear.Where(x => !string.IsNullOrEmpty(x.ChiTietToi)).Select(x => x.ChiTietToi).ToList();
            ColumnDisplay = DynamicMLNS.SettingColumnVisibility(chiTietTois);

            foreach (SktSoLieuChiTietMlnsQuery item in _dataDetailPlanBeginYear.Where(x => string.IsNullOrEmpty(x.L)))
            {
                if (!string.IsNullOrEmpty(item.ChiTietToi))
                    _dataDetailPlanBeginYearDynamic.AddRange(GetChildItems(_dataDetailPlanBeginYear.Where(x => x.LNS == item.LNS).ToList(), item));
                else _dataDetailPlanBeginYearDynamic.Add(item);
            }
            _dataDetailPlanBeginYearDynamic = _dataDetailPlanBeginYearDynamic.OrderBy(x => x.XauNoiMa).ToList();
        }

        public List<SktSoLieuChiTietMlnsQuery> GetChildItems(List<SktSoLieuChiTietMlnsQuery> data, SktSoLieuChiTietMlnsQuery root)
        {
            List<SktSoLieuChiTietMlnsQuery> childrens = new List<SktSoLieuChiTietMlnsQuery>();
            switch (root.ChiTietToi)
            {
                case nameof(MLNSFiled.NG):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.NG) && string.IsNullOrEmpty(x.TNG)).Select(x => { x.BHangCha = false; return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.NG) && string.IsNullOrEmpty(x.TNG)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
                case nameof(MLNSFiled.TNG):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.TNG) && string.IsNullOrEmpty(x.TNG1)).Select(x => { x.BHangCha = false; return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.TNG) && string.IsNullOrEmpty(x.TNG1)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
                case nameof(MLNSFiled.TNG1):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.TNG1) && string.IsNullOrEmpty(x.TNG2)).Select(x => { x.BHangCha = false; return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.TNG1) && string.IsNullOrEmpty(x.TNG2)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
                case nameof(MLNSFiled.TNG2):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.TNG2) && string.IsNullOrEmpty(x.TNG3)).Select(x => { x.BHangCha = false; return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.TNG2) && string.IsNullOrEmpty(x.TNG3)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
                case nameof(MLNSFiled.TNG3):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.TNG3)).Select(x => { x.BHangCha = false; return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.TNG3)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
            }
            return childrens;
        }

        private bool DetailFilter(object obj)
        {
            bool result = true;
            if (obj is SktSoLieuChiTietMLNSModel item)
            {
                if (!string.IsNullOrEmpty(SelectedTypeDisplays))
                {
                    if (SelectedTypeDisplays == TypeDisplay.CO_SO_LIEU)
                        result = result && ((item.ChiTiet != 0
                            || item.HangNhap != 0 || item.HangMua != 0 || item.PhanCap != 0 || item.ChuaPhanCap != 0 || item.UocThucHien != 0 || item.MucTienPhanBo != 0
                            || item.X1.TuChi != 0 || item.X1.HangNhap != 0 || item.X1.HangMua != 0 || item.X1.PhanCap != 0
                            || item.X2.TuChi != 0 || item.X2.HangNhap != 0 || item.X2.HangMua != 0 || item.X2.PhanCap != 0
                            || item.X3.TuChi != 0 || item.X3.HangNhap != 0 || item.X3.HangMua != 0 || item.X3.PhanCap != 0
                            || item.X4.TuChi != 0 || item.X4.HangNhap != 0 || item.X4.HangMua != 0 || item.X4.PhanCap != 0
                            || item.X5.TuChi != 0 || item.X5.HangNhap != 0 || item.X5.HangMua != 0 || item.X5.PhanCap != 0
                            || (item.IsModified && (item.IdDb == Guid.Empty || item.IdDb == null))));
                    if (SelectedTypeDisplays == TypeDisplay.DA_NHAP_SO_DU_TOAN)
                        result = result && (item.ChiTiet != 0 || (item.IsModified && (item.IdDb == Guid.Empty || item.IdDb == null)));
                    if (SelectedMucLuc != null && (!string.IsNullOrEmpty(SelectedMucLuc.Stt) || !SelectedMucLuc.BHangCha))
                    {
                        result = result && (item.SKT_KyHieu == SelectedMucLuc.KyHieu
                            || (ListParentFilter.Contains(item) && item.IsHangCha && ListChildFilter.Where(n => !n.IsHangCha).Any(n => n.SKT_KyHieu == SelectedMucLuc.KyHieu))
                            || ListMucLucSelected.Any(n => n.KyHieu == item.SKT_KyHieu
                            || (ListParentFilter.Contains(item) && item.IsHangCha)
                            )
                            );
                    }
                    if (SelectedLoaiNganSach != null && LoaiChungTu != null && LoaiChungTu == VoucherType.NSBD_Key)
                    {
                        result = result && (((SelectedLoaiNganSach.HiddenValue.Contains(item.NG) && !string.IsNullOrEmpty(item.NG))
                            || string.IsNullOrEmpty(SelectedLoaiNganSach.HiddenValue)) || ListParentFilter.Any(n => n.XauNoiMa == item.XauNoiMa)
                            );
                    }
                }
                if (!string.IsNullOrEmpty(SelectedLNS))
                    result = result && (item.LNS.ToLower().StartsWith(SelectedLNS.Trim().ToLower()) || ListParentFilter.Contains(item));
                if (!string.IsNullOrEmpty(MLNSDetailFilter.L))
                    result = result && (item.L.ToLower().StartsWith(MLNSDetailFilter.L.Trim().ToLower()) || ListParentFilter.Contains(item));
                if (!string.IsNullOrEmpty(MLNSDetailFilter.K))
                    result = result && (item.K.ToLower().StartsWith(MLNSDetailFilter.K.Trim().ToLower()) || ListParentFilter.Contains(item));
                if (!string.IsNullOrEmpty(MLNSDetailFilter.M))
                    result = result && (item.M.ToLower().StartsWith(MLNSDetailFilter.M.Trim().ToLower()) || ListParentFilter.Contains(item));
                if (!string.IsNullOrEmpty(MLNSDetailFilter.TM))
                    result = result && (item.TM.ToLower().StartsWith(MLNSDetailFilter.TM.Trim().ToLower()) || ListParentFilter.Contains(item));
                if (!string.IsNullOrEmpty(MLNSDetailFilter.TTM))
                    result = result && (item.TTM.ToLower().StartsWith(MLNSDetailFilter.TTM.Trim().ToLower()) || ListParentFilter.Contains(item));
                if (!string.IsNullOrEmpty(MLNSDetailFilter.NG))
                    result = result && (item.NG.ToLower().StartsWith(MLNSDetailFilter.NG.Trim().ToLower()) || ListParentFilter.Contains(item));
                if (!string.IsNullOrEmpty(MLNSDetailFilter.TNG))
                    result = result && (item.TNG.ToLower().StartsWith(MLNSDetailFilter.TNG.Trim().ToLower()) || ListParentFilter.Contains(item));
                if (!string.IsNullOrEmpty(MLNSDetailFilter.TNG1))
                    result = result && (item.TNG1.ToLower().StartsWith(MLNSDetailFilter.TNG1.Trim().ToLower()) || ListParentFilter.Contains(item));
                if (!string.IsNullOrEmpty(MLNSDetailFilter.TNG2))
                    result = result && (item.TNG2.ToLower().StartsWith(MLNSDetailFilter.TNG2.Trim().ToLower()) || ListParentFilter.Contains(item));
                if (!string.IsNullOrEmpty(MLNSDetailFilter.TNG3))
                    result = result && (item.TNG3.ToLower().StartsWith(MLNSDetailFilter.TNG3.Trim().ToLower()) || ListParentFilter.Contains(item));
                item.IsFilter = result;
            }
            return result;
        }

        public void GetListParentFilter()
        {
            ListParentFilter = new List<SktSoLieuChiTietMLNSModel>();
            ListChildFilter = new List<SktSoLieuChiTietMLNSModel>();
            ListMucLucSelected = new List<SktMucLucDuToanDauNamModel>();
            GetDataListMucLucSelected();
            ListChildFilter = Items.Where(item =>
                                    (((item.ChiTiet != 0 || item.UocThucHien != 0 || item.MucTienPhanBo != 0 || (item.IsModified && (item.IdDb == Guid.Empty || item.IdDb == null))) && SelectedTypeDisplays == TypeDisplay.CO_SO_LIEU)
                                    || ((item.DuToan != 0 || (item.IsModified && (item.IdDb == Guid.Empty || item.IdDb == null))) && SelectedTypeDisplays == TypeDisplay.DA_NHAP_SO_DU_TOAN)
                                    || SelectedTypeDisplays == TypeDisplay.HIEN_THI_TAT_CA
                                    )
                                && SelectedMucLuc != null
                                && (SelectedMucLuc.KyHieu == item.SKT_KyHieu || (SelectedMucLuc.IsHangCha && ListMucLucSelected.Any(n => n.KyHieu == item.SKT_KyHieu)))
                                && (
                                    (SelectedLoaiNganSach != null && LoaiChungTu != null && LoaiChungTu == VoucherType.NSBD_Key
                                            && (SelectedLoaiNganSach.HiddenValue.Contains(item.NG) || string.IsNullOrEmpty(SelectedLoaiNganSach.HiddenValue)) && !string.IsNullOrEmpty(item.NG)
                                    )
                                    ||
                                    (LoaiChungTu != null && LoaiChungTu == VoucherType.NSSD_Key)
                                )
                                && (item.LNS.ToLower().StartsWith(SelectedLNS.Trim().ToLower()) || string.IsNullOrEmpty(SelectedLNS.Trim().ToLower()))
                                && (item.L.ToLower().StartsWith(MLNSDetailFilter.L.Trim().ToLower()) || string.IsNullOrEmpty(MLNSDetailFilter.L.Trim().ToLower()))
                                && (item.K.ToLower().StartsWith(MLNSDetailFilter.K.Trim().ToLower()) || string.IsNullOrEmpty(MLNSDetailFilter.K.Trim().ToLower()))
                                && (item.M.ToLower().StartsWith(MLNSDetailFilter.M.Trim().ToLower()) || string.IsNullOrEmpty(MLNSDetailFilter.M.Trim().ToLower()))
                                && (item.TM.ToLower().StartsWith(MLNSDetailFilter.TM.Trim().ToLower()) || string.IsNullOrEmpty(MLNSDetailFilter.TM.Trim().ToLower()))
                                && (item.TTM.ToLower().StartsWith(MLNSDetailFilter.TTM.Trim().ToLower()) || string.IsNullOrEmpty(MLNSDetailFilter.TTM.Trim().ToLower()))
                                && (item.NG.ToLower().StartsWith(MLNSDetailFilter.NG.Trim().ToLower()) || string.IsNullOrEmpty(MLNSDetailFilter.NG.Trim().ToLower()))
                                && (item.TNG.ToLower().StartsWith(MLNSDetailFilter.TNG.Trim().ToLower()) || string.IsNullOrEmpty(MLNSDetailFilter.TNG.Trim().ToLower()))
                                && (item.TNG1.ToLower().StartsWith(MLNSDetailFilter.TNG1.Trim().ToLower()) || string.IsNullOrEmpty(MLNSDetailFilter.TNG1.Trim().ToLower()))
                                && (item.TNG2.ToLower().StartsWith(MLNSDetailFilter.TNG2.Trim().ToLower()) || string.IsNullOrEmpty(MLNSDetailFilter.TNG2.Trim().ToLower()))
                                && (item.TNG3.ToLower().StartsWith(MLNSDetailFilter.TNG3.Trim().ToLower()) || string.IsNullOrEmpty(MLNSDetailFilter.TNG3.Trim().ToLower()))
              ).ToList();
            if (SelectedLoaiNganSach != null)
            {
                if (ListChildFilter != null && ListChildFilter.Count > 0)
                {
                    List<string> listLNSChild = StringUtils.GetListXauNoiMaParent(ListChildFilter.Select(n => n.XauNoiMa).ToList());
                    ListParentFilter = Items.Where(n => n.IsHangCha
                        && listLNSChild.Any(x => x == n.XauNoiMa)).ToList();
                }
            }
        }

        public void GetDataListMucLucSelected()
        {
            try
            {
                _countLoop = 0;
                if (SelectedMucLuc != null)
                    ListMucLucSelected.AddRange(GetItemChildMucLucSelected(SelectedMucLuc));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                MessageBoxHelper.Error(ex.Message);
            }
        }

        public List<SktMucLucDuToanDauNamModel> GetItemChildMucLucSelected(SktMucLucDuToanDauNamModel parent)
        {
            _countLoop++;
            if (_countLoop > DataMucLuc.Count)
            {
                throw new Exception("Có lỗi xảy ra do Mục lục số kiểm tra chưa đúng. Vui lòng kiểm tra lại");
            }
            List<SktMucLucDuToanDauNamModel> listChild = DataMucLuc.Where(n => n.IdParent == parent.IdMucLuc).ToList();
            foreach (SktMucLucDuToanDauNamModel item in listChild)
            {
                ListMucLucSelected.AddRange(GetItemChildMucLucSelected(item));
            }
            return (listChild != null && listChild.Count > 0) ? listChild : new List<SktMucLucDuToanDauNamModel>();
        }

        public void LoadDataMucLuc()
        {
            if (Model == null) return;
            string loai;

            if (Model.Loai == LoaiDonVi.ROOT) loai = "3";
            else loai = "2,4";

            List<string> listKyHieu = new List<string>();
            if (LoaiChungTu == VoucherType.NSBD_Key)
            {
                List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => n.SGiaTri.Split(",").Contains(Model.Id_DonVi)).ToList();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    List<NsSktMucLuc> listMucLuc = _sktMucLucService.FindByNganh(_sessionService.Current.YearOfWork, listDanhMuc.Select(n => n.IIDMaDanhMuc).ToList()).ToList();
                    listKyHieu = StringUtils.GetListKyHieuParent(listMucLuc.Select(n => n.SKyHieu).ToList());
                }
            }
            IEnumerable<SktMucLucDtQuery> data = _sktMucLucService.FindByConditionBVTC(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget,
                loai, Model.Id_DonVi, int.Parse(LoaiChungTu), Model.ILoaiNguonNganSach, Model.Id.ToString()).ToList();
            if (LoaiChungTu == VoucherType.NSBD_Key && Model.Loai != LoaiDonVi.ROOT)
            {
                data = data.Where(n => listKyHieu.Contains(n.KyHieu)).ToList();
            }

            _dataMucLuc = _mapper.Map<ObservableCollection<Model.SktMucLucDuToanDauNamModel>>(data);
            _dataMucLuc.Insert(0, new SktMucLucDuToanDauNamModel { Stt = string.Empty, MoTa = "TỔNG CỘNG", BHangCha = true, TuChi = 0 });
            _dataMucLuc.Select(n => { n.IsVisibilily = true; return n; }).ToList();
            _dataMuclucFilter = CollectionViewSource.GetDefaultView(DataMucLuc);
            _dataMuclucFilter.Filter = MucLucFilter;
            if (DataMucLuc != null && DataMucLuc.Count > 0)
            {
                SelectedMucLuc = DataMucLuc.FirstOrDefault();
            }
            CalculateDataMucLuc();
            OnPropertyChanged(nameof(DataMucLuc));
            GetListParentSktSearch();
            _dataMuclucFilter.Refresh();
            UpdateSelectedSktSearch();
        }

        private void OnRefeshFilterSkt()
        {
            GetListParentSktSearch();
            _dataMuclucFilter.Refresh();
            UpdateSelectedSktSearch();
        }

        private void CalculateMucLuc()
        {
            OnPropertyChanged(nameof(Items));
            foreach (SktMucLucDuToanDauNamModel item in _dataMucLuc)
            {
                item.TongSo = Items != null ? Items.Where(n => n.SKT_KyHieu == item.KyHieu && !n.IsHangCha
                    && !string.IsNullOrEmpty(n.SKT_KyHieu) && !string.IsNullOrEmpty(item.KyHieu)).Sum(n => n.ChiTiet) : 0;

                double tongSoHangMua = Items != null ? Items.Where(n => n.SKT_KyHieu == item.KyHieu && !n.IsHangCha
                    && !string.IsNullOrEmpty(n.SKT_KyHieu) && !string.IsNullOrEmpty(item.KyHieu)).Sum(n => n.HangMua) : 0;
                double tongSoHangNhap = Items != null ? Items.Where(n => n.SKT_KyHieu == item.KyHieu && !n.IsHangCha
                    && !string.IsNullOrEmpty(n.SKT_KyHieu) && !string.IsNullOrEmpty(item.KyHieu)).Sum(n => n.HangNhap) : 0;
                item.TongSoHang = tongSoHangMua + tongSoHangNhap;

                item.TongDacThu = Items != null ? Items.Where(n => n.SKT_KyHieu == item.KyHieu && !n.IsHangCha
                    && !string.IsNullOrEmpty(n.SKT_KyHieu) && !string.IsNullOrEmpty(item.KyHieu)).Sum(n => n.PhanCap) : 0;
            }
            CalculateDataMucLuc();
            OnPropertyChanged(nameof(DataMucLuc));
        }

        private bool MucLucFilter(object obj)
        {
            bool result = true;
            SktMucLucDuToanDauNamModel item = (SktMucLucDuToanDauNamModel)obj;
            if (!string.IsNullOrEmpty(SelectedTypeDisplaysMucLuc))
            {
                if (LoaiChungTu == VoucherType.NSSD_Key)
                {
                    if (SelectedTypeDisplaysMucLuc == TypeDisplay.DA_NHAP_SKT)
                        result = result && (item.TuChi != 0 || item.TongSo != 0 || item.ConLai != 0 || item.ConLaiHang != 0 || item.ConLaiDacThu != 0 || (string.IsNullOrEmpty(item.Stt) && item.MoTa == "TỔNG CỘNG"));
                }
                if (LoaiChungTu == VoucherType.NSBD_Key)
                {
                    if (SelectedTypeDisplaysMucLuc == TypeDisplay.DA_NHAP_SKT)
                        result = result && (item.HangNhap != 0 || item.HangMua != 0 || item.PhanCap != 0 || (string.IsNullOrEmpty(item.Stt) && item.MoTa == "TỔNG CỘNG"));
                }
            }
            if (!string.IsNullOrEmpty(SktSKyHieuSearch))
            {
                result = result && (item.KyHieu == SktSKyHieuSearch || (ListParentSktSearch.Contains(item.KyHieu) && ListParentSktSearch.Count > 0));
            }
            result = result && item.IsVisibilily;
            item.IsFilter = result;
            return result;
        }

        private void CalculateData()
        {
            IndexCheck = 0;
            Items.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.MucTienPhanBo = 0; x.ChiTiet = 0; x.HangMua = 0; x.HangNhap = 0; x.PhanCap = 0; x.PhanCapConLai = 0; x.ChuaPhanCap = 0; x.UocThucHien = 0; x.MucTienPhanBo = 0;
                    x.X1 = new SktSoLieuChiTietCanCuDetail();
                    x.X2 = new SktSoLieuChiTietCanCuDetail();
                    x.X3 = new SktSoLieuChiTietCanCuDetail();
                    x.X4 = new SktSoLieuChiTietCanCuDetail();
                    x.X5 = new SktSoLieuChiTietCanCuDetail();
                    return x;
                }).ToList();

            IEnumerable<SktSoLieuChiTietMLNSModel> listChild = Items.Where(x => x.IsFilter && !x.IsHangCha && !x.IsDeleted &&
            (x.ChiTiet != 0 || x.UocThucHien != 0 || x.MucTienPhanBo != 0 || x.HangMua != 0 || x.HangNhap != 0 || x.PhanCap != 0 || x.ChuaPhanCap != 0 || x.IsUpdateCanCu
            || x.X1.TuChi != 0 || x.X1.HangNhap != 0 || x.X1.HangMua != 0 || x.X1.PhanCap != 0
            || x.X2.TuChi != 0 || x.X2.HangNhap != 0 || x.X2.HangMua != 0 || x.X2.PhanCap != 0
            || x.X3.TuChi != 0 || x.X3.HangNhap != 0 || x.X3.HangMua != 0 || x.X3.PhanCap != 0
            || x.X4.TuChi != 0 || x.X4.HangNhap != 0 || x.X4.HangMua != 0 || x.X4.PhanCap != 0
            || x.X5.TuChi != 0 || x.X5.HangNhap != 0 || x.X5.HangMua != 0 || x.X5.PhanCap != 0
            ));
            Dictionary<Guid, SktSoLieuChiTietMLNSModel> dictByMlns = Items.GroupBy(x => x.MlnsId).ToDictionary(x => x.Key, x => x.FirstOrDefault());

            foreach (SktSoLieuChiTietMLNSModel item in listChild)
            {
                CalculateParent(item.MlnsIdParent, item, dictByMlns);
            }
            CalculateTotal();
        }

        private void CalculateParent(Guid? idParent, SktSoLieuChiTietMLNSModel selfItem, Dictionary<Guid, SktSoLieuChiTietMLNSModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey((Guid)idParent)) return;

            SktSoLieuChiTietMLNSModel parentItem = dictByMlns[(Guid)idParent];

            parentItem.MucTienPhanBo += selfItem.MucTienPhanBo;
            parentItem.ChiTiet += selfItem.ChiTiet;
            parentItem.UocThucHien += selfItem.UocThucHien;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;
            parentItem.ChuaPhanCap += selfItem.ChuaPhanCap;
            parentItem.PhanCapConLai += selfItem.PhanCapConLai;

            for (int i = 1; i <= 5; i++)
            {
                if (parentItem.GetType().GetProperty($"X{i}").GetValue(parentItem, null) is SktSoLieuChiTietCanCuDetail parent
                    && selfItem.GetType().GetProperty($"X{i}").GetValue(selfItem, null) is SktSoLieuChiTietCanCuDetail self)
                {
                    parent.TuChi += self.TuChi;
                    parent.HangNhap += self.HangNhap;
                    parent.HangMua += self.HangMua;
                    parent.PhanCap += self.PhanCap;
                    parent.PhanCapConLai += self.PhanCapConLai;
                }
            }

            CalculateParent(parentItem.MlnsIdParent, selfItem, dictByMlns);
        }

        private void CalculateDataMucLuc()
        {
            _dataMucLuc.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.ConLai = 0; x.ConLaiHang = 0; x.ConLaiDacThu = 0; x.TuChi = 0;
                    x.TongSo = 0; x.HangNhap = 0; x.HangMua = 0; x.PhanCap = 0; x.MuaHangHienVat = 0; x.DacThu = 0; return x;
                }).ToList();

            Dictionary<Guid, SktMucLucDuToanDauNamModel> dictByIdMucLuc = DataMucLuc.GroupBy(x => x.IdMucLuc).ToDictionary(x => x.Key, x => x.First());

            List<SktMucLucDuToanDauNamModel> listChild = _dataMucLuc.Where(x => !x.IsHangCha && !x.IsDeleted && (x.TuChi != 0 || x.TongSo != 0 || x.ConLai != 0 || x.ConLaiHang != 0 || x.ConLaiDacThu != 0 ||
                                                                x.HangNhap != 0 || x.HangMua != 0 || x.PhanCap != 0 || x.MuaHangHienVat != 0 || x.DacThu != 0)).ToList();
            foreach (SktMucLucDuToanDauNamModel item in listChild)
            {
                CalculateParentMucLuc(item.IdParent, item, dictByIdMucLuc);
            }
            CalculateTotal();
            CalculateTotalMucLuc();
        }

        private void CalculateParentMucLuc(Guid? idParent, SktMucLucDuToanDauNamModel selfItem, Dictionary<Guid, SktMucLucDuToanDauNamModel> dictByIdMucLuc)
        {
            if (idParent == null || !dictByIdMucLuc.ContainsKey((Guid)idParent))
            {
                return;
            }
            SktMucLucDuToanDauNamModel parentItem = dictByIdMucLuc[(Guid)idParent];

            parentItem.TuChi += selfItem.TuChi;
            parentItem.ConLai += selfItem.ConLai;

            parentItem.TongSo += selfItem.TongSo;
            parentItem.TongSoHang += selfItem.TongSoHang;
            parentItem.TongDacThu += selfItem.TongDacThu;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;

            parentItem.MuaHangHienVat += selfItem.MuaHangHienVat;
            parentItem.DacThu += selfItem.DacThu;
            parentItem.ConLaiHang += selfItem.ConLaiHang;
            parentItem.ConLaiDacThu += selfItem.ConLaiDacThu;
            CalculateParentMucLuc(parentItem.IdParent, selfItem, dictByIdMucLuc);
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (IsGetCanCu) return;
            if (args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.ChiTiet)
                || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.MucTienPhanBo)
                || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.HangNhap)
                || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.HangMua)
                || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.PhanCap)
                || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.ChuaPhanCap))
            {
                SktSoLieuChiTietMLNSModel item = Items.FirstOrDefault(x => x.Id == ((SktSoLieuChiTietMLNSModel)sender).Id);
                item.IsModified = true;
                item.PhanCapConLai = (item.PhanCap != 0) ? item.PhanCap - item.TuChi : 0;

                if (!IsInit)
                {
                    CalculateData();
                    CalculateMucLuc();
                }

                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }

            if (args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.UocThucHien) && !IsInit)
            {
                SktSoLieuChiTietMLNSModel item = Items.FirstOrDefault(x => x.Id == ((SktSoLieuChiTietMLNSModel)sender).Id);
                item.IsModified = true;

                CalculateData();

                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        public void LoadCombobox()
        {
            TypeDisplaysMucLuc = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { ValueItem = TypeDisplay.DA_NHAP_SKT, DisplayItem = TypeDisplay.DA_NHAP_SKT },
                new ComboboxItem { ValueItem = TypeDisplay.HIEN_THI_TAT_CA, DisplayItem = TypeDisplay.HIEN_THI_TAT_CA }
            };
            SelectedTypeDisplaysMucLuc = TypeDisplay.HIEN_THI_TAT_CA;

            TypeDisplays = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { ValueItem = TypeDisplay.CO_SO_LIEU, DisplayItem = TypeDisplay.CO_SO_LIEU },
                new ComboboxItem { ValueItem = TypeDisplay.DA_NHAP_SO_DU_TOAN, DisplayItem = TypeDisplay.DA_NHAP_SO_DU_TOAN },
                new ComboboxItem { ValueItem = TypeDisplay.HIEN_THI_TAT_CA, DisplayItem = TypeDisplay.HIEN_THI_TAT_CA }
            };
            SelectedTypeDisplays = TypeDisplay.HIEN_THI_TAT_CA;

            TypeShowAgency = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI },
                new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI }
            };
            _selectedTypeShowAgency = TypeShowAgency.FirstOrDefault();

            List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh_Nganh", _sessionService.Current.YearOfWork).OrderBy(n => n.STen).ToList();
            LoaiNganSach = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { ValueItem = string.Empty, DisplayItem = "Nhóm ngành", HiddenValue = string.Empty }
            };

            foreach (DanhMuc item in listDanhMuc)
            {
                LoaiNganSach.Add(new ComboboxItem { ValueItem = item.IIDMaDanhMuc, DisplayItem = item.STen, HiddenValue = item.SGiaTri });
            }
            SelectedLoaiNganSach = LoaiNganSach.FirstOrDefault();
        }

        private void LoadPopupData()
        {
            System.Linq.Expressions.Expression<Func<NsSktMucLuc, bool>> predicate = PredicateBuilder.True<NsSktMucLuc>();
            //predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            //predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            //predicate = predicate.And(x => x.IIDMLSKTCha == Guid.Empty);
            //var idMucLucParent = _sktMucLucService.FindByCondition(predicate).Select(x => x.IIDMLSKT).ToList().Cast<Guid?>();
            predicate = PredicateBuilder.True<NsSktMucLuc>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork && x.ITrangThai == 1);
            //predicate = predicate.And(x => x.IIDMLSKTCha == Guid.Empty || idMucLucParent.Contains(x.IIDMLSKTCha));
            IOrderedEnumerable<NsSktMucLuc> temp = _sktMucLucService.FindByCondition(predicate).OrderBy(x => x.SKyHieu);
            SktMucLucModelItems = _mapper.Map<ObservableCollection<SktMucLucModel>>(temp);
            _searchPopupView = CollectionViewSource.GetDefaultView(SktMucLucModelItems);
            _searchPopupView.Filter = PopupFilter;
        }

        private bool PopupFilter(object obj)
        {
            SktMucLucModel temp = (SktMucLucModel)obj;
            if (!string.IsNullOrEmpty(PopupSearchText))
            {
                return (temp.SSTT.ToLower().Contains(PopupSearchText.Trim().ToLower())
                       || temp.SMoTa.ToLower().Contains(PopupSearchText.Trim().ToLower())
                       || temp.SKyHieu.ToLower().Contains(PopupSearchText.Trim().ToLower()));
            }
            else return true;
        }

        public override void Init()
        {
            try
            {
                base.Init();
                IsInit = true;
                LoadCanCuVisibility();
                NamLamViec = _sessionService.Current.YearOfWork;
                SelectedLNS = string.Empty;
                MLNSDetailFilter = new AllocationDetailFilterModel();
                LoadComboboxDone = false;
                Items = new ObservableCollection<Model.SktSoLieuChiTietMLNSModel>();
                LoadPopupData();
                LoadCombobox();
                LoadDataMucLuc();
                LoadComboboxDone = true;
                LoadData();
                CalculateMucLuc();
                LoadCanCu();
                CalculateData();
                LoadBudgetIndexCondition();

                IsReadOnlyTable = !(ListNguoiDungDonVi != null && ListNguoiDungDonVi.Any(x => x.IIdMaDonVi.Contains(Model.Id_DonVi)) && !IsLocked);

                if (!string.IsNullOrEmpty(Model.NguoiTao) && _sessionService.Current.Principal != Model.NguoiTao)
                {
                    IsReadOnlyTable = true;
                }

                OnPropertyChanged(nameof(IsReadOnlyTable));
                OnPropertyChanged(nameof(IsShowTypeAgency));
                OnPropertyChanged(nameof(IsEnableButtonCanCu));
                OnPropertyChanged(nameof(VisibleNganSachSuDung));
                OnPropertyChanged(nameof(VisibleNganSachDamBao));
                OnPropertyChanged(nameof(IsDeleteAll));
                OnPropertyChanged(nameof(IsShowTypeAgencyDetail));
                IsInit = false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadCanCu(ObservableCollection<CauHinhCanCuModel> obj)
        {
            LstCanCu = obj;
            foreach (CauHinhCanCuModel cc in obj)
            {
                if (cc.LstIdChungTuCanCu is null || cc.LstIdChungTuCanCu.Count == 0) continue;
                string lstIdChungTu = (cc.LstIdChungTuCanCu != null && cc.LstIdChungTuCanCu.Count > 0) ? string.Join(",", cc.LstIdChungTuCanCu)
                    : Guid.Empty.ToString();
                string idDonVi = Model.Id_DonVi;

                int loaiCanCu = DictionaryLoaiCanCu.ContainsKey(cc.IIDMaChucNang) ? DictionaryLoaiCanCu[cc.IIDMaChucNang] : 0;

                int namLamViec = cc.INamCanCu.GetValueOrDefault();
                string idLns = cc.IdLns;
                string nhomNganh = cc.NganhSelected;
                string listIdMucLuc = "-1";
                if (!String.IsNullOrEmpty(idLns))
                {
                    listIdMucLuc = string.Join(",", _nsMucLucNganSachService.FindChildMlnsByParentLNS(idLns,
                        _sessionService.Current.YearOfWork).Select(item => item.MlnsId).ToList());
                }
                if (!String.IsNullOrEmpty(nhomNganh))
                {
                    listIdMucLuc = string.Join(",", _sktChungTuChiTietService.FindMucLucSKTTheoNganh(nhomNganh, 1, _sessionService.Current.YearOfWork).Select(item => item.IIdMlskt).ToList());
                }
                List<DuToanDauNamCanCuQuery> lstCanCu = _sktSoLieuChiTietCanCuDataService.FindCanCuSoNhuCau(lstIdChungTu, listIdMucLuc, idDonVi, loaiCanCu, namLamViec).ToList();

                if (DictionaryCanCu.TryGetValue(cc.IIDMaChucNang, out string position))
                {
                    Items.ForAll(x =>
                    {
                        if (x?.GetType().GetProperty($"X{position}").GetValue(x, null) is SktSoLieuChiTietCanCuDetail muc)
                        {
                            muc = new SktSoLieuChiTietCanCuDetail();
                        }
                    });

                    IsInit = true;
                    Parallel.ForEach(lstCanCu, item =>
                    {
                        SktSoLieuChiTietMLNSModel mucLuc = Items.FirstOrDefault(x => x.XauNoiMa.Equals(item.XauNoiMa));
                        if (mucLuc?.GetType().GetProperty($"X{position}").GetValue(mucLuc, null) is SktSoLieuChiTietCanCuDetail muc)
                        {
                            mucLuc.IsUpdateCanCu = true;
                            mucLuc.IsModified = true;
                            muc.TuChi = item.TuChi;
                            muc.HangNhap = item.HangNhap;
                            muc.HangMua = item.HangMua;
                            muc.PhanCap = item.PhanCap;
                            muc.Loai = cc.IIDMaChucNang;
                            muc.IdCanCu = cc.Id;

                            if (LoaiChungTu == VoucherType.NSBD_Key && (loaiCanCu == 2 || loaiCanCu == 3))
                            {
                                if (item.XauNoiMa.StartsWith(LNSValue.LNS_1040200) || item.XauNoiMa.StartsWith(LNSValue.LNS_1040100))
                                {
                                    muc.HangMua = 0;
                                    muc.PhanCap = 0;
                                }
                                else if (item.XauNoiMa.StartsWith(LNSValue.LNS_1040300))
                                {
                                    muc.HangNhap = 0;
                                    muc.PhanCap = 0;
                                }
                            }
                        }
                    });
                    IsInit = false;
                }
            }
            CalculateData();
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void CalculateDataCanCu(List<CanCuDuToanQtCpSoNhuCauQuery> items)
        {
            items.Where(x => x.BHangCha.GetValueOrDefault()).ToList();
            IEnumerable<CanCuDuToanQtCpSoNhuCauQuery> temp = items.Where(x => !x.BHangCha.GetValueOrDefault());
            foreach (CanCuDuToanQtCpSoNhuCauQuery item in temp)
            {
                CalculateParentCanCu(item.IdMlnsCha, items, item);
            }
        }

        private void CalculateParentCanCu(Guid? idParent, List<CanCuDuToanQtCpSoNhuCauQuery> listData, CanCuDuToanQtCpSoNhuCauQuery item)
        {
            CanCuDuToanQtCpSoNhuCauQuery model = listData.FirstOrDefault(x => x.IdMlns == idParent.GetValueOrDefault());
            if (model == null) return;
            model.TuChi += item.TuChi;
            model.HangNhap += item.HangNhap;
            model.HangMua += item.HangMua;
            model.PhanCap += item.PhanCap;
            model.MuaHangHienVat += item.MuaHangHienVat;
            model.DacThu += item.DacThu;
            CalculateParentCanCu(model.IdMlnsCha, listData, item);
        }

        public List<NsMucLucNganSach> FindListParentMucLucByChildNamTruoc(List<Guid> listIdMucLuc, int namLamViec)
        {
            List<NsMucLucNganSach> listMLNS = _nsMucLucNganSachService.FindAll(namLamViec).ToList();
            List<string> listMLNSExist = listMLNS
                .Where(x => listIdMucLuc.Contains(x.MlnsId))
                .Select(x => x.XauNoiMa)
                .ToList();

            List<NsMucLucNganSach> listMLNSFull = listMLNS.Where(x => listMLNSExist.Any(y => y.StartsWith(x.XauNoiMa))).ToList();
            listMLNSFull = listMLNSFull.GroupBy(x => x.MlnsId).Select(x => x.FirstOrDefault()).OrderBy(x => x.XauNoiMa).ToList();
            return listMLNSFull;
        }

        private List<CanCuDuToanQtCpSoNhuCauQuery> GetCanCuDuToanQtCp(string lstChungTu, string lstIdMucLuc, string idDonVi, string loaiCanCu,
            int namLamViec)
        {
            List<CanCuDuToanQtCpSoNhuCauQuery> lstCanCu = _sktChungTuChiTietService
                .FindCanCuDuToanSoNhuCau(lstChungTu, lstIdMucLuc, idDonVi, loaiCanCu, namLamViec).ToList();
            List<Guid> lstIdMlns = lstCanCu.Select(x => x.IdMlns).Distinct().ToList();
            List<NsMucLucNganSach> sktMucLucs = FindListParentMucLucByChildNamTruoc(lstIdMlns, namLamViec);
            foreach (NsMucLucNganSach mlc in sktMucLucs)
            {
                if (!lstIdMlns.Contains(mlc.MlnsId))
                {
                    CanCuDuToanQtCpSoNhuCauQuery mlCha = new CanCuDuToanQtCpSoNhuCauQuery
                    {
                        IdMlns = mlc.MlnsId,
                        IdMlnsCha = mlc.MlnsIdParent,
                        SXauNoiMa = mlc.XauNoiMa,
                        BHangCha = mlc.BHangCha
                    };
                    lstCanCu.Add(mlCha);
                }
            }
            CalculateDataCanCu(lstCanCu);
            return lstCanCu;
        }

        private void LoadCanCuDuToanDefault(CauHinhCanCuModel cancu)
        {
            string idDonVi = Model.Id_DonVi;
            int loaiChungTu = int.Parse(LoaiChungTu);
            int? namChungTu = cancu.INamCanCu;
            System.Linq.Expressions.Expression<Func<NsDtChungTu, bool>> predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(item => item.ILoaiChungTu == loaiChungTu);
            predicate = predicate.And(item => item.ILoaiDuToan == (int)BudgetType.YEAR);
            predicate = predicate.And(item => true.Equals(item.BKhoa));

            if (idDonVi.Equals(_sessionService.Current.IdDonVi))
            {
                predicate = predicate.And(item => item.ILoai == SoChungTuType.ReceiveEstimate);
            }
            else
            {
                predicate = predicate.And(item => item.ILoai == SoChungTuType.EstimateDivision);
                predicate = predicate.And(item => item.SDsidMaDonVi.Contains(idDonVi));
            }

            List<NsDtChungTu> listCTCanCu = _iDtChungTuService.FindByCondition(predicate).ToList();
            List<CanCuDuToanQtCpSoNhuCauQuery> lstChiTiet = new List<CanCuDuToanQtCpSoNhuCauQuery>();


            if (listCTCanCu.Any())
            {
                string llstIdCt = string.Join(",", listCTCanCu.Select(x => x.Id));
                lstChiTiet = GetCanCuDuToanQtCp(llstIdCt, "-1", idDonVi, cancu.IIDMaChucNang, namChungTu.GetValueOrDefault());
            }
            else
            {
                bool conditionGetChild = IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI;
                conditionGetChild = conditionGetChild && !string.IsNullOrEmpty(Model.DSSoChungTuTongHop);

                if (conditionGetChild)
                {
                    string listChildIdChungTu = GetIdChungTuChild(idDonVi);
                    string listChildIdMaDonViChungTu = GetIdMaDonViChungTuChild(idDonVi);
                    lstChiTiet = GetCanCuDuToanQtCp(listChildIdChungTu, "-1", listChildIdMaDonViChungTu, cancu.IIDMaChucNang, namChungTu.GetValueOrDefault());
                }
                else return;
            }

            foreach (CanCuDuToanQtCpSoNhuCauQuery cc in lstChiTiet)
            {
                SktSoLieuChiTietMLNSModel mucLuc = Items.FirstOrDefault(x => x.XauNoiMa.Equals(cc.SXauNoiMa));
                if (mucLuc != null)
                {
                    if (DictionaryCanCu.TryGetValue(cancu.IIDMaChucNang, out string position))
                    {
                        if (mucLuc.GetType().GetProperty($"X{position}").GetValue(mucLuc, null) is SktSoLieuChiTietCanCuDetail canCu)
                        {
                            mucLuc.IsUpdateCanCu = true;
                            canCu.TuChi = cc.TuChi;
                            canCu.HangNhap = cc.HangNhap;
                            canCu.HangMua = cc.HangMua;
                            canCu.PhanCap = cc.PhanCap;
                            if (cancu.IIDMaChucNang == TypeCanCu.ESTIMATE || cancu.IIDMaChucNang == TypeCanCu.SETTLEMENT)
                            {
                                if (mucLuc.XauNoiMa.StartsWith(LNSValue.LNS_1040200) || mucLuc.XauNoiMa.StartsWith(LNSValue.LNS_1040100))
                                {
                                    canCu.HangMua = 0;
                                    canCu.IsHangCha = mucLuc.IsHangCha;
                                    // Trường hợp nhập tự chi mà không nhập hàng nhập thì set fHangNhap = Tuchi
                                    if (canCu.HangNhap == 0 && cc.TuChi != 0) canCu.HangNhap = cc.TuChi;
                                }
                                else if (mucLuc.XauNoiMa.StartsWith(LNSValue.LNS_1040300))
                                {
                                    canCu.HangNhap = 0;
                                    canCu.IsHangCha = mucLuc.IsHangCha;
                                    // Trường hợp nhập tự chi mà không nhập hàng mua thì set fHangMua = Tuchi
                                    if (canCu.HangMua == 0 && cc.TuChi != 0) canCu.HangMua = cc.TuChi;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadCanCuQuyetToanDefault(CauHinhCanCuModel cancu)
        {
            string idDonVi = Model.Id_DonVi;
            int? namChungTu = cancu.INamCanCu;
            System.Linq.Expressions.Expression<Func<NsQtChungTu, bool>> predicate = PredicateBuilder.True<NsQtChungTu>();
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(item => true.Equals(item.BKhoa));
            predicate = predicate.And(item => item.IIdMaDonVi == idDonVi);

            List<NsQtChungTu> listCTCanCu = _iQtChungTuService.FindByCondition(predicate).ToList();
            List<CanCuDuToanQtCpSoNhuCauQuery> lstChiTiet = new List<CanCuDuToanQtCpSoNhuCauQuery>();


            if (listCTCanCu.Any())
            {
                string llstIdCt = string.Join(",", listCTCanCu.Select(x => x.Id));
                lstChiTiet = GetCanCuDuToanQtCp(llstIdCt, "-1", idDonVi, cancu.IIDMaChucNang, namChungTu.GetValueOrDefault());
            }
            else
            {
                bool conditionGetChild = IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI;
                conditionGetChild = conditionGetChild && !string.IsNullOrEmpty(Model.DSSoChungTuTongHop);

                if (conditionGetChild)
                {
                    string listChildIdChungTu = GetIdChungTuChild(idDonVi);
                    string listChildIdMaDonViChungTu = GetIdMaDonViChungTuChild(idDonVi);
                    lstChiTiet = GetCanCuDuToanQtCp(listChildIdChungTu, "-1", listChildIdMaDonViChungTu, cancu.IIDMaChucNang, namChungTu.GetValueOrDefault());
                }
                else return;
            }

            foreach (CanCuDuToanQtCpSoNhuCauQuery cc in lstChiTiet)
            {
                SktSoLieuChiTietMLNSModel mucLuc = Items.FirstOrDefault(x => x.XauNoiMa.Equals(cc.SXauNoiMa));
                if (mucLuc != null)
                {
                    if (DictionaryCanCu.TryGetValue(cancu.IIDMaChucNang, out string position))
                    {
                        if (mucLuc.GetType().GetProperty($"X{position}").GetValue(mucLuc, null) is SktSoLieuChiTietCanCuDetail canCu)
                        {
                            mucLuc.IsUpdateCanCu = true;
                            canCu.TuChi = cc.TuChi;
                            canCu.HangNhap = cc.HangNhap;
                            canCu.HangMua = cc.HangMua;
                            canCu.PhanCap = cc.PhanCap;
                            if (cancu.IIDMaChucNang == TypeCanCu.ESTIMATE || cancu.IIDMaChucNang == TypeCanCu.SETTLEMENT)
                            {
                                if (mucLuc.XauNoiMa.StartsWith(LNSValue.LNS_1040200) || mucLuc.XauNoiMa.StartsWith(LNSValue.LNS_1040100))
                                {
                                    canCu.HangMua = 0;
                                    canCu.IsHangCha = mucLuc.IsHangCha;
                                    // Trường hợp nhập tự chi mà không nhập hàng nhập thì set fHangNhap = Tuchi
                                    if (canCu.HangNhap == 0 && cc.TuChi != 0) canCu.HangNhap = cc.TuChi;
                                }
                                else if (mucLuc.XauNoiMa.StartsWith(LNSValue.LNS_1040300))
                                {
                                    canCu.HangNhap = 0;
                                    canCu.IsHangCha = mucLuc.IsHangCha;
                                    // Trường hợp nhập tự chi mà không nhập hàng mua thì set fHangMua = Tuchi
                                    if (canCu.HangMua == 0 && cc.TuChi != 0) canCu.HangMua = cc.TuChi;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadCanCuVisibility()
        {
            for (int i = 1; i <= 5; i++)
            {
                this.GetType().GetProperty($"TC{i}Visible").SetValue(this, Visibility.Collapsed);
                this.GetType().GetProperty($"HangMua{i}Visible").SetValue(this, Visibility.Collapsed);
                this.GetType().GetProperty($"HangNhap{i}Visible").SetValue(this, Visibility.Collapsed);
                this.GetType().GetProperty($"PhanCap{i}Visible").SetValue(this, Visibility.Collapsed);
                this.GetType().GetProperty($"IsReadOnlyX{i}").SetValue(this, true);
            }
        }

        private void LoadCanCu()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            System.Linq.Expressions.Expression<Func<NsCauHinhCanCu, bool>> predicate = PredicateBuilder.True<NsCauHinhCanCu>();

            predicate = predicate.And(item => item.SModule == TypeModuleCanCu.PLAN_BEGIN_YEAR);

            predicate = predicate.And(item => item.INamLamViec == yearOfWork);
            IOrderedEnumerable<NsCauHinhCanCu> listCanCu = _iCauHinhCanCuService.FindByCondition(predicate).OrderBy(n => n.INamCanCu);
            ObservableCollection<CauHinhCanCuModel> cauHinhCanCu = _mapper.Map<ObservableCollection<CauHinhCanCuModel>>(listCanCu);
            _lstCanCuInit = cauHinhCanCu;
            System.Linq.Expressions.Expression<Func<NsDtdauNamChungTuChiTiet, bool>> predicate1 = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();
            predicate1 = predicate1.And(x => x.IIdCtdtdauNam == Model.Id);
            IEnumerable<NsDtdauNamChungTuChiTiet> listChungTuChiTiet = _sktSoLieuService.FindByCondition(predicate1);
            foreach (CauHinhCanCuModel item in cauHinhCanCu)
            {
                if (!DictionaryCanCu.TryGetValue(item.IIDMaChucNang, out string position)) return;
                if (LoaiChungTu == VoucherType.NSSD_Key)
                {
                    this.GetType().GetProperty($"TC{position}Visible").SetValue(this, Visibility.Visible);
                    this.GetType().GetProperty($"TC{position}").SetValue(this, item?.STenCot ?? string.Empty);
                }
                else
                {
                    this.GetType().GetProperty($"HangNhap{position}Visible").SetValue(this, Visibility.Visible);
                    this.GetType().GetProperty($"HangMua{position}Visible").SetValue(this, Visibility.Visible);
                    this.GetType().GetProperty($"PhanCap{position}Visible").SetValue(this, Visibility.Visible);

                    this.GetType().GetProperty($"HangNhap{position}").SetValue(this, item != null ? $"{item.STenCot} hàng nhập" : string.Empty);
                    this.GetType().GetProperty($"HangMua{position}").SetValue(this, item != null ? $"{item.STenCot} hàng mua" : string.Empty);
                    this.GetType().GetProperty($"PhanCap{position}").SetValue(this, item != null ? $"{item.STenCot} phân cấp" : string.Empty);
                }

                if (item?.BChinhSua ?? false)
                {
                    this.GetType().GetProperty($"IsReadOnlyX{position}").SetValue(this, false);
                }

                System.Linq.Expressions.Expression<Func<NsDtdauNamChungTuChiTietCanCu, bool>> predicateCc = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
                if (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    predicateCc = predicateCc.And(x => GetIdChungTuChild(Model.Id_DonVi).Contains(x.IID_CTDTDauNam.ToString()));
                }
                else
                {
                    predicateCc = predicateCc.And(x => x.IID_CTDTDauNam == Model.Id);
                    predicateCc = predicateCc.And(x => x.IIdMaDonVi.Equals(Model.Id_DonVi));
                }
                predicateCc = predicateCc.And(x => x.IIdCanCu.HasValue && x.IIdCanCu.Equals(item.Id));
                predicateCc = predicateCc.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                //predicateCc = predicateCc.And(x => x.LoaiChungTu == loaiChungTu);
                IEnumerable<NsDtdauNamChungTuChiTietCanCu> lstCanCu = _sktSoLieuChiTietCanCuDataService.FindByCondition(predicateCc);

                if (!listChungTuChiTiet.Any())
                {
                    if (!lstCanCu.Any() && item.IIDMaChucNang.Equals(TypeCanCu.ESTIMATE) && item.INamCanCu == yearOfWork - 1)
                    {
                        LoadCanCuDuToanDefault(item);
                    }
                    if (!lstCanCu.Any() && item.IIDMaChucNang.Equals(TypeCanCu.SETTLEMENT) && item.INamCanCu == yearOfWork - 2)
                    {
                        LoadCanCuQuyetToanDefault(item);
                    }
                }

                IsGetCanCu = true;
                var dict1 = Items.GroupBy(x => new { x.XauNoiMa, x.IdDonVi }).ToDictionary(x => x.Key, x => x.FirstOrDefault());
                var dict2 = Items.GroupBy(x => x.XauNoiMa).ToDictionary(x => x.Key, x => x.FirstOrDefault());
                lstCanCu.AsParallel().ForAll(cc =>
                {
                    SktSoLieuChiTietMLNSModel mucLuc = new SktSoLieuChiTietMLNSModel();
                    if (IsShowTypeAgency && SelectedTypeShowAgency?.ValueItem == TypeDisplay.CHITIET_DONVI)
                    {
                        dict1.TryGetValue(new { XauNoiMa = cc.SXauNoiMa, IdDonVi = cc.IIdMaDonVi }, out mucLuc);
                    }
                    else
                    {
                        dict2.TryGetValue(cc.SXauNoiMa, out mucLuc);
                    }
                    if (mucLuc is { })
                    {
                        object volume = mucLuc.GetType().GetProperty($"X{position}").GetValue(mucLuc, null);
                        if (volume is SktSoLieuChiTietCanCuDetail vol)
                        {
                            vol.TuChi = cc.FTuChi;
                            vol.HangNhap = cc.FHangNhap;
                            vol.HangMua = cc.FHangMua;
                            vol.PhanCap = cc.FPhanCap;
                            vol.ChuaPhanCap = cc.FChuaPhanCap;
                        }
                    }
                });
                IsGetCanCu = false;
            }
        }

        private void CalculateTotal()
        {
            List<SktSoLieuChiTietMLNSModel> listChildren = Items.Where(x => x.IsFilter && !x.IsHangCha && !x.IsDeleted).ToList();
            Model.TotalMucTienPhanBo = listChildren.Sum(n => n.MucTienPhanBo);
            Model.TotalChiTiet = listChildren.Sum(n => n.ChiTiet);
            Model.TotalUocThucHien = listChildren.Sum(n => n.UocThucHien);
            Model.TotalHangNhap = listChildren.Sum(n => n.HangNhap);
            Model.TotalHangMua = listChildren.Sum(n => n.HangMua);
            Model.TotalPhanCap = listChildren.Sum(n => n.PhanCap);
            Model.TotalChuaPhanCap = listChildren.Sum(n => n.ChuaPhanCap);
            Model.TotalPhanCapConLai = listChildren.Sum(n => n.PhanCapConLai);

            for (int i = 1; i <= 5; i++)
            {
                Model.GetType().GetProperty($"X{i}").SetValue(Model, new ChiTietDuToanDauNamCanCuTong());
                if (Model.GetType().GetProperty($"X{i}").GetValue(Model, null) is ChiTietDuToanDauNamCanCuTong item)
                {
                    item.TongTuChi = listChildren.Sum(n =>
                    {
                        if (n.GetType().GetProperty($"X{i}").GetValue(n, null) is SktSoLieuChiTietCanCuDetail child) return child.TuChi;
                        else return 0;
                    });
                    item.TongHangNhap = listChildren.Sum(n =>
                    {
                        if (n.GetType().GetProperty($"X{i}").GetValue(n, null) is SktSoLieuChiTietCanCuDetail child) return child.HangNhap;
                        else return 0;
                    });
                    item.TongHangMua = listChildren.Sum(n =>
                    {
                        if (n.GetType().GetProperty($"X{i}").GetValue(n, null) is SktSoLieuChiTietCanCuDetail child) return child.HangMua;
                        else return 0;
                    });
                    item.TongPhanCap = listChildren.Sum(n =>
                    {
                        if (n.GetType().GetProperty($"X{i}").GetValue(n, null) is SktSoLieuChiTietCanCuDetail child) return child.PhanCap;
                        else return 0;
                    });
                    item.TongPhanCapConLai = listChildren.Sum(n =>
                    {
                        if (n.GetType().GetProperty($"X{i}").GetValue(n, null) is SktSoLieuChiTietCanCuDetail child) return child.PhanCapConLai;
                        else return 0;
                    });
                }
            }
        }

        private void CalculateTotalMucLuc()
        {
            List<SktMucLucDuToanDauNamModel> listChildren = _dataMucLuc.Where(x => !x.IsHangCha && !x.IsDeleted).ToList();
            Model.TotalMucLuc = listChildren.Sum(n => n.TuChi);
            Model.TotalMucLucConLai = listChildren.Sum(n => n.ConLai);
            Model.TotalMucLucConLaiHang = listChildren.Sum(n => n.ConLaiHang);
            Model.TotalMucLucConLaiDacThu = listChildren.Sum(n => n.ConLaiDacThu);
            Model.TotalMucLucHangNhap = listChildren.Sum(n => n.HangNhap);
            Model.TotalMucLucHangMua = listChildren.Sum(n => n.HangMua);
            Model.TotalMucLucPhanCap = listChildren.Sum(n => n.PhanCap);
            Model.TotalMucLucMuaHangHienVat = listChildren.Sum(n => n.MuaHangHienVat);
            Model.TotalMucLucDacThu = listChildren.Sum(n => n.DacThu);

            Model.TotalDtTuChi = listChildren.Sum(n => n.DtTuChi);
            Model.TotalHangDuToan = listChildren.Sum(n => n.DtHangMua) + listChildren.Sum(n => n.DtHangNhap);
            Model.TotalDtPhanCap = listChildren.Sum(n => n.DtPhanCap);
        }

        private void OnCloseWindow()
        {
            ClosePopup?.Invoke(this, new EventArgs());
        }

        public void OnRefeshIndexWindow()
        {
            RefeshIndexWindow?.Invoke(this, new EventArgs());
        }

        private void OnShowPopupReportChiTiet(object param)
        {
            string printType = (string)param;
            PrintReportChiTietDuToanDonViViewModel.LoaiChungTu = LoaiChungTu;
            PrintReportChiTietDuToanDonViViewModel.TypeReport = printType;
            PrintReportChiTietDuToanDonViViewModel.Init();
            PrintReportChiTietDuToanDonVi view = new PrintReportChiTietDuToanDonVi()
            {
                DataContext = PrintReportChiTietDuToanDonViViewModel
            };
            DialogHost.Show(view, "PlanBeginYearDetailDialog", null, null);
        }

        public void OnShowPopupChild()
        {
            if (LoaiChungTu == VoucherType.NSSD_Key || SelectedItem == null || SelectedItem.IsHangCha ||
                //SelectedItem.IsDeleted || SelectedItem.IdDb == null || SelectedItem.IdDb == Guid.Empty || SelectedItem.PhanCap == 0)
                SelectedItem.IsDeleted || SelectedItem.PhanCap == 0)
                return;
            //base.OnSelectionDoubleClick(obj);
            OpenDetailDialog();
        }

        private void OpenDetailDialog()
        {
            if (SelectedItem == null)
                return;
            List<NsMucLucNganSach> mucluc = _nsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
            if (mucluc.Where(n => n.XauNoiMa == SelectedItem.XauNoiMa.Replace("1040100", "1020100")).ToList().Count == 0)
                return;
            PlanBeginYearDetailChildViewModel.Model = Model;
            PlanBeginYearDetailChildViewModel.XauNoiMa = SelectedItem.XauNoiMa.Replace("1040100", "1020100");
            PlanBeginYearDetailChildViewModel.ListXauNoiMa = string.Join(",", StringUtils.SplitXauNoiMaParent(SelectedItem.XauNoiMa.Replace("1040100", "1020100")));
            PlanBeginYearDetailChildViewModel.IdChiTiet = SelectedItem.IdDb.ToString();
            PlanBeginYearDetailChildViewModel.TotalGlobal = SelectedItem.PhanCap;
            PlanBeginYearDetailChildViewModel.Id_DonVi = Model.Id_DonVi;
            PlanBeginYearDetailChildViewModel.TenDonVi = Model.TenDonVi;
            PlanBeginYearDetailChildViewModel.IsReadOnlyTable = IsReadOnlyTable;
            PlanBeginYearDetailChildViewModel.sXauNoiMaGoc = SelectedItem.XauNoiMa;
            PlanBeginYearDetailChildViewModel.Init();
            PlanBeginYearDetailChild view = new PlanBeginYearDetailChild() { DataContext = PlanBeginYearDetailChildViewModel };
            view.ShowDialog();
        }

        private void OnShowPopupReportCompare(object param)
        {
            try
            {
                PrintReportCompareDemandCheckViewModel.LoaiChungTu = LoaiChungTu;
                PrintReportCompareDemandCheckViewModel.Init();
                PrintReportCompareDemandCheck view = new PrintReportCompareDemandCheck()
                {
                    DataContext = PrintReportCompareDemandCheckViewModel
                };
                System.Threading.Tasks.Task<object> result = DialogHost.Show(view, "PlanBeginYearDetailDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void GetDataUocThucHien(object obj)
        {
            if (obj is bool check)
            {
                int loai = 1;
                string sMaDonVi = Model.Id_DonVi;
                if (Model.Id_DonVi.Equals(_sessionService.Current.IdDonVi))
                {
                    loai = check ? 0 : loai;
                    sMaDonVi = check ? "-1" : sMaDonVi;
                }

                List<CanCuDuToanNamTruocQuery> lstCanCu = _sktSoLieuService
                    .FindCanCuSoLapDuToanDauNam(int.Parse(LoaiChungTu), loai, sMaDonVi, _sessionService.Current.YearOfWork - 1, 2, _sessionService.Current.Budget).ToList();

                IsInit = true;
                foreach (CanCuDuToanNamTruocQuery cc in lstCanCu)
                {
                    SktSoLieuChiTietMLNSModel mucLuc = Items.FirstOrDefault(item => item.MlnsId.Equals(cc.iID_MLNS));
                    if (mucLuc != null)
                    {
                        mucLuc.UocThucHien = cc.TuChi + cc.PhanCap + cc.HangNhap + cc.HangMua;
                        mucLuc.IsModified = true;
                    }
                }
                CalculateData();
                IsInit = false;
                OnPropertyChanged(nameof(IsSaveData));
            }

        }

        public void CopyDuToanNamTruocIntoUocThucHien()
        {
            IsInit = true;

            if (DictionaryCanCu.TryGetValue(TypeCanCu.ESTIMATE, out string position))
            {
                if (LoaiChungTu == VoucherType.NSSD_Key)
                {
                    Items.Where(x => x.UocThucHien != 0).Select(x =>
                    {
                        x.UocThucHien = 0;
                        x.IsModified = true;
                        return x;
                    }).ToList();
                    Items.Where(x =>
                    {
                        if (x.GetType().GetProperty($"X{position}").GetValue(x, null) is SktSoLieuChiTietCanCuDetail chiTiet)
                        {
                            return chiTiet.TuChi != 0 && !x.IsHangCha;
                        }
                        else { return false; }
                    }).Select(x =>
                    {
                        if (x.GetType().GetProperty($"X{position}").GetValue(x, null) is SktSoLieuChiTietCanCuDetail chiTiet)
                        {
                            x.UocThucHien = chiTiet.TuChi;
                            x.IsModified = true;
                        }
                        return x;
                    }).ToList();
                }
                else
                {
                    Items.Where(x =>
                    {
                        if (x.GetType().GetProperty($"X{position}").GetValue(x, null) is SktSoLieuChiTietCanCuDetail chiTiet)
                        {
                            return (chiTiet.HangNhap != 0 || chiTiet.HangMua != 0 || chiTiet.PhanCap != 0) && !x.IsHangCha;
                        }
                        else { return false; }
                    }).Select(x =>
                    {
                        if (x.GetType().GetProperty($"X{position}").GetValue(x, null) is SktSoLieuChiTietCanCuDetail chiTiet)
                        {
                            x.UocThucHien = chiTiet.HangNhap + chiTiet.HangMua + chiTiet.PhanCap;
                            x.IsModified = true;
                        }
                        return x;
                    }).ToList();
                }
            }

            CalculateData();
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));

            IsInit = false;
        }
    }
}
