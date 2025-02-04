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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac
{
    public class QuyetToanNamChiKinhPhiKhacDetailViewModel : DetailViewModelBase<BhQtcNamKinhPhiKhacModel, BhQtcNamKinhPhiKhacChiTietModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly IBhQtcNamKinhPhiKhacService _kinhPhiKhacService;
        private readonly IBhQtcNamKinhPhiKhacChiTietService _kinhPhiKhacChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private ICollectionView _itemsView;
        #endregion

        #region Property
        private SessionInfo _sessionInfo;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public bool IsAggregate => !string.IsNullOrEmpty(Model.STongHop);
        public bool? IsReadOnlySoThucChi => Model.BThucChiTheo4Quy;
        public bool IsOpenPrintPopup = true;
        public bool? IsReadOnlyThucChi => Model.BThucChiTheo4Quy;
        private string _searchText;
        readonly List<BhQtcNamKinhPhiKhacChiTietQuery> lstChungTuExistDuToan;
        private ICollection<BhQtcNamKinhPhiKhacChiTietModel> _filterResult = new HashSet<BhQtcNamKinhPhiKhacChiTietModel>();
        private string _xnmConcatenation = "";
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

        private ObservableCollection<ComboboxItem> _typeShowAgency;
        public ObservableCollection<ComboboxItem> TypeShowAgency
        {
            get => _typeShowAgency;
            set => SetProperty(ref _typeShowAgency, value);
        }

        private ComboboxItem _selectedTypeShowAgency;
        public ComboboxItem SelectedTypeShowAgency
        {
            get => _selectedTypeShowAgency;
            set
            {
                if (SetProperty(ref _selectedTypeShowAgency, value))
                {
                    LoadData();
                    OnPropertyChanged(nameof(IsShowAgencyFilter));
                }
            }
        }

        public bool IsShowAgencyFilter => IsAggregate && _selectedTypeShowAgency?.ValueItem == TypeDisplay.CHITIET_DONVI;

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

        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted)
                    || Items.Any(x => !x.IsHangCha);
        public bool IsInit { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public override Type ContentType => typeof(QuyetToanNamChiKinhPhiKhacDetail);

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private ObservableCollection<BhQtcNamKinhPhiKhacChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhQtcNamKinhPhiKhacChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhQtcNamKinhPhiKhacChiTietModel _selectedPopupItem;
        public BhQtcNamKinhPhiKhacChiTietModel SelectedPopupItem
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
                //BeforeRefresh();
                //_itemsView.Refresh();
                //CalculateData();
            }
        }


        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private ObservableCollection<BhQtcNamKinhPhiKhacChiTietModel> _dataSearch;
        public ObservableCollection<BhQtcNamKinhPhiKhacChiTietModel> DataSearch
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
                if (SetProperty(ref _typeShowsSelected, value) && _itemsView != null)
                {
                    OnRefresh();
                    _itemsView.Refresh();
                }
            }
        }

        #endregion

        #region View model
        public PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel { get; set; }
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
        public QuyetToanNamChiKinhPhiKhacDetailViewModel(
              IMapper mapper,
              ISessionService sessionService,
              ILog loger,
              INsDonViService nsDonViService,
              INsNguoiDungDonViService iNguoiDungDonViService,
              IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
              IBhQtcNamKinhPhiKhacService namKinhPhiKhacService,
              IBhQtcNamKinhPhiKhacChiTietService namKinhPhiKhacChiTietService,
              PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel printQuyetToanChiNamKinhPhiKhacNoticeViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = loger;
            _nsDonViService = nsDonViService;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _kinhPhiKhacService = namKinhPhiKhacService;
            _kinhPhiKhacChiTietService = namKinhPhiKhacChiTietService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;

            SaveCommand = new RelayCommand(obj => OnSave());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            PrintReportCommand = new RelayCommand(OnPrintDetal);
            RefreshCommand = new RelayCommand(obj => Init());
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SearchCommand = new RelayCommand(OnSearch);
            PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel = printQuyetToanChiNamKinhPhiKhacNoticeViewModel;
        }
        #endregion

        #region Init
        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _selectedAgency = null;
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                OnPropertyChanged(nameof(IsReadOnlySoThucChi));
            }

            LoadComboboxTypeShow();
            SearchText = string.Empty;
            IsInit = true;
            LoadTypeShow();
            LoadData();
            IsInit = false;
            OnClearSearch(false);
        }
        #endregion

        #region Load data
        private void LoadComboboxTypeShow()
        {
            TypeShowAgency = new ObservableCollection<ComboboxItem>();
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgency = TypeShowAgency.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedTypeShowAgency));
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

        public override void LoadData(params object[] args)
        {
            try
            {
                base.LoadData(args);
                int iNamLamViec = _sessionInfo.YearOfWork;
                List<BhDanhMucLoaiChi> danhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(iNamLamViec).ToList();
                string sLNS = danhMucLoaiChi.Where(x => x.Id.Equals(Model.IID_LoaiChi)).Select(x => x.SLNS).FirstOrDefault();

                List<BhQtcNamKinhPhiKhacChiTietQuery> _listChungTuChiTiet = new List<BhQtcNamKinhPhiKhacChiTietQuery>();

                QtcNamKinhPhiKhacCriteria searchCondition = new QtcNamKinhPhiKhacCriteria();
                searchCondition.NamLamViec = iNamLamViec;
                searchCondition.SNguoiTao = Model.SNguoiTao;
                searchCondition.SLNS = sLNS;
                searchCondition.DNgayChungTu = DateTime.Now;
                searchCondition.IsTongHop4Quy = Model.BThucChiTheo4Quy;

                bool existChungTuChiTiet = _kinhPhiKhacChiTietService.ExitChungTuChiTiet(searchCondition);

                if (IsAggregate && SelectedTypeShowAgency?.ValueItem == TypeDisplay.CHITIET_DONVI && _selectedAgency == null)
                {
                    List<string> voucherNos = Model.STongHop.Split(",").ToList();
                    List<BhQtcNamKinhPhiKhac> listChungTu = _kinhPhiKhacService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork && voucherNos.Contains(x.SSoChungTu) && x.BDaTongHop).ToList();
                    string agencyIds = string.Join(",", listChungTu.Select(x => x.IID_MaDonVi));
                    IEnumerable<DonVi> listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);

                    List<BhQtcNamKinhPhiKhacChiTietQuery> listChungTuChiTietParent = new List<BhQtcNamKinhPhiKhacChiTietQuery>();
                    List<BhQtcNamKinhPhiKhacChiTietQuery> listChungTuChiTietChildren = new List<BhQtcNamKinhPhiKhacChiTietQuery>();
                    List<BhQtcNamKinhPhiKhacChiTietQuery> temp = new List<BhQtcNamKinhPhiKhacChiTietQuery>();
                    foreach (BhQtcNamKinhPhiKhac chungTu in listChungTu)
                    {
                        searchCondition.NamLamViec = iNamLamViec;
                        searchCondition.IDMaDonVi = chungTu.IID_MaDonVi;
                        searchCondition.IDDonVi = chungTu.IID_DonVi;
                        searchCondition.SNguoiTao = chungTu.SNguoiTao;
                        searchCondition.LoaiChungTu = !IsDonViRoot(chungTu.IID_MaDonVi) ? BhxhLoaiChungTu.BhxhChungTu : BhxhLoaiChungTu.BhxhChungTuTongHop;
                        searchCondition.ID = chungTu.Id;
                        searchCondition.SLNS = sLNS;
                        searchCondition.IDLoaiChi = chungTu.IID_LoaiChi;
                        searchCondition.DNgayChungTu = DateTime.Now;
                        searchCondition.IsTongHop4Quy = chungTu.BThucChiTheo4Quy;
                        List<BhQtcNamKinhPhiKhacChiTietQuery> listQuery = _kinhPhiKhacChiTietService.FindChungTuChiTiet(searchCondition).ToList();

                        listQuery.Where(x => !x.IsHangCha).Select(x => x.STenDonVi = listDonVi.FirstOrDefault(y => y.IIDMaDonVi == chungTu.IID_MaDonVi).TenDonVi).ToList();

                        listChungTuChiTietParent.AddRange(listQuery.Where(x => x.IsHangCha));
                        listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.IsHangCha));
                    }
                    List<string> listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
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

                    if (_selectedAgency != null && SelectedTypeShowAgency?.ValueItem == TypeDisplay.CHITIET_DONVI)
                    {
                        searchCondition.IDMaDonVi = _selectedAgency.ValueItem;
                        searchCondition.LoaiChungTu = !IsDonViRoot(searchCondition.IDMaDonVi) ? BhxhLoaiChungTu.BhxhChungTu : BhxhLoaiChungTu.BhxhChungTuTongHop;
                        System.Linq.Expressions.Expression<Func<BhQtcNamKinhPhiKhac, bool>> predicateCtDv = PredicateBuilder.True<BhQtcNamKinhPhiKhac>();
                        predicateCtDv = predicateCtDv.And(x => x.INamLamViec == Model.INamLamViec);
                        predicateCtDv = predicateCtDv.And(x => x.IID_MaDonVi == _selectedAgency.ValueItem);
                        BhQtcNamKinhPhiKhac ctDonVi = _kinhPhiKhacService.FindByCondition(predicateCtDv).FirstOrDefault();
                        if (ctDonVi != null)
                            searchCondition.ID = ctDonVi.Id;
                        _listChungTuChiTiet = _kinhPhiKhacChiTietService.FindChungTuChiTiet(searchCondition).ToList();
                    }
                    else
                    {
                        searchCondition.LoaiChungTu = !IsDonViRoot(Model.IID_MaDonVi) ? BhxhLoaiChungTu.BhxhChungTu : BhxhLoaiChungTu.BhxhChungTuTongHop;
                        _listChungTuChiTiet = _kinhPhiKhacChiTietService.FindChungTuChiTiet(searchCondition).ToList();
                    }

                }

                Items = _mapper.Map<ObservableCollection<BhQtcNamKinhPhiKhacChiTietModel>>(_listChungTuChiTiet);
                DataPopupSearchItems = _mapper.Map<ObservableCollection<BhQtcNamKinhPhiKhacChiTietModel>>(_listChungTuChiTiet);
                _itemsView = CollectionViewSource.GetDefaultView(Items);
                _itemsView.Filter = ItemsViewFilter;
                foreach (BhQtcNamKinhPhiKhacChiTietModel chungTu in Items)
                {
                    chungTu.IsFilter = true;
                    if (!chungTu.IsHangCha)
                    {
                        chungTu.PropertyChanged += (sender, args) =>
                        {
                            BhQtcNamKinhPhiKhacChiTietModel item = (BhQtcNamKinhPhiKhacChiTietModel)sender;
                            if (args.PropertyName == nameof(BhQtcNamKinhPhiKhacChiTietModel.FTien_ThucChi))
                            {
                                //ShowColumn(item);
                                item.IsModified = true;
                                item.FTien_TongDuToanDuocGiao = item.FTien_DuToanGiaoNamNay.GetValueOrDefault(0) + item.FTien_DuToanNamTruocChuyenSang.GetValueOrDefault(0);
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

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        private bool ItemsViewFilter(object obj)
        {
            if (!(obj is BhQtcNamKinhPhiKhacChiTietModel temp)) return true;
            bool result = true;
            BhQtcNamKinhPhiKhacChiTietModel item = obj as BhQtcNamKinhPhiKhacChiTietModel;
            result = VoucherDetailFilter(item);
            if (!result && item.IsHangCha)
            {
                result = _xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = DataSearch.Any(x => x.IID_MucLucNganSach.Equals(item.IID_MucLucNganSach));
            }
            return result;
        }

        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            BhQtcNamKinhPhiKhacChiTietModel item = obj as BhQtcNamKinhPhiKhacChiTietModel;
            if (TypeShowsSelected != null)
            {
                if (TypeShowsSelected.ValueItem == TypeDisplay.CO_SO_LIEU)
                    result = result && item.IsHasData && !item.BHangCha;
                else if (TypeShowsSelected.ValueItem == TypeDisplay.CHUA_CO_SO_LIEU)
                    result = result && !item.IsHasData && !item.BHangCha;
            }
            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIdMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;
        }

        private void CalculateData()
        {
            try
            {
                Items.Where(x => x.IsHangCha)
                 .ForAll(x =>
                 {
                     x.FTien_DuToanNamTruocChuyenSang = 0;
                     // x.FTien_DuToanGiaoNamNay = 0;
                     x.FTien_TongDuToanDuocGiao = 0;
                     x.FTien_ThucChi = 0;
                     x.FTienThieu = 0;
                     x.FTienThua = 0;
                     x.STenDonVi = string.Empty;
                 });

                Items.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi) && x.IsHangCha).Select(x =>
                {
                    x.FTien_DuToanNamTruocChuyenSang += x.FDuToanNamTruocChuyenSang;
                    return x;
                }).ToList();

                List<BhQtcNamKinhPhiKhacChiTietModel> temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
                //var dictByMlns = Items.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
                foreach (BhQtcNamKinhPhiKhacChiTietModel item in temp)
                {
                    CalculateParent(item, item);
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
            List<BhQtcNamKinhPhiKhacChiTietModel> roots = Items.Where(t => t.SXauNoiMa.Length < 8).ToList();
            foreach (BhQtcNamKinhPhiKhacChiTietModel item in roots)
            {
                Model.FTongTien_DuToanNamTruocChuyenSang += item.FTien_DuToanNamTruocChuyenSang;
                Model.FTongTien_DuToanGiaoNamNay += item.FTien_DuToanGiaoNamNay;
                Model.FTongTien_TongDuToanDuocGiao += item.FTien_TongDuToanDuocGiao;
                Model.FTongTien_ThucChi += item.FTien_ThucChi;
                Model.FTongTienThua += item.FTienThua;
                Model.FTongTienThieu += item.FTienThieu;
                Model.FTiLeThucHienTrenDuToan += item.FTiLeThucHienTrenDuToan;
            }
        }

        private void CalculateParent(BhQtcNamKinhPhiKhacChiTietModel item, BhQtcNamKinhPhiKhacChiTietModel selfItem)
        {
            BhQtcNamKinhPhiKhacChiTietModel parentItem = Items.Where(x => x.IID_MucLucNganSach != null && item.IdParent != null && x.IID_MucLucNganSach == item.IdParent).FirstOrDefault();
            if (parentItem is null) { return; }
            parentItem.FTien_DuToanNamTruocChuyenSang = (parentItem.FTien_DuToanNamTruocChuyenSang ?? 0) + (selfItem.FTien_DuToanNamTruocChuyenSang ?? 0);
            //parentItem.FTien_DuToanGiaoNamNay += selfItem.FTien_DuToanGiaoNamNay;
            parentItem.FTien_TongDuToanDuocGiao += selfItem.FTien_TongDuToanDuocGiao;
            parentItem.FTien_ThucChi += selfItem.FTien_ThucChi;

            CalculateParent(parentItem, selfItem);
        }

        private void SearchDataParent()
        {
            if (_itemsView != null)
            {
                _itemsView.Refresh();
            }
        }
        private void BeforeRefresh()
        {
            _filterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            _xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }
        protected override void OnRefresh()
        {
            IsInit = true;
            LoadData();
            IsInit = false;
        }
        #endregion

        #region Print
        private void OnPrintDetal(object obj)
        {
            try
            {
                if (!_sessionService.Current.IsQuanLyDonViCha)
                {
                    MessageBoxHelper.Warning(Resources.AlertRolePrintReportAllocation);
                    return;
                }

                int dialogType = (int)obj;

                switch (dialogType)
                {
                    case (int)SettlementTypePrint.PRINT_SETTLEMENT_ADDENDUM:
                    case (int)SettlementTypePrint.PRINT_SETTLEMENT_PALN:
                        PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel.SettlementTypeValue = dialogType;
                        PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel.Init();
                        PrintQuyetToanNamChiKinhPhiKhacNotice view2 = new PrintQuyetToanNamChiKinhPhiKhacNotice
                        {
                            DataContext = PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel
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

        #region OnSave
        public override void OnSave()
        {
            try
            {
                if (!IsSaveData)
                {
                    return;
                }

                Func<BhQtcNamKinhPhiKhacChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.Id.IsNullOrEmpty() && !x.IsHangCha;
                Func<BhQtcNamKinhPhiKhacChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.Id.IsNullOrEmpty() && !x.IsHangCha;

                List<BhQtcNamKinhPhiKhacChiTietModel> detailsAdd = Items.Where(isAdd).ToList();
                List<BhQtcNamKinhPhiKhacChiTietModel> detailsUpdate = Items.Where(isUpdate).ToList();

                //thêm mới chứng từ chi tiết
                if (detailsAdd.Count > 0)
                {
                    List<BhQtcNamKinhPhiKhacChiTiet> addItems = new List<BhQtcNamKinhPhiKhacChiTiet>();
                    detailsAdd.ForEach(x =>
                    {
                        x.IIdMaDonVi = Model.IID_MaDonVi;
                        x.INamLamViec = _sessionInfo.YearOfWork;
                        x.DNgayTao = DateTime.Now;
                        x.SNguoiTao = _sessionInfo.Principal;
                    });
                    _mapper.Map(detailsAdd, addItems);
                    _kinhPhiKhacChiTietService.AddRange(addItems);

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
                    foreach (BhQtcNamKinhPhiKhacChiTietModel updateItem in detailsUpdate)
                    {
                        BhQtcNamKinhPhiKhacChiTiet chungTuChiTiet = _kinhPhiKhacChiTietService.FindById(updateItem.Id);
                        updateItem.IIdMaDonVi = Model.IID_MaDonVi;
                        updateItem.SNguoiSua = _sessionInfo.Principal;
                        updateItem.DNgaySua = DateTime.Now;
                        _mapper.Map(updateItem, chungTuChiTiet);
                        chungTuChiTiet.DNgaySua = DateTime.Now;
                        chungTuChiTiet.SNguoiSua = _sessionInfo.Principal;
                        _kinhPhiKhacChiTietService.Update(chungTuChiTiet);
                        updateItem.IsModified = false;
                    }
                }

                //cập nhật tổng cộng chứng từ
                if (detailsAdd.Count > 0 || detailsUpdate.Count > 0)
                {
                    BhQtcNamKinhPhiKhac chungTuChiTiet = _kinhPhiKhacService.FindById(Model.Id);
                    chungTuChiTiet.FTongTien_DuToanNamTruocChuyenSang = Model.FTongTien_DuToanNamTruocChuyenSang;
                    chungTuChiTiet.FTongTien_DuToanGiaoNamNay = Model.FTongTien_DuToanGiaoNamNay;
                    chungTuChiTiet.FTongTien_TongDuToanDuocGiao = Model.FTongTien_TongDuToanDuocGiao;
                    chungTuChiTiet.FTongTienThua = Model.FTongTienThua;
                    chungTuChiTiet.FTongTien_ThucChi = Model.FTongTien_ThucChi;
                    chungTuChiTiet.FTongTienThieu = Model.FTongTienThieu;
                    chungTuChiTiet.FTiLeThucHienTrenDuToan = Model.FTiLeThucHienTrenDuToan;
                    chungTuChiTiet.DNgaySua = DateTime.Now;
                    chungTuChiTiet.SNguoiSua = _sessionInfo.Principal;
                    _kinhPhiKhacService.Update(chungTuChiTiet);
                    OnRefresh();
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IsDeleteAll));
                    UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
                    string message = Resources.MsgSaveDone;
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

        private void OnSearch(object obj)
        {
            SearchTextFilter();
        }

        private void OnClearSearch(object obj)
        {
            SNoiDungSearch = string.Empty;
            if (!(obj is bool temp))
            {
                _itemsView.Refresh();
            }
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhQtcNamKinhPhiKhacChiTietModel> results = new List<BhQtcNamKinhPhiKhacChiTietModel>();

                List<string> lstSXaNoiMaChildSearch = Items.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                List<string> lstSXaNoiMaParentSearch = Items.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                if (!lstSXaNoiMaChildSearch.IsEmpty())
                {
                    lstParents.AddRange(StringUtils.GetListKyHieuParent(lstSXaNoiMaChildSearch));
                    if (lstParents.Any(x => x.Count() >= 3))
                    {
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
                    }
                    results = Items.Where(x => lstParents.Contains(x.SXauNoiMa)).ToList();
                }
                if (!lstSXaNoiMaParentSearch.IsEmpty())
                {
                    if (results.IsEmpty())
                        results = GetDataParent(lstSXaNoiMaParentSearch);
                    else
                        results.AddRange(GetDataParent(lstSXaNoiMaParentSearch.Where(x => !lstParents.Contains(x)).ToList()));
                }
                DataSearch = new ObservableCollection<BhQtcNamKinhPhiKhacChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhQtcNamKinhPhiKhacChiTietModel>();
            }
            _itemsView.Refresh();
        }

        private List<BhQtcNamKinhPhiKhacChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhQtcNamKinhPhiKhacChiTietModel> result = new List<BhQtcNamKinhPhiKhacChiTietModel>();
            List<string> lstParent = StringUtils.GetListKyHieuParent(lstInput);
            if (!lstParent.IsEmpty() && lstParent.Any(x => x.Count() >= 3))
            {
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
            }
            List<BhQtcNamKinhPhiKhacChiTietModel> lstData = Items.Where(x => lstParent.Contains(x.SXauNoiMa)).ToList();
            result.AddRange(lstData);
            GetListChild(lstData.Where(x => lstInput.Contains(x.SXauNoiMa)).ToList(), result);
            return result;
        }

        private void GetListChild(List<BhQtcNamKinhPhiKhacChiTietModel> lstInput, List<BhQtcNamKinhPhiKhacChiTietModel> results)
        {
            List<BhQtcNamKinhPhiKhacChiTietModel> itemChild = Items.Where(x => lstInput.Select(x => x.IID_MucLucNganSach).Distinct().Contains(x.IdParent)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (BhQtcNamKinhPhiKhacChiTietModel item in itemChild.Where(x => Items.Select(y => y.IdParent).Distinct().Contains(x.IID_MucLucNganSach ?? Guid.Empty)))
                {
                    GetListChild(new List<BhQtcNamKinhPhiKhacChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

    }
}
