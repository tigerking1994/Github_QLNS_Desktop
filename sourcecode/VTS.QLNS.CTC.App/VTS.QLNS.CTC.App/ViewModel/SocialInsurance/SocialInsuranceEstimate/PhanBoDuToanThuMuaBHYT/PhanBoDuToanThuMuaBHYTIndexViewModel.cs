using AutoMapper;
using log4net;
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
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThuMuaBHYT;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThuMuaBHYT.ExportReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThuMuaBHYT.ExportReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThuMuaBHYT
{
    public class PhanBoDuToanThuMuaBHYTIndexViewModel : GridViewModelBase<BhPbdttmBHYTModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IPbdttmBHYTService _pdttmBHYTService;
        private readonly IPbdttmBHYTChiTietService _pbdttmBHYTChiTietService;
        private readonly IPbdttmMapBHYTService _pbdttmMapBHYTService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDtTmBHYTTNService _bhDtTmBHYTTNService;
        private readonly IExportService _exportService;
        private readonly INsDonViService _donViService;
        private readonly IDanhMucService _danhMucService;
        private readonly IVdtFtpRootService _ftpService;
        private readonly FtpStorageService _ftpStorageService;
        private ICollectionView _dtChungTuView;
        private List<BhPbdtcBHXHChiTietModel> _listChungTuChiTiet;
        private SessionInfo _sessionInfo;
        public override string FuncCode => NSFunctionCode.BUDGET_ESTIMATE_DIVISION;
        public override string GroupName => MenuItemContants.GROUP_THU;
        public override string Name => "Phân bổ DT thu BHYT thân nhân";
        public override string Description => "Danh sách đợt phân bổ dự toán thu BHYT thân nhân";
        public override Type ContentType => typeof(PhanBoDuToanThuMuaBHYTIndex);
        public override PackIconKind IconKind => PackIconKind.AxisArrow;

        public bool IsEdit => SelectedItem != null && !SelectedItem.BIsKhoa;
        //public bool IsLock => SelectedItem != null && SelectedItem.bIsKhoa;
        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }
        public bool IsEnableLockMultiple => Items.All(x => !x.Selected) ? false : Items.Any(x => x.Selected && !x.BIsKhoa);
        public bool IsEnableUnlockMultiple => Items.All(x => !x.Selected) ? false : Items.Any(x => x.Selected && x.BIsKhoa);
        //public bool IsEnableLock => SelectedItem != null;
        public bool IsEnableLock
        {
            get
            {
                var listItemSelected = Items.Where(n => n.Selected).ToList();
                var result = false;
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && listItemSelected.Count > 0)
                {
                    result = true;
                }
                else
                {
                    var lstSelectedKhoa = listItemSelected.Where(x => x.BIsKhoa).ToList();
                    var lstSelectedMo = listItemSelected.Where(x => !x.BIsKhoa).ToList();
                    if (lstSelectedKhoa.Count() > 0 && lstSelectedMo.Count() > 0)
                    {
                        result = false;
                    }
                    else if (lstSelectedKhoa.Count() > 0)
                    {
                        IsLock = true;
                        result = true;
                    }
                    else if (lstSelectedMo.Count() > 0)
                    {
                        IsLock = false;
                        result = true;
                    }
                }
                return result;
            }
        }
        public bool HasParentAgency { get; set; }

        public Dictionary<string, string> DictIdChungTu = new Dictionary<string, string>();

        private ObservableCollection<ComboboxItem> _cbxVoucherType;
        public ObservableCollection<ComboboxItem> CbxVoucherType
        {
            get => _cbxVoucherType;
            set => SetProperty(ref _cbxVoucherType, value);
        }

        private ComboboxItem _cbxVoucherTypeSelected;
        public ComboboxItem CbxVoucherTypeSelected
        {
            get => _cbxVoucherTypeSelected;
            set
            {
                SetProperty(ref _cbxVoucherTypeSelected, value);
                if (_dtChungTuView != null)
                {
                    _dtChungTuView.Refresh();
                }

                IsAllItemsSelected = false;
                OnPropertyChanged(nameof(IsAllItemsSelected));
                if (_cbxVoucherTypeSelected != null)
                {
                    //SelectedItem = Items.FirstOrDefault(x => x.ILoaiChungTu.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem)));
                }
                LoadData();
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
                OnPropertyChanged(nameof(IsEnableLock));
                if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
                {
                    IsLock = true;
                }
                else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                {
                    IsLock = false;
                }
                if (_dtChungTuView != null)
                {
                    _dtChungTuView.Refresh();
                }
            }
        }

        public bool IsExportGridData
        {
            get
            {
                return Items.Any(item => item.Selected);
            }
        }

        private bool _isAllItemsSelected;
        public bool IsAllItemsSelected
        {
            get
            {
                if (_cbxVoucherTypeSelected == null || Items == null || !Items.Any())
                {
                    return false;
                }

                var listItemFilter = Items.Where(x => x.ILoaiDuToan.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem)));
                return !listItemFilter.Any() ? false : listItemFilter.All(item => item.Selected);
            }
            set
            {
                SetProperty(ref _isAllItemsSelected, value);
                if (Items != null && _cbxVoucherTypeSelected != null)
                {
                    Items.Where(x => x.ILoaiDuToan.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem))).ForAll(c => c.Selected = _isAllItemsSelected);
                }
            }
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private BhPbdttmBHYTModel _selectedBhPhanBoDuToanTMModel;
        public BhPbdttmBHYTModel SelectedBhPhanBoDuToanTMModel
        {
            get => _selectedBhPhanBoDuToanTMModel;
            set
            {
                SetProperty(ref _selectedBhPhanBoDuToanTMModel, value);
            }
        }

        public PhanBoDuToanThuMuaBHYTDialogViewModel PhanBoDuToanThuMuaBHYTDialogViewModel { get; set; }
        public PhanBoDuToanThuMuaBHYTDetailViewModel PhanBoDuToanThuMuaBHYTDetailViewModel { get; set; }
        public ExportPhanBoDuToanThuMuaBHYTViewModel ExportPhanBoDuToanThuMuaBHYTViewModel { get; set; }
        public TongHopThuChiViewModel TongHopThuChiViewModel { get; set; }
        public PrintPhuLucDuToanThuMuaBHYTViewModel PrintPhuLucDuToanThuMuaBHYTViewModel { get; set; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintDTTMCommand { get; }
        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand ExportGridDataCommand { get; }
        public PhanBoDuToanThuMuaBHYTIndexViewModel(
            IExportService exportService,
            INsDonViService donViService,
            IPbdttmBHYTService pdttmBHYTService,
            IPbdttmBHYTChiTietService pbdttmBHYTChiTietService,
            IPbdttmMapBHYTService pbdttmMapBHYTService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDtTmBHYTTNService bhDtTmBHYTTNService,
            ILog logger,
            IMapper mapper,
            PhanBoDuToanThuMuaBHYTDialogViewModel phanBoDuToanThuMuaBHYTDialogViewModel,
            PhanBoDuToanThuMuaBHYTDetailViewModel phanBoDuToanThuMuaBHYTDetailViewModel,
            ExportPhanBoDuToanThuMuaBHYTViewModel exportPhanBoDuToanThuMuaBHYTViewModel,
            TongHopThuChiViewModel tongHopThuChiViewModel,
            PrintPhuLucDuToanThuMuaBHYTViewModel printPhuLucDuToanThuMuaBHYTViewModel,
            ISessionService sessionService,
            IDanhMucService danhMucService)
        {
            _logger = logger;
            _mapper = mapper;
            _exportService = exportService;
            _sessionService = sessionService;
            _donViService = donViService;
            _pdttmBHYTService = pdttmBHYTService;
            _pbdttmBHYTChiTietService = pbdttmBHYTChiTietService;
            _pbdttmMapBHYTService = pbdttmMapBHYTService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDtTmBHYTTNService = bhDtTmBHYTTNService;
            _danhMucService = danhMucService;

            PhanBoDuToanThuMuaBHYTDialogViewModel = phanBoDuToanThuMuaBHYTDialogViewModel;
            PhanBoDuToanThuMuaBHYTDetailViewModel = phanBoDuToanThuMuaBHYTDetailViewModel;
            ExportPhanBoDuToanThuMuaBHYTViewModel = exportPhanBoDuToanThuMuaBHYTViewModel;
            TongHopThuChiViewModel = tongHopThuChiViewModel;
            PrintPhuLucDuToanThuMuaBHYTViewModel = printPhuLucDuToanThuMuaBHYTViewModel;
            PrintCommand = new RelayCommand(obj => OnPrint(obj));
            PrintDTTMCommand = new RelayCommand(OnPrintDTTM);
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            ExportGridDataCommand = new RelayCommand(obj => OnExportData());
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

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            LoadLockStatus();
            LoadVoucherType();
            LoadData();
        }

        private void LoadVoucherType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key}
            };

            CbxVoucherType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            CbxVoucherTypeSelected = CbxVoucherType.ElementAt(0);
        }

        private void LoadData()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<BhPbdttmBHYT>();
            predicate = predicate.And(x => x.INamLamViec == yearOfWork);
            var listPhanbo = _bhDtTmBHYTTNService.FindByCondition(_sessionInfo.YearOfWork).OrderBy(x => x.SSoChungTu);
            var listChungTu = _pdttmBHYTService.FindByCondition(predicate).OrderBy(x => x.SSoChungTu).ToList();
            Items = _mapper.Map<ObservableCollection<BhPbdttmBHYTModel>>(listChungTu);
            //Items.ForAll(x => x.ListSoChungTuDotNhan = string.Join(",", x.IIdDotNhan.Split(",").Select(e => DictIdChungTu.GetValueOrDefault(e, string.Empty))));

            _dtChungTuView = CollectionViewSource.GetDefaultView(Items);
            _dtChungTuView.Filter = VoucherFilter;

            foreach (var item in Items)
            {
                var listDotNhan = item.SDS_DotNhan.Split(",");
                item.SDS_TenDotNhan = String.Join(", " ,listPhanbo.Where(x => listDotNhan.Contains(x.Id.ToString())).Select(x => x.SSoChungTu).ToList());
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(BhPbdtcBHXHModel.Selected))
                    {
                        OnPropertyChanged(nameof(IsExportGridData));
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                        OnPropertyChanged(nameof(IsEnableLock));
                        OnPropertyChanged(nameof(IsEnableLockMultiple));
                        OnPropertyChanged(nameof(IsEnableUnlockMultiple));
                    }
                };
            }
        }

        protected override void OnDelete()
        {
            base.OnDelete();

            if (SelectedItem.SNguoiTao != _sessionService.Current.Principal)
            {
                System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleDelete, SelectedItem.SNguoiTao), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, SelectedItem.DNgayChungTu.Value.ToString("dd/MM/yyyy"));
            MessageBoxResult result = MessageBox.Show(messageBuilder.ToString(), Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                DeleteSelectedVoucher();
        }

        private void DeleteSelectedVoucher()
        {
            BhPbdttmBHYT chungtu = new BhPbdttmBHYT();
            _mapper.Map(SelectedBhPhanBoDuToanTMModel, chungtu);


            //Xóa chứng từ Map
            List<BhPbdttmMapBHYT> chungtuMaps = new List<BhPbdttmMapBHYT>();
            var predicate_map = PredicateBuilder.True<BhPbdttmMapBHYT>();
            predicate_map = predicate_map.And(x => x.IID_DTTM_BHYT_PhanBo == chungtu.Id);
            chungtuMaps = _pbdttmMapBHYTService.FindByCondition(predicate_map).ToList();
            _pbdttmMapBHYTService.RemoveRange(chungtuMaps);

            //Xóa chứng từ chi tiét
            List<BhPbdttmBHYTChiTiet> chungtuChiTiets = new List<BhPbdttmBHYTChiTiet>();
            var predicate_chitiet = PredicateBuilder.True<BhPbdttmBHYTChiTiet>();
            predicate_chitiet = predicate_chitiet.And(x => x.IID_DTTM_BHYT_ThanNhan_PhanBo == chungtu.Id);
            chungtuChiTiets = _pbdttmBHYTChiTietService.FindByCondition(predicate_chitiet).ToList();
            _pbdttmBHYTChiTietService.RemoveRange(chungtuChiTiets);

            //Xóa chứng từ 
            _pdttmBHYTService.Delete(chungtu);


            MessageBox.Show(Resources.MsgDeleteSuccess, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            OnRefresh();
        }

        private void OnSelectedChange(object obj)
        {
            SelectedBhPhanBoDuToanTMModel = (BhPbdttmBHYTModel)obj;

        }

        protected override void OnLockUnLock()
        {
            if (IsLock)
            {
                var listSoChungTu = string.Join(", ", Items.Where(n => n.Selected && n.BIsKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBox.Show(string.Format("Đồng chí không được mở khóa chứng từ {0} do không có quyền tổng hợp", listSoChungTu), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                var listSoChungTuInvalid = string.Join(", ", Items.Where(n => n.Selected && n.SNguoiTao != _sessionInfo.Principal && !n.BIsKhoa).Select(n => n.SSoChungTu));

                if (!string.IsNullOrEmpty(listSoChungTuInvalid))
                {
                    MessageBox.Show(string.Format("Đồng chí không có quyền kháo chứng từ {0} do không phải người tạo", listSoChungTuInvalid), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult result = MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);

            var lstChungTuChon = Items.Where(n => n.Selected).ToList();
            if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
            {
                lstChungTuChon = lstChungTuChon.Where(x => x.BIsKhoa).ToList();
            }
            else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
            {
                lstChungTuChon = lstChungTuChon.Where(x => !x.BIsKhoa).ToList();
            }

            if (result == MessageBoxResult.Yes)
            {
                foreach (var SelectedItemElement in lstChungTuChon)
                {
                    var rs = _pdttmBHYTService.LockOrUnLock(SelectedItemElement.Id, !SelectedItemElement.BIsKhoa);
                    if (rs == DBContextSaveChangeState.SUCCESS)
                    {
                        SelectedItemElement.BIsKhoa = !SelectedItemElement.BIsKhoa;
                        OnPropertyChanged(nameof(IsLock));
                        OnPropertyChanged(nameof(IsEdit));
                    }
                }

                MessageBoxHelper.Info(msgDone);
                LockStatusSelected = LockStatus.ElementAt(0);
            }
        }


        private bool VoucherFilter(object obj)
        {
            bool result = true;
            var item = (BhPbdttmBHYTModel)obj;


            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
            {
                result = result && item.BIsKhoa;
            }

            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
            {
                result = result && !item.BIsKhoa;
            }


            return result;
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            //OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEnableLock));
        }

        protected override void OnAdd()
        {
            PhanBoDuToanThuMuaBHYTDialogViewModel.Name = "Thêm mới chứng từ";
            PhanBoDuToanThuMuaBHYTDialogViewModel.Description = "Thêm mới chứng từ phân bổ thu mua BHYT";
            //PhanBoDuToanChiDialogViewModel.IsEdit = false;
            PhanBoDuToanThuMuaBHYTDialogViewModel.Model = new BhPbdttmBHYTModel();
            PhanBoDuToanThuMuaBHYTDialogViewModel.Init();
            PhanBoDuToanThuMuaBHYTDialogViewModel.SavedAction = obj =>
            {
                var pbdtcChungTu = (BhPbdttmBHYTModel)obj;
                this.LoadData();
                OpenDetailDialog(pbdtcChungTu);
            };
            var exportView = new PhanBoDuToanThuMuaBHYTDialog() { DataContext = PhanBoDuToanThuMuaBHYTDialogViewModel };
            DialogHost.Show(exportView, DemandCheckScreen.ROOT_DIALOG);
        }
        private void OpenDetailDialog(BhPbdttmBHYTModel bhPbdttmBHYTModel, params bool[] isNew)
        {
            PhanBoDuToanThuMuaBHYTDetailViewModel.Model = bhPbdttmBHYTModel;
            PhanBoDuToanThuMuaBHYTDetailViewModel.UpdateParentWindowEventHandler += RefreshAfterSaveData;
            PhanBoDuToanThuMuaBHYTDetailViewModel.Init();
            PhanBoDuToanThuMuaBHYTDetailViewModel.SavedAction = obj =>
            {
                OnRefresh();
            };
            var view = new PhanBoDuToanThuMuaBHYTDetail() { DataContext = PhanBoDuToanThuMuaBHYTDetailViewModel };
            view.ShowDialog();
        }

        private void RefreshAfterSaveData(object sender, EventArgs e)
        {
            OnRefresh();
        }

        protected override void OnUpdate()
        {
            //check quyền được chỉnh sửa
            if (SelectedItem.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBox.Show(string.Format(Resources.MsgRoleUpdate, SelectedItem.SNguoiTao), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            PhanBoDuToanThuMuaBHYTDialogViewModel.Name = "Cập nhập chứng từ";
            PhanBoDuToanThuMuaBHYTDialogViewModel.Description = "Cập nhập chứng từ phân bổ thu mua BHYT";
            PhanBoDuToanThuMuaBHYTDialogViewModel.Model = SelectedItem;
            PhanBoDuToanThuMuaBHYTDialogViewModel.RefreshVoucherEvent += (object sender, EventArgs e) =>
            {
                OnRefresh();
            };
            PhanBoDuToanThuMuaBHYTDialogViewModel.Init();
            PhanBoDuToanThuMuaBHYTDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            var view = new PhanBoDuToanThuMuaBHYTDialog { DataContext = PhanBoDuToanThuMuaBHYTDialogViewModel };

            DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
            PhanBoDuToanThuMuaBHYTDialogViewModel.RefreshVoucherEvent -= (object sender, EventArgs e) =>
            {
                OnRefresh();
            };
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhPbdttmBHYTModel)obj);
        }

        private void OnExportData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ExportResult> results = new List<ExportResult>();
                    FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                    string templateFileName = "rpt_BH_PhanBoDuToanThuMuaBHYT.xlsx";
                    var namLamViec = _sessionService.Current.YearOfWork;
                    var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                    predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == namLamViec);
                    var listMucLucNganSach = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => x.SLNS.StartsWith("903")).OrderBy(x => x.SXauNoiMa).ToList();
                    var lstDonVi = _donViService.FindAll().Where(x => x.NamLamViec == namLamViec).ToList();
                    var itemsExport = Items.Where(x => x.Selected);
                    var dictDonVi = _donViService.FindByListIdDonVi(string.Join(",", itemsExport.Select(x => x.SDS_IDMaDonVi)), namLamViec)
                       .GroupBy(x => x.IIDMaDonVi)
                       .ToDictionary(x => x.Key, x => x.First());

                    foreach (var item in itemsExport)
                    {
                        var dataExportDetail = LoadDataExportDetail(item);
                        List<string> lstDonViPhanBo = item.SDS_IDMaDonVi.Split(",").ToList();
                        foreach (var dv in lstDonViPhanBo)
                        {
                            var donvi = lstDonVi.Where(x => x.IIDMaDonVi == dv).FirstOrDefault();
                            var lstDataPrent = dataExportDetail.Where(x => x.BHangCha).ToList();
                            var lstDataChildbyDonVi = dataExportDetail.Where(x => !x.BHangCha && x.IID_MaDonVi == dv).ToList();

                            lstDataPrent.AddRange(lstDataChildbyDonVi);
                            List<BhPbdttmBHYTChiTietModel> lstData = new List<BhPbdttmBHYTChiTietModel>();
                            lstData = _mapper.Map(lstDataPrent, lstData);
                            CalculateData(lstData);
                            lstData = lstData.Where(x => x.FDuToan > 0).OrderBy(x => x.SXauNoiMa).ToList();
                            if (lstData.Any())
                                lstData.RemoveAt(0);
                            var data = new Dictionary<string, object>();
                            data.Add("FormatNumber", formatNumber);
                            data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                            data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                            data.Add("TitleFirst", $"DỰ TOÁN CHI NGÂN SÁCH NĂM {_sessionService.Current.YearOfWork}");
                            data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoQuyetDinh}, ngày: {DateUtils.Format(item.DNgayQuyetDinh)})");
                            data.Add("HeaderTenDonVi", $"Đơn vị: {donvi?.IIDMaDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{donvi?.TenDonVi}");
                            data.Add("TenDonVi", $"{donvi?.IIDMaDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{donvi?.TenDonVi}");
                            data.Add("SoChungTu", item.SSoChungTu);
                            data.Add("NgayChungTu", DateUtils.Format(item.DNgayChungTu));
                            data.Add("SoQuyetDinh", item.SSoQuyetDinh);
                            data.Add("NgayQuyetDinh", DateUtils.Format(item.DNgayQuyetDinh));
                            data.Add("MoTa", item.SMoTa);
                            data.Add("LoaiDuToan", VoucherType.BudgetTypeDict.GetValueOrDefault(item.ILoaiDuToan, string.Empty));
                            data.Add("NguoiTao", item.SNguoiTao);
                            data.Add("NgayTao", DateUtils.Format(item.DNgayTao));
                            data.Add("Items", lstData);
                            data.Add("MLNS", listMucLucNganSach);
                            double? TotalDuToan = lstData?.Where(x => !x.BHangCha).Sum(x => x.FDuToan);
                            data.Add("TotalDuToan", TotalDuToan);
                            List<int> hideColumns = new List<int>();
                            var xlsFile = _exportService.Export<BhPbdttmBHYTChiTietModel, BhDmMucLucNganSach>(templateFileName, data, hideColumns);
                            var nameRange = xlsFile.GetNamedRange(1);
                            nameRange.Comment = "Workbook";
                            xlsFile.SetNamedRange(nameRange);
                            xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                            xlsFile.SetCellValue(50, 50, "CheckSum");
                            xlsFile.SetRowHidden(50, true);
                            string fileNamePrefix = string.Format("{0}_{1}_{2}", item.SSoChungTu, item.SSoQuyetDinh, StringUtils.ConvertVN(donvi?.TenDonVi));
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
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

        private void OnPrint(object param)
        {
            var divisionPrintType = (SocialInsuranceDivisionEstimatePrintType)((int)param);
            TongHopThuChiViewModel.ReportNameTypeValue = (int)divisionPrintType;
            TongHopThuChiViewModel.ReportTypeValue = divisionPrintType;
            TongHopThuChiViewModel.Init();
            var view = new TongHopThuChi
            {
                DataContext = TongHopThuChiViewModel
            };
            DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
        }

        private void OnPrintDTTM(object param)
        {
            PrintPhuLucDuToanThuMuaBHYTViewModel.Init();
            var view = new PrintPhuLucDuToanThuMuaBHYT
            {
                DataContext = PrintPhuLucDuToanThuMuaBHYTViewModel
            };
            DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
        }

        private List<BhPbdttmBHYTChiTietQuery> LoadDataExportDetail(BhPbdttmBHYTModel item)
        {
            var lstChungTuChiTiet = _pbdttmBHYTChiTietService.ExportExcelPhanBoDuToanChi(item.Id, item.SDSLNS, item.INamLamViec).ToList();
            return lstChungTuChiTiet;
        }

        private void CalculateData(List<BhPbdttmBHYTChiTietModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FDuToan = 0;
                    return x;
                }).ToList();

            foreach (var item in listData.Where(x => !x.BHangCha && (x.FDuToan != 0)))
            {
                CalculateParent(listData, item, item);
            }
        }

        private void CalculateParent(List<BhPbdttmBHYTChiTietModel> listData, BhPbdttmBHYTChiTietModel currentItem, BhPbdttmBHYTChiTietModel seftItem)
        {
            var parrentItem = listData.FirstOrDefault(x => x.IID_MLNS == currentItem.IID_MLNS_Cha);
            if (parrentItem == null) return;
            parrentItem.FDuToan = (parrentItem.FDuToan ?? 0) + (seftItem.FDuToan ?? 0);

            CalculateParent(listData, parrentItem, seftItem);
        }
    }
}
