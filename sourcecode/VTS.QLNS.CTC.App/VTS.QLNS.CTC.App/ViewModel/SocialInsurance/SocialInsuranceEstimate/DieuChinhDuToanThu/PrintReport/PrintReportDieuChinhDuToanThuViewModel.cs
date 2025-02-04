using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
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

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu.PrintReport
{
    public class PrintReportDieuChinhDuToanThuViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ICollectionView _donViCollectionView;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly IBhDcDuToanThuService _dcDTTService;
        private readonly IBhDcDuToanThuChiTietService _dcDTTChiTietService;
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
        public static BHXHCheckPrintType BHXHCheckPrintType { get; set; }
        private string _txtTitleFirst;
        private int[] _arrParent = { 1, 10, 25 };
        public bool IsPrintByUnit;
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

        public string name { get; set; }
        private string ReportName
        {
            get
            {
                name = "In chi tiết điều chỉnh DT thu BHXH, BHYT, BHTN";
                return name;
            }
        }

        public override string Name => ReportName;
        public override string Title => ReportName;
        public override string Description => ReportName;

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
        public string DonViChungTu { get; set; }
        public string DonViChaChungTu { get; set; }

        private Guid _dcDttId;
        public Guid DcDttId
        {
            get => _dcDttId;
            set => SetProperty(ref _dcDttId, value);
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
                foreach (var item in ListDonVi)
                    item.IsChecked = _selectAllDonVi;
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
            }
        }

        private ComboboxItem _khoiSelected;
        public ComboboxItem KhoiSelected
        {
            get => _khoiSelected;
            set
            {
                SetProperty(ref _khoiSelected, value);
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
        public bool _isShowVoucherType;
        public bool IsShowVoucherType
        {
            get => _isShowVoucherType;
            set
            {
                SetProperty(ref _isShowVoucherType, value);
            }
        }

        private List<ComboboxItem> _reportTypes;
        public List<ComboboxItem> ReportTypes
        {
            get => _reportTypes;
            set => SetProperty(ref _reportTypes, value);
        }

        private ComboboxItem _selectedReportType;
        public ComboboxItem SelectedReportType
        {
            get => _selectedReportType;
            set
            {
                SetProperty(ref _selectedReportType, value);
                LoadDonVi();
                OnPropertyChanged(nameof(IsEnableAggregate));
                IsInTheoTongHop = false;
            }
        }

        public bool IsEnableAggregate => SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString();

        private BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel { get; set; }
        public RelayCommand NoteCommand { get; }
        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintReportDieuChinhDuToanThuViewModel(INsDonViService nsDonViService,
            IExportService exportService,
            IBhDcDuToanThuService dcDTTService,
            IBhDcDuToanThuChiTietService dcDTTChiTietService,
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
            _dcDTTService = dcDTTService;
            _dcDTTChiTietService = dcDTTChiTietService;
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
            LoadReportType();
            LoadCatUnitTypes();
            LoadTypeChuKy();
            LoadTitleFirst();
            LoadPaperPrintTypes();
            LoadKieuGiayIn();
            LoadDiaDiem();
            LoadDonVi();
            IsContainBVTCChecked = true;
        }

        private void LoadReportType()
        {
            _reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencyDetail.ToString() },
                new ComboboxItem { DisplayItem = "Chi tiết đơn vị theo đối tượng", ValueItem = SummaryLNSReportType.AgencySummaryRank.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp - chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencySummaryDetail.ToString() }
            };
            _selectedReportType = _reportTypes.First();
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
            }
        }
        public void LoadDiaDiem()
        {
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        public void LoadDonVi()
        {
            var listChungTu = _mapper.Map<List<BhDcDuToanThuModel>>(_dcDTTService.FindByYearOfWord(_sessionService.Current.YearOfWork));

            if (IsInTheoTongHop && SelectedReportType.ValueItem != SummaryLNSReportType.AgencySummaryDetail.ToString())
            {
                listChungTu = listChungTu.Where(x => x.IIDMaDonVi.Equals(_sessionService.Current.IdDonVi) && x.ILoaiTongHop == KhcKcbBhxhLoaiChungTu.BhxhChungTuTongHop).ToList();
            }
            else if (!IsInTheoTongHop)
            {
                listChungTu = listChungTu.Where(x => !x.IIDMaDonVi.Equals(_sessionService.Current.IdDonVi) && x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTu).ToList();
            }
            else if (IsInTheoTongHop && SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryDetail.ToString())
            {
                listChungTu = listChungTu.Where(x => !x.IIDMaDonVi.Equals(_sessionService.Current.IdDonVi) && x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTu
                && x.BDaTongHop == true).ToList();
            }

            var listDonViCheckBox = listChungTu.Select(item => new CheckBoxItem
            {
                ValueItem = item.IIDMaDonVi,
                DisplayItem = string.Join("-", item.IIDMaDonVi, item.STenDonVi),
                NameItem = item.STenDonVi,
                IsChecked = item.STenDonVi.Equals(DonViChungTu)
            }).OrderBy(item => item.ValueItem);

            ListDonVi = new ObservableCollection<CheckBoxItem>(listDonViCheckBox);

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

        private void LoadTypeChuKy()
        {
            _typeChuky = TypeChuKy.RPT_BHXH_DT_DC_DTT_CHITIET;
        }
        public void LoadTitleFirst()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TxtTitleFirst = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
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
        
        public void OnExport(ExportType exportType)
        {
            List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
            var lstSelectedUnit = string.Join(",", lstDonViSelected.Select(x => x.ValueItem.ToString()).ToList());

            if (lstDonViSelected.Count() <= 0)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }

            if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
            {
                OnPrintReportByAgencyDetail(exportType, lstDonViSelected);
            }
            else if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryRank.ToString())
            {
                OnPrintReportByRank(exportType, lstDonViSelected);
            }
            else if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryDetail.ToString())
            {
                OnPrintReportByAgencySummaryDetail(exportType, lstDonViSelected);
            }
        }
        public void OnPrintReportKhtBhxtTheoDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    BhDcDuToanThuChiTietCriteria searchCondition = new BhDcDuToanThuChiTietCriteria();
                    searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                    searchCondition.BhDcDuToanThuId = DcDttId;
                    searchCondition.DonViTinh = donViTinh;
                    var lstExportData = _dcDTTChiTietService.FindBhDcDttChiTietByCondition(searchCondition).ToList();
                    lstExportData.Select(x => { x.Tang = x.FTang; x.Giam = x.FGiam; return x; }).ToList();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();

                    foreach (var item in lstExportData)
                    {
                        item.DttDauNam = Math.Round(item.DttDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                        item.Dtt6ThangDauNam = Math.Round(item.Dtt6ThangDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                        item.Dtt6ThangCuoiNam = Math.Round(item.Dtt6ThangCuoiNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                        item.Tang = Math.Round(item.Tang.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                        item.Giam = Math.Round(item.Giam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                        item.TongCong = Math.Round(item.TongCong.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    }

                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DonViIn", DonViChungTu);
                    data.Add("ListData", lstExportData);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", $"{TxtTitleFirst} NĂM {_sessionInfo.YearOfWork}");
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonVi", DonViChungTu);
                    data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("TotalDuocGiao", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.DttDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                    data.Add("TotalDtt6ThangDauNam", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Dtt6ThangDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                    data.Add("TotalDtt6ThangCuoiNam", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Dtt6ThangCuoiNam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                    data.Add("Total", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => x.TongCong));
                    data.Add("TotalTang", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Tang.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                    data.Add("TotalGiam", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Giam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                    AddChuKy(data, _typeChuky);

                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_DC_DU_TOAN_THU_DOC));
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<BhDcDuToanThuChiTietModel, BhDcDuToanThuChiTiet, RptDcDuToanThuChiTietQuery>(templateFileName, data);
                    results.Add(new ExportResult("ĐIỀU CHỈNH DỰ TOÁN THU BHXH, BHYT, BHTH NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        public void OnPrintReportByAgencyDetail(ExportType exportType, List<CheckBoxItem> lstSelectedUnit)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    
                    foreach (var dv in lstSelectedUnit)
                    {
                        var lstExportData = _dcDTTChiTietService.FindBhDcDttChiTietByUnit(dv.ValueItem, donViTinh, yearOfWork, IsInTheoTongHop).ToList();
                        lstExportData.Select(x => { x.Tang = x.FTang; x.Giam = x.FGiam; return x; }).ToList();
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        CurrencyToText currencyToText = new CurrencyToText();
                        var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();

                        if (lstExportData.Any())
                        {
                            foreach (var item in lstExportData)
                            {
                                item.DttDauNam = Math.Round(item.DttDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.Dtt6ThangDauNam = Math.Round(item.Dtt6ThangDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.Dtt6ThangCuoiNam = Math.Round(item.Dtt6ThangCuoiNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.Tang = Math.Round(item.Tang.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.Giam = Math.Round(item.Giam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.TongCong = Math.Round(item.TongCong.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            }
                        }

                        data.Add("currencyToText", currencyToText);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("ListData", lstExportData);
                        data.Add("DonViIn", dv.NameItem);
                        data.Add("TieuDe1", $"{TxtTitleFirst} NĂM {_sessionInfo.YearOfWork}");
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("h1", h1);
                        data.Add("h2", "");
                        data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("DonVi", _sessionInfo.TenDonVi);
                        data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                        data.Add("TotalDuocGiao", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.DttDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                        data.Add("TotalDtt6ThangDauNam", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Dtt6ThangDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                        data.Add("TotalDtt6ThangCuoiNam", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Dtt6ThangCuoiNam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                        data.Add("Total", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.TongCong.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                        data.Add("TotalTang", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Tang.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                        data.Add("TotalGiam", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Giam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                        if (IsInTheoTongHop)
                            data.Add("IsShowUnit", false);
                        else
                            data.Add("IsShowUnit", true);
                        AddChuKy(data, _typeChuky);
                        if (IsInTheoTongHop)
                            _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                        else
                            _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork, dv.ValueItem);

                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_DC_DU_TOAN_THU_DOC));
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                        var xlsFile = _exportService.Export<BhDcDuToanThuChiTietModel, BhDcDuToanThuChiTiet, RptDcDuToanThuChiTietQuery>(templateFileName, data);
                        results.Add(new ExportResult("ĐIỀU CHỈNH DỰ TOÁN THU BHXH, BHYT, BHTH NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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

        public void OnPrintReportByRank(ExportType exportType, List<CheckBoxItem> lstSelectedUnit)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    foreach (var dv in lstSelectedUnit)
                    {
                        var lstExportData = _dcDTTChiTietService.ExportDieuChinhDtTheoDoiTuong(dv.ValueItem, donViTinh, yearOfWork, IsInTheoTongHop).ToList();
                        lstExportData.Select(x => { x.Tang = x.FTang; x.Giam = x.FGiam; return x; }).ToList();
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        CurrencyToText currencyToText = new CurrencyToText();
                        var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();

                        foreach (var item in lstExportData)
                        {
                            item.DttDauNam = Math.Round(item.DttDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.Dtt6ThangDauNam = Math.Round(item.Dtt6ThangDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.Dtt6ThangCuoiNam = Math.Round(item.Dtt6ThangCuoiNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.Tang = Math.Round(item.Tang.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.Giam = Math.Round(item.Giam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.TongCong = Math.Round(item.TongCong.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                        }

                        data.Add("currencyToText", currencyToText);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("ListData", lstExportData);
                        data.Add("DonViIn", dv.NameItem);
                        data.Add("TieuDe1", $"{TxtTitleFirst} NĂM {_sessionInfo.YearOfWork}");
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("h1", h1);
                        data.Add("h2", "");
                        data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("DonVi", _sessionInfo.TenDonVi);
                        data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                        data.Add("TotalDuocGiao", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.DttDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                        data.Add("TotalDtt6ThangDauNam", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Dtt6ThangDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                        data.Add("TotalDtt6ThangCuoiNam", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Dtt6ThangCuoiNam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                        data.Add("Total", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.TongCong.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                        data.Add("TotalTang", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Tang.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                        data.Add("TotalGiam", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Giam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                        if (IsInTheoTongHop)
                            data.Add("IsShowUnit", false);
                        else
                            data.Add("IsShowUnit", true);
                        AddChuKy(data, _typeChuky);
                        if (IsInTheoTongHop)
                            _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                        else
                            _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork, dv.ValueItem);

                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_DC_DU_TOAN_THU_DOC));
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                        var xlsFile = _exportService.Export<BhDcDuToanThuChiTietModel, BhDcDuToanThuChiTiet, RptDcDuToanThuChiTietQuery>(templateFileName, data);
                        results.Add(new ExportResult("ĐIỀU CHỈNH DỰ TOÁN THU BHXH, BHYT, BHTH NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (exportType == ExportType.PDF)
                        {
                            exportType = ExportType.PDF_ONE_PAPER;
                        }
                        else
                        {
                            exportType = ExportType.EXCEL_ONE_PAPER;
                        }
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

        public void OnPrintReportByAgencySummaryDetail(ExportType exportType, List<CheckBoxItem> lstSelectedUnit)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var selectedUnits = string.Join(",", lstSelectedUnit.Select(x => x.ValueItem.ToString()).ToList());

                    var lstExportData = _dcDTTChiTietService.FindBhDcDttChiTietByAgencySummaryDetail(selectedUnits, donViTinh, yearOfWork).ToList();
                    lstExportData.Select(x => { x.Tang = x.FTang; x.Giam = x.FGiam; return x; }).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();

                    foreach (var item in lstExportData)
                    {
                        item.DttDauNam = Math.Round(item.DttDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                        item.Dtt6ThangDauNam = Math.Round(item.Dtt6ThangDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                        item.Dtt6ThangCuoiNam = Math.Round(item.Dtt6ThangCuoiNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                        item.Tang = Math.Round(item.Tang.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                        item.Giam = Math.Round(item.Giam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                        item.TongCong = Math.Round(item.TongCong.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    }

                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", lstExportData);
                    data.Add("DonViIn", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", $"{TxtTitleFirst} NĂM {_sessionInfo.YearOfWork}");
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonVi", _sessionInfo.TenDonVi);
                    data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("TotalDuocGiao", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.DttDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                    data.Add("TotalDtt6ThangDauNam", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Dtt6ThangDauNam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                    data.Add("TotalDtt6ThangCuoiNam", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Dtt6ThangCuoiNam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                    data.Add("Total", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.TongCong.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                    data.Add("TotalTang", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Tang.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                    data.Add("TotalGiam", lstExportData.Where(x => _arrParent.Contains(x.STT)).Sum(x => Math.Round(x.Giam.GetValueOrDefault(), MidpointRounding.AwayFromZero)));
                    data.Add("IsShowUnit", false);
                    AddChuKy(data, _typeChuky);
                    if (IsInTheoTongHop)
                        _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                    else
                        _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork, _sessionInfo.TenDonVi);

                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_DC_DU_TOAN_THU_DOC));
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<BhDcDuToanThuChiTietModel, BhDcDuToanThuChiTiet, RptDcDuToanThuChiTietQuery>(templateFileName, data);
                    results.Add(new ExportResult("ĐIỀU CHỈNH DỰ TOÁN THU BHXH, BHYT, BHTH NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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
        }
        public string GetTemplate(string input)
        {
            if (SelectedKieuGiayIn.ValueItem == "1")
                input = input + "_Doc";
            return Path.Combine(ExportPrefix.PATH_BH_DC_DTT, input + FileExtensionFormats.Xlsx);
        }

        private void OnNoteCommand()
        {
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();
            BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_BHXH_DT_DC_DTT_CHITIET };
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(ListDonVi);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuky;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = true;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = false;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
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
    }
}
