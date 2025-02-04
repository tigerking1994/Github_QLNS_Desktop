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
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.ForexAsset;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ForexAsset
{
    //public class AssetIndexViewModel : GridViewModelBase<NhQtTaiSanModel>
    public class AssetIndexViewModel : GridViewModelBase<NhQtChungTuTaiSanModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDmLoaiTaiSanService _nhDmLoaiTaiSanService;
        private readonly INhQtTaiSanService _nhQtTaiSanService;
        private readonly INhQtChungTuTaiSanService _service;
        private ICollectionView _itemsCollectionView;

        public override string GroupName => MenuItemContants.GROUP_FOREX_ASSET;
        public override string Name => "Tài sản";
        public override string Title => "Tài sản hình thành theo hợp đồng";
        public override string Description => "Danh sách tài sản hình thành theo hợp đồng";
        public override Type ContentType => typeof(AssetIndex);
        public override PackIconKind IconKind => PackIconKind.BagChecked;

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

        //private NhDmLoaiTaiSanModel _selectedLoaiTaiSan;
        //public NhDmLoaiTaiSanModel SelectedLoaiTaiSan
        //{
        //    get => _selectedLoaiTaiSan;
        //    set => SetProperty(ref _selectedLoaiTaiSan, value);
        //}F
        //private IEnumerable<NhDmLoaiTaiSanModel> _itemsLoaiTaiSan;
        //public IEnumerable<NhDmLoaiTaiSanModel> ItemsLoaiTaiSan
        //{
        //    get => _itemsLoaiTaiSan;
        //    set => SetProperty(ref _itemsLoaiTaiSan, value);
        //}


        //private NhDmTrangThaiSuDungModel _selectedTrangThaiSuDung;
        //public NhDmTrangThaiSuDungModel SelectedTrangThaiSuDung
        //{
        //    get => _selectedTrangThaiSuDung;
        //    set => SetProperty(ref _selectedTrangThaiSuDung, value);
        //}
        //private ObservableCollection<NhDmTrangThaiSuDungModel> _itemsTrangThaiSuDung;
        //public ObservableCollection<NhDmTrangThaiSuDungModel> ItemsTrangThaiSuDung
        //{
        //    get => _itemsTrangThaiSuDung;
        //    set => SetProperty(ref _itemsTrangThaiSuDung, value);
        //}


        private NhQtChungTuTaiSanModel _itemsFilter;
        public NhQtChungTuTaiSanModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }
        //private NhQtTaiSanModel _itemsFilter;
        //public NhQtTaiSanModel ItemsFilter
        //{
        //    get => _itemsFilter;
        //    set => SetProperty(ref _itemsFilter, value);
        //}

        public AssetDialogViewModel AssetDialogViewModel { get; set; }
        public AssetDetailDialogViewModel AssetDetailDialogViewModel { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }

        public AssetIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INhDmLoaiTaiSanService nhDmLoaiTaiSanService,
            AssetDialogViewModel assetDialogViewModel,
            AssetDetailDialogViewModel assetDetailDialogViewModel,
            INhQtTaiSanService nhQtTaiSanService,
            INhQtChungTuTaiSanService service
            )
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nhDmLoaiTaiSanService = nhDmLoaiTaiSanService;
            _service = service;
            _nhQtTaiSanService = nhQtTaiSanService;

            AssetDialogViewModel = assetDialogViewModel;
            AssetDetailDialogViewModel = assetDetailDialogViewModel;
            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate());
            DeleteCommand = new RelayCommand(o => OnDelete());
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock(), obj => SelectedItem != null);
        }

        public override void Init()
        {
            LoadDefault();
            //LoadDonVi();
            //LoadLoaiTaiSan();
            //LoadTrangThaiSuDung();
            LoadData();
        }

        private void LoadDefault()
        {
            //ItemsFilter = new NhQtTaiSanModel();
            ItemsFilter = new NhQtChungTuTaiSanModel();
        }

        //private void LoadDonVi()
        //{
        //    try
        //    {
        //        var data = _nsDonViService.FindInternalByNamLamViec(_sessionService.Current.YearOfWork);
        //        ItemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message, ex);
        //    }
        //}
        //private void LoadLoaiTaiSan()
        //{
        //    var data = _nhDmLoaiTaiSanService.FindAll();
        //    _itemsLoaiTaiSan = _mapper.Map<IEnumerable<NhDmLoaiTaiSanModel>>(data);
        //}
        //private void LoadTrangThaiSuDung()
        //{
        //    _itemsTrangThaiSuDung = new ObservableCollection<NhDmTrangThaiSuDungModel>();
        //    _itemsTrangThaiSuDung.Add(new NhDmTrangThaiSuDungModel() { Id = 1, STen = "Chưa sử dụng" });
        //    _itemsTrangThaiSuDung.Add(new NhDmTrangThaiSuDungModel() { Id = 2, STen = "Đang sử dụng" });
        //    _itemsTrangThaiSuDung.Add(new NhDmTrangThaiSuDungModel() { Id = 2, STen = "Không sử dụng" });
        //    OnPropertyChanged(nameof(_itemsTrangThaiSuDung));
        //}

        public override void LoadData(params object[] args)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                //Items = new ObservableCollection<NhQtTaiSanModel>();
                Items = new ObservableCollection<NhQtChungTuTaiSanModel>();
                e.Result = _service.FindAll();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    //Items = _mapper.Map<ObservableCollection<NhQtTaiSanModel>>(e.Result);
                    Items = _mapper.Map<ObservableCollection<NhQtChungTuTaiSanModel>>(e.Result);
                    // Process when run completed. e.Result
                    if (Items != null && Items.Count > 0)
                    {
                        SelectedItem = Items.FirstOrDefault();
                        //foreach (var item in Items)
                        //{
                        //    var loaiTaiSan = _nhDmLoaiTaiSanService.FindAll().Where(n => n.Id == item.IIdLoaiTaiSan).FirstOrDefault();
                        //    if (item.ITrangThai == 1) { item.STrangThai = "Chưa sử dụng"; };
                        //    if (item.ITrangThai == 2) { item.STrangThai = "Đang sử dụng"; };
                        //    if (item.ITrangThai == 3) { item.STrangThai = "Không sử dụng"; };
                        //    if (loaiTaiSan != null) item.SLoaiTaiSan = loaiTaiSan.STenLoaiTaiSan;
                        //}
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
            AssetDialogViewModel.IsDetail = false;
            AssetDialogViewModel.Model = new NhQtChungTuTaiSanModel();
            AssetDialogViewModel.Init();
            AssetDialogViewModel.SavedAction = obj => this.OnRefresh();
            AssetDialogViewModel.Show();
        }

        protected override void OnUpdate()
        {
            AssetDialogViewModel.IsDetail = false;
            AssetDialogViewModel.Model = SelectedItem;
            AssetDialogViewModel.Init();
            AssetDialogViewModel.SavedAction = obj => this.OnRefresh();
            AssetDialogViewModel.Show();

            //AssetDetailDialogViewModel.Init();
            //var view = new AssetDetailDialog { DataContext = AssetDetailDialogViewModel };
            //DialogHost.Show(view, "RootDialog", null, null);
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            AssetDialogViewModel.IsDetail = true;
            AssetDialogViewModel.Model = SelectedItem;
            AssetDialogViewModel.Init();
            AssetDialogViewModel.SavedAction = obj => this.OnRefresh();
            AssetDialogViewModel.Show();
        }

        private void OnRemoveFilter()
        {
            //ItemsFilter = new NhQtTaiSanModel();
            ItemsFilter = new NhQtChungTuTaiSanModel();
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
                //Xóa chứng từ
                _service.Delete(SelectedItem.Id);
                //Xóa list tài sản theo chứng từ
                var listTaiSan = _nhQtTaiSanService.FindAll().Where(x => x.IIdChungTuTaiSanId == SelectedItem.Id);
                foreach (var item in listTaiSan)
                {
                    _nhQtTaiSanService.Delete(item.Id);
                }
                OnRefresh();
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

        private bool Items_Filter(object obj)
        {
            //if (obj is NhQtTaiSanModel item)
            if (obj is NhQtChungTuTaiSanModel item)
            {
                bool result = true;
                if (ItemsFilter != null)
                {
                    if (!string.IsNullOrEmpty(ItemsFilter.SSoChungTu))
                    {
                        result &= item.SSoChungTu != null && item.SSoChungTu.Contains(ItemsFilter.SSoChungTu, StringComparison.OrdinalIgnoreCase);
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.STenChungTu))
                    {
                        result &= item.STenChungTu != null && item.STenChungTu.Contains(ItemsFilter.STenChungTu, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.DNgayChungTu != null)
                    {
                        result &= item.DNgayChungTu.HasValue && item.DNgayChungTu.Value.Date == ItemsFilter.DNgayChungTu.Value.Date;
                    }
                    //if (!string.IsNullOrEmpty(ItemsFilter.SMaTaiSan))
                    //{
                    //    result &= item.SMaTaiSan != null && item.SMaTaiSan.Contains(ItemsFilter.SMaTaiSan, StringComparison.OrdinalIgnoreCase);
                    //}
                    //if (!string.IsNullOrEmpty(ItemsFilter.STenTaiSan))
                    //{
                    //    result &= item.STenTaiSan != null && item.STenTaiSan.Contains(ItemsFilter.STenTaiSan, StringComparison.OrdinalIgnoreCase);
                    //}
                    //if (SelectedLoaiTaiSan != null)
                    //{
                    //    result &= result && item.SLoaiTaiSan == SelectedLoaiTaiSan.STenLoaiTaiSan;
                    //}
                    //if (SelectedTrangThaiSuDung != null)
                    //{
                    //    result &= result && item.STrangThai == SelectedTrangThaiSuDung.STen;
                    //}
                    //if (ItemsFilter.DNgayBatDauSuDung != null)
                    //{
                    //    result &= item.DNgayBatDauSuDung.HasValue && item.DNgayBatDauSuDung.Value.Date == ItemsFilter.DNgayBatDauSuDung.Value.Date;
                    //}
                }
                return result;
            }
            return false;
        }
    }
}
