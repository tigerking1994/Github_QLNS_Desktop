using AutoMapper;
using FlexCel.Core;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Demand3Y;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.FunctionMap.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand3Y
{
    public class Demand3YIndexViewModel : GridViewModelBase<NsSktChungTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly INc3YChungTuChiTietService _nc3YChungTuChiTietService;
        private readonly ISktMucLucService _SktMucLucService;
        private readonly ICauHinhCanCuService _iCauHinhCanCuService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
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
        public override Type ContentType => typeof(Demand3YIndex);

        public override string Description => "Chứng từ nhập số nhu cầu ngân sách 3 năm (" +
                                              _sessionInfo.YearOfWork + " - " + (_sessionInfo.YearOfWork + 1) + " - " + (_sessionInfo.YearOfWork + 2) + ")";
        public override string Name => "Nhu cầu NS 3 năm";
        public string ColName1 => $"NC năm {_sessionInfo.YearOfWork}";
        public string ColName2 => $"NC năm {_sessionInfo.YearOfWork + 1}";
        public string ColName3 => $"NC năm {_sessionInfo.YearOfWork + 2}";
        public string ColName4 => $"Tổng nhu cầu";
        public override PackIconKind IconKind => PackIconKind.RhombusOutline;

        public Demand3YDialogViewModel Demand3YDialogViewModel { get; }
        public PrintReportDemandOrgViewModel PrintReportDemandOrgViewModel { get; }
        public Demand3YDetailViewModel Demand3YDetailViewModel { get; }
        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand ExportAggregateDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportDataFilterCommand { get; }


        public Demand3YIndexViewModel(
            ISktChungTuService sktChungTuService,
            INc3YChungTuChiTietService nc3YChungTuChiTietService,
            INsDonViService nsDonViService,
            ILog logger,
            IMapper mapper,
            ISktMucLucService sktMucLucService,
            IExportService exportService,
            Demand3YDialogViewModel demand3YDialogViewModel,
            ISessionService sessionService,
            INsDonViService donViService,
            ICauHinhCanCuService iCauHinhCanCuService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            PrintReportDemandOrgViewModel printReportDemandOrgViewModel,
            Demand3YDetailViewModel demand3YDetailViewModel,
            ISysAuditLogService log,
            IDanhMucService danhMucService,
            IVdtFtpRootService ftpService,
            FtpStorageService ftpStorageService,
            IHTTPUploadFileService hTTPUploadFileService,
            ICryptographyService cryptographyService)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _sktChungTuService = sktChungTuService;
            _nc3YChungTuChiTietService = nc3YChungTuChiTietService;
            _iCauHinhCanCuService = iCauHinhCanCuService;
            _nsDonViService = nsDonViService;
            _SktMucLucService = sktMucLucService;
            _exportService = exportService;
            _donViService = donViService;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _log = log;
            _danhMucService = danhMucService;
            _ftpService = ftpService;
            _ftpStorageService = ftpStorageService;
            _hTTPUploadFileService = hTTPUploadFileService;
            _cryptographyService = cryptographyService;
            Demand3YDialogViewModel = demand3YDialogViewModel;
            Demand3YDetailViewModel = demand3YDetailViewModel;
            PrintReportDemandOrgViewModel = printReportDemandOrgViewModel;

            Demand3YDialogViewModel.ParentPage = this;
            Demand3YDetailViewModel.ParentPage = this;

            ExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            LockCommand = new RelayCommand(OnLock);
            PrintCommand = new RelayCommand(OnPrint);
            SearchCommand = new RelayCommand(obj => SearchData());
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
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
                                && x.ILoai == DemandCheckType.DEMAND3Y
                                && x.INamLamViec == _sessionInfo.YearOfWork).FirstOrDefault();
                            if (ctChild != null)
                            {
                                ctChild.BDaTongHop = false;
                                _sktChungTuService.Update(ctChild);
                            }
                        }
                    }
                    _log.WriteLog(Resources.ApplicationName, "Xóa chứng từ số nhu cầu", (int)TypeExecute.Delete, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    var predicate = PredicateBuilder.True<NsNc3YChungTuChiTiet>();
                    predicate = predicate.And(x => x.IIdCtsoKiemTra == sktChungTu.Id);
                    var nc3YChungTuChiTiets = _nc3YChungTuChiTietService.FindByCondition(predicate);
                    _nc3YChungTuChiTietService.RemoveRange(nc3YChungTuChiTiets);

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
                _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                    ListSortDirection.Ascending));
                _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.TenDonVi),
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
            var iLoai = DemandCheckType.DEMAND3Y;
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
            Demand3YDialogViewModel.Name = "Thêm chứng từ";
            Demand3YDialogViewModel.Description = "Tạo mới chứng từ số nhu cầu";
            Demand3YDialogViewModel.NsSktChungTuModel = new NsSktChungTuModel();
            Demand3YDialogViewModel.isSummary = false;
            Demand3YDialogViewModel.Init();
            Demand3YDialogViewModel.SavedAction = obj =>
            {
                var sktChungTu = (NsSktChungTuModel)obj;
                _voucherTypeSelected = sktChungTu.ILoaiChungTu.HasValue
                    ? _voucherTypes.First(x => x.ValueItem.Equals(sktChungTu.ILoaiChungTu.Value.ToString()))
                    : _voucherTypes.First();
                OnPropertyChanged(nameof(VoucherTypeSelected));
                this.LoadData();
                OpenDetailDialog(sktChungTu);
            };
            var exportView = new Demand3YDialog() { DataContext = Demand3YDialogViewModel };
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
                    Demand3YDialogViewModel.NsSktChungTuModel = SelectedNsSktChungTuModel;
                    Demand3YDialogViewModel.Name = "Sửa chứng từ";
                    Demand3YDialogViewModel.Description = "Cập nhật thông tin chứng từ số nhu cầu";
                    Demand3YDialogViewModel.isSummary = false;
                    Demand3YDialogViewModel.Init();
                    Demand3YDialogViewModel.SavedAction = obj =>
                    {
                        this.LoadData();
                    };
                    var editView = new Demand3YDialog() { DataContext = Demand3YDialogViewModel };
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
                    MessageBoxHelper.Warning(string.Format("Đồng chí không có quyền mở khóa chứng từ {0} do không có quyền tổng hợp", lstSoChungTu));
                    return;
                }
                string lstSoChungTuDaTongHop = string.Join(", ", Items.Where(n => n.Selected && n.BDaTongHop.GetValueOrDefault() && n.BKhoa && !n.IIdMaDonVi.Equals(_sessionInfo.IdDonVi)).Select(n => n.SSoChungTu));

                if (!string.IsNullOrEmpty(lstSoChungTuDaTongHop))
                {
                    MessageBoxHelper.Warning(string.Format("Đồng chí không có quyền mở khóa chứng từ {0} do đã gửi lên tổng hợp", lstSoChungTuDaTongHop));
                    return;
                }

                var predicate = PredicateBuilder.True<NsSktChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicate = predicate.And(x => x.ILoai == DemandCheckType.DEMAND3Y);
                predicate = predicate.And(x => x.IIdMaDonVi.Equals(_sessionInfo.IdDonVi));
                var ctTongHop = _sktChungTuService.FindByCondition(predicate).FirstOrDefault();
                if (ctTongHop != null && ctTongHop.BKhoa)
                {
                    var lstCtChild = ctTongHop.SDssoChungTuTongHop;
                    string lstSoChungTuCoTongHopKhoa = string.Join(", ", Items.Where(n => n.Selected && lstCtChild != null && lstCtChild.Contains(n.SSoChungTu)));
                    if (!string.IsNullOrEmpty(lstSoChungTuCoTongHopKhoa))
                    {
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
            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult dialogResult = MessageBoxHelper.Confirm(message);
            if (dialogResult == MessageBoxResult.Yes)
            {
                LockOrUnLockMultiVoucher();
                MessageBoxHelper.Info(msgDone);
                LockStatusSelected = LockStatus.ElementAt(0);
            }
        }
        private void OnPrint(object param)
        {
            var demandCheckPrintType = (DemandCheckPrintType)((int)param);
            object content;
            switch (demandCheckPrintType)
            {
                case DemandCheckPrintType.REPORT_ORG_DEMAND3Y_DETAIL_NUMBER:
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
                case DemandCheckPrintType.REPORT_DEMAND3Y_NUMBER_SUMMARY:
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
                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, DivisionScreen.ROOT_DIALOG, null, null);
            }
        }
        /// <summary>
        ///     Mở màn hình in
        /// </summary>
        /// <param name="param"></param>       

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
            Demand3YDetailViewModel.Model = ObjectCopier.Clone(nsSktDetail);
            Demand3YDetailViewModel.CtTongHop = chungTuTH;
            Demand3YDetailViewModel.IsVoucherSummary = nsSktDetail.IIdMaDonVi.Equals(idDonViCurrent) && !string.IsNullOrEmpty(nsSktDetail.SDssoChungTuTongHop);
            Demand3YDetailViewModel.LstCanCu = null;
            Demand3YDetailViewModel.ShowColNSBD = nsSktDetail.ILoaiChungTu.Equals(int.Parse(VoucherType.NSBD_Key)) ? Visibility.Visible : Visibility.Collapsed;
            Demand3YDetailViewModel.ShowColNSSD = nsSktDetail.ILoaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key)) ? Visibility.Visible : Visibility.Collapsed;
            if (isNew.Length > 0 && !isNew[0])
            {
                Demand3YDetailViewModel.IndexDataState = DataStateValue.CO_SO_LIEU_DT_QT_SKT;
            }
            else
            {
                Demand3YDetailViewModel.IndexDataState = DataStateValue.HIEN_THI_TAT_CA;
            }
            Demand3YDetailViewModel.Init();
            var view = new Demand3YDetail() { DataContext = Demand3YDetailViewModel };
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
            Demand3YDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
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

            Demand3YDialogViewModel.Name = "Thêm chứng từ";
            Demand3YDialogViewModel.Description = "Tạo mới chứng từ tổng hợp";
            Demand3YDialogViewModel.NsSktChungTuModel = new NsSktChungTuModel();
            Demand3YDialogViewModel.isSummary = true;
            Demand3YDialogViewModel.ListIdsSktChungTuSummary = selectedSktChungTus;
            Demand3YDialogViewModel.Init();
            Demand3YDialogViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.MLNS;
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((NsSktChungTuModel)obj);
            };
            var addView = new Demand3YDialog() { DataContext = Demand3YDialogViewModel };
            DialogHost.Show(addView, DemandCheckScreen.ROOT_DIALOG, null, ClosingEventHandler);
        }

        private void OnAggregateEdit()
        {
            //kiểm tra trạng thái các bản ghi
            List<NsSktChungTuModel> selectedSktChungTus = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedNsSktChungTuModel.SDssoChungTuTongHop) && SelectedNsSktChungTuModel.SDssoChungTuTongHop.Contains(x.SSoChungTu)).ToList();

            Demand3YDialogViewModel.Name = "Sửa chứng từ";
            Demand3YDialogViewModel.Description = "Sửa chứng từ tổng hợp";
            Demand3YDialogViewModel.NsSktChungTuModel = SelectedNsSktChungTuModel;
            Demand3YDialogViewModel.isSummary = true;
            Demand3YDialogViewModel.ListIdsSktChungTuSummary = selectedSktChungTus;
            Demand3YDialogViewModel.Init();
            Demand3YDialogViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((NsSktChungTuModel)obj);
            };
            var addView = new Demand3YDialog() { DataContext = Demand3YDialogViewModel };
            DialogHost.Show(addView, DemandCheckScreen.ROOT_DIALOG, null, ClosingEventHandler);
        }



        /// <summary>
        /// Xuất excel chứng từ tổng hợp
        /// </summary>
        
        #region Code Gửi dữ liệu quản lý ngân sách
        /// <summary>
        /// Code by Gửi dữ liệu Quản lý ngân sách.
        /// </summary>
        /// <returns></returns>
        /// 
        private void RefreshAfterClosePopupSendData(object sender, EventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            OnRefresh();
        }        

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

        private List<NsNc3YChungTuChiTietModel> GetDemandVoucherDetail(NsSktChungTuModel nsSktChungTuModel)
        {
            var loaiChungTu = nsSktChungTuModel.ILoaiChungTu.GetValueOrDefault(-1);
            Nc3YChungTuChiTietCriteria searchCondition = new Nc3YChungTuChiTietCriteria();
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
            var temp = _nc3YChungTuChiTietService.FindByConditionForChildUnit(searchCondition);
            var lstChungTuChiTietModels = _mapper.Map<List<NsNc3YChungTuChiTietModel>>(temp);
            CalculateData(lstChungTuChiTietModels);
            return lstChungTuChiTietModels;
        }

        private void CalculateData(List<NsNc3YChungTuChiTietModel> lstNc3YChungTuChiTiet)
        {
            lstNc3YChungTuChiTiet.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FDuToan = 0;
                    x.FUocTH = 0;
                    x.FNCNam1 = 0;
                    x.FNCNam2 = 0;
                    x.FNCNam3 = 0;
                    return x;
                }).ToList();
            var temp = lstNc3YChungTuChiTiet.Where(x => !x.IsHangCha && !x.IsDeleted);
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, lstNc3YChungTuChiTiet);
            }

        }

        private void CalculateParent(Guid idParent, NsNc3YChungTuChiTietModel item, List<NsNc3YChungTuChiTietModel> lstNc3YChungTuChiTiet)
        {
            var model = lstNc3YChungTuChiTiet.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            model.FDuToan += item.FDuToan;
            model.FUocTH += item.FUocTH;
            model.FNCNam1 += item.FNCNam1;
            model.FNCNam2 += item.FNCNam2;
            model.FNCNam3 += item.FNCNam3;
            CalculateParent(model.IdParent, item, lstNc3YChungTuChiTiet);
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