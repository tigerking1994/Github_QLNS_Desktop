using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.CollectionsBudget.RealRevenueExpenditure;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Import;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.RealRevenueExpenditure;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.RealRevenueExpenditure.PrintRealRevenueExpenditureReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.RealRevenueExpenditure;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.RealRevenueExpenditure.PrintRealRevenueExpenditureReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.RegularBudget;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.RevenueExpenditureType;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.CollectionsBudget.RealRevenueExpenditure
{
    public class RealRevenueExpenditureIndexViewModel : GridViewModelBase<TnQtChungTuHD4554Model>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITnQtChungTuHD4554Service _tnQtChungTuService;
        private readonly IExportService _exportService;
        private readonly ITTnDanhMucLoaiHinhService _tnDanhMucLoaiHinhService;
        private readonly ITnQtChungTuChiTietHD4554Service _tnQtChungTuChiTietService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly INsDonViService _sDonViService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private INsDonViService _donViService;
        private ICollectionView _tnQtChungTuView;
        private ICollectionView _tnQtChungTuViewummaries;
        private TnQtChungTuHD4554 _aggregateVoucher;
        private DonVi _aggregateAgency;
        private RevenueExpenditureImport1 _revenueExpenditureImport;

        public override string Name => "Số liệu thực thu";
        public override string Description => "Số liệu thực thu";
        public override Type ContentType => typeof(RealRevenueExpenditureIndex);
        public override PackIconKind IconKind => PackIconKind.Input;

        public bool IsEdit => SelectedItem != null && SelectedItem.BKhoa != null && !SelectedItem.BKhoa.Value;
        //public bool IsLock => SelectedItem != null && SelectedItem.BKhoa != null && SelectedItem.BKhoa.Value;
        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }
        public bool IsEnableLock => SelectedItem != null;

        public bool IsExportAggregateData => Items.Where(x => x.IsSelected).ToList().Count > 0 ? true : false;

        private VoucherTabIndex _tabIndex;
        public VoucherTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadData();
                OnSelectedItemChanged();
                OnPropertyChanged(nameof(IsExportAggregateData));
            }
        }

        public bool IsCensorship
        {
            get
            {
                var itemSelected = Items.Where(x => x.IsSelected);
                return itemSelected.Any() && itemSelected.All(x => x.BKhoa.Value);
            }
        }

        #region Month
        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set
            {
                SetProperty(ref _monthSelected, value);
                if (_tnQtChungTuView != null)
                    _tnQtChungTuView.Refresh();
            }
        }
        #endregion

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
                if (_tnQtChungTuView != null)
                    _tnQtChungTuView.Refresh();
            }
        }
        #endregion
        //public bool IsCensorship => Items.Where(x => x.IsSelected).ToList().Count > 0 ? true : false;
        public bool IsAggregate
        {
            get
            {
                IEnumerable<TnQtChungTuHD4554Model> listSelected = Items.Where(x => x.IsSelected);
                if (listSelected.Count() > 0)
                    if (listSelected.Any(x => !string.IsNullOrEmpty(x.STongHop)))
                        return false;
                    else return true;
                return false;
            }
        }

        private ObservableCollection<TnQtChungTuHD4554Model> _settlementVouchers;
        public ObservableCollection<TnQtChungTuHD4554Model> SettlementVouchers
        {
            get => _settlementVouchers;
            set => SetProperty(ref _settlementVouchers, value);
        }

        private ObservableCollection<TnQtChungTuHD4554Model> _settlementVoucherSummaries;
        public ObservableCollection<TnQtChungTuHD4554Model> SettlementVoucherSummaries
        {
            get => _settlementVoucherSummaries;
            set => SetProperty(ref _settlementVoucherSummaries, value);
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
                    var selected = Items.Select(item => item.IsSelected).Distinct().ToList();
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

        /// <summary>
        /// Checkbox select all property
        /// </summary>
        public bool? IsAllItemSummariesSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(item => item.IsSelected).Distinct().ToList();
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
                        Items.Where(x => x.BDaTongHop is null || (x.BDaTongHop.HasValue && !x.BDaTongHop.Value)).ForAll(c => c.IsSelected = value.Value);
                    }
                }
            }
        }

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
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
                OnPropertyChanged(nameof(IsButtonEnable));
                //OnRefresh();
                //OnPropertyChanged(nameof(IsButtonEnable));
                if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
                {
                    IsLock = true;
                }
                else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                {
                    IsLock = false;
                }
                _tnQtChungTuView.Refresh();
            }
        }

        //public bool IsButtonEnable => LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0");
        public bool IsButtonEnable
        {
            get
            {
                bool result = false;
                List<TnQtChungTuHD4554Model> lstSelected = Items.Where(x => x.IsSelected).ToList();
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Any())
                {
                    result = true;
                }
                else
                {
                    result = lstSelected.Any() && (lstSelected.All(x => x.BKhoa.HasValue && x.BKhoa.Value) || lstSelected.All(x => x.BKhoa.HasValue && !x.BKhoa.Value));
                    IsLock = lstSelected.Any(x => x.BKhoa.HasValue && x.BKhoa.Value);
                }
                return result;
            }
        }

        public RealRevenueExpenditureDialogViewModel RealRevenueExpenditureDialogViewModel { get; set; }
        public RealRevenueExpenditureDetailViewModel RealRevenueExpenditureDetailViewModel { get; set; }
        public PrintRealRevenueExpenditureReportViewModel PrintRealRevenueExpenditureReportViewModel { get; set; }
        public RevenueExpenditureImportViewModelClone RevenueExpenditureImportViewModel { get; set; }

        public RelayCommand PrintCommand { get; }
        public RelayCommand SendCensorshipCommand { get; }
        public RelayCommand AcceptCensorship { get; }
        public RelayCommand DenyCensorship { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand ExportAggregateDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand SearchCommand { get; set; }

        public RealRevenueExpenditureIndexViewModel(IMapper mapper,
            ISessionService sessionService,
            ITnQtChungTuHD4554Service tnQtChungTuService,
            INsDonViService nsDonViService,
            IExportService exportService,
            ITTnDanhMucLoaiHinhService tnDanhMucLoaiHinhService,
            ITnQtChungTuChiTietHD4554Service tnQtChungTuChiTietService,
            ILog logger,
            RealRevenueExpenditureDialogViewModel realRevenueExpenditureDialogViewModel,
            RealRevenueExpenditureDetailViewModel realRevenueExpenditureDetailViewModel,
            PrintRealRevenueExpenditureReportViewModel printRealRevenueExpenditureReportViewModel,
            RevenueExpenditureImportViewModelClone revenueExpenditureImportViewModel,
            IDanhMucService danhMucService,
            INsMucLucNganSachService mucLucNganSachService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _tnQtChungTuService = tnQtChungTuService;
            _donViService = nsDonViService;
            _exportService = exportService;
            _tnDanhMucLoaiHinhService = tnDanhMucLoaiHinhService;
            _tnQtChungTuChiTietService = tnQtChungTuChiTietService;
            _logger = logger;

            RevenueExpenditureImportViewModel = revenueExpenditureImportViewModel;
            RealRevenueExpenditureDialogViewModel = realRevenueExpenditureDialogViewModel;
            RealRevenueExpenditureDetailViewModel = realRevenueExpenditureDetailViewModel;
            PrintRealRevenueExpenditureReportViewModel = printRealRevenueExpenditureReportViewModel;

            PrintCommand = new RelayCommand(obj => OnShowPrint(obj));
            //SendCensorshipCommand = new RelayCommand(obj => OnSendCensorship());
            AcceptCensorship = new RelayCommand(obj => OnAcceptCensorship());
            //DenyCensorship = new RelayCommand(obj => OnDenyCensorship());
            AggregateCommand = new RelayCommand(obj => OnAggregate());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData());
            ImportDataCommand = new RelayCommand(obj => OnImportData(obj));
            SearchCommand = new RelayCommand(obj => _tnQtChungTuView.Refresh());
            _danhMucService = danhMucService;
            _mucLucNganSachService = mucLucNganSachService;
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            ResetCondition();
            LoadData();
            LoadLockStatus();
            RealRevenueExpenditureDetailViewModel.UpdateSettlementVoucherEvent += RefreshAfterSaveData;
        }

        private void LoadLockStatus()
        {
            List<ComboboxItem> lockStatus = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Toàn bộ", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đã khóa", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Chưa khóa", ValueItem = "2"},
            };

            LockStatus = new ObservableCollection<ComboboxItem>(lockStatus);
            LockStatusSelected = LockStatus.ElementAt(0);
        }

        private void ResetCondition()
        {
            _monthSelected = null;
            _censorshipSelected = null;
            _tabIndex = VoucherTabIndex.VOUCHER;
            OnPropertyChanged(nameof(MonthSelected));
            OnPropertyChanged(nameof(CensorshipSelected));
            OnPropertyChanged(nameof(TabIndex));
        }

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        public override void LoadData(params object[] args)
        {
            try
            {
                //_months = FnCommonUtils.LoadMonths();
                _months = LoadMonths();
                LoadCensorships();
                LoadAgencies();
                Items = new ObservableCollection<TnQtChungTuHD4554Model>();
                var predicate = CreatePredicate();
                List<TnQtChungTuHD4554> _listChungTu = _tnQtChungTuService.FindByCondition(predicate).ToList();
                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == VoucherTabIndex.VOUCHER)
                    {
                        Items = _mapper.Map<ObservableCollection<TnQtChungTuHD4554Model>>(_listChungTu.Where(x => !IsDonViRoot(x.IIdMaDonVi) && !x.BDaTongHop.Value));
                    }
                    else
                    {
                        List<TnQtChungTuHD4554Model> listChungTuTongHop = new List<TnQtChungTuHD4554Model>();
                        foreach (TnQtChungTuHD4554 chungTu in _listChungTu.Where(x => IsDonViRoot(x.IIdMaDonVi)))
                        {
                            TnQtChungTuHD4554Model parent = _mapper.Map<TnQtChungTuHD4554Model>(chungTu);
                            parent.IsExpand = true;
                            listChungTuTongHop.Add(parent);
                            if (!string.IsNullOrEmpty(chungTu.STongHop))
                            {
                                List<TnQtChungTuHD4554Model> listChild = _mapper.Map<List<TnQtChungTuHD4554Model>>(_listChungTu.Where(x => chungTu.STongHop.Split(",").Contains(x.SSoChungTu) && x.BDaTongHop.Value).ToList());
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = chungTu.SSoChungTu; });
                                listChungTuTongHop.AddRange(listChild);
                            }
                        }
                        Items = new ObservableCollection<TnQtChungTuHD4554Model>(listChungTuTongHop);

                    }
                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<TnQtChungTuHD4554Model>>(_listChungTu.Where(x => !x.BDaTongHop.Value));
                }

                //Items = _mapper.Map<ObservableCollection<TnQtChungTuHD4554Model>>(_listChungTu);

                if (Items != null && Items.Count > 0)
                {
                    SelectedItem = Items.FirstOrDefault();
                }

                _tnQtChungTuView = CollectionViewSource.GetDefaultView(Items);
                _tnQtChungTuView.Filter = ListSettlementVoucherFilter;
                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(TnQtChungTuHD4554Model.IsSelected))
                        {
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            //OnPropertyChanged(nameof(IsLock));
                        }
                        if (args.PropertyName == nameof(TnQtChungTuHD4554Model.IsCollapse))
                        {
                            ExpandChild();
                        }
                    };
                }

                OnPropertyChanged(nameof(IsEdit));
                //OnPropertyChanged(nameof(IsLock));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private List<ComboboxItem> LoadMonths()
        {
            int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int[] quaters = { 3, 6, 9, 12 };
            List<ComboboxItem> cbxMonthQuater = new List<ComboboxItem>();
            foreach (var item in months)
            {
                ComboboxItem month = new ComboboxItem("Tháng " + item, item.ToString());
                cbxMonthQuater.Add(month);
            }


            foreach (var item in quaters)
            {
                if (item.Equals(3))
                {
                    ComboboxItem quater = new ComboboxItem("Quý I ", item.ToString());
                    cbxMonthQuater.Add(quater);
                }
                else if (item.Equals(6))
                {
                    ComboboxItem quater = new ComboboxItem("Quý II ", item.ToString());
                    cbxMonthQuater.Add(quater);
                }
                else if (item.Equals(9))
                {
                    ComboboxItem quater = new ComboboxItem("Quý III ", item.ToString());
                    cbxMonthQuater.Add(quater);
                }
                else if (item.Equals(12))
                {
                    ComboboxItem quater = new ComboboxItem("Quý IV ", item.ToString());
                    cbxMonthQuater.Add(quater);
                }
            }

            return cbxMonthQuater;
        }

        private void ExpandChild()
        {
            Items?.Where(n => n.SoChungTuParent == SelectedItem.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
        }

        private bool ListSettlementVoucherFilter(object obj)
        {
            bool result = true;
            var item = (TnQtChungTuHD4554Model)obj;
            if (MonthSelected != null)
                result = result && item.IThangQuy.Value.ToString() == MonthSelected.ValueItem && item.SThangQuyMoTa.Equals(MonthSelected.DisplayItem);


            if (LockStatusSelected != null)
            {
                if (LockStatusSelected.ValueItem.Equals("1"))
                {
                    result = result && item.BKhoa == true;
                }
                if (LockStatusSelected.ValueItem.Equals("2"))
                {
                    result = result && item.BKhoa == false;
                }
            }
            //if (!string.IsNullOrEmpty(SearchText))
            //    result = result && item.SSoChungTu.ToLower().Contains(SearchText.ToLower());

            //if (_sessionService.Current.Authorities.Contains(Role.TRO_LY_TONG_HOP))
            //    result = result && item.BKhoa == true;

            item.IsFilter = result;
            return result;
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
                predicate = predicate.And(x => x.NamLamViec == yearOfWork).And(x => x.Loai == "1");
                if (_aggregateAgency != null)
                    predicate = predicate.And(x => x.IdParent == _aggregateAgency.Id);
                List<DonVi> listDonVi = _donViService.FindByCondition(predicate).ToList();
                _agencies = new List<ComboboxItem>();
                _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        // <summary>
        /// Action when checkbox select all is selected
        /// </summary>
        /// <param name="select">true/false</param>
        /// <param name="models">items source of data grid</param>
        private static void SelectAll(bool select, IEnumerable<TnQtChungTuHD4554Model> models)
        {
            foreach (var model in models.Where(x => x.IsFilter))
            {
                model.IsSelected = select;
            }
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

        public Expression<Func<TnQtChungTuHD4554, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<TnQtChungTuHD4554>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.INguonNganSach == _sessionService.Current.Budget);

            return predicate;
        }

        private void RefreshAfterSaveData(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            {
                TnQtChungTuHD4554Model item = Items.Where(x => x.Id == SelectedItem.Id).First();
                item.FTongSoTien = ((TnQtChungTuHD4554Model)sender).FTongSoTien;
                //item.TongSoChiPhiSum = ((TnQtChungTuHD4554Model)sender).TongSoChiPhiSum;
            }

            this.OnRefresh();
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
            OnPropertyChanged(nameof(Model));
        }

        protected override void OnSelectedItemChanged()
        {
            //OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsEnableLock));
            OnPropertyChanged(nameof(IsButtonEnable));
            OnPropertyChanged(nameof(Items));
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
            var lstSelected = Items.Where(x => x.IsSelected).ToList();
            bool isLock = !lstSelected.FirstOrDefault().BKhoa.Value;
            foreach (var item in lstSelected)
            {
                var khoa = isLock;
                _tnQtChungTuService.LockOrUnLock(item.Id, khoa);
                SelectedItem.BKhoa = !SelectedItem.BKhoa;

                if (_sessionService.Current.Authorities.Contains(Role.TRO_LY_TONG_HOP) && khoa)
                {
                    var settlementVocher = Items.ToList().Where(x => x.Id == SelectedItem.Id).First();
                    Items.Remove(settlementVocher);
                    SelectedItem = Items.Count > 0 ? Items.First() : new TnQtChungTuHD4554Model();
                }
            }

            LoadData();
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(Items));
        }

        private void OnShowPrint(object param)
        {
            if (param != null)
                PrintRealRevenueExpenditureReportViewModel.RealRevenueExpenditureTypes = (RealRevenueExpenditureType)((int)param);
            PrintRealRevenueExpenditureReportViewModel.Init();
            var view = new PrintRealRevenueExpenditureReport
            {
                DataContext = PrintRealRevenueExpenditureReportViewModel
            };

            DialogHost.Show(view, ROOT_DIALOG);
        }

        //private void OnSendCensorship()
        //{
        //    try
        //    {
        //        //kiểm tra trạng thái các bản ghi
        //        if (Items.Any(x => x.IsSelected))
        //        {
        //            MessageBox.Show(Resources.AlertSendCensorship, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }
        //        else
        //        {
        //            MessageBoxResult result = MessageBox.Show(Resources.ConfirmSendCensorship, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //            if (result == MessageBoxResult.Yes)
        //                UpdateCensorshipStatus((int)Censorship.PENDING);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message, ex);
        //    }
        //}

        private void OnAcceptCensorship()
        {
            try
            {
                //kiểm tra trạng thái các bản ghi
                if (Items.Any(x => x.IsSelected))
                    MessageBox.Show(Resources.AlertAcceptCensorship, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    MessageBoxResult result = MessageBox.Show(Resources.ConfirmAcceptCensorship, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                        UpdateCensorshipStatus((int)Censorship.ACCEPT);
                }
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
                List<TnQtChungTuHD4554Model> selectedSettlementVouchers = Items.Where(x => x.IsSelected).ToList();

                if (selectedSettlementVouchers.Any(x => !x.BKhoa.Value))
                    message = Resources.AlertAggregateUnLocked;
                else if (selectedSettlementVouchers.GroupBy(x => x.IThangQuy).ToList().Count() > 1)
                    message = Resources.AlertAgreeQuarterMonth;
                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                //kiểm tra đã tồn tại chứng từ tổng hợp từ các chứng từ đã chọn chưa
                _aggregateVoucher = _tnQtChungTuService.FindAggregateVoucher(string.Join(",", selectedSettlementVouchers.OrderBy(x => x.SSoChungTu).Select(x => x.SSoChungTu).OrderBy(x => x).ToList()));
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
                else
                {
                    CreateAggregateVoucher(selectedSettlementVouchers);

                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd()
        {
            RealRevenueExpenditureDialogViewModel.Model = new TnQtChungTuHD4554Model();
            RealRevenueExpenditureDialogViewModel.IsAdjustEnabled = true;
            RealRevenueExpenditureDialogViewModel.IsAggregate = false;
            RealRevenueExpenditureDialogViewModel.Init();
            RealRevenueExpenditureDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDivisionDetail((TnQtChungTuHD4554Model)obj);
            };

            var view = new RealRevenueExpenditureDialog
            {
                DataContext = RealRevenueExpenditureDialogViewModel
            };

            DialogHost.Show(view, ROOT_DIALOG);
        }

        private void CreateAggregateVoucher(List<TnQtChungTuHD4554Model> selectedSettlementVouchers)
        {
            if (_aggregateVoucher != null)
            {
                _tnQtChungTuService.Delete(_aggregateVoucher.Id);
            }

            RealRevenueExpenditureDialogViewModel.Model = new TnQtChungTuHD4554Model();
            RealRevenueExpenditureDialogViewModel.AggregateAgency = _aggregateAgency;
            RealRevenueExpenditureDialogViewModel.AggregateSettlementVouchers = selectedSettlementVouchers;
            RealRevenueExpenditureDialogViewModel.IsAggregate = true;
            RealRevenueExpenditureDialogViewModel.AggregateLNS = string.Join(StringUtils.COMMA, selectedSettlementVouchers.Select(x => x.SDSLNS).Distinct());
            RealRevenueExpenditureDialogViewModel.Init();
            RealRevenueExpenditureDialogViewModel.SavedAction = obj =>
            {

                OnOpenDivisionDetail((TnQtChungTuHD4554Model)obj);
                _tabIndex = VoucherTabIndex.VOUCHER_AGREGATE;
                OnPropertyChanged(nameof(TabIndex));
                this.OnRefresh();
            };
            var view = new RealRevenueExpenditureDialog { DataContext = RealRevenueExpenditureDialogViewModel };
            DialogHost.Show(view, SettlementScreen.ROOT_DIALOG);
        }

        private void UpdateCensorshipStatus(int status)
        {
            foreach (var item in Items.Where(x => x.IsSelected))
            {
                TnQtChungTuHD4554 chungTu = _tnQtChungTuService.FindById(item.Id);
                //chungTu.IKiemDuyet = status;
                chungTu.DNgayTao = DateTime.Now;
                chungTu.SNguoiTao = _sessionService.Current.Principal;
                _tnQtChungTuService.Update(chungTu);
            }
            LoadData();
        }

        /// <summary>
        /// Xuất excel chứng từ tổng hợp
        /// </summary>
        private void OnExportAggregateData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string fileName = RevenueExpenditureType.RPT_QT_CHUNG_TU_SOLIEU_THUCTHU;
                    string templateFileName = string.Empty;
                    DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).Where(x => x.ITrangThai == NSEntityStatus.ACTIVED).ToList();
                    List<TnQtChungTuHD4554Model> settlementVouchers = Items.Where(x => x.IsSelected).ToList();
                    foreach (var item in settlementVouchers)
                    {
                        var donVi = _donViService.FindByIdDonVi(item.IIdMaDonVi, _sessionService.Current.YearOfWork);
                        EstimationVoucherDetailCriteria _searchCondition = new EstimationVoucherDetailCriteria
                        {
                            VoucherId = item.Id,
                            LNS = item.SDSLNS,
                            YearOfWork = item.INamLamViec,
                            YearOfBudget = item.INamNganSach,
                            VoucherDate = item.DNgayChungTu,
                            BudgetSource = item.INguonNganSach,
                            ILoai = item.IThangQuyLoai.Value,
                            IThangQuy = item.IThangQuy != null ? item.IThangQuy.Value : 0
                        };
                        var _chungTuChiTiet = _tnQtChungTuChiTietService.FindByRealRevenueExpenditureCondition(_searchCondition).ToList();
                        var settlementVoucherDetails = _mapper.Map<ObservableCollection<TnQtChungTuChiTietHD4554Model>>(_chungTuChiTiet);
                        CalculateData(settlementVoucherDetails.ToList());
                        //List<TnDanhMucLoaiHinh> mucLucNganSaches = _tnDanhMucLoaiHinhService.FindByLoaiHinh(
                        //    _sessionService.Current.YearOfWork,
                        //    NSEntityStatus.ACTIVED).ToList();

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                        data.Add("TitleFirst", $"SỐ LIỆU THỰC THU {_sessionService.Current.YearOfWork}");
                        data.Add("TitleSecond", $"(Kèm theo Chứng từ số: {item.SSoChungTu}, ngày: {DateUtils.Format(item.DNgayChungTu)})");
                        data.Add("HeaderTenDonVi", $"Đơn vị: {donVi.TenDonVi}");
                        data.Add("Count", 10000);
                        data.Add("Items", settlementVoucherDetails.Where(x => x.HasData).ToList());
                        data.Add("MLNS", mucLucNganSaches);
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_THUNOP_NGANSACH, fileName);
                        List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(GetChiTietToi(settlementVoucherDetails.ToList()));
                        var xlsFile = _exportService.Export<TnQtChungTuChiTietHD4554Model, NsMucLucNganSach>(templateFileName, data, hideColumns.Select(x => x += 2).ToList());
                        string fileNamePrefix = item.SSoChungTu;
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
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

        private string GetChiTietToi(List<TnQtChungTuChiTietHD4554Model> items)
        {
            List<string> sChiTietTois = items.Where(x => !string.IsNullOrEmpty(x.ChiTietToi)).Select(x => x.ChiTietToi).Distinct().ToList();
            if (sChiTietTois.IsEmpty()) return "NG";
            return DynamicMLNS.GetMaxNameColumnByChiTietToi(sChiTietTois);
        }

        private void CalculateData(List<TnQtChungTuChiTietHD4554Model> lstChungTuChiTiets)
        {
            lstChungTuChiTiets.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.FSoTien = 0;
                    x.FSoTienDeNghi = 0;
                    return x;
                }).ToList();

            var temp = lstChungTuChiTiets.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = lstChungTuChiTiets.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                //CalculateParent(item, item);
                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }
        }

        private void CalculateParent(Guid idParent, TnQtChungTuChiTietHD4554Model item, Dictionary<Guid?, TnQtChungTuChiTietHD4554Model> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FSoTien += item.FSoTien.GetValueOrDefault(0);
            model.FSoTienDeNghi += item.FSoTienDeNghi.GetValueOrDefault(0);

            CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
        }

        private List<TnQtChungTuChiTietHD4554Model> GetSettlementVoucherDetail(TnQtChungTuHD4554Model settlementVoucher)
        {
            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = settlementVoucher.Id,
                LNS = settlementVoucher.SDSLNS,
                YearOfWork = settlementVoucher.INamLamViec,
                YearOfBudget = settlementVoucher.INamNganSach,
                VoucherDate = settlementVoucher.DNgayChungTu,
                BudgetSource = settlementVoucher.INguonNganSach,
                ILoai = settlementVoucher.IThangQuyLoai.Value,
                IThangQuy = settlementVoucher.IThangQuy != null ? settlementVoucher.IThangQuy.Value : 0
            };

            List<TnQtChungTuChiTietHD4554Query> _listChungTuChiTiet = _tnQtChungTuChiTietService.FindByRealRevenueExpenditureCondition(searchCondition).ToList();
            return _mapper.Map<List<TnQtChungTuChiTietHD4554Model>>(_listChungTuChiTiet);
        }

        private void OnImportData(object param)
        {
            RevenueExpenditureImportViewModel.RevenueExpenditureImportTypes = (RevenueExpenditureImportType)((int)param);
            RevenueExpenditureImportViewModel.Init();
            RevenueExpenditureImportViewModel.SavedRealBudgetAction = obj =>
            {
                _revenueExpenditureImport.Close();
                this.OnRefresh();
                OnOpenDivisionDetail(obj);
            };

            _revenueExpenditureImport = new RevenueExpenditureImport1 { DataContext = RevenueExpenditureImportViewModel };
            _revenueExpenditureImport.ShowDialog();
        }

        protected override void OnDelete()
        {
            try
            {
                base.OnDelete();
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, SelectedItem.DNgayChungTu.HasValue ? DateTimeExtension.ToStringDate(SelectedItem.DNgayChungTu.Value) : string.Empty);
                MessageBoxResult result = MessageBoxHelper.Confirm(messageBuilder.ToString());
                if (result == MessageBoxResult.Yes)
                    DeleteEventHandler(SelectedItem);
                //var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
                //DialogHost.Show(messageBox.Content, "RootDialog");
                //MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }



        private void DeleteEventHandler(TnQtChungTuHD4554Model model)
        {
            _tnQtChungTuService.Delete(model.Id);
            if (!string.IsNullOrEmpty(model.STongHop))
            {
                string[] lstSoCtChild = model.STongHop.Split(",");
                foreach (string soct in lstSoCtChild)
                {
                    TnQtChungTuHD4554 ctChild = _tnQtChungTuService.FindByCondition(x => x.SSoChungTu.Equals(soct) && x.INamLamViec == model.INamLamViec).FirstOrDefault();
                    if (ctChild != null)
                    {
                        ctChild.BDaTongHop = false;
                        _tnQtChungTuService.Update(ctChild);
                    }
                }
            }

            var lstDetail = _tnQtChungTuChiTietService.FindByIdChiTiet(model.Id);
            if (lstDetail.Any())
            {
                foreach (var item in lstDetail)
                {
                    _tnQtChungTuChiTietService.Delete(item.Id);
                }
            }

            var itemDeleted = Items.Where(x => x.Id == model.Id).First();
            Items.Remove(itemDeleted);
            LoadData();
            MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            //TnQtChungTuHD4554Model model =tabInde

            RealRevenueExpenditureDialogViewModel.Model = SelectedItem;
            RealRevenueExpenditureDialogViewModel.IsAggregate = false;
            RealRevenueExpenditureDialogViewModel.IsAdjustEnabled = false;
            RealRevenueExpenditureDialogViewModel.Init();
            RealRevenueExpenditureDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDivisionDetail((TnQtChungTuHD4554Model)obj);
            };

            var view = new RealRevenueExpenditureDialog
            {
                DataContext = RealRevenueExpenditureDialogViewModel
            };

            DialogHost.Show(view, ROOT_DIALOG);
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenDivisionDetail((TnQtChungTuHD4554Model)obj);
        }

        private void OnOpenDivisionDetail(TnQtChungTuHD4554Model SelectedItem)
        {
            RealRevenueExpenditureDetailViewModel.Model = SelectedItem;
            RealRevenueExpenditureDetailViewModel.Init();
            var view = new RealRevenueExpenditureDetail { DataContext = RealRevenueExpenditureDetailViewModel };
            view.ShowDialog();
        }

    }
}
