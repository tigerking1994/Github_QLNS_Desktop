using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.ChuTruongDauTu;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.ChuTruongDauTu
{
    public class ChuTruongDauTuIndexViewModel : GridViewModelBase<ChuTruongDauTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtDaChuTruongDauTuService _vdtDaChuTruongDauTuService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IAttachmentService _iAttachmentService;
        private ICollectionView _itemsCollectionView;

        public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD_CHU_TRUONG_DAU_TU_INDEX;

        public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGEMENT;

        //public override string Name => "Quản lý phê duyệt chủ trương đầu tư";

        public override string Name => "Chủ trương đầu tư";
        public override string Title => "Chủ trương đầu tư";
        public override string Description => "Danh sách phê duyệt chủ trương đầu tư";
        public override Type ContentType => typeof(ChuTruongDauTuIndex);
        public bool IsEditable => SelectedItem != null && SelectedItem.BActive && !SelectedItem.BKhoa;
        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;

        private ChuTruongDTFilterModel _chuTruongDTFilter;
        public ChuTruongDTFilterModel ChuTruongDTFilter
        {
            get => _chuTruongDTFilter;
            set => SetProperty(ref _chuTruongDTFilter, value);
        }

        private ObservableCollection<CheckBoxItem> _itemsDonVi;
        public ObservableCollection<CheckBoxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private string _selectedDonVi;
        public string SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
            }
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }

        public ChuTruongDauTuDialogViewModel ChuTruongDauTuDialogViewModel { get; }
        public AttachmentViewModel AttachmentViewModel { get; set; }

        public ChuTruongDauTuIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtDaChuTruongDauTuService vdtDaChuTruongDauTuService,
            IApproveProjectService approveProjectService,
            IProjectManagerService projectManagerService,
            IAttachmentService iAttachmentService,
            ChuTruongDauTuDialogViewModel chuTruongDauTuDialogViewModel,
            AttachmentViewModel attachmentViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _vdtDaChuTruongDauTuService = vdtDaChuTruongDauTuService;
            _approveProjectService = approveProjectService;
            _projectManagerService = projectManagerService;
            _iAttachmentService = iAttachmentService;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(obj), obj => base.SelectedItem != null && base.SelectedItem.TotalFiles > 0);

            ChuTruongDauTuDialogViewModel = chuTruongDauTuDialogViewModel;
            AttachmentViewModel = attachmentViewModel;
        }

        public override void Init()
        {
            LoadDonVi();
            LoadData();
        }
        private void LoadDonVi()
        {
            ItemsDonVi = new ObservableCollection<CheckBoxItem>();
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindInternalByNamLamViec(_sessionService.Current.YearOfWork);
            ItemsDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonVi);
        }

        private void LoadData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                ChuTruongDTFilter = new ChuTruongDTFilterModel();
                Items = new ObservableCollection<ChuTruongDauTuModel>();
                //e.Result = _vdtDaChuTruongDauTuService.FindByCondition(_sessionService.Current.YearOfWork, _sessionService.Current.Principal);
                e.Result = _vdtDaChuTruongDauTuService.FindByConditionUserLogin(_sessionService.Current.Principal);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    Items = _mapper.Map<ObservableCollection<Model.ChuTruongDauTuModel>>(e.Result);
                    // Process when run completed. e.Result
                    if (Items != null && Items.Count > 0)
                    {
                        SelectedItem = Items.FirstOrDefault();
                    }
                    _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                    _itemsCollectionView.Filter = Items_Filter;
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private bool Items_Filter(object obj)
        {
            if (obj is ChuTruongDauTuModel item)
            {
                bool result = true;
                if (!string.IsNullOrEmpty(SelectedDonVi))
                {
                    result = !string.IsNullOrEmpty(item.IIdMaDonViQuanLy) && item.IIdMaDonViQuanLy.ToLower().Contains(SelectedDonVi.ToLower());
                }
                if (!string.IsNullOrEmpty(ChuTruongDTFilter.SoQuyetDinh))
                {
                    result = result && item.SSoQuyetDinh.ToLower().Contains(ChuTruongDTFilter.SoQuyetDinh.ToLower());
                }
                if (!string.IsNullOrEmpty(ChuTruongDTFilter.NoiDung) && !string.IsNullOrEmpty(item.STenDuAn))
                {
                    result = result && item.STenDuAn.ToLower().Contains(ChuTruongDTFilter.NoiDung.ToLower());
                }
                if (ChuTruongDTFilter.NgayQuyetDinhFrom != null)
                {
                    result = result && item.DNgayQuyetDinh.HasValue && (item.DNgayQuyetDinh.Value.Date >= ChuTruongDTFilter.NgayQuyetDinhFrom.Value.Date);
                }
                if (ChuTruongDTFilter.NgayQuyetDinhTo != null)
                {
                    result = result && item.DNgayQuyetDinh.HasValue && (item.DNgayQuyetDinh.Value.Date <= ChuTruongDTFilter.NgayQuyetDinhTo.Value.Date);
                }

                if(ChuTruongDTFilter.TMDTValue != null && ChuTruongDTFilter.TMDTValue != 0)
                {
                    result = result && item.FTmdtduKienPheDuyet.Value == ChuTruongDTFilter.TMDTValue;
                }

                if(ChuTruongDTFilter.TMDTValue == 0)
                {
                    ChuTruongDTFilter.TMDTValue = null;
                }
                //if (ChuTruongDTFilter.TMDTFrom != null)
                //{
                //    result = result && item.FTmdtduKienPheDuyet != null && (item.FTmdtduKienPheDuyet.Value >= ChuTruongDTFilter.TMDTFrom);
                //}
                //if (ChuTruongDTFilter.TMDTTo != null)
                //{
                //    result = result && item.FTmdtduKienPheDuyet != null && (item.FTmdtduKienPheDuyet.Value <= ChuTruongDTFilter.TMDTTo);
                //}
                return result;
            }
            return false;
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null || SelectedItem.IIdDuAnId == null || !IsEditable)
            {
                return;
            }
            if (SelectedItem.BKhoa)
            {
                return;
            }
            if (CheckChuTruongExistQDDauTu(SelectedItem.IIdDuAnId.Value, SelectedItem.Id))
            {
                MessageBoxHelper.Error(Resources.MsgErrDeleteChuTruongDT);
                return;
            }

            if (!CheckCanSuaXoa())
            {
                MessageBoxHelper.Error(string.Format(Resources.MsgRoleDelete, SelectedItem.SUserCreate));
                return;
            }

            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                // Nếu là xóa bản ghi điều chỉnh thì bản ghi gốc sẽ được update bactive = 1
                if (SelectedItem.IIdParentId.HasValue)
                {
                    VdtDaChuTruongDauTu chuTruongParent = _vdtDaChuTruongDauTuService.FindById(SelectedItem.IIdParentId.Value);
                    if (chuTruongParent != null)
                    {
                        chuTruongParent.BActive = true;
                        _vdtDaChuTruongDauTuService.Update(chuTruongParent);
                    }
                }
                _vdtDaChuTruongDauTuService.DeleteChuTruongDauTu(SelectedItem.Id, SelectedItem.IIdParentId);
                _iAttachmentService.DeleteByObjectIdAndModuleType(SelectedItem.Id, (int)AttachmentEnum.Type.VDT_CHUTRUONG_DAUTU);
                OnRefresh();
            }
        }

        public bool CheckChuTruongExistQDDauTu(Guid duanId, Guid iIdChuTruongId)
        {
            VdtDaQddauTu qdDauTu = _approveProjectService.FindByDuAnId(duanId);
            if (qdDauTu != null && qdDauTu.IIdChuTruongDauTuId == iIdChuTruongId)
            {
                return true;
            }
            return false;
        }


        protected override void OnAdd()
        {
            ChuTruongDauTuDialogViewModel.IsDieuChinh = false;
            ChuTruongDauTuDialogViewModel.IsNotViewDetail = true;
            ChuTruongDauTuDialogViewModel.Model = new ChuTruongDauTuModel();
            ChuTruongDauTuDialogViewModel.Init();
            ChuTruongDauTuDialogViewModel.SavedAction = obj => this.OnRefresh();
            ChuTruongDauTuDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.BKhoa)
                {
                    return;
                }
                // check phân quyền, nếu không có quyền thì ko cho sửa
                if (!CheckCanSuaXoa())
                {
                    MessageBoxHelper.Info(string.Format(Resources.MsgRoleUpdate, SelectedItem.SUserCreate));
                    return;
                }
                ChuTruongDauTuDialogViewModel.IsDieuChinh = false;
                ChuTruongDauTuDialogViewModel.IsNotViewDetail = true;
                ChuTruongDauTuDialogViewModel.Model = SelectedItem;
                ChuTruongDauTuDialogViewModel.Init();
                ChuTruongDauTuDialogViewModel.SavedAction = obj => this.OnRefresh();
                ChuTruongDauTuDialogViewModel.ShowDialog();
            }
        }

        private bool CheckCanSuaXoa()
        {
            var user = _sessionService.Current.Principal;

            if (user == SelectedItem.SUserCreate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void OnDieuChinh()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.BKhoa)
                {
                    return;
                }

                if (!CheckCanSuaXoa())
                {
                    MessageBoxHelper.Info(string.Format(Resources.MsgRoleDieuChinh, SelectedItem.SUserCreate));
                    return;
                }

                if (CheckDuAnQuyetToanHoanThanh())
                {
                    MessageBoxHelper.Info(Resources.MsgErrorDuAnDaQuyetToan);
                    return;
                }

                ChuTruongDauTuDialogViewModel.IsDieuChinh = true;
                ChuTruongDauTuDialogViewModel.IsNotViewDetail = true;
                ChuTruongDauTuDialogViewModel.Model = SelectedItem;
                ChuTruongDauTuDialogViewModel.Init();
                ChuTruongDauTuDialogViewModel.SavedAction = obj => this.OnRefresh();
                ChuTruongDauTuDialogViewModel.ShowDialog();
            }
        }

        private bool CheckDuAnQuyetToanHoanThanh()
        {
            bool isQuyetToan = _projectManagerService.CheckDuAnQuyetToanHoanThanh(SelectedItem.IIdDuAnId.Value);
            return isQuyetToan;
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void OnRemoveFilter()
        {
            ChuTruongDTFilter = new ChuTruongDTFilterModel();
            SelectedDonVi = string.Empty;
            _itemsCollectionView.Refresh();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (SelectedItem != null)
            {
                ChuTruongDauTuDialogViewModel.IsDieuChinh = false;
                ChuTruongDauTuDialogViewModel.IsNotViewDetail = false;
                ChuTruongDauTuDialogViewModel.Model = SelectedItem;
                ChuTruongDauTuDialogViewModel.Init();
                ChuTruongDauTuDialogViewModel.ShowDialog();
            }
        }

        private void OnViewAttachment(object obj)
        {
            if (base.SelectedItem != null)
            {
                AttachmentViewModel.ModuleType = AttachmentEnum.Type.VDT_CHUTRUONG_DAUTU;
                AttachmentViewModel.ObjectId = base.SelectedItem.Id;
                AttachmentViewModel.Init();
                AttachmentViewModel.ShowDialogHost();
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEditable));
        }

        public override void Dispose()
        {
            base.Dispose();

            // Clear data
            _itemsCollectionView = null;
            Items = new ObservableCollection<ChuTruongDauTuModel>();
            ItemsDonVi = new ObservableCollection<CheckBoxItem>();
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
                        MessageBoxHelper.Error(Resources.MsgRoleUnlock);
                        return;
                    }
                }
                else
                {
                    if (SelectedItem.SUserCreate != _sessionService.Current.Principal)
                    {
                        MessageBoxHelper.Error(string.Format(Resources.MsgRoleLock, SelectedItem.SUserCreate));
                        return;
                    }

                    if (!SelectedItem.BActive)
                    {
                        MessageBoxHelper.Error(string.Format(Resources.VoucherLockModified, SelectedItem.SUserCreate));
                        return;
                    }
                }

                string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
                if (MessageBoxHelper.Confirm(message) == MessageBoxResult.Yes)
                    LockConfirmEventHandler();
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
                _vdtDaChuTruongDauTuService.LockOrUnlock(SelectedItem.Id, !SelectedItem.BKhoa);
                SelectedItem.BKhoa = !SelectedItem.BKhoa;

                OnPropertyChanged(nameof(IsLock));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
