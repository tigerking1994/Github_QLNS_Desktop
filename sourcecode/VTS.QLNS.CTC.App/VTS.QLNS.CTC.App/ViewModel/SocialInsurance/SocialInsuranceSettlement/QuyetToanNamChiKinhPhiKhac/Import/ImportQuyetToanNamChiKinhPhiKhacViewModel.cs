using AutoMapper;
using FlexCel.XlsAdapter;
using log4net;
using log4net.Util;
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
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.Import
{
    public class ImportQuyetToanNamChiKinhPhiKhacViewModel : ViewModelBase
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IBhQtcNamKinhPhiKhacService _namKinhPhiKhacService;
        private readonly IBhQtcNamKinhPhiKhacChiTietService _namKinhPhiKhacChiTietService;
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
        public override Type ContentType => typeof(ImportQuyetToanNamChiKinhPhiKhac);
        public override PackIconKind IconKind => PackIconKind.Import;
        private Dictionary<Guid, BhQtcNamKinhPhiKhacChiTiet> _dicChiTiet;
        private List<BhDmMucLucNganSachModel> _mergeItems;
        private List<BhDmMucLucNganSach> _nsBhYtMucLucs;
        private List<BhDmMucLucNganSach> _lsAllBhYtMucLucs;
        private BhQtcNamKPKChungTuDetailImportModel _selectedItem;
        public BhQtcNamKPKChungTuDetailImportModel SelectedItem
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
        private ObservableCollection<BhQtcNamKPKChungTuDetailImportModel> _items;
        public ObservableCollection<BhQtcNamKPKChungTuDetailImportModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private ObservableCollection<BhQtcNamKPKChungTuDetailImportModel> _itemsImport;
        public ObservableCollection<BhQtcNamKPKChungTuDetailImportModel> ItemsImport
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

        #region Constructor
        public ImportQuyetToanNamChiKinhPhiKhacViewModel(
               ISessionService sessionService,
               INsDonViService donViService,
               IMapper mapper,
               ILog logger,
               IImportExcelService importService,
               IBhDmMucLucNganSachService bhDmMucLucNganSachService,
               IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
               IBhQtcNamKinhPhiKhacService namKinhPhiKhacService,
               IBhQtcNamKinhPhiKhacChiTietService namKinhPhiKhacChiTietService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _logger = logger;
            _importService = importService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _namKinhPhiKhacService = namKinhPhiKhacService;
            _namKinhPhiKhacChiTietService = namKinhPhiKhacChiTietService;

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
                SSoChungTu = GetNextSoChungTu(_sessionInfo.YearOfWork);
                NgayChungTu = DateTime.Now;
                DNgayQuyetDinh = DateTime.Now;
                OnResetData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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
        }


        private string GetNextSoChungTu(int yearOfWork)
        {
            try
            {
                var soChungTuIndex = _namKinhPhiKhacService.GetSoChungTuIndexByCondition(yearOfWork);
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
                                                            || x.SLNS == LNSValue.LNS_9010010);

            listDanhMucLoaiChi = listDanhMucLoaiChi.OrderBy(x => x.SMaLoaiChi);
            if (listDanhMucLoaiChi != null)
            {
                ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenDanhMucLoaiChi,
                    ValueItem = n.Id.ToString(),
                    HiddenValue = n.SLNS,
                    Id = n.Id,
                }));

                SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.ElementAt(0);
            }

            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
        }

        private void LoadDonVi()
        {
            ItemsDonVi = new ObservableCollection<ComboboxItem>();
            listDonVi = _donViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDonVi != null)
            {
                ItemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.TenDonVi,
                    ValueItem = n.IIDMaDonVi,
                    HiddenValue = n.Loai,
                    Id = n.Id
                })); ;

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
                    _listErrChungTu = new List<ImportErrorItem>();
                    List<Guid> _lstIdQTCChiTiet = new List<Guid>();
                    bool isExist;
                    _dicChiTiet = new Dictionary<Guid, BhQtcNamKinhPhiKhacChiTiet>();
                    XlsFile xls = new XlsFile(false);
                    xls.Open(FilePath);
                    xls.ActiveSheet = 1;

                    string sLNSImport = string.Empty;
                    var kinhPhiKhacChiTiet = _namKinhPhiKhacChiTietService.FindAllChungTu();
                    var kinhPhiKhac = _namKinhPhiKhacService.FindIndex(_sessionInfo.YearOfWork).ToList();
                    var dataImport = _importService.ProcessData<BhQtcNamKPKChungTuDetailImportModel>(FilePath);
                    ItemsImport = new ObservableCollection<BhQtcNamKPKChungTuDetailImportModel>(dataImport.Data);
                    var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                    predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == _sessionInfo.YearOfWork);

                    List<string> lstError = new List<string>();
                    if (kinhPhiKhacChiTiet != null)
                    {
                        foreach (var item in kinhPhiKhacChiTiet)
                        {
                            if (!_lstIdQTCChiTiet.Contains(item.Id))
                            {
                                _lstIdQTCChiTiet.Add(item.Id);
                            }
                        }
                    }
                    if (dataImport.ImportErrors.Count > 0)
                    {
                        _listErrChungTu.AddRange(dataImport.ImportErrors);
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

                    if (SelectedDanhMucLoaiChi == null)
                    {
                        MessageBoxHelper.Error(Resources.AlertLoaiDuToanEmpty);
                        OnResetData();
                        return;
                    }


                    isExist = kinhPhiKhac.Any(x => x.IID_MaDonVi.Equals(SelectedDonVi.ValueItem) && x.IID_LoaiChi.Equals(SelectedDanhMucLoaiChi.Id));

                    if (isExist)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.AlertExistSettlementVoucher, SelectedDonVi.ValueItem, _sessionInfo.YearOfWork, SelectedDanhMucLoaiChi.DisplayItem));
                        OnResetData();
                        return;

                    }

                    var lstSLNS = SelectedDanhMucLoaiChi.HiddenValue.Split(',');
                    IEnumerable<BhDmMucLucNganSach> _nsMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => lstSLNS.Contains(x.SLNS))
                                                                            .OrderBy(x => x.SXauNoiMa).ToList();

                    List<BhQtcNamKPKChungTuDetailImportModel> listDetailImport = ItemsImport.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasData
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
                            if (args.PropertyName == nameof(BhQtcNamKPKChungTuDetailImportModel.SXauNoiMa))
                            {
                                var entityDetail = (BhQtcNamKPKChungTuDetailImportModel)sender;
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
                if (ex.Message == "Sai template")
                {
                    System.Windows.MessageBox.Show(Resources.FileImportWrongTemplate, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
                _logger.Error(ex.Message, ex);
            }
        }

        private List<ImportErrorItem> ValidateItem(BhQtcNamKPKChungTuDetailImportModel item, int rowIndex)
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

        #region OnDownloadTemplate
        private void OnDownloadTemplate()
        {
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTC_NKPK_IMPORT_TEMPLATE, ExportFileName.IMPORT_BH_QTC_NKPK_CHUNGTU_CHITIET);
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
            List<string> errors = _listErrChungTu.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region OnResetData
        private void OnResetData()
        {
            _mergeItems = new List<BhDmMucLucNganSachModel>();
            _filePath = string.Empty;
            _itemsImport = new ObservableCollection<BhQtcNamKPKChungTuDetailImportModel>();
            _items = new ObservableCollection<BhQtcNamKPKChungTuDetailImportModel>();
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

        #region On Save Data
        private void OnSaveData()
        {
            try
            {

                var namLamViec = _sessionInfo.YearOfWork;
                var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();

                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == namLamViec);
                IEnumerable<BhDmMucLucNganSach> _nsMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).OrderBy(x => x.SXauNoiMa).ToList();


                var lstLNS = SelectedDanhMucLoaiChi.HiddenValue.Split(",");

                _nsMucLucs = _nsMucLucs.Where(x => lstLNS.Contains(x.SLNS)).ToList();

                BhQtcNamKinhPhiKhac kinhPhiKhac = new BhQtcNamKinhPhiKhac();
                kinhPhiKhac.SSoQuyetDinh = string.Empty;
                kinhPhiKhac.SSoChungTu = SSoChungTu;
                kinhPhiKhac.BIsKhoa = false;
                kinhPhiKhac.DNgayTao = DateTime.Now;
                kinhPhiKhac.DNgayChungTu = DateTime.Now;
                kinhPhiKhac.DNgayQuyetDinh = DNgayQuyetDinh;
                kinhPhiKhac.SNguoiTao = _sessionInfo.Principal;
                kinhPhiKhac.BThucChiTheo4Quy = false;
                kinhPhiKhac.INamLamViec = namLamViec;
                kinhPhiKhac.SDSLNS = SelectedDanhMucLoaiChi.HiddenValue;
                kinhPhiKhac.IID_DonVi = SelectedDonVi.Id;
                kinhPhiKhac.IID_MaDonVi = SelectedDonVi.ValueItem;
                kinhPhiKhac.IID_LoaiChi = SelectedDanhMucLoaiChi.Id;
                if (SelectedDonVi.HiddenValue == LoaiDonVi.ROOT)
                {
                    kinhPhiKhac.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTuTongHop;
                    kinhPhiKhac.IID_TongHopID = Guid.NewGuid();
                }
                else
                {
                    kinhPhiKhac.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTu;
                }

                _namKinhPhiKhacService.Add(kinhPhiKhac);

                List<BhQtcNamKinhPhiKhacChiTiet> kinhPhiKhacChiTiets = new List<BhQtcNamKinhPhiKhacChiTiet>();
                List<BhQtcNamKPKChungTuDetailImportModel> lstDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasData
                                                                                && _nsMucLucs.Any(y => !y.BHangCha && y.SXauNoiMa == x.SXauNoiMa)).ToList();

                var lstDuToan = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasData && !string.IsNullOrEmpty(x.SDuToanChiTietToi)
                && _nsMucLucs.Any(y => y.SXauNoiMa == x.SXauNoiMa)).ToList();

                foreach (var item in lstDetailImport)
                {
                    BhDmMucLucNganSach mucLuc = _nsBhYtMucLucs.FirstOrDefault(x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE
                    && item.SNoiDung == x.SMoTa && item.SXauNoiMa == x.SXauNoiMa);

                    if (mucLuc == null)
                    {
                        continue;
                    }

                    BhQtcNamKinhPhiKhacChiTiet kinhPhiKhacChiTiet = new BhQtcNamKinhPhiKhacChiTiet();
                    kinhPhiKhacChiTiet.IID_QTC_Nam_KPK = kinhPhiKhac.Id;
                    kinhPhiKhacChiTiet.IID_MucLucNganSach = mucLuc.IIDMLNS;
                    kinhPhiKhacChiTiet.SNoiDung = mucLuc.SMoTa;
                    kinhPhiKhacChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    kinhPhiKhacChiTiet.IIdMaDonVi = SelectedDonVi.ValueItem;
                    kinhPhiKhacChiTiet.INamLamViec = _sessionInfo.YearOfWork;
                    kinhPhiKhacChiTiet.FTien_DuToanNamTruocChuyenSang = ConvertStringToNumber(item.STienNamTruocChuyenSang);
                    kinhPhiKhacChiTiet.FTien_DuToanGiaoNamNay = ConvertStringToNumber(item.STienDuToanDuocGiao);
                    kinhPhiKhacChiTiet.FTien_TongDuToanDuocGiao = ConvertStringToNumber(item.STienTongCong);
                    kinhPhiKhacChiTiet.FTien_ThucChi = ConvertStringToNumber(item.STienSoThucChiCaNam);
                    kinhPhiKhacChiTiet.FTienThieu = ConvertStringToNumber(item.STienThieu);
                    kinhPhiKhacChiTiet.FTienThua = ConvertStringToNumber(item.STienThua);
                    kinhPhiKhacChiTiet.FTiLeThucHienTrenDuToan = ConvertStringToNumber(item.STiLe);
                    kinhPhiKhacChiTiet.DNgayTao = DateTime.Now;
                    kinhPhiKhacChiTiet.SNguoiTao = _sessionInfo.Principal;


                    kinhPhiKhacChiTiets.Add(kinhPhiKhacChiTiet);
                }

                foreach (var item in lstDuToan)
                {
                    BhDmMucLucNganSach mucLuc = _nsBhYtMucLucs.FirstOrDefault(x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE
                   && item.SNoiDung == x.SMoTa && item.SXauNoiMa == x.SXauNoiMa);

                    if (mucLuc == null)
                    {
                        continue;
                    }

                    BhQtcNamKinhPhiKhacChiTiet kinhPhiKhacChiTiet = new BhQtcNamKinhPhiKhacChiTiet();
                    kinhPhiKhacChiTiet.IID_QTC_Nam_KPK = kinhPhiKhac.Id;
                    kinhPhiKhacChiTiet.IID_MucLucNganSach = mucLuc.IIDMLNS;
                    kinhPhiKhacChiTiet.SNoiDung = mucLuc.SMoTa;
                    kinhPhiKhacChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    kinhPhiKhacChiTiet.IIdMaDonVi = SelectedDonVi.ValueItem;
                    kinhPhiKhacChiTiet.INamLamViec = _sessionInfo.YearOfWork;
                    //kinhPhiKhacChiTiet.FTien_DuToanNamTruocChuyenSang = ConvertStringToNumber(item.STienNamTruocChuyenSang);
                    kinhPhiKhacChiTiet.FTien_DuToanGiaoNamNay = ConvertStringToNumber(item.STienDuToanDuocGiao);
                    //kinhPhiKhacChiTiet.FTien_TongDuToanDuocGiao = ConvertStringToNumber(item.STienTongCong);
                    //kinhPhiKhacChiTiet.FTien_ThucChi = ConvertStringToNumber(item.STienSoThucChiCaNam);
                    //kinhPhiKhacChiTiet.FTienThieu = ConvertStringToNumber(item.STienThieu);
                    //kinhPhiKhacChiTiet.FTienThua = ConvertStringToNumber(item.STienThua);
                    //kinhPhiKhacChiTiet.FTiLeThucHienTrenDuToan = ConvertStringToNumber(item.STiLe);
                    kinhPhiKhacChiTiet.DNgayTao = DateTime.Now;
                    kinhPhiKhacChiTiet.SNguoiTao = _sessionInfo.Principal;


                    kinhPhiKhacChiTiets.Add(kinhPhiKhacChiTiet);
                }
                _namKinhPhiKhacChiTietService.AddRange(kinhPhiKhacChiTiets);

                if (kinhPhiKhacChiTiets.Any())
                {
                    kinhPhiKhac.FTongTien_DuToanNamTruocChuyenSang = kinhPhiKhacChiTiets.Sum(x => x.FTien_DuToanNamTruocChuyenSang);
                    kinhPhiKhac.FTongTien_DuToanGiaoNamNay = kinhPhiKhacChiTiets.Sum(x => x.FTien_DuToanGiaoNamNay);
                    kinhPhiKhac.FTongTien_ThucChi = kinhPhiKhacChiTiets.Sum(x => x.FTien_ThucChi);
                    kinhPhiKhac.FTongTien_TongDuToanDuocGiao = kinhPhiKhacChiTiets.Sum(x => x.FTien_DuToanNamTruocChuyenSang) + kinhPhiKhacChiTiets.Sum(x => x.FTien_DuToanGiaoNamNay);
                    kinhPhiKhac.FTongTienThua = (kinhPhiKhac.FTongTien_TongDuToanDuocGiao > kinhPhiKhac.FTongTien_ThucChi) ? kinhPhiKhac.FTongTien_TongDuToanDuocGiao - kinhPhiKhac.FTongTien_ThucChi : 0;
                    kinhPhiKhac.FTongTienThieu = (kinhPhiKhac.FTongTien_ThucChi > kinhPhiKhac.FTongTien_TongDuToanDuocGiao) ? kinhPhiKhac.FTongTien_ThucChi - kinhPhiKhac.FTongTien_TongDuToanDuocGiao : 0;
                    kinhPhiKhac.FTiLeThucHienTrenDuToan = kinhPhiKhac.FTongTien_TongDuToanDuocGiao > 0 ? (kinhPhiKhac.FTongTien_ThucChi / kinhPhiKhac.FTongTien_TongDuToanDuocGiao) * 100 : 0;

                    _namKinhPhiKhacService.Update(kinhPhiKhac);
                }

                BhQtcNamKinhPhiKhacModel kinhPhiKhacModel = _mapper.Map<BhQtcNamKinhPhiKhacModel>(kinhPhiKhac);
                DialogHost.CloseDialogCommand.Execute(kinhPhiKhacModel, null);
                SavedAction?.Invoke(kinhPhiKhacModel);
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private double? ConvertStringToNumber(string strNumber)
        {
            double result = 0;
            try
            {
                if (strNumber == null) return result;
                if (double.TryParse(strNumber, out result))
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
