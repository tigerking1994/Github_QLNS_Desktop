using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using MessageBox = System.Windows.MessageBox;
using VTS.QLNS.CTC.App.ViewModel.Salary.Report.ListReport;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.SettlementNumber
{
    public class SettlementNumberIndexViewModel : GridViewModelBase<TlQsChungTuModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlQsChungTuService _tlQsChungTuService;
        private ICollectionView _dtQsChungTuView;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITlQsChungTuChiTietService _tlQsChungTuChiTietService;
        private readonly INsQsChungTuService _nsQsChungTuService;
        private readonly INsQsChungTuChiTietService _nsQsChungTuChiTietService;
        private readonly ITlBaoCaoService _tlBaoCaoService;

        public override string FuncCode => NSFunctionCode.SALARY_SETTLEMENT_SETTLEMENT_NUMBER_INDEX;
        public override string Title => "Quyết toán quân số";
        public override string Name => "Quyết toán quân số";
        public override string Description => "Quyết toán quân số";
        public override PackIconKind IconKind => PackIconKind.ShieldAccount;
        public override Type ContentType => typeof(View.Salary.Settlement.SalarySettlementNumber.SalarySettlementNumberIndex);

        public SettlementNumberDialogViewModel SettlementNumberDialogViewModel { get; }
        public SettlementNumberDetailViewModel SettlementNumberDetailViewModel { get; }
        public SettlementSyntheticDialogViewModel SettlementSyntheticDialogViewModel { get; }
        public ReportDialogViewModel ListReportDialogViewModel { get; }

        public RelayCommand OpenInitializationDataCommand { get; }
        public RelayCommand OpenClockCommand { get; }
        public RelayCommand OpenSyntheticCommand { get; }
        public RelayCommand OpentransferDocument { get; }
        public RelayCommand ExportQsThangCommand { get; }
        public RelayCommand ExportQsQtCommand { get; }
        public RelayCommand ExportChiTietQsCommand { get; }

        public SettlementNumberInittizationDialogViewModel OnInittizationDialogViewModel { get; }

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
                if (SetProperty(ref _monthSelected, value) && _dtQsChungTuView != null)
                {
                    _dtQsChungTuView.Refresh();
                }
            }
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _yearSelected;
        public ComboboxItem YearSelected
        {
            get => _yearSelected;
            set
            {
                if (SetProperty(ref _yearSelected, value) && _dtQsChungTuView != null)
                {
                    _dtQsChungTuView.Refresh();
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
                if (SetProperty(ref _selectedDonViItems, value) && _dtQsChungTuView != null)
                {
                    _dtQsChungTuView.Refresh();
                }
            }
        }

        private string _searchChungTu;
        public string SearchChungTu
        {
            get => _searchChungTu;
            set => SetProperty(ref _searchChungTu, value);
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Where(x => x.IsFilter).Select(x => x.Selected).Distinct().ToList();
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

        public bool IsButtonEnableLock
        {
            get
            {
                var lstCtSelected = Items.Where(x => x.Selected);
                if (lstCtSelected.Count() > 0)
                {
                    var checkTt = lstCtSelected.Select(x => x.IsLock.GetValueOrDefault()).Distinct();
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

        public bool IsCensorship
        {
            get
            {
                return Items.Where(item => item.Selected).ToList().Count > 0 && Items
                    .Where(item => item.Selected).All(x => x.Selected && !x.IsSummaryVocher && x.IsLock.GetValueOrDefault());
            }
        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
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

        public bool IsEnabledTransferButton => TabIndex == ImportTabIndex.MLNS;
        public bool IsEnabledDelete => SelectedItem != null && !SelectedItem.IsLock.GetValueOrDefault(false);

        public string ComboboxDisplayMemberPathDonVi => nameof(SelectedDonViItems.TenDonVi);

        public RelayCommand SearchCommand { get; }

        public SettlementNumberIndexViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            SettlementNumberDialogViewModel settlementNumberDialogViewModel,
            ITlQsChungTuService tlQsChungTuService,
            ITlDmDonViService tlDmDonViService,
            INsDonViService nsDonViService,
            SettlementNumberDetailViewModel settlementNumberDetailViewModel,
            ITlQsChungTuChiTietService tlQsChungTuChiTietService,
            SettlementNumberInittizationDialogViewModel onInittizationDialogViewModel,
            SettlementSyntheticDialogViewModel settlementSyntheticDialogViewModel,
            ReportDialogViewModel reportDialogViewModel,
            INsQsChungTuService nsQsChungTuService,
            INsQsChungTuChiTietService nsQsChungTuChiTietService,
            ITlBaoCaoService tlBaoCaoService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            SettlementNumberDialogViewModel = settlementNumberDialogViewModel;
            _tlQsChungTuService = tlQsChungTuService;
            _tlDmDonViService = tlDmDonViService;
            _nsDonViService = nsDonViService;
            SettlementNumberDetailViewModel = settlementNumberDetailViewModel;
            _tlQsChungTuChiTietService = tlQsChungTuChiTietService;
            OnInittizationDialogViewModel = onInittizationDialogViewModel;
            SettlementSyntheticDialogViewModel = settlementSyntheticDialogViewModel;
            ListReportDialogViewModel = reportDialogViewModel;
            _nsQsChungTuChiTietService = nsQsChungTuChiTietService;
            _nsQsChungTuService = nsQsChungTuService;
            _tlBaoCaoService = tlBaoCaoService;

            SearchCommand = new RelayCommand(o => _dtQsChungTuView.Refresh());
            OpenInitializationDataCommand = new RelayCommand(obj => OnInitializationData());
            OpenClockCommand = new RelayCommand(OnLock);
            OpenSyntheticCommand = new RelayCommand(obj => OnOpenSyntheticDialog());
            OpentransferDocument = new RelayCommand(obj => OnOpenTransferDocument());
            ExportQsThangCommand = new RelayCommand(o => OnPrint("10"));
            ExportQsQtCommand = new RelayCommand(o => OnPrint("15.1"));
            ExportChiTietQsCommand = new RelayCommand(o => OnPrint("17"));
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(10);
            LoadYear();
            LoadMonths();
            LoadDonVi();
            LoadData();
        }

        private void SelectAll(bool select, IEnumerable<TlQsChungTuModel> models)
        {
            var items = models.Where(x => x.IsFilter).ToList();
            foreach (var model in items)
            {
                model.Selected = select;
            }
        }

        protected override void OnAdd()
        {
            TlQsChungTuModel qsChungTuModel = new TlQsChungTuModel();
            DateTime myDate = DateTime.Today;
            DateTime firstDayOfMonth = new DateTime(myDate.Year, DateTime.Now.Month, DateTime.Now.Day);
            qsChungTuModel.NgayTao = firstDayOfMonth;
            if (MonthSelected.ValueItem == null)
            {
                qsChungTuModel.Thang = _sessionService.Current.Month;
            }
            else
            {
                qsChungTuModel.Thang = int.Parse(MonthSelected.ValueItem);
            }
            qsChungTuModel.Nam = int.Parse(YearSelected.ValueItem);
            SettlementNumberDialogViewModel.Model = qsChungTuModel;
            if (MonthSelected.ValueItem != null)
            {
                SettlementNumberDialogViewModel.Model.Thang = int.Parse(MonthSelected.ValueItem);
            }
            SettlementNumberDialogViewModel.Model.Nam = int.Parse(YearSelected.ValueItem);
            SettlementNumberDialogViewModel.ViewState = Utility.Enum.FormViewState.ADD;
            SettlementNumberDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };

            SettlementNumberDialogViewModel.Init();
            var view = new View.Salary.Settlement.SalarySettlementNumber.SalarySettlementNumberDialog
            {
                DataContext = SettlementNumberDialogViewModel
            };
            SettlementNumberDialogViewModel.ShowDialogHost();
        }

        private void LoadData()
        {
            try
            {
                //var listData = _tlQsChungTuService.FindAll().OrderByDescending(x => x.Thang).ToList();
                List<TlQsChungTu> listData = new List<TlQsChungTu>();

                var _listDonVi = _nsDonViService.FindByCondition(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.ITrangThai == 1).ToList();
                if (_listDonVi.Any(n => _sessionService.Current.IdsDonViQuanLy.Contains(n.IIDMaDonVi) && n.Loai == "0") || _sessionService.Current.Principal.Equals("admin"))
                {
                    listData = _tlQsChungTuService.FindAll().OrderByDescending(x => x.Thang).ToList();
                }
                else
                {
                    listData = _tlQsChungTuService.FindAll().Where(n => _sessionService.Current.IdsPhanHoQuanLy.Contains(n.MaDonVi)).OrderByDescending(x => x.Thang).ToList();
                }

                if (TabIndex == ImportTabIndex.Data)
                {
                    listData = listData.Where(x => string.IsNullOrEmpty(x.STongHop)).OrderByDescending(x => x.Thang).ToList();
                }
                else
                {
                    listData = listData.Where(x => !string.IsNullOrEmpty(x.STongHop)).OrderByDescending(x => x.Thang).ToList();
                }
                Items = _mapper.Map<ObservableCollection<TlQsChungTuModel>>(listData);

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
                            OnPropertyChanged(nameof(IsEnabledDelete));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                        }
                    };
                }

                _dtQsChungTuView = CollectionViewSource.GetDefaultView(Items);
                _dtQsChungTuView.Filter = ListQsChungTuFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListQsChungTuFilter(object obj)
        {
            bool result = true;
            var item = (TlQsChungTuModel)obj;
            if (MonthSelected != null && MonthSelected.ValueItem != null)
            {
                result &= item.Thang == int.Parse(MonthSelected.ValueItem);
            }
            if (SelectedDonViItems != null && !SelectedDonViItems.Id.Equals(Guid.Empty))
            {
                result &= item.MaDonVi == SelectedDonViItems.MaDonVi;
            }
            if (YearSelected != null)
            {
                result &= item.Nam == int.Parse(YearSelected.ValueItem);
            }
            if (SearchChungTu != null)
            {
                result = (result && item.SoChungTu.ToLower().Contains(SearchChungTu.ToLower()))
                    || (result && item.MoTa.ToLower().Contains(SearchChungTu.ToLower()));
            }

            item.IsFilter = result;
            return result;
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            ComboboxItem tatCa = new ComboboxItem("-- Tất cả --", null);
            _months.Add(tatCa);
            for (var i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            var thang = _sessionService.Current.Month;
            OnPropertyChanged(nameof(Months));
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == thang.ToString());
        }

        private void LoadDonVi()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);

            var lstDonVi = new List<TlDmDonViModel>();

            TlDmDonViModel tlDmDonViModel = new TlDmDonViModel();
            tlDmDonViModel.TenDonVi = "-- Tất cả --";
            tlDmDonViModel.Id = Guid.Empty;

            lstDonVi.Add(tlDmDonViModel);
            lstDonVi.AddRange(_mapper.Map<ObservableCollection<TlDmDonViModel>>(data).ToList());

            SelectedDonViItems = tlDmDonViModel;

            DonViItems = new ObservableCollection<TlDmDonViModel>(lstDonVi);
        }

        private void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                var year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            YearSelected = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenTlQsChungTuChiTietDetail((TlQsChungTuModel)obj);
        }

        private void OnOpenTlQsChungTuChiTietDetail(TlQsChungTuModel tlQsChungTuModel)
        {
            SettlementNumberDetailViewModel.Model = tlQsChungTuModel;
            SettlementNumberDetailViewModel.Init();
            var view = new View.Salary.Settlement.SalarySettlementNumber.SalarySettlementNumberDetail() { DataContext = SettlementNumberDetailViewModel };
            view.ShowDialog();
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
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
                            if (!it.IsLock.GetValueOrDefault())
                            {
                                _tlQsChungTuChiTietService.DeleteByChungTuId(it.Id);
                                _tlQsChungTuService.Delete(it.Id);
                            }
                        }
                        OnRefresh();
                    }
                }
                else
                {
                    if (SelectedItem != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Đồng chí chắc chắn muốn xóa chứng từ này?", Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            _tlQsChungTuChiTietService.DeleteByChungTuId(SelectedItem.Id);
                            _tlQsChungTuService.Delete(SelectedItem.Id);
                            OnRefresh();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnUpdate()
        {
            try
            {
                SettlementNumberDialogViewModel.Model = SelectedItem;
                SettlementNumberDialogViewModel.ViewState = Utility.Enum.FormViewState.UPDATE;
                SettlementNumberDialogViewModel.Init();
                if (SelectedItem != null)
                {
                    SettlementNumberDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };

                    var view = new View.Salary.Settlement.SalarySettlementNumber.SalarySettlementNumberDialog
                    {
                        DataContext = SettlementNumberDialogViewModel
                    };
                    SettlementNumberDialogViewModel.ShowDialogHost();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnInitializationData()
        {
            try
            {
                var lstDonViQuanSoNam = _tlDmDonViService.FindAllDonViQuanSoNam(int.Parse(YearSelected.ValueItem));
                bool check = true;
                foreach(var donvi in lstDonViQuanSoNam)
                {
                    var chungTuKhoiTao = _tlQsChungTuService.FindAll(x => x.Nam == int.Parse(YearSelected.ValueItem) && x.MaDonVi.Equals(donvi.MaDonVi)).OrderBy(x => x.Thang).FirstOrDefault();
                    if (chungTuKhoiTao is null) { check = false; break; }// có đơn vị chưa có chứng từ
                }
                
                if (check)
                {
                    var chungTuDauNam = _tlQsChungTuService.FindAll(x => x.Nam == int.Parse(YearSelected.ValueItem)).OrderBy(x => x.Thang).First();
                    MessageBox.Show(string.Format("Chứng từ đã được khởi tạo vào tháng {0}/{1}", chungTuDauNam.Thang, chungTuDauNam.Nam), Resources.ConfirmTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                TlQsChungTuModel tlQsChungTuModel = new TlQsChungTuModel();
                OnInittizationDialogViewModel.Model = tlQsChungTuModel;
                OnInittizationDialogViewModel.Model.Thang = 1;
                OnInittizationDialogViewModel.Model.NgayTao = DateTime.Now;
                OnInittizationDialogViewModel.Model.GhiChu = "Chứng từ khởi tạo đầu kì";
                OnInittizationDialogViewModel.Model.Nam = int.Parse(YearSelected.ValueItem);
                OnInittizationDialogViewModel.ViewState = Utility.Enum.FormViewState.ADD;
                OnInittizationDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                OnInittizationDialogViewModel.Init();
                OnInittizationDialogViewModel.ShowDialogHost();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnLock(object obj)
        {
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            var messageBox = new NSMessageBoxViewModel(message, "Xác nhận", NSMessageBoxButtons.YesNo, OnLockHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void OnLockHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            //if (SelectedItem == null) return;
            var lstCtSelected = Items.Where(x => x.Selected);
            if (lstCtSelected.Count() > 0)
            {
                IsLock = !IsLock;
                foreach (var it in lstCtSelected)
                {
                    _tlQsChungTuService.LockOrUnlock(it.Id, IsLock);
                    var qsChungTu = Items.First(x => x.Id == it.Id);
                    qsChungTu.IsLock = IsLock;
                }
                lstCtSelected.ForAll(x => x.Selected = IsLock);
                OnPropertyChanged(nameof(IsCensorship));
            }
            else
            {
                IsLock = !IsLock;
                _tlQsChungTuService.LockOrUnlock(SelectedItem.Id, IsLock);
                var qsChungTu = Items.First(x => x.Id == SelectedItem.Id);
                qsChungTu.IsLock = !SelectedItem.IsLock.GetValueOrDefault();
            }
            //LoadData();

        }

        protected override void OnSelectedItemChanged()
        {
            try
            {
                base.OnSelectedItemChanged();
                if (SelectedItem != null)
                {
                    IsLock = SelectedItem.IsLock.GetValueOrDefault();
                }
                OnPropertyChanged(nameof(IsEnabledDelete));
                OnPropertyChanged(nameof(IsButtonEnableLock)); 
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
        }

        private void OnOpenSyntheticDialog()
        {
            try
            {
                List<TlQsChungTuModel> selectedQsChungTus = Items.Where(x => x.Selected && !x.IsSummaryVocher).ToList();
                bool isSameMonth = selectedQsChungTus.Select(x => x.Thang).Distinct().Count() == 1;
                if (!isSameMonth)
                {
                    MessageBox.Show(string.Format(Resources.MsgChungTuKhacThang), Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                List<TlQsChungTuModel> listChungTuModelCheck = Items.Where(x => x.Selected && !(bool)x.IsLock).ToList();
                if (listChungTuModelCheck.Count() > 0)
                {
                    MessageBox.Show(string.Format(Resources.MsgTongHopQtQsLock), "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    SettlementSyntheticDialogViewModel.Model = selectedQsChungTus;
                    SettlementSyntheticDialogViewModel.ViewState = Utility.Enum.FormViewState.ADD;
                    SettlementSyntheticDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                        LoadData();
                        TabIndex = ImportTabIndex.MLNS;
                    };
                    SettlementSyntheticDialogViewModel.Init();
                    SettlementSyntheticDialogViewModel.ShowDialogHost();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnOpenTransferDocument()
        {
            try
            {
                var lstCtSelected = Items.Where(x => x.Selected);
                if (lstCtSelected.Count() <= 0)
                {
                    MessageBox.Show(Resources.AlertChonChungTuChuyenSoLieu, Resources.Alert, MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                var messageBox = MessageBox.Show(Resources.ConfirmTransferVoucherToBudget, "Xác nhận",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBox != MessageBoxResult.Yes) return;
                IsLock = !IsLock;
                foreach (var it in lstCtSelected)
                {
                    _tlQsChungTuService.LockOrUnlock(it.Id, IsLock);
                    var qsChungTu = Items.First(x => x.Id == it.Id);
                    qsChungTu.IsLock = IsLock;
                }

                LoadData();
                //string message = ValidateTransfer();
                //if (!string.IsNullOrEmpty(message))
                //{
                //    System.Windows.Forms.MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    DialogResult dialogLock = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgTransferChungTu), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    if (dialogLock == DialogResult.Yes)
                //    {
                //        var chungTuNs = _mapper.Map<NsQsChungTu>(SelectedItem);
                //        var predicate = PredicateBuilder.True<TlQsChungTuChiTiet>();
                //        //var cv1 = Guid.Parse("e3f021b1-67cb-4397-9834-f637e98d2268");
                //        predicate = predicate.And(x => SelectedItem.Id.ToString().Equals(x.IdChungTu));
                //        var ChungTuChiTiet = _tlQsChungTuChiTietService.FindAll(predicate);
                //        var ChungTuChiTietModel = _mapper.Map<ObservableCollection<TlQsChungTuChiTietModel>>(ChungTuChiTiet);
                //        var ChungTuChiTietNS = _mapper.Map<ObservableCollection<NsQsChungTuChiTiet>>(ChungTuChiTietModel);
                //        _nsQsChungTuService.Add(chungTuNs);
                //        _nsQsChungTuChiTietService.AddRange(ChungTuChiTietNS.ToList());
                //        System.Windows.Forms.MessageBox.Show(Resources.MsgChuyenDuLieuThanhCong, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //}
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnPrint(string maBaoCao)
        {
            TlBaoCaoModel tlBaoCaoModel = new TlBaoCaoModel();
            var lstBaoCao = _tlBaoCaoService.FindAll();
            var baoCao = lstBaoCao.FirstOrDefault(x => x.MaBaoCao.Equals(maBaoCao));
            if (baoCao != null)
            {
                tlBaoCaoModel = _mapper.Map<TlBaoCaoModel>(baoCao);
            }
            tlBaoCaoModel.SelectedMonth = int.Parse(MonthSelected.ValueItem);
            tlBaoCaoModel.SelectedYear = int.Parse(YearSelected.ValueItem);

            ListReportDialogViewModel.Model = tlBaoCaoModel;
            ListReportDialogViewModel.Init();
            ListReportDialogViewModel.ShowDialogHost();
        }

        private string ValidateTransfer()
        {
            List<string> messages = new List<string>();
            if (string.IsNullOrEmpty(SelectedItem.STongHop))
            {
                messages.Add(string.Format(Resources.MsgNotTransfer));
                goto End;
            }
            else if (!(bool)SelectedItem.IsLock)
            {
                messages.Add(string.Format(Resources.MsgTransferLock));
                goto End;
            }
        End:
            return string.Join(Environment.NewLine, messages);
        }
    }
}