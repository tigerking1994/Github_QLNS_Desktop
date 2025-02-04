using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using System.IO;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNKeHoachLuaChonNhaThau.MSTNImportGoiThau;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNKeHoachLuaChonNhaThau;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.NHKeHoachLuaChonNhaThau;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNKeHoachLuaChonNhaThau
{
    public class MSTNKeHoachLuaChonNhaThauDialogViewModel : DialogAttachmentViewModelBase<NhDaKhlcNhaThauModel>
    {
        #region Private
        private readonly INhDaKhlcNhaThauService _service;
        private readonly INhDaDuToanService _dutoanService;
        private readonly INhDaQdDauTuService _qddautuService;
        private readonly INhDmLoaiHopDongService _nhdmLoaiHopDongService;
        private readonly INhDmPhuongThucChonNhaThauService _nhdmPhuongThucChonNhaThauService;
        private readonly INhDmHinhThucChonNhaThauService _nhdmHinhThucChonNhaThauService;
        private readonly IExportService _exportService;
        private readonly INhDaQdDauTuNguonVonService _qddtNguonVonService;
        private readonly INhDaQdDauTuChiPhiService _qddtChiPhiService;
        private readonly INhDaQdDauTuHangMucService _qddtHangMucService;
        private readonly INhDaGoiThauService _goithauService;
        private readonly INhDaGoiThauNguonVonService _goithauNguonVonService;
        private readonly INhDaGoiThauChiPhiService _goithauChiPhiService;
        private readonly INhDaGoiThauHangMucSerrvice _goithauHangMucService;
        private readonly INsDonViService _dvService;
        private readonly IDmChuDauTuService _cdtService;
        private readonly INhDaDuAnService _duanService;
        private readonly ISessionService _sessionService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly ILog _logger;
        private List<NhDaGoiThauDetailNguonVonModel> _itemsNguonVon;
        private List<NhDaGoiThauDetailChiPhiModel> _itemsChiPhi;
        private List<NhDaGoiThauDetailHangMucModel> _itemsHangMuc;
        private List<NhDaGoiThauDetailNguonVonModel> _itemsNguonVonByGoiThau;
        private List<NhDaGoiThauDetailChiPhiModel> _itemsChiPhiByGoiThau;
        private List<NhDaGoiThauDetailHangMucModel> _itemsHangMucByGoiThau;
        #endregion

        #region Public
        public override string Name => "THÔNG TIN KẾ HOẠCH ĐẶT HÀNG/ LỰA CHỌN NHÀ THẦU";
        public override string Title => "Kế hoạch đặt hàng/ lựa chọn nhà thầu";
        public override string Description { get; set; }
        public override Type ContentType => typeof(MSTNKeHoachLuaChonNhaThauDialog);
        public bool BIsDuToan => SelectedSoCuTrucTiep == null || SelectedSoCuTrucTiep.ValueItem == ((int)SO_CU_TRUC_TIEP.TypeValue.DU_TOAN).ToString();
        public bool BIsDieuChinh => IsAdd && Model.IIdParentID.HasValue && !IsDetail;
        public bool IsEdit => !IsAdd || BIsDieuChinh || IsDetail;
        public bool IsInsert => Model == null || Model.Id.IsNullOrEmpty();
        public int IThuocMenu { get; set; }
        public int ILoai { get; set; }
        public bool IsShowDuAn { get; set; }
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.NH_DA_KHLCNhaThau;
        #endregion

        #region Items
        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }
        private bool _isVisibleTiGiaNhap;
        public bool IsVisibleTiGiaNhap
        {
            get => _isVisibleTiGiaNhap;
            set => SetProperty(ref _isVisibleTiGiaNhap, value);
        }
        private bool _isDetail;
        public bool IsDetail
        {
            get => _isDetail;
            set => SetProperty(ref _isDetail, value);
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

        private ObservableCollection<NhDaGoiThauModel> _itemsGoiThau;
        public ObservableCollection<NhDaGoiThauModel> ItemsGoiThau
        {
            get => _itemsGoiThau;
            set => SetProperty(ref _itemsGoiThau, value);
        }

        private ObservableCollection<NhDaGoiThauModel> _itemsGoiThauGoc;
        public ObservableCollection<NhDaGoiThauModel> ItemsGoiThauGoc
        {
            get => _itemsGoiThauGoc;
            set => SetProperty(ref _itemsGoiThauGoc, value);
        }
        private ObservableCollection<NhDmHinhThucChonNhaThauModel> _itemsHinhThuc;
        public ObservableCollection<NhDmHinhThucChonNhaThauModel> ItemsHinhThuc
        {
            get => _itemsHinhThuc;
            set => SetProperty(ref _itemsHinhThuc, value);
        }
        private ObservableCollection<NhDmPhuongThucChonNhaThauModel> _itemsPhuongThuc;
        public ObservableCollection<NhDmPhuongThucChonNhaThauModel> ItemsPhuongThuc
        {
            get => _itemsPhuongThuc;
            set => SetProperty(ref _itemsPhuongThuc, value);
        }
        private NhDaGoiThauModel _selectedGoiThau;
        public NhDaGoiThauModel SelectedGoiThau
        {
            get => _selectedGoiThau;
            set => SetProperty(ref _selectedGoiThau, value);
        }

        private ObservableCollection<ComboboxItem> _itemsSoCuTrucTiep;
        public ObservableCollection<ComboboxItem> ItemsSoCuTrucTiep
        {
            get => _itemsSoCuTrucTiep;
            set => SetProperty(ref _itemsSoCuTrucTiep, value);
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
                    //LoadChuongTrinh();
                    if (!IsShowDuAn) LoadDuToan();
                }
            }
        }

        private ObservableCollection<NhDaDuAn> _itemsDuAn;
        public ObservableCollection<NhDaDuAn> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }

        private NhDaDuAn _selectedDuAn;
        public NhDaDuAn SelectedDuAn
        {
            get => _selectedDuAn;
            set
            {
                if (SetProperty(ref _selectedDuAn, value))
                {
                    SetDataBySelectedDuAn();

                    if (SelectedSoCuTrucTiep == null || SelectedSoCuTrucTiep.ValueItem == ((int)SO_CU_TRUC_TIEP.TypeValue.DU_TOAN).ToString())
                        LoadDuToan();
                    else
                        FindQDDauTuByDuAn();
                }
            }
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
                    LoadTiGiaTheoDuToan();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsHinhThucLuaChon;
        public ObservableCollection<ComboboxItem> ItemsHinhThucLuaChon
        {
            get => _itemsHinhThucLuaChon;
            set => SetProperty(ref _itemsHinhThucLuaChon, value);
        }

        private ObservableCollection<ComboboxItem> _itemsPhuongThucDauThau;
        public ObservableCollection<ComboboxItem> ItemsPhuongThucDauThau
        {
            get => _itemsPhuongThucDauThau;
            set => SetProperty(ref _itemsPhuongThucDauThau, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiHopDong;
        public ObservableCollection<ComboboxItem> ItemsLoaiHopDong
        {
            get => _itemsLoaiHopDong;
            set => SetProperty(ref _itemsLoaiHopDong, value);
        }


        private ObservableCollection<NhDmLoaiHopDongModel> _itemsLoaiHopDongExport;
        public ObservableCollection<NhDmLoaiHopDongModel> ItemsLoaiHopDongExport
        {
            get => _itemsLoaiHopDongExport;
            set => SetProperty(ref _itemsLoaiHopDongExport, value);
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
            set => SetProperty(ref _selectedChuongTrinh, value);
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
                    SetLabelTiGiaChiTiet();
                    IsVisibleTiGiaNhap = true;
                    ShowTiGiaNhap();
                }
            }
        }
        private double? _fTiGiaNhap;
        public double? FTiGiaNhap
        {
            get => _fTiGiaNhap;
            set
            {
                if (SetProperty(ref _fTiGiaNhap, value))
                {
                    CalculateByTiGiaNhap();
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

        private double? _fGiaTriVND;
        public double? FGiaTriVND
        {
            get => _fGiaTriVND;
            set => SetProperty(ref _fGiaTriVND, value);
        }

        private double? _fGiaTriUSD;
        public double? FGiaTriUSD
        {
            get => _fGiaTriUSD;
            set => SetProperty(ref _fGiaTriUSD, value);
        }

        private double? _fGiaTriEUR;
        public double? FGiaTriEUR
        {
            get => _fGiaTriEUR;
            set => SetProperty(ref _fGiaTriEUR, value);
        }

        private double? _fGiaTriNgoaiTeKhac;
        public double? FGiaTriNgoaiTeKhac
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

        private bool _isOpenExcelPopup;
        public bool IsOpenExcelPopup
        {
            get => _isOpenExcelPopup;
            set => SetProperty(ref _isOpenExcelPopup, value);
        }
        #endregion

        public MSTNKeHoachLuaChonNhaThauDetailViewModel NHKeHoachLuaChonNhaThauDetailViewModel { get; }
        public CategoryForexDialogViewModel CategoryForexDialogViewModel { get; set; }
        public MSTNImportGoiThauViewModel ImportGoiThauViewModel { get; set; }

        #region RelayCommnand
        public RelayCommand ShowGoiThauCommand { get; }
        public RelayCommand AddGoiThauCommand { get; }
        public RelayCommand DeleteGoiThauCommand { get; }
        public RelayCommand SaveDataCommand { get; set; }
        public RelayCommand OpenImportCommand { get; set; }
        public RelayCommand DowwloadTemplateCommand { get; set; }
        public RelayCommand OpenPopupExcelCommand { get; }
        #endregion

        public MSTNKeHoachLuaChonNhaThauDialogViewModel(
            MSTNKeHoachLuaChonNhaThauDetailViewModel nHKeHoachLuaChonNhaThauDetailViewModel,
            CategoryForexDialogViewModel categoryForexDialogViewModel,
            INhDaQdDauTuService qddautuService,
            INhDaQdDauTuNguonVonService qddtNguonVonService,
            INhDaQdDauTuChiPhiService qddtChiPhiService,
            IExportService exportService,
            INhDaQdDauTuHangMucService qddtHangMucService,
            INhDaGoiThauNguonVonService goithauNguonVonService,
            INhDaGoiThauChiPhiService goithauChiPhiService,
            INhDaGoiThauHangMucSerrvice goithauHangMucService,
            INhDaDuToanService dutoanService,
            INhDaKhlcNhaThauService service,
            INhDaGoiThauService goithauService,
            IDmChuDauTuService cdtService,
            INsDonViService dvService,
            INhDaDuAnService duanService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            ISessionService sessionService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            IMapper mapper,
            ILog logger,
            INhDmLoaiHopDongService nhdmLoaiHopDongService,
            INhDmHinhThucChonNhaThauService nhdmHinhThucChonNhaThauService,
            INhDmPhuongThucChonNhaThauService nhdmPhuongThucChonNhaThauService,
            MSTNImportGoiThauViewModel importGoiThauViewModel) : base(mapper, storageServiceFactory, attachService)
        {
            NHKeHoachLuaChonNhaThauDetailViewModel = nHKeHoachLuaChonNhaThauDetailViewModel;
            NHKeHoachLuaChonNhaThauDetailViewModel.ParentPage = this;
            CategoryForexDialogViewModel = categoryForexDialogViewModel;
            CategoryForexDialogViewModel.ParentPage = this;
            ImportGoiThauViewModel = importGoiThauViewModel;

            _service = service;
            _nhdmLoaiHopDongService = nhdmLoaiHopDongService;
            _nhdmHinhThucChonNhaThauService = nhdmHinhThucChonNhaThauService;
            _nhdmPhuongThucChonNhaThauService = nhdmPhuongThucChonNhaThauService;
            _exportService = exportService;
            _dutoanService = dutoanService;
            _qddautuService = qddautuService;
            _qddtNguonVonService = qddtNguonVonService;
            _qddtChiPhiService = qddtChiPhiService;
            _qddtHangMucService = qddtHangMucService;
            _goithauNguonVonService = goithauNguonVonService;
            _goithauChiPhiService = goithauChiPhiService;
            _goithauHangMucService = goithauHangMucService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
            _goithauService = goithauService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _dvService = dvService;
            _cdtService = cdtService;
            _duanService = duanService;
            _sessionService = sessionService;
            _logger = logger;

            ShowGoiThauCommand = new RelayCommand(obj => OnShowGoiThau());
            AddGoiThauCommand = new RelayCommand(obj => OnAddGoiThau(), obj => !BIsReadOnly);
            DeleteGoiThauCommand = new RelayCommand(obj => OnDeleteGoiThau(), obj => !BIsReadOnly);
            SaveDataCommand = new RelayCommand(obj => OnSaveData(obj));
            OpenImportCommand = new RelayCommand(obj => OnShowImport());
            DowwloadTemplateCommand = new RelayCommand(obj => OnDowloadTemplate());
            OpenPopupExcelCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
        }

        private void OnDowloadTemplate()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNameWithoutExtension;
                    templateFileName = Path.Combine(ExportPrefix.PATH_NH_DUAN, ExportFileName.RPT_GOITHAU);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(Path.GetFileNameWithoutExtension(templateFileName));

                    var dataLoaiHopDongs = _nhdmLoaiHopDongService.FindAll();
                    var lstLoaiHopDong = _mapper.Map<ObservableCollection<NhDmLoaiHopDongModel>>(dataLoaiHopDongs);
                    var dataPhuongThucs = _nhdmPhuongThucChonNhaThauService.FindAll();
                    var lstPhuongThuc = _mapper.Map<ObservableCollection<NhDmPhuongThucChonNhaThauModel>>(dataPhuongThucs);
                    var dataHinhThucs = _nhdmHinhThucChonNhaThauService.FindAll();
                    var lstHinhThuc = _mapper.Map<ObservableCollection<NhDmHinhThucChonNhaThauModel>>(dataHinhThucs);

                    var data = new Dictionary<string, object>();

                    data.Add("ItemsLoaiHopDong", lstLoaiHopDong);
                    data.Add("ItemsPhuongThuc", lstPhuongThuc);
                    data.Add("ItemsHinhThuc", lstHinhThuc);
                    var xlsFile = _exportService.Export<NhDmLoaiHopDongModel, NhDmPhuongThucChonNhaThauModel, NhDmHinhThucChonNhaThauModel>(templateFileName, data);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.EXCEL);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }

                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        #region Event
        public override void Init()
        {
            MarginRequirement = new Thickness(10);
            ShowDuAn();
            LoadLoaiKHLCNT();
            LoadTiGia();
            LoadTiGiaChiTiet();
            LoadLoaiHopDong();
            LoadLoaiHopDongExport();
            LoadPhuongThucLuaChonNhaThau();
            LoadHinhThucLuaChonNhaThau();
            LoadSoCuTrucTiep();
            LoadDonViQuanLy();
            LoadDuAn();
            LoadHinhThuc();
            LoadPhuongThuc();
            LoadData();
            LoadAttach();
        }

        private void LoadPhuongThuc()
        {
            var data = _nhdmPhuongThucChonNhaThauService;
            _itemsPhuongThuc = _mapper.Map<ObservableCollection<NhDmPhuongThucChonNhaThauModel>>(data);
            OnPropertyChanged(nameof(ItemsPhuongThuc));
        }
        private void CalculateByTiGiaNhap()
        {
            if (ItemsGoiThau != null && ItemsGoiThau.Count > 0)
            {
                //double? FGiaTriPheDuyetUSD = 0;
                // tính lại gói thầu
                foreach (var item in ItemsGoiThau)
                {
                    if (FTiGiaNhap != null)
                    {
                        item.FGiaGoiThauUsd = item.FGiaGoiThauVnd / FTiGiaNhap.Value;
                    }
                    //FGiaTriPheDuyetUSD += item.FGiaGoiThauUsd;
                }
                // tính lại giá trị phê duyệt
                //    FGiaTriUSD = FGiaTriPheDuyetUSD;
            }
        }
        private void LoadHinhThuc()
        {
            var data = _nhdmHinhThucChonNhaThauService;
            _itemsHinhThuc = _mapper.Map<ObservableCollection<NhDmHinhThucChonNhaThauModel>>(data);
            OnPropertyChanged(nameof(ItemsHinhThuc));
        }

        private void LoadLoaiHopDongExport()
        {
            var data = _nhdmLoaiHopDongService;
            _itemsLoaiHopDongExport = _mapper.Map<ObservableCollection<NhDmLoaiHopDongModel>>(data);
            OnPropertyChanged(nameof(ItemsLoaiHopDongExport));
        }

        public void ShowDuAn()
        {
            if (ILoai == 2 && IThuocMenu == 4 || ILoai == 3 && IThuocMenu == 3)
            {
                IsShowDuAn = true;
            }
        }
        public override void LoadData(params object[] args)
        {
            if (Model.Id == Guid.Empty)
            {
                SelectedSoCuTrucTiep = ItemsSoCuTrucTiep.FirstOrDefault();
                SelectedDonVi = null;
                SelectedDuAn = null;
                SSoQuyetDinh = string.Empty;
                FGiaTriUSD = null;
                FGiaTriVND = null;
                //FGiaTriEUR = null;
                //FGiaTriNgoaiTeKhac = null;
                DNgayQuyetDinh = DateTime.Now;
                SMoTa = string.Empty;
                FTiGiaNhap = null;
            }
            else
            {
                if (Model.IIdQDDauTuID.HasValue)
                    SelectedSoCuTrucTiep = ItemsSoCuTrucTiep.FirstOrDefault(n => n.ValueItem == ((int)SO_CU_TRUC_TIEP.TypeValue.QD_DAU_TU).ToString());
                else if (Model.IIdDuToanID.HasValue)
                    SelectedSoCuTrucTiep = ItemsSoCuTrucTiep.FirstOrDefault(n => n.ValueItem == ((int)SO_CU_TRUC_TIEP.TypeValue.DU_TOAN).ToString());
                if (Model.ILoaiKHLCNT != null)
                {
                    SelectedLoaiKHLCNT = ItemsLoaiKHLCNT.FirstOrDefault(n => n.ValueItem == Model.ILoaiKHLCNT.ToString());
                }
                SelectedDonVi = ItemsDonVi.FirstOrDefault(n => n.IIDMaDonVi == Model.IIdMaDonViQuanLy);
                if (SelectedDonVi != null)
                {
                    if (!IsShowDuAn) _selectedChuongTrinh = ItemsChuongTrinh.FirstOrDefault(x => x.IIdKHTTNhiemVuChiId == Model.IIdKHTTNhiemVuChiId);
                }
                if (Model.IIdDuToanID.HasValue)
                    SelectedDuToan = ItemsDuToan.FirstOrDefault(n => n.Id == Model.IIdDuToanID);
                SelectedDuAn = ItemsDuAn.FirstOrDefault(n => n.Id == Model.IIdDuAnID);
                SSoQuyetDinh = Model.SSoQuyetDinh;
                DNgayQuyetDinh = Model.DNgayQuyetDinh;
                SMoTa = Model.SMoTa;
                FTiGiaNhap = Model.FTiGiaNhap;
            }
            GetGridGoiThau();
            GetKhlcNhaThauDetailById();
            foreach(var item in ItemsGoiThau)
            {
                GetHangMucByGoiThau(item.Id);
                GetChiPhiByGoiThau(item.Id);
            }
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            OnPropertyChanged(nameof(SelectedSoCuTrucTiep));
            OnPropertyChanged(nameof(SelectedDuToan));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SSoQuyetDinh));
            OnPropertyChanged(nameof(DNgayQuyetDinh));
            OnPropertyChanged(nameof(SMoTa));
        }

        private void OnAddGoiThau()
        {
            NhDaGoiThauModel objGoiThau = new NhDaGoiThauModel();
            objGoiThau.Id = Guid.NewGuid();
            objGoiThau.IsAdded = true;
            objGoiThau.ILoai = 2; // các gói thầu trong màn KHLCNT là gói thầu trong nước
            objGoiThau.IThuocMenu = IThuocMenu;
            ItemsGoiThau.Add(objGoiThau);
            OnPropertyChanged(nameof(ItemsGoiThau));
        }

        private void OnDeleteGoiThau()
        {
            if (SelectedGoiThau == null) return;
            SelectedGoiThau.IsDeleted = !SelectedGoiThau.IsDeleted;
            OnPropertyChanged(nameof(ItemsGoiThau));
        }

        private void OnShowGoiThau()
        {
            if (SelectedGoiThau == null) return;
            if (SelectedDuToan == null && !IIdQDDauTuId.HasValue)
            {
                MessageBoxHelper.Error(Resources.MsgErrorChungTuEmpty);
                return;
            }
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

            NHKeHoachLuaChonNhaThauDetailViewModel.IsDetail = IsDetail;
            NHKeHoachLuaChonNhaThauDetailViewModel.ILoaiDuToan = SelectedDuToan.IdLoaiDuToan;
            NHKeHoachLuaChonNhaThauDetailViewModel.SLoaiSoCu = SelectedSoCuTrucTiep.ValueItem;
            //NHKeHoachLuaChonNhaThauDetailViewModel.LstTiGiaChiTiet = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
            if (SelectedTiGia != null)
            {
                NHKeHoachLuaChonNhaThauDetailViewModel.SMaTienTeGoc = SelectedTiGia.SMaTienTeGoc;
            }
            NHKeHoachLuaChonNhaThauDetailViewModel.FTiGiaNhap = FTiGiaNhap;
            NHKeHoachLuaChonNhaThauDetailViewModel.SMaTienTeQuyDoi = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
            NHKeHoachLuaChonNhaThauDetailViewModel.ItemNguonVon = GetNguonVonByGoiThau(SelectedGoiThau.Id).Clone();
            NHKeHoachLuaChonNhaThauDetailViewModel.LstChiPhi = GetChiPhiByGoiThau(SelectedGoiThau.Id).Clone();
            NHKeHoachLuaChonNhaThauDetailViewModel.LstHangMuc = GetHangMucByGoiThau(SelectedGoiThau.Id).Clone();
            NHKeHoachLuaChonNhaThauDetailViewModel.Model = SelectedGoiThau;
            NHKeHoachLuaChonNhaThauDetailViewModel.Init();
            NHKeHoachLuaChonNhaThauDetailViewModel.SavedAction = obj => SaveGoiThauDetail();
            var view = new MSTNKeHoachLuaChonNhaThauDetail { DataContext = NHKeHoachLuaChonNhaThauDetailViewModel };
            view.ShowDialog();
            OnPropertyChanged(nameof(ItemsGoiThau));
        }

        private void OnSaveData(object obj)
        {
            if (!ValidateForm()) return;
            if (!ValidateGoiThau()) return;
            var objKhlcNhaThau = SaveKhlcNhaThauData();
            if (!IsDieuChinh)
            {
                SaveGoiThau(objKhlcNhaThau.Id);
                SaveDetailGoiThau();
            }
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            Window window = obj as Window;
            SavedAction?.Invoke(Model);
            window.Close();
        }
        #endregion

        #region Helper
        private bool ValidateForm()
        {
            List<string> lstError = new List<string>();

            if (SelectedSoCuTrucTiep == null && IsShowDuAn)
            {
                lstError.Add(string.Format(Resources.MsgCheckSoCuTrucTiep));
            }
            else if (SelectedDuAn != null && IsShowDuAn)
            {
                if (SelectedSoCuTrucTiep.ValueItem == ((int)SO_CU_TRUC_TIEP.TypeValue.QD_DAU_TU).ToString() && !IIdQDDauTuId.HasValue)
                {
                    lstError.Add(string.Format(Resources.MsgErrorItemNotFound, string.Format("quyết định đầu tư của dự án {0}", SelectedDuAn.STenDuAn)));
                }
                else if (SelectedSoCuTrucTiep.ValueItem == ((int)SO_CU_TRUC_TIEP.TypeValue.DU_TOAN).ToString() && SelectedDuToan == null)
                {
                    lstError.Add(string.Format(Resources.MsgErrorRequire, SO_CU_TRUC_TIEP.TypeName.DU_TOAN.ToLower()));
                }
            }

            //if (SelectedTiGia == null)
            //    lstError.Add(string.Format(Resources.MsgCheckTiGiaNgoaiHoi));
            if (SelectedDonVi == null)
                lstError.Add(string.Format(Resources.MsgCheckDonVi));
            if (SelectedDuAn == null && IsShowDuAn)
                lstError.Add(string.Format(Resources.MsgCheckDuAn));
            if (string.IsNullOrEmpty(SSoQuyetDinh))
                lstError.Add(string.Format(Resources.MsgCheckSoQD));
            if (!DNgayQuyetDinh.HasValue)
                lstError.Add(string.Format(Resources.ErrorNgayQuyetDinhEmpty));
            if (SelectedChuongTrinh == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckChuongTrinh));
            }

            //if (SelectedTiGiaChiTiet == null)
            //{
            //    lstError.Add(string.Format(Resources.MsgCheckMaNgoaiTeNgoaiHoi));
            //}

            if (lstError.Count != 0)
            {
                MessageBoxHelper.Error(string.Join("\n", lstError));
                return false;
            }
            return true;
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

        private void LoadTiGiaTheoDuToan()
        {
            if (SelectedDuToan != null && ItemsTiGia.Any())
            {
                SelectedTiGia = ItemsTiGia.FirstOrDefault(x => x.Id.Equals(SelectedDuToan.IIdTiGiaId));
                FTiGiaNhap = SelectedDuToan.FTiGiaNhap;
            }
        }
        private void GetGridGoiThau()
        {
            if (Model.Id == Guid.Empty)
                ItemsGoiThau = new ObservableCollection<NhDaGoiThauModel>();
            else
                ItemsGoiThau = _mapper.Map<ObservableCollection<NhDaGoiThauModel>>(_goithauService.FindByIidKhlcNhaThau(Model.Id));
            OnPropertyChanged(nameof(ItemsGoiThau));
        }

        private void SaveDetailGoiThau()
        {
            Dictionary<string, Guid> dicGoiThauNV = new Dictionary<string, Guid>();
            SaveNguonVon(ref dicGoiThauNV);
            Dictionary<string, Guid> dicGoiThauChiPhi = new Dictionary<string, Guid>();
            SaveChiPhi(ref dicGoiThauChiPhi, dicGoiThauNV);
            SaveHangMuc(dicGoiThauChiPhi);
        }

        private void SaveNguonVon(ref Dictionary<string, Guid> dicGoiThauNV)
        {
            List<NhDaGoiThauNguonVon> listData = _goithauNguonVonService.FindByListNguonVonKhlcntId(Model.Id).ToList();
            var lstDelete = new List<NhDaGoiThauNguonVon>();
            var listAdd = new List<NhDaGoiThauNguonVon>();
            var ListUpdate = new List<NhDaGoiThauNguonVon>();
            var listNguonVonResult = new List<NhDaGoiThauNguonVon>();
            var lstNguonVon = _itemsNguonVonByGoiThau.Where(n => n.IsChecked).Select(n => new NhDaGoiThauNguonVon()
            {
                FTienGoiThauEur = n.FGiaTriPheDuyetEUR,
                FTienGoiThauNgoaiTeKhac = n.FGiaTriPheDuyetNgoaiTeKhac,
                FTienGoiThauUsd = n.FGiaTriPheDuyetUSD,
                FTienGoiThauVnd = n.FGiaTriPheDuyetVND,
                IIdGoiThauId = n.IIdGoiThauID,
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
                listNguonVonResult.AddRange(listAdd);
            }
            if (ListUpdate.Count > 0)
            {
                _goithauNguonVonService.UpdateRange(ListUpdate);
                listNguonVonResult.AddRange(ListUpdate);
            }
            if (lstDelete.Count > 0)
            {
                foreach (var item in lstDelete)
                {
                    _goithauNguonVonService.DeleteNguonVon(item.Id);
                }
            }
            if (!listNguonVonResult.IsEmpty())
            {
                foreach (var item in listNguonVonResult)
                {
                    if (_itemsNguonVonByGoiThau.Any(f => f.IIdNguonVonID == item.IIdNguonVonId && f.IIdGoiThauID.Equals(item.IIdGoiThauId)))
                    {
                        var nvGoithau = _itemsNguonVonByGoiThau.FirstOrDefault(f => f.IIdNguonVonID == item.IIdNguonVonId && f.IIdGoiThauID.Equals(item.IIdGoiThauId));
                        dicGoiThauNV.Add($"{nvGoithau.Id}-{nvGoithau.IIdGoiThauID}", item.Id);
                    }
                    else
                    {
                        if (!dicGoiThauNV.ContainsKey($"{Guid.Empty}-{item.IIdGoiThauId}"))
                        {
                            dicGoiThauNV.Add($"{Guid.Empty}-{item.IIdGoiThauId}", item.Id);
                        }
                    }
                }
            }
        }

        private void SaveChiPhi(ref Dictionary<string, Guid> dicGoiThauChiPhi, Dictionary<string, Guid> dicGoiThauNV)
        {
            List<NhDaGoiThauChiPhi> listData = _goithauChiPhiService.FindListChiPhiByKhlcnt(Model.Id).ToList();
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
                IIdGoiThauId = n.IIdGoiThauID,
                IIdGoiThauNguonVonId = n.IIdNguonVonId.IsNullOrEmpty() ? Guid.Empty : dicGoiThauNV.Any() ? (dicGoiThauNV.ContainsKey($"{(Guid)n.IIdNguonVonId}-{n.IIdGoiThauID}") ? dicGoiThauNV[$"{(Guid)n.IIdNguonVonId}-{n.IIdGoiThauID}"] : Guid.Empty) : Guid.Empty,
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
                    dicGoiThauChiPhi.Add(string.Format("{0}\t{1}", item.IIdGoiThauId, item.IIdDuToanChiPhiId), item.Id);
                else
                    dicGoiThauChiPhi.Add(string.Format("{0}\t{1}", item.IIdGoiThauId, item.IIdQdDauTuChiPhiId), item.Id);
            }
        }

        private void SaveHangMuc(Dictionary<string, Guid> dicGoiThauChiPhi)
        {
            var lstHangMuc = _itemsHangMucByGoiThau.Where(n => n.IsChecked).Select(n => new NhDaGoiThauHangMuc()
            {
                //FTienGoiThauEur = n.FGiaTriPheDuyetEUR,
                //FTienGoiThauNgoaiTeKhac = n.FGiaTriPheDuyetNgoaiTeKhac,
                IIdParentId = n.IIdParentID,
                FTienGoiThauUsd = n.FGiaTriPheDuyetUSD,
                FTienGoiThauVnd = n.FGiaTriPheDuyetVND,
                IIdDuToanHangMucId = SelectedDuToan != null ? n.IIdHangMucID : null,
                IIdQdDauTuHangMucId = IIdQDDauTuId.HasValue ? n.IIdHangMucID : null,
                IIdGoiThauChiPhiId = dicGoiThauChiPhi.Any() ? (dicGoiThauChiPhi.ContainsKey(string.Format("{0}\t{1}", n.IIdGoiThauID, n.IIdChiPhiID)) ? dicGoiThauChiPhi[string.Format("{0}\t{1}", n.IIdGoiThauID, n.IIdChiPhiID.Value)] : Guid.Empty) : Guid.Empty,
                IsAdded = true
            }).ToList();
            _goithauHangMucService.AddRange(lstHangMuc);
        }

        private void SaveGoiThauDetail()
        {
            var objGoiThau = ItemsGoiThau.FirstOrDefault(n => n.Id == NHKeHoachLuaChonNhaThauDetailViewModel.Model.Id);
            if (objGoiThau == null) return;
            _itemsNguonVonByGoiThau = _itemsNguonVonByGoiThau.Where(n => n.IIdGoiThauID != objGoiThau.Id).ToList();
            _itemsNguonVonByGoiThau.AddRange(NHKeHoachLuaChonNhaThauDetailViewModel.ItemNguonVon.Where(n => n.IsChecked));

            _itemsChiPhiByGoiThau = _itemsChiPhiByGoiThau.Where(n => n.IIdGoiThauID != objGoiThau.Id).ToList();
            var lstItemChiPhiInsert = NHKeHoachLuaChonNhaThauDetailViewModel.ItemChiPhi.Where(n => n.IsChecked);
            _itemsChiPhiByGoiThau.AddRange(lstItemChiPhiInsert);

            _itemsHangMucByGoiThau = _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauID != objGoiThau.Id).ToList();
            _itemsHangMucByGoiThau.AddRange(NHKeHoachLuaChonNhaThauDetailViewModel.LstHangMuc.Where(n => n.IsChecked && lstItemChiPhiInsert.Select(x => x.IIdChiPhiID).Contains(n.IIdChiPhiID)));

            objGoiThau.FGiaGoiThauUsd = NHKeHoachLuaChonNhaThauDetailViewModel.ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriPheDuyetUSD);
            objGoiThau.FGiaGoiThauVnd = NHKeHoachLuaChonNhaThauDetailViewModel.ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriPheDuyetVND);
            objGoiThau.FGiaGoiThauEur = NHKeHoachLuaChonNhaThauDetailViewModel.ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriPheDuyetEUR);
            objGoiThau.FGiaGoiThauNgoaiTeKhac = NHKeHoachLuaChonNhaThauDetailViewModel.ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriPheDuyetNgoaiTeKhac);
            //NHKeHoachLuaChonNhaThauDetailViewModel.ItemChiPhi.Clear();
            OnPropertyChanged(nameof(ItemsGoiThau));
        }

        private void LoadDonViQuanLy()
        {
            try
            {
                var data = _dvService.FindByNamLamViec(_sessionService.Current.YearOfWork);
                _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
                OnPropertyChanged(nameof(ItemsDonVi));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadChuongTrinh()
        {
            ItemsChuongTrinh = new ObservableCollection<NhDmNhiemVuChiModel>();
            if (SelectedDuToan != null)
            {
                var data = _nhDmNhiemVuChiService.FindNhiemVuChiDuToanByDuToanId(SelectedDuToan.Id);
                _itemsChuongTrinh = _mapper.Map<ObservableCollection<NhDmNhiemVuChiModel>>(data);

            }
            else if (Model.IIdDuToanID != null)
            {
                var data = _nhDmNhiemVuChiService.FindNhiemVuChiDuToanByDuToanId(Model.IIdDuToanID.Value);
                _itemsChuongTrinh = _mapper.Map<ObservableCollection<NhDmNhiemVuChiModel>>(data);
            }
            
            OnPropertyChanged(nameof(ItemsChuongTrinh));
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
                var lstData = _service.GetDuAnHaveQDDauTu(Model.Id, SelectedDonVi.IIDMaDonVi).Where(s => s.ILoai == 1); 
                if (lstData == null) return;
                ItemsDuAn = new ObservableCollection<NhDaDuAn>(lstData);
                if (Model.IIdDuAnID.HasValue)
                    SelectedDuAn = ItemsDuAn.FirstOrDefault(n => n.Id == Model.IIdDuAnID.Value);
                OnPropertyChanged(nameof(ItemsDuAn));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadSoCuTrucTiep()
        {
            // SelectedDuToan = null;
            IIdQDDauTuId = null;
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem() { ValueItem = ((int)SO_CU_TRUC_TIEP.TypeValue.DU_TOAN).ToString(), DisplayItem = SO_CU_TRUC_TIEP.TypeName.DU_TOAN });
            lstData.Add(new ComboboxItem() { ValueItem = ((int)SO_CU_TRUC_TIEP.TypeValue.QD_DAU_TU).ToString(), DisplayItem = SO_CU_TRUC_TIEP.TypeName.QD_DAU_TU });
            ItemsSoCuTrucTiep = new ObservableCollection<ComboboxItem>(lstData);
            OnPropertyChanged(nameof(ItemsSoCuTrucTiep));
        }

        private void SetDataBySelectedDuAn()
        {
            if (SelectedDuAn != null && SelectedDuAn.IIdChuDauTuId.HasValue)
            {
                STenChuDauTu = _cdtService.FindById(SelectedDuAn.IIdChuDauTuId.Value).STenDonVi;
                SDiaDiemThucHien = SelectedDuAn.SDiaDiem;
            }
            else
            {
                STenChuDauTu = string.Empty;
                SDiaDiemThucHien = string.Empty;
            }
            OnPropertyChanged(nameof(STenChuDauTu));
            OnPropertyChanged(nameof(SDiaDiemThucHien));
        }

        private void LoadDuToan()
        {
            ItemsDuToan = new ObservableCollection<NhDaDuToan>();
            if ((SelectedSoCuTrucTiep == null
                || SelectedSoCuTrucTiep.ValueItem != ((int)SO_CU_TRUC_TIEP.TypeValue.DU_TOAN).ToString()
                || SelectedDuAn == null) && IsShowDuAn)
                return;
            var data = new List<NhDaDuToan>();
            if (IsShowDuAn)
            {
                data = _dutoanService.FindDuAnInKhlcNhaThau(Model.Id, SelectedDuAn.Id).ToList();
            }
            else
            {
                if (SelectedDonVi == null) return;
                data = _dutoanService.FindDuToanMoTaMSByIdDonVi(SelectedDonVi.IIDMaDonVi, ILoai).ToList();
            }
            if (data != null) ItemsDuToan = new ObservableCollection<NhDaDuToan>(data);

            if (Model.IIdDuToanID.HasValue)
                SelectedDuToan = ItemsDuToan.FirstOrDefault(n => n.Id == Model.IIdDuToanID);
            OnPropertyChanged(nameof(ItemsDuToan));
        }

        private void LoadPhuongThucLuaChonNhaThau()
        {
            var lstData = _service.GetPhuongThucLuaChonNhaThau();
            if (lstData == null)
                ItemsPhuongThucDauThau = new ObservableCollection<ComboboxItem>();
            else
                ItemsPhuongThucDauThau = new ObservableCollection<ComboboxItem>(lstData.Select(n => new ComboboxItem()
                {
                    ValueItem = n.Id.ToString(),
                    DisplayItem = n.STenPhuongThucChonNhaThau
                }));
            OnPropertyChanged(nameof(ItemsPhuongThucDauThau));
        }

        private void LoadHinhThucLuaChonNhaThau()
        {
            var lstData = _service.GetHinhThucLuaChonNhaThau();
            if (lstData == null)
                ItemsHinhThucLuaChon = new ObservableCollection<ComboboxItem>();
            else
                ItemsHinhThucLuaChon = new ObservableCollection<ComboboxItem>(lstData.Select(n => new ComboboxItem()
                {
                    ValueItem = n.Id.ToString(),
                    DisplayItem = n.STenHinhThucChonNhaThau
                }));
            OnPropertyChanged(nameof(ItemsHinhThucLuaChon));
        }

        private void LoadLoaiHopDong()
        {
            var lstData = _service.GetLoaiHopDong();
            if (lstData == null)
                ItemsLoaiHopDong = new ObservableCollection<ComboboxItem>();
            else
                ItemsLoaiHopDong = new ObservableCollection<ComboboxItem>(lstData.Select(n => new ComboboxItem()
                {
                    ValueItem = n.IIdLoaiHopDongId.ToString(),
                    DisplayItem = n.STenLoaiHopDong
                }));
            OnPropertyChanged(nameof(ItemsLoaiHopDong));
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

        private void GetQdDauTuDetail(Guid iIdQdDauTuId)
        {
            _itemsNguonVon = _mapper.Map<List<NhDaGoiThauDetailNguonVonModel>>(_qddtNguonVonService.GetNguonVonByQdDauTuId(iIdQdDauTuId));
            _itemsChiPhi = _mapper.Map<List<NhDaGoiThauDetailChiPhiModel>>(_qddtChiPhiService.GetChiPhiByQdDauTuId(iIdQdDauTuId));
            _itemsHangMuc = _mapper.Map<List<NhDaGoiThauDetailHangMucModel>>(_qddtHangMucService.GetHangMucByQdDauTuId(iIdQdDauTuId));
        }

        private void GetDuToanDetail(Guid iIdDuToanId)
        {
            _itemsNguonVon = _mapper.Map<List<NhDaGoiThauDetailNguonVonModel>>(_dutoanService.GetNguonVonByDuToanId(iIdDuToanId));
            _itemsChiPhi = _mapper.Map<List<NhDaGoiThauDetailChiPhiModel>>(_dutoanService.GetChiPhiByDuToanId(iIdDuToanId));
            _itemsHangMuc = _mapper.Map<List<NhDaGoiThauDetailHangMucModel>>(_dutoanService.GetHangMucByDuToanId(iIdDuToanId));
        }

        private ObservableCollection<NhDaGoiThauDetailNguonVonModel> GetNguonVonByGoiThau(Guid iIDGoiThauId)
        {
            Dictionary<string, NhDaGoiThauDetailNguonVonModel> dicNguonVon = new Dictionary<string, NhDaGoiThauDetailNguonVonModel>();
            if (_itemsNguonVonByGoiThau == null) _itemsNguonVonByGoiThau = new List<NhDaGoiThauDetailNguonVonModel>();

            if (_itemsNguonVonByGoiThau.Any(n => n.IIdGoiThauID == iIDGoiThauId))
                //dicNguonVon = _itemsNguonVonByGoiThau
                //    .Where(n => n.IIdGoiThauID == iIDGoiThauId
                //     && (n.FGiaTriPheDuyetNgoaiTeKhac != 0 || n.FGiaTriPheDuyetUSD != 0 || n.FGiaTriPheDuyetVND != 0 || n.FGiaTriPheDuyetEUR != 0))
                //    .ToDictionary(n => n.IIdNguonVonID, n => n);
                dicNguonVon = _itemsNguonVonByGoiThau.Where(x => x.IIdGoiThauID == iIDGoiThauId && x.IsChecked).ToDictionary(d => $"{d.IIdNguonVonID}-{d.IIdGoiThauID}", d => d);
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

                if (dicNguonVon.ContainsKey($"{item.IIdNguonVonID}-{obj.IIdGoiThauID}"))
                {
                    obj.IsChecked = true;
                    obj.FGiaTriPheDuyetVND = dicNguonVon[$"{item.IIdNguonVonID}-{obj.IIdGoiThauID}"].FGiaTriPheDuyetVND;
                    obj.FGiaTriPheDuyetUSD = dicNguonVon[$"{item.IIdNguonVonID}-{obj.IIdGoiThauID}"].FGiaTriPheDuyetUSD;
                    obj.FGiaTriPheDuyetEUR = dicNguonVon[$"{item.IIdNguonVonID}-{obj.IIdGoiThauID}"].FGiaTriPheDuyetEUR;
                    obj.FGiaTriPheDuyetNgoaiTeKhac = dicNguonVon[$"{item.IIdNguonVonID}-{obj.IIdGoiThauID}"].FGiaTriPheDuyetNgoaiTeKhac;
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
            Dictionary<Guid?, NhDaGoiThauDetailChiPhiModel> dicChiPhi = new Dictionary<Guid?, NhDaGoiThauDetailChiPhiModel>();
            if (_itemsChiPhiByGoiThau == null) _itemsChiPhiByGoiThau = new List<NhDaGoiThauDetailChiPhiModel>();
            if (_itemsChiPhiByGoiThau != null && _itemsChiPhiByGoiThau.Any(n => n.IIdGoiThauID == iIDGoiThauId))
            {
                //_itemsChiPhiByGoiThau = _itemsChiPhiByGoiThau
                //    .Where(n => n.IIdGoiThauID == iIDGoiThauId
                //    && (n.FGiaTriPheDuyetNgoaiTeKhac != 0 || n.FGiaTriPheDuyetUSD != 0 || n.FGiaTriPheDuyetVND != 0 || n.FGiaTriPheDuyetEUR != 0)).ToList();
                //dicChiPhi = _itemsChiPhiByGoiThau.ToDictionary(n => n.IIdChiPhiID, n => n);
                dicChiPhi = _itemsChiPhiByGoiThau.Where(x => x.IIdGoiThauID == iIDGoiThauId && x.IsChecked).ToDictionary(d => d.IIdChiPhiID, d => d);

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

                obj.FGiaTriPheDuyetUSD = _itemsChiPhiByGoiThau.IsEmpty() ? item.FGiaTriUSD : (_itemsChiPhiByGoiThau.Any(a => a.IIdGoiThauID.Equals(obj.IIdGoiThauID) && a.IIdChiPhiID.Equals(obj.IIdChiPhiID)) ? _itemsChiPhiByGoiThau.FirstOrDefault(a => a.IIdGoiThauID.Equals(obj.IIdGoiThauID) && a.IIdChiPhiID.Equals(obj.IIdChiPhiID)).FGiaTriPheDuyetUSD : item.FGiaTriUSD);
                obj.FGiaTriPheDuyetVND = _itemsChiPhiByGoiThau.IsEmpty() ? item.FGiaTriVND : (_itemsChiPhiByGoiThau.Any(a => a.IIdGoiThauID.Equals(obj.IIdGoiThauID) && a.IIdChiPhiID.Equals(obj.IIdChiPhiID)) ? _itemsChiPhiByGoiThau.FirstOrDefault(a => a.IIdGoiThauID.Equals(obj.IIdGoiThauID) && a.IIdChiPhiID.Equals(obj.IIdChiPhiID)).FGiaTriPheDuyetVND : item.FGiaTriVND);
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
            Dictionary<Guid?, NhDaGoiThauDetailHangMucModel> dicHangMuc = new Dictionary<Guid?, NhDaGoiThauDetailHangMucModel>();
            if (_itemsHangMucByGoiThau == null) _itemsHangMucByGoiThau = new List<NhDaGoiThauDetailHangMucModel>();

            if (_itemsHangMucByGoiThau.Any(n => n.IIdGoiThauID == iIDGoiThauId))
                //dicHangMuc = _itemsHangMucByGoiThau
                //    .Where(n => n.IIdGoiThauID == iIDGoiThauId
                //     && (n.FGiaTriPheDuyetNgoaiTeKhac != 0 || n.FGiaTriPheDuyetUSD != 0 || n.FGiaTriPheDuyetVND != 0 || n.FGiaTriPheDuyetEUR != 0))
                //    .ToDictionary(n => n.IIdHangMucID, n => n);
                dicHangMuc = _itemsHangMucByGoiThau.Where(x => x.IIdGoiThauID == iIDGoiThauId && x.IsChecked).ToDictionary(d => d.IIdHangMucID, d => d);


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

            _itemsHangMucByGoiThau = _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauID != iIDGoiThauId).ToList();
            _itemsHangMucByGoiThau.AddRange(lstData);
            return lstData;
        }
        NhDaKhlcnhaThau objGoiThau = new NhDaKhlcnhaThau();

        private NhDaKhlcnhaThau SaveKhlcNhaThauData()
        {
            NhDaKhlcnhaThau objNhaThau = new NhDaKhlcnhaThau();
            // Nếu có id thì là sửa hoặc điều chỉnh
            if (Model.Id != Guid.Empty)
            {
                if (IsDieuChinh)
                {
                    //Nếu là điều chỉnh thì update bản ghi gốc
                    var objOld = _service.Find(Model.Id);
                    objOld.BIsActive = false;
                    _service.Update(objOld);

                    // sau đó tạo mới 1 obj và cập nhật thêm một số trường trường active, lần điều chỉnh, gốc, parent
                    objNhaThau.DNgayTao = DateTime.Now;
                    objNhaThau.SNguoiTao = _sessionService.Current.Principal;
                    objNhaThau.BIsGoc = false;
                    objNhaThau.IIdLcnhaThauGocId = objOld.IIdLcnhaThauGocId;
                    objNhaThau.ILanDieuChinh = objOld.ILanDieuChinh + 1;
                    objNhaThau.IIdParentId = objOld.Id;
                }
                else
                {
                    // Nếu là cập nhật thì cập nhật ngày sửa
                    objNhaThau = _service.Find(Model.Id);
                    objNhaThau.DNgaySua = DateTime.Now;
                    objNhaThau.SNguoiSua = _sessionService.Current.Principal;
                }
            }
            else
            {
                // Nếu ko có id thì là thêm mới
                objNhaThau.DNgayTao = DateTime.Now;
                objNhaThau.SNguoiTao = _sessionService.Current.Principal;
                objNhaThau.IIdParentId = null;
                objNhaThau.BIsGoc = true;
                objNhaThau.IIdLcnhaThauGocId = null;
                objNhaThau.ILanDieuChinh = 0;
            }

            // Lấy giá trị đơn vị
            if (SelectedDonVi != null)
            {
                objNhaThau.IIdDonViQuanLyId = SelectedDonVi.Id;
                objNhaThau.IIdMaDonViQuanLy = SelectedDonVi.IIDMaDonVi;
            }

            // Lấy giá trị dự toán
            if (SelectedDuToan != null)
                objNhaThau.IIdDuToanId = SelectedDuToan.Id;

            // Lấy giá trị đầu tư
            if (IIdQDDauTuId.HasValue)
                objNhaThau.IIdQdDauTuId = IIdQDDauTuId.Value;

            // Lấy giá trị tỉ giá
            if (SelectedTiGia != null)
                objNhaThau.IIdTiGiaId = SelectedTiGia.Id;

            // Lấy giá trị ...
            if (SelectedSoCuTrucTiep != null)
            {
                objNhaThau.ILoai = int.Parse(SelectedSoCuTrucTiep.ValueItem);
            }

            // Lấy giá trị chương trình
            if (SelectedChuongTrinh != null)
                objNhaThau.IIdKHTTNhiemVuChiId = SelectedChuongTrinh.IIdKHTTNhiemVuChiId;

            // Lấy giá trị dự án
            if (SelectedDuAn != null) objNhaThau.IIdDuAnId = SelectedDuAn.Id;

            // Lấy giá trị mã ngoại tệ
            //objNhaThau.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;

            // Lấy giá trị loại KHLCNT, số quyết định, ngày, thuộc menu, mô tả
            if (SelectedLoaiKHLCNT != null)
                objNhaThau.ILoaiKHLCNT = int.Parse(SelectedLoaiKHLCNT.ValueItem);
            objNhaThau.SSoQuyetDinh = SSoQuyetDinh;
            objNhaThau.DNgayQuyetDinh = DNgayQuyetDinh;
            objNhaThau.IThuocMenu = IThuocMenu;
            objNhaThau.SMoTa = SMoTa;
            objNhaThau.FTiGiaNhap = FTiGiaNhap;
            objNhaThau.BIsActive = true;

            // Nếu obj vừa khởi tạo đã có Id thì là update, không có id thì là thêm mới hoặc điều chỉnh
            if (objNhaThau.Id.IsNullOrEmpty())
            {
                _service.Insert(objNhaThau);
                // Nếu không có id thì llà điều chỉnh hoặc thêm mới => insert

                if (IsDieuChinh)
                {
                    // Insert list gói thầu điều chỉnh
                    List<NhDaGoiThau> lstGoiThauDieuChinh = new List<NhDaGoiThau>();
                    foreach (var item in ItemsGoiThau.Where(x => !x.IsDeleted))
                    {
                        var gt = SetDataGoiThau(objNhaThau, objNhaThau.Id, item, true);
                        gt.Id = new Guid();
                        lstGoiThauDieuChinh.Add(gt);
                    }
                    if (lstGoiThauDieuChinh.Count != 0)
                        _goithauService.AddRange(lstGoiThauDieuChinh);
                }
            }
            else
            {
                // Nếu có id thì là update
                _service.Update(objNhaThau);
            }

            // Lưu attachment
            objGoiThau = objNhaThau;
            SaveAttachment(objNhaThau.Id);
            return objNhaThau;
        }

        private bool SaveGoiThau(Guid iIdKhlcNhaThauId)
        {
            List<NhDaGoiThau> lstGoiThauAdd = new List<NhDaGoiThau>();
            List<NhDaGoiThau> lstGoiThauUpdate = new List<NhDaGoiThau>();
            List<NhDaGoiThau> lstGoiThauDelete = new List<NhDaGoiThau>();
            List<Guid> lstIdDeleteDetail = ItemsGoiThau.Where(n => n.IsDeleted).Select(n => n.Id).ToList();
            foreach (var item in ItemsGoiThau)
            {
                if (item.IsAdded && !item.IsDeleted)
                    lstGoiThauAdd.Add(SetDataGoiThau(objGoiThau, iIdKhlcNhaThauId, item));
                else if (!item.IsAdded && !item.IsDeleted)
                    lstGoiThauUpdate.Add(SetDataGoiThau(objGoiThau, iIdKhlcNhaThauId, item, true));
                else if (!item.IsAdded && item.IsDeleted)
                    lstGoiThauDelete.Add(SetDataGoiThau(objGoiThau, iIdKhlcNhaThauId, item));
            }
            foreach (var item in lstGoiThauAdd)
            {
                item.ILoai = 2;
                if (SelectedTiGia != null)
                {
                    item.IIdTiGiaId = SelectedTiGia.Id;
                }
                //item.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
                item.IIdDuToanId = SelectedDuToan.Id;
                item.IThuocMenu = IThuocMenu;
                item.FTiGiaNhap = FTiGiaNhap;
                //item.SSoQuyetDinh = SSoQuyetDinh;
            }
            foreach (var item in lstGoiThauUpdate)
            {
                item.ILoai = 2;
                if (SelectedTiGia != null)
                {
                    item.IIdTiGiaId = SelectedTiGia.Id;
                }
                //item.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
                item.IIdDuToanId = SelectedDuToan.Id;
                item.IThuocMenu = IThuocMenu;
                //item.SSoQuyetDinh = SSoQuyetDinh;
                item.FTiGiaNhap = FTiGiaNhap;
            }
            if (lstGoiThauAdd.Count != 0)
                _goithauService.AddRange(lstGoiThauAdd);
            if (lstGoiThauUpdate.Count != 0)
                _goithauService.UpdateRange(lstGoiThauUpdate);
            if (lstIdDeleteDetail != null && lstIdDeleteDetail.Count != 0)
                DeleteGoiThauDetailInGoiThauDelete(lstIdDeleteDetail);
            if (lstGoiThauDelete.Count != 0)
                _goithauService.DeleteRange(lstGoiThauDelete);
            if (ItemsGoiThauGoc != null)
            {
                List<NhDaGoiThau> lstGoiThauGoc = new List<NhDaGoiThau>();
                foreach (var item in ItemsGoiThauGoc)
                {
                    var obj = _goithauService.FindById(item.Id);
                    if (obj != null)
                    {
                        obj.BActive = false;
                        lstGoiThauGoc.Add(obj);
                    }
                }
                if (lstGoiThauGoc.Count != 0)
                    _goithauService.UpdateRange(lstGoiThauGoc, false);
            }
            return true;
        }

        private bool ValidateGoiThau()
        {
            List<string> messErrors = new List<string>();
            if (!ItemsGoiThau.Any(n => !n.IsDeleted))
                messErrors.Add(string.Format(Resources.MsgErrorDataEmpty, "gói thầu"));

            if (messErrors.Count > 0)
            {
                MessageBoxHelper.Error(string.Join("\n", messErrors));
                return false;
            }
            int index = 1;
            foreach (var item in ItemsGoiThau.Where(n => !n.IsDeleted))
            {
                if (string.IsNullOrEmpty(item.STenGoiThau))
                    messErrors.Add(string.Format("dòng thứ {0} : {1}", index, string.Format(Resources.MsgErrorRequire, "Tên gói thầu")));
                if (!item.IIdHinhThucChonNhaThauId.HasValue)
                    messErrors.Add(string.Format("dòng thứ {0} : {1}", index, string.Format(Resources.MsgErrorRequire, "Hình thức chọn nhà thầu")));
                if (!item.IIdPhuongThucDauThauId.HasValue)
                    messErrors.Add(string.Format("dòng thứ {0} : {1}", index, string.Format(Resources.MsgErrorRequire, "Phương thức chọn nhà thầu")));
                if (!item.IIdLoaiHopDongId.HasValue)
                    messErrors.Add(string.Format("dòng thứ {0} : {1}", index, string.Format(Resources.MsgErrorRequire, "Loại hợp đồng")));
                if (!item.IThoiGianThucHien.HasValue)
                    messErrors.Add(string.Format("dòng thứ {0} : {1}", index, string.Format(Resources.MsgErrorRequire, "Thời gian thực hiện")));
            }
            if (messErrors.Count != 0)
            {
                MessageBoxHelper.Error(string.Join("\n", messErrors));
                return false;
            }
            return true;
        }

        private void LoadTiGia()
        {
            var drpData = _nhDmTiGiaService.FindAll().ToList();
            ItemsTiGia = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(drpData);
            if (Model.IIdTiGiaID.HasValue)
            SelectedTiGia = ItemsTiGia.FirstOrDefault(n => n.Id == Model.IIdTiGiaID.Value);
        }
        private void LoadLoaiKHLCNT()
        {
            ComboboxItem bqpPheDuyet = new ComboboxItem("BQP phê duyệt", "1");
            ComboboxItem cdtPheDuyet = new ComboboxItem("CĐT phê duyệt", "2");
            ItemsLoaiKHLCNT = new ObservableCollection<ComboboxItem>() { bqpPheDuyet, cdtPheDuyet };
        }

        private void LoadTiGiaChiTiet()
        {
            _itemsTiGiaChiTiet = new ObservableCollection<NhDmTiGiaChiTietModel>();
            var lstTiGiaDef = new List<string>();
            lstTiGiaDef.Add("VND");
            lstTiGiaDef.Add("USD");
            //lstTiGiaDef.Add("EUR");
            if (SelectedTiGia != null)
            {
                // Lấy danh sách tỉ giá chi tiết
                IEnumerable<NhDmTiGiaChiTiet> listTiGiaChiTiet = _nhDmTiGiaChiTietService.FindByTiGiaId(_selectedTiGia.Id);

                // Lọc ra danh sách tỉ giá ngoại tệ khác
                ItemsTiGiaChiTiet = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(listTiGiaChiTiet.Where(x => !lstTiGiaDef.Contains(x.SMaTienTeQuyDoi)));
                // Lọc ra danh sách tỉ giá chính (USD, VND, EUR)
                ItemsTiGiaChiTietChinh = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(listTiGiaChiTiet.Where(x => lstTiGiaDef.Contains(x.SMaTienTeQuyDoi)));

                //if (!string.IsNullOrEmpty(Model.SMaNgoaiTeKhac))
                //    SelectedTiGiaChiTiet = ItemsTiGiaChiTiet.FirstOrDefault(n => n.SMaTienTeQuyDoi == Model.SMaNgoaiTeKhac);
                //else
                //    SelectedTiGiaChiTiet = _itemsTiGiaChiTiet.First();
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

                _itemsNguonVonByGoiThau = _mapper.Map<List<NhDaGoiThauDetailNguonVonModel>>(_goithauNguonVonService.GetGoiThauNguonVonByKhlcntId(Model.Id));
                if (_itemsNguonVonByGoiThau != null) _itemsNguonVonByGoiThau = _itemsNguonVonByGoiThau
                         .Select(n =>
                         {
                             n.FGiaTriPheDuyetEUR = n.FGiaTriEUR;
                             n.FGiaTriPheDuyetNgoaiTeKhac = n.FGiaTriNgoaiTeKhac;
                             n.FGiaTriPheDuyetUSD = n.FGiaTriUSD;
                             n.FGiaTriPheDuyetVND = n.FGiaTriVND;
                             return n;
                         }).ToList();

                _itemsChiPhiByGoiThau = _mapper.Map<List<NhDaGoiThauDetailChiPhiModel>>(_goithauChiPhiService.GetGoiThauChiPhiByKhlcntId(Model.Id));
                if (_itemsChiPhiByGoiThau != null) _itemsChiPhiByGoiThau = _itemsChiPhiByGoiThau
                         .Select(n =>
                         {
                             n.FGiaTriPheDuyetEUR = n.FGiaTriEUR;
                             n.FGiaTriPheDuyetNgoaiTeKhac = n.FGiaTriNgoaiTeKhac;
                             n.FGiaTriPheDuyetUSD = n.FGiaTriUSD;
                             n.FGiaTriPheDuyetVND = n.FGiaTriVND;
                             return n;
                         }).ToList();

                _itemsHangMucByGoiThau = _mapper.Map<List<NhDaGoiThauDetailHangMucModel>>(_goithauHangMucService.GetGoiThauHangMucByKhlcntId(Model.Id));
            }
        }

        private void GetGoiThauGoc()
        {
            ItemsGoiThauGoc = new ObservableCollection<NhDaGoiThauModel>();
            if (!IsAdd || !Model.IIdParentID.HasValue) return;
            var lstGoiThau = _goithauService.FindByIidKhlcNhaThau(Model.IIdParentID.Value);
            if (lstGoiThau == null) return;
            ItemsGoiThauGoc = _mapper.Map<ObservableCollection<NhDaGoiThauModel>>(lstGoiThau);
            OnPropertyChanged(nameof(ItemsGoiThauGoc));
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
            //item.sSoKeHoachDatHang = objNhaThau.SSoQuyetDinh;
            //item.DNgayKeHoach = objNhaThau.DNgayQuyetDinh;
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
            if (bIsUpdate)
            {
                item.DNgaySua = DateTime.Now;
                item.SNguoiSua = _sessionService.Current.Principal;
                item.DNgayTao = data.DNgayTao;
                item.SNguoiTao = data.SNguoiTao;
                //item.DNgayQuyetDinh = data.DNgayQuyetDinh;
                //item.SSoQuyetDinh = data.SSoQuyetDinh;

            }
            else
            {
                item.DNgayTao = DateTime.Now;
                item.SNguoiTao = _sessionService.Current.Principal;
                //item.DNgayQuyetDinh = objNhaThau.DNgayQuyetDinh;

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

        private void DeleteGoiThauDetailInGoiThauDelete(List<Guid> iIdGoiThaus)
        {
            _itemsNguonVonByGoiThau = _itemsNguonVonByGoiThau.Where(n => n.IIdGoiThauID.HasValue && !iIdGoiThaus.Contains(n.IIdGoiThauID.Value)).ToList();
            _itemsChiPhiByGoiThau = _itemsChiPhiByGoiThau.Where(n => n.IIdGoiThauID.HasValue && !iIdGoiThaus.Contains(n.IIdGoiThauID.Value)).ToList();
            _itemsHangMucByGoiThau = _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauID.HasValue && !iIdGoiThaus.Contains(n.IIdGoiThauID.Value)).ToList();
        }
        #endregion

        private void OnShowImport()
        {
            ImportGoiThauViewModel.Init();
            ImportGoiThauViewModel.SavedAction = obj =>
            {
                if (ItemsGoiThau.Any())
                {
                    if(ImportGoiThauViewModel != null && ImportGoiThauViewModel.ItemsGoiThauModel.Any())
                    {
                        foreach (var item in ImportGoiThauViewModel.ItemsGoiThauModel)
                        {
                            ItemsGoiThau.Add(item);
                        }
                    }
                }
                else
                {
                    ItemsGoiThau = ImportGoiThauViewModel.ItemsGoiThauModel;

                }
                //this.LoadData();
            };
            ImportGoiThauViewModel.ShowDialog();
        }
    }
}
