using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.CapPhatThanhToan.PrintDialog
{
    public class CapPhatThanhToanPrintDialogViewModel : ViewModelBase
    {
        private readonly IVdtTtDeNghiThanhToanService _deNghiThanhToanService;
        private readonly ISessionService _sessionService;
        private readonly IVdtKhvPhanBoVonService _vdtKhvPhanBoVonService;
        private readonly ILog _logger;
        private readonly DmChuKyDialogViewModel _dmChuKyDialogViewModel;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private readonly IVdtDaDuAnService _duAnService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private readonly INsDonViService _nsDonViService;
        private readonly IMapper _mapper;
        private IExportService _exportService;
        private string _diaDiem;
        private readonly string _typeChuky = TypeChuKy.RPT_VDT_THUCHIENDAUTU_DENGHITHANHTOAN;
        private readonly string TITLE_FIRST_DEFAULT_VALUE = "GIẤY ĐỀ NGHỊ THANH TOÁN VỐN ĐẦU TƯ";
        private readonly string TITLE_SECOND_DEFAULT_VALUE = "";
        public override string Title => "Đề nghị thanh toán (thanh toán, tạm ứng)";
        public override string Description => "Đề nghị thanh toán (thanh toán, tạm ứng)";

        public IEnumerable<VdtTtDeNghiThanhToanModel> VdtTtDeNghiThanhToanModels { get; set; }

        private string _txtTitleFirst;
        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set
            {
                SetProperty(ref _txtTitleFirst, value);
            }
        }

        private string _txtTitleSecond;
        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }

        private string _sKinhGuiKhoBac;
        public string SKinhGuiKhoBac
        {
            get => _sKinhGuiKhoBac;
            set => SetProperty(ref _sKinhGuiKhoBac, value);
        }

        private string _sKinhGuiCoQuanTaiChinh;
        public string SKinhGuiCoQuanTaiChinh
        {
            get => _sKinhGuiCoQuanTaiChinh;
            set => SetProperty(ref _sKinhGuiCoQuanTaiChinh, value);
        }

        private ObservableCollection<ComboboxItem> _catUnitTypes = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> CatUnitTypes
        {
            get => _catUnitTypes;
            set => SetProperty(ref _catUnitTypes, value);
        }

        private ComboboxItem _catUnitTypeSelected;
        public ComboboxItem CatUnitTypeSelected
        {
            get => _catUnitTypeSelected;
            set => SetProperty(ref _catUnitTypeSelected, value);
        }

        private ObservableCollection<ComboboxItem> _catUnitSendReport = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> CatUnitSendReport
        {
            get => _catUnitSendReport;
            set => SetProperty(ref _catUnitSendReport, value);
        }

        private ComboboxItem _catUnitSendReportSelected;
        public ComboboxItem CatUnitSendReportSelected
        {
            get => _catUnitSendReportSelected;
            set => SetProperty(ref _catUnitSendReportSelected, value);
        }

        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public CapPhatThanhToanPrintDialogViewModel(IVdtTtDeNghiThanhToanService vdtTtDeNghiThanhToanService,
            ISessionService sessionService,
            IVdtKhvPhanBoVonService vdtKhvPhanBoVonService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            IExportService exportService,
            IVdtDaDuAnService duAnService,
            IDmChuDauTuService dmChuDauTuService,
            INsDonViService nsDonViService,
            ILog logger,
            IMapper mapper)
        {
            _deNghiThanhToanService = vdtTtDeNghiThanhToanService;
            _sessionService = sessionService;
            _vdtKhvPhanBoVonService = vdtKhvPhanBoVonService;
            _dmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _exportService = exportService;
            _dmChuDauTuService = dmChuDauTuService;
            _nsDonViService = nsDonViService;
            _duAnService = duAnService;
            _logger = logger;
            _mapper = mapper;
            PrintReportCommand = new RelayCommand(obj => OnExport(true));
            ExportCommand = new RelayCommand(obj => OnExport(false));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            LoadCatUnitTypes();
            LoadTitleFirst();
            LoadUnitSendReportSelected();
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

       

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            DmChuKy _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            _dmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            _dmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TxtTitleFirst = chuKy != null ? (string.IsNullOrEmpty(chuKy.TieuDe1MoTa) ? TITLE_FIRST_DEFAULT_VALUE : chuKy.TieuDe1MoTa) : string.Empty;
                TxtTitleSecond = chuKy != null ? (string.IsNullOrEmpty(chuKy.TieuDe2MoTa) ? TITLE_SECOND_DEFAULT_VALUE : chuKy.TieuDe2MoTa) : string.Empty;
            };
            _dmChuKyDialogViewModel.Init();
            _dmChuKyDialogViewModel.ShowDialog();
        }

        private void OnExport(bool isPrintPDF)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName, templateFileNameCoQuanTaiChinh;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    foreach (VdtTtDeNghiThanhToanModel item in VdtTtDeNghiThanhToanModels)
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        CapPhatThanhToanReportQuery dataReport = _deNghiThanhToanService.GetDataReport(item.Id.ToString(), _sessionService.Current.YearOfWork, Int32.Parse(CatUnitTypeSelected.ValueItem)).FirstOrDefault();

                        double luyKeTTTN = 0;
                        double luyKeTTNN = 0;
                        double luyKeTUTN = 0;
                        double luyKeTUNN = 0;
                        double luyKeTUUngTruocTN = 0;
                        double luyKeTUUngTruocNN = 0;

                        double sumTN = 0;
                        double sumNN = 0;

                        double sumTHTN = 0;
                        double sumTHNN = 0;

                        Guid iIdChungTu = new Guid();
                        if (item.BThanhToanTheoHopDong)
                            iIdChungTu = (item.iID_HopDongId ?? Guid.Empty);
                        else
                            iIdChungTu = (item.IIdChiPhiId ?? Guid.Empty);
                        if (item.dNgayDeNghi.HasValue && iIdChungTu != Guid.Empty)
                        {
                            _deNghiThanhToanService.LoadGiaTriThanhToanBaoCao(item.iCoQuanThanhToan.Value, item.dNgayDeNghi.Value, item.BThanhToanTheoHopDong, iIdChungTu.ToString(), item.iID_NguonVonID, item.iNamKeHoach, item.Id, item.loaiCoQuanTaiChinh,
                                ref luyKeTTTN, ref luyKeTTNN, ref luyKeTUTN, ref luyKeTUNN, ref luyKeTUUngTruocTN, ref luyKeTUUngTruocNN, ref sumTN, ref sumNN, ref sumTHTN, ref sumTHNN, null);
                        }

                        string CapTren = NSConstants.BO_QUOC_PHONG;
                        DmChuDauTu dmChuDauTu = new DmChuDauTu();
                        if (item.iID_DuAnId.HasValue)
                        {
                            VdtDaDuAn duan = _duAnService.FindById(item.iID_DuAnId.Value);
                            if (duan != null && duan.IIdChuDauTuId.HasValue)
                            {
                                dmChuDauTu = _dmChuDauTuService.FindById(duan.IIdChuDauTuId.Value);
                                if (dmChuDauTu != null)
                                {
                                    int namLamViec = dmChuDauTu.INamLamViec.HasValue ? dmChuDauTu.INamLamViec.Value :
                                        DateTime.Now.Year;
                                    DonVi donvi = _nsDonViService.FindByIdDonVi(dmChuDauTu.IIDMaDonVi, namLamViec);
                                    if (!"0".Equals(donvi?.Loai))
                                    {
                                        DonVi donViCapTren = _nsDonViService.FindByLoai("0", namLamViec);
                                        CapTren = donViCapTren?.TenDonVi;
                                    }
                                }
                            }
                        }

                        string sChudauTu = dataReport.TenChuDauTu;

                        if (CatUnitSendReportSelected != null && CatUnitSendReportSelected.ValueItem == "1")
                        {
                            CapTren = dataReport.TenChuDauTu;
                            sChudauTu = dataReport.SBanQuanLyDuAn;
                        }
                        data.Add("CapTren", CapTren);
                        data.Add("ChuDauTu", sChudauTu);
                        int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                        FormatNumber formatNumber = new FormatNumber(donViTinh, isPrintPDF ? ExportType.PDF : ExportType.EXCEL);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DonViTinh", (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem.ToLower() : ""));
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TenDuAn", dataReport.TenDuAn);
                        data.Add("MaDuAn", dataReport.MaDuAn);
                        
                        data.Add("TenHopDong", dataReport.TenHopDong);
                        data.Add("NgayHopDong", dataReport.NgayHopDong.HasValue ? dataReport.NgayHopDong.Value.Day.ToString() : string.Empty);
                        data.Add("ThangHopDong", dataReport.NgayHopDong.HasValue ? dataReport.NgayHopDong.Value.Month.ToString() : string.Empty);
                        data.Add("NamHopDong", dataReport.NgayHopDong.HasValue ? dataReport.NgayHopDong.Value.Year.ToString() : string.Empty);
                        data.Add("NguonVon", dataReport.TenNguonVon);
                        data.Add("NamKeHoach", dataReport.NamKeHoach);
                        data.Add("GiaTriHopDong", dataReport.GiaTriHopDong);
                        data.Add("NoiDung", dataReport.NoiDung);

                        //Thay đổi theo yêu cầu mới 25/11/2023 : Trong đề nghị thanh toán đã bao gồm số tiền thu hồi
                        Double ThuHuongTN = (dataReport.ThanhToanTN > 0 ? (dataReport.ThanhToanTN - dataReport.ThuHoiTN - dataReport.ChuyenTienBaoHanh) : (dataReport.ThanhToanTN - dataReport.ThuHoiTN)) / Int32.Parse(CatUnitTypeSelected.ValueItem);
                        Double ThuHuongNN = (dataReport.ThanhToanNN > 0 ? (dataReport.ThanhToanNN - dataReport.ThuHoiNN - dataReport.ChuyenTienBaoHanh) : (dataReport.ThanhToanNN - dataReport.ThuHoiNN)) / Int32.Parse(CatUnitTypeSelected.ValueItem);
                        Double TongThuHuong = ThuHuongTN + ThuHuongNN;

                        if (item.iLoaiThanhToan == 1) // Thanh toán
                        {
                            //data.Add("ThanhToanTN", (dataReport.ThanhToanTN + dataReport.ThuHoiTN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                            //data.Add("ThanhToanNN", (dataReport.ThanhToanNN + dataReport.ThuHoiNN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                            ////data.Add("LuyKeTN", (luyKeTTTN + luyKeTUTN + luyKeTUUngTruocTN));
                            //data.Add("LuyKeTN", (item.fLuyKeThanhToanTN + item.fLuyKeTUChuaThuHoiKhacTN + item.fLuyKeTUChuaThuHoiTN - dataReport.ThuHoiTN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                            ////data.Add("LuyKeNN", (luyKeTTNN + luyKeTUNN + luyKeTUUngTruocNN));
                            //data.Add("LuyKeNN", (item.fLuyKeThanhToanNN + item.fLuyKeTUChuaThuHoiNN + item.fLuyKeTUChuaThuHoiKhacNN - dataReport.ThuHoiNN) / Int32.Parse(CatUnitTypeSelected.ValueItem));

                            //Thay đổi theo yêu cầu mới 25/11/2023
                            data.Add("ThanhToanTN", (dataReport.ThanhToanTN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                            data.Add("ThanhToanNN", (dataReport.ThanhToanNN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                            //data.Add("LuyKeTN", (luyKeTTTN + luyKeTUTN + luyKeTUUngTruocTN));
                            //data.Add("LuyKeTN", (item.fLuyKeThanhToanTN + item.fLuyKeTUChuaThuHoiKhacTN + item.fLuyKeTUChuaThuHoiTN - dataReport.ThuHoiTN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                            data.Add("LuyKeTN", (luyKeTTTN + luyKeTUTN + luyKeTUUngTruocTN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                            //data.Add("LuyKeNN", (luyKeTTNN + luyKeTUNN + luyKeTUUngTruocNN));
                            //data.Add("LuyKeNN", (item.fLuyKeThanhToanNN + item.fLuyKeTUChuaThuHoiNN + item.fLuyKeTUChuaThuHoiKhacNN - dataReport.ThuHoiNN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                            data.Add("LuyKeNN", (luyKeTTNN + luyKeTUNN + luyKeTUUngTruocNN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                        }
                        // Tạm ứng
                        else
                        {
                            //data.Add("ThanhToanTN", (dataReport.ThanhToanTN - dataReport.ThuHoiTN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                            //data.Add("ThanhToanNN", (dataReport.ThanhToanNN - dataReport.ThuHoiTN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                            ////data.Add("LuyKeTN", (luyKeTTTN + luyKeTUTN + luyKeTUUngTruocTN));
                            //data.Add("LuyKeTN", (dataReport.ThanhToanTN + item.fLuyKeTUChuaThuHoiKhacTN + item.fLuyKeTUChuaThuHoiTN - dataReport.ThuHoiTN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                            ////data.Add("LuyKeNN", (luyKeTTNN + luyKeTUNN + luyKeTUUngTruocNN));
                            //data.Add("LuyKeNN", (dataReport.ThanhToanNN + item.fLuyKeTUChuaThuHoiNN + item.fLuyKeTUChuaThuHoiKhacNN - dataReport.ThuHoiNN) / Int32.Parse(CatUnitTypeSelected.ValueItem));

                            //Thay đổi theo yêu cầu mới 25/11/2023
                            data.Add("ThanhToanTN", (dataReport.ThanhToanTN ) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                            data.Add("ThanhToanNN", (dataReport.ThanhToanNN ) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                            //data.Add("LuyKeTN", (luyKeTTTN + luyKeTUTN + luyKeTUUngTruocTN));
                            data.Add("LuyKeTN", (item.fLuyKeThanhToanTN + item.fLuyKeTUChuaThuHoiKhacTN + item.fLuyKeTUChuaThuHoiTN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                            //data.Add("LuyKeNN", (luyKeTTNN + luyKeTUNN + luyKeTUUngTruocNN));
                            data.Add("LuyKeNN", (item.fLuyKeThanhToanNN + item.fLuyKeTUChuaThuHoiNN + item.fLuyKeTUChuaThuHoiKhacNN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                        }                        
                        data.Add("ThueGiaTriGiaTang", item.FThueGiaTriGiaTang / Int32.Parse(CatUnitTypeSelected.ValueItem));
                        data.Add("ChuyenTienBaoHanh", item.FChuyenTienBaoHanh / Int32.Parse(CatUnitTypeSelected.ValueItem));
                        data.Add("SoTraDonViThuHuong", item.FSoTraDonViThuHuong / Int32.Parse(CatUnitTypeSelected.ValueItem));

                       
                        //data.Add("ThuHuongTN", (dataReport.ThanhToanTN - dataReport.ThuHoiTN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                        //data.Add("ThuHuongNN", (dataReport.ThanhToanNN - dataReport.ThuHoiNN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                        //data.Add("TongThuHuong", ((dataReport.ThanhToanNN - dataReport.ThuHoiNN) + (dataReport.ThanhToanTN - dataReport.ThuHoiTN)) / Int32.Parse(CatUnitTypeSelected.ValueItem));

                        data.Add("ThuHuongTN", ThuHuongTN);
                        data.Add("ThuHuongNN", ThuHuongNN);
                        data.Add("TongThuHuong", TongThuHuong);

                        data.Add("TenNhaThau", item.STenDonViThuHuong);
                        data.Add("SoTaiKhoanNhaThau", item.SSoTaiKhoanNhaThau);
                        data.Add("CoQuanThanhToan", item.SMaNganHang);
                        //data.Add("SSoBangKlht", item.SSoBangKlht);
                        data.Add("sThongTinCanCu", item.sThongTinCanCu);
                        data.Add("MaSoDVSDNS", dmChuDauTu != null ? dmChuDauTu.MaSoDVSDNS : string.Empty);
                        data.Add("STKTrongNuoc", dmChuDauTu != null ? dmChuDauTu.STKTrongNuoc : string.Empty);
                        data.Add("ChiNhanhTrongNuoc", dmChuDauTu != null ? dmChuDauTu.ChiNhanhTrongNuoc : string.Empty);
                        data.Add("STKNuocNgoai", dmChuDauTu != null ? dmChuDauTu.STKNuocNgoai : string.Empty);
                        data.Add("ChiNhanhNuocNgoai", dmChuDauTu != null ? dmChuDauTu.ChiNhanhNuocNgoai : string.Empty);
                        //if (item.DNgayBangKlht.HasValue)
                        //    data.Add("SNgayBangKlht", String.Format("ngày {0} tháng {1} năm {2}", item.DNgayBangKlht.Value.Day, item.DNgayBangKlht.Value.Month, item.DNgayBangKlht.Value.Year));
                        //else
                        //    data.Add("SNgayBangKlht", String.Format("ngày {0} tháng {0} năm {0}", "..."));
                        data.Add("FLuyKeGiaTriNghiemThuKlht", item.FLuyKeGiaTriNghiemThuKlht);
                        data.Add("FTongThanhToan", (item.fGiaTriThanhToanTN + item.fGiaTriThanhToanNN)/Int32.Parse(CatUnitTypeSelected.ValueItem));
                        data.Add("FTongThanhToanFull", (item.fGiaTriThanhToanTN + item.fGiaTriThanhToanNN));
                        data.Add("FThuHoiTamUng", (item.FGiaTriThuHoiUngTruocTn + item.FGiaTriThuHoiUngTruocNn + item.fGiaTriThuHoiTN + item.fGiaTriThuHoiNN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                        data.Add("ThuHoiTN", (item.FGiaTriThuHoiUngTruocTn + item.fGiaTriThuHoiTN) / Int32.Parse(CatUnitTypeSelected.ValueItem));
                        data.Add("ThuHoiNN", (item.FGiaTriThuHoiUngTruocNn + item.fGiaTriThuHoiNN) / Int32.Parse(CatUnitTypeSelected.ValueItem));

                        data.Add("duToanPheDuyet", dataReport.duToanPheDuyet);
                        data.Add("ngayDuToanPheDuyet", dataReport.khvNgayQuyetDinh.Day);
                        data.Add("thangDuToanPheDuyet", dataReport.khvNgayQuyetDinh.Month);
                        data.Add("namDuToanPheDuyet", dataReport.khvNgayQuyetDinh.Year);
                        data.Add("deNghiThanhToan", dataReport.SoDeNghi);
                        data.Add("ngayDeNghiThanhToan", dataReport.thanhtoanNgayDeNghi.Day);
                        data.Add("thangDeNghiThanhToan", dataReport.thanhtoanNgayDeNghi.Month);
                        data.Add("namDeNghiThanhToan", dataReport.thanhtoanNgayDeNghi.Year);


                        if (item.iCoQuanThanhToan.HasValue && item.iCoQuanThanhToan.Value == (int)CoQuanThanhToanEnum.Type.KHO_BAC)
                        {
                            data.Add("TenCoQuan", SKinhGuiKhoBac);
                        }
                        else if (item.iCoQuanThanhToan.HasValue && item.iCoQuanThanhToan.Value == (int)CoQuanThanhToanEnum.Type.CQTC)
                        {
                            data.Add("TenCoQuan", SKinhGuiCoQuanTaiChinh);
                        }

                        //double luyKeTTTN = 0;
                        //double luyKeTTNN = 0;
                        //double luyKeTUTN = 0;
                        //double luyKeTUNN = 0;
                        //double luyKeTUUngTruocTN = 0;
                        //double luyKeTUUngTruocNN = 0;

                        //double sumTN = 0;
                        //double sumNN = 0;

                        //Guid iIdChungTu = new Guid();
                        //if (item.BThanhToanTheoHopDong)
                        //    iIdChungTu = (item.iID_HopDongId ?? Guid.Empty);
                        //else
                        //    iIdChungTu = (item.IIdChiPhiId ?? Guid.Empty);

                        //if (item.dNgayDeNghi.HasValue && iIdChungTu != Guid.Empty)
                        //{
                        //    _deNghiThanhToanService.LoadGiaTriThanhToan(item.iCoQuanThanhToan.Value, item.dNgayDeNghi.Value, item.BThanhToanTheoHopDong, iIdChungTu.ToString(), item.iID_NguonVonID, item.iNamKeHoach, item.Id, item.loaiCoQuanTaiChinh,
                        //        ref luyKeTTTN, ref luyKeTTNN, ref luyKeTUTN, ref luyKeTUNN, ref luyKeTUUngTruocTN, ref luyKeTUUngTruocNN, ref sumTN, ref sumNN, null);
                        //}                        

                        AddChuKy(data, _typeChuky);

                        List<KeHoachVonQuery> listKeHoachVon = new List<KeHoachVonQuery>();
                        if (item.iID_DuAnId.HasValue)
                        {
                            listKeHoachVon = _vdtKhvPhanBoVonService.GetKeHoachVonCapPhatThanhToan(item.iID_DuAnId.Value.ToString(), item.iID_NguonVonID, item.dNgayDeNghi.Value, item.iNamKeHoach, (item.iCoQuanThanhToan ?? 0), item.Id);
                        }

                        if (item.iLoaiThanhToan == 1)
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDT_CAPPHATTHANHTOANTHANHTOAN);
                            templateFileNameCoQuanTaiChinh = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDTQUANLYCAPPHATTHANHTOANTHANHTOAN_NODATA);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDT_CAPPHATTHANHTOAN);
                            templateFileNameCoQuanTaiChinh = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDT_CAPPHATTHANHTOAN_NODATA);
                        }
                        fileNamePrefix = string.Format("rptCapPhatThanhToan_{0}", item.sSoDeNghi);
                        string fileNamePrefixCoQuanTaiChinh = string.Format("rptCapPhatThanhToanCoQuanTaiChinh_{0}", item.sSoDeNghi);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        string fileNameWithoutExtensionCoQuanTaiChinh = StringUtils.CreateExportFileName(fileNamePrefixCoQuanTaiChinh);
                        var xlsFile = _exportService.Export<TlRptQuanSoKeHoachQuery>(templateFileName, data);
                        var xlsFileCoQuanTaiChinh = _exportService.Export<TlRptQuanSoKeHoachQuery>(templateFileNameCoQuanTaiChinh, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        results.Add(new ExportResult(fileNameWithoutExtensionCoQuanTaiChinh, fileNameWithoutExtensionCoQuanTaiChinh, null, xlsFileCoQuanTaiChinh));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, isPrintPDF ? ExportType.PDF : ExportType.EXCEL);
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

        private void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add ngày địa điểm
            data.Add("Ngay", DateUtils.GetCurrentDateReport());
            data.Add("DiaDiem", _diaDiem);
            // add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh1MoTa))
            {
                data.Add("ThuaLenh1", dmChyKy.ThuaLenh1MoTa);
                data.Add("ChucDanh1", dmChyKy.ChucDanh1MoTa);
                data.Add("GhiChuKy1", "(Ký, họ tên)");
                data.Add("Ten1", dmChyKy.Ten1MoTa);
            }
            if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh2MoTa))
            {
                data.Add("ThuaLenh2", dmChyKy.ThuaLenh2MoTa);
                data.Add("ChucDanh2", dmChyKy.ChucDanh2MoTa);
                data.Add("GhiChuKy2", "(Ký, họ tên)");
                data.Add("Ten2", dmChyKy.Ten2MoTa);
            }
            if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh3MoTa))
            {
                data.Add("ThuaLenh3", dmChyKy.ThuaLenh3MoTa);
                data.Add("ChucDanh3", dmChyKy.ChucDanh3MoTa);
                data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
                data.Add("Ten3", dmChyKy.Ten3MoTa);
            }
        }

        private void LoadCatUnitTypes()
        {
            _catUnitTypes = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH));
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            List<DanhMuc> data = _danhMucService.FindByCondition(predicate).OrderBy(x => x.SGiaTri).ToList();
            _catUnitTypes = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            if (data.Count == 0)
            {
                _catUnitTypes.Insert(0, new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            _catUnitTypeSelected = _catUnitTypes.FirstOrDefault();
        }

        private void LoadTitleFirst()
        {
            //DmChuKy _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TxtTitleFirst = TITLE_FIRST_DEFAULT_VALUE;
            TxtTitleSecond = TITLE_SECOND_DEFAULT_VALUE;
        }
        public void LoadUnitSendReportSelected()
        {
            _catUnitSendReport = new ObservableCollection<ComboboxItem>();
            _catUnitSendReport.Add(new ComboboxItem { DisplayItem = "Ban quản lý gửi dự án lên chủ đầu tư", ValueItem = "1" });
            _catUnitSendReport.Add(new ComboboxItem { DisplayItem = "Chủ đầu tư gửi lên cục tài chính", ValueItem = "2" });

            _catUnitSendReportSelected = _catUnitSendReport.FirstOrDefault();
        }
    }
}
