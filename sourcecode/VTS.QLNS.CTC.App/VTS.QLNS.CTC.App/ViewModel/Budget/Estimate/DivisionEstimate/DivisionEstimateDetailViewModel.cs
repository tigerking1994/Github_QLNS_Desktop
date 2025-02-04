using Aspose.Cells;
using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate
{
    public class DivisionEstimateDetailViewModel : DetailViewModelBase<DtChungTuModel, DtChungTuChiTietModel>
    {
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsDcChungTuChiTietService _dcChungTuChiTietService;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDtNhanPhanBoMapService _dtChungTuMapService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _budgetCatalogItemsView;
        private ICollectionView _itemsView;
        private readonly INsDonViService _nsDonViService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly INsMucLucNganSachService _mlnsService;
        private readonly IDanhMucService _danhMucService;
        private readonly ILog _logger;
        private DanhMucNganhService _danhMucNganhService;
        private EstimationVoucherDetailCriteria _searchCondition;
        private bool _isNamLuyKe;
        private bool _isShowQuyetDinh;
        private List<NsMucLucNganSach> _listMLNS;
        private SessionInfo _sessionInfo;
        private List<DanhMuc> _listDanhMucNganh;
        private ICollection<DtChungTuChiTietModel> _filterResult = new HashSet<DtChungTuChiTietModel>();
        private List<Tuple<string, Guid?>> _filterResultWithSQD = new List<Tuple<string, Guid?>>();
        private HashSet<(string, Guid?)> _mapSQD = new HashSet<(string, Guid?)> ();
        private HashSet<string> _mapCha = new HashSet<string> ();
        private string xnmConcatenation = "";
        private Dictionary<string, List<string>> _dicDonViNganh = new Dictionary<string, List<string>>();

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateVoucherEvent;
        public override Type ContentType => typeof(View.Budget.Estimate.Division.DivisionDetail);
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        public bool IsDuToanDauNam => (Model.ILoaiDuToan.HasValue && BudgetType.YEAR.Equals((BudgetType)Model.ILoaiDuToan.Value));
        public bool IsDieuChinh => (Model.ILoaiDuToan.HasValue && (BudgetType.ADDITIONAL.Equals((BudgetType)Model.ILoaiDuToan.Value) || BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR.Equals((BudgetType)Model.ILoaiDuToan.Value)));
        public bool IsDeleteAll => Items.Any(item => !item.IsModified && !item.IsConLai && !item.IsPhanBo && item.HasData);
        public bool IsTypeLuyKe => _isNamLuyKe;
        public bool IsShowQuyetDinh => _isShowQuyetDinh;
        public bool HasLastDivisionEstimateVoucher { get; set; }
        public bool IsShowAdjEstimate => (Model.ILoaiDuToan.Equals((int)BudgetType.ADDITIONAL) || Model.ILoaiDuToan.Equals((int)BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR)) && Model.ILoai == 1;
        public bool IsEnableAdjEstimate => !Model.BKhoa;
        public int NamLamViec { get; set; }
        public Dictionary<string, List<NsDtChungTu>> DicNhanPhanBo = new Dictionary<string, List<NsDtChungTu>>();
        public Dictionary<string, List<NsDtChungTuChiTiet>> DicDtChungTuChiTiet = new Dictionary<string, List<NsDtChungTuChiTiet>>();

        private DivisionEstimateDetailPropertyHelper _detailHelper = new DivisionEstimateDetailPropertyHelper();
        public DivisionEstimateDetailPropertyHelper DetailHelper
        {
            get => _detailHelper;
            set => SetProperty(ref _detailHelper, value);
        }

        private DivisionEstimateDetailPropertyHelper _detailTotal;

        public DivisionEstimateDetailPropertyHelper DetailTotal
        {
            get => _detailTotal;
            set => SetProperty(ref _detailTotal, value);
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _budgetCatalogItemsView.Refresh();
                    CalculateTotal();
                }
            }
        }

        private ObservableCollection<DtChungTuChiTietModel> _budgetCatalogItems;
        public ObservableCollection<DtChungTuChiTietModel> BudgetCatalogItems
        {
            get => _budgetCatalogItems;
            set => SetProperty(ref _budgetCatalogItems, value);
        }

        private DtChungTuChiTietModel _selectedBudgetCatalog;
        public DtChungTuChiTietModel SelectedBudgetCatalog
        {
            get => _selectedBudgetCatalog;
            set
            {
                if (SetProperty(ref _selectedBudgetCatalog, value))
                {
                    if (_selectedBudgetCatalog != null)
                        SelectedLNS = _selectedBudgetCatalog.SLns;
                    BeForeRefresh();
                    _itemsView.Refresh();
                }
                CalculateTotalParent();
                IsOpenLnsPopup = false;
            }
        }

        private bool _isOpenLnsPopup;
        public bool IsOpenLnsPopup
        {
            get => _isOpenLnsPopup;
            set => SetProperty(ref _isOpenLnsPopup, value);
        }

        private string _selectedLNS;
        public string SelectedLNS
        {
            get => _selectedLNS;
            set => SetProperty(ref _selectedLNS, value);
        }

        private EstimationDetailCriteria _detailFilter;
        public EstimationDetailCriteria DetailFilter
        {
            get => _detailFilter;
            set => SetProperty(ref _detailFilter, value);
        }

        private ObservableCollection<ComboboxItem> _agencies;
        public ObservableCollection<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private ComboboxItem _selectedAgency;
        public ComboboxItem SelectedAgency
        {
            get => _selectedAgency;
            set
            {
                SetProperty(ref _selectedAgency, value);
                BeForeRefresh();
                _itemsView.Refresh();
                CalculateTotalParent();
                OnPropertyChanged(nameof(IsEnablePhanBoAll));
            }
        }

        public Visibility ShowColNSSD { get; set; }
        public bool FirstTimePhanBo { get; set; }


        public bool IsEnablePhanBoAll => SelectedAgency != null;

        public IEnumerable<DtChungTuModel> DtChungTuModelNhanPhanBos { get; set; }
        public IEnumerable<DtChungTuModel> DtChungTuModelPhanBos { get; set; }
        private ObservableCollection<ComboboxItem> _cbxNhanPhanBos;
        public ObservableCollection<ComboboxItem> CbxNhanPhanBos
        {
            get => _cbxNhanPhanBos;
            set => SetProperty(ref _cbxNhanPhanBos, value);
        }
        private ObservableCollection<ComboboxItem> _typeDisplays;
        public ObservableCollection<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private string _typeDisplaysselected;
        public string TypeDisplaysSelected
        {
            get => _typeDisplaysselected;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    SetProperty(ref _typeDisplaysselected, value);
                    OnRefresh();
                }
            }
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private DivisionColumnVisibility _columnVisibility;
        public DivisionColumnVisibility ColumnVisibility
        {
            get => _columnVisibility;
            set => SetProperty(ref _columnVisibility, value);
        }

        private ObservableCollection<DanhMucNganhModel> _NNganhItems;
        public ObservableCollection<DanhMucNganhModel> NNganhItems
        {
            get => _NNganhItems;
            set => SetProperty(ref _NNganhItems, value);
        }

        private DanhMucNganhModel _selectedNNganh;
        public DanhMucNganhModel SelectedNNganh
        {
            get => _selectedNNganh;
            set
            {
                if (SetProperty(ref _selectedNNganh, value))
                {
                    BeForeRefresh();
                    _itemsView.Refresh();
                    CalculateTotalParent();
                }
            }
        }

        private ObservableCollection<DanhMucNganhModel> _cNganhItems;
        public ObservableCollection<DanhMucNganhModel> CNganhItems
        {
            get => _cNganhItems;
            set => SetProperty(ref _cNganhItems, value);
        }

        private DanhMucNganhModel _selectedCNganh;
        public DanhMucNganhModel SelectedCNganh
        {
            get => _selectedCNganh;
            set
            {
                if (SetProperty(ref _selectedCNganh, value))
                {
                    BeForeRefresh();
                    _itemsView.Refresh();
                    CalculateTotalParent();
                }
            }
        }

        private bool _isAdjusted;
        public bool IsAdjusted
        {
            get => _isAdjusted;
            set => SetProperty(ref _isAdjusted, value);
        }

        private bool _isProcessReport;
        public bool IsProcessReport
        {
            get => _isProcessReport;
            set => SetProperty(ref _isProcessReport, value);
        }

        private int _progressValue;
        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        public bool IsFillDataDauNam { get; set; }
        public PrintReportCoverSheetViewModel PrintReportCoverSheetViewModel { get; set; }
        public PrintReportTargetAgencyViewModel PrintReportTargetAgencyViewModel { get; set; }
        public PrintReportTargetMajorsViewModel PrintReportTargetMajorsViewModel { get; set; }
        public PrintReportDetailTargetAgencyLnsViewModel PrintReportDetailTargetAgencyLnsViewModel { get; set; }
        public PrintReportSynthesisAgencyViewModel PrintReportSynthesisAgencyViewModel { get; set; }
        public PrintReportSynthesisTargetMajorsViewModel PrintReportSynthesisTargetMajorsViewModel { get; set; }
        public PrintReportTargetMajorsDayViewModel PrintReportTargetMajorsDayViewModel { get; set; }
        public PrintReportTargetMajorAgencyViewModel PrintReportTargetMajorAgencyViewModel { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand SaveDataCommand { get; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand FillDataDauNamCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand FillDataDieuChinhCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand PhanBoConLaiCommand { get; }
        public RelayCommand GetAdjEstimateCommand { get; }

        public DivisionEstimateDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsDcChungTuChiTietService dcChungTuChiTietService,
            INsDtChungTuService dtChungTuService,
            INsDtNhanPhanBoMapService dtChungTuMapService,
            INsDonViService nsDonViService,
            ISktSoLieuService sktSoLieuService,
            INsMucLucNganSachService mlnsService,
            IDanhMucService danhMucService,
            ILog logger,
            DanhMucNganhService danhMucNganhService,
            PrintReportCoverSheetViewModel printReportCoverSheetViewModel,
            PrintReportTargetAgencyViewModel printReportTargetAgencyViewModel,
            PrintReportTargetMajorsViewModel printReportTargetMajorsViewModel,
            PrintReportSynthesisAgencyViewModel printReportSynthesisAgencyViewModel,
            PrintReportDetailTargetAgencyLnsViewModel printReportDetailTargetAgencyLnsViewModel,
            PrintReportTargetMajorsDayViewModel printReportTargetMajorsDayViewModel,
            PrintReportTargetMajorAgencyViewModel printReportTargetMajorAgencyViewModel,
            PrintReportSynthesisTargetMajorsViewModel printReportSynthesisTargetMajorsViewModel) : base(danhMucService, sessionService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _dcChungTuChiTietService = dcChungTuChiTietService;
            _dtChungTuService = dtChungTuService;
            _dtChungTuMapService = dtChungTuMapService;
            _nsDonViService = nsDonViService;
            _sktSoLieuService = sktSoLieuService;
            _mlnsService = mlnsService;
            _danhMucService = danhMucService;
            _danhMucNganhService = danhMucNganhService;
            _logger = logger;

            SearchCommand = new RelayCommand(obj => { BeForeRefresh(); _itemsView.Refresh(); CalculateTotalParent(); });
            FillDataDauNamCommand = new RelayCommand(obj => FillDataDauNam());
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            FillDataDieuChinhCommand = new RelayCommand(obj => FillDataDieuChinh());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            PhanBoConLaiCommand = new RelayCommand(obj => OnPhanBoConLai());
            GetAdjEstimateCommand = new RelayCommand(obj => GetAdjustedEstimateVoucher());

            PrintReportCoverSheetViewModel = printReportCoverSheetViewModel;
            PrintReportTargetAgencyViewModel = printReportTargetAgencyViewModel;
            PrintReportTargetMajorsViewModel = printReportTargetMajorsViewModel;
            PrintReportTargetMajorsDayViewModel = printReportTargetMajorsDayViewModel;
            PrintReportTargetMajorAgencyViewModel = printReportTargetMajorAgencyViewModel;
            PrintReportDetailTargetAgencyLnsViewModel = printReportDetailTargetAgencyLnsViewModel;
            PrintReportSynthesisAgencyViewModel = printReportSynthesisAgencyViewModel;
            PrintReportSynthesisTargetMajorsViewModel = printReportSynthesisTargetMajorsViewModel;
        }

        public override void Init()
        {
            base.Init();
            IsFillDataDauNam = true;
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            ResetConditionSearch();
            LoadChuyenNganh();
            LoadNhomNganh();
            LoadNamLuyKe();
            LoadControlVisibility();
            LoadAgencies();
            if (Model.ILoaiChungTu.HasValue && VoucherType.NSBD_Key.Equals(Model.ILoaiChungTu.ToString()))
            {
                LoadNganhByDonVi();
            }
            LoadTypeDisplay();
            LoadDotNhan();
            CheckLastDivisionEstimateVoucher();
        }

        private void ResetConditionSearch()
        {
            DetailTotal = new DivisionEstimateDetailPropertyHelper();
            DetailFilter = new EstimationDetailCriteria();
            _listDanhMucNganh = new List<DanhMuc>();
            _dicDonViNganh = new Dictionary<string, List<string>>();
            Items = new ObservableCollection<DtChungTuChiTietModel>();
            _isAdjusted = false;
            if (Model.ILoaiDuToan.HasValue && BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDuToan.Value))
                _isAdjusted = true;
        }

        private void LoadNhomNganh()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.INamLamViec == yearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.SType.Equals(VoucherType.VOCHER_TYPE));
            var list = _danhMucService.FindByCondition(predicate).ToList();
            NNganhItems = _mapper.Map<ObservableCollection<DanhMucNganhModel>>(list);
            _selectedNNganh = null;
        }

        private void LoadChuyenNganh()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.INamLamViec == yearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.SType.Equals(VoucherType.DM_Nganh));
            if (SelectedNNganh != null)
            {
                var lstMaCn = SelectedNNganh.SGiaTri.Split(',').ToList();
                predicate = predicate.And(x => lstMaCn.Contains(x.IIDMaDanhMuc));
            }
            var list = _danhMucService.FindByCondition(predicate).ToList();
            CNganhItems = _mapper.Map<ObservableCollection<DanhMucNganhModel>>(list);
            _selectedCNganh = null;
        }

        private void LoadNamLuyKe()
        {
            DanhMuc dmNamLuyKe = _danhMucService.FindByCode(MaDanhMuc.NAM_LUY_KE, _sessionService.Current.YearOfWork);
            if (dmNamLuyKe != null)
                bool.TryParse(dmNamLuyKe.SGiaTri, out _isNamLuyKe);
            else _isNamLuyKe = false;
        }

        private void LoadControlVisibility()
        {
            string lns = Model.SDslns;
            _listMLNS = _mlnsService.FindByListLnsDonVi(lns, _sessionService.Current.YearOfWork).ToList();
            _columnVisibility = new DivisionColumnVisibility();
            _columnVisibility.IsDisplayTuChi = _listMLNS.Any(x => x.BTuChi) && !_isAdjusted;
            _columnVisibility.IsDisplayTuChiDieuChinh = _listMLNS.Any(x => x.BTuChi) && _isAdjusted;
            _columnVisibility.IsDisplayHienVat = _listMLNS.Any(x => x.BHienVat) && !_isAdjusted;
            _columnVisibility.IsDisplayHienVatDieuChinh = _listMLNS.Any(x => x.BHienVat) && _isAdjusted;
            _columnVisibility.IsDisplayDuPhong = _listMLNS.Any(x => x.BDuPhong);
            _columnVisibility.IsDisplayHangMua = _listMLNS.Any(x => x.BHangMua) && !_isAdjusted;
            _columnVisibility.IsDisplayHangMuaDieuChinh = _listMLNS.Any(x => x.BHangMua) && _isAdjusted;
            _columnVisibility.IsDisplayHangNhap = _listMLNS.Any(x => x.BHangNhap) && !_isAdjusted;
            _columnVisibility.IsDisplayHangNhapDieuChinh = _listMLNS.Any(x => x.BHangNhap) && _isAdjusted;
            _columnVisibility.IsDisplayPhanCap = _listMLNS.Any(x => x.BPhanCap) && !_isAdjusted;
            _columnVisibility.IsDisplayPhanCapDieuChinh = _listMLNS.Any(x => x.BPhanCap) && _isAdjusted;

            DetailHelper.VisibilityBudgetTypeAdjusted = _isAdjusted ? Visibility.Visible : Visibility.Collapsed;
            DetailHelper.VisibilityBudgetTypeNoneAdjusted = !_isAdjusted ? Visibility.Visible : Visibility.Collapsed;
            OnPropertyChanged(nameof(ColumnVisibility));
        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Tất cả" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.DA_NHAN_DUTOAN, DisplayItem = "Đã nhận dự toán" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Đã nhập dữ liệu" });
            TypeDisplaysSelected = TypeDisplay.DA_NHAN_DUTOAN;
        }

        private void OnResetFilter()
        {
            DetailFilter = new EstimationDetailCriteria();
            SelectedLNS = string.Empty;
            if (_itemsView != null)
            {
                BeForeRefresh();
                _itemsView.Refresh();
                CalculateTotalParent();
            }
        }

        private void LoadNganhByDonVi()
        {
            DonVi donVi = _nsDonViService.FindByIdDonVi(_sessionService.Current.IdDonVi, _sessionService.Current.YearOfWork);
            if ((donVi.Loai == LoaiDonVi.ROOT || donVi.Loai == LoaiDonVi.NOI_BO) || donVi.BCoNSNganh)
            {
                var authenticationInfo = _mapper.Map<AuthenticationInfo>(_sessionService.Current);
                _listDanhMucNganh = _danhMucNganhService.FindAll(authenticationInfo).ToList();
            }
            var listIDAgency = Agencies.Select(y => y.ValueItem).ToList();
            _listDanhMucNganh = _listDanhMucNganh.Where(x => x.SGiaTri != null && x.SGiaTri.Split(",").Any(y => listIDAgency.Contains(y))).ToList();
            // _dicDonViNganh = _listDanhMucNganh.Where(x => Agencies.Select(y => y.ValueItem).Contains(x.SGiaTri)).GroupBy(x => x.SGiaTri).ToDictionary(x => x.Key, x => x.Select(x => x.IIDMaDanhMuc).ToList());
        }

        private List<string> DictionaryDanhMucNganh(string iDMaDonVi)
        {
            return _listDanhMucNganh.Select(x =>
            {
                if (x.SGiaTri.Split(",").Contains(iDMaDonVi))
                {
                    return x.IIDMaDanhMuc;
                }
                else
                {
                    return "";
                }
            }).Where(x => x != "").ToList();
        }

        private void LoadLNSIndexCondition()
        {
            List<DtChungTuChiTietModel> listLNS = Items.Where(x => string.IsNullOrEmpty(x.SL) &&
                string.IsNullOrEmpty(x.SK) &&
                string.IsNullOrEmpty(x.SM) &&
                string.IsNullOrEmpty(x.STm) &&
                string.IsNullOrEmpty(x.STtm) &&
                string.IsNullOrEmpty(x.SNg) &&
                string.IsNullOrEmpty(x.STng) &&
                !x.IsConLai).ToList();
            listLNS.Insert(0, new DtChungTuChiTietModel
            {
                SLns = string.Empty,
                SMoTa = "-- TẤT CẢ --"
            });
            BudgetCatalogItems = new ObservableCollection<DtChungTuChiTietModel>(listLNS);
            _budgetCatalogItemsView = CollectionViewSource.GetDefaultView(BudgetCatalogItems);
            _budgetCatalogItemsView.Filter = BudgetCatalogItemsFilter;
        }

        private void LoadData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                _isShowQuyetDinh = _sessionService.Current.IsQuanLyDonViCha;
                _searchCondition = new EstimationVoucherDetailCriteria
                {
                    VoucherId = Model.Id,
                    LNS = Model.SDslns,
                    YearOfWork = Model.INamLamViec,
                    YearOfBudget = Model.INamNganSach,
                    BudgetSource = Model.IIdMaNguonNganSach,
                    UserName = _sessionService.Current.Principal,
                    IsNamLuyKe = _isNamLuyKe,
                    IdDonVi = _sessionService.Current.IsQuanLyDonViCha ? Model.SDsidMaDonVi : _sessionService.Current.IdsDonViQuanLy,
                    IsGetAll = TypeDisplay.TAT_CA.Equals(TypeDisplaysSelected)
                };

                List<string> listNganh = new List<string>();
                if (Agencies.Count > 0 && Model.ILoaiChungTu.HasValue && VoucherType.NSBD_Key.Equals(Model.ILoaiChungTu.ToString()))
                {
                    listNganh = _listDanhMucNganh.Where(x => Agencies.Select(x => x.ValueItem).Any(y => x.SGiaTri.Split(",").Contains(y))).Select(x => x.IIDMaDanhMuc).ToList();
                }
                if (_isAdjusted)
                {
                    var listDataDieuChinhQuery = _dtChungTuChiTietService.FindChungTuChiTietDieuChinh(_searchCondition).ToList();
                    listDataDieuChinhQuery = listDataDieuChinhQuery.Where(n => string.IsNullOrEmpty(n.IIdMaDonVi) || (!string.IsNullOrEmpty(n.IIdMaDonVi) && n.FTuChiTruocDieuChinh != 0 || n.FTuChi != 0)).ToList();
                    if (_listDanhMucNganh.Any())
                    {
                        var listXauNoiMa = StringUtils.GetListXauNoiMaParent(listDataDieuChinhQuery.Where(x => !x.BHangCha && listNganh.Contains(x.SNg)).Select(x => x.SXauNoiMa).ToList());
                        listDataDieuChinhQuery = listDataDieuChinhQuery.Where(x => listXauNoiMa.Contains(x.SXauNoiMa) && x.BHangCha || (!x.BHangCha && (DictionaryDanhMucNganh(x.IIdMaDonVi).Any(y => y.Contains(x.SNg))))).ToList();
                    }

                    /*
                    if (_dicDonViNganh.Count > 0)
                    {
                        var listXauNoiMa = StringUtils.GetListXauNoiMaParent(listDataDieuChinhQuery.Where(x => !x.BHangCha && listNganh.Contains(x.SNg)).Select(x => x.SXauNoiMa).ToList());
                        listDataDieuChinhQuery = listDataDieuChinhQuery.Where(x => listXauNoiMa.Contains(x.SXauNoiMa) && x.BHangCha || (!x.BHangCha && (DictionaryDanhMucNganh(x.IIdMaDonVi).Any(y => y.Contains(x.SNg))))).ToList();
                    }
                    */
                    e.Result = _mapper.Map<List<DtChungTuChiTietModel>>(listDataDieuChinhQuery);
                }
                else
                {
                    var listDataQuery = _dtChungTuChiTietService.FindChungTuChiTiet(_searchCondition).ToList();
                    //if ((TypeDisplaysSelected == null || TypeDisplaysSelected != TypeDisplay.TAT_CA))
                    //{
                    //    var listChungTuNhan = _dtChungTuService.FindDotNhanByChungTuPhanBo(Model.Id).GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.SelectMany(y => y.SDslns.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)));
                    //    listDataQuery = listDataQuery.Where(x =>
                    //    {
                    //        if (x.IIdCtduToanNhan.HasValue && !x.IIdCtduToanNhan.Value.Equals(Guid.Empty))
                    //        {
                    //            return listChungTuNhan[x.IIdCtduToanNhan.Value].Contains(x.SLns);
                    //        }
                    //        else return true;
                    //    }).ToList();
                    //}

                    if (_listDanhMucNganh.Any())
                    {
                        var listXauNoiMa = StringUtils.GetListXauNoiMaParent(listDataQuery.Where(x => !x.BHangCha && listNganh.Contains(x.SNg)).Select(x => x.SXauNoiMa).ToList());
                        listDataQuery = listDataQuery.Where(x => listXauNoiMa.Contains(x.SXauNoiMa) && x.BHangCha || (!x.BHangCha && (DictionaryDanhMucNganh(x.IIdMaDonVi).Any(y => y.Contains(x.SNg))))).ToList();
                    }

                    /*
                    if (_dicDonViNganh.Count > 0)
                    {
                        var listXauNoiMa = StringUtils.GetListXauNoiMaParent(listDataQuery.Where(x => !x.BHangCha && listNganh.Contains(x.SNg)).Select(x => x.SXauNoiMa).ToList());
                        //listDataQuery = listDataQuery.Where(x => listXauNoiMa.Contains(x.SXauNoiMa) && x.BHangCha || (!x.BHangCha && (_dicDonViNganh.Keys.Contains(x.IIdMaDonVi) && _dicDonViNganh[x.IIdMaDonVi].Any(y => y.Contains(x.SNg))))).ToList();
                        listDataQuery = listDataQuery.Where(x => listXauNoiMa.Contains(x.SXauNoiMa) && x.BHangCha || (!x.BHangCha && (DictionaryDanhMucNganh(x.IIdMaDonVi).Any(y => y.Contains(x.SNg))))).ToList();
                    }
                    */
                    listDataQuery = listDataQuery.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.SSoQuyetDinh).ThenBy(x => x.IIdCtduToanNhan).ThenBy(x => x.IIdMaDonVi).ThenBy(x => x.IRowType).ToList();
                    e.Result = _mapper.Map<List<DtChungTuChiTietModel>>(listDataQuery);
                }
            }, (s, e) =>
            {
                IsLoading = false;
                var result = (List<DtChungTuChiTietModel>)e.Result;
                Items = new ObservableCollection<DtChungTuChiTietModel>(result);

                BeForeRefresh();
                _itemsView = CollectionViewSource.GetDefaultView(Items);
                _itemsView.Filter = ItemsViewFilter;

                if (Items != null)
                    SelectedItem = Items.FirstOrDefault(x => !x.IsHangCha);

                if (_isAdjusted)
                {
                    Items.Where(x => x.BEmpty && x.IRowType == (int)DuToanRowType.RowChiTiet && x.IsFilter).ForAll(x =>
                    {
                        x.CbxDonVi = ObjectCopier.Clone(Agencies);
                        x.CbxNhanPhanBos = ObjectCopier.Clone(CbxNhanPhanBos);
                    });
                    SettingEditable();
                    CalculateTotalParentAdjusted();
                }
                else
                {
                    CalculateTotalParent();
                }

                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
                LoadLNSIndexCondition();
                Items.Where(model => model.IsEditable).ForAll(model => model.PropertyChanged += DetailModel_PropertyChanged);
            });
        }

        private void LoadAgencies()
        {
            List<DonVi> listNsDonVi = new List<DonVi>();
            listNsDonVi = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Join(StringUtils.COMMA, new string[] { LoaiDonVi.NOI_BO, LoaiDonVi.ROOT })).ToList();

            if (listNsDonVi.Exists(x => x.Loai == LoaiDonVi.ROOT))
            {
                var predicate = PredicateBuilder.True<DonVi>();
                predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                predicate = predicate.And(x => x.Loai == SoChungTuType.EstimateDivision.ToString());
                predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);

                listNsDonVi = _nsDonViService.FindByCondition(predicate).ToList();
            }

            if (Model.ILoaiChungTu.HasValue && VoucherType.NSBD_Key.Equals(Model.ILoaiChungTu.ToString()))
            {
                listNsDonVi = listNsDonVi.Where(x => true.Equals(x.BCoNSNganh)).ToList();
            }
            // Remove 999 hard code
            listNsDonVi = listNsDonVi.Where(x => !x.IIDMaDonVi.Equals("999")).ToList();

            if (Model.ILoaiDuToan.HasValue && !BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDuToan.Value))
            {
                var listIdDonVi = string.IsNullOrEmpty(Model.SDsidMaDonVi) ? new List<string>() : Model.SDsidMaDonVi.Split(",").ToList();
                listNsDonVi = listNsDonVi.Where(x => listIdDonVi.Contains(x.IIDMaDonVi)).ToList();
            }

            Agencies = _mapper.Map<ObservableCollection<ComboboxItem>>(listNsDonVi);
        }

        private void LoadDotNhan()
        {
            List<NsDtChungTu> chungTus = _dtChungTuService.FindDotNhanByChungTuPhanBo(Model.Id).ToList();
            List<ComboboxItem> cbxChungTus = new List<ComboboxItem>();
            foreach (var chungTu in chungTus)
            {
                cbxChungTus.Add(new ComboboxItem { ValueItem = chungTu.Id.ToString(), DisplayItem = chungTu.SSoQuyetDinh });
            }
            CbxNhanPhanBos = new ObservableCollection<ComboboxItem>(cbxChungTus);
        }

        private bool BudgetCatalogItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is DtChungTuChiTietModel item && (item.SLns.StartsWith(_searchLNS, StringComparison.OrdinalIgnoreCase) || item.SMoTa.StartsWith(_searchLNS, StringComparison.OrdinalIgnoreCase));
        }

        private bool ItemsViewFilter(object obj)
        {
            var item = obj as DtChungTuChiTietModel;
            bool result = true;
            if (item.IsHangCha)
            {
                result = _mapCha.Contains(item.SXauNoiMa);
            } else
            {
                result = ChungTuChiTietItemsViewFilter(item);
                if (TypeDisplay.DA_NHAN_DUTOAN.Equals(TypeDisplaysSelected))
                {
                    result = result && _mapSQD.Contains((item.SXauNoiMa, item.IIdCtduToanNhan));
                }
                if (TypeDisplay.CO_DU_LIEU.Equals(TypeDisplaysSelected))
                {
                    result = result && (item.IRowType == 3 && HasInputData(item) || item.IRowType != 3);
                }
            }

            if (!TypeDisplay.TAT_CA.Equals(TypeDisplaysSelected) && (item.IRowType == 1 || item.IRowType == 2))
            {
                result = result && _mapSQD.Contains((item.SXauNoiMa, item.IIdCtduToanNhan));
            }

            item.IsFilter = result;
            return result;
        }

        private bool ChungTuChiTietItemsViewFilter(DtChungTuChiTietModel item)
        {
            bool result = true;
            if (!string.IsNullOrEmpty(SelectedLNS))
                result = result && item.SLns.ToLower().StartsWith(SelectedLNS.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.L))
                result = result && item.SL.ToLower().StartsWith(DetailFilter.L.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.K))
                result = result && item.SK.ToLower().StartsWith(DetailFilter.K.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.SM.ToLower().StartsWith(DetailFilter.M.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.STm.ToLower().StartsWith(DetailFilter.TM.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.STtm.ToLower().StartsWith(DetailFilter.TTM.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.SNg.ToLower().StartsWith(DetailFilter.NG.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TNG))
                result = result && item.STng.ToLower().StartsWith(DetailFilter.TNG.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TNG1))
                result = result && item.STng1.ToLower().StartsWith(DetailFilter.TNG1.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TNG2))
                result = result && item.STng2.ToLower().StartsWith(DetailFilter.TNG2.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TNG3))
                result = result && item.STng3.ToLower().StartsWith(DetailFilter.TNG3.ToLower().Trim());

            //if (TypeDisplay.DA_NHAN_DUTOAN.Equals(TypeDisplaysSelected))
            //{
            //    result = result && (item.IRowType == 1 && HasInputData(item));
            //    //result = result && (HasInputData(item) || (item.IsModified && (item.IIdDtchungTu == Guid.Empty || item.IIdDtchungTu == null) && !item.IsDeleted));
            //}
            //else if (TypeDisplay.CO_DU_LIEU.Equals(TypeDisplaysSelected))
            //{
            //    result = result && HasInputData(item);
            //    //result = result && !string.IsNullOrEmpty(item.SSoQuyetDinh) && ((item.IRowType != 3 || (item.IRowType == 3 && HasInputData(item))) || item.IsHangCha);
            //}

            if (SelectedNNganh != null)
            {
                result = result && SelectedNNganh.SGiaTri.Split(",").Contains(item.SNg);
            }
            if (SelectedCNganh != null)
            {
                result = result && SelectedCNganh.SGiaTri == item.SNg;
            }
            if (SelectedAgency != null)
            {
                result = result && SelectedAgency.ValueItem == item.IIdMaDonVi;
            }

            item.IsFilter = result;
            return result;
        }

        private void BeForeRefresh()
        {
            //_mapSQD = Items.Where(x => x.IRowType == 1 && !string.IsNullOrEmpty(x.SXauNoiMa) && x.IIdCtduToanNhan.HasValue).GroupBy(x => (x.SXauNoiMa, x.IIdCtduToanNhan)).Select(x => x.Key).ToHashSet();
            //var itemFull = Items.Where(item => ChungTuChiTietItemsViewFilter(item)).ToList();
            //var itemDetail = itemFull.Where(item => !item.IsHangCha).ToList();

            if (TypeDisplay.CO_DU_LIEU.Equals(TypeDisplaysSelected))
            {
                _mapSQD = Items
                    .Where(x => ChungTuChiTietItemsViewFilter(x))
                    .Where(x => !string.IsNullOrEmpty(x.SXauNoiMa) && x.IIdCtduToanNhan.HasValue && HasInputData(x))
                    .GroupBy(x => (x.SXauNoiMa, x.IIdCtduToanNhan)).Select(x => x.Key).ToHashSet();
                var xauNoiMa = Items
                    .Where(x => ChungTuChiTietItemsViewFilter(x))
                    .Where(x => x.IRowType == 3 && HasInputData(x)).Select(x => x.SXauNoiMa).Distinct().ToList();
                _mapCha = StringUtils.GetXauNoiMaParentOptimize(xauNoiMa);
            } else if (TypeDisplay.DA_NHAN_DUTOAN.Equals(TypeDisplaysSelected))
            {
                _mapSQD = Items
                    .Where(x => ChungTuChiTietItemsViewFilter(x))
                    .Where(x => x.IRowType == 1 && !string.IsNullOrEmpty(x.SXauNoiMa) && x.IIdCtduToanNhan.HasValue && HasInputData(x))
                    .GroupBy(x => (x.SXauNoiMa, x.IIdCtduToanNhan)).Select(x => x.Key).ToHashSet();
                var xauNoiMa = Items
                    .Where(x => ChungTuChiTietItemsViewFilter(x))
                    .Where(x => x.IRowType == 1 && HasInputData(x)).Select(x => x.SXauNoiMa).Distinct().ToList();
                _mapCha = StringUtils.GetXauNoiMaParentOptimize(xauNoiMa);
            } else
            {
                var xauNoiMa = Items
                    .Where(x => ChungTuChiTietItemsViewFilter(x))
                    .Select(x => x.SXauNoiMa).Distinct().ToList();
                _mapCha = StringUtils.GetXauNoiMaParentOptimize(xauNoiMa);
            }

            //if (!string.IsNullOrEmpty(TypeDisplaysSelected))
            //{
            //    if (TypeDisplaysSelected == TypeDisplay.DA_NHAN_DUTOAN)
            //    {
            //        _filterResultWithSQD = itemFull.Where(item => !item.IsHangCha || (item.IsHangCha && HasInputData(item))).Select(item => new Tuple<string, Guid?>(item.SXauNoiMa, item.IIdCtduToanNhan)).ToList();
            //        _filterResult = itemFull.Where(item => !item.IsHangCha || (item.IsHangCha && HasInputData(item))).ToList();
            //    }
            //    else if (TypeDisplaysSelected == TypeDisplay.CO_DU_LIEU)
            //    {
            //        _filterResultWithSQD = itemFull.Where(item => itemDetail.Select(n => n.SXauNoiMa).Contains(item.SXauNoiMa)).Select(item => new Tuple<string, Guid?>(item.SXauNoiMa, item.IIdCtduToanNhan)).ToList();
            //        _filterResult = itemFull.Where(item => itemDetail.Select(n => n.SXauNoiMa).Contains(item.SXauNoiMa)).ToList();
            //    }
            //    else
            //    {
            //        _filterResultWithSQD = itemFull.Select(item => new Tuple<string, Guid?>(item.SXauNoiMa, item.IIdCtduToanNhan)).ToList();
            //    }

            //}
            //xnmConcatenation = string.Join(StringUtils.SEMICOLON, _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        protected override void OnLockUnLock()
        {
            string msgConfirm = Model.BKhoa ? Resources.UnlockMultiChungTu : Resources.LockMultiChungTu;
            string msgDone = Model.BKhoa ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            var result = MessageBox.Show(msgConfirm, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _dtChungTuService.LockOrUnLock(Model.Id, !Model.BKhoa);
                Model.BKhoa = !Model.BKhoa;
                OnPropertyChanged(nameof(Model.BKhoa));
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
                MessageBox.Show(msgDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            // refresh dữ liệu ở màn index
            DataChangedEventHandler handler = UpdateVoucherEvent;

            if (handler != null)
            {
                handler(Model, new EventArgs());
            }
        }

        protected override void OnAdd()
        {
            if (_isNamLuyKe || (Model.ILoaiDuToan.HasValue && !BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDuToan.Value)))
            {
                MessageBox.Show("Thêm mới không khả dụng. Hãy thao tác với các bản ghi đang hiển thị trên lưới", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (SelectedItem != null)
            {
                int currentRow = Items.IndexOf(SelectedItem);
                int targetRow = Items.ToList().FindIndex(currentRow, x => x.IsEditable);
                if (targetRow > -1)
                {
                    DtChungTuChiTietModel sourceItem = Items.ElementAt(targetRow);
                    DtChungTuChiTietModel targetItem = CloneRow(sourceItem);
                    OnPropertyChanged(nameof(targetItem));
                    targetItem.PropertyChanged += DetailModel_PropertyChanged;
                    Items.Insert(targetRow + 1, targetItem);

                    OnPropertyChanged(nameof(Items));
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IsDeleteAll));
                }
            }
        }

        private DtChungTuChiTietModel CloneRow(DtChungTuChiTietModel sourceItem)
        {
            DtChungTuChiTietModel targetItem = ObjectCopier.Clone(sourceItem);

            targetItem.Id = Guid.Empty;
            targetItem.IIdDtchungTu = null;
            targetItem.FTuChiTruocDieuChinh = 0;
            targetItem.FTuChi = 0;
            targetItem.FRutKBNN = 0;
            targetItem.FTuChiSauDieuChinh = 0;
            targetItem.FHienVatTruocDieuChinh = 0;
            targetItem.FHienVat = 0;
            targetItem.FHienVatSauDieuChinh = 0;
            targetItem.FHangNhapTruocDieuChinh = 0;
            targetItem.FHangNhap = 0;
            targetItem.FHangNhapSauDieuChinh = 0;
            targetItem.FHangMuaTruocDieuChinh = 0;
            targetItem.FHangMua = 0;
            targetItem.FHangMuaSauDieuChinh = 0;
            targetItem.FPhanCapTruocDieuChinh = 0;
            targetItem.FPhanCap = 0;
            targetItem.FPhanCapSauDieuChinh = 0;
            targetItem.FDuPhong = 0;
            targetItem.SGhiChu = string.Empty;
            targetItem.IsModified = true;
            targetItem.IIdMaDonVi = string.Empty;
            targetItem.STenDonVi = string.Empty;
            targetItem.SDotPhanBoTruoc = string.Empty;
            targetItem.IIdCtduToanNhan = sourceItem.IIdCtduToanNhan;
            targetItem.BEmpty = true;
            targetItem.CbxDonVi = ObjectCopier.Clone(Agencies);
            targetItem.CbxNhanPhanBos = ObjectCopier.Clone(CbxNhanPhanBos);
            return targetItem;
        }

        protected override void OnRefresh()
        {
            if (IsSaveData)
            {
                var result = MessageBox.Show(Resources.ConfirmReloadData, Resources.ConfirmTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
                if (result == MessageBoxResult.Cancel)
                    return;
                else if (result == MessageBoxResult.Yes)
                    OnSaveData();
            }
            LoadData();
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(DtChungTuChiTietModel.FTuChi)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FRutKBNN)
                || args.PropertyName == nameof(DtChungTuChiTietModel.SGhiChu)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FHienVat)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FHangNhap)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FHangMua)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FPhanCap)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FDuPhong)
                || args.PropertyName == nameof(DtChungTuChiTietModel.IIdCtduToanNhan))
            {
                DtChungTuChiTietModel item = (DtChungTuChiTietModel)sender;
                item.IsModified = true;
                if (args.PropertyName == nameof(DtChungTuChiTietModel.FTuChi)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FRutKBNN)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FHienVat)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FHangNhap)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FHangMua)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FPhanCap)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FDuPhong)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.IIdCtduToanNhan))
                {
                    if (IsFillDataDauNam)
                    {
                        CalculateSelf(item);
                        if (_isAdjusted)
                        {
                            SettingEditable();
                            CalculateTotalParentAdjusted();
                        }
                        else
                        {
                            CalculateTotalParent();
                        }
                    }
                }
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void OnSaveData()
        {
            List<DtChungTuChiTietModel> listChungTuChiTietAdd = Items.Where(x => x.IsEditable && x.IsModified && (x.IIdDtchungTu == Guid.Empty || x.IIdDtchungTu == null)).ToList();
            listChungTuChiTietAdd = listChungTuChiTietAdd.Where(n => n.FTonKho != 0 || n.FTuChi != 0 || n.FRutKBNN != 0 || n.FDuPhong != 0 || n.FHangMua != 0 || n.FHangNhap != 0 || n.FPhanCap != 0 || n.FHienVat != 0).ToList();
            List<DtChungTuChiTietModel> listChungTuChiTietUpdate = Items.Where(x => x.IsEditable && x.IsModified && x.IIdDtchungTu != Guid.Empty && x.IIdDtchungTu != null).ToList();
            List<DtChungTuChiTietModel> listChungTuChiTietDelete = Items.Where(x => x.IsDeleted && x.IIdDtchungTu != null).ToList();

            if (IsAdjusted)
            {
                var listSDsidMaDonVi = Items.Where(x => (x.FTuChi != 0 || x.FRutKBNN != 0 || x.FHienVat != 0) && !string.IsNullOrEmpty(x.IIdMaDonVi)).Select(n => n.IIdMaDonVi).Distinct();
                Model.SDsidMaDonVi = string.Join(",", listSDsidMaDonVi);
                NsDtChungTu entity = _dtChungTuService.FindById(Model.Id);
                _mapper.Map(Model, entity);
                entity.DNgaySua = DateTime.Now;
                entity.SNguoiSua = _sessionService.Current.Principal;
                _dtChungTuService.Update(entity);
            }

            // Thêm mới chứng từ chi tiết
            if (listChungTuChiTietAdd.Count > 0)
            {
                if (listChungTuChiTietAdd.Any(x => !x.IIdCtduToanNhan.HasValue || string.IsNullOrEmpty(x.IIdMaDonVi)))
                {
                    MessageBox.Show(Resources.AlertEmptyDonViAndSoQuyetDinh, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var duplicateDvSqd = Items.Where(x => x.IRowType == (int)DuToanRowType.RowChiTiet && listChungTuChiTietAdd.Select(y => y.IIdMlns).Contains(x.IIdMlns))
                                            .GroupBy(x => new { x.IIdMaDonVi, x.IIdCtduToanNhan, x.IIdMlns })
                                            .Where(x => x.Count() > 1)
                                            .Select(x => x.Key).ToList();
                if (duplicateDvSqd.Count > 0)
                {
                    MessageBox.Show(Resources.AlertDuplicateDonViAndSoQuyetDinh, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                listChungTuChiTietAdd = listChungTuChiTietAdd.Select(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.IIdDtchungTu = Model.Id;
                    x.INamNganSach = Model.INamNganSach;
                    x.INamLamViec = Model.INamLamViec;
                    x.IIdMaNguonNganSach = Model.IIdMaNguonNganSach;
                    x.IPhanCap = NSDuToan.IPHANCAP_PHANBO_DUTOAN;
                    return x;
                }).ToList();

                List<NsDtChungTuChiTiet> listChungTuChiTiets = _mapper.Map<List<NsDtChungTuChiTiet>>(listChungTuChiTietAdd);
                _dtChungTuChiTietService.AddRange(listChungTuChiTiets);
            }

            // Cập nhật chứng từ chi tiết
            if (listChungTuChiTietUpdate.Count > 0)
            {
                foreach (var item in listChungTuChiTietUpdate.Where(n => n.FTonKho != 0 || n.FTuChi != 0 || n.FRutKBNN != 0 || n.FDuPhong != 0 || n.FHangMua != 0 || n.FHangNhap != 0 || n.FPhanCap != 0 || n.FHienVat != 0))
                {
                    NsDtChungTuChiTiet chungTuChiTiet = _dtChungTuChiTietService.FindById(item.Id);
                    _mapper.Map(item, chungTuChiTiet);
                    chungTuChiTiet.IPhanCap = NSDuToan.IPHANCAP_PHANBO_DUTOAN;
                    chungTuChiTiet.INamNganSach = Model.INamNganSach;
                    chungTuChiTiet.INamLamViec = Model.INamLamViec;
                    chungTuChiTiet.IIdMaNguonNganSach = Model.IIdMaNguonNganSach;
                    _dtChungTuChiTietService.Update(chungTuChiTiet);
                    // Reset flag
                    item.IsModified = false;
                }
                _dtChungTuChiTietService.DeleteByIds(listChungTuChiTietUpdate.Where(n => n.FTonKho == 0 && n.FTuChi == 0 && n.FRutKBNN == 0 && n.FDuPhong == 0 && n.FHangMua == 0 && n.FHangNhap == 0 && n.FPhanCap == 0 && n.FHienVat == 0).Select(x => x.Id.ToString()));
            }

            // Xóa chứng từ chi tiết
            _dtChungTuChiTietService.DeleteByIds(listChungTuChiTietDelete.Select(x => x.Id.ToString()));
            foreach (var item in listChungTuChiTietDelete)
            {
                ResetItemValue(item);
                item.SGhiChu = string.Empty;
                item.IsModified = false;
                item.IsDeleted = false;
                item.IIdDtchungTu = Guid.Empty;
            }
            // Reset value has changed
            Items.Select(x => { x.IsModified = false; x.IsDeleted = false; return x; }).ToList();

            //Cập nhật thông tin chứng từ
            UpdateChungTu();

            MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);

            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));

            //refresh dữ liệu ở màn index
            DataChangedEventHandler handler = UpdateVoucherEvent;

            if (handler != null)
            {
                handler(Model, new EventArgs());
            }
        }


        protected override bool CanDelete(object obj)
        {
            return !Model.BKhoa;
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha && !SelectedItem.IsPhanBo && !SelectedItem.IsConLai)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;

                SelectedItem.CbxDonVi?.ForAll(x => x.IsDeleted = SelectedItem.IsDeleted);
                SelectedItem.CbxNhanPhanBos?.ForAll(x => x.IsDeleted = SelectedItem.IsDeleted);

                CalculateSelf(SelectedItem);
                CalculateTotalParent();
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void FillDataDauNam()
        {
            if (!Model.ILoaiDuToan.HasValue || !BudgetType.YEAR.Equals((BudgetType)Model.ILoaiDuToan.Value))
            {
                return;
            }

            if (!Model.ILoaiChungTu.HasValue)
            {
                return;
            }

            var searchPredicate = new EstimationVoucherDetailCriteria
            {
                YearOfWork = Model.INamLamViec,
                YearOfBudget = Model.INamNganSach,
                BudgetSource = Model.IIdMaNguonNganSach,
                ILoai = 3,
                LoaiChungTu = Model.ILoaiChungTu.Value,
                IdDonVi = string.Join(",", Agencies.Select(x => x.ValueItem))
            };

            IEnumerable<SktSoLieuChiTietMLNSBudget> listDataDetail = _sktSoLieuService.FindForFillBudget(searchPredicate, "sp_skt_so_lieu_chi_tiet_mlns_for_fill_budget").ToList();

            if (!listDataDetail.Any())
            {
                return;
            }

            var dataGroupByMlns = listDataDetail
                .GroupBy(skt => skt.MlnsId.ToString())
                .ToDictionary(g => g.Key,
                    g => g.GroupBy(e => e.IdDonVi).ToDictionary(e => e.Key, e => e.ToList()));
            IsFillDataDauNam = false;
            Items.Where(item => item.IsEditable).ForAll(item =>
            {
                var dictByMlns = dataGroupByMlns.GetValueOrDefault(item.IIdMlns.ToString(), new Dictionary<string, List<SktSoLieuChiTietMLNSBudget>>());
                var dataGroupByIdDonVi = dictByMlns.GetValueOrDefault(item.IIdMaDonVi, new List<SktSoLieuChiTietMLNSBudget>());
                if (dataGroupByIdDonVi.Count > 0)
                {
                    item.FTuChi = dataGroupByIdDonVi.Sum(x => x.TuChi);
                    item.FRutKBNN = dataGroupByIdDonVi.Sum(x => x.RutKBNN);
                    item.FHienVat = dataGroupByIdDonVi.Sum(x => x.HienVat);
                    item.FHangNhap = dataGroupByIdDonVi.Sum(x => x.HangNhap);
                    item.FHangMua = dataGroupByIdDonVi.Sum(x => x.HangMua);
                    item.FPhanCap = dataGroupByIdDonVi.Sum(x => x.PhanCap);
                    item.FDuPhong = dataGroupByIdDonVi.Sum(x => x.DuPhong.HasValue ? x.DuPhong.Value : 0);
                }
            });
            IsFillDataDauNam = true;

            CalculateTotalParent();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            BeForeRefresh();
            _itemsView.Refresh();
        }

        private void FillDataDieuChinh()
        {
            var searchCriteria = new EstimationVoucherDetailCriteria
            {
                YearOfWork = Model.INamLamViec,
                YearOfBudget = Model.INamNganSach,
                BudgetSource = Model.IIdMaNguonNganSach,
                LNS = Model.SDslns,
                IdDonVi = Model.SDsidMaDonVi,
                LoaiChungTu = Model.ILoaiChungTu.Value,
                VoucherDate = Model.DNgayQuyetDinh.HasValue ? Model.DNgayQuyetDinh.Value : Model.DNgayChungTu.Value,
                UserName = _sessionInfo.Principal
            };
            var listDataDieuChinh = _dcChungTuChiTietService.FindDataDieuChinh(searchCriteria).ToList();
            if (listDataDieuChinh.Count == 0)
                MessageBoxHelper.Info(Resources.MsgNoDataAdjustedEstimate);
            foreach (var dieuChinh in listDataDieuChinh)
            {
                var item = Items.Where(x => x.SXauNoiMa == dieuChinh.SXauNoiMa && x.IIdMaDonVi == dieuChinh.IIdMaDonVi).FirstOrDefault();
                if (item != null)
                {
                    var value = dieuChinh.FDuKienQtDauNam.Value + dieuChinh.FDuKienQtCuoiNam.Value - dieuChinh.FDuToanNganSachNam.Value;
                    if ((ColumnVisibility.IsDisplayTuChi || ColumnVisibility.IsDisplayTuChiDieuChinh) && item.IsEditTuChi)
                        item.FTuChi = value;
                    else if (item.SLns == "1040200" && (ColumnVisibility.IsDisplayHangNhap || ColumnVisibility.IsDisplayHangNhapDieuChinh) && item.IsEditHangNhap)
                        item.FHangNhap = value;
                    else if (item.SLns == "1040300" && (ColumnVisibility.IsDisplayHangMua || ColumnVisibility.IsDisplayHangMuaDieuChinh) && item.IsEditHangMua)
                        item.FHangMua = value;
                }
            }
            CalculateTotalParent();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
        }

        private void CountParentChild(List<DtChungTuChiTietModel> listChild, DtChungTuChiTietModel parent)
        {
            if (parent == null) return;
            parent.FRutKBNN = listChild.Sum(n => n.FRutKBNN);
            parent.FTuChi = listChild.Sum(n => n.FTuChi);
            parent.FHienVat = listChild.Sum(n => n.FHienVat);
            parent.FHangNhap = listChild.Sum(n => n.FHangNhap);
            parent.FHangMua = listChild.Sum(n => n.FHangMua);
            parent.FDuPhong = listChild.Sum(n => n.FDuPhong);
            parent.FPhanCap = listChild.Sum(n => n.FPhanCap);
            if (_isAdjusted)
            {
                parent.FTuChiTruocDieuChinh = listChild.Sum(n => n.FTuChiTruocDieuChinh);
                parent.FTuChiSauDieuChinh = listChild.Sum(n => n.FTuChiSauDieuChinh);
            }
        }


        public void OnPhanBoConLai()
        {
            FirstTimePhanBo = true;
            foreach (var conLai in Items)
            {
                if (conLai.IRowType == (int)DuToanRowType.RowConLai && conLai.FTuChi != 0)
                {
                    var dv =
                        Items.FirstOrDefault(x => x.IsFilter && x.IIdMlnsCha == conLai.IIdMlnsCha && x.IIdMlns == conLai.IIdMlns && x.IIdMaDonVi.Equals(_selectedAgency.ValueItem) && conLai.IIdCtduToanNhan.Equals(x.IIdCtduToanNhan));
                    if (dv != null)
                    {
                        dv.FTuChi += conLai.FTuChi;
                        dv.IsModified = true;
                    }
                }
            }
            CalculateTotalParent();
            FirstTimePhanBo = false;
        }

        private void CalculateTotalParent()
        {
            var dictItem = Items.Where(x => x.IIdMlns.HasValue).GroupBy(x => x.IIdMlns).ToDictionary(x => x.Key, x => x.FirstOrDefault());
            Items.Where(x => x.IsHangCha && x.IsFilter && x.IRowType == (int)DuToanRowType.RowCha).ForAll(x => ResetItemValue(x));

            var temps = Items.Where(x => x.IsEditable && x.IsFilter && HasInputData(x) && x.IRowType == (int)DuToanRowType.RowChiTiet).GroupBy(n => n.IIdMlnsCha).ToList();

            var listParent = temps.Select(x =>
            {
                _ = dictItem.TryGetValue(x.Key, out var parent);
                CountParentChild(x.ToList(), parent);
                return parent;
            }).Where(x => x != null).ToList();

            CalculateReverse(listParent, dictItem);

            CalculateTotal();

            if (!_sessionService.Current.IdsDonViQuanLy.Split(StringUtils.COMMA).Any(x => _nsDonViService.FindByIdDonVi(x, _sessionService.Current.YearOfWork)?.Loai.Equals(LoaiDonVi.ROOT) ?? false))
            {
                var temps2 = Items.Where(x => x.IRowType == (int)DuToanRowType.RowChiTiet || x.IRowType == (int)DuToanRowType.RowNhanPhanBoOrTong).GroupBy(n => new { n.IIdMlnsCha, n.SXauNoiMa }).ToList();

                foreach (var temp in temps2)
                {
                    var listChild = temp.ToList();
                    CountParentChild(listChild.Skip(1).ToList(), listChild.FirstOrDefault());
                }
            }
        }

        private void CalculateReverse(List<DtChungTuChiTietModel> items, Dictionary<Guid?, DtChungTuChiTietModel> dictItem)
        {
            if (items == null || !items.Any()) return;
            var temps = items.Where(x => x.IIdMlnsCha.HasValue).GroupBy(n => n.IIdMlnsCha).ToList();

            var listParent = temps.Select(x =>
            {
                var listChild = Items.Where(y => y.IsFilter
                    && y.IIdMlnsCha == x.Key
                    && y.IRowType != (int)DuToanRowType.RowConLai
                    && y.IRowType != (int)DuToanRowType.RowNhanPhanBoOrTong).ToList();
                _ = dictItem.TryGetValue(x.Key, out var parent);
                CountParentChild(listChild, parent);
                return parent;
            }).Where(x => x != null).ToList();

            CalculateReverse(listParent, dictItem);
        }


        private void CalculateTotal()
        {
            DetailTotal.TotalTuChi = 0;
            DetailTotal.TotalHienVat = 0;
            DetailTotal.TotalHangNhap = 0;
            DetailTotal.TotalHangMua = 0;
            DetailTotal.TotalPhanCap = 0;
            DetailTotal.TotalDuPhong = 0;

            DetailTotal.TotalTuChiTruocDieuChinh = 0;
            DetailTotal.TotalTuChiDieuChinh = 0;
            DetailTotal.TotalTuChiSauDieuChinh = 0;
            DetailTotal.TotalHienVatTruocDieuChinh = 0;
            DetailTotal.TotalHienVatDieuChinh = 0;
            DetailTotal.TotalHienVatSauDieuChinh = 0;
            DetailTotal.TotalHangNhapTruocDieuChinh = 0;
            DetailTotal.TotalHangNhapDieuChinh = 0;
            DetailTotal.TotalHangNhapSauDieuChinh = 0;
            DetailTotal.TotalHangMuaTruocDieuChinh = 0;
            DetailTotal.TotalHangMuaDieuChinh = 0;
            DetailTotal.TotalHangMuaSauDieuChinh = 0;
            DetailTotal.TotalPhanCapTruocDieuChinh = 0;
            DetailTotal.TotalPhanCapDieuChinh = 0;
            DetailTotal.TotalPhanCapSauDieuChinh = 0;

            var listMlnsId = Items.Select(n => n.IIdMlns).ToList();

            //var listChildren = Items.Where(x => x.IsEditable && x.IsFilter && HasInputData(x) && x.IRowType == (int)DuToanRowType.RowChiTiet).ToList();
            var listChildren = Items.Where(x => x.IRowType == (int)DuToanRowType.RowCha && !x.IsDeleted && !x.IIdMlnsCha.HasValue).ToList();
            //List<AllocationDetailModel> listChildren = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();

            if (listChildren.Count > 1)
            {
                DetailTotal.TotalTuChi = listChildren.Sum(x => x.FTuChi);
                DetailTotal.TotalRutKBNN = listChildren.Sum(x => x.FRutKBNN);
                DetailTotal.TotalHienVat = listChildren.Sum(x => x.FHienVat);
                DetailTotal.TotalHangNhap = listChildren.Sum(x => x.FHangNhap);
                DetailTotal.TotalHangMua = listChildren.Sum(x => x.FHangMua);
                DetailTotal.TotalPhanCap = listChildren.Sum(x => x.FPhanCap);
                DetailTotal.TotalDuPhong = listChildren.Sum(x => x.FDuPhong);
                if (_isAdjusted)
                {
                    DetailTotal.TotalTuChiTruocDieuChinh = listChildren.Sum(x => x.FTuChiTruocDieuChinh);
                    DetailTotal.TotalTuChiDieuChinh = listChildren.Sum(x => x.FTuChi);
                    DetailTotal.TotalTuChiSauDieuChinh = listChildren.Sum(x => x.FTuChiSauDieuChinh);
                    DetailTotal.TotalHienVatTruocDieuChinh = listChildren.Sum(x => x.FHienVatTruocDieuChinh);
                    DetailTotal.TotalHienVatDieuChinh = listChildren.Sum(x => x.FHienVat);
                    DetailTotal.TotalHienVatSauDieuChinh = listChildren.Sum(x => x.FHienVatSauDieuChinh);
                    DetailTotal.TotalHangNhapTruocDieuChinh = listChildren.Sum(x => x.FHangNhapTruocDieuChinh);
                    DetailTotal.TotalHangNhapDieuChinh = listChildren.Sum(x => x.FHangNhap);
                    DetailTotal.TotalHangNhapSauDieuChinh = listChildren.Sum(x => x.FHangNhapSauDieuChinh);
                    DetailTotal.TotalHangMuaTruocDieuChinh = listChildren.Sum(x => x.FHangMuaTruocDieuChinh);
                    DetailTotal.TotalHangMuaDieuChinh = listChildren.Sum(x => x.FHangMua);
                    DetailTotal.TotalHangMuaSauDieuChinh = listChildren.Sum(x => x.FHangMuaSauDieuChinh);
                    DetailTotal.TotalPhanCapTruocDieuChinh = listChildren.Sum(x => x.FPhanCapTruocDieuChinh);
                    DetailTotal.TotalPhanCapDieuChinh = listChildren.Sum(x => x.FPhanCap);
                    DetailTotal.TotalPhanCapSauDieuChinh = listChildren.Sum(x => x.FPhanCapSauDieuChinh);
                }
                //foreach (DtChungTuChiTietModel item in listChildren)
                //{
                //    DetailTotal.TotalTuChi += item.FTuChi;
                //    DetailTotal.TotalHienVat += item.FHienVat;
                //    DetailTotal.TotalHangNhap += item.FHangNhap;
                //    DetailTotal.TotalHangMua += item.FHangMua;
                //    DetailTotal.TotalPhanCap += item.FPhanCap;
                //    DetailTotal.TotalDuPhong += item.FDuPhong;
                //    if (_isAdjusted)
                //    {
                //        DetailTotal.TotalTuChiTruocDieuChinh += item.FTuChiTruocDieuChinh;
                //        DetailTotal.TotalTuChiDieuChinh += item.FTuChi;
                //        DetailTotal.TotalTuChiSauDieuChinh += item.FTuChiSauDieuChinh;
                //        DetailTotal.TotalHienVatTruocDieuChinh += item.FHienVatTruocDieuChinh;
                //        DetailTotal.TotalHienVatDieuChinh += item.FHienVat;
                //        DetailTotal.TotalHienVatSauDieuChinh += item.FHienVatSauDieuChinh;
                //        DetailTotal.TotalHangNhapTruocDieuChinh += item.FHangNhapTruocDieuChinh;
                //        DetailTotal.TotalHangNhapDieuChinh += item.FHangNhap;
                //        DetailTotal.TotalHangNhapSauDieuChinh += item.FHangNhapSauDieuChinh;
                //        DetailTotal.TotalHangMuaTruocDieuChinh += item.FHangMuaTruocDieuChinh;
                //        DetailTotal.TotalHangMuaDieuChinh += item.FHangMua;
                //        DetailTotal.TotalHangMuaSauDieuChinh += item.FHangMuaSauDieuChinh;
                //        DetailTotal.TotalPhanCapTruocDieuChinh += item.FPhanCapTruocDieuChinh;
                //        DetailTotal.TotalPhanCapDieuChinh += item.FPhanCap;
                //        DetailTotal.TotalPhanCapSauDieuChinh += item.FPhanCapSauDieuChinh;
                //    }
                //}
            }
            else
            {
                var child = Items.FirstOrDefault(x => x.IRowType == (int)DuToanRowType.RowCha && !x.IsDeleted);
                if (child != null)
                {
                    DetailTotal.TotalTuChi = child.FTuChi;
                    DetailTotal.TotalRutKBNN = child.FRutKBNN;
                    DetailTotal.TotalHienVat = child.FHienVat;
                    DetailTotal.TotalHangNhap = child.FHangNhap;
                    DetailTotal.TotalHangMua = child.FHangMua;
                    DetailTotal.TotalPhanCap = child.FPhanCap;
                    DetailTotal.TotalDuPhong = child.FDuPhong;
                    if (_isAdjusted)
                    {
                        DetailTotal.TotalTuChiTruocDieuChinh = child.FTuChiTruocDieuChinh;
                        DetailTotal.TotalTuChiDieuChinh = child.FTuChi;
                        DetailTotal.TotalTuChiSauDieuChinh = child.FTuChiSauDieuChinh;
                        DetailTotal.TotalHienVatTruocDieuChinh = child.FHienVatTruocDieuChinh;
                        DetailTotal.TotalHienVatDieuChinh = child.FHienVat;
                        DetailTotal.TotalHienVatSauDieuChinh = child.FHienVatSauDieuChinh;
                        DetailTotal.TotalHangNhapTruocDieuChinh = child.FHangNhapTruocDieuChinh;
                        DetailTotal.TotalHangNhapDieuChinh = child.FHangNhap;
                        DetailTotal.TotalHangNhapSauDieuChinh = child.FHangNhapSauDieuChinh;
                        DetailTotal.TotalHangMuaTruocDieuChinh = child.FHangMuaTruocDieuChinh;
                        DetailTotal.TotalHangMuaDieuChinh = child.FHangMua;
                        DetailTotal.TotalHangMuaSauDieuChinh = child.FHangMuaSauDieuChinh;
                        DetailTotal.TotalPhanCapTruocDieuChinh = child.FPhanCapTruocDieuChinh;
                        DetailTotal.TotalPhanCapDieuChinh = child.FPhanCap;
                        DetailTotal.TotalPhanCapSauDieuChinh = child.FPhanCapSauDieuChinh;
                    }
                }

            }


        }

        private void CalculateTotalParentAdjusted()
        {
            Items.Where(x => x.IsHangCha && x.IsFilter && x.IRowType == (int)DuToanRowType.RowNhanPhanBoOrTong).ForAll(x => ResetItemValue(x));
            var temps = Items.Where(x => x.IsEditable && x.IsFilter && HasInputData(x) && x.IRowType == (int)DuToanRowType.RowChiTiet).GroupBy(n => n.IIdMlnsCha).ToList();
            var dictItem = Items.Where(x => x.IIdMlns.HasValue).GroupBy(x => x.IIdMlns).ToDictionary(x => x.Key, x => x.FirstOrDefault());


            var listParent = new List<DtChungTuChiTietModel>();
            foreach (var temp in temps)
            {
                var listChild = temp.ToList();
                _ = dictItem.TryGetValue(temp.Key, out var parent);
                if (parent != null)
                {
                    CountParentChild(listChild, parent);
                    listParent.Add(parent);
                }
            }
            if (listParent.Count > 0)
            {
                CalculateReverse(listParent, dictItem);
            }

            var temps2 = Items.Where(x => x.IRowType == (int)DuToanRowType.RowChiTiet || x.IRowType == (int)DuToanRowType.RowNhanPhanBoOrTong).GroupBy(n => new { n.IIdMlnsCha, n.SXauNoiMa }).ToList();

            foreach (var temp in temps2)
            {
                var listChild = temp.ToList();
                CountParentChildAdjusted(listChild.Skip(1).ToList(), listChild.FirstOrDefault());
            }
        }



        private void CountParentChildAdjusted(List<DtChungTuChiTietModel> listChild, DtChungTuChiTietModel parent)
        {
            if (parent == null) return;
            parent.FTuChi = listChild.Sum(n => n.FTuChi);
            parent.FRutKBNN = listChild.Sum(n => n.FRutKBNN);
            parent.FTuChiTruocDieuChinh = listChild.Sum(n => n.FTuChiTruocDieuChinh);
            parent.FTuChiSauDieuChinh = listChild.Sum(n => n.FTuChiSauDieuChinh);

            parent.FHienVat = listChild.Sum(n => n.FHienVat);
            parent.FHienVatTruocDieuChinh = listChild.Sum(n => n.FHienVatTruocDieuChinh);
            parent.FHienVatSauDieuChinh = listChild.Sum(n => n.FHienVatSauDieuChinh);

            parent.FHangNhap = listChild.Sum(n => n.FHangNhap);
            parent.FHangNhapTruocDieuChinh = listChild.Sum(n => n.FHangNhapTruocDieuChinh);
            parent.FHangNhapSauDieuChinh = listChild.Sum(n => n.FHangNhapSauDieuChinh);

            parent.FHangMua = listChild.Sum(n => n.FHangMua);
            parent.FHangMuaTruocDieuChinh = listChild.Sum(n => n.FHangMuaTruocDieuChinh);
            parent.FHangMuaSauDieuChinh = listChild.Sum(n => n.FHangMuaSauDieuChinh);

            parent.FPhanCap = listChild.Sum(n => n.FPhanCap);
            parent.FPhanCapTruocDieuChinh = listChild.Sum(n => n.FPhanCapTruocDieuChinh);
            parent.FPhanCapSauDieuChinh = listChild.Sum(n => n.FPhanCapSauDieuChinh);

            parent.FDuPhong = listChild.Sum(n => n.FDuPhong);
        }

        private DtChungTuChiTietModel ResetItemValue(DtChungTuChiTietModel item)
        {
            item.FTuChi = 0;
            item.FRutKBNN = 0;
            item.FTuChiTruocDieuChinh = 0;
            item.FTuChiSauDieuChinh = 0;

            item.FHienVat = 0;
            item.FHienVatTruocDieuChinh = 0;
            item.FHienVatSauDieuChinh = 0;

            item.FHangNhap = 0;
            item.FHangNhapTruocDieuChinh = 0;
            item.FHangNhapSauDieuChinh = 0;

            item.FHangMua = 0;
            item.FHangMuaTruocDieuChinh = 0;
            item.FHangMuaSauDieuChinh = 0;

            item.FPhanCap = 0;
            item.FPhanCapTruocDieuChinh = 0;
            item.FPhanCapSauDieuChinh = 0;

            item.FDuPhong = 0;
            item.SGhiChu = string.Empty;
            return item;
        }

        private void CalculateParentAdjusted(DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem)
        {
            var parrentItem = Items.Where(x => x.IIdMlns == currentItem.IIdMlns && x.Id != currentItem.Id && x.IRowType == (int)DuToanRowType.RowNhanPhanBoOrTong).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FTuChi += seftItem.FTuChi;
            parrentItem.FRutKBNN += seftItem.FRutKBNN;
            parrentItem.FTuChiTruocDieuChinh += seftItem.FTuChiTruocDieuChinh;
            parrentItem.FTuChiSauDieuChinh += seftItem.FTuChiSauDieuChinh;

            parrentItem.FHienVat += seftItem.FHienVat;
            parrentItem.FHienVatTruocDieuChinh += seftItem.FHienVatTruocDieuChinh;
            parrentItem.FHienVatSauDieuChinh += seftItem.FHienVatSauDieuChinh;

            parrentItem.FHangNhap += seftItem.FHangNhap;
            parrentItem.FHangNhapTruocDieuChinh += seftItem.FHangNhapTruocDieuChinh;
            parrentItem.FHangNhapSauDieuChinh += seftItem.FHangNhapSauDieuChinh;

            parrentItem.FHangMua += seftItem.FHangMua;
            parrentItem.FHangMuaTruocDieuChinh += seftItem.FHangMuaTruocDieuChinh;
            parrentItem.FHangMuaSauDieuChinh += seftItem.FHangMuaSauDieuChinh;

            parrentItem.FPhanCap += seftItem.FPhanCap;
            parrentItem.FPhanCapTruocDieuChinh += seftItem.FPhanCapTruocDieuChinh;
            parrentItem.FPhanCapSauDieuChinh += seftItem.FPhanCapSauDieuChinh;

            parrentItem.FDuPhong += seftItem.FDuPhong;

            CalculateParentAdjusted(parrentItem, seftItem);
        }

        private void CalculateParent(DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem)
        {
            var parrentItem = Items.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FTuChi += seftItem.FTuChi;
            parrentItem.FRutKBNN += seftItem.FRutKBNN;
            parrentItem.FHienVat += seftItem.FHienVat;
            parrentItem.FHangNhap += seftItem.FHangNhap;
            parrentItem.FHangMua += seftItem.FHangMua;
            parrentItem.FPhanCap += seftItem.FPhanCap;
            parrentItem.FDuPhong += seftItem.FDuPhong;
            CalculateParent(parrentItem, seftItem);
        }

        private void CalculateSelf(DtChungTuChiTietModel item)
        {
            // BudgetType.ADJUSTED
            item.FTuChiSauDieuChinh = item.FTuChiTruocDieuChinh + item.FTuChi;
            item.FHienVatSauDieuChinh = item.FHienVatTruocDieuChinh + item.FHienVat;
            item.FHangNhapSauDieuChinh = item.FHangNhapTruocDieuChinh + item.FHangNhap;
            item.FHangMuaSauDieuChinh = item.FHangMuaTruocDieuChinh + item.FHangMua;
            item.FPhanCapSauDieuChinh = item.FPhanCapTruocDieuChinh + item.FPhanCap;

            if (_isAdjusted)
            {
                var tongItem = Items.FirstOrDefault(x => x.IRowType == (int)DuToanRowType.RowNhanPhanBoOrTong && x.IIdMlns.ToString().Equals(item.IIdMlns.ToString()));
                if (tongItem == null)
                    return;
                var lnsItem = Items.Where(x => x.IsEditable && x.IRowType == (int)DuToanRowType.RowChiTiet && x.IIdMlns.ToString().Equals(item.IIdMlns.ToString())).ToList();

                tongItem.FTuChiTruocDieuChinh = lnsItem.Sum(x => x.FTuChiTruocDieuChinh);
                tongItem.FTuChi = lnsItem.Sum(x => x.FTuChi);
                tongItem.FRutKBNN = lnsItem.Sum(x => x.FRutKBNN);
                tongItem.FTuChiSauDieuChinh = lnsItem.Sum(x => x.FTuChiSauDieuChinh);
                tongItem.FHienVatTruocDieuChinh = lnsItem.Sum(x => x.FHienVatTruocDieuChinh);
                tongItem.FHienVat = lnsItem.Sum(x => x.FHienVat);
                tongItem.FHienVatSauDieuChinh = lnsItem.Sum(x => x.FHienVatSauDieuChinh);
                tongItem.FHangNhapTruocDieuChinh = lnsItem.Sum(x => x.FHangNhapTruocDieuChinh);
                tongItem.FHangNhap = lnsItem.Sum(x => x.FHangNhap);
                tongItem.FHangNhapSauDieuChinh = lnsItem.Sum(x => x.FHangNhapSauDieuChinh);
                tongItem.FHangMuaTruocDieuChinh = lnsItem.Sum(x => x.FHangMuaTruocDieuChinh);
                tongItem.FHangMua = lnsItem.Sum(x => x.FHangMua);
                tongItem.FHangMuaSauDieuChinh = lnsItem.Sum(x => x.FHangMuaSauDieuChinh);
                tongItem.FPhanCapTruocDieuChinh = lnsItem.Sum(x => x.FPhanCapTruocDieuChinh);
                tongItem.FPhanCap = lnsItem.Sum(x => x.FPhanCap);
                tongItem.FPhanCapSauDieuChinh = lnsItem.Sum(x => x.FPhanCapSauDieuChinh);
                tongItem.FDuPhong = lnsItem.Sum(x => x.FDuPhong);
            }
            else
            {
                var phanBoItem = Items.FirstOrDefault(x => x.IRowType == (int)DuToanRowType.RowNhanPhanBoOrTong && x.IIdCtduToanNhan == item.IIdCtduToanNhan && x.IIdMlns.ToString().Equals(item.IIdMlns.ToString()));
                var conlaiItem = Items.FirstOrDefault(x => x.IRowType == (int)DuToanRowType.RowConLai && x.IIdCtduToanNhan == item.IIdCtduToanNhan && x.IIdMlns.ToString().Equals(item.IIdMlns.ToString()));
                if (conlaiItem == null || phanBoItem == null)
                    return;

                var lnsItem = Items.Where(x => x.IsEditable && x.IIdCtduToanNhan == item.IIdCtduToanNhan && x.IIdMlns.ToString().Equals(item.IIdMlns.ToString())).ToList();
                conlaiItem.FTuChi = phanBoItem.FTuChi - lnsItem.Sum(x => x.FTuChi);
                conlaiItem.FRutKBNN = phanBoItem.FRutKBNN - lnsItem.Sum(x => x.FRutKBNN);
                conlaiItem.FHienVat = phanBoItem.FHienVat - lnsItem.Sum(x => x.FHienVat);
                conlaiItem.FHangNhap = phanBoItem.FHangNhap - lnsItem.Sum(x => x.FHangNhap);
                conlaiItem.FHangMua = phanBoItem.FHangMua - lnsItem.Sum(x => x.FHangMua);
                conlaiItem.FPhanCap = phanBoItem.FPhanCap - lnsItem.Sum(x => x.FPhanCap);
                conlaiItem.FDuPhong = phanBoItem.FDuPhong - lnsItem.Sum(x => x.FDuPhong);
            }
        }

        private void SettingEditable()
        {
            foreach (var item in Items.Where(x => !x.IsHangCha))
            {
                var mlns = _listMLNS.Where(x => x.Lns == item.SLns).FirstOrDefault();
                if (mlns != null)
                {
                    item.IsEditTuChi = mlns.BTuChi;
                    item.IsEditHienVat = mlns.BHienVat;
                    item.IsEditHangNhap = mlns.BHangNhap;
                    item.IsEditHangMua = mlns.BHangMua;
                    item.IsEditDuPhong = mlns.BDuPhong;
                    item.IsEditPhanCap = mlns.BPhanCap;
                }
            }
        }

        /// <summary>
        /// open screen print
        /// </summary>
        /// <param name="param"></param>
        private void OpenPrintDialog(object param)
        {
            var divisionEstimatePrintType = (DivisionEstimatePrintType)((int)param);
            object content = null;
            switch (divisionEstimatePrintType)
            {
                case DivisionEstimatePrintType.COVER_SHEET:
                    PrintReportCoverSheetViewModel.Model = Model;
                    PrintReportCoverSheetViewModel.Models = new ObservableCollection<DtChungTuModel>();
                    PrintReportCoverSheetViewModel.Init();
                    content = new PrintReportCoverSheet
                    {
                        DataContext = PrintReportCoverSheetViewModel
                    };
                    break;
                case DivisionEstimatePrintType.TARGET_AGENCY:
                    PrintReportTargetAgencyViewModel.Model = Model;
                    PrintReportTargetAgencyViewModel.Models = new ObservableCollection<DtChungTuModel>(); ;
                    PrintReportTargetAgencyViewModel.Init();
                    content = new PrintReportTargetAgency
                    {
                        DataContext = PrintReportTargetAgencyViewModel
                    };
                    break;
                case DivisionEstimatePrintType.TARGET_MAJORS:
                    PrintReportTargetMajorsViewModel.Model = Model;
                    PrintReportTargetMajorsViewModel.Models = new ObservableCollection<DtChungTuModel>();
                    PrintReportTargetMajorsViewModel.Init();
                    content = new PrintReportTargetMajors
                    {
                        DataContext = PrintReportTargetMajorsViewModel
                    };
                    break;
                case DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_AGENCY_LNS:
                    PrintReportDetailTargetAgencyLnsViewModel.Model = Model;
                    PrintReportDetailTargetAgencyLnsViewModel.Models = new ObservableCollection<DtChungTuModel>();
                    PrintReportDetailTargetAgencyLnsViewModel.Init();
                    content = new PrintReportDetailTargetAgencyLns
                    {
                        DataContext = PrintReportDetailTargetAgencyLnsViewModel
                    };
                    break;
                case DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_LNS_AGENCY:
                    PrintReportSynthesisAgencyViewModel.Model = Model;
                    PrintReportSynthesisAgencyViewModel.Models = new ObservableCollection<DtChungTuModel>();
                    PrintReportSynthesisAgencyViewModel.Init();
                    content = new PrintReportSynthesisAgency
                    {
                        DataContext = PrintReportSynthesisAgencyViewModel
                    };
                    break;
                case DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_MAJORS:
                    PrintReportSynthesisTargetMajorsViewModel.Model = Model;
                    PrintReportSynthesisTargetMajorsViewModel.Models = new ObservableCollection<DtChungTuModel>();
                    PrintReportSynthesisTargetMajorsViewModel.Init();
                    content = new PrintReportSynthesisTargetMajors
                    {
                        DataContext = PrintReportSynthesisTargetMajorsViewModel
                    };
                    break;
                case DivisionEstimatePrintType.TARGET_MAJORS_DAY:
                    PrintReportTargetMajorsDayViewModel.Init();
                    content = new PrintReportTargetMajorsDay
                    {
                        DataContext = PrintReportTargetMajorsDayViewModel
                    };
                    break;
                case DivisionEstimatePrintType.TARGET_MAJORS_AGENCY:
                    PrintReportTargetMajorAgencyViewModel.Init();
                    content = new PrintReportTargetMajorAgency
                    {
                        DataContext = PrintReportTargetMajorAgencyViewModel
                    };
                    break;
                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, "DivisionEstimateDetailDialog", null, null);
            }
        }

        private bool HasInputData(DtChungTuChiTietModel item)
        {
            bool hasDataTuChi = (ColumnVisibility.IsDisplayTuChi || ColumnVisibility.IsDisplayTuChiDieuChinh) && (item.FTuChi != 0 || item.FTuChiTruocDieuChinh != 0 || item.FRutKBNN != 0);
            bool hasDataHienVat = (ColumnVisibility.IsDisplayHienVat || ColumnVisibility.IsDisplayHienVatDieuChinh) && (item.FHienVat != 0 || item.FHienVatTruocDieuChinh != 0);
            bool hasDataHangMua = (ColumnVisibility.IsDisplayHangMua || ColumnVisibility.IsDisplayHangMuaDieuChinh) && (item.FHangMua != 0 || item.FHangMuaTruocDieuChinh != 0);
            bool hasDataHangNhap = (ColumnVisibility.IsDisplayHangNhap || ColumnVisibility.IsDisplayHangNhapDieuChinh) && (item.FHangNhap != 0 || item.FHangNhapTruocDieuChinh != 0);
            bool hasDataPhanCap = (ColumnVisibility.IsDisplayPhanCap || ColumnVisibility.IsDisplayPhanCapDieuChinh) && (item.FPhanCap != 0 || item.FPhanCapTruocDieuChinh != 0);
            bool hasDataDuPhong = ColumnVisibility.IsDisplayDuPhong && item.FDuPhong != 0;
            //bool hasQD = !string.IsNullOrEmpty(item.SSoQuyetDinh);
            return hasDataTuChi || hasDataHienVat || hasDataPhanCap || hasDataHangMua || hasDataHangNhap || hasDataDuPhong;
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            var result = MessageBox.Show(Resources.DeleteAllChungTuChiTiet, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                if (Items != null)
                {
                    Items.Where(x => x.IsFilter && !x.IsHangCha && !x.IsPhanBo && !x.IsConLai).ForAll(x => x.IsDeleted = true);
                    OnSaveData();
                }
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void UpdateChungTu()
        {
            NsDtChungTu chungTu = _dtChungTuService.FindById(Model.Id);
            var childs = Items.Where(x => !x.IsHangCha && (x.FTuChi != 0 || x.FRutKBNN != 0 || x.FHienVat != 0 || x.FHangNhap != 0 || x.FHangMua != 0 ||
                                    x.FPhanCap != 0 || x.FDuPhong != 0 || x.FTuChiSauDieuChinh != 0 || x.FHienVatSauDieuChinh != 0 ||
                                    x.FHangNhapSauDieuChinh != 0 || x.FHangMuaSauDieuChinh != 0 || x.FPhanCapSauDieuChinh != 0)).ToList();

            chungTu.FTongTuChi = childs.Sum(x => x.FTuChi);
            chungTu.FTongRutKBNN = childs.Sum(x => x.FRutKBNN);
            chungTu.FTongHienVat = childs.Sum(x => x.FHienVat);
            chungTu.FTongHangNhap = childs.Sum(x => x.FHangNhap);
            chungTu.FTongHangMua = childs.Sum(x => x.FHangMua);
            chungTu.FTongPhanCap = childs.Sum(x => x.FPhanCap);
            chungTu.FTongDuPhong = childs.Sum(x => x.FDuPhong);


            _dtChungTuService.Update(chungTu);

            Model.FTongTuChi = chungTu.FTongTuChi;
            Model.FTongHienVat = chungTu.FTongHienVat;
            Model.FTongHangNhap = chungTu.FTongHangNhap;
            Model.FTongHangMua = chungTu.FTongHangMua;
            Model.FTongPhanCap = chungTu.FTongPhanCap;
            Model.FTongDuPhong = chungTu.FTongDuPhong;
        }

        //Kiểm tra chứng từ phân bổ có phải là cuối cùng không
        private void CheckLastDivisionEstimateVoucher()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == Model.ILoai);
            predicate = predicate.And(x => x.ILoaiChungTu == Model.ILoaiChungTu);
            predicate = predicate.And(x => x.Id != Model.Id);

            var listChungTu = _dtChungTuService.FindByCondition(predicate).ToList();
            listChungTu = listChungTu.Where(x => x.IIdDotNhan.Split(",").Intersect(Model.IIdDotNhan.Split(",").ToList()).Any()).ToList();

            if (listChungTu.Count > 0)
            {
                var maxDate = listChungTu.Select(x => { return x.DNgayQuyetDinh.HasValue ? x.DNgayQuyetDinh.Value.Date : x.DNgayChungTu.Value.Date; }).Max(x => x);
                var modelDate = Model.DNgayQuyetDinh.HasValue ? Model.DNgayQuyetDinh.Value.Date : Model.DNgayChungTu.Value.Date;
                if (listChungTu.Any() && modelDate < maxDate)
                    MessageBoxHelper.Info(string.Format(Resources.AlertUpdateDivisionEstimate, Model.SSoChungTu));
                else
                {
                    List<NsDtNhanPhanBoMap> dtNhanPhanBoMaps = _dtChungTuMapService.FindByIdNhanDuToan(Model.Id).ToList();
                    if (dtNhanPhanBoMaps.Count() > 0)
                        MessageBoxHelper.Info(Resources.AlertDivisionEstimateAdjusted);
                }
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        private void GetAdjustedEstimateVoucher()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                var model = Model.SDsidMaDonVi;
                var dotNhan = Model.IIdDotNhan;
                var dataQuery = _dcChungTuChiTietService.FindByUnits(model, _sessionInfo.YearOfWork, dotNhan);
                var dataAdj = _mapper.Map<List<DcChungTuChiTietModel>>(dataQuery);

                if (dataAdj.Any())
                {
                    IsProcessReport = true;
                    var itemsMap = Items.Where(x => !x.IsHangCha && dataAdj.Select(a => a.SXauNoiMa).Contains(x.SXauNoiMa) && dataAdj.Select(a => a.IIdMaDonVi).Contains(x.IIdMaDonVi)).ToList();
                    int count = itemsMap.Select(x => x.IIdMaDonVi.Split(",")).SelectMany(x => x).Count();
                    int index = 0;
                    Parallel.ForEach(itemsMap, item =>
                    {
                        (s as BackgroundWorker).ReportProgress((index++) * 100 / count, null);
                        var dataMap = dataAdj.FirstOrDefault(x => x.SXauNoiMa.Equals(item.SXauNoiMa) && x.IIdMaDonVi.Equals(item.IIdMaDonVi));
                        if (dataMap != null)
                        {
                            //Ngan sach su dung
                            if (Model.ILoaiChungTu == int.Parse(VoucherType.NSSD_Key))
                            {
                                if (dataMap.FTang != 0)
                                    item.FTuChi = dataMap.FTang;
                                else if (dataMap.FGiam != 0)
                                    item.FTuChi = 0 - dataMap.FGiam;
                            }
                            //Ngan sach dac thu nganh
                            else if (Model.ILoaiChungTu == int.Parse(VoucherType.NSBD_Key))
                            {
                                var mlnsCha = _listMLNS.FirstOrDefault(x => x.XauNoiMa == item.SLns);
                                if (dataMap.FTang != 0)
                                {
                                    if (mlnsCha.BHangNhap == true && item.SLns == "1040200")
                                        item.FHangNhap = dataMap.FTang;
                                    else if (mlnsCha.BHangMua == true && item.SLns == "1040300")
                                        item.FHangMua = dataMap.FTang;
                                    else
                                        item.FTuChi = dataMap.FTang;
                                }
                                if (dataMap.FGiam != 0)
                                {
                                    if (mlnsCha.BHangNhap == true && item.SLns == "1040200")
                                        item.FHangNhap = 0 - dataMap.FGiam;
                                    else if (mlnsCha.BHangMua == true && item.SLns == "1040300")
                                        item.FHangMua = 0 - dataMap.FGiam;
                                    else
                                        item.FTuChi = 0 - dataMap.FGiam;
                                }
                            }
                        }
                    });
                    MessageBox.Show(Resources.MsgTransferDataDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show(Resources.MsgAdjDataEmpty, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }, (s, e) =>
            {
                IsProcessReport = false;
            }, (s, e) =>
            {
                ProgressValue = e.ProgressPercentage;
            });
        }
    }
}
