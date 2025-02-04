using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.Budget.CollectionsBudget.Plan;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Import;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Plan;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Plan.PrintPlanReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Plan;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Plan.PrintPlanReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.RevenueExpenditureType;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.App.Model.Report;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.CollectionsBudget.Plan
{
    public class PlanBudgetBeginYearIndexViewModel : GridViewModelBase<TnDtdnChungTuModel>
    {
        private readonly ITnDtChungTuService _tnDtChungTuService;
        private readonly ITnDtdnChungTuService _tnDtdnChungTuService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly INsDonViService _donViService;
        private readonly ITnDtChungTuChiTietService _tnDtChungTuChiTietService;
        private readonly ITnDtdnChungTuChiTietService _tnDtdnChungTuChiTietService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IDanhMucService _danhMucService;

        private readonly ILog _logger;
        private IExportService _exportService;
        private ICollectionView _tnDtChungTuView;
        private TnDtChungTu _aggregateVoucher;
        private DonVi _aggregateAgency;
        private TnDtdnChungTuModel Model = new TnDtdnChungTuModel();
        public override string Name => "Lập dự toán đầu năm";
        public override string Description => "Lập dự toán đầu năm";
        public override Type ContentType => typeof(PlanBudgetBeginYearIndex);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public override string FuncCode => NSFunctionCode.BUDGET_REVENUE_EXPENDITURE_PLAN;

        //private bool _isEdit;
        //public bool IsEdit
        //{
        //    get => _isEdit;
        //    set => SetProperty(ref _isEdit, value);
        //}
        public bool IsLock => TabIndex == ImportTabIndex.Data ? IsEnableLock && Items.FirstOrDefault(x => x.Selected).BKhoa : IsEnableLock && Items.FirstOrDefault(x => x.IsSummaryVocher && x.Selected).BKhoa;
        public bool IsEdit => TabIndex == ImportTabIndex.Data ? Items.Any(x => x.Selected) && (Items.Where(x => x.Selected).Count() == NSConstants.DEFAULT_INDEX) && !Items.FirstOrDefault(x => x.Selected).BKhoa : Items.Any(x => x.IsSummaryVocher && x.Selected) && (Items.Where(x => x.IsSummaryVocher && x.Selected).Count() == NSConstants.DEFAULT_INDEX) && !Items.FirstOrDefault(x => x.IsSummaryVocher && x.Selected).BKhoa;
        public bool IsEnableLock => TabIndex == ImportTabIndex.Data ? Items.Any(x => x.Selected) && (Items.Where(x => x.Selected).Select(s => s.BKhoa).Distinct().Count() == NSConstants.DEFAULT_INDEX) : Items.Any(x => x.IsSummaryVocher && x.Selected) && (Items.Where(x => x.IsSummaryVocher && x.Selected).Select(s => s.BKhoa).Distinct().Count() == NSConstants.DEFAULT_INDEX);

        public Visibility VisibilityNsqpPrint
        {
            get => _sessionService.Current.Budget.Equals(RevenueExpenditureType.NSQP) ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility VisibilityNsnnPrint
        {
            get => _sessionService.Current.Budget.Equals(RevenueExpenditureType.NSNN) ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool IsCensorship
        {
            get
            {
                var itemSelected = Items.Where(x => x.Selected);
                return itemSelected.Any() && itemSelected.All(x => !x.IsSummaryVocher && x.BKhoa) && itemSelected.Select(x => x.IIdMaNguonNganSach).Distinct().Count() == 1;
            }
        }
        public bool IsExportAggregateData => Items.Where(x => x.Selected).ToList().Count > 0 ? true : false;

        #region Censorships
        private List<ComboboxItem> _censorships;
        public List<ComboboxItem> Censorships
        {
            get => _censorships;
            set => SetProperty(ref _censorships, value);
        }

        private ComboboxItem _censorshipSelected;
        public ComboboxItem CensorshipSelected
        {
            get => _censorshipSelected;
            set
            {
                SetProperty(ref _censorshipSelected, value);
                if (_tnDtChungTuView != null)
                    _tnDtChungTuView.Refresh();
            }
        }

        public bool? IsAllItemSummariesSelected
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
                    if (Items != null)
                    {
                        Items.Where(x => (x.BDaTongHop is null || (x.BDaTongHop.HasValue && !x.BDaTongHop.Value)) && x.IsFilter).ForAll(c => c.Selected = value.Value);
                        OnPropertyChanged();
                        OnPropertyChanged(nameof(IsEnableLock));
                        OnPropertyChanged(nameof(IsLock));
                        OnPropertyChanged(nameof(IsEdit));
                    }
                }
            }
        }

        #endregion

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }


        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadData();
            }
        }

        private string _thucHienThu;
        public string ThucHienThu
        {
            get => _thucHienThu;
            set
            {
                SetProperty(ref _thucHienThu, value);
            }
        }
        private string _duToanNam;
        public string DuToanNam
        {
            get => _duToanNam;
            set
            {
                SetProperty(ref _duToanNam, value);
            }
        }
        private string _uocThucHien;
        public string UocThucHien
        {
            get => _uocThucHien;
            set
            {
                SetProperty(ref _uocThucHien, value);
            }
        }
        private string _duToanThu;
        public string DuToanThu
        {
            get => _duToanThu;
            set
            {
                SetProperty(ref _duToanThu, value);
            }
        }
        /// <summary>
        /// Checkbox select all property
        /// </summary>
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

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }


        private ObservableCollection<ComboboxItem> _lockStatus = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> LockStatus
        {
            get => _lockStatus;
            set => SetProperty(ref _lockStatus, value);
        }

        private ComboboxItem _lockStatusSelected;

        public ComboboxItem LockStatusSelected
        {
            get => _lockStatusSelected;
            set
            {
                SetProperty(ref _lockStatusSelected, value);
                OnRefresh();
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            if (SelectedItem == null)
            {
               // IsEdit = false;
            }
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEnableLock));
        }

        public PlanBudgetBeginYearDialogViewModel PlanBudgetBeginYearDialogViewModel { get; set; }
        public PlanBudgetBeginYearDetailViewModel PlanBudgetBeginYearDetailViewModel { get; set; }
        public PrintPlanBudgetReportViewModel PrintPlanBudgetReportViewModel { get; set; }
        public PlanBudgetBeginYearImportViewModel PlanBudgetBeginYearImportViewModel { get; set; }
        public PlanBudgetBeginYearSummaryViewModel PlanBudgetBeginYearSummaryViewModel { get; set; }
        public PlanBudgetBeginYearReportViewModel PlanBudgetBeginYearReportViewModel { get; set; }
        public PlanBudgetBeginYearReportSummaryViewModel PlanBudgetBeginYearReportSummaryViewModel { get; set; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand SendCensorshipCommand { get; }
        public RelayCommand AcceptCensorship { get; }
        public RelayCommand DenyCensorship { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand ExportAggregateDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand SelectionChangedCommand { get; }

        public PlanBudgetBeginYearIndexViewModel(IMapper mapper,
            ISessionService sessionService,
            ITnDtChungTuService tnDtChungTuService,
            INsDonViService nsDonViService,
            ITnDtChungTuChiTietService tnDtChungTuChiTietService,
            INsMucLucNganSachService nsMucLucNganSachService,
            IExportService exportService,
            ILog logger,
            ITnDtdnChungTuService tnDtdnChungTuService,
            IDanhMucService danhMucService,
            ITnDtdnChungTuChiTietService tnDtdnChungTuChiTietService,
            PlanBudgetBeginYearDialogViewModel planBudgetBeginYearDialogViewModel,
            PlanBudgetBeginYearDetailViewModel planBudgetBeginYearDetailViewModel,
            PrintPlanBudgetReportViewModel printPlanBudgetReportViewModel,
            PlanBudgetBeginYearImportViewModel planBudgetBeginYearImportViewModel,
            PlanBudgetBeginYearSummaryViewModel planBudgetBeginYearSummaryViewModel,
            PlanBudgetBeginYearReportViewModel planBudgetBeginYearReportViewModel,
            PlanBudgetBeginYearReportSummaryViewModel planBudgetBeginYearReportSummaryViewModel)
        {
            _tnDtChungTuService = tnDtChungTuService;
            _tnDtdnChungTuService = tnDtdnChungTuService;
            _sessionService = sessionService;
            _donViService = nsDonViService;
            _tnDtChungTuChiTietService = tnDtChungTuChiTietService;
            _mucLucNganSachService = nsMucLucNganSachService;
            _tnDtdnChungTuChiTietService = tnDtdnChungTuChiTietService;
            _danhMucService = danhMucService;
            _exportService = exportService;
            _logger = logger;
            _mapper = mapper;

            PlanBudgetBeginYearDialogViewModel = planBudgetBeginYearDialogViewModel;
            PlanBudgetBeginYearDetailViewModel = planBudgetBeginYearDetailViewModel;
            PrintPlanBudgetReportViewModel = printPlanBudgetReportViewModel;
            PlanBudgetBeginYearImportViewModel = planBudgetBeginYearImportViewModel;
            PlanBudgetBeginYearSummaryViewModel = planBudgetBeginYearSummaryViewModel;
            PlanBudgetBeginYearReportViewModel = planBudgetBeginYearReportViewModel;
            PlanBudgetBeginYearReportSummaryViewModel = planBudgetBeginYearReportSummaryViewModel;

            PrintActionCommand = new RelayCommand(obj => OnPrint(obj));
            SendCensorshipCommand = new RelayCommand(obj => OnSendCensorship());
            AcceptCensorship = new RelayCommand(obj => OnAcceptCensorship());
            DenyCensorship = new RelayCommand(obj => OnDenyCensorship());
            AggregateCommand = new RelayCommand(obj => OnAggregate());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportData());
            ImportDataCommand = new RelayCommand(obj => OnImportData(obj));
            SearchCommand = new RelayCommand(obj => _tnDtChungTuView.Refresh());
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);

        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            _tabIndex = ImportTabIndex.Data;
            LoadHeader();
            LoadLockStatus();
            ResetCondition();
            LoadData();
            PlanBudgetBeginYearDetailViewModel.UpdateSettlementVoucherEvent += RefreshAfterSaveData;
        }

        private void LoadLockStatus()
        {
            var lockStatus = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Tất cả", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đã khóa", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Chưa khóa", ValueItem = "2"},
            };

            LockStatus = new ObservableCollection<ComboboxItem>(lockStatus);
            LockStatusSelected = LockStatus.ElementAt(0);
        }
        private void LoadHeader()
        {
            var year = _sessionService.Current.YearOfWork;
            _thucHienThu = $"Thực hiện thu năm {year - 2}";
            _duToanNam = $"Dự toán năm {year - 1}";
            _uocThucHien = $"Ước thực hiện năm {year - 1}";
            _duToanThu = $"Dự toán thu năm kế hoạch {year}";
        }

        private void ResetCondition()
        {
            _censorshipSelected = null;

            OnPropertyChanged(nameof(CensorshipSelected));
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                LoadCensorships();
                LoadAgencies();

                var predicate = CreatePredicate();
                List<TnDtdnChungTu> listChungTu = _tnDtdnChungTuService.FindByCondition(predicate).ToList();
                Items = _mapper.Map<ObservableCollection<TnDtdnChungTuModel>>(listChungTu);
                if (TabIndex == ImportTabIndex.Data)
                {
                    Items = _mapper.Map<ObservableCollection<TnDtdnChungTuModel>>(listChungTu.Where(x => !x.IIdMaDonVi.Equals(_sessionService.Current.IdDonVi) && !x.BDaTongHop.GetValueOrDefault()).OrderByDescending(o => o.ISoChungTuIndex));

                    foreach (var item in Items.Select((value, index) => (value, index)))
                    {
                        item.value.SoThuTu = (item.index + 1).ToString();
                    }
                }
                else
                {
                    var listCTTongHop = listChungTu.Where(x => x.IIdMaDonVi.Equals(_sessionService.Current.IdDonVi)).OrderByDescending(o => o.ISoChungTuIndex).ToList();

                    var listCTTongHopRoot = _mapper.Map<List<TnDtdnChungTuModel>>(listCTTongHop);
                    var listTongHop = new List<TnDtdnChungTuModel>();

                    foreach (var item in listCTTongHopRoot.Select((value, index) => (value, index)))
                    {
                        var parent = item.value;
                        parent.SoThuTu = (item.index + 1).ToString();
                        parent.IsExpand = true;
                        listTongHop.Add(parent);
                        if (!string.IsNullOrEmpty(parent.SDSSoChungTuTongHop))
                        {
                            var listChild = _mapper.Map<List<TnDtdnChungTuModel>>(listChungTu.Where(x => parent.SDSSoChungTuTongHop != null && parent.SDSSoChungTuTongHop.Contains(x.SSoChungTu)));
                            listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = parent.SSoChungTu; });
                            listTongHop.AddRange(listChild);
                        }
                    }
                    Items = _mapper.Map<ObservableCollection<TnDtdnChungTuModel>>(listTongHop);
                }
                SelectedItem = Items.FirstOrDefault();
                _tnDtChungTuView = CollectionViewSource.GetDefaultView(Items);
                _tnDtChungTuView.Filter = ListSettlementVoucherFilter;
                ChangeProperty();

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ChangeProperty()
        {
            foreach (var model in Items)
            {
                model.TypeIcon = model.BSent.GetValueOrDefault(false) ? "CheckBold" : "CancelBold";
                if (model.IIdMaDonVi == _sessionService.Current.IdDonVi)
                {
                    model.IsSummaryVocher = true;
                }
                if (!string.IsNullOrEmpty(model.IIdMaDonVi))
                {
                    model.SMaTenDonVi = Agencies.FirstOrDefault(y => model.IIdMaDonVi.Equals(y.ValueItem))?.DisplayItem ?? string.Empty;
                    model.STenDonVi = Agencies.FirstOrDefault(y => model.IIdMaDonVi.Equals(y.ValueItem))?.DisplayItemOption2 ?? string.Empty;
                }
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(TnDtdnChungTuModel.Selected))
                    {
                        OnPropertyChanged(nameof(IsCensorship));
                        OnPropertyChanged(nameof(IsExportAggregateData));
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                        OnPropertyChanged(nameof(IsEnableLock));
                        OnPropertyChanged(nameof(IsLock));
                        OnPropertyChanged(nameof(IsEdit));
                    }
                    if (args.PropertyName == nameof(TnDtdnChungTuModel.IsCollapse))
                    {
                        ExpandChild();
                    }
                };
            }
        }

        private void ExpandChild()
        {
            if (!Items.IsEmpty())
            {
                Items.Where(n => n.SoChungTuParent == SelectedItem.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
            OnPropertyChanged(nameof(Items));
        }

        private bool ListSettlementVoucherFilter(object obj)
        {
            bool result = true;
            var item = (TnDtdnChungTuModel)obj;
            //if (CensorshipSelected != null)
            //    result = result && item.IKiemDuyet.Value == int.Parse(CensorshipSelected.ValueItem);
            //if (!string.IsNullOrEmpty(SearchText))
            //    result = result && item.SoChungTu.ToLower().Contains(SearchText.ToLower());
            if (LockStatusSelected != null)
            {
                if (LockStatusSelected.ValueItem.Equals("1"))
                {
                    result = result && item.BKhoa;
                }
                if (LockStatusSelected.ValueItem.Equals("2"))
                {
                    result = result && item.BKhoa == false;
                }
            }
            item.IsFilter = result;
            return result;
        }

        public Expression<Func<TnDtdnChungTu, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<TnDtdnChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);

            return predicate;
        }

        private void LoadCensorships()
        {
            _censorships = new List<ComboboxItem>();
            if (!_sessionService.Current.Authorities.Contains(Role.TRO_LY_TONG_HOP))
                _censorships.Add(new ComboboxItem("Mới", ((int)Censorship.NEW).ToString()));

            _censorships.Add(new ComboboxItem("Chờ kiểm duyệt", ((int)Censorship.PENDING).ToString()));
            _censorships.Add(new ComboboxItem("Từ chối", ((int)Censorship.DENY).ToString()));
            _censorships.Add(new ComboboxItem("Kiểm duyệt", ((int)Censorship.ACCEPT).ToString()));

            if (_sessionService.Current.Authorities.Contains(Role.TRO_LY_TONG_HOP))
                _censorships.Add(new ComboboxItem("Tổng hợp", ((int)Censorship.AGGREGATE).ToString()));
        }

        /// <summary>
        /// Tạo data cho combobox đơn vị
        /// </summary>
        private void LoadAgencies()
        {
            try
            {
                int yearOfWork = _sessionService.Current.YearOfWork;
                _aggregateAgency = _donViService.FindByIdDonVi(_sessionService.Current.IdDonVi, yearOfWork);
                var predicate = PredicateBuilder.True<DonVi>();
                predicate = predicate.And(x => x.NamLamViec == yearOfWork).And(x => x.ITrangThai == 1);
                //if (_aggregateAgency != null)
                //    predicate = predicate.And(x => x.IdParent == _aggregateAgency.Id);
                List<DonVi> listDonVi = _donViService.FindByCondition(predicate).ToList();
                _agencies = new List<ComboboxItem>();
                _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void RefreshAfterSaveData(object sender, EventArgs e)
        {
            if (SelectedItem != null && sender != null)
            {
                TnDtdnChungTuModel item = Items.Where(x => x.Id == SelectedItem.Id).FirstOrDefault();
                item.FTongDuToanNamKeHoach = ((TnDtdnChungTuModel)sender).FTongDuToanNamKeHoach;
                item.FTongDuToanNamNay = ((TnDtdnChungTuModel)sender).FTongDuToanNamNay;
                item.FTongThucThuNamTruoc = ((TnDtdnChungTuModel)sender).FTongThucThuNamTruoc;
                item.FTongUocThucHienNamNay = ((TnDtdnChungTuModel)sender).FTongUocThucHienNamNay;
            }

            this.OnRefresh();
            OnSelectedItemChanged();
        }

        // <summary>
        /// Action when checkbox select all is selected
        /// </summary>
        /// <param name="select">true/false</param>
        /// <param name="models">items source of data grid</param>
        private static void SelectAll(bool select, IEnumerable<TnDtdnChungTuModel> models)
        {
            foreach (var model in models.Where(x => x.IsFilter))
            {
                model.Selected = select;
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnLockUnLock()
        {
            try
            {
                string message = string.Empty;
                if (_sessionService.Current.Authorities.Contains(Role.TRO_LY_PHONG_BAN) && IsLock)
                {
                    message = string.Format(Resources.UnlockNotValid, SelectedItem.SSoChungTu);
                    MessageBox.Show(message, Resources.NotifiTitle, MessageBoxButton.YesNo, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
                    MessageBoxResult result = MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                        LockConfirmEventHandler();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LockConfirmEventHandler()
        {
            var itemCheckeds = Items.Where(x => x.Selected);
            foreach (var item in itemCheckeds)
            {
                _tnDtdnChungTuService.LockOrUnLock(item.Id, !item.BKhoa);
                item.BKhoa = !item.BKhoa;

                if (_sessionService.Current.Authorities.Contains(Role.TRO_LY_TONG_HOP) && !item.BKhoa && item.SDSSoChungTuTongHop is null)
                {
                    var settlementVocher = Items.ToList().Where(x => x.Id == item.Id).First();
                    Items.Remove(settlementVocher);
                    SelectedItem = Items.Count > 0 ? Items.Where(x => x.Selected).First() : new TnDtdnChungTuModel();
                }
            }

            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsCensorship));
        }

        protected override void OnAdd()
        {
            PlanBudgetBeginYearDialogViewModel.Model = new TnDtdnChungTuModel();
            PlanBudgetBeginYearDialogViewModel.Init();
            PlanBudgetBeginYearDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDivisionDetail((TnDtdnChungTuModel)obj);
            };

            var view = new PlanBudgetBeginYearDialog
            {
                DataContext = PlanBudgetBeginYearDialogViewModel
            };
            DialogHost.Show(view, ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {
            PlanBudgetBeginYearDialogViewModel.Model = SelectedItem;
            PlanBudgetBeginYearDialogViewModel.IsAggregate = !(TabIndex == ImportTabIndex.Data);
            PlanBudgetBeginYearDialogViewModel.Init();
            PlanBudgetBeginYearDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDivisionDetail((TnDtdnChungTuModel)obj);
            };

            var view = new PlanBudgetBeginYearDialog
            {
                DataContext = PlanBudgetBeginYearDialogViewModel
            };
            DialogHost.Show(view, ROOT_DIALOG);
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenDivisionDetail((TnDtdnChungTuModel)obj);
        }

        private void OnOpenDivisionDetail(TnDtdnChungTuModel SelectedItem)
        {
            PlanBudgetBeginYearDetailViewModel.Model = SelectedItem;
            PlanBudgetBeginYearDetailViewModel.Init();
            var view = new PlanBudgetBeginYearDetail { DataContext = PlanBudgetBeginYearDetailViewModel };
            //view.Owner = System.Windows.Application.Current.MainWindow;
            view.ShowDialog();
        }

        private void OnPrint(object param)
        {
            int dialogType = (int)param;
            switch (dialogType)
            {
                case (int)PlanBudgetBeginYearType.PRINT_AGENCY:
                    PlanBudgetBeginYearReportViewModel.Init();
                    var view = new PlanBudgetBeginYearReport
                    {
                        DataContext = PlanBudgetBeginYearReportViewModel
                    };
                    DialogHost.Show(view, ROOT_DIALOG);
                    break;

                case (int)PlanBudgetBeginYearType.PRINT_SUMMARY:
                    PlanBudgetBeginYearReportSummaryViewModel.Init();
                    var view2 = new PlanBudgetBeginYearReportSummary
                    {
                        DataContext = PlanBudgetBeginYearReportSummaryViewModel
                    };
                    DialogHost.Show(view2, ROOT_DIALOG);
                    break;
            }
        }


        private void OnSendCensorship()
        {
            try
            {
                //kiểm tra trạng thái các bản ghi
                //if (Items.Any(x => x.IKiemDuyet != (int)Censorship.NEW && x.Selected))
                //{
                //    MessageBox.Show(Resources.AlertSendCensorship, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                //}
                //else
                //{
                //    MessageBoxResult result = MessageBox.Show(Resources.ConfirmSendCensorship, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                //    if (result == MessageBoxResult.Yes)
                //        UpdateCensorshipStatus((int)Censorship.PENDING);
                //}
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void UpdateCensorshipStatus(int status)
        {
            foreach (var item in Items.Where(x => x.Selected))
            {
                TnDtChungTu chungTu = _tnDtChungTuService.FindById(item.Id);
                chungTu.IKiemDuyet = status;
                chungTu.DateModified = DateTime.Now;
                chungTu.UserModifier = _sessionService.Current.Principal;
                _tnDtChungTuService.Update(chungTu);
            }
            LoadData();
        }

        private void OnAcceptCensorship()
        {
            try
            {
                //kiểm tra trạng thái các bản ghi
                //if (Items.Any(x => x.IKiemDuyet != (int)Censorship.PENDING && x.Selected))
                //    MessageBox.Show(Resources.AlertAcceptCensorship, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                //else
                //{
                //    MessageBoxResult result = MessageBox.Show(Resources.ConfirmAcceptCensorship, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                //    if (result == MessageBoxResult.Yes)
                //        UpdateCensorshipStatus((int)Censorship.ACCEPT);
                //}
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnDenyCensorship()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show(Resources.ConfirmDenyCensorship, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    UpdateCensorshipStatus((int)Censorship.DENY);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnAggregate()
        {
            try
            {
                //kiểm tra trạng thái các bản ghi
                string message = string.Empty;
                List<TnDtdnChungTuModel> selectedSettlementVouchers = Items.Where(x => x.Selected).ToList();

                if (selectedSettlementVouchers.Any(x => !x.BKhoa))
                    message = Resources.AlertAggregateUnLocked;

                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                //kiểm tra đã tồn tại chứng từ tổng hợp từ các chứng từ đã chọn chưa
                _aggregateVoucher = _tnDtChungTuService.FindAggregateVoucher(string.Join(",", selectedSettlementVouchers.OrderBy(x => x.SSoChungTu).Select(x => x.SSoChungTu).OrderBy(x => x).ToList()));
                if (_aggregateVoucher != null)
                {
                    MessageBoxResult result = MessageBox.Show(Resources.AlertExistAggregateVoucher, "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            CreateAggregateVoucher(selectedSettlementVouchers);
                            break;
                        case MessageBoxResult.No:
                            return;
                    }
                }
                else CreateAggregateVoucher(selectedSettlementVouchers);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateAggregateVoucher(List<TnDtdnChungTuModel> selectedSettlementVouchers)
        {
            if (_aggregateVoucher != null)
            {
                _tnDtdnChungTuService.Delete(_aggregateVoucher.Id);
            }

            PlanBudgetBeginYearSummaryViewModel.Model = new TnDtdnChungTuModel();
            //PlanBudgetBeginYearSummaryViewModel.AggregateAgency = _aggregateAgency;
            PlanBudgetBeginYearSummaryViewModel.DataPlan = new ObservableCollection<TnDtdnChungTuModel>(selectedSettlementVouchers);
            //PlanBudgetBeginYearSummaryViewModel.IsAggregate = true;
            PlanBudgetBeginYearSummaryViewModel.Init();

            PlanBudgetBeginYearSummaryViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDivisionDetail((TnDtdnChungTuModel)obj);
            };
            var view = new PlanBudgetBeginYearSummary { DataContext = PlanBudgetBeginYearSummaryViewModel };
            DialogHost.Show(view, SettlementScreen.ROOT_DIALOG);
        }

        /// <summary>
        /// Xuất excel chứng từ tổng hợp
        /// </summary>
        private void OnExportData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = RevenueExpenditureType.RPT_TN_DTDN_CHUNG_TU_TONG_HOP;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    List<TnDtdnChungTuModel> settlementVouchers = Items.Where(x => x.Selected).ToList();
                    DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();

                    foreach (var item in settlementVouchers)
                    {
                        List<TnDtdnChungTuChiTietModel> settlementVoucherDetails = GetVoucherDetail(item); //GetSettlementVoucherDetail(item);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                        data.Add("TitleFirst", $"LẬP DỰ TOÁN ĐẦU NĂM {_sessionService.Current.YearOfWork}");
                        data.Add("TitleSecond", $"(Kèm theo Chứng từ số: {item.SSoChungTu}, ngày: {DateUtils.Format(item.DNgayChungTu)})");
                        data.Add("HeaderTenDonVi", $"Đơn vị: {item.STenDonVi}");
                        data.Add("MoTa", item.SMoTa);
                        data.Add("NguoiTao", item.SNguoiTao);
                        data.Add("NgayTao", item.DNgaySua != null ? item.DNgayTao.Value.ToString("dd/MM/yyyy") : string.Empty);
                        data.Add("Items", settlementVoucherDetails);
                        data.Add("MLNS", mucLucNganSaches);
                        data.Add("TotalFThucThuNamTruoc", Model.FTongThucThuNamTruoc);
                        data.Add("TotalFDuToanNamNay", Model.FTongDuToanNamNay);
                        data.Add("TotalFUocThucHienNamNay", Model.FTongUocThucHienNamNay);
                        data.Add("TotalFDuToanNamKeHoach", Model.FTongDuToanNamKeHoach);
                        data.Add("Year", _sessionService.Current.YearOfWork);
                        data.Add("Year1", _sessionService.Current.YearOfWork - 1);
                        data.Add("Year2", _sessionService.Current.YearOfWork - 2);
                        data.Add("Count", 100000);

                        fileNamePrefix = $"{item.SSoChungTu}_{item.STenDonVi}";
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(GetChiTietToi(settlementVoucherDetails));
                        var xlsFile = _exportService.Export<TnDtdnChungTuChiTietModel, NsMucLucNganSach>(templateFileName, data, hideColumns.Select(x => x += 2).ToList());
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
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
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string GetChiTietToi(List<TnDtdnChungTuChiTietModel> items)
        {
            List<string> sChiTietTois = items.Where(x => !string.IsNullOrEmpty(x.ChiTietToi)).Select(x => x.ChiTietToi).Distinct().ToList();
            if (sChiTietTois.IsEmpty()) return "NG";
            return DynamicMLNS.GetMaxNameColumnByChiTietToi(sChiTietTois);
        }

        private List<TnDtdnChungTuChiTietModel> GetVoucherDetail(TnDtdnChungTuModel settlementVoucher)
        {
            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = settlementVoucher.Id,
                LNS = settlementVoucher.SDSLNS,
                YearOfWork = settlementVoucher.INamLamViec,
                YearOfBudget = settlementVoucher.INamNganSach,
                VoucherDate = settlementVoucher.DNgayChungTu,
                BudgetSource = settlementVoucher.IIdMaNguonNganSach
            };
            List<TnDtdnChungTuChiTietQuery> _listChungTuChiTiet = _tnDtdnChungTuChiTietService.FindByDataDetailCondition(searchCondition).ToList();
            var itemsDetail = _mapper.Map<List<TnDtdnChungTuChiTietModel>>(_listChungTuChiTiet);
            CalculateData(itemsDetail);
            itemsDetail = itemsDetail.Where(x => x.IsHasData).ToList();
            return itemsDetail;
        }

        private void CalculateData(List<TnDtdnChungTuChiTietModel> items)
        {
            items.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.FDuToanNamKeHoach = 0;
                    x.FDuToanNamNay = 0;
                    x.FThucThuNamTruoc = 0;
                    x.FUocThucHienNamNay = 0;
                    return x;
                }).ToList();

            foreach (var item in items.Where(x => x.IsEditable && (x.FDuToanNamKeHoach != 0 || x.FDuToanNamNay != 0 || x.FThucThuNamTruoc != 0 || x.FUocThucHienNamNay != 0)))
            {
                CalculateParent(item, item, items);
            }

            CalculateTotal(items);
        }

        private void CalculateParent(TnDtdnChungTuChiTietModel currentItem, TnDtdnChungTuChiTietModel seftItem, List<TnDtdnChungTuChiTietModel> items)
        {
            var parrentItem = items.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FDuToanNamKeHoach += seftItem.FDuToanNamKeHoach;
            parrentItem.FDuToanNamNay += seftItem.FDuToanNamNay;
            parrentItem.FThucThuNamTruoc += seftItem.FThucThuNamTruoc;
            parrentItem.FUocThucHienNamNay += seftItem.FUocThucHienNamNay;
            CalculateParent(parrentItem, seftItem, items);
        }

        private void CalculateTotal(List<TnDtdnChungTuChiTietModel> items)

        {
            Model ??= new TnDtdnChungTuModel();
            Model.FTongDuToanNamKeHoach = 0;
            Model.FTongDuToanNamNay = 0;
            Model.FTongThucThuNamTruoc = 0;
            Model.FTongUocThucHienNamNay = 0;
            var listChildren = items.Where(x => x.IsEditable && x.IsFilter).ToList();
            foreach (var item in listChildren)
            {
                Model.FTongDuToanNamKeHoach += item.FDuToanNamKeHoach;
                Model.FTongDuToanNamNay += item.FDuToanNamNay;
                Model.FTongThucThuNamTruoc += item.FThucThuNamTruoc;
                Model.FTongUocThucHienNamNay += item.FUocThucHienNamNay;
            }
            OnPropertyChanged(nameof(Model));
        }

        private void OnSelectedChange(object obj)
        {
            SelectedItem = (TnDtdnChungTuModel)obj;
            if (SelectedItem is { BKhoa: true } || SelectedItem == null)
            {
                //IsEdit = false;
            }
            else
            {
                //IsEdit = true;
            }
        }
        private void OnImportData(object param)
        {
            var view = new PlanBudgetBeginYearImport { DataContext = PlanBudgetBeginYearImportViewModel };
            PlanBudgetBeginYearImportViewModel.RevenueExpenditureImportTypes = (RevenueExpenditureImportType)((int)param);
            PlanBudgetBeginYearImportViewModel.SavedAction = obj =>
            {
                view.Close();
                this.OnRefresh();
                OnOpenDivisionDetail((TnDtdnChungTuModel)obj);
            }; 

            PlanBudgetBeginYearImportViewModel.Init();
            var result = view.ShowDialog();
        }

        protected override void OnDelete()
        {
            try
            {
                base.OnDelete();
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, SelectedItem.DNgayChungTu);
                var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
                DialogHost.Show(messageBox.Content, "RootDialog");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            _tnDtdnChungTuService.Delete(SelectedItem.Id);
            this.OnRefresh();
        }
    }
}
