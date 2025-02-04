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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKhac;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKhac.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKhac.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKhac
{
    public class LapKeHoachChiKhacDetailViewModel : DetailViewModelBase<BhKhcKModel, BhKhcKChiTietModel>
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private readonly IBhKhcKChiTietService _bhKhcKcbChiTietService;
        private readonly IBhKhcKService _bhKhcKcbService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly INsDonViService _iNsDonViService;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;
        private ICollection<BhKhcKChiTietModel> _lstFilterResult = new HashSet<BhKhcKChiTietModel>();
        private ICollectionView _khcKPKChiTietModelsView { get; set; }
        #endregion

        #region Property
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public bool IsOpenPrintPopup = true;
        private string xnmConcatenation = "";
        public bool IsVoucherSummary { get; set; }
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
        public override Type ContentType => typeof(LapKeHoachChiKhacDetail);
        public bool IsInit { get; set; }
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
                BeForeRefresh();
                _khcKPKChiTietModelsView.Refresh();
                CalculateData();
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
                if (SetProperty(ref _typeShowsSelected, value) && _khcKPKChiTietModelsView != null)
                {
                    OnRefresh();
                    _khcKPKChiTietModelsView.Refresh();
                }
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
                if (SetProperty(ref _typeDisplaysSelected, value) && _khcKPKChiTietModelsView != null)
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

        private ObservableCollection<BhKhcKChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhKhcKChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhKhcKChiTietModel _selectedPopupItem;
        public BhKhcKChiTietModel SelectedPopupItem
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

        private ObservableCollection<BhKhcKChiTietModel> _dataSearch;
        public ObservableCollection<BhKhcKChiTietModel> DataSearch
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
        public RelayCommand RefreshCommand { get; }
        #endregion

        #region View model
        PrintReportKhcKViewModel PrintReportKhcKViewModel { get; set; }
        #endregion

        #region Constructor
        public LapKeHoachChiKhacDetailViewModel(
            IBhKhcKService bhKhcKcbService,
            IBhKhcKChiTietService bhKhcKcbChiTietService,
            IMapper mapper,
            ILog loger,
            ISessionService sessionService,
            PrintReportKhcKViewModel printReportKhcKcbViewModel,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            INsDonViService iNsDonViService)
        {
            _bhKhcKcbChiTietService = bhKhcKcbChiTietService;
            _bhKhcKcbService = bhKhcKcbService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = loger;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;

            SaveCommand = new RelayCommand(obj => OnSave());
            CloseCommand = new RelayCommand(OnClose);
            PrintCommand = new RelayCommand(obj => OnPrintDetal(obj));
            RefreshCommand = new RelayCommand(obj => Init());
            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(OnClearSearch);

            PrintReportKhcKViewModel = printReportKhcKcbViewModel;
            _iNsDonViService = iNsDonViService;
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
        }
        #endregion

        #region Load data
        public override void LoadData(params object[] args)
        {
            try
            {
                List<BhKhcKChiTiet> listChungTuChiTiet = new List<BhKhcKChiTiet>();
                Items = new ObservableCollection<BhKhcKChiTietModel>();
                var lstDmLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
                string sLNS = lstDmLoaiChi.Where(x => x.Id == Model.IIDLoaiChi).Select(x => x.SLNS).FirstOrDefault();
                KhcKChiTietCriteria searchCondition = new KhcKChiTietCriteria();
                searchCondition.NamLamViec = _sessionService.Current.YearOfWork;
                if (IsVoucherSummary && TypeDisplaysSelected != null && TypeDisplaysSelected.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    List<string> soChungTus = Model.STongHop.Split(",").ToList();
                    var predicate = PredicateBuilder.True<BhKhcK>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && soChungTus.Contains(x.SSoChungTu));
                    List<BhKhcK> lstChungTu = _bhKhcKcbService.FindByCondition(predicate).ToList();
                    List<BhKhcKChiTiet> listChungTuChiTietParent = new List<BhKhcKChiTiet>();
                    List<BhKhcKChiTiet> listChungTuChiTietChildren = new List<BhKhcKChiTiet>();
                    foreach (var chungTu in lstChungTu)
                    {
                        searchCondition.IdDonVi = chungTu.IID_MaDonVi;
                        searchCondition.IIDLoaiChi = chungTu.IIDLoaiChi;
                        searchCondition.KhcKcbId = chungTu.Id;
                        searchCondition.SLNS = sLNS;
                        var lstQuery = _bhKhcKcbChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
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
                    searchCondition.IIDLoaiChi = Model.IIDLoaiChi;
                    searchCondition.KhcKcbId = Model.Id;
                    searchCondition.SLNS = sLNS;
                    listChungTuChiTiet = _bhKhcKcbChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
                }

                var existBhChiTiet = _bhKhcKcbChiTietService.ExistKhcKcbChiTiet(searchCondition.KhcKcbId);
                foreach (var item in listChungTuChiTiet)
                {
                    item.IsAuToFillTuChi = !existBhChiTiet;
                }

                Items = _mapper.Map<ObservableCollection<BhKhcKChiTietModel>>(listChungTuChiTiet);
                DataPopupSearchItems = _mapper.Map<ObservableCollection<BhKhcKChiTietModel>>(listChungTuChiTiet);
                _khcKPKChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
                _khcKPKChiTietModelsView.Filter = ItemsViewFilter;
                foreach (var khcKcbChiTietModel in Items)
                {
                    khcKcbChiTietModel.IsFilter = true;
                    if (!khcKcbChiTietModel.IsHangCha)
                    {
                        khcKcbChiTietModel.PropertyChanged += (sender, args) =>
                        {
                            BhKhcKChiTietModel item = (BhKhcKChiTietModel)sender;
                            item.IsModified = true;
                            CalculateData();
                            khcKcbChiTietModel.IsModified = true;
                            OnPropertyChanged(nameof(IsSaveData));
                            OnPropertyChanged(nameof(IsOpenPrintPopup));
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
            Items.Where(x => x.IsHangCha)
               .ForAll(x =>
               {
                   x.FTienDaThucHienNamTruoc = 0;
                   x.FTienUocThucHienNamTruoc = 0;
                   x.FTienKeHoachThucHienNamNay = 0;
               });
            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }

            UpdateTotal(temp);
        }

        private void CalculateParent(Guid idParent, BhKhcKChiTietModel item, Dictionary<Guid?, BhKhcKChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            model.FTienDaThucHienNamTruoc += item.FTienDaThucHienNamTruoc;
            model.FTienUocThucHienNamTruoc += item.FTienUocThucHienNamTruoc;
            model.FTienKeHoachThucHienNamNay += item.FTienKeHoachThucHienNamNay;

            CalculateParent(model.IdParent, item, dictByMlns);
        }

        private void UpdateTotal(List<BhKhcKChiTietModel> temp)
        {
            Model.FTongTienDaThucHienNamTruoc = 0;
            Model.FTongTienUocThucHienNamTruoc = 0;
            Model.FTongTienKeHoachThucHienNamNay = 0;

            var roots = Items.Where(t => t.IdParent.Equals(Guid.Empty)).ToList();

            foreach (var item in roots)
            {
                Model.FTongTienDaThucHienNamTruoc += item.FTienDaThucHienNamTruoc;
                Model.FTongTienUocThucHienNamTruoc += item.FTienUocThucHienNamTruoc;
                Model.FTongTienKeHoachThucHienNamNay += item.FTienKeHoachThucHienNamNay;
            }
        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            TypeDisplaysSelected = TypeDisplays.FirstOrDefault();
            OnPropertyChanged(nameof(TypeDisplaysSelected));
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

        private void LoadAgencies(string agencyIds)
        {
            var listDonVi = _iNsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            OnPropertyChanged(nameof(Agencies));
        }

        private void BeForeRefresh()
        {
            _lstFilterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _lstFilterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhKhcKChiTietModel)obj;
            result = VoucherDetailFilter(item);
            if (!result && item.IsHangCha)
            {
                result = xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (result)
                item.IsFilter = result;
            if (!string.IsNullOrEmpty(SNoiDungSearch))
                result = DataSearch.Any(x => x.Id.Equals(item.Id));
            return result;
        }

        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            var item = (BhKhcKChiTietModel)obj;

            if (TypeShowsSelected != null)
            {
                if (TypeShowsSelected.ValueItem == TypeDisplay.CO_SO_LIEU)
                    result = result && item.IsDataNotNull;
                else if (TypeShowsSelected.ValueItem == TypeDisplay.CHUA_CO_SO_LIEU)
                    result = result && !item.IsDataNotNull && !item.IsHangCha;
            }

            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIDMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;
        }
        #endregion

        #region On save
        public override void OnSave()
        {
            if (!IsSaveData)
            {
                return;
            }
            try
            {
                Func<BhKhcKChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
                Func<BhKhcKChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;

                var detailsAdd = Items.Where(isAdd).ToList();
                var detailsUpdate = Items.Where(isUpdate).ToList();

                //thêm mới chứng từ chi tiết
                if (detailsAdd.Count > 0)
                {
                    var addItems = new List<BhKhcKChiTiet>();
                    detailsAdd.ForEach(x =>
                    {
                        x.INamLamViec = Model.INamLamViec.Value;
                        x.IIDMaDonVi = Model.IID_MaDonVi;
                        x.DNgayTao = DateTime.Now;
                        x.SNguoiTao = _sessionInfo.Principal;
                    });

                    _mapper.Map(detailsAdd, addItems);
                    _bhKhcKcbChiTietService.AddRange(addItems);
                    var khcKByID = _bhKhcKcbService.FindById(detailsAdd[0].IID_KHC_K);
                    OnUpdateKhcKhac(khcKByID);

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
                        var khtBHXHChiTiet = _bhKhcKcbChiTietService.FindById(updateItem.Id);
                        updateItem.IIDMaDonVi = Model.IID_MaDonVi;
                        updateItem.INamLamViec = Model.INamLamViec.Value;
                        updateItem.SNguoiSua = _sessionInfo.Principal;
                        updateItem.DNgaySua = DateTime.Now;
                        _mapper.Map(updateItem, khtBHXHChiTiet);
                        _bhKhcKcbChiTietService.Update(khtBHXHChiTiet);
                        var khcKcbByID = _bhKhcKcbService.FindById(detailsUpdate[0].IID_KHC_K);
                        OnUpdateKhcKhac(khcKcbByID);
                        updateItem.IsModified = false;
                    }
                }

                if (detailsAdd.Count > 0 || detailsUpdate.Count > 0)
                {
                    OnRefresh();

                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IsDeleteAll));
                    var message = Resources.MsgSaveDone;
                    var messageBox = new NSMessageBoxViewModel(message);
                    UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
                    DialogHost.Show(messageBox.Content, SystemConstants.DETAIL_DIALOG);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnUpdateKhcKhac(BhKhcK khcKBy)
        {
            try
            {
                khcKBy.FTongTienDaThucHienNamTruoc = Model.FTongTienDaThucHienNamTruoc;
                khcKBy.FTongTienUocThucHienNamTruoc = Model.FTongTienUocThucHienNamTruoc;
                khcKBy.FTongTienKeHoachThucHienNamNay = Model.FTongTienKeHoachThucHienNamNay;
                khcKBy.SNguoiSua = _sessionService.Current.Principal;
                khcKBy.DNgaySua = DateTime.Now;

                _bhKhcKcbService.Update(khcKBy);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Refresh
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
            var KhcQLKPCheckPrintType = (KhcKcbCheckPrintType)((int)obj);
            object content;
            switch (KhcQLKPCheckPrintType)
            {
                case KhcKcbCheckPrintType.KHCKCBBHXHCT:
                    PrintReportKhcKViewModel.KhcKcbCheckType = KhcQLKPCheckPrintType;
                    PrintReportKhcKViewModel.IsSummary = false;
                    PrintReportKhcKViewModel.IsLoaiKCB = true;
                    PrintReportKhcKViewModel.IsShowTheoTongHop = true;
                    PrintReportKhcKViewModel.Name = "In kế hoạch chi khác BHXH, BHYT";
                    PrintReportKhcKViewModel.Description = "In kế hoạch chi khác BHXH, BHYT";
                    PrintReportKhcKViewModel.Init();

                    content = new PrintReportKeHoachChiKhacChiTiet
                    {
                        DataContext = PrintReportKhcKViewModel
                    };

                    break;
                case KhcKcbCheckPrintType.KHCKCBBHXHTH:
                    PrintReportKhcKViewModel.Name = "In dự toán chi khác BHXH, BHYT";
                    PrintReportKhcKViewModel.Description = "In dự toán chi khác BHXH, BHYT";
                    PrintReportKhcKViewModel.KhcKcbCheckType = KhcQLKPCheckPrintType;
                    PrintReportKhcKViewModel.IsShowTheoTongHop = false;
                    PrintReportKhcKViewModel.Init();
                    PrintReportKhcKViewModel.IsSummary = true;
                    PrintReportKhcKViewModel.IsLoaiKCB = true;
                    content = new PrintReportKeHoachChiKhacChiTiet
                    {
                        DataContext = PrintReportKhcKViewModel
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

        #region On Close
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

        private void OnClearSearch(object obj)
        {
            SNoiDungSearch = string.Empty;
            _khcKPKChiTietModelsView.Refresh();
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhKhcKChiTietModel> results = new List<BhKhcKChiTietModel>();

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
                DataSearch = new ObservableCollection<BhKhcKChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhKhcKChiTietModel>();
            }
            _khcKPKChiTietModelsView.Refresh();
        }

        private List<BhKhcKChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhKhcKChiTietModel> result = new List<BhKhcKChiTietModel>();
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

        private void GetListChild(List<BhKhcKChiTietModel> lstInput, List<BhKhcKChiTietModel> results)
        {
            var itemChild = Items.Where(x => lstInput.Select(x => x.IID_MucLucNganSach).Distinct().Contains(x.IdParent)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => Items.Select(y => y.IdParent).Distinct().Contains(x.IID_MucLucNganSach ?? Guid.Empty)))
                {
                    GetListChild(new List<BhKhcKChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

    }
}
