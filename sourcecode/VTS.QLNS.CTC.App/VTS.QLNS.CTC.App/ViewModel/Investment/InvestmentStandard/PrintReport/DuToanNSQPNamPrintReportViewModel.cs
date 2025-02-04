using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.PrintReport;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using log4net;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.PrintReport
{
    public class DuToanNSQPNamPrintReportViewModel : GridViewModelBase<RptDuToanNSQPNam>
    {
        private static string[] lstDonViExclude = new string[] { "0", "1" };

        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IExportService _exportService;
        private ICollectionView _projectInformationPrintReportView;
        private DuToanNSQPNamReportSearch _searchCondition;

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_DU_TOAN_NSQP_NAM_PRINT_REPORT;
        public override string GroupName => MenuItemContants.GROUP_REPORT;
        public override string Name => "Báo cáo dự toán NSQP năm";
        public override string Description => "Báo cáo dự toán NSQP theo năm";
        public override PackIconKind IconKind => PackIconKind.ClipboardFileOutline;
        public override Type ContentType => typeof(DuToanNSQPNamPrintReport);

        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set
            {
                SetProperty(ref _cbxLoaiDonViSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _dataDonVi;
        public ObservableCollection<ComboboxItem> DataDonVi
        {
            get => _dataDonVi;
            set => SetProperty(ref _dataDonVi, value);
        }

        private int _yearOfOrigination;
        public int YearOfOrigination
        {
            get => _yearOfOrigination;
            set => SetProperty(ref _yearOfOrigination, value);
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }

        public DuToanNSQPNamPrintReportViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IProjectManagerService projectManagerService,
            IExportService exportService)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _projectManagerService = projectManagerService;
            _exportService = exportService;

            SearchCommand = new RelayCommand(obj => LoadData());
            PrintBrowserCommand = new RelayCommand(obj => OnPrint(ExportType.EXCEL));
        }

        public override void Init()
        {
            LoadDataDonVi();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            ResetConditionSearch();
            List<ReportDuToanNSQPNamQuery> lstDuAn = _projectManagerService.FindByConditionDuToanNSQPNamReport(_searchCondition).ToList();

            List<ReportDuToanNSQPNamQuery> listParent = _projectManagerService.FindListParentDuToanNSQPNamReport(_searchCondition).ToList();
            if (listParent != null && listParent.Count > 0 && lstDuAn != null && lstDuAn.Count > 0)
            {
                foreach (var item in lstDuAn)
                {
                    int index = listParent.IndexOf(listParent.Where(n => n.CT == item.CT && n.LoaiDuAn == item.LoaiDuAn).FirstOrDefault());
                    if (index > 0)
                    {
                        listParent.Insert(index + 1, item);
                    }
                }
            }
            Items = _mapper.Map<ObservableCollection<RptDuToanNSQPNam>>(listParent);
            
            CaculateData();

        }

        private void ResetConditionSearch()
        {
            _searchCondition = new DuToanNSQPNamReportSearch()
            {
                MaDonViQuanLy = CbxLoaiDonViSelected.ValueItem,
                NamThucHien = _yearOfOrigination != null ? _yearOfOrigination : DateTime.Now.Year,
            };
        }

        private void LoadDataDonVi()
        {
            IEnumerable<DonViQuery> lstDonVi = _nsDonViService.FindAllDonViNotDuplicate();
            var a = lstDonVi.ToList();
            _dataDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(lstDonVi);

            if (DataDonVi != null && DataDonVi.Count() > 0)
                _cbxLoaiDonViSelected = _dataDonVi.FirstOrDefault();
        }

        private void CaculateData()
        {
            var listChildren = Items.Where(x => !(bool)x.IsHangCha).ToList();
            if (listChildren != null && listChildren.Count > 0)
            {
                foreach (var item in listChildren)
                {
                    FindParent(item);
                }
            }
        }

        private void FindParent(RptDuToanNSQPNam item)
        {
            var parentItemLoaiDuAn = Items.Where(x => (bool)x.IsHangCha && x.CT == item.CT && x.LoaiDuAn == item.LoaiDuAn).FirstOrDefault();
            if (parentItemLoaiDuAn != null)
            {
                CalculateParent(item, parentItemLoaiDuAn);
            }
            
            var parentItemLoaiCongTrinh = Items.Where(x => (bool)x.IsHangCha && x.LoaiDuAn == 0 && x.CT == item.CT).FirstOrDefault();

            if (parentItemLoaiCongTrinh != null)
            {
                CalculateParent(item, parentItemLoaiCongTrinh);
            }
        }

        private void CalculateParent(RptDuToanNSQPNam child, RptDuToanNSQPNam parent)
        {
            parent.FGiaTrPhanBo += child.FGiaTrPhanBo;
        }

        private void OnPrint(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<RptDuToanNSQPNam> lstReport = Items.ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Items", lstReport);
                    data.Add("Cap1", CbxLoaiDonViSelected.DisplayItem);
                    data.Add("Header1", string.Format("DỰ TOÁN CHI NGÂN SÁCH QUỐC PHÒNG NĂM {0}", YearOfOrigination.ToString()));

                    string templateFileName = "DuToanChiNganSachQuocPhongNam.xls";
                    string fileNamePrefix = "DuToanChiNganSachQuocPhongNam";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<RptDuToanNSQPNam>(templateFileName, data);
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
    }
}
