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
using VTS.QLNS.CTC.App.View.Budget.Estimate;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate
{
    public class DivisionIndexViewModel : GridViewModelBase<DtChungTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly INsDtChungTuService _estimationService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsDtNhanPhanBoMapService _dtChungTuMapService;
        private readonly INsDtChungTuCanCuService _dtChungTuCanCuService;
        private readonly IMucLucNganSachService _mucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private DivisionImportJson _importJsonView;
        private readonly IExportService _exportService;
        private ICollectionView _dtChungTuView;
        private SessionInfo _sessionInfo;

        public override string FuncCode => NSFunctionCode.BUDGET_ESTIMATE_RECEIVED_DIVISION;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Nhận phân bổ";
        public override string Description => "Danh sách chứng từ phân bổ từ cấp trên";
        public override Type ContentType => typeof(DivisionIndex);
        public override PackIconKind IconKind => PackIconKind.ArrowDownBoldHexagonOutline;

        public bool IsProcessing { get; set; } = false;

        public bool IsEdit => SelectedItem != null && !SelectedItem.BKhoa;
        //public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;
        //public bool IsEnableLock => SelectedItem != null;
        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }
        public bool IsEnableLock
        {
            get
            {
                List<DtChungTuModel> listItem = Items.Where(n => n.Selected).ToList();
                bool result = false;
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && listItem.Count > 0)
                {
                    result = true;
                }
                else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("0") && listItem.Count > 0)
                {
                    List<DtChungTuModel> lstSelectedKhoa = listItem.Where(x => x.BKhoa).ToList();
                    List<DtChungTuModel> lstSelectedMo = listItem.Where(x => !x.BKhoa).ToList();
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
        public bool IsEnableJson => false;

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

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
                _dtChungTuView?.Refresh();
                IsAllItemsSelected = false;
                if (!IsProcessing) LoadData();
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

        private bool _isAllItemsSelected;
        public bool IsAllItemsSelected
        {
            get
            {
                if (_cbxVoucherTypeSelected == null || Items == null || !Items.Any())
                {
                    return false;
                }

                IEnumerable<DtChungTuModel> listItemFilter = Items.Where(x => x.ILoaiChungTu.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem)));
                return listItemFilter.Any() && listItemFilter.All(item => item.Selected);
            }
            set
            {
                SetProperty(ref _isAllItemsSelected, value);
                if (Items != null && _cbxVoucherTypeSelected != null)
                {
                    Items.Where(x => x.ILoaiChungTu.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem))).ForAll(c => c.Selected = _isAllItemsSelected);
                }
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEnableLock));
        }

        public DivisionDialogViewModel DivisionDialogViewModel { get; set; }
        public DivisionDetailViewModel DivisionDetailViewModel { get; set; }
        public PrintReportReceiveDivisionViewModel PrintReportReceiveDivisionViewModel { get; set; }
        public PrintReportGeneralReceiveDivisionViewModel PrintReportGeneralReceiveDivisionViewModel { get; set; }
        public PrintReportPublicDivisionViewModel PrintReportPublicDivisionViewModel { get; set; }
        public PrintReportEstimateByReceiveDivisionViewModel PrintReportEstimateByReceiveDivisionViewModel { get; set; }
        public DivisionImportViewModel DivisionImportViewModel { get; set; }
        public DivisionImportJsonViewModel DivisionImportJsonViewModel { get; set; }
        public DivisionImportJson DivisionImportJson { get; set; }

        public DivisionImport _divisionImportView;

        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ShowDynamicColumnCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportJsonCommand { get; }
        public RelayCommand ImportJsonCommand { get; }

        public DivisionIndexViewModel(
            ILog logger,
            IMapper mapper,
            INsDtChungTuService estimationService,
            INsDtNhanPhanBoMapService dtChungTuMapService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            IMucLucNganSachService mucLucNganSachService,
            INsDtChungTuCanCuService dtChungTuCanCuService,
            INsDonViService donViService,
            DivisionDialogViewModel divisionDialogViewModel,
            DivisionDetailViewModel divisionDetailViewModel,
            PrintReportReceiveDivisionViewModel printReportReceiveDivisionViewModel,
            PrintReportGeneralReceiveDivisionViewModel printReportGeneralReceiveDivisionViewModel,
            PrintReportPublicDivisionViewModel printReportPublicDivisionViewModel,
            DivisionImportViewModel divisionImportViewModel,
            DivisionImportJsonViewModel divisionImportJsonViewModel,
            IExportService exportService,
            PrintReportEstimateByReceiveDivisionViewModel printReportEstimateByReceiveDivisionViewModel,
            ISessionService sessionService)
        {
            _mapper = mapper;
            _estimationService = (NsDtChungTuService)estimationService;
            _mucLucNganSachService = mucLucNganSachService;
            _sessionService = sessionService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _dtChungTuMapService = dtChungTuMapService;
            _dtChungTuCanCuService = dtChungTuCanCuService;
            _donViService = donViService;
            _exportService = exportService;

            DivisionDialogViewModel = divisionDialogViewModel;
            DivisionDetailViewModel = divisionDetailViewModel;
            DivisionImportJsonViewModel = divisionImportJsonViewModel;
            PrintReportReceiveDivisionViewModel = printReportReceiveDivisionViewModel;
            PrintReportGeneralReceiveDivisionViewModel = printReportGeneralReceiveDivisionViewModel;
            PrintReportPublicDivisionViewModel = printReportPublicDivisionViewModel;
            DivisionImportViewModel = divisionImportViewModel;
            PrintReportEstimateByReceiveDivisionViewModel = printReportEstimateByReceiveDivisionViewModel;
            DivisionDialogViewModel.ParentPage = this;

            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            ExportJsonCommand = new RelayCommand(obj => OnExportJson());
            ImportJsonCommand = new RelayCommand(obj => OnImportJson());

        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            IsProcessing = true;
            MarginRequirement = new System.Windows.Thickness(10);
            LoadLockStatus();
            LoadVoucherType();
            LoadData();
            IsProcessing = false;
            DivisionDetailViewModel.UpdateVoucherEvent += SelfRefresh;
        }

        private void LoadLockStatus()
        {
            List<ComboboxItem> lockStatus = new List<ComboboxItem>
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
            LoadLockStatus();
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                EstimationVoucherCriteria condition = new EstimationVoucherCriteria
                {
                    EstimationType = SoChungTuType.ReceiveEstimate,
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    BudgetSource = _sessionInfo.Budget,
                    Status = (int)Status.ACTIVE,
                    UserName = _sessionInfo.Principal,
                    VoucherType = CbxVoucherTypeSelected == null ? CbxVoucherType.Select(item => int.Parse(item.ValueItem)).FirstOrDefault() : int.Parse(CbxVoucherTypeSelected.ValueItem)
                };
                e.Result = _estimationService.FindByCondition(condition).ToList();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    // Process when run completed
                    Items = _mapper.Map<ObservableCollection<DtChungTuModel>>(e.Result);
                    _dtChungTuView = CollectionViewSource.GetDefaultView(Items);
                    _dtChungTuView.Filter = VoucherFilter;

                    if (Items != null && Items.Count > 0)
                    {
                        SelectedItem = Items.FirstOrDefault();
                    }

                    foreach (DtChungTuModel item in Items)
                    {
                        item.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(DtChungTuModel.Selected))
                            {
                                OnPropertyChanged(nameof(IsAllItemsSelected));
                                OnPropertyChanged(nameof(IsEnableLock));
                            }
                        };
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private bool VoucherFilter(object obj)
        {
            bool result = true;
            DtChungTuModel item = (DtChungTuModel)obj;

            if (CbxVoucherTypeSelected == null || LockStatusSelected == null)
                return true;

            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
            {
                return result && item.ILoaiChungTu.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem)) && item.BKhoa;
            }
            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
            {
                return result && item.ILoaiChungTu.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem)) && !item.BKhoa;
            }

            return result && item.ILoaiChungTu.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem));

        }

        private void LoadVoucherType()
        {
            List<ComboboxItem> cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key}
            };

            CbxVoucherType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            CbxVoucherTypeSelected = CbxVoucherType.ElementAt(0);
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            StringBuilder messageBuilder = new StringBuilder();
            //check quyền được chỉnh sửa
            if (SelectedItem.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBox.Show(string.Format(Resources.MsgRoleDelete, SelectedItem.SNguoiTao), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //kiểm tra chứng từ đã được phân bổ chưa, nếu đã được phân bổ thì không cho xóa
            List<NsDtNhanPhanBoMap> dtNhanPhanBoMaps = _dtChungTuMapService.FindByIdNhanDuToan(SelectedItem.Id).ToList();
            if (dtNhanPhanBoMaps.Count() > 0)
            {
                MessageBox.Show(Resources.AlertDeleteDivisionVoucher, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, SelectedItem.DNgayChungTu.HasValue ? SelectedItem.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty);
            MessageBoxResult result = MessageBox.Show(messageBuilder.ToString(), Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                DeleteSelectedVoucher();
        }

        private void DeleteSelectedVoucher()
        {
            _dtChungTuMapService.DeleteByIdNhanPhanBoDuToan(SelectedItem.Id);
            _dtChungTuChiTietService.DeleteByIdChungTu(SelectedItem.Id);
            _dtChungTuCanCuService.DeleteByIdChungTuDuToan(SelectedItem.Id);
            _estimationService.Delete(SelectedItem.Id);
            MessageBox.Show(Resources.MsgDeleteSuccess, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            OnRefresh();
        }

        protected override void OnLockUnLock()
        {
            if (IsLock)
            {
                //chỉ có đơn vị cha mới được mở khóa chứng từ
                string listItemKhoa = string.Join(", ", Items.Where(n => n.Selected && n.BKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    //MessageBox.Show(Resources.MsgRoleUnlock, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    MessageBox.Show(string.Format("Đồng chí không được mở khóa chứng từ {0} do không có quyền tổng hợp", listItemKhoa), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                //kiểm tra chứng từ đã được phân bổ chưa, nếu đã được phân bổ thì không cho mở khóa
                //List<NsDtNhanPhanBoMap> dtNhanPhanBoMaps = _dtChungTuMapService.FindByIdNhanDuToan(SelectedItem.Id).ToList();
                //if (dtNhanPhanBoMaps.Count() > 0)
                //{
                //    MessageBox.Show(Resources.AlertUnlockDivisionVoucher, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                //    return;
                //}
            }
            else
            {
                string listItemInvalid = string.Join(", ", Items.Where(n => n.Selected && !n.BKhoa && n.SNguoiTao != _sessionInfo.Principal).Select(n => n.SSoChungTu));
                if (!string.IsNullOrEmpty(listItemInvalid))
                {
                    //MessageBox.Show(string.Format(Resources.MsgRoleLock, SelectedItem.SNguoiTao), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    MessageBox.Show(string.Format("Đồng chí không có quyền khóa chứng từ {0} do không phải người tạo", listItemInvalid), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult result = MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            List<DtChungTuModel> lstChungTuChon = Items.Where(n => n.Selected).ToList();
            if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
            {
                lstChungTuChon = lstChungTuChon.Where(x => x.BKhoa).ToList();
            }
            else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
            {
                lstChungTuChon = lstChungTuChon.Where(x => !x.BKhoa).ToList();
            }


            if (result == MessageBoxResult.Yes)
            {
                foreach (DtChungTuModel SelectedItemElement in lstChungTuChon)
                {
                    int rs = _estimationService.LockOrUnLock(SelectedItemElement.Id, !SelectedItemElement.BKhoa);
                    if (rs == DBContextSaveChangeState.SUCCESS)
                    {
                        SelectedItemElement.BKhoa = !SelectedItemElement.BKhoa;
                        OnPropertyChanged(nameof(IsLock));
                        OnPropertyChanged(nameof(IsEdit));
                    }
                }
                OnPropertyChanged(nameof(LockStatusSelected));
                MessageBoxHelper.Info(msgDone);
                //LockStatusSelected = IsLock ? LockStatus.ElementAt(2) : LockStatus.ElementAt(1);
                LockStatusSelected = LockStatus.ElementAt(0);
                //_dtChungTuView.Refresh();
            }

        }

        protected override void OnAdd()
        {
            //check quyền được tạo mới
            List<DonVi> userAgency = _donViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
            if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
            {
                MessageBox.Show(Resources.MsgRoleAdd, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DivisionDialogViewModel.Model = new DtChungTuModel();
            DivisionDialogViewModel.Init();
            /*
            DivisionDialogViewModel.SavedAction = (obj) =>
            {
                if (obj.GetType().Equals(typeof(DtChungTuModel)))
                {
                    if (((DtChungTuModel)obj).ILoaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key)))
                    {
                        CbxVoucherTypeSelected = CbxVoucherType.ElementAt(0);
                    }
                    else
                    {
                        CbxVoucherTypeSelected = CbxVoucherType.ElementAt(1);
                    }
                    OnRefresh();
                    OnOpenDivisionDetail((DtChungTuModel)obj);
                }
            };
            */

            // Đoạn mã được viết gọn
            DivisionDialogViewModel.SavedAction = (obj) =>
            {
                if (obj is DtChungTuModel dtChungTu)
                {
                    CbxVoucherTypeSelected = dtChungTu.ILoaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key)) ? CbxVoucherType.ElementAt(0) : CbxVoucherType.ElementAt(1);
                    OnRefresh();
                    OnOpenDivisionDetail(dtChungTu);
                }
            };
            DivisionDialogViewModel.ShowDialogHost();
        }

        protected override void OnUpdate()
        {
            //check quyền được chỉnh sửa
            if (SelectedItem.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBox.Show(string.Format(Resources.MsgRoleUpdate, SelectedItem.SNguoiTao), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DivisionDialogViewModel.Model = SelectedItem;
            DivisionDialogViewModel.Init();
            DivisionDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            DivisionDialogViewModel.ShowDialogHost();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenDivisionDetail((DtChungTuModel)obj);
        }

        private void OnOpenDivisionDetail(DtChungTuModel SelectedItem)
        {
            DivisionDetailViewModel.Model = SelectedItem;
            if (CbxVoucherTypeSelected != null)
                DivisionDetailViewModel.LoaiChungTu = int.Parse(CbxVoucherTypeSelected.ValueItem);
            else
                DivisionDetailViewModel.LoaiChungTu = int.Parse(VoucherType.NSSD_Key);
            DivisionDetailViewModel.UpdateVoucherEvent += RefreshAfterSaveData;
            DivisionDetailViewModel.Init();
            DivisionDetail view = new DivisionDetail { DataContext = DivisionDetailViewModel };
            view.ShowDialog();
            DivisionDetailViewModel.UpdateVoucherEvent -= RefreshAfterSaveData;
        }

        private void RefreshAfterSaveData(object sender, EventArgs e)
        {
            DtChungTuModel model = (DtChungTuModel)sender;
            DtChungTuModel item = Items.Where(x => x.Id == model.Id).FirstOrDefault();
            if (item != null)
            {
                item.FTongTuChi = model.FTongTuChi;
                item.FTongHienVat = model.FTongHienVat;
                item.FTongHangMua = model.FTongHangMua;
                item.FTongHangNhap = model.FTongHangNhap;
                item.FTongDuPhong = model.FTongDuPhong;
                item.FTongPhanCap = model.FTongPhanCap;
                item.FTongTonKho = model.FTongTonKho;
                item.FTongDuToan = item.FTongTuChi + item.FTongHienVat + item.FTongHangNhap + item.FTongHangMua + item.FTongDuPhong + item.FTongPhanCap + item.FTongTonKho;
            }
            OnPropertyChanged(nameof(item.FTongDuToan));
            OnPropertyChanged(nameof(IsEdit));
        }

        /// <summary>
        /// open screen print
        /// </summary>
        /// <param name="param"></param>
        private void OpenPrintDialog(object param)
        {
            DtChungTuModel selectedByCheckbox = Items.FirstOrDefault(c => c.IsChecked);
            DivisionPrintType divisionPrintType = (DivisionPrintType)((int)param);
            object content;
            if (SelectedItem == null)
                return;
            switch (divisionPrintType)
            {
                case DivisionPrintType.BUDGET_PERIOD:
                case DivisionPrintType.BUDGET_ACCUMULATION:
                case DivisionPrintType.BUDGET_SPECIALIZED:
                    PrintReportReceiveDivisionViewModel.DivisionPrintType = divisionPrintType;
                    if (CbxVoucherTypeSelected != null)
                    {
                        PrintReportReceiveDivisionViewModel.LoaiChungTu = int.Parse(CbxVoucherTypeSelected.ValueItem);
                    }
                    else
                    {
                        PrintReportReceiveDivisionViewModel.LoaiChungTu = int.Parse(VoucherType.NSSD_Key);
                    }
                    //PrintReportReceiveDivisionViewModel.Init();
                    PrintReportReceiveDivisionViewModel.InitWithSelected(SelectedItem.Id);
                    content = new PrintReportReceiveDivision
                    {
                        DataContext = PrintReportReceiveDivisionViewModel
                    };
                    break;
                case DivisionPrintType.SYNTHESIS_BUDGET_SELF:
                case DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS:
                case DivisionPrintType.SYNTHESIS_BUDGET_COMMON:
                    PrintReportGeneralReceiveDivisionViewModel.DivisionPrintType = divisionPrintType;
                    if (CbxVoucherTypeSelected != null)
                    {
                        PrintReportGeneralReceiveDivisionViewModel.LoaiChungTu = int.Parse(CbxVoucherTypeSelected.ValueItem);
                    }
                    else
                    {
                        PrintReportGeneralReceiveDivisionViewModel.LoaiChungTu = int.Parse(VoucherType.NSSD_Key);
                    }
                    //PrintReportReceiveDivisionViewModel.Init();
                    PrintReportGeneralReceiveDivisionViewModel.InitWithSelected(SelectedItem.Id);
                    content = new PrintReportGeneralReceiveDivision
                    {
                        DataContext = PrintReportGeneralReceiveDivisionViewModel
                    };
                    break;
                case DivisionPrintType.ESTIMATE_BY_RECEIVE_DIVISION:
                    PrintReportEstimateByReceiveDivisionViewModel.Init();
                    content = new PrintReportEstimateByReceiveDivision
                    {
                        DataContext = PrintReportEstimateByReceiveDivisionViewModel
                    };
                    break;
                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, DivisionScreen.ROOT_DIALOG);
            }
        }

        private void OnImportData()
        {
            DivisionImportViewModel.SavedAction = obj =>
            {
                if (obj.GetType().Equals(typeof(DtChungTuModel)))
                {
                    _divisionImportView.Close();
                    if (obj.ILoaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key)))
                    {
                        CbxVoucherTypeSelected = CbxVoucherType.ElementAt(0);
                    }
                    else
                    {
                        CbxVoucherTypeSelected = CbxVoucherType.ElementAt(1);
                    }
                    OnRefresh();
                    OnOpenDivisionDetail(obj);
                }
            };
            DivisionImportViewModel.Init();
            _divisionImportView = new DivisionImport { DataContext = DivisionImportViewModel };
            _divisionImportView.ShowDialog();
        }

        #region Json
        private void OnImportJson()
        {
            DivisionImportJsonViewModel.Init();
            DivisionImportJsonViewModel.SavedAction = obj =>
            {
                OnRefresh();
                _importJsonView.Close();
            };
            _importJsonView = new DivisionImportJson { DataContext = DivisionImportJsonViewModel };
            _importJsonView.Show();
        }

        private void OnExportJson()
        {
            if (!Items.Any(n => n.Selected))
            {
                MessageBoxHelper.Error(Resources.MsgRecordEmpty);
                return;
            }
            List<NsDtChungTu> lstData = GetDataExportJson();
            _exportService.OpenJson(lstData);
        }

        private List<NsDtChungTu> GetDataExportJson()
        {
            List<NsDtChungTu> lstData = new List<NsDtChungTu>();

            if (!Items.Any(n => n.Selected)) return lstData;
            return _mapper.Map<ObservableCollection<NsDtChungTu>>(Items.Where(n => n.Selected)).Select(n =>
            {
                n.ListDetailChiTiet = _dtChungTuChiTietService.FindByIdChungTu(n.Id.ToString()).ToList();
                n.ListDetailCanCu = _dtChungTuCanCuService.FindAll(m => m.Id.Equals(n.Id)).ToList();
                return n;
            }).ToList();
        }
        #endregion

        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }
    }
}
