using AutoMapper;
using ControlzEx.Standard;
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
using static VTS.QLNS.CTC.Utility.Enum.BaoHiemDuToanTypeEnum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu.PrintReport
{
    public class PrintReportKhtBhxhViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ICollectionView _donViCollectionView;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly IKhtBHXHService _khtBHXHService;
        private readonly IKhtBHXHChiTietService _khtBHXHChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IDmChuKyService _dmChuKyService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private readonly IDanhMucService _danhMucService;
        private readonly IBhBaoCaoGhiChuService _bhGhiChuService;
        private string _diaDiem;

        private string _typeChuky;
        private bool _isInTheoTongHop;
        public static BHXHCheckPrintType BHXHCheckPrintType { get; set; }
        private string _txtTitleFirst;
        public string name { get; set; }
        public int ReportNameTypeValue;
        private string ReportName
        {
            get
            {
                switch (ReportNameTypeValue)
                {
                    case (int)BHXHCheckPrintType.KE_HOACH_THU_BHXH_BHYT_BHTN:
                        name = "In báo cáo kế hoạch thu BHXH, BHYT, BHTN";
                        break;
                    case (int)BHXHCheckPrintType.DU_TOAN_THU_CHI_TONG_HOP:
                        name = "In tổng hợp dự toán thu, chi BHXH, BHYT, BHTN năm";
                        break;
                    case (int)BHXHCheckPrintType.PHU_LUC_II:
                        name = "In dự toán thu BHXH";
                        break;
                    case (int)BHXHCheckPrintType.PHU_LUC_III:
                        name = "In dự toán thu BHTN";
                        break;
                    case (int)BHXHCheckPrintType.PHU_LUC_IV:
                        name = "In dự toán thu BHYT Quân nhân";
                        break;
                    case (int)BHXHCheckPrintType.PHU_LUC_V:
                        name = "In dự toán thu BHYT NLĐ";
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
        public string MaDonViChungTu { get; set; }
        public string DonViChaChungTu { get; set; }

        private Guid _khtBhxhId;
        public Guid KhtBhxhId
        {
            get => _khtBhxhId;
            set => SetProperty(ref _khtBhxhId, value);
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
                LoadTypeChuKy();
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
        private BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel { get; set; }
        public RelayCommand NoteCommand { get; }
        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintReportKhtBhxhViewModel(INsDonViService nsDonViService,
            IExportService exportService,
            IKhtBHXHService khtBHXHService,
            IKhtBHXHChiTietService khtBHXHChiTietService,
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
            _khtBHXHService = khtBHXHService;
            _khtBHXHChiTietService = khtBHXHChiTietService;
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
            LoadDonVi();
            LoadCatUnitTypes();
            LoadTypeChuKy();
            LoadPaperPrintTypes();
            LoadKieuGiayIn();
            LoadDiaDiem();
            IsContainBVTCChecked = true;
        }

        public bool IsShowInTheoTongHop => BHXHCheckPrintType.KE_HOACH_THU_BHXH_BHYT_BHTN.Equals(BHXHCheckPrintType) && _sessionService.Current.IsQuanLyDonViCha;
        public bool IsEnableAggregate => BHXHCheckPrintType.KE_HOACH_THU_BHXH_BHYT_BHTN.Equals(BHXHCheckPrintType)
            && (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString() || SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryRank.ToString());
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
            ListDonVi = LoadDonViKhtBHXHChiTiet();
            //IsEnabledUnit = true;

            // Filter
            _donViCollectionView = CollectionViewSource.GetDefaultView(ListDonVi);
            _donViCollectionView.Filter = obj => string.IsNullOrWhiteSpace(_searchDonVi)
                                                 || (obj is CheckBoxItem item &&
                                                     item.DisplayItem.Contains(_searchDonVi));

            foreach (var org in ListDonVi)
            {
                if (org.ValueItem == MaDonViChungTu)
                {
                    org.IsChecked = true;
                }
                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountDonVi));
                    OnPropertyChanged(nameof(SelectAllDonVi));
                };
            }
        }

        private void LoadReportType()
        {
            _reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencyDetail.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() }
            };

            if (BHXHCheckPrintType.KE_HOACH_THU_BHXH_BHYT_BHTN.Equals(BHXHCheckPrintType))
            {
                _reportTypes.Add(new ComboboxItem { DisplayItem = "Tổng hợp đơn vị - Nhóm theo đối tượng", ValueItem = SummaryLNSReportType.AgencySummaryRank.ToString() });
            }

            _selectedReportType = _reportTypes.First();
        }

        private void LoadTypeChuKy()
        {
            switch (BHXHCheckPrintType)
            {
                case BHXHCheckPrintType.KE_HOACH_THU_BHXH_BHYT_BHTN:
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_BHXH_KHT_CHITIET) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    _typeChuky = TypeChuKy.RPT_BHXH_KHT_CHITIET;
                    TxtTitleFirst = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultKHTReportTitle.KHT_DETAIL_TITLE_1;
                    TxtTitleSecond = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
                    TxtTitleThird = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
                    break;
                case BHXHCheckPrintType.PHU_LUC_II:
                    _typeChuky = TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHXH;
                    break;
                case BHXHCheckPrintType.PHU_LUC_III:
                    _typeChuky = TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHTN;
                    break;
                case BHXHCheckPrintType.PHU_LUC_IV:
                    _typeChuky = TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHYT_QUAN_NHAN;
                    break;
                case BHXHCheckPrintType.PHU_LUC_V:
                    _typeChuky = TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHYT_NLD;
                    break;
                case BHXHCheckPrintType.DU_TOAN_THU_CHI_TONG_HOP:
                    _typeChuky = TypeChuKy.RPT_BHXH_KHTC_TONG_HOP;
                    TxtTitleFirst = $"Tổng hợp dự toán thu, chi BHXH, BHYT, BHTN năm {DateTime.Now.Year}";
                    TxtTitleSecond = $"(Kèm theo quyết định số: ......../QĐ-BQP ngày ...../...../...... của Bộ trưởng Bộ Quốc phòng)";
                    break;
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
        
        private ObservableCollection<CheckBoxItem> LoadDonViKhtBHXHChiTiet()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var lstIdDonVi = GetListIdDonViChungTuBHXHDuocXem();
            if (lstIdDonVi != null && lstIdDonVi.Count > 0)
            {
                var predicateDv = PredicateBuilder.True<DonVi>();
                predicateDv = predicateDv.And(x => x.NamLamViec == yearOfWork);
                predicateDv = predicateDv.And(x => lstIdDonVi.Contains(x.IIDMaDonVi));
                var lstDonVi = _nsDonViService.FindByCondition(predicateDv);

                if (BHXHCheckPrintType.DU_TOAN_THU_CHI_TONG_HOP.Equals(BHXHCheckPrintType))
                {
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        lstDonVi = lstDonVi.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                    }
                }

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
        private List<string> GetListIdDonViChungTuBHXHDuocXem()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            IEnumerable<BhKhtBHXHQuery> listChungTuBHXH;
            if (BHXHCheckPrintType.KE_HOACH_THU_BHXH_BHYT_BHTN.Equals(BHXHCheckPrintType) && IsInTheoTongHop
                && SelectedReportType.ValueItem != SummaryLNSReportType.AgencySummaryRank.ToString())
            {
                listChungTuBHXH = _khtBHXHService.FindChungTuChiTietTongHopByCondition(yearOfWork, BhxhLoaiChungTu.BhxhChungTuTongHop
                    , _sessionService.Current.Principal).ToList();
            }
            else if (BHXHCheckPrintType.KE_HOACH_THU_BHXH_BHYT_BHTN.Equals(BHXHCheckPrintType) && !IsInTheoTongHop
                && SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryRank.ToString())
            {
                var lstVoucher = _khtBHXHService.FindByVoucherType(yearOfWork);
                listChungTuBHXH = _mapper.Map<ObservableCollection<BhKhtBHXHQuery>>(lstVoucher).ToList();
            }
            else if (BHXHCheckPrintType.KE_HOACH_THU_BHXH_BHYT_BHTN.Equals(BHXHCheckPrintType) && IsInTheoTongHop
                && SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryRank.ToString())
            {
                var lstVoucher = _khtBHXHService.FindByVoucherAggregateType(yearOfWork);
                listChungTuBHXH = _mapper.Map<ObservableCollection<BhKhtBHXHQuery>>(lstVoucher).ToList();
            }
            else
            {
                listChungTuBHXH = _khtBHXHService.FindChungTuChiTietByCondition(yearOfWork, false).ToList();
            }

            var lstIdDonVi = listChungTuBHXH.Select(x => x.IID_MaDonVi).Distinct().ToList();
            return lstIdDonVi;
        }
        public void OnExport(ExportType exportType)
        {
            List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
            var lstSelectedUnit = string.Join(",", lstDonViSelected.Select(x => x.ValueItem.ToString()).ToList());
            if (BHXHCheckPrintType.KE_HOACH_THU_BHXH_BHYT_BHTN.Equals(BHXHCheckPrintType))
            {
                if (!ListDonVi.Any(item => item.IsChecked) && IsEnabledUnit)
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                else if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                {
                    OnPrintVoucherDetail(exportType);
                }
                else if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString())
                {
                    OnPrintVoucherDetailAggregate(exportType);
                }
                else if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryRank.ToString())
                {
                    OnPrintVoucherDetailAggregateByRank(exportType);
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
                    if (BHXHCheckPrintType.PHU_LUC_II.Equals(BHXHCheckPrintType) || BHXHCheckPrintType.PHU_LUC_III.Equals(BHXHCheckPrintType))
                    {
                        OnPrintReportKhtDuToanThuBHXH(exportType, lstSelectedUnit);
                    }
                    if (BHXHCheckPrintType.PHU_LUC_IV.Equals(BHXHCheckPrintType) || BHXHCheckPrintType.PHU_LUC_V.Equals(BHXHCheckPrintType))
                    {
                        OnPrintReportKhtDuToanThuBHYT(exportType, lstSelectedUnit);
                    }
                    if (BHXHCheckPrintType.DU_TOAN_THU_CHI_TONG_HOP.Equals(BHXHCheckPrintType))
                    {
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                        {
                            OnPrintReportKhtcTongHop(exportType, lstDonViSelected.Select(x => x.ValueItem));
                        }
                        else
                        {
                            OnPrintReportKhtcTongHop(exportType, lstSelectedUnit);
                        }
                    }
                }
            }

        }
        public void OnPrintReportKhtcTongHop(ExportType exportType, string lstSelectedUnit)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";

                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    List<ReportKhtDuToanBHXHQuery> listData = new List<ReportKhtDuToanBHXHQuery>();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();

                    //BHYT - Quan nhan
                    listData = _khtBHXHChiTietService.FindReportKhtcTongHop(yearOfWork, lstSelectedUnit, donViTinh).ToList();
                    listData.ForEach(x => x.BHangCha = new List<string>() { "A", "B" }.Contains(x.STTDisplay));


                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_DU_TOAN_TONG_HOP_THU_CHI_PLI));
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                    data.Add("ListData", listData);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("TongCong", listData.Where(x => new List<string>() { "A", "B" }.Contains(x.STTDisplay)).Sum(x => x.FSoTien));
                    data.Add("TongCongThu", listData.Where(x => new List<string>() { "A" }.Contains(x.STTDisplay)).Sum(x => x.FSoTien));
                    data.Add("TongCongChi", listData.Where(x => new List<string>() { "B" }.Contains(x.STTDisplay)).Sum(x => x.FSoTien));

                    data.Add("CurrencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                    AddChuKy(data, _typeChuky);
                    int stt = 1;
                    foreach (var i in listData)
                    {
                        i.STT = stt++;
                    }

                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<ReportKhtDuToanBHXHQuery>(templateFileName, data);
                    exportResults.Add(new ExportResult("DỰ TOÁN TỔNG HỢP THU CHI BẢO HIỂM XÃ HỘI NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = exportResults;
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

        public void OnPrintReportKhtcTongHop(ExportType exportType, IEnumerable<string> lstSelectedUnit)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";

                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    List<ReportKhtDuToanBHXHQuery> listData = new List<ReportKhtDuToanBHXHQuery>();

                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();

                    foreach (var item in lstSelectedUnit)
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        //BHYT - Quan nhan
                        listData = _khtBHXHChiTietService.FindReportKhtcTongHop(yearOfWork, item, donViTinh).ToList();
                        listData.ForEach(x => x.BHangCha = new List<string>() { "A", "B" }.Contains(x.STTDisplay));


                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_DU_TOAN_TONG_HOP_THU_CHI_NGANG));
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                        data.Add("ListData", listData);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("TongCong", listData.Where(x => new List<string>() { "A", "B" }.Contains(x.STTDisplay)).Sum(x => x.FSoTien));
                        data.Add("TongCongThu", listData.Where(x => new List<string>() { "A" }.Contains(x.STTDisplay)).Sum(x => x.FSoTien));
                        data.Add("TongCongChi", listData.Where(x => new List<string>() { "B" }.Contains(x.STTDisplay)).Sum(x => x.FSoTien));

                        data.Add("CurrencyToText", currencyToText);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", _sessionInfo.TenDonVi);
                        data.Add("h1", h1);
                        data.Add("h2", "");
                        data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        AddChuKy(data, _typeChuky);
                        _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                        int stt = 1;
                        foreach (var i in listData)
                        {
                            i.STT = stt++;
                        }

                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                        var xlsFile = _exportService.Export<ReportKhtDuToanBHXHQuery>(templateFileName, data);
                        exportResults.Add(new ExportResult("DỰ TOÁN TỔNG HỢP THU CHI BẢO HIỂM XÃ HỘI NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    }



                    e.Result = exportResults;
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

        public void OnPrintReportKhtDuToanThuBHYT(ExportType exportType, string lstSelectedUnit)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";

                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    List<ReportKhtDuToanBHXHQuery> listData = new List<ReportKhtDuToanBHXHQuery>();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    //BHYT - Quan nhan
                    if (BHXHCheckPrintType.PHU_LUC_IV.Equals(BHXHCheckPrintType))
                    {
                        listData = _khtBHXHChiTietService.FindReportKhtDuToanBHYT(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, BhxhLoaiChungTu.BhxhDaTongHop,
                        lstSelectedUnit, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN, BhxhLoaiSM.QUAN_NHAN, donViTinh).ToList();
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHT_DU_TOAN_THU_BHYT_QUAN_NHAN));
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                        data.Add("ListData", listData);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("BHYTTongDuToan", listData.Sum(x => x.BHYTTongCongDuToan));
                        data.Add("BHYTTongHachToan", listData.Sum(x => x.BHYTTongCongHachToan));
                        data.Add("BHYTTongCong", listData.Sum(x => x.BhytTongCong));
                    }
                    //BHYT - Nguoi Lao Dong
                    if (BHXHCheckPrintType.PHU_LUC_V.Equals(BHXHCheckPrintType))
                    {
                        listData = _khtBHXHChiTietService.FindReportKhtDuToanBHYT(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, BhxhLoaiChungTu.BhxhDaTongHop,
                        lstSelectedUnit, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN, BhxhLoaiSM.NGUOI_LAO_DONG, donViTinh).ToList();
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHT_DU_TOAN_THU_BHYT_NLD));
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                        data.Add("ListData", listData);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("TotalBhytNldDongDuToan", listData.Sum(x => x.BhytNldDongDuToan));
                        data.Add("TotalBhytNsddDongDuToan", listData.Sum(x => x.BhytNsddDongDuToan));
                        data.Add("TotalBHYTTongCongDuToan", listData.Sum(x => x.BHYTTongCongDuToan));
                        data.Add("TotalBhytNldDongHachToan", listData.Sum(x => x.BhytNldDongHachToan));
                        data.Add("TotalBhytNsddDongHachToan", listData.Sum(x => x.BhytNsddDongHachToan));
                        data.Add("BHYTTongCongHachToan", listData.Sum(x => x.BHYTTongCongHachToan));
                        data.Add("TotalBHYT", listData.Sum(x => x.BhytTongCong));
                    }

                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    AddChuKy(data, _typeChuky);
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                    int stt = 1;
                    foreach (var i in listData)
                    {
                        i.STT = stt++;
                    }

                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<ReportKhtDuToanBHXHQuery>(templateFileName, data);
                    exportResults.Add(new ExportResult("DỰ TOÁN THU BẢO HIỂM XÃ HỘI NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = exportResults;
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

        public void OnPrintVoucherDetail(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                    var yearOfWork = _sessionInfo.YearOfWork;
                    KhtBHXHChiTietCriteria searchCondition = new KhtBHXHChiTietCriteria();
                    searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                    searchCondition.DonViTinh = donViTinh;
                    List<BhKhtBHXHChiTiet> lstKhtChungTuChiTiet = new List<BhKhtBHXHChiTiet>();
                    List<ExportResult> results = new List<ExportResult>();
                    foreach (var dv in lstDonViSelected)
                    {
                        DonViChungTu = dv.NameItem;
                        searchCondition.IsPrintReport = true;
                        searchCondition.khtBhxhId = KhtBhxhId;
                        searchCondition.MaDonVi = dv.ValueItem;
                        lstKhtChungTuChiTiet = _khtBHXHChiTietService.FindBhKhtBHXHChiTietByCondition(searchCondition).ToList();
                        HandleToPrint(results, lstKhtChungTuChiTiet, donViTinh, exportType, yearOfWork, dv.ValueItem);
                        e.Result = results;
                    }

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

        public void OnPrintVoucherDetailAggregate(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                    var maDonVis = string.Join(",", lstDonViSelected.Select(x => x.ValueItem.ToString()).ToList());
                    var lstData = _khtBHXHChiTietService.PrintVoucherDetailAggregate(_sessionInfo.YearOfWork, maDonVis, donViTinh).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    string templateFileName;
                    string fileNamePrefix;
                    var sumTotal = lstData.Sum(x => x.FTongCong);
                    var lstExportData = _mapper.Map<ObservableCollection<BhKhtBHXHChiTietQuery>>(lstData).ToList();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();

                    CalculateData(lstExportData);
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", lstExportData.Where(x => x.HasDataToPrint));
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("YearOfWork", "NĂM " + _sessionInfo.YearOfWork);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("TongSoTien", sumTotal / donViTinh);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("DonVi", _sessionInfo.TenDonVi);
                    data.Add("DonViChungTu", DonViChungTu);
                    data.Add("IsAggregate", true);

                    data.Add("TotalQSBQ", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.IQSBQNam));
                    data.Add("TotalLuongChinh", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FLuongChinh));
                    data.Add("TotalPCCV", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FPhuCapChucVu));
                    data.Add("TotalPCTNN", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FPCTNNghe));
                    data.Add("TotalPCTNVK", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FPCTNVuotKhung));
                    data.Add("TotalNghiOm", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FNghiOm));
                    data.Add("TotalHSBL", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FHSBL));
                    data.Add("TotalTongQTLN", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FTongQuyTienLuongNam));
                    data.Add("TotalBHXHNld", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNguoiLaoDong));
                    data.Add("TotalBHXHNsd", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNguoiSuDungLaoDong));
                    data.Add("TotalTongBHXH", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FTongThuBHXH));
                    data.Add("TotalBHYTNld", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNguoiLaoDong));
                    data.Add("TotalBHYTNsd", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNguoiSuDungLaoDong));
                    data.Add("TotalTongBHYT", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FTongThuBHYT));
                    data.Add("TotalBHTNNld", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNguoiLaoDong));
                    data.Add("TotalBHTNNsd", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNguoiSuDungLaoDong));
                    data.Add("TotalTongBHTN", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FTongThuBHTN));
                    data.Add("TotalTongCong", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FTongCong));
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);

                    AddChuKy(data, _typeChuky);
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHT_ChungTu_ChiTiet_PhuLuc_KHT_BHXH_Doc));
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<BhKhtBHXHChiTietQuery, BhDmMucLucNganSach, BhKhtBHXHChiTiet>(templateFileName, data);
                    results.Add(new ExportResult("KẾ HOẠCH THU BHXH, BHYT, BHTH NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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

        private void HandleToPrint(List<ExportResult> results, List<BhKhtBHXHChiTiet> lstVoucherDetail, int donViTinh, ExportType exportType, int yearOfWork, string MaDonVi = null)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            var h1 = _catUnitTypeSelected.DisplayItem;
            string templateFileName;
            string fileNamePrefix;
            var sumTotal = lstVoucherDetail.Sum(x => x.FTongCong);
            var lstExportData = _mapper.Map<ObservableCollection<BhKhtBHXHChiTietQuery>>(lstVoucherDetail).ToList();
            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
            CurrencyToText currencyToText = new CurrencyToText();
            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            CalculateDataByCurrency(lstExportData, donViTinh);
            CalculateData(lstExportData);
            data.Add("currencyToText", currencyToText);
            data.Add("FormatNumber", formatNumber);
            data.Add("ListData", lstExportData.Where(x => x.HasDataToPrint));
            data.Add("Cap1", _sessionInfo.TenDonVi);
            data.Add("TieuDe1", TxtTitleFirst);
            data.Add("TieuDe2", TxtTitleSecond);
            data.Add("TieuDe3", TxtTitleThird);
            data.Add("YearOfWork", "NĂM " + yearOfWork);
            data.Add("h1", h1);
            data.Add("h2", "");
            data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
            data.Add("TongSoTien", sumTotal / donViTinh);
            data.Add("DiaDiem", _diaDiem);
            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
            data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
            data.Add("DonVi", _sessionInfo.TenDonVi);
            data.Add("DonViChungTu", DonViChungTu);

            data.Add("TotalQSBQ", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.IQSBQNam));
            data.Add("TotalLuongChinh", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FLuongChinh));
            data.Add("TotalPCCV", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FPhuCapChucVu));
            data.Add("TotalPCTNN", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FPCTNNghe));
            data.Add("TotalPCTNVK", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FPCTNVuotKhung));
            data.Add("TotalNghiOm", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FNghiOm));
            data.Add("TotalHSBL", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FHSBL));
            data.Add("TotalTongQTLN", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FTongQuyTienLuongNam));
            data.Add("TotalBHXHNld", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNguoiLaoDong));
            data.Add("TotalBHXHNsd", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNguoiSuDungLaoDong));
            data.Add("TotalTongBHXH", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FTongThuBHXH));
            data.Add("TotalBHYTNld", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNguoiLaoDong));
            data.Add("TotalBHYTNsd", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNguoiSuDungLaoDong));
            data.Add("TotalTongBHYT", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FTongThuBHYT));
            data.Add("TotalBHTNNld", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNguoiLaoDong));
            data.Add("TotalBHTNNsd", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNguoiSuDungLaoDong));
            data.Add("TotalTongBHTN", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FTongThuBHTN));
            data.Add("TotalTongCong", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FTongCong));
            if (IsInTheoTongHop)
            {
                data.Add("IsAggregate", true);
                _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
            }
            else
            {
                _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork, MaDonVi);
            }

            AddChuKy(data, _typeChuky);
            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHT_ChungTu_ChiTiet_PhuLuc_KHT_BHXH_Doc));
            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
            var xlsFile = _exportService.Export<BhKhtBHXHChiTietQuery, BhDmMucLucNganSach, BhKhtBHXHChiTiet>(templateFileName, data);
            results.Add(new ExportResult("KẾ HOẠCH THU BHXH, BHYT, BHTH NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

        }

        public void OnPrintReportKhtDuToanThuBHXH(ExportType exportType, string lstSelectedUnit)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";
                    List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                    var lstSelectedUnit = string.Join(",", lstDonViSelected.Select(x => x.ValueItem.ToString()).ToList());
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var listData = _khtBHXHChiTietService.FindReportKhtDuToanBHXH(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, BhxhLoaiChungTu.BhxhDaTongHop,
                        lstSelectedUnit, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN, donViTinh).ToList();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listData);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("TongSoTien", 0);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    AddChuKy(data, _typeChuky);
                    //BHXH
                    if (BHXHCheckPrintType.PHU_LUC_II.Equals(BHXHCheckPrintType))
                    {
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHT_DU_TOAN_THU_BHXH));
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("TotalBHXHNldDongDuToan", listData.Sum(x => x.BhxhNldDongDuToan));
                        data.Add("TotalBHXHNsddDongDuToan", listData.Sum(x => x.BhxhNsddDongDuToan));
                        data.Add("TotalBHXHNldDongHachToan", listData.Sum(x => x.BhxhNldDongHachToan));
                        data.Add("TotalBHXHNsddDongHachToan", listData.Sum(x => x.BhxhNsddDongHachToan));
                        data.Add("TotalBHXHDuToan", listData.Sum(x => x.BHXHTongCongDuToan));
                        data.Add("TotalBHXHHachToan", listData.Sum(x => x.BHXHTongCongHachToan));
                        data.Add("TotalBHXH", listData.Sum(x => x.BHXHTongCong));
                    }
                    //BHTN
                    if (BHXHCheckPrintType.PHU_LUC_III.Equals(BHXHCheckPrintType))
                    {
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHT_DU_TOAN_THU_BHTN));
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("TotalBHTNNldDongDuToan", listData.Sum(x => x.BhtnNldDongDuToan));
                        data.Add("TotalBHTNNsddDongDuToan", listData.Sum(x => x.BhtnNsddDongDuToan));
                        data.Add("TotalBHTNNldDongHachToan", listData.Sum(x => x.BhtnNldDongHachToan));
                        data.Add("TotalBHTNNsddDongHachToan", listData.Sum(x => x.BhtnNsddDongHachToan));
                        data.Add("TotalBHTNDuToan", listData.Sum(x => x.BHTNTongCongDuToan));
                        data.Add("TotalBHTNHachToan", listData.Sum(x => x.BHTNTongCongHachToan));
                        data.Add("TotalBHTN", listData.Sum(x => x.BhtnTongCong));
                    }
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                    int stt = 1;
                    foreach (var i in listData)
                    {
                        i.STT = stt++;
                    }

                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<ReportKhtDuToanBHXHQuery>(templateFileName, data);
                    exportResults.Add(new ExportResult("DỰ TOÁN THU BẢO HIỂM XÃ HỘI NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = exportResults;
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

        public void OnPrintVoucherDetailAggregateByRank(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                    List<BhKhtBHXHChiTietQuery> lstData = new List<BhKhtBHXHChiTietQuery>();

                    if (IsInTheoTongHop)
                    {
                        List<string> lstChildVoucher = new List<string>();
                        List<string> lstUnit = new List<string>();

                        foreach (var item in lstDonViSelected)
                        {
                            var voucher = _khtBHXHService.FindByCondition(_sessionInfo.YearOfWork, item.ValueItem, BhxhLoaiChungTu.BhxhChungTuTongHop).FirstOrDefault();
                            var childVoucher = voucher.STongHop.Split(",").Distinct().ToList();
                            lstChildVoucher.AddRange(childVoucher);
                        }

                        foreach (var ct in lstChildVoucher)
                        {
                            var voucherInfo = _khtBHXHService.FindChungTuDaTongHopBySCT(ct, _sessionInfo.YearOfWork).FirstOrDefault();
                            lstUnit.Add(voucherInfo.IID_MaDonVi);
                        }

                        var maDonVis = string.Join(",", lstUnit.ToList());
                        lstData = _khtBHXHChiTietService.PrintVoucherDetailAggregateByUnits(_sessionInfo.YearOfWork, maDonVis, IsInTheoTongHop,
                            BhxhLoaiChungTu.BhxhChungTu, donViTinh).ToList();
                    }
                    else
                    {
                        var maDonVis = string.Join(",", lstDonViSelected.Select(x => x.ValueItem.ToString()).ToList());
                        lstData = _khtBHXHChiTietService.PrintVoucherDetailAggregateByUnits(_sessionInfo.YearOfWork, maDonVis, IsInTheoTongHop,
                            BhxhLoaiChungTu.BhxhChungTu, donViTinh).ToList();
                    }


                    List<ExportResult> results = new List<ExportResult>();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    string templateFileName;
                    string fileNamePrefix;
                    var sumTotal = lstData.Sum(x => x.FTongCong);
                    var lstExportData = _mapper.Map<ObservableCollection<BhKhtBHXHChiTietQuery>>(lstData).ToList();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();

                    CalculateData(lstExportData);
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", lstExportData.Where(x => x.HasDataToPrint));
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("YearOfWork", "NĂM " + _sessionInfo.YearOfWork);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("TongSoTien", sumTotal / donViTinh);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    data.Add("DonVi", _sessionInfo.TenDonVi);
                    data.Add("DonViChungTu", DonViChungTu);
                    data.Add("IsAggregate", true);

                    data.Add("TotalQSBQ", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.IQSBQNam));
                    data.Add("TotalLuongChinh", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FLuongChinh));
                    data.Add("TotalPCCV", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FPhuCapChucVu));
                    data.Add("TotalPCTNN", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FPCTNNghe));
                    data.Add("TotalPCTNVK", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FPCTNVuotKhung));
                    data.Add("TotalNghiOm", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FNghiOm));
                    data.Add("TotalHSBL", lstExportData.Where(x => x.IsHangCha == false).Sum(x => x.FHSBL));
                    data.Add("TotalTongQTLN", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FTongQuyTienLuongNam));
                    data.Add("TotalBHXHNld", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FThuBHXHNguoiLaoDong));
                    data.Add("TotalBHXHNsd", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FThuBHXHNguoiSuDungLaoDong));
                    data.Add("TotalTongBHXH", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FTongThuBHXH));
                    data.Add("TotalBHYTNld", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FThuBHYTNguoiLaoDong));
                    data.Add("TotalBHYTNsd", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FThuBHYTNguoiSuDungLaoDong));
                    data.Add("TotalTongBHYT", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FTongThuBHYT));
                    data.Add("TotalBHTNNld", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FThuBHTNNguoiLaoDong));
                    data.Add("TotalBHTNNsd", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FThuBHTNNguoiSuDungLaoDong));
                    data.Add("TotalTongBHTN", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FTongThuBHTN));
                    data.Add("TotalTongCong", lstExportData.Where(x => x.BHangCha == false).Sum(x => x.FTongCong));
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                    AddChuKy(data, _typeChuky);
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_KHT_ChungTu_ChiTiet_PhuLuc_KHT_BHXH_Doc));
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<BhKhtBHXHChiTietQuery, BhDmMucLucNganSach, BhKhtBHXHChiTiet>(templateFileName, data);
                    results.Add(new ExportResult("KẾ HOẠCH THU BHXH, BHYT, BHTH NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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
                input += "_Doc";
            return Path.Combine(ExportPrefix.PATH_BH_KHT, input + FileExtensionFormats.Xlsx);
        }

        private void CalculateData(List<BhKhtBHXHChiTietQuery> lstKhtChungTuChiTiet)
        {
            lstKhtChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.IQSBQNam = 0;
                    x.FLuongChinh = 0;
                    x.FPhuCapChucVu = 0;
                    x.FPCTNNghe = 0;
                    x.FPCTNVuotKhung = 0;
                    x.FNghiOm = 0;
                    x.FHSBL = 0;
                    x.FTongQuyTienLuongNam = 0;
                    x.FThuBHXHNguoiLaoDong = 0;
                    x.FThuBHXHNguoiSuDungLaoDong = 0;
                    x.FTongThuBHXH = 0;
                    x.FThuBHYTNguoiLaoDong = 0;
                    x.FThuBHYTNguoiSuDungLaoDong = 0;
                    x.FTongThuBHYT = 0;
                    x.FThuBHTNNguoiLaoDong = 0;
                    x.FThuBHTNNguoiSuDungLaoDong = 0;
                    x.FTongThuBHTN = 0;
                    x.FTongCong = 0;
                    return x;
                }).ToList();
            var temp = lstKhtChungTuChiTiet.Where(x => !x.BHangCha);
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, lstKhtChungTuChiTiet);
            }

        }

        private void CalculateDataByCurrency(List<BhKhtBHXHChiTietQuery> lstKhtChungTuChiTiet, int dvt)
        {
            foreach (var item in lstKhtChungTuChiTiet)
            {
                item.FLuongChinh = item.FLuongChinh / dvt;
                item.FPhuCapChucVu = item.FPhuCapChucVu / dvt;
                item.FPCTNNghe = item.FPCTNNghe / dvt;
                item.FPCTNVuotKhung = item.FPCTNVuotKhung / dvt;
                item.FNghiOm = item.FNghiOm / dvt;
                item.FHSBL = item.FHSBL / dvt;
                item.FTongQuyTienLuongNam = item.FTongQuyTienLuongNam / dvt;
                item.FThuBHXHNguoiLaoDong = item.FThuBHXHNguoiLaoDong / dvt;
                item.FThuBHXHNguoiSuDungLaoDong = item.FThuBHXHNguoiSuDungLaoDong / dvt;
                item.FThuBHYTNguoiLaoDong = item.FThuBHYTNguoiLaoDong / dvt;
                item.FThuBHYTNguoiSuDungLaoDong = item.FThuBHYTNguoiSuDungLaoDong / dvt;
                item.FThuBHTNNguoiLaoDong = item.FThuBHTNNguoiLaoDong / dvt;
                item.FThuBHTNNguoiSuDungLaoDong = item.FThuBHTNNguoiSuDungLaoDong / dvt;
                item.FTongThuBHXH = item.FTongThuBHXH / dvt;
                item.FTongThuBHYT = item.FTongThuBHYT / dvt;
                item.FTongThuBHTN = item.FTongThuBHTN / dvt;
                item.FTongCong = item.FTongThuBHXH + item.FTongThuBHYT + item.FTongThuBHTN;
            }

        }

        private void CalculateParent(Guid? idParent, BhKhtBHXHChiTietQuery item, List<BhKhtBHXHChiTietQuery> lstKhtChungTuChiTiet)
        {
            var model = lstKhtChungTuChiTiet.FirstOrDefault(x => x.IIDMucLucNganSach == idParent);
            if (model == null) return;
            model.IQSBQNam += item.IQSBQNam.GetValueOrDefault();
            model.FLuongChinh += item.FLuongChinh.GetValueOrDefault();
            model.FPhuCapChucVu += item.FPhuCapChucVu.GetValueOrDefault();
            model.FPCTNNghe += item.FPCTNNghe.GetValueOrDefault();
            model.FPCTNVuotKhung += item.FPCTNVuotKhung.GetValueOrDefault();
            model.FNghiOm += item.FNghiOm.GetValueOrDefault();
            model.FHSBL += item.FHSBL.GetValueOrDefault();
            model.FTongQuyTienLuongNam += item.FTongQuyTienLuongNam.GetValueOrDefault();
            model.FThuBHXHNguoiLaoDong += item.FThuBHXHNguoiLaoDong.GetValueOrDefault();
            model.FThuBHXHNguoiSuDungLaoDong += item.FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault();
            model.FTongThuBHXH += item.FTongThuBHXH.GetValueOrDefault();
            model.FThuBHYTNguoiLaoDong += item.FThuBHYTNguoiLaoDong.GetValueOrDefault();
            model.FThuBHYTNguoiSuDungLaoDong += item.FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault();
            model.FTongThuBHYT += item.FTongThuBHYT.GetValueOrDefault();
            model.FThuBHTNNguoiLaoDong += item.FThuBHTNNguoiLaoDong;
            model.FThuBHTNNguoiSuDungLaoDong += item.FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault();
            model.FTongThuBHTN += item.FTongThuBHTN.GetValueOrDefault();
            model.FTongCong += item.FTongCong.GetValueOrDefault();
            CalculateParent(model.IdParent, item, lstKhtChungTuChiTiet);
        }


        private void OnConfigSign()
        {
            LoadTypeChuKy();
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


        private void OnNoteCommand()
        {
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();
            BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_BHXH_KHT_CHITIET, TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHXH, TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHTN, TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHYT_QUAN_NHAN, TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHYT_NLD, TypeChuKy.RPT_BHXH_KHTC_TONG_HOP };
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(ListDonVi);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuky;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = true;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = false;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
        }
    }
}
