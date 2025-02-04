using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PheDuyetDuAn
{
    public class PheDuyetDuAnIndexViewModel : GridViewModelBase<ApproveProjectModel>
    {
        #region Private
        private IApproveProjectService _approveProjectService;
        private IVdtDaDuToanService _duToanService;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private INsDonViService _nsDonViService;
        private ICollectionView _dataApproveProjectFilter;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IVdtDaChuTruongDauTuService _chutruongService;
        private readonly IAttachmentService _attachService;
        private readonly ILog _logger;
        #endregion

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_PHE_DUYET_DU_AN_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGER;

        public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGEMENT;

        //public override string Name => "Quản lý phê duyệt dự án";
        public override string Name => "Phê duyệt dự án";
        public override string Title => "Phê duyệt dự án";
        public override string Description => "Danh sách phê duyệt dự án";
        public override Type ContentType => typeof(View.Investment.MediumTermPlan.PheDuyetDuAn.PheDuyetDuAnIndex);

        #region Items
        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa == true;
        public bool IsEdit => SelectedItem != null && !SelectedItem.IsLocked;
        public bool IsEnableLock => SelectedItem != null;
        public bool IsEditable => SelectedItem != null && (SelectedItem.BActive ?? false) && SelectedItem.BKhoa == false;

        private ApproveProjectFilterModel _projectFilter;
        public ApproveProjectFilterModel ProjectFilter
        {
            get => _projectFilter;
            set => SetProperty(ref _projectFilter, value);
        }

        private ObservableCollection<CheckBoxItem> _dataDonVi;
        public ObservableCollection<CheckBoxItem> DataDonVi
        {
            get => _dataDonVi;
            set => SetProperty(ref _dataDonVi, value);
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
        #endregion

        #region View
        public PheDuyetDuAnDialogViewModel PheDuyetDuAnDialogViewModel { get; }
        public PheDuyetDuAnDetailViewModel PheDuyetDuAnDetailViewModel { get; }
        public PheDuyetDuAnDieuChinhDialogViewModel PheDuyetDuAnDieuChinhDialogViewModel { get; }
        public PheDuyetDuAnDieuChinhDetailViewModel PheDuyetDuAnDieuChinhDetailViewModel { get; }
        public PheDuyetDuAnHangMucAllDeTailViewModel pheDuyetDuAnHangMucAllDeTailViewModel { get; }
        public AttachmentViewModel AttachmentViewModel { get; set; }
        #endregion

        #region RelayCommand
        public RelayCommand ShowPopupDieuChinhCommand { get; }
        public RelayCommand ShowPopupDieuChinhChiTietCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }
        #endregion

        public PheDuyetDuAnIndexViewModel(
            ILog logger,
            IApproveProjectService approveProjectService,
            IVdtDaDuToanService duToanService,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IProjectManagerService projectManagerService,
            IVdtDaChuTruongDauTuService chuTruongDauTuService,
            IAttachmentService attachService,
            PheDuyetDuAnDialogViewModel pheDuyetDuAnDialogViewModel,
            PheDuyetDuAnDetailViewModel pheDuyetDuAnDetailViewModel,
            PheDuyetDuAnDieuChinhDialogViewModel pheDuyetDuAnDieuChinhDialogViewModel,
            PheDuyetDuAnDieuChinhDetailViewModel pheDuyetDuAnDieuChinhDetailViewModel,
            PheDuyetDuAnHangMucAllDeTailViewModel pdDuAnHangMucAllDeTailViewModel,
            AttachmentViewModel attachmentViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _approveProjectService = approveProjectService;
            _duToanService = duToanService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _projectManagerService = projectManagerService;
            _chutruongService = chuTruongDauTuService;
            _attachService = attachService;

            ShowPopupDieuChinhCommand = new RelayCommand(o => OnShowPopupDieuChinh(), obj => IsEditable);
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            SearchCommand = new RelayCommand(obj => _dataApproveProjectFilter.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(obj), obj => base.SelectedItem != null && base.SelectedItem.TotalFiles > 0);

            PheDuyetDuAnDialogViewModel = pheDuyetDuAnDialogViewModel;
            PheDuyetDuAnDetailViewModel = pheDuyetDuAnDetailViewModel;
            PheDuyetDuAnDieuChinhDialogViewModel = pheDuyetDuAnDieuChinhDialogViewModel;
            PheDuyetDuAnDieuChinhDetailViewModel = pheDuyetDuAnDieuChinhDetailViewModel;
            pheDuyetDuAnHangMucAllDeTailViewModel = pdDuAnHangMucAllDeTailViewModel;
            AttachmentViewModel = attachmentViewModel;
        }

        protected override void OnAdd()
        {
            PheDuyetDuAnDialogViewModel.Model = new ApproveProjectModel();
            PheDuyetDuAnDialogViewModel.IsNotViewDetail = true;
            PheDuyetDuAnDialogViewModel.Init();
            PheDuyetDuAnDialogViewModel.SavedAction = obj => this.OnRefresh();
            PheDuyetDuAnDialogViewModel.ShowDialog();
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
                    PheDuyetDuAnDialogViewModel.Model = SelectedItem;
                    PheDuyetDuAnDialogViewModel.IsNotViewDetail = true;
                    PheDuyetDuAnDialogViewModel.Init();
                    PheDuyetDuAnDialogViewModel.SavedAction = obj => this.OnRefresh();
                    PheDuyetDuAnDialogViewModel.ShowDialog();
                }
                else
                {
                    if (!CheckCanSuaXoa())
                    {
                        MessageBoxHelper.Error(string.Format(Resources.MsgRoleDieuChinh, SelectedItem.SUserCreate));
                        return;
                    }
                    PheDuyetDuAnDieuChinhDialogViewModel.Model = SelectedItem;
                    PheDuyetDuAnDieuChinhDialogViewModel.IsAddDieuChinh = false;
                    PheDuyetDuAnDieuChinhDialogViewModel.IsDieuChinh = false;
                    PheDuyetDuAnDieuChinhDialogViewModel.QDDauTu = SelectedItem;
                    PheDuyetDuAnDieuChinhDialogViewModel.IsNotViewDetail = true;
                    PheDuyetDuAnDieuChinhDialogViewModel.Init();
                    PheDuyetDuAnDieuChinhDialogViewModel.SavedAction = obj => this.OnRefresh();
                    PheDuyetDuAnDieuChinhDialogViewModel.ShowDialog();
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
                Guid? iIdChuTruong = null;
                if (CheckCTDauTuDieuChinh(ref iIdChuTruong))
                {
                    PheDuyetDuAnDialogViewModel.Model = SelectedItem;
                    PheDuyetDuAnDialogViewModel.Model.IIdChuTruongDauTuId = iIdChuTruong;
                    PheDuyetDuAnDialogViewModel.IsNotViewDetail = true;
                    PheDuyetDuAnDialogViewModel.IsDieuChinhPDDA = true;
                    PheDuyetDuAnDialogViewModel.Init();
                    PheDuyetDuAnDialogViewModel.SavedAction = obj => this.OnRefresh();
                    PheDuyetDuAnDialogViewModel.ShowDialog();
                    PheDuyetDuAnDialogViewModel.IsDieuChinhPDDA = false;
                    this.OnRefresh();
                }
                else
                {
                    PheDuyetDuAnDieuChinhDialogViewModel.Model = SelectedItem;
                    PheDuyetDuAnDieuChinhDialogViewModel.Model.IIdChuTruongDauTuId = iIdChuTruong;
                    PheDuyetDuAnDieuChinhDialogViewModel.IsAddDieuChinh = true;
                    PheDuyetDuAnDieuChinhDialogViewModel.IsNotViewDetail = true;
                    PheDuyetDuAnDieuChinhDialogViewModel.IsDieuChinh = true;
                    PheDuyetDuAnDieuChinhDialogViewModel.Init();
                    PheDuyetDuAnDieuChinhDialogViewModel.SavedAction = obj => this.OnRefresh();
                    PheDuyetDuAnDieuChinhDialogViewModel.ShowDialog();
                    PheDuyetDuAnDialogViewModel.IsDieuChinhPDDA = false;
                    this.OnRefresh();
                }
            }
        }

        // tìm xem nếu CTDT có bản ghi điều chỉnh khác bản ghi đã lưu dữ liệu không, nếu có bản ghi điều chỉnh khác thì lấy bản ghi đó làm căn cứ điều chỉnh PDDA.
        private bool CheckCTDauTuDieuChinh(ref Guid? iIdChuTruong)
        {
            if (SelectedItem == null || SelectedItem.IIdChuTruongDauTuId == null)
            {
                return false;
            }
            // tìm CTDT điều chỉnh mới nhất khác với CTDT được tạo trong dự toán.
            VdtDaChuTruongDauTu CTDTDieuChinh = _chutruongService.FindCTDTDieuChinhByDuAn(SelectedItem.IIdChuTruongDauTuId.Value, SelectedItem.IIdDuAnId.Value);
            if (CTDTDieuChinh != null)
            {
                iIdChuTruong = CTDTDieuChinh.Id;
                return true;
            }
            else
            {
                iIdChuTruong = SelectedItem.IIdChuTruongDauTuId;
                return false;
            }
        }

        public override void Init()
        {
            ProjectFilter = new ApproveProjectFilterModel();
            LoadDonVi();
            LoadData();
            PheDuyetDuAnDetailViewModel.ClosePopup += RefreshAfterClosePopup;
        }

        private void LoadDonVi()
        {
            DataDonVi = new ObservableCollection<CheckBoxItem>();

            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => LoaiDonVi.ROOT.Equals(x.Loai) || LoaiDonVi.NOI_BO.Equals(x.Loai));
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindByCondition(predicate);
            DataDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonVi);
        }

        private void LoadData()
        {
            IEnumerable<ApproveProjectQuery> data = _approveProjectService.FindByCondition
                (_sessionService.Current.YearOfWork, _sessionService.Current.Budget, _sessionService.Current.IdDonVi);
            Items = _mapper.Map<ObservableCollection<Model.ApproveProjectModel>>(data);
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
            _dataApproveProjectFilter = CollectionViewSource.GetDefaultView(Items);
            _dataApproveProjectFilter.Filter = ApproveProjectFilter;
        }

        private void OnRemoveFilter()
        {
            ProjectFilter.SoQuyetDinh = string.Empty;
            ProjectFilter.DuAn = string.Empty;
            ProjectFilter.NgayQuyetDinhFrom = null;
            ProjectFilter.NgayQuyetDinhTo = null;
            SelectedDonVi = string.Empty;
            LoadData();
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private bool ApproveProjectFilter(object obj)
        {
            bool result = true;
            var item = (ApproveProjectModel)obj;

            if (!string.IsNullOrEmpty(SelectedDonVi))
            {
                result = result && !string.IsNullOrEmpty(item.Id_DonVi) && item.Id_DonVi.ToLower().Contains(SelectedDonVi.ToLower());
            }

            if (!string.IsNullOrEmpty(ProjectFilter.SoQuyetDinh))
                result = result && !string.IsNullOrEmpty(item.SSoQuyetDinh) && item.SSoQuyetDinh.ToLower().Contains(ProjectFilter.SoQuyetDinh.ToLower());
            if (!string.IsNullOrEmpty(ProjectFilter.DuAn))
                result = result && !string.IsNullOrEmpty(item.STenDuAn) && item.STenDuAn.ToLower().Contains(ProjectFilter.DuAn.ToLower());
            if (ProjectFilter.NgayQuyetDinhFrom != null)
                result = result && item.DNgayQuyetDinh.HasValue && (item.DNgayQuyetDinh.Value.Date >= ProjectFilter.NgayQuyetDinhFrom.Value.Date);
            if (ProjectFilter.NgayQuyetDinhTo != null)
                result = result && item.DNgayQuyetDinh.HasValue && (item.DNgayQuyetDinh.Value.Date <= ProjectFilter.NgayQuyetDinhTo.Value.Date);
            return result;
        }

        public void OnShowDetailApproveProject(ApproveProjectModel PheDuyetDuAnDetail)
        {
            if (PheDuyetDuAnDetail == null)
                return;
            PheDuyetDuAnDetailViewModel.Model = PheDuyetDuAnDetail;
            PheDuyetDuAnDetailViewModel.Init();
            PheDuyetDuAnDetailViewModel.ShowDialog();
        }

        private void RefreshAfterClosePopup(object sender, EventArgs e)
        {
            OnRefresh();
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
            if (_duToanService.CheckQDDTExistTKTCTDT(SelectedItem.Id))
            {
                MessageBoxHelper.Error(Resources.MsgErrorDuAnExistInTktc);
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
                if (SelectedItem.IIdParentId != null)
                {
                    VdtDaQddauTu qdDauTuParent = _approveProjectService.FindById(SelectedItem.IIdParentId.Value);
                    if (qdDauTuParent != null)
                    {
                        qdDauTuParent.BActive = true;
                        _approveProjectService.Update(qdDauTuParent);
                    }
                }
                _approveProjectService.DeleteQDDauTuChiTiet(SelectedItem.Id, SelectedItem.IIdParentId);
                DeleteAttachment(SelectedItem.Id);
                OnRefresh();
            }
        }

        private void DeleteAttachment(Guid idPdda)
        {
            _attachService.DeleteByObjectIdAndModuleType(idPdda, (int)AttachmentEnum.Type.VDT_PHEDUYET_DUAN);
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (SelectedItem != null)
            {
                if (SelectedItem != null)
                {
                    if (SelectedItem.BIsGoc.Value == true)
                    {
                        PheDuyetDuAnDialogViewModel.Model = SelectedItem;
                        PheDuyetDuAnDialogViewModel.IsNotViewDetail = false;
                        PheDuyetDuAnDialogViewModel.Init();
                        PheDuyetDuAnDialogViewModel.ShowDialog();
                    }
                    else
                    {
                        PheDuyetDuAnDieuChinhDialogViewModel.Model = SelectedItem;
                        PheDuyetDuAnDieuChinhDialogViewModel.QDDauTu = SelectedItem;
                        PheDuyetDuAnDieuChinhDialogViewModel.IsAddDieuChinh = false;
                        PheDuyetDuAnDieuChinhDialogViewModel.IsNotViewDetail = false;
                        PheDuyetDuAnDieuChinhDialogViewModel.Init();
                        PheDuyetDuAnDieuChinhDialogViewModel.ShowDialog();
                    }
                }
            }
        }

        private void OnViewAttachment(object obj)
        {
            if (base.SelectedItem != null)
            {
                AttachmentViewModel.ModuleType = AttachmentEnum.Type.VDT_PHEDUYET_DUAN;
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
                _approveProjectService.LockOrUnlock(SelectedItem.Id, !SelectedItem.BKhoa);
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

