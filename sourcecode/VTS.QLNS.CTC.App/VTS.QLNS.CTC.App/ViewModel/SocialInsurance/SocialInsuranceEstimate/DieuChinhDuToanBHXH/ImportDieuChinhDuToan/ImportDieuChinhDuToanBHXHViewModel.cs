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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH.ImportDieuChinhDuToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH.ImportDieuChinhDuToan
{
    public class ImportDieuChinhDuToanBHXHViewModel : ViewModelBase
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IBhDtcDcdToanChiService _bhDtcDcdToanChiService;
        private readonly IBhDtcDcdToanChiChiTietService _bhDtcDcdToanChiChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDanhMucLoaiChiService _bhMucLoaiChiService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        #endregion

        #region Property
        public bool IsUploadFile;
        IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi;
        public override string Name => "IMPORT ĐIỀU CHỈNH DỰ TOÁN ";
        public override string Description => "Import điều chỉnh dự toán " + _sessionInfo.YearOfWork;
        public override Type ContentType => typeof(ImportDieuChinhDuToanBHXH);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private Dictionary<Guid, BhDtcDcdToanChiChiTiet> _dicDcDuToanChiTiet;
        private List<BhDmMucLucNganSachModel> _mergeItems;
        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        private List<ImportErrorItem> _importErrors;
        private IEnumerable<BhDmMucLucNganSach> _nsMucLucs;
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

        private DtcDcDuToanChiDetailImportModel _selectedItem;
        public DtcDcDuToanChiDetailImportModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        private DateTime? _dNgayChungTu;
        public DateTime? DngayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }

        public bool _isSelectedFile;
        public bool IsSelectedFile
        {
            get => !string.IsNullOrEmpty(_filePath);
            set => SetProperty(ref _isSelectedFile, value);
        }

        private string _soChungTu;
        public string SoChungTu
        {
            get => _soChungTu;
            set => SetProperty(ref _soChungTu, value);
        }
        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }


        private bool _isSaveData;
        public bool IsSaveData
        {
            get
            {
                _isSaveData = Items != null && Items.Count > 0 && SelectedDonVi != null && !_importErrors.Any();
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

        private ObservableCollection<DtcDcDuToanChiDetailImportModel> _itemsImport;
        public ObservableCollection<DtcDcDuToanChiDetailImportModel> ItemsImport
        {
            get => _itemsImport;
            set
            {
                SetProperty(ref _itemsImport, value);

            }
        }

        private ObservableCollection<DtcDcDuToanChiDetailImportModel> _items;
        public ObservableCollection<DtcDcDuToanChiDetailImportModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }

        private ComboboxItem _selectedDanhMucLoaiChi;
        public ComboboxItem SelectedDanhMucLoaiChi
        {
            get => _selectedDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selectedDanhMucLoaiChi, value);
                if (value != null)
                {
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

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        public DateTime NgayChungTu { get; set; }
        public string MoTa { get; set; }

        #endregion

        #region Model
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
        public ImportDieuChinhDuToanBHXHViewModel(
            ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            IBhDtcDcdToanChiService bhDtcDcdToanChiService,
            IBhDtcDcdToanChiChiTietService bhDtcDcdToanChiChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            ILog logger)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _bhDtcDcdToanChiService = bhDtcDcdToanChiService;
            _bhDtcDcdToanChiChiTietService = bhDtcDcdToanChiChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhMucLoaiChiService = bhDanhMucLoaiChiService;
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
            LoadDanhMucLoaiChi();
            SoChungTu = GetSoChungTu();
            LoadDonVi();
            DngayChungTu = DateTime.Now;
            OnResetData();
        }
        #endregion

        #region Load data
        private void LoadDanhMucLoaiChi()
        {
            ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
            listDanhMucLoaiChi = null;
            listDanhMucLoaiChi = _bhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDanhMucLoaiChi != null)
            {
                ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenDanhMucLoaiChi,
                    ValueItem = n.SMaLoaiChi.ToString(),
                    HiddenValue = n.SLNS.ToString(),
                    Id = n.Id
                }));
                SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.ElementAt(0);
            }

            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
        }
        private void LoadDonVi()
        {
            ItemsDonVi = new ObservableCollection<ComboboxItem>();
            IEnumerable<DonVi> listDonVi = _donViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDonVi != null)
            {
                listDonVi = listDonVi.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                ItemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.TenDonVi,
                    ValueItem = n.IIDMaDonVi,
                    HiddenValue = n.Loai,
                    Id = n.Id
                }));
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

                if (_importErrors != null && _importErrors.Any())
                {
                    _importErrors = new List<ImportErrorItem>();
                    foreach (var item in Items)
                    {
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
                        IsUploadFile = false;
                        var messageOfRow = _importErrors.Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).Distinct();
                        System.Windows.MessageBox.Show(string.Join(Environment.NewLine, messageOfRow), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);

                    }

                    OnPropertyChanged(nameof(IsSaveData));
                }
                else
                {

                    if (Items.Count > 0 && !_importErrors.Any() && !IsUploadFile)
                    {
                        OnPropertyChanged(nameof(IsSaveData));
                        return;
                    }

                    IsUploadFile = false;
                    List<Guid> _lstIdBhxhChiTiet = new List<Guid>();
                    string sLNSImport = string.Empty;
                    var predicateSummaryDetail = PredicateBuilder.True<BhDtcDcdToanChiChiTiet>();
                    predicateSummaryDetail = predicateSummaryDetail.And(x => x.IID_BH_DTC.IsNullOrEmpty());
                    var BhxhChiTiet = _bhDtcDcdToanChiChiTietService.FindAll(predicateSummaryDetail);
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

                    _dicDcDuToanChiTiet = new Dictionary<Guid, BhDtcDcdToanChiChiTiet>();
                    XlsFile xls = new XlsFile(false);
                    xls.Open(FilePath);
                    xls.ActiveSheet = 1;
                    var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                    predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                    _nsMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).OrderBy(x => x.SXauNoiMa).ToList();
                    var sLNS = listDanhMucLoaiChi.Where(x => x.Id == SelectedDanhMucLoaiChi.Id).Select(x => x.SLNS).FirstOrDefault();
                    var dataSLNS = sLNS.Split(',');
                    _nsMucLucs = _nsMucLucs.Where(x => dataSLNS.Contains(x.SLNS)).ToList();

                    var dataImport = _importService.ProcessData<DtcDcDuToanChiDetailImportModel>(FilePath);
                    ItemsImport = new ObservableCollection<DtcDcDuToanChiDetailImportModel>(dataImport.Data);

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

                    List<DtcDcDuToanChiDetailImportModel> listDetailImport = ItemsImport.Where(x => x.ImportStatus && !x.IsWarning && x.IsDataNotNull
                   && _nsMucLucs.Any(y => y.SXauNoiMa == x.SXauNoiMa)).ToList();

                    if (listDetailImport.Count <= 0)
                    {
                        System.Windows.MessageBox.Show(Resources.MsgCheckLuaChonLaiLoaiKP, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
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
                        BhDmMucLucNganSach mucLuc = _nsMucLucs.FirstOrDefault(x => item.STenMLNS == x.SMoTa && item.SXauNoiMa == x.SXauNoiMa);
                        if (mucLuc == null)
                        {
                            continue;
                        }
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
                            if (args.PropertyName == nameof(DtcDcDuToanChiDetailImportModel.SXauNoiMa))
                            {
                                var entityDetail = (DtcDcDuToanChiDetailImportModel)sender;
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

        private IEnumerable<ImportErrorItem> ValidateItem(DtcDcDuToanChiDetailImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                var listMLNS = _nsMucLucs.Select(x => x.SXauNoiMa).ToList();

                if (!listMLNS.Contains(item.SXauNoiMa))
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

        private string GetSoChungTu()
        {
            var soChungTuIndex = _bhDtcDcdToanChiService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
            return "DC-" + soChungTuIndex.ToString("D3");
        }
        #endregion

        #region Download template
        private void OnDownloadTemplate()
        {
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_DT_DCDT_IMPORT_TEMPLATE, ExportFileName.IMPORT_BH_DT_DCDT_CHUNGTU_CHITIET_BHXH);
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

        #region Check error and Reset data
        private void ShowError()
        {
            int rowIndex = _items.IndexOf(SelectedItem);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnResetData()
        {
            _filePath = string.Empty;
            _importErrors = new List<ImportErrorItem>();
            _mergeItems = new List<BhDmMucLucNganSachModel>();
            ItemsImport = new ObservableCollection<DtcDcDuToanChiDetailImportModel>();
            Items = new ObservableCollection<DtcDcDuToanChiDetailImportModel>();

            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);
            var lstLNS = SelectedDanhMucLoaiChi.HiddenValue.Split(',');
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => lstLNS.Contains(x.SLNS)).OrderBy(x => x.SXauNoiMa).ToList();
            _importedMlns = new ObservableCollection<BhDmMucLucNganSachModel>();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));
            _tabIndex = ImportTabIndex.Data;
            LstFile = new ObservableCollection<FileFtpModel>();
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsSelectedFile));
        }

        private void ReloadMLNS()
        {
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            var lstLNS = SelectedDanhMucLoaiChi.HiddenValue.Split(',');
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => lstLNS.Contains(x.SLNS)).OrderBy(x => x.SXauNoiMa).ToList();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));
        }
        #endregion

        #region On save data
        private void OnSaveData()
        {
            try
            {
                var listDanhMucLoaiChi = _bhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();
                var namLamViec = _sessionInfo.YearOfWork;
                var lstLNS = SelectedDanhMucLoaiChi.HiddenValue.Split(",");
                var lstChungTu6ThangQuyetToan = _bhDtcDcdToanChiChiTietService.FindData6ThangDauNamChiTiet(namLamViec, SelectedDonVi.ValueItem
                                                                            , SelectedDanhMucLoaiChi.Id, SelectedDanhMucLoaiChi.ValueItem
                                                                            , SelectedDanhMucLoaiChi.HiddenValue, DateTime.Now);
                var lstChungTuPhanBoDuToan = _bhDtcDcdToanChiChiTietService.FindPbDtChiChiTiet(namLamViec, SelectedDonVi.ValueItem
                                                                        , SelectedDanhMucLoaiChi.Id, DateTime.Now, SelectedDanhMucLoaiChi.HiddenValue);
                List<BhDmMucLucNganSach> _nsMucLucs = _bhDmMucLucNganSachService.GetByLnsDieuChinhDuToan(namLamViec, SelectedDanhMucLoaiChi.HiddenValue);

                BhDtcDcdToanChi chungTu = new BhDtcDcdToanChi();
                chungTu.SSoChungTu = SoChungTu;
                chungTu.IIdDonViId = SelectedDonVi.Id;
                chungTu.IID_MaDonVi = SelectedDonVi.ValueItem;
                chungTu.IID_LoaiCap = SelectedDanhMucLoaiChi.Id;
                chungTu.DNgayChungTu = DngayChungTu;
                chungTu.BDaTongHop = false;
                chungTu.SLNS = SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002 ? LNSValue.LNS_9010001_9010002 : SelectedDanhMucLoaiChi.HiddenValue;
                chungTu.SMoTa = string.Empty;
                chungTu.INamLamViec = namLamViec;
                chungTu.DNgayTao = DateTime.Now;
                chungTu.SNguoiTao = _sessionInfo.Principal;
                chungTu.SMaLoaiChi = SelectedDanhMucLoaiChi.ValueItem;
                if (SelectedDonVi.HiddenValue == LoaiDonVi.ROOT)
                {
                    chungTu.ILoaiTongHop = DtDcDtBhxhLoaiChungTu.BhxhChungTuTongHop;
                    chungTu.IID_TongHopID = Guid.NewGuid();
                }
                else
                {
                    chungTu.ILoaiTongHop = DtDcDtBhxhLoaiChungTu.BhxhChungTu;
                }

                _bhDtcDcdToanChiService.Add(chungTu);

                List<BhDtcDcdToanChiChiTiet> chungTuChiTiets = new List<BhDtcDcdToanChiChiTiet>();

                List<DtcDcDuToanChiDetailImportModel> listDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsDataNotNull
               && _nsMucLucs.Any(y => !y.BHangCha && y.SMoTa == x.STenMLNS && y.SXauNoiMa == x.SXauNoiMa)).ToList();

                var listDetailImportDuToan = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsDataNotNull && !string.IsNullOrWhiteSpace(x.SDuToanChiTietToi)
               && _nsMucLucs.Any(y => y.SMoTa == x.STenMLNS && y.SXauNoiMa == x.SXauNoiMa)).ToList();
                foreach (var item in listDetailImport)
                {
                    BhDmMucLucNganSach mucLuc = _nsMucLucs.FirstOrDefault(x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE
                    && item.STenMLNS == x.SMoTa && item.SXauNoiMa == x.SXauNoiMa);
                    var chungTu6Thang = lstChungTu6ThangQuyetToan.FirstOrDefault(x => x.SXauNoiMa == item.SXauNoiMa);
                    var pbDuToan = lstChungTuPhanBoDuToan.FirstOrDefault(x => x.SXauNoiMa == item.SXauNoiMa);
                    if (mucLuc == null)
                    {
                        continue;
                    }

                    BhDtcDcdToanChiChiTiet bhDtcDcdToanChiChiTiet = new BhDtcDcdToanChiChiTiet();
                    bhDtcDcdToanChiChiTiet.Id = Guid.NewGuid();
                    bhDtcDcdToanChiChiTiet.IID_BH_DTC = chungTu.Id;
                    bhDtcDcdToanChiChiTiet.IID_MucLucNganSach = mucLuc.IIDMLNS;
                    bhDtcDcdToanChiChiTiet.SNoiDung = mucLuc.SMoTa;
                    bhDtcDcdToanChiChiTiet.DNgayTao = DateTime.Now;
                    bhDtcDcdToanChiChiTiet.SNguoiTao = _sessionInfo.Principal;
                    bhDtcDcdToanChiChiTiet.SM = mucLuc.SM;
                    bhDtcDcdToanChiChiTiet.STM = mucLuc.STM;
                    bhDtcDcdToanChiChiTiet.SGhiChu = item.SGhiChu;
                    bhDtcDcdToanChiChiTiet.BHangCha = mucLuc.BHangCha;
                    bhDtcDcdToanChiChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    bhDtcDcdToanChiChiTiet.FTienThucHien06ThangDauNam = item.FTienThucHien06ThangDauNam ?? 0;
                    bhDtcDcdToanChiChiTiet.FTienUocThucHien06ThangCuoiNam = item.FTienUocThucHien06ThangCuoiNam ?? 0;
                    bhDtcDcdToanChiChiTiet.FTienUocThucHienCaNam = item.FTienUocThucHienCaNam;
                    bhDtcDcdToanChiChiTiet.INamLamViec = _sessionInfo.YearOfWork;
                    bhDtcDcdToanChiChiTiet.IIdMaDonVi = SelectedDonVi.ValueItem;
                    chungTuChiTiets.Add(bhDtcDcdToanChiChiTiet);
                }

                //foreach (var item in listDetailImportDuToan)
                //{
                //    BhDmMucLucNganSach mucLuc = _nsMucLucs.FirstOrDefault(x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE
                //    && item.STenMLNS == x.SMoTa && item.SXauNoiMa == x.SXauNoiMa);
                //    if (mucLuc == null)
                //    {
                //        continue;
                //    }

                //    BhDtcDcdToanChiChiTiet bhDtcDcdToanChiChiTiet = new BhDtcDcdToanChiChiTiet();
                //    bhDtcDcdToanChiChiTiet.Id = Guid.NewGuid();
                //    bhDtcDcdToanChiChiTiet.IID_BH_DTC = chungTu.Id;
                //    bhDtcDcdToanChiChiTiet.IID_MucLucNganSach = mucLuc.IIDMLNS;
                //    bhDtcDcdToanChiChiTiet.SNoiDung = mucLuc.SMoTa;
                //    bhDtcDcdToanChiChiTiet.DNgayTao = DateTime.Now;
                //    bhDtcDcdToanChiChiTiet.SNguoiTao = _sessionInfo.Principal;
                //    bhDtcDcdToanChiChiTiet.SM = mucLuc.SM;
                //    bhDtcDcdToanChiChiTiet.STM = mucLuc.STM;
                //    bhDtcDcdToanChiChiTiet.SGhiChu = item.SGhiChu;
                //    bhDtcDcdToanChiChiTiet.BHangCha = mucLuc.BHangCha;
                //    bhDtcDcdToanChiChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                //    bhDtcDcdToanChiChiTiet.FTienSoSanhTang = item.FTienSoSanhTang;
                //    bhDtcDcdToanChiChiTiet.FTienSoSanhGiam = item.FTienSoSanhGiam;
                //    bhDtcDcdToanChiChiTiet.INamLamViec = _sessionInfo.YearOfWork;
                //    bhDtcDcdToanChiChiTiet.IIdMaDonVi = SelectedDonVi.ValueItem;
                //    chungTuChiTiets.Add(bhDtcDcdToanChiChiTiet);
                //}
                _bhDtcDcdToanChiChiTietService.AddRange(chungTuChiTiets);

                BhDtcDcdToanChiChiTietCriteria searchCondition = new BhDtcDcdToanChiChiTietCriteria();
                searchCondition.NamLamViec = _sessionService.Current.YearOfWork;
                searchCondition.IdDonVi = chungTu.IID_MaDonVi;
                searchCondition.ILoaiDanhMucChi = chungTu.IID_LoaiCap;
                searchCondition.DtcDcdToanChiId = chungTu.Id;
                searchCondition.LNS = chungTu.SLNS;
                searchCondition.NgayChungTu = chungTu.DNgayChungTu;
                searchCondition.ILoaiTongHop = chungTu.ILoaiTongHop;
                searchCondition.MaLoaiChi = chungTu.SMaLoaiChi;
                var listDieuChinhDuToanChiTiet =
                    _bhDtcDcdToanChiChiTietService.FindByConditionForChildUnit(searchCondition).ToList();

                if (chungTuChiTiets.Count > 0)
                {
                    chungTu.FTienDuToanDuocGiao = listDieuChinhDuToanChiTiet.Sum(item => item.FTienDuToanDuocGiao);
                    chungTu.FTienThucHien06ThangDauNam = listDieuChinhDuToanChiTiet.Sum(item => item.FTienThucHien06ThangDauNam);
                    chungTu.FTienUocThucHien06ThangCuoiNam = listDieuChinhDuToanChiTiet.Sum(item => item.FTienUocThucHien06ThangCuoiNam);
                    chungTu.FTienUocThucHienCaNam = listDieuChinhDuToanChiTiet.Sum(item => item.FTienUocThucHienCaNam);
                    chungTu.FTienSoSanhTang = (chungTu.FTienUocThucHienCaNam - chungTu.FTienDuToanDuocGiao) > 0 ? chungTu.FTienUocThucHienCaNam - chungTu.FTienDuToanDuocGiao : 0;
                    chungTu.FTienSoSanhGiam = (chungTu.FTienDuToanDuocGiao - chungTu.FTienUocThucHienCaNam) > 0 ? chungTu.FTienDuToanDuocGiao - chungTu.FTienUocThucHienCaNam : 0;
                    _bhDtcDcdToanChiService.Update(chungTu);
                }

                BhDtcDcdToanChiModel bhDtcDcdToanChi = _mapper.Map<BhDtcDcdToanChiModel>(chungTu);
                DialogHost.CloseDialogCommand.Execute(bhDtcDcdToanChi, null);
                SavedAction?.Invoke(bhDtcDcdToanChi);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
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

                FilePath = openFileDialog.FileName;
                _importErrors = new List<ImportErrorItem>();
                IsUploadFile = true;
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
