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
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.ForexAsset;
using VTS.QLNS.CTC.App.ViewModel.Forex.Domestic.ExchangeRateDifference.PrintDialog;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ForexAsset
{
    public class BaoCaoTaiSanIndexViewModel : GridViewModelBase<NhQtTaiSanModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private ICollectionView _itemsCollectionView;
        private ICollectionView _itemsThongKeCollectionView;
        private readonly ISessionService _sessionService;
        private readonly INhQtTaiSanService _nhQtTaiSanService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        public override Type ContentType => typeof(BaoCaoTaiSanIndex);
        public override PackIconKind IconKind => PackIconKind.BagChecked;
        private VoucherTabIndex _voucherTabIndex;
        public VoucherTabIndex VoucherTabIndex
        {
            get => _voucherTabIndex;
            set => SetProperty(ref _voucherTabIndex, value);
        }

        private ObservableCollection<NhQtTaiSanModel> _itemsTaiSan;
        public ObservableCollection<NhQtTaiSanModel> ItemsTaiSan
        {
            get => _itemsTaiSan;
            set => SetProperty(ref _itemsTaiSan, value);
        }

        private NhQtTaiSanModel _selectedTaiSan;
        public NhQtTaiSanModel SelectedTaiSan
        {
            get => _selectedTaiSan;
            set => SetProperty(ref _selectedTaiSan, value);
        }

        private ObservableCollection<NhQtTaiSanModel> _itemsThongKeTaiSan;
        public ObservableCollection<NhQtTaiSanModel> ItemsThongKeTaiSan
        {
            get => _itemsThongKeTaiSan;
            set => SetProperty(ref _itemsThongKeTaiSan, value);
        }

        private NhQtTaiSanModel _selectedThongKeTaiSan;
        public NhQtTaiSanModel SelectedThongKeTaiSan
        {
            get => _selectedThongKeTaiSan;
            set => SetProperty(ref _selectedThongKeTaiSan, value);
        }

        private NhDmTrangThaiSuDungModel _selectedTrangThaiSuDung;
        public NhDmTrangThaiSuDungModel SelectedTrangThaiSuDung
        {
            get => _selectedTrangThaiSuDung;
            set => SetProperty(ref _selectedTrangThaiSuDung, value);
        }
        private ObservableCollection<NhDmTrangThaiSuDungModel> _itemsTrangThaiSuDung;
        public ObservableCollection<NhDmTrangThaiSuDungModel> ItemsTrangThaiSuDung
        {
            get => _itemsTrangThaiSuDung;
            set => SetProperty(ref _itemsTrangThaiSuDung, value);
        }
        private NhDmTinhTrangSuDungModel _selectedTinhTrangSuDung;
        public NhDmTinhTrangSuDungModel SelectedTinhTrangSuDung
        {
            get => _selectedTinhTrangSuDung;
            set => SetProperty(ref _selectedTinhTrangSuDung, value);
        }
        private ObservableCollection<NhDmTinhTrangSuDungModel> _itemsTinhTrangSuDung;
        public ObservableCollection<NhDmTinhTrangSuDungModel> ItemsTinhTrangSuDung
        {
            get => _itemsTinhTrangSuDung;
            set => SetProperty(ref _itemsTinhTrangSuDung, value);
        }
        private NhDmLoaiTaiSanQuyetToanModel _selectedLoaiTaiSanQuyetToan;
        public NhDmLoaiTaiSanQuyetToanModel SelectedLoaiTaiSanQuyetToan
        {
            get => _selectedLoaiTaiSanQuyetToan;
            set => SetProperty(ref _selectedLoaiTaiSanQuyetToan, value);
        }
        private ObservableCollection<NhDmLoaiTaiSanQuyetToanModel> _itemsLoaiTaiSanQuyetToan;
        public ObservableCollection<NhDmLoaiTaiSanQuyetToanModel> ItemsLoaiTaiSanQuyetToan
        {
            get => _itemsLoaiTaiSanQuyetToan;
            set => SetProperty(ref _itemsLoaiTaiSanQuyetToan, value);
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
            set
            {
                SetProperty(ref _selectedDonVi, value);
                LoadDuAn();
                LoadHopDong();
            }
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private NhDaHopDongModel _selectedHopDong;
        public NhDaHopDongModel SelectedHopDong
        {
            get => _selectedHopDong;
            set => SetProperty(ref _selectedHopDong, value);
        }

        private ObservableCollection<NhDaHopDongModel> _itemsHopDong;
        public ObservableCollection<NhDaHopDongModel> ItemsHopDong
        {
            get => _itemsHopDong;
            set => SetProperty(ref _itemsHopDong, value);
        }
        private NhDaDuAnModel _selectedDuAn;
        public NhDaDuAnModel SelectedDuAn
        {
            get => _selectedDuAn;
            set => SetProperty(ref _selectedDuAn, value);
        }
        private ObservableCollection<NhDaDuAnModel> _itemsDuAn;
        public ObservableCollection<NhDaDuAnModel> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }
        private NhQtTaiSanModel _itemsFilter;
        public NhQtTaiSanModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand PrintCommand { get; }
        public BaoCaoTaiSanExportDialogViewModel BaoCaoTaiSanExportDialogViewModel { get; }
        public override string GroupName => MenuItemContants.GROUP_FOREX_ASSET;
        public override string Name => "Báo cáo tài sản hình thành theo hợp đồng";
        public override string Title => "Báo cáo tài sản hình thành theo hợp đồng";
        public override string Description => "Báo cáo tài sản hình thành theo hợp đồng";
        public BaoCaoTaiSanIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INhDaHopDongService nhDaHopDongService,
            INhDaDuAnService nhDaDuAnService,
            INhQtTaiSanService nhQtTaiSanService,
            BaoCaoTaiSanExportDialogViewModel baoCaoTaiSanExportDialogViewModel
        )
        {
            _logger = logger;
            _mapper = mapper;
            _nsDonViService = nsDonViService;
            _sessionService = sessionService;
            _nhDaHopDongService = nhDaHopDongService;
            _nhDaDuAnService = nhDaDuAnService;
            _nhQtTaiSanService = nhQtTaiSanService;
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            SearchCommand = new RelayCommand(obj => OnSearch());
            PrintCommand = new RelayCommand(obj => OnPrint());
            BaoCaoTaiSanExportDialogViewModel = baoCaoTaiSanExportDialogViewModel;
        }

        private void OnSearch()
        {
            if (VoucherTabIndex == VoucherTabIndex.VOUCHER)
            {
                _itemsCollectionView.Refresh();
            }
            else
            {
                _itemsThongKeCollectionView.Refresh();
            }
        }

        private void OnPrint()
        {
            //ResetConditionSearch();
            BaoCaoTaiSanExportDialogViewModel.Model = SelectedItem;
            BaoCaoTaiSanExportDialogViewModel.ItemsTaiSan = ItemsTaiSan.ToList();
            BaoCaoTaiSanExportDialogViewModel.ItemsThongKeTaiSan = ItemsThongKeTaiSan.ToList();
            BaoCaoTaiSanExportDialogViewModel.Init();
            BaoCaoTaiSanExportDialogViewModel.ShowDialogHost();
        }

        public override void Init()
        {
            VoucherTabIndex = VoucherTabIndex.VOUCHER;
            LoadTrangThaiSuDung();
            LoadTinhTrangSuDung();
            LoadLoaiTaiSanQuyetToan();
            LoadHopDong();
            LoadDonVi();
            LoadDuAn();
            LoadData();
        }
        public override void LoadData(params object[] args)
        {
            var dataTaiSan = _nhQtTaiSanService.FindAll().Where(x => x.IIdChungTuTaiSanId != null);
            ItemsTaiSan = _mapper.Map<ObservableCollection<NhQtTaiSanModel>>(dataTaiSan);

            //Convert data
            foreach (var item in ItemsTaiSan)
            {
                if (item.IIdMaDonViId != null) 
                {
                    var donVi = _nsDonViService.Find((Guid)item.IIdMaDonViId);
                    if (donVi != null) item.STenDonVi = $"{donVi.IIDMaDonVi} - {donVi.TenDonVi}";
                }
                if(item.IIdDuAnId != null)
                {
                    var duAn = _nhDaDuAnService.FindById((Guid)item.IIdDuAnId);
                    if (duAn != null) item.STenDuAn = duAn.STenDuAn;
                }
                if (item.IIdHopDongId != null)
                {
                    var duAn = _nhDaHopDongService.FindById((Guid)item.IIdHopDongId);
                    if (duAn != null) item.STenHopDong = duAn.STenHopDong;
                }

                if (item.ILoaiTaiSan == 1) item.SLoaiTaiSan = "Tài sản hữu hình";
                else if (item.ILoaiTaiSan == 2) item.SLoaiTaiSan = "Tài sản vô hình";

                if (item.ITinhTrangSuDung == 1) item.STinhTrangSuDung = "Mới";
                else if (item.ITinhTrangSuDung == 2) item.STinhTrangSuDung = "Cũ";
                else if (item.ITinhTrangSuDung == 3) item.STinhTrangSuDung = "Hết giá trị";

                if (item.ITrangThai == 1) item.STrangThai = "Chưa sử dụng";
                else if (item.ITrangThai == 2) item.STrangThai = "Đang sử dụng";
                else if (item.ITrangThai == 3) item.STrangThai = "Không sử dụng";
            }

            _itemsCollectionView = CollectionViewSource.GetDefaultView(ItemsTaiSan);
            _itemsCollectionView.Filter = DataFilter;

            var dataThongKeTaiSan = _nhQtTaiSanService.FindAllThongKeTaiSan();
            _itemsThongKeTaiSan = _mapper.Map<ObservableCollection<NhQtTaiSanModel>>(dataThongKeTaiSan);
            _itemsThongKeCollectionView = CollectionViewSource.GetDefaultView(ItemsThongKeTaiSan);
            _itemsThongKeCollectionView.Filter = DataThongKeFilter;
            LoadDefault();
            OnPropertyChanged(nameof(ItemsTaiSan));
            OnPropertyChanged(nameof(ItemsThongKeTaiSan));
        }
        protected override void OnRefresh()
        {
            LoadData();
        }
        private void OnRemoveFilter()
        {
            ItemsFilter = new NhQtTaiSanModel();
            SelectedDonVi = null;
            SelectedDuAn = null;
            SelectedHopDong = null;
            LoadData();
        }
        private void LoadDefault()
        {
            ItemsFilter = new NhQtTaiSanModel();
            SelectedDonVi = null;
            SelectedDuAn = null;
            SelectedHopDong = null;
        }
        private bool DataFilter(object obj)
        {
            if (obj is NhQtTaiSanModel item)
            {
                bool result = true;
                if (SelectedDonVi != null)
                {
                    result &= item.IIdMaDonViId.Equals(SelectedDonVi.Id);
                }
                if(SelectedDuAn != null)
                {
                    result &= item.IIdDuAnId.Equals(SelectedDuAn.Id);
                }
                if (SelectedHopDong != null)
                {
                    result &= item.IIdHopDongId.Equals(SelectedHopDong.Id);
                }
                return result;
            }
            return false;
        }

        private bool DataThongKeFilter(object obj)
        {
            if (obj is NhQtTaiSanModel item)
            {
                bool result = true;
                if (SelectedDonVi != null)
                {
                    result &= item.IIdMaDonViId.Equals(SelectedDonVi.Id);
                }
                return result;
            }
            return false;
        }

        private void LoadTrangThaiSuDung()
        {
            _itemsTrangThaiSuDung = new ObservableCollection<NhDmTrangThaiSuDungModel>();
            _itemsTrangThaiSuDung.Add(new NhDmTrangThaiSuDungModel() { Id = 1, STen = "Chưa sử dụng" });
            _itemsTrangThaiSuDung.Add(new NhDmTrangThaiSuDungModel() { Id = 2, STen = "Đang sử dụng" });
            _itemsTrangThaiSuDung.Add(new NhDmTrangThaiSuDungModel() { Id = 3, STen = "Không sử dụng" });
            OnPropertyChanged(nameof(_itemsTrangThaiSuDung));
        }

        private void LoadTinhTrangSuDung()
        {
            _itemsTinhTrangSuDung = new ObservableCollection<NhDmTinhTrangSuDungModel>();
            _itemsTinhTrangSuDung.Add(new NhDmTinhTrangSuDungModel() { Id = 1, STen = "Mới" });
            _itemsTinhTrangSuDung.Add(new NhDmTinhTrangSuDungModel() { Id = 2, STen = "Cũ" });
            _itemsTinhTrangSuDung.Add(new NhDmTinhTrangSuDungModel() { Id = 3, STen = "Hết giá trị" });
            OnPropertyChanged(nameof(_itemsTinhTrangSuDung));
        }

        private void LoadLoaiTaiSanQuyetToan()
        {
            _itemsLoaiTaiSanQuyetToan = new ObservableCollection<NhDmLoaiTaiSanQuyetToanModel>();
            _itemsLoaiTaiSanQuyetToan.Add(new NhDmLoaiTaiSanQuyetToanModel() { Id = 1, STen = "Tài sản hữu hình" });
            _itemsLoaiTaiSanQuyetToan.Add(new NhDmLoaiTaiSanQuyetToanModel() { Id = 2, STen = "Tài sản vô hình" });
            OnPropertyChanged(nameof(_itemsLoaiTaiSanQuyetToan));
        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            ItemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadHopDong()
        {
            if (SelectedDonVi != null)
            {
                var data = _nhDaHopDongService.FindAll().Where(x => x.IIdDonViQuanLyId == SelectedDonVi.Id);
                _itemsHopDong = _mapper.Map<ObservableCollection<NhDaHopDongModel>>(data);
                OnPropertyChanged(nameof(ItemsHopDong));
            }
        }

        private void LoadDuAn()
        {
            if (SelectedDonVi != null)
            {
                var data = _nhDaDuAnService.FindAll().Where(x => x.IIdDonViQuanLyId == SelectedDonVi.Id);
                _itemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(data);
                OnPropertyChanged(nameof(ItemsDuAn));
            }
        }
    }
}
