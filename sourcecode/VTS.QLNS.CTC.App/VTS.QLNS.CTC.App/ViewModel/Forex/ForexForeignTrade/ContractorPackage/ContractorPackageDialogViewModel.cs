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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanDetail;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForeignTrade.ContractorPackage
{
    public class ContractorPackageDialogViewModel : DialogAttachmentViewModelBase<NhDaGoiThauModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INhHdnkCacQuyetDinhService _nhHdnkCacQuyetDinhService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INhDmHinhThucChonNhaThauService _nhDmHinhThucChonNhaThauService;
        private readonly INhDmPhuongThucChonNhaThauService _nhDmPhuongThucChonNhaThauService;
        private readonly INhDmLoaiHopDongService _nhDmLoaiHopDongService;
        private readonly INhHdnkCacQuyetDinhNguonVonService _nhHdnkCacQuyetDinhNguonVonService;
        private readonly INhHdnkCacQuyetDinhChiPhiService _nhHdnkCacQuyetDinhChiPhiService;
        private readonly INhDaGoiThauService _nhDaGoiThauService;
        private readonly INhDaGoiThauChiPhiService _nhDaGoiThauChiPhiService;
        private readonly INhDaGoiThauHangMucSerrvice _nhDaGoiThauHangMucSerrvice;
        private readonly INhDaGoiThauNguonVonService _nhDaGoiThauNguonVonService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        private readonly INhHdnkCacQuyetDinhChiPhiHangMucService _nhHdnkCacQuyetDinhChiPhiHangMucService;

        public RelayCommand PhuLucHangMucCommand { get; }

        public DmTiGiaDialogViewModel DmTiGiaDialogViewModel { get; set; }
        public ContractorPackageHangMucDetailDialogViewModel ContractorPackageHangMucDetailDialogViewModel { get; set; }
        public bool IsNotQuyetDinhChiTrongNuoc { get; set; }
        public override Type ContentType => typeof(View.Forex.ForeignTrade.ContractorPackage.ContractorPackageDialog);
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.NH_DA_GOITHAU_NGOAITHUONG;

        public bool IsDetail { get; set; }
        public Guid? GoiThauDieuChinhId { get; set; }
        public bool IsUpDate { get; set; }

        private FormViewState _viewState;
        public FormViewState ViewState
        {
            get => _viewState;
            set
            {
                SetProperty(ref _viewState, value);
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        public bool IsReadOnly => ViewState == FormViewState.DETAIL;

        private List<ComboboxItem> _loaiKeHoachTongThe;
        public List<ComboboxItem> LoaiKeHoachTongThe
        {
            get => _loaiKeHoachTongThe;
            set
            {
                SetProperty(ref _loaiKeHoachTongThe, value);
                LoadSoKhTongThe();
            }
        }

        private ComboboxItem _selectedKeHoachTongThe;
        public ComboboxItem SelectedKeHoachTongThe
        {
            get => _selectedKeHoachTongThe;
            set
            {
                SetProperty(ref _selectedKeHoachTongThe, value);
                LoadSoKhTongThe();
            }
        }

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
                LoadTenNhiemVuChi();
            }
        }

        private ObservableCollection<NhKhTongTheModel> _itemsSoKhTongThe;
        public ObservableCollection<NhKhTongTheModel> ItemsSoKHTongThe
        {
            get => _itemsSoKhTongThe;
            set
            {
                SetProperty(ref _itemsSoKhTongThe, value);
            }
        }

        private NhKhTongTheModel _selectedSoKhTongThe;
        public NhKhTongTheModel SelectedSoKhTongThe
        {
            get => _selectedSoKhTongThe;
            set
            {
                SetProperty(ref _selectedSoKhTongThe, value);
                LoadTenNhiemVuChi();
            }
        }

        private List<ComboboxItem> _itemsTenNhiemVuChi;
        public List<ComboboxItem> ItemsTenNHiemVuChi
        {
            get => _itemsTenNhiemVuChi;
            set => SetProperty(ref _itemsTenNhiemVuChi, value);
        }

        private ComboboxItem _selectedTenNHiemVuChi;
        public ComboboxItem SelectedTenNhiemVuChi
        {
            get => _selectedTenNHiemVuChi;
            set
            {
                SetProperty(ref _selectedTenNHiemVuChi, value);
                LoadSoQuyetDinhSoCu();
            }
        }

        private List<ComboboxItem> _itemsLoaiSoCu;
        public List<ComboboxItem> ItemsLoaiSoCu
        {
            get => _itemsLoaiSoCu;
            set
            {
                SetProperty(ref _itemsLoaiSoCu, value);
            }
        }

        private ComboboxItem _selectedLoaiSoCu;
        public ComboboxItem SelectedLoaiSoCu
        {
            get => _selectedLoaiSoCu;
            set
            {
                SetProperty(ref _selectedLoaiSoCu, value);
                LoadSoQuyetDinhSoCu();
            }
        }

        private List<ComboboxItem> _itemsSoQuyetDinhSoCu;
        public List<ComboboxItem> ItemsSoQuyetDinhSoCu
        {
            get => _itemsSoQuyetDinhSoCu;
            set => SetProperty(ref _itemsSoQuyetDinhSoCu, value);
        }

        private ComboboxItem _selectedSoQuyetDinhSoCu;
        public ComboboxItem SoQuyetDinhSoCuSelected
        {
            get => _selectedSoQuyetDinhSoCu;
            set
            {
                SetProperty(ref _selectedSoQuyetDinhSoCu, value);
                LoadThongTinNGuonVon();
                LoadThongTinChiPhi();
            }
        }

        private ObservableCollection<NhDmHinhThucChonNhaThauModel> _itemsHinhThucChonNhathau;
        public ObservableCollection<NhDmHinhThucChonNhaThauModel> ItemsHinhThucChonNhaThau
        {
            get => _itemsHinhThucChonNhathau;
            set => SetProperty(ref _itemsHinhThucChonNhathau, value);
        }

        private NhDmHinhThucChonNhaThauModel _selectedHinhThucChonNhaThau;
        public NhDmHinhThucChonNhaThauModel SelectedHinhThucChonNhaThau
        {
            get => _selectedHinhThucChonNhaThau;
            set => SetProperty(ref _selectedHinhThucChonNhaThau, value);
        }

        private ObservableCollection<NhDmPhuongThucChonNhaThauModel> _itemsPhuongThucChoNhaThau;
        public ObservableCollection<NhDmPhuongThucChonNhaThauModel> ItemsPhuongThucChonNhaThau
        {
            get => _itemsPhuongThucChoNhaThau;
            set => SetProperty(ref _itemsPhuongThucChoNhaThau, value);
        }

        private NhDmPhuongThucChonNhaThauModel _selectedPhuongThucChonNhaThau;
        public NhDmPhuongThucChonNhaThauModel SelectedPhuongThucChonNhaThau
        {
            get => _selectedPhuongThucChonNhaThau;
            set => SetProperty(ref _selectedPhuongThucChonNhaThau, value);
        }

        private ObservableCollection<NhDmLoaiHopDongModel> _itemsLoaiHopDong;
        public ObservableCollection<NhDmLoaiHopDongModel> ItemsLoaiHopDong
        {
            get => _itemsLoaiHopDong;
            set => SetProperty(ref _itemsLoaiHopDong, value);
        }

        private NhDmLoaiHopDongModel _selectedLoaiHopDong;
        public NhDmLoaiHopDongModel SelectedLoaiHopDong
        {
            get => _selectedLoaiHopDong;
            set => SetProperty(ref _selectedLoaiHopDong, value);
        }

        private int _thoiGianThucHien;
        public int ThoiGianThucHien
        {
            get => _thoiGianThucHien;
            set => SetProperty(ref _thoiGianThucHien, value);
        }

        private ObservableCollection<NhHdnkCacQuyetDinhNguonVonModel> _itemsThongTinNguonVon;
        public ObservableCollection<NhHdnkCacQuyetDinhNguonVonModel> ItemsThongTinNguonVon
        {
            get => _itemsThongTinNguonVon;
            set => SetProperty(ref _itemsThongTinNguonVon, value);
        }

        private NhHdnkCacQuyetDinhNguonVonModel _selectedThongTinNguonVon;
        public NhHdnkCacQuyetDinhNguonVonModel SelectedThongTinNguon
        {
            get => _selectedThongTinNguonVon;
            set => SetProperty(ref _selectedThongTinNguonVon, value);
        }

        public bool? IsAllNGuonVonItemSelected
        {
            get
            {
                if (ItemsThongTinNguonVon != null)
                {
                    var selected = ItemsThongTinNguonVon.Select(x => x.IsSelected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, ItemsThongTinNguonVon);
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<NhHdnkCacQuyetDinhChiPhiModel> _itemsThongTinChiPhi;
        public ObservableCollection<NhHdnkCacQuyetDinhChiPhiModel> ItemsThongTinChiPhi
        {
            get => _itemsThongTinChiPhi;
            set => SetProperty(ref _itemsThongTinChiPhi, value);
        }

        private NhHdnkCacQuyetDinhChiPhiModel _selectedThongTinChiPhi;
        public NhHdnkCacQuyetDinhChiPhiModel SelectedThongTinChiPhi
        {
            get => _selectedThongTinChiPhi;
            set => SetProperty(ref _selectedThongTinChiPhi, value);
        }

        public bool? IsAllChiPhiItemSelected
        {
            get
            {
                if (ItemsThongTinChiPhi != null)
                {
                    var selected = ItemsThongTinChiPhi.Select(x => x.IsSelected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, ItemsThongTinChiPhi);
                    OnPropertyChanged();
                }
            }
        }

        private List<NhDaGoiThauHangMucModel> _listHangMuc;
        public List<NhDaGoiThauHangMucModel> ListHangMuc
        {
            get => _listHangMuc;
            set
            {
                SetProperty(ref _listHangMuc, value);
            }
        }

        private List<NhDaGoiThauHangMucModel> ListHangMucSave = new List<NhDaGoiThauHangMucModel>();

        private double? _fGiaTriNgoaiTeKhacNguonVon;
        public double? FGiaTriNgoaiTeKhacNguonVon
        {
            get => _fGiaTriNgoaiTeKhacNguonVon;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacNguonVon, value);
        }

        private double? _fGiaTriUSDNguonVon;
        public double? FGiaTriUSDNguonVon
        {
            get => _fGiaTriUSDNguonVon;
            set => SetProperty(ref _fGiaTriUSDNguonVon, value);
        }

        private double? _fGiaTriVNDNguonVon;
        public double? FGiaTriVNDNguonVon
        {
            get => _fGiaTriVNDNguonVon;
            set => SetProperty(ref _fGiaTriVNDNguonVon, value);
        }

        private double? _fGiaTriEURNguonVon;
        public double? FGiaTriEURNguonVon
        {
            get => _fGiaTriEURNguonVon;
            set => SetProperty(ref _fGiaTriEURNguonVon, value);
        }

        private Visibility _buttonHangMuc;
        public Visibility ButtonHangMuc
        {
            get => _buttonHangMuc;
            set => SetProperty(ref _buttonHangMuc, value);
        }

        private double? _fGiaTriNgoaiTeKhacChiPhi;
        public double? FGiaTriNgoaiTeKhacChiPhi
        {
            get => _fGiaTriNgoaiTeKhacChiPhi;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacChiPhi, value);
        }

        private double? _fGiaTriUSDChiPhi;
        public double? FGiaTriUSDChiPhi
        {
            get => _fGiaTriUSDChiPhi;
            set => SetProperty(ref _fGiaTriUSDChiPhi, value);
        }

        private double? _fGiaTriVNDChiPhi;
        public double? FGiaTriVNDChiPhi
        {
            get => _fGiaTriVNDChiPhi;
            set => SetProperty(ref _fGiaTriVNDChiPhi, value);
        }

        private double? _fGiaTriEURChiPhi;
        public double? FGiaTriEURChiPhi
        {
            get => _fGiaTriEURChiPhi;
            set => SetProperty(ref _fGiaTriEURChiPhi, value);
        }

        private double? _fGiaTriNgoaiTeKhacNguonVonConLai;
        public double? FGiaTriNgoaiTeKhacNguonVonConLai
        {
            get => _fGiaTriNgoaiTeKhacNguonVonConLai;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacNguonVonConLai, value);
        }

        private double? _fGiaTriUSDNguonVonConLai;
        public double? FGiaTriUSDNguonVonConLai
        {
            get => _fGiaTriUSDNguonVonConLai;
            set => SetProperty(ref _fGiaTriUSDNguonVonConLai, value);
        }

        private double? _fGiaTriVNDNguonVonConLai;
        public double? FGiaTriVNDNguonVonConLai
        {
            get => _fGiaTriVNDNguonVonConLai;
            set => SetProperty(ref _fGiaTriVNDNguonVonConLai, value);
        }

        private double? _fGiaTriEURNguonVonConLai;
        public double? FGiaTriEURNguonVonConLai
        {
            get => _fGiaTriEURNguonVonConLai;
            set => SetProperty(ref _fGiaTriEURNguonVonConLai, value);
        }

        private double? _giaGoiThauUSD;
        public double? GiaGoiThauUSD
        {
            get => _giaGoiThauUSD;
            set => SetProperty(ref _giaGoiThauUSD, value);
        }

        private double? _giaGoiThauVND;
        public double? GiaGoiThauVND
        {
            get => _giaGoiThauVND;
            set => SetProperty(ref _giaGoiThauVND, value);
        }

        private double? _giaGoiThauEUR;
        public double? GiaGoiThauEUR
        {
            get => _giaGoiThauEUR;
            set => SetProperty(ref _giaGoiThauEUR, value);
        }

        private double? _giaGoiThauNgoaiTeKhac;
        public double? GiaGoiThauNgoaiTeKhac
        {
            get => _giaGoiThauNgoaiTeKhac;
            set => SetProperty(ref _giaGoiThauNgoaiTeKhac, value);
        }

        private string _soQuyetDinhGoiThau;
        public string SoQuyetDinhGoiThau
        {
            get => _soQuyetDinhGoiThau;
            set => SetProperty(ref _soQuyetDinhGoiThau, value);
        }

        private string _tenGoiThau;
        public string TenGoiThau
        {
            get => _tenGoiThau;
            set => SetProperty(ref _tenGoiThau, value);
        }

        private ObservableCollection<NhDmTiGiaModel> _itemsTiGia;
        public ObservableCollection<NhDmTiGiaModel> ItemsTiGia
        {
            get => _itemsTiGia;
            set => SetProperty(ref _itemsTiGia, value);
        }

        private NhDmTiGiaModel _selectedTiGia;
        public NhDmTiGiaModel SelectedTiGia
        {
            get => _selectedTiGia;
            set
            {
                if (SetProperty(ref _selectedTiGia, value))
                {
                    LoadTiGiaChiTiet();
                }
            }
        }

        private ObservableCollection<NhDmTiGiaChiTietModel> _itemsTiGiaChiTiet;
        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTiet
        {
            get => _itemsTiGiaChiTiet;
            set => SetProperty(ref _itemsTiGiaChiTiet, value);
        }

        private NhDmTiGiaChiTietModel _selectedTiGiaChiTiet;
        public NhDmTiGiaChiTietModel SelectedTiGiaChiTiet
        {
            get => _selectedTiGiaChiTiet;
            set => SetProperty(ref _selectedTiGiaChiTiet, value);
        }

        public ContractorPackageDialogViewModel(
            IMapper mapper,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            ISessionService sessionService,
            ILog logger,
            INsDonViService nsDonViService,
            INhKhTongTheService nhKhTongTheService,
            INhHdnkCacQuyetDinhService nhHdnkCacQuyetDinhService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService,
            INhDmHinhThucChonNhaThauService nhDmHinhThucChonNhaThauService,
            INhDmPhuongThucChonNhaThauService nhDmPhuongThucChonNhaThauService,
            INhDmLoaiHopDongService nhDmLoaiHopDongService,
            INhHdnkCacQuyetDinhNguonVonService nhHdnkCacQuyetDinhNguonVonService,
            INhHdnkCacQuyetDinhChiPhiService nhHdnkCacQuyetDinhChiPhiService,
            DmTiGiaDialogViewModel dmTiGiaDialogViewModel,
            ContractorPackageHangMucDetailDialogViewModel contractorPackageHangMucDetailDialogViewModel,
            INhDaGoiThauService nhDaGoiThauService,
            INhDaGoiThauChiPhiService nhDaGoiThauChiPhiService,
            INhDaGoiThauHangMucSerrvice nhDaGoiThauHangMucSerrvice,
            INhDaGoiThauNguonVonService nhDaGoiThauNguonVonService,
            INhHdnkCacQuyetDinhChiPhiHangMucService nhHdnkCacQuyetDinhChiPhiHangMucService) : base(mapper, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nhKhTongTheService = nhKhTongTheService;
            _nhHdnkCacQuyetDinhService = nhHdnkCacQuyetDinhService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;
            _nhDmHinhThucChonNhaThauService = nhDmHinhThucChonNhaThauService;
            _nhDmPhuongThucChonNhaThauService = nhDmPhuongThucChonNhaThauService;
            _nhDmLoaiHopDongService = nhDmLoaiHopDongService;
            _nhHdnkCacQuyetDinhNguonVonService = nhHdnkCacQuyetDinhNguonVonService;
            _nhHdnkCacQuyetDinhChiPhiService = nhHdnkCacQuyetDinhChiPhiService;
            _nhDaGoiThauChiPhiService = nhDaGoiThauChiPhiService;
            _nhDaGoiThauHangMucSerrvice = nhDaGoiThauHangMucSerrvice;
            _nhDaGoiThauService = nhDaGoiThauService;
            _nhDaGoiThauNguonVonService = nhDaGoiThauNguonVonService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
            _nhHdnkCacQuyetDinhChiPhiHangMucService = nhHdnkCacQuyetDinhChiPhiHangMucService;
            DmTiGiaDialogViewModel = dmTiGiaDialogViewModel;
            ContractorPackageHangMucDetailDialogViewModel = contractorPackageHangMucDetailDialogViewModel;

            PhuLucHangMucCommand = new RelayCommand(obj => OnPhuLucHangMuc());
        }

        public override void Init()
        {
            base.Init();
            LoadAttach();
            LoadLoaiKeHoachTongThe();
            LoadDonVi();
            LoadLoaiSoCu();
            LoadSoQuyetDinhSoCu();
            LoadHinhThucChonNhaThau();
            LoadPhuongThucChonNhaThau();
            LoadLoaiHopDong();
            LoadThongTinNGuonVon();
            LoadThongTinChiPhi();
            LoadTiGia();
            LoadData();
        }

        private void LoadLoaiKeHoachTongThe()
        {
            ComboboxItem theoGiaiDoan = new ComboboxItem("Theo giai đoạn", "0");
            ComboboxItem theoNam = new ComboboxItem("Theo năm", "1");
            LoaiKeHoachTongThe = new List<ComboboxItem>() { theoGiaiDoan, theoNam };
        }

        private void LoadSoKhTongThe()
        {
            ItemsSoKHTongThe = new ObservableCollection<NhKhTongTheModel>();
            var predicate = PredicateBuilder.True<NhKhTongThe>();
            predicate = predicate.And(x => x.BIsActive);
            if (SelectedKeHoachTongThe != null)
            {
                if (SelectedKeHoachTongThe.ValueItem.Equals("0"))
                {
                    predicate = predicate.And(x => x.IIdParentId == null);
                }
                else
                {
                    predicate = predicate.And(x => x.IIdParentId != null);
                }
            }
            var data = _nhKhTongTheService.FindAll(predicate);
            _itemsSoKhTongThe = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data);
            OnPropertyChanged(nameof(ItemsSoKHTongThe));
        }

        private void LoadDonVi()
        {
            var dataDonVi = _nsDonViService.FindAll().Where(x => x.NamLamViec == _sessionService.Current.YearOfWork).OrderBy(x => x.IIDMaDonVi);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(dataDonVi);
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadLoaiSoCu()
        {
            ComboboxItem ketQuaDamPhan = new ComboboxItem("Quyết định phê duyệt kết quả đàm phán ", "1");
            ComboboxItem chiTrongNuoc = new ComboboxItem("Quyết định phê duyệt chi trong nước", "2");
            ItemsLoaiSoCu = new List<ComboboxItem>() { ketQuaDamPhan, chiTrongNuoc };
        }

        private void LoadTenNhiemVuChi()
        {
            ItemsTenNHiemVuChi = new List<ComboboxItem>();
            if (SelectedSoKhTongThe != null && SelectedDonVi != null)
            {
                List<NhKhTongTheNhiemVuChiQuery> listKhTongTheNhiemVuChi = _nhKhTongTheNhiemVuChiService.FindByIdKhTongTheAndIdDonVi(SelectedSoKhTongThe.Id, SelectedDonVi.Id).ToList();
                if (listKhTongTheNhiemVuChi == null) return;
                var drpItem = listKhTongTheNhiemVuChi.Select(x => new ComboboxItem()
                {
                    ValueItem = x.Id.ToString(),
                    DisplayItem = (x.SMaNhiemVuChi + " - " + x.STenNhiemVuChi),
                    HiddenValue = x.IIdNhiemVuChiId.ToString(),
                    DisplayItemOption2 = x.IIdNhiemVuChiId.ToString()
                });
                _itemsTenNhiemVuChi = _mapper.Map<ObservableCollection<ComboboxItem>>(drpItem).ToList();
                OnPropertyChanged(nameof(ItemsTenNHiemVuChi));
            }
        }

        private void LoadSoQuyetDinhSoCu()
        {
            ItemsSoQuyetDinhSoCu = new List<ComboboxItem>();
            if (SelectedLoaiSoCu != null && SelectedTenNhiemVuChi != null)
            {
                List<NhCacQuyetDinhNhiemVuChiQuery> listData = _nhHdnkCacQuyetDinhService.FindByNhiemVuChi(Guid.Parse(SelectedTenNhiemVuChi.DisplayItemOption2), int.Parse(SelectedLoaiSoCu.ValueItem), SelectedSoKhTongThe.Id).ToList();
                if (listData == null) return;
                var dataItems = listData.Select(x => new ComboboxItem()
                {
                    ValueItem = x.IdCacQuyetDinh.ToString(),
                    DisplayItem = x.SSoQuyetDinh,
                    HiddenValue = x.STenNhiemVuChi
                });
                _itemsSoQuyetDinhSoCu = _mapper.Map<ObservableCollection<ComboboxItem>>(dataItems).ToList();
                OnPropertyChanged(nameof(ItemsSoQuyetDinhSoCu));
            }
        }

        private void LoadHinhThucChonNhaThau()
        {
            var data = _nhDmHinhThucChonNhaThauService.FindAll();
            _itemsHinhThucChonNhathau = _mapper.Map<ObservableCollection<NhDmHinhThucChonNhaThauModel>>(data);
            OnPropertyChanged(nameof(ItemsHinhThucChonNhaThau));
        }

        private void LoadPhuongThucChonNhaThau()
        {
            var data = _nhDmPhuongThucChonNhaThauService.FindAll();
            _itemsPhuongThucChoNhaThau = _mapper.Map<ObservableCollection<NhDmPhuongThucChonNhaThauModel>>(data);
            OnPropertyChanged(nameof(ItemsPhuongThucChonNhaThau));
        }
        private void LoadLoaiHopDong()
        {
            var data = _nhDmLoaiHopDongService.FindAll();
            _itemsLoaiHopDong = _mapper.Map<ObservableCollection<NhDmLoaiHopDongModel>>(data);
            OnPropertyChanged(nameof(ItemsLoaiHopDong));
        }

        private void LoadThongTinNGuonVon()
        {
            ItemsThongTinNguonVon = new ObservableCollection<NhHdnkCacQuyetDinhNguonVonModel>();
            FGiaTriUSDNguonVon = 0;
            FGiaTriVNDNguonVon = 0;
            FGiaTriEURNguonVon = 0;
            FGiaTriNgoaiTeKhacNguonVon = 0;
            FGiaTriUSDNguonVonConLai = 0;
            FGiaTriVNDNguonVonConLai = 0;
            FGiaTriEURNguonVonConLai = 0;
            FGiaTriNgoaiTeKhacNguonVonConLai = 0;
            if (SoQuyetDinhSoCuSelected != null)
            {
                var data = _nhHdnkCacQuyetDinhNguonVonService.FindByThongTinNguonVon(Guid.Parse(SoQuyetDinhSoCuSelected.ValueItem));
                _itemsThongTinNguonVon = _mapper.Map<ObservableCollection<NhHdnkCacQuyetDinhNguonVonModel>>(data);
                foreach (var item in _itemsThongTinNguonVon)
                {
                    item.PropertyChanged += ThongTinNguonvon_PropertyChanged;
                }
                OnPropertyChanged(nameof(ItemsThongTinNguonVon));
            }
            else
            {
                ItemsThongTinNguonVon = null;
            }
        }

        private void LoadThongTinChiPhi()
        {
            ItemsThongTinChiPhi = new ObservableCollection<NhHdnkCacQuyetDinhChiPhiModel>();
            FGiaTriUSDChiPhi = 0;
            FGiaTriVNDChiPhi = 0;
            FGiaTriEURChiPhi = 0;
            FGiaTriNgoaiTeKhacChiPhi = 0;
            if (SoQuyetDinhSoCuSelected != null)
            {
                var data = _nhHdnkCacQuyetDinhChiPhiService.FindByIdQuyetDinhGoiThau(Guid.Parse(SoQuyetDinhSoCuSelected.ValueItem)).OrderBy(x => x.SMaOrder);
                _itemsThongTinChiPhi = _mapper.Map<ObservableCollection<NhHdnkCacQuyetDinhChiPhiModel>>(data);
                foreach (var item in _itemsThongTinChiPhi)
                {
                    if (item.IIdParentId.IsNullOrEmpty()) item.IsHangCha = true;
                    var listHangMuc = _nhHdnkCacQuyetDinhChiPhiHangMucService.FindByIdQuyetDinhChiPhi(item.Id);
                    if (listHangMuc != null && listHangMuc.Count() > 0) item.IsEditHangMuc = true;
                    item.PropertyChanged += ThongTinChiPhi_PropertyChanged;
                }
                OnPropertyChanged(nameof(ItemsThongTinChiPhi));
            }
            else
            {
                ItemsThongTinNguonVon = null;
            }
        }

        private void LoadTiGia()
        {
            var data = _nhDmTiGiaService.FindAll();
            _itemsTiGia = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(data);
            OnPropertyChanged(nameof(ItemsTiGia));
        }

        private void LoadTiGiaChiTiet()
        {
            _itemsTiGiaChiTiet = new ObservableCollection<NhDmTiGiaChiTietModel>();
            if (SelectedTiGia != null)
            {
                IEnumerable<NhDmTiGiaChiTiet> listTiGiaChiTiet = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
                _itemsTiGiaChiTiet = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(listTiGiaChiTiet);
            }
            OnPropertyChanged(nameof(ItemsTiGiaChiTiet));
        }

        private void LoadGoiThauNguonVon()
        {
            ItemsThongTinNguonVon = new ObservableCollection<NhHdnkCacQuyetDinhNguonVonModel>();
            if (!Model.Id.IsNullOrEmpty())
            {
                var listNguonVon = _nhDaGoiThauNguonVonService.FindCacQuyetDinhNguonVonByIdGoiThau(Model.Id);
                _itemsThongTinNguonVon = _mapper.Map<ObservableCollection<NhHdnkCacQuyetDinhNguonVonModel>>(listNguonVon);
                foreach (var item in _itemsThongTinNguonVon)
                {
                    item.PropertyChanged += ThongTinNguonvon_PropertyChanged;
                    item.IsSelected = true;
                }
                OnPropertyChanged(nameof(ItemsThongTinNguonVon));
                TinhTongNguonVon(true);
            }
        }

        private void LoadGoiThauChiPhi()
        {
            ItemsThongTinChiPhi = new ObservableCollection<NhHdnkCacQuyetDinhChiPhiModel>();
            if (!Model.Id.IsNullOrEmpty())
            {
                var listChiPhi = _nhDaGoiThauChiPhiService.FindByCacQuyetDinhChiPhiByGoiThauId(Model.Id).OrderBy(x => x.SMaOrder);
                _itemsThongTinChiPhi = _mapper.Map<ObservableCollection<NhHdnkCacQuyetDinhChiPhiModel>>(listChiPhi);
                foreach (var item in _itemsThongTinChiPhi)
                {
                    if (item.IIdParentId.IsNullOrEmpty()) item.IsHangCha = true;
                    var listHangMuc = _nhDaGoiThauHangMucSerrvice.FindByChiPhi(item.Id);
                    if (listHangMuc != null && listHangMuc.Count() > 0) item.IsEditHangMuc = true;
                    item.PropertyChanged += ThongTinChiPhi_PropertyChanged;
                    item.IsSelected = true;
                }
                OnPropertyChanged(nameof(ItemsThongTinChiPhi));
                TinhTongTien(true);
            }
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                if (GoiThauDieuChinhId.IsNullOrEmpty())
                {
                    Description = "Thêm mới gói thầu";
                    Title = "THÊM MỚI GÓI THẦU";
                }
                else
                {
                    Description = "Điều chỉnh gói thầu";
                    Title = "ĐIỀU CHỈNH GÓI THẦU";
                }
                GiaGoiThauUSD = 0;
                GiaGoiThauVND = 0;
                GiaGoiThauEUR = 0;
                GiaGoiThauNgoaiTeKhac = 0;
            }
            else
            {
                NhDaGoiThau entity = _nhDaGoiThauService.FindById(Model.Id);
                Model = _mapper.Map<NhDaGoiThauModel>(entity);
                if (IsDetail)
                {
                    Description = "Chi tiết gói thầu";
                    Title = "CHI TIẾT GÓI THẦU";
                }
                else
                {
                    Description = "Cập nhập gói thầu";
                    Title = "CẬP NHẬP GÓI THẦU";
                }
                LoadDetailData();
            }

            LoadGoiThauNguonVon();
            LoadGoiThauChiPhi();

            OnPropertyChanged(nameof(SelectedLoaiHopDong));
            OnPropertyChanged(nameof(SelectedPhuongThucChonNhaThau));
            OnPropertyChanged(nameof(SelectedHinhThucChonNhaThau));
            OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
            OnPropertyChanged(nameof(SelectedTenNhiemVuChi));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedKeHoachTongThe));
            OnPropertyChanged(nameof(SelectedSoKhTongThe));
            OnPropertyChanged(nameof(SelectedLoaiSoCu));
            OnPropertyChanged(nameof(SoQuyetDinhSoCuSelected));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
        }

        // Load chi tiết gói thầu
        private void LoadDetailData()
        {
            var thongTinPheDuyet = _nhDaGoiThauService.FindGoiThauDetail();
            var dataItemsCacQuyetDinh = thongTinPheDuyet.Select(x => new ComboboxItem()
            {
                ValueItem = x.IdCacQuyetDinh.ToString(),
                DisplayItem = x.SoQuyetDinh,
                HiddenValue = x.STenNhiemVuchi,
                DisplayItemOption2 = x.IIdDonViThuHuongId.ToString()
            });

            var dataItemsTenNhiemVuChi = thongTinPheDuyet.Select(x => new ComboboxItem()
            {
                ValueItem = x.IIdKeHoachTongTheNHiemVuChiId.ToString(),
                DisplayItem = (x.SMaNhiemVuChi + " - " + x.STenNhiemVuchi),
                HiddenValue = x.IIdNhiemVuChiId.ToString(),
                DisplayItemOption2 = x.IIdGoiThauId.ToString(),

            });
            ItemsTenNHiemVuChi = _mapper.Map<ObservableCollection<ComboboxItem>>(dataItemsTenNhiemVuChi).ToList();
            ItemsSoQuyetDinhSoCu = _mapper.Map<ObservableCollection<ComboboxItem>>(dataItemsCacQuyetDinh).ToList();

            _selectedSoQuyetDinhSoCu = ItemsSoQuyetDinhSoCu.FirstOrDefault(x => Guid.Parse(x.ValueItem) == Model.IIdCacQuyetDinhId);
            _selectedPhuongThucChonNhaThau = ItemsPhuongThucChonNhaThau.FirstOrDefault(x => x.Id == Model.IIdPhuongThucDauThauId);
            _selectedHinhThucChonNhaThau = ItemsHinhThucChonNhaThau.FirstOrDefault(x => x.Id == Model.IIdHinhThucChonNhaThauId);
            _selectedTenNHiemVuChi = ItemsTenNHiemVuChi.FirstOrDefault(x => Guid.Parse(x.DisplayItemOption2) == Model.Id);
            if (_selectedSoQuyetDinhSoCu != null)
            {
                _selectedDonVi = ItemsDonVi.FirstOrDefault(x => x.Id == Guid.Parse(_selectedSoQuyetDinhSoCu.DisplayItemOption2));
                if (thongTinPheDuyet.FirstOrDefault(x => x.IIdGoiThauId == Model.Id).ILoaiQuyetDinh == 1)
                {
                    _selectedLoaiSoCu = ItemsLoaiSoCu.FirstOrDefault(x => x.ValueItem == "1");
                }
                else
                {
                    _selectedLoaiSoCu = ItemsLoaiSoCu.FirstOrDefault(x => x.ValueItem == "2");
                }
            }
            _selectedSoKhTongThe = ItemsSoKHTongThe.FirstOrDefault(x => x.Id == thongTinPheDuyet.FirstOrDefault(y => y.IIdGoiThauId == Model.Id).IIdkeHoachTongTheId);
            if (_selectedSoKhTongThe.INamKeHoach != null)
            {
                _selectedKeHoachTongThe = LoaiKeHoachTongThe.FirstOrDefault(x => x.ValueItem == "1");
            }
            else
            {
                _selectedKeHoachTongThe = LoaiKeHoachTongThe.FirstOrDefault(x => x.ValueItem == "0");
            }

            // load tỉ giá
            _selectedTiGia = ItemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
            LoadTiGiaChiTiet();
            _selectedTiGiaChiTiet = ItemsTiGiaChiTiet.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
            _selectedLoaiHopDong = ItemsLoaiHopDong.FirstOrDefault(x => x.IIdLoaiHopDongId == Model.IIdLoaiHopDongId);
            GiaGoiThauUSD = Model.FGiaGoiThauUsd;
            GiaGoiThauVND = Model.FGiaGoiThauVnd;
            GiaGoiThauEUR = Model.FGiaGoiThauEur;
            GiaGoiThauNgoaiTeKhac = Model.FGiaGoiThauNgoaiTeKhac;
        }

        private static void SelectAll(bool select, IEnumerable<ModelBase> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }

        private void ThongTinNguonvon_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            NhHdnkCacQuyetDinhNguonVonModel item = (NhHdnkCacQuyetDinhNguonVonModel)sender;
            if (args.PropertyName == nameof(NhHdnkCacQuyetDinhNguonVonModel.IsSelected))
            {
                UpDateStatusThongTinNguonvon(item);
            }
            else if (item.IsSelected && (args.PropertyName == nameof(NhHdnkCacQuyetDinhNguonVonModel.FGiaTriUSDGoiThau) ||
                args.PropertyName == nameof(NhHdnkCacQuyetDinhNguonVonModel.FGiaTriVNDGoiThau) ||
                args.PropertyName == nameof(NhHdnkCacQuyetDinhNguonVonModel.FGiaTriEURGoiThau)))
            {
                if (SelectedTiGia != null)
                {
                    var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                    string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                    string sourceCurrency;
                    string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                    double value;
                    switch (args.PropertyName)
                    {
                        case nameof(NhHdnkCacQuyetDinhNguonVonModel.FGiaTriVNDGoiThau):
                            sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                            value = item.FGiaTriVNDGoiThau;
                            break;
                        case nameof(NhHdnkCacQuyetDinhNguonVonModel.FGiaTriEURGoiThau):
                            sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                            value = item.FGiaTriEURGoiThau;
                            break;
                        case nameof(NhHdnkCacQuyetDinhNguonVonModel.FGiaTriNgoaiTeKhacGoiThau):
                            sourceCurrency = LoaiTienTeEnum.TypeCode.NGOAI_TE_KHAC;
                            value = item.FGiaTriNgoaiTeKhacGoiThau;
                            break;
                        default:
                            sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                            value = item.FGiaTriUSDGoiThau;
                            break;
                    }
                    item.FGiaTriVNDGoiThau = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    item.FGiaTriEURGoiThau = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    item.FGiaTriUSDGoiThau = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    item.FGiaTriNgoaiTeKhacGoiThau = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                }
                CalculateGiaTriThongTinNguonVon(item);
            }
            item.IsModified = true;
        }

        private void CalculateGiaTriThongTinNguonVon(NhHdnkCacQuyetDinhNguonVonModel item)
        {
            item.FGiaTriUSDConLai = item.FGiaTriUsd.Value - item.FGiaTriUSDGoiThau;
            item.FGiaTriVNDConLai = item.FGiaTriVnd.Value - item.FGiaTriVNDGoiThau;
            item.FGiaTriNgoaiTeKhacConLai = item.FGiaTriNgoaiTeKhac.Value - item.FGiaTriNgoaiTeKhacGoiThau;
            item.FGiaTriEURConLai = item.FGiaTriEur.Value - item.FGiaTriEURGoiThau;
            TinhTongNguonVon(false);
        }

        private void UpDateStatusThongTinNguonvon(NhHdnkCacQuyetDinhNguonVonModel item)
        {
            var nguonVon = ItemsThongTinNguonVon.FirstOrDefault(x => x.Id == item.Id);
            if (nguonVon == null)
            {
                return;
            }
            if (!item.IsSelected)
            {
                item.FGiaTriVNDGoiThau = 0;
                item.FGiaTriUSDGoiThau = 0;
                item.FGiaTriEURGoiThau = 0;
                item.FGiaTriNgoaiTeKhacGoiThau = 0;
                item.FGiaTriUSDConLai = item.FGiaTriUsd.Value;
                item.FGiaTriVNDConLai = item.FGiaTriVnd.Value;
                item.FGiaTriEURConLai = item.FGiaTriEur.Value;
                item.FGiaTriNgoaiTeKhacConLai = item.FGiaTriNgoaiTeKhacGoiThau;
            }
            TinhTongNguonVon(false);
        }

        private void ThongTinChiPhi_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            NhHdnkCacQuyetDinhChiPhiModel item = (NhHdnkCacQuyetDinhChiPhiModel)sender;
            if (args.PropertyName == nameof(NhHdnkCacQuyetDinhChiPhiModel.IsSelected))
            {
                UpDateStatusThongTinChiPhi(item);
            }
            if (args.PropertyName == nameof(NhHdnkCacQuyetDinhChiPhiModel.FGiaTriUSDGoiThau) ||
               args.PropertyName == nameof(NhHdnkCacQuyetDinhChiPhiModel.FGiaTriVNDGoiThau) ||
               args.PropertyName == nameof(NhHdnkCacQuyetDinhChiPhiModel.FGiaTriEURGoiThau) ||
               args.PropertyName == nameof(NhHdnkCacQuyetDinhChiPhiModel.FGiaTriNgoaiTeKhac))
            {
                if (SelectedTiGia != null)
                {
                    var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                    string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                    string sourceCurrency;
                    string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                    double value;
                    switch (args.PropertyName)
                    {
                        case nameof(NhHdnkCacQuyetDinhChiPhiModel.FGiaTriVNDGoiThau):
                            sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                            value = item.FGiaTriVNDGoiThau;
                            break;
                        case nameof(NhHdnkCacQuyetDinhChiPhiModel.FGiaTriEURGoiThau):
                            sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                            value = item.FGiaTriEURGoiThau;
                            break;
                        case nameof(NhHdnkCacQuyetDinhChiPhiModel.FGiaTriNgoaiTeKhacGoiThau):
                            sourceCurrency = LoaiTienTeEnum.TypeCode.NGOAI_TE_KHAC;
                            value = item.FGiaTriNgoaiTeKhacGoiThau;
                            break;
                        default:
                            sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                            value = item.FGiaTriUSDGoiThau;
                            break;
                    }
                    item.FGiaTriVNDGoiThau = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    item.FGiaTriEURGoiThau = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    item.FGiaTriUSDGoiThau = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    item.FGiaTriNgoaiTeKhacGoiThau = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                }
                CalculateGiaTriThongTinChiPhi(item);
            }
            item.IsModified = true;
        }

        private void UpDateStatusThongTinChiPhi(NhHdnkCacQuyetDinhChiPhiModel item)
        {
            var chiPhi = ItemsThongTinChiPhi.FirstOrDefault(x => x.Id == item.Id);
            if (chiPhi == null)
            {
                return;
            }
            if (chiPhi.IIdParentId == null)
            {
                ItemsThongTinChiPhi.Select(x =>
                {
                    if (x.IIdParentId == item.Id)
                    {
                        x.IsSelected = item.IsSelected;
                    }
                    return x;
                }).ToList();
            }
            if (!item.IsSelected)
            {
                item.FGiaTriVNDGoiThau = 0;
                item.FGiaTriUSDGoiThau = 0;
                item.FGiaTriNgoaiTeKhacGoiThau = 0;
                item.FGiaTriEURGoiThau = 0;
                item.FGiaTriUSDConLai = item.FGiaTriUsd.Value;
                item.FGiaTriVNDConLai = item.FGiaTriVnd.Value;
                item.FGiaTriEURConLai = item.FGiaTriEur.Value;
                item.FGiaTriNgoaiTeKhacConLai = item.FGiaTriNgoaiTeKhac.Value;
                _buttonHangMuc = Visibility.Hidden;
            }
            TinhTongTien(false);
        }

        private void CalculateGiaTriThongTinChiPhi(NhHdnkCacQuyetDinhChiPhiModel item)
        {
            item.FGiaTriUSDConLai = item.FGiaTriUsd.Value - item.FGiaTriUSDGoiThau;
            item.FGiaTriVNDConLai = item.FGiaTriVnd.Value - item.FGiaTriVNDGoiThau;
            item.FGiaTriNgoaiTeKhacConLai = item.FGiaTriNgoaiTeKhac.Value - item.FGiaTriNgoaiTeKhacGoiThau;
            item.FGiaTriEURConLai = item.FGiaTriEur.Value - item.FGiaTriEURGoiThau;
            TinhTongTien(false);
        }

        private void OnPhuLucHangMuc()
        {
            //ListHangMucSave = new List<NhDaGoiThauHangMucModel>();
            NhHdnkCacQuyetDinhChiPhiModel chiPhiModel = new NhHdnkCacQuyetDinhChiPhiModel();
            chiPhiModel = SelectedThongTinChiPhi;
            ContractorPackageHangMucDetailDialogViewModel.Model = chiPhiModel;
            if (Model.Id.IsNullOrEmpty())
            {
                ContractorPackageHangMucDetailDialogViewModel.IsDetail = false;
            }
            else
            {
                ContractorPackageHangMucDetailDialogViewModel.IsDetail = true;
            }
            ContractorPackageHangMucDetailDialogViewModel.ListHangMucLoad = ListHangMucSave.Where(x => x.IIdGoiThauChiPhiId == SelectedThongTinChiPhi.IIdChiPhiId).ToList();
            ContractorPackageHangMucDetailDialogViewModel.ViewState = FormViewState.ADD;
            ContractorPackageHangMucDetailDialogViewModel.CurrencyExchangeAction = (obj, propName) => GoiThauHangMucCurrencyExChange(obj, propName);
            ContractorPackageHangMucDetailDialogViewModel.Init();
            ContractorPackageHangMucDetailDialogViewModel.SavedAction = obj =>
            {
                // xóa những hạng mục đã chọn ứng với chi phí
                ListHangMucSave.RemoveAll(x => x.IIdGoiThauChiPhiId == SelectedThongTinChiPhi.IIdChiPhiId);
                this.ListHangMuc = ContractorPackageHangMucDetailDialogViewModel.ListHangMuc;
                OnPropertyChanged(nameof(ListHangMuc));
                SelectedThongTinChiPhi.FGiaTriUSDGoiThau = (double)ListHangMuc.Sum(x => x.FTienGoiThauUsd);
                SelectedThongTinChiPhi.FGiaTriVNDGoiThau = (double)ListHangMuc.Sum(x => x.FTienGoiThauVnd);
                SelectedThongTinChiPhi.FGiaTriNgoaiTeKhacGoiThau = (double)ListHangMuc.Sum(x => x.FTienGoiThauNgoaiTeKhac);
                SelectedThongTinChiPhi.FGiaTriEURGoiThau = (double)ListHangMuc.Sum(x => x.FTienGoiThauEur);
                ListHangMucSave.AddRange(ListHangMuc);
                TinhTongTien(false);
            };
            ContractorPackageHangMucDetailDialogViewModel.ShowDialog();
        }

        private void GoiThauHangMucCurrencyExChange(object sender, string propName)
        {
            if (SelectedTiGia != null)
            {
                NhHdnkCacQuyetDinhChiPhiHangMucModel objectSender = (NhHdnkCacQuyetDinhChiPhiHangMucModel)sender;
                var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                string sourceCurrency;
                string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                double value;
                switch (propName)
                {
                    case nameof(NhHdnkCacQuyetDinhChiPhiHangMucModel.FGiaTriVNDGoiThau):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                        value = objectSender.FGiaTriVNDGoiThau.Value;
                        break;
                    case nameof(NhHdnkCacQuyetDinhChiPhiHangMucModel.FGiaTriEURGoiThau):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                        value = objectSender.FGiaTriEURGoiThau.Value;
                        break;
                    case nameof(NhHdnkCacQuyetDinhChiPhiHangMucModel.FGiaTriNgoaiTeKhacGoiThau):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.NGOAI_TE_KHAC;
                        value = objectSender.FGiaTriNgoaiTeKhacGoiThau.Value;
                        break;
                    default:
                        sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                        value = objectSender.FGiaTriUSDGoiThau.Value;
                        break;
                }
                objectSender.FGiaTriVNDGoiThau = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                objectSender.FGiaTriEURGoiThau = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                objectSender.FGiaTriUSDGoiThau = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                objectSender.FGiaTriNgoaiTeKhacGoiThau = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
            }
        }

        private void TinhTongTien(bool detail)
        {
            var listData = new List<NhHdnkCacQuyetDinhChiPhiModel>();
            if (detail)
            {
                listData = ItemsThongTinChiPhi.ToList();
            }
            else
            {
                listData = ItemsThongTinChiPhi.Where(x => x.IsSelected).ToList();
            }
            FGiaTriNgoaiTeKhacChiPhi = listData.Sum(x => x.FGiaTriNgoaiTeKhacGoiThau);
            FGiaTriUSDChiPhi = listData.Sum(x => x.FGiaTriUSDGoiThau);
            FGiaTriVNDChiPhi = listData.Sum(x => x.FGiaTriVNDGoiThau);
            FGiaTriEURChiPhi = listData.Sum(x => x.FGiaTriEURGoiThau);

            FGiaTriUSDNguonVonConLai = FGiaTriUSDNguonVon - FGiaTriUSDChiPhi;
            FGiaTriVNDNguonVonConLai = FGiaTriVNDNguonVon - FGiaTriVNDChiPhi;
            FGiaTriNgoaiTeKhacNguonVonConLai = FGiaTriNgoaiTeKhacNguonVon - FGiaTriNgoaiTeKhacChiPhi;
            FGiaTriEURNguonVonConLai = FGiaTriEURNguonVon - FGiaTriEURChiPhi;

            if (FGiaTriUSDNguonVonConLai == 0 && FGiaTriVNDNguonVonConLai == 0
                && FGiaTriNgoaiTeKhacNguonVonConLai == 0 && FGiaTriEURNguonVonConLai == 0)
            {
                GiaGoiThauUSD = FGiaTriUSDNguonVon;
                GiaGoiThauVND = FGiaTriVNDNguonVon;
                GiaGoiThauEUR = FGiaTriEURNguonVon;
                GiaGoiThauNgoaiTeKhac = FGiaTriNgoaiTeKhacNguonVon;
            }
            else
            {
                GiaGoiThauUSD = 0;
                GiaGoiThauVND = 0;
                GiaGoiThauEUR = 0;
                GiaGoiThauNgoaiTeKhac = 0;
            }
        }

        private void TinhTongNguonVon(bool detail)
        {
            var listData = new List<NhHdnkCacQuyetDinhNguonVonModel>();
            if (!detail)
            {
                listData = ItemsThongTinNguonVon.Where(x => x.IsSelected).ToList();
            }
            else
            {
                listData = ItemsThongTinNguonVon.ToList();
            }
            FGiaTriUSDNguonVon = listData.Sum(x => x.FGiaTriUSDGoiThau);
            FGiaTriVNDNguonVon = listData.Sum(x => x.FGiaTriVNDGoiThau);
            FGiaTriNgoaiTeKhacNguonVon = listData.Sum(x => x.FGiaTriNgoaiTeKhacGoiThau);
            FGiaTriEURNguonVon = listData.Sum(x => x.FGiaTriEURGoiThau);

            FGiaTriUSDNguonVonConLai = FGiaTriUSDNguonVon - FGiaTriUSDChiPhi;
            FGiaTriVNDNguonVonConLai = FGiaTriVNDNguonVon - FGiaTriVNDChiPhi;
            FGiaTriNgoaiTeKhacNguonVonConLai = FGiaTriNgoaiTeKhacNguonVon - FGiaTriNgoaiTeKhacChiPhi;
            FGiaTriEURNguonVonConLai = FGiaTriEURNguonVon - FGiaTriEURChiPhi;

            if (FGiaTriUSDNguonVonConLai == 0 && FGiaTriVNDNguonVonConLai == 0
                && FGiaTriNgoaiTeKhacNguonVonConLai == 0 && FGiaTriEURNguonVonConLai == 0)
            {
                GiaGoiThauUSD = FGiaTriUSDNguonVon;
                GiaGoiThauVND = FGiaTriVNDNguonVon;
                GiaGoiThauEUR = FGiaTriEURNguonVon;
                GiaGoiThauNgoaiTeKhac = FGiaTriNgoaiTeKhacNguonVon;
            }
            else
            {
                GiaGoiThauUSD = 0;
                GiaGoiThauVND = 0;
                GiaGoiThauEUR = 0;
                GiaGoiThauNgoaiTeKhac = 0;
            }
        }

        public override void OnSave(Object obj)
        {
            if (!CheckValidate())
            {
                return;
            }
            ConverData();
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                NhDaGoiThau entity;
                if (Model.Id.IsNullOrEmpty())
                {
                    entity = _mapper.Map<NhDaGoiThau>(Model);
                    entity.DNgayTao = DateTime.Now;
                    entity.BIsKhoa = false;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.BActive = true;
                    entity.Id = Guid.NewGuid();
                    entity.ILoai = 1;
                    if (GoiThauDieuChinhId.IsNullOrEmpty())
                    {
                        entity.BIsGoc = true;
                        entity.ILanDieuChinh = 0;
                    }
                    else
                    {
                        // điều chỉnh
                        var goiThauGoc = _nhDaGoiThauService.FindById(GoiThauDieuChinhId);
                        goiThauGoc.BActive = false;
                        _nhDaGoiThauService.Update(goiThauGoc);

                        entity.ILanDieuChinh = goiThauGoc.ILanDieuChinh.Value + 1;
                        entity.IIdParentAdjustId = goiThauGoc.Id;
                        entity.BIsGoc = false;
                        entity.IIdParentId = !goiThauGoc.IIdParentId.IsNullOrEmpty() ? goiThauGoc.IIdParentId : goiThauGoc.Id;
                    }
                    //entity.IIdParentId = entity.Id;
                    List<NhDaGoiThauNguonVonModel> listGoiThauNguonVon = new List<NhDaGoiThauNguonVonModel>();
                    foreach (var nguonVon in ItemsThongTinNguonVon)
                    {
                        if (nguonVon.IsSelected)
                        {
                            NhDaGoiThauNguonVonModel nguonVonModel = new NhDaGoiThauNguonVonModel();
                            nguonVonModel.IIdGoiThauId = entity.Id;
                            nguonVonModel.IIdCacQuyetDinhNguonVonId = nguonVon.Id;
                            nguonVonModel.IIdNguonVonId = nguonVon.IIdNguonVonId;
                            nguonVonModel.FTienGoiThauUsd = nguonVon.FGiaTriUSDGoiThau;
                            nguonVonModel.FTienGoiThauVnd = nguonVon.FGiaTriVNDGoiThau;
                            nguonVonModel.FTienGoiThauEur = nguonVon.FGiaTriEURGoiThau;
                            nguonVonModel.FTienGoiThauNgoaiTeKhac = nguonVon.FGiaTriNgoaiTeKhacGoiThau;
                            listGoiThauNguonVon.Add(nguonVonModel);
                        }
                    }

                    List<NhDaGoiThauChiPhiModel> listGoiThauChiPhi = new List<NhDaGoiThauChiPhiModel>();
                    List<NhDaGoiThauHangMucModel> listGoiThauHangMuc = new List<NhDaGoiThauHangMucModel>();
                    foreach (var chiPhi in ItemsThongTinChiPhi)
                    {
                        if (chiPhi.IsSelected)
                        {
                            NhDaGoiThauChiPhiModel chiPhiModel = new NhDaGoiThauChiPhiModel();
                            chiPhiModel.Id = Guid.NewGuid();
                            chiPhiModel.IIdGoiThauId = entity.Id;
                            chiPhiModel.IIdCacQuyetDinhChiPhiId = chiPhi.Id;
                            chiPhiModel.FTienGoiThauUsd = chiPhi.FGiaTriUSDGoiThau;
                            chiPhiModel.FTienGoiThauVnd = chiPhi.FGiaTriVNDGoiThau;
                            chiPhiModel.FTienGoiThauEur = chiPhi.FGiaTriEURGoiThau;
                            chiPhiModel.FTienGoiThauNgoaiTeKhac = chiPhi.FGiaTriNgoaiTeKhacGoiThau;
                            listGoiThauHangMuc = ListHangMucSave.Where(x => x.IIdGoiThauChiPhiId == chiPhi.IIdChiPhiId).ToList();
                            listGoiThauHangMuc.Select(x =>
                            {
                                x.IIdGoiThauChiPhiId = chiPhiModel.Id;
                                return x;
                            }).ToList();
                            listGoiThauChiPhi.Add(chiPhiModel);
                        }
                    }
                    _nhDaGoiThauService.Add(entity);
                    var listGoiThauNguonVonEntity = _mapper.Map<ObservableCollection<NhDaGoiThauNguonVon>>(listGoiThauNguonVon).ToList();
                    var listGoiThauChiPhiEntity = _mapper.Map<ObservableCollection<NhDaGoiThauChiPhi>>(listGoiThauChiPhi).ToList();
                    var listGoiThauHangMucEntity = _mapper.Map<ObservableCollection<NhDaGoiThauHangMuc>>(listGoiThauHangMuc).ToList();
                    _nhDaGoiThauNguonVonService.AddRange(listGoiThauNguonVonEntity);
                    _nhDaGoiThauChiPhiService.AddRange(listGoiThauChiPhiEntity);
                    _nhDaGoiThauHangMucSerrvice.AddRange(listGoiThauHangMucEntity);
                }
                else
                {
                    entity = _nhDaGoiThauService.FindById(Model.Id);
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    _mapper.Map(Model, entity);
                    List<NhDaGoiThauNguonVonModel> listGoiThauNguonVon = new List<NhDaGoiThauNguonVonModel>();
                    foreach (var nguonVon in ItemsThongTinNguonVon)
                    {
                        if (nguonVon.IsSelected)
                        {
                            NhDaGoiThauNguonVonModel nguonVonModel = new NhDaGoiThauNguonVonModel();
                            nguonVonModel.Id = nguonVon.Id;
                            nguonVonModel.IIdCacQuyetDinhNguonVonId = nguonVon.IIdCacQuyetDinhNguonVonId;
                            nguonVonModel.IIdDuToanNguonVonId = nguonVon.IIdDuToanNguonVonId;
                            nguonVonModel.IIdQddauTuNguonVonId = nguonVon.IIdQdDauTuNguonVonId;
                            nguonVonModel.IIdGoiThauId = entity.Id;
                            nguonVonModel.IIdNguonVonId = nguonVon.IIdNguonVonId;
                            nguonVonModel.FTienGoiThauUsd = nguonVon.FGiaTriUSDGoiThau;
                            nguonVonModel.FTienGoiThauVnd = nguonVon.FGiaTriVNDGoiThau;
                            nguonVonModel.FTienGoiThauEur = nguonVon.FGiaTriEURGoiThau;
                            nguonVonModel.FTienGoiThauNgoaiTeKhac = nguonVon.FGiaTriNgoaiTeKhacGoiThau;
                            listGoiThauNguonVon.Add(nguonVonModel);
                        }
                    }
                    List<NhDaGoiThauHangMucModel> listGoiThauHangMuc = new List<NhDaGoiThauHangMucModel>();
                    List<NhDaGoiThauChiPhiModel> listGoiThauChiPhi = new List<NhDaGoiThauChiPhiModel>();
                    foreach (var chiPhi in ItemsThongTinChiPhi)
                    {

                        if (chiPhi.IsSelected)
                        {
                            NhDaGoiThauChiPhiModel chiPhiModel = new NhDaGoiThauChiPhiModel();
                            chiPhiModel.Id = chiPhi.Id;
                            chiPhiModel.IIdGoiThauId = entity.Id;
                            chiPhiModel.IIdCacQuyetDinhChiPhiId = chiPhi.IIdCacQuyetDinhChiPhiId;
                            chiPhiModel.IIdQdDauTuChiPhiId = chiPhi.IIdQdDauTuChiPhiId;
                            chiPhiModel.IIdDuToanChiPhiId = chiPhi.IIdDuToanChiPhiId;
                            chiPhiModel.FTienGoiThauUsd = chiPhi.FGiaTriUSDGoiThau;
                            chiPhiModel.FTienGoiThauVnd = chiPhi.FGiaTriVNDGoiThau;
                            chiPhiModel.FTienGoiThauEur = chiPhi.FGiaTriEURGoiThau;
                            chiPhiModel.FTienGoiThauNgoaiTeKhac = chiPhi.FGiaTriNgoaiTeKhacGoiThau;
                            var ListHangMucChiPhi = ListHangMucSave.Where(x => x.IIdGoiThauChiPhiId == chiPhi.IIdChiPhiId).ToList();
                            ListHangMucChiPhi.Select(x =>
                            {
                                x.IIdGoiThauChiPhiId = chiPhiModel.Id;
                                return x;
                            }).ToList();
                            listGoiThauChiPhi.Add(chiPhiModel);
                        }
                    }
                    _nhDaGoiThauService.Update(entity);
                    var listGoiThauNguonVonEntity = _mapper.Map<ObservableCollection<NhDaGoiThauNguonVon>>(listGoiThauNguonVon).ToList();
                    var listGoiThauChiPhiEntity = _mapper.Map<ObservableCollection<NhDaGoiThauChiPhi>>(listGoiThauChiPhi).ToList();
                    var listGoiThauHangMucEntity = _mapper.Map<ObservableCollection<NhDaGoiThauHangMuc>>(listGoiThauHangMuc).ToList();
                    _nhDaGoiThauNguonVonService.UpdateRange(listGoiThauNguonVonEntity);
                    _nhDaGoiThauChiPhiService.UpdateRange(listGoiThauChiPhiEntity);
                    _nhDaGoiThauHangMucSerrvice.UpdateRange(listGoiThauHangMucEntity);
                }

                SaveAttachment(entity.Id);
                e.Result = entity;
            }, (s, e) =>
            {
                IsLoading = false;
                if (e.Error == null)
                {
                    Model = _mapper.Map<NhDaGoiThauModel>(e.Result);
                    SavedAction?.Invoke(Model);
                    this.IsDetail = true;
                    LoadData();

                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            });
        }

        private void ConverData()
        {
            if (SelectedPhuongThucChonNhaThau != null)
            {
                Model.IIdPhuongThucDauThauId = SelectedPhuongThucChonNhaThau.Id;
            }
            if (SelectedHinhThucChonNhaThau != null)
            {
                Model.IIdHinhThucChonNhaThauId = SelectedHinhThucChonNhaThau.Id;
            }
            if (SelectedLoaiHopDong != null)
            {
                Model.IIdLoaiHopDongId = SelectedLoaiHopDong.IIdLoaiHopDongId;
            }
            if (SelectedTiGia != null)
            {
                Model.IIdTiGiaId = SelectedTiGia.Id;
            }
            if (SelectedTiGiaChiTiet != null)
            {
                Model.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
            }
            if (SoQuyetDinhSoCuSelected != null)
            {
                Model.IIdCacQuyetDinhId = Guid.Parse(SoQuyetDinhSoCuSelected.ValueItem);
            }
            Model.FGiaGoiThauUsd = GiaGoiThauUSD;
            Model.FGiaGoiThauVnd = GiaGoiThauVND;
            Model.FGiaGoiThauEur = GiaGoiThauEUR;
            Model.FGiaGoiThauNgoaiTeKhac = GiaGoiThauNgoaiTeKhac;
        }

        private bool CheckValidate()
        {
            List<string> lstError = new List<string>();
            if (SelectedDonVi == null)
            {
                lstError.Add(Resources.MsgCheckDonVi);
            }
            if (string.IsNullOrEmpty(Model.STenGoiThau))
            {
                lstError.Add(Resources.MsgCheckTenDuAn);
            }
            if (SelectedKeHoachTongThe == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckKeHoachTongThe));
            }
            if (SelectedSoKhTongThe == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckSoKHTongThe));
            }
            if (SelectedTenNhiemVuChi == null)
            {
                lstError.Add(string.Format(Resources.MsgNhiemVuChi));
            }
            if (SelectedLoaiSoCu == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckLoaiSoCu));
            }
            if (SoQuyetDinhSoCuSelected == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckQDSoCu));
            }
            if (SelectedLoaiHopDong == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckHopDong));
            }
            if (SelectedPhuongThucChonNhaThau == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckPhuongThucChonNhaThau));
            }
            if (SelectedHinhThucChonNhaThau == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckHinhThucChonNhaThau));
            }
            if (lstError.Count() > 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return false;
            }
            return true;
        }

        public override void OnClose(object obj)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}