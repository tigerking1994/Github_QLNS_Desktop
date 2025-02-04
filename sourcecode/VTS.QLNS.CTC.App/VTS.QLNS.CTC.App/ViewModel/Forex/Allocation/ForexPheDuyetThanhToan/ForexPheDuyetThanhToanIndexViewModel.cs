using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexPheDuyetThanhToan;
using VTS.QLNS.CTC.App.View.Forex.ForexAllocation.ForexPheDuyetThanhToan.PrintDialog;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexAllocation.ForexPheDuyetThanhToan.PrintDialog;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;


namespace VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexPheDuyetThanhToan
{ 
    public class ForexPheDuyetThanhToanIndexViewModel : GridViewModelBase<NhTtThanhToanModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly INsDonViService _iNsDonViService;
        private readonly INsNguonNganSachService _iNsNguonNganSachService;
        private readonly INhTtThanhToanService _iNhTtThanhToanService;
        private readonly INhTtThongTriCapPhatChiTietService _nhTtThongTriCapPhatChiTietService;
        private readonly INhThTongHopService _nhThTongHopService;
        private ICollectionView _itemsCollectionView;

        public override string Name => "Phê duyệt thanh toán";
        public override Type ContentType => typeof(View.Forex.ForexAllocation.ForexPheDuyetThanhToan.ForexPheDuyetThanhToanIndex);
        public override string Title => "Phê duyệt thanh toán";
        public override string Description => "Danh sách thông tin quản lý phê duyệt thanh toán";
        public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa;
        public bool IsEnabled => SelectedItem != null && !SelectedItem.BIsKhoa;

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

        public ForexPheDuyetThanhToanDialogViewModel ForexPheDuyetThanhToanDialogViewModel { get; set; }
        public ForexPheDuyetThanhToanPrintDialogViewModel ForexPheDuyetThanhToanPrintDialogViewModel { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public ForexPheDuyetThanhToanIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            INsDonViService iNsDonViService,
            INsNguonNganSachService iNsNguonNganSachService,
            INhTtThanhToanService iNhTtThanhToanService,
            INhThTongHopService nhThTongHopService,
            INhTtThongTriCapPhatChiTietService nhTtThongTriCapPhatChiTietService,
            ForexPheDuyetThanhToanDialogViewModel forexPheDuyetThanhToanDialogViewModel,
            ForexPheDuyetThanhToanPrintDialogViewModel forexPheDuyetThanhToanPrintDialogViewModel)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _iNsDonViService = iNsDonViService;
            _iNsNguonNganSachService = iNsNguonNganSachService;
            _iNhTtThanhToanService = iNhTtThanhToanService;
            _nhThTongHopService = nhThTongHopService;
            _nhTtThongTriCapPhatChiTietService = nhTtThongTriCapPhatChiTietService;

