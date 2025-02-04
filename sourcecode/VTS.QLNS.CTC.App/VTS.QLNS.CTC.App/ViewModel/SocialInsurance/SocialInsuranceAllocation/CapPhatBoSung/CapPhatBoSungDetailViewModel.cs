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
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung
{
    public class CapPhatBoSungDetailViewModel : DetailViewModelBase<BhCpBsChungTuModel, BhCpBsChungTuChiTietModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IBhCpBsChungTuChiTietService _chungTuChiTietService;
        private readonly IBhCpBsChungTuService _chungTuService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IBhDmCoSoYTeService _bhDmCoSoYTeService;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;

        public string SoCapTamUng => Model.IQuy switch
        {
            1 => $"Số đã cấp, thanh toán quý 4/{Model.INamLamViec - 1} và tạm ứng quý 1/{Model.INamLamViec}",
            _ => $"Số đã cấp, thanh toán quý {Model.IQuy - 1}/{Model.INamLamViec} và tạm ứng quý {Model.IQuy}/{Model.INamLamViec}"
        };

        private ICollectionView _chungTuChiTietModelsView { get; set; }
        private ICollectionView _searchPopupView { get; set; }
        public List<BhCpBsChungTuModel> ListIdsChungTuSummary { get; set; }
        private BhCpBsChungTuModel _ctTongHop;
        public BhCpBsChungTuModel CtTongHop
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
        private ObservableCollection<BhCpBsChungTuChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhCpBsChungTuChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhCpBsChungTuChiTietModel _selectedPopupItem;
        public BhCpBsChungTuChiTietModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SNoiDungSearch = _selectedPopupItem?.STenMLNS;
                OnPropertyChanged(nameof(SNoiDungSearch));
                IsPopupOpen = false;
            }
        }
        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }
        private ObservableCollection<BhCpBsChungTuChiTietModel> _dataSearch;
        public ObservableCollection<BhCpBsChungTuChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
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

        private bool IsDefault
        {
            get
            {
                return string.IsNullOrEmpty(SearchLNS) && string.IsNullOrEmpty(SNoiDungSearch);
            }
        }

        public bool IsFilterDonVi => _viewSummarySelected != null &&
                                     _viewSummarySelected.ValueItem.Equals(TypeViewSummary.Detail.ToString());
        public bool IsAnotherUserCreate { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsEnabledBtn => !IsLock;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public int NamLamViec { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand CopyCommand { get; }
        public DateTime DtNow => DateTime.Now;

        public CapPhatBoSungDetailViewModel(
            IBhCpBsChungTuChiTietService bhCpBsChungTuChiTietService,
            IBhCpBsChungTuService bhCpBsChungTuService,
            ISessionService sessionService,
            IMapper mapper,
            ISysAuditLogService log,
            INsDonViService nsDonViService,
            IBhDmCoSoYTeService bhDmCoSoYTeService)
        {
            _chungTuChiTietService = bhCpBsChungTuChiTietService;
            _chungTuService = bhCpBsChungTuService;
            _sessionService = sessionService;
            _log = log;
            _mapper = mapper;
            _nsDonViService = nsDonViService;
            _bhDmCoSoYTeService = bhDmCoSoYTeService;

            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SearchCommand = new RelayCommand(OnSearch);
            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(OnClose);
            PrintCommand = new RelayCommand(OnPrint);
            CopyCommand = new RelayCommand(obj => OnCopy());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            if (Model != null)
            {
                IsLock = Model.BKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            LoadData();
            LoadDataAgency();
            OnClearSearch(false);
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

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null || IsLock)
                return;
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
            Func<BhCpBsChungTuChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
            Func<BhCpBsChungTuChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
            Func<BhCpBsChungTuChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

            var detailsAdd = Items.Where(isAdd).ToList();
            var detailsUpdate = Items.Where(isUpdate).ToList();
            var detailsDelete = Items.Where(isDelete).ToList();

            //thêm mới chứng từ chi tiết
            if (detailsAdd.Count > 0)
            {
                var addItems = new List<BhCpBsChungTuChiTiet>();
                _mapper.Map(detailsAdd, addItems);
                _chungTuChiTietService.AddRange(addItems);

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
                    var khtBHXHChiTiet = _chungTuChiTietService.FindById(updateItem.Id);
                    _mapper.Map(updateItem, khtBHXHChiTiet);
                    _chungTuChiTietService.Update(khtBHXHChiTiet);
                    updateItem.IsModified = false;
                }
            }
            //cập nhật tổng cộng chứng từ
            var chungTu = _chungTuService.FindById(Model.Id);
            chungTu.FTongDaQuyetToan = Model.FTongDaQuyetToan;
            chungTu.FTongDaCapUng = Model.FTongDaCapUng;
            chungTu.FTongThuaThieu = Model.FTongThuaThieu;
            chungTu.FTongSoCapBoSung = Model.FTongSoCapBoSung;
            _chungTuService.Update(chungTu);

            _log.WriteLog(Resources.ApplicationName, "Cấp phát bổ sung KP KCB BHYT - Chứng từ chi tiết", (int)TypeExecute.Adjust, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
            OnRefresh();
            var message = Resources.MsgSaveDone;
            MessageBoxHelper.Info(message);
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        public bool IsVoucherSummary { get; set; }

        public override void LoadData(params object[] args)
        {
            List<BhCpBsChungTuChiTiet> temp = new List<BhCpBsChungTuChiTiet>();
            BhCpBsChungTuChiTietCriteria searchCondition = new BhCpBsChungTuChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            searchCondition.IIDCTCapPhatBS = Model.Id;
            searchCondition.LstLns = Model.SDslns.Split(",").Distinct().ToList();
            searchCondition.LstCSYT = Model.SCoSoYTe;
            searchCondition.IQuy = Model.IQuy;
            searchCondition.IdDonVi = _sessionInfo.IdDonVi;
            if (_selectedDonVi != null)
            {
                searchCondition.IdDonViFilter = _selectedDonVi.IIDMaDonVi;
                searchCondition.IdDonVi = _selectedDonVi.IIDMaDonVi;
            }

            //if (IsVoucherSummary)
            //{
            //    var voucherNos = Model.SDSSoChungTuTongHop.Split(",").ToList();
            //    List<BhCpBsChungTu> listChungTu = _chungTuService.FindByAggregateVoucher(voucherNos, _sessionInfo.YearOfWork).ToList();
            //    List<BhCpBsChungTuChiTiet> listChungTuChiTietParent = new List<BhCpBsChungTuChiTiet>();
            //    List<BhCpBsChungTuChiTiet> listChungTuChiTietChildren = new List<BhCpBsChungTuChiTiet>();
            //    foreach (var chungTu in listChungTu)
            //    {
            //        searchCondition.IIDCTCapPhatBS = chungTu.Id;
            //        searchCondition.IdDonVi = chungTu.IIDMaDonVi;
            //        var listQuery = _chungTuChiTietService.FindVoucherDetailByCondition(searchCondition).ToList();
            //        listChungTuChiTietParent.AddRange(listQuery.Where(x => x.IsHangCha));
            //        listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.IsHangCha));
            //    }
            //    var listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
            //    listChungTuChiTietParent = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x => x.First()).Distinct().ToList();
            //    temp.AddRange(listChungTuChiTietParent);
            //    temp.AddRange(listChungTuChiTietChildren);
            //    temp = temp.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IIdMaDonVi).ToList();
            //}

            temp = _chungTuChiTietService.FindVoucherDetailByCondition(searchCondition).OrderBy(x => x.IIDMaCoSoYTe).ToList();

            var existBhChiTiet = _chungTuChiTietService.ExistVoucherDetail(Model.Id);
            if (!temp.IsEmpty())
            {
                temp = temp.Select(x =>
                {
                    x.IsAuToFillTuChi = !existBhChiTiet;
                    x.FDaCapUng = Math.Round(x.FDaCapUng.GetValueOrDefault());
                    return x;
                }).ToList();
            }
            Items = _mapper.Map<ObservableCollection<BhCpBsChungTuChiTietModel>>(temp);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhCpBsChungTuChiTietModel>>(temp);
            _chungTuChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
            _chungTuChiTietModelsView.Filter = ItemsViewFilter;
            foreach (var khtBhxhChiTietModel in Items)
            {
                khtBhxhChiTietModel.IsFilter = true;
                khtBhxhChiTietModel.NamLamViec = _sessionInfo.YearOfWork;
                if (khtBhxhChiTietModel.FDaQuyetToan < khtBhxhChiTietModel.FDaCapUng)
                {
                    khtBhxhChiTietModel.SGhiChu = $"KP thừa sau QT quý {Model.IQuy}: {(khtBhxhChiTietModel.FDaCapUng - khtBhxhChiTietModel.FDaQuyetToan ?? 0).ToString("N0")}";
                }
                else if (khtBhxhChiTietModel.SGhiChu != null && khtBhxhChiTietModel.SGhiChu.Contains("KP thừa sau QT quý"))
                {
                    khtBhxhChiTietModel.SGhiChu = string.Empty;
                }
                if (!khtBhxhChiTietModel.IsHangCha)
                {
                    khtBhxhChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        var item = sender as BhCpBsChungTuChiTietModel;
                        item.IsModified = true;

                        if (args.PropertyName == nameof(BhCpBsChungTuChiTietModel.FDaQuyetToan) || args.PropertyName == nameof(BhCpBsChungTuChiTietModel.FDaCapUng))
                        {
                            if (item.FDaQuyetToan < item.FDaCapUng)
                            {
                                item.SGhiChu = $"KP thừa sau QT quý {Model.IQuy}: {(item.FDaCapUng - item.FDaQuyetToan ?? 0).ToString("N0")}";
                            }
                            else if (item.SGhiChu != null && item.SGhiChu.Contains("KP thừa sau QT quý"))
                            {
                                item.SGhiChu = string.Empty;
                            }
                        }
                        if (args.PropertyName != nameof(BhCpBsChungTuChiTietModel.SGhiChu))
                        {
                            CalculateData();
                        }
                        khtBhxhChiTietModel.IsModified = true;
                        OnPropertyChanged(nameof(IsSaveData));
                    };
                }
            }
            CalculateData();
        }
        private void CalculateParent(Guid idParent, BhCpBsChungTuChiTietModel item, Dictionary<Guid, BhCpBsChungTuChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FDaQuyetToan += item.FDaQuyetToan;
            model.FDaCapUng += item.FDaCapUng;
            model.FThua += item.FThua;
            model.FThieu += item.FThieu;
            model.FSoCapBoSung += item.FSoCapBoSung;

            CalculateParent(model.IdParent, item, dictByMlns);
        }
        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.FDaQuyetToan = 0;
                    x.FDaCapUng = 0;
                    x.FThieu = 0;
                    x.FThua = 0;
                    x.FSoCapBoSung = 0;
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
            Model.FTongDaQuyetToan = 0;
            Model.FTongDaCapUng = 0;
            Model.FTongThua = 0;
            Model.FTongThieu = 0;
            Model.FTongSoCapBoSung = 0;

            var roots = Items.Where(t => !t.IsChildDeleted && !t.IsHangCha).ToList();
            foreach (var item in roots)
            {
                Model.FTongDaQuyetToan += item.FDaQuyetToan;
                Model.FTongDaCapUng += item.FDaCapUng;
                Model.FTongThua += item.FThua;
                Model.FTongThieu += item.FThieu;
                Model.FTongSoCapBoSung += item.FSoCapBoSung;
            }
        }
        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnabledDelete));
        }

        private void OnPrint(object param)
        {

        }

        #region Search

        private void OnSearch(object obj)
        {
            SearchTextFilter();
        }

        private void OnClearSearch(object obj)
        {
            SearchLNS = string.Empty;
            SNoiDungSearch = string.Empty;
            SelectedAgency = null;
            if (!(obj is bool temp))
            {
                _chungTuChiTietModelsView.Refresh();
            }
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhCpBsChungTuChiTietModel)obj;

            if (!IsDefault)
            {
                result = DataSearch.Any(x => x.IIdFilter.Equals(item.IIdFilter));
            }
            result = result && ChungTuChiTietItemsViewFilter(item);
            if (result)
                item.IsFilter = result;
            return result;
        }

        private bool ChungTuChiTietItemsViewFilter(BhCpBsChungTuChiTietModel item)
        {
            bool result = true;
            if (SelectedAgency != null)
            {
                result = result && !string.IsNullOrEmpty(item.IIDMaCoSoYTe) && item.IIDMaCoSoYTe.Equals(SelectedAgency.ValueItem);
            }
            return result;
        }

        private void SearchTextFilter()
        {
            List<string> lstParents = new List<string>();
            List<BhCpBsChungTuChiTietModel> lstChildSearch = new List<BhCpBsChungTuChiTietModel>();
            List<BhCpBsChungTuChiTietModel> lstParentSearch = new List<BhCpBsChungTuChiTietModel>();
            List<BhCpBsChungTuChiTietModel> results = new List<BhCpBsChungTuChiTietModel>();
            if (!string.IsNullOrEmpty(SearchLNS))
            {
                lstChildSearch = Items.Where(x => x.SLns.ToLower().Contains(SearchLNS.ToLower()) && !x.IsHangCha).ToList();
                lstParentSearch = Items.Where(x => x.SLns.ToLower().Contains(SearchLNS.ToLower()) && x.IsHangCha).ToList();
            }

            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                if (!string.IsNullOrEmpty(SearchLNS))
                {
                    lstChildSearch = lstChildSearch.Where(x => x.STenMLNS.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).ToList();
                    lstParentSearch = lstParentSearch.Where(x => x.STenMLNS.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).ToList();
                }
                else
                {
                    lstChildSearch = Items.Where(x => x.STenMLNS.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).ToList();
                    lstParentSearch = Items.Where(x => x.STenMLNS.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).ToList();
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
            DataSearch = new ObservableCollection<BhCpBsChungTuChiTietModel>(results);
            _chungTuChiTietModelsView.Refresh();
        }

        private List<BhCpBsChungTuChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhCpBsChungTuChiTietModel> result = new List<BhCpBsChungTuChiTietModel>();
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

        private void GetListChild(List<BhCpBsChungTuChiTietModel> lstInput, List<BhCpBsChungTuChiTietModel> results)
        {
            var itemChild = DataPopupSearchItems.Where(x => lstInput.Select(x => x.IIdMlns).Distinct().Contains(x.IIdMlnsCha ?? Guid.Empty)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => DataPopupSearchItems.Select(y => y.IIdMlnsCha).Distinct().Contains(x.IIdMlns)))
                {
                    GetListChild(new List<BhCpBsChungTuChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

        private void OnCopy()
        {
            foreach (var item in Items)
            {
                if (item.FThieu > 0)
                {
                    item.FSoCapBoSung = Math.Abs(item.FThieu.GetValueOrDefault());
                }
            }
        }
    }
}
