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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.Explanation;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.Explanation;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac
{
    public class QuyetToanChiQuyKinhPhiKhacDetailViewModel : DetailViewModelBase<BhQtcQuyKPKModel, BhQtcQuyKPKChiTietModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly IBhQtcQuyKPKService _quyKCBService;
        private readonly IBhQtcQuyKPKChiTietService _quyKCBChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private ICollectionView ItemsView;
        private string xnmConcatenation = "";
        #endregion

        #region Property
        private SessionInfo _sessionInfo;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public bool IsAggregate => !string.IsNullOrEmpty(Model.STongHop);
        public bool IsVoucherSummary => !string.IsNullOrEmpty(Model.STongHop) ? true : false;
        public bool IsReadOnlyNamTruocChuyenSang => Model.IQuyChungTu > SettlementTypeQuy.Quy;
        public bool IsShowAgencyFilter => IsAggregate && _selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI;
        private ICollection<BhQtcQuyKPKChiTietModel> _filterResult = new HashSet<BhQtcQuyKPKChiTietModel>();
        public bool IsOpenPrintPopup = true;
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
                    if (_selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
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

        List<BhQtcQuyKPKChiTietQuery> _listChungTuChiTietTheoQuy;
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted)
                    || Items.Any(x => !x.IsHangCha);
        public bool IsInit { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public override Type ContentType => typeof(QuyetToanChiQuyKinhPhiKhacDetail);

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private ObservableCollection<BhQtcQuyKPKChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhQtcQuyKPKChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhQtcQuyKPKChiTietModel _selectedPopupItem;
        public BhQtcQuyKPKChiTietModel SelectedPopupItem
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
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private ObservableCollection<BhQtcQuyKPKChiTietModel> _dataSearch;
        public ObservableCollection<BhQtcQuyKPKChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }
        #endregion

        #region RelayCommand
        public RelayCommand RefreshCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand GiaiThichBangLoiCommand { get; }
        #endregion

        #region View model
        public PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel { get; set; }
        public QuyetToanChiGiaiThichBangLoiQuyKhacViewModel QuyetToanChiGiaiThichBangLoiQuyKhacViewModel { get; set; }
        #endregion

        #region Constructor
        public QuyetToanChiQuyKinhPhiKhacDetailViewModel(
                IMapper mapper,
                ISessionService sessionService,
                ILog loger,
                INsDonViService nsDonViService,
                INsNguoiDungDonViService iNguoiDungDonViService,
                IBhQtcQuyKPKService quyKCBService,
                IBhQtcQuyKPKChiTietService quyKCBChiTietService,
                IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
                PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel printQuyetToanChiQuyKinhPhiKhacNoticeViewModel,
                QuyetToanChiGiaiThichBangLoiQuyKhacViewModel quyetToanChiGiaiThichBangLoiQuyKhacViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = loger;
            _nsDonViService = nsDonViService;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _quyKCBService = quyKCBService;
            _quyKCBChiTietService = quyKCBChiTietService;

            SaveCommand = new RelayCommand(obj => OnSave());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            PrintReportCommand = new RelayCommand(OnPrintDetal);
            RefreshCommand = new RelayCommand(obj => Init());
            GiaiThichBangLoiCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());
            PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel = printQuyetToanChiQuyKinhPhiKhacNoticeViewModel;
            QuyetToanChiGiaiThichBangLoiQuyKhacViewModel = quyetToanChiGiaiThichBangLoiQuyKhacViewModel;
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SearchCommand = new RelayCommand(OnSearch);
        }

        private void OnOpenVerbalExplanationDialog()
        {
            QuyetToanChiGiaiThichBangLoiQuyKhacViewModel.BhQtcQuyKPKModel = Model;
            QuyetToanChiGiaiThichBangLoiQuyKhacViewModel.Init();
            var view = new VerbalExplanation { DataContext = QuyetToanChiGiaiThichBangLoiQuyKhacViewModel };
            view.ShowDialog();
        }
        #endregion

        #region Init
        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _isShowColumnUnit = false;
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                //IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            if (!string.IsNullOrEmpty(Model.STongHop))
            {
                LoadComboboxTypeShow();
            }
            LoadTypeShow();
            SearchText = string.Empty;
            _selectedAgency = null;
            IsInit = true;
            LoadData();
            IsInit = false;
            LoadDefault();
        }
        #endregion

        #region close
        public override void OnClose(object obj)
        {
            ((Window)obj).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }
        #endregion

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

        #region Load data

        private void LoadDefault()
        {
            SNoiDungSearch = string.Empty;
            DataSearch = new ObservableCollection<BhQtcQuyKPKChiTietModel>();
        }

        private void LoadAgencies(string agencyIds)
        {
            var listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            OnPropertyChanged(nameof(Agencies));
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            try
            {
                var iNamLamViec = _sessionInfo.YearOfWork;
                var danhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(iNamLamViec).ToList();
                var sLNS = danhMucLoaiChi.Where(x => x.Id.Equals(Model.IID_LoaiChi)).Select(x => x.SLNS).FirstOrDefault();
                DonVi donViParent = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, iNamLamViec);
                QtcQuyKCBCriteria searchCondition = new QtcQuyKCBCriteria();
                List<BhQtcQuyKPKChiTietQuery> _listChungTuChiTiet = new List<BhQtcQuyKPKChiTietQuery>();
                searchCondition.NamLamViec = iNamLamViec;
                searchCondition.IDMaDonVi = Model.IID_MaDonVi;
                searchCondition.IDDonVi = Model.IID_DonVi;
                searchCondition.SNguoiTao = Model.SNguoiTao;
                searchCondition.ID = Model.Id;
                searchCondition.SLNS = Model.SDSLNS;
                searchCondition.IDLoaiChi = Model.IID_LoaiChi;
                searchCondition.DNgayChungTu = Model.DNgayChungTu;
                searchCondition.IQuyChungTu = Model.IQuyChungTu;
                var existChungTuChiTiet = _quyKCBChiTietService.ExitChungTuChiTiet(searchCondition);

                if (IsAggregate && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI && _selectedAgency == null)
                {
                    var voucherNos = Model.STongHop.Split(",").ToList();
                    List<BhQtcQuyKPK> listChungTu = _quyKCBService.FindByAggregateVoucher(voucherNos, _sessionInfo.YearOfWork).ToList();
                    string agencyIds = string.Join(",", listChungTu.Select(x => x.IID_MaDonVi));
                    var listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
                    List<BhQtcQuyKPKChiTietQuery> listChungTuChiTietParent = new List<BhQtcQuyKPKChiTietQuery>();
                    List<BhQtcQuyKPKChiTietQuery> listChungTuChiTietChildren = new List<BhQtcQuyKPKChiTietQuery>();

                    foreach (var chungTu in listChungTu)
                    {
                        searchCondition.LoaiChungTu = chungTu.IID_MaDonVi == donViParent.IIDMaDonVi ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;
                        searchCondition.ID = chungTu.Id;
                        searchCondition.IDMaDonVi = chungTu.IID_MaDonVi;
                        List<BhQtcQuyKPKChiTietQuery> listQuery = _quyKCBChiTietService.FindChungTuChiTiet(searchCondition).ToList();

                        listQuery.Where(x => !x.BHangCha).Select(x => x.STenDonVi = listDonVi.FirstOrDefault(y => y.IIDMaDonVi == chungTu.IID_MaDonVi).TenDonVi).ToList();
                        listChungTuChiTietParent.AddRange(listQuery.Where(x => x.IsHangCha));
                        listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.IsHangCha));
                    }
                    var listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                    var temp = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x =>
                        new
                        {
                            Data = x.FirstOrDefault(),
                            FTien_DuToanGiaoNamNay = x.Sum(x => x.FTien_DuToanGiaoNamNay)
                        }
                    ).ToList();
                    temp.ForEach(x =>
                    {
                        x.Data.FTien_DuToanGiaoNamNay = x.FTien_DuToanGiaoNamNay;
                    });
                    listChungTuChiTietParent = temp.Select(x => x.Data).ToList();
                    _listChungTuChiTiet.AddRange(listChungTuChiTietParent);
                    _listChungTuChiTiet.AddRange(listChungTuChiTietChildren);
                    _listChungTuChiTiet = _listChungTuChiTiet.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IID_MaDonVi).ToList();


                    _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
                    OnPropertyChanged(nameof(Agencies));
                }
                else
                {
                    if (_selectedAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
                    {
                        searchCondition.IDMaDonVi = _selectedAgency.ValueItem;
                        var predicateCtDv = PredicateBuilder.True<BhQtcQuyKPK>();
                        predicateCtDv = predicateCtDv.And(x => x.IID_MaDonVi == _selectedAgency.ValueItem);
                        predicateCtDv = predicateCtDv.And(x => x.IQuyChungTu == Model.IQuyChungTu);
                        predicateCtDv = predicateCtDv.And(x => x.INamChungTu == Model.INamChungTu);
                        predicateCtDv = predicateCtDv.And(x => x.IID_LoaiChi == Model.IID_LoaiChi);
                        var ctDonVi = _quyKCBService.FindByCondition(predicateCtDv).FirstOrDefault();
                        if (ctDonVi != null)
                        {
                            searchCondition.ID = ctDonVi.Id;
                        }
                    }

                    searchCondition.LoaiChungTu = searchCondition.IDMaDonVi == donViParent.IIDMaDonVi ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;
                    _listChungTuChiTiet = _quyKCBChiTietService.FindChungTuChiTiet(searchCondition).ToList();
                }

                _listChungTuChiTiet.ForEach(t =>
                {
                    if (t.IsHangCha)
                    {
                        t.STenDonVi = string.Empty;
                    }
                });

                Items = _mapper.Map<ObservableCollection<BhQtcQuyKPKChiTietModel>>(_listChungTuChiTiet);
                DataPopupSearchItems = _mapper.Map<ObservableCollection<BhQtcQuyKPKChiTietModel>>(_listChungTuChiTiet);
                ItemsView = CollectionViewSource.GetDefaultView(Items);
                ItemsView.Filter = ItemsViewFilter;

                foreach (var chungTu in Items)
                {
                    chungTu.IsFilter = true;
                    if (!chungTu.IsHangCha)
                    {
                        chungTu.PropertyChanged += (sender, args) =>
                        {
                            BhQtcQuyKPKChiTietModel item = (BhQtcQuyKPKChiTietModel)sender;
                            if (args.PropertyName.ToLower().StartsWith("f") || args.PropertyName.Equals(nameof(BhQtcQuyKPKChiTietModel.SGhiChu)))
                            {
                                //item.FTienXacNhanQuyetToanQuyNay = item.FTienDeNghiQuyetToanQuyNay;
                                item.IsModified = true;
                                chungTu.IsModified = true;
                            }
                            CalculateData();
                            OnPropertyChanged(nameof(IsSaveData));
                            OnPropertyChanged(nameof(IsOpenPrintPopup));

                        };
                    }
                }

                CalculateData();
                OnPropertyChanged(nameof(IsReadOnlyNamTruocChuyenSang));
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
                     x.FTienDeNghiQuyetToanQuyNay = 0;
                     x.FTienXacNhanQuyetToanQuyNay = 0;
                     x.FTienQuyetToanDaDuyet = 0;
                     x.FTienThucChi = 0;
                     //x.FTien_DuToanNamTruocChuyenSang = 0;
                     //x.FTien_DuToanGiaoNamNay = 0;
                     x.FTien_TongDuToanDuocGiao = 0;

                 });

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

        private void CalculateParent(Guid idParent, BhQtcQuyKPKChiTietModel item, Dictionary<Guid?, BhQtcQuyKPKChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            //model.FTien_DuToanNamTruocChuyenSang += item.FTien_DuToanNamTruocChuyenSang;
            //model.FTien_DuToanGiaoNamNay += item.FTien_DuToanGiaoNamNay;
            model.FTien_TongDuToanDuocGiao += item.FTien_TongDuToanDuocGiao;
            model.FTienThucChi = item.FTienThucChi;
            model.FTienQuyetToanDaDuyet += item.FTienQuyetToanDaDuyet;
            model.FTienXacNhanQuyetToanQuyNay += item.FTienXacNhanQuyetToanQuyNay;
            model.FTienDeNghiQuyetToanQuyNay += item.FTienDeNghiQuyetToanQuyNay;

            CalculateParent(model.IdParent, item, dictByMlns);
        }

        private void UpdateTotal()
        {
            Model.FTongTien_DuToanNamTruocChuyenSang = Items?.Where(x => x.SXauNoiMa == LNSValue.LNS_9010006_9010007).Select(x => x.FTien_DuToanNamTruocChuyenSang).FirstOrDefault(); ;
            Model.FTongTien_DuToanGiaoNamNay = Items?.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTien_DuToanGiaoNamNay).FirstOrDefault();
            Model.FTongTien_TongDuToanDuocGiao = 0;
            Model.FTongTienThucChi = 0;
            Model.FTongTienQuyetToanDaDuyet = 0;
            Model.FTongTienDeNghiQuyetToanQuyNay = 0;
            Model.FTongTienXacNhanQuyetToanQuyNay = 0;

            var roots = Items.Where(t => string.IsNullOrEmpty(t.SM)).ToList();
            foreach (var item in roots)
            {
                //Model.FTongTien_DuToanNamTruocChuyenSang += item.FTien_DuToanNamTruocChuyenSang;
                //Model.FTongTien_DuToanGiaoNamNay += item.FTien_DuToanGiaoNamNay;
                Model.FTongTien_TongDuToanDuocGiao += item.FTien_TongDuToanDuocGiao;
                Model.FTongTienThucChi += item.FTienThucChi;
                Model.FTongTienQuyetToanDaDuyet += item.FTienQuyetToanDaDuyet;
                Model.FTongTienDeNghiQuyetToanQuyNay += item.FTienDeNghiQuyetToanQuyNay;
                Model.FTongTienXacNhanQuyetToanQuyNay += item.FTienXacNhanQuyetToanQuyNay;
            }
        }

        private void SearchDataParent()
        {
            if (ItemsView != null)
            {
                ItemsView.Refresh();
            }
        }

        private bool ItemsViewFilter(object obj)
        {
            //if (!(obj is BhQtcQuyKPKChiTietModel temp)) return true;
            //bool result = true;
            //var item = (BhQtcQuyKPKChiTietModel)obj;
            //if (!string.IsNullOrEmpty(SNoiDungSearch))
            //{
            //    result = DataSearch.Any(x => x.IID_MucLucNganSach.Equals(item.IID_MucLucNganSach));
            //}
            //return result;

            bool result = true;
            var item = (BhQtcQuyKPKChiTietModel)obj;
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
            var item = (BhQtcQuyKPKChiTietModel)obj;
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

        protected override void OnRefresh()
        {
            IsInit = true;
            LoadData();
            IsInit = false;
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }
        #endregion

        #region Print
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
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS:
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_AGENCY:
                    case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:
                        PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel.SettlementTypeValue = dialogType;
                        PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel.Init();
                        var view1 = new PrintQuyetToanChiQuyKinhPhiKhacNotice
                        {
                            DataContext = PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel
                        };
                        DialogHost.Show(view1, SystemConstants.DETAIL_DIALOG, null, null);
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

                Func<BhQtcQuyKPKChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.Id.IsNullOrEmpty() && !x.IsHangCha;
                Func<BhQtcQuyKPKChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.Id.IsNullOrEmpty() && !x.IsHangCha;

                var detailsAdd = Items.Where(isAdd).ToList();
                var lstDataAdDuToan = Items.Where(x => x.Id == Guid.Empty && x.IsModified && !string.IsNullOrEmpty(x.SDuToanChiTietToi));
                detailsAdd.AddRange(lstDataAdDuToan);
                var detailsUpdate = Items.Where(isUpdate).ToList();
                if (Model.IQuyChungTu > SettlementTypeQuy.Quy)
                {
                    var lstDataUpdateDuToan = Items.Where(x => x.IsModified && !string.IsNullOrEmpty(x.SDuToanChiTietToi));
                    detailsUpdate.AddRange(lstDataUpdateDuToan);
                }

                //thêm mới chứng từ chi tiết
                if (detailsAdd.Count > 0)
                {
                    var addItems = new List<BhQtcQuyKPKChiTiet>();
                    _mapper.Map(detailsAdd, addItems);
                    _quyKCBChiTietService.AddRange(addItems);

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
                        var chungTuChiTiet = _quyKCBChiTietService.FindById(updateItem.Id);
                        _mapper.Map(updateItem, chungTuChiTiet);
                        _quyKCBChiTietService.Update(chungTuChiTiet);
                        updateItem.IsModified = false;
                    }
                }

                //cập nhật tổng cộng chứng từ
                if (detailsAdd.Count > 0 || detailsUpdate.Count > 0)
                {
                    // To Do
                    var chungTuChiTiet = _quyKCBService.FindById(Model.Id);
                    chungTuChiTiet.FTongTien_DuToanNamTruocChuyenSang = Model.FTongTien_DuToanNamTruocChuyenSang;
                    chungTuChiTiet.FTongTien_DuToanGiaoNamNay = Model.FTongTien_DuToanGiaoNamNay;
                    chungTuChiTiet.FTongTienThucChi = Model.FTongTienThucChi;
                    chungTuChiTiet.FTongTien_TongDuToanDuocGiao = Model.FTongTien_TongDuToanDuocGiao;
                    chungTuChiTiet.FTongTienQuyetToanDaDuyet = Model.FTongTienQuyetToanDaDuyet;
                    chungTuChiTiet.FTongTienXacNhanQuyetToanQuyNay = Model.FTongTienXacNhanQuyetToanQuyNay;
                    chungTuChiTiet.FTongTienDeNghiQuyetToanQuyNay = Model.FTongTienDeNghiQuyetToanQuyNay;
                    _quyKCBService.Update(chungTuChiTiet);
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
                ItemsView.Refresh();
            }
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhQtcQuyKPKChiTietModel> results = new List<BhQtcQuyKPKChiTietModel>();

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
                DataSearch = new ObservableCollection<BhQtcQuyKPKChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhQtcQuyKPKChiTietModel>();
            }
            ItemsView.Refresh();
        }

        private List<BhQtcQuyKPKChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhQtcQuyKPKChiTietModel> result = new List<BhQtcQuyKPKChiTietModel>();
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

        private void GetListChild(List<BhQtcQuyKPKChiTietModel> lstInput, List<BhQtcQuyKPKChiTietModel> results)
        {
            var itemChild = Items.Where(x => lstInput.Select(x => x.IID_MucLucNganSach).Distinct().Contains(x.IdParent)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => Items.Select(y => y.IdParent).Distinct().Contains(x.IID_MucLucNganSach ?? Guid.Empty)))
                {
                    GetListChild(new List<BhQtcQuyKPKChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

    }
}
