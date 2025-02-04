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
using System.Linq.Expressions;
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
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao.ImportNdtBHXH;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao.ImportNdtBHXH
{
    public class ImportNdtctgBHXHViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private INdtctgBHXHService _ndtctgBHXHService;
        private INdtctgBHXHChiTietService _ndtctgBHXHChiTietService;
        private IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private IConfiguration _configuration;
        private string _importFolder;
        private string _fileName;
        private ImpHistory _impHistory;
        private List<ImportErrorItem> _listErrChungTuChiTiet;
        private List<ImportErrorItem> _listErrChungTu;
        private List<BhDmMucLucNganSachModel> _mergeItems;
        private bool _isUpdateLoad;
        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        private int _lastRowToRead = 0;

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        public Action<BhDtctgBHXHModel> SavedAction;
        public override string Name => "Dự toán";
        public override Type ContentType => typeof(ImportNdtctgBHXH);
        public override string Description => "Nhận phân bổ dự toán chi trên giao";
        public override PackIconKind IconKind => PackIconKind.Dollar;

        public IEnumerable<BhDmMucLucNganSach> ListNsMucLucNganSach = new List<BhDmMucLucNganSach>();
        public IEnumerable<BhDanhMucLoaiChi> lstDanhMucLoaiChi;

        private NdtctgDetailImportModel _selectedDivision;
        public NdtctgDetailImportModel SelectedDivisio
        {
            get => _selectedDivision;
            set => SetProperty(ref _selectedDivision, value);
        }

        private ObservableCollection<NdtctgDetailImportModel> _divisionDetails = new ObservableCollection<NdtctgDetailImportModel>();
        public ObservableCollection<NdtctgDetailImportModel> DivisionDetails
        {
            get => _divisionDetails;
            set => SetProperty(ref _divisionDetails, value);
        }

        private NdtctgDetailImportModel _selectedDivisionDetail;
        public NdtctgDetailImportModel SelectedDivisionDetail
        {
            get => _selectedDivisionDetail;
            set => SetProperty(ref _selectedDivisionDetail, value);
        }

        private ComboboxItem _cbxExpenseTypeSelected;
        public ComboboxItem CbxExpenseTypeSelected
        {
            get => _cbxExpenseTypeSelected;
            set
            {
                SetProperty(ref _cbxExpenseTypeSelected, value);

            }
        }
        private ObservableCollection<ComboboxItem> _cbxExpenseType;
        public ObservableCollection<ComboboxItem> CbxExpenseType
        {
            get => _cbxExpenseType;
            set => SetProperty(ref _cbxExpenseType, value);
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
            }

        }

        private ObservableCollection<ComboboxItem> _typeDotPhanBo = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> TypeDotPhanBo
        {
            get => _typeDotPhanBo;
            set => SetProperty(ref _typeDotPhanBo, value);
        }

        private ComboboxItem _selectDotPhanBo;
        public ComboboxItem SelectDotPhanBo
        {
            get => _selectDotPhanBo;
            set
            {
                SetProperty(ref _selectDotPhanBo, value);

            }
        }

        public bool _isSaveData;
        public bool IsSaveData
        {
            get => DivisionDetails.Count > 0 && DivisionDetails.All(x => x.ImportStatus) && !_importErrors.Any();
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

        public bool IsEnabledMergeBtn
        {
            get => ImportedMlns.Any(i => i.IsSelected) && ExistedMlns.Where(i => i.IsSelected).Count() == 1;
        }

        public bool IsEnabledUnmergeCommand => ExistedMlns.Any(i => i.IsModified);

        private string _soQuyetDinh;
        public string SoQuyetDinh
        {
            get => _soQuyetDinh;
            set => SetProperty(ref _soQuyetDinh, value);
        }

        private string _sSoChungTu;
        public string SSoChungTu
        {
            get => _sSoChungTu;
            set => SetProperty(ref _sSoChungTu, value);
        }

        private DateTime? _ngayQuyetDinh;
        public DateTime? NgayQuyetDinh
        {
            get => _ngayQuyetDinh;
            set => SetProperty(ref _ngayQuyetDinh, value);
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

        public ImportNdtctgBHXHViewModel(ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            INdtctgBHXHService ntctgBHXHService,
            INdtctgBHXHChiTietService ndtctgBHXHChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            ILog log,
            IConfiguration configuration)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _ndtctgBHXHService = ntctgBHXHService;
            _ndtctgBHXHChiTietService = ndtctgBHXHChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _configuration = configuration;
            _logger = log;

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
            _importFolder = _configuration.GetSection("ImportFolder").Value;
            SoQuyetDinh = string.Empty;
            NgayQuyetDinh = DateTime.Now;
            NgayChungTu = DateTime.Now;
            SSoChungTu = GetNextSoChungTuIndex();
            Directory.CreateDirectory(_importFolder);
            OnResetData();
            LoadExpenseType();
            LoadVoucherType();
            LoadBudgetType();

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
            var typeReport = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Đầu năm", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Bổ sung", ValueItem = "2"},
            };

            TypeDotPhanBo = new ObservableCollection<ComboboxItem>(typeReport);
            SelectDotPhanBo = TypeDotPhanBo.ElementAt(0);
        }

        private void OnResetData()
        {
            _mergeItems = new List<BhDmMucLucNganSachModel>();
            _filePath = string.Empty;
            _importErrors = new List<ImportErrorItem>();
            _divisionDetails = new ObservableCollection<NdtctgDetailImportModel>();
            //lstDanhMucLoaiChi = null;
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);

            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).ToList();
            _mucLucNganSachs = _mucLucNganSachs.OrderBy(x => x.SXauNoiMa).ToList();
            _importedMlns = new ObservableCollection<BhDmMucLucNganSachModel>();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));
            _impHistory = new ImpHistory();
            _listErrChungTuChiTiet = new List<ImportErrorItem>();
            _listErrChungTu = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;
            LstFile = new ObservableCollection<FileFtpModel>();

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(CbxVoucherTypeSelected));
            OnPropertyChanged(nameof(SelectDotPhanBo));
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
            _isUpdateLoad = true;
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
        }

        private void OnProcessFile()
        {
            var fileExtension = Path.GetExtension(FilePath).ToLower();
            if (!(fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx")))
            {
                System.Windows.MessageBox.Show(Resources.FileImportWrongExtensionExcel, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _listErrChungTuChiTiet = new List<ImportErrorItem>();
            _listErrChungTu = new List<ImportErrorItem>();

            var messages = new List<string>();

            if (string.IsNullOrEmpty(FilePath))
            {
                messages.Add(Resources.ErrorFileEmpty);
            }
            //if (_cbxExpenseTypeSelected == null)
            //{
            //    messages.Add(Resources.MsgCheckLuaChonLoaChi);
            //}
            if (_selectDotPhanBo == null)
            {
                messages.Add(Resources.ErrorAgencyEmpty);
            }

            var message = string.Join(Environment.NewLine, messages);
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.MessageBox.Show(message, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {

                if (_importErrors != null && _importErrors.Any())
                {
                    _importErrors = new List<ImportErrorItem>();
                    foreach (var error in _divisionDetails)
                    {
                        var rowIndex = _divisionDetails.IndexOf(error);
                        var listError = ValidateItem(error, rowIndex);
                        if (listError.Any())
                        {
                            _importErrors.AddRange(listError);
                            error.ImportStatus = false;
                            if (listError.Any(x => x.IsErrorMLNS))
                            {
                                error.IsError = true;
                            }
                        }
                        else
                        {
                            error.ImportStatus = true;
                            error.IsError = false;
                            _importErrors = _importErrors.Where(x => x.Row != rowIndex).ToList();
                        }

                        var mucLuc = _mucLucNganSachs.FirstOrDefault(x => x.SXauNoiMa == error.SXauNoiMa && x.SMoTa == error.STenMLNS);
                        if (mucLuc != null)
                        {
                            error.IsHangCha = mucLuc.BHangChaDuToan.Value;
                            error.BHangCha = mucLuc.BHangChaDuToan.Value;
                        }
                        OnPropertyChanged(nameof(IsSaveData));
                    }

                    if (_importErrors != null && _importErrors.Any())
                    {
                        var messageOfRow = _importErrors.Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).Distinct();
                        System.Windows.MessageBox.Show(string.Join(Environment.NewLine, messageOfRow), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    if (_importErrors == null && !_importErrors.Any() && !_isUpdateLoad)
                    {
                        OnPropertyChanged(nameof(IsSaveData));
                        return;
                    }
                    XlsFile xls = new XlsFile(false);
                    xls.Open(FilePath);
                    xls.ActiveSheet = 1;
                    _isUpdateLoad = false;
                    _importErrors = new List<ImportErrorItem>();
                    var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                    predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                    //IEnumerable<BhDmMucLucNganSach> _nsMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).OrderBy(x => x.SXauNoiMa).ToList();
                    //var sLNS = lstDanhMucLoaiChi.Where(x => x.Id == CbxExpenseTypeSelected.Id).Select(x => x.SLNS).FirstOrDefault();
                    //var dataSLNS = sLNS.Split(',');
                    //_nsMucLucs = _nsMucLucs.Where(x => dataSLNS.Contains(x.SLNS)).ToList();

                    var dataImport = _importService.ProcessData<NdtctgDetailImportModel>(FilePath);
                    var lstChungTuImport = new ObservableCollection<NdtctgDetailImportModel>(dataImport.Data);

                    List<string> lstError = new List<string>();

                    if (dataImport.ImportErrors.Count > 0)
                    {
                        _importErrors.AddRange(dataImport.ImportErrors);
                    }

                    if (lstChungTuImport == null || lstChungTuImport.Count <= 0)
                    {
                        System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    int i = 0;
                    foreach (var item in lstChungTuImport)
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
                        var mucLuc = _mucLucNganSachs.FirstOrDefault(x => x.SXauNoiMa == item.SXauNoiMa && x.SMoTa == item.STenMLNS);
                        if (mucLuc is null)
                        {
                            continue;
                        }

                        item.IsHangCha = mucLuc.BHangChaDuToan.Value;
                        item.BHangCha = mucLuc.BHangChaDuToan.Value;
                    }

                    if (lstChungTuImport.Any(x => !x.ImportStatus))
                    {
                        System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    ImportedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs.OrderBy(x => x.SXauNoiMa)));
                    DivisionDetails = lstChungTuImport;

                    foreach (var item in _divisionDetails)
                    {
                        item.PropertyChanged += (sender, args) =>
                        {
                            if (nameof(NdtctgDetailImportModel.SXauNoiMa) == nameof(NdtctgDetailImportModel.SXauNoiMa))
                            {
                                var entityDetail = (NdtctgDetailImportModel)sender;
                                var rowIndex = _divisionDetails.IndexOf(entityDetail);
                                var listError = _importService.ValidateItem(entityDetail, rowIndex);
                                if (listError.Count > 0)
                                {
                                    var messageOfRow = listError.Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToList();
                                    System.Windows.MessageBox.Show(string.Join(Environment.NewLine, messageOfRow), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                                    _importErrors.AddRange(listError);
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
                                    _importErrors = _importErrors.Where(x => x.Row != rowIndex).ToList();
                                }
                                OnPropertyChanged(nameof(IsSaveData));
                            }
                        };
                    }
                    OnPropertyChanged(nameof(DivisionDetails));
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

        private IEnumerable<ImportErrorItem> ValidateItem(NdtctgDetailImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                if (string.IsNullOrEmpty(item.STenMLNS))
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

        private string GetLNS(List<string> lstSMaLoaiChi)
        {
            string sLNS = string.Empty;
            if (lstSMaLoaiChi.Contains(MaLoaiChiBHXH.SMABHXH))
            {
                sLNS += LNSValue.LNS_901_9010001_9010002 + ",";
            }
            if (lstSMaLoaiChi.Contains(MaLoaiChiBHXH.SMAKPQL))
            {
                sLNS += LNSValue.LNS_9010003 + ",";
            }
            if (lstSMaLoaiChi.Contains(MaLoaiChiBHXH.SMAKCBQYDV))
            {
                sLNS += LNSValue.LNS_9010004_9010005 + ",";
            }
            if (lstSMaLoaiChi.Contains(MaLoaiChiBHXH.SMAKCBTS))
            {
                sLNS += LNSValue.LNS_9010006_9010007 + ",";
            }
            if (lstSMaLoaiChi.Contains(MaLoaiChiBHXH.SMAKCBBHYT))
            {
                sLNS += LNSValue.LNS_9010008 + ",";
            }
            if (lstSMaLoaiChi.Contains(MaLoaiChiBHXH.SMAMSTTBYT))
            {
                sLNS += LNSValue.LNS_9010009 + ",";
            }
            if (lstSMaLoaiChi.Contains(MaLoaiChiBHXH.SMABHTN))
            {
                sLNS += LNSValue.LNS_9010010 + ",";
            }
            if (lstSMaLoaiChi.Contains(MaLoaiChiBHXH.SMAHSSVNLD))
            {
                sLNS += LNSValue.LNS_9050001_9050002 + ",";
            }

            return sLNS.Remove(sLNS.Length - 1);
        }
        private void OnSaveData()
        {
            string errorValidate = GetMessageValidate();
            if (!string.IsNullOrEmpty(errorValidate))
            {
                System.Windows.MessageBox.Show(errorValidate, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var divisionDetailsImport = _divisionDetails.Where(x => x.ImportStatus && !x.BHangCha);
            List<string> lstMaLoaiChi = divisionDetailsImport.Select(x => x.SMaLoaiChi).Distinct().ToList();

            BhDtctgBHXH chungTu = new BhDtctgBHXH();

            string sLNS = GetLNS(lstMaLoaiChi);
            string sMaLoaiChi = string.Join(",", divisionDetailsImport.Select(x => x.SMaLoaiChi).ToList().Distinct());
            chungTu.SSoChungTu = SSoChungTu;
            chungTu.SSoQuyetDinh = SoQuyetDinh;
            chungTu.DNgayChungTu = NgayChungTu;
            chungTu.DNgayQuyetDinh = NgayQuyetDinh;
            chungTu.SLNS = sLNS;
            chungTu.ILoaiDotNhanPhanBo = int.Parse(SelectDotPhanBo.ValueItem);
            chungTu.IID_MaDonVi = _sessionService.Current.IdDonVi;
            chungTu.INamLamViec = _sessionService.Current.YearOfWork;
            //chungTu.IIdLoaiDanhMucChi = CbxExpenseTypeSelected.Id;
            chungTu.SMaLoaiChi = sMaLoaiChi;
            chungTu.SNguoiTao = _sessionService.Current.Principal;
            chungTu.DNgayTao = DateTime.Now;
            _ndtctgBHXHService.Add(chungTu);

            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            IEnumerable<BhDmMucLucNganSach> _nsMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc)
                                                                                    .OrderBy(x => x.SXauNoiMa).ToList();
            _nsMucLucs = _nsMucLucs.Where(x => x.BHangChaDuToan != null).ToList();
            List<BhDtctgBHXHChiTiet> lstChungTuChiTiet = new List<BhDtctgBHXHChiTiet>();
            foreach (var item in divisionDetailsImport)
            {
                BhDmMucLucNganSach mucLuc = _nsMucLucs.FirstOrDefault(x => x.INamLamViec == _sessionInfo.YearOfWork && x.ITrangThai == StatusType.ACTIVE
                && item.STenMLNS == x.SMoTa && item.SXauNoiMa == x.SXauNoiMa);
                if (mucLuc == null)
                {
                    continue;
                }

                BhDtctgBHXHChiTiet chitiet = new BhDtctgBHXHChiTiet();
                chitiet.Id = Guid.NewGuid();
                chitiet.IID_DTC_DuToanChiTrenGiao = chungTu.Id;
                chitiet.IID_MucLucNganSach = mucLuc.IIDMLNS;
                chitiet.SLNS = mucLuc.SLNS;
                chitiet.SM = mucLuc.SM;
                chitiet.STM = mucLuc.STM;
                chitiet.STTM = mucLuc.STTM;
                chitiet.SNG = mucLuc.SNG;
                chitiet.SNoiDung = mucLuc.SMoTa;
                chitiet.FTienTuChi = string.IsNullOrEmpty(item.SDuToan) ? 0 : item.FTienDuToan;
                chitiet.INamLamViec = _sessionService.Current.YearOfWork;
                chitiet.SXauNoiMa = mucLuc.SXauNoiMa;
                chitiet.IIDMaDonVi = _sessionService.Current.IdDonVi;
                chitiet.SMaLoaiChi = item.SMaLoaiChi;
                chitiet.SNguoiTao = _sessionService.Current.Principal;
                chitiet.DNgayTao = DateTime.Now;

                chitiet.SNguoiSua = _sessionService.Current.Principal;
                chitiet.DNgaySua = DateTime.Now;
                lstChungTuChiTiet.Add(chitiet);
            }
            _ndtctgBHXHChiTietService.AddRange(lstChungTuChiTiet);

            chungTu.FTongTienTuChi = divisionDetailsImport.Where(x => !x.BHangCha && !string.IsNullOrEmpty(x.SDuToan)).Sum(x => x.FTienDuToan);
            _ndtctgBHXHService.Update(chungTu);

            // show message
            System.Windows.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);

            // mở màn hình chứng từ chi tiết
            var entityModel = _mapper.Map<BhDtctgBHXHModel>(chungTu);
            SavedAction?.Invoke(entityModel);
        }

        private string GetNextSoChungTuIndex()
        {
            var soChungTuIndex = _ndtctgBHXHService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
            return "DTC-" + soChungTuIndex.ToString("D3");
        }

        private void LoadExpenseType()
        {
            lstDanhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
            var cbxExpense = lstDanhMucLoaiChi?.Select(x => new ComboboxItem
            {
                DisplayItem = x.STenDanhMucLoaiChi,
                HiddenValue = x.SMaLoaiChi,
                ValueItem = x.SLNS,
                Id = x.Id
            }).ToList();
            CbxExpenseType = new ObservableCollection<ComboboxItem>(cbxExpense);
            if (CbxExpenseType.Count() > 0)
            {
                CbxExpenseTypeSelected = CbxExpenseType.ElementAt(0);
            }
        }

        private void ShowError(object param)
        {
            int rowIndex = _divisionDetails.IndexOf(SelectedDivisionDetail);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
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
            if (!NgayChungTu.HasValue)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            if (_selectDotPhanBo == null)
            {
                messages.Add(Resources.AlertLoaiDuToanEmpty);
            }

            //if (_cbxExpenseTypeSelected == null)
            //{
            //    messages.Add(Resources.MsgCheckLuaChonLoaChi);
            //}

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
    }
}
