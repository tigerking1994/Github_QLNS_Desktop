using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.Settlement.VoucherList;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.VoucherList
{
    public class VoucherListIndexViewModel : GridViewModelBase<VoucherListModel>
    {
        private INsBkChungTuService _chungTuService;
        private INsBkChungTuChiTietService _chungTuChiTietService;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private ICollectionView _listVoucherList;
        private SessionInfo _sessionInfo;
        private VoucherListDialogViewModel VoucherListDialogViewModel;
        private VoucherListDetailViewModel VoucherListDetailViewModel;
        private List<NsBkChungTu> _listChungTu;
        private PrintVoucherListViewModel PrintVoucherListViewModel;
        private PrintSummaryVoucherListViewModel PrintSummaryVoucherListViewModel;

        public override string FuncCode => NSFunctionCode.BUDGET_SETTLEMENT_VOUCHERLIST;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Bảng kê chứng từ";
        public override Type ContentType => typeof(View.Budget.Settlement.VoucherList.VoucherListIndex);
        public override string Description => "Bảng kê chứng từ chi tiêu";
        public override PackIconKind IconKind => PackIconKind.TicketPercentOutline;

        private ObservableCollection<VoucherListModel> _voucherLists;
        public ObservableCollection<VoucherListModel> VoucherLists
        {
            get => _voucherLists;
            set => SetProperty(ref _voucherLists, value);
        }

        private List<ComboboxItem> _quarters;
        public List<ComboboxItem> Quarters
        {
            get => _quarters;
            set => SetProperty(ref _quarters, value);
        }

        private ComboboxItem _quarterSelected;
        public ComboboxItem QuarterSelected
        {
            get => _quarterSelected;
            set
            {
                SetProperty(ref _quarterSelected, value);
                _listVoucherList.Refresh();
            }
        }

        private ObservableCollection<ComboboxItem> _lockStatus = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> LockStatus
        {
            get => _lockStatus;
            set => SetProperty(ref _lockStatus, value);
        }

        private ComboboxItem _lockStatusSelected;

        public ComboboxItem LockStatusSelected
        {
            get => _lockStatusSelected;
            set
            {
                SetProperty(ref _lockStatusSelected, value);
                _listVoucherList?.Refresh();
            }
        }

        public bool IsLock => VoucherLists.Where(x => x.IsChecked).All(x => x.BKhoa) || LockStatusSelected.ValueItem == "1";

        public bool IsEnableLock => VoucherLists.Where(x => x.IsChecked).Select(x => x.BKhoa).Distinct().Count() == 1;

        public bool IsEdit => SelectedItem != null && !SelectedItem.BKhoa;

        private bool _isOpenDialog;
        public bool IsOpenDialog
        {
            get => _isOpenDialog;
            set => SetProperty(ref _isOpenDialog, value);
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                return VoucherLists.All(item => item.IsChecked);
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, VoucherLists);
                    OnPropertyChanged();
                }
            }
        }

        private static void SelectAll(bool select, IEnumerable<VoucherListModel> models)
        {
            foreach (var model in models.Where(x => x.IsFilter))
            {
                model.IsChecked = select;
            }
        }

        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }

        public VoucherListIndexViewModel(INsBkChungTuService chungTuService,
            INsBkChungTuChiTietService chungTuChiTietService,
            IMapper mapper,
            ISessionService sessionService,
            VoucherListDialogViewModel voucherListDialogViewModel,
            VoucherListDetailViewModel voucherListDetailViewModel,
            PrintVoucherListViewModel printVoucherListViewModel,
            PrintSummaryVoucherListViewModel printSummaryVoucherListViewModel)
        {
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _mapper = mapper;
            _sessionService = sessionService;
            VoucherListDialogViewModel = voucherListDialogViewModel;
            VoucherListDetailViewModel = voucherListDetailViewModel;
            PrintVoucherListViewModel = printVoucherListViewModel;
            PrintSummaryVoucherListViewModel = printSummaryVoucherListViewModel;

            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            VoucherListDetailViewModel.UpdateVoucherListEvent += RefreshAfterSaveData;
            ResetCondition();
            LoadQuarters();
            LoadLockStatus();
            LoadData();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEnableLock));
        }

        private void ResetCondition()
        {
            _quarterSelected = null;
            OnPropertyChanged(nameof(QuarterSelected));
        }


        /// <summary>
        /// Tạo data cho combobox qúy
        /// </summary>
        private void LoadQuarters()
        {
            _quarters = new List<ComboboxItem>();
            _quarters.Add(new ComboboxItem("Quý I", "3"));
            _quarters.Add(new ComboboxItem("Quý II", "6"));
            _quarters.Add(new ComboboxItem("Quý III", "9"));
            _quarters.Add(new ComboboxItem("Quý IV", "12"));
        }

        private void LoadLockStatus()
        {
            var lockStatus = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Toàn bộ", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đã khóa", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Chưa khóa", ValueItem = "2"},
            };

            LockStatus = new ObservableCollection<ComboboxItem>(lockStatus);
            LockStatusSelected = LockStatus.ElementAt(0);
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            var predicate = PredicateBuilder.True<NsBkChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork
                                           && x.INamNganSach == _sessionInfo.YearOfBudget
                                           && x.IIdMaNguonNganSach == _sessionInfo.Budget);
            if (_quarterSelected != null)
                predicate = predicate.And(x => x.IThangQuy == int.Parse(_quarterSelected.ValueItem));
            _listChungTu = _chungTuService.FindByCondition(predicate);


            _voucherLists = _mapper.Map<ObservableCollection<VoucherListModel>>(_listChungTu);
            if (_voucherLists.Count > 0)
                SelectedItem = _voucherLists.First();
            _listVoucherList = CollectionViewSource.GetDefaultView(_voucherLists);
            _listVoucherList.Filter = ListVoucherListFilter;
            foreach (var model in VoucherLists)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(VoucherListModel.IsChecked))
                    {
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                        OnPropertyChanged(nameof(IsEnableLock));
                        OnPropertyChanged(nameof(IsLock));
                    }
                };
            }

            OnPropertyChanged(nameof(VoucherLists));
        }

        private bool ListVoucherListFilter(object obj)
        {
            bool result = true;
            var item = (VoucherListModel)obj;
            if (QuarterSelected != null)
                result = result && item.IThangQuy == int.Parse(QuarterSelected.ValueItem);

            if (LockStatusSelected != null && LockStatusSelected.ValueItem != "0")
                result = result && item.BKhoa == (LockStatusSelected.ValueItem == "1");

            item.IsFilter = result;
            return result;
        }

        /// <summary>
        /// Mở dialog confirm khóa/ mở khóa chứng từ
        /// </summary>
        /// <param name="obj"></param>
        protected override void OnLockUnLock()
        {
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            MessageBoxResult result = MessageBoxHelper.Confirm(message);
            if (result == MessageBoxResult.Yes)
                LockConfirmEventHandler();
            LockStatusSelected = LockStatus.ElementAt(0);
        }

        /// <summary>
        /// xử lý khóa/mở khóa chứng từ sau khi đóng dialog
        /// </summary>
        private void LockConfirmEventHandler()
        {
            string msgDone = SelectedItem.BKhoa ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            bool isLock = IsLock;
            VoucherLists.Where(x => x.IsChecked).Select(x => x.BKhoa = !isLock).ToList();
            var listChecked = _mapper.Map<ObservableCollection<NsBkChungTu>>(VoucherLists.Where(x => x.IsChecked));
            _chungTuService.UpdateRange(listChecked);
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
            MessageBoxHelper.Info(msgDone);
        }

        protected override void OnAdd()
        {
            base.OnAdd();
            VoucherListDialogViewModel.Id = Guid.Empty;
            int voucherNoIndex = 1;
            if (_listChungTu != null && _listChungTu.Count > 0)
                voucherNoIndex = _listChungTu.Max(x => x.ISoChungTuIndex).Value + 1;
            VoucherListDialogViewModel.VoucherNoIndex = voucherNoIndex;
            VoucherListDialogViewModel.Init();
            VoucherListDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OpenVoucherListDetailDialog((VoucherListModel)obj);
            };
            var view = new VoucherListDialog { DataContext = VoucherListDialogViewModel };
            DialogHost.Show(view, SettlementScreen.ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            VoucherListDialogViewModel.Id = SelectedItem.Id;
            VoucherListDialogViewModel.Init();
            VoucherListDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            var view = new VoucherListDialog { DataContext = VoucherListDialogViewModel };
            DialogHost.Show(view, SettlementScreen.ROOT_DIALOG);
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, SelectedItem.DNgayChungTu.HasValue ? SelectedItem.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty);

            MessageBoxResult result = MessageBoxHelper.Confirm(messageBuilder.ToString());
            if (result == MessageBoxResult.Yes)
                DeleteSelectedVoucher();
        }

        /// <summary>
        /// xử lý xóa chứng từ sau khi đóng dialog
        /// </summary>
        private void DeleteSelectedVoucher()
        {
            Guid voucherId = SelectedItem.Id;
            _chungTuService.Delete(voucherId);
            _chungTuChiTietService.DeleteByVoucherId(voucherId);
            var itemDeleted = VoucherLists.Where(x => x.Id == voucherId).First();
            VoucherLists.Remove(itemDeleted);

            var chungtuDeletd = _listChungTu.Where(x => x.Id == voucherId).First();
            _listChungTu.Remove(chungtuDeletd);
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            VoucherListModel voucherList = (VoucherListModel)obj;
            if (voucherList != null)
                OpenVoucherListDetailDialog(voucherList);
        }

        /// <summary>
        /// Mở màn hình chi tiết chứng từ
        /// </summary>
        /// <param name="settlementVoucher"></param>
        private void OpenVoucherListDetailDialog(VoucherListModel voucherList)
        {
            SelectedItem = voucherList;
            VoucherListDetailViewModel.Model = voucherList;
            VoucherListDetailViewModel.Init();
            VoucherListDetail view = new VoucherListDetail { DataContext = VoucherListDetailViewModel };
            view.ShowDialog();
        }

        private void RefreshAfterSaveData(object sender, EventArgs e)
        {
            VoucherListModel item = VoucherLists.Where(x => x.Id == SelectedItem.Id).First();
            item.FTongTuChi = ((VoucherListModel)sender).FTongTuChi;
            item.FTongHienVat = ((VoucherListModel)sender).FTongHienVat;
        }

        /// <summary>
        /// Mở màn hình in
        /// </summary>
        /// <param name="param"></param>
        private void OpenPrintDialog(object param)
        {
            int dialogType = (int)param;
            switch (dialogType)
            {
                case (int)VoucherListPrintType.PRINT_VOUCHER_LIST:
                    PrintVoucherListViewModel.VoucherList = SelectedItem;
                    PrintVoucherListViewModel.Init();
                    var view1 = new PrintVoucherList { DataContext = PrintVoucherListViewModel };
                    //show the dialog
                    DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)VoucherListPrintType.PRINT_SUMMARY_VOUCHER_LIST:
                    PrintSummaryVoucherListViewModel.Init();
                    var view2 = new PrintSummaryVoucherList { DataContext = PrintSummaryVoucherListViewModel };
                    //show the dialog
                    DialogHost.Show(view2, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
            }
        }
    }
}