            ForexPheDuyetThanhToanDialogViewModel = forexPheDuyetThanhToanDialogViewModel;
            ForexPheDuyetThanhToanPrintDialogViewModel = forexPheDuyetThanhToanPrintDialogViewModel;
            ForexPheDuyetThanhToanPrintDialogViewModel.ParentPage = this;
            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            PrintReportCommand = new RelayCommand(obj => OnShowExportDialog());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
        }

        public override void Init()
        {
            base.Init();
            LoadDefault();
            LoadDonVi();
            LoadNguonVon();
            LoadLoaiNoiDungChi();
            LoadQuyKeHoach();
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

        private void LoadNguonVon()
        {
            var lstNguonNs = _iNsNguonNganSachService.FindAll().ToList();
            _itemsNguonVon = _mapper.Map<ObservableCollection<ComboboxItem>>(lstNguonNs);
        }

        private void LoadLoaiNoiDungChi()
        {
            var loaiNoiDungChis = new ObservableCollection<ComboboxItem>();
            loaiNoiDungChis.Add(new ComboboxItem("Chi bằng ngoại tệ", "1"));
            loaiNoiDungChis.Add(new ComboboxItem("Chi bằng nội tệ", "2"));
            _itemsLoaiNoiDungChi = loaiNoiDungChis;
        }
        private void LoadQuyKeHoach()
        {
            var quyKeHoachs = new ObservableCollection<ComboboxItem>();
            quyKeHoachs.Add(new ComboboxItem("Quý 1", "1"));
            quyKeHoachs.Add(new ComboboxItem("Quý 2", "2"));
            quyKeHoachs.Add(new ComboboxItem("Quý 3", "3"));
            quyKeHoachs.Add(new ComboboxItem("Quý 4", "4"));
            _itemsQuyKeHoach = quyKeHoachs;
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    // Main process
                    Items = new ObservableCollection<NhTtThanhToanModel>();
                    e.Result = _iNhTtThanhToanService.FindIndex(_sessionService.Current.YearOfWork, -1, false);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        Items = _mapper.Map<ObservableCollection<NhTtThanhToanModel>>(e.Result);
                        List<string> LstIdTTDaCoThongTri = _nhTtThongTriCapPhatChiTietService.FindAll().Where(n => n.IIdPheDuyetThanhToanId != null).Select(n => n.IIdPheDuyetThanhToanId.ToString()).ToList();
                        foreach (var item in Items)
                        {
                            if (LstIdTTDaCoThongTri.Contains(item.Id.ToString())) 
                            {
                                item.BIsKhoa = true;
                            }
                        }
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
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd()
        {
            ForexPheDuyetThanhToanDialogViewModel.Model = new NhTtThanhToanModel();
            ForexPheDuyetThanhToanDialogViewModel.IsDetail = false;
            ForexPheDuyetThanhToanDialogViewModel.IsEdit = false;
            ForexPheDuyetThanhToanDialogViewModel.Init();
            ForexPheDuyetThanhToanDialogViewModel.SavedAction = obj => this.OnRefresh();
            ForexPheDuyetThanhToanDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            ForexPheDuyetThanhToanDialogViewModel.Model = SelectedItem;
            ForexPheDuyetThanhToanDialogViewModel.IsDetail = false;
            ForexPheDuyetThanhToanDialogViewModel.IsEdit = true;
            ForexPheDuyetThanhToanDialogViewModel.Init();
            ForexPheDuyetThanhToanDialogViewModel.SavedAction = obj => this.OnRefresh();
            ForexPheDuyetThanhToanDialogViewModel.ShowDialog();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            ForexPheDuyetThanhToanDialogViewModel.Model = SelectedItem;
            ForexPheDuyetThanhToanDialogViewModel.IsDetail = true;
            ForexPheDuyetThanhToanDialogViewModel.IsEdit = false;
            ForexPheDuyetThanhToanDialogViewModel.Init();
            ForexPheDuyetThanhToanDialogViewModel.SavedAction = obj => this.OnRefresh();
            ForexPheDuyetThanhToanDialogViewModel.ShowDialog();
        }

        protected override void OnDelete()
        {
            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                //if(SelectedItem.ITrangThai == 2)
                //{
                //    if(SelectedItem.ILoaiDeNghi == 1)
                //        _nhThTongHopService.InsertNHTongHop_Tang("TTCP", 3, SelectedItem.Id, null);
                //    if(SelectedItem.ILoaiDeNghi == 3)
                //    {
                //        _nhThTongHopService.InsertNHTongHop_Tang("TTCP", 3, SelectedItem.Id, null);
                //        _nhThTongHopService.DeleteNHTongHop_Giam("TTCP", SelectedItem.Id);
                //    }
                //    else
                //        _nhThTongHopService.DeleteNHTongHop_Giam("TTCP", SelectedItem.Id);
                //}
                _iNhTtThanhToanService.Delete(_mapper.Map<NhTtThanhToan>(SelectedItem));
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
                        _iNhTtThanhToanService.LockOrUnlock(item.Id, !item.BIsKhoa);
                        item.BIsKhoa = !item.BIsKhoa;
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

        private void OnShowExportDialog()
        {
            NhTtThanhToan entity = _iNhTtThanhToanService.FindById(SelectedItem.Id);
            ForexPheDuyetThanhToanPrintDialogViewModel.nhDeNghiThanhToanModels = _mapper.Map<NhTtThanhToanModel>(entity);
            ForexPheDuyetThanhToanPrintDialogViewModel.Init();
            object content = new ForexPheDuyetThanhToanPrintDialog
            {
                DataContext = ForexPheDuyetThanhToanPrintDialogViewModel
            };
            DialogHost.Show(content, DemandCheckScreen.ROOT_DIALOG, null, null);
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
                    if (ItemsFilter.DNgayPheDuyet.HasValue)
                    {
                        result &= item.DNgayPheDuyet.HasValue && DateTime.Compare(item.DNgayPheDuyet.Value, ItemsFilter.DNgayPheDuyet.Value) == 0;
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

        protected override void OnRefresh()
        {
            Init();
        }
    }
}
