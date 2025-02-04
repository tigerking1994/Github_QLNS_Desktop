using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewCadresPlan
{
    public class NewCadresPlanDetailViewModel : DialogViewModelBase<TlDmCanBoKeHoachNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmPhuCapNq104Service _phuCapService;
        private readonly ITlDmCanBoKeHoachNq104Service _tlDmCanBoKeHoachService;
        private readonly ITlDmCapBacNq104Service _tlDmCapBacService;
        private readonly ITlDmChucVuService _tlDmChucVuService;
        private readonly ITlDmTangGiamService _tlDmTangGiamService;
        private readonly ITlCanBoPhuCapKeHoachService _tlCanBoPhuCapKeHoachService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly INsQsMucLucService _qsMucLucService;
        private readonly ITlDmCapBacKeHoachService _tlDmCapBacKeHoachService;
        private readonly ITlDmHslKeHoachService _tlDmHslKeHoachService;
        private ICollectionView _phuCapView;
        private ICollectionView _canBoSearchView;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_QUAN_LY_LUONG_KE_HOACH_DANH_SACH_DOI_TUONG_HUONG_LUONG_KE_HOACH_DETAIL;

        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagementPlan.NewCadresPlan.NewCadresPlanDetail);
        public override PackIconKind IconKind => PackIconKind.AccountDetails;
        public override string Title => "CHI TIẾT ĐỐI TƯỢNG HƯỞNG LƯƠNG, PHỤ CẤP";
        public override string Description => "Chi tiết đối tượng hưởng lương, phụ cấp";

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;

        private ObservableCollection<AllowenceNq104Model> _itemsAllowence;
        public ObservableCollection<AllowenceNq104Model> ItemsAllowence
        {
            get => _itemsAllowence;
            set => SetProperty(ref _itemsAllowence, value);
        }

        private AllowenceNq104Model _selectedAllowence;
        public AllowenceNq104Model SelectedAllowence
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

        private ObservableCollection<TlDmCapBacNq104Model> _itemsCapBac;
        public ObservableCollection<TlDmCapBacNq104Model> ItemsCapBac
        {
            get => _itemsCapBac;
            set => SetProperty(ref _itemsCapBac, value);
        }

        private TlDmCapBacNq104Model _selectedCapBac;
        public TlDmCapBacNq104Model SelectedCapBac
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

        public string ComboboxDisplayMemberPathTangGiam => nameof(SelectedTangGiamItems.SMoTa);

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

        private ObservableCollection<TlDmDonViNq104Model> _donviData;
        public ObservableCollection<TlDmDonViNq104Model> DonViItems
        {
            get => _donviData;
            set => SetProperty(ref _donviData, value);
        }

        private TlDmDonViNq104Model _selectedDonVi;
        public TlDmDonViNq104Model SelectedDonVi
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

        private ObservableCollection<TlDmCanBoKeHoachNq104Model> _lstCanBo;
        public ObservableCollection<TlDmCanBoKeHoachNq104Model> LstCanBo
        {
            get => _lstCanBo;
            set
            {
                SetProperty(ref _lstCanBo, value);
                OnPropertyChanged(nameof(Model));
            }
        }

        private TlDmCanBoKeHoachNq104Model _selectedCanBo;
        public TlDmCanBoKeHoachNq104Model SelectedCanBo
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

        public bool IsFirst { get; set; }
        public bool ChooseEnabled => SelectedCanBo != null;

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
        public RelayCommand SaveAndCloseCommand { get; }
        public RelayCommand SaveAndCopyCommand { get; }
        public RelayCommand CloseDatagridCommand { get; }
        public RelayCommand ChooseCommand { get; }

        public NewCadresPlanDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmPhuCapNq104Service phuCapService,
            ITlDmCanBoKeHoachNq104Service tlDmCanBoKeHoachService,
            ITlDmCapBacNq104Service tlDmCapBacService,
            ITlDmChucVuService tlDmChucVuService,
            ITlDmTangGiamService tlDmTangGiamService,
            ITlCanBoPhuCapKeHoachService tlCanBoPhuCapKeHoachService,
            ITlDmDonViNq104Service tlDmDonViService,
            INsQsMucLucService qsMucLucService,
            ITlDmCapBacKeHoachService tlDmCapBacKeHoachService,
            ITlDmHslKeHoachService tlDmHslKeHoachService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _tlDmCanBoKeHoachService = tlDmCanBoKeHoachService;
            _phuCapService = phuCapService;
            _tlDmCapBacService = tlDmCapBacService;
            _tlDmChucVuService = tlDmChucVuService;
            _tlDmTangGiamService = tlDmTangGiamService;
            _tlCanBoPhuCapKeHoachService = tlCanBoPhuCapKeHoachService;
            _tlDmDonViService = tlDmDonViService;
            _qsMucLucService = qsMucLucService;
            _tlDmCapBacKeHoachService = tlDmCapBacKeHoachService;
            _tlDmHslKeHoachService = tlDmHslKeHoachService;

            FirstCommand = new RelayCommand(o => OnFirst());
            PreviousCommand = new RelayCommand(o => OnPrevious());
            NextCommand = new RelayCommand(o => OnNext());
            LastCommand = new RelayCommand(o => OnLast());
            SearchCommand = new RelayCommand(o => _phuCapView.Refresh());
            ResetFilterCommand = new RelayCommand(o => OnResetFilter());
            SuaCommand = new RelayCommand(o => OnUpdate());
            RefreshCommand = new RelayCommand(o => OnRefresh());
            SaveDataCommand = new RelayCommand(o => OnSaveAndClear());
            SaveAndCloseCommand = new RelayCommand(o => OnSaveAndClose(o));
            SaveAndCopyCommand = new RelayCommand(o => OnSaveAndCopy());
            CloseDatagridCommand = new RelayCommand(o => OnCloseSearchDatagrid());
            SearchCanBoCommand = new RelayCommand(o => OnSearchCanBo());
            ChooseCommand = new RelayCommand(o => OnChooseCanBo());
        }

        public override void Init()
        {
            try
            {
                IsFirst = true;
                SearchCapBac = string.Empty;
                SearchDoiTuong = string.Empty;
                SearchSoSoLuong = string.Empty;
                SearchCanBo = string.Empty;
                IsPopupOpen = false;
                NgayNhapNgu = Model.NgayNn;
                NgayXuatNgu = Model.NgayXn;
                NgayTaiNgu = Model.NgayTn;
                MarginRequirement = new Thickness(10);
                if (Model.BKhongTinhNTN == null) Model.BKhongTinhNTN = false;
                Model.PropertyChanged += DetailModel_PropertyChanged;
                LoadData();
                LoadDanhMucCapBac();
                LoadDanhMucChucVu();
                LoadDanhMucTangGiam();
                LoadCapBacKeHoach();
                LoadListNhom();
                LoadHslKeHoach();
                LoadGender();
                LoadDonVi();
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
            var data = _tlCanBoPhuCapKeHoachService.FindCanBoPhuCapNq104(Model.MaCanBo);
            List<AllowenceNq104Model> listAllowence = _mapper.Map<List<AllowenceNq104Model>>(data);

            foreach (AllowenceNq104Model model in listAllowence)
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

            ItemsAllowence = new ObservableCollection<AllowenceNq104Model>(listAllowence);
            _phuCapView = CollectionViewSource.GetDefaultView(ItemsAllowence);
            _phuCapView.GroupDescriptions.Add(new PropertyGroupDescription("ParentName"));
            _phuCapView.Filter = ListPhuCapFilter;

            OnPropertyChanged(nameof(ItemsAllowence));
        }

        private void OnSaveData()
        {
            try
            {
                TlDmCanBoKeHoachNq104 tlDmCanBo;
                List<AllowenceNq104Model> listSave = ItemsAllowence.Where(x => !x.IsHangCha && x.IsModified).ToList();
                ObservableCollection<TlCanBoPhuCapKeHoachModel> tlCanBoPhuCapModels = new ObservableCollection<TlCanBoPhuCapKeHoachModel>();
                Model.NgayNn = NgayNhapNgu;
                Model.NgayXn = NgayXuatNgu;
                Model.NgayTn = NgayTaiNgu;
                Model.NamTn = NamThamNien;
                Model.ThangTnn = ThangThamNienNghe;
                Model.MaCb = SelectedCapBac.MaCb;
                Model.CapBac = SelectedCapBac.Note;
                Model.Parent = SelectedDonVi.MaDonVi;
                Model.TenDonVi = SelectedDonVi.TenDonVi;

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

                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    tlDmCanBo = _tlDmCanBoKeHoachService.Find(Model.Id);
                    _tlCanBoPhuCapKeHoachService.DeleteByMaCanBo(Model.MaCanBo);
                    var dataAllAllowence = _mapper.Map<ObservableCollection<AllowenceNq104Model>>(_phuCapService.FindAll()).ToList().OrderBy(x => x.MaPhuCap);

                    foreach (var item in ItemsAllowence)
                    {
                        var phucap = dataAllAllowence.FirstOrDefault(x => x.Id == item.Id);
                        if (phucap != null)
                        {
                            phucap.IsModified = item.IsModified;
                            phucap.GiaTri = item.GiaTri;
                            phucap.HuongPCSN = item.HuongPCSN;
                            phucap.DateEnd = item.DateEnd;
                            phucap.DateStart = item.DateStart;
                            phucap.ISoThangHuong = item.ISoThangHuong;
                        }
                    }

                    foreach (var item in dataAllAllowence)
                    {
                        TlCanBoPhuCapKeHoachModel tlCanBoPhuCap = new TlCanBoPhuCapKeHoachModel();
                        tlCanBoPhuCap.MaCanBo = Model.MaCanBo;
                        tlCanBoPhuCap.MaPhuCap = item.MaPhuCap;
                        tlCanBoPhuCap.GiaTri = item.GiaTri;
                        tlCanBoPhuCap.HuongPcSn = (int?)item.HuongPCSN;
                        tlCanBoPhuCap.DateStart = item.DateStart;
                        tlCanBoPhuCap.DateEnd = item.DateEnd;
                        tlCanBoPhuCap.ISoThangHuong = item.ISoThangHuong;
                        tlCanBoPhuCap.IsModified = item.IsModified;
                        tlCanBoPhuCapModels.Add(tlCanBoPhuCap);
                    }

                    var listCanBoPhuCap = _mapper.Map<ObservableCollection<TlCanBoPhuCapKeHoach>>(tlCanBoPhuCapModels);

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

                    if (!SelectedCapBac.MaCb.StartsWith("0") || Model.IsNam == true)
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

                    var phuCapNtn = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.NTN);
                    if (phuCapNtn != null)
                    {
                        phuCapNtn.GiaTri = NamThamNien;
                    }

                    Model.UserModifier = _sessionService.Current.Principal;
                    Model.DateModified = DateTime.Now;
                    _tlCanBoPhuCapKeHoachService.AddRange(listCanBoPhuCap);
                    _mapper.Map(Model, tlDmCanBo);
                    _tlDmCanBoKeHoachService.Update(tlDmCanBo);
                }
                else
                {
                    Model.Thang = Int32.Parse(Model.MaCanBo.Substring(4, 2));
                    Model.IsDelete = true;
                    Model.UserCreator = _sessionService.Current.Principal;
                    Model.DateCreated = DateTime.Now;
                    var dataAllAllowence = _mapper.Map<ObservableCollection<AllowenceNq104Model>>(_phuCapService.FindAll()).ToList();

                    foreach (var item in ItemsAllowence)
                    {
                        var phucap = dataAllAllowence.FirstOrDefault(x => x.Id == item.Id);
                        if (phucap != null)
                        {
                            phucap.GiaTri = item.GiaTri;
                            phucap.HuongPCSN = item.HuongPCSN;
                            phucap.DateEnd = item.DateEnd;
                            phucap.DateStart = item.DateStart;
                            phucap.ISoThangHuong = item.ISoThangHuong;
                        }
                    }

                    foreach (var item in dataAllAllowence)
                    {
                        TlCanBoPhuCapKeHoachModel tlCanBoPhuCap = new TlCanBoPhuCapKeHoachModel();
                        tlCanBoPhuCap.MaCanBo = Model.MaCanBo;
                        tlCanBoPhuCap.MaPhuCap = item.MaPhuCap;
                        tlCanBoPhuCap.GiaTri = item.GiaTri;
                        tlCanBoPhuCap.HuongPcSn = (int?)item.HuongPCSN;
                        tlCanBoPhuCap.DateEnd = item.DateEnd;
                        tlCanBoPhuCap.DateStart = item.DateStart;
                        tlCanBoPhuCap.ISoThangHuong = item.ISoThangHuong;
                        tlCanBoPhuCapModels.Add(tlCanBoPhuCap);
                    }

                    var listCanBoPhuCap = _mapper.Map<ObservableCollection<TlCanBoPhuCapKeHoach>>(tlCanBoPhuCapModels);

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

                    if (!SelectedCapBac.MaCb.StartsWith("0") || Model.IsNam == true)
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

                    var phuCapNtn = listCanBoPhuCap.FirstOrDefault(x => x.MaPhuCap == PhuCap.NTN);
                    if (phuCapNtn != null)
                    {
                        phuCapNtn.GiaTri = NamThamNien;
                    }

                    _tlCanBoPhuCapKeHoachService.AddRange(listCanBoPhuCap);

                    Model.Nam = DateTime.Now.Year;
                    tlDmCanBo = new TlDmCanBoKeHoachNq104();
                    _mapper.Map(Model, tlDmCanBo);
                    _tlDmCanBoKeHoachService.Add(tlDmCanBo);

                }

                if (ViewState != FormViewState.ADD)
                {
                    ViewState = FormViewState.DETAIL;
                    LoadCanBo();
                }
                MessageBoxHelper.Info("Lưu thành công.");
                // Refresh state form
                ItemsAllowence.Select(x => { x.IsModified = false; return x; }).ToList();
                SavedAction?.Invoke(tlDmCanBo);
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
                System.Windows.Forms.MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                LoaiSave = LoaiSave.SAVE_AND_CLEAR;
                OnSaveData();
                if (ViewState == FormViewState.ADD)
                {
                    TlDmCanBoKeHoachNq104Model newModel = new TlDmCanBoKeHoachNq104Model();
                    newModel.Parent = Model.Parent;
                    newModel.TenDonVi = Model.TenDonVi;
                    NgayNhapNgu = null;
                    NgayXuatNgu = null;
                    NgayTaiNgu = null;
                    NamThamNien = 0;
                    ThangThamNienNghe = 0;
                    newModel.KhongLuong = false;
                    newModel.Tm = true;
                    newModel.MaHieuCanBo = (int.Parse(Model.MaHieuCanBo) + 1).ToString();
                    newModel.MaCanBo = Model.MaCanBo.Substring(0, 6) + newModel.MaHieuCanBo;
                    newModel.SoSoLuong = newModel.MaHieuCanBo.PadLeft(7, '0');

                    Model = newModel;
                    Init();
                }
            }
        }

        private void OnSaveAndClose(object obj)
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.Forms.MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                LoaiSave = LoaiSave.SAVE_AND_CLOSE;
                OnSaveData();
                Window window = obj as Window;
                window.Close();
            }
        }

        private void OnSaveAndCopy()
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.Forms.MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                LoaiSave = LoaiSave.SAVE_AND_COPY;
                OnSaveData();
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
                _lstCanBo = new ObservableCollection<TlDmCanBoKeHoachNq104Model>(CanBoView.Cast<TlDmCanBoKeHoachNq104Model>());
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

        private void LoadDanhMucCapBac()
        {
            var data = _tlDmCapBacService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork).OrderBy(x => x.XauNoiMa).ToList();
            var listCapBac = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(data);
            var capBacItem = listCapBac.Select(x =>
            {
                var dict = data.Select(x => x.Parent).ToHashSet();
                x.IsHangCha = dict.Contains(x.MaCb);
                x.TenCha = data.FirstOrDefault(y => y.MaCb == x.Parent)?.TenCb;
                return x;
            }).Where(x => !x.IsHangCha);
            _itemsCapBac = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(capBacItem);
            SelectedCapBac = _itemsCapBac.FirstOrDefault(x => x.MaCb.Equals(Model.MaCb));
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

        private void LoadDanhMucTangGiam()
        {
            try
            {
                var data = _qsMucLucService.FindAll().Where(x => x.BHangCha == false && x.SHienThi != "2" && x.INamLamViec == DateTime.Now.Year).ToList();
                _tangGiamItems = new ObservableCollection<QsMucLucModel>();
                _tangGiamItems = _mapper.Map<ObservableCollection<QsMucLucModel>>(data);
                if (Model.MaTangGiam != null)
                {
                    _selectedTangGiamItems = _tangGiamItems.FirstOrDefault(x => x.SKyHieu == Model.MaTangGiam);
                    //if (SelectedTangGiamItems != null)
                    //{
                    //    _selectedTangGiamItems = _tangGiamItems.FirstOrDefault(x => x.IIdMlns.Equals(Model.MaTangGiam));
                    //}
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
            _donviData = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
            if (ViewState != FormViewState.ADD)
            {
                _selectedDonVi = _donviData.FirstOrDefault(x => x.MaDonVi == Model.Parent);
            }
            else
            {
                _selectedDonVi = _donviData.FirstOrDefault(x => x.MaDonVi == SelectedDonVi.MaDonVi);
            }

            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(DonViItems));
        }

        private void DetailPhuCap_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(AllowenceNq104Model.GiaTri)
                || args.PropertyName == nameof(AllowenceNq104Model.HuongPCSN)
                || args.PropertyName == nameof(AllowenceNq104Model.DateStart)
                || args.PropertyName == nameof(AllowenceNq104Model.DateEnd))
            {
                AllowenceNq104Model item = (AllowenceNq104Model)sender;
                item.IsModified = true;
                OnPropertyChanged(nameof(ItemsAllowence));

                if (PhuCap.LHT_HS.Equals(item.MaPhuCap))
                {
                    _heSoLuong = item.GiaTri;
                }

                if (PhuCap.GTPT_SN.Equals(item.MaPhuCap))
                {
                    _soNguoiPhuThuoc = item.GiaTri;
                }

                OnPropertyChanged(nameof(HeSoLuong));
                OnPropertyChanged(nameof(SoNguoiPhuThuoc));
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlDmCanBoKeHoachNq104Model item = (TlDmCanBoKeHoachNq104Model)sender;
            if (args.PropertyName == nameof(TlDmCanBoKeHoachNq104Model.Tm))
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
            if (args.PropertyName == nameof(TlDmCanBoKeHoachNq104Model.BHTN))
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
            if (args.PropertyName == nameof(TlDmCanBoKeHoachNq104Model.PCCV))
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
            if (args.PropertyName == nameof(TlDmCanBoKeHoachNq104Model.BKhongTinhNTN))
            {
                if (item.BKhongTinhNTN != null)
                    TinhNamThamNien();
            }

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
                    //phucap.GiaTri = _selectedCapBac.LhtHs;
                    phucap.IsModified = false;
                    //Model.HeSoLuong = _selectedCapBac.LhtHs;
                }
                var phucapTiLeHuong = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.TILE_HUONG);
                if (_selectedCapBac != null && phucapTiLeHuong != null)
                {
                    //phucapTiLeHuong.GiaTri = _selectedCapBac.TiLeHuong;
                    phucapTiLeHuong.IsModified = false;
                }
                OnPropertyChanged(nameof(ItemsAllowence));
                OnPropertyChanged(nameof(CadresNq104Model));
            }
            if (_selectedCapBac.MaCb.StartsWith("1") || _selectedCapBac.MaCb.StartsWith("4"))
            {
                SelectedNhom = null;
                OnPropertyChanged(nameof(SelectedNhom));
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
                if (Model.BKhongTinhNTN.HasValue && !Model.BKhongTinhNTN.Value)
                {
                    NamThamNien = DateUtils.TinhNamThamNien(NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangThamNienNghe, (int)Model.Thang, (int)Model.Nam);
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
        //var ngayThangCanBo = new DateTime((int)Model.Nam, (int)Model.Thang, DateTime.DaysInMonth((int)Model.Nam, (int)Model.Thang));
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
            var item = (AllowenceNq104Model)obj;

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

        private void OnResetFilter()
        {
            SearchMaPhuCap = string.Empty;
            SearchTenPhuCap = string.Empty;

            _phuCapView.Refresh();
        }

        private void OnUpdate()
        {
            ViewState = FormViewState.DETAIL;
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
            SearchCapBac = string.Empty;
            SearchDoiTuong = string.Empty;
            SearchSoSoLuong = string.Empty;
            IsPopupOpen = false;
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

        private bool CanBoFilter(object obj)
        {
            bool result = true;
            if (IsFirst)
            {
                return false;
            }
            var item = (TlDmCanBoKeHoachNq104Model)obj;
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
    }
}
