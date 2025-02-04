using AutoMapper;
using ControlzEx.Standard;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanPhanBoDuToanThuBHYT
{
    public class NhanPhanBoDuToanThuBHYTDetailViewModel : DetailViewModelBase<BhDtTmBHYTTNModel, BhDtTmBHYTTNChiTietModel>
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private readonly IBhDtTmBHYTTNService _bhDtTmBHYTTNService;
        private readonly IBhDtTmBHYTTNChiTietService _bhDtTmBHYTTNChiTietService;
        private readonly ISysAuditLogService _log;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IKhtmBHYTChiTietService _khtmService;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        #endregion

        #region Property
        private ICollectionView _dttmBHYTChiTietModelsView { get; set; }
        private BhDtTmBHYTTNModel _ctTongHop;
        public BhDtTmBHYTTNModel CtTongHop
        {
            get => _ctTongHop;
            set => SetProperty(ref _ctTongHop, value);
        }
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

        public bool IsShowAggregatePlanButton => Model.ILoaiDuToan == (int)EstimateTypeNum.YEAR;

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private ObservableCollection<ComboboxItem> _viewSummary = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ViewSummary
        {
            get => _viewSummary;
            set => SetProperty(ref _viewSummary, value);
        }

        private ComboboxItem _viewSummarySelected;

        private ObservableCollection<DonViModel> _donViItems;
        public ObservableCollection<DonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
                LoadData();
            }
        }

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private ObservableCollection<BhDtTmBHYTTNChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhDtTmBHYTTNChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhDtTmBHYTTNChiTietModel _selectedPopupItem;
        public BhDtTmBHYTTNChiTietModel SelectedPopupItem
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

        private ObservableCollection<BhDtTmBHYTTNChiTietModel> _dataSearch;
        public ObservableCollection<BhDtTmBHYTTNChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }

        public bool IsFilterDonVi => _viewSummarySelected != null &&
                                     _viewSummarySelected.ValueItem.Equals(TypeViewSummary.Detail.ToString());
        public bool IsAnotherUserCreate { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public int NamLamViec { get; set; }

        public bool IsInit { get; set; }
        public DateTime DtNow => DateTime.Now;
        #endregion

        #region RelayCommand
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand GetAggregatePlanDataCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }

        #endregion

        #region Constructor
        public NhanPhanBoDuToanThuBHYTDetailViewModel(
            IBhDtTmBHYTTNService bhDtTmBHYTTNService,
            IBhDtTmBHYTTNChiTietService bhDtTmBHYTTNChiTietService,
            ISessionService sessionService,
            IMapper mapper,
            IKhtmBHYTChiTietService iKhtmBHYTChiTietService,
            INsDonViService nsDonViService, ILog logger)
        {
            _bhDtTmBHYTTNChiTietService = bhDtTmBHYTTNChiTietService;
            _bhDtTmBHYTTNService = bhDtTmBHYTTNService;
            _sessionService = sessionService;
            _mapper = mapper;
            _nsDonViService = nsDonViService;
            _logger = logger;
            _khtmService = iKhtmBHYTChiTietService;

            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(OnClose);
            GetAggregatePlanDataCommand = new RelayCommand(obj => GetAggregatePlanData());
            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }
        #endregion

        #region Init
        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            IsInit = true;
            LoadData();
            IsInit = false;
            OnClearSearch(false);
        }
        #endregion

        #region Load data
        public override void LoadData(params object[] args)
        {
            DtTmBHYTTNChiTietCriteria searchCondition = new DtTmBHYTTNChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            searchCondition.IdDonVi = Model.IIDMaDonVi;
            searchCondition.DttmBhytId = Model.Id;
            searchCondition.LstLns = Model.SDSLNS.Split(",").Distinct().ToList();
            if (_selectedDonVi != null)
            {
                searchCondition.IdDonViFilter = _selectedDonVi.IIDMaDonVi;
                searchCondition.IdDonVi = _selectedDonVi.IIDMaDonVi;
            }
            var temp = _bhDtTmBHYTTNChiTietService.FindDttmBHYTChiTietById(searchCondition).ToList();

            var existBhChiTiet = _bhDtTmBHYTTNChiTietService.ExistBHXHChiTiet(Model.Id);
            foreach (var item in temp)
            {
                item.IsAuToFillTuChi = !existBhChiTiet;
            }
            Items = _mapper.Map<ObservableCollection<BhDtTmBHYTTNChiTietModel>>(temp);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhDtTmBHYTTNChiTietModel>>(temp);
            _dttmBHYTChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
            _dttmBHYTChiTietModelsView.Filter = ItemsViewFilter;
            foreach (var dttBhxhChiTietModel in Items)
            {
                dttBhxhChiTietModel.IsFilter = true;
                if (!dttBhxhChiTietModel.IsHangCha)
                {
                    dttBhxhChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        BhDtTmBHYTTNChiTietModel item = (BhDtTmBHYTTNChiTietModel)sender;
                        item.IsModified = true;
                        CalculateData();
                        dttBhxhChiTietModel.IsModified = true;
                        OnPropertyChanged(nameof(IsSaveData));
                    };
                }
                CalculateData();
            }
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
               .ForAll(x =>
               {
                   x.FDuToan = 0;

               });
            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }

            UpdateTotal();
        }

        private void UpdateTotal()
        {
            Model.FDuToan = 0;
            var roots = Items.Where(t => !t.IsHangCha).ToList();
            foreach (var item in roots)
            {
                Model.FDuToan += item.FDuToan;
            }
        }

        private void CalculateParent(Guid idParent, BhDtTmBHYTTNChiTietModel item, Dictionary<Guid, BhDtTmBHYTTNChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FDuToan += item.FDuToan;

            CalculateParent(model.IdParent, item, dictByMlns);
        }

        protected override void OnRefresh()
        {
            IsInit = true;
            LoadData();
            IsInit = false;
        }
        #endregion

        #region On save
        public override void OnSave()
        {
            try
            {
                if (!IsSaveData)
                {
                    return;
                }
                Func<BhDtTmBHYTTNChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
                Func<BhDtTmBHYTTNChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
                Func<BhDtTmBHYTTNChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

                var detailsAdd = Items.Where(isAdd).ToList();
                var detailsUpdate = Items.Where(isUpdate).ToList();
                var detailsDelete = Items.Where(isDelete).ToList();

                //thêm mới chứng từ chi tiết
                if (detailsAdd.Count > 0)
                {
                    var addItems = new List<BhDtTmBHYTTNChiTiet>();
                    _mapper.Map(detailsAdd, addItems);
                    _bhDtTmBHYTTNChiTietService.AddRange(addItems);

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
                        var khtBHXHChiTiet = _bhDtTmBHYTTNChiTietService.FindById(updateItem.Id);
                        _mapper.Map(updateItem, khtBHXHChiTiet);
                        _bhDtTmBHYTTNChiTietService.Update(khtBHXHChiTiet);
                        updateItem.IsModified = false;
                    }
                }

                Guid ID = Model.Id;
                //cập nhật tổng cộng chứng từ
                var dtTmBHYTChungTu = _bhDtTmBHYTTNService.FindById(ID);
                dtTmBHYTChungTu.FDuToan = Model.FDuToan;
                _bhDtTmBHYTTNService.Update(dtTmBHYTChungTu);
                OnRefresh();
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
                UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
                var message = Resources.MsgSaveDone;
                MessageBoxHelper.Info(message);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void GetAggregatePlanData()
        {
            var planData = _khtmService.GetAggregatePlanData(NamLamViec, Model.IIDMaDonVi).ToList();
            if (planData != null)
            {
                var itemFilter = Items.Where(x => !x.IsHangCha && planData.Select(s => s.SXauNoiMa).ToList().Contains(x.SXauNoiMa));
                Parallel.ForEach(itemFilter, item =>
                {
                    item.FDuToan = planData.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => x.FThanhTien.GetValueOrDefault()).FirstOrDefault();
                });
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

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhDtTmBHYTTNChiTietModel)obj;
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = DataSearch.Any(x => x.Id.Equals(item.Id));

            }
            return result;
        }


        private void OnSearch()
        {
            SearchTextFilter();
        }

        private void OnClearSearch(object obj)
        {
            SNoiDungSearch = string.Empty;
            if (!(obj is bool temp))
            {
                _dttmBHYTChiTietModelsView.Refresh();
            }
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhDtTmBHYTTNChiTietModel> results = new List<BhDtTmBHYTTNChiTietModel>();

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
                DataSearch = new ObservableCollection<BhDtTmBHYTTNChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhDtTmBHYTTNChiTietModel>();
            }
            _dttmBHYTChiTietModelsView.Refresh();
        }

        private List<BhDtTmBHYTTNChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhDtTmBHYTTNChiTietModel> result = new List<BhDtTmBHYTTNChiTietModel>();
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

        private void GetListChild(List<BhDtTmBHYTTNChiTietModel> lstInput, List<BhDtTmBHYTTNChiTietModel> results)
        {
            var itemChild = Items.Where(x => lstInput.Select(x => x.IID_MLNS).Distinct().Contains(x.IdParent)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => Items.Select(y => y.IdParent).Distinct().Contains(x.IID_MLNS)))
                {
                    GetListChild(new List<BhDtTmBHYTTNChiTietModel>() { item }, results);
                }
            }
        }
        #endregion
    }
}
