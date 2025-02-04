using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using log4net;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexAllocation.ForexPheDuyetThanhToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.Windows.Controls;
using VTS.QLNS.CTC.Core.Service.Impl;
using System.Net;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexPheDuyetThanhToan
{
    public class ForexPheDuyetThanhToanDialogViewModel : DialogCurrencyAttachmentViewModelBase<NhTtThanhToanModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _iNsDonViService;
        private readonly INhKhTongTheService _iNhKhTongTheService;
        private readonly INhDmNhiemVuChiService _iDmNhiemVuChiService;
        private readonly IDmChuDauTuService _iDmChuDauTuService;
        private readonly INhDaHopDongService _iNhDaHopDongService;
        private readonly INsNguonNganSachService _iNguonNganSachService;
        private readonly INhDmNhaThauService _iNhDmNhaThauService;
        private readonly INsMucLucNganSachService _iNsMucLucNganSachService;
        private readonly INhTtThanhToanService _iNhTtThanhToanService;
        private readonly INhTtThanhToanChiTietService _iNhTtThanhToanChiTietService;
        private readonly INhDaDuAnService _iNhDaDuAnService;
        private readonly INhThTongHopService _nhThTongHopService;
        private readonly INhDaQuyetDinhKhacService _nhDaQuyetDinhKhacService;

        private static Dictionary<string, Guid> _dicMucLucNganSach = new Dictionary<string, Guid>();

        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.NH_PHE_DUYET_THANH_TOAN;
        public override Type ContentType => typeof(ForexPheDuyetThanhToanDialog);
        public bool IsDetail { get; set; }
        public bool IsEdit { get; set; }
        public bool IsHiddenDuAn { get; set; }
        public bool IsHiddenHopDong { get; set; }
        public bool IsHiddenChiPhiQdk { get; set; }


        private ObservableCollection<DonViModel> _itemsDonViCapTren;
        public ObservableCollection<DonViModel> ItemsDonViCapTren
        {
            get => _itemsDonViCapTren;
            set => SetProperty(ref _itemsDonViCapTren, value);
        }

        private DonViModel _selectedDonViCapTren;
        public DonViModel SelectedDonViCapTren
        {
            get => _selectedDonViCapTren;
            set
            {
                SetProperty(ref _selectedDonViCapTren, value);
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
                LoadNhiemVuChi();
                LoadThongTinLuyKe();
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
                SetProperty(ref _selectedKeHoachTongThe, value);
                LoadNhiemVuChi();
            }
        }

        private ObservableCollection<NhDmNhiemVuChiModel> _itemsNhiemVuChi;
        public ObservableCollection<NhDmNhiemVuChiModel> ItemsNhiemVuChi
        {
            get => _itemsNhiemVuChi;
            set => SetProperty(ref _itemsNhiemVuChi, value);
        }

        private NhDmNhiemVuChiModel _selectedNhiemVuChi;
        public NhDmNhiemVuChiModel SelectedNhiemVuChi
        {
            get => _selectedNhiemVuChi;
            set
            {
                SetProperty(ref _selectedNhiemVuChi, value);
                LoadDuAnHopDong();
                LoadDADuAn();
                LoadThongTinLuyKe();
            }
        }

        private ObservableCollection<DmChuDauTuModel> _itemsChuDauTu;
        public ObservableCollection<DmChuDauTuModel> ItemsChuDauTu
        {
            get => _itemsChuDauTu;
            set => SetProperty(ref _itemsChuDauTu, value);
        }

        private DmChuDauTuModel _selectedChuDauTu;
        public DmChuDauTuModel SelectedChuDauTu
        {
            get => _selectedChuDauTu;
            set
            {
                SetProperty(ref _selectedChuDauTu, value);
                LoadDADuAn();
                LoadThongTinLuyKe();
            }
        }

        private ObservableCollection<NhDaHopDongModel> _itemsDaHopDong;
        public ObservableCollection<NhDaHopDongModel> ItemsDaHopDong
        {
            get => _itemsDaHopDong;
            set => SetProperty(ref _itemsDaHopDong, value);
        }

        private NhDaHopDongModel _selectedDaHopDong;
        public NhDaHopDongModel SelectedDaHopDong
        {
            get => _selectedDaHopDong;
            set
            {
                SetProperty(ref _selectedDaHopDong, value);
                SetGiaTriHopDongDuocDuyet();
                //SetGiaTriKinhPhiKyTruoc();
                LoadThongTinLuyKe();
                if (value != null)
                {
                    LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                    CalculateDataDeNghiThanhToanTongHop();
                }
            }
        }

        private ObservableCollection<NhDaDuAnModel> _itemsDaDuAn;
        public ObservableCollection<NhDaDuAnModel> ItemsDaDuAn
        {
            get => _itemsDaDuAn;
            set => SetProperty(ref _itemsDaDuAn, value);
        }

        private NhDaDuAnModel _selectedDaDuAn;
        public NhDaDuAnModel SelectedDaDuAn
        {
            get => _selectedDaDuAn;
            set
            {
                SetProperty(ref _selectedDaDuAn, value);
                LoadThongTinLuyKe();
                if (value != null)
                {
                    LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                    CalculateDataDeNghiThanhToanTongHop();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiDeNghi;
        public ObservableCollection<ComboboxItem> ItemsLoaiDeNghi
        {
            get => _itemsLoaiDeNghi;
            set => SetProperty(ref _itemsLoaiDeNghi, value);
        }

        private ComboboxItem _selectedLoaiDeNghi;
        public ComboboxItem SelectedLoaiDeNghi
        {
            get => _selectedLoaiDeNghi;
            set
            {
                SetProperty(ref _selectedLoaiDeNghi, value);
                if (value != null)
                {
                    LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                    CalculateDataDeNghiThanhToanTongHop();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsNamNganSach;
        public ObservableCollection<ComboboxItem> ItemsNamNganSach
        {
            get => _itemsNamNganSach;
            set => SetProperty(ref _itemsNamNganSach, value);
        }

        private ComboboxItem _selectedNamNganSach;
        public ComboboxItem SelectedNamNganSach
        {
            get => _selectedNamNganSach;
            set
            {
                SetProperty(ref _selectedNamNganSach, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsCoQuanThanhToan;
        public ObservableCollection<ComboboxItem> ItemsCoQuanThanhToan
        {
            get => _itemsCoQuanThanhToan;
            set => SetProperty(ref _itemsCoQuanThanhToan, value);
        }
        private ObservableCollection<ComboboxItem> _itemsThanhToanTheo;
        public ObservableCollection<ComboboxItem> ItemsThanhToanTheo
        {
            get => _itemsThanhToanTheo;
            set => SetProperty(ref _itemsThanhToanTheo, value);
        }
        public ComboboxItem _selectedThanhToanTheo;
        public ComboboxItem SelectedThanhToanTheo
        {
            get => _selectedThanhToanTheo;
            set
            {
                SetProperty(ref _selectedThanhToanTheo, value);
                LoadDAHDTheoThanhToan();
                if (value != null)
                {
                    LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                    CalculateDataDeNghiThanhToanTongHop();
                }

            }
        }

        private ComboboxItem _selectedCoQuanThanhToan;
        public ComboboxItem SelectedCoQuanThanhToan
        {
            get => _selectedCoQuanThanhToan;
            set
            {
                SetProperty(ref _selectedCoQuanThanhToan, value);
            }
        }

        private ObservableCollection<NguonNganSachModel> _itemsNguonVon;
        public ObservableCollection<NguonNganSachModel> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private NguonNganSachModel _selectedNguonVon;
        public NguonNganSachModel SelectedNguonVon
        {
            get => _selectedNguonVon;
            set
            {
                SetProperty(ref _selectedNguonVon, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiNoiDungChi;
        public ObservableCollection<ComboboxItem> ItemsLoaiNoiDungChi
        {
            get => _itemsLoaiNoiDungChi;
            set => SetProperty(ref _itemsLoaiNoiDungChi, value);
        }

        private ComboboxItem _selectedLoaiNoiDungChi;
        public ComboboxItem SelectedLoaiNoiDungChi
        {
            get => _selectedLoaiNoiDungChi;
            set
            {
                SetProperty(ref _selectedLoaiNoiDungChi, value);
                LoadLabel();
                QuiDoiTienKhiDoiLoaiNoiDungChi();
                LoadValueLoaiNoiDungChi();
                LoadTiLeThanhToanByNoiDungChi();
                if (value != null)
                {
                    LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                    CalculateDataDeNghiThanhToanTongHop();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsQuyKeHoach;
        public ObservableCollection<ComboboxItem> ItemsQuyKeHoach
        {
            get => _itemsQuyKeHoach;
            set => SetProperty(ref _itemsQuyKeHoach, value);
        }

        private ComboboxItem _selectedQuyKeHoach;
        public ComboboxItem SelectedQuyKeHoach
        {
            get => _selectedQuyKeHoach;
            set
            {
                SetProperty(ref _selectedQuyKeHoach, value);
            }
        }

        private ObservableCollection<NhDmNhaThauModel> _itemsDmNhaThau;
        public ObservableCollection<NhDmNhaThauModel> ItemsDmNhaThau
        {
            get => _itemsDmNhaThau;
            set => SetProperty(ref _itemsDmNhaThau, value);
        }

        private NhDmNhaThauModel _selectedDmNhaThau;
        public NhDmNhaThauModel SelectedDmNhaThau
        {
            get => _selectedDmNhaThau;
            set
            {
                SetProperty(ref _selectedDmNhaThau, value);
                if (!IsFirstShow)
                {
                    LoadDataTheoDonviThuHuong();
                }
                else
                {
                    IsFirstShow = false;
                }
            }
        }

        private ObservableCollection<NhDaQuyetDinhKhac> _itemsChiPhiKhac;
        public ObservableCollection<NhDaQuyetDinhKhac> ItemsChiPhiKhac
        {
            get => _itemsChiPhiKhac;
            set => SetProperty(ref _itemsChiPhiKhac, value);
        }

        private NhDaQuyetDinhKhac _selectedChiPhiKhac;
        public NhDaQuyetDinhKhac SelectedChiPhiKhac
        {
            get => _selectedChiPhiKhac;
            set
            {
                SetProperty(ref _selectedChiPhiKhac, value);
                //SetGiaTriHopDongDuocDuyet();
                //SetGiaTriKinhPhiKyTruoc();
                LoadThongTinLuyKe();
                LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                CalculateDataDeNghiThanhToanTongHop();
            }
        }

        private bool _isUsd;
        public bool IsUsd
        {
            get => _isUsd;
            set => SetProperty(ref _isUsd, value);
        }

        private ObservableCollection<NhTtThanhToanChiTietModel> _itemsNhTtThanhToanChiTiet;
        public ObservableCollection<NhTtThanhToanChiTietModel> ItemsNhTtThanhToanChiTiet
        {
            get => _itemsNhTtThanhToanChiTiet;
            set => SetProperty(ref _itemsNhTtThanhToanChiTiet, value);
        }

        private NhTtThanhToanChiTietModel _selectedNhTtThanhToanChiTiet;
        public NhTtThanhToanChiTietModel SelectedNhTtThanhToanChiTiet
        {
            get => _selectedNhTtThanhToanChiTiet;
            set => SetProperty(ref _selectedNhTtThanhToanChiTiet, value);
        }

        private NhDmTiGiaModel _selectedTiGiaPheDuyet;
        public NhDmTiGiaModel SelectedTiGiaPheDuyet
        {
            get => _selectedTiGiaPheDuyet;
            set
            {
                SetProperty(ref _selectedTiGiaPheDuyet, value);
                LoadTiGiaChiTietPheDuyet();
            }
        }

        private ObservableCollection<NhDmTiGiaChiTietModel> _itemsTiGiaChiTietPheDuyet;
        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTietPheDuyet
        {
            get => _itemsTiGiaChiTietPheDuyet;
            set => SetProperty(ref _itemsTiGiaChiTietPheDuyet, value);
        }

        public string SoDuTamUngLabel { get; set; }
        public bool IsFirstShow { get; set; }
        public IEnumerable<NHTHTongHop> ItemsTongHops;

        public SelectMLNSDialogViewModel SelectMLNSDialogViewModel { get; set; }

        public RelayCommand AddThongTinThanhToanCommand { get; set; }
        public RelayCommand DeleteThongTinThanhToanCommand { get; set; }
        public RelayCommand SelectMLNSCommand { get; set; }

        public ForexPheDuyetThanhToanDialogViewModel(
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            IMapper mapper,
            ILog logger,
            ISessionService sessionService,
            INsDonViService iNsDonViService,
            INhKhTongTheService iNhKhTongTheService,
            INhDmNhiemVuChiService iDmNhiemVuChiService,
            IDmChuDauTuService iDmChuDauTuService,
            INhDaHopDongService iNhDaHopDongService,
            INhDaDuAnService iNhDaDuAnService,
            INsNguonNganSachService iNguonNganSachService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            INhDmNhaThauService iNhDmNhaThauService,
            INsMucLucNganSachService iNsMucLucNganSachService,
            INhThTongHopService nhThTongHopService,
            INhTtThanhToanService iNhTtThanhToanService,
            SelectMLNSDialogViewModel selectMLNSDialogViewModel,
            INhTtThanhToanChiTietService iNhTtThanhToanChiTietService,
            INhDaQuyetDinhKhacService nhDaQuyetDinhKhacService) : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _iNsDonViService = iNsDonViService;
            _iNhKhTongTheService = iNhKhTongTheService;
            _iDmNhiemVuChiService = iDmNhiemVuChiService;
            _iDmChuDauTuService = iDmChuDauTuService;
            _iNhDaHopDongService = iNhDaHopDongService;
            _iNhDaDuAnService = iNhDaDuAnService;
            _iNguonNganSachService = iNguonNganSachService;
            _iNhDmNhaThauService = iNhDmNhaThauService;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;
            _nhThTongHopService = nhThTongHopService;
            _iNhTtThanhToanService = iNhTtThanhToanService;
            _iNhTtThanhToanChiTietService = iNhTtThanhToanChiTietService;
            _nhDaQuyetDinhKhacService = nhDaQuyetDinhKhacService;

            SelectMLNSDialogViewModel = selectMLNSDialogViewModel;

            AddThongTinThanhToanCommand = new RelayCommand(obj => OnAddThongTinThanhToanDetail());
            DeleteThongTinThanhToanCommand = new RelayCommand(obj => OnDeleteThongTinThanhToanDetail());
            SelectMLNSCommand = new RelayCommand(obj => OnSelectMLNS(obj));
        }

        public override void Init()
        {
            base.Init();
            LoadLabel();
            LoadDonViCapTren();
            LoadDonVi();
            LoadKeHoachTongThe();
            LoadChuDauTu();
            LoadNhiemVuChi();
            LoadDuAnHopDong();
            LoadDADuAn();
            LoadLoaiDeNghi();
            LoadNguonVon();
            LoadItemsTiGia();
            LoadTiGiaChiTiet();
            LoadTiGiaChiTietPheDuyet();
            LoadAttach();
            LoadLoaiNoiDungChi();
            LoadValueLoaiNoiDungChi();
            LoadCoQuanThanhToan();
            LoadThanhToanTheo();
            LoadDmNhaThau();
            LoadNamNganSach();
            LoadQuyKeHoach();
            LoadData();
        }

        private void LoadLabel()
        {
            SoDuTamUngLabel = "Số dư tạm ứng đề nghị thanh toán";
            if (_selectedLoaiNoiDungChi != null)
            {
                if (_selectedLoaiNoiDungChi.ValueItem.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()))
                {
                    SoDuTamUngLabel = "Số dư tạm ứng đề nghị thanh toán (USD)";
                }
                else
                {
                    SoDuTamUngLabel = "Số dư tạm ứng đề nghị thanh toán (VND)";
                }
            }
            OnPropertyChanged(nameof(SoDuTamUngLabel));
        }

        private void QuiDoiTienKhiDoiLoaiNoiDungChi()
        {
            Model.PropertyChanged -= ThanhToan_PropertyChanged;
            if (_selectedLoaiNoiDungChi != null &&
                _selectedLoaiNoiDungChi.ValueItem.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE).ToString()))
            {
                Model.FSoDuTamUng = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.VND,
                        LoaiTienTeEnum.TypeCode.USD, Model.FSoDuTamUng);
                Model.FTongDeNghiBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.VND,
                    LoaiTienTeEnum.TypeCode.USD, Model.FTongDeNghiBangSo);
                Model.FThuHoiTamUngBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.VND,
                    LoaiTienTeEnum.TypeCode.USD, Model.FThuHoiTamUngBangSo);
                Model.FTraDonViThuHuongBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.VND,
                    LoaiTienTeEnum.TypeCode.USD, Model.FTraDonViThuHuongBangSo);
                Model.FChuyenKhoanBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.VND,
                    LoaiTienTeEnum.TypeCode.USD, Model.FChuyenKhoanBangSo);
                Model.FTienMatBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.VND,
                    LoaiTienTeEnum.TypeCode.USD, Model.FTienMatBangSo);
                Model.FTongPheDuyetBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.VND,
                    LoaiTienTeEnum.TypeCode.USD, Model.FTongPheDuyetBangSo);
                Model.FThuHoiTamUngPheDuyetBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.VND,
                    LoaiTienTeEnum.TypeCode.USD, Model.FThuHoiTamUngPheDuyetBangSo);
                Model.FTraDonViThuHuongPheDuyetBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.VND,
                    LoaiTienTeEnum.TypeCode.USD, Model.FTraDonViThuHuongPheDuyetBangSo);
                Model.FTuChoiThanhToanBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.VND,
                    LoaiTienTeEnum.TypeCode.USD, Model.FTuChoiThanhToanBangSo);
            }
            else
            {
                Model.FSoDuTamUng = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.USD,
                    LoaiTienTeEnum.TypeCode.VND, Model.FSoDuTamUng);
                Model.FTongDeNghiBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.USD,
                    LoaiTienTeEnum.TypeCode.VND, Model.FTongDeNghiBangSo);
                Model.FThuHoiTamUngBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.USD,
                    LoaiTienTeEnum.TypeCode.VND, Model.FThuHoiTamUngBangSo);
                Model.FTraDonViThuHuongBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.USD,
                    LoaiTienTeEnum.TypeCode.VND, Model.FTraDonViThuHuongBangSo);
                Model.FChuyenKhoanBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.USD,
                    LoaiTienTeEnum.TypeCode.VND, Model.FChuyenKhoanBangSo);
                Model.FTienMatBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.USD,
                    LoaiTienTeEnum.TypeCode.VND, Model.FTienMatBangSo);
                Model.FTongPheDuyetBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.USD,
                    LoaiTienTeEnum.TypeCode.VND, Model.FTongPheDuyetBangSo);
                Model.FThuHoiTamUngPheDuyetBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.USD,
                    LoaiTienTeEnum.TypeCode.VND, Model.FThuHoiTamUngPheDuyetBangSo);
                Model.FTraDonViThuHuongPheDuyetBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.USD,
                    LoaiTienTeEnum.TypeCode.VND, Model.FTraDonViThuHuongPheDuyetBangSo);
                Model.FTuChoiThanhToanBangSo = ExchangeCurrencyTheoNoiDungChi(LoaiTienTeEnum.TypeCode.USD,
                    LoaiTienTeEnum.TypeCode.VND, Model.FTuChoiThanhToanBangSo);
            }
            if (_selectedLoaiNoiDungChi != null)
                GenerateTienBangChu(_selectedLoaiNoiDungChi.ValueItem);
            Model.PropertyChanged += ThanhToan_PropertyChanged;
        }

        private void LoadDataTheoDonviThuHuong()
        {
            Model.PropertyChanged -= ThanhToan_PropertyChanged;
            if (_selectedDmNhaThau != null && !IsDetail)
            {
                Model.SSoTaiKhoan = _iNhDmNhaThauService.FindById(_selectedDmNhaThau.Id).SSoTaiKhoan;
                Model.SNganHang = _iNhDmNhaThauService.FindById(_selectedDmNhaThau.Id).SNganHang;
                Model.SNguoiLienHe = _iNhDmNhaThauService.FindById(_selectedDmNhaThau.Id).SNguoiLienHe;
                Model.SSoCmnd = _iNhDmNhaThauService.FindById(_selectedDmNhaThau.Id).SSoCmnd;
                Model.SNoiCapCmnd = _iNhDmNhaThauService.FindById(_selectedDmNhaThau.Id).SNoiCapCmnd; 
                Model.DNgayCapCmnd = _iNhDmNhaThauService.FindById(_selectedDmNhaThau.Id).DNgayCapCmnd;
            }
            Model.PropertyChanged += ThanhToan_PropertyChanged;
        }

        private void GenerateTienBangChu(string nt)
        {
            if (nt != null &&
                nt.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()))
            {
                Model.STongDeNghiBangChu = StringUtils.NumberToText(Model.FTongDeNghiBangSo.GetValueOrDefault(),false);
                Model.FThuHoiTamUngBangChu = StringUtils.NumberToText(Model.FThuHoiTamUngBangSo.GetValueOrDefault(), false);
                Model.FTraDonViThuHuongBangChu = StringUtils.NumberToText(Model.FTraDonViThuHuongBangSo.GetValueOrDefault(), false);
                Model.SChuyenKhoanBangChu = StringUtils.NumberToText(Model.FChuyenKhoanBangSo.GetValueOrDefault(), false);
                Model.STienMatBangChu = StringUtils.NumberToText(Model.FTienMatBangSo.GetValueOrDefault(), false);
                Model.STongPheDuyetBangChu = StringUtils.NumberToText(Model.FTongPheDuyetBangSo.GetValueOrDefault(), false);
                Model.FThuHoiTamUngPheDuyetBangChu = StringUtils.NumberToText(Model.FThuHoiTamUngPheDuyetBangSo.GetValueOrDefault(), false);
                Model.FTraDonViThuHuongPheDuyetBangChu = StringUtils.NumberToText(Model.FTraDonViThuHuongPheDuyetBangSo.GetValueOrDefault(), false);
            }
            else
            {
                Model.STongDeNghiBangChu = StringUtils.NumberToText(Model.FTongDeNghiBangSo.GetValueOrDefault());
                Model.FThuHoiTamUngBangChu = StringUtils.NumberToText(Model.FThuHoiTamUngBangSo.GetValueOrDefault());
                Model.FTraDonViThuHuongBangChu = StringUtils.NumberToText(Model.FTraDonViThuHuongBangSo.GetValueOrDefault());
                Model.SChuyenKhoanBangChu = StringUtils.NumberToText(Model.FChuyenKhoanBangSo.GetValueOrDefault());
                Model.STienMatBangChu = StringUtils.NumberToText(Model.FTienMatBangSo.GetValueOrDefault());
                Model.STongPheDuyetBangChu = StringUtils.NumberToText(Model.FTongPheDuyetBangSo.GetValueOrDefault());
                Model.FThuHoiTamUngPheDuyetBangChu = StringUtils.NumberToText(Model.FThuHoiTamUngPheDuyetBangSo.GetValueOrDefault());
                Model.FTraDonViThuHuongPheDuyetBangChu = StringUtils.NumberToText(Model.FTraDonViThuHuongPheDuyetBangSo.GetValueOrDefault());
            }
        }

        private double? ExchangeCurrencyTheoNoiDungChi(string sourceCurrency, string destCurrency, double? value)
        {
            if (SelectedTiGia != null)
            {
                var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTietPheDuyet);
                string rootCurrency = SelectedTiGiaPheDuyet != null ? SelectedTiGiaPheDuyet.SMaTienTeGoc : string.Empty;
                return _nhDmTiGiaService.CurrencyExchange(sourceCurrency, destCurrency, rootCurrency, listTiGiaChiTiet, value.GetValueOrDefault());
            }
            return value;
        }

        private void LoadDonViCapTren()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            var lstDonVi = _iNsDonViService.FindByCondition(predicate).ToList();
            _itemsDonViCapTren = _mapper.Map<ObservableCollection<DonViModel>>(lstDonVi);
        }

        private void LoadDonVi()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            var lstDonVi = _iNsDonViService.FindByCondition(predicate).ToList();
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(lstDonVi);
        }

        private void LoadKeHoachTongThe()
        {
            var predicate = PredicateBuilder.True<NhKhTongThe>();
            predicate = predicate.And(x => true.Equals(x.BIsActive));
            var lstKeHoachTongThe = _iNhKhTongTheService.FindAll(predicate).ToList();
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
                        cb.DisplayItem = "KHTT " + x.IGiaiDoanTu_BQP.GetValueOrDefault() + "-" + x.IGiaiDoanDen_BQP.GetValueOrDefault() + "- Số KH: " + x.SSoKeHoachBqp;
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
        }

        private void LoadChuDauTu()
        {
            var predicate = PredicateBuilder.True<DmChuDauTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            var lstChuDauTu = _iDmChuDauTuService.FindByCondition(predicate).ToList();
            _itemsChuDauTu = new ObservableCollection<DmChuDauTuModel>(_mapper.Map<List<DmChuDauTuModel>>(lstChuDauTu));
        }

        private void LoadNhiemVuChi()
        {
            ItemsNhiemVuChi = new ObservableCollection<NhDmNhiemVuChiModel>();
            if (_selectedKeHoachTongThe != null && _selectedDonVi != null)
            {
                //var lstIdKhTongTheNvChiCoHopDong = GetListIdKhTongTheNhiemVuChiCoHopDong();
                var lstKHNhiemVuChi = GetListIdKhTongTheNhiemVuChi();
                var lstNhiemVuChi = _iDmNhiemVuChiService.FindAll().Where(x => lstKHNhiemVuChi.Select(y => y.Item1).Contains(x.Id)).ToList();
                ItemsNhiemVuChi = new ObservableCollection<NhDmNhiemVuChiModel>(_mapper.Map<List<NhDmNhiemVuChiModel>>(lstNhiemVuChi));
                if (!ItemsNhiemVuChi.IsEmpty())
                {
                    ItemsNhiemVuChi.Select(x =>
                    {
                        if (lstKHNhiemVuChi.Select(y => y.Item1).Any(a => a.Equals(x.Id)))
                        {
                            x.IIdKHTTNhiemVuChiId = lstKHNhiemVuChi.FirstOrDefault(z => z.Item1.Equals(x.Id)).Item2;
                            x.IID_KHTT_NhiemVuChiID = lstKHNhiemVuChi.FirstOrDefault(z => z.Item1.Equals(x.Id)).Item2;
                        }
                        return x;
                    }).ToList();
                }
            }
        }

        private List<Guid?> GetListIdKhTongTheNhiemVuChiCoHopDong()
        {
            var predicate = PredicateBuilder.True<NhDaHopDong>();
            predicate = predicate.And(x => x.NhKhTongTheNhiemVuChi.IIdKhTongTheId.ToString().Equals(_selectedKeHoachTongThe.ValueItem));
            predicate = predicate.And(x => x.NhKhTongTheNhiemVuChi.IIdDonViThuHuongId.Equals(_selectedDonVi.Id));
            var lstDaHopDong = _iNhDaHopDongService.FindByCondition(predicate).ToList();
            if (lstDaHopDong.Any())
            {
                return lstDaHopDong.Select(x => x.IIdKhTongTheNhiemVuChiId).ToList();
            }
            return new List<Guid?>();
        }

        private List<Tuple<Guid, Guid, NhKhTongTheNhiemVuChi>> GetListIdKhTongTheNhiemVuChi()
        {
            List<Tuple<Guid, Guid, NhKhTongTheNhiemVuChi>> tuples = new List<Tuple<Guid, Guid, NhKhTongTheNhiemVuChi>>();
            var predicate = PredicateBuilder.True<NhKhTongTheNhiemVuChi>();
            predicate = predicate.And(x => x.IIdKhTongTheId.ToString().Equals(_selectedKeHoachTongThe.ValueItem));
            predicate = predicate.And(x => x.IIdDonViThuHuongId.Equals(_selectedDonVi.Id));
            var lstKHNhiemVuChi = _iNhKhTongTheService.FindKHTongTheNVCByConditon(predicate);
            if (lstKHNhiemVuChi.Any())
            {
                tuples = lstKHNhiemVuChi.Select(x => new Tuple<Guid, Guid, NhKhTongTheNhiemVuChi>
                (
                    x.IIdNhiemVuChiId,
                    x.Id,
                    x
                )).ToList();
            }

            return tuples;
        }

        private void LoadLoaiDeNghi()
        {
            var results = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem(NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.CAP_KINH_PHI), ((int)NhLoaiDeNghi.Type.CAP_KINH_PHI).ToString()),
                new ComboboxItem(NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.TAM_UNG_KINH_PHI), ((int)NhLoaiDeNghi.Type.TAM_UNG_KINH_PHI).ToString()),
                new ComboboxItem(NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.THANH_TOAN), ((int)NhLoaiDeNghi.Type.THANH_TOAN).ToString()),
                new ComboboxItem(NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.TAM_UNG_THEO_CHE_DO), ((int)NhLoaiDeNghi.Type.TAM_UNG_THEO_CHE_DO).ToString())
            };
            _itemsLoaiDeNghi = results;
        }

        private void LoadNamNganSach()
        {
            var results = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem("Năm nay", "1"),
                new ComboboxItem("Năm trước chuyển sang", "2")
            };
            _itemsNamNganSach = results;
            _selectedNamNganSach = Model.Id == Guid.Empty ? _itemsNamNganSach[1] : _selectedNamNganSach;
        }

        private void LoadNguonVon()
        {
            var lstNguonNs = _iNguonNganSachService.FindAll().Where(x => x.IIdMaNguonNganSach == (int)NSNguonNganSachEnum.Type.NGAN_SACH_DB).ToList();
            _itemsNguonVon = _mapper.Map<ObservableCollection<NguonNganSachModel>>(lstNguonNs);
            SelectedNguonVon = _itemsNguonVon.FirstOrDefault();
        }

        private void LoadItemsTiGia()
        {
            LoadTiGia();
            ItemsTiGia = new ObservableCollection<NhDmTiGiaModel>(ItemsTiGia.OrderByDescending(x => x.DNgayTao));
        }

        private void LoadLoaiNoiDungChi()
        {
            var results = new ObservableCollection<ComboboxItem>();
            results.Add(new ComboboxItem(LoaiNoiDungChi.Get((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE), ((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()));
            results.Add(new ComboboxItem(LoaiNoiDungChi.Get((int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE), ((int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE).ToString()));
            _itemsLoaiNoiDungChi = results;
            _selectedLoaiNoiDungChi = Model.Id == Guid.Empty ? _itemsLoaiNoiDungChi[1] : _selectedLoaiNoiDungChi;
        }

        public void LoadTiGiaChiTietPheDuyet()
        {
            _itemsTiGiaChiTietPheDuyet = new ObservableCollection<NhDmTiGiaChiTietModel>();
            if (SelectedTiGiaPheDuyet != null)
            {
                var data = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGiaPheDuyet.Id);
                _itemsTiGiaChiTietPheDuyet = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsTiGiaChiTietPheDuyet));
        }

        private void LoadCoQuanThanhToan()
        {
            var results = new ObservableCollection<ComboboxItem>();
            results.Add(new ComboboxItem(NhCoQuanThanhToan.Get((int)NhCoQuanThanhToan.Type.CTC_CAP), ((int)NhCoQuanThanhToan.Type.CTC_CAP).ToString()));
            results.Add(new ComboboxItem(NhCoQuanThanhToan.Get((int)NhCoQuanThanhToan.Type.DON_VI_CAP), ((int)NhCoQuanThanhToan.Type.DON_VI_CAP).ToString()));
            _itemsCoQuanThanhToan = results;
        }

        private void LoadDmNhaThau()
        {
            var results = _iNhDmNhaThauService.FindAll();
            _itemsDmNhaThau = new ObservableCollection<NhDmNhaThauModel>(_mapper.Map<ObservableCollection<NhDmNhaThauModel>>(results));
        }

        private void LoadQuyKeHoach()
        {
            var quyKeHoachs = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem(LoaiQuyEnum.TypeName.QUY_1, ((int)LoaiQuyEnum.Type.QUY_1).ToString()),
                new ComboboxItem(LoaiQuyEnum.TypeName.QUY_2, ((int)LoaiQuyEnum.Type.QUY_2).ToString()),
                new ComboboxItem(LoaiQuyEnum.TypeName.QUY_3, ((int)LoaiQuyEnum.Type.QUY_3).ToString()),
                new ComboboxItem(LoaiQuyEnum.TypeName.QUY_4, ((int)LoaiQuyEnum.Type.QUY_4).ToString())
            };
            _itemsQuyKeHoach = quyKeHoachs;
        }

        public override void LoadData(params object[] args)
        {
            GetDicMucLucNganSach();
            if (Model.Id.IsNullOrEmpty())
            {
                Title = "PHÊ DUYỆT CẤP PHÁT, THANH TOÁN";
                Description = "Thêm mới thông tin phê duyệt cấp phát, thanh toán kinh phí nguồn quỹ dự trữ ngoại hối";
            }
            else
            {
                NhTtThanhToan entity = _iNhTtThanhToanService.FindById(Model.Id);
                Model = _mapper.Map<NhTtThanhToanModel>(entity);
                if (IsDetail)
                {
                    Title = "PHÊ DUYỆT CẤP PHÁT, THANH TOÁN";
                    Description = "Chi tiết thông tin phê duyệt cấp phát, thanh toán kinh phí nguồn quỹ dự trữ ngoại hối";

                }
                else
                {
                    Title = "PHÊ DUYỆT CẤP PHÁT, THANH TOÁN";
                    Description = "Cập nhật thông tin phê duyệt cấp phát, thanh toán kinh phí nguồn quỹ dự trữ ngoại hối";
                }
                IsFirstShow = true;
                SelectedDonViCapTren = ItemsDonViCapTren.FirstOrDefault(x => x.Id == Model.IIdDonViCapTren);
                SelectedDonVi = ItemsDonVi.FirstOrDefault(x => x.Id == Model.IIdDonVi);
                SelectedKeHoachTongThe = ItemsKeHoachTongThe.FirstOrDefault(x => x.ValueItem.ToString().Equals(Model.IIdKhtongTheId.GetValueOrDefault().ToString()));
                SelectedNhiemVuChi = ItemsNhiemVuChi.FirstOrDefault(x => x.Id == Model.IIdNhiemVuChiId);
                SelectedChuDauTu = ItemsChuDauTu.FirstOrDefault(x => x.Id == Model.IIdChuDauTuId);
                SelectedDaHopDong = ItemsDaHopDong.FirstOrDefault(x => x.Id == Model.IIdHopDongId);
                SelectedLoaiDeNghi = ItemsLoaiDeNghi.FirstOrDefault(x => x.ValueItem.Equals(Model.ILoaiDeNghi.ToString()));
                SelectedNguonVon = ItemsNguonVon.FirstOrDefault(x => x.IIdMaNguonNganSach == Model.IIdNguonVonId);
                SelectedDmNhaThau = ItemsDmNhaThau.FirstOrDefault(x => x.Id == Model.IIdNhaThauId);
                SelectedLoaiNoiDungChi = ItemsLoaiNoiDungChi.FirstOrDefault(x => x.ValueItem.Equals(Model.ILoaiNoiDungChi.GetValueOrDefault().ToString()));
                SelectedCoQuanThanhToan = ItemsCoQuanThanhToan.FirstOrDefault(x => x.ValueItem.Equals(Model.ICoQuanThanhToan.GetValueOrDefault().ToString()));
                SelectedThanhToanTheo = ItemsThanhToanTheo.FirstOrDefault(x => x.ValueItem.Equals(Model.IThanhToanTheo.GetValueOrDefault().ToString()));
                SelectedDaDuAn = ItemsDaDuAn.FirstOrDefault(x => x.Id == Model.IIdDuAnId);
                SelectedNamNganSach = ItemsNamNganSach.FirstOrDefault(x => x.ValueItem.Equals(Model.INamNganSach.GetValueOrDefault().ToString()));
                SelectedQuyKeHoach = ItemsQuyKeHoach.FirstOrDefault(x => x.ValueItem.Equals(Model.IQuyKeHoach.GetValueOrDefault().ToString()));
                SelectedChiPhiKhac = ItemsChiPhiKhac.IsEmpty() ? null : ItemsChiPhiKhac.FirstOrDefault(x => x.Id.Equals(Model.IIdQuyetDinhKhacId));
                // Load tỉ giá và ngoại tệ đề nghị
                SelectedTiGia = ItemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
                LoadTiGiaChiTiet();
                SelectedTiGiaChiTiet = ItemsTiGiaChiTiet.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));

                // Load tỉ giá và ngoại tệ phê duyệt
                SelectedTiGiaPheDuyet = ItemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaPheDuyetId);
                LoadDAHDTheoThanhToan();
            }

            LoadThanhToanChiTiet();
            //SetGiaTriKinhPhiKyTruoc();
            LoadThongTinLuyKe();
            Model.PropertyChanged += ThanhToan_PropertyChanged;
        }

        public override void OnSave()
        {
            ConvertData();
            if (!ValidateViewModelHelper.Validate(Model)) return;
            if (!ValidateData()) return;
            var tiGiaUsdVnd = SelectedTiGia == null ? null : _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id).FirstOrDefault(x => x.SMaTienTeQuyDoi == LoaiTienTeEnum.TypeCode.VND);

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                if (SelectedLoaiNoiDungChi.ValueItem == ((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString())
                {
                    Model.FTongPheDuyetBangSoUSD = Model.FTongDeNghiBangSo;
                    Model.FTongPheDuyetBangSoVND = Model.FTongDeNghiBangSo * (tiGiaUsdVnd == null ? 0 : tiGiaUsdVnd.FTiGia);
                    Model.FThuhoiTamUngPheDuyetBangSoUSD = Model.FThuHoiTamUngPheDuyetBangSo;
                    Model.FThuhoiTamUngPheDuyetBangSoVND = Model.FThuHoiTamUngPheDuyetBangSo * (tiGiaUsdVnd == null ? 0 : tiGiaUsdVnd.FTiGia);
                    Model.FTraDonViThuHuongPheDuyetBangSoUSD = Model.FTraDonViThuHuongBangSo;
                    Model.FTraDonViThuHuongPheDuyetBangSoVND = Model.FTraDonViThuHuongBangSo * (tiGiaUsdVnd == null ? 0 : tiGiaUsdVnd.FTiGia);
                }
                else
                {
                    Model.FTongPheDuyetBangSoVND = Model.FTongDeNghiBangSo;
                    Model.FTongPheDuyetBangSoUSD = tiGiaUsdVnd == null ? 0 : Model.FTongDeNghiBangSo / tiGiaUsdVnd.FTiGia;
                    Model.FThuhoiTamUngPheDuyetBangSoVND = Model.FThuHoiTamUngPheDuyetBangSo;
                    Model.FThuhoiTamUngPheDuyetBangSoUSD = tiGiaUsdVnd == null ? 0 : Model.FThuHoiTamUngPheDuyetBangSo / tiGiaUsdVnd.FTiGia;
                    Model.FTraDonViThuHuongPheDuyetBangSoVND = tiGiaUsdVnd == null ? 0 : Model.FTraDonViThuHuongBangSo / tiGiaUsdVnd.FTiGia;
                    Model.FTraDonViThuHuongPheDuyetBangSoUSD = tiGiaUsdVnd == null ? 0 : Model.FTraDonViThuHuongBangSo / tiGiaUsdVnd.FTiGia;
                }
                NhTtThanhToan entity;
                if (Model.Id.IsNullOrEmpty())
                {
                    Model.ITrangThai = 2;
                    entity = _mapper.Map<NhTtThanhToan>(Model);
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    _iNhTtThanhToanService.Add(entity);

                    //if(Model.ILoaiDeNghi != 2)
                    //{
                    //    _nhThTongHopService.InsertNHTongHop_Tang("TTCP", 1, entity.Id, null);
                    //}
                    //if (Model.ILoaiDeNghi != 1)
                    //{
                    //    _nhThTongHopService.InsertNHTongHop_Giam("TTCP", 1, entity.Id, null);
                    //}
                }
                else
                {
                    entity = _iNhTtThanhToanService.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    entity.ITrangThai = 2;
                    _iNhTtThanhToanService.Update(entity);
                    if(Model.ITrangThai == 2)
                    {
                        //_nhThTongHopService.InsertNHTongHop_Tang("TTCP", 2, entity.Id, null);
                        //if (Model.ILoaiDeNghi != 2)
                        //{
                        //    _nhThTongHopService.InsertNHTongHop_Tang("TTCP", 2, entity.Id, null);
                        //}
                        //if (Model.ILoaiDeNghi != 1)
                        //{
                        //    _nhThTongHopService.InsertNHTongHop_Giam("TTCP", 2, entity.Id, null);
                        //}
                    }
                    if(Model.DNgayPheDuyet.HasValue && Model.ITrangThai == 1)
                    {
                        //_nhThTongHopService.InsertNHTongHop_Tang("TTCP", 1, entity.Id, null);
                        //if (Model.ILoaiDeNghi != 2)
                        //{
                        //    _nhThTongHopService.InsertNHTongHop_Tang("TTCP", 1, entity.Id, null);
                        //}
                        //if (Model.ILoaiDeNghi != 1)
                        //{
                        //    _nhThTongHopService.InsertNHTongHop_Giam("TTCP", 1, entity.Id, null);
                        //}
                        Model.ITrangThai = 2;
                    }

                }

                SaveAttachment(entity.Id);
                e.Result = entity;
            }, (s, e) =>
            {
                IsLoading = false;

                if (e.Error == null)
                {
                    // Reload data
                    Model = _mapper.Map<NhTtThanhToanModel>(e.Result);
                    SavedAction?.Invoke(Model);
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

        public void ConvertData()
        {
            if (SelectedDonVi != null)
            {
                Model.IIdDonVi = SelectedDonVi.Id;
                Model.IIdMaDonVi = SelectedDonVi.IIDMaDonVi;
            }
            if (SelectedKeHoachTongThe != null)
            {
                Model.IIdKhtongTheId = new Guid(SelectedKeHoachTongThe.ValueItem);
            }
            if (SelectedNhiemVuChi != null)
            {
                Model.IIdNhiemVuChiId = SelectedNhiemVuChi.Id;
            }
            if (SelectedChuDauTu != null)
            {
                Model.IIdChuDauTuId = SelectedChuDauTu.Id;
                Model.IIdMaChuDauTu = SelectedChuDauTu.IIDMaDonVi;
            }
            if (SelectedLoaiDeNghi != null)
            {
                Model.ILoaiDeNghi = int.Parse(SelectedLoaiDeNghi.ValueItem);
            }
            if (SelectedNguonVon != null)
            {
                Model.IIdNguonVonId = SelectedNguonVon.IIdMaNguonNganSach;
            }
            if (SelectedTiGia != null)
            {
                Model.IIdTiGiaId = SelectedTiGia.Id;
            }
            if (SelectedTiGiaPheDuyet != null)
            {
                Model.IIdTiGiaPheDuyetId = SelectedTiGiaPheDuyet.Id;
            }
            if (SelectedTiGiaChiTiet != null)
            {
                Model.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
            }
            if (SelectedDmNhaThau != null)
            {
                Model.IIdNhaThauId = SelectedDmNhaThau.Id;
            }
            if (SelectedCoQuanThanhToan != null)
            {
                Model.ICoQuanThanhToan = int.Parse(SelectedCoQuanThanhToan.ValueItem);
            }
            if (SelectedLoaiNoiDungChi != null)
            {
                Model.ILoaiNoiDungChi = int.Parse(SelectedLoaiNoiDungChi.ValueItem);
            }
            if (SelectedThanhToanTheo != null)
            {
                Model.IThanhToanTheo = int.TryParse(SelectedThanhToanTheo.ValueItem, out int idthanhtoan) ? idthanhtoan : (int?)null;
            }
            if (SelectedNamNganSach != null)
            {
                Model.INamNganSach = int.Parse(SelectedNamNganSach.ValueItem);
            }
            if (SelectedQuyKeHoach != null)
            {
                Model.IQuyKeHoach = int.Parse(SelectedQuyKeHoach.ValueItem);
            }
            if (SelectedChiPhiKhac != null)
            {
                Model.IIdQuyetDinhKhacId = SelectedChiPhiKhac.Id;
            }
            //Dự án, hợp đồng
            Model.IIdHopDongId = SelectedDaHopDong != null ? SelectedDaHopDong.Id : (Guid?)null;
            Model.IIdDuAnId = SelectedDaDuAn != null ? SelectedDaDuAn.Id : (Guid?)null;
            var lstChiTietValid = ItemsNhTtThanhToanChiTiet != null
                ? ItemsNhTtThanhToanChiTiet.Where(x => x.IIdMlnsId.HasValue).ToList()
                : new List<NhTtThanhToanChiTietModel>();
            Model.NhTtThanhToanChiTiets = new ObservableCollection<NhTtThanhToanChiTietModel>(lstChiTietValid);
            //Thông tin lũy kế
            Model.FLuyKeVND = Model.NhTtThanhToanChiTiets.Count() > 0 ? Model.NhTtThanhToanChiTiets[0].FDuocCapKyTruocVnd : 0;
            Model.FLuyKeUSD = Model.NhTtThanhToanChiTiets.Count() > 0 ? Model.NhTtThanhToanChiTiets[0].FDuocCapKyTruocUsd : 0;
            Model.FLuyKeEUR = Model.NhTtThanhToanChiTiets.Count() > 0 ? Model.NhTtThanhToanChiTiets[0].FDuocCapKyTruocEur : 0;
            Model.FLuyKeNgoaiTeKhac = Model.NhTtThanhToanChiTiets.Count() > 0  ? Model.NhTtThanhToanChiTiets[0].FDuocCapKyTruocNgoaiTeKhac : 0;
            //Hợp đồng dư toán được duyêt
            Model.FTongPheDuyetVND = Model.NhTtThanhToanChiTiets.Count() > 0 ? Model.NhTtThanhToanChiTiets.FirstOrDefault().FDuocDuyetVnd : 0;
            Model.FTongPheDuyetUSD = Model.NhTtThanhToanChiTiets.Count() > 0 ? Model.NhTtThanhToanChiTiets.FirstOrDefault().FDuocDuyetUsd : 0;
            Model.FTongPheDuyetEUR = Model.NhTtThanhToanChiTiets.Count() > 0 ? Model.NhTtThanhToanChiTiets.FirstOrDefault().FDuocDuyetEur : 0;
            Model.FTongPheDuyetNgoaiTeKhac = Model.NhTtThanhToanChiTiets.Count() > 0 ? Model.NhTtThanhToanChiTiets.FirstOrDefault().FDuocDuyetNgoaiTeKhac : 0;
            //Kinh phí đề nghị cấp
            Model.FTongDeNghiUSD = Model.NhTtThanhToanChiTiets.Count() > 0 ? Model.NhTtThanhToanChiTiets.Sum(x => x.FDeNghiCapKyNayUsd) : 0;
            Model.FTongDeNghiVND = Model.NhTtThanhToanChiTiets.Count() > 0 ? Model.NhTtThanhToanChiTiets.Sum(x => x.FDeNghiCapKyNayVnd) : 0;
            Model.FTongDeNghiEUR = Model.NhTtThanhToanChiTiets.Count() > 0 ? Model.NhTtThanhToanChiTiets.Sum(x => x.FDeNghiCapKyNayEur) : 0;
            Model.FTongDeNghiNgoaiTeKhac = Model.NhTtThanhToanChiTiets.Count() > 0 ? Model.NhTtThanhToanChiTiets.Sum(x => x.FDeNghiCapKyNayNgoaiTeKhac) : 0;
        }

        private bool ValidateData()
        {
            List<string> lstError = new List<string>();
            //if (SelectedDonVi == null)
            //{
            //    lstError.Add(Resources.MsgCheckDonVi);
            //}
            //if (string.IsNullOrEmpty(Model.SSoDeNghi))
            //{
            //    lstError.Add(Resources.MsgCheckSoDeNghi);
            //}
            //if (!Model.DNgayDeNghi.HasValue)
            //{
            //    lstError.Add(Resources.MsgCheckNgayDeNghi);
            //}
            //if (SelectedKeHoachTongThe == null)
            //{
            //    lstError.Add(Resources.MsgCheckKeHoachTongThe);
            //}
            //if (SelectedNhiemVuChi == null)
            //{
            //    lstError.Add(Resources.MsgNhiemVuChi);
            //}
            //if (SelectedChuDauTu == null)
            //{
            //    lstError.Add(Resources.MsgCheckChuDauTu);
            //}
            //if (SelectedDaHopDong == null)
            //{
            //    lstError.Add(Resources.MsgCheckHopDong);
            //}
            //if (SelectedThanhToanTheo == null)
            //{
            //    lstError.Add(Resources.MsgCheckThanhToanTheo);
            //}
            //if (SelectedLoaiDeNghi == null)
            //{
            //    lstError.Add(Resources.MsgCheckLoaiDeNghi);
            //}
            //if (!Model.INamKeHoach.HasValue)
            //{
            //    lstError.Add(Resources.MsgCheckNamKeHoach);
            //}
            if (!Model.DNgayPheDuyet.HasValue)
            {
                lstError.Add(Resources.MsgCheckNgayPheDuyet);
            }
            if (SelectedThanhToanTheo != null)
            {
                if (Model.IThanhToanTheo.HasValue)
                {
                    switch (Model.IThanhToanTheo.Value)
                    {
                        case 1:
                            if (SelectedDaHopDong == null)
                            {
                                lstError.Add(string.Format(Resources.MsgErrorRequire, "Tên hợp đồng"));
                            }
                            break;
                        case 2:
                            if (SelectedDaDuAn == null)
                            {
                                lstError.Add(string.Format(Resources.MsgErrorRequire, "Tên dự án"));
                            }
                            break;
                    }
                }
            }
            if (ItemsNhTtThanhToanChiTiet != null)
            {
                GetInfoMucLucNs();
                var lstChiTietNotValid = ItemsNhTtThanhToanChiTiet.Where(x => !x.IIdMlnsId.HasValue).ToList();

                if (lstChiTietNotValid.Any())
                {
                    lstError.Add(Resources.MsgValidateMlnsDntt);
                }
            }
            if (lstError.Count != 0)
            {
                MessageBoxHelper.Error(string.Join("\n", lstError));
                return false;
            }
            return true;
        }

        public void OnAddThongTinThanhToanDetail()
        {
            if (_itemsNhTtThanhToanChiTiet == null) _itemsNhTtThanhToanChiTiet = new ObservableCollection<NhTtThanhToanChiTietModel>();

            int currentRow = -1;
            if (!_itemsNhTtThanhToanChiTiet.IsEmpty())
            {
                currentRow = 0;
                if (SelectedNhTtThanhToanChiTiet != null)
                {
                    currentRow = _itemsNhTtThanhToanChiTiet.IndexOf(SelectedNhTtThanhToanChiTiet);
                }
            }

            NhTtThanhToanChiTietModel targetItem = new NhTtThanhToanChiTietModel();
            targetItem.Id = Guid.NewGuid();
            targetItem.SLns = "";
            targetItem.SL = "010";
            targetItem.SK = "011";
            targetItem.SM = "";
            targetItem.STm = "";
            targetItem.STtm = "";
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            targetItem.PropertyChanged += ThanhToanChiTiet_PropertyChanged;
            _itemsNhTtThanhToanChiTiet.Insert(currentRow + 1, targetItem);
            SetGiaTriHopDongDuocDuyet();
            //SetGiaTriKinhPhiKyTruoc();
            LoadThongTinLuyKe();
            OnPropertyChanged(nameof(ItemsNhTtThanhToanChiTiet));
        }

        public void OnDeleteThongTinThanhToanDetail()
        {
            if (SelectedNhTtThanhToanChiTiet != null)
            {
                SelectedNhTtThanhToanChiTiet.IsDeleted = !SelectedNhTtThanhToanChiTiet.IsDeleted;
                CaculateTotalThongTinThanhToan();
            }
        }

        private void CaculateTotalThongTinThanhToan()
        {
            if (!ItemsNhTtThanhToanChiTiet.IsEmpty())
            {
                //tong hop dong phe duyet
                Model.FTongPheDuyetUSD = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FDuocDuyetUsd);
                Model.FTongPheDuyetVND = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FDuocDuyetVnd);
                Model.FTongPheDuyetEUR = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FDuocDuyetEur);
                Model.FTongPheDuyetNgoaiTeKhac = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FDuocDuyetNgoaiTeKhac);
                //tong De nghi phe duyet
                Model.FTongDeNghiKyNayUsd = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FDeNghiCapKyNayUsd);
                Model.FTongDeNghiKyNayVnd = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FDeNghiCapKyNayVnd);
                Model.FTongDeNghiKyNayEur = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FDeNghiCapKyNayEur);
                Model.FTongDeNghiKyNayNgoaiTeKhac = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FDeNghiCapKyNayNgoaiTeKhac);
                //tong phe duyet cap
                Model.FTongPheDuyetCapKyNayUsd = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FPheDuyetCapKyNayUsd);
                Model.FTongPheDuyetCapKyNayVnd = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FPheDuyetCapKyNayVnd);
                Model.FTongPheDuyetCapKyNayEur = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FPheDuyetCapKyNayEur);
                Model.FTongPheDuyetCapKyNayNgoaiTeKhac = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FPheDuyetCapKyNayNgoaiTeKhac);
                //tong luy ke phe duyet
                Model.FTongLuyKePheDuyetUSD = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriUsdCol4);
                Model.FTongLuyKePheDuyetVND = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriVndCol4);
                Model.FTongLuyKePheDuyetEUR = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriEurCol4);
                Model.FTongLuyKePheDuyetNgoaiTeKhac = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FGiatriNgoaiTeKhacCol4);
                OnPropertyChanged(nameof(Model));
            }
        }

        private void ThanhToanChiTiet_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            foreach (var item in _itemsNhTtThanhToanChiTiet)
            {
                item.PropertyChanged -= ThanhToanChiTiet_PropertyChanged;
            }
            var objSender = (NhTtThanhToanChiTietModel)sender;
            if ((args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.STenNoiDungChi)))
                && SelectedNhTtThanhToanChiTiet != null
                && !string.IsNullOrEmpty(SelectedNhTtThanhToanChiTiet.SLns)
                && !string.IsNullOrEmpty(SelectedNhTtThanhToanChiTiet.SL)
                && !string.IsNullOrEmpty(SelectedNhTtThanhToanChiTiet.SK)
                && !string.IsNullOrEmpty(SelectedNhTtThanhToanChiTiet.SM))
            {
                string sXauNoiChuoi = string.Join("-", new string[] { SelectedNhTtThanhToanChiTiet.SLns, SelectedNhTtThanhToanChiTiet.SL,
                                                                      SelectedNhTtThanhToanChiTiet.SK, SelectedNhTtThanhToanChiTiet.SM,
                                                                      SelectedNhTtThanhToanChiTiet.STm, SelectedNhTtThanhToanChiTiet.STtm
                                                                    }.Where(s => !string.IsNullOrEmpty(s)));

                if (!_dicMucLucNganSach.ContainsKey(sXauNoiChuoi))
                {
                    MessageBox.Show(Resources.MsgErrorMucLucNganSachNotExist);
                    return;
                }
                else
                {
                    var idMlns = _dicMucLucNganSach[sXauNoiChuoi];
                    var mlns = _iNsMucLucNganSachService.FindByCondition(x => x.Id.Equals(idMlns)).FirstOrDefault();
                    if (mlns != null)
                    {
                        SelectedNhTtThanhToanChiTiet.STenNoiDungChi = mlns.MoTa;
                        SelectedNhTtThanhToanChiTiet.IIdMucLucNganSachId = mlns.Id;
                        SelectedNhTtThanhToanChiTiet.IIdMlnsId = mlns.MlnsId;
                    }
                }
            }
            if (args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.IsDeleted))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FTongGiaTriTheoHoaDonUsd))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FTongGiaTriTheoHoaDonVnd))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FTongGiaTriTheoHoaDonEur))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FTongGiaTriTheoHoaDonNgoaiTeKhac))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FTiLeThanhToan))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FPheDuyetCapKyNayUsd))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FPheDuyetCapKyNayVnd))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FPheDuyetCapKyNayEur))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FPheDuyetCapKyNayNgoaiTeKhac)))
            {
                QuiDoiTienTeGiaTriTheoHoaDon(objSender, args);
                QuiDoiTienTePheDuyet(objSender, args);
                TinhGiaTriThanhToanKyNay(objSender);
                ValidateInputThanhToanKyNay(objSender);
                ValidateInputPheDuyetKyNay(objSender);
                CaculateTotalThongTinThanhToan();
                TinhTongSoDeNghiThanhToanKyNay();
                TinhTongSoChapNhanThanhToanKyNay();
                objSender.IsModified = true;
            }

            foreach (var item in _itemsNhTtThanhToanChiTiet)
            {
                item.PropertyChanged += ThanhToanChiTiet_PropertyChanged;
            }
        }

        private void QuiDoiTienTeGiaTriTheoHoaDon(NhTtThanhToanChiTietModel objSender, PropertyChangedEventArgs args)
        {
            if (SelectedTiGia != null && !args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.IsDeleted)))
            {
                var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                string sourceCurrency;
                string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                double value;
                switch (args.PropertyName)
                {
                    case nameof(NhTtThanhToanChiTietModel.FTongGiaTriTheoHoaDonVnd):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                        value = objSender.FTongGiaTriTheoHoaDonVnd.GetValueOrDefault();
                        break;
                    case nameof(NhTtThanhToanChiTietModel.FTongGiaTriTheoHoaDonEur):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                        value = objSender.FTongGiaTriTheoHoaDonEur.GetValueOrDefault();
                        break;
                    case nameof(NhTtThanhToanChiTietModel.FTongGiaTriTheoHoaDonNgoaiTeKhac):
                        sourceCurrency = otherCurrency;
                        value = objSender.FTongGiaTriTheoHoaDonNgoaiTeKhac.GetValueOrDefault();
                        break;
                    default:
                        sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                        value = objSender.FTongGiaTriTheoHoaDonUsd.GetValueOrDefault();
                        break;
                }
                var listTiGiaCT = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
                var listTiGiaChiTietNew = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(listTiGiaCT);
                objSender.FTongGiaTriTheoHoaDonVnd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTietNew, value);
                objSender.FTongGiaTriTheoHoaDonEur = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTietNew, value);
                objSender.FTongGiaTriTheoHoaDonUsd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTietNew, value);
                objSender.FTongGiaTriTheoHoaDonNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTietNew, value);
            }
        }

        private void QuiDoiTienTeDeNghi(NhTtThanhToanChiTietModel objSender, PropertyChangedEventArgs args)
        {
            if (SelectedTiGia != null && !args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.IsDeleted)))
            {
                var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                string sourceCurrency;
                string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                double value;
                switch (args.PropertyName)
                {
                    case nameof(NhTtThanhToanChiTietModel.FDeNghiCapKyNayVnd):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                        value = objSender.FDeNghiCapKyNayVnd.GetValueOrDefault();
                        break;
                    case nameof(NhTtThanhToanChiTietModel.FDeNghiCapKyNayEur):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                        value = objSender.FDeNghiCapKyNayEur.GetValueOrDefault();
                        break;
                    case nameof(NhTtThanhToanChiTietModel.FDeNghiCapKyNayNgoaiTeKhac):
                        sourceCurrency = otherCurrency;
                        value = objSender.FDeNghiCapKyNayNgoaiTeKhac.GetValueOrDefault();
                        break;
                    default:
                        sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                        value = objSender.FDeNghiCapKyNayUsd.GetValueOrDefault();
                        break;
                }
                objSender.FDeNghiCapKyNayVnd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                objSender.FDeNghiCapKyNayEur = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                objSender.FDeNghiCapKyNayUsd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                objSender.FDeNghiCapKyNayNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
            }
        }

        private void QuiDoiTienTePheDuyet(NhTtThanhToanChiTietModel objSender, PropertyChangedEventArgs args)
        {
            if (SelectedTiGiaPheDuyet != null && !args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.IsDeleted)))
            {
                var listTiGiaChiTietPheDuyet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTietPheDuyet);
                string rootCurrency = SelectedTiGiaPheDuyet.SMaTienTeGoc;
                string sourceCurrency;
                string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                double value;
                switch (args.PropertyName)
                {
                    case nameof(NhTtThanhToanChiTietModel.FPheDuyetCapKyNayVnd):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                        value = objSender.FPheDuyetCapKyNayVnd.GetValueOrDefault();
                        break;
                    case nameof(NhTtThanhToanChiTietModel.FPheDuyetCapKyNayEur):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                        value = objSender.FPheDuyetCapKyNayEur.GetValueOrDefault();
                        break;
                    case nameof(NhTtThanhToanChiTietModel.FPheDuyetCapKyNayNgoaiTeKhac):
                        sourceCurrency = otherCurrency;
                        value = objSender.FPheDuyetCapKyNayNgoaiTeKhac.GetValueOrDefault();
                        break;
                    default:
                        sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                        value = objSender.FPheDuyetCapKyNayUsd.GetValueOrDefault();
                        break;
                }
                objSender.FPheDuyetCapKyNayVnd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTietPheDuyet, value);
                objSender.FPheDuyetCapKyNayEur = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTietPheDuyet, value);
                objSender.FPheDuyetCapKyNayUsd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTietPheDuyet, value);
                objSender.FPheDuyetCapKyNayNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTietPheDuyet, value);
            }
        }

        private void TinhGiaTriThanhToanKyNay(NhTtThanhToanChiTietModel obj)
        {
            //obj.FDeNghiCapKyNayEur = obj.FTongGiaTriTheoHoaDonEur.GetValueOrDefault() * obj.FTiLeThanhToan.GetValueOrDefault();
            //obj.FDeNghiCapKyNayVnd = obj.FTongGiaTriTheoHoaDonVnd.GetValueOrDefault() * obj.FTiLeThanhToan.GetValueOrDefault();
            //obj.FDeNghiCapKyNayUsd = obj.FTongGiaTriTheoHoaDonUsd.GetValueOrDefault() * obj.FTiLeThanhToan.GetValueOrDefault();
            //obj.FDeNghiCapKyNayNgoaiTeKhac = obj.FTongGiaTriTheoHoaDonNgoaiTeKhac.GetValueOrDefault() * obj.FTiLeThanhToan.GetValueOrDefault();
            obj.FGiaTriUsdCol4 = obj.FGiaTriUsdChangeCol4;
            obj.FGiaTriVndCol4 = obj.FGiaTriVndChangeCol4;
            obj.FGiaTriEurCol4 = obj.FGiaTriEurChangeCol4;
            obj.FGiatriNgoaiTeKhacCol4 = obj.FGiaTriKinhPhiKhacChangeCol4;
            if (IsUsd)
                obj.FTiLeThanhToan = obj.FTiLeThanhToanUsdChange;
            else
                obj.FTiLeThanhToan = obj.FTiLeThanhToanVNDChange;
        }

        public void ValidateInputThanhToanKyNay(NhTtThanhToanChiTietModel objSender)
        {
            if (_selectedLoaiNoiDungChi != null &&
                _selectedLoaiNoiDungChi.ValueItem.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()))
            {
                var soTienChuaThanhToan = objSender.FDuocDuyetUsd.GetValueOrDefault() - objSender.FDuocCapKyTruocUsd.GetValueOrDefault();
                if (objSender.FDeNghiCapKyNayUsd > soTienChuaThanhToan)
                {
                    MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(Resources.MsgWarningThanhToanKyNayHonChuaThanhToan);
                    if (messageBoxResult == MessageBoxResult.No)
                    {
                        objSender.FDeNghiCapKyNayUsd = 0;
                        objSender.FDeNghiCapKyNayVnd = 0;
                        objSender.FDeNghiCapKyNayEur = 0;
                        objSender.FDeNghiCapKyNayNgoaiTeKhac = 0;
                        objSender.FTiLeThanhToan = 0;
                    }
                }
            }
            else
            {
                var soTienChuaThanhToan = objSender.FDuocDuyetVnd.GetValueOrDefault() - objSender.FDuocCapKyTruocVnd.GetValueOrDefault();
                if (objSender.FDeNghiCapKyNayVnd.GetValueOrDefault() > soTienChuaThanhToan)
                {
                    MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(Resources.MsgWarningThanhToanKyNayHonChuaThanhToan);
                    if (messageBoxResult == MessageBoxResult.No)
                    {
                        objSender.FDeNghiCapKyNayUsd = 0;
                        objSender.FDeNghiCapKyNayVnd = 0;
                        objSender.FDeNghiCapKyNayEur = 0;
                        objSender.FDeNghiCapKyNayNgoaiTeKhac = 0;
                        objSender.FTiLeThanhToan = 0;
                    }
                }
            }
        }

        public void ValidateInputPheDuyetKyNay(NhTtThanhToanChiTietModel objSender)
        {
            if (_selectedLoaiNoiDungChi != null &&
                _selectedLoaiNoiDungChi.ValueItem.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()))
            {
                if (objSender.FPheDuyetCapKyNayUsd > objSender.FDeNghiCapKyNayUsd)
                {
                    MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(Resources.MsgWarningChapNhanKyNayHonDeNghiKyNay);
                    if (messageBoxResult == MessageBoxResult.No)
                    {
                        objSender.FPheDuyetCapKyNayUsd = 0;
                        objSender.FPheDuyetCapKyNayVnd = 0;
                        objSender.FPheDuyetCapKyNayEur = 0;
                        objSender.FPheDuyetCapKyNayNgoaiTeKhac = 0;
                    }
                }
            }
            else
            {
                if (objSender.FPheDuyetCapKyNayVnd > objSender.FDeNghiCapKyNayVnd)
                {
                    MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(Resources.MsgWarningChapNhanKyNayHonDeNghiKyNay);
                    if (messageBoxResult == MessageBoxResult.No)
                    {
                        objSender.FPheDuyetCapKyNayUsd = 0;
                        objSender.FPheDuyetCapKyNayVnd = 0;
                        objSender.FPheDuyetCapKyNayEur = 0;
                        objSender.FPheDuyetCapKyNayNgoaiTeKhac = 0;
                    }
                }
            }
        }

        public void TinhTongSoDeNghiThanhToanKyNay()
        {
            if (_selectedLoaiNoiDungChi != null &&
                _selectedLoaiNoiDungChi.ValueItem.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()))
            {
                if (ItemsNhTtThanhToanChiTiet != null)
                {
                    Model.FTongDeNghiBangSo =
                        ItemsNhTtThanhToanChiTiet.Sum(x => x.FDeNghiCapKyNayUsd.GetValueOrDefault());
                    Model.STongDeNghiBangChu = StringUtils.NumberToText(Model.FTongDeNghiBangSo.GetValueOrDefault(), false);
                    Model.FTienMatBangSo = Model.FTongDeNghiBangSo;
                    Model.STienMatBangChu = StringUtils.NumberToText(Model.FTongDeNghiBangSo.GetValueOrDefault(), false);
                }
            }
            else
            {
                if (ItemsNhTtThanhToanChiTiet != null)
                {
                    Model.FTongDeNghiBangSo =
                        ItemsNhTtThanhToanChiTiet.Sum(x => x.FDeNghiCapKyNayVnd.GetValueOrDefault());
                    Model.STongDeNghiBangChu = StringUtils.NumberToText(Model.FTongDeNghiBangSo.GetValueOrDefault());
                    Model.FTienMatBangSo = Model.FTongDeNghiBangSo;
                    Model.STienMatBangChu = StringUtils.NumberToText(Model.FTongDeNghiBangSo.GetValueOrDefault());
                }
            }
        }

        public void TinhTongSoChapNhanThanhToanKyNay()
        {
            if (_selectedLoaiNoiDungChi != null &&
                _selectedLoaiNoiDungChi.ValueItem.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()))
            {
                if (ItemsNhTtThanhToanChiTiet != null)
                {
                    Model.FTongPheDuyetBangSo =
                        ItemsNhTtThanhToanChiTiet.Sum(x => x.FPheDuyetCapKyNayUsd.GetValueOrDefault());
                    Model.STongPheDuyetBangChu = StringUtils.NumberToText(Model.FTongPheDuyetBangSo.GetValueOrDefault(), false);
                }
            }
            else
            {
                if (ItemsNhTtThanhToanChiTiet != null)
                {
                    Model.FTongPheDuyetBangSo =
                        ItemsNhTtThanhToanChiTiet.Sum(x => x.FPheDuyetCapKyNayVnd.GetValueOrDefault());
                    Model.STongPheDuyetBangChu = StringUtils.NumberToText(Model.FTongPheDuyetBangSo.GetValueOrDefault());
                }
            }
        }

        public void SetGiaTriHopDongDuocDuyet()
        {
            if (_itemsNhTtThanhToanChiTiet != null)
            {
                foreach (var it in _itemsNhTtThanhToanChiTiet)
                {
                    //Check nếu thanh toán theo hợp đồng thì hiển thị giá trị theo hợp đồng, nếu chọn dự án thì hiển thị giá trị theo dự án
                    if (_selectedThanhToanTheo != null && Convert.ToInt16(_selectedThanhToanTheo.ValueItem) == (int)NHThanhToanTheo.Type.CHI_THEO_HOP_DONG)
                    {
                        it.FDuocDuyetUsd = _selectedDaHopDong != null ? _selectedDaHopDong.FGiaTriUsd : 0;
                        it.FDuocDuyetVnd = _selectedDaHopDong != null ? _selectedDaHopDong.FGiaTriVnd : 0;
                        it.FDuocDuyetEur = _selectedDaHopDong != null ? _selectedDaHopDong.FGiaTriEur : 0;
                        it.FDuocDuyetNgoaiTeKhac = _selectedDaHopDong != null ? _selectedDaHopDong.FGiaTriNgoaiTeKhac : 0;
                    }
                    else if (_selectedThanhToanTheo != null && Convert.ToInt16(_selectedThanhToanTheo.ValueItem) == (int)NHThanhToanTheo.Type.CHI_THEO_DU_AN_KHONG_HINH_THANH_HOP_DONG)
                    {
                        it.FDuocDuyetUsd = _selectedDaDuAn != null ? _selectedDaDuAn.FUsd : 0;
                        it.FDuocDuyetVnd = _selectedDaDuAn != null ? _selectedDaDuAn.FVnd : 0;
                        it.FDuocDuyetEur = _selectedDaDuAn != null ? _selectedDaDuAn.FEur : 0;
                        it.FDuocDuyetNgoaiTeKhac = _selectedDaDuAn != null ? _selectedDaDuAn.FNgoaiTeKhac : 0;
                    }
                    else
                    {
                        it.FDuocDuyetUsd = 0;
                        it.FDuocDuyetVnd = 0;
                        it.FDuocDuyetEur = 0;
                        it.FDuocDuyetNgoaiTeKhac = 0;
                    }

                }
                OnPropertyChanged(nameof(ItemsNhTtThanhToanChiTiet));
            }
        }

        public void SetGiaTriKinhPhiKyTruoc()
        {
            var lstPheDuyetThanhToan = GetListPheDuyetThanhToan();
            var lstIdPheDuyet = lstPheDuyetThanhToan.Select(x => x.Id).ToList();
            var predicate = PredicateBuilder.True<NhTtThanhToanChiTiet>();
            predicate = predicate.And(x => x.IIdDeNghiThanhToanId.HasValue && lstIdPheDuyet.Contains(x.IIdDeNghiThanhToanId.Value));
            var results = _iNhTtThanhToanChiTietService.FindByCondition(predicate).ToList();
            var lstThanhToanChiTietModel = _mapper.Map<List<NhTtThanhToanChiTietModel>>(results);
            if (_itemsNhTtThanhToanChiTiet != null)
            {
                foreach (var it in _itemsNhTtThanhToanChiTiet)
                {
                    it.FDuocCapKyTruocUsd = lstThanhToanChiTietModel.Sum(x => x.FPheDuyetCapKyNayUsd.GetValueOrDefault());
                    it.FDuocCapKyTruocVnd = lstThanhToanChiTietModel.Sum(x => x.FPheDuyetCapKyNayVnd.GetValueOrDefault());
                    it.FDuocCapKyTruocEur = lstThanhToanChiTietModel.Sum(x => x.FPheDuyetCapKyNayEur.GetValueOrDefault());
                    it.FDuocCapKyTruocNgoaiTeKhac = lstThanhToanChiTietModel.Sum(x => x.FPheDuyetCapKyNayNgoaiTeKhac.GetValueOrDefault());
                }
                OnPropertyChanged(nameof(ItemsNhTtThanhToanChiTiet));
            }
        }

        public List<NhTtThanhToanModel> GetListPheDuyetThanhToan()
        {
            if (_selectedDaHopDong != null)
            {
                var predicate = PredicateBuilder.True<NhTtThanhToan>();
                predicate = predicate.And(x => x.ITrangThai == (int)NhLoaiThanhToan.Type.PHE_DUYET);
                predicate = predicate.And(x => x.IIdHopDongId.Equals(_selectedDaHopDong.Id));
                var results = _iNhTtThanhToanService.FindByCondition(predicate).ToList();
                return _mapper.Map<List<NhTtThanhToanModel>>(results);
            }
            return new List<NhTtThanhToanModel>();
        }

        private void LoadDuAnHopDong()
        {
            ItemsDaHopDong = new ObservableCollection<NhDaHopDongModel>();
            if (_selectedKeHoachTongThe != null && _selectedNhiemVuChi != null)
            {
                var predicate = PredicateBuilder.True<NhDaHopDong>();
                predicate = predicate.And(x => x.NhKhTongTheNhiemVuChi.IIdKhTongTheId.ToString().Equals(_selectedKeHoachTongThe.ValueItem));
                predicate = predicate.And(x => x.NhKhTongTheNhiemVuChi.IIdNhiemVuChiId.Equals(_selectedNhiemVuChi.Id));
                var lstDaHopDong = _iNhDaHopDongService.FindByCondition(predicate).ToList();
                ItemsDaHopDong = new ObservableCollection<NhDaHopDongModel>(_mapper.Map<List<NhDaHopDongModel>>(lstDaHopDong));
            }
        }

        private void LoadThanhToanChiTiet()
        {
            _itemsNhTtThanhToanChiTiet = new ObservableCollection<NhTtThanhToanChiTietModel>();
            if (!Model.Id.IsNullOrEmpty())
            {
                var data = _iNhTtThanhToanChiTietService.FindByCondition(x => x.IIdDeNghiThanhToanId.Equals(Model.Id));
                _itemsNhTtThanhToanChiTiet = _mapper.Map<ObservableCollection<NhTtThanhToanChiTietModel>>(data);
                _selectedNhTtThanhToanChiTiet = _itemsNhTtThanhToanChiTiet.FirstOrDefault();
                SetGiaTriHopDongDuocDuyet();
                LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                CalculateDataDeNghiThanhToanTongHop();
                foreach (var item in _itemsNhTtThanhToanChiTiet)
                {
                    SetLnsChiTiet(item);
                    item.FGiaTriUsdCol4 = item.FGiaTriUsdChangeCol4;
                    item.FGiaTriVndCol4 = item.FGiaTriVndChangeCol4;
                    item.FGiaTriEurCol4 = item.FGiaTriEurChangeCol4;
                    item.FGiatriNgoaiTeKhacCol4 = item.FGiaTriKinhPhiKhacChangeCol4;
                    if (SelectedLoaiNoiDungChi.ValueItem == LoaiNoiDungChi.Type.CHI_BANG_NOI_TE.ToString())
                        item.FTiLeThanhToan = item.FTiLeThanhToanUsdChange;
                    else
                        item.FTiLeThanhToan = item.FTiLeThanhToanVNDChange;
                    item.PropertyChanged += ThanhToanChiTiet_PropertyChanged;
                }
            }

            CaculateTotalThongTinThanhToan();
            OnPropertyChanged(nameof(ItemsNhTtThanhToanChiTiet));
            OnPropertyChanged(nameof(SelectedNhTtThanhToanChiTiet));
        }

        private void SetLnsChiTiet(NhTtThanhToanChiTietModel item)
        {
            if (item.IIdMucLucNganSachId.HasValue)
            {
                var mlns = _iNsMucLucNganSachService.FindByCondition(x => x.Id.Equals(item.IIdMucLucNganSachId.Value)).FirstOrDefault();
                if (mlns != null)
                {
                    item.SLns = mlns.Lns;
                    item.SL = mlns.L;
                    item.SK = mlns.K;
                    item.SM = mlns.M;
                    item.STm = mlns.Tm;
                    item.STtm = mlns.Ttm;
                }
            }
        }

        private void GetDicMucLucNganSach()
        {
            try
            {
                var data = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
                _dicMucLucNganSach = new Dictionary<string, Guid>();
                if (data != null && data.Any())
                {
                    _dicMucLucNganSach = data.GroupBy(g => g.SXauNoiMa).Select(g => new { g.Key, g.First().Id }).ToDictionary(x => x.Key, x => x.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        public void GetInfoMucLucNs()
        {
            try
            {
                if (SelectedNhTtThanhToanChiTiet != null)
                {
                    string sXauNoiChuoi = SelectedNhTtThanhToanChiTiet.SLns + "-" + SelectedNhTtThanhToanChiTiet.SL + "-" + SelectedNhTtThanhToanChiTiet.SK + "-" + SelectedNhTtThanhToanChiTiet.SM + "-" + SelectedNhTtThanhToanChiTiet.STm + "-" + SelectedNhTtThanhToanChiTiet.STtm;
                    if (string.IsNullOrEmpty(SelectedNhTtThanhToanChiTiet.SLns)
                        || string.IsNullOrEmpty(SelectedNhTtThanhToanChiTiet.SL)
                        || string.IsNullOrEmpty(SelectedNhTtThanhToanChiTiet.SK)
                        || string.IsNullOrEmpty(SelectedNhTtThanhToanChiTiet.SM)
                        || !_dicMucLucNganSach.ContainsKey(sXauNoiChuoi))
                    {
                        MessageBox.Show(Resources.MsgErrorMucLucNganSachNotExist);
                        return;
                    }

                    var idMlns = _dicMucLucNganSach[sXauNoiChuoi];
                    var mlns = _iNsMucLucNganSachService.FindByCondition(x => x.Id.Equals(idMlns)).FirstOrDefault();
                    if (mlns != null)
                    {
                        SelectedNhTtThanhToanChiTiet.STenNoiDungChi = mlns.MoTa;
                        SelectedNhTtThanhToanChiTiet.IIdMucLucNganSachId = mlns.Id;
                        SelectedNhTtThanhToanChiTiet.IIdMlnsId = mlns.MlnsId;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        public void OnSelectMLNS(object obj)
        {
            try
            {
                DataGrid dataGrid = obj as DataGrid;
                if (SelectedNhTtThanhToanChiTiet != null || (dataGrid.CurrentCell != null && dataGrid.CurrentCell.Column != null
                    && (dataGrid.CurrentCell.Column.SortMemberPath.Equals("SLns")
                    || dataGrid.CurrentCell.Column.SortMemberPath.Equals("SL")
                    || dataGrid.CurrentCell.Column.SortMemberPath.Equals("SK")
                    || dataGrid.CurrentCell.Column.SortMemberPath.Equals("SM")
                    || dataGrid.CurrentCell.Column.SortMemberPath.Equals("STm")
                    || dataGrid.CurrentCell.Column.SortMemberPath.Equals("STtm"))))
                {
                    SelectMLNSDialogViewModel.Init();
                    SelectMLNSDialogViewModel.SavedAction = obj =>
                    {
                        NsMucLucNganSach mlns = (NsMucLucNganSach)obj;
                        foreach (var item in ItemsNhTtThanhToanChiTiet)
                        {
                            if (item.Id == SelectedNhTtThanhToanChiTiet.Id)
                            {
                                item.IIdMlnsId = mlns.MlnsId;
                                item.SLns = mlns.Lns;
                                item.SK = mlns.K;
                                item.SL = mlns.L;
                                item.SM = mlns.M;
                                item.STm = mlns.Tm;
                                item.STtm = mlns.Ttm;
                                item.STenNoiDungChi = mlns.MoTa;
                                item.PropertyChanged += ThanhToanChiTiet_PropertyChanged;
                            }
                        }

                    };
                    SelectMLNSDialogViewModel.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ThanhToan_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var objSender = (NhTtThanhToanModel)sender;
            if (args.PropertyName.Equals(nameof(NhTtThanhToanModel.FTongDeNghiBangSo))
                || args.PropertyName.Equals(nameof(NhTtThanhToanModel.FThuHoiTamUngBangSo))
                || args.PropertyName.Equals(nameof(NhTtThanhToanModel.FChuyenKhoanBangSo))
                || args.PropertyName.Equals(nameof(NhTtThanhToanModel.FTienMatBangSo))
                || args.PropertyName.Equals(nameof(NhTtThanhToanModel.FTongPheDuyetBangSo))
                || args.PropertyName.Equals(nameof(NhTtThanhToanModel.FThuHoiTamUngPheDuyetBangSo)))
            {
                objSender.FTraDonViThuHuongBangSo = objSender.FTongDeNghiBangSo.GetValueOrDefault() -
                                                    objSender.FThuHoiTamUngBangSo.GetValueOrDefault();
                if (args.PropertyName.Equals(nameof(NhTtThanhToanModel.FChuyenKhoanBangSo)))
                {
                    objSender.FTienMatBangSo = objSender.FTraDonViThuHuongBangSo.GetValueOrDefault() -
                                           objSender.FChuyenKhoanBangSo.GetValueOrDefault();
                }
                if (args.PropertyName.Equals(nameof(NhTtThanhToanModel.FTienMatBangSo)))
                {
                    objSender.FChuyenKhoanBangSo = objSender.FTraDonViThuHuongBangSo.GetValueOrDefault() -
                                           objSender.FTienMatBangSo.GetValueOrDefault();
                }
                objSender.FTraDonViThuHuongPheDuyetBangSo = objSender.FTongPheDuyetBangSo.GetValueOrDefault() -
                                                            objSender.FThuHoiTamUngPheDuyetBangSo.GetValueOrDefault();
                if (objSender.FTongDeNghiBangSo.GetValueOrDefault() >= objSender.FTongPheDuyetBangSo.GetValueOrDefault())
                {
                    objSender.FTuChoiThanhToanBangSo = objSender.FTongDeNghiBangSo.GetValueOrDefault() -
                                                       objSender.FTongPheDuyetBangSo.GetValueOrDefault();
                }
                else
                {
                    objSender.FTuChoiThanhToanBangSo = 0;
                }

                if (objSender.FTraDonViThuHuongBangSo.GetValueOrDefault() < 0 && objSender.FThuHoiTamUngBangSo.GetValueOrDefault() != 0)
                {
                    MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(Resources.MsgConfirmSoTraDonViThuHuongAm);
                    if (messageBoxResult == MessageBoxResult.No)
                    {
                        objSender.FThuHoiTamUngBangSo = 0;
                    }
                }

                if (objSender.FTraDonViThuHuongPheDuyetBangSo.GetValueOrDefault() < 0 && objSender.FThuHoiTamUngPheDuyetBangSo.GetValueOrDefault() != 0)
                {
                    MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(Resources.MsgConfirmSoTraDonViThuHuongAm);
                    if (messageBoxResult == MessageBoxResult.No)
                    {
                        objSender.FTraDonViThuHuongPheDuyetBangSo = 0;
                    }
                }

                if (_selectedLoaiNoiDungChi == null ||
                    _selectedLoaiNoiDungChi.ValueItem.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE).ToString()))
                {
                    objSender.FThuHoiTamUngBangChu = StringUtils.NumberToText(objSender.FThuHoiTamUngBangSo.GetValueOrDefault());
                    objSender.FTraDonViThuHuongBangChu = StringUtils.NumberToText(objSender.FTraDonViThuHuongBangSo.GetValueOrDefault());
                    objSender.SChuyenKhoanBangChu = StringUtils.NumberToText(objSender.FChuyenKhoanBangSo.GetValueOrDefault());
                    objSender.STienMatBangChu = StringUtils.NumberToText(objSender.FTienMatBangSo.GetValueOrDefault());
                    objSender.FThuHoiTamUngPheDuyetBangChu = StringUtils.NumberToText(objSender.FThuHoiTamUngPheDuyetBangSo.GetValueOrDefault());
                    objSender.FTraDonViThuHuongPheDuyetBangChu = StringUtils.NumberToText(objSender.FTraDonViThuHuongPheDuyetBangSo.GetValueOrDefault());
                }
                else if (_selectedLoaiNoiDungChi == null ||
                    _selectedLoaiNoiDungChi.ValueItem.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()))
                {
                    objSender.FThuHoiTamUngBangChu = StringUtils.NumberToText(objSender.FThuHoiTamUngBangSo.GetValueOrDefault(), false);
                    objSender.FTraDonViThuHuongBangChu = StringUtils.NumberToText(objSender.FTraDonViThuHuongBangSo.GetValueOrDefault(), false);
                    objSender.SChuyenKhoanBangChu = StringUtils.NumberToText(objSender.FChuyenKhoanBangSo.GetValueOrDefault(), false);
                    objSender.STienMatBangChu = StringUtils.NumberToText(objSender.FTienMatBangSo.GetValueOrDefault(), false);
                    objSender.FThuHoiTamUngPheDuyetBangChu = StringUtils.NumberToText(objSender.FThuHoiTamUngPheDuyetBangSo.GetValueOrDefault(), false);
                    objSender.FTraDonViThuHuongPheDuyetBangChu = StringUtils.NumberToText(objSender.FTraDonViThuHuongPheDuyetBangSo.GetValueOrDefault(), false);
                }
            }
        }

        public void LoadDAHDTheoThanhToan()
        {
            if (_selectedThanhToanTheo != null)
            {
                if (Convert.ToInt16(_selectedThanhToanTheo.ValueItem) == (int)NHThanhToanTheo.Type.CHI_THEO_HOP_DONG)
                {
                    IsHiddenDuAn = false;
                    IsHiddenHopDong = true;
                    IsHiddenChiPhiQdk = false;
                }
                else if (Convert.ToInt16(_selectedThanhToanTheo.ValueItem) == (int)NHThanhToanTheo.Type.CHI_THEO_DU_AN_KHONG_HINH_THANH_HOP_DONG)
                {
                    IsHiddenDuAn = true;
                    IsHiddenHopDong = false;
                    IsHiddenChiPhiQdk = false;

                }
                else
                {
                    IsHiddenDuAn = false;
                    IsHiddenHopDong = false;
                    IsHiddenChiPhiQdk = true;
                    //LoadQuyetDinhChiPhiKhac();

                }
            }
            OnPropertyChanged(nameof(IsHiddenDuAn));
            OnPropertyChanged(nameof(IsHiddenHopDong));
            OnPropertyChanged(nameof(IsHiddenChiPhiQdk));
        }

        private void LoadDADuAn()
        {
            ItemsDaDuAn = new ObservableCollection<NhDaDuAnModel>();
            if (_selectedNhiemVuChi != null && _selectedChuDauTu != null)
            {
                var predicate = PredicateBuilder.True<NhKhTongTheNhiemVuChi>();
                predicate = predicate.And(x => x.IIdKhTongTheId.ToString().Equals(_selectedKeHoachTongThe.ValueItem));
                predicate = predicate.And(x => x.IIdNhiemVuChiId.Equals(_selectedNhiemVuChi.Id));
                var id_KHTT_NVC = _iNhKhTongTheService.FindKHTongTheNVCByConditon(predicate).FirstOrDefault();
                var lstDaDuAn = _iNhDaDuAnService.FindAll().Where(x => x.IIdKhttNhiemVuChiId == id_KHTT_NVC.Id && x.IIdChuDauTuId == _selectedChuDauTu.Id).ToList();
                ItemsDaDuAn = new ObservableCollection<NhDaDuAnModel>(_mapper.Map<List<NhDaDuAnModel>>(lstDaDuAn));
            }
        }

        private void LoadQuyetDinhChiPhiKhac()
        {
            ItemsChiPhiKhac = new ObservableCollection<NhDaQuyetDinhKhac>();
            var predicate = PredicateBuilder.True<NhDaQuyetDinhKhac>();
            if (_selectedNhiemVuChi != null)
            {
                predicate = predicate.And(x => x.IIdKHTTNhiemVuChiId.Equals(_selectedNhiemVuChi.IIdKHTTNhiemVuChiId));
            }
            var data = _nhDaQuyetDinhKhacService.FindByCondition(predicate);
            ItemsChiPhiKhac = new ObservableCollection<NhDaQuyetDinhKhac>(data);

        }


        private void LoadThanhToanTheo()
        {
            var results = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem(NHThanhToanTheo.Get((int)NHThanhToanTheo.Type.CHI_THEO_HOP_DONG), ((int)NHThanhToanTheo.Type.CHI_THEO_HOP_DONG).ToString()),
                new ComboboxItem(NHThanhToanTheo.Get((int)NHThanhToanTheo.Type.CHI_THEO_DU_AN_KHONG_HINH_THANH_HOP_DONG), ((int)NHThanhToanTheo.Type.CHI_THEO_DU_AN_KHONG_HINH_THANH_HOP_DONG).ToString()),
                new ComboboxItem(NHThanhToanTheo.Get((int)NHThanhToanTheo.Type.CHI_KHONG_THEO_DU_AN_HOP_DONG), ((int)NHThanhToanTheo.Type.CHI_KHONG_THEO_DU_AN_HOP_DONG).ToString())
            };

            _itemsThanhToanTheo = results;
            _selectedThanhToanTheo = Model.Id == Guid.Empty ? _itemsThanhToanTheo[0] : _selectedThanhToanTheo;
            IsHiddenDuAn = false;
            IsHiddenHopDong = true;
        }

        public void LoadThongTinLuyKe()
        {
            //công thức tính lũy kế fluyke = sum(fpheduyetthanhtoan) cùng đơn vị, nhiệm vụ chi, chủ đầu tư
            if (_selectedDonVi != null && _selectedNhiemVuChi != null && _selectedChuDauTu != null)
            {
                var predicate = PredicateBuilder.True<NhTtThanhToan>();
                predicate = predicate.And(x => x.IIdDonVi == _selectedDonVi.Id);
                predicate = predicate.And(x => x.IIdNhiemVuChiId == _selectedNhiemVuChi.Id);
                predicate = predicate.And(x => x.IIdChuDauTuId == _selectedChuDauTu.Id);
                if (SelectedThanhToanTheo != null && SelectedThanhToanTheo.ValueItem == "2" && _selectedDaDuAn != null)
                {
                    predicate = predicate.And(x => x.IIdDuAnId == _selectedDaDuAn.Id);
                    predicate = predicate.And(x => x.IThanhToanTheo == 2);
                }
                if (SelectedThanhToanTheo != null && SelectedThanhToanTheo.ValueItem == "1" && _selectedDaHopDong != null)
                {
                    predicate = predicate.And(x => x.IIdHopDongId == _selectedDaHopDong.Id);
                    predicate = predicate.And(x => x.IIdDuAnId == _selectedDaHopDong.IIdDuAnId);
                    predicate = predicate.And(x => x.IThanhToanTheo == 1);
                }
                if (SelectedThanhToanTheo != null && SelectedThanhToanTheo.ValueItem == "3")
                {
                    predicate = predicate.And(x => x.IThanhToanTheo == 3);
                }

                var lstthanhtoan = _iNhTtThanhToanService.FindByCondition(predicate).ToList();
                IEnumerable<NhTtThanhToan> querythanhtoan;

                if (Model.Id.IsNullOrEmpty())
                {
                    querythanhtoan = from tt in lstthanhtoan
                                     where (tt.ILoaiDeNghi == 1 && tt.ICoQuanThanhToan == 2) || (tt.ILoaiDeNghi == 2 && tt.ICoQuanThanhToan == 1) || (tt.ILoaiDeNghi == 3 && tt.ICoQuanThanhToan == 1)
                                     select tt;
                }
                else
                {
                    querythanhtoan = from tt in lstthanhtoan
                                     where ((tt.ILoaiDeNghi == 1 && tt.ICoQuanThanhToan == 2) || (tt.ILoaiDeNghi == 2 && tt.ICoQuanThanhToan == 1) || (tt.ILoaiDeNghi == 3 && tt.ICoQuanThanhToan == 1)) && (tt.DNgayTao < Model.DNgayTao)
                                     select tt;
                }

                double? fLuyKeUSD = 0;
                double? fLuyKeVND = 0;
                double? fLuyKeEUR = 0;
                double? fLuyKeKhac = 0;

                foreach (var item in querythanhtoan)
                {
                    var predicate_chitiet = PredicateBuilder.True<NhTtThanhToanChiTiet>();
                    predicate_chitiet = predicate_chitiet.And(x => x.IIdDeNghiThanhToanId == item.Id);
                    var lstchitiet = _iNhTtThanhToanChiTietService.FindByCondition(predicate_chitiet).ToList();
                    fLuyKeUSD += lstchitiet.Sum(x => x.FPheDuyetCapKyNayUsd);
                    fLuyKeVND += lstchitiet.Sum(x => x.FPheDuyetCapKyNayVnd);
                    fLuyKeEUR += lstchitiet.Sum(x => x.FPheDuyetCapKyNayEur);
                    fLuyKeKhac += lstchitiet.Sum(x => x.FPheDuyetCapKyNayNgoaiTeKhac);
                }

                if (_itemsNhTtThanhToanChiTiet != null)
                {
                    foreach (var it in _itemsNhTtThanhToanChiTiet)
                    {
                        //it.FDuocCapKyTruocUsd = fLuyKeUSD;
                        //it.FDuocCapKyTruocVnd = fLuyKeVND;
                        //it.FDuocCapKyTruocEur = fLuyKeEUR;
                        //it.FDuocCapKyTruocNgoaiTeKhac = fLuyKeKhac;
                    }
                }
            }
        }

        public override void OnClosing()
        {
            // Clear items
            //if (!_itemsDonViCapTren.IsEmpty()) _itemsDonViCapTren.Clear();
            //if (!_itemsDonVi.IsEmpty()) _itemsDonVi.Clear();
            //if (!_itemsKeHoachTongThe.IsEmpty()) _itemsKeHoachTongThe.Clear();
            //if (!_itemsNhiemVuChi.IsEmpty()) _itemsNhiemVuChi.Clear();
            //if (!_itemsChuDauTu.IsEmpty()) _itemsChuDauTu.Clear();
            //if (!_itemsDaHopDong.IsEmpty()) _itemsDaHopDong.Clear();
            //if (!_itemsLoaiDeNghi.IsEmpty()) _itemsLoaiDeNghi.Clear();
            //if (!_itemsNguonVon.IsEmpty()) _itemsNguonVon.Clear();
            //if (!_itemsDmNhaThau.IsEmpty()) _itemsDmNhaThau.Clear();
            //if (!_itemsLoaiNoiDungChi.IsEmpty()) _itemsLoaiNoiDungChi.Clear();
            //if (!_itemsCoQuanThanhToan.IsEmpty()) _itemsCoQuanThanhToan.Clear();
            //if (!_itemsThanhToanTheo.IsEmpty()) _itemsThanhToanTheo.Clear();
            //if (!ItemsDaDuAn.IsEmpty()) _itemsDaDuAn.Clear();
            //if (!_itemsQuyKeHoach.IsEmpty()) _itemsQuyKeHoach.Clear();
            //Clear selected
        }

        public override void OnClose(object obj)
        {
            if (obj is System.Windows.Window window)
            {
                window.Close();
            }
        }

        private void LoadValueLoaiNoiDungChi()
        {
            if (SelectedLoaiNoiDungChi != null)
            {
                IsUsd = SelectedLoaiNoiDungChi.ValueItem == ((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString();
            }
        }

        private void LoadTiLeThanhToanByNoiDungChi()
        {
            if (SelectedLoaiNoiDungChi != null)
            {
                if (!IsUsd)
                {
                    if (!ItemsNhTtThanhToanChiTiet.IsEmpty())
                        ItemsNhTtThanhToanChiTiet.ForAll(x => { x.FTiLeThanhToan = x.FTiLeThanhToanVNDChange ?? 0; });
                }
                else
                {
                    if (!ItemsNhTtThanhToanChiTiet.IsEmpty())
                        ItemsNhTtThanhToanChiTiet.ForAll(x => { x.FTiLeThanhToan = x.FTiLeThanhToanUsdChange ?? 0; });
                }
            }
            OnPropertyChanged(nameof(ItemsNhTtThanhToanChiTiet));
        }

        private double ConvertConcurency(string sourceCurrency, string sourceOut, double value)
        {
            if (SelectedTiGia != null)
            {
                string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                var listTiGiaCT = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
                var listTiGiaChiTietNew = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(listTiGiaCT);
                return _nhDmTiGiaService.CurrencyExchange(sourceCurrency, sourceOut, rootCurrency, listTiGiaChiTietNew, value);
            }
            return 0;
        }
        public void LoadDataNhTongHop(int iNamDeNghi)
        {
            var lstMaNguon = NHConstants.MA_TH_THANH_TOAN.Split(',').ToList();
            lstMaNguon.Select(x => x.ToString().Trim()).ToList();
            var predicate = PredicateBuilder.True<NHTHTongHop>();
            predicate = predicate.And(x => x.INamKeHoach == iNamDeNghi || x.INamKeHoach == iNamDeNghi - 1);
            predicate = predicate.And(x => lstMaNguon.Contains(x.SMaNguon));
            predicate = predicate.Or(x => lstMaNguon.Contains(x.SMaNguonCha));

            if (SelectedThanhToanTheo != null)
            {
                switch (SelectedThanhToanTheo.ValueItem)
                {
                    case "1":
                        if (SelectedDaHopDong != null)
                        {
                            predicate = predicate.And(x => x.IIdHopDongId == SelectedDaHopDong.Id);
                        }
                        break;
                    case "2":
                        if (SelectedDaHopDong != null)
                        {
                            predicate = predicate.And(x => x.IIdDuAnId == SelectedDaDuAn.Id);
                        }
                        break;
                    case "3":
                        if (SelectedDaHopDong != null)
                        {
                            predicate = predicate.And(x => x.IIDKHTTNhiemVuChiID == SelectedNhiemVuChi.Id);
                        }
                        break;
                }
            }
            ItemsTongHops = _nhThTongHopService.FindByCondition(predicate);
            if (!ItemsTongHops.IsEmpty())
                ItemsTongHops = ItemsTongHops.Where(x => !x.IIdChungTu.Equals(Model.Id));
        }

        private void CalculateDataDeNghiThanhToanTongHop()
        {
            if (ItemsTongHops.Any())
            {
                var iNoiDungChi = SelectedLoaiNoiDungChi != null ? int.Parse(SelectedLoaiNoiDungChi.ValueItem) : (int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE;

                var lstValueLoaiDeNghiKP = new List<string> { ((int)NhLoaiDeNghi.Type.CAP_KINH_PHI).ToString(), ((int)NhLoaiDeNghi.Type.TAM_UNG_KINH_PHI).ToString() };
                List<string> lstMaNguon = new List<string>();
                List<string> lstMaNguonSum = new List<string>();
                List<string> lstMaNguonMinus = new List<string>();
                //Tính toán data chi tiết
                //TT_LK_KPDC_KD_CKT
                var dataNguonTT_LK_KPDC_KD_CKT = ItemsTongHops.Where(x => (x.SMaNguon == NhTongHopConstants.MA_306 || x.SMaNguonCha == NhTongHopConstants.MA_306) && x.INamKeHoach == Model.INamKeHoach - 1);
                //CKP_NTCS, CKP_NN, TT_NN, TUCD_NTCS, TUCD_NN
                lstMaNguonSum = new List<string> { NhTongHopConstants.MA_101, NhTongHopConstants.MA_102,                                                                                                          NhTongHopConstants.MA_111, NhTongHopConstants.MA_112,
                                                   NhTongHopConstants.MA_121, NhTongHopConstants.MA_122,};
                var dataNguonSumTh = ItemsTongHops.Where(x => (lstMaNguonSum.Contains(x.SMaNguon) || lstMaNguonSum.Contains(x.SMaNguonCha)) && x.INamKeHoach == Model.INamKeHoach);
                lstMaNguonMinus = new List<string> { NhTongHopConstants.MA_131, NhTongHopConstants.MA_132 };
                var dataNguonMinusTh = ItemsTongHops.Where(x => (lstMaNguonMinus.Contains(x.SMaNguon) || lstMaNguonMinus.Contains(x.SMaNguonCha)) && x.INamKeHoach == Model.INamKeHoach);

                // case 1: tính data theo TT_SDTUKP nếu loại đề nghị là kinh phí (1,2) 
                if (SelectedLoaiDeNghi != null && lstValueLoaiDeNghiKP.Contains(SelectedLoaiDeNghi.ValueItem))
                {
                    // data nguồn thanh toán
                    var dataNguonTT_SDTUKP = ItemsTongHops.Where(x => (x.SMaNguon == NhTongHopConstants.MA_309 || x.SMaNguonCha == NhTongHopConstants.MA_309) && x.INamKeHoach == Model.INamKeHoach - 1);
                    lstMaNguon = new List<string> { NhTongHopConstants.MA_141, NhTongHopConstants.MA_142 };
                    var dataTUKP_NTCS_NN = ItemsTongHops.Where(x => (lstMaNguon.Contains(x.SMaNguon) || lstMaNguon.Contains(x.SMaNguonCha)) && x.INamKeHoach == Model.INamKeHoach);

                    if (iNoiDungChi == (int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE)
                    {
                        // thanh toán
                        var fNguonTT_SDTUKP = dataNguonTT_SDTUKP.Where(x => x.SMaNguon == NhTongHopConstants.MA_309).Sum(x => x.FGiaTriVnd) - dataNguonTT_SDTUKP.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_309).Sum(x => x.FGiaTriVnd);
                        var fNguonTUKP_NTCS_NN = dataTUKP_NTCS_NN.Where(x => lstMaNguon.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataTUKP_NTCS_NN.Where(x => lstMaNguon.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                        Model.FSoDuTamUng = (fNguonTT_SDTUKP ?? 0) + (fNguonTUKP_NTCS_NN ?? 0);
                        // thanh toán chi tiết
                        var fNguonTT_LK_KPDC_KD_CKT = dataNguonTT_LK_KPDC_KD_CKT.Where(x => x.SMaNguon == NhTongHopConstants.MA_306)
                            .Sum(x => x.FGiaTriVnd) - dataNguonTT_LK_KPDC_KD_CKT.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriVnd) ?? 0;

                        var fNguonSumTh = dataNguonSumTh.Where(x => lstMaNguonSum.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataNguonSumTh.Where(x => lstMaNguonSum.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd) ?? 0;
                        var fNguonMinusTh = dataNguonMinusTh.Where(x => lstMaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataNguonMinusTh.Where(x => lstMaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd) ?? 0;

                        Model.FTongDuocCapKyTruocVND = fNguonTT_LK_KPDC_KD_CKT + fNguonSumTh - fNguonMinusTh;
                        Model.FTongDuocCapKyTruocUSD = ConvertConcurency(LoaiTienTeEnum.TypeCode.VND, LoaiTienTeEnum.TypeCode.USD, (double)Model.FTongDuocCapKyTruocVND);
                        Model.FTongDuocCapKyTruocEUR = 0;
                        Model.FTongDuocCapKyTruocNgoaiTeKhac = 0;
                    }
                    else
                    {
                        var fNguonTT_SDTUKP = dataNguonTT_SDTUKP.Where(x => x.SMaNguon == NhTongHopConstants.MA_309).Sum(x => x.FGiaTriUsd) - dataNguonTT_SDTUKP.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_309).Sum(x => x.FGiaTriUsd);
                        var fNguonTUKP_NTCS_NN = dataTUKP_NTCS_NN.Where(x => lstMaNguon.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataTUKP_NTCS_NN.Where(x => lstMaNguon.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                        Model.FSoDuTamUng = (fNguonTT_SDTUKP ?? 0) + (fNguonTUKP_NTCS_NN ?? 0);
                        // thanh toán chi tiết
                        var fNguonTT_LK_KPDC_KD_CKT = dataNguonTT_LK_KPDC_KD_CKT.Where(x => x.SMaNguon == NhTongHopConstants.MA_306)
                            .Sum(x => x.FGiaTriUsd) - dataNguonTT_LK_KPDC_KD_CKT.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriUsd) ?? 0;

                        var fNguonSumTh = dataNguonSumTh.Where(x => lstMaNguonSum.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataNguonSumTh.Where(x => lstMaNguonSum.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd) ?? 0;
                        var fNguonMinusTh = dataNguonMinusTh.Where(x => lstMaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataNguonMinusTh.Where(x => lstMaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd) ?? 0;

                        Model.FTongDuocCapKyTruocUSD = fNguonTT_LK_KPDC_KD_CKT + fNguonSumTh - fNguonMinusTh;
                        Model.FTongDuocCapKyTruocVND = ConvertConcurency(LoaiTienTeEnum.TypeCode.USD, LoaiTienTeEnum.TypeCode.VND, (double)Model.FTongDuocCapKyTruocUSD);
                        Model.FTongDuocCapKyTruocEUR = 0;
                        Model.FTongDuocCapKyTruocNgoaiTeKhac = 0;
                    }

                }
                else if (SelectedLoaiDeNghi != null)
                {
                    var dataNguonTT_SDTUCD = ItemsTongHops.Where(x => (x.SMaNguon == NhTongHopConstants.MA_310 || x.SMaNguonCha == NhTongHopConstants.MA_310) && x.INamKeHoach == Model.INamKeHoach - 1);
                    lstMaNguon = new List<string> { NhTongHopConstants.MA_121, NhTongHopConstants.MA_122 };
                    var dataTUCD_NTCS = ItemsTongHops.Where(x => (lstMaNguon.Contains(x.SMaNguon) || lstMaNguon.Contains(x.SMaNguonCha)) && x.INamKeHoach == Model.INamKeHoach);
                    lstMaNguonMinus = new List<string> { NhTongHopConstants.MA_131, NhTongHopConstants.MA_132 };
                    var dataTUCD_NN = ItemsTongHops.Where(x => (lstMaNguonMinus.Contains(x.SMaNguon) || lstMaNguonMinus.Contains(x.SMaNguonCha)) && x.INamKeHoach == Model.INamKeHoach);

                    if (iNoiDungChi == (int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE)
                    {
                        //Thanh toan
                        var fNguonTT_SDTUCD = dataNguonTT_SDTUCD.Where(x => x.SMaNguon == NhTongHopConstants.MA_310).Sum(x => x.FGiaTriVnd) - dataNguonTT_SDTUCD.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_310).Sum(x => x.FGiaTriVnd);
                        var fNguonTUCD_NTCS = dataTUCD_NTCS.Where(x => lstMaNguon.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataTUCD_NTCS.Where(x => lstMaNguon.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                        var fNguonTUCD_NN = dataTUCD_NN.Where(x => lstMaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataTUCD_NN.Where(x => lstMaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                        Model.FSoDuTamUng = (fNguonTT_SDTUCD ?? 0) + (fNguonTUCD_NTCS ?? 0) - (fNguonTUCD_NN ?? 0);
                        // thanh toán chi tiết
                        var fNguonTT_LK_KPDC_KD_CKT = dataNguonTT_LK_KPDC_KD_CKT.Where(x => x.SMaNguon == NhTongHopConstants.MA_306)
                            .Sum(x => x.FGiaTriVnd) - dataNguonTT_LK_KPDC_KD_CKT.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriVnd) ?? 0;

                        var fNguonSumTh = dataNguonSumTh.Where(x => lstMaNguonSum.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataNguonSumTh.Where(x => lstMaNguonSum.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd) ?? 0;
                        var fNguonMinusTh = dataNguonMinusTh.Where(x => lstMaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataNguonMinusTh.Where(x => lstMaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd) ?? 0;

                        Model.FTongDuocCapKyTruocVND = fNguonTT_LK_KPDC_KD_CKT + fNguonSumTh - fNguonMinusTh;
                        Model.FTongDuocCapKyTruocUSD = ConvertConcurency(LoaiTienTeEnum.TypeCode.VND, LoaiTienTeEnum.TypeCode.USD, (double)Model.FTongDuocCapKyTruocVND);
                        Model.FTongDuocCapKyTruocEUR = 0;
                        Model.FTongDuocCapKyTruocNgoaiTeKhac = 0;
                    }
                    else
                    {
                        var fNguonTT_SDTUCD = dataNguonTT_SDTUCD.Where(x => x.SMaNguon == NhTongHopConstants.MA_310)
                                            .Sum(x => x.FGiaTriUsd) - dataNguonTT_SDTUCD.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_310).Sum(x => x.FGiaTriUsd);
                        var fNguonTUCD_NTCS = dataTUCD_NTCS.Where(x => lstMaNguon.Contains(x.SMaNguon))
                                            .Sum(x => x.FGiaTriUsd) - dataTUCD_NTCS.Where(x => lstMaNguon.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                        var fNguonTUCD_NN = dataTUCD_NN.Where(x => lstMaNguonMinus.Contains(x.SMaNguon))
                                            .Sum(x => x.FGiaTriUsd) - dataTUCD_NN.Where(x => lstMaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                        Model.FSoDuTamUng = (fNguonTT_SDTUCD ?? 0) + (fNguonTUCD_NTCS ?? 0) - (fNguonTUCD_NN ?? 0);

                        // thanh toán chi tiết
                        var fNguonTT_LK_KPDC_KD_CKT = dataNguonTT_LK_KPDC_KD_CKT.Where(x => x.SMaNguon == NhTongHopConstants.MA_306)
                            .Sum(x => x.FGiaTriUsd) - dataNguonTT_LK_KPDC_KD_CKT.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriUsd) ?? 0;

                        var fNguonSumTh = dataNguonSumTh.Where(x => lstMaNguonSum.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataNguonSumTh.Where(x => lstMaNguonSum.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd) ?? 0;
                        var fNguonMinusTh = dataNguonMinusTh.Where(x => lstMaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataNguonMinusTh.Where(x => lstMaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd) ?? 0;

                        Model.FTongDuocCapKyTruocUSD = fNguonTT_LK_KPDC_KD_CKT + fNguonSumTh - fNguonMinusTh;
                        Model.FTongDuocCapKyTruocVND = ConvertConcurency(LoaiTienTeEnum.TypeCode.USD, LoaiTienTeEnum.TypeCode.VND, (double)Model.FTongDuocCapKyTruocUSD);
                        Model.FTongDuocCapKyTruocEUR = 0;
                        Model.FTongDuocCapKyTruocNgoaiTeKhac = 0;
                    }
                }


                if (ItemsNhTtThanhToanChiTiet != null && ItemsNhTtThanhToanChiTiet.Any())
                {
                    foreach (var item in ItemsNhTtThanhToanChiTiet)
                    {
                        item.FDuocCapKyTruocUsd = Model.FTongDuocCapKyTruocUSD;
                        item.FDuocCapKyTruocVnd = Model.FTongDuocCapKyTruocVND;
                        item.FDuocCapKyTruocEur = Model.FTongDuocCapKyTruocEUR;
                        item.FDuocCapKyTruocNgoaiTeKhac = Model.FTongDuocCapKyTruocNgoaiTeKhac;
                    }
                }
            }
            else
            {
                Model.FSoDuTamUng = 0;
                Model.FTongDuocCapKyTruocUSD = 0;
                Model.FTongDuocCapKyTruocVND = 0;
                Model.FTongDuocCapKyTruocEUR = 0;
                Model.FTongDuocCapKyTruocNgoaiTeKhac = 0;
            }

            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(ItemsNhTtThanhToanChiTiet));
        }
    }
}
