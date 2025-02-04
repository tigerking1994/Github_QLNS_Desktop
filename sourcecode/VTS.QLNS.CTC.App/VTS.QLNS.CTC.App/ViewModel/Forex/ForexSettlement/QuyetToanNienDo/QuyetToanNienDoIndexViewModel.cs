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
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.DeNghiQTDAHT;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.QuyetToanNienDo;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.DeNghiQTDAHT;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.QuyetToanNienDo.PrintDialog;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.QuyetToanNienDo
{
    public class QuyetToanNienDoIndexViewModel : GridViewModelBase<NhQtQuyetToanNienDoModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhQtQuyetToanNienDoService _service;
        private readonly INhQtQuyetToanNienDoChiTietService _serviceDetail;
        private ICollectionView _itemsCollectionView;
        private VoucherTabIndex _voucherTabIndex;
        private readonly INhThTongHopService _nhThTongHopService;
        public VoucherTabIndex VoucherTabIndex
        {
            get => _voucherTabIndex;
            set => SetProperty(ref _voucherTabIndex, value);
        }
        public override string GroupName => MenuItemContants.GROUP_FOREX_QUYETTOAN_NIENDO;
        public override string Name => "Đề nghị quyết toán niên độ";
        public override string Title => "Đề nghị quyết toán niên độ";
        public override string Description => "Danh sách đề nghị quyết toán niên độ";
        public override Type ContentType => typeof(QuyetToanNienDoIndex);
        public override PackIconKind IconKind => PackIconKind.BagChecked;
        public bool IsEditable => SelectedItem != null && SelectedItem.BIsActive == true && !SelectedItem.BIsKhoa;
        public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa;

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
            set
            {
                SetProperty(ref _selectedDonVi, value);
            }
        }

        private ObservableCollection<NguonNganSachModel> _itemsNguonVon;
        public ObservableCollection<NguonNganSachModel> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }
        private NguonNganSachModel _selectedNguonVon;
        public NguonNganSachModel SelectedNguonVon
        {
            get => _selectedNguonVon;
            set
            {
                SetProperty(ref _selectedNguonVon, value);
            }
        }

        private ObservableCollection<NhDmLoaiThanhToanModel> _itemsLoaiThanhToan;
        public ObservableCollection<NhDmLoaiThanhToanModel> ItemsLoaiThanhToan
        {
            get => _itemsLoaiThanhToan;
            set => SetProperty(ref _itemsLoaiThanhToan, value);
        }

        private NhDmLoaiThanhToanModel _selectedLoaiThanhToan;
        public NhDmLoaiThanhToanModel SelectedLoaiThanhToan
        {
            get => _selectedLoaiThanhToan;
            set
            {
                SetProperty(ref _selectedLoaiThanhToan, value);
            }
        }

        private ObservableCollection<NhDmLoaiThanhToanModel> _itemsLoaiQuyetToan;
        public ObservableCollection<NhDmLoaiThanhToanModel> ItemsLoaiQuyetToan
        {
            get => _itemsLoaiQuyetToan;
            set => SetProperty(ref _itemsLoaiQuyetToan, value);
        }

        private NhDmLoaiThanhToanModel _selectedLoaiQuyetToan;
        public NhDmLoaiThanhToanModel SelectedLoaiQuyetToan
        {
            get => _selectedLoaiQuyetToan;
            set
            {
                SetProperty(ref _selectedLoaiQuyetToan, value);
            }
        }

        private ObservableCollection<NhDmCoQuanThanhToanModel> _itemsCoQuanThanhToan;
        public ObservableCollection<NhDmCoQuanThanhToanModel> ItemsCoQuanThanhToan
        {
            get => _itemsCoQuanThanhToan;
            set => SetProperty(ref _itemsCoQuanThanhToan, value);
        }

        private NhDmCoQuanThanhToanModel _selectedCoQuanThanhToan;
        public NhDmCoQuanThanhToanModel SelectedCoQuanThanhToan
        {
            get => _selectedCoQuanThanhToan;
            set
            {
                SetProperty(ref _selectedCoQuanThanhToan, value);
            }
        }

        private ObservableCollection<NhQtQuyetToanNienDoModel> _listQuyetToanTongHop;
        public ObservableCollection<NhQtQuyetToanNienDoModel> ListQuyetToanTongHop
        {
            get => _listQuyetToanTongHop;
            set
            {
                if (SetProperty(ref _listQuyetToanTongHop, value))
                {
                    OnItemsChanged();
                }
            }
        }

        private ObservableCollection<NhQtQuyetToanNienDoModel> _listQuyetToanTongHopView;
        public ObservableCollection<NhQtQuyetToanNienDoModel> ListQuyetToanTongHopView
        {
            get => _listQuyetToanTongHopView;
            set
            {
                if (SetProperty(ref _listQuyetToanTongHopView, value))
                {
                    OnItemsChanged();
                }
            }
        }

        private NhQtQuyetToanNienDoModel _selectedTongHopItem;
        public NhQtQuyetToanNienDoModel SelectedTongHopItem
        {
            get => _selectedTongHopItem;
            set
            {
                if (SetProperty(ref _selectedTongHopItem, value))
                {
                    OnSelectedItemChanged();
                }
            }
        }

        private NhQtQuyetToanNienDoModel _itemsFilter;
        public NhQtQuyetToanNienDoModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

        public QuyetToanNienDoDialogViewModel QuyetToanNienDoDialogViewModel { get; set; }
        public QuyetToanNienDoDetailViewModel QuyetToanNienDoDetailViewModel { get; set; }
        public QuyetToanNienDoPrintDialogViewModel QuyetToanNienDoPrintDialogViewModel { get; set; }
        public QuyetToanNienDoTongHopViewModel QuyetToanNienDoTongHopViewModel { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand TongHopCommand { get; }

        public QuyetToanNienDoIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            INhQtQuyetToanNienDoService service,
            INhThTongHopService nhThTongHopService,
            INhQtQuyetToanNienDoChiTietService serviceDetail,
            QuyetToanNienDoDialogViewModel quyetToanNienDoDialogViewModel,
            QuyetToanNienDoDetailViewModel quyetToanNienDoDetailViewModel,
            QuyetToanNienDoPrintDialogViewModel quyetToanNienDoPrintDialogViewModel,
            QuyetToanNienDoTongHopViewModel quyetToanNienDoTongHopViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _service = service;
            _nhThTongHopService = nhThTongHopService;
            _serviceDetail = serviceDetail;
            QuyetToanNienDoDialogViewModel = quyetToanNienDoDialogViewModel;
            QuyetToanNienDoDetailViewModel = quyetToanNienDoDetailViewModel;
            QuyetToanNienDoPrintDialogViewModel = quyetToanNienDoPrintDialogViewModel;
            QuyetToanNienDoTongHopViewModel = quyetToanNienDoTongHopViewModel;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            DeleteCommand = new RelayCommand(o => OnDelete(), obj => IsEditable);
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock(), obj => SelectedItem != null);
            PrintCommand = new RelayCommand(obj => OnPrint());
            TongHopCommand = new RelayCommand(o => OnTongHopQTND(), obj => Items.Where(t => Items_Filter(t) && t.IsChecked).Any());
        }

        public override void Init()
        {
            VoucherTabIndex = VoucherTabIndex.VOUCHER;
            LoadDefault();
            LoadDonVi();
            LoadNguonVon();
            LoadLoaiThanhToan();
            LoadLoaiQuyetToan();
            LoadCoQuanThanhToan();
            LoadData();
        }

        private void LoadDefault()
        {
            ItemsFilter = new NhQtQuyetToanNienDoModel();
        }

        private void LoadDonVi()
        {
            try
            {
                var data = _nsDonViService.FindInternalByNamLamViec(_sessionService.Current.YearOfWork);
                ItemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadNguonVon()
        {
            var data = _nsNguonNganSachService.FindAll();
            _itemsNguonVon = _mapper.Map<ObservableCollection<NguonNganSachModel>>(data);
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        private void LoadLoaiThanhToan()
        {
            _itemsLoaiThanhToan = new ObservableCollection<NhDmLoaiThanhToanModel>();
            _itemsLoaiThanhToan.Add(new NhDmLoaiThanhToanModel() { Id = 1, STen = "Cấp kinh phí" });
            _itemsLoaiThanhToan.Add(new NhDmLoaiThanhToanModel() { Id = 2, STen = "Tạm ứng" });
            _itemsLoaiThanhToan.Add(new NhDmLoaiThanhToanModel() { Id = 3, STen = "Thanh toán" });
            OnPropertyChanged(nameof(ItemsLoaiThanhToan));
        }
        private void LoadLoaiQuyetToan()
        {
            _itemsLoaiQuyetToan = new ObservableCollection<NhDmLoaiThanhToanModel>();
            _itemsLoaiQuyetToan.Add(new NhDmLoaiThanhToanModel() { Id = 1, STen = "Quyết toán theo dự án" });
            _itemsLoaiQuyetToan.Add(new NhDmLoaiThanhToanModel() { Id = 2, STen = "Quyết toán theo hợp đồng" });
            OnPropertyChanged(nameof(ItemsLoaiQuyetToan));
        }

        private void LoadCoQuanThanhToan()
        {
            _itemsCoQuanThanhToan = new ObservableCollection<NhDmCoQuanThanhToanModel>();
            _itemsCoQuanThanhToan.Add(new NhDmCoQuanThanhToanModel() { Id = 1, STen = "Kho bạc" });
            _itemsCoQuanThanhToan.Add(new NhDmCoQuanThanhToanModel() { Id = 2, STen = "Cơ quan tài chính Bộ quốc phòng" });
            OnPropertyChanged(nameof(ItemsCoQuanThanhToan));
        }

        public override void LoadData(params object[] args)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                Items = new ObservableCollection<NhQtQuyetToanNienDoModel>();
                e.Result = _service.FindIndex();

                // Load data tổng hợp
                var lstTongHop = _service.FindTongHopIndex();
                ListQuyetToanTongHop = _mapper.Map<ObservableCollection<NhQtQuyetToanNienDoModel>>(lstTongHop);
                ListQuyetToanTongHopView = _mapper.Map<ObservableCollection<NhQtQuyetToanNienDoModel>>(lstTongHop.Where(x => !x.iID_TongHopID.HasValue));
                for (int i = 0; i < ListQuyetToanTongHopView.Count; i++)
                {
                    var ele = ListQuyetToanTongHopView.ElementAt(i);
                    ele.PropertyChanged += QuyetToanNienDoTongHopModelPropertyChanged;
                }
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    Items = _mapper.Map<ObservableCollection<NhQtQuyetToanNienDoModel>>(e.Result);
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

        protected override void OnAdd()
        {
            QuyetToanNienDoDialogViewModel.IsDetail = false;
            QuyetToanNienDoDialogViewModel.Model = new NhQtQuyetToanNienDoModel();
            QuyetToanNienDoDialogViewModel.Init();
            QuyetToanNienDoDialogViewModel.SavedAction = obj => this.OnRefresh();
            QuyetToanNienDoDialogViewModel.ShowDialogHost();
        }

        protected override void OnUpdate()
        {
            QuyetToanNienDoDialogViewModel.IsDetail = false;
            QuyetToanNienDoDialogViewModel.Model = SelectedItem;
            QuyetToanNienDoDialogViewModel.Init();
            QuyetToanNienDoDialogViewModel.SavedAction = obj => this.OnRefresh();
            QuyetToanNienDoDialogViewModel.ShowDialogHost();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (VoucherTabIndex == VoucherTabIndex.VOUCHER_AGREGATE)
            {
                return;
            }
            QuyetToanNienDoDetailViewModel.Model = SelectedItem;
            QuyetToanNienDoDetailViewModel.Init();
            QuyetToanNienDoDetailViewModel.ShowDialog();
        }

        private void OnPrint()
        {
            QuyetToanNienDoPrintDialogViewModel.Model = SelectedItem;
            QuyetToanNienDoPrintDialogViewModel.Init();
            QuyetToanNienDoPrintDialogViewModel.ShowDialog();
        }

        private void OnRemoveFilter()
        {
            ItemsFilter = new NhQtQuyetToanNienDoModel();
        }

        protected override void OnRefresh()
        {
            Init();
        }

        protected override void OnDelete()
        {
            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                //_nhThTongHopService.DeleteNHTongHop_Giam("QUYET_TOAN", SelectedItem.Id);
                //_nhThTongHopService.DeleteNHTongHop_Giam("QTND", SelectedItem.Id);
                _nhThTongHopService.InsertNHTongHop_New(NHConstants.QUYET_TOAN, (int)TypeExecute.Delete, SelectedItem.Id);

                _serviceDetail.DeleteQTNDChiTiet(_mapper.Map<NhQtQuyetToanNienDo>(SelectedItem));
                _service.Delete(_mapper.Map<NhQtQuyetToanNienDo>(SelectedItem));

                OnRefresh();
            }
        }

        protected override void OnLockUnLock()
        {
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            var result = MessageBoxHelper.Confirm(message);
            if (result == MessageBoxResult.Yes)
            {
                foreach (var item in Items)
                {
                    if (item.IsChecked)
                    {
                        _service.LockOrUnlock(item.Id, !item.BIsKhoa);
                        item.BIsKhoa = !item.BIsKhoa;
                        if (item.BIsKhoa)
                            _nhThTongHopService.InsertNHTongHop_New(NHConstants.QUYET_TOAN, (int)TypeExecute.Insert, item.Id);

                    }
                }
                //_service.LockOrUnlock(SelectedItem.Id, !SelectedItem.BIsKhoa);
                //SelectedItem.BIsKhoa = !SelectedItem.BIsKhoa;
                LoadData();
                OnPropertyChanged(nameof(IsLock));
            }
        }

        public override void Dispose()
        {
            if (!Items.IsEmpty()) Items.Clear();
        }

        public override void OnClose(object obj)
        {
            // Method intentionally left empty.
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEditable));
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhQtQuyetToanNienDoModel item)
            {
                bool result = true;

                if (_selectedDonVi != null)
                {
                    result &= item.IIdMaDonVi.Equals(_selectedDonVi.IIDMaDonVi);
                }
                if (_selectedCoQuanThanhToan != null)
                {
                    result &= item.ICoQuanThanhToan.Equals(_selectedCoQuanThanhToan.Id);
                }
                if (_selectedNguonVon != null)
                {
                    result &= item.IIdNguonVonId.Equals(_selectedNguonVon.IIdMaNguonNganSach);
                }
                if (_selectedLoaiThanhToan != null)
                {
                    result &= item.ILoaiThanhToan.Equals(_selectedLoaiThanhToan.Id);
                }
                if (_selectedLoaiQuyetToan != null)
                {
                    result &= item.ILoaiQuyetToan.Equals(_selectedLoaiQuyetToan.Id);
                }

                if (ItemsFilter != null)
                {
                    if (!string.IsNullOrEmpty(ItemsFilter.SSoDeNghi))
                    {
                        result &= item.SSoDeNghi != null && item.SSoDeNghi.Contains(ItemsFilter.SSoDeNghi, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.DNgayDeNghi != null)
                    {
                        result &= item.DNgayDeNghi.HasValue && item.DNgayDeNghi.Value.Date == ItemsFilter.DNgayDeNghi.Value.Date;
                    }
                    if (ItemsFilter.INamKeHoach != null)
                    {
                        result &= item.INamKeHoach != null && item.INamKeHoach == ItemsFilter.INamKeHoach;
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.SMoTa))
                    {
                        result &= item.SMoTa != null && item.SMoTa.Contains(ItemsFilter.SMoTa, StringComparison.OrdinalIgnoreCase);
                    }
                }
                return result;
            }
            return false;
        }

        private void OnTongHopQTND()
        {
            var lstTongHop = Items.Where(t => Items_Filter(t) && t.IsChecked).ToList();

            if (lstTongHop.Count <= 0)
            {
                MessageBoxHelper.Error("Không tìm thấy bản ghi đã chọn, vui lòng tải lại dữ liệu để thực hiện tổng hợp.");
                return;
            }

            var nguonVonDef = lstTongHop.FirstOrDefault().IIdNguonVonId;
            var namKeHoachDef = lstTongHop.FirstOrDefault().INamKeHoach;
            var loaiThanhToanDef = lstTongHop.FirstOrDefault().ILoaiThanhToan;

            var isInValid = lstTongHop.All(x => x.IIdNguonVonId == nguonVonDef && x.INamKeHoach == namKeHoachDef && x.ILoaiThanhToan == loaiThanhToanDef && x.BIsKhoa);
            if (!isInValid)
            {
                MessageBoxHelper.Info("Vui lòng chọn các bản ghi đã khóa có cùng năm kế hoạch, nguồn vốn và loại thanh toán để thực hiện tổng hợp.");
                return;
            }

            var isTongHop = lstTongHop.All(x => x.iID_TongHopID.HasValue);
            if (isTongHop)
            {
                MessageBoxHelper.Info("Không thể tổng hợp bản ghi đã tổng hợp.");
                return;
            }

            var dnqtTongHop = new NhQtQuyetToanNienDoModel();
            dnqtTongHop.INamKeHoach = namKeHoachDef;
            dnqtTongHop.ILoaiThanhToan = loaiThanhToanDef;
            dnqtTongHop.IIdNguonVonId = nguonVonDef;
            dnqtTongHop.sTongHopChildID = String.Join(";", lstTongHop.Select(x => x.Id).ToArray());

            QuyetToanNienDoTongHopViewModel.Init();
            QuyetToanNienDoTongHopViewModel.Model = dnqtTongHop;
            QuyetToanNienDoTongHopViewModel.ListQuyetToanTongHop = lstTongHop;
            QuyetToanNienDoTongHopViewModel.SavedAction = obj =>
            {
                LoadData();
                VoucherTabIndex = VoucherTabIndex.VOUCHER_AGREGATE;
            };

            var view = new QuyetToanNienDoTongHopDialog { DataContext = QuyetToanNienDoTongHopViewModel };
            DialogHost.Show(view, "RootDialog");
        }

        private void QuyetToanNienDoTongHopModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(NhQtQuyetToanNienDoModel.IsShowChildren)))
            {
                NhQtQuyetToanNienDoModel model = sender as NhQtQuyetToanNienDoModel;
                if (model.IsShowChildren)
                {
                    OnExpand();
                }
                else
                {
                    OnCollapse();
                }
            }
        }

        private void OnExpand()
        {
            int currentIndex = ListQuyetToanTongHopView.IndexOf(SelectedTongHopItem);
            SelectedTongHopItem.IsShowChildren = true;
            IEnumerable<NhQtQuyetToanNienDoModel> children = new List<NhQtQuyetToanNienDoModel>(ListQuyetToanTongHop.Where(t => SelectedTongHopItem.Id.Equals(t.iID_TongHopID)));
            foreach (var item in children)
            {
                item.AncestorIds = new HashSet<Guid>();
                item.AncestorIds.Add(SelectedTongHopItem.Id);
                ListQuyetToanTongHopView.Insert(++currentIndex, item);
            }
        }

        private void OnCollapse()
        {
            SelectedTongHopItem.IsShowChildren = false;
            ListQuyetToanTongHopView = new ObservableCollection<NhQtQuyetToanNienDoModel>(ListQuyetToanTongHopView.Where(t => t.AncestorIds == null || !t.AncestorIds.Contains(SelectedTongHopItem.Id)));
        }
    }
}
