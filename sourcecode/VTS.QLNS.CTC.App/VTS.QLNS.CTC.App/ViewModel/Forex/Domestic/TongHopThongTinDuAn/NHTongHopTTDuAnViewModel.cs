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
using VTS.QLNS.CTC.App.View.Forex.Domestic.TongHopThongTinDuAn;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Domestic.TongHopThongTinDuAn
{
    public class NHTongHopTTDuAnViewModel : GridViewModelBase<RptNHTongHopThongTinDuAn>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly ILog _logger;
        private ICollectionView _projectInformationPrintReportView;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private AggregateProjectInformationReportCriteria _searchCondition;

        // public override string FuncCode => NSFunctionCode.FOREX_PROJECT_CONTRACT_TONGHOP_TT_DUAN;
        public override string GroupName => MenuItemContants.GROUP_FOREX_QUYETTOAN_BAOCAO;
        public override string Name => "Tổng hợp thông tin dự án";
        public override string Description => "Tổng hợp thông tin dự án";
        public override Type ContentType => typeof(NHTongHopThongTinDuAn);
        public override PackIconKind IconKind => PackIconKind.Report;

        private RptNHTongHopThongTinDuAn _rptPrInformationSummary = new RptNHTongHopThongTinDuAn();
        public RptNHTongHopThongTinDuAn RptPrInformationSummary
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

        public NHTongHopTTDuAnViewModel(IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IExportService exportService,
            INhDaDuAnService nhDaDuAnService,
            ILog logger)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _nhDaDuAnService = nhDaDuAnService;
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
            if (_cbxLoaiDonViSelected != null)
            {
                _searchCondition = new AggregateProjectInformationReportCriteria()
                {
                    IdMaDonViQuanLy = _cbxLoaiDonViSelected.ValueItem,
                    NamKeHoach = _sessionService.Current.YearOfWork
                };
            } else
            {
                _searchCondition = null;
            }
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                ResetConditionSearch();
                List<ReportNHTongHopThongTinDuAnQuery> lstDuAn = _nhDaDuAnService.FindByAggregateProjectInformationReport(_searchCondition).ToList();

                Items = _mapper.Map<ObservableCollection<RptNHTongHopThongTinDuAn>>(lstDuAn);

                _projectInformationPrintReportView = CollectionViewSource.GetDefaultView(Items);
                _projectInformationPrintReportView.Filter = ListSettlementVoucherFilter;

                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(RptNHTongHopThongTinDuAn.Selected))
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
                var item = (RptNHTongHopThongTinDuAn)obj;

                if (CbxLoaiDonViSelected != null)
                    result = result && item.SMaDonVi.Equals(CbxLoaiDonViSelected.ValueItem);

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

                //if (CbxLoaiDonVi != null && CbxLoaiDonVi.Count() > 0)
                //    _cbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private static void SelectAll(bool select, IEnumerable<RptNHTongHopThongTinDuAn> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }

        protected override void OnRefresh()
        {
            CbxLoaiDonViSelected = null;
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

                    List<RptNHTongHopThongTinDuAn> lstReport = Items.Where(x => x.Selected && ListSettlementVoucherFilter(x)).ToList();
                    lstReport.Select((x, index) => { x.Stt = index + 1; return x; }).ToList();

                    RptPrInformationSummary.FGiaTriEuroChuTruong = lstReport.Sum(x => x.FGiaTriEuroChuTruong);
                    RptPrInformationSummary.FGiaTriEuroDauTu = lstReport.Sum(x => x.FGiaTriEuroDauTu);
                    RptPrInformationSummary.FGiaTriKhacChuTruong = lstReport.Sum(x => x.FGiaTriKhacChuTruong);
                    RptPrInformationSummary.FGiaTriKhacDauTu = lstReport.Sum(x => x.FGiaTriKhacDauTu);
                    RptPrInformationSummary.FGiaTriUSDChuTruong = lstReport.Sum(x => x.FGiaTriUSDChuTruong);
                    RptPrInformationSummary.FGiaTriUSDDauTu = lstReport.Sum(x => x.FGiaTriUSDDauTu);
                    RptPrInformationSummary.FGiaTriVNDChuTruong = lstReport.Sum(x => x.FGiaTriVNDChuTruong);
                    RptPrInformationSummary.FGiaTriVNDDauTu = lstReport.Sum(x => x.FGiaTriVNDDauTu);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Items", lstReport);
                    data.Add("Cap1", _sessionService.Current.TenDonViTrucThuocReportHeader);
                    data.Add("Cap2", _sessionService.Current.TenDonViReportHeader);
                    data.Add("Header1", string.Format("TỔNG HỢP THÔNG TIN DỰ ÁN NĂM {0}", _sessionService.Current.YearOfWork.ToString()));
                    data.Add("DonVi", string.Format("Đơn vị: {0}", _cbxLoaiDonViSelected?.DisplayItem));
                    data.Add("FGiaTriEuroChuTruongSum", RptPrInformationSummary.FGiaTriEuroChuTruong);
                    data.Add("FGiaTriEuroDauTuSum", RptPrInformationSummary.FGiaTriEuroDauTu);
                    data.Add("FGiaTriKhacChuTruongSum", RptPrInformationSummary.FGiaTriKhacChuTruong);
                    data.Add("FGiaTriKhacDauTuSum", RptPrInformationSummary.FGiaTriKhacDauTu);
                    data.Add("FGiaTriUSDChuTruongSum", RptPrInformationSummary.FGiaTriUSDChuTruong);
                    data.Add("FGiaTriUSDDauTuSum", RptPrInformationSummary.FGiaTriUSDDauTu);
                    data.Add("FGiaTriVNDChuTruongSum", RptPrInformationSummary.FGiaTriVNDChuTruong);
                    data.Add("FGiaTriVNDDauTuSum", RptPrInformationSummary.FGiaTriVNDDauTu);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_NH_CP_DNTT, ExportFileName.RPT_NH_TONGHOP_TT_DUAN);
                    string fileNamePrefix = "RptNHTongHopThongTinDuAn";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<RptNHTongHopThongTinDuAn>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, ExportType.PDF);
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
