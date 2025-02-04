
using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
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
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT.PrintReport
{
    public class PrintReportKhtmBhytViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ICollectionView _donViCollectionView;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly IKhtmBHYTService _khtmBHYTService;
        private readonly IKhtmBHYTChiTietService _khtmBHYTChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IBhBaoCaoGhiChuService _bhGhiChuService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private readonly IDanhMucService _danhMucService;
        private string _diaDiem;
        private string _typeChuky;
        private bool _isInTheoTongHop;
        public static BHYTCheckPrintType BHYTCheckPrintType { get; set; }
        private string _txtTitleFirst;
        public string name { get; set; }
        public int ReportNameTypeValue;
        private string ReportName
        {
            get
            {
                switch (ReportNameTypeValue)
                {
                    case (int)BHYTCheckPrintType.BHYT_DETAIL:
                        name = "In báo cáo kế hoạch thu mua BHYT thân nhân";
                        break;
                    case (int)BHYTCheckPrintType.BHYT_THAN_NHAN:
                        name = "In dự toán thu BHYT thân nhân";
                        break;
                    case (int)BHYTCheckPrintType.BHYT_HSSV:
                        name = "In dự toán thu BHYT HSSV";
                        break;
                }
                return name;
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

        private ObservableCollection<ComboboxItem> _itemsTypeReport = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ItemsTypeReport
        {
            get => _itemsTypeReport;
            set => SetProperty(ref _itemsTypeReport, value);
        }

        private ComboboxItem _selectedTypeReport;

        public ComboboxItem SelectedTypeReport
        {
            get => _selectedTypeReport;
            set => SetProperty(ref _selectedTypeReport, value);
        }

        public string DonViChungTu { get; set; }
        public string DonViChaChungTu { get; set; }

        private Guid _khtmBhytId;
        public Guid KhtmBhytId
        {
            get => _khtmBhytId;
            set => SetProperty(ref _khtmBhytId, value);
        }
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
                OnPropertyChanged(nameof(LabelSelectedCountDonVi));
                OnPropertyChanged(nameof(SelectAllDonVi));
            }
        }
        public bool IsShowInTheoTongHop => BHYTCheckPrintType.BHYT_DETAIL.Equals(BHYTCheckPrintType) && _sessionService.Current.IsQuanLyDonViCha;
        private BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel { get; set; }
        public RelayCommand NoteCommand { get; }
        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintReportKhtmBhytViewModel(INsDonViService nsDonViService,
            IExportService exportService,
            IKhtmBHYTService khtmBHYTService,
            IKhtmBHYTChiTietService khtmBHYTChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IDmChuKyService dmChuKyService,
            ISessionService sessionService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IMapper mapper,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IDanhMucService danhMucService,
            IBhBaoCaoGhiChuService bhBaoCaoGhiChuService,
            BhBaoCaoGhiChuDialogViewModel bhBaoCaoGhiChuDialogViewModel)
        {
            _sessionService = sessionService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _mapper = mapper;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _khtmBHYTService = khtmBHYTService;
            _khtmBHYTChiTietService = khtmBHYTChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _dmChuKyService = dmChuKyService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _danhMucService = danhMucService;
            _bhGhiChuService = bhBaoCaoGhiChuService;
            BhBaoCaoGhiChuDialogViewModel = bhBaoCaoGhiChuDialogViewModel;

            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            ExportPdfActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
        }
        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            _isInTheoTongHop = false;
            InitReportDefaultDate();
            Clear();
            LoadDonVi();
            LoadCatUnitTypes();
            LoadTypeChuKy();
            LoadTitleFirst();
            LoadTypeReport();
            LoadPaperPrintTypes();
            LoadKieuGiayIn();
            LoadDiaDiem();
            IsContainBVTCChecked = true;
        }

        private void LoadTypeReport()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Chi tiết đơn vị", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Tổng hợp đơn vị", ValueItem = "2"},
                new ComboboxItem {DisplayItem = "Tổng hợp - chi tiết đơn vị", ValueItem = "3"}
            };

            ItemsTypeReport = new ObservableCollection<ComboboxItem>(data);
            SelectedTypeReport = _itemsTypeReport.ElementAt(0);
        }

        private bool _isEnabledUnit;
        public bool IsEnabledUnit
        {
            get => _isEnabledUnit;
            set => SetProperty(ref _isEnabledUnit, value);
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
            ListDonVi = LoadDonViKhtmBHYTChiTiet();
            IsEnabledUnit = true;

            // Filter
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
            switch (BHYTCheckPrintType)
            {
                case BHYTCheckPrintType.BHYT_DETAIL:
                    _typeChuky = TypeChuKy.RPT_BHYT_KHTM_CHITIET;
                    break;
                case BHYTCheckPrintType.BHYT_THAN_NHAN:
                    _typeChuky = TypeChuKy.RPT_BHYT_KHTM_THAN_NHAN;
                    break;
                case BHYTCheckPrintType.BHYT_HSSV:
                    _typeChuky = TypeChuKy.RPT_BHYT_KHTM_HSSV;
                    break;
            }
        }
        public void LoadTitleFirst()
        {
            if (ReportNameTypeValue == (int)BHYTCheckPrintType.BHYT_DETAIL || ReportNameTypeValue == 0)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                TxtTitleFirst = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultKHTMReportTitle.TITLE_1;
                TxtTitleSecond = _dmChuKy?.TieuDe2MoTa ?? string.Empty;
                TxtTitleThird = _dmChuKy?.TieuDe3MoTa ?? string.Empty;
            }
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
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(1);
        }
        public void Clear()
        {
            _donViCollectionView = null;
        }
        private ObservableCollection<CheckBoxItem> LoadDonViKhtmBHYTChiTiet()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var lstIdDonVi = GetListIdDonViChungTuBHYTDuocXem();
            if (lstIdDonVi != null && lstIdDonVi.Count > 0)
            {
                var predicateDv = PredicateBuilder.True<DonVi>();
                predicateDv = predicateDv.And(x => x.NamLamViec == yearOfWork);
                predicateDv = predicateDv.And(x => lstIdDonVi.Contains(x.IIDMaDonVi));
                var lstDonVi = _nsDonViService.FindByCondition(predicateDv);
                var result = lstDonVi.Select(item => new CheckBoxItem
                {
                    ValueItem = item.IIDMaDonVi,
                    DisplayItem = string.Join("-", item.IIDMaDonVi, item.TenDonVi),
                    NameItem = item.TenDonVi
                }).OrderBy(item => item.ValueItem);
                return new ObservableCollection<CheckBoxItem>(result);
            }

            return new ObservableCollection<CheckBoxItem>();
        }
        private List<string> GetListIdDonViChungTuBHYTDuocXem()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            IEnumerable<BhKhtmBHYTQuery> listChungTuBHYT;
            if (BHYTCheckPrintType.BHYT_DETAIL.Equals(BHYTCheckPrintType) && IsInTheoTongHop)
            {
                listChungTuBHYT = _khtmBHYTService.FindChungTuChiTietTongHopByCondition(yearOfWork, BhxhLoaiChungTu.BhxhChungTuTongHop, false, _sessionService.Current.Principal).ToList();
            }
            else
            {
                listChungTuBHYT = _khtmBHYTService.FindChungTuChiTietDonVi(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, _sessionService.Current.Principal).ToList();
            }

            var lstIdDonVi = listChungTuBHYT.Select(x => x.IIDMaDonVi).Distinct().ToList();
            return lstIdDonVi;
        }
        public void OnExport(ExportType exportType)
        {
            List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
            var lstSelectedUnit = string.Join(",", lstDonViSelected.Select(x => x.ValueItem.ToString()).ToList());

            if (BHYTCheckPrintType.BHYT_DETAIL.Equals(BHYTCheckPrintType))
            {
                if (ListDonVi.Where(item => item.IsChecked).Count() <= 0)
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                else
                {
                    if (SelectedTypeReport != null && SelectedTypeReport.ValueItem == "2")
                    {
                        OnPrintReportKhtBhytTongHopDonVi(exportType);
                    }
                    else if (SelectedTypeReport.ValueItem == "1")
                    {
                        OnPrintReportKhtBhytTheoDonVi(exportType);
                    }
                    else
                    {
                        OnPrintReportKhtBhytTheoTongHopDonViChiTiet(exportType);
                    }
                }
            }
            else
            {
                if (ListDonVi.Where(item => item.IsChecked).Count() <= 0)
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                else
                {
                    OnPrintReportKhtmDuToanThuBhyt(exportType, lstSelectedUnit);
                }
            }
        }
        public void OnPrintReportKhtBhytTheoDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                    foreach (var dv in lstDonViSelected)
                    {
                        DonViChungTu = dv.NameItem;
                        string templateFileName;
                        string fileNamePrefix;
                        var h1 = _catUnitTypeSelected.DisplayItem;
                        var yearOfWork = _sessionInfo.YearOfWork;
                        KhtmBHYTChiTietCriteria searchCondition = new KhtmBHYTChiTietCriteria();
                        searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.khtmBhytId = KhtmBhytId;
                        searchCondition.IsPrintReport = true;
                        searchCondition.MaDonVi = dv.ValueItem;
                        var lstKhtmChungTuChiTiet = _khtmBHYTChiTietService.FindBhKhtmBHYTChiTietByCondition(searchCondition).ToList();
                        var lstExportData = _mapper.Map<ObservableCollection<BhKhtmBHYTChiTietQuery>>(lstKhtmChungTuChiTiet).ToList();
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        CurrencyToText currencyToText = new CurrencyToText();
                        var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                        CalculateDataByCurrency(lstExportData, donViTinh);
                        CalculateData(lstExportData);
                        foreach (var item in lstExportData)
                        {
                            item.ISoNguoi = (item.ISoNguoi == 0) ? null : item.ISoNguoi;
                            item.ISoThang = (item.ISoThang == 0) ? null : item.ISoThang;
                            item.FDinhMuc = (item.FDinhMuc == 0) ? null : item.FDinhMuc;
                            item.FThanhTien = (item.FThanhTien == 0) ? null : item.FThanhTien;
                        }
                        var tongThanhTien = lstExportData.Where(x => !x.IsHangCha).Sum(x => x.FThanhTien) ?? 0;
                        data.Add("currencyToText", currencyToText);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("ListData", lstExportData);
                        data.Add("Cap1", _sessionInfo.TenDonVi);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("h1", h1);
                        data.Add("h2", "");
                        data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("DonVi", _sessionInfo.TenDonVi);
                        data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                        data.Add("DonViChungTu", DonViChungTu);
                        data.Add("TongSoNguoi", lstExportData.Where(x => !x.IsHangCha).Sum(x => x.ISoNguoi));
                        data.Add("TongSoThang", lstExportData.Where(x => !x.IsHangCha).Sum(x => x.ISoThang));
                        data.Add("TongDinhMuc", lstExportData.Where(x => !x.IsHangCha).Sum(x => x.FDinhMuc));
                        data.Add("TongThanhTien", Math.Round(tongThanhTien, 0));
                        data.Add("NamChungTu", lstExportData.Select(x => x.INamLamViec).FirstOrDefault());
                        if (IsInTheoTongHop)
                        {
                            data.Add("IsAggregate", true);
                            data.Add("SoThangDisplay", "Tổng số tháng");
                            data.Add("SoNguoiDisplay", "Tổng số người");
                            _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                        }
                        else
                        {
                            data.Add("SoThangDisplay", "Số tháng");
                            data.Add("SoNguoiDisplay", "Số người");
                            _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork, dv.ValueItem);

                        }
                        AddChuKy(data, _typeChuky);

                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHTM_BHYT_ChungTu_ChiTiet_BC));
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                        var xlsFile = _exportService.Export<BhKhtmBHYTChiTietQuery>(templateFileName, data);
                        results.Add(new ExportResult("KẾ HOẠCH THU MUA BHYT " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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


        public void OnPrintReportKhtBhytTheoTongHopDonViChiTiet(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    List<string> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem).ToList();
                    string templateFileName;
                    string fileNamePrefix;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    KhtmBHYTChiTietCriteria searchCondition = new KhtmBHYTChiTietCriteria();
                    searchCondition.NamLamViec = yearOfWork;
                    searchCondition.khtmBhytId = KhtmBhytId;
                    searchCondition.IsPrintReport = true;
                    searchCondition.DonViTinh = donViTinh;

                    List<BhKhtmBHYTChiTietQuery> lstKhtmChungTuChiTiet = new List<BhKhtmBHYTChiTietQuery>();
                    if (IsInTheoTongHop)
                    {
                        searchCondition.MaDonVi = lstDonViSelected.FirstOrDefault();
                        lstKhtmChungTuChiTiet = _khtmBHYTChiTietService.FindBhKhtmBHYTTongHopChiTietByCondition(searchCondition).ToList();
                    }
                    else
                    {
                        searchCondition.MaDonVi = string.Join(",", lstDonViSelected);
                        lstKhtmChungTuChiTiet = _khtmBHYTChiTietService.FindBhKhtmBHYTTongHopChiTietByCondition(searchCondition).ToList();
                    }
                    //var lstExportData = _mapper.Map<ObservableCollection<BhKhtmBHYTChiTietQuery>>(lstKhtmChungTuChiTiet).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, yearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    CalculateDataByCurrency(lstKhtmChungTuChiTiet, donViTinh);
                    var lstExportData = lstKhtmChungTuChiTiet;
                    CalculateData(lstKhtmChungTuChiTiet.Where(x => x.Type == 0).ToList());
                    foreach (var item in lstExportData)
                    {
                        item.ISoNguoi = (item.ISoNguoi == 0) ? null : item.ISoNguoi;
                        item.ISoThang = (item.ISoThang == 0) ? null : item.ISoThang;
                        item.FDinhMuc = (item.FDinhMuc == 0) ? null : item.FDinhMuc;
                        item.FThanhTien = (item.FThanhTien == 0) ? null : item.FThanhTien;
                    }
                    var tongThanhTien = lstExportData.Where(x => !x.IsHangCha).Sum(x => x.FThanhTien) ?? 0;
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", lstExportData);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonVi", _sessionInfo.TenDonVi);
                    data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("DonViChungTu", "");
                    data.Add("TongSoNguoi", lstExportData.Where(x => !x.IsHangCha).Sum(x => x.ISoNguoi));
                    data.Add("TongSoThang", lstExportData.Where(x => !x.IsHangCha).Sum(x => x.ISoThang));
                    data.Add("TongDinhMuc", lstExportData.Where(x => !x.IsHangCha).Sum(x => x.FDinhMuc));
                    data.Add("TongThanhTien", Math.Round(tongThanhTien, 0));
                    data.Add("NamChungTu", lstExportData.Select(x => x.INamLamViec).FirstOrDefault());
                    if (IsInTheoTongHop)
                    {
                        data.Add("IsAggregate", true);
                        data.Add("SoThangDisplay", "Tổng số tháng");
                        data.Add("SoNguoiDisplay", "Tổng số người");
                    }
                    else
                    {
                        data.Add("IsAggregate", true);
                        data.Add("SoThangDisplay", "Số tháng");
                        data.Add("SoNguoiDisplay", "Số người");
                    }
                    AddChuKy(data, _typeChuky);

                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHTM_BHYT_ChungTu_ChiTiet_BC));
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<BhKhtmBHYTChiTietQuery>(templateFileName, data);
                    results.Add(new ExportResult("KẾ HOẠCH THU MUA BHYT " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        public void OnPrintReportKhtBhytTongHopDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    List<string> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem).ToList();
                    string templateFileName;
                    string fileNamePrefix;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    KhtmBHYTChiTietCriteria searchCondition = new KhtmBHYTChiTietCriteria();
                    searchCondition.NamLamViec = yearOfWork;
                    searchCondition.khtmBhytId = KhtmBhytId;
                    searchCondition.IsPrintReport = true;

                    List<BhKhtmBHYTChiTiet> lstKhtmChungTuChiTiet = new List<BhKhtmBHYTChiTiet>();
                    if (IsInTheoTongHop)
                    {
                        searchCondition.MaDonVi = lstDonViSelected.FirstOrDefault();
                        lstKhtmChungTuChiTiet = _khtmBHYTChiTietService.FindBhKhtmBHYTChiTietByCondition(searchCondition).ToList();
                    }
                    else
                    {
                        searchCondition.MaDonVi = string.Join(",", lstDonViSelected);
                        lstKhtmChungTuChiTiet = _khtmBHYTChiTietService.FindBhKhtmBHYTReportByCondition(searchCondition).ToList();
                    }
                    var lstExportData = _mapper.Map<ObservableCollection<BhKhtmBHYTChiTietQuery>>(lstKhtmChungTuChiTiet).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, yearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    CalculateDataByCurrency(lstExportData, donViTinh);
                    CalculateData(lstExportData);
                    foreach (var item in lstExportData)
                    {
                        item.ISoNguoi = (item.ISoNguoi == 0) ? null : item.ISoNguoi;
                        item.ISoThang = (item.ISoThang == 0) ? null : item.ISoThang;
                        item.FDinhMuc = (item.FDinhMuc == 0) ? null : item.FDinhMuc;
                        item.FThanhTien = (item.FThanhTien == 0) ? null : item.FThanhTien;
                    }
                    var tongThanhTien = lstExportData.Where(x => !x.IsHangCha).Sum(x => x.FThanhTien) ?? 0;
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", lstExportData);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonVi", _sessionInfo.TenDonVi);
                    data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("DonViChungTu", "");
                    data.Add("TongSoNguoi", lstExportData.Where(x => !x.IsHangCha).Sum(x => x.ISoNguoi));
                    data.Add("TongSoThang", lstExportData.Where(x => !x.IsHangCha).Sum(x => x.ISoThang));
                    data.Add("TongDinhMuc", lstExportData.Where(x => !x.IsHangCha).Sum(x => x.FDinhMuc));
                    data.Add("TongThanhTien", Math.Round(tongThanhTien, 0));
                    data.Add("NamChungTu", lstExportData.Select(x => x.INamLamViec).FirstOrDefault());
                    if (IsInTheoTongHop)
                    {
                        data.Add("SoThangDisplay", "Tổng số tháng");
                        data.Add("SoNguoiDisplay", "Tổng số người");
                    }
                    else
                    {
                        data.Add("SoThangDisplay", "Số tháng");
                        data.Add("SoNguoiDisplay", "Số người");
                    }
                    data.Add("IsAggregate", true);
                    AddChuKy(data, _typeChuky);

                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHTM_BHYT_ChungTu_ChiTiet_BC));
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<BhKhtmBHYTChiTietQuery>(templateFileName, data);
                    results.Add(new ExportResult("KẾ HOẠCH THU MUA BHYT " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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
        public void OnPrintReportKhtmDuToanThuBhyt(ExportType exportType, string lstSelectedUnit)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = "";
                    string fileNamePrefix;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    KhtmBHYTChiTietCriteria searchCondition = new KhtmBHYTChiTietCriteria();
                    searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                    searchCondition.khtmBhytId = KhtmBhytId;
                    List<ReportKhtmDuToanBHYTQuery> lstBHYT = new List<ReportKhtmDuToanBHYTQuery>();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    if (BHYTCheckPrintType.BHYT_THAN_NHAN.Equals(BHYTCheckPrintType))
                    {
                        lstBHYT = _khtmBHYTChiTietService.FindKhtmDuToanThuBhytThanNhan(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, BhxhLoaiChungTu.BhxhDaTongHop,
                        lstSelectedUnit, BhxhMLNS.THU_MUA_BHYT_TNQN, BhxhMLNS.THU_MUA_BHYT_CNVQP, BhxhMLNS.SM_DU_TOAN, BhxhMLNS.SM_HACH_TOAN, donViTinh).ToList();
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHTM_Du_Toan_Thu_BHYT_Than_Nhan));
                        data.Add("TongTNQNDuToan", lstBHYT.Sum(x => x.TNQNDuToan));
                        data.Add("TongTNCNVQPDuToan", lstBHYT.Sum(x => x.TNCNVQPDuToan));
                        data.Add("TongCongDuToan", lstBHYT.Sum(x => x.TongDuToan));
                        data.Add("TongTNQNHachToan", lstBHYT.Sum(x => x.TNQNHachToan));
                        data.Add("TongTNCNVQPHachToan", lstBHYT.Sum(x => x.TNCNVQPHachToan));
                        data.Add("TongCongHachToan", lstBHYT.Sum(x => x.TongHachToan));
                        data.Add("TongCong", lstBHYT.Sum(x => x.TongCongThanNhan));
                    }
                    if (BHYTCheckPrintType.BHYT_HSSV.Equals(BHYTCheckPrintType))
                    {
                        lstBHYT = _khtmBHYTChiTietService.FindKhtmDuToanThuBhytHSSV(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, BhxhLoaiChungTu.BhxhDaTongHop,
                        lstSelectedUnit, BhxhMLNS.SLNS_HSSV, BhxhMLNS.SLNS_LUU_HS, BhxhMLNS.SLNS_HVQS, BhxhMLNS.SLNS_SQ_DU_BI, donViTinh).ToList();
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHTM_Du_Toan_Thu_BHYT_HSSV));
                        data.Add("TongHSSV", lstBHYT.Sum(x => x.HSSV));
                        data.Add("TongLuuHS", lstBHYT.Sum(x => x.LuuHS));
                        data.Add("TongHSSVLuuHS", lstBHYT.Sum(x => x.TongHSSV));
                        data.Add("TongHVQS", lstBHYT.Sum(x => x.HVQS));
                        data.Add("TongSQDuBi", lstBHYT.Sum(x => x.SQDuBi));
                        data.Add("TongCong", lstBHYT.Sum(x => x.TongCongHSSV));
                    }
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", lstBHYT);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonVi", DonViChungTu);
                    data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    AddChuKy(data, _typeChuky);
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                    int stt = 1;
                    foreach (var i in lstBHYT)
                    {
                        i.STT = stt++;
                    }

                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<ReportKhtmDuToanBHYTQuery>(templateFileName, data);
                    results.Add(new ExportResult("KẾ HOẠCH THU MUA BHYT " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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
        private void CalculateDataByCurrency(List<BhKhtmBHYTChiTietQuery> lstKhtmChungTuChiTiet, int dvt)
        {
            foreach (var item in lstKhtmChungTuChiTiet)
            {
                item.DHeSoLCS = item.DHeSoLCS.GetValueOrDefault();
                item.FDinhMuc = (!item.IsHangCha ? (double)(item.DHeSoBHYT.GetValueOrDefault() * item.DHeSoLCS.GetValueOrDefault()) : item.FDinhMuc.GetValueOrDefault());
                item.FThanhTien = item.FThanhTien.GetValueOrDefault() / dvt;
            }

        }
        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy?.ThuaLenh1MoTa ?? string.Empty);
            data.Add("ChucDanh1", dmChyKy?.ChucDanh1MoTa ?? string.Empty);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy?.Ten1MoTa ?? string.Empty);
            data.Add("ThuaLenh2", dmChyKy?.ThuaLenh2MoTa ?? string.Empty);
            data.Add("ChucDanh2", dmChyKy?.ChucDanh2MoTa ?? string.Empty);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy?.Ten2MoTa ?? string.Empty);
            data.Add("ThuaLenh3", dmChyKy?.ThuaLenh3MoTa ?? string.Empty);
            data.Add("ChucDanh3", dmChyKy?.ChucDanh3MoTa ?? string.Empty);
            data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten3", dmChyKy?.Ten3MoTa ?? string.Empty);
            data.Add("ChucDanh4", dmChyKy?.ChucDanh4MoTa ?? string.Empty);
            data.Add("ChucDanh5", dmChyKy?.ChucDanh5MoTa ?? string.Empty);
            data.Add("ThuaLenh4", dmChyKy?.ThuaLenh4MoTa ?? string.Empty);
            data.Add("ThuaLenh5", dmChyKy?.ThuaLenh5MoTa ?? string.Empty);
            data.Add("Ten4", dmChyKy?.Ten4MoTa ?? string.Empty);
            data.Add("Ten5", dmChyKy?.Ten5MoTa ?? string.Empty);
        }
        public string GetTemplate(string input)
        {
            if (SelectedKieuGiayIn.ValueItem == "1")
                input = input + "_Doc";
            return Path.Combine(ExportPrefix.PATH_BH_KHTM, input + FileExtensionFormats.Xlsx);
        }
        private void CalculateData(List<BhKhtmBHYTChiTietQuery> lstKhtmChungTuChiTiet)
        {
            lstKhtmChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.ISoNguoi = 0;
                    if (IsInTheoTongHop)
                    {
                        x.ISoThang = 0;
                        x.FDinhMuc = 0;
                    }
                    x.FThanhTien = 0;
                    return x;
                }).ToList();
            var temp = lstKhtmChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, lstKhtmChungTuChiTiet);
            }

        }
        private void CalculateParent(Guid? idParent, BhKhtmBHYTChiTietQuery item, List<BhKhtmBHYTChiTietQuery> lstKhtChungTuChiTiet)
        {
            var model = lstKhtChungTuChiTiet.FirstOrDefault(x => x.IIDNoiDung == idParent);
            if (model == null) return;
            model.ISoNguoi += item.ISoNguoi;
            if (IsInTheoTongHop)
            {
                model.ISoThang += item.ISoThang;
                //model.FDinhMuc += item.FDinhMuc;
            }
            model.FThanhTien += item.FThanhTien;
            CalculateParent(model.IdParent, item, lstKhtChungTuChiTiet);
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
            if (BHYTCheckPrintType.BHYT_DETAIL.Equals(BHYTCheckPrintType))
            {
                DmChuKyDialogViewModel.HasAddedSign4 = true;
                DmChuKyDialogViewModel.HasAddedSign5 = true;
            }
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TxtTitleFirst = chuKy.TieuDe1MoTa;
                TxtTitleSecond = chuKy.TieuDe2MoTa;
                TxtTitleThird = chuKy.TieuDe3MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void OnNoteCommand()
        {
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();
            BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_BHYT_KHTM_CHITIET, TypeChuKy.RPT_BHYT_KHTM_HSSV, TypeChuKy.RPT_BHYT_KHTM_THAN_NHAN };
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(ListDonVi);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuky;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = true;
            if (IsInTheoTongHop)
                BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = false;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = true;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
        }

    }
}

