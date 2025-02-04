using AutoMapper;
using ControlzEx.Standard;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation
{
    public class CapPhatTamUngKCBBHYTDetailViewModel : DetailViewModelBase<BhCptuBHYTModel, BhCptuBHYTChiTietModel>
    {
        private IMapper _mapper;
        private ISessionService _sessionService;

        private ICptuBHYTService _cptuBHYTService;
        private ICptuBHYTChiTietService _cptuBHYTChiTietService;
        private IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private IBhDmCoSoYTeService _bhDmCoSoYTeService;
        private ICollectionView ItemsView;
        private readonly ILog _logger;

        private SessionInfo _sessionInfo;
        private bool _isCapPhatToanDonVi;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public override Type ContentType => typeof(CapPhatTamUngKCBBHYTDetail);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;
        public bool IsSaveData;
        public bool IsDelete => _selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI ? false : (SelectedItem != null ? true : false);
        public bool IsDeleteAll => Items.Any(item => !item.IsModified);
        public bool IsReadOnlyGrid => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI) ? true : false;
        public bool IsTongHop => !string.IsNullOrEmpty(Model.SDSSoChungTuTongHop) ? true : false;
        public Visibility VisibleColAgency => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI) ?
            Visibility.Collapsed : Visibility.Visible;

        public Visibility VisibleVoucherNo => !string.IsNullOrEmpty(Model.SDSSoChungTuTongHop) && VisibleColAgency == Visibility.Visible ? Visibility.Visible : Visibility.Collapsed;

        public bool ReadOnlyCapPhat => IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI ? true : false;
        public bool ReadOnlyDeNghi => IsTongHop ? true : false;
        public bool IsEditByRole => Model.SNguoiTao == _sessionInfo.Principal;

        private string xnmConcatenation = "";
        public bool IsVoucherSummary { get; set; }

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
        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set
            {
                SetProperty(ref _isPopupOpen, value);
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

        private ObservableCollection<BhCptuBHYTChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhCptuBHYTChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhCptuBHYTChiTietModel _selectedPopupItem;
        public BhCptuBHYTChiTietModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SNoiDungSearch = _selectedPopupItem?.SMoTa;
                OnPropertyChanged(nameof(SNoiDungSearch));
                IsPopupOpen = false;
            }
        }

        private ObservableCollection<BhCptuBHYTChiTietModel> _dataSearch;
        public ObservableCollection<BhCptuBHYTChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }

        private bool IsDefault
        {
            get
            {
                return string.IsNullOrEmpty(SearchLNS) && string.IsNullOrEmpty(SNoiDungSearch);
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

        private ObservableCollection<ComboboxItem> _typeDisplays;
        public ObservableCollection<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private string _typeDisplaysselected;
        public string TypeDisplaysSelected
        {
            get => _typeDisplaysselected;
            set
            {
                if (SetProperty(ref _typeDisplaysselected, value) && ItemsView != null)
                {
                    OnRefresh();
                    //BeForeRefresh();
                    ItemsView.Refresh();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _agencies;
        public ObservableCollection<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private ComboboxItem _selectedAgency;
        public ComboboxItem SelectedAgency
        {
            get => _selectedAgency;
            set => SetProperty(ref _selectedAgency, value);
        }


        public RelayCommand SaveCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public RelayCommand RefreshAllDataCommand { get; }
        public RelayCommand AutoFillDataCommand { get; }
        public new RelayCommand CloseCommand { get; }


        public CapPhatTamUngKCBBHYTDetailViewModel(ICpChungTuService cpChungTuService,
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            IDanhMucService danhMucService,
            ICptuBHYTService cptuBHYTService,
            ICptuBHYTChiTietService cptuBHYTChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDmCoSoYTeService bhDmCoSoYTeService
            )
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _cptuBHYTService = cptuBHYTService;
            _cptuBHYTChiTietService = cptuBHYTChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDmCoSoYTeService = bhDmCoSoYTeService;

            SaveCommand = new RelayCommand(obj => OnSaveData());
            RefreshAllDataCommand = new RelayCommand(obj => OnRefreshAllData());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(OnClearSearch);
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
                LoadData();
                LoadDataAgency();
                LoadTypeDisplay();
                OnClearSearch(false);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDataAgency()
        {
            var data = _bhDmCoSoYTeService.GetListCoSoYTe(_sessionInfo.YearOfWork);
            if (data.IsEmpty())
            {
                _agencies = new ObservableCollection<ComboboxItem>();
            }
            else
            {
                _agencies = new ObservableCollection<ComboboxItem>(_mapper.Map<List<ComboboxItem>>(data));
            }

        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Tất cả" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Có dữ liệu" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.KHONG_CO_DU_LIEU, DisplayItem = "Không có dữ liệu" });
            TypeDisplaysSelected = TypeDisplay.TAT_CA;
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsDelete));
        }

        public override void LoadData(params object[] args)
        {
            int iNamLamViec = _sessionInfo.YearOfWork;
            string sLNS = Model.SDSLNS;
            string sIdCsYTe = Model.SDSID_CoSoYTe;

            int iQuyKyTruoc = 0;
            int iNamKyTruoc = 0;

            if (Model.IQuy == 1)
            {
                iQuyKyTruoc = 4;
                iNamKyTruoc = (iNamLamViec - 1);
            }
            else
            {
                iQuyKyTruoc = Model.IQuy - 1;
                iNamKyTruoc = iNamLamViec;
            }

            List<BhCptuBHYTChiTietQuery> listDataQuery = new List<BhCptuBHYTChiTietQuery>();
            listDataQuery = _cptuBHYTChiTietService.FinChungTuChiTiet(Model.Id, sLNS, sIdCsYTe, iNamLamViec, iQuyKyTruoc, iNamKyTruoc, _sessionInfo.Principal).ToList();
            Items = _mapper.Map<ObservableCollection<BhCptuBHYTChiTietModel>>(listDataQuery);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhCptuBHYTChiTietModel>>(listDataQuery);
            ItemsView = CollectionViewSource.GetDefaultView(Items);
            ItemsView.Filter = ItemsViewFilter;
            CalculateData();
            foreach (var bhCptuBHYTChiTietModel in Items)
            {
                bhCptuBHYTChiTietModel.IsFilter = true;
                bhCptuBHYTChiTietModel.INamLamViec = iNamLamViec;
                bhCptuBHYTChiTietModel.IIDMaDonVi = _sessionInfo.IdDonVi;
                if (!bhCptuBHYTChiTietModel.BHangCha)
                {
                    bhCptuBHYTChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        BhCptuBHYTChiTietModel item = (BhCptuBHYTChiTietModel)sender;

                        if (args.PropertyName == nameof(SelectedItem.FQTQuyTruoc) || args.PropertyName == nameof(SelectedItem.FLuyKeCapDenCuoiQuy))
                        {
                            item.IsModified = true;
                            bhCptuBHYTChiTietModel.IsModified = true;
                            item.FTamUngQuyNay = (item.FQTQuyTruoc ?? 0) * 80 / 100;
                            CalculateData();
                        }
                        if (args.PropertyName == nameof(SelectedItem.SGhiChu))
                        {
                            item.IsModified = true;
                            bhCptuBHYTChiTietModel.IsModified = true;
                        }
                        IsSaveData = true;
                        OnPropertyChanged(nameof(IsSaveData));
                        //OnPropertyChanged(nameof(IsOpenPrintPopup));

                    }; 

                }
            }
        }
        private void CalculateData()
        {
            Items.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    x.FQTQuyTruoc = 0;
                    x.FLuyKeCapDenCuoiQuy = 0;
                    x.FTamUngQuyNay = 0;

                });
            var dictByMlns = Items.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            var temp = Items.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (var item in temp)
            {
                item.FLuyKeCapDenCuoiQuy = item.FluyKeCap;
                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }

            UpdateTotal();
        }

        private void CalculateParent(Guid? idParent, BhCptuBHYTChiTietModel item, Dictionary<Guid?, BhCptuBHYTChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            model.FQTQuyTruoc = (model.FQTQuyTruoc ?? 0) + (item.FQTQuyTruoc ?? 0);
            model.FTamUngQuyNay = (model.FTamUngQuyNay ?? 0) + (item.FTamUngQuyNay ?? 0);
            model.FLuyKeCapDenCuoiQuy = model.FluyKeCap;

            CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
        }

        private void UpdateTotal()
        {
            Model.FQTQuyTruoc = 0;
            Model.FLuyKeCapDenCuoiQuy = 0;
            Model.FTamUngQuyNay = 0;
            Model.FCapThuaQuyTruocChuyenSang = 0;
            Model.FPhaiCapTamUngQuyNay = 0;

            Model.FQTQuyTruoc = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FQTQuyTruoc);
            Model.FLuyKeCapDenCuoiQuy = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FLuyKeCapDenCuoiQuy);
            Model.FTamUngQuyNay = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTamUngQuyNay);
            Model.FPhaiCapTamUngQuyNay = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FPhaiCapTamUngQuyNay);
            Model.FCapThuaQuyTruocChuyenSang = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FCapThuaQuyTruocChuyenSang);

        }
        private void OnSaveData()
        {
            if (!IsSaveData)
            {
                return;
            }

            var lstDataAdd = Items.Where(x => !x.BHangCha && x.Id == Guid.Empty && x.IsModified).ToList();
            var lstDataUpdate = Items.Where(x => !x.BHangCha && x.Id != Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
            var lstDataDelete = Items.Where(x => !x.BHangCha && x.IsDeleted && x.IsModified && x.Id != Guid.Empty).ToList();

            var addItemList = new List<BhCptuBHYTChiTiet>();
            if (lstDataAdd.Count() > 0)
            {
                _mapper.Map(lstDataAdd, addItemList);
                addItemList.Select(x => { x.Id = Guid.NewGuid(); x.IID_BH_CP_CapTamUng_KCB_BHYT = Model.Id; return x; }).ToList();
                _cptuBHYTChiTietService.AddRange(addItemList);

                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            if (lstDataUpdate.Count() > 0)
            {
                _mapper.Map(lstDataUpdate, addItemList);
                addItemList.Select(x => { x.IID_BH_CP_CapTamUng_KCB_BHYT = Model.Id; return x; }).ToList();
                _cptuBHYTChiTietService.UpdateRange(addItemList);
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            if (lstDataDelete.Count() > 0)
            {
                _mapper.Map(lstDataDelete, addItemList);
                _cptuBHYTChiTietService.RemoveRange(addItemList);
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; x.IsDeleted = false; return x; }).ToList();
            }

            //Update cấp phát tạm ứng
            BhCptuBHYT cp = new BhCptuBHYT();
            cp = _cptuBHYTService.FindById(Model.Id);

            var predicate_chitiet = PredicateBuilder.True<BhCptuBHYTChiTiet>();
            predicate_chitiet = predicate_chitiet.And(x => x.IID_BH_CP_CapTamUng_KCB_BHYT == Model.Id);
            var lstCpChiTiet = _cptuBHYTChiTietService.FindByCondition(predicate_chitiet).ToList();

            cp.FQTQuyTruoc = lstCpChiTiet?.Select(x => x.FQTQuyTruoc).Sum();
            cp.FTamUngQuyNay = lstCpChiTiet?.Select(x => x.FTamUngQuyNay).Sum();
            _cptuBHYTService.Update(cp);

            IsSaveData = false;
            LoadData();
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            SavedAction?.Invoke(null);
        }

        protected override void OnAdd()
        {


        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.BHangCha)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                CalculateData();
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            var result = MessageBox.Show(Resources.DeleteAllChungTuChiTiet, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                if (Items != null)
                {
                    Items.Where(x => x.IsFilter && !x.BHangCha).ForAll(x => x.IsDeleted = true);
                    OnSave();
                }
                OnPropertyChanged(nameof(IsDeleteAll));
            }
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
                    var result = MessageBoxHelper.ConfirmCancel(Resources.ConfirmReloadData);
                    if (result == MessageBoxResult.Cancel)
                        return;
                    else if (result == MessageBoxResult.Yes)
                        OnSaveData();
                }
                IsCreate = false;
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {

        }

        private void OpenPrintDialog(object param)
        {

        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        #region Search

        private void OnSearch()
        {
            SearchTextFilter();
        }
        private void OnClearSearch(object obj)
        {
            SearchLNS = string.Empty;
            SNoiDungSearch = string.Empty;
            if (!(obj is bool temp))
            {
                ItemsView.Refresh();

            }
        }


        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhCptuBHYTChiTietModel)obj;

            if (!IsDefault)
            {
                result = DataSearch.Any(x => x.IIdFilter.Equals(item.IIdFilter));
            }
            result = result && ChungTuChiTietItemsViewFilter(item);
            if (result)
                item.IsFilter = result;
            return result;
        }

        private bool ChungTuChiTietItemsViewFilter(BhCptuBHYTChiTietModel item)
        {
            bool result = true;
            if (!string.IsNullOrEmpty(TypeDisplaysSelected))
            {
                if (TypeDisplaysSelected == TypeDisplay.KHONG_CO_DU_LIEU)
                    result = result && !item.BHasData && !item.IsDeleted && !item.IsHangCha;
                else if (TypeDisplaysSelected == TypeDisplay.CO_DU_LIEU)
                    result = result && item.BHasData && !item.IsHangCha;
            }

            if (SelectedAgency != null)
            {
                result = result && item.IID_MaCoSoYTe.Equals(SelectedAgency.ValueItem);
            }
            return result;
        }

        private void SearchTextFilter()
        {
            List<string> lstParents = new List<string>();
            List<BhCptuBHYTChiTietModel> lstChildSearch = new List<BhCptuBHYTChiTietModel>();
            List<BhCptuBHYTChiTietModel> lstParentSearch = new List<BhCptuBHYTChiTietModel>();
            List<BhCptuBHYTChiTietModel> results = new List<BhCptuBHYTChiTietModel>();
            if (!string.IsNullOrEmpty(SearchLNS))
            {
                lstChildSearch = Items.Where(x => x.SLNS.ToLower().Contains(SearchLNS.ToLower()) && !x.IsHangCha).ToList();
                lstParentSearch = Items.Where(x => x.SLNS.ToLower().Contains(SearchLNS.ToLower()) && x.IsHangCha).ToList();
            }

            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                if (!string.IsNullOrEmpty(SearchLNS))
                {
                    lstChildSearch = lstChildSearch.Where(x => x.SMoTa.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).ToList();
                    lstParentSearch = lstParentSearch.Where(x => x.SMoTa.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).ToList();
                }
                else
                {
                    lstChildSearch = Items.Where(x => x.SMoTa.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).ToList();
                    lstParentSearch = Items.Where(x => x.SMoTa.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).ToList();
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
            DataSearch = new ObservableCollection<BhCptuBHYTChiTietModel>(results);
            ItemsView.Refresh();
        }

        private List<BhCptuBHYTChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhCptuBHYTChiTietModel> result = new List<BhCptuBHYTChiTietModel>();
            List<string> lstParent = StringUtils.GetListKyHieuParent(lstInput);
            if (!lstParent.IsEmpty() && lstParent.Any(x => x.Count() >= 3))
            {
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
            }
            var lstData = DataPopupSearchItems.Where(x => lstParent.Contains(x.SXauNoiMa)).ToList();
            result.AddRange(lstData);
            GetListChild(lstData.Where(x => lstInput.Contains(x.SXauNoiMa)).ToList(), result);
            return result;
        }

        private void GetListChild(List<BhCptuBHYTChiTietModel> lstInput, List<BhCptuBHYTChiTietModel> results)
        {
            var itemChild = DataPopupSearchItems.Where(x => lstInput.Select(x => x.IID_MLNS).Distinct().Contains(x.IID_MLNS_Cha ?? Guid.Empty)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => DataPopupSearchItems.Select(y => y.IID_MLNS_Cha).Distinct().Contains(x.IID_MLNS)))
                {
                    GetListChild(new List<BhCptuBHYTChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

    }
}
