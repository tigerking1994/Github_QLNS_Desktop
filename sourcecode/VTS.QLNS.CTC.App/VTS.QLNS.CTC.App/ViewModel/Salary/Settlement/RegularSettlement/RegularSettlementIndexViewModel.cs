using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Salary.Settlement.RegularSettlement;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using MessageBox = System.Windows.MessageBox;
using static VTS.QLNS.CTC.Utility.DateTimeExtension;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.RegularSettlement
{
    public class RegularSettlementIndexViewModel : GridViewModelBase<TlQtChungTuModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITlQtChungTuService _tlQtChungTuService;
        private readonly ITlQtChungTuChiTietService _tlQtChungTuChiTietService;
        private readonly ITlQtChungTuChiTietGiaiThichService _tlQtChungTuChiTietGiaiThichService;
        private readonly ITlDmCanBoService _tlDmCanBoService;
        private readonly IExportService _exportService;
        private readonly INsDtChungTuService _estimationService;
        private ICollectionView _chungTuView;
        private SessionInfo _sessionInfo;

        public override string FuncCode => NSFunctionCode.SALARY_SETTLEMENT_REGULAR_SETTLEMENT_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Quyết toán thường xuyên";
        public override string Title => "Quyết toán kinh phí thường xuyên";
        public override string Description => "Quyết toán kinh phí thường xuyên";
        public override PackIconKind IconKind => PackIconKind.CurrencyUsd;
        public override Type ContentType => typeof(View.Salary.Settlement.RegularSettlement.RegularSettlement);

        private List<TlQtChungTuChiTietModel> _lstChungTuChiTiet;

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
                if (SetProperty(ref _monthSelected, value) && _chungTuView != null)
                {
                    OnSearch();
                }
            }
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _selectedYear;
        public ComboboxItem SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (SetProperty(ref _selectedYear, value) && _chungTuView != null)
                {
                    OnSearch();
                }
            }
        }

        private ObservableCollection<TlDmDonViModel> _donViItems;
        public ObservableCollection<TlDmDonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViModel _selectedDonViItems;
        public TlDmDonViModel SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                if (SetProperty(ref _selectedDonViItems, value) && _chungTuView != null)
                {
                    OnSearch();
                }
            }
        }

        private string _searchChungTu;
        public string SearchChungTu
        {
            get => _searchChungTu;
            set => SetProperty(ref _searchChungTu, value);
        }

        public bool IsButtonEnableLock
        {
            get
            {
                var lstCtSelected = Items.Where(x => x.Selected);
                if (lstCtSelected.Count() > 0)
                {
                    var checkTt = lstCtSelected.Select(x => x.BKhoa).Distinct();
                    if (checkTt.Count() == 1)
                    {
                        IsLock = checkTt.FirstOrDefault();
                        return true;
                    }

                    return false;
                }
                return SelectedItem != null;
            }
        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        /// <summary>
        /// Checkbox select all property
        /// </summary>
        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null && MonthSelected != null && SelectedYear != null && SelectedDonViItems != null)
                {
                    var selected = Items.Where(item => (MonthSelected.ValueItem == null || item.Thang == int.Parse(MonthSelected.ValueItem))
                                               && item.Nam == int.Parse(SelectedYear.ValueItem)
                                               && (string.IsNullOrEmpty(SelectedDonViItems.MaDonVi) || item.MaDonVi.Equals(SelectedDonViItems.MaDonVi))).Select(item => item.Selected).Distinct().ToList();
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


        //chung tu tong hop
        public bool? IsAllItemsSelected1
        {
            get
            {
                if (Items != null && MonthSelected != null && SelectedYear != null && SelectedDonViItems != null)
                {
                    var selected = Items.Where(item => (MonthSelected.ValueItem == null || item.Thang == int.Parse(MonthSelected.ValueItem))
                                               && item.Nam == int.Parse(SelectedYear.ValueItem)
                                               && (string.IsNullOrEmpty(SelectedDonViItems.MaDonVi) || item.MaDonVi.Equals(SelectedDonViItems.MaDonVi))).Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll1(value.Value, Items);
                    OnPropertyChanged();
                }
            }
        }

        public bool IsCensorship
        {
            get
            {
                return Items.Where(item => item.Selected).ToList().Count > 0 && Items
                    .Where(item => item.Selected).All(x => x.Selected && !x.IsSummaryVocher && x.BKhoa);
            }
        }

        //public bool IsExportAggregateData
        //{
        //    get
        //    {
        //        return Items.Where(item => item.Selected).ToList().Count > 0 && Items
        //            .Where(item => item.Selected).All(x => x.Selected && x.IsSummaryVocher);
        //    }
        //}

        private void SelectAll(bool select, IEnumerable<TlQtChungTuModel> models)
        {
            foreach (var model in models)
            {
                if (!model.IsSummaryVocher && !model.BDaTongHop.GetValueOrDefault(false)
                                           && MonthSelected != null && (MonthSelected.ValueItem == null || model.Thang == int.Parse(MonthSelected.ValueItem))
                                           && SelectedYear != null && model.Nam == int.Parse(SelectedYear.ValueItem)
                                           && SelectedDonViItems != null && (string.IsNullOrEmpty(SelectedDonViItems.MaDonVi) || model.MaDonVi.Equals(SelectedDonViItems.MaDonVi)))
                {
                    model.Selected = select;
                }
            }
        }

        private void SelectAll1(bool select, IEnumerable<TlQtChungTuModel> models)
        {
            foreach (var model in models)
            {
                if (model.IsSummaryVocher && !model.BDaTongHop.GetValueOrDefault(false)
                                           && MonthSelected != null && (MonthSelected.ValueItem == null || model.Thang == int.Parse(MonthSelected.ValueItem))
                                           && SelectedYear != null && model.Nam == int.Parse(SelectedYear.ValueItem)
                                           && SelectedDonViItems != null && (string.IsNullOrEmpty(SelectedDonViItems.MaDonVi) || model.MaDonVi.Equals(SelectedDonViItems.MaDonVi)))
                {
                    model.Selected = select;
                }
            }
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                OnPropertyChanged(nameof(IsEnabledTransferButton));
                LoadData();
            }
        }

        public bool IsEnabled
        {
            get
            {
                var lstCtSelected = Items.Where(x => x.Selected);
                if (lstCtSelected.Count() > 0)
                {
                    return lstCtSelected.Any(x => !x.BKhoa);
                }
                else
                {
                    return SelectedItem != null && !SelectedItem.BKhoa;
                }
            }
        }

        private TlQtChungTuChiTietGiaiThichModel _tlRegularDataIntertation;

        public bool IsEnabledTransferButton => TabIndex == ImportTabIndex.MLNS;

        public RegularSettlementDialogViewModel RegularSettlementDialogViewModel { get; }
        public RegularSettlementDetailViewModel RegularSettlementDetailViewModel { get; }
        public RegularSettlementPrintDialogViewModel RegularSettlementPrintDialogViewModel { get; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand TransferDataToBudget { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand CapNhatCommand { get; }

        public RegularSettlementIndexViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            ITlDmDonViService tlDmDonViService,
            INsDonViService nsDonViService,
            ITlQtChungTuService tlQtChungTuService,
            RegularSettlementDialogViewModel regularSettlementDialogViewModel,
            RegularSettlementDetailViewModel regularSettlementDetailViewModel,
            ITlQtChungTuChiTietService tlQtChungTuChiTietService,
            IExportService exportService,
            ITlQtChungTuChiTietGiaiThichService tlQtChungTuChiTietGiaiThichService,
            ITlDmCanBoService tlDmCanBoService,
            INsDtChungTuService nsDtChungTuService,
            RegularSettlementPrintDialogViewModel regularSettlementPrintDialogViewModel)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;

            _tlDmDonViService = tlDmDonViService;
            _nsDonViService = nsDonViService;
            _tlQtChungTuService = tlQtChungTuService;
            _tlQtChungTuChiTietService = tlQtChungTuChiTietService;
            _exportService = exportService;
            _tlQtChungTuChiTietGiaiThichService = tlQtChungTuChiTietGiaiThichService;
            _tlDmCanBoService = tlDmCanBoService;
            _estimationService = nsDtChungTuService;

            RegularSettlementDialogViewModel = regularSettlementDialogViewModel;
            RegularSettlementDetailViewModel = regularSettlementDetailViewModel;
            RegularSettlementPrintDialogViewModel = regularSettlementPrintDialogViewModel;

            SearchCommand = new RelayCommand(o => OnSearch());
            LockCommand = new RelayCommand(OnLock);
            TransferDataToBudget = new RelayCommand(OnTransferToBudget);
            AggregateCommand = new RelayCommand(ConfirmAggregate);
            PrintCommand = new RelayCommand(o => OnOpenPrintDialog());
            CapNhatCommand = new RelayCommand(obj => OnCapNhat());
        }

        public override void Init()
        {
            base.Init();
            TabIndex = ImportTabIndex.Data;
            _sessionInfo = _sessionService.Current;
            LoadMonths();
            LoadYears();
            LoadDonViData();
            LoadData();
        }

        private void LoadData()
        {
            //List<TlQtChungTu> data = _tlQtChungTuService.FindAll().OrderByDescending(x => x.Thang).ToList();
            List<TlQtChungTu> data = new List<TlQtChungTu>();

            var _listDonVi = _nsDonViService.FindByCondition(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.ITrangThai == 1).ToList();
            if (_listDonVi.Any(n => _sessionService.Current.IdsDonViQuanLy.Contains(n.IIDMaDonVi) && n.Loai == "0") || _sessionService.Current.Principal.Equals("admin"))
            {
                data = _tlQtChungTuService.FindAll().OrderByDescending(x => x.Thang).ToList();
            }
            else
            {
                data = _tlQtChungTuService.FindAll().Where(n => _sessionService.Current.IdsPhanHoQuanLy.Contains(n.MaDonVi)).OrderByDescending(x => x.Thang).ToList();
            }

            if (TabIndex == ImportTabIndex.Data)
            {
                data = data.Where(x => string.IsNullOrEmpty(x.STongHop)).OrderByDescending(x => x.Thang).ToList();
            }
            else
            {
                data = data.Where(x => !string.IsNullOrEmpty(x.STongHop)).OrderByDescending(x => x.Thang).ToList();
            }

            Items = _mapper.Map<ObservableCollection<TlQtChungTuModel>>(data.OrderByDescending(x => x.ChungTuIndex));

            foreach (var model in Items)
            {
                if (!string.IsNullOrEmpty(model.STongHop))
                {
                    model.IsSummaryVocher = true;
                }
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(TlQtChungTuModel.Selected))
                    {
                        OnPropertyChanged(nameof(IsCensorship));
                        OnPropertyChanged(nameof(IsButtonEnableLock));
                        OnPropertyChanged(nameof(IsEnabled));
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                    }
                };
            }

            _chungTuView = CollectionViewSource.GetDefaultView(Items);
            _chungTuView.Filter = ChungTuFilter;
        }

        private void OnOpenPrintDialog()
        {
            //RegularSettlementPrintDialogViewModel.ItemsChungTu = Items.Where(n => n.Selected).ToList();
            var lstChungTu = new List<TlQtChungTuModel>();
            RegularSettlementPrintDialogViewModel.BIsDetailView = false;
            if (SelectedItem != null)
            {
                RegularSettlementPrintDialogViewModel.SNam = SelectedItem.Nam.ToString();
                RegularSettlementPrintDialogViewModel.Nam = SelectedItem.Nam;
                RegularSettlementPrintDialogViewModel.Thang = SelectedItem.Thang;
                RegularSettlementPrintDialogViewModel.MaDonVi = SelectedItem.MaDonVi;
                RegularSettlementPrintDialogViewModel.TenDonVi = SelectedItem.TenDonVi;
                RegularSettlementPrintDialogViewModel.ItemsChungTu = new List<TlQtChungTuModel>() { SelectedItem };
            }
            else
            {
                if (SelectedYear != null)
                {
                    RegularSettlementPrintDialogViewModel.SNam = SelectedYear.ValueItem;
                    RegularSettlementPrintDialogViewModel.Nam = int.Parse(SelectedYear.ValueItem);
                }
                if (MonthSelected.ValueItem == null)
                {
                    RegularSettlementPrintDialogViewModel.Thang = (int)TimeConst.Types.CA_NAM;
                }
                else
                {
                    RegularSettlementPrintDialogViewModel.Thang = int.Parse(MonthSelected.ValueItem);
                }
                if (SelectedDonViItems != null)
                {
                    RegularSettlementPrintDialogViewModel.MaDonVi = SelectedDonViItems.MaDonVi;
                    RegularSettlementPrintDialogViewModel.TenDonVi = SelectedDonViItems.TenDonVi;
                }
                RegularSettlementPrintDialogViewModel.ItemsChungTu = null;
            }
            RegularSettlementPrintDialogViewModel.Init();
            RegularSettlementPrintDialogViewModel.ShowDialogHost();
        }

        private void LoadMonths()
        {
            ComboboxItem tatCa = new ComboboxItem("-- Tất cả --", null);
            _months = new List<ComboboxItem>();
            _months.Add(tatCa);

            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }

            //var thang = _sessionService.Current.Month;
            OnPropertyChanged(nameof(Months));
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == _sessionService.Current.Month.ToString());
        }

        private void LoadYears()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 30; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            SelectedYear = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        private void LoadDonViData()
        {
            var data = _tlDmDonViService.FindAll().OrderBy(x => x.XauNoiMa);

            var lstDonVi = new List<TlDmDonViModel>();

            TlDmDonViModel tlDmDonViModel = new TlDmDonViModel();
            tlDmDonViModel.TenDonVi = "-- Tất cả --";
            tlDmDonViModel.Id = Guid.Empty;

            lstDonVi.Add(tlDmDonViModel);
            lstDonVi.AddRange(_mapper.Map<ObservableCollection<TlDmDonViModel>>(data).ToList());

            SelectedDonViItems = tlDmDonViModel;

            DonViItems = new ObservableCollection<TlDmDonViModel>(lstDonVi);
        }

        protected override void OnAdd()
        {
            base.OnAdd();
            RegularSettlementDialogViewModel.ViewState = FormViewState.ADD;
            TlQtChungTuModel tlQtChungTuModel = new TlQtChungTuModel();

            RegularSettlementDialogViewModel.Model = tlQtChungTuModel;
            RegularSettlementDialogViewModel.Model.Thang = (MonthSelected != null && MonthSelected.ValueItem != null) ? int.Parse(MonthSelected.ValueItem) : 1;
            RegularSettlementDialogViewModel.Model.Nam = SelectedYear != null ? int.Parse(SelectedYear.ValueItem) : _sessionInfo.YearOfWork;
            RegularSettlementDialogViewModel.IsSummary = false;
            RegularSettlementDialogViewModel.Init();
            RegularSettlementDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            var view = new RegularSettlementDialog { DataContext = RegularSettlementDialogViewModel };
            DialogHost.Show(view, SettlementScreen.ROOT_DIALOG);
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenChiTietChungTu((TlQtChungTuModel)obj);
        }

        private void OnOpenChiTietChungTu(TlQtChungTuModel tlQtChungTuModel)
        {
            try
            {
                if (tlQtChungTuModel == null)
                    return;
                RegularSettlementDetailViewModel.Model = tlQtChungTuModel;
                RegularSettlementDetailViewModel.Init();
                var view = new RegularSettlementDetail
                {
                    DataContext = RegularSettlementDetailViewModel
                };
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnSelectedItemChanged()
        {
            if (SelectedItem != null)
            {
                IsLock = SelectedItem.BKhoa;
            }
            OnPropertyChanged(nameof(IsEnabled));
            OnPropertyChanged(nameof(IsButtonEnableLock));
        }

        protected override void OnDelete()
        {
            try
            {
                var lstCtSelected = Items.Where(x => x.Selected);
                if (lstCtSelected.Count() > 0)
                {
                    MessageBoxResult result = MessageBox.Show("Đồng chí chắc chắn muốn xóa các chứng từ này (chỉ xóa chứng từ không khóa)?", Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (var it in lstCtSelected)
                        {
                            if (!it.BKhoa)
                            {
                                if (!string.IsNullOrEmpty(it.IidChungTuDuToan))
                                {
                                    var lstIdDuToan = it.IidChungTuDuToan.Split(StringUtils.SEMICOLON, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (var item in lstIdDuToan)
                                    {
                                        var ctDuToan = _estimationService.FindById(Guid.Parse(item));
                                        ctDuToan.BLuongNhanDuLieu = false;
                                        ctDuToan.SDonViNhanDuLieu = string.Join(",", ctDuToan.SDonViNhanDuLieu.Split(',').Where(x => !x.IsEmpty() && !x.Equals(it.MaDonVi)));
                                        _estimationService.Update(ctDuToan);
                                    }
                                }
                                var lstGiaiThich = _tlQtChungTuChiTietGiaiThichService.FindListByChungTuId(it.Id);
                                _tlQtChungTuChiTietService.DeleteByChungTuId(it.Id);
                                _tlQtChungTuChiTietGiaiThichService.RemoveRange(lstGiaiThich);
                                _tlQtChungTuService.Delete(it.Id);
                            }
                        }
                        MessageBoxHelper.Info("Xóa chứng từ thành công");
                    }
                }
                else
                {
                    if (SelectedItem != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Đồng chí chắc chắn muốn xóa chứng từ này?", Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            _tlQtChungTuChiTietService.DeleteByChungTuId(SelectedItem.Id);
                            _tlQtChungTuService.Delete(SelectedItem.Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
            finally
            {
                OnRefresh();
            }
        }

        private void OnTransferToBudget(object obj)
        {
            var lstCtSelected = Items.Where(x => x.Selected);
            if (lstCtSelected.Count() <= 0)
            {
                MessageBox.Show(Resources.AlertChonChungTuChuyenSoLieu, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var messageBox = new NSMessageBoxViewModel(Resources.ConfirmTransferVoucherToBudget, "Xác nhận", NSMessageBoxButtons.YesNo, OnTransferToBudgetHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
            //}
        }

        private void OnLock(object obj)
        {
            if (IsLock)
            {
                if (!CheckChungTuNganSachDaLayDulieu())
                {
                    return;
                }
            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            var messageBox = new NSMessageBoxViewModel(message, "Xác nhận", NSMessageBoxButtons.YesNo, OnLockHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void OnLockHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            if (SelectedItem == null) return;
            DateTime dtNow = DateTime.Now;
            var lstCtSelected = Items.Where(x => x.Selected);
            if (lstCtSelected.Count() > 0)
            {
                IsLock = !IsLock;
                foreach (var it in lstCtSelected)
                {
                    _tlQtChungTuService.LockOrUnlock(it.Id, IsLock);
                    var qtChungTu = Items.First(x => x.Id == it.Id);
                    qtChungTu.BKhoa = IsLock;
                }
                lstCtSelected.ForAll(x => x.Selected = IsLock);
                OnPropertyChanged(nameof(IsCensorship));
            }
            else
            {
                IsLock = !IsLock;
                _tlQtChungTuService.LockOrUnlock(SelectedItem.Id, IsLock);
                var qtChungTu = Items.First(x => x.Id == SelectedItem.Id);
                qtChungTu.BKhoa = !SelectedItem.BKhoa;
            }
        }

        private void OnTransferToBudgetHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            if (SelectedItem == null) return;
            var lstCtSelected = Items.Where(x => x.Selected);
            var bkhoa = true;
            if (lstCtSelected.Count() > 0)
            {
                foreach (var it in lstCtSelected)
                {
                    _tlQtChungTuService.LockOrUnlock(it.Id, bkhoa);
                    var qtChungTu = Items.First(x => x.Id == it.Id);
                    qtChungTu.BKhoa = bkhoa;
                }
            }
            else
            {
                IsLock = !IsLock;
                _tlQtChungTuService.LockOrUnlock(SelectedItem.Id, bkhoa);
                var qtChungTu = Items.First(x => x.Id == SelectedItem.Id);
                qtChungTu.BKhoa = bkhoa;
            }
            LoadData();
        }

        private void ConfirmAggregate(object obj)
        {
            List<TlQtChungTuModel> selectedQtChungTus = Items.Where(x => x.Selected && !x.IsSummaryVocher).ToList();
            bool isSameMonth = selectedQtChungTus.Select(x => x.Thang).Distinct().Count() == 1;
            if (!isSameMonth)
            {
                MessageBox.Show(string.Format(Resources.MsgChungTuKhacThang), Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var checkAllowAggregate = selectedQtChungTus.All(x => x.BKhoa);
            if (checkAllowAggregate)
            {
                OnAggregate();
            }
            else
            {
                string message = Resources.ConfirmAggregate;
                MessageBoxResult result = MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    OnAggregate();
            }
        }

        private void OnAggregate()
        {
            string message = string.Empty;
            List<TlQtChungTuModel> selectedQtChungTus = Items.Where(x => x.Selected && x.BKhoa && !x.IsSummaryVocher).ToList();
            if (selectedQtChungTus.Count <= 0)
            {
                return;
            }
            RegularSettlementDialogViewModel.ViewState = FormViewState.ADD;
            var data = _tlQtChungTuService.FindAll();
            TlQtChungTuModel tlQtChungTuModel = new TlQtChungTuModel();
            tlQtChungTuModel.ChungTuIndex = data.LastOrDefault() == null ? 1 : (data.OrderBy(x => x.ChungTuIndex).LastOrDefault().ChungTuIndex + 1);
            RegularSettlementDialogViewModel.ListIdsQtChungTuSummary = selectedQtChungTus;
            RegularSettlementDialogViewModel.Model = tlQtChungTuModel;
            RegularSettlementDialogViewModel.Model.Thang = selectedQtChungTus.First().Thang;
            RegularSettlementDialogViewModel.Model.Nam = selectedQtChungTus.First().Nam;
            RegularSettlementDialogViewModel.IsSummary = true;
            RegularSettlementDialogViewModel.Init();
            RegularSettlementDialogViewModel.SavedAction = obj =>
            {
                this.LoadData();
                IsAllItemsSelected = false;
                OnPropertyChanged(nameof(IsCensorship));
                TabIndex = ImportTabIndex.MLNS;
            };
            var view = new RegularSettlementDialog { DataContext = RegularSettlementDialogViewModel };
            DialogHost.Show(view, SettlementScreen.ROOT_DIALOG);
        }

        public bool CheckChungTuNganSachDaLayDulieu()
        {
            var lstCtSelected = Items.Where(x => x.Selected);
            if (lstCtSelected.Count() > 0)
            {
                if (lstCtSelected.Any(x => x.BNganSachNhanDuLieu.GetValueOrDefault(false)))
                {
                    if (MessageBoxHelper.Confirm(Resources.UnlockSettelementApprove, Resources.Alert) == MessageBoxResult.Yes)
                    {
                        foreach (var item in lstCtSelected)
                        {
                            var obj = _tlQtChungTuService.FindById(item.Id);
                            obj.BNganSachNhanDuLieu = false;
                            _tlQtChungTuService.Update(obj);
                        }
                        return true;
                    }
                }
                else return true;
            }
            else
            {
                if (SelectedItem != null)
                {
                    if (SelectedItem.BNganSachNhanDuLieu.GetValueOrDefault(false))
                    {
                        if (MessageBoxHelper.Confirm(Resources.UnlockSettelementApprove, Resources.Alert) == MessageBoxResult.Yes)
                        {
                            var obj = _tlQtChungTuService.FindById(SelectedItem.Id);
                            obj.BNganSachNhanDuLieu = false;
                            _tlQtChungTuService.Update(obj);
                            return true;
                        }
                    } 
                    else return true;
                }
            }

            return false;
        }

        public void OnSearch()
        {
            _chungTuView.Refresh();
            ResetSelected();
        }

        public void ResetSelected()
        {
            foreach (var it in Items)
            {
                it.Selected = false;
            }
        }

        private bool ChungTuFilter(object obj)
        {
            bool result = true;
            var item = (TlQtChungTuModel)obj;
            if (SelectedDonViItems != null && !SelectedDonViItems.Id.Equals(Guid.Empty))
            {
                result &= item.MaDonVi == SelectedDonViItems.MaDonVi;
            }
            if (MonthSelected != null && MonthSelected.ValueItem != null)
            {
                result = result && item.Thang == int.Parse(MonthSelected.ValueItem);
            }
            if (SelectedYear != null)
            {
                result = result && item.Nam == int.Parse(SelectedYear.ValueItem);
            }

            if (SearchChungTu != null)
            {
                result = (result && item.SoChungTu.ToLower().Contains(SearchChungTu.ToLower()))
                    || (result && item.MoTa.ToLower().Contains(SearchChungTu.ToLower()));
            }
            return result;
        }

        private void OnCapNhat()
        {
            try
            {
                if (TabIndex == ImportTabIndex.Data)
                {
                    DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Đ/c muốn cập nhật lại quyết toán kinh phí thường xuyên không?", Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    List<TlQtChungTuModel> tlChungTuUpdate = Items.Where(x => x.Selected).ToList();
                    if (tlChungTuUpdate.Any(n => n.BKhoa))
                    {
                        MessageBoxHelper.Info("Không thể cập nhật chứng từ đã bị khóa !");
                        return;
                    }
                    BackgroundWorkerHelper.Run((s, e) =>
                    {
                        IsLoading = true;
                        if (!tlChungTuUpdate.IsEmpty())
                        {
                            if (dialogResult == DialogResult.Yes)
                            {
                                string idXoa = string.Join(",", tlChungTuUpdate.Select(x => x.Id.ToString()));
                                var lstAdd = _mapper.Map<List<TlQtChungTu>>(tlChungTuUpdate);
                                ProcessUpdateData(idXoa, lstAdd);
                            }
                        }
                    }, (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            if (dialogResult == DialogResult.Yes)
                            {
                                MessageBoxHelper.Info("Cập nhật dữ liệu thành công");
                                OnRefresh();
                            }
                        }
                        else
                        {
                            _logger.Error(e.Error.Message);
                        }
                        IsLoading = false;
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        #region Helper
        private void ProcessUpdateData(string idXoa, List<TlQtChungTu> lstData)
        {
            List<TlQtChungTu> chungtuInsert = new List<TlQtChungTu>();
            List<TlQtChungTuChiTiet> detailInsert = new List<TlQtChungTuChiTiet>();
            foreach(var item in lstData)
            {
                var obj = item.Clone();
                obj.Id = Guid.NewGuid();
                if(!string.IsNullOrEmpty(obj.SoChungTu) && obj.SoChungTu.IndexOf("- Cap nhat") == -1)
                    obj.SoChungTu = String.Format("{0} - Cap nhat", obj.SoChungTu);
                obj.NgayTao = DateTime.Now;
                chungtuInsert.Add(obj);

                var dataBangLuongThang = _tlQtChungTuChiTietService.FindByCondition(obj.MaDonVi, obj.Thang, obj.Nam, CachTinhLuong.CACH0).ToList();
                var dataBangLuongTruyThu = _tlQtChungTuChiTietService.FindByCondition(obj.MaDonVi, obj.Thang, obj.Nam, CachTinhLuong.CACH1).ToList();
                var dataBangLuongTruyLinh = _tlQtChungTuChiTietService.FindByCondition(obj.MaDonVi, obj.Thang, obj.Nam, CachTinhLuong.CACH5).ToList();
                var dataBangLuongTongHop = _tlQtChungTuChiTietService.FindByCondition(obj.MaDonVi, obj.Thang, obj.Nam, string.Format("{0},{1}", CachTinhLuong.CACH0, CachTinhLuong.CACH5)).ToList();
                var dataBangLuongBHXH = _tlQtChungTuChiTietService.FindByCondition(obj.MaDonVi, obj.Thang, obj.Nam, CachTinhLuong.CACH2).ToList();
                var dataBangLuongBHXHCach3 = _tlQtChungTuChiTietService.FindByCondition(obj.MaDonVi, obj.Thang, obj.Nam, CachTinhLuong.CACH3).ToList();
                var dataBangLuongBHXHCach4 = _tlQtChungTuChiTietService.FindByCondition(obj.MaDonVi, obj.Thang, obj.Nam, CachTinhLuong.CACH4).ToList();

                var chungTuChiTietQueriesCach0 = dataBangLuongThang.Where(x => x.MaDonVi.Equals(obj.MaDonVi));
                var lstChungTuChiTietCach0 = _mapper.Map<List<TlQtChungTuChiTiet>>(chungTuChiTietQueriesCach0);
                foreach (var child in lstChungTuChiTietCach0)
                {
                    child.Id = Guid.NewGuid();
                    child.IdChungTu = obj.Id;
                    child.IdDonVi = obj.MaDonVi;
                    child.TenDonVi = obj.TenDonVi;
                    child.DateCreated = DateTime.Now;
                    child.UserCreator = _sessionInfo.Principal;
                    child.MaCachTl = CachTinhLuong.CACH0;
                }
                detailInsert.AddRange(lstChungTuChiTietCach0);

                var chungTuChiTietQueriesCach1 = dataBangLuongTruyThu.Where(x => x.MaDonVi.Equals(obj.MaDonVi));
                var lstChungTuChiTietCach1 = _mapper.Map<List<TlQtChungTuChiTiet>>(chungTuChiTietQueriesCach1);
                foreach (var child in lstChungTuChiTietCach1)
                {
                    child.Id = Guid.NewGuid();
                    child.IdChungTu = obj.Id;
                    child.IdDonVi = obj.MaDonVi;
                    child.TenDonVi = obj.TenDonVi;
                    child.DateCreated = DateTime.Now;
                    child.UserCreator = _sessionInfo.Principal;
                    child.MaCachTl = CachTinhLuong.CACH1;
                }
                detailInsert.AddRange(lstChungTuChiTietCach1);

                var chungTuChiTietQueriesCach5 = dataBangLuongTruyLinh.Where(x => x.MaDonVi.Equals(obj.MaDonVi));
                var lstChungTuChiTietCach5 = _mapper.Map<List<TlQtChungTuChiTiet>>(chungTuChiTietQueriesCach5);
                foreach (var child in lstChungTuChiTietCach5)
                {
                    child.Id = Guid.NewGuid();
                    child.IdChungTu = obj.Id;
                    child.IdDonVi = obj.MaDonVi;
                    child.TenDonVi = obj.TenDonVi;
                    child.DateCreated = DateTime.Now;
                    child.UserCreator = _sessionInfo.Principal;
                    child.MaCachTl = CachTinhLuong.CACH5;
                }
                detailInsert.AddRange(lstChungTuChiTietCach5);

                var chungTudataBangLuongBHXH = dataBangLuongBHXH.Where(x => x.MaDonVi.Equals(obj.MaDonVi));
                var lstChungTuChiTietBHXH = _mapper.Map<List<TlQtChungTuChiTiet>>(chungTudataBangLuongBHXH);
                foreach (var child in lstChungTuChiTietBHXH)
                {
                    child.Id = Guid.NewGuid();
                    child.IdChungTu = obj.Id;
                    child.IdDonVi = obj.MaDonVi;
                    child.TenDonVi = obj.TenDonVi;
                    child.DateCreated = DateTime.Now;
                    child.UserCreator = _sessionInfo.Principal;
                    child.MaCachTl = CachTinhLuong.CACH2;
                }
                detailInsert.AddRange(lstChungTuChiTietBHXH);

                var chungTudataBangLuongBHXHCach3 = dataBangLuongBHXHCach3.Where(x => x.MaDonVi.Equals(obj.MaDonVi));
                var lstChungTuChiTietBHXHBHXHCach3 = _mapper.Map<List<TlQtChungTuChiTiet>>(chungTudataBangLuongBHXHCach3);
                foreach (var child in lstChungTuChiTietBHXHBHXHCach3)
                {
                    child.Id = Guid.NewGuid();
                    child.IdChungTu = obj.Id;
                    child.IdDonVi = obj.MaDonVi;
                    child.TenDonVi = obj.TenDonVi;
                    child.DateCreated = DateTime.Now;
                    child.UserCreator = _sessionInfo.Principal;
                    child.MaCachTl = CachTinhLuong.CACH3;
                }
                detailInsert.AddRange(lstChungTuChiTietBHXHBHXHCach3);

                var chungTudataBangLuongBHXHCach4 = dataBangLuongBHXHCach4.Where(x => x.MaDonVi.Equals(obj.MaDonVi));
                var lstChungTuChiTietBHXHBHXHCach4 = _mapper.Map<List<TlQtChungTuChiTiet>>(chungTudataBangLuongBHXHCach4);
                foreach (var child in lstChungTuChiTietBHXHBHXHCach4)
                {
                    child.Id = Guid.NewGuid();
                    child.IdChungTu = obj.Id;
                    child.IdDonVi = obj.MaDonVi;
                    child.TenDonVi = obj.TenDonVi;
                    child.DateCreated = DateTime.Now;
                    child.UserCreator = _sessionInfo.Principal;
                    child.MaCachTl = CachTinhLuong.CACH4;
                }
                detailInsert.AddRange(lstChungTuChiTietBHXHBHXHCach4);

                var chungTuChiTietQueriesTongHop = dataBangLuongTongHop.Where(x => x.MaDonVi.Equals(obj.MaDonVi));
                var lstChungTuChiTietTongHop = _mapper.Map<List<TlQtChungTuChiTiet>>(chungTuChiTietQueriesTongHop);
                foreach (var child in lstChungTuChiTietTongHop)
                {
                    child.Id = Guid.NewGuid();
                    child.IdChungTu = obj.Id;
                    child.IdDonVi = obj.MaDonVi;
                    child.TenDonVi = obj.TenDonVi;
                    child.DateCreated = DateTime.Now;
                    child.UserCreator = _sessionInfo.Principal;
                    child.MaCachTl = string.Empty;
                    child.TongCong -= (detailInsert.FirstOrDefault(x => x.XauNoiMa.Equals(child.XauNoiMa) && x.MaCachTl.Equals(CachTinhLuong.CACH2))?.TongCong ?? 0);
                    child.DieuChinh = child.TongCong;
                }
                detailInsert.AddRange(lstChungTuChiTietTongHop);

                _tlQtChungTuChiTietService.DeleteByChungTuId(item.Id);
                _tlQtChungTuService.Delete(item.Id);
            }
            _tlQtChungTuService.Add(chungtuInsert, detailInsert);
        }

        #endregion
    }
}
