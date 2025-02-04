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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV.Import;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV.Import;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV
{
    public class LapKeHoachChiKCBQYDVIndexViewModel : GridViewModelBase<BhKhcKcbModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IBhKhcKcbService _bhKhcKcbService;
        private readonly IBhKhcKcbChiTietService _bhKhcKcbChiTietService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ILog _logger;
        private readonly IExportService _exportService;
        private SessionInfo _sessionInfo;
        private ICollectionView _bhKhKeHoachChiKhacView;
        private ICollectionView _nsDonViModelsView;
        #endregion

        #region Property

        private ImportKhcKCBQYDV _importKhcKCBQYDV;
        public override string GroupName => MenuItemContants.GROUP_CHI;
        public override string Name => "KH chi KCB quân y đơn vị";
        public override string Description => "Danh sách kế hoạch chi KCB quân y đơn vị " + _sessionService.Current.YearOfWork;
        public override Type ContentType => typeof(LapKeHoachChiKCBQYDVIndex);

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
        public bool IsExportDataFilter => _selectedBhKhcKcbModel != null;

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

        private ObservableCollection<ComboboxItem> _loaiDuToanChi = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> LoaiDuToanChi
        {
            get => _loaiDuToanChi;
            set => SetProperty(ref _loaiDuToanChi, value);
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
        private List<BhKhcKcbModel> _lstChungTuOrigin;
        public List<BhKhcKcbModel> LstChungTuOrigin
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

        private ObservableCollection<ComboboxItem> _lockStatus;

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

        private BhKhcKcbModel _selectedBhKhcKcbModel;
        public BhKhcKcbModel SelectedBhLapKeHoachChiModel
        {
            get => _selectedBhKhcKcbModel;
            set
            {
                SetProperty(ref _selectedBhKhcKcbModel, value);
                if (_selectedBhKhcKcbModel != null)
                {
                    IsLock = _selectedBhKhcKcbModel.BIsKhoa;
                }
                else
                {
                    IsEdit = false;
                }

                if (_selectedBhKhcKcbModel == null)
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
                if (_bhKhKeHoachChiKhacView != null) _bhKhKeHoachChiKhacView.Refresh();
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
        public RelayCommand ExportAggregateDataCommand { get; set; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand AggregateCommand { get; set; }
        #endregion RelayCommand

        #region ViewModel
        public LapKeHoachChiKCBQYDVDialogViewModel LapKeHoachChiKCBQYDVDialogViewModel { get; set; }
        public LapKeHoachChiKCBQYDVDetailViewModel LapKeHoachChiKCBQYDVDetailViewModel { get; set; }
        public ImportKhcKCBQYDVViewModel ImportKhcKCBQYDVViewModel { get; set; }
        public PrintReportKhcKCBQYDVViewModel PrintReportKhcKCBQYDVViewModel { get; set; }
        #endregion

        #region Contructor

        public LapKeHoachChiKCBQYDVIndexViewModel(
                ILog logger,
                IMapper mapper,
                ISessionService sessionService,
                INsDonViService nsDonViService,
                IBhKhcKcbService bhKhcKcbService,
                IExportService exportService,
                INsNguoiDungDonViService iNguoiDungDonViService,
                IBhKhcKcbChiTietService bhKhcKcbChiTietService,

                 LapKeHoachChiKCBQYDVDialogViewModel keHoachChiKCBQYDVDialogViewModel,
                 LapKeHoachChiKCBQYDVDetailViewModel keHoachChiKCBQYDVDetailViewModel,
                 ImportKhcKCBQYDVViewModel importKhcKCBQYDVViewModel,
                 PrintReportKhcKCBQYDVViewModel printReportKhcKCBQYDVViewModel
,
                 IBhDmMucLucNganSachService bhDmMucLucNganSachService

            )
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _bhKhcKcbService = bhKhcKcbService;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _bhKhcKcbChiTietService = bhKhcKcbChiTietService;

            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            SearchCommand = new RelayCommand(obj => SearchData());
            RefreshCommand = new RelayCommand(obj => OnResetFilter());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintReportCommand = new RelayCommand(obj => OnOpenReport(obj));
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());


            LapKeHoachChiKCBQYDVDialogViewModel = keHoachChiKCBQYDVDialogViewModel;
            LapKeHoachChiKCBQYDVDetailViewModel = keHoachChiKCBQYDVDetailViewModel;
            ImportKhcKCBQYDVViewModel = importKhcKCBQYDVViewModel;
            PrintReportKhcKCBQYDVViewModel = printReportKhcKCBQYDVViewModel;
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
                LapKeHoachChiKCBQYDVDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region On add
        protected override void OnAdd()
        {
            try
            {
                LapKeHoachChiKCBQYDVDialogViewModel.Model = new BhKhcKcbModel();
                LapKeHoachChiKCBQYDVDialogViewModel.BhKhcKCBModel = new BhKhcKcbModel();
                LapKeHoachChiKCBQYDVDialogViewModel.IsDetail = true;
                LapKeHoachChiKCBQYDVDialogViewModel.IsAgregate = false;
                LapKeHoachChiKCBQYDVDialogViewModel.IsSummary = false;
                LapKeHoachChiKCBQYDVDialogViewModel.Name = "THÊM MỚI KẾ HOẠCH CHI";
                LapKeHoachChiKCBQYDVDialogViewModel.Description = "Tạo mới chứng từ tổng hợp kế hoạch chi khám chữa bệnh quân y đơn vị";
                LapKeHoachChiKCBQYDVDialogViewModel.Init();
                LapKeHoachChiKCBQYDVDialogViewModel.SavedAction = obj =>
                {
                    var khcChungTu = (BhKhcKcbModel)obj;
                    this.LoadData();
                    if (khcChungTu != null)
                    {
                        OpenDetailDialog(khcChungTu);
                    }
                };
                var exportView = new LapKeHoachChiKCBQYDVDialog() { DataContext = LapKeHoachChiKCBQYDVDialogViewModel };
                DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Open Detail
        /// <summary>
        /// Open Detail
        /// </summary>
        /// <param name="khcChungTu"></param>
        private void OpenDetailDialog(BhKhcKcbModel khcChungTu)
        {
            var idDonViCurrent = GetNsDonViOfCurrentUser();
            var chungTuTH = Items.FirstOrDefault(item => item.IID_MaDonVi.Equals(idDonViCurrent.IIDMaDonVi));

            LapKeHoachChiKCBQYDVDetailViewModel.Model = ObjectCopier.Clone(khcChungTu);
            LapKeHoachChiKCBQYDVDetailViewModel.IsVoucherSummary = khcChungTu.IID_MaDonVi.Equals(idDonViCurrent.IIDMaDonVi) && !string.IsNullOrEmpty(khcChungTu.STongHop);
            if (LapKeHoachChiKCBQYDVDetailViewModel.IsVoucherSummary)
            {
                var lstSoKeHoach = new List<string>();
                if (!string.IsNullOrEmpty(khcChungTu.STongHop) && khcChungTu.STongHop.Contains(","))
                {
                    lstSoKeHoach = khcChungTu.STongHop.Split(StringUtils.COMMA).ToList();
                }
                else if (!string.IsNullOrEmpty(khcChungTu.STongHop))
                {
                    lstSoKeHoach.Add(khcChungTu.STongHop);
                }
                LapKeHoachChiKCBQYDVDetailViewModel.LstSoKeHoach = lstSoKeHoach;
            }
            else
            {
                LapKeHoachChiKCBQYDVDetailViewModel.LstSoKeHoach = new List<string>() { khcChungTu.SSoChungTu };
            }

            LapKeHoachChiKCBQYDVDetailViewModel.Init();

            var view = new LapKeHoachChiKCBQYDVDetail() { DataContext = LapKeHoachChiKCBQYDVDetailViewModel };
            view.ShowDialog();
        }
        #endregion

        #region Load data
        public override void LoadData(params object[] args)
        {
            try
            {
                var listChungTu = _bhKhcKcbService.FindIndex().Where(x => x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(x => x.SSoChungTu);
                _lstChungTuOrigin = _mapper.Map<List<BhKhcKcbModel>>(listChungTu);
                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == VoucherTabIndex.VOUCHER)
                    {
                        Items = _mapper.Map<ObservableCollection<BhKhcKcbModel>>(_lstChungTuOrigin.Where(x => x.ILoaiTongHop == KhcBhxhLoaiChungTu.BhxhChungTu && !x.BDaTongHop));
                    }
                    else
                    {
                        var listCTTongHop = listChungTu.Where(x => x.IID_MaDonVi.Equals(_sessionService.Current.IdDonVi) && x.ILoaiTongHop == KhcKcbBhxhLoaiChungTu.BhxhChungTuTongHop).ToList();
                        var listTongHop = new List<BhKhcKcbModel>();
                        foreach (var ctTongHop in listCTTongHop)
                        {
                            var parent = _mapper.Map<BhKhcKcbModel>(ctTongHop);
                            parent.IsExpand = true;
                            listTongHop.Add(parent);
                            if (!string.IsNullOrEmpty(ctTongHop.STongHop))
                            {
                                var listChild = _mapper.Map<List<BhKhcKcbModel>>(listChungTu.Where(x => ctTongHop.STongHop != null && ctTongHop.STongHop.Contains(x.SSoChungTu)));
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                                listTongHop.AddRange(listChild);
                            }
                        }
                        Items = _mapper.Map<ObservableCollection<BhKhcKcbModel>>(listTongHop);
                    }
                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<BhKhcKcbModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop));
                }

                LoadNsDonVi();
                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhKhcKcbModel.IsSelected))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsExportDataFilter));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                            OnPropertyChanged(nameof(IsLock));
                        }
                        if (args.PropertyName == nameof(BhKhcKcbModel.IsCollapse))
                        {
                            ExpandChild();
                        }
                    };
                }

                _bhKhKeHoachChiKhacView = CollectionViewSource.GetDefaultView(Items);
                _bhKhKeHoachChiKhacView.Filter = KhcChungTuModelsFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private bool KhcChungTuModelsFilter(object obj)
        {
            if (!(obj is BhKhcKcbModel temp)) return true;
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

        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }

        protected override void OnRefresh()
        {
            this.LoadData();
            OnPropertyChanged(nameof(Items));
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

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        #endregion

        #region Chung tu tong hop
        private void ConfirmAggregate()
        {
            List<BhKhcKcbModel> selectedBhKhcKcbChungtu = Items.Where(x => x.IsSelected).ToList();
            bool checkAllowAggregate = selectedBhKhcKcbChungtu.All(x => x.BIsKhoa);
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

            List<BhKhcKcbModel> selectedKhcKcbChungTus = Items.Where(x => x.IsSelected && x.BIsKhoa).ToList();
            LapKeHoachChiKCBQYDVDialogViewModel.IsSummary = true;
            LapKeHoachChiKCBQYDVDialogViewModel.IsDetail = false;
            LapKeHoachChiKCBQYDVDialogViewModel.ListIdsBhKhcKcbModel = selectedKhcKcbChungTus;
            LapKeHoachChiKCBQYDVDialogViewModel.Model = new BhKhcKcbModel();
            LapKeHoachChiKCBQYDVDialogViewModel.Name = "THÊM MỚI KẾ HOẠCH CHI";
            LapKeHoachChiKCBQYDVDialogViewModel.Description = "Tạo mới kế hoạch chi khám chữa bệnh quân y đơn vị";
            LapKeHoachChiKCBQYDVDialogViewModel.IsAgregate = true;
            LapKeHoachChiKCBQYDVDialogViewModel.Init();
            LapKeHoachChiKCBQYDVDialogViewModel.SavedAction = obj =>
            {
                TabIndex = VoucherTabIndex.VOUCHER;
                this.OnRefresh();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhKhcKcbModel)obj);
            };

            var view = new LapKeHoachChiKCBQYDVDialog
            {
                DataContext = LapKeHoachChiKCBQYDVDialogViewModel
            };
            DialogHost.Show(view, SystemConstants.ROOT_DIALOG);
        }
        #endregion

        #region Report
        private void OnOpenReport(object obj)
        {
            var KhcQLKPCheckPrintType = (KhcKcbCheckPrintType)((int)obj);
            object content;
            switch (KhcQLKPCheckPrintType)
            {
                case KhcKcbCheckPrintType.KHCKCBBHXHCT:
                    PrintReportKhcKCBQYDVViewModel.KhcKcbCheckType = KhcQLKPCheckPrintType;
                    PrintReportKhcKCBQYDVViewModel.IsSummary = false;
                    PrintReportKhcKCBQYDVViewModel.IsLoaiKCB = true;
                    PrintReportKhcKCBQYDVViewModel.Name = "In kế hoạch chi khác BHXH, BHYT";
                    PrintReportKhcKCBQYDVViewModel.Description = "In kế hoạch chi khác BHXH, BHYT";
                    PrintReportKhcKCBQYDVViewModel.IsShowTheoTongHop = true;
                    PrintReportKhcKCBQYDVViewModel.Init();

                    content = new PrintReportKeHoachChiKCBQYDVChiTiet
                    {
                        DataContext = PrintReportKhcKCBQYDVViewModel
                    };

                    break;
                case KhcKcbCheckPrintType.KHCKCBBHXHTH:
                    PrintReportKhcKCBQYDVViewModel.Name = "In dự toán chi khác BHXH, BHYT";
                    PrintReportKhcKCBQYDVViewModel.Description = "In dự toán chi khác BHXH, BHYT";
                    PrintReportKhcKCBQYDVViewModel.KhcKcbCheckType = KhcQLKPCheckPrintType;
                    PrintReportKhcKCBQYDVViewModel.IsShowTheoTongHop = false;
                    PrintReportKhcKCBQYDVViewModel.Init();
                    PrintReportKhcKCBQYDVViewModel.IsSummary = true;
                    PrintReportKhcKCBQYDVViewModel.IsLoaiKCB = true;
                    content = new PrintReportKeHoachChiKCBQYDVChiTiet
                    {
                        DataContext = PrintReportKhcKCBQYDVViewModel
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
        #endregion

        #region Import data
        private void OnImportData()
        {
            try
            {

                ImportKhcKCBQYDVViewModel.SavedAction = obj =>
                {
                    _importKhcKCBQYDV.Close();
                    this.OnRefresh();
                    IsAllItemsSelected = false;
                    OpenDetailDialog((BhKhcKcbModel)obj);
                };
                ImportKhcKCBQYDVViewModel.Init();
                _importKhcKCBQYDV = new ImportKhcKCBQYDV { DataContext = ImportKhcKCBQYDVViewModel };
                _importKhcKCBQYDV.ShowDialog();
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

                    List<BhKhcKcbModel> khcKcbModelsSummary = Items.Where(x => x.IsSelected).ToList();

                    var yearOfWork = _sessionService.Current.YearOfWork;

                    foreach (var item in khcKcbModelsSummary)
                    {
                        var currentDonVi = GetNsDonViOfCurrentUser();

                        KhcKcbChiTietCriteria searchCondition = new KhcKcbChiTietCriteria();
                        searchCondition.NamLamViec = _sessionService.Current.YearOfWork;
                        searchCondition.IdDonVi = item.IID_MaDonVi;
                        searchCondition.KhcKcbId = item.IID_BH_KHC_KCB;
                        var lstMLNS = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(searchCondition.NamLamViec, LNSValue.LNS_9010004_9010005);
                        var khcMucLucsOrder = _bhKhcKcbChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
                        var listkhcMucLucsOrders = _mapper.Map<ObservableCollection<BhKhcKcbChiTietModel>>(khcMucLucsOrder).ToList();
                        CalculateData(listkhcMucLucsOrders);
                        listkhcMucLucsOrders = listkhcMucLucsOrders.Where(x => x.IsDataNotNull).ToList();

                        Dictionary<string, object> Data = new Dictionary<string, object>();

                        var FTongTienNamTruoc = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.FTienUocThucHienNamTruoc).Sum();
                        var FTongTienNamNay = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.FTienKeHoachThucHienNamNay).Sum();

                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        Data.Add("TitleFirst", $"QUYẾT TOÁN CHI KCB QUÂN Y ĐƠN VỊ {_sessionService.Current.YearOfWork}");
                        Data.Add("TitleSecond", $"Ngày chứng từ: {DateUtils.Format(item.DNgayChungTu)}");
                        Data.Add("TxtTitleThird", string.Empty);
                        Data.Add("FTongTienNamTruoc", FTongTienNamTruoc);
                        Data.Add("FTongTienNamNay", FTongTienNamNay);
                        Data.Add("FormatNumber", formatNumber);
                        Data.Add("SNguoiTao", item.SNguoiTao);
                        Data.Add("Cap2", currentDonVi.TenDonVi);
                        Data.Add("Cap1", _sessionInfo.TenDonViTrucThuoc);
                        Data.Add("DonVi", _sessionService.Current.TenDonVi);
                        Data.Add("YearWork", yearOfWork);
                        Data.Add("YearWorkOld", yearOfWork - 1);
                        Data.Add("h2", "Lữ đoàn X");
                        Data.Add("h1", "Lữ đoàn X");
                        Data.Add("ListData", listkhcMucLucsOrders);
                        Data.Add("SKTML", lstMLNS);

                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_KHC_KCB, ExportFileName.RPT_BH_KHC_KCB_CHUNGTU_CHITIET_BHXH);
                        fileNamePrefix = item.SSoChungTu;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhKhcKcbModel, BhDmMucLucNganSach, BhKhcKcbChiTietModel>(templateFileName, Data);
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

        private void CalculateData(List<BhKhcKcbChiTietModel> listkhcMucLucsOrders)
        {
            listkhcMucLucsOrders.Where(x => x.IsHangCha).Select(x =>
            {
                x.FTienDaThucHienNamTruoc = 0;
                x.FTienKeHoachThucHienNamNay = 0;
                x.FTienUocThucHienNamTruoc = 0;
                return x;
            }).ToList();

            var temp = listkhcMucLucsOrders.Where(x => !x.IsHangCha && !x.IsDeleted);
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, listkhcMucLucsOrders);
            }
        }

        private void CalculateParent(Guid idParent, BhKhcKcbChiTietModel item, List<BhKhcKcbChiTietModel> listkhcMucLucsOrders)
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

            CalculateParent(model.IdParent, item, listkhcMucLucsOrders);
        }
        #endregion

        #region ResetFilter
        private void OnResetFilter()
        {
            try
            {
                SearchSoKeHoachText = string.Empty;
                NsDonViModelItems = null;
                _bhKhKeHoachChiKhacView?.Refresh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Selected Change
        private void OnSelectedChange(object obj)
        {
            SelectedItem = (BhKhcKcbModel)obj;
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
            OpenDetailDialog((BhKhcKcbModel)obj);
        }
        #endregion

        #region Search data
        private void SearchData()
        {
            if (_bhKhKeHoachChiKhacView != null)
            {
                _bhKhKeHoachChiKhacView.Refresh();
            }
        }

        private void SelectAll(bool select, ObservableCollection<BhKhcKcbModel> models)
        {
            foreach (var model in models.Where(x => x.IsFilter))
            {
                model.IsSelected = select;
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
                    _bhKhcKcbService.LockOrUnlock(item.IID_BH_KHC_KCB, isLock);
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

        #region Update
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
                    LapKeHoachChiKCBQYDVDialogViewModel.IsDetail = true;
                    LapKeHoachChiKCBQYDVDialogViewModel.IsSummary = false;
                    LapKeHoachChiKCBQYDVDialogViewModel.Model = SelectedItem;
                    LapKeHoachChiKCBQYDVDialogViewModel.BhKhcKCBModel = SelectedItem;
                    LapKeHoachChiKCBQYDVDialogViewModel.Name = "SỬA KẾ HOẠCH CHI";
                    LapKeHoachChiKCBQYDVDialogViewModel.Description = "Cập nhật kế hoạch chi khám chữa bệnh quân y đơn vị";
                    LapKeHoachChiKCBQYDVDialogViewModel.Init();
                    LapKeHoachChiKCBQYDVDialogViewModel.SavedAction = obj => this.OnRefresh();
                    LapKeHoachChiKCBQYDVDialogViewModel.ShowDialogHost();
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnAggregateEdit()
        {
            //kiểm tra trạng thái các bản ghi
            List<BhKhcKcbModel> selectedSKhcChungTus = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedItem.STongHop) && SelectedItem.STongHop.Contains(x.SSoChungTu)).ToList();

            LapKeHoachChiKCBQYDVDialogViewModel.IsAgregate = true;
            LapKeHoachChiKCBQYDVDialogViewModel.IsSummary = true;
            LapKeHoachChiKCBQYDVDialogViewModel.IsDetail = false;
            LapKeHoachChiKCBQYDVDialogViewModel.BhKhcKCBModel = new BhKhcKcbModel();
            LapKeHoachChiKCBQYDVDialogViewModel.ListIdsBhKhcKcbModel = selectedSKhcChungTus;
            LapKeHoachChiKCBQYDVDialogViewModel.Name = "SỬA KẾ HOẠCH CHI";
            LapKeHoachChiKCBQYDVDialogViewModel.Description = "Cập nhật chứng từ tổng hợp kế hoạch chi khám chữa bệnh quân y đơn vị";
            LapKeHoachChiKCBQYDVDialogViewModel.Model = SelectedItem;
            LapKeHoachChiKCBQYDVDialogViewModel.Init();
            LapKeHoachChiKCBQYDVDialogViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhKhcKcbModel)obj);
            };
            var addView = new LapKeHoachChiKCBQYDVDialog() { DataContext = LapKeHoachChiKCBQYDVDialogViewModel };
            DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhKhcKcbModel)eventArgs.Parameter);
        }
        #endregion

        #region On delete
        protected override void OnDelete()
        {
            try
            {
                if (SelectedItem != null && (SelectedItem.BIsKhoa)) return;
                if (SelectedItem != null)
                {
                    var entity = _bhKhcKcbService.FindById(SelectedItem.IID_BH_KHC_KCB);

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
                    _bhKhcKcbService.Delete(SelectedItem.IID_BH_KHC_KCB);

                    if (SelectedItem.IID_TongHopID.IsNullOrEmpty())
                    {
                        var lstKeHoachChiTiet = _bhKhcKcbChiTietService.FindByIdChiTiet(SelectedItem.IID_BH_KHC_KCB).ToList();
                        if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                        {
                            foreach (var item in lstKeHoachChiTiet)
                            {
                                _bhKhcKcbChiTietService.Delete(item.Id);
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(SelectedItem.STongHop))
                        {
                            var lstKeHoachChiTiet = _bhKhcKcbChiTietService.FindByIdChiTiet(SelectedItem.IID_BH_KHC_KCB).ToList();
                            if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                            {
                                foreach (var item in lstKeHoachChiTiet)
                                {
                                    _bhKhcKcbChiTietService.Delete(item.Id);
                                }
                            }
                        }
                        else
                        {
                            var lstSoCtChild = SelectedItem.STongHop.Split(",");
                            foreach (var soct in lstSoCtChild)
                            {
                                var ctChild = _bhKhcKcbService.FindByCondition(x => x.SSoChungTu.Equals(soct)
                                        && x.INamLamViec == _sessionService.Current.YearOfWork).FirstOrDefault();
                                if (ctChild != null)
                                {
                                    ctChild.ILoaiTongHop = KhcKcbBhxhLoaiChungTu.BhxhChungTu;
                                    ctChild.BDaTongHop = false;
                                    ctChild.SNguoiSua = _sessionService.Current.Principal;
                                    ctChild.DNgaySua = DateTime.Now;
                                    _bhKhcKcbService.Update(ctChild);
                                }
                            }
                            var lstKeHoachChiTiet = _bhKhcKcbChiTietService.FindByIdChiTiet(SelectedItem.IID_BH_KHC_KCB).ToList();
                            if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                            {
                                foreach (var item in lstKeHoachChiTiet)
                                {
                                    _bhKhcKcbChiTietService.Delete(item.Id);
                                }
                            }
                        }
                    }
                }

                var itemDeleted = Items.Where(x => x.IID_BH_KHC_KCB == SelectedItem.IID_BH_KHC_KCB).First();
                Items.Remove(itemDeleted);
                this.LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}
