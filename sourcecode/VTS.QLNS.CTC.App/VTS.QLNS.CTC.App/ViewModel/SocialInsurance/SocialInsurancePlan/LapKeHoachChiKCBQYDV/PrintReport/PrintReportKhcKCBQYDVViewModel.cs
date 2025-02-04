using AutoMapper;
using FlexCel.Core;
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
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV.PrintReport
{
    public class PrintReportKhcKCBQYDVViewModel : ViewModelBase
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private ICollectionView _listAgency;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly IBhKhcKcbService _bhkhcKcbService;
        private readonly IBhKhcKcbChiTietService _bhKhcKcbChiTietService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly ILog _logger;
        private readonly IDanhMucService _danhMucService;
        private IBhBaoCaoGhiChuService _bhGhiChuService;
        private SessionInfo _sessionInfo;
        private string _diaDiem;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private string _typeChuky;
        #endregion

        #region Property
        public override Type ContentType => typeof(PrintReportKeHoachChiKCBQYDVChiTiet);
        public override string Title => KhcKcbCheckPrintType.KHCKCBBHXHCT.Equals(KhcKcbCheckType) ? "IN KẾ HOẠCH CHI KCB QUÂN Y ĐƠN VỊ" : "DỰ TOÁN CHI KINH PHÍ KCB";
        public override string Description => KhcKcbCheckPrintType.KHCKCBBHXHCT.Equals(KhcKcbCheckType) ? "IN KẾ HOẠCH CHI KCB QUÂN Y ĐƠN VỊ" : "DỰ TOÁN CHI KINH PHÍ KCB";
        public static KhcKcbCheckPrintType KhcKcbCheckType { get; set; }
        private string _cap1;
        private bool _checkAllAgencies;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        public bool IsExportEnable => Agencies != null && Agencies.Any(x => x.Selected);
        private string _txtTitleFirst;
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

        private string _txtGhiChu;
        public string TxtGhiChu
        {
            get => _txtGhiChu;
            set
            {
                SetProperty(ref _txtGhiChu, value);
            }
        }

        public bool IsShowInTheoTongHop => IsSummary;
        public bool IsSummary { get; set; }

        public bool IsLoaiKCB { get; set; }
        public bool IsEnabled => !IsSummary;

        private ObservableCollection<ComboboxItem> _paperPrintTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> PaperPrintTypes
        {
            get => _paperPrintTypes;
            set => SetProperty(ref _paperPrintTypes, value);
        }

        private bool _isEnabledUnit;
        public bool IsEnabledUnit
        {
            get => _isEnabledUnit;
            set => SetProperty(ref _isEnabledUnit, value);
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
        #region list agency
        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private string _searchAgencyText;
        public string SearchAgencyText
        {
            get => _searchAgencyText;
            set
            {
                if (SetProperty(ref _searchAgencyText, value))
                {
                    _listAgency.Refresh();
                }
            }
        }

        public string SelectedAgencyCount
        {
            get
            {
                int totalCount = 0;
                int totalSelected = 0;
                if (_agencies != null)
                {
                    totalCount = Agencies != null ? Agencies.Where(x => x.IsFilter).Count() : 0;
                    totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                }
                return string.Format(SELECTED_AGENCY_COUNT_STR, totalSelected, totalCount);
            }
        }
        private bool _isSelectedAllAgency;
        public bool IsSelectedAllAgency
        {
            get => Agencies != null && Agencies.Where(x => x.IsFilter).All(x => x.Selected);
            set
            {
                SetProperty(ref _isSelectedAllAgency, value);
                _checkAllAgencies = true;
                foreach (AgencyModel item in Agencies)
                {
                    item.Selected = _isSelectedAllAgency;
                    OnPropertyChanged(nameof(IsExportEnable));
                }
                _checkAllAgencies = false;
                OnPropertyChanged(nameof(SelectedAgencyCount));
            }
        }
        #endregion

        private bool _isInTheoTongHop;
        public bool IsInTheoTongHop
        {
            get => _isInTheoTongHop;
            set
            {
                SetProperty(ref _isInTheoTongHop, value);
                LoadDonVi();
            }
        }

        public bool IsShowTheoTongHop { get; set; }
        private BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel { get; set; }
        #endregion

        #region RelayCommand
        public RelayCommand NoteCommand { get; }
        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        #endregion

        #region Constructor
        public PrintReportKhcKCBQYDVViewModel(
            INsDonViService nsDonViService,
            IExportService exportService,
            IBhKhcKcbService bhKhcKcbService,
            IBhKhcKcbChiTietService bhKhcKcbChiTietService,
            IDmChuKyService dmChuKyService,
            ISessionService sessionService,
            IDanhMucService danhMucService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IMapper mapper,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IBhBaoCaoGhiChuService bhBaoCaoGhiChuService,
            BhBaoCaoGhiChuDialogViewModel bhBaoCaoGhiChuDialogViewModel)
        {
            _bhkhcKcbService = bhKhcKcbService;
            _bhKhcKcbChiTietService = bhKhcKcbChiTietService;

            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _logger = logger;
            _mapper = mapper;
            _dmChuKyService = dmChuKyService;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _bhGhiChuService = bhBaoCaoGhiChuService;
            BhBaoCaoGhiChuDialogViewModel = bhBaoCaoGhiChuDialogViewModel;
            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportPdfActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());

        }
        #endregion

        #region Init
        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            _isInTheoTongHop = false;
            InitReportDefaultDate();
            LoadDonVi();
            LoadTypeChuKy();
            LoadTitleFirst();
            LoadCatUnitTypes();
            LoadDiaDiem();
            //LoadPaperPrintTypes();
            LoadKieuGiayIn();
        }
        #endregion

        #region Load data
        public void LoadDiaDiem()
        {
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }
        private void LoadTitleFirst()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (KhcKcbCheckType == KhcKcbCheckPrintType.KHCKCBBHXHCT)
            {
                TxtTitleFirst = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : PlanKHCTitle.Title1BaoCaoKHCKCBQY;
                TxtTitleSecond = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
                TxtTitleThird = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
            }
            else
            {
                TxtTitleFirst = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : PlanKHCTitle.Title1BaoCaoDuToanKHCKCBQY;
                TxtTitleSecond = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : PlanKHCTitle.Title2BaoCaoDuToanKHCKCBQY;
                TxtTitleThird = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : PlanKHCTitle.Title3BaoCaoDuToan;
            }
        }

        private void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(1);
        }

        private void LoadCatUnitTypes()
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

            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
        }

        private void LoadTypeChuKy()
        {

            switch (KhcKcbCheckType)
            {
                case KhcKcbCheckPrintType.KHCKCBBHXHCT:
                    _typeChuky = TypeChuKy.RPT_BHXH_KHC_KCB_QYDV_CHITIET;
                    break;
                case KhcKcbCheckPrintType.KHCKCBBHXHTH:
                    _typeChuky = TypeChuKy.RPT_BHXH_KHC_KCB_QYDV_PHUlUC;
                    break;
            }
        }

        private void LoadDonVi()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                var yearOfWork = _sessionInfo.YearOfWork;

                List<DonVi> lstDonVis = new List<DonVi>();
                if (IsShowTheoTongHop)
                {
                    lstDonVis = _bhkhcKcbService.FindByDonViForNamLamViec(yearOfWork).ToList();
                    if (!IsInTheoTongHop)
                        lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                    else
                        lstDonVis = lstDonVis.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();
                }
                else
                {
                    lstDonVis = _bhkhcKcbService.FindByDonViForNamLamViec(yearOfWork).ToList();
                    lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                }

                e.Result = lstDonVis.OrderBy(x => x.IIDMaDonVi).ToList();
            }, (s, e) =>
            {
                if (e.Result != null)
                {
                    List<DonVi> agencies = (List<DonVi>)e.Result;
                    _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(agencies);
                }
                else
                    _agencies = new ObservableCollection<AgencyModel>();
                _listAgency = CollectionViewSource.GetDefaultView(_agencies);
                _listAgency.Filter = ListAgencyFilter;
                foreach (var model in Agencies)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(AgencyModel.Selected) && !_checkAllAgencies)
                        {
                            OnPropertyChanged(nameof(SelectedAgencyCount));
                            OnPropertyChanged(nameof(IsSelectedAllAgency));
                            OnPropertyChanged(nameof(IsExportEnable));
                        }
                    };
                }

                OnPropertyChanged(nameof(Agencies));
                OnPropertyChanged(nameof(IsSelectedAllAgency));
                OnPropertyChanged(nameof(SelectedAgencyCount));
                OnPropertyChanged(nameof(IsExportEnable));
                IsLoading = false;
            });
        }

        private bool ListAgencyFilter(object obj)
        {
            bool result = true;
            var item = (AgencyModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchAgencyText))
                result = item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        #endregion

        #region OnConfigSign
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
        #endregion

        #region Note
        private void OnNoteCommand()
        {
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();
            BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_BHXH_KHC_KCB_QYDV_CHITIET, TypeChuKy.RPT_BHXH_KHC_KCB_QYDV_PHUlUC };
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(Agencies);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuky;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = true;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = false;
            if (IsInTheoTongHop)
            {
                BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = false;
                BhBaoCaoGhiChuDialogViewModel.IsAgregate = true;
            }
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
        }

        #endregion

        #region Export
        private void OnExport(ExportType pDF)
        {
            switch (KhcKcbCheckType)
            {
                case KhcKcbCheckPrintType.KHCKCBBHXHCT:
                    OnPrintReportKhcKcbChiTiet(pDF);
                    break;
                case KhcKcbCheckPrintType.KHCKCBBHXHTH:
                    OnPrintReportKhcKcbTongHopTheoDonVi(pDF);
                    break;
            }
        }

        private void OnPrintReportKhcKcbTongHopTheoDonVi(ExportType exportType)
        {
            try
            {

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstDonViChecked = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
                    string templateFileName;
                    string fileNamePrefix;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    int stt = 1;
                    List<ReportKhcKcbBHXHQuery> khcMucLucsOrder = new List<ReportKhcKcbBHXHQuery>();
                    khcMucLucsOrder = _bhKhcKcbChiTietService.FindChungTuTongHopForDonVi(yearOfWork, lstDonViChecked, KhcQlBhxhLoaiChungTu.BhxhChungTuTongHop).ToList();
                    var lstDonVi = khcMucLucsOrder.Select(x => x.IDDonVi).Distinct().ToList();
                    List<ReportKhcKcbBHXHQuery> lstReportKhcKcbTongHopForDonVi = new List<ReportKhcKcbBHXHQuery>();

                    foreach (var item in lstDonVi)
                    {
                        ReportKhcKcbBHXHQuery report = new ReportKhcKcbBHXHQuery();
                        report.STT += stt;
                        report.STenDonVi = item;
                        report.IsHangCha = false;
                        lstReportKhcKcbTongHopForDonVi.Add(report);

                        stt++;
                    }

                    foreach (var item in khcMucLucsOrder)
                    {
                        foreach (var reportKhcTongHopDonVi in lstReportKhcKcbTongHopForDonVi)
                        {
                            if (item.IDDonVi.Equals(reportKhcTongHopDonVi.STenDonVi))
                            {
                                reportKhcTongHopDonVi.FCong = item.TienKeHoachThucHienNamNayDT + item.TienKeHoachThucHienNamNayHT + reportKhcTongHopDonVi.FCong;
                            }
                        }
                    }

                    var TongSoTien = lstReportKhcKcbTongHopForDonVi.Sum(x => x.FCong);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    lstReportKhcKcbTongHopForDonVi.ForAll(x =>
                    {
                        x.TienKeHoachThucHienNamNayHT /= donViTinh;
                        x.TienKeHoachThucHienNamNayDT /= donViTinh;
                        x.FCong /= donViTinh;
                        x.TienDaThucHienNamTruocHT /= donViTinh;
                        x.TienDaThucHienNamTruocDT /= donViTinh;
                        x.TienUocThucHienNamTruocDT /= donViTinh;
                        x.TienUocThucHienNamTruocHT /= donViTinh;
                    });

                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond + "  " + _sessionInfo.YearOfWork);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ghichu", TxtGhiChu);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DonVi", _sessionInfo.TenDonVi);
                    data.Add("ListData", lstReportKhcKcbTongHopForDonVi);
                    data.Add("Cong", TongSoTien / donViTinh);
                    data.Add("TongSoTien", TongSoTien != 0 ? StringUtils.NumberToText((double)TongSoTien, true) : string.Empty);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                    data.Add("SKTML", lstReportKhcKcbTongHopForDonVi);
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHC_KCB_CHUNGTU_TONGHOP_PHULUC_DOC));
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportKhcKcbBHXHQuery, DonVi>(templateFileName, data);
                    var nameRange = xlsFile.GetNamedRange(1);
                    nameRange.Comment = "Workbook";
                    xlsFile.SetNamedRange(nameRange);
                    xlsFile.SetNamedRange(new TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                    xlsFile.SetCellValue(50, 50, "CheckSum");
                    xlsFile.SetRowHidden(50, true);
                    results.Add(new ExportResult("KẾ HOẠCH CHI KHÁM CHỮ BỆNH QUÂN Y ĐƠN VỊ " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        private string GetLevelTitle(DmChuKy dmChuKy, int level)
        {
            if (dmChuKy == null) return string.Empty;
            var loaiDVBanHanh = dmChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty;
            var danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

            return loaiDVBanHanh switch
            {
                LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => string.Empty,
                LoaiDonViBanHanh.TUY_CHINH => dmChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
        }

        private void OnPrintReportKhcKcbChiTiet(ExportType pDF)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstDonVi = Agencies.Where(x => x.Selected).ToList();
                    string templateFileName;
                    string fileNamePrefix;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);

                    foreach (var donVi in lstDonVi)
                    {
                        double? SumTienDaThucHienNamTruoc = 0;
                        double? SumTienUocThucHienNamTruoc = 0;
                        double? SumTienKeHoachThucHienNamNay = 0;
                        KhcKcbChiTietCriteria searchCondition = new KhcKcbChiTietCriteria();
                        searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.IdDonVi = donVi.Id;
                        searchCondition.DonViTinh = donViTinh;
                        var khcMucLucsOrder = _bhKhcKcbChiTietService.GetDataDetailVoucher(searchCondition);
                        var listkhcMucLucsOrders = _mapper.Map<ObservableCollection<BhKhcKcbChiTietModel>>(khcMucLucsOrder).ToList();
                        SumTienDaThucHienNamTruoc = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(x => x.FTienUocThucHienNamTruoc);

                        // Ước đã thực hiện năm
                        SumTienUocThucHienNamTruoc = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(x => x.FTienUocThucHienNamTruoc);

                        // Kế hoạch thực hien nam nay 
                        SumTienKeHoachThucHienNamNay = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(x => x.FTienKeHoachThucHienNamNay);

                        CalculateData(listkhcMucLucsOrders);

                        listkhcMucLucsOrders = listkhcMucLucsOrders.Where(x => x.IsDataNotNull).ToList();
                        var listSumMucLuc = listkhcMucLucsOrders.Where(x => x.SXauNoiMa.Equals(KhcKcbBhxhMLNS.TRUONG_SA_DK_KHOI_DU_TOAN)
                                                                    || x.SXauNoiMa.Equals(KhcKcbBhxhMLNS.TRUONG_SA_DK_KHOI_HACH_TOAN)).ToList();

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, pDF);
                        // Total All 
                        var TongSoTien = SumTienKeHoachThucHienNamNay * donViTinh;

                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);

                        data.Add("YearWork", yearOfWork);
                        data.Add("YearWorkOld", yearOfWork - 1);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("ListData", listkhcMucLucsOrders);

                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonViReportHeader);
                        data.Add("DonVi", donVi.TenDonVi);

                        data.Add("SumTienKeHoachThucHienNamNay", SumTienKeHoachThucHienNamNay);
                        data.Add("SumTienUocThucHienNamTruoc", SumTienUocThucHienNamTruoc);
                        data.Add("SumTienDaThucHienNamTruoc", SumTienDaThucHienNamTruoc);

                        data.Add("TienBangChu", TongSoTien != null ? StringUtils.NumberToText((double)TongSoTien, true) : string.Empty);

                        AddChuKy(data, _typeChuky);
                        data.Add("ThoiGian",  DateUtils.FormatDateReport(ReportDate));
                        if (IsInTheoTongHop)
                            _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                        else
                            _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork, donVi.IIDMaDonVi);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHC_KCB_CHUNGTU_CHITIET_PHULUC_DOC));

                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + donVi);
                        var xlsFile = _exportService.Export<BhKhcKcbChiTietModel, BhDmMucLucNganSach, BhKhcKcbChiTiet>(templateFileName, data);
                        results.Add(new ExportResult("DỰ TOÁN CHI KHÁM CHỮ BỆNH QUÂN Y ĐƠN VỊ BHXH, BHYT NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, pDF);
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

        private void CalculateData(List<BhKhcKcbChiTietModel> listkhcMucLucsOrders)
        {
            listkhcMucLucsOrders.Where(x => x.IsHangCha).Select(x =>
            {
                x.FTienDaThucHienNamTruoc = 0;
                x.FTienKeHoachThucHienNamNay = 0;
                x.FTienUocThucHienNamTruoc = 0;
                return x;
            }).ToList();

            var temp = listkhcMucLucsOrders.Where(x => !x.IsHangCha && !x.IsDeleted);

            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, listkhcMucLucsOrders);
            }
        }

        private void CalculateParent(Guid idParent, BhKhcKcbChiTietModel item, List<BhKhcKcbChiTietModel> listkhcMucLucsOrders)
        {
            var dictByMlns = listkhcMucLucsOrders.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienDaThucHienNamTruoc += item.FTienDaThucHienNamTruoc;
            model.FTienKeHoachThucHienNamNay += item.FTienKeHoachThucHienNamNay;
            model.FTienUocThucHienNamTruoc += item.FTienUocThucHienNamTruoc;

            CalculateParent(model.IdParent, item, listkhcMucLucsOrders);
        }

        private string GetTemplate(string input)
        {
            if (SelectedKieuGiayIn.ValueItem == "1")
                input = input + "_Doc";
            return Path.Combine(ExportPrefix.PATH_BH_KHC_KCB, input + FileExtensionFormats.Xlsx);
        }

        private void AddChuKy(Dictionary<string, object> data, string typeChuky)
        {
            //add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
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
        #endregion
    }
}
