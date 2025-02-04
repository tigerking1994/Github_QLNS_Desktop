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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.Import
{
    public class ImportQuyetToanChiQuyKinhPhiKhacViewModel : ViewModelBase
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IBhQtcQuyKPKService _quyKCBService;
        private readonly IBhQtcQuyKPKChiTietService _quyKCBChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private ImpHistory _impHistory;
        #endregion

        #region Property
        private bool _isUploadFile;
        IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi;
        IEnumerable<DonVi> listDonVi;
        private List<ImportErrorItem> _listErrChungTu = new List<ImportErrorItem>();
        public override string Name => "IMPORT DỮ LIỆU CHỨNG TỪ QUYẾT TOÁN QUÝ CHI KINH PHÍ KHÁC " + _sessionInfo.YearOfWork;
        public override Type ContentType => typeof(ImportQuyetToanChiQuyKinhPhiKhac);
        public override PackIconKind IconKind => PackIconKind.Import;
        private Dictionary<Guid, BhQtcQuyKPKChiTiet> _dicChiTiet;
        private List<BhDmMucLucNganSachModel> _mergeItems;
        private List<BhDmMucLucNganSach> _nsBhYtMucLucs;
        private List<BhDmMucLucNganSach> _lsAllBhYtMucLucs;
        private BhQtcQuyKPKDetailImportModel _selectedItem;
        public BhQtcQuyKPKDetailImportModel SelectedItem
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
                _isSaveData = (Items.Any() && !_listErrChungTu.Any());
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
        private ObservableCollection<BhQtcQuyKPKDetailImportModel> _items;
        public ObservableCollection<BhQtcQuyKPKDetailImportModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
        }
        private ObservableCollection<BhQtcQuyKPKDetailImportModel> _itemsImport;
        public ObservableCollection<BhQtcQuyKPKDetailImportModel> ItemsImport
        {
            get => _itemsImport;
            set
            {
                SetProperty(ref _itemsImport, value);
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
        private DateTime? _ngayChungTu;
        public DateTime? NgayChungTu
        {
            get => _ngayChungTu;
            set => SetProperty(ref _ngayChungTu, value);
        }
        private string _sSoChungTu;
        public string SSoChungTu
        {
            get => _sSoChungTu;
            set => SetProperty(ref _sSoChungTu, value);
        }
        public string MoTa { get; set; }
        private ComboboxItem _selectedDanhMucLoaiChi;
        public ComboboxItem SelectedDanhMucLoaiChi
        {
            get => _selectedDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selectedDanhMucLoaiChi, value);
                if (value != null)
                {
                    _isUploadFile = true;
                    ReloadMLNS();
                    OnPropertyChanged(nameof(ExistedMlns));
                }
            }
        }
        private ObservableCollection<ComboboxItem> _itemsDanhMucLoaiChi;
        public ObservableCollection<ComboboxItem> ItemsDanhMucLoaiChi
        {
            get => _itemsDanhMucLoaiChi;
            set => SetProperty(ref _itemsDanhMucLoaiChi, value);
        }
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

        #region Constructer
        public ImportQuyetToanChiQuyKinhPhiKhacViewModel(
               ISessionService sessionService,
               INsDonViService donViService,
               IMapper mapper,
               ILog logger,
               IImportExcelService importService,
               IBhDmMucLucNganSachService bhDmMucLucNganSachService,
               IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
               IBhQtcQuyKPKService bhQtcQuyKPKService,
               IBhQtcQuyKPKChiTietService bhQtcQuyKPKChiTietService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _logger = logger;
            _importService = importService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _quyKCBService = bhQtcQuyKPKService;
            _quyKCBChiTietService = bhQtcQuyKPKChiTietService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            DownloadTemplateCommand = new RelayCommand(obj => OnDownloadTemplate());
            HandleDataCommand = new RelayCommand(obj => OnCheckFileImport());
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
                LoadDanhMucLoaiChi();
                LoadQuarters();
                SSoChungTu = GetSoChungTu();
                OnResetData();
                NgayChungTu = DateTime.Now;
                DNgayQuyetDinh = DateTime.Now;
                OnPropertyChanged(nameof(SSoChungTu));

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string GetSoChungTu()
        {
            try
            {
                var soChungTuIndex = _quyKCBService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                var soChungTu = "QTC-" + soChungTuIndex.ToString("D3");
                return soChungTu;
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
                return string.Empty;
            }

        }
        #endregion

        #region Load data
        private void LoadDanhMucLoaiChi()
        {
            ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
            listDanhMucLoaiChi = null;
            listDanhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            listDanhMucLoaiChi = listDanhMucLoaiChi.Where(x => x.SLNS == LNSValue.LNS_9010006_9010007
                                                                     || x.SLNS == LNSValue.LNS_9050001_9050002
                                                                     || x.SLNS == LNSValue.LNS_9010008
                                                                     || x.SLNS == LNSValue.LNS_9010009
                                                                     || x.SLNS == LNSValue.LNS_9010010).OrderBy(x => x.SMaLoaiChi).ToList();
            if (listDanhMucLoaiChi != null)
            {
                ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenDanhMucLoaiChi,
                    ValueItem = n.Id.ToString(),
                    HiddenValue = n.SLNS,
                    Id = n.Id,
                }));
            }
            SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.ElementAt(0);
            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
        }

        private void LoadQuarters()
        {
            _quarters = FnCommonUtils.LoadQuarters();
            if (_quarters.Count > 0)
            {
                QuartersSelected = Quarters.ElementAt(0);
            }
        }

        private void LoadDonVi()
        {
            ItemsDonVi = new ObservableCollection<ComboboxItem>();
            listDonVi = _donViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDonVi != null)
            {
                listDonVi = listDonVi.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                ItemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.TenDonVi,
                    ValueItem = n.IIDMaDonVi,
                    HiddenValue = n.Loai,
                    Id = n.Id
                })); ;
            }
            SelectedDonVi = ItemsDonVi.ElementAt(0);
            OnPropertyChanged(nameof(ItemsDonVi));

        }

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

                if (_listErrChungTu != null && _listErrChungTu.Any())
                {
                    _listErrChungTu = new List<ImportErrorItem>();
                    foreach (var item in Items)
                    {
                        var rowIndex = Items.IndexOf(item);
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

                    OnPropertyChanged(nameof(IsSaveData));
                }
                else
                {

                    if (Items.Count > 0 && !_listErrChungTu.Any() && !_isUploadFile)
                    {
                        OnPropertyChanged(nameof(IsSaveData));
                        return;
                    }

                    _isUploadFile = false;
                    List<Guid> _lstIdBhxhChiTiet = new List<Guid>();
                    var kinhPhiQuanLyChiTiet = _quyKCBChiTietService.FindAllChungTu();
                    var kinhPhiQuanLy = _quyKCBService.FindIndex(_sessionInfo.YearOfWork).ToList();

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

                    _dicChiTiet = new Dictionary<Guid, BhQtcQuyKPKChiTiet>();
                    XlsFile xls = new XlsFile(false);
                    xls.Open(FilePath);
                    xls.ActiveSheet = 1;

                    string sLNSImport = string.Empty;
                    var dataImport = _importService.ProcessData<BhQtcQuyKPKDetailImportModel>(FilePath);
                    ItemsImport = new ObservableCollection<BhQtcQuyKPKDetailImportModel>(dataImport.Data);
                    var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                    predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == _sessionInfo.YearOfWork);

                    List<string> lstError = new List<string>();

                    if (dataImport.ImportErrors.Count > 0)
                    {
                        _listErrChungTu.AddRange(dataImport.ImportErrors);
                    }

                    if (ItemsImport == null || ItemsImport.Count <= 0)
                    {
                        System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    if (ItemsImport.Any(x => !x.ImportStatus))
                    {
                        System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    if (string.IsNullOrEmpty(FilePath))
                    {
                        lstError.Add(Resources.ErrorFileEmpty);
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


                    isExist = kinhPhiQuanLy.Any(x => x.IID_MaDonVi.Equals(SelectedDonVi.ValueItem)
                                                && x.IQuyChungTu == int.Parse(QuartersSelected.ValueItem)
                                                && x.IID_LoaiChi.Equals(SelectedDanhMucLoaiChi.Id));

                    if (isExist)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.AlertExistSettlementQuarterVoucher, SelectedDonVi.ValueItem, QuartersSelected.ValueItem, SelectedDanhMucLoaiChi.HiddenValue));
                        OnResetData();
                        return;

                    }

                    var lstSLNS = SelectedDanhMucLoaiChi.HiddenValue.Split(',');
                    IEnumerable<BhDmMucLucNganSach> _nsMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => lstSLNS.Contains(x.SLNS))
                                                                            .OrderBy(x => x.SXauNoiMa).ToList();

                    List<BhQtcQuyKPKDetailImportModel> listDetailImport = ItemsImport.Where(x => x.ImportStatus && !x.IsWarning && x.IsDataNotNull
                                                                        && _nsMucLucs.Any(y => !y.BHangCha && y.SXauNoiMa == x.SXauNoiMa)).ToList();
                    if (listDetailImport.Count <= 0)
                    {
                        System.Windows.MessageBox.Show(Resources.MsgCheckLuaChonLaiLoaiKP, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        OnResetData();
                        return;
                    }

                    var lstMucLucMap = _nsMucLucs.Where(x => string.IsNullOrEmpty(x.SDuToanChiTietToi)).ToList();

                    foreach (var item in ItemsImport)
                    {
                        foreach (var itemMap in lstMucLucMap)
                        {
                            if (item.SXauNoiMa == itemMap.SXauNoiMa)
                            {
                                item.IsHangCha = itemMap.BHangCha;
                            }
                        }
                    }

                    Items = ItemsImport;

                    foreach (var item in Items)
                    {
                        item.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(BhQtcQuyKPKDetailImportModel.SXauNoiMa))
                            {
                                var entityDetail = (BhQtcQuyKPKDetailImportModel)sender;
                                var rowIndex = Items.IndexOf(entityDetail);
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
                        System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    System.Windows.MessageBox.Show(Resources.ErrorImport, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    _logger.Error(ex.Message, ex);
                }
            }
        }

        private List<ImportErrorItem> ValidateItem(BhQtcQuyKPKDetailImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                var yearOfWork = _sessionService.Current.YearOfWork;
                var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);

                _lsAllBhYtMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).ToList();
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

        #region DowloadTemplate
        private void OnDownloadTemplate()
        {
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTC_QUYKPK_IMPORT_TEMPLATE, ExportFileName.IMPORT_BH_QTC_QKPK_CHUNGTU_CHITIET);
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

        #region Show Error
        private void ShowError()
        {
            int rowIndex = Items.IndexOf(SelectedItem);
            List<string> errors = _listErrChungTu.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region Reset data
        private void OnResetData()
        {
            _mergeItems = new List<BhDmMucLucNganSachModel>();
            _filePath = string.Empty;
            _items = new ObservableCollection<BhQtcQuyKPKDetailImportModel>();
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);
            var lstSLNS = SelectedDanhMucLoaiChi.HiddenValue.Split(',');
            _nsBhYtMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => lstSLNS.Contains(x.SLNS)).ToList();
            _importedMlns = new ObservableCollection<BhDmMucLucNganSachModel>();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_nsBhYtMucLucs.OrderBy(x => x.SXauNoiMa)));
            _impHistory = new ImpHistory();
            _listErrChungTu = new List<ImportErrorItem>();
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

        private void ReloadMLNS()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);
            var lstSLNS = SelectedDanhMucLoaiChi.HiddenValue.Split(',');
            _nsBhYtMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => lstSLNS.Contains(x.SLNS)).ToList();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_nsBhYtMucLucs.OrderBy(x => x.SXauNoiMa)));

        }
        #endregion

        #region On Save data
        private void OnSaveData()
        {

            try
            {
                var namLamViec = _sessionInfo.YearOfWork;
                var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == namLamViec);
                IEnumerable<BhDmMucLucNganSach> _nsMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc)
                                                                                        .OrderBy(x => x.SXauNoiMa).ToList();

                var lstLNS = SelectedDanhMucLoaiChi.HiddenValue.Split(",");

                _nsMucLucs = _nsMucLucs.Where(x => lstLNS.Contains(x.SLNS)).ToList();
                BhQtcQuyKPK chungTu = new BhQtcQuyKPK();
                chungTu.SSoChungTu = SSoChungTu;
                chungTu.IID_DonVi = SelectedDonVi.Id;
                chungTu.IID_MaDonVi = SelectedDonVi.ValueItem;
                chungTu.IID_LoaiChi = SelectedDanhMucLoaiChi.Id;
                chungTu.DNgayChungTu = NgayChungTu == null ? DateTime.Now : NgayChungTu;
                chungTu.IQuyChungTu = int.Parse(QuartersSelected.ValueItem);
                chungTu.SDSLNS = SelectedDanhMucLoaiChi.HiddenValue;
                chungTu.SMoTa = string.Empty;
                chungTu.INamChungTu = namLamViec;
                chungTu.DNgayTao = DateTime.Now;
                chungTu.SNguoiTao = _sessionInfo.Principal;
                if (SelectedDonVi.HiddenValue == LoaiDonVi.ROOT)
                {
                    chungTu.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTuTongHop;
                    chungTu.IID_TongHopID = Guid.NewGuid();
                }
                else
                {
                    chungTu.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTu;
                }

                _quyKCBService.Add(chungTu);

                List<BhQtcQuyKPKChiTiet> chungTuChiTiets = new List<BhQtcQuyKPKChiTiet>();

                List<BhQtcQuyKPKDetailImportModel> listDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsDataNotNull
                && _nsMucLucs.Any(y => !y.BHangCha && y.SXauNoiMa == x.SXauNoiMa)).ToList();

                var lstDuToan = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsDataNotNull && !string.IsNullOrEmpty(x.SDuToanChiTietToi)
               && _nsMucLucs.Any(y => y.SXauNoiMa == x.SXauNoiMa)).ToList();

                foreach (var item in listDetailImport)
                {
                    BhDmMucLucNganSach mucLuc = _nsMucLucs.FirstOrDefault(x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE
                    && item.SNoiDung == x.SMoTa && item.SXauNoiMa == x.SXauNoiMa);
                    if (mucLuc == null)
                    {
                        continue;
                    }

                    BhQtcQuyKPKChiTiet kCBChiTiet = new BhQtcQuyKPKChiTiet();
                    kCBChiTiet.IID_QTC_Quy_KPK = chungTu.Id;
                    kCBChiTiet.IID_MucLucNganSach = mucLuc.IIDMLNS;
                    kCBChiTiet.SNoiDung = mucLuc.SMoTa;
                    kCBChiTiet.DNgayTao = DateTime.Now;
                    kCBChiTiet.SNguoiTao = _sessionInfo.Principal;
                    kCBChiTiet.IIdMaDonVi = SelectedDonVi.ValueItem;
                    kCBChiTiet.INamLamViec = _sessionInfo.YearOfWork;
                    kCBChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    kCBChiTiet.FTien_DuToanNamTruocChuyenSang = ConvertStringToNumber(item.STienNamTruocChuyenSang);
                    //kCBChiTiet.FTien_DuToanGiaoNamNay = ConvertStringToNumber(item.STienNamNayDuocGiao);
                    kCBChiTiet.FTien_TongDuToanDuocGiao = ConvertStringToNumber(item.STienTongCong);
                    kCBChiTiet.FTienThucChi = ConvertStringToNumber(item.STienThucChi);
                    kCBChiTiet.FTienQuyetToanDaDuyet = ConvertStringToNumber(item.STienDaDuyet);
                    kCBChiTiet.FTienDeNghiQuyetToanQuyNay = ConvertStringToNumber(item.STienDeNghiQuyetToanQuy);
                    kCBChiTiet.FTienXacNhanQuyetToanQuyNay = ConvertStringToNumber(item.STienXacNhanQuy);

                    chungTuChiTiets.Add(kCBChiTiet);
                }

                //foreach (var item in lstDuToan)
                //{
                //    BhDmMucLucNganSach mucLuc = _nsMucLucs.FirstOrDefault(x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE
                //    && item.SNoiDung == x.SMoTa && item.SXauNoiMa == x.SXauNoiMa);
                //    if (mucLuc == null)
                //    {
                //        continue;
                //    }

                //    BhQtcQuyKPKChiTiet kCBChiTiet = new BhQtcQuyKPKChiTiet();
                //    kCBChiTiet.IID_QTC_Quy_KPK = chungTu.Id;
                //    kCBChiTiet.IID_MucLucNganSach = mucLuc.IIDMLNS;
                //    kCBChiTiet.SNoiDung = mucLuc.SMoTa;
                //    kCBChiTiet.DNgayTao = DateTime.Now;
                //    kCBChiTiet.SNguoiTao = _sessionInfo.Principal;
                //    kCBChiTiet.IIdMaDonVi = SelectedDonVi.ValueItem;
                //    kCBChiTiet.INamLamViec = _sessionInfo.YearOfWork;
                //    kCBChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                //    //kCBChiTiet.FTien_DuToanNamTruocChuyenSang = ConvertStringToNumber(item.STienNamTruocChuyenSang);
                //    kCBChiTiet.FTien_DuToanGiaoNamNay = ConvertStringToNumber(item.STienNamNayDuocGiao);
                //    //kCBChiTiet.FTien_TongDuToanDuocGiao = ConvertStringToNumber(item.STienTongCong);
                //    //kCBChiTiet.FTienThucChi = ConvertStringToNumber(item.STienThucChi);
                //    //kCBChiTiet.FTienQuyetToanDaDuyet = ConvertStringToNumber(item.STienDaDuyet);
                //    //kCBChiTiet.FTienDeNghiQuyetToanQuyNay = ConvertStringToNumber(item.STienDeNghiQuyetToanQuy);
                //    //kCBChiTiet.FTienXacNhanQuyetToanQuyNay = ConvertStringToNumber(item.STienXacNhanQuy);

                //    chungTuChiTiets.Add(kCBChiTiet);
                //}

                _quyKCBChiTietService.AddRange(chungTuChiTiets);

                string sumDuToanNamTruocChuyenSang = string.IsNullOrWhiteSpace(Items.Where(x => x.SXauNoiMa == LNSValue.LNS_9010004_9010005).FirstOrDefault().STienNamTruocChuyenSang?.ToString())
                                    ? "0" : Items.Where(x => x.SXauNoiMa == LNSValue.LNS_9010004_9010005).FirstOrDefault().STienNamTruocChuyenSang?.ToString();

                string sumDuToanGiaoNamNay = string.IsNullOrWhiteSpace(Items.Where(x => x.SXauNoiMa == LNSValue.LNS_9010004_9010005).FirstOrDefault().STienNamNayDuocGiao?.ToString())
                                                    ? "0" : Items.Where(x => x.SXauNoiMa == LNSValue.LNS_9010004_9010005).FirstOrDefault().STienNamNayDuocGiao?.ToString();

                if (chungTuChiTiets.Count > 0)
                {
                    chungTu.FTongTien_DuToanNamTruocChuyenSang = Convert.ToDouble(sumDuToanNamTruocChuyenSang);
                    chungTu.FTongTien_DuToanGiaoNamNay = Convert.ToDouble(sumDuToanGiaoNamNay);
                    chungTu.FTongTien_TongDuToanDuocGiao = chungTu.FTongTien_DuToanNamTruocChuyenSang + chungTu.FTongTien_DuToanGiaoNamNay;
                    chungTu.FTongTienThucChi = chungTuChiTiets.Sum(x => x.FTienThucChi);
                    chungTu.FTongTienQuyetToanDaDuyet = chungTuChiTiets.Sum(x => x.FTienQuyetToanDaDuyet);
                    chungTu.FTongTienDeNghiQuyetToanQuyNay = chungTuChiTiets.Sum(x => x.FTienDeNghiQuyetToanQuyNay);
                    chungTu.FTongTienXacNhanQuyetToanQuyNay = chungTuChiTiets.Sum(x => x.FTienXacNhanQuyetToanQuyNay);

                    _quyKCBService.Update(chungTu);
                }

                BhQtcQuyKPKModel bhQtcQuyKModel = _mapper.Map<BhQtcQuyKPKModel>(chungTu);
                DialogHost.CloseDialogCommand.Execute(bhQtcQuyKModel, null);
                SavedAction?.Invoke(bhQtcQuyKModel);
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
        #endregion

        #region Upload file
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
                _listErrChungTu.Clear();
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
        #endregion
    }
}
