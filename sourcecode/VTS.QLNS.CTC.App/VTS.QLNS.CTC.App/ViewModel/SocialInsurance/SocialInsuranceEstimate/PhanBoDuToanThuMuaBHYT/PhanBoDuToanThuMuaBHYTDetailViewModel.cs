using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Model.Control;
using System.Windows;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate;
using System.CodeDom;
using Aspose.Cells.Charts;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThuMuaBHYT;
using ControlzEx.Standard;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.Report;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi
{
    public class PhanBoDuToanThuMuaBHYTDetailViewModel : DetailViewModelBase<BhPbdttmBHYTModel, BhPbdttmBHYTChiTietModel>
    {
        private readonly IPbdttmBHYTChiTietService _pbdttmBHYTChiTietService;
        private readonly IPbdttmBHYTService _pbdttmBHYTService;
        private readonly IBhDtTmBHYTTNChiTietService _bhDtTmBHYTTNChiTietService;
        private readonly IKhtmBHYTService _khtmService;
        private readonly IKhtmBHYTChiTietService _khtmChiTietService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _budgetCatalogItemsView;
        private ICollectionView ItemsView;
        private readonly INsDonViService _nsDonViService;
        private readonly IDanhMucService _danhMucService;
        private readonly ILog _logger;
        private bool _isNamLuyKe;
        private bool _isShowQuyetDinh;
        private List<BhDmMucLucNganSach> _listMLNS;
        private SessionInfo _sessionInfo;
        private List<DanhMuc> _listDanhMucNganh;
        private ICollection<BhPbdttmBHYTChiTietModel> _filterResult = new HashSet<BhPbdttmBHYTChiTietModel>();
        private List<Tuple<string, Guid?>> _filterResultWithSQD = new List<Tuple<string, Guid?>>();
        private string xnmConcatenation = "";
        private Dictionary<string, List<string>> _dicDonViNganh = new Dictionary<string, List<string>>();

        public HashSet<string> _filterXauNoiMaDTTM = new HashSet<string>();
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public override Type ContentType => typeof(PhanBoDuToanThuMuaBHYTDetail);
        public bool IsSaveData;
        public bool IsDieuChinh => (BudgetType.ADDITIONAL.Equals((BudgetType)Model.ILoaiDuToan) || BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR.Equals((BudgetType)Model.ILoaiDuToan));
        public bool IsDeleteAll;
        public bool IsTypeLuyKe => _isNamLuyKe;
        public bool IsShowQuyetDinh => _isShowQuyetDinh;
        public bool HasLastDivisionEstimateVoucher { get; set; }
        public int NamLamViec { get; set; }
        public bool IsShowKhtmPlanButton => Model.ILoaiDuToan == (int)EstimateTypeNum.YEAR;

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
                }
            }
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
                ItemsView.Refresh();
                CalculateTotalParent();
                OnPropertyChanged(nameof(IsEnablePhanBoAll));
            }
        }

        private Visibility _isShowColDieuChinh;
        public Visibility IsShowColDieuChinh
        {

            get => _isShowColDieuChinh;
            set
            {
                SetProperty(ref _isShowColDieuChinh, value);

            }
        }
        public bool FirstTimePhanBo { get; set; }

        public bool IsAnotherUserCreate { get; set; }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }


        public bool IsEnablePhanBoAll => SelectedAgency != null;
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
                if (SetProperty(ref _typeDisplaysselected, value) && ItemsView != null)
                {
                    OnRefresh();
                    BeForeRefresh();
                    ItemsView.Refresh();
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
                    ItemsView.Refresh();
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
                    ItemsView.Refresh();
                }
            }
        }

        private bool _isAdjusted;
        public bool IsAdjusted
        {
            get => _isAdjusted;
            set => SetProperty(ref _isAdjusted, value);
        }
        public bool IsFillDataDauNam { get; set; }

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set
            {
                if (SetProperty(ref _sNoiDungSearch, value))
                {
                    SearchTextFilter();
                    ItemsView.Refresh();
                    //_budgetCatalogItemsView.Refresh();
                }
            }
        }

        private ObservableCollection<BhPbdttmBHYTChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhPbdttmBHYTChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhPbdttmBHYTChiTietModel _selectedPopupItem;
        public BhPbdttmBHYTChiTietModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SNoiDungSearch = _selectedPopupItem?.SNoiDung;
                OnPropertyChanged(nameof(SNoiDungSearch));
                IsPopupOpen = false;
            }
        }

        private ObservableCollection<BhPbdttmBHYTChiTietModel> _dataSearch;
        public ObservableCollection<BhPbdttmBHYTChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }


        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand FillDataDauNamCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand GetKhttmPlanDataCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintDTTMCommand {  get; }
        public TongHopThuChiViewModel TongHopThuChiViewModel { get; set; }
        public PrintPhuLucDuToanThuMuaBHYTViewModel PrintPhuLucDuToanThuMuaBHYTViewModel { get; set; }

        public PhanBoDuToanThuMuaBHYTDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IPbdttmBHYTService pbdttmBHYTService,
            IPbdttmBHYTChiTietService pbdttmBHYTChiTietService,
            IBhDtTmBHYTTNChiTietService bhDtTmBHYTTNChiTietService,
            INsDonViService nsDonViService,
            IDanhMucService danhMucService,
            ILog logger,
            IKhtmBHYTService iKhtmBHYTService,
            IKhtmBHYTChiTietService iKhtmBHYTChiTietService,
            TongHopThuChiViewModel tongHopThuChiViewModel,
            PrintPhuLucDuToanThuMuaBHYTViewModel printPhuLucDuToanThuMuaBHYTViewModel
            ) : base(danhMucService, sessionService)
        {
            _mapper = mapper;
            _pbdttmBHYTService = pbdttmBHYTService;
            _pbdttmBHYTChiTietService = pbdttmBHYTChiTietService;
            _khtmService = iKhtmBHYTService;
            _bhDtTmBHYTTNChiTietService = bhDtTmBHYTTNChiTietService;
            _khtmChiTietService = iKhtmBHYTChiTietService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _danhMucService = danhMucService;
            _logger = logger;

            TongHopThuChiViewModel = tongHopThuChiViewModel;
            PrintPhuLucDuToanThuMuaBHYTViewModel = printPhuLucDuToanThuMuaBHYTViewModel;
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            GetKhttmPlanDataCommand = new RelayCommand(obj => GetPlanData());
            PrintCommand = new RelayCommand(obj => OnPrint(obj));
            PrintDTTMCommand = new RelayCommand(OnPrintDTTM);
        }

        private void OnResetFilter()
        {
            DetailFilter = new EstimationDetailCriteria();
            SelectedLNS = string.Empty;
            SelectedAgency = null;
            BeForeRefresh();
            CalculateTotalParent();
            LoadData();
        }

        public override void Init()
        {
            base.Init();
            IsFillDataDauNam = true;
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            ResetConditionSearch();
            LoadDotNhan();
            LoadControlVisibility();
            LoadChuyenNganh();
            LoadNhomNganh();
            LoadNamLuyKe();
            LoadTypeDisplay();
            LoadAgencies();
            LoadData();
        }
        private void LoadControlVisibility()
        {
            string lns = Model.SDSLNS;
            _columnVisibility = new DivisionColumnVisibility();
            _columnVisibility.IsDisplayTuChiDieuChinh = _isAdjusted;
            _columnVisibility.IsDisplayHienVatDieuChinh = _isAdjusted;

            DetailHelper.VisibilityBudgetTypeAdjusted = _isAdjusted ? Visibility.Visible : Visibility.Collapsed;
            DetailHelper.VisibilityBudgetTypeNoneAdjusted = !_isAdjusted ? Visibility.Visible : Visibility.Collapsed;

            IsShowColDieuChinh = _isAdjusted ? Visibility.Visible : Visibility.Hidden;

            OnPropertyChanged(nameof(IsShowColDieuChinh));
            OnPropertyChanged(nameof(ColumnVisibility));
        }
        private void ResetConditionSearch()
        {
            DetailTotal = new DivisionEstimateDetailPropertyHelper();
            DetailFilter = new EstimationDetailCriteria();
            _listDanhMucNganh = new List<DanhMuc>();
            _dicDonViNganh = new Dictionary<string, List<string>>();
            Items = new ObservableCollection<BhPbdttmBHYTChiTietModel>();
            _isAdjusted = false;
            if (BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDuToan))
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

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Tất cả" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.DA_NHAN_DUTOAN, DisplayItem = "Đã nhận dự toán" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Đã nhập dữ liệu" });
            TypeDisplaysSelected = TypeDisplay.DA_NHAN_DUTOAN;
        }

        private void LoadData()
        {
            var listDonViQuanLy = _sessionService.Current.IdsDonViQuanLy.Split(StringUtils.COMMA).ToList();
            var listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).Where(x => x.Loai.Equals(LoaiDonVi.ROOT)).Select(y => y.IIDMaDonVi).ToList();
            var listIntersect = listDonViQuanLy.Intersect(listDonVi);

            int iNamLamViec = _sessionInfo.YearOfWork;
            string sLNS = Model.SDSLNS;
            string sIdDonVi = Model.SDS_IDMaDonVi;

            List<BhPbdttmBHYTChiTietQuery> listDataQuery = new List<BhPbdttmBHYTChiTietQuery>();

            if (Model != null && Model.ILoaiDuToan == (int)BaoHiemDuToanTypeEnum.LoaiChungTu.DIEU_CHINH)
            {
                listDataQuery = _pbdttmBHYTChiTietService.FindChungTuChiTietDieuChinh(Model.Id, sLNS, iNamLamViec, _sessionInfo.Principal).ToList();
            }
            else
            {
                listDataQuery = _pbdttmBHYTChiTietService.FindChungTuChiTiet(Model.Id, sLNS, sIdDonVi, iNamLamViec, _sessionInfo.Principal).ToList();
            }

            Items = _mapper.Map<ObservableCollection<BhPbdttmBHYTChiTietModel>>(listDataQuery);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhPbdttmBHYTChiTietModel>>(listDataQuery);
            BeForeRefresh();
            ItemsView = CollectionViewSource.GetDefaultView(Items);
            ItemsView.Filter = ItemsViewFilter;

            foreach (var bhdttmBHYTChiTietModel in Items)
            {
                bhdttmBHYTChiTietModel.IsFilter = true;
                if (!bhdttmBHYTChiTietModel.BHangCha)
                {
                    bhdttmBHYTChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        BhPbdttmBHYTChiTietModel item = (BhPbdttmBHYTChiTietModel)sender;

                        if (args.PropertyName == nameof(SelectedItem.FDuToan))
                        {
                            bhdttmBHYTChiTietModel.IsModified = true;
                            item.IsModified = true;
                            item.FDuToanSauDieuChinh = (item.FDuToan ?? 0) + (item.FDuToanTruocDieuChinh ?? 0);
                            CalculateData();
                            if (!IsAdjusted)
                            {
                                CalculateRemainRow();
                            }
                        }

                        IsSaveData = true;
                        OnPropertyChanged(nameof(IsSaveData));
                        //OnPropertyChanged(nameof(IsOpenPrintPopup));

                    };

                }
            }
            CalculateData();
            CalculateRemainRow();
        }

        private void CalculateRemainRow()
        {
            var lstRemainRows = Items.Where(x => x.IsRemainRow);
            foreach (var item in lstRemainRows)
            {
                if (Items.Any(y => y.BHangCha && y.SXauNoiMa.Equals(item.SXauNoiMa) && y.IID_DTTM_BHYT_ThanNhan.Equals(item.IID_DTTM_BHYT_ThanNhan) && !y.IsRemainRow))
                {
                    var items = Items.Where(y => !y.IsRemainRow && !y.BHangCha && y.SXauNoiMa.Equals(item.SXauNoiMa) && y.IID_DTTM_BHYT_ThanNhan.Equals(item.IID_DTTM_BHYT_ThanNhan)).Sum(s => s.FDuToan.GetValueOrDefault());
                    item.FDuToan = item.FDuToanTruocDieuChinh.GetValueOrDefault() - items;
                }
            }
        }

        private void CalculateData()
        {
            Items.Where(x => x.BHangCha && x.Type != (int)BaoHiemDuToanTypeEnum.Type.SO_CHUA_PHAN_BO)
                .ForAll(x =>
                {
                    x.FDuToan = 0;
                    x.FDuToanTruocDieuChinh = 0;
                    x.FDuToanSauDieuChinh = 0;
                });
            var dictByMlns = Items.Where(x => !x.IsRemainRow).GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            var temp = Items.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (var item in temp)
            {

                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }
            UpdateTotal();
        }

        private void CalculateParent(Guid? idParent, BhPbdttmBHYTChiTietModel item, Dictionary<Guid?, BhPbdttmBHYTChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            if (!model.IsRemainRow || model.Type != 2)
            {
                //Trước điều chỉnh
                model.FDuToanTruocDieuChinh += item.FDuToanTruocDieuChinh.GetValueOrDefault();
                model.FDuToan += item.FDuToan.GetValueOrDefault();
                //Sau điều chinh
                model.FDuToanSauDieuChinh += item.FDuToanSauDieuChinh.GetValueOrDefault();

                CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
            }
        }

        private void UpdateSoChuaPhanBo()
        {
            var bhPbdtcChiTiet = (BhPbdttmBHYTChiTietModel)SelectedItem;
            if (bhPbdtcChiTiet != null)
            {
                var lstCtChiTietNhanPb = _bhDtTmBHYTTNChiTietService.FindAllChungTuDuToan().Where(x => x.IID_DTTM_BHYT_ThanNhan == bhPbdtcChiTiet.IID_DTTM_BHYT_ThanNhan && x.IID_MLNS == bhPbdtcChiTiet.IID_MLNS).ToList();
                Double? fDuToanNhanPhanBo = lstCtChiTietNhanPb?.Select(x => x.FDuToan).Sum();

                var predicate = PredicateBuilder.True<BhPbdttmBHYTChiTiet>();
                predicate = predicate.And(x => x.IID_DTTM_BHYT_ThanNhan == bhPbdtcChiTiet.IID_DTTM_BHYT_ThanNhan);
                predicate = predicate.And(x => x.IID_MLNS == bhPbdtcChiTiet.IID_MLNS);
                predicate = predicate.And(x => x.IID_DTTM_BHYT_ThanNhan_PhanBo != Model.Id);
                var lstCtChiTietDaPhanBo = _pbdttmBHYTChiTietService.FindByCondition(predicate);

                Double? fDuToanDaPhanBo = lstCtChiTietDaPhanBo?.Select(x => x.FDuToan).Sum();

                Double? fDuToanPhanBo = Items.Where(x => x.IID_MLNS == SelectedItem.IID_MLNS && x.IID_DTTM_BHYT_ThanNhan == SelectedItem.IID_DTTM_BHYT_ThanNhan && !x.BHangCha && !x.IsDeleted).Select(x => x.FDuToan).Sum();

                Items.Where(x => x.Type == (int)BaoHiemDuToanTypeEnum.Type.SO_CHUA_PHAN_BO && x.IID_MLNS == SelectedItem.IID_MLNS && x.IID_DTTM_BHYT_ThanNhan == SelectedItem.IID_DTTM_BHYT_ThanNhan)
                    .Select(x =>
                    {
                        x.FDuToan = (fDuToanNhanPhanBo ?? 0) - (fDuToanDaPhanBo ?? 0) - (fDuToanPhanBo ?? 0);
                        return x;
                    }).ToList();
            }
        }

        private void UpdateTotal()
        {
            Model.FTongDuToan = 0;
            Model.FTongDuToanTruocDieuChinh = 0;
            Model.FTongDuToanSauDieuChinh = 0;

            var root = Items.Where(x => !x.BHangCha && !x.IsDeleted).ToList();
            if (root.Any())
            {
                Model.FTongDuToan = root.Sum(x => x.FDuToan);
                Model.FTongDuToanTruocDieuChinh = root.Sum(x => x.FDuToanTruocDieuChinh);
                Model.FTongDuToanSauDieuChinh = root.Sum(x => x.FDuToanSauDieuChinh);
            }
        }

        private void LoadAgencies()
        {
            List<DonVi> listNsDonVi = new List<DonVi>();
            listNsDonVi = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Join(StringUtils.COMMA, new string[] { LoaiDonVi.NOI_BO, LoaiDonVi.ROOT })).ToList();

            if (listNsDonVi.Any(x => x.Loai == LoaiDonVi.ROOT))
            {
                var predicate = PredicateBuilder.True<DonVi>();
                predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                predicate = predicate.And(x => x.Loai == SoChungTuType.EstimateDivision.ToString());
                predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);

                listNsDonVi = _nsDonViService.FindByCondition(predicate).ToList();
            }

            var listIdDonVi = string.IsNullOrEmpty(Model.SDS_IDMaDonVi) ? new List<string>() : Model.SDS_IDMaDonVi.Split(",").ToList();
            listNsDonVi = listNsDonVi.Where(x => listIdDonVi.Contains(x.IIDMaDonVi)).ToList();

            Agencies = _mapper.Map<ObservableCollection<ComboboxItem>>(listNsDonVi);
        }

        private void LoadDotNhan()
        {
            List<BhPbdttmBHYT> chungTus = _pbdttmBHYTService.FindDotNhanByChungTuPhanBo(Model.Id).ToList();
            List<ComboboxItem> cbxChungTus = new List<ComboboxItem>();
            foreach (var chungTu in chungTus)
            {
                cbxChungTus.Add(new ComboboxItem { ValueItem = chungTu.Id.ToString(), DisplayItem = chungTu.SSoQuyetDinh });
            }
            CbxNhanPhanBos = new ObservableCollection<ComboboxItem>(cbxChungTus);
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhPbdttmBHYTChiTietModel)obj;
            result = AllocationDetailItemsFilter(item);
            if (!result && item.BHangCha && !string.IsNullOrEmpty(TypeDisplaysSelected) && TypeDisplaysSelected == TypeDisplay.DA_NHAN_DUTOAN)
            {
                result = _filterXauNoiMaDTTM.Any(x => x.Equals(item.SXauNoiMa));
            }

            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = result && DataSearch.Any(x => x.IID_MLNS.Equals(item.IID_MLNS));
            }

            if (result)
                item.IsFilter = result;
            return result;
        }

        private void OnPrint(object param)
        {
            var divisionPrintType = (SocialInsuranceDivisionEstimatePrintType)((int)param);
            TongHopThuChiViewModel.ReportNameTypeValue = (int)divisionPrintType;
            TongHopThuChiViewModel.ReportTypeValue = divisionPrintType;
            TongHopThuChiViewModel.Init();
            var view = new TongHopThuChi
            {
                DataContext = TongHopThuChiViewModel
            };
            DialogHost.Show(view, "DivisionEstimateDetailDialog", null, null);
        }

        private void OnPrintDTTM(object param)
        {
            PrintPhuLucDuToanThuMuaBHYTViewModel.Init();
            var view = new PrintPhuLucDuToanThuMuaBHYT
            {
                DataContext = PrintPhuLucDuToanThuMuaBHYTViewModel
            };
            DialogHost.Show(view, "DivisionEstimateDetailDialog", null, null);
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.BHangCha)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                CalculateData();
                if (!_isAdjusted)
                {
                    UpdateSoChuaPhanBo();
                }
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
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
                    Items.Where(x => x.IsFilter && !x.BHangCha).ForAll(x => x.IsDeleted = true);
                    OnSave();
                }
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(BhPbdttmBHYTChiTietModel.FDuToan))
            {
                BhPbdttmBHYTChiTietModel item = (BhPbdttmBHYTChiTietModel)sender;
                item.FDuToanSauDieuChinh = (item.FDuToan ?? 0) + (item.FDuToanTruocDieuChinh ?? 0);
                CalculateData();
                if (_isAdjusted)
                {
                    UpdateSoChuaPhanBo();
                }

                item.IsModified = true;
                IsSaveData = true;
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }
        protected override void OnAdd()
        {
            if (_isNamLuyKe || (Model.ILoaiDuToan != null && !BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDuToan)))
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
                    BhPbdttmBHYTChiTietModel sourceItem = Items.ElementAt(targetRow);
                    BhPbdttmBHYTChiTietModel targetItem = CloneRow(sourceItem);
                    OnPropertyChanged(nameof(targetItem));
                    targetItem.PropertyChanged += DetailModel_PropertyChanged;
                    Items.Insert(targetRow + 1, targetItem);
                    OnPropertyChanged(nameof(Items));
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IsDeleteAll));
                }
            }
        }

        private BhPbdttmBHYTChiTietModel CloneRow(BhPbdttmBHYTChiTietModel sourceItem)
        {
            BhPbdttmBHYTChiTietModel targetItem = ObjectCopier.Clone(sourceItem);

            targetItem.Id = Guid.Empty;
            targetItem.IID_DTTM_BHYT_ThanNhan = Guid.Empty;
            targetItem.BEmty = true;
            targetItem.FDuToan = 0;
            targetItem.FDuToanTruocDieuChinh = 0;
            targetItem.FDuToanSauDieuChinh = 0;
            targetItem.CbxDonVi = Agencies;
            targetItem.CbxNhanPhanBos = CbxNhanPhanBos;

            return targetItem;
        }

        public override void OnSave()
        {
            if (!IsSaveData)
            {
                return;
            }

            var lstDataAdd = Items.Where(x => !x.BHangCha && string.IsNullOrEmpty(x.IID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet.ToString()) && !x.IsDeleted && x.IsModified).ToList();
            var lstDataUpdate = Items.Where(x => !x.BHangCha && !string.IsNullOrEmpty(x.IID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet.ToString()) && !x.IsDeleted && x.IsModified).ToList();
            var lstDataDelete = Items.Where(x => !x.BHangCha && x.IsDeleted && x.IsModified && !string.IsNullOrEmpty(x.IID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet.ToString())).ToList();

            var addItemList = new List<BhPbdttmBHYTChiTiet>();
            if (lstDataAdd.Count() > 0)
            {
                _mapper.Map(lstDataAdd, addItemList);
                addItemList.ForAll(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.IID_DTTM_BHYT_ThanNhan_PhanBo = Model.Id;
                    x.DNgayChungTu = Model.DNgayChungTu.Value;
                    x.DNgayQuyetDinh = Model.DNgayQuyetDinh.Value;
                    x.SSoQuyetDinh = Model.SSoQuyetDinh;
                    x.SSoChungTu = Model.SSoChungTu;
                    x.DNgayTao = DateTime.Now;
                    x.DNgaySua = null;
                    x.SNguoiTao = _sessionInfo.Principal;
                    x.INamLamViec = _sessionService.Current.YearOfWork;
                });

                _pbdttmBHYTChiTietService.AddRange(addItemList);
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }
            if (lstDataUpdate.Count() > 0)
            {
                addItemList = lstDataUpdate.Select(x => new BhPbdttmBHYTChiTiet
                {
                    Id = x.IID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ?? x.IID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet.Value,
                    DNgayTao = DateTime.Now,
                    DNgaySua = DateTime.Now,
                    DNgayChungTu = Model.DNgayChungTu.Value,
                    DNgayQuyetDinh = Model.DNgayQuyetDinh.Value,
                    SNguoiTao = x.SNguoiTao,
                    SNguoiSua = _sessionInfo.Principal,
                    IID_DTTM_BHYT_ThanNhan = x.IID_DTTM_BHYT_ThanNhan,
                    IID_DTTM_BHYT_ThanNhan_PhanBo = Model.Id,
                    IID_MaDonVi = x.IID_MaDonVi,
                    IID_MLNS = x.IID_MLNS ?? x.IID_MLNS.Value,
                    SLNS = x.SLNS,
                    SMoTa = x.SNoiDung,
                    SXauNoiMa = x.SXauNoiMa,
                    FDuToan = x.FDuToan,
                    INamLamViec = Model.INamLamViec,
                    SSoQuyetDinh = Model.SSoQuyetDinh,
                    SSoChungTu = Model.SSoChungTu
                }).ToList();

                foreach (var item in addItemList)
                {
                    _pbdttmBHYTChiTietService.Update(item);
                }
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();

            }

            if (lstDataDelete.Count > 0)
            {
                addItemList = lstDataDelete.Select(x => new BhPbdttmBHYTChiTiet
                {
                    Id = x.IID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet.Value,
                }).ToList();

                foreach (var item in addItemList)
                {
                    _pbdttmBHYTChiTietService.Delete(item);
                }
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }


            //Update phân bổ dự toán
            var chungTuPhanBoModel = _pbdttmBHYTService.FindById(Model.Id);
            var predicate_chitiet = PredicateBuilder.True<BhPbdttmBHYTChiTiet>();
            predicate_chitiet = predicate_chitiet.And(x => x.IID_DTTM_BHYT_ThanNhan_PhanBo == Model.Id);
            var lstChungTuChiTiet = _pbdttmBHYTChiTietService.FindByCondition(predicate_chitiet).ToList();

            chungTuPhanBoModel.FDuToan = lstChungTuChiTiet?.Select(x => x.FDuToan).Sum();
            if (Model.ILoaiDuToan == (int)BudgetType.ADJUSTED)
            {
                List<string> lstMaDv = new List<string>();
                lstMaDv = lstChungTuChiTiet.Select(x => x.IID_MaDonVi).Distinct().ToList();
                chungTuPhanBoModel.SDS_IDMaDonVi = string.Join(",", lstMaDv);
            }

            BhPbdttmBHYT chungtu = new BhPbdttmBHYT();

            chungtu = _mapper.Map(chungTuPhanBoModel, chungtu);
            _pbdttmBHYTService.Update(chungtu);

            IsSaveData = false;
            OnRefresh();
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            SavedAction?.Invoke(null);
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        private void GetPlanData()
        {
            var vouchers = _khtmService.FindAggregateVoucher(NamLamViec);
            if (vouchers != null)
            {
                var planData = _khtmChiTietService.GetPlanData(NamLamViec, vouchers.STongHop).ToList();
                if (planData != null)
                {
                    var itemFilter = Items.Where(x => !x.IsHangCha && x.Type == (int)BaoHiemDuToanTypeEnum.Type.HANG_CON && planData.Select(x => x.IIDMaDonVi).Contains(x.IID_MaDonVi));

                    Parallel.ForEach(itemFilter, itemPB =>
                    {
                        itemPB.FDuToan = planData.Where(x => x.SXauNoiMa == itemPB.SXauNoiMa && x.IIDMaDonVi == itemPB.IID_MaDonVi)
                            .Select(x => x.FThanhTien.GetValueOrDefault()).FirstOrDefault();

                        if (itemPB.IsEmptyPlanData)
                            itemPB.IsModified = false;
                    });
                }
            }
        }
        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhPbdttmBHYTChiTietModel> results = new List<BhPbdttmBHYTChiTietModel>();

                List<string> lstSXaNoiMaChildSearch = DataPopupSearchItems.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.BHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                List<string> lstSXaNoiMaParentSearch = DataPopupSearchItems.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.BHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                if (!lstSXaNoiMaChildSearch.IsEmpty())
                {
                    lstParents.AddRange(StringUtils.GetListKyHieuParent(lstSXaNoiMaChildSearch));
                    if (lstParents.Any(x => x.Count() >= 3))
                    {
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
                    }
                    results = DataPopupSearchItems.Where(x => lstParents.Contains(x.SXauNoiMa)).ToList();
                }
                if (!lstSXaNoiMaParentSearch.IsEmpty())
                {
                    if (results.IsEmpty())
                        results = GetDataParent(lstSXaNoiMaParentSearch);
                    else
                        results.AddRange(GetDataParent(lstSXaNoiMaParentSearch.Where(x => !lstParents.Contains(x)).ToList()));
                }
                DataSearch = new ObservableCollection<BhPbdttmBHYTChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhPbdttmBHYTChiTietModel>();
            }
            ItemsView.Refresh();
        }

        private List<BhPbdttmBHYTChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhPbdttmBHYTChiTietModel> result = new List<BhPbdttmBHYTChiTietModel>();
            List<string> lstParent = StringUtils.GetListKyHieuParent(lstInput);
            if (!lstParent.IsEmpty() && lstParent.Any(x => x.Count() >= 3))
            {
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
            }
            var lstData = DataPopupSearchItems.Where(x => lstParent.Contains(x.SXauNoiMa)).ToList();
            result.AddRange(lstData);
            GetListChild(lstData.Where(x => lstInput.Contains(x.SXauNoiMa)).ToList(), result);
            return result;
        }

        private void GetListChild(List<BhPbdttmBHYTChiTietModel> lstInput, List<BhPbdttmBHYTChiTietModel> results)
        {
            var itemChild = DataPopupSearchItems.Where(x => lstInput.Select(x => x.IID_MLNS).Distinct().Contains(x.IID_MLNS_Cha ?? Guid.Empty)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => DataPopupSearchItems.Select(y => y.IID_MLNS_Cha).Distinct().Contains(x.IID_MLNS)))
                {
                    GetListChild(new List<BhPbdttmBHYTChiTietModel>() { item }, results);
                }
            }
        }

        private bool AllocationDetailItemsFilter(object obj)
        {
            bool result = true;
            var item = (BhPbdttmBHYTChiTietModel)obj;
            if (!string.IsNullOrEmpty(SelectedLNS))
                result = result && item.SLNS.ToLower().StartsWith(SelectedLNS.Trim().ToLower());

            if (!string.IsNullOrEmpty(TypeDisplaysSelected))
            {
                if (TypeDisplaysSelected == TypeDisplay.CO_DU_LIEU)
                    result = result && (!NumberUtils.DoubleIsNullOrZero(item.FDuToan) && !item.BHangCha && !item.IsDeleted);
                else if (TypeDisplaysSelected == TypeDisplay.DA_NHAN_DUTOAN)
                    result = result && (!NumberUtils.DoubleIsNullOrZero(item.FDuToan) || (!item.IID_DTTM_BHYT_ThanNhan.IsNullOrEmpty()) && !item.IsDeleted);
            }

            if (SelectedAgency != null)
            {
                result = result && ((!string.IsNullOrEmpty(item.IID_MaDonVi) && item.IID_MaDonVi.StartsWith(SelectedAgency.ValueItem)));
            }

            if (!string.IsNullOrEmpty(DetailFilter.L))
                result = result && item.SL.ToLower().StartsWith(DetailFilter.L.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.K))
                result = result && item.SK.ToLower().StartsWith(DetailFilter.K.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.SM.ToLower().StartsWith(DetailFilter.M.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.STM.ToLower().StartsWith(DetailFilter.TM.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.STTM.ToLower().StartsWith(DetailFilter.TTM.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.SNG.ToLower().StartsWith(DetailFilter.NG.Trim().ToLower());

            item.IsFilter = result;
            return result;
        }

        public void BeForeRefresh()
        {
            _filterResult = Items.Where(item => AllocationDetailItemsFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
            if (!string.IsNullOrEmpty(TypeDisplaysSelected) && TypeDisplaysSelected == TypeDisplay.DA_NHAN_DUTOAN)
            {
                var lstXauNoiMa = Items.Where(x => !x.IsHangCha && !x.IID_DTTM_BHYT_ThanNhan.IsNullOrEmpty()).Select(s => s.SXauNoiMa).Distinct().ToList();
                if (lstXauNoiMa.IsEmpty())
                    _filterXauNoiMaDTTM = new HashSet<string>();
                else
                {
                    var lstXc = StringUtils.GetListKyHieuParent(lstXauNoiMa);
                    _filterXauNoiMaDTTM = new HashSet<string>(StringUtils.GetListKyHieuParent(lstXauNoiMa));
                }
            }
        }

        private void CalculateTotalParent()
        {
            // Reset value parent
            Items.Where(x => x.IsHangCha && x.IsFilter && x.Type == (int)BaoHiemDuToanTypeEnum.RowType.HANG_CHA).ForAll(x => ResetItemValue(x));

            var temps = Items.Where(x => x.IsEditable && x.IsFilter && HasInputData(x) && x.Type == (int)BaoHiemDuToanTypeEnum.RowType.HANG_CON).GroupBy(n => n.IID_MLNS_Cha).ToList();

            var listParent = new List<BhPbdttmBHYTChiTietModel>();
            foreach (var temp in temps)
            {
                var listChild = temp.ToList();
                var parent = Items.FirstOrDefault(x => x.IID_MLNS == listChild.First().IID_MLNS_Cha);
                if (parent != null)
                {
                    CountParentChild(listChild, parent);
                    listParent.Add(parent);
                }
            }
            if (listParent.Count > 0)
            {
                CalculateReverse(listParent);
            }

            if (!_sessionService.Current.IdsDonViQuanLy.Split(StringUtils.COMMA).Any(x => _nsDonViService.FindByIdDonVi(x, _sessionService.Current.YearOfWork)?.Loai.Equals(LoaiDonVi.ROOT) ?? false))
            {
                var temps2 = Items.Where(x => x.Type == (int)DuToanRowType.RowChiTiet || x.Type == (int)DuToanRowType.RowNhanPhanBoOrTong).GroupBy(n => new { n.IID_MLNS_Cha, n.SXauNoiMa }).ToList();

                foreach (var temp in temps2)
                {
                    var listChild = temp.ToList();
                    CountParentChild(listChild.Skip(1).ToList(), listChild.First());
                }
            }
        }

        private BhPbdttmBHYTChiTietModel ResetItemValue(BhPbdttmBHYTChiTietModel item)
        {
            item.FDuToan = 0;
            item.SGhiChu = string.Empty;
            return item;
        }

        private void CountParentChild(List<BhPbdttmBHYTChiTietModel> listChild, BhPbdttmBHYTChiTietModel parent)
        {
            if (parent == null) return;
            parent.FDuToan = listChild.Sum(n => n.FDuToan);
        }

        private void CalculateReverse(List<BhPbdttmBHYTChiTietModel> items)
        {
            var temps = items.GroupBy(n => n.IID_MLNS_Cha).ToList();
            var listParent = new List<BhPbdttmBHYTChiTietModel>();
            foreach (var temp in temps)
            {
                var listChild = Items.Where(x => x.IsFilter && x.IID_MLNS_Cha == temp.First().IID_MLNS_Cha && x.Type != (int)BaoHiemDuToanTypeEnum.RowType.SO_CHUA_PHAN_BO).ToList();
                if (listChild.Count > 0)
                {
                    var parent = Items.FirstOrDefault(x => x.IID_MLNS == listChild.First().IID_MLNS_Cha);
                    if (parent != null)
                    {
                        CountParentChild(listChild, parent);
                        listParent.Add(parent);
                    }
                }
            }
            if (listParent.Count > 0)
            {
                CalculateReverse(listParent);
            }
        }

        private bool HasInputData(BhPbdttmBHYTChiTietModel item)
        {
            return item.HasData;
        }
    }
}
