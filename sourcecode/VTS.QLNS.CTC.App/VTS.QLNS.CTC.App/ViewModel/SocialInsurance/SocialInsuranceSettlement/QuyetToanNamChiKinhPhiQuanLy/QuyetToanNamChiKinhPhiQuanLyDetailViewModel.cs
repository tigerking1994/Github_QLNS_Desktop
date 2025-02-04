using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiQuanLy;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiQuanLy.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiQuanLy.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiQuanLy
{
    public class QuyetToanNamChiKinhPhiQuanLyDetailViewModel : DetailViewModelBase<BhQtcNamKinhPhiQuanLyModel, BhQtcNamKinhPhiQuanLyChiTietModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly IBhQtcNamKinhPhiQuanLyService _kinhPhiQuanLyService;
        private readonly IBhQtcNamKinhPhiQuanLyChiTietService _kinhPhiQuanLyChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private ICollectionView ItemsView;
        private string xnmConcatenation = "";
        #endregion

        #region Property
        private SessionInfo _sessionInfo;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public bool IsAggregate => !string.IsNullOrEmpty(Model.STongHop);
        public bool IsShowAgencyFilter => IsAggregate && _selectedTypeShowAgencyKHT != null && _selectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI;
        private ICollection<BhQtcNamKinhPhiQuanLyChiTietModel> _filterResult = new HashSet<BhQtcNamKinhPhiQuanLyChiTietModel>();
        public bool? IsReadOnlySoThucChi => !Model.BThucChiTheo4Quy;
        public bool IsOpenPrintPopup = true;
        public bool? IsReadOnlyThucChi => Model.BThucChiTheo4Quy;
        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set
            {
                SetProperty(ref _isLock, value);
                OnPropertyChanged(nameof(IsEnabledDelete));
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                if (!string.IsNullOrEmpty(_searchText))
                {
                    SearchDataParent();
                }
            }
        }

        private bool _isShowColumnUnit;
        public bool IsShowColumnUnit
        {
            get => _isShowColumnUnit;
            set => SetProperty(ref _isShowColumnUnit, value);
        }

        private ObservableCollection<ComboboxItem> _typeShowAgencyKHT;
        public ObservableCollection<ComboboxItem> TypeShowAgencyKHT
        {
            get => _typeShowAgencyKHT;
            set => SetProperty(ref _typeShowAgencyKHT, value);
        }

        private ComboboxItem _selectedTypeShowAgencyKHT;
        public ComboboxItem SelectedTypeShowAgencyKHT
        {
            get => _selectedTypeShowAgencyKHT;
            set
            {
                if (SetProperty(ref _selectedTypeShowAgencyKHT, value))
                {
                    if (_selectedTypeShowAgencyKHT != null && _selectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI)
                        _isShowColumnUnit = true;
                    else _isShowColumnUnit = false;
                    LoadData();
                    OnPropertyChanged(nameof(IsShowColumnUnit));
                    OnPropertyChanged(nameof(IsShowAgencyFilter));
                }
            }
        }

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
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
                if (_selectedAgency != null)
                {
                    LoadData();
                }
                //BeForeRefresh();
                //ItemsView.Refresh();
                //CalculateData();
            }
        }

        //List<BhQtcNamKinhPhiQuanLyChiTietQuery> _listChungTuChiTiet;
        List<BhQtcNamKinhPhiQuanLyChiTietQuery> _listChungTuChiTietTheoQuy;

        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted)
                    || Items.Any(x => !x.IsHangCha);
        public bool IsInit { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public override Type ContentType => typeof(QuyetToanNamChiKinhPhiQuanLyDetail);

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set
            {
                if (SetProperty(ref _sNoiDungSearch, value))
                {
                    //SearchTextFilter();
                    //ItemsView.Refresh();
                }
            }
        }

        private string _sM;
        public string SM
        {
            get => _sM;
            set
            {
                if (SetProperty(ref _sM, value))
                {
                    //SearchTextFilter();
                    //ItemsView.Refresh();
                }
            }
        }

        private string _sTM;
        public string STM
        {
            get => _sTM;
            set
            {
                if (SetProperty(ref _sTM, value))
                {
                    //SearchTextFilter();
                    //_itemViews.Refresh();
                }
            }
        }

        private ObservableCollection<BhQtcNamKinhPhiQuanLyChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhQtcNamKinhPhiQuanLyChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhQtcNamKinhPhiQuanLyChiTietModel _selectedPopupItem;
        public BhQtcNamKinhPhiQuanLyChiTietModel SelectedPopupItem
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

        private bool _isPopupOpen;
        private bool IsDefault
        {
            get
            {
                return string.IsNullOrEmpty(SM) && string.IsNullOrEmpty(STM) && string.IsNullOrEmpty(SNoiDungSearch);
            }
        }
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private ObservableCollection<BhQtcNamKinhPhiQuanLyChiTietModel> _dataSearch;
        public ObservableCollection<BhQtcNamKinhPhiQuanLyChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }

        private ObservableCollection<ComboboxItem> _typeShows;
        public ObservableCollection<ComboboxItem> TypeShows
        {
            get => _typeShows;
            set => SetProperty(ref _typeShows, value);
        }

        private ComboboxItem _typeShowsSelected;
        public ComboboxItem TypeShowsSelected
        {
            get { return _typeShowsSelected; }
            set
            {
                if (SetProperty(ref _typeShowsSelected, value) && ItemsView != null)
                {
                    OnRefresh();
                    ItemsView.Refresh();
                }
            }
        }


        #endregion

        #region View model
        public PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel { get; set; }
        #endregion

        #region RelayCommand
        public RelayCommand RefreshCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand SearchCommand { get; }
        #endregion

        #region Constructor
        public QuyetToanNamChiKinhPhiQuanLyDetailViewModel(
              IMapper mapper,
              ISessionService sessionService,
              ILog loger,
              INsDonViService nsDonViService,
              INsNguoiDungDonViService iNguoiDungDonViService,
              IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
              IBhQtcNamKinhPhiQuanLyService namKinhPhiQuanLyService,
              IBhQtcNamKinhPhiQuanLyChiTietService namKinhPhiQuanLyChiTietService,
              PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel printQuyetToanChiNamKinhPhiQuanLyNoticeViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = loger;
            _nsDonViService = nsDonViService;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _kinhPhiQuanLyService = namKinhPhiQuanLyService;
            _kinhPhiQuanLyChiTietService = namKinhPhiQuanLyChiTietService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;

            SaveCommand = new RelayCommand(obj => OnSave());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            PrintReportCommand = new RelayCommand(OnPrintDetal);
            RefreshCommand = new RelayCommand(obj => Init());
            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel = printQuyetToanChiNamKinhPhiQuanLyNoticeViewModel;
        }
        #endregion

        #region Init
        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _isShowColumnUnit = false;
            _selectedAgency = null;
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                //IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            if (!string.IsNullOrEmpty(Model.STongHop))
            {
                LoadComboboxTypeShow();
            }
            SearchText = string.Empty;
            IsInit = true;
            LoadTypeShow();
            LoadData();
            IsInit = false;
            LoadDefault();
        }

        private void LoadComboboxTypeShow()
        {
            TypeShowAgencyKHT = new ObservableCollection<ComboboxItem>();
            TypeShowAgencyKHT.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgencyKHT.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgencyKHT = TypeShowAgencyKHT.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedTypeShowAgencyKHT));
        }

        private void LoadTypeShow()
        {
            TypeShows = new ObservableCollection<ComboboxItem>();
            TypeShows.Add(new ComboboxItem { ValueItem = TypeDisplay.BH_TAT_CA, DisplayItem = TypeDisplay.BH_TAT_CA });
            TypeShows.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_SO_LIEU, DisplayItem = TypeDisplay.CO_SO_LIEU });
            TypeShows.Add(new ComboboxItem { ValueItem = TypeDisplay.CHUA_CO_SO_LIEU, DisplayItem = TypeDisplay.CHUA_CO_SO_LIEU });
            TypeShowsSelected = TypeShows.FirstOrDefault();
            OnPropertyChanged(nameof(TypeShowsSelected));
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        private void LoadAgencies(string agencyIds)
        {
            var listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            OnPropertyChanged(nameof(Agencies));
        }


        #endregion

        #region On Open Report
        private void OnPrintDetal(object param)
        {
            try
            {
                if (!_sessionService.Current.IsQuanLyDonViCha)
                {
                    MessageBoxHelper.Warning(Resources.AlertRolePrintReportAllocation);
                    return;
                }

                int dialogType = (int)param;

                switch (dialogType)
                {
                    case (int)SettlementTypePrint.PRINT_SETTLEMENT_ADDENDUM:
                    case (int)SettlementTypePrint.PRINT_SETTLEMENT_PALN:
                        PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel.SettlementTypeValue = dialogType;
                        PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel.Init();
                        var view2 = new PrintQuyetToanNamChiKinhPhiQuanLyNotice
                        {
                            DataContext = PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel
                        };
                        DialogHost.Show(view2, SettlementScreen.DETAIL_DIALOG, null, null);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Load data
        public override void LoadData(params object[] args)
        {
            try
            {
                base.LoadData(args);
                List<BhQtcNamKinhPhiQuanLyChiTietQuery> _listChungTuChiTiet = new List<BhQtcNamKinhPhiQuanLyChiTietQuery>();
                var iNamLamViec = _sessionInfo.YearOfWork;
                var danhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(iNamLamViec).ToList();
                var loaiChi = danhMucLoaiChi.Where(x => x.SLNS.Equals(SettlementTypeSLNS.SLNS)).FirstOrDefault();
                DonVi donViParent = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, iNamLamViec);
                QtcNamKinhPhiQuanLyCriteria searchCondition = new QtcNamKinhPhiQuanLyCriteria();
                searchCondition.NamLamViec = iNamLamViec;
                searchCondition.IDMaDonVi = Model.IID_MaDonVi;
                searchCondition.IDDonVi = Model.IID_DonVi;
                searchCondition.SNguoiTao = Model.SNguoiTao;
                searchCondition.LoaiChungTu = Model.ILoaiTongHop;
                searchCondition.ID = Model.Id;
                searchCondition.SLNS = LNSValue.LNS_9010003;
                searchCondition.IDLoaiChi = loaiChi.Id;
                searchCondition.DNgayChungTu = Model.DNgayChungTu;
                searchCondition.IsTongHop4Quy = Model.BThucChiTheo4Quy;
                searchCondition.SMaLoaiChi = loaiChi.SMaLoaiChi;

                var existChungTuChiTiet = _kinhPhiQuanLyChiTietService.ExitChungTuChiTiet(searchCondition);
                if (IsAggregate && SelectedTypeShowAgencyKHT != null && SelectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI && _selectedAgency == null)
                {
                    var voucherNos = Model.STongHop.Split(",").ToList();
                    List<BhQtcNamKinhPhiQuanLy> listChungTu = _kinhPhiQuanLyService.FindByAggregateVoucher(voucherNos, _sessionInfo.YearOfWork).ToList();
                    string agencyIds = string.Join(",", listChungTu.Select(x => x.IID_MaDonVi));
                    var listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
                    List<BhQtcNamKinhPhiQuanLyChiTietQuery> listChungTuChiTietParent = new List<BhQtcNamKinhPhiQuanLyChiTietQuery>();
                    List<BhQtcNamKinhPhiQuanLyChiTietQuery> listChungTuChiTietChildren = new List<BhQtcNamKinhPhiQuanLyChiTietQuery>();
                    var temp = new List<BhQtcNamKinhPhiQuanLyChiTietQuery>();
                    foreach (var chungTu in listChungTu)
                    {
                        searchCondition.LoaiChungTu = chungTu.IID_MaDonVi == donViParent.IIDMaDonVi ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;
                        searchCondition.ID = chungTu.Id;
                        searchCondition.IDMaDonVi = chungTu.IID_MaDonVi;
                        List<BhQtcNamKinhPhiQuanLyChiTietQuery> listQuery = _kinhPhiQuanLyChiTietService.FindChungTuChiTiet(searchCondition).ToList();
                        listQuery.Where(x => !x.IsHangCha).Select(x => x.STenDonVi == listDonVi.FirstOrDefault(x => x.IIDMaDonVi == chungTu.IID_MaDonVi).TenDonVi).ToList();
                        listChungTuChiTietParent.AddRange(listQuery.Where(x => x.IsHangCha));
                        listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.IsHangCha));
                    }
                    var listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                    var lstChungTu = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x =>
                        new
                        {
                            Data = x.FirstOrDefault(),
                            FTien_DuToanGiaoNamNay = x.Sum(x => x.FTien_DuToanGiaoNamNay)
                        }
                    ).ToList();

                    lstChungTu.ForEach(x =>
                    {
                        x.Data.FTien_DuToanGiaoNamNay = x.FTien_DuToanGiaoNamNay;
                    });

                    listChungTuChiTietParent = lstChungTu.Select(x => x.Data).ToList();
                    temp.AddRange(listChungTuChiTietParent);
                    temp.AddRange(listChungTuChiTietChildren);
                    temp = temp.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.STenDonVi).ToList();
                    _listChungTuChiTiet = temp;
                    _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
                    OnPropertyChanged(nameof(Agencies));
                }
                else
                {
                    searchCondition.ID = Model.Id;
                    searchCondition.IDMaDonVi = Model.IID_MaDonVi;

                    if (_selectedAgency != null && SelectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI)
                    {
                        searchCondition.IDMaDonVi = _selectedAgency.ValueItem;
                        searchCondition.LoaiChungTu = _selectedAgency.ValueItem == donViParent.IIDMaDonVi ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;
                        var predicateCtDv = PredicateBuilder.True<BhQtcNamKinhPhiQuanLy>();
                        predicateCtDv = predicateCtDv.And(x => x.INamLamViec == Model.INamLamViec);
                        predicateCtDv = predicateCtDv.And(x => x.IID_MaDonVi == _selectedAgency.ValueItem);
                        var ctDonVi = _kinhPhiQuanLyService.FindByCondition(predicateCtDv).FirstOrDefault();
                        if (ctDonVi != null)
                            searchCondition.ID = ctDonVi.Id;

                        _listChungTuChiTiet = _kinhPhiQuanLyChiTietService.FindChungTuChiTiet(searchCondition).ToList();
                    }
                    else
                    {

                        searchCondition.LoaiChungTu = Model.IID_MaDonVi == donViParent.IIDMaDonVi ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;
                        _listChungTuChiTiet = _kinhPhiQuanLyChiTietService.FindChungTuChiTiet(searchCondition).ToList();
                    }
                }

                Items = _mapper.Map<ObservableCollection<BhQtcNamKinhPhiQuanLyChiTietModel>>(_listChungTuChiTiet);
                DataPopupSearchItems = _mapper.Map<ObservableCollection<BhQtcNamKinhPhiQuanLyChiTietModel>>(_listChungTuChiTiet);
                ItemsView = CollectionViewSource.GetDefaultView(Items);
                ItemsView.Filter = ItemsViewFilter;
                foreach (var chungTu in Items)
                {
                    chungTu.IsFilter = true;
                    if (!chungTu.IsHangCha)
                    {
                        chungTu.PropertyChanged += (sender, args) =>
                        {
                            BhQtcNamKinhPhiQuanLyChiTietModel item = (BhQtcNamKinhPhiQuanLyChiTietModel)sender;
                            if (args.PropertyName == nameof(BhQtcNamKinhPhiQuanLyChiTietModel.FTien_ThucChi))
                            {
                                item.IsModified = true;
                                CalculateData();
                                chungTu.IsModified = true;
                                OnPropertyChanged(nameof(IsSaveData));
                                OnPropertyChanged(nameof(IsOpenPrintPopup));
                            }
                        };
                    }
                }

                CalculateData();
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateData()
        {
            try
            {
                Items.Where(x => x.IsHangCha)
                 .ForAll(x =>
                 {
                     x.FTien_DuToanNamTruocChuyenSang = 0;
                     //x.FTien_DuToanGiaoNamNay = 0;
                     x.FTien_ThucChi = 0;
                     x.STenDonVi = string.Empty;
                 });

                Items.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi) && x.IsHangCha).Select(x =>
                {
                    x.FTien_DuToanNamTruocChuyenSang += x.FDuToanNamTruocChuyenSang;
                    return x;
                }).ToList();

                var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
                var dictByMlns = Items.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
                foreach (var item in temp)
                {
                    CalculateParent(item.IdParent, item, dictByMlns);
                }

                UpdateTotal();
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private void UpdateTotal()
        {
            Model.FTongTien_DuToanNamTruocChuyenSang = 0;
            Model.FTongTien_DuToanGiaoNamNay = 0;
            Model.FTongTien_TongDuToanDuocGiao = 0;
            Model.FTongTien_ThucChi = 0;
            Model.FTongTienThieu = 0;
            Model.FTongTienThua = 0;
            Model.FTiLeThucHienTrenDuToan = 0;
            var roots = Items.Where(t => string.IsNullOrEmpty(t.SL)).ToList();
            foreach (var item in roots)
            {
                Model.FTongTien_DuToanNamTruocChuyenSang += item.FTien_DuToanNamTruocChuyenSang;
                Model.FTongTien_DuToanGiaoNamNay += item.FTien_DuToanGiaoNamNay;
                Model.FTongTien_TongDuToanDuocGiao += item.FTien_TongDuToanDuocGiao;
                Model.FTongTien_ThucChi += item.FTien_ThucChi;
                Model.FTongTienThua += item.FTienThua;
                Model.FTongTienThieu += item.FTienThieu;
                //Model.FTiLeThucHienTrenDuToan += item.FTiLeThucHienTrenDuToan;
            }

            Model.FTiLeThucHienTrenDuToan = Model.FTongTien_TongDuToanDuocGiao > 0 ? (Model.FTongTien_ThucChi / Model.FTongTien_TongDuToanDuocGiao) * 100 : 0;
        }

        private void CalculateParent(Guid idParent, BhQtcNamKinhPhiQuanLyChiTietModel item, Dictionary<Guid?, BhQtcNamKinhPhiQuanLyChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTien_DuToanNamTruocChuyenSang = (model.FTien_DuToanNamTruocChuyenSang ?? 0) + (item.FTien_DuToanNamTruocChuyenSang ?? 0);
            model.FTien_DuToanGiaoNamNay += item.FTien_DuToanGiaoNamNay;
            model.FTien_TongDuToanDuocGiao += item.FTien_TongDuToanDuocGiao;
            model.FTien_ThucChi += item.FTien_ThucChi;

            CalculateParent(model.IdParent, item, dictByMlns);
        }

        private void SearchDataParent()
        {
            ItemsView?.Refresh();
        }

        protected override void OnRefresh()
        {
            IsInit = true;
            LoadData();
            IsInit = false;
        }
        #endregion

        #region OnSave
        public override void OnSave()
        {
            try
            {
                if (!IsSaveData)
                {
                    return;
                }

                Func<BhQtcNamKinhPhiQuanLyChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.Id.IsNullOrEmpty() && !x.IsHangCha;
                Func<BhQtcNamKinhPhiQuanLyChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.Id.IsNullOrEmpty() && !x.IsHangCha;

                var detailsAdd = Items.Where(isAdd).ToList();

                var detailsUpdate = Items.Where(isUpdate).ToList();

                //thêm mới chứng từ chi tiết
                if (detailsAdd.Count > 0)
                {
                    var addItems = new List<BhQtcNamKinhPhiQuanLyChiTiet>();

                    detailsAdd.ForEach(x =>
                    {
                        x.IIdMaDonVi = Model.IID_MaDonVi;
                        x.DNgayTao = DateTime.Now;
                        x.SNguoiTao = _sessionInfo.Principal;
                    });
                    _mapper.Map(detailsAdd, addItems);
                    _kinhPhiQuanLyChiTietService.AddRange(addItems);

                    Items.Where(isAdd).Select(x =>
                    {
                        x.IsModified = false;
                        x.IsAdd = false;
                        return x;
                    }).ToList();
                }

                //cập nhật chứng từ chi tiết
                if (detailsUpdate.Count > 0)
                {
                    foreach (var updateItem in detailsUpdate)
                    {
                        var chungTuChiTiet = _kinhPhiQuanLyChiTietService.FindById(updateItem.Id);
                        updateItem.IIdMaDonVi = Model.IID_MaDonVi;
                        updateItem.SNguoiSua = _sessionInfo.Principal;
                        updateItem.DNgayTao = DateTime.Now;
                        _mapper.Map(updateItem, chungTuChiTiet);
                        chungTuChiTiet.DNgaySua = DateTime.Now;
                        chungTuChiTiet.SNguoiSua = _sessionInfo.Principal;
                        _kinhPhiQuanLyChiTietService.Update(chungTuChiTiet);
                        updateItem.IsModified = false;
                    }
                }

                //cập nhật tổng cộng chứng từ
                if (detailsAdd.Count > 0 || detailsUpdate.Count > 0)
                {
                    var chungTuChiTiet = _kinhPhiQuanLyService.FindById(Model.Id);
                    chungTuChiTiet.FTongTien_DuToanNamTruocChuyenSang = Model.FTongTien_DuToanNamTruocChuyenSang;
                    chungTuChiTiet.FTongTien_DuToanGiaoNamNay = Model.FTongTien_DuToanGiaoNamNay;
                    chungTuChiTiet.FTongTien_TongDuToanDuocGiao = Model.FTongTien_TongDuToanDuocGiao;
                    chungTuChiTiet.FTongTienThua = Model.FTongTienThua;
                    chungTuChiTiet.FTongTien_ThucChi = Model.FTongTien_ThucChi;
                    chungTuChiTiet.FTongTienThieu = Model.FTongTienThieu;
                    chungTuChiTiet.FTiLeThucHienTrenDuToan = chungTuChiTiet.FTongTien_TongDuToanDuocGiao > 0 ? (chungTuChiTiet.FTongTien_ThucChi / chungTuChiTiet.FTongTien_TongDuToanDuocGiao) * 100 : 0;
                    chungTuChiTiet.DNgaySua = DateTime.Now;
                    chungTuChiTiet.SNguoiSua = _sessionInfo.Principal;
                    _kinhPhiQuanLyService.Update(chungTuChiTiet);

                    OnRefresh();
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IsDeleteAll));
                    UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
                    var message = Resources.MsgSaveDone;
                    MessageBoxHelper.Info(message);
                }
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region close
        public override void OnClose(object obj)
        {
            ((Window)obj).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }
        #endregion

        #region Search

        private bool ItemsViewFilter(object obj)
        {
            //bool result = true;
            //var item = (BhQtcNamKinhPhiQuanLyChiTietModel)obj;
            //if (result)
            //    item.IsFilter = result;
            //if (!IsDefault)
            //{
            //    result = result && DataSearch.Any(x => x.IID_MucLucNganSach.Equals(item.IID_MucLucNganSach));

            //}
            //return result;

            bool result = true;
            var item = (BhQtcNamKinhPhiQuanLyChiTietModel)obj;
            result = VoucherDetailFilter(item);
            if (!result && item.IsHangCha)
            {
                result = xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (result)
                item.IsFilter = result;
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = DataSearch.Any(x => x.IID_MucLucNganSach.Equals(item.IID_MucLucNganSach));
            }

            return result;
        }

        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            var item = (BhQtcNamKinhPhiQuanLyChiTietModel)obj;

            if (TypeShowsSelected != null)
            {
                if (TypeShowsSelected.ValueItem == TypeDisplay.CO_SO_LIEU)
                    result = result && item.IsHasData && !item.BHangCha;
                else if (TypeShowsSelected.ValueItem == TypeDisplay.CHUA_CO_SO_LIEU)
                    result = result && !item.IsHasDataChil;
            }
            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIdMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;
        }


        private void OnSearch()
        {
            SearchTextFilter();
        }


        private void LoadDefault()
        {
            SM = string.Empty;
            STM = string.Empty;
            SNoiDungSearch = string.Empty;
            DataSearch = new ObservableCollection<BhQtcNamKinhPhiQuanLyChiTietModel>();
        }

        private void OnClearSearch(object obj)
        {
            LoadDefault();
            ItemsView.Refresh();
        }

        private void SearchTextFilter()
        {
            List<string> lstParents = new List<string>();
            List<BhQtcNamKinhPhiQuanLyChiTietModel> lstChildSearch = new List<BhQtcNamKinhPhiQuanLyChiTietModel>();
            List<BhQtcNamKinhPhiQuanLyChiTietModel> lstParentSearch = new List<BhQtcNamKinhPhiQuanLyChiTietModel>();
            List<BhQtcNamKinhPhiQuanLyChiTietModel> results = new List<BhQtcNamKinhPhiQuanLyChiTietModel>();
            if (!string.IsNullOrEmpty(SM))
            {
                lstChildSearch = Items.Where(x => x.SM.ToLower().Contains(SM.ToLower()) && !x.IsHangCha).ToList();
                lstParentSearch = Items.Where(x => x.SM.ToLower().Contains(SM.ToLower()) && x.IsHangCha).ToList();
            }

            if (!string.IsNullOrEmpty(STM))
            {
                if (!string.IsNullOrEmpty(SM))
                {
                    lstChildSearch = lstChildSearch.Where(x => x.STM.ToLower().Contains(STM.ToLower()) && !x.IsHangCha).ToList();
                    lstParentSearch = lstParentSearch.Where(x => x.STM.ToLower().Contains(STM.ToLower()) && x.IsHangCha).ToList();
                }
                else
                {
                    lstChildSearch = Items.Where(x => x.STM.ToLower().Contains(STM.ToLower()) && !x.IsHangCha).ToList();
                    lstParentSearch = Items.Where(x => x.STM.ToLower().Contains(STM.ToLower()) && x.IsHangCha).ToList();
                }
            }
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                if (!string.IsNullOrEmpty(SM))
                {
                    lstChildSearch = lstChildSearch.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).ToList();
                    lstParentSearch = lstParentSearch.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).ToList();
                }
                else if (!string.IsNullOrEmpty(STM))
                {
                    lstChildSearch = lstChildSearch.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).ToList();
                    lstParentSearch = lstParentSearch.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).ToList();
                }
                else
                {
                    lstChildSearch = Items.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).ToList();
                    lstParentSearch = Items.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).ToList();
                }

            }
            if (!lstChildSearch.IsEmpty())
            {
                lstParents.AddRange(StringUtils.GetListKyHieuParent(lstChildSearch.Select(x => x.SXauNoiMa).Distinct().ToList()));
                if (lstParents.Any(x => x.Count() >= 3))
                {
                    lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                    lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
                }

                results = Items.Where(x => lstParents.Contains(x.SXauNoiMa)).ToList();
            }
            if (!lstParentSearch.IsEmpty())
            {
                if (results.IsEmpty())
                    results = GetDataParent(lstParentSearch.Select(x => x.SXauNoiMa).Distinct().ToList());
                else
                    results.AddRange(GetDataParent(lstParentSearch.Select(x => x.SXauNoiMa).Distinct().ToList()));
            }
            DataSearch = new ObservableCollection<BhQtcNamKinhPhiQuanLyChiTietModel>(results);
            ItemsView.Refresh();
        }

        private List<BhQtcNamKinhPhiQuanLyChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhQtcNamKinhPhiQuanLyChiTietModel> result = new List<BhQtcNamKinhPhiQuanLyChiTietModel>();
            List<string> lstParent = StringUtils.GetListKyHieuParent(lstInput);
            if (!lstParent.IsEmpty() && lstParent.Any(x => x.Count() >= 3))
            {
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
            }
            var lstData = Items.Where(x => lstParent.Contains(x.SXauNoiMa)).ToList();
            result.AddRange(lstData);
            GetListChild(lstData.Where(x => lstInput.Contains(x.SXauNoiMa)).ToList(), result);
            return result;
        }

        private void GetListChild(List<BhQtcNamKinhPhiQuanLyChiTietModel> lstInput, List<BhQtcNamKinhPhiQuanLyChiTietModel> results)
        {
            var itemChild = Items.Where(x => lstInput.Select(x => x.IID_MucLucNganSach).Distinct().Contains(x.IdParent)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => Items.Select(y => y.IdParent).Distinct().Contains(x.IID_MucLucNganSach ?? Guid.Empty)))
                {
                    GetListChild(new List<BhQtcNamKinhPhiQuanLyChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

    }
}
