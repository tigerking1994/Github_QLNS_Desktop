using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using log4net;
using VTS.QLNS.CTC.App.Service.UserFunction;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PrintReportAnnualBudgetAllocation
{
    public class PrintReportAnnualBudgetAllocationViewModel : ViewModelBase
    {
        #region Private
        private readonly ISessionService _sessionService;
        private readonly IVdtKhvPhanBoVonService _phanBoVonService;
        private IExportService _exportService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        #endregion

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_PRINT_REPORT_ANNUAL_BUDGET_ALLOCATION;
        public override string GroupName => MenuItemContants.GROUP_REPORT;
        public override string Name => "Báo cáo cấp phát ngân sách năm";
        public override string Description => "Báo cáo cấp phát ngân sách năm";
        public override PackIconKind IconKind => PackIconKind.ClipboardFileOutline;
        public override Type ContentType => typeof(View.Investment.InvestmentImplementation.PrintReportAnnualBudgetAllocation.PrintReportAnnualBudgetAllocation);

        #region Items
        private double _fCapPhatNamTruoc { get; set; }
        private double _fChiTieuNamTruoc { get; set; }
        private double _fQuyetToan { get; set; }
        private double _fThongBaoDauNam { get; set; }
        private double _fTongBoXung { get; set; }
        private double _fCapPhatNamNay { get; set; }
        private double _fTamUng { get; set; }

        private ObservableCollection<RptAnnualBudgetAllocationModel> _data;
        public ObservableCollection<RptAnnualBudgetAllocationModel> Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        private string _iNamThucHien;

        public string INamThucHien
        {
            get => _iNamThucHien;
            set => SetProperty(ref _iNamThucHien, value);
        }

        private DateTime? _dDenNgay;
        public DateTime? DenNgay
        {
            get => _dDenNgay;
            set => SetProperty(ref _dDenNgay, value);
        }
        #endregion

        public RelayCommand SearchCommand { get; }
        public RelayCommand ExportCommand { get; }

        public PrintReportAnnualBudgetAllocationViewModel(ISessionService sessionService,
            IVdtKhvPhanBoVonService phanBoVonService,
            IExportService exportService,
            ILog logger,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _phanBoVonService = phanBoVonService;
            _logger = logger;
            _mapper = mapper;
            _exportService = exportService;
            SearchCommand = new RelayCommand(obj => OnSearch());
            ExportCommand = new RelayCommand(o => OnExport());
        }

        #region Relay Command
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
        }

        public void OnSearch()
        {
            int iNamThucHien = 0;
            StringBuilder messageBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(INamThucHien))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Năm thực hiện");
                messageBuilder.AppendLine();
            }
            else if (!int.TryParse(INamThucHien, out iNamThucHien))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorFormat, "Năm thực hiện");
                messageBuilder.AppendLine();
            }
            if (!DenNgay.HasValue)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Đến ngày");
                messageBuilder.AppendLine();
            }
            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join(Environment.NewLine, messageBuilder.ToString()));
                LoadData();
                return;
            }

            var data = _phanBoVonService.GetDataRptAnnualBudgetAllocation(
                iNamThucHien,
                DenNgay.Value,
                _sessionService.Current.Budget,
                _sessionService.Current.Principal);
            List<RptAnnualBudgetAllocationModel> lstDataConvert = _mapper.Map<List<RptAnnualBudgetAllocationModel>>(data);
            ConvertData(lstDataConvert);
        }

        private void OnExport()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    string sDenNgay = String.Format("Đến ngày {0} tháng {1} năm {2}", DenNgay.Value.Day, DenNgay.Value.Month, DenNgay.Value.Year);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(1, Utility.Enum.ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("iNamThucHien", INamThucHien);
                    data.Add("iNamTruoc", (int.Parse(INamThucHien) - 1));
                    data.Add("sDenNgay", sDenNgay);
                    data.Add("fChiTieuNamTruoc", _fChiTieuNamTruoc);
                    data.Add("fQuyetToan", _fQuyetToan);
                    data.Add("fThongBaoDauNam", _fThongBaoDauNam);
                    data.Add("fTongBoXung", _fTongBoXung);
                    data.Add("fCapPhatNamNay", _fCapPhatNamNay);
                    data.Add("fTamUng gay", _fTamUng);
                    data.Add("Items", Data);


                    string templateFileName = "rpt_TongHopTinhHinhThucHienNganSachNam.xlsx";
                    string fileNamePrefix = string.Format("TongHopTinhHinhThucHienNganSachNam_{0}", INamThucHien);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<RptAnnualBudgetAllocationModel>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, Utility.Enum.ExportType.EXCEL);
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
        #endregion

        #region Helper
        private void ConvertData(List<RptAnnualBudgetAllocationModel> lstDataConvert)
        {
            _fCapPhatNamTruoc = lstDataConvert.Sum(n => n.fCapPhatNamTruoc ?? 0);
            _fChiTieuNamTruoc = lstDataConvert.Sum(n => n.fChiTieuNamTruoc ?? 0);
            _fQuyetToan = lstDataConvert.Sum(n => n.fQuyetToan ?? 0);
            _fThongBaoDauNam = lstDataConvert.Sum(n => n.fThongBaoDauNam ?? 0);
            _fTongBoXung = lstDataConvert.Sum(n => n.fTongBoXung ?? 0);
            _fCapPhatNamNay = lstDataConvert.Sum(n => n.fCapPhatNamNay ?? 0);
            _fTamUng = lstDataConvert.Sum(n => n.fTamUng ?? 0);

            List<RptAnnualBudgetAllocationModel> data = new List<RptAnnualBudgetAllocationModel>();
            data.Add(new RptAnnualBudgetAllocationModel()
            {
                sTenDonVi = "TỔNG CỘNG",
                fCapPhatNamTruoc = _fCapPhatNamTruoc,
                fChiTieuNamTruoc = _fChiTieuNamTruoc,
                fQuyetToan = _fQuyetToan,
                fThongBaoDauNam = _fThongBaoDauNam,
                fTongBoXung = _fTongBoXung,
                fCapPhatNamNay = _fCapPhatNamNay,
                fTamUng = _fTamUng,
                IsHangCha = true
            });
            data.Add(new RptAnnualBudgetAllocationModel()
            {
                iID_MaDonViQuanLy = "A",
                sTenDonVi = _sessionService.Current.BudgetStr,
                fCapPhatNamTruoc = _fCapPhatNamTruoc,
                fChiTieuNamTruoc = _fChiTieuNamTruoc,
                fQuyetToan = _fQuyetToan,
                fThongBaoDauNam = _fThongBaoDauNam,
                fTongBoXung = _fTongBoXung,
                fCapPhatNamNay = _fCapPhatNamNay,
                fTamUng = _fTamUng,
                IsHangCha = true
            });
            data.Add(new RptAnnualBudgetAllocationModel()
            {
                sTenDonVi = "Phân bổ cho đơn vị",
                fCapPhatNamTruoc = _fCapPhatNamTruoc,
                fChiTieuNamTruoc = _fChiTieuNamTruoc,
                fQuyetToan = _fQuyetToan,
                fThongBaoDauNam = _fThongBaoDauNam,
                fTongBoXung = _fTongBoXung,
                fCapPhatNamNay = _fCapPhatNamNay,
                fTamUng = _fTamUng,
                IsHangCha = true
            });
            data.AddRange(lstDataConvert);
            Data = _mapper.Map<ObservableCollection<RptAnnualBudgetAllocationModel>>(data);
        }
        #endregion

    }
}
