using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DuToanThuBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DuToanThuBHXH.Import;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service.Impl;
using System.IO;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DuToanThuBHXH.Import;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DuToanThuBHXH
{
    public class DuToanThuBHXHIndexViewModel : GridViewModelBase<BhDttBHXHModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IDttBHXHService _dttBHXHService;
        private readonly IDttBHXHChiTietService _dttBHXHChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IExportService _exportService;
        private ICollectionView _bhChungTuModelsView;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private IDanhMucService _danhMucService;
        private readonly IDttBHXHPhanBoService _estimationService;

        private DuToanThuBHXHImport _duToanThuBHXHImport;
        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
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

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
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

        private BhDttBHXHModel _selectedBhDuToanThuModel;
        public BhDttBHXHModel SelectedBhDuToanThuModel
        {
            get => _selectedBhDuToanThuModel;
            set
            {
                SetProperty(ref _selectedBhDuToanThuModel, value);
                if (_selectedBhDuToanThuModel != null)
                {
                    IsLock = _selectedBhDuToanThuModel.BIsKhoa;
                }
                else
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_selectedBhDuToanThuModel == null)
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsExportAggregateData));
                OnPropertyChanged(nameof(IsExportDataFilter));
            }
        }

        private ObservableCollection<DonViModel> _bhDonViModelItems;
        public ObservableCollection<DonViModel> BhDonViModelItems
        {
            get => _bhDonViModelItems;
            set => SetProperty(ref _bhDonViModelItems, value);
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
                return itemSelected.Any() && itemSelected.All(x => x.BIsKhoa);
            }
        }

        public bool IsExportAggregateData => Items != null && Items.Any(n => n.Selected);
        public bool IsExportDataFilter => _selectedBhDuToanThuModel != null;

        private void SelectAll(bool select, IEnumerable<BhDttBHXHModel> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
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

        public bool IsQuanLyDonViCha { get; set; }
        public string ComboboxDisplayMemberPath => nameof(SelectedNsDonViModel.TenDonViIdDonVi);
        public override Type ContentType => typeof(DuToanThuBHXHIndex);
        public override string GroupName => MenuItemContants.GROUP_THU;
        public override string Description => "Danh sách đợt nhận dự toán thu BHXH, BHYT, BHTN năm " + _sessionService.Current.YearOfWork;
        public override string Name => "Nhận DT thu BHXH, BHYT, BHTN";
        public override PackIconKind IconKind => PackIconKind.RhombusOutline;
        public DuToanThuBHXHImportViewModel DuToanThuBHXHImportViewModel { get; }
        public DuToanThuBHXHDialogViewModel DuToanThuBHXHDialogViewModel { get; }
        public DuToanThuBHXHDetailViewModel DuToanThuBHXHDetailViewModel { get; }
        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand ExportAggregateDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportDataFilterCommand { get; }
        public RelayCommand UploadFileCommand { get; }

        public DuToanThuBHXHIndexViewModel(
            IDttBHXHService dttBHXHService,
            IDttBHXHChiTietService dttBHXHChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            INsDonViService nsDonViService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            DuToanThuBHXHDialogViewModel duToanThuBHXHDialogViewModel,
            DuToanThuBHXHDetailViewModel duToanThuBHXHDetailViewModel,
            ISessionService sessionService,
            INsDonViService donViService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            ISysAuditLogService log,
            IDanhMucService danhMucService,
            IDttBHXHPhanBoService estimationService,
            DuToanThuBHXHImportViewModel duToanThuBHXHImportViewModel
            )
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _dttBHXHService = dttBHXHService;
            _dttBHXHChiTietService = dttBHXHChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _exportService = exportService;
            _donViService = donViService;
            _danhMucService = danhMucService;
            _estimationService = estimationService;
            _log = log;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            DuToanThuBHXHDialogViewModel = duToanThuBHXHDialogViewModel;
            DuToanThuBHXHDetailViewModel = duToanThuBHXHDetailViewModel;
            DuToanThuBHXHImportViewModel = duToanThuBHXHImportViewModel;

            DuToanThuBHXHImportViewModel.ParentPage = this;
            DuToanThuBHXHDialogViewModel.ParentPage = this;
            DuToanThuBHXHDetailViewModel.ParentPage = this;

            ExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            LockCommand = new RelayCommand(OnLock);
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            SearchCommand = new RelayCommand(obj => SearchData());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData());
        }
        private void SearchData()
        {
            if (_bhChungTuModelsView != null)
                _bhChungTuModelsView.Refresh();
        }
        public override void OnCancel()
        {
            base.OnCancel();
            ParentPage.ParentPage.CurrentPage = null;
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhDttBHXHModel)obj, false);
        }

        protected override void OnDelete()
        {
            if (SelectedBhDuToanThuModel == null) return;
            if (SelectedBhDuToanThuModel.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedBhDuToanThuModel.SNguoiTao));
                return;
            }

            //kiểm tra chứng từ đã được phân bổ chưa, nếu đã được phân bổ thì không cho xóa
            List<BhDtPhanBoChungTu> dtNhanPhanBoMaps = _estimationService.FindByIdNhanDuToan(SelectedBhDuToanThuModel.Id.ToString()).ToList();
            if (dtNhanPhanBoMaps.Count() > 0)
            {
                MessageBox.Show(Resources.AlertDeleteDivisionVoucher, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTuKhtBHXH, SelectedBhDuToanThuModel.SSoChungTu, SelectedBhDuToanThuModel.DNgayChungTu);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                OnDeleteHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }
        private bool BhxhChungTuModelsFilter(object obj)
        {
            if (!(obj is BhDttBHXHModel temp)) return true;
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
        private void OnDeleteHandler(NSDialogResult result)
        {
            DttBHXHChiTietCriteria searchCondition = new DttBHXHChiTietCriteria();
            if (result != NSDialogResult.Yes) return;
            DateTime dtNow = DateTime.Now;
            if (SelectedBhDuToanThuModel != null)
            {
                var dttChungTu = _dttBHXHService.FindById(SelectedBhDuToanThuModel.Id);
                searchCondition.DtttBhxhId = dttChungTu.Id;
                if (dttChungTu != null)
                {
                    var lstDttBhxhChiTiet = _dttBHXHChiTietService.FindByParentId(searchCondition).ToList();
                    //Xóa chứng từ BHXH
                    _dttBHXHService.Delete(dttChungTu);

                    //Xóa chi tiết chứng từ BHXH
                    _dttBHXHChiTietService.RemoveRange(lstDttBhxhChiTiet);
                    _log.WriteLog(Resources.ApplicationName, "Xóa chứng từ kế hoạch thu BHXH", (int)TypeExecute.Delete, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    LoadDttBHXH();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
            }
        }

        private void OnImportData()
        {
            DuToanThuBHXHImportViewModel.Init();
            DuToanThuBHXHImportViewModel.SavedAction = obj =>
            {
                _duToanThuBHXHImport.Close();
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhDttBHXHModel)obj);
            };
            _duToanThuBHXHImport = new DuToanThuBHXHImport
            {
                DataContext = DuToanThuBHXHImportViewModel
            };
            _duToanThuBHXHImport.ShowDialog();
        }

        private void OnSelectedChange(object obj)
        {
            SelectedBhDuToanThuModel = (BhDttBHXHModel)obj;
            if (SelectedBhDuToanThuModel is { BIsKhoa: true } || SelectedBhDuToanThuModel == null)
            {
                IsEdit = false;
            }
            else
            {
                IsEdit = true;
            }
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhDttBHXHModel)eventArgs.Parameter);
        }

        private void LoadData()
        {
            _sessionInfo = _sessionService.Current;
            LoadDttBHXH();
            LoadDonVi();
            OnPropertyChanged(nameof(IsCensorship));
        }

        private void LoadDonVi()
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
                _bhChungTuModelsView = CollectionViewSource.GetDefaultView(BhDonViModelItems);
                _bhChungTuModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                    ListSortDirection.Ascending));
                _bhChungTuModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.TenDonVi),
                    ListSortDirection.Ascending));
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

        private void ExpandChild()
        {
            if (Items != null)
            {
                Items.Where(n => n.SoChungTuParent == SelectedBhDuToanThuModel.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
        }

        private void LoadDttBHXH()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var currentIdDonVi = _sessionInfo.IdDonVi;
            var listChungTu = _dttBHXHService.FindByCondition(_sessionInfo.YearOfWork);

            Items = _mapper.Map<ObservableCollection<BhDttBHXHModel>>(listChungTu);

            foreach (var model in Items)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(BhDttBHXHModel.Selected))
                    {
                        OnPropertyChanged(nameof(IsCensorship));
                        OnPropertyChanged(nameof(IsExportAggregateData));
                        OnPropertyChanged(nameof(IsExportDataFilter));
                        OnPropertyChanged(nameof(IsButtonEnable));
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                    }
                    if (args.PropertyName == nameof(BhDttBHXHModel.IsCollapse))
                    {
                        ExpandChild();
                    }
                };
            }
            _bhChungTuModelsView = CollectionViewSource.GetDefaultView(Items);
            _bhChungTuModelsView.Filter = BhxhChungTuModelsFilter;
        }
        private void LockOrUnLockMultiVoucher()
        {
            DateTime dtNow = DateTime.Now;
            var lstSelected = Items.Where(x => x.Selected).ToList();
            var isLock = !lstSelected.FirstOrDefault().BIsKhoa;
            foreach (var ct in lstSelected)
            {
                _dttBHXHService.LockOrUnlock(ct.Id, isLock);
                var dttBhxh = Items.First(x => x.Id == ct.Id);
                dttBhxh.BIsKhoa = !ct.BIsKhoa;
            }
            _log.WriteLog(Resources.ApplicationName, "Khóa chứng từ kế hoạch thu", (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            LoadDttBHXH();
        }

        protected override void OnAdd()
        {
            DuToanThuBHXHDialogViewModel.Name = "Thêm mới chứng từ";
            DuToanThuBHXHDialogViewModel.Description = "Tạo mới chứng từ dự toán thu";
            DuToanThuBHXHDialogViewModel.Model = new BhDttBHXHModel();
            DuToanThuBHXHDialogViewModel.Init();
            DuToanThuBHXHDialogViewModel.SavedAction = obj =>
            {
                var dttChungTu = (BhDttBHXHModel)obj;
                this.LoadData();
                OpenDetailDialog(dttChungTu);
            };
            var exportView = new DuToanThuBHXHDialog() { DataContext = DuToanThuBHXHDialogViewModel };
            DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {
            if (SelectedBhDuToanThuModel != null)
            {
                if (SelectedBhDuToanThuModel.SNguoiTao != _sessionInfo.Principal)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedBhDuToanThuModel.SNguoiTao));
                    return;
                }
                DuToanThuBHXHDialogViewModel.Model = SelectedBhDuToanThuModel;
                DuToanThuBHXHDialogViewModel.Name = "Sửa chứng từ";
                DuToanThuBHXHDialogViewModel.Description = "Cập nhật chứng từ nhận phân bổ dự toán";
                DuToanThuBHXHDialogViewModel.Init();
                DuToanThuBHXHDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                DuToanThuBHXHDialogViewModel.ShowDialogHost();
            }
        }

        private void OnLock(object obj)
        {
            if (IsLock)
            {
                string lstSoChungTu = string.Join(", ", Items.Where(n => n.Selected && (bool)n.BIsKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (userAgency.All(x => x.Loai != LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUnlock, lstSoChungTu));
                    return;
                }
            }
            else
            {
                string lstSoChungTuInvalid = string.Join(", ", Items.Where(n => n.Selected && n.SNguoiTao != _sessionInfo.Principal && !(bool)n.BIsKhoa).Select(n => n.SSoChungTu));

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
        protected override void OnRefresh()
        {
            LoadDttBHXH();
        }

        private void UnCheckBoxAll()
        {
            foreach (var item in Items)
            {
                item.Selected = false;
            }
        }

        /// <summary>
        /// Open Detail
        /// </summary>
        /// <param name="BhDttBHXHModel"></param>
        private void OpenDetailDialog(BhDttBHXHModel bhdttBHXHDetail, params bool[] isNew)
        {
            var idDonViCurrent = _sessionInfo.IdDonVi;
            var chungTu = Items.FirstOrDefault(item => item.IIDMaDonVi.Equals(idDonViCurrent));
            DuToanThuBHXHDetailViewModel.Model = ObjectCopier.Clone(bhdttBHXHDetail);
            DuToanThuBHXHDetailViewModel.CtTongHop = chungTu;

            DuToanThuBHXHDetailViewModel.Init();
            var view = new DuToanThuBHXHDetail() { DataContext = DuToanThuBHXHDetailViewModel };
            view.ShowDialog();
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
            DuToanThuBHXHDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            var nsDonViOfCurrentUser = _donViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }
        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(IsAllItemsSelected));
        }
        private void CalculateData(List<BhDttBHXHChiTietModel> lstDttChungTuChiTiet)
        {
            lstDttChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.FThuBHXHNguoiLaoDong = 0;
                    x.FThuBHXHNguoiSuDungLaoDong = 0;
                    x.FThuBHYTNguoiLaoDong = 0;
                    x.FThuBHYTNguoiSuDungLaoDong = 0;
                    x.FThuBHTNNguoiLaoDong = 0;
                    x.FThuBHTNNguoiSuDungLaoDong = 0;
                    return x;
                }).ToList();
            var temp = lstDttChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, lstDttChungTuChiTiet);
            }

        }
        private void CalculateParent(Guid? idParent, BhDttBHXHChiTietModel item, List<BhDttBHXHChiTietModel> lstDttChungTuChiTiet)
        {
            var model = lstDttChungTuChiTiet.FirstOrDefault(x => x.IIdMlns == idParent);
            if (model == null) return;
            model.FThuBHXHNguoiLaoDong += item.FThuBHXHNguoiLaoDong;
            model.FThuBHXHNguoiSuDungLaoDong += item.FThuBHXHNguoiSuDungLaoDong;
            model.FThuBHYTNguoiLaoDong += item.FThuBHYTNguoiLaoDong;
            model.FThuBHYTNguoiSuDungLaoDong += item.FThuBHYTNguoiSuDungLaoDong;
            model.FThuBHTNNguoiLaoDong += item.FThuBHTNNguoiLaoDong;
            model.FThuBHTNNguoiSuDungLaoDong += item.FThuBHTNNguoiSuDungLaoDong;
            CalculateParent(model.IdParent, item, lstDttChungTuChiTiet);
        }

        private void OnExportAggregateData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_BH_DTT, ExportFileName.RPT_BH_DU_TOAN_THU_BHXH_CHUNGTU);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    List<BhDttBHXHModel> listExport = Items.Where(x => x.Selected).ToList();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    foreach (var item in listExport)
                    {
                        var dataSLNS = item.SDslns.Split(',');
                        int namLamViec = _sessionInfo.YearOfWork;

                        DttBHXHChiTietCriteria searchCondition = new DttBHXHChiTietCriteria();
                        searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.IdDonVi = item.IIDMaDonVi;
                        searchCondition.DtttBhxhId = item.Id;
                        searchCondition.LstLns = dataSLNS.ToList();
                        List<BhDttBHXHChiTiet> _listChungTuChiTiet = new List<BhDttBHXHChiTiet>();

                        _listChungTuChiTiet = _dttBHXHChiTietService.FindDttBHXHChiTietByIdBhxh(searchCondition).ToList();
                        var lstChungTu = _mapper.Map<List<BhDttBHXHChiTietModel>>(_listChungTuChiTiet);
                        CalculateData(lstChungTu);
                        lstChungTu = lstChungTu.Where(x => x.FTongCong != 0).ToList();
                        var lstExportData = _mapper.Map<ObservableCollection<BhDttBHXHChiTietQuery>>(lstChungTu).ToList();
                        foreach (var row in lstExportData)
                        {
                            row.FThuBHXHNguoiLaoDong = Math.Round(row.FThuBHXHNguoiLaoDong.GetValueOrDefault());
                            row.FThuBHXHNguoiSuDungLaoDong = Math.Round(row.FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault());
                            row.FTongThuBHXH = Math.Round(row.FTongThuBHXH.GetValueOrDefault());
                            row.FThuBHYTNguoiLaoDong = Math.Round(row.FThuBHYTNguoiLaoDong.GetValueOrDefault());
                            row.FThuBHYTNguoiSuDungLaoDong = Math.Round(row.FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault());
                            row.FTongThuBHYT = Math.Round(row.FTongThuBHYT.GetValueOrDefault());
                            row.FThuBHTNNguoiLaoDong = Math.Round(row.FThuBHTNNguoiLaoDong.GetValueOrDefault());
                            row.FThuBHTNNguoiSuDungLaoDong = Math.Round(row.FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault());
                            row.FTongThuBHTN = Math.Round(row.FTongThuBHTN.GetValueOrDefault());
                            row.FTongCong = Math.Round(row.FTongCong.GetValueOrDefault());
                        }
                        var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
                        predicate = predicate.And(x => x.INamLamViec == namLamViec);
                        IEnumerable<BhDmMucLucNganSach> mucLucNganSaches = _bhDmMucLucNganSachService.FindByCondition(predicate).ToList().OrderBy(x => x.SXauNoiMa);
                        var lstMucLuc = mucLucNganSaches.Where(x => dataSLNS.Contains(x.SLNS)).ToList();
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        var donvi = _donViService.FindByNamLamViec(namLamViec).Where(x => x.IIDMaDonVi == item.IIDMaDonVi).FirstOrDefault();
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                        data.Add("TitleFirst", $"DỰ TOÁN THU BHXH NĂM {_sessionService.Current.YearOfWork}");
                        data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoQuyetDinh}, ngày: {DateUtils.Format(item.DNgayQuyetDinh)})");
                        data.Add("HeaderTenDonVi", $"Đơn vị: {donvi?.IIDMaDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{donvi?.TenDonVi}");
                        data.Add("TenDonVi", $"{donvi?.IIDMaDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{donvi?.TenDonVi}");
                        data.Add("Header2", string.Empty);
                        data.Add("Header1", string.Empty);
                        data.Add("TieuDe1", "Chứng từ cấp phát");
                        data.Add("TieuDe2", string.Format("Số chứng từ: {0}", item.SSoChungTu));
                        data.Add("ThoiGian", string.Format("Ngày chứng từ: {0}", item.DNgayChungTu.HasValue ? item.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty));
                        data.Add("NamLamViec", _sessionInfo.YearOfWork);
                        data.Add("Items", lstExportData);
                        data.Add("MLNS", lstMucLuc);

                        data.Add("TotalBHXHNLD", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNguoiLaoDong.GetValueOrDefault()));
                        data.Add("TotalBHYTNLD", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNguoiLaoDong.GetValueOrDefault()));
                        data.Add("TotalBHTNNLD", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNguoiLaoDong.GetValueOrDefault()));
                        data.Add("TotalBHXHNSD", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault()));
                        data.Add("TotalBHYTNSD", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault()));
                        data.Add("TotalBHTNNSD", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault()));
                        data.Add("TotalBHXH", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FTongThuBHXH.GetValueOrDefault()));
                        data.Add("TotalBHYT", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FTongThuBHYT.GetValueOrDefault()));
                        data.Add("TotalBHTN", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FTongThuBHTN.GetValueOrDefault()));
                        data.Add("Total", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FTongCong.GetValueOrDefault()));

                        fileNamePrefix = item.SSoChungTu;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhDttBHXHChiTietQuery, BhDttBHXHChiTietModel, BhDmMucLucNganSach>(templateFileName, data);
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
    }
}
