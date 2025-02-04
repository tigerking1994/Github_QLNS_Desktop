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
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.FunctionMap.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check.ImportCheck;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using PrintCommunicateSettlementLNS = VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check.PrintReport.PrintCommunicateSettlementLNS;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check
{
    public class CheckIndexViewModel : GridViewModelBase<NsSktChungTuModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly INsDonViService _donViService;
        private readonly ISysAuditLogService _log;
        private ICollectionView _sktChungTuModelsView;

        public override string FuncCode => NSFunctionCode.BUDGET_DEMANDCHECK_CHECK;
        public bool IsEdit => _selectedNsSktChungTuModel != null && !_selectedNsSktChungTuModel.BKhoa;
        public bool IsButtonEnable => _selectedNsSktChungTuModel != null;
        // public bool IsAddable => _sktChungTuModelItems.Count < 1;
        //public bool IsAddable => (VoucherTypeSelected != null && VoucherType.NSSD_Key == VoucherTypeSelected.ValueItem 
        //                          && (_sktChungTuModelItems.Count(n => !_benhVienTuChuItems.Contains(n.IIdMaDonVi)) < 1 
        //                          || _sktChungTuModelItems.Count(n => _benhVienTuChuItems.Contains(n.IIdMaDonVi)) < _benhVienTuChuItems.Count()))
        //                          || (VoucherTypeSelected != null && VoucherType.NSBD_Key == VoucherTypeSelected.ValueItem && _sktChungTuModelItems.Count < 1);

        public bool IsAddable => (VoucherTypeSelected != null && VoucherType.NSSD_Key == VoucherTypeSelected.ValueItem)
                                  || (VoucherTypeSelected != null && VoucherType.NSBD_Key == VoucherTypeSelected.ValueItem && _sktChungTuModelItems.Count < 1);

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }
        private bool _isOpenExcelPopup;
        public bool IsOpenExcelPopup
        {
            get => _isOpenExcelPopup;
            set => SetProperty(ref _isOpenExcelPopup, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private NsSktChungTuModel _selectedNsSktChungTuModel;
        public NsSktChungTuModel SelectedNsSktChungTuModel
        {
            get => _selectedNsSktChungTuModel;
            set
            {
                SetProperty(ref _selectedNsSktChungTuModel, value);
                if (_selectedNsSktChungTuModel != null)
                {
                    IsLock = _selectedNsSktChungTuModel.BKhoa;
                }
                OnPropertyChanged(nameof(IsButtonEnable));
                OnPropertyChanged(nameof(IsEdit));
            }
        }

        private ObservableCollection<NsSktChungTuModel> _sktChungTuModelItems;
        public ObservableCollection<NsSktChungTuModel> SktChungTuModelItems
        {
            get => _sktChungTuModelItems;
            set
            {
                SetProperty(ref _sktChungTuModelItems, value);
                OnPropertyChanged(nameof(IsAddable));
            }
        }

        private List<string> _benhVienTuChuItems;
        public List<string> BenhVienTuChuItems
        {
            get => _benhVienTuChuItems;
            set
            {
                SetProperty(ref _benhVienTuChuItems, value);
            }
        }

        private ObservableCollection<ComboboxItem> _voucherTypes = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> VoucherTypes
        {
            get => _voucherTypes;
            set => SetProperty(ref _voucherTypes, value);
        }

        private ComboboxItem _voucherTypeSelected;
        public ComboboxItem VoucherTypeSelected
        {
            get => _voucherTypeSelected;
            set
            {
                SetProperty(ref _voucherTypeSelected, value);
                LoadSktChungTus();
                OnPropertyChanged(nameof(ShowColNSBD));
                OnPropertyChanged(nameof(ShowColNSSD));
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
                LoadSktChungTus();
            }
        }

        private ObservableCollection<ComboboxItem> _budgetSourceTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BudgetSourceTypes
        {
            get => _budgetSourceTypes;
            set => SetProperty(ref _budgetSourceTypes, value);
        }

        private ComboboxItem _budgetSourceTypeSelected;

        public ComboboxItem BudgetSourceTypeSelected
        {
            get => _budgetSourceTypeSelected;
            set
            {
                SetProperty(ref _budgetSourceTypeSelected, value);
                if (_voucherTypeSelected != null)
                {
                    LoadSktChungTus();
                }
                OnPropertyChanged();
            }
        }

        public Visibility ShowColNSBD => VoucherTypeSelected != null && VoucherType.NSBD_Key == VoucherTypeSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowColNSSD => VoucherTypeSelected != null && VoucherType.NSSD_Key == VoucherTypeSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;
        public override Type ContentType => typeof(CheckIndex);
        public override string Description => "Chứng từ nhận số kiểm tra ngân sách năm " +
                                              _sessionService.Current.YearOfWork;
        public override string Name => "Nhận số kiểm tra";
        public override PackIconKind IconKind => PackIconKind.RhombusOutline;
        public DateTime DtNow => DateTime.Now;

        public CheckDialogViewModel CheckDialogViewModel { get; }
        public CheckDetailViewModel CheckDetailViewModel { get; }
        public ImportCheckViewModel ImportCheckViewModel { get; }
        public PrintReportReceiveTheCheckNumberViewModel PrintReportReceiveTheCheckNumberViewModel { get; }
        public PrintReportDemandOrgViewModel PrintReportDemandOrgViewModel { get; set; }
        public PrintReportSumaryCheckNumberViewModel PrintReportSumaryCheckNumberViewModel { get; set; }
        public View.Budget.DemandCheck.Check.ImportCheck.ImportCheck _importCheckView;

        public RelayCommand CancelCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintBvtcCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ImportDataCommand { get; }

        public CheckIndexViewModel(ISktChungTuService sktChungTuService,
            ISktChungTuChiTietService sktChungTuChiTietService, IMapper mapper,
            CheckDialogViewModel checkDialogViewModel, ISessionService sessionService, INsDonViService donViService,
            CheckDetailViewModel checkDetailViewModel, PrintReportReceiveTheCheckNumberViewModel printReportReceiveTheCheckNumberViewModel,
            ImportCheckViewModel importCheckViewModel,
            PrintReportDemandOrgViewModel printReportDemandOrgViewModel,
            PrintReportSumaryCheckNumberViewModel printReportSumaryCheckNumberViewModel,
            ISysAuditLogService log)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _sktChungTuService = sktChungTuService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _donViService = donViService;
            _log = log;

            CheckDialogViewModel = checkDialogViewModel;
            CheckDetailViewModel = checkDetailViewModel;
            ImportCheckViewModel = importCheckViewModel;
            PrintReportReceiveTheCheckNumberViewModel = printReportReceiveTheCheckNumberViewModel;
            PrintReportDemandOrgViewModel = printReportDemandOrgViewModel;
            PrintReportSumaryCheckNumberViewModel = printReportSumaryCheckNumberViewModel;

            PrintReportReceiveTheCheckNumberViewModel.ParentPage = this;
            CheckDialogViewModel.ParentPage = this;
            ImportCheckViewModel.ParentPage = this;
            PrintReportSumaryCheckNumberViewModel.ParentPage = this;

            CancelCommand = new RelayCommand(obj => { ParentPage.ParentPage.CurrentPage = null; });
            ExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            PrintCommand = new RelayCommand(OnPrint);
            LockCommand = new RelayCommand(OnLock);
            SearchCommand = new RelayCommand(obj => SearchData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintBvtcCommand = new RelayCommand(obj => OnPrintBvtcCommand());
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((NsSktChungTuModel)obj);
        }

        protected override void OnDelete()
        {
            if (SelectedNsSktChungTuModel == null) return;
            if (SelectedNsSktChungTuModel.SNguoiTao != _sessionService.Current.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedNsSktChungTuModel.SNguoiTao));
                return;
            }
            var messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTuSKT, SelectedNsSktChungTuModel.SSoChungTu, SelectedNsSktChungTuModel.SSoQuyetDinh,
                SelectedNsSktChungTuModel.DNgayChungTu.GetValueOrDefault(DateTime.Now).ToString("dd/MM/yyyy"));
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                OnDeleteHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void OnDeleteHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            if (SelectedNsSktChungTuModel != null)
            {
                var sktChungTu = _sktChungTuService.FindById(SelectedNsSktChungTuModel.Id);
                if (sktChungTu != null)
                {
                    _sktChungTuService.Delete(sktChungTu);
                    var predicate = PredicateBuilder.True<NsSktChungTuChiTiet>();
                    predicate = predicate.And(x => x.IIdCtsoKiemTra == sktChungTu.Id);
                    var sktChungTuChiTiets = _sktChungTuChiTietService.FindByCondition(predicate);
                    _sktChungTuChiTietService.RemoveRange(sktChungTuChiTiets);
                    _log.WriteLog(Resources.ApplicationName, "Nhận số kiểm tra", (int)TypeExecute.Delete, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    LoadSktChungTus();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    MessageBoxHelper.Info(Resources.CheckDeleteSuccess);
                }
            }
        }

        private void LoadBudgetSourceTypes()
        {
            BudgetSourceTypes = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem() { DisplayItem = "Tất cả", ValueItem = TypeLoaiNNS.TAT_CA.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách dự toán", ValueItem = TypeLoaiNNS.DU_TOAN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách bệnh viện tự chủ", ValueItem = TypeLoaiNNS.BENH_VIEN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách doanh nghiệp", ValueItem = TypeLoaiNNS.DOANH_NGHIEP.ToString() }
            };
            BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(0);
        }

        private void LoadBenhVienTuChu()
        {
            var idsDonViQuanLy = _sessionService.Current.IdsDonViQuanLy;
            var listBenhVienTuChu = _donViService.FindByCondition(x => x.Khoi.Equals(KhoiDonVi.BENH_VIEN_TU_CHU) && x.NamLamViec == _sessionService.Current.YearOfWork && x.ITrangThai.Equals(StatusType.ACTIVE)).ToList();
            BenhVienTuChuItems = listBenhVienTuChu.Select(n => n.IIDMaDonVi).Where(m => _sessionService.Current.IsQuanLyDonViCha || idsDonViQuanLy.Contains(m)).ToList();
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadSktChungTus();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((NsSktChungTuModel)eventArgs.Parameter);
        }

        private void LoadSktChungTus()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var yearOfBudget = _sessionService.Current.YearOfBudget;
            var budgetSource = _sessionService.Current.Budget;
            var currentIdDonVi = _sessionService.Current.IdDonVi;
            var loaiNguonNganSach = BudgetSourceTypeSelected != null ? Int32.Parse(BudgetSourceTypeSelected.ValueItem) : 0;
            //var iLoai = DemandCheckType.CHECK;
            var iLoai = DemandCheckType.CHECK + "," + DemandCheckType.CORPORATIZED_HOSPITAL;
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            IEnumerable<NsSktChungTu> listChungTu;
            //listChungTu = _sktChungTuService
            //   .FindChungTuIndexByConditionBVTC(iLoai, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, _sessionService.Current.Principal, "sp_skt_nhan_so_kiem_tra_1").ToList();
            listChungTu = _sktChungTuService
               .FindChungTuIndexByConditionBVTC(iLoai, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, _sessionService.Current.Principal, loaiNguonNganSach, "sp_skt_nhan_so_kiem_tra_1").ToList();

            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
            {
                listChungTu = listChungTu.Where(x => x.BKhoa).ToList();
            }
            else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
            {
                listChungTu = listChungTu.Where(x => x.BKhoa == false).ToList();
            }

            SktChungTuModelItems = _mapper.Map<ObservableCollection<NsSktChungTuModel>>(listChungTu);

            foreach (var item in SktChungTuModelItems)
            {
                var donVi = _donViService.FindByIdDonVi(item.IIdMaDonVi, item.INamLamViec);
                if (donVi != null)
                {
                    item.STenDonVi = donVi.TenDonVi;
                }
            }
            _sktChungTuModelsView = CollectionViewSource.GetDefaultView(SktChungTuModelItems);
            _sktChungTuModelsView.Filter = SktChungTuModelsFilter;
        }

        private bool SktChungTuModelsFilter(object obj)
        {
            var keyword = SearchText?.Trim().ToLower();
            var temp = (NsSktChungTuModel)obj;
            var condition = true;
            var condition2 = true;
            if (!string.IsNullOrEmpty(keyword))
                condition = condition && temp.SSoChungTu.ToLower().Contains(keyword) ||
                            temp.SSoQuyetDinh.ToLower().Contains(keyword) || temp.SMoTa.ToLower().Contains(keyword) ||
                            temp.SNguoiTao.ToLower().Contains(keyword) || temp.STenDonVi.ToLower().Contains(keyword);


            if (VoucherTypeSelected != null)
            {
                condition2 = condition2 && (temp.ILoaiChungTu.HasValue && temp.ILoaiChungTu.Value == Int32.Parse(VoucherTypeSelected.ValueItem));
            }
            return condition && condition2;
        }

        private void LockOrUnLockRegularBudget()
        {
            if (SelectedNsSktChungTuModel == null) return;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            IsLock = !IsLock;
            _sktChungTuService.LockOrUnlock(SelectedNsSktChungTuModel.Id, IsLock);
            MessageBoxHelper.Info(msgDone);
            _log.WriteLog(Resources.ApplicationName, "Nhận số kiểm tra", (int)TypeExecute.Update, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            var sktChungTu = SktChungTuModelItems.First(x => x.Id == SelectedNsSktChungTuModel.Id);
            sktChungTu.BKhoa = !SelectedNsSktChungTuModel.BKhoa;
        }


        private void LoadVoucherTypes()
        {
            var voucherTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key}
            };

            VoucherTypes = new ObservableCollection<ComboboxItem>(voucherTypes);
            VoucherTypeSelected = VoucherTypes.ElementAt(0);
        }

        protected override void OnAdd()
        {
            //check quyền được tạo mới
            List<DonVi> userAgency = _donViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
            if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
            {
                MessageBoxHelper.Warning(Resources.MsgRoleAdd);
                return;
            }
            CheckDialogViewModel.Name = "Thêm chứng từ";
            CheckDialogViewModel.Description = "Tạo mới chứng từ nhận số kiểm tra";
            CheckDialogViewModel.NsSktChungTuModel = new NsSktChungTuModel();
            CheckDialogViewModel.ListIdsSktChungTu = SktChungTuModelItems.Select(x => x.IIdMaDonVi).ToList();
            //CheckDialogViewModel.IsSummary = BenhVienTuChuItems.Any(n => !SktChungTuModelItems.Select(x => x.IIdMaDonVi).Contains(n));
            CheckDialogViewModel.IsSummary = BenhVienTuChuItems.Any();
            CheckDialogViewModel.Init();
            CheckDialogViewModel.SavedAction = obj =>
            {
                var sktChungTu = (NsSktChungTuModel)obj;
                _voucherTypeSelected = sktChungTu.ILoaiChungTu.HasValue
                    ? _voucherTypes.First(x => x.ValueItem.Equals(sktChungTu.ILoaiChungTu.Value.ToString()))
                    : _voucherTypes.First();
                OnPropertyChanged(nameof(VoucherTypeSelected));
                this.OnRefresh();
                OpenDetailDialog(sktChungTu);
            };
            var addView = new CheckDialog() { DataContext = CheckDialogViewModel };
            DialogHost.Show(addView, DemandCheckScreen.ROOT_DIALOG, null, ClosingEventHandler);
        }

        protected override void OnUpdate()
        {
            if (SelectedNsSktChungTuModel != null)
            {
                if (SelectedNsSktChungTuModel.SNguoiTao != _sessionService.Current.Principal)
                {
                    MessageBox.Show(string.Format(Resources.MsgRoleUpdate, SelectedNsSktChungTuModel.SNguoiTao), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                CheckDialogViewModel.NsSktChungTuModel = SelectedNsSktChungTuModel;
                CheckDialogViewModel.ListIdsSktChungTu = SktChungTuModelItems.Select(x => x.IIdMaDonVi).ToList();
                CheckDialogViewModel.Name = "Sửa chứng từ";
                CheckDialogViewModel.Description = "Cập nhật thông tin chứng từ số kiểm tra";
                //CheckDialogViewModel.IsSummary = BenhVienTuChuItems.Any(n => !SktChungTuModelItems.Select(x => x.IIdMaDonVi).Contains(n));
                CheckDialogViewModel.IsSummary = BenhVienTuChuItems.Any();
                CheckDialogViewModel.Init();
                CheckDialogViewModel.SavedAction = obj =>
                {
                    var sktChungTu = (NsSktChungTuModel)obj;
                    this.OnRefresh();
                    OpenDetailDialog(sktChungTu);
                };
                var editView = new CheckDialog { DataContext = CheckDialogViewModel };
                DialogHost.Show(editView, DemandCheckScreen.ROOT_DIALOG, null, ClosingEventHandler);
            }
        }

        private void OnLock(object obj)
        {
            if (IsLock)
            {
                List<DonVi> userAgency = _donViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                    return;
                }
            }
            else
            {
                if (SelectedNsSktChungTuModel.SNguoiTao != _sessionService.Current.Principal)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, SelectedNsSktChungTuModel.SNguoiTao));
                    return;
                }
            }
            var message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            MessageBoxResult dialogResult = MessageBoxHelper.Confirm(message);
            if (dialogResult == MessageBoxResult.Yes)
            {
                LockOrUnLockRegularBudget();
                OnPropertyChanged(nameof(IsEdit));
                LockStatusSelected = LockStatus.ElementAt(0);
            }
        }

        /// <summary>
        ///     Mở màn hình in
        /// </summary>
        /// <param name="param"></param>
        private void OnPrint(object param)
        {
            var dialogType = (int)param;
            switch (dialogType)
            {
                case (int)DemandCheckPrintType.THE_REPORT_RECEIVES_THE_CHECK_NUMBER:
                    PrintReportReceiveTheCheckNumberViewModel._demandCheckPrintType =
                        DemandCheckPrintType.THE_REPORT_RECEIVES_THE_CHECK_NUMBER;
                    PrintReportReceiveTheCheckNumberViewModel.Init();
                    PrintReportReceiveTheCheckNumberViewModel.ShowDialogHost();
                    break;
                case (int)DemandCheckPrintType.REPORT_SO_SANH_NHAN_SKT_NAM_TRUOC_NAM_NAY:
                    PrintReportReceiveTheCheckNumberViewModel._demandCheckPrintType = DemandCheckPrintType.REPORT_SO_SANH_NHAN_SKT_NAM_TRUOC_NAM_NAY;
                    PrintReportReceiveTheCheckNumberViewModel.Init();
                    PrintReportReceiveTheCheckNumberViewModel.ShowDialogHost();
                    break;
                case (int)DemandCheckPrintType.SUMMARY_REPORT_OF_TEST_NUMBER_ALLOCATION:
                    var view2 = new PrintCommunicateSettlementLNS();
                    //show the dialog
                    DialogHost.Show(view2, "RootDialog", null, null);
                    break;
                case (int)DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH:
                    var demandCheckPrintType = (DemandCheckPrintType)(int)param;
                    object content;
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = demandCheckPrintType;
                    PrintReportDemandOrgViewModel.Init();
                    content = new PrintReportDemandOrg
                    {
                        DataContext = PrintReportDemandOrgViewModel
                    };
                    DialogHost.Show(content, DemandCheckScreen.ROOT_DIALOG, null, null);
                    break;
            }
        }

        private void OnPrintBvtcCommand()
        {
            PrintReportSumaryCheckNumberViewModel.Init();
            var view = new PrintReportSumaryCheckNumber
            {
                DataContext = PrintReportSumaryCheckNumberViewModel
            };
            DialogHost.Show(view, "RootDialog", null, null);
        }

        protected override void OnRefresh()
        {
            LoadSktChungTus();
        }

        private void SearchData()
        {
            _sktChungTuModelsView.Refresh();
        }

        private void OpenDetailDialog(NsSktChungTuModel itemDetail)
        {
            CheckDetailViewModel.Model = itemDetail;
            CheckDetailViewModel.ShowColNSBD = itemDetail.ILoaiChungTu.Equals(int.Parse(VoucherType.NSBD_Key)) ? Visibility.Visible : Visibility.Collapsed;
            CheckDetailViewModel.ShowColNSSD = itemDetail.ILoaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key)) ? Visibility.Visible : Visibility.Collapsed;
            CheckDetailViewModel.Init();
            var view = new CheckDetail { DataContext = CheckDetailViewModel };
            //view.Owner = ApplicationHelper.GetCurrentMainWindow();
            view.ShowDialog();
        }

        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }

        public override void Init()
        {
            base.Init();
            LoadLockStatus();
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            LoadBenhVienTuChu();
            LoadVoucherTypes();
            CheckDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
            LoadBudgetSourceTypes();
        }

        private void OnImportData()
        {
            ImportCheckViewModel.Init();
            ImportCheckViewModel.SavedAction = obj =>
            {
                _importCheckView.Close();
                this.LoadData();
                OpenDetailDialog((NsSktChungTuModel)obj);
            };

            _importCheckView = new View.Budget.DemandCheck.Check.ImportCheck.ImportCheck { DataContext = ImportCheckViewModel };
            _importCheckView.ShowDialog();
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

        public void OnChangeVisibilityColumn()
        {
            OnPropertyChanged(nameof(ShowColNSBD));
            OnPropertyChanged(nameof(ShowColNSSD));
        }
    }
}