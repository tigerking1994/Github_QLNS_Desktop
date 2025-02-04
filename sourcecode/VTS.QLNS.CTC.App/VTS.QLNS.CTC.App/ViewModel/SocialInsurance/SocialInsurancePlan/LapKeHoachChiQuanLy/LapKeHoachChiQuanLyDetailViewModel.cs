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
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy
{
    public class LapKeHoachChiQuanLyDetailViewModel : DetailViewModelBase<BhKhcKinhphiQuanlyModel, BhKhcKinhphiQuanlyChiTietModel>
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private readonly IBhKhcKinhphiQuanlyChiTietService _bhKhcKinhphiQuanlyChiTietService;
        private readonly IBhKhcKinhphiQuanlyService _bhKhcKinhphiQuanlyService;
        private readonly INsDonViService _iNsDonViService;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;
        private ICollection<BhKhcKinhphiQuanlyChiTietModel> _lstFilterResult = new HashSet<BhKhcKinhphiQuanlyChiTietModel>();
        private ICollectionView _khcKPQLChiTietModelsView { get; set; }
        #endregion

        #region Property
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public bool IsOpenPrintPopup = true;
        private string xnmConcatenation = "";
        public bool IsAggregate => !string.IsNullOrEmpty(Model.STongHop);
        public bool IsInit { get; set; }
        public bool IsVoucherSummary { get; set; }
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
                if (SetProperty(ref _typeShowsSelected, value) && _khcKPQLChiTietModelsView != null)
                {
                    OnRefresh();
                    _khcKPQLChiTietModelsView.Refresh();
                }
            }
        }

        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted)
                        || Items.Any(x => !x.IsHangCha);
        private ObservableCollection<ComboboxItem> _viewSummary = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ViewSummary
        {
            get => _viewSummary;
            set => SetProperty(ref _viewSummary, value);
        }
        private ComboboxItem _viewSummarySelected;
        public ComboboxItem ViewSummarySelected
        {
            get => _viewSummarySelected;
            set
            {
                SetProperty(ref _viewSummarySelected, value);
                LoadData();
            }
        }
        private ObservableCollection<DonViModel> _donViItems;
        public ObservableCollection<DonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }
        public string HeaderSoDaThucHienNam => "Số đã thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderUocThucHienNam => "Ước thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderKehoachThucHienNam => "Kế hoạch thực hiện năm " + (_sessionService.Current.YearOfWork);
        public bool IsChungTuTongHop => Model != null;
        public bool IsAnotherUserCreate { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public override Type ContentType => typeof(LapKeHoachChiDetail);
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
                    BeForeRefresh();
                }

                _khcKPQLChiTietModelsView.Refresh();
            }
        }
        private ObservableCollection<ComboboxItem> _typeDisplays;
        public ObservableCollection<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private ComboboxItem _typeDisplaysSelected;
        public ComboboxItem TypeDisplaysSelected
        {
            get => _typeDisplaysSelected;
            set
            {
                if (SetProperty(ref _typeDisplaysSelected, value) && _khcKPQLChiTietModelsView != null)
                {
                    if (_typeDisplaysSelected != null && _typeDisplaysSelected.ValueItem == TypeDisplay.CHITIET_DONVI)
                        _isShowColumnUnit = true;
                    else _isShowColumnUnit = false;
                    LoadData();
                    OnPropertyChanged(nameof(IsShowColumnUnit));
                    OnPropertyChanged(nameof(IsShowAgencyFilter));
                }
            }
        }
        public bool IsShowAgencyFilter => IsVoucherSummary && TypeDisplaysSelected != null && TypeDisplaysSelected.ValueItem == TypeDisplay.CHITIET_DONVI;
        private bool _isShowColumnUnit;
        public bool IsShowColumnUnit
        {
            get => _isShowColumnUnit;
            set => SetProperty(ref _isShowColumnUnit, value);
        }

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private string _sM;
        public string SM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }

        private string _sTM;
        public string STM
        {
            get => _sTM;
            set => SetProperty(ref _sTM, value);
        }

        private ObservableCollection<BhKhcKinhphiQuanlyChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhKhcKinhphiQuanlyChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhKhcKinhphiQuanlyChiTietModel _selectedPopupItem;
        public BhKhcKinhphiQuanlyChiTietModel SelectedPopupItem
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

        private ObservableCollection<BhKhcKinhphiQuanlyChiTietModel> _dataSearch;
        public ObservableCollection<BhKhcKinhphiQuanlyChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }
        #endregion

        #region RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        #endregion

        #region ViewModel
        PrintReportQuanLyKinhPhiViewModel PrintReportQuanLyKinhPhiViewModel { get; set; }
        #endregion

        #region Constructor
        public LapKeHoachChiQuanLyDetailViewModel(
            IBhKhcKinhphiQuanlyService bhKhcKinhphiQuanlyService,
            IBhKhcKinhphiQuanlyChiTietService bhKhcKinhphiQuanlyChiTietService,
            IMapper mapper,
            ILog loger,
            INsDonViService nsDonViService,
            ISessionService sessionService,
            PrintReportQuanLyKinhPhiViewModel printReportQuanLyKinhPhiViewModel
            )
        {
            _bhKhcKinhphiQuanlyChiTietService = bhKhcKinhphiQuanlyChiTietService;
            _bhKhcKinhphiQuanlyService = bhKhcKinhphiQuanlyService;
            _iNsDonViService = nsDonViService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = loger;

            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(OnClose);
            PrintCommand = new RelayCommand(obj => OnPrintDetal(obj));
            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(OnClearSearch);

            PrintReportQuanLyKinhPhiViewModel = printReportQuanLyKinhPhiViewModel;
        }
        #endregion

        #region Init
        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            IsInit = true;
            LoadTypeDisplay();
            LoadTypeShow();
            LoadData();
            IsInit = false;
            LoadDefault();
        }
        #endregion

        #region Load data
        public override void LoadData(params object[] args)
        {
            try
            {
                List<BhKhcKinhphiQuanlyChiTiet> listChungTuChiTiet = new List<BhKhcKinhphiQuanlyChiTiet>();
                Items = new ObservableCollection<BhKhcKinhphiQuanlyChiTietModel>();
                KhcQuanlyKinhphiChiTietCriteria searchCondition = new KhcQuanlyKinhphiChiTietCriteria();
                searchCondition.NamLamViec = _sessionService.Current.YearOfWork;

                if (IsVoucherSummary && TypeDisplaysSelected != null && TypeDisplaysSelected.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    List<string> soChungTus = Model.STongHop.Split(",").ToList();
                    var predicate = PredicateBuilder.True<BhKhcKinhphiQuanly>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && soChungTus.Contains(x.SSoChungTu));
                    List<BhKhcKinhphiQuanly> lstChungTu = _bhKhcKinhphiQuanlyService.FindByCondition(predicate).ToList();
                    List<BhKhcKinhphiQuanlyChiTiet> listChungTuChiTietParent = new List<BhKhcKinhphiQuanlyChiTiet>();
                    List<BhKhcKinhphiQuanlyChiTiet> listChungTuChiTietChildren = new List<BhKhcKinhphiQuanlyChiTiet>();
                    foreach (var chungTu in lstChungTu)
                    {
                        searchCondition.KhcKinhphiQuanlyId = chungTu.Id;
                        searchCondition.IdDonVi = chungTu.IID_MaDonVi;
                        var lstQuery = _bhKhcKinhphiQuanlyChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
                        listChungTuChiTietParent.AddRange(lstQuery.Where(x => x.IsHangCha));
                        listChungTuChiTietChildren.AddRange(lstQuery.Where(x => !x.IsHangCha));
                    }

                    var listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                    listChungTuChiTietParent = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa)))
                                                                        .GroupBy(x => x.SXauNoiMa).Select(x => x.First()).Distinct()
                                                                        .ToList();
                    listChungTuChiTiet.AddRange(listChungTuChiTietParent);
                    listChungTuChiTiet.AddRange(listChungTuChiTietChildren);
                    listChungTuChiTiet = listChungTuChiTiet.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IIdMaDonVi).ToList();
                    string agencyIds = string.Join(",", lstChungTu.Select(x => x.IID_MaDonVi));
                    LoadAgencies(agencyIds);

                    listChungTuChiTiet.Where(x => x.IsHangCha).ForAll
                    (x =>
                    {
                        x.STenDonVi = string.Empty;
                    });
                }
                else
                {
                    searchCondition.IdDonVi = Model.IID_MaDonVi;
                    searchCondition.KhcKinhphiQuanlyId = Model.Id.IsNullOrEmpty() ? Model.IID_BH_KHC_KinhPhiQuanLy : Model.Id;
                    listChungTuChiTiet = _bhKhcKinhphiQuanlyChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
                }

                var existBhChiTiet = _bhKhcKinhphiQuanlyChiTietService.ExistKhcKinhphiQuanlyChiTiet(Model.Id);

                foreach (var item in listChungTuChiTiet)
                {
                    item.IsAuToFillTuChi = !existBhChiTiet;
                }

                Items = _mapper.Map<ObservableCollection<BhKhcKinhphiQuanlyChiTietModel>>(listChungTuChiTiet);
                CalculateData();
                DataPopupSearchItems = _mapper.Map<ObservableCollection<BhKhcKinhphiQuanlyChiTietModel>>(Items);
                _khcKPQLChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
                _khcKPQLChiTietModelsView.Filter = ItemsViewFilter;

                foreach (var khcBhxhChiTietModel in Items)
                {
                    khcBhxhChiTietModel.IsFilter = true;
                    if (!khcBhxhChiTietModel.IsHangCha)
                    {
                        khcBhxhChiTietModel.PropertyChanged += (sender, args) =>
                        {
                            BhKhcKinhphiQuanlyChiTietModel item = (BhKhcKinhphiQuanlyChiTietModel)sender;
                            item.IsModified = true;
                            CalculateData();
                            khcBhxhChiTietModel.IsModified = true;
                            OnPropertyChanged(nameof(IsSaveData));
                            OnPropertyChanged(nameof(IsOpenPrintPopup));
                        };

                    }
                }


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

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhKhcKinhphiQuanlyChiTietModel)obj;
            result = VoucherDetailFilter(item);
            if (!result && item.IsHangCha)
            {
                result = xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (result)
                item.IsFilter = result;
            if (!IsDefault)
            {
                result = DataSearch.Any(x => x.Id.Equals(item.Id));

            }
            return result;
        }

        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            var item = (BhKhcKinhphiQuanlyChiTietModel)obj;
            if (TypeShowsSelected != null)
            {
                if (TypeShowsSelected.ValueItem == TypeDisplay.CO_SO_LIEU)
                    result = result && (item.FTienUocThucHienNamTruoc.GetValueOrDefault(0) != 0 || item.FTienKeHoachThucHienNamNay.GetValueOrDefault(0) != 0);
                else if (TypeShowsSelected.ValueItem == TypeDisplay.CHUA_CO_SO_LIEU)
                    result = result && !item.IsHasData;
            }

            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIDMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;
        }

        private void LoadAgencies(string agencyIds)
        {
            var listDonVi = _iNsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            OnPropertyChanged(nameof(Agencies));
        }

        private void BeForeRefresh()
        {
            _lstFilterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();

            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsOpenPrintPopup));
            xnmConcatenation = string.Join(";", _lstFilterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.FTienDaThucHienNamTruoc = 0;
                    x.FTienUocThucHienNamTruoc = 0;
                    x.FTienKeHoachThucHienNamNay = 0;
                    x.FTienCanBo = 0;
                    x.FTienQuanLuc = 0;
                    x.FTienTaiChinh = 0;
                    x.FTienQuanY = 0;
                });

            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }

            UpdateTotal(temp);
        }

        private void UpdateTotal(List<BhKhcKinhphiQuanlyChiTietModel> listChildren)
        {
            Model.FTongTienDaThucHienNamTruoc = 0;
            Model.FTongTienUocThucHienNamTruoc = 0;
            Model.FTongTienKeHoachThucHienNamNay = 0;

            Model.FTongTienCanBo = 0;
            Model.FTongTienQuanLuc = 0;

            Model.FTongTienTaiChinh = 0;
            Model.FTongTienQuanY = 0;


            var roots = Items.Where(t => t.IdParent.Equals(Guid.Empty)).ToList();

            foreach (var item in roots)
            {
                Model.FTongTienDaThucHienNamTruoc += item.FTienDaThucHienNamTruoc;
                Model.FTongTienUocThucHienNamTruoc += item.FTienUocThucHienNamTruoc;
                Model.FTongTienKeHoachThucHienNamNay += item.FTienKeHoachThucHienNamNay;
                Model.FTongTienCanBo += item.FTienCanBo;
                Model.FTongTienQuanLuc += item.FTienQuanLuc;
                Model.FTongTienTaiChinh += item.FTienTaiChinh;
                Model.FTongTienQuanY += item.FTienQuanY;

            }
        }

        private void CalculateParent(Guid idParent, BhKhcKinhphiQuanlyChiTietModel item, Dictionary<Guid?, BhKhcKinhphiQuanlyChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            model.FTienDaThucHienNamTruoc += item.FTienDaThucHienNamTruoc;
            model.FTienUocThucHienNamTruoc += item.FTienUocThucHienNamTruoc;
            model.FTienKeHoachThucHienNamNay += item.FTienKeHoachThucHienNamNay;
            model.FTienCanBo += item.FTienCanBo;
            model.FTienQuanLuc += item.FTienQuanLuc;
            model.FTienTaiChinh += item.FTienTaiChinh;
            model.FTienQuanY += item.FTienQuanY;

            CalculateParent(model.IdParent, item, dictByMlns);
        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            TypeDisplaysSelected = TypeDisplays.FirstOrDefault();
            OnPropertyChanged(nameof(TypeDisplaysSelected));
        }

        protected override void OnRefresh()
        {
            IsInit = true;
            LoadTypeDisplay();
            LoadData();
            IsInit = false;
        }
        #endregion

        #region Print 
        private void OnPrintDetal(object obj)
        {
            var KhcQLKPCheckPrintType = (KhcQLKPCheckPrintType)((int)obj);
            object content;
            switch (KhcQLKPCheckPrintType)
            {
                case KhcQLKPCheckPrintType.KHCQLKPCT:
                    PrintReportQuanLyKinhPhiViewModel.KhcQLKPCheckPrintType = KhcQLKPCheckPrintType;
                    PrintReportQuanLyKinhPhiViewModel.IsSummary = false;
                    PrintReportQuanLyKinhPhiViewModel.IsShowTheoTongHop = true;
                    PrintReportQuanLyKinhPhiViewModel.Name = "In báo cáo kế hoạch chi kinh phí quản lý";
                    PrintReportQuanLyKinhPhiViewModel.Description = "In báo cáo kế hoạch chi kinh phí quản lý";
                    PrintReportQuanLyKinhPhiViewModel.Init();

                    content = new PrintReportKeHoachChiQLKinhPhiChiTiet
                    {
                        DataContext = PrintReportQuanLyKinhPhiViewModel
                    };

                    break;
                case KhcQLKPCheckPrintType.KHCQLKPTH:
                    PrintReportQuanLyKinhPhiViewModel.Name = "In dự toán chi kinh phí quản lý BHXH, BHYT, BHTN";
                    PrintReportQuanLyKinhPhiViewModel.Description = "In dự toán chi kinh phí quản lý BHXH, BHYT, BHTN";
                    PrintReportQuanLyKinhPhiViewModel.KhcQLKPCheckPrintType = KhcQLKPCheckPrintType;
                    PrintReportQuanLyKinhPhiViewModel.IsShowTheoTongHop = false;
                    PrintReportQuanLyKinhPhiViewModel.IsSummary = true;
                    PrintReportQuanLyKinhPhiViewModel.Init();
                    content = new PrintReportKeHoachChiQLKinhPhiChiTiet
                    {
                        DataContext = PrintReportQuanLyKinhPhiViewModel
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
        #endregion

        #region On save
        public override void OnSave()
        {
            if (!IsSaveData)
            {
                return;
            }
            Func<BhKhcKinhphiQuanlyChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
            Func<BhKhcKinhphiQuanlyChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
            Func<BhKhcKinhphiQuanlyChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

            var detailsAdd = Items.Where(isAdd).ToList();
            var detailsUpdate = Items.Where(isUpdate).ToList();
            var detailsDelete = Items.Where(isDelete).ToList();

            //thêm mới chứng từ chi tiết
            if (detailsAdd.Count > 0)
            {
                var addItems = new List<BhKhcKinhphiQuanlyChiTiet>();

                detailsAdd.ForEach(x =>
                {
                    x.INamLamViec = _sessionInfo.YearOfWork;
                    x.SNguoiTao = _sessionInfo.Principal;
                    x.DNgayTao = DateTime.Now;
                });
                _mapper.Map(detailsAdd, addItems);
                _bhKhcKinhphiQuanlyChiTietService.AddRange(addItems);
                var khcKinhphiQuanlyByID = _bhKhcKinhphiQuanlyService.FindById(detailsAdd[0].IID_KHC_KinhPhiQuanLy);
                OnUpdateKhcKinhphiQuanLy(khcKinhphiQuanlyByID);

                Items.Where(isAdd).Select(x =>
                {
                    x.IsModified = false;
                    x.IsAdd = false;
                    return x;
                }).ToList();
            }

            //cập chứng từ chi tiết
            if (detailsUpdate.Count > 0)
            {
                foreach (var updateItem in detailsUpdate)
                {
                    var khtBHXHChiTiet = _bhKhcKinhphiQuanlyChiTietService.FindById(updateItem.Id);

                    updateItem.INamLamViec = _sessionInfo.YearOfWork;
                    updateItem.DNgaySua = DateTime.Now;
                    updateItem.DNguoiSua = _sessionInfo.Principal;
                    _mapper.Map(updateItem, khtBHXHChiTiet);
                    _bhKhcKinhphiQuanlyChiTietService.Update(khtBHXHChiTiet);
                    var khcKinhphiQuanlyByID = _bhKhcKinhphiQuanlyService.FindById(detailsUpdate[0].IID_KHC_KinhPhiQuanLy);
                    OnUpdateKhcKinhphiQuanLy(khcKinhphiQuanlyByID);
                    updateItem.IsModified = false;
                }
            }

            OnRefresh();
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            var message = Resources.MsgSaveDone;
            var messageBox = new NSMessageBoxViewModel(message);
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
            DialogHost.Show(messageBox.Content, SystemConstants.DETAIL_DIALOG);
        }

        private void OnUpdateKhcKinhphiQuanLy(BhKhcKinhphiQuanly khcKinhphiQuanlyByID)
        {
            try
            {
                khcKinhphiQuanlyByID.FTongTienDaThucHienNamTruoc = Model.FTongTienDaThucHienNamTruoc;
                khcKinhphiQuanlyByID.FTongTienUocThucHienNamTruoc = Model.FTongTienUocThucHienNamTruoc;
                khcKinhphiQuanlyByID.FTongTienKeHoachThucHienNamNay = Model.FTongTienKeHoachThucHienNamNay;
                khcKinhphiQuanlyByID.FTongTienCanBo = Model.FTongTienCanBo;
                khcKinhphiQuanlyByID.FTongTienQuanLuc = Model.FTongTienQuanLuc;
                khcKinhphiQuanlyByID.FTongTienQuanY = Model.FTongTienQuanY;
                khcKinhphiQuanlyByID.FTongTienTaiChinh = Model.FTongTienTaiChinh;
                khcKinhphiQuanlyByID.SNguoiSua = _sessionService.Current.Principal;
                khcKinhphiQuanlyByID.DNgaySua = DateTime.Now;

                _bhKhcKinhphiQuanlyService.Update(khcKinhphiQuanlyByID);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Close
        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }
        #endregion

        #region Search

        private void OnSearch()
        {
            SearchTextFilter();
        }


        private void LoadDefault()
        {
            SM = string.Empty;
            STM = string.Empty;
            SNoiDungSearch = string.Empty;
            DataSearch = new ObservableCollection<BhKhcKinhphiQuanlyChiTietModel>();
        }
        private void OnClearSearch(object obj)
        {
            LoadDefault();
            _khcKPQLChiTietModelsView.Refresh();
        }

        private void SearchTextFilter()
        {
            List<string> lstParents = new List<string>();
            List<BhKhcKinhphiQuanlyChiTietModel> lstChildSearch = new List<BhKhcKinhphiQuanlyChiTietModel>();
            List<BhKhcKinhphiQuanlyChiTietModel> lstParentSearch = new List<BhKhcKinhphiQuanlyChiTietModel>();
            List<BhKhcKinhphiQuanlyChiTietModel> results = new List<BhKhcKinhphiQuanlyChiTietModel>();
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
                    results.AddRange(GetDataParent(lstParentSearch.Select(x => x.SXauNoiMa).Distinct().Where(x => !lstParents.Contains(x)).ToList()));
            }
            DataSearch = new ObservableCollection<BhKhcKinhphiQuanlyChiTietModel>(results);
            _khcKPQLChiTietModelsView.Refresh();
        }

        private List<BhKhcKinhphiQuanlyChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhKhcKinhphiQuanlyChiTietModel> result = new List<BhKhcKinhphiQuanlyChiTietModel>();
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

        private void GetListChild(List<BhKhcKinhphiQuanlyChiTietModel> lstInput, List<BhKhcKinhphiQuanlyChiTietModel> results)
        {
            var itemChild = Items.Where(x => lstInput.Select(x => x.IID_MucLucNganSach).Distinct().Contains(x.IdParent)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => Items.Select(y => y.IdParent).Distinct().Contains(x.IID_MucLucNganSach ?? Guid.Empty)))
                {
                    GetListChild(new List<BhKhcKinhphiQuanlyChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

    }
}
