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
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.ChuyenDuLieuQuyetToan;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ChuyenDuLieuQuyetToan
{
    public class ChuyenDuLieuQuyetToanIndexViewModel : GridViewModelBase<NhQtChuyenQuyetToanModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhQtChuyenQuyetToanService _service;
        private readonly INhQtChuyenQuyetToanChiTietService _chuyenQTChiTietService;
        private ICollectionView _itemsCollectionView;

        public override string GroupName => MenuItemContants.GROUP_FOREX_CHUYENDULIEU_QUYETTOAN;
        public override string Name => "Chuyển dữ liệu quyết toán";
        public override string Title => "Chuyển dữ liệu quyết toán";
        public override string Description => "Danh sách chuyển dữ liệu quyết toán";
        public override Type ContentType => typeof(ChuyenDuLieuQuyetToanIndex);
        public override PackIconKind IconKind => PackIconKind.Transfer;

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

        private ObservableCollection<ComboboxItem> _itemsLoaiThoiGian;
        public ObservableCollection<ComboboxItem> ItemsLoaiThoiGian
        {
            get => _itemsLoaiThoiGian;
            set => SetProperty(ref _itemsLoaiThoiGian, value);
        }

        private ComboboxItem _selectedLoaiThoiGian;
        public ComboboxItem SelectedLoaiThoiGian
        {
            get => _selectedLoaiThoiGian;
            set
            {
                SetProperty(ref _selectedLoaiThoiGian, value);
                LoadThoiGian();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsThoiGian;
        public ObservableCollection<ComboboxItem> ItemsThoiGian
        {
            get => _itemsThoiGian;
            set => SetProperty(ref _itemsThoiGian, value);
        }

        private ComboboxItem _selectedThoiGian;
        public ComboboxItem SelectedThoiGian
        {
            get => _selectedThoiGian;
            set => SetProperty(ref _selectedThoiGian, value);
        }

        private NhQtChuyenQuyetToanModel _itemsFilter;
        public NhQtChuyenQuyetToanModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

        private bool? _isAllSelected;

        public bool? IsAllSelected
        {
            get => _isAllSelected;
            set => SetProperty(ref _isAllSelected, value);
        }

        public ChuyenDulieuQuyetToanDetailViewModel ChuyenDulieuQuyetToanDetailViewModel { get; set; }
        public ChuyenDulieuQuyetToanDialogViewModel ChuyenDulieuQuyetToanDialogViewModel { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand TransferDataCommand { get; }
        public RelayCommand CheckRowCommand { get; private set; }
        public RelayCommand CheckAllRowsCommand { get; private set; }

        public ChuyenDuLieuQuyetToanIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INhQtChuyenQuyetToanService service,
            INhQtChuyenQuyetToanChiTietService chuyenQTChiTietService,
            ChuyenDulieuQuyetToanDetailViewModel chuyenDulieuQuyetToanDetailViewModel,
            ChuyenDulieuQuyetToanDialogViewModel chuyenDulieuQuyetToanDialogViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _service = service;
            _chuyenQTChiTietService = chuyenQTChiTietService;

            ChuyenDulieuQuyetToanDetailViewModel = chuyenDulieuQuyetToanDetailViewModel;
            ChuyenDulieuQuyetToanDialogViewModel = chuyenDulieuQuyetToanDialogViewModel;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate());
            DeleteCommand = new RelayCommand(o => OnDelete());
            TransferDataCommand = new RelayCommand(o => OnTransferData());
            CheckRowCommand = new RelayCommand(o => OnCheckRow());
            CheckAllRowsCommand = new RelayCommand(o => OnCheckAllRows());
        }

        private void OnCheckAllRows()
        {
            if (Items != null)
            {
                if (IsAllSelected == true)
                {
                    Items.ToList().ForEach(x => x.IsChecked = true);
                }
                else
                {
                    Items.ToList().ForEach(x => x.IsChecked = false);
                }
            }
        }

        private void OnCheckRow()
        {
            if (Items != null)
            {
                if (Items.ToList().All(x => x.IsChecked))
                {
                    IsAllSelected = true;
                }
                else if (Items.ToList().All(x => !x.IsChecked))
                {
                    IsAllSelected = false;
                }
                else
                {
                    IsAllSelected = null;
                }
            }
        }

        public override void Init()
        {
            LoadDefault();
            LoadDonVi();
            LoadLoaiThoiGian();
            LoadThoiGian();
            LoadData();
        }

        private void LoadDefault()
        {
            ItemsFilter = new NhQtChuyenQuyetToanModel();
        }

        private void LoadDonVi()
        {
            try
            {
                var data = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
                ItemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadLoaiThoiGian()
        {
            _itemsLoaiThoiGian = new ObservableCollection<ComboboxItem>();
            _itemsLoaiThoiGian.Add(new ComboboxItem("Tháng", "1"));
            _itemsLoaiThoiGian.Add(new ComboboxItem("Quý", "2"));
            OnPropertyChanged(nameof(ItemsLoaiThoiGian));
            SelectedLoaiThoiGian = null;
        }

        private void LoadThoiGian()
        {
            _itemsThoiGian = new ObservableCollection<ComboboxItem>();
            if (SelectedLoaiThoiGian != null)
            {
                switch (SelectedLoaiThoiGian.ValueItem)
                {
                    case "1":
                        ComboboxItem month;
                        for (int i = 1; i <= 12; i++)
                        {
                            month = new ComboboxItem("Tháng " + i, i.ToString());
                            _itemsThoiGian.Add(month);
                        }
                        break;
                    case "2":
                        ComboboxItem quy;
                        for (int i = 1; i <= 4; i++)
                        {
                            quy = new ComboboxItem("Quý " + i, i.ToString());
                            _itemsThoiGian.Add(quy);
                        }
                        break;
                    default:
                        break;
                }
            }
            OnPropertyChanged(nameof(ItemsThoiGian));
            SelectedThoiGian = null;
        }

        protected override void OnAdd()
        {
            ChuyenDulieuQuyetToanDialogViewModel.IsDetail = false;
            ChuyenDulieuQuyetToanDialogViewModel.Model = new NhQtChuyenQuyetToanModel();
            ChuyenDulieuQuyetToanDialogViewModel.Init();
            ChuyenDulieuQuyetToanDialogViewModel.SavedAction = obj => this.OnRefresh();
            ChuyenDulieuQuyetToanDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            if (SelectedItem == null) return;
            ChuyenDulieuQuyetToanDialogViewModel.IsDetail = false;
            ChuyenDulieuQuyetToanDialogViewModel.Model = SelectedItem;
            ChuyenDulieuQuyetToanDialogViewModel.Init();
            ChuyenDulieuQuyetToanDialogViewModel.SavedAction = obj => this.OnRefresh();
            ChuyenDulieuQuyetToanDialogViewModel.ShowDialog();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            ChuyenDulieuQuyetToanDetailViewModel.IsDetail = true;
            ChuyenDulieuQuyetToanDetailViewModel.Model = SelectedItem;
            ChuyenDulieuQuyetToanDetailViewModel.Init();
            ChuyenDulieuQuyetToanDetailViewModel.ShowDialog();
        }

        protected override void OnDelete()
        {
            if (SelectedItem != null)
            {
                string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
                if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
                {
                    _service.Delete(SelectedItem.Id);
                    _chuyenQTChiTietService.DeleteByChuyenQuyetToanId(SelectedItem.Id);
                    OnRefresh();
                }
            }
        }

        private void OnTransferData()
        {
            MessageBoxHelper.Info(Resources.MsgChuyenDuLieuThanhCong);
        }

        private void OnRemoveFilter()
        {
            SelectedDonVi = null;
            SelectedLoaiThoiGian = null;
            SelectedThoiGian = null;
            ItemsFilter = new NhQtChuyenQuyetToanModel();
            OnRefresh();
        }

        protected override void OnRefresh()
        {
            Init();
        }

        public override void LoadData(params object[] args)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                Items = new ObservableCollection<NhQtChuyenQuyetToanModel>();
                e.Result = _service.FindIndex();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    Items = _mapper.Map<ObservableCollection<NhQtChuyenQuyetToanModel>>(e.Result);
                    if (Items != null && Items.Count > 0)
                    {
                        SelectedItem = Items.FirstOrDefault();
                    }
                    _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                    _itemsCollectionView.Filter = Items_Filter;

                    IsAllSelected = false;
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
            if (obj is NhQtChuyenQuyetToanModel item)
            {
                bool result = true;
                if (ItemsFilter != null)
                {
                    if (!string.IsNullOrEmpty(ItemsFilter.sSoChungTu))
                    {
                        result &= item.sSoChungTu != null && item.sSoChungTu.Contains(ItemsFilter.sSoChungTu, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.dNgayChungTu != null)
                    {
                        result &= item.dNgayChungTu.HasValue && item.dNgayChungTu.Value.Date == ItemsFilter.dNgayChungTu.Value.Date;
                    }
                }
                if (SelectedDonVi != null)
                {
                    result &= item.iID_DonViID.HasValue && item.iID_DonViID.Value.Equals(SelectedDonVi.Id);
                }
                if (SelectedLoaiThoiGian != null)
                {
                    result &= item.iLoaiThoiGian.HasValue && item.iLoaiThoiGian.Value == int.Parse(SelectedLoaiThoiGian.ValueItem);
                }
                if (SelectedThoiGian != null)
                {
                    result &= item.iThoiGian.HasValue && item.iThoiGian.Value == int.Parse(SelectedThoiGian.ValueItem);
                }
                return result;
            }
            return false;
        }

        public override void Dispose()
        {
            if (!Items.IsEmpty()) Items.Clear();
        }
    }
}
