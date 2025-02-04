using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.TKTCVaTongDuToan;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.TKTCVaTongDuToan
{
    public class TKTCVaTongDuToanIndexViewModel : GridViewModelBase<VdtDuToanModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IVdtQddtKhlcnhaThauService _khlcnhaThauService;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IAttachmentService _iAttachmentService;
        private readonly IDmChuDauTuService _chuDauTuService;
        private ICollectionView _dataDuToanFilter;

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_TKTC_VA_TONG_DU_TOAN_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGER;
        public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGEMENT;
        //public override string Name => "Thiết kế thi công và tổng dự toán";

        public override string Name => "Thiết kế thi công và tổng dự toán";
        public override string Title => "Quản lý phê duyệt thiết kế thi công và tổng dự toán";
        public override string Description => "Danh sách phê duyệt thiết kế thi công và tổng dự toán";
        public override Type ContentType => typeof(TKTCVaTongDuToanIndex);
        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;
        public bool IsEdit => SelectedItem != null;
        public bool IsEditable => SelectedItem != null && SelectedItem.BActive.Value && !SelectedItem.BKhoa;

        private VdtDuToanFilterModel _vdtDuToanFilter;
        public VdtDuToanFilterModel ItemsFilter
        {
            get => _vdtDuToanFilter;
            set => SetProperty(ref _vdtDuToanFilter, value);
        }

        private ObservableCollection<ComboboxItem> _dataDonVi;
        public ObservableCollection<ComboboxItem> DataDonVi
        {
            get => _dataDonVi;
            set => SetProperty(ref _dataDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
            }
        }

        public TKTCVaTongDuToanDialogViewModel VdtDaDuToanDialogViewModel { get; }
        public TKTCVaTongDuToanDetailViewModel TKTCVaTongDuToanDetailViewModel { get; }
        public TKTCVaTongDuToanHangMucAllDetailViewModel tKTCVaTongDuToanHangMucAllDetailViewModel { get; }
        public TKTCVaTongDuToanDieuChinhDialogViewModel TKTCVaTongDuToanDieuChinhDialogViewModel { get; }
        public AttachmentViewModel AttachmentViewModel { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand ShowPopupDieuChinhCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }

        public TKTCVaTongDuToanIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtDaDuToanService vdtDaDuToanService,
            IVdtQddtKhlcnhaThauService khlcnhaThauService,
            IProjectManagerService projectManagerService,
            IApproveProjectService approveProjectService,
            IAttachmentService iAttachmentService,
            IDmChuDauTuService chuDauTuService,
            TKTCVaTongDuToanDialogViewModel vdtDaDuToanDialogViewModel,
            TKTCVaTongDuToanDetailViewModel tKTCVaTongDuToanDetailViewModel,
            TKTCVaTongDuToanHangMucAllDetailViewModel tKTCHangMucAllDetailViewModel,
            TKTCVaTongDuToanDieuChinhDialogViewModel tKTCVaTongDuToanDieuChinhDialogViewModel,
            AttachmentViewModel attachmentViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _khlcnhaThauService = khlcnhaThauService;
            _projectManagerService = projectManagerService;
            _approveProjectService = approveProjectService;
            _iAttachmentService = iAttachmentService;
            _chuDauTuService = chuDauTuService;

            SearchCommand = new RelayCommand(obj => _dataDuToanFilter.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            ShowPopupDieuChinhCommand = new RelayCommand(o => OnShowPopupDieuChinh(), obj => IsEditable);
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(obj), obj => base.SelectedItem != null && base.SelectedItem.TotalFiles > 0);

            VdtDaDuToanDialogViewModel = vdtDaDuToanDialogViewModel;
            TKTCVaTongDuToanDetailViewModel = tKTCVaTongDuToanDetailViewModel;
            tKTCVaTongDuToanHangMucAllDetailViewModel = tKTCHangMucAllDetailViewModel;
            TKTCVaTongDuToanDieuChinhDialogViewModel = tKTCVaTongDuToanDieuChinhDialogViewModel;
            AttachmentViewModel = attachmentViewModel;
        }

        public override void Init()
        {
            ItemsFilter = new VdtDuToanFilterModel();
            LoadDonVi();
            LoadData();
        }

        private void LoadDonVi()
        {
            DataDonVi = new ObservableCollection<ComboboxItem>();
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            DataDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi);
        }

        private void LoadData()
        {
            IEnumerable<VdtDaDuToanQuery> data = _vdtDaDuToanService.FindByCondition(_sessionService.Current.YearOfWork);
            Items = _mapper.Map<ObservableCollection<Model.VdtDuToanModel>>(data);
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
            _dataDuToanFilter = CollectionViewSource.GetDefaultView(Items);
            _dataDuToanFilter.Filter = VdtDuToanFilterMethod;
        }

        private bool VdtDuToanFilterMethod(object obj)
        {
            bool result = true;
            var item = (VdtDuToanModel)obj;

            if (SelectedDonVi != null && !string.IsNullOrEmpty(item.IIdMaDonViQuanLy))
            {
                result = result && item.IIdMaDonViQuanLy.Contains(SelectedDonVi.ValueItem);
            }

            if (!string.IsNullOrEmpty(ItemsFilter.SoQuyetDinh))
                result = result && item.SSoQuyetDinh.ToLower().Contains(ItemsFilter.SoQuyetDinh.ToLower());
            if (!string.IsNullOrEmpty(ItemsFilter.DuAn))
                result = result && item.STenDuAn.ToLower().Contains(ItemsFilter.DuAn.ToLower());
            if (ItemsFilter.NgayQuyetDinhFrom != null)
                result = result && (item.DNgayQuyetDinh.Value.Date >= ItemsFilter.NgayQuyetDinhFrom.Value.Date);
            if (ItemsFilter.NgayQuyetDinhTo != null)
                result = result && (item.DNgayQuyetDinh.Value.Date <= ItemsFilter.NgayQuyetDinhTo.Value.Date);
            return result;
        }

        private void OnRemoveFilter()
        {
            ItemsFilter.SoQuyetDinh = string.Empty;
            ItemsFilter.DuAn = string.Empty;
            ItemsFilter.NgayQuyetDinhFrom = null;
            ItemsFilter.NgayQuyetDinhTo = null;
            SelectedDonVi = null;
            LoadData();
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnDelete()
        {
            if (!IsEditable)
            {
                return;
            }
            if (SelectedItem.BKhoa != null && SelectedItem.BKhoa == true)
            {
                return;
            }
            if (_khlcnhaThauService.CheckDuAnkExistKHLCNT(SelectedItem.Id))
            {
                MessageBoxHelper.Error(Resources.MsgErrorDuAnExistInKHLCNT);
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
                if (SelectedItem.IIdParentId.HasValue)
                {
                    VdtDaDuToan duToanParent = _vdtDaDuToanService.FindById(SelectedItem.IIdParentId.Value);
                    if (duToanParent != null)
                    {
                        duToanParent.BActive = true;
                        _vdtDaDuToanService.Update(duToanParent);
                    }
                }

                _vdtDaDuToanService.DeleteDuToanChiTiet(SelectedItem.Id);
                _iAttachmentService.DeleteByObjectIdAndModuleType(SelectedItem.Id, (int) AttachmentEnum.Type.VDT_TKTC_TONGDUTOAN);
                OnRefresh();
            }
        }

        protected override void OnAdd()
        {
            VdtDaDuToanDialogViewModel.Model = new Model.VdtDuToanModel();
            VdtDaDuToanDialogViewModel.IsNotViewDetail = true;
            VdtDaDuToanDialogViewModel.IsDieuChinh = false;
            VdtDaDuToanDialogViewModel.Init();
            VdtDaDuToanDialogViewModel.SavedAction = obj => this.OnRefresh();
            VdtDaDuToanDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.BKhoa == true)
                {
                    return;
                }

                if (SelectedItem.BIsGoc.Value == true)
                {
                    if (!CheckCanSuaXoa())
                    {
                        MessageBoxHelper.Error(string.Format(Resources.MsgRoleUpdate, SelectedItem.SUserCreate));
                        return;
                    }
                    this.VdtDaDuToanDialogViewModel.Model = SelectedItem;
                    VdtDaDuToanDialogViewModel.IsNotViewDetail = true;
                    VdtDaDuToanDialogViewModel.Init();
                    VdtDaDuToanDialogViewModel.SavedAction = obj => this.OnRefresh();
                    VdtDaDuToanDialogViewModel.ShowDialog();
                }
                else
                {
                    if (!CheckCanSuaXoa())
                    {
                        MessageBoxHelper.Error(string.Format(Resources.MsgRoleDieuChinh, SelectedItem.SUserCreate));
                        return;
                    }
                    TKTCVaTongDuToanDieuChinhDialogViewModel.Model = SelectedItem;
                    TKTCVaTongDuToanDieuChinhDialogViewModel.Model.sTenChuDauTu = GetTenChuDauTuByDuAn(SelectedItem.IIdDuAnId??Guid.Empty);
                    TKTCVaTongDuToanDieuChinhDialogViewModel.DuToan = SelectedItem;
                    
                    TKTCVaTongDuToanDieuChinhDialogViewModel.IsAddDieuChinh = false;
                    TKTCVaTongDuToanDieuChinhDialogViewModel.IsNotViewDetail = true;
                    TKTCVaTongDuToanDieuChinhDialogViewModel.Init();
                    TKTCVaTongDuToanDieuChinhDialogViewModel.SavedAction = obj => this.OnRefresh();
                    TKTCVaTongDuToanDieuChinhDialogViewModel.ShowDialog();
                }
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

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.BIsGoc.Value == true)
                {
                    VdtDaDuToanDialogViewModel.Model = SelectedItem;
                    VdtDaDuToanDialogViewModel.IsNotViewDetail = false;
                    VdtDaDuToanDialogViewModel.IsDieuChinhTKTC = false;
                    VdtDaDuToanDialogViewModel.Init();
                    VdtDaDuToanDialogViewModel.ShowDialog();
                }
                else
                {
                    TKTCVaTongDuToanDieuChinhDialogViewModel.Model = SelectedItem;
                    TKTCVaTongDuToanDieuChinhDialogViewModel.Model.sTenChuDauTu = GetTenChuDauTuByDuAn(SelectedItem.IIdDuAnId??Guid.Empty);
                    TKTCVaTongDuToanDieuChinhDialogViewModel.DuToan = SelectedItem;
                    TKTCVaTongDuToanDieuChinhDialogViewModel.IsAddDieuChinh = false;
                    TKTCVaTongDuToanDieuChinhDialogViewModel.IsNotViewDetail = false;
                    TKTCVaTongDuToanDieuChinhDialogViewModel.Init();
                    TKTCVaTongDuToanDieuChinhDialogViewModel.SavedAction = obj => this.OnRefresh();
                    TKTCVaTongDuToanDieuChinhDialogViewModel.ShowDialog();
                }
            }

        }

        private void OnShowPopupDieuChinh()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.BKhoa == true)
                {
                    return;
                }
                if (!CheckCanSuaXoa())
                {
                    MessageBoxHelper.Error(string.Format(Resources.MsgRoleDieuChinh, SelectedItem.SUserCreate));
                    return;
                }

                if (_projectManagerService.CheckDuAnQuyetToanHoanThanh(SelectedItem.IIdDuAnId.Value))
                {
                    MessageBoxHelper.Error(Resources.MsgErrorDuAnDaQuyetToan);
                    return;
                }
                Guid? iIdQuyetDinhDauTu = null;
                if (CheckQDDauTuDieuChinh(ref iIdQuyetDinhDauTu))
                {
                    VdtDaDuToanDialogViewModel.Model = SelectedItem;
                    VdtDaDuToanDialogViewModel.Model.IIdQddauTuId = iIdQuyetDinhDauTu;
                    VdtDaDuToanDialogViewModel.IsNotViewDetail = true;
                    VdtDaDuToanDialogViewModel.IsDieuChinhTKTC = true;
                    VdtDaDuToanDialogViewModel.Init();
                    VdtDaDuToanDialogViewModel.SavedAction = obj => this.OnRefresh();
                    VdtDaDuToanDialogViewModel.ShowDialog();
                    VdtDaDuToanDialogViewModel.IsDieuChinhTKTC = false;
                    this.OnRefresh();
                }
                else
                {
                    TKTCVaTongDuToanDieuChinhDialogViewModel.Model = SelectedItem;
                    TKTCVaTongDuToanDieuChinhDialogViewModel.Model.sTenChuDauTu = GetTenChuDauTuByDuAn(SelectedItem.IIdDuAnId ?? Guid.Empty);
                    TKTCVaTongDuToanDieuChinhDialogViewModel.Model.IIdQddauTuId = iIdQuyetDinhDauTu;
                    TKTCVaTongDuToanDieuChinhDialogViewModel.IsAddDieuChinh = true;
                    TKTCVaTongDuToanDieuChinhDialogViewModel.IsNotViewDetail = true;
                    TKTCVaTongDuToanDieuChinhDialogViewModel.Init();
                    TKTCVaTongDuToanDieuChinhDialogViewModel.SavedAction = obj => this.OnRefresh();
                    TKTCVaTongDuToanDieuChinhDialogViewModel.ShowDialog();
                    this.OnRefresh();
                }

            }
        }

        // tìm xem nếu QDDT có bản ghi điều chỉnh khác bản ghi đã lưu dữ liệu không, nếu có bản ghi điều chỉnh khác thì lấy bản ghi đó làm căn cứ điều chỉnh TKTC.
        private bool CheckQDDauTuDieuChinh(ref Guid? iIDQuyetDinhDauTu)
        {
            if (SelectedItem == null || SelectedItem.IIdQddauTuId == null)
            {
                return false;
            }
            // tìm QDDT điều chỉnh mới nhất khác với QDDT được tạo trong dự toán.
            VdtDaQddauTu qdDauTuDieuChinh = _approveProjectService.FindQDDaTuDieuChinhByDuAn(SelectedItem.IIdQddauTuId.Value, SelectedItem.IIdDuAnId.Value);
            if (qdDauTuDieuChinh != null)
            {
                iIDQuyetDinhDauTu = qdDauTuDieuChinh.Id;
                return true;
            }
            else
            {
                iIDQuyetDinhDauTu = SelectedItem.IIdQddauTuId;
                return false;
            }
        }

        private void RefreshAfterClosePopup(object sender, EventArgs e)
        {
            OnRefresh();
        }

        public void OnShowDetailDuToan(VdtDuToanModel duToanDetail)
        {
            if (duToanDetail == null)
                return;
            TKTCVaTongDuToanDetailViewModel.Model = duToanDetail;
            TKTCVaTongDuToanDetailViewModel.Init();
            TKTCVaTongDuToanDetailViewModel.ShowDialog();
        }

        private void OnViewAttachment(object obj)
        {
            if (base.SelectedItem != null)
            {
                AttachmentViewModel.ModuleType = AttachmentEnum.Type.VDT_TKTC_TONGDUTOAN;
                AttachmentViewModel.ObjectId = base.SelectedItem.Id;
                AttachmentViewModel.Init();
                AttachmentViewModel.ShowDialogHost();
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEditable));
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

                    if (SelectedItem.BActive != true)
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
                _vdtDaDuToanService.LockOrUnlock(SelectedItem.Id, !SelectedItem.BKhoa);
                SelectedItem.BKhoa = !SelectedItem.BKhoa;

                OnPropertyChanged(nameof(IsLock));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        private string GetTenChuDauTuByDuAn(Guid idDuAn)
        {
            IEnumerable<DmChuDauTu> listChuDauTu = _chuDauTuService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            var duAn = _projectManagerService.FindById(idDuAn);
            var chuDauTu = listChuDauTu.FirstOrDefault(x => x.Id == duAn.IIdChuDauTuId);
            return chuDauTu?.STenDonVi ?? String.Empty;
        }
    }
}
