using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility.Exceptions;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Import
{
    public class ImportDemandViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktMucLucService _sktMucLucService;
        private readonly ICauHinhCanCuService _iCauHinhCanCuService;
        private readonly ISktChungTuChiTietCanCuService _iSktChungTuChiTietCanCuService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private readonly FtpStorageService _ftpStorageService;
        private readonly IHTTPUploadFileService _hTTPUploadFileService;
        public bool IsSendHTTP;

        public override string Name => "Ngân sách thường xuyên";
        public override Type ContentType => typeof(View.Budget.DemandCheck.Demand.ImportDemand.ImportDemand);
        public override string Description => "Quyết toán Lương, Phụ cấp, Trợ cấp, Tiền ăn";
        public override PackIconKind IconKind => PackIconKind.Dollar;


        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }
        private List<NsSktMucLuc> _nsSktMucLucs;
        private List<DemandVoucherDetailImportModel> _demandVoucherDetailProcess;

        private ObservableCollection<DemandVoucherDetailImportModel> _demandVoucherDetails;
        public ObservableCollection<DemandVoucherDetailImportModel> DemandVoucherDetails
        {
            get => _demandVoucherDetails;
            set => SetProperty(ref _demandVoucherDetails, value);
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
            set
            {
                SetProperty(ref _budgetSourceTypeSelected, value);
                LoadDonVi();
                OnPropertyChanged();
            }
        }

        private DemandVoucherDetailImportModel _selectedItem;
        public DemandVoucherDetailImportModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (DemandVoucherDetails.Count > 0)
                    return !DemandVoucherDetails.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }

        private ObservableCollection<ComboboxItem> _listDonVi = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }

        private ComboboxItem _donViSelected;

        public ComboboxItem DonViSelected
        {
            get => _donViSelected;
            set
            {
                SetProperty(ref _donViSelected, value);
            }
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
                OnPropertyChanged(nameof(ShowColNSBD));
                OnPropertyChanged(nameof(ShowColNSSD));
                LoadDonVi();
                ShowColumn();
            }
        }

        private ObservableCollection<SktMucLucModel> _existedMlskt;
        public ObservableCollection<SktMucLucModel> ExistedMlskt
        {
            get => _existedMlskt;
            set => SetProperty(ref _existedMlskt, value);
        }

        private ObservableCollection<SktMucLucModel> _importedMlskt;
        public ObservableCollection<SktMucLucModel> ImportedMlskt
        {
            get => _importedMlskt;
            set => SetProperty(ref _importedMlskt, value);
        }

        private List<SktMucLucModel> _mergeItems;
        public bool IsEnableSaveMLSKT => _mergeItems.Count > 0;

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }
        //private string _filePath;
        //public string FilePath
        //{
        //    get => _filePath;
        //    set => SetProperty(ref _filePath, value);
        //}
        public bool IsEnabledMergeBtn
        {
            get => ImportedMlskt.Where(i => i.IsSelected).Count() > 0 && ExistedMlskt.Where(i => i.IsSelected).Count() == 1;
        }

        public bool IsEnabledUnmergeCommand
        {
            get => ExistedMlskt.Where(i => i.IsModified && i.IsSelected).Count() > 0;
        }

        private SktMucLucModel _selectedParent;
        public SktMucLucModel SelectedParent
        {
            get => _selectedParent;
            set
            {
                SetProperty(ref _selectedParent, value);
                OnPropertyChanged(nameof(IsEnabledMergeBtn));
            }
        }
        private ObservableCollection<FileFilterQuery> _lstFile;
        public ObservableCollection<FileFilterQuery> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }

        private FileFilterQuery _fileSelected;
        public FileFilterQuery FileSelected
        {
            get => _fileSelected;
            set => SetProperty(ref _fileSelected, value);
        }

        public DateTime NgayChungTu { get; set; }
        public string MoTa { get; set; }
        public Visibility ShowColNSBD => VoucherTypeSelected != null && VoucherType.NSBD_Key == VoucherTypeSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowColNSSD => VoucherTypeSelected != null && VoucherType.NSSD_Key == VoucherTypeSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;

        private Visibility _tc1Visible;
        public Visibility TC1Visible
        {
            get => _tc1Visible;
            set => SetProperty(ref _tc1Visible, value);
        }
        private Visibility _tc2Visible;
        public Visibility TC2Visible
        {
            get => _tc2Visible;
            set => SetProperty(ref _tc2Visible, value);
        }
        private Visibility _tc3Visible;
        public Visibility TC3Visible
        {
            get => _tc3Visible;
            set => SetProperty(ref _tc3Visible, value);
        }
        private Visibility _tc4Visible;
        public Visibility TC4Visible
        {
            get => _tc4Visible;
            set => SetProperty(ref _tc4Visible, value);
        }
        private Visibility _tc5Visible;
        public Visibility TC5Visible
        {
            get => _tc5Visible;
            set => SetProperty(ref _tc5Visible, value);
        }

        private Visibility _mHHV1Visible;
        public Visibility MHHV1Visible
        {
            get => _mHHV1Visible;
            set => SetProperty(ref _mHHV1Visible, value);
        }

        private Visibility _dT1Visible;
        public Visibility DT1Visible
        {
            get => _dT1Visible;
            set => SetProperty(ref _dT1Visible, value);
        }

        private Visibility _mHHV2Visible;
        public Visibility MHHV2Visible
        {
            get => _mHHV2Visible;
            set => SetProperty(ref _mHHV2Visible, value);
        }

        private Visibility _dT2Visible;
        public Visibility DT2Visible
        {
            get => _dT2Visible;
            set => SetProperty(ref _dT2Visible, value);
        }
        private Visibility _mHHV3Visible;
        public Visibility MHHV3Visible
        {
            get => _mHHV3Visible;
            set => SetProperty(ref _mHHV3Visible, value);
        }

        private Visibility _dT3Visible;
        public Visibility DT3Visible
        {
            get => _dT3Visible;
            set => SetProperty(ref _dT3Visible, value);
        }
        private Visibility _mHHV4Visible;
        public Visibility MHHV4Visible
        {
            get => _mHHV4Visible;
            set => SetProperty(ref _mHHV4Visible, value);
        }

        private Visibility _dT4Visible;
        public Visibility DT4Visible
        {
            get => _dT4Visible;
            set => SetProperty(ref _dT4Visible, value);
        }
        private Visibility _mHHV5Visible;
        public Visibility MHHV5Visible
        {
            get => _mHHV5Visible;
            set => SetProperty(ref _mHHV5Visible, value);
        }

        private Visibility _dT5Visible;
        public Visibility DT5Visible
        {
            get => _dT5Visible;
            set => SetProperty(ref _dT5Visible, value);
        }



        public bool IsReadOnlyX1 { get; set; }
        public bool IsReadOnlyX2 { get; set; }
        public bool IsReadOnlyX3 { get; set; }
        public bool IsReadOnlyX4 { get; set; }
        public bool IsReadOnlyX5 { get; set; }

        private string _tc1;
        public string TC1
        {
            get => _tc1;
            set => SetProperty(ref _tc1, value);
        }

        private string _tc2;
        public string TC2
        {
            get => _tc2;
            set => SetProperty(ref _tc2, value);
        }

        private string _tc3;
        public string TC3
        {
            get => _tc3;
            set => SetProperty(ref _tc3, value);
        }

        private string _tc4;
        public string TC4
        {
            get => _tc4;
            set => SetProperty(ref _tc4, value);
        }

        private string _tc5;
        public string TC5
        {
            get => _tc5;
            set => SetProperty(ref _tc5, value);
        }

        private ImportResult<DemandVoucherDetailImportModelNSSD> _demandVoucherDetailNSSDResult;
        public ImportResult<DemandVoucherDetailImportModelNSSD> DemandVoucherDetailNSSDResult
        {
            get => _demandVoucherDetailNSSDResult;
            set
            {
                SetProperty(ref _demandVoucherDetailNSSDResult, value);
                OnPropertyChanged(nameof(_demandVoucherDetailNSSDResult));
            }
        }

        private ImportResult<DemandVoucherDetailImportModelNSBD> _demandVoucherDetailNSBDResult;
        public ImportResult<DemandVoucherDetailImportModelNSBD> DemandVoucherDetailNSBDResult
        {
            get => _demandVoucherDetailNSBDResult;
            set
            {
                SetProperty(ref _demandVoucherDetailNSBDResult, value);
                OnPropertyChanged(nameof(_demandVoucherDetailNSBDResult));
            }
        }

        public string MHHV1 { get; set; }
        public string DT1 { get; set; }
        public string MHHV2 { get; set; }
        public string DT2 { get; set; }
        public string MHHV3 { get; set; }
        public string DT3 { get; set; }
        public string MHHV4 { get; set; }
        public string DT4 { get; set; }
        public string MHHV5 { get; set; }
        public string DT5 { get; set; }
        public string CanCuBaoDam1 { get; set; }
        public string CanCuBaoDam2 { get; set; }
        public string CanCuBaoDam3 { get; set; }
        public string CanCuBaoDam4 { get; set; }
        public string CanCuBaoDam5 { get; set; }
        public string TonkhoDenNgayCol { get; set; }
        public string KhungNganSachDuocDuyet => $"Khung ngân sách được duyệt năm {_sessionInfo.YearOfWork}";
        public string SoNganhPhanCap => $"Số ngành đã phân cấp theo khung ngân sách năm {_sessionInfo.YearOfWork}";
        public string SoNhuCauNam => $"Nhu cầu ngân sách năm {_sessionInfo.YearOfWork}";

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand AddMLSKTCommand { get; }
        public RelayCommand MergeCommand { get; }
        public RelayCommand UnmergeCommand { get; }
        public RelayCommand SaveMLSKTCommand { get; }

        public RelayCommand GetFileFtpCommandHTTP { get; }
        public RelayCommand GetFileFtpCommandFTP { get; }
        public RelayCommand DownloadFileFtpServer { get; }

        public ImportDemandViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            ISktChungTuService sktChungTuService,
            ICauHinhCanCuService iCauHinhCanCuService,
            ISktMucLucService sktMucLucService,
            FtpStorageService ftpStorageService,
            ISktChungTuChiTietCanCuService iSktChungTuChiTietCanCuService,
            ILog logger,
            IHTTPUploadFileService hTTPUploadFileService,
            ISktChungTuChiTietService sktChungTuChiTietService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _iSktChungTuChiTietCanCuService = iSktChungTuChiTietCanCuService;
            _importService = importService;
            _sktChungTuService = sktChungTuService;
            _iCauHinhCanCuService = iCauHinhCanCuService;
            _sktMucLucService = sktMucLucService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _ftpStorageService = ftpStorageService;
            _hTTPUploadFileService = hTTPUploadFileService;
            _logger = logger;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj =>
            {
                if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSSD_Key)
                {
                    OnProcessFile();
                }
                else
                {
                    OnProcessFileNSBD();
                }

            });
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            AddMLSKTCommand = new RelayCommand(obj => OnAddMLSKT());
            MergeCommand = new RelayCommand(obj => OnMerge());
            UnmergeCommand = new RelayCommand(obj => OnUnMerge());
            SaveMLSKTCommand = new RelayCommand(obj => OnSaveMLSKT());
            GetFileFtpCommandHTTP = new RelayCommand(async obj => await OnGetFileFtpCommand(true));
            GetFileFtpCommandFTP = new RelayCommand(async obj => await OnGetFileFtpCommand(false));
            DownloadFileFtpServer = new RelayCommand(async obj => await OnDownloadFileFtpServer(obj));
        }

        public void ShowColumn()
        {
            TC1Visible = Visibility.Collapsed;
            TC2Visible = Visibility.Collapsed;
            TC3Visible = Visibility.Collapsed;
            TC4Visible = Visibility.Collapsed;
            TC5Visible = Visibility.Collapsed;

            MHHV1Visible = Visibility.Collapsed;
            DT1Visible = Visibility.Collapsed;
            MHHV2Visible = Visibility.Collapsed;
            DT2Visible = Visibility.Collapsed;
            MHHV3Visible = Visibility.Collapsed;
            DT3Visible = Visibility.Collapsed;
            MHHV4Visible = Visibility.Collapsed;
            DT4Visible = Visibility.Collapsed;
            MHHV5Visible = Visibility.Collapsed;
            DT5Visible = Visibility.Collapsed;


            IsReadOnlyX1 = true;
            IsReadOnlyX2 = true;
            IsReadOnlyX3 = true;
            IsReadOnlyX4 = true;
            IsReadOnlyX5 = true;

            int loaiChungTu = 0;
            if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSSD_Key)
            {
                loaiChungTu = int.Parse(VoucherType.NSSD_Key);
            }
            else if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSBD_Key)
            {
                loaiChungTu = int.Parse(VoucherType.NSBD_Key);
            }

            int yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<NsCauHinhCanCu>();
            predicate = predicate.And(item => item.SModule == TypeModuleCanCu.DEMAND);
            predicate = predicate.And(item => item.INamLamViec == yearOfWork);
            var listCanCu = _iCauHinhCanCuService.FindByCondition(predicate).OrderBy(n => n.INamCanCu);
            var cauHinhCanCu = _mapper.Map<ObservableCollection<CauHinhCanCuModel>>(listCanCu);

            for (int i = 0; i < cauHinhCanCu.Count; i++)
            {
                if (i == 0)
                {
                    if (loaiChungTu == int.Parse(VoucherType.NSSD_Key))
                    {
                        TC1Visible = Visibility.Visible;
                        TC1 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot : "";
                    }
                    else
                    {
                        MHHV1Visible = Visibility.Visible;
                        MHHV1 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " mua hàng cấp hiện vật " : "";
                        DT1Visible = Visibility.Visible;
                        DT1 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " đặc thù " : "";
                        var iidMaChucNang = cauHinhCanCu[i] != null ? cauHinhCanCu[i].IIDMaChucNang : "-1";
                        CanCuBaoDam1 = VoucherType.TypeCanCuDict.GetValueOrDefault(iidMaChucNang, string.Empty);
                    }

                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX1 = false;
                    }
                }
                if (i == 1)
                {
                    if (loaiChungTu == int.Parse(VoucherType.NSSD_Key))
                    {
                        TC2Visible = Visibility.Visible;
                        TC2 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot : "";
                    }
                    else
                    {
                        MHHV2Visible = Visibility.Visible;
                        MHHV2 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " mua hàng cấp hiện vật " : "";
                        DT2Visible = Visibility.Visible;
                        DT2 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " đặc thù " : "";
                        var iidMaChucNang = cauHinhCanCu[i] != null ? cauHinhCanCu[i].IIDMaChucNang : "-1";
                        CanCuBaoDam2 = VoucherType.TypeCanCuDict.GetValueOrDefault(iidMaChucNang, string.Empty);
                    }
                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX2 = false;
                    }
                }
                if (i == 2)
                {
                    if (loaiChungTu == int.Parse(VoucherType.NSSD_Key))
                    {
                        TC3Visible = Visibility.Visible;
                        TC3 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot : "";
                    }
                    else
                    {
                        MHHV3Visible = Visibility.Visible;
                        MHHV3 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " mua hàng cấp hiện vật " : "";
                        DT3Visible = Visibility.Visible;
                        DT3 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " đặc thù " : "";
                        var iidMaChucNang = cauHinhCanCu[i] != null ? cauHinhCanCu[i].IIDMaChucNang : "-1";
                        CanCuBaoDam3 = VoucherType.TypeCanCuDict.GetValueOrDefault(iidMaChucNang, string.Empty);
                    }
                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX3 = false;
                    }
                }
                if (i == 3)
                {
                    if (loaiChungTu == int.Parse(VoucherType.NSSD_Key))
                    {
                        TC4Visible = Visibility.Visible;
                        TC4 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot : "";
                    }
                    else
                    {
                        MHHV4Visible = Visibility.Visible;
                        MHHV4 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " mua hàng cấp hiện vật " : "";
                        DT4Visible = Visibility.Visible;
                        DT4 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " đặc thù " : "";
                        var iidMaChucNang = cauHinhCanCu[i] != null ? cauHinhCanCu[i].IIDMaChucNang : "-1";
                        CanCuBaoDam4 = VoucherType.TypeCanCuDict.GetValueOrDefault(iidMaChucNang, string.Empty);
                    }
                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX4 = false;
                    }
                }
                if (i == 4)
                {
                    if (loaiChungTu == int.Parse(VoucherType.NSSD_Key))
                    {
                        TC5Visible = Visibility.Visible;
                        TC5 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot : "";
                    }
                    else
                    {
                        MHHV5Visible = Visibility.Visible;
                        MHHV5 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " mua hàng cấp hiện vật " : "";
                        DT5Visible = Visibility.Visible;
                        DT5 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " đặc thù " : "";
                        var iidMaChucNang = cauHinhCanCu[i] != null ? cauHinhCanCu[i].IIDMaChucNang : "-1";
                        CanCuBaoDam5 = VoucherType.TypeCanCuDict.GetValueOrDefault(iidMaChucNang, string.Empty);
                    }
                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX5 = false;
                    }
                }
            }
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            OnResetData();
            LoadVoucherTypes();
            LoadBudgetSourceTypes();
            LoadDonVi();
            NgayChungTu = DateTime.Now;
            TonkhoDenNgayCol = VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSSD_Key 
                ? "Tồn kho đến ngày 01/01/" + (_sessionService.Current.YearOfWork - 1)
                : "Giá trị hàng hóa tồn kho 01/01/" + (_sessionService.Current.YearOfWork - 1);
            ShowColumn();
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

        private void OnResetData()
        {
            _fileName = string.Empty;
            _demandVoucherDetails = new ObservableCollection<DemandVoucherDetailImportModel>();
            _importErrors = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;
            _nsSktMucLucs = _sktMucLucService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(x => x.SKyHieu).ToList();
            _mergeItems = new List<SktMucLucModel>();
            _importedMlskt = new ObservableCollection<SktMucLucModel>();
            _existedMlskt = new ObservableCollection<SktMucLucModel>(_mapper.Map<ObservableCollection<SktMucLucModel>>(_nsSktMucLucs));
            LstFile = new ObservableCollection<FileFilterQuery>();

            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(DemandVoucherDetails));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(LstFile));
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

        public void LoadDonVi()
        {
            ListDonVi = LoadNsDonVisTheoLoaiChungTu();
            ListDonVi = new ObservableCollection<ComboboxItem>(ListDonVi.GroupBy(item => item.ValueItem).Select(item => item.First()));
            if (ListDonVi != null && ListDonVi.Count > 0)
            {
                DonViSelected = ListDonVi.ElementAt(0);
            }
        }

        private ObservableCollection<ComboboxItem> LoadNsDonVisTheoLoaiChungTu()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            var iTrangThai = StatusType.ACTIVE;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var iLoai = DemandCheckType.DEMAND;
            var loaiNguonNganSach = Int32.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            IEnumerable<NsSktChungTu> listChungTu;
            listChungTu = _sktChungTuService
                .FindChungTuIndexByCondition(iLoai, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, null,
                    _sessionInfo.Principal, "sp_skt_nhap_so_nhu_cau").ToList();
            listChungTu = listChungTu.Where(x => x.ILoaiNguonNganSach == loaiNguonNganSach).ToList();
            var ListIdDonViHasCt = listChungTu.Select(item => item.IIdMaDonVi).ToList();
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x =>
                x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == "1");

            bool isDvCap4 = _donViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
            if (isDvCap4)
            {
                predicate = predicate.And(
                    x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == "0");
            }
            else
            {
                if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == "1")
                {
                    predicate = predicate.And(x =>
                        x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == "1");
                    predicate = predicate.And(x => ListIdDonViHasCt.All(y => y != x.IIDMaDonVi));
                }
                else if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == "2")
                {
                    predicate = predicate.And(x =>
                        x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == "1" &&
                        true.Equals(x.BCoNSNganh));
                    predicate = predicate.And(x => ListIdDonViHasCt.All(y => y != x.IIDMaDonVi));
                }
            }

            var listUnit = _donViService.FindByCondition(predicate).ToList();
            var listDonViByUser = _donViService
                .FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork,
                    LoaiDonVi.NOI_BO).Select(x => x.IIDMaDonVi);
            var listUnitResult = listUnit.Where(x => listDonViByUser.Contains(x.IIDMaDonVi));
            var result = listUnitResult.Select(item => new ComboboxItem()
            {
                ValueItem = item.IIDMaDonVi,
                DisplayItem = item.TenDonVi
            }).OrderBy(item => item.ValueItem);
            return new ObservableCollection<ComboboxItem>(result);
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var currentIdDonVi = _sessionService.Current.IdDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == currentIdDonVi);
            var nsDonViOfCurrentUser = _donViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private void OnUploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Chọn file excel");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            FileName = openFileDialog.FileName;
        }

        private void ShowError()
        {
            int rowIndex = _demandVoucherDetails.IndexOf(SelectedItem);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnProcessFile(bool isLoadFromServer = false)
        {
            string message = string.Empty;
            if (!isLoadFromServer && string.IsNullOrEmpty(FileName))
            {
                message = Resources.ErrorFileEmpty;
                //goto ShowError;
            }
            //ShowError:
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                int loaiChungTu = 0;
                if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSSD_Key)
                {
                    loaiChungTu = int.Parse(VoucherType.NSSD_Key);
                }
                else if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSBD_Key)
                {
                    loaiChungTu = int.Parse(VoucherType.NSBD_Key);
                }
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                //xử lý chứng từ chi tiết
                ImportResult<DemandVoucherDetailImportModelNSSD> _voucherDetailResult = isLoadFromServer ? DemandVoucherDetailNSSDResult : _importService.ProcessDataUnique<DemandVoucherDetailImportModelNSSD>(FileName);

                _demandVoucherDetailProcess = new List<DemandVoucherDetailImportModel>();

                //xác định cha con trong data import
                foreach (var item in _voucherDetailResult.Data)
                {
                    if (!string.IsNullOrEmpty(item.X1SoTien))
                    {
                        item.X1.SoTien = Convert.ToDouble(item.X1SoTien);
                    }
                    if (!string.IsNullOrEmpty(item.X2SoTien))
                    {
                        item.X2.SoTien = Convert.ToDouble(item.X2SoTien);
                    }
                    if (!string.IsNullOrEmpty(item.X3SoTien))
                    {
                        item.X3.SoTien = Convert.ToDouble(item.X3SoTien);
                    }
                    if (!string.IsNullOrEmpty(item.X4SoTien))
                    {
                        item.X4.SoTien = Convert.ToDouble(item.X4SoTien);
                    }
                    if (!string.IsNullOrEmpty(item.X5SoTien))
                    {
                        item.X5.SoTien = Convert.ToDouble(item.X5SoTien);
                    }

                    if (!item.IsErrorMLNS)
                    {
                        var mlns = _nsSktMucLucs.Where(x => x.SKyHieu == item.KyHieu).FirstOrDefault();
                        if (mlns != null)
                            item.BHangCha = mlns.BHangCha;
                    }
                    var childs = _voucherDetailResult.Data.Where(x => x.KyHieu.Contains(item.KyHieu) && x != item).ToList();
                    if (childs.Count > 0)
                        item.BHangCha = true;
                }

                //kiểm tra loại ngân sách con thỏa mãn điều kiện để import
                foreach (var item in _voucherDetailResult.Data)
                {
                    var itemChange = _mapper.Map<DemandVoucherDetailImportModel>(item);
                    if (!itemChange.ImportStatus && itemChange.IsErrorMLNS)
                    {
                        var parents = _demandVoucherDetailProcess.Where(x => item.KyHieu.Contains(x.KyHieu) && x != itemChange).ToList();
                        if (parents.Count > 0)
                        {
                            int columnIndexOrigin = 0;
                            DemandVoucherDetailImportModel parent = new DemandVoucherDetailImportModel();
                            foreach (var p in parents)
                            {
                                int maxColumn = p.KyHieu.Split("-").Count();
                                if (maxColumn > columnIndexOrigin)
                                {
                                    columnIndexOrigin = maxColumn;
                                    parent = p;
                                }
                            }
                            int columnIndexImport = item.KyHieu.Split("-").Count();
                            if (columnIndexOrigin < columnIndexImport)
                            {
                                if (parent.ListKyHieuChild == null)
                                    parent.ListKyHieuChild = new List<string>();
                                parent.ListKyHieuChild.Add(item.KyHieu);
                                item.KyHieuParent = parent.KyHieu;

                                var parentOrigin = _nsSktMucLucs.Where(x => x.SKyHieu == parent.KyHieu).FirstOrDefault();
                                if (parentOrigin != null && !parentOrigin.BHangCha)
                                {
                                    item.IsWarning = true;
                                }
                                if (parent.IsWarning)
                                    item.IsWarning = true;
                            }
                        }
                    }
                    _demandVoucherDetailProcess.Add(itemChange);
                }

                if (loaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key)))
                {
                    foreach (var item in _demandVoucherDetailProcess)
                    {
                        item.MuaHangHienVat = "";
                        item.DacThu = "";
                    }
                }
                else
                {
                    foreach (var item in _demandVoucherDetailProcess)
                    {
                        item.TuChi = "";
                        // item.HuyDong = "";
                    }
                }

                //CalculateData();
                _demandVoucherDetails = new ObservableCollection<DemandVoucherDetailImportModel>(_demandVoucherDetailProcess);
                foreach (var item in _demandVoucherDetails)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName != nameof(DemandVoucherDetailImportModel.ImportStatus)
                            //&& args.PropertyName != nameof(DemandVoucherDetailImportModel.KyHieu)
                            && args.PropertyName != nameof(DemandVoucherDetailImportModel.IsErrorMLNS))
                        {
                            var voucherDetail = (DemandVoucherDetailImportModel)sender;
                            int rowIndex = _demandVoucherDetails.IndexOf(voucherDetail);
                            var listError = _importService.ValidateItem<DemandVoucherDetailImportModel>(voucherDetail, rowIndex);
                            if (listError.Count > 0)
                            {
                                List<string> errors = listError.Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                                string message = string.Join(Environment.NewLine, errors);
                                System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
                                _importErrors.AddRange(listError);
                                voucherDetail.ImportStatus = false;
                                if (listError.Any(x => x.IsErrorMLNS))
                                    voucherDetail.IsErrorMLNS = true;
                            }
                            else
                            {
                                voucherDetail.ImportStatus = true;
                                voucherDetail.IsErrorMLNS = false;
                                _importErrors.RemoveAll(x => x.Row == rowIndex);
                                OnPropertyChanged(nameof(IsSaveData));
                            }
                        }
                    };
                }

                OnPropertyChanged(nameof(DemandVoucherDetails));
                if (_voucherDetailResult.ImportErrors.Count > 0)
                    errors.AddRange(_voucherDetailResult.ImportErrors);

                if (_demandVoucherDetails == null || _demandVoucherDetails.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (DemandVoucherDetails.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                if (errors.Count > 0)
                {
                    _importErrors = new ObservableCollection<ImportErrorItem>(errors).ToList();
                    OnPropertyChanged(nameof(ImportErrors));
                }
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                _logger.Error("The report form is not in the correct format from the software!");
                if (ex is WrongReportException)
                {
                    System.Windows.MessageBox.Show(Resources.WrongReportFormat, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void OnProcessFileNSBD(bool isLoadFromServer = false)
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(FileName))
            {
                message = Resources.ErrorFileEmpty;
                goto ShowError;
            }
        ShowError:
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                int loaiChungTu = 0;
                if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSSD_Key)
                {
                    loaiChungTu = int.Parse(VoucherType.NSSD_Key);
                }
                else if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSBD_Key)
                {
                    loaiChungTu = int.Parse(VoucherType.NSBD_Key);
                }
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                //xử lý chứng từ chi tiết
                ImportResult<DemandVoucherDetailImportModelNSBD> _voucherDetailResult = isLoadFromServer ? DemandVoucherDetailNSBDResult : _importService.ProcessDataUnique<DemandVoucherDetailImportModelNSBD>(FileName);

                _demandVoucherDetailProcess = new List<DemandVoucherDetailImportModel>();

                //xác định cha con trong data import
                foreach (var item in _voucherDetailResult.Data)
                {
                    if (!string.IsNullOrEmpty(item.X1SoTienMHHV))
                    {
                        item.X1.SoTienMHHV = Convert.ToDouble(item.X1SoTienMHHV);
                    }
                    if (!string.IsNullOrEmpty(item.X1SoTienDT))
                    {
                        item.X1.SoTienDT = Convert.ToDouble(item.X1SoTienDT);
                    }
                    if (!string.IsNullOrEmpty(item.X2SoTienMHHV))
                    {
                        item.X2.SoTienMHHV = Convert.ToDouble(item.X2SoTienMHHV);
                    }
                    if (!string.IsNullOrEmpty(item.X2SoTienDT))
                    {
                        item.X2.SoTienDT = Convert.ToDouble(item.X2SoTienDT);
                    }
                    if (!string.IsNullOrEmpty(item.X3SoTienMHHV))
                    {
                        item.X3.SoTienMHHV = Convert.ToDouble(item.X3SoTienMHHV);
                    }
                    if (!string.IsNullOrEmpty(item.X3SoTienDT))
                    {
                        item.X3.SoTienDT = Convert.ToDouble(item.X3SoTienDT);
                    }
                    if (!string.IsNullOrEmpty(item.X4SoTienMHHV))
                    {
                        item.X4.SoTienMHHV = Convert.ToDouble(item.X4SoTienMHHV);
                    }
                    if (!string.IsNullOrEmpty(item.X4SoTienDT))
                    {
                        item.X4.SoTienDT = Convert.ToDouble(item.X4SoTienDT);
                    }
                    if (!string.IsNullOrEmpty(item.X5SoTienMHHV))
                    {
                        item.X5.SoTienMHHV = Convert.ToDouble(item.X5SoTienMHHV);
                    }
                    if (!string.IsNullOrEmpty(item.X5SoTienDT))
                    {
                        item.X5.SoTienDT = Convert.ToDouble(item.X5SoTienDT);
                    }

                    if (!item.IsErrorMLNS)
                    {
                        var mlns = _nsSktMucLucs.Where(x => x.SKyHieu == item.KyHieu).FirstOrDefault();
                        if (mlns != null)
                            item.BHangCha = mlns.BHangCha;
                    }
                    var childs = _voucherDetailResult.Data.Where(x => x.KyHieu.Contains(item.KyHieu) && x != item).ToList();
                    if (childs.Count > 0)
                        item.BHangCha = true;
                }

                //kiểm tra loại ngân sách con thỏa mãn điều kiện để import
                foreach (var item in _voucherDetailResult.Data)
                {
                    var itemChange = _mapper.Map<DemandVoucherDetailImportModel>(item);
                    if (!itemChange.ImportStatus && itemChange.IsErrorMLNS)
                    {
                        var parents = _demandVoucherDetailProcess.Where(x => item.KyHieu.Contains(x.KyHieu) && x != itemChange).ToList();
                        if (parents.Count > 0)
                        {
                            int columnIndexOrigin = 0;
                            DemandVoucherDetailImportModel parent = new DemandVoucherDetailImportModel();
                            foreach (var p in parents)
                            {
                                int maxColumn = p.KyHieu.Split("-").Count();
                                if (maxColumn > columnIndexOrigin)
                                {
                                    columnIndexOrigin = maxColumn;
                                    parent = p;
                                }
                            }
                            int columnIndexImport = item.KyHieu.Split("-").Count();
                            if (columnIndexOrigin < columnIndexImport)
                            {
                                if (parent.ListKyHieuChild == null)
                                    parent.ListKyHieuChild = new List<string>();
                                parent.ListKyHieuChild.Add(item.KyHieu);
                                item.KyHieuParent = parent.KyHieu;

                                var parentOrigin = _nsSktMucLucs.Where(x => x.SKyHieu == parent.KyHieu).FirstOrDefault();
                                if (parentOrigin != null && !parentOrigin.BHangCha)
                                {
                                    item.IsWarning = true;
                                }
                                if (parent.IsWarning)
                                    item.IsWarning = true;
                            }
                        }
                    }
                    _demandVoucherDetailProcess.Add(itemChange);
                }

                if (loaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key)))
                {
                    foreach (var item in _demandVoucherDetailProcess)
                    {
                        item.MuaHangHienVat = "";
                        item.DacThu = "";
                    }
                }
                else
                {
                    foreach (var item in _demandVoucherDetailProcess)
                    {
                        item.TuChi = "";
                        // item.HuyDong = "";
                    }
                }

                //CalculateData();
                //_demandVoucherDetails = new ObservableCollection<DemandVoucherDetailImportModel>(_demandVoucherDetailProcessNSBD);
                _demandVoucherDetails = _mapper.Map<ObservableCollection<DemandVoucherDetailImportModel>>(_demandVoucherDetailProcess);
                foreach (var item in _demandVoucherDetails)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName != nameof(DemandVoucherDetailImportModel.ImportStatus)
                            //&& args.PropertyName != nameof(DemandVoucherDetailImportModel.KyHieu)
                            && args.PropertyName != nameof(DemandVoucherDetailImportModel.IsErrorMLNS))
                        {
                            var voucherDetail = (DemandVoucherDetailImportModel)sender;
                            int rowIndex = _demandVoucherDetails.IndexOf(voucherDetail);
                            var listError = _importService.ValidateItem<DemandVoucherDetailImportModel>(voucherDetail, rowIndex);
                            if (listError.Count > 0)
                            {
                                List<string> errors = listError.Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                                string message = string.Join(Environment.NewLine, errors);
                                System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
                                _importErrors.AddRange(listError);
                                voucherDetail.ImportStatus = false;
                                if (listError.Any(x => x.IsErrorMLNS))
                                    voucherDetail.IsErrorMLNS = true;
                            }
                            else
                            {
                                voucherDetail.ImportStatus = true;
                                voucherDetail.IsErrorMLNS = false;
                                _importErrors.RemoveAll(x => x.Row == rowIndex);
                                OnPropertyChanged(nameof(IsSaveData));
                            }
                        }
                    };
                }

                OnPropertyChanged(nameof(DemandVoucherDetails));
                if (_voucherDetailResult.ImportErrors.Count > 0)
                    errors.AddRange(_voucherDetailResult.ImportErrors);

                if (_demandVoucherDetails == null || _demandVoucherDetails.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (DemandVoucherDetails.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                if (errors.Count > 0)
                {
                    _importErrors = new ObservableCollection<ImportErrorItem>(errors).ToList();
                    OnPropertyChanged(nameof(ImportErrors));
                }
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
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
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var soChungTuIndex = _sktChungTuService.GetSoChungTuIndexByCondition(DemandCheckType.DEMAND.ToString(),
                _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget);
            var SoChungTu = "SNC-" + soChungTuIndex.ToString("D3");
            var NamLamViec = _sessionInfo.YearOfWork;
            int loaiChungTu = 0;
            var loaiNguonNganSach = Int32.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");

            if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSSD_Key)
            {
                loaiChungTu = int.Parse(VoucherType.NSSD_Key);
            }
            else if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSBD_Key)
            {
                loaiChungTu = int.Parse(VoucherType.NSBD_Key);
            }

            var chungTuNhuCau = _sktChungTuService.FindByCondition(PredicateBuilder.True<NsSktChungTu>().And(i =>
                i.INamLamViec == NamLamViec && i.ILoai == DemandCheckType.DEMAND &&
                i.ILoaiChungTu == loaiChungTu && i.IIdMaDonVi == DonViSelected.ValueItem &&
                i.ILoaiNguonNganSach == loaiNguonNganSach));

            if (chungTuNhuCau.Count() > 0)
            {
                System.Windows.MessageBox.Show(string.Format(Resources.VoucherIsExists, VoucherTypeSelected.DisplayItem, DonViSelected.DisplayItem), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _nsSktMucLucs = _sktMucLucService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork).ToList();
            NsSktChungTu chungTu = new NsSktChungTu();
            chungTu.SSoChungTu = SoChungTu;
            chungTu.ISoChungTuIndex = soChungTuIndex;
            chungTu.IIdMaDonVi = DonViSelected.ValueItem;
            chungTu.STenDonVi = DonViSelected.DisplayItem;
            chungTu.DNgayChungTu = NgayChungTu == null ? DateTime.Now : NgayChungTu;
            chungTu.SMoTa = MoTa;
            chungTu.ILoai = DemandCheckType.DEMAND;
            chungTu.INamLamViec = NamLamViec;
            chungTu.INamNganSach = yearOfBudget;
            chungTu.IIdMaNguonNganSach = budgetSource;
            chungTu.DNgayTao = DateTime.Now;
            chungTu.SNguoiTao = _sessionInfo.Principal;
            chungTu.ILoaiChungTu = loaiChungTu;
            chungTu.ILoaiNguonNganSach = int.Parse(BudgetSourceTypeSelected.ValueItem);

            _sktChungTuService.Add(chungTu);

            List<NsSktChungTuChiTiet> chungTuChiTiets = new List<NsSktChungTuChiTiet>();
            List<DemandVoucherDetailImportModel> listDetailImport = _demandVoucherDetails.Where(x => x.ImportStatus && !x.IsWarning && _nsSktMucLucs.Any(y => y.SKyHieu == x.KyHieu && !y.BHangCha)).ToList();
            foreach (var item in listDetailImport)
            {
                NsSktMucLuc mucLuc = _nsSktMucLucs.FirstOrDefault((x => x.SKyHieu.Equals(item.KyHieu) && x.INamLamViec == NamLamViec && x.ITrangThai == StatusType.ACTIVE));
                if (item.KyHieu.Equals("") || mucLuc == null)
                {
                    continue;
                }
                NsSktChungTuChiTiet nsSktChiTiet = new NsSktChungTuChiTiet();
                nsSktChiTiet.IIdCtsoKiemTra = chungTu.Id;
                nsSktChiTiet.IIdMaDonVi = DonViSelected.ValueItem;
                nsSktChiTiet.STenDonVi = DonViSelected.DisplayItem;
                nsSktChiTiet.IIdMlskt = mucLuc.IIDMLSKT;
                nsSktChiTiet.SMoTa = mucLuc.SMoTa;
                nsSktChiTiet.SKyHieu = mucLuc.SKyHieu;
                nsSktChiTiet.ILoai = DemandCheckType.DEMAND;
                nsSktChiTiet.INamLamViec = NamLamViec;
                nsSktChiTiet.DNgayTao = DateTime.Now;
                nsSktChiTiet.ILoaiChungTu = chungTu.ILoaiChungTu;
                nsSktChiTiet.INamNganSach = yearOfBudget;
                nsSktChiTiet.IIdMaNguonNganSach = budgetSource;
                nsSktChiTiet.FHuyDongTonKho = string.IsNullOrEmpty(item.HuyDong) ? 0 : Double.Parse(item.HuyDong);
                nsSktChiTiet.FKhungNganSachDuocDuyet = string.IsNullOrEmpty(item.KhungNganSachDuocDuyet) ? 0 : Double.Parse(item.KhungNganSachDuocDuyet);
                nsSktChiTiet.FSoNganhPhanCap = string.IsNullOrEmpty(item.SoNganhPhanCap) ? 0 : Double.Parse(item.SoNganhPhanCap);
                nsSktChiTiet.FTuChi = string.IsNullOrEmpty(item.TuChi) ? 0 : Double.Parse(item.TuChi);
                nsSktChiTiet.FTonKhoDenNgay = string.IsNullOrEmpty(item.TonkhoDenNgay) ? 0 : Double.Parse(item.TonkhoDenNgay);
                nsSktChiTiet.FTuChiDeNghi = string.IsNullOrEmpty(item.TuChi) ? 0 : Double.Parse(item.TuChi);
                nsSktChiTiet.FPhanCap = string.IsNullOrEmpty(item.DacThu) ? 0 : Double.Parse(item.DacThu);
                nsSktChiTiet.FMuaHangCapHienVat = string.IsNullOrEmpty(item.MuaHangHienVat) ? 0 : Double.Parse(item.MuaHangHienVat);
                nsSktChiTiet.SNguoiTao = _sessionInfo.Principal;
                chungTuChiTiets.Add(nsSktChiTiet);
            }
            _sktChungTuChiTietService.AddRange(chungTuChiTiets);
            if (chungTuChiTiets.Count > 0)
            {
                chungTu.FTongTuChi = chungTuChiTiets.Sum(item => item.FTuChi);
                chungTu.FTongPhanCap = chungTuChiTiets.Sum(item => item.FPhanCap);
                chungTu.FTongMuaHangCapHienVat = chungTuChiTiets.Sum(item => item.FMuaHangCapHienVat);
                _sktChungTuService.Update(chungTu);
            }
            MessageBoxResult dialogResult = MessageBoxHelper.Confirm("Lưu dữ liệu căn cứ trên file excel?");
            if (dialogResult == MessageBoxResult.Yes)
            {
                int yearOfWork = _sessionInfo.YearOfWork;
                var predicate = PredicateBuilder.True<NsCauHinhCanCu>();
                predicate = predicate.And(item => item.SModule == TypeModuleCanCu.DEMAND);
                predicate = predicate.And(item => item.INamLamViec == yearOfWork);
                var listCanCu = _iCauHinhCanCuService.FindByCondition(predicate).OrderBy(n => n.INamCanCu);
                var cauHinhCanCu = _mapper.Map<ObservableCollection<CauHinhCanCuModel>>(listCanCu);

                List<NsSktChungTuChiTietCanCu> listCanCus = new List<NsSktChungTuChiTietCanCu>();

                for (int i = 0; i < cauHinhCanCu.Count; i++)
                {
                    var withoutParent = DemandVoucherDetails.Where(n => !n.BHangCha).ToList();
                    foreach (var item in withoutParent)
                    {
                        NsSktMucLuc mucLuc = _nsSktMucLucs.FirstOrDefault((x => x.SKyHieu.Equals(item.KyHieu) && x.INamLamViec == NamLamViec && x.ITrangThai == StatusType.ACTIVE));
                        var predicateCT = PredicateBuilder.True<NsSktChungTuChiTietCanCu>();
                        predicateCT = predicateCT.And(x => x.IiIdCtsoKiemTra == chungTu.Id);
                        predicateCT = predicateCT.And(x => x.IIdCanCu == cauHinhCanCu[i].Id);
                        predicateCT = predicateCT.And(x => x.SKyHieu == item.KyHieu);
                        predicateCT = predicateCT.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                        var sktcanCanCus = _iSktChungTuChiTietCanCuService.FindByCondition(predicateCT).ToList();
                        _iSktChungTuChiTietCanCuService.RemoveRange(sktcanCanCus);

                        NsSktChungTuChiTietCanCu canCuChungTu = new NsSktChungTuChiTietCanCu();
                        canCuChungTu.IiIdCtsoKiemTra = chungTu.Id;
                        canCuChungTu.SKyHieu = item.KyHieu;
                        canCuChungTu.IIdMlskt = mucLuc.IIDMLSKT;
                        canCuChungTu.IIdCanCu = cauHinhCanCu[i].Id;
                        canCuChungTu.INamLamViec = _sessionInfo.YearOfWork;
                        if (item.X1 != null && i == 0)
                        {
                            canCuChungTu.FTuChi = item.X1.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X1.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X1.SoTienDT;
                        }

                        if (item.X2 != null && i == 1)
                        {
                            canCuChungTu.FTuChi = item.X2.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X2.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X2.SoTienDT;
                        }

                        if (item.X3 != null && i == 2)
                        {
                            canCuChungTu.FTuChi = item.X3.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X3.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X3.SoTienDT;
                        }

                        if (item.X4 != null && i == 3)
                        {
                            canCuChungTu.FTuChi = item.X4.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X4.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X4.SoTienDT;
                        }

                        if (item.X5 != null && i == 4)
                        {
                            canCuChungTu.FTuChi = item.X5.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X5.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X5.SoTienDT;
                        }

                        listCanCus.Add(canCuChungTu);
                    }
                }

                _iSktChungTuChiTietCanCuService.AddRange(listCanCus);

            }
            NsSktChungTuModel demandVoucher = _mapper.Map<NsSktChungTuModel>(chungTu);
            DialogHost.CloseDialogCommand.Execute(demandVoucher, null);
            SavedAction?.Invoke(demandVoucher);

        }


        private void OnAddMLSKT()
        {
            TabIndex = ImportTabIndex.MLNS;
            SktMucLucModel importItem = new SktMucLucModel();
            if (ImportedMlskt.Contains(importItem))
                return;

            bool isImportGroup = false;
            if (!_importedMlskt.Any(x => x.SKyHieu.Contains(SelectedItem.KyHieu))
                || !_existedMlskt.Any(x => x.SKyHieu.Contains(SelectedItem.KyHieu)))
            {
                List<DemandVoucherDetailImportModel> data = new List<DemandVoucherDetailImportModel>();
                data.Add(SelectedItem);
                if (SelectedItem.BHangCha)
                    data.AddRange(_demandVoucherDetailProcess.Where(x => SelectedItem.ListKyHieuChild.Contains(x.KyHieu)).ToList());
                else
                {
                    var it = _demandVoucherDetailProcess.Where(x => x.KyHieu == SelectedItem.KyHieuParent)
                        .FirstOrDefault();
                    if (it != null)
                    {
                        data.Add(it);
                    }
                }
                if (data.Count > 1)
                    isImportGroup = true;
                data = data.OrderBy(x => x.KyHieu).ToList();
                foreach (var item in data)
                {
                    if (!_nsSktMucLucs.Any(x => x.SKyHieu == item.KyHieu))
                        _importedMlskt.Add(new SktMucLucModel()
                        {
                            IIDMLSKT = Guid.NewGuid(),
                            SKyHieu = item.KyHieu,
                            SNg = item.Nganh,
                            SNGCha = item.NganhCha,
                            SSTT = item.STT,
                            SMoTa = item.Description,
                            BHangCha = item.BHangCha,
                            ITrangThai = StatusType.ACTIVE,
                            INamLamViec = _sessionInfo.YearOfWork,
                            SM = "",
                            SLoaiNhap = "1,2",
                            IsModified = true
                        });
                }
            }
            foreach (SktMucLucModel model in _importedMlskt.ToList())
            {
                SktMucLucModel parent = null;
                if (isImportGroup && !model.BHangCha)
                    parent = _importedMlskt.Where(x => model.SKyHieu.Contains(x.SKyHieu) && model.SKyHieu != x.SKyHieu).FirstOrDefault();
                if (parent == null)
                    parent = FindParent(model, _existedMlskt);
                if (parent != null)
                {
                    int index = _existedMlskt.IndexOf(parent);
                    _existedMlskt.Insert(index + 1, model);
                    //_importedMlns.Remove(model);
                    model.IIDMLSKTCha = parent.IIDMLSKT;
                    model.BHangCha = model.BHangCha;
                    model.ITrangThai = 1;
                    model.DNguoiTao = _sessionInfo.Principal;
                    model.SM = "";
                    model.DNgayTao = DateTime.Now;
                    _mergeItems.Add(model);
                    OnPropertyChanged(nameof(IsEnableSaveMLSKT));
                }
            }
            _importedMlskt = new ObservableCollection<SktMucLucModel>();
            OnPropertyChanged(nameof(ExistedMlskt));
            OnPropertyChanged(nameof(ImportedMlskt));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnSelectionChanged();
        }

        private void OnSelectionChanged()
        {
            foreach (var i in _existedMlskt)
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
            foreach (var i in _importedMlskt.Where(x => x.IsModified))
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

        public SktMucLucModel FindParent(SktMucLucModel model, IEnumerable<SktMucLucModel> ExistedMlns)
        {
            IEnumerable<SktMucLucModel> ancestors = _existedMlskt.Where(i => !Guid.Empty.Equals(i.Id) && !model.SKyHieu.Equals(i.SKyHieu) &&
                                                                            model.SKyHieu.StartsWith(i.SKyHieu + "-") && model.INamLamViec == i.INamLamViec)
                .OrderByDescending(i => i.SKyHieu.Length);
            return ancestors.FirstOrDefault();
        }

        private void OnMerge()
        {
            if (SelectedParent == null)
                return;
            int index = _existedMlskt.ToList().FindIndex(x => x.IsSelected);
            _mergeItems = _importedMlskt.Where(i => i.IsSelected && i.IsModified).ToList();
            foreach (var item in _mergeItems)
            {
                item.IIDMLSKTCha = SelectedParent.IIDMLSKT;
                item.BHangCha = false;
                item.ITrangThai = 1;
                item.DNguoiTao = _sessionInfo.Principal;
                item.DNgayTao = DateTime.Now;
            }

            List<SktMucLucModel> nsMuclucSktModels = _existedMlskt.ToList();
            nsMuclucSktModels.InsertRange(index + 1, _mergeItems);
            _existedMlskt = new ObservableCollection<SktMucLucModel>(nsMuclucSktModels);
            _importedMlskt = new ObservableCollection<SktMucLucModel>(ImportedMlskt.Where(i => !i.IsSelected || !i.IsModified));
            OnPropertyChanged(nameof(ExistedMlskt));
            OnPropertyChanged(nameof(ImportedMlskt));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnPropertyChanged(nameof(IsEnableSaveMLSKT));
            OnSelectionChanged();
        }

        private void OnUnMerge()
        {
            IEnumerable<SktMucLucModel> unmergeItems = _existedMlskt.Where(i => i.IsSelected && i.IsModified).ToList();
            foreach (var item in unmergeItems)
            {
                _mergeItems.Remove(item);
            }
            List<SktMucLucModel> nsMuclucSktModels = ImportedMlskt.ToList();
            nsMuclucSktModels.AddRange(unmergeItems);
            _importedMlskt = new ObservableCollection<SktMucLucModel>(nsMuclucSktModels);
            _existedMlskt = new ObservableCollection<SktMucLucModel>(_existedMlskt.Where(i => !i.IsSelected || !i.IsModified));
            OnPropertyChanged(nameof(ExistedMlskt));
            OnPropertyChanged(nameof(ImportedMlskt));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnPropertyChanged(nameof(IsEnableSaveMLSKT));
            OnSelectionChanged();
        }

        private void OnSaveMLSKT()
        {
            var result = System.Windows.MessageBox.Show(Resources.ConfirmAddMLNS, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    List<NsSktMucLuc> listMLSKT = _mapper.Map<List<NsSktMucLuc>>(_mergeItems);
                    _sktMucLucService.AddRange(listMLSKT);
                    _existedMlskt.Where(x => x.IsModified).Select(x => { x.IsModified = false; x.IsSelected = false; return x; }).ToList();
                    _mergeItems = new List<SktMucLucModel>();
                    OnPropertyChanged(nameof(ExistedMlskt));
                    OnPropertyChanged(nameof(IsEnableSaveMLSKT));
                    System.Windows.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);

                    foreach (var item in listMLSKT)
                    {
                        var importItem = _demandVoucherDetails.Where(x => x.KyHieu == item.SKyHieu).FirstOrDefault();
                        var listError = _importService.ValidateItem<DemandVoucherDetailImportModel>(importItem, _demandVoucherDetails.IndexOf(importItem));
                        if (listError.Count == 0)
                        {
                            importItem.ImportStatus = true;
                            importItem.IsErrorMLNS = false;
                            TabIndex = ImportTabIndex.Data;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(Resources.MsgSaveError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private async Task OnGetFileFtpCommand(bool isSendHTTP)
        {
            IsSendHTTP = isSendHTTP;
            if (VoucherTypeSelected == null || DonViSelected == null)
            {

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng nhập đầy đủ dữ liệu");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                return;
            }
            IsLoading = true;

            var fileFilter = new FileFilterModel();
            fileFilter.AgencyCode = DonViSelected != null ? DonViSelected.ValueItem : null;
            fileFilter.Module = "BUDGET";
            fileFilter.SubModule = NSFunctionCode.BUDGET_DEMANDCHECK_DEMAND;
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
                IsLoading = false;
                return;
            }
            LstFile = new ObservableCollection<FileFilterQuery>(lst);
            IsLoading = false;

            //if (VoucherTypeSelected == null || DonViSelected == null)
            //{

            //    StringBuilder messageBuilder = new StringBuilder();
            //    messageBuilder.AppendFormat("Vui lòng nhập đầy đủ dữ liệu");
            //    System.Windows.MessageBox.Show(messageBuilder.ToString());
            //    return;
            //}
            //var btmTenDonVi = StringUtils.UCS2Convert(DonViSelected.ValueItem) + "-" + StringUtils.UCS2Convert(DonViSelected.DisplayItem).Replace("---", "-");
            //var btm = Convert.ToInt32(StringUtils.UCS2Convert(VoucherTypeSelected.ValueItem)) == 1 ? StringUtils.UCS2Convert(VoucherType.NSSD_Value) : StringUtils.UCS2Convert(VoucherType.NSBD_Value);
            //string sTime = string.Format("{0}", btm);
            //var strUrl = string.Format("{0}/{1}/{2}", btmTenDonVi, ConstantUrlPathPhanHe.UrlQlnsNcdvformSend, sTime);
            //var lstData = _ftpStorageService.GetFileServerFtp(strUrl);
            //if (lstData == null || lstData.Count == 0)
            //{
            //    StringBuilder messageBuilder = new StringBuilder();
            //    messageBuilder.AppendFormat("Không tìm thấy dữ liệu");
            //    System.Windows.MessageBox.Show(messageBuilder.ToString());
            //    return;
            //}
            //LstFile = new ObservableCollection<FileFtpModel>(lstData);
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
                    if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSSD_Key)
                    {
                        DemandVoucherDetailNSSDResult = _importService.ProcessDataUnique<DemandVoucherDetailImportModelNSSD>(fileStream, file.FileTokenKey);
                        OnProcessFile(true);
                    }
                    else
                    {
                        DemandVoucherDetailNSBDResult = _importService.ProcessDataUnique<DemandVoucherDetailImportModelNSBD>(fileStream, file.FileTokenKey);
                        OnProcessFileNSBD(true);
                    }
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
        //    foreach (var item in LstFile)
        //    {
        //        if (item.BIsCheck)
        //        {
        //            urlUrIDownLoad = item.SUrl;
        //            fileName = item.SNameFile;
        //            string filePath = _ftpStorageService.DowLoadFileFtpGiveLocal(urlUrIDownLoad, fileName);
        //            FileName = filePath;
        //        }
        //    }
        //    BackgroundWorkerHelper.Run((s, e) =>
        //    {
        //        OnProcessFile();

        //    }, (s, e) =>
        //    {
        //        IsLoading = false;
        //    });
        //}


        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}
