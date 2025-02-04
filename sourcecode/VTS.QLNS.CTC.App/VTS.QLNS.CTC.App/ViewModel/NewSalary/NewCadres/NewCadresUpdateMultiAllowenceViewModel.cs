using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Interop;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewCadres
{
    public class NewCadresUpdateMultiAllowenceViewModel : GridViewModelBase<AllowenceNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private ICollectionView _dtCadresView;
        private ICollectionView _phuCapView;
        private ICollectionView _donViView;
        private ICollectionView _capBacView;
        private ICollectionView _chucVuView;
        private readonly ITlDmPhuCapNq104Service _phuCapService;
        private readonly ITlDmCanBoNq104Service _cadresService;
        private readonly ITlDmCapBacNq104Service _tlDmCapBacService;
        private readonly ITlDmChucVuNq104Service _tlDmChucVuService;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly INsQsMucLucService _qsMucLucService;
        private readonly ITlDmCachTinhLuongChuanNq104Service _tlDmCachTinhLuongChuanService;
        private readonly ITlDmCapBacLuongNq104Service _tlDmCapBacLuongService;
        private readonly ITlCanBoPhuCapBridgeNq104Service _tlCanBoPhuCapBridgeNq104Service;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_CADRES_UPDATE_MULTI;

        public override Type ContentType => typeof(View.NewSalary.NewCadres.NewCadresUpdateMultiAllowence);
        public override PackIconKind IconKind => PackIconKind.AccountDetails;
        public override string Title => "CẬP NHẬT ĐỒNG THỜI CÁC YẾU TỐ LƯƠNG, PHỤ CẤP";
        public override string Description => "Cập nhật đồng thời các yếu tố lương, phụ cấp cho nhiều đối tượng";

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;

        private AllowenceNq104Model _model;
        public AllowenceNq104Model Model
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


        private ObservableCollection<TlDmDonViNq104Model> _donviData;
        public ObservableCollection<TlDmDonViNq104Model> DonViItems
        {
            get => _donviData;
            set => SetProperty(ref _donviData, value);
        }

        private TlDmDonViNq104Model _selectedDonVi;
        public TlDmDonViNq104Model SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private TlDmDonViNq104Model _selectedDonViDieuChinh;
        public TlDmDonViNq104Model SelectedDonViDieuChinh
        {
            get => _selectedDonViDieuChinh;
            set => SetProperty(ref _selectedDonViDieuChinh, value);
        }

        private ObservableCollection<TlDmCapBacLuongNq104Model> _itemsLoaiNhom;
        public ObservableCollection<TlDmCapBacLuongNq104Model> ItemsLoaiNhom
        {
            get => _itemsLoaiNhom;
            set => SetProperty(ref _itemsLoaiNhom, value);
        }

        private TlDmCapBacLuongNq104Model _selectedLoaiNhom;
        public TlDmCapBacLuongNq104Model SelectedLoaiNhom
        {
            get => _selectedLoaiNhom;
            set
            {
                SetProperty(ref _selectedLoaiNhom, value);
                if (!IsFirst)
                    LoadDanhMucBacLuong();
            }
        }

        private ObservableCollection<TlDmCapBacLuongNq104Model> _itemsBacTienLuong;
        public ObservableCollection<TlDmCapBacLuongNq104Model> ItemsBacTienLuong
        {
            get => _itemsBacTienLuong;
            set => SetProperty(ref _itemsBacTienLuong, value);
        }

        private TlDmCapBacLuongNq104Model _selectedBacTienLuong;
        public TlDmCapBacLuongNq104Model SelectedBacTienLuong
        {
            get => _selectedBacTienLuong;
            set
            {
                if (SetProperty(ref _selectedBacTienLuong, value))
                {
                    if (!IsFirst)
                    {
                        TienNangLuongCb = value?.TienNangLuong;
                    }
                    TienLuongCb = value?.TienLuong;
                    var temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LCB_TT"));

                    if (temp != null && !IsFirst && _selectedBacTienLuong != null)
                    {
                        temp.GiaTri = _selectedBacTienLuong?.TienLuong;
                    }
                }
            }
        }

        private decimal? _tienNangLuongCb;
        public decimal? TienNangLuongCb
        {
            get => _tienNangLuongCb;
            set
            {
                SetProperty(ref _tienNangLuongCb, value);
                DataModelChange(TypeChangeCustom.TienNangLuongCb, value ?? 0);
            }
        }

        private decimal? _tienLuongCb;
        public decimal? TienLuongCb
        {
            get => _tienLuongCb;
            set
            {
                SetProperty(ref _tienLuongCb, value);
                DataModelChange(TypeChangeCustom.TienLuongCb, value);

            }
        }

        private ObservableCollection<AllowenceNq104Model> _itemsAllowence;
        public ObservableCollection<AllowenceNq104Model> ItemsAllowence
        {
            get => _itemsAllowence;
            set => SetProperty(ref _itemsAllowence, value);
        }

        private AllowenceNq104Model _selectedAllowence;
        public AllowenceNq104Model SelectedAllowence
        {
            get => _selectedAllowence;
            set => SetProperty(ref _selectedAllowence, value);
        }

        private ObservableCollection<TlDmCapBacNq104Model> _itemsCapBac;
        public ObservableCollection<TlDmCapBacNq104Model> ItemsCapBac
        {
            get => _itemsCapBac;
            set => SetProperty(ref _itemsCapBac, value);
        }

        private TlDmCapBacNq104Model _selectedCapBac;
        public TlDmCapBacNq104Model SelectedCapBac
        {
            get => _selectedCapBac;
            set
            {
                SetProperty(ref _selectedCapBac, value);
                if (value != null)
                {
                    LoadBacLuongChange();
                    LoadDanhMucLoaiNhom();
                }
            }
        }

        private ObservableCollection<TlDmChucVuNq104Model> _itemsChucVu;
        public ObservableCollection<TlDmChucVuNq104Model> ItemsChucVu
        {
            get => _itemsChucVu;
            set => SetProperty(ref _itemsChucVu, value);
        }

        private TlDmChucVuNq104Model _selectedChucVu;
        public TlDmChucVuNq104Model SelectedChucVu
        {
            get => _selectedChucVu;
            set => SetProperty(ref _selectedChucVu, value);
        }

        private decimal? _tienLuongCvd;
        public decimal? TienLuongCvd
        {
            get => _tienLuongCvd;
            set
            {
                SetProperty(ref _tienLuongCvd, value);

            }
        }

        private decimal? _tienNangLuongCvd;
        public decimal? TienNangLuongCvd
        {
            get => _tienNangLuongCvd;
            set
            {
                SetProperty(ref _tienNangLuongCvd, value);
                DataModelChange(TypeChangeCustom.TienNangLuongCvd, value);
            }
        }

        private TlDmChucVuNq104Model _selectedChucVuDieuChinh;
        public TlDmChucVuNq104Model SelectedChucVuDieuChinh
        {
            get => _selectedChucVuDieuChinh;
            set
            {
                SetProperty(ref _selectedChucVuDieuChinh, value);
                TienLuongCvd = value?.TienLuong;

                if (!IsFirst)
                {
                    TienNangLuongCvd = value?.TienNangLuong;
                }

                LoadHeSoChucVu();
            }
        }

        private ObservableCollection<CadresNq104Model> _lstCanBo;
        public ObservableCollection<CadresNq104Model> ItemsCanBo
        {
            get => _lstCanBo;
            set => SetProperty(ref _lstCanBo, value);
        }

        private CadresNq104Model _selectedCanBo;
        public CadresNq104Model SelectedCanBo
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

        public NewCadresUpdateMultiAllowenceViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmPhuCapNq104Service phuCapService,
            ITlDmCanBoNq104Service cadresService,
            ITlDmCapBacNq104Service tlDmCapBacService,
            ITlDmChucVuNq104Service tlDmChucVuService,
            ITlCanBoPhuCapNq104Service tlCanBoPhuCapService,
            ITlDmDonViNq104Service tlDmDonViService,
            ITlDmCachTinhLuongChuanNq104Service tlDmCachTinhLuongChuanService,
            INsQsMucLucService nsQsMucLucService,
            ITlDmCapBacLuongNq104Service tlDmCapBacLuongService,
            ITlCanBoPhuCapBridgeNq104Service tlCanBoPhuCapBridgeNq104Service)
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
            _tlDmCapBacLuongService = tlDmCapBacLuongService;
            _tlCanBoPhuCapBridgeNq104Service = tlCanBoPhuCapBridgeNq104Service;

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
            _selectedCapBac = null;
            SelectedChucVu = null;
            SelectedChucVuDieuChinh = null;
            SelectedBacTienLuong = null;
            SelectedLoaiNhom = null;
            SearchCapBac = string.Empty;
            SearchDonVi = string.Empty;
            TienNangLuongCvd = null;
            TienNangLuongCb = null;
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
            var data = new List<TlDmPhuCapNq104>();
            if (MenuType == UpdateMultiMenuType.THUE)
            {
                var predicate = PredicateBuilder.True<TlDmPhuCapNq104>();
                predicate = predicate.And(x => x.Chon == true);
                predicate = predicate.And(x => x.IsReadonly == false);
                predicate = predicate.And(x => x.MaPhuCap == PhuCap.THUONG_TT || x.MaPhuCap == PhuCap.GIAMTHUE_TT
                                            || x.MaPhuCap == PhuCap.THUNHAPKHAC_TT || x.MaPhuCap == PhuCap.THUEDANOP_TT);
                data = _phuCapService.FindByCondition(predicate).Select(x => { x.GiaTri = null; return x; }).ToList();
            }
            else if (MenuType == UpdateMultiMenuType.CHIKHAC)
            {
                var predicate = PredicateBuilder.True<TlDmPhuCapNq104>();
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
            List<AllowenceNq104Model> listData = _mapper.Map<List<AllowenceNq104Model>>(data);
            List<AllowenceNq104Model> listAllowence = new List<AllowenceNq104Model>(listData);

            foreach (var item in listAllowence)
            {
                var phuCapCha = _phuCapService.FindByMaPhuCap(item.Parent);
                if (phuCapCha != null)
                {
                    item.ParentName = string.Format("{0} - {1}", phuCapCha.MaPhuCap, phuCapCha.TenPhuCap);
                }
            }

            foreach (AllowenceNq104Model model in listAllowence)
            {
                model.PropertyChanged += DetailPhuCap_PropertyChanged;
            }

            ItemsAllowence = new ObservableCollection<AllowenceNq104Model>(listAllowence);
            _phuCapView = CollectionViewSource.GetDefaultView(ItemsAllowence);
            _phuCapView.GroupDescriptions.Add(new PropertyGroupDescription("ParentName"));
            _phuCapView.Filter = ListPhuCapFilter;

            OnPropertyChanged(nameof(ItemsAllowence));
        }

        private void LoadDanhMucTangGiam()
        {
            try
            {
                var data = _qsMucLucService.FindAll().Where(x => x.BHangCha == false && x.SHienThi != "2" && (x.SKyHieu.StartsWith("2") || x.SKyHieu.StartsWith("3") || x.SKyHieu.StartsWith("0")) && x.INamLamViec == _sessionService.Current.YearOfWork && x.ITrangThai == ItrangThaiStatus.ON).ToList();
                _tangGiamItems = new ObservableCollection<QsMucLucModel>();
                data = data.OrderBy(x => x.SKyHieu).ToList();
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
            var data = new List<TlDmCapBacNq104>();
            if (!IsHsq)
            {
                data = _tlDmCapBacService.FindByNote().ToList();
            }
            else
            {
                data = _tlDmCapBacService.FindAll(x => x.Parent == "4").ToList();
            }
            var listCapBac = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(data);

            _itemsCapBac = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(listCapBac.Select(x =>
            {
                var dict = data.Select(x => x.Parent).ToHashSet();
                x.IsHangCha = dict.Contains(x.MaCb);
                x.TenCha = data.FirstOrDefault(y => y.MaCb == x.Parent)?.TenCb;
                return x;
            }).OrderBy(x => x.XauNoiMa));

            foreach (var item in _itemsCapBac)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(TlDmCapBacNq104Model.IsSelected))
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
            //_capBacView.GroupDescriptions.Add(new PropertyGroupDescription("TenCha"));
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
            var data = _tlDmChucVuService.FindAll().OrderBy(x => x.XauNoiMa).ThenBy(x => x.MaSo);
            var chucVus = _mapper.Map<ObservableCollection<TlDmChucVuNq104Model>>(data);

            _itemsChucVu = _mapper.Map<ObservableCollection<TlDmChucVuNq104Model>>(chucVus.Select(x =>
            {
                var dict = data.Select(x => x.MaCha).ToHashSet();
                x.IsHangCha = dict.Contains(x.Ma);
                x.TenCha = data.FirstOrDefault(y => y.Ma == x.MaCha)?.Ten;
                return x;
            }).Where(x => !x.IsHangCha));

            _chucVuView = CollectionViewSource.GetDefaultView(ItemsChucVu);
            _chucVuView.GroupDescriptions.Add(new PropertyGroupDescription("LoaiTen"));
            _chucVuView.GroupDescriptions.Add(new PropertyGroupDescription("TenCha"));
            OnPropertyChanged(nameof(SelectedChucVu));
            OnPropertyChanged(nameof(ItemsChucVu));
        }

        private void LoadDonVi()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            _donviData = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);

            foreach (var item in _donviData)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(TlDmDonViNq104Model.IsSelected))
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
                    SelectedChucVu == null ? null : SelectedChucVu.Ma,
                    TienAn,
                    NgayNhapNgu,
                    IsHsq
                );
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    ItemsCanBo = _mapper.Map<ObservableCollection<CadresNq104Model>>(e.Result);
                    _dtCadresView = CollectionViewSource.GetDefaultView(ItemsCanBo);
                }
                IsLoading = false;
            });
        }

        private void DetailPhuCap_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(AllowenceNq104Model.GiaTri) || args.PropertyName == nameof(AllowenceNq104Model.HuongPCSN))
            {
                AllowenceNq104Model item = (AllowenceNq104Model)sender;
                item.IsModified = true;
                OnPropertyChanged(nameof(ItemsAllowence));
            }
        }

        private bool CapBacFilter(object obj)
        {
            bool result = true;
            var item = (TlDmCapBacNq104Model)obj;
            if (!string.IsNullOrEmpty(SearchCapBac))
            {
                result &= item.MaCb.Contains(SearchCapBac) || item.Note.ToLower().Contains(SearchCapBac.ToLower());
            }
            return result;
        }

        private bool DonViFilter(object obj)
        {
            bool result = true;
            var item = (TlDmDonViNq104Model)obj;
            if (!string.IsNullOrEmpty(SearchDonVi))
            {
                result &= item.MaDonVi.Contains(SearchDonVi) || item.TenDonVi.ToLower().Contains(SearchDonVi.ToLower());
            }
            return result;
        }

        private bool ListPhuCapFilter(object obj)
        {
            bool result = true;

            var item = (AllowenceNq104Model)obj;

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
                var lstPhuCapThanhTien = new List<AllowenceNq104Model>();
                foreach (var item in lstPhuCapUpdate)
                {
                    var cachTinh = _tlDmCachTinhLuongChuanService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork).Where(n =>
                        n.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).Contains(item.MaPhuCap));
                    cachTinh.ForAll(item =>
                    {
                        var phuCapThanhTien = _phuCapService.FindByMaPhuCap(item.MaCot);
                        if (phuCapThanhTien != null)
                            lstPhuCapThanhTien.Add(_mapper.Map<AllowenceNq104Model>(phuCapThanhTien));
                    });
                }
                lstPhuCapUpdate.AddRange(lstPhuCapThanhTien);

                var pcLht = lstPhuCapUpdate.FirstOrDefault(x => PhuCap.LHT_HS.Equals(x.MaPhuCap));
                var lstUpdatePhuCap = new List<TlCanBoPhuCapNq104>();

                foreach (var item in lstUpdate)
                {
                    item.NgayXn = NgayXuatNguMoi;
                    if (lstPhuCapUpdate.Any())
                    {
                        item.HeSoLuong = pcLht?.GiaTri ?? item.HeSoLuong;
                        item.MaCvd104 = SelectedChucVuDieuChinh?.Ma ?? item.MaCvd104;
                        item.LoaiDoiTuong = SelectedCapBac?.Parent ?? item.LoaiDoiTuong;
                        item.MaCb104 = SelectedCapBac?.MaCb ?? item.MaCb104;
                        item.Loai = SelectedLoaiNhom?.MaDm ?? item.Loai;
                        item.NhomChuyenMon = SelectedLoaiNhom?.MaNhom ?? item.NhomChuyenMon;
                        item.MaBacLuong = SelectedBacTienLuong?.MaDm ?? item.MaBacLuong;
                        item.TienLuongCb = SelectedBacTienLuong?.TienLuong ?? item.TienLuongCb;
                        item.TienNangLuongCb = TienNangLuongCb ?? item.TienNangLuongCb;
                        item.TienLuongCvd = TienLuongCvd ?? item.TienLuongCvd;
                        item.TienNangLuongCvd = TienNangLuongCvd ?? item.TienNangLuongCvd;

                        if (SelectedDonViDieuChinh != null)
                        {
                            item.Parent = SelectedDonViDieuChinh.MaDonVi;
                            item.TenDonVi = SelectedDonViDieuChinh.TenDonVi;
                        }
                        if (_selectedCapBac != null)
                        {
                            item.MaCb = _selectedCapBac.MaCb;
                        }
                        if (SelectedChucVuDieuChinh != null)
                        {
                            item.MaCv = SelectedChucVuDieuChinh.Ma;
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

                        foreach (var itemPC in lstPhuCapUpdate)
                        {
                            if (TienAnMoi != null && !string.IsNullOrEmpty(TienAnMoi.ToString()) && itemPC.MaPhuCap.StartsWith("TA"))
                            {
                                itemPC.GiaTri = TienAnMoi;
                            }
                            if (TrichLuong != null && !string.IsNullOrEmpty(TrichLuong.ToString()) && PhuCap.TRICHLUONG_SN.Equals(itemPC.MaPhuCap))
                            {
                                itemPC.GiaTri = TrichLuong;
                            }
                            if (TienTrichLuong != null && !string.IsNullOrEmpty(TienTrichLuong.ToString()) && PhuCap.TRICHLUONG_TIEN.Equals(itemPC.MaPhuCap))
                            {
                                itemPC.GiaTri = TienTrichLuong;
                            }
                            if (NgayXuatNguMoi != null && itemPC.MaPhuCap.Equals(PhuCap.THANG_TCXN) && item.MaCb.StartsWith("0"))
                            {
                                item.NgayXn = NgayXuatNguMoi;
                                itemPC.GiaTri = TinhThangHuongTcxn(item.NgayNn, item.NgayXn);
                            }
                            if (NgayXuatNguMoi != null && itemPC.MaPhuCap.Equals(PhuCap.THANG_TCVIECLAM))
                            {
                                var phuCapThangViecLam = _phuCapService.FindByMaPhuCap(PhuCap.THANG_TCVIECLAM);
                                itemPC.GiaTri = phuCapThangViecLam.GiaTri;
                            }
                        }
                    }

                    var canBoPhuCap = _tlCanBoPhuCapService.FindByMaCanBo(item.MaCanBo).FirstOrDefault();
                    var allowencesSaved = new List<AllowencePhuCapNq104Criteria>();
                    if (canBoPhuCap != null)
                    {
                        var plainText = CompressExtension.DecompressFromBase64(canBoPhuCap.Data);
                        allowencesSaved = JsonConvert.DeserializeObject<AllowenceCanBoNq104Criteria>(plainText).X.ToList();

                        foreach (var phuCap in allowencesSaved)
                        {
                            if (lstPhuCapUpdate.Any(x => x.MaPhuCap == phuCap.A))
                            {
                                phuCap.B = lstPhuCapUpdate.FirstOrDefault(x => x.MaPhuCap == phuCap.A).GiaTri;
                                phuCap.C = lstPhuCapUpdate.FirstOrDefault(x => x.MaPhuCap == phuCap.A).HuongPCSN;
                            }
                        }
                    }
                    string strJson = JsonConvert.SerializeObject(new AllowenceCanBoNq104Criteria()
                    {
                        X = allowencesSaved
                    });
                    canBoPhuCap.MaPhuCap = "";
                    canBoPhuCap.Data = CompressExtension.CompressToBase64(strJson);
                    lstUpdatePhuCap.Add(canBoPhuCap);
                }

                var lstCanbo = _mapper.Map<ObservableCollection<TlDmCanBoNq104>>(lstUpdate).ToList();
                _cadresService.UpdateRange(lstCanbo);
                _tlCanBoPhuCapService.UpdateRange(lstUpdatePhuCap);
                _tlCanBoPhuCapBridgeNq104Service.DataPreprocess(int.Parse(MonthSelected.ValueItem), int.Parse(YearSelected.ValueItem));

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

        private static void SelectAll(bool select, IEnumerable<CadresNq104Model> models)
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
            if (!ItemsCanBo.Where(x => x.Selected).Any())
            {
                messages.Add(Resources.MsgCadresAdjust);
            }

            return string.Join(Environment.NewLine, messages);
        }

        private void LoadDanhMucBacLuong()
        {
            string[] text = { "1", "3.2", "4", "5" };

            var listBacTienLuong = _tlDmCapBacLuongService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork && x.Loai == 3)
                .OrderBy(x => x.XauNoiMa).ToList();

            if (SelectedCapBac != null && text.Contains(SelectedCapBac.Parent))
            {
                listBacTienLuong = listBacTienLuong.Where(x => !string.IsNullOrEmpty(x.LoaiDoiTuong)
                                && x.LoaiDoiTuong.Split(',').Contains(SelectedCapBac.Parent)).ToList();
            }
            else
            {
                listBacTienLuong = listBacTienLuong.Where(x => SelectedLoaiNhom != null
                                            && x.XauNoiMa.Contains(SelectedLoaiNhom.XauNoiMa + "-" + SelectedLoaiNhom.MaNhom)).ToList();
            }

            ItemsBacTienLuong = _mapper.Map<ObservableCollection<TlDmCapBacLuongNq104Model>>(listBacTienLuong);

            List<string> lstString = new List<string>() { LoaiDoiTuong.SQ, LoaiDoiTuong.HCY, LoaiDoiTuong.HSQCS };
            if (lstString.Contains(SelectedCapBac.Parent) && SelectedLoaiNhom == null)
            {
                string sMaCapBac = SelectedCapBac.Parent.Equals(LoaiDoiTuong.HCY) ? SelectedCapBac.MaCb.Remove(0, 1).Insert(0, LoaiDoiTuong.SQ) : SelectedCapBac.MaCb;
                SelectedBacTienLuong = ItemsBacTienLuong.FirstOrDefault(x => x.MaDm.Equals(sMaCapBac));
            }
            else if (SelectedLoaiNhom != null && SelectedLoaiNhom.XauNoiMaNhom != null)
            {
                SelectedBacTienLuong = ItemsBacTienLuong.FirstOrDefault(x => x.XauNoiMa.Contains(SelectedLoaiNhom?.XauNoiMaNhom));
            }
            else
            {
                SelectedBacTienLuong = ItemsBacTienLuong.FirstOrDefault(x => x.MaDm.Equals(SelectedCapBac?.MaCb));
            }
        }

        private void DataModelChange(TypeChangeCustom type, decimal? value)
        {
            AllowenceNq104Model temp = new AllowenceNq104Model();
            AllowenceNq104Model tempOld = new AllowenceNq104Model();
            if (ItemsAllowence.IsEmpty()) return;
            switch (type)
            {
                case TypeChangeCustom.TienLuongCb:
                    temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TLCB_TT"));
                    if (temp != null)
                        temp.GiaTri = value;
                    break;
                case TypeChangeCustom.TienNangLuongCb:
                    temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("NLCB_TT"));
                    tempOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TNLCB_TT_CU"));
                    if (tempOld != null && !IsFirst)
                        tempOld.GiaTri = temp?.GiaTri;
                    if (temp != null && !IsFirst)
                        temp.GiaTri = value;
                    break;
                case TypeChangeCustom.TienLuongBaoLuuCb:
                    temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LBLCB_TT"));
                    tempOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TLBLCB_TT_CU"));
                    if (tempOld != null && !IsFirst)
                        tempOld.GiaTri = temp?.GiaTri;
                    if (temp != null && !IsFirst)
                        temp.GiaTri = value < 0 ? 0 : value;
                    break;
                case TypeChangeCustom.TienNangLuongCvd:
                    temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TNLCV_CD_CC_TT"));
                    tempOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TNLCV_CD_TT_CU"));

                    if (tempOld != null && !IsFirst)
                        tempOld.GiaTri = temp?.GiaTri;
                    if (temp != null && !IsFirst)
                        temp.GiaTri = value;
                    break;
                case TypeChangeCustom.TienLuongBaoLuuCvd:
                    temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TLBLCV_CD_CC_TT"));
                    tempOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TLBLCV_CD_TT_CU"));
                    if (tempOld != null && !IsFirst)
                        tempOld.GiaTri = temp?.GiaTri;
                    if (temp != null && !IsFirst)
                        temp.GiaTri = value;
                    break;

                default:
                    break;

            }
            OnPropertyChanged(nameof(ItemsAllowence));

        }

        private void LoadBacLuongChange()
        {
            if (SelectedCapBac != null && !_itemsBacTienLuong.IsEmpty())
            {
                List<string> lstString = new List<string>() { LoaiDoiTuong.SQ, LoaiDoiTuong.HCY, LoaiDoiTuong.HSQCS };
                if (string.IsNullOrEmpty(SelectedCapBac.MaCb)) return;
                if (!lstString.Contains(SelectedCapBac.Parent)) return;
                string sMaCapBac = SelectedCapBac.Parent.Equals(LoaiDoiTuong.HCY) ? SelectedCapBac.MaCb.Remove(0, 1).Insert(0, LoaiDoiTuong.SQ) : SelectedCapBac.MaCb;
                SelectedBacTienLuong = _itemsBacTienLuong.FirstOrDefault(x => x.MaDm.Equals(sMaCapBac));
            }
        }

        private void LoadDanhMucLoaiNhom()
        {
            string[] text = { "1", "3.2", "4", "5" };

            if (SelectedCapBac != null && text.Contains(SelectedCapBac.Parent))
            {
                ItemsLoaiNhom = new ObservableCollection<TlDmCapBacLuongNq104Model>();
                SelectedLoaiNhom = null;
                LoadDanhMucBacLuong();
            }
            else
            {
                var listLoaiNhom = _tlDmCapBacLuongService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork).Where(x =>
                                SelectedCapBac != null
                                && !string.IsNullOrEmpty(x.LoaiDoiTuong)
                                && x.LoaiDoiTuong.Split(',').Contains(SelectedCapBac.Parent)
                                && (x.Loai == 1 || x.Loai == 2))
                        .OrderBy(x => x.XauNoiMa)
                        .ToList();

                var listLoai = listLoaiNhom.Where(x => x.Loai == 1).ToList();
                var listNhom = listLoaiNhom.Where(x => x.Loai == 2).ToList();

                var listData = from loai in listLoai
                               join nhom in listNhom on loai.MaDm equals nhom.MaDmCha
                               into gj
                               from full in gj.DefaultIfEmpty()
                               select new
                               {
                                   Data = loai,
                                   TenNhom = full?.TenDm,
                                   MaNhom = full?.MaDm,
                                   XauNoiMaNhom = full?.XauNoiMa
                               };
                var listEnd = ObjectCopier.Clone(listData).Select(x =>
                {
                    if (!string.IsNullOrEmpty(x.TenNhom))
                    {
                        x.Data.LoaiNhom = $"{x.Data.TenDm} - {x.TenNhom}";
                        x.Data.MaNhom = x.MaNhom;
                        x.Data.MaLoai = x.Data.MaDm;
                        x.Data.XauNoiMaNhom = x.XauNoiMaNhom;
                    }
                    else
                    {
                        x.Data.LoaiNhom = x.Data.TenDm;
                    }
                    return x.Data;
                }).ToList();
                ItemsLoaiNhom = _mapper.Map<ObservableCollection<TlDmCapBacLuongNq104Model>>(listEnd);
                //SelectedLoaiNhom = _itemsLoaiNhom.FirstOrDefault(x => x.MaLoai == Model.Loai && x.MaNhom == Model.NhomChuyenMon);
            }
        }

        private void LoadHeSoChucVu()
        {
            if (ItemsAllowence != null)
            {
                var phuCap = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == "TLCV_CD_CC_TT");

                if (_selectedChucVuDieuChinh != null && phuCap != null)
                {
                    phuCap.GiaTri = _selectedChucVuDieuChinh.TienLuong;
                    phuCap.IsModified = false;
                }
                else if (_selectedChucVuDieuChinh == null && phuCap != null)
                {
                    phuCap.GiaTri = 0;
                }

                //Chuc Vu
                var cvLCVTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LCV_TT"));
                var cvLNLCVTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("NLCV_TT"));
                var cvLLBLCVTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LBLCV_TT"));

                //Chuc Danh
                var cdLCDTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LCD_TT"));
                var cdNLCDHTTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("NLCD_TT"));
                var cdNLLBLCDHTTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LBLCD_TT"));

                //Chuc Vu
                if (_selectedChucVuDieuChinh != null && _selectedChucVuDieuChinh.Loai == false && cvLCVTT != null)
                {
                    cvLCVTT.GiaTri = _selectedChucVuDieuChinh.TienLuong;
                    cvLCVTT.IsModified = true;
                    if (cdLCDTT != null) cdLCDTT.GiaTri = 0;
                    if (cdNLCDHTTT != null) cdNLCDHTTT.GiaTri = 0;
                    if (cdNLLBLCDHTTT != null) cdNLLBLCDHTTT.GiaTri = 0;
                }
                if (_selectedChucVuDieuChinh != null && _selectedChucVuDieuChinh.Loai == false && cvLNLCVTT != null)
                {
                    cvLNLCVTT.GiaTri = TienNangLuongCvd;
                    cvLNLCVTT.IsModified = true;
                    if (cdLCDTT != null) cdLCDTT.GiaTri = 0;
                    if (cdNLCDHTTT != null) cdNLCDHTTT.GiaTri = 0;
                    if (cdNLLBLCDHTTT != null) cdNLLBLCDHTTT.GiaTri = 0;
                }

                //if (_selectedChucVuDieuChinh != null && _selectedChucVuDieuChinh.Loai == false && cvLLBLCVTT != null)
                //{
                //    cvLLBLCVTT.GiaTri = TienBaoLuuCvd;
                //    cvLLBLCVTT.IsModified = true;
                //    if (cdLCDTT != null) cdLCDTT.GiaTri = 0;
                //    if (cdNLCDHTTT != null) cdNLCDHTTT.GiaTri = 0;
                //    if (cdNLLBLCDHTTT != null) cdNLLBLCDHTTT.GiaTri = 0;
                //}

                //Chuc Danh
                if (_selectedChucVuDieuChinh != null && _selectedChucVuDieuChinh.Loai == true && cdLCDTT != null)
                {
                    cdLCDTT.GiaTri = _selectedChucVuDieuChinh.TienLuong;
                    cdLCDTT.IsModified = true;
                    if (cvLCVTT != null) cvLCVTT.GiaTri = 0;
                    if (cvLNLCVTT != null) cvLNLCVTT.GiaTri = 0;
                    if (cvLLBLCVTT != null) cvLLBLCVTT.GiaTri = 0;
                }
                if (_selectedChucVuDieuChinh != null && _selectedChucVuDieuChinh.Loai == true && cdNLCDHTTT != null)
                {
                    cdNLCDHTTT.GiaTri = TienNangLuongCvd;
                    cdNLCDHTTT.IsModified = true;
                    if (cvLCVTT != null) cvLCVTT.GiaTri = 0;
                    if (cvLNLCVTT != null) cvLNLCVTT.GiaTri = 0;
                    if (cvLLBLCVTT != null) cvLLBLCVTT.GiaTri = 0;
                }

                //if (_selectedChucVuDieuChinh != null && _selectedChucVuDieuChinh.Loai == true && cdNLLBLCDHTTT != null)
                //{
                //    cdNLLBLCDHTTT.GiaTri = TienBaoLuuCvd;
                //    cdNLLBLCDHTTT.IsModified = true;
                //    if (cvLCVTT != null) cvLCVTT.GiaTri = 0;
                //    if (cvLNLCVTT != null) cvLNLCVTT.GiaTri = 0;
                //    if (cvLLBLCVTT != null) cvLLBLCVTT.GiaTri = 0;
                //}

                OnPropertyChanged(nameof(ItemsAllowence));
            }
        }
    }
}
