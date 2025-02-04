using AutoMapper;
using FlexCel.Core;
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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy.ImportKhcQuanLyKinhPhi;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy
{
    public class LapKeHoachChiQuanLyIndexViewModel : GridViewModelBase<BhKhcKinhphiQuanlyModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IBhKhcKinhphiQuanlyService _bhKhcKinhphiQuanlyService;
        private readonly IBhKhcKinhphiQuanlyChiTietService _bhKhcKinhphiQuanlyChiTietService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ILog _logger;
        private readonly IExportService _exportService;
        private SessionInfo _sessionInfo;
        private ICollectionView _bhKhKeHoachChiKinhPhiView;
        private ICollectionView _nsDonViModelsView;
        #endregion

        #region Property
        private VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy.ImportKhcQuanLyKinhPhi.ImportKhcQuanLyKinhPhi _importKhcQLKinhPhi;
        public override string GroupName => MenuItemContants.GROUP_CHI;
        public override string Name => "KH chi Kinh phí quản lý";
        public override string Description => "Danh sách kế hoạch chi Kinh phí quản lý " + _sessionService.Current.YearOfWork;
        public override Type ContentType => typeof(LapKeHoachChiQuanLyIndex);
        //public override PackIconKind IconKind => PackIconKind.Money;

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
        public string HeaderSoDaThucHienNam => "Tổng số đã thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderUocThucHienNam => "Tổng ước thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderKehoachThucHienNam => "Tổng kế hoạch thực hiện năm " + (_sessionService.Current.YearOfWork);
        public bool IsExportAggregateData => Items != null && Items.Any(n => n.IsSelected);


        public string ComboboxDisplayMemberPath => nameof(SelectedNsDonViModel.TenDonViIdDonVi);
        public bool IsEnableButtonDataShow => TabIndex == VoucherTabIndex.VOUCHER;
        public bool IsAggregate => Items.Any(x => x.IsSelected);
        public bool IsEnableLock => SelectedItem != null;
        public bool IsExportDataFilter => _selectedBhLapKeHoachChiModel != null;

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private ObservableCollection<DonViModel> _nsDonViModelItems;
        public ObservableCollection<DonViModel> NsDonViModelItems
        {
            get => _nsDonViModelItems;
            set => SetProperty(ref _nsDonViModelItems, value);
        }


        private ComboboxItem _voucherTypeSelected;
        public ComboboxItem VoucherTypeSelected
        {
            get => _voucherTypeSelected;
            set
            {
                SetProperty(ref _voucherTypeSelected, value);
                if (_voucherTypeSelected != null)
                {
                    LoadData();
                }
                IsAllItemsSelected = false;
                IsAllItemSummariesSelected = false;
                UnCheckBoxAll();
            }
        }

        private void UnCheckBoxAll()
        {
            foreach (var item in Items)
            {
                item.IsSelected = false;
            }
        }

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
                        //Items.Where(x => x.IsExpand).ForAll(c => c.IsSelected = value.Value);
                        SelectAll(value.Value, Items);
                        OnPropertyChanged();
                    }
                }
            }
        }
        private string _searchSoKeHoachText;
        public string SearchSoKeHoachText
        {
            get => _searchSoKeHoachText;
            set => SetProperty(ref _searchSoKeHoachText, value);
        }
        private List<BhKhcKinhphiQuanlyModel> _lstChungTuOrigin;
        public List<BhKhcKinhphiQuanlyModel> LstChungTuOrigin
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

        private BhKhcKinhphiQuanlyModel _selectedBhLapKeHoachChiModel;
        public BhKhcKinhphiQuanlyModel SelectedBhLapKeHoachChiModel
        {
            get => _selectedBhLapKeHoachChiModel;
            set
            {
                SetProperty(ref _selectedBhLapKeHoachChiModel, value);
                if (_selectedBhLapKeHoachChiModel != null)
                {
                    IsLock = _selectedBhLapKeHoachChiModel.BIsKhoa;
                }
                else
                {
                    IsEdit = false;
                }

                if (_selectedBhLapKeHoachChiModel == null)
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsExportAggregateData));
                OnPropertyChanged(nameof(IsExportDataFilter));
            }
        }

        private ObservableCollection<ComboboxItem> _projectTypeItems;
        public ObservableCollection<ComboboxItem> ProjectTypeItems
        {
            get => _projectTypeItems;
            set => SetProperty(ref _projectTypeItems, value);
        }

        private ComboboxItem _selectedProjectType;
        public ComboboxItem SelectedProjectType
        {
            get => _selectedProjectType;
            set
            {
                SetProperty(ref _selectedProjectType, value);
                if (_bhKhKeHoachChiKinhPhiView != null) _bhKhKeHoachChiKinhPhiView.Refresh();
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
        #endregion

        #region RelayCommand
        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand FixDataCommand { get; set; }
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand ExportAggregateDataCommand { get; set; }
        public RelayCommand UploadFileCommand { get; set; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand AggregateCommand { get; set; }
        #endregion RelayCommand

        #region ViewModel
        public LapKeHoachChiQuanLyDialogViewModel LapKeHoachChiQuanLyDialogViewModel { get; set; }
        public LapKeHoachChiQuanLyDetailViewModel LapKeHoachChiQuanLyDetailViewModel { get; set; }
        public ImportKhcQuanLyKinhPhiViewModel ImportKhcQuanLyKinhPhiViewModel { get; set; }
        public PrintReportQuanLyKinhPhiViewModel PrintReportQuanLyKinhPhiViewModel { get; set; }
        #endregion

        #region Constructor
        public LapKeHoachChiQuanLyIndexViewModel(ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IBhKhcKinhphiQuanlyService bhKhcKinhphiQuanlyService,
            IBhKhcKinhphiQuanlyChiTietService bhKhcKinhphiQuanlyChiTietService,
            LapKeHoachChiQuanLyDialogViewModel lapKeHoachChiQuanLyDialogViewModel,
            LapKeHoachChiQuanLyDetailViewModel lapKeHoachChiQuanLyDetailViewModel,
            ImportKhcQuanLyKinhPhiViewModel importKhcQuanLyKinhPhiViewModel,
            PrintReportQuanLyKinhPhiViewModel printReportQuanLyKinhPhiViewModel,
            IExportService exportService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _bhKhcKinhphiQuanlyService = bhKhcKinhphiQuanlyService;
            _bhKhcKinhphiQuanlyChiTietService = bhKhcKinhphiQuanlyChiTietService;
            _iNguoiDungDonViService = iNguoiDungDonViService;

            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            SearchCommand = new RelayCommand(obj => SearchData());
            RefreshCommand = new RelayCommand(obj => OnResetFilter());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintReportCommand = new RelayCommand(obj => OnOpenReport(obj));
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
            LapKeHoachChiQuanLyDialogViewModel = lapKeHoachChiQuanLyDialogViewModel;
            LapKeHoachChiQuanLyDetailViewModel = lapKeHoachChiQuanLyDetailViewModel;
            ImportKhcQuanLyKinhPhiViewModel = importKhcQuanLyKinhPhiViewModel;
            PrintReportQuanLyKinhPhiViewModel = printReportQuanLyKinhPhiViewModel;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
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
                OnResetFilter();
                LoadData();
                LoadNsDonVi();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
                LapKeHoachChiQuanLyDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Load data
        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }

        protected override void OnRefresh()
        {
            this.LoadData();
            OnPropertyChanged(nameof(Items));
        }

        private void OnResetFilter()
        {
            try
            {
                SearchSoKeHoachText = string.Empty;
                NsDonViModelItems = null;
                SelectedProjectType = null;
                _bhKhKeHoachChiKinhPhiView?.Refresh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnSelectedChange(object obj)
        {
            SelectedItem = (BhKhcKinhphiQuanlyModel)obj;
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
        private static void SelectAll(bool select, IEnumerable<BhKhcKinhphiQuanlyModel> models)
        {
            foreach (var model in models.Where(x => x.IsFilter))
            {
                model.IsSelected = select;
            }
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                var listChungTu = _bhKhcKinhphiQuanlyService.FindIndex().Where(x => x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(x => x.SSoChungTu);
                _lstChungTuOrigin = _mapper.Map<List<BhKhcKinhphiQuanlyModel>>(listChungTu);
                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == VoucherTabIndex.VOUCHER)
                    {
                        Items = _mapper.Map<ObservableCollection<BhKhcKinhphiQuanlyModel>>(_lstChungTuOrigin.Where(x => x.ILoaiTongHop == KhcBhxhLoaiChungTu.BhxhChungTu && !x.BDaTongHop));
                    }
                    else
                    {
                        var listCTTongHop = listChungTu.Where(x => x.IID_MaDonVi.Equals(_sessionService.Current.IdDonVi) && x.ILoaiTongHop == KhcBhxhLoaiChungTu.BhxhChungTuTongHop).ToList();
                        var listTongHop = new List<BhKhcKinhphiQuanlyModel>();
                        foreach (var ctTongHop in listCTTongHop)
                        {
                            var parent = _mapper.Map<BhKhcKinhphiQuanlyModel>(ctTongHop);
                            parent.IsExpand = true;
                            listTongHop.Add(parent);
                            if (!string.IsNullOrEmpty(ctTongHop.STongHop))
                            {
                                var listChild = _mapper.Map<List<BhKhcKinhphiQuanlyModel>>(listChungTu.Where(x => ctTongHop.STongHop != null && ctTongHop.STongHop.Contains(x.SSoChungTu)));
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                                listTongHop.AddRange(listChild);
                            }
                        }
                        Items = _mapper.Map<ObservableCollection<BhKhcKinhphiQuanlyModel>>(listTongHop);
                    }
                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<BhKhcKinhphiQuanlyModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop));
                }

                LoadNsDonVi();

                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhKhcKinhphiQuanlyModel.IsSelected))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsExportDataFilter));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                            OnPropertyChanged(nameof(IsLock));
                        }
                        if (args.PropertyName == nameof(BhKhcKinhphiQuanlyModel.IsCollapse))
                        {
                            ExpandChild();
                        }
                    };
                }

                _bhKhKeHoachChiKinhPhiView = CollectionViewSource.GetDefaultView(Items);
                _bhKhKeHoachChiKinhPhiView.Filter = KhcChungTuModelsFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool KhcChungTuModelsFilter(object obj)
        {
            if (!(obj is BhKhcKinhphiQuanlyModel temp)) return true;
            var keyword = SearchText?.Trim().ToLower() ?? string.Empty.Trim().ToLower();
            var condition1 = false;
            var condition2 = true;
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(temp.SSoChungTu))
                    condition1 = condition1 || temp.SSoChungTu.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SSoQuyetDinh))
                    condition1 = condition1 || temp.SSoQuyetDinh.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SMoTa))
                    condition1 = condition1 || temp.SMoTa.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SNguoiTao))
                    condition1 = condition1 || temp.SNguoiTao.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.STenDonVi))
                    condition1 = condition1 || temp.STenDonVi.ToLower().Contains(keyword);
            }
            else
            {
                condition1 = true;
            }

            if (SelectedNsDonViModel != null)
            {
                condition2 = condition2 && temp.IID_MaDonVi == SelectedNsDonViModel.IIDMaDonVi;
            }

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

            var result = condition1 && condition2;
            temp.IsFilter = result;
            return result;
        }

        private void ExpandChild()
        {
            Items?.Where(n => n.SoChungTuParent == SelectedItem.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
        }

        private void LoadNsDonVi()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            if (Items != null && Items.Count > 0)
            {
                var idDonVis = Items.Select(x => x.IID_MaDonVi).ToList();
                predicate = predicate.And(x => idDonVis.Any(y => y == x.IIDMaDonVi));
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

        #endregion

        #region On add
        protected override void OnAdd()
        {
            try
            {
                LapKeHoachChiQuanLyDialogViewModel.Model = new BhKhcKinhphiQuanlyModel();
                LapKeHoachChiQuanLyDialogViewModel.IsDetail = true;
                LapKeHoachChiQuanLyDialogViewModel.IsAgregate = false;
                LapKeHoachChiQuanLyDialogViewModel.IsSummary = false;
                LapKeHoachChiQuanLyDialogViewModel.BhKhcKinhphiQuanlyModel = new BhKhcKinhphiQuanlyModel();
                LapKeHoachChiQuanLyDialogViewModel.Name = "THÊM MỚI KẾ HOẠCH CHI";
                LapKeHoachChiQuanLyDialogViewModel.Description = "Tạo mới kế hoạch chi kinh phí quản lý";
                LapKeHoachChiQuanLyDialogViewModel.Init();
                LapKeHoachChiQuanLyDialogViewModel.SavedAction = obj =>
                {
                    var khcChungTu = (BhKhcKinhphiQuanlyModel)obj;
                    this.LoadData();
                    if (khcChungTu != null)
                    {
                        OpenDetailDialog(khcChungTu);
                    }
                };
                var exportView = new LapKeHoachChiQuanLyDialog() { DataContext = LapKeHoachChiQuanLyDialogViewModel };
                DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// Open Detail
        /// </summary>
        /// <param name="bhKhcKinhphiQuanlyModel"></param>
        private void OpenDetailDialog(BhKhcKinhphiQuanlyModel bhKhcKinhphiQuanlyModel)
        {
            var idDonViCurrent = GetNsDonViOfCurrentUser();
            var chungTuTH = Items.FirstOrDefault(item => item.IID_MaDonVi.Equals(idDonViCurrent.IIDMaDonVi));
            LapKeHoachChiQuanLyDetailViewModel.Model = ObjectCopier.Clone(bhKhcKinhphiQuanlyModel);
            LapKeHoachChiQuanLyDetailViewModel.IsVoucherSummary = bhKhcKinhphiQuanlyModel.IID_MaDonVi.Equals(idDonViCurrent.IIDMaDonVi) && !string.IsNullOrEmpty(bhKhcKinhphiQuanlyModel.STongHop);
            LapKeHoachChiQuanLyDetailViewModel.Init();

            var view = new LapKeHoachChiQuanLyDetail() { DataContext = LapKeHoachChiQuanLyDetailViewModel };
            view.ShowDialog();
        }
        #endregion

        #region On update
        protected override void OnUpdate()
        {
            try
            {
                if (SelectedItem.IID_MaDonVi.Equals(_sessionService.Current.IdDonVi))
                {
                    OnAggregateEdit();
                }
                else
                {
                    LapKeHoachChiQuanLyDialogViewModel.IsDetail = true;
                    LapKeHoachChiQuanLyDialogViewModel.IsSummary = false;
                    LapKeHoachChiQuanLyDialogViewModel.Model = SelectedItem;
                    LapKeHoachChiQuanLyDialogViewModel.BhKhcKinhphiQuanlyModel = SelectedItem;
                    LapKeHoachChiQuanLyDialogViewModel.Name = "CẬP NHẬT KẾ HOẠCH CHI";
                    LapKeHoachChiQuanLyDialogViewModel.Description = "Cập nhật kế hoạch chi kinh phí quản lý";
                    LapKeHoachChiQuanLyDialogViewModel.Init();
                    LapKeHoachChiQuanLyDialogViewModel.SavedAction = obj => this.OnRefresh();
                    LapKeHoachChiQuanLyDialogViewModel.ShowDialogHost();
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Search
        private void SearchData()
        {
            if (_bhKhKeHoachChiKinhPhiView != null)
            {
                _bhKhKeHoachChiKinhPhiView.Refresh();
            }
        }
        #endregion

        #region Chung tu tong hop
        private void OnAggregateEdit()
        {
            //kiểm tra trạng thái các bản ghi
            List<BhKhcKinhphiQuanlyModel> selectedSKhcChungTus = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedItem.STongHop) && SelectedItem.STongHop.Contains(x.SSoChungTu)).ToList();

            LapKeHoachChiQuanLyDialogViewModel.IsAgregate = true;
            LapKeHoachChiQuanLyDialogViewModel.IsSummary = true;
            LapKeHoachChiQuanLyDialogViewModel.IsDetail = false;
            LapKeHoachChiQuanLyDialogViewModel.ListIdsBhKhcCheDoBhXhModel = selectedSKhcChungTus;
            LapKeHoachChiQuanLyDialogViewModel.Name = "CẬP NHẬT KẾ HOẠCH CHI";
            LapKeHoachChiQuanLyDialogViewModel.Description = "Cập nhật chứng từ tổng hợp kế hoạch chi kinh phí quản lý";
            LapKeHoachChiQuanLyDialogViewModel.Model = SelectedItem;
            LapKeHoachChiQuanLyDialogViewModel.Init();
            LapKeHoachChiQuanLyDialogViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhKhcKinhphiQuanlyModel)obj);
            };
            var addView = new LapKeHoachChiDialog() { DataContext = LapKeHoachChiQuanLyDialogViewModel };
            DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhKhcKinhphiQuanlyModel)eventArgs.Parameter);
        }

        private void ConfirmAggregate()
        {
            List<BhKhcKinhphiQuanlyModel> selectedBhDmCheDoBhXhChungtu = Items.Where(x => x.IsSelected).ToList();
            bool checkAllowAggregate = selectedBhDmCheDoBhXhChungtu.All(x => x.BIsKhoa);
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
            if (Items.Where(x => x.IsSelected).GroupBy(x => new { x.INamLamViec }).Count() > 1)
            {
                MessageBox.Show(Resources.MsgErrorTongHopKeHoachVonUng, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //kiểm tra đã tồn tại chứng từ tổng hợp từ các chứng từ đã chọn chưa
            OnAddTongHopChungTu();
        }

        private void OnAddTongHopChungTu()
        {
            if (!_sessionService.Current.IsQuanLyDonViCha)
            {
                MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                return;
            }

            List<BhKhcKinhphiQuanlyModel> selectedKhcCheDoBhXhChungTus = Items.Where(x => x.IsSelected && x.BIsKhoa).ToList();
            LapKeHoachChiQuanLyDialogViewModel.IsSummary = true;
            LapKeHoachChiQuanLyDialogViewModel.IsDetail = false;
            LapKeHoachChiQuanLyDialogViewModel.ListIdsBhKhcCheDoBhXhModel = selectedKhcCheDoBhXhChungTus;
            LapKeHoachChiQuanLyDialogViewModel.Model = new BhKhcKinhphiQuanlyModel();
            LapKeHoachChiQuanLyDialogViewModel.Name = "THÊM MỚI KẾ HOẠCH CHI";
            LapKeHoachChiQuanLyDialogViewModel.Description = "Tạo mới chứng từ tổng hợp kế hoạch chi kinh phí quản lý";
            LapKeHoachChiQuanLyDialogViewModel.IsAgregate = true;
            LapKeHoachChiQuanLyDialogViewModel.Init();
            LapKeHoachChiQuanLyDialogViewModel.SavedAction = obj =>
            {
                TabIndex = VoucherTabIndex.VOUCHER;
                this.OnRefresh();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhKhcKinhphiQuanlyModel)obj);
            };

            var view = new LapKeHoachChiQuanLyDialog
            {
                DataContext = LapKeHoachChiQuanLyDialogViewModel
            };
            DialogHost.Show(view, SystemConstants.ROOT_DIALOG);
        }
        #endregion

        #region Report
        private void OnOpenReport(object obj)
        {
            try
            {
                var KhcQLKPCheckPrintType = (KhcQLKPCheckPrintType)((int)obj);
                object content;
                switch (KhcQLKPCheckPrintType)
                {
                    case KhcQLKPCheckPrintType.KHCQLKPCT:
                        PrintReportQuanLyKinhPhiViewModel.KhcQLKPCheckPrintType = KhcQLKPCheckPrintType;
                        PrintReportQuanLyKinhPhiViewModel.IsSummary = false;
                        PrintReportQuanLyKinhPhiViewModel.IsShowTheoTongHop = true;
                        PrintReportQuanLyKinhPhiViewModel.Title = "In báo cáo kế hoạch chi kinh phí quản lý";
                        PrintReportQuanLyKinhPhiViewModel.Description = "In báo cáo kế hoạch chi kinh phí quản lý";
                        PrintReportQuanLyKinhPhiViewModel.Init();

                        content = new PrintReportKeHoachChiQLKinhPhiChiTiet
                        {
                            DataContext = PrintReportQuanLyKinhPhiViewModel
                        };

                        break;
                    case KhcQLKPCheckPrintType.KHCQLKPTH:
                        PrintReportQuanLyKinhPhiViewModel.Title = "In dự toán chi kinh phí quản lý BHXH, BHYT, BHTN";
                        PrintReportQuanLyKinhPhiViewModel.Description = "In dự toán chi kinh phí quản lý BHXH, BHYT, BHTN";
                        PrintReportQuanLyKinhPhiViewModel.KhcQLKPCheckPrintType = KhcQLKPCheckPrintType;
                        PrintReportQuanLyKinhPhiViewModel.IsShowTheoTongHop = false;
                        PrintReportQuanLyKinhPhiViewModel.IsSummary = true;
                        PrintReportQuanLyKinhPhiViewModel.Init();
                        content = new PrintReportKeHoachChiQLKinhPhiChiTiet
                        {
                            DataContext = PrintReportQuanLyKinhPhiViewModel
                        };
                        break;

                    default:
                        content = null;
                        break;
                }

                if (content != null)
                {
                    DialogHost.Show(content, SystemConstants.ROOT_DIALOG, null, null);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Import data
        private void OnImportData()
        {
            try
            {
                ImportKhcQuanLyKinhPhiViewModel.Init();
                ImportKhcQuanLyKinhPhiViewModel.SavedAction = obj =>
                {
                    _importKhcQLKinhPhi.Close();
                    this.OnRefresh();
                    OpenDetailDialog((BhKhcKinhphiQuanlyModel)obj);
                };
                _importKhcQLKinhPhi = new View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy.ImportKhcQuanLyKinhPhi.ImportKhcQuanLyKinhPhi { DataContext = ImportKhcQuanLyKinhPhiViewModel };
                _importKhcQLKinhPhi.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Export
        private void OnExportAggregateData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    List<BhKhcKinhphiQuanlyModel> khcBhxhModelsSummary = Items.Where(x => x.IsSelected).ToList();

                    var yearOfWork = _sessionService.Current.YearOfWork;

                    foreach (var item in khcBhxhModelsSummary)
                    {
                        var currentDonVi = GetNsDonViOfCurrentUser();

                        KhcQuanlyKinhphiChiTietCriteria searchCondition = new KhcQuanlyKinhphiChiTietCriteria();
                        searchCondition.NamLamViec = _sessionService.Current.YearOfWork;
                        searchCondition.IdDonVi = item.IID_MaDonVi;
                        searchCondition.KhcKinhphiQuanlyId = item.IID_BH_KHC_KinhPhiQuanLy;
                        var lstMLNS = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(searchCondition.NamLamViec, LNSValue.LNS_9010003);
                        var khcMucLucsOrder = _bhKhcKinhphiQuanlyChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
                        var listkhcMucLucsOrders = _mapper.Map<ObservableCollection<BhKhcKinhphiQuanlyChiTietModel>>(khcMucLucsOrder).ToList();
                        CalculateData(listkhcMucLucsOrders);
                        listkhcMucLucsOrders = listkhcMucLucsOrders.Where(x => x.IsDataNotNull).ToList();

                        Dictionary<string, object> Data = new Dictionary<string, object>();

                        var FTongTienUocThucHienNamTruoc = listkhcMucLucsOrders?.Where(x => !x.IsHangCha).Select(x => x.FTienUocThucHienNamTruoc).Sum();
                        var FTongTienKeHoachThucHienNamNay = listkhcMucLucsOrders?.Where(x => !x.IsHangCha).Select(x => x.FTienKeHoachThucHienNamNay).Sum();
                        var FTongTienCanBo = listkhcMucLucsOrders?.Where(x => !x.IsHangCha).Select(x => x.FTienCanBo).Sum();
                        var FTongTienQuanLuc = listkhcMucLucsOrders?.Where(x => !x.IsHangCha).Select(x => x.FTienQuanLuc).Sum();
                        var FTongTienTaiChinh = listkhcMucLucsOrders?.Where(x => !x.IsHangCha).Select(x => x.FTienTaiChinh).Sum();
                        var FTongTienQuanY = listkhcMucLucsOrders?.Where(x => !x.IsHangCha).Select(x => x.FTienQuanY).Sum();

                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        Data.Add("TitleFirst", $"KẾ HOẠCH CHI KPQL {_sessionService.Current.YearOfWork}");
                        Data.Add("TitleSecond", $"Ngày chứng từ: {DateUtils.Format(item.DNgayChungTu)}");
                        Data.Add("TxtTitleThird", string.Empty);
                        Data.Add("FormatNumber", formatNumber);
                        Data.Add("FTongTienUocThucHienNamTruoc", FTongTienUocThucHienNamTruoc);
                        Data.Add("FTongTienKeHoachThucHienNamNay", FTongTienKeHoachThucHienNamNay);
                        Data.Add("FTongTienCanBo", FTongTienCanBo);
                        Data.Add("FTongTienQuanLuc", FTongTienQuanLuc);
                        Data.Add("FTongTienTaiChinh", FTongTienTaiChinh);
                        Data.Add("FTongTienQuanY", FTongTienQuanY);
                        Data.Add("SNguoiTao", item.SNguoiTao);
                        Data.Add("Cap2", currentDonVi.TenDonVi);
                        Data.Add("Cap1", _sessionInfo.TenDonViTrucThuocReportHeader);
                        Data.Add("DonVi", _sessionService.Current.TenDonVi);
                        Data.Add("YearWork", yearOfWork);
                        Data.Add("YearWorkOld", yearOfWork - 1);
                        Data.Add("h2", "Lữ đoàn X");
                        Data.Add("h1", "Lữ đoàn X");
                        Data.Add("ListData", listkhcMucLucsOrders);
                        Data.Add("SKTML", lstMLNS);

                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_KHC_KPQL, ExportFileName.RPT_BH_KHC_KPQL_CHUNGTU_CHITIET_BHXH);
                        fileNamePrefix = StringUtils.ConvertVN(item.SSoChungTu + "_" + item.IID_MaDonVi + "_" + item.STenDonVi);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhKhcKinhphiQuanlyModel, BhDmMucLucNganSach, BhKhcKinhphiQuanlyChiTietModel>(templateFileName, Data);
                        var nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);

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

        private void CalculateData(List<BhKhcKinhphiQuanlyChiTietModel> listkhcMucLucsOrders)
        {
            listkhcMucLucsOrders.Where(x => x.IsHangCha).Select(x =>
            {
                x.FTienDaThucHienNamTruoc = 0;
                x.FTienKeHoachThucHienNamNay = 0;
                x.FTienUocThucHienNamTruoc = 0;
                x.FTienQuanLuc = 0;
                x.FTienQuanY = 0;
                x.FTienTaiChinh = 0;
                x.FTienCanBo = 0;
                return x;
            }).ToList();

            var temp = listkhcMucLucsOrders.Where(x => !x.IsHangCha && !x.IsDeleted);
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, listkhcMucLucsOrders);
            }
        }

        private void CalculateParent(Guid idParent, BhKhcKinhphiQuanlyChiTietModel item, List<BhKhcKinhphiQuanlyChiTietModel> listkhcMucLucsOrders)
        {
            var dictByMlns = listkhcMucLucsOrders.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienDaThucHienNamTruoc += item.FTienDaThucHienNamTruoc;
            model.FTienKeHoachThucHienNamNay += item.FTienKeHoachThucHienNamNay;
            model.FTienUocThucHienNamTruoc += item.FTienUocThucHienNamTruoc;
            model.FTienCanBo += item.FTienCanBo;
            model.FTienQuanLuc += item.FTienQuanLuc;
            model.FTienQuanY += item.FTienQuanY;
            model.FTienTaiChinh += item.FTienTaiChinh;

            CalculateParent(model.IdParent, item, listkhcMucLucsOrders);
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }
        #endregion

        #region Delete
        protected override void OnDelete()
        {
            try
            {
                if (SelectedItem != null && (SelectedItem.BIsKhoa)) return;
                if (SelectedItem != null)
                {
                    var entity = _bhKhcKinhphiQuanlyService.FindById(SelectedItem.IID_BH_KHC_KinhPhiQuanLy);

                    if (entity != null && !string.IsNullOrEmpty(entity.SNguoiTao) && !entity.SNguoiTao.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {

                        MessageBox.Show(string.Format(Resources.VoucherDeleteKHTHWarning, entity.SNguoiTao), Resources.Alert);
                        return;
                    }
                }

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, SelectedItem.DNgayChungTu.HasValue ? DateTimeExtension.ToStringDate(SelectedItem.DNgayChungTu.Value) : string.Empty);
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
            try
            {
                if (result != NSDialogResult.Yes) return;
                DateTime dtNow = DateTime.Now;
                if (SelectedItem != null)
                {
                    _bhKhcKinhphiQuanlyService.Delete(SelectedItem.IID_BH_KHC_KinhPhiQuanLy);

                    if (SelectedItem.IID_TongHopID.IsNullOrEmpty())
                    {
                        var lstKeHoachChiTiet = _bhKhcKinhphiQuanlyChiTietService.FindByIdChiTiet(SelectedItem.IID_BH_KHC_KinhPhiQuanLy).ToList();
                        if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                        {
                            foreach (var item in lstKeHoachChiTiet)
                            {
                                _bhKhcKinhphiQuanlyChiTietService.Delete(item.Id);
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(SelectedItem.STongHop))
                        {
                            var lstKeHoachChiTiet = _bhKhcKinhphiQuanlyChiTietService.FindByIdChiTiet(SelectedItem.IID_BH_KHC_KinhPhiQuanLy).ToList();
                            if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                            {
                                foreach (var item in lstKeHoachChiTiet)
                                {
                                    _bhKhcKinhphiQuanlyChiTietService.Delete(item.Id);
                                }
                            }
                        }
                        else
                        {
                            var lstSoCtChild = SelectedItem.STongHop.Split(",");
                            foreach (var soct in lstSoCtChild)
                            {
                                var ctChild = _bhKhcKinhphiQuanlyService.FindByCondition(x => x.SSoChungTu.Equals(soct)
                                        && x.INamLamViec == _sessionService.Current.YearOfWork).FirstOrDefault();
                                if (ctChild != null)
                                {
                                    ctChild.ILoaiTongHop = KhcBhxhLoaiChungTu.BhxhChungTu;
                                    ctChild.BDaTongHop = false;
                                    ctChild.DNgaySua = DateTime.Now;
                                    ctChild.SNguoiSua = _sessionInfo.Principal;
                                    _bhKhcKinhphiQuanlyService.Update(ctChild);
                                }
                            }

                            var lstKeHoachChiTiet = _bhKhcKinhphiQuanlyChiTietService.FindByIdChiTiet(SelectedItem.IID_BH_KHC_KinhPhiQuanLy).ToList();
                            if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                            {
                                foreach (var item in lstKeHoachChiTiet)
                                {
                                    _bhKhcKinhphiQuanlyChiTietService.Delete(item.Id);
                                }
                            }
                        }
                    }
                }

                var itemDeleted = Items.Where(x => x.IID_BH_KHC_KinhPhiQuanLy == SelectedItem.IID_BH_KHC_KinhPhiQuanLy).First();
                Items.Remove(itemDeleted);
                this.LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region OnLockUnLock
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
                    _bhKhcKinhphiQuanlyService.LockOrUnlock(item.IID_BH_KHC_KinhPhiQuanLy, isLock);
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

        #region View Detail
        protected override void OnSelectedItemChanged()
        {
            if (SelectedItem != null)
            {
                OnPropertyChanged(nameof(IsAggregate));
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhKhcKinhphiQuanlyModel)obj);
        }
        #endregion
    }
}
