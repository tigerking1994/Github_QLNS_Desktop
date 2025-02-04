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
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.ImportChiKinhPhiQuanLy;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.ImportChiKinhPhiQuanLy
{
    public class ImportQuyetToanChiKinhPhiQuanLyViewModel : ViewModelBase
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IBhQtcQuyKinhPhiQuanLyService _kinhPhiQuanLyService;
        private readonly IBhQtcQuyKinhPhiQuanLyChiTietService _kinhPhiQuanLyChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private ImpHistory _impHistory;
        #endregion

        #region Property
        private bool _isUploadFile;
        IEnumerable<DonVi> listDonVi;
        public override string Name => "IMPORT DỮ LIỆU CHỨNG TỪ QUYẾT TOÁN QUÝ CHI KINH PHÍ QUẢN LÝ " + _sessionInfo.YearOfWork;
        public override Type ContentType => typeof(ImportQuyetToanChiKinhPhiQuanLy);
        public override PackIconKind IconKind => PackIconKind.Import;
        private Dictionary<Guid, BhQtcQuyKinhPhiQuanLyChiTiet> _dicChiTiet;
        private List<BhDmMucLucNganSachModel> _mergeItems;
        private List<BhDmMucLucNganSach> _nsBhYtMucLucs;
        private List<BhDmMucLucNganSach> _lsAllBhYtMucLucs;
        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }
        private BhQtcQuyCKPQLChungTuDetailImportModel _selectedItem;
        public BhQtcQuyCKPQLChungTuDetailImportModel SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }

        public bool _isSelectedFile;
        public bool IsSelectedFile
        {
            get => !string.IsNullOrEmpty(_filePath);
            set => SetProperty(ref _isSelectedFile, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }
        private List<ComboboxItem> _quarters;
        public List<ComboboxItem> Quarters
        {
            get => _quarters;
            set => SetProperty(ref _quarters, value);
        }

        private ComboboxItem _quartersSelected;
        public ComboboxItem QuartersSelected
        {
            get => _quartersSelected;
            set
            {
                SetProperty(ref _quartersSelected, value);
            }
        }
        private bool _isSaveData;
        public bool IsSaveData
        {
            get
            {
                _isSaveData = (Items.Any() && !_importErrors.Any());
                return _isSaveData;
            }
            set
            {
                SetProperty(ref _isSaveData, value);
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

        private ObservableCollection<BhQtcQuyCKPQLChungTuDetailImportModel> _items;
        public ObservableCollection<BhQtcQuyCKPQLChungTuDetailImportModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private ObservableCollection<BhQtcQuyCKPQLChungTuDetailImportModel> _itemsImport;
        public ObservableCollection<BhQtcQuyCKPQLChungTuDetailImportModel> ItemsImport
        {
            get => _itemsImport;
            set
            {
                SetProperty(ref _itemsImport, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private ObservableCollection<BhDmMucLucNganSachModel> _importedMlns;
        public ObservableCollection<BhDmMucLucNganSachModel> ImportedMlns
        {
            get => _importedMlns;
            set => SetProperty(ref _importedMlns, value);
        }
        public bool IsEnabledMergeBtn
        {
            get => ImportedMlns.Any(i => i.IsSelected) && ExistedMlns.Where(i => i.IsSelected).Count() == 1;
        }
        private ObservableCollection<BhDmMucLucNganSachModel> _existedMlns;
        public ObservableCollection<BhDmMucLucNganSachModel> ExistedMlns
        {
            get => _existedMlns;
            set => SetProperty(ref _existedMlns, value);
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
        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }

        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }
        public bool IsEnableSaveMLNS = false;
        public DateTime? DNgayQuyetDinh { get; set; }

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
        public string MoTa { get; set; }
        #endregion

        #region  RelayCommand
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand DownloadTemplateCommand { get; }
        public RelayCommand HandleDataCommand { get; }
        #endregion

        #region Constructor
        public ImportQuyetToanChiKinhPhiQuanLyViewModel(
               ISessionService sessionService,
               INsDonViService donViService,
               IMapper mapper,
               ILog logger,
               IImportExcelService importService,
               IBhQtcQuyKinhPhiQuanLyService qtcQuyKinhPhiQuanLyService,
               IBhQtcQuyKinhPhiQuanLyChiTietService qtcQuyKinhPhiQuanLyChiTietService,
               IBhDmMucLucNganSachService bhDmMucLucNganSachService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _logger = logger;
            _importService = importService;
            _kinhPhiQuanLyChiTietService = qtcQuyKinhPhiQuanLyChiTietService;
            _kinhPhiQuanLyService = qtcQuyKinhPhiQuanLyService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            DownloadTemplateCommand = new RelayCommand(obj => OnDownloadTemplate());
            HandleDataCommand = new RelayCommand(obj => HandleData());
        }
        #endregion

        #region Init
        public override void Init()
        {
            try
            {
                base.Init();
                _sessionInfo = _sessionService.Current;
                LoadDonVi();
                LoadQuarters();
                SSoChungTu = GetSoChungTu();
                DNgayChungTu = DateTime.Now;
                DNgayQuyetDinh = DateTime.Now;
                OnResetData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string GetSoChungTu()
        {
            var soChungTuIndex = _kinhPhiQuanLyService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
            var soChungTu = "QTC-" + soChungTuIndex.ToString("D3");
            return soChungTu;
        }
        #endregion

        #region 
        private void LoadQuarters()
        {
            _quarters = FnCommonUtils.LoadQuarters();
            QuartersSelected = _quarters.FirstOrDefault();
        }

        private void LoadDonVi()
        {
            ItemsDonVi = new ObservableCollection<ComboboxItem>();
            listDonVi = _donViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDonVi != null)
            {
                listDonVi = listDonVi.Where(x => x.Loai != LoaiDonVi.ROOT).OrderBy(x => x.IIDMaDonVi).ToList();
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
                        var rowIndex = Items.IndexOf(item);
                        var listError = _importService.ValidateItem(item, rowIndex);

                        if (listError.Count > 0)
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
                    var kinhPhiQuanLyChiTiet = _kinhPhiQuanLyChiTietService.FindAllChungTuDuToan();
                    var kinhPhiQuanLy = _kinhPhiQuanLyService.FindIndex(_sessionInfo.YearOfWork).ToList();
                    bool isExist;

                    if (kinhPhiQuanLyChiTiet != null)
                    {
                        foreach (var item in kinhPhiQuanLyChiTiet)
                        {
                            if (!_lstIdBhxhChiTiet.Contains(item.Id))
                            {
                                _lstIdBhxhChiTiet.Add(item.Id);
                            }
                        }
                    }


                    _dicChiTiet = new Dictionary<Guid, BhQtcQuyKinhPhiQuanLyChiTiet>();
                    XlsFile xls = new XlsFile(false);
                    xls.Open(FilePath);
                    xls.ActiveSheet = 1;
                    _isUploadFile = false;
                    string sLNSImport = string.Empty;
                    var dataImport = _importService.ProcessData<BhQtcQuyCKPQLChungTuDetailImportModel>(FilePath);
                    ItemsImport = new ObservableCollection<BhQtcQuyCKPQLChungTuDetailImportModel>(dataImport.Data);

                    List<string> lstError = new List<string>();

                    if (dataImport.ImportErrors.Count > 0)
                    {
                        _importErrors.AddRange(dataImport.ImportErrors);
                    }

                    if (ItemsImport.IsEmpty())
                    {
                        System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (string.IsNullOrEmpty(FilePath))
                    {
                        lstError.Add(Resources.ErrorFileEmpty);
                        return;

                    }

                    if (QuartersSelected == null)
                    {
                        MessageBoxHelper.Error(Resources.AlertQuartyEmpty);
                        OnResetData();
                        return;
                    }

                    if (SelectedDonVi == null)
                    {
                        MessageBoxHelper.Error(Resources.MsgDonViEmpty);
                        OnResetData();
                        return;
                    }


                    if (!ExistedMlns.IsEmpty())
                    {
                        var lstMlnsImport = ExistedMlns.Where(x => ItemsImport.Select(s => s.SXauNoiMa).Distinct().Contains(x.SXauNoiMa)).ToList();
                        ImportedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(lstMlnsImport);
                    }

                    isExist = kinhPhiQuanLy.Any(x => x.IID_MaDonVi.Equals(SelectedDonVi.ValueItem)
                                                && x.IQuyChungTu == int.Parse(QuartersSelected.ValueItem)
                                                && x.INamChungTu == _sessionInfo.YearOfWork);

                    if (isExist)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.AlertExistSettlementQuarterVoucher, SelectedDonVi.ValueItem, QuartersSelected.ValueItem, SettlementTypeSLNS.SLNS));
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
                            lstError.Add(Resources.MsgXauNoiMaKhongTonTai);
                            item.ImportStatus = false;
                            if (listError.Any(x => x.IsErrorMLNS))
                            {
                                item.IsError = true;
                            }
                        }                     
                    }

                    Items = ItemsImport;
                    foreach (var item in Items)
                    {
                        item.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(BhQtcQuyCKPQLChungTuDetailImportModel.SXauNoiMa))
                            {
                                var entityDetail = (BhQtcQuyCKPQLChungTuDetailImportModel)sender;
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
                        System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(ImportedMlns));
                    OnPropertyChanged(nameof(Items));
                }

            }
            catch (Exception ex)
            {
                FilePath = "";
                if (ex.Message == "Sai template")
                {
                    System.Windows.MessageBox.Show(Resources.FileImportWrongTemplate, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    System.Windows.MessageBox.Show(Resources.ErrorImport, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    _logger.Error(ex.Message, ex);
                }
            }
        }
        #endregion

        #region On Download Template
        private void OnDownloadTemplate()
        {
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTC_QUYKPQL_IMPORT_TEMPLATE, ExportFileName.IMPORT_BH_QTC_KPQL_CHUNGTU_CHITIET);
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
                    MessageBoxHelper.Error(Resources.MesDownloadSuccess);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }

            }
        }
        #endregion

        #region ShowError
        private void ShowError()
        {
            int rowIndex = Items.IndexOf(SelectedItem);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region On Reset Data
        private void OnResetData()
        {
            _mergeItems = new List<BhDmMucLucNganSachModel>();
            _filePath = string.Empty;
            _importErrors = new List<ImportErrorItem>();
            _itemsImport = new ObservableCollection<BhQtcQuyCKPQLChungTuDetailImportModel>();
            _items = new ObservableCollection<BhQtcQuyCKPQLChungTuDetailImportModel>();
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);

            _nsBhYtMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => x.SLNS == SettlementTypeSLNS.SLNS).ToList();
            _importedMlns = new ObservableCollection<BhDmMucLucNganSachModel>();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_nsBhYtMucLucs.OrderBy(x => x.SXauNoiMa)));
            _impHistory = new ImpHistory();
            _importErrors = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;
            LstFile = new ObservableCollection<FileFtpModel>();

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(TabIndex));
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(LstFile));
            OnPropertyChanged(nameof(IsSelectedFile));
        }
        #endregion

        #region On save data
        private void OnSaveData()
        {
            try
            {
                var namLamViec = _sessionInfo.YearOfWork;
                var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == namLamViec && x.SLNS == SettlementTypeSLNS.SLNS);

                IEnumerable<BhDmMucLucNganSach> _nsMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).OrderBy(x => x.SXauNoiMa).ToList();

                BhQtcQuyKinhPhiQuanLy kinhPhiQuanLy = new BhQtcQuyKinhPhiQuanLy();
                //kinhPhiQuanLy.SSoQuyetDinh = SSoQuyetDinh;
                kinhPhiQuanLy.SSoChungTu = SSoChungTu;
                kinhPhiQuanLy.BIsKhoa = false;
                kinhPhiQuanLy.DNgayTao = DateTime.Now;
                kinhPhiQuanLy.DNgayChungTu = DNgayChungTu;
                kinhPhiQuanLy.DNgayQuyetDinh = DNgayQuyetDinh;
                kinhPhiQuanLy.SNguoiTao = _sessionInfo.Principal;
                kinhPhiQuanLy.IQuyChungTu = int.Parse(QuartersSelected.ValueItem);
                kinhPhiQuanLy.INamChungTu = namLamViec;
                kinhPhiQuanLy.IID_DonVi = SelectedDonVi.Id;
                kinhPhiQuanLy.IID_MaDonVi = SelectedDonVi.ValueItem;
                kinhPhiQuanLy.SDSLNS = LNSValue.LNS_9010003;
                if (SelectedDonVi.HiddenValue == LoaiDonVi.ROOT)
                {
                    kinhPhiQuanLy.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTuTongHop;
                    kinhPhiQuanLy.IID_TongHopID = Guid.NewGuid();
                }
                else
                {
                    kinhPhiQuanLy.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTu;
                }

                _kinhPhiQuanLyService.Add(kinhPhiQuanLy);

                List<BhQtcQuyKinhPhiQuanLyChiTiet> kinhPhiQuanLyChiTiets = new List<BhQtcQuyKinhPhiQuanLyChiTiet>();
                List<BhQtcQuyCKPQLChungTuDetailImportModel> lstDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasData
                                                                                && _nsMucLucs.Any(y => !y.BHangCha && y.SXauNoiMa == x.SXauNoiMa)).ToList();
                var lstDataDuToan = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasData
                                                                                && _nsMucLucs.Any(y => y.SXauNoiMa == x.SXauNoiMa)).ToList();
                foreach (var item in lstDetailImport)
                {
                    BhDmMucLucNganSach mucLuc = _nsMucLucs.FirstOrDefault(x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE
                    && item.SNoiDung == x.SMoTa && item.SXauNoiMa == x.SXauNoiMa);

                    if (mucLuc == null)
                    {
                        continue;
                    }

                    BhQtcQuyKinhPhiQuanLyChiTiet kinhPhiQuanLyChiTiet = new BhQtcQuyKinhPhiQuanLyChiTiet();
                    kinhPhiQuanLyChiTiet.IID_QTC_Quy_KinhPhiQuanLy = kinhPhiQuanLy.Id;
                    kinhPhiQuanLyChiTiet.IID_MucLucNganSach = mucLuc.IIDMLNS;
                    kinhPhiQuanLyChiTiet.SM = mucLuc.SM;
                    kinhPhiQuanLyChiTiet.STM = mucLuc.STM;
                    kinhPhiQuanLyChiTiet.SNoiDung = mucLuc.SMoTa;
                    kinhPhiQuanLyChiTiet.IIdMaDonVi = SelectedDonVi.ValueItem;
                    kinhPhiQuanLyChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    kinhPhiQuanLyChiTiet.INamLamViec = _sessionInfo.YearOfWork;
                    kinhPhiQuanLyChiTiet.FTienDeNghiQuyetToanQuyNay = ConvertStringToNumber(item.STienDeNghiQuyetToanQuyNay);
                    kinhPhiQuanLyChiTiet.FTienXacNhanQuyetToanQuyNay = ConvertStringToNumber(item.STienXacNhanQuyetToanQuyNay);
                    kinhPhiQuanLyChiTiet.FTienThucChi = ConvertStringToNumber(item.STienSoThucChi);
                    kinhPhiQuanLyChiTiet.FTienDuToanDuocGiao = ConvertStringToNumber(item.STienDuToanDuocGiao);
                    kinhPhiQuanLyChiTiet.FTienQuyetToanDaDuyet = ConvertStringToNumber(item.STienQuyetToanDaDuocDuyet);

                    kinhPhiQuanLyChiTiets.Add(kinhPhiQuanLyChiTiet);
                }

                //foreach (var itemDuToan in lstDataDuToan.Where(x => x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToiKPQL).ToList())
                //{
                //    BhDmMucLucNganSach mucLuc = _nsMucLucs.FirstOrDefault(x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE
                //    && itemDuToan.SNoiDung == x.SMoTa && itemDuToan.SXauNoiMa == x.SXauNoiMa);

                //    if (mucLuc == null)
                //    {
                //        continue;
                //    }

                //    BhQtcQuyKinhPhiQuanLyChiTiet kinhPhiQuanLyChiTiet = new BhQtcQuyKinhPhiQuanLyChiTiet();
                //    kinhPhiQuanLyChiTiet.IID_QTC_Quy_KinhPhiQuanLy = kinhPhiQuanLy.Id;
                //    kinhPhiQuanLyChiTiet.IID_MucLucNganSach = mucLuc.IIDMLNS;
                //    kinhPhiQuanLyChiTiet.SM = mucLuc.SM;
                //    kinhPhiQuanLyChiTiet.STM = mucLuc.STM;
                //    kinhPhiQuanLyChiTiet.SNoiDung = mucLuc.SMoTa;
                //    kinhPhiQuanLyChiTiet.IIdMaDonVi = SelectedDonVi.ValueItem;
                //    kinhPhiQuanLyChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                //    kinhPhiQuanLyChiTiet.INamLamViec = _sessionInfo.YearOfWork;
                //    //kinhPhiQuanLyChiTiet.FTienDeNghiQuyetToanQuyNay = ConvertStringToNumber(item.STienDeNghiQuyetToanQuyNay);
                //    //kinhPhiQuanLyChiTiet.FTienXacNhanQuyetToanQuyNay = ConvertStringToNumber(item.STienXacNhanQuyetToanQuyNay);
                //    //kinhPhiQuanLyChiTiet.FTienThucChi = ConvertStringToNumber(item.STienSoThucChi);
                //    kinhPhiQuanLyChiTiet.FTienDuToanDuocGiao = ConvertStringToNumber(itemDuToan.STienDuToanDuocGiao);
                //    //kinhPhiQuanLyChiTiet.FTienQuyetToanDaDuyet = ConvertStringToNumber(item.STienQuyetToanDaDuocDuyet);

                //    kinhPhiQuanLyChiTiets.Add(kinhPhiQuanLyChiTiet);
                //}


                _kinhPhiQuanLyChiTietService.AddRange(kinhPhiQuanLyChiTiets);

                if (kinhPhiQuanLyChiTiets.Count > 0)
                {
                    kinhPhiQuanLy.FTongTienDeNghiQuyetToanQuyNay = kinhPhiQuanLyChiTiets.Sum(x => x.FTienDeNghiQuyetToanQuyNay);
                    kinhPhiQuanLy.FTongTienDuToanDuocGiao = kinhPhiQuanLyChiTiets.Sum(x => x.FTienDuToanDuocGiao);
                    kinhPhiQuanLy.FTongTienQuyetToanDaDuyet = kinhPhiQuanLyChiTiets.Sum(x => x.FTienQuyetToanDaDuyet);
                    kinhPhiQuanLy.FTongTienThucChi = kinhPhiQuanLyChiTiets.Sum(x => x.FTienThucChi);
                    kinhPhiQuanLy.FTongTienXacNhanQuyetToanQuyNay = kinhPhiQuanLyChiTiets.Sum(x => x.FTienXacNhanQuyetToanQuyNay);

                    _kinhPhiQuanLyService.Update(kinhPhiQuanLy);
                }

                BhQtcQuyKinhPhiQuanLyModel kinhPhiQuanLyModel = _mapper.Map<BhQtcQuyKinhPhiQuanLyModel>(kinhPhiQuanLy);
                DialogHost.CloseDialogCommand.Execute(kinhPhiQuanLyModel, null);
                SavedAction?.Invoke(kinhPhiQuanLyModel);
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private double? ConvertStringToNumber(string str)
        {
            double result = 0;
            try
            {
                if (str == null) return result;
                if (double.TryParse(str, out result))
                {
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return result;
            }
        }

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
                _importErrors.Clear();
                _isUploadFile = true;
                FilePath = openFileDialog.FileName;
                OnPropertyChanged(nameof(IsSelectedFile));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.MsgErrorImport, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
                return;
            }
        }


        private List<ImportErrorItem> ValidateItem(BhQtcQuyCKPQLChungTuDetailImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                var yearOfWork = _sessionService.Current.YearOfWork;
                var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);

                _lsAllBhYtMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => x.SLNS == SettlementTypeSLNS.SLNS).ToList();
                var lstXauNoiMaMlns = _lsAllBhYtMucLucs.Select(x => x.SXauNoiMa).ToList();
                if (!lstXauNoiMaMlns.Contains(item.SXauNoiMa))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Xâu nối mã",
                        Error = string.Format(Resources.MsgXauNoiMaKhongTonTai, "nội dung"),
                        Row = rowIndex - 1
                    });
                }

                foreach (var BhYtMucLucs in _lsAllBhYtMucLucs)
                {
                    if (BhYtMucLucs.SXauNoiMa.Equals(item.SXauNoiMa))
                    {
                        item.IsHangCha = BhYtMucLucs.BHangCha;
                    }
                }

                return errors;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new List<ImportErrorItem>();
            }
        }
        #endregion

        #region On close
        public override void OnClose(object obj)
        {
            try
            {
                var window = obj as Window;
                window.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}
