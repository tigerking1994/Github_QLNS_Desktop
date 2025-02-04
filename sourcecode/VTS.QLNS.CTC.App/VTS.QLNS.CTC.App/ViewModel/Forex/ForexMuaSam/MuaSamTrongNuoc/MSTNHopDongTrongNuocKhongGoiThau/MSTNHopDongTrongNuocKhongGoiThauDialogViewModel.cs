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
using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNHopDongTrongNuocKhongGoiThau;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNHopDongTrongNuocKhongGoiThau
{
    public class MSTNHopDongTrongNuocKhongGoiThauDialogViewModel : DialogViewModelBase<NhDaHopDongModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILogger<MSTNHopDongTrongNuocKhongGoiThauDialogViewModel> _logger;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDmLoaiHopDongService _nhDmLoaiHopDongService;
        private readonly INhDmNhaThauService _nhDmNhaThauService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly INhDaHopDongHangMucService _nhDaHopDongHangMucService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INhMstnKeHoachDatHangService _nhKhDatHangService;
        private readonly INhMstnKeHoachDatHangDanhMucService _nhKhDatHangDanhMucService;

        public override Type ContentType => typeof(HopDongTrongNuocKhongGoiThauDialog);

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
                if (SetProperty(ref _selectedDonVi, value) && value != null)
                {
                    LoadKeHoachTongThe();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsKeHoachTongThe;
        public ObservableCollection<ComboboxItem> ItemsKeHoachTongThe
        {
            get => _itemsKeHoachTongThe;
            set => SetProperty(ref _itemsKeHoachTongThe, value);
        }

        private ComboboxItem _selectedKeHoachTongThe;
        public ComboboxItem SelectedKeHoachTongThe
        {
            get => _selectedKeHoachTongThe;
            set
            {
                if (SetProperty(ref _selectedKeHoachTongThe, value) && value != null)
                {
                    LoadChuongTrinh();
                }
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
                if (SetProperty(ref _selectedChuongTrinh, value) && value != null)
                {
                    LoadKeHoachDatHang();
                }
            }
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
                    //LoadTiGiaChiTiet();
                    if (value != null)
                    {
                        IsVisibleTiGiaNhap = true;
                        ShowTiGiaNhap();
                    }
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

        private ObservableCollection<NhDmNhaThauModel> _itemsNhaThau;
        public ObservableCollection<NhDmNhaThauModel> ItemsNhaThau
        {
            get => _itemsNhaThau;
            set => SetProperty(ref _itemsNhaThau, value);
        }

        private ObservableCollection<ComboboxItem> _itemsNhaThauGoiThau;
        public ObservableCollection<ComboboxItem> ItemsNhaThauGoiThau
        {
            get => _itemsNhaThauGoiThau;
            set => SetProperty(ref _itemsNhaThauGoiThau, value);
        }

        private ObservableCollection<NhMstnKeHoachDatHangModel> _itemsKeHoachDatHang;
        public ObservableCollection<NhMstnKeHoachDatHangModel> ItemsKeHoachDatHang
        {
            get => _itemsKeHoachDatHang;
            set => SetProperty(ref _itemsKeHoachDatHang, value);
        }

        private NhMstnKeHoachDatHangModel _selectedKeHoachDatHang;
        public NhMstnKeHoachDatHangModel SelectedKeHoachDatHang
        {
            get => _selectedKeHoachDatHang;
            set
            {
                if (SetProperty(ref _selectedKeHoachDatHang, value))
                {
                    LoadKHDHDanhMucByKHDTId();
                    LoadTiGiaByKeHoachDatHang();
                }
            }
        }

        private ObservableCollection<NhMstnKeHoachDatHangDanhMucModel> _itemsKeHoachDatHangDanhMuc;
        public ObservableCollection<NhMstnKeHoachDatHangDanhMucModel> ItemsKeHoachDatHangDanhMuc
        {
            get => _itemsKeHoachDatHangDanhMuc;
            set => SetProperty(ref _itemsKeHoachDatHangDanhMuc, value);
        }

        private NhMstnKeHoachDatHangDanhMucModel _selectedKeHoachDatHangDanhMuc;
        public NhMstnKeHoachDatHangDanhMucModel SelectedKeHoachDatHangDanhMuc
        {
            get => _selectedKeHoachDatHangDanhMuc;
            set => SetProperty(ref _selectedKeHoachDatHangDanhMuc, value);
        }

        private IEnumerable<NhDmTiGiaChiTiet> _lstTiGiaChiTiet;
        public IEnumerable<NhDmTiGiaChiTiet> LstTiGiaChiTiet
        {
            get => _lstTiGiaChiTiet;
            set => SetProperty(ref _lstTiGiaChiTiet, value);
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
                    if (ItemsKeHoachDatHangDanhMuc != null)
                    {
                        CalculateHangMuc();
                        CalculateGiaTriDuocDuyet();
                    }
                }
            }
        }

        public bool IsDieuChinh { get; set; }
        public bool IsDetail { get; set; }
        public Guid HopDongDieuChinhId { get; set; }
        public int ILoai { get; set; }
        public int IThuocMenu { get; set; }

        public MSTNHopDongTrongNuocKhongGoiThauDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILogger<MSTNHopDongTrongNuocKhongGoiThauDialogViewModel> logger,
            INsDonViService nsDonViService,
            INhDmLoaiHopDongService nhDmLoaiHopDongService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            INhDmNhaThauService nhDmNhaThauService,
            INhDaHopDongService nhDaHopDongService,
            INhKhTongTheService nhKhTongTheService,
            INhDaHopDongHangMucService nhDaHopDongHangMucService,
            INhMstnKeHoachDatHangService nhKhDatHangService,
            INhMstnKeHoachDatHangDanhMucService nhKhDatHangDanhMucService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _nsDonViService = nsDonViService;
            _nhDmLoaiHopDongService = nhDmLoaiHopDongService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
            _nhDaHopDongService = nhDaHopDongService;
            _nhDaHopDongHangMucService = nhDaHopDongHangMucService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _nhDmNhaThauService = nhDmNhaThauService;
            _nhKhTongTheService = nhKhTongTheService;
            _nhKhDatHangService = nhKhDatHangService;
            _nhKhDatHangDanhMucService = nhKhDatHangDanhMucService;
        }

        public override void Init()
        {
            LoadDonVi();
            LoadLoaiHopDong();
            LoadNhaThau();
            LoadTiGia();
            LoadData();
        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindByCondition(x => x.NamLamViec == _sessionService.Current.YearOfWork).OrderBy(x => x.IIDMaDonVi);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadLoaiHopDong()
        {
            var data = _nhDmLoaiHopDongService.FindAll();
            _itemsLoaiHopDong = _mapper.Map<ObservableCollection<NhDmLoaiHopDongModel>>(data);
            OnPropertyChanged(nameof(ItemsLoaiHopDong));
        }

        private void LoadTiGia()
        {
            var data = _nhDmTiGiaService.FindAll();
            _itemsTiGia = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(data);
            OnPropertyChanged(nameof(ItemsTiGia));
        }

        private void LoadKeHoachDatHang()
        {
            _itemsKeHoachDatHang = new ObservableCollection<NhMstnKeHoachDatHangModel>();
            if (SelectedDonVi != null && SelectedKeHoachTongThe != null && SelectedChuongTrinh != null)
            {
                IEnumerable<NhMstnKeHoachDatHangQuery> data = _nhKhDatHangService.FindMstnKeHoachDatHangByCondition(SelectedDonVi.Id, Guid.Parse(SelectedKeHoachTongThe.ValueItem), SelectedChuongTrinh.IIdKHTTNhiemVuChiId);
                _itemsKeHoachDatHang = _mapper.Map<ObservableCollection<NhMstnKeHoachDatHangModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsKeHoachDatHang));
        }

        private void LoadKeHoachTongThe()
        {
            var lstKeHoachTongThe = _nhKhTongTheService.FindByDonViId(SelectedDonVi.Id).ToList();
            if (lstKeHoachTongThe.Any())
            {
                var result = lstKeHoachTongThe.Select(x =>
                {
                    ComboboxItem cb = new ComboboxItem();
                    if (x.INamKeHoach.HasValue)
                    {
                        cb.DisplayItem = "KHTT " + x.INamKeHoach.Value + "- Số KH: " + x.SSoKeHoachBqp;
                        cb.ValueItem = x.Id.ToString();
                    }
                    else
                    {
                        cb.DisplayItem = "KHTT " + x.IGiaiDoanTu_BQP.GetValueOrDefault() + "-" + x.IGiaiDoanDen_BQP.GetValueOrDefault() + " - Số KH: " + x.SSoKeHoachBqp;
                        cb.ValueItem = x.Id.ToString();
                    }
                    return cb;
                }).ToList();
                _itemsKeHoachTongThe = new ObservableCollection<ComboboxItem>(result);
            }
            else
            {
                _itemsKeHoachTongThe = new ObservableCollection<ComboboxItem>();
            }
            OnPropertyChanged(nameof(ItemsKeHoachTongThe));
        }

        private void LoadChuongTrinh()
        {
            ItemsChuongTrinh = new ObservableCollection<NhDmNhiemVuChiModel>();
            if (_selectedKeHoachTongThe != null && _selectedDonVi != null)
            {
                var data = _nhDmNhiemVuChiService.FindByKhTongTheIdAndDonViId(Guid.Parse(SelectedKeHoachTongThe.ValueItem), SelectedDonVi.Id);
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

        private void LoadNhaThau()
        {
            var data = _nhDmNhaThauService.FindAll();
            _itemsNhaThau = _mapper.Map<ObservableCollection<NhDmNhaThauModel>>(data);
            OnPropertyChanged(nameof(ItemsNhaThau));

            _itemsNhaThauGoiThau = _mapper.Map<ObservableCollection<ComboboxItem>>(ItemsNhaThau);
            OnPropertyChanged(nameof(ItemsNhaThauGoiThau));
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

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                IconKind = PackIconKind.PlaylistPlus;
                Description = "Thêm mới hợp đồng";
                Title = "THÊM MỚI HỢP ĐỒNG";
                SelectedDonVi = null;
                SelectedKeHoachTongThe = null;
                SelectedChuongTrinh = null;
                SelectedKeHoachDatHang = null;
                FTiGiaNhap = null;
                IsVisibleTiGiaNhap = false;
            }
            else
            {
                if (IsDetail)
                {
                    IconKind = PackIconKind.Details;
                    Description = "Chi tiết hợp đồng";
                    Title = "CHI TIẾT HỢP ĐỒNG";
                }
                else
                {
                    if (HopDongDieuChinhId.IsNullOrEmpty())
                    {
                        IconKind = PackIconKind.NoteEditOutline;
                        Description = "Cập nhập hợp đồng";
                        Title = "CẬP NHẬP HỢP ĐỒNG";
                    }
                    else
                    {
                        IconKind = PackIconKind.Adjust;
                        Description = "Điều chỉnh hợp đồng";
                        Title = "ĐIỀU CHỈNH HỢP ĐỒNG";
                    }
                }
                IsVisibleTiGiaNhap = false;
                LoadDetailData();
            }
            LoadKeHoachDatHangDanhMuc();
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedKeHoachTongThe));
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            //OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
            OnPropertyChanged(nameof(SelectedLoaiHopDong));
        }

        private void LoadDetailData()
        {
            SelectedDonVi = ItemsDonVi.FirstOrDefault(x => x.Id == Model.IIdDonViQuanLyId);
            SelectedKeHoachTongThe = ItemsKeHoachTongThe.IsEmpty() ? null : ItemsKeHoachTongThe.FirstOrDefault(x => x.ValueItem.ToString().Equals(Model.IIdKhTongTheId.GetValueOrDefault().ToString()));
            SelectedChuongTrinh = ItemsChuongTrinh.IsEmpty() ? null : ItemsChuongTrinh.FirstOrDefault(x => x.IIdKHTTNhiemVuChiId == Model.IIdKhTongTheNhiemVuChiId);
            _selectedKeHoachDatHang = !ItemsKeHoachDatHang.IsEmpty() ? ItemsKeHoachDatHang.FirstOrDefault(x => x.Id == Model.IIdKeHoachDatHangId) : null;

            SelectedTiGia = ItemsTiGia.IsEmpty() ? null : ItemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
            //LoadTiGiaChiTiet();
            //_selectedTiGiaChiTiet = ItemsTiGiaChiTiet.FirstOrDefault(x => x.SMaTienTeQuyDoi.ToUpper().Equals(Model.SMaNgoaiTeKhac.ToUpper()));
            if (SelectedTiGia != null)
            {
                IsVisibleTiGiaNhap = true;
            }
            FTiGiaNhap = Model.FTiGiaNhap;
            _selectedLoaiHopDong = ItemsLoaiHopDong.IsEmpty() ? null : ItemsLoaiHopDong.FirstOrDefault(x => x.IIdLoaiHopDongId == Model.IIdLoaiHopDongId);

            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedKeHoachTongThe));
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            //OnPropertyChanged(nameof(SelectedTiGia));
            //OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
            OnPropertyChanged(nameof(SelectedLoaiHopDong));
            OnPropertyChanged(nameof(SelectedKeHoachDatHang));
        }

        private void LoadKeHoachDatHangDanhMuc()
        {
            ItemsKeHoachDatHangDanhMuc = new ObservableCollection<NhMstnKeHoachDatHangDanhMucModel>();
            if (!Model.Id.IsNullOrEmpty())
            {
                if (Model.IIdKeHoachDatHangId.HasValue)
                {
                    var data = _nhKhDatHangDanhMucService.FindByKHDatHangId(Model.IIdKeHoachDatHangId.Value);
                    var keHoachDatHangDanhMucList = _nhDaHopDongHangMucService.FindByIdHopDong(Model.Id);
                    _itemsKeHoachDatHangDanhMuc = _mapper.Map<ObservableCollection<NhMstnKeHoachDatHangDanhMucModel>>(data);
                    NhDaHopDongHangMuc keHoachDatHangDanhMuc;
                    foreach (var item in _itemsKeHoachDatHangDanhMuc)
                    {
                        keHoachDatHangDanhMuc = keHoachDatHangDanhMucList.FirstOrDefault(x => x.IIdKeHoachDatHangDanhMucId.Equals(item.Id));
                        item.IsChecked = keHoachDatHangDanhMuc != null;
                        if (item.IsChecked)
                        {
                            item.STenDanhMuc = keHoachDatHangDanhMuc.STenHangMuc;
                            item.SDonViTinh = keHoachDatHangDanhMuc.SDonViTinh;
                            item.IID_NhaThauID = keHoachDatHangDanhMuc.IIdHopDongNhaThauId;
                            item.ISoLuong = keHoachDatHangDanhMuc.ISoLuong;
                            item.FDonGia_VND = keHoachDatHangDanhMuc.FDonGia;
                            item.FGiaTriVnd = keHoachDatHangDanhMuc.FGiaTriVnd;
                            item.FGiaTriUsd = keHoachDatHangDanhMuc.FGiaTriUsd;
                            //item.FGiaTriEur = keHoachDatHangDanhMuc.FGiaTriEur;
                            //item.FGiaTriNgoaiTeKhac = keHoachDatHangDanhMuc.FGiaTriNgoaiTeKhac;
                            item.SGhiChu = keHoachDatHangDanhMuc.SGhiChu;
                        }
                    }
                    OrderItems();
                    UpdateTreeItems();
                    _itemsKeHoachDatHangDanhMuc.ForAll(x => x.PropertyChanged += KeHoachDatHangDanhMucModel_PropertyChanged);
                    var sortingListQueries = new List<SortingListQuery<NhMstnKeHoachDatHangDanhMucModel>>();
                    sortingListQueries = SortingHierarchicalNumber<NhMstnKeHoachDatHangDanhMucModel>.GetSortingLists(_itemsKeHoachDatHangDanhMuc.Select(x => new SortingListQuery<NhMstnKeHoachDatHangDanhMucModel>() { Key = x, SortKey = x.SMaOrder }).ToList(), '-');
                    _itemsKeHoachDatHangDanhMuc = new ObservableCollection<NhMstnKeHoachDatHangDanhMucModel>(sortingListQueries.Select(x => x.Key));
                    OnPropertyChanged(nameof(ItemsKeHoachDatHangDanhMuc));
                }
            }
        }

        private void LoadKHDHDanhMucByKHDTId()
        {
            if (SelectedKeHoachDatHang != null)
            {
                var data = _nhKhDatHangDanhMucService.FindByKHDatHangId(SelectedKeHoachDatHang.Id);
                var keHoachDatHangDanhMucList = _nhDaHopDongHangMucService.FindByIdHopDong(Model.Id);
                _itemsKeHoachDatHangDanhMuc = _mapper.Map<ObservableCollection<NhMstnKeHoachDatHangDanhMucModel>>(data);
                NhDaHopDongHangMuc keHoachDatHangDanhMuc;
                foreach (var item in _itemsKeHoachDatHangDanhMuc)
                {
                    keHoachDatHangDanhMuc = keHoachDatHangDanhMucList.FirstOrDefault(x => x.IIdKeHoachDatHangDanhMucId.Equals(item.Id));
                    item.IsChecked = keHoachDatHangDanhMuc != null;
                    if (item.IsChecked)
                    {
                        item.STenDanhMuc = keHoachDatHangDanhMuc.STenHangMuc;
                        item.SDonViTinh = keHoachDatHangDanhMuc.SDonViTinh;
                        item.IID_NhaThauID = keHoachDatHangDanhMuc.IIdHopDongNhaThauId;
                        item.ISoLuong = keHoachDatHangDanhMuc.ISoLuong;
                        item.FDonGia_VND = keHoachDatHangDanhMuc.FDonGia;
                        item.FGiaTriVnd = keHoachDatHangDanhMuc.FGiaTriVnd;
                        item.FGiaTriUsd = keHoachDatHangDanhMuc.FGiaTriUsd;
                        //item.FGiaTriEur = keHoachDatHangDanhMuc.FGiaTriEur;
                        //item.FGiaTriNgoaiTeKhac = keHoachDatHangDanhMuc.FGiaTriNgoaiTeKhac;
                        item.SGhiChu = keHoachDatHangDanhMuc.SGhiChu;
                    }
                }
                OrderItems();
                UpdateTreeItems();
                _itemsKeHoachDatHangDanhMuc.ForAll(x => x.PropertyChanged += KeHoachDatHangDanhMucModel_PropertyChanged);
                var sortingListQueries = new List<SortingListQuery<NhMstnKeHoachDatHangDanhMucModel>>();
                sortingListQueries = SortingHierarchicalNumber<NhMstnKeHoachDatHangDanhMucModel>.GetSortingLists(_itemsKeHoachDatHangDanhMuc.Select(x => new SortingListQuery<NhMstnKeHoachDatHangDanhMucModel>() { Key = x, SortKey = x.SMaOrder }).ToList(), '-');
                _itemsKeHoachDatHangDanhMuc = new ObservableCollection<NhMstnKeHoachDatHangDanhMucModel>(sortingListQueries.Select(x => x.Key));
                OnPropertyChanged(nameof(ItemsKeHoachDatHangDanhMuc));
            }
        }

        private void LoadTiGiaByKeHoachDatHang()
        {
            //Show ty gia
            if (SelectedKeHoachDatHang != null)
            {
                SelectedTiGia = _itemsTiGia.IsEmpty() ? null : _itemsTiGia.FirstOrDefault(x => x.Id.Equals(SelectedKeHoachDatHang.IIdTiGiaID));
                IsVisibleTiGiaNhap = true;
                FTiGiaNhap = SelectedKeHoachDatHang.FTiGiaNhap ?? 0;
            }

        }

        private void OrderItems(Guid? parentId = null)
        {
            var childs = ItemsKeHoachDatHangDanhMuc.Where(x => x.IID_ParentID == parentId);
            if (!childs.IsEmpty())
            {
                foreach (var child in childs)
                {
                    child.SMaDanhMuc = StringUtils.ConvertMaOrder(child.SMaOrder);
                    OrderItems(child.Id);
                }
            }
        }

        private void UpdateTreeItems()
        {
            if (!_itemsKeHoachDatHangDanhMuc.IsEmpty())
            {
                _itemsKeHoachDatHangDanhMuc.ForAll(s => s.IsEnabled = !_itemsKeHoachDatHangDanhMuc.Any(y => y.IID_ParentID == s.Id));
                _itemsKeHoachDatHangDanhMuc.ForAll(x =>
                {
                    if (x.IID_ParentID.IsNullOrEmpty() || !_itemsKeHoachDatHangDanhMuc.Any(y => y.Id == x.IID_ParentID)) x.IsHangCha = true;
                    if (!x.IsEnabled) x.IsHangCha = true;
                    else if (_itemsKeHoachDatHangDanhMuc.Any(y => y.IID_ParentID == x.IID_ParentID && !y.IsEnabled)) x.IsHangCha = true;
                });
            };
        }

        private void KeHoachDatHangDanhMucModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            foreach (var item in _itemsKeHoachDatHangDanhMuc)
            {
                item.PropertyChanged -= KeHoachDatHangDanhMucModel_PropertyChanged;
            }
            NhMstnKeHoachDatHangDanhMucModel objectSender = (NhMstnKeHoachDatHangDanhMucModel)sender;
            switch (e.PropertyName)
            {
                case nameof(NhMstnKeHoachDatHangDanhMucModel.ISoLuong):
                case nameof(NhMstnKeHoachDatHangDanhMucModel.FDonGia_VND):
                    //if (SelectedTiGia != null)
                    //{
                    //    LstTiGiaChiTiet = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
                    CalculateHangMuc();
                    //}
                    CalculateGiaTriDuocDuyet();
                    break;
                case nameof(NhMstnKeHoachDatHangDanhMucModel.IsChecked):
                    UpdateTreeCheckbox(objectSender);
                    CalculateGiaTriDuocDuyet();
                    break;
                default:
                    break;
            }
            foreach (var item in _itemsKeHoachDatHangDanhMuc)
            {
                item.PropertyChanged += KeHoachDatHangDanhMucModel_PropertyChanged;
            }
        }

        private void UpdateTreeCheckbox(NhMstnKeHoachDatHangDanhMucModel currentItem)
        {
            UpdateCheckboxChilds(currentItem);
            if (currentItem.IID_ParentID != null)
            {
                UpdateCheckboxParents(currentItem);
            }
        }

        private void UpdateCheckboxParents(NhMstnKeHoachDatHangDanhMucModel currentItem)
        {
            var parent = ItemsKeHoachDatHangDanhMuc.FirstOrDefault(x => x.Id == currentItem.IID_ParentID);
            if (parent != null)
            {
                int countItems = ItemsKeHoachDatHangDanhMuc.Count(x => x.IID_ParentID == parent.Id);
                int countItemsChecked = ItemsKeHoachDatHangDanhMuc.Count(x => x.IID_ParentID == parent.Id && x.IsChecked);
                parent.IsChecked = countItems == countItemsChecked;
                UpdateCheckboxParents(parent);
            }
        }

        private void UpdateCheckboxChilds(NhMstnKeHoachDatHangDanhMucModel currentItem)
        {
            var childs = ItemsKeHoachDatHangDanhMuc.Where(x => x.IID_ParentID == currentItem.Id);
            foreach (var child in childs)
            {
                child.IsChecked = currentItem.IsChecked;
                UpdateCheckboxChilds(child);
            }
        }

        private void CalculateGiaTriDuocDuyet()
        {
            var rowChilds = ItemsKeHoachDatHangDanhMuc.Where(x => x.IsChecked && !ItemsKeHoachDatHangDanhMuc.Where(y => y.IID_ParentID == x.Id).Any());
            Model.FGiaTriHopDongUSD = rowChilds.Sum(x => x.FGiaTriUsd);
            Model.FGiaTriHopDongVND = rowChilds.Sum(x => x.FGiaTriVnd);
            //Model.FGiaTriHopDongEUR = rowChilds.Sum(x => x.FGiaTriEur);
            //Model.FGiaTriHopDongNgoaiTeKhac = rowChilds.Sum(x => x.FGiaTriNgoaiTeKhac);
        }

        private void CalculateHangMuc()
        {
            var parents = ItemsKeHoachDatHangDanhMuc.Where(x => x.IID_ParentID == null);
            if (parents.Count() > 0)
            {
                foreach (var item in parents)
                {
                    CalculateHangMuc(item);
                    var childs = ItemsKeHoachDatHangDanhMuc.Where(x => x.IID_ParentID == item.Id);
                    if (childs.Count() == 0)
                    {
                        if (item.FDonGia_VND == null || item.ISoLuong == null)
                        {
                            item.FGiaTriUsd = 0;
                            item.FGiaTriVnd = 0;
                            //item.FGiaTriEur = 0;
                            //item.FGiaTriNgoaiTeKhac = 0;
                        }
                        else
                        {
                            item.FGiaTriVnd = item.ISoLuong * item.FDonGia_VND;
                            if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                            {
                                item.FGiaTriUsd = (item.FGiaTriVnd != null && item.FGiaTriVnd.HasValue) ? (item.FGiaTriVnd.Value / FTiGiaNhap.Value) : 0;
                            }
                            else
                            {
                                item.FGiaTriUsd = 0;
                            }
                            //item.FGiaTriUsd = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, LoaiTienTeEnum.TypeCode.USD, SelectedTiGia.SMaTienTeGoc.ToUpper(), LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                            //item.FGiaTriEur = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, LoaiTienTeEnum.TypeCode.EUR, SelectedTiGia.SMaTienTeGoc.ToUpper(), LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                            //item.FGiaTriNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, (SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi.ToUpper() : ""), SelectedTiGia.SMaTienTeGoc.ToUpper(), LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                        }
                    }
                }
            }
        }

        private void CalculateHangMuc(NhMstnKeHoachDatHangDanhMucModel parentItem)
        {
            var childs = ItemsKeHoachDatHangDanhMuc.Where(x => x.IID_ParentID == parentItem.Id);
            if (childs.Count() > 0)
            {
                foreach (var item in childs)
                {
                    CalculateHangMuc(item);
                    if (item.ISoLuong != null && item.FDonGia_VND != null)
                    {
                        item.FGiaTriVnd = item.ISoLuong * item.FDonGia_VND;
                        if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                        {
                            item.FGiaTriUsd = (item.FGiaTriVnd != null && item.FGiaTriVnd.HasValue) ? (item.FGiaTriVnd.Value / FTiGiaNhap.Value) : 0;
                        }
                        else
                        {
                            item.FGiaTriUsd = 0;
                        }
                        //item.FGiaTriUsd = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, LoaiTienTeEnum.TypeCode.USD, SelectedTiGia.SMaTienTeGoc.ToUpper(), LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                        //item.FGiaTriEur = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, LoaiTienTeEnum.TypeCode.EUR, SelectedTiGia.SMaTienTeGoc.ToUpper(), LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                        //item.FGiaTriNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, (SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi.ToUpper() : ""), SelectedTiGia.SMaTienTeGoc.ToUpper(), LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                    }
                }
                parentItem.ISoLuong = null;
                parentItem.FDonGia_VND = null;
                parentItem.FGiaTriUsd = childs.Sum(x => x.FGiaTriUsd);
                parentItem.FGiaTriVnd = childs.Sum(x => x.FGiaTriVnd);
                //parentItem.FGiaTriEur = childs.Sum(x => x.FGiaTriEur);
                //parentItem.FGiaTriNgoaiTeKhac = childs.Sum(x => x.FGiaTriNgoaiTeKhac);
            }
        }

        public void HangMuc_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedKeHoachDatHangDanhMuc = (NhMstnKeHoachDatHangDanhMucModel)e.Row.Item;
            if (e.Column.SortMemberPath.Equals(nameof(NhMstnKeHoachDatHangDanhMucModel.FDonGia_VND)) ||
                e.Column.SortMemberPath.Equals(nameof(NhMstnKeHoachDatHangDanhMucModel.ISoLuong)))
            {
                if (SelectedKeHoachDatHangDanhMuc.IsHangCha && ItemsKeHoachDatHangDanhMuc.Where(x => x.IID_ParentID != null && x.IID_ParentID == SelectedKeHoachDatHangDanhMuc.Id).Any())
                {
                    e.Cancel = true;
                }
            }
        }

        public override void OnSave(object obj)
        {
            if (!CheckValidate()) return;

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                NhDaHopDong entity;
                ConverData();
                if (Model.Id.IsNullOrEmpty() || IsDieuChinh)
                {
                    entity = _mapper.Map<NhDaHopDong>(Model);
                    entity.Id = Guid.NewGuid();
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
                    if (HopDongDieuChinhId.IsNullOrEmpty())
                    {
                        entity.BIsGoc = true;
                        entity.ILanDieuChinh = 0;
                    }
                    else
                    {
                        var hopDongGoc = _nhDaHopDongService.FindById(HopDongDieuChinhId);
                        hopDongGoc.BIsActive = false;
                        _nhDaHopDongService.Update(hopDongGoc);

                        entity.ILanDieuChinh = hopDongGoc.ILanDieuChinh + 1;
                        entity.IIdParentAdjustId = hopDongGoc.Id;
                        entity.BIsGoc = false;
                        entity.IIdParentId = !hopDongGoc.IIdParentId.IsNullOrEmpty() ? hopDongGoc.IIdParentId : hopDongGoc.Id;
                    }
                    _nhDaHopDongService.Add(entity);
                }
                else
                {
                    entity = _nhDaHopDongService.FindById(Model.Id);
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    _mapper.Map(Model, entity);
                    entity.FTiGiaNhap = FTiGiaNhap;
                    _nhDaHopDongService.Update(entity);
                }
                OnSaveHopDongHangMuc(entity.Id);
                e.Result = entity;
            }, (s, e) =>
            {
                IsLoading = false;
                if (e.Error == null)
                {
                    Model = _mapper.Map<NhDaHopDongModel>(e.Result);
                    SavedAction?.Invoke(Model);
                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    var view = obj as Window;
                    if (view != null) view.Close();
                }
                else
                {
                    _logger.LogError(e.Error.Message);
                }
            });
        }

        private void OnSaveHopDongHangMuc(Guid hopDongId)
        {
            _nhDaHopDongHangMucService.DeleteByIdHopDong(hopDongId);
            List<NhMSTNKeHoachDatHangDanhMuc> data = _mapper.Map<List<NhMSTNKeHoachDatHangDanhMuc>>(ItemsKeHoachDatHangDanhMuc.Where(x => x.IsChecked));
            List<NhDaHopDongHangMuc> nhDaHopDongHangMucs = _mapper.Map<List<NhDaHopDongHangMuc>>(data);
            nhDaHopDongHangMucs.ForEach(x => {
                x.Id = Guid.NewGuid();
                x.IIdHopDongId = hopDongId;
            });
            _nhDaHopDongHangMucService.AddRange(nhDaHopDongHangMucs);
        }

        private void ConverData()
        {
            if (SelectedDonVi != null)
            {
                Model.IIdDonViQuanLyId = SelectedDonVi.Id;
                Model.IIdMaDonViQuanLy = SelectedDonVi.IIDMaDonVi;
            }
            if (SelectedKeHoachTongThe != null)
            {
                Model.IIdKhTongTheId = Guid.Parse(SelectedKeHoachTongThe.ValueItem);
            }
            if (SelectedChuongTrinh != null)
            {
                Model.IIdKhTongTheNhiemVuChiId = SelectedChuongTrinh.IIdKHTTNhiemVuChiId;
            }
            if (SelectedKeHoachDatHang != null)
            {
                Model.IIdKeHoachDatHangId = SelectedKeHoachDatHang.Id;
            }
            if (SelectedLoaiHopDong != null)
            {
                Model.IIdLoaiHopDongId = SelectedLoaiHopDong.IIdLoaiHopDongId;
            }
            if (SelectedTiGia != null)
            {
                Model.IIdTiGiaId = SelectedTiGia.Id;
            }
            //if (SelectedTiGiaChiTiet != null)
            //{
            //    Model.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
            //}
        }

        private bool CheckValidate()
        {
            List<string> lstError = new List<string>();
            if (SelectedDonVi == null)
            {
                lstError.Add(Resources.MsgCheckDonVi);
            }
            if (SelectedKeHoachTongThe == null)
            {
                lstError.Add(Resources.MsgCheckKeHoachTongThe);
            }
            if (SelectedChuongTrinh == null)
            {
                lstError.Add(Resources.MsgCheckChuongTrinh);
            }
            if (SelectedKeHoachDatHang == null)
            {
                lstError.Add(Resources.MsgCheckKeHoachDatHang);
            }
            if (string.IsNullOrWhiteSpace(Model.SSoHopDong))
            {
                lstError.Add(Resources.MsgCheckSoHopDong);
            }
            if (string.IsNullOrWhiteSpace(Model.STenHopDong))
            {
                lstError.Add(Resources.MsgCheckTenHopDong);
            }
            if (Model.DNgayHopDong == null)
            {
                lstError.Add(Resources.MsgCheckNgayKyHopDong);
            }
            if (SelectedLoaiHopDong == null)
            {
                lstError.Add(Resources.MsgCheckLoaiHopDong);
            }
            //if (SelectedTiGia == null)
            //{
            //    lstError.Add(Resources.MsgCheckTiGiaTheoVND);
            //}
            //if (SelectedTiGiaChiTiet == null)
            //{
            //    lstError.Add(Resources.MsgCheckMaNgoaiTeNgoaiHoi);
            //}
            if (Model.DKhoiCongDuKien != null && Model.DKetThucDuKien != null && Model.DKhoiCongDuKien.HasValue && Model.DKetThucDuKien.HasValue)
            {
                if (Model.DKhoiCongDuKien.Value > Model.DKetThucDuKien.Value)
                {
                    lstError.Add(Resources.MsgErrorPeriodDate);
                }
            }
            if (lstError.Count() > 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return false;
            }

            var hopDongList = _nhDaHopDongService.FindByCondition(x => x.IThuocMenu == IThuocMenu && x.ILoai == ILoai && x.BIsActive == true && x.Id != Model.Id && x.SSoHopDong == Model.SSoHopDong);
            if (hopDongList.Any())
            {
                lstError.Add(Resources.MsgCheckExistSoHopDong);
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return false;
            }

            IEnumerable<NhMstnKeHoachDatHangDanhMucModel> rowChilds = ItemsKeHoachDatHangDanhMuc.Where(x => x.IsChecked && !ItemsKeHoachDatHangDanhMuc.Where(y => y.IID_ParentID == x.Id).Any());
            List<string> fieldNames = new List<string>();
            foreach (var row in rowChilds)
            {
                fieldNames.Clear();
                if (string.IsNullOrWhiteSpace(row.STenDanhMuc))
                {
                    fieldNames.Add("Tên danh mục");
                }
                if (row.ISoLuong == null || !row.ISoLuong.HasValue || row.ISoLuong.Value == 0)
                {
                    fieldNames.Add("Số lượng");
                }
                if (row.FDonGia_VND == null || !row.FDonGia_VND.HasValue || row.FDonGia_VND.Value == 0)
                {
                    fieldNames.Add("Đơn giá (VND)");
                }
                if (fieldNames.Count() > 0)
                {
                    lstError.Add("Dòng [" + row.SMaDanhMuc + "]: " + string.Join(", ", fieldNames) + " chưa được nhập!");
                    break;
                }
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
            var view = obj as Window;
            if (view != null) view.Close();
        }
    }
}
