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
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Settlement.DefenseBudget;
using VTS.QLNS.CTC.App.View.Budget.Settlement.Import;
using VTS.QLNS.CTC.App.View.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.DefenseBudget.ExportDefenseBudget;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.DefenseBudget.SendDataDefenseBudget;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.DefenseBudget
{
    public class DefenseBudgetIndexViewModel : GridViewModelBase<SettlementVoucherModel>
    {
        private readonly INsDonViService _donViService;
        private readonly INsQtChungTuService _chungTuService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private ICollectionView _listSettlementVoucher;
        private ICollectionView _listSettlementVoucherSummary;
        private readonly INsQtChungTuChiTietService _chungTuChiTietService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsNguoiDungDonViService _iNsNguoiDungDonViService;
        private readonly IVdtFtpRootService _ftpService;
        private readonly FtpStorageService _ftpStorageService;
        private readonly INsPhongBanService _phongBanService;
        private readonly ICryptographyService _cryptographyService;
        private readonly IHTTPUploadFileService _hTTPUploadFileService;
        private SessionInfo _sessionInfo;
        private readonly PrintSummaryRegularSettlementViewModel PrintSummaryRegularSettlementViewModel;
        private readonly PrintSettlementVoucherViewModel PrintSettlementVoucherViewModel;
        private readonly PrintCommunicateSettlementLNSViewModel PrintCommunicateSettlementLNSViewModel;
        private readonly PrintReceiveSettlementViewModel PrintReceiveSettlementViewModel;
        private readonly PrintCommunicateSettlementAgencyViewModel PrintCommunicateSettlementAgencyViewModel;
        private readonly PrintSummaryLNSViewModel PrintSummaryLNSViewModel;
        private readonly PrintEstimateSettlementViewModel PrintEstimateSettlementViewModel;
        private readonly PrintSummaryYearSettlementViewModel PrintSummaryYearSettlementViewModel;
        private readonly PrintSummaryAgencyViewModel PrintSummaryAgencyViewModel;
        private readonly SettlementImportViewModel SettlementImportViewModel;
        private List<NsQtChungTuQuery> _listChungTu;
        private DonVi _aggregateAgency;
        private SettlementImport _settlementImportView;
        private readonly ILog _logger;
        private SettlementVoucherCriteria _condition;
        private SettlementVoucherModel SelectedVoucher;
        private List<SettlementVoucherDetailModel> _settlementVoucherDetailExports;
        private SettlementImportJson _importJsonView;
        private readonly SettlementImportJsonViewModel SettlementImportJsonViewModel;
        private readonly ExportDefenseBudgetViewModel ExportDefenseBudgetViewModel;
        private readonly SendDataDefenseBudgetViewModel SendDataDefenseBudgetViewModel;
        private readonly PrintReportPublicSettlementViewModel PrintReportPublicSettlementViewModel;

        public override string FuncCode => NSFunctionCode.BUDGET_SETTLEMENT_DEFENSEBUDGET;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Ngân sách quốc phòng";
        public override Type ContentType => typeof(View.Budget.Settlement.DefenseBudget.DefenseBudgetIndex);
        public override string Description => "Quyết toán ngân sách Quốc phòng (nghiệp vụ và NSK)";
        public override PackIconKind IconKind => PackIconKind.ShieldStar;

        private ObservableCollection<SettlementVoucherModel> _settlementVouchers;
        public ObservableCollection<SettlementVoucherModel> SettlementVouchers
        {
            get => _settlementVouchers;
            set => SetProperty(ref _settlementVouchers, value);
        }

        //public bool IsButtonEnable => LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0");
        public bool IsButtonEnable
        {
            get
            {
                bool result = false;
                if (TabIndex == ImportTabIndex.Data)
                {
                    List<SettlementVoucherModel> lstSelected = SettlementVouchers.Where(x => x.Selected).ToList();
                    if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count() > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        List<SettlementVoucherModel> lstSelectedKhoa = lstSelected.Where(x => x.BKhoa).ToList();
                        List<SettlementVoucherModel> lstSelectedMo = lstSelected.Where(x => !x.BKhoa).ToList();

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
                }
                else
                {
                    List<SettlementVoucherModel> lstSelectedSummary = SettlementVoucherSummaries.Where(x => x.Selected).ToList();
                    if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelectedSummary.Count() > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        List<SettlementVoucherModel> lstSelectedSummaryKhoa = lstSelectedSummary.Where(x => x.BKhoa).ToList();
                        List<SettlementVoucherModel> lstSelectedSummaryMo = lstSelectedSummary.Where(x => !x.BKhoa).ToList();

                        if (lstSelectedSummaryKhoa.Count() > 0 && lstSelectedSummaryMo.Count() > 0)
                        {
                            result = false;
                        }
                        else if (lstSelectedSummaryKhoa.Count() > 0)
                        {
                            IsLock = true;
                            result = true;
                        }
                        else if (lstSelectedSummaryMo.Count() > 0)
                        {
                            IsLock = false;
                            result = true;
                        }
                    }
                }
                return result;
            }
        }


        private ObservableCollection<SettlementVoucherModel> _settlementVoucherSummaries;
        public ObservableCollection<SettlementVoucherModel> SettlementVoucherSummaries
        {
            get => _settlementVoucherSummaries;
            set => SetProperty(ref _settlementVoucherSummaries, value);
        }

        private SettlementVoucherModel _selectedItemSummary;
        public SettlementVoucherModel SelectedItemSummary
        {
            get => _selectedItemSummary;
            set
            {
                if (SetProperty(ref _selectedItemSummary, value))
                    OnSelectedItemChanged();
            }
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
                _listSettlementVoucher.Refresh();
                _listSettlementVoucherSummary.Refresh();
            }
        }

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private ComboboxItem _agencySelected;
        public ComboboxItem AgencySelected
        {
            get => _agencySelected;
            set
            {
                SetProperty(ref _agencySelected, value);
                _listSettlementVoucher.Refresh();
                _listSettlementVoucherSummary.Refresh();
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
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
                {
                    IsLock = true;
                }
                else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                {
                    IsLock = false;
                }
                _listSettlementVoucher.Refresh();
                _listSettlementVoucherSummary.Refresh();
            }
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private bool _isOpenExcelPopup;
        public bool IsOpenExcelPopup
        {
            get => _isOpenExcelPopup;
            set => SetProperty(ref _isOpenExcelPopup, value);
        }

        //public bool IsLock => TabIndex == ImportTabIndex.Data ? SelectedItem != null && SelectedItem.BKhoa : SelectedItemSummary != null && SelectedItemSummary.BKhoa;
        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }
        public bool IsEdit => TabIndex == ImportTabIndex.Data ? (SelectedItem != null && !SelectedItem.BKhoa) : (SelectedItemSummary != null && !SelectedItemSummary.BKhoa);
        public bool IsDelete => TabIndex == ImportTabIndex.Data ? (SelectedItem != null && !SelectedItem.BKhoa) : (SelectedItemSummary != null && !SelectedItemSummary.BKhoa);

        public bool IsButtonSendDataEnable => TabIndex != ImportTabIndex.Data;

        private bool _isOpenDialog;
        public bool IsOpenDialog
        {
            get => _isOpenDialog;
            set => SetProperty(ref _isOpenDialog, value);
        }

        public bool IsAggregate
        {
            get
            {
                IEnumerable<SettlementVoucherModel> listSelected = _settlementVouchers.Where(x => x.Selected);
                if (listSelected.Count() > 0)
                    if (listSelected.Any(x => !string.IsNullOrEmpty(x.STongHop)))
                        return false;
                    else return true;
                return false;
            }
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                OnSelectedItemChanged();
            }
        }
        public bool IsExportAggregateData => (TabIndex == ImportTabIndex.Aggregate && _selectedItemSummary != null) || (TabIndex == ImportTabIndex.Data && SelectedItem != null);
        /// <summary>
        /// Checkbox select all property
        /// </summary>
        public bool? IsAllItemsSelected
        {
            get
            {
                if (SettlementVouchers != null)
                {
                    List<bool> selected = SettlementVouchers.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, SettlementVouchers);
                    OnPropertyChanged();
                }
            }
        }

        public bool? IsAllItemSummariesSelected
        {
            get
            {
                if (SettlementVoucherSummaries != null)
                {
                    List<bool> selected = SettlementVoucherSummaries.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, SettlementVoucherSummaries);
                    OnPropertyChanged();
                }
            }
        }
        public DefenseBudgetDialogViewModel DefenseBudgetDialogViewModel { get; set; }
        public DefenseBudgetDetailViewModel DefenseBudgetDetailViewModel { get; set; }
        public RelayCommand ExcelCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand ExportAggregateDataCommand { get; }
        public RelayCommand ExportDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand UploadFileCommandHTTP { get; }
        public RelayCommand UploadFileCommandFTP { get; }
        public RelayCommand ImportJsonCommand { get; }
        public RelayCommand ExportJsonCommand { get; }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsDelete));
            OnPropertyChanged(nameof(IsButtonEnable));
            OnPropertyChanged(nameof(IsButtonSendDataEnable));
            OnPropertyChanged(nameof(IsExportAggregateData));
            //OnPropertyChanged(nameof(IsLock));
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDefenseBudgetDetailDialog((SettlementVoucherModel)obj);
        }

        public DefenseBudgetIndexViewModel(DefenseBudgetDialogViewModel defenseBudgetDialogViewModel,
                                           DefenseBudgetDetailViewModel defenseBudgetDetailViewModel,
                                           INsDonViService donViService,
                                           INsQtChungTuService chungTuService,
                                           IMapper mapper,
                                           ISessionService sessionService,
                                           PrintEstimateSettlementViewModel printEstimateSettlementViewModel,
                                           PrintSummaryRegularSettlementViewModel printSummaryRegularSettlementViewModel,
                                           PrintSettlementVoucherViewModel printSettlementVoucherViewModel,
                                           PrintCommunicateSettlementLNSViewModel printCommunicateSettlementLNSViewModel,
                                           PrintReceiveSettlementViewModel printReceiveSettlementViewModel,
                                           PrintCommunicateSettlementAgencyViewModel printCommunicateSettlementAgencyViewModel,
                                           PrintSummaryLNSViewModel printSummaryLNSViewModel,
                                           PrintSummaryYearSettlementViewModel printSummaryYearSettlementViewModel,
                                           PrintSummaryAgencyViewModel printSummaryAgencyViewModel,
                                           INsQtChungTuChiTietService chungTuChiTietService,
                                           INsMucLucNganSachService mucLucNganSachService,
                                           INsNguoiDungDonViService iNsNguoiDungDonViService,
                                           IExportService exportService,
                                           IVdtFtpRootService ftpService,
                                           FtpStorageService ftpStorageService,
                                           SettlementImportViewModel settlementImportViewModel,
                                           SettlementImportJsonViewModel settlementImportJsonViewModel,
                                           ExportDefenseBudgetViewModel exportDefenseBudgetViewModel,
                                           SendDataDefenseBudgetViewModel sendDataDefenseBudgetViewModel,
                                           PrintReportPublicSettlementViewModel printReportPublicSettlementViewModel,
                                           IDanhMucService danhMucService,
                                           INsPhongBanService phongBanService,
                                           ICryptographyService cryptographyService,
                                           IHTTPUploadFileService hTTPUploadFileService,
                                           ILog logger)
        {
            DefenseBudgetDialogViewModel = defenseBudgetDialogViewModel;
            DefenseBudgetDetailViewModel = defenseBudgetDetailViewModel;
            _donViService = donViService;
            _phongBanService = phongBanService;
            _cryptographyService = cryptographyService;
            _hTTPUploadFileService = hTTPUploadFileService;
            _chungTuService = chungTuService;
            _mapper = mapper;
            _sessionService = sessionService;
            _chungTuChiTietService = chungTuChiTietService;
            _mucLucNganSachService = mucLucNganSachService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _iNsNguoiDungDonViService = iNsNguoiDungDonViService;
            _ftpService = ftpService;
            _ftpStorageService = ftpStorageService;
            _logger = logger;

            PrintEstimateSettlementViewModel = printEstimateSettlementViewModel;
            PrintSummaryRegularSettlementViewModel = printSummaryRegularSettlementViewModel;
            PrintSettlementVoucherViewModel = printSettlementVoucherViewModel;
            PrintCommunicateSettlementLNSViewModel = printCommunicateSettlementLNSViewModel;
            PrintReceiveSettlementViewModel = printReceiveSettlementViewModel;
            PrintCommunicateSettlementAgencyViewModel = printCommunicateSettlementAgencyViewModel;
            PrintSummaryLNSViewModel = printSummaryLNSViewModel;
            PrintSummaryYearSettlementViewModel = printSummaryYearSettlementViewModel;
            PrintSummaryAgencyViewModel = printSummaryAgencyViewModel;
            SettlementImportViewModel = settlementImportViewModel;
            SettlementImportJsonViewModel = settlementImportJsonViewModel;
            ExportDefenseBudgetViewModel = exportDefenseBudgetViewModel;
            SendDataDefenseBudgetViewModel = sendDataDefenseBudgetViewModel;
            PrintReportPublicSettlementViewModel = printReportPublicSettlementViewModel;

            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            ExcelCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            AggregateCommand = new RelayCommand(obj => OnAggregate());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateDataDialog());
            ExportDataCommand = new RelayCommand(obj => OnExportAggregateData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            UploadFileCommandHTTP = new RelayCommand(obj => OnUploadDialog(true));
            UploadFileCommandFTP = new RelayCommand(obj => OnUploadDialog(false));
            //UploadFileCommand = new RelayCommand(async obj => await OnUpload());
            ImportJsonCommand = new RelayCommand(obj => OnImportJson());
            ExportJsonCommand = new RelayCommand(obj => OnExportJson());
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

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            ResetCondition();
            LoadDefenseBudget();
            LoadQuarters();
            LoadAgencies();
            LoadLockStatus();
        }

        private void ResetCondition()
        {
            _quarterSelected = null;
            _agencySelected = null;
            _tabIndex = ImportTabIndex.Data;

            OnPropertyChanged(nameof(QuarterSelected));
            OnPropertyChanged(nameof(AgencySelected));
            OnPropertyChanged(nameof(TabIndex));
        }

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        /// <summary>
        /// Lấy dữ liệu chứng từ hiển thị ban đầu
        /// </summary>
        private void LoadDefenseBudget()
        {
            _condition = new SettlementVoucherCriteria
            {
                SettlementType = SettlementType.DEFENSE_BUDGET,
                YearOfWork = _sessionInfo.YearOfWork,
                YearOfBudget = _sessionInfo.YearOfBudget,
                BudgetSource = _sessionInfo.Budget,
                Status = (int)Status.ACTIVE,
                UserName = _sessionInfo.Principal
            };
            if (QuarterSelected != null)
                _condition.QuarterMonth = Convert.ToInt32(QuarterSelected.ValueItem);
            if (AgencySelected != null)
                _condition.AgencyId = AgencySelected.ValueItem;
            _listChungTu = _chungTuService.FindByCondition(_condition);

            if (_sessionService.Current.IsQuanLyDonViCha)
            {
                SettlementVouchers = _mapper.Map<ObservableCollection<SettlementVoucherModel>>(_listChungTu.Where(x => !IsDonViRoot(x.IIdMaDonVi) && x.BDaTongHop.HasValue && !x.BDaTongHop.Value));

                List<SettlementVoucherModel> listChungTuTongHop = new List<SettlementVoucherModel>();
                foreach (NsQtChungTuQuery chungTu in _listChungTu.Where(x => IsDonViRoot(x.IIdMaDonVi)))
                {
                    SettlementVoucherModel parent = _mapper.Map<SettlementVoucherModel>(chungTu);
                    parent.IsExpand = true;
                    listChungTuTongHop.Add(parent);
                    if (!string.IsNullOrEmpty(chungTu.STongHop))
                    {
                        List<SettlementVoucherModel> listChild = _mapper.Map<List<SettlementVoucherModel>>(_listChungTu.Where(x => chungTu.STongHop.Split(",").Contains(x.SSoChungTu) && x.BDaTongHop.HasValue && x.BDaTongHop.Value).ToList());
                        listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = chungTu.SSoChungTu; });
                        listChungTuTongHop.AddRange(listChild);
                    }
                }
                SettlementVoucherSummaries = new ObservableCollection<SettlementVoucherModel>(listChungTuTongHop);
            }
            else
            {
                SettlementVouchers = _mapper.Map<ObservableCollection<SettlementVoucherModel>>(_listChungTu.Where(x => !IsDonViRoot(x.IIdMaDonVi)));
                SettlementVoucherSummaries = new ObservableCollection<SettlementVoucherModel>();
            }

            foreach (SettlementVoucherModel model in SettlementVouchers)
            {
                model.PropertyChanged += Model_PropertyChanged;
            }

            foreach (SettlementVoucherModel model in SettlementVoucherSummaries)
            {
                model.PropertyChanged += Model_PropertyChanged;
                model.TypeIcon = model.IsSent ? "CheckBold" : "CancelBold";
            }

            _listSettlementVoucher = CollectionViewSource.GetDefaultView(SettlementVouchers);
            _listSettlementVoucher.Filter = ListSettlementVoucherFilter;
            _listSettlementVoucherSummary = CollectionViewSource.GetDefaultView(SettlementVoucherSummaries);
            _listSettlementVoucherSummary.Filter = ListSettlementVoucherFilter;
            SelectedItem = SettlementVouchers.FirstOrDefault();
            SelectedItemSummary = SettlementVoucherSummaries.FirstOrDefault();
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(SettlementVoucherModel.Selected))
            {
                if (TabIndex == ImportTabIndex.Data)
                    OnPropertyChanged(nameof(IsAllItemsSelected));
                else OnPropertyChanged(nameof(IsAllItemSummariesSelected));
                OnPropertyChanged(nameof(IsAggregate));
                OnPropertyChanged(nameof(IsButtonEnable));
            }

            if (args.PropertyName == nameof(SettlementVoucherModel.IsCollapse))
            {
                ExpandChild();
            }
        }

        private void ExpandChild()
        {
            if (SelectedItemSummary != null)
            {
                SettlementVoucherSummaries.Where(n => n.SoChungTuParent == SelectedItemSummary.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
            OnPropertyChanged(nameof(SettlementVoucherSummaries));
        }

        private bool ListSettlementVoucherFilter(object obj)
        {
            bool result = true;
            SettlementVoucherModel item = (SettlementVoucherModel)obj;
            if (QuarterSelected != null)
                result = result && item.IThangQuy == Convert.ToInt32(QuarterSelected.ValueItem);
            if (AgencySelected != null)
                result = result && item.IIdMaDonVi == AgencySelected.ValueItem;
            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
            {
                result = result && item.BKhoa;
            }
            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
            {
                result = result && !item.BKhoa;
            }
            item.IsFilter = result;
            return result;
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

        /// <summary>
        /// Tạo data cho combobox đơn vị
        /// </summary>
        private void LoadAgencies()
        {
            _aggregateAgency = _donViService.FindByIdDonVi(_sessionInfo.IdDonVi, _sessionInfo.YearOfWork);
            List<DonVi> listDonVi = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.NOI_BO).ToList();
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
        }

        /// <summary>
        /// Action when checkbox select all is selected
        /// </summary>
        /// <param name="select">true/false</param>
        /// <param name="models">items source of data grid</param>
        private void SelectAll(bool select, IEnumerable<SettlementVoucherModel> models)
        {
            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
            {
                models = models.Where(x => x.BKhoa).ToList();
            }
            else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
            {
                models = models.Where(x => !x.BKhoa).ToList();
            }
            foreach (SettlementVoucherModel model in models)
            {
                model.Selected = select;
            }
        }

        protected override void OnAdd()
        {
            base.OnAdd();
            DefenseBudgetDialogViewModel.Id = Guid.Empty;
            int voucherNoIndex = _chungTuService.CreateVoucherIndex(_condition);
            DefenseBudgetDialogViewModel.VoucherNoIndex = voucherNoIndex;
            DefenseBudgetDialogViewModel.IsAggregate = false;
            DefenseBudgetDialogViewModel.IsAdjustEnabled = true;
            DefenseBudgetDialogViewModel.Init();
            DefenseBudgetDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OpenDefenseBudgetDetailDialog((SettlementVoucherModel)obj);
            };
            DefenseBudgetDialog view = new DefenseBudgetDialog { DataContext = DefenseBudgetDialogViewModel };
            DialogHost.Show(view, SettlementScreen.ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            SettlementVoucherModel model = TabIndex == ImportTabIndex.Data ? SelectedItem : SelectedItemSummary;

            if (model.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Info(string.Format(Resources.MsgRoleEdit));
                return;
            }
            DefenseBudgetDialogViewModel.Id = model.Id;
            DefenseBudgetDialogViewModel.IsAggregate = false;
            DefenseBudgetDialogViewModel.IsAdjustEnabled = false;
            DefenseBudgetDialogViewModel.Init();
            DefenseBudgetDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            DefenseBudgetDialog view = new DefenseBudgetDialog { DataContext = DefenseBudgetDialogViewModel };
            DialogHost.Show(view, SettlementScreen.ROOT_DIALOG);
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadDefenseBudget();
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
                case (int)SettlementPrintType.PRINT_RECEIVE_SETTLEMENT:
                    PrintReceiveSettlementViewModel.Init();
                    PrintReceiveSettlementViewModel.ShowDialogHost();
                    break;
                case (int)SettlementPrintType.PRINT_COMMUNICATE_SETTLEMENT_LNS:
                    PrintCommunicateSettlementLNSViewModel.SettlementVoucher = TabIndex == ImportTabIndex.Data ? SelectedItem : SelectedItemSummary;
                    if (PrintCommunicateSettlementLNSViewModel.SettlementVoucher == null)
                    {
                        MessageBoxHelper.Warning(Resources.AlertEmptySettlementVoucher);
                        return;
                    }
                    PrintCommunicateSettlementLNSViewModel.Init();
                    PrintCommunicateSettlementLNS view1 = new PrintCommunicateSettlementLNS { DataContext = PrintCommunicateSettlementLNSViewModel };
                    //show the dialog
                    DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)SettlementPrintType.PRINT_COMMUNICATE_SETTLEMENT_AGENCY:
                    PrintCommunicateSettlementAgencyViewModel.SettlementTypeValue = SettlementType.DEFENSE_BUDGET;
                    PrintCommunicateSettlementAgencyViewModel.Init();
                    PrintCommunicateSettlementAgency view2 = new PrintCommunicateSettlementAgency { DataContext = PrintCommunicateSettlementAgencyViewModel };
                    //show the dialog
                    DialogHost.Show(view2, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)SettlementPrintType.PRINT_REGULARLY_SETTLEMENT:
                    PrintSummaryLNSViewModel.SettlementTypeValue = SettlementType.REGULAR_BUDGET;
                    PrintSummaryLNSViewModel.TieuDeBaoCao = Name;
                    PrintSummaryLNSViewModel.IsShowDatePeople = false;
                    PrintSummaryLNSViewModel.Init();
                    PrintSummaryLNS view3 = new PrintSummaryLNS { DataContext = PrintSummaryLNSViewModel };
                    //show the dialog
                    DialogHost.Show(view3, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)SettlementPrintType.PRINT_DEFENSE_SETTLEMENT:
                    PrintSummaryLNSViewModel.SettlementTypeValue = SettlementType.DEFENSE_BUDGET;
                    PrintSummaryLNSViewModel.TieuDeBaoCao = Name;
                    PrintSummaryLNSViewModel.IsShowDatePeople = true;
                    PrintSummaryLNSViewModel.Init();
                    PrintSummaryLNS view4 = new PrintSummaryLNS { DataContext = PrintSummaryLNSViewModel };
                    //show the dialog
                    DialogHost.Show(view4, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)SettlementPrintType.PRINT_STATE_SETTLEMENT:
                    PrintSummaryLNSViewModel.SettlementTypeValue = SettlementType.STATE_BUDGET;
                    PrintSummaryLNSViewModel.TieuDeBaoCao = Name;
                    PrintSummaryLNSViewModel.IsShowDatePeople = false;
                    PrintSummaryLNSViewModel.Init();
                    PrintSummaryLNS view5 = new PrintSummaryLNS { DataContext = PrintSummaryLNSViewModel };
                    //show the dialog
                    DialogHost.Show(view5, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)SettlementPrintType.PRINT_FOREX_SETTLEMENT:
                    PrintSummaryLNSViewModel.SettlementTypeValue = SettlementType.FOREX_BUDGET;
                    PrintSummaryLNSViewModel.TieuDeBaoCao = Name;
                    PrintSummaryLNSViewModel.IsShowDatePeople = false;
                    PrintSummaryLNSViewModel.Init();
                    PrintSummaryLNS view6 = new PrintSummaryLNS { DataContext = PrintSummaryLNSViewModel };
                    //show the dialog
                    DialogHost.Show(view6, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)SettlementPrintType.PRINT_EXPENSE_SETTLEMENT:
                    PrintSummaryLNSViewModel.SettlementTypeValue = SettlementType.EXPENSE_BUDGET;
                    PrintSummaryLNSViewModel.TieuDeBaoCao = Name;
                    PrintSummaryLNSViewModel.IsShowDatePeople = false;
                    PrintSummaryLNSViewModel.Init();
                    PrintSummaryLNS view7 = new PrintSummaryLNS { DataContext = PrintSummaryLNSViewModel };
                    //show the dialog
                    DialogHost.Show(view7, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)SettlementPrintType.PRINT_LNS_SETTLEMENT:
                    PrintSummaryLNSViewModel.SettlementTypeValue = SettlementType.LNS;
                    PrintSummaryLNSViewModel.TieuDeBaoCao = Name;
                    PrintSummaryLNSViewModel.IsShowDatePeople = false;
                    PrintSummaryLNSViewModel.Init();
                    PrintSummaryLNS view8 = new PrintSummaryLNS { DataContext = PrintSummaryLNSViewModel };
                    //show the dialog
                    DialogHost.Show(view8, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)SettlementPrintType.PRINT_SUMMARY_AGENCY:
                    PrintSummaryAgencyViewModel.Init();
                    PrintSummaryAgency view9 = new PrintSummaryAgency { DataContext = PrintSummaryAgencyViewModel };
                    //show the dialog
                    DialogHost.Show(view9, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)SettlementPrintType.PRINT_ESTIMATE_SETTLEMENT:
                    PrintEstimateSettlementViewModel.Init();
                    PrintEstimateSettlement view10 = new PrintEstimateSettlement { DataContext = PrintEstimateSettlementViewModel };
                    //show the dialog
                    DialogHost.Show(view10, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)SettlementPrintType.PRINT_SUMMARY_YEAR_SETTLEMENT:
                    PrintSummaryYearSettlementViewModel.Init();
                    PrintSummaryYearSettlement view11 = new PrintSummaryYearSettlement { DataContext = PrintSummaryYearSettlementViewModel };
                    //show the dialog
                    DialogHost.Show(view11, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)SettlementPrintType.PRINT_REPORT_PUBLIC_SETTLEMENT:
                    PrintReportPublicSettlementViewModel.Init();
                    PrintReportPublicSettlement view12 = new PrintReportPublicSettlement { DataContext = PrintReportPublicSettlementViewModel };
                    //show the dialog
                    DialogHost.Show(view12, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
            }
        }

        /// <summary>
        /// Khóa hoặc mở khóa chứng từ
        /// </summary>
        protected override void OnLockUnLock()
        {
            //var model = TabIndex == ImportTabIndex.Data ? SelectedItem : SelectedItemSummary;
            //if (IsLock)
            //{
            //    List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
            //    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
            //    {
            //        MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
            //        return;
            //    }
            //    if (model.BDaTongHop.GetValueOrDefault())
            //    {
            //        MessageBoxHelper.Warning(Resources.AlertUnlockAggregatedVoucher);
            //        return;
            //    }
            //}
            //else
            //{
            //    if (model.SNguoiTao != _sessionInfo.Principal)
            //    {
            //        MessageBoxHelper.Info(string.Format(Resources.MsgRoleLock, model.SNguoiTao));
            //        return;
            //    }
            //}
            //string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            //MessageBoxResult result = MessageBoxHelper.Confirm(message);
            //if (result == MessageBoxResult.Yes)
            //    LockConfirmEventHandler(model);
            if (TabIndex == ImportTabIndex.Data)
            {
                if (IsLock)
                {
                    string listSoChungTu = string.Join(", ", SettlementVouchers.Where(n => n.Selected && n.BKhoa));
                    List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                    {
                        //MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                        MessageBoxHelper.Warning(string.Format("Đồng chí không được mở khóa chứng từ {0} do không có quyền tổng hợp", listSoChungTu));
                        return;
                    }

                    string listSoChungTuDaTongHop = string.Join(", ", SettlementVouchers.Where(n => n.Selected && n.BDaTongHop.GetValueOrDefault() && n.BKhoa && !n.IIdMaDonVi.Equals(_sessionInfo.IdDonVi)).Select(n => n.SSoChungTu));

                    if (!string.IsNullOrEmpty(listSoChungTuDaTongHop))
                    {
                        //MessageBoxHelper.Warning(Resources.AlertUnlockAggregatedVoucher);
                        MessageBoxHelper.Warning(string.Format("Đồng chí không được mở khóa chứng từ {0} do đã gửi lên tổng hợp", listSoChungTuDaTongHop));
                        return;
                    }

                }
                else
                {
                    string listSoChungTuInvalid = string.Join(", ", SettlementVouchers.Where(n => n.Selected && n.SNguoiTao != _sessionInfo.Principal && !n.BKhoa).Select(n => n.SSoChungTu));

                    if (!string.IsNullOrEmpty(listSoChungTuInvalid))
                    {
                        //MessageBoxHelper.Info(string.Format(Resources.MsgRoleLock, SelectedItemElement.SNguoiTao));
                        MessageBoxHelper.Info(string.Format("Đồng chí không có quyền khóa chứng từ {0} do không phải người tạo", listSoChungTuInvalid));
                        return;
                    }

                }


                string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
                string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;

                MessageBoxResult result = MessageBoxHelper.Confirm(message);
                IEnumerable<SettlementVoucherModel> xx = SettlementVouchers.Where(n => n.Selected);
                if (result == MessageBoxResult.Yes)
                {
                    foreach (SettlementVoucherModel SelectedItemElement in SettlementVouchers.Where(n => n.Selected))
                    {
                        LockConfirmEventHandler(SelectedItemElement);
                    }
                    MessageBoxHelper.Info(msgDone);
                    //LockStatusSelected = IsLock ? LockStatus.ElementAt(2) : LockStatus.ElementAt(1);
                    LockStatusSelected = LockStatus.ElementAt(0);

                    //OnPropertyChanged(nameof(LockStatusSelected));
                    //_listSettlementVoucher.Refresh();
                    //_listSettlementVoucherSummary.Refresh();

                }
                //Reset danh sách về uncheckbox
                foreach (SettlementVoucherModel item in SettlementVouchers)
                {
                    item.Selected = false;
                }
            }
            else
            {
                if (IsLock)
                {
                    string listSoChungTu = string.Join(", ", SettlementVoucherSummaries.Where(n => n.Selected && n.BKhoa));
                    List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                    {
                        //MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                        MessageBoxHelper.Warning(string.Format("Đồng chí không được mở khóa chứng từ {0} do không có quyền tổng hợp", listSoChungTu));
                        return;
                    }
                    string listSoChungTuDaTongHop = string.Join(", ", SettlementVoucherSummaries.Where(n => n.Selected && n.BDaTongHop.GetValueOrDefault() && n.BKhoa).Select(n => n.SSoChungTu));

                    if (!string.IsNullOrEmpty(listSoChungTuDaTongHop))
                    {
                        //MessageBoxHelper.Warning(Resources.AlertUnlockAggregatedVoucher);
                        MessageBoxHelper.Warning(string.Format("Đồng chí không được mở khóa chứng từ {0} do đã gửi lên tổng hợp", listSoChungTuDaTongHop));
                        return;
                    }
                }
                else
                {
                    string listSoChungTuInvalid = string.Join(", ", SettlementVoucherSummaries.Where(n => n.Selected && n.SNguoiTao != _sessionInfo.Principal && !n.BKhoa).Select(n => n.SSoChungTu));

                    if (!string.IsNullOrEmpty(listSoChungTuInvalid))
                    {
                        //MessageBoxHelper.Info(string.Format(Resources.MsgRoleLock, SelectedItemElement.SNguoiTao));
                        MessageBoxHelper.Info(string.Format("Đồng chí không có quyền khóa chứng từ {0} do không phải người tạo", listSoChungTuInvalid));
                        return;
                    }
                }
                string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
                string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                MessageBoxResult result = MessageBoxHelper.Confirm(message);
                if (result == MessageBoxResult.Yes)
                {
                    foreach (SettlementVoucherModel SelectedItemSummaryElement in SettlementVoucherSummaries.Where(n => n.Selected))
                    {
                        LockConfirmEventHandler(SelectedItemSummaryElement);
                    }
                    MessageBoxHelper.Info(msgDone);
                    //LockStatusSelected = IsLock ? LockStatus.ElementAt(1) : LockStatus.ElementAt(2);
                    LockStatusSelected = LockStatus.ElementAt(0);
                    //OnPropertyChanged(nameof(LockStatusSelected));
                    //_listSettlementVoucher.Refresh();
                    //_listSettlementVoucherSummary.Refresh();
                }
                //Reset danh sách về uncheckbox
                foreach (SettlementVoucherModel item in SettlementVouchers)
                {
                    item.Selected = false;
                }
            }

        }

        /// <summary>
        /// xử lý khóa/mở khóa chứng từ sau khi đóng dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void LockConfirmEventHandler(SettlementVoucherModel model)
        {
            //string msgDone = model.BKhoa ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            _chungTuService.LockOrUnlock(model.Id, !model.BKhoa);
            model.BKhoa = !model.BKhoa;

            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsDelete));
            OnPropertyChanged(nameof(IsAggregate));
            //MessageBoxHelper.Info(msgDone);
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            StringBuilder messageBuilder = new StringBuilder();
            SettlementVoucherModel model = TabIndex == ImportTabIndex.Data ? SelectedItem : SelectedItemSummary;

            if (model.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, model.SNguoiTao));
                return;
            }
            messageBuilder.AppendFormat(Resources.DeleteChungTu, model.SSoChungTu, model.DNgayChungTu.ToString("dd/MM/yyyy"));

            MessageBoxResult result = MessageBoxHelper.Confirm(messageBuilder.ToString());
            if (result == MessageBoxResult.Yes)
                DeleteSelectedVoucher(model);
        }

        /// <summary>
        /// xử lý xóa chứng từ sau khi đóng dialog
        /// </summary>
        private void DeleteSelectedVoucher(SettlementVoucherModel model)
        {
            if (IsAggregatedVoucher(model))
            {
                MessageBoxHelper.Warning(Resources.AlertDeleteAggregatedVoucher);
                return;
            }
            Guid voucherId = model.Id;
            _chungTuService.Delete(voucherId);
            _chungTuChiTietService.DeleteByVoucherId(voucherId);

            if (!string.IsNullOrEmpty(model.STongHop))
            {
                List<Guid> voucherIds = _settlementVoucherSummaries.Where(x => x.SoChungTuParent == model.SSoChungTu).Select(x => x.Id).ToList();
                if (voucherIds.Count > 0)
                {
                    _chungTuService.UpdateAggregateStatus(string.Join(",", voucherIds));
                }
            }
            LoadDefenseBudget();
            MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
        }

        private bool IsAggregatedVoucher(SettlementVoucherModel model)
        {
            List<string> aggregatedVouchers = SettlementVoucherSummaries.Where(x => !string.IsNullOrEmpty(x.STongHop)).Select(x => x.STongHop).ToList();
            return aggregatedVouchers.Any(x => x.Contains(model.SSoChungTu));
        }

        /// <summary>
        /// Mở màn hình chi tiết chứng từ
        /// </summary>
        /// <param name="settlementVoucher"></param>
        private void OpenDefenseBudgetDetailDialog(SettlementVoucherModel settlementVoucher)
        {
            SelectedVoucher = settlementVoucher;
            if (SelectedVoucher.SNguoiTao != _sessionInfo.Principal)
                MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, SelectedVoucher.SNguoiTao));
            DefenseBudgetDetailViewModel.Model = settlementVoucher;
            DefenseBudgetDetailViewModel.UpdateSettlementVoucherEvent += RefreshAfterSaveData;
            DefenseBudgetDetailViewModel.Init();
            DefenseBudgetDetail view = new DefenseBudgetDetail { DataContext = DefenseBudgetDetailViewModel };
            view.ShowDialog();
            DefenseBudgetDetailViewModel.UpdateSettlementVoucherEvent -= RefreshAfterSaveData;
        }

        private void RefreshAfterSaveData(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedVoucher.STongHop))
            {
                SettlementVoucherModel voucher = (SettlementVoucherModel)sender;
                SettlementVoucherModel item = SettlementVouchers.Where(x => x.Id == voucher.Id).FirstOrDefault();
                if (item != null)
                {
                    item.FTongTuChiDeNghi = ((SettlementVoucherModel)sender).FTongTuChiDeNghi;
                    item.FTongTuChiPheDuyet = ((SettlementVoucherModel)sender).FTongTuChiPheDuyet;
                }
            }
            else
            {
                List<SettlementVoucherModel> vouchers = (List<SettlementVoucherModel>)sender;
                foreach (SettlementVoucherModel voucher in vouchers)
                {
                    SettlementVoucherModel item = SettlementVoucherSummaries.Where(x => x.Id == voucher.Id).FirstOrDefault();
                    if (item != null)
                    {
                        item.FTongTuChiDeNghi = voucher.FTongTuChiDeNghi;
                        item.FTongTuChiPheDuyet = voucher.FTongTuChiPheDuyet;
                    }
                }
            }
        }

        private void OnAggregate()
        {
            //check quyền được tổng hợp
            List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
            if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
            {
                MessageBoxHelper.Warning(Resources.MsgRoleAggregate);
                return;
            }

            List<SettlementVoucherModel> selectedSettlementVouchers = _settlementVouchers.Where(x => x.IsFilter && x.Selected).ToList();
            if (selectedSettlementVouchers.GroupBy(x => x.IThangQuy).ToList().Count() > 1)
            {
                MessageBoxHelper.Info(Resources.AlertAggregateQuarterMonth);
                return;
            }

            if (selectedSettlementVouchers.GroupBy(x => x.ILoaiChungTu).ToList().Count() > 1)
            {
                MessageBoxHelper.Info(Resources.AlertAggregateAdjust);
                return;
            }

            //kiểm tra trạng thái các bản ghi
            if (selectedSettlementVouchers.Any(x => !x.BKhoa))
            {
                MessageBoxHelper.Info(Resources.AlertAggregateUnLocked);
                return;
            }

            //kiểm tra đã tồn tại chứng từ tổng hợp từ các chứng từ đã chọn chưa
            Dictionary<Guid, List<string>> dicTongHop = new Dictionary<Guid, List<string>>();
            foreach (SettlementVoucherModel item in _settlementVoucherSummaries.Where(x => !x.IsChildSummary))
            {
                if (!dicTongHop.ContainsKey(item.Id))
                    dicTongHop.Add(item.Id, item.STongHop?.Split(StringUtils.COMMA).ToList() ?? new List<string> { item.SSoChungTu });
            }
            List<string> listChungTu = selectedSettlementVouchers.Select(x => x.SSoChungTu).ToList();
            if (dicTongHop.Values.Any(x => x.Intersect(listChungTu).Any()))
            {
                MessageBoxResult result = MessageBoxHelper.Confirm(Resources.AlertExistAggregateVoucher);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        foreach (KeyValuePair<Guid, List<string>> item in dicTongHop)
                        {
                            if (item.Value.Intersect(listChungTu).Any())
                                _chungTuService.Delete(item.Key);
                        }
                        CreateAggregateVoucher(selectedSettlementVouchers);
                        break;
                    case MessageBoxResult.No:
                        return;
                }
            }
            else CreateAggregateVoucher(selectedSettlementVouchers);
        }

        private void CreateAggregateVoucher(List<SettlementVoucherModel> selectedSettlementVouchers)
        {
            int voucherNoIndex = _chungTuService.CreateVoucherIndex(_condition);
            DefenseBudgetDialogViewModel.Id = Guid.Empty;
            DefenseBudgetDialogViewModel.VoucherNoIndex = voucherNoIndex;
            DefenseBudgetDialogViewModel.AggregateSettlementVouchers = selectedSettlementVouchers;
            DefenseBudgetDialogViewModel.AggregateAgency = _aggregateAgency;
            DefenseBudgetDialogViewModel.AggregateLNS = string.Join(",", selectedSettlementVouchers.Select(x => x.SDslns).Distinct());
            DefenseBudgetDialogViewModel.IsAggregate = true;
            DefenseBudgetDialogViewModel.Init();
            DefenseBudgetDialogViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.Aggregate;
                this.OnRefresh();
                OpenDefenseBudgetDetailDialog((SettlementVoucherModel)obj);
            };
            DefenseBudgetDialog view = new DefenseBudgetDialog { DataContext = DefenseBudgetDialogViewModel };
            DialogHost.Show(view, SettlementScreen.ROOT_DIALOG);
        }

        /// <summary>
        /// Xuất excel chứng từ tổng hợp
        /// </summary>

        private void OnExportAggregateDataDialog()
        {
            ExportDefenseBudgetViewModel._settlementVoucherSummaries = _settlementVoucherSummaries;
            ExportDefenseBudgetViewModel._settlementVouchers = _settlementVouchers;
            ExportDefenseBudgetViewModel.TabIndex = TabIndex;
            ExportDefenseBudgetViewModel.AggregateAgency = _aggregateAgency;
            ExportDefenseBudgetViewModel.IsAggregate = IsAggregate;
            ExportDefenseBudgetViewModel.Init();
            View.Budget.Settlement.DefenseBudget.ExportDefenseBudget.ExportDefenseBudget addView = new View.Budget.Settlement.DefenseBudget.ExportDefenseBudget.ExportDefenseBudget() { DataContext = ExportDefenseBudgetViewModel };
            DialogHost.Show(addView, SettlementScreen.ROOT_DIALOG, null, null);
        }

        private void OnExportAggregateData()
        {
            try
            {
                if (TabIndex == ImportTabIndex.Aggregate)
                {
                    if (_settlementVoucherSummaries.Where(s => s.Selected).Count() < 1)
                    {
                        MessageBoxHelper.Info(Resources.IsCheckbox);
                        return;
                    }
                }
                else
                {
                    if (_settlementVouchers.Where(s => s.Selected).Count() < 1)
                    {
                        MessageBoxHelper.Info(Resources.IsCheckbox);
                        return;
                    }
                }

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_CHUNGTU_TONGHOP);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    string donVi1 = _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper();
                    string donVi2 = _sessionInfo.TenDonVi;

                    if (TabIndex == ImportTabIndex.Aggregate)
                    {
                        List<SettlementVoucherModel> settlementVouchers = _settlementVoucherSummaries.Where(x => x.Selected && IsDonViRoot(x.IIdMaDonVi)).ToList();
                        foreach (SettlementVoucherModel item in settlementVouchers)
                        {
                            _settlementVoucherDetailExports = GetSettlementVoucherDetail(item);
                            CalculateData();
                            _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => x.FDuToan != 0 || x.FDaQuyetToan != 0
                                                                            || x.FTuChiDeNghi != 0 || x.FTuChiPheDuyet != 0
                                                                            || x.FChuyenNamSauChuaCap.GetValueOrDefault() != 0
                                                                            || x.FChuyenNamSauDaCap.GetValueOrDefault() != 0
                                                                            || x.FDeNghiChuyenNamSau != 0).OrderBy(x => x.SXauNoiMa).ToList();
                            _settlementVoucherDetailExports.ForEach(x => x.FTuChiDeNghi = x.FTuChiPheDuyet);
                            RptQuyetToanChungTuTongHop ctTongHop = new RptQuyetToanChungTuTongHop
                            {
                                DonVi1 = donVi1,
                                DonVi2 = donVi2,
                                TieuDe1 = "Chứng từ tổng hợp",
                                TieuDe2 = "Quyết toán ngân sách quốc phòng",
                                ThoiGian = string.Format("Ngày chứng từ: {0}", item.DNgayChungTu.ToString("dd/MM/yyyy")),
                                Items = _settlementVoucherDetailExports,
                                MLNS = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList()
                            };
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            foreach (System.Reflection.PropertyInfo prop in ctTongHop.GetType().GetProperties())
                            {
                                data.Add(prop.Name, prop.GetValue(ctTongHop));
                            }

                            fileNamePrefix = $"{item.SSoChungTu}_{StringUtils.ConvertVN(item.STenDonVi)}";
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            FlexCel.Core.ExcelFile xlsFile = _exportService.Export<SettlementVoucherDetailModel, NsMucLucNganSach>(templateFileName, data);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    else if (TabIndex == ImportTabIndex.Data)
                    {
                        List<SettlementVoucherModel> settlementVouchers = _settlementVouchers.Where(x => x.Selected).ToList();
                        foreach (SettlementVoucherModel item in settlementVouchers)
                        {
                            _settlementVoucherDetailExports = GetSettlementVoucherDetail(item);
                            CalculateData();
                            _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => x.FDuToan != 0 || x.FDaQuyetToan != 0
                                                                            || x.FTuChiDeNghi != 0 || x.FTuChiPheDuyet != 0
                                                                            || x.FChuyenNamSauChuaCap.GetValueOrDefault() != 0
                                                                            || x.FChuyenNamSauDaCap.GetValueOrDefault() != 0
                                                                            || x.FDeNghiChuyenNamSau != 0).OrderBy(x => x.SXauNoiMa).ToList();
                            _settlementVoucherDetailExports.ForEach(x => x.FTuChiDeNghi = x.FTuChiPheDuyet);
                            RptQuyetToanChungTuTongHop ctTongHop = new RptQuyetToanChungTuTongHop
                            {
                                DonVi1 = donVi1,
                                DonVi2 = donVi2,
                                TieuDe1 = "Chứng từ",
                                TieuDe2 = "Quyết toán ngân sách quốc phòng",
                                ThoiGian = string.Format("Ngày chứng từ: {0}", item.DNgayChungTu.ToString("dd/MM/yyyy")),
                                Items = _settlementVoucherDetailExports,
                                MLNS = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList()
                            };
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            foreach (System.Reflection.PropertyInfo prop in ctTongHop.GetType().GetProperties())
                            {
                                data.Add(prop.Name, prop.GetValue(ctTongHop));
                            }

                            fileNamePrefix = $"{item.SSoChungTu}_{StringUtils.ConvertVN(item.STenDonVi)}";
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            FlexCel.Core.ExcelFile xlsFile = _exportService.Export<SettlementVoucherDetailModel, NsMucLucNganSach>(templateFileName, data);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
                        _exportService.OpenEncrypt(result, ExportType.EXCEL);
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

        private List<SettlementVoucherDetailModel> GetSettlementVoucherDetailDepartment(SettlementVoucherModel settlementVoucher, string department)
        {
            IEnumerable<string> listMlns = _mucLucNganSachService.FindByCondition(n => n.NamLamViec == _sessionInfo.YearOfWork && n.IdPhongBan == department).Select(n => n.Lns);
            IEnumerable<string> listLns = settlementVoucher.SDslns.Split(StringUtils.COMMA).Where(x => listMlns.Contains(x));
            SettlementVoucherDetailSearch searchCondition = new SettlementVoucherDetailSearch
            {
                VoucherId = settlementVoucher.Id,
                LNS = string.Join(",", listLns),
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                Type = settlementVoucher.SLoai,
                BudgetSource = 1,
                AgencyId = settlementVoucher.IIdMaDonVi,
                VoucherDate = settlementVoucher.DNgayChungTu,
                UserName = _sessionInfo.Principal,
                QuarterMonth = settlementVoucher.IThangQuy.ToString(),
            };
            List<QtChungTuChiTietQuery> listChungTuChiTiet = _chungTuChiTietService.FindByCondition(searchCondition);
            return _mapper.Map<List<SettlementVoucherDetailModel>>(listChungTuChiTiet);
        }


        private List<SettlementVoucherDetailModel> GetSettlementVoucherDetail(SettlementVoucherModel settlementVoucher)
        {
            SettlementVoucherDetailSearch searchCondition = new SettlementVoucherDetailSearch
            {
                VoucherId = settlementVoucher.Id,
                LNS = string.Join(",", settlementVoucher.SDslns),
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                Type = settlementVoucher.SLoai,
                BudgetSource = 1,
                AgencyId = settlementVoucher.IIdMaDonVi,
                VoucherDate = settlementVoucher.DNgayChungTu,
                UserName = _sessionInfo.Principal,
                QuarterMonth = settlementVoucher.IThangQuy.ToString(),
            };
            List<QtChungTuChiTietQuery> listChungTuChiTiet = _chungTuChiTietService.FindByCondition(searchCondition);
            return _mapper.Map<List<SettlementVoucherDetailModel>>(listChungTuChiTiet);
        }

        private void OnImportData()
        {
            int voucherNoIndex = _chungTuService.CreateVoucherIndex(_condition);
            SettlementImportViewModel.VoucherNoIndex = voucherNoIndex;
            SettlementImportViewModel.SettlementImportType = SettlementType.DEFENSE_BUDGET;
            SettlementImportViewModel.SavedAction = obj =>
            {
                _settlementImportView.Close();
                OnRefresh();
                OpenDefenseBudgetDetailDialog((SettlementVoucherModel)obj);
            };
            SettlementImportViewModel.Init();
            _settlementImportView = new SettlementImport { DataContext = SettlementImportViewModel };
            _settlementImportView.ShowDialog();
        }

        private void CalculateData()
        {
            // Reset value parrent
            _settlementVoucherDetailExports.Where(x => x.IsHangCha)
                .Select(x => { x.FDuToan = x.FDuToanOrigin != 0 ? x.FDuToanOrigin : 0; x.FDaQuyetToan = 0; x.FTuChiDeNghi = 0; x.FTuChiPheDuyet = 0; x.FChuyenNamSauDaCap = 0; x.FDeNghiChuyenNamSau = 0; return x; }).ToList();
            // Caculate value child
            foreach (SettlementVoucherDetailModel item in _settlementVoucherDetailExports.Where(x => x.FDuToanOrigin != 0 || (x.IsEditable && (x.FDaQuyetToan != 0 || x.FTuChiPheDuyet != 0 || x.FTuChiDeNghi != 0 || x.FChuyenNamSauDaCap.GetValueOrDefault() != 0 || x.FChuyenNamSauChuaCap.GetValueOrDefault() != 0 || x.FDeNghiChuyenNamSau != 0))))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(SettlementVoucherDetailModel currentItem, SettlementVoucherDetailModel selfItem)
        {
            SettlementVoucherDetailModel parentItem = _settlementVoucherDetailExports.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parentItem == null) return;
            if (selfItem.FDuToanOrigin != 0)
                parentItem.FDuToan += selfItem.FDuToan;
            if (parentItem.FDuToan != 0 && currentItem.FDuToan == 0)
            {
                currentItem.IsCalculateConLai = false;
                OnPropertyChanged(nameof(currentItem.FConLai));
            }
            parentItem.FDaQuyetToan += selfItem.FDaQuyetToan;
            parentItem.FTuChiDeNghi += selfItem.FTuChiDeNghi;
            parentItem.FTuChiPheDuyet += selfItem.FTuChiPheDuyet;
            parentItem.FDeNghiChuyenNamSau += selfItem.FDeNghiChuyenNamSau;
            parentItem.FChuyenNamSauDaCap += selfItem.FChuyenNamSauDaCap.GetValueOrDefault();
            CalculateParent(parentItem, selfItem);
        }

        /*
         *  Thêm popup chọn tiêu chí để gửi dữ liệu
         */
        private async void OnUploadDialog(bool isSendHTTP)
        {
            //if (!SettlementVoucherSummaries.Any(n => n.Selected) || SettlementVoucherSummaries.Where(n => n.Selected).Count() > 1)
            //{
            //    StringBuilder messageBuilder = new StringBuilder();
            //    messageBuilder.AppendFormat("Vui lòng chọn duy nhất 1 bản ghi !");
            //    MessageBox.Show(messageBuilder.ToString());
            //    return;
            //}
            if (SettlementVoucherSummaries.Any(n => n.Selected))
            {
                string referenceQuarter = GetQuarter(SettlementVoucherSummaries.Where(n => n.Selected).FirstOrDefault().IThangQuy);
                foreach (SettlementVoucherModel item in SettlementVoucherSummaries.Where(n => n.Selected))
                {
                    if (GetQuarter(item.IThangQuy) != referenceQuarter)
                    {
                        StringBuilder messageBuilder = new StringBuilder();
                        messageBuilder.AppendFormat("Vui lòng chọn các bản ghi cùng 1 quý!");
                        MessageBox.Show(messageBuilder.ToString());
                        return;
                    }
                }
            }
            else
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng chọn ít nhất 1 bản ghi !");
                MessageBox.Show(messageBuilder.ToString());
                return;
            }
            IsLoading = true;
            try
            {
                (int, string) info = await _hTTPUploadFileService.GetToken(isSendHTTP);
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
            catch (ConfigurationErrorsException)
            {
                IsLoading = false;
                new NSMessageBoxViewModel("Cấu hình sai đường dẫn hoặc cổng HTTP").ShowDialogHost();
                return;
            }
            SendDataDefenseBudgetViewModel._settlementVoucherSummaries = _settlementVoucherSummaries;
            SendDataDefenseBudgetViewModel._settlementVouchers = _settlementVouchers;
            SendDataDefenseBudgetViewModel.TabIndex = TabIndex;
            SendDataDefenseBudgetViewModel.IsSendHTTP = isSendHTTP;
            SendDataDefenseBudgetViewModel.Init();
            SendDataDefenseBudgetViewModel.ClosePopup += RefreshAfterClosePopupSendData;
            View.Budget.Settlement.DefenseBudget.SendDataDefenseBudget.SendDataDefenseBudget addView = new View.Budget.Settlement.DefenseBudget.SendDataDefenseBudget.SendDataDefenseBudget() { DataContext = SendDataDefenseBudgetViewModel };
            IsLoading = false;
            DialogHost.Show(addView, SettlementScreen.ROOT_DIALOG, null, null);
        }

        private void RefreshAfterClosePopupSendData(object sender, EventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            OnRefresh();
        }

        private string GetQuarter(int thang)
        {
            if (thang >= 1 && thang <= 3)
            {
                return "1,2,3";
            }
            else if (thang >= 4 && thang <= 6)
            {
                return "4,5,6";
            }
            else if (thang >= 7 && thang <= 9)
            {
                return "7,8,9";
            }
            else if (thang >= 10 && thang <= 12)
            {
                return "10,11,12";
            }
            else
            {
                return "";
            }
        }

        //private async Task OnUpload()
        //{
        //    var departments = _phongBanService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork).Select(n => n.IIDMaBQuanLy);
        //    try
        //    {
        //        if (!SettlementVoucherSummaries.Any(n => n.Selected) || SettlementVoucherSummaries.Where(n => n.Selected).Count() > 1)
        //        {
        //            StringBuilder messageBuilder = new StringBuilder();
        //            messageBuilder.AppendFormat("Vui lòng chọn duy nhất 1 bản ghi !");
        //            MessageBox.Show(messageBuilder.ToString());
        //            return;
        //        }

        //        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_CHUNGTU_TONGHOP);
        //        string fileNamePrefix;
        //        string fileNameWithoutExtension;

        //        string donVi1 = _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper();
        //        string donVi2 = _sessionInfo.TenDonVi;
        //        List<SettlementVoucherModel> settlementVouchers = _settlementVoucherSummaries.Where(x => x.Selected && !string.IsNullOrEmpty(x.STongHop)).ToList();
        //        var item = settlementVouchers.FirstOrDefault(x => x.Selected);
        //        string token = await _hTTPUploadFileService.GetToken();
        //        string salt = _cryptographyService.GetSalt();
        //        string tokenKey = Scramble(token + salt);
        //        int count = 0;
        //        foreach (var department in departments)
        //        {
        //            _settlementVoucherDetailExports = GetSettlementVoucherDetailDepartment(item, department);
        //            if (!_settlementVoucherDetailExports.Any())
        //            {
        //                count++;
        //                continue;
        //            }
        //            CalculateData();
        //            _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => x.FDuToan != 0 || x.FDaQuyetToan != 0
        //                                                            || x.FTuChiDeNghi != 0 || x.FTuChiPheDuyet != 0).OrderBy(x => x.SXauNoiMa).ToList();
        //            _settlementVoucherDetailExports.ForEach(x => x.FTuChiDeNghi = x.FTuChiPheDuyet);
        //            RptQuyetToanChungTuTongHop ctTongHop = new RptQuyetToanChungTuTongHop
        //            {
        //                DonVi1 = donVi1,
        //                DonVi2 = donVi2,
        //                TieuDe1 = "Chứng từ tổng hợp",
        //                TieuDe2 = "Quyết toán ngân sách quốc phòng",
        //                ThoiGian = string.Format("Ngày chứng từ: {0}", item.DNgayChungTu.ToString("dd/MM/yyyy")),
        //                Items = _settlementVoucherDetailExports,
        //                MLNS = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList()
        //            };
        //            Dictionary<string, object> data = new Dictionary<string, object>();
        //            foreach (var prop in ctTongHop.GetType().GetProperties())
        //            {
        //                data.Add(prop.Name, prop.GetValue(ctTongHop));
        //            }

        //            fileNamePrefix = $"{item.SSoChungTu}_{StringUtils.ConvertVN(item.STenDonVi)}";
        //            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
        //            var xlsFile = _exportService.Export<SettlementVoucherDetailModel, NsMucLucNganSach>(templateFileName, data);
        //            var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);

        //            var sStage = "thang-" + item.IThangQuy;
        //            var sMaDonVi = $"{item.IIdMaDonVi}-{StringUtils.UCS2Convert(item.STenDonVi)}";

        //            var fileStream = new MemoryStream();
        //            var outputFileStream = new MemoryStream();
        //            _exportService.Open(Result, ref fileStream);

        //            await _cryptographyService.EncryptFile(fileStream, ref outputFileStream, tokenKey);
        //            await _hTTPUploadFileService.UploadFile(new FileUploadStreamModel()
        //            {
        //                File = outputFileStream,
        //                Name = fileNameWithoutExtension + FileExtensionFormats.Security,
        //                Description = "Chứng từ tổng hợp",
        //                Module = NSFunctionCode.BUDGET,
        //                ModuleName = "Ngân sách",
        //                SubModule = NSFunctionCode.BUDGET_SETTLEMENT_DEFENSE_BUDGET,
        //                SubModuleName = "Quyết toán ngân sách quốc phòng",
        //                TokenKey = tokenKey,
        //                YearOfWork = _sessionInfo.YearOfWork,
        //                YearOfBudget = _sessionInfo.YearOfBudget,
        //                SourceOfBudget = _sessionInfo.Budget,
        //                Department = department,
        //                Quarter = (item.IThangQuyLoai == (int)QuarterMonth.MONTH) ? item.IThangQuy.ToString() : string.Join(",", CreateThangFromQuy(item.IThangQuy))
        //            });
        //        }

        //        if (departments.Count() == count)
        //        {
        //            MessageBox.Show(new StringBuilder("Không có dữ liệu gửi").ToString());
        //            return;
        //        }

        //        MessageBox.Show(new StringBuilder("Gửi dữ liệu thành công").ToString());
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is System.Configuration.ConfigurationErrorsException)
        //        {
        //            MessageBox.Show(new StringBuilder("Cấu hình thông tin chưa đúng").ToString());
        //        }
        //        MessageBox.Show(new StringBuilder("Gửi dữ liệu thất bại").ToString());
        //        _logger.Error(ex.Message, ex);
        //        return;
        //    }
        //}
        private IEnumerable<int> CreateThangFromQuy(int quy)
        {
            int i = 3;
            while (i > 0)
            {
                yield return quy + 1 - (i--);
            }
        }
        private string Scramble(string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }


        private void OnImportJson()
        {
            SettlementImportJsonViewModel.Init();
            SettlementImportJsonViewModel.SLoai = SettlementType.DEFENSE_BUDGET;
            SettlementImportJsonViewModel.SavedAction = obj =>
            {
                OnRefresh();
                _importJsonView.Close();
            };
            _importJsonView = new SettlementImportJson { DataContext = SettlementImportJsonViewModel };
            _importJsonView.Show();
        }

        private void OnExportJson()
        {
            if (!SettlementVouchers.Any(n => n.Selected))
            {
                MessageBoxHelper.Error(Resources.MsgRecordEmpty);
                return;
            }
            List<NsQtChungTu> lstData = _chungTuService.GetDataExportJson(SettlementVouchers.Where(n => n.Selected).Select(n => n.Id).ToList());
            _exportService.OpenJson(lstData);
        }
    }
}
