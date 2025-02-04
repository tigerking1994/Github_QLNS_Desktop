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

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Cadres
{
    public class CadresFinanceReferralViewModel : DialogViewModelBase<TlGtTaiChinhModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmCapBacService _tlDmCapBacService;
        private readonly ITlDmChucVuService _tlDmChucVuService;
        private readonly ITlGtTaiChinhService _tlGtTaiChinhService;
        private readonly ITlDmCachTinhLuongChuanService _tlDmCachTinhLuongChuanService;
        private readonly IExportService _exportService;
        private readonly ITlBangLuongThangService _tlBangLuongThangService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly ITlDmPhuCapService _tlDmPhuCapService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _donViService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDmCanBoService _canboService;
        private readonly IDmChuKyService _iDmChuKyService;
        private readonly IServiceProvider _serviceProvider;
        private ICollectionView _canboView;

        public override string FuncCode => NSFunctionCode.SALARY_CADRES_PRINT_FINANCE_REFERRAL;

        public override Type ContentType => typeof(View.Salary.Cadres.CadresFinanceReferral);
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

        private ObservableCollection<TlDmDonViModel> _donViItems;
        public ObservableCollection<TlDmDonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViModel _selectedDonViItems;
        public TlDmDonViModel SelectedDonViItems
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

        private ObservableCollection<CadresModel> _itemsCanBo;
        public ObservableCollection<CadresModel> ItemsCanBo
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
            set {
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
                    SelectAll(value.Value, _mapper.Map<IEnumerable<CadresModel>>(_canboView));
                    OnPropertyChanged();
                }
            }
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

        public CadresFinanceReferralViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmChucVuService tlDmChucVuService,
            ITlDmCapBacService tlDmCapBacService,
            ITlGtTaiChinhService tlGtTaiChinhService,
            ITlDmCachTinhLuongChuanService tlDmCachTinhLuongChuanService,
            IExportService exportService,
            ITlBangLuongThangService tlBangLuongThangService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            IDanhMucService danhMucService,
            INsDonViService donViService,
            ITlDmCanBoService canboService,
            ITlDmDonViService tlDmDonViService,
            IDmChuKyService iDmChuKyService,
            IServiceProvider serviceProvider,
            ITlDmPhuCapService tlDmPhuCapService)
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
            TlDmDonViModel donViDefault = new TlDmDonViModel();
            donViDefault.Id = Guid.Empty;
            donViDefault.TenDonVi = " - Tất cả -";

            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            DonViItems = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);
            DonViItems.Insert(0, donViDefault);
            SelectedDonViItems = donViDefault;
        }

        private void LoadCanBo()
        {
            if (SelectedYear == null || MonthSelected == null) return;
            var predicate = PredicateBuilder.True<TlDmCanBo>();
            string maDonVi = "";
            
            if (SelectedDonViItems != null && SelectedDonViItems.Id != Guid.Empty)
                maDonVi = SelectedDonViItems.MaDonVi;
            ItemsCanBo = _mapper.Map<ObservableCollection<CadresModel>>(
                _canboService.FindDanhSachCanBoByCondition(int.Parse(_monthSelected.ValueItem), int.Parse(_selectedYear.ValueItem), maDonVi));
            foreach (CadresModel model in ItemsCanBo)
            {
                model.PropertyChanged += DetailCanBo_PropertyChanged;
            }
            _canboView = CollectionViewSource.GetDefaultView(ItemsCanBo);
            _canboView.Filter = ListCanBoFilter;
        }

        private void DetailCanBo_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(CadresModel.Selected))
            {
                SCountCanBo = string.Format("Đã chọn {0} cán bộ", ItemsCanBo.Count(x => x.Selected));
            }
        }

        private bool ListCanBoFilter(object obj)
        {
            bool result = true;

            var item = (CadresModel)obj;

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
            _itemsCapBac = _mapper.Map<ObservableCollection<TlDmCapBacModel>>(data);
            TlDmCapBacModel allCapBacItem = new TlDmCapBacModel()
            {
                MaCb = "",
                Note = "Tất cả"
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

                    var predicate = PredicateBuilder.True<TlBangLuongThang>();
                    predicate = predicate.And(x => x.MaCachTl == CachTinhLuong.CACH0);
                    predicate = predicate.And(x => x.Thang == int.Parse(MonthSelected.ValueItem));
                    predicate = predicate.And(x => x.Nam == int.Parse(SelectedYear.ValueItem));
                    predicate = predicate.And(x => listCanSelected.Select(x => x.MaCanBo).Contains(x.MaCbo));

                    var lstData = _tlBangLuongThangService.FindByCondition(predicate);
                    Dictionary<string, decimal?> dicTienPhuCap = new Dictionary<string, decimal?>();
                    foreach (var item in lstData)
                    {
                        if (!dicTienPhuCap.ContainsKey(item.MaPhuCap + item.MaCbo))
                            dicTienPhuCap.Add(item.MaPhuCap + item.MaCbo, item.GiaTri);
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
                        //var giayGioiThieuCanBo = _mapper.Map<TlGtTaiChinhModel>(canbo);
                        RptGiayGtTaiChinhModel rptGiayGtTaiChinhModel = _mapper.Map<RptGiayGtTaiChinhModel>(canbo);
                        rptGiayGtTaiChinhModel.sNgayNn = canbo.NgayNn;
                        rptGiayGtTaiChinhModel.sNgayXn = canbo.NgayXn;
                        rptGiayGtTaiChinhModel.sNgayTn = canbo.NgayTn;

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
                        rptGiayGtTaiChinhModel.fTaDg = TaDg;

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("MaCanBo", rptGiayGtTaiChinhModel.sMaCanBo);
                        data.Add("TenCanBo", rptGiayGtTaiChinhModel.sTenCanBo);
                        data.Add("TenCapBac", rptGiayGtTaiChinhModel.sTenCapBac);
                        data.Add("TenChucVu", rptGiayGtTaiChinhModel.sTenCv);
                        data.Add("NgayNhapNgu", rptGiayGtTaiChinhModel.sNgayNn.HasValue ? rptGiayGtTaiChinhModel.sNgayNn.Value.ToString("dd/MM/yyyy") : string.Empty);
                        data.Add("NgayXuatNgu", rptGiayGtTaiChinhModel.sNgayXn.HasValue ? rptGiayGtTaiChinhModel.sNgayXn.Value.ToString("dd/MM/yyyy") : string.Empty);
                        data.Add("NgayTaiNgu", rptGiayGtTaiChinhModel.sNgayTn.HasValue ? rptGiayGtTaiChinhModel.sNgayXn.Value.ToString("dd/MM/yyyy") : string.Empty);
                        data.Add("SoSoLuong", rptGiayGtTaiChinhModel.sSoSoLuong);
                        data.Add("SoTaiKhoan", rptGiayGtTaiChinhModel.sSoTaiKhoan);
                        data.Add("NganHang", Model.NganHang);
                        data.Add("NoiChuyenDen", Model.NoiChuyenDen);
                        data.Add("QuyetDinhSo", Model.SoQd);
                        data.Add("NgayThangQuyetDinh", string.Format("Ngày {0} Tháng {1} Năm {2}", Model.NgayKyQd.Value.Day, Model.NgayKyQd.Value.Month, Model.NgayKyQd.Value.Year));
                        data.Add("LhtTt", rptGiayGtTaiChinhModel.fLhtTt == 0 ? null : rptGiayGtTaiChinhModel.fLhtTt);
                        data.Add("PccvTt", rptGiayGtTaiChinhModel.fPccvTt == 0 ? null : rptGiayGtTaiChinhModel.fPccvTt);
                        data.Add("PctnTt", rptGiayGtTaiChinhModel.fPctnTt == 0 ? null : rptGiayGtTaiChinhModel.fPctnTt);
                        data.Add("PcTraSum", rptGiayGtTaiChinhModel.fPcTraSum == 0 ? null : rptGiayGtTaiChinhModel.fPcTraSum);
                        data.Add("PcKhacSum", rptGiayGtTaiChinhModel.fPcKhacSum == 0 ? null : rptGiayGtTaiChinhModel.fPcKhacSum);
                        data.Add("TaDg", rptGiayGtTaiChinhModel.fTaDg == 0 ? null : rptGiayGtTaiChinhModel.fTaDg);
                        data.Add("PctnvkTt", rptGiayGtTaiChinhModel.fPctnvkTt == 0 ? null : rptGiayGtTaiChinhModel.fPctnvkTt);
                        data.Add("PckvTt", rptGiayGtTaiChinhModel.fPckvTt == 0 ? null : rptGiayGtTaiChinhModel.fPckvTt);
                        data.Add("PcthdTt", rptGiayGtTaiChinhModel.fPcthdTt == 0 ? null : rptGiayGtTaiChinhModel.fPcthdTt);
                        data.Add("PccovTt", rptGiayGtTaiChinhModel.fPccovTt == 0 ? null : rptGiayGtTaiChinhModel.fPccovTt);
                        data.Add("BuGiaGao", rptGiayGtTaiChinhModel.fBuGiaGao == 0 ? null : rptGiayGtTaiChinhModel.fBuGiaGao);
                        DateTime capNhapNgayThangHet = new DateTime(tlGtTaiChinhModel.CapPhatTiepNam, int.Parse(MonthDeNghiSelected.ValueItem), 1).AddMonths(-1);
                        data.Add("CapPhatNgayThangHet", string.Format("{0} năm {1}", capNhapNgayThangHet.Month, capNhapNgayThangHet.Year));
                        data.Add("CapPhatNgayThang", string.Format("{0} năm {1}", MonthDeNghiSelected.ValueItem, tlGtTaiChinhModel.CapPhatTiepNam));
                        data.Add("LoPhiDuocCap", (decimal?)Model.LoPhiDuocCap);
                        data.Add("LoPhiThanhToan", (decimal?)Model.LoPhiThanhToan);
                        data.Add("NgayKyGiay", string.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader); 
                        data.Add("ChuKyQuanNhan", canbo.TenCanBo);
                        AddChuKy(data, TypeChuKy.RPT_TL_GIAY_GIOI_THIEU_TAI_CHINH);

                        string templateFileName = "";
                        if (SizeSelected != null && SizeSelected.ValueItem.Equals("A5"))
                        {
                            data.Add("PcDacThuSum", rptGiayGtTaiChinhModel.fPcDacThuSum == 0 ? null : rptGiayGtTaiChinhModel.fPcDacThuSum);
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_GIAY_GT_TC);
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
                            FormatNumber formatNumber = new FormatNumber(1, ExportType.PDF);
                            data.Add("FormatNumber", formatNumber);

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
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_GIAY_GT_TC_A4_KHONG_PC_HUONG);
                            }
                            else
                            {
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_GIAY_GT_TC_A4);
                            }
                        }

                        string fileNamePrefix = string.Format("Giay_GioiThieu_TaiChinh_CanBo_{0}_Thang_{1}_Nam_{2}",
                            rptGiayGtTaiChinhModel.sTenCanBo, MonthSelected.ValueItem, SelectedYear.ValueItem);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<RptGiayGtTaiChinhModel>(templateFileName, data);
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
                            } else {
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
                string idTypeBc = TypeChuKy.RPT_TL_GIAY_GIOI_THIEU_TAI_CHINH;
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
            } else
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
