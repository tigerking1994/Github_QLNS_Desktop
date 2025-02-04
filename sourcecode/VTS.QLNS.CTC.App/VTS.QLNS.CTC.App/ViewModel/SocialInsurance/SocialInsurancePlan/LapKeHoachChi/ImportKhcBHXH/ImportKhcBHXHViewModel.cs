using AutoMapper;
using FlexCel.XlsAdapter;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.ImportKhcBHXH
{
    public class ImportKhcBHXHViewModel : ViewModelBase
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IBhKhcCheDoBhXhService _khcBHXHService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhKhcCheDoBhXhChiTietService _khcBHXHChiTietService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        #endregion

        #region Property
        private SessionInfo _sessionInfo;
        private bool _isUploadFile;
        public override string Name => "Kế hoạch chi chế độ BHXH";
        public override Type ContentType => typeof(VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.ImportKhcBHXH.ImportKhcBHXH);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private Dictionary<Guid, BhKhcCheDoBhXhChiTiet> _dicKhcCheDoBhXhChiTiet;
        private List<BhDmMucLucNganSach> _lstMuLucNSBHXH;
        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
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
        private List<BhDmMucLucNganSach> _nsBhxhMucLucs;
        private KhcBhxhDetailImportModel _selectedItem;
        public KhcBhxhDetailImportModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        private List<BhDmMucLucNganSachModel> _mergeItems;
        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }
        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        public bool _isSelectedFile;
        public bool IsSelectedFile
        {
            get => !string.IsNullOrEmpty(_filePath);
            set => SetProperty(ref _isSelectedFile, value);
        }

        private bool _isSaveData;
        public bool IsSaveData
        {
            get
            {
                _isSaveData = ((Items != null && Items.Count > 0) && SelectedDonVi != null && !_importErrors.Any());
                return _isSaveData;
            }
            set
            {
                SetProperty(ref _isSaveData, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private string _sSoChungTu;
        public string SSoChungTu
        {
            get => _sSoChungTu;
            set => SetProperty(ref _sSoChungTu, value);
        }

        private DateTime? _dNgayChungTu;
        public DateTime? DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);


        }

        private ObservableCollection<ComboboxItem> _listDonVi = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }
        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
                if (value != null)
                {
                    OnPropertyChanged(nameof(IsSaveData));
                }
            }
        }
        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }
        private ObservableCollection<KhcBhxhDetailImportModel> _items;
        public ObservableCollection<KhcBhxhDetailImportModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));
            }
        }


        private ObservableCollection<KhcBhxhDetailImportModel> _itemsImport;
        public ObservableCollection<KhcBhxhDetailImportModel> ItemsImport
        {
            get => _itemsImport;
            set
            {
                SetProperty(ref _itemsImport, value);

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

        public string MoTa { get; set; }
        #endregion

        #region RelayCommand
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand DownloadTemplateCommand { get; }
        public RelayCommand CheckFileImport { get; }
        #endregion

        #region Constructor
        public ImportKhcBHXHViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            IBhKhcCheDoBhXhService khcCheDoBhXhService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog logger,
            IBhKhcCheDoBhXhChiTietService bhKhcCheDoBhXhChiTietService
            )
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _importService = importService;
            _mapper = mapper;
            _khcBHXHService = khcCheDoBhXhService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _khcBHXHChiTietService = bhKhcCheDoBhXhChiTietService;
            _logger = logger;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            DownloadTemplateCommand = new RelayCommand(obj => OnDownloadTemplate());
            CheckFileImport = new RelayCommand(obj => OnCheckFileImport());
        }
        #endregion

        #region Init
        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadDonVi();
            SSoChungTu = GetSoChungTu();
            DNgayChungTu = DateTime.Now;
            OnResetData();
        }
        #endregion

        #region Reset Data
        private void OnResetData()
        {
            _filePath = string.Empty;
            _mergeItems = new List<BhDmMucLucNganSachModel>();
            _importErrors = new List<ImportErrorItem>();
            ItemsImport = new ObservableCollection<KhcBhxhDetailImportModel>();
            Items = new ObservableCollection<KhcBhxhDetailImportModel>();
            _lstMuLucNSBHXH = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(_sessionInfo.YearOfWork, LNSValue.LNS_9010001_9010002);
            int yearOfWork = _sessionService.Current.YearOfWork;
            System.Linq.Expressions.Expression<Func<BhDmMucLucNganSach, bool>> predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => x.SLNS == KhcBhxhMLNS.KHOI_DU_TOAN || x.SLNS == KhcBhxhMLNS.KHOI_HACH_TOAN).OrderBy(x => x.SXauNoiMa).ToList();
            _importedMlns = new ObservableCollection<BhDmMucLucNganSachModel>();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));
            _tabIndex = ImportTabIndex.Data;
            LstFile = new ObservableCollection<FileFtpModel>();
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsSelectedFile));
        }
        #endregion

        #region Load data
        private void LoadDonVi()
        {
            ItemsDonVi = new ObservableCollection<ComboboxItem>();
            IEnumerable<DonVi> listDonVi = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork);
            if (listDonVi != null)
            {
                //listDonVi = listDonVi.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                ItemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.TenDonVi,
                    ValueItem = n.IIDMaDonVi,
                    HiddenValue = n.Loai,
                    Id = n.Id
                }));

                SelectedDonVi = ItemsDonVi.ElementAt(0);
            }

            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void HandleData()
        {

            try
            {
                string fileExtension = Path.GetExtension(FilePath).ToLower();
                if (!(fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx")))
                {
                    System.Windows.MessageBox.Show(Resources.FileImportWrongExtensionExcel, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_importErrors != null && _importErrors.Any())
                {
                    _importErrors = new List<ImportErrorItem>();

                    foreach (KhcBhxhDetailImportModel item in Items)
                    {
                        BhDmMucLucNganSach mucLuc = _lstMuLucNSBHXH.FirstOrDefault(x => x.SXauNoiMa.Equals(item.SXauNoiMa) && x.SMoTa.Equals(item.STenMLNS));
                        if (mucLuc != null)
                        {
                            item.BHangCha = mucLuc.BHangCha;
                            item.IsHangCha = mucLuc.BHangCha;
                        }

                        int rowIndex = Items.IndexOf(item);
                        IEnumerable<ImportErrorItem> listError = ValidateItem(item, rowIndex);

                        if (listError.Any())
                        {
                            //var messageOfRow = listError.Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).FirstOrDefault();
                            _importErrors.AddRange(listError);
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
                            _importErrors = _importErrors.Where(x => x.Row != rowIndex).ToList();
                        }
                    }
                    if (_importErrors != null && _importErrors.Any())
                    {
                        _isUploadFile = false;
                        IEnumerable<string> messageOfRow = _importErrors.Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).Distinct();
                        System.Windows.MessageBox.Show(string.Join(Environment.NewLine, messageOfRow), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    OnPropertyChanged(nameof(IsSaveData));
                }
                else
                {
                    if (Items.Count > 0 && !_importErrors.Any() && !_isUploadFile)
                    {
                        OnPropertyChanged(nameof(IsSaveData));
                        return;
                    }

                    List<Guid> _lstIdBhxhChiTiet = new List<Guid>();
                    List<Core.Domain.Query.BhKhcCheDoBhXhQuery> lstChungTu = _khcBHXHService.FindIndex().Where(x => x.INamLamViec == _sessionInfo.YearOfWork).ToList();
                    _isUploadFile = false;
                    bool isExist;
                    IEnumerable<BhKhcCheDoBhXhChiTiet> BhxhChiTiet = _khcBHXHChiTietService.FindAll(n => 1 == 1);
                    _dicKhcCheDoBhXhChiTiet = new Dictionary<Guid, BhKhcCheDoBhXhChiTiet>();
                    XlsFile xls = new XlsFile(false);
                    xls.Open(FilePath);
                    xls.ActiveSheet = 1;

                    if (BhxhChiTiet != null)
                    {
                        foreach (BhKhcCheDoBhXhChiTiet item in BhxhChiTiet)
                        {
                            if (!_lstIdBhxhChiTiet.Contains(item.Id))
                            {
                                _lstIdBhxhChiTiet.Add(item.Id);
                            }
                        }
                    }
                    _importErrors = new List<ImportErrorItem>();

                    ImportResult<KhcBhxhDetailImportModel> dataImport = _importService.ProcessData<KhcBhxhDetailImportModel>(FilePath);
                    ItemsImport = new ObservableCollection<KhcBhxhDetailImportModel>(dataImport.Data);

                    List<string> lstError = new List<string>();

                    if (dataImport.ImportErrors.Count > 0)
                    {
                        _importErrors.AddRange(dataImport.ImportErrors);
                    }

                    if (ItemsImport == null || ItemsImport.Count <= 0)
                    {
                        System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    if (string.IsNullOrEmpty(FilePath))
                    {
                        lstError.Add(Resources.ErrorFileEmpty);
                    }

                    if (SelectedDonVi == null)
                    {
                        MessageBoxHelper.Error(Resources.MsgDonViEmpty);
                        OnResetData();
                        return;
                    }

                    isExist = lstChungTu.Any(x => x.IID_MaDonVi == SelectedDonVi.ValueItem && x.INamLamViec == _sessionInfo.YearOfWork);
                    if (isExist)
                    {
                        MessageBoxHelper.Warning(Resources.MsgExistDonVi);
                        OnResetData();
                        return;
                    }

                    int i = 0;
                    foreach (KhcBhxhDetailImportModel item in ItemsImport)
                    {
                        i++;
                        IEnumerable<ImportErrorItem> listError = ValidateItem(item, i);

                        if (listError.Any())
                        {
                            _importErrors.AddRange(listError);
                            item.ImportStatus = false;
                            if (listError.Any(x => x.IsErrorMLNS))
                            {
                                item.IsError = true;
                            }
                        }
                        BhDmMucLucNganSach mucLuc = _lstMuLucNSBHXH.FirstOrDefault(x => x.SXauNoiMa.Equals(item.SXauNoiMa) && x.SMoTa.Equals(item.STenMLNS));
                        if (mucLuc is null)
                        {
                            continue;
                        }

                        item.BHangCha = mucLuc.BHangCha;
                        item.IsHangCha = mucLuc.BHangCha;
                    }

                    if (ItemsImport.Any(x => !x.ImportStatus))
                    {
                        System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    Items = ItemsImport;

                    foreach (KhcBhxhDetailImportModel item in Items)
                    {
                        item.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(KhcBhxhDetailImportModel.SXauNoiMa))
                            {
                                KhcBhxhDetailImportModel entityDetail = (KhcBhxhDetailImportModel)sender;
                                int rowIndex = Items.IndexOf(entityDetail);
                                List<ImportErrorItem> listError = _importService.ValidateItem(entityDetail, rowIndex);
                                if (listError.Count > 0)
                                {
                                    List<string> messageOfRow = listError.Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToList();
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

                    if (lstError.Any())
                    {
                        string sMessError = string.Join(Environment.NewLine, lstError);
                        System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }


                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                FilePath = "";
                OnPropertyChanged(nameof(IsSelectedFile));
                if (ex.Message == "Sai template")
                {
                    System.Windows.MessageBox.Show(Resources.FileImportWrongTemplate, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    _logger.Error(ex.Message, ex);
                }
            }
        }

        private string GetSoChungTu()
        {
            int NamLamViec = _sessionInfo.YearOfWork;
            int soChungTuIndex = _khcBHXHService.GetSoChungTuIndexByCondition(NamLamViec);
            string SoChungTu = "KHC-" + soChungTuIndex.ToString("D3");
            return SoChungTu;
        }
        #endregion

        #region Dowload template
        private void OnDownloadTemplate()
        {
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_KHC_IMPORT_TEMPLATE, ExportFileName.IMPORT_BH_KHC_CHUNGTU_CHITIET_BHXH);
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Chọn file excel",
                RestoreDirectory = true,
                DefaultExt = StringUtils.EXCEL_EXTENSION,
                Filter = "Excel files (*.xlsx)|*.xlsx"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileTemplatePath = Path.Combine(IOExtensions.ApplicationPath, templateFileName);
                try
                {
                    File.Copy(fileTemplatePath, saveFileDialog.FileName);
                    MessageBoxHelper.Info(Resources.MesDownloadSuccess);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }

            }
        }
        #endregion


        #region On save data
        private void OnSaveData()
        {
            if (SelectedDonVi == null)
            {
                MessageBoxHelper.Error(Resources.MsgDonViEmpty);
                OnResetData();
                return;
            }

            _nsBhxhMucLucs = _bhDmMucLucNganSachService.GetListBhMucLucNs(_sessionInfo.YearOfWork, KhcBhxhMLNS.KHOI_DU_TOAN, KhcBhxhMLNS.KHOI_HACH_TOAN);
            BhKhcCheDoBhXh chungTu = new BhKhcCheDoBhXh();
            chungTu.SSoChungTu = SSoChungTu;
            chungTu.IIdDonViId = SelectedDonVi.Id;
            chungTu.IID_MaDonVi = SelectedDonVi.ValueItem;
            chungTu.DNgayChungTu = DNgayChungTu == null ? DateTime.Now : DNgayChungTu;
            chungTu.BDaTongHop = false;
            chungTu.SMoTa = string.Empty;
            chungTu.INamLamViec = _sessionInfo.YearOfWork;
            chungTu.DNgayTao = DateTime.Now;
            chungTu.SNguoiTao = _sessionInfo.Principal;
            if (SelectedDonVi.HiddenValue == LoaiDonVi.ROOT)
            {
                chungTu.ILoaiTongHop = KhcBhxhLoaiChungTu.BhxhChungTuTongHop;
                chungTu.IID_TongHopID = Guid.NewGuid();
            }
            else
            {
                chungTu.ILoaiTongHop = KhcBhxhLoaiChungTu.BhxhChungTu;

            }
            _khcBHXHService.Add(chungTu);

            List<BhKhcCheDoBhXhChiTiet> chungTuChiTiets = new List<BhKhcCheDoBhXhChiTiet>();
            List<KhcBhxhDetailImportModel> listDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsDataNotNull
            && _nsBhxhMucLucs.Any(y => !y.BHangCha && y.SMoTa == x.STenMLNS && y.SXauNoiMa == x.SXauNoiMa)).ToList();
            foreach (KhcBhxhDetailImportModel item in listDetailImport)
            {
                BhDmMucLucNganSach mucLuc = _nsBhxhMucLucs.FirstOrDefault(x => x.INamLamViec == _sessionInfo.YearOfWork && x.ITrangThai == StatusType.ACTIVE
                && item.STenMLNS == x.SMoTa && item.SXauNoiMa == x.SXauNoiMa);
                if (mucLuc == null)
                {
                    continue;
                }
                BhKhcCheDoBhXhChiTiet bhKhcChiTiet = new BhKhcCheDoBhXhChiTiet();
                bhKhcChiTiet.Id = Guid.NewGuid();
                bhKhcChiTiet.IID_KHC_CheDoBHXH = chungTu.Id;
                bhKhcChiTiet.IID_MucLucNganSach = mucLuc.IIDMLNS;
                bhKhcChiTiet.DNgayTao = DateTime.Now;
                bhKhcChiTiet.SNguoiTao = _sessionInfo.Principal;
                bhKhcChiTiet.INamLamViec = _sessionInfo.YearOfWork;
                bhKhcChiTiet.IIdMaDonVi = SelectedDonVi.ValueItem;
                bhKhcChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                bhKhcChiTiet.SLoaiTroCap = mucLuc.SMoTa;
                //bhKhcChiTiet.ISoDaThucHienNamTruoc = item.ISoDaThucHienNamTruoc;
                //bhKhcChiTiet.FTienDaThucHienNamTruoc = item.FTienDaThucHienNamTruoc;

                bhKhcChiTiet.ISoUocThucHienNamTruoc = item.ISoUocThucHienNamTruoc;
                bhKhcChiTiet.FTienUocThucHienNamTruoc = item.FTienUocThucHienNamTruoc;

                bhKhcChiTiet.ISoKeHoachThucHienNamNay = item.ISoKeHoachThucHienNamNay;
                bhKhcChiTiet.FTienKeHoachThucHienNamNay = item.FTienKeHoachThucHienNamNay;

                bhKhcChiTiet.ISoSQ = item.ISoSQ;
                bhKhcChiTiet.FTienSQ = item.FTienSQ;

                bhKhcChiTiet.ISoQNCN = item.ISoQNCN;
                bhKhcChiTiet.FTienQNCN = item.FTienQNCN;

                bhKhcChiTiet.ISoCNVQP = item.ISoCNVQP;
                bhKhcChiTiet.FTienCNVQP = item.FTienCNVQP;

                bhKhcChiTiet.ISoLDHD = item.ISoLDHD;
                bhKhcChiTiet.FTienLDHD = item.FTienLDHD;

                bhKhcChiTiet.ISoHSQBS = item.ISoHSQBS;
                bhKhcChiTiet.FTienHSQBS = item.FTienHSQBS;

                bhKhcChiTiet.DNgayTao = DateTime.Now;
                bhKhcChiTiet.SNguoiTao = _sessionInfo.Principal;
                bhKhcChiTiet.SGhiChu = item.SGhiChu;
                chungTuChiTiets.Add(bhKhcChiTiet);
            }
            _khcBHXHChiTietService.AddRange(chungTuChiTiets);
            if (chungTuChiTiets.Count > 0)
            {
                chungTu.ITongSoDaThucHienNamTruoc = chungTuChiTiets.Sum(item => item.ISoDaThucHienNamTruoc);
                chungTu.FTongTienDaThucHienNamTruoc = chungTuChiTiets.Sum(item => item.FTienDaThucHienNamTruoc);

                chungTu.ITongSoUocThucHienNamTruoc = chungTuChiTiets.Sum(item => item.ISoUocThucHienNamTruoc);
                chungTu.FTongTienUocThucHienNamTruoc = chungTuChiTiets.Sum(item => item.FTienUocThucHienNamTruoc);

                chungTu.ITongSoKeHoachThucHienNamNay = chungTuChiTiets.Sum(item => item.ISoKeHoachThucHienNamNay);
                chungTu.FTongTienKeHoachThucHienNamNay = chungTuChiTiets.Sum(item => item.FTienKeHoachThucHienNamNay);

                chungTu.ITongSoSQ = chungTuChiTiets.Sum(item => item.ISoSQ);
                chungTu.FTongTienSQ = chungTuChiTiets.Sum(item => item.FTienSQ);

                chungTu.ITongSoQNCN = chungTuChiTiets.Sum(item => item.ISoQNCN);
                chungTu.FTongTienQNCN = chungTuChiTiets.Sum(item => item.FTienQNCN);

                chungTu.ITongSoCNVQP = chungTuChiTiets.Sum(item => item.ISoCNVQP);
                chungTu.FTongTienCNVQP = chungTuChiTiets.Sum(item => item.FTienCNVQP);

                chungTu.ITongSoLDHD = chungTuChiTiets.Sum(item => item.ISoLDHD);
                chungTu.FTongTienLDHD = chungTuChiTiets.Sum(item => item.FTienLDHD);

                chungTu.ITongSoHSQBS = chungTuChiTiets.Sum(item => item.ISoHSQBS);
                chungTu.FTongTienHSQBS = chungTuChiTiets.Sum(item => item.FTienHSQBS);

                _khcBHXHService.Update(chungTu);
            }

            BhKhcCheDoBhXhModel khcBhxh = _mapper.Map<BhKhcCheDoBhXhModel>(chungTu);
            //DialogHost.CloseDialogCommand.Execute(khcBhxh, null);
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            SavedAction?.Invoke(khcBhxh);
        }
        #endregion

        #region Upload file
        private void OnUploadFile()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
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
                _isUploadFile = true;
                OnPropertyChanged(nameof(IsSelectedFile));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.MsgErrorImport, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
                return;
            }
        }
        #endregion

        #region Check error 
        private void OnCheckFileImport()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                HandleData();
            }, (s, e) =>
            {
                IsLoading = false;
            });

            if (SelectedDonVi != null)
            {
                OnPropertyChanged(nameof(IsSaveData));
            }
        }
        private void ShowError()
        {
            int rowIndex = _items.IndexOf(SelectedItem);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private IEnumerable<ImportErrorItem> ValidateItem(KhcBhxhDetailImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();

                int yearOfWork = _sessionService.Current.YearOfWork;
                System.Linq.Expressions.Expression<Func<BhDmMucLucNganSach, bool>> predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);
                List<string> lstMucLucNSBHXH = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Select(x => x.SXauNoiMa).ToList();
                if (!lstMucLucNSBHXH.Contains(item.SXauNoiMa))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Nội dung",
                        Error = string.Format(Resources.MsgXauNoiMaKhongTonTai, "nội dung"),
                        Row = rowIndex - 1
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
        #endregion
    }
}
