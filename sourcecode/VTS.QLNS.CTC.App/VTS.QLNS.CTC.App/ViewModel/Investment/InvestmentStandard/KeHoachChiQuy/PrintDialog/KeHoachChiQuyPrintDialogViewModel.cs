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
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachChiQuy.PrintDialog
{
    public class KeHoachChiQuyPrintDialogViewModel : ViewModelBase
    {
        private readonly IVdtNcNhuCauChiService _service;
        private readonly ILog _logger;
        private readonly DmChuKyDialogViewModel _dmChuKyDialogViewModel;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _nsDonViService;
        private readonly IMapper _mapper;
        private IExportService _exportService;
        private string _diaDiem;
        private readonly string _typeChuky = TypeChuKy.RPT_VDT_THUCHIENDAUTU_KEHOACHCHIQUY;
        private readonly string TITLE_FIRST_DEFAULT_VALUE = "Báo cáo kế hoạch chi quý {0} năm {1}";
        private readonly string TITLE_SECOND_DEFAULT_VALUE = "Nguồn vốn {0}";
        public override string Title => "Báo cáo kế hoạch chi quý";
        public override string Description => "Báo cáo kế hoạch chi quý";

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

        public List<VdtNcNhuCauChiModel> VdtNcNhuCauChiModels { get; set; }

        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public KeHoachChiQuyPrintDialogViewModel(IVdtNcNhuCauChiService service,
            IExportService exportService,
            ISessionService sessionService,
            IDanhMucService danhMucService,
            IMapper mapper,
            IDmChuKyService dmChuKyService,
            INsDonViService nsDonViService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            ILog logger)
        {
            _service = service;
            _exportService = exportService;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _nsDonViService = nsDonViService;
            _dmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _mapper = mapper;
            _logger = logger;
            PrintReportCommand = new RelayCommand(obj => OnPrintReport(ExportType.PDF));
            ExportCommand = new RelayCommand(obj => OnPrintReport(ExportType.EXCEL));
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
            TxtTitleFirst = TITLE_FIRST_DEFAULT_VALUE;
            TxtTitleSecond = TITLE_SECOND_DEFAULT_VALUE;
        }

        private void OnPrintReport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    foreach (var item in VdtNcNhuCauChiModels)
                    {
                        var lstData = _mapper.Map<List<VdtNcNhuCauChiChiTietModel>>(_service.GetNhuCauChiDetail(item.iID_MaDonViQuanLy, item.iNamKeHoach.Value, item.iID_NguonVonID.Value, item.iQuy,donViTinh));
                        lstData = lstData.Select(n => { n.iStt = lstData.IndexOf(n) + 1; return n; }).ToList();
                        var detailData = _service.GetDetailByParent(item.Id);
                        double tongtien = 0;

                        if (detailData != null)
                        {
                            foreach (var child in detailData)
                            {
                                var currentData = lstData.FirstOrDefault(n => n.iID_DuAnId == child.IIdDuAnId && n.sLoaiThanhToan == child.SLoaiThanhToan);
                                if (currentData != null)
                                {
                                    currentData.fGiaTriDeNghi = child.FGiaTriDeNghi/donViTinh ?? 0;
                                    currentData.sGhiChu = child.SGhiChu;
                                    tongtien += child.FGiaTriDeNghi ?? 0;
                                }
                            }
                        }
                    
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        string CapTren = NSConstants.BO_QUOC_PHONG;
                        DonVi donvi = _nsDonViService.FindByIdDonVi(item.iID_MaDonViQuanLy, _sessionService.Current.YearOfWork);
                        if (!"0".Equals(donvi?.Loai))
                        {
                            DonVi donViCapTren = _nsDonViService.FindByLoai("0", _sessionService.Current.YearOfWork);
                            CapTren = donViCapTren?.TenDonVi;
                        }
                        data.Add("CapTren", CapTren);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                        data.Add("sTenDonVi", item.sTenDonVi == null ? string.Empty : item.sTenDonVi.ToUpper());
                        data.Add("iQuy", item.iQuy);
                        data.Add("iNam", item.iNamKeHoach);
                        data.Add("sTenNguonVon", item.sTenNguonVon);
                        data.Add("sNgayThangNam", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")));
                        data.Add("Items", lstData);
                        data.Add("TongTienBangChu", tongtien * donViTinh);
                        data.Add("Header1", CatUnitTypeSelected != null ? CatUnitTypeSelected.DisplayItem : string.Empty);
                        data.Add("ThoiGian", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                        AddChuKy(data, _typeChuky);
                        data.Add("TieuDe1", string.Format(TxtTitleFirst, item.iQuy, item.iNamKeHoach));
                        data.Add("TieuDe2", string.Format(TxtTitleSecond, item.sTenNguonVon));


                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHCQ, ExportFileName.RPT_VDT_NC_NHUCAUCHI);
                        string fileNamePrefix = ExportFileName.RPT_VDT_NC_NHUCAUCHI;
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<VdtNcNhuCauChiChiTietModel>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
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
    }
}
