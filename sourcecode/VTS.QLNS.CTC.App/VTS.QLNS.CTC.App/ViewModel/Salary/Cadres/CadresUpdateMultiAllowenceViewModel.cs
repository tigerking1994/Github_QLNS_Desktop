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
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Cadres
{
    public class CadresUpdateMultiAllowenceViewModel : GridViewModelBase<AllowenceModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private ICollectionView _dtCadresView;
        private ICollectionView _phuCapView;
        private ICollectionView _donViView;
        private ICollectionView _capBacView;
        private readonly ITlDmPhuCapService _phuCapService;
        private readonly ITlDmCanBoService _cadresService;
        private readonly ITlDmCapBacService _tlDmCapBacService;
        private readonly ITlDmChucVuService _tlDmChucVuService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly INsQsMucLucService _qsMucLucService;
        private readonly ITlDmCachTinhLuongChuanService _tlDmCachTinhLuongChuanService;

        public override string FuncCode => NSFunctionCode.SALARY_CADRES_UPDATE_MULTI;

        public override Type ContentType => typeof(View.Salary.Cadres.CadresUpdateMultiAllowence);
        public override PackIconKind IconKind => PackIconKind.AccountDetails;
        public override string Title => "CẬP NHẬT ĐỒNG THỜI CÁC YẾU TỐ LƯƠNG, PHỤ CẤP";
        public override string Description => "Cập nhật đồng thời các yếu tố lương, phụ cấp cho nhiều đối tượng";

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;

        private AllowenceModel _model;
        public AllowenceModel Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set => SetProperty(ref _monthSelected, value);
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _yearSelected;
        public ComboboxItem YearSelected
        {
            get => _yearSelected;
            set => SetProperty(ref _yearSelected, value);
        }


        private ObservableCollection<TlDmDonViModel> _donviData;
        public ObservableCollection<TlDmDonViModel> DonViItems
        {
            get => _donviData;
            set => SetProperty(ref _donviData, value);
        }

        private TlDmDonViModel _selectedDonVi;
        public TlDmDonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private TlDmDonViModel _selectedDonViDieuChinh;
        public TlDmDonViModel SelectedDonViDieuChinh
        {
            get => _selectedDonViDieuChinh;
            set => SetProperty(ref _selectedDonViDieuChinh, value);
        }

        private ObservableCollection<AllowenceModel> _itemsAllowence;
        public ObservableCollection<AllowenceModel> ItemsAllowence
        {
            get => _itemsAllowence;
            set => SetProperty(ref _itemsAllowence, value);
        }

        private AllowenceModel _selectedAllowence;
        public AllowenceModel SelectedAllowence
        {
            get => _selectedAllowence;
            set => SetProperty(ref _selectedAllowence, value);
        }

        private ObservableCollection<TlDmCapBacModel> _itemsCapBac;
        public ObservableCollection<TlDmCapBacModel> ItemsCapBac
        {
            get => _itemsCapBac;
            set => SetProperty(ref _itemsCapBac, value);
        }

        private TlDmCapBacModel _selectedCapBac;
        public TlDmCapBacModel SelectedCapBac
        {
            get => _selectedCapBac;
            set => SetProperty(ref _selectedCapBac, value);
        }

        private TlDmCapBacModel _selectedCapBacDieuChinh;
        public TlDmCapBacModel SelectedCapBacDieuChinh
        {
            get => _selectedCapBacDieuChinh;
            set
            {
                SetProperty(ref _selectedCapBacDieuChinh, value);
                if (ItemsAllowence != null)
                {
                    var phucap = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.LHT_HS);
                    if (_selectedCapBacDieuChinh != null && phucap != null)
                    {
                        phucap.GiaTri = _selectedCapBacDieuChinh.LhtHs;
                        phucap.IsModified = true;
                    }
                    var phucapTiLeHuong = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.TILE_HUONG);
                    if (_selectedCapBacDieuChinh != null && phucapTiLeHuong != null)
                    {
                        phucapTiLeHuong.GiaTri = _selectedCapBacDieuChinh.TiLeHuong;
                        phucapTiLeHuong.IsModified = true;
                    }
                    OnPropertyChanged(nameof(ItemsAllowence));
                }
            }
        }

        private ObservableCollection<TlDmChucVuModel> _itemsChucVu;
        public ObservableCollection<TlDmChucVuModel> ItemsChucVu
        {
            get => _itemsChucVu;
            set => SetProperty(ref _itemsChucVu, value);
        }

        private TlDmChucVuModel _selectedChucVu;
        public TlDmChucVuModel SelectedChucVu
        {
            get => _selectedChucVu;
            set => SetProperty(ref _selectedChucVu, value);
        }

        private TlDmChucVuModel _selectedChucVuDieuChinh;
        public TlDmChucVuModel SelectedChucVuDieuChinh
        {
            get => _selectedChucVuDieuChinh;
            set
            {
                SetProperty(ref _selectedChucVuDieuChinh, value);
                if (ItemsAllowence != null)
                {
                    var phucap = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCCV_HS);
                    if (_selectedChucVuDieuChinh != null && phucap != null)
                    {
                        phucap.GiaTri = _selectedChucVuDieuChinh.HeSoCv;
                        phucap.IsModified = true;
                    }
                    OnPropertyChanged(nameof(ItemsAllowence));
                }
            }
        }

        private ObservableCollection<CadresModel> _lstCanBo;
        public ObservableCollection<CadresModel> ItemsCanBo
        {
            get => _lstCanBo;
            set => SetProperty(ref _lstCanBo, value);
        }

        private CadresModel _selectedCanBo;
        public CadresModel SelectedCanBo
        {
            get => _selectedCanBo;
            set => SetProperty(ref _selectedCanBo, value);
        }

        private string _searchMaPhuCap;
        public string SearchMaPhuCap
        {
            get => _searchMaPhuCap;
            set => SetProperty(ref _searchMaPhuCap, value);
        }

        private string _searchTenPhuCap;
        public string SearchTenPhuCap
        {
            get => _searchTenPhuCap;
            set => SetProperty(ref _searchTenPhuCap, value);
        }

        private DateTime? _fromDate;
        public DateTime? FromDate
        {
            get => _fromDate;
            set => SetProperty(ref _fromDate, value);
        }

        private DateTime? _toDate;
        public DateTime? ToDate
        {
            get => _toDate;
            set => SetProperty(ref _toDate, value);
        }

        private string _fromNamThamNien;
        public string FromNamThamNien
        {
            get => _fromNamThamNien;
            set => SetProperty(ref _fromNamThamNien, value);
        }

        private string _toNamThamNien;
        public string ToNamThamNien
        {
            get => _toNamThamNien;
            set => SetProperty(ref _toNamThamNien, value);
        }

        public bool? IsAllItemSelected
        {
            get
            {
                if (Items != null && ItemsCanBo != null)
                {
                    var selected = ItemsCanBo.Select(x => x.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, ItemsCanBo);
                    OnPropertyChanged();
                }
            }
        }

        private decimal? _heSoKhuVuc;
        public decimal? HeSoKhuVuc
        {
            get => _heSoKhuVuc;
            set => SetProperty(ref _heSoKhuVuc, value);
        }

        private decimal? _heSoKhuVucMoi;
        public decimal? HeSoKhuVucMoi
        {
            get => _heSoKhuVucMoi;
            set
            {
                SetProperty(ref _heSoKhuVucMoi, value);
                if (ItemsAllowence != null && ItemsAllowence.Count > 0)
                {
                    var pc = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.PCKV_HS));
                    if (pc != null && _heSoKhuVucMoi != null)
                    {
                        pc.GiaTri = _heSoKhuVucMoi;
                        pc.IsModified = true;
                        OnPropertyChanged(nameof(ItemsAllowence));
                    }
                }
            }
        }

        private decimal? _tienAn;
        public decimal? TienAn
        {
            get => _tienAn;
            set => SetProperty(ref _tienAn, value);
        }

        private decimal? _tienAnMoi;
        public decimal? TienAnMoi
        {
            get => _tienAnMoi;
            set => SetProperty(ref _tienAnMoi, value);
        }

        private decimal? _trichLuong;
        public decimal? TrichLuong
        {
            get => _trichLuong;
            set => SetProperty(ref _trichLuong, value);
        }

        private ObservableCollection<QsMucLucModel> _tangGiamItems;
        public ObservableCollection<QsMucLucModel> TangGiamItems
        {
            get => _tangGiamItems;
            set => SetProperty(ref _tangGiamItems, value);
        }

        private List<ComboboxItem> _gender;
        public List<ComboboxItem> DataGenders
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        private ComboboxItem _selectedGender;
        public ComboboxItem SelectedGender
        {
            get => _selectedGender;
            set => SetProperty(ref _selectedGender, value);
        }

        private QsMucLucModel _selectedTangGiamCu;
        public QsMucLucModel SelectedTangGiamCu
        {
            get => _selectedTangGiamCu;
            set => SetProperty(ref _selectedTangGiamCu, value);
        }

        private QsMucLucModel _selectedTangGiamDieuChinh;
        public QsMucLucModel SelectedTangGiamDieuChinh
        {
            get => _selectedTangGiamDieuChinh;
            set => SetProperty(ref _selectedTangGiamDieuChinh, value);
        }

        private decimal? _tienTrichLuong;
        public decimal? TienTrichLuong
        {
            get => _tienTrichLuong;
            set => SetProperty(ref _tienTrichLuong, value);
        }

        private DateTime? _ngayNhapNgu;
        public DateTime? NgayNhapNgu
        {
            get => _ngayNhapNgu;
            set => SetProperty(ref _ngayNhapNgu, value);
        }

        private DateTime? _ngayXuatNguMoi;
        public DateTime? NgayXuatNguMoi
        {
            get => _ngayXuatNguMoi;
            set => SetProperty(ref _ngayXuatNguMoi, value);
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
            }
        }

        private bool _selectedAllDonVi;
        public bool SelectedAllDonVi
        {
            get => DonViItems.All(x => x.IsSelected);
            set
            {
                SetProperty(ref _selectedAllDonVi, value);
                foreach (var item in DonViItems) item.IsSelected = _selectedAllDonVi;
            }
        }

        public string LabelSelectedDonVi
        {
            get
            {
                var totalCount = DonViItems.Count();
                var totalSelectedCount = DonViItems.Count(x => x.IsSelected);
                return $"Đơn vị ({totalSelectedCount} / {totalCount})";
            }
        }

        private bool _selectedAllCapBac;
        public bool SelectedAllCapBac
        {
            get => ItemsCapBac.All(x => x.IsSelected);
            set
            {
                SetProperty(ref _selectedAllCapBac, value);
                foreach (var item in ItemsCapBac) item.IsSelected = _selectedAllCapBac;
            }
        }

        public string LabelSelectedCapBac
        {
            get
            {
                var totalCount = ItemsCapBac.Count();
                var totalSelectedCount = ItemsCapBac.Count(x => x.IsSelected);
                return $"Cấp bậc ({totalSelectedCount} / {totalCount})";
            }
        }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value) && _donViView != null)
                {
                    _donViView.Refresh();
                }
            }
        }

        private string _searchCapBac;
        public string SearchCapBac
        {
            get => _searchCapBac;
            set
            {
                if (SetProperty(ref _searchCapBac, value) && _capBacView != null)
                {
                    _capBacView.Refresh();
                }
            }
        }

        private bool _isHsq;
        public bool IsHsq
        {
            get => _isHsq;
            set => SetProperty(ref _isHsq, value);
        }

        public string MenuType { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand SaveDataCommand { get; }
        public RelayCommand SearchCanBoCommand { get; }
        public RelayCommand SaveAndCloseCommand { get; }

        public bool IsFirst { get; set; }

        public CadresUpdateMultiAllowenceViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmPhuCapService phuCapService,
            ITlDmCanBoService cadresService,
            ITlDmCapBacService tlDmCapBacService,
            ITlDmChucVuService tlDmChucVuService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            ITlDmDonViService tlDmDonViService,
            ITlDmCachTinhLuongChuanService tlDmCachTinhLuongChuanService,
            INsQsMucLucService nsQsMucLucService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _cadresService = cadresService;
            _phuCapService = phuCapService;
            _tlDmCapBacService = tlDmCapBacService;
            _tlDmChucVuService = tlDmChucVuService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _tlDmDonViService = tlDmDonViService;
            _qsMucLucService = nsQsMucLucService;
            _tlDmCachTinhLuongChuanService = tlDmCachTinhLuongChuanService;

            SearchCommand = new RelayCommand(o => _phuCapView.Refresh());
            ResetFilterCommand = new RelayCommand(o => OnResetFilter());
            SaveDataCommand = new RelayCommand(o => OnSaveData(o));
            SearchCanBoCommand = new RelayCommand(o => OnSearchCanBo());
            SaveAndCloseCommand = new RelayCommand(o => OnSaveAndClose(o));
        }

        public override void Init()
        {
            base.Init();
            TabIndex = ImportTabIndex.Data;
            IsFirst = true;
            LoadDefault();
            LoadDanhMucCapBac();
            LoadDanhMucChucVu();
            LoadDonVi();
            LoadMonths();
            LoadYear();
            LoadData();
            LoadDanhMucTangGiam();
            LoadCanBo();
            LoadGender();
            IsFirst = false;
        }

        private void LoadDefault()
        {
            FromDate = null;
            ToDate = null;
            FromNamThamNien = null;
            ToNamThamNien = null;
            SelectedDonViDieuChinh = null;
            SelectedCapBac = null;
            SelectedCapBacDieuChinh = null;
            SelectedChucVu = null;
            SelectedChucVuDieuChinh = null;
            SearchCapBac = string.Empty;
            SearchDonVi = string.Empty;
            FromDate = null;
            ToDate = null;
            FromNamThamNien = null;
            ToNamThamNien = null;
            HeSoKhuVuc = null;
            HeSoKhuVucMoi = null;
            TienAn = null;
            TienAnMoi = null;
            TrichLuong = null;
            _dtCadresView = null;
            NgayNhapNgu = null;
            NgayXuatNguMoi = null;
            IsFirst = false;
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            var data = new List<TlDmPhuCap>();
            if (MenuType == UpdateMultiMenuType.THUE)
            {
                var predicate = PredicateBuilder.True<TlDmPhuCap>();
                predicate = predicate.And(x => x.Chon == true);
                predicate = predicate.And(x => x.IsReadonly == false);
                predicate = predicate.And(x => x.MaPhuCap == PhuCap.THUONG_TT || x.MaPhuCap == PhuCap.GIAMTHUE_TT
                                            || x.MaPhuCap == PhuCap.THUNHAPKHAC_TT || x.MaPhuCap == PhuCap.THUEDANOP_TT);
                data = _phuCapService.FindByCondition(predicate).Select(x => { x.GiaTri = null; return x; }).ToList();
            }
            else if (MenuType == UpdateMultiMenuType.CHIKHAC)
            {
                var predicate = PredicateBuilder.True<TlDmPhuCap>();
                predicate = predicate.And(x => x.Chon == true);
                predicate = predicate.And(x => x.IsReadonly == false);
                predicate = predicate.And(x => x.MaPhuCap == PhuCap.GTKHAC_TT || x.MaPhuCap == PhuCap.TIENCTLH_TT
                                            || x.MaPhuCap == PhuCap.TIENANDUONG_TT || x.MaPhuCap == PhuCap.TIENTAUXE_TT);
                data = _phuCapService.FindByCondition(predicate).Select(x => { x.GiaTri = null; return x; }).ToList();
            }
            else
            {
                data = _phuCapService.GetDmPhuCapInDcTapTheCanBo().Select(x => { x.GiaTri = null; return x; }).ToList();
            }
            List<AllowenceModel> listData = _mapper.Map<List<AllowenceModel>>(data);
            List<AllowenceModel> listAllowence = new List<AllowenceModel>(listData);

            foreach (var item in listAllowence)
            {
                var phuCapCha = _phuCapService.FindByMaPhuCap(item.Parent);
                if (phuCapCha != null)
                {
                    item.ParentName = string.Format("{0} - {1}", phuCapCha.MaPhuCap, phuCapCha.TenPhuCap);
                }
            }

            foreach (AllowenceModel model in listAllowence)
            {
                model.PropertyChanged += DetailPhuCap_PropertyChanged;
            }

            ItemsAllowence = new ObservableCollection<AllowenceModel>(listAllowence);
            _phuCapView = CollectionViewSource.GetDefaultView(ItemsAllowence);
            _phuCapView.GroupDescriptions.Add(new PropertyGroupDescription("ParentName"));
            _phuCapView.Filter = ListPhuCapFilter;

            OnPropertyChanged(nameof(ItemsAllowence));
        }

        private void LoadDanhMucTangGiam()
        {
            try
            {
                var data = _qsMucLucService.FindAll().Where(x => x.BHangCha == false && x.SHienThi != "2" && x.INamLamViec == DateTime.Now.Year).ToList();
                _tangGiamItems = new ObservableCollection<QsMucLucModel>();
                _tangGiamItems = _mapper.Map<ObservableCollection<QsMucLucModel>>(data);
                OnPropertyChanged(nameof(TangGiamItems));
                OnPropertyChanged(nameof(SelectedTangGiamDieuChinh));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDanhMucCapBac()
        {
            var data = new List<TlDmCapBac>();
            if (!IsHsq)
            {
                data = _tlDmCapBacService.FindByNote().ToList();
            }
            else
            {
                data = _tlDmCapBacService.FindAll(x => x.Parent == "4").ToList();
            }
            _itemsCapBac = _mapper.Map<ObservableCollection<TlDmCapBacModel>>(data);

            foreach (var item in _itemsCapBac)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(TlDmCapBacModel.IsSelected))
                    {
                        foreach (var donVi in _itemsCapBac)
                        {
                            if (donVi.Parent == item.MaCb)
                            {
                                donVi.IsSelected = item.IsSelected;
                            }
                        }
                    }
                    OnPropertyChanged(nameof(SelectedAllCapBac));
                    OnPropertyChanged(nameof(LabelSelectedCapBac));
                };
            }

            _capBacView = CollectionViewSource.GetDefaultView(_itemsCapBac);
            _capBacView.Filter = CapBacFilter;

            OnPropertyChanged(nameof(SelectedCapBac));
            OnPropertyChanged(nameof(ItemsCapBac));
        }

        private void LoadGender()
        {
            DataGenders = new List<ComboboxItem>();
            _gender.Add(new ComboboxItem(Gender.NAM, Gender.NAM));
            _gender.Add(new ComboboxItem(Gender.NU, Gender.NU));
        }

        private void LoadDanhMucChucVu()
        {
            var data = _tlDmChucVuService.FindAll().OrderBy(x => x.MaCv);
            _itemsChucVu = _mapper.Map<ObservableCollection<TlDmChucVuModel>>(data);
            OnPropertyChanged(nameof(SelectedChucVu));
            OnPropertyChanged(nameof(ItemsChucVu));
        }

        private void LoadDonVi()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            _donviData = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);

            foreach (var item in _donviData)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(TlDmDonViModel.IsSelected))
                    {
                        foreach (var donVi in _donviData)
                        {
                            if (donVi.ParentId == item.MaDonVi)
                            {
                                donVi.IsSelected = item.IsSelected;
                            }
                        }
                    }
                    OnPropertyChanged(nameof(SelectedAllDonVi));
                    OnPropertyChanged(nameof(LabelSelectedDonVi));
                };
            }

            _donViView = CollectionViewSource.GetDefaultView(_donviData);
            _donViView.Filter = DonViFilter;

            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(DonViItems));
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            var thang = _sessionService.Current.Month;
            if (Model != null)
            {
                MonthSelected = _months.FirstOrDefault(x => x.ValueItem.Equals(Model.SelectedMonth.ToString()));
            }
            OnPropertyChanged(nameof(Months));
        }

        private void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            if (Model != null)
            {
                YearSelected = _years.FirstOrDefault(x => x.ValueItem.Equals(Model.SelectedYear.ToString()));
            }
            OnPropertyChanged(nameof(Years));
        }

        private void LoadCanBo()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                var lstDonViSelectedStr = string.Join(",", DonViItems.Where(x => x.IsSelected).Select(x => x.MaDonVi).ToList());
                var lstCapBacSelectedStr = string.Join(",", ItemsCapBac.Where(x => x.IsSelected).Select(x => x.MaCb).ToList());

                e.Result = _cadresService.FindCanBoDieuChinh
                (
                    int.Parse(MonthSelected.ValueItem),
                    int.Parse(YearSelected.ValueItem),
                    string.IsNullOrEmpty(lstDonViSelectedStr) ? null : lstDonViSelectedStr,
                    string.IsNullOrEmpty(lstCapBacSelectedStr) ? null : lstCapBacSelectedStr,
                    HeSoKhuVuc,
                    SelectedTangGiamCu == null ? null : SelectedTangGiamCu.SKyHieu,
                    SelectedChucVu == null ? null : SelectedChucVu.MaCv,
                    TienAn,
                    NgayNhapNgu,
                    IsHsq
                );
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    ItemsCanBo = _mapper.Map<ObservableCollection<CadresModel>>(e.Result);
                    _dtCadresView = CollectionViewSource.GetDefaultView(ItemsCanBo);
                }
                IsLoading = false;
            });
        }

        private void DetailPhuCap_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(AllowenceModel.GiaTri) || args.PropertyName == nameof(AllowenceModel.HuongPCSN))
            {
                AllowenceModel item = (AllowenceModel)sender;
                item.IsModified = true;
                OnPropertyChanged(nameof(ItemsAllowence));
            }
        }

        private bool CapBacFilter(object obj)
        {
            bool result = true;
            var item = (TlDmCapBacModel)obj;
            if (!string.IsNullOrEmpty(SearchCapBac))
            {
                result &= item.MaCb.Contains(SearchCapBac) || item.Note.ToLower().Contains(SearchCapBac.ToLower());
            }
            return result;
        }

        private bool DonViFilter(object obj)
        {
            bool result = true;
            var item = (TlDmDonViModel)obj;
            if (!string.IsNullOrEmpty(SearchDonVi))
            {
                result &= item.MaDonVi.Contains(SearchDonVi) || item.TenDonVi.ToLower().Contains(SearchDonVi.ToLower());
            }
            return result;
        }

        private bool ListPhuCapFilter(object obj)
        {
            bool result = true;

            var item = (AllowenceModel)obj;

            if (!string.IsNullOrEmpty(SearchMaPhuCap))
            {
                result &= !string.IsNullOrEmpty(item.MaPhuCap) && item.MaPhuCap.ToLower().Contains(SearchMaPhuCap.ToLower());
            }
            if (!string.IsNullOrEmpty(SearchTenPhuCap))
            {
                result &= !string.IsNullOrEmpty(item.TenPhuCap) && item.TenPhuCap.ToLower().Contains(SearchTenPhuCap.ToLower());
            }

            return result;
        }

        private void OnSearchCanBo()
        {
            LoadCanBo();
        }

        private void OnResetFilter()
        {
            SearchMaPhuCap = string.Empty;
            SearchTenPhuCap = string.Empty;

            _phuCapView.Refresh();
        }

        private void OnSaveData(object o)
        {
            string messages = GetMessageValidate();
            if (!string.IsNullOrEmpty(messages))
            {
                MessageBoxHelper.Warning(messages);
                return;
            }

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                var lstUpdate = ItemsCanBo.Where(x => x.Selected);
                var lstPhuCapUpdate = ItemsAllowence.Where(x => x.IsModified).ToList();
                var lstPhuCapThanhTien = new List<AllowenceModel>();
                foreach (var item in lstPhuCapUpdate)
                {
                    var cachTinh = _tlDmCachTinhLuongChuanService.FindAll().Where(n =>
                        n.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).Contains(item.MaPhuCap));
                    cachTinh.ForAll(item =>
                    {
                        var phuCapThanhTien = _phuCapService.FindByMaPhuCap(item.MaCot);
                        if (phuCapThanhTien != null)
                            lstPhuCapThanhTien.Add(_mapper.Map<AllowenceModel>(phuCapThanhTien));
                    });
                }
                lstPhuCapUpdate.AddRange(lstPhuCapThanhTien);

                var pcLht = lstPhuCapUpdate.FirstOrDefault(x => PhuCap.LHT_HS.Equals(x.MaPhuCap));
                var lstUpdatePhuCap = new List<TlCanBoPhuCapModel>();
                var lstAddPhuCap = new List<TlCanBoPhuCapModel>();


                foreach (var item in lstUpdate)
                {
                    var phuCapQuery = _tlCanBoPhuCapService.FindCanBoPhuCapByMaCanBo(item.MaCanBo);
                    var phuCaps = _mapper.Map<List<TlCanBoPhuCapModel>>(phuCapQuery);
                    //var phuCaps = _mapper.Map<List<TlCanBoPhuCapModel>>(_tlCanBoPhuCapService.FindByMaCanBo(item.MaCanBo));
                    phuCaps.Select(x => x.BCapNhat = true).ToList();

                    foreach (var element in lstPhuCapUpdate)
                    {
                        var pcCbUpdate = phuCaps.FirstOrDefault(x => x.MaPhuCap == element.MaPhuCap);
                        if (pcCbUpdate != null && pcCbUpdate.Id != Guid.Empty)
                        {
                            if (element.IsModified)
                            {
                                pcCbUpdate.GiaTri = element.GiaTri;
                                pcCbUpdate.HuongPcSn = element.HuongPCSN;
                                lstUpdatePhuCap.Add(pcCbUpdate);
                            }
                        }
                        else
                        {
                            var canbophucapadd = new TlCanBoPhuCapModel();
                            canbophucapadd.MaCbo = item.MaCanBo;
                            canbophucapadd.MaPhuCap = element.MaPhuCap;
                            canbophucapadd.GiaTri = element.GiaTri;
                            canbophucapadd.HuongPcSn = element.HuongPCSN;
                            canbophucapadd.Flag = false;
                            canbophucapadd.BSaoChep = true;
                            lstAddPhuCap.Add(canbophucapadd);
                        }
                    }

                    if (pcLht != null && pcLht.GiaTri != null && pcLht.GiaTri != 0)
                    {
                        item.HeSoLuong = pcLht.GiaTri;
                    }
                    //lstUpdatePhuCap.AddRange(phuCaps);

                    if (SelectedDonViDieuChinh != null)
                    {
                        item.Parent = SelectedDonViDieuChinh.MaDonVi;
                        item.TenDonVi = SelectedDonViDieuChinh.TenDonVi;
                    }
                    if (SelectedCapBacDieuChinh != null)
                    {
                        item.MaCb = SelectedCapBacDieuChinh.MaCb;
                    }
                    if (SelectedChucVuDieuChinh != null)
                    {
                        item.MaCv = SelectedChucVuDieuChinh.MaCv;
                    }
                    if (SelectedGender != null)
                    {
                        if (SelectedGender.ValueItem == Gender.NAM)
                        {
                            item.IsNam = true;
                        }
                        else
                        {
                            item.IsNam = false;
                        }
                    }
                    if (TienAnMoi != null && !string.IsNullOrEmpty(TienAnMoi.ToString()))
                    {
                        var phuCap = item.TlCanBoPhuCaps.Where(x => x.MaPhuCap.StartsWith("TA") && x.GiaTri == TienAn);
                        if (phuCap != null && phuCap.Count() > 0)
                        {
                            foreach (var item1 in phuCap)
                            {
                                item1.GiaTri = TienAnMoi;
                                lstUpdatePhuCap.Add(item1);
                            }
                        }
                    }
                    if (TrichLuong != null && !string.IsNullOrEmpty(TrichLuong.ToString()))
                    {
                        var phuCap = item.TlCanBoPhuCaps.FirstOrDefault(x => PhuCap.TRICHLUONG_SN.Equals(x.MaPhuCap));
                        if (phuCap != null)
                        {
                            phuCap.GiaTri = TrichLuong;
                            lstUpdatePhuCap.Add(phuCap);
                        }
                    }
                    if (TienTrichLuong != null && !string.IsNullOrEmpty(TienTrichLuong.ToString()))
                    {
                        var phuCap = item.TlCanBoPhuCaps.FirstOrDefault(x => PhuCap.TRICHLUONG_TIEN.Equals(x.MaPhuCap));
                        if (phuCap != null)
                        {
                            phuCap.GiaTri = TienTrichLuong;
                            lstUpdatePhuCap.Add(phuCap);
                        }
                    }
                    if (NgayXuatNguMoi != null)
                    {
                        item.NgayXn = NgayXuatNguMoi;
                        if (item.MaCb.StartsWith("0"))
                        {
                            var pcThangTcxn = _tlCanBoPhuCapService.FindByMaCanBoAndMaPhuCap(item.MaCanBo, PhuCap.THANG_TCXN);
                            pcThangTcxn.GiaTri = TinhThangHuongTcxn(item.NgayNn, item.NgayXn);
                            lstUpdatePhuCap.Add(_mapper.Map<TlCanBoPhuCapModel>(pcThangTcxn));
                        }
                        var pcThangTcViecLam = _tlCanBoPhuCapService.FindByMaCanBoAndMaPhuCap(item.MaCanBo, PhuCap.THANG_TCVIECLAM);
                        if (pcThangTcViecLam != null)
                        {
                            var phuCapThangViecLam = _phuCapService.FindByMaPhuCap(PhuCap.THANG_TCVIECLAM);
                            pcThangTcViecLam.GiaTri = phuCapThangViecLam.GiaTri;
                            lstUpdatePhuCap.Add(_mapper.Map<TlCanBoPhuCapModel>(pcThangTcViecLam));
                        }
                    }
                }
                //else
                //{
                //    var pcThangTcViecLam = _tlCanBoPhuCapService.FindByMaCanBoAndMaPhuCap(item.MaCanBo, PhuCap.THANG_TCVIECLAM);
                //    if (pcThangTcViecLam != null)
                //    {
                //        pcThangTcViecLam.GiaTri = 0;
                //        lstUpdatePhuCap.Add(_mapper.Map<TlCanBoPhuCapModel>(pcThangTcViecLam));
                //    }
                //}

                var lstEntityUpdate = _mapper.Map<List<TlCanBoPhuCap>>(lstUpdatePhuCap);
                var lstEntityAdd = _mapper.Map<List<TlCanBoPhuCap>>(lstAddPhuCap);

                var lstCanbo = _mapper.Map<ObservableCollection<TlDmCanBo>>(lstUpdate).ToList();
                _cadresService.UpdateRange(lstCanbo);
                _tlCanBoPhuCapService.UpdateRange(lstEntityUpdate);
                //_cadresService.UpdateMulti(lstCanbo, lstEntityUpdate);
                _tlCanBoPhuCapService.BulkInsert(lstEntityAdd);


                if (SelectedTangGiamDieuChinh != null)
                {
                    lstCanbo.ForAll(x =>
                    {
                        x.MaTangGiam = SelectedTangGiamDieuChinh.SKyHieu;
                        if (x.MaTangGiam.StartsWith("3"))
                        {
                            x.IsDelete = false;
                            x.ITrangThai = 3;
                        }
                        _cadresService.Update(x);
                    });
                }

            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    MessageBoxHelper.Info("Cập nhật thành công");
                    SavedAction?.Invoke(null);
                }
                else
                {
                    _logger.Error(e.Error.Message, e.Error);
                }
                IsLoading = false;
                Init();
            });
        }

        private int TinhThangHuongTcxn(DateTime? ngayNn, DateTime? ngayXn)
        {
            var ngayNhapNgu = (DateTime)ngayNn;
            var ngayXuatNgu = (DateTime)ngayXn;
            var monthDiff = (ngayXuatNgu.Year - ngayNhapNgu.Year) * 12 + ngayXuatNgu.Month - ngayNhapNgu.Month + 1;
            var phanNguyen = monthDiff / 12;
            var phanDu = monthDiff % 12;

            int thangDu = 0;
            if (1 <= phanDu && phanDu <= 6)
            {
                thangDu = 1;
            }
            else if (7 <= phanDu && phanDu <= 12)
            {
                thangDu = 2;
            }

            return (phanNguyen * 2 + thangDu);
        }

        private void OnSaveAndClose(object o)
        {
            OnSaveData(o);
            ((Window)o).Close();
            ClosePopup?.Invoke(null, new EventArgs());
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            ClosePopup?.Invoke(ItemsAllowence, new EventArgs());
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            Init();
        }

        private static void SelectAll(bool select, IEnumerable<CadresModel> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();
            var lstUpadte = ItemsCanBo.Where(x => x.Selected);
            if (lstUpadte == null && lstUpadte.Count() > 0)
            {
                messages.Add("Không có đối tượng nào được chọn.");
            }
            var lstPhuCapUpdate = ItemsAllowence.Where(x => x.IsModified);
            if (lstPhuCapUpdate == null && lstPhuCapUpdate.Count() > 0)
            {
                messages.Add("Không có thay đổi.");
            }
            if (SelectedDonViDieuChinh != null && Guid.Empty.Equals(SelectedDonViDieuChinh.Id))
            {
                messages.Add("Đơn vị điều chỉnh không được là tất cả.");
            }

            return string.Join(Environment.NewLine, messages);
        }
    }
}
