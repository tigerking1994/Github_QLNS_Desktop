using AutoMapper;
using ControlzEx.Standard;
using log4net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Report
{
    public class InsuranceSalaryReportViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ICollectionView _donViCollectionView;
        private readonly IMapper _mapper;
        private readonly ITlDmDonViService _dmDonViService;
        private readonly IExportService _exportService;
        private readonly ITlBangLuongThangBHXHService _bangLuongService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsDonViService _donViService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private readonly IDanhMucService _danhMucService;
        private string _diaDiem;

        private string _typeChuky;
        private bool _isInTheoTongHop;
        public static InsuranceSalaryPrintType InsuranceSalaryPrintType { get; set; }
        private string _txtTitleFirst;
        public string name { get; set; }
        public int ReportNameTypeValue;
        public int IndexMonth { get; set; }
        public int IndexYear { get; set; }
        private string ReportName
        {
            get
            {
                switch (ReportNameTypeValue)
                {
                    case (int)InsuranceSalaryPrintType.TRO_CAP_OM_DAU:
                        name = "Bảng thanh toán trợ cấp ốm đau";
                        break;
                    case (int)InsuranceSalaryPrintType.TRO_CAP_THAI_SAN:
                        name = "Bảng thanh toán trợ cấp thai sản";
                        break;
                    case (int)InsuranceSalaryPrintType.TRO_CAP_TNLD:
                        name = "Bảng thanh toán trợ cấp tai nạn lao động";
                        break;
                    case (int)InsuranceSalaryPrintType.TRO_CAP_HUU_TRI:
                        name = "Bảng thanh toán trợ cấp hưu trí";
                        break;
                    case (int)InsuranceSalaryPrintType.TRO_CAP_XUAT_NGU:
                        name = "Bảng thanh toán trợ cấp xuất ngũ";
                        break;
                }
                return name;
            }
        }
        public bool IsShowSummaryType => IsSummary;
        public bool IsShowSummary { get; set; }
        public bool IsShowThucNhan => InsuranceSalaryPrintType.TRO_CAP_OM_DAU.Equals(InsuranceSalaryPrintType);
        private bool _isSummary;
        public bool IsSummary
        {
            get => _isSummary;
            set
            {
                SetProperty(ref _isSummary, value);
                OnPropertyChanged(nameof(IsShowSummaryType));
            }
        }
        private bool _isThucNhan;
        public bool IsThucNhan
        {
            get => _isThucNhan;
            set
            {
                SetProperty(ref _isThucNhan, value);
            }
        }
        private bool _isGiaiThich;
        public bool IsGiaiThich
        {
            get => _isGiaiThich;
            set
            {
                SetProperty(ref _isGiaiThich, value);
            }
        }
        public override string Name => ReportName;
        public override string Title => ReportName;
        public override string Description => ReportName;
        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set
            {
                SetProperty(ref _txtTitleFirst, value);
            }
        }
        private string _tenDVBanHanh1;
        public string TenDVBanHanh1
        {
            get => _tenDVBanHanh1;
            set
            {
                SetProperty(ref _tenDVBanHanh1, value);
            }
        }
        public bool IsQuanLyDonViCha { get; set; }
        private string _txtTitleSecond;
        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }
        private string _txtTitleThird;

        public string TxtTitleThird
        {
            get => _txtTitleThird;
            set
            {
                SetProperty(ref _txtTitleThird, value);
            }
        }

        private ObservableCollection<ComboboxItem> _catUnitTypes = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> CatUnitTypes
        {
            get => _catUnitTypes;
            set => SetProperty(ref _catUnitTypes, value);
        }

        private ComboboxItem _catUnitTypeSelected;

        public ComboboxItem CatUnitTypeSelected
        {
            get => _catUnitTypeSelected;
            set => SetProperty(ref _catUnitTypeSelected, value);
        }
        private ObservableCollection<ComboboxItem> _paperPrintTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> PaperPrintTypes
        {
            get => _paperPrintTypes;
            set => SetProperty(ref _paperPrintTypes, value);
        }
        private ObservableCollection<CheckBoxItem> _listDonVi = new ObservableCollection<CheckBoxItem>();
        public ObservableCollection<CheckBoxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }
        public string MaDonViChungTu { get; set; }
        public string DonViChaChungTu { get; set; }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value))
                {
                    _donViCollectionView.Refresh();
                }
            }
        }
        public string LabelSelectedCountDonVi
        {
            get => $"ĐƠN VỊ ({ListDonVi.Count(item => item.IsChecked)}/{ListDonVi.Count})";
        }
        private bool _selectAllDonVi;

        public bool SelectAllDonVi
        {
            get => ListDonVi.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDonVi, value);
                foreach (var item in ListDonVi) item.IsChecked = _selectAllDonVi;
            }
        }
        private ComboboxItem _paperPrintTypeSelected;
        public ComboboxItem PaperPrintTypeSelected
        {
            get => _paperPrintTypeSelected;
            set
            {
                SetProperty(ref _paperPrintTypeSelected, value);
                LoadTitleFirst();
                LoadDonVi();
            }
        }

        private ComboboxItem _khoiSelected;
        public ComboboxItem KhoiSelected
        {
            get => _khoiSelected;
            set
            {
                SetProperty(ref _khoiSelected, value);
                LoadDonVi();
            }
        }
        public bool IsInTheoTongHop
        {
            get => _isInTheoTongHop;
            set
            {
                SetProperty(ref _isInTheoTongHop, value);
                LoadDonVi();
            }
        }

        private List<ComboboxItem> _itemsMonth;
        public List<ComboboxItem> ItemsMonth
        {
            get => _itemsMonth;
            set => SetProperty(ref _itemsMonth, value);
        }

        private ComboboxItem _selectedMonth;
        public ComboboxItem SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                SetProperty(ref _selectedMonth, value);
                LoadDonVi();
            }
        }

        private List<ComboboxItem> _itemsYear;
        public List<ComboboxItem> ItemsYear
        {
            get => _itemsYear;
            set => SetProperty(ref _itemsYear, value);
        }

        private ComboboxItem _selectedYear;
        public ComboboxItem SelectedYear
        {
            get => _selectedYear;
            set
            {
                SetProperty(ref _selectedYear, value);
                LoadDonVi();
            }
        }


        private List<ComboboxItem> _itemsSummaryType;
        public List<ComboboxItem> ItemsSummaryType
        {
            get => _itemsSummaryType;
            set => SetProperty(ref _itemsSummaryType, value);
        }

        private ComboboxItem _selectedSummaryType;
        public ComboboxItem SelectedSummaryType
        {
            get => _selectedSummaryType;
            set
            {
                SetProperty(ref _selectedSummaryType, value);
            }
        }

        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public InsuranceSalaryReportViewModel(ITlDmDonViService dmDonViService,
            IExportService exportService,
            ITlBangLuongThangBHXHService iTlBangLuongThangBHXHService,
            IDmChuKyService dmChuKyService,
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IDanhMucService danhMucService,
            INsDonViService donViService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _dmDonViService = dmDonViService;
            _exportService = exportService;
            _bangLuongService = iTlBangLuongThangBHXHService;
            _dmChuKyService = dmChuKyService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _danhMucService = danhMucService;
            _donViService = donViService;

            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            ExportPdfActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
        }
        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            _isInTheoTongHop = false;
            _isThucNhan = false;
            _isSummary = false;
            IsGiaiThich = false;
            InitReportDefaultDate();
            Clear();
            LoadMonths();
            LoadYears();
            LoadDonVi();
            LoadCatUnitTypes();
            LoadTypeChuKy();
            LoadTitleFirst();
            LoadPaperPrintTypes();
            LoadKieuGiayIn();
            LoadDiaDiem();
            IsContainBVTCChecked = true;
            LoadSummaryPrintType();
        }

        private ObservableCollection<ComboboxItem> _itemsKieuGiayIn = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ItemsKieuGiayIn
        {
            get => _itemsKieuGiayIn;
            set => SetProperty(ref _itemsKieuGiayIn, value);
        }
        private ComboboxItem _selectedKieuGiayIn;

        public ComboboxItem SelectedKieuGiayIn
        {
            get => _selectedKieuGiayIn;
            set => SetProperty(ref _selectedKieuGiayIn, value);
        }
        private bool _isContainBVTCChecked = true;

        public bool IsContainBVTCChecked
        {
            get => _isContainBVTCChecked;
            set
            {
                SetProperty(ref _isContainBVTCChecked, value);
                LoadDonVi();
            }
        }

        private void LoadSummaryPrintType()
        {
            _itemsSummaryType = new List<ComboboxItem>();
            _itemsSummaryType.Add(new ComboboxItem(LoaiTongHopBhxh.GetNameLoai(LoaiTongHopBhxh.CTTL_DON_VI), LoaiTongHopBhxh.CTTL_DON_VI.ToString()));
            if (!(ReportNameTypeValue.Equals((int)InsuranceSalaryPrintType.TRO_CAP_XUAT_NGU)))
            {
                _itemsSummaryType.Add(new ComboboxItem(LoaiTongHopBhxh.GetNameLoai(LoaiTongHopBhxh.CTTL_DOI_TUONG), LoaiTongHopBhxh.CTTL_DOI_TUONG.ToString()));
            }
            _selectedSummaryType = _itemsSummaryType.FirstOrDefault();
        }

        private void LoadMonths()
        {
            _itemsMonth = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _itemsMonth.Add(month);
            }
            _selectedMonth = _itemsMonth.FirstOrDefault(x => x.ValueItem == IndexMonth.ToString());
            OnPropertyChanged(nameof(SelectedMonth));
            OnPropertyChanged(nameof(ItemsMonth));
        }

        private void LoadYears()
        {
            _itemsYear = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                var year = new ComboboxItem(i.ToString(), i.ToString());
                _itemsYear.Add(year);
            }
            OnPropertyChanged(nameof(ItemsYear));
            _selectedYear = _itemsYear.FirstOrDefault(x => x.ValueItem == IndexYear.ToString());
            OnPropertyChanged(nameof(SelectedYear));
        }

        public void LoadDiaDiem()
        {
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }
        public void LoadCatUnitTypes()
        {
            _catUnitTypes = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH));
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            List<DanhMuc> data = _danhMucService.FindByCondition(predicate).OrderBy(x => x.SGiaTri).ToList();
            _catUnitTypes = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            if (data.Count == 0)
            {
                _catUnitTypes.Insert(0, new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            _catUnitTypeSelected = _catUnitTypes.FirstOrDefault();
        }
        public void LoadDonVi()
        {
            ListDonVi = LoadDanhSachDonVi();
            _donViCollectionView = CollectionViewSource.GetDefaultView(ListDonVi);
            _donViCollectionView.Filter = obj => string.IsNullOrWhiteSpace(_searchDonVi)
                                                 || (obj is CheckBoxItem item &&
                                                     item.DisplayItem.Contains(_searchDonVi));

            foreach (var org in ListDonVi)
            {
                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountDonVi));
                    OnPropertyChanged(nameof(SelectAllDonVi));
                };
            }
        }

        private void LoadTypeChuKy()
        {
            switch (InsuranceSalaryPrintType)
            {
                case InsuranceSalaryPrintType.TRO_CAP_OM_DAU:
                    _typeChuky = TypeChuKy.RPT_LUONG_BHXH_TRO_CAP_OM_DAU;
                    break;
                case InsuranceSalaryPrintType.TRO_CAP_THAI_SAN:
                    _typeChuky = TypeChuKy.RPT_LUONG_BHXH_TRO_CAP_THAI_SAN;
                    break;
                case InsuranceSalaryPrintType.TRO_CAP_TNLD:
                    _typeChuky = TypeChuKy.RPT_LUONG_BHXH_TRO_CAP_TNLD;
                    break;
                case InsuranceSalaryPrintType.TRO_CAP_HUU_TRI:
                    _typeChuky = TypeChuKy.RPT_LUONG_BHXH_TRO_CAP_HUU_TRI;
                    break;
                case InsuranceSalaryPrintType.TRO_CAP_XUAT_NGU:
                    _typeChuky = TypeChuKy.RPT_LUONG_BHXH_TRO_CAP_XUAT_NGU;
                    break;
            }
        }
        public void LoadTitleFirst()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            switch (InsuranceSalaryPrintType)
            {
                case InsuranceSalaryPrintType.TRO_CAP_OM_DAU:
                    _txtTitleFirst = (_dmChuKy != null && _dmChuKy.TieuDe1MoTa != null) ? _dmChuKy.TieuDe1MoTa : DefaultReportTitle.TROCAPOMDAU;
                    break;
                case InsuranceSalaryPrintType.TRO_CAP_THAI_SAN:
                    _txtTitleFirst = (_dmChuKy != null && _dmChuKy.TieuDe1MoTa != null) ? _dmChuKy.TieuDe1MoTa : DefaultReportTitle.TROCAPTHAISAN;
                    break;
                case InsuranceSalaryPrintType.TRO_CAP_TNLD:
                    _txtTitleFirst = (_dmChuKy != null && _dmChuKy.TieuDe1MoTa != null) ? _dmChuKy.TieuDe1MoTa : DefaultReportTitle.TROCAPTNLD;
                    break;
                case InsuranceSalaryPrintType.TRO_CAP_HUU_TRI:
                    _txtTitleFirst = (_dmChuKy != null && _dmChuKy.TieuDe1MoTa != null) ? _dmChuKy.TieuDe1MoTa : DefaultReportTitle.TROCAPHUUTRI;
                    break;
                case InsuranceSalaryPrintType.TRO_CAP_XUAT_NGU:
                    _txtTitleFirst = (_dmChuKy != null && _dmChuKy.TieuDe1MoTa != null) ? _dmChuKy.TieuDe1MoTa : DefaultReportTitle.TROCAPXUATNGU;
                    break;
            }
            TxtTitleSecond = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
            TxtTitleThird = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
        }
        public void LoadPaperPrintTypes()
        {
            var paperPrintTypes = new List<ComboboxItem>();
            paperPrintTypes = new List<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Phụ lục", ValueItem = "2"},
                    new ComboboxItem {DisplayItem = "Biểu trình ký", ValueItem = "1"}
                };
            PaperPrintTypes = new ObservableCollection<ComboboxItem>(paperPrintTypes);
            _paperPrintTypeSelected = paperPrintTypes.ElementAt(0);
        }
        public virtual void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(0);
        }
        public void Clear()
        {
            _donViCollectionView = null;
        }

        private ObservableCollection<CheckBoxItem> LoadDanhSachDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var lstIdDonVi = GetListIdDonVi();
            if (lstIdDonVi != null && lstIdDonVi.Count > 0)
            {
                var predicateDv = PredicateBuilder.True<TlDmDonVi>();
                predicateDv = predicateDv.And(x => lstIdDonVi.Contains(x.MaDonVi));
                var lstDonVi = _dmDonViService.FindByCondition(predicateDv);
                var result = lstDonVi.Select(item => new CheckBoxItem
                {
                    ValueItem = item.MaDonVi,
                    DisplayItem = string.Join("-", item.MaDonVi, item.TenDonVi),
                    NameItem = item.TenDonVi
                }).OrderBy(item => item.ValueItem);
                return new ObservableCollection<CheckBoxItem>(result);
            }

            return new ObservableCollection<CheckBoxItem>();
        }
        private List<string> GetListIdDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            List<string> lstIdDonVi = new List<string>();
            if (SelectedMonth != null && SelectedYear != null)
            {
                var lstBangLuongBHXH = _bangLuongService.GetBangLuongTheoPhanHo(int.Parse(SelectedYear.ValueItem), int.Parse(SelectedMonth.ValueItem)).ToList();
                lstIdDonVi = lstBangLuongBHXH.Select(x => x.MaDonVi).Distinct().ToList();
            }
            return lstIdDonVi;
        }
        public void OnExport(ExportType exportType)
        {
            List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
            var lstSelectedUnit = string.Join(",", lstDonViSelected.Select(x => x.ValueItem.ToString()).ToList());
            var selectedYear = int.Parse(SelectedYear.ValueItem);
            var selectedMonth = int.Parse(SelectedMonth.ValueItem);

            if (ListDonVi.Where(item => item.IsChecked).Count() <= 0)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }
            else
            {
                if (InsuranceSalaryPrintType.TRO_CAP_OM_DAU.Equals(InsuranceSalaryPrintType))
                {
                    PrintBangThanhToanTroCapOmDau(exportType, selectedYear, selectedMonth);
                }
                if (InsuranceSalaryPrintType.TRO_CAP_THAI_SAN.Equals(InsuranceSalaryPrintType))
                {
                    PrintBangThanhToanTroCapThaiSan(exportType, selectedYear, selectedMonth);
                }
                if (InsuranceSalaryPrintType.TRO_CAP_TNLD.Equals(InsuranceSalaryPrintType))
                {
                    PrintBangThanhToanTroCapTNLD(exportType, selectedYear, selectedMonth);
                }
                if (InsuranceSalaryPrintType.TRO_CAP_HUU_TRI.Equals(InsuranceSalaryPrintType))
                {
                    PrintBangThanhToanTroCapHuuTriPhucVienThoiViecTuTuat(exportType, selectedYear, selectedMonth);
                }
                if (InsuranceSalaryPrintType.TRO_CAP_XUAT_NGU.Equals(InsuranceSalaryPrintType))
                {
                    PrintBangThanhToanTroCapXuatNgu(exportType, selectedYear, selectedMonth);
                }
            }
        }

        public void PrintBangThanhToanTroCapOmDau(ExportType exportType, int selectedYear, int selectedMonth)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    string templateFileName;
                    string fileNamePrefix;
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    string lstDonVi = string.Join(StringUtils.COMMA, lstDonViSelected.Select(x => x.ValueItem));
                    var exportData = _bangLuongService.ExportBangThanhToanTroCapOmDau(lstDonVi, selectedYear, selectedMonth, donViTinh).Where(x => x.IsHasData).ToList();
                    if (IsSummary && SelectedSummaryType != null)
                    {
                        exportData = CalculateDataExportByAgency(exportData, lstDonViSelected);
                        if (IsThucNhan)
                            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_OM_DAU_DON_VI_IN_SO_THUC_NHAN));
                        else
                            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_OM_DAU_DON_VI));
                    }
                    else
                    {
                        exportData = CalculateDataExportByAgency(exportData, lstDonViSelected);
                        if (IsThucNhan)
                            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_OM_DAU_IN_SO_THUC_NHAN));
                        else
                            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_OM_DAU));
                    }
                    //Update STT dòng cha
                    var parentRow = exportData.Where(x => x.IsHangCha == true);
                    foreach (var parent in parentRow)
                    {
                        parent.STT = GetNewReportIndex(parent.STT);
                    }

                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", exportData);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("YearOfWork", "năm " + selectedYear);
                    data.Add("Month", "Tháng " + selectedMonth);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("DonVi", GetHeader2Report());
                    data.Add("TotalLuongCanCu", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FLuongCanCu.GetValueOrDefault()));
                    data.Add("TotalSoNgayBenhDaiNgayD14Ngay", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.SoNgayBenhDaiNgayD14Ngay.GetValueOrDefault()));
                    data.Add("TotalFBenhDaiNgayD14Ngay", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FBenhDaiNgayD14Ngay.GetValueOrDefault()));
                    data.Add("TotalSoNgayBenhDaiNgayT14Ngay", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.SoNgayBenhDaiNgayT14Ngay.GetValueOrDefault()));
                    data.Add("TotalFBenhDauNgayT14Ngay", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FBenhDauNgayT14Ngay.GetValueOrDefault()));
                    data.Add("TotalSoNgayOmKhacD14Ngay", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.SoNgayOmKhacD14Ngay.GetValueOrDefault()));
                    data.Add("TotalFOmKhacD14Ngay", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FOmKhacD14Ngay.GetValueOrDefault()));
                    data.Add("TotalSoNgayOmKhacT14Ngay", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.SoNgayOmKhacT14Ngay.GetValueOrDefault()));
                    data.Add("TotalFOmKhacT14Ngay", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FOmKhacT14Ngay.GetValueOrDefault()));
                    data.Add("TotalSoNgayConOm", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.SoNgayConOm.GetValueOrDefault()));
                    data.Add("TotalFConOm", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FConOm.GetValueOrDefault()));
                    data.Add("TotalSoNgayDuongSuc", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.SoNgayDuongSuc.GetValueOrDefault()));
                    data.Add("TotalFDuongSucPHSK", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FDuongSucPHSK.GetValueOrDefault()));
                    data.Add("TotalFTongSoTien", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FTongSoTien.GetValueOrDefault()));
                    data.Add("TotalTruBHXH", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FSoPhaiTruBHXH.GetValueOrDefault()));
                    data.Add("TotalTruBHYT", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FSoPhaiTruBHYT.GetValueOrDefault()));
                    data.Add("TotalPhaiTru", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FTongPhaiTru.GetValueOrDefault()));
                    data.Add("TotalDuocNhan", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FDuocNhan.GetValueOrDefault()));
                    AddChuKy(data, _typeChuky);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<TlBangLuongThangBHXHQuery>(templateFileName, data);
                    if (IsSummary && SelectedSummaryType != null && SelectedSummaryType.ValueItem.Equals(LoaiTongHopBhxh.CTTL_DON_VI.ToString()))
                    {
                        int firstColdv = 1;
                        int lastColdv = 2;
                        int firstRowdv = 11;
                        foreach (var item in exportData.Where(x => x.IsAgency))
                        {
                            var index = exportData.IndexOf(item);
                            xlsFile.MergeCells(firstRowdv + index, firstColdv, firstRowdv + index, lastColdv);

                        }
                    }
                    results.Add(new ExportResult("Bảng thanh toán trợ cấp ốm đau " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    //In giai thich
                    if (IsGiaiThich)
                    {
                        int typePrint = LoaiTongHopBhxh.CTTL_DON_VI;
                        List<TlBangLuongThangBHXHReportQuery> dataExportGiaiThich = new List<TlBangLuongThangBHXHReportQuery>();
                        string templateFileNameGt = string.Empty;
                        string fileNamePrefixGt = string.Empty;
                        if (!exportData.IsEmpty())
                        {
                            if (SelectedSummaryType != null && SelectedSummaryType.ValueItem.Equals(LoaiTongHopBhxh.CTTL_DON_VI.ToString()) && IsSummary)
                            {
                                templateFileNameGt = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_OM_DAU_GIAI_THICH_DON_VI));

                            }
                            else if (SelectedSummaryType != null && SelectedSummaryType.ValueItem.Equals(LoaiTongHopBhxh.CTTL_DOI_TUONG.ToString()) && IsSummary)
                            {
                                typePrint = LoaiTongHopBhxh.CTTL_DOI_TUONG;
                                templateFileNameGt = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_OM_DAU_GIAI_THICH_DON_VI));

                            }
                            else
                            {
                                typePrint = LoaiTongHopBhxh.CTTL_DOI_TUONG;
                                templateFileNameGt = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_OM_DAU_GIAI_THICH));

                            }
                            fileNamePrefixGt = Path.GetFileNameWithoutExtension(templateFileName);

                            dataExportGiaiThich = _bangLuongService.ExportBangThanhToanTroCapOmDauGiaiThich(string.Join(StringUtils.COMMA, exportData.Where(x => !string.IsNullOrEmpty(x.MaCbo) && !x.IsHangCha.GetValueOrDefault()).Select(x => x.MaCbo).Distinct()), selectedYear, selectedMonth, donViTinh, typePrint, lstDonVi).ToList();
                        }

                        if (!dataExportGiaiThich.IsEmpty())
                        {
                            var listNganh = new List<string>() { NgachLuongBhxh.SQ, NgachLuongBhxh.QNCN, NgachLuongBhxh.HSQ_BS, NgachLuongBhxh.VCQP, NgachLuongBhxh.LDHD };

                            dataExportGiaiThich.Where(x => listNganh.Contains(x.LoaiDoiTuong) && x.Level.Equals(1)).Select(x =>
                            {
                                var ItemsGiaiThich = new List<TlBangLuongThangBHXHReportQuery>();
                                if (typePrint.Equals(LoaiTongHopBhxh.CTTL_DON_VI))
                                {
                                    ItemsGiaiThich = dataExportGiaiThich.Where(y => y.Level == 2 && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong) && !string.IsNullOrEmpty(x.MaDonVi) && x.MaDonVi.Equals(y.MaDonVi)).ToList();
                                }
                                else
                                {
                                    ItemsGiaiThich = dataExportGiaiThich.Where(y => y.Level == 2 && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).ToList();

                                }
                                if (!ItemsGiaiThich.IsEmpty())
                                {
                                    x.SoCanBo = ItemsGiaiThich.Count();
                                    foreach (var item in ItemsGiaiThich.Select((value, index) => new { index, value }))
                                    {
                                        item.value.STT = (item.index + 1).ToString();
                                    }
                                }
                                return x;
                            }).ToList();
                            dataExportGiaiThich.Where(x => x.Level.Equals(0)).Select(x => x.SoCanBo = dataExportGiaiThich.Where(y => y.Level.Equals(2) && y.MaDonVi.Equals(x.MaDonVi)).Count()).ToList();
                            dataExportGiaiThich.Where(x => x.Level.Equals(3)).Select(x => x.STT = string.Empty).ToList();
                        }
                        //Update STT dòng cha
                        var parentGTRow = dataExportGiaiThich.Where(x => x.Level == 1);
                        foreach (var parent in parentGTRow)
                        {
                            parent.STT = GetNewReportIndex(parent.STT);
                            if (parent.TenChiTieu == "SQ")
                                parent.TenChiTieu = "Sĩ quan";
                            if (parent.TenChiTieu == "VCQP")
                                parent.TenChiTieu = "CNVCQP";
                            if (parent.TenChiTieu == "LDHD")
                                parent.TenChiTieu = "LĐHĐ";
                        }
                        Dictionary<string, object> dataGiaiThich = new Dictionary<string, object>();
                        dataGiaiThich.Add("FormatNumber", formatNumber);
                        dataGiaiThich.Add("ListData", dataExportGiaiThich);
                        dataGiaiThich.Add("Cap1", _sessionInfo.TenDonVi);
                        dataGiaiThich.Add("TieuDe1", "BẢNG GIẢI THÍCH THANH TOÁN TRỢ CẤP ỐM ĐAU");
                        dataGiaiThich.Add("TieuDe2", TxtTitleSecond);
                        dataGiaiThich.Add("TieuDe3", TxtTitleThird);
                        dataGiaiThich.Add("YearOfWork", "năm " + selectedYear);
                        dataGiaiThich.Add("Month", "Tháng " + selectedMonth);
                        dataGiaiThich.Add("h1", h1);
                        dataGiaiThich.Add("h2", "");
                        dataGiaiThich.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                        dataGiaiThich.Add("DiaDiem", _diaDiem);
                        dataGiaiThich.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        dataGiaiThich.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                        dataGiaiThich.Add("DonVi", GetHeader2Report());
                        dataGiaiThich.Add("TotalLBH_TT", dataExportGiaiThich.Where(w => w.Level.Equals(3)).Sum(x => x.LBH_TT.GetValueOrDefault()));
                        dataGiaiThich.Add("TotalHSBLBH_TT", dataExportGiaiThich.Where(w => w.Level.Equals(3)).Sum(x => x.HSBLBH_TT.GetValueOrDefault()));
                        dataGiaiThich.Add("TotalPCCVBH_TT", dataExportGiaiThich.Where(w => w.Level.Equals(3)).Sum(x => x.PCCVBH_TT.GetValueOrDefault()));
                        dataGiaiThich.Add("TotalPCTNBH_TT", dataExportGiaiThich.Where(w => w.Level.Equals(3)).Sum(x => x.PCTNBH_TT.GetValueOrDefault()));
                        dataGiaiThich.Add("TotalPCTNVKBH_TT", dataExportGiaiThich.Where(w => w.Level.Equals(3)).Sum(x => x.PCTNVKBH_TT.GetValueOrDefault()));
                        dataGiaiThich.Add("Total", dataExportGiaiThich.Where(w => w.Level.Equals(3)).Sum(x => x.Total.GetValueOrDefault()));
                        string filenameGt = StringUtils.CreateExportFileName(fileNamePrefixGt + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                        var xlsFileGt = _exportService.Export<TlBangLuongThangBHXHReportQuery>(templateFileNameGt, dataGiaiThich);
                        if (typePrint.Equals(LoaiTongHopBhxh.CTTL_DON_VI))
                        {
                            int firstCol = 1;
                            int lastCol = 2;
                            int firstRow = 7;
                            foreach (var item in dataExportGiaiThich.Where(x => x.IsAgency))
                            {
                                var index = dataExportGiaiThich.IndexOf(item);
                                xlsFileGt.MergeCells(firstRow + index, firstCol, firstRow + index, lastCol);
                            }
                        }
                        results.Add(new ExportResult("Bảng giải thích thanh toán trợ cấp ốm đau " + _sessionInfo.YearOfWork, filenameGt, null, xlsFileGt));
                    }
                    e.Result = results;

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

        public void PrintBangThanhToanTroCapThaiSan(ExportType exportType, int selectedYear, int selectedMonth)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    string templateFileName;
                    string fileNamePrefix;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    string lstDonVi = string.Join(StringUtils.COMMA, lstDonViSelected.Select(x => x.ValueItem));
                    var exportData = _bangLuongService.ExportBangThanhToanTroCapThaiSan(lstDonVi, selectedYear, selectedMonth, donViTinh).Where(x => x.IsHasData).ToList();
                    if (IsSummary && SelectedSummaryType != null)
                    {
                        exportData = CalculateDataExportByAgency(exportData, lstDonViSelected);
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_THAI_SAN_DON_VI));
                    }
                    else
                    {
                        exportData = CalculateDataExportByAgency(exportData, lstDonViSelected);
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_THAI_SAN));
                    }
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork)
                    .Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", exportData);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("YearOfWork", "năm " + selectedYear);
                    data.Add("Month", "Tháng " + selectedMonth);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("DonVi", GetHeader2Report());
                    data.Add("TotalLuongCanCu", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FLuongCanCu.GetValueOrDefault()));
                    data.Add("TotalSoNgaySinhConNuoiCon", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.SoNgaySinhConNuoiCon.GetValueOrDefault()));
                    data.Add("TotalSoNgayTroCap1Lan", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.SoNgayTroCap1Lan.GetValueOrDefault()));
                    data.Add("TotalSoNgayKhamThai", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.SoNgayKhamThai.GetValueOrDefault()));
                    data.Add("TotalSoNgayDuongSucPHSKThaiSan", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.SoNgayDuongSucPHSKThaiSan.GetValueOrDefault()));
                    data.Add("TotalFSinhConNuoiCon", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FSinhConNuoiCon.GetValueOrDefault()));
                    data.Add("TotalFTroCap1Lan", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCap1Lan.GetValueOrDefault()));
                    data.Add("TotalFKhamThai", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FKhamThai.GetValueOrDefault()));
                    data.Add("TotalFDuongSucPHSKThaiSan", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FDuongSucPHSKThaiSan.GetValueOrDefault()));
                    data.Add("TotalFTongSoTien", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FTongSoTien.GetValueOrDefault()));
                    AddChuKy(data, _typeChuky);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<TlBangLuongThangBHXHQuery>(templateFileName, data);
                    if (IsSummary && SelectedSummaryType != null && SelectedSummaryType.ValueItem.Equals(LoaiTongHopBhxh.CTTL_DON_VI.ToString()))
                    {
                        int firstCol = 1;
                        int lastCol = 2;
                        int firstRow = 11;
                        foreach (var item in exportData.Where(x => x.IsAgency))
                        {
                            var index = exportData.IndexOf(item);
                            xlsFile.MergeCells(firstRow + index, firstCol, firstRow + index, lastCol);

                        }
                    }

                    results.Add(new ExportResult("Bảng thanh toán trợ cấp thai sản " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    e.Result = results;

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

        private List<TlBangLuongThangBHXHQuery> CalculateDataExportByAgency(List<TlBangLuongThangBHXHQuery> dataInput, List<CheckBoxItem> agencies)
        {
            List<TlBangLuongThangBHXHQuery> results = new List<TlBangLuongThangBHXHQuery>();
            if (SelectedSummaryType != null && SelectedSummaryType.ValueItem.Equals(LoaiTongHopBhxh.CTTL_DON_VI.ToString()) && !dataInput.IsEmpty() && IsSummary)
            {
                foreach (var itemAgency in agencies)
                {
                    var AgencyData = dataInput.Where(x => !x.IsHangCha.GetValueOrDefault() && x.MaDonVi.Equals(itemAgency.ValueItem)).ToList();
                    TlBangLuongThangBHXHQuery itemAgencyData = new TlBangLuongThangBHXHQuery
                    {
                        MaDonVi = itemAgency.ValueItem,
                        IsHangCha = true,
                        IsHasData = true,
                        IsAgency = true,
                        TenCbo = itemAgency.NameItem
                    };
                    //itemDataAgency.FLuongCanCu
                    if (!AgencyData.IsEmpty())
                    {
                        itemAgencyData.SoCanBo = AgencyData.Count();
                        itemAgencyData.FLuongCanCu = AgencyData.Sum(x => x.FLuongCanCu.GetValueOrDefault());
                        itemAgencyData.FSinhConNuoiCon = AgencyData.Sum(x => x.FSinhConNuoiCon.GetValueOrDefault());
                        itemAgencyData.FTroCap1Lan = AgencyData.Sum(x => x.FTroCap1Lan.GetValueOrDefault());
                        itemAgencyData.FKhamThai = AgencyData.Sum(x => x.FKhamThai.GetValueOrDefault());
                        itemAgencyData.FDuongSucPHSKThaiSan = AgencyData.Sum(x => x.FDuongSucPHSKThaiSan.GetValueOrDefault());
                        itemAgencyData.FTongSoTien = AgencyData.Sum(x => x.FTongSoTien.GetValueOrDefault());
                        itemAgencyData.FTroCapKhuVuc = AgencyData.Sum(x => x.FTroCapKhuVuc.GetValueOrDefault());
                        itemAgencyData.FTroCapMaiTang = AgencyData.Sum(x => x.FTroCapMaiTang.GetValueOrDefault());
                        itemAgencyData.FChiGiamDinh = AgencyData.Sum(x => x.FChiGiamDinh.GetValueOrDefault());
                        itemAgencyData.FTroCapTheoPhieuTruyTra = AgencyData.Sum(x => x.FTroCapTheoPhieuTruyTra.GetValueOrDefault());
                        itemAgencyData.FTroCapHangThang = AgencyData.Sum(x => x.FTroCapHangThang.GetValueOrDefault());
                        itemAgencyData.FTroCapPHCN = AgencyData.Sum(x => x.FTroCapPHCN.GetValueOrDefault());
                        itemAgencyData.FTroCapChetDoTNLD = AgencyData.Sum(x => x.FTroCapChetDoTNLD.GetValueOrDefault());
                        itemAgencyData.FDuongSucTNLD = AgencyData.Sum(x => x.FDuongSucTNLD.GetValueOrDefault());
                        itemAgencyData.FConOm = AgencyData.Sum(x => x.FConOm.GetValueOrDefault());
                        itemAgencyData.FBenhDaiNgayD14Ngay = AgencyData.Sum(x => x.FBenhDaiNgayD14Ngay.GetValueOrDefault());
                        itemAgencyData.FBenhDauNgayT14Ngay = AgencyData.Sum(x => x.FBenhDauNgayT14Ngay.GetValueOrDefault());
                        itemAgencyData.FOmKhacD14Ngay = AgencyData.Sum(x => x.FOmKhacD14Ngay.GetValueOrDefault());
                        itemAgencyData.FOmKhacT14Ngay = AgencyData.Sum(x => x.FOmKhacT14Ngay.GetValueOrDefault());
                        itemAgencyData.FDuongSucPHSK = AgencyData.Sum(x => x.FDuongSucPHSK.GetValueOrDefault());
                        itemAgencyData.FSoPhaiTruBHXH = AgencyData.Sum(x => x.FSoPhaiTruBHXH.GetValueOrDefault());
                        itemAgencyData.FDuocNhan = AgencyData.Sum(x => x.FDuocNhan.GetValueOrDefault());
                        itemAgencyData.FTongSoTienThangNay = AgencyData.Sum(x => x.FTongSoTienThangNay.GetValueOrDefault());
                        itemAgencyData.FTongSoTienTruyLinh = AgencyData.Sum(x => x.FTongSoTienTruyLinh.GetValueOrDefault());
                        itemAgencyData.FTroCapKhuVucTruyLinh = AgencyData.Sum(x => x.FTroCapKhuVucTruyLinh.GetValueOrDefault());
                        itemAgencyData.FTroCapMaiTangTruyLinh = AgencyData.Sum(x => x.FTroCapMaiTangTruyLinh.GetValueOrDefault());
                        itemAgencyData.FTroCap1LanTruyLinh = AgencyData.Sum(x => x.FTroCap1LanTruyLinh.GetValueOrDefault());
                        itemAgencyData.FChiGiamDinhTruyLinh = AgencyData.Sum(x => x.FChiGiamDinhTruyLinh.GetValueOrDefault());
                        itemAgencyData.FTroCapHangThangTruyLinh = AgencyData.Sum(x => x.FTroCapHangThangTruyLinh.GetValueOrDefault());
                        itemAgencyData.FTroCapPHCNTruyLinh = AgencyData.Sum(x => x.FTroCapPHCNTruyLinh.GetValueOrDefault());
                        itemAgencyData.FTroCapChetDoTNLDTruyLinh = AgencyData.Sum(x => x.FTroCapChetDoTNLDTruyLinh.GetValueOrDefault());
                        itemAgencyData.FDuongSucTNLDTruyLinh = AgencyData.Sum(x => x.FDuongSucTNLDTruyLinh.GetValueOrDefault());
                        itemAgencyData.SoNgayDuongSucTNLD = AgencyData.Sum(x => x.SoNgayDuongSucTNLD.GetValueOrDefault());
                        itemAgencyData.SoNgayDuongSucTNLDTruyLinh = AgencyData.Sum(x => x.SoNgayDuongSucTNLDTruyLinh.GetValueOrDefault());
                        itemAgencyData.SoNgayBenhDaiNgayD14Ngay = AgencyData.Sum(x => x.SoNgayBenhDaiNgayD14Ngay.GetValueOrDefault());
                        itemAgencyData.SoNgayBenhDaiNgayT14Ngay = AgencyData.Sum(x => x.SoNgayBenhDaiNgayT14Ngay.GetValueOrDefault());
                        itemAgencyData.SoNgayOmKhacD14Ngay = AgencyData.Sum(x => x.SoNgayOmKhacD14Ngay.GetValueOrDefault());
                        itemAgencyData.SoNgayOmKhacT14Ngay = AgencyData.Sum(x => x.SoNgayOmKhacT14Ngay.GetValueOrDefault());
                        itemAgencyData.SoNgayConOm = AgencyData.Sum(x => x.SoNgayConOm.GetValueOrDefault());
                        itemAgencyData.SoNgayDuongSuc = AgencyData.Sum(x => x.SoNgayDuongSuc.GetValueOrDefault());

                        results.Add(itemAgencyData);
                        if (AgencyData.Any(x => !string.IsNullOrEmpty(x.SLoaiTC)))
                        {
                            foreach (var itemLoai in AgencyData.Where(x => !string.IsNullOrEmpty(x.SLoaiTC)).OrderBy(o => o.STT).Select(x => x.SLoaiTC).Distinct())
                            {
                                var itemsLoai = AgencyData.Where(x => x.SLoaiTC.Equals(itemLoai));
                                TlBangLuongThangBHXHQuery itemLoaiData = new TlBangLuongThangBHXHQuery
                                {
                                    MaDonVi = itemAgency.ValueItem,
                                    IsHangCha = true,
                                    IsHasData = true,
                                    IsAgency = false,
                                    STT = !itemsLoai.IsEmpty() ? dataInput.FirstOrDefault(x => !string.IsNullOrEmpty(x.SLoaiTC) && x.SLoaiTC.Equals(itemLoai)).STT : string.Empty,
                                    TenCbo = !itemsLoai.IsEmpty() ? dataInput.FirstOrDefault(x => !string.IsNullOrEmpty(x.SLoaiTC) && x.SLoaiTC.Equals(itemLoai)).TenCbo : string.Empty,
                                };
                                if (!itemsLoai.IsEmpty())
                                {
                                    itemLoaiData.SoCanBo = itemsLoai.Count();
                                    itemLoaiData.FLuongCanCu = itemsLoai.Sum(x => x.FLuongCanCu.GetValueOrDefault());
                                    itemLoaiData.FSinhConNuoiCon = itemsLoai.Sum(x => x.FSinhConNuoiCon.GetValueOrDefault());
                                    itemLoaiData.FTroCap1Lan = itemsLoai.Sum(x => x.FTroCap1Lan.GetValueOrDefault());
                                    itemLoaiData.FKhamThai = itemsLoai.Sum(x => x.FKhamThai.GetValueOrDefault());
                                    itemLoaiData.FDuongSucPHSKThaiSan = itemsLoai.Sum(x => x.FDuongSucPHSKThaiSan.GetValueOrDefault());
                                    itemLoaiData.FTongSoTien = itemsLoai.Sum(x => x.FTongSoTien.GetValueOrDefault());
                                    itemLoaiData.FTroCapKhuVuc = itemsLoai.Sum(x => x.FTroCapKhuVuc.GetValueOrDefault());
                                    itemLoaiData.FTroCapMaiTang = itemsLoai.Sum(x => x.FTroCapMaiTang.GetValueOrDefault());
                                    itemLoaiData.FChiGiamDinh = itemsLoai.Sum(x => x.FChiGiamDinh.GetValueOrDefault());
                                    itemLoaiData.FTroCapTheoPhieuTruyTra = itemsLoai.Sum(x => x.FTroCapTheoPhieuTruyTra.GetValueOrDefault());
                                    itemLoaiData.FTroCapHangThang = itemsLoai.Sum(x => x.FTroCapHangThang.GetValueOrDefault());
                                    itemLoaiData.FTroCapPHCN = itemsLoai.Sum(x => x.FTroCapPHCN.GetValueOrDefault());
                                    itemLoaiData.FTroCapChetDoTNLD = itemsLoai.Sum(x => x.FTroCapChetDoTNLD.GetValueOrDefault());
                                    itemLoaiData.FDuongSucTNLD = itemsLoai.Sum(x => x.FDuongSucTNLD.GetValueOrDefault());
                                    itemLoaiData.FConOm = itemsLoai.Sum(x => x.FConOm.GetValueOrDefault());
                                    itemLoaiData.FBenhDaiNgayD14Ngay = itemsLoai.Sum(x => x.FBenhDaiNgayD14Ngay.GetValueOrDefault());
                                    itemLoaiData.FBenhDauNgayT14Ngay = itemsLoai.Sum(x => x.FBenhDauNgayT14Ngay.GetValueOrDefault());
                                    itemLoaiData.FOmKhacD14Ngay = itemsLoai.Sum(x => x.FOmKhacD14Ngay.GetValueOrDefault());
                                    itemLoaiData.FOmKhacT14Ngay = itemsLoai.Sum(x => x.FOmKhacT14Ngay.GetValueOrDefault());
                                    itemLoaiData.FDuongSucPHSK = itemsLoai.Sum(x => x.FDuongSucPHSK.GetValueOrDefault());
                                    itemLoaiData.FSoPhaiTruBHXH = itemsLoai.Sum(x => x.FSoPhaiTruBHXH.GetValueOrDefault());
                                    itemLoaiData.FDuocNhan = itemsLoai.Sum(x => x.FDuocNhan.GetValueOrDefault());
                                    itemLoaiData.FTongSoTienThangNay = itemsLoai.Sum(x => x.FTongSoTienThangNay.GetValueOrDefault());
                                    itemLoaiData.FTongSoTienTruyLinh = itemsLoai.Sum(x => x.FTongSoTienTruyLinh.GetValueOrDefault());
                                    itemLoaiData.FTroCap1LanTruyLinh = itemsLoai.Sum(x => x.FTroCap1LanTruyLinh.GetValueOrDefault());
                                    itemLoaiData.FTroCapKhuVucTruyLinh = itemsLoai.Sum(x => x.FTroCapKhuVucTruyLinh.GetValueOrDefault());
                                    itemLoaiData.FTroCapMaiTangTruyLinh = itemsLoai.Sum(x => x.FTroCapMaiTangTruyLinh.GetValueOrDefault());

                                    itemLoaiData.FChiGiamDinhTruyLinh = itemsLoai.Sum(x => x.FChiGiamDinhTruyLinh.GetValueOrDefault());
                                    itemLoaiData.FHoTroCdnnTruyLinh = itemsLoai.Sum(x => x.FHoTroCdnnTruyLinh.GetValueOrDefault());
                                    itemLoaiData.FTroCapHangThangTruyLinh = itemsLoai.Sum(x => x.FTroCapHangThangTruyLinh.GetValueOrDefault());
                                    itemLoaiData.FTroCapPHCNTruyLinh = itemsLoai.Sum(x => x.FTroCapPHCNTruyLinh.GetValueOrDefault());
                                    itemLoaiData.FTroCapChetDoTNLDTruyLinh = itemsLoai.Sum(x => x.FTroCapChetDoTNLDTruyLinh.GetValueOrDefault());
                                    itemLoaiData.FDuongSucTNLDTruyLinh = itemsLoai.Sum(x => x.FDuongSucTNLDTruyLinh.GetValueOrDefault());
                                    itemLoaiData.SoNgayDuongSucTNLD = itemsLoai.Sum(x => x.SoNgayDuongSucTNLD.GetValueOrDefault());
                                    itemLoaiData.SoNgayDuongSucTNLDTruyLinh = itemsLoai.Sum(x => x.SoNgayDuongSucTNLDTruyLinh.GetValueOrDefault());

                                    itemLoaiData.FHoTroCdnn = itemsLoai.Sum(x => x.FHoTroCdnn.GetValueOrDefault());
                                    itemLoaiData.FHoTroPhongNgua = itemsLoai.Sum(x => x.FHoTroPhongNgua.GetValueOrDefault());
                                    itemLoaiData.FHoTroPhongNguaTruyLinh = itemsLoai.Sum(x => x.FHoTroPhongNguaTruyLinh.GetValueOrDefault());
                                    results.Add(itemLoaiData);
                                }

                                foreach (var item in itemsLoai.OrderBy(o => o.STT).Select(x => x.LoaiDoiTuong).Distinct())
                                {
                                    var ItemsNganh = itemsLoai.Where(x => x.LoaiDoiTuong.Equals(item)).ToList();
                                    var itemInput = dataInput.FirstOrDefault(x => !string.IsNullOrEmpty(x.LoaiDoiTuong) && x.LoaiDoiTuong.Equals(item) && x.IsHangCha.GetValueOrDefault());
                                    TlBangLuongThangBHXHQuery itemNganh = new TlBangLuongThangBHXHQuery
                                    {
                                        STT = itemInput.STT,
                                        TenCbo = itemInput.TenCbo,
                                        MaDonVi = itemAgency.ValueItem,
                                        IsHangCha = true,
                                        IsHasData = true,
                                        IsAgency = false,
                                        IsDeleteParent = true,
                                    };
                                    if (!ItemsNganh.IsEmpty())
                                    {
                                        itemNganh.SoCanBo = ItemsNganh.Count();
                                        itemNganh.FLuongCanCu = ItemsNganh.Sum(x => x.FLuongCanCu.GetValueOrDefault());
                                        itemNganh.FSinhConNuoiCon = ItemsNganh.Sum(x => x.FSinhConNuoiCon.GetValueOrDefault());
                                        itemNganh.FTroCap1Lan = ItemsNganh.Sum(x => x.FTroCap1Lan.GetValueOrDefault());
                                        itemNganh.FKhamThai = ItemsNganh.Sum(x => x.FKhamThai.GetValueOrDefault());
                                        itemNganh.FDuongSucPHSKThaiSan = ItemsNganh.Sum(x => x.FDuongSucPHSKThaiSan.GetValueOrDefault());
                                        itemNganh.FTongSoTien = ItemsNganh.Sum(x => x.FTongSoTien.GetValueOrDefault());
                                        itemNganh.FTroCapKhuVuc = ItemsNganh.Sum(x => x.FTroCapKhuVuc.GetValueOrDefault());
                                        itemNganh.FTroCapMaiTang = ItemsNganh.Sum(x => x.FTroCapMaiTang.GetValueOrDefault());
                                        itemNganh.FConOm = ItemsNganh.Sum(x => x.FConOm.GetValueOrDefault());
                                        itemNganh.FBenhDaiNgayD14Ngay = ItemsNganh.Sum(x => x.FBenhDaiNgayD14Ngay.GetValueOrDefault());
                                        itemNganh.FBenhDauNgayT14Ngay = ItemsNganh.Sum(x => x.FBenhDauNgayT14Ngay.GetValueOrDefault());
                                        itemNganh.FOmKhacD14Ngay = ItemsNganh.Sum(x => x.FOmKhacD14Ngay.GetValueOrDefault());
                                        itemNganh.FOmKhacT14Ngay = ItemsNganh.Sum(x => x.FOmKhacT14Ngay.GetValueOrDefault());
                                        itemNganh.FDuongSucPHSK = ItemsNganh.Sum(x => x.FDuongSucPHSK.GetValueOrDefault());
                                        itemNganh.FSoPhaiTruBHXH = ItemsNganh.Sum(x => x.FSoPhaiTruBHXH.GetValueOrDefault());
                                        itemNganh.FDuocNhan = ItemsNganh.Sum(x => x.FDuocNhan.GetValueOrDefault());
                                        itemNganh.FTongSoTienThangNay = ItemsNganh.Sum(x => x.FTongSoTienThangNay.GetValueOrDefault());
                                        itemNganh.FTongSoTienTruyLinh = ItemsNganh.Sum(x => x.FTongSoTienTruyLinh.GetValueOrDefault());
                                        itemNganh.FTroCap1LanTruyLinh = ItemsNganh.Sum(x => x.FTroCap1LanTruyLinh.GetValueOrDefault());
                                        itemNganh.FTroCapKhuVucTruyLinh = ItemsNganh.Sum(x => x.FTroCapKhuVucTruyLinh.GetValueOrDefault());
                                        itemNganh.FTroCapMaiTangTruyLinh = ItemsNganh.Sum(x => x.FTroCapMaiTangTruyLinh.GetValueOrDefault());
                                        itemNganh.FChiGiamDinhTruyLinh = ItemsNganh.Sum(x => x.FChiGiamDinhTruyLinh.GetValueOrDefault());
                                        itemNganh.FHoTroCdnnTruyLinh = ItemsNganh.Sum(x => x.FHoTroCdnnTruyLinh.GetValueOrDefault());
                                        itemNganh.FTroCapHangThangTruyLinh = ItemsNganh.Sum(x => x.FTroCapHangThangTruyLinh.GetValueOrDefault());
                                        itemNganh.FTroCapPHCNTruyLinh = ItemsNganh.Sum(x => x.FTroCapPHCNTruyLinh.GetValueOrDefault());
                                        itemNganh.FTroCapChetDoTNLDTruyLinh = ItemsNganh.Sum(x => x.FTroCapChetDoTNLDTruyLinh.GetValueOrDefault());
                                        itemNganh.FDuongSucTNLDTruyLinh = ItemsNganh.Sum(x => x.FDuongSucTNLDTruyLinh.GetValueOrDefault());
                                        itemNganh.SoNgayDuongSucTNLD = ItemsNganh.Sum(x => x.SoNgayDuongSucTNLD.GetValueOrDefault());
                                        itemNganh.SoNgayDuongSucTNLDTruyLinh = ItemsNganh.Sum(x => x.SoNgayDuongSucTNLDTruyLinh.GetValueOrDefault());
                                        itemNganh.FHoTroCdnn = ItemsNganh.Sum(x => x.FHoTroCdnn.GetValueOrDefault());
                                        itemNganh.FHoTroPhongNgua = ItemsNganh.Sum(x => x.FHoTroPhongNgua.GetValueOrDefault());
                                        itemNganh.FHoTroPhongNguaTruyLinh = ItemsNganh.Sum(x => x.FHoTroPhongNguaTruyLinh.GetValueOrDefault());
                                    }

                                    foreach (var ite in ItemsNganh.Select((value, index) => new { index, value }))
                                    {
                                        ite.value.STT = (ite.index + 1).ToString();
                                    }
                                    results.Add(itemNganh);
                                    results.AddRange(ItemsNganh);
                                }
                            }
                        }
                        else if (AgencyData.Any(x => !string.IsNullOrEmpty(x.LoaiDoiTuong)))
                        {
                            foreach (var item in AgencyData.OrderBy(o => o.OrderNganh).Select(x => x.LoaiDoiTuong).Distinct())
                            {
                                var ItemsNganh = AgencyData.Where(x => x.LoaiDoiTuong.Equals(item)).ToList();
                                var itemInput = dataInput.FirstOrDefault(x => !string.IsNullOrEmpty(x.LoaiDoiTuong) && x.LoaiDoiTuong.Equals(item) && x.IsHangCha.GetValueOrDefault());
                                TlBangLuongThangBHXHQuery itemNganh = new TlBangLuongThangBHXHQuery
                                {
                                    STT = itemInput.STT,
                                    TenCbo = itemInput.TenCbo,
                                    MaDonVi = itemAgency.ValueItem,
                                    IsHangCha = true,
                                    IsHasData = true,
                                    IsAgency = false,
                                    IsDeleteParent = true,
                                };
                                if (!ItemsNganh.IsEmpty())
                                {
                                    itemNganh.SoCanBo = ItemsNganh.Count();
                                    itemNganh.FLuongCanCu = ItemsNganh.Sum(x => x.FLuongCanCu.GetValueOrDefault());
                                    itemNganh.FSinhConNuoiCon = ItemsNganh.Sum(x => x.FSinhConNuoiCon.GetValueOrDefault());
                                    itemNganh.FTroCap1Lan = ItemsNganh.Sum(x => x.FTroCap1Lan.GetValueOrDefault());
                                    itemNganh.FKhamThai = ItemsNganh.Sum(x => x.FKhamThai.GetValueOrDefault());
                                    itemNganh.FDuongSucPHSKThaiSan = ItemsNganh.Sum(x => x.FDuongSucPHSKThaiSan.GetValueOrDefault());
                                    itemNganh.FTongSoTien = ItemsNganh.Sum(x => x.FTongSoTien.GetValueOrDefault());
                                    itemNganh.FTroCapKhuVuc = ItemsNganh.Sum(x => x.FTroCapKhuVuc.GetValueOrDefault());
                                    itemNganh.FTroCapMaiTang = ItemsNganh.Sum(x => x.FTroCapMaiTang.GetValueOrDefault());
                                    itemNganh.FChiGiamDinh = ItemsNganh.Sum(x => x.FChiGiamDinh.GetValueOrDefault());
                                    itemNganh.FTroCapTheoPhieuTruyTra = ItemsNganh.Sum(x => x.FTroCapTheoPhieuTruyTra.GetValueOrDefault());
                                    itemNganh.FTroCapHangThang = ItemsNganh.Sum(x => x.FTroCapHangThang.GetValueOrDefault());
                                    itemNganh.FTroCapPHCN = ItemsNganh.Sum(x => x.FTroCapPHCN.GetValueOrDefault());
                                    itemNganh.FTroCapChetDoTNLD = ItemsNganh.Sum(x => x.FTroCapChetDoTNLD.GetValueOrDefault());
                                    itemNganh.FDuongSucTNLD = ItemsNganh.Sum(x => x.FDuongSucTNLD.GetValueOrDefault());
                                    itemNganh.FConOm = ItemsNganh.Sum(x => x.FConOm.GetValueOrDefault());
                                    itemNganh.FBenhDaiNgayD14Ngay = ItemsNganh.Sum(x => x.FBenhDaiNgayD14Ngay.GetValueOrDefault());
                                    itemNganh.FBenhDauNgayT14Ngay = ItemsNganh.Sum(x => x.FBenhDauNgayT14Ngay.GetValueOrDefault());
                                    itemNganh.FOmKhacD14Ngay = ItemsNganh.Sum(x => x.FOmKhacD14Ngay.GetValueOrDefault());
                                    itemNganh.FOmKhacT14Ngay = ItemsNganh.Sum(x => x.FOmKhacT14Ngay.GetValueOrDefault());
                                    itemNganh.FDuongSucPHSK = ItemsNganh.Sum(x => x.FDuongSucPHSK.GetValueOrDefault());
                                    itemNganh.FSoPhaiTruBHXH = ItemsNganh.Sum(x => x.FSoPhaiTruBHXH.GetValueOrDefault());
                                    itemNganh.FDuocNhan = ItemsNganh.Sum(x => x.FDuocNhan.GetValueOrDefault());
                                    itemNganh.FTongSoTienThangNay = ItemsNganh.Sum(x => x.FTongSoTienThangNay.GetValueOrDefault());
                                    itemNganh.FTongSoTienTruyLinh = ItemsNganh.Sum(x => x.FTongSoTienTruyLinh.GetValueOrDefault());
                                    itemNganh.FTroCap1LanTruyLinh = ItemsNganh.Sum(x => x.FTroCap1LanTruyLinh.GetValueOrDefault());
                                    itemNganh.FTroCapKhuVucTruyLinh = ItemsNganh.Sum(x => x.FTroCapKhuVucTruyLinh.GetValueOrDefault());
                                    itemNganh.FTroCapMaiTangTruyLinh = ItemsNganh.Sum(x => x.FTroCapMaiTangTruyLinh.GetValueOrDefault());
                                    itemNganh.FChiGiamDinhTruyLinh = ItemsNganh.Sum(x => x.FChiGiamDinhTruyLinh.GetValueOrDefault());
                                    itemNganh.FHoTroCdnnTruyLinh = ItemsNganh.Sum(x => x.FHoTroCdnnTruyLinh.GetValueOrDefault());
                                    itemNganh.FTroCapHangThangTruyLinh = ItemsNganh.Sum(x => x.FTroCapHangThangTruyLinh.GetValueOrDefault());
                                    itemNganh.FTroCapPHCNTruyLinh = ItemsNganh.Sum(x => x.FTroCapPHCNTruyLinh.GetValueOrDefault());
                                    itemNganh.FTroCapChetDoTNLDTruyLinh = ItemsNganh.Sum(x => x.FTroCapChetDoTNLDTruyLinh.GetValueOrDefault());
                                    itemNganh.FDuongSucTNLDTruyLinh = ItemsNganh.Sum(x => x.FDuongSucTNLDTruyLinh.GetValueOrDefault());
                                    itemNganh.SoNgayDuongSucTNLD = ItemsNganh.Sum(x => x.SoNgayDuongSucTNLD.GetValueOrDefault());
                                    itemNganh.SoNgayDuongSucTNLDTruyLinh = ItemsNganh.Sum(x => x.SoNgayDuongSucTNLDTruyLinh.GetValueOrDefault());
                                    itemNganh.FHoTroCdnn = ItemsNganh.Sum(x => x.FHoTroCdnn.GetValueOrDefault());
                                    itemNganh.FHoTroPhongNgua = ItemsNganh.Sum(x => x.FHoTroPhongNgua.GetValueOrDefault());
                                    itemNganh.FHoTroPhongNguaTruyLinh = ItemsNganh.Sum(x => x.FHoTroPhongNguaTruyLinh.GetValueOrDefault());
                                    itemNganh.SoNgayBenhDaiNgayD14Ngay = ItemsNganh.Sum(x => x.SoNgayBenhDaiNgayD14Ngay.GetValueOrDefault());
                                    itemNganh.SoNgayBenhDaiNgayT14Ngay = ItemsNganh.Sum(x => x.SoNgayBenhDaiNgayT14Ngay.GetValueOrDefault());
                                    itemNganh.SoNgayOmKhacD14Ngay = ItemsNganh.Sum(x => x.SoNgayOmKhacD14Ngay.GetValueOrDefault());
                                    itemNganh.SoNgayOmKhacT14Ngay = ItemsNganh.Sum(x => x.SoNgayOmKhacT14Ngay.GetValueOrDefault());
                                    itemNganh.SoNgayConOm = ItemsNganh.Sum(x => x.SoNgayConOm.GetValueOrDefault());
                                    itemNganh.SoNgayDuongSuc = ItemsNganh.Sum(x => x.SoNgayDuongSuc.GetValueOrDefault());

                                }

                                foreach (var ite in ItemsNganh.Select((value, index) => new { index, value }))
                                {
                                    ite.value.STT = (ite.index + 1).ToString();
                                }
                                results.Add(itemNganh);
                                results.AddRange(ItemsNganh);
                            }

                        }
                        else
                        {
                            foreach (var ite in AgencyData.Select((value, index) => new { index, value }))
                            {
                                ite.value.STT = (ite.index + 1).ToString();
                            }
                            results.AddRange(AgencyData);
                        }
                    }
                }
                return results;
            }
            else
            {
                var ItemsNganh = dataInput.Where(x => x.IsHangCha.GetValueOrDefault() && !string.IsNullOrEmpty(x.LoaiDoiTuong)).ToList();
                var ItemsLoai = dataInput.Where(x => x.IsHangCha.GetValueOrDefault() && !string.IsNullOrEmpty(x.SLoaiTC)).ToList();
                var Items = dataInput.Where(x => x.IsHangCha.GetValueOrDefault() && string.IsNullOrEmpty(x.SLoaiTC) && string.IsNullOrEmpty(x.LoaiDoiTuong)).ToList();
                if (!ItemsLoai.IsEmpty())
                {
                    ItemsLoai.Select(x =>
                    {
                        x.SoCanBo = dataInput.Count(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC));
                        x.FLuongCanCu = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FLuongCanCu.GetValueOrDefault());
                        x.FSinhConNuoiCon = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FSinhConNuoiCon.GetValueOrDefault());
                        x.FTroCap1Lan = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCap1Lan.GetValueOrDefault());
                        x.FKhamThai = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FKhamThai.GetValueOrDefault());
                        x.FDuongSucPHSKThaiSan = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FDuongSucPHSKThaiSan.GetValueOrDefault());
                        x.FTongSoTien = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTongSoTien.GetValueOrDefault());
                        x.FTroCapKhuVuc = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCapKhuVuc.GetValueOrDefault());
                        x.FTroCapMaiTang = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCapMaiTang.GetValueOrDefault());
                        x.FChiGiamDinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FChiGiamDinh.GetValueOrDefault());
                        x.FTroCapTheoPhieuTruyTra = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCapTheoPhieuTruyTra.GetValueOrDefault());
                        x.FTroCapHangThang = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCapHangThang.GetValueOrDefault());
                        x.FTroCapPHCN = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCapPHCN.GetValueOrDefault());
                        x.FTroCapChetDoTNLD = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCapChetDoTNLD.GetValueOrDefault());
                        x.FDuongSucTNLD = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FDuongSucTNLD.GetValueOrDefault());
                        x.FConOm = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FConOm.GetValueOrDefault());
                        x.FBenhDaiNgayD14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FBenhDaiNgayD14Ngay.GetValueOrDefault());
                        x.FBenhDauNgayT14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FBenhDauNgayT14Ngay.GetValueOrDefault());
                        x.FOmKhacD14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FOmKhacD14Ngay.GetValueOrDefault());
                        x.FOmKhacT14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FOmKhacT14Ngay.GetValueOrDefault());
                        x.FDuongSucPHSK = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FDuongSucPHSK.GetValueOrDefault());
                        x.FSoPhaiTruBHXH = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FSoPhaiTruBHXH.GetValueOrDefault());
                        x.FDuocNhan = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FDuocNhan.GetValueOrDefault());

                        x.FTroCapKhuVucTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCapKhuVucTruyLinh.GetValueOrDefault());
                        x.FTroCapMaiTangTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCapMaiTangTruyLinh.GetValueOrDefault());
                        x.FTroCap1LanTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCap1LanTruyLinh.GetValueOrDefault());
                        x.FTongSoTienThangNay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTongSoTienThangNay.GetValueOrDefault());
                        x.FTongSoTienTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTongSoTienTruyLinh.GetValueOrDefault());

                        x.FChiGiamDinhTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FChiGiamDinhTruyLinh.GetValueOrDefault());
                        x.FHoTroCdnnTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FHoTroCdnnTruyLinh.GetValueOrDefault());
                        x.FTroCapHangThangTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCapHangThangTruyLinh.GetValueOrDefault());
                        x.FTroCapPHCNTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCapPHCNTruyLinh.GetValueOrDefault());
                        x.FTroCapChetDoTNLDTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCapChetDoTNLDTruyLinh.GetValueOrDefault());
                        x.FDuongSucTNLDTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FDuongSucTNLDTruyLinh.GetValueOrDefault());
                        x.SoNgayDuongSucTNLD = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.SoNgayDuongSucTNLD.GetValueOrDefault());
                        x.SoNgayDuongSucTNLDTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.SoNgayDuongSucTNLDTruyLinh.GetValueOrDefault());

                        return x;
                    }).ToList();

                    if (!ItemsNganh.IsEmpty())
                    {
                        ItemsNganh.Select(x =>
                        {
                            x.SoCanBo = dataInput.Count(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong) && y.SLoaiTC.Equals(x.SLoaiTC));
                            x.FLuongCanCu = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong) && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FLuongCanCu.GetValueOrDefault());
                            x.FSinhConNuoiCon = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong) && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FSinhConNuoiCon.GetValueOrDefault());
                            x.FTroCap1Lan = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong) && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCap1Lan.GetValueOrDefault());
                            x.FKhamThai = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong) && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FKhamThai.GetValueOrDefault());
                            x.FDuongSucPHSKThaiSan = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong) && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FDuongSucPHSKThaiSan.GetValueOrDefault());
                            x.FTongSoTien = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong) && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTongSoTien.GetValueOrDefault());
                            x.FTroCapKhuVuc = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong) && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCapKhuVuc.GetValueOrDefault());
                            x.FTroCapMaiTang = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong) && y.SLoaiTC.Equals(x.SLoaiTC)).Sum(x => x.FTroCapMaiTang.GetValueOrDefault());
                            x.FChiGiamDinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FChiGiamDinh.GetValueOrDefault());
                            x.FTroCapTheoPhieuTruyTra = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapTheoPhieuTruyTra.GetValueOrDefault());
                            x.FTroCapHangThang = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapHangThang.GetValueOrDefault());
                            x.FTroCapPHCN = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapPHCN.GetValueOrDefault());
                            x.FTroCapChetDoTNLD = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapChetDoTNLD.GetValueOrDefault());
                            x.FDuongSucTNLD = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FDuongSucTNLD.GetValueOrDefault());
                            x.FConOm = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FConOm.GetValueOrDefault());
                            x.FBenhDaiNgayD14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FBenhDaiNgayD14Ngay.GetValueOrDefault());
                            x.FBenhDauNgayT14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FBenhDauNgayT14Ngay.GetValueOrDefault());
                            x.FOmKhacD14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FOmKhacD14Ngay.GetValueOrDefault());
                            x.FOmKhacT14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FOmKhacT14Ngay.GetValueOrDefault());
                            x.FDuongSucPHSK = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FDuongSucPHSK.GetValueOrDefault());
                            x.FSoPhaiTruBHXH = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FSoPhaiTruBHXH.GetValueOrDefault());
                            x.FDuocNhan = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FDuocNhan.GetValueOrDefault());
                            x.FTongSoTienThangNay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTongSoTienThangNay.GetValueOrDefault());
                            x.FTongSoTienTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTongSoTienTruyLinh.GetValueOrDefault());
                            x.FTroCap1LanTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCap1LanTruyLinh.GetValueOrDefault());
                            x.FTroCapKhuVucTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapKhuVucTruyLinh.GetValueOrDefault());
                            x.FTroCapMaiTangTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapMaiTangTruyLinh.GetValueOrDefault());

                            x.FChiGiamDinhTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FChiGiamDinhTruyLinh.GetValueOrDefault());
                            x.FHoTroCdnnTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FHoTroCdnnTruyLinh.GetValueOrDefault());
                            x.FTroCapHangThangTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapHangThangTruyLinh.GetValueOrDefault());
                            x.FTroCapPHCNTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapPHCNTruyLinh.GetValueOrDefault());
                            x.FTroCapChetDoTNLDTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapChetDoTNLDTruyLinh.GetValueOrDefault());
                            x.FDuongSucTNLDTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FDuongSucTNLDTruyLinh.GetValueOrDefault());
                            x.SoNgayDuongSucTNLD = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.SoNgayDuongSucTNLD.GetValueOrDefault());
                            x.SoNgayDuongSucTNLDTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.SoNgayDuongSucTNLDTruyLinh.GetValueOrDefault());

                            x.FHoTroCdnn = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FHoTroCdnn.GetValueOrDefault());
                            x.FHoTroPhongNgua = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FHoTroPhongNgua.GetValueOrDefault());
                            x.FHoTroPhongNguaTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.SLoaiTC.Equals(x.SLoaiTC) && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FHoTroPhongNguaTruyLinh.GetValueOrDefault());
                            return x;
                        }).ToList();
                    }
                }
                else
                {
                    if (!ItemsNganh.IsEmpty())
                    {
                        ItemsNganh.Select(x =>
                        {
                            x.SoCanBo = dataInput.Count(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong));
                            x.FLuongCanCu = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FLuongCanCu.GetValueOrDefault());
                            x.FSinhConNuoiCon = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FSinhConNuoiCon.GetValueOrDefault());
                            x.FTroCap1Lan = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCap1Lan.GetValueOrDefault());
                            x.FKhamThai = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FKhamThai.GetValueOrDefault());
                            x.FDuongSucPHSKThaiSan = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FDuongSucPHSKThaiSan.GetValueOrDefault());
                            x.FTongSoTien = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTongSoTien.GetValueOrDefault());
                            x.FTroCapKhuVuc = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapKhuVuc.GetValueOrDefault());
                            x.FTroCapMaiTang = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapMaiTang.GetValueOrDefault());
                            x.FChiGiamDinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FChiGiamDinh.GetValueOrDefault());
                            x.FTroCapTheoPhieuTruyTra = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapTheoPhieuTruyTra.GetValueOrDefault());
                            x.FTroCapHangThang = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapHangThang.GetValueOrDefault());
                            x.FTroCapPHCN = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapPHCN.GetValueOrDefault());
                            x.FTroCapChetDoTNLD = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapChetDoTNLD.GetValueOrDefault());
                            x.FDuongSucTNLD = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FDuongSucTNLD.GetValueOrDefault());
                            x.FConOm = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FConOm.GetValueOrDefault());
                            x.FBenhDaiNgayD14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FBenhDaiNgayD14Ngay.GetValueOrDefault());
                            x.FBenhDauNgayT14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FBenhDauNgayT14Ngay.GetValueOrDefault());
                            x.FOmKhacD14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FOmKhacD14Ngay.GetValueOrDefault());
                            x.FOmKhacT14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FOmKhacT14Ngay.GetValueOrDefault());
                            x.FDuongSucPHSK = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FDuongSucPHSK.GetValueOrDefault());
                            x.FSoPhaiTruBHXH = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FSoPhaiTruBHXH.GetValueOrDefault());
                            x.FDuocNhan = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FDuocNhan.GetValueOrDefault());

                            x.FTroCapKhuVucTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapKhuVucTruyLinh.GetValueOrDefault());
                            x.FTroCapMaiTangTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapMaiTangTruyLinh.GetValueOrDefault());
                            x.FTroCap1LanTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCap1LanTruyLinh.GetValueOrDefault());
                            x.FTongSoTienThangNay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTongSoTienThangNay.GetValueOrDefault());
                            x.FTongSoTienTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTongSoTienTruyLinh.GetValueOrDefault());

                            x.FChiGiamDinhTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FChiGiamDinhTruyLinh.GetValueOrDefault());
                            x.FHoTroCdnnTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FHoTroCdnnTruyLinh.GetValueOrDefault());
                            x.FTroCapHangThangTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapHangThangTruyLinh.GetValueOrDefault());
                            x.FTroCapPHCNTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapPHCNTruyLinh.GetValueOrDefault());
                            x.FTroCapChetDoTNLDTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FTroCapChetDoTNLDTruyLinh.GetValueOrDefault());
                            x.FDuongSucTNLDTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FDuongSucTNLDTruyLinh.GetValueOrDefault());
                            x.SoNgayDuongSucTNLDTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.SoNgayDuongSucTNLDTruyLinh.GetValueOrDefault());
                            x.SoNgayDuongSucTNLD = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.SoNgayDuongSucTNLD.GetValueOrDefault());

                            x.FHoTroCdnn = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FHoTroCdnn.GetValueOrDefault());
                            x.FHoTroPhongNgua = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FHoTroPhongNgua.GetValueOrDefault());
                            x.FHoTroPhongNguaTruyLinh = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.FHoTroPhongNguaTruyLinh.GetValueOrDefault());

                            x.SoNgayBenhDaiNgayD14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.SoNgayBenhDaiNgayD14Ngay.GetValueOrDefault());
                            x.SoNgayBenhDaiNgayT14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.SoNgayBenhDaiNgayT14Ngay.GetValueOrDefault());
                            x.SoNgayOmKhacD14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.SoNgayOmKhacD14Ngay.GetValueOrDefault());
                            x.SoNgayOmKhacT14Ngay = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.SoNgayOmKhacT14Ngay.GetValueOrDefault());
                            x.SoNgayConOm = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.SoNgayConOm.GetValueOrDefault());
                            x.SoNgayDuongSuc = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.SoNgayDuongSuc.GetValueOrDefault());
                            x.SoNgaySinhConNuoiCon= dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.SoNgaySinhConNuoiCon.GetValueOrDefault());
                            x.SoNgayTroCap1Lan = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.SoNgayTroCap1Lan.GetValueOrDefault());
                            x.SoNgayKhamThai = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.SoNgayKhamThai.GetValueOrDefault());
                            x.SoNgayDuongSucPHSKThaiSan = dataInput.Where(y => !y.IsHangCha.GetValueOrDefault() && y.LoaiDoiTuong.Equals(x.LoaiDoiTuong)).Sum(x => x.SoNgayDuongSucPHSKThaiSan.GetValueOrDefault());
                            return x;
                        }).ToList();
                    }
                }
                return dataInput;
            }
        }

        public void PrintBangThanhToanTroCapTNLD(ExportType exportType, int selectedYear, int selectedMonth)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    string lstDonVi = string.Join(StringUtils.COMMA, lstDonViSelected.Select(x => x.ValueItem));
                    var exportData = _bangLuongService.ExportBangThanhToanTroCapTNLD(lstDonVi, selectedYear, selectedMonth, donViTinh).Where(x => x.IsHasData).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    string templateFileName;
                    string fileNamePrefix;
                    if (IsSummary && SelectedSummaryType != null)
                    {
                        exportData = CalculateDataExportByAgency(exportData, lstDonViSelected);
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_TNLD_DON_VI));
                    }
                    else
                    {
                        exportData = CalculateDataExportByAgency(exportData, lstDonViSelected);
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_TNLD));
                    }
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", exportData);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("YearOfWork", "năm " + selectedYear);
                    data.Add("Month", "Tháng " + selectedMonth);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("DonVi", GetHeader2Report());
                    data.Add("TotalSoNgayDuongSucTNLD", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.SoNgayDuongSucTNLD));
                    data.Add("TotalFChiGiamDinh", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FChiGiamDinh));
                    data.Add("TotalFTroCapTheoPhieuTruyTra", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCapTheoPhieuTruyTra));
                    data.Add("TotalFTroCapHangThang", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCapHangThang));
                    data.Add("TotalFTroCapPHCN", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCapPHCN));
                    data.Add("TotalFHoTroCdnn", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FHoTroCdnn));
                    data.Add("TotalFHoTroPhongNgua", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FHoTroPhongNgua));
                    data.Add("TotalFTroCap1Lan", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCap1Lan));
                    data.Add("TotalFTroCapChetDoTNLD", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCapChetDoTNLD));
                    data.Add("TotalFDuongSucTNLD", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FDuongSucTNLD));
                    data.Add("TotalLuongCanCu", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FLuongCanCu));
                    data.Add("TotalFTongSoTien", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTongSoTien));
                    data.Add("TotalFTongSoTienThangNay", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTongSoTienThangNay));
                    data.Add("TotalFTongSoTienTruyLinh", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTongSoTienTruyLinh));
                    data.Add("TotalFChiGiamDinhTruyLinh", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FChiGiamDinhTruyLinh.GetValueOrDefault()));
                    data.Add("TotalFTroCap1LanTruyLinh", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCap1LanTruyLinh.GetValueOrDefault()));
                    data.Add("TotalFHoTroCdnnTruyLinh", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FHoTroCdnnTruyLinh.GetValueOrDefault()));
                    data.Add("TotalFTroCapHangThangTruyLinh", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCapHangThangTruyLinh.GetValueOrDefault()));
                    data.Add("TotalFTroCapPHCNTruyLinh", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCapPHCNTruyLinh.GetValueOrDefault()));
                    data.Add("TotalFHoTroPhongNguaTruyLinh", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FHoTroPhongNguaTruyLinh.GetValueOrDefault()));
                    data.Add("TotalFTroCapChetDoTNLDTruyLinh", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCapChetDoTNLDTruyLinh.GetValueOrDefault()));
                    data.Add("TotalFDuongSucTNLDTruyLinh", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.FDuongSucTNLDTruyLinh.GetValueOrDefault()));
                    data.Add("TotalSoNgayDuongSucTNLDTruyLinh", exportData.Where(w => !w.IsHangCha.GetValueOrDefault()).Sum(x => x.SoNgayDuongSucTNLDTruyLinh.GetValueOrDefault()));
                    AddChuKy(data, _typeChuky);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<TlBangLuongThangBHXHQuery>(templateFileName, data);
                    if (IsSummary && SelectedSummaryType != null && SelectedSummaryType.ValueItem.Equals(LoaiTongHopBhxh.CTTL_DON_VI.ToString()))
                    {
                        int firstCol = 1;
                        int lastCol = 2;
                        int firstRow = 11;
                        foreach (var item in exportData.Where(x => x.IsAgency))
                        {
                            var index = exportData.IndexOf(item);
                            xlsFile.MergeCells(firstRow + index, firstCol, firstRow + index, lastCol);

                        }
                    }

                    results.Add(new ExportResult("Bảng thanh toán trợ cấp tai nạn lao động " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    e.Result = results;

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

        public void PrintBangThanhToanTroCapHuuTriPhucVienThoiViecTuTuat(ExportType exportType, int selectedYear, int selectedMonth)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    string lstDonVi = string.Join(StringUtils.COMMA, lstDonViSelected.Select(x => x.ValueItem));
                    var exportData = _bangLuongService.ExportBangThanhToanTroCapHuuTriPhucVienThoiViecTuTuat(lstDonVi, selectedYear, selectedMonth, donViTinh).Where(x => x.IsHasData).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    string templateFileName;
                    string fileNamePrefix;
                    if (IsSummary && SelectedSummaryType != null)
                    {
                        exportData = CalculateDataExportByAgency(exportData, lstDonViSelected);
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_HT_PV_TV_TT_DON_VI));
                    }
                    else
                    {
                        exportData = CalculateDataExportByAgency(exportData, lstDonViSelected);
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_HT_PV_TV_TT));
                    }
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork)
                    .Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", exportData);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("YearOfWork", "năm " + selectedYear);
                    data.Add("Month", "Tháng " + selectedMonth);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("DonVi", GetHeader2Report());
                    data.Add("TotalFTroCap1Lan", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCap1Lan));
                    data.Add("TotalFTroCapKhuVuc", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCapKhuVuc));
                    data.Add("TotalFTroCapMaiTang", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCapMaiTang));
                    data.Add("TotalFTongSoTien", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTongSoTien));
                    data.Add("TotalFTroCap1LanTruyLinh", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCap1LanTruyLinh));
                    data.Add("TotalFTroCapKhuVucTruyLinh", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCapKhuVucTruyLinh));
                    data.Add("TotalFTroCapMaiTangTruyLinh", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTroCapMaiTangTruyLinh));
                    data.Add("TotalFTongSoTienTruyLinh", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTongSoTienTruyLinh));
                    data.Add("TotalFTongSoTienThangNay", exportData.Where(x => !x.IsHangCha.GetValueOrDefault()).Sum(x => x.FTongSoTienThangNay));
                    AddChuKy(data, _typeChuky);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<TlBangLuongThangBHXHQuery>(templateFileName, data);
                    if (IsSummary && SelectedSummaryType != null && SelectedSummaryType.ValueItem.Equals(LoaiTongHopBhxh.CTTL_DON_VI.ToString()))
                    {
                        int firstCol = 1;
                        int lastCol = 2;
                        int firstRow = 10;
                        foreach (var item in exportData.Where(x => x.IsAgency))
                        {
                            var index = exportData.IndexOf(item);
                            xlsFile.MergeCells(firstRow + index, firstCol, firstRow + index, lastCol);
                        }
                    }

                    results.Add(new ExportResult("Bảng thanh toán trợ cấp tai nạn lao động " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

        public void PrintBangThanhToanTroCapXuatNgu(ExportType exportType, int selectedYear, int selectedMonth)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    string templateFileName;
                    string fileNamePrefix;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    string lstDonVi = string.Join(StringUtils.COMMA, lstDonViSelected.Select(x => x.ValueItem));
                    var exportData = _bangLuongService.ExportBangThanhToanTroXuatNgu(lstDonVi, selectedYear, selectedMonth, donViTinh).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    var h1 = _catUnitTypeSelected.DisplayItem;

                    if (IsSummary && SelectedSummaryType != null)
                    {
                        exportData = CalculateDataExportByAgency(exportData, lstDonViSelected);
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_XUAT_NGU_DON_VI));
                    }
                    else
                    {
                        exportData = CalculateDataExportByAgency(exportData, lstDonViSelected);
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_LUONG_TRO_CAP_XUAT_NGU));
                    }
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork)
                    .Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", exportData);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("YearOfWork", "năm " + selectedYear);
                    data.Add("Month", "Tháng " + selectedMonth);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("DonVi", GetHeader2Report());
                    data.Add("TotalFTroCap1Lan", exportData.Sum(x => x.FTroCap1Lan));
                    data.Add("TOTALLUONGCANCU", exportData.Sum(x => x.FTroCap1Lan));
                    AddChuKy(data, _typeChuky);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<TlBangLuongThangBHXHQuery>(templateFileName, data);
                    results.Add(new ExportResult("Bảng thanh toán trợ cấp tai nạn lao động " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh1MoTa);
            data.Add("ChucDanh1", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh1MoTa);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy == null ? string.Empty : dmChyKy.Ten1MoTa);
            data.Add("ThuaLenh2", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh2MoTa);
            data.Add("ChucDanh2", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh2MoTa);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy == null ? string.Empty : dmChyKy.Ten2MoTa);
            data.Add("ThuaLenh3", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh3MoTa);
            data.Add("ChucDanh3", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh3MoTa);
            data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten3", dmChyKy == null ? string.Empty : dmChyKy.Ten3MoTa);
            data.Add("ThuaLenh4", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh4MoTa);
            data.Add("ChucDanh4", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh4MoTa);
            data.Add("GhiChuKy4", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten4", dmChyKy == null ? string.Empty : dmChyKy.Ten4MoTa);
            if (dmChyKy != null && (!dmChyKy.ThuaLenh4MoTa.IsEmpty() || !dmChyKy.ChucDanh4MoTa.IsEmpty() || !dmChyKy.Ten4MoTa.IsEmpty()))
            {
                data.Add("Co4ChuKy", true);
            }
        }

        public string GetTemplate(string input)
        {
            return Path.Combine(ExportPrefix.PATH_TL_LUONG, input + FileExtensionFormats.Xlsx);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            if (InsuranceSalaryPrintType.TRO_CAP_OM_DAU.Equals(InsuranceSalaryPrintType)
               || InsuranceSalaryPrintType.TRO_CAP_THAI_SAN.Equals(InsuranceSalaryPrintType))
            {
                DmChuKyDialogViewModel.HasAddedSign4 = true;
            }
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TxtTitleFirst = chuKy.TieuDe1MoTa;
                TxtTitleSecond = chuKy.TieuDe2MoTa;
                TxtTitleThird = chuKy.TieuDe3MoTa;
                TenDVBanHanh1 = chuKy.TenDVBanHanh1;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        private string GetNewReportIndex(string input)
        {
            string sSTT = "";
            switch (input)
            {
                case "1":
                    sSTT = "I";
                    break;
                case "2":
                    sSTT = "II";
                    break;
                case "3":
                    sSTT = "III";
                    break;
                case "4":
                    sSTT = "IV";
                    break;
                case "5":
                    sSTT = "V";
                    break;
                case "6":
                    sSTT = "VI";
                    break;
                case "7":
                    sSTT = "VII";
                    break;
                case "8":
                    sSTT = "VIII";
                    break;
                case "9":
                    sSTT = "IX";
                    break;
                case "10":
                    sSTT = "X";
                    break;
                default:
                    sSTT = "I";
                    break;
            }
            return sSTT;
        }
    }
}
