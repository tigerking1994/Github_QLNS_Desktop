using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Forex.Domestic.ExchangeRateDifference.PrintDialog;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Domestic.ExchangeRateDifference.PrintDialog
{
    public class ExchangeRateDifferencePrintDialogViewModel : DialogViewModelBase<NhDaChenhLechTiGiaModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INhDaChenhLechTiGiaService _service;
        private readonly IExportService _exportService;

        private readonly string _typeChuky = TypeChuKy.RPT_NH_CHENHLECH_TIGIA_HOIDOAI;
        private readonly string TITLE_FIRST_DEFAULT_VALUE = "BÁO CÁO CHÊNH LỆCH TỈ GIÁ HỐI ĐOÁI THEO HỢP ĐỒNG CỦA NGUỒN QUỸ DỰ TRỮ NGOẠI HỐI";
        public override string Title => "Báo cáo chênh lệch tỉ giá hối đoái";
        public override string Description => "Báo cáo chênh lệch tỉ giá hối đoái";
        public override Type ContentType => typeof(ExchangeRateDifferencePrintDialog);

        private string _tieuDe1;
        public string TieuDe1
        {
            get => _tieuDe1;
            set => SetProperty(ref _tieuDe1, value);
        }

        private string _tieuDe2;
        public string TieuDe2
        {
            get => _tieuDe2;
            set => SetProperty(ref _tieuDe2, value);
        }

        private string _tieuDe3;
        public string TieuDe3
        {
            get => _tieuDe3;
            set => SetProperty(ref _tieuDe3, value);
        }

        public ExchangeRateDifferenceCriteria conditionSearch { get; set; }
        private DmChuKyDialogViewModel DmChuKyDialogViewModel { get; }

        public RelayCommand ExportExcelCommand { get; set; }
        public RelayCommand ExportPdfCommand { get; set; }
        public RelayCommand ConfigSignCommand { get; }

        public ExchangeRateDifferencePrintDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            IDmChuKyService dmChuKyService,
            IExportService exportService,
            INhDaChenhLechTiGiaService service,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _dmChuKyService = dmChuKyService;
            _exportService = exportService;
            _service = service;

            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ExportExcelCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportPdfCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            LoadTitleFirst();
        }

        private void LoadTitleFirst()
        {
            TieuDe1 = TITLE_FIRST_DEFAULT_VALUE;
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
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TieuDe1 = chuKy.TieuDe1MoTa.IsEmpty("") ? TITLE_FIRST_DEFAULT_VALUE : chuKy.TieuDe1MoTa;
                TieuDe2 = chuKy.TieuDe2MoTa;
                TieuDe3 = chuKy.TieuDe3MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    Dictionary<string, object> data = new Dictionary<string, object>();

                    data.Add("sTenDonViCapTren", _sessionService.Current.TenDonViTrucThuocReportHeader);
                    data.Add("sTenDonViCapDuoi", _sessionService.Current.TenDonViReportHeader);
                    data.Add("TieuDe1", TieuDe1);
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    AddChuKy(data, _typeChuky);

                    var exchangeRateDiffExportList = _service.FindAllExchangeRateDifference(conditionSearch);
                    IEnumerable<RptChenhLechTiGiaHoiDoai> chenhLechTiGiaList = exchangeRateDiffExportList.Select(x => new RptChenhLechTiGiaHoiDoai
                    {
                        sTen = x.IsHangCha ? string.Concat(x.position, ". ", x.sTen) : x.sTen,
                        sTienKHTTBQPCapUSD = DinhDangSo((x.fTienKHTTBQPCapUSD.HasValue ? x.fTienKHTTBQPCapUSD.Value.ToString(CultureInfo.InvariantCulture) : string.Empty), 2),
                        sTienKHTTBQPCapVND = DinhDangSo((x.fTienKHTTBQPCapVND.HasValue ? x.fTienKHTTBQPCapVND.Value.ToString(CultureInfo.InvariantCulture) : string.Empty), 0),
                        sTienTheoDuAnUSD = DinhDangSo((x.fTienTheoDuAnUSD.HasValue ? x.fTienTheoDuAnUSD.Value.ToString(CultureInfo.InvariantCulture) : string.Empty), 2),
                        sTienTheoDuAnVND = DinhDangSo((x.fTienTheoDuAnVND.HasValue ? x.fTienTheoDuAnVND.Value.ToString(CultureInfo.InvariantCulture) : string.Empty), 0),
                        sTienTheoHopDongUSD = DinhDangSo((x.fTienTheoHopDongUSD.HasValue ? x.fTienTheoHopDongUSD.Value.ToString(CultureInfo.InvariantCulture) : string.Empty), 2),
                        sTienTheoHopDongVND = DinhDangSo((x.fTienTheoHopDongVND.HasValue ? x.fTienTheoHopDongVND.Value.ToString(CultureInfo.InvariantCulture) : string.Empty), 0),
                        sKinhPhiDuocCapChoCDTUSD = DinhDangSo((x.fKinhPhiDuocCapChoCDTUSD.HasValue ? x.fKinhPhiDuocCapChoCDTUSD.Value.ToString(CultureInfo.InvariantCulture) : string.Empty), 2),
                        sKinhPhiDuocCapChoCDTVND = DinhDangSo((x.fKinhPhiDuocCapChoCDTVND.HasValue ? x.fKinhPhiDuocCapChoCDTVND.Value.ToString(CultureInfo.InvariantCulture) : string.Empty), 0),
                        sKinhPhiDaThanhToanUSD = DinhDangSo((x.fKinhPhiDaThanhToanUSD.HasValue ? x.fKinhPhiDaThanhToanUSD.Value.ToString(CultureInfo.InvariantCulture) : string.Empty), 2),
                        sKinhPhiDaThanhToanVND = DinhDangSo((x.fKinhPhiDaThanhToanVND.HasValue ? x.fKinhPhiDaThanhToanVND.Value.ToString(CultureInfo.InvariantCulture) : string.Empty), 0),
                        sTiGiaCLHopDongVsCDTUSD = GetGiaTienChenhLech(x.fTiGiaCLHopDongVsCDTUSD, 2),
                        sTiGiaCLHopDongVsCDTVND = GetGiaTienChenhLech(x.fTiGiaCLHopDongVsCDTVND, 0),
                        sTiGiaCLKinhPhiDuocCapVsGiaiNganUSD = GetGiaTienChenhLech(x.fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD, 2),
                        sTiGiaCLKinhPhiDuocCapVsGiaiNganVND = GetGiaTienChenhLech(x.fTiGiaCLKinhPhiDuocCapVsGiaiNganVND, 0),
                        IsHangCha = x.IsHangCha
                    });

                    data.Add("dt", chenhLechTiGiaList);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_NH_DUAN, ExportFileName.PRT_NH_CHENHLECH_TIGIAHOIDOAI);
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<RptChenhLechTiGiaHoiDoai>(templateFileName, data);
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

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            // add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy != null ? dmChyKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ChucDanh1", dmChyKy != null ? dmChyKy.ChucDanh1MoTa : string.Empty);
            data.Add("Ten1", dmChyKy != null ? dmChyKy.Ten1MoTa : string.Empty);
            data.Add("ThuaLenh2", dmChyKy != null ? dmChyKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ChucDanh2", dmChyKy != null ? dmChyKy.ChucDanh2MoTa : string.Empty);
            data.Add("Ten2", dmChyKy != null ? dmChyKy.Ten2MoTa : string.Empty);
            data.Add("ThuaLenh3", dmChyKy != null ? dmChyKy.ThuaLenh3MoTa : string.Empty);
            data.Add("ChucDanh3", dmChyKy != null ? dmChyKy.ChucDanh3MoTa : string.Empty);
            data.Add("Ten3", dmChyKy != null ? dmChyKy.Ten3MoTa : string.Empty);
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

        private string GetGiaTienChenhLech(double? giaTien, int toFixed)
        {
            string sGiaTienChenhLech = string.Empty;
            if (giaTien.HasValue)
            {
                if (giaTien.Value == 0)
                {
                    sGiaTienChenhLech = toFixed == 2 ? "0,00" : "0";
                }
                else
                {
                    sGiaTienChenhLech = DinhDangSo(giaTien.Value.ToString(CultureInfo.InvariantCulture), toFixed);
                }
            }
            return sGiaTienChenhLech;
        }
    }
}
