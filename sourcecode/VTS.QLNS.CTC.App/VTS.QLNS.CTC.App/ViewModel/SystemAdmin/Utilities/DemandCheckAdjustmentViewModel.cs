using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.FunctionMap.PrintReport;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.View.SystemAdmin.Utilities;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.Utilities
{
    public class DemandCheckAdjustmentViewModel : GridViewModelBase<NsSktChungTuModel>
    {
        public ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly INsDonViService _donViService;
        private readonly ISysAuditLogService _log;
        private ICollectionView _sktChungTuModelsView;
        public SktMucLucService _mlnsService;
        public AuthenticationInfo _authenticationInfo;
        public override string FuncCode => NSFunctionCode.BUDGET_DEMANDCHECK_CHECK;
        public bool IsEdit => _selectedNsSktChungTuModel != null && !_selectedNsSktChungTuModel.BKhoa;
        public bool IsButtonEnable => _selectedNsSktChungTuModel != null;
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

        public ObservableCollection<ComboboxItem> LstTrangThai { get; set; }

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

        private ObservableCollection<SktMucLucModel> _sktMLSktModelItems;
        public ObservableCollection<SktMucLucModel> SktMLSktModelItems
        {
            get => _sktMLSktModelItems;
            set
            {
                SetProperty(ref _sktMLSktModelItems, value);
                OnPropertyChanged(nameof(IsAddable));
            }
        }

        private ObservableCollection<SktMucLucModel> _sktMLSktNextYearModelItems;
        public ObservableCollection<SktMucLucModel> SktMLSktNextYearModelItems
        {
            get => _sktMLSktNextYearModelItems;
            set
            {
                SetProperty(ref _sktMLSktNextYearModelItems, value);
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
        private List<string> mlnsType = new List<string>
        {
            "TNG3", "TNG2", "TNG1", "TNG", "NG"
        };

        public Visibility ShowColNSBD => VoucherTypeSelected != null && VoucherType.NSBD_Key == VoucherTypeSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowColNSSD => VoucherTypeSelected != null && VoucherType.NSSD_Key == VoucherTypeSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;
        public override Type ContentType => typeof(DemandCheckAdjustmentIndex);
        public override string Description => "Chứng từ nhận số kiểm tra ngân sách năm " +
                                              _sessionService.Current.YearOfWork;
        public override string Name => "Điều chỉnh số nhu cầu Ngân sách";
        public override PackIconKind IconKind => PackIconKind.Adjust;
        public DateTime DtNow => DateTime.Now;

        public CheckDetailViewModel CheckDetailViewModel { get; }

        public RelayCommand CancelCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintBvtcCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand AdjustDataCommand { get; }
        public RelayCommand RevertDataCommand { get; }

        public DemandCheckAdjustmentViewModel(ISktChungTuService sktChungTuService,
            ISktChungTuChiTietService sktChungTuChiTietService, IMapper mapper,
            ISessionService sessionService, INsDonViService donViService,
            CheckDetailViewModel checkDetailViewModel,
           
            ISysAuditLogService log,
            ISktMucLucService service)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _sktChungTuService = sktChungTuService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _donViService = donViService;
            _log = log;

            CheckDetailViewModel = checkDetailViewModel;
            _mlnsService = service as SktMucLucService;

            CancelCommand = new RelayCommand(obj => { ParentPage.ParentPage.CurrentPage = null; });
            ExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            LockCommand = new RelayCommand(OnLock);
            SearchCommand = new RelayCommand(obj => SearchData());
            AdjustDataCommand = new RelayCommand(obj => OnAdjust());
            RevertDataCommand = new RelayCommand(obj => OnRevertMLSKT());
        }

        private void InitBaseData()
        {
            LstTrangThai = new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Pending", ValueItem = "0" },
                    new ComboboxItem { DisplayItem = "Active", ValueItem = "1" }
                };
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            if(SelectedNsSktChungTuModel != null)
            {
                base.OnSelectionDoubleClick(obj);
                OpenDetailDialog((NsSktChungTuModel)obj);
            }
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
            var iLoai = DemandCheckType.CHECK + "," + DemandCheckType.CORPORATIZED_HOSPITAL;
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            IEnumerable<NsSktChungTu> listChungTu;
            listChungTu = _sktChungTuService
               .FindChungTuIndexByConditionBVTC(iLoai, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, _sessionService.Current.Principal, 0, "sp_skt_nhan_so_kiem_tra_1").ToList();

            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
            {
                listChungTu = listChungTu.Where(x => x.BKhoa == true).ToList();
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

        public void LoadMLSKT(params object[] args)
        {
            _authenticationInfo = _mapper.Map<AuthenticationInfo>(_sessionService.Current);
            var dataOld = _mlnsService.FindAllOld(_authenticationInfo);
            var dataNew = _mlnsService.FindAllNew(_authenticationInfo);
            SktMLSktModelItems = _mapper.Map<ObservableCollection<SktMucLucModel>>(dataOld);
            SktMLSktNextYearModelItems = _mapper.Map<ObservableCollection<SktMucLucModel>>(dataNew);
            foreach (var skt in SktMLSktNextYearModelItems)
            {
                skt.PropertyChanged += (sender, args) =>
                {
                    SktMucLucModel item = (SktMucLucModel)sender;
                    item.IsModified = true;
                    skt.IsModified = true;
                };
            }
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

        protected override void OnRefresh()
        {
            LoadSktChungTus();
            LoadMLSKT();
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
            InitBaseData();
            LoadMLSKT();
            LoadLockStatus();
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            LoadVoucherTypes();
            CheckDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
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

        protected void OnRevertMLSKT()
        {
            var result = MessageBox.Show(Resources.AdjustAll, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                _mlnsService.RevertAllMLSKT(_sessionService.Current.YearOfWork);
                OnRefresh();
            }
        }

        protected void OnAdjust()
        {
            try
            {
                var time = DateTime.Now;
                string msgConfirm = "Bạn chắc chắn muốn lưu thay đổi ?";
                MessageBoxResult dialogResult = MessageBox.Show(msgConfirm, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        var itemModified = SktMLSktNextYearModelItems.Where(i => i.IsModified);
                        var dataToSave = _mapper.Map<IEnumerable<NsSktMucLuc>>(itemModified);

                        foreach(var item in dataToSave)
                        {
                            _mlnsService.UpdateSKTML(item);
                        }
                        _mlnsService.UpdateNSMlsktMlnsMapping();

                        if (SelectedNsSktChungTuModel != null)
                        {
                            _mlnsService.UpdateSKTChungTuChiTiet(SelectedNsSktChungTuModel.Id);
                        }

                        OnRefresh();
                        MessageBoxHelper.Info("Lưu dữ liệu thành công");
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
