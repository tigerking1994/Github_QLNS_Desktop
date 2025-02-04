using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PrintReport
{
    public class ProjectInformationPrintReportViewModel : GridViewModelBase<RptTongHopThongTinDuAn>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtDmDonViThucHienDuAnService _vdtDmDonViThucHienDuAnService;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IExportService _exportService;
        private readonly ILog _logger;
        private ICollectionView _projectInformationPrintReportView;
        private AggregateProjectInformationReportCriteria _searchCondition;

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_PROJECT_INFORMATION_PRINT_REPORT;
        //public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGER;
        public override string GroupName => MenuItemContants.GROUP_REPORT;
        public override string Name => "Tổng hợp thông tin dự án";
        public override string Description => "Tổng hợp thông tin dự án";
        public override Type ContentType => typeof(ProjectInformationPrintReport);
        public override PackIconKind IconKind => PackIconKind.Report;

        private RptTongHopThongTinDuAn _rptPrInformationSummary = new RptTongHopThongTinDuAn();
        public RptTongHopThongTinDuAn RptPrInformationSummary
        {
            get => _rptPrInformationSummary;
            set => SetProperty(ref _rptPrInformationSummary, value);
        }

        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set
            {
                SetProperty(ref _cbxLoaiDonViSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, Items);
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand PrintCommand { get; }

        public ProjectInformationPrintReportViewModel(IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IProjectManagerService projectManagerService,
            IExportService exportService,
            IVdtDmDonViThucHienDuAnService vdtDmDonViThucHienDuAnService,
            ILog logger)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _projectManagerService = projectManagerService;
            _vdtDmDonViThucHienDuAnService = vdtDmDonViThucHienDuAnService;
            _exportService = exportService;
            _logger = logger;

            PrintCommand = new RelayCommand(obj => OnPrint());
            SearchCommand = new RelayCommand(obj => LoadData());
        }

        public override void Init()
        {
            try
            {
                LoadDataDonVi();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ResetConditionSearch()
        {
            _searchCondition = new AggregateProjectInformationReportCriteria()
            {
                IdMaDonViQuanLy = _cbxLoaiDonViSelected.ValueItem,
                NamKeHoach = _sessionService.Current.YearOfWork
            };
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                ResetConditionSearch();
                List<ReportTongHopThongTinDuAnQuery> lstDuAn = _projectManagerService.FindByAggregateProjectInformationReport(_searchCondition).ToList();
                lstDuAn.Select(x =>
                {
                    x.ChenhLechQTThanhToan = x.GiaTriQuyetToan - x.DaThanhToan;
                    return x;
                }).ToList();

                Items = _mapper.Map<ObservableCollection<RptTongHopThongTinDuAn>>(lstDuAn);

                _projectInformationPrintReportView = CollectionViewSource.GetDefaultView(Items);
                _projectInformationPrintReportView.Filter = ListSettlementVoucherFilter;

                int i = 1;
                foreach (var model in Items)
                {
                    model.Stt = i;
                    ++i;
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(RptTongHopThongTinDuAn.Selected))
                        {
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListSettlementVoucherFilter(object obj)
        {
            try
            {
                bool result = true;
                var item = (RptTongHopThongTinDuAn)obj;

                if (!string.IsNullOrEmpty(SearchText))
                    result = result && item.SDuAnDonVi.ToLower().Contains(_searchText.ToLower());

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return true;
            }
        }

        private void LoadDataDonVi()
        {
            try
            {
                var predicate = PredicateBuilder.True<DonVi>();
                predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                predicate = predicate.And(x => LoaiDonVi.NOI_BO.Equals(x.Loai) || LoaiDonVi.ROOT.Equals(x.Loai));
                var lstDonVi = _nsDonViService.FindByCondition(predicate).ToList();

                _cbxLoaiDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(lstDonVi);

                if (CbxLoaiDonVi != null && CbxLoaiDonVi.Count() > 0)
                    _cbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private static void SelectAll(bool select, IEnumerable<RptTongHopThongTinDuAn> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
            OnPropertyChanged(nameof(Items));
        }

        private void OnPrint()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<RptTongHopThongTinDuAn> lstReport = Items.Where(x => x.Selected).ToList();
                    lstReport.Select((x, index) => { x.Stt = index + 1; return x; }).ToList();

                    RptPrInformationSummary.GiaTriDauTu = lstReport.Sum(x => x.GiaTriDauTu);
                    RptPrInformationSummary.TongMucDauTu = lstReport.Sum(x => x.TongMucDauTu);
                    RptPrInformationSummary.LuyKeVonNamTruoc = lstReport.Sum(x => x.LuyKeVonNamTruoc);
                    RptPrInformationSummary.KeHoachVonNamNay = lstReport.Sum(x => x.KeHoachVonNamNay);
                    RptPrInformationSummary.DaThanhToan = lstReport.Sum(x => x.DaThanhToan);
                    RptPrInformationSummary.ChuaThanhToan = lstReport.Sum(x => x.ChuaThanhToan);
                    RptPrInformationSummary.LuyKeVonNamNay = lstReport.Sum(x => x.LuyKeVonNamNay);
                    RptPrInformationSummary.GiaTriQuyetToan = lstReport.Sum(x => x.GiaTriQuyetToan);
                    RptPrInformationSummary.ChenhLechQTThanhToan = lstReport.Sum(x => x.ChenhLechQTThanhToan);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Items", lstReport);
                    data.Add("Cap1", "BỘ QUỐC PHÒNG");
                    data.Add("Cap2", "CỤC TÀI CHÍNH");
                    data.Add("Header1", string.Format("TỔNG HỢP THÔNG TIN DỰ ÁN NĂM {0}", _sessionService.Current.YearOfWork.ToString()));
                    data.Add("DonVi", string.Format("Đơn vị: {0}", _cbxLoaiDonViSelected.DisplayItem));
                    data.Add("GiaTriDauTuSum", RptPrInformationSummary.GiaTriDauTu);
                    data.Add("TongMucDauTuSum", RptPrInformationSummary.TongMucDauTu);
                    data.Add("LuyKeVonNamTruocSum", RptPrInformationSummary.LuyKeVonNamTruoc);
                    data.Add("KeHoachVonNamNaySum", RptPrInformationSummary.KeHoachVonNamNay);
                    data.Add("DaThanhToanSum", RptPrInformationSummary.DaThanhToan);
                    data.Add("ChuaThanhToanSum", RptPrInformationSummary.ChuaThanhToan);
                    data.Add("LuyKeVonNamNaySum", RptPrInformationSummary.LuyKeVonNamNay);
                    data.Add("GiaTriQuyetToanSum", RptPrInformationSummary.GiaTriQuyetToan);
                    data.Add("ChenhLechQTThanhToanSum", RptPrInformationSummary.ChenhLechQTThanhToan);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTDUAN, ProjectCapitialAdjustmentType.TONG_HOP_THONG_TIN_DU_AN_TEMPLATE);
                    string fileNamePrefix = "rptTongHopThongTinDuAn";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<RptTongHopThongTinDuAn>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, ExportType.EXCEL);
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
