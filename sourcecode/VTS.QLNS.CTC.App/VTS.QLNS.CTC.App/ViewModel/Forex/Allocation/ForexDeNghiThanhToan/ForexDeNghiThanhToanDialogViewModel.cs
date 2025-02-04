using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using AutoMapper;
using log4net;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexAllocation.ForexDeNghiThanhToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexDeNghiThanhToan
{
    public class ForexDeNghiThanhToanDialogViewModel : DialogCurrencyAttachmentViewModelBase<NhTtThanhToanModel>
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
        private readonly INhDaQuyetDinhKhacChiPhiService _nhDaQuyetDinhKhacChiPhiService;
        private readonly INhDaHopDongChiPhiService _nhDaHopDongChiPhiService;
        private readonly INhDaQdDauTuChiPhiService _nhDaQdDauTuChiPhiService;

        private static Dictionary<string, Guid> _dicMucLucNganSach = new Dictionary<string, Guid>();

        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.NH_DE_NGHI_THANH_TOAN;
        public override Type ContentType => typeof(ForexDeNghiThanhToanDialog);
        public bool IsDetail { get; set; }
        public bool IsHiddenDuAn { get; set; }
        public bool IsHiddenDuAnChiPhi { get; set; }
        public bool IsHiddenDuAnHopDong { get; set; }
        //public bool IsHiddenHopDongChiPhi { get; set; }
        public bool IsHiddenHopDong { get; set; }
        public bool IsHiddenQuyetDinhCPK { get; set; }
        public bool IsHiddenChiPhiQdk { get; set; }
        private bool _isUsd;
        public bool IsUsd
        {
            get => _isUsd;
            set => SetProperty(ref _isUsd, value);
        }
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
                LoadKeHoachTongThe();
                //LoadNhiemVuChi();
                //LoadThongTinLuyKe();
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
                LoadQuyetDinhChiPhiKhac();
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
                //LoadDADuAn();
                //LoadThongTinLuyKe();
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
                SetGiaTriHopDongDuToanQuyetDinhKhac();
                //SetGiaTriKinhPhiKyTruoc();
                LoadThongTinLuyKe();
                LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                CalculateDataDeNghiThanhToanTongHop();
                LoadDataChiPhi();
                LoadDADuAn();
            }
        }

        private ObservableCollection<NhDaQuyetDinhKhacModel> _itemsChiPhiKhac;
        public ObservableCollection<NhDaQuyetDinhKhacModel> ItemsChiPhiKhac
        {
            get => _itemsChiPhiKhac;
            set => SetProperty(ref _itemsChiPhiKhac, value);
        }

        private NhDaQuyetDinhKhacModel _selectedChiPhiKhac;
        public NhDaQuyetDinhKhacModel SelectedChiPhiKhac
        {
            get => _selectedChiPhiKhac;
            set
            {
                SetProperty(ref _selectedChiPhiKhac, value);
                //SetGiaTriHopDongDuocDuyet();
                //SetGiaTriKinhPhiKyTruoc();
                SetGiaTriHopDongDuToanQuyetDinhKhac();
                LoadThongTinLuyKe();
                LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                CalculateDataDeNghiThanhToanTongHop();
                LoadDataChiPhi();
            }
        }

        private ObservableCollection<NhDaQuyetDinhKhacChiPhiModel> _itemsChiPhiQDKhac;
        public ObservableCollection<NhDaQuyetDinhKhacChiPhiModel> ItemsChiPhiQDKhac
        {
            get => _itemsChiPhiQDKhac;
            set => SetProperty(ref _itemsChiPhiQDKhac, value);
        }

        private NhDaQuyetDinhKhacChiPhiModel _selectedChiPhiQDKhac;
        public NhDaQuyetDinhKhacChiPhiModel SelectedChiPhiQDKhac
        {
            get => _selectedChiPhiQDKhac;
            set
            {
                SetProperty(ref _selectedChiPhiQDKhac, value);
            }
        }

        private ObservableCollection<NhDaHopDongChiPhi> _itemsChiPhiHopDong;
        public ObservableCollection<NhDaHopDongChiPhi> ItemsChiPhiHopDong
        {
            get => _itemsChiPhiHopDong;
            set => SetProperty(ref _itemsChiPhiHopDong, value);
        }

        private NhDaHopDongChiPhi _selectedChiPhihopDong;
        public NhDaHopDongChiPhi SelectedChiPhiHopDong
        {
            get => _selectedChiPhihopDong;
            set
            {
                SetProperty(ref _selectedChiPhihopDong, value);
                //SetGiaTriHopDongDuocDuyet();
                //SetGiaTriKinhPhiKyTruoc();
                //LoadThongTinLuyKe();
                //LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                //CalculateDataDeNghiThanhToanTongHop();
            }
        }


        private ObservableCollection<NhDaQdDauTuChiPhi> _itemsChiPhiDuAn;
        public ObservableCollection<NhDaQdDauTuChiPhi> ItemsChiPhiDuAn
        {
            get => _itemsChiPhiDuAn;
            set => SetProperty(ref _itemsChiPhiDuAn, value);
        }

        private NhDaQdDauTuChiPhi _selectedChiPhiDuAn;
        public NhDaQdDauTuChiPhi SelectedChiPhiDuAn
        {
            get => _selectedChiPhiDuAn;
            set
            {
                SetProperty(ref _selectedChiPhiDuAn, value);
                //SetGiaTriHopDongDuocDuyet();
                //SetGiaTriKinhPhiKyTruoc();
                //LoadThongTinLuyKe();
                //LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                //CalculateDataDeNghiThanhToanTongHop();
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
                LoadDataChiPhi();
                LoadThongTinLuyKe();
                //Model.PropertyChanged += ThanhToan_PropertyChanged;
                //Model.IIdDuAnId = value.Id;
                LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                CalculateDataDeNghiThanhToanTongHop();
                SetGiaTriHopDongDuToanQuyetDinhKhac();
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
                    Model.PropertyChanged += ThanhToan_PropertyChanged;
                    Model.ILoaiDeNghi = int.Parse(value.ValueItem.ToString());
                    LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                    CalculateDataDeNghiThanhToanTongHop();
                }
            }
        }

        private int? _iNamKeHoach;
        public int? INamKeHoach
        {
            get => _iNamKeHoach;
            set
            {
                SetProperty(ref _iNamKeHoach, value);
                if (value != null && 999 < value && value < 10000)
                {
                    Model.PropertyChanged += ThanhToan_PropertyChanged;
                    //Model.ILoaiDeNghi = int.Parse(value.ValueItem.ToString());
                    LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                    CalculateDataDeNghiThanhToanTongHop();
                    Model.INamKeHoach = value;
                }
            }

        }
        private ObservableCollection<ComboboxItem> _itemsNamNganSach;
        public ObservableCollection<ComboboxItem> ItemsNamNganSach
        {
            get => _itemsNamNganSach;
            set
            {
                SetProperty(ref _itemsNamNganSach, value);
                Model.PropertyChanged += ThanhToan_PropertyChanged;
                LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                CalculateDataDeNghiThanhToanTongHop();
            }
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

        private ComboboxItem _selectedCoQuanThanhToan;
        public ComboboxItem SelectedCoQuanThanhToan
        {
            get => _selectedCoQuanThanhToan;
            set
            {
                SetProperty(ref _selectedCoQuanThanhToan, value);
            }
        }

        public ComboboxItem _selectedThanhToanTheo;
        public ComboboxItem SelectedThanhToanTheo
        {
            get => _selectedThanhToanTheo;
            set
            {
                SetProperty(ref _selectedThanhToanTheo, value);
                LoadDAHDTheoThanhToan();
                LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                CalculateDataDeNghiThanhToanTongHop();
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
                Model.PropertyChanged += ThanhToan_PropertyChanged;
                LoadDataNhTongHop(Model.INamKeHoach ?? 1);
                CalculateDataDeNghiThanhToanTongHop();
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
                    LoadChiTietNhaThau();
                }
                else
                {
                    IsFirstShow = false;
                }
            }
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
        public string _sTiTleDuocDuyet;

        public string STiTleDuocDuyet
        {
            get => _sTiTleDuocDuyet;
            set => SetProperty(ref _sTiTleDuocDuyet, value);
        }

        public IEnumerable<NHTHTongHop> ItemsTongHops;
        public string SoDuTamUngLabel { get; set; }
        public bool IsFirstShow { get; set; }
        public SelectMLNSDialogViewModel SelectMLNSDialogViewModel { get; set; }
        public RelayCommand AddThongTinThanhToanCommand { get; set; }
        public RelayCommand DeleteThongTinThanhToanCommand { get; set; }
        public RelayCommand SelectMLNSCommand { get; set; }

        public ForexDeNghiThanhToanDialogViewModel(
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
            INhTtThanhToanService iNhTtThanhToanService,
            SelectMLNSDialogViewModel selectMLNSDialogViewModel,
            INhTtThanhToanChiTietService iNhTtThanhToanChiTietService,
            INhThTongHopService nhThTongHopService,
            INhDaQuyetDinhKhacService nhDaQuyetDinhKhacService,
            INhDaQuyetDinhKhacChiPhiService nhDaQuyetDinhKhacChiPhiService,
            INhDaHopDongChiPhiService nhDaHopDongChiPhiService,
            INhDaQdDauTuChiPhiService nhDaQdDauTuChiPhiService) : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
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
            _iNhTtThanhToanService = iNhTtThanhToanService;
            _iNhTtThanhToanChiTietService = iNhTtThanhToanChiTietService;
            _nhThTongHopService = nhThTongHopService;
            _nhDaQuyetDinhKhacService = nhDaQuyetDinhKhacService;
            SelectMLNSDialogViewModel = selectMLNSDialogViewModel;
            _nhDaQuyetDinhKhacChiPhiService = nhDaQuyetDinhKhacChiPhiService;
            _nhDaQdDauTuChiPhiService = nhDaQdDauTuChiPhiService;
            _nhDaHopDongChiPhiService = nhDaHopDongChiPhiService;

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
            //LoadKeHoachTongThe();
            LoadChuDauTu();
            LoadNhiemVuChi();
            LoadDuAnHopDong();
            LoadDADuAn();
            LoadLoaiDeNghi();
            LoadQuyKeHoach();
            LoadNguonVon();
            LoadItemsTiGia();
            LoadAttach();
            LoadLoaiNoiDungChi();
            LoadCoQuanThanhToan();
            LoadThanhToanTheo();
            LoadDmNhaThau();
            LoadNamNganSach();
            LoadData();
            LoadValueLoaiNoiDungChi();
            LoadDAHDTheoThanhToan();
            OnPropertyChanged(nameof(ItemsNhTtThanhToanChiTiet));
            OnPropertyChanged(nameof(Model));
            Model.PropertyChanged += ThanhToan_PropertyChanged;
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

        private void LoadChiTietNhaThau()
        {
            if (_selectedDmNhaThau != null && !IsDetail)
            {
                Model.SSoTaiKhoan = _selectedDmNhaThau.SSoTaiKhoan;
                Model.SNganHang = _selectedDmNhaThau.SNganHang;
                Model.SNguoiLienHe = _selectedDmNhaThau.SNguoiLienHe;
                Model.SSoCmnd = _selectedDmNhaThau.SSoCmnd;
                Model.SNoiCapCmnd = _selectedDmNhaThau.SNoiCapCmnd;
                Model.SSoTaiKhoan = _selectedDmNhaThau.SSoTaiKhoan;
                Model.DNgayCapCmnd = _selectedDmNhaThau.DNgayCapCmnd;
            }
        }

        private void QuiDoiTienKhiDoiLoaiNoiDungChi()
        {
            Model.PropertyChanged -= ThanhToan_PropertyChanged;
            if (_selectedLoaiNoiDungChi != null &&
                _selectedLoaiNoiDungChi.ValueItem.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()))
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
            }
            GenerateTienBangChu();
            Model.PropertyChanged += ThanhToan_PropertyChanged;
        }

        private void GenerateTienBangChu()
        {
            if (_selectedLoaiNoiDungChi != null &&
                _selectedLoaiNoiDungChi.ValueItem.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()))
            {
                Model.STongDeNghiBangChu = StringUtils.NumberToText(Model.FTongDeNghiBangSo.GetValueOrDefault(), false);
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
                var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                string rootCurrency = SelectedTiGia.SMaTienTeGoc;
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

        private void LoadKeHoachTongThe()
        {
            var lstKeHoachTongThe = new List<NhKhTongThe>();
            if (SelectedDonVi != null) lstKeHoachTongThe = _iNhKhTongTheService.FindByDonViId(SelectedDonVi.Id).ToList();
            if (lstKeHoachTongThe.Any())
            {
                var result = lstKeHoachTongThe.Select(x =>
                {
                    ComboboxItem cb = new ComboboxItem();
                    if (x.INamKeHoach.HasValue)
                    {
                        cb.DisplayItem = "KHTT " + x.INamKeHoach.Value + "- Số KH: " + x.SSoKeHoachBqp;
                        cb.ValueItem = x.Id.ToString();
                        cb.Id = x.Id;
                    }
                    else
                    {
                        cb.DisplayItem = "KHTT " + x.IGiaiDoanTu_BQP.GetValueOrDefault() + "-" + x.IGiaiDoanDen_BQP.GetValueOrDefault() + "- Số KH: " + x.SSoKeHoachBqp;
                        cb.ValueItem = x.Id.ToString();
                        cb.Id = x.Id;
                    }
                    return cb;
                }).ToList();
                ItemsKeHoachTongThe = new ObservableCollection<ComboboxItem>(result);
            }
            else
            {
                ItemsKeHoachTongThe = new ObservableCollection<ComboboxItem>();
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
                var lstNhiemVuChi = _iDmNhiemVuChiService.FindAll().Where(x => lstKHNhiemVuChi.Select(x => x.IIdNhiemVuChiId).Contains(x.Id)).ToList();

                ItemsNhiemVuChi = new ObservableCollection<NhDmNhiemVuChiModel>(_mapper.Map<List<NhDmNhiemVuChiModel>>(lstNhiemVuChi));
            }
        }

        private void LoadQuyetDinhChiPhiKhac()
        {
            ItemsChiPhiKhac = new ObservableCollection<NhDaQuyetDinhKhacModel>();
            var predicate = PredicateBuilder.True<NhDaQuyetDinhKhac>();
            if (_selectedKeHoachTongThe != null && _selectedNhiemVuChi != null)
            {
                var lstKhttNhiemVuChi = GetListIdKhTongTheNhiemVuChi();
                predicate = predicate.And(x => lstKhttNhiemVuChi.Select(s => s.Id).Contains(x.IIdKHTTNhiemVuChiId ?? Guid.Empty));
            }
            ItemsChiPhiKhac = new ObservableCollection<NhDaQuyetDinhKhacModel>(_mapper.Map<List<NhDaQuyetDinhKhacModel>>(_nhDaQuyetDinhKhacService.FindByCondition(predicate)));

        }

        private void LoadDataChiPhi()
        {
            // Load chi phi quyet dinh khác
            var predicate = PredicateBuilder.True<NhDaQuyetDinhKhacChiPhi>();
            if (SelectedChiPhiKhac != null)
            {
                predicate = predicate.And(x => x.IIdQuyetDinhKhacId.Equals(SelectedChiPhiKhac.Id));
                ItemsChiPhiQDKhac = new ObservableCollection<NhDaQuyetDinhKhacChiPhiModel>(_mapper.Map<List<NhDaQuyetDinhKhacChiPhiModel>>(_nhDaQuyetDinhKhacChiPhiService.FindByCondition(predicate)));
            }
            if (SelectedDaHopDong != null)
            {
                var chiPhiHDs = _nhDaHopDongChiPhiService.FindByIdHopDong(SelectedDaHopDong.Id);
                ItemsChiPhiHopDong = new ObservableCollection<NhDaHopDongChiPhi>(chiPhiHDs);
            }
            if (SelectedDaDuAn != null)
            {
                var chiphiDA = _nhDaQdDauTuChiPhiService.FindByQdDauTuByDuAnId(SelectedDaDuAn.Id);
                ItemsChiPhiDuAn = new ObservableCollection<NhDaQdDauTuChiPhi>(chiphiDA);
            }
        }

        private List<NhKhTongTheNhiemVuChi> GetListIdKhTongTheNhiemVuChi()
        {
            var predicate = PredicateBuilder.True<NhKhTongTheNhiemVuChi>();
            predicate = predicate.And(x => x.IIdKhTongTheId.ToString().Equals(_selectedKeHoachTongThe.ValueItem));
            predicate = predicate.And(x => x.IIdDonViThuHuongId.Equals(_selectedDonVi.Id));
            var lstKHNhiemVuChi = _iNhKhTongTheService.FindKHTongTheNVCByConditon(predicate);
            if (lstKHNhiemVuChi.Any())
            {
                return lstKHNhiemVuChi.ToList();
            }
            return new List<NhKhTongTheNhiemVuChi>();
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
            var results = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem(LoaiNoiDungChi.Get((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE), ((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()),
                new ComboboxItem(LoaiNoiDungChi.Get((int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE), ((int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE).ToString())
            };
            _itemsLoaiNoiDungChi = results;
            _selectedLoaiNoiDungChi = Model.Id == Guid.Empty ? _itemsLoaiNoiDungChi[1] : _selectedLoaiNoiDungChi;
        }

        private void LoadCoQuanThanhToan()
        {
            var results = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem(NhCoQuanThanhToan.Get((int)NhCoQuanThanhToan.Type.CTC_CAP), ((int)NhCoQuanThanhToan.Type.CTC_CAP).ToString()),
                new ComboboxItem(NhCoQuanThanhToan.Get((int)NhCoQuanThanhToan.Type.DON_VI_CAP), ((int)NhCoQuanThanhToan.Type.DON_VI_CAP).ToString())
            };
            _itemsCoQuanThanhToan = results;
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

        private void LoadDmNhaThau()
        {
            var results = _iNhDmNhaThauService.FindAll();
            _itemsDmNhaThau = new ObservableCollection<NhDmNhaThauModel>(_mapper.Map<ObservableCollection<NhDmNhaThauModel>>(results));
        }

        public override void LoadData(params object[] args)
        {
            GetDicMucLucNganSach();
            if (Model.Id.IsNullOrEmpty())
            {
                Title = "ĐỀ NGHỊ CẤP PHÁT, THANH TOÁN";
                Description = "Thêm mới thông tin đề nghị cấp phát, thanh toán kinh phí nguồn quỹ dự trữ ngoại hối";
            }
            else
            {
                NhTtThanhToan entity = _iNhTtThanhToanService.FindById(Model.Id);
                Model = _mapper.Map<NhTtThanhToanModel>(entity);
                if (IsDetail)
                {
                    Title = "ĐỀ NGHỊ CẤP PHÁT, THANH TOÁN";
                    Description = "Chi tiết thông tin đề nghị cấp phát, thanh toán kinh phí nguồn quỹ dự trữ ngoại hối";
                }
                else
                {
                    Title = "ĐỀ NGHỊ CẤP PHÁT, THANH TOÁN";
                    Description = "Cập nhật thông tin đề nghị cấp phát, thanh toán kinh phí nguồn quỹ dự trữ ngoại hối";
                }

                IsFirstShow = true;
                SelectedDonViCapTren = ItemsDonViCapTren.FirstOrDefault(x => x.Id == Model.IIdDonViCapTren);
                SelectedDonVi = ItemsDonVi.FirstOrDefault(x => x.Id == Model.IIdDonVi);
                SelectedKeHoachTongThe = ItemsKeHoachTongThe.FirstOrDefault(x => x.ValueItem.ToString().Equals(Model.IIdKhtongTheId.GetValueOrDefault().ToString()));
                SelectedNhiemVuChi = ItemsNhiemVuChi.FirstOrDefault(x => x.Id == Model.IIdNhiemVuChiId);
                SelectedChuDauTu = ItemsChuDauTu.FirstOrDefault(x => x.Id == Model.IIdChuDauTuId);
                SelectedDaHopDong = ItemsDaHopDong.FirstOrDefault(x => x.Id == Model.IIdHopDongId);
                SelectedLoaiDeNghi = ItemsLoaiDeNghi.FirstOrDefault(x => x.ValueItem.Equals(Model.ILoaiDeNghi.ToString()));
                SelectedQuyKeHoach = ItemsQuyKeHoach.FirstOrDefault(x => x.ValueItem.Equals(Model.IQuyKeHoach.GetValueOrDefault().ToString()));
                SelectedNguonVon = ItemsNguonVon.FirstOrDefault(x => x.IIdMaNguonNganSach == Model.IIdNguonVonId);
                SelectedDmNhaThau = ItemsDmNhaThau.FirstOrDefault(x => x.Id == Model.IIdNhaThauId);
                _selectedLoaiNoiDungChi = ItemsLoaiNoiDungChi.FirstOrDefault(x => x.ValueItem.Equals(Model.ILoaiNoiDungChi.GetValueOrDefault().ToString()));
                SelectedCoQuanThanhToan = ItemsCoQuanThanhToan.FirstOrDefault(x => x.ValueItem.Equals(Model.ICoQuanThanhToan.GetValueOrDefault().ToString()));
                SelectedThanhToanTheo = ItemsThanhToanTheo.FirstOrDefault(x => x.ValueItem.Equals(Model.IThanhToanTheo.GetValueOrDefault().ToString()));
                SelectedDaDuAn = ItemsDaDuAn.FirstOrDefault(x => x.Id == Model.IIdDuAnId);
                SelectedNamNganSach = ItemsNamNganSach.FirstOrDefault(x => x.ValueItem.Equals(Model.INamNganSach.GetValueOrDefault().ToString()));
                SelectedChiPhiDuAn = ItemsChiPhiDuAn.IsEmpty() ? null : ItemsChiPhiDuAn.FirstOrDefault(x => x.Id.Equals(Model.IIdDuAnChiPhiId ?? Guid.Empty));
                SelectedChiPhiHopDong = ItemsChiPhiHopDong.IsEmpty() ? null : ItemsChiPhiHopDong.FirstOrDefault(x => x.Id.Equals(Model.IIdHopDongChiPhiId ?? Guid.Empty));
                SelectedChiPhiKhac = ItemsChiPhiKhac.IsEmpty() ? null : ItemsChiPhiKhac.FirstOrDefault(x => x.Id.Equals(Model.IIdQuyetDinhKhacId));
                SelectedChiPhiQDKhac = ItemsChiPhiQDKhac.IsEmpty() ? null : ItemsChiPhiQDKhac.FirstOrDefault(x => x.Id.Equals(Model.IIdQuyetDinhKhacChiPhiId ?? Guid.Empty));
                // Load tỉ giá và ngoại tệ
                SelectedTiGia = ItemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
                LoadTiGiaChiTiet();
                SelectedTiGiaChiTiet = ItemsTiGiaChiTiet.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
                LoadDAHDTheoThanhToan();
            }

            LoadThanhToanChiTiet();
            SetGiaTriHopDongDuToanQuyetDinhKhac();
            //SetGiaTriKinhPhiKyTruoc();
            LoadThongTinLuyKe();
            INamKeHoach = Model.INamKeHoach;
            Model.PropertyChanged += ThanhToan_PropertyChanged;
        }

        public override void OnSave(object obj)
        {
            ConvertData();
            if (!ValidateViewModelHelper.Validate(Model)) return;
            if (!ValidateData()) return;
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                NhTtThanhToan entity;
                if (Model.Id.IsNullOrEmpty())
                {
                    entity = _mapper.Map<NhTtThanhToan>(Model);
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.ITrangThai = 1;
                    _iNhTtThanhToanService.Add(entity);
                }
                else
                {
                    entity = _iNhTtThanhToanService.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    _iNhTtThanhToanService.Update(entity);
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
                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);

                    var view = obj as Window;
                    if (view != null) view.Close();

                    SavedAction?.Invoke(Model);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            });
        }

        public void ConvertData()
        {
            if (SelectedDonViCapTren != null)
            {
                Model.IIdDonViCapTren = SelectedDonViCapTren.Id;
                Model.IIdMaDonViCapTren = SelectedDonViCapTren.IIDMaDonVi;
            }
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
            if (SelectedQuyKeHoach != null)
            {
                Model.IQuyKeHoach = int.Parse(SelectedQuyKeHoach.ValueItem);
            }
            if (SelectedNguonVon != null)
            {
                Model.IIdNguonVonId = SelectedNguonVon.IIdMaNguonNganSach;
            }
            if (SelectedTiGia != null)
            {
                Model.IIdTiGiaId = SelectedTiGia.Id;
            }
            if (SelectedTiGiaChiTiet != null)
            {
                Model.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
            }
            if (SelectedCoQuanThanhToan != null)
            {
                Model.ICoQuanThanhToan = int.Parse(SelectedCoQuanThanhToan.ValueItem);
            }
            if (SelectedLoaiNoiDungChi != null)
            {
                Model.ILoaiNoiDungChi = int.Parse(SelectedLoaiNoiDungChi.ValueItem);
            }
            if (SelectedDmNhaThau != null)
            {
                Model.IIdNhaThauId = SelectedDmNhaThau.Id;
            }
            if (SelectedNamNganSach != null)
            {
                Model.INamNganSach = int.Parse(SelectedNamNganSach.ValueItem);
            }
            if (SelectedThanhToanTheo != null)
            {
                Model.IThanhToanTheo = int.TryParse(SelectedThanhToanTheo.ValueItem, out int idthanhtoan) ? idthanhtoan : (int?)null;
            }
            if (SelectedChiPhiKhac != null)
            {
                Model.IIdQuyetDinhKhacId = SelectedChiPhiKhac.Id;
            }
            if (SelectedChiPhiDuAn != null)
            {
                Model.IIdDuAnChiPhiId = SelectedChiPhiDuAn.Id;
            }
            if (SelectedChiPhiHopDong != null)
            {
                Model.IIdHopDongChiPhiId = SelectedChiPhiHopDong.Id;
            }
            if (SelectedChiPhiQDKhac != null)
            {
                Model.IIdQuyetDinhKhacChiPhiId = SelectedChiPhiQDKhac.Id;
            }
            //Dự án, hợp đồng
            Model.IIdHopDongId = SelectedDaHopDong != null ? SelectedDaHopDong.Id : (Guid?)null;
            Model.IIdDuAnId = SelectedDaDuAn != null ? SelectedDaDuAn.Id : (Guid?)null;
            foreach (var item in ItemsNhTtThanhToanChiTiet)
            {
                string sXauNoiChuoi = item.SLns + "-" + item.SL + "-" + item.SK + "-" + item.SM + "-" + item.STm + "-" + item.STtm;
                if (_dicMucLucNganSach.ContainsKey(sXauNoiChuoi))
                {
                    item.IIdMucLucNganSachId = _dicMucLucNganSach[sXauNoiChuoi];
                }
            }
            Model.NhTtThanhToanChiTiets = new ObservableCollection<NhTtThanhToanChiTietModel>(ItemsNhTtThanhToanChiTiet);
            //Thông tin lũy kế
            Model.FLuyKeVND = !Model.NhTtThanhToanChiTiets.IsEmpty() ? Model.NhTtThanhToanChiTiets[0].FDuocCapKyTruocVnd : 0;
            Model.FLuyKeUSD = !Model.NhTtThanhToanChiTiets.IsEmpty() ? Model.NhTtThanhToanChiTiets[0].FDuocCapKyTruocUsd : 0;
            Model.FLuyKeEUR = !Model.NhTtThanhToanChiTiets.IsEmpty() ? Model.NhTtThanhToanChiTiets[0].FDuocCapKyTruocEur : 0;
            Model.FLuyKeNgoaiTeKhac = !Model.NhTtThanhToanChiTiets.IsEmpty() ? Model.NhTtThanhToanChiTiets[0].FDuocCapKyTruocNgoaiTeKhac : 0;

            //Kinh phí đề nghị cấp
            Model.FTongDeNghiUSD = !Model.NhTtThanhToanChiTiets.IsEmpty() ? Model.NhTtThanhToanChiTiets.Sum(x => x.FDeNghiCapKyNayUsd) : 0;
            Model.FTongDeNghiVND = !Model.NhTtThanhToanChiTiets.IsEmpty() ? Model.NhTtThanhToanChiTiets.Sum(x => x.FDeNghiCapKyNayVnd) : 0;
            Model.FTongDeNghiEUR = !Model.NhTtThanhToanChiTiets.IsEmpty() ? Model.NhTtThanhToanChiTiets.Sum(x => x.FDeNghiCapKyNayEur) : 0;
            Model.FTongDeNghiNgoaiTeKhac = !Model.NhTtThanhToanChiTiets.IsEmpty() ? Model.NhTtThanhToanChiTiets.Sum(x => x.FDeNghiCapKyNayNgoaiTeKhac) : 0;

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
            if (SelectedThanhToanTheo != null)
            {
                if (Model.IThanhToanTheo != null && Model.IThanhToanTheo.HasValue)
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
                        case 3:
                            if (SelectedChiPhiKhac == null)
                            {
                                lstError.Add(string.Format(Resources.MsgErrorRequire, "Tên quyết định chi phí khác"));
                            }
                            break;
                    }
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
            targetItem.IsDeNghiThanhToan = true;
            targetItem.FDuocDuyetUsd = Model.FTongDuocDuyetUSD;
            targetItem.FDuocDuyetVnd = Model.FTongDuocDuyetVND;
            targetItem.PropertyChanged += ThanhToanChiTiet_PropertyChanged;
            _itemsNhTtThanhToanChiTiet.Insert(currentRow + 1, targetItem);
            SetGiaTriHopDongDuToanQuyetDinhKhac();
            //SetGiaTriKinhPhiKyTruoc();
            LoadThongTinLuyKe();
            OnPropertyChanged(nameof(ItemsNhTtThanhToanChiTiet));
        }

        public void OnDeleteThongTinThanhToanDetail()
        {
            if (SelectedNhTtThanhToanChiTiet != null)
            {
                SelectedNhTtThanhToanChiTiet.IsDeleted = !SelectedNhTtThanhToanChiTiet.IsDeleted;
                foreach (var item in ItemsNhTtThanhToanChiTiet)
                {
                    if (item.Id == SelectedNhTtThanhToanChiTiet.Id)
                        item.IsDeleted = SelectedNhTtThanhToanChiTiet.IsDeleted;
                }
                CaculateTotalThongTinThanhToan();
                TinhTongSoDeNghiThanhToanKyNay();
                Model.PropertyChanged += ThanhToan_PropertyChanged;
            }
        }

        private void TinhGiaTriThanhToanKyNay(NhTtThanhToanChiTietModel obj)
        {
            obj.FDeNghiCapKyNayEur = obj.FTongGiaTriTheoHoaDonEur.GetValueOrDefault() * obj.FTiLeThanhToan.GetValueOrDefault();
            obj.FDeNghiCapKyNayVnd = obj.FTongGiaTriTheoHoaDonVnd.GetValueOrDefault() * obj.FTiLeThanhToan.GetValueOrDefault();
            obj.FDeNghiCapKyNayUsd = obj.FTongGiaTriTheoHoaDonUsd.GetValueOrDefault() * obj.FTiLeThanhToan.GetValueOrDefault();
            obj.FDeNghiCapKyNayNgoaiTeKhac = obj.FTongGiaTriTheoHoaDonNgoaiTeKhac.GetValueOrDefault() * obj.FTiLeThanhToan.GetValueOrDefault();
        }

        private void CaculateTotalThongTinThanhToan()
        {
            if (!ItemsNhTtThanhToanChiTiet.IsEmpty())
            {
                Model.FTongDeNghiKyNayUsd = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FDeNghiCapKyNayUsd);
                Model.FTongDeNghiKyNayVnd = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FDeNghiCapKyNayVnd);
                Model.FTongDeNghiKyNayEur = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FDeNghiCapKyNayEur);
                Model.FTongDeNghiKyNayNgoaiTeKhac = ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FDeNghiCapKyNayNgoaiTeKhac);
                OnPropertyChanged(nameof(Model));
            }
            else
            {
                Model.FTongDeNghiKyNayUsd = 0;
                Model.FTongDeNghiKyNayVnd = 0;
                Model.FTongDeNghiKyNayEur = 0;
                Model.FTongDeNghiKyNayNgoaiTeKhac = 0;
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

            if (args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.IsDeleted))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FTongGiaTriTheoHoaDonUsd))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FTongGiaTriTheoHoaDonVnd))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FTongGiaTriTheoHoaDonEur))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FTongGiaTriTheoHoaDonNgoaiTeKhac))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FTiLeThanhToan))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FDeNghiCapKyNayUsd))
                || args.PropertyName.Equals(nameof(NhTtThanhToanChiTietModel.FDeNghiCapKyNayVnd)))
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
                        case nameof(NhTtThanhToanChiTietModel.FDeNghiCapKyNayUsd):
                            sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                            value = objSender.FDeNghiCapKyNayUsd.GetValueOrDefault();
                            break;
                        case nameof(NhTtThanhToanChiTietModel.FDeNghiCapKyNayVnd):
                            sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                            value = objSender.FDeNghiCapKyNayVnd.GetValueOrDefault();
                            break;
                        default:
                            sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                            value = objSender.FTongGiaTriTheoHoaDonUsd.GetValueOrDefault();
                            break;
                    }
                    var listTiGiaCT = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
                    var listTiGiaChiTietNew = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(listTiGiaCT);
                    if (args.PropertyName == nameof(NhTtThanhToanChiTietModel.FDeNghiCapKyNayUsd) || args.PropertyName == nameof(NhTtThanhToanChiTietModel.FDeNghiCapKyNayVnd))
                    {
                        objSender.FDeNghiCapKyNayUsd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTietNew, value);
                        objSender.FDeNghiCapKyNayVnd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTietNew, value);
                        objSender.FDeNghiCapKyNayEur = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTietNew, value);
                        objSender.FDeNghiCapKyNayNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTietNew, value);
                        _itemsNhTtThanhToanChiTiet.ForAll(f =>
                        {
                            if (f.IIdDeNghiThanhToanId.Equals(objSender.IIdDeNghiThanhToanId))
                            {
                                f.FDeNghiCapKyNayUsd = objSender.FDeNghiCapKyNayUsd;
                                f.FDeNghiCapKyNayVnd = objSender.FDeNghiCapKyNayVnd;
                                f.FDeNghiCapKyNayEur = objSender.FDeNghiCapKyNayEur;
                                f.FDeNghiCapKyNayNgoaiTeKhac = objSender.FDeNghiCapKyNayNgoaiTeKhac;    
                                f.FGiaTriEurCol4 = objSender.FGiaTriEurChangeCol4;
                                f.FGiaTriUsdCol4 = objSender.FGiaTriUsdChangeCol4;
                                f.FGiaTriVndCol4 = objSender.FGiaTriVndChangeCol4;
                                f.FGiatriNgoaiTeKhacCol4 = objSender.FGiaTriKinhPhiKhacChangeCol4;
                                if (SelectedLoaiNoiDungChi.ValueItem == ((int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE).ToString())
                                    f.FTiLeThanhToan = f.FTiLeThanhToanVNDChange;
                                else
                                    f.FTiLeThanhToan = f.FTiLeThanhToanUsdChange;
                            }
                        });
                    }
                    else
                    {
                        objSender.FTongGiaTriTheoHoaDonVnd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTietNew, value);
                        objSender.FTongGiaTriTheoHoaDonEur = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTietNew, value);
                        objSender.FTongGiaTriTheoHoaDonUsd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTietNew, value);
                        objSender.FTongGiaTriTheoHoaDonNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTietNew, value);
                    }
                    //TinhGiaTriThanhToanKyNay(objSender);
                    ValidateInputThanhToanKyNay(objSender);
                    CaculateTotalThongTinThanhToan();
                    TinhTongSoDeNghiThanhToanKyNay();

                }
                else
                {
                    _itemsNhTtThanhToanChiTiet.ForAll(f =>
                    {
                        if (f.IIdDeNghiThanhToanId.Equals(objSender.IIdDeNghiThanhToanId))
                        {
                            f.FDeNghiCapKyNayUsd = objSender.FDeNghiCapKyNayUsd;
                            f.FDeNghiCapKyNayVnd = objSender.FDeNghiCapKyNayVnd;
                            f.FDeNghiCapKyNayEur = objSender.FDeNghiCapKyNayEur;
                            f.FDeNghiCapKyNayNgoaiTeKhac = objSender.FDeNghiCapKyNayNgoaiTeKhac;
                            f.FGiaTriEurCol4 = objSender.FGiaTriEurChangeCol4;
                            f.FGiaTriUsdCol4 = objSender.FGiaTriUsdChangeCol4;
                            f.FGiaTriVndCol4 = objSender.FGiaTriVndChangeCol4;
                            f.FGiatriNgoaiTeKhacCol4 = objSender.FGiaTriKinhPhiKhacChangeCol4;
                            if (SelectedLoaiNoiDungChi.ValueItem == ((int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE).ToString())
                                f.FTiLeThanhToan = f.FTiLeThanhToanVNDChange;
                            else
                                f.FTiLeThanhToan = f.FTiLeThanhToanUsdChange;
                        }
                    });
                    ValidateInputThanhToanKyNay(objSender);
                    CaculateTotalThongTinThanhToan();
                    TinhTongSoDeNghiThanhToanKyNay();
                }
                //objSender.IsModified = true;
            }

            foreach (var item in _itemsNhTtThanhToanChiTiet)
            {
                item.PropertyChanged += ThanhToanChiTiet_PropertyChanged;
            }
            objSender.IsModified = true;
            OnPropertyChanged(nameof(ItemsNhTtThanhToanChiTiet));
        }

        public void ValidateInputThanhToanKyNay(NhTtThanhToanChiTietModel objSender)
        {
            if (_selectedLoaiNoiDungChi != null &&
                _selectedLoaiNoiDungChi.ValueItem.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()))
            {
                var soTienChuaThanhToan = objSender.FDuocDuyetUsd.GetValueOrDefault() -
                                          objSender.FDuocCapKyTruocUsd.GetValueOrDefault();
                if (objSender.FDeNghiCapKyNayUsd > soTienChuaThanhToan)
                {
                    if (!objSender.FlagMessage)
                    {
                        objSender.FlagMessage = true;
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
            else
            {
                var soTienChuaThanhToan = objSender.FDuocDuyetVnd.GetValueOrDefault() -
                                          objSender.FDuocCapKyTruocVnd.GetValueOrDefault();
                if (objSender.FDeNghiCapKyNayVnd.GetValueOrDefault() > soTienChuaThanhToan)
                {
                    if (!objSender.FlagMessage)
                    {
                        objSender.FlagMessage = true;
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
        }

        public void TinhTongSoDeNghiThanhToanKyNay()
        {
            if (_selectedLoaiNoiDungChi != null &&
                _selectedLoaiNoiDungChi.ValueItem.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()))
            {
                if (ItemsNhTtThanhToanChiTiet != null)
                {
                    Model.FTongDeNghiBangSo =
                        ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FDeNghiCapKyNayUsd.GetValueOrDefault());
                    Model.STongDeNghiBangChu = StringUtils.NumberToText(Model.FTongDeNghiBangSo.GetValueOrDefault(), false);
                }
            }
            else
            {
                if (ItemsNhTtThanhToanChiTiet != null)
                {
                    Model.FTongDeNghiBangSo =
                        ItemsNhTtThanhToanChiTiet.Where(x => !x.IsDeleted).Sum(x => x.FDeNghiCapKyNayVnd.GetValueOrDefault());
                    Model.STongDeNghiBangChu = StringUtils.NumberToText(Model.FTongDeNghiBangSo.GetValueOrDefault());
                }
            }
        }

        public void LoadThongTinLuyKe()
        {
            //công thúc fluyke = sum(fpheduyetthanhtoan) cùng đơn vị, nhiệm vụ chi, chủ đầu tư, (hợp đồng, dự án nếu có)
            //if (_selectedDonVi != null && _selectedNhiemVuChi != null && _selectedChuDauTu != null)
            //{
            //    var predicate = PredicateBuilder.True<NhTtThanhToan>();
            //    predicate = predicate.And(x => x.IIdDonVi == _selectedDonVi.Id);
            //    predicate = predicate.And(x => x.IIdNhiemVuChiId == _selectedNhiemVuChi.Id);
            //    predicate = predicate.And(x => x.IIdChuDauTuId == _selectedChuDauTu.Id);
            //    if (SelectedThanhToanTheo != null && Convert.ToInt16(SelectedThanhToanTheo.ValueItem) == (int)NHThanhToanTheo.Type.CHI_THEO_DU_AN_KHONG_HINH_THANH_HOP_DONG && _selectedDaDuAn != null)
            //    {
            //        predicate = predicate.And(x => x.IIdDuAnId == _selectedDaDuAn.Id);
            //        predicate = predicate.And(x => x.IThanhToanTheo == (int)NHThanhToanTheo.Type.CHI_THEO_DU_AN_KHONG_HINH_THANH_HOP_DONG);
            //    }
            //    if (SelectedThanhToanTheo != null && Convert.ToInt16(SelectedThanhToanTheo.ValueItem) == (int)NHThanhToanTheo.Type.CHI_THEO_HOP_DONG && _selectedDaHopDong != null)
            //    {
            //        predicate = predicate.And(x => x.IIdHopDongId == _selectedDaHopDong.Id);
            //        predicate = predicate.And(x => x.IIdDuAnId == _selectedDaHopDong.IIdDuAnId);
            //        predicate = predicate.And(x => x.IThanhToanTheo == (int)NHThanhToanTheo.Type.CHI_THEO_HOP_DONG);
            //    }
            //    if (SelectedThanhToanTheo != null && Convert.ToInt16(SelectedThanhToanTheo.ValueItem) == (int)NHThanhToanTheo.Type.CHI_KHONG_THEO_DU_AN_HOP_DONG)
            //    {
            //        predicate = predicate.And(x => x.IThanhToanTheo == (int)NHThanhToanTheo.Type.CHI_KHONG_THEO_DU_AN_HOP_DONG);
            //    }

            //    var lstthanhtoan = _iNhTtThanhToanService.FindByCondition(predicate).ToList();
            //    var querythanhtoan = from tt in lstthanhtoan
            //                         where (tt.ILoaiDeNghi == 1 && tt.ICoQuanThanhToan == 2) || (tt.ILoaiDeNghi == 2 && tt.ICoQuanThanhToan == 1) || (tt.ILoaiDeNghi == 3 && tt.ICoQuanThanhToan == 1) && tt.ITrangThai == 2
            //                         select tt;

            //    double? fLuyKeUSD = 0;
            //    double? fLuyKeVND = 0;
            //    double? fLuyKeEUR = 0;
            //    double? fLuyKeKhac = 0;

            //    foreach (var item in querythanhtoan)
            //    {
            //        var predicate_chitiet = PredicateBuilder.True<NhTtThanhToanChiTiet>();
            //        predicate_chitiet = predicate_chitiet.And(x => x.IIdDeNghiThanhToanId == item.Id);
            //        var lstchitiet = _iNhTtThanhToanChiTietService.FindByCondition(predicate_chitiet).ToList();
            //        fLuyKeUSD += lstchitiet.Sum(x => x.FPheDuyetCapKyNayUsd);
            //        fLuyKeVND += lstchitiet.Sum(x => x.FPheDuyetCapKyNayVnd);
            //        fLuyKeEUR += lstchitiet.Sum(x => x.FPheDuyetCapKyNayEur);
            //        fLuyKeKhac += lstchitiet.Sum(x => x.FPheDuyetCapKyNayNgoaiTeKhac);
            //    }

            if (_itemsNhTtThanhToanChiTiet != null)
            {
                foreach (var item in _itemsNhTtThanhToanChiTiet)
                {
                    item.FDuocCapKyTruocUsd = Model.FTongDuocCapKyTruocUSD;
                    item.FDuocCapKyTruocVnd = Model.FTongDuocCapKyTruocVND;
                    item.FDuocCapKyTruocEur = Model.FTongDuocCapKyTruocEUR;
                    item.FDuocCapKyTruocNgoaiTeKhac = Model.FTongDuocCapKyTruocNgoaiTeKhac;
                    item.FGiaTriUsdCol4 = item.FGiaTriUsdChangeCol4;
                    item.FGiaTriVndCol4 = item.FGiaTriVndChangeCol4;
                    item.FGiaTriEurCol4 = item.FGiaTriEurChangeCol4;
                    item.FGiatriNgoaiTeKhacCol4 = item.FGiaTriKinhPhiKhacChangeCol4;
                }
            }
            OnPropertyChanged(nameof(ItemsNhTtThanhToanChiTiet));
            
        }

        public void SetGiaTriHopDongDuToanQuyetDinhKhac()
        {
            if (!_itemsNhTtThanhToanChiTiet.IsEmpty())
            {
                foreach (var it in _itemsNhTtThanhToanChiTiet)
                {
                    //Check nếu thanh toán theo hợp đồng thì hiển thị giá trị theo hợp đồng, nếu chọn dự án thì hiển thị giá trị theo dự án
                    if (_selectedThanhToanTheo != null && Convert.ToInt16(_selectedThanhToanTheo.ValueItem) == (int)NHThanhToanTheo.Type.CHI_THEO_HOP_DONG)
                    {
                        it.FDuocDuyetUsd = _selectedDaHopDong != null ? _selectedDaHopDong.FGiaTriHopDongUSD : 0;
                        it.FDuocDuyetVnd = _selectedDaHopDong != null ? _selectedDaHopDong.FGiaTriHopDongVND : 0;
                        it.FDuocDuyetEur = _selectedDaHopDong != null ? _selectedDaHopDong.FGiaTriHopDongEUR : 0;
                        it.FDuocDuyetNgoaiTeKhac = _selectedDaHopDong != null ? _selectedDaHopDong.FGiaTriHopDongNgoaiTeKhac : 0;
                    }
                    else if (_selectedThanhToanTheo != null && Convert.ToInt16(_selectedThanhToanTheo.ValueItem) == (int)NHThanhToanTheo.Type.CHI_THEO_DU_AN_KHONG_HINH_THANH_HOP_DONG)
                    {
                        it.FDuocDuyetUsd = _selectedDaDuAn != null ? _selectedDaDuAn.FUsd : 0;
                        it.FDuocDuyetVnd = _selectedDaDuAn != null ? _selectedDaDuAn.FVnd : 0;
                        it.FDuocDuyetEur = _selectedDaDuAn != null ? _selectedDaDuAn.FEur : 0;
                        it.FDuocDuyetNgoaiTeKhac = _selectedDaDuAn != null ? _selectedDaDuAn.FNgoaiTeKhac : 0;
                    }
                    else if (_selectedThanhToanTheo != null && Convert.ToInt16(_selectedThanhToanTheo.ValueItem) == (int)NHThanhToanTheo.Type.CHI_KHONG_THEO_DU_AN_HOP_DONG)
                    {
                        it.FDuocDuyetUsd = _selectedChiPhiKhac != null ? _selectedChiPhiKhac.FGiaTriUsd : 0;
                        it.FDuocDuyetVnd = _selectedChiPhiKhac != null ? _selectedChiPhiKhac.FGiaTriVnd : 0;
                        it.FDuocDuyetEur = 0;
                        it.FDuocDuyetNgoaiTeKhac = 0;
                    }
                    else
                    {
                        it.FDuocDuyetUsd = 0;
                        it.FDuocDuyetVnd = 0;
                        it.FDuocDuyetEur = 0;
                        it.FDuocDuyetNgoaiTeKhac = 0;
                    }

                }
                Model.FTongDuocDuyetUSD = _itemsNhTtThanhToanChiTiet.FirstOrDefault().FDuocDuyetUsd;
                Model.FTongDuocDuyetVND = _itemsNhTtThanhToanChiTiet.FirstOrDefault().FDuocDuyetVnd;
                Model.FTongDuocDuyetEUR = _itemsNhTtThanhToanChiTiet.FirstOrDefault().FDuocDuyetEur;
                Model.FTongDuocDuyetNgoaiTeKhac = _itemsNhTtThanhToanChiTiet.FirstOrDefault().FDuocDuyetNgoaiTeKhac;
                OnPropertyChanged(nameof(Model));
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

        private void LoadDADuAn()
        {
            ItemsDaDuAn = new ObservableCollection<NhDaDuAnModel>();
            if (_selectedNhiemVuChi != null)
            {
                var predicate = PredicateBuilder.True<NhKhTongTheNhiemVuChi>();
                predicate = predicate.And(x => x.IIdKhTongTheId.ToString().Equals(_selectedKeHoachTongThe.ValueItem));
                predicate = predicate.And(x => x.IIdNhiemVuChiId.Equals(_selectedNhiemVuChi.Id));
                var id_KHTT_NVC = _iNhKhTongTheService.FindKHTongTheNVCByConditon(predicate).FirstOrDefault();
                var lstDaDuAn = _iNhDaDuAnService.FindAll().Where(x => x.IIdKhttNhiemVuChiId == id_KHTT_NVC.Id).ToList();
                if (SelectedDaHopDong != null)
                {
                    lstDaDuAn = lstDaDuAn.Where(x => x.Id.Equals(SelectedDaHopDong.IIdDuAnId ?? Guid.Empty)).ToList();
                    IsHiddenDuAnHopDong = true;
                }
                ItemsDaDuAn = new ObservableCollection<NhDaDuAnModel>(_mapper.Map<List<NhDaDuAnModel>>(lstDaDuAn));
            }
        }

        private void LoadThanhToanChiTiet()
        {
            _itemsNhTtThanhToanChiTiet = new ObservableCollection<NhTtThanhToanChiTietModel>();
            if (!Model.Id.IsNullOrEmpty())
            {
                var data = _iNhTtThanhToanChiTietService.FindByCondition(x => x.IIdDeNghiThanhToanId.Equals(Model.Id));
                _itemsNhTtThanhToanChiTiet = _mapper.Map<ObservableCollection<NhTtThanhToanChiTietModel>>(data);
                _itemsNhTtThanhToanChiTiet.ForAll(x => x.IsDeNghiThanhToan = true);
                _selectedNhTtThanhToanChiTiet = _itemsNhTtThanhToanChiTiet.FirstOrDefault();
                foreach (var item in _itemsNhTtThanhToanChiTiet)
                {
                    SetLnsChiTiet(item);
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
                    item.STenNoiDungChi = mlns.MoTa;
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
                    //foreach (var n in data)
                    //{
                    //    string sKey = n.Lns + "-" + n.L + "-" + n.K + "-" + n.M + "-" + n.Tm + "-" + n.Ttm;
                    //    if (!_dicMucLucNganSach.ContainsKey(sKey))
                    //        _dicMucLucNganSach.Add(sKey, n.Id);
                    //}
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

        private void ThanhToan_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var objSender = (NhTtThanhToanModel)sender;
            if (args.PropertyName.Equals(nameof(NhTtThanhToanModel.FTongDeNghiBangSo))
                || args.PropertyName.Equals(nameof(NhTtThanhToanModel.FThuHoiTamUngBangSo))
                || args.PropertyName.Equals(nameof(NhTtThanhToanModel.FChuyenKhoanBangSo))
                || args.PropertyName.Equals(nameof(NhTtThanhToanModel.INamKeHoach))
                || args.PropertyName.Equals(nameof(NhTtThanhToanModel.ILoaiDeNghi)))
            {
                objSender.FTraDonViThuHuongBangSo = objSender.FTongDeNghiBangSo.GetValueOrDefault() -
                                                    objSender.FThuHoiTamUngBangSo.GetValueOrDefault();
                objSender.FTienMatBangSo = objSender.FTraDonViThuHuongBangSo.GetValueOrDefault() -
                                           objSender.FChuyenKhoanBangSo.GetValueOrDefault();
                if (_selectedLoaiNoiDungChi == null ||
                    _selectedLoaiNoiDungChi.ValueItem.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE).ToString()))
                {
                    objSender.FThuHoiTamUngBangChu = StringUtils.NumberToText(objSender.FThuHoiTamUngBangSo.GetValueOrDefault());
                    objSender.FTraDonViThuHuongBangChu = StringUtils.NumberToText(objSender.FTraDonViThuHuongBangSo.GetValueOrDefault());
                    objSender.SChuyenKhoanBangChu = StringUtils.NumberToText(objSender.FChuyenKhoanBangSo.GetValueOrDefault());
                    objSender.STienMatBangChu = StringUtils.NumberToText(objSender.FTienMatBangSo.GetValueOrDefault());

                }
                else if (_selectedLoaiNoiDungChi == null ||
                    _selectedLoaiNoiDungChi.ValueItem.Equals(((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()))
                {
                    objSender.FThuHoiTamUngBangChu = StringUtils.NumberToText(objSender.FThuHoiTamUngBangSo.GetValueOrDefault(), false);
                    objSender.FTraDonViThuHuongBangChu = StringUtils.NumberToText(objSender.FTraDonViThuHuongBangSo.GetValueOrDefault(), false);
                    objSender.SChuyenKhoanBangChu = StringUtils.NumberToText(objSender.FChuyenKhoanBangSo.GetValueOrDefault(), false);
                    objSender.STienMatBangChu = StringUtils.NumberToText(objSender.FTienMatBangSo.GetValueOrDefault(), false);
                }

                if (objSender.FTraDonViThuHuongBangSo.GetValueOrDefault() < 0 && objSender.FThuHoiTamUngBangSo.GetValueOrDefault() != 0)
                {
                    MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(Resources.MsgConfirmSoTraDonViThuHuongAm);
                    if (messageBoxResult == MessageBoxResult.No)
                    {
                        objSender.FThuHoiTamUngBangSo = 0;
                    }
                }

                if (objSender.INamKeHoach != null)
                {
                    CaculateThanhToan(objSender);
                }

            }
        }

        private void CaculateThanhToan(NhTtThanhToanModel objSender)
        {
            LoadDataNhTongHop((int)objSender.INamKeHoach);
            if (ItemsTongHops.Any())
            {
                var iNoiDungChi = SelectedLoaiNoiDungChi != null ? int.Parse(SelectedLoaiNoiDungChi.ValueItem) : (int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE;

                var lstValueLoaiDeNghiKP = new List<string> { ((int)NhLoaiDeNghi.Type.CAP_KINH_PHI).ToString(), ((int)NhLoaiDeNghi.Type.TAM_UNG_KINH_PHI).ToString() };
                List<string> lstMaNguon = new List<string>();

                // case 1: tính data theo TT_SDTUKP nếu loại đề nghị là kinh phí (1,2) 
                if (SelectedLoaiDeNghi != null && lstValueLoaiDeNghiKP.Contains(SelectedLoaiDeNghi.ValueItem))
                {
                    var dataNguonTT_SDTUKP = ItemsTongHops.Where(x => (x.SMaNguon == NhTongHopConstants.MA_309 || x.SMaNguonCha == NhTongHopConstants.MA_309) && x.INamKeHoach == Model.INamKeHoach - 1);
                    lstMaNguon = new List<string> { NhTongHopConstants.MA_141, NhTongHopConstants.MA_142 };
                    var dataTUKP_NTCS_NN = ItemsTongHops.Where(x => (lstMaNguon.Contains(x.SMaNguon) || lstMaNguon.Contains(x.SMaNguonCha)) && x.INamKeHoach == Model.INamKeHoach);
                    if (iNoiDungChi == (int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE)
                    {
                        var fNguonTT_SDTUKP = dataNguonTT_SDTUKP.Where(x => x.SMaNguon == NhTongHopConstants.MA_309).Sum(x => x.FGiaTriVnd) - dataNguonTT_SDTUKP.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_309).Sum(x => x.FGiaTriVnd);
                        var fNguonTUKP_NTCS_NN = dataNguonTT_SDTUKP.Where(x => lstMaNguon.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataNguonTT_SDTUKP.Where(x => lstMaNguon.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                        objSender.FSoDuTamUng = (fNguonTT_SDTUKP ?? 0) + (fNguonTUKP_NTCS_NN ?? 0);
                    }
                    else
                    {
                        var fNguonTT_SDTUKP = dataNguonTT_SDTUKP.Where(x => x.SMaNguon == NhTongHopConstants.MA_309).Sum(x => x.FGiaTriUsd) - dataNguonTT_SDTUKP.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_309).Sum(x => x.FGiaTriUsd);
                        var fNguonTUKP_NTCS_NN = dataNguonTT_SDTUKP.Where(x => lstMaNguon.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataNguonTT_SDTUKP.Where(x => lstMaNguon.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                        objSender.FSoDuTamUng = (fNguonTT_SDTUKP ?? 0) + (fNguonTUKP_NTCS_NN ?? 0);
                    }

                }
                else if (SelectedLoaiDeNghi != null)
                {
                    var dataNguonTT_SDTUCD = ItemsTongHops.Where(x => (x.SMaNguon == NhTongHopConstants.MA_310 || x.SMaNguonCha == NhTongHopConstants.MA_310) && x.INamKeHoach == Model.INamKeHoach - 1);
                    lstMaNguon = new List<string> { NhTongHopConstants.MA_121, NhTongHopConstants.MA_122 };
                    var dataTUCD_NTCS = ItemsTongHops.Where(x => (lstMaNguon.Contains(x.SMaNguon) || lstMaNguon.Contains(x.SMaNguonCha)) && x.INamKeHoach == Model.INamKeHoach);
                    lstMaNguon = new List<string> { NhTongHopConstants.MA_131, NhTongHopConstants.MA_132 };
                    var dataTUCD_NN = ItemsTongHops.Where(x => (lstMaNguon.Contains(x.SMaNguon) || lstMaNguon.Contains(x.SMaNguonCha)) && x.INamKeHoach == Model.INamKeHoach);

                    if (iNoiDungChi == (int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE)
                    {
                        var fNguonTT_SDTUCD = dataNguonTT_SDTUCD.Where(x => x.SMaNguon == NhTongHopConstants.MA_310).Sum(x => x.FGiaTriVnd) - dataNguonTT_SDTUCD.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_310).Sum(x => x.FGiaTriVnd);
                        var fNguonTUCD_NTCS = dataTUCD_NTCS.Where(x => lstMaNguon.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataTUCD_NTCS.Where(x => lstMaNguon.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                        var fNguonTUCD_NN = dataTUCD_NN.Where(x => lstMaNguon.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataTUCD_NN.Where(x => lstMaNguon.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                        objSender.FSoDuTamUng = (fNguonTT_SDTUCD ?? 0) + (fNguonTUCD_NTCS ?? 0) - (fNguonTUCD_NN ?? 0);
                    }
                    else
                    {
                        var fNguonTT_SDTUCD = dataNguonTT_SDTUCD.Where(x => x.SMaNguon == NhTongHopConstants.MA_310)
                                            .Sum(x => x.FGiaTriUsd) - dataNguonTT_SDTUCD.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_310).Sum(x => x.FGiaTriUsd);
                        var fNguonTUCD_NTCS = dataTUCD_NTCS.Where(x => lstMaNguon.Contains(x.SMaNguon))
                                            .Sum(x => x.FGiaTriUsd) - dataTUCD_NTCS.Where(x => lstMaNguon.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                        var fNguonTUCD_NN = dataTUCD_NN.Where(x => lstMaNguon.Contains(x.SMaNguon))
                                            .Sum(x => x.FGiaTriUsd) - dataTUCD_NN.Where(x => lstMaNguon.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                        objSender.FSoDuTamUng = (fNguonTT_SDTUCD ?? 0) + (fNguonTUCD_NTCS ?? 0) - (fNguonTUCD_NN ?? 0);
                    }
                }

            }
        }

        public void LoadDAHDTheoThanhToan()
        {

            var defaultStr = "Giá trị hợp đồng hoặc dự toán được duyệt";
            if (_selectedThanhToanTheo != null)
            {
                if (Convert.ToInt16(_selectedThanhToanTheo.ValueItem) == (int)NHThanhToanTheo.Type.CHI_THEO_HOP_DONG)
                {
                    IsHiddenDuAn = false;
                    IsHiddenHopDong = true;
                    IsHiddenQuyetDinhCPK = false;
                    IsHiddenChiPhiQdk = false;
                    IsHiddenDuAnHopDong = false;
                    IsHiddenDuAnChiPhi = false;
                    SelectedChiPhiDuAn = null;
                    SelectedChiPhiQDKhac = null;
                }
                else if (Convert.ToInt16(_selectedThanhToanTheo.ValueItem) == (int)NHThanhToanTheo.Type.CHI_THEO_DU_AN_KHONG_HINH_THANH_HOP_DONG)
                {
                    IsHiddenDuAn = true;
                    IsHiddenHopDong = false;
                    IsHiddenQuyetDinhCPK = false;
                    IsHiddenChiPhiQdk = false;
                    IsHiddenDuAnHopDong = false;
                    IsHiddenDuAnChiPhi = false;
                    SelectedChiPhiHopDong = null;
                    SelectedChiPhiQDKhac = null;
                }
                else if (Convert.ToInt16(_selectedThanhToanTheo.ValueItem) == (int)NHThanhToanTheo.Type.CHI_KHONG_THEO_DU_AN_HOP_DONG)
                {
                    IsHiddenDuAn = false;
                    IsHiddenHopDong = false;
                    IsHiddenQuyetDinhCPK = true;
                    IsHiddenChiPhiQdk = false;
                    IsHiddenDuAnHopDong = false;
                    IsHiddenDuAnChiPhi = false;
                    SelectedChiPhiDuAn = null;
                    SelectedChiPhiHopDong = null;
                    defaultStr = "Giá trị hợp đồng hoặc dự toán được duyệt hoặc chi phí";
                }
                else
                {
                    IsHiddenDuAn = false;
                    IsHiddenHopDong = true;
                    IsHiddenQuyetDinhCPK = false;
                    IsHiddenChiPhiQdk = false;
                    IsHiddenDuAnHopDong = false;
                    IsHiddenDuAnChiPhi = false;
                    SelectedChiPhiDuAn = null;
                    SelectedChiPhiQDKhac = null;
                }
            }
            STiTleDuocDuyet = defaultStr;
            OnPropertyChanged(nameof(IsHiddenDuAn));
            OnPropertyChanged(nameof(IsHiddenHopDong));
            OnPropertyChanged(nameof(IsHiddenQuyetDinhCPK));
            OnPropertyChanged(nameof(IsHiddenDuAnHopDong));
            OnPropertyChanged(nameof(IsHiddenDuAnChiPhi));
            OnPropertyChanged(nameof(STiTleDuocDuyet));
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
            //if (!_itemsQuyKeHoach.IsEmpty()) _itemsQuyKeHoach.Clear();
            //if (!_itemsNguonVon.IsEmpty()) _itemsNguonVon.Clear();
            //if (!_itemsDmNhaThau.IsEmpty()) _itemsDmNhaThau.Clear();
            //if (!_itemsLoaiNoiDungChi.IsEmpty()) _itemsLoaiNoiDungChi.Clear();
            //if (!_itemsCoQuanThanhToan.IsEmpty()) _itemsCoQuanThanhToan.Clear();
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
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
                    ItemsNhTtThanhToanChiTiet.ForAll(x => { x.FTiLeThanhToan = x.FTiLeThanhToanVNDChange ?? 0; });
                }
                else
                {
                    ItemsNhTtThanhToanChiTiet.ForAll(x => { x.FTiLeThanhToan = x.FTiLeThanhToanUsdChange ?? 0; });
                }
            }
            OnPropertyChanged(nameof(ItemsNhTtThanhToanChiTiet));
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
                        if (SelectedDaDuAn != null)
                        {
                            predicate = predicate.And(x => x.IIdDuAnId == SelectedDaDuAn.Id);
                        }
                        break;
                    case "3":
                        if (SelectedNhiemVuChi != null)
                        {
                            predicate = predicate.And(x => x.IIDKHTTNhiemVuChiID == SelectedNhiemVuChi.Id);
                        }
                        break;
                }
            }
            ItemsTongHops = _nhThTongHopService.FindByCondition(predicate);
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
                lstMaNguonSum = new List<string> { NhTongHopConstants.MA_101, NhTongHopConstants.MA_102,                                                                                                            NhTongHopConstants.MA_111, NhTongHopConstants.MA_112,
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

                if (!ItemsNhTtThanhToanChiTiet.IsEmpty())
                {
                    ItemsNhTtThanhToanChiTiet.ForAll(x =>
                    {
                        x.FDuocCapKyTruocUsd = Model.FTongDuocCapKyTruocUSD;
                        x.FDuocCapKyTruocVnd = Model.FTongDuocCapKyTruocVND;
                        x.FDuocCapKyTruocEur = Model.FTongDuocCapKyTruocEUR;
                        x.FDuocCapKyTruocNgoaiTeKhac = Model.FTongDuocCapKyTruocNgoaiTeKhac;
                    });
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
    }
}