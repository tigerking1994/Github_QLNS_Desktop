using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Command;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.ForexAsset;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Service.Impl;
using System.ComponentModel;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ForexAsset
{
    //public class AssetDialogViewModel : DialogCurrencyAttachmentViewModelBase<NhQtTaiSanModel>
    public class AssetDialogViewModel : DialogCurrencyAttachmentViewModelBase<NhQtChungTuTaiSanModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        //private readonly INhQtTaiSanChiTietService _nhQtTaiSanChiTietService;
        private readonly INhQtTaiSanService _nhQtTaiSanService;
        private readonly INhDmLoaiTaiSanService _nhDmLoaiTaiSanService;
        private readonly INhDmDonViTinhService _nhDmDonViTinhService;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly IServiceProvider _provider;
        private static Guid iIDDon_Vi = Guid.Empty;

        private readonly INhDmXuatXuService _nhDmXuatXuService;
        //private readonly INhQtTaiSanService _service;
        private readonly INhQtChungTuTaiSanService _service;
        private SessionInfo _sessionInfo;

        //public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD_CHU_TRUONG_DAU_TU_DIALOG;
        public override string Title => "Tài sản hình thành theo hợp đồng";
        public override string Name => "Tài sản hình thành theo hợp đồng";
        public override Type ContentType => typeof(AssetDialog);
        public bool IsDetail { get; set; }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();

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
                if (SetProperty(ref _selectedDonVi, value))
                {
                    LoadDuAn();
                    LoadHopDong();
                }
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
            set => SetProperty(ref _selectedNguonVon, value);
        }

        private ObservableCollection<NhDmLoaiThanhToanModel> _itemsLoaiThanhToan;
        public ObservableCollection<NhDmLoaiThanhToanModel> ItemsLoaiThanhToan
        {
            get => _itemsLoaiThanhToan;
            set => SetProperty(ref _itemsLoaiThanhToan, value);
        }

        private NhDmLoaiTaiSanModel _selectedLoaiTaiSan;
        public NhDmLoaiTaiSanModel SelectedLoaiTaiSan
        {
            get => _selectedLoaiTaiSan;
            set => SetProperty(ref _selectedLoaiTaiSan, value);
        }
        private IEnumerable<NhDmLoaiTaiSanModel> _itemsLoaiTaiSan;
        public IEnumerable<NhDmLoaiTaiSanModel> ItemsLoaiTaiSan
        {
            get => _itemsLoaiTaiSan;
            set => SetProperty(ref _itemsLoaiTaiSan, value);
        }

        //private ObservableCollection<NhQtTaiSanChiTietModel> _itemsHangMuc;
        //public ObservableCollection<NhQtTaiSanChiTietModel> ItemsHangMuc
        //{
        //    get => _itemsHangMuc;
        //    set => SetProperty(ref _itemsHangMuc, value);
        //}
        private ObservableCollection<NhQtTaiSanModel> _itemsTaiSan;
        public ObservableCollection<NhQtTaiSanModel> ItemsTaiSan
        {
            get => _itemsTaiSan;
            set => SetProperty(ref _itemsTaiSan, value);
        }
        //private ObservableCollection<NhDmXuatXuModel> _itemsXuatXu;
        //public ObservableCollection<NhDmXuatXuModel> ItemsXuatXu
        //{
        //    get => _itemsXuatXu;
        //    set => SetProperty(ref _itemsXuatXu, value);
        //}

        //private ObservableCollection<NhDmDonViTinhModel> _itemsDonViTinh;
        //public ObservableCollection<NhDmDonViTinhModel> ItemsDonViTinh
        //{
        //    get => _itemsDonViTinh;
        //    set => SetProperty(ref _itemsDonViTinh, value);
        //}

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

        //private NhQtTaiSanChiTietModel _selectedHangMuc;
        //public NhQtTaiSanChiTietModel SelectedHangMuc
        //{
        //    get => _selectedHangMuc;
        //    set => SetProperty(ref _selectedHangMuc, value);
        //}

        private NhQtTaiSanModel _selectedTaiSan;
        public NhQtTaiSanModel SelectedTaiSan
        {
            get => _selectedTaiSan;
            set => SetProperty(ref _selectedTaiSan, value);
        }

        private ObservableCollection<NhDmCoQuanThanhToanModel> _itemsCoQuanThanhToan;
        public ObservableCollection<NhDmCoQuanThanhToanModel> ItemsCoQuanThanhToan
        {
            get => _itemsCoQuanThanhToan;
            set => SetProperty(ref _itemsCoQuanThanhToan, value);
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
        //public RelayCommand AddHangMucCommand { get; }
        //public RelayCommand DeleteHangMucCommand { get; }
        public RelayCommand AddTaiSanCommand { get; }
        public RelayCommand DeleteTaiSanCommand { get; }
        public RelayCommand UpdateTaiSanCommand { get; }
        public AssetDetailDialogViewModel AssetDetailDialogViewModel { get; set; }

        public AssetDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            //INhQtTaiSanService service,
            INhQtChungTuTaiSanService service,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            IStorageServiceFactory storageServiceFactory,
            INhDmLoaiTaiSanService nhDmLoaiTaiSanService,
            INhDmDonViTinhService nhDmDonViTinhService,
            INhDmXuatXuService nhDmXuatXuService,
            //INhQtTaiSanChiTietService nhQtTaiSanChiTietService,
            INhQtTaiSanService nhQtTaiSanService,
            INhDaHopDongService nhDaHopDongService,
            INhDaDuAnService nhDaDuAnService,
            IAttachmentService attachService,
            IServiceProvider serviceProvider,
            AssetDetailDialogViewModel assetDetailDialogViewModel)
            : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDmLoaiTaiSanService = nhDmLoaiTaiSanService;
            _nhDmDonViTinhService = nhDmDonViTinhService;
            _provider = serviceProvider;
            _nhDmXuatXuService = nhDmXuatXuService;
            _nhDaHopDongService = nhDaHopDongService;
            //_nhQtTaiSanChiTietService = nhQtTaiSanChiTietService;
            _nhQtTaiSanService = nhQtTaiSanService;
            _nhDaDuAnService = nhDaDuAnService;
            _service = service;
            AssetDetailDialogViewModel = assetDetailDialogViewModel;

            //AddHangMucCommand = new RelayCommand(obj => OnAddHangMuc());
            //DeleteHangMucCommand = new RelayCommand(obj => OnDeleteHangMuc());
            AddTaiSanCommand = new RelayCommand(obj => OnAddTaiSan());
            DeleteTaiSanCommand = new RelayCommand(obj => OnDeleteTaiSan());
            UpdateTaiSanCommand = new RelayCommand(obj => OnUpdateTaiSan(obj));
        }

        protected override void OnModelPropertyChanged()
        {
            OnPropertyChanged(nameof(IsEditable));
        }

        public override void Init()
        {
            LoadDefault();
            LoadData();
            LoadLoaiTaiSan();
            LoadTrangThaiSuDung();
            LoadTinhTrangSuDung();
            LoadLoaiTaiSanQuyetToan();
            //LoadDonViTinh();
            LoadHopDong();
            LoadDonVi();
            LoadDuAn();
            //LoadXuatXu();
        }

        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
        }
        private void LoadDonVi()
        {
            //_itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(_nsDonViService.FindAll());
            //OnPropertyChanged(nameof(ItemsDonVi));
            var data = _nsDonViService.FindAll().Where(x => x.NamLamViec == _sessionService.Current.YearOfWork).OrderBy(x => x.IIDMaDonVi);
            ItemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }
        private void LoadHopDong()
        {
            //var data = _nhDaHopDongService.FindAll().Where(n => n.STenHopDong != null && n.STenHopDong != "");
            //_itemsHopDong = _mapper.Map<ObservableCollection<NhDaHopDongModel>>(data);
            //OnPropertyChanged(nameof(ItemsHopDong));

            _itemsHopDong = new ObservableCollection<NhDaHopDongModel>();
            if (_selectedDonVi != null)
            {
                List<DonViQuery> listHopDong = _nsDonViService.FindAllHopDongByDonViId(_selectedDonVi.Id).ToList();
                _itemsHopDong = _mapper.Map<ObservableCollection<NhDaHopDongModel>>(listHopDong);
            }
            OnPropertyChanged(nameof(ItemsHopDong));
        }
        private void LoadLoaiTaiSan()
        {
            var data = _nhDmLoaiTaiSanService.FindAll();
            _itemsLoaiTaiSan = _mapper.Map<IEnumerable<NhDmLoaiTaiSanModel>>(data);
        }
        private void LoadDuAn()
        {
            //var data = _nhDaDuAnService.FindAll();
            //_itemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(data);
            //OnPropertyChanged(nameof(ItemsDuAn));

            _itemsDuAn = new ObservableCollection<NhDaDuAnModel>();
            if (_selectedDonVi != null)
            {
                List<DonViQuery> listHopDong = _nsDonViService.FindAllDuAnByDonViId(_selectedDonVi.Id).ToList();
                _itemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(listHopDong);
            }
            OnPropertyChanged(nameof(ItemsHopDong));
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

        //private void LoadDataTaiSanChiTietByTaiSan()
        //{
        //    List<NhQtTaiSanChiTiet> listData = _nhQtTaiSanChiTietService.FindByTaiSanChiTietId(Model.Id).ToList();
        //    ItemsHangMuc = _mapper.Map<ObservableCollection<NhQtTaiSanChiTietModel>>(listData);
        //    OnPropertyChanged(nameof(ItemsHangMuc));
        //}
        private void LoadDataTaiSanByChungTuTaiSan()
        {
            List<NhQtTaiSanQuery> listData = _nhQtTaiSanService.FindByTaiSanByIdChungTu(Model.Id).ToList();
            ItemsTaiSan = _mapper.Map<ObservableCollection<NhQtTaiSanModel>>(listData);
            var listDonVi = _mapper.Map<ObservableCollection<DonViModel>>(_nsDonViService.FindAll());
            foreach (var item in ItemsTaiSan)
            {
                item.ItemsDonVi = listDonVi;
                item.SelectedDonVi = listDonVi.FirstOrDefault(n => item.IIdMaDonViId == n.Id);
            }
            OnPropertyChanged(nameof(ItemsTaiSan));
        }

        private void OnAddTaiSan()
        {
            NhQtTaiSanModel newItem = new NhQtTaiSanModel();
            //newItem.Id = Guid.NewGuid();
            //newItem.SMoTaTaiSan = "aa";
           ItemsTaiSan.Insert(ItemsTaiSan.Count, newItem);
            OnPropertyChanged(nameof(ItemsTaiSan));
        }

        private void OnDeleteTaiSan()
        {
            if (SelectedTaiSan != null)
            {
                SelectedTaiSan.IsDeleted = !SelectedTaiSan.IsDeleted;
            }
            OnPropertyChanged(nameof(ItemsTaiSan));
        }
        private void OnRefresh()
        {
            LoadData();
        }
        private void OnUpdateTaiSan(object obj)
        {

            //AssetDetailDialogViewModel.Model = SelectedTaiSan;
            //AssetDetailDialogViewModel.Model.IIdChungTuTaiSanId = Model.Id;
            //AssetDetailDialogViewModel.Init();
            //AssetDetailDialogViewModel.SavedAction = obj => this.OnRefresh();
            //AssetDetailDialogViewModel.Show();

            try
            {
                NhQtTaiSanModel listtaisan = new NhQtTaiSanModel();
                var iIDDon_Vi = listtaisan.IIdMaDonViId;
                if (SelectedTaiSan != null)
                {
                    DataGrid dataGrid = obj as DataGrid;

                    if (dataGrid.CurrentCell.Column.SortMemberPath.Equals("STenTaiSan"))
                    {
                        GenericControlCustomViewModel<NhDmLoaiTaiSanModel, Core.Domain.NhDmLoaiTaiSan, NhDmLoaiTaiSanService> viewModelBase = new GenericControlCustomViewModel<NhDmLoaiTaiSanModel, Core.Domain.NhDmLoaiTaiSan, NhDmLoaiTaiSanService>((NhDmLoaiTaiSanService)_nhDmLoaiTaiSanService, _mapper, _sessionService, _provider)
                        {
                            Name = "Danh mục loại tài sản",
                            Title = "Danh mục loại tài sản",
                            Description = "Danh sách Danh mục loại tài sản",
                            IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                            IsDialog = true,
                        };
                        GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(viewModelBase);
                        VTS.QLNS.CTC.App.View.Shared.GenericControlCustomWindow GenericControlCustomWindow = new VTS.QLNS.CTC.App.View.Shared.GenericControlCustomWindow
                        {
                            DataContext = genericControlCustomWindowViewModel,
                            Title = "Danh mục loại tài sản",
                        };

                        GenericControlCustomWindow.SavedAction = obj =>
                        {
                            try
                            {
                                VTS.QLNS.CTC.App.Model.NhDmLoaiTaiSanModel item2 = (VTS.QLNS.CTC.App.Model.NhDmLoaiTaiSanModel)obj;

                                if (item2 != null && SelectedTaiSan != null)
                                {
                                    foreach (var item in ItemsTaiSan)
                                    {
                                        if ((item.Id == null) || item.Id == Guid.Empty)
                                        {
                                            if (item.IIdLoaiTaiSan.Equals(SelectedTaiSan.IIdLoaiTaiSan))
                                            {
                                                item.STenTaiSan = item2.STenLoaiTaiSan;
                                                item.IIdLoaiTaiSan = item2.Id;
                                                item.SMaTaiSan = item2.SMaLoaiTaiSan;
                                            }
                                        }
                                        else if (item.Id != null && item.Id != Guid.Empty)
                                        {
                                            if (item.Id.Equals(SelectedTaiSan.Id))
                                            {
                                                item.STenTaiSan = item2.STenLoaiTaiSan;
                                                item.IIdLoaiTaiSan = item2.Id;
                                                item.SMaTaiSan = item2.SMaLoaiTaiSan;
                                                _nhQtTaiSanService.Update(_mapper.Map<NhQtTaiSan>(item));
                                                OnPropertyChanged(nameof(item));
                                            }
                                        }
                                    }
                                }

                                GenericControlCustomWindow.Close();
                                OnPropertyChanged(nameof(ItemsTaiSan));
                            }
                            catch (Exception ex)
                            {
                                _logger.Error(ex.Message, ex);
                            }
                        };

                        viewModelBase.GenericControlCustomWindow = GenericControlCustomWindow;
                        GenericControlCustomWindow.Show();

                    }
                    else if (dataGrid.CurrentCell.Column.SortMemberPath.Equals("STenDuAn"))
                    {
                      
                        GenericControlCustomViewModel<NhDaDuAnModel, Core.Domain.NhDaDuAn, NhDaDuAnService> viewModelBase = new GenericControlCustomViewModel<NhDaDuAnModel, Core.Domain.NhDaDuAn, NhDaDuAnService>((NhDaDuAnService)_nhDaDuAnService, _mapper, _sessionService, _provider)
                        {
                            Name = "Danh mục loại dự án",
                            Title = "Danh mục loại dự án",
                            Description = "Danh sách Danh mục loại dự án",
                            IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                            IsDialog = true,
                            iID_DonViID = SelectedTaiSan.IIdMaDonViId
                        };
                        GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(viewModelBase);
                        VTS.QLNS.CTC.App.View.Shared.GenericControlCustomWindow GenericControlCustomWindow = new VTS.QLNS.CTC.App.View.Shared.GenericControlCustomWindow
                        {
                            DataContext = genericControlCustomWindowViewModel,
                            Title = "Danh mục loại dự án",
                        };

                        GenericControlCustomWindow.SavedAction = obj =>
                        {
                            try
                            {
                                VTS.QLNS.CTC.App.Model.NhDaDuAnModel item2 = (VTS.QLNS.CTC.App.Model.NhDaDuAnModel)obj;

                                if (item2 != null && SelectedTaiSan != null)
                                {
                                    foreach (var item in ItemsTaiSan)
                                    {
                                        if ((item.Id == null) || item.Id == Guid.Empty)
                                        {
                                            if (item.IIdDuAnId.Equals(SelectedTaiSan.IIdDuAnId))
                                            {
                                                item.STenDuAn = item2.STenDuAn;
                                                item.IIdDuAnId = item2.Id;
                                            }
                                        }
                                        else if (item.Id != null && item.Id != Guid.Empty)
                                        {
                                            if (item.Id.Equals(SelectedTaiSan.Id))
                                            {
                                                item.STenDuAn = item2.STenDuAn;
                                                item.IIdDuAnId = item2.Id;
                                                _nhQtTaiSanService.Update(_mapper.Map<NhQtTaiSan>(item));
                                                OnPropertyChanged(nameof(item));
                                            }
                                        }
                                    }
                                }

                                GenericControlCustomWindow.Close();
                                OnPropertyChanged(nameof(ItemsTaiSan));
                            }
                            catch (Exception ex)
                            {
                                _logger.Error(ex.Message, ex);
                            }
                        };

                        viewModelBase.GenericControlCustomWindow = GenericControlCustomWindow;
                        GenericControlCustomWindow.Show();
                    }
                    else if (dataGrid.CurrentCell.Column.SortMemberPath.Equals("STenHopDong"))
                    {
                        GenericControlCustomViewModel<NhDaHopDongModel, Core.Domain.NhDaHopDong, NhDaHopDongService> viewModelBase = new GenericControlCustomViewModel<NhDaHopDongModel, Core.Domain.NhDaHopDong, NhDaHopDongService>((NhDaHopDongService)_nhDaHopDongService, _mapper, _sessionService, _provider)
                        {
                            Name = "Danh mục loại hợp đồng",
                            Title = "Danh mục loại hợp đồng",
                            Description = "Danh sách Danh mục loại hợp đồng",
                            IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                            IsDialog = true,
                            iID_DonViID = SelectedTaiSan.IIdMaDonViId
                        };
                        GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(viewModelBase);
                        VTS.QLNS.CTC.App.View.Shared.GenericControlCustomWindow GenericControlCustomWindow = new VTS.QLNS.CTC.App.View.Shared.GenericControlCustomWindow
                        {
                            DataContext = genericControlCustomWindowViewModel,
                            Title = "Danh mục loại hợp đồng",
                        };

                        GenericControlCustomWindow.SavedAction = obj =>
                        {
                            try
                            {
                                VTS.QLNS.CTC.App.Model.NhDaHopDongModel item2 = (VTS.QLNS.CTC.App.Model.NhDaHopDongModel)obj;

                                if (item2 != null && SelectedTaiSan != null)
                                {
                                    foreach (var item in ItemsTaiSan)
                                    {
                                        if ((item.Id == null) || item.Id == Guid.Empty)
                                        {
                                            if (item.IIdHopDongId.Equals(SelectedTaiSan.IIdHopDongId))
                                            {
                                                item.STenHopDong = item2.STenHopDong;
                                                item.IIdHopDongId = item2.Id;
                                            }
                                        }
                                        else if (item.Id != null && item.Id != Guid.Empty)
                                        {
                                            if (item.Id.Equals(SelectedTaiSan.Id))
                                            {
                                                item.STenHopDong = item2.STenHopDong;
                                                item.IIdHopDongId = item2.Id;
                                                _nhQtTaiSanService.Update(_mapper.Map<NhQtTaiSan>(item));
                                                OnPropertyChanged(nameof(item));
                                            }
                                        }
                                    }
                                }

                                GenericControlCustomWindow.Close();
                                OnPropertyChanged(nameof(ItemsTaiSan));
                            }
                            catch (Exception ex)
                            {
                                _logger.Error(ex.Message, ex);
                            }
                        };

                        viewModelBase.GenericControlCustomWindow = GenericControlCustomWindow;
                        GenericControlCustomWindow.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                IconKind = PackIconKind.PlaylistPlus;
                Description = "Thêm mới tài sản hình thành theo hợp đồng";
                //ItemsHangMuc = new ObservableCollection<NhQtTaiSanChiTietModel>();
                ItemsTaiSan = new ObservableCollection<NhQtTaiSanModel>();
            }
            else
            {
                Description = "Cập nhật tài sản hình thành theo hợp đồng";
                //LoadDataTaiSanChiTietByTaiSan();
                LoadDataTaiSanByChungTuTaiSan();
            }

            OnPropertyChanged(nameof(Model));
            //OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
        }

        public override void OnSave()
        {
            //if (!Validate()) return;
            if (!ValidateTaiSan()) return;
            if (!ValidateViewModelHelper.Validate(Model)) return;
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                //NhQtTaiSan entity;
                NhQtChungTuTaiSan entity;
                if (Model.Id.IsNullOrEmpty())
                {
                    // Thêm mới
                    //entity = _mapper.Map<NhQtTaiSan>(Model);
                    entity = _mapper.Map<NhQtChungTuTaiSan>(Model);
                    entity.Id = Guid.NewGuid();
                    _service.Add(entity);
                    Model.Id = entity.Id;
                    SaveTaiSan();
                }
                else
                {
                    // Cập nhật
                    entity = _service.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    _service.Update(entity);
                    SaveTaiSan();
                }

                e.Result = entity;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    // Reload data
                    /*Model = _mapper.Map<NhQtQuyetToanNienDoModel>(e.Result);*/

                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);

                    SavedAction?.Invoke(Model);
                    OnPropertyChanged(nameof(Model));
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
                LoadData();
            });
        }

        private bool ValidateTaiSan()
        {
            List<string> messErrors = new List<string>();
            foreach (var item in ItemsTaiSan.Where(n => !n.IsDeleted))
            {
                if (string.IsNullOrEmpty(item.SMaTaiSan))
                    messErrors.Add(string.Format(Resources.MsgErrorRequire, "Mã tài sản"));
                if (string.IsNullOrEmpty(item.STenTaiSan))
                    messErrors.Add(string.Format(Resources.MsgErrorRequire, "Tên tài sản"));
                if (!item.IIdLoaiTaiSan.HasValue)
                    messErrors.Add(string.Format(Resources.MsgErrorRequire, "Loại tài sản"));
                if (!item.ITinhTrangSuDung.HasValue)
                    messErrors.Add(string.Format(Resources.MsgErrorRequire, "Tình trạng sử dụng"));
                if (!item.ITrangThai.HasValue)
                    messErrors.Add(string.Format(Resources.MsgErrorRequire, "Trạng thái sử dụng"));
                if (string.IsNullOrEmpty(item.STenDuAn))
                    messErrors.Add(string.Format(Resources.MsgErrorRequire, "Tên dự án"));
                if (string.IsNullOrEmpty(item.STenHopDong))
                    messErrors.Add(string.Format(Resources.MsgErrorRequire, "Tên hợp đồng"));
            }
            if (messErrors.Count != 0)
            {
                MessageBoxHelper.Error(string.Join("\n", messErrors));
                return false;
            }
            return true;
        }

        //private bool Validate()
        //{
        //    List<string> lstError = new List<string>();
        //    if (Model.SSoChungTu == null)
        //    {
        //        lstError.Add(string.Format("Nhập số chứng từ"));
        //    }
        //    if (Model.STenChungTu == null)
        //    {
        //        lstError.Add(string.Format("Nhập tên chứng từ"));
        //    }
        //    if (lstError.Count != 0)
        //    {
        //        MessageBoxHelper.Warning(string.Join("\n", lstError));
        //        return false;
        //    }
        //    return true;
        //}

        public override void OnClosing()
        {
            // Clear items
            if (!ItemsLoaiTaiSan.IsEmpty()) ItemsLoaiTaiSan = new List<NhDmLoaiTaiSanModel>();
            if (!ItemsTrangThaiSuDung.IsEmpty()) ItemsTrangThaiSuDung.Clear();
        }

        private void SaveTaiSan()
        {
            
            var lstInsert = ItemsTaiSan.Where(x => !x.IsDeleted && x.Id == Guid.Empty).ToList();
            var lstUpdate = ItemsTaiSan.Where(x => !x.IsDeleted && x.Id != Guid.Empty).ToList();
            var lstDelete = ItemsTaiSan.Where(x => x.IsDeleted && x.Id != Guid.Empty).ToList();

            if (lstInsert != null && lstInsert.Count > 0)
            {
                AddTaiSanSave(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count > 0)
            {
                UpdateTaiSanSave(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count > 0)
            {
                DeleteTaiSanSave(lstDelete);
            }
        }

        //private void AddHangMucSave(List<NhQtTaiSanChiTietModel> listAdd)
        //{
        //    foreach (var item in listAdd)
        //    {
        //        item.IIdTaiSanId = Model.Id;
        //    }
        //    var hangMuc = _mapper.Map<List<NhQtTaiSanChiTiet>>(listAdd);
        //    foreach (var item in hangMuc)
        //    {
        //        item.Id = Guid.NewGuid();
        //        _nhQtTaiSanChiTietService.Add(item);
        //    }
        //}
        private void AddTaiSanSave(List<NhQtTaiSanModel> listAdd)
        {
            foreach (var item in listAdd)
            {
                item.Id = Guid.NewGuid();
                item.IIdChungTuTaiSanId = Model.Id;
                //item.IIdMaDonViId = SelectedTaiSan.IIdMaDonViId;
                //item.IIdDuAnId = SelectedTaiSan.IIdDuAnId;
                //item.IIdHopDongId= SelectedTaiSan.IIdHopDongId;
                //item.IIdLoaiTaiSan= SelectedTaiSan.IIdLoaiTaiSan;
                var taiSan = _mapper.Map<NhQtTaiSan>(item);
                _nhQtTaiSanService.Add(taiSan);
                OnPropertyChanged(nameof(SelectedTaiSan));
            }
        }

        private void UpdateTaiSanSave(List<NhQtTaiSanModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                NhQtTaiSan taiSan = _nhQtTaiSanService.FindById(item.Id);
                if (taiSan != null)
                {
                    taiSan.SMaTaiSan = item.SMaTaiSan;
                    taiSan.STenTaiSan = item.STenTaiSan;
                    taiSan.SMoTaTaiSan = item.SMoTaTaiSan;
                    taiSan.ITrangThai = item.ITrangThai;
                    taiSan.DNgayBatDauSuDung = item.DNgayBatDauSuDung;
                    taiSan.IIdChungTuTaiSanId = Model.Id;
                    taiSan.IIdDuAnId = item.IIdDuAnId;
                    taiSan.IIdLoaiTaiSan = item.IIdLoaiTaiSan;
                    taiSan.IIdKhttNhiemVuChiId = item.IIdKhttNhiemVuChiId;
                    taiSan.ILoaiTaiSan = item.ILoaiTaiSan;
                    taiSan.ITinhTrangSuDung = item.ITinhTrangSuDung;
                    taiSan.FSoLuong = item.FSoLuong;
                    taiSan.FNguyenGia = item.FNguyenGia;
                    taiSan.IIdMaDonViId = item.IIdMaDonViId;
                    taiSan.IIdHopDongId = item.IIdHopDongId;
                    taiSan.SDonViTinh = item.SDonViTinh;

                    //taiSan.IIdMaDonViId = SelectedDonVi.Id;
                    //taiSan.IIdDuAnId = SelectedDuAn.Id;
                    //taiSan.IIdHopDongId = SelectedHopDong.Id;
                    //taiSan.IIdLoaiTaiSan = SelectedLoaiTaiSan.Id;
                    //taiSan.IIdChungTuTaiSanId = Model.Id;
                    _nhQtTaiSanService.Update(taiSan);
                }
            }
        }

        private void DeleteTaiSanSave(List<NhQtTaiSanModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                _nhQtTaiSanService.Delete(item.Id);
            }
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }
    }
}
