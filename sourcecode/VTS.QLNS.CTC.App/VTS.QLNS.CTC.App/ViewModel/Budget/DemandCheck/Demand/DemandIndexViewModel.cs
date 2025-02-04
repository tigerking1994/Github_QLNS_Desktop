using AutoMapper;
using FlexCel.Core;
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
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Demand;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.FunctionMap.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand.ExportDemand;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand.SendDataDemand;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using PrintCommunicateSettlementLNS = VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check.PrintReport.PrintCommunicateSettlementLNS;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand
{
    public class DemandIndexViewModel : GridViewModelBase<NsSktChungTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly ISktMucLucService _SktMucLucService;
        private readonly ICauHinhCanCuService _iCauHinhCanCuService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly ISktChungTuChiTietCanCuService _iSktChungTuChiTietCanCuService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IExportService _exportService;
        private ICollectionView _nsDonViModelsView;
        private ICollectionView _sktChungTuModelsView;
        private readonly IDanhMucService _danhMucService;
        private readonly IVdtFtpRootService _ftpService;
        private readonly FtpStorageService _ftpStorageService;
        private IHTTPUploadFileService _hTTPUploadFileService;
        private ICryptographyService _cryptographyService;

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        public bool IsButtonEnable
        {
            get
            {
                var result = false;
                var lstSelected = Items.Where(x => x.Selected).ToList();
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    result = true;
                }
                else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    var lstSelectedKhoa = lstSelected.Where(x => x.BKhoa).ToList();
                    var lstSelectedMo = lstSelected.Where(x => !x.BKhoa).ToList();
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
                //if (lstSelected.Count > 0)
                //{
                //    var numberCtuLock = lstSelected.Count(x => x.BKhoa);
                //    var isValid = true;
                //    foreach (var ct in lstSelected)
                //    {
                //        if (ct.SNguoiTao != _sessionInfo.Principal)
                //        {
                //            isValid = false;
                //            break;
                //        }
                //    }
                //    return (numberCtuLock == 0 || numberCtuLock == lstSelected.Count) && isValid;
                //}
                //return _selectedNsSktChungTuModel != null;
                //return LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0;
            }
        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        private DonViModel _selectedNsDonViModel;
        public DonViModel SelectedNsDonViModel
        {
            get => _selectedNsDonViModel;
            set
            {
                SetProperty(ref _selectedNsDonViModel, value);
                SearchData();
            }
        }

        private bool _isOpenExcelPopup;
        public bool IsOpenExcelPopup
        {
            get => _isOpenExcelPopup;
            set => SetProperty(ref _isOpenExcelPopup, value);
        }

        private List<OrderComboboxItem> _orderTypes;
        public List<OrderComboboxItem> OrderTypes
        {
            get => _orderTypes;
            set => SetProperty(ref _orderTypes, value);
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
                //if (_selectedNsSktChungTuModel != null)
                //{
                //    IsLock = _selectedNsSktChungTuModel.BKhoa;
                //}
                //else
                //{
                //    IsEdit = false;
                //}
                //OnPropertyChanged(nameof(IsButtonEnable));
                if (_selectedNsSktChungTuModel == null)
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsExportAggregateData));
                OnPropertyChanged(nameof(IsExportDataFilter));
            }
        }

        private List<NsSktChungTuModel> _lstChungTuOrigin;
        public List<NsSktChungTuModel> LstChungTuOrigin
        {
            get => _lstChungTuOrigin;
            set
            {
                SetProperty(ref _lstChungTuOrigin, value);
            }
        }

        private ObservableCollection<DonViModel> _nsDonViModelItems;
        public ObservableCollection<DonViModel> NsDonViModelItems
        {
            get => _nsDonViModelItems;
            set => SetProperty(ref _nsDonViModelItems, value);
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
                if (_voucherTypeSelected != null)
                {
                    LoadSktChungTus();
                }
                IsAllItemsSelected = false;
                UnCheckBoxAll();
                OnPropertyChanged(nameof(ShowColNSBD));
                OnPropertyChanged(nameof(ShowColNSSD));
            }
        }

        public bool? IsAllItemSummariesSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    if (Items != null && _voucherTypeSelected != null)
                    {
                        Items.Where(x => !x.BDaTongHop.Value && x.ILoaiChungTu.Equals(int.Parse(_voucherTypeSelected.ValueItem))).ForAll(c => c.Selected = value.Value);
                    }
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
                OnRefresh();
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
                {
                    IsLock = true;
                }
                else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                {
                    IsLock = false;
                }
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
                UnCheckBoxAll();
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Checkbox select all property
        /// </summary>
        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null && VoucherTypeSelected != null)
                {
                    var selected = Items.Select(item => item.Selected && item.ILoaiChungTu == Int32.Parse(VoucherTypeSelected.ValueItem)).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, Items);
                    OnPropertyChanged();
                }
            }
        }

        public bool IsCensorship
        {
            get
            {
                var itemSelected = Items.Where(x => x.Selected);
                return itemSelected.Any() && itemSelected.All(x => !x.IsSummaryVocher && x.BKhoa) && itemSelected.Select(x => x.ILoaiNguonNganSach).Distinct().Count() == 1;
            }
        }

        public bool IsExportAggregateData => Items != null && Items.Any(n => n.Selected);
        public bool IsExportDataFilter => _selectedNsSktChungTuModel != null && _selectedNsSktChungTuModel.IsSummaryVocher;

        private void SelectAll(bool select, IEnumerable<NsSktChungTuModel> models)
        {
            foreach (var model in models)
            {
                if (!model.IsSummaryVocher && !model.BDaTongHop.GetValueOrDefault(false) && VoucherTypeSelected != null && model.ILoaiChungTu == Int32.Parse(VoucherTypeSelected.ValueItem))
                {
                    model.Selected = select;
                }
            }
        }
        public bool IsEnableButtonDataShow => TabIndex != ImportTabIndex.Data;

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadData();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));

            }
        }

        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set
            {
                SetProperty(ref _isCollapse, value);
                LoadData();
            }
        }

        public Visibility ShowColNSBD => VoucherTypeSelected != null && VoucherType.NSBD_Key == VoucherTypeSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowColNSSD => VoucherTypeSelected != null && VoucherType.NSSD_Key == VoucherTypeSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;

        public override string FuncCode => NSFunctionCode.BUDGET_DEMANDCHECK_DEMAND;
        public string ComboboxDisplayMemberPath => nameof(SelectedNsDonViModel.TenDonViIdDonVi);
        public override Type ContentType => typeof(DemandIndex);

        public override string Description => "Chứng từ nhập số nhu cầu ngân sách năm " +
                                              _sessionInfo.YearOfWork;
        public override string Name => "Nhu cầu NS năm " + _sessionInfo.YearOfWork;
        public override PackIconKind IconKind => PackIconKind.RhombusOutline;

        public DemandDialogViewModel DemandDialogViewModel { get; }
        public DemandDetailViewModel DemandDetailViewModel { get; }
        public PrintReportDemandOrgViewModel PrintReportDemandOrgViewModel { get; }
        public ImportDemandViewModel ImportDemandViewModel { get; }
        public ExportDemandViewModel ExportDemandViewModel { get; }
        public SendDataDemandViewModel SendDataDemandViewModel { get; }

        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand ExportAggregateDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportDataFilterCommand { get; }
        public RelayCommand UploadFileCommandHTTP { get; }
        public RelayCommand UploadFileCommandFTP { get; }


        public DemandIndexViewModel(
            ISktChungTuService sktChungTuService,
            ISktChungTuChiTietService sktChungTuChiTietService,
            INsDonViService nsDonViService,
            ILog logger,
            IMapper mapper,
            ISktMucLucService sktMucLucService,
            IExportService exportService,
            DemandDialogViewModel demandDialogViewModel,
            ISessionService sessionService,
            INsDonViService donViService,
            ICauHinhCanCuService iCauHinhCanCuService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            PrintReportDemandOrgViewModel printReportDemandOrgViewModel,
            DemandDetailViewModel demandDetailViewModel,
            ImportDemandViewModel importDemandViewModel,
            ExportDemandViewModel exportDemandViewModel,
            SendDataDemandViewModel sendDataDemandViewModel,
            ISysAuditLogService log,
            IDanhMucService danhMucService,
            IVdtFtpRootService ftpService,
            FtpStorageService ftpStorageService,
            IHTTPUploadFileService hTTPUploadFileService,
            ICryptographyService cryptographyService,

            ISktChungTuChiTietCanCuService iSktChungTuChiTietCanCuService)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _sktChungTuService = sktChungTuService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _iCauHinhCanCuService = iCauHinhCanCuService;
            _nsDonViService = nsDonViService;
            _SktMucLucService = sktMucLucService;
            _exportService = exportService;
            _donViService = donViService;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _iSktChungTuChiTietCanCuService = iSktChungTuChiTietCanCuService;
            _log = log;
            _danhMucService = danhMucService;
            _ftpService = ftpService;
            _ftpStorageService = ftpStorageService;
            _hTTPUploadFileService = hTTPUploadFileService;
            _cryptographyService = cryptographyService;
            DemandDialogViewModel = demandDialogViewModel;
            DemandDetailViewModel = demandDetailViewModel;
            ImportDemandViewModel = importDemandViewModel;
            ExportDemandViewModel = exportDemandViewModel;
            SendDataDemandViewModel = sendDataDemandViewModel;
            PrintReportDemandOrgViewModel = printReportDemandOrgViewModel;

            DemandDialogViewModel.ParentPage = this;
            DemandDetailViewModel.ParentPage = this;
            ImportDemandViewModel.ParentPage = this;
            ExportDemandViewModel.ParentPage = this;
            SendDataDemandViewModel.ParentPage = this;

            ExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            PrintCommand = new RelayCommand(OnPrint);
            LockCommand = new RelayCommand(OnLock);
            SearchCommand = new RelayCommand(obj => SearchData());
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            ExportDataFilterCommand = new RelayCommand(obj => OnExportDataFilter());
            UploadFileCommandHTTP = new RelayCommand(obj => OnUploadDialog(true));
            UploadFileCommandFTP = new RelayCommand(obj => OnUploadDialog(false));
            //UploadFileCommand = new RelayCommand(obj => OnUpload());

        }

        public override void OnCancel()
        {
            base.OnCancel();
            ParentPage.ParentPage.CurrentPage = null;
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((NsSktChungTuModel)obj, false);
        }

        protected override void OnDelete()
        {
            if (SelectedNsSktChungTuModel == null) return;
            if (SelectedNsSktChungTuModel.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedNsSktChungTuModel.SNguoiTao));
                return;
            }
            var messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTuSKT, SelectedNsSktChungTuModel.SSoChungTu, SelectedNsSktChungTuModel.DNgayChungTu);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                OnDeleteHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void OnDeleteHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            DateTime dtNow = DateTime.Now;
            if (SelectedNsSktChungTuModel != null)
            {
                var sktChungTu = _sktChungTuService.FindById(SelectedNsSktChungTuModel.Id);
                if (sktChungTu != null)
                {
                    _sktChungTuService.Delete(sktChungTu);
                    if (!string.IsNullOrEmpty(sktChungTu.SDssoChungTuTongHop))
                    {
                        var lstSoCtChild = sktChungTu.SDssoChungTuTongHop.Split(",");
                        foreach (var soct in lstSoCtChild)
                        {
                            var ctChild = _sktChungTuService.FindByCondition(x => x.SSoChungTu.Equals(soct)
                                && x.ILoai == DemandCheckType.DEMAND
                                && x.INamLamViec == _sessionInfo.YearOfWork).FirstOrDefault();
                            if (ctChild != null)
                            {
                                ctChild.BDaTongHop = false;
                                _sktChungTuService.Update(ctChild);
                            }
                        }
                    }
                    _log.WriteLog(Resources.ApplicationName, "Xóa chứng từ số nhu cầu", (int)TypeExecute.Delete, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    var predicate = PredicateBuilder.True<NsSktChungTuChiTiet>();
                    predicate = predicate.And(x => x.IIdCtsoKiemTra == sktChungTu.Id);
                    var sktChungTuChiTiets = _sktChungTuChiTietService.FindByCondition(predicate);
                    _sktChungTuChiTietService.RemoveRange(sktChungTuChiTiets);

                    var predicateCT = PredicateBuilder.True<NsSktChungTuChiTietCanCu>();
                    predicateCT = predicateCT.And(x => x.IiIdCtsoKiemTra == sktChungTu.Id);
                    predicateCT = predicateCT.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                    var sktcanCanCus = _iSktChungTuChiTietCanCuService.FindByCondition(predicateCT).ToList();
                    _iSktChungTuChiTietCanCuService.RemoveRange(sktcanCanCus);

                    LoadSktChungTus();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    MessageBoxHelper.Info(string.Format(Resources.DemandDeleteSuccess));
                }
            }
        }

        private void OnSelectedChange(object obj)
        {
            SelectedNsSktChungTuModel = (NsSktChungTuModel)obj;
            if (SelectedNsSktChungTuModel is { BKhoa: true } || SelectedNsSktChungTuModel == null)
            {
                IsEdit = false;
            }
            else
            {
                IsEdit = true;
            }
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((NsSktChungTuModel)eventArgs.Parameter);
        }

        private void LoadData()
        {
            _sessionInfo = _sessionService.Current;
            LoadSktChungTus();
            LoadOrderTypes();
            LoadNsDonVi();
            OnPropertyChanged(nameof(IsCensorship));
        }

        private void LoadNsDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            if (Items != null && Items.Count > 0)
            {
                var idDonVis = Items.Select(x => x.IIdMaDonVi).ToList();
                predicate = predicate.And(x => idDonVis.Any(y => y == x.IIDMaDonVi));
                var listUnit = _nsDonViService.FindByCondition(predicate).ToList();
                NsDonViModelItems = new ObservableCollection<DonViModel>();
                NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
                _nsDonViModelsView = CollectionViewSource.GetDefaultView(NsDonViModelItems);
                //_nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                //    ListSortDirection.Ascending));
                _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.IIDMaDonVi),
                    ListSortDirection.Ascending));
            }
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

        private void LoadVoucherTypes()
        {
            var voucherTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key},
            };

            VoucherTypes = new ObservableCollection<ComboboxItem>(voucherTypes);
            VoucherTypeSelected = VoucherTypes.ElementAt(0);
        }

        private void LoadOrderTypes()
        {
            OrderTypes = new List<OrderComboboxItem>
            {
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.SO_CHUNG_TU_ASC, ValueItem = DemandCheckSortTypeValue.SO_CHUNG_TU_ASC,
                    SortDirection = ListSortDirection.Ascending, SortMemberPath = nameof(NsSktChungTuModel.SSoChungTu)
                },
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.SO_CHUNG_TU_DESC, ValueItem = DemandCheckSortTypeValue.SO_CHUNG_TU_DESC,
                    SortDirection = ListSortDirection.Descending, SortMemberPath = nameof(NsSktChungTuModel.SSoChungTu)
                },
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.NGAY_CHUNG_TU_ASC,
                    ValueItem = DemandCheckSortTypeValue.NGAY_CHUNG_TU_ASC,
                    SortDirection = ListSortDirection.Ascending, SortMemberPath = nameof(NsSktChungTuModel.DNgayChungTu)
                },
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.NGAY_CHUNG_TU_DESC,
                    ValueItem = DemandCheckSortTypeValue.NGAY_CHUNG_TU_DESC,
                    SortDirection = ListSortDirection.Descending, SortMemberPath = nameof(NsSktChungTuModel.DNgayChungTu)
                },
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.SO_QUYET_DINH_ASC, ValueItem = DemandCheckSortTypeValue.SO_QUYET_DINH_ASC,
                    SortDirection = ListSortDirection.Ascending, SortMemberPath = nameof(NsSktChungTuModel.SSoQuyetDinh)
                },
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.SO_QUYET_DINH_DESC,
                    ValueItem = DemandCheckSortTypeValue.SO_QUYET_DINH_DESC,
                    SortDirection = ListSortDirection.Descending, SortMemberPath = nameof(NsSktChungTuModel.SSoQuyetDinh)
                },
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.NGAY_QUYET_DINH_ASC,
                    ValueItem = DemandCheckSortTypeValue.NGAY_QUYET_DINH_ASC,
                    SortDirection = ListSortDirection.Ascending, SortMemberPath = nameof(NsSktChungTuModel.DNgayQuyetDinh)
                },
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.NGAY_QUYET_DINH_DESC,
                    ValueItem = DemandCheckSortTypeValue.NGAY_QUYET_DINH_DESC,
                    SortDirection = ListSortDirection.Descending, SortMemberPath = nameof(NsSktChungTuModel.DNgayQuyetDinh)
                }
            };
        }

        private void LoadSktChungTus()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var currentIdDonVi = _sessionInfo.IdDonVi;
            var iLoai = DemandCheckType.DEMAND;
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            var loaiNguonNganSach = BudgetSourceTypeSelected != null ? Int32.Parse(BudgetSourceTypeSelected.ValueItem) : 1;
            IEnumerable<NsSktChungTuQuery> listChungTu;
            listChungTu = _sktChungTuService
                .FindChungTuIndexByConditionOptimizeClone(iLoai, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, _sessionInfo.Principal, loaiNguonNganSach, "sp_skt_nhap_so_nhu_cau_clone").ToList();
            _lstChungTuOrigin = _mapper.Map<List<NsSktChungTuModel>>(listChungTu);

            if (_sessionService.Current.IsQuanLyDonViCha)
            {
                if (TabIndex == ImportTabIndex.Data)
                {
                    Items = _mapper.Map<ObservableCollection<NsSktChungTuModel>>(listChungTu.Where(x => !x.IIdMaDonVi.Equals(_sessionInfo.IdDonVi) && !x.BDaTongHop.GetValueOrDefault()));
                }
                else
                {
                    var listCTTongHop = listChungTu.Where(x => x.IIdMaDonVi.Equals(_sessionInfo.IdDonVi)).ToList();
                    var listTongHop = new List<NsSktChungTuModel>();
                    foreach (var ctTongHop in listCTTongHop)
                    {
                        var parent = _mapper.Map<NsSktChungTuModel>(ctTongHop);
                        parent.IsExpand = true;
                        listTongHop.Add(parent);
                        if (!string.IsNullOrEmpty(ctTongHop.SDssoChungTuTongHop))
                        {
                            var listChild = _mapper.Map<List<NsSktChungTuModel>>(listChungTu.Where(x => ctTongHop.SDssoChungTuTongHop != null && ctTongHop.SDssoChungTuTongHop.Contains(x.SSoChungTu)));
                            listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                            listTongHop.AddRange(listChild);
                        }

                        //if (IsCollapse)
                        //{
                        //    Items = _mapper.Map<ObservableCollection<NsSktChungTuModel>>(listChungTu.Where(x => x.Id.Equals(ctTongHop.Id) || ctTongHop.SDssoChungTuTongHop != null && ctTongHop.SDssoChungTuTongHop.Contains(x.SSoChungTu)));
                        //    foreach (var it in Items)
                        //    {
                        //        if (string.IsNullOrEmpty(it.SDssoChungTuTongHop))
                        //        {
                        //            it.IsChildSumary = true;
                        //        }
                        //    }
                        //}
                    }

                    Items = _mapper.Map<ObservableCollection<NsSktChungTuModel>>(listTongHop);

                }
            }
            else
            {
                Items = _mapper.Map<ObservableCollection<NsSktChungTuModel>>(listChungTu);
            }

            foreach (var model in Items)
            {
                model.TypeIcon = model.IsSent ? "CheckBold" : "CancelBold";
                if (model.IIdMaDonVi == currentIdDonVi)
                {
                    model.IsSummaryVocher = true;
                }
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(NsSktChungTuModel.Selected))
                    {
                        OnPropertyChanged(nameof(IsCensorship));
                        OnPropertyChanged(nameof(IsExportAggregateData));
                        OnPropertyChanged(nameof(IsExportDataFilter));
                        OnPropertyChanged(nameof(IsButtonEnable));
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                    }
                    if (args.PropertyName == nameof(NsSktChungTuModel.IsCollapse))
                    {
                        ExpandChild();
                    }
                };
            }

            _sktChungTuModelsView = CollectionViewSource.GetDefaultView(Items);
            _sktChungTuModelsView.Filter = SktChungTuModelsFilter;
        }

        private void ExpandChild()
        {
            if (Items != null)
            {
                Items.Where(n => n.SoChungTuParent == SelectedNsSktChungTuModel.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
        }

        private bool SktChungTuModelsFilter(object obj)
        {
            if (!(obj is NsSktChungTuModel temp)) return true;
            var keyword = SearchText?.Trim().ToLower() ?? string.Empty.Trim().ToLower();
            var condition1 = false;
            var condition2 = true;
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(temp.SSoChungTu))
                    condition1 = condition1 || temp.SSoChungTu.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SSoQuyetDinh))
                    condition1 = condition1 || temp.SSoQuyetDinh.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SMoTa))
                    condition1 = condition1 || temp.SMoTa.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SNguoiTao))
                    condition1 = condition1 || temp.SNguoiTao.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.STenDonVi))
                    condition1 = condition1 || temp.STenDonVi.ToLower().Contains(keyword);
            }
            else
            {
                condition1 = true;
            }

            if (SelectedNsDonViModel != null)
            {
                condition2 = condition2 && temp.IIdMaDonVi == SelectedNsDonViModel.IIDMaDonVi;
            }

            if (VoucherTypeSelected != null)
            {
                condition2 = condition2 && temp.ILoaiChungTu.HasValue && temp.ILoaiChungTu.Value == Int32.Parse(VoucherTypeSelected.ValueItem);
            }

            if (LockStatusSelected != null)
            {
                if (LockStatusSelected.ValueItem.Equals("1"))
                {
                    condition2 = condition2 && temp.BKhoa;
                }
                if (LockStatusSelected.ValueItem.Equals("2"))
                {
                    condition2 = condition2 && temp.BKhoa == false;
                }
            }

            var result = condition1 && condition2;
            return result;
        }

        private void LockOrUnLockRegularBudget()
        {
            if (SelectedNsSktChungTuModel == null) return;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            DateTime dtNow = DateTime.Now;
            IsLock = !IsLock;
            _sktChungTuService.LockOrUnlock(SelectedNsSktChungTuModel.Id, IsLock);
            MessageBoxHelper.Info(msgDone);
            _log.WriteLog(Resources.ApplicationName, "Khóa chứng từ số nhu cầu", (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            var sktChungTu = Items.First(x => x.Id == SelectedNsSktChungTuModel.Id);
            sktChungTu.BKhoa = !SelectedNsSktChungTuModel.BKhoa;
            IsEdit = !sktChungTu.BKhoa;
            LoadSktChungTus();
            OnPropertyChanged(nameof(IsAllItemsSelected));
        }

        private void OnLockMultiHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            LockOrUnLockMultiVoucher();
        }

        private void LockOrUnLockMultiVoucher()
        {
            DateTime dtNow = DateTime.Now;
            var lstSelected = Items.Where(x => x.Selected).ToList();
            var isLock = !lstSelected.FirstOrDefault().BKhoa;
            foreach (var ct in lstSelected)
            {
                _sktChungTuService.LockOrUnlock(ct.Id, isLock);
                var sktChungTu = Items.First(x => x.Id == ct.Id);
                sktChungTu.BKhoa = !ct.BKhoa;
            }
            _log.WriteLog(Resources.ApplicationName, "Khóa chứng từ số nhu cầu", (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            LoadSktChungTus();
        }

        protected override void OnAdd()
        {
            DemandDialogViewModel.Name = "Thêm chứng từ";
            DemandDialogViewModel.Description = "Tạo mới chứng từ số nhu cầu";
            DemandDialogViewModel.NsSktChungTuModel = new NsSktChungTuModel();
            DemandDialogViewModel.isSummary = false;
            DemandDialogViewModel.Init();
            DemandDialogViewModel.SavedAction = obj =>
            {
                var sktChungTu = (NsSktChungTuModel)obj;
                _voucherTypeSelected = sktChungTu.ILoaiChungTu.HasValue
                    ? _voucherTypes.First(x => x.ValueItem.Equals(sktChungTu.ILoaiChungTu.Value.ToString()))
                    : _voucherTypes.First();
                OnPropertyChanged(nameof(VoucherTypeSelected));
                this.LoadData();
                OpenDetailDialog(sktChungTu);
            };
            var exportView = new DemandDialog() { DataContext = DemandDialogViewModel };
            DialogHost.Show(exportView, DemandCheckScreen.ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {
            if (SelectedNsSktChungTuModel != null)
            {
                if (SelectedNsSktChungTuModel.IIdMaDonVi.Equals(_sessionInfo.IdDonVi))
                {
                    OnAggregateEdit();
                }
                else
                {
                    if (SelectedNsSktChungTuModel.SNguoiTao != _sessionInfo.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedNsSktChungTuModel.SNguoiTao));
                        return;
                    }
                    DemandDialogViewModel.NsSktChungTuModel = SelectedNsSktChungTuModel;
                    DemandDialogViewModel.Name = "Sửa chứng từ";
                    DemandDialogViewModel.Description = "Cập nhật thông tin chứng từ số nhu cầu";
                    DemandDialogViewModel.isSummary = false;
                    DemandDialogViewModel.Init();
                    DemandDialogViewModel.SavedAction = obj =>
                    {
                        this.LoadData();
                    };
                    var editView = new DemandDialog() { DataContext = DemandDialogViewModel };
                    DialogHost.Show(editView, DemandCheckScreen.ROOT_DIALOG);
                }
            }
        }

        private void OnLock(object obj)
        {
            if (IsLock)
            {
                string lstSoChungTu = string.Join(", ", Items.Where(n => n.Selected && n.BKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (userAgency.All(x => x.Loai != LoaiDonVi.ROOT))
                {
                    //MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                    MessageBoxHelper.Warning(string.Format("Đồng chí không có quyền mở khóa chứng từ {0} do không có quyền tổng hợp", lstSoChungTu));
                    return;
                }
                string lstSoChungTuDaTongHop = string.Join(", ", Items.Where(n => n.Selected && n.BDaTongHop.GetValueOrDefault() && n.BKhoa && !n.IIdMaDonVi.Equals(_sessionInfo.IdDonVi)).Select(n => n.SSoChungTu));

                if (!string.IsNullOrEmpty(lstSoChungTuDaTongHop))
                {
                    //MessageBoxHelper.Warning(Resources.AlertUnlockAggregatedVoucher);
                    MessageBoxHelper.Warning(string.Format("Đồng chí không có quyền mở khóa chứng từ {0} do đã gửi lên tổng hợp", lstSoChungTuDaTongHop));
                    return;
                }

                var predicate = PredicateBuilder.True<NsSktChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicate = predicate.And(x => x.ILoai == DemandCheckType.DEMAND);
                predicate = predicate.And(x => x.IIdMaDonVi.Equals(_sessionInfo.IdDonVi));
                var ctTongHop = _sktChungTuService.FindByCondition(predicate).FirstOrDefault();
                if (ctTongHop != null && ctTongHop.BKhoa)
                {
                    var lstCtChild = ctTongHop.SDssoChungTuTongHop;
                    string lstSoChungTuCoTongHopKhoa = string.Join(", ", Items.Where(n => n.Selected && lstCtChild != null && lstCtChild.Contains(n.SSoChungTu)));
                    //if (lstCtChild != null && lstCtChild.Contains(SelectedNsSktChungTuModel.SSoChungTu))
                    if (!string.IsNullOrEmpty(lstSoChungTuCoTongHopKhoa))
                    {
                        //MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                        MessageBoxHelper.Warning(string.Format("Vui lòng mở khóa chứng từ tổng hợp của chứng từ {0}", lstSoChungTuCoTongHopKhoa));
                        return;
                    }
                }
            }
            else
            {
                string lstSoChungTuInvalid = string.Join(", ", Items.Where(n => n.Selected && n.SNguoiTao != _sessionInfo.Principal && !n.BKhoa).Select(n => n.SSoChungTu));
                if (!string.IsNullOrEmpty(lstSoChungTuInvalid))
                {
                    MessageBoxHelper.Warning(string.Format("Đồng chí không có quyền khóa chứng từ {0} do không phải người tạo", lstSoChungTuInvalid));
                    return;
                }

                //if (SelectedNsSktChungTuModel.SNguoiTao != _sessionInfo.Principal)
                //{
                //    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, SelectedNsSktChungTuModel.SNguoiTao));
                //    return;
                //}
            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult dialogResult = MessageBoxHelper.Confirm(message);
            if (dialogResult == MessageBoxResult.Yes)
            {
                LockOrUnLockMultiVoucher();
                MessageBoxHelper.Info(msgDone);
                //LockStatusSelected = IsLock ? LockStatus.ElementAt(2) : LockStatus.ElementAt(1);
                LockStatusSelected = LockStatus.ElementAt(0);
                //OnPropertyChanged(nameof(LockStatusSelected));
                //OnRefresh();
                //LockOrUnLockRegularBudget();
            }
        }

        /// <summary>
        ///     Mở màn hình in
        /// </summary>
        /// <param name="param"></param>
        private void OnPrint(object param)
        {
            var demandCheckPrintType = (DemandCheckPrintType)((int)param);
            object content;
            switch (demandCheckPrintType)
            {
                case DemandCheckPrintType.REPORT_ORG_DEMAND_DETAIL_NUMBER:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = demandCheckPrintType;
                    if (Items != null && Items.Count > 0)
                    {
                        var listDonViCheckBox = Items.Select(item => new CheckBoxItem
                        {
                            ValueItem = item.IIdMaDonVi,
                            DisplayItem = string.Join("-", item.IIdMaDonVi, item.STenDonVi)
                        }).OrderBy(item => item.ValueItem);
                        PrintReportDemandOrgViewModel.ListDonVi = new ObservableCollection<CheckBoxItem>(listDonViCheckBox);
                    }
                    PrintReportDemandOrgViewModel.Init();
                    content = new PrintReportDemandOrg
                    {
                        DataContext = PrintReportDemandOrgViewModel
                    };
                    break;
                case DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = demandCheckPrintType;
                    PrintReportDemandOrgViewModel.Init();
                    content = new PrintReportDemandOrg
                    {
                        DataContext = PrintReportDemandOrgViewModel
                    };
                    break;
                case DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = demandCheckPrintType;
                    PrintReportDemandOrgViewModel.Init();
                    content = new PrintReportDemandOrg
                    {
                        DataContext = PrintReportDemandOrgViewModel
                    };
                    break;
                case DemandCheckPrintType.SUMMARY_REPORT_OF_TEST_NUMBER_ALLOCATION:
                    content = new PrintCommunicateSettlementLNS();
                    break;
                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, DivisionScreen.ROOT_DIALOG, null, null);
            }
        }

        protected override void OnRefresh()
        {
            LoadSktChungTus();
        }

        private void SearchData()
        {
            if (_sktChungTuModelsView != null)
                _sktChungTuModelsView.Refresh();
        }

        private void UnCheckBoxAll()
        {
            foreach (var item in Items)
            {
                item.Selected = false;
            }
        }

        /// <summary>
        /// Open Detail
        /// </summary>
        /// <param name="sktChungTuModel"></param>
        private void OpenDetailDialog(NsSktChungTuModel nsSktDetail, params bool[] isNew)
        {
            var idDonViCurrent = _sessionInfo.IdDonVi;
            var chungTuTH = Items.FirstOrDefault(item => item.IIdMaDonVi.Equals(idDonViCurrent));
            DemandDetailViewModel.Model = ObjectCopier.Clone(nsSktDetail);
            DemandDetailViewModel.CtTongHop = chungTuTH;
            DemandDetailViewModel.IsVoucherSummary = nsSktDetail.IIdMaDonVi.Equals(idDonViCurrent) && !string.IsNullOrEmpty(nsSktDetail.SDssoChungTuTongHop);
            DemandDetailViewModel.LstCanCu = null;
            DemandDetailViewModel.ShowColNSBD = nsSktDetail.ILoaiChungTu.Equals(int.Parse(VoucherType.NSBD_Key)) ? Visibility.Visible : Visibility.Collapsed;
            DemandDetailViewModel.ShowColNSSD = nsSktDetail.ILoaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key)) ? Visibility.Visible : Visibility.Collapsed;
            if (isNew.Length > 0 && !isNew[0])
            {
                DemandDetailViewModel.IndexDataState = DataStateValue.CO_SO_LIEU_DT_QT_SKT;
            }
            else
            {
                DemandDetailViewModel.IndexDataState = DataStateValue.HIEN_THI_TAT_CA;
            }
            DemandDetailViewModel.Init();
            var view = new DemandDetail() { DataContext = DemandDetailViewModel };
            view.ShowDialog();
        }

        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }

        public override void Init()
        {
            base.Init();
            _tabIndex = ImportTabIndex.Data;
            _sessionInfo = _sessionService.Current;
            LoadLockStatus();
            LoadOrderTypes();
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            LoadVoucherTypes();
            LoadNsDonVi();
            DemandDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
            LoadBudgetSourceTypes();
        }

        private void ConfirmAggregate()
        {
            List<NsSktChungTuModel> selectedSktChungTus = Items.Where(x => x.Selected && !x.IsSummaryVocher).ToList();
            bool checkAllowAggregate = selectedSktChungTus.All(x => x.BKhoa);
            if (checkAllowAggregate)
            {
                OnAggregate();
            }
            else
            {
                string message = Resources.ConfirmAggregate;
                MessageBoxResult result = MessageBoxHelper.Confirm(message);
                if (result == MessageBoxResult.Yes)
                    OnAggregate();
            }
        }

        private void OnAggregate()
        {
            bool existTongHop = false;
            if (TabIndex == ImportTabIndex.Data)
            {
                existTongHop = _sktChungTuService.IsExistChungTuTongHop(1, _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget);
            }
            else
            {
                existTongHop = _sktChungTuService.IsExistChungTuTongHop(2, _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget);
            }
            if (existTongHop)
            {
                MessageBoxResult result = MessageBoxHelper.Confirm("Đã tồn tại bản ghi chứng từ tổng hợp, bạn có muốn thay thế không?");
                if (result != MessageBoxResult.Yes)
                    return;
            }
            //kiểm tra trạng thái các bản ghi
            if (!_sessionService.Current.IsQuanLyDonViCha)
            {
                MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                return;
            }
            List<NsSktChungTuModel> selectedSktChungTus = Items.Where(x => x.Selected && x.BKhoa && !x.IsSummaryVocher).ToList();

            DemandDialogViewModel.Name = "Thêm chứng từ";
            DemandDialogViewModel.Description = "Tạo mới chứng từ tổng hợp";
            DemandDialogViewModel.NsSktChungTuModel = new NsSktChungTuModel();
            DemandDialogViewModel.isSummary = true;
            DemandDialogViewModel.ListIdsSktChungTuSummary = selectedSktChungTus;
            DemandDialogViewModel.Init();
            DemandDialogViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.MLNS;
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((NsSktChungTuModel)obj);
            };
            var addView = new DemandDialog() { DataContext = DemandDialogViewModel };
            DialogHost.Show(addView, DemandCheckScreen.ROOT_DIALOG, null, ClosingEventHandler);
        }

        private void OnAggregateEdit()
        {
            //kiểm tra trạng thái các bản ghi
            List<NsSktChungTuModel> selectedSktChungTus = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedNsSktChungTuModel.SDssoChungTuTongHop) && SelectedNsSktChungTuModel.SDssoChungTuTongHop.Contains(x.SSoChungTu)).ToList();

            DemandDialogViewModel.Name = "Sửa chứng từ";
            DemandDialogViewModel.Description = "Sửa chứng từ tổng hợp";
            DemandDialogViewModel.NsSktChungTuModel = SelectedNsSktChungTuModel;
            DemandDialogViewModel.isSummary = true;
            DemandDialogViewModel.ListIdsSktChungTuSummary = selectedSktChungTus;
            DemandDialogViewModel.Init();
            DemandDialogViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((NsSktChungTuModel)obj);
            };
            var addView = new DemandDialog() { DataContext = DemandDialogViewModel };
            DialogHost.Show(addView, DemandCheckScreen.ROOT_DIALOG, null, ClosingEventHandler);
        }



        /// <summary>
        /// Xuất excel chứng từ tổng hợp
        /// </summary>
        private void OnExportAggregateData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;


                    //List<NsSktChungTuModel> sktChungTuModelsSummary = Items.Where(x => x.IsSummaryVocher).ToList();
                    List<NsSktChungTuModel> sktChungTuModelsSummary = Items.Where(x => x.IIdMaDonVi == _selectedNsSktChungTuModel.IIdMaDonVi && x.ILoai == _selectedNsSktChungTuModel.ILoai).ToList();

                    var yearOfWork = _sessionInfo.YearOfWork;
                    var predicateChcc = PredicateBuilder.True<NsCauHinhCanCu>();
                    predicateChcc = predicateChcc.And(item => item.SModule == TypeModuleCanCu.DEMAND);
                    predicateChcc = predicateChcc.And(item => item.INamLamViec == yearOfWork);
                    var listCanCu = _iCauHinhCanCuService.FindByCondition(predicateChcc).OrderBy(n => n.INamCanCu);
                    var cauHinhCanCu = _mapper.Map<ObservableCollection<CauHinhCanCuModel>>(listCanCu);

                    foreach (var item in sktChungTuModelsSummary)
                    {
                        string X1 = "", X2 = "", X3 = "", X4 = "", X5 = "";
                        int count = 0;
                        var currentDonVi = GetNsDonViOfCurrentUser();
                        List<NsSktChungTuChiTietModel> sktChungTuChiTietModels = GetDemandVoucherDetail(item);
                        var predicate = PredicateBuilder.True<NsSktMucLuc>();
                        predicate = predicate.And(x => x.INamLamViec == yearOfWork);
                        predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
                        List<NsSktMucLuc> sktMucLucs = _SktMucLucService.FindByCondition(predicate).ToList();
                        var sktMucLucsOrder = from sktMucLuc in sktMucLucs
                                              orderby sktMucLuc.SKyHieu
                                              select sktMucLuc;
                        foreach (var ct in sktChungTuChiTietModels)
                        {
                            var ml = sktMucLucsOrder.FirstOrDefault(x => x.IIDMLSKT.Equals(ct.IIdMlskt));
                            if (ml != null)
                            {
                                ct.Nganh = ml.SNg;
                                ct.NganhParent = ml.SNGCha;
                                ct.Stt = ml.SSTT;
                            }
                        }
                        foreach (var element in cauHinhCanCu)
                        {
                            var predicateCc = PredicateBuilder.True<NsSktChungTuChiTietCanCu>();
                            predicateCc = predicateCc.And(x => x.IiIdCtsoKiemTra.Equals(item.Id));
                            predicateCc = predicateCc.And(x => x.IIdCanCu.Equals(element.Id));
                            var lstCanCu = _iSktChungTuChiTietCanCuService.FindByCondition(predicateCc).ToList();

                            if (count == 0)
                            {
                                X1 = element.STenCot;
                            }
                            if (count == 1)
                            {
                                X2 = element.STenCot;
                            }
                            if (count == 2)
                            {
                                X3 = element.STenCot;
                            }
                            if (count == 3)
                            {
                                X4 = element.STenCot;
                            }
                            if (count == 4)
                            {
                                X5 = element.STenCot;
                            }

                            /* Hàm tối ưu
                            foreach (var cc in lstCanCu)
                            {
                                var mucLuc = sktChungTuChiTietModels.FirstOrDefault(x => x.SKyHieu.Equals(cc.SKyHieu));
                                if (mucLuc != null)
                                {
                                    for (int i = 0; i < 5; i++)
                                    {
                                        var xProperty = typeof(NsSktChungTuChiTietModel).GetProperty($"X{i + 1}");
                                        xProperty.SetValue(mucLuc, new NsSktChungTuChiTietModel.ChiTietCanCu
                                        {
                                            SoTien = cc.FTuChi,
                                            SoTienMHHV = cc.FMuaHangCapHienVat,
                                            SoTienDT = cc.FPhanCap
                                        });
                                    }
                                }
                            }
                            */


                            foreach (var cc in lstCanCu)
                            {
                                // var mucLuc = Items.FirstOrDefault(x => x.IIdMlskt.Equals(cc.IIdMlskt));
                                var mucLuc = sktChungTuChiTietModels.FirstOrDefault(x => x.SKyHieu.Equals(cc.SKyHieu));
                                if (mucLuc != null)
                                {
                                    if (count == 0)
                                    {
                                        // Lay so lieu
                                        mucLuc.X1.SoTien = cc.FTuChi;
                                        mucLuc.X1.SoTienMHHV = cc.FMuaHangCapHienVat;
                                        mucLuc.X1.SoTienDT = cc.FPhanCap;
                                    }

                                    if (count == 1)
                                    {
                                        // Lay so lieu
                                        mucLuc.X2.SoTien = cc.FTuChi;
                                        mucLuc.X2.SoTienMHHV = cc.FMuaHangCapHienVat;
                                        mucLuc.X2.SoTienDT = cc.FPhanCap;
                                    }

                                    if (count == 2)
                                    {
                                        // Lay so lieu
                                        mucLuc.X3.SoTien = cc.FTuChi;
                                        mucLuc.X3.SoTienMHHV = cc.FMuaHangCapHienVat;
                                        mucLuc.X3.SoTienDT = cc.FPhanCap;
                                    }

                                    if (count == 3)
                                    {
                                        // Lay so lieu
                                        mucLuc.X4.SoTien = cc.FTuChi;
                                        mucLuc.X4.SoTienMHHV = cc.FMuaHangCapHienVat;
                                        mucLuc.X4.SoTienDT = cc.FPhanCap;
                                    }

                                    if (count == 4)
                                    {
                                        // Lay so lieu
                                        mucLuc.X5.SoTien = cc.FTuChi;
                                        mucLuc.X5.SoTienMHHV = cc.FMuaHangCapHienVat;
                                        mucLuc.X5.SoTienDT = cc.FPhanCap;
                                    }
                                }
                            }
                            count++;

                        }
                        CalculateData(sktChungTuChiTietModels);
                        sktChungTuChiTietModels = sktChungTuChiTietModels.Where(item => item.X1.SoTien > 0 || item.X2.SoTien > 0 ||
                                                                                item.X3.SoTien > 0 || item.X4.SoTien > 0 || item.X5.SoTien > 0 ||
                                                                                item.FHuyDongTonKho > 0 || item.FTuChi > 0 || item.FMuaHangCapHienVat > 0 ||
                                                                                item.FPhanCap > 0 || item.FTonKhoDenNgay > 0 ||
                                                                                item.X1.SoTienMHHV > 0 || item.X1.SoTienDT > 0 ||
                                                                                item.X2.SoTienMHHV > 0 || item.X2.SoTienDT > 0 ||
                                                                                item.X3.SoTienMHHV > 0 || item.X3.SoTienDT > 0 ||
                                                                                item.X4.SoTienMHHV > 0 || item.X4.SoTienDT > 0 ||
                                                                                item.X5.SoTienMHHV > 0 || item.X5.SoTienDT > 0
                                                                                ).ToList();


                        //NSSD
                        double SumTotalTonKhoDenNgay = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTonKhoDenNgay);
                        double SumTotalHuyDong = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FHuyDongTonKho);
                        double SumTotalTuChi = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTuChi);
                        double SumTotalTongCongNSSD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongHuyDongTuChi);
                        //NSBD
                        double SumTotalMuaHangHienVat = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FMuaHangCapHienVat);
                        double SumTotalDacThu = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FPhanCap);
                        double SumTotalTongCongNSBD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongMuaHangHienVatDacThu);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        List<int> columnHidden = new List<int>();
                        if (item.ILoaiChungTu == 1)
                        {
                            if (string.IsNullOrEmpty(X1))
                            {
                                columnHidden.Add(5);
                            }
                            if (string.IsNullOrEmpty(X2))
                            {
                                columnHidden.Add(6);
                            }
                            if (string.IsNullOrEmpty(X3))
                            {
                                columnHidden.Add(7);
                            }
                            if (string.IsNullOrEmpty(X4))
                            {
                                columnHidden.Add(8);
                            }
                            if (string.IsNullOrEmpty(X5))
                            {
                                columnHidden.Add(9);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(X1))
                            {
                                columnHidden.AddRange(new List<int> { 7, 8 });
                            }
                            if (string.IsNullOrEmpty(X2))
                            {
                                columnHidden.AddRange(new List<int> { 9, 10 });
                            }
                            if (string.IsNullOrEmpty(X3))
                            {
                                columnHidden.AddRange(new List<int> { 11, 12 });
                            }
                            if (string.IsNullOrEmpty(X4))
                            {
                                columnHidden.AddRange(new List<int> { 13, 14 });
                            }
                            if (string.IsNullOrEmpty(X5))
                            {
                                columnHidden.AddRange(new List<int> { 15, 16 });
                            }
                        }


                        data.Add("X1", X1);
                        data.Add("X2", X2);
                        data.Add("X3", X3);
                        data.Add("X4", X4);
                        data.Add("X5", X5);
                        data.Add("SoChungTu", item.SSoChungTu);
                        data.Add("TenDonVi", item.STenDonVi);
                        data.Add("IdDonVi", item.IIdMaDonVi);
                        data.Add("Cap1", currentDonVi.TenDonVi);
                        data.Add("TieuDe1", "BÁO CÁO CHI TIẾT SỐ NHU CẦU NGÂN SÁCH NĂM " + _sessionInfo.YearOfWork);
                        data.Add("h2", "Lữ đoàn X");
                        data.Add("h1", "Lữ đoàn X");
                        data.Add("LoaiChungTu", item.ILoaiChungTu == 1 ? VoucherType.NSSD_Value : VoucherType.NSBD_Value);
                        data.Add("MoTa", item.SMoTa);
                        data.Add("NgayChungTu", item.DNgayChungTu.GetValueOrDefault(DateTime.Now).ToString("dd/MM/yyyy"));
                        data.Add("NguoiTao", item.SNguoiTao);
                        data.Add("NgayTao", item.DNgayTao.GetValueOrDefault(new DateTime()).ToString("dd/MM/yyyy"));
                        data.Add("SumTotalTonKhoDenNgay", SumTotalTonKhoDenNgay);
                        data.Add("SumTotalHuyDong", SumTotalHuyDong);
                        data.Add("SumTotalTuChi", SumTotalTuChi);
                        data.Add("SumTotalMHHV", SumTotalMuaHangHienVat);
                        data.Add("SumTotalDT", SumTotalDacThu);
                        data.Add("SumTotalTongCongNSSD", SumTotalTongCongNSSD);
                        data.Add("SumTotalTongCongNSBD", SumTotalTongCongNSBD);
                        data.Add("SumTotalTonKho", sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTonKhoDenNgay));
                        data.Add("ListData", sktChungTuChiTietModels);
                        data.Add("Count", 10000);
                        data.Add("SKTML", sktMucLucsOrder);
                        data.Add("TonKhoDenNgay", item.ILoaiChungTu == 1 ? "Tồn kho đến ngày 01/01/" + (_sessionInfo.YearOfWork - 1) : "Giá trị hàng hóa tồn kho 01/01/" + (_sessionInfo.YearOfWork - 1));
                        data.Add("KhungNganSachDuocDuyet", "Khung ngân sách được duyệt năm " + _sessionInfo.YearOfWork);
                        data.Add("SoNganhPhanCap", "Số ngành đã phân cấp theo khung ngân sách năm " + _sessionInfo.YearOfWork);
                        //templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSSD);
                        if (item.ILoaiChungTu == 1)
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSSD);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSBD);
                        }
                        fileNamePrefix = item.SSoChungTu;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.HiddenExport<NsSktChungTuChiTietModel, NsSktMucLuc>(templateFileName, data, columnHidden, null, true);
                        var nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);

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
        #region Code Gửi dữ liệu quản lý ngân sách
        /// <summary>
        /// Code by Gửi dữ liệu Quản lý ngân sách.
        /// </summary>
        /// <returns></returns>
        /// 


        private async void OnUploadDialog(bool isSendHTTP)
        {
            if (!Items.Any(n => n.Selected) || Items.Where(n => n.Selected).Count() > 1)
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng chọn 1 bản ghi !");
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
            } catch (ConfigurationErrorsException ex)
            {
                IsLoading = false;
                new NSMessageBoxViewModel("Cấu hình sai đường dẫn hoặc cổng HTTP").ShowDialogHost();
                return;
            }

            SendDataDemandViewModel._selectedNsSktChungTuModel = _selectedNsSktChungTuModel;
            SendDataDemandViewModel.Items = Items;
            SendDataDemandViewModel.IsSendHTTP = isSendHTTP;
            SendDataDemandViewModel.Init();
            SendDataDemandViewModel.ClosePopup += RefreshAfterClosePopupSendData;
            var addView = new View.Budget.DemandCheck.Demand.SendDataDemand.SendDataDemand() { DataContext = SendDataDemandViewModel };
            IsLoading = false;
            DialogHost.Show(addView, SettlementScreen.ROOT_DIALOG, null, null);
        }

        private void RefreshAfterClosePopupSendData(object sender, EventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            OnRefresh();
        }

        //private async void OnUpload()
        //{
        //    if (!Items.Any(n => n.Selected) || Items.Where(n => n.Selected).Count() > 1)
        //    {
        //        StringBuilder messageBuilder = new StringBuilder();
        //        messageBuilder.AppendFormat("Vui lòng chọn 1 bản ghi !");
        //        MessageBox.Show(messageBuilder.ToString());
        //        return;
        //    }
        //    try
        //    {
        //        List<ExportResult> results = new List<ExportResult>();
        //        string templateFileName;
        //        string fileNamePrefix;
        //        string fileNameWithoutExtension;
        //        List<NsSktChungTuModel> sktChungTuModelsSummary = Items.Where(x => x.IIdMaDonVi == _selectedNsSktChungTuModel.IIdMaDonVi && x.ILoai == _selectedNsSktChungTuModel.ILoai && x.Selected).ToList();
        //        var yearOfWork = _sessionInfo.YearOfWork;
        //        var predicateChcc = PredicateBuilder.True<NsCauHinhCanCu>();
        //        predicateChcc = predicateChcc.And(item => item.SModule == TypeModuleCanCu.DEMAND);
        //        predicateChcc = predicateChcc.And(item => item.INamLamViec == yearOfWork);
        //        var listCanCu = _iCauHinhCanCuService.FindByCondition(predicateChcc).OrderBy(n => n.INamCanCu);
        //        var cauHinhCanCu = _mapper.Map<ObservableCollection<CauHinhCanCuModel>>(listCanCu);
        //        if (sktChungTuModelsSummary.Any())
        //        {
        //            foreach (var item in sktChungTuModelsSummary)
        //            {
        //                string X1 = "", X2 = "", X3 = "", X4 = "", X5 = "";
        //                int count = 0;
        //                var currentDonVi = GetNsDonViOfCurrentUser();
        //                List<NsSktChungTuChiTietModel> sktChungTuChiTietModels = GetDemandVoucherDetail(item);
        //                var predicate = PredicateBuilder.True<NsSktMucLuc>();
        //                predicate = predicate.And(x => x.INamLamViec == yearOfWork);
        //                predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
        //                List<NsSktMucLuc> sktMucLucs = _SktMucLucService.FindByCondition(predicate).ToList();
        //                string token = await _hTTPUploadFileService.GetToken();
        //                string salt = _cryptographyService.GetSalt();
        //                string tokenKey = Scramble(token + salt);
        //                var sktMucLucsOrder = from sktMucLuc in sktMucLucs
        //                                      orderby sktMucLuc.SKyHieu
        //                                      select sktMucLuc;
        //                foreach (var ct in sktChungTuChiTietModels)
        //                {
        //                    var ml = sktMucLucsOrder.FirstOrDefault(x => x.IIDMLSKT.Equals(ct.IIdMlskt));
        //                    if (ml != null)
        //                    {
        //                        ct.Nganh = ml.SNg;
        //                        ct.NganhParent = ml.SNGCha;
        //                        ct.Stt = ml.SSTT;
        //                    }
        //                }
        //                foreach (var element in cauHinhCanCu)
        //                {
        //                    var predicateCc = PredicateBuilder.True<NsSktChungTuChiTietCanCu>();
        //                    predicateCc = predicateCc.And(x => x.IiIdCtsoKiemTra.Equals(item.Id));
        //                    predicateCc = predicateCc.And(x => x.IIdCanCu.Equals(element.Id));
        //                    var lstCanCu = _iSktChungTuChiTietCanCuService.FindByCondition(predicateCc).ToList();

        //                    if (count == 0)
        //                    {
        //                        X1 = element.STenCot;
        //                    }
        //                    if (count == 1)
        //                    {
        //                        X2 = element.STenCot;
        //                    }
        //                    if (count == 2)
        //                    {
        //                        X3 = element.STenCot;
        //                    }
        //                    if (count == 3)
        //                    {
        //                        X4 = element.STenCot;
        //                    }
        //                    if (count == 4)
        //                    {
        //                        X5 = element.STenCot;
        //                    }


        //                    foreach (var cc in lstCanCu)
        //                    {
        //                        // var mucLuc = Items.FirstOrDefault(x => x.IIdMlskt.Equals(cc.IIdMlskt));
        //                        var mucLuc = sktChungTuChiTietModels.FirstOrDefault(x => x.SKyHieu.Equals(cc.SKyHieu));
        //                        if (mucLuc != null)
        //                        {
        //                            if (count == 0)
        //                            {
        //                                // Lay so lieu
        //                                mucLuc.X1.SoTien = cc.FTuChi;
        //                                mucLuc.X1.SoTienMHHV = cc.FMuaHangCapHienVat;
        //                                mucLuc.X1.SoTienDT = cc.FPhanCap;
        //                            }

        //                            if (count == 1)
        //                            {
        //                                // Lay so lieu
        //                                mucLuc.X2.SoTien = cc.FTuChi;
        //                                mucLuc.X2.SoTienMHHV = cc.FMuaHangCapHienVat;
        //                                mucLuc.X2.SoTienDT = cc.FPhanCap;
        //                            }

        //                            if (count == 2)
        //                            {
        //                                // Lay so lieu
        //                                mucLuc.X3.SoTien = cc.FTuChi;
        //                                mucLuc.X3.SoTienMHHV = cc.FMuaHangCapHienVat;
        //                                mucLuc.X3.SoTienDT = cc.FPhanCap;
        //                            }

        //                            if (count == 3)
        //                            {
        //                                // Lay so lieu
        //                                mucLuc.X4.SoTien = cc.FTuChi;
        //                                mucLuc.X4.SoTienMHHV = cc.FMuaHangCapHienVat;
        //                                mucLuc.X4.SoTienDT = cc.FPhanCap;
        //                            }

        //                            if (count == 4)
        //                            {
        //                                // Lay so lieu
        //                                mucLuc.X5.SoTien = cc.FTuChi;
        //                                mucLuc.X5.SoTienMHHV = cc.FMuaHangCapHienVat;
        //                                mucLuc.X5.SoTienDT = cc.FPhanCap;
        //                            }
        //                        }
        //                    }
        //                    count++;

        //                }
        //                CalculateData(sktChungTuChiTietModels);
        //                sktChungTuChiTietModels = sktChungTuChiTietModels.Where(item => item.X1.SoTien > 0 || item.X2.SoTien > 0 ||
        //                                                                        item.X3.SoTien > 0 || item.X4.SoTien > 0 || item.X5.SoTien > 0 ||
        //                                                                        item.FHuyDongTonKho > 0 || item.FTuChi > 0 || item.FMuaHangCapHienVat > 0 ||
        //                                                                        item.FPhanCap > 0 || item.FTonKhoDenNgay > 0 ||
        //                                                                        item.X1.SoTienMHHV > 0 || item.X1.SoTienDT > 0 ||
        //                                                                        item.X2.SoTienMHHV > 0 || item.X2.SoTienDT > 0 ||
        //                                                                        item.X3.SoTienMHHV > 0 || item.X3.SoTienDT > 0 ||
        //                                                                        item.X4.SoTienMHHV > 0 || item.X4.SoTienDT > 0 ||
        //                                                                        item.X5.SoTienMHHV > 0 || item.X5.SoTienDT > 0
        //                                                                        ).ToList();
        //                //NSSD
        //                double SumTotalHuyDong = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FHuyDongTonKho);
        //                double SumTotalTuChi = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTuChi);
        //                double SumTotalTongCongNSSD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongHuyDongTuChi);
        //                //NSBD
        //                double SumTotalMuaHangHienVat = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FMuaHangCapHienVat);
        //                double SumTotalDacThu = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FPhanCap);
        //                double SumTotalTongCongNSBD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongMuaHangHienVatDacThu);

        //                Dictionary<string, object> data = new Dictionary<string, object>();
        //                List<int> columnHidden = new List<int>();
        //                if (item.ILoaiChungTu == 1)
        //                {
        //                    if (string.IsNullOrEmpty(X1))
        //                    {
        //                        columnHidden.Add(5);
        //                    }
        //                    if (string.IsNullOrEmpty(X2))
        //                    {
        //                        columnHidden.Add(6);
        //                    }
        //                    if (string.IsNullOrEmpty(X3))
        //                    {
        //                        columnHidden.Add(7);
        //                    }
        //                    if (string.IsNullOrEmpty(X4))
        //                    {
        //                        columnHidden.Add(8);
        //                    }
        //                    if (string.IsNullOrEmpty(X5))
        //                    {
        //                        columnHidden.Add(9);
        //                    }
        //                }
        //                else
        //                {
        //                    if (string.IsNullOrEmpty(X1))
        //                    {
        //                        columnHidden.AddRange(new List<int> { 7, 8 });
        //                    }
        //                    if (string.IsNullOrEmpty(X2))
        //                    {
        //                        columnHidden.AddRange(new List<int> { 9, 10 });
        //                    }
        //                    if (string.IsNullOrEmpty(X3))
        //                    {
        //                        columnHidden.AddRange(new List<int> { 11, 12 });
        //                    }
        //                    if (string.IsNullOrEmpty(X4))
        //                    {
        //                        columnHidden.AddRange(new List<int> { 13, 14 });
        //                    }
        //                    if (string.IsNullOrEmpty(X5))
        //                    {
        //                        columnHidden.AddRange(new List<int> { 15, 16 });
        //                    }
        //                }
        //                data.Add("X1", X1);
        //                data.Add("X2", X2);
        //                data.Add("X3", X3);
        //                data.Add("X4", X4);
        //                data.Add("X5", X5);
        //                data.Add("SoChungTu", item.SSoChungTu);
        //                data.Add("TenDonVi", item.STenDonVi);
        //                data.Add("IdDonVi", item.IIdMaDonVi);
        //                data.Add("Cap1", currentDonVi.TenDonVi);
        //                data.Add("TieuDe1", "BÁO CÁO CHI TIẾT SỐ NHU CẦU NGÂN SÁCH NĂM " + _sessionInfo.YearOfWork);
        //                data.Add("h2", "Lữ đoàn X");
        //                data.Add("h1", "Lữ đoàn X");
        //                data.Add("LoaiChungTu", item.ILoaiChungTu == 1 ? VoucherType.NSSD_Value : VoucherType.NSBD_Value);
        //                data.Add("MoTa", item.SMoTa);
        //                data.Add("NgayChungTu", item.DNgayChungTu.GetValueOrDefault(DateTime.Now).ToString("dd/MM/yyyy"));
        //                data.Add("NguoiTao", item.SNguoiTao);
        //                data.Add("NgayTao", item.DNgayTao.GetValueOrDefault(new DateTime()).ToString("dd/MM/yyyy"));
        //                data.Add("SumTotalHuyDong", SumTotalHuyDong);
        //                data.Add("SumTotalTuChi", SumTotalTuChi);
        //                data.Add("SumTotalMHHV", SumTotalMuaHangHienVat);
        //                data.Add("SumTotalDT", SumTotalDacThu);
        //                data.Add("SumTotalTongCongNSSD", SumTotalTongCongNSSD);
        //                data.Add("SumTotalTongCongNSBD", SumTotalTongCongNSBD);
        //                data.Add("SumTotalTonKho", sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTonKhoDenNgay));
        //                data.Add("ListData", sktChungTuChiTietModels);
        //                data.Add("SKTML", sktMucLucsOrder);
        //                data.Add("TonKhoDenNgay", "Tồn kho đến ngày 1/1/" + (_sessionInfo.YearOfWork - 1));
        //                //templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSSD);
        //                if (item.ILoaiChungTu == 1)
        //                {
        //                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSSD);
        //                }
        //                else
        //                {
        //                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSBD);
        //                }
        //                fileNamePrefix = item.SSoChungTu;
        //                fileNameWithoutExtension = StringUtils.ConvertVN(StringUtils.CreateExportFileName(fileNamePrefix));
        //                var xlsFile = _exportService.HiddenExport<NsSktChungTuChiTietModel, NsSktMucLuc>(templateFileName, data, columnHidden);
        //                var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        //                var fileStream = new MemoryStream();
        //                var outputFileStream = new MemoryStream();
        //                _exportService.Open(Result, ref fileStream);

        //                await _cryptographyService.EncryptFile(fileStream, ref outputFileStream, tokenKey);
        //                await _hTTPUploadFileService.UploadFile(new FileUploadStreamModel()
        //                {
        //                    File = outputFileStream,
        //                    Name = fileNameWithoutExtension + FileExtensionFormats.Security,
        //                    Description = "Chứng từ tổng hợp",
        //                    Module = NSFunctionCode.BUDGET,
        //                    ModuleName = "Số nhu cầu - Kiểm tra",
        //                    SubModule = item.ILoaiChungTu == 1 ? NSFunctionCode.BUDGET_USE_DEMANDCHECK_PLAN : NSFunctionCode.BUDGET_GUARANTEE_DEMANDCHECK_PLAN,
        //                    SubModuleName = "Nhập số nhu cầu đơn vị",
        //                    TokenKey = tokenKey,
        //                    YearOfWork = _sessionInfo.YearOfWork,
        //                    YearOfBudget = _sessionInfo.YearOfBudget,
        //                    SourceOfBudget = _sessionInfo.Budget,
        //                    Department = "",
        //                    Quarter = ""
        //                });

        //            }
        //            StringBuilder messageBuilderSuccess = new StringBuilder();
        //            messageBuilderSuccess.AppendFormat("Gửi dữ liệu thành công");
        //            MessageBox.Show(messageBuilderSuccess.ToString());
        //            return;
        //        }
        //        StringBuilder messageBuilder = new StringBuilder();
        //        messageBuilder.AppendFormat("Không có dữ liệu gửi");
        //        MessageBox.Show(messageBuilder.ToString());
        //        return;

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message, ex);
        //        StringBuilder messageBuilder = new StringBuilder();
        //        messageBuilder.AppendFormat("Gửi dữ liệu thất bại");
        //        MessageBox.Show(messageBuilder.ToString());
        //        return;
        //    }
        //}

        #endregion
        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private string Scramble(string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }

        private List<NsSktChungTuChiTietModel> GetDemandVoucherDetail(NsSktChungTuModel nsSktChungTuModel)
        {
            var loaiChungTu = nsSktChungTuModel.ILoaiChungTu.GetValueOrDefault(-1);
            SktChungTuChiTietCriteria searchCondition = new SktChungTuChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            searchCondition.NamNganSach = _sessionInfo.YearOfBudget;
            searchCondition.NguonNganSach = _sessionInfo.Budget;
            searchCondition.ITrangThai = StatusType.ACTIVE;
            searchCondition.SktChungTuId = nsSktChungTuModel.Id;
            searchCondition.ILoai = nsSktChungTuModel.ILoai;
            searchCondition.IdDonVi = nsSktChungTuModel.IIdMaDonVi;
            searchCondition.CurrentIdDonVi = _sessionInfo.IdDonVi;
            searchCondition.LoaiChungTu = loaiChungTu;
            searchCondition.UserName = _sessionInfo.Principal;
            var temp = _sktChungTuChiTietService.FindByConditionForChildUnit(searchCondition);
            var lstChungTuChiTietModels = _mapper.Map<List<NsSktChungTuChiTietModel>>(temp);
            CalculateData(lstChungTuChiTietModels);
            //lstChungTuChiTietModels = lstChungTuChiTietModels.Where(item => item.FHuyDongTonKho > 0 || item.FTuChi > 0 || item.FMuaHangCapHienVat > 0 || item.FPhanCap > 0 || item.FTonKhoDenNgay > 0).ToList();
            return lstChungTuChiTietModels;
        }

        private void CalculateData(List<NsSktChungTuChiTietModel> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHuyDongTonKho = 0;
                    x.FMuaHangCapHienVat = 0;
                    x.FKhungNganSachDuocDuyet = 0;
                    x.FSoNganhPhanCap = 0;
                    x.FPhanCap = 0;
                    x.FTonKhoDenNgay = 0;
                    x.X1 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X2 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X3 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X4 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X5 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.IsHangCha && !x.IsDeleted);
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, lstSktChungTuChiTiet);
            }

        }

        private void CalculateParent(Guid idParent, NsSktChungTuChiTietModel item, List<NsSktChungTuChiTietModel> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            model.FTuChi += item.FTuChi;
            model.FHuyDongTonKho += item.FHuyDongTonKho;
            model.FMuaHangCapHienVat += item.FMuaHangCapHienVat;
            model.FPhanCap += item.FPhanCap;
            model.FTonKhoDenNgay += item.FTonKhoDenNgay;
            model.FKhungNganSachDuocDuyet += item.FKhungNganSachDuocDuyet;
            model.FSoNganhPhanCap += item.FSoNganhPhanCap;
            model.X1.SoTien += item.X1.SoTien;
            model.X1.SoTienMHHV += item.X1.SoTienMHHV;
            model.X1.SoTienDT += item.X1.SoTienDT;
            model.X2.SoTien += item.X2.SoTien;
            model.X2.SoTienMHHV += item.X2.SoTienMHHV;
            model.X2.SoTienDT += item.X2.SoTienDT;
            model.X3.SoTien += item.X3.SoTien;
            model.X3.SoTienMHHV += item.X3.SoTienMHHV;
            model.X3.SoTienDT += item.X3.SoTienDT;
            model.X4.SoTien += item.X4.SoTien;
            model.X4.SoTienMHHV += item.X4.SoTienMHHV;
            model.X4.SoTienDT += item.X4.SoTienDT;
            model.X5.SoTien += item.X5.SoTien;
            model.X5.SoTienMHHV += item.X5.SoTienMHHV;
            model.X5.SoTienDT += item.X5.SoTienDT;
            CalculateParent(model.IdParent, item, lstSktChungTuChiTiet);
        }

        private void OnExportDataFilter()
        {
            ExportDemandViewModel.Init();
            var modelTemp = Items.FirstOrDefault(x => x.SSoChungTu == SelectedNsSktChungTuModel.SSoChungTu && x.IIdMaDonVi == _selectedNsSktChungTuModel.IIdMaDonVi && x.ILoai == _selectedNsSktChungTuModel.ILoai);
            ExportDemandViewModel.Model = modelTemp;
            if (!string.IsNullOrEmpty(modelTemp.SDssoChungTuTongHop))
            {
                ExportDemandViewModel.ListChildrenModel = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(x.SSoChungTu) && modelTemp.SDssoChungTuTongHop.Contains(x.SSoChungTu)).ToList();
            }

            var addView = new View.Budget.DemandCheck.Demand.ExportDemand.ExportDemand() { DataContext = ExportDemandViewModel };
            DialogHost.Show(addView, DivisionScreen.ROOT_DIALOG, null, null);
        }

        private void OnImportData()
        {
            ImportDemandViewModel.Init();
            ImportDemandViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((NsSktChungTuModel)obj);
            };
            ImportDemandViewModel.ShowDialog();
        }

        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(IsAllItemsSelected));
        }

        public void OnChangeVisibilityColumn()
        {
            OnPropertyChanged(nameof(ShowColNSBD));
            OnPropertyChanged(nameof(ShowColNSSD));
        }
    }
}