using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.PrintReport
{
    public class PrintReportLapKeHoachChiViewModel : ViewModelBase
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private ICollectionView _listAgency;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly IBhKhcCheDoBhXhService _bhKhcCheDoBhXhService;
        private readonly IBhKhcCheDoBhXhChiTietService _bhKhcCheDoBhXhChiTietService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly ILog _logger;
        private readonly IDanhMucService _danhMucService;
        private SessionInfo _sessionInfo;
        private readonly IBhBaoCaoGhiChuService _bhGhiChuService;
        #endregion

        #region Property
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private string _typeChuky;
        private string _cap1;
        private string _diaDiem;
        private bool _checkAllAgencies;
        public bool IsExportEnable => Agencies != null && Agencies.Any(x => x.Selected);
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        public override Type ContentType => typeof(PrintReportKeHoachChiCheDoBhXhChiTiet);
        public override string Title => KhcCheckPrintType.KHCBHXHCT.Equals(KhcCheckPrintType) ? "IN BÁO CÁO KẾ HOẠCH CHI CHẾ ĐỘ BHXH" : "DỰ TOÁN CHI CÁC CHẾ ĐỘ BHXH";
        public override string Description => KhcCheckPrintType.KHCBHXHCT.Equals(KhcCheckPrintType) ? "IN BÁO CÁO KẾ HOẠCH CHI CHẾ ĐỘ BHXH" : "DỰ TOÁN CHI CÁC CHẾ ĐỘ BHXH"; public static KhcCheckPrintType KhcCheckPrintType { get; set; }
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
        public bool IsSummary { get; set; }
        public Visibility IsTitleType => IsSummary ? Visibility.Visible : Visibility.Collapsed;
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
        #endregion

        #region View model
        private BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel { get; set; }
        public RelayCommand NoteCommand { get; }
        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        #endregion

        #region Constructor
        public PrintReportLapKeHoachChiViewModel(
            INsDonViService nsDonViService,
            IExportService exportService,
            IBhKhcCheDoBhXhService bhKhcCheDoBhXhService,
            IBhKhcCheDoBhXhChiTietService bhKhcCheDoBhXhChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
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
            _bhKhcCheDoBhXhService = bhKhcCheDoBhXhService;
            _bhKhcCheDoBhXhChiTietService = bhKhcCheDoBhXhChiTietService;
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
            LoadTypeReport();

        }
        #endregion

        #region Load data
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


        private void LoadTypeReport()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Chi tiết đơn vị", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Tổng hợp đơn vị", ValueItem = "2"}
            };

            ItemsTypeReport = new ObservableCollection<ComboboxItem>(data);
            SelectedTypeReport = _itemsTypeReport.ElementAt(0);
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

        private void LoadTitleFirst()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (KhcCheckPrintType.KHCBHXHCT.Equals(KhcCheckPrintType))
            {
                TxtTitleFirst = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : PlanKHCTitle.Title1BaoCaoKHC;
                TxtTitleSecond = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
                TxtTitleThird = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
            }
            else
            {
                TxtTitleFirst = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : PlanKHCTitle.Title1BaoCaoDuToan;
                TxtTitleSecond = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : PlanKHCTitle.Title2BaoCaoDuToan;
                TxtTitleThird = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : PlanKHCTitle.Title3BaoCaoDuToan;
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
                    lstDonVis = _bhKhcCheDoBhXhService.FindByDonViForNamLamViec(yearOfWork).ToList();
                    if (!IsInTheoTongHop)
                        lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                    else
                        lstDonVis = lstDonVis.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();
                }
                else
                {
                    lstDonVis = _bhKhcCheDoBhXhService.FindByDonViForNamLamViec(yearOfWork).ToList();
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
        public void LoadDiaDiem()
        {
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }
        #endregion

        #region Export
        private void OnExport(ExportType exportType)
        {
            if (KhcCheckPrintType.KHCBHXHCT.Equals(KhcCheckPrintType))
            {
                if (SelectedTypeReport != null && SelectedTypeReport.ValueItem == "2")
                {

                    OnPrintReportKhcCdBhXhTongHopDonVi(exportType);
                }
                else
                {
                    OnPrintReportKhcCdBhXhChiTietByID(exportType);
                }

            }
            else
            {
                OnPrintReportKhcTongHopTheoDonVi(exportType);
            }
        }
        private void OnPrintReportKhcTongHopTheoDonVi(ExportType exportType)
        {
            try
            {

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    List<ExportResult> results = new List<ExportResult>();
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var lstDonViChecked = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
                    string templateFileName;
                    string fileNamePrefix;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    int stt = 1;

                    // sum khoi du toan
                    double sumTroCapOmDauDT = 0;
                    double sumTroCapThaiSanDT = 0;
                    double sumTroCapTLLĐNNDT = 0;
                    double sumTroCapHuuTriDT = 0;
                    double sumTroCapPhucVienDT = 0;
                    double sumTroCapXuatNguDT = 0;
                    double sumTroCapThoiViecDT = 0;
                    double sumTroCapTuTuatDT = 0;

                    //  sum khoi hach toan
                    double sumTroCapOmDauHT = 0;
                    double sumTroCapThaiSanHT = 0;
                    double sumTroCapTLLĐNNHT = 0;
                    double sumTroCapHuuTriHT = 0;
                    double sumTroCapPhucVienHT = 0;
                    double sumTroCapXuatNguHT = 0;
                    double sumTroCapThoiViecHT = 0;
                    double sumTroCapTuTuatHT = 0;

                    List<ReportKhcTongHopBHXHQuery> khcMucLucsOrder;
                    khcMucLucsOrder = _bhKhcCheDoBhXhChiTietService.FindChungTuTongHopForDonVi(yearOfWork, lstDonViChecked, KhcBhxhLoaiChungTu.BhxhChungTu).ToList();

                    var lstDonViChungTu = khcMucLucsOrder.Select(x => x.STenDonVi).Distinct().ToList();

                    List<ReportKhcTongHopBHXHCriteria> lstReportKhcTongHopBHXHForDonVi = new List<ReportKhcTongHopBHXHCriteria>();

                    foreach (var item in lstDonViChungTu)
                    {
                        ReportKhcTongHopBHXHCriteria report = new ReportKhcTongHopBHXHCriteria();

                        if (!string.IsNullOrEmpty(item))
                        {
                            report.STT += stt;
                            report.SMota = item;
                            report.IsHangCha = true;
                            lstReportKhcTongHopBHXHForDonVi.Add(report);
                        }

                        report = new ReportKhcTongHopBHXHCriteria();
                        // add khoi du toan

                        report.SMota = KhcCheckHachToanAnDuToan.KhoiDuToan;
                        lstReportKhcTongHopBHXHForDonVi.Add(report);

                        // add khoi hach toan
                        report = new ReportKhcTongHopBHXHCriteria();
                        report.SMota = KhcCheckHachToanAnDuToan.KhoiHoachToan;
                        lstReportKhcTongHopBHXHForDonVi.Add(report);
                        stt++;
                    }

                    foreach (var item in khcMucLucsOrder)
                    {
                        for (int i = 0; i < lstReportKhcTongHopBHXHForDonVi.Count; i++)
                        {
                            if (lstReportKhcTongHopBHXHForDonVi[i].SMota.Equals(item.STenDonVi))
                            {
                                ReportKhcTongHopBHXHCriteria report = new ReportKhcTongHopBHXHCriteria();
                                // khoi du toan

                                if (item.SLNS.Equals(KhcCheckHachToanAnDuToan.SLNSKhoiDuToan))
                                {
                                    report = lstReportKhcTongHopBHXHForDonVi[i + 1];
                                    var reportKhcTongHopKhoiDuToan = ReportKhcTongHopKhoi(item);
                                    report.TroCapOmDau += reportKhcTongHopKhoiDuToan.TroCapOmDau;
                                    report.TroCapThaiSan += reportKhcTongHopKhoiDuToan.TroCapThaiSan;
                                    report.TroCapTLLĐNN += reportKhcTongHopKhoiDuToan.TroCapTLLĐNN;
                                    report.TroCapHuuTri += reportKhcTongHopKhoiDuToan.TroCapHuuTri;
                                    report.TroCapPhucVien += reportKhcTongHopKhoiDuToan.TroCapPhucVien;
                                    report.TroCapXuatNgu += reportKhcTongHopKhoiDuToan.TroCapXuatNgu;
                                    report.TroCapThoiViec += reportKhcTongHopKhoiDuToan.TroCapThoiViec;
                                    report.TroCapTuTuat += reportKhcTongHopKhoiDuToan.TroCapTuTuat;
                                    report.Cong += reportKhcTongHopKhoiDuToan.Cong;
                                }

                                if (item.SLNS.Equals(KhcCheckHachToanAnDuToan.SLNSKhoiHoachToan))
                                {
                                    // khoi khoi hoach toan
                                    report = lstReportKhcTongHopBHXHForDonVi[i + 2];
                                    var reportkhcTongHopKhoiHachToan = ReportKhcTongHopKhoi(item);
                                    report.TroCapOmDau += reportkhcTongHopKhoiHachToan.TroCapOmDau;
                                    report.TroCapThaiSan += reportkhcTongHopKhoiHachToan.TroCapThaiSan;
                                    report.TroCapTLLĐNN += reportkhcTongHopKhoiHachToan.TroCapTLLĐNN;
                                    report.TroCapHuuTri += reportkhcTongHopKhoiHachToan.TroCapHuuTri;
                                    report.TroCapPhucVien += reportkhcTongHopKhoiHachToan.TroCapPhucVien;
                                    report.TroCapXuatNgu += reportkhcTongHopKhoiHachToan.TroCapXuatNgu;
                                    report.TroCapThoiViec += reportkhcTongHopKhoiHachToan.TroCapThoiViec;
                                    report.TroCapTuTuat += reportkhcTongHopKhoiHachToan.TroCapTuTuat;
                                    report.Cong += reportkhcTongHopKhoiHachToan.Cong;
                                }
                            }
                        }
                    }

                    lstReportKhcTongHopBHXHForDonVi.ForAll(x =>
                    {
                        x.TroCapOmDau = x.TroCapOmDau / donViTinh;
                        x.TroCapThaiSan = x.TroCapThaiSan / donViTinh;
                        x.TroCapTLLĐNN = x.TroCapTLLĐNN / donViTinh;
                        x.TroCapHuuTri = x.TroCapHuuTri / donViTinh;
                        x.TroCapTuTuat = x.TroCapTuTuat / donViTinh;
                        x.TroCapPhucVien = x.TroCapPhucVien / donViTinh;
                        x.TroCapThoiViec = x.TroCapThoiViec / donViTinh;
                        x.TroCapXuatNgu = x.TroCapXuatNgu / donViTinh;
                        x.Cong = x.Cong / donViTinh;
                    });

                    if (khcMucLucsOrder.Count > 0)
                    {
                        // sum khoi du toan
                        var listKhoiDuToan = lstReportKhcTongHopBHXHForDonVi.Where(x => x.SMota == KhcCheckHachToanAnDuToan.KhoiDuToan).ToList();
                        sumTroCapOmDauDT = listKhoiDuToan.Sum(x => x.TroCapOmDau);
                        sumTroCapThaiSanDT = listKhoiDuToan.Sum(x => x.TroCapThaiSan);
                        sumTroCapTLLĐNNDT = listKhoiDuToan.Sum(x => x.TroCapTLLĐNN);
                        sumTroCapHuuTriDT = listKhoiDuToan.Sum(x => x.TroCapHuuTri);
                        sumTroCapPhucVienDT = listKhoiDuToan.Sum(x => x.TroCapPhucVien);
                        sumTroCapXuatNguDT = listKhoiDuToan.Sum(x => x.TroCapXuatNgu);
                        sumTroCapThoiViecDT = listKhoiDuToan.Sum(x => x.TroCapThoiViec);
                        sumTroCapTuTuatDT = listKhoiDuToan.Sum(x => x.TroCapTuTuat);

                        // sum khoi hach toan
                        var listKhoiHachToan = lstReportKhcTongHopBHXHForDonVi.Where(x => x.SMota == KhcCheckHachToanAnDuToan.KhoiHoachToan).ToList();
                        sumTroCapOmDauHT = listKhoiHachToan.Sum(x => x.TroCapOmDau);
                        sumTroCapThaiSanHT = listKhoiHachToan.Sum(x => x.TroCapThaiSan);
                        sumTroCapTLLĐNNHT = listKhoiHachToan.Sum(x => x.TroCapTLLĐNN);
                        sumTroCapHuuTriHT = listKhoiHachToan.Sum(x => x.TroCapHuuTri);
                        sumTroCapPhucVienHT = listKhoiDuToan.Sum(x => x.TroCapPhucVien);
                        sumTroCapXuatNguHT = listKhoiHachToan.Sum(x => x.TroCapXuatNgu);
                        sumTroCapThoiViecHT = listKhoiHachToan.Sum(x => x.TroCapThoiViec);
                        sumTroCapTuTuatHT = listKhoiHachToan.Sum(x => x.TroCapTuTuat);
                    }

                    for (int i = 0; i < lstReportKhcTongHopBHXHForDonVi.Count; i++)
                    {
                        if (lstReportKhcTongHopBHXHForDonVi[i].STT > 0)
                        {
                            lstReportKhcTongHopBHXHForDonVi[i].TroCapOmDau = lstReportKhcTongHopBHXHForDonVi[i + 1].TroCapOmDau + lstReportKhcTongHopBHXHForDonVi[i + 2].TroCapOmDau;
                            lstReportKhcTongHopBHXHForDonVi[i].TroCapThaiSan = lstReportKhcTongHopBHXHForDonVi[i + 1].TroCapThaiSan + lstReportKhcTongHopBHXHForDonVi[i + 2].TroCapThaiSan;
                            lstReportKhcTongHopBHXHForDonVi[i].TroCapTLLĐNN = lstReportKhcTongHopBHXHForDonVi[i + 1].TroCapTLLĐNN + lstReportKhcTongHopBHXHForDonVi[i + 2].TroCapTLLĐNN;
                            lstReportKhcTongHopBHXHForDonVi[i].TroCapHuuTri = lstReportKhcTongHopBHXHForDonVi[i + 1].TroCapHuuTri + lstReportKhcTongHopBHXHForDonVi[i + 2].TroCapHuuTri;
                            lstReportKhcTongHopBHXHForDonVi[i].TroCapPhucVien = lstReportKhcTongHopBHXHForDonVi[i + 1].TroCapPhucVien + lstReportKhcTongHopBHXHForDonVi[i + 2].TroCapPhucVien;
                            lstReportKhcTongHopBHXHForDonVi[i].TroCapXuatNgu = lstReportKhcTongHopBHXHForDonVi[i + 1].TroCapXuatNgu + lstReportKhcTongHopBHXHForDonVi[i + 2].TroCapXuatNgu;
                            lstReportKhcTongHopBHXHForDonVi[i].TroCapThoiViec = lstReportKhcTongHopBHXHForDonVi[i + 1].TroCapThoiViec + lstReportKhcTongHopBHXHForDonVi[i + 2].TroCapThoiViec;
                            lstReportKhcTongHopBHXHForDonVi[i].TroCapTuTuat = lstReportKhcTongHopBHXHForDonVi[i + 1].TroCapTuTuat + lstReportKhcTongHopBHXHForDonVi[i + 2].TroCapTuTuat;
                            lstReportKhcTongHopBHXHForDonVi[i].Cong = lstReportKhcTongHopBHXHForDonVi[i + 1].Cong + lstReportKhcTongHopBHXHForDonVi[i + 2].Cong;
                        }
                    }

                    var TongSoTienDT = (sumTroCapOmDauDT + sumTroCapThaiSanDT + sumTroCapTLLĐNNDT
                    + sumTroCapHuuTriDT + sumTroCapXuatNguDT + sumTroCapThoiViecDT
                    + sumTroCapTuTuatDT + sumTroCapPhucVienDT);

                    var TongSoTienHT = (sumTroCapOmDauHT + sumTroCapThaiSanHT + sumTroCapTLLĐNNHT
                    + sumTroCapHuuTriHT + sumTroCapXuatNguHT + sumTroCapThoiViecHT
                    + sumTroCapTuTuatHT + sumTroCapPhucVienHT);

                    var TongSoTien = TongSoTienDT + TongSoTienHT;
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);


                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond + _sessionInfo.YearOfWork);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", lstReportKhcTongHopBHXHForDonVi);

                    data.Add("sumTroCapOmDauDT", sumTroCapOmDauDT);
                    data.Add("sumTroCapThaiSanDT", sumTroCapThaiSanDT);
                    data.Add("sumTroCapTLLĐNNDT", sumTroCapTLLĐNNDT);
                    data.Add("sumTroCapHuuTriDT", sumTroCapHuuTriDT);
                    data.Add("sumTroCapPhucVienDT", sumTroCapPhucVienDT);
                    data.Add("sumTroCapXuatNguDT", sumTroCapXuatNguDT);
                    data.Add("sumTroCapThoiViecDT", sumTroCapThoiViecDT);
                    data.Add("sumTroCapTuTuatDT", sumTroCapTuTuatDT);

                    data.Add("sumTroCapOmDauHT", sumTroCapOmDauHT);
                    data.Add("sumTroCapThaiSanHT", sumTroCapThaiSanHT);
                    data.Add("sumTroCapTLLĐNNHT", sumTroCapTLLĐNNHT);
                    data.Add("sumTroCapHuuTriHT", sumTroCapHuuTriHT);
                    data.Add("sumTroCapPhucVienHT", sumTroCapPhucVienHT);
                    data.Add("sumTroCapXuatNguHT", sumTroCapXuatNguHT);
                    data.Add("sumTroCapThoiViecHT", sumTroCapThoiViecHT);
                    data.Add("sumTroCapTuTuatHT", sumTroCapTuTuatHT);
                    // Tổng cộng
                    data.Add("sumTongCongTroCapOmDau", sumTroCapOmDauDT + sumTroCapOmDauHT);
                    data.Add("sumTongCongTroCapThaiSan", sumTroCapThaiSanDT + sumTroCapThaiSanHT);
                    data.Add("sumTongCongTroCapTLLĐNN", sumTroCapTLLĐNNDT + sumTroCapTLLĐNNHT);
                    data.Add("sumTongCongTroCapHuuTri", sumTroCapHuuTriDT + sumTroCapHuuTriHT);
                    data.Add("sumTongCongTroCapPhucVien", sumTroCapPhucVienDT + sumTroCapPhucVienHT);
                    data.Add("sumTongCongTroCapXuatNgu", sumTroCapXuatNguDT + sumTroCapXuatNguHT);
                    data.Add("sumTongCongTroCapThoiViec", sumTroCapThoiViecDT + sumTroCapThoiViecHT);
                    data.Add("sumTongCongTroCapTuTuat", sumTroCapTuTuatDT + sumTroCapTuTuatHT);
                    data.Add("TongSoTienDT", TongSoTienDT);
                    data.Add("TongSoTienHT", TongSoTienHT);
                    data.Add("DonVi", _sessionInfo.TenDonVi);
                    data.Add("TongTien", TongSoTien);

                    data.Add("TongSoTien", TongSoTien != 0 ? StringUtils.NumberToText((double)TongSoTien * donViTinh, true) : string.Empty);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));

                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHC_CHUNGTU_TONGHOP_PHULUC_KCT_BHXH_DOC));

                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportKhcTongHopBHXHCriteria, DonVi>(templateFileName, data);
                    results.Add(new ExportResult("KẾ HOẠCH CHI BHXH NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        private static ReportKhcTongHopBHXHCriteria ReportKhcTongHopKhoi(ReportKhcTongHopBHXHQuery item)
        {
            ReportKhcTongHopBHXHCriteria reportKhcTongHopBHXHCriteria = new ReportKhcTongHopBHXHCriteria();
            reportKhcTongHopBHXHCriteria.SMota = KhcCheckHachToanAnDuToan.KhoiDuToan;
            reportKhcTongHopBHXHCriteria.IsHangCha = false;

            if (item.SM.Equals(KhcCheckHachToanAnDuToan.TroCapOmDau))
            {
                reportKhcTongHopBHXHCriteria.TroCapOmDau = item.TienKeHoachThucHienNamNay;
            }

            if (item.SM.Equals(KhcCheckHachToanAnDuToan.TroCapThaiSan))
            {
                reportKhcTongHopBHXHCriteria.TroCapThaiSan = item.TienKeHoachThucHienNamNay;
            }

            if (item.SM.Equals(KhcCheckHachToanAnDuToan.TroCapTLLĐNN))
            {
                reportKhcTongHopBHXHCriteria.TroCapTLLĐNN = item.TienKeHoachThucHienNamNay;
            }

            if (item.SM.Equals(KhcCheckHachToanAnDuToan.TroCapHuuTri))
            {
                reportKhcTongHopBHXHCriteria.TroCapHuuTri = item.TienKeHoachThucHienNamNay;
            }

            if (item.SM.Equals(KhcCheckHachToanAnDuToan.TroCapPhucVien))
            {
                reportKhcTongHopBHXHCriteria.TroCapPhucVien = item.TienKeHoachThucHienNamNay;
            }

            if (item.SM.Equals(KhcCheckHachToanAnDuToan.TroCapXuatNgu))
            {
                reportKhcTongHopBHXHCriteria.TroCapXuatNgu = item.TienKeHoachThucHienNamNay;
            }

            if (item.SM.Equals(KhcCheckHachToanAnDuToan.TroCapThoiViec))
            {
                reportKhcTongHopBHXHCriteria.TroCapThoiViec = item.TienKeHoachThucHienNamNay;
            }

            if (item.SM.Equals(KhcCheckHachToanAnDuToan.TroCapTuTuat))
            {
                reportKhcTongHopBHXHCriteria.TroCapTuTuat = item.TienKeHoachThucHienNamNay;
            }

            reportKhcTongHopBHXHCriteria.Cong = reportKhcTongHopBHXHCriteria.TroCapOmDau + reportKhcTongHopBHXHCriteria.TroCapThaiSan
                                               + reportKhcTongHopBHXHCriteria.TroCapTLLĐNN + reportKhcTongHopBHXHCriteria.TroCapHuuTri
                                               + reportKhcTongHopBHXHCriteria.TroCapPhucVien + reportKhcTongHopBHXHCriteria.TroCapXuatNgu
                                               + reportKhcTongHopBHXHCriteria.TroCapThoiViec + reportKhcTongHopBHXHCriteria.TroCapTuTuat;
            return reportKhcTongHopBHXHCriteria;
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

        private void OnPrintReportKhcCdBhXhChiTietByID(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.Id).ToList();
                    string templateFileName;
                    string fileNamePrefix;
                    var menhGia = _catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : "";
                    var yearOfWork = _sessionInfo.YearOfWork;

                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);
                    foreach (var donVi in lstIdDonVi)
                    {
                        DonVi donViChild = _nsDonViService.FindByIdDonVi(donVi, yearOfWork);
                        KhcCheDoBhXhChiTietCriteria searchCondition = new KhcCheDoBhXhChiTietCriteria();
                        searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.IdDonVi = donVi;
                        searchCondition.DonViTinh = donViTinh;
                        var khcMucLucsOrder = _bhKhcCheDoBhXhChiTietService.GetDataDetailVoucher(searchCondition).ToList();
                        var listkhcMucLucsOrders = _mapper.Map<ObservableCollection<BhKhcCheDoBhXhChiTietModel>>(khcMucLucsOrder).ToList();
                        CalculateData(listkhcMucLucsOrders);

                        listkhcMucLucsOrders.ForEach(x =>
                        {
                            x.IsHangCha = x.BHangCha;
                        });

                        listkhcMucLucsOrders = listkhcMucLucsOrders.Where(x => x.IsHasData).ToList();
                        //var lisSum = listkhcMucLucsOrders.Where(x => x.SXauNoiMa == KhcBhxhMLNS.KHOI_DU_TOAN || x.SXauNoiMa == KhcBhxhMLNS.KHOI_HACH_TOAN).ToList();

                        // Ước đã thực hiện năm
                        var SumSoUocThucHienNamTruoc = ConvertNumberToString(listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(item => item.ISoUocThucHienNamTruoc));
                        var SumTienUocThucHienNamTruoc = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(item => item.FTienUocThucHienNamTruoc);

                        // Kế hoạch thực hien nam nay 
                        var SumSoKeHoachThucHienNamNay = ConvertNumberToString(listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(item => item.ISoKeHoachThucHienNamNay));
                        var SumTienKeHoachThucHienNamNay = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(item => item.FTienKeHoachThucHienNamNay);

                        // Sĩ quan
                        var SumSoSQ = ConvertNumberToString(listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(item => item.ISoSQ));
                        var SumTienSQ = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(item => item.FTienSQ);

                        // QNCN
                        var SumSoQNCN = ConvertNumberToString(listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(item => item.ISoQNCN));
                        var SumTienQNCN = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(item => item.FTienQNCN);

                        // CN & VCQP
                        var SumSoCNVQP = ConvertNumberToString(listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(item => item.ISoCNVQP));
                        var SumTienCNVQP = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(item => item.FTienCNVQP);

                        // LĐHĐ
                        var SumSoLDHD = ConvertNumberToString(listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(item => item.ISoLDHD));
                        var SumTienLDHD = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(item => item.FTienLDHD);

                        // HSQ, BS
                        var SumSoHSQBS = ConvertNumberToString(listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(item => item.ISoHSQBS));
                        var SumTienHSQBS = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Sum(item => item.FTienHSQBS);

                        // Total All 
                        var TongSoTien = SumTienKeHoachThucHienNamNay;

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("YearWork", yearOfWork);
                        data.Add("YearWorkOld", yearOfWork - 1);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("ListData", listkhcMucLucsOrders);
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi));
                        data.Add("TieuDeDonVi", "Đơn vị: ");
                        data.Add("DonVi", donViChild.TenDonVi);
                        data.Add("SumSoKeHoachThucHienNamNay", SumSoKeHoachThucHienNamNay);
                        data.Add("SumTienKeHoachThucHienNamNay", SumTienKeHoachThucHienNamNay);
                        data.Add("SumSoUocThucHienNamTruoc", SumSoUocThucHienNamTruoc);
                        data.Add("SumTienUocThucHienNamTruoc", SumTienUocThucHienNamTruoc);
                        data.Add("SumSoSQ", SumSoSQ);
                        data.Add("SumTienSQ", SumTienSQ);
                        data.Add("SumSoQNCN", SumSoQNCN);
                        data.Add("SumTienQNCN", SumTienQNCN);
                        data.Add("SumSoCNVQP", SumSoCNVQP);
                        data.Add("SumTienCNVQP", SumTienCNVQP);
                        data.Add("SumSoLDHD", SumSoLDHD);
                        data.Add("SumTienLDHD", SumTienLDHD);
                        data.Add("SumSoHSQBS", SumSoHSQBS);
                        data.Add("SumTienHSQBS", SumTienHSQBS);
                        data.Add("TongSoTien", TongSoTien != null ? StringUtils.NumberToText((double)TongSoTien * donViTinh, true) : string.Empty);
                        _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork, donVi);
                        AddChuKy(data, _typeChuky);
                        data.Add("ThoiGian", DateUtils.FormatDateReport(ReportDate));

                        data.Add("DonViTinh", menhGia);
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHC_CHUNGTU_CHITIET_PHULUC_KCT_BHXH_DOC));

                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + donVi);
                        var xlsFile = _exportService.Export<BhKhcCheDoBhXhChiTietModel, BhDmMucLucNganSach, BhKhcCheDoBhXhChiTiet>(templateFileName, data);
                        results.Add(new ExportResult("KẾ HOẠCH CHI BHXH NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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

        private void OnPrintReportKhcCdBhXhTongHopDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.Id).ToList();
                    string templateFileName;
                    string fileNamePrefix;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var menhGia = _catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : "";

                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);

                    KhcCheDoBhXhChiTietCriteria searchCondition = new KhcCheDoBhXhChiTietCriteria();
                    searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                    searchCondition.IdDonVi = string.Join(",", lstIdDonVi);
                    searchCondition.DonViTinh = donViTinh;
                    List<BhKhcCheDoBhXhChiTiet> khcMucLucsOrder = new List<BhKhcCheDoBhXhChiTiet>();

                    if (IsInTheoTongHop)
                        khcMucLucsOrder = _bhKhcCheDoBhXhChiTietService.GetDataDetailVoucher(searchCondition).ToList();
                    else
                        khcMucLucsOrder = _bhKhcCheDoBhXhChiTietService.GetDataSummaryDetailVoucher(searchCondition).ToList();

                    var listkhcMucLucsOrders = _mapper.Map<ObservableCollection<BhKhcCheDoBhXhChiTietModel>>(khcMucLucsOrder).ToList();
                    CalculateData(listkhcMucLucsOrders);

                    listkhcMucLucsOrders.ForEach(x =>
                    {
                        x.IsHangCha = x.BHangCha;
                    });

                    listkhcMucLucsOrders = listkhcMucLucsOrders.Where(x => x.IsHasData).ToList();
                    var lisSum = listkhcMucLucsOrders.Where(x => x.SXauNoiMa == KhcBhxhMLNS.KHOI_DU_TOAN || x.SXauNoiMa == KhcBhxhMLNS.KHOI_HACH_TOAN).ToList();

                    // Ước đã thực hiện năm
                    var SumSoUocThucHienNamTruoc = ConvertNumberToString(lisSum.Sum(item => item.ISoUocThucHienNamTruoc));
                    var SumTienUocThucHienNamTruoc = lisSum.Sum(item => item.FTienUocThucHienNamTruoc);

                    // Kế hoạch thực hien nam nay 
                    var SumSoKeHoachThucHienNamNay = ConvertNumberToString(lisSum.Sum(item => item.ISoKeHoachThucHienNamNay));
                    var SumTienKeHoachThucHienNamNay = lisSum.Sum(item => item.FTienKeHoachThucHienNamNay);

                    // Sĩ quan
                    var SumSoSQ = ConvertNumberToString(lisSum.Sum(item => item.ISoSQ));
                    var SumTienSQ = lisSum.Sum(item => item.FTienSQ);

                    // QNCN
                    var SumSoQNCN = ConvertNumberToString(lisSum.Sum(item => item.ISoQNCN));
                    var SumTienQNCN = lisSum.Sum(item => item.FTienQNCN);

                    // CN & VCQP
                    var SumSoCNVQP = ConvertNumberToString(lisSum.Sum(item => item.ISoCNVQP));
                    var SumTienCNVQP = lisSum.Sum(item => item.FTienCNVQP);

                    // LĐHĐ
                    var SumSoLDHD = ConvertNumberToString(lisSum.Sum(item => item.ISoLDHD));
                    var SumTienLDHD = lisSum.Sum(item => item.FTienLDHD);

                    // HSQ, BS
                    var SumSoHSQBS = ConvertNumberToString(lisSum.Sum(item => item.ISoHSQBS));
                    var SumTienHSQBS = lisSum.Sum(item => item.FTienHSQBS);

                    // Total All 
                    var TongSoTien = SumTienKeHoachThucHienNamNay;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("YearWork", yearOfWork);
                    data.Add("YearWorkOld", yearOfWork - 1);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listkhcMucLucsOrders);
                    data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                    data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi));
                    data.Add("TieuDeDonVi", "");
                    data.Add("DonVi", "");
                    data.Add("SumSoKeHoachThucHienNamNay", SumSoKeHoachThucHienNamNay);
                    data.Add("SumTienKeHoachThucHienNamNay", SumTienKeHoachThucHienNamNay);
                    data.Add("SumSoUocThucHienNamTruoc", SumSoUocThucHienNamTruoc);
                    data.Add("SumTienUocThucHienNamTruoc", SumTienUocThucHienNamTruoc);
                    data.Add("SumSoSQ", SumSoSQ);
                    data.Add("SumTienSQ", SumTienSQ);
                    data.Add("SumSoQNCN", SumSoQNCN);
                    data.Add("SumTienQNCN", SumTienQNCN);
                    data.Add("SumSoCNVQP", SumSoCNVQP);
                    data.Add("SumTienCNVQP", SumTienCNVQP);
                    data.Add("SumSoLDHD", SumSoLDHD);
                    data.Add("SumTienLDHD", SumTienLDHD);
                    data.Add("SumSoHSQBS", SumSoHSQBS);
                    data.Add("SumTienHSQBS", SumTienHSQBS);
                    data.Add("IsAggregate", true);
                    data.Add("TongSoTien", TongSoTien != null ? StringUtils.NumberToText((double)TongSoTien * donViTinh, true) : string.Empty);
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                    AddChuKy(data, _typeChuky);
                    data.Add("ThoiGian", DateUtils.FormatDateReport(ReportDate));

                    data.Add("DonViTinh", menhGia);
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHC_CHUNGTU_CHITIET_PHULUC_KCT_BHXH_DOC));

                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + "Tổng hợp đơn vị");
                    var xlsFile = _exportService.Export<BhKhcCheDoBhXhChiTietModel, BhDmMucLucNganSach, BhKhcCheDoBhXhChiTiet>(templateFileName, data);
                    results.Add(new ExportResult("KẾ HOẠCH CHI BHXH NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        private void CalculateData(List<BhKhcCheDoBhXhChiTietModel> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.ISoDaThucHienNamTruoc = 0;
                    x.FTienDaThucHienNamTruoc = 0;
                    x.ISoUocThucHienNamTruoc = 0;
                    x.FTienUocThucHienNamTruoc = 0;
                    //x.ISoKeHoachThucHienNamNay = 0;
                    //x.FTienKeHoachThucHienNamNay = 0;
                    x.ISoSQ = 0;
                    x.FTienSQ = 0;
                    x.ISoQNCN = 0;
                    x.FTienQNCN = 0;
                    x.ISoCNVQP = 0;
                    x.FTienCNVQP = 0;
                    x.ISoLDHD = 0;
                    x.FTienLDHD = 0;
                    x.ISoHSQBS = 0;
                    x.FTienHSQBS = 0;
                });
            var temp = lstSktChungTuChiTiet.Where(x => !x.IsHangCha && !x.IsDeleted);
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, lstSktChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid idParent, BhKhcCheDoBhXhChiTietModel item, List<BhKhcCheDoBhXhChiTietModel> lstSktChungTuChiTiet)
        {
            var dictByMlns = lstSktChungTuChiTiet.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }
            var model = dictByMlns[idParent];
            model.ISoDaThucHienNamTruoc += item.ISoDaThucHienNamTruoc;
            model.FTienDaThucHienNamTruoc += item.FTienDaThucHienNamTruoc;

            model.ISoUocThucHienNamTruoc += item.ISoUocThucHienNamTruoc;
            model.FTienUocThucHienNamTruoc += item.FTienUocThucHienNamTruoc;

            //model.ISoKeHoachThucHienNamNay += item.ISoKeHoachThucHienNamNay;
            //model.FTienKeHoachThucHienNamNay += item.FTienKeHoachThucHienNamNay;

            model.ISoSQ += item.ISoSQ;
            model.FTienSQ += item.FTienSQ;

            model.ISoQNCN += item.ISoQNCN;
            model.FTienQNCN += item.FTienQNCN;

            model.ISoCNVQP += item.ISoCNVQP;
            model.FTienCNVQP += item.FTienCNVQP;

            model.ISoLDHD += item.ISoLDHD;
            model.FTienLDHD += item.FTienLDHD;

            model.ISoHSQBS += item.ISoHSQBS;
            model.FTienHSQBS += item.FTienHSQBS;

            CalculateParent(model.IdParent, item, lstSktChungTuChiTiet);
        }

        private string GetTemplate(string input)
        {
            if (SelectedKieuGiayIn.ValueItem == "1")
                input = input + "_Doc";
            return Path.Combine(ExportPrefix.PATH_BH_KHC, input + FileExtensionFormats.Xlsx);
        }

        public object ConvertNumberToString(object value)
        {
            if (value == null) return null;
            int input = (int)value;
            if (input == 0)
                return null;
            return value;
        }

        private void LoadTypeChuKy()
        {
            switch (KhcCheckPrintType)
            {
                case KhcCheckPrintType.KHCBHXHCT:
                    _typeChuky = TypeChuKy.RPT_BHXH_KHC_CHITIET;
                    break;
                case KhcCheckPrintType.KHCBHXHTH:
                    _typeChuky = TypeChuKy.RPT_BHXH_KHC_TONGHOP;
                    break;
            }
        }
        #endregion

        #region Add chu ky
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
            BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_BHXH_KHC_CHITIET, TypeChuKy.RPT_BHXH_KHC_TONGHOP };
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(Agencies);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuky;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = true;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = false;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
        }

        #endregion
    }
}
