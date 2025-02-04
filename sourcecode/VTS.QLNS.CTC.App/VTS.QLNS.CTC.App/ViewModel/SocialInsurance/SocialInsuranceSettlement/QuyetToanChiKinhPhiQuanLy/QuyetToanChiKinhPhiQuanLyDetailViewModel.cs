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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.Explanation;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.Explanation;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy
{
    public class QuyetToanChiKinhPhiQuanLyDetailViewModel : DetailViewModelBase<BhQtcQuyKinhPhiQuanLyModel, BhQtcQuyKinhPhiQuanLyChiTietModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly IBhQtcQuyKinhPhiQuanLyService _bhQtcQKPQuanLyService;
        private readonly IBhQtcQuyKinhPhiQuanLyChiTietService _bhQtcQKPQuanLyChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private ICollectionView ItemsView;
        #endregion

        #region Property
        private SessionInfo _sessionInfo;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public bool IsOpenPrintPopup = true;
        private string _xnmConcatenation = "";
        private ICollection<BhQtcQuyKinhPhiQuanLyChiTietModel> _filterResult = new HashSet<BhQtcQuyKinhPhiQuanLyChiTietModel>();
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

        public bool IsShowAgencyFilter => IsAggregate && _selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI;

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

        public bool IsAggregate => !string.IsNullOrEmpty(Model.STongHop);
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
        List<BhQtcQuyKinhPhiQuanLyChiTietQuery> _listChungTuChiTiet;
        List<BhQtcQuyKinhPhiQuanLyChiTietQuery> _listChungTuChiTietTheoQuy;
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted)
                    || Items.Any(x => !x.IsHangCha);
        public bool IsInit { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public override Type ContentType => typeof(QuyetToanChiKinhPhiQuanLyDetail);

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

        private ObservableCollection<BhQtcQuyKinhPhiQuanLyChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhQtcQuyKinhPhiQuanLyChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhQtcQuyKinhPhiQuanLyChiTietModel _selectedPopupItem;
        public BhQtcQuyKinhPhiQuanLyChiTietModel SelectedPopupItem
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

        public bool IsVoucherSummary => !string.IsNullOrEmpty(Model.STongHop) ? true : false;

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

        private ObservableCollection<BhQtcQuyKinhPhiQuanLyChiTietModel> _dataSearch;
        public ObservableCollection<BhQtcQuyKinhPhiQuanLyChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }

        #endregion

        #region RelayCommand
        public RelayCommand RefreshCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand GiaiThichBangLoiCommand { get; }
        #endregion

        #region View model
        public PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel { get; set; }
        public QuyetToanChiGiaiThichBangLoiQuyKPQLViewModel QuyetToanChiGiaiThichBangLoiQuyKPQLViewModel { get; set; }
        #endregion

        #region Constructor
        public QuyetToanChiKinhPhiQuanLyDetailViewModel(
                IMapper mapper,
                ISessionService sessionService,
                ILog loger,
                INsDonViService nsDonViService,
                INsNguoiDungDonViService iNguoiDungDonViService,
                IBhQtcQuyKinhPhiQuanLyService bhQtcQuyKinhPhiQuanLyService,
                IBhQtcQuyKinhPhiQuanLyChiTietService bhQtcQuyKinhPhiQuanLyChiTietService,
                IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
                PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel printQuyetToanChiKinhPhiQuanLyNoticeViewModel,
                QuyetToanChiGiaiThichBangLoiQuyKPQLViewModel toanThuGiaiThichBangLoiQuyKPQLViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = loger;
            _nsDonViService = nsDonViService;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _bhQtcQKPQuanLyService = bhQtcQuyKinhPhiQuanLyService;
            _bhQtcQKPQuanLyChiTietService = bhQtcQuyKinhPhiQuanLyChiTietService;

            SaveCommand = new RelayCommand(obj => OnSave());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            PrintReportCommand = new RelayCommand(obj => OnPrintDetal(obj));
            RefreshCommand = new RelayCommand(obj => Init());
            //SearchCommandParent = new RelayCommand(obj => SearchDataParent());
            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            GiaiThichBangLoiCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());

            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            QuyetToanChiGiaiThichBangLoiQuyKPQLViewModel = toanThuGiaiThichBangLoiQuyKPQLViewModel;
            PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel = printQuyetToanChiKinhPhiQuanLyNoticeViewModel;
        }

        private void OnOpenVerbalExplanationDialog()
        {
            QuyetToanChiGiaiThichBangLoiQuyKPQLViewModel.BhQtcQuyKinhPhiQuanLyModel = Model;
            QuyetToanChiGiaiThichBangLoiQuyKPQLViewModel.Init();
            var view = new VerbalExplanation { DataContext = QuyetToanChiGiaiThichBangLoiQuyKPQLViewModel };
            view.ShowDialog();
        }
        #endregion

        #region Init
        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadComboboxTypeShow();
            LoadTypeShow();
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                //IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            SearchText = string.Empty;
            IsInit = true;
            _selectedAgency = null;
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
        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            try
            {
                var iNamLamViec = _sessionInfo.YearOfWork;
                var danhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(iNamLamViec).ToList();
                var iDDmLoaiChi = danhMucLoaiChi.Where(x => x.SLNS.Equals(SettlementTypeSLNS.SLNS)).Select(x => x.Id).FirstOrDefault();
                DonVi donViParent = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, iNamLamViec);
                bool existChungTuChiTiet = false;
                if (IsAggregate && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI && _selectedAgency == null)
                {
                    var voucherNos = Model.STongHop.Split(",").ToList();
                    List<BhQtcQuyKinhPhiQuanLy> listChungTu = _bhQtcQKPQuanLyService.FindByCondition(x => x.INamChungTu == _sessionInfo.YearOfWork && voucherNos.Contains(x.SSoChungTu)).ToList();
                    string agencyIds = string.Join(",", listChungTu.Select(x => x.IID_MaDonVi));
                    var listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);

                    var listChungTuChiTietParent = new List<BhQtcQuyKinhPhiQuanLyChiTietQuery>();
                    var listChungTuChiTietChildren = new List<BhQtcQuyKinhPhiQuanLyChiTietQuery>();
                    var temp = new List<BhQtcQuyKinhPhiQuanLyChiTietQuery>();
                    foreach (var chungTu in listChungTu)
                    {
                        QtcQuyKinhPhiQuanLyCriteria searchCondition = new QtcQuyKinhPhiQuanLyCriteria();
                        searchCondition.NamLamViec = iNamLamViec;
                        searchCondition.IDMaDonVi = chungTu.IID_MaDonVi;
                        searchCondition.IDDonVi = chungTu.IID_DonVi;
                        searchCondition.SNguoiTao = chungTu.SNguoiTao;
                        searchCondition.LoaiChungTu = chungTu.IID_MaDonVi == donViParent.IIDMaDonVi ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;
                        searchCondition.ID = chungTu.Id;
                        searchCondition.SLNS = SettlementTypeSLNS.SLNS;
                        searchCondition.IDLoaiChi = iDDmLoaiChi;
                        searchCondition.DNgayChungTu = DateTime.Now;
                        searchCondition.IQuyChungTu = chungTu.IQuyChungTu;
                        var listQuery = _bhQtcQKPQuanLyChiTietService.FindChungTuChiTiet(searchCondition).ToList();
                        listQuery.Where(x => !x.IsHangCha).Select(x => x.STenDonVi == listDonVi.FirstOrDefault(x => x.IIDMaDonVi == chungTu.IID_MaDonVi).TenDonVi).ToList();

                        listChungTuChiTietParent.AddRange(listQuery.Where(x => x.IsHangCha));
                        listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.IsHangCha));
                    }
                    var listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                    var lstChungTu = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x =>
                        new
                        {
                            Data = x.FirstOrDefault(),
                            FTienDuToanDuocGiao = x.Sum(x => x.FTienDuToanDuocGiao)
                        }
                    ).ToList();

                    lstChungTu.ForEach(x =>
                    {
                        x.Data.FTienDuToanDuocGiao = x.FTienDuToanDuocGiao;
                    });

                    listChungTuChiTietParent = lstChungTu.Select(x => x.Data).ToList();
                    temp.AddRange(listChungTuChiTietParent);
                    temp.AddRange(listChungTuChiTietChildren);
                    temp = temp.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.STenDonVi).ToList();
                    _listChungTuChiTiet = temp;

                    //LoadAgencies(agencyIds);
                    _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
                    OnPropertyChanged(nameof(Agencies));
                }
                else
                {

                    QtcQuyKinhPhiQuanLyCriteria searchCondition = new QtcQuyKinhPhiQuanLyCriteria();
                    searchCondition.NamLamViec = iNamLamViec;
                    searchCondition.IDMaDonVi = Model.IID_MaDonVi;
                    searchCondition.IDDonVi = Model.IID_DonVi;
                    searchCondition.SNguoiTao = Model.SNguoiTao;
                    searchCondition.LoaiChungTu = Model.IID_MaDonVi == donViParent.IIDMaDonVi ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;
                    searchCondition.ID = Model.Id;
                    searchCondition.SLNS = SettlementTypeSLNS.SLNS;
                    searchCondition.IDLoaiChi = iDDmLoaiChi;
                    searchCondition.DNgayChungTu = DateTime.Now;
                    searchCondition.IQuyChungTu = Model.IQuyChungTu;

                    if (_selectedAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
                    {
                        searchCondition.IDMaDonVi = _selectedAgency.ValueItem;
                        searchCondition.LoaiChungTu = _selectedAgency.ValueItem == donViParent.IIDMaDonVi ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;
                        var predicateCtDv = PredicateBuilder.True<BhQtcQuyKinhPhiQuanLy>();
                        predicateCtDv = predicateCtDv.And(x => x.INamChungTu == Model.INamChungTu);
                        predicateCtDv = predicateCtDv.And(x => x.IID_MaDonVi == _selectedAgency.ValueItem);
                        predicateCtDv = predicateCtDv.And(x => x.IQuyChungTu == Model.IQuyChungTu);
                        var ctDonVi = _bhQtcQKPQuanLyService.FindByCondition(predicateCtDv).FirstOrDefault();
                        if (ctDonVi != null)
                            searchCondition.ID = ctDonVi.Id;
                    }

                    _listChungTuChiTiet = _bhQtcQKPQuanLyChiTietService.FindChungTuChiTiet(searchCondition).ToList();
                }

                Items = _mapper.Map<ObservableCollection<BhQtcQuyKinhPhiQuanLyChiTietModel>>(_listChungTuChiTiet);
                DataPopupSearchItems = _mapper.Map<ObservableCollection<BhQtcQuyKinhPhiQuanLyChiTietModel>>(_listChungTuChiTiet);
                var listData = _listChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.SL) || !string.IsNullOrEmpty(x.SDuToanChiTietToi)).ToList();

                CalculateDataDuToan(listData);

                foreach (var itemdata in listData)
                {
                    foreach (var item in Items)
                    {
                        if (item.SXauNoiMa == itemdata.SXauNoiMa)
                        {
                            item.FTienDuToanDuocGiao = itemdata.FTienDuToanDuocGiao;
                        }
                    }
                }

                foreach (var chungTu in Items)
                {
                    chungTu.IsFilter = true;
                    if (!chungTu.IsHangCha)
                    {
                        chungTu.PropertyChanged += (sender, args) =>
                        {
                            BhQtcQuyKinhPhiQuanLyChiTietModel item = (BhQtcQuyKinhPhiQuanLyChiTietModel)sender;
                            if (args.PropertyName.ToLower().StartsWith("f") || args.PropertyName.Equals(nameof(BhQtcQuyKinhPhiQuanLyChiTietModel.SGhiChu)))
                            {
                                //item.FTienXacNhanQuyetToanQuyNay = item.FTienDeNghiQuyetToanQuyNay ?? 0;
                                item.IsModified = true;
                                chungTu.IsModified = true;
                                CalculateData();
                            }
                            OnPropertyChanged(nameof(IsSaveData));
                            OnPropertyChanged(nameof(IsOpenPrintPopup));

                        };
                    }
                }
                CalculateData();
                ItemsView = CollectionViewSource.GetDefaultView(Items);
                ItemsView.Filter = ItemsViewFilter;
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateDataDuToan(List<BhQtcQuyKinhPhiQuanLyChiTietQuery> listData)
        {
            listData.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    x.FTienDuToanDuocGiao = 0;
                });
            var dictByMlns = listData.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            var temp = listData.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {

                CalculateParentDuToan(item.IdParent, item, dictByMlns);
            }

            UpdateTotalDuToan(listData);
        }

        private void UpdateTotalDuToan(List<BhQtcQuyKinhPhiQuanLyChiTietQuery> listData)
        {
            Model.FTongTienDuToanDuocGiao = 0;
            Model.FTongTienDuToanDuocGiao = listData.Where(x => x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToi).Sum(x => x.FTienDuToanDuocGiao ?? 0);
        }

        private void CalculateParentDuToan(Guid? IdParent, BhQtcQuyKinhPhiQuanLyChiTietQuery item, Dictionary<Guid?, BhQtcQuyKinhPhiQuanLyChiTietQuery> dictByMlns)
        {
            if (IdParent == null || !dictByMlns.ContainsKey(IdParent))
            {
                return;
            }

            var model = dictByMlns[IdParent];
            model.FTienDuToanDuocGiao = (model.FTienDuToanDuocGiao ?? 0) + (item.FTienDuToanDuocGiao ?? 0);
            CalculateParentDuToan(model.IID_MLNS_Cha, item, dictByMlns);
        }

        private bool BHQTCQKPQLModelsFilter(object obj)
        {
            if (!(obj is BhQtcQuyKinhPhiQuanLyChiTietModel temp)) return true;
            var keyword = SearchText?.Trim().ToLower() ?? string.Empty;
            var condition1 = false;
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(temp.SNoiDung))
                    condition1 = condition1 || temp.SNoiDung.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SM))
                    condition1 = condition1 || temp.SM.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.STM))
                    condition1 = condition1 || temp.STM.ToLower().Contains(keyword);
            }
            else
            {
                condition1 = true;
            }

            var result = condition1;
            return result;
        }


        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            _xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            var item = obj as BhQtcQuyKinhPhiQuanLyChiTietModel;

            if (TypeShowsSelected != null)
            {
                if (TypeShowsSelected.ValueItem == TypeDisplay.CO_SO_LIEU)
                    result = result && item.IsHasData && !item.BHangCha;
                else if (TypeShowsSelected.ValueItem == TypeDisplay.CHUA_CO_SO_LIEU)
                    result = result && !item.IsHasDataIsHangCha;
            }

            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIdMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;

        }
        private void SearchDataParent()
        {
            if (ItemsView != null)
            {
                ItemsView.Refresh();
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

                     x.FTienQuyetToanDaDuyet = 0;
                     //x.FTienThucChi = 0;
                     x.FTienXacNhanQuyetToanQuyNay = 0;
                     x.STenDonVi = string.Empty;
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

        private void UpdateTotal()
        {
            Model.FTongTienQuyetToanDaDuyet = 0;
            Model.FTongTienDuToanDuocGiao = Items?.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienDuToanDuocGiao).FirstOrDefault();
            Model.FTongTienDeNghiQuyetToanQuyNay = 0;
            Model.FTongTienThucChi = 0;
            Model.FTongTienXacNhanQuyetToanQuyNay = 0;
            var roots = Items.Where(t => string.IsNullOrEmpty(t.SM) && string.IsNullOrEmpty(t.STM) && !t.SXauNoiMa.Contains("-")).ToList();
            foreach (var item in roots)
            {
                Model.FTongTienQuyetToanDaDuyet += item.FTienQuyetToanDaDuyet;
                //Model.FTongTienDuToanDuocGiao += item.FTienDuToanDuocGiao;
                Model.FTongTienDeNghiQuyetToanQuyNay += item.FTienDeNghiQuyetToanQuyNay;
                Model.FTongTienThucChi += item.FTienThucChi;
                Model.FTongTienXacNhanQuyetToanQuyNay += item.FTienXacNhanQuyetToanQuyNay;
            }
        }

        private void CalculateParent(Guid idParent, BhQtcQuyKinhPhiQuanLyChiTietModel item, Dictionary<Guid?, BhQtcQuyKinhPhiQuanLyChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienXacNhanQuyetToanQuyNay += item.FTienXacNhanQuyetToanQuyNay;
            model.FTienDeNghiQuyetToanQuyNay += item.FTienDeNghiQuyetToanQuyNay;
            //model.FTienThucChi = item.FTienThucChi;
            model.FTienQuyetToanDaDuyet += item.FTienQuyetToanDaDuyet;

            CalculateParent(model.IdParent, item, dictByMlns);
        }

        protected override void OnRefresh()
        {
            IsInit = true;
            LoadData();
            IsInit = false;
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

        #region OnSave
        public override void OnSave()
        {
            try
            {
                if (!IsSaveData)
                {
                    return;
                }

                Func<BhQtcQuyKinhPhiQuanLyChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.Id.IsNullOrEmpty() && !x.IsHangCha;
                Func<BhQtcQuyKinhPhiQuanLyChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.Id.IsNullOrEmpty() && !x.IsHangCha;

                var detailsAdd = Items.Where(isAdd).ToList();
                var lstDataAdDuToan = Items.Where(x => x.Id == Guid.Empty && x.IsModified && !string.IsNullOrEmpty(x.SDuToanChiTietToi));
                detailsAdd.AddRange(lstDataAdDuToan);
                var detailsUpdate = Items.Where(isUpdate).ToList();

                //thêm mới chứng từ chi tiết
                if (detailsAdd.Count > 0)
                {
                    var addItems = new List<BhQtcQuyKinhPhiQuanLyChiTiet>();
                    _mapper.Map(detailsAdd, addItems);
                    addItems = detailsAdd.Select(x => new BhQtcQuyKinhPhiQuanLyChiTiet
                    {
                        Id = Guid.NewGuid(),
                        DNgayTao = DateTime.Now,
                        SNguoiTao = _sessionInfo.Principal,
                        IID_QTC_Quy_KinhPhiQuanLy = Model.Id,
                        IIdMaDonVi = Model.IID_MaDonVi,
                        IID_MucLucNganSach = x.IID_MucLucNganSach,
                        SM = x.SM,
                        STM = x.STM,
                        SNoiDung = x.SNoiDung,
                        INamLamViec = _sessionInfo.YearOfWork,
                        SXauNoiMa = x.SXauNoiMa,
                        FTienDuToanDuocGiao = x.FTienDuToanDuocGiao,
                        FTienThucChi = !x.BHangCha ? x.FTienThucChi ?? 0 : 0,
                        FTienDeNghiQuyetToanQuyNay = !x.BHangCha ? x.FTienDeNghiQuyetToanQuyNay ?? 0 : 0,
                        FTienQuyetToanDaDuyet = !x.BHangCha ? x.FTienQuyetToanDaDuyet ?? 0 : 0,
                        FTienXacNhanQuyetToanQuyNay = !x.BHangCha ? x.FTienXacNhanQuyetToanQuyNay ?? 0 : 0,
                        SGhiChu = x.SGhiChu ?? string.Empty,
                    }).ToList();

                    _bhQtcQKPQuanLyChiTietService.AddRange(addItems);

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
                        //updateItem.FTienThucChi = !updateItem.BHangCha ? updateItem.FTienThucChi ?? 0 : 0;
                        updateItem.FTienDeNghiQuyetToanQuyNay = !updateItem.BHangCha ? updateItem.FTienDeNghiQuyetToanQuyNay ?? 0 : 0;
                        updateItem.FTienQuyetToanDaDuyet = !updateItem.BHangCha ? updateItem.FTienQuyetToanDaDuyet ?? 0 : 0;
                        updateItem.FTienXacNhanQuyetToanQuyNay = !updateItem.BHangCha ? updateItem.FTienXacNhanQuyetToanQuyNay ?? 0 : 0;
                        var chungTuChiTiet = _bhQtcQKPQuanLyChiTietService.FindById(updateItem.Id);
                        _mapper.Map(updateItem, chungTuChiTiet);
                        _bhQtcQKPQuanLyChiTietService.Update(chungTuChiTiet);
                        updateItem.IsModified = false;
                    }
                }

                //cập nhật tổng cộng chứng từ
                if (detailsAdd.Count > 0 || detailsUpdate.Count > 0)
                {
                    var chungTuChiTiet = _bhQtcQKPQuanLyService.FindById(Model.Id);
                    chungTuChiTiet.FTongTienXacNhanQuyetToanQuyNay = Model.FTongTienXacNhanQuyetToanQuyNay;
                    chungTuChiTiet.FTongTienDeNghiQuyetToanQuyNay = Model.FTongTienDeNghiQuyetToanQuyNay;
                    chungTuChiTiet.FTongTienDuToanDuocGiao = Model.FTongTienDuToanDuocGiao;
                    chungTuChiTiet.FTongTienThucChi = Model.FTongTienThucChi;
                    chungTuChiTiet.FTongTienQuyetToanDaDuyet = Model.FTongTienQuyetToanDaDuyet;
                    _bhQtcQKPQuanLyService.Update(chungTuChiTiet);
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
                object content;
                switch (dialogType)
                {
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS:
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_AGENCY:
                    case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:
                        PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel.SettlementTypeValue = dialogType;
                        PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel.Init();
                        content = new PrintQuyetToanChiKinhPhiQuanLyNotice
                        {
                            DataContext = PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel
                        };
                        break;
                    default:
                        content = null;
                        break;
                }

                if (content != null)
                {
                    DialogHost.Show(content, SystemConstants.DETAIL_DIALOG, null, null);
                }
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Search

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhQtcQuyKinhPhiQuanLyChiTietModel)obj;

            result = VoucherDetailFilter(item);
            if (!result && item.IsHangCha)
            {
                result = _xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (result)
                item.IsFilter = result;
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = DataSearch.Any(x => x.IID_MucLucNganSach.Equals(item.IID_MucLucNganSach));
            }
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
            DataSearch = new ObservableCollection<BhQtcQuyKinhPhiQuanLyChiTietModel>();
        }

        private void OnClearSearch(object obj)
        {
            LoadDefault();
            ItemsView.Refresh();
        }

        private void SearchTextFilter()
        {
            List<string> lstParents = new List<string>();
            List<BhQtcQuyKinhPhiQuanLyChiTietModel> lstChildSearch = new List<BhQtcQuyKinhPhiQuanLyChiTietModel>();
            List<BhQtcQuyKinhPhiQuanLyChiTietModel> lstParentSearch = new List<BhQtcQuyKinhPhiQuanLyChiTietModel>();
            List<BhQtcQuyKinhPhiQuanLyChiTietModel> results = new List<BhQtcQuyKinhPhiQuanLyChiTietModel>();
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
                lstParents.Add(lstParents[0].Substring(0, 1));
                lstParents.Add(lstParents[0].Substring(0, 3));
                results = Items.Where(x => lstParents.Contains(x.SXauNoiMa)).ToList();
            }
            if (!lstParentSearch.IsEmpty())
            {
                if (results.IsEmpty())
                    results = GetDataParent(lstParentSearch.Select(x => x.SXauNoiMa).Distinct().ToList());
                else
                    results.AddRange(GetDataParent(lstParentSearch.Select(x => x.SXauNoiMa).Distinct().Where(x => !lstParents.Contains(x)).ToList()));
            }
            DataSearch = new ObservableCollection<BhQtcQuyKinhPhiQuanLyChiTietModel>(results);
            ItemsView.Refresh();
        }

        private List<BhQtcQuyKinhPhiQuanLyChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhQtcQuyKinhPhiQuanLyChiTietModel> result = new List<BhQtcQuyKinhPhiQuanLyChiTietModel>();
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

        private void GetListChild(List<BhQtcQuyKinhPhiQuanLyChiTietModel> lstInput, List<BhQtcQuyKinhPhiQuanLyChiTietModel> results)
        {
            var itemChild = Items.Where(x => lstInput.Select(x => x.IID_MucLucNganSach).Distinct().Contains(x.IdParent)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => Items.Select(y => y.IdParent).Distinct().Contains(x.IID_MucLucNganSach ?? Guid.Empty)))
                {
                    GetListChild(new List<BhQtcQuyKinhPhiQuanLyChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

    }
}
