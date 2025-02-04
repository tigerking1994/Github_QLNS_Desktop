using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using ControlzEx.Standard;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.NHKeHoachLuaChonNhaThau
{
    public class NHKeHoachLuaChonNhaThauIndexViewModel : GridViewModelBase<NhDaKhlcNhaThauModel>
    {
        #region Private
        private readonly INhDaKhlcNhaThauService _service;
        private readonly INsDonViService _dvService;
        private readonly INhDaDuAnService _duanService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private ICollectionView _luaChonNhaThauView;
        #endregion

        #region Public
        public override string Name => "Kế hoạch đặt hàng / lựa chọn nhà thầu";
        public override string Title => "Quản lý kế hoạch đặt hàng / lựa chọn nhà thầu";
        public override string Description => "Danh sách kế hoạch đặt hàng / lựa chọn nhà thầu";
        public override Type ContentType => typeof(View.Forex.ForexDuAn.QuanLyDuAn.NHKeHoachLuaChonNhaThau.NHKeHoachLuaChonNhaThauIndex);
        public bool IsEdit => SelectedItem != null && SelectedItem.BIsActive;
        public bool IsEnableLock => SelectedItem != null;
        public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa;
        public int IThuocMenu { get; set; }
        public int ILoai { get; set; }
        public bool IsShowDuAn { get; set; }
        public bool IsEditable => SelectedItem != null && SelectedItem.BIsActive && !SelectedItem.BIsKhoa;
        #endregion

        #region Items
        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private DateTime? _dNgayQuyetDinhTu;
        public DateTime? DNgayQuyetDinhTu
        {
            get => _dNgayQuyetDinhTu;
            set => SetProperty(ref _dNgayQuyetDinhTu, value);
        }

        private DateTime? _dNgayQuyetDinhDen;
        public DateTime? DNgayQuyetDinhDen
        {
            get => _dNgayQuyetDinhDen;
            set => SetProperty(ref _dNgayQuyetDinhDen, value);
        }

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
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

        private ObservableCollection<ComboboxItem> _itemsChuongTrinh;
        public ObservableCollection<ComboboxItem> ItemsChuongTrinh
        {
            get => _itemsChuongTrinh;
            set => SetProperty(ref _itemsChuongTrinh, value);
        }

        private ComboboxItem _selectedChuongTrinh;
        public ComboboxItem SelectedChuongTrinh
        {
            get => _selectedChuongTrinh;
            set => SetProperty(ref _selectedChuongTrinh, value);
        }
        #endregion

        #region declare RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand DieuChinhCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }
        #endregion

        public NHKeHoachLuaChonNhaThauDialogViewModel NHKeHoachLuaChonNhaThauDialogViewModel { get; }

        public NHKeHoachLuaChonNhaThauIndexViewModel(
            NHKeHoachLuaChonNhaThauDialogViewModel nHKeHoachLuaChonNhaThauDialogViewModel,
            INhDaKhlcNhaThauService service,
            INhDaDuAnService duanService,
            INsDonViService dvService,
            ISessionService sessionService,
            IMapper mapper,
            ILog logger)
        {
            NHKeHoachLuaChonNhaThauDialogViewModel = nHKeHoachLuaChonNhaThauDialogViewModel;
            NHKeHoachLuaChonNhaThauDialogViewModel.ParentPage = this;

            _service = service;
            _duanService = duanService;
            _dvService = dvService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;

            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => onResetFilter());

            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            DieuChinhCommand = new RelayCommand(o => OnDieuChinh(), obj => IsEditable);
            DeleteCommand = new RelayCommand(o => OnDelete(), obj => IsEditable);
        }

        #region Event
        public override void Init()
        {
            MarginRequirement = new Thickness(10);
            LoadDonViQuanLy();
            LoadDuAn();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    // Main process
                    Items = new ObservableCollection<NhDaKhlcNhaThauModel>();
                    if (IsShowDuAn)
                    {
                        e.Result = _service.GetAllKhlcntIndex(IThuocMenu);
                    }
                    else
                    {
                        e.Result = _service.GetAllKhlcntMuaSam().Where(a => a.IThuocMenu == IThuocMenu);
                    }
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        Items = _mapper.Map<ObservableCollection<NhDaKhlcNhaThauModel>>(e.Result);
                        // Process when run completed. e.Result
                        if (Items != null && Items.Count > 0)
                        {
                            SelectedItem = Items.FirstOrDefault();
                        }
                        _luaChonNhaThauView = CollectionViewSource.GetDefaultView(Items);
                        _luaChonNhaThauView.Filter = OnFilter;
                        LoadChuongTrinh();

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

        protected override void OnAdd()
        {
            //NHKeHoachLuaChonNhaThauDialogViewModel.IsDieuChinh = false;
            //OpenDialog(new NhDaKhlcNhaThauModel());

            NHKeHoachLuaChonNhaThauDialogViewModel.Model = new NhDaKhlcNhaThauModel();
            NHKeHoachLuaChonNhaThauDialogViewModel.BIsReadOnly = false;
            NHKeHoachLuaChonNhaThauDialogViewModel.IsDieuChinh = false;
            NHKeHoachLuaChonNhaThauDialogViewModel.ILoai = ILoai;
            NHKeHoachLuaChonNhaThauDialogViewModel.IThuocMenu = IThuocMenu;
            NHKeHoachLuaChonNhaThauDialogViewModel.Init();
            NHKeHoachLuaChonNhaThauDialogViewModel.SavedAction = obj => OnRefresh();
            NHKeHoachLuaChonNhaThauDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            //NHKeHoachLuaChonNhaThauDialogViewModel.IsDieuChinh = false;
            //OpenDialog(SelectedItem);

            NHKeHoachLuaChonNhaThauDialogViewModel.Model = SelectedItem;
            NHKeHoachLuaChonNhaThauDialogViewModel.BIsReadOnly = false;
            NHKeHoachLuaChonNhaThauDialogViewModel.IsDieuChinh = false;
            NHKeHoachLuaChonNhaThauDialogViewModel.ILoai = ILoai;
            NHKeHoachLuaChonNhaThauDialogViewModel.IThuocMenu = IThuocMenu;
            NHKeHoachLuaChonNhaThauDialogViewModel.Init();
            NHKeHoachLuaChonNhaThauDialogViewModel.SavedAction = obj => OnRefresh();
            NHKeHoachLuaChonNhaThauDialogViewModel.ShowDialog();
        }
        protected override void OnDieuChinh()
        {
            //NHKeHoachLuaChonNhaThauDialogViewModel.IsDieuChinh = true;
            //OpenDialog(SelectedItem);

            NHKeHoachLuaChonNhaThauDialogViewModel.Model = SelectedItem;
            NHKeHoachLuaChonNhaThauDialogViewModel.BIsReadOnly = false;
            NHKeHoachLuaChonNhaThauDialogViewModel.IsDieuChinh = true;
            NHKeHoachLuaChonNhaThauDialogViewModel.ILoai = ILoai;
            NHKeHoachLuaChonNhaThauDialogViewModel.IThuocMenu = IThuocMenu;
            NHKeHoachLuaChonNhaThauDialogViewModel.Init();
            NHKeHoachLuaChonNhaThauDialogViewModel.SavedAction = obj => OnRefresh();
            NHKeHoachLuaChonNhaThauDialogViewModel.ShowDialog();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            //OpenDialog((NhDaKhlcNhaThauModel)obj);

            NHKeHoachLuaChonNhaThauDialogViewModel.Model = SelectedItem;
            NHKeHoachLuaChonNhaThauDialogViewModel.BIsReadOnly = true;
            NHKeHoachLuaChonNhaThauDialogViewModel.IsDieuChinh = false;
            NHKeHoachLuaChonNhaThauDialogViewModel.ILoai = ILoai;
            NHKeHoachLuaChonNhaThauDialogViewModel.IThuocMenu = IThuocMenu;
            NHKeHoachLuaChonNhaThauDialogViewModel.Init();
            NHKeHoachLuaChonNhaThauDialogViewModel.SavedAction = obj => OnRefresh();
            NHKeHoachLuaChonNhaThauDialogViewModel.ShowDialog();
        }

        protected override void OnDelete()
        {
            //base.OnDelete();
            //if (!SelectedItem.BIsKhoa)
            //{
            //    return;
            //}
            //if (!CheckCanSuaXoa())
            //{
            //    MessageBoxHelper.Error(string.Format(Resources.MsgRoleDelete, SelectedItem.SNguoiTao));
            //    return;
            //}

            //StringBuilder messageBuilder = new StringBuilder();
            //messageBuilder.AppendFormat(string.Format(Resources.MsgConfirmDeleteKHLCNT, SelectedItem.STenDuAn));
            //var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
            //DialogHost.Show(messageBox.Content, "RootDialog");

            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                _service.Delete(SelectedItem.Id);
                OnRefresh();
            }
        }

        protected override void OnLockUnLock()
        {
            //
            string message = IsLock ? "Bạn có chắc chắn muốn mở khóa bản ghi này?" : "Bạn có chắc chắn muốn khóa bản ghi này?";
            MessageBoxResult result = MessageBoxHelper.Confirm(message);
            if (result == MessageBoxResult.Yes)
            {
                _service.LockOrUnlock(SelectedItem.Id, !SelectedItem.BIsKhoa);
                SelectedItem.BIsKhoa = !SelectedItem.BIsKhoa;
                LoadData();
                OnPropertyChanged(nameof(IsLock));
            }
            //


            //if (IsLock)
            //{
            //    List<DonVi> userAgency = _dvService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
            //    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
            //    {
            //        MessageBoxHelper.Error(Resources.MsgRoleUnlock);
            //        return;
            //    }
            //}
            //else
            //{
            //    if (SelectedItem.SNguoiTao != _sessionService.Current.Principal)
            //    {
            //        MessageBoxHelper.Error(string.Format(Resources.MsgRoleLock, SelectedItem.SNguoiTao));
            //        return;
            //    }
            //}

            //string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            //var result = MessageBoxHelper.Confirm(message);
            //if (result == MessageBoxResult.Yes)
            //    LockConfirmEventHandler();
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

        private void onResetFilter()
        {
            SSoQuyetDinh = null;
            DNgayQuyetDinhTu = null;
            DNgayQuyetDinhDen = null;
            SMoTa = null;
            SelectedDonVi = null;
            SelectedDuAn = null;
            OnPropertyChanged(nameof(SSoQuyetDinh));
            OnPropertyChanged(nameof(DNgayQuyetDinhTu));
            OnPropertyChanged(nameof(DNgayQuyetDinhDen));
            OnPropertyChanged(nameof(SMoTa));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnSearch();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnableLock));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
        }
        #endregion

        #region Helper
        private void OpenDialog(NhDaKhlcNhaThauModel selectedItem)
        {
            NHKeHoachLuaChonNhaThauDialogViewModel.Model = selectedItem;
            NHKeHoachLuaChonNhaThauDialogViewModel.Init();
            NHKeHoachLuaChonNhaThauDialogViewModel.SavedAction = obj => OnRefresh();
            NHKeHoachLuaChonNhaThauDialogViewModel.ILoai = ILoai;
            NHKeHoachLuaChonNhaThauDialogViewModel.IThuocMenu = IThuocMenu;
            NHKeHoachLuaChonNhaThauDialogViewModel.IsShowDuAn = IsShowDuAn;
            NHKeHoachLuaChonNhaThauDialogViewModel.ShowDialog();
            LoadData();
        }

        private bool OnFilter(object obj)
        {
            if (!(obj is NhDaKhlcNhaThauModel temp)) return true;
            var bCondition = true;
            if (!string.IsNullOrEmpty(SSoQuyetDinh))
            {
                bCondition &= temp.SSoQuyetDinh.ToLower().Contains(SSoQuyetDinh.ToLower());
            }
            if (DNgayQuyetDinhTu.HasValue)
            {
                bCondition &= temp.DNgayQuyetDinh.HasValue && temp.DNgayQuyetDinh >= DNgayQuyetDinhTu;
            }
            if (DNgayQuyetDinhDen.HasValue)
            {
                bCondition &= temp.DNgayQuyetDinh.HasValue && temp.DNgayQuyetDinh <= DNgayQuyetDinhDen;
            }
            if (SelectedDonVi != null)
            {
                bCondition &= temp.SMaDonViQuanLy == SelectedDonVi.ValueItem;
            }
            if (SelectedDuAn != null)
            {
                bCondition &= temp.IIdDuAnID.HasValue && temp.IIdDuAnID.ToString() == SelectedDuAn.ValueItem;
            }
            if (SelectedChuongTrinh != null)
            {
                bCondition &= temp.IIdKHTTNhiemVuChiId == SelectedChuongTrinh.Id;
            }
            return bCondition;
        }

        private void LockConfirmEventHandler()
        {
            _service.Log(SelectedItem.Id, _sessionService.Current.Principal);
            SelectedItem.BIsKhoa = !SelectedItem.BIsKhoa;
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            try
            {
                if (result != NSDialogResult.Yes) return;
                _service.Delete(SelectedItem.Id);
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool CheckCanSuaXoa()
        {
            var user = _sessionService.Current.Principal;

            if (user == SelectedItem.SNguoiTao)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LoadDonViQuanLy()
        {
            try
            {
                var lstDonVi = _dvService.FindByNamLamViec(_sessionService.Current.YearOfWork);
                if (lstDonVi == null) return;
                ItemsDonVi = new ObservableCollection<ComboboxItem>(lstDonVi.Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = n.TenDonVi }));
                OnPropertyChanged(nameof(ItemsDonVi));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDuAn()
        {
            try
            {
                var lstDuAn = _duanService.FindAll();
                if (lstDuAn == null) return;
                ItemsDuAn = new ObservableCollection<ComboboxItem>(lstDuAn.Select(n => new ComboboxItem() { ValueItem = n.Id.ToString(), DisplayItem = n.STenDuAn }));
                OnPropertyChanged(nameof(ItemsDuAn));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadChuongTrinh()
        {
            try
            {
                if (Items == null) return;
                ItemsChuongTrinh = new ObservableCollection<ComboboxItem>(
                                   Items.GroupBy(g => g.IIdKHTTNhiemVuChiId)
                                        .Select(n => new ComboboxItem()
                                        { ValueItem = n.First().Id.ToString(), Id = n.First().IIdKHTTNhiemVuChiId != null ? n.First().IIdKHTTNhiemVuChiId.Value : Guid.Empty, DisplayItem = n.First().STenChuongTrinh }));

                OnPropertyChanged(nameof(ItemsChuongTrinh));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        #endregion
    }
}
