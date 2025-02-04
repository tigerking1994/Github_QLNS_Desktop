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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT
{
    public class QuyetToanCapKinhPhiKcbIndexViewModel : GridViewModelBase<BhQtCapKinhPhiKcbModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IQtcCapKinhPhiKcbService _chungTuService;
        private readonly IQtcCapKinhPhiKcbChiTietService _chungTuChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IExportService _exportService;
        private ICollectionView _bhChungTuModelsView;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;

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
                    var lstSelectedKhoa = lstSelected.Where(x => x.BKhoa).ToList();
                    var lstSelectedMo = lstSelected.Where(x => !x.BKhoa).ToList();
                    if (lstSelectedKhoa.Any() && lstSelectedMo.Any())
                    {
                        result = false;
                    }
                    else if (lstSelectedKhoa.Any())
                    {
                        IsLock = true;
                        result = true;
                    }
                    else if (lstSelectedMo.Any())
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

        private BhQtCapKinhPhiKcbModel _selectedChungTu;
        public BhQtCapKinhPhiKcbModel SelectedChungTu
        {
            get => _selectedChungTu;
            set
            {
                SetProperty(ref _selectedChungTu, value);
                if (_selectedChungTu != null)
                {
                    IsLock = _selectedChungTu.BKhoa;
                }
                else
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_selectedChungTu == null)
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsExportAggregateData));
                OnPropertyChanged(nameof(IsExportDataFilter));
            }
        }

        private List<BhQtCapKinhPhiKcbModel> _lstChungTuOrigin;
        public List<BhQtCapKinhPhiKcbModel> LstChungTuOrigin
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
                        Items.ForAll(c => c.Selected = value.Value);
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
                return itemSelected.Any() && itemSelected.All(x => x.BKhoa);
            }
        }

        public bool IsExportAggregateData => Items != null && Items.Any(n => n.Selected);
        public bool IsExportDataFilter => _selectedChungTu != null;

        private void SelectAll(bool select, IEnumerable<BhQtCapKinhPhiKcbModel> models)
        {
            foreach (var model in models.Where(x=>x.IsFilter))
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

        private ObservableCollection<ComboboxItem> _itemsLoaiKinhPhi;
        public ObservableCollection<ComboboxItem> ItemsLoaiKinhPhi
        {
            get => _itemsLoaiKinhPhi;
            set => SetProperty(ref _itemsLoaiKinhPhi, value);
        }

        private ComboboxItem _selectedLoaiKinhPhi;
        public ComboboxItem SelectedLoaiKinhPhi
        {
            get => _selectedLoaiKinhPhi;
            set
            {
                SetProperty(ref _selectedLoaiKinhPhi, value);
                if (_selectedLoaiKinhPhi != null)
                {
                    this.LoadData();
                }
            }
        }
        public string ComboboxDisplayMemberPath => nameof(SelectedNsDonViModel.TenDonViIdDonVi);
        public override Type ContentType => typeof(QuyetToanCapKinhPhiKcbIndex);
        //public override string GroupName => MenuItemContants.GROUP_QT_CHI_QUY;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;

        public override string Description => "Danh sách báo cáo QT KP KCB BHYT";
        public override string Name => "QT kinh phí KCB BHYT";
        public override PackIconKind IconKind => PackIconKind.BankTransferOut;
        public QuyetToanCapKinhPhiKcbImportViewModel QuyetToanCapKinhPhiKcbImportViewModel { get; }
        public QuyetToanCapKinhPhiKcbDialogViewModel QuyetToanCapKinhPhiKcbDialogViewModel { get; }
        public QuyetToanCapKinhPhiKcbDetailViewModel QuyetToanCapKinhPhiKcbDetailViewModel { get; }

        public PrintQuyetToanCapKinhPhiKCBBHYTViewModel PrintQuyetToanCapKinhPhiKCBBHYTViewModel { get; }

        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand ExportDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportDataFilterCommand { get; }
        public RelayCommand UploadFileCommand { get; }

        public QuyetToanCapKinhPhiKcbIndexViewModel(
            IQtcCapKinhPhiKcbService cpBsService,
            IQtcCapKinhPhiKcbChiTietService cpBsChungTuChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            QuyetToanCapKinhPhiKcbDialogViewModel quyetToanCapKinhPhiKcbDialogViewModel,
            QuyetToanCapKinhPhiKcbDetailViewModel quyetToanCapKinhPhiKcbDetailViewModel,
            QuyetToanCapKinhPhiKcbImportViewModel quyetToanCapKinhPhiKcbImportViewModel,
            PrintQuyetToanCapKinhPhiKCBBHYTViewModel printQuyetToanCapKinhPhiKCBBHYTViewModel,
            ISessionService sessionService,
            INsDonViService donViService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            ISysAuditLogService log)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _chungTuService = cpBsService;
            _chungTuChiTietService = cpBsChungTuChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _exportService = exportService;
            _donViService = donViService;
            _log = log;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            QuyetToanCapKinhPhiKcbDialogViewModel = quyetToanCapKinhPhiKcbDialogViewModel;
            QuyetToanCapKinhPhiKcbDetailViewModel = quyetToanCapKinhPhiKcbDetailViewModel;
            QuyetToanCapKinhPhiKcbImportViewModel = quyetToanCapKinhPhiKcbImportViewModel;
            PrintQuyetToanCapKinhPhiKCBBHYTViewModel = printQuyetToanCapKinhPhiKCBBHYTViewModel;

            QuyetToanCapKinhPhiKcbImportViewModel.ParentPage = this;
            QuyetToanCapKinhPhiKcbDialogViewModel.ParentPage = this;
            QuyetToanCapKinhPhiKcbDetailViewModel.ParentPage = this;
            PrintQuyetToanCapKinhPhiKCBBHYTViewModel.ParentPage = this;

            ExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            LockCommand = new RelayCommand(OnLock);
            ExportDataCommand = new RelayCommand(obj => OnExportDataCommand());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            SearchCommand = new RelayCommand(obj => SearchData());
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
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
            OpenDetailDialog((BhQtCapKinhPhiKcbModel)obj, false);
        }

        protected override void OnDelete()
        {
            if (SelectedChungTu == null)
                return;
            if (SelectedChungTu.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedChungTu.SNguoiTao));
                return;
            }
            var messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTuKhtBHXH, SelectedChungTu.SSoChungTu, SelectedChungTu.DNgayChungTu);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                OnDeleteHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }
        private bool ChungTuModelsFilter(object obj)
        {
            if (!(obj is BhQtCapKinhPhiKcbModel temp))
                return true;
            var keyword = SearchText?.Trim().ToLower() ?? string.Empty;
            var condition1 = false;
            var condition2 = true;
            var condition3 = true;
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(temp.SSoChungTu))
                    condition1 = condition1 || temp.SSoChungTu.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SMoTa))
                    condition1 = condition1 || temp.SMoTa.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SNguoiTao))
                    condition1 = condition1 || temp.SNguoiTao.ToLower().Contains(keyword);
            }
            else
            {
                condition1 = true;
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

            if (SelectedLoaiKinhPhi != null && SelectedLoaiKinhPhi.ValueItem != "-1")
            {
                if (SelectedLoaiKinhPhi.ValueItem == "1")
                {
                    // Do khi lưu dữ liệu với trường hợp "Kinh phí KCB BHYT quân nhân" thì ILoaiKinhPhi có thể lưu giá trị 0 hoặc 1
                    condition3 = condition3 && (temp.ILoaiKinhPhi == 0 || temp.ILoaiKinhPhi == 1);
                }
                else
                {
                    condition3 = condition3 && temp.ILoaiKinhPhi == Convert.ToInt32(SelectedLoaiKinhPhi.ValueItem);
                }
            }
            
            var result = condition1 && condition2 && condition3;
            temp.IsFilter = result;
            return result;
        }
        private void OnDeleteHandler(NSDialogResult result)
        {
            BhQtCapKinhPhiKcbChiTietCriteria searchCondition = new BhQtCapKinhPhiKcbChiTietCriteria();
            if (result != NSDialogResult.Yes)
                return;
            DateTime dtNow = DateTime.Now;
            if (SelectedChungTu != null)
            {
                var chungTu = _chungTuService.FindById(SelectedChungTu.Id);
                searchCondition.IIDCTCapKinhPhiKCB = chungTu.Id;
                if (chungTu != null)
                {
                    var lstChungTuChiTiet = _chungTuChiTietService.FindChungTuChiTietByChungTuId(searchCondition).ToList();
                    //Xóa chứng từ
                    _chungTuService.Delete(chungTu);
                    //Xóa chi tiết chứng từ
                    _chungTuChiTietService.RemoveRange(lstChungTuChiTiet);
                    _log.WriteLog(Resources.ApplicationName, "Xóa chứng từ", (int)TypeExecute.Delete, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    OnRefresh();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
            }
        }

        private void OnImportData()
        {
            QuyetToanCapKinhPhiKcbImportViewModel.Init();
            QuyetToanCapKinhPhiKcbImportViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQtCapKinhPhiKcbModel)obj);
            };
            QuyetToanCapKinhPhiKcbImportViewModel.ShowDialog();
        }

        private void OnSelectedChange(object obj)
        {
            SelectedChungTu = (BhQtCapKinhPhiKcbModel)obj;
            if (SelectedChungTu is { BKhoa: true } || SelectedChungTu == null)
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
                OpenDetailDialog((BhQtCapKinhPhiKcbModel)eventArgs.Parameter);
        }

        private void LoadData()
        {
            _sessionInfo = _sessionService.Current;
            LoadVouchers();
            OnPropertyChanged(nameof(IsCensorship));
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

        private void LoadVouchers()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var currentIdDonVi = _sessionInfo.IdDonVi;

            var listChungTu = _chungTuService.FindByYear(_sessionInfo.YearOfWork);
            _lstChungTuOrigin = _mapper.Map<List<BhQtCapKinhPhiKcbModel>>(listChungTu);
            Items = _mapper.Map<ObservableCollection<BhQtCapKinhPhiKcbModel>>(listChungTu.OrderBy(x => x.ILoaiKinhPhi).ThenBy(x => x.IQuy));

            foreach (var model in Items)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(BhQtCapKinhPhiKcbModel.Selected))
                    {
                        OnPropertyChanged(nameof(IsCensorship));
                        OnPropertyChanged(nameof(IsExportAggregateData));
                        OnPropertyChanged(nameof(IsExportDataFilter));
                        OnPropertyChanged(nameof(IsButtonEnable));
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                    }
                };
            }
            _bhChungTuModelsView = CollectionViewSource.GetDefaultView(Items);
            _bhChungTuModelsView.Filter = ChungTuModelsFilter;
        }

        private void LockOrUnLockMultiVoucher()
        {
            DateTime dtNow = DateTime.Now;
            var lstSelected = Items.Where(x => x.Selected).ToList();
            var isLock = !lstSelected.FirstOrDefault().BKhoa;
            foreach (var ct in lstSelected)
            {
                _chungTuService.LockOrUnlock(ct.Id, isLock);
                var chungTu = Items.First(x => x.Id == ct.Id);
                chungTu.BKhoa = !ct.BKhoa;
            }
            _log.WriteLog(Resources.ApplicationName, "Khóa chứng từ", (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            LoadVouchers();
        }

        protected override void OnAdd()
        {
            QuyetToanCapKinhPhiKcbDialogViewModel.Name = "Thêm mới";
            QuyetToanCapKinhPhiKcbDialogViewModel.Description = "Thêm mới báo cáo quyết toán cấp KP KCB BHYT";
            QuyetToanCapKinhPhiKcbDialogViewModel.Model = new BhQtCapKinhPhiKcbModel();
            QuyetToanCapKinhPhiKcbDialogViewModel.Init();
            QuyetToanCapKinhPhiKcbDialogViewModel.SavedAction = obj =>
            {
                var chungTu = (BhQtCapKinhPhiKcbModel)obj;
                this.LoadData();
                OpenDetailDialog(chungTu);
            };
            var exportView = new QuyetToanCapKinhPhiKcbDialog() { DataContext = QuyetToanCapKinhPhiKcbDialogViewModel };
            DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {
            if (SelectedChungTu != null)
            {

                if (SelectedChungTu.SNguoiTao != _sessionInfo.Principal)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedChungTu.SNguoiTao));
                    return;
                }
                QuyetToanCapKinhPhiKcbDialogViewModel.Model = SelectedChungTu;
                QuyetToanCapKinhPhiKcbDialogViewModel.Name = "Cập nhật";
                QuyetToanCapKinhPhiKcbDialogViewModel.Description = "Cập nhật thông tin";
                QuyetToanCapKinhPhiKcbDialogViewModel.Init();
                QuyetToanCapKinhPhiKcbDialogViewModel.SavedAction = obj =>
                {
                    var chungTu = (BhQtCapKinhPhiKcbModel)obj;
                    this.OnRefresh();
                    OpenDetailDialog(chungTu);
                };
                QuyetToanCapKinhPhiKcbDialogViewModel.ShowDialogHost();

            }
        }

        private void OnLock(object obj)
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
        protected override void OnRefresh()
        {
            LoadVouchers();
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
        /// <param name="BhQtCapKinhPhiKcbModel"></param>
        private void OpenDetailDialog(BhQtCapKinhPhiKcbModel voucherDetail, params bool[] isNew)
        {
            QuyetToanCapKinhPhiKcbDetailViewModel.Model = ObjectCopier.Clone(voucherDetail);
            QuyetToanCapKinhPhiKcbDetailViewModel.SoQuyetToanDisplay = voucherDetail.INamLamViec == voucherDetail.IQuy ? "Số quyết toán" : "Quyết toán quý này";
            QuyetToanCapKinhPhiKcbDetailViewModel.IsShowColumnKPKCBBHYT = !(voucherDetail.INamLamViec == voucherDetail.IQuy);
            QuyetToanCapKinhPhiKcbDetailViewModel.Init();
            var view = new QuyetToanCapKinhPhiKcbDetail() { DataContext = QuyetToanCapKinhPhiKcbDetailViewModel };
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
            LoadLoaiLinhPhi();
            QuyetToanCapKinhPhiKcbDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
        }

        /// <summary>
        /// Xuất excel
        /// </summary>
        private void OnExportDataCommand()
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

                    List<BhQtCapKinhPhiKcbModel> chungTu = Items.Where(x => x.Selected).ToList();

                    var yearOfWork = _sessionService.Current.YearOfWork;
                    var lstQuy = "1,2,3,4";
                    List<BhQtCapKinhPhiKcb>  listChungTu = _chungTuService.FindByYear(yearOfWork).Where(x => lstQuy.Contains(x.IQuy.ToString())).ToList();
                    foreach (var item in chungTu)
                    {
                        var currentDonVi = GetNsDonViOfCurrentUser();

                        BhQtCapKinhPhiKcbChiTietCriteria searchCondition = new BhQtCapKinhPhiKcbChiTietCriteria();
                        searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.IIDCTCapKinhPhiKCB = item.Id;
                        searchCondition.LstLns = item.SDslns.Split(",").Distinct().ToList();

                        searchCondition.IQuy = item.IQuy;
                        searchCondition.ILoaiKinhPhi = item.ILoaiKinhPhi;
                        var lstMaCoSoYTe = item.SCoSoYTe.Split(",").Distinct().ToList();
                        //foreach (var maCoSoYTe in lstMaCoSoYTe)
                        //{
                        searchCondition.SMaCSYT = item.SCoSoYTe;
                        var exportVoucher = _chungTuChiTietService.FindVoucherDetailByCondition(searchCondition).ToList();
                        var lstMucLuc = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(_sessionInfo.YearOfWork, item.SDslns);
                        var exportVoucherMap = _mapper.Map<ObservableCollection<BhQtCapKinhPhiKcbChiTietModel>>(exportVoucher).ToList();

                        foreach (var qtItem in exportVoucherMap)
                        {
                            if (item.IQuy == yearOfWork)
                            {
                                var listChungTuMap = listChungTu.Where(x => x.SCoSoYTe == qtItem.sMaCoSoYTe && x.SDslns == qtItem.SLns).ToList();
                                qtItem.FQuyetToan4Quy = listChungTuMap.Sum(x => x.FQuyetToanQuyNay);
                            }
                        }                     

                        CalculateData(exportVoucherMap);
                        var total = CalculateTotal(exportVoucherMap);
                        Dictionary<string, object> Data = new Dictionary<string, object>();

                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        Data.Add("TitleFirst", $"QUYẾT TOÁN CẤP KP KCB BHYT {_sessionService.Current.YearOfWork}");
                        Data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoChungTu}, ngày: {DateUtils.Format(item.DNgayChungTu)})");
                        Data.Add("FormatNumber", formatNumber);
                        Data.Add("SNguoiTao", item.SNguoiTao);
                        Data.Add("Cap1", currentDonVi.TenDonVi);
                        Data.Add("DonVi", _sessionService.Current.TenDonVi);
                        Data.Add("YearWork", yearOfWork);
                        Data.Add("YearWorkOld", yearOfWork - 1);
                        Data.Add("H2", "Lữ đoàn X");
                        Data.Add("H1", "Lữ đoàn X");
                        Data.Add("Items", exportVoucherMap);
                        Data.Add("MLNS", lstMucLuc);

                        Data.Add("TongKeHoachCap", total.FKeHoachCap);
                        Data.Add("TongQuyetToanQuyNay", total.FQuyetToanQuyNay);
                        Data.Add("TongConLai", total.FConLai);
                        Data.Add("TongDaQuyetToan", total.FDaQuyetToan);

                        if (item.IQuy == yearOfWork)
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTC_KP_KCB_BHYT, ExportFileName.RPT_BH_QTC_KPKCB_CHUNG_TU_CHITIET_NAM);
                        } else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTC_KP_KCB_BHYT, ExportFileName.RPT_BH_QTC_KPKCB_CHUNG_TU_CHITIET);
                        }

                        fileNamePrefix = StringUtils.ConvertVN(item.SSoChungTu);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhQtCapKinhPhiKcbModel, BhDmMucLucNganSach, BhQtCapKinhPhiKcbChiTietModel>(templateFileName, Data);
                        var nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);

                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        //}

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

        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(IsAllItemsSelected));
        }
        private void CalculateData(List<BhQtCapKinhPhiKcbChiTietModel> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.IsHangCha)
                 .ForAll(x =>
                 {
                     x.FKeHoachCap = 0;
                     x.FDaQuyetToan = 0;
                     x.FQuyetToanQuyNay = 0;
                 });

            var temp = lstChungTuChiTiet.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IIdMlns).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IIdMlnsCha, item, dictByMlns);
            }

        }
        private void CalculateParent(Guid idParent, BhQtCapKinhPhiKcbChiTietModel item, Dictionary<Guid, BhQtCapKinhPhiKcbChiTietModel> dictByMlns)
        {

            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FKeHoachCap += item.FKeHoachCap;
            model.FDaQuyetToan += item.FDaQuyetToan;
            model.FQuyetToanQuyNay += item.FQuyetToanQuyNay;

            CalculateParent(model.IIdMlnsCha, item, dictByMlns);
        }

        private BhQtCapKinhPhiKcbModel CalculateTotal(List<BhQtCapKinhPhiKcbChiTietModel> listData)
        {
            BhQtCapKinhPhiKcbModel chungTu = new BhQtCapKinhPhiKcbModel();
            chungTu.FKeHoachCap = 0;
            chungTu.FQuyetToanQuyNay = 0;
            chungTu.FConLai = 0;
            chungTu.FDaQuyetToan = 0;
            var listChildren = listData.Where(x => !x.IsHangCha).ToList();
            foreach (var item in listChildren)
            {
                chungTu.FKeHoachCap += item.FKeHoachCap;
                chungTu.FQuyetToanQuyNay += item.FQuyetToanQuyNay;
                chungTu.FConLai += item.FConLai;
                chungTu.FDaQuyetToan += item.FDaQuyetToan;
            }
            return chungTu;
        }

        /// <summary>
        /// Mở màn hình in
        /// </summary>
        /// <param name="param"></param>
        private void OpenPrintDialog(object param)
        {
            try
            {
                PrintQuyetToanCapKinhPhiKCBBHYTViewModel.Init();
                var view1 = new PrintQuyetToanCapKinhPhiKCBBHYT
                {
                    DataContext = PrintQuyetToanCapKinhPhiKCBBHYTViewModel
                };
                DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadLoaiLinhPhi()
        {
            ObservableCollection<ComboboxItem> lstKinhPhi = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem
                {
                    ValueItem = ((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN).ToString(),
                    DisplayItem = CapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN,
                    HiddenValue = LNSValue.LNS_9040001
                },
                new ComboboxItem
                {
                    ValueItem = ((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD).ToString(),
                    DisplayItem = CapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD,
                    HiddenValue = LNSValue.LNS_9040002
                },

            };
            ItemsLoaiKinhPhi = lstKinhPhi;
            ItemsLoaiKinhPhi.Insert(0, new ComboboxItem
            {
                DisplayItem = "--Tất cả--",
                ValueItem = "-1",
                HiddenValue = "-1",
            });

            SelectedLoaiKinhPhi = ItemsLoaiKinhPhi.First();
            OnPropertyChanged(nameof(ItemsLoaiKinhPhi));
        }
    }
}
