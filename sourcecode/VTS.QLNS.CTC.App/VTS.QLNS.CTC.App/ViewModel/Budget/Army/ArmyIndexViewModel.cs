using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Settlement.Army;
using VTS.QLNS.CTC.App.View.Budget.Settlement.Import;
using VTS.QLNS.CTC.App.View.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Army
{
    public class ArmyIndexViewModel : ViewModelBase
    {
        private IMapper _mapper;
        private INsQsChungTuService _chungTuService;
        private INsQsChungTuChiTietService _chungTuChiTietService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private ArmyDialogViewModel _armyDialogViewModel;
        private ArmyDetailViewModel _armyDetailViewModel;
        private ArmyDialog _armyDialog;
        private PrintArmyViewModel PrintArmyViewModel;
        private PrintArmyUpDownViewModel PrintArmyUpDownViewModel;
        private PrintArmyAverageViewModel PrintArmyAverageViewModel;
        private PrintArmyRegularViewModel PrintArmyRegularViewModel;
        private PrintArmyLeaveViewModel PrintArmyLeaveViewModel;
        private PrintArmyJurisprudenceViewModel PrintArmyJurisprudenceViewModel;
        private ArmyImportViewModel ArmyImportViewModel;
        private ArmyImport _armyImportView;

        public override string FuncCode => NSFunctionCode.BUDGET_SETTLEMENT_ARMY;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Quyết toán quân số";
        public override Type ContentType => typeof(View.Budget.Settlement.Army.ArmyIndex);
        public override string Description => "Chứng từ quyết toán quân số";
        public override PackIconKind IconKind => PackIconKind.ShieldAccount;

        private ObservableCollection<ArmyVoucherModel> _armyVouchers;
        public ObservableCollection<ArmyVoucherModel> ArmyVouchers
        {
            get => _armyVouchers;
            set => SetProperty(ref _armyVouchers, value);
        }

        private ArmyVoucherModel _selectedArmyVoucher;
        public ArmyVoucherModel SelectedArmyVoucher
        {
            get => _selectedArmyVoucher;
            set
            {
                SetProperty(ref _selectedArmyVoucher, value);
                if (_selectedArmyVoucher != null)
                {
                    IsLock = _selectedArmyVoucher.BKhoa;
                }
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
                LoadArmyVoucher();
            }

        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        public RelayCommand LockCommand { get; }
        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand SelectionDoubleClickCommand { get; }
        public RelayCommand DeleteArmyVoucherCommand { get; }
        public RelayCommand AddArmyVoucherCommand { get; }
        public RelayCommand EditArmyVoucherCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ImportDataCommand { get; }

        public ArmyIndexViewModel(IMapper mapper,
            INsQsChungTuService chungTuService,
            INsQsChungTuChiTietService chungTuChiTietService,
            ISessionService sessionService,
            ArmyDialogViewModel armyDialogViewModel,
            ArmyDetailViewModel armyDetailViewModel,
            PrintArmyViewModel printArmyViewModel,
            PrintArmyUpDownViewModel printArmyUpDownViewModel,
            PrintArmyAverageViewModel printArmyAverageViewModel,
            PrintArmyRegularViewModel printArmyRegularViewModel,
            PrintArmyLeaveViewModel printArmyLeaveViewModel,
            PrintArmyJurisprudenceViewModel printArmyJurisprudenceViewModel,
            ArmyImportViewModel armyImportViewModel)
        {
            _mapper = mapper;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _sessionService = sessionService;
            _armyDialogViewModel = armyDialogViewModel;
            _armyDetailViewModel = armyDetailViewModel;
            PrintArmyViewModel = printArmyViewModel;
            PrintArmyUpDownViewModel = printArmyUpDownViewModel;
            PrintArmyAverageViewModel = printArmyAverageViewModel;
            PrintArmyRegularViewModel = printArmyRegularViewModel;
            PrintArmyLeaveViewModel = printArmyLeaveViewModel;
            PrintArmyJurisprudenceViewModel = printArmyJurisprudenceViewModel;
            ArmyImportViewModel = armyImportViewModel;

            LockCommand = new RelayCommand(obj => OnLockArmyVoucher());
            SelectionChangedCommand = new RelayCommand(obj =>
            {
                SelectedArmyVoucher = (ArmyVoucherModel)obj;
                if (SelectedArmyVoucher != null && SelectedArmyVoucher.BKhoa)
                    IsEdit = false;
                else IsEdit = true;
            });
            DeleteArmyVoucherCommand = new RelayCommand(obj => OnDeleteArmyVoucher());
            AddArmyVoucherCommand = new RelayCommand(obj => OnOpenVoucherDialog((int)SettlementAction.ADD_SETTLEMENT_VOUCHER));
            EditArmyVoucherCommand = new RelayCommand(obj => OnOpenVoucherDialog((int)SettlementAction.EDIT_SETTLEMENT_VOUCHER));
            RefreshCommand = new RelayCommand(obj => LoadArmyVoucher());
            SelectionDoubleClickCommand = new RelayCommand(obj => OnOpenArmyVoucherDetailDialog((ArmyVoucherModel)obj));
            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            ImportDataCommand = new RelayCommand(obj => OnImportData());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadArmyVoucher();
            LoadLockStatus();
        }

        private void LoadArmyVoucher()
        {
            var predicate = PredicateBuilder.True<NsQsChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);

            List<NsQsChungTu> chungTus = _chungTuService.FindByCondition(predicate);

            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
            {
                chungTus = chungTus.Where(x => x.BKhoa).ToList();
            }
            else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
            {
                chungTus = chungTus.Where(x => !x.BKhoa).ToList();
            }

            ArmyVouchers = _mapper.Map<ObservableCollection<ArmyVoucherModel>>(chungTus);

            SelectedArmyVoucher = ArmyVouchers.FirstOrDefault();
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

        private void OnLockArmyVoucher()
        {
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            MessageBoxResult result = MessageBoxHelper.Confirm(message);
            if (result == MessageBoxResult.Yes)
                LockOrUnLockArmyVoucher();
        }

        /// <summary>
        /// Khóa hoặc mở khóa chứng từ
        /// </summary>
        private void LockOrUnLockArmyVoucher()
        {
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            IsLock = !IsLock;
            _chungTuService.LockOrUnlock(SelectedArmyVoucher.Id, !SelectedArmyVoucher.BKhoa);
            var armyVoucher = ArmyVouchers.Where(x => x.Id == SelectedArmyVoucher.Id).First();
            armyVoucher.BKhoa = !SelectedArmyVoucher.BKhoa;
            IsEdit = !armyVoucher.BKhoa;
            MessageBoxHelper.Info(msgDone);
        }

        /// <summary>
        /// mở dialog confirm xóa voucher
        /// </summary>
        private void OnDeleteArmyVoucher()
        {
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedArmyVoucher.SSoChungTu, SelectedArmyVoucher.DNgayChungTu == null ? string.Empty : SelectedArmyVoucher.DNgayChungTu.Value.ToString("dd/MM/yyyy"));

            MessageBoxResult result = MessageBoxHelper.Confirm(messageBuilder.ToString());
            if (result == MessageBoxResult.Yes)
            {
                DeleteSelectedVoucher();
            }
        }

        private void DeleteSelectedVoucher()
        {
            var iThangQuyCurrent = _chungTuService.FindById(SelectedArmyVoucher.Id).IThangQuy;

            var _listMonth = _chungTuService.FindMonthOfArmy(_sessionInfo.YearOfWork);

            var monthLast = iThangQuyCurrent;
            while (_listMonth.Contains(monthLast))
            {
                monthLast++;
            }


            var listChungTuChiTiet = _chungTuChiTietService.FindByCondition(x => x.IThangQuy >= iThangQuyCurrent && x.INamLamViec == _sessionInfo.YearOfWork).ToList();
            var listChungTuChiTietNext = listChungTuChiTiet.Where(x => x.IThangQuy > iThangQuyCurrent && x.IThangQuy < monthLast && (x.SKyHieu == "100" || x.SKyHieu == "700")).ToList();
            var dataM7Delete = listChungTuChiTiet.Where(x => x.IThangQuy == (iThangQuyCurrent + 1) && x.SKyHieu == "100");
            var dataM7DeepCopy = dataM7Delete.Select(item => ObjectCopier.Clone(item)).ToList();

            //var dataM7Delete = _chungTuChiTietService.FindByCondition(x => x.IThangQuy == (iThangQuyCurrent + 1) && x.SKyHieu == "100" && x.INamLamViec == _sessionInfo.YearOfWork).ToList();



            _chungTuService.Delete(SelectedArmyVoucher.Id);
            _chungTuChiTietService.DeleteByVoucherId(SelectedArmyVoucher.Id);


            // Cập nhật chi tiết 100, 700 các tháng sau

            foreach (var item in listChungTuChiTietNext)
            {
                var dataDelete = dataM7DeepCopy.FirstOrDefault(x => x.IIdMaDonVi == item.IIdMaDonVi);
                item.FSoThieuUy -= dataDelete.FSoThieuUy;
                item.FSoTrungUy -= dataDelete.FSoTrungUy;
                item.FSoThuongUy -= dataDelete.FSoThuongUy;
                item.FSoDaiUy -= dataDelete.FSoDaiUy;
                item.FSoThieuTa -= dataDelete.FSoThieuTa;
                item.FSoTrungTa -= dataDelete.FSoTrungTa;
                item.FSoThuongTa -= dataDelete.FSoThuongTa;
                item.FSoDaiTa -= dataDelete.FSoDaiTa;
                item.FSoTuong -= dataDelete.FSoTuong;
                item.FSoBinhNhi -= dataDelete.FSoBinhNhi;
                item.FSoBinhNhat -= dataDelete.FSoBinhNhat;
                item.FSoHaSi -= dataDelete.FSoHaSi;
                item.FSoTrungSi -= dataDelete.FSoTrungSi;
                item.FSoThuongSi -= dataDelete.FSoThuongSi;
                item.FSoThuongTaQNCN -= dataDelete.FSoThuongTaQNCN;
                item.FSoTrungTaQNCN -= dataDelete.FSoTrungTaQNCN;
                item.FSoThieuTaQNCN -= dataDelete.FSoThieuTaQNCN;
                item.FSoDaiUyQNCN -= dataDelete.FSoDaiUyQNCN;
                item.FSoThuongUyQNCN -= dataDelete.FSoThuongUyQNCN;
                item.FSoTrungUyQNCN -= dataDelete.FSoTrungUyQNCN;
                item.FSoThieuUyQNCN -= dataDelete.FSoThieuUyQNCN;
                item.FSoVcqp -= dataDelete.FSoVcqp;
                item.FSoCnvqp -= dataDelete.FSoCnvqp;
                item.FSoLdhd -= dataDelete.FSoLdhd;
                //_chungTuChiTietService.Update(item);
            }

            _chungTuChiTietService.UpdateRange(listChungTuChiTietNext);
            LoadArmyVoucher();
            MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
        }

        /// <summary>
        /// Mở màn hình add/edit chứng từ
        /// </summary>
        private void OnOpenVoucherDialog(int actionType)
        {
            if (actionType == (int)SettlementAction.EDIT_SETTLEMENT_VOUCHER)
            {
                _armyDialogViewModel.Id = SelectedArmyVoucher.Id;
            }
            else
            {
                _armyDialogViewModel.Id = Guid.Empty;
                int voucherNoIndex = 1;
                if (ArmyVouchers != null && ArmyVouchers.Count > 0)
                {
                    var item = ArmyVouchers.OrderByDescending(x => x.ISoChungTuIndex).FirstOrDefault();
                    voucherNoIndex = item.ISoChungTuIndex == null ? 1 : item.ISoChungTuIndex.Value + 1;
                }
                _armyDialogViewModel.VoucherNoIndex = voucherNoIndex;
            }
            _armyDialogViewModel.SavedAction = obj =>
            {
                LoadArmyVoucher();
                OnOpenArmyVoucherDetailDialog((ArmyVoucherModel)obj);
            };
            _armyDialogViewModel.Init();
            _armyDialog = new ArmyDialog { DataContext = _armyDialogViewModel };
            DialogHost.Show(_armyDialog, SettlementScreen.ROOT_DIALOG);
        }

        /// <summary>
        /// Mở màn hình chi tiết chứng từ
        /// </summary>
        /// <param name="armyVoucher"></param>
        private void OnOpenArmyVoucherDetailDialog(ArmyVoucherModel armyVoucher)
        {
            _armyDetailViewModel.ArmyVoucher = armyVoucher;
            _armyDetailViewModel.Init();
            ArmyDetail view = new ArmyDetail { DataContext = _armyDetailViewModel };
            view.ShowDialog();
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
                case (int)ArmyPrintType.PRINT_ARMY:
                    PrintArmyViewModel.Init();
                    var view1 = new PrintArmy { DataContext = PrintArmyViewModel };
                    //show the dialog
                    DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)ArmyPrintType.PRINT_ARMY_UP_DOWN:
                    PrintArmyUpDownViewModel.Init();
                    var view2 = new PrintArmyUpDown { DataContext = PrintArmyUpDownViewModel };
                    //show the dialog
                    DialogHost.Show(view2, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)ArmyPrintType.PRINT_ARMY_AVERAGE:
                    PrintArmyAverageViewModel.Init();
                    var view3 = new PrintArmyAverage { DataContext = PrintArmyAverageViewModel };
                    //show the dialog
                    DialogHost.Show(view3, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)ArmyPrintType.PRINT_ARMY_REGULAR:
                    PrintArmyRegularViewModel.Init();
                    var view4 = new PrintArmyRegular { DataContext = PrintArmyRegularViewModel };
                    //show the dialog
                    DialogHost.Show(view4, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)ArmyPrintType.PRINT_ARMY_LEAVE:
                    PrintArmyLeaveViewModel.Init();
                    var view5 = new PrintArmyLeave { DataContext = PrintArmyLeaveViewModel };
                    //show the dialog
                    DialogHost.Show(view5, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)ArmyPrintType.PRINT_ARMY_JURISPRUDENCE:
                    PrintArmyJurisprudenceViewModel.Init();
                    var view6 = new PrintArmyJurisprudence { DataContext = PrintArmyJurisprudenceViewModel };
                    //show the dialog
                    DialogHost.Show(view6, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
            }
        }

        private void OnImportData()
        {
            int voucherNoIndex = 1;
            if (ArmyVouchers != null && ArmyVouchers.Count > 0)
            {
                var item = ArmyVouchers.OrderByDescending(x => x.ISoChungTuIndex).FirstOrDefault();
                voucherNoIndex = item.ISoChungTuIndex == null ? 1 : item.ISoChungTuIndex.Value + 1;
            }
            ArmyImportViewModel.VoucherNoIndex = voucherNoIndex;
            ArmyImportViewModel.Init();
            ArmyImportViewModel.SavedAction = obj =>
            {
                _armyImportView.Close();
                LoadArmyVoucher();
                OnOpenArmyVoucherDetailDialog((ArmyVoucherModel)obj);
            };
            _armyImportView = new ArmyImport() { DataContext = ArmyImportViewModel };
            //var result = DialogHost.Show(view, SettlementScreen.ROOT_DIALOG);
            _armyImportView.ShowDialog();
        }
    }
}
