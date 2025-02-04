using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Ink;
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
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ThongTriCapPhat.PrintDialog
{
    public class ThongTriCapPhatPrintDialogViewModel : ViewModelBase
    {
        private IMapper _mapper;
        private readonly IVdtDmLoaiCongTrinhService _loaicongtrinhService;
        private readonly ISessionService _sessionService;
        private readonly IVdtThongTriService _thongTriService;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly DmChuKyDialogViewModel _dmChuKyDialogViewModel;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly ILog _logger;
        private readonly string _typeChuky = TypeChuKy.RPT_VDT_THUCHIENDAUTU_THONGTRITHANHTOAN;
        private string _diaDiem;
        private Dictionary<Guid, VdtDmLoaiCongTrinh> _dicLoaiCongTrinh;
        private readonly string TITLE_FIRST_DEFAULT_VALUE = "THÔNG TRI";
        private readonly string TITLE_SECOND_DEFAULT_VALUE = "Cấp thanh toán kinh phí XDCB";

        public override string Title => "Thông tri thanh toán (thanh toán, tạm ứng)";
        public override string Description => "Thông tri thanh toán (thanh toán, tạm ứng)";

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
        
        private string _txtDescription;
        public string TxtDescription
        {
            get => _txtDescription;
            set => SetProperty(ref _txtDescription, value);
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

        public List<VdtThongTriModel> VdtThongTriModels { get; set; }

        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public ThongTriCapPhatPrintDialogViewModel(IMapper mapper,
            ISessionService sessionService,
            IVdtThongTriService thongTriService,
            INsDonViService nsDonViService,
            IExportService exportService,
            IDanhMucService danhMucService,
            IVdtDmLoaiCongTrinhService loaicongtrinhService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IDmChuKyService dmChuKyService,
            ILog logger)
        {
            _dmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _dmChuKyService = dmChuKyService;
            _logger = logger;
            _thongTriService = thongTriService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _danhMucService = danhMucService;
            _exportService = exportService;
            _loaicongtrinhService = loaicongtrinhService;
            _mapper = mapper;
            PrintReportCommand = new RelayCommand(obj => ExporThongTriThanhToan(VdtThongTriModels, ExportType.PDF));
            ExportCommand = new RelayCommand(obj => ExporThongTriThanhToan(VdtThongTriModels, ExportType.EXCEL));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            LoadLoaiCongTrinh();
            LoadCatUnitTypes();
            LoadTitleFirst();
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void ExporThongTriThanhToan(List<VdtThongTriModel> items, ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    foreach (VdtThongTriModel item in items)
                    {
                        string templateFileName = string.Empty;
                        string fileNamePrefix = string.Empty;

                        // các loại thông tri chi tiết cấp thanh toán
                        List<string> typesOfThanhToan = new List<string>();
                        typesOfThanhToan.Add(KieuThongTri.TT_KPQP);
                        typesOfThanhToan.Add(KieuThongTri.TT_Cap_KPK);
                        typesOfThanhToan.Add(KieuThongTri.TT_Cap_KPNN);
                        // các loại thông tri chi tiết cấp tạm ứng
                        List<string> typesOfTamUng = new List<string>();
                        typesOfTamUng.Add(KieuThongTri.TT_ThuUng_KPQP);
                        typesOfTamUng.Add(KieuThongTri.TT_ThuUng_KPNN);
                        typesOfTamUng.Add(KieuThongTri.TT_ThuUng_KPK);

                        var lstThongChiChiTiet = _thongTriService.GetVdtThongTriChiTietByParentId(item.Id.Value);
                        //var lstThongChiChiTiet = _thongTriService.FindByIdThongTri(item.Id.Value);
                        if (lstThongChiChiTiet == null) return;
                        var items = _mapper.Map<List<VdtThongTriChiTietModel>>(lstThongChiChiTiet);
                        double tongTien = 0;
                        var sTenDuAn = items.Any() && items.Count == 1 ? string.Join(StringUtils.COMMA, items.Select(s => s.STenDuAn)) : string.Empty;
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(int.Parse(CatUnitTypeSelected.ValueItem), exportType);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("Nam", DateTime.Now.Year.ToString());
                        data.Add("DonVi", item.sTenDonVi);
                        data.Add("Ve", string.Format("Tháng {0} năm {1}", DateTime.Now.Month, DateTime.Now.Year));
                        data.Add("NoiDung", "");
                        if (lstThongChiChiTiet != null && lstThongChiChiTiet.Count() > 0)
                        {
                            var dataExport = ConvertDataExport(lstThongChiChiTiet, ref tongTien, item.ILoaiThongTri);
                            var countData = dataExport.Count();
                            // Nếu dữ liệu < 10 chi tiết thì thêm cho đủ 10 chi tiết
                            if (countData < 10)
                            {

                                for (int i = 0; i < 10 - countData; i++)
                                {
                                    dataExport.Add(new VdtThongTriChiTietModel());
                                }
                            }
                            data.Add("Items", dataExport);
                        }
                        else
                        {
                            var dataExport = ConvertDataExport(_thongTriService.GetVdtThongTriChiTiet((item.Id ?? Guid.Empty), item.iID_MaDonViID, item.ILoaiThongTri, item.iNamThongTri, item.dNgayThongTri.Value,
                            item.sMaNguonVon, item.dNgayLapGanNhat).ToList(), ref tongTien, item.ILoaiThongTri);
                            var countData = dataExport.Count();

                            // Nếu dữ liệu < 10 chi tiết thì thêm cho đủ 10 chi tiết
                            if (countData < 10)
                            {
                                for (int i = 0; i < 10 - countData; i++)
                                {
                                    dataExport.Add(new VdtThongTriChiTietModel());
                                }
                            }

                            data.Add("Items", dataExport);
                        }
                        data.Add("TongChiTieu", tongTien);
                        data.Add("TienBangChu", StringUtils.NumberToText(tongTien, true));
                        AddChuKy(data, _typeChuky);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("Mota", item.sMoTa);
                        data.Add("sMaThongTri", item.sMaThongTri);
                        data.Add("sTenDuAn", sTenDuAn);
                        switch (item.ILoaiThongTri)
                        {
                            case (int)LoaiThongTriEnum.Type.CAP_THANH_TOAN:
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTTT, ExportFileName.RPT_VDT_THONGTRI_THANHTOAN);
                                fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_THONGTRI_THANHTOAN, item.sMaThongTri);
                                break;
                            case (int)LoaiThongTriEnum.Type.CAP_TAM_UNG:
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTTT, ExportFileName.RPT_VDT_THONGTRI_TAMUNG);
                                fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_THONGTRI_TAMUNG, item.sMaThongTri);
                                break;
                            case (int)LoaiThongTriEnum.Type.CAP_KINH_PHI:
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTTT, ExportFileName.RPT_VDT_THONGTRI_KINHPHI);
                                fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_THONGTRI_KINHPHI, item.sMaThongTri);
                                break;
                            case (int)LoaiThongTriEnum.Type.CAP_HOP_THUC:
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTTT, ExportFileName.RPT_VDT_THONGTRI_KINHPHI);
                                fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_THONGTRI_HOPTHUC, item.sMaThongTri);
                                break;
                        }

                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<VdtThongTriChiTietModel>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));

                        if (item.ILoaiThongTri == (int)LoaiThongTriEnum.Type.CAP_THANH_TOAN)
                        {
                            data = new Dictionary<string, object>();
                            data.Add("FormatNumber", formatNumber);
                            data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                            data.Add("Cap2", GetHeader2Report());
                            data.Add("Nam", DateTime.Now.Year.ToString());
                            data.Add("DonVi", item.sTenDonVi);
                            data.Add("Ve", string.Format("Tháng {0} năm {1}", DateTime.Now.Month, DateTime.Now.Year));
                            data.Add("NoiDung", "");
                            if (lstThongChiChiTiet != null && lstThongChiChiTiet.Count() > 0)
                            {
                                List<VdtThongTriChiTietModel> listThongTriChiTietModels =
                                    ConvertDataExport(lstThongChiChiTiet.Where(i => typesOfTamUng.Contains(i.SMaKieuThongTri)), ref tongTien, item.ILoaiThongTri);
                                var countData = listThongTriChiTietModels.Count();
                                // Nếu dữ liệu < 10 chi tiết thì thêm cho đủ 10 chi tiết
                                if (countData < 10)
                                {

                                    for (int i = 0; i < 10 - countData; i++)
                                    {
                                        listThongTriChiTietModels.Add(new VdtThongTriChiTietModel());
                                    }
                                }
                                data.Add("Items", listThongTriChiTietModels);
                            }
                            else
                            {
                                var dataExports = ConvertDataExport(_thongTriService.GetVdtThongTriChiTiet((item.Id ?? Guid.Empty), item.iID_MaDonViID, item.ILoaiThongTri, item.iNamThongTri, item.dNgayThongTri.Value,
                                item.sMaNguonVon, item.dNgayLapGanNhat).ToList(), ref tongTien, item.ILoaiThongTri);
                                var countData = dataExports.Count();
                                // Nếu dữ liệu < 10 chi tiết thì thêm cho đủ 10 chi tiết
                                if (countData < 10)
                                {

                                    for (int i = 0; i < 10 - countData; i++)
                                    {
                                        dataExports.Add(new VdtThongTriChiTietModel());
                                    }
                                }
                                data.Add("Items", dataExports);
                            }
                            data.Add("TongChiTieu", tongTien);
                            data.Add("TienBangChu", StringUtils.NumberToText(tongTien, true));
                            AddChuKy(data, _typeChuky);
                            data.Add("TieuDe1", TxtTitleFirst);
                            data.Add("Mota", item.sMoTa);
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTTT, ExportFileName.RPT_VDT_THONGTRI_THUHOIUNG);
                            fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_THONGTRI_THUHOIUNG, item.sMaThongTri);
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            xlsFile = _exportService.Export<VdtThongTriChiTietModel>(templateFileName, data);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
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

        public string GetHeader2Report()
        {
            DonVi donViParent = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
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

        private void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add ngày địa điểm
            data.Add("DonViTinh", string.Format("Đơn vị tính : {0}", CatUnitTypeSelected.DisplayItem));
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
            // DmChuKy _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TxtTitleFirst = TITLE_FIRST_DEFAULT_VALUE;
            TxtTitleSecond = TITLE_SECOND_DEFAULT_VALUE;
        }

        private List<VdtThongTriChiTietModel> ConvertDataExport(IEnumerable<VdtThongTriChiTietQuery> datas, ref double fTongTien, int iLoaiThanhToan, bool bIsThanhToan = true)
        {
            fTongTien = 0;
            int iDonViTinh = int.Parse(CatUnitTypeSelected.ValueItem);
            if (datas == null) return new List<VdtThongTriChiTietModel>();
            List<VdtThongTriChiTietModel> results = new List<VdtThongTriChiTietModel>();

            /*if (iLoaiThanhToan == (int)LoaiThongTriEnum.Type.CAP_THANH_TOAN)
            {
                List<string> lstKieuThongTriThanhToan = new List<string>() { KieuThongTri.TT_KPQP, KieuThongTri.TT_Cap_KPK, KieuThongTri.TT_Cap_KPNN };
                List<string> lstKieuThongTriThuHoi = new List<string>() { KieuThongTri.TT_ThuUng_KPQP, KieuThongTri.TT_ThuUng_KPNN, KieuThongTri.TT_ThuUng_KPK };
                if (bIsThanhToan)
                {
                    datas = datas.Where(n => lstKieuThongTriThanhToan.Contains(n.SMaKieuThongTri));
                }
                else
                {
                    datas = datas.Where(n => lstKieuThongTriThuHoi.Contains(n.SMaKieuThongTri));
                }
            }*/

            fTongTien = datas.Sum(n => n.FSoTien) / iDonViTinh;
            int asciiIndex = 65;

            foreach (var item in datas.GroupBy(n => n.IIdLoaiCongTrinhId).OrderBy(n => n.Key).Select(n => n.Key))
            {
                var lstChild = datas.Where(n => n.IIdLoaiCongTrinhId == item);
                if (item.HasValue)
                {
                    string sTenLoaiCongTrinh = _dicLoaiCongTrinh.ContainsKey(item.Value) ? _dicLoaiCongTrinh[item.Value].STenLoaiCongTrinh : string.Empty;
                    results.Add(new VdtThongTriChiTietModel()
                    {
                        FSoTien = lstChild.Sum(n => n.FSoTien),
                        IsHangCha = true,
                        STenDuAn = string.Format("{0}. {1}", (char)asciiIndex, sTenLoaiCongTrinh)
                    });
                    asciiIndex++;
                }
                if (lstChild != null)
                    results.AddRange(_mapper.Map<List<VdtThongTriChiTietModel>>(lstChild));
            }
            return results.Select(n =>
            {
                n.FSoTien = n.FSoTien / iDonViTinh;
                n.SDonViThuHuong = String.IsNullOrEmpty(n.SDonViThuHuong) ? "" : String.Concat(n.SDonViThuHuong, " : ");
                return n;
            }).ToList();
        }

        private void LoadLoaiCongTrinh()
        {
            _dicLoaiCongTrinh = new Dictionary<Guid, VdtDmLoaiCongTrinh>();
            var lstLoaiCongTrinh = _loaicongtrinhService.FindAll();
            if (lstLoaiCongTrinh == null) return;
            _dicLoaiCongTrinh = lstLoaiCongTrinh.ToDictionary(n => n.IIdLoaiCongTrinh, n => n);
        }
    }

}
