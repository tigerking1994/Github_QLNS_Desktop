using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
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
using VTS.QLNS.CTC.App.View.Forex.ForexAllocation.ForexDeNghiThanhToan;
using VTS.QLNS.CTC.App.View.Forex.ForexAllocation.ForexDeNghiThanhToan.PrintDialog;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexAllocation.ForexDeNghiThanhToan.PrintDialog;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexDeNghiThanhToan
{
    public class ForexDeNghiThanhToanIndexViewModel : GridViewModelBase<NhTtThanhToanModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly INsDonViService _iNsDonViService;
        private readonly INsNguonNganSachService _iNsNguonNganSachService;
        private readonly INhTtThanhToanService _iNhTtThanhToanService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private ICollectionView _itemsCollectionView;

        public override string Name => "Đề nghị thanh toán";
        public override Type ContentType => typeof(View.Forex.ForexAllocation.ForexDeNghiThanhToan.ForexDeNghiThanhToanIndex);
        public override string Title => "Đề nghị thanh toán";
        public override string Description => "Danh sách thông tin quản lý đề nghị thanh toán";
        public bool IsLock => VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER) && SelectedItem != null && SelectedItem.BIsKhoa
            || VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER_AGREGATE) && SelectedTongHopItem != null && SelectedTongHopItem.BIsKhoa;
        public bool IsEnabled => VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER) && SelectedItem != null && !SelectedItem.BIsKhoa ||
            VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER_AGREGATE) && SelectedTongHopItem != null && !SelectedTongHopItem.BIsKhoa;

        private NhTtThanhToanModel _itemsFilter;
        public NhTtThanhToanModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
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
            set
            {
                SetProperty(ref _selectedDonVi, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiDeNghi;
        public ObservableCollection<ComboboxItem> ItemsLoaiDeNghi
        {
            get => _itemsLoaiDeNghi;
            set => SetProperty(ref _itemsLoaiDeNghi, value);
        }

        private ComboboxItem _selectedLoaiDeNghi;
        public ComboboxItem SelectedLoaiDeNghi
        {
            get => _selectedLoaiDeNghi;
            set
            {
                SetProperty(ref _selectedLoaiDeNghi, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsQuyKeHoach;
        public ObservableCollection<ComboboxItem> ItemsQuyKeHoach
        {
            get => _itemsQuyKeHoach;
            set => SetProperty(ref _itemsQuyKeHoach, value);
        }

        private ComboboxItem _selectedQuyKeHoach;
        public ComboboxItem SelectedQuyKeHoach
        {
            get => _selectedQuyKeHoach;
            set
            {
                SetProperty(ref _selectedQuyKeHoach, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsNguonVon;
        public ObservableCollection<ComboboxItem> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private ComboboxItem _selectedNguonVon;
        public ComboboxItem SelectedNguonVon
        {
            get => _selectedNguonVon;
            set
            {
                SetProperty(ref _selectedNguonVon, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiNoiDungChi;
        public ObservableCollection<ComboboxItem> ItemsLoaiNoiDungChi
        {
            get => _itemsLoaiNoiDungChi;
            set => SetProperty(ref _itemsLoaiNoiDungChi, value);
        }

        private ComboboxItem _selectedLoaiNoiDungChi;
        public ComboboxItem SelectedLoaiNoiDungChi
        {
            get => _selectedLoaiNoiDungChi;
            set
            {
                SetProperty(ref _selectedLoaiNoiDungChi, value);
            }
        }

        private VoucherTabIndex _voucherTabIndex;
        public VoucherTabIndex VoucherTabIndex
        {
            get => _voucherTabIndex;
            set => SetProperty(ref _voucherTabIndex, value);
        }

        private ObservableCollection<NhTtThanhToanModel> _listDeNghiTongHop;
        public ObservableCollection<NhTtThanhToanModel> ListDeNghiTongHop
        {
            get => _listDeNghiTongHop;
            set
            {
                SetProperty(ref _listDeNghiTongHop, value);
                _deNghiThanhToanTongHopView = CollectionViewSource.GetDefaultView(ListDeNghiTongHop);
                _deNghiThanhToanTongHopView.Filter = NHTtDeNghiThanhToanTongHopFilter;
            }
        }

        private ObservableCollection<NhTtThanhToanModel> _listDeNghiTongHopChiTiet;
        public ObservableCollection<NhTtThanhToanModel> ListDeNghiTongHopChiTiet
        {
            get => _listDeNghiTongHopChiTiet;
            set
            {
                SetProperty(ref _listDeNghiTongHopChiTiet, value);
            }
        }

        private NhTtThanhToanModel _selectedTongHopItem;
        public NhTtThanhToanModel SelectedTongHopItem
        {
            get => _selectedTongHopItem;
            set
            {
                SetProperty(ref _selectedTongHopItem, value);
                OnPropertyChanged(nameof(IsLock));
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public ForexDeNghiThanhToanDialogViewModel ForexDeNghiThanhToanDialogViewModel { get; set; }
        public ForexDeNghiThanhToanPrintDialogViewModel ForexDeNghiThanhToanPrintDialogViewModel { get; set; }
        public ForexDeNghiTTTongHopDialogViewModel ForexDeNghiTTTongHopDialogViewModel { get; set; }
        public ForexDeNghiThanhToanImportViewModel ForexDeNghiThanhToanImportViewModel { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand TongHopDeNghiTTCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public ICollectionView _deNghiThanhToanTongHopView { get; private set; }

        public ForexDeNghiThanhToanIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            INsDonViService iNsDonViService,
            INsNguonNganSachService iNsNguonNganSachService,
            INhTtThanhToanService iNhTtThanhToanService,
            ForexDeNghiThanhToanPrintDialogViewModel forexDeNghiThanhToanPrintDialogViewModel,
            ForexDeNghiThanhToanDialogViewModel forexDeNghiThanhToanDialogViewModel,
            ForexDeNghiTTTongHopDialogViewModel forexDeNghiTTTongHopDialogViewModel,
            ForexDeNghiThanhToanImportViewModel forexDeNghiThanhToanImportViewModel,
            IDmChuDauTuService dmChuDauTuService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _iNsDonViService = iNsDonViService;
            _iNsNguonNganSachService = iNsNguonNganSachService;
            _iNhTtThanhToanService = iNhTtThanhToanService;

            ForexDeNghiThanhToanDialogViewModel = forexDeNghiThanhToanDialogViewModel;
            ForexDeNghiThanhToanPrintDialogViewModel = forexDeNghiThanhToanPrintDialogViewModel;
            ForexDeNghiTTTongHopDialogViewModel = forexDeNghiTTTongHopDialogViewModel;
            ForexDeNghiThanhToanImportViewModel = forexDeNghiThanhToanImportViewModel;
            SearchCommand = new RelayCommand(obj => { _itemsCollectionView.Refresh(); _deNghiThanhToanTongHopView.Refresh(); });
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            PrintReportCommand = new RelayCommand(obj => OnShowExportDialog());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            TongHopDeNghiTTCommand = new RelayCommand(obj => OnTongHopDeNghiTT(), obj => Items.Where(t => Items_Filter(t)).Any(t => t.IsChecked && t.BIsKhoa));
            _dmChuDauTuService = dmChuDauTuService;
        }

        public override void Init()
        {
            base.Init();
            VoucherTabIndex = VoucherTabIndex.VOUCHER;
            LoadDefault();
            LoadDonVi();
            LoadLoaiDeNghi();
            LoadQuyKeHoach();
            LoadNguonVon();
            LoadLoaiNoiDungChi();
            LoadData();
        }

        private void LoadDefault()
        {
            ItemsFilter = new NhTtThanhToanModel();
        }

        private void OnRemoveFilter()
        {
            ItemsFilter = new NhTtThanhToanModel();
            SelectedDonVi = null;
            SelectedQuyKeHoach = null;
            SelectedLoaiDeNghi = null;
            SelectedNguonVon = null;
            SelectedLoaiNoiDungChi = null;
            LoadData();
        }

        private void LoadDonVi()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            var lstDonVi = _iNsDonViService.FindByCondition(predicate).ToList();
            _itemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(lstDonVi);
        }

        private void LoadLoaiDeNghi()
        {
            var loaiDeNghis = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem(NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.CAP_KINH_PHI), ((int)NhLoaiDeNghi.Type.CAP_KINH_PHI).ToString()),
                new ComboboxItem(NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.TAM_UNG_KINH_PHI), ((int)NhLoaiDeNghi.Type.TAM_UNG_KINH_PHI).ToString()),
                new ComboboxItem(NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.THANH_TOAN), ((int)NhLoaiDeNghi.Type.THANH_TOAN).ToString()),
                new ComboboxItem(NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.TAM_UNG_THEO_CHE_DO), ((int)NhLoaiDeNghi.Type.TAM_UNG_THEO_CHE_DO).ToString())

            };
            _itemsLoaiDeNghi = loaiDeNghis;
        }
        private void LoadQuyKeHoach()
        {
            var quyKeHoachs = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem(LoaiQuyEnum.TypeName.QUY_1, ((int)LoaiQuyEnum.Type.QUY_1).ToString()),
                new ComboboxItem(LoaiQuyEnum.TypeName.QUY_2, ((int)LoaiQuyEnum.Type.QUY_2).ToString()),
                new ComboboxItem(LoaiQuyEnum.TypeName.QUY_3, ((int)LoaiQuyEnum.Type.QUY_3).ToString()),
                new ComboboxItem(LoaiQuyEnum.TypeName.QUY_4, ((int)LoaiQuyEnum.Type.QUY_4).ToString())
            };
            _itemsQuyKeHoach = quyKeHoachs;
        }

        private void LoadNguonVon()
        {
            var lstNguonNs = _iNsNguonNganSachService.FindAll().ToList();
            _itemsNguonVon = _mapper.Map<ObservableCollection<ComboboxItem>>(lstNguonNs);
        }

        private void LoadLoaiNoiDungChi()
        {
            var loaiNoiDungChis = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem(LoaiNoiDungChi.Get((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE), ((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()),
                new ComboboxItem(LoaiNoiDungChi.Get((int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE), ((int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE).ToString())
            };
            _itemsLoaiNoiDungChi = loaiNoiDungChis;
        }

        public override void LoadData(params object[] args)
        {
            // Main process
            Items = new ObservableCollection<NhTtThanhToanModel>();
            var entities = _iNhTtThanhToanService.FindIndex(_sessionService.Current.YearOfWork, 1, true);
            //Không lấy lên những chứng từ con đã được tổng hợp
            //entities = entities.Where(x => x.ParentId == null);

            Items = _mapper.Map<ObservableCollection<NhTtThanhToanModel>>(entities.Where(x => x.ParentId == null));
            // Process when run completed. e.Result
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
            _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
            _itemsCollectionView.Filter = Items_Filter;

            ListDeNghiTongHopChiTiet = _mapper.Map<ObservableCollection<NhTtThanhToanModel>>(entities.Where(x => x.ParentId != null));

            List<NhTtThanhToan> listTongHop = _iNhTtThanhToanService.FindDeNghiTongHop();
            var listTongHopItems = _mapper.Map<List<NhTtThanhToanModel>>(listTongHop);
            listTongHopItems = listTongHopItems.Select(n => { n.IsHangCha = true; return n; }).ToList();
            ListDeNghiTongHop = _mapper.Map<ObservableCollection<NhTtThanhToanModel>>(listTongHopItems);
            for (int i = 0; i < ListDeNghiTongHop.Count; i++)
            {
                ListDeNghiTongHop[i].FTongDeNghiKyNayVnd = ListDeNghiTongHop[i].FTongDeNghiVND;
                ListDeNghiTongHop[i].FTongDeNghiKyNayUsd = ListDeNghiTongHop[i].FTongDeNghiUSD;
                ListDeNghiTongHop[i].FTongDeNghiKyNayEur = ListDeNghiTongHop[i].FTongDeNghiEUR;
                ListDeNghiTongHop[i].FTongDeNghiKyNayNgoaiTeKhac = ListDeNghiTongHop[i].FTongDeNghiNgoaiTeKhac;
                ListDeNghiTongHop[i].TenNguonVon = ListDeNghiTongHop[i].IIdNguonVonId != null ? ItemsNguonVon.Where(n => n.ValueItem.Equals(ListDeNghiTongHop[i].IIdNguonVonId.ToString())).FirstOrDefault().DisplayItem : String.Empty;
                var ele = ListDeNghiTongHop.ElementAt(i);
                ele.HasChildren = ele.BTongHop.HasValue ? ele.BTongHop.Value : false;
                ele.PropertyChanged += DeNghiThanhToanModelPropertyChanged;
            }
        }

        protected override void OnAdd()
        {
            ForexDeNghiThanhToanDialogViewModel.Model = new NhTtThanhToanModel();
            ForexDeNghiThanhToanDialogViewModel.IsDetail = false;
            ForexDeNghiThanhToanDialogViewModel.Init();
            ForexDeNghiThanhToanDialogViewModel.SavedAction = obj => this.OnRefresh();
            ForexDeNghiThanhToanDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            if (VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER))
            {
                ForexDeNghiThanhToanDialogViewModel.Model = SelectedItem;
                ForexDeNghiThanhToanDialogViewModel.IsDetail = false;
                ForexDeNghiThanhToanDialogViewModel.Init();
                ForexDeNghiThanhToanDialogViewModel.SavedAction = obj => this.OnRefresh();
                ForexDeNghiThanhToanDialogViewModel.ShowDialog();
            }
            else
            {
                if (SelectedTongHopItem.BTongHop.HasValue && SelectedTongHopItem.BTongHop.Value)
                {
                    ForexDeNghiTTTongHopDialogViewModel.Model = SelectedTongHopItem;
                    ForexDeNghiTTTongHopDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };
                    ForexDeNghiTTTongHopDialogViewModel.Init();
                    ForexDeNghiTTTongHopDialogViewModel.VoucherAgregates = new ObservableCollection<NhTtThanhToanModel>(ListDeNghiTongHopChiTiet.Where(t => SelectedTongHopItem.Id.Equals(t.ParentId)));
                    var v = new ForexDeNghiThanhToanTongHopDialog { DataContext = ForexDeNghiTTTongHopDialogViewModel };
                    DialogHost.Show(v, "RootDialog");
                }
                else
                {
                    ForexDeNghiThanhToanDialogViewModel.Model = SelectedTongHopItem;
                    ForexDeNghiThanhToanDialogViewModel.IsDetail = false;
                    ForexDeNghiThanhToanDialogViewModel.Init();
                    ForexDeNghiThanhToanDialogViewModel.SavedAction = obj => this.OnRefresh();
                    ForexDeNghiThanhToanDialogViewModel.ShowDialog();
                }
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER))
            {
                ForexDeNghiThanhToanDialogViewModel.Model = SelectedItem;
                ForexDeNghiThanhToanDialogViewModel.IsDetail = true;
                ForexDeNghiThanhToanDialogViewModel.Init();
                ForexDeNghiThanhToanDialogViewModel.SavedAction = obj => this.OnRefresh();
                ForexDeNghiThanhToanDialogViewModel.ShowDialog();
            }
            else
            {
                if (SelectedTongHopItem.BTongHop.HasValue && SelectedTongHopItem.BTongHop.Value)
                {
                    ForexDeNghiTTTongHopDialogViewModel.Model = SelectedTongHopItem;
                    ForexDeNghiTTTongHopDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };
                    ForexDeNghiTTTongHopDialogViewModel.Init();
                    ForexDeNghiTTTongHopDialogViewModel.VoucherAgregates = new ObservableCollection<NhTtThanhToanModel>(ListDeNghiTongHopChiTiet.Where(t => SelectedTongHopItem.Id.Equals(t.ParentId)));
                    var v = new ForexDeNghiThanhToanTongHopDialog { DataContext = ForexDeNghiTTTongHopDialogViewModel };
                    DialogHost.Show(v, "RootDialog");
                }
                else
                {
                    ForexDeNghiThanhToanDialogViewModel.Model = SelectedTongHopItem;
                    ForexDeNghiThanhToanDialogViewModel.IsDetail = true;
                    ForexDeNghiThanhToanDialogViewModel.Init();
                    ForexDeNghiThanhToanDialogViewModel.SavedAction = obj => this.OnRefresh();
                    ForexDeNghiThanhToanDialogViewModel.ShowDialog();
                }
            }
        }

        protected override void OnDelete()
        {
            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                if (VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER))
                    _iNhTtThanhToanService.Delete(_mapper.Map<NhTtThanhToan>(SelectedItem));
                else
                    _iNhTtThanhToanService.Delete(_mapper.Map<NhTtThanhToan>(SelectedTongHopItem));
                OnRefresh();
            }
        }

        protected override void OnLockUnLock()
        {
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            var result = MessageBoxHelper.Confirm(message);
            if (result == MessageBoxResult.Yes)
            {
                if (_voucherTabIndex.Equals(VoucherTabIndex.VOUCHER))
                {
                    foreach (var item in Items)
                    {
                        if (item.IsChecked)
                        {
                            _iNhTtThanhToanService.LockOrUnlock(item.Id, !item.BIsKhoa);
                            item.BIsKhoa = !item.BIsKhoa;
                        }
                    }
                }
                else
                {
                    foreach (var item in Items)
                    {
                        if (item.IsChecked)
                        {
                            _iNhTtThanhToanService.LockOrUnlock(item.Id, !item.BIsKhoa);
                            item.BIsKhoa = !item.BIsKhoa;
                        }
                    }
                }

                LoadData();
                OnPropertyChanged(nameof(IsLock));
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEnabled));
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhTtThanhToanModel item)
            {
                bool result = true;
                if (_selectedDonVi != null)
                {
                    result &= item.IIdMaDonVi.Equals(_selectedDonVi.ValueItem);
                }

                if (_selectedLoaiDeNghi != null)
                {
                    result &= item.ILoaiDeNghi == int.Parse(_selectedLoaiDeNghi.ValueItem);
                }

                if (_selectedQuyKeHoach != null)
                {
                    result &= item.IQuyKeHoach == int.Parse(_selectedQuyKeHoach.ValueItem);
                }

                if (_selectedNguonVon != null)
                {
                    result &= item.IIdNguonVonId == int.Parse(_selectedNguonVon.ValueItem);
                }

                if (_selectedLoaiNoiDungChi != null)
                {
                    result &= item.ILoaiNoiDungChi == int.Parse(_selectedLoaiNoiDungChi.ValueItem);
                }
                if (ItemsFilter != null)
                {
                    if (!string.IsNullOrEmpty(ItemsFilter.SSoDeNghi))
                    {
                        result &= item.SSoDeNghi != null && item.SSoDeNghi.Contains(ItemsFilter.SSoDeNghi, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.DNgayDeNghi.HasValue)
                    {
                        result &= item.DNgayDeNghi.HasValue && DateTime.Compare(item.DNgayDeNghi.Value, ItemsFilter.DNgayDeNghi.Value) == 0;
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.STenNhiemVuChi))
                    {
                        result &= item.STenNhiemVuChi != null && item.STenNhiemVuChi.Contains(ItemsFilter.STenNhiemVuChi, StringComparison.OrdinalIgnoreCase);
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.STenHopDongSoHopDong))
                    {
                        result &= item.STenHopDongSoHopDong != null && item.STenHopDongSoHopDong.Contains(ItemsFilter.STenHopDongSoHopDong, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.INamKeHoach.HasValue)
                    {
                        result &= item.INamKeHoach.HasValue && item.INamKeHoach.Value == ItemsFilter.INamKeHoach.Value;
                    }
                }
                return result;
            }
            return false;
        }

        private bool NHTtDeNghiThanhToanTongHopFilter(object obj)
        {
            var temp = obj as NhTtThanhToanModel;
            if (temp.BTongHop.HasValue && temp.BTongHop.Value)
            {
                var children = ListDeNghiTongHop.Where(t => temp.Id.Equals(t.ParentId));
                foreach (var c in children)
                {
                    if (Items_Filter(c))
                        return true;
                }
            }
            return Items_Filter(obj);
        }

        protected override void OnRefresh()
        {
            Init();
        }

        private void OnShowExportDialog()
        {
            ForexDeNghiThanhToanPrintDialogViewModel.NhTtThanhToanModel = SelectedItem;
            if (SelectedItem.IIdDonVi.HasValue && SelectedItem.IIdDonVi != Guid.Empty)
            {
                var donvi = _iNsDonViService.FindById(SelectedItem.IIdDonVi.Value);
                ForexDeNghiThanhToanPrintDialogViewModel.NhTtThanhToanModel.sTenDonVi = donvi == null ? "" : donvi.TenDonVi;
            }
            if (SelectedItem.IIdChuDauTuId.HasValue && SelectedItem.IIdChuDauTuId != Guid.Empty)
            {
                var cdt = _dmChuDauTuService.FindById(SelectedItem.IIdChuDauTuId.Value);
                ForexDeNghiThanhToanPrintDialogViewModel.NhTtThanhToanModel.sTenChuDauTu = cdt == null ? "" : cdt.STenDonVi;
            }
            ForexDeNghiThanhToanPrintDialogViewModel.Init();
            object content = new ForexDeNghiThanhToanPrintDialog
            {
                DataContext = ForexDeNghiThanhToanPrintDialogViewModel
            };
            DialogHost.Show(content, DemandCheckScreen.ROOT_DIALOG, null, null);
        }

        private void OnTongHopDeNghiTT()
        {
            ForexDeNghiTTTongHopDialogViewModel.Model = new NhTtThanhToanModel();
            ForexDeNghiTTTongHopDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            ForexDeNghiTTTongHopDialogViewModel.Init();
            ForexDeNghiTTTongHopDialogViewModel.Model.Id = Guid.NewGuid();
            ForexDeNghiTTTongHopDialogViewModel.Model.INamKeHoach = Items.FirstOrDefault(t => t.IsChecked && Items_Filter(t)).INamKeHoach;
            ForexDeNghiTTTongHopDialogViewModel.Model.ICoQuanThanhToan = Items.FirstOrDefault(t => t.IsChecked && Items_Filter(t)).ICoQuanThanhToan;
            ForexDeNghiTTTongHopDialogViewModel.Model.IIdNguonVonId = Items.FirstOrDefault(t => t.IsChecked && Items_Filter(t)).IIdNguonVonId;
            ForexDeNghiTTTongHopDialogViewModel.Model.FTongDeNghiVND = Items.Where(t => t.IsChecked && Items_Filter(t)).Sum(n => n.FTongDeNghiKyNayVnd);
            ForexDeNghiTTTongHopDialogViewModel.Model.FTongDeNghiUSD = Items.Where(t => t.IsChecked && Items_Filter(t)).Sum(n => n.FTongDeNghiKyNayUsd);
            ForexDeNghiTTTongHopDialogViewModel.Model.FTongDeNghiEUR = Items.Where(t => t.IsChecked && Items_Filter(t)).Sum(n => n.FTongDeNghiKyNayEur);
            ForexDeNghiTTTongHopDialogViewModel.Model.FTongDeNghiNgoaiTeKhac = Items.Where(t => t.IsChecked && Items_Filter(t)).Sum(n => n.FTongDeNghiKyNayNgoaiTeKhac);
            ForexDeNghiTTTongHopDialogViewModel.Model.BTongHop = true;
            ForexDeNghiTTTongHopDialogViewModel.VoucherAgregates = new ObservableCollection<NhTtThanhToanModel>(Items.Where(t => t.IsChecked && Items_Filter(t)));
            // validation
            var validCoQuanTT = ForexDeNghiTTTongHopDialogViewModel.VoucherAgregates.Select(t => t.ICoQuanThanhToan).Distinct().Count() == 1;
            var validNKH = ForexDeNghiTTTongHopDialogViewModel.VoucherAgregates.Select(t => t.INamKeHoach).Distinct().Count() == 1;
            var validNV = ForexDeNghiTTTongHopDialogViewModel.VoucherAgregates.Select(t => t.IIdNguonVonId).Distinct().Count() == 1;
            var validBTongHop = ForexDeNghiTTTongHopDialogViewModel.VoucherAgregates.All(t => !t.ParentId.HasValue);
            if (!validBTongHop)
            {
                MessageBoxHelper.Info("Không thể tổng hợp bản ghi đã tổng hợp");
                return;
            }
            else if (!validCoQuanTT)
            {
                MessageBoxHelper.Info("Không thể tổng hợp bản ghi có cơ quan thanh toán khác nhau");
                return;
            }
            else if (!validNKH)
            {
                MessageBoxHelper.Info("Không thể tổng hợp bản ghi có năm kế hoạch khác nhau");
                return;
            }
            else if (!validNV)
            {
                MessageBoxHelper.Info("Không thể tổng hợp bản ghi có nguồn vốn khác nhau");
                return;
            }
            var view = new ForexDeNghiThanhToanTongHopDialog { DataContext = ForexDeNghiTTTongHopDialogViewModel };
            DialogHost.Show(view, "RootDialog");
        }

        private void DeNghiThanhToanModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(NhTtThanhToanModel.IsShowChildren)))
            {
                NhTtThanhToanModel model = sender as NhTtThanhToanModel;
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
            int currentIndex = ListDeNghiTongHop.IndexOf(SelectedTongHopItem);
            SelectedTongHopItem.IsShowChildren = true;
            IEnumerable<NhTtThanhToanModel> children = new List<NhTtThanhToanModel>(ListDeNghiTongHopChiTiet.Where(t => SelectedTongHopItem.Id.Equals(t.ParentId)));
            foreach (var item in children)
            {
                //item.Stt = SelectedItem.Stt + "_" + ++stt;
                item.AncestorIds = new HashSet<Guid>();
                item.AncestorIds.Add(SelectedTongHopItem.Id);
                ListDeNghiTongHop.Insert(++currentIndex, item);
            }
        }

        private void OnCollapse()
        {
            SelectedTongHopItem.IsShowChildren = false;
            ListDeNghiTongHop = new ObservableCollection<NhTtThanhToanModel>(ListDeNghiTongHop.Where(t => t.AncestorIds == null || !t.AncestorIds.Contains(SelectedTongHopItem.Id)));
        }

        private void OnImportData()
        {
            ForexDeNghiThanhToanImportViewModel.Init();
            var view = new ForexDeNghiThanhToanImport { DataContext = ForexDeNghiThanhToanImportViewModel };
            ForexDeNghiThanhToanImportViewModel.SavedAction = obj => view.Close();
            view.ShowDialog();
            LoadData();
        }
    }
}
