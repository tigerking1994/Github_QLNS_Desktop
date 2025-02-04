using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.CollectionsBudget.Plan;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Import
{
    public class PlanBudgetBeginYearImportViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly ITnDtChungTuService _tnDtChungTuService;
        private readonly ITnDtdnChungTuService _tnDtdnChungTuService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly ITTnDanhMucLoaiHinhService _tTnDanhMucLoaiHinhService;
        private readonly ITnDtChungTuChiTietService _tnDtChungTuChiTietService;
        private readonly ITnDtdnChungTuChiTietService _tnDtdnChungTuChiTietService;
        private readonly IImpHistoryService _impHistoryService;
        private readonly IImpTnDtThuNopService _impTnDtThuNopService;
        private readonly ILog _logger;
        private readonly IConfiguration _configuration;
        private List<NsMucLucNganSach> _mucLucNganSachs;
        private List<TnDanhMucLoaiHinh> _muLucLoaiHinhs;
        private List<ImportErrorItem> _errors;
        private List<NsMuclucNgansachModel> _mergeItems;
        private List<TnDanhMucLoaiHinhModel> _mergeItemsLh;
        private string _importFolder;
        private ImpHistory _impHistory;
        public override Type ContentType => typeof(PlanBudgetBeginYearImport);
        public Action<TnQtChungTuModel> SavedRealBudgetAction;

        public override string FuncCode => NSFunctionCode.BUDGET_REVENUE_EXPENDITURE_IMPORT;

        public RevenueExpenditureImportType RevenueExpenditureImportTypes { get; set; }

        public Visibility VisibilityMonth
        {
            get => RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REAL_REVENUE_EXPENDITURE_IMPORT_EXPORT) ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility VisibilityUnit
        {
            get => RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.APPROVED_ESTIMATION_IMPORT_EXPORT) ? Visibility.Collapsed : Visibility.Visible;
        }

        public Visibility VisibilityDataGridRealBudget
        {
            get => RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REAL_REVENUE_EXPENDITURE_IMPORT_EXPORT) ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility VisibilityDataGridAnother
        {
            get => !(RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REAL_REVENUE_EXPENDITURE_IMPORT_EXPORT)
                    || RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REVENUE_EXPENDITURE_DIVISION_IMPORT_EXPORT))
                ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility VisibilityDataGridAnotherDetail
        {
            get => !(RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REAL_REVENUE_EXPENDITURE_IMPORT_EXPORT))
                ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility VisibilityDataGridDivision
        {
            get => RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REVENUE_EXPENDITURE_DIVISION_IMPORT_EXPORT) ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility VisibilityMLNS
        {
            get => !RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REAL_REVENUE_EXPENDITURE_IMPORT_EXPORT) ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility VisibilityLH
        {
            get => RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REAL_REVENUE_EXPENDITURE_IMPORT_EXPORT) ? Visibility.Visible : Visibility.Collapsed;
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
            set => SetProperty(ref _agencySelected, value);
        }

        private List<ComboboxItem> _dataDot;
        public List<ComboboxItem> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ComboboxItem _dataDotSelected;
        public ComboboxItem DataDotSelected
        {
            get => _dataDotSelected;
            set => SetProperty(ref _dataDotSelected, value);
        }



        private ObservableCollection<TnDtdnChungTuChiTietImportModel> _voucherDetails;
        public ObservableCollection<TnDtdnChungTuChiTietImportModel> VoucherDetails
        {
            get => _voucherDetails;
            set => SetProperty(ref _voucherDetails, value);
        }



        private TnDtdnChungTuChiTietImportModel _voucherDetailsItem;
        public TnDtdnChungTuChiTietImportModel VoucherDetailsItem
        {
            get => _voucherDetailsItem;
            set => SetProperty(ref _voucherDetailsItem, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (VoucherDetails != null && VoucherDetails.Count > 0)
                    return !VoucherDetails.Any(x => !x.ImportStatus);
                return false;
            }
        }

        private ObservableCollection<ComboboxItem> _quarterMonths = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> QuarterMonths
        {
            get => _quarterMonths;
            set => SetProperty(ref _quarterMonths, value);
        }

        private ComboboxItem _quarterMonthSelected;
        public ComboboxItem QuarterMonthSelected
        {
            get => _quarterMonthSelected;
            set
            {
                SetProperty(ref _quarterMonthSelected, value);
                LoadTimeOptions();
                OnPropertyChanged(nameof(TimeOptionSelected));
            }
        }

        private ObservableCollection<ComboboxItem> _timeOptions = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> TimeOptions
        {
            get => _timeOptions;
            set => SetProperty(ref _timeOptions, value);
        }

        private ComboboxItem _timeOptionSelected;
        public ComboboxItem TimeOptionSelected
        {
            get => _timeOptionSelected;
            set => SetProperty(ref _timeOptionSelected, value);
        }

        private ObservableCollection<NsMuclucNgansachModel> _existedMlns;
        public ObservableCollection<NsMuclucNgansachModel> ExistedMlns
        {
            get => _existedMlns;
            set => SetProperty(ref _existedMlns, value);
        }

        private ObservableCollection<TnDanhMucLoaiHinhModel> _existedLh;
        public ObservableCollection<TnDanhMucLoaiHinhModel> ExistedLh
        {
            get => _existedLh;
            set => SetProperty(ref _existedLh, value);
        }

        private ObservableCollection<NsMuclucNgansachModel> _importedMlns;
        public ObservableCollection<NsMuclucNgansachModel> ImportedMlns
        {
            get => _importedMlns;
            set => SetProperty(ref _importedMlns, value);
        }

        private ObservableCollection<TnDanhMucLoaiHinhModel> _importedLh;
        public ObservableCollection<TnDanhMucLoaiHinhModel> ImportedLh
        {
            get => _importedLh;
            set => SetProperty(ref _importedLh, value);
        }

        public bool IsEnabledMergeBtn
        {
            get => ImportedMlns.Where(i => i.IsSelected).Count() > 0 && ExistedMlns.Where(i => i.IsSelected).Count() == 1;
        }

        public bool IsEnabledMergeLhBtn
        {
            get => ImportedLh.Where(i => i.IsSelected).Count() > 0 && ExistedLh.Where(i => i.IsSelected).Count() == 1;
        }

        public bool IsEnabledUnmergeCommand
        {
            get => ExistedMlns.Where(i => i.IsModified && i.IsSelected).Count() > 0;
        }

        public bool IsEnabledUnmergeLhCommand
        {
            get => ExistedLh.Where(i => i.IsModified && i.IsSelected).Count() > 0;
        }

        private NsMuclucNgansachModel _selectedParent;
        public NsMuclucNgansachModel SelectedParent
        {
            get => _selectedParent;
            set
            {
                SetProperty(ref _selectedParent, value);
                OnPropertyChanged(nameof(IsEnabledMergeBtn));
            }
        }

        private TnDanhMucLoaiHinhModel _selectedParentLh;
        public TnDanhMucLoaiHinhModel SelectedParentLh
        {
            get => _selectedParentLh;
            set
            {
                SetProperty(ref _selectedParentLh, value);
                OnPropertyChanged(nameof(IsEnabledMergeLhBtn));
            }
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private string _soChungTu;
        public string SoChungTu
        {
            get => _soChungTu;
            set => SetProperty(ref _soChungTu, value);
        }

        private DateTime? _dNgayChungTu;
        public DateTime? DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }

        private string _thucHienThu;
        public string ThucHienThu
        {
            get => _thucHienThu;
            set
            {
                SetProperty(ref _thucHienThu, value);
            }
        }
        private string _duToanNam;
        public string DuToanNam
        {
            get => _duToanNam;
            set
            {
                SetProperty(ref _duToanNam, value);
            }
        }
        private string _uocThucHien;
        public string UocThucHien
        {
            get => _uocThucHien;
            set
            {
                SetProperty(ref _uocThucHien, value);
            }
        }
        private string _duToanThu;
        public string DuToanThu
        {
            get => _duToanThu;
            set
            {
                SetProperty(ref _duToanThu, value);
            }
        }

        public int ISoChungTuIndex { get; set; }

        public bool IsEnableSaveMLNS => _mergeItems.Count > 0;

        public bool IsEnableSaveLH => _mergeItemsLh.Count > 0;

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand AddMLNSCommand { get; }
        public RelayCommand MergeCommand { get; }
        public RelayCommand UnmergeCommand { get; }
        public RelayCommand SaveMLNSCommand { get; }
        public RelayCommand CloseCommand { get; }

        public PlanBudgetBeginYearImportViewModel(IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IImportExcelService importExcelService,
            ITnDtChungTuService tnDtChungTuService,
            ITnDtChungTuChiTietService tnDtChungTuChiTietService,
            ITnDtdnChungTuService tnDtdnChungTuService,
            ITnDtdnChungTuChiTietService tnDtdnChungTuChiTietService,
            INsMucLucNganSachService nsMucLucNganSachService,
            IImpHistoryService impHistoryService,
            IConfiguration configuration,
            IImpTnDtThuNopService impTnDtThuNopService,
            ITTnDanhMucLoaiHinhService tTnDanhMucLoaiHinhService,
            IImpTnQtThuNopService impTnQtThuNopService,
            ILog logger)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _donViService = nsDonViService;
            _importService = importExcelService;
            _tnDtChungTuService = tnDtChungTuService;
            _tnDtChungTuChiTietService = tnDtChungTuChiTietService;
            _tnDtdnChungTuService = tnDtdnChungTuService;
            _tnDtdnChungTuChiTietService = tnDtdnChungTuChiTietService;
            _mucLucNganSachService = nsMucLucNganSachService;
            _impHistoryService = impHistoryService;
            _configuration = configuration;
            _impTnDtThuNopService = impTnDtThuNopService;
            _tTnDanhMucLoaiHinhService = tTnDanhMucLoaiHinhService;
            _logger = logger;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            AddMLNSCommand = new RelayCommand(obj => OnAddMLNS());
            MergeCommand = new RelayCommand(obj => OnMerge());
            UnmergeCommand = new RelayCommand(obj => OnUnMerge());
            SaveMLNSCommand = new RelayCommand(obj => OnSaveMLNS());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
        }

        public override void Init()
        {
            _importFolder = _configuration.GetSection("ImportFolder").Value;
            if (!Directory.Exists(_importFolder))
                Directory.CreateDirectory(_importFolder);
            LoadHeader();
            LoadAgencies();
            LoadQuarterMonths();
            OnResetData();
        }

        private void LoadHeader()
        {
            var year = _sessionService.Current.YearOfWork;
            _thucHienThu = $"Thực hiện thu năm {year - 2}";
            _duToanNam = $"Dự toán năm {year - 1}";
            _uocThucHien = $"Ước thực hiện năm {year - 1}";
            _duToanThu = $"Dự toán thu năm kế hoạch {year}";
        }

        /// <summary>
        /// Tạo data cho combobox đơn vị
        /// </summary>
        private void LoadAgencies()
        {
            try
            {
                int namLamViec = _sessionService.Current.YearOfWork;
                List<DonVi> listDonVi = _donViService.FindByNamLamViec(namLamViec).ToList();
                _agencies = new List<ComboboxItem>();
                _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);

                OnPropertyChanged(nameof(Agencies));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadQuarterMonths()
        {
            List<ComboboxItem> expenseTypes = new List<ComboboxItem>()
            {
                new ComboboxItem { DisplayItem = "Tháng", ValueItem = RevenueExpenditureType.REAL_REVENUE_REPORT_MONTH_KEY },
                new ComboboxItem { DisplayItem = "Quý", ValueItem = RevenueExpenditureType.REAL_REVENUE_REPORT_QUATER_KEY }
            };

            QuarterMonths = new ObservableCollection<ComboboxItem>(expenseTypes);

            if (QuarterMonths.Count() > 0)
                _quarterMonthSelected = expenseTypes.ElementAt(0);

            LoadTimeOptions();
        }

        private void LoadTimeOptions()
        {
            List<ComboboxItem> timeOptionsTypes = new List<ComboboxItem>();

            if (_quarterMonthSelected != null && _quarterMonthSelected.ValueItem.Equals(RevenueExpenditureType.REAL_REVENUE_REPORT_MONTH_KEY))
            {
                for (int month = 1; month <= 12; month++)
                {
                    ComboboxItem cbxMonth = new ComboboxItem { DisplayItem = "Tháng " + month.ToString(), ValueItem = month.ToString() };
                    timeOptionsTypes.Add(cbxMonth);
                }
            }
            else if (_quarterMonthSelected != null && _quarterMonthSelected.ValueItem.Equals(RevenueExpenditureType.REAL_REVENUE_REPORT_QUATER_KEY))
            {
                int item = 0;
                for (int quater = 3; quater <= 12; quater += 3)
                {
                    item += 1;
                    ComboboxItem cbxQuater = new ComboboxItem
                    {
                        DisplayItem = "Quý " + (quater - (quater - item)).ToString(),
                        ValueItem = quater.ToString()
                    };
                    timeOptionsTypes.Add(cbxQuater);
                }
            }

            TimeOptions = new ObservableCollection<ComboboxItem>(timeOptionsTypes);

            if (TimeOptions.Count() > 0)
                _timeOptionSelected = timeOptionsTypes.ElementAt(0);
        }

        private void OnUploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Chọn file excel");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            FilePath = openFileDialog.FileName;
            _fileName = openFileDialog.SafeFileName;

            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(FilePath));
        }

        private void OnProcessFile()
        {
            try
            {
                string message = string.Empty;

                if (string.IsNullOrEmpty(_fileName))
                {
                    message = Resources.ErrorFileEmpty;
                    goto ShowError;
                }

                if (!RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.APPROVED_ESTIMATION_IMPORT_EXPORT))
                {
                    if (_agencySelected == null)
                    {
                        message = Resources.ErrorAgencyEmpty;
                        goto ShowError;
                    }
                }

                if (RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REAL_REVENUE_EXPENDITURE_IMPORT_EXPORT))
                {
                    if (_quarterMonthSelected == null || _timeOptionSelected == null)
                    {
                        message = Resources.MsgImportQuatersMonth;
                        goto ShowError;
                    }
                }

                if (RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REVENUE_EXPENDITURE_DIVISION_IMPORT_EXPORT))
                {
                    if (_dataDotSelected == null)
                    {
                        message = Resources.MsgDotNhanEmpty;
                        goto ShowError;
                    }
                }

            ShowError:
                if (!string.IsNullOrEmpty(message))
                {
                    System.Windows.MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _errors = new List<ImportErrorItem>();
                string destFile = Path.Combine(_importFolder, string.Format("{0}_{1}", DateTime.Now.ToString("ddMMyyyyhhmmss"), _fileName));
                File.Copy(FilePath, destFile);

                string strServiceCode = "Thu nộp ngân sách - Lập dự toán đầu năm";

                _impHistory = new ImpHistory
                {
                    FileName = _fileName,
                    FilePath = _importFolder,
                    ServiceCode = strServiceCode,
                    UserCreator = _sessionService.Current.Principal,
                    DateCreated = DateTime.Now
                };

                _impHistoryService.Add(_impHistory);


                //xử lý chứng từ chi tiết
                ImportResult<TnDtdnChungTuChiTietImportModel> _voucherDetailResult = _importService.ProcessData<TnDtdnChungTuChiTietImportModel>(FilePath);
                _voucherDetails = new ObservableCollection<TnDtdnChungTuChiTietImportModel>(_voucherDetailResult.Data);

                foreach (var item in _voucherDetails)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName != nameof(RevenueExpenditurePlanDetailImportModel.ImportStatus)
                            && args.PropertyName != nameof(RevenueExpenditurePlanDetailImportModel.ConcatenateCode)
                            && args.PropertyName != nameof(RevenueExpenditurePlanDetailImportModel.IsErrorMLNS))
                        {
                            var voucherDetail = (TnDtdnChungTuChiTietImportModel)sender;
                            int rowIndex = _voucherDetails.IndexOf(voucherDetail);
                            var listError = _importService.ValidateItem<TnDtdnChungTuChiTietImportModel>(voucherDetail, rowIndex);

                            if (listError.Count > 0)
                            {
                                List<string> errors = listError.Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                                string message = string.Join(Environment.NewLine, errors);
                                System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
                                _errors.AddRange(listError);
                                voucherDetail.ImportStatus = false;
                                if (listError.Any(x => x.IsErrorMLNS))
                                    voucherDetail.IsErrorMLNS = true;
                            }
                            else
                            {
                                voucherDetail.ImportStatus = true;
                                voucherDetail.IsErrorMLNS = false;
                                _errors.RemoveAll(x => x.Row == rowIndex);
                                OnPropertyChanged(nameof(IsSaveData));
                            }
                        }
                    };
                }

                OnPropertyChanged(nameof(VoucherDetails));
                if (_voucherDetailResult.ImportErrors.Count > 0)
                {
                    _errors.AddRange(_voucherDetailResult.ImportErrors);
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                OnPropertyChanged(nameof(IsSaveData));

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                if (ex is Utility.Exceptions.WrongReportException)
                {
                    System.Windows.MessageBox.Show(Resources.WrongReportFormat, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void OnSaveData()
        {
            try
            {

                TnDtdnChungTu chungTu = new TnDtdnChungTu();
                List<TnDtdnChungTuChiTiet> chungTuChiTiets = _mapper.Map<List<TnDtdnChungTuChiTiet>>(_voucherDetails.Where(x => x.ImportStatus && !x.BHangCha));

                chungTu.SSoChungTu = SoChungTu;
                chungTu.IIdMaDonVi = _agencySelected.ValueItem;
                chungTu.ISoChungTuIndex = ISoChungTuIndex;
                chungTu.SNguoiTao = _sessionService.Current.Principal;
                chungTu.DNgayTao = DateTime.Now;
                chungTu.INamLamViec = _sessionService.Current.YearOfWork;
                chungTu.INamNganSach = _sessionService.Current.YearOfBudget;
                chungTu.IIdMaNguonNganSach = _sessionService.Current.Budget;
                chungTu.Id = Guid.NewGuid();
                chungTu.SDSLNS = string.Join(",", chungTuChiTiets.Select(x => x.Lns).ToList());
                chungTu.DNgayChungTu = DNgayChungTu;
                chungTu.FTongDuToanNamKeHoach = chungTuChiTiets.Where(x => !x.BHangCha).Sum(s => s.FDuToanNamKeHoach);
                chungTu.FTongUocThucHienNamNay = chungTuChiTiets.Where(x => !x.BHangCha).Sum(s => s.FUocThucHienNamNay);
                chungTu.FTongDuToanNamNay = chungTuChiTiets.Where(x => !x.BHangCha).Sum(s => s.FDuToanNamNay);
                chungTu.FTongThucThuNamTruoc = chungTuChiTiets.Where(x => !x.BHangCha).Sum(s => s.FThucThuNamTruoc);
                chungTu.SDSSoChungTuTongHop = _sessionService.Current.IdDonVi.Contains(_agencySelected.ValueItem) ? string.Empty : null;
                _tnDtdnChungTuService.Add(chungTu);

                foreach (var item in chungTuChiTiets.Where(x => !x.BHangCha))
                {
                    item.IdChungTu = chungTu.Id;
                    item.INamLamViec = _sessionService.Current.YearOfWork;
                    item.INamNganSach = _sessionService.Current.YearOfBudget;
                    item.IIdMaNguonNganSach = _sessionService.Current.Budget;
                    item.IIdMaDonVi = _agencySelected.ValueItem;
                    item.STenDonVi = _agencySelected.DisplayItem;
                    item.SNguoiTao = _sessionService.Current.Principal;
                    item.DNgayTao = DateTime.Now;
                }
                _tnDtdnChungTuChiTietService.AddRange(chungTuChiTiets);
                TnDtdnChungTuModel settlementVoucher = _mapper.Map<TnDtdnChungTuModel>(chungTu);
                DialogHost.CloseDialogCommand.Execute(settlementVoucher, null);
                SavedAction?.Invoke(settlementVoucher);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private Expression<Func<TnDtdnChungTu, bool>> CreateImportPredicate()
        {
            var predicate = PredicateBuilder.True<TnDtdnChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            return predicate;
        }

        private void OnResetData()
        {
            try
            {
                _mergeItems = new List<NsMuclucNgansachModel>();
                _fileName = string.Empty;
                _filePath = string.Empty;
                _impHistory = new ImpHistory();
                _errors = new List<ImportErrorItem>();
                _agencySelected = null;
                _timeOptionSelected = null;
                _quarterMonthSelected = null;
                _tabIndex = ImportTabIndex.DataDetail;

                _voucherDetails = new ObservableCollection<TnDtdnChungTuChiTietImportModel>();

                _mucLucNganSachs = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
                _muLucLoaiHinhs = _tTnDanhMucLoaiHinhService.FindByLoaiHinh(_sessionService.Current.YearOfWork, NSEntityStatus.ACTIVED).ToList();
                _importedMlns = new ObservableCollection<NsMuclucNgansachModel>();
                _importedLh = new ObservableCollection<TnDanhMucLoaiHinhModel>();
                _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(_mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(_mucLucNganSachs.Where(x => x.L == "")));
                _existedLh = new ObservableCollection<TnDanhMucLoaiHinhModel>(_mapper.Map<ObservableCollection<TnDanhMucLoaiHinhModel>>(_muLucLoaiHinhs.Where(x => x.BLaHangCha)));
                var predicate = CreateImportPredicate();
                int soChungTuIndex = _tnDtdnChungTuService.FindNextSoChungTuIndex(predicate);

                SoChungTu = "LT-" + soChungTuIndex.ToString("D3");
                DNgayChungTu = DateTime.Now;
                ISoChungTuIndex = soChungTuIndex;
                OnPropertyChanged(nameof(AgencySelected));
                OnPropertyChanged(nameof(VoucherDetails));
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(ExistedMlns));
                OnPropertyChanged(nameof(ExistedLh));
                OnPropertyChanged(nameof(ImportedMlns));
                OnPropertyChanged(nameof(FileName));
                OnPropertyChanged(nameof(FilePath));
                OnPropertyChanged(nameof(TimeOptionSelected));
                OnPropertyChanged(nameof(QuarterMonthSelected));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ShowError()
        {
            int rowIndex = 0;
            if (!RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REAL_REVENUE_EXPENDITURE_IMPORT_EXPORT))
            {
                rowIndex = _voucherDetails.IndexOf(VoucherDetailsItem);
            }
            else
            {
                rowIndex = _voucherDetails.IndexOf(VoucherDetailsItem);
            }

            List<string> errors = _errors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnAddMLNS()
        {
            try
            {
                TabIndex = ImportTabIndex.MLNS;

                if (!RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REAL_REVENUE_EXPENDITURE_IMPORT_EXPORT))
                {
                    NsMuclucNgansachModel importItem = new NsMuclucNgansachModel();
                    if (ImportedMlns.Contains(importItem))
                        return;

                    if (!_importedMlns.Any(x => x.XauNoiMa.Contains(VoucherDetailsItem.ConcatenateCode))
                        && !_existedMlns.Any(x => x.XauNoiMa.Contains(VoucherDetailsItem.ConcatenateCode)))
                    {
                        _importedMlns.Add(new NsMuclucNgansachModel
                        {
                            Lns = VoucherDetailsItem.LNS,
                            L = VoucherDetailsItem.L,
                            K = VoucherDetailsItem.K,
                            M = VoucherDetailsItem.M,
                            TM = VoucherDetailsItem.Tm,
                            TTM = VoucherDetailsItem.Ttm,
                            NG = VoucherDetailsItem.Ng,
                            TNG = VoucherDetailsItem.Tng,
                            TNG1 = VoucherDetailsItem.Tng1,
                            TNG2 = VoucherDetailsItem.Tng2,
                            TNG3 = VoucherDetailsItem.Tng3,
                            XauNoiMa = VoucherDetailsItem.ConcatenateCode,
                            MoTa = VoucherDetailsItem.MoTa,
                            NamLamViec = _sessionService.Current.YearOfWork,
                            MlnsId = Guid.NewGuid(),
                            IsModified = true
                        });
                    }
                    foreach (NsMuclucNgansachModel model in _importedMlns.ToList())
                    {
                        NsMuclucNgansachModel parent = FindParent(model, _existedMlns);
                        if (parent != null)
                        {
                            int index = _existedMlns.IndexOf(parent);
                            _existedMlns.Insert(index + 1, model);
                            _importedMlns.Remove(model);
                            model.MlnsIdParent = parent.MlnsId;
                            model.BHangCha = false;
                            model.ITrangThai = 1;
                            model.SNguoiTao = _sessionService.Current.Principal;
                            model.DNgayTao = DateTime.Now;
                            _mergeItems.Add(model);
                            OnPropertyChanged(nameof(IsEnableSaveMLNS));
                        }
                    }
                }
                else
                {
                    TnDanhMucLoaiHinhModel importItem = new TnDanhMucLoaiHinhModel();
                    if (ImportedLh.Contains(importItem))
                        return;

                    _importedLh.Add(new TnDanhMucLoaiHinhModel
                    {
                        Lns = VoucherDetailsItem.LNS,
                        MoTa = VoucherDetailsItem.MoTa,
                        IdMaLoaiHinh = Guid.NewGuid(),
                        INamLamViec = _sessionService.Current.YearOfWork,
                        IsModified = true
                    });

                    foreach (TnDanhMucLoaiHinhModel model in _importedLh.ToList())
                    {
                        TnDanhMucLoaiHinhModel parent = FindParentLh(model, _existedLh);
                        if (parent != null)
                        {
                            int index = _existedLh.IndexOf(parent);
                            _existedLh.Insert(index + 1, model);
                            _importedLh.Remove(model);
                            _mergeItemsLh.Add(model);
                            OnPropertyChanged(nameof(IsEnableSaveLH));
                        }
                    }
                }

                OnPropertyChanged(nameof(ExistedMlns));
                OnPropertyChanged(nameof(ExistedLh));
                OnPropertyChanged(nameof(ImportedLh));
                OnPropertyChanged(nameof(IsEnableSaveLH));
                OnPropertyChanged(nameof(ImportedMlns));
                OnPropertyChanged(nameof(IsEnabledMergeBtn));
                OnPropertyChanged(nameof(IsEnabledMergeLhBtn));
                OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                OnPropertyChanged(nameof(IsEnabledUnmergeLhCommand));
                OnSelectionChanged();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnMerge()
        {
            try
            {
                if (!RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REAL_REVENUE_EXPENDITURE_IMPORT_EXPORT))
                {
                    if (SelectedParent == null)
                        return;
                    int index = _existedMlns.ToList().FindIndex(x => x.IsSelected);
                    _mergeItems = _importedMlns.Where(i => i.IsSelected && i.IsModified).ToList();
                    foreach (var item in _mergeItems)
                    {
                        item.MlnsIdParent = SelectedParent.MlnsId;
                        item.BHangCha = false;
                        item.ITrangThai = 1;
                        item.SNguoiTao = _sessionService.Current.Principal;
                        item.DNgayTao = DateTime.Now;
                    }

                    List<NsMuclucNgansachModel> nsMuclucNgansachModels = _existedMlns.ToList();
                    nsMuclucNgansachModels.InsertRange(index + 1, _mergeItems);
                    _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(nsMuclucNgansachModels);
                    _importedMlns = new ObservableCollection<NsMuclucNgansachModel>(ImportedMlns.Where(i => !i.IsSelected || !i.IsModified));
                }
                else
                {
                    if (SelectedParentLh == null)
                        return;
                    int index = _existedLh.ToList().FindIndex(x => x.IsSelected);
                    _mergeItemsLh = _importedLh.Where(i => i.IsSelected && i.IsModified).ToList();
                    foreach (var item in _mergeItemsLh)
                    {
                        item.IdMaLoaiHinhCha = SelectedParentLh.IdMaLoaiHinh;
                        item.BLaHangCha = false;
                        item.ITrangThai = 1;
                        item.UserCreator = _sessionService.Current.Principal;
                        item.DateCreated = DateTime.Now;
                    }

                    List<TnDanhMucLoaiHinhModel> nsMucLucLh = _existedLh.ToList();
                    nsMucLucLh.InsertRange(index + 1, _mergeItemsLh);
                    _existedLh = new ObservableCollection<TnDanhMucLoaiHinhModel>(nsMucLucLh);
                    _importedLh = new ObservableCollection<TnDanhMucLoaiHinhModel>(ImportedLh.Where(i => !i.IsSelected || !i.IsModified));
                }

                OnPropertyChanged(nameof(ExistedMlns));
                OnPropertyChanged(nameof(ImportedMlns));
                OnPropertyChanged(nameof(IsEnabledMergeBtn));
                OnPropertyChanged(nameof(IsEnabledMergeLhBtn));
                OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                OnPropertyChanged(nameof(IsEnabledUnmergeLhCommand));
                OnPropertyChanged(nameof(IsEnableSaveMLNS));
                OnPropertyChanged(nameof(IsEnableSaveLH));
                OnPropertyChanged(nameof(ExistedLh));
                OnPropertyChanged(nameof(ImportedLh));
                OnSelectionChanged();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public NsMuclucNgansachModel FindParent(NsMuclucNgansachModel model, IEnumerable<NsMuclucNgansachModel> ExistedMlns)
        {
            IEnumerable<NsMuclucNgansachModel> ancestors = _existedMlns.Where(i => !Guid.Empty.Equals(i.Id) && !model.XauNoiMa.Equals(i.XauNoiMa) &&
                model.XauNoiMa.StartsWith(i.XauNoiMa + "-") && model.NamLamViec == i.NamLamViec)
                .OrderByDescending(i => i.XauNoiMa.Length);
            return ancestors.FirstOrDefault();
        }

        public TnDanhMucLoaiHinhModel FindParentLh(TnDanhMucLoaiHinhModel model, IEnumerable<TnDanhMucLoaiHinhModel> ExistedLh)
        {
            IEnumerable<TnDanhMucLoaiHinhModel> ancestors = _existedLh.Where(i => !Guid.Empty.Equals(i.Id) && !model.Lns.Equals(i.Lns)
                                                            && model.Lns.Contains(i.Lns) && model.INamLamViec == i.INamLamViec);
            return ancestors.FirstOrDefault();
        }

        private void OnSelectionChanged()
        {
            foreach (var i in _existedMlns)
            {
                i.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(ModelBase.IsSelected))
                    {
                        OnPropertyChanged(nameof(IsEnabledMergeBtn));
                        OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                        OnPropertyChanged(nameof(IsEnabledUnmergeLhCommand));
                        OnPropertyChanged(nameof(IsEnabledMergeLhBtn));
                    }
                };
            }

            foreach (var i in _importedMlns.Where(x => x.IsModified))
            {
                i.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(ModelBase.IsSelected))
                    {
                        OnPropertyChanged(nameof(IsEnabledMergeBtn));
                        OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                        OnPropertyChanged(nameof(IsEnabledUnmergeLhCommand));
                        OnPropertyChanged(nameof(IsEnabledMergeLhBtn));
                    }
                };
            }

            foreach (var i in _existedLh)
            {
                i.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(ModelBase.IsSelected))
                    {
                        OnPropertyChanged(nameof(IsEnabledMergeBtn));
                        OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                        OnPropertyChanged(nameof(IsEnabledUnmergeLhCommand));
                        OnPropertyChanged(nameof(IsEnabledMergeLhBtn));
                        OnPropertyChanged(nameof(IsEnableSaveLH));
                        OnPropertyChanged(nameof(IsEnableSaveMLNS));
                    }
                };
            }

            foreach (var i in _importedLh.Where(x => x.IsModified))
            {
                i.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(ModelBase.IsSelected))
                    {
                        OnPropertyChanged(nameof(IsEnabledMergeBtn));
                        OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                        OnPropertyChanged(nameof(IsEnabledUnmergeLhCommand));
                        OnPropertyChanged(nameof(IsEnabledMergeLhBtn));
                        OnPropertyChanged(nameof(IsEnableSaveLH));
                        OnPropertyChanged(nameof(IsEnableSaveMLNS));
                    }
                };
            }
        }

        private void OnUnMerge()
        {
            try
            {
                if (!RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REAL_REVENUE_EXPENDITURE_IMPORT_EXPORT))
                {
                    IEnumerable<NsMuclucNgansachModel> unmergeItems = _existedMlns.Where(i => i.IsSelected && i.IsModified).ToList();
                    foreach (var item in unmergeItems)
                    {
                        _mergeItems.Remove(item);
                    }
                    List<NsMuclucNgansachModel> nsMuclucNgansachModels = ImportedMlns.ToList();
                    nsMuclucNgansachModels.AddRange(unmergeItems);
                    _importedMlns = new ObservableCollection<NsMuclucNgansachModel>(nsMuclucNgansachModels);
                    _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(_existedMlns.Where(i => !i.IsSelected || !i.IsModified));
                }
                else
                {
                    IEnumerable<TnDanhMucLoaiHinhModel> unmergeItems = _existedLh.Where(i => i.IsSelected && i.IsModified).ToList();
                    foreach (var item in unmergeItems)
                    {
                        _mergeItemsLh.Remove(item);
                    }
                    List<TnDanhMucLoaiHinhModel> nsMucLucLh = ImportedLh.ToList();
                    nsMucLucLh.AddRange(unmergeItems);
                    _importedLh = new ObservableCollection<TnDanhMucLoaiHinhModel>(nsMucLucLh);
                    _existedLh = new ObservableCollection<TnDanhMucLoaiHinhModel>(_existedLh.Where(i => !i.IsSelected || !i.IsModified));
                }

                OnPropertyChanged(nameof(ExistedMlns));
                OnPropertyChanged(nameof(ImportedMlns));
                OnPropertyChanged(nameof(IsEnabledMergeBtn));
                OnPropertyChanged(nameof(IsEnabledMergeLhBtn));
                OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                OnPropertyChanged(nameof(IsEnabledUnmergeLhCommand));
                OnPropertyChanged(nameof(IsEnableSaveMLNS));
                OnPropertyChanged(nameof(IsEnableSaveLH));
                OnPropertyChanged(nameof(ExistedLh));
                OnPropertyChanged(nameof(ImportedLh));
                OnSelectionChanged();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnSaveMLNS()
        {
            var result = System.Windows.MessageBox.Show(Resources.ConfirmAddMLNS, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (!RevenueExpenditureImportTypes.Equals(RevenueExpenditureImportType.REAL_REVENUE_EXPENDITURE_IMPORT_EXPORT))
                    {
                        List<NsMucLucNganSach> listMLNS = _mapper.Map<List<NsMucLucNganSach>>(_mergeItems);
                        _mucLucNganSachService.AddRange(listMLNS);
                        _existedMlns.Where(x => x.IsModified).Select(x => { x.IsModified = false; x.IsSelected = false; return x; }).ToList();
                        _mergeItems = new List<NsMuclucNgansachModel>();
                        OnPropertyChanged(nameof(ExistedMlns));
                        OnPropertyChanged(nameof(IsEnableSaveMLNS));
                        OnPropertyChanged(nameof(IsEnableSaveLH));
                        System.Windows.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                        var listError = _importService.ValidateItem<TnDtdnChungTuChiTietImportModel>(VoucherDetailsItem, _voucherDetails.IndexOf(VoucherDetailsItem));
                        if (listError.Count == 0)
                        {
                            VoucherDetailsItem.ImportStatus = true;
                            VoucherDetailsItem.IsErrorMLNS = false;
                            TabIndex = ImportTabIndex.Data;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    }
                    else
                    {
                        List<TnDanhMucLoaiHinh> listMLNS = _mapper.Map<List<TnDanhMucLoaiHinh>>(_mergeItemsLh);
                        _tTnDanhMucLoaiHinhService.AddRange(listMLNS);
                        _existedMlns.Where(x => x.IsModified).Select(x => { x.IsModified = false; x.IsSelected = false; return x; }).ToList();
                        _mergeItems = new List<NsMuclucNgansachModel>();
                        OnPropertyChanged(nameof(ExistedMlns));
                        OnPropertyChanged(nameof(IsEnableSaveMLNS));
                        OnPropertyChanged(nameof(IsEnableSaveLH));
                    }

                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                    System.Windows.MessageBox.Show(Resources.MsgSaveError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}
