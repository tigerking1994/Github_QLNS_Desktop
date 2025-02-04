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
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Plan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.RevenueExpenditureType;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.App.Helper;
using System.Windows;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service.Impl;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Plan.PrintPlanReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Plan.PrintPlanReport;
using System.Web.UI.WebControls;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Plan
{
    public class PlanBudgetBeginYearDetailViewModel : DetailViewModelBase<TnDtdnChungTuModel, TnDtdnChungTuChiTietModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly ITnDtChungTuChiTietService _tnDtChungTuChiTietService;
        private readonly ITnDtChungTuService _tnDtChungTuService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IExportService _exportService;

        private readonly ITnDtdnChungTuChiTietService _tnDtdnChungTuChiTietService;
        private readonly ITnDtdnChungTuService _tnDtdnChungTuService;
        private readonly ILog _logger;
        private EstimationVoucherDetailCriteria _searchCondition;
        private ICollectionView _tnDtChungTuChiTietView;
        private ICollectionView _budgetCatalogItemsView;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateSettlementVoucherEvent;

        public override Type ContentType => typeof(PlanBudgetBeginYearDetail);
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

        private ObservableCollection<TnDtdnChungTuChiTietModel> _budgetCatalogItems;
        public ObservableCollection<TnDtdnChungTuChiTietModel> BudgetCatalogItems
        {
            get => _budgetCatalogItems;
            set => SetProperty(ref _budgetCatalogItems, value);
        }

        private TnDtdnChungTuChiTietModel _selectedBudgetCatalog;
        public TnDtdnChungTuChiTietModel SelectedBudgetCatalog
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
                    LoadData();
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

        private string _thucHienThu;
        public string ThucHienThu
        {
            get => _thucHienThu;
            set
            {
                SetProperty(ref _thucHienThu, value);
            }
        }
        private string _duToanNam;
        public string DuToanNam
        {
            get => _duToanNam;
            set
            {
                SetProperty(ref _duToanNam, value);
            }
        }
        private string _uocThucHien;
        public string UocThucHien
        {
            get => _uocThucHien;
            set
            {
                SetProperty(ref _uocThucHien, value);
            }
        }
        private string _duToanThu;
        public string DuToanThu
        {
            get => _duToanThu;
            set
            {
                SetProperty(ref _duToanThu, value);
            }
        }

        public List<TnDtdnChungTuChiTietModel> ListParentFilter;
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public PlanBudgetBeginYearReportViewModel PlanBudgetBeginYearReportViewModel { get; set; }
        public PlanBudgetBeginYearReportSummaryViewModel PlanBudgetBeginYearReportSummaryViewModel { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand SaveDataCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public new RelayCommand BtnExcelCommand { get; }
        public RelayCommand PrintActionCommand { get; }


        public PlanBudgetBeginYearDetailViewModel(IMapper mapper,
            ISessionService sessionService,
            IDanhMucService danhMucService,
            ITnDtChungTuChiTietService tnDtChungTuChiTietService,
            ITnDtChungTuService tnDtChungTuService,
            ILog logger,
            ITnDtdnChungTuChiTietService tnDtdnChungTuChiTietService,
            ITnDtdnChungTuService tnDtdnChungTuService,
            INsMucLucNganSachService nsMucLucNganSachService,
            IExportService exportService,

            PlanBudgetBeginYearReportViewModel planBudgetBeginYearReportViewModel,
            PlanBudgetBeginYearReportSummaryViewModel planBudgetBeginYearReportSummaryViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _tnDtChungTuChiTietService = tnDtChungTuChiTietService;
            _tnDtChungTuService = tnDtChungTuService;
            _logger = logger;
            _tnDtdnChungTuChiTietService = tnDtdnChungTuChiTietService;
            _tnDtdnChungTuService = tnDtdnChungTuService;
            _mucLucNganSachService = nsMucLucNganSachService;
            _exportService = exportService;
            PlanBudgetBeginYearReportViewModel = planBudgetBeginYearReportViewModel;
            PlanBudgetBeginYearReportSummaryViewModel = planBudgetBeginYearReportSummaryViewModel;

            SearchCommand = new RelayCommand(obj => { _tnDtChungTuChiTietView.Refresh(); CalculateTotal(); });
            ResetFilterCommand = new RelayCommand(obj => OnReSetFilter());
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            CloseCommand = new RelayCommand(OnClose);
            BtnExcelCommand = new RelayCommand(obj => OnExportData());
            PrintActionCommand = new RelayCommand(obj => OnPrint(obj));


        }

        public override void Init()
        {
            NamLamViec = _sessionService.Current.YearOfWork;
            LoadHeader();
            ResetConditionSearch();
            //LoadSpecialized();
            LoadData();
            LoadTypeDisplay();

            LoadLNSIndexCondition();
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }


        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Tất cả" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Đã nhập liệu" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.KHONG_CO_DU_LIEU, DisplayItem = "Chưa nhập liệu" });
            TypeDisplaysSelected = TypeDisplay.TAT_CA;
        }

        private void ResetConditionSearch()
        {
            DetailFilter = new EstimationDetailCriteria();
            _searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Model.Id,
                LNS = Model.SDSLNS,
                YearOfWork = Model.INamLamViec,
                YearOfBudget = Model.INamNganSach,
                VoucherDate = Model.DNgayChungTu,
                BudgetSource = Model.IIdMaNguonNganSach
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
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadHeader()
        {
            
            _thucHienThu = $"Thực hiện thu năm {NamLamViec - 2}";
            _duToanNam = $"Dự toán năm {NamLamViec - 1}";
            _uocThucHien = $"Ước thực hiện năm {NamLamViec - 1}";
            _duToanThu = $"Dự toán thu năm kế hoạch {NamLamViec}";
        }
        private void LoadData()
        {
            try
            {
                List<TnDtdnChungTuChiTietQuery> _chungTuChiTiet = _tnDtdnChungTuChiTietService.FindByDataDetailCondition(_searchCondition).ToList();
                if (TypeDisplaysSelected == TypeDisplay.KHONG_CO_DU_LIEU)
                {
                    _chungTuChiTiet = HandlerNoData(_chungTuChiTiet);
                }
                Items = _mapper.Map<ObservableCollection<TnDtdnChungTuChiTietModel>>(_chungTuChiTiet.OrderBy(x => x.XauNoiMa));

                GetListParentFilter();
                ProcessDynamicMLNS();
                _tnDtChungTuChiTietView = CollectionViewSource.GetDefaultView(Items);
                _tnDtChungTuChiTietView.Filter = ChungTuChiTietItemsViewFilter;

                CalculateData();

                if (Items != null)
                {
                    SelectedItem = Items.Where(x => !x.BHangCha).FirstOrDefault();
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
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private List<TnDtdnChungTuChiTietQuery> HandlerNoData(List<TnDtdnChungTuChiTietQuery> items)
        {
            List<TnDtdnChungTuChiTietQuery> results = new List<TnDtdnChungTuChiTietQuery>();
            if (items.IsEmpty()) return results;
            var itemsNoData = items.Where(x => !x.BHangCha && x.FDuToanNamKeHoach == 0 && x.FDuToanNamNay == 0 && x.FThucThuNamTruoc == 0 && x.FUocThucHienNamNay == 0);
            if (itemsNoData.IsEmpty()) return results;
            foreach (var item in itemsNoData)
            {
                FilterData(item, results, items);
            }
            return results;
        }

        private void FilterData(TnDtdnChungTuChiTietQuery item, List<TnDtdnChungTuChiTietQuery> results, List<TnDtdnChungTuChiTietQuery> items)
        {

            results.Add(item);
            AddParent(item, results, items);

        }
        private void AddParent(TnDtdnChungTuChiTietQuery item, List<TnDtdnChungTuChiTietQuery> results, List<TnDtdnChungTuChiTietQuery> items)
        {
            if (item.MlnsIdParent.IsNullOrEmpty()) return;
            if (results.Any(x => item.MlnsIdParent.Equals(x.MlnsId))) return;
            TnDtdnChungTuChiTietQuery parent = items.FirstOrDefault(x => x.MlnsId.Equals(item.MlnsIdParent));
            if (parent is null) return;
            results.Add(parent);
            AddParent(parent, results, items);
        }

        private bool ChungTuChiTietItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (TnDtdnChungTuChiTietModel)obj;
            if (!string.IsNullOrEmpty(SelectedLNS))
                result = result && item.Lns.ToLower().StartsWith(SelectedLNS.ToLower());

            if (!string.IsNullOrEmpty(DetailFilter.L))
                result = result && item.L.ToLower().StartsWith(DetailFilter.L.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.K))
                result = result && item.K.ToLower().StartsWith(DetailFilter.K.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.M.ToLower().StartsWith(DetailFilter.M.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.Tm.ToLower().StartsWith(DetailFilter.TM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.Ttm.ToLower().StartsWith(DetailFilter.TTM.ToLower());
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
                if (TypeDisplaysSelected == TypeDisplay.KHONG_CO_DU_LIEU)
                    result = result && (item.FDuToanNamKeHoach == 0 && item.FDuToanNamNay == 0 && item.FThucThuNamTruoc == 0 && item.FUocThucHienNamNay == 0 || (item.IsModified && (item.IdChungTu == Guid.Empty || item.IdChungTu == null) && !item.IsDeleted));
                else if (TypeDisplaysSelected == TypeDisplay.CO_DU_LIEU)
                    result = result && (item.FDuToanNamKeHoach != 0 || item.FDuToanNamNay != 0 || item.FThucThuNamTruoc != 0 || item.FUocThucHienNamNay != 0 || (item.IsModified && (item.IdChungTu == Guid.Empty || item.IdChungTu == null) && !item.IsDeleted));
            }

            item.IsFilter = result;
            return result;
        }

        private void LoadLNSIndexCondition()
        {
            try
            {
                List<TnDtdnChungTuChiTietModel> listLNS = Items.Where(x => string.IsNullOrEmpty(x.L) &&
                string.IsNullOrEmpty(x.K) &&
                string.IsNullOrEmpty(x.M) &&
                string.IsNullOrEmpty(x.Tm) &&
                string.IsNullOrEmpty(x.Ttm) &&
                string.IsNullOrEmpty(x.Ng) &&
                string.IsNullOrEmpty(x.Tng)).ToList();
                listLNS.Insert(0, new TnDtdnChungTuChiTietModel
                {
                    Lns = string.Empty,
                    SGhiChu = "-- TẤT CẢ --"
                });
                BudgetCatalogItems = new ObservableCollection<TnDtdnChungTuChiTietModel>(listLNS);
                _budgetCatalogItemsView = CollectionViewSource.GetDefaultView(BudgetCatalogItems);
                _budgetCatalogItemsView.Filter = BudgetCatalogItemsFilter;
            }
            catch (Exception ex)
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
            return obj is TnDtdnChungTuChiTietModel item && item.Lns.ToLower().StartsWith(_searchLNS!.ToLower());
        }

        private void OnReSetFilter()
        {
            DetailFilter = new EstimationDetailCriteria();
            SelectedLNS = string.Empty;
        }

        protected override void OnRefresh()
        {
            if (IsSaveData)
            {
                var messageBox = new NSMessageBoxViewModel(Resources.MsgConfirmEdit, "Xác nhận", NSMessageBoxButtons.YesNo, RefreshConfirmEventHandler);
                DialogHost.Show(messageBox.Content, "RevenuePlanBudgetBeginYearDetail");
            }
            else
            {
                LoadData();
            }
        }

        protected override void OnLockUnLock()
        {
            try
            {

                string message = string.Empty;
                message = Model.BKhoa ? Resources.UnlockChungTu : Resources.LockChungTu;
                MessageBoxResult result = System.Windows.MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    LockConfirmEventHandler();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LockConfirmEventHandler()
        {
            int rs = _tnDtdnChungTuService.LockOrUnLock(Model.Id, !Model.BKhoa);
            if (rs == DBContextSaveChangeState.SUCCESS)
            {
                string msgDone = Model.BKhoa ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                Model.BKhoa = !Model.BKhoa;
                OnPropertyChanged(nameof(IsSaveData));
                // refresh data screen index
                DataChangedEventHandler handler = UpdateSettlementVoucherEvent;
                if (handler != null)
                {
                    handler(Model, new EventArgs());
                }
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
                        TnDtdnChungTuChiTietModel sourceItem = Items.ElementAt(targetRow);
                        TnDtdnChungTuChiTietModel targetItem = ObjectCopier.Clone(sourceItem);

                        targetItem.Id = Guid.NewGuid();
                        targetItem.IdChungTu = null;
                        targetItem.FDuToanNamKeHoach = 0;
                        targetItem.FDuToanNamNay = 0;
                        targetItem.FThucThuNamTruoc = 0;
                        targetItem.FUocThucHienNamNay = 0;
                        targetItem.SGhiChu = null;
                        targetItem.IsModified = true;
                        targetItem.PropertyChanged += DetailModel_PropertyChanged;

                        Items.Insert(targetRow + 1, targetItem);
                        OnPropertyChanged(nameof(Items));
                        OnPropertyChanged(nameof(IsSaveData));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void RefreshConfirmEventHandler(NSDialogResult result)
        {
            if (result == NSDialogResult.Yes) return;
            LoadData();
        }

        private void CalculateData()
        {
            Items.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.FDuToanNamKeHoach = 0;
                    x.FDuToanNamNay = 0;
                    x.FThucThuNamTruoc = 0;
                    x.FUocThucHienNamNay = 0;
                    return x;
                }).ToList();

            foreach (var item in Items.Where(x => x.IsEditable && (x.FDuToanNamKeHoach != 0 || x.FDuToanNamNay != 0 || x.FThucThuNamTruoc != 0 || x.FUocThucHienNamNay != 0)))
            {
                CalculateParent(item, item);
            }

            CalculateTotal();
        }

        private void CalculateParent(TnDtdnChungTuChiTietModel currentItem, TnDtdnChungTuChiTietModel seftItem)
        {
            var parrentItem = Items.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FDuToanNamKeHoach += seftItem.FDuToanNamKeHoach;
            parrentItem.FDuToanNamNay += seftItem.FDuToanNamNay;
            parrentItem.FThucThuNamTruoc += seftItem.FThucThuNamTruoc;
            parrentItem.FUocThucHienNamNay += seftItem.FUocThucHienNamNay;

            CalculateParent(parrentItem, seftItem);

        }

        private void CalculateTotal()
        {
            Model.FTongDuToanNamKeHoach = 0;
            Model.FTongDuToanNamNay = 0;
            Model.FTongThucThuNamTruoc = 0;
            Model.FTongUocThucHienNamNay = 0;
            var listChildren = Items.Where(x => x.IsEditable && x.IsFilter).ToList();
            foreach (var item in listChildren)
            {
                Model.FTongDuToanNamKeHoach += item.FDuToanNamKeHoach;
                Model.FTongDuToanNamNay += item.FDuToanNamNay;
                Model.FTongThucThuNamTruoc += item.FThucThuNamTruoc;
                Model.FTongUocThucHienNamNay += item.FUocThucHienNamNay;
            }
            OnPropertyChanged(nameof(Model));
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            try
            {
                if (args.PropertyName == nameof(TnDtdnChungTuChiTietModel.FDuToanNamKeHoach)
                || args.PropertyName == nameof(TnDtdnChungTuChiTietModel.FDuToanNamNay)
                || args.PropertyName == nameof(TnDtdnChungTuChiTietModel.FThucThuNamTruoc)
                || args.PropertyName == nameof(TnDtdnChungTuChiTietModel.FUocThucHienNamNay)
                || args.PropertyName == nameof(TnDtdnChungTuChiTietModel.SGhiChu))
                {
                    TnDtdnChungTuChiTietModel item = (TnDtdnChungTuChiTietModel)sender;
                    item.IsModified = true;
                    if (args.PropertyName == nameof(TnDtdnChungTuChiTietModel.FDuToanNamKeHoach)
                    || args.PropertyName == nameof(TnDtdnChungTuChiTietModel.FDuToanNamNay)
                    || args.PropertyName == nameof(TnDtdnChungTuChiTietModel.FThucThuNamTruoc)
                    || args.PropertyName == nameof(TnDtdnChungTuChiTietModel.FUocThucHienNamNay))
                    {
                        CalculateData();
                    }
                    OnPropertyChanged(nameof(IsSaveData));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void GetListParentFilter()
        {
            ListParentFilter = new List<TnDtdnChungTuChiTietModel>();
            if (SpecializedSelected != null && !string.IsNullOrEmpty(SpecializedSelected.ValueItem))
            {
                List<TnDtdnChungTuChiTietModel> listChild = Items.Where(x => SpecializedSelected.ValueItem.Contains(x.Ng) && !string.IsNullOrEmpty(x.Ng)).ToList();
                if (listChild != null && listChild.Count > 0)
                {
                    ListParentFilter = Items.Where(n => n.BHangCha && listChild.Any(m => m.XauNoiMa.Contains(n.XauNoiMa))).ToList();
                }
            }
        }

        private void ProcessDynamicMLNS()
        {
            List<string> chiTietTois = Items.Where(x => !string.IsNullOrEmpty(x.ChiTietToi)).Select(x => x.ChiTietToi).ToList();
            ColumnDisplay = DynamicMLNS.SettingColumnVisibility(chiTietTois);
        }
        private void OnSaveData()
        {
            try
            {
                List<TnDtdnChungTuChiTietModel> listChungTuChiTietAdd = Items.Where(x => x.IsEditable && x.IsModified && x.IdChungTu == null).ToList();
                List<TnDtdnChungTuChiTietModel> listChungTuChiTietUpdate = Items.Where(x => x.IsEditable && x.IsModified && x.IdChungTu != null).ToList();
                List<TnDtdnChungTuChiTietModel> listChungTuChiTietDelete = Items.Where(x => x.IsDeleted && x.IdChungTu != null).ToList();

                // Thêm mới chứng từ chi tiết
                if (listChungTuChiTietAdd.Count > 0)
                {
                    listChungTuChiTietAdd = listChungTuChiTietAdd.Select(x =>
                    {
                        x.Id = Guid.NewGuid();
                        x.IdChungTu = Model.Id;
                        x.INamNganSach = Model.INamNganSach.HasValue ? Model.INamNganSach.Value : _sessionService.Current.YearOfBudget;
                        x.INamLamViec = Model.INamLamViec ?? _sessionService.Current.YearOfWork;
                        x.IIdMaNguonNganSach = Model.IIdMaNguonNganSach ?? _sessionService.Current.Budget;
                        x.IIdMaDonVi = Model.IIdMaDonVi;
                        x.STenDonVi = Model.STenDonVi;
                        return x;
                    }).ToList();

                    List<TnDtdnChungTuChiTiet> listChungTuChiTiets = _mapper.Map<List<TnDtdnChungTuChiTiet>>(listChungTuChiTietAdd);
                    _tnDtdnChungTuChiTietService.AddRange(listChungTuChiTiets);
                }

                // Cập nhật chứng từ chi tiết
                if (listChungTuChiTietUpdate.Count > 0)
                {
                    foreach (var item in listChungTuChiTietUpdate)
                    {
                        TnDtdnChungTuChiTiet chungTuChiTiet = _tnDtdnChungTuChiTietService.FindById(item.Id);
                        _mapper.Map(item, chungTuChiTiet);
                        _tnDtdnChungTuChiTietService.Update(chungTuChiTiet);
                    }
                }

                // Xóa chứng từ chi tiết
                if (listChungTuChiTietDelete.Count > 0)
                {
                    foreach (var item in listChungTuChiTietDelete)
                    {
                        _tnDtdnChungTuChiTietService.Delete(item.Id);
                        item.FDuToanNamKeHoach = 0;
                        item.FDuToanNamNay = 0;
                        item.FThucThuNamTruoc = 0;
                        item.FUocThucHienNamNay = 0;
                    }
                }

                if (listChungTuChiTietDelete.Count() != 0 || listChungTuChiTietUpdate.Count() != 0 || listChungTuChiTietAdd.Count != 0)
                {
                    Items = new ObservableCollection<TnDtdnChungTuChiTietModel>(Items.ToList().Select(x => { x.IsModified = false; return x; }));
                }

                LoadData();

                // cập nhật thông tin chứng từ
                TnDtdnChungTu chungTu = _tnDtdnChungTuService.FindById(Model.Id);
                chungTu.FTongDuToanNamKeHoach = Model.FTongDuToanNamKeHoach;
                chungTu.FTongDuToanNamNay = Model.FTongDuToanNamNay;
                chungTu.FTongThucThuNamTruoc = Model.FTongThucThuNamTruoc;
                chungTu.FTongUocThucHienNamNay = Model.FTongUocThucHienNamNay;
                _tnDtdnChungTuService.Update(chungTu);

                string message = Resources.MsgSaveDone;
                var messageBox = new NSMessageBoxViewModel(message);
                DialogHost.Show(messageBox.Content, "RevenuePlanBudgetBeginYearDetail");

                //refresh dữ liệu ở màn index
                DataChangedEventHandler handler = UpdateSettlementVoucherEvent;
                if (handler != null)
                {
                    handler(Model, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

        }

        /// <summary>
        /// Xuất excel chứng từ tổng hợp
        /// </summary>
        private void OnExportData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = RevenueExpenditureType.RPT_TN_DTDN_CHUNG_TU_TONG_HOP;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    List<TnDtdnChungTuModel> settlementVouchers = new List<TnDtdnChungTuModel> { Model };
                    DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();

                    foreach (var item in settlementVouchers)
                    {
                       // TypeDisplaysSelected = TypeDisplay.CO_DU_LIEU;
                        List<TnDtdnChungTuChiTietModel> settlementVoucherDetails = Items.Where(x => x.IsHasData).ToList();
                        Dictionary<string, object> data = new Dictionary<string, object>
                        {
                            { "Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "" },
                            { "Cap2", _sessionService.Current.TenDonVi.ToUpper() },
                            { "TitleFirst", $"LẬP DỰ TOÁN ĐẦU NĂM {_sessionService.Current.YearOfWork}" },
                            { "TitleSecond", $"(Kèm theo Chứng từ số: {item.SSoChungTu}, ngày: {DateUtils.Format(item.DNgayChungTu)})" },
                            { "HeaderTenDonVi", $"Đơn vị: {item.STenDonVi}" },
                            { "MoTa", item.SMoTa },
                            { "NguoiTao", item.SNguoiTao },
                            { "NgayTao", item.DNgaySua != null ? item.DNgayTao.Value.ToString("dd/MM/yyyy") : string.Empty },
                            { "Items", settlementVoucherDetails },
                            { "MLNS", mucLucNganSaches },
                            { "TotalFThucThuNamTruoc", Model.FTongThucThuNamTruoc },
                            { "TotalFDuToanNamNay", Model.FTongDuToanNamNay },
                            { "TotalFUocThucHienNamNay", Model.FTongUocThucHienNamNay },
                            { "TotalFDuToanNamKeHoach", Model.FTongDuToanNamKeHoach },
                            { "Year", _sessionService.Current.YearOfWork },
                            { "Year1", _sessionService.Current.YearOfWork - 1 },
                            { "Year2", _sessionService.Current.YearOfWork - 2 }
                        };

                        fileNamePrefix = item.SSoChungTu;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(GetChiTietToi(settlementVoucherDetails));
                        var xlsFile = _exportService.Export<TnDtdnChungTuChiTietModel, NsMucLucNganSach>(templateFileName, data, hideColumns.Select(x => x += 2).ToList());
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.EXCEL);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        //Print report
        private void OnPrint(object param)
        {
            int dialogType = (int)param;
            switch (dialogType)
            {
                case (int)PlanBudgetBeginYearType.PRINT_AGENCY:
                    PlanBudgetBeginYearReportViewModel.Init();
                    var view = new PlanBudgetBeginYearReport
                    {
                        DataContext = PlanBudgetBeginYearReportViewModel
                    };
                    DialogHost.Show(view, "RevenuePlanBudgetBeginYearDetail");

                    break;

                case (int)PlanBudgetBeginYearType.PRINT_SUMMARY:
                    PlanBudgetBeginYearReportSummaryViewModel.Init();
                    var view2 = new PlanBudgetBeginYearReportSummary
                    {
                        DataContext = PlanBudgetBeginYearReportSummaryViewModel
                    };
                    DialogHost.Show(view2, "RevenuePlanBudgetBeginYearDetail");
                    break;
            }
        }

        private string GetChiTietToi(List<TnDtdnChungTuChiTietModel> items)
        {
            List<string> sChiTietTois = items.Where(x => !string.IsNullOrEmpty(x.ChiTietToi)).Select(x => x.ChiTietToi).Distinct().ToList();
            if (sChiTietTois.IsEmpty()) return "NG";
            return DynamicMLNS.GetMaxNameColumnByChiTietToi(sChiTietTois);
        }
    }
}
