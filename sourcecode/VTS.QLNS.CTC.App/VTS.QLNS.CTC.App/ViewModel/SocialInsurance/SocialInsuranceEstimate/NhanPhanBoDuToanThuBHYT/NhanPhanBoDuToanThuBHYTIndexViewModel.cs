using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.NhanPhanBoDuToanThuBHYT;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.NhanPhanBoDuToanThuBHYT.Import;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanPhanBoDuToanThuBHYT.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanPhanBoDuToanThuBHYT
{
    public class NhanPhanBoDuToanThuBHYTIndexViewModel : GridViewModelBase<BhDtTmBHYTTNModel>
    {
        #region Interface
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IBhDtTmBHYTTNService _bhDtTmBHYTTNService;
        private readonly IBhDtTmBHYTTNChiTietService _chiTietService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IExportService _exportService;
        private ICollectionView _bhChungTuModelsView;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private ICollectionView _bhNhanPhanBoDuToanView;
        private ICollectionView _nsDonViModelsView;
        private ICollectionView _listBudgetIndex;
        private readonly IPbdttmBHYTService _pdttmBHYTService;

        #endregion

        #region Property
        private ImportNhanPhanBoDuToanThuBHYT _importNhanPhanBoDuToanThuBHYT;
        public override string GroupName => MenuItemContants.GROUP_THU;
        public override string Name => "Nhận DT thu BHYT thân nhân";
        public override string Description => "Danh sách đợt nhận DT thu BHYT thân nhân năm " + _sessionService.Current.YearOfWork;
        public override Type ContentType => typeof(NhanPhanBoDuToanThuBHYTIndex);
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
        public override PackIconKind IconKind => PackIconKind.RhombusOutline;

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


        private List<BhDtcDcdToanChiModel> _lstChungTuOrigin;
        public List<BhDtcDcdToanChiModel> LstChungTuOrigin
        {
            get => _lstChungTuOrigin;
            set
            {
                SetProperty(ref _lstChungTuOrigin, value);
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
                    this.LoadData();
                    IsLock = true;
                }
                else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                {
                    this.LoadData();
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
        public bool IsQuanLyDonViCha { get; set; }
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

        private void SelectAll(bool select, ObservableCollection<BhDtTmBHYTTNModel> models)
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
        #endregion

        #region ViewModel
        public NhanPhanBoDuToanThuBHYTDialogViewModel NhanPhanBoDuToanThuBHYTDialogViewModel { get; set; }
        public NhanPhanBoDuToanThuBHYTDetailViewModel NhanPhanBoDuToanThuBHYTDetailViewModel { get; set; }
        public NhanPhanBoDuToanThuBHYTImportlViewModel NhanPhanBoDuToanThuBHYTImportlViewModel { get; set; }
        #endregion

        #region RelayCommand
        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand LockUnLockCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportDataFilterCommand { get; }
        public RelayCommand UploadFileCommand { get; }
        #endregion

        #region Constructor
        public NhanPhanBoDuToanThuBHYTIndexViewModel(
            ILog loger,
            IMapper mapper,
            IBhDtTmBHYTTNService bhDtTmBHYTTNService,
            IBhDtTmBHYTTNChiTietService bhDtTmBHYTTNChiTietService,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            ISysAuditLogService sysAuditLogService,
            IExportService exportService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IPbdttmBHYTService pdttmBHYTService,
            NhanPhanBoDuToanThuBHYTDialogViewModel nhanPhanBoDuToanThuBHYTDialogViewModel,
            NhanPhanBoDuToanThuBHYTDetailViewModel nhanPhanBoDuToanThuBHYTDetailViewModel,
            NhanPhanBoDuToanThuBHYTImportlViewModel nhanPhanBoDuToanThuBHYTImportlViewModel)
        {
            _logger = loger;
            _mapper = mapper;
            _bhDtTmBHYTTNService = bhDtTmBHYTTNService;
            _chiTietService = bhDtTmBHYTTNChiTietService;
            _sessionService = sessionService;
            _iNguoiDungDonViService = nsNguoiDungDonViService;
            _nsDonViService = nsDonViService;
            _log = sysAuditLogService;
            _exportService = exportService;
            _pdttmBHYTService = pdttmBHYTService;

            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            LockUnLockCommand = new RelayCommand(OnLock);
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            SearchCommand = new RelayCommand(obj => SearchData());
            RefreshCommand = new RelayCommand(obj => Init());
            NhanPhanBoDuToanThuBHYTDialogViewModel = nhanPhanBoDuToanThuBHYTDialogViewModel;
            NhanPhanBoDuToanThuBHYTDetailViewModel = nhanPhanBoDuToanThuBHYTDetailViewModel;
            NhanPhanBoDuToanThuBHYTImportlViewModel = nhanPhanBoDuToanThuBHYTImportlViewModel;
        }
        #endregion

        #region Init
        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadDonVi();
            OnResetFilter();
            LoadLockStatus();
            LoadData();
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            NhanPhanBoDuToanThuBHYTDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
        }
        #endregion

        #region Update
        protected override void OnUpdate()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.SNguoiTao != _sessionInfo.Principal)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedItem.SNguoiTao));
                    return;
                }
                NhanPhanBoDuToanThuBHYTDialogViewModel.Model = SelectedItem;
                NhanPhanBoDuToanThuBHYTDialogViewModel.Init();
                NhanPhanBoDuToanThuBHYTDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                NhanPhanBoDuToanThuBHYTDialogViewModel.ShowDialogHost();
            }
        }
        #endregion

        #region Reset
        private void OnResetFilter()
        {
            try
            {
                SelectedDanhMucLoaiChi = null;
                _bhChungTuModelsView?.Refresh();
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

        protected override void OnRefresh()
        {
            this.LoadData();
            OnPropertyChanged(nameof(Items));
        }
        #endregion

        #region Load data
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
                NsDonViModelItems = new ObservableCollection<DonViModel>();
                NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
                _nsDonViModelsView = CollectionViewSource.GetDefaultView(NsDonViModelItems);
                _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                    ListSortDirection.Ascending));
                _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.TenDonVi),
                    ListSortDirection.Ascending));
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhDtTmBHYTTNModel)obj);
        }
        #endregion

        #region Importdata
        private void OnImportData()
        {
            try
            {
                NhanPhanBoDuToanThuBHYTImportlViewModel.Init();
                NhanPhanBoDuToanThuBHYTImportlViewModel.SavedAction = obj =>
                {
                    _importNhanPhanBoDuToanThuBHYT.Close();
                    this.LoadData();
                    //OnPropertyChanged(nameof(IsCensorship));
                    this.OnRefresh();
                    IsAllItemsSelected = false;
                    OpenDetailDialog((BhDtTmBHYTTNModel)obj);
                };
                _importNhanPhanBoDuToanThuBHYT = new ImportNhanPhanBoDuToanThuBHYT { DataContext = NhanPhanBoDuToanThuBHYTImportlViewModel };
                _importNhanPhanBoDuToanThuBHYT.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Lock
        private void OnLock(object obj)
        {
            if (IsLock)
            {
                string lstSoChungTu = string.Join(", ", Items.Where(n => n.IsSelected && (bool)n.BIsKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (userAgency.All(x => x.Loai != LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUnlock, lstSoChungTu));
                    return;
                }
            }
            else
            {
                string lstSoChungTuInvalid = string.Join(", ", Items.Where(n => n.IsSelected && n.SNguoiTao != _sessionInfo.Principal && !(bool)n.BIsKhoa).Select(n => n.SSoChungTu));

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

        private void LockOrUnLockMultiVoucher()
        {
            DateTime dtNow = DateTime.Now;
            var lstSelected = Items.Where(x => x.IsSelected).ToList();
            var isLock = !lstSelected.FirstOrDefault().BIsKhoa;
            foreach (var ct in lstSelected)
            {
                _bhDtTmBHYTTNService.LockOrUnlock(ct.Id, isLock);
                var dttmBhyt = Items.First(x => x.Id == ct.Id);
                dttmBhyt.BIsKhoa = !ct.BIsKhoa;
            }
            _log.WriteLog(Resources.ApplicationName, "Khóa chứng từ nhận phân bổ dự toán thu BHYT ", (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
        }
        #endregion

        #region Load data
        private void OnSelectedChange(object obj)
        {
            SelectedItem = (BhDtTmBHYTTNModel)obj;
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
        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(IsAllItemsSelected));
        }
        public override void LoadData(params object[] args)
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var currentIdDonVi = _sessionInfo.IdDonVi;
            var listChungTu = _bhDtTmBHYTTNService.FindByCondition(_sessionInfo.YearOfWork).OrderBy(x => x.SSoChungTu);
            Items = _mapper.Map<ObservableCollection<BhDtTmBHYTTNModel>>(listChungTu);
            foreach (var model in Items)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(BhDtTmBHYTTNModel.IsSelected))
                    {
                        OnPropertyChanged(nameof(IsCensorship));
                        OnPropertyChanged(nameof(IsExportAggregateData));

                        OnPropertyChanged(nameof(IsButtonEnable));
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                    }
                    if (args.PropertyName == nameof(BhDtTmBHYTTNModel.IsCollapse))
                    {
                        ExpandChild();
                    }
                };
            }
            _bhChungTuModelsView = CollectionViewSource.GetDefaultView(Items);
            _bhChungTuModelsView.Filter = ChungTuModelsFilter;
        }

        private void SearchData()
        {
            _bhNhanPhanBoDuToanView?.Refresh();
        }
        private void ExpandChild()
        {
            Items?.Where(n => n.SoChungTuParent == SelectedItem.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
        }

        private bool ChungTuModelsFilter(object obj)
        {
            if (!(obj is BhDtTmBHYTTNModel temp)) return true;
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
        #endregion

        #region Add 
        protected override void OnAdd()
        {
            NhanPhanBoDuToanThuBHYTDialogViewModel.Name = "Thêm mới chứng từ";
            NhanPhanBoDuToanThuBHYTDialogViewModel.Description = "Tạo mới chứng từ dự toán thu";
            NhanPhanBoDuToanThuBHYTDialogViewModel.Model = new BhDtTmBHYTTNModel();
            NhanPhanBoDuToanThuBHYTDialogViewModel.Init();
            NhanPhanBoDuToanThuBHYTDialogViewModel.SavedAction = obj =>
            {
                var dttChungTu = (BhDtTmBHYTTNModel)obj;
                this.LoadData();
                OpenDetailDialog(dttChungTu);
            };
            var exportView = new NhanPhanBoDuToanThuBHYTDialog() { DataContext = NhanPhanBoDuToanThuBHYTDialogViewModel };
            DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
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
                    var entity = _bhDtTmBHYTTNService.FindById(SelectedItem.Id);

                    if (entity != null && !string.IsNullOrEmpty(entity.SNguoiTao) && !entity.SNguoiTao.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {

                        MessageBox.Show(string.Format(Resources.VoucherDeleteKHTHWarning, entity.SNguoiTao), Resources.Alert);
                        return;
                    }

                    //kiểm tra chứng từ đã được phân bổ chưa, nếu đã được phân bổ thì không cho xóa
                    List<BhPbdttmBHYT> dtNhanPhanBoMaps = _pdttmBHYTService.FindByIdNhanDuToan(SelectedItem.Id.ToString()).ToList();
                    if (dtNhanPhanBoMaps.Count() > 0)
                    {
                        MessageBox.Show(Resources.AlertDeleteDivisionVoucher, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, !(SelectedItem.DNgayChungTu == DateTime.MinValue) ? DateTimeExtension.ToStringDate(SelectedItem.DNgayChungTu.Value) : string.Empty);
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
                DtTmBHYTTNChiTietCriteria searchCondition = new DtTmBHYTTNChiTietCriteria();
                if (result != NSDialogResult.Yes) return;
                DateTime dtNow = DateTime.Now;
                if (SelectedItem != null)
                {
                    var dttChungTu = _bhDtTmBHYTTNService.FindById(SelectedItem.Id);
                    searchCondition.DttmBhytId = dttChungTu.Id;
                    if (dttChungTu != null)
                    {
                        var lstDttBhxhChiTiet = _chiTietService.FindByParentId(searchCondition).ToList();
                        //Xóa chứng từ BHXH
                        _bhDtTmBHYTTNService.Delete(dttChungTu);

                        if (lstDttBhxhChiTiet != null)
                        {
                            //Xóa chi tiết chứng từ BHXH
                            _chiTietService.RemoveRange(lstDttBhxhChiTiet);
                        }

                        _log.WriteLog(Resources.ApplicationName, "Xóa chứng từ nhận dự toán phân bổ BHYT", (int)TypeExecute.Delete, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                        OnRefresh();
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Open detail
        private void OpenDetailDialog(BhDtTmBHYTTNModel dttChungTu)
        {
            var idDonViCurrent = _sessionInfo.IdDonVi;
            var chungTu = Items.FirstOrDefault(item => item.IIDMaDonVi.Equals(idDonViCurrent));
            NhanPhanBoDuToanThuBHYTDetailViewModel.Model = ObjectCopier.Clone(dttChungTu);
            NhanPhanBoDuToanThuBHYTDetailViewModel.CtTongHop = chungTu;

            NhanPhanBoDuToanThuBHYTDetailViewModel.Init();
            var view = new NhanPhanBoDuToanThuBHYTDetail() { DataContext = NhanPhanBoDuToanThuBHYTDetailViewModel };
            view.ShowDialog();
        }
        #endregion
    }
}
