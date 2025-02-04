using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PheDuyetThanhToan.PrintDialog
{
    public class PheDuyetThanhToanPrintDialogViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly IVdtTtPheDuyetThanhToanChiTietService _pheDuyetThanhToanChiTietService;
        private readonly IVdtTtDeNghiThanhToanService _deNghiThanhToanService;
        private readonly IExportService _exportService;
        private readonly DmChuKyDialogViewModel _dmChuKyDialogViewModel;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly string _typeChuky = TypeChuKy.RPT_VDT_THUCHIENDAUTU_PHEDUYETTHANHTOAN;
        private string _diaDiem;
        private readonly string TITLE_FIRST_DEFAULT_VALUE = "PHẦN GHI CỦA CƠ QUAN TÀI CHÍNH";
        private readonly string TITLE_SECOND_DEFAULT_VALUE = "";
        public override string Title => "Phần ghi của cơ quan tài chính (thanh toán, tạm ứng)";
        public override string Description => "Phần ghi của cơ quan tài chính (thanh toán, tạm ứng)";

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

        public List<VdtTtDeNghiThanhToanModel> VdtTtDeNghiThanhToanModels { get; set; }

        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand ExportCommand { get; }

        public PheDuyetThanhToanPrintDialogViewModel(ISessionService sessionService,
            IVdtTtPheDuyetThanhToanChiTietService pheDuyetThanhToanChiTietService,
            IExportService exportService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IVdtTtDeNghiThanhToanService deNghiThanhToanService,
            IDmChuKyService dmChuKyService,
            IDanhMucService danhMucService,
            IMapper mapper,
            ILog logger)
        {
            _sessionService = sessionService;
            _pheDuyetThanhToanChiTietService = pheDuyetThanhToanChiTietService;
            _exportService = exportService;
            _dmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _dmChuKyService = dmChuKyService;
            _danhMucService = danhMucService;
            _deNghiThanhToanService = deNghiThanhToanService;
            _mapper = mapper;
            _logger = logger;
            PrintReportCommand = new RelayCommand(obj => OnPrintReport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            ExportCommand = new RelayCommand(obj => OnPrintReport(ExportType.EXCEL));
        }

        public override void Init()
        {
            LoadCatUnitTypes();
            LoadTitleFirst();
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private List<VdtTtPheDuyetThanhToanChiTiet> GetListDataToPrintWithDonViTinh(List<VdtTtPheDuyetThanhToanChiTiet> listData)
        {
            List<VdtTtPheDuyetThanhToanChiTiet> listWithDVT = listData.Where(n => (!string.IsNullOrEmpty(n.M) || !string.IsNullOrEmpty(n.Tm) || !string.IsNullOrEmpty(n.Ttm) || !string.IsNullOrEmpty(n.Ng))).ToList();
            listWithDVT.ForEach(data =>
            {
                data.TongSoWithDonViTinh = data.TongSo / Int32.Parse(CatUnitTypeSelected.ValueItem);
                data.FGiaTriThanhToanTn = data.FGiaTriThanhToanTn.HasValue ? data.FGiaTriThanhToanTn / Int32.Parse(CatUnitTypeSelected.ValueItem) : data.FGiaTriThanhToanTn; // có giá trị thì chia k thì kệ
                data.FGiaTriThanhToanNn = data.FGiaTriThanhToanNn.HasValue ? data.FGiaTriThanhToanNn / Int32.Parse(CatUnitTypeSelected.ValueItem) : data.FGiaTriThanhToanNn; // có giá trị thì chia k thì kệ
            });
            return listWithDVT;
        }

        private void OnPrintReport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension; 
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    foreach (VdtTtDeNghiThanhToanModel item in VdtTtDeNghiThanhToanModels)
                    {
                        List<VdtTtPheDuyetThanhToanChiTiet> listData = _pheDuyetThanhToanChiTietService.FindByDeNghiThanhToanId(item.Id);
                        List<VdtTtPheDuyetThanhToanChiTiet> listDataThuHoi = listData.Where(n => (string.IsNullOrEmpty(n.M) && string.IsNullOrEmpty(n.Tm)
                                                                                    && string.IsNullOrEmpty(n.Ttm) && string.IsNullOrEmpty(n.Ng))).ToList();
                        double thuHoiNamTrcTN = (listDataThuHoi != null && listDataThuHoi.Count > 0) ?
                            listDataThuHoi.Select(n => (n.FGiaTriThuHoiNamTruocTn ?? 0) + (n.FGiaTriThuHoiUngTruocNamTruocTn ?? 0)).Sum() : 0;
                        double thuHoiNamTrcNN = (listDataThuHoi != null && listDataThuHoi.Count > 0) ?
                            listDataThuHoi.Select(n => (n.FGiaTriThuHoiNamTruocNn ?? 0) + (n.FGiaTriThuHoiUngTruocNamTruocNn ?? 0)).Sum() : 0;
                        double thuHoiNamNayTN = (listData != null && listData.Count > 0) ?
                            listData.Select(n => (n.FGiaTriThuHoiNamNayTn ?? 0) + (n.FGiaTriThuHoiUngTruocNamNayTn ?? 0)).Sum() : 0;
                        double thuHoiNamNayNN = (listData != null && listData.Count > 0) ?
                            listData.Select(n => (n.FGiaTriThuHoiNamNayNn ?? 0) + (n.FGiaTriThuHoiUngTruocNamNayNn ?? 0)).Sum() : 0;

                        var objGiaTriPheDuyet = _deNghiThanhToanService.LoadGiaTriPheDuyetThanhToanByParentId(item.Id);
                        var fGiaTriTuChoiTN = item.fGiaTriThanhToanTN - ((item.iLoaiThanhToan == (int)PaymentTypeEnum.Type.THANH_TOAN) ? objGiaTriPheDuyet.ThanhToanTN : (objGiaTriPheDuyet.TamUngTN + objGiaTriPheDuyet.TamUngUngTruocTN));
                        var fGiaTriTuChoiNN = item.fGiaTriThanhToanNN - ((item.iLoaiThanhToan == (int)PaymentTypeEnum.Type.THANH_TOAN) ? objGiaTriPheDuyet.ThanhToanNN : (objGiaTriPheDuyet.TamUngNN + objGiaTriPheDuyet.TamUngUngTruocNN));

                        var fTraDonViThuHuongTN = listData.Where(n => (!string.IsNullOrEmpty(n.M) || !string.IsNullOrEmpty(n.Tm) || !string.IsNullOrEmpty(n.Ttm) || !string.IsNullOrEmpty(n.Ng))).Sum(n => n.FGiaTriThanhToanTn ?? 0)
                            - thuHoiNamTrcTN - thuHoiNamNayTN;
                        var fTraDonViThuHuongNN = listData.Where(n => (!string.IsNullOrEmpty(n.M) || !string.IsNullOrEmpty(n.Tm) || !string.IsNullOrEmpty(n.Ttm) || !string.IsNullOrEmpty(n.Ng))).Sum(n => n.FGiaTriThanhToanNn ?? 0)
                            - thuHoiNamTrcNN - thuHoiNamNayNN;
                        var fTongTraDonViThuHuong = fTraDonViThuHuongTN + fTraDonViThuHuongNN;

                        Dictionary<string, object> data = new Dictionary<string, object>(); 
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Items", GetListDataToPrintWithDonViTinh(listData));
                        data.Add("DonViTinh", (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                        data.Add("ThuHoiNamTrcTN", thuHoiNamTrcTN / donViTinh);
                        data.Add("ThuHoiNamTrcNN", thuHoiNamTrcNN / donViTinh);
                        data.Add("ThuHoiNamNayTN", thuHoiNamNayTN / donViTinh);
                        data.Add("ThuHoiNamNayNN", thuHoiNamNayNN / donViTinh);
                        data.Add("ThuHoiNamTrcTong", (thuHoiNamTrcTN + thuHoiNamTrcNN) / donViTinh);
                        data.Add("ThuHoiNamNayTong", (thuHoiNamNayTN + thuHoiNamNayNN) / donViTinh);
                        data.Add("GiaTriTuChoiTN", fGiaTriTuChoiTN / donViTinh);
                        data.Add("GiaTriTuChoiNN", fGiaTriTuChoiNN / donViTinh);
                        data.Add("TongGiaTriTuChoi", (fGiaTriTuChoiTN + fGiaTriTuChoiNN) / donViTinh);
                        data.Add("TraDonViThuHuongTN", fTraDonViThuHuongTN / donViTinh);
                        data.Add("TraDonViThuHuongNN", fTraDonViThuHuongNN / donViTinh);
                        data.Add("TongTraDonViThuHuong", fTongTraDonViThuHuong / donViTinh);
                        data.Add("TongTraDonViThuHuongText", StringUtils.NumberToText(fTongTraDonViThuHuong));
                        data.Add("LyDoTuChoi", item.SLyDoTuChoi);
                        data.Add("GhiChuPheDuyet", item.SGhiChuPheDuyet);
                        data.Add("Ngay", item.dNgayPheDuyet.HasValue ? item.dNgayPheDuyet.Value.ToString("dd/MM/yyyy") : string.Empty);
                        AddChuKy(data, _typeChuky);

                        if (item.iLoaiThanhToan == 1)
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDTQUANLYCAPPHATTHANHTOANTHANHTOAN);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDT_QUANLYCAPPHATTHANHTOAN);
                        }
                        fileNamePrefix = string.Format("rptQuanLyCapPhatThanhToan_{0}", item.sSoDeNghi);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<VdtTtPheDuyetThanhToanChiTiet>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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
            //data.Add("Ngay", DateUtils.GetCurrentDateReport());
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
        }
    }
}
