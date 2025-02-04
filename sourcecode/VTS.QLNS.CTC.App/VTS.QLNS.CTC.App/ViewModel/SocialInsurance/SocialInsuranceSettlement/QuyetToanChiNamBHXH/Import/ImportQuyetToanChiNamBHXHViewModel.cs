using AutoMapper;
using FlexCel.XlsAdapter;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.Import
{
    public class ImportQuyetToanChiNamBHXHViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private IQtcnBHXHService _qtcnBHXHService;
        private IQtcnBHXHChiTietService _qtcnBHXHChiTietService;
        private IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private IBhDmCoSoYTeService _bhDmCoSoYTeService;
        private INsDonViService _nSDonViService;
        private ILog _logger;
        private IConfiguration _configuration;
        private SessionInfo _sessionInfo;
        private string _importFolder;
        private string _fileName;
        private ImpHistory _impHistory;
        private List<ImportErrorItem> _listErrChungTuChiTiet;
        private List<ImportErrorItem> _listErrChungTu;
        private List<BhDmMucLucNganSachModel> _mergeItems;
        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        private int _lastRowToRead = 0;
        private bool _isUploadFile;
        public Action<BhQtcnBHXHModel> SavedAction;

        private BhQtcnBHXHModel _dataBhCptuBHYT;
        public BhQtcnBHXHModel DataBhCptuBHYT
        {
            get => _dataBhCptuBHYT;
            set => SetProperty(ref _dataBhCptuBHYT, value);
        }

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        public override string Name => "Quyết toán";
        public override Type ContentType => typeof(ImportQuyetToanChiNamBHXH);
        public override string Description => "Import dữ liệu quyết toán chi năm BHXH";
        public override PackIconKind IconKind => PackIconKind.Dollar;

        public IEnumerable<BhDmMucLucNganSach> ListNsMucLucNganSach = new List<BhDmMucLucNganSach>();

        private ObservableCollection<QtcnDetailImportModel> _divisionDetails;
        public ObservableCollection<QtcnDetailImportModel> DivisionDetails
        {
            get => _divisionDetails;
            set => SetProperty(ref _divisionDetails, value);
        }

        private ObservableCollection<QtcnDetailImportModel> _itemsImport;
        public ObservableCollection<QtcnDetailImportModel> ItemsImport
        {
            get => _itemsImport;
            set => SetProperty(ref _itemsImport, value);
        }

        private QtcnDetailImportModel _selectedDivisionDetail;
        public QtcnDetailImportModel SelectedDivisionDetail
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

        public bool _isSaveData;
        public bool IsSaveData
        {
            get => DivisionDetails.Count > 0 && DivisionDetails.All(x => x.ImportStatus) && !IsValidateExists;
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

        private ObservableCollection<BhDmMucLucNganSachModel> _existedMlns;
        public ObservableCollection<BhDmMucLucNganSachModel> ExistedMlns
        {
            get => _existedMlns;
            set => SetProperty(ref _existedMlns, value);
        }

        private ObservableCollection<BhDmMucLucNganSachModel> _importedMlns;
        public ObservableCollection<BhDmMucLucNganSachModel> ImportedMlns
        {
            get => _importedMlns;
            set => SetProperty(ref _importedMlns, value);
        }

        private BhDmMucLucNganSachModel _selectedParent;
        public BhDmMucLucNganSachModel SelectedParent
        {
            get => _selectedParent;
            set
            {
                SetProperty(ref _selectedParent, value);
                OnPropertyChanged(nameof(IsEnabledMergeBtn));
            }
        }

        private ComboboxItem _cbxUnitsSelected;
        public ComboboxItem CbxUnitsSelected
        {
            get => _cbxUnitsSelected;
            set
            {
                SetProperty(ref _cbxUnitsSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _cbxUnits;
        public ObservableCollection<ComboboxItem> CbxUnits
        {
            get => _cbxUnits;
            set => SetProperty(ref _cbxUnits, value);
        }

        public bool IsEnabledMergeBtn
        {
            get => ImportedMlns.Any(i => i.IsSelected) && ExistedMlns.Where(i => i.IsSelected).Count() == 1;
        }

        public bool IsEnabledUnmergeCommand => ExistedMlns.Any(i => i.IsModified);

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

        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }

        public bool IsEnableSaveMLNS = false;
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand AddMLNSCommand { get; }
        public RelayCommand SaveMLNSCommand { get; }
        public RelayCommand CloseCommand { get; }

        public ImportQuyetToanChiNamBHXHViewModel(ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            IQtcnBHXHService qtcnBHXHService,
            IQtcnBHXHChiTietService qtcnBHXHChiTietService,
            INsDonViService nSDonViService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDmCoSoYTeService bhDmCoSoYTeService,
            ILog log,
            IConfiguration configuration)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _logger = log;

            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDmCoSoYTeService = bhDmCoSoYTeService;
            _configuration = configuration;
            _nSDonViService = nSDonViService;
            _qtcnBHXHService = qtcnBHXHService;
            _qtcnBHXHChiTietService = qtcnBHXHChiTietService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnCheckFileImport());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());

            ShowErrorCommand = new RelayCommand(ShowError);
            SaveMLNSCommand = new RelayCommand(obj => OnSaveMLNS());
            CloseCommand = new RelayCommand(OnCloseWindow);
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadUnits();
            _importFolder = _configuration.GetSection("ImportFolder").Value;
            Directory.CreateDirectory(_importFolder);
            DataBhCptuBHYT = new BhQtcnBHXHModel();
            int soChungTuIndex = GetNextSoChungTuIndex();
            DataBhCptuBHYT.SSoChungTu = "QTC-" + soChungTuIndex.ToString("D3");
            DataBhCptuBHYT.DNgayChungTu = DateTime.Now;
            DataBhCptuBHYT.DNgayQuyetDinh = DateTime.Now;
            OnResetData();
        }

        private void OnCheckFileImport()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                OnProcessFile();
            }, (s, e) =>
            {
                IsLoading = false;
            });
        }



        private void OnResetData()
        {
            _mergeItems = new List<BhDmMucLucNganSachModel>();
            _filePath = string.Empty;
            _itemsImport = new ObservableCollection<QtcnDetailImportModel>();
            _divisionDetails = new ObservableCollection<QtcnDetailImportModel>();

            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);
            var dataSLNS = LNSValue.LNS_9010001_9010002.Split(',');
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => dataSLNS.Contains(x.SLNS)).OrderBy(x => x.SXauNoiMa).ToList();
            _importedMlns = new ObservableCollection<BhDmMucLucNganSachModel>();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));
            _impHistory = new ImpHistory();
            _listErrChungTuChiTiet = new List<ImportErrorItem>();
            _listErrChungTu = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;
            LstFile = new ObservableCollection<FileFtpModel>();

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(DivisionDetails));
            OnPropertyChanged(nameof(IsSaveData));

            OnPropertyChanged(nameof(TabIndex));
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(LstFile));
        }
        private void LoadUnits()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            // remove 999 hard code
            predicate = predicate.And(x => !x.IIDMaDonVi.Equals("999"));
            predicate = predicate.And(x => x.Loai != LoaiDonVi.ROOT);
            var listUnits = _nSDonViService.FindByCondition(predicate).OrderBy(n => n.IIDMaDonVi).ToList();

            var lstCbUnits = listUnits.Select(x => new ComboboxItem
            {
                DisplayItem = x.IIDMaDonVi + "-" + x.TenDonVi,
                ValueItem = x.IIDMaDonVi,
                HiddenValue = x.Id.ToString()

            }).ToList();
            CbxUnits = new ObservableCollection<ComboboxItem>(lstCbUnits);
            CbxUnitsSelected = CbxUnits.ElementAt(0);
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

            _isUploadFile = true;
            FilePath = openFileDialog.FileName;
            _fileName = openFileDialog.SafeFileName;
            _listErrChungTu = new List<ImportErrorItem>();
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
        }

        private void OnProcessFile()
        {
            try
            {
                var fileExtension = Path.GetExtension(FilePath).ToLower();
                if (!(fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx")))
                {
                    System.Windows.MessageBox.Show(Resources.FileImportWrongExtensionExcel, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_listErrChungTu != null && _listErrChungTu.Any())
                {
                    _listErrChungTu = new List<ImportErrorItem>();
                    foreach (var item in DivisionDetails)
                    {
                        var rowIndex = DivisionDetails.IndexOf(item);
                        var listError = _importService.ValidateItem(item, rowIndex);

                        if (listError.Count > 0)
                        {
                            _listErrChungTu.AddRange(listError);
                            item.ImportStatus = false;
                            if (listError.Any(x => x.IsErrorMLNS))
                            {
                                item.IsError = true;
                            }
                        }
                        else
                        {
                            item.ImportStatus = true;
                            item.IsError = false;
                            _listErrChungTu = _listErrChungTu.Where(x => x.Row != rowIndex).ToList();
                        }
                    }

                    if (_listErrChungTu != null && _listErrChungTu.Any())
                    {
                        _isUploadFile = false;
                        var messageOfRow = _listErrChungTu.Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).Distinct();
                        System.Windows.MessageBox.Show(string.Join(Environment.NewLine, messageOfRow), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    OnPropertyChanged(nameof(DivisionDetails));
                    OnPropertyChanged(nameof(IsSaveData));
                }
                else
                {
                    if (DivisionDetails.Count > 0 && !_listErrChungTu.Any() && !_isUploadFile)
                    {
                        OnPropertyChanged(nameof(IsSaveData));
                        return;
                    }

                    bool isExist;
                    _isUploadFile = false;
                    _listErrChungTuChiTiet = new List<ImportErrorItem>();
                    _listErrChungTu = new List<ImportErrorItem>();
                    XlsFile xls = new XlsFile(false);
                    xls.Open(FilePath);
                    xls.ActiveSheet = 1;
                    var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                    predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                    IEnumerable<BhDmMucLucNganSach> _nsMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).OrderBy(x => x.SXauNoiMa).ToList();
                    var dataSLNS = LNSValue.LNS_9010001_9010002.Split(',');
                    _nsMucLucs = _nsMucLucs.Where(x => dataSLNS.Contains(x.SLNS)).ToList();
                    ListNsMucLucNganSach = _nsMucLucs;
                    var lstChungTu = _qtcnBHXHService.GetDanhSachQuyetToanNamBHXH(_sessionInfo.YearOfWork);
                    var messages = new List<string>();

                    if (string.IsNullOrEmpty(FilePath))
                    {
                        messages.Add(Resources.ErrorFileEmpty);
                    }

                    var message = string.Join(Environment.NewLine, messages);

                    var dataImport = _importService.ProcessData<QtcnDetailImportModel>(FilePath);
                    ItemsImport = new ObservableCollection<QtcnDetailImportModel>(dataImport.Data);

                    List<string> lstError = new List<string>();

                    if (dataImport.ImportErrors.Count > 0)
                    {
                        _listErrChungTu.AddRange(dataImport.ImportErrors);
                    }

                    if (ItemsImport == null || ItemsImport.Count <= 0)
                    {
                        System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (CbxUnitsSelected == null)
                    {
                        MessageBoxHelper.Error(Resources.MsgDonViEmpty);
                        OnResetData();
                        return;
                    }

                    if (lstChungTu.Count() > 0)
                    {
                        isExist = lstChungTu.Any(x => x.IIdMaDonVi == CbxUnitsSelected.ValueItem);
                        if (isExist)
                        {
                            System.Windows.Forms.MessageBox.Show(Resources.MsgExistDonVi, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            OnResetData();
                            return;
                        }
                    }

                    int i = 0;
                    foreach (var item in ItemsImport)
                    {
                        i++;
                        var listError = ValidateItem(item, i);

                        if (listError.Any())
                        {
                            _importErrors.AddRange(listError);
                            item.ImportStatus = false;
                            if (listError.Any(x => x.IsErrorMLNS))
                            {
                                item.IsError = true;
                            }
                        }
                    }

                    if (ItemsImport.Any(x => !x.ImportStatus))
                    {
                        System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    foreach (var item in _nsMucLucs.Where(x => x.BHangCha).ToList())
                    {
                        foreach (var itemChungTuChiTiet in ItemsImport)
                        {
                            if (item.SXauNoiMa == itemChungTuChiTiet.SXauNoiMa)
                            {
                                itemChungTuChiTiet.IsHangCha = item.BHangCha;
                            }
                        }
                    }

                    ImportedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_nsMucLucs.OrderBy(x => x.SXauNoiMa)));
                    DivisionDetails = ItemsImport;

                    foreach (var item in DivisionDetails)
                    {
                        item.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(QtcnDetailImportModel.SXauNoiMa))
                            {
                                var entityDetail = (QtcnDetailImportModel)sender;
                                var rowIndex = DivisionDetails.IndexOf(entityDetail);
                                var listError = _importService.ValidateItem(entityDetail, rowIndex);
                                if (listError.Count > 0)
                                {
                                    var messageOfRow = listError.Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToList();
                                    System.Windows.MessageBox.Show(string.Join(Environment.NewLine, messageOfRow), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                                    _listErrChungTu.AddRange(listError);
                                    entityDetail.ImportStatus = false;
                                    if (listError.Any(x => x.IsErrorMLNS))
                                    {
                                        entityDetail.IsError = true;
                                    }
                                }
                                else
                                {
                                    entityDetail.ImportStatus = true;
                                    entityDetail.IsError = false;
                                    _listErrChungTu = _listErrChungTu.Where(x => x.Row != rowIndex).ToList();
                                }
                                OnPropertyChanged(nameof(IsSaveData));
                            }
                        };
                    }
                    if (lstError.Any())
                    {
                        string sMessError = string.Join(Environment.NewLine, lstError);
                        System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    OnPropertyChanged(nameof(IsSaveData));
                }

            }
            catch (Exception e)
            {
                if (e.Message == "Sai template")
                {
                    System.Windows.MessageBox.Show(Resources.FileImportWrongTemplate, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (e is Utility.Exceptions.WrongReportException)
                {
                    System.Windows.MessageBox.Show(Resources.WrongReportFormat, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    System.Windows.MessageBox.Show(Resources.ErrorImport, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private IEnumerable<ImportErrorItem> ValidateItem(QtcnDetailImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                var lstMLNS = ListNsMucLucNganSach.Select(x => x.SXauNoiMa).ToList();
                if (!lstMLNS.Contains(item.SXauNoiMa))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Xâu nối mã",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "nội dung"),
                        Row = rowIndex
                    });
                }
                OnPropertyChanged(nameof(IsSaveData));
                return errors;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new List<ImportErrorItem>();
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
            if (!ListNsMucLucNganSach.Any(x => x.SXauNoiMa.Equals(xauNoiMa)))
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


        private void OnSaveData()
        {

            BhQtcnBHXH chungTu = new BhQtcnBHXH();
            var divisionDetailsImport = _divisionDetails.Where(x => x.ImportStatus && !x.IsHangCha);
            var divisionDetailsImportDuToan = _divisionDetails.Where(x => x.ImportStatus && x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToi);
            string message = GetMessageValidate();

            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.Forms.MessageBox.Show(message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //var chungTuChiTiets = _mapper.Map<List<BhDtctgBHXHChiTiet>>(divisionDetailsImport);
            chungTu = _mapper.Map(DataBhCptuBHYT, chungTu);
            chungTu.INamLamViec = _sessionService.Current.YearOfWork;
            chungTu.SNguoiTao = _sessionService.Current.Principal;
            chungTu.IIdDonVi = Guid.Parse(CbxUnitsSelected.HiddenValue);
            chungTu.IIdMaDonVi = CbxUnitsSelected.ValueItem;
            chungTu.SDSLNS = LNSValue.LNS_9010001_9010002;
            chungTu.DNgayTao = DateTime.Now;
            chungTu.DNgaySua = null;
            var lstDonVi = _nSDonViService.FindAll().Where(x => x.NamLamViec == _sessionService.Current.YearOfWork).ToList();
            var donvi = lstDonVi?.Where(x => x.IIDMaDonVi == CbxUnitsSelected.ValueItem).FirstOrDefault();
            if (donvi != null && donvi.Loai == LoaiDonVi.ROOT)
            {
                chungTu.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                chungTu.IIdTongHopID = Guid.NewGuid();
                chungTu.BDaTongHop = false;
            }
            else
            {
                chungTu.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTu;

            }
            _qtcnBHXHService.Add(chungTu);

            var dictNsNganSachByXauNoiMa = ListNsMucLucNganSach.GroupBy(x => x.SXauNoiMa)
                .ToDictionary(x => x.Key, x => x.First());

            List<BhQtcnBHXHChiTiet> lstChungTuChiTiet = new List<BhQtcnBHXHChiTiet>();
            foreach (var item in divisionDetailsImport)
            {
                BhQtcnBHXHChiTiet chitiet = new BhQtcnBHXHChiTiet();

                chitiet.Id = Guid.NewGuid();
                chitiet.IIdQTCNamCheDoBHXH = chungTu.Id;

                var nsMucLucNganSach = dictNsNganSachByXauNoiMa[item.SXauNoiMa];
                chitiet.IIdMucLucNganSach = nsMucLucNganSach.IIDMLNS;
                chitiet.SLoaiTroCap = nsMucLucNganSach.SMoTa;
                chitiet.INamLamViec = _sessionService.Current.YearOfWork;
                chitiet.IIdMaDonVi = CbxUnitsSelected.ValueItem;
                chitiet.SXauNoiMa = nsMucLucNganSach.SXauNoiMa;
                //chitiet.ISoDuToanDuocDuyet = string.IsNullOrEmpty(item.ISoDuToanDuocDuyet) ? 0 : int.Parse(item.ISoDuToanDuocDuyet);
                //chitiet.FTienDuToanDuyet = string.IsNullOrEmpty(item.FTienDuToanDuyet) ? 0 : Double.Parse(item.FTienDuToanDuyet);
                chitiet.ITongSoThucChi = string.IsNullOrEmpty(item.ITongSoThucChi) ? 0 : Convert.ToInt32(Convert.ToDouble(item.ITongSoThucChi));
                chitiet.FTongTienThucChi = string.IsNullOrEmpty(item.FTongTienThucChi) ? 0 : Double.Parse(item.FTongTienThucChi);
                chitiet.ISoSQThucChi = string.IsNullOrEmpty(item.ISoSQThucChi) ? 0 : Convert.ToInt32(Convert.ToDouble(item.ISoSQThucChi));
                chitiet.FTienSQThucChi = string.IsNullOrEmpty(item.FTienSQThucChi) ? 0 : Double.Parse(item.FTienSQThucChi);
                chitiet.ISoQNCNThucChi = string.IsNullOrEmpty(item.ISoQNCNThucChi) ? 0 : Convert.ToInt32(Convert.ToDouble(item.ISoQNCNThucChi));
                chitiet.FTienQNCNThucChi = string.IsNullOrEmpty(item.FTienQNCNThucChi) ? 0 : Double.Parse(item.FTienQNCNThucChi);
                chitiet.ISoCNVCQPThucChi = string.IsNullOrEmpty(item.ISoCNVCQPThucChi) ? 0 : Convert.ToInt32(Convert.ToDouble(item.ISoCNVCQPThucChi));
                chitiet.FTienCNVCQPThucChi = string.IsNullOrEmpty(item.FTienCNVCQPThucChi) ? 0 : Double.Parse(item.FTienCNVCQPThucChi);
                chitiet.ISoHSQBSThucChi = string.IsNullOrEmpty(item.ISoHSQBSThucChi) ? 0 : Convert.ToInt32(Convert.ToDouble(item.ISoHSQBSThucChi));
                chitiet.FTienHSQBSThucChi = string.IsNullOrEmpty(item.FTienHSQBSThucChi) ? 0 : Double.Parse(item.FTienHSQBSThucChi);
                chitiet.FTienThua = string.IsNullOrEmpty(item.FTienThua) ? 0 : Double.Parse(item.FTienThua);
                chitiet.FTienThieu = string.IsNullOrEmpty(item.FTienThieu) ? 0 : Double.Parse(item.FTienThieu);
                chitiet.ISoLDHDThucChi = string.IsNullOrEmpty(item.ISoLDHDThucChi) ? 0 : Convert.ToInt32(Convert.ToDouble(item.ISoLDHDThucChi));
                chitiet.FTienLDHDThucChi = string.IsNullOrEmpty(item.FTienLDHDThucChi) ? 0 : Double.Parse(item.FTienLDHDThucChi);

                lstChungTuChiTiet.Add(chitiet);
            }

            foreach (var item in divisionDetailsImportDuToan)
            {
                BhQtcnBHXHChiTiet chitiet = new BhQtcnBHXHChiTiet();

                chitiet.Id = Guid.NewGuid();
                chitiet.IIdQTCNamCheDoBHXH = chungTu.Id;
                var nsMucLucNganSach = dictNsNganSachByXauNoiMa[item.SXauNoiMa];
                chitiet.IIdMucLucNganSach = nsMucLucNganSach.IIDMLNS;
                chitiet.SLoaiTroCap = nsMucLucNganSach.SMoTa;
                chitiet.INamLamViec = _sessionService.Current.YearOfWork;
                chitiet.IIdMaDonVi = CbxUnitsSelected.ValueItem;
                chitiet.SXauNoiMa = nsMucLucNganSach.SXauNoiMa;
                //chitiet.ISoDuToanDuocDuyet = string.IsNullOrEmpty(item.ISoDuToanDuocDuyet) ? 0 : int.Parse(item.ISoDuToanDuocDuyet);
                chitiet.FTienDuToanDuyet = string.IsNullOrEmpty(item.FTienDuToanDuyet) ? 0 : Double.Parse(item.FTienDuToanDuyet);

                lstChungTuChiTiet.Add(chitiet);
            }
            _qtcnBHXHChiTietService.AddRange(lstChungTuChiTiet);

            //update chungtu
            chungTu.FTongTienDuToanDuyet = lstChungTuChiTiet.Select(x => x.FTienDuToanDuyet).Sum();
            chungTu.ITongSoSQDeNghi = lstChungTuChiTiet.Select(x => x.ISoSQThucChi).Sum();
            chungTu.FTongTienSQDeNghi = lstChungTuChiTiet.Select(x => x.FTienSQThucChi).Sum();
            chungTu.ITongSoQNCNDeNghi = lstChungTuChiTiet.Select(x => x.ISoQNCNThucChi).Sum();
            chungTu.FTongTienQNCNDeNghi = lstChungTuChiTiet.Select(x => x.FTienQNCNThucChi).Sum();
            chungTu.ITongSoCNVCQPDeNghi = lstChungTuChiTiet.Select(x => x.ISoCNVCQPThucChi).Sum();
            chungTu.FTongTienCNVCQPDeNghi = lstChungTuChiTiet.Select(x => x.FTienCNVCQPThucChi).Sum();
            chungTu.ITongSoHSQBSDeNghi = lstChungTuChiTiet.Select(x => x.ISoHSQBSThucChi).Sum();
            chungTu.FTongTienHSQBSDeNghi = lstChungTuChiTiet.Select(x => x.FTienHSQBSThucChi).Sum();
            chungTu.ITongSoDeNghi = lstChungTuChiTiet.Select(x => x.ITongSoThucChi).Sum();
            chungTu.ITongSoLDHDDeNghi = lstChungTuChiTiet.Select(x => x.ISoLDHDThucChi).Sum();
            chungTu.FTongTienLDHDDeNghi = lstChungTuChiTiet.Select(x => x.FTongTienThucChi).Sum();

            _qtcnBHXHService.Update(chungTu);

            // show message
            System.Windows.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);

            // mở màn hình chứng từ chi tiết
            var entityModel = _mapper.Map<BhQtcnBHXHModel>(chungTu);
            SavedAction?.Invoke(entityModel);
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (DataBhCptuBHYT.DNgayChungTu == null)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            //Check đã tồn tại đơn vị
            var predicate = PredicateBuilder.True<BhQtcnBHXH>();
            predicate = predicate.And(x => x.IIdDonVi == Guid.Parse(CbxUnitsSelected.HiddenValue));
            predicate = predicate.And(x => x.IIdMaDonVi == CbxUnitsSelected.ValueItem);
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            var chungtu = _qtcnBHXHService.FindByCondition(predicate).FirstOrDefault();
            if (chungtu != null)
            {
                messages.Add(string.Format(Resources.AlertExistSettlementMonthVoucher, CbxUnitsSelected.DisplayItem, _sessionService.Current.YearOfWork, ""));
            }

            return string.Join(Environment.NewLine, messages);
        }


        private int GetNextSoChungTuIndex()
        {
            var soChungTuIndex = _qtcnBHXHService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
            return soChungTuIndex;
        }

        private void ShowError(object param)
        {
            int rowIndex = _divisionDetails.IndexOf(SelectedDivisionDetail);
            List<string> errors = _listErrChungTu.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnSaveMLNS()
        {

        }

        private void OnCloseWindow(object obj)
        {
            var window = obj as Window;
            window.Close();
        }

    }
}
