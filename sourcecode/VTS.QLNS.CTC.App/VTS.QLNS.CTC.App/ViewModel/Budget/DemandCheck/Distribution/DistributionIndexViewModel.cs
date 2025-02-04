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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Distribution;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.FunctionMap.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Distribution
{
    public class DistributionIndexViewModel : GridViewModelBase<NsSktChungTuModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly INsSktNganhThamDinhChiTietSktService _sktChungTuThamDinhService;
        private readonly ISktMucLucService _SktMucLucService;
        private IExportService _exportService;
        private ICollectionView _nsDonViModelsView;
        private ICollectionView _sktChungTuModelsView;
        private const string _idDonViDuPhong = "999";
        private SessionInfo _sessionInfo;

        private readonly FtpStorageService _ftpStorageService;
        private readonly IDanhMucService _danhMucService;
        private readonly IVdtFtpRootService _ftpService;
        private IHTTPUploadFileService _hTTPUploadFileService;
        private DistributionImportJson _importJsonView;
        public SendDataDistributionViewModel SendDataDistributionViewModel { get; }

        private bool _isDelete;
        public bool IsDelete
        {
            get => _isDelete;
            set => SetProperty(ref _isDelete, value);
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
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
                if (_selectedNsSktChungTuModel != null)
                    IsLock = _selectedNsSktChungTuModel.BKhoa;
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
                    LoadNsDonVis();
                    IsAllItemsSelected = false;
                    OnPropertyChanged(nameof(ShowColNSBD));
                    OnPropertyChanged(nameof(ShowColNSSD));
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
            }
        }

        /// <summary>
        /// Checkbox select all property
        /// </summary>
        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
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

        private void SelectAll(bool select, IEnumerable<NsSktChungTuModel> models)
        {
            foreach (var model in models)
            {
                if (model.IIdMaDonVi != _sessionService.Current.IdDonVi && VoucherTypeSelected != null && model.ILoaiChungTu == Int32.Parse(VoucherTypeSelected.ValueItem))
                {
                    model.Selected = select;
                }
            }
        }

        public bool IsExportDistributionData
        {
            get
            {
                return Items.Where(item => item.Selected).ToList().Count > 0;
            }
        }

        public Visibility ShowColNSBD => VoucherTypeSelected != null && VoucherType.NSBD_Key == VoucherTypeSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowColNSSD => VoucherTypeSelected != null && VoucherType.NSSD_Key == VoucherTypeSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;

        public string ComboboxDisplayMemberPath => nameof(SelectedNsDonViModel.TenDonViIdDonVi);
        public override string FuncCode => NSFunctionCode.BUDGET_DEMANDCHECK_DISTRIBUTION;
        public override Type ContentType => typeof(DistributionIndex);
        public override string Description => "Chứng từ phân bổ số kiểm tra ngân sách năm " +
                                              _sessionInfo.YearOfWork;
        public override string Name => "Phân bổ số kiểm tra";
        public override PackIconKind IconKind => PackIconKind.RhombusOutline;

        public DistributionDialogViewModel DistributionDialogViewModel { get; }
        public DistributionDetailViewModel DistributionDetailViewModel { get; }
        public DistributionChildDetailViewModel DistributionChildDetailViewModel { get; }
        public CheckDetailViewModel CheckDetailViewModel { get; }
        public PrintReportDemandOrgViewModel PrintReportDemandOrgViewModel { get; }
        public DistributionImportJsonViewModel DistributionImportJsonViewModel { get; }

        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ExportDistributionDataCommand { get; }
        public RelayCommand UploadFileCommandHTTP { get; }
        public RelayCommand UploadFileCommandFTP { get; }
        public RelayCommand ImportJsonCommand { get; }
        public RelayCommand ExportJsonCommand { get; }


        public DistributionIndexViewModel(
            ISktChungTuService sktChungTuService,
            INsDonViService nsDonViService,
            ILog logger,
            IMapper mapper,
            DistributionDialogViewModel distributionDialogViewModel, ISessionService sessionService,
            DistributionDetailViewModel distributionDetailViewModel,
            DistributionChildDetailViewModel distributionChildDetailViewModel,
            PrintReportDemandOrgViewModel printReportDemandOrgViewModel,
            DistributionImportJsonViewModel distributionImportJsonViewModel,
            CheckDetailViewModel checkDetailViewModel,
            ISktMucLucService SktMucLucService,
            ISktChungTuChiTietService sktChungTuChiTietService,
            INsSktNganhThamDinhChiTietSktService sktChungTuThamDinhService,
             FtpStorageService ftpStorageService,
            IDanhMucService danhMucService,
            IVdtFtpRootService ftpService,
            IExportService exportService,
            SendDataDistributionViewModel sendDataDistributionViewModel,
            IHTTPUploadFileService hTTPUploadFileService)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _sktChungTuService = sktChungTuService;
            _nsDonViService = nsDonViService;
            _SktMucLucService = SktMucLucService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _sktChungTuThamDinhService = sktChungTuThamDinhService;
            _exportService = exportService;

            _danhMucService = danhMucService;
            _ftpStorageService = ftpStorageService;
            _ftpService = ftpService;
            DistributionDialogViewModel = distributionDialogViewModel;
            DistributionDetailViewModel = distributionDetailViewModel;
            DistributionChildDetailViewModel = distributionChildDetailViewModel;
            CheckDetailViewModel = checkDetailViewModel;
            PrintReportDemandOrgViewModel = printReportDemandOrgViewModel;
            DistributionImportJsonViewModel = distributionImportJsonViewModel;
            SendDataDistributionViewModel = sendDataDistributionViewModel;
            _hTTPUploadFileService = hTTPUploadFileService;

            CancelCommand = new RelayCommand(obj => { ParentPage.ParentPage.CurrentPage = null; });
            ExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            PrintCommand = new RelayCommand(OnPrint);
            LockCommand = new RelayCommand(OnLock);
            SearchCommand = new RelayCommand(obj => SearchData());
            ExportDistributionDataCommand = new RelayCommand(obj => OnExportDistributionData());
            //UploadFileCommand = new RelayCommand(obj => OnUpload());
            UploadFileCommandHTTP = new RelayCommand(obj => OnUploadDialog(true));
            UploadFileCommandFTP = new RelayCommand(obj => OnUploadDialog(false));
            DeleteCommand = new RelayCommand(obj => OnDelete());
            ImportJsonCommand = new RelayCommand(obj => OnImportJson());
            ExportJsonCommand = new RelayCommand(obj => OnExportJson());
        }

        private async Task OnUploadDialog(bool isSendHTTP)
        {
            if (!Items.Any(n => n.Selected))
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
            }
            catch (ConfigurationErrorsException ex)
            {
                IsLoading = false;
                new NSMessageBoxViewModel("Cấu hình sai đường dẫn hoặc cổng HTTP").ShowDialogHost();
                return;
            }
            SendDataDistributionViewModel.ListChildAgency = await _hTTPUploadFileService.FindAllChildren(isSendHTTP);
            SendDataDistributionViewModel._selectedNsSktChungTuModel = _selectedNsSktChungTuModel;
            SendDataDistributionViewModel.Items = Items;
            SendDataDistributionViewModel.IsSendHTTP = isSendHTTP;
            SendDataDistributionViewModel.Init();
            SendDataDistributionViewModel.ShowDialogHost();
            var addView = new View.Budget.DemandCheck.Distribution.SendDataDistribution.SendDataDistribution() { DataContext = SendDataDistributionViewModel };
            IsLoading = false;
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog();
        }

        protected override void OnDelete()
        {

            if (SelectedNsSktChungTuModel == null)
                return;

            var messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTuSKT, SelectedNsSktChungTuModel.SSoChungTu, SelectedNsSktChungTuModel.SSoQuyetDinh,
                SelectedNsSktChungTuModel.DNgayChungTu.GetValueOrDefault(DateTime.Now).ToString("dd/MM/yyyy"));
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                OnDeleteHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void OnDeleteHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes)
                return;
            _sktChungTuService.DeletePhanBoDuToan(SelectedNsSktChungTuModel.Id, SelectedNsSktChungTuModel.IIdMaDonVi, _sessionInfo.YearOfWork);
            LoadSktChungTus();
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void OnSelectedChange(object obj)
        {
            if (obj is NsSktChungTuModel selected)
            {
                var parent = Items.FirstOrDefault(x => x.Id == selected.Id && x.IIdMaDonVi == _sessionInfo.IdDonVi && x.IIdMaNguonNganSach == selected.IIdMaNguonNganSach);
                IsEdit = parent is object && !parent.BKhoa;
                IsDelete = parent is object && !parent.BKhoa;
            }
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (VoucherTypeSelected != null)
            {
                LoadSktChungTus();
            }
        }

        private void LoadData()
        {
            LoadOrderTypes();
            LoadNsDonVis();
        }

        private void LoadNsDonVis()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var nsDonViOfCurrentUser = GetNsDonViOfCurrentUser();
            var predicate = PredicateBuilder.True<DonVi>();
            var iTrangThai = StatusType.ACTIVE;
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == "1");
            bool isDvCap4 = _nsDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
            if (isDvCap4)
            {
                predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == "0");
            }
            else
            {
                if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == "1")
                {
                    predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && (x.Loai == "1" || x.Loai == "0"));
                }
                else if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == "2")
                {
                    predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && (x.Loai == "1" || x.Loai == "0") && true.Equals(x.BCoNSNganh));
                }
            }
            var listUnit = _nsDonViService.FindByCondition(predicate).ToList();
            NsDonViModelItems = new ObservableCollection<DonViModel>();
            //NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit.Where(n => n.Khoi != "3"));
            NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
            _nsDonViModelsView = CollectionViewSource.GetDefaultView(NsDonViModelItems);
            _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                ListSortDirection.Ascending));
            _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.IIDMaDonVi),
                ListSortDirection.Ascending));
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
                    SortDirection = ListSortDirection.Ascending, SortMemberPath = nameof(Model.NsSktChungTuModel.SSoChungTu)
                },
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.SO_CHUNG_TU_DESC, ValueItem = DemandCheckSortTypeValue.SO_CHUNG_TU_DESC,
                    SortDirection = ListSortDirection.Descending, SortMemberPath = nameof(Model.NsSktChungTuModel.SSoChungTu)
                },
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.NGAY_CHUNG_TU_ASC,
                    ValueItem = DemandCheckSortTypeValue.NGAY_CHUNG_TU_ASC,
                    SortDirection = ListSortDirection.Ascending, SortMemberPath = nameof(Model.NsSktChungTuModel.DNgayChungTu)
                },
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.NGAY_CHUNG_TU_DESC,
                    ValueItem = DemandCheckSortTypeValue.NGAY_CHUNG_TU_DESC,
                    SortDirection = ListSortDirection.Descending, SortMemberPath = nameof(Model.NsSktChungTuModel.DNgayChungTu)
                },
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.SO_QUYET_DINH_ASC, ValueItem = DemandCheckSortTypeValue.SO_QUYET_DINH_ASC,
                    SortDirection = ListSortDirection.Ascending, SortMemberPath = nameof(Model.NsSktChungTuModel.SSoQuyetDinh)
                },
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.SO_QUYET_DINH_DESC,
                    ValueItem = DemandCheckSortTypeValue.SO_QUYET_DINH_DESC,
                    SortDirection = ListSortDirection.Descending, SortMemberPath = nameof(Model.NsSktChungTuModel.SSoQuyetDinh)
                },
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.NGAY_QUYET_DINH_ASC,
                    ValueItem = DemandCheckSortTypeValue.NGAY_QUYET_DINH_ASC,
                    SortDirection = ListSortDirection.Ascending, SortMemberPath = nameof(Model.NsSktChungTuModel.DNgayQuyetDinh)
                },
                new OrderComboboxItem
                {
                    DisplayItem = DemandCheckSortTypeName.NGAY_QUYET_DINH_DESC,
                    ValueItem = DemandCheckSortTypeValue.NGAY_QUYET_DINH_DESC,
                    SortDirection = ListSortDirection.Descending, SortMemberPath = nameof(Model.NsSktChungTuModel.DNgayQuyetDinh)
                }
            };
        }

        private void LoadSktChungTus()
        {
            var idDonVi = _sessionInfo.IdDonVi;
            var nsDonViOfCurrentUser = GetNsDonViOfCurrentUser();
            var namLamViec = _sessionInfo.YearOfWork;
            var namNganSach = _sessionInfo.YearOfBudget;
            var nguonNganSach = _sessionInfo.Budget;
            var iLoai = DemandCheckType.CHECK;
            var iTrangThai = StatusType.ACTIVE;
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai && x.Loai == "1");
            bool isDvCap4 = _nsDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
            var loaiDV = "";
            int loaiChungTu = -1;
            if (VoucherTypeSelected != null)
            {
                loaiChungTu = Int32.Parse(VoucherTypeSelected.ValueItem);
            }
            IEnumerable<NsSktChungTu> listChungTu;
            loaiDV = isDvCap4 ? "0" : "1";

            listChungTu = _sktChungTuService
                .FindByCondition(iLoai, namLamViec, namNganSach, nguonNganSach, iTrangThai, idDonVi, loaiDV, loaiChungTu, _sessionInfo.Principal)
                .Where(item => item.FTongTuChi != 0 || item.FTongMuaHangCapHienVat != 0 || item.FTongPhanCap != 0 || item.IndexDonVi == 0).Select(
                    (x, index) =>
                    {
                        x.Index = index;
                        return x;
                    });

            Items = _mapper.Map<ObservableCollection<NsSktChungTuModel>>(listChungTu);
            foreach (var item in Items)
            {
                if (item.ILoaiChungTu == null && VoucherTypeSelected != null)
                {
                    item.ILoaiChungTu = Int32.Parse(VoucherTypeSelected.ValueItem);
                }

                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(NsSktChungTuModel.Selected))
                    {
                        OnPropertyChanged(nameof(IsExportDistributionData));
                    }
                };
            }
            _sktChungTuModelsView = CollectionViewSource.GetDefaultView(Items);
            _sktChungTuModelsView.Filter = SktChungTuModelsFilter;
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var currentIdDonVi = _sessionInfo.IdDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private DonVi GetNsDonVi(string idDV)
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == idDV && x.ITrangThai == 1);
            var nsDonVi = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonVi;
        }

        private IEnumerable<DonVi> GetChildNsDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var currentIdDonVi = SelectedNsSktChungTuModel.IIdMaDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == currentIdDonVi);
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            var predicate2 = PredicateBuilder.True<DonVi>();
            predicate2 = predicate2.And(x =>
                x.NamLamViec == yearOfWork && x.ITrangThai == StatusType.ACTIVE &&
                x.IdParent == nsDonViOfCurrentUser.Id);
            var listChild = _nsDonViService.FindByCondition(predicate2);
            return listChild;
        }

        private bool SktChungTuModelsFilter(object obj)
        {
            if (!(obj is NsSktChungTuModel temp))
                return true;
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

            var result = condition1 && condition2;
            return result;
        }

        private void LockOrUnLockRegularBudget()
        {
            if (SelectedNsSktChungTuModel == null)
                return;
            IsLock = !IsLock;
            _sktChungTuService.LockOrUnlock(SelectedNsSktChungTuModel.Id, IsLock);
            var sktChungTu = Items.First(x => x.Id == SelectedNsSktChungTuModel.Id);
            sktChungTu.BKhoa = !SelectedNsSktChungTuModel.BKhoa;
            IsEdit = !sktChungTu.BKhoa;
        }

        protected override void OnAdd()
        {
            DistributionDialogViewModel.ParentPage = this;
            DistributionDialogViewModel.Name = "Thêm chứng từ";
            DistributionDialogViewModel.Description = "Tạo mới chứng từ nhận số kiểm tra";
            DistributionDialogViewModel.Id = new Guid();
            DistributionDialogViewModel.SktChungTuModels = Items.ToList();
            DistributionDialogViewModel.Init();
            var addView = new DistributionDialog { DataContext = DistributionDialogViewModel };
            DialogHost.Show(addView, DemandCheckScreen.ROOT_DIALOG, null, ClosingEventHandler);
        }

        protected override void OnUpdate()
        {
            if (SelectedNsSktChungTuModel != null)
            {
                DistributionDialogViewModel.ParentPage = this;
                DistributionDialogViewModel.Id = SelectedNsSktChungTuModel.Id;
                DistributionDialogViewModel.SktChungTuModels = Items.ToList();
                DistributionDialogViewModel.Name = "Sửa chứng từ";
                DistributionDialogViewModel.Description = "Cập nhật thông tin chứng từ";
                DistributionDialogViewModel.Init();
                var editView = new DistributionDialog { DataContext = DistributionDialogViewModel };
                DialogHost.Show(editView, DemandCheckScreen.ROOT_DIALOG, null, ClosingEventHandler);
            }
        }

        private void OnLock(object obj)
        {
            var message = string.Empty;
            message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            var messageBox = new NSMessageBoxViewModel(message, "Xác nhận", NSMessageBoxButtons.YesNo, OnLockHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void OnLockHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes)
                return;
            LockOrUnLockRegularBudget();
        }

        /// <summary>
        ///     Mở màn hình in
        /// </summary>
        /// <param name="param"></param>
        private void OnPrint(object param)
        {
            var demandCheckPrintType = (DemandCheckPrintType)(int)param;
            object content;
            switch (demandCheckPrintType)
            {
                case DemandCheckPrintType.REPORT_PHAN_BO_SO_KIEM_TRA_THEO_DON_VI:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = demandCheckPrintType;
                    PrintReportDemandOrgViewModel.Init();
                    content = new PrintReportDemandOrg
                    {
                        DataContext = PrintReportDemandOrgViewModel
                    };
                    break;
                case DemandCheckPrintType.REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = demandCheckPrintType;
                    PrintReportDemandOrgViewModel.Init();
                    content = new PrintReportDemandOrg
                    {
                        DataContext = PrintReportDemandOrgViewModel
                    };
                    break;
                case DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH:
                case DemandCheckPrintType.REPORT_SO_SANH_PHAN_BO_SKT_NAM_TRUOC_NAM_NAY:
                case DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA:
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

        protected override void OnRefresh()
        {
            if (VoucherTypeSelected != null)
            {
                LoadSktChungTus();
            }
        }

        private void SearchData()
        {
            _sktChungTuModelsView.Refresh();
        }

        /// <summary>
        /// Open Detail
        /// </summary>
        /// <param name="sktChungTuModel"></param>
        private void OpenDetailDialog()
        {
            /// don vi cha
            var dvDetail = GetNsDonVi(SelectedNsSktChungTuModel.IIdMaDonVi);
            if (dvDetail != null && dvDetail.Loai == "0")
            {
                DistributionDetailViewModel.Model = SelectedNsSktChungTuModel.Clone();
                DistributionDetailViewModel.ShowColNSBD = SelectedNsSktChungTuModel.ILoaiChungTu.Equals(int.Parse(VoucherType.NSBD_Key)) ? Visibility.Visible : Visibility.Collapsed;
                DistributionDetailViewModel.ShowColNSSD = SelectedNsSktChungTuModel.ILoaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key)) ? Visibility.Visible : Visibility.Collapsed;
                DistributionDetailViewModel.Init();
                if (DistributionDetailViewModel.IsLock)
                {
                    DistributionDetailViewModel.IsReadOnly = true;
                }
                else
                    DistributionDetailViewModel.IsReadOnly = false;
                DistributionDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
                var view = new DistributionDetail() { DataContext = DistributionDetailViewModel };
                view.ShowDialog();
            }
            else
            {
                DistributionChildDetailViewModel.Model = SelectedNsSktChungTuModel.Clone();
                DistributionChildDetailViewModel.ShowColNSBD = SelectedNsSktChungTuModel.ILoaiChungTu.Equals(int.Parse(VoucherType.NSBD_Key)) ? Visibility.Visible : Visibility.Collapsed;
                DistributionChildDetailViewModel.ShowColNSSD = SelectedNsSktChungTuModel.ILoaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key)) ? Visibility.Visible : Visibility.Collapsed;
                DistributionChildDetailViewModel.IsBVTC = SelectedNsSktChungTuModel.ILoai == DemandCheckType.CORPORATIZED_HOSPITAL ? true : false;
                DistributionChildDetailViewModel.Init();
                DistributionChildDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
                var view = new DistributionChildDetail() { DataContext = DistributionChildDetailViewModel };
                view.ShowDialog();
            }
        }

        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }

        /// <summary>
        /// Xuất excel chứng từ tổng hợp
        /// </summary>
        private void OnExportDistributionData()
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

                    List<NsSktChungTuModel> sktChungTuModelsSummary = Items.Where(x => x.Selected).ToList();
                    foreach (var item in sktChungTuModelsSummary)
                    {
                        var yearOfWork = _sessionInfo.YearOfWork;
                        var currentIdDonVi = _sessionInfo.IdDonVi;
                        var currentDonVi = GetNsDonViOfCurrentUser();
                        List<NsSktChungTuChiTietModel> sktChungTuChiTietModels = GetDistributionVoucherDetail(item);
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
                        //NSSD
                        double SumTotalHuyDong = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FHuyDongTonKho);
                        double SumTotalTuChi = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTuChi);
                        double SumTotalTongCongNSSD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongHuyDongTuChi);
                        //NSBD
                        double SumTotalMHHV = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FMuaHangCapHienVat);
                        double SumTotalDT = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FPhanCap);
                        double SumTotalTongCongNSBD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongMuaHangHienVatDacThu);

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("SoChungTu", item.SSoChungTu);
                        data.Add("TenDonVi", item.STenDonVi);
                        data.Add("IdDonVi", item.IIdMaDonVi);
                        data.Add("Cap1", currentDonVi.TenDonVi);
                        data.Add("TieuDe1", "THÔNG BÁO SỐ KIỂM TRA DỰ TOÁN NGÂN SÁCH NĂM " + _sessionService.Current.YearOfWork);
                        data.Add("DonViPB", item.STenDonVi);
                        data.Add("h2", "Lữ đoàn X");
                        data.Add("h1", "Lữ đoàn X");
                        data.Add("LoaiChungTu", item.ILoaiChungTu == 1 ? VoucherType.NSSD_Value : VoucherType.NSBD_Value);
                        data.Add("MoTa", item.SMoTa);
                        data.Add("NgayChungTu", item.DNgayChungTu.GetValueOrDefault(DateTime.Now).ToString("dd/MM/yyyy"));
                        data.Add("NguoiTao", item.SNguoiTao);
                        data.Add("NgayTao", item.DNgayTao.GetValueOrDefault(new DateTime()).ToString("dd/MM/yyyy"));
                        data.Add("SumTotalHuyDong", SumTotalHuyDong);
                        data.Add("SumTotalTuChi", SumTotalTuChi);
                        data.Add("SumTotalMHHV", SumTotalMHHV);
                        data.Add("SumTotalDT", SumTotalDT);
                        data.Add("SumTotalTongCongNSSD", SumTotalTongCongNSSD);
                        data.Add("SumTotalTongCongNSBD", SumTotalTongCongNSBD);
                        data.Add("ListData", sktChungTuChiTietModels);
                        data.Add("SKTML", sktMucLucsOrder);

                        if (item.ILoaiChungTu == 1)
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_PHANBO_NSSD);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_PHANBO_NSBD);
                        }
                        fileNamePrefix = item.STenDonVi;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<NsSktChungTuChiTietModel, NsSktMucLuc>(templateFileName, data);
                        var nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
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

        private void OnUpload()
        {
            try
            {
                if (!Items.Any(n => n.Selected) || Items.Where(n => n.Selected).Count() > 1 || SelectedNsSktChungTuModel == null)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Vui lòng chọn 1 bản ghi !");
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }
                List<ExportResult> results = new List<ExportResult>();
                string templateFileName;
                string fileNamePrefix;
                string fileNameWithoutExtension;

                List<NsSktChungTuModel> sktChungTuModelsSummary = Items.Where(x => x.Selected).ToList();
                foreach (var item in sktChungTuModelsSummary)
                {
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var currentIdDonVi = _sessionInfo.IdDonVi;
                    var currentDonVi = GetNsDonViOfCurrentUser();
                    List<NsSktChungTuChiTietModel> sktChungTuChiTietModels = GetDistributionVoucherDetail(item);
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
                    //NSSD
                    double SumTotalHuyDong = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FHuyDongTonKho);
                    double SumTotalTuChi = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTuChi);
                    double SumTotalTongCongNSSD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongHuyDongTuChi);
                    //NSBD
                    double SumTotalMHHV = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FMuaHangCapHienVat);
                    double SumTotalDT = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FPhanCap);
                    double SumTotalTongCongNSBD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongMuaHangHienVatDacThu);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("SoChungTu", item.SSoChungTu);
                    data.Add("TenDonVi", item.STenDonVi);
                    data.Add("IdDonVi", item.IIdMaDonVi);
                    data.Add("Cap1", currentDonVi.TenDonVi);
                    data.Add("TieuDe1", "THÔNG BÁO SỐ KIỂM TRA DỰ TOÁN NGÂN SÁCH NĂM " + _sessionService.Current.YearOfWork);
                    data.Add("DonViPB", item.STenDonVi);
                    data.Add("h2", "Lữ đoàn X");
                    data.Add("h1", "Lữ đoàn X");
                    data.Add("LoaiChungTu", item.ILoaiChungTu == 1 ? VoucherType.NSSD_Value : VoucherType.NSBD_Value);
                    data.Add("MoTa", item.SMoTa);
                    data.Add("NgayChungTu", item.DNgayChungTu.GetValueOrDefault(DateTime.Now).ToString("dd/MM/yyyy"));
                    data.Add("NguoiTao", item.SNguoiTao);
                    data.Add("NgayTao", item.DNgayTao.GetValueOrDefault(new DateTime()).ToString("dd/MM/yyyy"));
                    data.Add("SumTotalHuyDong", SumTotalHuyDong);
                    data.Add("SumTotalTuChi", SumTotalTuChi);
                    data.Add("SumTotalMHHV", SumTotalMHHV);
                    data.Add("SumTotalDT", SumTotalDT);
                    data.Add("SumTotalTongCongNSSD", SumTotalTongCongNSSD);
                    data.Add("SumTotalTongCongNSBD", SumTotalTongCongNSBD);
                    data.Add("ListData", sktChungTuChiTietModels);
                    data.Add("SKTML", sktMucLucsOrder);

                    if (item.ILoaiChungTu == 1)
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_PHANBO_NSSD);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_PHANBO_NSBD);
                    }
                    fileNamePrefix = item.STenDonVi;
                    fileNameWithoutExtension = StringUtils.ConvertVN(StringUtils.CreateExportFileName(fileNamePrefix));
                    var xlsFile = _exportService.Export<NsSktChungTuChiTietModel, NsSktMucLuc>(templateFileName, data);
                    var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                    string filePathLocal = string.Empty;
                    string sStage = string.Empty;
                    _exportService.Open(Result, ExportType.EXCEL, ref filePathLocal);
                    if (SelectedNsSktChungTuModel != null)
                    {
                        if (item.ILoaiChungTu == 1)
                        {
                            sStage = StringUtils.UCS2Convert(VoucherType.NSSD_Value);
                        }
                        else
                        {
                            sStage = StringUtils.UCS2Convert(VoucherType.NSBD_Value);
                        }

                    }
                    string sMaDonVi = item.IIdMaDonVi;
                    string sNameUnit = string.Empty;
                    sNameUnit = StringUtils.UCS2Convert(item.TenDonViIdDonVi).Replace("---", "-");
                    string sFolderRoot = ConstantUrlPathPhanHe.UrlQlnsSktformReceive;
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
                        dataRoot = _ftpService.GetVdtFtpRoot(sMaDonVi, SIpAddress, sFolderRoot);
                        if (dataRoot == null)
                        {
                            dataRoot = new VdtFtpRoot()
                            {
                                SMaDonVi = sMaDonVi,
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
                            StringBuilder messageBuilder = new StringBuilder();
                            messageBuilder.AppendFormat("Gửi dữ liệu thất bại");
                            MessageBox.Show(messageBuilder.ToString());
                            return;
                        }
                        else
                        {
                            StringBuilder messageBuilder = new StringBuilder();
                            messageBuilder.AppendFormat("Gửi dữ liệu thành công");
                            MessageBox.Show(messageBuilder.ToString());
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }

        }
        private List<NsSktChungTuChiTietModel> GetDistributionVoucherDetail(NsSktChungTuModel nsSktChungTuModel)
        {
            SktChungTuChiTietCriteria searchCondition = new SktChungTuChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            searchCondition.NamNganSach = _sessionInfo.YearOfBudget;
            searchCondition.NguonNganSach = _sessionInfo.Budget;
            searchCondition.ITrangThai = StatusType.ACTIVE;
            searchCondition.SktChungTuId = nsSktChungTuModel.Id;
            searchCondition.ILoai = nsSktChungTuModel.ILoai;
            //searchCondition.ILoai = nsSktChungTuModel.IIdMaDonVi == _sessionInfo.IdDonVi ? DemandCheckType.CHECK : DemandCheckType.DISTRIBUTION;
            searchCondition.IdDonVi = nsSktChungTuModel.IIdMaDonVi;
            searchCondition.CurrentIdDonVi = _sessionInfo.IdDonVi;
            searchCondition.UserName = _sessionInfo.Principal;
            searchCondition.LoaiChungTu = nsSktChungTuModel.ILoaiChungTu.GetValueOrDefault();
            var temp = _sktChungTuChiTietService.FindByConditionForChildUnit(searchCondition);
            var lstChungTuChiTietModels = _mapper.Map<List<NsSktChungTuChiTietModel>>(temp);
            CalculateData(lstChungTuChiTietModels);
            lstChungTuChiTietModels = lstChungTuChiTietModels.Where(item => item.FTuChi > 0 || item.FHuyDongTonKho > 0 || item.FMuaHangCapHienVat > 0 || item.FPhanCap > 0).ToList();
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
                    x.FPhanCap = 0;
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
            if (model == null)
                return;
            model.FTuChi += item.FTuChi;
            model.FHuyDongTonKho += item.FHuyDongTonKho;
            model.FMuaHangCapHienVat += item.FMuaHangCapHienVat;
            model.FPhanCap += item.FPhanCap;
            CalculateParent(model.IdParent, item, lstSktChungTuChiTiet);
        }

        public void OnChangeVisibilityColumn()
        {
            OnPropertyChanged(nameof(ShowColNSBD));
            OnPropertyChanged(nameof(ShowColNSSD));
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadLockStatus();
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            //LoadData();
            LoadVoucherTypes();
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

        private void OnImportJson()
        {
            DistributionImportJsonViewModel.Init();
            DistributionImportJsonViewModel.SavedAction = obj =>
            {
                OnRefresh();
                _importJsonView.Close();
            };
            _importJsonView = new DistributionImportJson { DataContext = DistributionImportJsonViewModel };
            _importJsonView.Show();
        }

        private void OnExportJson()
        {
            if (!Items.Any(n => n.Selected))
            {
                MessageBoxHelper.Error(Resources.MsgRecordEmpty);
                return;
            }
            List<NsSktChungTu> lstData = GetDataExportJson();
            _exportService.OpenJson(lstData);
        }

        private List<NsSktChungTu> GetDataExportJson()
        {
            List<NsSktChungTu> lstData = new List<NsSktChungTu>();
            if (!Items.Any(n => n.Selected))
                return lstData;
            List<Guid> lstIdChungTu = Items.Where(n => n.Selected).Select(n => n.Id).ToList();

            Dictionary<Guid, List<NsSktChungTuChiTiet>> lstDetail = new Dictionary<Guid, List<NsSktChungTuChiTiet>>();
            Dictionary<Guid, List<JsonNsSktNganhThamDinhChiTietSktQuery>> lstThamDinh = new Dictionary<Guid, List<JsonNsSktNganhThamDinhChiTietSktQuery>>();

            lstData = _sktChungTuService.FindByCondition(n => lstIdChungTu.Contains(n.Id)).ToList();
            var details = _sktChungTuChiTietService.FindByCondition(n => lstIdChungTu.Contains(n.IIdCtsoKiemTra));
            if (details != null)
            {
                lstDetail = details.GroupBy(n => n.IIdCtsoKiemTra).ToDictionary(n => n.Key, n => n.ToList());
            }
            var thamdinhs = _sktChungTuThamDinhService.GetNsSktNganhThamDinhChiTietByChungTuId(lstIdChungTu);
            if (thamdinhs != null)
            {
                lstThamDinh = thamdinhs.GroupBy(n => n.IIdCtsoKiemTra).ToDictionary(n => n.Key, n => n.ToList());
            }
            foreach (var item in lstData)
            {
                if (lstDetail.ContainsKey(item.Id))
                    item.ListDetail = lstDetail[item.Id].OrderBy(n => n.SKyHieu).ToList();
                if (lstThamDinh.ContainsKey(item.Id))
                    item.ListThamDinh = lstThamDinh[item.Id].OrderBy(n => n.SKyHieu).ToList();
            }
            return lstData;
        }
    }
}