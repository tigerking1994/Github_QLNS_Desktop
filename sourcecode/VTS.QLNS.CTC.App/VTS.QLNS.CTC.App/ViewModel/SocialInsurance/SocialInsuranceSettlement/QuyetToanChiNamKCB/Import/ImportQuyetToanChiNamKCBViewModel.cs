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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamKCB.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamKCB.Import
{
    public class ImportQuyetToanChiNamKCBViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private IQtcnKCBService _qtcnKCBService;
        private IQtcnKCBChiTietService _qtcnKCBChiTietService;
        private IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private IBhDmCoSoYTeService _bhDmCoSoYTeService;
        private INsDonViService _nSDonViService;
        private IConfiguration _configuration;
        private string _importFolder;
        private string _fileName;
        private SessionInfo _sessionInfo;
        private ILog _logger;
        private ImpHistory _impHistory;
        private List<ImportErrorItem> _listErrChungTuChiTiet;
        private List<ImportErrorItem> _listErrChungTu;
        private List<BhDmMucLucNganSachModel> _mergeItems;
        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        private int _lastRowToRead = 0;
        private bool _isUploadFile;
        public Action<BhQtcnKCBModel> SavedAction;

        private BhQtcnKCBModel _dataBhCptuBHYT;
        public BhQtcnKCBModel DataBhCptuBHYT
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

        public override string Name => "";
        public override Type ContentType => typeof(ImportQuyetToanChiNamKCB);
        public override string Description => "Nhận phân bổ dự toán chi trên giao";
        public override PackIconKind IconKind => PackIconKind.Dollar;

        public IEnumerable<BhDmMucLucNganSach> ListNsMucLucNganSach = new List<BhDmMucLucNganSach>();


        private NdtctgDetailImportModel _selectedDivision;
        public NdtctgDetailImportModel SelectedDivision
        {
            get => _selectedDivision;
            set => SetProperty(ref _selectedDivision, value);
        }

        private ObservableCollection<QtcnKCBDetailImportModel> _itemsImport;
        public ObservableCollection<QtcnKCBDetailImportModel> ItemsImport
        {
            get => _itemsImport;
            set => SetProperty(ref _itemsImport, value);
        }

        private ObservableCollection<QtcnKCBDetailImportModel> _divisionDetails;
        public ObservableCollection<QtcnKCBDetailImportModel> DivisionDetails
        {
            get => _divisionDetails;
            set => SetProperty(ref _divisionDetails, value);
        }

        private QtcnKCBDetailImportModel _selectedDivisionDetail;
        public QtcnKCBDetailImportModel SelectedDivisionDetail
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
            get => DivisionDetails.Count > 0 && DivisionDetails.All(x => x.ImportStatus) && !_listErrChungTu.Any();
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

        public ImportQuyetToanChiNamKCBViewModel(ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            IQtcnKCBService qtcnKCBService,
            IQtcnKCBChiTietService qtcnKCBChiTietService,
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
            _qtcnKCBService = qtcnKCBService;
            _qtcnKCBChiTietService = qtcnKCBChiTietService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
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
            DataBhCptuBHYT = new BhQtcnKCBModel();
            int soChungTuIndex = GetNextSoChungTuIndex();
            DataBhCptuBHYT.SSoChungTu = "QTC-" + soChungTuIndex.ToString("D3");
            DataBhCptuBHYT.DNgayChungTu = DateTime.Now;
            DataBhCptuBHYT.DNgayQuyetDinh = DateTime.Now;
            OnResetData();
        }

        private void OnResetData()
        {
            _mergeItems = new List<BhDmMucLucNganSachModel>();
            _filePath = string.Empty;
            _itemsImport = new ObservableCollection<QtcnKCBDetailImportModel>();
            _divisionDetails = new ObservableCollection<QtcnKCBDetailImportModel>();
            var lstLns = BhxhMLNS.SLNS_QTC_KCB_QYDV.Split(StringUtils.COMMA);
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);
            predicate_danhmuc = predicate_danhmuc.And(x => lstLns.Contains(x.SLNS));
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).OrderBy(x => x.SXauNoiMa).ToList();
            _importedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));
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

            _listErrChungTu.Clear();
            FilePath = openFileDialog.FileName;
            _fileName = openFileDialog.SafeFileName;
            _isUploadFile = true;
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
        }

        private void OnProcessFile()
        {
            _listErrChungTu = new List<ImportErrorItem>();
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

                    _isUploadFile = false;
                    _listErrChungTu = new List<ImportErrorItem>();
                    var messages = new List<string>();

                    if (string.IsNullOrEmpty(FilePath))
                    {
                        messages.Add(Resources.ErrorFileEmpty);
                    }
                    if (string.IsNullOrEmpty(DataBhCptuBHYT.SSoQuyetDinh))
                    {
                        messages.Add(Resources.AlertSoQuyetDinhEmpty);
                    }

                    var message = string.Join(Environment.NewLine, messages);

                    XlsFile xls = new XlsFile(false);
                    xls.Open(FilePath);
                    xls.ActiveSheet = 1;
                    var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                    predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                    IEnumerable<BhDmMucLucNganSach> _nsMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).OrderBy(x => x.SXauNoiMa).ToList();

                    var dataSLNS = LNSValue.LNS_9010004_9010005.Split(',');
                    _nsMucLucs = _nsMucLucs.Where(x => dataSLNS.Contains(x.SLNS)).ToList();
                    ListNsMucLucNganSach = _nsMucLucs;

                    var dataImport = _importService.ProcessData<QtcnKCBDetailImportModel>(FilePath);
                    ItemsImport = new ObservableCollection<QtcnKCBDetailImportModel>(dataImport.Data);

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

                    int i = 0;
                    foreach (var item in ItemsImport)
                    {
                        i++;
                        var listError = ValidateItem(item, i);

                        if (listError.Any())
                        {
                            _listErrChungTu.AddRange(listError);
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
                    _divisionDetails = new ObservableCollection<QtcnKCBDetailImportModel>(ItemsImport);

                    OnPropertyChanged(nameof(DivisionDetails));
                    foreach (var item in DivisionDetails)
                    {
                        item.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(QtcnKCBDetailImportModel.SXauNoiMa))
                            {
                                var entityDetail = (QtcnKCBDetailImportModel)sender;
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

        private IEnumerable<ImportErrorItem> ValidateItem(QtcnKCBDetailImportModel item, int rowIndex)
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

            BhQtcnKCB chungTu = new BhQtcnKCB();
            var divisionDetailsImport = _divisionDetails.Where(x => x.ImportStatus && !x.IsHangCha);
            var divisionDetailsDuToan = _divisionDetails.Where(x => x.ImportStatus && x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToiKCBQY);

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
            chungTu.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTu;
            chungTu.IIdDonVi = Guid.Parse(CbxUnitsSelected.HiddenValue);
            chungTu.SDSLNS = LNSValue.LNS_9010004_9010005;
            chungTu.IIdMaDonVi = CbxUnitsSelected.ValueItem;
            chungTu.DNgayTao = DateTime.Now;
            chungTu.DNgaySua = null;
            var lstDonVi = _nSDonViService.FindAll().Where(x => x.NamLamViec == _sessionService.Current.YearOfWork).ToList();
            var donvi = lstDonVi?.Where(x => x.IIDMaDonVi == CbxUnitsSelected.ValueItem).FirstOrDefault();
            if (donvi != null && donvi.Loai == LoaiDonVi.ROOT)
            {
                chungTu.BDaTongHop = false;
                chungTu.IITongHopID = Guid.NewGuid();
                chungTu.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
            }
            else
            {
                chungTu.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTu;
                chungTu.BDaTongHop = false;
            }
            _qtcnKCBService.Add(chungTu);

            var dictNsNganSachByXauNoiMa = ListNsMucLucNganSach.GroupBy(x => x.SXauNoiMa)
                .ToDictionary(x => x.Key, x => x.First());

            List<BhQtcnKCBChiTiet> lstChungTuChiTiet = new List<BhQtcnKCBChiTiet>();
            foreach (var item in divisionDetailsImport)
            {
                BhQtcnKCBChiTiet chitiet = new BhQtcnKCBChiTiet();

                chitiet.Id = Guid.NewGuid();
                chitiet.IIdQTCNamKCBQuanYDonVi = chungTu.Id;

                var nsMucLucNganSach = dictNsNganSachByXauNoiMa[item.SXauNoiMa];
                chitiet.IIdMucLucNganSach = nsMucLucNganSach.IIDMLNS;
                chitiet.SNoiDung = nsMucLucNganSach.SMoTa;
                chitiet.SXauNoiMa = nsMucLucNganSach.SXauNoiMa;
                chitiet.IIdMaDonVi = CbxUnitsSelected.ValueItem;
                chitiet.INamLamViec = _sessionService.Current.YearOfWork;
                chitiet.FTienDuToanNamTruocChuyenSang = string.IsNullOrEmpty(item.FTienDuToanNamTruocChuyenSang) ? 0 : Double.Parse(item.FTienDuToanNamTruocChuyenSang);
                chitiet.FTienDuToanGiaoNamNay = string.IsNullOrEmpty(item.FTienDuToanGiaoNamNay) ? 0 : Double.Parse(item.FTienDuToanGiaoNamNay);
                chitiet.FTienTongDuToanDuocGiao = string.IsNullOrEmpty(item.FTienTongDuToanDuocGiao) ? 0 : Double.Parse(item.FTienTongDuToanDuocGiao);
                chitiet.FTienThucChi = string.IsNullOrEmpty(item.FTienThucChi) ? 0 : Double.Parse(item.FTienThucChi);
                chitiet.FTienThua = string.IsNullOrEmpty(item.FTienThua) ? 0 : Double.Parse(item.FTienThua);
                chitiet.FTienThieu = string.IsNullOrEmpty(item.FTienThieu) ? 0 : Double.Parse(item.FTienThieu);


                lstChungTuChiTiet.Add(chitiet);
            }

            foreach (var item in divisionDetailsDuToan)
            {
                BhQtcnKCBChiTiet chitiet = new BhQtcnKCBChiTiet();

                chitiet.Id = Guid.NewGuid();
                chitiet.IIdQTCNamKCBQuanYDonVi = chungTu.Id;

                var nsMucLucNganSach = dictNsNganSachByXauNoiMa[item.SXauNoiMa];
                chitiet.IIdMucLucNganSach = nsMucLucNganSach.IIDMLNS;
                chitiet.SNoiDung = nsMucLucNganSach.SMoTa;
                chitiet.SXauNoiMa = nsMucLucNganSach.SXauNoiMa;
                chitiet.IIdMaDonVi = CbxUnitsSelected.ValueItem;
                chitiet.INamLamViec = _sessionService.Current.YearOfWork;
                chitiet.FTienDuToanGiaoNamNay = string.IsNullOrEmpty(item.FTienDuToanGiaoNamNay) ? 0 : Double.Parse(item.FTienDuToanGiaoNamNay);
                lstChungTuChiTiet.Add(chitiet);
            }
            _qtcnKCBChiTietService.AddRange(lstChungTuChiTiet);

            //update chungtu
            chungTu.FTongTienDuToanNamTruocChuyenSang = lstChungTuChiTiet.Select(x => x.FTienDuToanNamTruocChuyenSang).Sum();
            chungTu.FTongTienDuToanGiaoNamNay = lstChungTuChiTiet.Select(x => x.FTienDuToanGiaoNamNay).Sum();
            chungTu.FTongTienTongDuToanDuocGiao = chungTu.FTongTienDuToanNamTruocChuyenSang + chungTu.FTongTienDuToanGiaoNamNay;
            chungTu.FTongTienThucChi = lstChungTuChiTiet.Select(x => x.FTienThucChi).Sum();
            chungTu.FTongTienThua = (chungTu.FTongTienTongDuToanDuocGiao > chungTu.FTongTienThucChi) ? chungTu.FTongTienTongDuToanDuocGiao - chungTu.FTongTienThucChi : 0;
            chungTu.FTongTienThieu = (chungTu.FTongTienThucChi > chungTu.FTongTienTongDuToanDuocGiao) ? chungTu.FTongTienThucChi - chungTu.FTongTienTongDuToanDuocGiao : 0;
            chungTu.FTiLeThucHienTrenDuToan = (chungTu.FTongTienTongDuToanDuocGiao > 0) ? (chungTu.FTongTienThucChi / chungTu.FTongTienTongDuToanDuocGiao) * 100 : 0;
            _qtcnKCBService.Update(chungTu);

            // show message
            System.Windows.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);

            // mở màn hình chứng từ chi tiết
            var entityModel = _mapper.Map<BhQtcnKCBModel>(chungTu);
            SavedAction?.Invoke(entityModel);
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (DataBhCptuBHYT.DNgayChungTu == null)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            //if (string.IsNullOrEmpty(DataBhCptuBHYT.SSoQuyetDinh))
            //{
            //    messages.Add(Resources.AlertSoQuyetDinhEmpty);
            //}

            //if (DataBhCptuBHYT.DNgayQuyetDinh == null)
            //{
            //    messages.Add(Resources.AlertNgayQuyetDinhEmpty);
            //}

            //Check đã tồn tại đơn vị
            var predicate = PredicateBuilder.True<BhQtcnKCB>();
            predicate = predicate.And(x => x.IIdDonVi == Guid.Parse(CbxUnitsSelected.HiddenValue));
            predicate = predicate.And(x => x.IIdMaDonVi == CbxUnitsSelected.ValueItem);
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            var chungtu = _qtcnKCBService.FindByCondition(predicate).FirstOrDefault();
            if (chungtu != null)
            {
                messages.Add(string.Format(Resources.AlertExistSettlementMonthVoucher, CbxUnitsSelected.DisplayItem, _sessionService.Current.YearOfWork, ""));
            }

            return string.Join(Environment.NewLine, messages);
        }


        private int GetNextSoChungTuIndex()
        {
            var soChungTuIndex = _qtcnKCBService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
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
