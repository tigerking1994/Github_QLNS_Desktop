using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.DeNghiQTDAHT;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.DeNghiQTDAHT
{
    public class DeNghiQTDAHTReportDialogViewModel : DialogViewModelBase<NhQtQuyetToanDahtModel>
    {
        private INsDonViService _nsDonViService;
        private readonly IMapper _mapper;
        private ISessionService _sessionService;
        private IExportService _exportService;
        private readonly ILog _logger;
        private readonly IDanhMucService _danhMucService;
        private readonly INhQtQuyetToanDahtService _nhQtQuyetToanDahtService;
        private readonly INhDaQdDauTuNguonVonService _nhDaQdDauTuNguonVonService;
        private readonly IVdtDaDuAnService _duAnService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private DanhMucCauHinhHeThongService _danhMucCauHinhHeThongService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private IDmChuKyService _dmChuKyService;
        private DmChuKy _dmChuKy;
        public override string GroupName => MenuItemContants.GROUP_REPORT;
        public override string Name => "Báo cáo tổng hợp quyết toán";
        public override string Title => "Báo cáo tổng hợp quyết toán";
        public override string Description => "Báo cáo tổng hợp quyết toán";
        public override PackIconKind IconKind => PackIconKind.ClipboardFileOutline;
        public override Type ContentType => typeof(DeNghiQTDAHTReportDialog);

        private ObservableCollection<NhQtQuyetToanDahtModel> _listDeNghi;
        public ObservableCollection<NhQtQuyetToanDahtModel> ListDeNghi
        {
            get => _listDeNghi;
            set => SetProperty(ref _listDeNghi, value);
        }

        private string _tieuDe;
        public string TieuDe
        {
            get => _tieuDe;
            set => SetProperty(ref _tieuDe, value);
        }

        private string _diaDiem;
        public string DiaDiem
        {
            get => _diaDiem;
            set => SetProperty(ref _diaDiem, value);
        }

        private string _kinhGui;
        public string KinhGui
        {
            get => _kinhGui;
            set => SetProperty(ref _kinhGui, value);
        }

        private DateTime? _ngayBaoCao;
        public DateTime? NgayBaoCao
        {
            get => _ngayBaoCao;
            set => SetProperty(ref _ngayBaoCao, value);
        }

        private string _tinhHinhThucHienDuAn;
        public string TinhHinhThucHienDuAn
        {
            get => _tinhHinhThucHienDuAn;
            set => SetProperty(ref _tinhHinhThucHienDuAn, value);
        }

        private string _nhanXetDanhGia;
        public string NhanXetDanhGia
        {
            get => _nhanXetDanhGia;
            set => SetProperty(ref _nhanXetDanhGia, value);
        }

        private string _kienNghi;
        public string KienNghi
        {
            get => _kienNghi;
            set => SetProperty(ref _kienNghi, value);
        }

        private ObservableCollection<ComboboxItem> _dataLoaiBaoCao;
        public ObservableCollection<ComboboxItem> DataLoaiBaoCao
        {
            get => _dataLoaiBaoCao;
            set => SetProperty(ref _dataLoaiBaoCao, value);
        }

        private ComboboxItem _selectedLoaiBaoCao;
        public ComboboxItem SelectedLoaiBaoCao
        {
            get => _selectedLoaiBaoCao;
            set
            {
                SetProperty(ref _selectedLoaiBaoCao, value);
                if (_selectedLoaiBaoCao != null)
                {
                    //LoadTieuDe();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _DataDonViTinh;
        public ObservableCollection<ComboboxItem> DataDonViTinh
        {
            get => _DataDonViTinh;
            set => SetProperty(ref _DataDonViTinh, value);
        }

        private ComboboxItem _selectedDonViTinh;
        public ComboboxItem SelectedDonViTinh
        {
            get => _selectedDonViTinh;
            set => SetProperty(ref _selectedDonViTinh, value);
        }

        private ObservableCollection<ComboboxItem> _dataDeNghi;
        public ObservableCollection<ComboboxItem> DataDeNghi
        {
            get => _dataDeNghi;
            set => SetProperty(ref _dataDeNghi, value);
        }

        private NhQtQuyetToanDahtModel _selectedDeNghi;
        public NhQtQuyetToanDahtModel SelectedDeNghi
        {
            get => _selectedDeNghi;
            set => SetProperty(ref _selectedDeNghi, value);
        }

        public RelayCommand ExportCommand { get; set; }
        public RelayCommand PrintPDFCommand { get; set; }
        public RelayCommand ConfigSignCommand { get; }
        private List<NHDAQDDauTuChiPhiHangMucModel> Items { get; set; }

        private List<NHDAQDDauTuChiPhiHangMucModel> _exportCPHM { get; set; }

        public DeNghiQTDAHTReportDialogViewModel(INsDonViService nsDonViService,
                                            ISessionService sessionService,
                                            IVdtDaDuAnService vdtDaDuAnService,
                                            IExportService exportService,
                                            IVdtQtQuyetToanService vdtQtQuyetToanService,
                                            ILog logger,
                                            IDmChuKyService dmChuKyService,
                                            DmChuKyDialogViewModel dmChuKyDialogViewModel,
                                            IDanhMucService danhMucService,
                                            IVdtDaDuAnService duAnService,
                                            IDmChuDauTuService dmChuDauTuService,
                                            INhQtQuyetToanDahtService nhQtQuyetToanDahtService,
                                            DanhMucCauHinhHeThongService danhMucCauHinhHeThongService,
                                            INhDaQdDauTuNguonVonService nhDaQdDauTuNguonVonService,
                                            IMapper mapper)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _dmChuDauTuService = dmChuDauTuService;
            _duAnService = duAnService;
            _nhDaQdDauTuNguonVonService = nhDaQdDauTuNguonVonService;
            _nhQtQuyetToanDahtService = nhQtQuyetToanDahtService;
            _danhMucCauHinhHeThongService = danhMucCauHinhHeThongService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _logger = logger;
            _mapper = mapper;

            ExportCommand = new RelayCommand(o => OnExport(ExportType.EXCEL));
            PrintPDFCommand = new RelayCommand(o => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public double GetDonViTinh()
        {
            if (SelectedDonViTinh == null || string.IsNullOrEmpty(SelectedDonViTinh.ValueItem))
                return 1;
            return double.Parse(SelectedDonViTinh.ValueItem);
        }

        private void OnExport(ExportType exportType)
        {
            if (ListDeNghi == null || SelectedDeNghi == null)
            {
                return;
            }
            /*NhQtQuyetToanDahtModel deNghiItem = ListDeNghi.Where(n => n.Id.ToString() == SelectedDeNghi.ValueItem).FirstOrDefault();
            if (deNghiItem == null)
            {
                return;
            }*/
            if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_TO_TRINH)
            {
                ExportToTrinh(SelectedDeNghi, exportType);
            }
            else if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC)
            {
                ExportPhuLuc(SelectedDeNghi, exportType);
            }
        }

        private void ExportPhuLuc(NhQtQuyetToanDahtModel deNghiItem, ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NH_TONG_HOP_QUYETTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                    double donViTinh = GetDonViTinh();

                    string CapTren = NSConstants.BO_QUOC_PHONG;

                    LoadChiPhi(deNghiItem);

                    FormatNumber formatNumber = new FormatNumber((int)donViTinh, exportType);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("CapTren", CapTren);
                    data.Add("ChuDauTu", deNghiItem.STenCDT);
                    data.Add("TieuDe", TieuDe);
                    data.Add("DonViTinh", SelectedDonViTinh.DisplayItem);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Items", _exportCPHM);
                    data.Add("ThuaLenh1", string.Empty);
                    data.Add("ThuaLenh2", string.Empty);
                    data.Add("ThuaLenh3", string.Empty);
                    data.Add("ChucDanh1", string.Empty);
                    data.Add("ChucDanh2", string.Empty);
                    data.Add("ChucDanh3", string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    data.Add("SumDT", Items.Sum(t => t.VNDDT.HasValue ? t.VNDDT.Value : 0));
                    data.Add("SumQT", Items.Sum(t => t.VNDQT.HasValue ? t.VNDQT.Value : 0));
                    data.Add("SumKT", Items.Sum(t => t.VNDKT.HasValue ? t.VNDKT.Value : 0));
                    data.Add("SumCDT", Items.Sum(t => t.VNDCDT.HasValue ? t.VNDCDT.Value : 0));
                    data.Add("SumSSDT", Items.Sum(t => t.VNDSSDT.HasValue ? t.VNDSSDT.Value : 0));
                    data.Add("SumSSQT", Items.Sum(t => t.VNDSSQT.HasValue ? t.VNDSSQT.Value : 0));
                    data.Add("SumSSKT", Items.Sum(t => t.VNDSSKT.HasValue ? t.VNDSSKT.Value : 0));
                    string templateFileName = Path.Combine(ExportPrefix.PATH_NH_QT, ExportFileName.RPT_NH_TONGHOPQUYETTOANDUANHOANTHANH_PHULUC);
                    string fileNamePrefix = "rptQuyetToanHoanThanhPhuLuc";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<NHDAQDDauTuChiPhiHangMucModel>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
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

        private void LoadChiPhi(NhQtQuyetToanDahtModel NhQtQuyetToanDahtModel)
        {
            var data = _nhQtQuyetToanDahtService.GetChiPhiHangMucByDuAnId(NhQtQuyetToanDahtModel.IIdDuAnId.Value, NhQtQuyetToanDahtModel.Id);
            Items = _mapper.Map<List<NHDAQDDauTuChiPhiHangMucModel>>(data);
            _exportCPHM = OrderChiPhi(Items);
            for (int i = 0; i < _exportCPHM.Count; i++)
            {
                if (_exportCPHM.ElementAt(i).IType == NHDAQDDauTuChiPhiHangMucModel_Loai.CP)
                {
                    AddHmToCp(i, _exportCPHM.ElementAt(i));
                }
            }
        }

        private void ExportToTrinh(NhQtQuyetToanDahtModel deNghiItem, ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NH_TONG_HOP_QUYETTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                    double donViTinh = GetDonViTinh();
                    double giaTriDuToan = 0;
                    string CapTren = NSConstants.BO_QUOC_PHONG;

                    var nguonVon = _nhDaQdDauTuNguonVonService.FindByDuAnId(deNghiItem.IIdDuAnId.Value).ToList();
                    var nguonVonItems = _mapper.Map<List<NHDAQDDauTuNguonVonQueryModel>>(nguonVon);
                    nguonVonItems = nguonVonItems.Select((t, i) =>
                    {
                        t.STT = i + 1;
                        return t;
                    }).ToList();
                    LoadChiPhi(deNghiItem);
                    _exportCPHM = _exportCPHM.Where(t => t.IType == NHDAQDDauTuChiPhiHangMucModel_Loai.CP).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("CapTren", CapTren);
                    data.Add("ChuDauTu", deNghiItem.STenCDT);
                    FormatNumber formatNumber = new FormatNumber((int)donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TieuDe", TieuDe);
                    data.Add("TenDuAn", deNghiItem.STenDuAn);
                    //data.Add("DuyetDieuChinhLanCuoi", (quyetDinhDauTu != null && quyetDinhDauTu.Id != Guid.Empty && quyetDinhDauTu.FTongMucDauTuPheDuyet.HasValue) ? quyetDinhDauTu.FTongMucDauTuPheDuyet / donViTinh : 0);
                    data.Add("DuToanDuocDuyetCuoi", giaTriDuToan / donViTinh);
                    data.Add("DonViTinh", SelectedDonViTinh.DisplayItem);
                    data.Add("NguonVonItems", nguonVonItems);
                    data.Add("CPHM", _exportCPHM);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                    data.Add("NguyenNhanBatKhaKhang", deNghiItem.FCpthietHaiVnd.HasValue ? deNghiItem.FCpthietHaiVnd.Value : 0);
                    data.Add("ChiPhiKhongTaoTaiSan", deNghiItem.FCpkhongTaoTaiSanVnd.HasValue ? deNghiItem.FCpkhongTaoTaiSanVnd.Value : 0);
                    data.Add("TaiSanDaiHan", deNghiItem.FTaiSanDaiHanVnd.HasValue ? deNghiItem.FTaiSanDaiHanVnd.Value : 0);
                    data.Add("TaiSanNganHan", deNghiItem.FTaiSanNganHanVnd.HasValue ? deNghiItem.FTaiSanNganHanVnd.Value : 0);
                    data.Add("TinhHinhThucHienDuAn", TinhHinhThucHienDuAn);
                    data.Add("NhanXetDanhGia", NhanXetDanhGia);
                    data.Add("KienNghi", KienNghi);
                    data.Add("Kinhgui", KinhGui);
                    data.Add("Diadiem", DiaDiem);
                    data.Add("Ngay", NgayBaoCao.HasValue ? NgayBaoCao.Value.Day.ToString() : "...");
                    data.Add("Thang", NgayBaoCao.HasValue ? NgayBaoCao.Value.Month.ToString() : "...");
                    data.Add("Nam", NgayBaoCao.HasValue ? NgayBaoCao.Value.Year.ToString() : "...");

                    string templateFileName = Path.Combine(ExportPrefix.PATH_NH_QT, ExportFileName.RPT_NH_TONGHOPQUYETTOANDUANHOANTHANH);
                    string fileNamePrefix = "rptQuyetToanHoanThanh";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<NHDAQDDauTuNguonVonQueryModel, NHDAQDDauTuChiPhiHangMucModel>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
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

        private void LoadDeNghi()
        {
            if (ListDeNghi != null && ListDeNghi.Count > 0)
            {
                SelectedDeNghi = ListDeNghi.FirstOrDefault();
            }
            OnPropertyChanged(nameof(SelectedDeNghi));
        }

        private void LoadDiaDiem()
        {
            DiaDiem = _sessionService.Current.TenDanhMucDiaDiem;
        }

        private void LoadDonViTinh()
        {
            DataDonViTinh = new ObservableCollection<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh == null || listDonViTinh.Count <= 0)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            foreach (var dvt in listDonViTinh)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = dvt.SGiaTri.ToString(), DisplayItem = dvt.STen });
            }
            SelectedDonViTinh = DataDonViTinh.FirstOrDefault();
        }

        private void LoadLoaiBaoCao()
        {
            DataLoaiBaoCao = new ObservableCollection<ComboboxItem>();
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_TO_TRINH, DisplayItem = LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_TO_TRINH });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC, DisplayItem = LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC });
            SelectedLoaiBaoCao = DataLoaiBaoCao.FirstOrDefault();
        }

        private void LoadTieuDe()
        {
            if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_TO_TRINH)
            {
                TieuDe = "Tổng hợp quyết toán dự án hoàn thành";
            }
            else if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC)
            {
                TieuDe = "Chi phí đầu tư đề nghị quyết toán";
            }
            OnPropertyChanged(nameof(TieuDe));
        }

        private void LoadData()
        {
            try
            {
                LoadLoaiBaoCao();
                LoadDeNghi();
                LoadDonViTinh();
                LoadDiaDiem();
                //LoadTieuDe();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void Init()
        {
            try
            {
                if (SelectedLoaiBaoCao == null || SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_TO_TRINH)
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NH_TONG_HOP_QUYETTOAN_TOTRINH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                else if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC)
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NH_TONG_HOP_QUYETTOAN_PHULUC) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                TieuDe = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : "Tổng hợp quyết toán dự án hoàn thành";
                LoadData();
                OnPropertyChanged(nameof(TieuDe));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_TO_TRINH)
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NH_TONG_HOP_QUYETTOAN_TOTRINH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            else if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC)
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NH_TONG_HOP_QUYETTOAN_PHULUC) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NH_TONG_HOP_QUYETTOAN_TOTRINH;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC)
            {
                DmChuKyDialogViewModel.IsEnableChuKy = false;
                chuKyModel.IdType = TypeChuKy.RPT_NH_TONG_HOP_QUYETTOAN_PHULUC;
            }
            else
            {
                DmChuKyDialogViewModel.IsEnableChuKy = true;
                chuKyModel.IdType = TypeChuKy.RPT_NH_TONG_HOP_QUYETTOAN_TOTRINH;
            }

            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TieuDe = chuKy.TieuDe1MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private List<NHDAQDDauTuChiPhiHangMucModel> OrderChiPhi(IEnumerable<NHDAQDDauTuChiPhiHangMucModel> datas)
        {
            int index = 1;
            List<NHDAQDDauTuChiPhiHangMucModel> results = new List<NHDAQDDauTuChiPhiHangMucModel>();
            var cp = datas.Where(n => n.IType == NHDAQDDauTuChiPhiHangMucModel_Loai.CP);
            foreach (var item in cp.Where(n => !n.ParentId.HasValue))
            {
                item.STT = index.ToString();
                index++;
                results.AddRange(RecusiveChiPhi(item, cp.ToList()));
            }
            return results;
        }

        private List<NHDAQDDauTuChiPhiHangMucModel> RecusiveChiPhi(NHDAQDDauTuChiPhiHangMucModel item, List<NHDAQDDauTuChiPhiHangMucModel> datas)
        {
            List<NHDAQDDauTuChiPhiHangMucModel> results = new List<NHDAQDDauTuChiPhiHangMucModel>();
            List<NHDAQDDauTuChiPhiHangMucModel> rootItems = datas.Where(n => n.ParentId == item.Id).ToList();
            results.Add(item);
            for (int j = 0; j < rootItems.Count; j++)
            {
                var child = rootItems.ElementAt(j);
                child.STT = item.STT + "." + j;
                results.AddRange(RecusiveChiPhi(child, datas));
            }
            return results;
        }

        private void AddHmToCp(int index, NHDAQDDauTuChiPhiHangMucModel cp)
        {
            var hm = Items.Where(n => n.IType == NHDAQDDauTuChiPhiHangMucModel_Loai.HM && n.ChiPhiId == cp.Id).ToList();
            foreach (var item in hm.Where(n => !n.ParentId.HasValue))
            {
                _exportCPHM.InsertRange(index + 1, RecusiveHM(item, hm));
            }
        }

        private List<NHDAQDDauTuChiPhiHangMucModel> RecusiveHM(NHDAQDDauTuChiPhiHangMucModel item, List<NHDAQDDauTuChiPhiHangMucModel> datas)
        {
            List<NHDAQDDauTuChiPhiHangMucModel> results = new List<NHDAQDDauTuChiPhiHangMucModel>();
            results.Add(item);
            foreach (var child in datas.Where(n => n.ParentId == item.Id))
            {
                results.AddRange(RecusiveHM(child, datas));
            }
            return results;
        }
    }
}
