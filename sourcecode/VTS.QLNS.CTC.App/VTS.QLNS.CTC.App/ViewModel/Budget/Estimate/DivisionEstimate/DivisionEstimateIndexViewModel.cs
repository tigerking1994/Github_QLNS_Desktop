using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
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
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.Estimate.DivisionEstimate;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate.ExportReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate.SendDataDivisionEstimate;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;


namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate
{
    public class DivisionEstimateIndexViewModel : GridViewModelBase<DtChungTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly INsDtChungTuService _estimationService;
        private readonly ISessionService _sessionService;
        private readonly INsDtNhanPhanBoMapService _dtChungTuMapService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly IExportService _exportService;
        private readonly INsDonViService _donViService;
        private readonly IDanhMucService _danhMucService;
        private readonly IVdtFtpRootService _ftpService;
        private readonly FtpStorageService _ftpStorageService;
        private IHTTPUploadFileService _hTTPUploadFileService;
        private DivisionEstimateImportJson _importJsonView;
        private ICollectionView _dtChungTuView;
        private List<DtChungTuChiTietModel> _listChungTuChiTiet;
        private SessionInfo _sessionInfo;
        private SendDataDivisionEstimateViewModel SendDataDivisionEstimateViewModel;


        public override string FuncCode => NSFunctionCode.BUDGET_ESTIMATE_DIVISION;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Phân bổ dự toán";
        public override string Description => "Danh sách chứng từ phân cấp theo đơn vị";
        public override Type ContentType => typeof(View.Budget.Estimate.DivisionEstimateIndex);
        public override PackIconKind IconKind => PackIconKind.AxisArrow;

        public bool IsEdit => SelectedItem != null && !SelectedItem.BKhoa;
        //public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;
        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }
        public bool IsEnableLockMultiple => Items.All(x => !x.Selected) ? false : Items.Any(x => x.Selected && !x.BKhoa);
        public bool IsEnableUnlockMultiple => Items.All(x => !x.Selected) ? false : Items.Any(x => x.Selected && x.BKhoa);
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
                    var lstSelectedKhoa = listItemSelected.Where(x => x.BKhoa).ToList();
                    var lstSelectedMo = listItemSelected.Where(x => !x.BKhoa).ToList();
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
                    SelectedItem = Items.FirstOrDefault(x => x.ILoaiChungTu.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem)));
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
                if (_cbxVoucherTypeSelected == null || Items == null || !Items.Any())
                {
                    return false;
                }
                return Items.Where(x => x.ILoaiChungTu.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem))).Any(item => item.Selected);
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

                var listItemFilter = Items.Where(x => x.ILoaiChungTu.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem)));
                return !listItemFilter.Any() ? false : listItemFilter.All(item => item.Selected);
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

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            //OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEnableLock));
        }

        public DivisionEstimateDialogViewModel DivisionEstimateDialogViewModel { get; set; }
        public DivisionEstimateDetailViewModel DivisionEstimateDetailViewModel { get; set; }
        public PrintReportCoverSheetViewModel PrintReportCoverSheetViewModel { get; set; }
        public PrintReportTargetAgencyViewModel PrintReportTargetAgencyViewModel { get; set; }
        public PrintReportTargetAgencySummaryViewModel PrintReportTargetAgencySummaryViewModel { get; set; }
        public PrintReportTargetMajorsViewModel PrintReportTargetMajorsViewModel { get; set; }
        public PrintReportSynthesisAgencyViewModel PrintReportSynthesisAgencyViewModel { get; set; }
        public PrintReportSynthesisDivisionViewModel PrintReportSynthesisDivisionViewModel { get; set; }
        public PrintReportTargetAgencyLnsViewModel PrintReportTargetAgencyLnsViewModel { get; set; }
        public PrintReportTargetMajorsDayViewModel PrintReportTargetMajorsDayViewModel { get; set; }
        public PrintReportTargetMajorAgencyViewModel PrintReportTargetMajorAgencyViewModel { get; set; }
        public DivisionEstimateImportJsonViewModel DivisionEstimateImportJsonViewModel { get; set; }
        public PrintReportPublicFinanceViewModel PrintReportPublicFinanceViewModel { get; set; }
        public PrintReportPublicDivisionViewModel PrintReportPublicDivisionViewModel { get; set; }
        public ExportDivisionEstimateViewModel ExportDivisionEstimateViewModel { get; set; }
        public ExportJsonDivisionEstimateViewModel ExportJsonDivisionEstimateViewModel { get; set; }
        public PrintReportDivisionPlanViewModel PrintReportDivisionPlanViewModel { get; set; }
        public PrintReportTargetAgencyHD4554ViewModel PrintReportTargetAgencyHD4554ViewModel { get; set; }

        public RelayCommand ExportGridDataCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand UploadFileCommandHTTP { get; }
        public RelayCommand UploadFileCommandFTP { get; }
        public RelayCommand ExportJsonCommand { get; }
        public RelayCommand ImportJsonCommand { get; }


        public DivisionEstimateIndexViewModel(INsDtChungTuService estimationService,
            INsDtNhanPhanBoMapService dtChungTuMapService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            IExportService exportService,
            INsMucLucNganSachService nsMucLucNganSachService,
            INsDonViService donViService,
            ILog logger,
            IMapper mapper,
            IDanhMucService danhMucService,
            IVdtFtpRootService ftpService,
            FtpStorageService ftpStorageService,
            SendDataDivisionEstimateViewModel sendDataDivisionEstimateViewModel,
            DivisionEstimateDialogViewModel divisionEstimateDialogViewModel,
            DivisionEstimateDetailViewModel divisionEstimateDetailViewModel,
            PrintReportCoverSheetViewModel printReportCoverSheetViewModel,
            PrintReportTargetAgencyViewModel printReportTargetAgencyViewModel,
            PrintReportTargetAgencySummaryViewModel printReportTargetAgencySummaryViewModel,
            PrintReportTargetMajorsViewModel printReportTargetMajorsViewModel,
            PrintReportSynthesisAgencyViewModel printReportSynthesisAgencyViewModel,
            PrintReportSynthesisDivisionViewModel printReportSynthesisDivisionViewModel,
            PrintReportTargetAgencyLnsViewModel printReportTargetAgencyLnsViewModel,
            PrintReportTargetMajorsDayViewModel printReportTargetMajorsDayViewModel,
            DivisionEstimateImportJsonViewModel divisionEstimateImportJsonViewModel,
            PrintReportTargetMajorAgencyViewModel printReportTargetMajorAgencyViewModel,
            PrintReportPublicFinanceViewModel printReportPublicFinanceViewModel,
            PrintReportPublicDivisionViewModel printReportPublicDivisionViewModel,
            ExportDivisionEstimateViewModel exportDivisionEstimateViewModel,
            ExportJsonDivisionEstimateViewModel exportJsonDivisionEstimateViewModel,
            PrintReportDivisionPlanViewModel printReportDivisionPlanViewModel,
            PrintReportTargetAgencyHD4554ViewModel printReportTargetAgencyHD4554ViewModel,
            ISessionService sessionService,
            IHTTPUploadFileService hTTPUploadFileService)
        {
            _logger = logger;
            _mapper = mapper;
            _estimationService = (NsDtChungTuService)estimationService;
            _dtChungTuMapService = dtChungTuMapService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _exportService = exportService;
            _sessionService = sessionService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _donViService = donViService;
            _danhMucService = danhMucService;
            _ftpService = ftpService;
            _ftpStorageService = ftpStorageService;
            _hTTPUploadFileService = hTTPUploadFileService;

            SendDataDivisionEstimateViewModel = sendDataDivisionEstimateViewModel;
            DivisionEstimateDialogViewModel = divisionEstimateDialogViewModel;
            DivisionEstimateDetailViewModel = divisionEstimateDetailViewModel;
            PrintReportCoverSheetViewModel = printReportCoverSheetViewModel;
            PrintReportTargetAgencyViewModel = printReportTargetAgencyViewModel;
            PrintReportTargetAgencySummaryViewModel = printReportTargetAgencySummaryViewModel;
            PrintReportTargetMajorsViewModel = printReportTargetMajorsViewModel;
            PrintReportSynthesisAgencyViewModel = printReportSynthesisAgencyViewModel;
            PrintReportSynthesisDivisionViewModel = printReportSynthesisDivisionViewModel;
            PrintReportTargetAgencyLnsViewModel = printReportTargetAgencyLnsViewModel;
            PrintReportTargetMajorsDayViewModel = printReportTargetMajorsDayViewModel;
            PrintReportTargetMajorAgencyViewModel = printReportTargetMajorAgencyViewModel;
            PrintReportPublicFinanceViewModel = printReportPublicFinanceViewModel;
            DivisionEstimateImportJsonViewModel = divisionEstimateImportJsonViewModel;
            PrintReportPublicDivisionViewModel = printReportPublicDivisionViewModel;
            PrintReportDivisionPlanViewModel = printReportDivisionPlanViewModel;
            ExportDivisionEstimateViewModel = exportDivisionEstimateViewModel;
            ExportJsonDivisionEstimateViewModel = exportJsonDivisionEstimateViewModel;
            PrintReportTargetAgencyHD4554ViewModel = printReportTargetAgencyHD4554ViewModel;

            ExportGridDataCommand = new RelayCommand(obj => OnExportDataDialog());
            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            UploadFileCommandHTTP = new RelayCommand(obj => OnUploadDialog(true));
            UploadFileCommandFTP = new RelayCommand(obj => OnUploadDialog(false));
            ExportJsonCommand = new RelayCommand(obj => OnExportJsonDataDialog());
            ImportJsonCommand = new RelayCommand(obj => OnImportJson());
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
            MarginRequirement = new System.Windows.Thickness(10);
            LoadLockStatus();
            CheckParentAgency();
            LoadVoucherType();
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

        protected override void OnDelete()
        {
            base.OnDelete();

            if (SelectedItem.SNguoiTao != _sessionService.Current.Principal)
            {
                System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleDelete, SelectedItem.SNguoiTao), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, SelectedItem.DNgayChungTu.HasValue ? SelectedItem.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty);
            MessageBoxResult result = MessageBox.Show(messageBuilder.ToString(), Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                DeleteSelectedVoucher();
        }

        private void DeleteSelectedVoucher()
        {
            _dtChungTuMapService.DeleteByIdPhanBoDuToan(SelectedItem.Id);
            // Delete by fluent api cascade
            _dtChungTuChiTietService.DeleteByIdChungTu(SelectedItem.Id);
            _estimationService.Delete(SelectedItem.Id);
            MessageBox.Show(Resources.MsgDeleteSuccess, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            OnRefresh();
        }

        protected override void OnLockUnLock()
        {
            if (IsLock)
            {
                var listSoChungTu = string.Join(", ", Items.Where(n => n.Selected && n.BKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    //MessageBox.Show(Resources.MsgRoleUnlock, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    MessageBox.Show(string.Format("Đồng chí không được mở khóa chứng từ {0} do không có quyền tổng hợp", listSoChungTu), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                var listSoChungTuInvalid = string.Join(", ", Items.Where(n => n.Selected && n.SNguoiTao != _sessionInfo.Principal && !n.BKhoa).Select(n => n.SSoChungTu));

                if (!string.IsNullOrEmpty(listSoChungTuInvalid))
                {
                    //MessageBox.Show(string.Format(Resources.MsgRoleLock, SelectedItemElement.SNguoiTao), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
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
                lstChungTuChon = lstChungTuChon.Where(x => x.BKhoa).ToList();
            }
            else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
            {
                lstChungTuChon = lstChungTuChon.Where(x => !x.BKhoa).ToList();
            }

            if (result == MessageBoxResult.Yes)
            {
                foreach (var SelectedItemElement in lstChungTuChon)
                {
                    var rs = _estimationService.LockOrUnLock(SelectedItemElement.Id, !SelectedItemElement.BKhoa);
                    if (rs == DBContextSaveChangeState.SUCCESS)
                    {
                        SelectedItemElement.BKhoa = !SelectedItemElement.BKhoa;
                        OnPropertyChanged(nameof(IsLock));
                        OnPropertyChanged(nameof(IsEdit));
                    }
                }
                //OnPropertyChanged(nameof(LockStatusSelected));
                MessageBoxHelper.Info(msgDone);
                //_dtChungTuView.Refresh();

                //LockStatusSelected = IsLock ? LockStatus.ElementAt(2) : LockStatus.ElementAt(1);
                LockStatusSelected = LockStatus.ElementAt(0);
            }
        }

        private void LoadData()
        {
            EstimationVoucherCriteria condition = new EstimationVoucherCriteria
            {
                EstimationType = SoChungTuType.EstimateDivision,
                YearOfWork = _sessionInfo.YearOfWork,
                YearOfBudget = _sessionInfo.YearOfBudget,
                BudgetSource = _sessionInfo.Budget,
                Status = (int)Status.ACTIVE,
                UserName = _sessionInfo.Principal,
                VoucherType = CbxVoucherTypeSelected == null ? CbxVoucherType.Select(item => int.Parse(item.ValueItem)).FirstOrDefault() : int.Parse(CbxVoucherTypeSelected.ValueItem)
            };
            var listChungTu = _estimationService.FindByCondition(condition).ToList();
            Items = _mapper.Map<ObservableCollection<DtChungTuModel>>(listChungTu);
            var dictIdChungTu = _estimationService.FindAllDict();
            Items.ForAll(x => x.ListSoChungTuDotNhan = string.Join(",", x.IIdDotNhan.Split(",").Select(e => dictIdChungTu.GetValueOrDefault(e, string.Empty))));

            _dtChungTuView = CollectionViewSource.GetDefaultView(Items);
            _dtChungTuView.Filter = VoucherFilter;

            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }

            foreach (var item in Items)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(DtChungTuModel.Selected))
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

        private bool VoucherFilter(object obj)
        {
            bool result = true;
            var item = (DtChungTuModel)obj;

            if (_cbxVoucherTypeSelected != null)
            {
                result = result && item.ILoaiChungTu.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem));
            }

            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
            {
                result = result && item.BKhoa;
            }

            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
            {
                result = result && !item.BKhoa;
            }


            return result;
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
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

            DivisionEstimateDialogViewModel.Model = new DtChungTuModel();
            DivisionEstimateDialogViewModel.RefreshVoucherEvent += (object sender, EventArgs e) =>
            {
                OnRefresh();
            };
            DivisionEstimateDialogViewModel.Init();
            DivisionEstimateDialogViewModel.SavedAction = obj =>
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
            var view = new DivisionEstimateDialog
            {
                DataContext = DivisionEstimateDialogViewModel
            };
            DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
            DivisionEstimateDialogViewModel.RefreshVoucherEvent -= (object sender, EventArgs e) =>
            {
                OnRefresh();
            };

        }

        protected override void OnUpdate()
        {
            //check quyền được chỉnh sửa
            if (SelectedItem.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBox.Show(string.Format(Resources.MsgRoleUpdate, SelectedItem.SNguoiTao), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DivisionEstimateDialogViewModel.Model = SelectedItem;
            DivisionEstimateDialogViewModel.RefreshVoucherEvent += (object sender, EventArgs e) =>
            {
                OnRefresh();
            };
            DivisionEstimateDialogViewModel.Init();
            DivisionEstimateDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            var view = new DivisionEstimateDialog { DataContext = DivisionEstimateDialogViewModel };

            DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
            DivisionEstimateDialogViewModel.RefreshVoucherEvent -= (object sender, EventArgs e) =>
            {
                OnRefresh();
            };
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenDivisionDetail((DtChungTuModel)obj);
        }

        private void OnOpenDivisionDetail(DtChungTuModel selectEstimation)
        {
            DivisionEstimateDetailViewModel.Model = selectEstimation;
            DivisionEstimateDetailViewModel.UpdateVoucherEvent += RefreshAfterSaveData;
            DivisionEstimateDetailViewModel.Init();
            var view = new DivisionEstimateDetail { DataContext = DivisionEstimateDetailViewModel };
            view.ShowDialog();
            DivisionEstimateDetailViewModel.UpdateVoucherEvent -= RefreshAfterSaveData;
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

        private void OnExportDataDialog()
        {
            ExportDivisionEstimateViewModel._listChungTuChiTiet = _listChungTuChiTiet;
            ExportDivisionEstimateViewModel.Items = Items;
            ExportDivisionEstimateViewModel.Init();
            ExportDivisionEstimateViewModel.ShowDialogHost();
        }

        private void OnExportJsonDataDialog()
        {
            List<NsDtChungTu> lstData = new List<NsDtChungTu>();
            if (!Items.Any(n => n.Selected))
            {
                new NSMessageBoxViewModel("Vui lòng chọn bản ghi!").ShowDialogHost();
                return;
            }
                
            var listChungTu = _mapper.Map<ObservableCollection<NsDtChungTu>>(Items.Where(n => n.Selected)).Select(n =>
            {
                n.ListVoucherMap = _dtChungTuMapService.FindByIdPhanBoDuToan(n.Id.ToString()).ToList();
                n.ListVoucher = _estimationService.FindDotNhanByChungTuPhanBo(n.Id).ToList();
                n.ListDetailVoucher = _dtChungTuChiTietService.FindByListIdChungTu(n.ListVoucherMap.Select(x => x.IIdCtduToanNhan.ToString()))
                    .Select(x =>
                    {
                        x.IIdMaDonVi = _sessionInfo.IdDonVi;
                        return x;
                    })
                    .ToList();
                n.ListDetailChiTiet = _dtChungTuChiTietService.FindByIdChungTu(n.Id.ToString()).ToList();                
                //if (n.ILoaiDuToan == 4)
                //{
                //    n.ListDetailVoucher.ForEach(d => { d.IIdDtchungTu = n.ListVoucher.Where(t=>t.Id == d.IIdCtduToanNhan).First().Id; d.IIdCtduToanNhan = null; });
                //    n.ILoaiDuToan = 2;
                //    n.IIdDotNhan = n.ListVoucher.First().Id.ToString();
                //    n.ListVoucherMap.ForEach(d => d.IIdCtduToanNhan = n.ListVoucher.First().Id);
                //}
                return n;
            }).ToList();

            ExportJsonDivisionEstimateViewModel.ListChungTu = listChungTu;
            ExportJsonDivisionEstimateViewModel.Items = Items;
            ExportJsonDivisionEstimateViewModel.Init();
            ExportJsonDivisionEstimateViewModel.ShowDialogHost();
        }

        private void OnExportGridData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                string chiTietToi = "NG";
                DanhMuc danhMucChiTietToi = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                if (danhMucChiTietToi != null)
                    chiTietToi = danhMucChiTietToi.SGiaTri;
                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ExportResult> results = new List<ExportResult>();

                string templateFileName = "rpt_DT_ChungTu.xlsx";

                var namLamViec = _sessionService.Current.YearOfWork;
                var listNsMucLucNganSach = _nsMucLucNganSachService.FindAll(namLamViec);
                var itemsExport = Items.Where(x => x.Selected);
                var dictDonVi = _donViService.FindByListIdDonVi(string.Join(",", itemsExport.Select(x => x.SDsidMaDonVi)), namLamViec)
                    .GroupBy(x => x.IIDMaDonVi)
                    .ToDictionary(x => x.Key, x => x.First());
                foreach (var item in itemsExport)
                {
                    var dataExportDetail = LoadDataExportDetail(item);
                    var listDonVi = item.SDsidMaDonVi.Split(",");
                    var isNSSD = item.ILoaiChungTu.HasValue && VoucherType.NSSD_Key.Equals(item.ILoaiChungTu.ToString());

                    var listMLNS = _nsMucLucNganSachService.FindByListLnsDonVi(item.SDslns, _sessionService.Current.YearOfWork).ToList();
                    DivisionColumnVisibility columnVisibility = new DivisionColumnVisibility();
                    columnVisibility.IsDisplayTuChi = listMLNS.Any(x => x.BTuChi);
                    columnVisibility.IsDisplayHienVat = listMLNS.Any(x => x.BHienVat);
                    columnVisibility.IsDisplayDuPhong = listMLNS.Any(x => x.BDuPhong);
                    columnVisibility.IsDisplayHangMua = listMLNS.Any(x => x.BHangMua);
                    columnVisibility.IsDisplayHangNhap = listMLNS.Any(x => x.BHangNhap);
                    columnVisibility.IsDisplayPhanCap = listMLNS.Any(x => x.BPhanCap);
                    columnVisibility.IsDisplayTonKho = listMLNS.Any(x => x.BTonKho);

                    foreach (var idDonVi in listDonVi)
                    {
                        var data = new Dictionary<string, object>();
                        var tenDonVi = dictDonVi.GetValueOrDefault(idDonVi, new DonVi()).TenDonVi;

                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                        data.Add("TitleFirst", $"DỰ TOÁN CHI NGÂN SÁCH NĂM {_sessionService.Current.YearOfWork}");
                        data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoQuyetDinh}, ngày: {DateUtils.Format(item.DNgayQuyetDinh)})");

                        data.Add("HeaderTenDonVi", $"Đơn vị: {idDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{tenDonVi}");
                        data.Add("TenDonVi", $"{idDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{tenDonVi}");
                        data.Add("SoChungTu", item.SSoChungTu);
                        data.Add("NgayChungTu", DateUtils.Format(item.DNgayChungTu));
                        data.Add("SoQuyetDinh", item.SSoQuyetDinh);
                        data.Add("NgayQuyetDinh", DateUtils.Format(item.DNgayQuyetDinh));
                        data.Add("MoTa", item.SMoTa);
                        data.Add("LoaiDuToan", VoucherType.BudgetTypeDict.GetValueOrDefault(item.ILoaiDuToan, string.Empty));
                        data.Add("LoaiChungTu", VoucherType.VoucherTypeDict.GetValueOrDefault(item.ILoaiChungTu, string.Empty));
                        data.Add("NguoiTao", item.SNguoiTao);
                        data.Add("NgayTao", DateUtils.Format(item.DNgayTao));

                        var listData = dataExportDetail.Where(x => x.IsHangCha || idDonVi.Equals(x.IIdMaDonVi)).ToList();
                        CalculateData(listData);
                        var chungTu = CalculateTotal(listData);

                        var listDataExport = listData.Where(CheckIsHasData).ToList();
                        data.Add("Items", listDataExport);
                        data.Add("MLNS", listNsMucLucNganSach);

                        data.Add("TotalTuChi", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongTuChi));
                        data.Add("TotalHienVat", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongHienVat));
                        data.Add("TotalHangNhap", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongHangNhap));
                        data.Add("TotalHangMua", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongHangMua));
                        data.Add("TotalPhanCap", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongPhanCap));
                        data.Add("TotalDuPhong", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongDuPhong));
                        data.Add("TotalTonKho", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongTonKho));

                        List<int> hideColumns = new List<int>();
                        hideColumns.AddRange(ExportExcelHelper<DtChungTuChiTietModel>.HideColumn(chiTietToi));
                        hideColumns.AddRange(ExportExcelHelper<DtChungTuChiTietModel>.HideColumnDivision(columnVisibility));
                        var xlsFile = _exportService.Export<DtChungTuChiTietModel, NsMucLucNganSach>(templateFileName, data, hideColumns);
                        string fileNamePrefix = string.Format("{0}_{1}_{2}", item.SSoChungTu, item.SSoQuyetDinh, tenDonVi);
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

        private void CalculateData(List<DtChungTuChiTietModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHienVat = 0;
                    x.FHangNhap = 0;
                    x.FHangMua = 0;
                    x.FPhanCap = 0;
                    return x;
                }).ToList();

            foreach (var item in listData.Where(x => x.IsEditable && (x.FTuChi != 0 || x.FHienVat != 0 || x.FHangNhap != 0 || x.FHangMua != 0 || x.FPhanCap != 0)))
            {
                CalculateParent(listData, item, item);
            }
        }

        private void CalculateParent(List<DtChungTuChiTietModel> listData, DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem)
        {
            var parrentItem = listData.FirstOrDefault(x => x.IIdMlns == currentItem.IIdMlnsCha);
            if (parrentItem == null) return;
            parrentItem.FTuChi += seftItem.FTuChi;
            parrentItem.FHienVat += seftItem.FHienVat;
            parrentItem.FHangNhap += seftItem.FHangNhap;
            parrentItem.FHangMua += seftItem.FHangMua;
            parrentItem.FPhanCap += seftItem.FPhanCap;
            CalculateParent(listData, parrentItem, seftItem);
        }

        private DtChungTuModel CalculateTotal(List<DtChungTuChiTietModel> listData)
        {
            DtChungTuModel chungTu = new DtChungTuModel();

            var listChildren = listData.Where(x => x.IsEditable).ToList();
            foreach (var item in listChildren)
            {
                chungTu.FTongTuChi += item.FTuChi;
                chungTu.FTongHienVat += item.FHienVat;
                chungTu.FTongHangNhap += item.FHangNhap;
                chungTu.FTongHangMua += item.FHangMua;
                chungTu.FTongPhanCap += item.FPhanCap;
            }

            return chungTu;
        }

        private List<DtChungTuChiTietModel> LoadDataExportDetail(DtChungTuModel item)
        {
            var searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = item.Id,
                LNS = item.SDslns,
                YearOfWork = item.INamLamViec,
                YearOfBudget = item.INamNganSach,
                BudgetSource = item.IIdMaNguonNganSach,
                VoucherDate = item.DNgayChungTu,
                IdDotNhan = item.IIdDotNhan,
                SoChungTu = item.SSoChungTu
            };

            if (item.ILoaiDuToan.HasValue && BudgetType.ADJUSTED.Equals((BudgetType)item.ILoaiDuToan.Value))
            {
                var listNhanPhanBo = LoadNhanPhanBo(item.Id.ToString());
                searchCondition.LNS = string.Join(",", listNhanPhanBo.Select(x => x.SDslns));
            }

            var listChungTuChiTiet = _dtChungTuChiTietService.FindByCond(searchCondition, procedure: "sp_dt_export_phan_bo_du_toan_chi_tiet").ToList();
            _listChungTuChiTiet = _mapper.Map<List<DtChungTuChiTietModel>>(listChungTuChiTiet);
            return _listChungTuChiTiet;
        }

        private bool CheckIsHasData(DtChungTuChiTietModel chiTietModel)
        {
            return chiTietModel.FTuChi != 0 || chiTietModel.FHienVat != 0 || chiTietModel.FDuPhong != 0 ||
                   chiTietModel.FHangNhap != 0 || chiTietModel.FHangMua != 0 || chiTietModel.FPhanCap != 0;
        }

        private IEnumerable<NsDtChungTu> LoadNhanPhanBo(string idPhanBoDuToan)
        {
            var dtChungTuMapByIdPhanBoDuToan = _dtChungTuMapService.FindByIdPhanBoDuToan(idPhanBoDuToan).ToList();
            var listIdNhanPhanBo = dtChungTuMapByIdPhanBoDuToan.Select(e => e.IIdCtduToanNhan.ToString()).ToHashSet();

            var listNhanPhanBo = new List<NsDtChungTu>();
            if (dtChungTuMapByIdPhanBoDuToan.Count() > 0)
            {
                var predicate = PredicateBuilder.True<NsDtChungTu>();
                predicate = predicate.And(x => listIdNhanPhanBo.Contains(x.Id.ToString()));
                listNhanPhanBo = _estimationService.FindByCondition(predicate).OrderBy(x => x.DNgayChungTu).ToList();
            }
            return listNhanPhanBo;
        }

        /// <summary>
        /// open screen print
        /// </summary>
        /// <param name="param"></param>
        private void OpenPrintDialog(object param)
        {
            var divisionEstimatePrintType = (DivisionEstimatePrintType)((int)param);
            object content = null;
            var models = Items;
            if (_cbxVoucherTypeSelected != null)
            {
                models = new ObservableCollection<DtChungTuModel>(Items.Where(item => item.ILoaiChungTu.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem))));
            }
            switch (divisionEstimatePrintType)
            {
                case DivisionEstimatePrintType.COVER_SHEET:
                    PrintReportCoverSheetViewModel.Init();
                    content = new PrintReportCoverSheet
                    {
                        DataContext = PrintReportCoverSheetViewModel
                    };
                    break;
                case DivisionEstimatePrintType.TARGET_AGENCY:
                    PrintReportTargetAgencyViewModel.Model = SelectedItem;
                    PrintReportTargetAgencyViewModel.Init();
                    content = new PrintReportTargetAgency
                    {
                        DataContext = PrintReportTargetAgencyViewModel
                    };
                    break;
                case DivisionEstimatePrintType.TARGET_AGENCY_SUMMARY:
                    PrintReportTargetAgencySummaryViewModel.Model = SelectedItem;
                    PrintReportTargetAgencySummaryViewModel.Init();
                    content = new PrintReportTargetAgencySummary
                    {
                        DataContext = PrintReportTargetAgencySummaryViewModel
                    };
                    break;
                case DivisionEstimatePrintType.TARGET_MAJORS:
                    PrintReportTargetMajorsViewModel.Init();
                    content = new PrintReportTargetMajors
                    {
                        DataContext = PrintReportTargetMajorsViewModel
                    };
                    break;
                case DivisionEstimatePrintType.SYNTHESIS_BUDGET_AGENCY:
                    PrintReportSynthesisAgencyViewModel.InMotToCheckedVisibility = Visibility.Visible;
                    PrintReportSynthesisAgencyViewModel.Model = SelectedItem;
                    PrintReportSynthesisAgencyViewModel.Models = models;
                    PrintReportSynthesisAgencyViewModel.Init();
                    content = new PrintReportSynthesisAgency
                    {
                        DataContext = PrintReportSynthesisAgencyViewModel
                    };
                    break;
                case DivisionEstimatePrintType.SYNTHESIS_BUDGET_DIVISION:
                    PrintReportSynthesisDivisionViewModel.Model = SelectedItem;
                    PrintReportSynthesisDivisionViewModel.Models = models;
                    PrintReportSynthesisDivisionViewModel.Init();
                    content = new PrintReportSynthesisDivision
                    {
                        DataContext = PrintReportSynthesisDivisionViewModel
                    };
                    break;
                case DivisionEstimatePrintType.TARGET_AGENCY_LNS:
                    PrintReportTargetAgencyLnsViewModel.Model = SelectedItem;
                    PrintReportTargetAgencyLnsViewModel.Models = models;
                    PrintReportTargetAgencyLnsViewModel.Init();
                    content = new PrintReportTargetAgencyLns
                    {
                        DataContext = PrintReportTargetAgencyLnsViewModel
                    };
                    break;
                case DivisionEstimatePrintType.TARGET_MAJORS_DAY:
                    PrintReportTargetMajorsDayViewModel.Init();
                    content = new PrintReportTargetMajorsDay
                    {
                        DataContext = PrintReportTargetMajorsDayViewModel
                    };
                    break;
                case DivisionEstimatePrintType.TARGET_MAJORS_AGENCY:
                    PrintReportTargetMajorAgencyViewModel.Init();
                    content = new PrintReportTargetMajorAgency
                    {
                        DataContext = PrintReportTargetMajorAgencyViewModel
                    };
                    break;
                case DivisionEstimatePrintType.PUBLIC_FINANCE:
                    PrintReportPublicFinanceViewModel.Models = models;
                    PrintReportPublicDivisionViewModel.DivisionPrintType = DivisionEstimatePrintType.PUBLIC_FINANCE;
                    PrintReportPublicFinanceViewModel.Init();
                    content = new PrintReportPublicFinance
                    {
                        DataContext = PrintReportPublicFinanceViewModel
                    };
                    break;
                case DivisionEstimatePrintType.PUBLIC_DIVISION:
                    PrintReportPublicDivisionViewModel.DivisionPrintType = DivisionEstimatePrintType.PUBLIC_DIVISION;
                    PrintReportPublicDivisionViewModel.LoaiChungTu = int.Parse(VoucherType.NSSD_Key);
                    PrintReportPublicDivisionViewModel.Items = models;
                    PrintReportPublicDivisionViewModel.Init();
                    //PrintReportPublicDivisionViewModel.InitWithSelected(SelectedItem.Id);
                    content = new PrintReportPublicDivision
                    {
                        DataContext = PrintReportPublicDivisionViewModel
                    };
                    break;
                case DivisionEstimatePrintType.DIVISION_PLAN:
                    PrintReportDivisionPlanViewModel.DivisionPrintType = DivisionEstimatePrintType.DIVISION_PLAN;
                    PrintReportDivisionPlanViewModel.Items = models;
                    PrintReportDivisionPlanViewModel.Init();
                    content = new PrintReportDivisionPlan
                    {
                        DataContext = PrintReportDivisionPlanViewModel
                    };
                    break;
                case DivisionEstimatePrintType.TARGET_AGENCY_HD4554:
                    PrintReportTargetAgencyHD4554ViewModel.Model = SelectedItem;
                    PrintReportTargetAgencyHD4554ViewModel.Init();
                    content = new PrintReportTargetAgencyHD4554
                    {
                        DataContext = PrintReportTargetAgencyHD4554ViewModel
                    };
                    break;
                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, DivisionEstimateScreen.ROOT_DIALOG, null, null);
            }
        }

        private void CheckParentAgency()
        {
            HasParentAgency = false;
            List<DonVi> donVis = _donViService.GetDanhSachDonViByNguoiDung(_sessionInfo.Principal, _sessionInfo.YearOfWork);
            if (donVis.Any(x => x.Loai == LoaiDonVi.ROOT))
                HasParentAgency = true;
        }

        /*
         *  Thêm popup chọn tiêu chí để gửi dữ liệu
         */

        private async void OnUploadDialog(bool isSendHTTP)
        {
            if (!Items.Any(n => n.Selected) || Items.Where(n => n.Selected).Count() > 1)
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng chọn duy nhất 1 bản ghi !");
                MessageBox.Show(messageBuilder.ToString());
                return;
            }
            IsLoading = true;
            try
            {
                var info = await _hTTPUploadFileService.GetToken(isSendHTTP);
                if (info.Item1 != 200)
                {
                    IsLoading = false;
                    new NSMessageBoxViewModel(info.Item2).ShowDialogHost();
                    return;
                }
                else if (string.IsNullOrEmpty(info.Item2))
                {
                    IsLoading = false;
                    new NSMessageBoxViewModel("Cấu hình sai đường dẫn hoặc cổng HTTP").ShowDialogHost();
                    return;
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                IsLoading = false;
                new NSMessageBoxViewModel("Cấu hình sai đường dẫn hoặc cổng HTTP").ShowDialogHost();
                return;
            }
            SendDataDivisionEstimateViewModel.ListChildAgency = await _hTTPUploadFileService.FindAllChildren(isSendHTTP);
            SendDataDivisionEstimateViewModel._listChungTuChiTiet = _listChungTuChiTiet;
            SendDataDivisionEstimateViewModel.Items = Items;
            SendDataDivisionEstimateViewModel.IsSendHTTP = isSendHTTP;
            SendDataDivisionEstimateViewModel.Init();
            var addView = new View.Budget.Estimate.DivisionEstimate.SendDataDivisionEstimate.SendDataDivisionEstimate() { DataContext = SendDataDivisionEstimateViewModel };
            IsLoading = false;
            DialogHost.Show(addView, SettlementScreen.ROOT_DIALOG, null, null);
        }

        private void OnUpload()
        {
            try
            {
                if (!Items.Any(n => n.Selected) || Items.Where(n => n.Selected).Count() > 1)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Vui lòng chọn duy nhất 1 bản ghi !");
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }

                string chiTietToi = "NG";
                DanhMuc danhMucChiTietToi = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                if (danhMucChiTietToi != null)
                    chiTietToi = danhMucChiTietToi.SGiaTri;
                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ExportResult> results = new List<ExportResult>();

                string templateFileName = "rpt_DT_ChungTu.xlsx";

                var namLamViec = _sessionService.Current.YearOfWork;
                var listNsMucLucNganSach = _nsMucLucNganSachService.FindAll(namLamViec);
                var itemsExport = Items.Where(x => x.Selected);
                var dictDonVi = _donViService.FindByListIdDonVi(string.Join(",", itemsExport.Select(x => x.SDsidMaDonVi)), namLamViec)
                    .GroupBy(x => x.IIDMaDonVi)
                    .ToDictionary(x => x.Key, x => x.First());
                foreach (var item in itemsExport)
                {
                    var dataExportDetail = LoadDataExportDetail(item);
                    var listDonVi = item.SDsidMaDonVi.Split(",");
                    var isNSSD = item.ILoaiChungTu.HasValue && VoucherType.NSSD_Key.Equals(item.ILoaiChungTu.ToString());
                    StringBuilder errorMessageBuilder = new StringBuilder();

                    var listMLNS = _nsMucLucNganSachService.FindByListLnsDonVi(item.SDslns, _sessionService.Current.YearOfWork).ToList();
                    DivisionColumnVisibility columnVisibility = new DivisionColumnVisibility();
                    columnVisibility.IsDisplayTuChi = listMLNS.Any(x => x.BTuChi);
                    columnVisibility.IsDisplayHienVat = listMLNS.Any(x => x.BHienVat);
                    columnVisibility.IsDisplayDuPhong = listMLNS.Any(x => x.BDuPhong);
                    columnVisibility.IsDisplayHangMua = listMLNS.Any(x => x.BHangMua);
                    columnVisibility.IsDisplayHangNhap = listMLNS.Any(x => x.BHangNhap);
                    columnVisibility.IsDisplayPhanCap = listMLNS.Any(x => x.BPhanCap);
                    columnVisibility.IsDisplayTonKho = listMLNS.Any(x => x.BTonKho);
                    string sNameUnit = string.Empty;

                    foreach (var idDonVi in listDonVi)
                    {
                        var data = new Dictionary<string, object>();
                        var tenDonVi = dictDonVi.GetValueOrDefault(idDonVi, new DonVi()).TenDonVi;
                        string sStage = StringUtils.UCS2Convert(VoucherType.VoucherTypeDict.GetValueOrDefault(item.ILoaiChungTu, string.Empty));

                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                        data.Add("TitleFirst", $"DỰ TOÁN CHI NGÂN SÁCH NĂM {_sessionService.Current.YearOfWork}");
                        data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoQuyetDinh}, ngày: {DateUtils.Format(item.DNgayQuyetDinh)})");

                        data.Add("HeaderTenDonVi", $"Đơn vị: {idDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{tenDonVi}");
                        data.Add("TenDonVi", $"{idDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{tenDonVi}");
                        data.Add("SoChungTu", item.SSoChungTu);
                        data.Add("NgayChungTu", DateUtils.Format(item.DNgayChungTu));
                        data.Add("SoQuyetDinh", item.SSoQuyetDinh);
                        data.Add("NgayQuyetDinh", DateUtils.Format(item.DNgayQuyetDinh));
                        data.Add("MoTa", item.SMoTa);
                        data.Add("LoaiDuToan", VoucherType.BudgetTypeDict.GetValueOrDefault(item.ILoaiDuToan, string.Empty));
                        data.Add("LoaiChungTu", VoucherType.VoucherTypeDict.GetValueOrDefault(item.ILoaiChungTu, string.Empty));
                        data.Add("NguoiTao", item.SNguoiTao);
                        data.Add("NgayTao", DateUtils.Format(item.DNgayTao));

                        var listData = dataExportDetail.Where(x => x.IsHangCha || idDonVi.Equals(x.IIdMaDonVi)).ToList();
                        CalculateData(listData);
                        var chungTu = CalculateTotal(listData);

                        var listDataExport = listData.Where(CheckIsHasData).ToList();
                        data.Add("Items", listDataExport);
                        data.Add("MLNS", listNsMucLucNganSach);

                        data.Add("TotalTuChi", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongTuChi));
                        data.Add("TotalHienVat", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongHienVat));
                        data.Add("TotalHangNhap", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongHangNhap));
                        data.Add("TotalHangMua", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongHangMua));
                        data.Add("TotalPhanCap", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongPhanCap));
                        data.Add("TotalDuPhong", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongDuPhong));
                        data.Add("TotalTonKho", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongTonKho));

                        List<int> hideColumns = new List<int>();
                        hideColumns.AddRange(ExportExcelHelper<DtChungTuChiTietModel>.HideColumn(chiTietToi));
                        hideColumns.AddRange(ExportExcelHelper<DtChungTuChiTietModel>.HideColumnDivision(columnVisibility));
                        var xlsFile = _exportService.Export<DtChungTuChiTietModel, NsMucLucNganSach>(templateFileName, data, hideColumns);
                        string fileNamePrefix = string.Format("{0}_{1}_{2}", item.SSoChungTu, item.SSoQuyetDinh, tenDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);

                        ////
                        string filePathLocal = string.Empty;
                        _exportService.Open(Result, ExportType.EXCEL, ref filePathLocal);
                        sNameUnit = StringUtils.UCS2Convert(idDonVi) + "-" + StringUtils.UCS2Convert(tenDonVi).Replace("---", "-");
                        string sFolderRoot = ConstantUrlPathPhanHe.UrlPbdtWinformReceive;
                        var strUrl = string.Format("{0}/{1}/{2}", sNameUnit, sFolderRoot, sStage);
                        if (!File.Exists(strUrl))
                        {
                            string strActiveFileName = "";
                            string[] splitActiveFiName = xlsFile.ActiveFileName.Split("\\");
                            if (strActiveFileName != null && splitActiveFiName.Length != 0)
                            {
                                strActiveFileName = splitActiveFiName[splitActiveFiName.Length - 1];
                            }
                            VdtFtpRoot dataRoot = new VdtFtpRoot();
                            List<string> configCodes = new List<string>()
                            {
                                STORAGE_CONFIG.FTP_HOST
                            };
                            var rs = _danhMucService.FindByCodes(configCodes).ToList();
                            var SIpAddress = rs.FirstOrDefault(x => STORAGE_CONFIG.FTP_HOST.Equals(x.IIDMaDanhMuc)).SGiaTri;
                            // Lấy ra 1 bản ghi trong bảng VDT_FtpRoot để kiểm tra xem đã tồn tại hay chưa.
                            dataRoot = _ftpService.GetVdtFtpRoot(idDonVi, SIpAddress, sFolderRoot);
                            if (dataRoot == null)
                            {
                                dataRoot = new VdtFtpRoot()
                                {
                                    SMaDonVi = idDonVi,
                                    SIpAddress = SIpAddress, // vd: ftp:\\10.60.108.246
                                    SFolderRoot = sFolderRoot,
                                    SNguoiTao = _sessionService.Current.Principal,
                                    DNgayTao = DateTime.Now
                                };
                                _ftpService.Add(dataRoot);
                            }
                            var result = _ftpStorageService.UploadCommand(dataRoot.Id, filePathLocal, strActiveFileName, strUrl);
                            if (result != 1)
                            {
                                errorMessageBuilder.AppendFormat($"Gửi dữ liệu file {strActiveFileName} thất bại\n");
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(errorMessageBuilder.ToString()))
                    {
                        StringBuilder messageBuilder = new StringBuilder();
                        messageBuilder.AppendFormat("Gửi dữ liệu thành công");
                        MessageBox.Show(messageBuilder.ToString());
                        return;
                    }
                    else
                    {
                        MessageBox.Show(errorMessageBuilder.ToString());
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        #region Json
        private void OnImportJson()
        {
            DivisionEstimateImportJsonViewModel.Init();
            DivisionEstimateImportJsonViewModel.SavedAction = obj =>
            {
                OnRefresh();
                _importJsonView.Close();
            };
            _importJsonView = new DivisionEstimateImportJson { DataContext = DivisionEstimateImportJsonViewModel };
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
                n.ListVoucherMap = _dtChungTuMapService.FindByIdPhanBoDuToan(n.Id.ToString()).ToList();
                n.ListVoucher = _estimationService.FindDotNhanByChungTuPhanBo(n.Id).ToList();
                n.ListDetailChiTiet = _dtChungTuChiTietService.FindByIdChungTu(n.Id.ToString()).ToList();
                return n;
            }).ToList();

        }
        #endregion
    }
}
