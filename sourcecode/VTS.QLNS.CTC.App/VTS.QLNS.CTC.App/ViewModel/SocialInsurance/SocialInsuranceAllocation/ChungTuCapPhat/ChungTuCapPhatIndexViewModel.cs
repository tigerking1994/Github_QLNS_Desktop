using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat.ImportChungTuCapPhat;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat.ImportChungTuCapPhat;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat
{
    public class ChungTuCapPhatIndexViewModel : GridViewModelBase<BhCpChungTuModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly ILog _logger;
        private readonly IExportService _exportService;
        private SessionInfo _sessionInfo;
        private ICollectionView _bhCpChungTuView;
        private ICollectionView _nsDonViModelsView;
        private readonly IBhCpChungTuService _bhCpChungTuService;
        private readonly IBhCpChungTuChiTietService _bhCpChungTuChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        #endregion

        #region Property
        List<BhCpChungTuChiTietQuery> _listChungTuChiTiet;
        public override string Name => "Cấp kinh phí";
        public override string Description => "Danh sách cấp kinh phí " + _sessionInfo.YearOfWork;
        public override string Title => "Danh sách cấp kinh phí";
        public override Type ContentType => typeof(ChungTuCapPhatIndex);
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override PackIconKind IconKind => PackIconKind.ViewList;
        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set
            {
                SetProperty(ref _isCollapse, value);
                LoadData();
            }
        }

        public bool IsExportAggregateData => Items != null && Items.Any(n => n.IsSelected);
        public string ComboboxDisplayMemberPath => nameof(SelectedNsDonViModel.TenDonViIdDonVi);
        public bool IsEnableButtonDataShow => TabIndex == VoucherTabIndex.VOUCHER;
        public bool IsAggregate => Items.Any(x => x.IsSelected);
        public bool IsEnableLock => SelectedItem != null;


        private ObservableCollection<DonViModel> _nsDonViModelItems;
        public ObservableCollection<DonViModel> NsDonViModelItems
        {
            get => _nsDonViModelItems;
            set => SetProperty(ref _nsDonViModelItems, value);
        }
        private List<BhCpChungTuModel> _lstChungTuOrigin;
        public List<BhCpChungTuModel> LstChungTuOrigin
        {
            get => _lstChungTuOrigin;
            set
            {
                SetProperty(ref _lstChungTuOrigin, value);
            }
        }


        private void LoadLockStatus()
        {
            var lockStatus = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Toàn bộ", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đã khóa", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Chưa khóa", ValueItem = "2"},
            };

            LockStatus = new ObservableCollection<ComboboxItem>(lockStatus);
            LockStatusSelected = LockStatus.ElementAt(0);
        }

        private void LoadQuarter()
        {
            var lstQuarter = new List<ComboboxItem>
            {
                 new ComboboxItem {DisplayItem = "Tất cả", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Quý I", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Quý II", ValueItem = "2"},
                new ComboboxItem {DisplayItem = "Quý III", ValueItem = "3"},
                new ComboboxItem {DisplayItem = "Quý IV", ValueItem = "4"}
            };

            ItemsQuarter = new ObservableCollection<ComboboxItem>(lstQuarter);
            //QuarterSelected = ItemsQuarter.ElementAt(0);
        }


        private DonViModel _selectedNsDonViModel;
        public DonViModel SelectedNsDonViModel
        {
            get => _selectedNsDonViModel;
            set
            {
                SetProperty(ref _selectedNsDonViModel, value);
                SearchData();
            }
        }
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
                    OnPropertyChanged(nameof(IsEnableButtonDataShow));
                }
            }
        }

        private bool _isLock;

        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
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
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
                {
                    IsLock = true;
                }
                else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                {
                    IsLock = false;
                }
            }
        }


        private ObservableCollection<ComboboxItem> _itemsQuarter = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ItemsQuarter
        {
            get => _itemsQuarter;
            set => SetProperty(ref _itemsQuarter, value);
        }

        private ComboboxItem _quarterSelected;

        public ComboboxItem QuarterSelected
        {
            get => _quarterSelected;
            set
            {
                SetProperty(ref _quarterSelected, value);
                if (_quarterSelected != null)
                {
                    this.LoadData();
                }
            }
        }

        public bool IsButtonEnable
        {
            get
            {
                var result = false;
                var lstSelected = Items.Where(x => x.IsSelected).ToList();
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    result = true;
                }
                else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    var lstSelectedKhoa = lstSelected.Where(x => x.BIsKhoa).ToList();
                    var lstSelectedMo = lstSelected.Where(x => !x.BIsKhoa).ToList();
                    if (lstSelectedKhoa.Count() > 0 && lstSelectedMo.Count() > 0)
                    {
                        result = false;
                    }
                    else if (lstSelectedKhoa.Count() > 0)
                    {
                        IsLock = true;
                        result = true;
                    }
                    else if (lstSelectedMo.Count() > 0)
                    {
                        IsLock = false;
                        result = true;
                    }

                }
                return result;

            }
        }

        public bool IsCensorship
        {
            get
            {
                var itemSelected = Items.Where(x => x.IsSelected);
                return itemSelected.Any() && itemSelected.All(x => !x.IsSummaryVocher && x.BIsKhoa);
            }
        }

        private VoucherTabIndex _tabIndex;
        public VoucherTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadData();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
            }
        }

        private ComboboxItem _selectedDanhMucLoaiChi;
        public ComboboxItem SelectedDanhMucLoaiChi
        {
            get => _selectedDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selectedDanhMucLoaiChi, value);
                if (_selectedDanhMucLoaiChi != null)
                {
                    this.LoadData();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDanhMucLoaiChi;
        public ObservableCollection<ComboboxItem> ItemsDanhMucLoaiChi
        {
            get => _itemsDanhMucLoaiChi;
            set => SetProperty(ref _itemsDanhMucLoaiChi, value);
        }
        #endregion

        #region RelayCommand
        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand FixDataCommand { get; set; }
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand ExportAggregateDataCommand { get; set; }
        public RelayCommand UploadFileCommand { get; set; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand AggregateCommand { get; set; }
        //public RelayCommand LockUnLockCommand { get; }
        #endregion RelayCommand

        #region Model
        public ChungTuCapPhatDialogViewModel ChungTuCapPhatDialogViewModel { get; set; }
        public ChungTuCapPhatDetailViewModel ChungTuCapPhatDetailViewModel { get; set; }
        public ImportChungTuCapPhatViewModel ImportChungTuCapPhatViewModel { get; set; }
        public PrintChungTuCapPhatNoticeViewModel PrintChungTuCapPhatNoticeViewModel { get; set; }
        public PrintChungTuCapPhatDonViViewModel PrintChungTuCapPhatDonViViewModel { get; set; }
        public PrintChungTuCapPhatThongTriViewModel PrintChungTuCapPhatThongTriViewModel { get; set; }
        #endregion

        #region Constructor
        public ChungTuCapPhatIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IExportService exportService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            IBhCpChungTuService bhCpChungTuService,
            IBhCpChungTuChiTietService bhCpChungTuChiTietService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            ChungTuCapPhatDialogViewModel chungTuCapPhatDialogViewModel,
            ChungTuCapPhatDetailViewModel chungTuCapPhatDetailViewModel,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ImportChungTuCapPhatViewModel importChungTuCapPhatViewModel,
            PrintChungTuCapPhatNoticeViewModel printChungTuCapPhatNoticeViewModel,
            PrintChungTuCapPhatDonViViewModel printChungTuCapPhatDonViViewModel,
            PrintChungTuCapPhatThongTriViewModel printChungTuCapPhatThongTriViewModel)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _bhCpChungTuService = bhCpChungTuService;
            _bhCpChungTuChiTietService = bhCpChungTuChiTietService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;

            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            SearchCommand = new RelayCommand(obj => SearchData());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintReportCommand = new RelayCommand(obj => OnOpenReport(obj));
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());

            ChungTuCapPhatDialogViewModel = chungTuCapPhatDialogViewModel;
            ChungTuCapPhatDetailViewModel = chungTuCapPhatDetailViewModel;
            ImportChungTuCapPhatViewModel = importChungTuCapPhatViewModel;
            PrintChungTuCapPhatNoticeViewModel = printChungTuCapPhatNoticeViewModel;
            PrintChungTuCapPhatDonViViewModel = printChungTuCapPhatDonViViewModel;
            PrintChungTuCapPhatThongTriViewModel = printChungTuCapPhatThongTriViewModel;
        }
        #endregion

        #region Init
        public override void Init()
        {
            try
            {
                _tabIndex = VoucherTabIndex.VOUCHER;
                _sessionInfo = _sessionService.Current;
                LoadLockStatus();
                LoadQuarter();
                OnResetFilter();
                LoadData();
                LoadNsDonVi();
                LoadDanhMucLoaiChi();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
                ChungTuCapPhatDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Refresh Data
        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }
        protected override void OnRefresh()
        {
            LoadData();
        }

        #endregion

        #region Load danh muc loai chi
        private void LoadDanhMucLoaiChi()
        {
            ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
            IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDanhMucLoaiChi != null)
            {
                ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenDanhMucLoaiChi,
                    ValueItem = n.Id.ToString(),
                    HiddenValue = n.SLNS.ToString(),
                    Id = n.Id
                }));

                ItemsDanhMucLoaiChi.Insert(0, new ComboboxItem
                {
                    DisplayItem = "--Tất cả--",
                    ValueItem = "-1",
                    HiddenValue = "-1",
                    Id = Guid.NewGuid()
                });
            }

            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
        }
        #endregion

        #region Load data
        public override void LoadData(params object[] args)
        {
            try
            {
                var yearOfWork = _sessionInfo.YearOfWork;
                var listChungTu = _bhCpChungTuService.FindIndex(yearOfWork).OrderBy(x => x.SMaLoaiChi).ThenBy(x => x.IQuy);
                _lstChungTuOrigin = _mapper.Map<List<BhCpChungTuModel>>(listChungTu);
                Items = _mapper.Map<ObservableCollection<BhCpChungTuModel>>(_lstChungTuOrigin.Where(x => x.ILoaiTongHop == AllocationTypeLoaiChungTu.ChungTu));
                //if (_sessionService.Current.IsQuanLyDonViCha)
                //{
                //    if (TabIndex == VoucherTabIndex.VOUCHER)
                //    {
                //        Items = _mapper.Map<ObservableCollection<BhCpChungTuModel>>(_lstChungTuOrigin.Where(x => x.ILoaiTongHop == AllocationTypeLoaiChungTu.ChungTu));
                //    }
                //    else
                //    {
                //        var listCTTongHop = listChungTu.Where(x => x.ILoaiTongHop == AllocationTypeLoaiChungTu.ChungTuTongHop && x.SID_MaDonVi.Equals(_sessionService.Current.IdDonVi)).ToList();
                //        var listTongHop = new List<BhCpChungTuModel>();
                //        foreach (var ctTongHop in listCTTongHop)
                //        {
                //            var parent = _mapper.Map<BhCpChungTuModel>(ctTongHop);
                //            parent.IsExpand = true;
                //            listTongHop.Add(parent);
                //            if (!string.IsNullOrEmpty(ctTongHop.STongHop))
                //            {
                //                var listChild = _mapper.Map<List<BhCpChungTuModel>>(listChungTu.Where(x => ctTongHop.STongHop != null && ctTongHop.STongHop.Contains(x.SSoChungTu)));
                //                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                //                listTongHop.AddRange(listChild);
                //            }
                //        }
                //        Items = _mapper.Map<ObservableCollection<BhCpChungTuModel>>(listTongHop);
                //    }
                //}
                //else
                //{
                //    Items = _mapper.Map<ObservableCollection<BhCpChungTuModel>>(listChungTu);
                //}

                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhCpChungTuModel.IsSelected))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                            OnPropertyChanged(nameof(IsLock));
                        }
                        if (args.PropertyName == nameof(BhCpChungTuModel.IsCollapse))
                        {
                            ExpandChild();
                        }
                    };
                }

                _bhCpChungTuView = CollectionViewSource.GetDefaultView(Items);
                _bhCpChungTuView.Filter = CpChungTuViewFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExpandChild()
        {
            Items?.Where(n => n.SoChungTuParent == SelectedItem.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
        }

        private bool CpChungTuViewFilter(object obj)
        {
            if (!(obj is BhCpChungTuModel temp)) return true;
            var condition1 = true;
            var condition2 = true;
            var condition3 = true;

            if (LockStatusSelected != null)
            {
                if (LockStatusSelected.ValueItem.Equals("1"))
                {
                    condition2 = condition2 && temp.BIsKhoa == true;
                }
                if (LockStatusSelected.ValueItem.Equals("2"))
                {
                    condition2 = condition2 && temp.BIsKhoa == false;
                }
            }

            if (SelectedDanhMucLoaiChi != null && SelectedDanhMucLoaiChi.ValueItem != "-1")
            {

                condition2 = condition2 && temp.IID_LoaiCap == SelectedDanhMucLoaiChi.Id;

            }

            if (QuarterSelected != null)
            {
                if (QuarterSelected.ValueItem != "0")
                {
                    condition3 = condition3 && temp.IQuy == int.Parse(QuarterSelected.ValueItem);
                }
            }

            var result = condition1 && condition2 & condition3;
            temp.IsFilter = result;
            return result;
        }

        private void LoadNsDonVi()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            if (Items != null && Items.Count > 0)
            {
                var listUnit = _nsDonViService.FindByCondition(predicate).ToList();
                NsDonViModelItems = new ObservableCollection<DonViModel>();
                NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
                _nsDonViModelsView = CollectionViewSource.GetDefaultView(NsDonViModelItems);
                _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                    ListSortDirection.Ascending));
                _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.TenDonVi),
                    ListSortDirection.Ascending));
            }
        }

        private void SelectAll(bool select, ObservableCollection<BhCpChungTuModel> models)
        {
            foreach (var model in models.Where(x => x.IsFilter))
            {
                model.IsSelected = select;
            }
        }
        #endregion

        #region Add chung tu thường
        protected override void OnAdd()
        {
            try
            {
                ChungTuCapPhatDialogViewModel.Model = new BhCpChungTuModel();
                ChungTuCapPhatDialogViewModel.IsDetail = true;
                ChungTuCapPhatDialogViewModel.IsAgregate = false;
                ChungTuCapPhatDialogViewModel.IsSummary = false;
                ChungTuCapPhatDialogViewModel.Init();
                ChungTuCapPhatDialogViewModel.SavedAction = obj =>
                {
                    var khcChungTu = (BhCpChungTuModel)obj;
                    this.LoadData();
                    if (khcChungTu != null)
                    {
                        OpenDetailDialog(khcChungTu);
                    }
                };
                var view = new ChungTuCapPhatDialog() { DataContext = ChungTuCapPhatDialogViewModel };
                DialogHost.Show(view, SystemConstants.ROOT_DIALOG);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region OnUpdate
        protected override void OnUpdate()
        {
            try
            {
                if (SelectedItem.ILoaiTongHop.Equals(AllocationTypeLoaiChungTu.ChungTuTongHop))
                {
                    OnAggregateEdit();
                }
                else
                {
                    ChungTuCapPhatDialogViewModel.IsDetail = true;
                    ChungTuCapPhatDialogViewModel.IsSummary = false;
                    ChungTuCapPhatDialogViewModel.Model = SelectedItem;
                    ChungTuCapPhatDialogViewModel.Init();
                    ChungTuCapPhatDialogViewModel.SavedAction = obj =>
                    {
                        var khcChungTu = (BhCpChungTuModel)obj;
                        this.OnRefresh();
                        OpenDetailDialog(khcChungTu);
                    };
                    var exportView = new ChungTuCapPhatDialog() { DataContext = ChungTuCapPhatDialogViewModel };
                    DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Lock chung tu
        protected override void OnLockUnLock()
        {
            try
            {
                if (IsLock)
                {
                    List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                    {
                        MessageBox.Show(Resources.MsgRoleUnlock, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                else
                {
                    if (SelectedItem != null && SelectedItem.SNguoiTao != _sessionService.Current.Principal)
                    {
                        MessageBox.Show(string.Format(Resources.MsgRoleLock, SelectedItem.SNguoiTao), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
                if (MessageBoxHelper.Confirm(message) == MessageBoxResult.Yes)
                    LockConfirmEventHandler();
                LockStatusSelected = LockStatus.ElementAt(0);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LockConfirmEventHandler()
        {
            try
            {
                var lstSelected = Items.Where(x => x.IsSelected).ToList();
                var isLock = !lstSelected.FirstOrDefault().BIsKhoa;
                foreach (var item in lstSelected)
                {
                    _bhCpChungTuService.LockOrUnlock(item.Id, isLock);
                    item.BIsKhoa = !item.BIsKhoa;
                }

                LoadData();
                OnPropertyChanged(nameof(IsLock));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Add chung tu tong hop
        private void ConfirmAggregate()
        {
            List<BhCpChungTuModel> bhCpChungTuModels = Items.Where(x => x.IsSelected).ToList();
            bool checkAllowAggregate = bhCpChungTuModels.All(x => x.BIsKhoa);
            if (checkAllowAggregate)
            {
                OnAggregate();
            }
            else
            {
                string message = Resources.ConfirmAggregate;
                MessageBoxResult result = MessageBoxHelper.Confirm(message);
                if (result == MessageBoxResult.Yes)
                    OnAggregate();
            }
        }

        private void OnAggregate()
        {
            // check quyền được tổng hợp
            List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
            if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
            {
                MessageBox.Show(Resources.MsgRoleAggregate, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //kiểm tra trạng thái các bản ghi
            if (Items.Where(x => x.IsSelected).Any(x => !x.BIsKhoa))
            {
                MessageBox.Show(Resources.AlertAggregateUnLocked, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //kiểm tra cùng giai đoạn
            if (Items.Where(x => x.IsSelected).GroupBy(x => new { x.INamChungTu }).Count() > 1)
            {
                MessageBox.Show(Resources.MsgErrorTongHopKeHoachVonUng, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //kiểm tra cùng giai đoạn
            if (Items.Where(x => x.IsSelected).GroupBy(x => new { x.IID_LoaiCap }).Count() > 1)
            {
                MessageBox.Show(Resources.MsgErrorTongHopLoaiDuToanPhanBo, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //kiểm tra đã tồn tại chứng từ tổng hợp từ các chứng từ đã chọn chưa
            OnAddTongHopChungTu();
        }

        private void OnAddTongHopChungTu()
        {
            try
            {
                if (!_sessionService.Current.IsQuanLyDonViCha)
                {
                    MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                    return;
                }
                List<BhCpChungTuModel> bhCpChungTuModels = Items.Where(x => x.IsSelected && x.BIsKhoa).ToList();
                ChungTuCapPhatDialogViewModel.IsSummary = true;
                ChungTuCapPhatDialogViewModel.IsDetail = false;
                ChungTuCapPhatDialogViewModel.ListBhCpChungTuModel = bhCpChungTuModels;
                ChungTuCapPhatDialogViewModel.Model = new BhCpChungTuModel();
                ChungTuCapPhatDialogViewModel.IsAgregate = true;
                ChungTuCapPhatDialogViewModel.Init();
                ChungTuCapPhatDialogViewModel.SavedAction = obj =>
                {
                    TabIndex = VoucherTabIndex.VOUCHER;
                    this.OnRefresh();
                    OnPropertyChanged(nameof(IsCensorship));
                    IsAllItemsSelected = false;
                    OpenDetailDialog((BhCpChungTuModel)obj);
                };

                var view = new ChungTuCapPhatDialog
                {
                    DataContext = ChungTuCapPhatDialogViewModel
                };

                DialogHost.Show(view, SystemConstants.ROOT_DIALOG);
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Update chung tu
        private void OnAggregateEdit()
        {
            try
            {
                //kiểm tra trạng thái các bản ghi
                List<BhCpChungTuModel> selectedCpChungTu = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedItem.STongHop) && SelectedItem.STongHop.Contains(x.SSoChungTu)).ToList();

                if (selectedCpChungTu == null)
                {
                    return;
                }

                if (!_sessionService.Current.IsQuanLyDonViCha)
                {
                    MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                    return;
                }

                ChungTuCapPhatDialogViewModel.IsAgregate = true;
                ChungTuCapPhatDialogViewModel.IsSummary = true;
                ChungTuCapPhatDialogViewModel.IsDetail = false;
                ChungTuCapPhatDialogViewModel.ListBhCpChungTuModel = selectedCpChungTu;
                ChungTuCapPhatDialogViewModel.Model = SelectedItem;
                ChungTuCapPhatDialogViewModel.Init();
                ChungTuCapPhatDialogViewModel.SavedAction = obj =>
                {
                    TabIndex = VoucherTabIndex.VOUCHER;
                    this.OnRefresh();
                    OnPropertyChanged(nameof(IsCensorship));
                    IsAllItemsSelected = false;
                    OpenDetailDialog((BhCpChungTuModel)obj);
                };

                var view = new ChungTuCapPhatDialog
                {
                    DataContext = ChungTuCapPhatDialogViewModel
                };

                DialogHost.Show(view, SystemConstants.ROOT_DIALOG);
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Delete chung tu
        protected override void OnDelete()
        {
            try
            {
                if (SelectedItem != null && (SelectedItem.BIsKhoa)) return;
                if (SelectedItem != null)
                {
                    var entity = _bhCpChungTuService.FindById(SelectedItem.Id);

                    if (entity != null && !string.IsNullOrEmpty(entity.SNguoiTao) && !entity.SNguoiTao.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {

                        MessageBox.Show(string.Format(Resources.VoucherDeleteKHTHWarning, entity.SNguoiTao), Resources.Alert);
                        return;
                    }
                }

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, SelectedItem.DNgayChungTu.HasValue ? DateTimeExtension.ToStringDate(SelectedItem.DNgayChungTu.Value) : string.Empty);
                var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
                DialogHost.Show(messageBox.Content, SystemConstants.ROOT_DIALOG);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            try
            {
                if (result != NSDialogResult.Yes) return;
                DateTime dtNow = DateTime.Now;
                if (SelectedItem != null)
                {
                    _bhCpChungTuService.Delete(SelectedItem.Id);

                    if (string.IsNullOrEmpty(SelectedItem.STongHop))
                    {
                        var lstKeHoachChiTiet = _bhCpChungTuChiTietService.FindByIdChiTiet(SelectedItem.Id).ToList();
                        if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                        {
                            foreach (var item in lstKeHoachChiTiet)
                            {
                                _bhCpChungTuChiTietService.Delete(item.Id);
                            }
                        }
                    }
                    else
                    {
                        var lstKeHoachChiTiet = _bhCpChungTuChiTietService.FindByIdChiTiet(SelectedItem.Id).ToList();
                        foreach (var item in lstKeHoachChiTiet)
                        {
                            _bhCpChungTuChiTietService.Delete(item.Id);
                        }

                        var lstSoCtChild = SelectedItem.STongHop.Split(",");
                        foreach (var soct in lstSoCtChild)
                        {
                            var ctChild = _bhCpChungTuService.FindByCondition(x => x.SSoChungTu.Equals(soct)
                                    && x.INamChungTu == _sessionService.Current.YearOfWork).FirstOrDefault();
                            if (ctChild != null)
                            {
                                ctChild.ILoaiTongHop = KhcKcbBhxhLoaiChungTu.BhxhChungTu;
                                _bhCpChungTuService.Update(ctChild);
                            }
                        }
                    }
                }

                var itemDeleted = Items.Where(x => x.Id == SelectedItem.Id).First();
                Items.Remove(itemDeleted);
                this.LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Open Report
        private void OnOpenReport(object param)
        {
            try
            {
                if (!_sessionService.Current.IsQuanLyDonViCha)
                {
                    MessageBoxHelper.Warning(Resources.AlertRolePrintReportAllocation);
                    return;
                }

                int dialogType = (int)param;
                switch (dialogType)
                {
                    case (int)AllocationPrintTypeOfBH.PRINT_AllOCATION_NOTICE:
                        PrintChungTuCapPhatNoticeViewModel.IsShowDotCap = true;
                        PrintChungTuCapPhatNoticeViewModel.AllocationPrintType = (AllocationPrintTypeOfBH)dialogType;
                        PrintChungTuCapPhatNoticeViewModel.IsEnableTongTop = true;
                        PrintChungTuCapPhatNoticeViewModel.Init();
                        var view1 = new PrintChungTuCapPhatNotice
                        {
                            DataContext = PrintChungTuCapPhatNoticeViewModel
                        };
                        DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                    case (int)AllocationPrintTypeOfBH.PRINT_ALLOCATION_PLAN:
                        PrintChungTuCapPhatNoticeViewModel.IsShowDotCap = false;
                        PrintChungTuCapPhatNoticeViewModel.AllocationPrintType = (AllocationPrintTypeOfBH)dialogType;
                        PrintChungTuCapPhatNoticeViewModel.Init();
                        var view2 = new PrintChungTuCapPhatNotice
                        {
                            DataContext = PrintChungTuCapPhatNoticeViewModel
                        };
                        DialogHost.Show(view2, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                    case (int)AllocationPrintTypeOfBH.PRINT_ALLOCATION_AGENCY:
                        PrintChungTuCapPhatDonViViewModel.Init();
                        var view3 = new PrintChungTuCapPhatDonVi
                        {
                            DataContext = PrintChungTuCapPhatDonViViewModel
                        };
                        DialogHost.Show(view3, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                    case (int)AllocationPrintTypeOfBH.PRINT_ALLOCATION_TYPES:
                        PrintChungTuCapPhatThongTriViewModel.Init();
                        var view4 = new PrintChungTuCapPhatThongTri
                        {
                            DataContext = PrintChungTuCapPhatThongTriViewModel
                        };
                        DialogHost.Show(view4, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Import data
        private void OnImportData()
        {
            try
            {
                ImportChungTuCapPhatViewModel.Init();
                ImportChungTuCapPhatViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                    OnPropertyChanged(nameof(IsCensorship));
                    this.OnRefresh();
                    IsAllItemsSelected = false;
                    OpenDetailDialog((BhCpChungTuModel)obj);
                };
                var view = new ImportChungTuCapPhatBH
                { DataContext = ImportChungTuCapPhatViewModel };
                var result = view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Export excel

        private void OnExportAggregateData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_BH_CP, ExportFileName.RP_BH_EXPORT_CAPPHAT);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    List<BhCpChungTuModel> listExport = Items.Where(x => x.IsSelected).ToList();

                    foreach (var item in listExport)
                    {
                        List<string> listIdDonVi = item.SID_MaDonVi.Split(",").ToList();
                        foreach (var idDonVi in listIdDonVi)
                        {
                            DonVi donViChild = _nsDonViService.FindByIdDonVi(idDonVi, _sessionInfo.YearOfWork);
                            DonVi donViParent = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);

                            int namLamViec = _sessionInfo.YearOfWork;
                            double? FTongTienDuToan = 0;
                            double? FTongTienDaCap = 0;
                            double? FTongTienKeHoachCap = 0;
                            BhCpChungTuChiChiTietCriteria searchCondition = new BhCpChungTuChiChiTietCriteria();
                            searchCondition.NamLamViec = _sessionService.Current.YearOfWork;
                            searchCondition.SIdDonVi = idDonVi;
                            searchCondition.ILoaiDanhMucChi = item.IID_LoaiCap;
                            searchCondition.CpChungTuChiId = item.Id;
                            searchCondition.SLNS = item.SLNS;
                            searchCondition.NgayChungTu = DateTime.Now;
                            searchCondition.ILoaiTongHop = item.ILoaiTongHop;

                            if (item.SLNS == LNSValue.LNS_9010001_9010002 || item.SLNS == LNSValue.LNS_901_9010001_9010002)
                            {
                                searchCondition.SLNS = LNSValue.LNS_901_9010001_9010002;
                                _listChungTuChiTiet = _bhCpChungTuChiTietService.FindBhCpChungTuCheDoBHXHChiTiet(searchCondition).ToList();
                            }
                            else if (item.SLNS == LNSValue.LNS_9050001_9050002)
                            {
                                _listChungTuChiTiet = _bhCpChungTuChiTietService.FindBhCpChungTuCSSKHSSVandNLDChiTiet(searchCondition).ToList();
                            }
                            else
                            {
                                _listChungTuChiTiet = _bhCpChungTuChiTietService.FindBhCpChungTuChiTiet(searchCondition).ToList();
                            }

                            if (item.SLNS != LNSValue.LNS_9050001_9050002)
                            {
                                _listChungTuChiTiet.ForEach(x =>
                                {
                                    if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                                    {
                                        x.IsHangCha = false;
                                        x.BHangCha = false;
                                    }
                                    if (item.SLNS == LNSValue.LNS_9010001_9010002)
                                    {
                                        x.IsHangCha = false;
                                        x.BHangCha = false;
                                    }
                                });
                            }


                            var lstChungTu = _mapper.Map<ObservableCollection<BhCpChungTuChiTietModel>>(_listChungTuChiTiet);
                            CalculateData(lstChungTu);
                            if (item.SLNS != LNSValue.LNS_9050001_9050002)
                            {
                                FTongTienDuToan = lstChungTu.Select(x => x.FTienDuToan).Sum();
                                FTongTienDaCap = lstChungTu.Select(x => x.FTienDaCap).Sum();
                                FTongTienKeHoachCap = lstChungTu.Select(x => x.FTienKeHoachCap).Sum();
                            }
                            else
                            {
                                FTongTienDuToan = lstChungTu.Where(x => !x.IsHangCha).Select(x => x.FTienDuToan).Sum();
                                FTongTienDaCap = lstChungTu.Where(x => !x.IsHangCha).Select(x => x.FTienDaCap).Sum();
                                FTongTienKeHoachCap = lstChungTu.Where(x => !x.IsHangCha).Select(x => x.FTienKeHoachCap).Sum();
                            }
                            var lstChungTungData = lstChungTu.Where(x => x.IsHasData).ToList();
                            var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
                            predicate = predicate.And(x => x.INamLamViec == namLamViec);
                            var dataSLNS = searchCondition.SLNS.Split(',');
                            IEnumerable<BhDmMucLucNganSach> mucLucNganSaches = _bhDmMucLucNganSachService.FindByCondition(predicate).ToList().OrderBy(x => x.SXauNoiMa);
                            var lstMucLuc = mucLucNganSaches.Where(x => dataSLNS.Contains(x.SLNS)).ToList();
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("FTongTienDuToan", FTongTienDuToan);
                            data.Add("FTongTienDaCap", FTongTienDaCap);
                            data.Add("FTongTienKeHoachCap", FTongTienKeHoachCap);
                            data.Add("Header2", donViChild != null ? donViChild.TenDonVi : string.Empty);
                            data.Add("Header1", donViParent != null ? donViParent.TenDonVi : string.Empty);
                            data.Add("TieuDe1", "Chứng từ cấp phát");
                            data.Add("TieuDe2", string.Format("Số chứng từ: {0}", item.SSoChungTu));
                            data.Add("ThoiGian", string.Format("Ngày chứng từ: {0}", item.DNgayChungTu.HasValue ? item.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty));
                            data.Add("NamLamViec", _sessionInfo.YearOfWork);
                            data.Add("Items", lstChungTungData);
                            data.Add("MLNS", lstMucLuc);
                            double? tongTien = (lstChungTungData != null && lstChungTungData.Count > 0) ? lstChungTungData.Where(n => !n.IsHangCha).Select(n => n.FTienKeHoachCap).Sum() : 0;
                            data.Add("TongTienBangChu", StringUtils.NumberToText(tongTien ?? 0, true));
                            fileNamePrefix = item.SSoChungTu;

                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<BhCpChungTuChiTietModel, BhDmMucLucNganSach>(templateFileName, data);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
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

        private void CalculateData(ObservableCollection<BhCpChungTuChiTietModel> lstChungTu)
        {
            try
            {
                lstChungTu.Where(x => x.IsHangCha)
                 .ForAll(x =>
                 {
                     x.FTienDaCaQuyTruoc = 0;
                     x.FTienDuToan = 0;
                     x.FTienKeHoachCap = 0;
                 });

                var temp = lstChungTu.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
                var dictByMlns = lstChungTu.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());

                var aaa = lstChungTu.GroupBy(x => x.IID_MucLucNganSach);
                foreach (var item in temp)
                {
                    CalculateParent(item.IdParent, item, dictByMlns);
                }
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateParent(Guid idParent, BhCpChungTuChiTietModel item, Dictionary<Guid?, BhCpChungTuChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienDuToan += item.FTienDuToan;
            model.FTienDaCaQuyTruoc += item.FTienDaCap;
            model.FTienKeHoachCap += item.FTienKeHoachCap;

            CalculateParent(model.IdParent, item, dictByMlns);
        }
        #endregion

        #region Search data
        private void OnResetFilter()
        {
            try
            {
                SelectedDanhMucLoaiChi = null;
                _bhCpChungTuView?.Refresh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void SearchData()
        {
            if (_bhCpChungTuView != null)
            {
                _bhCpChungTuView.Refresh();
            }
        }
        #endregion

        #region Selected Change
        private void OnSelectedChange(object obj)
        {
            SelectedItem = (BhCpChungTuModel)obj;
            if (SelectedItem is { BIsKhoa: true } || SelectedItem == null)
            {
                IsEdit = false;
            }
            else
            {
                OnPropertyChanged(nameof(IsExportAggregateData));
                IsEdit = true;
            }
        }
        #endregion

        #region Closing Event Handler
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhCpChungTuModel)eventArgs.Parameter);
        }
        #endregion

        #region Open Detail Dialog
        private void OpenDetailDialog(BhCpChungTuModel SelectedItem)
        {
            ChungTuCapPhatDetailViewModel.Model = ObjectCopier.Clone(SelectedItem);
            ChungTuCapPhatDetailViewModel.Init();
            var view = new ChungTuCapPhatDetail() { DataContext = ChungTuCapPhatDetailViewModel };
            view.ShowDialog();
        }


        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhCpChungTuModel)obj);
        }
        #endregion
    }
}
