using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Plan.PrintPlanReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.RevenueExpenditureType;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Plan.PrintPlanReport
{
    public class PrintPlanBudgetReportViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITnDtChungTuChiTietService _tnDtChungTuChiTietService;
        private readonly IExportService _exportService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly INsDonViService _nsDonViService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private ICollectionView _dataLNSView;
        private ICollectionView _dataUnitView;
        private EstimationVoucherDetailCriteria _searchCondition;

        public override Type ContentType => typeof(PrintPlanBudgetReport);
        public override string Title => "BÁO CÁO - THU NỘP NGÂN SÁCH";
        public override string Description => "(báo cáo - thu nộp ngân sách)";

        private ObservableCollection<TnDtChungTuChiTietReportQuery> _items;
        public ObservableCollection<TnDtChungTuChiTietReportQuery> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
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

        private ObservableCollection<CheckBoxItem> _dataUnit;
        public ObservableCollection<CheckBoxItem> DataUnit
        {
            get => _dataUnit;
            set => SetProperty(ref _dataUnit, value);
        }

        public string SelectedCountUnit
        {
            get
            {
                int totalCount = DataUnit != null ? DataUnit.Count : 0;
                int totalSelected = DataUnit != null ? DataUnit.Count(item => item.IsChecked) : 0;
                return string.Format("CHỌN ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }

        private bool _selectAllUnit;
        public bool SelectAllUnit
        {
            get => (DataUnit == null || !DataUnit.Any()) ? false : DataUnit.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllUnit, value);
                if (DataUnit != null)
                {
                    DataUnit.Select(c => { c.IsChecked = _selectAllUnit; return c; }).ToList();
                }
            }
        }

        private string _searchUnit;
        public string SearchUnit
        {
            get => _searchUnit;
            set
            {
                SetProperty(ref _searchUnit, value);
                _dataUnitView.Refresh();
            }
        }

        private ComboboxItem _reportTypeSelected;
        public ComboboxItem ReportTypeSelected
        {
            get => _reportTypeSelected;
            set
            {
                SetProperty(ref _reportTypeSelected, value);
                OnPropertyChanged(nameof(VisibilityCbxDataUnit));
            }
        }

        private ObservableCollection<ComboboxItem> _reportTypes = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ReportTypes
        {
            get => _reportTypes;
            set => SetProperty(ref _reportTypes, value);
        }

        private string _txtTitleFirst;
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

        private string _txtTitleThird;
        public string TxtTitleThird
        {
            get => _txtTitleThird;
            set => SetProperty(ref _txtTitleThird, value);
        }

        public Visibility VisibilityCbxDataUnit
        {
            get => (_reportTypeSelected == null || _reportTypeSelected.ValueItem.Equals(RevenueExpenditureType.PLAN_REPORT_SUM_TYPE_KEY)) ? Visibility.Collapsed : Visibility.Visible;
        }

        private ObservableCollection<NsMuclucNgansachModel> _dataLNS;
        public ObservableCollection<NsMuclucNgansachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalItems = DataLNS != null ? DataLNS.Count() : 0;
                int totalSelected = DataLNS != null ? DataLNS.Count(item => item.IsSelected) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalItems);
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => (DataLNS == null || !DataLNS.Any()) ? false : DataLNS.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                if (DataLNS != null)
                {
                    DataLNS.Select(c => { c.IsSelected = _selectAllLNS; return c; }).ToList();
                }
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                SetProperty(ref _searchLNS, value);
                _dataLNSView.Refresh();
            }
        }

        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintPlanBudgetReportViewModel(IMapper mapper,
            IDmChuKyService dmChuKyService,
            ISessionService sessionService,
            ITnDtChungTuChiTietService tnDtChungTuChiTietService,
            IExportService exportService,
            INsMucLucNganSachService nsMucLucNganSachService,
            INsDonViService nsDonViService,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _dmChuKyService = dmChuKyService;
            _tnDtChungTuChiTietService = tnDtChungTuChiTietService;
            _exportService = exportService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _nsDonViService = nsDonViService;
            _logger = logger;

            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            PrintBrowserCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            LoadCatUnitTypes();
            LoadReportTypes();
            LoadDataUnit();
            LoadDataLNS();
        }

        private void LoadCatUnitTypes()
        {
            if (_sessionService.Current.Budget.Equals(RevenueExpenditureType.NSQP))
                _txtTitleFirst = string.Format("DỰ TOÁN NGÂN SÁCH QUỐC PHÒNG NĂM {0} - PHẦN THU", _sessionService.Current.YearOfWork.ToString());
            else if (_sessionService.Current.Budget.Equals(RevenueExpenditureType.NSNN))
                _txtTitleFirst = string.Format("DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC NĂM {0} - PHẦN THU", _sessionService.Current.YearOfWork.ToString());
            _txtTitleSecond = "(Kèm theo quyết định)";
            var expenseTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Đồng", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Nghìn đồng", ValueItem = "2"}
            };

            CatUnitTypes = new ObservableCollection<ComboboxItem>(expenseTypes);
            _catUnitTypeSelected = expenseTypes.ElementAt(0);
        }

        private void LoadReportTypes()
        {
            var reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Báo cáo tổng hợp dự toán đầu năm", ValueItem = RevenueExpenditureType.PLAN_REPORT_SUM_TYPE_KEY},
                new ComboboxItem {DisplayItem = "Báo cáo dự toán đầu năm theo từng đơn vị", ValueItem = RevenueExpenditureType.PLAN_REPORT_SUB_TYPE_KEY}
            };

            ReportTypes = new ObservableCollection<ComboboxItem>(reportTypes);
            _reportTypeSelected = reportTypes.ElementAt(0);
        }

        private void LoadDataUnit()
        {
            try
            {
                List<TnDtChungTuChiTiet> chungTuChiTiet = _tnDtChungTuChiTietService.FindAll().ToList();
                chungTuChiTiet = chungTuChiTiet.Where(x => x.IdDonVi != null && !string.IsNullOrEmpty(x.TenDonVi)).GroupBy(x => x.IdDonVi).Select(g => g.First()).ToList();
                DataUnit = _mapper.Map<ObservableCollection<CheckBoxItem>>(chungTuChiTiet);

                _dataUnitView = CollectionViewSource.GetDefaultView(DataUnit);
                _dataUnitView.Filter = ListUnitFilter;

                if (_dataUnit != null && _dataUnit.Count > 0)
                {
                    foreach (var model in _dataUnit)
                    {
                        model.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                            {
                                OnPropertyChanged(nameof(SelectAllUnit));
                                OnPropertyChanged(nameof(SelectedCountUnit));
                            }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListUnitFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchUnit))
            {
                return true;
            }
            return obj is CheckBoxItem item && item.ValueItem.ToLower().Contains(_searchUnit!.ToLower());
        }

        private void LoadDataLNS()
        {
            try
            {
                int yearOfWork = _sessionService.Current.YearOfWork;
                string idDonVi = _sessionService.Current.IdDonVi;
                List<NsMucLucNganSach> listNsMucLucNganSach = new List<NsMucLucNganSach>();

                if (_sessionService.Current.Budget.Equals(RevenueExpenditureType.NSQP))
                {
                    listNsMucLucNganSach = _nsMucLucNganSachService.FindByMLNS(yearOfWork, RevenueExpenditureType.MLNS_QP).ToList();
                }
                else if (_sessionService.Current.Budget.Equals(RevenueExpenditureType.NSNN))
                {
                    listNsMucLucNganSach = _nsMucLucNganSachService.FindByMLNS(yearOfWork, RevenueExpenditureType.MLNS_NN).ToList();
                }

                DataLNS = new ObservableCollection<NsMuclucNgansachModel>();
                DataLNS = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listNsMucLucNganSach);

                _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
                _dataLNSView.Filter = ListLNSFilter;

                DataLNS.Select(c => { c.IsSelected = true; return c; }).ToList();

                if (_dataLNS != null && _dataLNS.Count > 0)
                {
                    foreach (var model in _dataLNS)
                    {
                        model.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                            {
                                foreach (var item in _dataLNS)
                                {
                                    if (item.MlnsIdParent == model.MlnsId)
                                    {
                                        item.IsSelected = model.IsSelected;
                                    }
                                }
                                OnPropertyChanged(nameof(SelectAllLNS));
                                OnPropertyChanged(nameof(SelectedCountLNS));
                            }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListLNSFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.Lns.ToLower().Contains(_searchLNS!.ToLower());
        }

        private void ResetConditionSearch()
        {
            _searchCondition = new EstimationVoucherDetailCriteria
            {
                LNS = BudgetCatalogSelectedToStringConvert.GetValueSelected(DataLNS),
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                BudgetSource = _sessionService.Current.Budget,
                ILoai = RevenueAndExpenditureType.PlanEstimation,
                IdDonVi = CheckboxSelectedToStringConvert.GetValueSelected(_dataUnit)
            };
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.KDuToan = 0;
                    x.KDoanhNghiep = 0;
                    return x;
                }).ToList();

            foreach (var item in Items.Where(x => (x.KDuToan > 0 || x.KDoanhNghiep > 0)))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(TnDtChungTuChiTietReportQuery currentItem, TnDtChungTuChiTietReportQuery seftItem)
        {
            var parrentItem = Items.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.KDuToan += seftItem.KDuToan;
            parrentItem.KDoanhNghiep += seftItem.KDoanhNghiep;
            CalculateParent(parrentItem, seftItem);
        }

        private Dictionary<string, object> HandleData(ExportType exportType = ExportType.PDF)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            ResetConditionSearch();
            List<TnDtChungTuChiTietReportQuery> chungTuReport = _tnDtChungTuChiTietService.FindByPlanBudgetReportCondition(_searchCondition, Int32.Parse(_reportTypeSelected.ValueItem)).ToList();

            chungTuReport = chungTuReport.GroupBy(g => new { g.MlnsId, g.MlnsIdParent, g.Lns, g.L, g.K, g.M, g.Tm, g.Ttm, g.Ng, g.XauNoiMa, g.NoiDung, g.IsHangCha })
                .Select(x => new TnDtChungTuChiTietReportQuery
                {
                    MlnsId = x.Key.MlnsId,
                    MlnsIdParent = x.Key.MlnsIdParent,
                    Lns = x.Key.Lns,
                    L = x.Key.L,
                    K = x.Key.K,
                    M = x.Key.M,
                    Tm = x.Key.Tm,
                    Ttm = x.Key.Ttm,
                    Ng = x.Key.Ng,
                    XauNoiMa = x.Key.XauNoiMa,
                    NoiDung = x.Key.NoiDung,
                    IsHangCha = x.Key.IsHangCha,
                    KDuToan = x.Sum(rpt => rpt.KDuToan),
                    KDoanhNghiep = x.Sum(rpt => rpt.KDoanhNghiep)
                }).ToList();

            Items = new ObservableCollection<TnDtChungTuChiTietReportQuery>(chungTuReport);

            CalculateData();

            chungTuReport = Items.ToList().Where(x => (x.KDuToan > 0 || x.KDoanhNghiep > 0)).ToList();
            chungTuReport.Select(item => { item.FTongDN = (item.KDuToan ?? 0) + (item.KDoanhNghiep ?? 0); return item; }).ToList();

            TnDtChungTuChiTietReportQuery itemSummary = new TnDtChungTuChiTietReportQuery();
            itemSummary.FTongDN = chungTuReport.Where(item => !item.IsHangCha).Sum(item => item.FTongDN);
            itemSummary.KDuToan = chungTuReport.Where(item => !item.IsHangCha).Sum(item => item.KDuToan);
            itemSummary.KDoanhNghiep = chungTuReport.Where(item => !item.IsHangCha).Sum(item => item.KDoanhNghiep);

            data.Add("Cap1", "");
            data.Add("TieuDe1", TxtTitleFirst);
            data.Add("TieuDe2", TxtTitleSecond);
            data.Add("TieuDe3", TxtTitleThird);
            data.Add("Header1", _catUnitTypeSelected.DisplayItem.ToString());
            data.Add("FTongDNSum", itemSummary.FTongDN);
            data.Add("KDuToanSum", itemSummary.KDuToan);
            data.Add("KDoanhNghiepSum", itemSummary.KDoanhNghiep);
            data.Add("Items", chungTuReport);

            FormatNumber formatNumber = new FormatNumber(Int32.Parse(CatUnitTypeSelected.ValueItem), exportType);
            data.Add("FormatNumber", formatNumber);

            return data;
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    if (_reportTypeSelected.ValueItem.Equals(RevenueExpenditureType.PLAN_REPORT_SUM_TYPE_KEY))
                    {
                        Dictionary<string, object> data = HandleData(exportType);

                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_THUNOP);
                        string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<TnDtChungTuChiTietReportQuery>(templateFileName, data);
                        e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                    }
                    else
                    {
                        List<ExportResult> results = new List<ExportResult>();
                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_THUNOP);
                        string fileNamePrefix;
                        string fileNameWithoutExtension;
                        var listUnit = DataUnit.Where(x => x.IsChecked).ToList();
                        if (listUnit.Count > 0)
                        {
                            foreach (var item in listUnit)
                            {
                                EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
                                {
                                    LNS = BudgetCatalogSelectedToStringConvert.GetValueSelected(DataLNS),
                                    YearOfWork = _sessionService.Current.YearOfWork,
                                    YearOfBudget = _sessionService.Current.YearOfBudget,
                                    BudgetSource = _sessionService.Current.Budget,
                                    ILoai = RevenueAndExpenditureType.PlanEstimation,
                                    IdDonVi = item.ValueItem != null ? item.ValueItem : string.Empty
                                };
                                List<TnDtChungTuChiTietReportQuery> chungTuReport = _tnDtChungTuChiTietService.FindByPlanBudgetReportCondition(searchCondition, Int32.Parse(_reportTypeSelected.ValueItem)).ToList();
                                Items = new ObservableCollection<TnDtChungTuChiTietReportQuery>(chungTuReport);

                                CalculateData();

                                chungTuReport = Items.ToList().Where(x => (x.KDuToan > 0 || x.KDoanhNghiep > 0)).ToList();
                                chungTuReport.Select(item => { item.FTongDN = (item.KDuToan ?? 0) + (item.KDoanhNghiep ?? 0); return item; }).ToList();

                                TnDtChungTuChiTietReportQuery itemSummary = new TnDtChungTuChiTietReportQuery();
                                itemSummary.FTongDN = chungTuReport.Where(item => !item.IsHangCha).Sum(item => item.FTongDN);
                                itemSummary.KDuToan = chungTuReport.Where(item => !item.IsHangCha).Sum(item => item.KDuToan);
                                itemSummary.KDoanhNghiep = chungTuReport.Where(item => !item.IsHangCha).Sum(item => item.KDoanhNghiep);

                                Dictionary<string, object> data = new Dictionary<string, object>();
                                data.Add("Cap1", item.DisplayItem);
                                data.Add("TieuDe1", TxtTitleFirst);
                                data.Add("TieuDe2", TxtTitleSecond);
                                data.Add("Header1", _catUnitTypeSelected.DisplayItem.ToString());
                                data.Add("FTongDNSum", itemSummary.FTongDN);
                                data.Add("KDuToanSum", itemSummary.KDuToan);
                                data.Add("KDoanhNghiepSum", itemSummary.KDoanhNghiep);
                                data.Add("Items", chungTuReport);

                                FormatNumber formatNumber = new FormatNumber(Int32.Parse(CatUnitTypeSelected.ValueItem), exportType);
                                data.Add("FormatNumber", formatNumber);

                                fileNamePrefix = string.Format("{0}_{1}", Path.GetFileNameWithoutExtension(templateFileName), item.ValueItem);
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                var xlsFile = _exportService.Export<TnDtChungTuChiTietReportQuery>(templateFileName, data);
                                results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                            }
                        }
                        e.Result = results;
                    }
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        if (e.Result is ExportResult result)
                        {
                            _exportService.Open(result, exportType);
                        }
                        else if (e.Result is List<ExportResult> results)
                        {
                            _exportService.Open(results, exportType);
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

        private void OnConfigSign()
        {
            try
            {
                DmChuKyModel chuKyModel = new DmChuKyModel();
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_THUNOPNGANSACH_LAPDUTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                if (_dmChuKy == null)
                {
                    chuKyModel.IdType = TypeChuKy.RPT_NS_THUNOPNGANSACH_LAPDUTOAN;
                }
                else
                {
                    chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
                }

                DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
                DmChuKyDialogViewModel.SavedAction = obj =>
                {
                    DmChuKyModel chuKy = (DmChuKyModel)obj;
                    if (chuKy.TieuDe1MoTa != null)
                    {
                        TxtTitleFirst = chuKy.TieuDe1MoTa;
                    }
                    if (chuKy.TieuDe2MoTa != null)
                    {
                        TxtTitleSecond = chuKy.TieuDe2MoTa;
                    }
                };

                DmChuKyDialogViewModel.Init();
                DmChuKyDialogViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
