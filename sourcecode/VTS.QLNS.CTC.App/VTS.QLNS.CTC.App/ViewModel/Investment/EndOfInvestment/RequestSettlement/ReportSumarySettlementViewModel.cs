using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using System.IO;
using System.Windows;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.RequestSettlement
{
    public class ReportSumarySettlementViewModel : DialogViewModelBase<string>
    {
        private INsDonViService _nsDonViService;
        private readonly IMapper _mapper;
        private ISessionService _sessionService;
        private IVdtDaDuAnService _vdtDaDuAnService;
        private IExportService _exportService;
        private readonly ILog _logger;
        private IVdtQtQuyetToanService _vdtQtQuyetToanService;
        private IVdtDeNghiQuyetToanService _iVdtDeNghiQuyetToanService;

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_PRINT_REPORT_PROCESS_PROJECT;
        //public override string GroupName => MenuItemContants.GROUP_REPORT;
        public override string GroupName => MenuItemContants.GROUP_FINISH_SETTLEMENT;
        public override string Name => "Báo cáo dự án hoàn thành đã phê duyệt quyết toán trong kỳ";
        public override string Title => "Báo cáo dự án hoàn thành đã phê duyệt quyết toán trong kỳ";
        public override string Description => "Báo cáo dự án hoàn thành đã phê duyệt quyết toán trong kỳ";
        public override PackIconKind IconKind => PackIconKind.ClipboardFileOutline;
        public override Type ContentType => typeof(View.Investment.EndOfInvestment.RequestSettlement.ReportSumarySettlement);

        private ObservableCollection<ComboboxItem> _dataLoaiBaoCao;
        public ObservableCollection<ComboboxItem> DataLoaiBaoCao
        {
            get => _dataLoaiBaoCao;
            set => SetProperty(ref _dataLoaiBaoCao, value);
        }

        private int? _nam;
        public int? Nam
        {
            get => _nam;
            set => SetProperty(ref _nam, value);
        }

        private ComboboxItem _selectedLoaiBaoCao;
        public ComboboxItem SelectedLoaiBaoCao
        {
            get => _selectedLoaiBaoCao;
            set
            {
                if (SetProperty(ref _selectedLoaiBaoCao, value) && _selectedLoaiBaoCao != null)
                {
                    LoadData();
                }
            }
        }

        private ObservableCollection<ReportQuyetToanHoanThanhQuery> _dataBaoCaoDuAnHoanThanhPheDuyet;
        public ObservableCollection<ReportQuyetToanHoanThanhQuery> DataBaoCaoDuAnHoanThanhPheDuyet
        {
            get => _dataBaoCaoDuAnHoanThanhPheDuyet;
            set => SetProperty(ref _dataBaoCaoDuAnHoanThanhPheDuyet, value);
        }

        private Visibility _statusShowGridReportDuAnHoanThanh;
        public Visibility StatusShowGridReportDuAnHoanThanh
        {
            get => _statusShowGridReportDuAnHoanThanh;
            set => SetProperty(ref _statusShowGridReportDuAnHoanThanh, value);
        }

        public RelayCommand ExportCommand { get; }
        public RelayCommand SearchCommand { get; }

        public ReportSumarySettlementViewModel(INsDonViService nsDonViService,
                                             ISessionService sessionService,
                                             IVdtDaDuAnService vdtDaDuAnService,
                                             IExportService exportService,
                                             IVdtQtQuyetToanService vdtQtQuyetToanService,
                                             ILog logger,
                                             IVdtDeNghiQuyetToanService iVdtDeNghiQuyetToanService,
                                             IMapper mapper)
        {
            _sessionService = sessionService;
            _vdtDaDuAnService = vdtDaDuAnService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _vdtQtQuyetToanService = vdtQtQuyetToanService;
            _iVdtDeNghiQuyetToanService = iVdtDeNghiQuyetToanService;
            _logger = logger;
            _mapper = mapper;

            ExportCommand = new RelayCommand(o => OnExport());
            SearchCommand = new RelayCommand(o => OnSearch());
        }

        private void OnSearch()
        {
            LoadData();
        }

        private void OnExport()
        {
            switch (SelectedLoaiBaoCao.ValueItem)
            {
                case NSFunctionCode.INVESTMENT_END_OF_INVESTMENT_REPORT_SUMMARY_SETTLEMENT:
                    ExportReportSummarySettlement();
                    break;
                case NSFunctionCode.INVESTMENT_END_OF_INVESTMENT_REPORT_PROJECT_APPLY_FILE:

                    break;
                default:
                    break;
            }
        }

        public List<ReportQuyetToanHoanThanhQuery> GetDataExportReportSummarySettlement()
        {
            if (Nam == null)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgInputYear, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return new List<ReportQuyetToanHoanThanhQuery>();
            }
            List<ReportQuyetToanHoanThanhQuery> listData = _iVdtDeNghiQuyetToanService.GetDataReportQuyetToanHoanThanh(Nam.Value);
            List<ReportQuyetToanHoanThanhQuery> resultPart2 = new List<ReportQuyetToanHoanThanhQuery>();
            List<ReportQuyetToanHoanThanhQuery> resultPart1 = new List<ReportQuyetToanHoanThanhQuery>();
            List<ReportQuyetToanHoanThanhQuery> result = new List<ReportQuyetToanHoanThanhQuery>();
            if (listData != null && listData.Count > 0)
            {
                listData.Select(n => { n.Loai = 3; n.IdReport = Guid.NewGuid(); return n; }).ToList();

                List<string> listDonVi = listData.Select(n => n.MaDonVi).Distinct().ToList();
                int count = 1;
                foreach (string donvi in listDonVi.OrderBy(n => n))
                {
                    if (listData.Where(n => n.MaDonVi == donvi).Count() > 0)
                    {
                        Guid idDonVi = Guid.NewGuid();
                        resultPart2.Add(new ReportQuyetToanHoanThanhQuery
                        {
                            IdReport = idDonVi,
                            Stt = count.ToString(),
                            SoDuAn = listData.Where(n => n.MaDonVi == donvi).ToList().Count(),
                            Loai = 1,
                            TenDuAn = listData.Where(n => n.MaDonVi == donvi).FirstOrDefault().TenDonVi
                        });
                        List<string> listMaNhomDuAn = listData.Where(n => n.MaDonVi == donvi).Select(n => n.NhomDuAnId.ToString()).Distinct().ToList();
                        int countNhom = 1;
                        foreach (string maNhom in listMaNhomDuAn)
                        {
                            if (listData.Where(n => n.MaDonVi == donvi && n.NhomDuAnId.ToString() == maNhom).Count() > 0)
                            {
                                Guid idGroup = Guid.NewGuid();
                                resultPart2.Add(new ReportQuyetToanHoanThanhQuery
                                {
                                    IdReport = idGroup,
                                    IdParentReport = idDonVi,
                                    SoDuAn = listData.Where(n => n.MaDonVi == donvi && n.NhomDuAnId.ToString() == maNhom).ToList().Count(),
                                    Stt = count.ToString() + "," + countNhom.ToString(),
                                    Loai = 2,
                                    TenDuAn = listData.Where(n => n.MaDonVi == donvi && n.NhomDuAnId.ToString() == maNhom).FirstOrDefault().TenNhomDuAn
                                });
                                foreach (ReportQuyetToanHoanThanhQuery item in listData.Where(n => n.MaDonVi == donvi && n.NhomDuAnId.ToString() == maNhom))
                                {
                                    item.IdParentReport = idGroup;
                                    resultPart2.Add(item);
                                }
                                countNhom++;
                            }
                        }
                    }
                    count++;
                }
                CalculateData(ref resultPart2);

                resultPart1 = new List<ReportQuyetToanHoanThanhQuery>();
                foreach (ReportQuyetToanHoanThanhQuery item in resultPart2.Where(n => n.Loai == 2).ToList())
                {
                    resultPart1.Add(new ReportQuyetToanHoanThanhQuery
                    {
                        SoBaoCao = item.SoBaoCao,
                        TenDuAn = item.TenDuAn,
                        ChuDauTu = item.ChuDauTu,
                        SoDuAn = item.SoDuAn,
                        TongMucDauTu = item.TongMucDauTu,
                        GiaTriDeNghiQuyetToan = item.GiaTriDeNghiQuyetToan,
                        GiaTriQuyetToanDuocDuyet = item.GiaTriQuyetToanDuocDuyet,
                        VonConLai = item.VonConLai,
                        MaDonVi = item.MaDonVi,
                        NhomDuAnId = item.NhomDuAnId,
                        Loai = item.Loai,
                        TenDonVi = item.TenDonVi,
                        TenNhomDuAn = item.TenNhomDuAn,
                        IdReport = item.IdReport,
                        IdParentReport = item.IdParentReport
                    });
                }

                resultPart1.Select(n => { n.Stt = (resultPart1.IndexOf(n) + 1).ToString(); return n; }).ToList();
                if (resultPart2.Count > 0)
                {
                    resultPart2.Insert(0, new ReportQuyetToanHoanThanhQuery
                    {
                        TenDuAn = "CHI TIẾT THEO ĐV CẤP DƯỚI",
                        Stt = "Phần II",
                        IsHangCha = true
                    });
                }
                if (resultPart1.Count > 0)
                {
                    resultPart1.Insert(0, new ReportQuyetToanHoanThanhQuery
                    {
                        TenDuAn = "TỔNG CỘNG",
                        TongMucDauTu = resultPart1.Sum(n => n.TongMucDauTu),
                        GiaTriDeNghiQuyetToan = resultPart1.Sum(n => n.GiaTriDeNghiQuyetToan),
                        GiaTriQuyetToanDuocDuyet = resultPart1.Sum(n => n.GiaTriQuyetToanDuocDuyet)
                    });
                    resultPart1.Insert(0, new ReportQuyetToanHoanThanhQuery
                    {
                        TenDuAn = "BÁO CÁO TỔNG SỐ DỰ ÁN",
                        Stt = "Phần I",
                        IsHangCha = true
                    });
                }
            }

            if (resultPart1.Count > 0)
            {
                result.AddRange(resultPart1);
            }
            if (resultPart2.Count > 0)
            {
                result.AddRange(resultPart2);
            }
            return result;
        }

        public void ExportReportSummarySettlement()
        {
            if (Nam == null)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgInputYear, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ReportQuyetToanHoanThanhQuery> result = GetDataExportReportSummarySettlement();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(1, Utility.Enum.ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("NamLamViec", _sessionService.Current.YearOfWork);
                    data.Add("Items", result);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KTDT, ExportFileName.RPT_VDT_QUYETTOANHOANTHANH);
                    string fileNamePrefix = "rptQuyetToanHoanThanh";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportQuyetToanHoanThanhQuery>(templateFileName, data);
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

        private void CalculateData(ref List<ReportQuyetToanHoanThanhQuery> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.TongMucDauTu = 0; x.GiaTriDeNghiQuyetToan = 0; x.GiaTriQuyetToanDuocDuyet = 0;
                    return x;
                }).ToList();
            foreach (ReportQuyetToanHoanThanhQuery item in listData.Where(x => !x.IsHangCha && (x.TongMucDauTu != 0 || x.GiaTriDeNghiQuyetToan != 0 || x.GiaTriQuyetToanDuocDuyet != 0)))
            {
                CalculateParent(ref listData, item, item);
            }
        }

        private void CalculateParent(ref List<ReportQuyetToanHoanThanhQuery> listData, ReportQuyetToanHoanThanhQuery currentItem, ReportQuyetToanHoanThanhQuery selfItem)
        {
            ReportQuyetToanHoanThanhQuery parentItem = listData.Where(x => x.IdReport == currentItem.IdParentReport).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TongMucDauTu += selfItem.TongMucDauTu;
            parentItem.GiaTriDeNghiQuyetToan += selfItem.GiaTriDeNghiQuyetToan;
            parentItem.GiaTriQuyetToanDuocDuyet += selfItem.GiaTriQuyetToanDuocDuyet;
            CalculateParent(ref listData, parentItem, selfItem);
        }

        public void LoadComboboxLoaiBaoCao()
        {
            DataLoaiBaoCao = new ObservableCollection<ComboboxItem>();
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = NSFunctionCode.INVESTMENT_END_OF_INVESTMENT_REPORT_SUMMARY_SETTLEMENT, DisplayItem = "Dự án hoàn thành đã phê duyệt quyết toán trong kỳ" });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = NSFunctionCode.INVESTMENT_END_OF_INVESTMENT_REPORT_PROJECT_APPLY_FILE, DisplayItem = "Dự án hoàn thành đã nộp hồ sơ quyết toán" });
            _selectedLoaiBaoCao = DataLoaiBaoCao.FirstOrDefault();
        }

        private void LoadData()
        {
            switch (SelectedLoaiBaoCao.ValueItem)
            {
                case NSFunctionCode.INVESTMENT_END_OF_INVESTMENT_REPORT_SUMMARY_SETTLEMENT:
                    List<ReportQuyetToanHoanThanhQuery> result = GetDataExportReportSummarySettlement();
                    DataBaoCaoDuAnHoanThanhPheDuyet = new ObservableCollection<ReportQuyetToanHoanThanhQuery>(result);
                    StatusShowGridReportDuAnHoanThanh = Visibility.Visible;
                    OnPropertyChanged(nameof(DataBaoCaoDuAnHoanThanhPheDuyet));
                    OnPropertyChanged(nameof(StatusShowGridReportDuAnHoanThanh));
                    break;
                case NSFunctionCode.INVESTMENT_END_OF_INVESTMENT_REPORT_PROJECT_APPLY_FILE:
                    StatusShowGridReportDuAnHoanThanh = Visibility.Collapsed;
                    OnPropertyChanged(nameof(StatusShowGridReportDuAnHoanThanh));
                    break;
                default:
                    break;
            }
        }

        public override void Init()
        {
            try
            {
                DataBaoCaoDuAnHoanThanhPheDuyet = new ObservableCollection<ReportQuyetToanHoanThanhQuery>();
                LoadComboboxLoaiBaoCao();
                Nam = null;
                //LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
