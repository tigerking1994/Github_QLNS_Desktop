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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV
{
    public class LapKeHoachChiKCBQYDVDetailViewModel : DetailViewModelBase<BhKhcKcbModel, BhKhcKcbChiTietModel>
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private readonly IBhKhcKcbChiTietService _bhKhcKcbChiTietService;
        private readonly IBhKhcKcbService _bhKhcKcbService;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;
        private readonly INsDonViService _iNsDonViService;
        private ICollection<BhKhcKcbChiTietModel> _lstFilterResult = new HashSet<BhKhcKcbChiTietModel>();
        private ICollectionView _khcKcbChiTietModelsView { get; set; }
        #endregion

        #region Property
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        private string xnmConcatenation = "";
        public bool IsOpenPrintPopup = true;
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
                _khcKcbChiTietModelsView.Refresh();
                CalculateData();
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
                if (SetProperty(ref _typeDisplaysSelected, value) && _khcKcbChiTietModelsView != null)
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
                if (SetProperty(ref _typeShowsSelected, value) && _khcKcbChiTietModelsView != null)
                {
                    OnRefresh();
                    _khcKcbChiTietModelsView.Refresh();
                }
            }
        }

        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted)
                        || Items.Any(x => !x.IsHangCha);

        public string HeaderSoDaThucHienNam => "Số đã thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderUocThucHienNam => "Ước thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderKehoachThucHienNam => "Kế hoạch thực hiện năm " + (_sessionService.Current.YearOfWork);
        public bool IsChungTuTongHop => Model != null;
        public bool IsAnotherUserCreate { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public override Type ContentType => typeof(LapKeHoachChiKCBQYDVDetail);
        public bool IsInit { get; set; }

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private ObservableCollection<BhKhcKcbChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhKhcKcbChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhKhcKcbChiTietModel _selectedPopupItem;
        public BhKhcKcbChiTietModel SelectedPopupItem
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

        private ObservableCollection<BhKhcKcbChiTietModel> _dataSearch;
        public ObservableCollection<BhKhcKcbChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }
        public List<string> LstSoKeHoach { get; set; }
        #endregion

        #region View model
        PrintReportKhcKCBQYDVViewModel PrintReportKhcKCBQYDVViewModel { get; set; }
        #endregion

        #region RelayCommand
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public new RelayCommand RefreshCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        #endregion

        public LapKeHoachChiKCBQYDVDetailViewModel(
            IBhKhcKcbService bhKhcKcbService,
            IBhKhcKcbChiTietService bhKhcKcbChiTietService,
            IMapper mapper,
            ILog loger,
            ISessionService sessionService,
            PrintReportKhcKCBQYDVViewModel printReportKhcKCBQYDVViewModel,
            INsDonViService iNsDonViService)
        {
            _bhKhcKcbChiTietService = bhKhcKcbChiTietService;
            _bhKhcKcbService = bhKhcKcbService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = loger;

            SaveCommand = new RelayCommand(obj => OnSave());
            CloseCommand = new RelayCommand(OnClose);
            PrintCommand = new RelayCommand(obj => OnPrintDetail(obj));
            RefreshCommand = new RelayCommand(obj => OnRefresh());
            SearchCommand = new RelayCommand(o => SearchTextFilter());
            ClearSearchCommand = new RelayCommand(OnClearSearch);

            PrintReportKhcKCBQYDVViewModel = printReportKhcKCBQYDVViewModel;
            _iNsDonViService = iNsDonViService;
        }

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
                List<BhKhcKcbChiTiet> listChungTuChiTiet = new List<BhKhcKcbChiTiet>();
                Items = new ObservableCollection<BhKhcKcbChiTietModel>();
                KhcKcbChiTietCriteria searchCondition = new KhcKcbChiTietCriteria();
                searchCondition.NamLamViec = _sessionService.Current.YearOfWork;
                string sMaDonVi = string.Empty;
                if (IsVoucherSummary && TypeDisplaysSelected != null && TypeDisplaysSelected.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    List<string> soChungTus = Model.STongHop.Split(",").ToList();
                    var predicate = PredicateBuilder.True<BhKhcKcb>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && soChungTus.Contains(x.SSoChungTu));
                    List<BhKhcKcb> lstChungTu = _bhKhcKcbService.FindByCondition(predicate).ToList();
                    List<BhKhcKcbChiTiet> listChungTuChiTietParent = new List<BhKhcKcbChiTiet>();
                    List<BhKhcKcbChiTiet> listChungTuChiTietChildren = new List<BhKhcKcbChiTiet>();
                    sMaDonVi = string.Join(",", lstChungTu.Select(x => x.IID_MaDonVi));
                    foreach (var chungTu in lstChungTu)
                    {
                        searchCondition.KhcKcbId = chungTu.Id;
                        searchCondition.IdDonVi = chungTu.IID_MaDonVi;
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
                    sMaDonVi = Model.IID_MaDonVi;
                    if (IsAggregate)
                    {
                        var predicate = PredicateBuilder.True<BhKhcKcb>();
                        predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && LstSoKeHoach.Contains(x.SSoChungTu));
                        List<BhKhcKcb> lstChungTu = _bhKhcKcbService.FindByCondition(predicate).ToList();
                        if (!lstChungTu.IsEmpty())
                        {
                            sMaDonVi = string.Join(StringUtils.COMMA, lstChungTu.Select(x => x.IID_MaDonVi));
                        }
                    }
                    searchCondition.IdDonVi = Model.IID_MaDonVi;
                    searchCondition.KhcKcbId = Model.Id.IsNullOrEmpty() ? Model.IID_BH_KHC_KCB : Model.Id;
                    listChungTuChiTiet = _bhKhcKcbChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
                }

                var existBhChiTiet = _bhKhcKcbChiTietService.ExistKhcKcbChiTiet(searchCondition.KhcKcbId);

                foreach (var item in listChungTuChiTiet)
                {
                    item.IsAuToFillTuChi = !existBhChiTiet;
                }
                var lstKhtBhxhQuanNhan = _bhKhcKcbChiTietService.FindGiaTriKeHoachThuBHXH(sMaDonVi, Model.INamLamViec ?? 0, Model.FTyLeThu ?? 0);
                var lstKhtBhxhQuanNhanMap = _mapper.Map<List<BhKhcKcbChiTietModel>>(lstKhtBhxhQuanNhan);
                var listChungTuChiTietMap = _mapper.Map<List<BhKhcKcbChiTietModel>>(listChungTuChiTiet);
                var itemRoot = listChungTuChiTietMap.FirstOrDefault(x => !string.IsNullOrEmpty(x.SXauNoiMa) && x.SXauNoiMa.Count() == 3);
                var indexInsert = listChungTuChiTietMap.IndexOf(itemRoot);
                listChungTuChiTietMap.InsertRange(indexInsert + 1, lstKhtBhxhQuanNhanMap);
                Items = new ObservableCollection<BhKhcKcbChiTietModel>(listChungTuChiTietMap);
                DataPopupSearchItems = _mapper.Map<ObservableCollection<BhKhcKcbChiTietModel>>(listChungTuChiTiet);
                _khcKcbChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
                _khcKcbChiTietModelsView.Filter = ItemsViewFilter;
                foreach (var khcKcbChiTietModel in Items)
                {
                    khcKcbChiTietModel.IsFilter = true;
                    if (!khcKcbChiTietModel.IsHangCha && !khcKcbChiTietModel.IsRemainRow)
                    {
                        khcKcbChiTietModel.PropertyChanged += (sender, args) =>
                        {
                            BhKhcKcbChiTietModel item = (BhKhcKcbChiTietModel)sender;
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

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhKhcKcbChiTietModel)obj;
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
            var item = (BhKhcKcbChiTietModel)obj;

            if (TypeShowsSelected != null)
            {
                if (TypeShowsSelected.ValueItem == TypeDisplay.CO_SO_LIEU)
                    result = result && item.IsDataNotNull && !item.BHangCha;
                else if (TypeShowsSelected.ValueItem == TypeDisplay.CHUA_CO_SO_LIEU)
                    result = result && !item.IsDataNotNull && !item.BHangCha;
            }

            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIDMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;
        }

        private void BeForeRefresh()
        {
            _lstFilterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
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
               });
            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }

            UpdateTotal(temp);
        }

        private void CalculateParent(Guid idParent, BhKhcKcbChiTietModel item, Dictionary<Guid?, BhKhcKcbChiTietModel> dictByMlns)
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

        private void UpdateTotal(List<BhKhcKcbChiTietModel> temp)
        {
            Model.FTongTienDaThucHienNamTruoc = 0;
            Model.FTongTienUocThucHienNamTruoc = 0;
            Model.FTongTienKeHoachThucHienNamNay = 0;

            var roots = Items.Where(t => !string.IsNullOrEmpty(t.SXauNoiMa) && !t.IdParent.IsNullOrEmpty() && !Items.Select(y => y.IID_MucLucNganSach.GetValueOrDefault()).Contains(t.IdParent)).ToList();
            var remainRow = Items.FirstOrDefault(t => t.IRemainRow.Equals(1));

            Items.Where(x => x.IRemainRow.Equals(2)).Select(x => x.FTienKeHoachThucHienNamNay = (remainRow.FTienKeHoachThucHienNamNay.GetValueOrDefault() - (roots.IsEmpty() ? 0 : roots.FirstOrDefault().FTienKeHoachThucHienNamNay.GetValueOrDefault()))).ToList();
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
                if (Items.Any(x => x.IRemainRow.Equals(2) && x.FTienKeHoachThucHienNamNay.GetValueOrDefault() < 0))
                {
                    var mess = string.Format(Resources.MessageErrorSaveKcbDetail, Model.FTyLeThu);
                    MessageBoxHelper.Error(mess);
                    return;
                }

                Func<BhKhcKcbChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
                Func<BhKhcKcbChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;

                var detailsAdd = Items.Where(isAdd).ToList();
                var detailsUpdate = Items.Where(isUpdate).ToList();

                //thêm mới chứng từ chi tiết
                if (detailsAdd.Count > 0)
                {
                    var addItems = new List<BhKhcKcbChiTiet>();

                    detailsAdd.ForEach(x =>
                    {
                        x.INamLamViec = Model.INamLamViec.Value;
                        x.IIDMaDonVi = Model.IID_MaDonVi;
                        x.DNgayTao = DateTime.Now;
                        x.SNguoiTao = _sessionInfo.Principal;
                    });
                    _mapper.Map(detailsAdd, addItems);
                    _bhKhcKcbChiTietService.AddRange(addItems);
                    var khcKcbByID = _bhKhcKcbService.FindById(detailsAdd[0].IID_KHC_KCB);
                    OnUpdateKhcKCB(khcKcbByID);

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
                        updateItem.INamLamViec = Model.INamLamViec.Value;
                        updateItem.SNguoiSua = _sessionInfo.Principal;
                        updateItem.DNgaySua = DateTime.Now;
                        _mapper.Map(updateItem, khtBHXHChiTiet);
                        _bhKhcKcbChiTietService.Update(khtBHXHChiTiet);
                        var khcKcbByID = _bhKhcKcbService.FindById(detailsUpdate[0].IID_KHC_KCB);
                        OnUpdateKhcKCB(khcKcbByID);
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
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnUpdateKhcKCB(BhKhcKcb khcKcbByID)
        {
            try
            {
                khcKcbByID.FTongTienDaThucHienNamTruoc = Model.FTongTienDaThucHienNamTruoc;
                khcKcbByID.FTongTienUocThucHienNamTruoc = Model.FTongTienUocThucHienNamTruoc;
                khcKcbByID.FTongTienKeHoachThucHienNamNay = Model.FTongTienKeHoachThucHienNamNay;
                khcKcbByID.SNguoiSua = _sessionService.Current.Principal;
                khcKcbByID.DNgaySua = DateTime.Now;

                _bhKhcKcbService.Update(khcKcbByID);
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
        private void OnPrintDetail(object obj)
        {
            var KhcQLKPCheckPrintType = (KhcKcbCheckPrintType)((int)obj);
            object content;
            switch (KhcQLKPCheckPrintType)
            {
                case KhcKcbCheckPrintType.KHCKCBBHXHCT:
                    PrintReportKhcKCBQYDVViewModel.KhcKcbCheckType = KhcQLKPCheckPrintType;
                    PrintReportKhcKCBQYDVViewModel.IsSummary = false;
                    PrintReportKhcKCBQYDVViewModel.IsLoaiKCB = true;
                    PrintReportKhcKCBQYDVViewModel.IsShowTheoTongHop = true;
                    PrintReportKhcKCBQYDVViewModel.Name = "In kế hoạch chi khác BHXH, BHYT";
                    PrintReportKhcKCBQYDVViewModel.Description = "In kế hoạch chi khác BHXH, BHYT";
                    PrintReportKhcKCBQYDVViewModel.Init();

                    content = new PrintReportKeHoachChiKCBQYDVChiTiet
                    {
                        DataContext = PrintReportKhcKCBQYDVViewModel
                    };

                    break;
                case KhcKcbCheckPrintType.KHCKCBBHXHTH:
                    PrintReportKhcKCBQYDVViewModel.Name = "In dự toán chi khác BHXH, BHYT";
                    PrintReportKhcKCBQYDVViewModel.Description = "In dự toán chi khác BHXH, BHYT";
                    PrintReportKhcKCBQYDVViewModel.KhcKcbCheckType = KhcQLKPCheckPrintType;
                    PrintReportKhcKCBQYDVViewModel.IsShowTheoTongHop = false;
                    PrintReportKhcKCBQYDVViewModel.Init();
                    PrintReportKhcKCBQYDVViewModel.IsSummary = true;
                    PrintReportKhcKCBQYDVViewModel.IsLoaiKCB = true;
                    content = new PrintReportKeHoachChiKCBQYDVChiTiet
                    {
                        DataContext = PrintReportKhcKCBQYDVViewModel
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

        #region Close
        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }
        #endregion

        #region Search
        private void OnClearSearch(object obj)
        {
            SNoiDungSearch = string.Empty;
            _khcKcbChiTietModelsView.Refresh();
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhKhcKcbChiTietModel> results = new List<BhKhcKcbChiTietModel>();

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
                DataSearch = new ObservableCollection<BhKhcKcbChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhKhcKcbChiTietModel>();
            }
            _khcKcbChiTietModelsView.Refresh();
        }

        private List<BhKhcKcbChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhKhcKcbChiTietModel> result = new List<BhKhcKcbChiTietModel>();
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

        private void GetListChild(List<BhKhcKcbChiTietModel> lstInput, List<BhKhcKcbChiTietModel> results)
        {
            var itemChild = Items.Where(x => lstInput.Select(x => x.IID_MucLucNganSach).Distinct().Contains(x.IdParent)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => Items.Select(y => y.IdParent).Distinct().Contains(x.IID_MucLucNganSach ?? Guid.Empty)))
                {
                    GetListChild(new List<BhKhcKcbChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

    }
}
