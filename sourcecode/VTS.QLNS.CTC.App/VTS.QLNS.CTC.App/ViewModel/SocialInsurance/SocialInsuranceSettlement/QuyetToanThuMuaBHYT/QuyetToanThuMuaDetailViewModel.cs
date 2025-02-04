using AutoMapper;
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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT
{
    public class QuyetToanThuMuaDetailViewModel : DetailViewModelBase<BhQttmBHYTModel, BhQttmBHYTChiTietModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IQttmBHYTChiTietService _chungTuChiTietService;
        private readonly IQttmBHYTService _chungTuService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;
        private readonly INsDonViService _donViService;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        private ICollectionView _qttmBHYTChiTietModelsView { get; set; }
        private ICollection<BhQttmBHYTChiTietModel> _filterResult = new HashSet<BhQttmBHYTChiTietModel>();
        private string _xnmConcatenation = "";
        private ICollectionView _searchPopupView { get; set; }
        public List<BhQttmBHYTModel> ListIdsVoucherSummary { get; set; }
        public int ILoaiQuyNam { get; set; }
        private BhQttmBHYTModel _ctTongHop;
        public BhQttmBHYTModel CtTongHop
        {
            get => _ctTongHop;
            set => SetProperty(ref _ctTongHop, value);
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

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private ObservableCollection<BhQttmBHYTChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhQttmBHYTChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhQttmBHYTChiTietModel _selectedPopupItem;
        public BhQttmBHYTChiTietModel SelectedPopupItem
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

        private ObservableCollection<BhQttmBHYTChiTietModel> _dataSearch;
        public ObservableCollection<BhQttmBHYTChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
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
                if (SetProperty(ref _selectedTypeShowAgency, value) && IsVoucherSummary)
                {
                    LoadData();
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
                BeForeRefresh();
                _qttmBHYTChiTietModelsView.Refresh();
                CalculateData();
            }
        }

        public bool IsYearVoucher => ILoaiQuyNam.Equals((int)QuarterMonth.YEAR);

        public bool IsShowAgencyFilter => IsVoucherSummary && _selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI;
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
        public PrintQuyetToanThuMuaViewModel PrintQuyetToanThuMuaViewModel { get; }

        public bool IsInit { get; set; }

        public DateTime DtNow => DateTime.Now;

        public QuyetToanThuMuaDetailViewModel(
            IQttmBHYTChiTietService qttmBHYTChiTietService,
            IQttmBHYTService qttmBHYTService,
            ISessionService sessionService,
            IMapper mapper,
            ISysAuditLogService log,
            PrintQuyetToanThuMuaViewModel printQuyetToanThuMuaViewModel,
            INsDonViService nsDonViService)
        {
            _chungTuChiTietService = qttmBHYTChiTietService;
            _chungTuService = qttmBHYTService;
            _sessionService = sessionService;
            _log = log;
            _mapper = mapper;
            _donViService = nsDonViService;
            PrintQuyetToanThuMuaViewModel = printQuyetToanThuMuaViewModel;

            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(OnClose);
            PrintCommand = new RelayCommand(OnPrint);
            SearchCommand = new RelayCommand(obj => SearchDataParent());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            LoadComboboxTypeShow();
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            SearchText = string.Empty;
            IsInit = true;
            LoadData();
            IsInit = false;
            LoadDefault();
        }

        private void LoadComboboxTypeShow()
        {
            TypeShowAgency = new ObservableCollection<ComboboxItem>();
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgency = TypeShowAgency.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedTypeShowAgency));
        }
        private void SearchDataParent()
        {
            SearchTextFilter();
        }

        private void LoadDefault()
        {
            SNoiDungSearch = string.Empty;
            DataSearch = new ObservableCollection<BhQttmBHYTChiTietModel>();
        }

        private void OnClearSearch(object obj)
        {
            LoadDefault();
            _qttmBHYTChiTietModelsView.Refresh();
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
            Func<BhQttmBHYTChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha && x.IsHasData;
            Func<BhQttmBHYTChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
            Func<BhQttmBHYTChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

            var detailsAdd = Items.Where(isAdd).ToList();
            var detailsUpdate = Items.Where(isUpdate).ToList();
            var detailsDelete = Items.Where(isDelete).ToList();

            //thêm mới chứng từ chi tiết
            if (detailsAdd.Count > 0)
            {
                var addItems = new List<BhQttmBHYTChiTiet>();
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
                    var chungTuChiTiet = _chungTuChiTietService.FindById(updateItem.Id);
                    _mapper.Map(updateItem, chungTuChiTiet);
                    _chungTuChiTietService.Update(chungTuChiTiet);
                    updateItem.IsModified = false;
                }
            }
            //cập nhật tổng cộng chứng từ
            var qttmChungTu = _chungTuService.FindById(Model.Id);
            qttmChungTu.FDuToan = Model.FDuToan;
            qttmChungTu.FDaQuyetToan = Model.FDaQuyetToan;
            qttmChungTu.FConLai = Model.FConLai;
            qttmChungTu.FSoPhaiThu = Model.FSoPhaiThu;

            _chungTuService.Update(qttmChungTu);
            OnRefresh();
            _log.WriteLog(Resources.ApplicationName, "Quyết toán thu mua BHYT - chứng từ chi tiết", (int)TypeExecute.Adjust, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
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
            BhQttmBHYTChiTietCriteria searchCondition = new BhQttmBHYTChiTietCriteria();
            searchCondition.INamLamViec = _sessionInfo.YearOfWork;
            searchCondition.IIDMaDonVi = Model.IIDMaDonVi;
            searchCondition.VoucherID = Model.Id;
            searchCondition.SLns = Model.sDSMLNS;
            searchCondition.IQuyNamLoai = Model.IQuyNamLoai;
            searchCondition.IsDonViCha = CheckParentUnit();
            if (_selectedDonVi != null)
            {
                searchCondition.IdDonViFilter = _selectedDonVi.IIDMaDonVi;
                searchCondition.IIDMaDonVi = _selectedDonVi.IIDMaDonVi;
            }

            var temp = new List<BhQttmBHYTChiTiet>();
            if (IsVoucherSummary && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
            {
                var voucherNos = Model.STongHop.Split(",").ToList();
                List<BhQttmBHYTQuery> listChungTu = _chungTuService.FindByCondition(_sessionInfo.YearOfWork).Where(x => voucherNos.Contains(x.SSoChungTu) && x.BDaTongHop.GetValueOrDefault(false)).ToList();
                string agencyIds = string.Join(",", listChungTu.Select(x => x.IIDMaDonVi));
                var listDonVi = _donViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);

                List<BhQttmBHYTChiTiet> listChungTuChiTietParent = new List<BhQttmBHYTChiTiet>();
                List<BhQttmBHYTChiTiet> listChungTuChiTietChildren = new List<BhQttmBHYTChiTiet>();
                foreach (var chungTu in listChungTu)
                {
                    searchCondition.INamLamViec = _sessionInfo.YearOfWork;
                    searchCondition.IIDMaDonVi = chungTu.IIDMaDonVi;
                    searchCondition.VoucherID = chungTu.Id;
                    searchCondition.IQuyNamLoai = chungTu.IQuyNamLoai;
                    searchCondition.IsDonViCha = false;
                    var listQuery = _chungTuChiTietService.FindVoucherDetailByCondition(searchCondition).ToList();
                    listQuery.Where(x => !x.IsHangCha).Select(x => x.STenDonVi = listDonVi.FirstOrDefault(y => y.IIDMaDonVi == chungTu.IIDMaDonVi).TenDonVi).ToList();
                    listChungTuChiTietParent.AddRange(listQuery.Where(x => x.IsHangCha));
                    listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.IsHangCha));
                }
                var listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                listChungTuChiTietParent = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x => x.First()).Distinct().ToList();
                temp.AddRange(listChungTuChiTietParent);
                temp.AddRange(listChungTuChiTietChildren);
                temp = temp.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IIDMaDonVi).ToList();

                //LoadAgencies(agencyIds);
                _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
                OnPropertyChanged(nameof(Agencies));
            }
            else
            {
                temp = _chungTuChiTietService.FindVoucherDetailByCondition(searchCondition).ToList();
            }

            var existBhChiTiet = _chungTuChiTietService.ExistVoucherDetail(Model.Id);
            foreach (var item in temp)
            {
                item.IsAuToFillTuChi = !existBhChiTiet;
            }
            Items = _mapper.Map<ObservableCollection<BhQttmBHYTChiTietModel>>(temp);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhQttmBHYTChiTietModel>>(temp);
            _qttmBHYTChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
            _qttmBHYTChiTietModelsView.Filter = BHQttmBHYTModelsFilter;
            foreach (var khtBhxhChiTietModel in Items)
            {
                khtBhxhChiTietModel.IsFilter = true;
                if (!khtBhxhChiTietModel.IsHangCha)
                {
                    khtBhxhChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        BhQttmBHYTChiTietModel item = (BhQttmBHYTChiTietModel)sender;
                        item.IsModified = true;
                        CalculateData();
                        khtBhxhChiTietModel.IsModified = true;
                        OnPropertyChanged(nameof(IsSaveData));
                    };
                }
            }
            CalculateData();
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            _xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        private bool BHQttmBHYTModelsFilter(object obj)
        {
            bool result = true;
            if (!(obj is BhQttmBHYTChiTietModel temp)) return true;
            var item = (BhQttmBHYTChiTietModel)obj;
            result = VoucherDetailFilter(item);
            if (!result && item.IsHangCha)
            {
                result = _xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = DataSearch.Any(x => x.IIDMLNS.Equals(item.IIDMLNS));
            }

            return result;
        }

        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            var item = obj as BhQttmBHYTChiTietModel;
            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIDMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;
        }

        private void CalculateParent(Guid idParent, BhQttmBHYTChiTietModel item, Dictionary<Guid, BhQttmBHYTChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FDuToan += item.FDuToan;
            model.FDaQuyetToan += item.FDaQuyetToan;
            model.FSoPhaiThu += item.FSoPhaiThu;

            CalculateParent(model.IIDMLNSCha, item, dictByMlns);
        }
        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.FDuToan = 0;
                    x.FDaQuyetToan = 0;
                    x.FSoPhaiThu = 0;
                });

            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IIDMLNS).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IIDMLNSCha, item, dictByMlns);
            }

            UpdateTotal();
        }
        private void UpdateTotal()
        {
            Model.FDuToan = 0;
            Model.FDaQuyetToan = 0;
            Model.FConLai = 0;
            Model.FSoPhaiThu = 0;

            var roots = Items.Where(t => !t.IsHangCha).ToList();
            foreach (var item in roots)
            {
                Model.FDuToan += item.FDuToan;
                Model.FDaQuyetToan += item.FDaQuyetToan;
                Model.FConLai += item.FConLai;
                Model.FSoPhaiThu += item.FSoPhaiThu;
            }
        }
        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnabledDelete));
        }

        private void OnPrint(object param)
        {
            int dialogType = (int)param;
            switch (dialogType)
            {
                case (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD:
                    PrintQuyetToanThuMuaViewModel.SettlementTypeValue = dialogType;
                    PrintQuyetToanThuMuaViewModel.IsEnableInTheo = false;
                    PrintQuyetToanThuMuaViewModel.Init();
                    var view = new PrintQuyetToanThuMua
                    {
                        DataContext = PrintQuyetToanThuMuaViewModel
                    };
                    DialogHost.Show(view, SettlementScreen.DETAIL_DIALOG, null, null);
                    break;
            }
        }

        private bool CheckParentUnit()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            return _donViService.IsDonViCha(Model.IIDMaDonVi, yearOfWork);
        }

        #region Search
        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhQttmBHYTChiTietModel> results = new List<BhQttmBHYTChiTietModel>();

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
                DataSearch = new ObservableCollection<BhQttmBHYTChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhQttmBHYTChiTietModel>();
            }
            _qttmBHYTChiTietModelsView.Refresh();
        }

        private List<BhQttmBHYTChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhQttmBHYTChiTietModel> result = new List<BhQttmBHYTChiTietModel>();
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

        private void GetListChild(List<BhQttmBHYTChiTietModel> lstInput, List<BhQttmBHYTChiTietModel> results)
        {
            var itemChild = Items.Where(x => lstInput.Select(x => x.IIDMLNS).Distinct().Contains(x.IIDMLNSCha)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => Items.Select(y => y.IIDMLNSCha).Distinct().Contains(x.IIDMLNS)))
                {
                    GetListChild(new List<BhQttmBHYTChiTietModel>() { item }, results);
                }
            }
        }
        #endregion
    }
}
