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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.Explanation;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.Explanation;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.PritnReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB
{
    public class QuyetToanChiQuyKCBDetailViewModel : DetailViewModelBase<BhQtcqKCBModel, BhQtcqKCBChiTietModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IQtcqKCBService _qtcqKCBService;
        private readonly IQtcqKCBChiTietService _qtcqKCBChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly INsDonViService _nsDonViService;

        private ICollectionView ItemsView;
        private readonly ILog _logger;

        private SessionInfo _sessionInfo;
        private readonly bool _isCapPhatToanDonVi;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public override Type ContentType => typeof(QuyetToanChiQuyKCBDetail);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;
        public bool IsSaveData;
        public bool IsDelete => (_selectedTypeShowAgency == null || _selectedTypeShowAgency.ValueItem != TypeDisplay.TONG_DONVI) && (SelectedItem != null);
        public bool IsDeleteAll => Items.Any(item => !item.IsModified);
        public bool IsReadOnlyGrid => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI);
        public bool IsTongHop => !string.IsNullOrEmpty(Model.SDSSoChungTuTongHop);
        public bool IsShowAgencyFilter => IsTongHop && _selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI;
        private ICollection<BhQtcqKCBChiTietModel> _filterResult = new HashSet<BhQtcqKCBChiTietModel>();
        public Visibility VisibleColAgency => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI) ?
            Visibility.Collapsed : Visibility.Visible;

        public Visibility VisibleVoucherNo => !string.IsNullOrEmpty(Model.SDSSoChungTuTongHop) && VisibleColAgency == Visibility.Visible ? Visibility.Visible : Visibility.Collapsed;

        public bool ReadOnlyCapPhat => IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI;
        public bool ReadOnlyDeNghi => IsTongHop;
        public bool IsEditByRole => Model.SNguoiTao == _sessionInfo.Principal;

        private string xnmConcatenation = "";
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

        //Xác định lần đầu tiên tạo mới
        public bool IsCreate;

        public int NamLamViec { get; set; }

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

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private ObservableCollection<BhQtcqKCBChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhQtcqKCBChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhQtcqKCBChiTietModel _selectedPopupItem;
        public BhQtcqKCBChiTietModel SelectedPopupItem
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

        private ObservableCollection<BhQtcqKCBChiTietModel> _dataSearch;
        public ObservableCollection<BhQtcqKCBChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }


        public RelayCommand SaveCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }

        public RelayCommand RefreshAllDataCommand { get; }
        public RelayCommand AutoFillDataCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand GiaiThichBangLoiCommand { get; }

        public PrintQuyetToanChiQuyKCBViewModel PrintQuyetToanChiQuyKCBViewModel { get; set; }
        public QuyetToanChiGiaiThichBangLoiQuyKCBViewModel QuyetToanChiGiaiThichBangLoiQuyKCBViewModel { get; set; }
        public PrintQuyetToanChiQuyKCBTongHopChiViewModel PrintQuyetToanChiQuyKCBTongHopChiViewModel { get; }

        public QuyetToanChiQuyKCBDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            IQtcqKCBService qtcqKCBService,
            IQtcqKCBChiTietService qtcqKCBChiTietService,
            IDanhMucService danhMucService,
            INsDonViService nsDonViService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            PrintQuyetToanChiQuyKCBViewModel printQuyetToanChiQuyKCBViewModel,
            PrintQuyetToanChiQuyKCBTongHopChiViewModel printQuyetToanChiQuyKCBTongHopChiViewModel,
            QuyetToanChiGiaiThichBangLoiQuyKCBViewModel quyetToanChiGiaiThichBangLoiQuyKCBViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _qtcqKCBService = qtcqKCBService;
            _nsDonViService = nsDonViService;
            _qtcqKCBChiTietService = qtcqKCBChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            PrintQuyetToanChiQuyKCBViewModel = printQuyetToanChiQuyKCBViewModel;
            QuyetToanChiGiaiThichBangLoiQuyKCBViewModel = quyetToanChiGiaiThichBangLoiQuyKCBViewModel;
            PrintQuyetToanChiQuyKCBTongHopChiViewModel = printQuyetToanChiQuyKCBTongHopChiViewModel;

            PrintCommand = new RelayCommand(OnPrintDetail);
            SaveCommand = new RelayCommand(obj => OnSaveData());
            RefreshAllDataCommand = new RelayCommand(obj => OnRefreshAllData());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SearchCommand = new RelayCommand(OnSearch);
            GiaiThichBangLoiCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());
        }

        public override void Init()
        {
            try
            {
                MarginRequirement = new System.Windows.Thickness(10);
                _isShowColumnUnit = false;
                _sessionInfo = _sessionService.Current;
                NamLamViec = _sessionService.Current.YearOfWork;
                IsSummaryVoucher = false;
                IsShowTypeAgency = false;
                LoadTypeShow();
                if (Model != null && !string.IsNullOrEmpty(Model.SDSSoChungTuTongHop))
                {
                    LoadComboboxTypeShow();
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
                _selectedAgency = null;
                LoadData();
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

        private void OnOpenVerbalExplanationDialog()
        {
            QuyetToanChiGiaiThichBangLoiQuyKCBViewModel.BhQtcqKCBModel = Model;
            QuyetToanChiGiaiThichBangLoiQuyKCBViewModel.Init();
            VerbalExplanation view = new VerbalExplanation { DataContext = QuyetToanChiGiaiThichBangLoiQuyKCBViewModel };
            view.ShowDialog();
        }


        private void SearchDataParent()
        {
            if (ItemsView != null)
            {
                ItemsView.Refresh();
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsDelete));
        }

        private void OnPrintDetail(object obj)
        {
            int dialogType = (int)obj;
            switch (dialogType)
            {
                case (int)BhQuyetToanChiQuyKCBType.PRINT_BAOCAOKCBQUANYDONVI:
                case (int)BhQuyetToanChiQuyKCBType.PRINT_BAOCAOTONGHOPCCACDONVI:
                case (int)BhQuyetToanChiQuyKCBType.PRINT_THONGTRIQUYETTOANCHIKINHPHIKCB:
                    PrintQuyetToanChiQuyKCBViewModel.SettlementTypeValue = dialogType;
                    PrintQuyetToanChiQuyKCBViewModel.Init();
                    PrintQuyetToanChiQuyKCB view1 = new PrintQuyetToanChiQuyKCB
                    {
                        DataContext = PrintQuyetToanChiQuyKCBViewModel
                    };
                    DialogHost.Show(view1, SettlementScreen.DETAIL_DIALOG, null, null);
                    break;
                case (int)BhQuyetToanChiQuyKCBType.PRINT_BAOCAOTONGHOPCHI:
                    PrintQuyetToanChiQuyKCBTongHopChiViewModel.Init();
                    var view2 = new PrintQuyetToanChiQuyKCBTongHopChi
                    {
                        DataContext = PrintQuyetToanChiQuyKCBTongHopChiViewModel
                    };
                    DialogHost.Show(view2, SettlementScreen.DETAIL_DIALOG, null, null);
                    break;
            }
        }

        private void LoadComboboxTypeShow()
        {
            TypeShowAgency = new ObservableCollection<ComboboxItem>();
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgency = TypeShowAgency.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedTypeShowAgency));
        }

        private void LoadAgencies(string agencyIds)
        {
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            OnPropertyChanged(nameof(Agencies));
        }

        public override void LoadData(params object[] args)
        {
            int iNamLamViec = _sessionInfo.YearOfWork;
            List<BhQtcqKCBChiTietQuery> listDataQuery = new List<BhQtcqKCBChiTietQuery>();
            List<BhQtcqKCBChiTietQuery> listDataQueryDuToan = new List<BhQtcqKCBChiTietQuery>();

            List<BhDanhMucLoaiChi> danhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(iNamLamViec).ToList();
            BhDanhMucLoaiChi loaiChi = danhMucLoaiChi.Where(x => x.SLNS.Equals(LNSValue.LNS_9010004_9010005)).FirstOrDefault();
            bool existChungTuChiTiet = _qtcqKCBChiTietService.ExitChungTuChiTiet(Model.Id);


            if (IsSummaryVoucher && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI && _selectedAgency == null)
            {
                List<string> voucherNos = Model.SDSSoChungTuTongHop.Split(",").ToList();
                List<BhQtcqKCB> listChungTu = _qtcqKCBService.FindByAggregateVoucher(voucherNos, _sessionInfo.YearOfWork).ToList();
                string agencyIds = string.Join(",", listChungTu.Select(x => x.IIdMaDonVi));
                IEnumerable<DonVi> listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
                List<BhQtcqKCBChiTietQuery> listChungTuChiTietParent = new List<BhQtcqKCBChiTietQuery>();
                List<BhQtcqKCBChiTietQuery> listChungTuChiTietChildren = new List<BhQtcqKCBChiTietQuery>();

                foreach (BhQtcqKCB chungTu in listChungTu)
                {
                    List<BhQtcqKCBChiTietQuery> listQuery = _qtcqKCBChiTietService.GetChiTietQuyetToanChiQuyKCB(chungTu.Id, loaiChi.Id, loaiChi.SLNS
                                                                                                            , loaiChi.SMaLoaiChi, chungTu.IIdMaDonVi, chungTu.DNgayChungTu
                                                                                                            , chungTu.IQuyChungTu, iNamLamViec, chungTu.ILoaiTongHop).ToList().ToList();
                    listQuery.Where(x => !x.BHangCha).Select(x => x.STenDonVi = listDonVi.FirstOrDefault(y => y.IIDMaDonVi == chungTu.IIdMaDonVi).TenDonVi).ToList();
                    /*
                    var listQueryDV = from x in listQuery.Where(x => !x.BHangCha)
                                      join y in listDonVi on x.IIDMaDonVi equals y.IIDMaDonVi
                                      select new
                                      {
                                          Data = x,
                                          STenDonVi = y.TenDonVi
                                      };
                    listQueryDV.ForAll(x => x.Data.IIDMaDonVi = x.STenDonVi);
                    */

                    listChungTuChiTietParent.AddRange(listQuery.Where(x => x.BHangCha));
                    listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.BHangCha));
                }
                List<string> listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                var temp = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x =>
                    new
                    {
                        Data = x.FirstOrDefault(),
                        FTienDuToanGiaoNamNay = x.Sum(x => x.FTienDuToanGiaoNamNay)
                    }
                ).ToList();
                temp.ForEach(x =>
                {
                    x.Data.FTienDuToanGiaoNamNay = x.FTienDuToanGiaoNamNay;
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
                    int LoaiTongHop = !IsDonViRoot(_selectedAgency.ValueItem) ? BhxhLoaiChungTu.BhxhChungTu : BhxhLoaiChungTu.BhxhChungTuTongHop;
                    Guid idChungTu = Guid.Empty;
                    System.Linq.Expressions.Expression<Func<BhQtcqKCB, bool>> predicateCtDv = PredicateBuilder.True<BhQtcqKCB>();
                    predicateCtDv = predicateCtDv.And(x => x.IIdMaDonVi == _selectedAgency.ValueItem);
                    predicateCtDv = predicateCtDv.And(x => x.IQuyChungTu == Model.IQuyChungTu);
                    predicateCtDv = predicateCtDv.And(x => x.INamChungTu == Model.INamChungTu);
                    BhQtcqKCB ctDonVi = _qtcqKCBService.FindByCondition(predicateCtDv).FirstOrDefault();
                    if (ctDonVi != null)
                        idChungTu = ctDonVi.Id;

                    listDataQuery = _qtcqKCBChiTietService.GetChiTietQuyetToanChiQuyKCB(idChungTu, loaiChi.Id,
                                                                                loaiChi.SLNS, loaiChi.SMaLoaiChi, sMaDonVi,
                                                                                Model.DNgayChungTu, Model.IQuyChungTu, iNamLamViec,
                                                                                LoaiTongHop).ToList();
                }
                else
                {
                    listDataQuery = _qtcqKCBChiTietService.GetChiTietQuyetToanChiQuyKCB(Model.Id, loaiChi.Id,
                                                                                loaiChi.SLNS, loaiChi.SMaLoaiChi, Model.IIdMaDonVi,
                                                                                Model.DNgayChungTu, Model.IQuyChungTu, iNamLamViec,
                                                                                Model.ILoaiTongHop).ToList();
                }

            }
            listDataQuery.ForEach(x =>
            {
                if (x.BHangCha)
                    x.STenDonVi = string.Empty;
            });
            listDataQuery = listDataQuery.OrderBy(x => x.SXauNoiMa).ToList();
            Items = _mapper.Map<ObservableCollection<BhQtcqKCBChiTietModel>>(listDataQuery);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhQtcqKCBChiTietModel>>(listDataQuery);
            ItemsView = CollectionViewSource.GetDefaultView(Items);
            ItemsView.Filter = ItemsViewFilter;
            CalculateData();
            foreach (BhQtcqKCBChiTietModel bhQtcnBHYTChiTietModel in Items)
            {
                bhQtcnBHYTChiTietModel.IsFilter = true;
                if (!bhQtcnBHYTChiTietModel.BHangCha)
                {
                    bhQtcnBHYTChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        BhQtcqKCBChiTietModel item = (BhQtcqKCBChiTietModel)sender;
                        //if (args.PropertyName.ToLower().StartsWith("f"))
                        //{
                        //    item.IsModified = true;
                        //    bhQtcnBHYTChiTietModel.IsModified = true;

                        //}
                        item.IsModified = true;
                        bhQtcnBHYTChiTietModel.IsModified = true;
                        if (args.PropertyName == nameof(SelectedItem.FTienDuToanNamTruocChuyenSang) || args.PropertyName == nameof(SelectedItem.FTienDuToanGiaoNamNay)
                        || args.PropertyName == nameof(SelectedItem.FTienTongDuToanDuocGiao) || args.PropertyName == nameof(SelectedItem.FTienThucChi)
                        || args.PropertyName == nameof(SelectedItem.FTienQuyetToanDaDuyet) || args.PropertyName == nameof(SelectedItem.FTienDeNghiQuyetToanQuyNay)
                        || args.PropertyName == nameof(SelectedItem.FTienXacNhanQuyetToanQuyNay))
                        {
                            item.FTienTongDuToanDuocGiao = (item.FTienDuToanNamTruocChuyenSang ?? 0) + (item.FTienDuToanGiaoNamNay ?? 0);
                            //item.FTienXacNhanQuyetToanQuyNay = item.FTienDeNghiQuyetToanQuyNay ?? 0;
                            CalculateData();
                        }
                        IsSaveData = true;
                        OnPropertyChanged(nameof(IsSaveData));
                        //OnPropertyChanged(nameof(IsOpenPrintPopup));

                    };
                }
            }
        }

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        private bool ItemsViewFilter(object obj)
        {
            //bool result = true;
            //var item = (BhQtcqKCBChiTietModel)obj;
            //if (!string.IsNullOrEmpty(SNoiDungSearch))
            //{
            //    result = DataSearch.Any(x => x.IID_MLNS.Equals(item.IID_MLNS));
            //}
            //return result;

            bool result = true;
            BhQtcqKCBChiTietModel item = (BhQtcqKCBChiTietModel)obj;
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

        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            BhQtcqKCBChiTietModel item = (BhQtcqKCBChiTietModel)obj;

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
                    //x.FTienDuToanNamTruocChuyenSang = 0;
                    //x.FTienDuToanGiaoNamNay = 0;
                    x.FTienTongDuToanDuocGiao = 0;
                    x.FTienThucChi = 0;
                    x.FTienQuyetToanDaDuyet = 0;
                    x.FTienDeNghiQuyetToanQuyNay = 0;
                    x.FTienXacNhanQuyetToanQuyNay = 0;

                });
            Dictionary<Guid?, BhQtcqKCBChiTietModel> dictByMlns = Items.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            List<BhQtcqKCBChiTietModel> temp = Items.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (BhQtcqKCBChiTietModel item in temp)
            {

                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }

            UpdateTotal();
        }

        private void CalculateParent(Guid? idParent, BhQtcqKCBChiTietModel item, Dictionary<Guid?, BhQtcqKCBChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            BhQtcqKCBChiTietModel model = dictByMlns[idParent];

            //model.FTienDuToanNamTruocChuyenSang = (model.FTienDuToanNamTruocChuyenSang ?? 0) + (item.FTienDuToanNamTruocChuyenSang ?? 0);
            //model.FTienDuToanGiaoNamNay = (model.FTienDuToanGiaoNamNay ?? 0) + (item.FTienDuToanGiaoNamNay ?? 0);
            model.FTienTongDuToanDuocGiao = (model.FTienTongDuToanDuocGiao ?? 0) + (item.FTienTongDuToanDuocGiao ?? 0);
            model.FTienThucChi = (model.FTienThucChi ?? 0) + (item.FTienThucChi ?? 0);
            model.FTienQuyetToanDaDuyet = (model.FTienQuyetToanDaDuyet ?? 0) + (item.FTienQuyetToanDaDuyet ?? 0);
            model.FTienDeNghiQuyetToanQuyNay = (model.FTienDeNghiQuyetToanQuyNay ?? 0) + (item.FTienDeNghiQuyetToanQuyNay ?? 0);
            model.FTienXacNhanQuyetToanQuyNay = (model.FTienXacNhanQuyetToanQuyNay ?? 0) + (item.FTienXacNhanQuyetToanQuyNay ?? 0);

            CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
        }

        private void UpdateTotal()
        {
            Model.FTongTienDuToanNamTruocChuyenSang = Items?.Where(x => x.SXauNoiMa == LNSValue.LNS_9010004_9010005).Select(x => x.FTienDuToanNamTruocChuyenSang).FirstOrDefault();
            Model.FTongTienDuToanGiaoNamNay = Items?.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienDuToanGiaoNamNay).FirstOrDefault();
            Model.FTongTienTongDuToanDuocGiao = 0;
            Model.FTongTienThucChi = 0;
            Model.FTongTienQuyetToanDaDuyet = 0;
            Model.FTongTienDeNghiQuyetToanQuyNay = 0;
            Model.FTongTienXacNhanQuyetToanQuyNay = 0;

            Model.FTongTienTongDuToanDuocGiao = (Model.FTongTienDuToanNamTruocChuyenSang ?? 0) + (Model.FTongTienDuToanGiaoNamNay ?? 0);
            //Model.FTongTienDuToanGiaoNamNay = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienDuToanGiaoNamNay ?? 0);
            //Model.FTongTienTongDuToanDuocGiao = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienTongDuToanDuocGiao ?? 0);
            Model.FTongTienThucChi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienThucChi ?? 0);
            Model.FTongTienQuyetToanDaDuyet = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienQuyetToanDaDuyet ?? 0);
            Model.FTongTienDeNghiQuyetToanQuyNay = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienDeNghiQuyetToanQuyNay ?? 0);
            Model.FTongTienXacNhanQuyetToanQuyNay = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienXacNhanQuyetToanQuyNay ?? 0);

        }
        private void OnSaveData()
        {
            if (!IsSaveData)
            {
                return;
            }
            List<BhQtcqKCBChiTietModel> lstDataAdd = Items.Where(x => !x.BHangCha && x.Id == Guid.Empty && x.IsModified).ToList();
            //var lstDataDuToan = Items.Where(x => x.Id == Guid.Empty && x.IsModified && !string.IsNullOrEmpty(x.SDuToanChiTietToi)).ToList();
            //lstDataAdd.AddRange(lstDataDuToan);
            List<BhQtcqKCBChiTietModel> lstDataUpdate = Items.Where(x => !x.BHangCha && x.Id != Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
            //if (Model.IQuyChungTu > SettlementTypeQuy.Quy)
            //{
            //    var lstDataUpdateDuToan = Items.Where(x => x.IsModified && !string.IsNullOrEmpty(x.SDuToanChiTietToi));
            //    lstDataUpdate.AddRange(lstDataUpdateDuToan);
            //}
            List<BhQtcqKCBChiTietModel> lstDataDelete = Items.Where(x => !x.BHangCha && x.IsDeleted && x.IsModified && x.Id != Guid.Empty).ToList();

            List<BhQtcqKCBChiTiet> addItemList = new List<BhQtcqKCBChiTiet>();
            if (lstDataAdd.Count() > 0)
            {
                _mapper.Map(lstDataAdd, addItemList);
                addItemList.Select(x => { x.Id = Guid.NewGuid(); x.IIdQTCQuyKCB = Model.Id; x.DNgayTao = DateTime.Now; x.DNgaySua = null; x.INamLamViec = _sessionInfo.YearOfWork; return x; }).ToList();
                _qtcqKCBChiTietService.AddRange(addItemList);

                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }
            if (lstDataUpdate.Count() > 0)
            {
                _mapper.Map(lstDataUpdate, addItemList);
                addItemList.Select(x => { x.IIdQTCQuyKCB = Model.Id; x.DNgaySua = DateTime.Now; x.INamLamViec = _sessionInfo.YearOfWork; x.SNguoiSua = _sessionInfo.Principal; return x; }).ToList();
                _qtcqKCBChiTietService.UpdateRange(addItemList);
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            if (lstDataDelete.Count() > 0)
            {
                _mapper.Map(lstDataDelete, addItemList);
                _qtcqKCBChiTietService.RemoveRange(addItemList);
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; x.IsDeleted = false; return x; }).ToList();
            }

            //Update quyết toán chi nam BHXH

            BhQtcqKCB chungtu = _qtcqKCBService.FindById(Model.Id);
            if (chungtu != null)
            {
                chungtu.FTongTienDuToanNamTruocChuyenSang = Items.Where(x => x.SXauNoiMa == LNSValue.LNS_9010004_9010005).Select(x => x.FTienDuToanNamTruocChuyenSang).Sum();
                chungtu.FTongTienDuToanGiaoNamNay = Items.Where(x => !x.BHangCha).Select(x => x.FTienDuToanGiaoNamNay ?? 0).Sum();
                chungtu.FTongTienTongDuToanDuocGiao = Items.Where(x => !x.BHangCha).Select(x => x.FTienTongDuToanDuocGiao).Sum();
                chungtu.FTongTienThucChi = Items.Where(x => !x.BHangCha).Select(x => x.FTienThucChi ?? 0).Sum();
                chungtu.FTongTienQuyetToanDaDuyet = Items.Where(x => !x.BHangCha).Select(x => x.FTienQuyetToanDaDuyet).Sum();
                chungtu.FTongTienDeNghiQuyetToanQuyNay = Items.Where(x => !x.BHangCha).Select(x => x.FTienDeNghiQuyetToanQuyNay ?? 0).Sum();
                chungtu.FTongTienXacNhanQuyetToanQuyNay = Items.Where(x => !x.BHangCha).Select(x => x.FTienXacNhanQuyetToanQuyNay).Sum();

                _qtcqKCBService.Update(chungtu);
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

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.BHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
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
                ItemsView.Refresh();
            }
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhQtcqKCBChiTietModel> results = new List<BhQtcqKCBChiTietModel>();

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
                DataSearch = new ObservableCollection<BhQtcqKCBChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhQtcqKCBChiTietModel>();
            }
            ItemsView.Refresh();
        }

        private List<BhQtcqKCBChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhQtcqKCBChiTietModel> result = new List<BhQtcqKCBChiTietModel>();
            List<string> lstParent = StringUtils.GetListKyHieuParent(lstInput);
            if (!lstParent.IsEmpty() && lstParent.Any(x => x.Count() >= 3))
            {
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
            }
            List<BhQtcqKCBChiTietModel> lstData = Items.Where(x => lstParent.Contains(x.SXauNoiMa)).ToList();
            result.AddRange(lstData);
            GetListChild(lstData.Where(x => lstInput.Contains(x.SXauNoiMa)).ToList(), result);
            return result;
        }

        private void GetListChild(List<BhQtcqKCBChiTietModel> lstInput, List<BhQtcqKCBChiTietModel> results)
        {
            List<BhQtcqKCBChiTietModel> itemChild = Items.Where(x => lstInput.Select(x => x.IID_MLNS).Distinct().Contains(x.IID_MLNS_Cha ?? Guid.Empty)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (BhQtcqKCBChiTietModel item in itemChild.Where(x => Items.Select(y => y.IID_MLNS_Cha).Distinct().Contains(x.IID_MLNS)))
                {
                    GetListChild(new List<BhQtcqKCBChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

    }
}
