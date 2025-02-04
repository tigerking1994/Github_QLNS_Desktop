using AutoMapper;
using log4net;
using System;
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
using VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyDuAn.ThietKeKyThuatTongDuToan;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.ThietKeKyThuatTongDuToan
{
    public class ThietKeKyThuatTongDuToanIndexViewModel : GridViewModelBase<NhDaDuToanModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDaDuToanService _service;
        private ICollectionView _itemsCollectionView;

        public override Type ContentType => typeof(ThietKeKyThuatTongDuToanIndex);
        public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa;
        public bool IsEdit => SelectedItem != null;
        public int ILoai { get; set; }
        public bool IsTenDuAn => !(ILoai == 4|| ILoai==3);
        public bool IsTenChuongTrinh => !(ILoai == 1 || ILoai == 2);
        public bool IsShowDuAn { get; set; }
        public bool IsEditable => SelectedItem != null && SelectedItem.BIsActive.Value && !SelectedItem.BIsKhoa;

        private NhDaDuToanModel _itemsFilter;
        public NhDaDuToanModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }
        private ObservableCollection<NhDaDuToanModel> _itemsLoaiDuToan;
        public ObservableCollection<NhDaDuToanModel> ItemsLoaiDuToan
        {
            get => _itemsLoaiDuToan;
            set => SetProperty(ref _itemsLoaiDuToan, value);
        }

        private NhDaDuToanModel _selectedLoaiDuToan;
        public NhDaDuToanModel SelectedLoaiDuToan
        {
            get => _selectedLoaiDuToan;
            set => SetProperty(ref _selectedLoaiDuToan, value);
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

        public ThietKeKyThuatTongDuToanDialogViewModel NhDaDuToanDialogViewModel { get; }
        public AttachmentViewModel AttachmentViewModel { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand DieuChinhCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }

        public ThietKeKyThuatTongDuToanIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INhDaDuToanService nhDaDuToanService,
            ThietKeKyThuatTongDuToanDialogViewModel nhDaDuToanDialogViewModel,
            AttachmentViewModel attachmentViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _service = nhDaDuToanService;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            DieuChinhCommand = new RelayCommand(o => OnDieuChinh(), obj => IsEditable);
            DeleteCommand = new RelayCommand(o => OnDelete(), obj => IsEditable);
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock(), obj => SelectedItem != null);
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(), obj => SelectedItem != null && SelectedItem.TotalFiles > 0);

            NhDaDuToanDialogViewModel = nhDaDuToanDialogViewModel;
            AttachmentViewModel = attachmentViewModel;
        }

        public override void Init()
        {    
            OnPropertyChanged(nameof(IsTenDuAn));
            OnPropertyChanged(nameof(ItemsLoaiDuToan));
            OnPropertyChanged(nameof(IsTenChuongTrinh));          
            LoadDefault();
            LoadLoaiDuToan();
            LoadDonVi();
            LoadData();
        }

        private void LoadLoaiDuToan()
        {
            _itemsLoaiDuToan = new ObservableCollection<NhDaDuToanModel>();
            _itemsLoaiDuToan.Add(new NhDaDuToanModel() { IdLoaiDuToan = 1, STenLoaiDuToan = "Dự toán mua sắm được duyệt" });
            _itemsLoaiDuToan.Add(new NhDaDuToanModel() { IdLoaiDuToan = 2, STenLoaiDuToan = "Dự toán đặt hàng được duyệt" });
            OnPropertyChanged(nameof(ItemsLoaiDuToan));
        }

        private void LoadDefault()
        {
            ItemsFilter = new NhDaDuToanModel();
        }

        private void LoadDonVi()
        {
            _itemsDonVi = new ObservableCollection<DonViModel>();
            var data = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    // Main process
                    Items = new ObservableCollection<NhDaDuToanModel>();
                    e.Result = _service.FindIndex(_sessionService.Current.YearOfWork, ILoai);
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        Items = _mapper.Map<ObservableCollection<NhDaDuToanModel>>(e.Result);
                        // Process when run completed. e.Result
                        if (Items != null && Items.Count > 0)
                        {
                            SelectedItem = Items.FirstOrDefault();
                            foreach (var item in Items)
                            {
                                if (item.IdLoaiDuToan == (int)LOAI_DU_TOAN_FOREX.Type.LOAI_DU_TOAN_MUA_SAM) { item.STenLoaiDuToan = LOAI_DU_TOAN_FOREX.TypeName.LOAI_DU_TOAN_MUA_SAM; };
                                if (item.IdLoaiDuToan == (int)LOAI_DU_TOAN_FOREX.Type.LOAI_DU_TOAN_DAT_HANG) { item.STenLoaiDuToan = LOAI_DU_TOAN_FOREX.TypeName.LOAI_DU_TOAN_DAT_HANG; };
                            }
                        }
                        _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                        _itemsCollectionView.Filter = Items_Filter;
                        LoadChuongTrinh();
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd()
        {
            NhDaDuToanDialogViewModel.IsDetail = false;
            NhDaDuToanDialogViewModel.IsDieuChinh = false;
            NhDaDuToanDialogViewModel.Model = new NhDaDuToanModel();
            NhDaDuToanDialogViewModel.ILoai = ILoai;
            NhDaDuToanDialogViewModel.IsShowDuAn = IsShowDuAn;
            NhDaDuToanDialogViewModel.Init();
            NhDaDuToanDialogViewModel.SavedAction = obj => this.OnRefresh();
            NhDaDuToanDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            NhDaDuToanDialogViewModel.IsDetail = false;
            NhDaDuToanDialogViewModel.IsDieuChinh = false;
            NhDaDuToanDialogViewModel.Model = SelectedItem;
            NhDaDuToanDialogViewModel.ILoai = ILoai;
            NhDaDuToanDialogViewModel.IsShowDuAn = IsShowDuAn;
            NhDaDuToanDialogViewModel.Init();
            NhDaDuToanDialogViewModel.SavedAction = obj => this.OnRefresh();
            NhDaDuToanDialogViewModel.ShowDialog();
        }

        private void OnDieuChinh()
        {
            if (SelectedItem != null)
            {
                NhDaDuToanDialogViewModel.IsDetail = false;
                NhDaDuToanDialogViewModel.IsDieuChinh = true;
                NhDaDuToanDialogViewModel.Model = SelectedItem;
                NhDaDuToanDialogViewModel.ILoai = ILoai;
                NhDaDuToanDialogViewModel.IsShowDuAn = IsShowDuAn;
                NhDaDuToanDialogViewModel.Init();
                NhDaDuToanDialogViewModel.SavedAction = obj => this.OnRefresh();
                NhDaDuToanDialogViewModel.ShowDialog();
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            NhDaDuToanDialogViewModel.IsDetail = true;
            NhDaDuToanDialogViewModel.IsDieuChinh = false;
            NhDaDuToanDialogViewModel.Model = SelectedItem;
            NhDaDuToanDialogViewModel.ILoai = ILoai;
            NhDaDuToanDialogViewModel.IsShowDuAn = IsShowDuAn;
            NhDaDuToanDialogViewModel.Init();
            NhDaDuToanDialogViewModel.SavedAction = obj => this.OnRefresh();
            NhDaDuToanDialogViewModel.ShowDialog();
        }

        private void OnRemoveFilter()
        {
            ItemsFilter = new NhDaDuToanModel();
            SelectedDonVi = null;
            SelectedLoaiDuToan = null;
            _itemsCollectionView.Refresh();
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnDelete()
        {
            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                _service.Delete(_mapper.Map<NhDaDuToan>(SelectedItem));
                OnRefresh();
            }
        }

        protected override void OnLockUnLock()
        {
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            var result = MessageBoxHelper.Confirm(message);
            if (result == MessageBoxResult.Yes)
            {
                _service.LockOrUnlock(SelectedItem.Id, !SelectedItem.BIsKhoa);
                SelectedItem.BIsKhoa = !SelectedItem.BIsKhoa;

                LoadData();
                OnPropertyChanged(nameof(IsLock));
            }
        }

        private void OnViewAttachment()
        {
            if (base.SelectedItem != null)
            {
                AttachmentViewModel.ModuleType = AttachmentEnum.Type.NH_TKKT_TONGDUTOAN;
                AttachmentViewModel.ObjectId = base.SelectedItem.Id;
                AttachmentViewModel.Init();
                AttachmentViewModel.ShowDialogHost();
            }
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEditable));
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhDaDuToanModel item)
            {
                bool result = true;
                if (ItemsFilter != null)
                {
                    if (!string.IsNullOrEmpty(ItemsFilter.SSoQuyetDinh))
                    {
                        result &= item.SSoQuyetDinh != null && item.SSoQuyetDinh.Contains(ItemsFilter.SSoQuyetDinh, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.DNgayQuyetDinh != null)
                    {
                        result &= item.DNgayQuyetDinh.HasValue && item.DNgayQuyetDinh.Value.Date == ItemsFilter.DNgayQuyetDinh.Value.Date;
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.SMoTa))
                    {
                        result &= item.SMoTa != null && item.SMoTa.Contains(ItemsFilter.SMoTa, StringComparison.OrdinalIgnoreCase);
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.STenDuAn))
                    {
                        result &= item.STenDuAn != null && item.STenDuAn.Contains(ItemsFilter.STenDuAn, StringComparison.OrdinalIgnoreCase);
                    }
                }
                if (SelectedDonVi != null)
                {
                    if (item.IIdMaDonViQuanLy != null)
                    {
                        result &= item.IIdMaDonViQuanLy.Contains(SelectedDonVi.IIDMaDonVi);
                    }
                    else
                    {
                        result &= false;
                    }
                }
                if (SelectedLoaiDuToan != null)
                {
                    result &= item.IdLoaiDuToan.Equals(SelectedLoaiDuToan.IdLoaiDuToan);
                }
                if (SelectedChuongTrinh != null)
                {
                    result &= item.IIdKHTTNhiemVuChiId == SelectedChuongTrinh.Id;
                }
                return result;
            }

            return false;
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
    }
}
