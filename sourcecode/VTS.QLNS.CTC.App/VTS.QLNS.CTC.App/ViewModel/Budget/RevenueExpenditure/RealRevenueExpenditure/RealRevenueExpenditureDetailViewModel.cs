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
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.RealRevenueExpenditure;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.RealRevenueExpenditure.PrintRealRevenueExpenditureReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.RealRevenueExpenditure
{
    public class RealRevenueExpenditureDetailViewModel : DetailViewModelBase<TnQtChungTuHD4554Model, TnQtChungTuChiTietHD4554Model>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITnQtChungTuChiTietService _tnQtChungTuChiTietService;
        private readonly ITnQtChungTuService _tnQtChungTuService;
        private readonly ITnQtChungTuChiTietHD4554Service _tnQtChungTuChiTietHD4554Service;
        private readonly ITnQtChungTuHD4554Service _tnQtChungTuHD4554Service;
        private readonly ILog _logger;
        private EstimationVoucherDetailCriteria _searchCondition;
        private ICollectionView _tnQtChungTuChiTietView;
        private ICollectionView _budgetCatalogItemsView;
        private ICollection<TnQtChungTuChiTietHD4554Model> _filterResult = new HashSet<TnQtChungTuChiTietHD4554Model>();
        private string xnmConcatenation = "";
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateSettlementVoucherEvent;
        public override Type ContentType => typeof(RealRevenueExpenditureDetail);
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        public bool IsPropertyChange { get; set; }
        public int NamLamViec { get; set; }

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
                if (SetProperty(ref _typeDisplaysselected, value) && _tnQtChungTuChiTietView != null)
                {
                    LoadData();
                    //BeForeRefresh();
                    _tnQtChungTuChiTietView.Refresh();
                    //if (IsPropertyChange) CalculateData();
                }
            }
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
                    if (IsPropertyChange) CalculateData();
                }
            }
        }

        private ObservableCollection<TnQtChungTuChiTietHD4554Model> _budgetCatalogItems;
        public ObservableCollection<TnQtChungTuChiTietHD4554Model> BudgetCatalogItems
        {
            get => _budgetCatalogItems;
            set => SetProperty(ref _budgetCatalogItems, value);
        }

        private TnQtChungTuChiTietHD4554Model _selectedBudgetCatalog;
        public TnQtChungTuChiTietHD4554Model SelectedBudgetCatalog
        {
            get => _selectedBudgetCatalog;
            set
            {
                if (SetProperty(ref _selectedBudgetCatalog, value))
                {
                    if (_selectedBudgetCatalog != null)
                        SelectedLNS = _selectedBudgetCatalog.SLNS;
                    //BeForeRefresh();
                    _tnQtChungTuChiTietView.Refresh();
                    if (IsPropertyChange) CalculateData();
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


        public RelayCommand SaveDataCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; set; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand RefreshAllDataCommand { get; }

        public PrintRealRevenueExpenditureReportViewModel PrintRealRevenueExpenditureReportViewModel { get; set; }

        public RealRevenueExpenditureDetailViewModel(IMapper mapper,
            ISessionService sessionService,
            ITnQtChungTuChiTietService tnQtChungTuChiTietService,
            ITnQtChungTuService tnQtChungTuService,
            ITnQtChungTuChiTietHD4554Service tnQtChungTuChiTietHD4554Service,
            PrintRealRevenueExpenditureReportViewModel printRealRevenueExpenditureReportViewModel,
            ITnQtChungTuHD4554Service qtChungTuHD4554Service,
            ILog logger)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _tnQtChungTuChiTietService = tnQtChungTuChiTietService;
            _tnQtChungTuService = tnQtChungTuService;
            _tnQtChungTuHD4554Service = qtChungTuHD4554Service;
            _tnQtChungTuChiTietHD4554Service = tnQtChungTuChiTietHD4554Service;
            _logger = logger;

            PrintRealRevenueExpenditureReportViewModel = printRealRevenueExpenditureReportViewModel;
            PrintCommand = new RelayCommand(obj => OnShowPrint(obj));
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            SearchCommand = new RelayCommand(obj =>
            {
                //BeForeRefresh();
                _tnQtChungTuChiTietView.Refresh();
                if (IsPropertyChange) CalculateData();
            });
            ResetFilterCommand = new RelayCommand(obj => OnReSetFilter());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            RefreshAllDataCommand = new RelayCommand(obj => Init());
        }

        public override void Init()
        {
            base.Init();
            IsPropertyChange = false;
            NamLamViec = _sessionService.Current.YearOfWork;
            LoadTypeDisplay();
            ResetConditionSearch();
            LoadData();
            //LoadLNSIndexCondition();
            IsPropertyChange = true;
        }

        private void OnShowPrint(object param)
        {
            if (param != null)
                PrintRealRevenueExpenditureReportViewModel.RealRevenueExpenditureTypes = (RealRevenueExpenditureType)((int)param);
            PrintRealRevenueExpenditureReportViewModel.Init();
            var view = new VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.RealRevenueExpenditure.PrintRealRevenueExpenditureReport.PrintRealRevenueExpenditureReport
            {
                DataContext = PrintRealRevenueExpenditureReportViewModel
            };

            DialogHost.Show(view, SettlementScreen.REAL_REVENUE_EXPENDITURE_DETAIL_DIALOG);
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Đã nhập liệu" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Tất cả" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.KHONG_CO_DU_LIEU, DisplayItem = "Chưa nhập liệu" });
            TypeDisplaysSelected = TypeDisplay.TAT_CA;
        }

        private void ResetConditionSearch()
        {
            DetailFilter = new EstimationDetailCriteria();
            xnmConcatenation = string.Empty;
            _filterResult = new HashSet<TnQtChungTuChiTietHD4554Model>();
            _searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Model.Id,
                LNS = Model.SDSLNS,
                YearOfWork = Model.INamLamViec,
                YearOfBudget = Model.INamNganSach,
                VoucherDate = Model.DNgayChungTu,
                BudgetSource = Model.INguonNganSach,
                ILoai = Model.IThangQuyLoai.Value,
                IThangQuy = Model.IThangQuy != null ? Model.IThangQuy.Value : 0
            };
        }

        public virtual void LoadData(params object[] args)
        {
            base.LoadData();
            try
            {
                List<TnQtChungTuChiTietHD4554Query> _chungTuChiTiet = _tnQtChungTuChiTietHD4554Service.FindByRealRevenueExpenditureCondition(_searchCondition).ToList();
                if (TypeDisplaysSelected == TypeDisplay.KHONG_CO_DU_LIEU)
                {
                    _chungTuChiTiet = HandlerNoData(_chungTuChiTiet).OrderBy(x => x.SXauNoiMa).ToList();
                }
                Items = _mapper.Map<ObservableCollection<TnQtChungTuChiTietHD4554Model>>(_chungTuChiTiet.Distinct());
                ProcessDynamicMLNS();
                LoadLNSIndexCondition();
                _tnQtChungTuChiTietView = CollectionViewSource.GetDefaultView(Items);
                _tnQtChungTuChiTietView.Filter = ChungTuChiTietItemsViewFilter;


                if (Items != null)
                {
                    SelectedItem = Items.Where(x => !x.BHangCha).FirstOrDefault();
                }
                foreach (var model in Items)
                {
                    if (model.IsEditable && !model.BHangCha)
                    {
                        model.PropertyChanged += DetailModel_PropertyChanged;
                    }
                }

                CalculateData();
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(Items));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private List<TnQtChungTuChiTietHD4554Query> HandlerNoData(List<TnQtChungTuChiTietHD4554Query> items)
        {
            List<TnQtChungTuChiTietHD4554Query> results = new List<TnQtChungTuChiTietHD4554Query>();
            if (items.IsEmpty()) return results;
            var itemsNoData = items.Where(x => x.BHangCha != null && !x.BHangCha.Value && x.FSoTien.GetValueOrDefault(0) == 0 && x.FSoTienDeNghi.GetValueOrDefault(0) == 0);
            if (itemsNoData.IsEmpty()) return results;
            foreach (var item in itemsNoData)
            {
                FilterData(item, results, items);
            }
            return results;
        }

        private void FilterData(TnQtChungTuChiTietHD4554Query item, List<TnQtChungTuChiTietHD4554Query> results, List<TnQtChungTuChiTietHD4554Query> items)
        {
            results.Add(item);
            AddParent(item, results, items);
        }

        private void AddParent(TnQtChungTuChiTietHD4554Query item, List<TnQtChungTuChiTietHD4554Query> results, List<TnQtChungTuChiTietHD4554Query> items)
        {
            if (item.IID_MLNS_Cha.IsNullOrEmpty()) return;
            if (results.Any(x => item.IID_MLNS_Cha.Equals(x.IID_MLNS))) return;
            TnQtChungTuChiTietHD4554Query parent = items.FirstOrDefault(x => x.IID_MLNS.Equals(item.IID_MLNS_Cha));
            if (parent is null) return;
            results.Add(parent);
            AddParent(parent, results, items);
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (TnQtChungTuChiTietHD4554Model)obj;
            result = ChungTuChiTietItemsViewFilter(item);
            //if (!result && (item.IsHangCha || (!item.IsHangCha && ((HasInputData(item) && (TypeDisplaysSelected == TypeDisplay.TAT_CA || TypeDisplaysSelected == TypeDisplay.CO_DU_LIEU))))))
            //{

            //    if (string.IsNullOrEmpty(item.SL))
            //        result = xnmConcatenation.StartsWith(item.SLNS);
            //    else
            //    {
            //        result = xnmConcatenation.Contains(item.SXauNoiMa);
            //    }
            //}

            //if (result)
            item.IsFilter = result;
            return result;
        }
        private bool ChungTuChiTietItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (TnQtChungTuChiTietHD4554Model)obj;
            if (!string.IsNullOrEmpty(SelectedLNS))
                result = result && item.SLNS.ToLower().StartsWith(SelectedLNS.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.L))
                result = result && item.SL.ToLower().StartsWith(DetailFilter.L.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.K))
                result = result && item.SK.ToLower().StartsWith(DetailFilter.K.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.SM.ToLower().StartsWith(DetailFilter.M.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.STM.ToLower().StartsWith(DetailFilter.TM.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.STTM.ToLower().StartsWith(DetailFilter.TTM.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.SNG.ToLower().StartsWith(DetailFilter.NG.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TNG))
                result = result && item.STNG.ToLower().StartsWith(DetailFilter.TNG.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TNG1))
                result = result && item.STNG1.ToLower().StartsWith(DetailFilter.TNG1.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TNG2))
                result = result && item.STNG2.ToLower().StartsWith(DetailFilter.TNG2.ToLower().Trim());
            if (!string.IsNullOrEmpty(DetailFilter.TNG3))
                result = result && item.STNG3.ToLower().StartsWith(DetailFilter.TNG3.ToLower().Trim());

            if (!string.IsNullOrEmpty(TypeDisplaysSelected))
            {
                if (TypeDisplaysSelected == TypeDisplay.CO_DU_LIEU)
                    result = result && (item.FSoTien.GetValueOrDefault(0) != 0 || item.FSoTienDeNghi.GetValueOrDefault(0) != 0 || (item.IsModified && (item.IIdTnQtChungTu == Guid.Empty || item.IIdTnQtChungTu == null) && !item.IsDeleted));
                else if (TypeDisplaysSelected == TypeDisplay.KHONG_CO_DU_LIEU)
                    result = result && (item.FSoTien.GetValueOrDefault(0) == 0 || item.FSoTienDeNghi.GetValueOrDefault(0) == 0 || (item.IsModified && (item.IIdTnQtChungTu == Guid.Empty || item.IIdTnQtChungTu == null) && !item.IsDeleted));
            }

            item.IsFilter = result;
            return result;
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => ChungTuChiTietItemsViewFilter(item)).Where(item => !item.BHangCha || (item.BHangCha && HasInputData(item))).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        private bool HasInputData(TnQtChungTuChiTietHD4554Model item)
        {
            bool hasDataFSoTien = item.FSoTien.GetValueOrDefault(0) != 0;

            return hasDataFSoTien;
        }


        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            try
            {
                if (args.PropertyName == nameof(TnQtChungTuChiTietHD4554Model.FSoTien) || args.PropertyName == nameof(TnQtChungTuChiTietHD4554Model.FSoTienDeNghi) || args.PropertyName == nameof(TnQtChungTuChiTietHD4554Model.SGhiChu))
                {
                    TnQtChungTuChiTietHD4554Model item = (TnQtChungTuChiTietHD4554Model)sender;
                    item.IsModified = true;
                    if (args.PropertyName == nameof(TnQtChungTuChiTietHD4554Model.FSoTien) || args.PropertyName == nameof(TnQtChungTuChiTietHD4554Model.FSoTienDeNghi))
                    {
                        if (IsPropertyChange)
                        {
                            CalculateData();
                        }
                    }
                    OnPropertyChanged(nameof(IsSaveData));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnReSetFilter()
        {
            DetailFilter = new EstimationDetailCriteria();
            SelectedLNS = string.Empty;
            if (_tnQtChungTuChiTietView != null)
            {
                //BeForeRefresh();
                _tnQtChungTuChiTietView.Refresh();
                if (IsPropertyChange) CalculateData();

            }
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
                string msgConfirm = string.Format(Model.BKhoa.Value ? Resources.UnlockChungTu : Resources.LockChungTu, Environment.NewLine, Environment.NewLine);
                MessageBoxResult result = System.Windows.MessageBox.Show(msgConfirm, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == System.Windows.MessageBoxResult.No) return;
                LockConfirmEventHandler();

                System.Windows.MessageBox.Show(Model.BKhoa.Value ? Resources.MsgLockDone : Resources.MsgUnLockDone, Resources.Alert, System.Windows.MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LockConfirmEventHandler()
        {
            int rs = _tnQtChungTuHD4554Service.LockOrUnLock(Model.Id, !Model.BKhoa.Value);
            if (rs == DBContextSaveChangeState.SUCCESS)
            {
                string msgDone = Model.BKhoa.Value ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                Model.BKhoa = !Model.BKhoa.Value;
                OnPropertyChanged(nameof(IsSaveData));

                // refresh data screen index
                DataChangedEventHandler handler = UpdateSettlementVoucherEvent;
                if (handler != null)
                {
                    handler(Model, new EventArgs());
                }
            }
        }

        private void CalculateData()
        {
            IsPropertyChange = false;
            Items.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.FSoTien = 0;
                    x.FSoTienDeNghi = 0;

                    return x;
                }).ToList();
            List<TnQtChungTuChiTietHD4554Model> temp = new List<TnQtChungTuChiTietHD4554Model>();

            temp = Items.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();

            var dictByMlns = Items.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                //CalculateParent(item, item);
                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }

            CalculateTotal();
            IsPropertyChange = true;
        }

        private void CalculateParent(Guid idParent, TnQtChungTuChiTietHD4554Model item, Dictionary<Guid?, TnQtChungTuChiTietHD4554Model> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FSoTien += item.FSoTien.GetValueOrDefault(0);
            model.FSoTienDeNghi += item.FSoTienDeNghi.GetValueOrDefault(0);

            CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
        }


        private void CalculateTotal()
        {
            Model.FTongSoTien = 0;
            Model.FTongSoTienDeNghi = 0;
            //Model.TotalChiPhi = 0;

            var listChildren = Items.Where(x => !x.BHangCha && x.IsEditable && x.IsFilter).ToList();
            foreach (var item in listChildren)
            {
                Model.FTongSoTien += (item.FSoTien != null ? item.FSoTien.Value : 0);
                Model.FTongSoTienDeNghi += (item.FSoTienDeNghi != null ? item.FSoTienDeNghi.Value : 0);
                //Model.TotalChiPhi += (item.TongSoChiPhi != null ? item.TongSoChiPhi.Value : 0);
            }
            OnPropertyChanged(nameof(Model));
        }

        private void ProcessDynamicMLNS()
        {
            List<string> chiTietTois = Items.Where(x => !string.IsNullOrEmpty(x.SChiTietToi)).Select(x => x.SChiTietToi).ToList();
            ColumnDisplay = DynamicMLNS.SettingColumnVisibility(chiTietTois);
        }


        private void LoadLNSIndexCondition()
        {
            try
            {
                List<TnQtChungTuChiTietHD4554Model> listLNS = Items.Where(x => string.IsNullOrEmpty(x.SL) &&
                string.IsNullOrEmpty(x.SK) &&
                string.IsNullOrEmpty(x.SM) &&
                string.IsNullOrEmpty(x.STM) &&
                string.IsNullOrEmpty(x.STTM) &&
                string.IsNullOrEmpty(x.SNG) &&
                string.IsNullOrEmpty(x.STNG)).ToList();
                listLNS.Insert(0, new TnQtChungTuChiTietHD4554Model
                {
                    SLNS = string.Empty,
                    SNoiDung = "-- TẤT CẢ --"
                });
                BudgetCatalogItems = new ObservableCollection<TnQtChungTuChiTietHD4554Model>(listLNS);
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
            return obj is TnQtChungTuChiTietHD4554Model item && item.SLNS.ToLower().StartsWith(_searchLNS!.ToLower());
        }

        //protected override void OnDelete()
        //{
        //    if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha)
        //    {
        //        SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
        //        CalculateData();
        //        OnPropertyChanged(nameof(IsSaveData));
        //    }
        //}

        //protected override void OnAdd()
        //{
        //    try
        //    {
        //        if (SelectedItem != null)
        //        {
        //            int currentRow = Items.IndexOf(SelectedItem);
        //            int targetRow = Items.ToList().FindIndex(currentRow, x => x.IsEditable);
        //            if (targetRow > -1)
        //            {
        //                TnQtChungTuChiTietHD4554Model sourceItem = Items.ElementAt(targetRow);
        //                TnQtChungTuChiTietHD4554Model targetItem = ObjectCopier.Clone(sourceItem);

        //                targetItem.Id = Guid.NewGuid();
        //                targetItem.FSoTien = null;
        //                //targetItem.TongSoThu = 0;
        //                //targetItem.QtKhauHaoTscđ = 0;
        //                //targetItem.QtTienLuong = 0;
        //                //targetItem.QtQtnskhac = 0;
        //                //targetItem.ChiPhiKhac = 0;
        //                //targetItem.ThueGtgt = 0;
        //                //targetItem.ThueTndn = 0;
        //                //targetItem.ThueTndnBqp = 0;
        //                //targetItem.PhiLePhi = 0;
        //                //targetItem.NsnnKhac = 0;
        //                //targetItem.NsnnKhacBqp = 0;
        //                //targetItem.PpNopNsqp = 0;
        //                //targetItem.PpBoSungKinhPhi = 0;
        //                //targetItem.PpTrichCacQuy = 0;
        //                targetItem.SGhiChu = null;
        //                targetItem.IsModified = true;
        //                targetItem.PropertyChanged += DetailModel_PropertyChanged;

        //                Items.Insert(targetRow + 1, targetItem);
        //                OnPropertyChanged(nameof(Items));
        //                OnPropertyChanged(nameof(IsSaveData));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message, ex);
        //    }
        //}

        private void OnSaveData()
        {
            try
            {
                List<TnQtChungTuChiTietHD4554Model> listChungTuChiTietAdd = Items.Where(x => x.IsEditable && x.IsModified && x.IIdTnQtChungTu == null).ToList();
                List<TnQtChungTuChiTietHD4554Model> listChungTuChiTietUpdate = Items.Where(x => x.IsEditable && x.IsModified && x.IIdTnQtChungTu != null).ToList();
                List<TnQtChungTuChiTietHD4554Model> listChungTuChiTietDelete = Items.Where(x => x.IsDeleted && x.IIdTnQtChungTu != null).ToList();

                // Thêm mới chứng từ chi tiết
                if (listChungTuChiTietAdd.Count > 0)
                {
                    listChungTuChiTietAdd = listChungTuChiTietAdd.Select(x =>
                    {
                        x.Id = Guid.NewGuid();
                        x.IIdTnQtChungTu = Model.Id;
                        x.INamNganSach = Model.INamNganSach;
                        x.INamLamViec = Model.INamLamViec;
                        x.INguonNganSach = Model.INguonNganSach;
                        //x.ITrangThai = NSEntityStatus.ACTIVED;
                        x.IIdMaDonVi = Model.IIdMaDonVi;
                        x.IThangQuy = Model.IThangQuy;
                        x.IThangQuyLoai = Model.IThangQuyLoai;
                        x.DNgayTao = DateTime.Now;
                        return x;
                    }).ToList();

                    List<TnQtChungTuChiTietHD4554> listChungTuChiTiets = _mapper.Map<List<TnQtChungTuChiTietHD4554>>(listChungTuChiTietAdd);
                    _tnQtChungTuChiTietHD4554Service.AddRange(listChungTuChiTiets);
                }

                // Cập nhật chứng từ chi tiết
                if (listChungTuChiTietUpdate.Count > 0)
                {
                    foreach (var item in listChungTuChiTietUpdate)
                    {
                        TnQtChungTuChiTietHD4554 chungTuChiTiet = _tnQtChungTuChiTietHD4554Service.FindById(item.Id);
                        item.DNgaySua = DateTime.Now;
                        item.SNguoiSua = _sessionService.Current.Principal;
                        _mapper.Map(item, chungTuChiTiet);
                        _tnQtChungTuChiTietHD4554Service.Update(chungTuChiTiet);
                    }
                }

                // Xóa chứng từ chi tiết
                if (listChungTuChiTietDelete.Count > 0)
                {
                    foreach (var item in listChungTuChiTietDelete)
                    {
                        _tnQtChungTuChiTietHD4554Service.Delete(item.Id);
                        item.FSoTien = 0;
                    }
                }

                if (listChungTuChiTietDelete.Count() != 0 || listChungTuChiTietUpdate.Count() != 0 || listChungTuChiTietAdd.Count != 0)
                {
                    Items = new ObservableCollection<TnQtChungTuChiTietHD4554Model>(Items.ToList().Select(x => { x.IsModified = false; return x; }));
                }

                LoadData();

                // cập nhật thông tin chứng từ
                TnQtChungTuHD4554 chungTu = _tnQtChungTuHD4554Service.FindById(Model.Id);
                chungTu.FTongSoTien = Model.FTongSoTien;
                //chungTu.TongSoThuSum = Model.TotalTongSoThu;
                _tnQtChungTuHD4554Service.Update(chungTu);

                string message = Resources.MsgSaveDone;
                var messageBox = new NSMessageBoxViewModel(message);
                DialogHost.Show(messageBox.Content, SettlementScreen.REAL_REVENUE_EXPENDITURE_DETAIL_DIALOG);

                //refresh dữ liệu ở màn index
                DataChangedEventHandler handler = UpdateSettlementVoucherEvent;
                if (handler != null)
                {
                    handler(Model, new EventArgs());
                }

                OnPropertyChanged(nameof(Items));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
