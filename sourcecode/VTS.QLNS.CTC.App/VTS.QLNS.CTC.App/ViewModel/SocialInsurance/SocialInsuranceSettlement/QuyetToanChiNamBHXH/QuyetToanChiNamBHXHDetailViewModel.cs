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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.PritnReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH
{
    public class QuyetToanChiNamBHXHDetailViewModel : DetailViewModelBase<BhQtcnBHXHModel, BhQtcnBHXHChiTietModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;

        private readonly IQtcnBHXHService _qtcnBHXHService;
        private readonly IQtcnBHXHChiTietService _qtcnBHXHChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly INsDonViService _nsDonViService;

        private ICollectionView _itemsView;
        private readonly ILog _logger;
        public bool IsPropertyChange;
        private SessionInfo _sessionInfo;
        private readonly bool _isCapPhatToanDonVi;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public override Type ContentType => typeof(QuyetToanChiNamBHXHDetail);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;
        public bool IsSaveData;
        public bool IsReadOnlySoThucChi => Model.BThucChiTheo4Quy;
        public bool IsDelete => (_selectedTypeShowAgency == null || _selectedTypeShowAgency.ValueItem != TypeDisplay.TONG_DONVI) && (SelectedItem != null);
        public bool IsDeleteAll => Items.Any(item => !item.IsModified);
        public bool IsReadOnlyGrid => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI);
        public bool IsTongHop => !string.IsNullOrEmpty(Model.SDSSoChungTuTongHop);
        public bool IsShowAgencyFilter => IsTongHop && _selectedTypeShowAgencyKHT != null && _selectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI;
        public Visibility VisibleColAgency => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI) ?
            Visibility.Collapsed : Visibility.Visible;

        public Visibility VisibleVoucherNo => !string.IsNullOrEmpty(Model.SDSSoChungTuTongHop) && VisibleColAgency == Visibility.Visible ? Visibility.Visible : Visibility.Collapsed;

        public bool ReadOnlyCapPhat => IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI;
        public bool ReadOnlyDeNghi => IsTongHop;
        public bool IsEditByRole => Model.SNguoiTao == _sessionInfo.Principal;

        private string xnmConcatenation = "";
        private ICollection<BhQtcnBHXHChiTietModel> _filterResult = new HashSet<BhQtcnBHXHChiTietModel>();

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
                //_itemsView.Refresh();
                //CalculateData();
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

        //Xác định lần đầu tiên tạo mới
        public bool IsCreate;

        public int NamLamViec { get; set; }

        private bool _isSummaryVoucher;
        public bool IsSummaryVoucher
        {
            get => _isSummaryVoucher;
            set
            {
                SetProperty(ref _isSummaryVoucher, value);
                OnPropertyChanged(nameof(ReadOnlyCapPhat));
                OnPropertyChanged(nameof(ReadOnlyDeNghi));
            }
        }


        private bool _isOpenRefresh;
        public bool IsOpenRefresh
        {
            get => _isOpenRefresh;
            set => SetProperty(ref _isOpenRefresh, value);
        }
        private string _selectedLNS;
        public string SelectedLNS
        {
            get => _selectedLNS;
            set => SetProperty(ref _selectedLNS, value);
        }

        private bool _isOpenLnsPopup;
        public bool IsOpenLnsPopup
        {
            get => _isOpenLnsPopup;
            set
            {
                SetProperty(ref _isOpenLnsPopup, value);
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                SetProperty(ref _searchLNS, value);
            }
        }

        public Visibility ShowTotal => Items.Count > 0 ? Visibility.Visible : Visibility.Hidden;


        private bool _isShowTypeAgency;
        public bool IsShowTypeAgency
        {
            get => _isShowTypeAgency;
            set => SetProperty(ref _isShowTypeAgency, value);
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
                    OnPropertyChanged(nameof(ReadOnlyCapPhat));
                    OnPropertyChanged(nameof(ReadOnlyDeNghi));
                    LoadData();
                }
            }
        }

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private ObservableCollection<BhQtcnBHXHChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhQtcnBHXHChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhQtcnBHXHChiTietModel _selectedPopupItem;
        public BhQtcnBHXHChiTietModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SNoiDungSearch = _selectedPopupItem?.SLoaiTroCap;
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

        private ObservableCollection<BhQtcnBHXHChiTietModel> _dataSearch;
        public ObservableCollection<BhQtcnBHXHChiTietModel> DataSearch
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

        public RelayCommand PrintCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand AutoFillDataCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }

        public PrintQuyetToanChiNamBHXHViewModel PrintQuyetToanChiNamBHXHViewModel { get; }

        public QuyetToanChiNamBHXHDetailViewModel(ICpChungTuService cpChungTuService,
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            IQtcnBHXHService qtcnBHXHService,
            IQtcnBHXHChiTietService qtcnBHXHChiTietService,
            IDanhMucService danhMucService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            PrintQuyetToanChiNamBHXHViewModel printQuyetToanChiNamBHXHViewModel,
            INsDonViService nsDonViService
            )
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _qtcnBHXHService = qtcnBHXHService;
            _qtcnBHXHChiTietService = qtcnBHXHChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _nsDonViService = nsDonViService;

            PrintQuyetToanChiNamBHXHViewModel = printQuyetToanChiNamBHXHViewModel;

            SaveCommand = new RelayCommand(obj => OnSaveData());
            RefreshCommand = new RelayCommand(obj => OnRefreshAllData());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            PrintCommand = new RelayCommand(obj => OnPrintDetal(obj));
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SearchCommand = new RelayCommand(OnSearch);
        }

        public override void Init()
        {
            try
            {
                MarginRequirement = new System.Windows.Thickness(10);
                _sessionInfo = _sessionService.Current;
                NamLamViec = _sessionService.Current.YearOfWork;
                IsSummaryVoucher = false;
                IsShowTypeAgency = false;
                _isShowColumnUnit = false;
                _selectedAgency = null;
                if (!string.IsNullOrEmpty(Model.SDSSoChungTuTongHop))
                {
                    IsShowTypeAgency = true;
                    IsSummaryVoucher = true;
                    LoadComboboxTypeShow();
                    if (!IsEditByRole)
                        MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.SNguoiTao));
                    OnPropertyChanged(nameof(ReadOnlyCapPhat));
                    OnPropertyChanged(nameof(ReadOnlyDeNghi));
                    OnPropertyChanged(nameof(IsDeleteAll));
                    OnPropertyChanged(nameof(IsShowTypeAgency));
                    OnPropertyChanged(nameof(VisibleColAgency));
                    OnPropertyChanged(nameof(VisibleVoucherNo));
                }
                IsPropertyChange = false;
                LoadTypeShow();
                LoadData();
                LoadDefault();
                OnPropertyChanged(nameof(IsReadOnlySoThucChi));
                IsPropertyChange = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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

        private void LoadDefault()
        {
            SNoiDungSearch = string.Empty;
            DataSearch = new ObservableCollection<BhQtcnBHXHChiTietModel>();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsDelete));
        }

        private void SearchDataParent()
        {
            _itemsView?.Refresh();
        }

        private void OnPrintDetal(object param)
        {
            int dialogType = (int)param;
            switch (dialogType)
            {
                case (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH:
                case (int)BhQuyeToanChiNamType.PRINT_QUYETTOANCHIBHXH:
                    PrintQuyetToanChiNamBHXHViewModel.SettlementTypeValue = dialogType;
                    PrintQuyetToanChiNamBHXHViewModel.Init();
                    PrintQuyetToanChiNam view1 = new PrintQuyetToanChiNam
                    {
                        DataContext = PrintQuyetToanChiNamBHXHViewModel
                    };
                    DialogHost.Show(view1, SettlementScreen.DETAIL_DIALOG, null, null);
                    break;
            }
        }

        public override void LoadData(params object[] args)
        {
            int iNamLamViec = _sessionInfo.YearOfWork;
            List<BhQtcnBHXHChiTietQuery> listDataQuery = new List<BhQtcnBHXHChiTietQuery>();
            List<BhQtcnBHXHChiTietQuery> listDataPBDTCQuery = new List<BhQtcnBHXHChiTietQuery>();
            IEnumerable<BhDanhMucLoaiChi> lstLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(iNamLamViec);
            BhDanhMucLoaiChi loaiChi = lstLoaiChi.Where(x => x.SLNS == Model.SDSLNS).FirstOrDefault();
            bool voucherexist = _qtcnBHXHChiTietService.ExistVoucherDetail(Model.Id, Model.INamLamViec);

            if (IsTongHop && SelectedTypeShowAgencyKHT != null && SelectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI && _selectedAgency == null)
            {
                List<string> voucherNos = Model.SDSSoChungTuTongHop.Split(",").ToList();
                List<BhQtcnBHXH> listChungTu = _qtcnBHXHService.FindByAggregateVoucher(voucherNos, iNamLamViec).ToList();
                string agencyIds = string.Join(",", listChungTu.Select(x => x.IIdMaDonVi));
                IEnumerable<DonVi> listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);

                List<BhQtcnBHXHChiTietQuery> listChungTuChiTietParent = new List<BhQtcnBHXHChiTietQuery>();
                List<BhQtcnBHXHChiTietQuery> listChungTuChiTietChildren = new List<BhQtcnBHXHChiTietQuery>();
                List<BhQtcnBHXHChiTietQuery> temp = new List<BhQtcnBHXHChiTietQuery>();
                foreach (BhQtcnBHXH chungtu in listChungTu)
                {
                    List<BhQtcnBHXHChiTietQuery> listQuery = _qtcnBHXHChiTietService.GetChiTietQuyetToanChiNamBHXH(chungtu.Id, iNamLamViec, Model.BThucChiTheo4Quy, Model.ILoaiTongHop, Model.IIdMaDonVi).ToList();
                    listQuery.Where(x => !x.BHangCha).Select(x => x.STenDonVi == listDonVi.FirstOrDefault(x => x.IIDMaDonVi == chungtu.IIdMaDonVi).TenDonVi).ToList();
                    listChungTuChiTietParent.AddRange(listQuery.Where(x => x.BHangCha).ToList());
                    listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.BHangCha));
                }
                List<string> listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                var lstChungTu = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x =>
                    new
                    {
                        Data = x.FirstOrDefault(),
                        FTienDuToanDuyet = x.Sum(x => x.FTienDuToanDuyet)
                    }
                ).ToList();

                lstChungTu.ForEach(x =>
                {
                    x.Data.FTienDuToanDuyet = x.FTienDuToanDuyet;
                });

                listChungTuChiTietParent = lstChungTu.Select(x => x.Data).ToList();
                temp.AddRange(listChungTuChiTietParent);
                temp.AddRange(listChungTuChiTietChildren);
                temp = temp.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.STenDonVi).ToList();
                listDataQuery = temp;

                _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
                OnPropertyChanged(nameof(Agencies));
            }
            else
            {
                if (_selectedAgency != null && SelectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    string sMaDonVi = _selectedAgency.ValueItem;
                    Guid idChungTu = Guid.Empty;
                    int LoaiTongHop = IsDonViRoot(sMaDonVi) ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;
                    System.Linq.Expressions.Expression<Func<BhQtcnBHXH, bool>> predicateCtDv = PredicateBuilder.True<BhQtcnBHXH>();
                    predicateCtDv = predicateCtDv.And(x => x.INamLamViec == Model.INamLamViec);
                    predicateCtDv = predicateCtDv.And(x => x.IIdMaDonVi == sMaDonVi);
                    BhQtcnBHXH ctDonVi = _qtcnBHXHService.FindByCondition(predicateCtDv).FirstOrDefault();
                    if (ctDonVi != null)
                    {
                        idChungTu = ctDonVi.Id;
                    }

                    listDataQuery = _qtcnBHXHChiTietService.GetChiTietQuyetToanChiNamBHXH(idChungTu, iNamLamViec, Model.BThucChiTheo4Quy, LoaiTongHop, sMaDonVi).ToList();
                }
                else
                {
                    listDataQuery = _qtcnBHXHChiTietService.GetChiTietQuyetToanChiNamBHXH(Model.Id, iNamLamViec, Model.BThucChiTheo4Quy, Model.ILoaiTongHop, Model.IIdMaDonVi).ToList();
                }

            }
            if (!listDataQuery.IsEmpty()) listDataQuery.Select(x =>
            {
                x.IIdMaDonVi = Model.IIdMaDonVi;
                x.INamLamViec = Model.INamLamViec ?? _sessionService.Current.YearOfWork;
                return x;
            }).ToList();
            Items = _mapper.Map<ObservableCollection<BhQtcnBHXHChiTietModel>>(listDataQuery);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhQtcnBHXHChiTietModel>>(listDataQuery);
            _itemsView = CollectionViewSource.GetDefaultView(Items);
            _itemsView.Filter = ItemsViewFilter;

            List<BhQtcnBHXHChiTietQuery> listData = listDataQuery.Where(x => string.IsNullOrEmpty(x.SL) || x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToi).ToList();
            listData.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                {
                    x.BHangCha = false;
                }
            });
            CalculateDataDuToan(listData);

            foreach (BhQtcnBHXHChiTietQuery itemdata in listData)
            {
                foreach (BhQtcnBHXHChiTietModel item in Items)
                {
                    if (item.SXauNoiMa == itemdata.SXauNoiMa)
                    {
                        item.FTienDuToanDuyet = itemdata.FTienDuToanDuyet;
                        item.FTienThua = itemdata.FTienDuToanDuyet;
                    }
                }
            }

            foreach (BhQtcnBHXHChiTietModel bhQtcnBHYTChiTietModel in Items)
            {
                bhQtcnBHYTChiTietModel.IsFilter = true;
                if (!bhQtcnBHYTChiTietModel.BHangCha)
                {
                    bhQtcnBHYTChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        BhQtcnBHXHChiTietModel item = (BhQtcnBHXHChiTietModel)sender;
                        if (args.PropertyName == nameof(BhQtcnBHXHChiTietModel.ISoSQThucChi) || args.PropertyName == nameof(BhQtcnBHXHChiTietModel.FTienSQThucChi)
                        || args.PropertyName == nameof(BhQtcnBHXHChiTietModel.ISoQNCNThucChi) || args.PropertyName == nameof(BhQtcnBHXHChiTietModel.FTienQNCNThucChi)
                        || args.PropertyName == nameof(BhQtcnBHXHChiTietModel.ISoCNVCQPThucChi) || args.PropertyName == nameof(BhQtcnBHXHChiTietModel.FTienCNVCQPThucChi)
                        || args.PropertyName == nameof(BhQtcnBHXHChiTietModel.ISoHSQBSThucChi) || args.PropertyName == nameof(BhQtcnBHXHChiTietModel.FTienHSQBSThucChi)
                        || args.PropertyName == nameof(BhQtcnBHXHChiTietModel.ISoLDHDThucChi) || args.PropertyName == nameof(BhQtcnBHXHChiTietModel.FTienLDHDThucChi))
                        {
                            item.ITongSoThucChi = (item.ISoSQThucChi ?? 0) + (item.ISoQNCNThucChi ?? 0) + (item.ISoCNVCQPThucChi ?? 0) + (item.ISoHSQBSThucChi ?? 0) + (item.ISoLDHDThucChi ?? 0);
                            item.FTongTienThucChi = (item.FTienSQThucChi ?? 0) + (item.FTienQNCNThucChi ?? 0) + (item.FTienCNVCQPThucChi ?? 0) + (item.FTienHSQBSThucChi ?? 0) + (item.FTienLDHDThucChi ?? 0);

                            item.FTienThieu = (item.FTongTienThucChi.GetValueOrDefault() > item.FTienDuToanDuyet.GetValueOrDefault()) ? (item.FTongTienThucChi ?? 0) - (item.FTienDuToanDuyet ?? 0) : 0;
                            item.FTienThua = (item.FTongTienThucChi.GetValueOrDefault() < item.FTienDuToanDuyet.GetValueOrDefault()) ? (item.FTienDuToanDuyet ?? 0) - (item.FTongTienThucChi ?? 0) : 0;
                            item.FTiLeThucHienTrenDuToan = (item.FTienDuToanDuyet.GetValueOrDefault(0) > 0) ? ((item.FTongTienThucChi ?? 0) / (item.FTienDuToanDuyet ?? 0)) * 100 : 0;
                            if (IsPropertyChange)
                            {
                                item.IsModified = true;
                                bhQtcnBHYTChiTietModel.IsModified = true;
                                CalculateData();
                                IsSaveData = true;

                                OnPropertyChanged(nameof(IsSaveData));
                            }
                        }
                    };
                }
            }

            CalculateData();
        }

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        private void LoadAgencies(string agencyIds)
        {
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            OnPropertyChanged(nameof(Agencies));
        }

        private void CalculateDataDuToan(List<BhQtcnBHXHChiTietQuery> listData)
        {
            listData.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    x.FTienDuToanDuyet = 0;
                    x.FTienThua = 0;

                });
            Dictionary<Guid, BhQtcnBHXHChiTietQuery> dictByMlns = listData.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            List<BhQtcnBHXHChiTietQuery> temp = listData.Where(x => !x.BHangCha).ToList();
            foreach (BhQtcnBHXHChiTietQuery item in temp)
            {

                CalculateParentDuToan(item.IID_MLNS_Cha.Value, item, dictByMlns);
            }

            UpdateTotalDuToan(listData);
        }

        private void UpdateTotalDuToan(List<BhQtcnBHXHChiTietQuery> listData)
        {
            Model.FTongTienDuToanDuyet = 0;
            Model.FTongTienDuToanDuyet = listData.Where(x => x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToi).Sum(x => x.FTienDuToanDuyet ?? 0);
            Model.FTongTienThieu = listData.Where(x => x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToi).Sum(x => x.FTienThieu ?? 0);
            Model.FTongTienThua = listData.Where(x => x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToi).Sum(x => x.FTienThua ?? 0);
        }

        private void CalculateParentDuToan(Guid iID_MLNS_Cha, BhQtcnBHXHChiTietQuery item, Dictionary<Guid, BhQtcnBHXHChiTietQuery> dictByMlns)
        {
            if (iID_MLNS_Cha == null || !dictByMlns.ContainsKey(iID_MLNS_Cha))
            {
                return;
            }

            BhQtcnBHXHChiTietQuery model = dictByMlns[iID_MLNS_Cha];
            model.FTienDuToanDuyet = (model.FTienDuToanDuyet ?? 0) + (item.FTienDuToanDuyet ?? 0);
            model.FTienThua = (model.FTienThua ?? 0) + (item.FTienThua ?? 0);
            CalculateParentDuToan(model.IID_MLNS_Cha.Value, item, dictByMlns);
        }

        private bool ItemsViewFilter(object obj)
        {
            //if (!(obj is BhQtcnBHXHChiTietModel temp)) return true;
            //bool result = true;
            //var item = (BhQtcnBHXHChiTietModel)obj;
            //if (!string.IsNullOrEmpty(SNoiDungSearch))
            //{
            //    result = DataSearch.Any(x => x.IID_MLNS.Equals(item.IID_MLNS));
            //}
            //return result;

            bool result = true;
            if (!(obj is BhQtcnBHXHChiTietModel temp)) return result;
            BhQtcnBHXHChiTietModel item = (BhQtcnBHXHChiTietModel)obj;
            //item.IsEditable = false;
            result = VoucherDetailFilter(item);
            if (!result && item.BHangCha)
            {
                result = xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (result)
                item.IsFilter = result;
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = DataSearch.Any(x => x.IIdMucLucNganSach.Equals(item.IIdMucLucNganSach));
            }

            return result;
        }

        private void CalculateData()
        {
            Items.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    //x.FTienDuToanDuyet = 0;
                    x.ISoDuToanDuocDuyet = 0;
                    x.ITongSoThucChi = 0;
                    x.FTongTienThucChi = 0;
                    x.ISoSQThucChi = 0;
                    x.FTienSQThucChi = 0;
                    x.ISoQNCNThucChi = 0;
                    x.FTienQNCNThucChi = 0;
                    x.ISoCNVCQPThucChi = 0;
                    x.FTienCNVCQPThucChi = 0;
                    x.ISoHSQBSThucChi = 0;
                    x.FTienHSQBSThucChi = 0;
                    x.ISoLDHDThucChi = 0;
                    x.FTienLDHDThucChi = 0;
                    //x.FTienThua = 0;
                    //x.FTienThieu = 0;
                    x.STenDonVi = string.Empty;
                });
            Dictionary<Guid?, BhQtcnBHXHChiTietModel> dictByMlns = Items.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            List<BhQtcnBHXHChiTietModel> temp = Items.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (BhQtcnBHXHChiTietModel item in temp)
            {

                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }

            UpdateTotal();
        }

        private void CalculateParent(Guid? idParent, BhQtcnBHXHChiTietModel item, Dictionary<Guid?, BhQtcnBHXHChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            BhQtcnBHXHChiTietModel model = dictByMlns[idParent];

            //model.FTienDuToanDuyet = (model.FTienDuToanDuyet ?? 0) + (item.FTienDuToanDuyet ?? 0);
            model.ISoDuToanDuocDuyet = (model.ISoDuToanDuocDuyet ?? 0) + (item.ISoDuToanDuocDuyet ?? 0);
            model.ITongSoThucChi = (model.ITongSoThucChi ?? 0) + (item.ITongSoThucChi ?? 0);
            model.FTongTienThucChi = (model.FTongTienThucChi ?? 0) + (item.FTongTienThucChi ?? 0);
            model.ISoSQThucChi = (model.ISoSQThucChi ?? 0) + (item.ISoSQThucChi ?? 0);
            model.FTienSQThucChi = (model.FTienSQThucChi ?? 0) + (item.FTienSQThucChi ?? 0);
            model.ISoQNCNThucChi = (model.ISoQNCNThucChi ?? 0) + (item.ISoQNCNThucChi ?? 0);
            model.FTienQNCNThucChi = (model.FTienQNCNThucChi ?? 0) + (item.FTienQNCNThucChi ?? 0);
            model.ISoCNVCQPThucChi = (model.ISoCNVCQPThucChi ?? 0) + (item.ISoCNVCQPThucChi ?? 0);
            model.FTienCNVCQPThucChi = (model.FTienCNVCQPThucChi ?? 0) + (item.FTienCNVCQPThucChi ?? 0);
            model.ISoHSQBSThucChi = (model.ISoHSQBSThucChi ?? 0) + (item.ISoHSQBSThucChi ?? 0);
            model.FTienHSQBSThucChi = (model.FTienHSQBSThucChi ?? 0) + (item.FTienHSQBSThucChi ?? 0);
            model.ISoLDHDThucChi = (model.ISoLDHDThucChi ?? 0) + (item.ISoLDHDThucChi ?? 0);
            model.FTienLDHDThucChi = (model.FTienLDHDThucChi ?? 0) + (item.FTienLDHDThucChi ?? 0);
            model.FTienThua = (model.FTienThua ?? 0) + (item.FTienThua ?? 0);
            model.FTienThieu = (model.FTienThieu ?? 0) + (item.FTienThieu ?? 0);

            CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
        }

        private void UpdateTotal()
        {
            Model.ITongSoSQDeNghi = 0;
            Model.FTongTienSQDeNghi = 0;

            Model.ITongSoQNCNDeNghi = 0;
            Model.FTongTienQNCNDeNghi = 0;

            Model.ITongSoCNVCQPDeNghi = 0;
            Model.FTongTienCNVCQPDeNghi = 0;

            Model.ITongSoDeNghi = 0;
            Model.FTongTienDeNghi = 0;

            Model.ITongSoHSQBSDeNghi = 0;
            Model.FTongTienHSQBSDeNghi = 0;

            Model.FTongTienThua = 0;
            Model.FTongTienThieu = 0;


            //Model.FTongTienDuToanDuyet = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienDuToanDuyet ?? 0);
            Model.ITongSoSQDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.ISoSQThucChi ?? 0);
            Model.FTongTienSQDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienSQThucChi ?? 0);

            Model.ITongSoQNCNDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.ISoQNCNThucChi ?? 0);
            Model.FTongTienQNCNDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienQNCNThucChi ?? 0);

            Model.ITongSoCNVCQPDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.ISoCNVCQPThucChi ?? 0);
            Model.FTongTienCNVCQPDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienCNVCQPThucChi ?? 0);

            Model.ITongSoLDHDDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.ISoLDHDThucChi ?? 0);
            Model.FTongTienLDHDDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienLDHDThucChi ?? 0);
            Model.ITongSoHSQBSDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.ISoHSQBSThucChi ?? 0);
            Model.FTongTienHSQBSDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienHSQBSThucChi ?? 0);

            Model.ITongSoDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.ITongSoThucChi ?? 0);
            Model.FTongTienDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTongTienThucChi ?? 0);

            Model.FTongTienThua = Items.Where(x => string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTienThua ?? 0);
            Model.FTongTienThieu = Items.Where(x => string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTienThieu ?? 0);

        }
        private void OnSaveData()
        {
            if (!IsSaveData)
            {
                return;
            }
            List<BhQtcnBHXHChiTietModel> lstDataAdd = Items.Where(x => !x.BHangCha && x.Id == Guid.Empty && x.IsModified).ToList();
            //var lstDataAdDuToan = Items.Where(x => x.Id == Guid.Empty && x.IsModified && !string.IsNullOrEmpty(x.SDuToanChiTietToi));
            //lstDataAdd.AddRange(lstDataAdDuToan);

            List<BhQtcnBHXHChiTietModel> lstDataUpdate = Items.Where(x => !x.BHangCha && x.Id != Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
            //var lstDataUpdateDuToan = Items.Where(x => x.IsModified && !string.IsNullOrEmpty(x.SDuToanChiTietToi));
            //lstDataUpdate.AddRange(lstDataUpdateDuToan);

            List<BhQtcnBHXHChiTietModel> lstDataDelete = Items.Where(x => !x.BHangCha && x.IsDeleted && x.IsModified && x.Id != Guid.Empty).ToList();

            List<BhQtcnBHXHChiTiet> addItemList = new List<BhQtcnBHXHChiTiet>();
            if (lstDataAdd.Count() > 0)
            {
                _mapper.Map(lstDataAdd, addItemList);
                addItemList.Select(x => { x.Id = Guid.NewGuid(); x.IIdQTCNamCheDoBHXH = Model.Id; return x; }).ToList();
                _qtcnBHXHChiTietService.AddRange(addItemList);

                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }
            if (lstDataUpdate.Count() > 0)
            {
                _mapper.Map(lstDataUpdate, addItemList);
                addItemList.Select(x => { x.IIdQTCNamCheDoBHXH = Model.Id; return x; }).ToList();
                _qtcnBHXHChiTietService.UpdateRange(addItemList);
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            if (lstDataDelete.Count() > 0)
            {
                _mapper.Map(lstDataDelete, addItemList);
                _qtcnBHXHChiTietService.RemoveRange(addItemList);
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; x.IsDeleted = false; return x; }).ToList();
            }

            //Update quyết toán chi nam BHXH
            BhQtcnBHXH chungtu = _qtcnBHXHService.FindById(Model.Id);
            if (chungtu != null)
            {
                chungtu.FTongTienDuToanDuyet = Model.FTongTienDuToanDuyet;
                chungtu.ITongSoSQDeNghi = Model.ITongSoSQDeNghi;
                chungtu.FTongTienSQDeNghi = Model.FTongTienSQDeNghi;
                chungtu.ITongSoQNCNDeNghi = Model.ITongSoQNCNDeNghi;
                chungtu.FTongTienQNCNDeNghi = Model.FTongTienQNCNDeNghi;
                chungtu.ITongSoCNVCQPDeNghi = Model.ITongSoCNVCQPDeNghi;
                chungtu.FTongTienCNVCQPDeNghi = Model.FTongTienCNVCQPDeNghi;
                chungtu.ITongSoHSQBSDeNghi = Model.ITongSoHSQBSDeNghi;
                chungtu.FTongTienHSQBSDeNghi = Model.FTongTienHSQBSDeNghi;
                chungtu.ITongSoLDHDDeNghi = Model.ITongSoLDHDDeNghi;
                chungtu.FTongTienLDHDDeNghi = Model.FTongTienLDHDDeNghi;
                chungtu.ITongSoDeNghi = Model.ITongSoDeNghi;
                chungtu.FTongTienDeNghi = Model.FTongTienDeNghi;

                _qtcnBHXHService.Update(chungtu);
            }

            IsSaveData = false;
            LoadData();
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }


        protected override void OnRefresh()
        {
            IsOpenRefresh = !IsOpenRefresh;
        }

        private void OnRefreshAllData()
        {
            try
            {
                if (IsSaveData)
                {
                    MessageBoxResult result = MessageBoxHelper.ConfirmCancel(Resources.ConfirmReloadData);
                    if (result == MessageBoxResult.Cancel)
                        return;
                    else if (result == MessageBoxResult.Yes)
                        OnSaveData();
                }
                IsCreate = false;
                SearchText = string.Empty;
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

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

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.BHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            BhQtcnBHXHChiTietModel item = (BhQtcnBHXHChiTietModel)obj;

            if (TypeShowsSelected != null)
            {
                if (TypeShowsSelected.ValueItem == TypeDisplay.CO_SO_LIEU)
                    result = result && item.IsHadData;
                else if (TypeShowsSelected.ValueItem == TypeDisplay.CHUA_CO_SO_LIEU)
                    result = result && !item.IsHadDataChil;
            }

            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIdMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhQtcnBHXHChiTietModel> results = new List<BhQtcnBHXHChiTietModel>();

                List<string> lstSXaNoiMaChildSearch = Items.Where(x => x.SLoaiTroCap.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.BHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                List<string> lstSXaNoiMaParentSearch = Items.Where(x => x.SLoaiTroCap.ToLower().Contains(SNoiDungSearch.ToLower()) && x.BHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
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
                DataSearch = new ObservableCollection<BhQtcnBHXHChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhQtcnBHXHChiTietModel>();
            }
            _itemsView.Refresh();
        }

        private List<BhQtcnBHXHChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhQtcnBHXHChiTietModel> result = new List<BhQtcnBHXHChiTietModel>();
            List<string> lstParent = StringUtils.GetListKyHieuParent(lstInput);
            if (!lstParent.IsEmpty() && lstParent.Any(x => x.Count() >= 3))
            {
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
            }
            List<BhQtcnBHXHChiTietModel> lstData = Items.Where(x => lstParent.Contains(x.SXauNoiMa)).ToList();
            result.AddRange(lstData);
            GetListChild(lstData.Where(x => lstInput.Contains(x.SXauNoiMa)).ToList(), result);
            return result;
        }

        private void GetListChild(List<BhQtcnBHXHChiTietModel> lstInput, List<BhQtcnBHXHChiTietModel> results)
        {
            List<BhQtcnBHXHChiTietModel> itemChild = Items.Where(x => lstInput.Select(x => x.IID_MLNS).Distinct().Contains(x.IID_MLNS_Cha)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (BhQtcnBHXHChiTietModel item in itemChild.Where(x => Items.Select(y => y.IID_MLNS_Cha).Distinct().Contains(x.IID_MLNS ?? Guid.Empty)))
                {
                    GetListChild(new List<BhQtcnBHXHChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

    }
}
