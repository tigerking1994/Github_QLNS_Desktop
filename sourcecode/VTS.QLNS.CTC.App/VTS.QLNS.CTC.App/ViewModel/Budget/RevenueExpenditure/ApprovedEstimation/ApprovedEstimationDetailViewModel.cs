using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.ApprovedEstimation;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.RevenueExpenditureType;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.App.Helper;
using System.Windows;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.ApprovedEstimation
{
    public class ApprovedEstimationDetailViewModel : DetailViewModelBase<TnDtChungTuModel, TnDtChungTuChiTietModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly ITnDtChungTuChiTietService _tnDtChungTuChiTietService;
        private readonly ITnDtChungTuService _tnDtChungTuService;
        private readonly ILog _logger;
        private EstimationVoucherDetailCriteria _searchCondition;
        private ICollectionView _tnDtChungTuChiTietView;
        private ICollectionView _budgetCatalogItemsView;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateSettlementVoucherEvent;

        public override Type ContentType => typeof(ApprovedEstimationDetail);
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);

        public int NamLamViec { get; set; }

        private ObservableCollection<ComboboxItem> _typeDisplays;
        public ObservableCollection<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _budgetCatalogItemsView.Refresh();
                }
            }
        }

        private ObservableCollection<TnDtChungTuChiTietModel> _budgetCatalogItems;
        public ObservableCollection<TnDtChungTuChiTietModel> BudgetCatalogItems
        {
            get => _budgetCatalogItems;
            set => SetProperty(ref _budgetCatalogItems, value);
        }

        private TnDtChungTuChiTietModel _selectedBudgetCatalog;
        public TnDtChungTuChiTietModel SelectedBudgetCatalog
        {
            get => _selectedBudgetCatalog;
            set
            {
                if (SetProperty(ref _selectedBudgetCatalog, value))
                {
                    if (_selectedBudgetCatalog != null)
                        SelectedLNS = _selectedBudgetCatalog.Lns;
                    _tnDtChungTuChiTietView.Refresh();
                }
                CalculateTotal();
                IsOpenLnsPopup = false;
            }
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

        private string _selectedLNS;
        public string SelectedLNS
        {
            get => _selectedLNS;
            set => SetProperty(ref _selectedLNS, value);
        }

        private EstimationDetailCriteria _detailFilter;
        public EstimationDetailCriteria DetailFilter
        {
            get => _detailFilter;
            set => SetProperty(ref _detailFilter, value);
        }

        private string _typeDisplaysselected;
        public string TypeDisplaysSelected
        {
            get => _typeDisplaysselected;
            set
            {
                if (SetProperty(ref _typeDisplaysselected, value) && _tnDtChungTuChiTietView != null)
                {
                    _tnDtChungTuChiTietView.Refresh();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _listSpecialized;
        public ObservableCollection<ComboboxItem> ListSpecialized
        {
            get => _listSpecialized;
            set => SetProperty(ref _listSpecialized, value);
        }

        private ComboboxItem _specializedSelected;
        public ComboboxItem SpecializedSelected
        {
            get => _specializedSelected;
            set
            {
                SetProperty(ref _specializedSelected, value);
                if (_tnDtChungTuChiTietView != null && SpecializedSelected != null)
                {
                    GetListParentFilter();
                    _tnDtChungTuChiTietView.Refresh();
                }
                else if (_tnDtChungTuChiTietView != null && SpecializedSelected == null)
                {
                    _tnDtChungTuChiTietView.Refresh();
                }
            }
        }

        public List<TnDtChungTuChiTietModel> ListParentFilter;
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public new RelayCommand CloseCommand { get; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand SaveDataCommand { get; }

        public ApprovedEstimationDetailViewModel(IMapper mapper,
            ISessionService sessionService,
            IDanhMucService danhMucService,
            ITnDtChungTuChiTietService tnDtChungTuChiTietService,
            ITnDtChungTuService tnDtChungTuService,
            ILog logger)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _tnDtChungTuChiTietService = tnDtChungTuChiTietService;
            _tnDtChungTuService = tnDtChungTuService;
            _logger = logger;

            SearchCommand = new RelayCommand(obj => { _tnDtChungTuChiTietView.Refresh(); CalculateTotal(); });
            ResetFilterCommand = new RelayCommand(obj => OnReSetFilter());
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            CloseCommand = new RelayCommand(OnClose);
        }

        public override void Init()
        {
            NamLamViec = _sessionService.Current.YearOfWork;
            LoadTypeDisplay();
            ResetConditionSearch();
            LoadSpecialized();
            LoadData();
            LoadLNSIndexCondition();
        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Đã nhập dữ liệu" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Hiển thị tất cả" });
            TypeDisplaysSelected = TypeDisplay.TAT_CA;
        }

        private void ResetConditionSearch()
        {
            DetailFilter = new EstimationDetailCriteria();
            _searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Model.Id,
                LNS = Model.Lns,
                YearOfWork = Model.NamLamViec,
                YearOfBudget = Model.NamNganSach,
                VoucherDate = Model.NgayChungTu,
                BudgetSource = Model.NguonNganSach
            };
        }

        private void LoadSpecialized()
        {
            try
            {
                IEnumerable<DanhMuc> listDanhMuc = _danhMucService.FindByType(VoucherType.VOCHER_TYPE, _sessionService.Current.YearOfWork);
                ListSpecialized = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMuc);

                if (ListSpecialized != null && ListSpecialized.Count() > 0)
                    SpecializedSelected = ListSpecialized.FirstOrDefault();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void GetListParentFilter()
        {
            ListParentFilter = new List<TnDtChungTuChiTietModel>();
            if (SpecializedSelected != null && !string.IsNullOrEmpty(SpecializedSelected.ValueItem))
            {
                List<TnDtChungTuChiTietModel> listChild = Items.Where(x => SpecializedSelected.ValueItem.Contains(x.Ng) && !string.IsNullOrEmpty(x.Ng)).ToList();
                if (listChild != null && listChild.Count > 0)
                {
                    ListParentFilter = Items.Where(n => n.IsHangCha && listChild.Any(m => m.XauNoiMa.Contains(n.XauNoiMa))).ToList();
                }
            }
        }


        private void ProcessDynamicMLNS()
        {
            List<string> chiTietTois = Items.Where(x => !string.IsNullOrEmpty(x.SChiTietToi)).Select(x => x.SChiTietToi).ToList();
            ColumnDisplay = DynamicMLNS.SettingColumnVisibility(chiTietTois);
        }

        private void LoadData()
        {
            try
            {
                List<TnDtChungTuChiTietQuery> chungTuChiTiet = _tnDtChungTuChiTietService.FindByApprovedAndPlanEstimationCondition(_searchCondition, RevenueAndExpenditureType.ApprovedEstimation).ToList();

                Items = _mapper.Map<ObservableCollection<TnDtChungTuChiTietModel>>(chungTuChiTiet);

                GetListParentFilter();
                ProcessDynamicMLNS();
                _tnDtChungTuChiTietView = CollectionViewSource.GetDefaultView(Items);
                _tnDtChungTuChiTietView.Filter = ChungTuChiTietItemsViewFilter;

                CalculateData();

                if (Items != null)
                {
                    SelectedItem = Items.Where(x => !x.IsHangCha).FirstOrDefault();
                }
                foreach (var model in Items)
                {
                    if (model.IsEditable)
                    {
                        model.PropertyChanged += DetailModel_PropertyChanged;
                    }
                }

                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(Items));

                _tnDtChungTuChiTietView.Refresh();

            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ChungTuChiTietItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (TnDtChungTuChiTietModel)obj;
            if (!string.IsNullOrEmpty(SelectedLNS))
                result = result && item.Lns.ToLower().StartsWith(SelectedLNS.ToLower());

            if (!string.IsNullOrEmpty(DetailFilter.L))
                result = result && item.L.ToLower().StartsWith(DetailFilter.L.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.K))
                result = result && item.K.ToLower().StartsWith(DetailFilter.K.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.M.ToLower().StartsWith(DetailFilter.M.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.TM.ToLower().StartsWith(DetailFilter.TM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.TTM.ToLower().StartsWith(DetailFilter.TTM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.Ng.ToLower().StartsWith(DetailFilter.NG.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG))
                result = result && item.Ng.ToLower().StartsWith(DetailFilter.NG.ToLower());

            if (SpecializedSelected != null && !string.IsNullOrEmpty(SpecializedSelected.ValueItem))
            {
                result = result && ((SpecializedSelected.ValueItem.Contains(item.Ng) && !string.IsNullOrEmpty(item.Ng)) || ListParentFilter.Any(n => n.XauNoiMa == item.XauNoiMa));
            }

            if (!string.IsNullOrEmpty(TypeDisplaysSelected))
            {
                if (TypeDisplaysSelected == TypeDisplay.DA_NHAP)
                    result = result && (item.TuChi != 0 || (item.IsModified && (item.IdChungTu == Guid.Empty || item.IdChungTu == null) && !item.IsDeleted));
                else if (TypeDisplaysSelected == TypeDisplay.CO_DU_LIEU)
                    result = result && (item.TuChi != 0 || (item.IsModified && (item.IdChungTu == Guid.Empty || item.IdChungTu == null) && !item.IsDeleted));
            }

            item.IsFilter = result;
            return result;
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .Select(x => {
                    x.TuChi = 0;
                    return x;
                }).ToList();

            foreach (var item in Items.Where(x => x.IsEditable && (x.TuChi > 0)))
            {
                CalculateParent(item, item);
            }

            CalculateTotal();
        }

        private void CalculateParent(TnDtChungTuChiTietModel currentItem, TnDtChungTuChiTietModel seftItem)
        {
            var parrentItem = Items.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.TuChi += seftItem.TuChi;
            CalculateParent(parrentItem, seftItem);
        }

        private void CalculateTotal()
        {
            Model.TuChiSum = 0;
            var listChildren = Items.Where(x => x.IsEditable && x.IsFilter).ToList();
            foreach (var item in listChildren)
            {
                Model.TuChiSum += item.TuChi;
            }
            OnPropertyChanged(nameof(Model));
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            try
            {
                if (args.PropertyName == nameof(TnDtChungTuChiTietModel.TuChi)
                    || args.PropertyName == nameof(TnDtChungTuChiTietModel.GhiChu))
                {
                    TnDtChungTuChiTietModel item = (TnDtChungTuChiTietModel)sender;
                    item.IsModified = true;
                    if (args.PropertyName == nameof(TnDtChungTuChiTietModel.TuChi))
                    {
                        CalculateData();
                    }
                    OnPropertyChanged(nameof(IsSaveData));
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadLNSIndexCondition()
        {
            try
            {
                List<TnDtChungTuChiTietModel> listLNS = Items.Where(x => string.IsNullOrEmpty(x.L) &&
                string.IsNullOrEmpty(x.K) &&
                string.IsNullOrEmpty(x.M) &&
                string.IsNullOrEmpty(x.TM) &&
                string.IsNullOrEmpty(x.TTM) &&
                string.IsNullOrEmpty(x.Ng) &&
                string.IsNullOrEmpty(x.Tng)).ToList();
                listLNS.Insert(0, new TnDtChungTuChiTietModel
                {
                    Lns = string.Empty,
                    NoiDung = "-- TẤT CẢ --"
                });
                BudgetCatalogItems = new ObservableCollection<TnDtChungTuChiTietModel>(listLNS);
                _budgetCatalogItemsView = CollectionViewSource.GetDefaultView(BudgetCatalogItems);
                _budgetCatalogItemsView.Filter = BudgetCatalogItemsFilter;
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool BudgetCatalogItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is TnDtChungTuChiTietModel item && item.Lns.ToLower().StartsWith(_searchLNS!.ToLower());
        }
        
        private void OnReSetFilter()
        {
            DetailFilter = new EstimationDetailCriteria();
            SelectedLNS = string.Empty;
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        protected override void OnRefresh()
        {
            if (IsSaveData)
            {
                var messageBox = new NSMessageBoxViewModel(Resources.MsgConfirmEdit, "Xác nhận", NSMessageBoxButtons.YesNo, RefreshConfirmEventHandler);
                DialogHost.Show(messageBox.Content, "RevenueApprovedEstimationDetail");
            }
        }

        private void RefreshConfirmEventHandler(NSDialogResult result)
        {
            if (result == NSDialogResult.Yes) return;
            LoadData();
        }

        protected override void OnLockUnLock()
        {
            try
            {
                string msgConfirm = string.Format(Model.IsLocked ? Resources.MsgConfirmUnLockWarn : Resources.MsgConfirmLockWarn, Environment.NewLine, Environment.NewLine);
                var result = System.Windows.MessageBox.Show(msgConfirm, Resources.Alert, System.Windows.MessageBoxButton.YesNo);
                if (result == System.Windows.MessageBoxResult.No) return;
                LockConfirmEventHandler();
                System.Windows.MessageBox.Show(Model.IsLocked ? Resources.MsgLockDone : Resources.MsgUnLockDone, Resources.Alert, System.Windows.MessageBoxButton.OK);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LockConfirmEventHandler()
        {
            int rs = _tnDtChungTuService.LockOrUnLock(Model.Id, !Model.IsLocked);
            if (rs == DBContextSaveChangeState.SUCCESS)
            {
                string msgDone = Model.IsLocked ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                Model.IsLocked = !Model.IsLocked;
                OnPropertyChanged(nameof(IsSaveData));

                // refresh data screen index
                DataChangedEventHandler handler = UpdateSettlementVoucherEvent;
                if (handler != null)
                {
                    handler(Model, new EventArgs());
                }
            }
        }

        private void OnSaveData()
        {
            try
            {
                List<TnDtChungTuChiTietModel> listChungTuChiTietAdd = Items.Where(x => x.IsEditable && x.IsModified && x.IdChungTu == null && x.TuChi > 0).ToList();
                List<TnDtChungTuChiTietModel> listChungTuChiTietUpdate = Items.Where(x => x.IsEditable && x.IsModified && x.IdChungTu != null).ToList();
                List<TnDtChungTuChiTietModel> listChungTuChiTietDelete = Items.Where(x => x.IsDeleted && x.IdChungTu != null).ToList();

                // Thêm mới chứng từ chi tiết
                if (listChungTuChiTietAdd.Count > 0)
                {
                    listChungTuChiTietAdd = listChungTuChiTietAdd.Select(x =>
                    {
                        x.Id = Guid.NewGuid();
                        x.IdChungTu = Model.Id;
                        x.NamNganSach = Model.NamNganSach.HasValue ? Model.NamNganSach.Value : _sessionService.Current.YearOfBudget;
                        x.NamLamViec = Model.NamLamViec;
                        x.NguonNganSach = Model.NguonNganSach;
                        x.ITrangThai = NSEntityStatus.ACTIVED;
                        x.IPhanCap = RevenueAndExpenditureType.ApprovedEstimation;
                        x.IdDonVi = _sessionService.Current.IdDonVi;
                        return x;
                    }).ToList();

                    List<TnDtChungTuChiTiet> listChungTuChiTiets = _mapper.Map<List<TnDtChungTuChiTiet>>(listChungTuChiTietAdd);
                    _tnDtChungTuChiTietService.AddRange(listChungTuChiTiets);
                }

                // Cập nhật chứng từ chi tiết
                if (listChungTuChiTietUpdate.Count > 0)
                {
                    foreach (var item in listChungTuChiTietUpdate)
                    {
                        TnDtChungTuChiTiet chungTuChiTiet = _tnDtChungTuChiTietService.FindById(item.Id);
                        _mapper.Map(item, chungTuChiTiet);
                        _tnDtChungTuChiTietService.Update(chungTuChiTiet);
                    }
                }

                // Xóa chứng từ chi tiết
                if (listChungTuChiTietDelete.Count > 0)
                {
                    foreach (var item in listChungTuChiTietDelete)
                    {
                        _tnDtChungTuChiTietService.Delete(item.Id);
                        item.TuChi = 0;
                    }
                }

                if (listChungTuChiTietDelete.Count() != 0 || listChungTuChiTietUpdate.Count() != 0 || listChungTuChiTietAdd.Count != 0)
                {
                    Items = new ObservableCollection<TnDtChungTuChiTietModel>(Items.ToList().Select(x => { x.IsModified = false; return x; }));
                }

                LoadData();

                // cập nhật thông tin chứng từ
                TnDtChungTu chungTu = _tnDtChungTuService.FindById(Model.Id);
                chungTu.TuChiSum = Model.TuChiSum;
                _tnDtChungTuService.Update(chungTu);

                string message = Resources.MsgSaveDone;
                var messageBox = new NSMessageBoxViewModel(message);
                DialogHost.Show(messageBox.Content, "RevenueApprovedEstimationDetail");

                //refresh dữ liệu ở màn index
                DataChangedEventHandler handler = UpdateSettlementVoucherEvent;
                if (handler != null)
                {
                    handler(Model, new EventArgs());
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            } 
        }

        protected override void OnAdd()
        {
            try
            {
                if (SelectedItem != null)
                {
                    int currentRow = Items.IndexOf(SelectedItem);
                    int targetRow = Items.ToList().FindIndex(currentRow, x => x.IsEditable);
                    if (targetRow > -1)
                    {
                        TnDtChungTuChiTietModel sourceItem = Items.ElementAt(targetRow);
                        TnDtChungTuChiTietModel targetItem = ObjectCopier.Clone(sourceItem);

                        targetItem.Id = Guid.NewGuid();
                        targetItem.IdChungTu = null;
                        targetItem.TuChi = 0;
                        targetItem.GhiChu = null;
                        targetItem.IsModified = true;
                        targetItem.PropertyChanged += DetailModel_PropertyChanged;

                        Items.Insert(targetRow + 1, targetItem);
                        OnPropertyChanged(nameof(Items));
                        OnPropertyChanged(nameof(IsSaveData));
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                CalculateData();
                OnPropertyChanged(nameof(IsSaveData));
            }
        }
    }
}
