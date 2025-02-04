using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.QuyetToanVDT.PrintDialog
{
    public class QuyetToanVDTPrintDialogViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly DmChuKyDialogViewModel _dmChuKyDialogViewModel;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private readonly IMapper _mapper;
        private IExportService _exportService;
        private readonly IVdtQtBcQuyetToanNienDoService _service;
        private string _diaDiem;
        private readonly string _typeChuky = TypeChuKy.RPT_VDT_THUCHIENDAUTU_QTND;
        public override string Title => "Báo cáo quyết toán niên độ";
        public override string Description => "Báo cáo quyết toán niên độ";

        public List<VdtQtBcquyetToanNienDoModel> VdtQtBcquyetToanNienDoModels { get; set; }

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

        private string _txtTitleThird;
        public string TxtTitleThird
        {
            get => _txtTitleThird;
            set => SetProperty(ref _txtTitleThird, value);
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

        public QuyetToanVDTPrintDialogViewModel(ISessionService sessionService,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IDmChuKyService dmChuKyService,
            IDanhMucService danhMucService,
            IVdtQtBcQuyetToanNienDoService vdtQtBcQuyetToanNienDoService,
            IMapper mapper,
            IExportService exportService)
        {
            _sessionService = sessionService;
            _logger = logger;
            _dmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _dmChuKyService = dmChuKyService;
            _danhMucService = danhMucService;
            _service = vdtQtBcQuyetToanNienDoService;
            _mapper = mapper;
            _exportService = exportService;
            PrintReportCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ExportCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
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
                TxtTitleFirst = chuKy != null ? chuKy.TieuDe1MoTa : string.Empty;
                TxtTitleSecond = chuKy != null ? chuKy.TieuDe2MoTa : string.Empty;
                TxtTitleThird = chuKy != null ? chuKy.TieuDe3MoTa : string.Empty;
            };
            _dmChuKyDialogViewModel.Init();
            _dmChuKyDialogViewModel.ShowDialog();
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    var _listExportResult = new List<ExportResult>();
                    foreach (var item in VdtQtBcquyetToanNienDoModels)
                    {
                        switch (item.ILoaiThanhToan)
                        {
                            case (int)PaymentTypeEnum.Type.THANH_TOAN:
                                _listExportResult.Add(ExportQuyetToanVonNam(item, exportType));
                                break;
                            case (int)PaymentTypeEnum.Type.TAM_UNG:
                                _listExportResult.Add(ExportQuyetToanVonUng(item, exportType));
                                break;
                        }
                    }
                    e.Result = _listExportResult;
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

        private ExportResult ExportQuyetToanVonNam(VdtQtBcquyetToanNienDoModel item, ExportType exportType)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
            data.Add("FormatNumber", formatNumber);
            data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
            data.Add("TieuDe1", string.Format(TxtTitleFirst, item.INamKeHoach));
            data.Add("TieuDe2", string.Format(TxtTitleSecond, item.INamKeHoach));
            data.Add("TieuDe3", string.Format(TxtTitleThird, item.INamKeHoach));
            data.Add("sTenDonVi", item.STenDonVi);
            data.Add("dNgayHienTai", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")));
            data.Add("iNam", item.INamKeHoach);
            data.Add("Items", GetDataExportQuyetToanVonNam(item));
            AddChuKy(data, _typeChuky);

            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QT, ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONNAM);
            string fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONNAM, item.SSoDeNghi);
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ExportVdtQtBcquyetToanNienDoChiTiet1Model>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        private List<ExportVdtQtBcquyetToanNienDoChiTiet1Model> GetDataExportQuyetToanVonNam(VdtQtBcquyetToanNienDoModel item)
        {
            Dictionary<Guid, VdtQtBcQuyetToanNienDoChiTiet01> dicDataDetail = new Dictionary<Guid, VdtQtBcQuyetToanNienDoChiTiet01>();
            List<VdtQtBcquyetToanNienDoChiTiet1Query> data = new List<VdtQtBcquyetToanNienDoChiTiet1Query>();
            var defaultData = _service.GetQuyetToanNienDoVonNamByParentId(item.Id);
            if (defaultData == null || defaultData.Count == 0)
            {
                defaultData = _service.GetDeNghiQuyetToanNienDoDetail(item.IIDMaDonViQuanLy, item.INamKeHoach ?? 0, item.IIDNguonVonID ?? 0);
            }
            var results = SetupViewData(defaultData);
            int i = 0;
            foreach (var child in results)
            {
                if (child.IsHangCha)
                {
                    i = 0;
                    continue;
                }
                i++;
                child.iStt = i.ToString();
                child.FVonConLaiHuyBoKeoDaiNamNay = child.FKHVNamTruocChuyenNamNay - child.FTongThanhToanVonKeoDaiNamNay - child.FGiaTriNamTruocChuyenNamSau;
                child.FVonConLaiHuyBoNamNay = child.FKHVNamNay - child.FTongKeHoachThanhToanVonNamNay - child.FGiaTriNamNayChuyenNamSau;
                child.FLuyKeTamUngChuaThuHoiChuyenSangNam =
                    child.FTamUngTheoCheDoChuaThuHoiNamTruoc - child.FGiaTriTamUngDieuChinhGiam - child.FTamUngNamTruocThuHoiNamNay
                    + child.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay + child.FTamUngTheoCheDoChuaThuHoiNamNay;
            }
            return _mapper.Map<List<ExportVdtQtBcquyetToanNienDoChiTiet1Model>>(results);
        }

        private ExportResult ExportQuyetToanVonUng(VdtQtBcquyetToanNienDoModel item, ExportType exportType)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
            data.Add("FormatNumber", formatNumber);
            data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
            data.Add("TieuDe1", string.Format(TxtTitleFirst, item.INamKeHoach));
            data.Add("TieuDe2", string.Format(TxtTitleSecond, item.INamKeHoach));
            data.Add("TieuDe3", string.Format(TxtTitleThird, item.INamKeHoach));
            data.Add("sTenDonVi", item.STenDonVi);
            data.Add("dNgayHienTai", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")));
            data.Add("iNam", item.INamKeHoach);
            data.Add("Items", GetDataExportQuyetToanVonUng(item));
            AddChuKy(data, _typeChuky);

            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QT, ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONUNG);
            string fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONUNG, item.SSoDeNghi);
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ExportBcquyetToanNienDoVonUngChiTietModel>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        private List<ExportBcquyetToanNienDoVonUngChiTietModel> GetDataExportQuyetToanVonUng(VdtQtBcquyetToanNienDoModel item)
        {
            Dictionary<Guid, VdtQtBcQuyetToanNienDoChiTiet01> dicDataDetail = new Dictionary<Guid, VdtQtBcQuyetToanNienDoChiTiet01>();
            List<BcquyetToanNienDoVonUngChiTietQuery> data = new List<BcquyetToanNienDoVonUngChiTietQuery>();
            var defaultData = _service.GetQuyetToanNienDoVonUngByParentId(item.Id);
            if (defaultData == null || defaultData.Count() == 0)
            {
                defaultData = _service.GetDeNghiQuyetToanNienDoVonUngDetail(item.IIDMaDonViQuanLy, item.INamKeHoach ?? 0, item.IIDNguonVonID ?? 0);
            }
            var results = SetupViewDataVonUng(defaultData);
            int i = 0;
            foreach (var child in results)
            {
                if (child.IsHangCha)
                {
                    i = 0;
                    continue;
                }
                i++;
                child.iStt = i.ToString();
            }
            return _mapper.Map<List<ExportBcquyetToanNienDoVonUngChiTietModel>>(results);
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
            DmChuKy _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TxtTitleFirst = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
            TxtTitleSecond = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
            TxtTitleThird = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
        }

        private List<VdtQtBcquyetToanNienDoChiTiet1Model> SetupViewData(List<VdtQtBcquyetToanNienDoChiTiet1Query> lstData)
        {
            List<VdtQtBcquyetToanNienDoChiTiet1Model> results = new List<VdtQtBcquyetToanNienDoChiTiet1Model>();
            if (lstData == null) return results;
            List<VdtQtBcquyetToanNienDoChiTiet1Model> dataConvert = _mapper.Map<List<VdtQtBcquyetToanNienDoChiTiet1Model>>(lstData);

            if (dataConvert.Any(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.CQTC))
            {
                results.Add(new VdtQtBcquyetToanNienDoChiTiet1Model()
                {
                    STenDuAn = "* CẤP QUA BỘ QUỐC PHÒNG",
                    iStt = "A",
                    IsHangCha = true
                });
                results.AddRange(dataConvert.Where(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.CQTC));
            }
            if (dataConvert.Any(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC))
            {
                results.Add(new VdtQtBcquyetToanNienDoChiTiet1Model()
                {
                    STenDuAn = "* CẤP QUA KHO BẠC",
                    iStt = "B",
                    IsHangCha = true
                });
                results.AddRange(dataConvert.Where(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC));
            }
            return results;
        }

        private List<BcquyetToanNienDoVonUngChiTietModel> SetupViewDataVonUng(List<BcquyetToanNienDoVonUngChiTietQuery> lstData)
        {
            List<BcquyetToanNienDoVonUngChiTietModel> results = new List<BcquyetToanNienDoVonUngChiTietModel>();
            if (lstData == null) return results;
            List<BcquyetToanNienDoVonUngChiTietModel> dataConvert = _mapper.Map<List<BcquyetToanNienDoVonUngChiTietModel>>(lstData);

            if (dataConvert.Any(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.CQTC))
            {
                results.Add(new BcquyetToanNienDoVonUngChiTietModel()
                {
                    STenDuAn = "* CẤP QUA BỘ QUỐC PHÒNG",
                    IsHangCha = true,
                    iStt = "A"
                });
                results.AddRange(dataConvert.Where(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.CQTC));
            }
            if (dataConvert.Any(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC))
            {
                results.Add(new BcquyetToanNienDoVonUngChiTietModel()
                {
                    STenDuAn = "* CẤP QUA KHO BẠC",
                    IsHangCha = true,
                    iStt = "B"
                });
                results.AddRange(dataConvert.Where(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC));
            }
            return results;
        }
    }
}
