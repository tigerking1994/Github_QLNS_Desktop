using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNGoiThauTrongNuoc;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNGoiThauTrongNuoc;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNGoiThauTrongNuoc
{
    public class MSCTNGoiThauTrongNuocDialogViewModel : DialogAttachmentViewModelBase<NhDaGoiThauModel>
    {
        private readonly IMapper _mapper;
        private readonly INhDaKhlcNhaThauService _service;
        private readonly INsDonViService _dvService;
        private readonly INhDaGoiThauService _goithauService;
        private readonly INhDaGoiThauHangMucSerrvice _goithauHangMucService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        private readonly ISessionService _sessionService;
        private readonly INhDaGoiThauChiPhiService _goithauChiPhiService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDaQdDauTuNguonVonService _qddtNguonVonService;
        private readonly ILog _logger;
        private readonly INhDaDuToanService _dutoanService;
        private readonly INhDaQdDauTuService _qddautuService;
        private readonly INhDmPhuongThucChonNhaThauService _nhDmPhuongThucChonNhaThauService;
        private readonly INhDmHinhThucChonNhaThauService _nhDmHinhThucChonNhaThauService;
        private readonly INhDmLoaiHopDongService _nhDmLoaiHopDongService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDaQdDauTuHangMucService _qddtHangMucService;
        private readonly INhDaGoiThauNguonVonService _goithauNguonVonService;
        private readonly INhDaGoiThauService _nhDaGoiThauService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private List<NhDaGoiThauDetailNguonVonModel> _itemsNguonVon;
        private List<NhDaGoiThauDetailChiPhiModel> _itemsChiPhi;
        private List<NhDaGoiThauDetailHangMucModel> _itemsHangMuc;
        private readonly INhDaQdDauTuChiPhiService _qddtChiPhiService;
        private List<NhDaGoiThauDetailNguonVonModel> _itemsNguonVonByGoiThau;
        private List<NhDaGoiThauDetailChiPhiModel> _itemsChiPhiByGoiThau;
        private List<NhDaGoiThauDetailHangMucModel> _itemsHangMucByGoiThau;

        public override Type ContentType => typeof(MSCTNGoiThauTrongNuocDialog);

        public MSCTNGoiThauTrongNuocDetailViewModel ForexDomesticBiddingPackageDetailViewModel { get; }
        public MSCTNGoiThauTrongNuocChiPhiViewModel NHGoiThauTrongNuocChiPhiViewModel { get; }

        public bool BIsDuToan => SelectedSoCuTrucTiep == null || SelectedSoCuTrucTiep.ValueItem == ((int)SO_CU_TRUC_TIEP.TypeValue.DU_TOAN).ToString();

        public int IThuocMenu { get; set; }
        public int ILoai { get; set; }
        public bool IsDetail { get; set; }
        public RelayCommand AddGoiThauCommand { get; }
        public RelayCommand ShowGoiThauCommand { get; }
        public RelayCommand DeleteGoiThauCommand { get; }
        public bool IsShowDuAn { get; set; }
        public RelayCommand ShowGoiThauDetailCommand { get; }

        private ObservableCollection<NhDmLoaiHopDongModel> _itemsLoaiHopDong;
        public ObservableCollection<NhDmLoaiHopDongModel> ItemsLoaiHopDong
        {
            get => _itemsLoaiHopDong;
            set => SetProperty(ref _itemsLoaiHopDong, value);
        }
        private ObservableCollection<NhDmNhiemVuChiModel> _itemsChuongTrinh;
        public ObservableCollection<NhDmNhiemVuChiModel> ItemsChuongTrinh
        {
            get => _itemsChuongTrinh;
            set => SetProperty(ref _itemsChuongTrinh, value);
        }
        private ObservableCollection<NhDaGoiThauModel> _itemsGoiThau;
        public ObservableCollection<NhDaGoiThauModel> ItemsGoiThau
        {
            get => _itemsGoiThau;
            set => SetProperty(ref _itemsGoiThau, value);
        }
        private NhDaGoiThauModel _selectedGoiThau;
        public NhDaGoiThauModel SelectedGoiThau
        {
            get => _selectedGoiThau;
            set => SetProperty(ref _selectedGoiThau, value);
        }
        private NhDmLoaiHopDongModel _selectedLoaiHopDong;
        public NhDmLoaiHopDongModel SelectedLoaiHopDong
        {
            get => _selectedLoaiHopDong;
            set => SetProperty(ref _selectedLoaiHopDong, value);
        }
        private string _sTitlePheDuyetUsd;
        public string STitlePheDuyetUsd
        {
            get => _sTitlePheDuyetUsd;
            set => SetProperty(ref _sTitlePheDuyetUsd, value);
        }

        private string _sTitlePheDuyetVnd;
        public string STitlePheDuyetVnd
        {
            get => _sTitlePheDuyetVnd;
            set => SetProperty(ref _sTitlePheDuyetVnd, value);
        }

        private string _sTitlePheDuyetEur;
        public string STitlePheDuyetEur
        {
            get => _sTitlePheDuyetEur;
            set => SetProperty(ref _sTitlePheDuyetEur, value);
        }

        private string _sTitlePheDuyetKhac;
        public string STitlePheDuyetKhac
        {
            get => _sTitlePheDuyetKhac;
            set => SetProperty(ref _sTitlePheDuyetKhac, value);
        }
        private double? _fTiGiaNhap;
        public double? FTiGiaNhap
        {
            get => _fTiGiaNhap;
            set
            {
                SetProperty(ref _fTiGiaNhap, value);
                if (FTiGiaNhap != null)
                {
                    ChangeData();
                }
            }

        }

        private bool _isVisibleTiGiaNhap;
        public bool IsVisibleTiGiaNhap
        {
            get => _isVisibleTiGiaNhap;
            set => SetProperty(ref _isVisibleTiGiaNhap, value);
        }

        private ComboboxItem _selectedSoCuTrucTiep;
        public ComboboxItem SelectedSoCuTrucTiep
        {
            get => _selectedSoCuTrucTiep;
            set
            {
                if (SetProperty(ref _selectedSoCuTrucTiep, value))
                {
                    if (SelectedSoCuTrucTiep == null || SelectedSoCuTrucTiep.ValueItem == ((int)SO_CU_TRUC_TIEP.TypeValue.DU_TOAN).ToString())
                    {
                        STitlePheDuyetUsd = "Giá trị dự toán phê duyệt (USD)";
                        STitlePheDuyetVnd = "Giá trị dự toán phê duyệt (VND)";
                        STitlePheDuyetEur = "Giá trị dự toán phê duyệt (EUR)";
                        STitlePheDuyetKhac = "Giá trị dự toán phê duyệt (Ngoại tệ khác)";
                        LoadDuToan();
                    }
                    else
                    {
                        STitlePheDuyetUsd = "Giá trị quyết định đầu tư phê duyệt (USD)";
                        STitlePheDuyetVnd = "Giá trị quyết định đầu tư phê duyệt (VND)";
                        STitlePheDuyetEur = "Giá trị quyết định đầu tư phê duyệt (EUR)";
                        STitlePheDuyetKhac = "Giá trị quyết định đầu tư phê duyệt (Ngoại tệ khác)";
                        FindQDDauTuByDuAn();
                    }
                }
                OnPropertyChanged(nameof(BIsDuToan));
            }
        }
        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ObservableCollection<NhDmPhuongThucChonNhaThauModel> _itemsPhuongThucChonNhaThau;
        public ObservableCollection<NhDmPhuongThucChonNhaThauModel> ItemsPhuongThucChonNhaThau
        {
            get => _itemsPhuongThucChonNhaThau;
            set => SetProperty(ref _itemsPhuongThucChonNhaThau, value);
        }

        private NhDmPhuongThucChonNhaThauModel _selectedPhuongThucChonNhaThau;
        public NhDmPhuongThucChonNhaThauModel SelectedPhuongThucChonNhaThau
        {
            get => _selectedPhuongThucChonNhaThau;
            set => SetProperty(ref _selectedPhuongThucChonNhaThau, value);
        }

        private ObservableCollection<NhDmHinhThucChonNhaThauModel> _itemsHinhThucChonNhaThau;
        public ObservableCollection<NhDmHinhThucChonNhaThauModel> ItemsHinhThucChonNhaThau
        {
            get => _itemsHinhThucChonNhaThau;
            set => SetProperty(ref _itemsHinhThucChonNhaThau, value);
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

        private ObservableCollection<ComboboxItem> _itemsLoaiKHLCNT;
        public ObservableCollection<ComboboxItem> ItemsLoaiKHLCNT
        {
            get => _itemsLoaiKHLCNT;
            set => SetProperty(ref _itemsLoaiKHLCNT, value);
        }

        private ComboboxItem _selectedLoaiKHLCNT;
        public ComboboxItem SelectedLoaiKHLCNT
        {
            get => _selectedLoaiKHLCNT;
            set => SetProperty(ref _selectedLoaiKHLCNT, value);
        }

        private ObservableCollection<NhDmTiGiaChiTietModel> _itemsTiGiaChiTiet;
        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTiet
        {
            get => _itemsTiGiaChiTiet;
            set => SetProperty(ref _itemsTiGiaChiTiet, value);
        }

        private ObservableCollection<NhDmTiGiaChiTietModel> _itemsTiGiaChiTietChinh;
        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTietChinh
        {
            get => _itemsTiGiaChiTietChinh;
            set => SetProperty(ref _itemsTiGiaChiTietChinh, value);
        }

        private NhDmTiGiaChiTietModel _selectedTiGiaChiTiet;
        public NhDmTiGiaChiTietModel SelectedTiGiaChiTiet
        {
            get => _selectedTiGiaChiTiet;
            set
            {
                SetProperty(ref _selectedTiGiaChiTiet, value);
                if (value != null)
                {
                    SetLabelTiGiaChiTiet();
                }
            }
        }

        private string _sSoQDDauTu;
        public string SSoQDDauTu
        {
            get => _sSoQDDauTu;
            set => SetProperty(ref _sSoQDDauTu, value);
        }

        private Guid? _iIdQDDauTuId;
        public Guid? IIdQDDauTuId
        {
            get => _iIdQDDauTuId;
            set => SetProperty(ref _iIdQDDauTuId, value);
        }

        private string _sTenChuDauTu;
        public string STenChuDauTu
        {
            get => _sTenChuDauTu;
            set => SetProperty(ref _sTenChuDauTu, value);
        }

        private string _sDiaDiemThucHien;
        public string SDiaDiemThucHien
        {
            get => _sDiaDiemThucHien;
            set => SetProperty(ref _sDiaDiemThucHien, value);
        }

        private double _fGiaTriVND;
        public double FGiaTriVND
        {
            get => _fGiaTriVND;
            set => SetProperty(ref _fGiaTriVND, value);
        }

        private double _fGiaTriUSD;
        public double FGiaTriUSD
        {
            get => _fGiaTriUSD;
            set => SetProperty(ref _fGiaTriUSD, value);
        }

        private double _fGiaTriEUR;
        public double FGiaTriEUR
        {
            get => _fGiaTriEUR;
            set => SetProperty(ref _fGiaTriEUR, value);
        }

        private double _fGiaTriNgoaiTeKhac;
        public double FGiaTriNgoaiTeKhac
        {
            get => _fGiaTriNgoaiTeKhac;
            set => SetProperty(ref _fGiaTriNgoaiTeKhac, value);
        }

        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private string _sTiGia;
        public string STiGia
        {
            get => _sTiGia;
            set => SetProperty(ref _sTiGia, value);
        }

        private NhDmHinhThucChonNhaThauModel _selectedHinhThucChonNhaThau;
        public NhDmHinhThucChonNhaThauModel SelectedHinhThucChonNhaThau
        {
            get => _selectedHinhThucChonNhaThau;
            set => SetProperty(ref _selectedHinhThucChonNhaThau, value);
        }
        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
                if (value != null)
                {
                    LoadDuAn();
                    //   LoadChuongTrinh();
                   // if (!IsShowDuAn) LoadDuToan();

                }
            }
        }
        private ObservableCollection<NhDaDuAn> _itemsDuAn;
        public ObservableCollection<NhDaDuAn> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }
        private ObservableCollection<NhDaThongTinNhaThauHopDongModel> _itemsThongTinNhaThauHopDong;
        public ObservableCollection<NhDaThongTinNhaThauHopDongModel> ItemsThongTinNhaThauHopDong
        {
            get => _itemsThongTinNhaThauHopDong;
            set => SetProperty(ref _itemsThongTinNhaThauHopDong, value);
        }

        private NhDaThongTinNhaThauHopDongModel _selectedThongTinNhaThauHopDong;
        public NhDaThongTinNhaThauHopDongModel SelectedThongTinNhaThauHopDong
        {
            get => _selectedThongTinNhaThauHopDong;
            set => SetProperty(ref _selectedThongTinNhaThauHopDong, value);
        }
        private ObservableCollection<NhDaDuToan> _itemsDuToan;
        public ObservableCollection<NhDaDuToan> ItemsDuToan
        {
            get => _itemsDuToan;
            set => SetProperty(ref _itemsDuToan, value);
        }

        private NhDaDuToan _selectedDuToan;
        public NhDaDuToan SelectedDuToan
        {
            get => _selectedDuToan;
            set
            {
                if (SetProperty(ref _selectedDuToan, value))
                {
                    LoadChuongTrinh();
                    SetValueBySelectDuToan();
                }
            }
        }
        private NhDaDuAn _selectedDuAn;
        public NhDaDuAn SelectedDuAn
        {
            get => _selectedDuAn;
            set
            {
                if (SetProperty(ref _selectedDuAn, value))
                {
                    if (SelectedSoCuTrucTiep == null || SelectedSoCuTrucTiep.ValueItem == ((int)SO_CU_TRUC_TIEP.TypeValue.DU_TOAN).ToString())
                        LoadDuToan();
                    else
                        FindQDDauTuByDuAn();
                }
            }
        }
        private NhDmNhiemVuChiModel _selectedChuongTrinh;
        public NhDmNhiemVuChiModel SelectedChuongTrinh
        {
            get => _selectedChuongTrinh;
            set => SetProperty(ref _selectedChuongTrinh, value);
        }

        private Visibility _detail;
        public Visibility Detail
        {
            get => _detail;
            set => SetProperty(ref _detail, value);
        }

        private Visibility _save;
        public Visibility Save
        {
            get => _save;
            set => SetProperty(ref _save, value);
        }
        public MSCTNGoiThauTrongNuocDialogViewModel(
             MSCTNGoiThauTrongNuocDetailViewModel nHKeHoachLuaChonNhaThauDetailViewModel,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            IMapper mapper,
            ILog logger,
            ISessionService sessionService,
            INhDaKhlcNhaThauService service,
            INhDaGoiThauService goithauService,
            INhDaDuToanService dutoanService,
            INsDonViService dvService,
            INhDaQdDauTuHangMucService qddtHangMucService,
            INsDonViService nsDonViService,
            INhDaGoiThauChiPhiService goithauChiPhiService,
            INhDaQdDauTuService qddautuService,
            INhDmPhuongThucChonNhaThauService nhDmPhuongThucChonNhaThauService,
            INhDmHinhThucChonNhaThauService nhDmHinhThucChonNhaThauService,
            INhDmLoaiHopDongService nhDmLoaiHopDongService,
            INhDaQdDauTuNguonVonService qddtNguonVonService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            INhDaGoiThauHangMucSerrvice goithauHangMucService,
            INhDaGoiThauNguonVonService goithauNguonVonService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDaQdDauTuChiPhiService qddtChiPhiService,
             INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            INhDaGoiThauService nhDaGoiThauService,
            MSCTNGoiThauTrongNuocDetailViewModel forexDomesticBiddingPackageDetailViewModel,
            MSCTNGoiThauTrongNuocChiPhiViewModel nHGoiThauTrongNuocChiPhiViewModel) : base(mapper, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _goithauChiPhiService = goithauChiPhiService;
            _sessionService = sessionService;
            _qddtNguonVonService = qddtNguonVonService;
            _goithauHangMucService = goithauHangMucService;
            _qddtHangMucService = qddtHangMucService;
            _goithauService = goithauService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _goithauNguonVonService = goithauNguonVonService;
            _logger = logger;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
            _qddtChiPhiService = qddtChiPhiService;
            _dutoanService = dutoanService;
            _qddautuService = qddautuService;
            _nsDonViService = nsDonViService;
            _nhDaGoiThauService = nhDaGoiThauService;
            _nhDmPhuongThucChonNhaThauService = nhDmPhuongThucChonNhaThauService;
            _nhDmHinhThucChonNhaThauService = nhDmHinhThucChonNhaThauService;
            _nhDmLoaiHopDongService = nhDmLoaiHopDongService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _service = service;
            _dvService = dvService;
            ForexDomesticBiddingPackageDetailViewModel = forexDomesticBiddingPackageDetailViewModel;
            NHGoiThauTrongNuocChiPhiViewModel = nHGoiThauTrongNuocChiPhiViewModel;
            AddGoiThauCommand = new RelayCommand(obj => OnAddGoiThau());
            ShowGoiThauCommand = new RelayCommand(obj => OnShowGoiThau());
            ShowGoiThauDetailCommand = new RelayCommand(obj => OpenDetailGoiThau());
            DeleteGoiThauCommand = new RelayCommand(obj => OnDeleteGoiThau());
        }

        public override void Init()
        {
            base.Init();
            LoadLoaiHopDong();
            LoadHinhThucChonNhaThau();
            LoadPhuongThucChonNhaThau();
            LoadChuongTrinh();
            LoadDuAn();
            LoadThongTinNhaThauHopDong();
            //  LoadDuToan();
            LoadDonVi();
            LoadTiGia();
            GetGridGoiThau();
            GetKhlcNhaThauDetailById();
            LoadData();
            LoadDuToan();

        }

        public override void LoadData(params object[] args)
        {
            if (IsDetail)
            {
                Title = "THÔNG TIN GÓI THẦU";
                Description = "Thông tin gói thầu";
                _detail = Visibility.Visible;
                _save = Visibility.Collapsed;
                FTiGiaNhap = null;
                IsVisibleTiGiaNhap = false;

            }
            else
            {
                Title = "CẬP NHẬP THÔNG TIN GÓI THẦU";
                Description = "Cập nhập thông tin gói thầu";
                _save = Visibility.Visible;
                _detail = Visibility.Collapsed;
            }
            if (Model.Id == Guid.Empty)
            {
                // SelectedSoCuTrucTiep = ItemsSoCuTrucTiep.FirstOrDefault();
                SelectedDonVi = null;
                SelectedDuAn = null;
                SelectedDuToan = null;
                SelectedChuongTrinh = null;
                SSoQuyetDinh = string.Empty;
                DNgayQuyetDinh = DateTime.Now;
                SMoTa = string.Empty;
                FGiaTriEUR = 0;
                FGiaTriUSD = 0;
                FGiaTriVND = 0;
                FGiaTriNgoaiTeKhac = 0;
                IsVisibleTiGiaNhap = false;
                FTiGiaNhap = 0;

            }
            else
            {
                //if (Model.IIdQDDauTuID.HasValue)
                //    SelectedSoCuTrucTiep = ItemsSoCuTrucTiep.FirstOrDefault(n => n.ValueItem == ((int)SO_CU_TRUC_TIEP.TypeValue.QD_DAU_TU).ToString());
                //else if (Model.IIdDuToanID.HasValue)
                //    SelectedSoCuTrucTiep = ItemsSoCuTrucTiep.FirstOrDefault(n => n.ValueItem == ((int)SO_CU_TRUC_TIEP.TypeValue.DU_TOAN).ToString());
                //if (Model.ILoaiKHLCNT != null)
                //{
                //    SelectedLoaiKHLCNT = ItemsLoaiKHLCNT.FirstOrDefault(n => n.ValueItem == Model.ILoaiKHLCNT.ToString());
                //}
                // SelectedDonVi = ItemsDonVi.FirstOrDefault(n => n.IIDMaDonVi == Model.IIdMaDonViQuanLy);
                //if (SelectedDonVi != null)
                //{
                //    if (!IsShowDuAn) _selectedChuongTrinh = ItemsChuongTrinh.FirstOrDefault(x => x.IIdKHTTNhiemVuChiId == Model.IIdKHTTNhiemVuChiId);
                //}
                LoadDuAn();
                SelectedDuAn = ItemsDuAn.FirstOrDefault(n => n.Id == Model.IIdDuAnID);
                LoadDuToan();
                SelectedDuToan = ItemsDuToan.FirstOrDefault(n => n.Id == Model.IIdDuToanID);
                LoadTiGia();
                if (SelectedTiGia != null)
                {
                    IsVisibleTiGiaNhap = true;
                }
                FTiGiaNhap = Model.FTiGiaNhap;

                //SSoQuyetDinh = Model.SSoQuyetDinh;
                //DNgayQuyetDinh = Model.DNgayQuyetDinh;
                //SMoTa = Model.SMoTa;
            }
            GetGridGoiThau();
            GetKhlcNhaThauDetailById();
            foreach (var item in ItemsGoiThau)
            {
                GetHangMucByGoiThau(item.Id);
                GetChiPhiByGoiThau(item.Id);
            }
        }
        private void GetGridGoiThau()
        {
            if (Model.Id == Guid.Empty)
                ItemsGoiThau = new ObservableCollection<NhDaGoiThauModel>();
            else
                ItemsGoiThau = _mapper.Map<ObservableCollection<NhDaGoiThauModel>>(_goithauService.FindByIidKhlcNhaThauID(Model.Id));
            OnPropertyChanged(nameof(ItemsGoiThau));
        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindByCondition(x => x.NamLamViec == _sessionService.Current.YearOfWork).OrderBy(x => x.IIDMaDonVi);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            if (_itemsDonVi != null)
            {
                SelectedDonVi = _itemsDonVi.FirstOrDefault(x => x.Id == Model.IID_DonViQuanLyID);
            }
            OnPropertyChanged(nameof(ItemsDonVi));
        }
        private void LoadLoaiHopDong()
        {
            var data = _nhDmLoaiHopDongService.FindAll();
            _itemsLoaiHopDong = _mapper.Map<ObservableCollection<NhDmLoaiHopDongModel>>(data);
            if (_itemsLoaiHopDong != null)
            {
                SelectedLoaiHopDong = _itemsLoaiHopDong.FirstOrDefault(x => x.IIdLoaiHopDongId == Model.IIdLoaiHopDongId);
            }
            OnPropertyChanged(nameof(ItemsLoaiHopDong));
        }
        private void LoadDuAn()
        {
            if (SelectedDonVi == null)
            {
                ItemsDuAn = new ObservableCollection<NhDaDuAn>();
                OnPropertyChanged(nameof(ItemsDuAn));
                return;
            }
            try
            {
                int iloai = 1;
                var lstData = _service.GetDuAnByDonVi(Model.Id, SelectedDonVi.Id, iloai);
                if (lstData == null) return;
                ItemsDuAn = new ObservableCollection<NhDaDuAn>(lstData);
                if (Model.IIdDuAnId.HasValue)
                    SelectedDuAn = ItemsDuAn.FirstOrDefault(n => Model.IIdDuAnId == n.Id);
                OnPropertyChanged(nameof(ItemsDuAn));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadChuongTrinh()
        {
            ItemsChuongTrinh = new ObservableCollection<NhDmNhiemVuChiModel>();
            if (SelectedDonVi != null)
            {
                var data = _nhDmNhiemVuChiService.FindByDonViId(SelectedDonVi.Id);
                _itemsChuongTrinh = _mapper.Map<ObservableCollection<NhDmNhiemVuChiModel>>(data);
                if (_itemsChuongTrinh != null)
                {
                    SelectedChuongTrinh = _itemsChuongTrinh.FirstOrDefault(x => x.Id == Model.IID_KHTT_NhiemVuChiID);
                }

            }
            OnPropertyChanged(nameof(ItemsChuongTrinh));
        }

        private NhDaGoiThau SetDataGoiThau(NhDaKhlcnhaThau objNhaThau, Guid iIdKhlcNhaThauId, NhDaGoiThauModel data, bool bIsUpdate = false)
        {
            NhDaGoiThau item = new NhDaGoiThau();
            item.Id = data.Id;
            if (IsShowDuAn)
            {
                item.IIdDuAnId = SelectedDuAn.Id;
            }
            item.STenGoiThau = data.STenGoiThau;
            item.sSoKeHoachDatHang = objNhaThau.SSoQuyetDinh;
            item.DNgayKeHoach = objNhaThau.DNgayQuyetDinh;
            item.IIdHinhThucChonNhaThauId = data.IIdHinhThucChonNhaThauId;
            item.IIdPhuongThucDauThauId = data.IIdPhuongThucDauThauId;
            item.IIdLoaiHopDongId = data.IIdLoaiHopDongId;
            item.IThoiGianThucHien = data.IThoiGianThucHien;
            item.FGiaGoiThauEur = data.FGiaGoiThauEur;
            item.FGiaGoiThauNgoaiTeKhac = data.FGiaGoiThauNgoaiTeKhac;
            item.FGiaGoiThauUsd = data.FGiaGoiThauUsd;
            item.FGiaGoiThauVnd = data.FGiaGoiThauVnd;
            item.IIdKhTongTheNhiemVuChiId = objNhaThau.IIdKHTTNhiemVuChiId;
            item.IIdDonViQuanLyId = objNhaThau.IIdDonViQuanLyId;
            item.IIdMaDonViQuanLy = objNhaThau.IIdMaDonViQuanLy;
            //item.DNgayQuyetDinh = objNhaThau.DNgayQuyetDinh;
            if (bIsUpdate)
            {
                item.DNgaySua = DateTime.Now;
                item.SNguoiSua = _sessionService.Current.Principal;
            }
            else
            {
                item.DNgayTao = DateTime.Now;
                item.SNguoiTao = _sessionService.Current.Principal;
            }

            if (data.SelectedGoiThauParent != null)
            {
                item.IIdParentId = data.SelectedGoiThauParent.Id;
                item.IIdGoiThauGocId = data.IIdGoiThauGocId;
                item.BIsGoc = false;
            }
            else
            {
                item.IIdGoiThauGocId = data.Id;
                item.BIsGoc = true;
            }
            item.BActive = true;
            item.IIdKhlcnhaThau = iIdKhlcNhaThauId;
            return item;
        }
        private void OnShowGoiThau()
        {
            //if (SelectedGoiThau == null) return;
            //if (SelectedDuToan == null && !IIdQDDauTuId.HasValue)
            //{
            //    MessageBoxHelper.Error(Resources.MsgErrorChungTuEmpty);
            //    return;
            //}
            //if (SelectedTiGia == null)
            //{
            //    MessageBoxHelper.Error(string.Format(Resources.MsgErrorRequire, "Tỉ giá"));
            //    return;
            //}

            //if (SelectedTiGiaChiTiet == null)
            //{
            //    MessageBoxHelper.Error(string.Format(Resources.MsgErrorRequire, "Mã ngoại tệ khác"));
            //    return;
            //}

            NHGoiThauTrongNuocChiPhiViewModel.IsDetail = IsDetail;
            NHGoiThauTrongNuocChiPhiViewModel.BIsReadOnly = false;
            NHGoiThauTrongNuocChiPhiViewModel.BIsReadOnly = BIsReadOnly;
            //NHGoiThauTrongNuocChiPhiViewModel.ILoaiDuToan = SelectedDuToan.IdLoaiDuToan;
            //NHGoiThauTrongNuocChiPhiViewModel.SLoaiSoCu = SelectedSoCuTrucTiep.ValueItem;
            if (SelectedTiGia != null)
            {
                NHGoiThauTrongNuocChiPhiViewModel.LstTiGiaChiTiet = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
                NHGoiThauTrongNuocChiPhiViewModel.SMaTienTeGoc = SelectedTiGia.SMaTienTeGoc;
            }
            NHGoiThauTrongNuocChiPhiViewModel.FTiGiaNhap = FTiGiaNhap;
            NHGoiThauTrongNuocChiPhiViewModel.ItemNguonVon = GetNguonVonByGoiThau(Model.Id).Clone();
            NHGoiThauTrongNuocChiPhiViewModel.LstChiPhi = GetChiPhiByGoiThau(Model.Id).Clone();
            NHGoiThauTrongNuocChiPhiViewModel.LstHangMuc = GetHangMucByGoiThau(Model.Id).Clone();
            NHGoiThauTrongNuocChiPhiViewModel.Model = Model;
            NHGoiThauTrongNuocChiPhiViewModel.Init();
            NHGoiThauTrongNuocChiPhiViewModel.SavedAction = obj => SaveGoiThauDetail();
            var view = new MSCTNGoiThauTrongNuocChiPhi { DataContext = NHGoiThauTrongNuocChiPhiViewModel };
            view.ShowDialog();
            OnPropertyChanged(nameof(ItemsGoiThau));
        }
        private void SaveGoiThauDetail()
        {
            if (NHGoiThauTrongNuocChiPhiViewModel != null)
            {
                var objGoiThau = ItemsGoiThau.FirstOrDefault(n => n.Id == NHGoiThauTrongNuocChiPhiViewModel.Model.Id);
                if (objGoiThau == null)
                {
                    objGoiThau = Model;
                }
                if (objGoiThau == null) return;
                _itemsNguonVonByGoiThau = _itemsNguonVonByGoiThau.Where(n => n.IIdGoiThauID != objGoiThau.Id).ToList();
                _itemsNguonVonByGoiThau.AddRange(NHGoiThauTrongNuocChiPhiViewModel.ItemNguonVon.Where(n => n.IsChecked));

                _itemsChiPhiByGoiThau = _itemsChiPhiByGoiThau.Where(n => n.IIdGoiThauID != objGoiThau.Id).ToList();
                var lstItemChiPhiInsert = NHGoiThauTrongNuocChiPhiViewModel.ItemChiPhi.Where(n => n.IsChecked);
                _itemsChiPhiByGoiThau.AddRange(lstItemChiPhiInsert);

                _itemsHangMucByGoiThau = _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauID != objGoiThau.Id).ToList();
                _itemsHangMucByGoiThau.AddRange(NHGoiThauTrongNuocChiPhiViewModel.LstHangMuc.Where(n => n.IsChecked && lstItemChiPhiInsert.Select(x => x.IIdChiPhiID).Contains(n.IIdChiPhiID)));

                objGoiThau.FGiaGoiThauUsd = NHGoiThauTrongNuocChiPhiViewModel.ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriPheDuyetUSD);
                objGoiThau.FGiaGoiThauVnd = NHGoiThauTrongNuocChiPhiViewModel.ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriPheDuyetVND);
                //objGoiThau.FGiaGoiThauEur = NHGoiThauTrongNuocChiPhiViewModel.ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriPheDuyetEUR);
                //objGoiThau.FGiaGoiThauNgoaiTeKhac = NHGoiThauTrongNuocChiPhiViewModel.ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriPheDuyetNgoaiTeKhac);
                Model.FGiaGoiThauUsd = objGoiThau.FGiaGoiThauUsd;
                Model.FGiaGoiThauVnd = objGoiThau.FGiaGoiThauVnd;
                //Model.FGiaGoiThauEur = objGoiThau.FGiaGoiThauEur;
                //Model.FGiaGoiThauNgoaiTeKhac = objGoiThau.FGiaGoiThauNgoaiTeKhac;
                NHGoiThauTrongNuocChiPhiViewModel.ItemChiPhi.Clear();
                OnPropertyChanged(nameof(ItemsGoiThau));
            }
            // OnSave(obj);
        }
        private bool OnDeleteGoiThau()
        {
            if (BIsReadOnlyButtonAdd == false)
            {
                if (SelectedGoiThau == null)
                {
                    MessageBoxHelper.Warning(Resources.MsgChooseItem);
                    BIsReadOnlyButtonAdd = false;
                    return BIsReadOnlyButtonAdd;
                }
                else
                {
                    BIsReadOnlyButtonAdd = false;
                }
            }
            else
            {
                MessageBoxHelper.Warning(Resources.MsgChooseItem);
                BIsReadOnlyButtonAdd = true;
                return BIsReadOnlyButtonAdd;
            }
            SelectedGoiThau.IsDeleted = !SelectedGoiThau.IsDeleted;
            OnPropertyChanged(nameof(ItemsGoiThau));
            return BIsReadOnlyButtonAdd;
        }
        private bool OnAddGoiThau()
        {
            BIsReadOnlyButtonAdd = true;
            NhDaGoiThauModel objGoiThau = new NhDaGoiThauModel();
            objGoiThau.Id = Guid.NewGuid();
            objGoiThau.IsAdded = true;
            objGoiThau.ILoai = 2; // các gói thầu trong màn KHLCNT là gói thầu trong nước
            objGoiThau.IThuocMenu = IThuocMenu;
            ItemsGoiThau.Add(objGoiThau);
            OnPropertyChanged(nameof(ItemsGoiThau));
            return BIsReadOnlyButtonAdd;
        }
        private void LoadDuToan()
        {
            ItemsDuToan = new ObservableCollection<NhDaDuToan>();
            //if ((SelectedSoCuTrucTiep == null
            //    || SelectedSoCuTrucTiep.ValueItem != ((int)SO_CU_TRUC_TIEP.TypeValue.DU_TOAN).ToString()
            //    || SelectedDuAn == null) && IsShowDuAn)
            //    return;
            var data = new List<NhDaDuToan>();
            if (IsShowDuAn && SelectedDuAn != null)
            {
                data = _dutoanService.FindDuAnInKhlcNhaThau(Model.Id, SelectedDuAn.Id).ToList();
            }
            else
            {
                if (SelectedDonVi == null) return;
                if (Model.ICheckLuong == 1)
                {
                    data = _dutoanService.FindDuToanMoTaMSByIdDonVi(SelectedDonVi.IIDMaDonVi, 4).ToList();
                }
                else
                {
                    data = _dutoanService.FindDuToanMoTaMSByIdDonVi(SelectedDonVi.IIDMaDonVi, 4).ToList();
                }
            }
            if (data != null) ItemsDuToan = new ObservableCollection<NhDaDuToan>(data);

            if (Model.IIdDuToanID != null && Model.IIdDuToanID.HasValue)
                SelectedDuToan = ItemsDuToan.FirstOrDefault(n => n.Id == Model.IIdDuToanId);
            OnPropertyChanged(nameof(ItemsDuToan));
        }
        private void LoadHinhThucChonNhaThau()
        {
            var data = _nhDmHinhThucChonNhaThauService.FindAll();
            _itemsHinhThucChonNhaThau = _mapper.Map<ObservableCollection<NhDmHinhThucChonNhaThauModel>>(data);
            if (_itemsHinhThucChonNhaThau != null)
            {
                SelectedHinhThucChonNhaThau = _itemsHinhThucChonNhaThau.FirstOrDefault(x => x.Id == Model.IIdHinhThucChonNhaThauId);
            }
            OnPropertyChanged(nameof(ItemsHinhThucChonNhaThau));
        }


        private void LoadPhuongThucChonNhaThau()
        {
            var data = _nhDmPhuongThucChonNhaThauService.FindAll();
            _itemsPhuongThucChonNhaThau = _mapper.Map<ObservableCollection<NhDmPhuongThucChonNhaThauModel>>(data);
            if (_itemsPhuongThucChonNhaThau != null)
            {
                SelectedPhuongThucChonNhaThau = _itemsPhuongThucChonNhaThau.FirstOrDefault(x => x.Id == Model.IIdPhuongThucDauThauId);
            }
            OnPropertyChanged(nameof(ItemsPhuongThucChonNhaThau));
        }
        private void FindQDDauTuByDuAn()
        {
            if (SelectedDuAn == null) return;
            var objQDDauTu = _qddautuService.FindByDuAnId(SelectedDuAn.Id);
            if (objQDDauTu == null) return;
            SSoQDDauTu = objQDDauTu.SSoQuyetDinh;
            IIdQDDauTuId = objQDDauTu.Id;
            FGiaTriEUR = objQDDauTu.FGiaTriEur ?? 0;
            FGiaTriUSD = objQDDauTu.FGiaTriUsd ?? 0;
            FGiaTriVND = objQDDauTu.FGiaTriVnd ?? 0;
            FGiaTriNgoaiTeKhac = objQDDauTu.FGiaTriNgoaiTeKhac ?? 0;
            GetQdDauTuDetail(objQDDauTu.Id);
            OnPropertyChanged(nameof(SSoQDDauTu));
            OnPropertyChanged(nameof(IIdQDDauTuId));
            OnPropertyChanged(nameof(FGiaTriEUR));
            OnPropertyChanged(nameof(FGiaTriUSD));
            OnPropertyChanged(nameof(FGiaTriVND));
            OnPropertyChanged(nameof(FGiaTriNgoaiTeKhac));
        }
        private void LoadTiGiaChiTiet()
        {
            _itemsTiGiaChiTiet = new ObservableCollection<NhDmTiGiaChiTietModel>();
            var lstTiGiaDef = new List<string>();
            lstTiGiaDef.Add("VND");
            lstTiGiaDef.Add("USD");
            lstTiGiaDef.Add("EUR");
            if (SelectedTiGia != null)
            {
                // Lấy danh sách tỉ giá chi tiết
                IEnumerable<NhDmTiGiaChiTiet> listTiGiaChiTiet = _nhDmTiGiaChiTietService.FindByTiGiaId(_selectedTiGia.Id);

                // Lọc ra danh sách tỉ giá ngoại tệ khác
                ItemsTiGiaChiTiet = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(listTiGiaChiTiet.Where(x => !lstTiGiaDef.Contains(x.SMaTienTeQuyDoi)));
                // Lọc ra danh sách tỉ giá chính (USD, VND, EUR)
                ItemsTiGiaChiTietChinh = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(listTiGiaChiTiet.Where(x => lstTiGiaDef.Contains(x.SMaTienTeQuyDoi)));

                if (!string.IsNullOrEmpty(Model.SMaNgoaiTeKhac))
                    SelectedTiGiaChiTiet = ItemsTiGiaChiTiet.FirstOrDefault(n => n.SMaTienTeQuyDoi == Model.SMaNgoaiTeKhac);
                else
                    SelectedTiGiaChiTiet = _itemsTiGiaChiTiet.FirstOrDefault();
            }
            else
            {
                ItemsTiGiaChiTiet = new ObservableCollection<NhDmTiGiaChiTietModel>();
                ItemsTiGiaChiTietChinh = new ObservableCollection<NhDmTiGiaChiTietModel>();
                SelectedTiGiaChiTiet = null;
            }
        }

        private void SetLabelTiGiaChiTiet()
        {
            if (ItemsTiGiaChiTiet != null && ItemsTiGiaChiTiet.Count > 0 && ItemsTiGiaChiTietChinh != null && _selectedTiGia != null)
            {
                var label = new StringBuilder();
                // Load thông tin tỉ giá chính (USD, VND, EUR)
                foreach (var tg in ItemsTiGiaChiTietChinh)
                {
                    label.Append("1 " + _selectedTiGia.SMaTienTeGoc + " = " + tg.FTiGia + " " + tg.SMaTienTeQuyDoi + ". ");
                }

                // Load thêm các thông tin tỉ giá phụ (Ngoại tệ khác)
                if (SelectedTiGiaChiTiet != null)
                {
                    var tgNgoaiTeKhac = ItemsTiGiaChiTiet.FirstOrDefault(x => x.Id == SelectedTiGiaChiTiet.Id);
                    label.Append("1 " + _selectedTiGia.SMaTienTeGoc + " = " + tgNgoaiTeKhac.FTiGia + " " + tgNgoaiTeKhac.SMaTienTeQuyDoi + ". ");
                }

                Model.STiGiaChiTietLabel = label.ToString();
            }
            else
            {
                Model.STiGiaChiTietLabel = null;
            }
        }

        private void LoadThongTinNhaThauHopDong()
        {
            var data = _nhDaGoiThauService.GetThongTinHopDongByIdGoiThau(Model.Id);
            _itemsThongTinNhaThauHopDong = _mapper.Map<ObservableCollection<NhDaThongTinNhaThauHopDongModel>>(data);
            OnPropertyChanged(nameof(ItemsThongTinNhaThauHopDong));
        }
        private void SetValueBySelectDuToan()
        {
            if (SelectedDuToan == null) return;
            FGiaTriEUR = SelectedDuToan.FGiaTriEur ?? 0;
            FGiaTriUSD = SelectedDuToan.FGiaTriUsd ?? 0;
            FGiaTriVND = SelectedDuToan.FGiaTriVnd ?? 0;
            FGiaTriNgoaiTeKhac = SelectedDuToan.FGiaTriNgoaiTeKhac ?? 0;
            if (SelectedDuToan.IIdKHTTNhiemVuChiId.HasValue)
            {
                SelectedChuongTrinh = ItemsChuongTrinh.FirstOrDefault(x => x.IIdKHTTNhiemVuChiId == SelectedDuToan.IIdKHTTNhiemVuChiId);
            }
            GetDuToanDetail(SelectedDuToan.Id);
            OnPropertyChanged(nameof(FGiaTriEUR));
            OnPropertyChanged(nameof(FGiaTriUSD));
            OnPropertyChanged(nameof(FGiaTriVND));
            OnPropertyChanged(nameof(FGiaTriNgoaiTeKhac));
        }
        private ObservableCollection<NhDaGoiThauDetailNguonVonModel> GetNguonVonByGoiThau(Guid iIDGoiThauId)
        {
            GetKhlcNhaThauDetailById();
            Dictionary<int?, NhDaGoiThauDetailNguonVonModel> dicNguonVon = new Dictionary<int?, NhDaGoiThauDetailNguonVonModel>();
            if (_itemsNguonVonByGoiThau == null) _itemsNguonVonByGoiThau = new List<NhDaGoiThauDetailNguonVonModel>();

            if (_itemsNguonVonByGoiThau.Any(n => n.IIdGoiThauID == iIDGoiThauId))
            {
                _itemsNguonVonByGoiThau = _itemsNguonVonByGoiThau
                    .Where(n => n.IIdGoiThauID == iIDGoiThauId
                    && (n.FGiaTriPheDuyetNgoaiTeKhac != 0 || n.FGiaTriPheDuyetUSD != 0 || n.FGiaTriPheDuyetVND != 0 || n.FGiaTriPheDuyetEUR != 0)).ToList();

                dicNguonVon = _itemsNguonVonByGoiThau
                    .Where(n => n.IIdGoiThauID == iIDGoiThauId
                     && (n.FGiaTriPheDuyetNgoaiTeKhac != 0 || n.FGiaTriPheDuyetUSD != 0 || n.FGiaTriPheDuyetVND != 0 || n.FGiaTriPheDuyetEUR != 0))
                    .ToDictionary(n => n.IIdNguonVonID, n => n);
            }


            var lstDiff = _itemsNguonVonByGoiThau.Where(n => n.IIdGoiThauID != iIDGoiThauId)
                .GroupBy(n => n.IIdNguonVonID)
                .Select(n => new
                {
                    iIdNguonVonId = n.Key,
                    FGiaTriUSD = n.Sum(n => n.FGiaTriPheDuyetUSD),
                    FGiaTriEUR = n.Sum(n => n.FGiaTriPheDuyetEUR),
                    FGiaTriNgoaiTeKhac = n.Sum(n => n.FGiaTriPheDuyetNgoaiTeKhac),
                    FGiaTriVND = n.Sum(n => n.FGiaTriPheDuyetVND),
                });

            List<NhDaGoiThauDetailNguonVonModel> lstData = new List<NhDaGoiThauDetailNguonVonModel>();
            if (_itemsNguonVon == null) _itemsNguonVon = new List<NhDaGoiThauDetailNguonVonModel>();

            foreach (var item in _itemsNguonVon.OrderBy(n => n.IIdNguonVonID))
            {
                NhDaGoiThauDetailNguonVonModel obj = new NhDaGoiThauDetailNguonVonModel()
                {
                    IIdGoiThauID = iIDGoiThauId,
                    IIdNguonVonID = item.IIdNguonVonID,
                    STenNguonVon = item.STenNguonVon,
                    Id = item.Id
                };

                if (dicNguonVon.ContainsKey(item.IIdNguonVonID))
                {
                    obj.IsChecked = true;
                    obj.FGiaTriPheDuyetVND = dicNguonVon[item.IIdNguonVonID].FGiaTriPheDuyetVND;
                    obj.FGiaTriPheDuyetUSD = dicNguonVon[item.IIdNguonVonID].FGiaTriPheDuyetUSD;
                    obj.FGiaTriPheDuyetEUR = dicNguonVon[item.IIdNguonVonID].FGiaTriPheDuyetEUR;
                    obj.FGiaTriPheDuyetNgoaiTeKhac = dicNguonVon[item.IIdNguonVonID].FGiaTriPheDuyetNgoaiTeKhac;
                }

                obj.FGiaTriVND = item.FGiaTriVND;
                obj.FGiaTriUSD = item.FGiaTriUSD;
                obj.FGiaTriEUR = item.FGiaTriEUR;
                obj.FGiaTriNgoaiTeKhac = item.FGiaTriNgoaiTeKhac;

                if (lstDiff.Any(n => n.iIdNguonVonId == item.IIdNguonVonID))
                {
                    var objNguonVonDiff = lstDiff.FirstOrDefault(n => n.iIdNguonVonId == item.IIdNguonVonID);
                    obj.FGiaTriVND -= objNguonVonDiff.FGiaTriVND;
                    obj.FGiaTriUSD -= objNguonVonDiff.FGiaTriUSD;
                    obj.FGiaTriEUR -= objNguonVonDiff.FGiaTriEUR;
                    obj.FGiaTriNgoaiTeKhac -= objNguonVonDiff.FGiaTriNgoaiTeKhac;
                }

                lstData.Add(obj);
            }

            _itemsNguonVonByGoiThau = _itemsNguonVonByGoiThau.Where(n => n.IIdGoiThauID != iIDGoiThauId).ToList();
            _itemsNguonVonByGoiThau.AddRange(lstData);
            return new ObservableCollection<NhDaGoiThauDetailNguonVonModel>(lstData);
        }

        private ObservableCollection<NhDaGoiThauDetailChiPhiModel> GetChiPhiByGoiThau(Guid iIDGoiThauId)
        {
            GetKhlcNhaThauDetailById();
            Dictionary<Guid?, NhDaGoiThauDetailChiPhiModel> dicChiPhi = new Dictionary<Guid?, NhDaGoiThauDetailChiPhiModel>();
            if (_itemsChiPhiByGoiThau == null) _itemsChiPhiByGoiThau = new List<NhDaGoiThauDetailChiPhiModel>();
            if (_itemsChiPhiByGoiThau != null && _itemsChiPhiByGoiThau.Any(n => n.IIdGoiThauID == iIDGoiThauId))
            {
                _itemsChiPhiByGoiThau = _itemsChiPhiByGoiThau
                    .Where(n => n.IIdGoiThauID == iIDGoiThauId
                    && (n.FGiaTriPheDuyetNgoaiTeKhac != 0 || n.FGiaTriPheDuyetUSD != 0 || n.FGiaTriPheDuyetVND != 0 || n.FGiaTriPheDuyetEUR != 0)).ToList();
                dicChiPhi = _itemsChiPhiByGoiThau.ToDictionary(n => n.IIdChiPhiID, n => n);
            }
            var lstDiff = _itemsChiPhiByGoiThau.Where(n => n.IIdGoiThauID != iIDGoiThauId)
                .GroupBy(n => n.IIdChiPhiID)
                .Select(n => new
                {
                    iIdChiPhiId = n.Key,
                    FGiaTriUSD = n.Sum(n => n.FGiaTriPheDuyetUSD),
                    FGiaTriEUR = n.Sum(n => n.FGiaTriPheDuyetEUR),
                    FGiaTriNgoaiTeKhac = n.Sum(n => n.FGiaTriPheDuyetNgoaiTeKhac),
                    FGiaTriVND = n.Sum(n => n.FGiaTriPheDuyetVND)
                });

            List<NhDaGoiThauDetailChiPhiModel> lstData = new List<NhDaGoiThauDetailChiPhiModel>();
            if (_itemsChiPhi == null) _itemsChiPhi = new List<NhDaGoiThauDetailChiPhiModel>();
            foreach (var item in _itemsChiPhi.OrderBy(n => n.SMaOrder))
            {
                NhDaGoiThauDetailChiPhiModel obj = new NhDaGoiThauDetailChiPhiModel()
                {
                    IIdGoiThauID = iIDGoiThauId,
                    IIdGoiThauNguonVonId = item.IIdGoiThauNguonVonId,
                    IIdChiPhiID = item.IIdChiPhiID,
                    STenChiPhi = item.STenChiPhi,
                    SMaOrder = item.SMaOrder,
                    IIdParentID = item.IIdParentID,
                    IIdNguonVonId = item.IIdNguonVonId
                };

                if (dicChiPhi.ContainsKey(item.IIdChiPhiID))
                {
                    obj.IsChecked = true;
                    obj.FGiaTriPheDuyetVND = dicChiPhi[item.IIdChiPhiID].FGiaTriPheDuyetVND;
                    obj.FGiaTriPheDuyetUSD = dicChiPhi[item.IIdChiPhiID].FGiaTriPheDuyetUSD;
                    obj.FGiaTriPheDuyetEUR = dicChiPhi[item.IIdChiPhiID].FGiaTriPheDuyetEUR;
                    obj.FGiaTriPheDuyetNgoaiTeKhac = dicChiPhi[item.IIdChiPhiID].FGiaTriPheDuyetNgoaiTeKhac;
                }

                obj.FGiaTriVND = item.FGiaTriVND;
                obj.FGiaTriUSD = item.FGiaTriUSD;
                obj.FGiaTriEUR = item.FGiaTriEUR;
                obj.FGiaTriNgoaiTeKhac = item.FGiaTriNgoaiTeKhac;

                //if (lstDiff.Any(n => n.iIdChiPhiId == item.IIdChiPhiID))
                //{
                //    var objChiPhiDiff = lstDiff.FirstOrDefault(n => n.iIdChiPhiId == item.IIdChiPhiID);
                //    obj.FGiaTriVND -= objChiPhiDiff.FGiaTriVND;
                //    obj.FGiaTriUSD -= objChiPhiDiff.FGiaTriUSD;
                //    obj.FGiaTriEUR -= objChiPhiDiff.FGiaTriEUR;
                //    obj.FGiaTriNgoaiTeKhac -= objChiPhiDiff.FGiaTriNgoaiTeKhac;
                //}

                lstData.Add(obj);
            }

            _itemsChiPhiByGoiThau = _itemsChiPhiByGoiThau.Where(n => n.IIdGoiThauID != iIDGoiThauId).ToList();
            _itemsChiPhiByGoiThau.AddRange(lstData);
            return new ObservableCollection<NhDaGoiThauDetailChiPhiModel>(lstData);
        }

        private List<NhDaGoiThauDetailHangMucModel> GetHangMucByGoiThau(Guid iIDGoiThauId)
        {
            GetKhlcNhaThauDetailById();
            Dictionary<Guid?, NhDaGoiThauDetailHangMucModel> dicHangMuc = new Dictionary<Guid?, NhDaGoiThauDetailHangMucModel>();
            if (_itemsHangMucByGoiThau == null) _itemsHangMucByGoiThau = new List<NhDaGoiThauDetailHangMucModel>();

            if (_itemsHangMucByGoiThau.Any(n => n.IIdGoiThauID == iIDGoiThauId))
                dicHangMuc = _itemsHangMucByGoiThau
                    .Where(n => n.IIdGoiThauID == iIDGoiThauId
                     && (n.FGiaTriPheDuyetNgoaiTeKhac != 0 || n.FGiaTriPheDuyetUSD != 0 || n.FGiaTriPheDuyetVND != 0 || n.FGiaTriPheDuyetEUR != 0))
                    .ToDictionary(n => n.IIdHangMucID, n => n);

            var lstDiff = _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauID != iIDGoiThauId)
                .GroupBy(n => n.IIdHangMucID)
                .Select(n => new
                {
                    iIdHangMucId = n.Key,
                    FGiaTriUSD = n.Sum(n => n.FGiaTriPheDuyetUSD),
                    FGiaTriEUR = n.Sum(n => n.FGiaTriPheDuyetEUR),
                    FGiaTriNgoaiTeKhac = n.Sum(n => n.FGiaTriPheDuyetNgoaiTeKhac),
                    FGiaTriVND = n.Sum(n => n.FGiaTriPheDuyetVND)
                });

            List<NhDaGoiThauDetailHangMucModel> lstDataCheck = _itemsHangMucByGoiThau
                    .GroupBy(x => (x.IsChecked, x.IIdHangMucID)).Select(x => x.First())
                    .Select(x => new NhDaGoiThauDetailHangMucModel
                    {
                        IsChecked = x.IsChecked,
                        IIdHangMucID = x.IIdHangMucID
                    }).ToList();


            List<NhDaGoiThauDetailHangMucModel> lstData = new List<NhDaGoiThauDetailHangMucModel>();
            if (_itemsHangMuc == null) _itemsHangMuc = new List<NhDaGoiThauDetailHangMucModel>();

            foreach (var item in _itemsHangMuc.OrderBy(n => n.SMaOrder))
            {
                NhDaGoiThauDetailHangMucModel obj = new NhDaGoiThauDetailHangMucModel()
                {
                    IIdGoiThauID = iIDGoiThauId,
                    IIdChiPhiID = item.IIdChiPhiID,
                    IIdParentID = item.IIdParentID,
                    IIdHangMucID = item.IIdHangMucID,
                    STenHangMuc = item.STenHangMuc,
                    SMaOrder = item.SMaOrder
                };

                if (dicHangMuc.ContainsKey(item.IIdHangMucID))
                {
                    obj.IsChecked = true;
                    obj.FGiaTriPheDuyetVND = dicHangMuc[item.IIdHangMucID].FGiaTriPheDuyetVND;
                    obj.FGiaTriPheDuyetUSD = dicHangMuc[item.IIdHangMucID].FGiaTriPheDuyetUSD;
                    obj.FGiaTriPheDuyetEUR = dicHangMuc[item.IIdHangMucID].FGiaTriPheDuyetEUR;
                    obj.FGiaTriPheDuyetNgoaiTeKhac = dicHangMuc[item.IIdHangMucID].FGiaTriPheDuyetNgoaiTeKhac;
                }

                obj.FGiaTriVND = item.FGiaTriVND;
                obj.FGiaTriUSD = item.FGiaTriUSD;
                obj.FGiaTriEUR = item.FGiaTriEUR;
                obj.FGiaTriNgoaiTeKhac = item.FGiaTriNgoaiTeKhac;

                //if (lstDiff.Any(n => n.iIdHangMucId == item.IIdHangMucID))
                //{
                //    var objHangMucDiff = lstDiff.FirstOrDefault(n => n.iIdHangMucId == item.IIdHangMucID);
                //    obj.FGiaTriVND -= objHangMucDiff.FGiaTriVND;
                //    obj.FGiaTriUSD -= objHangMucDiff.FGiaTriUSD;
                //    obj.FGiaTriEUR -= objHangMucDiff.FGiaTriEUR;
                //    obj.FGiaTriNgoaiTeKhac -= objHangMucDiff.FGiaTriNgoaiTeKhac;
                //}

                var lstcheck = lstDataCheck.Where(x => x.IIdHangMucID == item.IIdHangMucID && x.IsChecked == true).FirstOrDefault();
                List<NhDaGoiThauDetailHangMucModel> lstCon = _itemsHangMucByGoiThau.Where(x => x.IIdParentID == item.IIdHangMucID).ToList();
                var checkCha = 0;
                foreach (var con in lstCon)
                {
                    if (con.IsChecked == false)
                        checkCha++;
                }
                if (SelectedDuToan != null)
                {
                    if (lstcheck == null && SelectedDuToan.IdLoaiDuToan == 1)
                        lstData.Add(obj);
                    else if (dicHangMuc.ContainsKey(item.IIdHangMucID) && obj.IsChecked == true && SelectedDuToan.IdLoaiDuToan == 1)
                    {
                        if ((lstcheck != null ? lstcheck.IIdHangMucID : Guid.Empty) != item.IIdHangMucID)
                            obj.IsChecked = false;

                        lstData.Add(obj);
                    }
                    else if (SelectedDuToan.IdLoaiDuToan != 1)
                        lstData.Add(obj);
                    else if (checkCha != 0)
                    {
                        obj.IsChecked = false;
                        lstData.Add(obj);
                    }
                }

            }

            _itemsHangMucByGoiThau = _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauID != iIDGoiThauId).ToList();
            if (SelectedDuToan != null)
            {
                if (SelectedDuToan.IdLoaiDuToan == 1)
                {
                    var a = _itemsChiPhi;
                }
            }

            _itemsHangMucByGoiThau.AddRange(lstData);
            return lstData;
        }
        NhDaKhlcnhaThau objGoiThau = new NhDaKhlcnhaThau();
        private void LoadTiGia()
        {
            var drpData = _nhDmTiGiaService.FindAll();
            ItemsTiGia = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(drpData);
            if (Model.IIdTiGiaID.HasValue)
                SelectedTiGia = ItemsTiGia.FirstOrDefault(n => n.Id == Model.IIdTiGiaID.Value);
        }


        public override void OnSave(object obj)
        {
            if (SelectedHinhThucChonNhaThau != null)
            {
                Model.IIdHinhThucChonNhaThauId = SelectedHinhThucChonNhaThau.Id;
            }
            if (SelectedPhuongThucChonNhaThau != null)
            {
                Model.IIdPhuongThucDauThauId = SelectedPhuongThucChonNhaThau.Id;
            }
            if (SelectedDuToan != null)
            {
                Model.IIdDuToanId = SelectedDuToan.Id;
            }
            if (SelectedDuAn != null)
            {
                Model.IIdDuAnId = SelectedDuAn.Id;
                Model.IIdKhTongTheNhiemVuChiId = SelectedDuAn.IIdKhttNhiemVuChiId;
            }
            if (SelectedChuongTrinh != null)
            {
                Model.IID_KHTT_NhiemVuChiID = SelectedChuongTrinh.IIdKHTTNhiemVuChiId;
            }
            if (SelectedLoaiHopDong != null)
            {
                Model.IIdLoaiHopDongId = SelectedLoaiHopDong.IIdLoaiHopDongId;
            }
            if (SelectedDonVi != null)
            {
                Model.IIdDonViQuanLyId = SelectedDonVi.Id;
            }
            if (SelectedTiGia != null)
            {
                Model.IIdTiGiaID = SelectedTiGia.Id;
            }
            if (!ValidateViewModelHelper.Validate(Model)) return;
            if (!ValidateData())
            {
                return;
            }
            var entity = _mapper.Map<NhDaGoiThau>(Model);
            entity.ILoai = ILoai;
            entity.IThuocMenu = IThuocMenu;
            entity.SNguoiSua = _sessionService.Current.Principal;
            entity.DNgayQuyetDinh = Model.DNgayQuyetDinh;
            if (Model.DBatDauChonNhaThau != null)
            {
                entity.DBatDauChonNhaThau = Model.DBatDauChonNhaThau;
            }
            if (Model.DKetThucChonNhaThau != null)
            {
                entity.DKetThucChonNhaThau = Model.DKetThucChonNhaThau;
            }
            if (SelectedChuongTrinh != null)
            {
                entity.IIdKhTongTheNhiemVuChiId = SelectedChuongTrinh.IIdKHTTNhiemVuChiId;
            }
            if (Model.SSoKeHoachDatHang != null)
            {
                entity.sSoKeHoachDatHang = Model.SSoKeHoachDatHang;
            }
            if (SelectedDonVi.Id != null)
            {
                entity.IIdDonViQuanLyId = SelectedDonVi.Id;
            }
            if (SelectedTiGia != null)
            {
                entity.IIdTiGiaId = SelectedTiGia.Id;
            }
            //if (SelectedTiGiaChiTiet.Id != null)
            //{
            //    entity.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
            //}
            if (SelectedDuToan.Id != null)
            {
                entity.IIdDuToanId = SelectedDuToan.Id;
            }
            if (SelectedDuAn.Id != null)
            {
                entity.IIdDuAnId = SelectedDuAn.Id;
            }
            if (Model.STenGoiThau != null)
            {
                entity.STenGoiThau = Model.STenGoiThau;
            }
            if (Model.DNgayKeHoach != null)
            {
                entity.DNgayKeHoach = Model.DNgayKeHoach;
            }
            if (Model.FGiaGoiThauEur != null)
            {
                entity.FGiaGoiThauEur = Model.FGiaGoiThauEur;
            }
            if (FTiGiaNhap != null)
            {
                entity.FTiGiaNhap = FTiGiaNhap;
            }
            if (Model.FGiaGoiThauEur != null)
            {
                entity.FGiaGoiThauEur = Model.FGiaGoiThauEur;
            }
            if (Model.FGiaGoiThauNgoaiTeKhac != null)
            {
                entity.FGiaGoiThauNgoaiTeKhac = Model.FGiaGoiThauNgoaiTeKhac;
            }
            if (SelectedHinhThucChonNhaThau.Id != null)
            {
                entity.IIdHinhThucChonNhaThauId = SelectedHinhThucChonNhaThau.Id;
            }
            if (SelectedPhuongThucChonNhaThau != null)
            {
                entity.IIdPhuongThucDauThauId = SelectedPhuongThucChonNhaThau.Id;
            }
            if (Model.SSoKeHoachDatHang != null)
            {
                entity.sSoKeHoachDatHang = Model.SSoKeHoachDatHang;
            }

            //

            if (_itemsNguonVonByGoiThau.Count != 0)
            {
                var GetAllGoiThau = _mapper.Map<List<NhDaGoiThauQuery>>(_goithauService.GetAll());
                var checklistgoithau = GetAllGoiThau.Where(n => n.Id == _itemsNguonVonByGoiThau[0].IIdGoiThauID).ToList();
                if (checklistgoithau.Count == 0)
                {
                    entity.Id = _itemsNguonVonByGoiThau[0].IIdGoiThauID != null ? _itemsNguonVonByGoiThau[0].IIdGoiThauID.Value : Guid.Empty;
                    entity.iCheckLuong = 1;
                    entity.DNgayTao = DateTime.Now;
                    _nhDaGoiThauService.Add(entity);
                    SaveDetailGoiThau(entity.Id);
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    Window window = obj as Window;
                    SavedAction?.Invoke(Model);
                    window.Close();
                }
                else
                {
                    entity.DNgaySua = DateTime.Now;
                    _nhDaGoiThauService.Update(entity);
                    SaveDetailGoiThau(Model.Id);
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    Window window = obj as Window;
                    SavedAction?.Invoke(Model);
                    window.Close();
                }
            }
            else
            {
                if (Model.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                    entity.iCheckLuong = 1;
                    entity.DNgayTao = DateTime.Now;
                    _nhDaGoiThauService.Add(entity);
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    Window window = obj as Window;
                    SavedAction?.Invoke(Model);
                    SaveDetailGoiThau(entity.Id);
                    window.Close();

                }
                else
                {
                    entity.DNgaySua = DateTime.Now;
                    _nhDaGoiThauService.Update(entity);
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    Window window = obj as Window;
                    SavedAction?.Invoke(Model);
                    SaveDetailGoiThau(Model.Id);
                    window.Close();

                }
            }
            LoadData();

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

        private void SaveDetailGoiThau(Guid id)
        {
            Dictionary<Guid, Guid> dicGoiThauNV = new Dictionary<Guid, Guid>();
            SaveNguonVon(ref dicGoiThauNV, id);
            Dictionary<string, Guid> dicGoiThauChiPhi = new Dictionary<string, Guid>();
            SaveChiPhi(ref dicGoiThauChiPhi, id, dicGoiThauNV);
            SaveHangMuc(dicGoiThauChiPhi, id);
        }

        private void SaveNguonVon(ref Dictionary<Guid, Guid> dicGoiThauNV, Guid id)
        {
            List<NhDaGoiThauNguonVon> listData = _goithauNguonVonService.FindByListNguonVonGoiThauId(Model.Id).ToList();
            var lstDelete = new List<NhDaGoiThauNguonVon>();
            var listAdd = new List<NhDaGoiThauNguonVon>();
            var ListUpdate = new List<NhDaGoiThauNguonVon>();
            var lstNguonVon = _itemsNguonVonByGoiThau.Where(n => n.IsChecked).Select(n => new NhDaGoiThauNguonVon()
            {
                FTienGoiThauEur = n.FGiaTriPheDuyetEUR,
                FTienGoiThauNgoaiTeKhac = n.FGiaTriPheDuyetNgoaiTeKhac,
                FTienGoiThauUsd = n.FGiaTriPheDuyetUSD,
                FTienGoiThauVnd = n.FGiaTriPheDuyetVND,
                IIdGoiThauId = id,
                IIdNguonVonId = n.IIdNguonVonID,
                IsAdded = true
            }).ToList();
            foreach (var item in lstNguonVon)
            {
                var itemData = listData.Where(x => x.IIdGoiThauId == item.IIdGoiThauId && x.IIdDuToanNguonVonId == item.IIdDuToanNguonVonId).FirstOrDefault();
                if (itemData != null)
                {
                    listData.Remove(itemData);
                    var itemUpdate = lstNguonVon.Where(x => x.IIdGoiThauId == item.IIdGoiThauId && x.IIdDuToanNguonVonId == item.IIdDuToanNguonVonId).FirstOrDefault();
                    itemData.FTienGoiThauEur = item.FTienGoiThauEur;
                    itemData.FTienGoiThauNgoaiTeKhac = item.FTienGoiThauNgoaiTeKhac;
                    itemData.FTienGoiThauUsd = item.FTienGoiThauUsd;
                    itemData.FTienGoiThauVnd = item.FTienGoiThauVnd;
                    ListUpdate.Add(itemData);
                }
                else
                {
                    item.IsAdded = true;
                    listAdd.Add(item);
                }
            }
            lstDelete = listData;
            if (listAdd.Count > 0)
            {
                _goithauNguonVonService.AddRange(listAdd);
                foreach (var item in listAdd)
                {
                    dicGoiThauNV.Add(_itemsNguonVonByGoiThau.FirstOrDefault(f => f.IIdNguonVonID == item.IIdNguonVonId).Id ?? Guid.Empty, item.Id);
                }
            }
            if (ListUpdate.Count > 0)
            {
                _goithauNguonVonService.UpdateRange(ListUpdate);
            }
            if (lstDelete.Count > 0)
            {
                foreach (var item in lstDelete)
                {
                    _goithauNguonVonService.DeleteNguonVon(item.Id);
                }
            }
        }

        private void SaveChiPhi(ref Dictionary<string, Guid> dicGoiThauChiPhi, Guid id, Dictionary<Guid, Guid> dicGoiThauNV)
        {
            List<NhDaGoiThauChiPhi> listData = _goithauChiPhiService.FindListChiPhiByGT(Model.Id).ToList();
            var lstDelete = new List<NhDaGoiThauChiPhi>();
            var listAdd = new List<NhDaGoiThauChiPhi>();
            var ListUpdate = new List<NhDaGoiThauChiPhi>();
            var listChiPhiResult = new List<NhDaGoiThauChiPhi>();
            var lstChiPhi = _itemsChiPhiByGoiThau.Where(n => n.IsChecked).Select(n => new NhDaGoiThauChiPhi()
            {
                FTienGoiThauEur = n.FGiaTriPheDuyetEUR,
                FTienGoiThauNgoaiTeKhac = n.FGiaTriPheDuyetNgoaiTeKhac,
                FTienGoiThauUsd = n.FGiaTriPheDuyetUSD,
                FTienGoiThauVnd = n.FGiaTriPheDuyetVND,
                IIdGoiThauId = id,
                IIdGoiThauNguonVonId = dicGoiThauNV.Any() ? (dicGoiThauNV.ContainsKey((Guid)n.IIdNguonVonId) ? dicGoiThauNV[(Guid)n.IIdNguonVonId] : Guid.Empty) : Guid.Empty,
                IIdDuToanChiPhiId = SelectedDuToan != null ? n.IIdChiPhiID : null,
                IIdQdDauTuChiPhiId = IIdQDDauTuId.HasValue ? n.IIdChiPhiID : null,
            }).ToList();
            foreach (var item in lstChiPhi)
            {
                var itemData = listData.Where(x => x.IIdGoiThauId == item.IIdGoiThauId && x.IIdDuToanChiPhiId == item.IIdDuToanChiPhiId).FirstOrDefault();
                if (itemData != null)
                {
                    listData.Remove(itemData);
                    var itemUpdate = lstChiPhi.Where(x => x.IIdGoiThauId == item.IIdGoiThauId && x.IIdDuToanChiPhiId == item.IIdDuToanChiPhiId).FirstOrDefault();
                    itemData.FTienGoiThauEur = item.FTienGoiThauEur;
                    itemData.FTienGoiThauNgoaiTeKhac = item.FTienGoiThauNgoaiTeKhac;
                    itemData.FTienGoiThauUsd = item.FTienGoiThauUsd;
                    itemData.FTienGoiThauVnd = item.FTienGoiThauVnd;
                    ListUpdate.Add(itemData);
                    item.Id = itemData.Id;
                }
                else
                {
                    item.IsAdded = true;
                    listAdd.Add(item);
                }
            }
            lstDelete = listData;
            if (listAdd.Count > 0)
            {
                _goithauChiPhiService.AddRange(listAdd);
                listChiPhiResult.AddRange(listAdd);

            }
            if (ListUpdate.Count > 0)
            {
                _goithauChiPhiService.UpdateRange(ListUpdate);
                listChiPhiResult.AddRange(ListUpdate);

            }
            if (lstDelete.Count > 0)
            {
                foreach (var item in lstDelete)
                {
                    _goithauChiPhiService.Delete(item.Id);
                }
            }

            foreach (var item in listChiPhiResult)
            {
                if (item.IIdDuToanChiPhiId.HasValue)
                    dicGoiThauChiPhi.Add(string.Format("{0}\t{1}", id, item.IIdDuToanChiPhiId), item.Id);
                else
                    dicGoiThauChiPhi.Add(string.Format("{0}\t{1}", id, item.IIdQdDauTuChiPhiId), item.Id);
            }
        }

        private void SaveHangMuc(Dictionary<string, Guid> dicGoiThauChiPhi, Guid id)
        {
            var listDataHangMuc = _goithauHangMucService.FindDataHangMucByGoiThauID(Model.Id).ToList();
            var listDelete = new List<NhDaGoiThauHangMuc>();
            var listAdd = new List<NhDaGoiThauHangMuc>();
            var ListUpdate = new List<NhDaGoiThauHangMuc>();
            var lstHangMuc = _itemsHangMucByGoiThau.Where(n => n.IsChecked).Select(n => new NhDaGoiThauHangMuc()
            {
                FTienGoiThauEur = n.FGiaTriPheDuyetEUR,
                FTienGoiThauNgoaiTeKhac = n.FGiaTriPheDuyetNgoaiTeKhac,
                FTienGoiThauUsd = n.FGiaTriPheDuyetUSD,
                FTienGoiThauVnd = n.FGiaTriPheDuyetVND,
                IIdDuToanHangMucId = SelectedDuToan != null ? n.IIdHangMucID : null,
                IIdQdDauTuHangMucId = IIdQDDauTuId.HasValue ? n.IIdHangMucID : null,
                IIdGoiThauChiPhiId = dicGoiThauChiPhi.Any() ? (dicGoiThauChiPhi.ContainsKey(string.Format("{0}\t{1}", id, n.IIdChiPhiID)) ? dicGoiThauChiPhi[string.Format("{0}\t{1}", id, n.IIdChiPhiID.Value)] : Guid.Empty) : Guid.Empty,
                IsAdded = true
            }).ToList();

            foreach (var item in lstHangMuc)
            {
                // check update or adder
                var itemUpdate = listDataHangMuc.FirstOrDefault(x => x.IIdDuToanHangMucId == item.IIdDuToanHangMucId && x.IIdGoiThauChiPhiId == item.IIdGoiThauChiPhiId);
                if (itemUpdate != null)
                {
                    itemUpdate.FTienGoiThauEur = item.FTienGoiThauEur;
                    itemUpdate.FTienGoiThauNgoaiTeKhac = item.FTienGoiThauNgoaiTeKhac;
                    itemUpdate.FTienGoiThauUsd = item.FTienGoiThauUsd;
                    itemUpdate.FTienGoiThauVnd = item.FTienGoiThauVnd;
                    ListUpdate.Add(itemUpdate);
                    listDataHangMuc.Remove(itemUpdate);
                }
                else
                {
                    item.IsAdded = true;
                    listAdd.Add(item);
                }

            }

            listDelete = listDataHangMuc;
            if (listAdd.Any())
            {
                _goithauHangMucService.AddRange(listAdd);
            }

            if (listDelete.Any())
            {
                foreach (var item in lstHangMuc)
                {
                    _goithauHangMucService.Delete(item);
                }
            }

            if (ListUpdate.Any())
            {
                _goithauHangMucService.UpdateRange(ListUpdate);
            }
        }

        private void GetDuToanDetail(Guid iIdDuToanId)
        {
            _itemsNguonVon = _mapper.Map<List<NhDaGoiThauDetailNguonVonModel>>(_dutoanService.GetNguonVonByDuToanId(iIdDuToanId));
            _itemsChiPhi = _mapper.Map<List<NhDaGoiThauDetailChiPhiModel>>(_dutoanService.GetChiPhiByDuToanId(iIdDuToanId));
            _itemsHangMuc = _mapper.Map<List<NhDaGoiThauDetailHangMucModel>>(_dutoanService.GetHangMucByDuToanId(iIdDuToanId));
        }
        private void GetQdDauTuDetail(Guid iIdQdDauTuId)
        {
            _itemsNguonVon = _mapper.Map<List<NhDaGoiThauDetailNguonVonModel>>(_qddtNguonVonService.GetNguonVonByQdDauTuId(iIdQdDauTuId));
            _itemsChiPhi = _mapper.Map<List<NhDaGoiThauDetailChiPhiModel>>(_qddtChiPhiService.GetChiPhiByQdDauTuId(iIdQdDauTuId));
            _itemsHangMuc = _mapper.Map<List<NhDaGoiThauDetailHangMucModel>>(_qddtHangMucService.GetHangMucByQdDauTuId(iIdQdDauTuId));
        }
        private bool ValidateData()
        {
            string error = "";
            if (Model.DBatDauChonNhaThau != null && Model.DKetThucChonNhaThau != null)
            {
                if (Model.DKetThucChonNhaThau <= Model.DBatDauChonNhaThau)
                {
                    error = "Ngày kết thúc phải lớn hơn ngày bắt đầu";
                }
            }
            if (IsShowDuAn && Model.IIdDuAnId == null)
            {
                error = "Chưa chọn dự án";
            }
            else if (!IsShowDuAn && Model.IID_KHTT_NhiemVuChiID != null)
            {
                error = "Chưa chọn chương trình";
            }

            if (error != "" && error != null)
            {
                MessageBoxHelper.Warning(error);
                return false;
            }
            return true;
        }
        private void GetKhlcNhaThauDetailById()
        {
            if (Model.Id.IsNullOrEmpty())
            {
                _itemsNguonVonByGoiThau = new List<NhDaGoiThauDetailNguonVonModel>();
                _itemsChiPhiByGoiThau = new List<NhDaGoiThauDetailChiPhiModel>();
                _itemsHangMucByGoiThau = new List<NhDaGoiThauDetailHangMucModel>();
            }
            else
            {
                if (Model.IIdQDDauTuID.HasValue)
                    GetQdDauTuDetail(Model.IIdQDDauTuID.Value);
                else if (Model.IIdDuToanID.HasValue)
                    GetDuToanDetail(Model.IIdDuToanID.Value);

                _itemsNguonVonByGoiThau = _mapper.Map<List<NhDaGoiThauDetailNguonVonModel>>(_goithauNguonVonService.GetGoiThauNguonVonByGoiThauId(Model.Id));
                if (_itemsNguonVonByGoiThau != null) _itemsNguonVonByGoiThau = _itemsNguonVonByGoiThau
                         .Select(n =>
                         {
                             n.FGiaTriPheDuyetEUR = n.FGiaTriEUR;
                             n.FGiaTriPheDuyetNgoaiTeKhac = n.FGiaTriNgoaiTeKhac;
                             n.FGiaTriPheDuyetUSD = n.FGiaTriUSD;
                             n.FGiaTriPheDuyetVND = n.FGiaTriVND;
                             n.IsChecked = true;
                             return n;
                         }).ToList();

                _itemsChiPhiByGoiThau = _mapper.Map<List<NhDaGoiThauDetailChiPhiModel>>(_goithauChiPhiService.GetGoiThauChiPhiByGoiThauId(Model.Id));
                if (_itemsChiPhiByGoiThau != null) _itemsChiPhiByGoiThau = _itemsChiPhiByGoiThau
                         .Select(n =>
                         {
                             n.FGiaTriPheDuyetEUR = n.FGiaTriEUR;
                             n.FGiaTriPheDuyetNgoaiTeKhac = n.FGiaTriNgoaiTeKhac;
                             n.FGiaTriPheDuyetUSD = n.FGiaTriUSD;
                             n.FGiaTriPheDuyetVND = n.FGiaTriVND;
                             n.IsChecked = true;

                             return n;
                         }).ToList();

                _itemsHangMucByGoiThau = _mapper.Map<List<NhDaGoiThauDetailHangMucModel>>(_goithauHangMucService.GetGoiThauHangMucByGoiThautId(Model.Id));
                if (!_itemsHangMucByGoiThau.IsEmpty())
                    _itemsHangMucByGoiThau.Select(x => { x.IsChecked = true; return x; }).ToList();
            }
        }

        private void OpenDetailGoiThau()
        {
            ForexDomesticBiddingPackageDetailViewModel.Model = Model;
            ForexDomesticBiddingPackageDetailViewModel.Init();
            ForexDomesticBiddingPackageDetailViewModel.ShowDialog();
            OnPropertyChanged(nameof(ItemsGoiThau));
        }
        private void ChangeData()
        {
            var lstnguonvon = GetNguonVonByGoiThau(Model.Id).Clone();
            var LstChiPhi = GetChiPhiByGoiThau(Model.Id).Clone().ToList();
            var LstHangMuc = GetHangMucByGoiThau(Model.Id).Clone();
            foreach (var item in LstHangMuc)
            {
                if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                {
                    item.FGiaTriPheDuyetUSD = item.FGiaTriPheDuyetVND / FTiGiaNhap.Value;
                }
            }
            foreach (var item in LstChiPhi)
            {
                if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                {
                    item.FGiaTriPheDuyetUSD = item.FGiaTriPheDuyetVND / FTiGiaNhap.Value;
                }
            }
            foreach (var item in lstnguonvon)
            {
                if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                {
                    item.FGiaTriPheDuyetUSD = item.FGiaTriPheDuyetVND / FTiGiaNhap.Value;
                }
            }
            Model.FGiaGoiThauUsd = 0;
            foreach (var item in LstChiPhi)
            {
                if (item.IsChecked == true)
                {
                    Model.FGiaGoiThauUsd += item.FGiaTriPheDuyetUSD;
                }
            }

            //    Model.FGiaGoiThauUsd = Model.FGiaGoiThauVnd / FTiGiaNhap;
            //}

        }
    }
}
