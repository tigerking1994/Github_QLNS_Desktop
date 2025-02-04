using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Report;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate
{
    public class ReportDivisionCurrentViewModel : DetailViewModelBase<DtChungTuModel, DtChungTuChiTietModel>
    {
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly ISessionService _sessionService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private IMapper _mapper;
        private EstimationVoucherDetailCriteria _searchCondition;
        private ICollectionView _budgetCatalogItemsView;
        private ICollectionView _chungTuChiTietItemsView;
        private ICollectionView _divisionModelsView;
        private List<NsMucLucNganSach> _listMLNS;

        public override string GroupName => "BÁO CÁO - THỐNG KÊ";
        public override string Name => "Số phân bổ hiện tại";
        public override Type ContentType => typeof(View.Budget.Estimate.ReportDivisionCurrent);
        public override PackIconKind IconKind => PackIconKind.Report;

        private ObservableCollection<DtChungTuModel> _divisionModels;
        public ObservableCollection<DtChungTuModel> DivisionModels
        {
            get => _divisionModels;
            set => SetProperty(ref _divisionModels, value);
        }
        public bool? IsAllDivisionModelsChecked
        {
            get => DivisionModels.All(x => x.IsChecked);
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, DivisionModels);
                    ResetConditionSearchSummary();
                }
            }
        }
        private string _searchDivisionModel;
        public string SearchDivisionModel
        {
            get => _searchDivisionModel;
            set
            {
                SetProperty(ref _searchDivisionModel, value);
                if (value != null)
                {
                    _divisionModelsView.Refresh();
                }

            }
        }

        private DtChungTuModel _selectedDivisionModel;
        public DtChungTuModel SelectedDivisionModel
        {
            get => _selectedDivisionModel;
            set
            {
                SetProperty(ref _selectedDivisionModel, value);
            }
        }

        private string _selectedDataDot;
        public string SelectedDataDot
        {
            get => _selectedDataDot;
            set => SetProperty(ref _selectedDataDot, value);
        }

        private EstimationDetailCriteria _detailFilter;
        public EstimationDetailCriteria DetailFilter
        {
            get => _detailFilter;
            set => SetProperty(ref _detailFilter, value);
        }

        private ObservableCollection<DtChungTuChiTietModel> _chungTuChiTietItems;
        public ObservableCollection<DtChungTuChiTietModel> ChungTuChiTietItems
        {
            get => _chungTuChiTietItems;
            set => SetProperty(ref _chungTuChiTietItems, value);
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _budgetCatalogItemsView.Refresh();
                }
            }
        }

        private ObservableCollection<NsMucLucNganSach> _budgetCatalogItems;
        public ObservableCollection<NsMucLucNganSach> BudgetCatalogItems
        {
            get => _budgetCatalogItems;
            set => SetProperty(ref _budgetCatalogItems, value);
        }

        private NsMucLucNganSach _selectedBudgetCatalog;
        public NsMucLucNganSach SelectedBudgetCatalog
        {
            get => _selectedBudgetCatalog;
            set
            {
                if (SetProperty(ref _selectedBudgetCatalog, value))
                {
                    if (_selectedBudgetCatalog != null)
                        SelectedLNS = _selectedBudgetCatalog.Lns;

                    _chungTuChiTietItemsView.Refresh();
                    CaculateTotal();
                }

                IsOpenLnsPopup = false;
            }
        }

        private string _selectedLNS;
        public string SelectedLNS
        {
            get => _selectedLNS;
            set => SetProperty(ref _selectedLNS, value);
        }

        private bool _isOpenLnsPopup;
        public bool IsOpenLnsPopup
        {
            get => _isOpenLnsPopup;
            set => SetProperty(ref _isOpenLnsPopup, value);
        }

        private bool _isOpenDotPopup;
        public bool IsOpenDotPopup
        {
            get => _isOpenDotPopup;
            set => SetProperty(ref _isOpenDotPopup, value);
        }

        private double _totalTuChi;
        public double TotalTuChi
        {
            get => _totalTuChi;
            set => SetProperty(ref _totalTuChi, value);
        }

        private double _totalHienVat;
        public double TotalHienVat
        {
            get => _totalHienVat;
            set => SetProperty(ref _totalHienVat, value);
        }

        private double _totalTuChiDaCap;
        public double TotalTuChiDaCap
        {
            get => _totalTuChiDaCap;
            set => SetProperty(ref _totalTuChiDaCap, value);
        }

        private double _totalHienVatDaCap;
        public double TotalHienVatDaCap
        {
            get => _totalHienVatDaCap;
            set => SetProperty(ref _totalHienVatDaCap, value);
        }

        private double _totalTuChiConLai;
        public double TotalTuChiConLai
        {
            get => _totalTuChiConLai;
            set => SetProperty(ref _totalTuChiConLai, value);
        }

        private double _totalHienVatConLai;
        public double TotalHienVatConLai
        {
            get => _totalHienVatConLai;
            set => SetProperty(ref _totalHienVatConLai, value);
        }

        public ReportDivisionCurrentDialogViewModel ReportDivisionCurrentDialogViewModel { get; set; }
        public ReportDivisionCurrentBatchDialogViewModel ReportDivisionCurrentBatchDialogViewModel { get; set; }

        public RelayCommand ShowPrintSpendCommand { get; }
        public RelayCommand ShowPrintDuPhongCommand { get; }
        public RelayCommand ShowPrintBatchCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetSearchCommand { get; set; }
        public RelayCommand GetDataCommand { get; set; }

        public ReportDivisionCurrentViewModel(INsDtChungTuService dtChungTuService,
            ISessionService sessionService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsMucLucNganSachService mucLucNganSachService,
            IMapper mapper,
            IDanhMucService danhMucService,
            ReportDivisionCurrentDialogViewModel reportDivisionCurrentDialogViewModel,
            ReportDivisionCurrentBatchDialogViewModel reportDivisionCurrentBatchDialogViewModel) : base(danhMucService, sessionService)
        {
            _dtChungTuService = dtChungTuService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _sessionService = sessionService;
            _mucLucNganSachService = mucLucNganSachService;
            _mapper = mapper;

            ReportDivisionCurrentDialogViewModel = reportDivisionCurrentDialogViewModel;

            SearchCommand = new RelayCommand(obj => { _chungTuChiTietItemsView.Refresh(); });
            ShowPrintSpendCommand = new RelayCommand(obj => OnShowPrintSpend(obj));
            ShowPrintDuPhongCommand = new RelayCommand(obj => OnShowPrintDuPhong(obj));
            ShowPrintBatchCommand = new RelayCommand(obj => OnShowPrintBatch(obj));
            RefreshCommand = new RelayCommand(obj => Init());
            ResetSearchCommand = new RelayCommand(obj => OnResetSearch());
            GetDataCommand = new RelayCommand(obj => OnGetData());
            ReportDivisionCurrentBatchDialogViewModel = reportDivisionCurrentBatchDialogViewModel;
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(10);
            _listMLNS = _mucLucNganSachService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();
            LoadDataDot();
        }

        private void LoadDataDot()
        {
            var predicate = CreatePredicate();
            IEnumerable<NsDtChungTu> listChungTu = _dtChungTuService.FindByCondition(predicate).OrderBy(x => x.DNgayChungTu);

            DivisionModels = _mapper.Map<ObservableCollection<DtChungTuModel>>(listChungTu);

            foreach (var model in DivisionModels)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    var isCheckedAll = new StackTrace().GetFrames().Select(x => x.GetMethod()).Any(x => x.Name == "SelectAll");
                    if (args.PropertyName == nameof(DtChungTuModel.IsChecked) && !isCheckedAll)
                    {
                        ResetConditionSearchSummary();
                    }
                };
            }

            if (DivisionModels != null && DivisionModels.Count > 0)
            {
                SelectedDivisionModel = DivisionModels.OrderBy(x => x.DNgayQuyetDinh).ThenBy(x => x.DNgayChungTu).LastOrDefault();
                SelectedDivisionModel.IsChecked = true;
                LoadDataGrid();
            }

            _divisionModelsView = CollectionViewSource.GetDefaultView(DivisionModels);
            _divisionModelsView.Filter = DivisionModelsFilter;
        }

        private bool DivisionModelsFilter(object obj)
        {
            if (string.IsNullOrEmpty(SearchDivisionModel))
                return true;

            return obj is DtChungTuModel item && item.SSoChungTu.ToLower().Contains(SearchDivisionModel!.ToLower());
        }

        private void ResetConditionSearch()
        {
            DetailFilter = new EstimationDetailCriteria();
            if (SelectedDivisionModel != null)
            {
                _searchCondition = new EstimationVoucherDetailCriteria
                {
                    VoucherId = SelectedDivisionModel.Id,
                    LNS = SelectedDivisionModel.SDslns,
                    YearOfWork = SelectedDivisionModel.INamLamViec,
                    YearOfBudget = SelectedDivisionModel.INamNganSach,
                    BudgetSource = SelectedDivisionModel.IIdMaNguonNganSach,
                    VoucherDate = SelectedDivisionModel.DNgayChungTu,
                    ILoai = SelectedDivisionModel.ILoai,
                    IdDotNhan = SelectedDivisionModel.IIdDotNhan,
                    SoChungTu = SelectedDivisionModel.SSoChungTu
                };
            }
        }

        private void ResetConditionSearchSummary()
        {
            DetailFilter = new EstimationDetailCriteria();
            if (DivisionModels.Any(x => x.IsChecked))
            {
                var itemsChecked = DivisionModels.Where(x => x.IsChecked);
                _searchCondition = new EstimationVoucherDetailCriteria
                {
                    VoucherIds = string.Join(",", itemsChecked.Select(x => x.Id)),
                    LNS = string.Join(",", itemsChecked.SelectMany(x => x.SDslns.Split(",")).Distinct()),
                    YearOfWork = _sessionService.Current.YearOfWork,
                    YearOfBudget = _sessionService.Current.YearOfBudget,
                    BudgetSource = _sessionService.Current.Budget,
                    VoucherDate = itemsChecked.Max(x => x.DNgayChungTu),
                    IdDotNhan = string.Join(",", itemsChecked.Select(x => x.IIdDotNhan)),
                    SoChungTu = string.Join(",", itemsChecked.Select(x => x.SSoChungTu)),
                };
            }
            else
            {
                _searchCondition = null;
            }
        }

        private void GroupBy(ref IEnumerable<NsDtChungTuChiTietQuery> listData)
        {
            listData = listData.GroupBy(g => new
            {
                g.IIdMlns,
                g.IIdMlnsCha,
                g.SXauNoiMa,
                g.SLns,
                g.SL,
                g.SK,
                g.SM,
                g.STm,
                g.STtm,
                g.SNg,
                g.STng,
                g.STng1,
                g.STng2,
                g.STng3,
                g.SMoTa,
                g.BHangChaDuToan
            }).Select(x => new NsDtChungTuChiTietQuery
            {
                IIdMlns = x.Key.IIdMlns,
                IIdMlnsCha = x.Key.IIdMlnsCha,
                SXauNoiMa = x.Key.SXauNoiMa,
                SLns = x.Key.SLns,
                SL = x.Key.SL,
                SK = x.Key.SK,
                SM = x.Key.SM,
                STm = x.Key.STm,
                STtm = x.Key.STtm,
                SNg = x.Key.SNg,
                STng = x.Key.STng,
                STng1 = x.Key.STng1,
                STng2 = x.Key.STng2,
                STng3 = x.Key.STng3,
                SMoTa = x.Key.SMoTa,
                FTuChi = x.Sum(x => x.FTuChi),
                FHienVat = x.Sum(x => x.FHienVat),
                FHangNhap = x.Sum(x => x.FHangNhap),
                FHangMua = x.Sum(x => x.FHangMua),
                FPhanCap = x.Sum(x => x.FPhanCap),
                FDuPhong = x.Sum(x => x.FDuPhong),
                BHangChaDuToan = x.Key.BHangChaDuToan
            });
        }

        private void LoadDataGrid()
        {
            if (_searchCondition == null)
            {
                ChungTuChiTietItems = null;
                return;
            }
            _searchCondition.IsPhanBo = false;
            var estimate = _dtChungTuChiTietService.FindByLuyKeTongHopSummary(_searchCondition);
            _searchCondition.IsPhanBo = true;
            var division = _dtChungTuChiTietService.FindByLuyKeTongHopSummary(_searchCondition);
            //var division = _dtChungTuChiTietService.FindByLuyKePhanBoTongHopSummary(_searchCondition);

            //GroupBy(ref estimate);
            //GroupBy(ref division);


            var listChungTuChiTiet = (from e in estimate
                                      join d in division
                                      on new { e.SXauNoiMa, e.SLns, e.SL, e.SK, e.SM, e.STm, e.STtm, e.SNg, e.STng, e.STng1, e.STng2, e.STng3 }
                                      equals new { d.SXauNoiMa, d.SLns, d.SL, d.SK, d.SM, d.STm, d.STtm, d.SNg, d.STng, d.STng1, d.STng2, d.STng3 }
                                      into result
                                      from r in result.DefaultIfEmpty()
                                      select new DtChungTuChiTietModel()
                                      {
                                          IIdMlns = e.IIdMlns,
                                          IIdMlnsCha = e.IIdMlnsCha,
                                          SXauNoiMa = e.SXauNoiMa,
                                          SLns = e.SLns,
                                          SL = e.SL,
                                          SK = e.SK,
                                          SM = e.SM,
                                          STm = e.STm,
                                          STtm = e.STtm,
                                          SNg = e.SNg,
                                          STng = e.STng,
                                          STng1 = e.STng1,
                                          STng2 = e.STng2,
                                          STng3 = e.STng3,
                                          SMoTa = e.SMoTa,
                                          FTuChi = e.FTuChi,
                                          FHienVat = e.FHienVat,
                                          TuChiDaCap = r?.FTuChi ?? 0,
                                          HienVatDaCap = r?.FHienVat ?? 0,
                                          FHangNhap = e.FHangNhap,
                                          FHangMua = e.FHangMua,
                                          FPhanCap = e.FPhanCap,
                                          FDuPhong = e.FDuPhong,
                                          IsHangCha = e.BHangChaDuToan
                                      });

            ChungTuChiTietItems = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(listChungTuChiTiet);
            CalculateData();
            _chungTuChiTietItemsView = CollectionViewSource.GetDefaultView(ChungTuChiTietItems);
            _chungTuChiTietItemsView.Filter = ChungTuChiTietItemsViewFilter;
            LoadLNSIndexCondition();
        }

        private bool ChungTuChiTietItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (DtChungTuChiTietModel)obj;
            if (!string.IsNullOrEmpty(SelectedLNS))
                result = result && item.SLns.ToLower().Contains(SelectedLNS.ToLower());

            if (!string.IsNullOrEmpty(DetailFilter.L))
                result = result && item.SL.ToLower().Contains(DetailFilter.L.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.K))
                result = result && item.SK.ToLower().Contains(DetailFilter.K.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.SM.ToLower().Contains(DetailFilter.M.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.STm.ToLower().Contains(DetailFilter.TM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.STtm.ToLower().Contains(DetailFilter.TTM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.SNg.ToLower().Contains(DetailFilter.NG.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG))
                result = result && item.STng.ToLower().Contains(DetailFilter.TNG.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG1))
                result = result && item.STng1.ToLower().Contains(DetailFilter.TNG1.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG2))
                result = result && item.STng2.ToLower().Contains(DetailFilter.TNG2.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG3))
                result = result && item.STng3.ToLower().Contains(DetailFilter.TNG3.ToLower());

            result = result && (item.FTuChi != 0 || item.FHienVat != 0 || item.TuChiDaCap != 0 || item.HienVatDaCap != 0 || item.TuChiConLai != 0 || item.HienVatConLai != 0);

            item.IsFilter = result;

            return result;
        }

        private static void SelectAll(bool select, ObservableCollection<DtChungTuModel> models)
        {
            foreach (var model in models)
            {
                model.IsChecked = select;
            }
        }

        private void LoadLNSIndexCondition()
        {
            List<NsMucLucNganSach> listMLNS = _listMLNS.Where(x => _chungTuChiTietItems.Where(x => x.FTuChi != 0 || x.FHienVat != 0 || x.TuChiDaCap != 0 || x.HienVatDaCap != 0).Select(x => x.SLns).ToList().Contains(x.Lns)).ToList();
            listMLNS.Insert(0, new NsMucLucNganSach
            {
                Lns = string.Empty,
                MoTa = "-- TẤT CẢ --"
            });
            BudgetCatalogItems = new ObservableCollection<NsMucLucNganSach>(listMLNS);
            _budgetCatalogItemsView = CollectionViewSource.GetDefaultView(BudgetCatalogItems);
            _budgetCatalogItemsView.Filter = BudgetCatalogItemsFilter;
        }

        private bool BudgetCatalogItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is DtChungTuChiTietModel item && item.SLns.ToLower().Contains(_searchLNS!.ToLower());
        }

        private void CalculateData()
        {
            _chungTuChiTietItems.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHienVat = 0;
                    x.TuChiDaCap = 0;
                    x.HienVatDaCap = 0;
                    return x;
                }).ToList();

            var item1 = _chungTuChiTietItems.Where(x => (x.FTuChi != 0 || x.FHienVat != 0 || x.TuChiDaCap != 0 || x.HienVatDaCap != 0 || x.TuChiConLai != 0 || x.HienVatConLai != 0)).ToList();
            foreach (var item in _chungTuChiTietItems.Where(x => !x.IsHangCha && x.IsFilter && (x.FTuChi != 0 || x.FHienVat != 0 || x.TuChiDaCap != 0 || x.HienVatDaCap != 0 || x.TuChiConLai != 0 || x.HienVatConLai != 0)))
            {
                CalculateParent(item, item);
            }

            CaculateTotal();
        }

        private void CalculateParent(DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem)
        {
            var parrentItem = _chungTuChiTietItems.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FTuChi += seftItem.FTuChi;
            parrentItem.FHienVat += seftItem.FHienVat;
            parrentItem.TuChiDaCap += seftItem.TuChiDaCap;
            parrentItem.HienVatDaCap += seftItem.HienVatDaCap;
            CalculateParent(parrentItem, seftItem);
        }

        private void CaculateTotal()
        {
            TotalTuChi = 0;
            TotalHienVat = 0;
            TotalTuChiConLai = 0;
            TotalHienVatConLai = 0;
            TotalTuChiDaCap = 0;
            TotalHienVatDaCap = 0;
            var listChildren = _chungTuChiTietItems.Where(x => !x.IsHangCha && !x.IsDeleted && (x.FTuChi != 0 || x.FHienVat != 0 || x.TuChiDaCap != 0 || x.HienVatDaCap != 0
                                    || x.TuChiConLai != 0 || x.HienVatConLai != 0) && x.IsFilter).ToList();
            foreach (var item in listChildren)
            {
                TotalTuChi += item.FTuChi;
                TotalHienVat += item.FHienVat;
                TotalTuChiConLai += item.TuChiConLai;
                TotalHienVatConLai += item.HienVatConLai;
                TotalTuChiDaCap += item.TuChiDaCap;
                TotalHienVatDaCap += item.HienVatDaCap;
            }
        }

        private void OnGetData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    LoadDataGrid();
                }, (s, e) =>
                {
                    IsLoading = false;
                });
            }
            catch
            {
                IsLoading = false;
            }
        }
        private void OnResetSearch()
        {
            SelectedLNS = string.Empty;
            DetailFilter = new EstimationDetailCriteria();
            _chungTuChiTietItemsView.Refresh();
        }

        private async void OnShowPrintSpend(object obj)
        {
            ReportDivisionCurrentDialogViewModel.IsSwitch = true;
            ReportDivisionCurrentDialogViewModel.SoChungTu = SelectedDivisionModel.SSoChungTu;
            ReportDivisionCurrentDialogViewModel.TypePrintSelected = false;
            ReportDivisionCurrentDialogViewModel.Init();
            var view = new ReportDivisionCurrentDialog
            {
                DataContext = ReportDivisionCurrentDialogViewModel
            };
            await DialogHost.Show(view, "RootDialog", null, null);
        }

        private async void OnShowPrintDuPhong(object obj)
        {
            ReportDivisionCurrentDialogViewModel.IsSwitch = false;
            ReportDivisionCurrentDialogViewModel.SoChungTu = SelectedDivisionModel.SSoChungTu;
            ReportDivisionCurrentDialogViewModel.TypePrintSelected = true;
            ReportDivisionCurrentDialogViewModel.Init();
            var view = new ReportDivisionCurrentDialog
            {
                DataContext = ReportDivisionCurrentDialogViewModel
            };
            await DialogHost.Show(view, "RootDialog", null, null);
        }

        private async void OnShowPrintBatch(object obj)
        {
            ReportDivisionCurrentBatchDialogViewModel.IsSwitch = false;
            ReportDivisionCurrentBatchDialogViewModel.SoChungTu = SelectedDivisionModel.SSoChungTu;
            ReportDivisionCurrentBatchDialogViewModel.TypePrintSelected = true;
            ReportDivisionCurrentBatchDialogViewModel.Init();
            var view = new ReportDivisionCurrentBatchDialog
            {
                DataContext = ReportDivisionCurrentBatchDialogViewModel
            };
            await DialogHost.Show(view, "RootDialog", null, null);
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.SDsidMaDonVi == _sessionService.Current.IdDonVi);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            return predicate;
        }
    }
}
