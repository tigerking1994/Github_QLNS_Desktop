using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Service.Impl;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Control;
using System.Windows;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility.Enum;
using System.Windows.Data;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using FlexCel.Core;
using System.IO;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT.ImportKhtmBHYT;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT.PrintReport;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT
{
    public class KeHoachThuMuaBHYTIndexViewModel : GridViewModelBase<BhKhtmBHYTModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IKhtmBHYTService _khtmBHYTService;
        private readonly IKhtmBHYTChiTietService _khtmBHYTChiTietService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly ISysAuditLogService _log;
        private IDanhMucService _danhMucService;
        private SessionInfo _sessionInfo;
        private readonly IExportService _exportService;
        private ICollectionView _bhDonViModelsView;
        private ICollectionView _bhChungTuModelsView;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        public string ComboboxDisplayMemberPath => nameof(SelectedNsDonViModel.TenDonViIdDonVi);
        public override Type ContentType => typeof(KeHoachThuMuaBHYTIndex);
        public override string GroupName => MenuItemContants.GROUP_THU;
        public override string Description => "Danh sách kế hoạch thu mua BHYT thân nhân";
        public override string Name => "KH thu BHYT thân nhân";
        public KeHoachThuMuaBHYTDialogViewModel KeHoachThuMuaBHYTDialogViewModel { get; }
        public KeHoachThuMuaBHYTDetailViewModel KeHoachThuMuaBHYTDetailViewModel { get; }
        public ImportKhtmBHYTViewModel ImportKhtmBHYTViewModel { get; }
        public override PackIconKind IconKind => PackIconKind.RhombusOutline;
        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand ExportAggregateDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand PrintCommand { get; }
        public PrintReportKhtmBhytViewModel PrintReportKhtmBhytViewModel { get; }

        public KeHoachThuMuaBHYTIndexViewModel(
            IKhtmBHYTService khtmBHYTService,
            IKhtmBHYTChiTietService khtmBHYTChiTietService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            ISessionService sessionService,
            INsDonViService donViService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            ISysAuditLogService log,
            KeHoachThuMuaBHYTDialogViewModel keHoachThuMuaBHYTDialogViewModel,
            KeHoachThuMuaBHYTDetailViewModel keHoachThuMuaBHYTDetailViewModel,
            ImportKhtmBHYTViewModel importKhtmBHYTViewModel,
            IDanhMucService danhMucService,
            PrintReportKhtmBhytViewModel printReportKhtmBhytViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _khtmBHYTService = khtmBHYTService;
            _khtmBHYTChiTietService = khtmBHYTChiTietService;
            _exportService = exportService;
            _donViService = donViService;
            _log = log;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _danhMucService = danhMucService;
            KeHoachThuMuaBHYTDialogViewModel = keHoachThuMuaBHYTDialogViewModel;
            KeHoachThuMuaBHYTDetailViewModel = keHoachThuMuaBHYTDetailViewModel;
            ImportKhtmBHYTViewModel = importKhtmBHYTViewModel;
            PrintReportKhtmBhytViewModel = printReportKhtmBhytViewModel;

            KeHoachThuMuaBHYTDialogViewModel.ParentPage = this;
            KeHoachThuMuaBHYTDetailViewModel.ParentPage = this;
            ImportKhtmBHYTViewModel.ParentPage = this;

            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            LockCommand = new RelayCommand(OnLock);
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
            SearchCommand = new RelayCommand(obj => SearchData());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintCommand = new RelayCommand(OnPrint);
        }
        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }
        public bool IsButtonEnable
        {
            get
            {
                var result = false;
                var lstSelected = Items.Where(x => x.Selected).ToList();
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    result = true;
                }
                else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    var lstSelectedKhoa = lstSelected.Where(x => x.BKhoa).ToList();
                    var lstSelectedMo = lstSelected.Where(x => !x.BKhoa).ToList();
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
        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
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

        private bool _isOpenExcelPopup;
        public bool IsOpenExcelPopup
        {
            get => _isOpenExcelPopup;
            set => SetProperty(ref _isOpenExcelPopup, value);
        }

        private BhKhtmBHYTModel _selectedBhKhtmBHYTModel;
        public BhKhtmBHYTModel SelectedBhKhtmBHYTModel
        {
            get => _selectedBhKhtmBHYTModel;
            set
            {
                SetProperty(ref _selectedBhKhtmBHYTModel, value);
                if (_selectedBhKhtmBHYTModel != null)
                {
                    IsLock = _selectedBhKhtmBHYTModel.BKhoa;
                }
                else
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_selectedBhKhtmBHYTModel == null)
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsExportAggregateData));
                OnPropertyChanged(nameof(IsExportDataFilter));
            }
        }

        private List<BhKhtmBHYTModel> _lstChungTuOrigin;
        public List<BhKhtmBHYTModel> LstChungTuOrigin
        {
            get => _lstChungTuOrigin;
            set
            {
                SetProperty(ref _lstChungTuOrigin, value);
            }
        }

        private ObservableCollection<DonViModel> _bhDonViModelItems;
        public ObservableCollection<DonViModel> BhDonViModelItems
        {
            get => _bhDonViModelItems;
            set => SetProperty(ref _bhDonViModelItems, value);
        }

        private ObservableCollection<ComboboxItem> _voucherTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> VoucherTypes
        {
            get => _voucherTypes;
            set => SetProperty(ref _voucherTypes, value);
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
                        Items.Where(x => !x.BDaTongHop.Value).ForAll(c => c.Selected = value.Value);
                    }
                }
            }
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

        public bool IsCensorship
        {
            get
            {
                var itemSelected = Items.Where(x => x.Selected);
                return itemSelected.Any() && itemSelected.All(x => !x.IsSummaryVocher && x.BKhoa);
            }
        }

        public bool IsExportAggregateData => Items != null && Items.Any(n => n.Selected);
        public bool IsExportDataFilter => _selectedBhKhtmBHYTModel != null;

        private void SelectAll(bool select, IEnumerable<BhKhtmBHYTModel> models)
        {
            foreach (var model in models)
            {
                if (!model.BDaTongHop.GetValueOrDefault(false))
                {
                    model.Selected = select;
                }
            }
        }
        public bool IsEnableButtonDataShow => TabIndex == ImportTabIndex.Data;

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadData();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));

            }
        }
        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }
        public override void Init()
        {
            base.Init();
            _tabIndex = ImportTabIndex.Data;
            _sessionInfo = _sessionService.Current;
            LoadData();
            LoadLockStatus();
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            KeHoachThuMuaBHYTDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
        }
        private void LoadData()
        {
            _sessionInfo = _sessionService.Current;
            LoadKhtmBHYT();
            LoadDonVi();
            OnPropertyChanged(nameof(IsCensorship));
        }
        private void LoadKhtmBHYT()
        {
            try
            {
                var yearOfWork = _sessionInfo.YearOfWork;
                int yearOfBudget = _sessionInfo.YearOfBudget;
                int budgetSource = _sessionInfo.Budget;
                var currentIdDonVi = _sessionInfo.IdDonVi;

                var listChungTu = _khtmBHYTService.FindByCondition(_sessionInfo.YearOfWork);
                _lstChungTuOrigin = _mapper.Map<List<BhKhtmBHYTModel>>(listChungTu);

                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == ImportTabIndex.Data)
                    {
                        Items = _mapper.Map<ObservableCollection<BhKhtmBHYTModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop.GetValueOrDefault()));
                    }
                    else
                    {
                        var listCTTongHop = listChungTu.Where(x => x.IIDMaDonVi.Equals(_sessionInfo.IdDonVi) && x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTuTongHop)).ToList();
                        var listTongHop = new List<BhKhtmBHYTModel>();
                        foreach (var ctTongHop in listCTTongHop)
                        {
                            var parent = _mapper.Map<BhKhtmBHYTModel>(ctTongHop);
                            parent.IsExpand = true;
                            listTongHop.Add(parent);

                            if (!string.IsNullOrEmpty(ctTongHop.STongHop))
                            {
                                var listChild = _mapper.Map<List<BhKhtmBHYTModel>>(listChungTu.Where(x => ctTongHop.STongHop != null && ctTongHop.STongHop.Contains(x.SSoChungTu)));
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                                listTongHop.AddRange(listChild);
                            }
                        }

                        Items = _mapper.Map<ObservableCollection<BhKhtmBHYTModel>>(listTongHop);
                    }
                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<BhKhtmBHYTModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop.GetValueOrDefault()));
                }

                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhKhtmBHYTModel.Selected))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsExportDataFilter));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                        }
                        if (args.PropertyName == nameof(BhKhtmBHYTModel.IsCollapse))
                        {
                            ExpandChild();
                        }
                    };
                }
                _bhChungTuModelsView = CollectionViewSource.GetDefaultView(Items);
                _bhChungTuModelsView.Filter = BhytChungTuModelsFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void ExpandChild()
        {
            if (Items != null)
            {
                Items.Where(n => n.SoChungTuParent == SelectedBhKhtmBHYTModel.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
        }
        private void LoadDonVi()
        {
            try
            {
                var yearOfWork = _sessionInfo.YearOfWork;
                var predicate = PredicateBuilder.True<DonVi>();
                predicate = predicate.And(x => x.NamLamViec == yearOfWork);
                if (Items != null && Items.Count > 0)
                {
                    var idDonVis = Items.Select(x => x.IIDMaDonVi).ToList();
                    predicate = predicate.And(x => idDonVis.Any(y => y == x.IIDMaDonVi));
                    var listUnit = _donViService.FindByCondition(predicate).ToList();
                    BhDonViModelItems = new ObservableCollection<DonViModel>();
                    BhDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
                    _bhDonViModelsView = CollectionViewSource.GetDefaultView(BhDonViModelItems);
                    _bhDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                        ListSortDirection.Ascending));
                    _bhDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.TenDonVi),
                        ListSortDirection.Ascending));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void OnSelectedChange(object obj)
        {
            SelectedBhKhtmBHYTModel = (BhKhtmBHYTModel)obj;
            if (SelectedBhKhtmBHYTModel is { BKhoa: true } || SelectedBhKhtmBHYTModel == null)
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
                if (SelectedBhKhtmBHYTModel != null)
                {
                    if (SelectedBhKhtmBHYTModel.IIDMaDonVi.Equals(_sessionInfo.IdDonVi) && SelectedBhKhtmBHYTModel.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop)
                    {
                        OnAggregateEdit();
                    }
                    else
                    {
                        if (SelectedBhKhtmBHYTModel.SNguoiTao != _sessionInfo.Principal)
                        {
                            MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedBhKhtmBHYTModel.SNguoiTao));
                            return;
                        }
                        KeHoachThuMuaBHYTDialogViewModel.BhKhtmBHYTModel = SelectedBhKhtmBHYTModel;
                        KeHoachThuMuaBHYTDialogViewModel.Name = "Sửa kế hoạch thu";
                        KeHoachThuMuaBHYTDialogViewModel.Description = "Cập nhật kế hoạch thu BHYT thân nhân";
                        KeHoachThuMuaBHYTDialogViewModel.isSummary = false;
                        KeHoachThuMuaBHYTDialogViewModel.Init();
                        KeHoachThuMuaBHYTDialogViewModel.SavedAction = obj =>
                        {
                            this.OnRefresh();
                        };
                        KeHoachThuMuaBHYTDialogViewModel.ShowDialogHost();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void OnAggregateEdit()
        {
            try
            {
                //kiểm tra trạng thái các bản ghi
                List<BhKhtmBHYTModel> selectedKhtmBhyts = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedBhKhtmBHYTModel.STongHop) && SelectedBhKhtmBHYTModel.STongHop.Contains(x.SSoChungTu)).ToList();
                KeHoachThuMuaBHYTDialogViewModel.Name = "Sửa chứng từ";
                KeHoachThuMuaBHYTDialogViewModel.Description = "Sửa chứng từ tổng hợp";
                KeHoachThuMuaBHYTDialogViewModel.BhKhtmBHYTModel = SelectedBhKhtmBHYTModel;
                KeHoachThuMuaBHYTDialogViewModel.ListIdsKhtmBHYTSummary = selectedKhtmBhyts;
                KeHoachThuMuaBHYTDialogViewModel.isSummary = true;
                KeHoachThuMuaBHYTDialogViewModel.Init();
                KeHoachThuMuaBHYTDialogViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                    OnPropertyChanged(nameof(IsCensorship));
                    IsAllItemsSelected = false;
                };
                var addView = new KeHoachThuMuaBHYTDialog() { DataContext = KeHoachThuMuaBHYTDialogViewModel };
                DialogHost.Show(addView);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void OnLock(object obj)
        {
            try
            {
                if (IsLock)
                {
                    string lstSoChungTu = string.Join(", ", Items.Where(n => n.Selected && (bool)n.BKhoa).Select(n => n.SSoChungTu));
                    List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                    if (userAgency.All(x => x.Loai != LoaiDonVi.ROOT))
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUnlock, lstSoChungTu));
                        return;
                    }
                }
                else
                {
                    string lstSoChungTuInvalid = string.Join(", ", Items.Where(n => n.Selected && n.SNguoiTao != _sessionInfo.Principal && !(bool)n.BKhoa).Select(n => n.SSoChungTu));

                    if (!string.IsNullOrEmpty(lstSoChungTuInvalid))
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, lstSoChungTuInvalid));
                        return;
                    }
                }
                string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
                string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                MessageBoxResult dialogResult = MessageBoxHelper.Confirm(message);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    LockOrUnLockMultiVoucher();
                    MessageBoxHelper.Info(msgDone);
                    LockStatusSelected = LockStatus.ElementAt(0);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void ConfirmAggregate()
        {
            List<BhKhtmBHYTModel> selectedBhytChungTus = Items.Where(x => x.Selected && !x.IsSummaryVocher).ToList();
            bool checkAllowAggregate = selectedBhytChungTus.All(x => x.BKhoa);
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
            try
            {
                bool existTongHop = false;
                existTongHop = _khtmBHYTService.IsExistChungTuTongHop(_sessionInfo.YearOfWork);
                if (existTongHop)
                {
                    MessageBoxResult result = MessageBoxHelper.Confirm(Resources.MesConfirmSaveAggregateDemand);
                    if (result != MessageBoxResult.Yes)
                        return;
                }
                //kiểm tra trạng thái các bản ghi
                if (!_sessionService.Current.IsQuanLyDonViCha)
                {
                    MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                    return;
                }
                List<BhKhtmBHYTModel> selectedKhtmChungTus = Items.Where(x => x.Selected && x.BKhoa && !x.IsSummaryVocher).ToList();

                KeHoachThuMuaBHYTDialogViewModel.Name = "Thêm chứng từ";
                KeHoachThuMuaBHYTDialogViewModel.Description = "Tạo mới chứng từ tổng hợp";
                KeHoachThuMuaBHYTDialogViewModel.BhKhtmBHYTModel = new BhKhtmBHYTModel();
                KeHoachThuMuaBHYTDialogViewModel.isSummary = true;
                KeHoachThuMuaBHYTDialogViewModel.ListIdsKhtmBHYTSummary = selectedKhtmChungTus;
                KeHoachThuMuaBHYTDialogViewModel.Init();
                KeHoachThuMuaBHYTDialogViewModel.SavedAction = obj =>
                {
                    TabIndex = ImportTabIndex.MLNS;
                    this.LoadData();
                    OnPropertyChanged(nameof(IsCensorship));
                    IsAllItemsSelected = false;
                    OpenDetailDialog((BhKhtmBHYTModel)obj);
                };
                var addView = new KeHoachThuMuaBHYTDialog() { DataContext = KeHoachThuMuaBHYTDialogViewModel };
                DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhKhtmBHYTModel)eventArgs.Parameter);
        }
        private void LockOrUnLockMultiVoucher()
        {
            DateTime dtNow = DateTime.Now;
            var lstSelected = Items.Where(x => x.Selected).ToList();
            var isLock = !lstSelected.FirstOrDefault().BKhoa;
            foreach (var ct in lstSelected)
            {
                _khtmBHYTService.LockOrUnlock(ct.Id, isLock);
                var khtBhxh = Items.First(x => x.Id == ct.Id);
                khtBhxh.BKhoa = !ct.BKhoa;
            }
            _log.WriteLog(Resources.ApplicationName, "Khóa chứng từ kế hoạch thu mua BHYT", (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            LoadKhtmBHYT();
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
        
        private bool BhytChungTuModelsFilter(object obj)
        {
            if (!(obj is BhKhtmBHYTModel temp)) return true;
            var keyword = SearchText?.Trim().ToLower() ?? string.Empty;
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
                condition2 = condition2 && temp.IIDMaDonVi == SelectedNsDonViModel.IIDMaDonVi;
            }

            if (LockStatusSelected != null)
            {
                if (LockStatusSelected.ValueItem.Equals("1"))
                {
                    condition2 = condition2 && temp.BKhoa == true;
                }
                if (LockStatusSelected.ValueItem.Equals("2"))
                {
                    condition2 = condition2 && temp.BKhoa == false;
                }
            }
            var result = condition1 && condition2;
            return result;
        }
        private void SearchData()
        {
            if (_bhChungTuModelsView != null)
                _bhChungTuModelsView.Refresh();
        }
        protected override void OnRefresh()
        {
            LoadKhtmBHYT();
        }
        protected override void OnDelete()
        {
            try
            {
                if (SelectedBhKhtmBHYTModel == null) return;
                if (SelectedBhKhtmBHYTModel.SNguoiTao != _sessionInfo.Principal)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedBhKhtmBHYTModel.SNguoiTao));
                    return;
                }
                var messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.DeleteChungTuKhtBHXH, SelectedBhKhtmBHYTModel.SSoChungTu, SelectedBhKhtmBHYTModel.DNgayChungTu);
                var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                    OnDeleteHandler);
                DialogHost.Show(messageBox.Content, "RootDialog");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnDeleteHandler(NSDialogResult result)
        {
            try
            {
                KhtmBHYTChiTietCriteria searchCondition = new KhtmBHYTChiTietCriteria();
                if (result != NSDialogResult.Yes) return;
                DateTime dtNow = DateTime.Now;
                if (SelectedBhKhtmBHYTModel != null)
                {
                    var khtChungTu = _khtmBHYTService.FindById(SelectedBhKhtmBHYTModel.Id);
                    searchCondition.khtmBhytId = khtChungTu.Id;
                    if (khtChungTu != null)
                    {
                        //Xóa chứng từ BHXH
                        _khtmBHYTService.Delete(khtChungTu);

                        if (!string.IsNullOrEmpty(khtChungTu.STongHop))
                        {
                            var lstSoCtChild = khtChungTu.STongHop.Split(",");
                            foreach (var soct in lstSoCtChild)
                            {
                                var ctChild = _khtmBHYTService.FindByCondition(x => x.SSoChungTu.Equals(soct) && x.INamLamViec == _sessionInfo.YearOfWork).FirstOrDefault();
                                if (ctChild != null)
                                {
                                    ctChild.BDaTongHop = false;
                                    _khtmBHYTService.Update(ctChild);
                                }
                            }
                        }
                        //Xóa chi tiết chứng từ BHXH
                        var lstKhtBhxhChiTiet = _khtmBHYTChiTietService.FindKhtmBHYTChiTietByIdBhyt(searchCondition).ToList();
                        _khtmBHYTChiTietService.RemoveRange(lstKhtBhxhChiTiet);

                        _log.WriteLog(Resources.ApplicationName, "Xóa chứng từ kế hoạch thu BHXH", (int)TypeExecute.Delete, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                        LoadKhtmBHYT();
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        protected override void OnAdd()
        {
            KeHoachThuMuaBHYTDialogViewModel.Name = "Thêm mới kế hoạch thu";
            KeHoachThuMuaBHYTDialogViewModel.Description = "Tạo mới kế hoạch thu BHYT thân nhân";
            KeHoachThuMuaBHYTDialogViewModel.BhKhtmBHYTModel = new BhKhtmBHYTModel();
            KeHoachThuMuaBHYTDialogViewModel.isSummary = false;
            KeHoachThuMuaBHYTDialogViewModel.Init();
            KeHoachThuMuaBHYTDialogViewModel.SavedAction = obj =>
            {
                var khtmChungTu = (BhKhtmBHYTModel)obj;
                this.LoadData();
                OpenDetailDialog(khtmChungTu);
            };
            var exportView = new KeHoachThuMuaBHYTDialog() { DataContext = KeHoachThuMuaBHYTDialogViewModel };
            DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
        }
        /// <summary>
        /// Open Detail
        /// </summary>
        /// <param name="BhKhtmBHYTModel"></param>
        private void OpenDetailDialog(BhKhtmBHYTModel bhKhtmBHYTDetail, params bool[] isNew)
        {
            var idDonViCurrent = _sessionInfo.IdDonVi;
            var chungTuTH = Items.FirstOrDefault(item => item.IIDMaDonVi.Equals(idDonViCurrent));
            KeHoachThuMuaBHYTDetailViewModel.Model = ObjectCopier.Clone(bhKhtmBHYTDetail);
            KeHoachThuMuaBHYTDetailViewModel.CtTongHop = chungTuTH;
            KeHoachThuMuaBHYTDetailViewModel.IsVoucherSummary = bhKhtmBHYTDetail.IIDMaDonVi.Equals(idDonViCurrent) && !string.IsNullOrEmpty(bhKhtmBHYTDetail.STongHop);
            KeHoachThuMuaBHYTDetailViewModel.NumOfMonth = KeHoachThuMuaBHYTDetailViewModel.IsVoucherSummary ? "Tổng số tháng" : "Số tháng";
            KeHoachThuMuaBHYTDetailViewModel.Init();
            var view = new KeHoachThuMuaBHYTDetail() { DataContext = KeHoachThuMuaBHYTDetailViewModel };
            view.ShowDialog();
        }
        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhKhtmBHYTModel)obj, false);
        }
        /// <summary>
        /// Xuất excel chứng từ lập kế hoạch thu mua BHYT
        /// </summary>
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
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork)
                    .Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<BhKhtmBHYTModel> khtmBhytModelsSummary = Items.Where(x => x.Selected).ToList();
                    var yearOfWork = _sessionInfo.YearOfWork;

                    foreach (var item in khtmBhytModelsSummary)
                    {
                        var voucherItem = _khtmBHYTService.FindById(item.Id);
                        var voucherUnit = _donViService.FindByMaDonViAndNamLamViec(voucherItem.IIDMaDonVi, yearOfWork);
                        KhtmBHYTChiTietCriteria searchCondition = new KhtmBHYTChiTietCriteria();
                        searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.MaDonVi = item.IIDMaDonVi;
                        searchCondition.khtmBhytId = item.Id;
                        var khtmMucLucsOrder = _khtmBHYTChiTietService.FindBhKhtmBHYTChiTietByCondition(searchCondition).ToList();
                        //var lstData = _mapper.Map<ObservableCollection<BhKhtmBHYTChiTietModel>>(khtmMucLucsOrder).ToList();
                        Dictionary<string, object> Data = new Dictionary<string, object>();
                        List<int> columnHidden = new List<int>();
                        
                        var lstExportData = _mapper.Map<ObservableCollection<BhKhtmBHYTChiTietQuery>>(khtmMucLucsOrder).ToList();
                        CalculateData(lstExportData);
                        foreach (var row in lstExportData)
                        {
                            row.FDinhMuc = Math.Round(row.FDinhMuc.GetValueOrDefault());
                            row.FThanhTien = Math.Round(row.FThanhTien.GetValueOrDefault());
                        }
                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        Data.Add("FormatNumber", formatNumber);
                        Data.Add("SNguoiTao", item.SNguoiTao);
                        Data.Add("DonVi", _sessionInfo.TenDonVi);
                        Data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                        Data.Add("TieuDe1", "BÁO CÁO CHI TIẾT KẾ HOẠCH THU MUA BHYT NĂM " + _sessionInfo.YearOfWork);
                        Data.Add("h2", "Lữ đoàn X");
                        Data.Add("h1", "Lữ đoàn X");
                        Data.Add("ListMLNS", lstExportData);
                        Data.Add("ListData", lstExportData);
                        Data.Add("TongSoNguoi", lstExportData.Where(x => !x.IsHangCha).Sum(x => x.ISoNguoi.GetValueOrDefault()));
                        Data.Add("TongThanhTien", lstExportData.Where(x => !x.IsHangCha).Sum(x => x.FThanhTien.GetValueOrDefault()));
                        Data.Add("count", 10000);

                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_KHTM, ExportFileName.RPT_BH_KHTM_BHYT_CHUNGTU_CHITIET);
                        fileNamePrefix = item.SSoChungTu + "_" + voucherUnit.TenDonVi;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhKhtmBHYTChiTietQuery, BhKhtmBHYTChiTietModel, BhDmMucLucNganSach, BhKhtmBHYTChiTiet>(templateFileName, Data);
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
        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            var nsDonViOfCurrentUser = _donViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }
        private void OnImportData()
        {
            ImportKhtmBHYTViewModel.Init();
            ImportKhtmBHYTViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhKhtmBHYTModel)obj);
            };
            ImportKhtmBHYTViewModel.ShowDialog();
        }
        private void OnPrint(object param)
        {
            var bhytCheckPrintType = (BHYTCheckPrintType)((int)param);
            object content;

            PrintReportKhtmBhytViewModel.BHYTCheckPrintType = bhytCheckPrintType;
            PrintReportKhtmBhytViewModel.ReportNameTypeValue = (int)bhytCheckPrintType;
            PrintReportKhtmBhytViewModel.Init();
            content = new PrintKhtmBHYT
            {
                DataContext = PrintReportKhtmBhytViewModel
            };

            if (content != null)
            {
                DialogHost.Show(content, SystemConstants.ROOT_DIALOG, null, null);
            }
        }
        private void CalculateData(List<BhKhtmBHYTChiTietQuery> lstKhtmChungTuChiTiet)
        {
            lstKhtmChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.FThanhTien = 0;
                    return x;
                }).ToList();
            var temp = lstKhtmChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, lstKhtmChungTuChiTiet);
            }

        }
        private void CalculateParent(Guid? idParent, BhKhtmBHYTChiTietQuery item, List<BhKhtmBHYTChiTietQuery> lstKhtChungTuChiTiet)
        {
            var model = lstKhtChungTuChiTiet.FirstOrDefault(x => x.IIDNoiDung == idParent);
            if (model == null) return;
            model.ISoNguoi += item.ISoNguoi;
            //model.ISoThang += item.ISoThang;
            //model.FDinhMuc += item.FDinhMuc;
            model.FThanhTien += item.FThanhTien;
            CalculateParent(model.IdParent, item, lstKhtChungTuChiTiet);
        }
    }
}
