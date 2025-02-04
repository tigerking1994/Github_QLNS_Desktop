using AutoMapper;
using FlexCel.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Markup;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.QuyetToanNienDo.PrintDialog;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.ThongTriQuyetToan;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static System.Net.Mime.MediaTypeNames;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ThongTriQuyetToan
{
    public class ThongTriQuyetToanPrintDialogViewModel : DialogViewModelBase<NhQtThongTriQuyetToanModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IExportService _exportService;
        private readonly INhQtThongTriQuyetToanService _nhQtThongTriQuyetToanService;
        private readonly INhQtThongTriQuyetToanChiTietService _nhQtThongTriQuyetToanChiTietService;
        private SessionInfo _sessionInfo;
        private DmChuKy _dmChuKy;
        private readonly string _typeChuky = TypeChuKy.RPT_NH_THONG_TRI_QUYET_TOAN;

        public override string Title => "Báo cáo Đề nghị quyết toán niên độ";
        public override string Description => "Báo cáo Đề nghị thanh toán niên độ";
        public override Type ContentType => typeof(ThongTriQuyetToanPrintDialog);

        private DmChuKyDialogViewModel DmChuKyDialogViewModel { get; }

        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public ObservableCollection<LookupQuery<string, string>> _itemsDonViTienTe;
        public ObservableCollection<LookupQuery<string, string>> ItemsDonViTienTe
        {
            get => _itemsDonViTienTe;
            set => SetProperty(ref _itemsDonViTienTe, value);
        }

        public LookupQuery<string, string> _selectedDonViTienTe;
        public LookupQuery<string, string> SelectedDonViTienTe
        {
            get => _selectedDonViTienTe;
            set => SetProperty(ref _selectedDonViTienTe, value);
        }

        public ThongTriQuyetToanPrintDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            IDmChuKyService dmChuKyService,
            IExportService exportService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            INhQtThongTriQuyetToanService nhQtThongTriQuyetToanService,
            INhQtThongTriQuyetToanChiTietService nhQtThongTriQuyetToanChiTietService)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _dmChuKyService = dmChuKyService;
            _exportService = exportService;

            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            PrintCommand = new RelayCommand(obj => OnPrint(obj));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            _nhQtThongTriQuyetToanChiTietService = nhQtThongTriQuyetToanChiTietService;
            _nhQtThongTriQuyetToanService = nhQtThongTriQuyetToanService;
        }

        public override void Init()
        {
            LoadDonViTienTe();
        }

        private void OnPrint(object obj)
        {
            if (SelectedDonViTienTe != null)
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    FormatNumber formatNumber = new FormatNumber(1, ExportType.PDF);
                    var ttqt = _nhQtThongTriQuyetToanService.GetThongTriById(Model.Id);
                    var ttqt_ct = _nhQtThongTriQuyetToanChiTietService.GetThongTriChiTietByTTQTId(Model.Id);
                    var tongtien = SelectedDonViTienTe.Id == "USD" ? ttqt.fThongTri_USD : ttqt.fThongTri_VND;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NH_THONG_TRI_QUYET_TOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                    Dictionary<string, object> report = new Dictionary<string, object>();
                    report.Add("sSoThongTri", ttqt.sSoThongTri);
                    report.Add("sLoaiThongTri", ttqt.iLoaiThongTri == 2 ? "Giảm quyết toán" : "Quyết toán");
                    report.Add("sTenDonVi", ttqt.sTenDonVi);
                    //report.Add("sTongTien", SelectedDonViTienTe.Id == "VND" ? StringUtils.NumberToText(tongtien.HasValue ? (long)tongtien : 0) : "");
                    report.Add("sTongTien", SelectedDonViTienTe.Id == "VND" ? StringUtils.NumberToText(Convert.ToDouble(tongtien)) : StringUtils.NumberToText(Convert.ToDouble(tongtien), false));
                    report.Add("fTongTien", tongtien.HasValue ? DinhDangSo(tongtien.Value.ToString("0.00" + new string('#', 397), CultureInfo.InvariantCulture), SelectedDonViTienTe.Id == "VND" ? 0 : 2) : "0");
                    report.Add("iNamThongTri", ttqt.iNamThongTri);
                    report.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    report.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    report.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    report.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    report.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    report.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    report.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    report.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    report.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    string templateFileName = Path.Combine(ExportPrefix.PATH_NH_QT, ExportFileName.RPT_NH_THONG_TRI_QUYET_TOAN);
                    string fileNamePrefix = string.Format("{0} {1}", "Báo cáo thông tri quyết toán năm ", ttqt.iNamThongTri);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);

                    foreach (var item in ttqt_ct)
                    {
                        item.sMoneyReport = ttqt.iLoaiThongTri == 1
                            ? SelectedDonViTienTe.Id == "USD"
                                ? item.fDeNghiQuyetToanNam_USD.HasValue ? DinhDangSo(item.fDeNghiQuyetToanNam_USD.Value.ToString("0.00" + new string('#', 397), CultureInfo.InvariantCulture), 2) : "0"
                                : item.fDeNghiQuyetToanNam_VND.HasValue ? DinhDangSo(item.fDeNghiQuyetToanNam_VND.Value.ToString("0.00" + new string('#', 397), CultureInfo.InvariantCulture), 0) : "0"
                            : SelectedDonViTienTe.Id == "USD"
                                ? item.fThuaNopTraNSNN_USD.HasValue ? DinhDangSo(item.fThuaNopTraNSNN_USD.Value.ToString("0.00" + new string('#', 397), CultureInfo.InvariantCulture), 2) : "0"
                                : item.fThuaNopTraNSNN_VND.HasValue ? DinhDangSo(item.fThuaNopTraNSNN_VND.Value.ToString("0.00" + new string('#', 397), CultureInfo.InvariantCulture), 0) : "0";
                    }

                    IEnumerable<RptThongTriQuyetToan> dataTableReport = _mapper.Map<IEnumerable<RptThongTriQuyetToan>>(ttqt_ct);
                    report.Add("dt", dataTableReport);
                    ExcelFile xlsFile = _exportService.Export<RptThongTriQuyetToan>(templateFileName, report);
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
            else
            {
                MessageBoxHelper.Info("Vui lòng chọn đơn vị tiền tệ!");
                return;
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
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                //LoadTitleFirst();
                //DmChuKyModel chuKy = (DmChuKyModel)obj;
                //if (chuKy != null)
                //{
                //    if (!string.IsNullOrEmpty(chuKy.TieuDe1MoTa))
                //        TxtTitleFirst = chuKy.TieuDe1MoTa;
                //    if (!string.IsNullOrEmpty(chuKy.TieuDe2MoTa))
                //        TxtTitleSecond = chuKy.TieuDe2MoTa;
                //}
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
        private void LoadDonViTienTe()
        {
            ItemsDonViTienTe = new ObservableCollection<LookupQuery<string, string>>();
            ItemsDonViTienTe.Add(new LookupQuery<string, string> { 
                DisplayName = "USD",
                Id = "USD"
            });
            ItemsDonViTienTe.Add(new LookupQuery<string, string>
            {
                DisplayName = "VND",
                Id = "VND"
            });
        }

        private string DinhDangSo(object so, int soSauDauPhay, bool removeSign = false, bool kieuSoVietNam = true)
        {
            string result = "";
            bool flagSign = false;
            string text = Convert.ToString(so);
            if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                double num = Convert.ToDouble(text);
                if (num == 0.0)
                {
                    return result;
                }
            }
            else if (string.IsNullOrEmpty(text))
            {
                return result;
            }

            if (so != null)
            {
                int num2 = text.IndexOf("-");
                if (num2 >= 0)
                {
                    flagSign = true;
                    text = text.Replace("-", "");
                }

                int num3 = text.IndexOf(".");
                int num4 = 0;
                string text2 = "";
                if (num3 >= 0)
                {
                    text2 = text.Substring(num3 + 1, text.Length - num3 - 1);
                    text = text.Substring(0, num3);
                }

                int length = text.Length;
                if (text.Length > 3)
                {
                    for (num2 = length; num2 > 1; num2--)
                    {
                        num4++;
                        if (num4 % 3 == 0)
                        {
                            text = !kieuSoVietNam ? text.Insert(num2 - 1, ",") : text.Insert(num2 - 1, ".");
                        }
                    }
                }

                if (soSauDauPhay >= 0)
                {
                    string text3 = "";
                    for (num3 = 0; num3 < soSauDauPhay; num3++)
                    {
                        text3 = ((num3 >= text2.Length) ? (text3 + "0") : (text3 + text2[num3]));
                    }

                    text2 = text3;
                }

                if (text2 != "")
                {
                    text = !kieuSoVietNam ? (text + "." + text2) : (text + "," + text2);
                }

                if (flagSign && !removeSign)
                {
                    text = "-" + text;
                }

                result = text;
            }

            return result;
        }
    }
}
