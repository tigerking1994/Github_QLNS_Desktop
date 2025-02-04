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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamKCB.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamKCB.PritnReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamKCB
{
    public class QuyetToanChiNamKCBDetailViewModel : DetailViewModelBase<BhQtcnKCBModel, BhQtcnKCBChiTietModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;

        private readonly IQtcnKCBService _qtcnKCBService;
        private readonly IQtcnKCBChiTietService _qtcnKCBChiTietService;
        private readonly INsDonViService _nsDonViService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;

        private ICollectionView _itemsView;
        private readonly ILog _logger;

        private SessionInfo _sessionInfo;
        private readonly bool _isCapPhatToanDonVi;
        private ICollection<BhQtcnKCBChiTietModel> _filterResult = new HashSet<BhQtcnKCBChiTietModel>();

        public bool IsReadOnlySoThucChi => Model.BThucChiTheo4Quy;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public override Type ContentType => typeof(QuyetToanChiNamBHXHDetail);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;
        public bool IsSaveData;
        public bool IsDelete => (_selectedTypeShowAgency == null || _selectedTypeShowAgency.ValueItem != TypeDisplay.TONG_DONVI) && (SelectedItem != null);
        public bool IsDeleteAll => Items.Any(item => !item.IsModified);
        public bool IsReadOnlyGrid => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI);
        public bool IsTongHop => !string.IsNullOrEmpty(Model.SDSSoChungTuTongHop);
        public Visibility VisibleColAgency => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI) ?
            Visibility.Collapsed : Visibility.Visible;

        public Visibility VisibleVoucherNo => !string.IsNullOrEmpty(Model.SDSSoChungTuTongHop) && VisibleColAgency == Visibility.Visible ? Visibility.Visible : Visibility.Collapsed;

        public bool ReadOnlyCapPhat => IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI;
        public bool ReadOnlyDeNghi => IsTongHop;
        public bool IsEditByRole => Model.SNguoiTao == _sessionInfo.Principal;

        private string _xnmConcatenation = "";

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

        public bool IsShowAgencyFilter => IsSummaryVoucher && _selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI;


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
                    OnPropertyChanged(nameof(IsShowAgencyFilter));
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

        private ObservableCollection<BhQtcnKCBChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhQtcnKCBChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhQtcnKCBChiTietModel _selectedPopupItem;
        public BhQtcnKCBChiTietModel SelectedPopupItem
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

        private ObservableCollection<BhQtcnKCBChiTietModel> _dataSearch;
        public ObservableCollection<BhQtcnKCBChiTietModel> DataSearch
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

        public PrintQuyetToanChiNamKCViewModel PrintQuyetToanChiNamKCViewModel { get; }

        public RelayCommand SaveCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand AutoFillDataCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ClearSearchCommand { get; }


        public QuyetToanChiNamKCBDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            IQtcnKCBService qtcnKCBService,
            IQtcnKCBChiTietService qtcnKCBChiTietService,
            IDanhMucService danhMucService,
            INsDonViService nsDonViService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            PrintQuyetToanChiNamKCViewModel printQuyetToanChiNamKCViewModel
            )
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _logger = logger;
            _qtcnKCBService = qtcnKCBService;
            _qtcnKCBChiTietService = qtcnKCBChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            PrintQuyetToanChiNamKCViewModel = printQuyetToanChiNamKCViewModel;

            PrintCommand = new RelayCommand(OnPrintDetail);
            SaveCommand = new RelayCommand(obj => OnSaveData());
            RefreshCommand = new RelayCommand(obj => OnRefreshAllData());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
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
                _selectedAgency = null;
                if (!string.IsNullOrEmpty(Model.SDSSoChungTuTongHop))
                {
                    LoadComboboxTypeShow();
                }
                if (!string.IsNullOrEmpty(Model.SDSSoChungTuTongHop))
                {
                    IsShowTypeAgency = true;
                    IsSummaryVoucher = true;
                    if (!IsEditByRole)
                        MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.SNguoiTao));
                    OnPropertyChanged(nameof(ReadOnlyCapPhat));
                    OnPropertyChanged(nameof(ReadOnlyDeNghi));
                    OnPropertyChanged(nameof(IsDeleteAll));
                    OnPropertyChanged(nameof(IsShowTypeAgency));
                    OnPropertyChanged(nameof(VisibleColAgency));
                    OnPropertyChanged(nameof(VisibleVoucherNo));
                }
                LoadTypeShow();
                LoadData();
                OnPropertyChanged(nameof(IsReadOnlySoThucChi));
                OnClearSearch(false);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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

        private void SearchDataParent()
        {
            if (_itemsView != null)
            {
                _itemsView.Refresh();
            }
        }

        private void OnPrintDetail(object param)
        {
            int dialogType = (int)param;
            switch (dialogType)
            {
                case (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH:
                case (int)BhQuyeToanChiNamType.PRINT_QUYETTOANCHIBHXH:
                    PrintQuyetToanChiNamKCViewModel.SettlementTypeValue = dialogType;
                    PrintQuyetToanChiNamKCViewModel.Init();
                    PrintQuyetToanChiNamKCB view1 = new PrintQuyetToanChiNamKCB
                    {
                        DataContext = PrintQuyetToanChiNamKCViewModel
                    };
                    DialogHost.Show(view1, SystemConstants.DETAIL_DIALOG, null, null);
                    break;
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsDelete));
        }

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;


        public override void LoadData(params object[] args)
        {
            int iNamLamViec = _sessionInfo.YearOfWork;
            List<BhQtcnKCBChiTietQuery> listDataQuery = new List<BhQtcnKCBChiTietQuery>();
            List<BhQtcnKCBChiTietQuery> listDataPBDTCQuery = new List<BhQtcnKCBChiTietQuery>();
            IEnumerable<BhDanhMucLoaiChi> lstLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(iNamLamViec);
            BhDanhMucLoaiChi loaiChi = lstLoaiChi.Where(x => x.SLNS == Model.SDSLNS).FirstOrDefault();
            bool voucherexist = _qtcnKCBChiTietService.ExistVoucherDetail(Model.Id, Model.INamLamViec);

            if (IsSummaryVoucher && SelectedTypeShowAgency?.ValueItem == TypeDisplay.CHITIET_DONVI && _selectedAgency == null)
            {
                List<string> voucherNos = Model.SDSSoChungTuTongHop.Split(",").ToList();
                List<BhQtcnKCB> listChungTu = _qtcnKCBService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork && voucherNos.Contains(x.SSoChungTu) && x.BDaTongHop).ToList();
                string agencyIds = string.Join(",", listChungTu.Select(x => x.IIdMaDonVi));
                IEnumerable<DonVi> listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);

                List<BhQtcnKCBChiTietQuery> listChungTuChiTietParent = new List<BhQtcnKCBChiTietQuery>();
                List<BhQtcnKCBChiTietQuery> listChungTuChiTietChildren = new List<BhQtcnKCBChiTietQuery>();
                foreach (BhQtcnKCB chungTu in listChungTu)
                {
                    List<BhQtcnKCBChiTietQuery> listQuery = _qtcnKCBChiTietService.GetChiTietQuyetToanChiNamKCB(chungTu.Id, chungTu.SDSLNS, iNamLamViec, chungTu.BThucChiTheo4Quy, chungTu.IIdMaDonVi, true).ToList();
                    listQuery.Where(x => !x.BHangCha).Select(x => x.STenDonVi = listDonVi.FirstOrDefault(y => y.IIDMaDonVi == chungTu.IIdMaDonVi).TenDonVi).ToList();
                    listChungTuChiTietParent.AddRange(listQuery.Where(x => x.BHangCha));
                    listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.BHangCha));
                }
                List<string> listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                var temp = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x =>
                      new
                      {
                          Data = x.FirstOrDefault(),
                          FTienDuToanGiaoNamNay = x.Sum(x => x.FTienDuToanGiaoNamNay),
                          FTienDuToanNamTruocChuyenSang = x.Sum(x => x.FTienDuToanNamTruocChuyenSang)
                      }
                  ).ToList();
                temp.ForEach(x =>
                {
                    x.Data.FTienDuToanGiaoNamNay = x.FTienDuToanGiaoNamNay;
                    x.Data.FTienDuToanNamTruocChuyenSang = x.FTienDuToanNamTruocChuyenSang;
                });
                listChungTuChiTietParent = temp.Select(x => x.Data).ToList();
                listDataQuery.AddRange(listChungTuChiTietParent);
                listDataQuery.AddRange(listChungTuChiTietChildren);
                listDataQuery = listDataQuery.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IIdMaDonVi).ToList();

                //LoadAgencies(agencyIds);
                _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
                OnPropertyChanged(nameof(Agencies));
            }
            else
            {
                if (_selectedAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    string sMaDonVi = _selectedAgency.ValueItem;
                    Guid idChungTu = Guid.Empty;
                    System.Linq.Expressions.Expression<Func<BhQtcnKCB, bool>> predicateCtDv = PredicateBuilder.True<BhQtcnKCB>();
                    predicateCtDv = predicateCtDv.And(x => x.INamLamViec == Model.INamLamViec);
                    predicateCtDv = predicateCtDv.And(x => x.IIdMaDonVi == sMaDonVi);
                    BhQtcnKCB ctDonVi = _qtcnKCBService.FindByCondition(predicateCtDv).FirstOrDefault();
                    if (ctDonVi != null)
                    {
                        idChungTu = ctDonVi.Id;
                    }

                    listDataQuery = _qtcnKCBChiTietService.GetChiTietQuyetToanChiNamKCB(idChungTu, Model.SDSLNS, iNamLamViec, Model.BThucChiTheo4Quy, sMaDonVi, !IsDonViRoot(sMaDonVi)).ToList();
                }
                else
                {
                    listDataQuery = _qtcnKCBChiTietService.GetChiTietQuyetToanChiNamKCB(Model.Id, Model.SDSLNS, iNamLamViec, Model.BThucChiTheo4Quy, Model.IIdMaDonVi, !IsDonViRoot(Model.IIdMaDonVi)).ToList();
                }
            }

            Items = _mapper.Map<ObservableCollection<BhQtcnKCBChiTietModel>>(listDataQuery);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhQtcnKCBChiTietModel>>(listDataQuery);
            _itemsView = CollectionViewSource.GetDefaultView(Items);
            _itemsView.Filter = ItemsViewFilter;
            CalculateData();
            foreach (BhQtcnKCBChiTietModel bhQtcnBHYTChiTietModel in Items)
            {
                bhQtcnBHYTChiTietModel.IsFilter = true;
                if (!bhQtcnBHYTChiTietModel.BHangCha)
                {
                    bhQtcnBHYTChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhQtcnKCBChiTietModel.FTienThucChi))
                        {
                            BhQtcnKCBChiTietModel item = (BhQtcnKCBChiTietModel)sender;
                            item.IsModified = true;
                            bhQtcnBHYTChiTietModel.IsModified = true;

                            item.FTienTongDuToanDuocGiao = (item.FTienDuToanNamTruocChuyenSang ?? 0) + (item.FTienDuToanGiaoNamNay ?? 0);
                            item.FTienThua = item.FTienTongDuToanDuocGiao.GetValueOrDefault() > item.FTienThucChi.GetValueOrDefault() ? (item.FTienTongDuToanDuocGiao ?? 0) - (item.FTienThucChi ?? 0) : 0;
                            item.FTienThieu = item.FTienTongDuToanDuocGiao.GetValueOrDefault() < item.FTienThucChi.GetValueOrDefault() ? (item.FTienThucChi ?? 0) - (item.FTienTongDuToanDuocGiao ?? 0) : 0;
                            CalculateData();
                            IsSaveData = true;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                }
            }
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            _xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        private bool ItemsViewFilter(object obj)
        {
            if (!(obj is BhQtcnKCBChiTietModel temp)) return true;
            bool result = true;
            BhQtcnKCBChiTietModel item = obj as BhQtcnKCBChiTietModel;
            result = VoucherDetailFilter(item);
            if (!result && item.IsHangCha)
            {
                result = _xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = DataSearch.Any(x => x.IID_MLNS.Equals(item.IID_MLNS));
            }
            return result;
        }


        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            BhQtcnKCBChiTietModel item = obj as BhQtcnKCBChiTietModel;

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
            Items.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    x.FTienDuToanNamTruocChuyenSang = 0;
                    //x.FTienDuToanGiaoNamNay = 0;
                    x.FTienTongDuToanDuocGiao = 0;
                    x.FTienThucChi = 0;
                    x.FTienThua = 0;
                    x.FTienThieu = 0;
                    x.STenDonVi = string.Empty;
                });

            Items.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi) && x.BHangCha).Select(x =>
            {
                x.FTienDuToanNamTruocChuyenSang = x.FDuToanNamTruocChuyenSang;
                return x;
            }).ToList();

            Dictionary<Guid?, BhQtcnKCBChiTietModel> dictByMlns = Items.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            List<BhQtcnKCBChiTietModel> temp = Items.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (BhQtcnKCBChiTietModel item in temp)
            {

                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }

            UpdateTotal();
        }

        private void LoadComboboxTypeShow()
        {
            TypeShowAgency = new ObservableCollection<ComboboxItem>();
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgency = TypeShowAgency.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedTypeShowAgency));
        }

        private void CalculateParent(Guid? idParent, BhQtcnKCBChiTietModel item, Dictionary<Guid?, BhQtcnKCBChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            BhQtcnKCBChiTietModel model = dictByMlns[idParent];

            model.FTienDuToanNamTruocChuyenSang = (model.FTienDuToanNamTruocChuyenSang ?? 0) + (item.FTienDuToanNamTruocChuyenSang ?? 0);
            //model.FTienDuToanGiaoNamNay = (model.FTienDuToanGiaoNamNay ?? 0) + (item.FTienDuToanGiaoNamNay ?? 0);
            model.FTienTongDuToanDuocGiao = (model.FTienTongDuToanDuocGiao ?? 0) + (item.FTienTongDuToanDuocGiao ?? 0);
            model.FTienThucChi = (model.FTienThucChi ?? 0) + (item.FTienThucChi ?? 0);
            //model.FTiLeThucHienTrenDuToan = (model.FTiLeThucHienTrenDuToan ?? 0) + (item.FTiLeThucHienTrenDuToan ?? 0);
            model.FTienThua = (model.FTienThua ?? 0) + (item.FTienThua ?? 0);
            model.FTienThieu = (model.FTienThieu ?? 0) + (item.FTienThieu ?? 0);

            CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
        }

        private void UpdateTotal()
        {
            Model.FTongTienDuToanNamTruocChuyenSang = 0;
            //Model.FTongTienDuToanGiaoNamNay = 0;
            Model.FTongTienTongDuToanDuocGiao = 0;
            Model.FTongTienThucChi = 0;
            Model.FTongTienThua = 0;
            Model.FTongTienThieu = 0;

            Model.FTongTienDuToanNamTruocChuyenSang = Items.Sum(x => x.FTienDuToanNamTruocChuyenSang ?? 0);
            //Model.FTongTienDuToanGiaoNamNay = Items.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTienDuToanGiaoNamNay ?? 0);
            Model.FTongTienDuToanGiaoNamNay = Items?.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienDuToanGiaoNamNay).FirstOrDefault();
            Model.FTongTienTongDuToanDuocGiao = Model.FTongTienDuToanNamTruocChuyenSang + Model.FTongTienDuToanGiaoNamNay;
            Model.FTongTienThucChi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienThucChi ?? 0);
            Model.FTongTienThua = Model.FTongTienTongDuToanDuocGiao > Model.FTongTienThucChi ? Model.FTongTienTongDuToanDuocGiao - Model.FTongTienThucChi : 0;
            Model.FTongTienThieu = Model.FTongTienTongDuToanDuocGiao < Model.FTongTienThucChi ? Model.FTongTienThucChi - Model.FTongTienTongDuToanDuocGiao : 0;
        }
        private void OnSaveData()
        {
            if (!IsSaveData)
            {
                return;
            }
            List<BhQtcnKCBChiTietModel> lstDataAdd = Items.Where(x => !x.BHangCha && x.Id == Guid.Empty && x.IsModified).ToList();
            List<BhQtcnKCBChiTietModel> lstDataUpdate = Items.Where(x => !x.BHangCha && x.Id != Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
            List<BhQtcnKCBChiTietModel> lstDataDelete = Items.Where(x => !x.BHangCha && x.IsDeleted && x.IsModified && x.Id != Guid.Empty).ToList();

            List<BhQtcnKCBChiTiet> addItemList = new List<BhQtcnKCBChiTiet>();
            if (lstDataAdd.Count() > 0)
            {
                lstDataAdd.ForEach(x =>
                {
                    x.INamLamViec = _sessionInfo.YearOfWork;
                    x.IIdMaDonVi = Model.IIdMaDonVi;
                    x.SNguoiTao = _sessionInfo.Principal;
                    x.DNgayTao = DateTime.Now;
                });
                _mapper.Map(lstDataAdd, addItemList);
                addItemList.Select(x => { x.Id = Guid.NewGuid(); x.IIdQTCNamKCBQuanYDonVi = Model.Id; return x; }).ToList();
                _qtcnKCBChiTietService.AddRange(addItemList);

                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }
            if (lstDataUpdate.Count() > 0)
            {
                lstDataUpdate.ForEach(x =>
                {
                    x.SNguoiSua = _sessionInfo.Principal;
                    x.DNgaySua = DateTime.Now; ;
                    x.IIdMaDonVi = Model.IIdMaDonVi;
                });
                _mapper.Map(lstDataUpdate, addItemList);
                addItemList.Select(x => { x.IIdQTCNamKCBQuanYDonVi = Model.Id; return x; }).ToList();
                _qtcnKCBChiTietService.UpdateRange(addItemList);
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            if (lstDataDelete.Count() > 0)
            {
                _mapper.Map(lstDataDelete, addItemList);
                _qtcnKCBChiTietService.RemoveRange(addItemList);
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; x.IsDeleted = false; return x; }).ToList();
            }

            //Update quyết toán chi nam BHXH

            BhQtcnKCB chungtu = _qtcnKCBService.FindById(Model.Id);
            if (chungtu != null)
            {
                chungtu.FTongTienDuToanNamTruocChuyenSang = Model.FTongTienDuToanNamTruocChuyenSang;
                chungtu.FTongTienDuToanGiaoNamNay = Items.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienDuToanGiaoNamNay ?? 0).Sum();
                chungtu.FTongTienTongDuToanDuocGiao = chungtu.FTongTienDuToanNamTruocChuyenSang + chungtu.FTongTienDuToanGiaoNamNay;
                chungtu.FTongTienThucChi = Items.Where(x => !x.BHangCha).Select(x => x.FTienThucChi ?? 0).Sum();
                chungtu.FTongTienThua = chungtu.FTongTienTongDuToanDuocGiao > chungtu.FTongTienThucChi ? chungtu.FTongTienTongDuToanDuocGiao - chungtu.FTongTienThucChi : 0;
                chungtu.FTongTienThieu = chungtu.FTongTienTongDuToanDuocGiao < chungtu.FTongTienThucChi ? chungtu.FTongTienThucChi - chungtu.FTongTienTongDuToanDuocGiao : 0;
                _qtcnKCBService.Update(chungtu);
            }


            IsSaveData = false;
            LoadData();
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
            //SavedAction?.Invoke(null);
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

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhQtcnKCBChiTietModel> results = new List<BhQtcnKCBChiTietModel>();

                List<string> lstSXaNoiMaChildSearch = Items.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.BHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                List<string> lstSXaNoiMaParentSearch = Items.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.BHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
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
                DataSearch = new ObservableCollection<BhQtcnKCBChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhQtcnKCBChiTietModel>();
            }
            _itemsView.Refresh();
        }

        private List<BhQtcnKCBChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhQtcnKCBChiTietModel> result = new List<BhQtcnKCBChiTietModel>();
            List<string> lstParent = StringUtils.GetListKyHieuParent(lstInput);
            if (!lstParent.IsEmpty() && lstParent.Any(x => x.Count() >= 3))
            {
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
            }
            List<BhQtcnKCBChiTietModel> lstData = Items.Where(x => lstParent.Contains(x.SXauNoiMa)).ToList();
            result.AddRange(lstData);
            GetListChild(lstData.Where(x => lstInput.Contains(x.SXauNoiMa)).ToList(), result);
            return result;
        }

        private void GetListChild(List<BhQtcnKCBChiTietModel> lstInput, List<BhQtcnKCBChiTietModel> results)
        {
            List<BhQtcnKCBChiTietModel> itemChild = Items.Where(x => lstInput.Select(x => x.IID_MLNS).Distinct().Contains(x.IID_MLNS_Cha)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (BhQtcnKCBChiTietModel item in itemChild.Where(x => Items.Select(y => y.IID_MLNS_Cha).Distinct().Contains(x.IID_MLNS ?? Guid.Empty)))
                {
                    GetListChild(new List<BhQtcnKCBChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

    }
}
