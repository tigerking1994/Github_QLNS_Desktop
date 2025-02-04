using AutoMapper;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.RevenueExpenditureType;
using MessageBox = System.Windows.Forms.MessageBox;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Import
{
    public class ApprovedEstimationImportViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private INsDtChungTuService _chungTuService;
        private INsDtChungTuChiTietService _chungTuChiTietService;
        private INsMucLucNganSachService _nsMucLucNganSachService;
        private IConfiguration _configuration;
        private IImpHistoryService _impHistoryService;
        private IImpDuToanService _impDuToanService;
        private readonly ITnDtChungTuService _tnDtChungTuService;
        private readonly ITnDtChungTuChiTietService _tnDtChungTuChiTietService;

        private readonly FtpStorageService _ftpStorageService;
        private string _importFolder;
        private string _fileName;
        private ImpHistory _impHistory;
        private List<ImportErrorItem> _listErrChungTuChiTiet;
        private List<ImportErrorItem> _listErrChungTu;
        private List<NsMuclucNgansachModel> _mergeItems;
        private List<NsMucLucNganSach> _mucLucNganSachs;
        private readonly IHTTPUploadFileService _hTTPUploadFileService;
        private SessionInfo _sessionInfo;
        public bool IsSendHTTP;

        public Action<TnDtChungTuModel> SavedAction;
        public override string Name => "Thu nộp ngân sách";
        public override Type ContentType => typeof(ApprovedEstimationImport);
        public override string Description => "Nhận phân bổ dự toán";
        public override PackIconKind IconKind => PackIconKind.Dollar;

        public IEnumerable<NsMucLucNganSach> ListNsMucLucNganSach = new List<NsMucLucNganSach>();

        private ImportResult<DivisionDetailImportModel> _divisionDetailResult;
        public ImportResult<DivisionDetailImportModel> DivisionDetailResult
        {
            get => _divisionDetailResult;
            set
            {
                SetProperty(ref _divisionDetailResult, value);
                OnPropertyChanged(nameof(_divisionDetailResult));
            }
        }

        private DivisionImportModel _selectedDivision;
        public DivisionImportModel SelectedDivision
        {
            get => _selectedDivision;
            set => SetProperty(ref _selectedDivision, value);
        }

        private ObservableCollection<DivisionDetailImportModel> _divisionDetails = new ObservableCollection<DivisionDetailImportModel>();
        public ObservableCollection<DivisionDetailImportModel> DivisionDetails
        {
            get => _divisionDetails;
            set => SetProperty(ref _divisionDetails, value);
        }

        private DivisionDetailImportModel _selectedDivisionDetail;
        public DivisionDetailImportModel SelectedDivisionDetail
        {
            get => _selectedDivisionDetail;
            set => SetProperty(ref _selectedDivisionDetail, value);
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        private bool _isNSSD;
        public bool IsNSSD
        {
            get => _isNSSD;
            set => SetProperty(ref _isNSSD, value);
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
                if (_cbxVoucherTypeSelected != null)
                {
                    CheckExitsDuToanDauNam();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxBudgetType;
        public ObservableCollection<ComboboxItem> CbxBudgetType
        {
            get => _cbxBudgetType;
            set => SetProperty(ref _cbxBudgetType, value);
        }

        private ComboboxItem _cbxBudgetTypeSelected;
        public ComboboxItem CbxBudgetTypeSelected
        {
            get => _cbxBudgetTypeSelected;
            set
            {
                SetProperty(ref _cbxBudgetTypeSelected, value);
                if (_cbxBudgetTypeSelected != null)
                {
                    CheckExitsDuToanDauNam();
                }
            }
        }

        public bool _isSaveData;
        public bool IsSaveData
        {
            get => DivisionDetails.Count > 0 && !DivisionDetails.Any(x => !x.ImportStatus) && !IsValidateExists;
            set => SetProperty(ref _isSaveData, value);
        }

        public bool _isSelectedFile;
        public bool IsSelectedFile
        {
            get => !string.IsNullOrEmpty(_filePath);
            set => SetProperty(ref _isSelectedFile, value);
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

        private string _soQuyetDinh;
        public string SoQuyetDinh
        {
            get => _soQuyetDinh;
            set => SetProperty(ref _soQuyetDinh, value);
        }

        private DateTime? _ngayQuyetDinh;
        public DateTime? NgayQuyetDinh
        {
            get => _ngayQuyetDinh;
            set => SetProperty(ref _ngayQuyetDinh, value);
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

        public bool _isValidateExists;
        public bool IsValidateExists
        {
            get => _isValidateExists;
            set
            {
                SetProperty(ref _isValidateExists, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private ObservableCollection<FileFilterQuery> _lstFile;
        public ObservableCollection<FileFilterQuery> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }

        private int SoChungTuIndex { get; set; }

        public bool IsEnableSaveMLNS => _mergeItems.Count > 0;
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
        public RelayCommand GetFileFtpCommandHTTP { get; }
        public RelayCommand GetFileFtpCommandFTP { get; }
        public RelayCommand DownloadFileFtpServer { get; }

        public ApprovedEstimationImportViewModel(ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            INsDtChungTuService chungTuService,
            INsDtChungTuChiTietService chungTuChiTietService,
            INsMucLucNganSachService nsMucLucNganSachService,
            IConfiguration configuration,
            IImpHistoryService impHistoryService,
            IImpDuToanService impDuToanService,
            FtpStorageService ftpStorageService,
            IHTTPUploadFileService hTTPUploadFileService,
            ITnDtChungTuService tnDtChungTuService,
            ITnDtChungTuChiTietService tnDtChungTuChiTietService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _configuration = configuration;
            _impHistoryService = impHistoryService;
            _impDuToanService = impDuToanService;
            _ftpStorageService = ftpStorageService;
            _hTTPUploadFileService = hTTPUploadFileService;
            _tnDtChungTuChiTietService = tnDtChungTuChiTietService;
            _tnDtChungTuService = tnDtChungTuService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());

            ShowErrorCommand = new RelayCommand(ShowError);
            AddMLNSCommand = new RelayCommand(obj => OnAddMLNS());
            MergeCommand = new RelayCommand(obj => OnMerge());
            UnmergeCommand = new RelayCommand(obj => OnUnMerge());
            SaveMLNSCommand = new RelayCommand(obj => OnSaveMLNS());
            CloseCommand = new RelayCommand(OnCloseWindow);
            GetFileFtpCommandHTTP = new RelayCommand(async obj => await OnGetFileFtpCommand(true));
            GetFileFtpCommandFTP = new RelayCommand(async obj => await OnGetFileFtpCommand(false));
            DownloadFileFtpServer = new RelayCommand(async obj => await OnDownloadFileFtpServer(obj));
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _importFolder = _configuration.GetSection("ImportFolder").Value;
            Directory.CreateDirectory(_importFolder);
            LoadVoucherType();
            LoadBudgetType();
            OnResetData();
        }

        private void LoadDefault()
        {
            var predicate = this.CreatePredicate2();
            int soChungTuIndex = _tnDtChungTuService.FindNextSoChungTuIndex(predicate);
            SoChungTu = "DT-" + soChungTuIndex.ToString("D3");
            SoChungTuIndex = soChungTuIndex;
            NgayChungTu = DateTime.Now;
            NgayQuyetDinh = DateTime.Now;
            SoQuyetDinh = string.Empty;
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

        private void LoadBudgetType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.BudgetTypeName[BudgetType.YEAR], ValueItem = ((int)BudgetType.YEAR).ToString()},
                new ComboboxItem {DisplayItem = VoucherType.BudgetTypeName[BudgetType.LAST_YEAR], ValueItem = ((int)BudgetType.LAST_YEAR).ToString()},
                new ComboboxItem {DisplayItem = VoucherType.BudgetTypeName[BudgetType.ADDITIONAL], ValueItem = ((int)BudgetType.ADDITIONAL).ToString()},
                new ComboboxItem {DisplayItem = VoucherType.BudgetTypeName[BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR], ValueItem = ((int)BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR).ToString()}
            };

            CbxBudgetType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            CbxBudgetTypeSelected = CbxBudgetType.ElementAt(1);
        }

        private void CheckExitsDuToanDauNam()
        {
            IsValidateExists = false;
            if (_cbxBudgetTypeSelected == null) return;
            var budgetType = (BudgetType)(int.Parse(_cbxBudgetTypeSelected.ValueItem));
            if (!BudgetType.YEAR.Equals(budgetType)) return;

            int loaiChungTu = int.Parse(VoucherType.NSSD_Key);
            if (_cbxVoucherTypeSelected != null && VoucherType.NSBD_Key.Equals(_cbxVoucherTypeSelected.ValueItem))
            {
                loaiChungTu = int.Parse(VoucherType.NSBD_Key);
            }

            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.ReceiveEstimate);
            predicate = predicate.And(x => x.ILoaiChungTu == loaiChungTu);
            predicate = predicate.And(x => x.ILoaiDuToan == (int)BudgetType.YEAR);

            IEnumerable<NsDtChungTu> result = _chungTuService.FindByCondition(predicate).ToList();
            if (result.Any())
            {
                IsValidateExists = true;
                var listSoChungTu = string.Join(",", result.Select(x => x.SSoChungTu));
                string message = $"Đã tồn tại dự án đầu năm: {listSoChungTu}";
                MessageBox.Show(message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnResetData()
        {
            _mergeItems = new List<NsMuclucNgansachModel>();
            _filePath = string.Empty;

            _divisionDetails = new ObservableCollection<DivisionDetailImportModel>();
            var predicateMlns = CreatePredicateMlns();
            ListNsMucLucNganSach = _nsMucLucNganSachService.FindByCondition(predicateMlns);
            _mucLucNganSachs = ListNsMucLucNganSach.ToList();
            _importedMlns = new ObservableCollection<NsMuclucNgansachModel>();
            _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(_mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(_mucLucNganSachs));
            _impHistory = new ImpHistory();
            _listErrChungTuChiTiet = new List<ImportErrorItem>();
            _listErrChungTu = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;
            LstFile = new ObservableCollection<FileFilterQuery>();
            LoadDefault();
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(CbxVoucherTypeSelected));
            OnPropertyChanged(nameof(CbxBudgetTypeSelected));
            OnPropertyChanged(nameof(DivisionDetails));
            OnPropertyChanged(nameof(IsSaveData));

            OnPropertyChanged(nameof(TabIndex));
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(LstFile));
        }

        private void OnUploadFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Chọn file excel",
                RestoreDirectory = true,
                DefaultExt = StringUtils.EXCEL_EXTENSION
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            FilePath = openFileDialog.FileName;
            _fileName = openFileDialog.SafeFileName;
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
        }

        private void OnProcessFile(bool isLoadFromServer = false)
        {
            _listErrChungTuChiTiet = new List<ImportErrorItem>();
            _listErrChungTu = new List<ImportErrorItem>();

            var messages = new List<string>();

            if (!isLoadFromServer && string.IsNullOrEmpty(FilePath))
            {
                messages.Add(Resources.ErrorFileEmpty);
            }
            if (_cbxVoucherType == null)
            {
                messages.Add(Resources.ErrorMonthEmpty);
            }
            if (_cbxBudgetTypeSelected == null)
            {
                messages.Add(Resources.ErrorAgencyEmpty);
            }

            var message = string.Join(Environment.NewLine, messages);
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.MessageBox.Show(message, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            IsNSSD = VoucherType.NSSD_Key.Equals(_cbxVoucherTypeSelected.ValueItem);

            try
            {
                //xử lý chứng từ chi tiết
                var sheetDetailAttribute = (SheetAttribute)Attribute.GetCustomAttribute(typeof(DivisionDetailImportModel), typeof(SheetAttribute));
                var importDivisionDetailResult = isLoadFromServer ? DivisionDetailResult : _importService.ProcessDataUnique<DivisionDetailImportModel>(FilePath);

                // get list MucLucNganSach permission handle
                var yearOfWork = _sessionService.Current.YearOfWork;
                //ListNsMucLucNganSach = _nsMucLucNganSachService.FindByXauNoiMaAndNamLamViec(importDivisionDetailResult.Data.Select(x => x.XauNoiMa), yearOfWork, int.Parse(_cbxVoucherTypeSelected.ValueItem)).ToList();
                var listXauNoiMaHangCha = ListNsMucLucNganSach.Where(x => x.BHangCha).Select(x => x.XauNoiMa).ToHashSet();
                var nsMucLucNganSachGroupByXauNoiMa = ListNsMucLucNganSach.GroupBy(x => x.XauNoiMa)
                    .ToDictionary(x => x.Key, x => x.First());

                var dictErr = importDivisionDetailResult.ImportErrors.ToLookup(x => x.Row)
                    .ToDictionary(x => x.Key, x => x.ToList());
                var numberRecordExclude = 0;


                var divisionDetailsImport = importDivisionDetailResult.Data.Select((x, i) =>
                {
                    var mlns = ListNsMucLucNganSach.FirstOrDefault(z => z.XauNoiMa.Equals(x.XauNoiMa));
                    var isHangCha = listXauNoiMaHangCha.Contains(x.XauNoiMa);
                    if (!isHangCha)
                    {
                        if (!string.IsNullOrEmpty(nsMucLucNganSachGroupByXauNoiMa.GetValueOrDefault(x.XauNoiMa, new NsMucLucNganSach()).MoTa))
                        {
                            x.MoTa = nsMucLucNganSachGroupByXauNoiMa.GetValueOrDefault(x.XauNoiMa, new NsMucLucNganSach()).MoTa;
                        }
                        //x.MoTa = nsMucLucNganSachGroupByXauNoiMa.GetValueOrDefault(x.XauNoiMa, new NsMucLucNganSach()).MoTa;
                        x.IsHangCha = false;
                        var listErrRepairIndex = dictErr.GetValueOrDefault(i, new List<ImportErrorItem>()).ToList();
                        listErrRepairIndex.ForEach(err => err.Row = i - numberRecordExclude);
                        _listErrChungTuChiTiet.AddRange(listErrRepairIndex);
                    }
                    else
                    {
                        x.IsHangCha = true;
                        numberRecordExclude++;
                    }
                    return x;
                }).ToList();

                _divisionDetails = new ObservableCollection<DivisionDetailImportModel>(divisionDetailsImport);
                OnPropertyChanged(nameof(DivisionDetails));

                if (!_divisionDetails.Any())
                {
                    message = string.Format(Resources.MsgSheetErrorDataEmpty, sheetDetailAttribute.SheetName);
                    System.Windows.MessageBox.Show(message, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    OnPropertyChanged(nameof(IsSaveData));
                    return;
                }

                foreach (var item in _divisionDetails)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName != nameof(DivisionDetailImportModel.ImportStatus)
                            && args.PropertyName != nameof(DivisionDetailImportModel.XauNoiMa)
                            && args.PropertyName != nameof(DivisionDetailImportModel.IsErrorMLNS))
                        {
                            var entityDetail = (DivisionDetailImportModel)sender;
                            var rowIndex = _divisionDetails.IndexOf(entityDetail);
                            var listError = _importService.ValidateItem(entityDetail, rowIndex);
                            if (listError.Count > 0)
                            {
                                var messageOfRow = listError.Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToList();
                                System.Windows.MessageBox.Show(string.Join(Environment.NewLine, messageOfRow), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                                _listErrChungTuChiTiet.AddRange(listError);
                                entityDetail.ImportStatus = false;
                                if (listError.Any(x => x.IsErrorMLNS))
                                {
                                    entityDetail.IsErrorMLNS = true;
                                }
                            }
                            else
                            {
                                entityDetail.ImportStatus = true;
                                entityDetail.IsErrorMLNS = false;
                                _listErrChungTuChiTiet = _listErrChungTuChiTiet.Where(x => x.Row != rowIndex).ToList();
                            }
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                }

                for (var i = 0; i < _divisionDetails.Count; i++)
                {
                    var item = _divisionDetails[i];
                    if (item.ImportStatus)
                    {
                        var errorsXauNoiMa = GetErrorXauNoiMa(sheetDetailAttribute.SheetName, i, item.XauNoiMa, item.MoTa);
                        if (errorsXauNoiMa.Any())
                        {
                            item.ImportStatus = false;
                            _listErrChungTuChiTiet.AddRange(errorsXauNoiMa);
                        }
                    }
                }

                OnPropertyChanged(nameof(DivisionDetails));
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception e)
            {
                if (e is Utility.Exceptions.WrongReportException)
                {
                    System.Windows.MessageBox.Show(Resources.WrongReportFormat, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    System.Windows.MessageBox.Show(Resources.ErrorImport, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private List<ImportErrorItem> GetError(string sheet, int row, int columnIndex, string value, string valueCompare, string message = "Dữ liệu không đúng.")
        {
            var errors = new List<ImportErrorItem>();
            if (string.IsNullOrWhiteSpace(valueCompare) || !value.Equals(valueCompare.Trim()))
            {
                errors.Add(new ImportErrorItem
                {
                    ColumnName = _importService.GetColumnAttribute<DivisionImportModel>(columnIndex).ColumnName,
                    Row = row,
                    Error = message,
                    SheetName = sheet
                });
            }
            return errors;
        }

        private List<ImportErrorItem> GetErrorXauNoiMa(string sheetName, int row, string xauNoiMa, string message)
        {
            var errors = new List<ImportErrorItem>();
            if (!ListNsMucLucNganSach.Any(x => x.XauNoiMa.Equals(xauNoiMa)))
            {
                errors.Add(new ImportErrorItem
                {
                    Row = row,
                    Error = $"Không có quyền thao tác với loại ngân sách: {message}.",
                    SheetName = sheetName
                });
            }
            return errors;
        }

        private void SaveFileToImportFolder()
        {
            var destFile = Path.Combine(_importFolder, $"{DateUtils.GetFormatDateReport()}_{StringUtils.RemoveSpecialCharacters(StringUtils.RemoveAccents(_fileName))}");
            File.Copy(FilePath, destFile);
            _impHistory = new ImpHistory
            {
                FileName = _fileName,
                FilePath = _importFolder,
                ServiceCode = "Dự toán",
                UserCreator = _sessionService.Current.Principal,
                DateCreated = DateTime.Now
            };
            _impHistoryService.Add(_impHistory);

            var duToans = _mapper.Map<List<ImpDuToan>>(_divisionDetails.Where(x => x.ImportStatus));
            foreach (var item in duToans)
            {
                item.ImportId = _impHistory.Id;
                item.Loai = "101";
            }
            _impDuToanService.AddRange(duToans);
        }

        private void OnSaveData()
        {
            string errorValidate = GetMessageValidate();
            if (!string.IsNullOrEmpty(errorValidate))
            {
                System.Windows.MessageBox.Show(errorValidate, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var chungTu = new TnDtChungTu
            {
                SoChungTu = SoChungTu,
                NgayChungTu = NgayChungTu,
                SoQuyetDinh = SoQuyetDinh,
                NgayQuyetDinh = NgayQuyetDinh,
                IdDonVi = _sessionService.Current.IdDonVi,
                NamLamViec = _sessionService.Current.YearOfWork,
                NamNganSach = _sessionService.Current.YearOfBudget,
                SoChungTuIndex = SoChungTuIndex
            };
            var divisionDetailsImport = _divisionDetails.Where(x => x.ImportStatus && !x.IsHangCha);

            var chungTuChiTiets = _mapper.Map<List<TnDtChungTuChiTiet>>(divisionDetailsImport);
            var lns = string.Join(StringUtils.COMMA, _divisionDetails.Where(x => x.ImportStatus).Select(x => x.LNS).ToHashSet());
            chungTu.Lns = lns;
            chungTu.NguonNganSach = _sessionService.Current.Budget;
            chungTu.IdDonViTao = _sessionService.Current.IdDonVi;
            chungTu.ILoai = RevenueAndExpenditureType.ApprovedEstimation;
            chungTu.UserCreator = _sessionService.Current.Principal;
            chungTu.DateCreated = DateTime.Now;
            chungTu.TuChiSum = chungTuChiTiets.Sum(x => x.TuChi);
            _tnDtChungTuService.Add(chungTu);

            var dictNsNganSachByXauNoiMa = ListNsMucLucNganSach.GroupBy(x => x.XauNoiMa)
                .ToDictionary(x => x.Key, x => x.First());
            var listChungTuChiTietResult = chungTuChiTiets.Where(x => dictNsNganSachByXauNoiMa.ContainsKey(x.XauNoiMa)).Select(chungTuChiTiet =>
            {
                var targetItem = ObjectCopier.Clone(chungTuChiTiet);

                targetItem.Id = Guid.Empty;
                targetItem.IdChungTu = chungTu.Id;
                targetItem.BHangCha = false;
                var nsMucLucNganSach = dictNsNganSachByXauNoiMa[chungTuChiTiet.XauNoiMa];
                targetItem.MlnsId = nsMucLucNganSach.MlnsId;
                targetItem.MlnsIdParent = nsMucLucNganSach.MlnsIdParent;
                targetItem.L = nsMucLucNganSach.L;
                targetItem.K = nsMucLucNganSach.K;
                targetItem.Tng = nsMucLucNganSach.Tng;

                targetItem.NamLamViec = _sessionService.Current.YearOfWork;
                targetItem.NamNganSach = _sessionService.Current.YearOfBudget;
                targetItem.NguonNganSach = _sessionService.Current.Budget;
                targetItem.ITrangThai = NSEntityStatus.ACTIVED;
                targetItem.IPhanCap = RevenueAndExpenditureType.ApprovedEstimation;
                targetItem.UserCreator = _sessionService.Current.Principal;
                targetItem.DateCreated = DateTime.Now;

                return targetItem;
            });
            _tnDtChungTuChiTietService.AddRange(listChungTuChiTietResult);

            // save file to import folder
            if (!String.IsNullOrEmpty(FilePath))
            {
                SaveFileToImportFolder();
            }

            // show message
            System.Windows.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);

            // mở màn hình chứng từ chi tiết
            var entityModel = _mapper.Map<TnDtChungTuModel>(chungTu);
            SavedAction?.Invoke(entityModel);
        }

        private int GetNextSoChungTuIndex()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.ReceiveEstimate);

            var soChungTuIndex = _chungTuService.FindNextSoChungTuIndex(predicate);
            return soChungTuIndex;
        }

        private void ShowError(object param)
        {
            var importTabIndex = (ImportTabIndex)((int)param);
            var errors = new HashSet<string>();
            int rowIndex;
            switch (importTabIndex)
            {
                case ImportTabIndex.Data:
                    rowIndex = _divisionDetails.IndexOf(SelectedDivisionDetail);
                    errors = _listErrChungTuChiTiet.Where(x => x.Row == rowIndex).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
                    break;
                case ImportTabIndex.MLNS:
                    errors = new HashSet<string>();
                    break;
            }
            System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public int OnAddMLNS()
        {
            int indexResult = -1;
            TabIndex = ImportTabIndex.MLNS;
            var importItem = new NsMuclucNgansachModel();
            if (ImportedMlns.Contains(importItem))
            {
                return indexResult;
            }

            if (!_importedMlns.Any(x => x.XauNoiMa.Contains(SelectedDivisionDetail.XauNoiMa))
                && !_existedMlns.Any(x => x.XauNoiMa.Contains(SelectedDivisionDetail.XauNoiMa)))
            {
                _importedMlns.Add(new NsMuclucNgansachModel
                {
                    Lns = SelectedDivisionDetail.LNS,
                    L = SelectedDivisionDetail.L,
                    K = SelectedDivisionDetail.K,
                    M = SelectedDivisionDetail.M,
                    TM = SelectedDivisionDetail.TM,
                    TTM = SelectedDivisionDetail.TTM,
                    NG = SelectedDivisionDetail.NG,
                    TNG = SelectedDivisionDetail.TNG,
                    XauNoiMa = SelectedDivisionDetail.XauNoiMa,
                    MoTa = SelectedDivisionDetail.MoTa,
                    NamLamViec = _sessionService.Current.YearOfWork,
                    BHangChaDuToan = false,
                    IsModified = true
                });
            }
            foreach (var model in _importedMlns.ToList())
            {
                var parent = FindParent(model, _existedMlns);
                if (parent != null)
                {
                    var index = _existedMlns.IndexOf(parent);
                    _existedMlns.Insert(index + 1, model);
                    _importedMlns.Remove(model);
                    _mergeItems.Add(model);
                    OnPropertyChanged(nameof(IsEnableSaveMLNS));
                }
            }

            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnSelectionChanged();

            if (SelectedDivisionDetail != null)
            {
                var item = _existedMlns.Where(n => n.XauNoiMa == SelectedDivisionDetail.XauNoiMa).FirstOrDefault();
                if (item != null)
                    indexResult = _existedMlns.IndexOf(item);
            }
            return indexResult;
        }

        public NsMuclucNgansachModel FindParent(NsMuclucNgansachModel model, IEnumerable<NsMuclucNgansachModel> ExistedMlns)
        {
            var ancestors = _existedMlns.Where(i => !Guid.Empty.Equals(i.Id) && !model.XauNoiMa.Equals(i.XauNoiMa) &&
                model.XauNoiMa.StartsWith(i.XauNoiMa + "-") && model.NamLamViec == i.NamLamViec).OrderByDescending(i => i.XauNoiMa.Length);
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
            {
                return;
            }
            var index = _existedMlns.ToList().FindIndex(x => x.IsSelected);
            _mergeItems = _importedMlns.Where(i => i.IsSelected && i.IsModified).ToList();
            foreach (var item in _mergeItems)
            {
                item.MlnsIdParent = SelectedParent.MlnsId;
                item.BHangCha = false;
                item.ITrangThai = 1;
                item.SNguoiTao = _sessionService.Current.Principal;
                item.DNgayTao = DateTime.Now;
            }

            var nsMuclucNgansachModels = _existedMlns.ToList();
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
            var unMergeItems = _existedMlns.Where(i => i.IsSelected && i.IsModified).ToList();
            foreach (var item in unMergeItems)
            {
                _mergeItems.Remove(item);
            }
            var nsMuclucNgansachModels = ImportedMlns.ToList();
            nsMuclucNgansachModels.AddRange(unMergeItems);
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
            var result = System.Windows.MessageBox.Show(Resources.ConfirmAddMLNS, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _nsMucLucNganSachService.AddRange(_mapper.Map<List<NsMucLucNganSach>>(_mergeItems));
                    _existedMlns.Where(x => x.IsModified).ForAll(x =>
                    {
                        x.IsModified = false;
                        x.IsSelected = false;
                    });
                    OnPropertyChanged(nameof(ExistedMlns));

                    System.Windows.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    var listError = _importService.ValidateItem(SelectedDivisionDetail, _divisionDetails.IndexOf(SelectedDivisionDetail));
                    if (!listError.Any())
                    {
                        SelectedDivisionDetail.ImportStatus = true;
                        SelectedDivisionDetail.IsErrorMLNS = false;
                        TabIndex = ImportTabIndex.Data;
                        OnPropertyChanged(nameof(IsSaveData));
                    }
                }
                catch
                {
                    System.Windows.MessageBox.Show(Resources.MsgSaveError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void OnCloseWindow(object obj)
        {
            var window = obj as Window;
            window.Close();
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (string.IsNullOrEmpty(SoQuyetDinh))
            {
                messages.Add(Resources.AlertSoQuyetDinhEmpty);
            }

            if (!NgayQuyetDinh.HasValue)
            {
                messages.Add(Resources.AlertNgayQuyetDinhEmpty);
            }

            if (_cbxBudgetTypeSelected == null)
            {
                messages.Add(Resources.AlertLoaiDuToanEmpty);
            }

            if (!messages.Any())
            {
                messages.AddRange(ValidateSoQuyetDinh());
                //messages.AddRange(ValidateNgayChungTu());
            }
            return string.Join(Environment.NewLine, messages);
        }

        private List<string> ValidateSoQuyetDinh()
        {
            List<string> messages = new List<string>();
            var predicate = CreatePredicate();
            predicate = predicate.And(x => x.SSoQuyetDinh == SoQuyetDinh);
            var listChungTu = _chungTuService.FindByCondition(predicate).ToList();
            if (listChungTu.Count > 0)
            {
                //if (listChungTu.Any(x => x.ILoaiChungTu == int.Parse(_cbxVoucherTypeSelected.ValueItem)))
                //    messages.Add($"Hệ thống đã tồn tại số quyết định {SoQuyetDinh} thuộc loại {_cbxVoucherTypeSelected.DisplayItem}");
                //else if (listChungTu.Any(x => x.ILoaiChungTu != int.Parse(_cbxVoucherTypeSelected.ValueItem) && x.DNgayQuyetDinh.Value.Date != NgayQuyetDinh.Value.Date))
                //    messages.Add($"Hệ thống đã tồn tại số quyết định {SoQuyetDinh} ngày {listChungTu.First().DNgayQuyetDinh.Value.ToString("dd/MM/yyyy")}");
                if (listChungTu.Any(x => x.DNgayQuyetDinh.Value.Date != NgayQuyetDinh.Value.Date))
                {
                    messages.Add(string.Format(Resources.VoucherValidateSoQuyetDinhNgayQuyetDinh, SoQuyetDinh, listChungTu.First().DNgayQuyetDinh.Value.ToString("dd/MM/yyyy")));
                }

            }
            return messages;
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.ReceiveEstimate);
            return predicate;
        }

        private Expression<Func<TnDtChungTu, bool>> CreatePredicate2()
        {
            var predicate = PredicateBuilder.True<TnDtChungTu>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.NamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == RevenueAndExpenditureType.ApprovedEstimation);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            return predicate;
        }
        
        private Expression<Func<NsMucLucNganSach, bool>> CreatePredicateMlns()
        {
            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.Lns.StartsWith("8"));
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            return predicate;
        }

        private async Task OnGetFileFtpCommand(bool isSendHTTP)
        {
            IsSendHTTP = isSendHTTP;
            //var btmTenDonVi = _sessionService.Current.IdDonVi + "-" + StringUtils.UCS2Convert(_sessionService.Current.TenDonVi).Replace("---", "-");
            //var loaiChungTu = StringUtils.UCS2Convert(CbxVoucherTypeSelected.DisplayItem);
            //var strUrl = string.Format("{0}/{1}/{2}", btmTenDonVi, ConstantUrlPathPhanHe.UrlPbdtWinformSend, loaiChungTu);
            //var lstData = _ftpStorageService.GetFileServerFtp(strUrl);
            //if (lstData == null || lstData.Count == 0)
            //{
            //    StringBuilder messageBuilder = new StringBuilder();
            //    messageBuilder.AppendFormat("Không tìm thấy dữ liệu");
            //    System.Windows.MessageBox.Show(messageBuilder.ToString());
            //    return;
            //}
            //LstFile = new ObservableCollection<FileFtpModel>(lstData);
            IsLoading = true;
            var fileFilter = new FileFilterModel();
            fileFilter.Module = "BUDGET";
            fileFilter.SubModule = CbxVoucherTypeSelected.ValueItem == "1" ? NSFunctionCode.BUDGET_ESTIMATE_DIVISION : NSFunctionCode.BUDGET_ESTIMATE_DIVISION_GUARANTEE;
            fileFilter.Quarter = "";
            fileFilter.YearOfWork = _sessionInfo.YearOfWork;
            fileFilter.YearOfBudget = _sessionInfo.YearOfBudget;
            fileFilter.SourceOfBudget = _sessionInfo.Budget;
            fileFilter.GetFileType = (int)GetFileType.GetParent;
            var lst = await _hTTPUploadFileService.GetFile(isSendHTTP, fileFilter);
            if (lst == null || lst.Count == 0)
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Không tìm thấy dữ liệu");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                IsLoading = false;
                return;
            }
            LstFile = new ObservableCollection<FileFilterQuery>(lst);
            IsLoading = false;
        }

        private async Task OnDownloadFileFtpServer(object obj)
        {
            //string urlUrIDownLoad = "";
            //string fileName = "";
            //if (LstFile == null || LstFile.Count == 0 || !LstFile.Any(e => e.BIsCheck))
            //{
            //    StringBuilder messageBuilder = new StringBuilder();
            //    messageBuilder.AppendFormat("Vui lòng lấy dữ liệu");
            //    System.Windows.MessageBox.Show(messageBuilder.ToString());
            //    return;
            //}
            //else if (LstFile.Where(n => n.BIsCheck).Count() > 1)
            //{
            //    System.Windows.MessageBox.Show("Chọn 1 file dữ liệu");
            //    return;
            //}

            //var item = LstFile.FirstOrDefault(x => x.BIsCheck);
            //urlUrIDownLoad = item.SUrl;
            //fileName = item.SNameFile;
            //FilePath = _ftpStorageService.DowLoadFileFtpGiveLocalStr(urlUrIDownLoad, fileName);

            //OnProcessFile();

            IsLoading = true;
            if (obj is FileFilterQuery file)
            {
                var id = file.FileId;
                var fileStream = await _hTTPUploadFileService.DownloadDecryptFile(IsSendHTTP, id, file.FileTokenKey);
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    DivisionDetailResult = _importService.ProcessDataUnique<DivisionDetailImportModel>(fileStream, file.FileTokenKey);
                    OnProcessFile(true);
                }, (s, e) =>
                {
                    IsLoading = false;
                });
            }
        }
    }
}
