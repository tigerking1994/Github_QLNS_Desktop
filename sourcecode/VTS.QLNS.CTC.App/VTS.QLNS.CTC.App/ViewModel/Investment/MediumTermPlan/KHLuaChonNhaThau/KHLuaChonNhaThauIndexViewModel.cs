using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
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

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.KHLuaChonNhaThau
{
    public class KHLuaChonNhaThauIndexViewModel : GridViewModelBase<KHLCNhaThauModel>
    {
        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_KH_LUA_CHON_NHA_THAU_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGER;
        public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGEMENT;
        public override string Name => "Kế hoạch lựa chọn nhà thầu";
        public override string Description => "Danh sách thông tin kế hoạch lựa chọn nhà thầu";
        public bool IsEdit => SelectedItem != null && SelectedItem.Id != Guid.Empty && SelectedItem.BActive && !SelectedItem.BKhoa;
        public override Type ContentType => typeof(View.Investment.MediumTermPlan.KHLuaChonNhaThau.KHLuaChonNhaThauIndex);

        #region Private
        private readonly IVdtQddtKhlcnhaThauService _service;
        private readonly IVdtDaGoiThauService _goithauService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtDaDuToanService _duToanService;
        private readonly IAttachmentService _iAttachmentService;
        private ICollectionView _luaChonNhaThauView;
        private IMapper _mapper;
        private readonly ILog _logger;
        #endregion

        #region Componer
        private bool _bIsShowDetail;
        public bool BIsShowDetail
        {
            get => _bIsShowDetail;
            set => SetProperty(ref _bIsShowDetail, value);
        }

        private DateTime? _dNgayQuyetDinhFrom;
        public DateTime? DNgayQuyetDinhFrom
        {
            get => _dNgayQuyetDinhFrom;
            set
            {
                SetProperty(ref _dNgayQuyetDinhFrom, value);
                //OnSearch();
            }
        }

        private DateTime? _dNgayQuyetDinhTo;
        public DateTime? DNgayQuyetDinhTo
        {
            get => _dNgayQuyetDinhTo;
            set
            {
                SetProperty(ref _dNgayQuyetDinhTo, value);
                //OnSearch();
            }
        }

        private string _soQuyetDinh;
        public string SoQuyetDinh
        {
            get => _soQuyetDinh;
            set
            {
                SetProperty(ref _soQuyetDinh, value);
                //OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _drpDonViQuanLy;
        public ObservableCollection<ComboboxItem> DrpDonViQuanLy
        {
            get => _drpDonViQuanLy;
            set => SetProperty(ref _drpDonViQuanLy, value);
        }

        private ComboboxItem _drpDonViQuanLySelected;
        public ComboboxItem DrpDonViQuanLySelected
        {
            get => _drpDonViQuanLySelected;
            set
            {
                SetProperty(ref _drpDonViQuanLySelected, value);
                //OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDuAn;
        public ObservableCollection<ComboboxItem> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }

        private ComboboxItem _selectedDuAn;
        public ComboboxItem SelectedDuAn
        {
            get => _selectedDuAn;
            set => SetProperty(ref _selectedDuAn, value);
        }

        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa == true;
        #endregion

        public KHLuaChonNhaThauDialogViewModel KHLuaChonNhaThauDialogViewModel { get; set; }
        public AttachmentViewModel AttachmentViewModel { get; set; }

        #region declare RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand ShowPopupDieuChinhCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }
        #endregion

        public KHLuaChonNhaThauIndexViewModel(
            ILog logger,
            IMapper mapper,
            IVdtQddtKhlcnhaThauService service,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtDaDuToanService duToanService,
            IAttachmentService iAttachmentService,
            IVdtDaGoiThauService goithauService,
            KHLuaChonNhaThauDialogViewModel kHLuaChonNhaThauDialogViewModel,
            AttachmentViewModel attachmentViewModel)
        {
            _logger = logger;
            _service = service;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _duToanService = duToanService;
            _iAttachmentService = iAttachmentService;
            _goithauService = goithauService;
            _mapper = mapper;

            KHLuaChonNhaThauDialogViewModel = kHLuaChonNhaThauDialogViewModel;
            kHLuaChonNhaThauDialogViewModel.ParentPage = this;
            AttachmentViewModel = attachmentViewModel;

            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => onResetFilter());
            ShowPopupDieuChinhCommand = new RelayCommand(obj => OnDieuChinh());
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(obj), obj => base.SelectedItem != null && base.SelectedItem.ITotalFiles > 0);
        }

        #region RelayCommand Event
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            //GetDonViQuanLy();
            SoQuyetDinh = null;
            DNgayQuyetDinhFrom = null;
            DNgayQuyetDinhTo = null;
            DrpDonViQuanLySelected = null;
            LoadDonVi();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            List<KHLCNhaThauQuery> listChungTu = _service.GetDataIndex().ToList();
            LoadDuAn(listChungTu);
            var lstItem = _mapper.Map<List<KHLCNhaThauModel>>(listChungTu);
            lstItem = lstItem.Select(n => { n.IRowIndex = lstItem.IndexOf(n) + 1; return n; }).ToList();
            Items = _mapper.Map<ObservableCollection<KHLCNhaThauModel>>(lstItem);
            _luaChonNhaThauView = CollectionViewSource.GetDefaultView(Items);
            _luaChonNhaThauView.Filter = VdtQddtKhlcnhaThauFilter;
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        public void OnSearch()
        {
            _luaChonNhaThauView.Refresh();
        }

        protected override void OnAdd()
        {
            KHLuaChonNhaThauDialogViewModel.IsAdd = true;
            KHLuaChonNhaThauDialogViewModel.IsDieuChinh = false;
            KHLuaChonNhaThauDialogViewModel.Model = new KHLCNhaThauModel();
            KHLuaChonNhaThauDialogViewModel.Init();
            KHLuaChonNhaThauDialogViewModel.SavedAction = obj => this.OnRefresh();
            KHLuaChonNhaThauDialogViewModel.ShowDialog();
            System.Windows.Application.Current.Properties["lstGoiThauIds"] = null;
            LoadData();
        }

        protected override void OnUpdate()
        {
            if (!CheckCanSuaXoa())
            {
                MessageBox.Show(string.Format(Resources.MsgRoleUpdate, SelectedItem.SUserCreate), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            KHLuaChonNhaThauDialogViewModel.IsDieuChinh = false;
            KHLuaChonNhaThauDialogViewModel.IsAdd = false;
            KHLuaChonNhaThauDialogViewModel.Model = SelectedItem;
            KHLuaChonNhaThauDialogViewModel.Init();
            KHLuaChonNhaThauDialogViewModel.SavedAction = obj => this.OnRefresh();
            KHLuaChonNhaThauDialogViewModel.ShowDialog();
            LoadData();
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

        protected void OnDieuChinh()
        {
            if (SelectedItem == null)
            {
                return;
            }

            if (!CheckCanSuaXoa())
            {
                MessageBox.Show(string.Format(Resources.MsgRoleDieuChinh, SelectedItem.SUserCreate), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            KHLuaChonNhaThauDialogViewModel.IsAdd = true;
            KHLuaChonNhaThauDialogViewModel.IsDieuChinh = true;
            KHLuaChonNhaThauDialogViewModel.Model = SelectedItem;
            KHLuaChonNhaThauDialogViewModel.Model.IIdParentId = SelectedItem.Id;
            KHLuaChonNhaThauDialogViewModel.Model.IIdLcNhaThauGocId = SelectedItem.IIdLcNhaThauGocId;
            KHLuaChonNhaThauDialogViewModel.Model.Id = Guid.Empty;
            KHLuaChonNhaThauDialogViewModel.Init();
            KHLuaChonNhaThauDialogViewModel.SavedAction = obj => this.OnRefresh();
            KHLuaChonNhaThauDialogViewModel.ShowDialog();
            LoadData();
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            if (SelectedItem.BKhoa != null && SelectedItem.BKhoa == true)
            {
                return;
            }
            if (!CheckCanSuaXoa())
            {
                MessageBox.Show(string.Format(Resources.MsgRoleDelete, SelectedItem.SUserCreate), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(string.Format(Resources.MsgConfirmDeleteKHLCNT, SelectedItem.STenDuAn));
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void onResetFilter()
        {
            SelectedDuAn = null;
            SoQuyetDinh = null;
            DNgayQuyetDinhFrom = null;
            DNgayQuyetDinhTo = null;
            DrpDonViQuanLySelected = null;
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(DNgayQuyetDinhFrom));
            OnPropertyChanged(nameof(DNgayQuyetDinhTo));
            OnPropertyChanged(nameof(DrpDonViQuanLySelected));
            OnSearch();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            KHLuaChonNhaThauDialogViewModel.IsDieuChinh = false;
            KHLuaChonNhaThauDialogViewModel.IsDetail = true;
            KHLuaChonNhaThauDialogViewModel.IsAdd = false;
            KHLuaChonNhaThauDialogViewModel.Model = (KHLCNhaThauModel)obj;
            KHLuaChonNhaThauDialogViewModel.Init();
            KHLuaChonNhaThauDialogViewModel.SavedAction = obj => this.OnRefresh();
            KHLuaChonNhaThauDialogViewModel.ShowDialog();
            KHLuaChonNhaThauDialogViewModel.IsDetail = false;
        }
        private void OnViewAttachment(object obj)
        {
            if (base.SelectedItem != null)
            {
                AttachmentViewModel.ModuleType = AttachmentEnum.Type.VDT_KH_LUACHON_NHATHAU;
                AttachmentViewModel.ObjectId = base.SelectedItem.Id;
                AttachmentViewModel.Init();
                AttachmentViewModel.ShowDialogHost();
            }
        }

        #endregion

        #region Helper
        private void GetDonViQuanLy()
        {
            var cbxLoaiDonViData = _nsDonViService.GetDanhSachDonViByNguoiDung(
                _sessionService.Current.Principal,
                _sessionService.Current.YearOfWork)
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = n.TenDonVi });
            _drpDonViQuanLy = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
        }

        private void LoadDonVi()
        {
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).Where(x => x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT);
            _drpDonViQuanLy = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi);
        }

        private void LoadDuAn(List<KHLCNhaThauQuery> lstKhlcnt)
        {
            ItemsDuAn = new ObservableCollection<ComboboxItem>();
            if (lstKhlcnt == null) return;
            Dictionary<Guid, ComboboxItem> dicDuAn = new Dictionary<Guid, ComboboxItem>();
            foreach (var item in lstKhlcnt)
            {
                if (!item.IIdDuAnId.HasValue || dicDuAn.ContainsKey(item.IIdDuAnId.Value)) continue;
                dicDuAn.Add(item.IIdDuAnId.Value, new ComboboxItem() { ValueItem = item.IIdDuAnId.Value.ToString(), DisplayItem = item.SMaDuAn + "-" + item.STenDuAn });
            }
            ItemsDuAn = new ObservableCollection<ComboboxItem>(dicDuAn.Values);
        }

        private bool VdtQddtKhlcnhaThauFilter(object obj)
        {
            if (!(obj is KHLCNhaThauModel temp)) return true;
            var bCondition = true;
            if (!string.IsNullOrEmpty(SoQuyetDinh))
            {
                bCondition &= (temp.SSoQuyetDinh.ToLower().Contains(SoQuyetDinh.ToLower()));
            }
            if (DNgayQuyetDinhFrom.HasValue)
            {
                bCondition &= (temp.DNgayQuyetDinh.HasValue && temp.DNgayQuyetDinh >= DNgayQuyetDinhFrom);
            }
            if (DNgayQuyetDinhTo.HasValue)
            {
                bCondition &= (temp.DNgayQuyetDinh.HasValue && temp.DNgayQuyetDinh <= DNgayQuyetDinhTo);
            }
            if (DrpDonViQuanLySelected != null)
            {
                bCondition &= (temp.IIdMaDonViQuanLy == DrpDonViQuanLySelected.ValueItem);
            }
            if (SelectedDuAn != null)
            {
                bCondition &= (temp.IIdDuAnId.HasValue && temp.IIdDuAnId.ToString() == SelectedDuAn.ValueItem);
            }
            return bCondition;
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            //nếu xóa gói thầu điều chỉnh thì update bactive = 1 của gói thầu cha
            if (SelectedItem.IIdParentId.HasValue)
            {
                VdtQddtKhlcnhaThau lCNTParent = _service.Find(SelectedItem.IIdParentId.Value);
                if (lCNTParent != null)
                {
                    lCNTParent.BActive = true;
                    lCNTParent.DDateUpdate = DateTime.Now;
                    lCNTParent.SUserUpdate = _sessionService.Current.Principal;
                    _service.UpdateLCNT(lCNTParent);
                }
                _goithauService.ReActiveGoiThauByKhlcntId(lCNTParent.Id);
            }
            _service.DeleteGoiThauByKHLCNTId(SelectedItem.Id);
            _service.Delete(_mapper.Map<KHLCNhaThauQuery>(SelectedItem));
            _iAttachmentService.DeleteByObjectIdAndModuleType(SelectedItem.Id, (int)AttachmentEnum.Type.VDT_KH_LUACHON_NHATHAU);

            LoadData();
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
                        MessageBox.Show(Resources.MsgRoleUnlock, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    if (SelectedItem.SUserCreate != _sessionService.Current.Principal)
                    {
                        MessageBox.Show(string.Format(Resources.MsgRoleLock, SelectedItem.SUserCreate), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (SelectedItem.BActive != true)
                    {
                        MessageBox.Show(string.Format(Resources.VoucherLockModified, SelectedItem.SUserCreate), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
                DialogResult result = MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
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
                _service.LockOrUnlock(SelectedItem.Id, !SelectedItem.BKhoa);
                SelectedItem.BKhoa = !SelectedItem.BKhoa;

                OnPropertyChanged(nameof(IsLock));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}
