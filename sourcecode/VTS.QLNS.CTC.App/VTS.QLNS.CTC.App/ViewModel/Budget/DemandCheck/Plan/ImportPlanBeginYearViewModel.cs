using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan
{
    public class ImportPlanBeginYearViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IConfiguration _configuration;
        private readonly IImpHistoryService _impHistoryService;
        private readonly IImpSktSoLieuService _impSktSoLieuService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISktSoLieuChungTuService _sktChungTuService;
        private readonly ISktMucLucService _sktMucLucService;
        private readonly ICauHinhCanCuService _iCauHinhCanCuService;
        private readonly ICryptographyService _iCryptographyService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISoLieuChiTietPhanCapService _soLieuChiTietPhanCapService;
        private readonly ISktSoLieuChiTietCanCuDataService _sktSoLieuChiTietCanCuDataService;
        private readonly IHTTPUploadFileService _hTTPUploadFileService;
        private SessionInfo _sessionInfo;
        private List<NsMucLucNganSach> _mucLucNganSachs;
        private Dictionary<string, NsMucLucNganSach> _dicMucLuc;
        private ImpHistory _impHistory;
        private List<ImportErrorItem> _errors;
        private List<ImportErrorItem> _phancapErrors;
        private List<NsMuclucNgansachModel> _mergeItems;
        private string _importFolder;
        private string _filePath;
        private List<PlanBeginYearImportModel> _importDataDetailProcess;
        private List<NsDuToanDauNamNsDacThuImportModel> _importDataDacThuProcess;
        private List<NsDtDauNamPhanCapImportModel> _importDataPhanCapProcess;
        private readonly FtpStorageService _ftpStorageService;
        public bool IsSendHTTP;

        public override string Name => "Dự toán đầu năm";
        public override Type ContentType => typeof(View.Budget.DemandCheck.Plan.ImportPlanBeginYear);
        public override string Description => "Import dự toán đầu năm";
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public int VoucherNoIndex;
        public bool IsEnableSaveMLNS => _mergeItems.Count > 0;

        private Dictionary<int, NsCauHinhCanCu> _dicCauHinhCanCu;
        private Dictionary<string, NsCauHinhCanCu> _dicCauHinhCanCuDamBao;
        private Dictionary<string, DonVi> _dicDonVi;

        private ImportResult<PlanBeginYearImportModel> _voucherDetailResult;
        public ImportResult<PlanBeginYearImportModel> VoucherDetailResult
        {
            get => _voucherDetailResult;
            set
            {
                SetProperty(ref _voucherDetailResult, value);
                OnPropertyChanged(nameof(_voucherDetailResult));
            }
        }

        private ImportResult<NsDuToanDauNamNsDacThuImportModel> _voucherDetailDacThuResult;
        public ImportResult<NsDuToanDauNamNsDacThuImportModel> VoucherDetailDacThuResult
        {
            get => _voucherDetailDacThuResult;
            set
            {
                SetProperty(ref _voucherDetailDacThuResult, value);
                OnPropertyChanged(nameof(_voucherDetailDacThuResult));
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
            set => SetProperty(ref _budgetSourceTypeSelected, value);
        }

        private ImportResult<NsDtDauNamPhanCapImportModel> _voucherDetailPhanCapResult;
        public ImportResult<NsDtDauNamPhanCapImportModel> VoucherDetailPhanCapResult
        {
            get => _voucherDetailPhanCapResult;
            set
            {
                SetProperty(ref _voucherDetailPhanCapResult, value);
                OnPropertyChanged(nameof(_voucherDetailPhanCapResult));
            }
        }

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

        private ObservableCollection<ComboboxItem> _dataChungTu;
        public ObservableCollection<ComboboxItem> DataChungTu
        {
            get => _dataChungTu;
            set => SetProperty(ref _dataChungTu, value);
        }

        private ComboboxItem _selectedChungTu;
        public ComboboxItem SelectedChungTu
        {
            get => _selectedChungTu;
            set
            {
                SetProperty(ref _selectedChungTu, value);
                OnResetData(false);
                LoadComboboxDonVi();
                OnPropertyChanged(nameof(VisibleNganSachSuDung));
                OnPropertyChanged(nameof(VisibleNganSachDamBao));
            }
        }
        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private ObservableCollection<ComboboxItem> _dataDonVi;
        public ObservableCollection<ComboboxItem> DataDonVi
        {
            get => _dataDonVi;
            set => SetProperty(ref _dataDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private string _loaiChungTu;
        public string LoaiChungTu
        {
            get => _loaiChungTu;
            set => SetProperty(ref _loaiChungTu, value);
        }

        private ObservableCollection<PlanBeginYearImportModel> _dataBeginYearImport;
        public ObservableCollection<PlanBeginYearImportModel> DataBeginYearImport
        {
            get => _dataBeginYearImport;
            set => SetProperty(ref _dataBeginYearImport, value);
        }

        private PlanBeginYearImportModel _selectedItem;
        public PlanBeginYearImportModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (SelectedChungTu != null && SelectedChungTu.ValueItem == "1" && DataBeginYearImport != null && DataBeginYearImport.Count > 0)
                    return !DataBeginYearImport.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                if (SelectedChungTu != null && SelectedChungTu.ValueItem == "2" && CanCuItems != null && CanCuItems.Count > 0)
                    return !CanCuItems.Where(x => !x.IsWarning).Any(x => !x.ImportStatus) && !PhanCapItems.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }

        private ObservableCollection<NsMuclucNgansachModel> _existedMlns;
        public ObservableCollection<NsMuclucNgansachModel> ExistedMlns
        {
            get => _existedMlns;
            set => SetProperty(ref _existedMlns, value);
        }

        public bool IsEnabledMergeBtn
        {
            get => ImportedMlns != null && ImportedMlns.Where(i => i.IsSelected).Count() > 0 && ExistedMlns.Where(i => i.IsSelected).Count() == 1;
        }

        public bool IsEnabledUnmergeCommand
        {
            get => ExistedMlns != null && ExistedMlns.Where(i => i.IsModified && i.IsSelected).Count() > 0;
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

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }

        private string _soChungTu;
        public string SoChungTu
        {
            get => _soChungTu;
            set => SetProperty(ref _soChungTu, value);
        }

        private DateTime? _ngayChungTu;
        public DateTime? NgayChungTu
        {
            get => _ngayChungTu;
            set => SetProperty(ref _ngayChungTu, value);
        }
        private ObservableCollection<FileFilterQuery> _lstFile;
        public ObservableCollection<FileFilterQuery> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }
        public string SHeader1 { get; set; }
        public string SHeader2 { get; set; }
        public string SHeader3 { get; set; }
        public string SHeader4 { get; set; }
        public string SHeader5 { get; set; }

        public Visibility VisibleNganSachSuDung => LoaiChungTu == VoucherType.NSSD_Key ? Visibility.Visible : Visibility.Collapsed;
        public Visibility VisibleNganSachDamBao => LoaiChungTu == VoucherType.NSBD_Key ? Visibility.Visible : Visibility.Collapsed;

        #region Dac thu nganh
        private ObservableCollection<NsDuToanDauNamNsDacThuImportModel> _cancuItems;
        public ObservableCollection<NsDuToanDauNamNsDacThuImportModel> CanCuItems
        {
            get => _cancuItems;
            set => SetProperty(ref _cancuItems, value);
        }

        private NsDuToanDauNamNsDacThuImportModel _cancuSelected;
        public NsDuToanDauNamNsDacThuImportModel CanCuSelected
        {
            get => _cancuSelected;
            set => SetProperty(ref _cancuSelected, value);
        }

        private ObservableCollection<NsDtDauNamPhanCapImportModel> _phancapItems;
        public ObservableCollection<NsDtDauNamPhanCapImportModel> PhanCapItems
        {
            get => _phancapItems;
            set => SetProperty(ref _phancapItems, value);
        }

        private NsDtDauNamPhanCapImportModel _phancapSelected;
        public NsDtDauNamPhanCapImportModel PhanCapSelected
        {
            get => _phancapSelected;
            set => SetProperty(ref _phancapSelected, value);
        }

        private ObservableCollection<ComboboxItem> _donviNewItems;
        public ObservableCollection<ComboboxItem> DonViNewItems
        {
            get => _donviNewItems;
            set => SetProperty(ref _donviNewItems, value);
        }
        #endregion

        #region Header
        private string _sTuChi1;
        public string STuChi1
        {
            get => _sTuChi1;
            set => SetProperty(ref _sTuChi1, value);
        }

        private string _sTuChi2;
        public string STuChi2
        {
            get => _sTuChi2;
            set => SetProperty(ref _sTuChi2, value);
        }

        private string _sTuChi3;
        public string STuChi3
        {
            get => _sTuChi3;
            set => SetProperty(ref _sTuChi3, value);
        }

        private string _sTuChi4;
        public string STuChi4
        {
            get => _sTuChi4;
            set => SetProperty(ref _sTuChi4, value);
        }

        private string _sTuChi5;
        public string STuChi5
        {
            get => _sTuChi5;
            set => SetProperty(ref _sTuChi5, value);
        }

        #region NS dac thu
        private string _sHangNhap1;
        public string SHangNhap1
        {
            get => _sHangNhap1;
            set => SetProperty(ref _sHangNhap1, value);
        }
        private string _sPhanCap1;
        public string SPhanCap1
        {
            get => _sPhanCap1;
            set => SetProperty(ref _sPhanCap1, value);
        }
        private string _sHangNhap2;
        public string SHangNhap2
        {
            get => _sHangNhap2;
            set => SetProperty(ref _sHangNhap2, value);
        }
        private string _sPhanCap2;
        public string SPhanCap2
        {
            get => _sPhanCap2;
            set => SetProperty(ref _sPhanCap2, value);
        }
        private string _sHangNhap3;
        public string SHangNhap3
        {
            get => _sHangNhap3;
            set => SetProperty(ref _sHangNhap3, value);
        }
        private string _sPhanCap3;
        public string SPhanCap3
        {
            get => _sPhanCap3;
            set => SetProperty(ref _sPhanCap3, value);
        }
        private string _sHangNhap4;
        public string SHangNhap4
        {
            get => _sHangNhap4;
            set => SetProperty(ref _sHangNhap4, value);
        }
        private string _sPhanCap4;
        public string SPhanCap4
        {
            get => _sPhanCap4;
            set => SetProperty(ref _sPhanCap4, value);
        }
        private string _sHangNhap5;
        public string SHangNhap5
        {
            get => _sHangNhap5;
            set => SetProperty(ref _sHangNhap5, value);
        }
        private string _sPhanCap5;
        public string SPhanCap5
        {
            get => _sPhanCap5;
            set => SetProperty(ref _sPhanCap5, value);
        }
        #endregion
        #endregion

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand ShowDacThuErrorCommand { get; }
        public RelayCommand ShowPhanCapErrorCommand { get; }
        //public RelayCommand AddMLNSCommand { get; }
        //public RelayCommand AddMlnsDacThuCommand { get; }
        //public RelayCommand AddMlnsPhanCapCommand { get; }
        public RelayCommand MergeCommand { get; }
        public RelayCommand UnmergeCommand { get; }
        public RelayCommand SaveMLNSCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand OpenReferencePopupCommand { get; }
        public RelayCommand GetFileFtpCommandHTTP { get; }
        public RelayCommand GetFileFtpCommandFTP { get; }
        public RelayCommand DownloadFileFtpServer { get; }
        public ImportPlanBeginYearViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            IConfiguration configuration,
            IImpHistoryService impHistoryService,
            ICauHinhCanCuService iCauHinhCanCuService,
            ILog logger,
            ISktSoLieuChungTuService sktChungTuService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            ICryptographyService iCryptographyService,
            INsMucLucNganSachService mucLucNganSachService,
            IImpSktSoLieuService impSktSoLieuService,
            ISoLieuChiTietPhanCapService soLieuChiTietPhanCapService,
            ISktSoLieuChiTietCanCuDataService sktSoLieuChiTietCanCuDataService,
            ISktSoLieuService sktSoLieuService,
            ISktMucLucService sktMucLucService,
            FtpStorageService ftpStorageService,
            IServiceProvider serviceProvider,
            IHTTPUploadFileService hTTPUploadFileService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _sktSoLieuService = sktSoLieuService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _mucLucNganSachService = mucLucNganSachService;
            _configuration = configuration;
            _impHistoryService = impHistoryService;
            _impSktSoLieuService = impSktSoLieuService;
            _iCryptographyService = iCryptographyService;
            _logger = logger;
            _sktChungTuService = sktChungTuService;
            _sktMucLucService = sktMucLucService;
            _serviceProvider = serviceProvider;
            _ftpStorageService = ftpStorageService;
            _iCauHinhCanCuService = iCauHinhCanCuService;
            _soLieuChiTietPhanCapService = soLieuChiTietPhanCapService;
            _sktSoLieuChiTietCanCuDataService = sktSoLieuChiTietCanCuDataService;
            _hTTPUploadFileService = hTTPUploadFileService;


            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            ShowDacThuErrorCommand = new RelayCommand(obj => ShowDacThuError());
            ShowPhanCapErrorCommand = new RelayCommand(obj => ShowPhanCapError());
            //AddMLNSCommand = new RelayCommand(obj => OnAddMLNS());
            //AddMlnsDacThuCommand = new RelayCommand(obj => OnAddMLNSCanCu());
            //AddMlnsPhanCapCommand = new RelayCommand(obj => OnAddMLNSCanCu());
            MergeCommand = new RelayCommand(obj => OnMerge());
            UnmergeCommand = new RelayCommand(obj => OnUnMerge());
            SaveMLNSCommand = new RelayCommand(obj => OnSaveMLNS());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            OpenReferencePopupCommand = new RelayCommand(obj => OnOpenReferencePopup(obj));
            GetFileFtpCommandHTTP = new RelayCommand(async obj => await OnGetFileFtpCommand(true));
            GetFileFtpCommandFTP = new RelayCommand(async obj => await OnGetFileFtpCommand(false));
            DownloadFileFtpServer = new RelayCommand(async obj => await OnDownloadFileFtpServer(obj));
        }

        private void OnUploadFile()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = string.Format("Chọn file excel");
                openFileDialog.RestoreDirectory = true;
                openFileDialog.DefaultExt = ".xlsx";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                FilePath = openFileDialog.FileName;
                _fileName = openFileDialog.SafeFileName;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadBudgetSourceTypes()
        {
            BudgetSourceTypes = new ObservableCollection<ComboboxItem> {
                new ComboboxItem() { DisplayItem = "Ngân sách dự toán", ValueItem = TypeLoaiNNS.DU_TOAN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách bệnh viện tự chủ", ValueItem = TypeLoaiNNS.BENH_VIEN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách doanh nghiệp", ValueItem = TypeLoaiNNS.DOANH_NGHIEP.ToString() }
            };

            BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(0);
        }

        private void OnUploadFileAndDecrypt()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = string.Format("Chọn file mã hóa");
                openFileDialog.RestoreDirectory = true;
                openFileDialog.DefaultExt = ".qlns";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                if (!openFileDialog.FileName.Contains(FileExtensionFormats.Security))
                {
                    var message = "Cần nhập file dữ liệu đã mã hóa";
                    MessageBoxHelper.Warning(message);
                    return;
                }
                //var pathExcel = openFileDialog.FileName.Replace(FileExtensionFormats.Security, FileExtensionFormats.Xlsx);
                var pathDir = Path.Combine(IOExtensions.ApplicationPath, ConstantUrlPathPhanHe.UrlFolderTemp);
                IOExtensions.CreateDirectoryIfNotExists(pathDir);
                var pathExcel = Path.Combine(pathDir, "temp.xlsx");
                _iCryptographyService.DecryptFile(openFileDialog.FileName, pathExcel);
                FilePath = pathExcel;
                FilePathSecurity = openFileDialog.FileName;
                _fileName = openFileDialog.SafeFileName;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnResetData(bool resetCombobox = true)
        {
            _fileName = string.Empty;
            if (resetCombobox)
            {
                if (DataChungTu != null && DataChungTu.Count > 0)
                {
                    _selectedChungTu = DataChungTu.FirstOrDefault();
                    LoadComboboxDonVi();
                    OnPropertyChanged(nameof(SelectedChungTu));
                }

                if (DataDonVi != null && DataDonVi.Count > 0)
                {
                    _selectedDonVi = DataDonVi.FirstOrDefault();
                    OnPropertyChanged(nameof(SelectedDonVi));
                }
            }
            LstFile = new ObservableCollection<FileFilterQuery>();

            _mergeItems = new List<NsMuclucNgansachModel>();
            _filePath = string.Empty;
            FilePathSecurity = string.Empty;
            _tabIndex = ImportTabIndex.Data;
            _impHistory = new ImpHistory();
            _dataBeginYearImport = new ObservableCollection<PlanBeginYearImportModel>();
            _cancuItems = new ObservableCollection<NsDuToanDauNamNsDacThuImportModel>();
            _phancapItems = new ObservableCollection<NsDtDauNamPhanCapImportModel>();
            _mucLucNganSachs = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
            _importedMlns = new ObservableCollection<NsMuclucNgansachModel>();
            _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(_mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(_mucLucNganSachs));
            _errors = new List<ImportErrorItem>();
            _phancapErrors = new List<ImportErrorItem>();
            _importFolder = _configuration.GetSection("ImportFolder").Value;
            if (!Directory.Exists(_importFolder))
                Directory.CreateDirectory(_importFolder);
            int soChungTuIndex = _sktChungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork,
              _sessionService.Current.Budget, _sessionService.Current.YearOfBudget, 0);
            SoChungTu = "DTDN-" + soChungTuIndex.ToString("D3");
            NgayChungTu = DateTime.Now;
            OnPropertyChanged(nameof(SoChungTu));
            OnPropertyChanged(nameof(NgayChungTu));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedChungTu));
            OnPropertyChanged(nameof(DataBeginYearImport));
            OnPropertyChanged(nameof(CanCuItems));
            OnPropertyChanged(nameof(PhanCapItems));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(LstFile));
            OnPropertyChanged(nameof(VisibleNganSachSuDung));
            OnPropertyChanged(nameof(VisibleNganSachDamBao));

        }

        public override void Init()
        {
            try
            {
                base.Init();
                OnResetData();
                _sessionInfo = _sessionService.Current;
                GetDonVi();
                LoadComboboxChungTu();
                LoadComboboxDonVi();
                GetCauHinhCanCu();
                LoadBudgetSourceTypes();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadComboboxChungTu()
        {
            DataChungTu = new ObservableCollection<ComboboxItem>();
            DataChungTu.Add(new ComboboxItem { ValueItem = VoucherType.NSSD_Key, DisplayItem = VoucherType.NSSD_Value });
            DataChungTu.Add(new ComboboxItem { ValueItem = VoucherType.NSBD_Key, DisplayItem = VoucherType.NSBD_Value });
            SelectedChungTu = DataChungTu.FirstOrDefault();
        }

        private List<string> GetListIdDonViHasCt()
        {
            if (SelectedChungTu == null)
            {
                return new List<string>();
            }
            var predicate = PredicateBuilder.True<NsDtdauNamChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(SelectedChungTu.ValueItem));
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);

            List<NsDtdauNamChungTu> chungTu = _sktChungTuService.FindByCondition(predicate).ToList();
            if (chungTu != null && chungTu.Count > 0)
            {
                return chungTu.Select(n => n.IIdMaDonVi).ToList();
            }
            return new List<string>();
        }

        private List<NguoiDungDonVi> GetListNguoiDungDonVi()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.IIDMaNguoiDung.Equals(_sessionService.Current.Principal));
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            List<NguoiDungDonVi> nsDungDonVis = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return nsDungDonVis;
        }

        private List<DonVi> GetListDonVi()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();

            List<DonVi> listUnit = _donViService.FindByNamLamViec(yearOfWork).ToList();
            if (SelectedChungTu != null && SelectedChungTu.ValueItem == VoucherType.NSBD_Key) listUnit = listUnit.Where(x => x.BCoNSNganh).ToList();

            List<NguoiDungDonVi> listNguoiDungDonVi = GetListNguoiDungDonVi();
            if (listNguoiDungDonVi != null && listNguoiDungDonVi.Count > 0)
            {
                listUnit = listUnit.Where(n => listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(n.IIDMaDonVi)).ToList();
            }
            else
            {
                listUnit = new List<DonVi>();
            }
            // var listDonViByUser = _donViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.NOIBO).Select(x => x.IIDMaDonVi);
            // listUnit = listUnit.Where(x => listDonViByUser.Contains(x.IIDMaDonVi)).ToList();
            return listUnit;
        }

        private void LoadComboboxDonVi()
        {
            LoaiChungTu = string.Empty;
            if (SelectedChungTu != null && SelectedChungTu.ValueItem == VoucherType.NSSD_Key)
            {
                LoaiChungTu = VoucherType.NSSD_Key;
            }
            else if (SelectedChungTu != null && SelectedChungTu.ValueItem == VoucherType.NSBD_Key)
            {
                LoaiChungTu = VoucherType.NSBD_Key;
            }
            DataDonVi = new ObservableCollection<ComboboxItem>();
            List<DonVi> listDonVi = GetListDonVi();
            foreach (var item in listDonVi)
            {
                DataDonVi.Add(new ComboboxItem { ValueItem = item.IIDMaDonVi, DisplayItem = item.MaTenDonVi, HiddenValue = item.TenDonVi, Type = item.Loai });
            }
            if (DataDonVi != null && DataDonVi.Count > 0)
                SelectedDonVi = DataDonVi.FirstOrDefault();
        }

        private void ShowError()
        {
            int rowIndex = _dataBeginYearImport.IndexOf(SelectedItem);
            List<string> errors = _errors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            MessageBoxHelper.Info(message);
        }

        private void ShowDacThuError()
        {
            int rowIndex = _cancuItems.IndexOf(CanCuSelected);
            List<string> errors = _errors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            MessageBoxHelper.Info(message);
        }

        private void ShowPhanCapError()
        {
            int rowIndex = _phancapItems.IndexOf(PhanCapSelected);
            List<string> errors = _phancapErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            MessageBoxHelper.Info(message);
        }

        public int OnAddMLNS()
        {
            int indexResult = -1;
            TabIndex = ImportTabIndex.MLNS;
            NsMuclucNgansachModel importItem = new NsMuclucNgansachModel();
            if (ImportedMlns.Contains(importItem))
                return indexResult;

            bool isImportGroup = false;
            if (!_importedMlns.Any(x => x.XauNoiMa.Contains(SelectedItem.ConcatenateCode))
                && !_existedMlns.Any(x => x.XauNoiMa.Contains(SelectedItem.ConcatenateCode)))
            {
                List<PlanBeginYearImportModel> data = new List<PlanBeginYearImportModel>();
                data.Add(SelectedItem);
                if (SelectedItem.BHangCha)
                {
                    if (_importDataDetailProcess.Any(x => SelectedItem.ListConcatenateCodeChild.Contains(x.ConcatenateCode)))
                        data.AddRange(_importDataDetailProcess.Where(x => SelectedItem.ListConcatenateCodeChild.Contains(x.ConcatenateCode)).ToList());
                }
                else
                {
                    if (_importDataDetailProcess.Any(x => x.ConcatenateCode == SelectedItem.ConcatenateCodeParent))
                        data.Add(_importDataDetailProcess.Where(x => x.ConcatenateCode == SelectedItem.ConcatenateCodeParent).FirstOrDefault());
                }
                if (data.Count > 1)
                    isImportGroup = true;
                data = data.OrderBy(x => x.ConcatenateCode).ToList();
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
                            MoTa = item.MoTa,
                            NamLamViec = _sessionService.Current.YearOfWork,
                            IsModified = true,
                            BHangChaDuToan = item.BHangCha
                        });
                }
            }
            foreach (NsMuclucNgansachModel model in _importedMlns.ToList())
            {
                NsMuclucNgansachModel parent = null;
                if (isImportGroup && model.BHangChaDuToan.HasValue && !model.BHangChaDuToan.Value)
                    parent = _importedMlns.Where(x => model.XauNoiMa.Contains(x.XauNoiMa) && model.XauNoiMa != x.XauNoiMa).FirstOrDefault();
                if (parent == null)
                    parent = FindParent(model, _existedMlns);
                if (parent != null)
                {
                    int index = _existedMlns.IndexOf(parent);
                    _existedMlns.Insert(index + 1, model);
                    //_importedMlns.Remove(model);
                    model.MlnsIdParent = parent.MlnsId;
                    model.BHangChaDuToan = model.BHangChaDuToan;
                    model.ITrangThai = 1;
                    model.SNguoiTao = _sessionService.Current.Principal;
                    model.DNgayTao = DateTime.Now;
                    _mergeItems.Add(model);
                    OnPropertyChanged(nameof(IsEnableSaveMLNS));
                }
            }
            _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(_existedMlns.OrderBy(n => n.XauNoiMa).ToList());
            _importedMlns = new ObservableCollection<NsMuclucNgansachModel>();
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnSelectionChanged();
            if (SelectedItem != null)
            {
                var item = _existedMlns.Where(n => n.XauNoiMa == SelectedItem.XauNoiMa).FirstOrDefault();
                if (item != null)
                    indexResult = _existedMlns.IndexOf(item);
            }
            return indexResult;
        }

        public int OnAddMLNSCanCu()
        {
            int indexResult = -1;
            TabIndex = ImportTabIndex.MLNS;
            NsMuclucNgansachModel importItem = new NsMuclucNgansachModel();
            if (ImportedMlns.Contains(importItem))
                return indexResult;

            bool isImportGroup = false;
            if (!_importedMlns.Any(x => x.XauNoiMa.Contains(CanCuSelected.ConcatenateCode))
                && !_existedMlns.Any(x => x.XauNoiMa.Contains(CanCuSelected.ConcatenateCode)))
            {
                List<NsDuToanDauNamNsDacThuImportModel> data = new List<NsDuToanDauNamNsDacThuImportModel>();
                data.Add(CanCuSelected);
                if (CanCuSelected.BHangCha)
                {
                    if (_importDataDacThuProcess.Any(x => CanCuSelected.ListConcatenateCodeChild.Contains(x.ConcatenateCode)))
                        data.AddRange(_importDataDacThuProcess.Where(x => CanCuSelected.ListConcatenateCodeChild.Contains(x.ConcatenateCode)).ToList());
                }
                else
                {
                    if (_importDataDacThuProcess.Any(x => x.ConcatenateCode == CanCuSelected.ConcatenateCodeParent))
                        data.Add(_importDataDacThuProcess.Where(x => x.ConcatenateCode == CanCuSelected.ConcatenateCodeParent).FirstOrDefault());
                }
                if (data.Count > 1)
                    isImportGroup = true;
                data = data.OrderBy(x => x.ConcatenateCode).ToList();
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
                            NamLamViec = _sessionService.Current.YearOfWork,
                            IsModified = true,
                            BHangChaDuToan = item.BHangCha
                        });
                }
            }
            foreach (NsMuclucNgansachModel model in _importedMlns.ToList())
            {
                NsMuclucNgansachModel parent = null;
                if (isImportGroup && model.BHangChaDuToan.HasValue && !model.BHangChaDuToan.Value)
                    parent = _importedMlns.Where(x => model.XauNoiMa.Contains(x.XauNoiMa) && model.XauNoiMa != x.XauNoiMa).FirstOrDefault();
                if (parent == null)
                    parent = FindParent(model, _existedMlns);
                if (parent != null)
                {
                    int index = _existedMlns.IndexOf(parent);
                    _existedMlns.Insert(index + 1, model);
                    //_importedMlns.Remove(model);
                    model.MlnsIdParent = parent.MlnsId;
                    model.BHangChaDuToan = model.BHangChaDuToan;
                    model.ITrangThai = 1;
                    model.SNguoiTao = _sessionService.Current.Principal;
                    model.DNgayTao = DateTime.Now;
                    _mergeItems.Add(model);
                    OnPropertyChanged(nameof(IsEnableSaveMLNS));
                }
            }
            _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(_existedMlns.OrderBy(n => n.XauNoiMa).ToList());
            _importedMlns = new ObservableCollection<NsMuclucNgansachModel>();
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnSelectionChanged();
            if (CanCuSelected != null)
            {
                var item = _existedMlns.Where(n => n.XauNoiMa == CanCuSelected.ConcatenateCode).FirstOrDefault();
                if (item != null)
                    indexResult = _existedMlns.IndexOf(item);
            }
            return indexResult;
        }

        public int OnAddMLNSPhanCap()
        {
            int indexResult = -1;
            TabIndex = ImportTabIndex.MLNS;
            NsMuclucNgansachModel importItem = new NsMuclucNgansachModel();
            if (ImportedMlns.Contains(importItem))
                return indexResult;

            bool isImportGroup = false;
            if (!_importedMlns.Any(x => x.XauNoiMa.Contains(PhanCapSelected.ConcatenateCode))
                && !_existedMlns.Any(x => x.XauNoiMa.Contains(PhanCapSelected.ConcatenateCode)))
            {
                List<NsDtDauNamPhanCapImportModel> data = new List<NsDtDauNamPhanCapImportModel>();
                data.Add(PhanCapSelected);
                if (PhanCapSelected.BHangCha)
                    data.AddRange(_importDataPhanCapProcess.Where(x => PhanCapSelected.ListConcatenateCodeChild.Contains(x.ConcatenateCode)).ToList());
                else
                    data.Add(_importDataPhanCapProcess.Where(x => x.ConcatenateCode == PhanCapSelected.ConcatenateCodeParent).FirstOrDefault());
                if (data.Count > 1)
                    isImportGroup = true;
                data = data.OrderBy(x => x.ConcatenateCode).ToList();
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
                            NamLamViec = _sessionService.Current.YearOfWork,
                            IsModified = true,
                            BHangChaDuToan = item.BHangCha
                        });
                }
            }
            foreach (NsMuclucNgansachModel model in _importedMlns.ToList())
            {
                NsMuclucNgansachModel parent = null;
                if (isImportGroup && model.BHangChaDuToan.HasValue && !model.BHangChaDuToan.Value)
                    parent = _importedMlns.Where(x => model.XauNoiMa.Contains(x.XauNoiMa) && model.XauNoiMa != x.XauNoiMa).FirstOrDefault();
                if (parent == null)
                    parent = FindParent(model, _existedMlns);
                if (parent != null)
                {
                    int index = _existedMlns.IndexOf(parent);
                    _existedMlns.Insert(index + 1, model);
                    //_importedMlns.Remove(model);
                    model.MlnsIdParent = parent.MlnsId;
                    model.BHangChaDuToan = model.BHangChaDuToan;
                    model.ITrangThai = 1;
                    model.SNguoiTao = _sessionService.Current.Principal;
                    model.DNgayTao = DateTime.Now;
                    _mergeItems.Add(model);
                    OnPropertyChanged(nameof(IsEnableSaveMLNS));
                }
            }
            _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(_existedMlns.OrderBy(n => n.XauNoiMa).ToList());
            _importedMlns = new ObservableCollection<NsMuclucNgansachModel>();
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnSelectionChanged();
            if (PhanCapSelected != null)
            {
                var item = _existedMlns.Where(n => n.XauNoiMa == PhanCapSelected.ConcatenateCode).FirstOrDefault();
                if (item != null)
                    indexResult = _existedMlns.IndexOf(item);
            }
            return indexResult;
        }

        public NsMuclucNgansachModel FindParent(NsMuclucNgansachModel model, IEnumerable<NsMuclucNgansachModel> ExistedMlns)
        {
            IEnumerable<NsMuclucNgansachModel> ancestors = _existedMlns.Where(i => !Guid.Empty.Equals(i.Id) && !model.XauNoiMa.Equals(i.XauNoiMa) &&
                model.XauNoiMa.StartsWith(i.XauNoiMa + "-") && model.NamLamViec == i.NamLamViec)
                .OrderByDescending(i => i.XauNoiMa.Length);
            return ancestors.FirstOrDefault();
        }

        private bool Validate(ref string message)
        {
            if (VoucherDetailResult == null && string.IsNullOrEmpty(FilePath))
            {
                message = Resources.MsgChooseFileImport;
                return false;
            }
            if (_selectedChungTu == null)
            {
                message = Resources.ErrorChungTuEmpty;
                return false;
            }
            if (_selectedDonVi == null)
            {
                message = string.Format(Resources.MsgInputDropdownRequire, "Đơn vị");
                return false;
            }
            return true;
        }

        private void OnProcessFile(bool isLoadFromServer = false)
        {
            if (!isLoadFromServer)
            {
                string message = string.Empty;
                bool checkValidate = Validate(ref message);
                if (!string.IsNullOrEmpty(message) || !checkValidate)
                {
                    MessageBoxHelper.Warning(message);
                    return;
                }
                _errors = new List<ImportErrorItem>();
                _phancapErrors = new List<ImportErrorItem>();
            }
            //save file to import folder
            string destFile = string.Empty;
            ImpHistory impHistory = new ImpHistory();
            impHistory.FileName = _fileName;
            impHistory.ServiceCode = "Dự toán đầu năm";
            impHistory.UserCreator = _sessionService.Current.Principal;
            impHistory.DateCreated = DateTime.Now;
            if (!isLoadFromServer)
            {
                destFile = Path.Combine(_importFolder, string.Format("{0}_{1}", DateTime.Now.ToString("ddMMyyyyhhmmss"), _fileName));
                File.Copy(FilePath, destFile);
                impHistory.FilePath = _importFolder;
            }
            if (_impHistory.Id == Guid.Empty)
                _impHistoryService.Add(_impHistory);

            try
            {
                if (SelectedChungTu.ValueItem == VoucherType.NSSD_Key)
                {
                    ImportResult<PlanBeginYearImportModel> _voucherDetailResult = isLoadFromServer ? VoucherDetailResult : _importService.ProcessDataUnique<PlanBeginYearImportModel>(FilePath);
                    //DataBeginYearImport = new ObservableCollection<PlanBeginYearImportModel>(_voucherDetailResult.Data);
                    _importDataDetailProcess = new List<PlanBeginYearImportModel>();
                    //xác định cha con trong data import
                    foreach (var item in _voucherDetailResult.Data)
                    {
                        if (!item.IsErrorMLNS)
                        {
                            var mlns = _mucLucNganSachs.Where(x => x.XauNoiMa == item.ConcatenateCode).FirstOrDefault();
                            if (mlns != null)
                                item.BHangCha = mlns.BHangCha;
                        }
                        var childs = _voucherDetailResult.Data.Where(x => x.ConcatenateCode.Length != item.ConcatenateCode.Length && x.ConcatenateCode.Contains(item.ConcatenateCode) && x != item).ToList();
                        if (childs.Count > 0)
                            item.BHangCha = true;
                        else
                            item.BHangCha = false;
                    }

                    //kiểm tra loại ngân sách con thỏa mãn điều kiện để import
                    foreach (var item in _voucherDetailResult.Data)
                    {
                        if (!item.ImportStatus && item.IsErrorMLNS)
                        {
                            var parents = _importDataDetailProcess.Where(x => item.ConcatenateCode.Contains(x.ConcatenateCode) && x != item).ToList();
                            if (parents.Count > 0)
                            {
                                int columnIndexOrigin = 0;
                                PlanBeginYearImportModel parent = new PlanBeginYearImportModel();
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
                                    if (!string.IsNullOrEmpty(parent.UocThucHien))
                                        parent.UocThucHien = "0";
                                    if (!string.IsNullOrEmpty(parent.ChiTiet))
                                        parent.ChiTiet = "0";
                                    if (!string.IsNullOrEmpty(parent.HangNhap))
                                        parent.HangNhap = "0";
                                    if (!string.IsNullOrEmpty(parent.HangMua))
                                        parent.HangMua = "0";
                                    if (!string.IsNullOrEmpty(parent.PhanCap))
                                        parent.PhanCap = "0";
                                    if (!string.IsNullOrEmpty(parent.ChuaPhanCap))
                                        parent.ChuaPhanCap = "0";
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
                        _importDataDetailProcess.Add(item);
                    }
                    CalculateData();
                    DataBeginYearImport = new ObservableCollection<PlanBeginYearImportModel>(_importDataDetailProcess);
                    foreach (var item in _dataBeginYearImport)
                    {
                        item.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName != nameof(PlanBeginYearImportModel.ImportStatus)
                                && args.PropertyName != nameof(PlanBeginYearImportModel.ConcatenateCode)
                                && args.PropertyName != nameof(PlanBeginYearImportModel.IsErrorMLNS))
                            {
                                var voucherDetail = (PlanBeginYearImportModel)sender;
                                int rowIndex = _dataBeginYearImport.IndexOf(voucherDetail);
                                var listError = _importService.ValidateItem<PlanBeginYearImportModel>(voucherDetail, rowIndex);
                                if (listError.Count > 0)
                                {
                                    List<string> errors = listError.Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                                    string message = string.Join(Environment.NewLine, errors);
                                    MessageBoxHelper.Info(message);
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
                    OnPropertyChanged(nameof(DataBeginYearImport));

                    if (_voucherDetailResult.ImportErrors.Count > 0)
                        _errors.AddRange(_voucherDetailResult.ImportErrors);
                    if (DataBeginYearImport.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                        MessageBoxHelper.Warning(Resources.AlertDataError);
                }
                else
                {
                    ImportResult<NsDuToanDauNamNsDacThuImportModel> _dataDacThu = isLoadFromServer ? VoucherDetailDacThuResult : _importService.ProcessDataUnique<NsDuToanDauNamNsDacThuImportModel>(FilePath);
                    ImportResult<NsDtDauNamPhanCapImportModel> _dataPhanCap = isLoadFromServer ? VoucherDetailPhanCapResult : _importService.ProcessDataUnique<NsDtDauNamPhanCapImportModel>(FilePath);
                    _importDataDacThuProcess = new List<NsDuToanDauNamNsDacThuImportModel>();
                    _importDataPhanCapProcess = new List<NsDtDauNamPhanCapImportModel>();
                    #region Data dac thu
                    foreach (var item in _dataDacThu.Data)
                    {
                        if (!item.IsErrorMLNS)
                        {
                            var mlns = _mucLucNganSachs.Where(x => x.XauNoiMa == item.ConcatenateCode).FirstOrDefault();
                            if (mlns != null)
                                item.BHangCha = mlns.BHangCha;
                        }
                        var childs = _dataDacThu.Data.Where(x => x.ConcatenateCode.Length != item.ConcatenateCode.Length && x.ConcatenateCode.Contains(item.ConcatenateCode) && x != item).ToList();
                        if (childs.Count > 0)
                            item.BHangCha = true;
                    }

                    //kiểm tra loại ngân sách con thỏa mãn điều kiện để import
                    foreach (var item in _dataDacThu.Data)
                    {
                        if (!item.ImportStatus && item.IsErrorMLNS)
                        {
                            var parents = _importDataDacThuProcess.Where(x => item.ConcatenateCode.Contains(x.ConcatenateCode) && x != item).ToList();
                            if (parents.Count > 0)
                            {
                                int columnIndexOrigin = 0;
                                NsDuToanDauNamNsDacThuImportModel parent = new NsDuToanDauNamNsDacThuImportModel();
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
                                    if (!string.IsNullOrEmpty(parent.SHangNhap1))
                                        parent.SHangNhap1 = "0";
                                    if (!string.IsNullOrEmpty(parent.SHangMua1))
                                        parent.SHangMua1 = "0";
                                    if (!string.IsNullOrEmpty(parent.SHangNhap2))
                                        parent.SHangNhap2 = "0";
                                    if (!string.IsNullOrEmpty(parent.SHangMua2))
                                        parent.SHangMua2 = "0";
                                    if (!string.IsNullOrEmpty(parent.SHangNhap3))
                                        parent.SHangNhap3 = "0";
                                    if (!string.IsNullOrEmpty(parent.SHangMua3))
                                        parent.SHangMua3 = "0";
                                    if (!string.IsNullOrEmpty(parent.SHangNhap4))
                                        parent.SHangNhap4 = "0";
                                    if (!string.IsNullOrEmpty(parent.SHangMua))
                                        parent.SHangMua = "0";
                                    if (!string.IsNullOrEmpty(parent.SHangNhap5))
                                        parent.SHangNhap5 = "0";
                                    if (!string.IsNullOrEmpty(parent.SHangMua5))
                                        parent.SHangMua5 = "0";
                                    if (!string.IsNullOrEmpty(parent.SUocThucHien))
                                        parent.SUocThucHien = "0";
                                    if (!string.IsNullOrEmpty(parent.SHangNhap))
                                        parent.SHangNhap = "0";
                                    if (!string.IsNullOrEmpty(parent.SHangMua))
                                        parent.SHangMua = "0";
                                    if (!string.IsNullOrEmpty(parent.SPhanCap))
                                        parent.SPhanCap = "0";
                                    if (!string.IsNullOrEmpty(parent.SChuaPhanCap))
                                        parent.SChuaPhanCap = "0";


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
                        _importDataDacThuProcess.Add(item);
                    }
                    CalculateDataDacThu();
                    CanCuItems = new ObservableCollection<NsDuToanDauNamNsDacThuImportModel>(_importDataDacThuProcess);
                    foreach (var item in _cancuItems)
                    {
                        item.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName != nameof(NsDuToanDauNamNsDacThuImportModel.ImportStatus)
                                && args.PropertyName != nameof(NsDuToanDauNamNsDacThuImportModel.ConcatenateCode)
                                && args.PropertyName != nameof(NsDuToanDauNamNsDacThuImportModel.IsErrorMLNS))
                            {
                                var voucherDetail = (NsDuToanDauNamNsDacThuImportModel)sender;
                                int rowIndex = _importDataDacThuProcess.IndexOf(voucherDetail);
                                var listError = _importService.ValidateItem<NsDuToanDauNamNsDacThuImportModel>(voucherDetail, rowIndex);
                                if (listError.Count > 0)
                                {
                                    List<string> errors = listError.Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                                    string message = string.Join(Environment.NewLine, errors);
                                    MessageBoxHelper.Info(message);
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
                    OnPropertyChanged(nameof(_cancuItems));

                    if (_dataDacThu.ImportErrors.Count > 0)
                        _errors.AddRange(_dataDacThu.ImportErrors);
                    if (_cancuItems.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                        MessageBoxHelper.Warning(Resources.AlertDataError);
                    #endregion

                    #region Data phan cap
                    foreach (var item in _dataPhanCap.Data)
                    {
                        if (!item.IsErrorMLNS)
                        {
                            var mlns = _mucLucNganSachs.Where(x => x.XauNoiMa == item.ConcatenateCode).FirstOrDefault();
                            if (mlns != null)
                                item.BHangCha = mlns.BHangCha;
                        }
                        var childs = _dataPhanCap.Data.Where(x => x.ConcatenateCode.Length != item.ConcatenateCode.Length && x.ConcatenateCode.Contains(item.ConcatenateCode) && x != item).ToList();
                        if (childs.Count > 0)
                            item.BHangCha = true;
                    }

                    //kiểm tra loại ngân sách con thỏa mãn điều kiện để import
                    foreach (var item in _dataPhanCap.Data)
                    {
                        if (!item.ImportStatus && item.IsErrorMLNS)
                        {
                            var parents = _importDataPhanCapProcess.Where(x => item.ConcatenateCode.Contains(x.ConcatenateCode) && x != item).ToList();
                            if (parents.Count > 0)
                            {
                                int columnIndexOrigin = 0;
                                NsDtDauNamPhanCapImportModel parent = new NsDtDauNamPhanCapImportModel();
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
                                    if (!string.IsNullOrEmpty(parent.STuChi))
                                        parent.STuChi = "0";


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
                        _importDataPhanCapProcess.Add(item);
                    }
                    CalculateDataPhanCap();
                    PhanCapItems = new ObservableCollection<NsDtDauNamPhanCapImportModel>(_importDataPhanCapProcess);
                    foreach (var item in _phancapItems)
                    {
                        item.PropertyChanged += (sender, args) =>
                        {

                            if (args.PropertyName != nameof(NsDtDauNamPhanCapImportModel.ImportStatus)
                                && args.PropertyName != nameof(NsDtDauNamPhanCapImportModel.ConcatenateCode)
                                && args.PropertyName != nameof(NsDtDauNamPhanCapImportModel.IsErrorMLNS))
                            {
                                var voucherDetail = (NsDtDauNamPhanCapImportModel)sender;
                                int rowIndex = _importDataPhanCapProcess.IndexOf(voucherDetail);
                                var listError = _importService.ValidateItem<NsDtDauNamPhanCapImportModel>(voucherDetail, rowIndex);
                                if (listError.Count > 0)
                                {
                                    List<string> errors = listError.Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                                    string message = string.Join(Environment.NewLine, errors);
                                    MessageBoxHelper.Info(message);
                                    _phancapErrors.AddRange(listError);
                                    voucherDetail.ImportStatus = false;
                                    if (listError.Any(x => x.IsErrorMLNS))
                                        voucherDetail.IsErrorMLNS = true;
                                }
                                else
                                {
                                    voucherDetail.ImportStatus = true;
                                    voucherDetail.IsErrorMLNS = false;
                                    _phancapErrors.RemoveAll(x => x.Row == rowIndex);
                                    OnPropertyChanged(nameof(IsSaveData));
                                }
                            }
                            if (args.PropertyName == nameof(NsDtDauNamPhanCapImportModel.SMaDonViMoi))
                            {
                                if (item.SMaDonViMoi != item.SMaDonViCu)
                                {
                                    item.SMaDonViCu = item.SMaDonViMoi;
                                    foreach (var child in PhanCapItems.Where(n => n.SMaDonVi == item.SMaDonVi))
                                    {
                                        child.SMaDonViMoi = item.SMaDonViMoi;
                                    }
                                }
                            }
                        };
                    }
                    OnPropertyChanged(nameof(_phancapItems));

                    if (_dataPhanCap.ImportErrors.Count > 0)
                        _phancapErrors.AddRange(_dataPhanCap.ImportErrors);
                    if (_phancapItems.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                        MessageBoxHelper.Warning(Resources.AlertDataError);

                    #endregion
                }

                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                //MessageBoxHelper.Warning(Resources.MsgCheckFormatFileImport);
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

        private void OnMerge()
        {
            try
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
                OnPropertyChanged(nameof(ExistedMlns));
                OnPropertyChanged(nameof(ImportedMlns));
                OnPropertyChanged(nameof(IsEnabledMergeBtn));
                OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                OnPropertyChanged(nameof(IsEnableSaveMLNS));
                OnSelectionChanged();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnUnMerge()
        {
            try
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
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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

        private void OnSaveMLNS()
        {
            var result = MessageBoxHelper.Confirm(Resources.ConfirmAddMLNS);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    List<NsMucLucNganSach> listMLNS = _mapper.Map<List<NsMucLucNganSach>>(_mergeItems);
                    _mucLucNganSachService.AddRangeWithMLSKT(listMLNS);
                    _existedMlns.Where(x => x.IsModified).Select(x => { x.IsModified = false; x.IsSelected = false; return x; }).ToList();
                    _mergeItems = new List<NsMuclucNgansachModel>();
                    OnPropertyChanged(nameof(ExistedMlns));
                    OnPropertyChanged(nameof(IsEnableSaveMLNS));
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    foreach (var item in listMLNS)
                    {
                        var importItem = _dataBeginYearImport.Where(x => x.ConcatenateCode == item.XauNoiMa).FirstOrDefault();
                        var listError = _importService.ValidateItem<PlanBeginYearImportModel>(importItem, _dataBeginYearImport.IndexOf(importItem));
                        if (listError.Count == 0)
                        {
                            importItem.ImportStatus = true;
                            importItem.IsErrorMLNS = false;
                            TabIndex = ImportTabIndex.Data;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                    MessageBoxHelper.Warning(Resources.MsgSaveError);
                }
            }
        }

        private void CalculateData()
        {
            // Reset value parrent
            foreach (var item in _importDataDetailProcess.Where(x => x.BHangCha))
            {
                if (!string.IsNullOrEmpty(item.FTuChi1))
                    item.FTuChi1 = "0";
                if (!string.IsNullOrEmpty(item.FTuChi2))
                    item.FTuChi2 = "0";
                if (!string.IsNullOrEmpty(item.FTuChi3))
                    item.FTuChi3 = "0";
                if (!string.IsNullOrEmpty(item.FTuChi4))
                    item.FTuChi4 = "0";
                if (!string.IsNullOrEmpty(item.FTuChi5))
                    item.FTuChi5 = "0";

                if (!string.IsNullOrEmpty(item.UocThucHien))
                    item.UocThucHien = "0";
                if (!string.IsNullOrEmpty(item.ChiTiet))
                    item.ChiTiet = "0";
                if (!string.IsNullOrEmpty(item.HangNhap))
                    item.HangNhap = "0";
                if (!string.IsNullOrEmpty(item.HangMua))
                    item.HangMua = "0";
                if (!string.IsNullOrEmpty(item.PhanCap))
                    item.PhanCap = "0";
                if (!string.IsNullOrEmpty(item.ChuaPhanCap))
                    item.ChuaPhanCap = "0";
            }
            foreach (var item in _importDataDetailProcess.Where(x => !x.BHangCha))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(PlanBeginYearImportModel currentItem, PlanBeginYearImportModel selfItem)
        {
            List<PlanBeginYearImportModel> parents = new List<PlanBeginYearImportModel>();
            if (!currentItem.IsWarning)
                parents = _importDataDetailProcess.Where(x => currentItem.ConcatenateCode.Contains(x.ConcatenateCode)
                           && x.BHangCha && currentItem.ConcatenateCode != x.ConcatenateCode && x.ConcatenateCode.Substring(0, 1).Equals(currentItem.ConcatenateCode.Substring(0, 1))).OrderByDescending(x => x.ConcatenateCode).ToList();
            else parents = _importDataDetailProcess.Where(x => currentItem.ConcatenateCode.Contains(x.ConcatenateCode) && currentItem.ConcatenateCode != x.ConcatenateCode).OrderByDescending(x => x.LNS).ToList();
            if (parents.Count > 0)
            {
                int columnIndexOrigin = 0;
                PlanBeginYearImportModel parent = new PlanBeginYearImportModel();
                foreach (var p in parents)
                {
                    int maxColumn = p.ConcatenateCode.Split("-").Count();
                    if (maxColumn > columnIndexOrigin)
                    {
                        columnIndexOrigin = maxColumn;
                        parent = p;
                    }
                }
                double parentValue = 0, childValue = 0;
                if (Double.TryParse(parent.FTuChi1, out parentValue) && Double.TryParse(selfItem.FTuChi1, out childValue))
                    parent.FTuChi1 = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.FTuChi2, out parentValue) && Double.TryParse(selfItem.FTuChi2, out childValue))
                    parent.FTuChi2 = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.FTuChi3, out parentValue) && Double.TryParse(selfItem.FTuChi3, out childValue))
                    parent.FTuChi3 = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.FTuChi4, out parentValue) && Double.TryParse(selfItem.FTuChi4, out childValue))
                    parent.FTuChi4 = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.FTuChi5, out parentValue) && Double.TryParse(selfItem.FTuChi5, out childValue))
                    parent.FTuChi5 = (parentValue + childValue).ToString();

                if (Double.TryParse(parent.UocThucHien, out parentValue) && Double.TryParse(selfItem.UocThucHien, out childValue))
                    parent.UocThucHien = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.ChiTiet, out parentValue) && Double.TryParse(selfItem.ChiTiet, out childValue))
                    parent.ChiTiet = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.HangNhap, out parentValue) && Double.TryParse(selfItem.HangNhap, out childValue))
                    parent.HangNhap = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.HangMua, out parentValue) && Double.TryParse(selfItem.HangMua, out childValue))
                    parent.HangMua = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.PhanCap, out parentValue) && Double.TryParse(selfItem.PhanCap, out childValue))
                    parent.PhanCap = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.ChuaPhanCap, out parentValue) && Double.TryParse(selfItem.ChuaPhanCap, out childValue))
                    parent.ChuaPhanCap = (parentValue + childValue).ToString();
                CalculateParent(parent, selfItem);
            }
            else return;
        }

        private void CalculateDataDacThu()
        {
            foreach (var item in _importDataDacThuProcess.Where(x => x.BHangCha))
            {
                if (!string.IsNullOrEmpty(item.SHangNhap1))
                    item.SHangNhap1 = "0";
                if (!string.IsNullOrEmpty(item.SHangMua1))
                    item.SHangMua1 = "0";
                if (!string.IsNullOrEmpty(item.SHangNhap2))
                    item.SHangNhap2 = "0";
                if (!string.IsNullOrEmpty(item.SHangMua2))
                    item.SHangMua2 = "0";
                if (!string.IsNullOrEmpty(item.SHangNhap3))
                    item.SHangNhap3 = "0";
                if (!string.IsNullOrEmpty(item.SHangMua3))
                    item.SHangMua3 = "0";
                if (!string.IsNullOrEmpty(item.SHangNhap4))
                    item.SHangNhap4 = "0";
                if (!string.IsNullOrEmpty(item.SHangMua))
                    item.SHangMua = "0";
                if (!string.IsNullOrEmpty(item.SHangNhap5))
                    item.SHangNhap5 = "0";
                if (!string.IsNullOrEmpty(item.SHangMua5))
                    item.SHangMua5 = "0";
                if (!string.IsNullOrEmpty(item.SUocThucHien))
                    item.SUocThucHien = "0";
                if (!string.IsNullOrEmpty(item.SHangNhap))
                    item.SHangNhap = "0";
                if (!string.IsNullOrEmpty(item.SHangMua))
                    item.SHangMua = "0";
                if (!string.IsNullOrEmpty(item.SPhanCap))
                    item.SPhanCap = "0";
                if (!string.IsNullOrEmpty(item.SChuaPhanCap))
                    item.SChuaPhanCap = "0";
            }
            foreach (var item in _importDataDacThuProcess.Where(x => !x.BHangCha))
            {
                CalculateParentDacThu(item, item);
            }
        }

        private void CalculateParentDacThu(NsDuToanDauNamNsDacThuImportModel currentItem, NsDuToanDauNamNsDacThuImportModel selfItem)
        {
            List<NsDuToanDauNamNsDacThuImportModel> parents = new List<NsDuToanDauNamNsDacThuImportModel>();
            if (!currentItem.IsWarning)
                parents = _importDataDacThuProcess.Where(x => currentItem.ConcatenateCode.Contains(x.ConcatenateCode)
                           && x.BHangCha && currentItem.ConcatenateCode != x.ConcatenateCode && x.ConcatenateCode.Substring(0, 1).Equals(currentItem.ConcatenateCode.Substring(0, 1))).OrderByDescending(x => x.ConcatenateCode).ToList();
            else parents = _importDataDacThuProcess.Where(x => currentItem.ConcatenateCode.Contains(x.ConcatenateCode) && currentItem.ConcatenateCode != x.ConcatenateCode).OrderByDescending(x => x.LNS).ToList();
            if (parents.Count > 0)
            {
                int columnIndexOrigin = 0;
                NsDuToanDauNamNsDacThuImportModel parent = new NsDuToanDauNamNsDacThuImportModel();
                foreach (var p in parents)
                {
                    int maxColumn = p.ConcatenateCode.Split("-").Count();
                    if (maxColumn > columnIndexOrigin)
                    {
                        columnIndexOrigin = maxColumn;
                        parent = p;
                    }
                }
                double parentValue = 0, childValue = 0;
                if (Double.TryParse(parent.SHangNhap1, out parentValue) && Double.TryParse(selfItem.SHangNhap1, out childValue))
                    parent.SHangNhap1 = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.SHangMua1, out parentValue) && Double.TryParse(selfItem.SHangMua1, out childValue))
                    parent.SHangMua1 = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.SHangNhap2, out parentValue) && Double.TryParse(selfItem.SHangNhap2, out childValue))
                    parent.SHangNhap2 = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.SHangMua2, out parentValue) && Double.TryParse(selfItem.SHangMua2, out childValue))
                    parent.SHangMua2 = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.SHangNhap3, out parentValue) && Double.TryParse(selfItem.SHangNhap3, out childValue))
                    parent.SHangNhap3 = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.SHangMua3, out parentValue) && Double.TryParse(selfItem.SHangMua3, out childValue))
                    parent.SHangMua3 = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.SHangNhap4, out parentValue) && Double.TryParse(selfItem.SHangNhap4, out childValue))
                    parent.SHangNhap4 = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.SHangNhap5, out parentValue) && Double.TryParse(selfItem.SHangNhap5, out childValue))
                    parent.SHangNhap5 = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.SHangMua5, out parentValue) && Double.TryParse(selfItem.SHangMua5, out childValue))
                    parent.SHangMua5 = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.SUocThucHien, out parentValue) && Double.TryParse(selfItem.SUocThucHien, out childValue))
                    parent.SUocThucHien = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.SHangNhap, out parentValue) && Double.TryParse(selfItem.SHangNhap, out childValue))
                    parent.SHangNhap = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.SPhanCap, out parentValue) && Double.TryParse(selfItem.SPhanCap, out childValue))
                    parent.SPhanCap = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.SChuaPhanCap, out parentValue) && Double.TryParse(selfItem.SChuaPhanCap, out childValue))
                    parent.SChuaPhanCap = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.SHangMua, out parentValue) && Double.TryParse(selfItem.SHangMua, out childValue))
                    parent.SHangMua = (parentValue + childValue).ToString();

                CalculateParentDacThu(parent, selfItem);
            }
            else return;
        }

        private void CalculateDataPhanCap()
        {
            foreach (var item in _importDataPhanCapProcess.Where(x => x.BHangCha))
            {
                if (!string.IsNullOrEmpty(item.STuChi))
                    item.STuChi = "0";
            }
            foreach (var item in _importDataPhanCapProcess.Where(x => !x.BHangCha))
            {
                CalculateParentPhanCap(item, item);
            }
        }

        private void CalculateParentPhanCap(NsDtDauNamPhanCapImportModel currentItem, NsDtDauNamPhanCapImportModel selfItem)
        {
            List<NsDtDauNamPhanCapImportModel> parents = new List<NsDtDauNamPhanCapImportModel>();
            if (!currentItem.IsWarning)
                parents = _importDataPhanCapProcess.Where(x => currentItem.ConcatenateCode.Contains(x.ConcatenateCode)
                           && x.BHangCha && currentItem.ConcatenateCode != x.ConcatenateCode).OrderByDescending(x => x.LNS).ToList();
            else parents = _importDataPhanCapProcess.Where(x => currentItem.ConcatenateCode.Contains(x.ConcatenateCode) && currentItem.ConcatenateCode != x.ConcatenateCode).OrderByDescending(x => x.LNS).ToList();
            if (parents.Count > 0)
            {
                int columnIndexOrigin = 0;
                NsDtDauNamPhanCapImportModel parent = new NsDtDauNamPhanCapImportModel();
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
                if (Double.TryParse(parent.STuChi, out parentValue) && Double.TryParse(selfItem.STuChi, out childValue))
                    parent.STuChi = (parentValue + childValue).ToString();

                CalculateParentPhanCap(parent, selfItem);
            }
            else return;
        }

        private Guid AddChungTu(string lns)
        {
            int soChungTuIndex = _sktChungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork,
                          _sessionService.Current.Budget, _sessionService.Current.YearOfBudget, int.Parse(LoaiChungTu));
            NsDtdauNamChungTu entity = new NsDtdauNamChungTu();
            entity.ISoChungTuIndex = soChungTuIndex;
            entity.SSoChungTu = SoChungTu;
            entity.SDslns = lns;
            entity.DNgayChungTu = NgayChungTu;
            entity.IIdMaDonVi = SelectedDonVi != null ? SelectedDonVi.ValueItem : string.Empty;
            entity.INamLamViec = _sessionService.Current.YearOfWork;
            entity.DNgayTao = DateTime.Now;
            entity.SNguoiTao = _sessionService.Current.Principal;
            entity.BKhoa = false;
            entity.ILoaiChungTu = int.Parse(LoaiChungTu);
            entity.INamNganSach = _sessionService.Current.YearOfBudget;
            entity.IIdMaNguonNganSach = _sessionService.Current.Budget;
            entity.ILoaiNguonNganSach = Int32.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
            _sktChungTuService.Add(entity);
            return entity.Id;
        }

        private void OnSaveData()
        {
            try
            {
                string message = string.Empty;
                bool checkValidate = Validate(ref message);
                if (!string.IsNullOrEmpty(message) || !checkValidate)
                {
                    MessageBoxHelper.Warning(message);
                    return;
                }
                _mucLucNganSachs = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
                _dicMucLuc = new Dictionary<string, NsMucLucNganSach>();
                if (_mucLucNganSachs != null)
                {
                    foreach (var item in _mucLucNganSachs.Where(n => n.BHangChaDuToan.HasValue && !n.BHangChaDuToan.Value))
                        if (!_dicMucLuc.ContainsKey(item.XauNoiMa))
                            _dicMucLuc.Add(item.XauNoiMa, item);
                }
                if (SelectedChungTu != null)
                {
                    if (SelectedChungTu.ValueItem == VoucherType.NSSD_Key)
                    {
                        OnSaveNsSuDung();
                    }
                    else
                    {
                        OnSaveNsDacThu();
                    }
                }

                /*
                var pathDir = Path.Combine(IOExtensions.ApplicationPath, ConstantUrlPathPhanHe.UrlFolderTemp);
                IOExtensions.ClearForlder(pathDir);
                */
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnSaveNsSuDung()
        {
            var dataImport = _dataBeginYearImport.Where(x => x.ImportStatus
                && !x.IsWarning && !string.IsNullOrEmpty(x.ConcatenateCode) && _dicMucLuc.ContainsKey(x.ConcatenateCode));
            List<NsDtdauNamChungTuChiTiet> chungTuChiTiets = new List<NsDtdauNamChungTuChiTiet>();
            if (dataImport != null)
            {
                foreach (var item in dataImport)
                {
                    chungTuChiTiets.Add(new NsDtdauNamChungTuChiTiet()
                    {
                        BHangCha = item.BHangCha,
                        SLns = item.LNS,
                        SL = item.L,
                        SK = item.K,
                        SM = item.M,
                        STm = item.TM,
                        STtm = item.TTM,
                        SNg = item.NG,
                        STng = item.TNG,
                        STng1 = item.TNG1,
                        STng2 = item.TNG2,
                        STng3 = item.TNG3,
                        SXauNoiMa = item.XauNoiMa,
                        SMoTa = item.MoTa,
                        FTuChi = !string.IsNullOrEmpty(item.ChiTiet) ? double.Parse(item.ChiTiet) : 0,
                        FChuaPhanCap = !string.IsNullOrEmpty(item.ChuaPhanCap) ? double.Parse(item.ChuaPhanCap) : 0,
                        FHangMua = !string.IsNullOrEmpty(item.HangMua) ? double.Parse(item.HangMua) : 0,
                        FHangNhap = !string.IsNullOrEmpty(item.HangNhap) ? double.Parse(item.HangNhap) : 0,
                        FPhanCap = !string.IsNullOrEmpty(item.PhanCap) ? double.Parse(item.PhanCap) : 0,
                        FUocThucHien = !string.IsNullOrEmpty(item.UocThucHien) ? double.Parse(item.UocThucHien) : 0
                    });
                }
            }

            List<ImpSktSoLieuChiTiet> impChungTuChiTiets = new List<ImpSktSoLieuChiTiet>();
            {
                if (_dataBeginYearImport.Any(x => x.ImportStatus && !x.IsWarning && _dicMucLuc.ContainsKey(x.ConcatenateCode)))
                {
                    foreach (var item in _dataBeginYearImport.Where(x => x.ImportStatus && !x.IsWarning && _dicMucLuc.ContainsKey(x.ConcatenateCode)))
                    {
                        impChungTuChiTiets.Add(new ImpSktSoLieuChiTiet()
                        {
                            BHangCha = item.BHangCha,
                            Lns = item.LNS,
                            L = item.L,
                            K = item.K,
                            M = item.M,
                            Tm = item.TM,
                            Ttm = item.TTM,
                            Ng = item.NG,
                            Tng = item.TNG,
                            XauNoiMa = item.XauNoiMa,
                            MoTa = item.MoTa,
                            DuPhong = !string.IsNullOrEmpty(item.UocThucHien) ? double.Parse(item.UocThucHien) : 0,
                            HangNhap = !string.IsNullOrEmpty(item.HangNhap) ? double.Parse(item.HangNhap) : 0,
                            HangMua = !string.IsNullOrEmpty(item.HangMua) ? double.Parse(item.HangMua) : 0,
                            PhanCap = !string.IsNullOrEmpty(item.PhanCap) ? double.Parse(item.PhanCap) : 0,
                            ChuaPhanCap = !string.IsNullOrEmpty(item.ChuaPhanCap) ? double.Parse(item.ChuaPhanCap) : 0,
                            TuChi = !string.IsNullOrEmpty(item.ChiTiet) ? double.Parse(item.ChiTiet) : 0
                        });
                    }
                }
            }
            List<string> listLNS = (chungTuChiTiets != null && chungTuChiTiets.Count > 0) ? chungTuChiTiets.Select(n => n.SLns).ToList() : new List<string>();
            listLNS = StringUtils.GetListXauNoiMaParent(listLNS);
            string strLNS = string.Join(",", listLNS.Distinct().ToList());
            //chung tu
            Guid chungTuId = AddChungTu(strLNS);

            MessageBoxResult dialogResult = MessageBoxHelper.Confirm("Lưu dữ liệu căn cứ trên file excel?");
            if (dialogResult == MessageBoxResult.Yes)
            {
                List<NsDtdauNamChungTuChiTietCanCu> lstCanCu = new List<NsDtdauNamChungTuChiTietCanCu>();
                foreach (var item in _dataBeginYearImport.Where(x => x.ImportStatus
                    && !x.IsWarning && _dicMucLuc.ContainsKey(x.ConcatenateCode)))
                {
                    var data = new NsDtdauNamChungTuChiTietCanCu()
                    {
                        DNgayTao = DateTime.Now,
                        IIdMaDonVi = SelectedDonVi != null ? SelectedDonVi.ValueItem : string.Empty,
                        IIdMaNguonNganSach = _sessionService.Current.Budget,
                        ILoaiChungTu = int.Parse(LoaiChungTu),
                        INamLamViec = _sessionService.Current.YearOfWork,
                        INamNganSach = _sessionService.Current.YearOfBudget,
                        SLns = item.LNS,
                        SL = item.L,
                        SK = item.K,
                        SM = item.M,
                        STm = item.TM,
                        STtm = item.TTM,
                        SNg = item.NG,
                        STng = item.TNG,
                        SNguoiTao = _sessionService.Current.Principal,
                        STenDonVi = SelectedDonVi != null ? SelectedDonVi.HiddenValue : string.Empty,
                        SXauNoiMa = item.XauNoiMa,
                        IID_CTDTDauNam = chungTuId
                    };
                    if (_dicCauHinhCanCu.ContainsKey(1) && !string.IsNullOrEmpty(item.FTuChi1) && item.FTuChi1 != "0")
                    {
                        var dataCanCu = data.Clone();
                        dataCanCu.FTuChi = double.Parse(item.FTuChi1);
                        dataCanCu.IIdCanCu = _dicCauHinhCanCu[1].Id;
                        lstCanCu.Add(dataCanCu);
                    }
                    if (_dicCauHinhCanCu.ContainsKey(2) && !string.IsNullOrEmpty(item.FTuChi2) && item.FTuChi2 != "0")
                    {
                        var dataCanCu = data.Clone();
                        dataCanCu.FTuChi = double.Parse(item.FTuChi2);
                        dataCanCu.IIdCanCu = _dicCauHinhCanCu[2].Id;
                        lstCanCu.Add(dataCanCu);
                    }
                    if (_dicCauHinhCanCu.ContainsKey(3) && !string.IsNullOrEmpty(item.FTuChi3) && item.FTuChi3 != "0")
                    {
                        var dataCanCu = data.Clone();
                        dataCanCu.FTuChi = double.Parse(item.FTuChi3);
                        dataCanCu.IIdCanCu = _dicCauHinhCanCu[3].Id;
                        lstCanCu.Add(dataCanCu);
                    }
                    if (_dicCauHinhCanCu.ContainsKey(4) && !string.IsNullOrEmpty(item.FTuChi4) && item.FTuChi4 != "0")
                    {
                        var dataCanCu = data.Clone();
                        dataCanCu.FTuChi = double.Parse(item.FTuChi4);
                        dataCanCu.IIdCanCu = _dicCauHinhCanCu[4].Id;
                        lstCanCu.Add(dataCanCu);
                    }
                    if (_dicCauHinhCanCu.ContainsKey(5) && !string.IsNullOrEmpty(item.FTuChi5) && item.FTuChi5 != "0")
                    {
                        var dataCanCu = data.Clone();
                        dataCanCu.FTuChi = double.Parse(item.FTuChi5);
                        dataCanCu.IIdCanCu = _dicCauHinhCanCu[5].Id;
                        lstCanCu.Add(dataCanCu);
                    }
                }
                if (lstCanCu.Count != 0)
                    _sktSoLieuChiTietCanCuDataService.AddRange(lstCanCu);
            }

            foreach (var item in chungTuChiTiets)
            {
                item.INamNganSach = _sessionService.Current.YearOfBudget;
                item.IIdMaNguonNganSach = _sessionService.Current.Budget;
                item.INamLamViec = _sessionService.Current.YearOfWork;
                item.BHangCha = false;
                item.ILoai = 3;
                item.IIdMaDonVi = SelectedDonVi != null ? SelectedDonVi.ValueItem : string.Empty;
                item.STenDonVi = SelectedDonVi != null ? SelectedDonVi.HiddenValue : string.Empty;
                item.DNgayTao = DateTime.Now;
                item.SNguoiTao = _sessionService.Current.Principal;
                item.DNgaySua = DateTime.Now;
                item.SNguoiSua = _sessionService.Current.Principal;
                item.BKhoa = false;
                item.IIdCtdtdauNam = chungTuId;
                item.ILoaiChungTu = LoaiChungTu;
                //if(_dicCauHinhCanCu.ContainsKey(1) && item.FTuChi)
            }
            _sktSoLieuService.AddRange(chungTuChiTiets);
            foreach (var item in impChungTuChiTiets)
            {
                item.NamNganSach = _sessionService.Current.YearOfBudget;
                item.NguonNganSach = _sessionService.Current.Budget;
                item.NamLamViec = _sessionService.Current.YearOfWork;
                item.BHangCha = false;
                item.ILoai = 3;
                item.ITrangThai = NSEntityStatus.ACTIVED;
                item.IdDonVi = SelectedDonVi != null ? SelectedDonVi.ValueItem : string.Empty;
                item.TenDonVi = SelectedDonVi != null ? SelectedDonVi.HiddenValue : string.Empty;
                item.DateCreated = DateTime.Now;
                item.UserCreator = _sessionService.Current.Principal;
                item.DateModified = DateTime.Now;
                item.UserModifier = _sessionService.Current.Principal;
                item.IsLocked = false;
                item.LoaiChungTu = LoaiChungTu;
            }
            _impSktSoLieuService.AddRange(impChungTuChiTiets);
            //update tong
            _sktChungTuService.UpdateTotalChungTu(SelectedDonVi != null ? SelectedDonVi.ValueItem : string.Empty,
                SelectedDonVi != null ? SelectedDonVi.Type : string.Empty, int.Parse(LoaiChungTu),
                _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, int.Parse(BudgetSourceTypeSelected.ValueItem), chungTuId.ToString());
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(new DuToanDauNamDonViChungTu { IdDonVi = SelectedDonVi != null ? SelectedDonVi.ValueItem : string.Empty, LoaiChungTu = LoaiChungTu });
        }

        private void OnSaveNsDacThu()
        {
            List<string> listLNS = (CanCuItems != null && CanCuItems.Count > 0) ? CanCuItems.Select(n => n.LNS).ToList() : new List<string>();
            listLNS = StringUtils.GetListXauNoiMaParent(listLNS);
            string strLNS = string.Join(",", listLNS.Distinct().ToList());
            //chung tu
            Guid chungTuId = AddChungTu(strLNS);

            List<NsDtdauNamChungTuChiTiet> lstChiTiet = new List<NsDtdauNamChungTuChiTiet>();
            List<NsDtdauNamChungTuChiTietCanCu> lstCanCu = new List<NsDtdauNamChungTuChiTietCanCu>();
            List<NsDtdauNamPhanCap> lstPhanCap = new List<NsDtdauNamPhanCap>();
            Dictionary<string, Guid> dicDetail = new Dictionary<string, Guid>();
            MessageBoxResult dialogResult = MessageBoxHelper.Confirm("Lưu dữ liệu căn cứ trên file excel?");
            if (dialogResult == MessageBoxResult.Yes)
            {
                foreach (var item in CanCuItems.Where(x => x.ImportStatus
                                                           && !x.IsWarning && _dicMucLuc.ContainsKey(x.ConcatenateCode)))
                {
                    var data = new NsDtdauNamChungTuChiTietCanCu()
                    {
                        DNgayTao = DateTime.Now,
                        IIdMaDonVi = SelectedDonVi != null ? SelectedDonVi.ValueItem : string.Empty,
                        IIdMaNguonNganSach = _sessionService.Current.Budget,
                        ILoaiChungTu = int.Parse(LoaiChungTu),
                        INamLamViec = _sessionService.Current.YearOfWork,
                        INamNganSach = _sessionService.Current.YearOfBudget,
                        SLns = item.LNS,
                        SL = item.L,
                        SK = item.K,
                        SM = item.M,
                        STm = item.TM,
                        STtm = item.TTM,
                        SNg = item.NG,
                        STng = item.TNG,
                        SNguoiTao = _sessionService.Current.Principal,
                        STenDonVi = SelectedDonVi != null ? SelectedDonVi.HiddenValue : string.Empty,
                        SXauNoiMa = item.ConcatenateCode,
                        IID_CTDTDauNam = chungTuId
                    };
                    if (_dicCauHinhCanCuDamBao.ContainsKey(TypeCanCu.SETTLEMENT) && (!string.IsNullOrEmpty(item.SHangNhap1) && item.SHangNhap1 != "0") || (!string.IsNullOrEmpty(item.SHangMua1) && item.SHangMua1 != "0"))
                    {
                        var dataCanCu = data.Clone();
                        dataCanCu.FHangNhap = double.Parse(item.SHangNhap1);
                        dataCanCu.FHangMua = double.Parse(item.SHangMua1);
                        dataCanCu.IIdCanCu = _dicCauHinhCanCuDamBao[TypeCanCu.SETTLEMENT].Id;
                        lstCanCu.Add(dataCanCu);
                    }
                    if (_dicCauHinhCanCuDamBao.ContainsKey(TypeCanCu.ESTIMATE) && (!string.IsNullOrEmpty(item.SHangNhap2) && item.SHangNhap2 != "0") || (!string.IsNullOrEmpty(item.SHangMua2) && item.SHangMua2 != "0"))
                    {
                        var dataCanCu = data.Clone();
                        dataCanCu.FHangNhap = double.Parse(item.SHangNhap2);
                        dataCanCu.FHangMua = double.Parse(item.SHangMua2);
                        dataCanCu.IIdCanCu = _dicCauHinhCanCuDamBao[TypeCanCu.ESTIMATE].Id;
                        lstCanCu.Add(dataCanCu);
                    }
                    if (_dicCauHinhCanCuDamBao.ContainsKey(TypeCanCu.ALLOCATION) && (!string.IsNullOrEmpty(item.SHangNhap3) && item.SHangNhap3 != "0") || (!string.IsNullOrEmpty(item.SHangMua3) && item.SHangMua3 != "0"))
                    {
                        var dataCanCu = data.Clone();
                        dataCanCu.FHangNhap = double.Parse(item.SHangNhap3);
                        dataCanCu.FHangMua = double.Parse(item.SHangMua3);
                        dataCanCu.IIdCanCu = _dicCauHinhCanCuDamBao[TypeCanCu.ESTIMATE].Id;
                        lstCanCu.Add(dataCanCu);
                    }
                    if (_dicCauHinhCanCuDamBao.ContainsKey(TypeCanCu.DEMAND) && (!string.IsNullOrEmpty(item.SHangNhap4) && item.SHangNhap4 != "0") || (!string.IsNullOrEmpty(item.SHangMua4) && item.SHangMua4 != "0"))
                    {
                        var dataCanCu = data.Clone();
                        dataCanCu.FHangNhap = double.Parse(item.SHangNhap4);
                        dataCanCu.FHangMua = double.Parse(item.SHangMua4);
                        dataCanCu.IIdCanCu = _dicCauHinhCanCuDamBao[TypeCanCu.DEMAND].Id;
                        lstCanCu.Add(dataCanCu);
                    }
                    if (_dicCauHinhCanCuDamBao.ContainsKey(TypeCanCu.CHECK_NUMBER) && (!string.IsNullOrEmpty(item.SHangNhap5) && item.SHangNhap5 != "0") || (!string.IsNullOrEmpty(item.SHangMua5) && item.SHangMua5 != "0"))
                    {
                        var dataCanCu = data.Clone();
                        dataCanCu.FHangNhap = double.Parse(item.SHangNhap5);
                        dataCanCu.FHangMua = double.Parse(item.SHangMua5);
                        dataCanCu.IIdCanCu = _dicCauHinhCanCuDamBao[TypeCanCu.CHECK_NUMBER].Id;
                        lstCanCu.Add(dataCanCu);
                    }
                }
            }
            foreach (var item in CanCuItems.Where(x => x.ImportStatus
                && !x.IsWarning && _dicMucLuc.ContainsKey(x.ConcatenateCode)))
            {
                NsDtdauNamChungTuChiTiet objDetail = new NsDtdauNamChungTuChiTiet()
                {
                    Id = Guid.NewGuid(),
                    INamNganSach = _sessionService.Current.YearOfBudget,
                    IIdMaNguonNganSach = _sessionService.Current.Budget,
                    INamLamViec = _sessionService.Current.YearOfWork,
                    SXauNoiMa = item.ConcatenateCode,
                    BHangCha = false,
                    ILoai = 3,
                    SLns = item.LNS,
                    SL = item.L,
                    SK = item.K,
                    SM = item.M,
                    STm = item.TM,
                    STtm = item.TTM,
                    SNg = item.NG,
                    STng = item.TNG,
                    STng1 = item.TNG1,
                    STng2 = item.TNG2,
                    STng3 = item.TNG3,
                    SMoTa = item.Description,
                    IIdMaDonVi = SelectedDonVi != null ? SelectedDonVi.ValueItem : string.Empty,
                    STenDonVi = SelectedDonVi != null ? SelectedDonVi.HiddenValue : string.Empty,
                    DNgayTao = DateTime.Now,
                    SNguoiTao = _sessionService.Current.Principal,
                    DNgaySua = DateTime.Now,
                    SNguoiSua = _sessionService.Current.Principal,
                    BKhoa = false,
                    IIdCtdtdauNam = chungTuId,
                    ILoaiChungTu = LoaiChungTu,
                    FChuaPhanCap = double.Parse(item.SChuaPhanCap),
                    FHangMua = double.Parse(item.SHangMua),
                    FHangNhap = double.Parse(item.SHangNhap),
                    FPhanCap = double.Parse(item.SPhanCap),
                    FUocThucHien = double.Parse(item.SUocThucHien)
                };
                if (!dicDetail.ContainsKey(objDetail.SXauNoiMa))
                    dicDetail.Add(objDetail.SXauNoiMa, objDetail.Id);
                lstChiTiet.Add(objDetail);
            }

            foreach (var item in PhanCapItems.Where(x => x.ImportStatus
                && !x.IsWarning && _dicMucLuc.ContainsKey(x.ConcatenateCode)))
            {
                if (!dicDetail.ContainsKey(item.SXauNoiMaGoc)) continue;
                NsDtdauNamPhanCap obj = new NsDtdauNamPhanCap()
                {
                    DNgayTao = DateTime.Now,
                    SNguoiTao = _sessionService.Current.Principal,
                    DNgaySua = DateTime.Now,
                    SNguoiSua = _sessionService.Current.Principal,
                    FTuChi = double.Parse(item.STuChi),
                    IIdMaDonVi = string.IsNullOrEmpty(item.SMaDonViMoi) ? item.SMaDonVi : item.SMaDonViMoi,
                    IIdMlns = _dicMucLuc[item.ConcatenateCode].MlnsId,
                    INamLamViec = _sessionService.Current.YearOfWork,
                    SXauNoiMa = item.ConcatenateCode,
                    IIdCtdtDauNam = chungTuId,
                    sXauNoiMaGoc = item.SXauNoiMaGoc,
                    IIdCtdtdauNamChiTiet = dicDetail[item.SXauNoiMaGoc]
                };
                if (!string.IsNullOrEmpty(obj.IIdMaDonVi) && _dicDonVi.ContainsKey(obj.IIdMaDonVi))
                {
                    obj.STenDonVi = _dicDonVi[obj.IIdMaDonVi].TenDonVi;
                }
                lstPhanCap.Add(obj);
            }
            _sktSoLieuService.AddRange(lstChiTiet);
            _sktSoLieuChiTietCanCuDataService.AddRange(lstCanCu);
            _soLieuChiTietPhanCapService.AddRange(lstPhanCap);

            _sktChungTuService.UpdateTotalChungTu(SelectedDonVi != null ? SelectedDonVi.ValueItem : string.Empty,
                SelectedDonVi != null ? SelectedDonVi.Type : string.Empty, int.Parse(LoaiChungTu),
                _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, int.Parse(BudgetSourceTypeSelected.ValueItem), chungTuId.ToString());
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(new DuToanDauNamDonViChungTu { IdDonVi = SelectedDonVi != null ? SelectedDonVi.ValueItem : string.Empty, LoaiChungTu = LoaiChungTu });
        }

        private async Task OnGetFileFtpCommand(bool isSendHTTP)
        {
            IsSendHTTP = isSendHTTP;
            var fileFilter = new FileFilterModel();
            fileFilter.AgencyCode = null;
            fileFilter.Module = "BUDGET";
            fileFilter.SubModule = SelectedChungTu.ValueItem == VoucherType.NSSD_Key ? NSFunctionCode.BUDGET_USE_DEMANDCHECK_PLAN_BEGIN_YEAR : NSFunctionCode.BUDGET_GUARANTEE_DEMANDCHECK_PLAN_BEGIN_YEAR;
            fileFilter.Quarter = "";
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
                return;
            }
            LstFile = new ObservableCollection<FileFilterQuery>(lst);
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
                    if (SelectedChungTu.ValueItem == VoucherType.NSSD_Key)
                    {
                        VoucherDetailResult = _importService.ProcessDataUnique<PlanBeginYearImportModel>(fileStream, file.FileTokenKey);
                    }
                    else
                    {
                        VoucherDetailDacThuResult = _importService.ProcessDataUnique<NsDuToanDauNamNsDacThuImportModel>(fileStream, file.FileTokenKey);
                        VoucherDetailPhanCapResult = _importService.ProcessDataUnique<NsDtDauNamPhanCapImportModel>(fileStream, file.FileTokenKey);
                    }
                    OnProcessFile(true);
                }, (s, e) =>
                {
                    IsLoading = false;
                });
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        private void OnOpenReferencePopup(object obj)
        {
            System.Windows.Controls.DataGrid dataGrid = obj as System.Windows.Controls.DataGrid;
            if (dataGrid.CurrentCell.Column.SortMemberPath.Equals(nameof(NsMuclucNgansachModel.MucLucSkt)))
            {
                if (SelectedParent.BHangCha || !SelectedParent.IsModified)
                {
                    return;
                }
                GenericControlCustomViewModel<SktMucLucModel, NsSktMucLuc, SktMucLucService> dialogVM = new GenericControlCustomViewModel<SktMucLucModel, NsSktMucLuc, SktMucLucService>
                    ((SktMucLucService)_sktMucLucService, _mapper, _sessionService, _serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục mục lục số kiểm tra";
                dialogVM.Title = "Danh mục mục lục số kiểm tra";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.SKyHieu.Equals(SelectedParent.SktMucLucMap?.SSktKyHieu)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    SktMucLucModel data = obj as SktMucLucModel;
                    SelectedParent.SktMucLucMap = new NsMlsktMlns
                    {
                        SSktKyHieu = data.SKyHieu,
                        SNsXauNoiMa = SelectedParent.XauNoiMa,
                        ITrangThai = 1,
                        DNgayTao = DateTime.Now,
                        SNguoiTao = _sessionService.Current.Principal,
                        INamLamViec = data.INamLamViec
                    };
                    SelectedParent.MucLucSkt = data.SKyHieu;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }

        private void GetCauHinhCanCu()
        {
            var predicate = PredicateBuilder.True<NsCauHinhCanCu>();
            _dicCauHinhCanCu = new Dictionary<int, NsCauHinhCanCu>();
            _dicCauHinhCanCuDamBao = new Dictionary<string, NsCauHinhCanCu>();
            predicate = predicate.And(item => item.SModule == TypeModuleCanCu.PLAN_BEGIN_YEAR);
            predicate = predicate.And(item => item.INamLamViec == _sessionService.Current.YearOfWork);
            var listCanCu = _iCauHinhCanCuService.FindByCondition(predicate).OrderBy(n => n.INamCanCu);
            if (listCanCu == null || listCanCu.Count() == 0) return;
            int count = 0;
            foreach (var item in listCanCu)
            {
                _dicCauHinhCanCu.Add(count + 1, item);
                _dicCauHinhCanCuDamBao.Add(item.IIDMaChucNang, item);
                switch (item.IIDMaChucNang)
                {
                    case TypeCanCu.ESTIMATE:
                        STuChi2 = item.STenCot;
                        SHeader2 = item.STenCot;
                        break;
                    case TypeCanCu.SETTLEMENT:
                        STuChi1 = item.STenCot;
                        SHeader1 = item.STenCot;

                        break;
                    case TypeCanCu.ALLOCATION:
                        STuChi3 = item.STenCot;
                        SHeader3 = item.STenCot;

                        break;
                    case TypeCanCu.DEMAND:
                        STuChi4 = item.STenCot;
                        SHeader4 = item.STenCot;

                        break;
                    case TypeCanCu.CHECK_NUMBER:
                        STuChi5 = item.STenCot;
                        SHeader5 = item.STenCot;

                        break;
                }
                count++;
            }
        }

        private void GetDonVi()
        {
            var lstDonVi = _donViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            _dicDonVi = new Dictionary<string, DonVi>();
            if (lstDonVi != null && lstDonVi.Any(n => n.Loai == "2"))
            {
                lstDonVi = lstDonVi.Where(n => n.Loai == "2");
                DonViNewItems = new ObservableCollection<ComboboxItem>(lstDonVi.Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = n.TenDonVi }));
                foreach (var item in lstDonVi)
                {
                    if (!_dicDonVi.ContainsKey(item.IIDMaDonVi))
                        _dicDonVi.Add(item.IIDMaDonVi, item);
                }
            }
            OnPropertyChanged(nameof(DonViNewItems));
        }
    }
}
