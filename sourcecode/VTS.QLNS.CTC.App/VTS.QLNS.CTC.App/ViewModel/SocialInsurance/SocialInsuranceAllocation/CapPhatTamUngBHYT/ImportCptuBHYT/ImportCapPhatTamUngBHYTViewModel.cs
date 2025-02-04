using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using MaterialDesignThemes.Wpf;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.App.Model.Import;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Command;
using System.Windows.Forms;
using System.IO;
using System.Windows;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Model;
using Microsoft.Extensions.Configuration;
using System.Data;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT.ImportCptuBHYT;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT.ImportCptuBHYT
{
    public class ImportCapPhatTamUngBHYTViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private ICptuBHYTService _cptuBHYTService;
        private ICptuBHYTChiTietService _cptuBHYTChiTietService;
        private IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private IBhDmCoSoYTeService _bhDmCoSoYTeService;
        private IConfiguration _configuration;
        private string _importFolder;
        private string _fileName;
        private string _sDSIDCoSoYTe;
        private ImpHistory _impHistory;
        private List<ImportErrorItem> _listErrChungTuChiTiet;
        private List<ImportErrorItem> _listErrChungTu;
        private List<BhDmMucLucNganSachModel> _mergeItems;
        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        private int _lastRowToRead = 0;

        public Action<BhCptuBHYTModel> SavedAction;

        private BhCptuBHYTModel _dataBhCptuBHYT;
        public BhCptuBHYTModel DataBhCptuBHYT
        {
            get => _dataBhCptuBHYT;
            set => SetProperty(ref _dataBhCptuBHYT, value);
        }
        public override string Name => "Dự toán";
        public override Type ContentType => typeof(ImportCapPhatTamUngBHYT);
        public override string Description => "Nhận phân bổ dự toán chi trên giao";
        public override PackIconKind IconKind => PackIconKind.Dollar;

        public IEnumerable<BhDmMucLucNganSach> ListNsMucLucNganSach { get; set; } = new List<BhDmMucLucNganSach>();


        private NdtctgDetailImportModel _selectedDivision;
        public NdtctgDetailImportModel SelectedDivision
        {
            get => _selectedDivision;
            set => SetProperty(ref _selectedDivision, value);
        }

        private ObservableCollection<CapPhatTamUngBHYTImportExport> _divisionDetails = new ObservableCollection<CapPhatTamUngBHYTImportExport>();
        public ObservableCollection<CapPhatTamUngBHYTImportExport> DivisionDetails
        {
            get => _divisionDetails;
            set => SetProperty(ref _divisionDetails, value);
        }

        private CapPhatTamUngBHYTImportExport _selectedDivisionDetail;
        public CapPhatTamUngBHYTImportExport SelectedDivisionDetail
        {
            get => _selectedDivisionDetail;
            set => SetProperty(ref _selectedDivisionDetail, value);
        }


        private ObservableCollection<CapPhatTamUngBHYTImport> _divisionDetailsTemplate = new ObservableCollection<CapPhatTamUngBHYTImport>();
        public ObservableCollection<CapPhatTamUngBHYTImport> DivisionDetailsTemplate
        {
            get => _divisionDetailsTemplate;
            set => SetProperty(ref _divisionDetailsTemplate, value);
        }

        private CapPhatTamUngBHYTImport _selectedDivisionDetailTemplate;
        public CapPhatTamUngBHYTImport SelectedDivisionDetailTemplate
        {
            get => _selectedDivisionDetailTemplate;
            set => SetProperty(ref _selectedDivisionDetailTemplate, value);
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


        private ObservableCollection<ComboboxItem> _cbxQuyType;
        public ObservableCollection<ComboboxItem> CbxQuyType
        {
            get => _cbxQuyType;
            set => SetProperty(ref _cbxQuyType, value);
        }

        private ComboboxItem _cbxQuyTypeSelected;
        public ComboboxItem CbxQuyTypeSelected
        {
            get => _cbxQuyTypeSelected;
            set
            {
                SetProperty(ref _cbxQuyTypeSelected, value);
            }

        }

        private ObservableCollection<ComboboxItem> _cbxDmCsYte;
        public ObservableCollection<ComboboxItem> CbxDmCsYte
        {
            get => _cbxDmCsYte;
            set => SetProperty(ref _cbxDmCsYte, value);
        }

        private ComboboxItem _cbxDmCsYteSelected;
        public ComboboxItem CbxDmCsYteSelected
        {
            get => _cbxDmCsYteSelected;
            set
            {
                SetProperty(ref _cbxDmCsYteSelected, value);
            }

        }

        private ObservableCollection<ComboboxItem> _cbxLoaiKinhPhi;
        public ObservableCollection<ComboboxItem> CbxLoaiKinhPhi
        {
            get => _cbxLoaiKinhPhi;
            set => SetProperty(ref _cbxLoaiKinhPhi, value);
        }

        private ComboboxItem _cbxLoaiKinhPhiSelected;
        public ComboboxItem CbxLoaiKinhPhiSelected
        {
            get => _cbxLoaiKinhPhiSelected;
            set
            {
                if (DivisionDetails.Count > 0 && value != null)
                {
                    CheckLoaiKinhPhi(value);
                }
                SetProperty(ref _cbxLoaiKinhPhiSelected, value);

                if (value != null)
                {
                    ReloadMLNS();
                    OnPropertyChanged(nameof(ExistedMlns));
                }
            }

        }


        private ObservableCollection<ComboboxItem> _cbxImport;
        public ObservableCollection<ComboboxItem> CbxImport
        {
            get => _cbxImport;
            set => SetProperty(ref _cbxImport, value);
        }

        private ComboboxItem _cbxImportSelected;
        public ComboboxItem CbxImportSelected
        {
            get => _cbxImportSelected;
            set
            {
                SetProperty(ref _cbxImportSelected, value);
                if (value != null)
                {
                    IsImportOption = _cbxImportSelected.ValueItem == "2";

                    _tabIndex = _cbxImportSelected.ValueItem == "2" ? _tabIndex = ImportTabIndex.Data : _tabIndex = ImportTabIndex.DataDetail;
                    OnPropertyChanged(nameof(IsImportOption));
                    OnPropertyChanged(nameof(IsImportOptionInFileExport));
                    OnPropertyChanged(nameof(TabIndex));
                }
            }
        }

        public bool IsImportOptionInFileExport => !IsImportOption;

        private bool _isImportOption;
        public bool IsImportOption
        {
            get => _isImportOption;
            set
            {
                SetProperty(ref _isImportOption, value);
            }
        }

        public Visibility IsImportVisibility => IsImportOption ? Visibility.Visible : Visibility.Collapsed;

        public bool _isSaveData;
        public bool IsSaveData
        {
            get
            {
                if (_cbxImportSelected.ValueItem == "1")
                {
                    return DivisionDetailsTemplate.Count > 0 && DivisionDetailsTemplate.All(x => x.ImportStatus) && !IsValidateExists;
                }
                else
                {
                    return DivisionDetails.Count > 0 && DivisionDetails.All(x => x.ImportStatus) && !IsValidateExists;
                }

            }

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

        public ImportCapPhatTamUngBHYTViewModel(ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            ICptuBHYTService cptuBHYTService,
            ICptuBHYTChiTietService cptuBHYTChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDmCoSoYTeService bhDmCoSoYTeService,
            IConfiguration configuration)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _cptuBHYTService = cptuBHYTService;
            _cptuBHYTChiTietService = cptuBHYTChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDmCoSoYTeService = bhDmCoSoYTeService;
            _configuration = configuration;

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

            _importFolder = _configuration.GetSection("ImportFolder").Value;
            Directory.CreateDirectory(_importFolder);
            LoadLoaiQuy();
            //LoadDanhMucCsYTe()
            LoadLoaiKinhPhi();
            LoadImport();
            DataBhCptuBHYT = new BhCptuBHYTModel();
            int soChungTuIndex = GetNextSoChungTuIndex();
            DataBhCptuBHYT.SSoChungTu = "CP-" + soChungTuIndex.ToString("D3");
            DataBhCptuBHYT.DNgayChungTu = DateTime.Now;
            DataBhCptuBHYT.DNgayQuyetDinh = DateTime.Now;
            OnResetData();
        }

        private void LoadLoaiKinhPhi()
        {
            ObservableCollection<ComboboxItem> lstKinhPhi = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem
                {
                    ValueItem = ((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN).ToString(),
                    DisplayItem = CapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN,
                    HiddenValue = LNSValue.LNS_9040001
                },
                new ComboboxItem
                {
                    ValueItem = ((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD).ToString(),
                    DisplayItem = CapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD,
                    HiddenValue = LNSValue.LNS_9040002
                },

            };
            CbxLoaiKinhPhi = lstKinhPhi;
            CbxLoaiKinhPhiSelected = CbxLoaiKinhPhi.ElementAt(0);
        }

        private void LoadImport()
        {
            ObservableCollection<ComboboxItem> lstImport = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem
                {
                    ValueItem = ((int)TypeImport.ImportTemplate).ToString(),
                    DisplayItem = TypeImportName.ImportTemplate,
                },
                new ComboboxItem
                {
                    ValueItem = ((int)TypeImport.ImportFileExport).ToString(),
                    DisplayItem = TypeImportName.ImportFileExport,
                },

            };
            CbxImport = lstImport;
            CbxImportSelected = CbxImport.ElementAt(0);
        }

        private void CheckLoaiKinhPhi(ComboboxItem LoaiKinhPhi)
        {
            if (DivisionDetails.Any(x => x.LNS != LoaiKinhPhi.HiddenValue))
            {
                System.Windows.MessageBox.Show(Resources.ValidateImportLoaiKinhPhi, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LoadLoaiQuy()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Quý I", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Quý II", ValueItem = "2"},
                new ComboboxItem {DisplayItem = "Quý III", ValueItem = "3"},
                new ComboboxItem {DisplayItem = "Quý IV", ValueItem = "4"}
            };

            CbxQuyType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            CbxQuyTypeSelected = CbxQuyType.ElementAt(0);
        }

        private void LoadDanhMucCsYTe()
        {
            var predicate = PredicateBuilder.True<BhDmCoSoYTe>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            var lstCsYTe = _bhDmCoSoYTeService.FindByCondition(predicate).ToList();
            var cbxCsYte = lstCsYTe.Select(x => new ComboboxItem
            {
                DisplayItem = x.STenCoSoYTe,
                ValueItem = x.IIDMaCoSoYTe,
                Id = x.Id
            });

            CbxDmCsYte = new ObservableCollection<ComboboxItem>(cbxCsYte);
            CbxDmCsYteSelected = CbxDmCsYte.ElementAt(0);
        }

        private void OnResetData()
        {
            _mergeItems = new List<BhDmMucLucNganSachModel>();
            _filePath = string.Empty;

            _divisionDetails = new ObservableCollection<CapPhatTamUngBHYTImportExport>();
            _divisionDetailsTemplate = new ObservableCollection<CapPhatTamUngBHYTImport>();

            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);

            var LNS = CbxLoaiKinhPhiSelected.HiddenValue.Split(',');
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => LNS.Contains(x.SLNS)).OrderBy(x => x.SXauNoiMa).ToList();
            _importedMlns = new ObservableCollection<BhDmMucLucNganSachModel>();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));
            _impHistory = new ImpHistory();
            _listErrChungTuChiTiet = new List<ImportErrorItem>();
            _listErrChungTu = new List<ImportErrorItem>();
            if (_cbxImportSelected.ValueItem == "1")
            {
                _tabIndex = ImportTabIndex.DataDetail;
            }
            else
            {
                _tabIndex = ImportTabIndex.Data;
            }

            LstFile = new ObservableCollection<FileFtpModel>();

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(CbxQuyTypeSelected));
            OnPropertyChanged(nameof(DivisionDetails));
            OnPropertyChanged(nameof(IsSaveData));

            OnPropertyChanged(nameof(TabIndex));
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(LstFile));
        }

        private void ReloadMLNS()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);

            var LNS = CbxLoaiKinhPhiSelected.HiddenValue.Split(',');
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => LNS.Contains(x.SLNS)).OrderBy(x => x.SXauNoiMa).ToList();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));
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
            if (string.IsNullOrEmpty(DataBhCptuBHYT.SSoQuyetDinh))
            {
                System.Windows.Forms.MessageBox.Show(Resources.AlertSoKeHoachEmpty, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var message = string.Join(Environment.NewLine, messages);

            //IsNSSD = VoucherType.NSSD_Key.Equals(_cbxVoucherTypeSelected.ValueItem)
            if (CbxImportSelected.ValueItem == "1")
            {
                OnProcessFileToTemplate(message);
            }
            else
            {
                OnProcessFileToExport(message);
            }
        }

        private void OnProcessFileToTemplate(string message)
        {
            try
            {
                //xử lý chứng từ chi tiết
                var sheetDetailAttribute = (SheetAttribute)Attribute.GetCustomAttribute(typeof(CapPhatTamUngBHYTImport), typeof(SheetAttribute));
                var importDivisionDetailResult = _importService.ProcessData<CapPhatTamUngBHYTImport>(FilePath);
                // Xóa dòng tổng cộng
                importDivisionDetailResult.Data.RemoveAt(importDivisionDetailResult.Data.Count - 1);

                // Xóa các dòng thua
                importDivisionDetailResult.Data.RemoveAll(x => !x.IsHadData);
                // get list MucLucNganSach permission handle
                var yearOfWork = _sessionService.Current.YearOfWork;

                var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);

                ListNsMucLucNganSach = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).ToList();
                var listXauNoiMaHangCha = ListNsMucLucNganSach.Where(x => x.BHangCha).Select(x => x.SXauNoiMa).ToHashSet();

                var nsMucLucNganSachGroupByXauNoiMa = ListNsMucLucNganSach.GroupBy(x => x.SXauNoiMa)
                    .ToDictionary(x => x.Key, x => x.First());

                var dictErr = importDivisionDetailResult.ImportErrors.ToLookup(x => x.Row)
                    .ToDictionary(x => x.Key, x => x.ToList());
                var numberRecordExclude = 0;
                var predicate_csyt = PredicateBuilder.True<BhDmCoSoYTe>();
                predicate_csyt = predicate_csyt.And(x => x.INamLamViec == yearOfWork);
                var lstCsYTe = _bhDmCoSoYTeService.FindByCondition(predicate_csyt).ToList();
                var listIdCSYT = new List<string>();

                var divisionDetailsImport = importDivisionDetailResult.Data.Select((x, i) =>
                {
                    if (!string.IsNullOrEmpty(x.IID_MaCoSoYTe))
                    {
                        listIdCSYT.Add(x.IID_MaCoSoYTe);
                        var coSoYTe = lstCsYTe.Find(y => y.IIDMaCoSoYTe == x.IID_MaCoSoYTe);
                        x.IID_MaCoSoYTe = coSoYTe != null ? coSoYTe.IIDMaCoSoYTe : string.Empty;
                        x.IID_CoSoYTe = coSoYTe != null ? coSoYTe.Id : Guid.NewGuid();
                    }

                    x.LNS = CbxLoaiKinhPhiSelected.HiddenValue;
                    int iQuyKyTruoc = 0;
                    int iNamKyTruoc = 0;
                    int iQuy = Convert.ToInt32(CbxQuyTypeSelected.ValueItem);
                    if (iQuy == 1)
                    {
                        iQuyKyTruoc = 4;
                        iNamKyTruoc = (yearOfWork - 1);
                    }
                    else
                    {
                        iQuyKyTruoc = iQuy - 1;
                        iNamKyTruoc = yearOfWork;
                    }

                    BhCptuBHYTChiTietQuery listDataQuery = _cptuBHYTChiTietService.FindChungTuImport(iQuy, CbxLoaiKinhPhiSelected.HiddenValue, x.IID_MaCoSoYTe, yearOfWork, iQuyKyTruoc, iNamKyTruoc, _sessionService.Current.Principal).FirstOrDefault();
                    if (listDataQuery is null)
                    {
                        return x;
                    }

                    var mlns = ListNsMucLucNganSach.FirstOrDefault(z => z.SXauNoiMa.Equals(CbxLoaiKinhPhiSelected.HiddenValue)); ;
                    x.XauNoiMa = mlns.SXauNoiMa;
                    var isHangCha = listXauNoiMaHangCha.Contains(CbxLoaiKinhPhiSelected.HiddenValue) && ListNsMucLucNganSach.Any(z => z.IIDMLNSCha.Equals(mlns.IIDMLNS));
                    if (!isHangCha)
                    {
                        if (!string.IsNullOrEmpty(nsMucLucNganSachGroupByXauNoiMa.GetValueOrDefault(CbxLoaiKinhPhiSelected.HiddenValue, new BhDmMucLucNganSach()).SMoTa))
                        {
                            x.MoTa = nsMucLucNganSachGroupByXauNoiMa.GetValueOrDefault(CbxLoaiKinhPhiSelected.HiddenValue, new BhDmMucLucNganSach()).SMoTa;
                        }
                        //x.MoTa = nsMucLucNganSachGroupByXauNoiMa.GetValueOrDefault(x.XauNoiMa, new NsMucLucNganSach()).MoTa
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

                    x.FCapThuaQuyTruocChuyenSang = listDataQuery.FCapThuaQuyTruocChuyenSang.ToString();
                    x.FTamUngDenQuyNay = (Convert.ToDouble(x.FQTQuyTruoc.Replace(".", "")) * 0.8).ToString();
                    x.FPhaiCapTamUngQuyNay = (Convert.ToDouble(x.FTamUngDenQuyNay) - Convert.ToDouble(x.FCapThuaQuyTruocChuyenSang)).ToString();
                    x.FLuyKeDenCuoiQuyNay = (Convert.ToDouble(x.FTamUngDenQuyNay) + Convert.ToDouble(listDataQuery.FLuyKeCapCacQuyTruoc)).ToString();

                    return x;
                }).ToList();

                _sDSIDCoSoYTe = string.Join(",", listIdCSYT);
                _divisionDetailsTemplate = new ObservableCollection<CapPhatTamUngBHYTImport>(divisionDetailsImport);
                OnPropertyChanged(nameof(DivisionDetails));

                if (!_divisionDetailsTemplate.Any())
                {
                    message = string.Format(Resources.MsgSheetErrorDataEmpty, sheetDetailAttribute.SheetName);
                    System.Windows.MessageBox.Show(message, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    OnPropertyChanged(nameof(IsSaveData));
                    return;
                }

                foreach (var item in _divisionDetailsTemplate)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName != nameof(CapPhatTamUngBHYTImport.ImportStatus)
                            && args.PropertyName != nameof(CapPhatTamUngBHYTImport.XauNoiMa)
                            && args.PropertyName != nameof(CapPhatTamUngBHYTImport.IsErrorMLNS))
                        {
                            var entityDetail = (CapPhatTamUngBHYTImport)sender;
                            var rowIndex = _divisionDetailsTemplate.IndexOf(entityDetail);
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

                OnPropertyChanged(nameof(DivisionDetailsTemplate));
                OnPropertyChanged(nameof(IsSaveData));
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

        private void OnProcessFileToExport(string message)
        {
            try
            {
                //xử lý chứng từ chi tiết
                var sheetDetailAttribute = (SheetAttribute)Attribute.GetCustomAttribute(typeof(CapPhatTamUngBHYTImportExport), typeof(SheetAttribute));
                var importDivisionDetailResult = _importService.ProcessData<CapPhatTamUngBHYTImportExport>(FilePath);
                // Xóa dòng tổng cộng
                importDivisionDetailResult.Data.RemoveAt(importDivisionDetailResult.Data.Count - 1);
                // get list MucLucNganSach permission handle
                var yearOfWork = _sessionService.Current.YearOfWork;

                var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);

                ListNsMucLucNganSach = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).ToList();
                var listXauNoiMaHangCha = ListNsMucLucNganSach.Where(x => x.BHangCha).Select(x => x.SXauNoiMa).ToHashSet();

                var nsMucLucNganSachGroupByXauNoiMa = ListNsMucLucNganSach.GroupBy(x => x.SXauNoiMa)
                    .ToDictionary(x => x.Key, x => x.First());

                var dictErr = importDivisionDetailResult.ImportErrors.ToLookup(x => x.Row)
                    .ToDictionary(x => x.Key, x => x.ToList());
                var numberRecordExclude = 0;
                var predicate_csyt = PredicateBuilder.True<BhDmCoSoYTe>();
                predicate_csyt = predicate_csyt.And(x => x.INamLamViec == yearOfWork);
                var lstCsYTe = _bhDmCoSoYTeService.FindByCondition(predicate_csyt).ToList();
                var listIdCSYT = new List<string>();

                var divisionDetailsImport = importDivisionDetailResult.Data.Select((x, i) =>
                {
                    if (!string.IsNullOrEmpty(x.IID_MaCoSoYTe))
                    {
                        listIdCSYT.Add(x.IID_MaCoSoYTe);
                        var coSoYTe = lstCsYTe.Find(y => y.IIDMaCoSoYTe == x.IID_MaCoSoYTe);
                        x.IID_MaCoSoYTe = coSoYTe != null ? coSoYTe.IIDMaCoSoYTe : string.Empty;
                        x.IID_CoSoYTe = coSoYTe != null ? coSoYTe.Id : Guid.NewGuid();
                    }

                    int iQuyKyTruoc = 0;
                    int iNamKyTruoc = 0;
                    int iQuy = Convert.ToInt32(CbxQuyTypeSelected.ValueItem);
                    if (iQuy == 1)
                    {
                        iQuyKyTruoc = 4;
                        iNamKyTruoc = (yearOfWork - 1);
                    }
                    else
                    {
                        iQuyKyTruoc = iQuy - 1;
                        iNamKyTruoc = yearOfWork;
                    }

                    BhCptuBHYTChiTietQuery listDataQuery = _cptuBHYTChiTietService.FindChungTuImport(iQuy, x.LNS, x.IID_MaCoSoYTe, yearOfWork, iQuyKyTruoc, iNamKyTruoc, _sessionService.Current.Principal).FirstOrDefault();
                    if (listDataQuery is null)
                    {
                        return x;
                    }

                    var mlns = ListNsMucLucNganSach.FirstOrDefault(z => z.SXauNoiMa.Equals(x.XauNoiMa));
                    var isHangCha = listXauNoiMaHangCha.Contains(x.XauNoiMa) && ListNsMucLucNganSach.Any(z => z.IIDMLNSCha.Equals(mlns.IIDMLNS));
                    if (!isHangCha)
                    {
                        if (!string.IsNullOrEmpty(nsMucLucNganSachGroupByXauNoiMa.GetValueOrDefault(x.XauNoiMa, new BhDmMucLucNganSach()).SMoTa))
                        {
                            x.MoTa = nsMucLucNganSachGroupByXauNoiMa.GetValueOrDefault(x.XauNoiMa, new BhDmMucLucNganSach()).SMoTa;
                        }
                        //x.MoTa = nsMucLucNganSachGroupByXauNoiMa.GetValueOrDefault(x.XauNoiMa, new NsMucLucNganSach()).MoTa
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

                    x.FTamUngDenQuyNay = (Convert.ToDouble(x.FQTQuyTruoc) * 0.8).ToString();
                    x.FCapThuaQuyTruocChuyenSang = listDataQuery.FCapThuaQuyTruocChuyenSang.ToString();
                    x.FLuyKeDenCuoiQuyNay = (Convert.ToDouble(x.FTamUngDenQuyNay) + Convert.ToDouble(listDataQuery.FLuyKeCapCacQuyTruoc)).ToString();
                    x.FPhaiCapTamUngQuyNay = (Convert.ToDouble(x.FTamUngDenQuyNay) - Convert.ToDouble(x.FCapThuaQuyTruocChuyenSang)).ToString();

                    return x;
                }).ToList();

                _sDSIDCoSoYTe = string.Join(",", listIdCSYT);
                _divisionDetails = new ObservableCollection<CapPhatTamUngBHYTImportExport>(divisionDetailsImport);
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
                        if (args.PropertyName != nameof(CapPhatTamUngBHYTImportExport.ImportStatus)
                            && args.PropertyName != nameof(CapPhatTamUngBHYTImportExport.XauNoiMa)
                            && args.PropertyName != nameof(CapPhatTamUngBHYTImportExport.IsErrorMLNS))
                        {
                            var entityDetail = (CapPhatTamUngBHYTImportExport)sender;
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
            if (string.IsNullOrEmpty(DataBhCptuBHYT.SSoQuyetDinh))
            {
                System.Windows.Forms.MessageBox.Show(Resources.AlertSoKeHoachEmpty, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (DivisionDetails.Count > 0 && CbxLoaiKinhPhiSelected != null && DivisionDetails.Any(x => x.LNS != CbxLoaiKinhPhiSelected.HiddenValue))
            {
                System.Windows.MessageBox.Show(Resources.ValidateImportLoaiKinhPhi, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            BhCptuBHYT chungTu = new BhCptuBHYT();
            var divisionDetailsImportTemplate = _divisionDetailsTemplate.Where(x => x.ImportStatus && !x.IsHangCha);

            var divisionDetailsImportExport = _divisionDetails.Where(x => x.ImportStatus && !x.IsHangCha);
            //var chungTuChiTiets = _mapper.Map<List<BhDtctgBHXHChiTiet>>(divisionDetailsImport)
            var lnsTemplate = string.Join(StringUtils.COMMA, _divisionDetailsTemplate.Where(x => x.ImportStatus).Select(x => x.LNS).ToHashSet());
            var lnsExport = string.Join(StringUtils.COMMA, _divisionDetails.Where(x => x.ImportStatus).Select(x => x.LNS).ToHashSet());
            chungTu = _mapper.Map(DataBhCptuBHYT, chungTu);
            chungTu.IQuy = int.Parse(CbxQuyTypeSelected.ValueItem);
            chungTu.ILoaiKinhPhi = int.Parse(CbxLoaiKinhPhiSelected.ValueItem);
            chungTu.SDSLNS = CbxImportSelected.ValueItem == "1" ? lnsTemplate : lnsExport;
            chungTu.SDSID_CoSoYTe = _sDSIDCoSoYTe;
            chungTu.INamLamViec = _sessionService.Current.YearOfWork;
            chungTu.IIDMaDonVi = _sessionService.Current.IdDonVi;
            chungTu.SNguoiTao = _sessionService.Current.Principal;
            chungTu.DNgayTao = DateTime.Now;
            _cptuBHYTService.Add(chungTu);

            var dictNsNganSachByXauNoiMa = ListNsMucLucNganSach.GroupBy(x => x.SXauNoiMa)
                .ToDictionary(x => x.Key, x => x.First());

            List<BhCptuBHYTChiTiet> lstChungTuChiTiet = new List<BhCptuBHYTChiTiet>();
            if (CbxImportSelected.ValueItem == "1")
            {
                foreach (var item in divisionDetailsImportTemplate)
                {
                    BhCptuBHYTChiTiet chitiet = new BhCptuBHYTChiTiet();
                    chitiet.Id = Guid.NewGuid();
                    chitiet.IID_BH_CP_CapTamUng_KCB_BHYT = chungTu.Id;
                    var nsMucLucNganSach = dictNsNganSachByXauNoiMa[item.XauNoiMa];
                    chitiet.IID_MLNS = nsMucLucNganSach.IIDMLNS;
                    chitiet.SLNS = nsMucLucNganSach.SLNS;
                    chitiet.SMoTa = nsMucLucNganSach.SMoTa;
                    chitiet.SXauNoiMa = item.XauNoiMa;
                    chitiet.FQTQuyTruoc = string.IsNullOrEmpty(item.FQTQuyTruoc) ? 0 : Double.Parse(item.FQTQuyTruoc);
                    chitiet.FTamUngQuyNay = string.IsNullOrEmpty(item.FTamUngDenQuyNay) ? 0 : Double.Parse(item.FTamUngDenQuyNay);
                    chitiet.FLuyKeCapDenCuoiQuy = string.IsNullOrEmpty(item.FLuyKeDenCuoiQuyNay) ? 0 : Double.Parse(item.FLuyKeDenCuoiQuyNay);
                    chitiet.IID_MaCoSoYTe = item.IID_MaCoSoYTe;
                    chitiet.IID_CoSoYTe = item.IID_CoSoYTe;
                    chitiet.INamLamViec = _sessionService.Current.YearOfWork;
                    chitiet.IIDMaDonVi = _sessionService.Current.IdDonVi;
                    lstChungTuChiTiet.Add(chitiet);
                }
            }
            else
            {
                foreach (var item in divisionDetailsImportExport)
                {
                    BhCptuBHYTChiTiet chitiet = new BhCptuBHYTChiTiet();
                    chitiet.Id = Guid.NewGuid();
                    chitiet.IID_BH_CP_CapTamUng_KCB_BHYT = chungTu.Id;
                    var nsMucLucNganSach = dictNsNganSachByXauNoiMa[item.XauNoiMa];
                    chitiet.IID_MLNS = nsMucLucNganSach.IIDMLNS;
                    chitiet.SLNS = nsMucLucNganSach.SLNS;
                    chitiet.SMoTa = nsMucLucNganSach.SMoTa;
                    chitiet.SXauNoiMa = item.XauNoiMa;
                    chitiet.FQTQuyTruoc = string.IsNullOrEmpty(item.FQTQuyTruoc) ? 0 : Double.Parse(item.FQTQuyTruoc);
                    chitiet.FTamUngQuyNay = string.IsNullOrEmpty(item.FTamUngDenQuyNay) ? 0 : Double.Parse(item.FTamUngDenQuyNay);
                    chitiet.FLuyKeCapDenCuoiQuy = string.IsNullOrEmpty(item.FLuyKeDenCuoiQuyNay) ? 0 : Double.Parse(item.FLuyKeDenCuoiQuyNay);
                    chitiet.IID_MaCoSoYTe = item.IID_MaCoSoYTe;
                    chitiet.IID_CoSoYTe = item.IID_CoSoYTe;
                    chitiet.INamLamViec = _sessionService.Current.YearOfWork;
                    chitiet.IIDMaDonVi = _sessionService.Current.IdDonVi;
                    lstChungTuChiTiet.Add(chitiet);
                }
            }
            _cptuBHYTChiTietService.AddRange(lstChungTuChiTiet);

            //update chungtu
            chungTu.FQTQuyTruoc = lstChungTuChiTiet.Select(x => x.FQTQuyTruoc).Sum();
            chungTu.FTamUngQuyNay = lstChungTuChiTiet.Select(x => x.FTamUngQuyNay).Sum();
            _cptuBHYTService.Update(chungTu);

            // show message
            System.Windows.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);

            // mở màn hình chứng từ chi tiết
            var entityModel = _mapper.Map<BhCptuBHYTModel>(chungTu);
            SavedAction?.Invoke(entityModel);
        }

        private int GetNextSoChungTuIndex()
        {
            var soChungTuIndex = _cptuBHYTService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
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
                case ImportTabIndex.DataDetail:
                    rowIndex = _divisionDetailsTemplate.IndexOf(SelectedDivisionDetailTemplate);
                    errors = _listErrChungTuChiTiet.Where(x => x.Row == rowIndex).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
                    break;
                case ImportTabIndex.MLNS:
                    errors = new HashSet<string>();
                    break;
            }
            System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
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
