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
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Budget.Allocation;
using VTS.QLNS.CTC.App.View.Budget.Allocation.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.Allocation.Report;
using VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.ExportAllocation;
using VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Allocation
{
    public class AllocationIndexViewModel : ViewModelBase
    {
        private ICpChungTuService _chungTuService;
        private IMapper _mapper;
        private INsMucLucNganSachService _mucLucNganSachService;
        private ISessionService _sessionService;
        private ICpChungTuChiTietService _cpChungTuChiTietService;
        private IExportService _exportService;
        private INsDonViService _nsDonViService;
        private INsNguoiDungDonViService _nsNguoiDungDonViService;
        private IDanhMucService _danhMucService;
        private AllocationDetail view;
        private AllocationImport _importView;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private bool _isCapPhatToanDonVi;
        private readonly FtpStorageService _ftpStorageService;
        private readonly IVdtFtpRootService _ftpService;

        public override string FuncCode => NSFunctionCode.BUDGET_ALLOCATION;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Chứng từ cấp phát";
        public override Type ContentType => typeof(AllocationIndex);
        public override PackIconKind IconKind => PackIconKind.ViewList;
        public override string Title => "Danh sách chứng từ cấp phát";
        public override string Description => "Danh sách chứng từ cấp phát";

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private ObservableCollection<AllocationModel> _dataAllocation;
        public ObservableCollection<AllocationModel> DataAllocation
        {
            get => _dataAllocation;
            set => SetProperty(ref _dataAllocation, value);
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
                OnPropertyChanged(nameof(IsEnableLock));
                if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
                {
                    IsLock = true;
                }
                else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                {
                    IsLock = false;
                }
                LoadData();
            }

        }
        public bool IsEnableButtonDataShow => TabIndex == ImportTabIndex.Data;

        private AllocationModel _allocation;
        public AllocationModel SelectedAllocation
        {
            get => _allocation;
            set
            {
                SetProperty(ref _allocation, value);
                OnPropertyChanged(nameof(IsEdit));
                OnPropertyChanged(nameof(IsLock));
                OnPropertyChanged(nameof(IsEnableLock));
            }
        }

        private ObservableCollection<AllocationModel> _dataAllocationSummary;
        public ObservableCollection<AllocationModel> DataAllocationSummary
        {
            get => _dataAllocationSummary;
            set => SetProperty(ref _dataAllocationSummary, value);
        }

        private AllocationModel _selectedAllocationSummary;
        public AllocationModel SelectedAllocationSummary
        {
            get => _selectedAllocationSummary;
            set
            {
                SetProperty(ref _selectedAllocationSummary, value);
                OnPropertyChanged(nameof(IsEdit));
                OnPropertyChanged(nameof(IsLock));
                OnPropertyChanged(nameof(IsEnableLock));
            }
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                OnPropertyChanged(nameof(IsLock));
                OnPropertyChanged(nameof(IsEdit));
                OnPropertyChanged(nameof(IsEnableLock));
                OnPropertyChanged(nameof(IsEnableButtonSummary));
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
            }
        }

        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set
            {
                SetProperty(ref _isCollapse, value);
                ExpandChild();
            }
        }


        public bool? IsAllItemsSelected
        {
            get
            {
                if (DataAllocation != null)
                {
                    var selected = DataAllocation.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, DataAllocation);
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsEnableButtonExport));
                    OnPropertyChanged(nameof(IsEnableButtonSummary));
                }
            }
        }

        public bool IsEnableButtonExport => DataAllocation != null && DataAllocation.Where(n => n.Selected).Count() > 0;

        private static void SelectAll(bool select, IEnumerable<VTS.QLNS.CTC.App.Model.AllocationModel> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }

        private bool _isLock;

        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        //public bool IsLock => TabIndex == ImportTabIndex.Data ? (SelectedAllocation != null && SelectedAllocation.IsLocked) : (SelectedAllocationSummary != null && SelectedAllocationSummary.IsLocked);
        public bool IsEdit => TabIndex == ImportTabIndex.Data ? (SelectedAllocation != null && !SelectedAllocation.IsLocked) : (SelectedAllocationSummary != null && !SelectedAllocationSummary.IsLocked);
        //public bool IsEnableLock => TabIndex == ImportTabIndex.Data ? SelectedAllocation != null : SelectedAllocationSummary != null;
        public bool IsEnableLock
        {
            get
            {
                var listAllocation = DataAllocation?.Where(n => n.Selected).ToList();
                var listAllocationSummary = DataAllocationSummary?.Where(n => n.Selected).ToList();
                var result = false;
                if (TabIndex == ImportTabIndex.Data)
                {
                    if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && listAllocation.Count > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        var lstSelectedKhoa = listAllocation.Where(x => x.IsLocked).ToList();
                        var lstSelectedMo = listAllocation.Where(x => !x.IsLocked).ToList();
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
                }
                else
                {
                    if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && listAllocationSummary.Count > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        var lstSelectedSummaryKhoa = listAllocationSummary.Where(x => x.IsLocked).ToList();
                        var lstSelectedSummaryMo = listAllocationSummary.Where(x => !x.IsLocked).ToList();
                        if (lstSelectedSummaryKhoa.Count() > 0 && lstSelectedSummaryMo.Count() > 0)
                        {
                            result = false;
                        }
                        else if (lstSelectedSummaryKhoa.Count() > 0)
                        {
                            IsLock = true;
                            result = true;
                        }
                        else if (lstSelectedSummaryMo.Count() > 0)
                        {
                            IsLock = false;
                            result = true;
                        }
                    }
                }

                return result;
            }
        }
        public bool IsEnableButtonSummary => !_isCapPhatToanDonVi && DataAllocation != null && DataAllocation.Where(n => n.Selected && n.IsLocked).Count() > 0;
        public bool IsVisibleSummaryTab => !_isCapPhatToanDonVi && _sessionService.Current.IsQuanLyDonViCha;
        public bool IsVisibleColumnStatus => !_isCapPhatToanDonVi && !_sessionService.Current.IsQuanLyDonViCha;

        public bool IsCreate;

        public RelayCommand SortCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand RefeshCommand { get; }
        public RelayCommand LockUnLockCommand { get; }
        public RelayCommand ShowPopupAddCommand { get; }
        public RelayCommand ShowPopupEditCommand { get; }
        public RelayCommand SelectionDoubleClickCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ShowPopupReportCompareCommand { get; }
        public RelayCommand ShowPopupReportUnitCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ExportDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand SummaryProcessCommand { get; set; }

        public AllocationDiagramViewModel DiagramAllocationViewModel { get; }
        public AllocationReportViewModel ReportAllocationViewModel { get; }
        public AllocationDialogViewModel AllocationDialogViewModel { get; }
        public AllocationDetailViewModel AllocationDetailViewModel { get; }
        public AllocationReportCompareViewModel AllocationReportCompareViewModel { get; }
        public AllocationReportUnitsCompareViewModel AllocationReportUnitsCompareViewModel { get; }
        public ExportAllocationViewModel ExportAllocationViewModel { get; }
        public PrintAllocationNoticeViewModel PrintAllocationNoticeViewModel { get; }
        public PrintAllocationDonViViewModel PrintAllocationDonViViewModel { get; }
        public PrintAllocationRequestViewModel PrintAllocationRequestViewModel { get; }
        public PrintAllocationTypeViewModel PrintAllocationTypeViewModel { get; }
        public AllocationImportViewModel AllocationImportViewModel { get; }
        public AllocationSummaryViewModel AllocationSummaryViewModel { get; set; }
        public RelayCommand UploadFileCommand { get; set; }


        public AllocationIndexViewModel(ICpChungTuService cpChungTuService,
            IMapper mapper,
            ISessionService sessionService,
            ICpChungTuChiTietService cpChungTuChiTietService,
            INsMucLucNganSachService mucLucNganSachService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IExportService exportService,
            ILog logger,
            INsDonViService nsDonViService,
            IDanhMucService danhMucService,
            IVdtFtpRootService ftpService,
            FtpStorageService ftpStorageService,
            AllocationDiagramViewModel diagramAllocationViewModel,
            AllocationReportViewModel reportAllocationViewModel,
            AllocationDialogViewModel allocationDialogViewModel,
            PrintAllocationNoticeViewModel printAllocationNoticeViewModel,
            AllocationDetailViewModel allocationDetailViewModel,
            AllocationReportCompareViewModel allocationReportCompareViewModel,
            AllocationReportUnitsCompareViewModel allocationReportUnitsCompareViewModel,
            ExportAllocationViewModel exportAllocationViewModel,
            PrintAllocationDonViViewModel printAllocationDonViViewModel,
            PrintAllocationRequestViewModel printAllocationRequestViewModel,
            PrintAllocationTypeViewModel printAllocationTypeViewModel,
            AllocationSummaryViewModel allocationSummaryViewModel,
            AllocationImportViewModel allocationImportViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _chungTuService = cpChungTuService;
            _sessionService = sessionService;
            _cpChungTuChiTietService = cpChungTuChiTietService;
            _mucLucNganSachService = mucLucNganSachService;
            _exportService = exportService;
            _nsDonViService = nsDonViService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _danhMucService = danhMucService;
            _ftpService = ftpService;
            _ftpStorageService = ftpStorageService;

            DiagramAllocationViewModel = diagramAllocationViewModel;
            ReportAllocationViewModel = reportAllocationViewModel;
            AllocationDialogViewModel = allocationDialogViewModel;
            PrintAllocationNoticeViewModel = printAllocationNoticeViewModel;
            AllocationDetailViewModel = allocationDetailViewModel;
            AllocationReportCompareViewModel = allocationReportCompareViewModel;
            AllocationReportUnitsCompareViewModel = allocationReportUnitsCompareViewModel;
            ExportAllocationViewModel = exportAllocationViewModel;
            PrintAllocationDonViViewModel = printAllocationDonViViewModel;
            PrintAllocationRequestViewModel = printAllocationRequestViewModel;
            PrintAllocationTypeViewModel = printAllocationTypeViewModel;
            AllocationImportViewModel = allocationImportViewModel;
            AllocationSummaryViewModel = allocationSummaryViewModel;

            DiagramAllocationViewModel.ParentPage = this;
            ReportAllocationViewModel.ParentPage = this;
            AllocationDialogViewModel.ParentPage = this;
            PrintAllocationNoticeViewModel.ParentPage = this;

            SelectionDoubleClickCommand = new RelayCommand(o => DoubleClickCommand((AllocationModel)o));
            ShowPopupAddCommand = new RelayCommand(o => OnShowPopupAdd());
            ShowPopupEditCommand = new RelayCommand(o => OnShowPopupEdit());
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock());
            DeleteCommand = new RelayCommand(o => OnDelete());
            RefeshCommand = new RelayCommand(o => OnRefesh());
            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            ShowPopupReportCompareCommand = new RelayCommand(obj => ShowPopupReportCompare());
            ShowPopupReportUnitCommand = new RelayCommand(obj => ShowPopupReportUnitCompare());
            ExportDataCommand = new RelayCommand(obj => OnExportDataDialog());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            SummaryProcessCommand = new RelayCommand(obj => OnShowPopupSummary());
            UploadFileCommand = new RelayCommand(obj => OnUpload());
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

        public override void Init()
        {
            try
            {
                _sessionInfo = _sessionService.Current;
                TabIndex = ImportTabIndex.Data;
                LoadLockStatus();
                OnPropertyChanged(nameof(TabIndex));
                LoadSettingCapPhat();
                LoadData();
                AllocationDetailViewModel.ClosePopup += RefreshAfterClosePopup;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadSettingCapPhat()
        {
            DanhMuc dmCapPhatToanDonVi = _danhMucService.FindByCode(MaDanhMuc.CAP_PHAT_TOAN_DON_VI);
            if (dmCapPhatToanDonVi != null)
                bool.TryParse(dmCapPhatToanDonVi.SGiaTri, out _isCapPhatToanDonVi);
            else _isCapPhatToanDonVi = false;
        }

        private void ShowPopupReportCompare()
        {
            try
            {
                AllocationReportCompareViewModel.Init();
                AllocationReportCompare view = new AllocationReportCompare()
                {
                    DataContext = AllocationReportCompareViewModel
                };
                var result = DialogHost.Show(view, "RootDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ShowPopupReportUnitCompare()
        {
            try
            {
                AllocationReportUnitsCompareViewModel.Init();
                AllocationReportUnitsCompare view = new AllocationReportUnitsCompare()
                {
                    DataContext = AllocationReportUnitsCompareViewModel
                };
                var result = DialogHost.Show(view, "RootDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private async void OnShowPopupSummary()
        {
            try
            {
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                    return;
                }

                //kiểm tra trạng thái các bản ghi
                if (DataAllocation.Any(x => x.Selected && !x.IsLocked))
                {
                    MessageBoxHelper.Info(Resources.AlertAggregateUnLocked);
                    return;
                }

                if (DataAllocation.Where(x => x.Selected).Select(x => x.LoaiCap).Distinct().Count() > 1)
                {
                    MessageBoxHelper.Info(Resources.AlertKhacLoaiCapPhat);
                    return;
                }

                AllocationSummaryViewModel.Allocation = new AllocationModel();
                AllocationSummaryViewModel.DataAllocation = new ObservableCollection<AllocationModel>(DataAllocation.Where(n => n.Selected && n.IsLocked).ToList());
                AllocationSummaryViewModel.IsEditProcess = false;
                AllocationSummaryViewModel.Init();
                AllocationSummaryViewModel.SavedAction = obj =>
                {
                    this.OnRefesh();
                };
                var view = new AllocationSummary
                {
                    DataContext = AllocationSummaryViewModel
                };
                var result = await DialogHost.Show(view, "RootDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnShowPopupAdd()
        {
            try
            {
                if (_isCapPhatToanDonVi && !_sessionInfo.IsQuanLyDonViCha)
                {
                    MessageBoxHelper.Warning(Resources.AlertRoleAddAllocation);
                    return;
                }
                AllocationDialogViewModel.Allocation = new AllocationModel();
                AllocationDialogViewModel.IsEditProcess = false;
                AllocationDialogViewModel.Init();
                IsCreate = true;
                AllocationDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefesh();
                    OnShowDetailAllocation((AllocationModel)obj);
                };
                var view = new AllocationDialog
                {
                    DataContext = AllocationDialogViewModel
                };

                DialogHost.Show(view, "RootDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnShowPopupEdit()
        {
            if (SelectedAllocationSummary != null && TabIndex == ImportTabIndex.MLNS)
            {

                DanhMuc dmCapPhatToanDonVi = _danhMucService.FindByCode(MaDanhMuc.CAP_PHAT_TOAN_DON_VI);
                if (dmCapPhatToanDonVi != null)
                    bool.TryParse(dmCapPhatToanDonVi.SGiaTri, out _isCapPhatToanDonVi);
                else _isCapPhatToanDonVi = false;
                List<CpChungTuQuery> data = _chungTuService.FindByCondition(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                                    _sessionService.Current.Budget, _sessionService.Current.Principal, _isCapPhatToanDonVi, 1).ToList();

                List<AllocationModel> listChildSummary = _mapper.Map<List<AllocationModel>>(data.Where(n => SelectedAllocationSummary.DSSoChungTuTongHop.Split(",").Contains(n.SoChungTu)));


                AllocationSummaryViewModel.Allocation = SelectedAllocationSummary;
                AllocationSummaryViewModel.DataAllocation = new ObservableCollection<AllocationModel>(listChildSummary);
                AllocationSummaryViewModel.IsEditProcess = false;
                AllocationSummaryViewModel.Init();
                AllocationSummaryViewModel.SavedAction = obj =>
                {
                    this.OnRefesh();
                };
                var view = new AllocationSummary
                {
                    DataContext = AllocationSummaryViewModel
                };
                DialogHost.Show(view, "RootDialog", null, null);
            }
            else if (SelectedAllocation != null && TabIndex == ImportTabIndex.Data)
            {
                //check quyền được chỉnh sửa
                if (SelectedAllocation.UserCreator != _sessionInfo.Principal)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedAllocation.UserCreator));
                    return;
                }

                this.AllocationDialogViewModel.Allocation = SelectedAllocation;
                AllocationDialogViewModel.IsEditProcess = true;
                AllocationDialogViewModel.Init();
                IsCreate = true;
                //double? soCapPhat = _chungTuService.FindById(selectedItem.Id).SoCapPhat;
                AllocationDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefesh();
                    //AllocationModel allocation = (AllocationModel)obj;
                    //if (soCapPhat == allocation.SoCapPhat)
                    //{
                    //    IsCreate = false;
                    //}
                    //else
                    //{
                    //    IsCreate = true;
                    //}    
                    OnShowDetailAllocation((AllocationModel)obj);


                };
                var view = new AllocationDialog
                {
                    DataContext = AllocationDialogViewModel
                };
                DialogHost.Show(view, "RootDialog", null, null);
            }
        }

        private void OnLockUnLock()
        {
            try
            {
                //var selectedItem = TabIndex == ImportTabIndex.Data ? SelectedAllocation : SelectedAllocationSummary;
                //if (selectedItem == null)
                //    return;
                //if (IsLock)
                //{
                //    List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                //    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                //    {
                //        MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                //        return;
                //    }
                //    if (selectedItem.BDaTongHop.GetValueOrDefault())
                //    {
                //        MessageBoxHelper.Warning(Resources.AlertUnlockAggregatedVoucher);
                //        return;
                //    }
                //}
                //else
                //{
                //    if (selectedItem.UserCreator != _sessionInfo.Principal)
                //    {
                //        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, selectedItem.UserCreator));
                //        return;
                //    }
                //}
                //string msgConfirm = string.Format(IsLock ? Resources.MsgUnLock : Resources.MsgLock, Environment.NewLine, Environment.NewLine);
                //string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                //MessageBoxResult dialogResult = MessageBoxHelper.Confirm(msgConfirm);
                //if (dialogResult == MessageBoxResult.Yes)
                //{
                //    OnLockHandler(selectedItem, msgDone);
                //}
                if (TabIndex == ImportTabIndex.Data)
                {
                    if (IsLock)
                    {
                        var listSoChungTu = string.Join(", ", DataAllocation.Where(n => n.Selected && n.IsLocked).Select(n => n.SoChungTu));
                        List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                        if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                        {
                            //MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                            MessageBoxHelper.Warning(string.Format("Đồng chí không được mở khóa chứng từ {0} do không có quyền tổng hợp"));
                            return;
                        }

                        var listSoChungTuDaTongHop = string.Join(", ", DataAllocation.Where(n => n.Selected && n.BDaTongHop.GetValueOrDefault() && n.IsLocked).Select(n => n.SoChungTu));

                        if (!string.IsNullOrEmpty(listSoChungTuDaTongHop))
                        {
                            //MessageBoxHelper.Warning(Resources.AlertUnlockAggregatedVoucher);
                            MessageBoxHelper.Warning(string.Format("Đồng chí không được mở khóa chứng từ {0} do đã lên tổng hợp", listSoChungTuDaTongHop));
                            return;
                        }
                    }
                    else
                    {
                        var listSoChungTuInvalid = string.Join(", ", DataAllocation.Where(n => n.Selected && n.UserCreator != _sessionInfo.Principal && !n.IsLocked).Select(n => n.SoChungTu));

                        if (!string.IsNullOrEmpty(listSoChungTuInvalid))
                        {
                            //MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, SelectedAllocationItem.UserCreator));
                            MessageBoxHelper.Warning(string.Format("Đồng chí không có quyền khóa chứng từ {0} do không phải người tạo", listSoChungTuInvalid));
                            return;
                        }

                    }
                    string msgConfirm = string.Format(IsLock ? Resources.MsgUnLock : Resources.MsgLock, Environment.NewLine, Environment.NewLine);
                    string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                    MessageBoxResult dialogResult = MessageBoxHelper.Confirm(msgConfirm);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        foreach (var SelectedAllocationItem in DataAllocation.Where(n => n.Selected))
                        {
                            OnLockHandler(SelectedAllocationItem, msgDone);
                        }
                        MessageBoxHelper.Info(msgDone);
                        //OnPropertyChanged(nameof(LockStatusSelected));
                        //LockStatusSelected = IsLock ? LockStatus.ElementAt(2) : LockStatus.ElementAt(1);
                        LockStatusSelected = LockStatus.ElementAt(0);

                    }

                }
                else
                {
                    if (IsLock)
                    {
                        var listSoChungTuSummary = string.Join(", ", DataAllocationSummary.Where(n => n.Selected).Select(n => n.SoChungTu));
                        List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                        if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                        {
                            //MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                            MessageBoxHelper.Warning(string.Format("Đồng chí không được mở khóa chứng từ {0} do không có quyền tổng hợp", listSoChungTuSummary));
                            return;
                        }

                        var listSoChungTuSummaryDaTongHop = string.Join(", ", DataAllocationSummary.Where(n => n.Selected && n.BDaTongHop.GetValueOrDefault()).Select(n => n.SoChungTu));
                        if (!string.IsNullOrEmpty(listSoChungTuSummaryDaTongHop))
                        {
                            //MessageBoxHelper.Warning(Resources.AlertUnlockAggregatedVoucher);
                            MessageBoxHelper.Warning(string.Format("Đồng chí không được mở khóa chứng từ {0} do đã lên tổng hợp", listSoChungTuSummaryDaTongHop));
                            return;
                        }
                    }
                    else
                    {
                        var listSoChungTuSummaryInvalid = string.Join(", ", DataAllocationSummary.Where(n => n.Selected && n.UserCreator != _sessionInfo.Principal).Select(n => n.SoChungTu));
                        if (!string.IsNullOrEmpty(listSoChungTuSummaryInvalid))
                        {
                            //MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, SelectedAllocationSummaryItem.UserCreator));
                            MessageBoxHelper.Warning(string.Format("Đồng chí không có quyền khóa chứng từ {0} do không phải người tạo", listSoChungTuSummaryInvalid));
                            return;
                        }
                    }
                    string msgConfirm = string.Format(IsLock ? Resources.MsgUnLock : Resources.MsgLock, Environment.NewLine, Environment.NewLine);
                    string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                    MessageBoxResult dialogResult = MessageBoxHelper.Confirm(msgConfirm);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        foreach (var SelectedAllocationSummaryItem in DataAllocationSummary.Where(n => n.Selected))
                        {
                            OnLockHandler(SelectedAllocationSummaryItem, msgDone);
                        }
                        MessageBoxHelper.Info(msgDone);
                        //OnPropertyChanged(nameof(LockStatusSelected));
                        //LockStatusSelected = IsLock ? LockStatus.ElementAt(2) : LockStatus.ElementAt(1);
                        LockStatusSelected = LockStatus.ElementAt(0);

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnLockHandler(AllocationModel obj, string msgDone)
        {
            _chungTuService.LockOrUnLock(obj.Id, !obj.IsLocked);
            //MessageBoxHelper.Info(msgDone);
            //OnRefesh();
        }

        private void OnDelete()
        {
            try
            {
                AllocationModel selectedItem = TabIndex == ImportTabIndex.Data ? SelectedAllocation : SelectedAllocationSummary;
                if (selectedItem.UserCreator != _sessionInfo.Principal)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, selectedItem.UserCreator));
                    return;
                }
                string msgConfirm = string.Format(Resources.DeleteChungTu, selectedItem.SoChungTu, selectedItem.NgayChungTuString);
                MessageBoxResult dialogResult = MessageBoxHelper.Confirm(msgConfirm);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    if (selectedItem.BDaTongHop.HasValue && selectedItem.BDaTongHop.Value)
                    {
                        MessageBoxHelper.Warning(Resources.AlertDeleteAggregatedVoucher);
                        return;
                    }
                    DeleteChungTuChiTiet(selectedItem.Id);
                    _chungTuService.Delete(selectedItem.Id);
                    if (!string.IsNullOrEmpty(selectedItem.DSSoChungTuTongHop))
                    {
                        List<Guid> voucherIds = DataAllocationSummary.Where(x => x.SoChungTuParent == selectedItem.SoChungTu).Select(x => x.Id).ToList();
                        _chungTuService.UpdateAggregateStatus(string.Join(",", voucherIds));
                    }
                }
                OnRefesh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void DeleteChungTuChiTiet(Guid idChungTu)
        {
            var predicate = PredicateBuilder.True<NsCpChungTuChiTiet>();
            predicate = predicate.And(x => x.IIdCtcapPhat == idChungTu);
            List<NsCpChungTuChiTiet> list = _cpChungTuChiTietService.FindByCondition(predicate).ToList();
            if (list != null && list.Count > 0)
            {
                foreach (NsCpChungTuChiTiet item in list)
                {
                    _cpChungTuChiTietService.Delete(item.Id);
                }
            }
        }

        private void OnRefesh()
        {
            LoadData();
        }

        public void DoubleClickCommand(AllocationModel allocationDetail)
        {
            IsCreate = false;
            OnShowDetailAllocation(allocationDetail);
        }
        public void OnShowDetailAllocation(AllocationModel allocationDetail)
        {
            try
            {
                if (allocationDetail == null)
                    return;
                AllocationDetailViewModel.Model = allocationDetail;
                AllocationDetailViewModel.DataAllocation = DataAllocation;
                AllocationDetailViewModel.DataAllocationSummary = DataAllocationSummary;
                AllocationDetailViewModel.Init();
                AllocationDetailViewModel.IsCreate = IsCreate;
                AllocationDetailViewModel.SavedAction = obj =>
                {
                    this.OnRefesh();
                };
                view = new AllocationDetail
                {
                    DataContext = AllocationDetailViewModel
                };
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void RefreshAfterClosePopup(object sender, EventArgs e)
        {
            try
            {
                view.Close();
                OnRefesh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadData()
        {
            try
            {
                var currentIdDonVi = _sessionInfo.IdDonVi;
                DonVi donVi0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
                List<CpChungTuQuery> data = _chungTuService.FindByCondition(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget,
                                                    _sessionInfo.Budget, _sessionInfo.Principal, _isCapPhatToanDonVi, 1).ToList();

                if (_sessionInfo.IsQuanLyDonViCha)
                {
                    DataAllocation = _mapper.Map<ObservableCollection<AllocationModel>>(data.Where(n => n.IdDonVi != donVi0.IIDMaDonVi || string.IsNullOrEmpty(n.DSSoChungTuTongHop)));

                    List<AllocationModel> listSummary = new List<AllocationModel>();
                    foreach (CpChungTuQuery itemTongHop in data.Where(n => n.IdDonVi == donVi0.IIDMaDonVi && !string.IsNullOrEmpty(n.DSSoChungTuTongHop)))
                    {
                        listSummary.Add(_mapper.Map<AllocationModel>(itemTongHop));
                        listSummary.Where(n => n.IdDonVi == donVi0.IIDMaDonVi).Select(n => { n.IsExpand = true; return n; }).ToList();
                        List<AllocationModel> listChildSummary = _mapper.Map<List<Model.AllocationModel>>(data.Where(n => itemTongHop.DSSoChungTuTongHop.Split(",").Contains(n.SoChungTu)));
                        listChildSummary.Select(n =>
                        {
                            n.IsChildSumary = true; n.SoChungTuParent = itemTongHop.SoChungTu;
                            return n;
                        }).ToList();

                        listSummary.AddRange(listChildSummary);
                    }
                    if (listSummary != null && listSummary.Count > 0)
                    {
                        List<string> chungTuChildSummary = new List<string>();
                        foreach (var item in listSummary)
                        {
                            if (!string.IsNullOrEmpty(item.DSSoChungTuTongHop))
                            {
                                chungTuChildSummary.AddRange(item.DSSoChungTuTongHop.Split(",").ToList());
                            }
                        }
                        DataAllocation = new ObservableCollection<AllocationModel>(DataAllocation.Where(n => !chungTuChildSummary.Contains(n.SoChungTu)).ToList());
                    }
                    DataAllocationSummary = new ObservableCollection<AllocationModel>(listSummary);
                }
                else
                {
                    if (_isCapPhatToanDonVi && !_sessionInfo.IsQuanLyDonViCha)
                        DataAllocation = new ObservableCollection<AllocationModel>();
                    else DataAllocation = _mapper.Map<ObservableCollection<AllocationModel>>(data.Where(n => string.IsNullOrEmpty(n.DSSoChungTuTongHop)));
                    DataAllocationSummary = new ObservableCollection<AllocationModel>();
                }

                if (DataAllocation != null && DataAllocation.Count > 0)
                {
                    if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
                    {
                        DataAllocation = new ObservableCollection<AllocationModel>(DataAllocation.Where(n => n.IsLocked).ToList());
                    }
                    if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
                    {
                        DataAllocation = new ObservableCollection<AllocationModel>(DataAllocation.Where(n => !n.IsLocked).ToList());
                    }
                    SelectedAllocation = DataAllocation.FirstOrDefault();
                }

                foreach (AllocationModel model in DataAllocation)
                {
                    model.PropertyChanged += DetailModel_PropertyChanged;
                }

                if (DataAllocationSummary != null && DataAllocationSummary.Count > 0)
                {
                    if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
                    {
                        DataAllocationSummary = new ObservableCollection<AllocationModel>(DataAllocationSummary.Where(n => n.IsLocked).ToList());
                    }
                    if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
                    {
                        DataAllocationSummary = new ObservableCollection<AllocationModel>(DataAllocationSummary.Where(n => !n.IsLocked).ToList());
                    }
                    SelectedAllocationSummary = DataAllocationSummary.FirstOrDefault();
                }
                foreach (AllocationModel model in DataAllocationSummary)
                {
                    model.PropertyChanged += DetailModel_PropertyChanged;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExpandChild()
        {
            if (SelectedAllocationSummary != null)
            {
                DataAllocationSummary.Where(n => n.SoChungTuParent == SelectedAllocationSummary.SoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
            OnPropertyChanged(nameof(DataAllocationSummary));
        }

        /// <summary>
        /// Mở màn hình in
        /// </summary>
        /// <param name="param"></param>
        private void OpenPrintDialog(object param)
        {
            try
            {
                if (_isCapPhatToanDonVi && !_sessionInfo.IsQuanLyDonViCha)
                {
                    MessageBoxHelper.Warning(Resources.AlertRolePrintReportAllocation);
                    return;
                }
                int dialogType = (int)param;
                switch (dialogType)
                {
                    case (int)AllocationPrintType.PRINT_AllOCATION_NOTICE:
                        PrintAllocationNoticeViewModel.Init();
                        var view1 = new PrintAllocationNotice
                        {
                            DataContext = PrintAllocationNoticeViewModel
                        };
                        DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                    case (int)AllocationPrintType.PRINT_ALLOCATION_DONVI:
                        PrintAllocationDonViViewModel.Init();
                        var view2 = new PrintAllocationDonVi
                        {
                            DataContext = PrintAllocationDonViViewModel
                        };
                        DialogHost.Show(view2, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                    case (int)AllocationPrintType.PRINT_ALLOCATION_REQUEST:
                        PrintAllocationRequestViewModel.Init();
                        var view3 = new PrintAllocationRequest
                        {
                            DataContext = PrintAllocationRequestViewModel
                        };
                        DialogHost.Show(view3, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                    case (int)AllocationPrintType.PRINT_ALLOCATION_TYPE:
                        PrintAllocationTypeViewModel.Init();
                        var view4 = new PrintAllocationType
                        {
                            DataContext = PrintAllocationTypeViewModel
                        };
                        DialogHost.Show(view4, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(AllocationModel.Selected))
            {
                OnPropertyChanged(nameof(IsEnableButtonExport));
                OnPropertyChanged(nameof(IsEnableButtonSummary));
                OnPropertyChanged(nameof(IsEnableLock));
            }

            if (args.PropertyName == nameof(AllocationModel.IsCollapse))
            {
                ExpandChild();
            }
        }

        private void OnExportDataDialog()
        {

            ExportAllocationViewModel.DataAllocation = DataAllocation;
            ExportAllocationViewModel.Init();
            var addView = new View.Budget.Allocation.ExportAllocation.ExportAllocation() { DataContext = ExportAllocationViewModel };
            DialogHost.Show(addView, SettlementScreen.ROOT_DIALOG, null, null);
        }

        public void OnExportData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CP, ExportFileName.RPT_CAPPHAT_CHUNGTU_EXPORT);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    List<AllocationModel> listExport = DataAllocation.Where(x => x.Selected).ToList();
                    foreach (AllocationModel item in listExport)
                    {
                        List<string> listIdDonVi = item.IdDonVi.Split(",").ToList();
                        foreach (string idDonVi in listIdDonVi)
                        {
                            DonVi donViChild = _nsDonViService.FindByIdDonVi(idDonVi, _sessionInfo.YearOfWork);
                            DonVi donViParent = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
                            List<AllocationDetailModel> listAllocationDetail = GetDetailDataExport(item, idDonVi);
                            int namLamViec = _sessionInfo.YearOfWork;
                            List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(namLamViec).ToList();
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("Header2", donViChild != null ? donViChild.TenDonVi : string.Empty);
                            data.Add("Header1", donViParent != null ? donViParent.TenDonVi : string.Empty);
                            data.Add("TieuDe1", "Chứng từ cấp phát");
                            data.Add("TieuDe2", string.Format("Số chứng từ: {0}", item.SoChungTu));
                            data.Add("ThoiGian", string.Format("Ngày chứng từ: {0}", item.NgayChungTu.HasValue ? item.NgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty));
                            data.Add("NamLamViec", _sessionInfo.YearOfWork);
                            data.Add("Items", listAllocationDetail);
                            data.Add("MLNS", mucLucNganSaches);
                            double tongTien = (listAllocationDetail != null && listAllocationDetail.Count > 0) ? listAllocationDetail.Where(n => !n.IsHangCha).Select(n => n.TuChi).Sum() : 0;
                            data.Add("TongTienBangChu", StringUtils.NumberToText(tongTien, true));

                            fileNamePrefix = item.SoChungTu;
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<AllocationDetailModel, NsMucLucNganSach>(templateFileName, data);
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

        private void OnUpload()
        {
            try
            {
                if (!DataAllocation.Any(n => n.Selected) || DataAllocation.Where(n => n.Selected).Count() > 1)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Vui lòng chọn duy nhất 1 bản ghi !");
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }
                List<ExportResult> results = new List<ExportResult>();
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CP, ExportFileName.RPT_CAPPHAT_CHUNGTU_EXPORT);
                string fileNamePrefix;
                string fileNameWithoutExtension;
                List<AllocationModel> listExport = DataAllocation.Where(x => x.Selected).ToList();
                string sError = string.Empty;
                string sNameUnit = string.Empty;
                foreach (AllocationModel item in listExport)
                {

                    List<string> listIdDonVi = item.IdDonVi.Split(",").ToList();
                    foreach (string idDonVi in listIdDonVi)
                    {
                        DonVi donViChild = _nsDonViService.FindByIdDonVi(idDonVi, _sessionInfo.YearOfWork);
                        DonVi donViParent = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
                        List<AllocationDetailModel> listAllocationDetail = GetDetailDataExport(item, idDonVi);
                        int namLamViec = _sessionInfo.YearOfWork;
                        List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(namLamViec).ToList();
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Header2", donViChild != null ? donViChild.TenDonVi : string.Empty);
                        data.Add("Header1", donViParent != null ? donViParent.TenDonVi : string.Empty);
                        data.Add("TieuDe1", "Chứng từ cấp phát");
                        data.Add("TieuDe2", string.Format("Số chứng từ: {0}", item.SoChungTu));
                        data.Add("ThoiGian", string.Format("Ngày chứng từ: {0}", item.NgayChungTu.HasValue ? item.NgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty));
                        data.Add("NamLamViec", _sessionInfo.YearOfWork);
                        data.Add("Items", listAllocationDetail);
                        data.Add("MLNS", mucLucNganSaches);
                        double tongTien = (listAllocationDetail != null && listAllocationDetail.Count > 0) ? listAllocationDetail.Where(n => !n.IsHangCha).Select(n => n.TuChi).Sum() : 0;
                        data.Add("TongTienBangChu", StringUtils.NumberToText(tongTien, true));

                        fileNamePrefix = item.SoChungTu;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<AllocationDetailModel, NsMucLucNganSach>(templateFileName, data);
                        var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                        string filePathLocal = string.Empty;
                        string sStage = string.Empty;
                        if (DataAllocation != null)
                        {
                            sStage = StringUtils.UCS2Convert(item.TenLoaiCap) + "/" + StringUtils.UCS2Convert(item.ITypeMoTa);
                        }
                        _exportService.Open(Result, ExportType.EXCEL, ref filePathLocal);
                        string sFolderRoot = ConstantUrlPathPhanHe.UrlQlnsCpformReceive;
                        sNameUnit = idDonVi + "-" + StringUtils.UCS2Convert(donViChild.TenDonVi).Replace("---", "-");
                        var strUrl = string.Format("{0}/{1}/{2}", sNameUnit, sFolderRoot, sStage);
                        if (!File.Exists(strUrl))
                        {
                            string strActiveFileName = "";
                            string[] splitActiveFiName = xlsFile.ActiveFileName.Split("\\");
                            if (strActiveFileName != null && splitActiveFiName.Length != 0)
                            {
                                strActiveFileName = splitActiveFiName[splitActiveFiName.Length - 1];
                            }
                            VdtFtpRoot dataRoot = new VdtFtpRoot();
                            List<string> configCodes = new List<string>()
                            {
                                STORAGE_CONFIG.FTP_HOST
                            };
                            var rs = _danhMucService.FindByCodes(configCodes).ToList();
                            var SIpAddress = rs.FirstOrDefault(x => STORAGE_CONFIG.FTP_HOST.Equals(x.IIDMaDanhMuc)).SGiaTri;
                            dataRoot = _ftpService.GetVdtFtpRoot(idDonVi, SIpAddress, sFolderRoot);
                            if (dataRoot == null)
                            {
                                dataRoot = new VdtFtpRoot()
                                {
                                    SMaDonVi = idDonVi,
                                    SIpAddress = SIpAddress, // vd: ftp:\\10.60.108.246
                                    SFolderRoot = sFolderRoot,
                                    SNguoiTao = _sessionService.Current.Principal,
                                    DNgayTao = DateTime.Now
                                };
                                _ftpService.Add(dataRoot);
                            }
                            var result = _ftpStorageService.UploadCommand(dataRoot.Id, filePathLocal, strActiveFileName, strUrl);
                            if (result != 1)
                            {
                                var errorMsg = $"Gửi dữ liệu {idDonVi} thất bại\n";
                                sError += errorMsg;
                            }
                        }

                    }
                    if (string.IsNullOrEmpty(sError))
                    {
                        StringBuilder messageBuilder = new StringBuilder();
                        messageBuilder.AppendFormat("Gửi dữ liệu thành công");
                        MessageBox.Show(messageBuilder.ToString());
                    }
                    else
                    {
                        MessageBox.Show(sError);
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }
        public List<AllocationDetailModel> GetDetailDataExport(AllocationModel itemAllocation, string idDonVi)
        {
            AllocationDetailCriteria searchConditon = new AllocationDetailCriteria
            {
                VoucherId = itemAllocation.Id.ToString(),
                LNS = itemAllocation.Lns,
                YearOfWork = _sessionInfo.YearOfWork,
                YearOfBudget = _sessionInfo.YearOfBudget,
                Type = itemAllocation.ILoai,
                BudgetSource = itemAllocation.NguonNganSach.HasValue ? itemAllocation.NguonNganSach.Value : 0,
                AgencyId = idDonVi,
                VoucherDate = itemAllocation.NgayChungTu
            };
            searchConditon.PhanCap = DynamicMLNS.FormatLevel(itemAllocation.ITypeMoTa);
            searchConditon.UserName = _sessionInfo.Principal;
            List<CpChungTuChiTietQuery> data = _cpChungTuChiTietService.FindChungTuChiTietByCondition(searchConditon, false).ToList();
            ProcessSummaryDetailData(ref data);
            List<AllocationDetailModel> listAllocationDetail = _mapper.Map<List<Model.AllocationDetailModel>>(data);
            CalculateData(ref listAllocationDetail);
            listAllocationDetail = listAllocationDetail.Where(x => (x.HienVat != 0 || x.DuToan != 0 || x.DaCap != 0 || x.ConLai != 0 || x.TuChi != 0 || x.DeNghiDonVi != 0)).ToList();
            FormatDetailExport(itemAllocation, ref listAllocationDetail);
            return listAllocationDetail;
        }

        private List<NsMucLucNganSach> GetChiTietToiMLNS()
        {
            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            predicate = predicate.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.SCPChiTietToi != null && x.XauNoiMa.Length == 7);
            List<NsMucLucNganSach> data = _mucLucNganSachService.FindByCondition(predicate).ToList();
            return data;
        }

        private void ProcessSummaryDetailData(ref List<CpChungTuChiTietQuery> input)
        {
            List<NsMucLucNganSach> dataMLNS = GetChiTietToiMLNS();
            foreach (var mlns in dataMLNS)
            {
                switch (mlns.SCPChiTietToi)
                {
                    case "NG":
                        input.Where(n => n.Lns.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length <= 25).Select(n => { n.BHangCha = true; return n; }).ToList();
                        input.Where(n => n.Lns.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length > 25).Select(n => { n.BHangCha = false; return n; }).ToList();
                        break;
                    case "TM":
                        input.Where(n => n.Lns.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length <= 20).Select(n => { n.BHangCha = true; return n; }).ToList();
                        input.Where(n => n.Lns.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length > 20).Select(n => { n.BHangCha = false; return n; }).ToList();
                        break;
                    case "M":
                        input.Where(n => n.Lns.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length <= 15).Select(n => { n.BHangCha = true; return n; }).ToList();
                        input.Where(n => n.Lns.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length > 15).Select(n => { n.BHangCha = false; return n; }).ToList();
                        break;
                    default:
                        break;
                }
            }
        }

        private void FormatDetailExport(AllocationModel itemAllocation, ref List<AllocationDetailModel> listAllocationDetail)
        {
            List<CpChungTuChiTietQuery> result = new List<CpChungTuChiTietQuery>();
            string chiTietToi = DynamicMLNS.FormatLevel(itemAllocation.ITypeMoTa);
            switch (chiTietToi)
            {
                case "NG":
                    listAllocationDetail = listAllocationDetail.Where(n =>
                    (!string.IsNullOrEmpty(n.Ng) && string.IsNullOrEmpty(n.Tng)
                                                    && string.IsNullOrEmpty(n.TNG1) && string.IsNullOrEmpty(n.TNG2) && string.IsNullOrEmpty(n.TNG3))
                    || (string.IsNullOrEmpty(n.Ng) && !string.IsNullOrEmpty(n.Ttm))
                    || string.IsNullOrEmpty(n.L) || string.IsNullOrEmpty(n.K) || string.IsNullOrEmpty(n.M)
                    || string.IsNullOrEmpty(n.Tm) || string.IsNullOrEmpty(n.Ttm)).ToList();
                    listAllocationDetail.Where(n => !string.IsNullOrEmpty(n.Ng)).Select(n => { n.IsHangCha = false; return n; }).ToList();
                    listAllocationDetail.Where(n => string.IsNullOrEmpty(n.Ng)).Select(n => { n.IsHangCha = true; return n; }).ToList();
                    break;
                case "M":
                    listAllocationDetail = listAllocationDetail.Where(n =>
                    (!string.IsNullOrEmpty(n.M)
                    && string.IsNullOrEmpty(n.Tm) && string.IsNullOrEmpty(n.Ttm) && string.IsNullOrEmpty(n.Ng)
                    && string.IsNullOrEmpty(n.Tng) && string.IsNullOrEmpty(n.TNG1) && string.IsNullOrEmpty(n.TNG2) && string.IsNullOrEmpty(n.TNG3))
                    || string.IsNullOrEmpty(n.L) || string.IsNullOrEmpty(n.K) || string.IsNullOrEmpty(n.M)).ToList();
                    listAllocationDetail.Where(n => !string.IsNullOrEmpty(n.M)).Select(n => { n.IsHangCha = false; return n; }).ToList();
                    listAllocationDetail.Where(n => string.IsNullOrEmpty(n.M)).Select(n => { n.IsHangCha = true; return n; }).ToList();
                    break;
                case "TM":
                    listAllocationDetail = listAllocationDetail.Where(n =>
                    (!string.IsNullOrEmpty(n.Tm)
                    && string.IsNullOrEmpty(n.Ttm) && string.IsNullOrEmpty(n.Ng)
                    && string.IsNullOrEmpty(n.Tng) && string.IsNullOrEmpty(n.TNG1) && string.IsNullOrEmpty(n.TNG2) && string.IsNullOrEmpty(n.TNG3))
                    || string.IsNullOrEmpty(n.L) || string.IsNullOrEmpty(n.K) || string.IsNullOrEmpty(n.M)
                    || string.IsNullOrEmpty(n.Tm)).ToList();
                    listAllocationDetail.Where(n => !string.IsNullOrEmpty(n.Tm)).Select(n => { n.IsHangCha = false; return n; }).ToList();
                    listAllocationDetail.Where(n => string.IsNullOrEmpty(n.Tm)).Select(n => { n.IsHangCha = true; return n; }).ToList();
                    break;
                default:
                    break;
            }
            listAllocationDetail = listAllocationDetail.OrderBy(x => x.XauNoiMa).ToList();
        }

        private void CalculateData(ref List<AllocationDetailModel> data)
        {
            data.Where(x => x.IsHangCha)
                .Select(x => { x.TuChi = 0; x.HienVat = 0; x.DaCap = 0; x.DuToan = 0; x.DeNghiDonVi = 0; return x; }).ToList();
            foreach (var item in data.Where(x => !x.IsHangCha && !x.IsDeleted && (x.HienVat != 0 || x.DuToan != 0 || x.DaCap != 0 || x.ConLai != 0 || x.TuChi != 0 || x.DeNghiDonVi != 0)))
            {
                CalculateParent(ref data, item, item);
            }
        }

        private void CalculateParent(ref List<AllocationDetailModel> data, AllocationDetailModel currentItem, AllocationDetailModel selfItem)
        {
            var parentItem = data.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.DuToan += selfItem.DuToan;
            parentItem.DaCap += selfItem.DaCap;
            parentItem.DeNghiDonVi += selfItem.DeNghiDonVi;
            CalculateParent(ref data, parentItem, selfItem);
        }

        private void OnImportData()
        {
            try
            {
                if (_isCapPhatToanDonVi && !_sessionInfo.IsQuanLyDonViCha)
                {
                    MessageBoxHelper.Warning(Resources.AlertRoleImportAllocation);
                    return;
                }
                AllocationImportViewModel.IsAllocationReceive = false;
                AllocationImportViewModel.Init();
                AllocationImportViewModel.SavedAction = obj =>
                {
                    _importView.Close();
                    OnRefesh();
                    OnShowDetailAllocation((AllocationModel)obj);
                };
                _importView = new AllocationImport { DataContext = AllocationImportViewModel };
                _importView.Show();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
