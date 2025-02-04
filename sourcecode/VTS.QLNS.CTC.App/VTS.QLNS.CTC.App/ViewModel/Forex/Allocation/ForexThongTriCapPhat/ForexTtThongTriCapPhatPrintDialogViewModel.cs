using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexThongTriCapPhat
{
    public class ForexTtThongTriCapPhatPrintDialogViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly string _typeChuky = TypeChuKy.RPT_NGOAIHOI_THONGTRI_CAPPHAT;
        private readonly INhTtThongTriCapPhatService _nhTtThongTriCapPhatService;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;

        public override Type ContentType => typeof(View.Forex.ForexAllocation.ForexThongTriCapPhat.ForexTtThongTriCapPhatPrintDialog);
        public override string Title => "THÔNG TRI CẤP KINH PHÍ";
        public override string Description => "Thông tri cấp kinh phí";

        public NhTtThongTriCapPhatModel NhTtThongTriCapModel { get; set; }
        public  DmChuKyDialogViewModel _dmChuKyDialogViewModel { get;  }

        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ExportCommand { get; }

        private string _txtTitleFirst = "THÔNG TRI CẤP KINH PHÍ BẰNG NGOẠI TỆ";
        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set => SetProperty(ref _txtTitleFirst, value);
        }

        private string _txtTitleSecond;
        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }

        public ForexTtThongTriCapPhatPrintDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            INhTtThongTriCapPhatService nhTtThongTriCapPhatService,
            INsDonViService nsDonViService,
            IExportService exportService,
            INsNguonNganSachService nsNguonNganSachService)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _dmChuKyService = dmChuKyService;
            _nhTtThongTriCapPhatService = nhTtThongTriCapPhatService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _nsNguonNganSachService = nsNguonNganSachService;

            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            PrintReportCommand = new RelayCommand(obj => OnPrintReport(ExportType.PDF));
            ExportCommand = new RelayCommand(obj => OnPrintReport(ExportType.EXCEL));
            _dmChuKyDialogViewModel = dmChuKyDialogViewModel;
        }

        public override void Init()
        {
            base.Init();
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
               /* TxtTitleFirst = chuKy != null ? (string.IsNullOrEmpty(chuKy.TieuDe1MoTa) ? TITLE_FIRST_DEFAULT_VALUE : chuKy.TieuDe1MoTa) : string.Empty;
                TxtTitleSecond = chuKy != null ? (string.IsNullOrEmpty(chuKy.TieuDe2MoTa) ? TITLE_SECOND_DEFAULT_VALUE : chuKy.TieuDe2MoTa) : string.Empty;*/
            };
            _dmChuKyDialogViewModel.Init();
            _dmChuKyDialogViewModel.ShowDialog();
        }

        private void OnPrintReport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    double totalUSD = 0, totalVND = 0;
                    IsLoading = true;
                    DataTable items = _nhTtThongTriCapPhatService.ReportThongTriCapPhat(NhTtThongTriCapModel.Id);

                    //Tính tổng
                    foreach (DataRow item in items.Rows)
                    {
                        double tempUSD = Convert.ToDouble(item[5]);
                        double tempVND = Convert.ToDouble(item[6]);
                        totalUSD += tempUSD;
                        totalVND += tempVND;
                    }
                    string strTongVND = StringUtils.NumberToText(totalVND);

                    DmChuKy _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    var STenDonVi = _nsDonViService.FindAll().FirstOrDefault(x => x.Id == NhTtThongTriCapModel.IIdDonViId).MaTenDonVi;
                    var STenNganSach = _nsNguonNganSachService.FindAll().FirstOrDefault(x => x.IIdMaNguonNganSach == NhTtThongTriCapModel.IIdNguonVonId).STen;
                    FormatNumber formatNumber = new FormatNumber(1, ExportType.PDF);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TenDonVi", STenDonVi);
                    data.Add("NguonNganSach", STenNganSach);
                    data.Add("Nam", NhTtThongTriCapModel.INamThucHien);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("Items", items);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : "");
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : "");
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : "");
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : "");
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : "");
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : "");
                    data.Add("TongUSD", totalUSD);
                    data.Add("TongVND", totalVND);
                    data.Add("TongVNDBangChu", strTongVND);
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_NGOAIHOI, ExportFileName.RPT_NGOAIHOI_THONGTRI_CAPPHAT);
                    string fileNamePrefix = string.Format("rptNgoaiHoi_ThongTri_CapPhat");
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export(templateFileName, data);
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
                _logger.Error(ex.Message);
            }
        }
    }
}
