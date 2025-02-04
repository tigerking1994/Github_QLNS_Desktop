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
using System.Windows.Markup;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu.Import;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu
{
    public class DieuChinhDuToanThuIndexViewModel : GridViewModelBase<BhDcDuToanThuModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IBhDcDuToanThuService _bhDcDuToanThuService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly IBhDcDuToanThuChiTietService _bhDcDuToanThuChiTietService;
        private readonly ILog _logger;
        private readonly IExportService _exportService;
        private SessionInfo _sessionInfo;
        private ICollectionView _bhDieuChinhDuToanView;
        private ICollectionView _nsDonViModelsView;

        public override string GroupName => MenuItemContants.GROUP_THU;
        public override string Name => "Điều chỉnh DT thu BHXH, BHYT, BHTN";
        public override string Description => "Điều chỉnh DT thu BHXH, BHYT, BHTN " + _sessionService.Current.YearOfWork;
        public override Type ContentType => typeof(DieuChinhDuToanThuIndex);
        public bool IsDelete => SelectedItem != null && !SelectedItem.BIsKhoa;
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
        public bool IsExportDataFilter => _selectedBhDcDuToanThuModel != null;

        private BhDcDuToanThuModel _selectedBhDcDuToanThuModel;
        public BhDcDuToanThuModel SelectedBhLapKeHoachThuModel
        {
            get => _selectedBhDcDuToanThuModel;
            set
            {
                SetProperty(ref _selectedBhDcDuToanThuModel, value);
                if (_selectedBhDcDuToanThuModel != null)
                {
                    IsLock = _selectedBhDcDuToanThuModel.BIsKhoa;
                }
                else
                {
                    IsEdit = false;
                }

                if (_selectedBhDcDuToanThuModel == null)
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsButtonEnable));
                OnPropertyChanged(nameof(IsExportAggregateData));
                OnPropertyChanged(nameof(IsExportDataFilter));
            }
        }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }
        private ObservableCollection<DonViModel> _BhDonViModelItems;
        public ObservableCollection<DonViModel> BhDonViModelItems
        {
            get => _BhDonViModelItems;
            set => SetProperty(ref _BhDonViModelItems, value);
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
                    if (Items != null && _voucherTypeSelected != null)
                    {
                        Items.Where(x => x.ILoaiTongHop.Equals(int.Parse(_voucherTypeSelected.ValueItem))).ForAll(c => c.IsSelected = value.Value);
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

        private List<BhDcDuToanThuModel> _lstChungTuOrigin;
        public List<BhDcDuToanThuModel> LstChungTuOrigin
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
            get => _isEdit = TabIndex == VoucherTabIndex.VOUCHER ? SelectedItem != null && !SelectedItem.BIsKhoa : false;
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

        private void SelectAll(bool select, ObservableCollection<BhDcDuToanThuModel> models)
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

        private ComboboxItem _selectedProjectType;
        public ComboboxItem SelectedProjectType
        {
            get => _selectedProjectType;
            set
            {
                SetProperty(ref _selectedProjectType, value);
                _bhDieuChinhDuToanView?.Refresh();
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
        public RelayCommand RefreshCommand { get; }
        public RelayCommand PrintCommand { get; }
        #endregion RelayCommand

        public DieuChinhDuToanThuDialogViewModel DieuChinhDuToanThuDialogViewModel { get; set; }
        public DieuChinhDuToanThuDetailViewModel DieuChinhDuToanThuDetailViewModel { get; set; }
        public DieuChinhDuToanThuImportViewModel DieuChinhDuToanThuImportViewModel { get; set; }
        public PrintReportDieuChinhDuToanThuViewModel PrintReportDieuChinhDuToanThuViewModel { get; }
        public DieuChinhDuToanThuIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IBhDcDuToanThuService bhDcDuToanChiService,
            IExportService exportService,
            IBhDcDuToanThuChiTietService bhDcDUToanChiChiTietService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            DieuChinhDuToanThuDialogViewModel dieuChinhDuToanThuDialogViewModel,
            DieuChinhDuToanThuDetailViewModel dieuChinhDuToanThuDetailViewModel,
            DieuChinhDuToanThuImportViewModel dieuChinhDuToanThuImportViewModel,
            PrintReportDieuChinhDuToanThuViewModel printReportDieuChinhDuToanThuViewModel)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _bhDcDuToanThuService = bhDcDuToanChiService;
            _bhDcDuToanThuChiTietService = bhDcDUToanChiChiTietService;

            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            SearchCommand = new RelayCommand(obj => SearchData());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintReportCommand = new RelayCommand(obj => OnOpenReport(obj));
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
            RefreshCommand = new RelayCommand(obj => Init());
            DieuChinhDuToanThuDialogViewModel = dieuChinhDuToanThuDialogViewModel;
            DieuChinhDuToanThuDetailViewModel = dieuChinhDuToanThuDetailViewModel;
            DieuChinhDuToanThuImportViewModel = dieuChinhDuToanThuImportViewModel;
            PrintReportDieuChinhDuToanThuViewModel = printReportDieuChinhDuToanThuViewModel;

            PrintCommand = new RelayCommand(OnPrint);
        }

        public override void Init()
        {
            try
            {
                _tabIndex = VoucherTabIndex.VOUCHER;
                _sessionInfo = _sessionService.Current;
                LoadLockStatus();
                OnResetFilter();
                LoadData();
                LoadDonVi();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
                DieuChinhDuToanThuDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                var listChungTu = _bhDcDuToanThuService.FindByYearOfWord(_sessionService.Current.YearOfWork);
                _lstChungTuOrigin = _mapper.Map<List<BhDcDuToanThuModel>>(listChungTu);
                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == VoucherTabIndex.VOUCHER)
                    {
                        Items = _mapper.Map<ObservableCollection<BhDcDuToanThuModel>>(_lstChungTuOrigin.Where(x => !x.IIDMaDonVi.Equals(_sessionService.Current.IdDonVi) && x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTu && !x.BDaTongHop.GetValueOrDefault()));
                    }
                    else
                    {
                        var listCTTongHop = listChungTu.Where(x => x.IIDMaDonVi.Equals(_sessionService.Current.IdDonVi) && x.ILoaiTongHop == KhcKcbBhxhLoaiChungTu.BhxhChungTuTongHop).ToList();
                        var listTongHop = new List<BhDcDuToanThuModel>();
                        foreach (var ctTongHop in listCTTongHop)
                        {
                            var parent = _mapper.Map<BhDcDuToanThuModel>(ctTongHop);
                            parent.IsExpand = true;
                            listTongHop.Add(parent);
                            if (!string.IsNullOrEmpty(ctTongHop.STongHop))
                            {
                                var listChild = _mapper.Map<List<BhDcDuToanThuModel>>(listChungTu.Where(x => ctTongHop.STongHop != null && ctTongHop.STongHop.Contains(x.SSoChungTu)));
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                                listTongHop.AddRange(listChild);
                            }
                        }
                        Items = _mapper.Map<ObservableCollection<BhDcDuToanThuModel>>(listTongHop);
                    }
                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<BhDcDuToanThuModel>>(listChungTu);
                }

                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhDcDuToanThuModel.IsSelected))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsExportDataFilter));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                            OnPropertyChanged(nameof(IsLock));
                        }
                        if (args.PropertyName == nameof(BhDcDuToanThuModel.IsCollapse))
                        {
                            ExpandChild();
                        }
                    };
                }

                _bhDieuChinhDuToanView = CollectionViewSource.GetDefaultView(Items);
                _bhDieuChinhDuToanView.Filter = dtChungTuModelsFilter;
                LoadDonVi();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd()
        {
            try
            {
                DieuChinhDuToanThuDialogViewModel.Model = new BhDcDuToanThuModel();
                DieuChinhDuToanThuDialogViewModel.BhDcDuToanThuModel = new BhDcDuToanThuModel();
                DieuChinhDuToanThuDialogViewModel.IsDetail = true;
                DieuChinhDuToanThuDialogViewModel.IsAgregate = false;
                DieuChinhDuToanThuDialogViewModel.IsSummary = false;
                DieuChinhDuToanThuDialogViewModel.Init();
                DieuChinhDuToanThuDialogViewModel.SavedAction = obj =>
                {
                    var khcChungTu = (BhDcDuToanThuModel)obj;
                    this.LoadData();
                    if (khcChungTu != null)
                    {
                        OpenDetailDialog(khcChungTu);
                    }
                };
                var exportView = new DieuChinhDuToanThuDialog() { DataContext = DieuChinhDuToanThuDialogViewModel };
                DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OpenDetailDialog(BhDcDuToanThuModel SelectedItem)
        {
            var idDonViCurrent = _sessionInfo.IdDonVi;
            DieuChinhDuToanThuDetailViewModel.Model = ObjectCopier.Clone(SelectedItem);
            DieuChinhDuToanThuDetailViewModel.IsVoucherSummary = SelectedItem.IIDMaDonVi.Equals(idDonViCurrent) && !string.IsNullOrEmpty(SelectedItem.STongHop);
            DieuChinhDuToanThuDetailViewModel.Init();
            var view = new DieuChinhDuToanThuDetail() { DataContext = DieuChinhDuToanThuDetailViewModel };
            view.ShowDialog();
        }

        private void ExpandChild()
        {
            if (SelectedItem != null)
            {
                Items.Where(n => n.SoChungTuParent == SelectedItem.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
            OnPropertyChanged(nameof(Items));
        }

        private bool dtChungTuModelsFilter(object obj)
        {
            if (!(obj is BhDcDuToanThuModel temp)) return true;
            var keyword = SearchText?.Trim().ToLower() ?? string.Empty.Trim().ToLower();
            var condition1 = false;
            var condition2 = true;
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(temp.SSoChungTu))
                    condition1 = condition1 || temp.SSoChungTu.ToLower().Contains(keyword);
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
                condition2 = condition2 && temp.IIDMaDonVi == SelectedNsDonViModel.IIDMaDonVi;
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
            return result;
        }

        private void LoadDonVi()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            if (Items != null && Items.Count > 0)
            {
                var idDonVis = Items.Select(x => x.IIDMaDonVi).ToList();
                predicate = predicate.And(x => idDonVis.Any(y => y == x.IIDMaDonVi));
                var listUnit = _nsDonViService.FindByCondition(predicate).ToList();
                BhDonViModelItems = new ObservableCollection<DonViModel>();
                BhDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
                _nsDonViModelsView = CollectionViewSource.GetDefaultView(BhDonViModelItems);
                _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                    ListSortDirection.Ascending));
                _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.TenDonVi),
                    ListSortDirection.Ascending));
            }
        }

        private void ConfirmAggregate()
        {
            List<BhDcDuToanThuModel> selectedBhKhcKcbChungtu = Items.Where(x => x.IsSelected).ToList();
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

            List<BhDcDuToanThuModel> BhDcDuToanThuModels = Items.Where(x => x.IsSelected && x.BIsKhoa).ToList();
            DieuChinhDuToanThuDialogViewModel.IsSummary = true;
            DieuChinhDuToanThuDialogViewModel.IsDetail = false;
            DieuChinhDuToanThuDialogViewModel.ListIdsBhDcDuToanThuModel = BhDcDuToanThuModels;
            DieuChinhDuToanThuDialogViewModel.Model = new BhDcDuToanThuModel();
            DieuChinhDuToanThuDialogViewModel.IsAgregate = true;
            DieuChinhDuToanThuDialogViewModel.Init();
            DieuChinhDuToanThuDialogViewModel.SavedAction = obj =>
            {
                TabIndex = VoucherTabIndex.VOUCHER;
                this.OnRefresh();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhDcDuToanThuModel)obj);
            };

            var view = new DieuChinhDuToanThuDialog
            {
                DataContext = DieuChinhDuToanThuDialogViewModel
            };
            DialogHost.Show(view, SystemConstants.ROOT_DIALOG);
        }

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
                    if (SelectedItem.SNguoiTao != _sessionService.Current.Principal)
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
                DateTime dtNow = DateTime.Now;
                var lstSelected = Items.Where(x => x.IsSelected).ToList();
                var isLock = !lstSelected.FirstOrDefault().BIsKhoa;
                foreach (var ct in lstSelected)
                {
                    _bhDcDuToanThuService.LockOrUnlock(ct.IIDDttDieuChinh, isLock);
                    var dttBhxh = Items.First(x => x.Id == ct.Id);
                    dttBhxh.BIsKhoa = !ct.BIsKhoa;
                }
                LoadData();
                OnPropertyChanged(nameof(IsLock));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnOpenReport(object obj)
        {

        }

        private void OnImportData()
        {
            DieuChinhDuToanThuImportViewModel.Init();
            DieuChinhDuToanThuImportViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhDcDuToanThuModel)obj);
            };
            DieuChinhDuToanThuImportViewModel.ShowDialog();
        }

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

                    List<BhDcDuToanThuModel> khcKcbModelsSummary = Items.Where(x => x.IsSelected).ToList();

                    var yearOfWork = _sessionService.Current.YearOfWork;

                    foreach (var item in khcKcbModelsSummary)
                    {
                        var currentDonVi = GetNsDonViOfCurrentUser();

                        BhDcDuToanThuChiTietCriteria searchCondition = new BhDcDuToanThuChiTietCriteria();
                        searchCondition.NamLamViec = _sessionService.Current.YearOfWork;
                        searchCondition.IdDonVi = item.IIDMaDonVi;
                        searchCondition.BhDcDuToanThuId = item.IIDDttDieuChinh;
                        searchCondition.LNS = item.SLNS;
                        searchCondition.ILoaiTongHop = item.ILoaiTongHop;
                        searchCondition.IIDTongHopID = item.IIDTongHopID;
                        var dtdieuchinhMucLucsOrder = _bhDcDuToanThuChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
                        var listdtdieuchinhMucLucsOrder = _mapper.Map<ObservableCollection<BhDcDuToanThuChiTietModel>>(dtdieuchinhMucLucsOrder).ToList();
                        CalculateData(listdtdieuchinhMucLucsOrder);
                        var lstExportData = _mapper.Map<ObservableCollection<BhDcDuToanThuChiTietQuery>>(listdtdieuchinhMucLucsOrder).ToList();
                        foreach (var row in lstExportData)
                        {
                            row.FThuBHXHNLD = Math.Round(row.FThuBHXHNLD.GetValueOrDefault());
                            row.FThuBHXHNSD = Math.Round(row.FThuBHXHNSD.GetValueOrDefault());
                            row.FThuBHYTNLD = Math.Round(row.FThuBHYTNLD.GetValueOrDefault());
                            row.FThuBHYTNSD = Math.Round(row.FThuBHYTNSD.GetValueOrDefault());
                            row.FThuBHTNNLD = Math.Round(row.FThuBHTNNLD.GetValueOrDefault());
                            row.FThuBHTNNSD = Math.Round(row.FThuBHTNNSD.GetValueOrDefault());
                            row.FThuBHXHNLDQTDauNam = Math.Round(row.FThuBHXHNLDQTDauNam.GetValueOrDefault());
                            row.FThuBHXHNSDQTDauNam = Math.Round(row.FThuBHXHNSDQTDauNam.GetValueOrDefault());
                            row.FThuBHYTNLDQTDauNam = Math.Round(row.FThuBHYTNLDQTDauNam.GetValueOrDefault());
                            row.FThuBHYTNSDQTDauNam = Math.Round(row.FThuBHYTNSDQTDauNam.GetValueOrDefault());
                            row.FThuBHTNNLDQTDauNam = Math.Round(row.FThuBHTNNLDQTDauNam.GetValueOrDefault());
                            row.FThuBHTNNSDQTDauNam = Math.Round(row.FThuBHTNNSDQTDauNam.GetValueOrDefault());
                            row.FThuBHXHNLDQTCuoiNam = Math.Round(row.FThuBHXHNLDQTCuoiNam.GetValueOrDefault());
                            row.FThuBHXHNSDQTCuoiNam = Math.Round(row.FThuBHXHNSDQTCuoiNam.GetValueOrDefault());
                            row.FThuBHYTNLDQTCuoiNam = Math.Round(row.FThuBHYTNLDQTCuoiNam.GetValueOrDefault());
                            row.FThuBHYTNSDQTCuoiNam = Math.Round(row.FThuBHYTNSDQTCuoiNam.GetValueOrDefault());
                            row.FThuBHTNNLDQTCuoiNam = Math.Round(row.FThuBHTNNLDQTCuoiNam.GetValueOrDefault());
                            row.FThuBHTNNSDQTCuoiNam = Math.Round(row.FThuBHTNNSDQTCuoiNam.GetValueOrDefault());
                            row.FThuBHXHNLDTang= Math.Round(row.FThuBHXHNLDTang.GetValueOrDefault());
                            row.FThuBHXHNSDTang= Math.Round(row.FThuBHXHNSDTang.GetValueOrDefault());
                            row.FThuBHXHNLDGiam= Math.Round(row.FThuBHXHNLDGiam.GetValueOrDefault());
                            row.FThuBHXHNSDGiam= Math.Round(row.FThuBHXHNSDGiam.GetValueOrDefault());
                            row.FThuBHYTNLDTang= Math.Round(row.FThuBHYTNLDTang.GetValueOrDefault());
                            row.FThuBHYTNSDTang= Math.Round(row.FThuBHYTNSDTang.GetValueOrDefault());
                            row.FThuBHYTNLDGiam= Math.Round(row.FThuBHYTNLDGiam.GetValueOrDefault());
                            row.FThuBHYTNSDGiam= Math.Round(row.FThuBHYTNSDGiam.GetValueOrDefault());
                            row.FThuBHTNNLDTang= Math.Round(row.FThuBHTNNLDTang.GetValueOrDefault());
                            row.FThuBHTNNSDTang= Math.Round(row.FThuBHTNNSDTang.GetValueOrDefault());
                            row.FThuBHTNNLDGiam= Math.Round(row.FThuBHTNNLDGiam.GetValueOrDefault());
                            row.FThuBHTNNSDGiam = Math.Round(row.FThuBHTNNSDGiam.GetValueOrDefault());
                        }
                        Dictionary<string, object> Data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        Data.Add("TitleFirst", $"ĐIỀU CHỈNH DỰ TOÁN THU BHXH, BHYT, BHTN NĂM {_sessionService.Current.YearOfWork}");
                        Data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoChungTu}, ngày: {DateUtils.Format(item.DNgayChungTu)})");
                        Data.Add("FormatNumber", formatNumber);
                        Data.Add("SNguoiTao", item.SNguoiTao);
                        Data.Add("Cap1", currentDonVi.TenDonVi);
                        Data.Add("DonVi", _sessionService.Current.TenDonVi);
                        Data.Add("YearWork", yearOfWork);
                        Data.Add("YearWorkOld", yearOfWork - 1);
                        Data.Add("H2", "Lữ đoàn X");
                        Data.Add("H1", "Lữ đoàn X");
                        Data.Add("Items", lstExportData);
                        Data.Add("MLNS", listdtdieuchinhMucLucsOrder);

                        Data.Add("TotalFThuBHXHNLD", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNLD.GetValueOrDefault()));
                        Data.Add("TotalFThuBHXHNSD", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNSD.GetValueOrDefault()));
                        Data.Add("TotalFThuBHYTNLD", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNLD.GetValueOrDefault()));
                        Data.Add("TotalFThuBHYTNSD", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNSD.GetValueOrDefault()));
                        Data.Add("TotalFThuBHTNNLD", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNLD.GetValueOrDefault()));
                        Data.Add("TotalFThuBHTNNSD", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNSD.GetValueOrDefault()));

                        Data.Add("TotalFThuBHXHNLDQTDauNam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNLDQTDauNam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHXHNSDQTDauNam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNSDQTDauNam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHYTNLDQTDauNam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNLDQTDauNam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHYTNSDQTDauNam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNSDQTDauNam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHTNNLDQTDauNam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNLDQTDauNam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHTNNSDQTDauNam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNSDQTDauNam.GetValueOrDefault()));

                        Data.Add("TotalFThuBHXHNLDQTCuoiNam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNLDQTCuoiNam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHXHNSDQTCuoiNam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNSDQTCuoiNam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHYTNLDQTCuoiNam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNLDQTCuoiNam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHYTNSDQTCuoiNam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNSDQTCuoiNam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHTNNLDQTCuoiNam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNLDQTCuoiNam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHTNNSDQTCuoiNam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNSDQTCuoiNam.GetValueOrDefault()));
                        
                        Data.Add("TotalFThuBHXHNLDTang", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNLDTang.GetValueOrDefault()));
                        Data.Add("TotalFThuBHXHNSDTang", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNSDTang.GetValueOrDefault()));
                        Data.Add("TotalFThuBHXHNLDGiam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNLDGiam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHXHNSDGiam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNSDGiam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHYTNLDTang", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNLDTang.GetValueOrDefault()));
                        Data.Add("TotalFThuBHYTNSDTang", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNSDTang.GetValueOrDefault()));
                        Data.Add("TotalFThuBHYTNLDGiam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNLDGiam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHYTNSDGiam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNSDGiam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHTNNLDTang", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNLDTang.GetValueOrDefault()));
                        Data.Add("TotalFThuBHTNNSDTang", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNSDTang.GetValueOrDefault()));
                        Data.Add("TotalFThuBHTNNLDGiam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNLDGiam.GetValueOrDefault()));
                        Data.Add("TotalFThuBHTNNSDGiam", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNSDGiam.GetValueOrDefault()));


                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_DC_DTT, ExportFileName.RPT_BH_DC_DTT_CHUNG_TU_CHITIET);
                        fileNamePrefix = item.SSoChungTu;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhDcDuToanThuChiTietQuery, BhDmMucLucNganSach, BhDcDuToanThuChiTietModel>(templateFileName, Data);
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

        private void CalculateData(List<BhDcDuToanThuChiTietModel> listDttMucLucsOrders)
        {
            listDttMucLucsOrders.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.FThuBHXHNLD = 0;
                    x.FThuBHXHNSD = 0;
                    x.FThuBHYTNLD = 0;
                    x.FThuBHYTNSD = 0;
                    x.FThuBHTNNLD = 0;
                    x.FThuBHTNNSD = 0;
                    x.FThuBHXHNLDQTDauNam = 0;
                    x.FThuBHXHNSDQTDauNam = 0;
                    x.FThuBHYTNLDQTDauNam = 0;
                    x.FThuBHYTNSDQTDauNam = 0;
                    x.FThuBHTNNLDQTDauNam = 0;
                    x.FThuBHTNNSDQTDauNam = 0;
                    x.FThuBHXHNLDQTCuoiNam = 0;
                    x.FThuBHXHNSDQTCuoiNam = 0;
                    x.FThuBHYTNLDQTCuoiNam = 0;
                    x.FThuBHYTNSDQTCuoiNam = 0;
                    x.FThuBHTNNLDQTCuoiNam = 0;
                    x.FThuBHTNNSDQTCuoiNam = 0;
                    x.FThuBHXHNLDTang = 0;
                    x.FThuBHXHNSDTang = 0;
                    x.FThuBHXHTang = 0;
                    x.FThuBHYTNLDTang = 0;
                    x.FThuBHYTNSDTang = 0;
                    x.FThuBHYTTang = 0;
                    x.FThuBHTNNLDTang = 0;
                    x.FThuBHTNNSDTang = 0;
                    x.FThuBHTNTang = 0;
                });

            listDttMucLucsOrders.Where(x => !x.IsHangCha)
              .ForAll(x =>
              {
                  x.FThuBHXHNLDTang = (x.FTongThuBHXHNLD > 0 && x.FTongThuBHXHNLD > x.FThuBHXHNLD) ? x.FTongThuBHXHNLD - x.FThuBHXHNLD : 0;
                  x.FThuBHXHNSDTang = (x.FTongThuBHXHNSD > 0 && x.FTongThuBHXHNSD > x.FThuBHXHNSD) ? x.FTongThuBHXHNSD - x.FThuBHXHNSD : 0;
                  x.FThuBHYTNLDTang = (x.FTongThuBHYTNLD > 0 && x.FTongThuBHYTNLD > x.FThuBHYTNLD) ? x.FTongThuBHYTNLD - x.FThuBHYTNLD : 0;
                  x.FThuBHYTNSDTang = (x.FTongThuBHYTNSD > 0 && x.FTongThuBHYTNSD > x.FThuBHYTNSD) ? x.FTongThuBHYTNSD - x.FThuBHYTNSD : 0;
                  x.FThuBHTNNLDTang = (x.FTongThuBHTNNLD > 0 && x.FTongThuBHTNNLD > x.FThuBHTNNLD) ? x.FTongThuBHTNNLD - x.FThuBHTNNLD : 0;
                  x.FThuBHTNNSDTang = (x.FTongThuBHTNNSD > 0 && x.FTongThuBHTNNSD > x.FThuBHTNNSD) ? x.FTongThuBHTNNSD - x.FThuBHTNNSD : 0;
              });

            var temp = listDttMucLucsOrders.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = listDttMucLucsOrders.GroupBy(x => x.IIDMLNS).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }
        }

        private void CalculateParent(Guid idParent, BhDcDuToanThuChiTietModel item, Dictionary<Guid, BhDcDuToanThuChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            model.FThuBHXHNLD += item.FThuBHXHNLD.GetValueOrDefault();
            model.FThuBHXHNSD += item.FThuBHXHNSD.GetValueOrDefault();
            model.FThuBHYTNLD += item.FThuBHYTNLD.GetValueOrDefault();
            model.FThuBHYTNSD += item.FThuBHYTNSD.GetValueOrDefault();
            model.FThuBHTNNLD += item.FThuBHTNNLD.GetValueOrDefault();
            model.FThuBHTNNSD += item.FThuBHTNNSD.GetValueOrDefault();

            model.FThuBHXHNLDQTDauNam += item.FThuBHXHNLDQTDauNam.GetValueOrDefault();
            model.FThuBHXHNSDQTDauNam += item.FThuBHXHNSDQTDauNam.GetValueOrDefault();
            model.FThuBHYTNLDQTDauNam += item.FThuBHYTNLDQTDauNam.GetValueOrDefault();
            model.FThuBHYTNSDQTDauNam += item.FThuBHYTNSDQTDauNam.GetValueOrDefault();
            model.FThuBHTNNLDQTDauNam += item.FThuBHTNNLDQTDauNam.GetValueOrDefault();
            model.FThuBHTNNSDQTDauNam += item.FThuBHTNNSDQTDauNam.GetValueOrDefault();
            model.FThuBHXHNLDQTCuoiNam += item.FThuBHXHNLDQTCuoiNam.GetValueOrDefault();
            model.FThuBHXHNSDQTCuoiNam += item.FThuBHXHNSDQTCuoiNam.GetValueOrDefault();
            model.FThuBHYTNLDQTCuoiNam += item.FThuBHYTNLDQTCuoiNam.GetValueOrDefault();
            model.FThuBHYTNSDQTCuoiNam += item.FThuBHYTNSDQTCuoiNam.GetValueOrDefault();
            model.FThuBHTNNLDQTCuoiNam += item.FThuBHTNNLDQTCuoiNam.GetValueOrDefault();
            model.FThuBHTNNSDQTCuoiNam += item.FThuBHTNNSDQTCuoiNam.GetValueOrDefault();

            model.FThuBHXHNLDTang += item.FThuBHXHNLDTang.GetValueOrDefault();
            model.FThuBHXHNSDTang += item.FThuBHXHNSDTang.GetValueOrDefault();
            model.FThuBHXHTang += item.FThuBHXHTang.GetValueOrDefault();
            model.FThuBHYTNLDTang += item.FThuBHYTNLDTang.GetValueOrDefault();
            model.FThuBHYTNSDTang += item.FThuBHYTNSDTang.GetValueOrDefault();
            model.FThuBHYTTang += item.FThuBHYTTang.GetValueOrDefault();
            model.FThuBHTNNLDTang += item.FThuBHTNNLDTang.GetValueOrDefault();
            model.FThuBHTNNSDTang += item.FThuBHTNNSDTang.GetValueOrDefault();
            model.FThuBHTNTang += item.FThuBHTNTang.GetValueOrDefault();
            model.FThuBHXHNLDGiam += item.FThuBHXHNLDGiam.GetValueOrDefault();
            model.FThuBHXHNSDGiam += item.FThuBHXHNSDGiam.GetValueOrDefault();
            model.FThuBHXHGiam += item.FThuBHXHGiam.GetValueOrDefault();
            model.FThuBHYTNLDGiam += item.FThuBHYTNLDGiam.GetValueOrDefault();
            model.FThuBHYTNSDGiam += item.FThuBHYTNSDGiam.GetValueOrDefault();
            model.FThuBHYTGiam += item.FThuBHYTGiam.GetValueOrDefault();
            model.FThuBHTNNLDGiam += item.FThuBHTNNLDGiam.GetValueOrDefault();
            model.FThuBHTNNSDGiam += item.FThuBHTNNSDGiam.GetValueOrDefault();
            model.FThuBHTNGiam += item.FThuBHTNGiam.GetValueOrDefault();

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

        private void OnResetFilter()
        {
            try
            {
                SearchSoKeHoachText = string.Empty;
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

        private void OnSelectedChange(object obj)
        {
            SelectedBhLapKeHoachThuModel = (BhDcDuToanThuModel)obj;
            if (SelectedBhLapKeHoachThuModel is { BIsKhoa: true } || SelectedBhLapKeHoachThuModel == null)
            {
                IsEdit = false;
            }
            else
            {
                IsEdit = true;
            }
        }

        protected override void OnUpdate()
        {
            try
            {
                if (SelectedItem.IIDMaDonVi.Equals(_sessionService.Current.IdDonVi))
                {
                    OnAggregateEdit();
                }
                else
                {
                    DieuChinhDuToanThuDialogViewModel.IsDetail = true;
                    DieuChinhDuToanThuDialogViewModel.IsSummary = false;
                    DieuChinhDuToanThuDialogViewModel.Model = SelectedItem;
                    DieuChinhDuToanThuDialogViewModel.Init();
                    DieuChinhDuToanThuDialogViewModel.SavedAction = obj => this.OnRefresh();
                    DieuChinhDuToanThuDialogViewModel.ShowDialogHost();
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
            List<BhDcDuToanThuModel> selectdtcDcdToanChi = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedItem.STongHop) && SelectedItem.STongHop.Contains(x.SSoChungTu)).ToList();
            DieuChinhDuToanThuDialogViewModel.IsAgregate = true;
            DieuChinhDuToanThuDialogViewModel.IsSummary = true;
            DieuChinhDuToanThuDialogViewModel.IsDetail = false;
            DieuChinhDuToanThuDialogViewModel.ListIdsBhDcDuToanThuModel = selectdtcDcdToanChi;
            DieuChinhDuToanThuDialogViewModel.Model = SelectedItem;
            DieuChinhDuToanThuDialogViewModel.Init();
            DieuChinhDuToanThuDialogViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhDcDuToanThuModel)obj);
            };
            var addView = new DieuChinhDuToanThuDialog() { DataContext = DieuChinhDuToanThuDialogViewModel };
            DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);

        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhDcDuToanThuModel)eventArgs.Parameter);
        }

        protected override void OnDelete()
        {
            try
            {
                if (SelectedItem != null && (SelectedItem.BIsKhoa)) return;
                if (SelectedItem != null)
                {
                    var entity = _bhDcDuToanThuService.FindById(SelectedItem.IIDDttDieuChinh);

                    if (entity != null && !string.IsNullOrEmpty(entity.SNguoiTao) && !entity.SNguoiTao.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {

                        MessageBox.Show(string.Format(Resources.VoucherDeleteKHTHWarning, entity.SNguoiTao), Resources.Alert);
                        return;
                    }
                }
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, SelectedItem.DNgayChungTu.HasValue ? SelectedItem.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty);
                MessageBoxResult result = MessageBoxHelper.Confirm(messageBuilder.ToString());
                if (result == MessageBoxResult.Yes)
                    DeleteEventHandler(SelectedItem);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DeleteEventHandler(BhDcDuToanThuModel model)
        {
            try
            {
                DateTime dtNow = DateTime.Now;
                if (model != null)
                {
                    _bhDcDuToanThuService.Delete(model.IIDDttDieuChinh);

                    if (string.IsNullOrEmpty(model.STongHop))
                    {
                        var lstKeHoachChiTiet = _bhDcDuToanThuChiTietService.FindByIdChiTiet(model.IIDDttDieuChinh).ToList();
                        if (lstKeHoachChiTiet.Any())
                        {
                            foreach (var item in lstKeHoachChiTiet)
                            {
                                _bhDcDuToanThuChiTietService.Delete(item.Id);
                            }
                        }
                    }
                    else
                    {
                        var lstSoCtChild = model.STongHop.Split(",");
                        foreach (var soct in lstSoCtChild)
                        {
                            var ctChild = _bhDcDuToanThuService.FindByCondition(x => x.SSoChungTu.Equals(soct)
                                    && x.INamLamViec == _sessionService.Current.YearOfWork).FirstOrDefault();
                            if (ctChild != null)
                            {
                                ctChild.ILoaiTongHop = KhcKcbBhxhLoaiChungTu.BhxhChungTu;
                                ctChild.BDaTongHop = false;
                                _bhDcDuToanThuService.Update(ctChild);
                                var lstKeHoachChiTiet = _bhDcDuToanThuChiTietService.FindByIdChiTiet(model.IIDDttDieuChinh).ToList();
                                if (lstKeHoachChiTiet.Any())
                                {
                                    foreach (var item in lstKeHoachChiTiet)
                                    {
                                        _bhDcDuToanThuChiTietService.Delete(item.Id);
                                    }
                                }
                            }
                        }
                    }
                }

                var itemDeleted = Items.Where(x => x.IIDDttDieuChinh == model.IIDDttDieuChinh).First();
                Items.Remove(itemDeleted);
                this.LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsDelete));
        }

        protected override void OnRefresh()
        {
            this.LoadData();
            OnPropertyChanged(nameof(Items));
        }
        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhDcDuToanThuModel)obj);
        }
        private void OnPrint(object param)
        {
            object content;
            PrintReportDieuChinhDuToanThuViewModel.IsEnabledUnit = true;
            PrintReportDieuChinhDuToanThuViewModel.IsPrintByUnit = true;

            var listDonViCheckBox = Items.Select(item => new CheckBoxItem
            {
                ValueItem = item.IIDMaDonVi,
                DisplayItem = string.Join("-", item.IIDMaDonVi, item.STenDonVi),
                NameItem = item.STenDonVi
            }).OrderBy(item => item.ValueItem);

            PrintReportDieuChinhDuToanThuViewModel.ListDonVi = new ObservableCollection<CheckBoxItem>(listDonViCheckBox);
            PrintReportDieuChinhDuToanThuViewModel.Init();
            content = new PrintDieuChinhDuToanThu
            {
                DataContext = PrintReportDieuChinhDuToanThuViewModel
            };

            if (content != null)
            {
                DialogHost.Show(content, SystemConstants.ROOT_DIALOG, null, null);
            }
        }
    }
}
