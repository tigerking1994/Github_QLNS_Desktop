using AutoMapper;
using ControlzEx.Standard;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Salary.Cadres;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.ViewModel.Salary.Cadres.CadresBHXH;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Cadres
{
    public class CadresDialogViewModel : DialogViewModelBase<CadresModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmPhuCapService _phuCapService;
        private readonly ITlDmCanBoService _cadresService;
        private readonly ITlDmCapBacService _tlDmCapBacService;
        private readonly ITlDmChucVuService _tlDmChucVuService;
        private readonly ITlDmTangGiamService _tlDmTangGiamService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly INsQsMucLucService _qsMucLucService;
        private readonly ITlDmCapBacKeHoachService _tlDmCapBacKeHoachService;
        private readonly ITlDsCapNhapBangLuongService _tlDsCapNhapBangLuongService;
        private readonly ITlBangLuongThangService _tlBangLuongThangService;
        private readonly ITlDmCachTinhLuongChuanService _tlDmCachTinhLuongChuanService;
        private readonly ITlDmHslKeHoachService _tlDmHslKeHoachService;
        private readonly ITlCanBoCheDoBHXHService _tlCanBoCheDoBHXHService;
        private readonly TlDmCheDoBHXHService _tlDmCheDoBHXHService;
        private readonly ITlDmNgayNghiService _tTlDmNgayNghiService;
        private readonly ITlBangLuongThangBHXHService _iTlBangLuongThangBHXHService;
        private readonly ITlDmCachTinhLuongBaoHiemService _tlDmCachTinhLuongBaoHiemService;
        private readonly ITlCanBoCheDoBHXHChiTietService _canBoCheDoBHXHChiTietService;
        private ICollectionView _phuCapView;
        private ICollectionView _canBoSearchView;
        private ICollectionView _cheDoView;
        public override string FuncCode => NSFunctionCode.SALARY_CADRES_DETAIL;

        public override Type ContentType => typeof(View.Salary.Cadres.CadresDetail);
        public override PackIconKind IconKind => PackIconKind.AccountDetails;
        public override string Title => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI ĐỐI TƯỢNG HƯỞNG LƯƠNG, PHỤ CẤP" : "CẬP NHẬT ĐỐI TƯỢNG HƯỞNG LƯƠNG, PHỤ CẤP";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới đối tượng hưởng lương, phụ cấp" : "Cập nhật đối tượng hưởng lương, phụ cấp";
        private int? _iBaseSalaryMonth;
        private int? _iBaseSalaryYear;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        public bool IsFirst { get; set; }
        private bool _isModifiedBHXH;
        public CadresDetail CadresDetail { get; set; }
        public List<DateTime?> LstHoliday { get; set; }
        private string _lstMaCheDoBHXH;
        private ObservableCollection<TlCanBoCheDoBHXHModel> _dataCanBoCheDoBHXH = new ObservableCollection<TlCanBoCheDoBHXHModel>();
        public ObservableCollection<TlCanBoCheDoBHXHModel> DataCanBoCheDoBHXH
        {
            get => _dataCanBoCheDoBHXH;
            set => SetProperty(ref _dataCanBoCheDoBHXH, value);
        }
        private ObservableCollection<TlCanBoCheDoBHXHModel> _allDataCanBoCheDoBHXH = new ObservableCollection<TlCanBoCheDoBHXHModel>();
        public ObservableCollection<TlCanBoCheDoBHXHModel> AllDataCanBoCheDoBHXH
        {
            get => _allDataCanBoCheDoBHXH;
            set => SetProperty(ref _allDataCanBoCheDoBHXH, value);
        }
        private TlCanBoCheDoBHXHModel _selectedCanBoCheDoBHXH;
        public TlCanBoCheDoBHXHModel SelectedCanBoCheDoBHXH
        {
            get => _selectedCanBoCheDoBHXH;
            set
            {
                SetProperty(ref _selectedCanBoCheDoBHXH, value);
            }
        }
        private List<ComboboxItem> _cbxNamCanCuDong;
        public List<ComboboxItem> CbxNamCanCuDong
        {
            get => _cbxNamCanCuDong;
            set => SetProperty(ref _cbxNamCanCuDong, value);
        }

        private List<ComboboxItem> _cbxThangLuongCanCuDong;
        public List<ComboboxItem> CbxThangLuongCanCuDong
        {
            get => _cbxThangLuongCanCuDong;
            set => SetProperty(ref _cbxThangLuongCanCuDong, value);
        }

        private ComboboxItem _cbxThangLuongCanCuDongSelected;
        public ComboboxItem CbxThangLuongCanCuDongSelected
        {
            get => _cbxThangLuongCanCuDongSelected;
            set
            {
                SetProperty(ref _cbxThangLuongCanCuDongSelected, value);
            }
        }
        private ObservableCollection<AllowenceModel> _itemsAllowence;
        public ObservableCollection<AllowenceModel> ItemsAllowence
        {
            get => _itemsAllowence;
            set => SetProperty(ref _itemsAllowence, value);
        }

        private AllowenceModel _selectedAllowence;
        public AllowenceModel SelectedAllowence
        {
            get => _selectedAllowence;
            set
            {
                if (SetProperty(ref _selectedAllowence, value) && _selectedAllowence != null)
                {
                    SelectedAllowenceHuong = ItemsAllowenceHuong.FirstOrDefault(x => x.MaPhuCap.Equals(_selectedAllowence.MaPhuCap));
                }
            }
        }

        private ObservableCollection<AllowenceModel> _itemsAllowenceHuong;
        public ObservableCollection<AllowenceModel> ItemsAllowenceHuong
        {
            get => _itemsAllowenceHuong;
            set => SetProperty(ref _itemsAllowenceHuong, value);
        }

        private AllowenceModel _selectedAllowenceHuong;
        public AllowenceModel SelectedAllowenceHuong
        {
            get => _selectedAllowenceHuong;
            set => SetProperty(ref _selectedAllowenceHuong, value);
        }

        private FormViewState _viewState;
        public FormViewState ViewState
        {
            get => _viewState;
            set
            {
                SetProperty(ref _viewState, value);
                OnPropertyChanged(nameof(IsReadOnly));
                OnPropertyChanged(nameof(IsEditVisibility));
                OnPropertyChanged(nameof(IsAddVisibility));
            }
        }

        public bool IsAddVisibility => ViewState == FormViewState.ADD;

        private LoaiSave _loaiSave;
        public LoaiSave LoaiSave
        {
            get => _loaiSave;
            set => SetProperty(ref _loaiSave, value);
        }

        private DateTime? _ngayNhapNgu;
        public DateTime? NgayNhapNgu
        {
            get => _ngayNhapNgu;
            set
            {
                SetProperty(ref _ngayNhapNgu, value);
                TinhNamThamNien();
            }
        }

        private DateTime? _ngayXuatNgu;
        public DateTime? NgayXuatNgu
        {
            get => _ngayXuatNgu;
            set
            {
                SetProperty(ref _ngayXuatNgu, value);
                TinhNamThamNien();
            }
        }

        private DateTime? _ngayTaiNgu;
        public DateTime? NgayTaiNgu
        {
            get => _ngayTaiNgu;
            set
            {
                SetProperty(ref _ngayTaiNgu, value);
                TinhNamThamNien();
            }
        }

        private int _namThamNien;
        public int NamThamNien
        {
            get => _namThamNien;
            set => SetProperty(ref _namThamNien, value);
        }

        private int _thangThamNienNghe;
        public int ThangThamNienNghe
        {
            get => _thangThamNienNghe;
            set
            {
                SetProperty(ref _thangThamNienNghe, value);
                TinhNamThamNien();
            }
        }

        public bool IsReadOnly => _viewState == FormViewState.DETAIL;
        public bool ChooseEnabled => SelectedCanBo != null;

        private ObservableCollection<TlDmCapBacModel> _itemsCapBac;
        public ObservableCollection<TlDmCapBacModel> ItemsCapBac
        {
            get => _itemsCapBac;
            set => SetProperty(ref _itemsCapBac, value);
        }

        private TlDmCapBacModel _selectedCapBac;
        public TlDmCapBacModel SelectedCapBac
        {
            get => _selectedCapBac;
            set
            {
                if (SetProperty(ref _selectedCapBac, value))
                {
                    LoadHeSoLuong();
                    LoadTangQuanHam(_selectedCapBac.MaCb);
                }
            }
        }

        private ObservableCollection<TlDmCapBacKeHoachModel> _itemsCapBacKeHoach;
        public ObservableCollection<TlDmCapBacKeHoachModel> ItemsCapBacKeHoach
        {
            get => _itemsCapBacKeHoach;
            set => SetProperty(ref _itemsCapBacKeHoach, value);
        }

        private TlDmCapBacKeHoachModel _selectedCapBacKeHoach;
        public TlDmCapBacKeHoachModel SelectedCapBacKeHoach
        {
            get => _selectedCapBacKeHoach;
            set => SetProperty(ref _selectedCapBacKeHoach, value);
        }

        private TlDmCapBacKeHoachModel _selectedCapBacKeHoachTran;
        public TlDmCapBacKeHoachModel SelectedCapBacKeHoachTran
        {
            get => _selectedCapBacKeHoachTran;
            set => SetProperty(ref _selectedCapBacKeHoachTran, value);
        }

        private ObservableCollection<TlDmHslKeHoachModel> _itemsHslKeHoach;
        public ObservableCollection<TlDmHslKeHoachModel> ItemsHslKeHoach
        {
            get => _itemsHslKeHoach;
            set => SetProperty(ref _itemsHslKeHoach, value);
        }

        private TlDmHslKeHoachModel _selectedHslKeHoach;
        public TlDmHslKeHoachModel SelectedHslKeHoach
        {
            get => _selectedHslKeHoach;
            set => SetProperty(ref _selectedHslKeHoach, value);
        }

        private TlDmHslKeHoachModel _selectedHslKeHoachTran;
        public TlDmHslKeHoachModel SelectedHslKeHoachTran
        {
            get => _selectedHslKeHoachTran;
            set => SetProperty(ref _selectedHslKeHoachTran, value);
        }

        private ObservableCollection<TlDmChucVuModel> _itemsChucVu;
        public ObservableCollection<TlDmChucVuModel> ItemsChucVu
        {
            get => _itemsChucVu;
            set => SetProperty(ref _itemsChucVu, value);
        }

        private TlDmChucVuModel _selectedChucVu;
        public TlDmChucVuModel SelectedChucVu
        {
            get => _selectedChucVu;
            set
            {
                SetProperty(ref _selectedChucVu, value);
                LoadHeSoChucVu();
            }
        }

        private ObservableCollection<TlDmTangGiamModel> _itemsTangGiam;
        public ObservableCollection<TlDmTangGiamModel> ItemsTangGiam
        {
            get => _itemsTangGiam;
            set => SetProperty(ref _itemsTangGiam, value);
        }

        private ObservableCollection<QsMucLucModel> _tangGiamItems;
        public ObservableCollection<QsMucLucModel> TangGiamItems
        {
            get => _tangGiamItems;
            set => SetProperty(ref _tangGiamItems, value);
        }

        private QsMucLucModel _selectedTangGiamItems;
        public QsMucLucModel SelectedTangGiamItems
        {
            get => _selectedTangGiamItems;
            set => SetProperty(ref _selectedTangGiamItems, value);
        }

        private string _selectedTangGiamIndex;
        public string SelectedTangGiamIndex
        {
            get => _selectedTangGiamIndex;
            set => SetProperty(ref _selectedTangGiamIndex, value);
        }

        public string ComboboxDisplayMemberPathTangGiam => nameof(SelectedTangGiamItems.TangGiam);

        private List<ComboboxItem> _gender;
        public List<ComboboxItem> GenderData
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        private ComboboxItem _selectedGender;
        public ComboboxItem SelectedGender
        {
            get => _selectedGender;
            set => SetProperty(ref _selectedGender, value);
        }

        private ObservableCollection<TlDmDonViModel> _donviData;
        public ObservableCollection<TlDmDonViModel> DonViItems
        {
            get => _donviData;
            set => SetProperty(ref _donviData, value);
        }

        private TlDmDonViModel _selectedDonVi;
        public TlDmDonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private ICollectionView _canBoView;
        public ICollectionView CanBoView
        {
            get => _canBoView;
            set
            {
                SetProperty(ref _canBoView, value);
            }
        }

        private Visibility _visibility;
        public Visibility Visibility
        {
            get => _visibility;
            set => SetProperty(ref _visibility, value);
        }

        public bool IsEditVisibility => ViewState != FormViewState.ADD;

        private bool _nextEnabled;
        public bool NextEnabled
        {
            get => _nextEnabled;
            set => SetProperty(ref _nextEnabled, value);
        }

        private bool _backEnabled;
        public bool BackEnabled
        {
            get => _backEnabled;
            set => SetProperty(ref _backEnabled, value);
        }

        private ObservableCollection<CadresModel> _lstCanBo;
        public ObservableCollection<CadresModel> LstCanBo
        {
            get => _lstCanBo;
            set
            {
                SetProperty(ref _lstCanBo, value);
                OnPropertyChanged(nameof(Model));
            }
        }

        private CadresModel _selectedCanBo;
        public CadresModel SelectedCanBo
        {
            get => _selectedCanBo;
            set
            {
                SetProperty(ref _selectedCanBo, value);
                OnPropertyChanged(nameof(ChooseEnabled));
            }
        }

        private string _searchMaPhuCap;
        public string SearchMaPhuCap
        {
            get => _searchMaPhuCap;
            set => SetProperty(ref _searchMaPhuCap, value);
        }

        private string _searchTenPhuCap;
        public string SearchTenPhuCap
        {
            get => _searchTenPhuCap;
            set => SetProperty(ref _searchTenPhuCap, value);
        }

        private string _searchMaCheDo;
        public string SearchMaCheDo
        {
            get => _searchMaCheDo;
            set => SetProperty(ref _searchMaCheDo, value);
        }

        private string _searchTenCheDo;
        public string SearchTenCheDo
        {
            get => _searchTenCheDo;
            set => SetProperty(ref _searchTenCheDo, value);
        }

        private string _searchCanbo;
        public string SearchCanBo
        {
            get => _searchCanbo;
            set => SetProperty(ref _searchCanbo, value);
        }

        private string _searchSoSoLuong;
        public string SearchSoSoLuong
        {
            get => _searchSoSoLuong;
            set => SetProperty(ref _searchSoSoLuong, value);
        }

        private string _searchDoiTuong;
        public string SearchDoiTuong
        {
            get => _searchDoiTuong;
            set => SetProperty(ref _searchDoiTuong, value);
        }

        private string _searchCapBac;
        public string SearchCapBac
        {
            get => _searchCapBac;
            set => SetProperty(ref _searchCapBac, value);
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private decimal? _heSoLuong;
        public decimal? HeSoLuong
        {
            get => _heSoLuong;
            set
            {
                if (SetProperty(ref _heSoLuong, value))
                {
                    LoadHsl();
                }
                Model.HeSoLuong = _heSoLuong;
            }
        }

        private decimal? _soNguoiPhuThuoc;
        public decimal? SoNguoiPhuThuoc
        {
            get => _soNguoiPhuThuoc;
            set
            {
                SetProperty(ref _soNguoiPhuThuoc, value);
                if (ItemsAllowence != null && _soNguoiPhuThuoc != null)
                {
                    ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.GTPT_SN).GiaTri = _soNguoiPhuThuoc;
                    OnPropertyChanged(nameof(ItemsAllowence));
                }
            }
        }

        private List<TlCachTinhLuongModel> dsCongThucLuong;
        public List<TlCachTinhLuongModel> DsCongThucLuong
        {
            get => dsCongThucLuong;
            set => dsCongThucLuong = value;
        }

        private List<ComboboxItem> _itemsNhom;
        public List<ComboboxItem> ItemsNhom
        {
            get => _itemsNhom;
            set => SetProperty(ref _itemsNhom, value);
        }

        private ComboboxItem _selectedNhom;
        public ComboboxItem SelectedNhom
        {
            get => _selectedNhom;
            set
            {
                SetProperty(ref _selectedNhom, value);
                if (_selectedCapBac != null)
                {
                    Model.MaCb = _selectedCapBac.MaCb;
                    LoadTangQuanHam(Model.MaCb);
                }
            }
        }

        private int _selectedTab;
        public int SelectedTab
        {
            get => _selectedTab;
            set
            {
                SetProperty(ref _selectedTab, value);
            }
        }

        private bool _isReadOnlyBHXH;
        public bool IsReadOnlyBHXH
        {
            get => _isReadOnlyBHXH;
            set => SetProperty(ref _isReadOnlyBHXH, value);
        }

        private int? _selectedThangLCCD;
        public int? SelectedThangThangLCCD
        {
            get => _selectedThangLCCD;
            set
            {
                SetProperty(ref _selectedThangLCCD, value);
            }
        }

        public CadresBHXHViewModel CadresBHXHViewModel { get; }

        public RelayCommand FirstCommand { get; }
        public RelayCommand PreviousCommand { get; }
        public RelayCommand NextCommand { get; }
        public RelayCommand LastCommand { get; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand ResetFilterRegimeCommand { get; set; }
        public RelayCommand SuaCommand { get; set; }
        public RelayCommand SearchCanBoCommand { get; set; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand SaveDataCommand { get; }
        public RelayCommand SaveAndCloseCommand { get; }
        public RelayCommand SaveAndCopyCommand { get; }
        public RelayCommand CloseDatagridCommand { get; }
        public RelayCommand ChooseCommand { get; }
        public RelayCommand DeleteCheDoBHXHCommand { get; }
        public RelayCommand SearchRegimeCommand { get; set; }
        public RelayCommand ShowPopupDetailCommand { get; }

        public CadresDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmPhuCapService phuCapService,
            ITlDmCanBoService cadresService,
            ITlDmCapBacService tlDmCapBacService,
            ITlDmChucVuService tlDmChucVuService,
            ITlDmTangGiamService tlDmTangGiamService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            ITlDmDonViService tlDmDonViService,
            INsQsMucLucService qsMucLucService,
            ITlDmCapBacKeHoachService tlDmCapBacKeHoachService,
            ITlDsCapNhapBangLuongService tlDsCapNhapBangLuongService,
            ITlBangLuongThangService tlBangLuongThangService,
            ITlDmCachTinhLuongChuanService tlDmCachTinhLuongChuanService,
            ITlDmHslKeHoachService tlDmHslKeHoachService,
            ITlCanBoCheDoBHXHService tlCanBoCheDoBHXHService,
            TlDmCheDoBHXHService tlDmCheDoBHXHService,
            ITlDmNgayNghiService tlDmNgayNghiService,
            ITlBangLuongThangBHXHService iTlBangLuongThangBHXHService,
            ITlDmCachTinhLuongBaoHiemService tlDmCachTinhLuongBaoHiemService,
            CadresBHXHViewModel cadresBHXHViewModel,
            ITlCanBoCheDoBHXHChiTietService canBoCheDoBHXHChiTietService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _cadresService = cadresService;
            _phuCapService = phuCapService;
            _tlDmCapBacService = tlDmCapBacService;
            _tlDmChucVuService = tlDmChucVuService;
            _tlDmTangGiamService = tlDmTangGiamService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _tlDmDonViService = tlDmDonViService;
            _qsMucLucService = qsMucLucService;
            _tlDmCapBacKeHoachService = tlDmCapBacKeHoachService;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _tlDmCachTinhLuongChuanService = tlDmCachTinhLuongChuanService;
            _tlDmHslKeHoachService = tlDmHslKeHoachService;
            _tlCanBoCheDoBHXHService = tlCanBoCheDoBHXHService;
            _tlDmCheDoBHXHService = tlDmCheDoBHXHService;
            _tTlDmNgayNghiService = tlDmNgayNghiService;
            _iTlBangLuongThangBHXHService = iTlBangLuongThangBHXHService;
            _tlDmCachTinhLuongBaoHiemService = tlDmCachTinhLuongBaoHiemService;
            _canBoCheDoBHXHChiTietService = canBoCheDoBHXHChiTietService;

            CadresBHXHViewModel = cadresBHXHViewModel;
            CadresBHXHViewModel.ParentPage = this;

            FirstCommand = new RelayCommand(o => OnFirst());
            PreviousCommand = new RelayCommand(o => OnPrevious());
            NextCommand = new RelayCommand(o => OnNext());
            LastCommand = new RelayCommand(o => OnLast());
            SearchCommand = new RelayCommand(o => _phuCapView.Refresh());
            SearchRegimeCommand = new RelayCommand(o => _cheDoView.Refresh());
            ResetFilterCommand = new RelayCommand(o => OnResetFilter());
            ResetFilterRegimeCommand = new RelayCommand(o => OnResetRegimeFilter());
            SuaCommand = new RelayCommand(o => OnUpdate());
            RefreshCommand = new RelayCommand(o => OnRefresh());
            SaveDataCommand = new RelayCommand(o => OnSaveAndClear());
            SaveAndCloseCommand = new RelayCommand(o => OnSaveAndClose(o));
            SaveAndCopyCommand = new RelayCommand(o => OnSaveAndCopy());
            CloseDatagridCommand = new RelayCommand(o => OnCloseSearchDatagrid());
            SearchCanBoCommand = new RelayCommand(o => OnSearchCanBo());
            ChooseCommand = new RelayCommand(o => OnChooseCanBo());
            DeleteCheDoBHXHCommand = new RelayCommand(obj => OnDeleteCheDoBHXH());
            ShowPopupDetailCommand = new RelayCommand(o => OnSelectionDoubleClick(o));
        }

        public override void Init()
        {
            try
            {
                _selectedTab = 0;
                IsFirst = true;
                SearchCanBo = string.Empty;
                SearchCapBac = string.Empty;
                SearchDoiTuong = string.Empty;
                SearchSoSoLuong = string.Empty;
                Model.BTamGiamTamGiu = Model.BTamGiamTamGiu ?? false;
                if (Model.bKhongTinhNTN == null) Model.bKhongTinhNTN = false;
                IsPopupOpen = false;
                MarginRequirement = new Thickness(10);
                Model.PropertyChanged += DetailModel_PropertyChanged;
                NgayNhapNgu = Model.NgayNn;
                NgayXuatNgu = Model.NgayXn;
                NgayTaiNgu = Model.NgayTn;
                SelectedAllowence = null;
                LoadData();
                LoadDanhMucCapBac();
                LoadListNhom();
                LoadCapBacKeHoach();
                LoadHslKeHoach();
                LoadDanhMucChucVu();
                LoadDanhMucTangGiam();
                LoadGender();
                LoadDonVi();
                LoadCanBo();
                FindCongThucLuong();
                IsFirst = false;
                CheckValidModel();
                GetHolidays();
                CadresBHXHViewModel.UpdateParentWindowEventHandler += RefreshCBCD;
                _iBaseSalaryMonth = Model.Thang;
                _iBaseSalaryYear = Model.Nam;
                _isModifiedBHXH = false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CheckValidModel()
        {
            var dtNow = DateTime.Now;
            var dNgayNhapNgu = NgayNhapNgu.HasValue ? NgayNhapNgu.Value.Date : dtNow.Date;
            var dNgayXuatNgu = NgayXuatNgu.HasValue ? NgayXuatNgu.Value.Date : dtNow.Date;
            if (NgayTaiNgu.HasValue && !NgayXuatNgu.HasValue || dNgayNhapNgu > dNgayXuatNgu)
            {
                MessageBoxHelper.Info(Resources.MessInvalidDateDemobilization);
            }
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            var data = _tlCanBoPhuCapService.FindCanBoPhuCap(Model.MaCanBo);
            List<AllowenceModel> listAllowence = _mapper.Map<List<AllowenceModel>>(data);

            foreach (AllowenceModel model in listAllowence)
            {
                model.PropertyChanged += DetailPhuCap_PropertyChanged;
            }
            var checkHsl = listAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.LHT_HS);
            var checkSoNgPT = listAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.GTPT_SN);

            if (checkHsl != null && checkSoNgPT != null)
            {
                _heSoLuong = checkHsl.GiaTri;
                _soNguoiPhuThuoc = checkSoNgPT.GiaTri;
            }
            else
            {
                _heSoLuong = 0;
                _soNguoiPhuThuoc = 0;
            }

            if (ViewState == FormViewState.ADD)
            {
                var tm = listAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.TM);
                if (tm != null)
                {
                    tm.GiaTri = 1;
                    tm.IsModified = false;
                }
            }

            ItemsAllowenceHuong = new ObservableCollection<AllowenceModel>(listAllowence);
            foreach (var item in ItemsAllowenceHuong)
            {
                item.PropertyChanged += DetailPhuCapHuong_PropertyChanged;
            }

            ItemsAllowence = new ObservableCollection<AllowenceModel>(listAllowence);
            _phuCapView = CollectionViewSource.GetDefaultView(ItemsAllowence);
            _phuCapView.GroupDescriptions.Add(new PropertyGroupDescription("ParentName"));
            _phuCapView.Filter = ListPhuCapFilter;

            LoadDataCheDoBHXH();
            OnPropertyChanged(nameof(ItemsAllowence));
            OnPropertyChanged(nameof(HeSoLuong));
            OnPropertyChanged(nameof(SoNguoiPhuThuoc));
        }

        private void OnSaveData()
        {
            try
            {
                string[] arrPHUCAP_MACDINH = { "TM", "THUONG_TT", "THUEDANOP_TT", "GIAMTHUE_TT", "THUNHAPKHAC_TT", "TIENTAUXE_TT", "TIENANDUONG_TT", "TIENCTLH_TT", "GTKHAC_TT", "THANG_TCXN", "THANG_TCVIECLAM" };
                TlDmCanBo tlDmCanBo;
                TlDmCanBo oldData = new TlDmCanBo();
                if (Model.Id != Guid.Empty)
                {
                    oldData = _cadresService.Find(Model.Id);
                }
                List<AllowenceModel> listSave = ItemsAllowence.Where(x => !x.IsHangCha && x.IsModified).ToList();
                ObservableCollection<TlCanBoPhuCapModel> tlCanBoPhuCapModels = new ObservableCollection<TlCanBoPhuCapModel>();
                Model.NgayNn = NgayNhapNgu;
                Model.NgayXn = NgayXuatNgu;
                Model.NgayTn = NgayTaiNgu;
                Model.NamTn = NamThamNien;
                Model.ThangTnn = ThangThamNienNghe;
                Model.MaCb = SelectedCapBac.MaCb;
                Model.CapBac = SelectedCapBac.Note;
                Model.Parent = SelectedDonVi.MaDonVi;
                Model.TenDonVi = SelectedDonVi.TenDonVi;
                Model.ITrangThai = 2;
                if (SelectedCapBacKeHoach != null)
                {
                    Model.ThoiHanTangCb = SelectedCapBacKeHoach.ThoiHanTang;
                }

                if (SelectedHslKeHoach != null)
                {
                    Model.CbKeHoach = SelectedHslKeHoach.Id.ToString();
                    Model.HsLuongKeHoach = SelectedHslKeHoach.LhtHsKh;
                }
                else
                {
                    Model.ThoiHanTangCb = null;
                    Model.CbKeHoach = Guid.Empty.ToString();
                }

                if (SelectedHslKeHoachTran != null)
                {
                    Model.IdLuongTran = SelectedHslKeHoachTran.Id;
                    Model.HsLuongTran = SelectedHslKeHoachTran.LhtHsKh;
                }

                if (SelectedGender.ValueItem == Gender.NAM)
                {
                    Model.IsNam = true;
                }
                else if (SelectedGender.ValueItem == Gender.NU)
                {
                    Model.IsNam = false;
                }

                if (SelectedChucVu != null)
                {
                    Model.MaCv = SelectedChucVu.MaCv;
                }
                else
                {
                    Model.MaCv = string.Empty;
                }

                // fixbug #41702
                //if (SelectedCapBac != null)
                //{
                //    var tiLeHuong = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.TILE_HUONG));
                //    if (tiLeHuong != null)
                //    {
                //        tiLeHuong.GiaTri = SelectedCapBac.TiLeHuong;
                //    }
                //}

                if (SelectedTangGiamItems != null)
                {
                    Model.MaTangGiam = SelectedTangGiamItems.SKyHieu;
                    if (Model.MaTangGiam.StartsWith("3")
                        && Model.MaTangGiam != "350"
                        && Model.MaTangGiam != "380")
                    {
                        Model.IsDelete = false;
                        Model.ITrangThai = 3;
                    }
                    if (Model.MaTangGiam == "290")
                    {
                        Model.ParentOld = oldData.Parent;
                    }
                }
                else
                {
                    if (oldData != null && oldData.Id != Guid.Empty
                        && !string.IsNullOrEmpty(oldData.MaTangGiam)
                        && oldData.MaTangGiam.StartsWith("3")
                        && oldData.MaTangGiam != "350"
                        && oldData.MaTangGiam != "380")
                    {
                        Model.IsDelete = true;
                    }
                    Model.MaTangGiam = String.Empty;
                }

                if (SelectedNhom != null)
                {
                    Model.Nhom = SelectedNhom.ValueItem;
                }

                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    tlDmCanBo = _cadresService.Find(Model.Id);
                    if (!Model.MaCb.Equals(tlDmCanBo.MaCb))
                    {
                        Model.MaCbCu = tlDmCanBo.MaCb;
                    }
                    var dataCanBoEdit = _tlCanBoPhuCapService.FindByMaCanBo(Model.MaCanBo);
                    _tlCanBoPhuCapService.DeleteByMaCanBo(Model.MaCanBo);
                    var dataAllAllowence = _mapper.Map<ObservableCollection<AllowenceModel>>(_phuCapService.FindAll()).ToList().OrderBy(x => x.MaPhuCap);

                    foreach (var item in ItemsAllowence)
                    {
                        var phucap = dataAllAllowence.FirstOrDefault(x => x.Id == item.Id);
                        if (phucap != null)
                        {
                            phucap.IsModified = item.IsModified;
                            phucap.GiaTri = item.GiaTri;
                            phucap.HuongPCSN = item.HuongPCSN;
                            phucap.DateStart = item.DateStart;
                            phucap.DateEnd = item.DateEnd;
                            phucap.ISoThangHuong = item.ISoThangHuong;
                            phucap.IsFormula = item.IsFormula;
                        }
                    }

                    foreach (var item in dataAllAllowence)
                    {
                        TlCanBoPhuCapModel tlCanBoPhuCap = new TlCanBoPhuCapModel();
                        tlCanBoPhuCap.MaCbo = Model.MaCanBo;
                        tlCanBoPhuCap.MaPhuCap = item.MaPhuCap;
                        tlCanBoPhuCap.GiaTri = item.GiaTri;
                        tlCanBoPhuCap.HuongPcSn = item.HuongPCSN;
                        tlCanBoPhuCap.DateStart = item.DateStart;
                        tlCanBoPhuCap.DateEnd = item.DateEnd;
                        tlCanBoPhuCap.ISoThangHuong = item.ISoThangHuong;
                        tlCanBoPhuCap.IsModified = item.IsModified;
                        tlCanBoPhuCap.BSaoChep = item.BSaoChep;
                        tlCanBoPhuCap.BCapNhat = true;
                        tlCanBoPhuCapModels.Add(tlCanBoPhuCap);
                    }

                    var listCanBoPhuCap = _mapper.Map<ObservableCollection<TlCanBoPhuCap>>(tlCanBoPhuCapModels.Select(x =>
                    {
                        x.Flag = false;
                        return x;
                    }).ToList());

                    if (SelectedCapBac.MaCb.StartsWith("0"))
                    {
                        var pctnHs = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCTN_HS);
                        if (pctnHs != null)
                        {
                            pctnHs.GiaTri = 0;
                        }
                    }
                    else
                    {
                        var pcTemThuTt = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCTEMTHU_TT);
                        if (pcTemThuTt != null)
                        {
                            pcTemThuTt.GiaTri = 0;
                        }
                    }


                    if (!SelectedCapBac.MaCb.StartsWith("0"))
                    {
                        var pcnu = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCNU_HS);
                        if (pcnu != null)
                        {
                            pcnu.GiaTri = 0;
                        }
                    }

                    var pcanqp = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCANQP_HS);
                    if (pcanqp != null && SelectedCapBac.MaCb.Equals("415"))
                    {
                        pcanqp.GiaTri = (decimal?)0.5;
                    }
                    else if (pcanqp != null && SelectedCapBac.MaCb.Equals("413"))
                    {
                        pcanqp.GiaTri = (decimal?)0.3;
                    }

                    var pcTm = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.TM);
                    if (pcTm != null)
                    {
                        if (Model.Tm == true)
                        {
                            pcTm.GiaTri = 0;
                        }
                        else
                        {
                            pcTm.GiaTri = 1;
                        }
                    }

                    var pcNopBhtn = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.Nop_BHTN);
                    //pcNopBhtn.GiaTri = (bool)Model.BHTN ? 1 : 0;


                    if (pcNopBhtn != null)
                    {
                        if (Model.BHTN == true)
                        {
                            pcNopBhtn.GiaTri = 1;
                        }
                        else
                        {
                            pcNopBhtn.GiaTri = 0;
                        }
                    }

                    var pcHuongCv = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.Huong_PCCOV);
                    if (pcHuongCv != null)
                    {
                        if (Model.PCCV == true)
                        {
                            pcHuongCv.GiaTri = 1;
                        }
                        else
                        {
                            pcHuongCv.GiaTri = 0;
                        }
                    }

                    var pcThangTcxn = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.THANG_TCXN);
                    if (pcThangTcxn != null && SelectedCapBac.MaCb.StartsWith("0") && Model.NgayXn != null)
                    {
                        pcThangTcxn.GiaTri = TinhThangHuongTcxn(Model.NgayNn, Model.NgayXn);
                    }

                    var pcThangTcViecLam = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.THANG_TCVIECLAM);
                    if (pcThangTcViecLam != null && Model.NgayXn == null)
                    {
                        pcThangTcViecLam.GiaTri = 0;
                    }

                    var phuCapBhxhdvHs = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHXHDV_HS);
                    if (phuCapBhxhdvHs != null)
                    {
                        phuCapBhxhdvHs.GiaTri = SelectedCapBac.BhxhCq;
                    }

                    var phuCapBhxhcnHs = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHXHCN_HS);
                    if (phuCapBhxhcnHs != null)
                    {
                        phuCapBhxhcnHs.GiaTri = SelectedCapBac.HsBhxh;
                    }

                    var phuCapBhytdvHs = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHYTDV_HS);
                    if (phuCapBhytdvHs != null)
                    {
                        phuCapBhytdvHs.GiaTri = SelectedCapBac.BhytCq;
                    }

                    var phuCapBhytcnHs = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHYTCN_HS);
                    if (phuCapBhytcnHs != null)
                    {
                        phuCapBhytcnHs.GiaTri = SelectedCapBac.HsBhyt;
                    }

                    var phuCapBhtndvHs = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHTNDV_HS);
                    if (phuCapBhtndvHs != null)
                    {
                        phuCapBhtndvHs.GiaTri = SelectedCapBac.BhtnCq;
                    }

                    var phuCapBhtncnHs = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHTNCN_HS);
                    if (phuCapBhtncnHs != null)
                    {
                        phuCapBhtncnHs.GiaTri = SelectedCapBac.HsBhtn;
                    }

                    var phuCapNtn = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.NTN);
                    if (phuCapNtn != null)
                    {
                        phuCapNtn.GiaTri = NamThamNien;
                    }

                    //var phuCapPccvTien = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCCV_TIEN);
                    //if (phuCapPccvTien != null && SelectedChucVu != null)
                    //{
                    //    phuCapPccvTien.GiaTri = SelectedChucVu.ThanhTienCv;
                    //}
                    //else
                    //{
                    //    phuCapPccvTien.GiaTri = 0;
                    //}

                    foreach (var item in dataCanBoEdit)
                    {
                        listCanBoPhuCap.Select(x =>
                        {
                            x.Flag = (x.MaPhuCap == item.MaPhuCap && x.GiaTri != item.GiaTri) ? x.Flag = true : x.Flag;
                            //x.BCapNhat = (x.MaPhuCap == item.MaPhuCap && x.GiaTri != item.GiaTri);
                            return x;
                        }).ToList();
                    }

                    Model.DateModified = DateTime.Now;
                    Model.UserModifier = _sessionService.Current.Principal;
                    if (_isModifiedBHXH)
                        SaveCheDoBHXH();
                    UpdateStatusHuongCheDoBHXH();
                    _mapper.Map(Model, tlDmCanBo);
                    _cadresService.Update(tlDmCanBo);
                    _tlCanBoPhuCapService.BulkInsert(listCanBoPhuCap.Where(x => x.GiaTri.GetValueOrDefault() != 0 || arrPHUCAP_MACDINH.Contains(x.MaPhuCap)));
                }
                else
                {
                    Model.Thang = Int32.Parse(Model.MaCanBo.Substring(4, 2));
                    Model.IsDelete = true;
                    Model.IsLock = false;
                    Model.UserCreator = _sessionService.Current.Principal;
                    Model.DateCreated = DateTime.Now;
                    Model.MaCbCu = string.Empty;
                    Model.ITrangThai = 1;
                    var dataAllAllowence = _mapper.Map<ObservableCollection<AllowenceModel>>(_phuCapService.FindAll()).ToList();

                    foreach (var item in ItemsAllowence)
                    {
                        var phucap = dataAllAllowence.FirstOrDefault(x => x.Id == item.Id);
                        if (phucap != null)
                        {
                            phucap.IsModified = item.IsModified;
                            phucap.GiaTri = item.GiaTri;
                            phucap.HuongPCSN = item.HuongPCSN;
                            phucap.DateStart = item.DateStart;
                            phucap.DateEnd = item.DateEnd;
                            phucap.ISoThangHuong = item.ISoThangHuong;
                            phucap.IsFormula = item.IsFormula;
                        }
                    }

                    foreach (var item in dataAllAllowence)
                    {
                        TlCanBoPhuCapModel tlCanBoPhuCap = new TlCanBoPhuCapModel();
                        tlCanBoPhuCap.MaCbo = Model.MaCanBo;
                        tlCanBoPhuCap.MaPhuCap = item.MaPhuCap;
                        tlCanBoPhuCap.GiaTri = item.GiaTri;
                        tlCanBoPhuCap.HuongPcSn = item.HuongPCSN;
                        tlCanBoPhuCap.DateStart = item.DateStart;
                        tlCanBoPhuCap.DateEnd = item.DateEnd;
                        tlCanBoPhuCap.ISoThangHuong = item.ISoThangHuong;
                        tlCanBoPhuCap.IsModified = item.IsModified;
                        tlCanBoPhuCap.BSaoChep = item.BSaoChep;
                        tlCanBoPhuCap.BCapNhat = true;
                        tlCanBoPhuCapModels.Add(tlCanBoPhuCap);
                    }

                    var listCanBoPhuCap = _mapper.Map<ObservableCollection<TlCanBoPhuCap>>(tlCanBoPhuCapModels);
                    var canBoPhuCapInserts = new List<TlCanBoPhuCap>();
                    if (SelectedCapBac.MaCb.StartsWith("0"))
                    {
                        var pctnHs = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCTN_HS);
                        if (pctnHs != null)
                        {
                            pctnHs.GiaTri = 0;
                            canBoPhuCapInserts.Add(pctnHs);
                        }
                    }
                    else
                    {
                        var pcTemThuTt = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCTEMTHU_TT);
                        if (pcTemThuTt != null)
                        {
                            pcTemThuTt.GiaTri = 0;
                        }
                    }

                    //if (SelectedCapBac.MaCb.StartsWith("0") && Model.NgayXn != null)
                    //{
                    //    var pcTcViecLam = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.TCVIECLAM_TT);
                    //    if (pcTcViecLam != null)
                    //    {
                    //        var phuCap = _phuCapService.FindByMaPhuCap(PhuCap.TCVIECLAM_TT);

                    //        pcTcViecLam.GiaTri = phuCap.GiaTri;
                    //    }
                    //}
                    //else
                    //{
                    //    var pcTcViecLam = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.TCVIECLAM_TT);
                    //    if (pcTcViecLam != null)
                    //    {
                    //        pcTcViecLam.GiaTri = 0;
                    //    }
                    //}

                    if (!SelectedCapBac.MaCb.StartsWith("0"))
                    {
                        var pcnu = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCNU_HS);
                        if (pcnu != null)
                        {
                            pcnu.GiaTri = 0;
                        }
                    }

                    var pcanqp = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCANQP_HS);
                    if (pcanqp != null && SelectedCapBac.MaCb.Equals("415"))
                    {
                        pcanqp.GiaTri = (decimal?)0.5;
                    }
                    else if (pcanqp != null && SelectedCapBac.MaCb.Equals("413"))
                    {
                        pcanqp.GiaTri = (decimal?)0.3;
                    }

                    var pcTm = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.TM);
                    if (pcTm != null)
                    {
                        if (Model.Tm == true)
                        {
                            pcTm.GiaTri = 0;
                        }
                        else
                        {
                            pcTm.GiaTri = 1;
                        }
                    }

                    var pcNopBhtn = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.Nop_BHTN);
                    if (pcNopBhtn != null)
                    {
                        if (Model.BHTN == true)
                        {
                            pcNopBhtn.GiaTri = 1;
                        }
                        else
                        {
                            pcNopBhtn.GiaTri = 0;
                        }
                    }

                    var pcHuongCv = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.Huong_PCCOV);
                    if (pcHuongCv != null)
                    {
                        if (Model.PCCV == true)
                        {
                            pcHuongCv.GiaTri = 1;
                        }
                        else
                        {
                            pcHuongCv.GiaTri = 0;
                        }
                    }

                    var pcThangTcxn = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.THANG_TCXN);
                    if (pcThangTcxn != null && SelectedCapBac.MaCb.StartsWith("0") && Model.NgayXn != null)
                    {
                        pcThangTcxn.GiaTri = TinhThangHuongTcxn(Model.NgayNn, Model.NgayXn);
                    }

                    var pcThangTcViecLam = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.THANG_TCVIECLAM);
                    if (pcThangTcViecLam != null && Model.NgayXn == null)
                    {
                        pcThangTcViecLam.GiaTri = 0;
                    }

                    var phuCapBhxhdvHs = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHXHDV_HS);
                    if (phuCapBhxhdvHs != null)
                    {
                        phuCapBhxhdvHs.GiaTri = SelectedCapBac.BhxhCq;
                    }

                    var phuCapBhxhcnHs = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHXHCN_HS);
                    if (phuCapBhxhcnHs != null)
                    {
                        phuCapBhxhcnHs.GiaTri = SelectedCapBac.HsBhxh;
                    }

                    var phuCapBhytdvHs = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHYTDV_HS);
                    if (phuCapBhytdvHs != null)
                    {
                        phuCapBhytdvHs.GiaTri = SelectedCapBac.BhytCq;
                    }

                    var phuCapBhytcnHs = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHYTCN_HS);
                    if (phuCapBhytcnHs != null)
                    {
                        phuCapBhytcnHs.GiaTri = SelectedCapBac.HsBhyt;
                    }

                    var phuCapBhtndvHs = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHTNDV_HS);
                    if (phuCapBhtndvHs != null)
                    {
                        phuCapBhtndvHs.GiaTri = SelectedCapBac.BhtnCq;
                    }

                    var phuCapBhtncnHs = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHTNCN_HS);
                    if (phuCapBhtncnHs != null)
                    {
                        phuCapBhtncnHs.GiaTri = SelectedCapBac.HsBhtn;
                    }

                    var phuCapNtn = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.NTN);
                    if (phuCapNtn != null)
                    {
                        phuCapNtn.GiaTri = NamThamNien;
                    }

                    //var phuCapPccvTien = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCCV_TIEN);
                    //if (phuCapPccvTien != null && SelectedChucVu != null)
                    //{
                    //    phuCapPccvTien.GiaTri = SelectedChucVu.ThanhTienCv;
                    //}
                    //else
                    //{
                    //    phuCapPccvTien.GiaTri = 0;
                    //}

                    listCanBoPhuCap.Select(x =>
                    {
                        x.Flag = false;
                        return x;
                    }).ToList();
                    _tlCanBoPhuCapService.BulkInsert(listCanBoPhuCap.Where(x => x.GiaTri.GetValueOrDefault() != 0 || arrPHUCAP_MACDINH.Contains(x.MaPhuCap)));
                    if (_isModifiedBHXH)
                        SaveCheDoBHXH();
                    UpdateStatusHuongCheDoBHXH();
                    tlDmCanBo = new TlDmCanBo();
                    _mapper.Map(Model, tlDmCanBo);
                    _cadresService.Add(tlDmCanBo);
                }

                if (ViewState != FormViewState.ADD)
                {
                    ViewState = FormViewState.DETAIL;
                    LoadCanBo();
                }
                if (_isModifiedBHXH)
                    LoadDataCheDoBHXH();
                // Refresh state form
                ItemsAllowence.Select(x => { x.IsModified = false; return x; }).ToList();
                SavedAction?.Invoke(_mapper.Map<CadresModel>(tlDmCanBo));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnSaveAndClear()
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Info(message);
            }
            else
            {
                LoaiSave = LoaiSave.SAVE_AND_CLEAR;
                OnSaveData();
                UpdateStatusHuongCheDoBHXH();
                if (ViewState == FormViewState.ADD)
                {
                    CadresModel newModel = new CadresModel();
                    newModel.Parent = Model.Parent;
                    newModel.TenDonVi = Model.TenDonVi;
                    NgayNhapNgu = null;
                    NgayXuatNgu = null;
                    NgayTaiNgu = null;
                    NamThamNien = 0;
                    ThangThamNienNghe = 0;
                    newModel.KhongLuong = false;
                    newModel.Tm = false;
                    newModel.BHTN = false;
                    newModel.PCCV = true;
                    newModel.MaHieuCanBo = (int.Parse(Model.MaHieuCanBo) + 1).ToString();
                    newModel.MaCanBo = Model.MaCanBo.Substring(0, 6) + newModel.MaHieuCanBo;
                    newModel.SoSoLuong = newModel.MaHieuCanBo.PadLeft(7, '0');
                    newModel.Nam = Model.Nam;
                    newModel.Thang = Model.Thang;
                    Model = newModel;
                    Init();

                }
                else
                {
                    ViewState = FormViewState.DETAIL;
                    LoadEnabled();
                }
                if (ViewState == FormViewState.ADD)
                {
                    MessageBoxHelper.Info(Resources.MsgAddCanbo);
                }
                else
                {
                    MessageBoxHelper.Info(Resources.MsgEditCanBo);
                }
            }
        }

        private void OnSaveAndClose(object obj)
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Info(message);
            }
            else
            {
                LoaiSave = LoaiSave.SAVE_AND_CLOSE;
                OnSaveData();
                if (ViewState == FormViewState.ADD)
                {
                    MessageBoxHelper.Info(Resources.MsgAddCanbo);
                }
                else
                {
                    MessageBoxHelper.Info(Resources.MsgEditCanBo);
                }
                Window window = obj as Window;
                window.Close();
            }
        }

        private void OnSaveAndCopy()
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Info(message);
            }
            else
            {
                LoaiSave = LoaiSave.SAVE_AND_COPY;
                OnSaveData();
                if (ViewState == FormViewState.ADD)
                {
                    MessageBoxHelper.Info(Resources.MsgAddCanbo);
                }
                else
                {
                    MessageBoxHelper.Info(Resources.MsgEditCanBo);
                }
                Model.MaHieuCanBo = (int.Parse(Model.MaHieuCanBo) + 1).ToString();
                Model.MaCanBo = Model.MaCanBo.Substring(0, 6) + Model.MaHieuCanBo;
                Model.SoSoLuong = Model.MaHieuCanBo.PadLeft(7, '0');
                OnPropertyChanged(nameof(Model));
            }
        }

        private void LoadCanBo()
        {
            BackEnabled = false;
            NextEnabled = false;

            if (CanBoView != null)
            {
                _lstCanBo = new ObservableCollection<CadresModel>(CanBoView.Cast<CadresModel>());
                _canBoSearchView = CollectionViewSource.GetDefaultView(_lstCanBo);
                _canBoSearchView.Filter = CanBoFilter;
            }

            LoadEnabled();
            OnPropertyChanged(nameof(LstCanBo));
            OnPropertyChanged(nameof(SelectedCanBo));
        }

        private void LoadListNhom()
        {
            ItemsNhom = new List<ComboboxItem>();
            _itemsNhom.Add(new ComboboxItem(NhomQncn.SOCAPN1, NhomQncn.SOCAPN1));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.SOCAPN2, NhomQncn.SOCAPN2));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.TRUNGCAPN1, NhomQncn.TRUNGCAPN1));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.TRUNGCAPN2, NhomQncn.TRUNGCAPN2));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.CAOCAPN1, NhomQncn.CAOCAPN1));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.CAOCAPN2, NhomQncn.CAOCAPN2));

            _selectedNhom = _itemsNhom.FirstOrDefault(x => x.ValueItem.Equals(Model.Nhom));
            OnPropertyChanged(nameof(SelectedNhom));
            OnPropertyChanged(nameof(ItemsNhom));
        }

        private void LoadCapBacKeHoach()
        {
            var data = _tlDmCapBacKeHoachService.FindAll().OrderBy(x => x.MaCb);
            _itemsCapBacKeHoach = _mapper.Map<ObservableCollection<TlDmCapBacKeHoachModel>>(data);
            if (!string.IsNullOrEmpty(Model.CbKeHoach))
            {
                _selectedCapBacKeHoach = _itemsCapBacKeHoach.FirstOrDefault(x => x.Id.ToString() == Model.CbKeHoach);
            }
            if (Model.IdLuongTran != null)
            {
                _selectedCapBacKeHoachTran = _itemsCapBacKeHoach.FirstOrDefault(x => x.Id == Model.IdLuongTran);
            }
            OnPropertyChanged(nameof(SelectedCapBacKeHoach));
            OnPropertyChanged(nameof(SelectedCapBacKeHoachTran));
            OnPropertyChanged(nameof(ItemsCapBacKeHoach));
        }

        private void LoadHslKeHoach()
        {
            var data = _tlDmHslKeHoachService.FindAll().OrderBy(x => x.MaCb);
            ItemsHslKeHoach = _mapper.Map<ObservableCollection<TlDmHslKeHoachModel>>(data);
            SelectedHslKeHoach = _itemsHslKeHoach.FirstOrDefault(x => x.Id.ToString().Equals(Model.CbKeHoach));
            SelectedHslKeHoachTran = _itemsHslKeHoach.FirstOrDefault(x => x.Id == Model.IdLuongTran);
        }

        private void LoadDanhMucCapBac()
        {
            var data = _tlDmCapBacService.FindByNote().Where(x => !string.IsNullOrEmpty(x.Parent));
            _itemsCapBac = _mapper.Map<ObservableCollection<TlDmCapBacModel>>(data);
            // Nếu là thêm mới thì Model.MaCb null => _selectedCapBacItems cũng null nên ko cần check thêm mới thì set null
            _selectedCapBac = _itemsCapBac.FirstOrDefault(x => x.MaCb.Equals(Model.MaCb));
            OnPropertyChanged(nameof(SelectedCapBac));
            OnPropertyChanged(nameof(ItemsCapBac));
        }

        private void LoadDanhMucChucVu()
        {
            var data = _tlDmChucVuService.FindAll().OrderBy(x => x.MaCv);
            _itemsChucVu = _mapper.Map<ObservableCollection<TlDmChucVuModel>>(data);
            if (string.IsNullOrEmpty(Model.MaCv))
            {
                _selectedChucVu = null;
            }
            else
            {
                _selectedChucVu = _itemsChucVu.FirstOrDefault(x => x.MaCv.Equals(Model.MaCv));
            }
            OnPropertyChanged(nameof(SelectedChucVu));
            OnPropertyChanged(nameof(ItemsChucVu));
        }

        private void LoadDanhMucTangGiam()
        {
            try
            {
                var data = _qsMucLucService.FindAll().Where(x => x.BHangCha == false && x.SHienThi != "2" && x.INamLamViec == _sessionService.Current.YearOfWork).ToList();
                _tangGiamItems = new ObservableCollection<QsMucLucModel>();
                _tangGiamItems = _mapper.Map<ObservableCollection<QsMucLucModel>>(data);
                if (Model.MaTangGiam != null)
                {
                    SelectedTangGiamItems = TangGiamItems.FirstOrDefault(x => x.SKyHieu == Model.MaTangGiam);
                }
                OnPropertyChanged(nameof(TangGiamItems));
                OnPropertyChanged(nameof(SelectedTangGiamItems));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadGender()
        {
            GenderData = new List<ComboboxItem>();
            _gender.Add(new ComboboxItem(Gender.NAM, Gender.NAM));
            _gender.Add(new ComboboxItem(Gender.NU, Gender.NU));
            if (ViewState == FormViewState.ADD)
            {
                SelectedGender = GenderData.FirstOrDefault(x => x.ValueItem == Gender.NAM);
            }
            else
            {
                switch (Model.IsNam)
                {
                    case true:
                        SelectedGender = GenderData.FirstOrDefault(x => x.ValueItem == Gender.NAM);
                        break;
                    case false:
                        SelectedGender = GenderData.FirstOrDefault(x => x.ValueItem == Gender.NU);
                        break;
                }
            }
        }

        private void LoadDonVi()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            _donviData = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);
            if (SelectedDonVi != null)
            {
                if (ViewState != FormViewState.ADD)
                {
                    _selectedDonVi = _donviData.FirstOrDefault(x => x.MaDonVi == Model.Parent);
                }
                else
                {
                    _selectedDonVi = _donviData.FirstOrDefault(x => x.MaDonVi == SelectedDonVi.MaDonVi);
                }
            }

            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(DonViItems));
        }

        private void DetailPhuCap_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(AllowenceModel.GiaTri)
                || args.PropertyName == nameof(AllowenceModel.HuongPCSN)
                || args.PropertyName == nameof(AllowenceModel.DateStart)
                || args.PropertyName == nameof(AllowenceModel.ISoThangHuong))
            {
                AllowenceModel item = (AllowenceModel)sender;
                item.IsModified = true;

                if (item.MaPhuCap == PhuCap.LHT_HS)
                {
                    _heSoLuong = item.GiaTri;
                    OnPropertyChanged(nameof(HeSoLuong));
                }

                if (item.MaPhuCap == PhuCap.GTPT_SN)
                {
                    _soNguoiPhuThuoc = item.GiaTri;
                    OnPropertyChanged(nameof(SoNguoiPhuThuoc));
                }

                var lstHsTruyLinh = new List<string>() { PhuCap.LHT_HS, PhuCap.PCCV_HS, PhuCap.PCTHUHUT_HS, PhuCap.PCCOV_HS, PhuCap.PCCU_HS };
                if (ViewState == FormViewState.ADD)
                {
                    if (lstHsTruyLinh.Contains(item.MaPhuCap))
                    {
                        var phuCapTruyLinh = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == string.Format("{0}{1}", item.MaPhuCap, "_CU"));
                        if (phuCapTruyLinh != null)
                        {
                            phuCapTruyLinh.GiaTri = item.GiaTri;
                            phuCapTruyLinh.IsModified = true;
                        }
                    }
                }

                OnPropertyChanged(nameof(ItemsAllowence));
            }
        }

        private void DetailPhuCapHuong_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(AllowenceModel.DateStart)
                || args.PropertyName == nameof(AllowenceModel.ISoThangHuong))
            {
                AllowenceModel item = (AllowenceModel)sender;
                var phuCap = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(item.MaPhuCap));
                if (phuCap != null)
                {
                    phuCap.ISoThangHuong = item.ISoThangHuong;
                    phuCap.DateStart = item.DateStart;
                    phuCap.IsModified = true;
                    OnPropertyChanged(nameof(ItemsAllowence));
                }
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            CadresModel item = (CadresModel)sender;
            if (args.PropertyName == nameof(CadresModel.Tm))
            {
                var Tm = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.TM));
                if ((bool)item.Tm && Tm != null)
                {
                    Tm.GiaTri = 0;
                }
                else if ((bool)!item.Tm && Tm != null)
                {
                    Tm.GiaTri = 1;
                }
            }
            if (args.PropertyName == nameof(CadresModel.BHTN))
            {
                var Nop_BHTN = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.Nop_BHTN));
                if ((bool)item.BHTN && Nop_BHTN != null)
                {
                    Nop_BHTN.GiaTri = 1;
                }
                else if ((bool)!item.BHTN && Nop_BHTN != null)
                {
                    Nop_BHTN.GiaTri = 0;
                }
            }
            if (args.PropertyName == nameof(CadresModel.PCCV))
            {
                var Huong_PCCOV = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.Huong_PCCOV));
                if ((bool)item.PCCV && Huong_PCCOV != null)
                {
                    Huong_PCCOV.GiaTri = 1;
                }
                else if ((bool)!item.PCCV && Huong_PCCOV != null)
                {
                    Huong_PCCOV.GiaTri = 0;
                }
            }
            if (args.PropertyName == nameof(CadresModel.SoTaiKhoan))
            {
                if (!string.IsNullOrEmpty(item.SoTaiKhoan))
                {
                    item.Tm = true;
                }
                else if (string.IsNullOrEmpty(item.SoTaiKhoan))
                {
                    item.Tm = false;
                }
            }
            if (args.PropertyName == nameof(CadresModel.bKhongTinhNTN))
            {
                if (item.bKhongTinhNTN != null)
                    TinhNamThamNien();
            }
            //if (args.PropertyName == nameof(CadresModel.NgayTruyLinh))
            //{
            //    if (item.NgayTruyLinh != null)
            //    {
            //        var pcTtl = ItemsAllowence.Where(x => x.MaPhuCap.Contains("TTL"));
            //        var ngayTruyLinh = (DateTime)item.NgayTruyLinh;
            //        var monthDiff = ((int)Model.Nam - ngayTruyLinh.Year) * 12 + (int)Model.Thang - ngayTruyLinh.Month + 1;

            //        foreach (var item1 in pcTtl)
            //        {
            //            //var dateTimeNow = new DateTime((int)Model.Nam, (int)Model.Thang, DateTime.DaysInMonth((int)Model.Nam, (int)Model.Thang));
            //            if (ngayTruyLinh.Day >= 15)
            //            {
            //                var value = monthDiff - 0.5;
            //                item1.GiaTri = (decimal?)value;
            //                item1.IsModified = true;
            //            }
            //            else
            //            {
            //                item1.GiaTri = monthDiff;
            //                item1.IsModified = true;
            //            }
            //        }
            //    }
            //}
            OnPropertyChanged(nameof(ItemsAllowence));
        }

        private void LoadHsl()
        {
            var pc = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.LHT_HS));
            if (HeSoLuong <= 0)
            {
                HeSoLuong = 0;
            }
            pc.GiaTri = HeSoLuong;
            if (_selectedCapBac != null)
            {
                Model.MaCb = _selectedCapBac.MaCb;
                LoadTangQuanHam(Model.MaCb);
            }
        }

        private void LoadHeSoLuong()
        {
            if (ItemsAllowence != null)
            {
                var phucap = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.LHT_HS);
                if (_selectedCapBac != null && phucap != null)
                {
                    phucap.GiaTri = _selectedCapBac.LhtHs;
                    phucap.IsModified = false;
                    Model.HeSoLuong = _selectedCapBac.LhtHs;
                }
                var phucapTiLeHuong = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.TILE_HUONG);
                if (_selectedCapBac != null && phucapTiLeHuong != null)
                {
                    phucapTiLeHuong.GiaTri = _selectedCapBac.TiLeHuong;
                    phucapTiLeHuong.IsModified = false;
                }
                OnPropertyChanged(nameof(ItemsAllowence));
                OnPropertyChanged(nameof(CadresModel));
            }
            if (_selectedCapBac != null)
            {
                if (_selectedCapBac.MaCb.StartsWith("1") || _selectedCapBac.MaCb.StartsWith("4"))
                {
                    SelectedNhom = null;
                    OnPropertyChanged(nameof(SelectedNhom));
                }
                if (_selectedCapBac.MaCb.StartsWith("0"))
                {
                    Model.PCCV = true;
                }
            }
        }

        private void LoadEnabled()
        {
            if (ViewState == FormViewState.DETAIL && LstCanBo.Count() > 1)
            {
                if (Model.MaCanBo == _lstCanBo.FirstOrDefault().MaCanBo)
                {
                    BackEnabled = false;
                    NextEnabled = true;
                }
                else if (Model.MaCanBo == _lstCanBo.LastOrDefault().MaCanBo)
                {
                    BackEnabled = true;
                    NextEnabled = false;
                }
                else
                {
                    BackEnabled = true;
                    NextEnabled = true;
                }
            }
        }

        private void LoadHeSoChucVu()
        {
            if (ItemsAllowence != null)
            {
                var phucap = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCCV_HS);
                if (_selectedChucVu != null && phucap != null)
                {
                    phucap.GiaTri = _selectedChucVu.HeSoCv;
                    phucap.IsModified = false;
                }
                else if (_selectedChucVu == null && phucap != null)
                {
                    phucap.GiaTri = 0;
                }
                OnPropertyChanged(nameof(ItemsAllowence));
            }
        }

        private void TinhNamThamNien()
        {
            if (Model != null)
            {
                if (Model.bKhongTinhNTN.HasValue && !Model.bKhongTinhNTN.Value)
                {
                    try
                    {
                        NamThamNien = DateUtils.TinhNamThamNien(NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangThamNienNghe, (int)Model.Thang, (int)Model.Nam);

                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex);
                        NamThamNien = 0;
                    }
                    if (NamThamNien < 0)
                    {
                        NamThamNien = 0;
                    }
                }
                else
                    NamThamNien = ThangThamNienNghe / 12;
                OnPropertyChanged(nameof(NamThamNien));
            }
        }

        private int TinhThangHuongTcxn(DateTime? ngayNn, DateTime? ngayXn)
        {
            var ngayNhapNgu = (DateTime)ngayNn;
            var ngayXuatNgu = (DateTime)ngayXn;
            var monthDiff = (ngayXuatNgu.Year - ngayNhapNgu.Year) * 12 + ngayXuatNgu.Month - ngayNhapNgu.Month + 1;
            var phanNguyen = monthDiff / 12;
            var phanDu = monthDiff % 12;

            int thangDu = 0;
            if (1 <= phanDu && phanDu <= 6)
            {
                thangDu = 1;
            }
            else if (7 <= phanDu && phanDu <= 12)
            {
                thangDu = 2;
            }

            return (phanNguyen * 2 + thangDu);
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();
            DateTime ngay = new DateTime((int)Model.Nam, (int)Model.Thang, 1);
            if (string.IsNullOrEmpty(Model.TenCanBo))
            {
                messages.Add(string.Format(Resources.CadresNameNull));
                goto End;
            }
            if (SelectedCapBac == null)
            {
                messages.Add(string.Format(Resources.CadresRankNull));
                goto End;
            }
            if (SelectedGender == null)
            {
                messages.Add(string.Format(Resources.GenderNull));
                goto End;
            }
            if (SelectedDonVi == null)
            {
                messages.Add(string.Format(Resources.UnitNull));
                goto End;
            }
            if (NgayNhapNgu > ngay)
            {
                messages.Add(string.Format(Resources.NgayNhapNgu));
                goto End;
            }
            if ((Model.KhongLuong ?? false))
            {
                if (SelectedTangGiamItems == null)
                {
                    messages.Add("Nếu không tính lương thì phải nhập mã tăng giảm !");
                    goto End;
                }
            }
            var ngayThangCanBo = new DateTime((int)Model.Nam, (int)Model.Thang, DateTime.DaysInMonth((int)Model.Nam, (int)Model.Thang));
            if (Model.NgayTruyLinh != null && Model.NgayTruyLinh > ngayThangCanBo)
            {
                messages.Add(string.Format(Resources.PursuitDayNotValid));
                goto End;
            }

            if (DataCanBoCheDoBHXH.Any(x => x.IsModified && !x.IsDeleted && x.FSoNgayHuongBHXH.GetValueOrDefault() < 0))
            {
                messages.Add(string.Format(Resources.ValueNotLessThanZero, "Số ngày hưởng chế độ BHXH"));
                goto End;
            }
        //foreach (var item in DataCanBoCheDoBHXH)
        //{
        //    if (item.FSoNgayHuongBHXH > 24)
        //    {
        //        messages.Add(string.Format(Resources.ValidateDayOfApplied, item.STenCheDo));
        //    }
        //}
        //foreach (var item in ItemsAllowence)
        //{
        //    if (item.DateStart != null && item.DateStart > ngayThangCanBo)
        //    {
        //        messages.Add(string.Format(Resources.DateStartNotValid, item.MaPhuCap));
        //        goto End;
        //    }
        //    if (item.ISoThangHuong != null && item.ISoThangHuong < 0)
        //    {
        //        messages.Add(string.Format(Resources.SoThangHuongInvalid, item.MaPhuCap));
        //        goto End;
        //    }
        //    if (item.DateStart != null && item.DateStart <= ngayThangCanBo && item.ISoThangHuong != null && item.ISoThangHuong > 0)
        //    {
        //        DateTime ngayBatDau = (DateTime)item.DateStart;
        //        if (ngayBatDau.AddMonths((int)item.ISoThangHuong) > ngayThangCanBo)
        //        {
        //            messages.Add(string.Format(Resources.DateStartAndSoThangHuongInvalid, item.MaPhuCap));
        //            goto End;
        //        }
        //    }
        //}
        End:
            return string.Join(Environment.NewLine, messages);
        }

        private void OnFirst()
        {
            Model = LstCanBo.First();
            Init();
        }

        private void OnPrevious()
        {
            var index = LstCanBo.IndexOf(Model);
            Model = LstCanBo[index - 1];
            Init();
        }

        private void OnNext()
        {
            var index = LstCanBo.IndexOf(Model);
            Model = LstCanBo[index + 1];
            Init();
        }

        private void OnLast()
        {
            Model = LstCanBo.Last();
            Init();
        }

        private bool ListPhuCapFilter(object obj)
        {
            bool result = true;
            var item = (AllowenceModel)obj;

            if (!string.IsNullOrEmpty(SearchMaPhuCap))
            {
                result = result && !string.IsNullOrEmpty(item.MaPhuCap) && item.MaPhuCap.ToLower().Contains(SearchMaPhuCap.ToLower());
            }
            if (!string.IsNullOrEmpty(SearchTenPhuCap))
            {
                result = result && !string.IsNullOrEmpty(item.TenPhuCap) && item.TenPhuCap.ToLower().Contains(SearchTenPhuCap.ToLower());
            }

            return result;
        }

        private bool ListCheDoFilter(object obj)
        {
            bool result = true;
            var item = (TlCanBoCheDoBHXHModel)obj;

            if (!string.IsNullOrEmpty(SearchMaCheDo))
            {
                result = result && !string.IsNullOrEmpty(item.SMaCheDo) && item.SMaCheDo.ToLower().Contains(SearchMaCheDo.ToLower());
            }
            if (!string.IsNullOrEmpty(SearchTenCheDo))
            {
                result = result && !string.IsNullOrEmpty(item.STenCheDo) && item.STenCheDo.ToLower().Contains(SearchTenCheDo.ToLower());
            }

            return result;
        }

        private bool CanBoFilter(object obj)
        {
            bool result = true;
            if (IsFirst)
            {
                return false;
            }
            var item = (CadresModel)obj;
            if (!string.IsNullOrEmpty(SearchSoSoLuong))
            {
                result = result && !string.IsNullOrEmpty(item.MaCanBo) && item.MaCanBo.ToLower().Contains(SearchSoSoLuong.ToLower());
            }
            if (!string.IsNullOrEmpty(SearchDoiTuong))
            {
                result = result && !string.IsNullOrEmpty(item.TenCanBo) && item.TenCanBo.ToLower().Contains(SearchDoiTuong.ToLower());
            }
            if (!string.IsNullOrEmpty(SearchCapBac))
            {
                result = result && !string.IsNullOrEmpty(item.MaCb) && item.MaCb.ToLower().Contains(SearchCapBac.ToLower());
            }
            return result;
        }

        private void OnResetFilter()
        {
            SearchMaPhuCap = string.Empty;
            SearchTenPhuCap = string.Empty;

            _phuCapView.Refresh();
        }

        private void OnUpdate()
        {
            if (Model.IsLock == true)
            {
                ViewState = FormViewState.DETAIL;
                MessageBoxHelper.Info(Resources.MsgNotEditCadre);
            }
            else
            {
                ViewState = FormViewState.UPDATE;
            }
            NextEnabled = false;
            BackEnabled = false;
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            ClosePopup?.Invoke(Model, new EventArgs());
        }

        public void OnRefresh()
        {
            Init();
        }

        private void LoadTangQuanHam(string maCapBac)
        {
            if (maCapBac.StartsWith("1") || maCapBac.StartsWith("0"))
            {
                var cbKeHoach = _tlDmCapBacKeHoachService.FindByMaCb(maCapBac);
                if (cbKeHoach != null)
                {
                    SelectedCapBacKeHoach = ItemsCapBacKeHoach.FirstOrDefault(x => x.Id.Equals(cbKeHoach.Id));
                    SelectedHslKeHoach = ItemsHslKeHoach.FirstOrDefault(x => x.Id == cbKeHoach.IdHslKeHoach);
                    SelectedHslKeHoachTran = ItemsHslKeHoach.FirstOrDefault(x => x.Id == cbKeHoach.IdHslTran);
                    Model.ThoiHanTangCb = cbKeHoach.ThoiHanTang;
                }
                else
                {
                    SelectedHslKeHoach = null;
                    SelectedHslKeHoachTran = null;
                }
            }
            else
            {
                var cbKeHoach = _tlDmCapBacKeHoachService.FindByMaCbAndHslAndNhom(maCapBac, HeSoLuong, SelectedNhom == null ? string.Empty : SelectedNhom.ValueItem);
                if (cbKeHoach != null)
                {
                    SelectedCapBacKeHoach = ItemsCapBacKeHoach.FirstOrDefault(x => x.Id == cbKeHoach.Id);
                    SelectedHslKeHoach = ItemsHslKeHoach.FirstOrDefault(x => x.Id == cbKeHoach.IdHslKeHoach);
                    SelectedHslKeHoachTran = ItemsHslKeHoach.FirstOrDefault(x => x.Id == cbKeHoach.IdHslTran);
                    Model.ThoiHanTangCb = cbKeHoach.ThoiHanTang;
                }
                else
                {
                    SelectedHslKeHoach = null;
                    SelectedHslKeHoachTran = null;
                }
            }

            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedHslKeHoach));
            OnPropertyChanged(nameof(SelectedHslKeHoachTran));
            OnPropertyChanged(nameof(SelectedCapBacKeHoach));
            OnPropertyChanged(nameof(SelectedCapBacKeHoachTran));
            OnPropertyChanged(nameof(ItemsCapBacKeHoach));
        }

        private void OnCloseSearchDatagrid()
        {
            IsPopupOpen = false;
            OnPropertyChanged(nameof(IsPopupOpen));
        }

        private void OnSearchCanBo()
        {
            _canBoSearchView.Refresh();
            IsPopupOpen = true;
        }

        private void OnChooseCanBo()
        {
            Model = SelectedCanBo;
            NgayNhapNgu = Model.NgayNn;
            NgayXuatNgu = Model.NgayXn;
            NgayTaiNgu = Model.NgayTn;
            SelectedAllowence = null;
            LoadDonVi();
            LoadEnabled();
            LoadData();
            LoadDanhMucCapBac();
            LoadListNhom();
            LoadCapBacKeHoach();
            LoadHslKeHoach();
            LoadDanhMucChucVu();
            LoadDanhMucTangGiam();
            LoadGender();
            LoadDonVi();
            LoadCanBo();
            FindCongThucLuong();
            SearchCapBac = string.Empty;
            SearchDoiTuong = string.Empty;
            SearchSoSoLuong = string.Empty;
            IsPopupOpen = false;
        }

        public void SaveBangLuong(CadresModel cadres)
        {

            try
            {
                TlDsCapNhapBangLuong tlDsCapNhapBangLuong = new TlDsCapNhapBangLuong();
                TlDSCapNhapBangLuongModel tlDsCapNhapBangLuongModel = new TlDSCapNhapBangLuongModel();
                tlDsCapNhapBangLuongModel.TenDsCnbluong = string.Format("Bảng lương {0} - {1} - {2}", cadres.Thang, cadres.Nam, SelectedDonVi.TenDonVi);
                tlDsCapNhapBangLuongModel.MaCachTl = CachTinhLuong.CACH0;
                tlDsCapNhapBangLuongModel.Status = true;
                tlDsCapNhapBangLuongModel.KhoaBangLuong = false;
                tlDsCapNhapBangLuongModel.NgayTaoBL = DateTime.Now;
                tlDsCapNhapBangLuongModel.MaCbo = cadres.Parent;
                DateTime firstDayOfMonth = new DateTime((int)cadres.Nam, (int)cadres.Thang, 1);
                DateTime lastDayOfMonth = new DateTime((int)cadres.Nam, (int)cadres.Thang, 1).AddMonths(1).AddDays(-1);
                tlDsCapNhapBangLuongModel.TuNgay = firstDayOfMonth;
                tlDsCapNhapBangLuongModel.DenNgay = lastDayOfMonth;
                tlDsCapNhapBangLuongModel.Thang = cadres.Thang;
                tlDsCapNhapBangLuongModel.Nam = cadres.Nam;
                tlDsCapNhapBangLuongModel.NguoiTao = _sessionService.Current.Principal;
                tlDsCapNhapBangLuongModel.Note = CachTinhLuong.NOTE;
                _mapper.Map(tlDsCapNhapBangLuongModel, tlDsCapNhapBangLuong);
                _tlDsCapNhapBangLuongService.Add(tlDsCapNhapBangLuong);
                var id = tlDsCapNhapBangLuong.Id;

                // lưu bảng lương tháng chi tiết
                var dataCanBo = _tlBangLuongThangService.FindCbLuong(Model.Thang, Model.Nam, Model.Parent).Where(x => x.IsDelete == true).ToList();
                IEnumerable<CadresModel> cadresModels = _mapper.Map<ObservableCollection<CadresModel>>(dataCanBo);
                ObservableCollection<TlBangLuongThangModel> tlBangLuongThangModels = new ObservableCollection<TlBangLuongThangModel>();

                foreach (var item in cadresModels)
                {
                    TlBangLuongThangModel tlBangLuongThangModel = new TlBangLuongThangModel();
                    var listPhuCap = _tlCanBoPhuCapService.FindByMaCanBo(item.MaCanBo);
                    var listPhuCapModel = _mapper.Map<ObservableCollection<TlCanBoPhuCapModel>>(listPhuCap).Select(x =>
                    {
                        x.GiaTri = (x.MaPhuCap == PhuCap.NTN && x.GiaTri < 5) ? x.GiaTri = 0 : x.GiaTri;
                        return x;
                    }).ToList();
                    if (listPhuCapModel != null && listPhuCapModel.Count > 0)
                    {

                        foreach (var congThucLuong in DsCongThucLuong)
                        {
                            congThucLuong.Value = 0;
                            congThucLuong.IsCalculated = false;
                        }
                        foreach (var item1 in listPhuCap)
                        {
                            var bangLuong = CreateBangLuongThangModel(id, item, item1.MaPhuCap, item1.GiaTri);
                            tlBangLuongThangModels.Add(bangLuong);
                        }
                        Dictionary<string, decimal> results = new Dictionary<string, decimal>();
                        foreach (var cachTinhLuong in DsCongThucLuong)
                        {
                            results.Add(cachTinhLuong.MaCot, TinhLuong(listPhuCapModel, cachTinhLuong));
                        }

                        var keys = results.Keys;
                        foreach (var key in keys.ToList())
                        {
                            string value = results[key].ToString("N4");
                            var bangLuong = tlBangLuongThangModels.Where(x => x.MaPhuCap == key && x.MaCbo == item.MaCanBo).FirstOrDefault();
                            bangLuong.GiaTri = Decimal.Parse(value);
                        }
                    }
                }
                IEnumerable<TlBangLuongThang> tlBangLuongThangs = _mapper.Map<ObservableCollection<TlBangLuongThang>>(tlBangLuongThangModels);
                _tlBangLuongThangService.Add(tlBangLuongThangs);

                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<TlDSCapNhapBangLuongModel>(tlDsCapNhapBangLuong));

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void FindCongThucLuong()
        {
            var data = _tlDmCachTinhLuongChuanService.FindAll().ToList();
            DsCongThucLuong = _mapper.Map<List<TlCachTinhLuongModel>>(data);

        }

        private TlCachTinhLuongModel CheckExitCongThuc(string congThuc)
        {
            return DsCongThucLuong.Where(x => x.MaCot == congThuc).FirstOrDefault();
        }

        private TlBangLuongThangModel CreateBangLuongThangModel(Guid id, CadresModel cadresModel, string maPhuCap,
            decimal? giaTri)
        {
            TlBangLuongThangModel model = new TlBangLuongThangModel();
            model.Parent = id;
            model.MaCachTl = CachTinhLuong.CACH0;
            model.Thang = cadresModel.Thang;
            model.Nam = cadresModel.Nam;
            model.MaCbo = cadresModel.MaCanBo;
            model.MaCb = cadresModel.MaCb;
            model.TenCbo = cadresModel.TenCanBo;
            model.MaDonVi = cadresModel.Parent;
            model.MaPhuCap = maPhuCap;
            model.MaCbo = cadresModel.MaCanBo;
            model.GiaTri = giaTri;
            model.MaHieuCanBo = cadresModel.MaHieuCanBo;
            return model;
        }

        private decimal TinhLuong(List<TlCanBoPhuCapModel> tlBangLuongThangModel, TlCachTinhLuongModel congThucLuong)
        {
            if (congThucLuong.IsCalculated == true)
            {
                return congThucLuong.Value;
            }
            else
            {
                var data = new Dictionary<string, object>();
                List<string> phuCap = congThucLuong.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (congThucLuong.CongThuc != PhuCap.THUETNCN_TT_CONGTHUC)
                {
                    foreach (var congThuc in phuCap)
                    {
                        var congThucExit = CheckExitCongThuc(congThuc);
                        if (congThucExit != null)
                        {
                            data.Add(congThuc, TinhLuong(tlBangLuongThangModel, congThucExit));
                        }
                        else
                        {
                            var property = tlBangLuongThangModel.Where(x => x.MaPhuCap == congThuc).FirstOrDefault();
                            if (property != null)
                            {
                                data.Add(congThuc, property.GiaTri ?? 0);
                            }
                        }
                    }
                }
                else if (congThucLuong.CongThuc == PhuCap.THUETNCN_TT_CONGTHUC)
                {
                    var luongThueTNCN = CheckExitCongThuc(PhuCap.LUONGTHUE_TT);
                    if (luongThueTNCN.Value == 0)
                    {
                        data.Add(PhuCap.LUONGTHUE_TT, TinhLuong(tlBangLuongThangModel, luongThueTNCN));
                    }
                    else
                    {
                        var tien = Convert.ToString(ThueTN(luongThueTNCN.Value));
                        data.Add(PhuCap.THUETNCN_TT_CONGTHUC, tien);
                    }
                }
                congThucLuong.IsCalculated = true;
                if (data.Count() > 0)
                {
                    var result = EvalExtensions.Execute(congThucLuong.CongThuc, data);
                    congThucLuong.Value = Decimal.Parse(result.ToString());
                    return congThucLuong.Value;
                }
            }
            return 0;
        }

        private decimal ThueTN(decimal luongThuThue)
        {
            var data = _tlBangLuongThangService.FindThue().OrderBy(x => x.ThuNhapTu).ToList();
            decimal tienThue = 0;
            decimal t = luongThuThue;
            var DmThuThue = _mapper.Map<List<TlDmThueThuNhapCaNhanModel>>(data);
            if (luongThuThue <= 0)
            {
                return 0;
            }
            else
            {
                foreach (var item in DmThuThue)
                {
                    if (luongThuThue >= (decimal)item.ThuNhapDen && item.ThuNhapDen != 0)
                    {
                        tienThue += ((decimal)item.ThuNhapDen - (decimal)item.ThuNhapTu) * ((decimal)item.ThueXuat / 100);
                        t = t - ((decimal)item.ThuNhapDen - (decimal)item.ThuNhapTu);
                    }
                    else if (item.ThuNhapDen == 0)
                    {
                        tienThue += (luongThuThue - (decimal)item.ThuNhapTu) * ((decimal)item.ThueXuat / 100);
                    }
                    else if (luongThuThue < (decimal)item.ThuNhapDen)
                    {
                        decimal tien = t * ((decimal)item.ThueXuat / 100);
                        tienThue += tien;
                        return tienThue;
                    }
                }
                return tienThue;
            }
        }

        private void DetailCanBoCheDoBHXHModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlCanBoCheDoBHXHModel objectSender = (TlCanBoCheDoBHXHModel)sender;
            if (args.PropertyName == nameof(TlCanBoCheDoBHXHModel.SSoQuyetDinh)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.DDenNgay)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.DNgayQuyetDinh)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.FSoTien)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.IThangLuongCanCuDong)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.INamCanCuDong)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.FGiaTriCanCu)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.FSoNgayNghiPhep)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.FSoNgayHuongBHXH))
            {
                List<TlCanBoCheDoBHXHModel> listCheDoBHXH = DataCanBoCheDoBHXH.Where(x => !x.IsDeleted).ToList();
                OnPropertyChanged(nameof(DataCanBoCheDoBHXH));
                OnPropertyChanged(nameof(Model));
                _isModifiedBHXH = true;
            }
            var existingSoNgayHuong = _canBoCheDoBHXHChiTietService.ExistSoNgayHuong(objectSender.SMaCanBo ?? Model.MaCanBo, objectSender.SMaCheDo, objectSender.IThang, objectSender.INam);

            if (existingSoNgayHuong > 0)
            {
                objectSender.FSoNgayHuongBHXH = existingSoNgayHuong;
            }
            else if (objectSender.DTuNgay != null && objectSender.DDenNgay != null && existingSoNgayHuong <= 0)
            {
                double value = CountDayOfBHXH(objectSender) - objectSender.FSoNgayNghiPhep.GetValueOrDefault();
                objectSender.FSoNgayHuongBHXH = value;
                if (objectSender.FSoNgayHuongBHXH.GetValueOrDefault() < 0)
                {
                    MessageBoxHelper.Info(string.Format(Resources.ValueNotLessThanZero, "Số ngày hưởng chế độ BHXH"));
                    return;
                }
            }
            else
            {
                objectSender.FSoNgayHuongBHXH = objectSender.FSoNgayHuongBHXH;
            }
            objectSender.IsModified = true;
            IsReadOnlyBHXH = objectSender.IsHangCha;
        }

        private void LoadDataCheDoBHXH()
        {
            var maCanBo = Model.MaCanBo;
            var maHieuCanBo = Model.MaHieuCanBo;
            var thang = Model.Thang;
            var nam = Model.Nam;
            var latestMonth = _tlBangLuongThangService.GetLatestSalary(maHieuCanBo, thang, nam);
            var listCanBoCheDoBHXH = _tlCanBoCheDoBHXHService.GetCanBoCheDoIndex(maCanBo).ToList();
            var listAllCanBoCheDoBHXH = _tlCanBoCheDoBHXHService.GetDataCheDoBHXH(maCanBo).ToList();
            var lstCanBoBHXHModel = _mapper.Map<List<TlCanBoCheDoBHXHModel>>(listCanBoCheDoBHXH);
            if (lstCanBoBHXHModel == null || lstCanBoBHXHModel.Count == 0) return;
            List<TlCanBoCheDoBHXHModel> results = new List<TlCanBoCheDoBHXHModel>();
            DataCanBoCheDoBHXH = _mapper.Map<ObservableCollection<TlCanBoCheDoBHXHModel>>(lstCanBoBHXHModel.Where(x => x.IsDisplay));
            AllDataCanBoCheDoBHXH = _mapper.Map<ObservableCollection<TlCanBoCheDoBHXHModel>>(listAllCanBoCheDoBHXH);

            foreach (var item in DataCanBoCheDoBHXH)
            {
                item.INam = nam;
                item.IThang = thang;

                if (!item.IsHangCha)
                {
                    if (item.IThangLuongCanCuDong == Model.Thang && item.INamCanCuDong == Model.Nam)
                    {
                        item.IThangLuongCanCuDong = Model.Thang;
                        item.INamCanCuDong = Model.Nam;
                    }
                    else if (latestMonth != null && item.IThangLuongCanCuDong.GetValueOrDefault() == 0 && item.INamCanCuDong.GetValueOrDefault() == 0)
                    {
                        item.IThangLuongCanCuDong = latestMonth.Thang;
                        item.INamCanCuDong = latestMonth.Nam;
                    }
                    LoadCbxThangCanCuDong();
                }
                item.PropertyChanged += DetailCanBoCheDoBHXHModel_PropertyChanged;
            }
            _cheDoView = CollectionViewSource.GetDefaultView(DataCanBoCheDoBHXH);
            _cheDoView.GroupDescriptions.Add(new PropertyGroupDescription("STenCheDoCha"));
            _cheDoView.Filter = ListCheDoFilter;
        }

        private void LoadCbxThangCanCuDong()
        {
            var currMonth = Model.Thang;
            var currYear = Model.Nam;
            CbxThangLuongCanCuDong = new List<ComboboxItem>();
            for (int i = 1; i <= currMonth; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                CbxThangLuongCanCuDong.Add(month);
            }

            CbxNamCanCuDong = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= currYear; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                CbxNamCanCuDong.Add(year);
            }
        }

        private void SaveCheDoBHXH()
        {
            if (DataCanBoCheDoBHXH != null)
            {
                List<TlCanBoCheDoBHXHModel> listCheDoAdd = DataCanBoCheDoBHXH.Where(x => !x.IsHangCha && x.IsModified && x.Id == Guid.Empty).ToList();
                List<TlCanBoCheDoBHXHModel> listCheDoEdit = DataCanBoCheDoBHXH.Where(x => !x.IsHangCha && x.IsModified && x.Id != Guid.Empty).ToList();

                var currYear = Model.Nam;
                var currMonth = Model.Thang;
                var maHieuCanBo = Model.MaHieuCanBo;
                var latestMonth = _tlBangLuongThangService.GetLatestSalary(maHieuCanBo, currMonth, currYear);

                if (latestMonth != null)
                {
                    _iBaseSalaryMonth = latestMonth.Thang;
                    _iBaseSalaryYear = latestMonth.Nam;
                }

                if (listCheDoAdd.Count > 0)
                {
                    UpdateBaseSalaryMotnh(listCheDoAdd, latestMonth);
                    foreach (var item in listCheDoAdd)
                    {
                        item.SMaCanBo = Model.MaCanBo;
                        if (item.Id == Guid.Empty)
                            item.Id = Guid.NewGuid();
                        if (BhxhSalary.GIAMTRU_BH.Contains(item.SMaCheDo))
                            UpdateCanBoCheDoCon(item);

                    }
                    var lstEntities = _mapper.Map<List<TlCanBoCheDoBHXH>>(listCheDoAdd);
                    _tlCanBoCheDoBHXHService.AddRangeCanBoCheDo(lstEntities);
                }

                if (listCheDoEdit.Count > 0)
                {
                    UpdateBaseSalaryMotnh(listCheDoEdit, latestMonth);
                    foreach (var item in listCheDoEdit)
                    {
                        var cheDoBHXH = _tlCanBoCheDoBHXHService.FindCanBoCheDo(item.Id);
                        if (cheDoBHXH != null)
                        {
                            _mapper.Map(item, cheDoBHXH);
                            _tlCanBoCheDoBHXHService.UpdateCanBoCheDo(cheDoBHXH);
                            if (BhxhSalary.GIAMTRU_BH.Contains(item.SMaCheDo))
                                UpdateCanBoCheDoCon(item);
                        }
                    }
                }
                //Xóa item trống
                var listCanBoCheDoBHXH = _tlCanBoCheDoBHXHService.GetDataCheDoBHXH(Model.MaCanBo).ToList();
                var lstCanBoBHXHModel = _mapper.Map<List<TlCanBoCheDoBHXHModel>>(listCanBoCheDoBHXH);
                var deleteItem = lstCanBoBHXHModel.Where(x => x.IsDelete).ToList();
                if (deleteItem.Any())
                {
                    foreach (var item in deleteItem)
                    {
                        if (item.Id != Guid.Empty)
                            _tlCanBoCheDoBHXHService.DeleteCanBoCheDo(item.Id);
                        var canBoCheDoChiTiet = _canBoCheDoBHXHChiTietService.GetCanBoCheDoChiTiet(Model.MaCanBo, item.SMaCheDo,
                            Model.Thang.GetValueOrDefault(), Model.Nam.GetValueOrDefault()).ToList();
                        if (canBoCheDoChiTiet.Any())
                            _canBoCheDoBHXHChiTietService.RemoveRange(canBoCheDoChiTiet);
                    }
                }
                UpdateStatusHuongCheDoBHXH();

                foreach (var item in DataCanBoCheDoBHXH)
                {
                    UpdateCBCDChiTiet(item.SMaCheDo);
                }

                DeleteCBCDChiTiet();
            }
        }

        protected void UpdateCanBoCheDoCon(TlCanBoCheDoBHXHModel item)
        {
            try
            {
                var cachTinhLuong = _tlDmCachTinhLuongBaoHiemService.FindByMaCot(item.SMaCheDo);
                List<string> lstCheDo = new List<string>();

                if (cachTinhLuong != null)
                {
                    lstCheDo = cachTinhLuong.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                }

                if (item.SMaCheDo == BhxhSalary.BENHDAINGAY_D14NGAY)
                {
                    lstCheDo.Add(BhxhSalary.BDN_D14N_BHXHCN_TT);
                    lstCheDo.Add(BhxhSalary.BDN_D14N_BHYTCN_TT);
                }

                if (item.SMaCheDo == BhxhSalary.OMKHAC_D14NGAY)
                {
                    lstCheDo.Add(BhxhSalary.OK_D14N_BHXHCN_TT);
                    lstCheDo.Add(BhxhSalary.OK_D14N_BHYTCN_TT);
                }

                var lstCheDoCon = AllDataCanBoCheDoBHXH.Where(x => lstCheDo.Contains(x.SMaCheDo));
                List<TlCanBoCheDoBHXHModel> lstCheDoConNew = new List<TlCanBoCheDoBHXHModel>();

                if (lstCheDoCon != null)
                {
                    foreach (var cheDo in lstCheDoCon)
                    {
                        if (cheDo.Id == Guid.Empty)
                        {
                            cheDo.Id = Guid.NewGuid();
                            cheDo.FSoNgayHuongBHXH = item.FSoNgayHuongBHXH;
                            cheDo.SMaCanBo = item.SMaCanBo;
                            cheDo.INam = item.INam;
                            cheDo.IThang = item.IThang;
                            cheDo.IThangLuongCanCuDong = item.IThangLuongCanCuDong;
                            cheDo.INamCanCuDong = item.INamCanCuDong;
                            cheDo.FSoTien = item.FSoTien;
                            lstCheDoConNew.Add(cheDo);
                        }
                        else
                        {
                            var cheDoBHXHCon = _tlCanBoCheDoBHXHService.FindCanBoCheDo(cheDo.Id);
                            cheDoBHXHCon.FSoNgayHuongBHXH = item.FSoNgayHuongBHXH;
                            cheDoBHXHCon.SMaCanBo = item.SMaCanBo;
                            cheDoBHXHCon.INam = item.INam;
                            cheDoBHXHCon.IThang = item.IThang;
                            cheDoBHXHCon.IThangLuongCanCuDong = item.IThangLuongCanCuDong;
                            cheDoBHXHCon.INamCanCuDong = item.INamCanCuDong;
                            cheDoBHXHCon.FSoTien = item.FSoTien;
                            _tlCanBoCheDoBHXHService.UpdateCanBoCheDo(cheDoBHXHCon);
                        }

                    }
                    if (lstCheDoConNew.Any())
                    {
                        _tlCanBoCheDoBHXHService.AddRangeCanBoCheDo(_mapper.Map<List<TlCanBoCheDoBHXH>>(lstCheDoConNew));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected void OnDeleteCheDoBHXH()
        {
            if (DataCanBoCheDoBHXH != null && DataCanBoCheDoBHXH.Count > 0 && SelectedCanBoCheDoBHXH != null)
            {
                SelectedCanBoCheDoBHXH.IsDeleted = !SelectedCanBoCheDoBHXH.IsDeleted;
                OnPropertyChanged(nameof(DataCanBoCheDoBHXH));
            }
        }

        private void GetHolidays()
        {
            LstHoliday = new List<DateTime?>();
            var holidays = _tTlDmNgayNghiService.FindByYear(_sessionService.Current.YearOfWork);
            if (holidays != null)
            {
                foreach (var typeH in holidays)
                {
                    DateTime? currDate = typeH.DTuNgay.GetValueOrDefault().Date;
                    while (currDate <= typeH.DDenNgay.GetValueOrDefault().Date)
                    {
                        LstHoliday.Add(currDate);
                        currDate = currDate.GetValueOrDefault().AddDays(1);
                    }
                }
            }
        }

        private int CountHolidays(DateTime? startDate, DateTime? endDate, List<DateTime?> lstHoliday)
        {
            int holidays = 0;
            DateTime? currDate = startDate.GetValueOrDefault().Date;

            while (currDate <= endDate.GetValueOrDefault().Date)
            {
                if (lstHoliday.Contains(currDate))
                {
                    holidays++;
                }
                currDate = currDate.GetValueOrDefault().AddDays(1);
            }

            return holidays;
        }

        private double CountDayOfBHXH(TlCanBoCheDoBHXHModel item)
        {
            double soNgayHuong;
            int sunDays = 0;
            int satDays = 0;
            var period = (item.DDenNgay.GetValueOrDefault() - item.DTuNgay.GetValueOrDefault()).Days + 1;
            for (int i = 0; i < period; i++)
            {
                DateTime currDay = item.DTuNgay.GetValueOrDefault().AddDays(i);
                if (currDay.DayOfWeek == DayOfWeek.Sunday)
                {
                    sunDays++;
                }
                if (currDay.DayOfWeek == DayOfWeek.Saturday)
                {
                    satDays++;
                }
            }

            var holidays = CountHolidays(item.DTuNgay, item.DDenNgay, LstHoliday);
            //TH1, CN 1,LE 1,T7 1
            if (item.BTinhCN.GetValueOrDefault() && item.BTinhNgayLe.GetValueOrDefault() & item.BTinhT7.GetValueOrDefault())
            {
                soNgayHuong = period;
            }
            // TH2, CN 1,LE 0,T7 1
            else if (item.BTinhCN.GetValueOrDefault() && !item.BTinhNgayLe.GetValueOrDefault() & item.BTinhT7.GetValueOrDefault())
            {
                soNgayHuong = period - holidays;
            }
            // TH3, CN 1,LE 0,T7 0
            else if (item.BTinhCN.GetValueOrDefault() && !item.BTinhNgayLe.GetValueOrDefault() & !item.BTinhT7.GetValueOrDefault())
            {
                soNgayHuong = period - satDays - holidays;
            }
            // TH4, CN 1,LE 1,T7 0
            else if (item.BTinhCN.GetValueOrDefault() && item.BTinhNgayLe.GetValueOrDefault() & !item.BTinhT7.GetValueOrDefault())
            {
                soNgayHuong = period - satDays;
            }
            // TH5, CN 0,LE 1,T7 1
            else if (!item.BTinhCN.GetValueOrDefault() && item.BTinhNgayLe.GetValueOrDefault() & item.BTinhT7.GetValueOrDefault())
            {
                soNgayHuong = period - sunDays;
            }
            // TH6, CN 0,LE 0,T7 1
            else if (!item.BTinhCN.GetValueOrDefault() && !item.BTinhNgayLe.GetValueOrDefault() & item.BTinhT7.GetValueOrDefault())
            {
                soNgayHuong = period - sunDays - holidays;
            }
            // TH7, CN 0,LE 1,T7 0
            else if (!item.BTinhCN.GetValueOrDefault() && item.BTinhNgayLe.GetValueOrDefault() & !item.BTinhT7.GetValueOrDefault())
            {
                soNgayHuong = period - sunDays - satDays;
            }
            // TH8, CN 0,LE 0,T7 0
            else
            {
                soNgayHuong = period - sunDays - satDays - holidays;
            }

            return soNgayHuong;
        }

        private void UpdateStatusHuongCheDoBHXH()
        {
            var isExistCanBoCheDo = _tlCanBoCheDoBHXHService.ExistCanBoCheDo(Model.MaCanBo);
            Model.BTinhBHXH = isExistCanBoCheDo ? true : false;
        }

        private void OnResetRegimeFilter()
        {
            SearchMaCheDo = string.Empty;
            SearchTenCheDo = string.Empty;

            _cheDoView.Refresh();
        }

        public List<TlCanBoCheDoBHXHModel> Recusive(TlCanBoCheDoBHXHModel item, List<TlCanBoCheDoBHXHModel> lstItem)
        {
            List<TlCanBoCheDoBHXHModel> lstData = new List<TlCanBoCheDoBHXHModel>();
            lstData.Add(item);
            if (lstItem.Any(n => n.SMaCheDoCha == item.SMaCheDo))
            {
                foreach (var child in lstItem.Where(n => n.SMaCheDoCha == item.SMaCheDo).OrderBy(n => n.SMaCheDo))
                {
                    lstData.AddRange(Recusive(child, lstItem));
                }
            }
            return lstData;
        }

        private void OnSelectionDoubleClick(object obj)
        {
            OnOpenCadresBHXHDetail((TlCanBoCheDoBHXHModel)obj);
        }

        private void OnOpenCadresBHXHDetail(TlCanBoCheDoBHXHModel obj)
        {
            try
            {
                if (SelectedCanBoCheDoBHXH == null || SelectedCanBoCheDoBHXH.BHangCha == true)
                {
                    return;
                }
                _lstMaCheDoBHXH = SelectedCanBoCheDoBHXH.SMaCheDo;
                CadresBHXHViewModel.TlCanBoCheDoBHXHModel = SelectedCanBoCheDoBHXH;
                CadresBHXHViewModel.SMaCanBo = Model.MaCanBo;
                CadresBHXHViewModel.IThang = Model.Thang.GetValueOrDefault();
                CadresBHXHViewModel.INam = Model.Nam.GetValueOrDefault();
                CadresBHXHViewModel.Init();
                CadresBHXHViewModel.ShowDialogHost("CadresDetail");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void RefreshCBCD(object sender, EventArgs e)
        {
            var maCanBo = Model.MaCanBo;
            var thang = Model.Thang;
            var nam = Model.Nam;
            foreach (var item in DataCanBoCheDoBHXH.Where(x => x.SMaCheDo == _lstMaCheDoBHXH))
            {
                var canBoCheDoChiTiet = _canBoCheDoBHXHChiTietService.GetTongSoNgayHuong(maCanBo, item.SMaCheDo, thang.GetValueOrDefault(), nam.GetValueOrDefault());
                if (canBoCheDoChiTiet != null)
                {
                    item.DTuNgay = canBoCheDoChiTiet.DTuNgay;
                    item.DDenNgay = canBoCheDoChiTiet.DDenNgay;
                    item.FSoNgayHuongBHXH = canBoCheDoChiTiet.FSoNgayHuongBHXH;
                }
            }
        }

        private void UpdateCBCDChiTiet(string sMaCheDo)
        {
            var canBoCheDoChiTiet = _canBoCheDoBHXHChiTietService.GetCanBoCheDoChiTiet(Model.MaCanBo, sMaCheDo, Model.Thang.GetValueOrDefault(), Model.Nam.GetValueOrDefault()).ToList();
            if (canBoCheDoChiTiet.Any())
            {
                foreach (var item in canBoCheDoChiTiet)
                {
                    item.BTrangThai = true;
                    _canBoCheDoBHXHChiTietService.UpdateCBCDChiTiet(item);
                }
            }
        }

        private void DeleteCBCDChiTiet()
        {
            var inactiveItems = _canBoCheDoBHXHChiTietService.GetCanBoCheDoChiTietInactive(Model.Thang.GetValueOrDefault(), Model.Nam.GetValueOrDefault()).ToList();
            if (inactiveItems.Any())
            {
                _canBoCheDoBHXHChiTietService.RemoveRange(inactiveItems);
            }
        }

        private void UpdateBaseSalaryMotnh(List<TlCanBoCheDoBHXHModel> listCheDo, TlBangLuongThang latestMonth)
        {
            foreach (var canBoCheDo in listCheDo)
            {
                if (latestMonth != null && canBoCheDo.IThangLuongCanCuDong.GetValueOrDefault() == 0 && canBoCheDo.INamCanCuDong.GetValueOrDefault() == 0)
                {
                    canBoCheDo.IThangLuongCanCuDong = _iBaseSalaryMonth;
                    canBoCheDo.INamCanCuDong = _iBaseSalaryYear;
                }
                else if (latestMonth == null && canBoCheDo.IThangLuongCanCuDong.GetValueOrDefault() == 0 && canBoCheDo.INamCanCuDong.GetValueOrDefault() == 0)
                {
                    canBoCheDo.IThangLuongCanCuDong = Model.Thang;
                    canBoCheDo.INamCanCuDong = Model.Nam;
                }
            }
        }
    }
}
