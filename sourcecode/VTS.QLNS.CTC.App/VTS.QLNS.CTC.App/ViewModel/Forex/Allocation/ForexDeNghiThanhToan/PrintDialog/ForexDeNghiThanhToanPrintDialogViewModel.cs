using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Text;
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

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexAllocation.ForexDeNghiThanhToan.PrintDialog
{
    public class ForexDeNghiThanhToanPrintDialogViewModel : ViewModelBase
    {
        private readonly INhTtThanhToanService _iNhTtThanhToanService;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly DmChuKyDialogViewModel _dmChuKyDialogViewModel;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDmNhaThauService _iNhDmNhaThauService;
        private readonly INhDmNhaThauNguoiNhanService _iNhDmNhaThauNguoiNhanService;
        private readonly INhDmNhaThauNganHangService _nhDmNhaThauNganHangService;
        private readonly INhTtThanhToanChiTietService _iNhTtThanhToanChiTietService;
        private readonly INhDaHopDongService _iNhDaHopDongService;
        private readonly IMucLucNganSachService _iMucLucNganSachService;
        private readonly IMapper _mapper;
        private IExportService _exportService;
        private string _diaDiem;
        private readonly string _typeChuky = TypeChuKy.RPT_NH_CAPPHAT_DENGHITHANHTOAN;
        private readonly string TITLE_FIRST_DEFAULT_VALUE = "GIẤY ĐỀ NGHỊ CẤP, THANH TOÁN KINH PHÍ NGUỒN QUỸ DỰ TRỮ NGOẠI HỐI";
        private readonly string TITLE_SECOND_DEFAULT_VALUE = "";
        public override string Title => "Đề nghị thanh toán (thanh toán, tạm ứng)";
        public override string Description => "Đề nghị thanh toán (thanh toán, tạm ứng)";

        public NhTtThanhToanModel NhTtThanhToanModel { get; set; }

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

        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public ForexDeNghiThanhToanPrintDialogViewModel(INhTtThanhToanService iNhTtThanhToanService,
            ISessionService sessionService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            IExportService exportService,
            IDmChuDauTuService dmChuDauTuService,
            INsDonViService nsDonViService,
            INhDmNhaThauService nhDmNhaThauService,
            INhDmNhaThauNguoiNhanService nhDmNhaThauNguoiNhanService,
            INhDmNhaThauNganHangService nhDmNhaThauNganHangService,
            INhTtThanhToanChiTietService nhTtThanhToanChiTietService,
            INhDaHopDongService nhDaHopDongService,
            IMucLucNganSachService mucLucNganSachService,
            ILog logger,
            IMapper mapper)
        {
            _iNhTtThanhToanService = iNhTtThanhToanService;
            _sessionService = sessionService;
            _dmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _exportService = exportService;
            _dmChuDauTuService = dmChuDauTuService;
            _nsDonViService = nsDonViService;
            _iNhDmNhaThauService = nhDmNhaThauService;
            _iNhDmNhaThauNguoiNhanService = nhDmNhaThauNguoiNhanService;
            _nhDmNhaThauNganHangService = nhDmNhaThauNganHangService;
            _iNhTtThanhToanChiTietService = nhTtThanhToanChiTietService;
            _iNhDaHopDongService = nhDaHopDongService;
            _iMucLucNganSachService = mucLucNganSachService;
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
                    
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    string CapTren = NSConstants.BO_QUOC_PHONG;
                    string sCapKinhPhi = "Cấp kinh phí";
                    string sThanhToan = "Thanh toán";
                    string sTamUng = "Tạm ứng";
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, "0", isPrintPDF ? ExportType.PDF : ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("CapTren", CapTren);
                    data.Add("Donvi", NhTtThanhToanModel.sTenDonVi);
                    data.Add("NgayDeNghi", NhTtThanhToanModel.DNgayDeNghi?.Day);
                    data.Add("ThangDeNghi", NhTtThanhToanModel.DNgayDeNghi?.Month);
                    data.Add("NamDeNghi", NhTtThanhToanModel.DNgayDeNghi?.Year);
                    data.Add("iLoaiDeNghi", NhTtThanhToanModel.ILoaiDeNghi);
                    data.Add("sCapKinhPhi", sCapKinhPhi);
                    data.Add("sTamUng", sTamUng);
                    data.Add("sThanhToan", sThanhToan);
                    data.Add("TieuDe", TxtTitleFirst);
                    data.Add("Kinhgui", NhTtThanhToanModel.SKinhGui);
                    data.Add("TenChuongTrinh", NhTtThanhToanModel.STenNhiemVuChi);
                    data.Add("ChuDauTu", NhTtThanhToanModel.sTenChuDauTu);
                    data.Add("CanCu", NhTtThanhToanModel.SCanCu);
                    data.Add("SoDuTamUng", NhTtThanhToanModel.FSoDuTamUng);
                    data.Add("NamKeHoach", NhTtThanhToanModel.INamKeHoach);
                    data.Add("TenNguonVon", NhTtThanhToanModel.TenNguonVon);
                    data.Add("TongDeNghiBangChu", NhTtThanhToanModel.STongDeNghiBangChu);
                    data.Add("ThuHoiTamUngBangSo", NhTtThanhToanModel.FThuHoiTamUngBangSo);
                    data.Add("ThuHoiTamUngBangChu", NhTtThanhToanModel.FThuHoiTamUngBangChu);
                    data.Add("TraDonViThuHuongBangSo", NhTtThanhToanModel.FTraDonViThuHuongBangSo);
                    data.Add("TraDonViThuHuongBangChu", NhTtThanhToanModel.FTraDonViThuHuongBangChu);
                    data.Add("SoDeNghi", NhTtThanhToanModel.SSoDeNghi);

                    NhDmNhaThau nhaThau = null;
                    if (NhTtThanhToanModel.IIdNhaThauId.HasValue)
                    {
                        nhaThau = _iNhDmNhaThauService.FindById(NhTtThanhToanModel.IIdNhaThauId.Value);
                    }
                    data.Add("TenDonViThuHuong", nhaThau?.STenNhaThau);

                    NhDmNhaThauNganHang nhaThauNganHang = null;
                    if (NhTtThanhToanModel.IIdNhaThauNganHangId.HasValue)
                    {
                        nhaThauNganHang = _nhDmNhaThauNganHangService.FindById(NhTtThanhToanModel.IIdNhaThauNganHangId.Value);
                    }
                    data.Add("SoTaiKhoan", NhTtThanhToanModel.SSoTaiKhoan);
                    data.Add("ChiNhanhNganHang", NhTtThanhToanModel.SNganHang);

                    data.Add("ChuyenKhoanBangSo", NhTtThanhToanModel.FChuyenKhoanBangSo);
                    data.Add("ChuyenKhoanBangChu", NhTtThanhToanModel.SChuyenKhoanBangChu);
                    data.Add("TienMatBangSo", NhTtThanhToanModel.FTienMatBangSo);
                    data.Add("TienMatBangChu", NhTtThanhToanModel.STienMatBangChu);

                    NhDmNhaThauNguoiNhan nhaThauNguoiNhan = null;
                    if (NhTtThanhToanModel.IIdNhaThauNguoiNhanId.HasValue)
                    {
                        nhaThauNguoiNhan = _iNhDmNhaThauNguoiNhanService.FindById(NhTtThanhToanModel.IIdNhaThauNguoiNhanId.Value);
                    }
                    data.Add("TenNguoiNhan", NhTtThanhToanModel.SNguoiLienHe);
                    data.Add("SoCMND", NhTtThanhToanModel.SSoCmnd);
                    data.Add("NoiCap", NhTtThanhToanModel.SNoiCapCmnd);
                    data.Add("NgayCap", NhTtThanhToanModel.DNgayCapCmnd?.ToString("dd/MM/yyyy"));

                    var listNhTtChiTietItems = new ObservableCollection<NhTtThanhToanChiTietModel>();
                    var listNhTtChiTiet = _iNhTtThanhToanChiTietService.FindByCondition(x => x.IIdDeNghiThanhToanId.Equals(NhTtThanhToanModel.Id));
                    if (listNhTtChiTiet != null && listNhTtChiTiet.Any())
                    {
                        var daHopDong = _iNhDaHopDongService.FindById(NhTtThanhToanModel.IIdHopDongId ?? Guid.Empty);
                        listNhTtChiTietItems = _mapper.Map<ObservableCollection<NhTtThanhToanChiTietModel>>(listNhTtChiTiet);
                        foreach (var it in listNhTtChiTietItems)
                        {
                            it.FDuocDuyetUsd = daHopDong != null ? daHopDong.FGiaTriUsd : 0;
                            it.FDuocDuyetVnd = daHopDong != null ? daHopDong.FGiaTriVnd : 0;
                            it.FDuocDuyetEur = daHopDong != null ? daHopDong.FGiaTriEur : 0;
                            it.FDuocDuyetNgoaiTeKhac = daHopDong != null ? daHopDong.FGiaTriNgoaiTeKhac : 0;
                            var muclucngansach = _iMucLucNganSachService.GetAll().Where(x => x.Id == it.IIdMucLucNganSachId).FirstOrDefault();
                            if (muclucngansach != null)
                            {
                                it.sMucLucNganSach = muclucngansach.Lns + '-' + muclucngansach.L + '-' + muclucngansach.K + '-' + muclucngansach.M + '-' + muclucngansach.Tm;
                            }

                        }
                        //SetGiaTriKinhPhiKyTruoc(daHopDong, listNhTtChiTietItems.ToList());
                        LayThongTinLuyKe(NhTtThanhToanModel, listNhTtChiTietItems.ToList());
                    }
                    data.Add("Items", listNhTtChiTietItems.ToList());
                    data.Add("FDuocDuyetUsdTotal", listNhTtChiTietItems.Sum(x => x.FDuocDuyetUsd));
                    data.Add("FDuocDuyetVndTotal", listNhTtChiTietItems.Sum(x => x.FDuocDuyetVnd));
                    data.Add("FDuocCapKyTruocUsdTotal", listNhTtChiTietItems.Sum(x => x.FDuocCapKyTruocUsd));
                    data.Add("FDuocCapKyTruocVndTotal", listNhTtChiTietItems.Sum(x => x.FDuocCapKyTruocVnd));
                    data.Add("FDeNghiCapKyNayUsdTotal", listNhTtChiTietItems.Sum(x => x.FDeNghiCapKyNayUsd));
                    data.Add("FDeNghiCapKyNayVndTotal", listNhTtChiTietItems.Sum(x => x.FDeNghiCapKyNayVnd));

                    AddChuKy(data, _typeChuky);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_NH_CP_DNTT, ExportFileName.RPT_NH_CAPPHAT_DENGHITHANHTOAN);
                    string fileNamePrefix = string.Format("rptNh_CapPhat_DeNghiThanhToan_{0}", NhTtThanhToanModel.SSoDeNghi);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<NhTtThanhToanChiTietModel>(templateFileName, data);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
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

        public void SetGiaTriKinhPhiKyTruoc(NhDaHopDong daHopDong, List<NhTtThanhToanChiTietModel> listTtThanhToanChiTietModel)
        {
            var lstPheDuyetThanhToan = GetListPheDuyetThanhToan(daHopDong);
            var lstIdPheDuyet = lstPheDuyetThanhToan.Select(x => x.Id).ToList();
            var predicate = PredicateBuilder.True<NhTtThanhToanChiTiet>();
            predicate = predicate.And(x => x.IIdDeNghiThanhToanId.HasValue && lstIdPheDuyet.Contains(x.IIdDeNghiThanhToanId.Value));
            var results = _iNhTtThanhToanChiTietService.FindByCondition(predicate).ToList();
            var lstThanhToanChiTietModel = _mapper.Map<List<NhTtThanhToanChiTietModel>>(results);
            if (listTtThanhToanChiTietModel != null)
            {
                foreach (var it in listTtThanhToanChiTietModel)
                {
                    it.FDuocCapKyTruocUsd = lstThanhToanChiTietModel.Sum(x => x.FPheDuyetCapKyNayUsd.GetValueOrDefault());
                    it.FDuocCapKyTruocVnd = lstThanhToanChiTietModel.Sum(x => x.FPheDuyetCapKyNayVnd.GetValueOrDefault());
                    it.FDuocCapKyTruocEur = lstThanhToanChiTietModel.Sum(x => x.FPheDuyetCapKyNayEur.GetValueOrDefault());
                    it.FDuocCapKyTruocNgoaiTeKhac = lstThanhToanChiTietModel.Sum(x => x.FPheDuyetCapKyNayNgoaiTeKhac.GetValueOrDefault());
                }
                OnPropertyChanged(nameof(listTtThanhToanChiTietModel));
            }
        }

        public void LayThongTinLuyKe(NhTtThanhToanModel thanhtoanmodel, List<NhTtThanhToanChiTietModel> listTtThanhToanChiTietModel)
        {
            var predicate = PredicateBuilder.True<NhTtThanhToan>();
            predicate = predicate.And(x => x.IIdDonVi == thanhtoanmodel.IIdDonVi);
            predicate = predicate.And(x => x.IIdNhiemVuChiId == thanhtoanmodel.IIdNhiemVuChiId);
            predicate = predicate.And(x => x.IIdChuDauTuId == thanhtoanmodel.IIdChuDauTuId);

            var lstthanhtoan = _iNhTtThanhToanService.FindByCondition(predicate).ToList();
            var querythanhtoan = from tt in lstthanhtoan
                                 where (tt.ILoaiDeNghi == 1 && tt.ICoQuanThanhToan == 2) || (tt.ILoaiDeNghi == 2 && tt.ICoQuanThanhToan == 1) || (tt.ILoaiDeNghi == 3 && tt.ICoQuanThanhToan == 1) && tt.ITrangThai == 2
                                 select tt;

            double? fLuyKeUSD = 0;
            double? fLuyKeVND = 0;
            double? fLuyKeEUR = 0;
            double? fLuyKeKhac = 0;

            foreach (var item in querythanhtoan)
            {
                var predicate_chitiet = PredicateBuilder.True<NhTtThanhToanChiTiet>();
                predicate_chitiet = predicate_chitiet.And(x => x.IIdDeNghiThanhToanId == item.Id);
                var lstchitiet = _iNhTtThanhToanChiTietService.FindByCondition(predicate_chitiet).ToList();
                fLuyKeUSD = fLuyKeUSD + lstchitiet.Sum(x => x.FPheDuyetCapKyNayUsd);
                fLuyKeVND = fLuyKeVND + lstchitiet.Sum(x => x.FPheDuyetCapKyNayVnd);
                fLuyKeEUR = fLuyKeEUR + lstchitiet.Sum(x => x.FPheDuyetCapKyNayEur);
                fLuyKeKhac = fLuyKeKhac + lstchitiet.Sum(x => x.FPheDuyetCapKyNayNgoaiTeKhac);
            }
            if (listTtThanhToanChiTietModel != null)
            {
                foreach (var chitiet in listTtThanhToanChiTietModel)
                {
                    chitiet.FDuocCapKyTruocUsd = fLuyKeUSD;
                    chitiet.FDuocCapKyTruocVnd = fLuyKeVND;
                    chitiet.FDuocCapKyTruocEur = fLuyKeEUR;
                    chitiet.FDuocCapKyTruocNgoaiTeKhac = fLuyKeKhac;
                }
                OnPropertyChanged(nameof(listTtThanhToanChiTietModel));
            }    
            
        }

        public List<NhTtThanhToanModel> GetListPheDuyetThanhToan(NhDaHopDong daHopDong)
        {
            if (daHopDong != null)
            {
                var predicate = PredicateBuilder.True<NhTtThanhToan>();
                predicate = predicate.And(x => x.ITrangThai == (int)NhLoaiThanhToan.Type.PHE_DUYET);
                predicate = predicate.And(x => x.IIdHopDongId.Equals(daHopDong.Id));
                var results = _iNhTtThanhToanService.FindByCondition(predicate).ToList();
                return _mapper.Map<List<NhTtThanhToanModel>>(results);
            }
            return new List<NhTtThanhToanModel>();
        }

        private void AddChuKy(Dictionary<string, object> data, string idType)
        {
            // add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1",dmChyKy != null ? dmChyKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2",dmChyKy != null ? dmChyKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ChucDanh1",dmChyKy != null ? dmChyKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2",dmChyKy != null ? dmChyKy.ChucDanh2MoTa : string.Empty);
            data.Add("Ten1", dmChyKy != null ? dmChyKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", dmChyKy != null ? dmChyKy.Ten2MoTa : string.Empty);
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
    }
}
