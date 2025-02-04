using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Control;
using FlexCel.XlsAdapter;
using System.Windows;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Utility.Enum;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using System.IO;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV.Import
{
    public class ImportKhcKCBQYDVViewModel : ViewModelBase
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IBhKhcKcbService _bhKhcKcbService;
        private readonly IBhKhcKcbChiTietService _bhKhcKcbChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhKhcKcbService _bhKcbService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        #endregion

        #region Property
        private bool _isUploadFile;
        private SessionInfo _sessionInfo;
        private List<BhDmMucLucNganSach> _lstMuLucNSBHXH;
        public override string Name => "Kế hoạch chi KCB quân y đơn vị ";
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV.Import.ImportKhcKCBQYDV);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private Dictionary<Guid, BhKhcKcbChiTiet> _dicKhcKcbChiTiet;
        private List<BhDmMucLucNganSachModel> _mergeItems;
        private List<BhDmMucLucNganSach> _mucLucNganSachs;

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

        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }

        private List<BhDmMucLucNganSach> _nsBhxhMucLucs;

        private KhcKcbDetailImportModel _selectedItem;
        public KhcKcbDetailImportModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public bool _isSelectedFile;
        public bool IsSelectedFile
        {
            get => !string.IsNullOrEmpty(_filePath);
            set => SetProperty(ref _isSelectedFile, value);
        }
        public List<string> ListIdDonViHasCt { get; set; }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private bool _isSaveData;
        public bool IsSaveData
        {
            get
            {
                _isSaveData = (Items != null && Items.Count > 0) && SelectedDonVi != null && !_importErrors.Any();
                return _isSaveData;
            }
            set
            {
                SetProperty(ref _isSaveData, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
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

        private ObservableCollection<KhcKcbDetailImportModel> _items;
        public ObservableCollection<KhcKcbDetailImportModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));
            }
        }


        private ObservableCollection<KhcKcbDetailImportModel> _itemsImport;
        public ObservableCollection<KhcKcbDetailImportModel> ItemsImport
        {
            get => _itemsImport;
            set
            {
                SetProperty(ref _itemsImport, value);

            }
        }
        private DateTime? _dNgayChungTu;
        public DateTime? DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }

        private string _sSoChungTu;
        public string SSoChungTu
        {
            get => _sSoChungTu;
            set => SetProperty(ref _sSoChungTu, value);
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
        public string HeaderSoDaThucHienNam => "Tổng số đã thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderUocThucHienNam => "Ước thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderKehoachThucHienNam => "Kế hoạch thực hiện năm " + (_sessionService.Current.YearOfWork);
        private DateTime _ngayChungTu;
        public DateTime NgayChungTu
        {
            get => _ngayChungTu;
            set => SetProperty(ref _ngayChungTu, value);
        }
        public string MoTa { get; set; }
        #endregion

        #region RelayCommand
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand DownloadTemplateCommand { get; }
        public RelayCommand CheckFileImport { get; }
        #endregion

        #region Constructor
        public ImportKhcKCBQYDVViewModel(
                ISessionService sessionService,
                INsDonViService donViService,
                IMapper mapper,
                IImportExcelService importService,
                IBhKhcKcbChiTietService bhKhcKcbChiTietService,
                IBhKhcKcbService bhKhcKcbService,
                IBhDmMucLucNganSachService bhDmMucLucNganSachService,
                ILog logger)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _bhKhcKcbService = bhKhcKcbService;
            _bhKhcKcbChiTietService = bhKhcKcbChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
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

        private string GetSoChungTu()
        {
            var NamLamViec = _sessionInfo.YearOfWork;
            var soChungTuIndex = _bhKhcKcbService.GetSoChungTuIndexByCondition(NamLamViec);
            var SoChungTu = "KHC-" + soChungTuIndex.ToString("D3");

            return SoChungTu;
        }
        #endregion

        #region Load data
        private void LoadDonVi()
        {
            ItemsDonVi = new ObservableCollection<ComboboxItem>();
            var yearOfWork = _sessionInfo.YearOfWork;
            var lstChungTu = _bhKhcKcbService.FindIndex().Where(x => x.INamLamViec == yearOfWork);
            IEnumerable<DonVi> listDonVi = _donViService.FindByNamLamViec(yearOfWork);
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
                var fileExtension = Path.GetExtension(FilePath).ToLower();
                if (!(fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx")))
                {
                    System.Windows.MessageBox.Show(Resources.FileImportWrongExtensionExcel, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_importErrors != null && _importErrors.Any())
                {
                    _importErrors = new List<ImportErrorItem>();
                    foreach (var item in Items)
                    {
                        var mucLuc = _lstMuLucNSBHXH.FirstOrDefault(x => x.SXauNoiMa.Equals(item.SXauNoiMa) && x.SMoTa.Equals(item.STenMLNS));
                        if (mucLuc != null)
                        {
                            item.BHangCha = mucLuc.BHangCha;
                            item.IsHangCha = mucLuc.BHangCha;
                        }

                        var rowIndex = Items.IndexOf(item);
                        var listError = ValidateItem(item, rowIndex);

                        if (listError.Any())
                        {
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
                        var messageOfRow = _importErrors.Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).Distinct();
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
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var lstChungTu = _bhKhcKcbService.FindIndex().Where(x => x.INamLamViec == yearOfWork).ToList();
                    _isUploadFile = false;
                    bool isExist;
                    var predicateSummaryDetail = PredicateBuilder.True<BhKhcKcbChiTiet>();
                    predicateSummaryDetail = predicateSummaryDetail.And(x => x.IID_KHC_KCB.IsNullOrEmpty());
                    var BhxhChiTiet = _bhKhcKcbChiTietService.FindAll(predicateSummaryDetail);
                    if (BhxhChiTiet != null)
                    {
                        foreach (var item in BhxhChiTiet)
                        {
                            if (!_lstIdBhxhChiTiet.Contains(item.Id))
                            {
                                _lstIdBhxhChiTiet.Add(item.Id);
                            }
                        }
                    }

                    _dicKhcKcbChiTiet = new Dictionary<Guid, BhKhcKcbChiTiet>();

                    XlsFile xls = new XlsFile(false);
                    xls.Open(FilePath);
                    xls.ActiveSheet = 1;

                    var dataImport = _importService.ProcessData<KhcKcbDetailImportModel>(FilePath);
                    ItemsImport = new ObservableCollection<KhcKcbDetailImportModel>(dataImport.Data.Where(x => x.IsDataNotNull));

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
                        MessageBoxHelper.Warning(Resources.MsgDonViEmpty);
                        OnResetData();
                        return;
                    }


                    isExist = lstChungTu.Any(x => x.IID_MaDonVi == SelectedDonVi.ValueItem);

                    if (isExist)
                    {
                        MessageBoxHelper.Warning(Resources.MsgExistDonVi);
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
                            _importErrors.AddRange(listError);
                            item.ImportStatus = false;
                            if (listError.Any(x => x.IsErrorMLNS))
                            {
                                item.IsError = true;
                            }
                        }
                        var mucLuc = _lstMuLucNSBHXH.FirstOrDefault(x => x.SXauNoiMa.Equals(item.SXauNoiMa) && x.SMoTa.Equals(item.STenMLNS));
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

                    foreach (var item in Items)
                    {
                        item.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(KhcKcbDetailImportModel.SXauNoiMa))
                            {
                                var entityDetail = (KhcKcbDetailImportModel)sender;
                                var rowIndex = Items.IndexOf(entityDetail);
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

                    if (lstError.Any())
                    {
                        string sMessError = string.Join(Environment.NewLine, lstError);
                        System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    OnPropertyChanged(nameof(IsSaveData));
                }
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

        private IEnumerable<ImportErrorItem> ValidateItem(KhcKcbDetailImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                var lstXauNoiMaMlns = _lstMuLucNSBHXH.Select(x => x.SXauNoiMa).ToList();
                if (!lstXauNoiMaMlns.Contains(item.SXauNoiMa))
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

        private void OnResetData()
        {
            _filePath = string.Empty;
            _importErrors = new List<ImportErrorItem>();
            _mergeItems = new List<BhDmMucLucNganSachModel>();
            ItemsImport = new ObservableCollection<KhcKcbDetailImportModel>();
            Items = new ObservableCollection<KhcKcbDetailImportModel>();
            _lstMuLucNSBHXH = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(_sessionInfo.YearOfWork, LNSValue.LNS_9010004_9010005);
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => x.SLNS == KhcKcbBhxhMLNS.QUAN_Y_DON_VI_KHOI_DU_TOAN || x.SLNS == KhcKcbBhxhMLNS.QUAN_Y_DON_VI_KHOI_HACH_TOAN).OrderBy(x => x.SXauNoiMa).ToList();
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

        #region Check file Import
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
        #endregion

        #region Download Template
        private void OnDownloadTemplate()
        {
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_KHC_KCB_IMPORT_TEMPLATE, ExportFileName.IMPORT_BH_KHC_KCB_CHUNGTU_CHITIET_BHXH);
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Chọn file excel",
                RestoreDirectory = true,
                DefaultExt = StringUtils.EXCEL_EXTENSION,
                Filter = "Excel files (*.xlsx)|*.xlsx"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileTemplatePath = Path.Combine(IOExtensions.ApplicationPath, templateFileName);
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

        #region Show error
        private void ShowError()
        {
            int rowIndex = _items.IndexOf(SelectedItem);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region On save data
        private void OnSaveData()
        {
            try
            {
                string sLoaiKHC = string.Empty;

                if (SelectedDonVi == null)
                {
                    MessageBoxHelper.Error(Resources.MsgDonViEmpty);
                    OnResetData();
                    return;
                }

                _nsBhxhMucLucs = _bhDmMucLucNganSachService.GetListBhMucLucNs(_sessionInfo.YearOfWork, KhcKcbBhxhMLNS.QUAN_Y_DON_VI_KHOI_DU_TOAN, KhcKcbBhxhMLNS.QUAN_Y_DON_VI_KHOI_HACH_TOAN);

                BhKhcKcb chungTu = new BhKhcKcb();
                chungTu.SSoChungTu = SSoChungTu;
                chungTu.IIdDonViId = SelectedDonVi.Id;
                chungTu.IID_MaDonVi = SelectedDonVi.ValueItem;
                chungTu.DNgayChungTu = DNgayChungTu == null ? DateTime.Now : DNgayChungTu;
                chungTu.SMoTa = string.Empty;
                chungTu.INamLamViec = _sessionInfo.YearOfWork;
                chungTu.DNgayTao = DateTime.Now;
                chungTu.SNguoiTao = _sessionInfo.Principal;
                chungTu.BDaTongHop = false;
                if (SelectedDonVi.HiddenValue == LoaiDonVi.ROOT)
                {
                    chungTu.ILoaiTongHop = KhcBhxhLoaiChungTu.BhxhChungTuTongHop;
                    chungTu.IID_TongHopID = Guid.NewGuid();
                }
                else
                {
                    chungTu.ILoaiTongHop = KhcBhxhLoaiChungTu.BhxhChungTu;
                }

                _bhKhcKcbService.Add(chungTu);

                List<BhKhcKcbChiTiet> chungTuChiTiets = new List<BhKhcKcbChiTiet>();

                List<KhcKcbDetailImportModel> listDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsDataNotNull
                && _nsBhxhMucLucs.Any(y => !y.BHangCha && y.SXauNoiMa == x.SXauNoiMa)).ToList();

                foreach (var item in listDetailImport)
                {
                    BhDmMucLucNganSach mucLuc = _nsBhxhMucLucs.FirstOrDefault(x => x.INamLamViec == _sessionInfo.YearOfWork && x.ITrangThai == StatusType.ACTIVE
                    && item.STenMLNS == x.SMoTa && item.SXauNoiMa == x.SXauNoiMa);
                    if (mucLuc == null)
                    {
                        continue;
                    }

                    BhKhcKcbChiTiet bhKhcChiTiet = new BhKhcKcbChiTiet();
                    bhKhcChiTiet.Id = Guid.NewGuid();
                    bhKhcChiTiet.IID_KHC_KCB = chungTu.Id;
                    bhKhcChiTiet.IID_MucLucNganSach = mucLuc.IIDMLNS;
                    bhKhcChiTiet.SNoiDung = mucLuc.SMoTa;
                    bhKhcChiTiet.SNguoiTao = _sessionInfo.Principal;
                    //bhKhcChiTiet.FTienDaThucHienNamTruoc = item.FTienDaThucHienNamTruoc;
                    bhKhcChiTiet.FTienUocThucHienNamTruoc = item.FTienUocThucHienNamTruoc;
                    bhKhcChiTiet.FTienKeHoachThucHienNamNay = item.FTienKeHoachThucHienNamNay;
                    bhKhcChiTiet.DNgayTao = DateTime.Now;
                    bhKhcChiTiet.SNguoiTao = _sessionInfo.Principal;
                    bhKhcChiTiet.SGhiChu = item.SGhiChu;
                    bhKhcChiTiet.BHangCha = mucLuc.BHangCha;
                    bhKhcChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    bhKhcChiTiet.INamLamViec = _sessionInfo.YearOfWork;
                    bhKhcChiTiet.IIdMaDonVi = _selectedDonVi.ValueItem;
                    chungTuChiTiets.Add(bhKhcChiTiet);
                }
                _bhKhcKcbChiTietService.AddRange(chungTuChiTiets);
                if (chungTuChiTiets.Count > 0)
                {
                    chungTu.FTongTienDaThucHienNamTruoc = chungTuChiTiets.Sum(item => item.FTienDaThucHienNamTruoc);
                    chungTu.FTongTienUocThucHienNamTruoc = chungTuChiTiets.Sum(item => item.FTienUocThucHienNamTruoc);
                    chungTu.FTongTienKeHoachThucHienNamNay = chungTuChiTiets.Sum(item => item.FTienKeHoachThucHienNamNay);

                    _bhKhcKcbService.Update(chungTu);
                }

                BhKhcKcbModel khcKinhphiQuanly = _mapper.Map<BhKhcKcbModel>(chungTu);
                MessageBoxHelper.Info(Resources.MsgSaveDone);
                SavedAction?.Invoke(khcKinhphiQuanly);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region OnUploadFile
        private void OnUploadFile()
        {
            try
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
    }
}
