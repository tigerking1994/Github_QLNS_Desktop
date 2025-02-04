using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.TransferData.TransferCadres
{
    public class TransferDataDetailViewModel : DialogViewModelBase<CadresModel>
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
        private ICollectionView _phuCapView;
        private ICollectionView _canBoSearchView;

        public override string FuncCode => NSFunctionCode.SALARY_CHUYEN_DOI_DU_LIEU_DOI_TUONG_DETAIL;

        public override Type ContentType => typeof(View.Salary.Cadres.CadresDetail);
        public override PackIconKind IconKind => PackIconKind.AccountDetails;
        public override string Title => "Xem trước danh sách import đối tượng hưởng lương, phụ cấp";
        public override string Description => "Xem trước danh sách import đối tượng hưởng lương, phụ cấp";

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;

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
            set => SetProperty(ref _selectedAllowence, value);
        }

        private FormViewState _viewState;
        public FormViewState ViewState
        {
            get => _viewState;
            set
            {
                SetProperty(ref _viewState, value);
                OnPropertyChanged(nameof(IsReadOnly));
                if (_viewState == FormViewState.DETAIL)
                {
                    EditVisibility = Visibility.Visible;
                }
                else
                {
                    EditVisibility = Visibility.Collapsed;
                }
                OnPropertyChanged(nameof(EditVisibility));
                if (_viewState == FormViewState.ADD)
                {
                    SaveVisibility = Visibility.Visible;
                }
                else
                {
                    SaveVisibility = Visibility.Collapsed;
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

        private Visibility _saveVisibility;
        public Visibility SaveVisibility
        {
            get => _saveVisibility;
            set => SetProperty(ref _saveVisibility, value);
        }

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

        private Visibility _editVisibility;
        public Visibility EditVisibility
        {
            get => _editVisibility;
            set => SetProperty(ref _editVisibility, value);
        }

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
                if (SetProperty(ref _selectedCanBo, value) && _selectedCanBo != null)
                {
                    Model = _selectedCanBo;
                    LoadData();
                    NgayNhapNgu = Model.NgayNn;
                    NgayXuatNgu = Model.NgayXn;
                    NgayTaiNgu = Model.NgayTn;
                    LoadDonVi();
                    LoadEnabled();
                    LoadDanhMucCapBac();
                    LoadListNhom();
                    LoadCapBacKeHoach();
                    LoadHslKeHoach();
                    LoadDanhMucChucVu();
                    LoadDanhMucTangGiam();
                    LoadGender();
                    LoadDonVi();
                    LoadEnabled();
                    LoadCanBo();
                    SearchCapBac = string.Empty;
                    SearchDoiTuong = string.Empty;
                    SearchSoSoLuong = string.Empty;
                    Model.PropertyChanged += DetailModel_PropertyChanged;
                }
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

        private ObservableCollection<TlCanBoPhuCapModel> _lstCanBoPhuCap;
        public ObservableCollection<TlCanBoPhuCapModel> LstCanBoPhuCap
        {
            get => _lstCanBoPhuCap;
            set => SetProperty(ref _lstCanBoPhuCap, value);
        }

        private decimal? _heSoLuong;
        public decimal? HeSoLuong
        {
            get => _heSoLuong;
            set
            {
                if (SetProperty(ref _heSoLuong, value) && ItemsAllowence != null && ItemsAllowence.Count > 0)
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

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        public bool IsFirst { get; set; }

        public RelayCommand FirstCommand { get; }
        public RelayCommand PreviousCommand { get; }
        public RelayCommand NextCommand { get; }
        public RelayCommand LastCommand { get; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand SuaCommand { get; set; }
        public RelayCommand SearchCanBoCommand { get; set; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand SaveDataCommand { get; }
        public RelayCommand SaveAllCommand { get; }
        public RelayCommand CloseDatagridCommand { get; }
        public RelayCommand ChooseCommand { get; }

        public TransferDataDetailViewModel(
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
            ITlDmHslKeHoachService tlDmHslKeHoachService)
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

            FirstCommand = new RelayCommand(o => OnFirst());
            PreviousCommand = new RelayCommand(o => OnPrevious());
            NextCommand = new RelayCommand(o => OnNext());
            LastCommand = new RelayCommand(o => OnLast());
            SearchCommand = new RelayCommand(o => _phuCapView.Refresh());
            ResetFilterCommand = new RelayCommand(o => OnResetFilter());
            SuaCommand = new RelayCommand(o => OnUpdate());
            RefreshCommand = new RelayCommand(o => OnRefresh());
            SaveDataCommand = new RelayCommand(o => OnSaveData());
            SearchCanBoCommand = new RelayCommand(o => OnSearchCanBo());
            SaveAllCommand = new RelayCommand(o => OnSaveAll(o));
            CloseDatagridCommand = new RelayCommand(o => OnCloseSearchDatagrid());
            ChooseCommand = new RelayCommand(o => OnChooseCanBo());
        }

        public override void Init()
        {
            try
            {
                IsFirst = true;
                SearchCanBo = string.Empty;
                SearchCapBac = string.Empty;
                SearchDoiTuong = string.Empty;
                SearchSoSoLuong = string.Empty;
                MarginRequirement = new Thickness(10);
                if (Model.bKhongTinhNTN == null) Model.bKhongTinhNTN = false;
                Model.PropertyChanged += DetailModel_PropertyChanged;
                NgayNhapNgu = Model.NgayNn;
                NgayXuatNgu = Model.NgayXn;
                NgayTaiNgu = Model.NgayTn;
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
                LoadEnabled();
                LoadCanBo();
                IsFirst = false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            var data = _phuCapService.FindByCondition();
            List<AllowenceModel> listData = _mapper.Map<List<AllowenceModel>>(data);
            List<AllowenceModel> listAllowence = new List<AllowenceModel>(listData);

            foreach (var item in listAllowence)
            {
                var phuCapCha = _phuCapService.FindByMaPhuCap(item.Parent);
                if (phuCapCha != null)
                {
                    item.ParentName = string.Format("{0} - {1}", phuCapCha.MaPhuCap, phuCapCha.TenPhuCap);
                }
            }

            var dataCanBoPhuCap = LstCanBoPhuCap.Where(x => x.MaCbo == Model.MaCanBo);

            if (dataCanBoPhuCap != null && dataCanBoPhuCap.Count() > 0)
            {
                foreach (var item in dataCanBoPhuCap)
                {
                    var allowence = listAllowence.FirstOrDefault(x => x.MaPhuCap == item.MaPhuCap);
                    if (allowence != null && !string.IsNullOrEmpty(allowence.Parent))
                    {
                        allowence.GiaTri = item.GiaTri;
                        allowence.HuongPCSN = item.HuongPcSn;
                        allowence.DateEnd = item.DateEnd;
                        allowence.DateStart = item.DateStart;
                        allowence.ISoThangHuong = item.ISoThangHuong;
                    }
                }
            }

            foreach (AllowenceModel model in listAllowence)
            {
                model.PropertyChanged += DetailPhuCap_PropertyChanged;
            }

            HeSoLuong = listAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.LHT_HS).GiaTri;
            SoNguoiPhuThuoc = listAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.GTPT_SN).GiaTri;

            ItemsAllowence = new ObservableCollection<AllowenceModel>(listAllowence);
            _phuCapView = CollectionViewSource.GetDefaultView(ItemsAllowence);
            _phuCapView.GroupDescriptions.Add(new PropertyGroupDescription("ParentName"));
            _phuCapView.Filter = ListPhuCapFilter;

            OnPropertyChanged(nameof(ItemsAllowence));
        }

        private void OnSaveData()
        {
            try
            {
                string message = GetMessageValidate();
                if (!string.IsNullOrEmpty(message))
                {
                    System.Windows.Forms.MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                List<AllowenceModel> listSave = ItemsAllowence.Where(x => !x.IsHangCha && x.IsModified).ToList();
                Model.NgayNn = NgayNhapNgu;
                Model.NgayXn = NgayXuatNgu;
                Model.NgayTn = NgayTaiNgu;
                Model.NamTn = NamThamNien;
                Model.ThangTnn = ThangThamNienNghe;
                Model.MaCb = SelectedCapBac.MaCb;
                Model.Parent = SelectedDonVi.MaDonVi;
                Model.TenDonVi = SelectedDonVi.TenDonVi;

                if (SelectedGender.ValueItem == Gender.NAM)
                {
                    Model.IsNam = true;
                }
                else if (SelectedGender.ValueItem == Gender.NU)
                {
                    Model.IsNam = false;
                }

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

                if (SelectedNhom != null)
                {
                    Model.Nhom = SelectedNhom.ValueItem;
                }

                if (SelectedChucVu != null)
                {
                    Model.MaCv = SelectedChucVu.MaCv;
                }
                else
                {
                    Model.MaCv = string.Empty;
                }
                if (SelectedTangGiamItems != null)
                {
                    Model.MaTangGiam = SelectedTangGiamItems.SKyHieu;
                }

                Model.IsDelete = true;
                Model.IsLock = false;
                var dataAllAllowence = _mapper.Map<ObservableCollection<AllowenceModel>>(_phuCapService.FindAll()).ToList();

                foreach (var item in ItemsAllowence)
                {
                    var phucap = dataAllAllowence.FirstOrDefault(x => x.Id == item.Id);
                    if (phucap != null)
                    {
                        phucap.GiaTri = item.GiaTri;
                        phucap.HuongPCSN = item.HuongPCSN;
                    }
                }

                foreach (var item in dataAllAllowence)
                {
                    var phuCap = LstCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == item.MaPhuCap && x.MaCbo == Model.MaCanBo);
                    phuCap.GiaTri = item.GiaTri;
                    phuCap.HuongPcSn = item.HuongPCSN;
                    phuCap.DateStart = item.DateStart;
                    phuCap.DateEnd = item.DateEnd;
                    phuCap.ISoThangHuong = item.ISoThangHuong;
                }

                var phuCapBhxhdvHs = LstCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHXHDV_HS && x.MaCbo == Model.MaCanBo);
                if (phuCapBhxhdvHs != null)
                {
                    phuCapBhxhdvHs.GiaTri = SelectedCapBac.BhxhCq;
                }

                //var tcViecLam = LstCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.TCVIECLAM_TT && x.MaCbo == Model.MaCanBo);
                //if (tcViecLam != null)
                //{
                //    if (!Model.MaCb.StartsWith("0") || (Model.MaCb.StartsWith("0") && Model.NgayXn == null))
                //        tcViecLam.GiaTri = 0;
                //}

                var phuCapViecLam = LstCanBoPhuCap.FirstOrDefault(pc => pc.MaCbo == Model.MaCanBo && pc.MaPhuCap == PhuCap.THANG_TCVIECLAM);

                if (phuCapViecLam != null && Model.NgayXn == null)
                {
                    phuCapViecLam.GiaTri = 0;
                }

                var phuCapBhxhcnHs = LstCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHXHCN_HS && x.MaCbo == Model.MaCanBo);
                if (phuCapBhxhcnHs != null)
                {
                    phuCapBhxhcnHs.GiaTri = SelectedCapBac.HsBhxh;
                }

                var phuCapBhytdvHs = LstCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHYTDV_HS && x.MaCbo == Model.MaCanBo);
                if (phuCapBhytdvHs != null)
                {
                    phuCapBhytdvHs.GiaTri = SelectedCapBac.BhytCq;
                }

                var phuCapBhytcnHs = LstCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHYTCN_HS && x.MaCbo == Model.MaCanBo);
                if (phuCapBhytcnHs != null)
                {
                    phuCapBhytcnHs.GiaTri = SelectedCapBac.HsBhyt;
                }

                var phuCapBhtndvHs = LstCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHTNDV_HS && x.MaCbo == Model.MaCanBo);
                if (phuCapBhtndvHs != null)
                {
                    phuCapBhtndvHs.GiaTri = SelectedCapBac.BhtnCq;
                }

                var phuCapBhtncnHs = LstCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHTNCN_HS && x.MaCbo == Model.MaCanBo);
                if (phuCapBhtncnHs != null)
                {
                    phuCapBhtncnHs.GiaTri = SelectedCapBac.HsBhtn;
                }

                var phuCapNtn = LstCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.NTN && x.MaCbo == Model.MaCanBo);
                if (phuCapNtn != null)
                {
                    phuCapNtn.GiaTri = NamThamNien;
                }

                //var phuCapPccvTien = LstCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCCV_TIEN && x.MaCbo == Model.MaCanBo);
                //if (phuCapPccvTien != null && SelectedChucVu != null)
                //{
                //    phuCapPccvTien.GiaTri = SelectedChucVu.ThanhTienCv;
                //}
                //else
                //{
                //    phuCapPccvTien.GiaTri = 0;
                //}

                if (ViewState != FormViewState.ADD)
                {
                    ViewState = FormViewState.DETAIL;
                    LoadCanBo();
                }
                // Refresh state form
                ItemsAllowence.Select(x => { x.IsModified = false; return x; }).ToList();
                OnPropertyChanged(nameof(LstCanBoPhuCap));
                OnPropertyChanged(nameof(Model));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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

        private void OnSaveAll(object obj)
        {
            try
            {
                int count = 0;
                foreach (var item in LstCanBo)
                {
                    if (string.IsNullOrEmpty(item.Parent))
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("Đồng chí chưa chọn đơn vị cho {0} đối tượng.", count), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DonViItems = new ObservableCollection<TlDmDonViModel>(DonViItems.GroupBy(x => x.MaDonVi).Select(x => x.First()));

                //List<string> duplicatesDonVi = DonViItems.GroupBy(x => x.MaDonVi).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
                //if (duplicatesDonVi.Count > 0)
                //{
                //    string duplicateMessage = string.Format("Mã đơn vị: {0} trùng lặp. Vui lòng thử lại", string.Join(", ", duplicatesDonVi));
                //    System.Windows.Forms.MessageBox.Show(duplicateMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    var lstDonVi = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
                    var lstDonViAdd = DonViItems.Where(x => !string.IsNullOrWhiteSpace(x.MaDonVi) && !lstDonVi.Any(y => y.MaDonVi.Equals(x.MaDonVi))).ToList();

                    _tlDmDonViService.AddRange(_mapper.Map<List<TlDmDonVi>>(lstDonViAdd));
                    _cadresService.BulkInsert(_mapper.Map<ObservableCollection<TlDmCanBo>>(LstCanBo));
                    _tlCanBoPhuCapService.BulkInsert(_mapper.Map<ObservableCollection<TlCanBoPhuCap>>(LstCanBoPhuCap));
                }, (s, e) =>
                {
                    IsLoading = false;
                    if (e.Error == null)
                    {
                        System.Windows.Forms.MessageBox.Show(string.Format("Chuyển đổi dữ liệu {0} đối tượng thành công!", LstCanBo.Count()), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SavedAction?.Invoke(LstCanBo);
                        Window window = obj as Window;
                        window.Close();
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                        System.Windows.Forms.MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadCanBo()
        {
            BackEnabled = false;
            NextEnabled = false;
            _canBoSearchView = CollectionViewSource.GetDefaultView(LstCanBo);
            _canBoSearchView.Filter = CanBoFilter;

            LoadEnabled();

            OnPropertyChanged(nameof(LstCanBo));
            OnPropertyChanged(nameof(SelectedCanBo));
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
            _itemsHslKeHoach = _mapper.Map<ObservableCollection<TlDmHslKeHoachModel>>(data);
            _selectedHslKeHoach = _itemsHslKeHoach.FirstOrDefault(x => x.Id.ToString() == Model.CbKeHoach);
            _selectedHslKeHoachTran = _itemsHslKeHoach.FirstOrDefault(x => x.Id == Model.IdLuongTran);

            OnPropertyChanged(nameof(ItemsHslKeHoach));
            OnPropertyChanged(nameof(SelectedHslKeHoach));
            OnPropertyChanged(nameof(SelectedHslKeHoachTran));
        }

        private void LoadDanhMucCapBac()
        {
            var data = _tlDmCapBacService.FindByNote();
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
                var data = _qsMucLucService.FindAll().Where(x => x.BHangCha == false && x.SHienThi != "2" && x.INamLamViec == DateTime.Now.Year).ToList();
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
            if (SelectedDonVi != null)
            {
                _selectedDonVi = _donviData.FirstOrDefault(x => x.MaDonVi == SelectedDonVi.MaDonVi);
            }

            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(DonViItems));
        }

        private void DetailPhuCap_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(AllowenceModel.GiaTri)
                || args.PropertyName == nameof(AllowenceModel.HuongPCSN)
                || args.PropertyName == nameof(AllowenceModel.DateStart)
                || args.PropertyName == nameof(AllowenceModel.DateEnd))
            {
                AllowenceModel item = (AllowenceModel)sender;
                item.IsModified = true;
                OnPropertyChanged(nameof(ItemsAllowence));

                if (item.MaPhuCap == PhuCap.LHT_HS)
                {
                    _heSoLuong = item.GiaTri;
                }

                if (item.MaPhuCap == PhuCap.GTPT_SN)
                {
                    _soNguoiPhuThuoc = item.GiaTri;
                }

                OnPropertyChanged(nameof(HeSoLuong));
                OnPropertyChanged(nameof(SoNguoiPhuThuoc));
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            CadresModel item = (CadresModel)sender;
            if (args.PropertyName == nameof(CadresModel.Tm))
            {
                var Tm = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.TM));
                if (item.Tm.HasValue && (bool)item.Tm && Tm != null)
                {
                    Tm.GiaTri = 0;
                }
                else if (item.Tm.HasValue && (bool)!item.Tm && Tm != null)
                {
                    Tm.GiaTri = 1;
                }
            }
            if (args.PropertyName == nameof(CadresModel.BHTN))
            {
                var Nop_BHTN = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.Nop_BHTN));
                if (item.BHTN.HasValue && (bool)item.BHTN && Nop_BHTN != null)
                {
                    Nop_BHTN.GiaTri = 1;
                }
                else if (item.BHTN.HasValue && (bool)!item.BHTN && Nop_BHTN != null)
                {
                    Nop_BHTN.GiaTri = 0;
                }
            }
            if (args.PropertyName == nameof(CadresModel.PCCV))
            {
                var Huong_PCCOV = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.Huong_PCCOV));
                if (item.PCCV.HasValue && (bool)item.PCCV && Huong_PCCOV != null)
                {
                    Huong_PCCOV.GiaTri = 1;
                }
                else if (item.PCCV.HasValue && (bool)!item.PCCV && Huong_PCCOV != null)
                {
                    Huong_PCCOV.GiaTri = 0;
                }
            }
            if (args.PropertyName == nameof(HeSoLuong))
            {
                var pc = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.LHT_HS));
                if (HeSoLuong <= 0)
                {
                    HeSoLuong = 0;
                }
                pc.GiaTri = HeSoLuong;
                LoadTangQuanHam(Model.MaCb);
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
            //        var pcTtl = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.TTL));
            //        if (pcTtl != null)
            //        {
            //            var ngayTruyLinh = (DateTime)item.NgayTruyLinh;
            //            //var dateTimeNow = new DateTime((int)Model.Nam, (int)Model.Thang, DateTime.DaysInMonth((int)Model.Nam, (int)Model.Thang));
            //            var monthDiff = ((int)Model.Nam - ngayTruyLinh.Year) * 12 + (int)Model.Thang - ngayTruyLinh.Month + 1;
            //            if (ngayTruyLinh.Day >= 15)
            //            {
            //                var value = monthDiff - 0.5;
            //                pcTtl.GiaTri = (decimal?)value;
            //                pcTtl.IsModified = true;
            //            }
            //            else
            //            {
            //                pcTtl.GiaTri = monthDiff;
            //                pcTtl.IsModified = true;
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
            if (_selectedCapBac.MaCb.StartsWith("1") || _selectedCapBac.MaCb.StartsWith("4"))
            {
                SelectedNhom = null;
                OnPropertyChanged(nameof(SelectedNhom));
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

            if (ViewState == FormViewState.ADD)
            {
                Visibility = Visibility.Collapsed;
            }
            else
            {
                Visibility = Visibility.Visible;
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
                    NamThamNien = DateUtils.TinhNamThamNien(NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangThamNienNghe, (int)Model.Thang, (int)Model.Nam);
                else
                    NamThamNien = 0;
                OnPropertyChanged(nameof(NamThamNien));
            }
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();
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
            End:
            return string.Join(Environment.NewLine, messages);
        }

        private void OnFirst()
        {
            SelectedCanBo = LstCanBo.First();
            NgayNhapNgu = Model.NgayNn;
            NgayXuatNgu = Model.NgayXn;
            NgayTaiNgu = Model.NgayTn;
        }

        private void OnPrevious()
        {
            var index = LstCanBo.IndexOf(Model);
            SelectedCanBo = LstCanBo[index - 1];
            NgayNhapNgu = Model.NgayNn;
            NgayXuatNgu = Model.NgayXn;
            NgayTaiNgu = Model.NgayTn;
        }

        private void OnNext()
        {
            var index = LstCanBo.IndexOf(Model);
            SelectedCanBo = LstCanBo[index + 1];
            NgayNhapNgu = Model.NgayNn;
            NgayXuatNgu = Model.NgayXn;
            NgayTaiNgu = Model.NgayTn;
        }

        private void OnLast()
        {
            SelectedCanBo = LstCanBo.Last();
            NgayNhapNgu = Model.NgayNn;
            NgayXuatNgu = Model.NgayXn;
            NgayTaiNgu = Model.NgayTn;
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
                result = result && !string.IsNullOrEmpty(item.SoSoLuong) && item.SoSoLuong.ToLower().Contains(SearchSoSoLuong.ToLower());
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
                DialogResult dialog = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgNotEditCadre), Resources.ConfirmTitle, MessageBoxButtons.OK);
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

        private void OnSearchCanBo()
        {
            _canBoSearchView.Refresh();
            IsPopupOpen = true;
        }

        private void OnCloseSearchDatagrid()
        {
            IsPopupOpen = false;
            OnPropertyChanged(nameof(IsPopupOpen));
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
            SearchCapBac = string.Empty;
            SearchDoiTuong = string.Empty;
            SearchSoSoLuong = string.Empty;
            IsPopupOpen = false;
        }

        //private void OnChooseCanBo()
        //{
        //    Model = SelectedCanBo;
        //    LoadData();
        //    LoadDanhMucCapBac();
        //    LoadDanhMucChucVu();
        //    LoadDanhMucTangGiam();
        //    LoadDonVi();
        //    LoadEnabled();
        //    SearchCapBac = string.Empty;
        //    SearchDoiTuong = string.Empty;
        //    SearchSoSoLuong = string.Empty;
        //    IsPopupOpen = false;
        //}
    }
}
