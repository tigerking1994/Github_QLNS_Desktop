using System;
using log4net;
using System.Linq;
using AutoMapper;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.Utility;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Command;
using MaterialDesignThemes.Wpf;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.App.Properties;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAPlanImport;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAPlanImport
{
    public class DANHPhuongAnNhapKhauIndexViewModel : GridViewModelBase<NhHdnkPhuongAnNhapKhauModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhHdnkPhuongAnNhapKhauService _phuongAnNhapKhauService;
        private ICollectionView _itemsCollectionView;

        public override string Name => "Phương án nhập khẩu ";
        public override string Title => "Quản lý Phương án nhập khẩu ";
        public override string Description => "Danh sách Phương án nhập khẩu";
        public override Type ContentType => typeof(DANHPhuongAnNhapKhauIndex);
        public override PackIconKind IconKind => PackIconKind.BagChecked;

        public bool IsEditable => SelectedItem != null && SelectedItem.BIsActive && !SelectedItem.BIsKhoa;
        public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa;
        public int ILoai { get; set; }

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

        private ObservableCollection<DonViModel> _itemsChuongTrinh;
        public ObservableCollection<DonViModel> ItemsChuongTrinh
        {
            get => _itemsChuongTrinh;
            set => SetProperty(ref _itemsChuongTrinh, value);
        }

        private DonViModel _selectedChuongTrinh;
        public DonViModel SelectedChuongTrinh
        {
            get => _selectedChuongTrinh;
            set => SetProperty(ref _selectedChuongTrinh, value);
        }


        private NhHdnkPhuongAnNhapKhauModel _itemsFilter;
        public NhHdnkPhuongAnNhapKhauModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand DieuChinhCommand { get; }

        public DANHPhuongAnNhapKhauDialogViewModel DANHPhuongAnNhapKhauDialogViewModel { get; }

        public DANHPhuongAnNhapKhauIndexViewModel
        (
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INhHdnkPhuongAnNhapKhauService phuongAnNhapKhauService,
            DANHPhuongAnNhapKhauDialogViewModel danhPhuongAnNhapKhauDialogViewModel
        )
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _phuongAnNhapKhauService = phuongAnNhapKhauService;

            DANHPhuongAnNhapKhauDialogViewModel = danhPhuongAnNhapKhauDialogViewModel;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            DieuChinhCommand = new RelayCommand(o => OnDieuChinh(), obj => IsEditable);
            DeleteCommand = new RelayCommand(o => OnDelete(), obj => IsEditable);
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock(), obj => SelectedItem != null);
        }

        public override void Init()
        {
            LoadDefault();
            LoadDonVi();
            LoadData();

        }

        private void LoadDefault()
        {
            ItemsFilter = new NhHdnkPhuongAnNhapKhauModel();
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
                    Items = new ObservableCollection<NhHdnkPhuongAnNhapKhauModel>();
                    e.Result = _phuongAnNhapKhauService.FindIndex(ILoai);
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        Items = _mapper.Map<ObservableCollection<NhHdnkPhuongAnNhapKhauModel>>(e.Result);
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
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd()
        {
            DANHPhuongAnNhapKhauDialogViewModel.Model = new NhHdnkPhuongAnNhapKhauModel();
            DANHPhuongAnNhapKhauDialogViewModel.BIsReadOnly = false;
            DANHPhuongAnNhapKhauDialogViewModel.IsDieuChinh = false;
            DANHPhuongAnNhapKhauDialogViewModel.IsAdd = true;
            DANHPhuongAnNhapKhauDialogViewModel.ILoai = this.ILoai;
            DANHPhuongAnNhapKhauDialogViewModel.Init();
            DANHPhuongAnNhapKhauDialogViewModel.SavedAction = obj => this.OnRefresh();
            DANHPhuongAnNhapKhauDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            DANHPhuongAnNhapKhauDialogViewModel.Model = SelectedItem;
            DANHPhuongAnNhapKhauDialogViewModel.BIsReadOnly = false;
            DANHPhuongAnNhapKhauDialogViewModel.IsDieuChinh = false;
            DANHPhuongAnNhapKhauDialogViewModel.IsAdd = false;
            DANHPhuongAnNhapKhauDialogViewModel.ILoai = this.ILoai;
            DANHPhuongAnNhapKhauDialogViewModel.Init();
            DANHPhuongAnNhapKhauDialogViewModel.SavedAction = obj => this.OnRefresh();
            DANHPhuongAnNhapKhauDialogViewModel.ShowDialog();
        }

        private void OnDieuChinh()
        {
            DANHPhuongAnNhapKhauDialogViewModel.Model = SelectedItem;
            DANHPhuongAnNhapKhauDialogViewModel.BIsReadOnly = false;
            DANHPhuongAnNhapKhauDialogViewModel.IsDieuChinh = true;
            DANHPhuongAnNhapKhauDialogViewModel.IsAdd = false;
            DANHPhuongAnNhapKhauDialogViewModel.ILoai = this.ILoai;
            DANHPhuongAnNhapKhauDialogViewModel.Init();
            DANHPhuongAnNhapKhauDialogViewModel.SavedAction = obj => this.OnRefresh();
            DANHPhuongAnNhapKhauDialogViewModel.ShowDialog();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            DANHPhuongAnNhapKhauDialogViewModel.Model = SelectedItem;
            DANHPhuongAnNhapKhauDialogViewModel.BIsReadOnly = true;
            DANHPhuongAnNhapKhauDialogViewModel.IsDieuChinh = false;
            DANHPhuongAnNhapKhauDialogViewModel.ILoai = this.ILoai;
            DANHPhuongAnNhapKhauDialogViewModel.Init();
            DANHPhuongAnNhapKhauDialogViewModel.SavedAction = obj => this.OnRefresh();
            DANHPhuongAnNhapKhauDialogViewModel.ShowDialog();
        }

        private void OnRemoveFilter()
        {
            ItemsFilter = new NhHdnkPhuongAnNhapKhauModel();
            SelectedDonVi = null;
            LoadData();
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
                _phuongAnNhapKhauService.Delete(_mapper.Map<NhHdnkPhuongAnNhapKhau>(SelectedItem));
                MessageBox.Show("Xóa dữ liệu thành công");
                OnRefresh();
            }
        }

        protected override void OnLockUnLock()
        {
            string message = IsLock ? "Bạn có chắc chắn muốn mở khóa bản ghi này?" : "Bạn có chắc chắn muốn khóa bản ghi này?";
            MessageBoxResult result = MessageBoxHelper.Confirm(message);
            if (result == MessageBoxResult.Yes)
            {
                _phuongAnNhapKhauService.LockOrUnlock(SelectedItem.Id, !SelectedItem.BIsKhoa);
                SelectedItem.BIsKhoa = !SelectedItem.BIsKhoa;
                LoadData();
                OnPropertyChanged(nameof(IsLock));
            }
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEditable));
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhHdnkPhuongAnNhapKhauModel item)
            {
                bool result = true;
                if (SelectedDonVi != null)
                {
                    result &= item.IIdDonViQuanLyId.Equals(SelectedDonVi.Id);
                }
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
                    if (!string.IsNullOrEmpty(ItemsFilter.STenChuongTrinh))
                    {
                        result &= item.STenChuongTrinh != null && item.STenChuongTrinh.Contains(ItemsFilter.STenChuongTrinh, StringComparison.OrdinalIgnoreCase);

                    }
                }
                return result;
            }
            return false;
        }
    }
}
