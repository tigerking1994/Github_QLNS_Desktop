using AutoMapper;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNNHHopDongTrongNuoc
{
    public class MSTNNHHopDongTrongNuocDialogViewModel : DialogAttachmentViewModelBase<NhDaHopDongModel>
    {
        private readonly ISessionService _sessionService;
        private readonly ILogger<MSTNNHHopDongTrongNuocDialogViewModel> _logger;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDaGoiThauService _nhDaGoiThauService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INhDmLoaiHopDongService _nhDmLoaiHopDongService;
        private readonly INhDmNhaThauService _nhDmNhaThauService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        private readonly INhDaGoiThauHangMucSerrvice _nhDaGoiThauHangMucSerrvice;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly INhDaHopDongGoiThauNhaThauService _nhDaHopDongGoiThauNhaThauService;
        private readonly INhDaHopDongChiPhiService _nhDaHopDongChiPhiService;
        private readonly INhDaHopDongHangMucService _nhDaHopDongHangMucService;
        private readonly INhDaGoiThauChiPhiService _nhDaGoiThauChiPhiService;
        private readonly INhDmLoaiTienTeService _nhDmLoaiTienTeService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;

        public override Type ContentType => typeof(View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNNHHopDongTrongNuoc.NHHopDongTrongNuocDialog);

        public bool IsDetail { get; set; }
        public bool IsAdd { get; set; }
        public bool IsHieuChinhImport { get; set; } = false;
        public Guid HopDongDieuChinhId { get; set; }
        private int currentRow = -1;
        private bool _isCount;
        public bool IsCount
        {
            get => _isCount;
            set
            {
                SetProperty(ref _isCount, value);
                //LoadChuongTrinh();
            }
        }
        public int ILoaiMenu { get; set; }
        public int ILoai { get; set; }
        public int IThuocMenu { get; set; }
        public bool IsShowDuAn { get; set; }
        public bool IsSaveData => ItemsGoiThau.Any(x => x.IsModified || x.IsDeleted);

        public RelayCommand AddGoiThauCommand { get; }
        public RelayCommand DeleteGoiThauCommand { get; }
        public RelayCommand ShowDetailChiPhiCommand { get; }

        public MSTNNHHopDongTrongNuocChiTietHangMucViewModel NHHopDongTrongNuocChiTietHangMucViewModel { get; }

        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.NH_DA_HOPDONG;

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
                LoadChuongTrinh();
                LoadDuAn();
            }
        }

        private ObservableCollection<NhDmNhiemVuChiModel> _itemsChuongTrinh;
        public ObservableCollection<NhDmNhiemVuChiModel> ItemsChuongTrinh
        {
            get => _itemsChuongTrinh;
            set => SetProperty(ref _itemsChuongTrinh, value);
        }

        private NhDmNhiemVuChiModel _selectedChuongTrinh;
        public NhDmNhiemVuChiModel SelectedChuongTrinh
        {
            get => _selectedChuongTrinh;
            set
            {
                SetProperty(ref _selectedChuongTrinh, value);
                if (value != null)
                {
                    LoadGoiThau();
                }
            }
        }
        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        private ObservableCollection<NhDaDuAnModel> _itemsDuAn;
        public ObservableCollection<NhDaDuAnModel> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }

        private NhDaDuAnModel _selectedDuAn;
        public NhDaDuAnModel SelectedDuAn
        {
            get => _selectedDuAn;
            set
            {
                SetProperty(ref _selectedDuAn, value);
                LoadGoiThau();
            }
        }

        private ObservableCollection<NhDmLoaiHopDongModel> _itemsLoaiHopDong;
        public ObservableCollection<NhDmLoaiHopDongModel> ItemsLoaiHopDong
        {
            get => _itemsLoaiHopDong;
            set => SetProperty(ref _itemsLoaiHopDong, value);
        }

        private bool _isVisibleTiGiaNhap;
        public bool IsVisibleTiGiaNhap
        {
            get => _isVisibleTiGiaNhap;
            set => SetProperty(ref _isVisibleTiGiaNhap, value);
        }
        private double? _fTiGiaNhap;
        public double? FTiGiaNhap
        {
            get => _fTiGiaNhap;
            set
            {
                if (SetProperty(ref _fTiGiaNhap, value))
                {
                    if (ItemsGoiThau != null)
                    {
                        CalculateHangMuc();
                    }
                }
            }
        }
        private NhDmLoaiHopDongModel _selectedLoaiHopDong;
        public NhDmLoaiHopDongModel SelectedLoaiHopDong
        {
            get => _selectedLoaiHopDong;
            set => SetProperty(ref _selectedLoaiHopDong, value);
        }

        private ObservableCollection<NhDmNhaThauModel> _itemsNhaThau;
        public ObservableCollection<NhDmNhaThauModel> ItemsNhaThau
        {
            get => _itemsNhaThau;
            set => SetProperty(ref _itemsNhaThau, value);
        }

        private NhDmNhaThauModel _selectedNhaThau;
        public NhDmNhaThauModel SelectedNhaThau
        {
            get => _selectedNhaThau;
            set => SetProperty(ref _selectedNhaThau, value);
        }

        private ObservableCollection<NhDaHopDongGoiThauNhaThauModel> _itemsGoiThau;
        public ObservableCollection<NhDaHopDongGoiThauNhaThauModel> ItemsGoiThau
        {
            get => _itemsGoiThau;
            set => SetProperty(ref _itemsGoiThau, value);
        }

        private NhDaHopDongGoiThauNhaThauModel _selectedGoiThau;
        public NhDaHopDongGoiThauNhaThauModel SelectedGoiThau
        {
            get => _selectedGoiThau;
            set => SetProperty(ref _selectedGoiThau, value);
        }

        private ObservableCollection<ComboboxItem> _itemsNhaThauGoiThau;
        public ObservableCollection<ComboboxItem> ItemsNhaThauGoiThau
        {
            get => _itemsNhaThauGoiThau;
            set => SetProperty(ref _itemsNhaThauGoiThau, value);
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
                SetProperty(ref _selectedTiGia, value);
                if (value != null)
                {
                    LoadTiGiaChiTiet();
                    //SetLabelTiGiaChiTiet();
                    IsVisibleTiGiaNhap = true;
                    ShowTiGiaNhap();
                }
            }
        }
        private ObservableCollection<NhDaHopDongTrongNuocHangMucGoiThauModel> _itemsHangMuc;
        public ObservableCollection<NhDaHopDongTrongNuocHangMucGoiThauModel> ItemsHangMuc
        {
            get => _itemsHangMuc;
            set => SetProperty(ref _itemsHangMuc, value);
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

        private List<NhDaHopDongTrongNuocChiPhiGoiThauModel> _listChiPhi;
        public List<NhDaHopDongTrongNuocChiPhiGoiThauModel> ListChiPhi
        {
            get => _listChiPhi;
            set => SetProperty(ref _listChiPhi, value);
        }
        private bool _selectAllGoiThauFilter;
        public bool SelectAllGoiThauFilter
        {
            get => (ItemsGoiThau == null || !ItemsGoiThau.Any()) ? false : ItemsGoiThau.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllGoiThauFilter, value);
                if (ItemsGoiThau != null)
                {
                    ItemsGoiThau.Select(c => { c.IsChecked = _selectAllGoiThauFilter; return c; }).ToList();
                }
            }
        }

        private List<NhDaHopDongTrongNuocHangMucGoiThauModel> _listHangMuc;
        public List<NhDaHopDongTrongNuocHangMucGoiThauModel> ListHangMuc
        {
            get => _listHangMuc;
            set => SetProperty(ref _listHangMuc, value);
        }

        private double? _fGiaHopDongUsd;
        public double? FGiaHopDongUsd
        {
            get => _fGiaHopDongUsd;
            set => SetProperty(ref _fGiaHopDongUsd, value);
        }

        private double? _fGiaHopDongVnd;
        public double? FGiaHopDongVnd
        {
            get => _fGiaHopDongVnd;
            set => SetProperty(ref _fGiaHopDongVnd, value);
        }
        public List<NhDaGoiThauHangMuc> listBackUpHangMuc;

        private double? _fGiaHopDongEur;
        public double? FGiaHopDongEur
        {
            get => _fGiaHopDongEur;
            set => SetProperty(ref _fGiaHopDongEur, value);
        }

        private double? _fGiaHopDongNgoaiTeKhac;
        public double? FGiaHopDongNgoaiTeKhac
        {
            get => _fGiaHopDongNgoaiTeKhac;
            set => SetProperty(ref _fGiaHopDongNgoaiTeKhac, value);
        }

        private List<ComboboxItem> _itemsLoaiTienTe;
        public List<ComboboxItem> ItemsLoaiTienTe
        {
            get => _itemsLoaiTienTe;
            set => SetProperty(ref _itemsLoaiTienTe, value);
        }

        private ComboboxItem _selectedLoaiTienTe;
        public ComboboxItem SelectedLoaiTienTe
        {
            get => _selectedLoaiTienTe;
            set => SetProperty(ref _selectedLoaiTienTe, value);
        }
        private List<NhDaHopDongTrongNuocChiPhiGoiThauModel> ListChiPhiSave = new List<NhDaHopDongTrongNuocChiPhiGoiThauModel>();
        private List<NhDaHopDongTrongNuocHangMucGoiThauModel> ListHangMucSave = new List<NhDaHopDongTrongNuocHangMucGoiThauModel>();

        public MSTNNHHopDongTrongNuocDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILogger<MSTNNHHopDongTrongNuocDialogViewModel> logger,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INsDonViService nsDonViService,
            INhDaGoiThauService nhDaGoiThauService,
            INhDaDuAnService nhDaDuAnService,
            INhDmLoaiHopDongService nhDmLoaiHopDongService,
            INhDmNhaThauService nhDmNhaThauService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            MSTNNHHopDongTrongNuocChiTietHangMucViewModel nHHopDongTrongNuocChiTietHangMucViewModel,
            INhDaHopDongService nhDaHopDongService,
            INhDaHopDongGoiThauNhaThauService nhDaHopDongGoiThauNhaThauService,
            INhDaHopDongChiPhiService nhDaHopDongChiPhiService,
            INhDaGoiThauHangMucSerrvice nhDaGoiThauHangMucSerrvice,
            INhDaHopDongHangMucService nhDaHopDongHangMucService,
            INhDaGoiThauChiPhiService nhDaGoiThauChiPhiService,
            INhDmLoaiTienTeService nhDmLoaiTienTeService) : base(mapper, storageServiceFactory, attachService)
        {
            _sessionService = sessionService;
            _logger = logger;
            _nsDonViService = nsDonViService;
            _nhDaGoiThauService = nhDaGoiThauService;
            _nhDaDuAnService = nhDaDuAnService;
            _nhDmLoaiHopDongService = nhDmLoaiHopDongService;
            _nhDmNhaThauService = nhDmNhaThauService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
            _nhDaHopDongService = nhDaHopDongService;
            _nhDaHopDongGoiThauNhaThauService = nhDaHopDongGoiThauNhaThauService;
            _nhDaHopDongChiPhiService = nhDaHopDongChiPhiService;
            _nhDaHopDongHangMucService = nhDaHopDongHangMucService;
            _nhDaGoiThauChiPhiService = nhDaGoiThauChiPhiService;
            _nhDaGoiThauHangMucSerrvice = nhDaGoiThauHangMucSerrvice;
            _nhDmLoaiTienTeService = nhDmLoaiTienTeService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            NHHopDongTrongNuocChiTietHangMucViewModel = nHHopDongTrongNuocChiTietHangMucViewModel;

            AddGoiThauCommand = new RelayCommand(obj => OnAddGoiThau(), obj => !IsDetail);
            ShowDetailChiPhiCommand = new RelayCommand(obj => OnShowDetailChiPhi());
            DeleteGoiThauCommand = new RelayCommand(obj => OnDeleteGoiThau(), obj => !IsDetail);
        }

        public override void Init()
        {

            base.Init();
            LoadAttach();
            LoadDonVi();
            LoadDuAn();
            LoadLoaiHopDong();
            LoadNhaThau();
            LoadNhaThauGoiThau();
            //LoadThanhToanBang();
            LoadTiGia();
            LoadData();
            //LoadGoiThau();

        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindByCondition(x => x.NamLamViec == _sessionService.Current.YearOfWork).OrderBy(x => x.IIDMaDonVi);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadDuAn()
        {
            if (ILoaiMenu == 1) //Thuộc menu mua sắm
            {
                if (SelectedDonVi != null)
                {
                    var data = _nhDaDuAnService.FindAll(x => x.ILoai == 1 && x.IIdDonViQuanLyId == SelectedDonVi.Id);
                    _itemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(data);
                    OnPropertyChanged(nameof(ItemsDuAn));
                }
            }
            else if (ILoaiMenu == 2) //Thuộc menu dự án
            {
                if (SelectedDonVi != null)
                {
                    var data = _nhDaDuAnService.FindAll(x => x.ILoai == 2 && x.IIdDonViQuanLyId == SelectedDonVi.Id);
                    _itemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(data);
                    OnPropertyChanged(nameof(ItemsDuAn));
                }
            }
            else
            {
                if (SelectedDonVi != null)
                {
                    var data = _nhDaDuAnService.FindAll(x => x.IIdDonViQuanLyId == SelectedDonVi.Id);
                    _itemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(data);
                    OnPropertyChanged(nameof(ItemsDuAn));
                }
            }
        }

        private void LoadLoaiHopDong()
        {
            var data = _nhDmLoaiHopDongService.FindAll();
            _itemsLoaiHopDong = _mapper.Map<ObservableCollection<NhDmLoaiHopDongModel>>(data);
            OnPropertyChanged(nameof(ItemsLoaiHopDong));
        }

        private void LoadNhaThau()
        {
            var data = _nhDmNhaThauService.FindAll();
            _itemsNhaThau = _mapper.Map<ObservableCollection<NhDmNhaThauModel>>(data);
            OnPropertyChanged(nameof(ItemsNhaThau));
        }

        private void LoadGoiThau()
        {
            ItemsGoiThau = new ObservableCollection<NhDaHopDongGoiThauNhaThauModel>();
            var listRemove = new List<NhDaHopDongGoiThauNhaThauModel>();
            if (IsShowDuAn && SelectedDuAn != null)
            {
                if (IThuocMenu == 5)
                {
                    IThuocMenu = 4;
                }
                var data = _nhDaGoiThauService.GetAllGoiThauTrongNuoc(ILoai, IThuocMenu).Where(x => x.IIdDuAnId == SelectedDuAn.Id).ToList();
                var dataHopDongs = _nhDaHopDongGoiThauNhaThauService.FindByIdHopDong(Model.Id);
                _itemsGoiThau = _mapper.Map<ObservableCollection<NhDaHopDongGoiThauNhaThauModel>>(data);
                foreach (var item in _itemsGoiThau)
                {
                    var dataGoiThauHMCP = _nhDaGoiThauHangMucSerrvice.FindGoiThauChiPhiHangMucByGoiThauId(item.IIdGoiThauId.Value);
                    if (dataHopDongs.Select(x => x.IIdGoiThauId).Contains(item.IIdGoiThauId))
                    {
                        item.IsChecked = true;
                        item.IsDisable = dataGoiThauHMCP.IsEmpty();
                    }
                    else
                    {
                        item.IsAdded = true;
                        if (dataGoiThauHMCP.Count() == dataGoiThauHMCP.Where(x => !x.IIdHopDongChiPhiId.IsNullOrEmpty()).Count())
                        {
                            listRemove.Add(item);
                        }
                        else
                        {
                            item.IsDisable = true;
                            item.PropertyChanged += DetailModel_PropertyChanged;
                        }
                    }
                    item.IIdHopDongId = Model.Id;
                }
                foreach (var item in listRemove)
                {
                    _itemsGoiThau.Remove(item);
                }
                IsCount = (ItemsGoiThau.Count() != 0);
            }
            else if (!IsShowDuAn && SelectedChuongTrinh != null)
            {
                // theo spec IThuocMenu của hợp đồng lớn hơn của gói thầu 1 đơn vị
                var data = _nhDaGoiThauService.GetAllGoiThauTrongNuoc(ILoai, IThuocMenu - 1).ToList();
                data = data.Where(x => x.IIdKHTTNhiemVuChiId == SelectedChuongTrinh.IIdKHTTNhiemVuChiId).ToList();
                var dataHopDongs = _nhDaHopDongGoiThauNhaThauService.FindByIdHopDong(Model.Id);
                _itemsGoiThau = _mapper.Map<ObservableCollection<NhDaHopDongGoiThauNhaThauModel>>(data);
                foreach (var item in _itemsGoiThau)
                {
                    var dataGoiThauHMCP = _nhDaGoiThauHangMucSerrvice.FindGoiThauChiPhiHangMucByGoiThauId(item.IIdGoiThauId.Value);
                    if (dataHopDongs.Select(x => x.IIdGoiThauId).Contains(item.IIdGoiThauId))
                    {
                        item.IsChecked = true;
                        item.IsDisable = dataGoiThauHMCP.IsEmpty();
                    }
                    else
                    {
                        item.IsAdded = true;
                        if (dataGoiThauHMCP.Count() == dataGoiThauHMCP.Where(x => !x.IIdHopDongChiPhiId.IsNullOrEmpty()).Count())
                        {
                            listRemove.Add(item);
                        }
                        else
                        {
                            item.IsDisable = true;
                            item.PropertyChanged += DetailModel_PropertyChanged;
                        }
                    }
                    item.IIdHopDongId = Model.Id;
                }
                foreach (var item in listRemove)
                {
                    _itemsGoiThau.Remove(item);
                }
                IsCount = (ItemsGoiThau.Count() != 0);
            }
            OnPropertyChanged(nameof(ItemsGoiThau));
        }

        private void LoadNhaThauGoiThau()
        {
            _itemsNhaThauGoiThau = _mapper.Map<ObservableCollection<ComboboxItem>>(ItemsNhaThau);
            OnPropertyChanged(nameof(ItemsNhaThauGoiThau));
        }

        private void LoadTiGia()
        {
            var data = _nhDmTiGiaService.FindAll();
            _itemsTiGia = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(data);
            OnPropertyChanged(nameof(ItemsTiGia));
        }

        private void LoadChuongTrinh()
        {
            ItemsChuongTrinh = new ObservableCollection<NhDmNhiemVuChiModel>();
            if (SelectedDonVi != null)
            {
                var data = _nhDmNhiemVuChiService.FindByDonViId(SelectedDonVi.Id);
                _itemsChuongTrinh = _mapper.Map<ObservableCollection<NhDmNhiemVuChiModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsChuongTrinh));
        }

        private void LoadTiGiaChiTiet()
        {
            _itemsTiGiaChiTiet = new ObservableCollection<NhDmTiGiaChiTietModel>();
            if (SelectedTiGia != null)
            {
                var data = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
                _itemsTiGiaChiTiet = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsTiGiaChiTiet));
        }

        private void LoadThanhToanBang()
        {
            List<string> maTienTe = new List<string>() { "USD", "VND", "EUR" };
            var data = _nhDmLoaiTienTeService.FindAll();
            List<NhDmLoaiTienTe> dataTienTe = new List<NhDmLoaiTienTe>();
            foreach (var item in maTienTe)
            {
                dataTienTe.Add(data.FirstOrDefault(x => x.SMaTienTe.ToUpper() == item));
            }
            _itemsLoaiTienTe = data.Select(x => new ComboboxItem() { ValueItem = x.Id.ToString(), DisplayItem = x.SMaTienTe }).ToList();
            OnPropertyChanged(nameof(ItemsLoaiTienTe));
        }

        public override void LoadData(params object[] args)
        {
            SelectedGoiThau = null;
            if (IsAdd)
            {
                if (HopDongDieuChinhId.IsNullOrEmpty())
                {
                    IconKind = PackIconKind.PlaylistPlus;
                    Description = "Thêm mới hợp đồng";
                    Title = "THÊM MỚI HỢP ĐÔNG";
                    if (IsHieuChinhImport)
                    {
                        LoadHopDongImportHieuChinh();
                        return;
                    }
                    IsVisibleTiGiaNhap = false;
                }
            }
            else
            {
                NhDaHopDong entity = _nhDaHopDongService.FindById(Model.Id);
                Model = _mapper.Map<NhDaHopDongModel>(entity);
                if (IsDetail)
                {
                    IconKind = PackIconKind.Details;
                    Description = "Chi tiết hợp đồng";
                    Title = "CHI TIẾT HỢP ĐÔNG";
                }
                else if (IsDieuChinh)
                {
                    IconKind = PackIconKind.Adjust;
                    Description = "Điều chỉnh hợp đồng";
                    Title = "ĐIỀU CHỈNH HỢP ĐÔNG";
                }
                else
                {
                    IconKind = PackIconKind.NoteEditOutline;
                    Description = "Cập nhập hợp đồng";
                    Title = "CẬP NHẬP HỢP ĐÔNG";
                }
                LoadDetailData();
            }
            LoadHopDongGoiThauNhaThau();
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedNhaThau));
            OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedLoaiHopDong));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
        }

        private void LoadHopDongImportHieuChinh()
        {
            LoadHopDongGoiThauNhaThau();
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedNhaThau));
            OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedLoaiHopDong));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
        }

        private void LoadDetailData()
        {
            _selectedDonVi = ItemsDonVi.FirstOrDefault(x => x.Id == Model.IIdDonViQuanLyId);
            if (SelectedDonVi != null)
            {
                LoadChuongTrinh();
                if (!IsShowDuAn) _selectedChuongTrinh = ItemsChuongTrinh.FirstOrDefault(x => x.Id == Model.IIdKhTongTheNhiemVuChiId);
            }
            LoadDuAn();
            _selectedDuAn = ItemsDuAn.FirstOrDefault(x => x.Id == Model.IIdDuAnId);
            LoadChuongTrinh();
            _selectedChuongTrinh = ItemsChuongTrinh.FirstOrDefault(x => x.IIdKHTTNhiemVuChiId == Model.IIdKhTongTheNhiemVuChiId);
            _selectedTiGia = ItemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
            if (SelectedTiGia != null)
            {
                IsVisibleTiGiaNhap = true;
            }
            FTiGiaNhap = Model.FTiGiaNhap;
            //LoadTiGiaChiTiet();
            //_selectedTiGiaChiTiet = ItemsTiGiaChiTiet.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
            _selectedLoaiHopDong = ItemsLoaiHopDong.FirstOrDefault(x => x.IIdLoaiHopDongId == Model.IIdLoaiHopDongId);
            _selectedNhaThau = ItemsNhaThau.FirstOrDefault(x => x.Id == Model.IIdNhaThauThucHienId);

            //LoadThanhToanBang();
            //FGiaHopDongUsd = Model.FGiaTriUsd;
            //FGiaHopDongVnd = Model.FGiaTriVnd;
            //FGiaHopDongEur = Model.FGiaTriEur;
            //FGiaHopDongNgoaiTeKhac = Model.FGiaTriNgoaiTeKhac;
        }

        private void LoadHopDongGoiThauNhaThau()
        {
            listBackUpHangMuc = new List<NhDaGoiThauHangMuc>();
            ItemsGoiThau = new ObservableCollection<NhDaHopDongGoiThauNhaThauModel>();
            var listRemove = new ObservableCollection<NhDaHopDongGoiThauNhaThauModel>();
            if (!IsAdd)
            {
                var data = _nhDaGoiThauService.GetAllGoiThauTrongNuoc(ILoai, IThuocMenu - 1).ToList();
                data = data.Where(x => x.IIdKHTTNhiemVuChiId == Model.IIdKhTongTheNhiemVuChiId).ToList();
                var dataHopDongs = _nhDaHopDongGoiThauNhaThauService.FindByIdHopDong(Model.Id);
                _itemsGoiThau = _mapper.Map<ObservableCollection<NhDaHopDongGoiThauNhaThauModel>>(data);
                foreach (var item in _itemsGoiThau)
                {
                    item.PropertyChanged += DetailModel_PropertyChanged;
                    var dataGoiThauHMCP = _nhDaGoiThauHangMucSerrvice.FindGoiThauChiPhiHangMucByGoiThauId(item.IIdGoiThauId.Value);
                    if (dataHopDongs.Select(x => x.IIdGoiThauId).Contains(item.IIdGoiThauId))
                    {
                        var goiThauNhauThau = dataHopDongs.FirstOrDefault(x => x.IIdGoiThauId.Equals(item.IIdGoiThauId));
                        item.Id = goiThauNhauThau.Id;
                        item.FGiaTriHopDong_Usd = goiThauNhauThau.FGiaTriHopDong_Usd;
                        item.FGiaTriHopDong_Vnd = goiThauNhauThau.FGiaTriHopDong_Vnd;
                        item.FGiaTriHopDong_Eur = goiThauNhauThau.FGiaTriHopDong_Eur;
                        item.FGiaTriHopDong_NgoaiTeKhac = goiThauNhauThau.FGiaTriHopDong_NgoaiTeKhac;
                        item.IsChecked = true;
                        item.IIdNhaThauId = goiThauNhauThau.IIdNhaThauId;
                        var listdata = _nhDaGoiThauHangMucSerrvice.FindByGoiThauId(item.IIdGoiThauId.Value);
                        listdata = listdata.Where(x => (x.IsCheck == 1 && x.IIDGoiThauCheck == item.Id) || x.IsCheck == 2).ToList();
                        foreach (var items in listdata)
                        {
                            NhDaGoiThauHangMuc itemsave = _nhDaGoiThauHangMucSerrvice.FindHangMucById(items.Id);
                            listBackUpHangMuc.Add(itemsave);
                        }
                        item.IsDisable = dataGoiThauHMCP.IsEmpty();
                    }
                    else
                    {

                        item.IsAdded = true;
                        if (!dataGoiThauHMCP.IsEmpty() && dataGoiThauHMCP.Count() == dataGoiThauHMCP.Where(x => !x.IIdHopDongChiPhiId.IsNullOrEmpty()).Count())
                        {
                            item.PropertyChanged -= DetailModel_PropertyChanged;
                            listRemove.Add(item);
                        }
                        item.IsDisable = true;

                    }
                    item.IIdHopDongId = Model.Id;
                }
                foreach (var item in listRemove)
                {
                    _itemsGoiThau.Remove(item);
                }
                OnPropertyChanged(nameof(ItemsGoiThau));
                //SumGiaTriHopDong();
            }
        }

        private void OnAddGoiThau()
        {
            if (SelectedGoiThau != null)
            {
                var data = _nhDaGoiThauHangMucSerrvice.FindByGoiThauId(SelectedGoiThau.IIdGoiThauId.Value).Where(x => x.IsCheck == 2 || x.IsCheck == 0).ToList();

                if (data.Count != 0)
                {
                    NhDaHopDongGoiThauNhaThauModel sourceItem = SelectedGoiThau;
                    NhDaHopDongGoiThauNhaThauModel targetItem = ObjectCopier.Clone(sourceItem);
                    targetItem.Id = Guid.NewGuid();
                    targetItem.IsAdded = true;
                    currentRow = ItemsGoiThau.IndexOf(SelectedGoiThau);
                    targetItem.PropertyChanged += DetailModel_PropertyChanged;
                    ItemsGoiThau.Insert(currentRow + 1, targetItem);
                }
                else
                {
                    MessageBoxHelper.Error(Resources.MSGoiThauKhongHangMuc);
                }
            }
        }

        private void OnDeleteGoiThau()
        {
            if (SelectedGoiThau != null && ItemsGoiThau.Count > 0)
            {
                SelectedGoiThau.IsDeleted = !SelectedGoiThau.IsDeleted;
                OnPropertyChanged(nameof(IsSaveData));
                //SumGiaTriHopDong();
            }
        }

        private void OnShowDetailChiPhi()
        {
            NHHopDongTrongNuocChiTietHangMucViewModel.Model = SelectedGoiThau;
            //if (Model.Id.IsNullOrEmpty())
            //{
            //    NHHopDongTrongNuocChiTietHangMucViewModel.IsEdit = false;
            //}
            //else
            //{
            //    NHHopDongTrongNuocChiTietHangMucViewModel.IsEdit = true;
            //}
            //NHHopDongTrongNuocChiTietHangMucViewModel.CurrencyExchangeActionHangMuc = (obj, propName) => ChiPhiHangMucCurrencyExChange(obj, propName);
            //NHHopDongTrongNuocChiTietHangMucViewModel.CurrencyExchangeActionChiPhi = (obj, propName) => ChiPhiCurrencyExChange(obj, propName);
            //NHHopDongTrongNuocChiTietHangMucViewModel.ListChiPhiLoad = ListChiPhiSave.Where(x => x.IIdGoiThauId == SelectedGoiThau.IIdGoiThauId).ToList();
            NHHopDongTrongNuocChiTietHangMucViewModel.Init();
            if (IsDetail == true)
            {
                NHHopDongTrongNuocChiTietHangMucViewModel.IsReadOnly = true;
            }
            else
            {
                NHHopDongTrongNuocChiTietHangMucViewModel.IsReadOnly = false;
            }
            NHHopDongTrongNuocChiTietHangMucViewModel.SavedAction = obj => SaveGoiThauDetail();
            //{
            //    // xóa những chi phí và hạng mục ứng với gói thầu
            //    var listChiPhiDelete = ListChiPhiSave.Where(x => x.IIdHopDongGoiThauNhaThauId == SelectedGoiThau.Id).ToList();
            //    foreach (var item in listChiPhiDelete)
            //    {
            //        ListHangMucSave.RemoveAll(x => x.IIdHopDongChiPhiId == item.Id);
            //    }
            //    ListChiPhiSave.RemoveAll(x => x.IIdHopDongGoiThauNhaThauId == SelectedGoiThau.Id);
            //    this.ListChiPhi = NHHopDongTrongNuocChiTietHangMucViewModel.ListChiPhi;
            //    this.ListHangMuc = NHHopDongTrongNuocChiTietHangMucViewModel.ListHangMuc;
            //    SelectedGoiThau.IsModified = true;
            //    OnPropertyChanged(nameof(ListChiPhi));
            //    OnPropertyChanged(nameof(ListHangMuc));
            //    ListChiPhiSave.AddRange(ListChiPhi);
            //    ListHangMucSave.AddRange(ListHangMuc);
            //    SumGiaTriHopDongGoiThau();
            //};
            NHHopDongTrongNuocChiTietHangMucViewModel.ShowDialog();
        }
        private void SaveGoiThauDetail()
        {
            //Code cũ
            if (NHHopDongTrongNuocChiTietHangMucViewModel != null)
            {
                var itemsHangMucCPDetail = NHHopDongTrongNuocChiTietHangMucViewModel.ItemsHangMuc;
                foreach (var item in itemsHangMucCPDetail)
                {
                    item.IIDGoiThauCheck = SelectedGoiThau.Id;
                    if (item.IsChecked == true)
                    {
                        item.IsCheck = 1;
                    }
                    else
                    {
                        item.IsCheck = 2;
                    }
                }
                foreach (var item in itemsHangMucCPDetail)
                {
                    NhDaGoiThauHangMuc itemsave = _nhDaGoiThauHangMucSerrvice.FindHangMucById(item.IIdGoiThauHangMucId ?? Guid.Empty);
                    if (itemsave == null) continue;
                    itemsave.IIDGoiThauCheck = item.IIDGoiThauCheck;
                    itemsave.IsCheck = item.IsCheck;
                    _nhDaGoiThauHangMucSerrvice.Update(itemsave);
                }
                //NHHopDongTrongNuocChiTietHangMucViewModel.ItemsHangMuc.Clear();

                //CRUD Hop dong hang muc, chi phi
                if (!itemsHangMucCPDetail.IsEmpty() && itemsHangMucCPDetail.Any(x => x.IsChecked))
                {
                    var predicate = PredicateBuilder.True<NhDaHopDongChiPhi>();
                    predicate = predicate.And(x => x.IIdHopDongGoiThauNhaThauId.Equals(itemsHangMucCPDetail.FirstOrDefault().IIdHopDongGoiThauNhaThauId));
                    var hdChiPhis = _nhDaHopDongChiPhiService.FindAll(predicate);
                    List<NhDaHopDongChiPhi> lstAdd = new List<NhDaHopDongChiPhi>();
                    List<NhDaHopDongChiPhi> lstDelete = new List<NhDaHopDongChiPhi>();
                    //_nhDaHopDongChiPhiService.DeleteChiphiHopDongTrongNuoc(itemsHangMucCPDetail.FirstOrDefault().IIdHopDongGoiThauNhaThauId);
                    var itemsHangMucCP = itemsHangMucCPDetail.Where(x => x.IsChecked && x.IIdHopDongChiPhiId.IsNullOrEmpty()).ToList();
                    itemsHangMucCP.ForEach(f =>
                    {
                        var idNew = Guid.NewGuid();
                        f.Id = idNew;
                        itemsHangMucCP.Select(x =>
                        {

                            if (!x.IIdParentId.IsNullOrEmpty() && x.IIdParentId.Equals(f.IIdGoiThauHangMucId))
                            {
                                x.IIdParentId = idNew;

                            }
                            return x;
                        }).ToList();
                    });

                    var ItemsChiPhi = itemsHangMucCP
                                    .GroupBy(g => new { g.IIdGoiThauChiPhiId, g.IIdChiPhiId })
                                    .Select(s => new NhDaHopDongChiPhi
                                    {
                                        Id = s.FirstOrDefault().Id,
                                        IIdHopDongId = Model.Id,
                                        IIdChiPhiId = s.Key.IIdChiPhiId,
                                        IIdParentId = s.FirstOrDefault().IIdParentChiPhiId,
                                        IIdGoiThauChiPhiId = s.Key.IIdGoiThauChiPhiId,
                                        IIdHopDongGoiThauNhaThauId = s.FirstOrDefault(x => !x.IIdHopDongGoiThauNhaThauId.IsNullOrEmpty()).IIdHopDongGoiThauNhaThauId,
                                        FGiaTriUsd = s.Sum(x => x.FTienGoiThauUSD),
                                        FGiaTriVnd = s.Sum(x => x.FTienGoiThauVND),
                                        FGiaTriEur = s.Sum(x => x.FTienGoiThauEUR),
                                        FGiaTriNgoaiTeKhac = s.Sum(x => x.FTienGoiThauNgoaiTeKhac),
                                        STenChiPhi = s.FirstOrDefault().STenChiPhiDT,
                                    }).ToList();

                    ItemsChiPhi.ForEach(f =>
                    {
                        f.Id = Guid.NewGuid();
                        ItemsChiPhi.Select(x =>
                        {
                            if (!x.IIdParentId.IsNullOrEmpty() && x.IIdParentId.Equals(f.IIdGoiThauChiPhiId))
                                x.IIdParentId = f.Id;
                            return x;
                        });
                    });

                    if (hdChiPhis.IsEmpty()) // add new
                    {
                        lstAdd = ItemsChiPhi;
                        _nhDaHopDongChiPhiService.AddRange(lstAdd);
                    }
                    else
                    {
                        //add new
                        var dataChiPhiAdd = ItemsChiPhi.Where(x => !hdChiPhis.Select(s => s.Id).Contains(x.Id)).ToList();
                        lstAdd = dataChiPhiAdd;
                        if (!lstAdd.IsEmpty()) _nhDaHopDongChiPhiService.AddRange(lstAdd);
                        var lstChiPhiUpdate = hdChiPhis.Where(x => itemsHangMucCPDetail.Where(x => x.IsChecked && !x.IIdHopDongChiPhiId.IsNullOrEmpty()).Select(y => y.IIdHopDongChiPhiId).Contains(x.Id)).ToList();
                        lstChiPhiUpdate.Select(s =>
                        {
                            s.FGiaTriUsd = itemsHangMucCPDetail.Any(x => x.IsChecked && !x.IIdGoiThauHangMucId.IsNullOrEmpty() && x.IIdHopDongChiPhiId.Equals(s.Id)) ? itemsHangMucCPDetail.Where(x => x.IsChecked && !x.IIdGoiThauHangMucId.IsNullOrEmpty() && x.IIdHopDongChiPhiId.Equals(s.Id)).Sum(z => z.FTienGoiThauUSD) : 0;
                            s.FGiaTriVnd = itemsHangMucCPDetail.Any(x => x.IsChecked && !x.IIdGoiThauHangMucId.IsNullOrEmpty() && x.IIdHopDongChiPhiId.Equals(s.Id)) ? itemsHangMucCPDetail.Where(x => x.IsChecked && !x.IIdGoiThauHangMucId.IsNullOrEmpty() && x.IIdHopDongChiPhiId.Equals(s.Id)).Sum(z => z.FTienGoiThauVND) : 0;
                            s.FGiaTriEur = itemsHangMucCPDetail.Any(x => x.IsChecked && !x.IIdGoiThauHangMucId.IsNullOrEmpty() && x.IIdHopDongChiPhiId.Equals(s.Id)) ? itemsHangMucCPDetail.Where(x => x.IsChecked && !x.IIdGoiThauHangMucId.IsNullOrEmpty() && x.IIdHopDongChiPhiId.Equals(s.Id)).Sum(z => z.FTienGoiThauEUR) : 0;
                            s.FGiaTriNgoaiTeKhac = itemsHangMucCPDetail.Any(x => x.IsChecked && !x.IIdGoiThauHangMucId.IsNullOrEmpty() && x.IIdHopDongChiPhiId.Equals(s.Id)) ? itemsHangMucCPDetail.Where(x => x.IsChecked && !x.IIdGoiThauHangMucId.IsNullOrEmpty() && x.IIdHopDongChiPhiId.Equals(s.Id)).Sum(z => z.FTienGoiThauNgoaiTeKhac) : 0;
                            return s;
                        }).ToList();
                        //delete cp
                        var dataChiPhiDelete = hdChiPhis.Where(x => !ItemsChiPhi.Select(s => s.Id).Contains(x.Id) && !itemsHangMucCPDetail.Where(x => x.IsChecked && !x.IIdHopDongChiPhiId.IsNullOrEmpty()).Select(y => y.IIdHopDongChiPhiId).Contains(x.Id)).ToList();
                        if (!dataChiPhiDelete.IsEmpty())
                        {
                            lstDelete = dataChiPhiDelete;
                            foreach (var item in dataChiPhiDelete)
                            {
                                _nhDaHopDongChiPhiService.Delete(item);
                            }
                        }
                    }

                    //delete hang muc ko chon
                    var hangMucHds = _nhDaHopDongHangMucService.FindByIdHopDong(Model.Id);
                    if (itemsHangMucCPDetail.Any(x => !x.IsChecked && !x.IIdHopDongHangMucId.IsNullOrEmpty()))
                    {
                        var itemsDel = hangMucHds.Where(z => itemsHangMucCPDetail.Where(x => !x.IsChecked && !x.IIdHopDongHangMucId.IsNullOrEmpty()).Select(s => s.IIdHopDongHangMucId).Contains(z.Id));
                        if (!itemsDel.IsEmpty())
                            _nhDaHopDongHangMucService.RemoveRange(itemsDel.ToList());
                    }

                    // delete hang muc co chi phi da xoa 
                    if (!lstDelete.IsEmpty())
                    {
                        var predicateHm = PredicateBuilder.True<NhDaHopDongHangMuc>();
                        predicateHm = predicateHm.And(x => lstDelete.Select(s => s.Id).Contains(x.IIdHopDongChiPhiId ?? Guid.Empty));
                        var dataDeleteByChiPhis = _nhDaHopDongHangMucService.FindAll(predicateHm);
                        if (!dataDeleteByChiPhis.IsEmpty())
                            _nhDaHopDongHangMucService.RemoveRange(dataDeleteByChiPhis.ToList());

                    }
                    //insert hang muc
                    var ItemsHangMuc = itemsHangMucCP
                                       .Where(x => !x.IIdGoiThauHangMucId.IsNullOrEmpty())
                                       .Select(s => new NhDaHopDongHangMuc
                                       {
                                           Id = s.Id,
                                           IIdHopDongChiPhiId = lstAdd.FirstOrDefault(f => f.IIdGoiThauChiPhiId.Equals(s.IIdGoiThauChiPhiId) && f.IIdChiPhiId.Equals(s.IIdChiPhiId)).Id,
                                           FGiaTriUsd = s.FTienGoiThauUSD,
                                           FGiaTriVnd = s.FTienGoiThauVND,
                                           FGiaTriEur = s.FTienGoiThauEUR,
                                           FGiaTriNgoaiTeKhac = s.FTienGoiThauNgoaiTeKhac,
                                           IIdParentId = s.IIdParentId,
                                           SMaHangMuc = s.SMaHangMuc,
                                           SMaOrder = s.SMaHangMuc,
                                           STenHangMuc = s.STenHangMuc,
                                           IIdHopDongId = Model.Id,
                                           IIdGoiThauHangMucId = s.IIdGoiThauHangMucId,
                                       }).ToList();
                    if (!ItemsHangMuc.IsEmpty()) _nhDaHopDongHangMucService.AddRange(ItemsHangMuc);
                }

                OnPropertyChanged(nameof(ItemsGoiThau));
            }
        }
        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NhDaHopDongGoiThauNhaThauModel item = (NhDaHopDongGoiThauNhaThauModel)sender;
            if (e.PropertyName == nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriUsd) ||
                e.PropertyName == nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriVnd) ||
                //e.PropertyName == nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriEur) ||
                //e.PropertyName == nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriNgoaiTeKhac) ||
                e.PropertyName == nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriHopDong_Usd) ||
                e.PropertyName == nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriHopDong_Vnd)
                //e.PropertyName == nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriHopDong_Eur) ||
                //e.PropertyName == nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriHopDong_NgoaiTeKhac)
                )

            {
                if (SelectedTiGia != null)
                {
                    item.PropertyChanged -= DetailModel_PropertyChanged;
                    //var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                    //string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                    //string sourceCurrency;
                    //string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                    //double value;
                    //switch (e.PropertyName)
                    //{
                    //    case nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriVnd):
                    //        sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                    //        value = item.FGiaTriVnd.Value;
                    //        break;
                    //    //case nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriEur):
                    //    //    sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                    //    //    value = item.FGiaTriEur.Value;
                    //    //    break;
                    //    //case nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriNgoaiTeKhac):
                    //    //    sourceCurrency = LoaiTienTeEnum.TypeCode.NGOAI_TE_KHAC;
                    //    //    value = item.FGiaTriNgoaiTeKhac.Value;
                    //    //    break;
                    //    case nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriUsd):
                    //        sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    //        value = item.FGiaTriUsd.Value;
                    //        break;
                    //    case nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriHopDong_Vnd):
                    //        sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                    //        value = item.FGiaTriHopDong_Vnd.Value;
                    //        break;
                    //    //case nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriHopDong_Eur):
                    //    //    sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                    //    //    value = item.FGiaTriHopDong_Eur.Value;
                    //    //    break;
                    //    //case nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriHopDong_NgoaiTeKhac):
                    //    //    sourceCurrency = LoaiTienTeEnum.TypeCode.NGOAI_TE_KHAC;
                    //    //    value = item.FGiaTriHopDong_NgoaiTeKhac.Value;
                    //    //    break;
                    //    default:
                    //        sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    //        value = item.FGiaTriHopDong_Usd.Value;
                    //        break;
                    //}
                    //if (e.PropertyName == nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriUsd) ||
                    //    e.PropertyName == nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriVnd) )
                    //e.PropertyName == nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriEur) ||
                    //e.PropertyName == nameof(NhDaHopDongGoiThauNhaThauModel.FGiaTriNgoaiTeKhac))
                    //{
                    //item.FGiaTriVnd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    //item.FGiaTriEur = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    //item.FGiaTriUsd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    //item.FGiaTriNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    //}
                    //else
                    //{
                    //item.FGiaTriHopDong_Eur = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    item.FGiaTriHopDong_Usd = (item.FGiaTriHopDong_Vnd != null && item.FGiaTriHopDong_Vnd.HasValue && FTiGiaNhap != null) ? (item.FGiaTriHopDong_Vnd.Value / FTiGiaNhap.Value) : 0;
                    //item.FGiaTriHopDong_NgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    //}
                    item.PropertyChanged += DetailModel_PropertyChanged;
                }

            }
            else if (e.PropertyName == nameof(NhDaHopDongGoiThauNhaThauModel.IsChecked))
            {
                item.PropertyChanged -= DetailModel_PropertyChanged;
                if (item.IsChecked == true)
                {
                    if (IsAdd) item.IIdHopDongId = Model.Id;
                    var dataGoiThauHMCP = _nhDaGoiThauHangMucSerrvice.FindGoiThauChiPhiHangMucByGoiThauId(item.IIdGoiThauId.Value);
                    if (dataGoiThauHMCP.IsEmpty())
                    {
                        item.IsDisable = true;
                    }
                    else
                    {
                        if (!(dataGoiThauHMCP.Count() == dataGoiThauHMCP.Where(x => !x.IIdHopDongChiPhiId.IsNullOrEmpty()).Count()))
                        {
                            item.IsDisable = false;
                        }
                    }
                }
                else
                {
                    if (IsAdd) item.IIdHopDongId = null;
                    item.IsDisable = true;
                }
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
            //OnPropertyChanged(nameof(ItemsGoiThau));
            //SumGiaTriHopDong();
            item.IsModified = true;
        }

        private void SumGiaTriHopDongGoiThau()
        {
            SelectedGoiThau.FGiaTriUsd = ListChiPhi.Sum(x => x.FTienHopDongUSD);
            SelectedGoiThau.FGiaTriVnd = ListChiPhi.Sum(x => x.FTienHopDongVND);
            SelectedGoiThau.FGiaTriEur = ListChiPhi.Sum(x => x.FTienHopDongEUR);
            SelectedGoiThau.FGiaTriNgoaiTeKhac = ListChiPhi.Sum(x => x.FTienHopDongNgoaiTeKhac);

            SelectedGoiThau.FGiaTriGoiThauConLaiUsd = SelectedGoiThau.FGiaTriGoiThauUsd - SelectedGoiThau.FGiaTriUsd;
            SelectedGoiThau.FGiaTriGoiThauConLaiVnd = SelectedGoiThau.FGiaTriGoiThauVnd - SelectedGoiThau.FGiaTriVnd;
            SelectedGoiThau.FGiaTriGoiThauConLaiEur = SelectedGoiThau.FGiaTriGoiThauEur - SelectedGoiThau.FGiaTriEur;
            SelectedGoiThau.FGiaTriGoiThauConLaiNgoaiTeKhac = SelectedGoiThau.FGiaTriGoiThauNgoaiTeKhac - SelectedGoiThau.FGiaTriNgoaiTeKhac;

            foreach (var item in ItemsGoiThau)
            {
                if (item.IIdGoiThauId == SelectedGoiThau.IIdGoiThauId && item.FGiaTriEur == null && item.FGiaTriUsd == null
                    && item.FGiaTriVnd == null && item.FGiaTriNgoaiTeKhac == null)
                {
                    item.FGiaTriGoiThauUsd = SelectedGoiThau.FGiaTriGoiThauConLaiUsd;
                    item.FGiaTriGoiThauVnd = SelectedGoiThau.FGiaTriGoiThauConLaiVnd;
                    item.FGiaTriGoiThauEur = SelectedGoiThau.FGiaTriGoiThauConLaiEur;
                    item.FGiaTriGoiThauNgoaiTeKhac = SelectedGoiThau.FGiaTriGoiThauConLaiNgoaiTeKhac;
                }
            }
            //SumGiaTriHopDong();
        }

        private void SumGiaTriHopDong()
        {
            var listGoiThau = ItemsGoiThau.Where(x => !x.IsDeleted).ToList();
            FGiaHopDongUsd = listGoiThau.Where(x => x.FGiaTriUsd != null).Sum(x => x.FGiaTriUsd);
            FGiaHopDongVnd = listGoiThau.Where(x => x.FGiaTriVnd != null).Sum(x => x.FGiaTriVnd);
            FGiaHopDongEur = listGoiThau.Where(x => x.FGiaTriEur != null).Sum(x => x.FGiaTriEur);
            FGiaHopDongNgoaiTeKhac = listGoiThau.Where(x => x.FGiaTriNgoaiTeKhac != null).Sum(x => x.FGiaTriNgoaiTeKhac);
        }

        public override void OnSave(Object obj)
        {
            ConverData();
            if (!ValidateViewModelHelper.Validate(Model)) return;
            if (!CheckValidate()) return;

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                NhDaHopDong entity;
                if (ILoaiMenu == 2 && IThuocMenu == 4)
                {
                    IThuocMenu = 5;
                }
                if (IsAdd)
                {
                    entity = _mapper.Map<NhDaHopDong>(Model);
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.BIsActive = true;
                    entity.BIsKhoa = false;
                    entity.ILoai = ILoai;
                    entity.IThuocMenu = IThuocMenu;
                    entity.IIdDonViQuanLyId = SelectedDonVi.Id;
                    entity.IIdMaDonViQuanLy = SelectedDonVi.IIDMaDonVi;
                    entity.DNgayHopDong = Model.DNgayHopDong;
                    entity.FTiGiaNhap = FTiGiaNhap;
                    entity.FGiaTriHopDongUSD = ItemsGoiThau.Sum(x => x.FGiaTriHopDong_Usd);
                    entity.FGiaTriHopDongVND = ItemsGoiThau.Sum(x => x.FGiaTriHopDong_Vnd);
                    entity.FGiaTriUsd = ItemsGoiThau.Sum(x => x.FGiaTriGoiThauUsd);
                    entity.FGiaTriVnd = ItemsGoiThau.Sum(x => x.FGiaTriGoiThauVnd);
                    //if (HopDongDieuChinhId.IsNullOrEmpty())
                    //{
                    //    entity.BIsGoc = true;
                    //    entity.ILanDieuChinh = 0;
                    //}
                    //else
                    //{
                    //    var hopDongGoc = _nhDaHopDongService.FindById(HopDongDieuChinhId);
                    //    hopDongGoc.BIsActive = false;
                    //    _nhDaHopDongService.Update(hopDongGoc);

                    //    entity.ILanDieuChinh = hopDongGoc.ILanDieuChinh + 1;
                    //    entity.IIdParentAdjustId = hopDongGoc.Id;
                    //    entity.BIsGoc = false;
                    //    entity.IIdParentId = !hopDongGoc.IIdParentId.IsNullOrEmpty() ? hopDongGoc.IIdParentId : hopDongGoc.Id;
                    //}
                    _nhDaHopDongService.Add(entity);
                }
                else if (IsDieuChinh)
                {
                    entity = _mapper.Map<NhDaHopDong>(Model);
                    entity.IIdParentAdjustId = Model.Id;
                    entity.BIsActive = true;
                    entity.BIsGoc = false;
                    entity.BIsKhoa = false;
                    entity.BIsXoa = false;
                    entity.FTiGiaNhap = FTiGiaNhap;
                    Model.FTiGiaNhap = FTiGiaNhap;
                    entity.ILanDieuChinh = entity.ILanDieuChinh + 1;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.DNgayTao = DateTime.Now;
                    entity.IIdParentId = Model.IIdParentId != null ? Model.IIdParentId : Model.Id;
                    entity.FGiaTriHopDongUSD = ItemsGoiThau.Sum(x => x.FGiaTriHopDong_Usd);
                    entity.FGiaTriHopDongVND = ItemsGoiThau.Sum(x => x.FGiaTriHopDong_Vnd);
                    entity.FGiaTriUsd = ItemsGoiThau.Sum(x => x.FGiaTriGoiThauUsd);
                    entity.FGiaTriVnd = ItemsGoiThau.Sum(x => x.FGiaTriGoiThauVnd);
                    _nhDaHopDongService.Adjust(entity);
                }
                else
                {
                    entity = _nhDaHopDongService.FindById(Model.Id);
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    entity.FTiGiaNhap = FTiGiaNhap;
                    entity.FGiaTriHopDongUSD = ItemsGoiThau.Sum(x => x.FGiaTriHopDong_Usd);
                    entity.FGiaTriHopDongVND = ItemsGoiThau.Sum(x => x.FGiaTriHopDong_Vnd);
                    entity.FGiaTriUsd = ItemsGoiThau.Sum(x => x.FGiaTriGoiThauUsd);
                    entity.FGiaTriVnd = ItemsGoiThau.Sum(x => x.FGiaTriGoiThauVnd);
                    Model.FTiGiaNhap = FTiGiaNhap;
                    _mapper.Map(Model, entity);
                    _nhDaHopDongService.Update(entity);
                }
                OnSavegoiThauChiPhiHangMuc(entity.Id);
                SaveAttachment(entity.Id);
                e.Result = entity;
            }, (s, e) =>
            {
                IsLoading = false;
                if (e.Error == null)
                {
                    Model = _mapper.Map<NhDaHopDongModel>(e.Result);
                    SavedAction?.Invoke(Model);
                    this.IsDetail = true;
                    LoadData();

                    // Invoke message
                    IsAdd = false;
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    System.Windows.Window window = obj as System.Windows.Window;
                    window.Close();
                }
                else
                {
                    _logger.LogError(e.Error.Message);
                }
            });
        }

        private void OnSavegoiThauChiPhiHangMuc(Guid hopDongId)
        {
            var dataHopDongs = _nhDaHopDongGoiThauNhaThauService.FindByIdHopDong(Model.Id);
            List<NhDaHopDongGoiThauNhaThauModel> listAdd = ItemsGoiThau.Where(x => x.IsChecked && !x.IsDeleted && x.IIdHopDongId.Equals(hopDongId) && x.IsAdded).ToList();
            List<NhDaHopDongGoiThauNhaThauModel> listEdit = ItemsGoiThau.Where(x => x.IsChecked && x.IsModified && !x.IsAdded && !x.IsDeleted && !x.IIdHopDongId.IsNullOrEmpty()).ToList();
            List<NhDaHopDongGoiThauNhaThauModel> listDelete = ItemsGoiThau.Where(x => x.IsDeleted && !x.IIdHopDongId.IsNullOrEmpty() || !dataHopDongs.IsEmpty() && dataHopDongs.Select(x => x.IIdGoiThauId).Contains(x.IIdGoiThauId) && !x.IsChecked).ToList();

            if (listAdd != null && listAdd.Count > 0)
            {
                for (int i = 0; i < listAdd.Count; i++)
                {
                    if (listAdd[i].IsChecked == true)
                    {
                        listAdd[i].IsCheck = 1;
                    }
                    else
                    {
                        listAdd[i].IsCheck = 2;
                    }
                }
                var listAddEntity = _mapper.Map<List<NhDaHopDongGoiThauNhaThau>>(listAdd).Select(x =>
                {
                    x.IIdHopDongId = hopDongId;

                    return x;
                }).ToList();

                _nhDaHopDongGoiThauNhaThauService.AddRange(listAddEntity);
            }
            if (listDelete != null && listDelete.Count > 0)
            {
                var listDeleteEntity = _mapper.Map<List<NhDaHopDongGoiThauNhaThau>>(listDelete);
                foreach (var item in listDeleteEntity)
                {
                    _nhDaHopDongChiPhiService.DeleteChiphiHopDongTrongNuoc(item.Id);
                    _nhDaHopDongGoiThauNhaThauService.Delete(item);
                }
            }
            if (listEdit != null && listEdit.Count > 0)
            {
                for (int i = 0; i < listEdit.Count; i++)
                {
                    if (listEdit[i].IsChecked == true)
                    {
                        listEdit[i].IsCheck = 1;
                    }
                    else
                    {
                        listEdit[i].IsCheck = 2;
                    }
                }
                var listEditEntity = _mapper.Map<List<NhDaHopDongGoiThauNhaThau>>(listEdit);
                _nhDaHopDongGoiThauNhaThauService.UpDateRange(listEditEntity);
            }
        }

        private void ConverData()
        {
            if (SelectedTiGia != null)
            {
                Model.IIdTiGiaId = SelectedTiGia.Id;
            }
            if (SelectedTiGiaChiTiet != null)
            {
                Model.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
            }
            if (SelectedNhaThau != null)
            {
                Model.IIdNhaThauThucHienId = SelectedNhaThau.Id;
            }
            if (SelectedDonVi != null)
            {
                Model.IIdDonViQuanLyId = SelectedDonVi.Id;
                Model.IIdMaDonViQuanLy = SelectedDonVi.IIDMaDonVi;
            }
            if (SelectedDuAn != null)
            {
                Model.IIdDuAnId = SelectedDuAn.Id;
                Model.IIdKhTongTheNhiemVuChiId = SelectedDuAn.IIdKhttNhiemVuChiId;
            }
            if (SelectedChuongTrinh != null)
            {
                Model.IIdKhTongTheNhiemVuChiId = SelectedChuongTrinh.IIdKHTTNhiemVuChiId;
            }
            if (SelectedLoaiHopDong != null)
            {
                Model.IIdLoaiHopDongId = SelectedLoaiHopDong.IIdLoaiHopDongId;
            }
            Model.FGiaTriUsd = FGiaHopDongUsd;
            Model.FGiaTriVnd = FGiaHopDongVnd;
            Model.FGiaTriEur = FGiaHopDongEur;
            Model.FGiaTriNgoaiTeKhac = FGiaHopDongNgoaiTeKhac;
        }
        private void ShowTiGiaNhap()
        {
            IEnumerable<NhDmTiGiaChiTiet> tiGiaChiTietList = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
            NhDmTiGiaChiTiet tiGiaChiTietObj = tiGiaChiTietList.FirstOrDefault(x => x.SMaTienTeQuyDoi.ToUpper().Equals(LoaiTienTeEnum.TypeCode.VND));
            if (tiGiaChiTietObj != null)
            {
                double? fQuyDoi = tiGiaChiTietObj.FTiGia;
                if (fQuyDoi != null && fQuyDoi.HasValue)
                {
                    FTiGiaNhap = fQuyDoi;
                }
                else
                {
                    FTiGiaNhap = null;
                }
            }
            else
            {
                FTiGiaNhap = null;
            }
        }
        private bool CheckValidate()
        {
            List<string> lstError = new List<string>();
            if (SelectedDonVi == null)
            {
                lstError.Add(Resources.MsgCheckDonVi);
            }
            if (SelectedDuAn == null && IsShowDuAn)
            {
                lstError.Add(Resources.MsgCheckDuAn);
            }
            //if (Model.SSoHopDong == null)
            //{
            //    lstError.Add(string.Format("Nhập số hợp đồng"));
            //}
            //if (Model.STenHopDong == null)
            //{
            //    lstError.Add(string.Format("Nhập tên hợp đồng"));
            //}
            //if (SelectedLoaiHopDong == null)
            //{
            //    lstError.Add(string.Format("Chọn loại hợp đồng"));
            //}
            if (SelectedNhaThau == null)
            {
                lstError.Add(Resources.MsgNhaThauDaiDien);
            }
            //if (SelectedTiGia == null)
            //{
            //    lstError.Add(string.Format("Chọn tỉ giá"));
            //}
            if (lstError.Count() > 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return false;
            }
            return true;
        }
        private void CalculateHangMuc()
        {
            var parents = ItemsGoiThau;
            if (parents.Count() > 0)
            {
                foreach (var item in parents)
                {
                    if (SelectedTiGia == null)
                    {
                        item.FGiaTriUsd = 0;
                        //item.FGiaTriVnd = 0;
                        //item.FGiaTriEur = 0;
                        //item.FGiaTriNgoaiTeKhac = 0;
                    }
                    else
                    {
                        if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                        {
                            item.FGiaTriHopDong_Usd = (item.FGiaTriHopDong_Vnd != null && item.FGiaTriHopDong_Vnd.HasValue) ? (item.FGiaTriHopDong_Vnd.Value / FTiGiaNhap.Value) : 0;
                        }
                        else
                        {
                            item.FGiaTriHopDong_Usd = 0;
                        }
                        //item.FGiaTriUsd = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, LoaiTienTeEnum.TypeCode.USD, SelectedTiGia.SMaTienTeGoc.ToUpper(), LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                        //item.FGiaTriEur = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, LoaiTienTeEnum.TypeCode.EUR, SelectedTiGia.SMaTienTeGoc.ToUpper(), LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                        //item.FGiaTriNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, (SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi.ToUpper() : ""), SelectedTiGia.SMaTienTeGoc.ToUpper(), LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                    }
                }
            }
        }
        private void ChiPhiHangMucCurrencyExChange(object sender, string propName)
        {
            if (SelectedTiGia != null)
            {
                NhDaHopDongTrongNuocHangMucGoiThauModel objectSender = (NhDaHopDongTrongNuocHangMucGoiThauModel)sender;
                var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                string sourceCurrency;
                string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                double value;
                switch (propName)
                {
                    case nameof(NhDaHopDongTrongNuocHangMucGoiThauModel.FTienHopDongVND):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                        value = objectSender.FTienHopDongVND.Value;
                        break;
                    case nameof(NhDaHopDongTrongNuocHangMucGoiThauModel.FTienHopDongEUR):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                        value = objectSender.FTienHopDongEUR.Value;
                        break;
                    case nameof(NhDaHopDongTrongNuocHangMucGoiThauModel.FTienHopDongNgoaiTeKhac):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.NGOAI_TE_KHAC;
                        value = objectSender.FTienHopDongNgoaiTeKhac.Value;
                        break;
                    default:
                        sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                        value = objectSender.FTienHopDongUSD.Value;
                        break;
                }
                objectSender.FTienHopDongVND = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                objectSender.FTienHopDongEUR = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                objectSender.FTienHopDongUSD = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                objectSender.FTienHopDongNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
            }
        }

        private void ChiPhiCurrencyExChange(object sender, string propName)
        {
            if (SelectedTiGia != null)
            {
                NhDaHopDongTrongNuocChiPhiGoiThauModel objectSender = (NhDaHopDongTrongNuocChiPhiGoiThauModel)sender;
                var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                string sourceCurrency;
                string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                double value;
                switch (propName)
                {
                    case nameof(NhDaHopDongTrongNuocChiPhiGoiThauModel.FTienHopDongVND):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                        value = objectSender.FTienHopDongVND.Value;
                        break;
                    case nameof(NhDaHopDongTrongNuocChiPhiGoiThauModel.FTienHopDongEUR):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                        value = objectSender.FTienHopDongEUR.Value;
                        break;
                    case nameof(NhDaHopDongTrongNuocChiPhiGoiThauModel.FTienHopDongNgoaiTeKhac):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.NGOAI_TE_KHAC;
                        value = objectSender.FTienHopDongNgoaiTeKhac.Value;
                        break;
                    default:
                        sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                        value = objectSender.FTienHopDongUSD.Value;
                        break;
                }
                objectSender.FTienHopDongVND = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                objectSender.FTienHopDongEUR = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                objectSender.FTienHopDongUSD = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                objectSender.FTienHopDongNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
            }
        }

        public override void OnClose(object obj)
        {
            if (IsAdd)
            {
                //Delete goi thau hang muc chi phi.
                _nhDaHopDongChiPhiService.DeleteChiphiHangMucHopDongByIdHopDong(Model.Id);
                foreach (var item in ItemsGoiThau)
                {
                    var data = _nhDaGoiThauHangMucSerrvice.FindByGoiThauId(item.IIdGoiThauId.Value).Where(x => (x.IsCheck == 1 && x.IIDGoiThauCheck == item.Id) || x.IsCheck == 2 || x.IsCheck == 0).ToList();

                    if (data.Count != 0)
                    {
                        foreach (var itemhangmuc in data)
                        {
                            NhDaGoiThauHangMuc itemsave = _nhDaGoiThauHangMucSerrvice.FindHangMucById(itemhangmuc.Id);
                            itemsave.IIDGoiThauCheck = null;
                            itemsave.IsCheck = 0;
                            _nhDaGoiThauHangMucSerrvice.Update(itemsave);
                        }
                    }
                }
            }
            else
            {
                foreach (var item in listBackUpHangMuc)
                {
                    _nhDaGoiThauHangMucSerrvice.Update(item);
                }
                listBackUpHangMuc.Clear();
            }
            IsAdd = false;
            System.Windows.Window window = obj as System.Windows.Window;
            window.Close();
        }
    }
}
