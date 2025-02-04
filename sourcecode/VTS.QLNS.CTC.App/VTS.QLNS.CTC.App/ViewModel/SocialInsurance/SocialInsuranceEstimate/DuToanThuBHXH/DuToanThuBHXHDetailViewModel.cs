using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DuToanThuBHXH
{
    public class DuToanThuBHXHDetailViewModel : DetailViewModelBase<BhDttBHXHModel, BhDttBHXHChiTietModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IDttBHXHChiTietService _dttBHXHChiTietService;
        private readonly IDttBHXHService _dttBHXHService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IKhtBHXHChiTietService _khtService;
        private readonly IBhDcDuToanThuChiTietService _dcDttService;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;

        private ICollectionView _dttBHXHChiTietModelsView { get; set; }
        private ICollectionView _searchPopupView { get; set; }
        public List<BhDttBHXHModel> ListIdsDttBHXHSummary { get; set; }
        private BhDttBHXHModel _ctTongHop;
        public BhDttBHXHModel CtTongHop
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
        public bool IsShowAggregateAdjustButton => Model.ILoaiDuToan == (int)EstimateTypeNum.ADDITIONAL;

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

        private ObservableCollection<BhDttBHXHChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhDttBHXHChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhDttBHXHChiTietModel _selectedPopupItem;
        public BhDttBHXHChiTietModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SNoiDungSearch = _selectedPopupItem?.STenBhMLNS;
                OnPropertyChanged(nameof(SNoiDungSearch));
                IsPopupOpen = false;
            }
        }

        private ObservableCollection<BhDttBHXHChiTietModel> _dataSearch;
        public ObservableCollection<BhDttBHXHChiTietModel> DataSearch
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
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand CanCuCommand { get; }
        public RelayCommand LaySummaryCanCuCommand { get; }
        public RelayCommand GetAggregatePlanDataCommand { get; }
        public RelayCommand GetAggregateAdjustDataCommand { get; }

        public Visibility QSBQNamVisible { get; set; }
        public Visibility LuongChinhVisible { get; set; }
        public Visibility PCChucVuVisible { get; set; }
        public Visibility PCTNNgheVisible { get; set; }
        public Visibility PCTNVKVisible { get; set; }
        public Visibility NghiOmVisible { get; set; }

        public Visibility CongLuongVisible { get; set; }
        public Visibility NLDDongBHXHVisible { get; set; }
        public Visibility NSDDongBHXHVisible { get; set; }
        public Visibility CongBHXHVisible { get; set; }
        public Visibility NLDDongBHYTVisible { get; set; }
        public Visibility NSDDongBHYTVisible { get; set; }
        public Visibility CongBHYTVisible { get; set; }
        public Visibility NLDDongBHTNVisible { get; set; }
        public Visibility NSDDongBHTNVisible { get; set; }
        public Visibility CongBHTNVisible { get; set; }
        public bool IsInit { get; set; }

        public DateTime DtNow => DateTime.Now;

        public DuToanThuBHXHDetailViewModel(
            IDttBHXHChiTietService dttBHXHChiTietService,
            IDttBHXHService dttBHXHService,
            ISessionService sessionService,
            IMapper mapper,
            ISysAuditLogService log,
            INsDonViService nsDonViService,
            IKhtBHXHChiTietService iKhtBHXHChiTietService,
            IBhDcDuToanThuChiTietService iBhDcDuToanThuChiTietService)
        {
            _dttBHXHChiTietService = dttBHXHChiTietService;
            _dttBHXHService = dttBHXHService;
            _sessionService = sessionService;
            _log = log;
            _mapper = mapper;
            _nsDonViService = nsDonViService;
            _khtService = iKhtBHXHChiTietService;
            _dcDttService = iBhDcDuToanThuChiTietService;

            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SearchCommand = new RelayCommand(OnSearch);
            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(OnClose);
            GetAggregatePlanDataCommand = new RelayCommand(obj => GetAggregatePlanData());
            GetAggregateAdjustDataCommand = new RelayCommand(obj => GetAggregateAdjustData());
        }

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

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null || IsLock) return;
            if (Model.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, Model.SNguoiTao));
                return;
            }
            SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
        }

        public override void OnSave()
        {
            if (!IsSaveData)
            {
                return;
            }
            Func<BhDttBHXHChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
            Func<BhDttBHXHChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
            Func<BhDttBHXHChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

            var detailsAdd = Items.Where(isAdd).ToList();
            var detailsUpdate = Items.Where(isUpdate).ToList();
            var detailsDelete = Items.Where(isDelete).ToList();

            //Thêm mới chứng từ chi tiết
            if (detailsAdd.Count > 0)
            {
                var addItems = new List<BhDttBHXHChiTiet>();
                _mapper.Map(detailsAdd, addItems);
                _dttBHXHChiTietService.AddRange(addItems);

                Items.Where(isAdd).Select(x =>
                {
                    x.IsModified = false;
                    x.IsAdd = false;
                    return x;
                }).ToList();
            }

            //Cập nhật chứng từ chi tiết
            if (detailsUpdate.Count > 0)
            {
                foreach (var updateItem in detailsUpdate)
                {
                    var dttBHXHChiTiet = _dttBHXHChiTietService.FindById(updateItem.Id);
                    _mapper.Map(updateItem, dttBHXHChiTiet);
                    _dttBHXHChiTietService.Update(dttBHXHChiTiet);
                    updateItem.IsModified = false;
                }
            }
            //cập nhật tổng cộng chứng từ
            var bhDttChungTu = _dttBHXHService.FindById(Model.Id);
            bhDttChungTu.FThuBHXHNLDDong = Model.FThuBHXHNLDDong;
            bhDttChungTu.FThuBHXHNSDDong = Model.FThuBHXHNSDDong;
            bhDttChungTu.FThuBHXH = Model.FThuBHXH;
            bhDttChungTu.FThuBHYTNLDDong = Model.FThuBHYTNLDDong;
            bhDttChungTu.FThuBHYTNSDDong = Model.FThuBHYTNSDDong;
            bhDttChungTu.FTongBHYT = Model.FTongBHYT;
            bhDttChungTu.FThuBHTNNLDDong = Model.FThuBHTNNLDDong;
            bhDttChungTu.FThuBHTNNSDDong = Model.FThuBHTNNSDDong;
            bhDttChungTu.FThuBHTN = Model.FThuBHTN;
            bhDttChungTu.FDuToan = Model.FDuToan;
            _dttBHXHService.Update(bhDttChungTu);

            _log.WriteLog(Resources.ApplicationName, "Số nhu cầu - chứng từ chi tiết", (int)TypeExecute.Adjust, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
            var message = Resources.MsgSaveDone;
            MessageBoxHelper.Info(message);
        }

        protected override void OnRefresh()
        {
            IsInit = true;
            LoadData();
            IsInit = false;
        }

        public bool IsVoucherSummary { get; set; }

        public override void LoadData(params object[] args)
        {
            DttBHXHChiTietCriteria searchCondition = new DttBHXHChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            searchCondition.IdDonVi = Model.IIDMaDonVi;
            searchCondition.DtttBhxhId = Model.Id;
            var sDSLNS = Model.SDslns ?? "";
            searchCondition.LstLns = sDSLNS.Split(",").Distinct().ToList();
            if (_selectedDonVi != null)
            {
                searchCondition.IdDonViFilter = _selectedDonVi.IIDMaDonVi;
                searchCondition.IdDonVi = _selectedDonVi.IIDMaDonVi;
            }
            var temp = _dttBHXHChiTietService.FindDttBHXHChiTietByIdBhxh(searchCondition).ToList();

            var existBhChiTiet = _dttBHXHChiTietService.ExistBHXHChiTiet(Model.Id);
            foreach (var item in temp)
            {
                item.IsAuToFillTuChi = !existBhChiTiet;
            }
            Items = _mapper.Map<ObservableCollection<BhDttBHXHChiTietModel>>(temp);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhDttBHXHChiTietModel>>(temp);
            _dttBHXHChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
            _dttBHXHChiTietModelsView.Filter = ItemsViewFilter;
            foreach (var dttBhxhChiTietModel in Items)
            {
                dttBhxhChiTietModel.IsFilter = true;
                if (!dttBhxhChiTietModel.IsHangCha)
                {
                    dttBhxhChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhDttBHXHChiTietModel.FThuBHXHNguoiLaoDong) || args.PropertyName == nameof(BhDttBHXHChiTietModel.FThuBHXHNguoiSuDungLaoDong)
                            || args.PropertyName == nameof(BhDttBHXHChiTietModel.FThuBHYTNguoiLaoDong) || args.PropertyName == nameof(BhDttBHXHChiTietModel.FThuBHYTNguoiSuDungLaoDong)
                            || args.PropertyName == nameof(BhDttBHXHChiTietModel.FThuBHTNNguoiLaoDong) || args.PropertyName == nameof(BhDttBHXHChiTietModel.FThuBHTNNguoiSuDungLaoDong))
                        {
                            BhDttBHXHChiTietModel item = (BhDttBHXHChiTietModel)sender;
                            item.IsModified = true;
                            CalculateData();
                            dttBhxhChiTietModel.IsModified = true;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                }
                CalculateData();
            }
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhDttBHXHChiTietModel)obj;
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = DataSearch.Any(x => x.Id.Equals(item.Id));
            }
            return result;
        }

        private void CalculateParent(Guid idParent, BhDttBHXHChiTietModel item, Dictionary<Guid, BhDttBHXHChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FThuBHXHNguoiLaoDong += item.FThuBHXHNguoiLaoDong;
            model.FThuBHXHNguoiSuDungLaoDong += item.FThuBHXHNguoiSuDungLaoDong;
            model.FThuBHYTNguoiLaoDong += item.FThuBHYTNguoiLaoDong;
            model.FThuBHYTNguoiSuDungLaoDong += item.FThuBHYTNguoiSuDungLaoDong;
            model.FThuBHTNNguoiLaoDong += item.FThuBHTNNguoiLaoDong;
            model.FThuBHTNNguoiSuDungLaoDong += item.FThuBHTNNguoiSuDungLaoDong;

            CalculateParent(model.IdParent, item, dictByMlns);
        }
        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.FThuBHXHNguoiLaoDong = 0;
                    x.FThuBHXHNguoiSuDungLaoDong = 0;
                    x.FThuBHYTNguoiLaoDong = 0;
                    x.FThuBHYTNguoiSuDungLaoDong = 0;
                    x.FThuBHTNNguoiLaoDong = 0;
                    x.FThuBHTNNguoiSuDungLaoDong = 0;
                });

            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IIdMlns).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }

            UpdateTotal();
        }
        private void UpdateTotal()
        {
            Model.FThuBHXHNLDDong = 0;
            Model.FThuBHXHNSDDong = 0;
            Model.FThuBHXH = 0;
            Model.FThuBHYTNLDDong = 0;
            Model.FThuBHYTNSDDong = 0;
            Model.FTongBHYT = 0;
            Model.FThuBHTNNLDDong = 0;
            Model.FThuBHTNNSDDong = 0;
            Model.FThuBHTN = 0;
            Model.FDuToan = 0;

            var roots = Items.Where(t => !t.IsHangCha).ToList();
            foreach (var item in roots)
            {
                Model.FThuBHXHNLDDong += item.FThuBHXHNguoiLaoDong;
                Model.FThuBHXHNSDDong += item.FThuBHXHNguoiSuDungLaoDong;
                Model.FThuBHXH += item.FTongThuBHXH;
                Model.FThuBHYTNLDDong += item.FThuBHYTNguoiLaoDong;
                Model.FThuBHYTNSDDong += item.FThuBHYTNguoiSuDungLaoDong;
                Model.FTongBHYT += item.FTongThuBHYT;
                Model.FThuBHTNNLDDong += item.FThuBHTNNguoiLaoDong;
                Model.FThuBHTNNSDDong += item.FThuBHTNNguoiSuDungLaoDong;
                Model.FThuBHTN += item.FTongThuBHTN;
                Model.FDuToan += item.FTongCong;
            }
        }
        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnabledDelete));
        }
        public void GetAggregatePlanData()
        {
            var planData = _khtService.GetAggregatePlanData(NamLamViec, Model.IIDMaDonVi).ToList();
            if (planData != null)
            {
                var itemFilter = Items.Where(x => !x.IsHangCha && planData.Select(s => s.XauNoiMa).ToList().Contains(x.SXauNoiMa));
                Parallel.ForEach(itemFilter, item =>
                {
                    item.FThuBHXHNguoiLaoDong = planData.Where(x => x.XauNoiMa == item.SXauNoiMa).Select(x => x.FThuBHXHNguoiLaoDong.GetValueOrDefault()).FirstOrDefault();
                    item.FThuBHXHNguoiSuDungLaoDong = planData.Where(x => x.XauNoiMa == item.SXauNoiMa).Select(x => x.FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault()).FirstOrDefault();
                    item.FThuBHYTNguoiLaoDong = planData.Where(x => x.XauNoiMa == item.SXauNoiMa).Select(x => x.FThuBHYTNguoiLaoDong.GetValueOrDefault()).FirstOrDefault();
                    item.FThuBHYTNguoiSuDungLaoDong = planData.Where(x => x.XauNoiMa == item.SXauNoiMa).Select(x => x.FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault()).FirstOrDefault();
                    item.FThuBHTNNguoiLaoDong = planData.Where(x => x.XauNoiMa == item.SXauNoiMa).Select(x => x.FThuBHTNNguoiLaoDong.GetValueOrDefault()).FirstOrDefault();
                    item.FThuBHTNNguoiSuDungLaoDong = planData.Where(x => x.XauNoiMa == item.SXauNoiMa).Select(x => x.FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault()).FirstOrDefault();
                });
            }
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
                _dttBHXHChiTietModelsView.Refresh();
            }
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhDttBHXHChiTietModel> results = new List<BhDttBHXHChiTietModel>();

                List<string> lstSXaNoiMaChildSearch = Items.Where(x => x.STenBhMLNS.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                List<string> lstSXaNoiMaParentSearch = Items.Where(x => x.STenBhMLNS.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
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
                DataSearch = new ObservableCollection<BhDttBHXHChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhDttBHXHChiTietModel>();
            }
            _dttBHXHChiTietModelsView.Refresh();
        }

        private List<BhDttBHXHChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhDttBHXHChiTietModel> result = new List<BhDttBHXHChiTietModel>();
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

        private void GetListChild(List<BhDttBHXHChiTietModel> lstInput, List<BhDttBHXHChiTietModel> results)
        {
            var itemChild = Items.Where(x => lstInput.Select(x => x.IIdMlns).Distinct().Contains(x.IIdMlnsCha ?? Guid.Empty)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => Items.Select(y => y.IIdMlnsCha).Distinct().Contains(x.IIdMlns)))
                {
                    GetListChild(new List<BhDttBHXHChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

        private void GetAggregateAdjustData()
        {
            var adjustData = _dcDttService.GetAggregateAdjustData(NamLamViec, Model.IIDMaDonVi).ToList();
            if (adjustData != null)
            {
                var itemFilter = Items.Where(x => !x.IsHangCha);

                Parallel.ForEach(itemFilter, item =>
                {
                    item.FThuBHXHNguoiLaoDong = adjustData.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => x.FThuBHXHNLDTang.GetValueOrDefault() > 0 ? x.FThuBHXHNLDTang.GetValueOrDefault() : x.FThuBHXHNLDGiam.GetValueOrDefault()).FirstOrDefault();
                    item.FThuBHXHNguoiSuDungLaoDong = adjustData.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => x.FThuBHXHNSDTang.GetValueOrDefault() > 0 ? x.FThuBHXHNSDTang.GetValueOrDefault() : x.FThuBHXHNSDGiam.GetValueOrDefault()).FirstOrDefault();
                    item.FThuBHYTNguoiLaoDong = adjustData.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => x.FThuBHYTNLDTang.GetValueOrDefault() > 0 ? x.FThuBHYTNLDTang.GetValueOrDefault() : x.FThuBHYTNLDGiam.GetValueOrDefault()).FirstOrDefault();
                    item.FThuBHYTNguoiSuDungLaoDong = adjustData.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => x.FThuBHYTNSDTang.GetValueOrDefault() > 0 ? x.FThuBHYTNSDTang.GetValueOrDefault() : x.FThuBHYTNSDGiam.GetValueOrDefault()).FirstOrDefault();
                    item.FThuBHTNNguoiLaoDong = adjustData.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => x.FThuBHTNNLDTang.GetValueOrDefault() > 0 ? x.FThuBHTNNLDTang.GetValueOrDefault() : x.FThuBHTNNLDGiam.GetValueOrDefault()).FirstOrDefault();
                    item.FThuBHTNNguoiSuDungLaoDong = adjustData.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => x.FThuBHTNNSDTang.GetValueOrDefault() > 0 ? x.FThuBHTNNSDTang.GetValueOrDefault() : x.FThuBHTNNSDGiam.GetValueOrDefault()).FirstOrDefault();
                });
            }
        }
    }
}
