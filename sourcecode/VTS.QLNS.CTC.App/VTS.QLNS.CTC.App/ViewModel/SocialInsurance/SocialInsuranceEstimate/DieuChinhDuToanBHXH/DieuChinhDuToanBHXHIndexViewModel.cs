using AutoMapper;
using ControlzEx.Standard;
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
using System.Windows.Markup;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH.ImportDieuChinhDuToan;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH.ImportDieuChinhDuToan;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH
{
    public class DieuChinhDuToanBHXHIndexViewModel : GridViewModelBase<BhDtcDcdToanChiModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IBhDtcDcdToanChiService _bhDcdToanChiService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly IBhDanhMucLoaiChiService _bhMucLoaiChiService;
        private readonly IBhDtcDcdToanChiChiTietService _bhDtcDcdToanChiChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IDanhMucService _danhMucService;
        private readonly ILog _logger;
        private readonly IExportService _exportService;
        private SessionInfo _sessionInfo;
        private ICollectionView _bhDieuChinhDuToanView;
        private ICollectionView _nsDonViModelsView;
        private ICollectionView _listBudgetIndex;
        private ImportDieuChinhDuToanBHXH _ImportDieuChinhDuToanBHXH;
        #endregion

        #region Property
        public override string GroupName => MenuItemContants.GROUP_CHI;
        public override string Name => "Điều chỉnh DT chi";
        public override string Description => "Điều chỉnh DT chi " + _sessionService.Current.YearOfWork;
        public override Type ContentType => typeof(DieuChinhDuToanBHXHIndex);
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
        public override PackIconKind IconKind => PackIconKind.AxisZRotateClockwise;

        public bool IsExportAggregateData => Items != null && Items.Any(n => n.IsSelected);
        public string ComboboxDisplayMemberPath => nameof(SelectedNsDonViModel.TenDonViIdDonVi);
        public bool IsEnableButtonDataShow => TabIndex == VoucherTabIndex.VOUCHER;
        public bool IsAggregate => Items.Any(x => x.IsSelected);
        public bool IsEnableLock => SelectedItem != null;

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

        private string _searchSoKeHoachText;
        public string SearchSoKeHoachText
        {
            get => _searchSoKeHoachText;
            set => SetProperty(ref _searchSoKeHoachText, value);
        }

        private List<BhDtcDcdToanChiModel> _lstChungTuOrigin;
        public List<BhDtcDcdToanChiModel> LstChungTuOrigin
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
                    SelectAll(value.Value, Items);
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsEnableButtonDataShow));
                }
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

        private void SelectAll(bool select, ObservableCollection<BhDtcDcdToanChiModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
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

        private DateTime? _dNgayLap;
        public DateTime? DNgayLap
        {
            get => _dNgayLap;
            set
            {
                SetProperty(ref _dNgayLap, value);
                SearchData();
            }
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
        public RelayCommand RefreshCommand { get; set; }
        #endregion RelayCommand

        #region View model
        public DieuChinhDuToanBHXHDialogViewModel DieuChinhDuToanBHXHDialogViewModel { get; set; }
        public DieuChinhDuToanBHXHDetailViewModel DieuChinhDuToanBHXHDetailViewModel { get; set; }
        public ImportDieuChinhDuToanBHXHViewModel ImportDieuChinhDuToanBHXHViewModel { get; set; }
        public PrintReportDieuChinhDuToanViewModel PrintReportDieuChinhDuToanViewModel { get; set; }
        public PrintReportDieuChinhDuToanTheoLanViewModel PrintReportDieuChinhDuToanTheoLanViewModel { get; set; }
        #endregion

        #region Constructor
        public DieuChinhDuToanBHXHIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IBhDtcDcdToanChiService bhDtcDcdToanChiService,
            IExportService exportService,
            IBhDtcDcdToanChiChiTietService bhDtcDcdToanChiChiTietService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            DieuChinhDuToanBHXHDialogViewModel dieuChinhDuToanBHXHDialogViewModel,
            DieuChinhDuToanBHXHDetailViewModel dieuChinhDuToanBHXHDetailViewModel,
            ImportDieuChinhDuToanBHXHViewModel importDieuChinhDuToanBHXHViewModel,
            PrintReportDieuChinhDuToanViewModel printReportDieuChinhDuToanViewModel,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IDanhMucService danhMucService,
            PrintReportDieuChinhDuToanTheoLanViewModel printReportDieuChinhDuToanTheoLanViewModel)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _bhDcdToanChiService = bhDtcDcdToanChiService;
            _bhMucLoaiChiService = bhDanhMucLoaiChiService;
            _bhDtcDcdToanChiChiTietService = bhDtcDcdToanChiChiTietService;
            _danhMucService = danhMucService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;

            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            SearchCommand = new RelayCommand(obj => SearchData());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintReportCommand = new RelayCommand(obj => OnOpenReport(obj));
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
            RefreshCommand = new RelayCommand(obje => Init());

            DieuChinhDuToanBHXHDialogViewModel = dieuChinhDuToanBHXHDialogViewModel;
            DieuChinhDuToanBHXHDetailViewModel = dieuChinhDuToanBHXHDetailViewModel;
            ImportDieuChinhDuToanBHXHViewModel = importDieuChinhDuToanBHXHViewModel;
            PrintReportDieuChinhDuToanViewModel = printReportDieuChinhDuToanViewModel;
            PrintReportDieuChinhDuToanTheoLanViewModel = printReportDieuChinhDuToanTheoLanViewModel;
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
                LoadDanhMucLoaiChi();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
                DieuChinhDuToanBHXHDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
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

        private void LoadDanhMucLoaiChi()
        {
            ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
            IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi = _bhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDanhMucLoaiChi != null)
            {
                ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenDanhMucLoaiChi,
                    ValueItem = n.Id.ToString(),
                    HiddenValue = n.Id.ToString()
                }));
            }

            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                var listChungTu = _bhDcdToanChiService.FindIndex(_sessionInfo.YearOfWork);
                _lstChungTuOrigin = _mapper.Map<List<BhDtcDcdToanChiModel>>(listChungTu);
                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == VoucherTabIndex.VOUCHER)
                    {
                        Items = _mapper.Map<ObservableCollection<BhDtcDcdToanChiModel>>(_lstChungTuOrigin.Where(x => x.ILoaiTongHop == KhcBhxhLoaiChungTu.BhxhChungTu && !x.BDaTongHop));
                    }
                    else
                    {
                        var listCTTongHop = listChungTu.Where(x => x.IID_MaDonVi.Equals(_sessionService.Current.IdDonVi) && x.ILoaiTongHop == KhcKcbBhxhLoaiChungTu.BhxhChungTuTongHop).ToList();
                        var listTongHop = new List<BhDtcDcdToanChiModel>();
                        foreach (var ctTongHop in listCTTongHop)
                        {
                            var parent = _mapper.Map<BhDtcDcdToanChiModel>(ctTongHop);
                            parent.IsExpand = true;
                            listTongHop.Add(parent);
                            if (!string.IsNullOrEmpty(ctTongHop.STongHop))
                            {
                                var listChild = _mapper.Map<List<BhDtcDcdToanChiModel>>(listChungTu.Where(x => ctTongHop.STongHop != null && ctTongHop.STongHop.Contains(x.SSoChungTu)));
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                                listTongHop.AddRange(listChild);
                            }
                        }
                        Items = _mapper.Map<ObservableCollection<BhDtcDcdToanChiModel>>(listTongHop);
                    }
                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<BhDtcDcdToanChiModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop));
                }

                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhDtcDcdToanChiModel.IsSelected))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                            OnPropertyChanged(nameof(IsLock));
                        }
                        if (args.PropertyName == nameof(BhDtcDcdToanChiModel.IsCollapse))
                        {
                            ExpandChild();
                        }
                    };
                }

                _bhDieuChinhDuToanView = CollectionViewSource.GetDefaultView(Items);
                _bhDieuChinhDuToanView.Filter = dtChungTuModelsFilter;
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

        #endregion

        #region On add
        protected override void OnAdd()
        {
            try
            {
                DieuChinhDuToanBHXHDialogViewModel.Model = new BhDtcDcdToanChiModel();
                DieuChinhDuToanBHXHDialogViewModel.BhDtcDcdToanChiModel = new BhDtcDcdToanChiModel();
                DieuChinhDuToanBHXHDialogViewModel.IsDetail = true;
                DieuChinhDuToanBHXHDialogViewModel.IsAgregate = false;
                DieuChinhDuToanBHXHDialogViewModel.IsSummary = false;
                DieuChinhDuToanBHXHDialogViewModel.Init();
                DieuChinhDuToanBHXHDialogViewModel.SavedAction = obj =>
                {
                    var khcChungTu = (BhDtcDcdToanChiModel)obj;
                    this.LoadData();
                    if (khcChungTu != null)
                    {
                        OpenDetailDialog(khcChungTu);
                    }
                };
                var exportView = new DieuChinhDuToanBHXHDialog() { DataContext = DieuChinhDuToanBHXHDialogViewModel };
                DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Open detail 
        private void OpenDetailDialog(BhDtcDcdToanChiModel SelectedItem)
        {
            DieuChinhDuToanBHXHDetailViewModel.Model = ObjectCopier.Clone(SelectedItem);
            DieuChinhDuToanBHXHDetailViewModel.Init();
            var view = new DieuChinhDuToanBHXHDetail() { DataContext = DieuChinhDuToanBHXHDetailViewModel };
            view.ShowDialog();
        }
        #endregion

        #region Filter
        private bool dtChungTuModelsFilter(object obj)
        {
            if (!(obj is BhDtcDcdToanChiModel temp)) return true;
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

            if (DNgayLap != null)
            {
                condition2 = condition2 && temp.DNgayQuyetDinh.HasValue && temp.DNgayQuyetDinh.Value.ToString("yyyy-MM-dd").ToLower().Contains(DNgayLap.Value.ToString("yyyy-MM-dd"));
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

            if (SelectedDanhMucLoaiChi != null)
            {

                condition2 = condition2 && temp.IID_LoaiCap == new Guid(SelectedDanhMucLoaiChi.ValueItem);

            }

            var result = condition1 && condition2;
            return result;
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

        private void OnResetFilter()
        {
            try
            {
                SearchSoKeHoachText = string.Empty;
                SelectedDanhMucLoaiChi = null;
                DNgayLap = null;
                _bhDieuChinhDuToanView?.Refresh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void SearchData()
        {
            _bhDieuChinhDuToanView?.Refresh();
        }

        protected override void OnSelectedItemChanged()
        {
            if (SelectedItem != null)
            {
                OnPropertyChanged(nameof(IsAggregate));
            }
        }

        protected override void OnRefresh()
        {
            this.LoadData();
            OnPropertyChanged(nameof(Items));
        }

        private void OnSelectedChange(object obj)
        {
            SelectedItem = (BhDtcDcdToanChiModel)obj;
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

        #region Add Chung tu tong hop
        private void ConfirmAggregate()
        {
            List<BhDtcDcdToanChiModel> selectedBhKhcKcbChungtu = Items.Where(x => x.IsSelected).ToList();
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
            if (!_sessionService.Current.IsQuanLyDonViCha)
            {
                MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                return;
            }

            List<BhDtcDcdToanChiModel> bhDtcDcdToanChiModels = Items.Where(x => x.IsSelected && x.BIsKhoa).ToList();
            DieuChinhDuToanBHXHDialogViewModel.IsSummary = true;
            DieuChinhDuToanBHXHDialogViewModel.IsDetail = false;
            DieuChinhDuToanBHXHDialogViewModel.ListIdsBhDtcDcdToanChiModel = bhDtcDcdToanChiModels;
            DieuChinhDuToanBHXHDialogViewModel.Model = new BhDtcDcdToanChiModel();
            DieuChinhDuToanBHXHDialogViewModel.IsAgregate = true;
            DieuChinhDuToanBHXHDialogViewModel.Init();
            DieuChinhDuToanBHXHDialogViewModel.SavedAction = obj =>
            {
                TabIndex = VoucherTabIndex.VOUCHER;
                this.OnRefresh();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhDtcDcdToanChiModel)obj);
            };

            var view = new DieuChinhDuToanBHXHDialog
            {
                DataContext = DieuChinhDuToanBHXHDialogViewModel
            };
            DialogHost.Show(view, SystemConstants.ROOT_DIALOG);
        }
        #endregion

        #region Khoa chung tu 
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
                    _bhDcdToanChiService.LockOrUnlock(item.IID_BH_DTC, isLock);
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

        #region Print Report
        private void OnOpenReport(object obj)
        {
            var dtDcDtCheckPrintType = (DtDcDtCheckPrintType)((int)obj);
            object content;
            switch (dtDcDtCheckPrintType)
            {
                //case DtDcDtCheckPrintType.DTDCCT:
                //    PrintReportDieuChinhDuToanViewModel.dtDcDtCheckPrintType = dtDcDtCheckPrintType;

                //    PrintReportDieuChinhDuToanViewModel.IsShowInTheoTongHop = true;
                //    PrintReportDieuChinhDuToanViewModel.Name = "In điều chỉnh dự toán";
                //    PrintReportDieuChinhDuToanViewModel.Description = "In điều chỉnh dự toán";
                //    PrintReportDieuChinhDuToanViewModel.Init();
                //    content = new PrintReportDieuChinhDuToanChiTiet
                //    {
                //        DataContext = PrintReportDieuChinhDuToanViewModel
                //    };

                //    break;
                case DtDcDtCheckPrintType.DTDCTheoDonVi:
                    PrintReportDieuChinhDuToanViewModel.dtDcDtCheckPrintType = dtDcDtCheckPrintType;
                    PrintReportDieuChinhDuToanViewModel.IsShowInTheoTongHop = true;
                    //PrintReportDieuChinhDuToanViewModel. = false;
                    PrintReportDieuChinhDuToanViewModel.Name = "In điều chỉnh dự toán";
                    PrintReportDieuChinhDuToanViewModel.Description = "In điều chỉnh dự toán";
                    PrintReportDieuChinhDuToanViewModel.Init();

                    content = new PrintReportDieuChinhDuToanChiTiet
                    {
                        DataContext = PrintReportDieuChinhDuToanViewModel
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

        #region Inport data
        private void OnImportData()
        {
            try
            {
                ImportDieuChinhDuToanBHXHViewModel.Init();
                ImportDieuChinhDuToanBHXHViewModel.SavedAction = obj =>
                {
                    _ImportDieuChinhDuToanBHXH.Close();
                    this.LoadData();
                    OnPropertyChanged(nameof(IsCensorship));
                    this.OnRefresh();
                    IsAllItemsSelected = false;
                    OpenDetailDialog((BhDtcDcdToanChiModel)obj);
                };
                _ImportDieuChinhDuToanBHXH = new ImportDieuChinhDuToanBHXH { DataContext = ImportDieuChinhDuToanBHXHViewModel };
                _ImportDieuChinhDuToanBHXH.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Export data
        private void OnExportData()
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
                    int iNamLamViec = _sessionService.Current.YearOfWork;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, iNamLamViec).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<BhDtcDcdToanChiModel> khcKcbModelsSummary = Items.Where(x => x.IsSelected).ToList();
                    var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                    predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == iNamLamViec);
                    List<string> lstSLNS = new List<string>();

                    foreach (var item in khcKcbModelsSummary)
                    {
                        var currentDonVi = GetNsDonViOfCurrentUser();
                        lstSLNS = new List<string>();
                        lstSLNS = item.SLNS.Split(",").ToList();
                        var listMucLucNganSach = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).OrderBy(x => x.SXauNoiMa).ToList();

                        //var lstLNS = item.SLNS.Split(',');
                        listMucLucNganSach = listMucLucNganSach.Where(x => lstSLNS.Contains(x.SLNS)).ToList();

                        DonVi donViChild = _nsDonViService.FindByIdDonVi(item.IID_MaDonVi, iNamLamViec);
                        BhDtcDcdToanChiChiTietCriteria searchCondition = new BhDtcDcdToanChiChiTietCriteria();
                        searchCondition.NamLamViec = iNamLamViec;
                        searchCondition.IdDonVi = item.IID_MaDonVi;
                        searchCondition.DtcDcdToanChiId = item.IID_BH_DTC;
                        searchCondition.ILoaiDanhMucChi = item.IID_LoaiCap;
                        searchCondition.LNS = item.SLNS;
                        searchCondition.ILoaiTongHop = item.ILoaiTongHop;
                        searchCondition.MaLoaiChi = item.SMaLoaiChi;
                        searchCondition.NgayChungTu = item.DNgayChungTu;

                        var lstDieuChinh = _bhDtcDcdToanChiChiTietService.FindByConditionForChildUnit(searchCondition);
                        var LstChungTu = _mapper.Map<ObservableCollection<BhDtcDcdToanChiChiTietModel>>(lstDieuChinh);
                        var lstDuToan = LstChungTu.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi) || string.IsNullOrEmpty(x.SM)).ToList();

                        if (item.SLNS == LNSValue.LNS_9010001_9010002 || item.SLNS == LNSValue.LNS_901_9010001_9010002)
                        {
                            lstDuToan.ForEach(x =>
                            {
                                if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                                {
                                    x.BHangCha = false;
                                    x.IsHangCha = false;
                                }
                            });

                            CalculateDataDuToan(lstDuToan);
                        }

                        lstDuToan.ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                            {
                                x.BHangCha = true;
                                x.IsHangCha = true;
                            }
                        });

                        var listdtdieuchinhMucLucsOrder = _mapper.Map<ObservableCollection<BhDtcDcdToanChiChiTietModel>>(LstChungTu).ToList();

                        CalculateData(listdtdieuchinhMucLucsOrder);

                        listdtdieuchinhMucLucsOrder.ForEach(x =>
                        {
                            x.FTienUocThucHienCaNam = x.FTienThucHien06ThangDauNam + x.FTienUocThucHien06ThangCuoiNam;
                            if (item.SLNS == LNSValue.LNS_9010003)
                            {
                                if (x.SXauNoiMa == LNSValue.LNS_9010003)
                                {
                                    x.FTienSoSanhTang = (!string.IsNullOrEmpty(x.SDuToanChiTietToi) || string.IsNullOrEmpty(x.SM)) ? ((x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao) > 0 ? x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao : 0) : 0;
                                    x.FTienSoSanhGiam = (!string.IsNullOrEmpty(x.SDuToanChiTietToi) || string.IsNullOrEmpty(x.SM)) ? ((x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam) > 0 ? x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam : 0) : 0;
                                }
                                else
                                {
                                    x.FTienSoSanhTang = 0;
                                    x.FTienSoSanhGiam = 0;
                                }
                            }

                            else
                            {
                                x.FTienSoSanhTang = (!string.IsNullOrEmpty(x.SDuToanChiTietToi) || string.IsNullOrEmpty(x.SM)) ? ((x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao) > 0 ? x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao : 0) : 0;
                                x.FTienSoSanhGiam = (!string.IsNullOrEmpty(x.SDuToanChiTietToi) || string.IsNullOrEmpty(x.SM)) ? ((x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam) > 0 ? x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam : 0) : 0;
                            }

                        });
                        listdtdieuchinhMucLucsOrder = listdtdieuchinhMucLucsOrder.Where(x => x.IsDataNotNull).ToList();
                        Dictionary<string, object> Data = new Dictionary<string, object>();

                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        Data.Add("FormatNumber", formatNumber);
                        Data.Add("SNguoiTao", item.SNguoiTao);
                        Data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                        Data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                        Data.Add("TitleFirst", $"ĐIỀU CHỈNH DỰ TOÁN NGÂN SÁCH NĂM {_sessionService.Current.YearOfWork}");
                        Data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoQuyetDinh}, ngày: {DateUtils.Format(item.DNgayQuyetDinh)})");
                        Data.Add("HeaderTenDonVi", $"Đơn vị: {donViChild?.IIDMaDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{donViChild?.TenDonVi}");
                        Data.Add("DonVi", _sessionService.Current.TenDonVi);
                        Data.Add("YearWork", iNamLamViec);
                        Data.Add("YearWorkOld", iNamLamViec - 1);
                        Data.Add("H2", "Lữ đoàn X");
                        Data.Add("H1", "Lữ đoàn X");
                        Data.Add("ListData", listdtdieuchinhMucLucsOrder);
                        Data.Add("SKTML", listMucLucNganSach);

                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_DT_DCDT, ExportFileName.RPT_BH_DT_DCDT_CHUNGTU_CHITIET_BHXH);
                        fileNamePrefix = string.Format("{0}_{1}_{2}", item.SSoChungTu, item.SSoQuyetDinh, StringUtils.ConvertVN(donViChild?.TenDonVi));
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhDtcDcdToanChiModel, BhDmMucLucNganSach, BhDtcDcdToanChiChiTietModel>(templateFileName, Data);
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

        private void CalculateData(List<BhDtcDcdToanChiChiTietModel> listkhcMucLucsOrders)
        {
            listkhcMucLucsOrders.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    //x.FTienDuToanDuocGiao = 0;
                    x.FTienThucHien06ThangDauNam = 0;
                    x.FTienUocThucHien06ThangCuoiNam = 0;
                    x.FTienUocThucHienCaNam = 0;
                    x.FTienSoSanhTang = 0;
                    x.FTienSoSanhGiam = 0;
                });

            var temp = listkhcMucLucsOrders.Where(x => !x.IsHangCha).ToList();
            var dictByMlns = listkhcMucLucsOrders.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }
        }

        private void CalculateDataDuToan(List<BhDtcDcdToanChiChiTietModel> listkhcMucLucsOrders)
        {
            listkhcMucLucsOrders.Where(x => x.IsHangCha)
               .ForAll(x =>
               {
                   x.FTienDuToanDuocGiao = 0;
               });

            var temp = listkhcMucLucsOrders.Where(x => !x.IsHangCha).ToList();
            var dictByMlns = listkhcMucLucsOrders.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParentDuToan(item.IdParent, item, dictByMlns);
            }
        }

        private void CalculateParentDuToan(Guid idParent, BhDtcDcdToanChiChiTietModel item, Dictionary<Guid, BhDtcDcdToanChiChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            model.FTienDuToanDuocGiao += item.FTienDuToanDuocGiao.GetValueOrDefault(0);

            CalculateParentDuToan(model.IdParent, item, dictByMlns);
        }


        private void CalculateParent(Guid idParent, BhDtcDcdToanChiChiTietModel item, Dictionary<Guid, BhDtcDcdToanChiChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            //model.FTienDuToanDuocGiao += item.FTienDuToanDuocGiao.GetValueOrDefault(0);
            model.FTienThucHien06ThangDauNam += item.FTienThucHien06ThangDauNam.GetValueOrDefault(0);
            model.FTienUocThucHien06ThangCuoiNam += item.FTienUocThucHien06ThangCuoiNam.GetValueOrDefault(0);
            model.FTienUocThucHienCaNam += item.FTienUocThucHienCaNam.GetValueOrDefault(0);
            model.FTienSoSanhTang += item.FTienSoSanhTang.GetValueOrDefault(0);
            model.FTienSoSanhGiam += item.FTienSoSanhGiam.GetValueOrDefault(0);

            CalculateParent(model.IdParent, item, dictByMlns);
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
                    DieuChinhDuToanBHXHDialogViewModel.IsDetail = true;
                    DieuChinhDuToanBHXHDialogViewModel.IsSummary = false;
                    DieuChinhDuToanBHXHDialogViewModel.IsAgregate = false;
                    DieuChinhDuToanBHXHDialogViewModel.Model = SelectedItem;
                    DieuChinhDuToanBHXHDialogViewModel.BhDtcDcdToanChiModel = SelectedItem;
                    DieuChinhDuToanBHXHDialogViewModel.Init();
                    DieuChinhDuToanBHXHDialogViewModel.SavedAction = obj => this.OnRefresh();
                    DieuChinhDuToanBHXHDialogViewModel.ShowDialogHost();
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
            List<BhDtcDcdToanChiModel> selectdtcDcdToanChi = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedItem.STongHop) && SelectedItem.STongHop.Contains(x.SSoChungTu)).ToList();
            DieuChinhDuToanBHXHDialogViewModel.IsAgregate = true;
            DieuChinhDuToanBHXHDialogViewModel.IsSummary = true;
            DieuChinhDuToanBHXHDialogViewModel.IsDetail = false;
            DieuChinhDuToanBHXHDialogViewModel.ListIdsBhDtcDcdToanChiModel = selectdtcDcdToanChi;
            DieuChinhDuToanBHXHDialogViewModel.Model = SelectedItem;
            DieuChinhDuToanBHXHDialogViewModel.Init();
            DieuChinhDuToanBHXHDialogViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhDtcDcdToanChiModel)obj);
            };

            DieuChinhDuToanBHXHDialogViewModel.ShowDialogHost();
            //var addView = new DieuChinhDuToanBHXHDialog() { DataContext = DieuChinhDuToanBHXHDialogViewModel };
            //DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhDtcDcdToanChiModel)eventArgs.Parameter);
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
                    var entity = _bhDcdToanChiService.FindById(SelectedItem.IID_BH_DTC);

                    if (entity != null && !string.IsNullOrEmpty(entity.SNguoiTao) && !entity.SNguoiTao.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {

                        MessageBox.Show(string.Format(Resources.VoucherDeleteKHTHWarning, entity.SNguoiTao), Resources.Alert);
                        return;
                    }
                }

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, !(SelectedItem.DNgayChungTu == DateTime.MinValue) ? DateTimeExtension.ToStringDate(SelectedItem.DNgayChungTu) : string.Empty);
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
                    _bhDcdToanChiService.Delete(SelectedItem.IID_BH_DTC);

                    if (string.IsNullOrEmpty(SelectedItem.STongHop))
                    {
                        var lstKeHoachChiTiet = _bhDtcDcdToanChiChiTietService.FindByIdChiTiet(SelectedItem.IID_BH_DTC).ToList();
                        if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                        {
                            foreach (var item in lstKeHoachChiTiet)
                            {
                                _bhDtcDcdToanChiChiTietService.Delete(item.Id);
                            }
                        }
                    }
                    else
                    {
                        var lstSoCtChild = SelectedItem.STongHop.Split(",");
                        foreach (var soct in lstSoCtChild)
                        {
                            var ctChild = _bhDcdToanChiService.FindByCondition(x => x.SSoChungTu.Equals(soct)
                                    && x.INamLamViec == _sessionService.Current.YearOfWork).FirstOrDefault();
                            if (ctChild != null)
                            {
                                ctChild.ILoaiTongHop = KhcBhxhLoaiChungTu.BhxhChungTu;
                                ctChild.BDaTongHop = false;
                                _bhDcdToanChiService.Update(ctChild);
                            }
                        }

                        var lstKeHoachChiTiet = _bhDtcDcdToanChiChiTietService.FindByIdChiTiet(SelectedItem.IID_BH_DTC).ToList();
                        if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                        {
                            foreach (var item in lstKeHoachChiTiet)
                            {
                                _bhDtcDcdToanChiChiTietService.Delete(item.Id);
                            }
                        }
                    }
                }

                var itemDeleted = Items.Where(x => x.IID_BH_DTC == SelectedItem.IID_BH_DTC).First();
                Items.Remove(itemDeleted);
                this.LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region DoubleClick
        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhDtcDcdToanChiModel)obj);
        }
        #endregion
    }
}
