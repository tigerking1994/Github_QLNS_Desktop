using AutoMapper;
using ControlzEx.Standard;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Shared.Import;
using VTS.QLNS.CTC.App.ViewModel.Shared.ImportViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Import
{
    public class SettlementImportViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IMapper _mapper;
        private readonly IImportExcelService _importService;
        private readonly INsQtChungTuService _chungTuService;
        private readonly INsQtChungTuChiTietService _chungTuChiTietService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly ICryptographyService _iCryptographyService;
        private readonly IConfiguration _configuration;
        private readonly IImpHistoryService _impHistoryService;
        private readonly IImpQuyetToanService _impQuyetToanService;
        private readonly FtpStorageService _ftpStorageService;
        private readonly IHTTPUploadFileService _hTTPUploadFileService;
        private SessionInfo _sessionInfo;
        private List<NsMucLucNganSach> _mucLucNganSachs;
        private ImpHistory _impHistory;
        private List<ImportErrorItem> _errors;
        private List<NsMuclucNgansachModel> _mergeItems;
        private string _importFolder;
        private string _fileName;
        private List<SettlementVoucherDetailImportModel> _settlementVoucherDetailProcess;
        private SuggestMlnsViewModel _suggestMlnsViewmodel;

        public override string Name => "Ngân sách thường xuyên";
        public override Type ContentType => typeof(View.Budget.Settlement.Import.SettlementImport);
        public override string Description => "Quyết toán Lương, Phụ cấp, Trợ cấp, Tiền ăn";
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public int VoucherNoIndex;
        public string SettlementImportType;
        public bool IsSendHTTP;
        public string SettlementName
        {
            get
            {
                switch (SettlementImportType)
                {
                    case SettlementType.REGULAR_BUDGET:
                        return "Import dữ liệu ngân sách thường xuyên";
                    case SettlementType.DEFENSE_BUDGET:
                        return "Import dữ liệu ngân sách quốc phòng";
                    case SettlementType.STATE_BUDGET:
                        return "Import dữ liệu ngân sách nhà nước";
                    case SettlementType.FOREX_BUDGET:
                        return "Import dữ liệu ngân sách ngoại hối";
                    case SettlementType.EXPENSE_BUDGET:
                        return "Import dữ liệu ngân sách khác";
                    default:
                        return "Import dữ liệu";
                }
            }
        }

        private ObservableCollection<SettlementVoucherDetailImportModel> _settlementVoucherDetails;
        public ObservableCollection<SettlementVoucherDetailImportModel> SettlementVoucherDetails
        {
            get => _settlementVoucherDetails;
            set => SetProperty(ref _settlementVoucherDetails, value);
        }

        private SettlementVoucherDetailImportModel _selectedItem;
        public SettlementVoucherDetailImportModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        private string _filePathSecurity;

        public string FilePathSecurity
        {
            get => _filePathSecurity;
            set => SetProperty(ref _filePathSecurity, value);
        }

        //private List<ComboboxItem> _months;
        //public List<ComboboxItem> Months
        //{
        //    get => _months;
        //    set => SetProperty(ref _months, value);
        //}

        private List<ComboboxItem> _quarterMonths;
        public List<ComboboxItem> QuarterMonths
        {
            get => _quarterMonths;
            set => SetProperty(ref _quarterMonths, value);
        }

        //private ComboboxItem _monthSelected;
        //public ComboboxItem MonthSelected
        //{
        //    get => _monthSelected;
        //    set => SetProperty(ref _monthSelected, value);
        //}

        private ComboboxItem _quarterMonthSelected;
        public ComboboxItem QuarterMonthSelected
        {
            get => _quarterMonthSelected;
            set => SetProperty(ref _quarterMonthSelected, value);
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

        private DateTime _voucherDate;
        public DateTime VoucherDate
        {
            get => _voucherDate;
            set => SetProperty(ref _voucherDate, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (SettlementVoucherDetails.Count > 0)
                    return !SettlementVoucherDetails.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }

        private ObservableCollection<NsMuclucNgansachModel> _existedMlns;
        public ObservableCollection<NsMuclucNgansachModel> ExistedMlns
        {
            get => _existedMlns;
            set => SetProperty(ref _existedMlns, value);
        }

        private ObservableCollection<NsMuclucNgansachModel> _importedMlns;
        public ObservableCollection<NsMuclucNgansachModel> ImportedMlns
        {
            get => _importedMlns;
            set => SetProperty(ref _importedMlns, value);
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

        public bool IsEnabledMergeBtn
        {
            get => ImportedMlns.Where(i => i.IsSelected).Count() > 0 && ExistedMlns.Where(i => i.IsSelected).Count() == 1;
        }

        public bool IsEnabledUnmergeCommand
        {
            get => ExistedMlns.Where(i => i.IsModified && i.IsSelected).Count() > 0;
        }

        public bool IsEnableSaveMLNS => _mergeItems.Count > 0;

        private ObservableCollection<FileFilterQuery> _lstFile;
        public ObservableCollection<FileFilterQuery> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }

        private ImportResult<SettlementVoucherDetailImportModel> _voucherDetailResult;
        public ImportResult<SettlementVoucherDetailImportModel> VoucherDetailResult
        {
            get => _voucherDetailResult;
            set
            {
                SetProperty(ref _voucherDetailResult, value);
                OnPropertyChanged(nameof(_voucherDetailResult));
            }
        }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand AddMLNSCommand { get; }
        public RelayCommand MergeCommand { get; }
        public RelayCommand UnmergeCommand { get; }
        public RelayCommand SaveMLNSCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand SuggestMlnsCommand { get; }
        public RelayCommand GetFileFtpCommandHTTP { get; }
        public RelayCommand GetFileFtpCommandFTP { get; }
        public RelayCommand DownloadFileFtpServer { get; }

        public SettlementImportViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            INsQtChungTuService chungTuService,
            INsQtChungTuChiTietService chungTuChiTietService,
            INsMucLucNganSachService mucLucNganSachService,
            IConfiguration configuration,
            IImpHistoryService impHistoryService,
            ICryptographyService iCryptographyService,
            IImpQuyetToanService impQuyetToanService,
            FtpStorageService ftpStorageService,
            SuggestMlnsViewModel suggestMlnsViewModel,
            IHTTPUploadFileService hTTPUploadFileService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _iCryptographyService = iCryptographyService;
            _mucLucNganSachService = mucLucNganSachService;
            _configuration = configuration;
            _impHistoryService = impHistoryService;
            _impQuyetToanService = impQuyetToanService;
            _ftpStorageService = ftpStorageService;
            _hTTPUploadFileService = hTTPUploadFileService;
            _suggestMlnsViewmodel = suggestMlnsViewModel;

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
            SuggestMlnsCommand = new RelayCommand(obj => SuggestSelectMlns());
            GetFileFtpCommandHTTP = new RelayCommand(async obj => await OnGetFileFtpCommand(true));
            GetFileFtpCommandFTP = new RelayCommand(async obj => await OnGetFileFtpCommand(false));
            DownloadFileFtpServer = new RelayCommand(async obj => await OnDownloadFileFtpServer(obj));
        }

        private void LoadQuarterMonths()
        {
            _quarterMonths = new List<ComboboxItem>();
            _quarterMonths.Add(new ComboboxItem("Quý I", "3"));
            _quarterMonths.Add(new ComboboxItem("Quý II", "6"));
            _quarterMonths.Add(new ComboboxItem("Quý III", "9"));
            _quarterMonths.Add(new ComboboxItem("Quý IV", "12"));
            for (int i = 1; i <= 12; i++)
            {
                _quarterMonths.Add(new ComboboxItem("Tháng " + i, i.ToString()));
            }
            QuarterMonthSelected = _quarterMonths.First();
        }

        public override void Init()
        {
            base.Init();
            LoadQuarterMonths();
            //_months = FnCommonUtils.LoadMonths();
            _sessionInfo = _sessionService.Current;
            _voucherDate = DateTime.Now;
            _importFolder = _configuration.GetSection("ImportFolder").Value;
            if (!Directory.Exists(_importFolder))
                Directory.CreateDirectory(_importFolder);

            LoadAgencies();
            OnResetData();
        }

        private void OnResetData()
        {
            _mergeItems = new List<NsMuclucNgansachModel>();
            _filePath = string.Empty;
            FilePathSecurity = string.Empty;
            QuarterMonthSelected = null;
            //_monthSelected = null;
            _agencySelected = null;
            _impHistory = new ImpHistory();
            _errors = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;

            _mucLucNganSachs = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
            _importedMlns = new ObservableCollection<NsMuclucNgansachModel>();
            _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(_mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(_mucLucNganSachs));
            _settlementVoucherDetails = new ObservableCollection<SettlementVoucherDetailImportModel>();
            LstFile = new ObservableCollection<FileFilterQuery>();

            OnPropertyChanged(nameof(FilePath));
            //OnPropertyChanged(nameof(MonthSelected));
            OnPropertyChanged(nameof(AgencySelected));
            OnPropertyChanged(nameof(SettlementVoucherDetails));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(TabIndex));
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(LstFile));
        }

        /// <summary>
        /// Tạo data cho combobox đơn vị
        /// </summary>
        private void LoadAgencies()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork).And(x => x.Loai == AgencyType.LEVEL1.ToString("D"));
            List<DonVi> listDonVi = _donViService.FindByCondition(predicate).ToList();
            _agencies = new List<ComboboxItem>();
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
        }

        private void OnUploadFileBackup()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Chọn file excel");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".xlsx";
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            FilePath = openFileDialog.FileName;
            _fileName = openFileDialog.SafeFileName;
        }

        private void OnUploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Chọn file mã hóa");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".qlns";
            openFileDialog.Filter = "Security Files|*.qlns;*.security;*.cryptography";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            var pathDir = Path.Combine(IOExtensions.ApplicationPath, ConstantUrlPathPhanHe.UrlFolderTemp);
            IOExtensions.CreateDirectoryIfNotExists(pathDir);
            var pathExcel = Path.Combine(pathDir, "temp.xlsx");
            _iCryptographyService.DecryptFile(openFileDialog.FileName, pathExcel);
            FilePath = pathExcel;
            FilePathSecurity = openFileDialog.FileName;
            _fileName = openFileDialog.SafeFileName;
        }

        private void OnProcessFile(bool isLoadFromServer = false)
        {
            if (!isLoadFromServer)
            {
                string message = string.Empty;
                if (string.IsNullOrEmpty(FilePath))
                {
                    message = Resources.ErrorFileEmpty;
                    goto ShowError;
                }
                if (_quarterMonthSelected == null)
                {
                    message = Resources.ErrorMonthEmpty;
                    goto ShowError;
                }
                if (_agencySelected == null)
                {
                    message = Resources.ErrorAgencyEmpty;
                    goto ShowError;
                }
                if (string.IsNullOrEmpty(_voucherDate.ToString()))
                {
                    message = Resources.ErrorVoucherDateEmpty;
                    goto ShowError;
                }
            ShowError:
                if (!string.IsNullOrEmpty(message))
                {
                    MessageBoxHelper.Warning(message);
                    return;
                }

                _errors = new List<ImportErrorItem>();
                //save file to import folder
                string destFile = Path.Combine(_importFolder, string.Format("{0}_{1}", DateTime.Now.ToString("ddMMyyyyhhmmss"), _fileName));
                File.Copy(FilePath, destFile);
                _impHistory = new ImpHistory
                {
                    FileName = _fileName,
                    FilePath = _importFolder,
                    ServiceCode = "Quyết toán",
                    TableName = "QT_ChungTu",
                    UserCreator = _sessionInfo.Principal,
                    DateCreated = DateTime.Now
                };
                _impHistoryService.Add(_impHistory);
            }            

            try
            {
                //xử lý chứng từ chi tiết
                ImportResult<SettlementVoucherDetailImportModel> _voucherDetailResult = isLoadFromServer ? VoucherDetailResult : _importService.ProcessData<SettlementVoucherDetailImportModel>(FilePath);
                _settlementVoucherDetailProcess = new List<SettlementVoucherDetailImportModel>();
                //xác định cha con trong data import
                foreach (var item in _voucherDetailResult.Data)
                {
                    if (!item.IsErrorMLNS)
                    {
                        var mlns = _mucLucNganSachs.Where(x => x.XauNoiMa == item.ConcatenateCode).FirstOrDefault();
                        if (mlns != null)
                            item.BHangCha = mlns.BHangChaQuyetToan.HasValue ? mlns.BHangChaQuyetToan.Value : false;
                    }
                    var childs = _voucherDetailResult.Data.Where(x => x.ConcatenateCode.Contains(item.ConcatenateCode) && x != item).ToList();
                    if (childs.Count > 0)
                        item.BHangCha = true;
                }

                //kiểm tra loại ngân sách con thỏa mãn điều kiện để import
                foreach (var item in _voucherDetailResult.Data)
                {
                    if (!item.ImportStatus && item.IsErrorMLNS)
                    {
                        var parents = _settlementVoucherDetailProcess.Where(x => item.ConcatenateCode.Contains(x.ConcatenateCode) && x != item).ToList();
                        if (parents.Count > 0)
                        {
                            int columnIndexOrigin = 0;
                            SettlementVoucherDetailImportModel parent = new SettlementVoucherDetailImportModel();
                            foreach (var p in parents)
                            {
                                int maxColumn = p.ConcatenateCode.Split("-").Count();
                                if (maxColumn > columnIndexOrigin)
                                {
                                    columnIndexOrigin = maxColumn;
                                    parent = p;
                                }
                            }
                            int columnIndexImport = item.ConcatenateCode.Split("-").Count();
                            if (columnIndexOrigin < columnIndexImport)
                            {
                                if (!string.IsNullOrEmpty(parent.Suggestion))
                                    parent.Suggestion = "0";
                                if (!string.IsNullOrEmpty(parent.Day))
                                    parent.Day = "0";
                                if (!string.IsNullOrEmpty(parent.People))
                                    parent.People = "0";
                                if (!string.IsNullOrEmpty(parent.Bout))
                                    parent.Bout = "0";
                                if (!string.IsNullOrEmpty(parent.FDeNghiChuyenNamSau))
                                    parent.FDeNghiChuyenNamSau = "0";
                                if (!string.IsNullOrEmpty(parent.FChuyenNamSauDaCap))
                                    parent.FChuyenNamSauDaCap = "0";
                                if (!string.IsNullOrEmpty(parent.FChuyenNamSauChuaCap))
                                    parent.FChuyenNamSauChuaCap = "0";
                                if (parent.ListConcatenateCodeChild == null)
                                    parent.ListConcatenateCodeChild = new List<string>();
                                parent.ListConcatenateCodeChild.Add(item.ConcatenateCode);
                                item.ConcatenateCodeParent = parent.ConcatenateCode;

                                var parentOrigin = _mucLucNganSachs.Where(x => x.XauNoiMa == parent.ConcatenateCode).FirstOrDefault();
                                if (parentOrigin != null && !parentOrigin.BHangCha)
                                {
                                    item.IsWarning = true;
                                }
                                if (parent.IsWarning)
                                    item.IsWarning = true;
                            }
                        }
                    }
                    _settlementVoucherDetailProcess.Add(item);
                }

                CalculateData();
                _settlementVoucherDetails = new ObservableCollection<SettlementVoucherDetailImportModel>(_settlementVoucherDetailProcess);
                foreach (var item in _settlementVoucherDetails)
                {
                    item.PropertyChanged += settlementVoucherDetailPropertyChanged;
                }
                OnPropertyChanged(nameof(SettlementVoucherDetails));
                if (_voucherDetailResult.ImportErrors.Count > 0)
                    _errors.AddRange(_voucherDetailResult.ImportErrors);
                if (SettlementVoucherDetails.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    MessageBoxHelper.Warning(Resources.AlertDataError);
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch
            {
                MessageBoxHelper.Warning(Resources.ErrorImport);
            }
        }

        private void settlementVoucherDetailPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(SettlementVoucherDetailImportModel.ImportStatus)
                && args.PropertyName != nameof(SettlementVoucherDetailImportModel.ConcatenateCode)
                && args.PropertyName != nameof(SettlementVoucherDetailImportModel.IsErrorMLNS))
            {
                var voucherDetail = (SettlementVoucherDetailImportModel)sender;
                int rowIndex = _settlementVoucherDetails.IndexOf(voucherDetail);
                var listError = _importService.ValidateItem<SettlementVoucherDetailImportModel>(voucherDetail, rowIndex);
                if (listError.Count > 0)
                {
                    List<string> errors = listError.Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                    string message = string.Join(Environment.NewLine, errors);
                    MessageBoxHelper.Info(message, "Thông tin lỗi");
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
        }

        private void OnSaveData()
        {
            var message = string.Empty;
            if (QuarterMonthSelected == null) message = Resources.ErrorMonthEmpty;
            if (_agencySelected == null) message = Resources.ErrorAgencyEmpty;

            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Warning(message);
                return;
            }
            _mucLucNganSachs = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
            List<NsQtChungTuChiTiet> chungTuChiTiets = _mapper.Map<List<NsQtChungTuChiTiet>>(_settlementVoucherDetails.Where(x => x.ImportStatus && !x.IsWarning && _mucLucNganSachs.Any(y => y.XauNoiMa == x.ConcatenateCode && y.BHangChaQuyetToan.HasValue && !y.BHangChaQuyetToan.Value)));
            List<ImpQuyetToan> quyetToans = _mapper.Map<List<ImpQuyetToan>>(_settlementVoucherDetails.Where(x => x.ImportStatus && !x.IsWarning));

            //kiểm tra tồn tại chứng từ theo đơn vị, tháng, LNS

            if (QuarterMonthSelected.DisplayItem.StartsWith("Quý"))
            {
                List<string> listLNSExist = _chungTuService.FindLNSExist(
                new SettlementVoucherCriteria
                {
                    SettlementType = SettlementImportType,
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    BudgetSource = _sessionInfo.Budget,
                    QuarterMonth = Convert.ToInt32(QuarterMonthSelected.ValueItem),
                    QuarterMonthType = 1,
                    AgencyId = _agencySelected.ValueItem,
                    AdjustType = 1
                }
                , Guid.Empty, chungTuChiTiets.Select(x => x.SLns).Distinct().ToList());
                if (listLNSExist.Count > 0)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.AlertExistSettlementQuarterVoucher, _agencySelected.DisplayItem, QuarterMonthSelected.DisplayItem, string.Join(",", listLNSExist)));
                    return;
                }
            }
            else
            {
                List<string> listLNSExist = _chungTuService.FindLNSExist(
                new SettlementVoucherCriteria
                {
                    SettlementType = SettlementImportType,
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    BudgetSource = _sessionInfo.Budget,
                    QuarterMonth = Convert.ToInt32(QuarterMonthSelected.ValueItem),
                    QuarterMonthType = 0,
                    AgencyId = _agencySelected.ValueItem,
                    AdjustType = 1
                }
                , Guid.Empty, chungTuChiTiets.Select(x => x.SLns).Distinct().ToList());
                if (listLNSExist.Count > 0)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.AlertExistSettlementMonthVoucher, _agencySelected.DisplayItem, QuarterMonthSelected.DisplayItem, string.Join(",", listLNSExist)));
                    return;
                }
            }

            string errorMess = string.Empty;
            switch (SettlementImportType)
            {
                case SettlementType.REGULAR_BUDGET:
                    var listLNS = new List<string> { "1010000", "1010001", "1010002", "1010100", "1010200", "1010300", "1010400" };
                    if (chungTuChiTiets.Select(x => x.SLns).Any(x => !listLNS.Contains(x)))
                        errorMess = "Dữ liệu ngân sách thường xuyên phải thuộc loại ngân sách 1010000, 1010001, 1010002, 1010100, 1010200, 1010300, 1010400";
                    break;
                case SettlementType.DEFENSE_BUDGET:
                    if (chungTuChiTiets.Select(x => x.SLns).Any(x => !x.StartsWith("1")))
                        errorMess = "Dữ liệu ngân sách quốc phòng phải thuộc loại ngân sách 1";
                    else if (chungTuChiTiets.Select(x => x.SLns).Any(x => x == "1010000"))
                        errorMess = "Dữ liệu ngân sách quốc phòng không chứa loại ngân sách 1010000";
                    break;
                case SettlementType.STATE_BUDGET:
                    if (chungTuChiTiets.Select(x => x.SLns).Any(x => !x.StartsWith("2")))
                        errorMess = "Dữ liệu ngân sách nhà nước phải thuộc loại ngân sách 2";
                    break;
                case SettlementType.FOREX_BUDGET:
                    if (chungTuChiTiets.Select(x => x.SLns).Any(x => !x.StartsWith("3")))
                        errorMess = "Dữ liệu ngân sách ngoại hối phải thuộc loại ngân sách 3";
                    break;
                case SettlementType.EXPENSE_BUDGET:
                    if (chungTuChiTiets.Select(x => x.SLns).Any(x => x.StartsWith("1") || x.StartsWith("2") || x.StartsWith("3")))
                        errorMess = "Dữ liệu ngân sách khác phải khác loại ngân sách 1, 2, 3";
                    break;
            }
            if (!string.IsNullOrEmpty(errorMess))
            {
                MessageBoxHelper.Warning(errorMess, "Thông tin lỗi");
                return;
            }
            NsQtChungTu chungTu = new NsQtChungTu();
            chungTu.ISoChungTuIndex = VoucherNoIndex;
            chungTu.SSoChungTu = "QT-" + VoucherNoIndex.ToString().PadLeft(3, '0');
            chungTu.DNgayChungTu = _voucherDate;
            chungTu.IIdMaDonVi = _agencySelected.ValueItem;
            chungTu.SDslns = string.Join(",", chungTuChiTiets.Select(x => x.SLns).Distinct());
            chungTu.SLoai = SettlementImportType;
            chungTu.IThangQuyLoai = QuarterMonthSelected.DisplayItem.Contains("Quý") ? 1 : 0;
            chungTu.IThangQuy = int.Parse(QuarterMonthSelected.ValueItem);
            chungTu.SThangQuyMoTa = QuarterMonthSelected.DisplayItem;
            chungTu.INamLamViec = _sessionInfo.YearOfWork;
            chungTu.INamNganSach = _sessionInfo.YearOfBudget;
            chungTu.IIdMaNguonNganSach = _sessionInfo.Budget;
            chungTu.SNguoiTao = _sessionInfo.Principal;
            chungTu.DNgayTao = DateTime.Now;
            chungTu.BDaTongHop = false;
            chungTu.FTongTuChiDeNghi = chungTuChiTiets.Where(x => x.FTuChiDeNghi != 0).Select(x => x.FTuChiDeNghi).Sum();
            chungTu.FTongTuChiPheDuyet = chungTu.FTongTuChiDeNghi;
            _chungTuService.Add(chungTu);

            foreach (var item in chungTuChiTiets)
            {
                NsMucLucNganSach mlns = _mucLucNganSachs.Where(x => x.XauNoiMa == item.SXauNoiMa).FirstOrDefault();
                item.FTuChiPheDuyet = item.FTuChiDeNghi;
                item.IIdMlns = mlns == null ? Guid.Empty : mlns.MlnsId;
                item.IIdMlnsCha = mlns == null ? Guid.Empty : mlns.MlnsIdParent;
                item.IIdQtchungTu = chungTu.Id;
                item.BHangCha = false;
                item.INamLamViec = _sessionInfo.YearOfWork;
                item.INamNganSach = _sessionInfo.YearOfBudget;
                item.IIdMaNguonNganSach = _sessionInfo.Budget;
                item.IThangQuyLoai = QuarterMonthSelected.DisplayItem.Contains("Quý") ? 1 : 0;
                item.IThangQuy = int.Parse(QuarterMonthSelected.ValueItem);
                item.FTuChiPheDuyet = item.FTuChiPheDuyet;
                item.IIdMaDonVi = _agencySelected.ValueItem;
                item.SNguoiTao = _sessionInfo.Principal;
                item.DNgayTao = DateTime.Now;
            }
            _chungTuChiTietService.AddRange(chungTuChiTiets);

            foreach (var item in quyetToans)
            {
                item.ImportId = _impHistory.Id;
                item.Loai = SettlementImportType;
            }
            _impQuyetToanService.AddRange(quyetToans);
            SettlementVoucherModel settlementVoucher = _mapper.Map<SettlementVoucherModel>(chungTu);

            MessageBoxHelper.Info(Resources.MsgSaveDone);

            SavedAction?.Invoke(settlementVoucher);
        }

        private void ShowError()
        {
            int rowIndex = _settlementVoucherDetails.IndexOf(SelectedItem);
            List<string> errors = _errors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            MessageBoxHelper.Warning(message, "Thông tin lỗi");
        }

        private void OnAddMLNS()
        {
            TabIndex = ImportTabIndex.MLNS;
            NsMuclucNgansachModel importItem = new NsMuclucNgansachModel();
            if (ImportedMlns.Contains(importItem))
                return;

            bool isImportGroup = false;
            if (!_importedMlns.Any(x => x.XauNoiMa.Contains(SelectedItem.ConcatenateCode))
                || !_existedMlns.Any(x => x.XauNoiMa.Contains(SelectedItem.ConcatenateCode)))
            {
                List<SettlementVoucherDetailImportModel> data = new List<SettlementVoucherDetailImportModel>();
                data.Add(SelectedItem);
                if (SelectedItem.BHangCha)
                    data.AddRange(_settlementVoucherDetailProcess.Where(x => SelectedItem.ListConcatenateCodeChild.Contains(x.ConcatenateCode)).ToList());
                else
                    data.Add(_settlementVoucherDetailProcess.Where(x => x.ConcatenateCode == SelectedItem.ConcatenateCodeParent).FirstOrDefault());
                if (data.Count > 1)
                    isImportGroup = true;
                data = data.Where(x => x != null).OrderBy(x => x.ConcatenateCode).ToList();
                foreach (var item in data)
                {
                    if (!_mucLucNganSachs.Any(x => x.XauNoiMa == item.ConcatenateCode))
                        _importedMlns.Add(new NsMuclucNgansachModel
                        {
                            MlnsId = Guid.NewGuid(),
                            Lns = item.LNS,
                            L = item.L,
                            K = item.K,
                            M = item.M,
                            TM = item.TM,
                            TTM = item.TTM,
                            NG = item.NG,
                            TNG = item.TNG,
                            XauNoiMa = item.ConcatenateCode,
                            MoTa = item.Description,
                            NamLamViec = _sessionInfo.YearOfWork,
                            IsModified = true,
                            BHangCha = item.BHangCha
                        });
                }
            }
            foreach (NsMuclucNgansachModel model in _importedMlns.ToList())
            {
                NsMuclucNgansachModel parent = null;
                if (isImportGroup && !model.BHangCha)
                    parent = _importedMlns.Where(x => model.XauNoiMa.Contains(x.XauNoiMa) && model.XauNoiMa != x.XauNoiMa).FirstOrDefault();
                if (parent == null)
                    parent = FindParent(model, _existedMlns);
                if (parent != null)
                {
                    int index = _existedMlns.IndexOf(parent);
                    _existedMlns.Insert(index + 1, model);
                    //_importedMlns.Remove(model);
                    model.MlnsIdParent = parent.MlnsId;
                    model.BHangCha = model.BHangCha;
                    model.ITrangThai = 1;
                    model.SNguoiTao = _sessionInfo.Principal;
                    model.DNgayTao = DateTime.Now;
                    _mergeItems.Add(model);
                    OnPropertyChanged(nameof(IsEnableSaveMLNS));
                }
            }
            _importedMlns = new ObservableCollection<NsMuclucNgansachModel>();
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnSelectionChanged();
        }

        public NsMuclucNgansachModel FindParent(NsMuclucNgansachModel model, IEnumerable<NsMuclucNgansachModel> ExistedMlns)
        {
            IEnumerable<NsMuclucNgansachModel> ancestors = _existedMlns.Where(i => !Guid.Empty.Equals(i.Id) && !model.XauNoiMa.Equals(i.XauNoiMa) &&
                model.XauNoiMa.StartsWith(i.XauNoiMa + "-") && model.NamLamViec == i.NamLamViec)
                .OrderByDescending(i => i.XauNoiMa.Length);
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
                    }
                };
            }
        }

        private void OnMerge()
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
                item.SNguoiTao = _sessionInfo.Principal;
                item.DNgayTao = DateTime.Now;
            }

            List<NsMuclucNgansachModel> nsMuclucNgansachModels = _existedMlns.ToList();
            nsMuclucNgansachModels.InsertRange(index + 1, _mergeItems);
            _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(nsMuclucNgansachModels);
            _importedMlns = new ObservableCollection<NsMuclucNgansachModel>(ImportedMlns.Where(i => !i.IsSelected || !i.IsModified));
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnPropertyChanged(nameof(IsEnableSaveMLNS));
            OnSelectionChanged();
        }

        private void OnUnMerge()
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
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnPropertyChanged(nameof(IsEnableSaveMLNS));
            OnSelectionChanged();
        }

        private void OnSaveMLNS()
        {
            var result = MessageBoxHelper.Confirm(Resources.ConfirmAddMLNS);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    List<NsMucLucNganSach> listMLNS = _mapper.Map<List<NsMucLucNganSach>>(_mergeItems);
                    _mucLucNganSachService.AddRange(listMLNS);
                    _existedMlns.Where(x => x.IsModified).Select(x => { x.IsModified = false; x.IsSelected = false; return x; }).ToList();
                    _mergeItems = new List<NsMuclucNgansachModel>();
                    OnPropertyChanged(nameof(ExistedMlns));
                    OnPropertyChanged(nameof(IsEnableSaveMLNS));
                    MessageBoxHelper.Info(Resources.MsgSaveDone);

                    foreach (var item in listMLNS)
                    {
                        var importItem = _settlementVoucherDetails.Where(x => x.ConcatenateCode == item.XauNoiMa).FirstOrDefault();
                        var listError = _importService.ValidateItem<SettlementVoucherDetailImportModel>(importItem, _settlementVoucherDetails.IndexOf(importItem));
                        if (listError.Count == 0)
                        {
                            importItem.ImportStatus = true;
                            importItem.IsErrorMLNS = false;
                            TabIndex = ImportTabIndex.Data;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    }
                }
                catch
                {
                    MessageBoxHelper.Warning(Resources.MsgSaveError);
                }
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        private void CalculateData()
        {
            // Reset value parrent
            foreach (var item in _settlementVoucherDetailProcess.Where(x => x.BHangCha))
            {
                if (!string.IsNullOrEmpty(item.Suggestion))
                    item.Suggestion = "0";
                if (!string.IsNullOrEmpty(item.Day))
                    item.Day = "0";
                if (!string.IsNullOrEmpty(item.People))
                    item.People = "0";
                if (!string.IsNullOrEmpty(item.Bout))
                    item.Bout = "0";
                if (!string.IsNullOrEmpty(item.FDeNghiChuyenNamSau))
                    item.FDeNghiChuyenNamSau = "0";
                if (!string.IsNullOrEmpty(item.FChuyenNamSauDaCap))
                    item.FChuyenNamSauDaCap = "0";
                if (!string.IsNullOrEmpty(item.FChuyenNamSauChuaCap))
                    item.FChuyenNamSauChuaCap = "0";
            }
            foreach (var item in _settlementVoucherDetailProcess.Where(x => !x.BHangCha))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(SettlementVoucherDetailImportModel currentItem, SettlementVoucherDetailImportModel selfItem)
        {
            List<SettlementVoucherDetailImportModel> parents = new List<SettlementVoucherDetailImportModel>();
            if (!currentItem.IsWarning)
                parents = _settlementVoucherDetailProcess.Where(x => currentItem.ConcatenateCode.Contains(x.ConcatenateCode)
                           && x.BHangCha && currentItem.ConcatenateCode != x.ConcatenateCode).OrderByDescending(x => x.LNS).ToList();
            else parents = _settlementVoucherDetailProcess.Where(x => currentItem.ConcatenateCode.Contains(x.ConcatenateCode) && currentItem.ConcatenateCode != x.ConcatenateCode).OrderByDescending(x => x.LNS).ToList();
            if (parents.Count > 0)
            {
                int columnIndexOrigin = 0;
                SettlementVoucherDetailImportModel parent = new SettlementVoucherDetailImportModel();
                foreach (var p in parents)
                {
                    int maxColumn = p.ConcatenateCode.Split("-").Count();
                    if (maxColumn > columnIndexOrigin)
                    {
                        columnIndexOrigin = maxColumn;
                        parent = p;
                    }
                }
                double parentValue, childValue = 0;
                if (Double.TryParse(parent.Suggestion, out parentValue) && Double.TryParse(selfItem.Suggestion, out childValue))
                    parent.Suggestion = StringUtils.DinhDangSo((parentValue + childValue).ToString(), 0, false);
                if (Double.TryParse(parent.Day, out parentValue) && Double.TryParse(selfItem.Day, out childValue))
                    parent.Day = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.People, out parentValue) && Double.TryParse(selfItem.People, out childValue))
                    parent.People = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.Bout, out parentValue) && Double.TryParse(selfItem.Bout, out childValue))
                    parent.Bout = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.FDeNghiChuyenNamSau, out parentValue) && Double.TryParse(selfItem.FDeNghiChuyenNamSau, out childValue))
                    parent.FDeNghiChuyenNamSau = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.FChuyenNamSauDaCap, out parentValue) && Double.TryParse(selfItem.FChuyenNamSauDaCap, out childValue))
                    parent.FChuyenNamSauDaCap = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.FChuyenNamSauChuaCap, out parentValue) && Double.TryParse(selfItem.FChuyenNamSauChuaCap, out childValue))
                    parent.FChuyenNamSauChuaCap = (parentValue + childValue).ToString();
                CalculateParent(parent, selfItem);
            }
            else return;
        }

        private void SuggestSelectMlns()
        {
            if (SelectedItem == null)
                return;
            int rowIndex = _settlementVoucherDetails.IndexOf(SelectedItem);
            SuggestMlns suggestMlns = new SuggestMlns
            {
                DataContext = _suggestMlnsViewmodel
            };
            _suggestMlnsViewmodel.SaveAction = obj =>
            {
                SelectedItem.PropertyChanged -= settlementVoucherDetailPropertyChanged;
                SelectedItem.LNS = obj.Lns;
                SelectedItem.L = obj.L;
                SelectedItem.K = obj.K;
                SelectedItem.M = obj.M;
                SelectedItem.TM = obj.TM;
                SelectedItem.TTM = obj.TTM;
                SelectedItem.NG = obj.NG;
                SelectedItem.TNG = obj.TNG;
                SelectedItem.TNG1 = obj.TNG1;
                SelectedItem.TNG2 = obj.TNG2;
                SelectedItem.TNG3 = obj.TNG3;
                SelectedItem.Description = obj.MoTa;
                SelectedItem.ImportStatus = true;
                SelectedItem.IsErrorMLNS = false;
                _errors.RemoveAll(x => x.Row == rowIndex);
                OnPropertyChanged(nameof(IsSaveData));
                SelectedItem.PropertyChanged += settlementVoucherDetailPropertyChanged;
                suggestMlns.Close();
            };
            _suggestMlnsViewmodel.excludeMlns = _mapper.Map<IEnumerable<NsMuclucNgansachModel>>(_settlementVoucherDetails);
            _suggestMlnsViewmodel.InvalidMlns = _mapper.Map<NsMucLucNganSach>(SelectedItem);
            _suggestMlnsViewmodel.FilterModel = new NsMuclucNgansachModel();
            _suggestMlnsViewmodel.Init();
            suggestMlns.ShowDialog();
        }

        private async Task OnGetFileFtpCommand(bool isSendHTTP)
        {
            IsSendHTTP = isSendHTTP;
            IsLoading = true;
            var fileFilter = new FileFilterModel();
            fileFilter.AgencyCode = null;
            fileFilter.Module = "BUDGET";
            switch (SettlementImportType)
            {
                case SettlementType.REGULAR_BUDGET:
                    fileFilter.SubModule = NSFunctionCode.BUDGET_SETTLEMENT_REGULAR_BUDGET;
                    break;
                case SettlementType.DEFENSE_BUDGET:
                    fileFilter.SubModule = NSFunctionCode.BUDGET_SETTLEMENT_DEFENSE_BUDGET;
                    break;
                case SettlementType.STATE_BUDGET:
                    fileFilter.SubModule = NSFunctionCode.BUDGET_SETTLEMENT_STATE_BUDGET;
                    break;
                case SettlementType.FOREX_BUDGET:
                    fileFilter.SubModule = NSFunctionCode.BUDGET_SETTLEMENT_FOREX_BUDGET;
                    break;
                case SettlementType.EXPENSE_BUDGET:
                    fileFilter.SubModule = NSFunctionCode.BUDGET_SETTLEMENT_EXPENSE_BUDGET;
                    break;                
            }
            if(QuarterMonthSelected != null)
            {
                fileFilter.Quarter = QuarterMonthSelected.DisplayItem.Contains("Quý") ? QuarterMonthSelected.DisplayItem.Substring(4) : QuarterMonthSelected.DisplayItem.Substring(6);
            }
            else
            {
                fileFilter.Quarter = "";
            }
            fileFilter.YearOfWork = _sessionInfo.YearOfWork;
            fileFilter.YearOfBudget = _sessionInfo.YearOfBudget;
            fileFilter.SourceOfBudget = _sessionInfo.Budget;
            var lst = await _hTTPUploadFileService.GetFile(isSendHTTP, fileFilter);
            if (lst == null || lst.Count == 0)
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Không tìm thấy dữ liệu");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                LstFile = new ObservableCollection<FileFilterQuery>();
                IsLoading = false;
                return;
            }
            LstFile = new ObservableCollection<FileFilterQuery>(lst);
            IsLoading = false;
        }

        private async Task OnDownloadFileFtpServer(object obj)
        {
            IsLoading = true;
            if (obj is FileFilterQuery file)
            {
                var id = file.FileId;
                var fileStream = await _hTTPUploadFileService.DownloadDecryptFile(IsSendHTTP, id, file.FileTokenKey);
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    VoucherDetailResult = _importService.ProcessDataUnique<SettlementVoucherDetailImportModel>(fileStream, file.FileTokenKey);
                    OnProcessFile(true);
                }, (s, e) =>
                {
                    IsLoading = false;
                });
            }
        }
        //private void OnDownloadFileFtpServer()
        //{
        //    string urlUrIDownLoad = "";
        //    string fileName = "";
        //    if (LstFile == null || LstFile.Count == 0 || !LstFile.Any(e => e.BIsCheck))
        //    {
        //        StringBuilder messageBuilder = new StringBuilder();
        //        messageBuilder.AppendFormat("Vui lòng lấy dữ liệu");
        //        System.Windows.MessageBox.Show(messageBuilder.ToString());
        //        return;
        //    }
        //    else if (LstFile.Where(n => n.BIsCheck).Count() > 1)
        //    {
        //        System.Windows.MessageBox.Show("Chọn 1 file dữ liệu");
        //        return;
        //    }

        //    var item = LstFile.FirstOrDefault(x => x.BIsCheck);

        //    urlUrIDownLoad = item.SUrl;
        //    fileName = item.SNameFile;
        //    FilePath = _ftpStorageService.DowLoadFileFtpGiveLocalStr(urlUrIDownLoad, fileName);

        //    OnProcessFile(true);
        //}
    }
}
