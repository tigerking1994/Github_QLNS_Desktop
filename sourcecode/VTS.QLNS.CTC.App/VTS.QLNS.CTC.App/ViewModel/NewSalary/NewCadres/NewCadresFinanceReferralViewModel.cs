using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service.UserFunction;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewCadres
{
    public class NewCadresFinanceReferralViewModel : DialogViewModelBase<TlGtTaiChinhModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmCapBacNq104Service _tlDmCapBacService;
        private readonly ITlDmChucVuNq104Service _tlDmChucVuService;
        private readonly ITlGtTaiChinhService _tlGtTaiChinhService;
        private readonly ITlDmCachTinhLuongChuanNq104Service _tlDmCachTinhLuongChuanService;
        private readonly IExportService _exportService;
        private readonly ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private readonly ITlBangLuongThangBridgeNq104Service _tlBangLuongThangBridgeNq104Service;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapService;
        private readonly ITlDmPhuCapNq104Service _tlDmPhuCapService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _donViService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlDmCanBoNq104Service _canboService;
        private readonly IDmChuKyService _iDmChuKyService;
        private readonly IServiceProvider _serviceProvider;
        private ICollectionView _canboView;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_CADRES_PRINT_FINANCE_REFERRAL;

        public override Type ContentType => typeof(View.NewSalary.NewCadres.NewCadresFinanceReferral);
        public override PackIconKind IconKind => PackIconKind.PlaylistPlus;
        public override string Title => "TẠO MỚI GIẤY CUNG CẤP TÀI CHÍNH ĐỐI TƯỢNG HƯỞNG LƯƠNG, PHỤ CẤP";
        public override string Description => "";

        private bool _inMotToChecked;
        public bool InMotToChecked
        {
            get => _inMotToChecked;
            set => SetProperty(ref _inMotToChecked, value);
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
            set
            {
                if (SetProperty(ref _monthSelected, value) && SelectedDonViItems != null && SelectedYear != null)
                {
                    LoadCanBo();
                }
            }
        }

        private ComboboxItem _monthDeNghiSelected;
        public ComboboxItem MonthDeNghiSelected
        {
            get => _monthDeNghiSelected;
            set => SetProperty(ref _monthDeNghiSelected, value);
        }

        private List<ComboboxItem> _sizes;
        public List<ComboboxItem> Sizes
        {
            get => _sizes;
            set => SetProperty(ref _sizes, value);
        }

        private ComboboxItem _sizeSelected;
        public ComboboxItem SizeSelected
        {
            get => _sizeSelected;
            set
            {
                SetProperty(ref _sizeSelected, value);
                OnPropertyChanged(nameof(IsShowA4));
            }
        }

        private decimal? _tadg;
        public decimal? TaDg
        {
            get => _tadg;
            set => SetProperty(ref _tadg, value);
        }

        private int? _thang1;
        public int? Thang1
        {
            get => _thang1;
            set => SetProperty(ref _thang1, value);
        }

        private int? _nam1;
        public int? Nam1
        {
            get => _nam1;
            set => SetProperty(ref _nam1, value);
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

        private decimal? _tienThue1;
        public decimal? TienThue1
        {
            get => _tienThue1;
            set => SetProperty(ref _tienThue1, value);
        }

        private int? _thang2;
        public int? Thang2
        {
            get => _thang2;
            set => SetProperty(ref _thang2, value);
        }

        private int? _nam2;
        public int? Nam2
        {
            get => _nam2;
            set => SetProperty(ref _nam2, value);
        }

        private decimal? _tienThue2;
        public decimal? TienThue2
        {
            get => _tienThue2;
            set => SetProperty(ref _tienThue2, value);
        }

        private int? _thang3;
        public int? Thang3
        {
            get => _thang3;
            set => SetProperty(ref _thang3, value);
        }

        private int? _nam3;
        public int? Nam3
        {
            get => _nam3;
            set => SetProperty(ref _nam3, value);
        }

        private decimal? _tienThue3;
        public decimal? TienThue3
        {
            get => _tienThue3;
            set => SetProperty(ref _tienThue3, value);
        }

        private ObservableCollection<TlDmDonViNq104Model> _donViItems;
        public ObservableCollection<TlDmDonViNq104Model> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViNq104Model _selectedDonViItems;
        public TlDmDonViNq104Model SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                if (SetProperty(ref _selectedDonViItems, value) && SelectedYear != null && MonthSelected != null)
                {
                    LoadCanBo();
                }
            }
        }

        private ObservableCollection<CadresNq104Model> _itemsCanBo;
        public ObservableCollection<CadresNq104Model> ItemsCanBo
        {
            get => _itemsCanBo;
            set => SetProperty(ref _itemsCanBo, value);
        }

        public int Thang { get; set; }
        public int Nam { get; set; }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> ItemsYear
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _selectedYear;
        public ComboboxItem SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (SetProperty(ref _selectedYear, value) && SelectedDonViItems != null && MonthSelected != null)
                {
                    LoadCanBo();
                }
            }
        }

        private string _searchTenCanBo;
        public string SearchTenCanBo
        {
            get => _searchTenCanBo;
            set
            {
                if (SetProperty(ref _searchTenCanBo, value))
                {
                    _canboView.Refresh();
                }
            }
        }

        private string _sCountCanBo;
        public string SCountCanBo
        {
            get => _sCountCanBo;
            set => SetProperty(ref _sCountCanBo, value);
        }

        public bool? IsAllItemSelected
        {
            get
            {
                if (ItemsCanBo != null)
                {
                    var selected = ItemsCanBo.Select(x => x.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : false;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, _mapper.Map<IEnumerable<CadresNq104Model>>(_canboView));
                    OnPropertyChanged();
                }
            }
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
                if (SetProperty(ref _selectedCapBac, value))
                {
                    _canboView.Refresh();
                }
            }
        }

        public RelayCommand NextCommand { get; }
        public RelayCommand ExportSignatureActionCommand { get; }
        public bool IsShowA4 => SizeSelected != null && SizeSelected.ValueItem.Equals("A4");
        private SessionInfo _sessionInfo;

        public NewCadresFinanceReferralViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmChucVuNq104Service tlDmChucVuService,
            ITlDmCapBacNq104Service tlDmCapBacService,
            ITlGtTaiChinhService tlGtTaiChinhService,
            ITlDmCachTinhLuongChuanNq104Service tlDmCachTinhLuongChuanService,
            IExportService exportService,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            ITlCanBoPhuCapNq104Service tlCanBoPhuCapService,
            IDanhMucService danhMucService,
            INsDonViService donViService,
            ITlDmCanBoNq104Service canboService,
            ITlDmDonViNq104Service tlDmDonViService,
            IDmChuKyService iDmChuKyService,
            IServiceProvider serviceProvider,
            ITlDmPhuCapNq104Service tlDmPhuCapService,
            ITlBangLuongThangBridgeNq104Service tlBangLuongThangBridgeNq104Service)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _tlDmChucVuService = tlDmChucVuService;
            _tlDmCapBacService = tlDmCapBacService;
            _tlGtTaiChinhService = tlGtTaiChinhService;
            _tlDmCachTinhLuongChuanService = tlDmCachTinhLuongChuanService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _donViService = donViService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _tlDmPhuCapService = tlDmPhuCapService;
            _canboService = canboService;
            _tlDmDonViService = tlDmDonViService;
            _iDmChuKyService = iDmChuKyService;
            _serviceProvider = serviceProvider;

            NextCommand = new RelayCommand(o => OnNext());
            ExportSignatureActionCommand = new RelayCommand(o => OnOpenCauHinhChuKyDialog());
            _tlBangLuongThangBridgeNq104Service = tlBangLuongThangBridgeNq104Service;
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            try
            {
                MarginRequirement = new Thickness(10);
                TaDg = null;
                Thang1 = null;
                Nam1 = null;
                TienThue1 = null;
                Thang2 = null;
                Nam2 = null;
                TienThue2 = null;
                Thang3 = null;
                Nam3 = null;
                TienThue3 = null;
                InMotToChecked = true;
                TabIndex = ImportTabIndex.Data;
                SCountCanBo = "Đã chọn 0 cán bộ";
                LoadData();
                LoadDonVi();
                LoadMonths();
                LoadYears();
                LoadSizes();
                LoadDanhMucCapBac();
                Model.CapPhatTiepNam = int.Parse(SelectedYear.ValueItem);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnNext()
        {
            TabIndex = ImportTabIndex.MLNS;
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                //var lstData = _tlGtTaiChinhService.FindAll();

                //var capBac = _tlDmCapBacService.FindByMaCapBac(Model.MaCb);
                //if (capBac != null)
                //{
                //    Model.TenCapBac = capBac.Note;
                //}

                //var chucVu = _tlDmChucVuService.FindByMaChucVu(Model.MaCv);
                //if (chucVu != null)
                //{
                //    Model.TenCv = chucVu.TenCv;
                //}

                //if (lstData == null || lstData.Count() == 0)
                //{
                //    Model.MaGiayGttc = "1";
                //}
                //else if (lstData != null || lstData.Count() > 0)
                //{
                //    lstData = lstData.OrderBy(x => int.Parse(x.MaGiayGttc)).ToList();
                //    var lastItem = lstData.LastOrDefault();
                //    Model.MaGiayGttc = (int.Parse(lastItem.MaGiayGttc) + 1).ToString();
                //}

                Model.NgayKyQd = DateTime.Now.Date;
                Model.NgayKy = DateTime.Now.Date;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
            base.LoadData(args);
        }

        private void LoadDonVi()
        {
            TlDmDonViNq104Model donViDefault = new TlDmDonViNq104Model();
            donViDefault.Id = Guid.Empty;
            donViDefault.TenDonVi = " - Tất cả -";

            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            DonViItems = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
            DonViItems.Insert(0, donViDefault);
            SelectedDonViItems = donViDefault;
        }

        private void LoadCanBo()
        {
            if (SelectedYear == null || MonthSelected == null) return;
            var predicate = PredicateBuilder.True<TlDmCanBoNq104>();
            string maDonVi = "";

            if (SelectedDonViItems != null && SelectedDonViItems.Id != Guid.Empty)
                maDonVi = SelectedDonViItems.MaDonVi;
            ItemsCanBo = _mapper.Map<ObservableCollection<CadresNq104Model>>(
                _canboService.FindDanhSachCanBoByCondition(int.Parse(_monthSelected.ValueItem), int.Parse(_selectedYear.ValueItem), maDonVi));
            foreach (CadresNq104Model model in ItemsCanBo)
            {
                model.PropertyChanged += DetailCanBo_PropertyChanged;
            }
            _canboView = CollectionViewSource.GetDefaultView(ItemsCanBo);
            _canboView.Filter = ListCanBoFilter;
        }

        private void DetailCanBo_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(CadresNq104Model.Selected))
            {
                SCountCanBo = string.Format("Đã chọn {0} cán bộ", ItemsCanBo.Count(x => x.Selected));
            }
        }

        private bool ListCanBoFilter(object obj)
        {
            bool result = true;

            var item = (CadresNq104Model)obj;

            if (!string.IsNullOrEmpty(SearchTenCanBo))
            {
                result &= !string.IsNullOrEmpty(item.TenCanBo) && item.TenCanBo.ToLower().Contains(SearchTenCanBo.ToLower());
            }

            if (SelectedCapBac != null && !SelectedCapBac.MaCb.IsEmpty())
            {
                result &= SelectedCapBac.MaCb.Equals(item.MaCb);
            }

            return result;
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            OnPropertyChanged(nameof(Months));
            MonthSelected = Months.FirstOrDefault(x => x.ValueItem == Thang.ToString());
            MonthDeNghiSelected = Months.FirstOrDefault(x => x.ValueItem == Thang.ToString());
        }

        private void LoadYears()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                var year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            OnPropertyChanged(nameof(ItemsYear));
            SelectedYear = _years.FirstOrDefault(x => x.ValueItem == Nam.ToString());
        }

        private void LoadSizes()
        {
            _sizes = new List<ComboboxItem>();
            _sizes.Add(new ComboboxItem("A5", "A5"));
            _sizes.Add(new ComboboxItem("A4", "A4"));
            OnPropertyChanged(nameof(Sizes));
            SizeSelected = Sizes.FirstOrDefault(x => x.ValueItem.Equals("A5"));
        }
        private void LoadDanhMucCapBac()
        {
            var data = _tlDmCapBacService.FindByNote().Where(x => !string.IsNullOrEmpty(x.Parent));
            _itemsCapBac = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(data);
            TlDmCapBacNq104Model allCapBacItem = new TlDmCapBacNq104Model()
            {
                MaCb = "",
                TenCb = "Tất cả"
            };
            _itemsCapBac.Insert(0, allCapBacItem);
            // Nếu là thêm mới thì Model.MaCb null => _selectedCapBacItems cũng null nên ko cần check thêm mới thì set null
            _selectedCapBac = allCapBacItem;
            OnPropertyChanged(nameof(SelectedCapBac));
            OnPropertyChanged(nameof(ItemsCapBac));
        }

        public override void OnSave()
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Warning(message);
            }
            else
            {
                try
                {
                    TlGtTaiChinh tlGtTaiChinh = new TlGtTaiChinh();
                    tlGtTaiChinh = _mapper.Map<TlGtTaiChinh>(Model);
                    tlGtTaiChinh.Id = Guid.NewGuid();
                    tlGtTaiChinh.CapPhatTiepThang = int.Parse(MonthDeNghiSelected.ValueItem);
                    tlGtTaiChinh.CapPhatTiepNam = int.Parse(SelectedYear.ValueItem);

                    //_tlGtTaiChinhService.Add(tlGtTaiChinh);
                    //SavedAction?.Invoke(_mapper.Map<CadresModel>(tlGtTaiChinh));
                    ExportGiayGtTaiChinh(Model);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                }
            }
        }

        private void ExportGiayGtTaiChinh(TlGtTaiChinhModel tlGtTaiChinhModel)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    var listCanSelected = ItemsCanBo.Where(x => x.Selected);
                    string maDonVi = "";

                    if (SelectedDonViItems != null && SelectedDonViItems.Id != Guid.Empty)
                        maDonVi = SelectedDonViItems.MaDonVi;
                    _tlBangLuongThangBridgeNq104Service.DataPreprocess(int.Parse(MonthSelected.ValueItem), int.Parse(SelectedYear.ValueItem), maDonVi, CachTinhLuong.CACH0);
                    //var predicate = PredicateBuilder.True<TlBangLuongThangNq104>();
                    //predicate = predicate.And(x => x.MaCachTl == CachTinhLuong.CACH0);
                    //predicate = predicate.And(x => x.Thang == int.Parse(MonthSelected.ValueItem));
                    //predicate = predicate.And(x => x.Nam == int.Parse(SelectedYear.ValueItem));
                    //predicate = predicate.And(x => listCanSelected.Select(x => x.MaCanBo).Contains(x.MaCbo));

                    //var lstData = _tlBangLuongThangService.FindByCondition(predicate);
                    var lstData = _tlBangLuongThangBridgeNq104Service.FindAll();
                    lstData = lstData.Where(x => listCanSelected.Select(x => x.MaCanBo).Contains(x.MaCanBo));
                    Dictionary<string, decimal?> dicTienPhuCap = new Dictionary<string, decimal?>();
                    foreach (var item in lstData)
                    {
                        if (!dicTienPhuCap.ContainsKey(item.MaPhuCap + item.MaCanBo))
                            dicTienPhuCap.Add(item.MaPhuCap + item.MaCanBo, item.GiaTri);
                    }
                    var lstAllPc = _tlDmPhuCapService.FindAll();
                    var cachTinhDacThu = _tlDmCachTinhLuongChuanService.FindByMaCot(PhuCap.PCDACTHU_SUM);
                    var isHasPcThuHut = false;
                    if (cachTinhDacThu != null)
                    {
                        isHasPcThuHut = cachTinhDacThu.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).Contains(PhuCap.PCTHUHUT_TT);
                    }

                    foreach (var canbo in listCanSelected)
                    {
                        var capBac = _tlDmCapBacService.FindByMaCapBac(canbo.LoaiDoiTuong);
                        var tenLoaiandTenNhomCanBo = _tlDmCapBacService.FindByTenLoaiAndTenNhom(int.Parse(SelectedYear.ValueItem), int.Parse(MonthSelected.ValueItem), canbo.MaCanBo, canbo.Parent).FirstOrDefault();
                        //var giayGioiThieuCanBo = _mapper.Map<TlGtTaiChinhModel>(canbo);
                        RptGiayGtTaiChinhNq104Model rptGiayGtTaiChinhModel = _mapper.Map<RptGiayGtTaiChinhNq104Model>(canbo);
                        rptGiayGtTaiChinhModel.NgayNn = canbo.NgayNn;
                        rptGiayGtTaiChinhModel.NgayXn = canbo.NgayXn;
                        rptGiayGtTaiChinhModel.NgayTn = canbo.NgayTn;
                        rptGiayGtTaiChinhModel.MaSoDinhDanh = canbo.MaSoDinhDanh;
                        rptGiayGtTaiChinhModel.TenLoaiDoiTuong = capBac.TenCb;
                        rptGiayGtTaiChinhModel.ThangTNN = canbo.ThangTnn;
                        rptGiayGtTaiChinhModel.LoaiNhom = (tenLoaiandTenNhomCanBo.Tenloai is null && tenLoaiandTenNhomCanBo.TenNhom is null) ? string.Empty : (tenLoaiandTenNhomCanBo.Tenloai + " - " + tenLoaiandTenNhomCanBo.TenNhom);
                        rptGiayGtTaiChinhModel.FLCB_SUM = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.LCB_SUM + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FLCBHT_TT = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.LCBHT_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FNLCBHT_TT = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.NLCBHT_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FLBLCBHT_TT = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.LBLCBHT_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FLCVCD_SUM = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.LCVCD_SUM + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FLCVCDHT_TT = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.LCVCDHT_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FNLCVCDHT_TT = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.NLCVCDHT_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FLBLCVCDHT_TT = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.LBLCVCDHT_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FPCKIE_TT = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.PCKIE_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FPCKV_TT = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.PCKV_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FPCTN_TT = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.PCTN_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FPCTNCV_TT = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.PCTNCV_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FPCTNVK_TT = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.PCTNVK_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FPCCTKK_TT = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.PCCTKK_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FPCANQP_TT = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.PCANQP_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FPCDACTHU_SUM = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.PCDACTHU_SUM + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FTaDg = dicTienPhuCap.GetValueOrDefault(PhuCapNq104.TA_DG + canbo.MaCanBo, 0);

                        rptGiayGtTaiChinhModel.fLhtTt = dicTienPhuCap.GetValueOrDefault(PhuCap.LHT_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.fPccvTt = dicTienPhuCap.GetValueOrDefault(PhuCap.PCCV_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.fPctnTt = dicTienPhuCap.GetValueOrDefault(PhuCap.PCTN_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.fPcTraSum = dicTienPhuCap.GetValueOrDefault(PhuCap.PCTRA_SUM + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.fPcDacThuSum = dicTienPhuCap.GetValueOrDefault(PhuCap.PCDACTHU_SUM + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.fPcKhacSum = dicTienPhuCap.GetValueOrDefault(PhuCap.PCKHAC_SUM + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.fPctnvkTt = dicTienPhuCap.GetValueOrDefault(PhuCap.PCTNVK_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.fPckvTt = dicTienPhuCap.GetValueOrDefault(PhuCap.PCKV_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.fPcthdTt = dicTienPhuCap.GetValueOrDefault(PhuCap.PCTHD_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.fPccovTt = dicTienPhuCap.GetValueOrDefault(PhuCap.PCCOV_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.fPcThuhutTt = dicTienPhuCap.GetValueOrDefault(PhuCap.PCTHUHUT_TT + canbo.MaCanBo, 0);
                        rptGiayGtTaiChinhModel.FTaDg = TaDg;

                        FormatNumber formatNumber = new FormatNumber(1, ExportType.PDF);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("MaCanBo", rptGiayGtTaiChinhModel.MaCanBo);
                        data.Add("TenCanBo", rptGiayGtTaiChinhModel.TenCanBo);
                        data.Add("TenCapBac", rptGiayGtTaiChinhModel.TenCapBac);
                        data.Add("TenChucVu", rptGiayGtTaiChinhModel.TenCv);

                        data.Add("MaDinhDanh", rptGiayGtTaiChinhModel.MaSoDinhDanh);
                        data.Add("TenLoaiDoiTuong", rptGiayGtTaiChinhModel.TenLoaiDoiTuong);
                        data.Add("LoaiNhom", rptGiayGtTaiChinhModel.LoaiNhom);
                        data.Add("ThangTNN", rptGiayGtTaiChinhModel.ThangTNN);

                        data.Add("NgayNhapNgu", rptGiayGtTaiChinhModel.NgayNn.HasValue ? rptGiayGtTaiChinhModel.NgayNn.Value.ToString("dd/MM/yyyy") : string.Empty);
                        data.Add("NgayXuatNgu", rptGiayGtTaiChinhModel.NgayXn.HasValue ? rptGiayGtTaiChinhModel.NgayXn.Value.ToString("dd/MM/yyyy") : string.Empty);
                        data.Add("NgayTaiNgu", rptGiayGtTaiChinhModel.NgayTn.HasValue ? rptGiayGtTaiChinhModel.NgayXn.Value.ToString("dd/MM/yyyy") : string.Empty);
                        data.Add("SoSoLuong", rptGiayGtTaiChinhModel.SoSoLuong);
                        data.Add("SoTaiKhoan", rptGiayGtTaiChinhModel.SoTaiKhoan);
                        data.Add("NganHang", Model.NganHang);
                        data.Add("NoiChuyenDen", Model.NoiChuyenDen);
                        data.Add("QuyetDinhSo", Model.SoQd);
                        data.Add("NgayThangQuyetDinh", string.Format("Ngày {0} Tháng {1} Năm {2}", Model.NgayKyQd.Value.Day, Model.NgayKyQd.Value.Month, Model.NgayKyQd.Value.Year));

                        data.Add("LCB_SUM", rptGiayGtTaiChinhModel.FLCB_SUM);
                        data.Add("LCBHT_TT", rptGiayGtTaiChinhModel.FLCBHT_TT);
                        data.Add("NLCBHT_TT", rptGiayGtTaiChinhModel.FNLCBHT_TT);
                        data.Add("LBLCBHT_TT", rptGiayGtTaiChinhModel.FLBLCBHT_TT);
                        data.Add("LCVCD_SUM", rptGiayGtTaiChinhModel.FLCVCD_SUM);
                        data.Add("LCVCDHT_TT", rptGiayGtTaiChinhModel.FLCVCDHT_TT);
                        data.Add("NLCVCDHT_TT", rptGiayGtTaiChinhModel.FNLCVCDHT_TT);
                        data.Add("LBLCVCDHT_TT", rptGiayGtTaiChinhModel.FLBLCVCDHT_TT);
                        data.Add("PCKIE_TT", rptGiayGtTaiChinhModel.FPCKIE_TT);
                        data.Add("PCKV_TT", rptGiayGtTaiChinhModel.FPCKV_TT);
                        data.Add("PCTN_TT", rptGiayGtTaiChinhModel.FPCTN_TT);
                        data.Add("PCTNCV_TT", rptGiayGtTaiChinhModel.FPCTNCV_TT);
                        data.Add("PCTNVK_TT", rptGiayGtTaiChinhModel.FPCTNVK_TT);
                        data.Add("PCANQP_TT", rptGiayGtTaiChinhModel.FPCANQP_TT);
                        data.Add("PCDACTHU_SUM", rptGiayGtTaiChinhModel.FPCDACTHU_SUM);
                        data.Add("PCCTKK_TT", rptGiayGtTaiChinhModel.FPCCTKK_TT);
                        data.Add("TA_DG", rptGiayGtTaiChinhModel.FTaDg);

                        DateTime capNhapNgayThangHet = new DateTime(tlGtTaiChinhModel.CapPhatTiepNam, int.Parse(MonthDeNghiSelected.ValueItem), 1).AddMonths(-1);
                        data.Add("CapPhatNgayThangHet", string.Format("{0} năm {1}", capNhapNgayThangHet.Month, capNhapNgayThangHet.Year));
                        data.Add("CapPhatNgayThang", string.Format("{0} năm {1}", MonthDeNghiSelected.ValueItem, tlGtTaiChinhModel.CapPhatTiepNam));

                        data.Add("LhtTt", rptGiayGtTaiChinhModel.fLhtTt == 0 ? null : rptGiayGtTaiChinhModel.fLhtTt);
                        data.Add("PccvTt", rptGiayGtTaiChinhModel.fPccvTt == 0 ? null : rptGiayGtTaiChinhModel.fPccvTt);
                        data.Add("PctnTt", rptGiayGtTaiChinhModel.fPctnTt == 0 ? null : rptGiayGtTaiChinhModel.fPctnTt);
                        data.Add("PcTraSum", rptGiayGtTaiChinhModel.fPcTraSum == 0 ? null : rptGiayGtTaiChinhModel.fPcTraSum);
                        data.Add("PcKhacSum", rptGiayGtTaiChinhModel.fPcKhacSum == 0 ? null : rptGiayGtTaiChinhModel.fPcKhacSum);
                        data.Add("TaDg", rptGiayGtTaiChinhModel.FTaDg == 0 ? null : rptGiayGtTaiChinhModel.FTaDg);
                        data.Add("PctnvkTt", rptGiayGtTaiChinhModel.fPctnvkTt == 0 ? null : rptGiayGtTaiChinhModel.fPctnvkTt);
                        data.Add("PckvTt", rptGiayGtTaiChinhModel.fPckvTt == 0 ? null : rptGiayGtTaiChinhModel.fPckvTt);
                        data.Add("PcthdTt", rptGiayGtTaiChinhModel.fPcthdTt == 0 ? null : rptGiayGtTaiChinhModel.fPcthdTt);
                        data.Add("PccovTt", rptGiayGtTaiChinhModel.fPccovTt == 0 ? null : rptGiayGtTaiChinhModel.fPccovTt);
                        data.Add("BuGiaGao", rptGiayGtTaiChinhModel.fBuGiaGao == 0 ? null : rptGiayGtTaiChinhModel.fBuGiaGao);
                        //DateTime capNhapNgayThangHet = new DateTime(tlGtTaiChinhModel.CapPhatTiepNam, int.Parse(MonthDeNghiSelected.ValueItem), 1).AddMonths(-1);
                        //data.Add("CapPhatNgayThangHet", string.Format("{0} năm {1}", capNhapNgayThangHet.Month, capNhapNgayThangHet.Year));
                        //data.Add("CapPhatNgayThang", string.Format("{0} năm {1}", MonthDeNghiSelected.ValueItem, tlGtTaiChinhModel.CapPhatTiepNam));
                        data.Add("LoPhiDuocCap", (decimal?)Model.LoPhiDuocCap);
                        data.Add("LoPhiThanhToan", (decimal?)Model.LoPhiThanhToan);
                        data.Add("NgayKyGiay", string.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        data.Add("ChuKyQuanNhan", canbo.TenCanBo);
                        AddChuKy(data, TypeChuKy.RPT_TL_GIAY_GIOI_THIEU_TAI_CHINH_NEW);

                        string templateFileName = "";
                        if (SizeSelected != null && SizeSelected.ValueItem.Equals("A5"))
                        {
                            data.Add("PcDacThuSum", rptGiayGtTaiChinhModel.fPcDacThuSum == 0 ? null : rptGiayGtTaiChinhModel.fPcDacThuSum);
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAY_GT_TC_NEW);
                        }
                        else if (SizeSelected != null && SizeSelected.ValueItem.Equals("A4"))
                        {
                            //var thanhTien = dicTienPhuCap.GetValueOrDefault(PhuCap.LUONGTHANG_SUM + Model.MaCanBo, 0);
                            var lstPcCoThoiHan = _tlCanBoPhuCapService.FindAll(x => x.DateStart != null && x.MaCbo.Equals(canbo.MaCanBo)).ToList();

                            decimal? pcDacThuSum = 0;
                            if (isHasPcThuHut)
                            {
                                pcDacThuSum = (rptGiayGtTaiChinhModel.fPcDacThuSum ?? 0) - (rptGiayGtTaiChinhModel.fPcThuhutTt ?? 0);
                            }
                            else
                            {
                                pcDacThuSum = rptGiayGtTaiChinhModel.fPcDacThuSum ?? 0;
                            }
                            var tongCong = (rptGiayGtTaiChinhModel.fLhtTt ?? 0) + (rptGiayGtTaiChinhModel.fPccvTt ?? 0)
                                + (rptGiayGtTaiChinhModel.fPctnTt ?? 0) + (rptGiayGtTaiChinhModel.fPctnvkTt ?? 0)
                                + (rptGiayGtTaiChinhModel.fPccovTt ?? 0) + (rptGiayGtTaiChinhModel.fPcthdTt ?? 0)
                                + (rptGiayGtTaiChinhModel.fPckvTt ?? 0) + (rptGiayGtTaiChinhModel.fPcTraSum ?? 0)
                                + (pcDacThuSum ?? 0) + (rptGiayGtTaiChinhModel.fPcThuhutTt ?? 0)
                                + (rptGiayGtTaiChinhModel.fPcKhacSum ?? 0);

                            data.Add("PcDacThuSum", pcDacThuSum == 0 ? null : pcDacThuSum);
                            data.Add("ThanhTien", Math.Ceiling(tongCong));
                            data.Add("Nam1", Nam1);
                            data.Add("Thang1", Thang1);
                            data.Add("TienThue1", TienThue1);
                            data.Add("Nam2", Nam2);
                            data.Add("Thang2", Thang2);
                            data.Add("TienThue2", TienThue2);
                            data.Add("Nam3", Nam3);
                            data.Add("Thang3", Thang3);
                            data.Add("TienThue3", TienThue3);
                            data.Add("PcThuHutTt", rptGiayGtTaiChinhModel.fPcThuhutTt == 0 ? null : rptGiayGtTaiChinhModel.fPcThuhutTt);


                            if (lstPcCoThoiHan == null || lstPcCoThoiHan.Count == 0)
                            {
                                goto FileName;
                            }

                            if (lstPcCoThoiHan.Count >= 1 && lstPcCoThoiHan[0] != null)
                            {
                                var phuCap = lstAllPc.FirstOrDefault(x => x.MaPhuCap.Equals(lstPcCoThoiHan[0].MaPhuCap));
                                if (phuCap == null)
                                {
                                    data.Add("Ten1", null);
                                    data.Add("Ngay1", null);
                                    data.Add("ThangDaHuong1", null);
                                    data.Add("HeSo1", 0);
                                }
                                else
                                {
                                    data.Add("Ten1", phuCap.TenPhuCap);
                                    data.Add("Ngay1", lstPcCoThoiHan[0].DateStart);
                                    data.Add("ThangDaHuong1", lstPcCoThoiHan[0].ISoThangHuong);
                                    data.Add("HeSo1", (lstPcCoThoiHan[0].GiaTri ?? 0));
                                }
                            }
                            else
                            {
                                data.Add("Ten1", null);
                                data.Add("Ngay1", null);
                                data.Add("ThangDaHuong1", null);
                                data.Add("HeSo1", 0);
                            }

                            if (lstPcCoThoiHan.Count >= 2 && lstPcCoThoiHan[1] != null)
                            {
                                var phuCap = lstAllPc.FirstOrDefault(x => x.MaPhuCap.Equals(lstPcCoThoiHan[1].MaPhuCap));
                                if (phuCap == null)
                                {
                                    data.Add("Ten2", null);
                                    data.Add("Ngay2", null);
                                    data.Add("ThangDaHuong2", null);
                                    data.Add("HeSo2", null);
                                }
                                else
                                {
                                    data.Add("Ten2", phuCap.TenPhuCap);
                                    data.Add("Ngay2", lstPcCoThoiHan[1].DateStart);
                                    data.Add("ThangDaHuong2", lstPcCoThoiHan[1].ISoThangHuong);
                                    data.Add("HeSo2", (lstPcCoThoiHan[1].GiaTri ?? 0));
                                }
                            }
                            else
                            {
                                data.Add("Ten2", null);
                                data.Add("Ngay2", null);
                                data.Add("ThangDaHuong2", null);
                                data.Add("HeSo2", null);
                            }

                            if (lstPcCoThoiHan.Count >= 3 && lstPcCoThoiHan[2] != null)
                            {
                                var phuCap = lstAllPc.FirstOrDefault(x => x.MaPhuCap.Equals(lstPcCoThoiHan[2].MaPhuCap));
                                if (phuCap == null)
                                {
                                    data.Add("Ten3", null);
                                    data.Add("Ngay3", null);
                                    data.Add("ThangDaHuong3", null);
                                    data.Add("HeSo3", null);
                                }
                                else
                                {
                                    data.Add("Ten3", phuCap.TenPhuCap);
                                    data.Add("Ngay3", lstPcCoThoiHan[2].DateStart);
                                    data.Add("ThangDaHuong3", lstPcCoThoiHan[2].ISoThangHuong);
                                    data.Add("HeSo3", (lstPcCoThoiHan[2].GiaTri ?? 0));
                                }
                            }
                            else
                            {
                                data.Add("Ten3", null);
                                data.Add("Ngay3", null);
                                data.Add("ThangDaHuong3", null);
                                data.Add("HeSo3", null);
                            }

                        FileName:
                            if (lstPcCoThoiHan == null || lstPcCoThoiHan.Count == 0)
                            {
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAY_GT_TC_A4_KHONG_PC_HUONG_NEW);
                            }
                            else
                            {
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAY_GT_TC_A4_NEW);
                            }
                        }

                        string fileNamePrefix = string.Format("Giay_GioiThieu_TaiChinh_CanBo_{0}_Thang_{1}_Nam_{2}",
                            rptGiayGtTaiChinhModel.TenCanBo, MonthSelected.ValueItem, SelectedYear.ValueItem);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<RptGiayGtTaiChinhNq104Model>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (result != null)
                        {
                            if (InMotToChecked)
                            {
                                _exportService.Open(result, ExportType.PDF_ONE_PAPER);
                            }
                            else
                            {
                                _exportService.Open(result, ExportType.PDF);
                            }
                        }
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
            if (string.IsNullOrEmpty(Model.NoiChuyenDen))
            {
                messages.Add(string.Format(Resources.PlaceNull));
                goto End;
            }

        End:
            return string.Join(Environment.NewLine, messages);
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        private void OnOpenCauHinhChuKyDialog()
        {
            try
            {
                string idTypeBc = TypeChuKy.RPT_TL_GIAY_GIOI_THIEU_TAI_CHINH_NEW;
                var dmChuKy = _iDmChuKyService.FindByCondition(x => x.IdType.Equals(idTypeBc)).FirstOrDefault();
                DmChuKyDialogViewModel dmChuKyDialogViewModel = new DmChuKyDialogViewModel(_mapper, _serviceProvider, _sessionService);
                dmChuKyDialogViewModel.DmChuKyModel =
                    dmChuKy != null ? _mapper.Map<DmChuKyModel>(dmChuKy) : new DmChuKyModel()
                    {
                        IdType = idTypeBc,
                        IdCode = "xx"
                    };
                dmChuKyDialogViewModel.Init();
                dmChuKyDialogViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            var dmChyKy = _iDmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (dmChyKy != null)
            {
                data.Add("ThuaLenh1", dmChyKy.ThuaLenh1MoTa);
                data.Add("ChucDanh1", dmChyKy.ChucDanh1MoTa);
                data.Add("Ten1", dmChyKy.Ten1MoTa);

                data.Add("ThuaLenh2", dmChyKy.ThuaLenh2MoTa);
                data.Add("ChucDanh2", dmChyKy.ChucDanh2MoTa);
                data.Add("Ten2", dmChyKy.Ten2MoTa);

                data.Add("ThuaLenh3", dmChyKy.ThuaLenh3MoTa);
                data.Add("ChucDanh3", dmChyKy.ChucDanh3MoTa);
                data.Add("Ten3", dmChyKy.Ten3MoTa);
            }
            else
            {
                data.Add("ThuaLenh1", string.Empty);
                data.Add("ChucDanh1", string.Empty);
                data.Add("Ten1", string.Empty);

                data.Add("ThuaLenh2", string.Empty);
                data.Add("ChucDanh2", string.Empty);
                data.Add("Ten2", string.Empty);

                data.Add("ThuaLenh3", string.Empty);
                data.Add("ChucDanh3", string.Empty);
                data.Add("Ten3", string.Empty);
            }
        }
    }
}
