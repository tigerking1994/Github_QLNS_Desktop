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
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat.ImportChungTuCapPhat;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat.ImportChungTuCapPhat
{
    public class ImportChungTuCapPhatViewModel : ViewModelBase
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IBhCpChungTuService _bhCpChungTuService;
        private readonly IBhCpChungTuChiTietService _bhCpChungTuChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private ImpHistory _impHistory;
        #endregion

        #region Property
        private bool _isUpdateLoad;
        IEnumerable<DonVi> listDonVi;
        IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi;
        private List<ImportErrorItem> _listErrChungTu = new List<ImportErrorItem>();
        public override string Name => "Import dữ liệu cấp phát chứng từ " + _sessionInfo.YearOfWork;
        public override Type ContentType => typeof(ImportChungTuCapPhatBH);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private Dictionary<Guid, BhCpChungTuChiTiet> _dicBhytChiTiet;
        private List<BhDmMucLucNganSachModel> _mergeItems;
        private List<BhDmMucLucNganSach> _nsBhYtMucLucs;
        private List<BhDmMucLucNganSach> _lsAllBhYtMucLucs;
        private BhCpChungTuDetailImportModel _selectedItem;
        private IEnumerable<BhDmMucLucNganSach> _nsMucLucs;
        public BhCpChungTuDetailImportModel SelectedItem
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
        private ObservableCollection<ComboboxItem> _cbxQuarter;
        public ObservableCollection<ComboboxItem> CbxQuarter
        {
            get => _cbxQuarter;
            set => SetProperty(ref _cbxQuarter, value);
        }
        private ComboboxItem _cbxQuarterSelected;
        public ComboboxItem CbxQuarterSelected
        {
            get => _cbxQuarterSelected;
            set
            {
                SetProperty(ref _cbxQuarterSelected, value);
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
        private ObservableCollection<BhCpChungTuDetailImportModel> _items;
        public ObservableCollection<BhCpChungTuDetailImportModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
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

        private ComboboxItem _selectedDanhMucLoaiChi;
        public ComboboxItem SelectedDanhMucLoaiChi
        {
            get => _selectedDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selectedDanhMucLoaiChi, value);
                if (_selectedDanhMucLoaiChi != null && (_selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002
                   || _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9010008
                   || _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9010009
                   || _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9010010))
                {
                    _isFTyLeThu = false;
                    FTyLeThu = BHXHConstants.CKP_TYLETHU_NOTDEFAULT;
                    OnPropertyChanged(nameof(IsFTyLeThu));
                    OnPropertyChanged(nameof(FTyLeThu));
                }
                else
                {
                    _isFTyLeThu = true;
                    FTyLeThu = BHXHConstants.CKP_TYLETHU_DEFAULT;
                    OnPropertyChanged(nameof(IsFTyLeThu));
                    OnPropertyChanged(nameof(FTyLeThu));
                }

                if (value != null)
                {
                    ReloadMLNS();
                    OnPropertyChanged(nameof(ExistedMlns));
                }
            }
        }

        private double? _fTyLeThu;
        public double? FTyLeThu
        {
            get => _fTyLeThu;
            set
            {
                SetProperty(ref _fTyLeThu, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDanhMucLoaiChi;
        public ObservableCollection<ComboboxItem> ItemsDanhMucLoaiChi
        {
            get => _itemsDanhMucLoaiChi;
            set => SetProperty(ref _itemsDanhMucLoaiChi, value);
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

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }
        private DateTime? _dNgayChungTu;
        public DateTime? DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }

        private bool _isFTyLeThu;
        public bool IsFTyLeThu
        {
            get => _isFTyLeThu;
            set => SetProperty(ref _isFTyLeThu, value);
        }
        private string sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => sSoQuyetDinh;
            set => SetProperty(ref sSoQuyetDinh, value);
        }

        private string _sSoChungTu;
        public string SSoChungTu
        {
            get => _sSoChungTu;
            set => SetProperty(ref _sSoChungTu, value);
        }
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
        public RelayCommand HandleDataCommand { get; }
        #endregion

        #region Constructor
        public ImportChungTuCapPhatViewModel(
               ISessionService sessionService,
               INsDonViService donViService,
               IMapper mapper,
               ILog logger,
               IImportExcelService importService,
               IBhCpChungTuService bhCpChungTuService,
               IBhCpChungTuChiTietService bhCpChungTuChiTietService,
               IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
               IBhDmMucLucNganSachService bhDmMucLucNganSachService
            )
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _logger = logger;
            _importService = importService;
            _bhCpChungTuChiTietService = bhCpChungTuChiTietService;
            _bhCpChungTuService = bhCpChungTuService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
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
                LoadDanhMucLoaiChi();
                LoadDonVi();
                LoadQuarters();
                FTyLeThu = BHXHConstants.CKP_TYLETHU_DEFAULT;
                SSoChungTu = GetSoChungTu();
                DNgayChungTu = DateTime.Now;
                DNgayQuyetDinh = DateTime.Now;
                OnResetData();
                OnPropertyChanged(nameof(FTyLeThu));
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
                int namLamViec = _sessionInfo.YearOfWork;
                var soChungTuIndex = _bhCpChungTuService.GetSoChungTuIndexByCondition(namLamViec);
                var soChungTu = "CP-" + soChungTuIndex.ToString("D3");
                return soChungTu;
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
                return string.Empty;
            }
        }
        #endregion

        #region Load file
        private void LoadQuarters()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q1], ValueItem = ((int)QuarterEnum.Q1).ToString()},
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q2], ValueItem = ((int)QuarterEnum.Q2).ToString()},
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q3], ValueItem = ((int)QuarterEnum.Q3).ToString()},
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q4], ValueItem = ((int)QuarterEnum.Q4).ToString()}
            };

            CbxQuarter = new ObservableCollection<ComboboxItem>(cbxVoucher);
            CbxQuarterSelected = CbxQuarter.First();
        }
        private void LoadDonVi()
        {
            ItemsDonVi = new ObservableCollection<ComboboxItem>();
            listDonVi = _donViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDonVi != null)
            {
                listDonVi = listDonVi.Where(x => x.Loai != LoaiDonVi.ROOT);
                ItemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.TenDonVi,
                    ValueItem = n.IIDMaDonVi,
                    HiddenValue = n.Id.ToString()
                }));

                SelectedDonVi = ItemsDonVi.ElementAt(0);
            }
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadDanhMucLoaiChi()
        {
            ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
            listDanhMucLoaiChi = null;
            listDanhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDanhMucLoaiChi != null)
            {
                ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenDanhMucLoaiChi,
                    ValueItem = n.Id.ToString(),
                    HiddenValue = n.SLNS.ToString(),
                    Id = n.Id
                }));
                SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.ElementAt(0);
            }

            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
        }

        private void HandleData()
        {
            var fileExtension = Path.GetExtension(FilePath).ToLower();
            if (!(fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx")))
            {
                System.Windows.MessageBox.Show(Resources.FileImportWrongExtensionExcel, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            List<Guid> _lstIdBhxhChiTiet = new List<Guid>();
            var BhYtChiTiet = _bhCpChungTuChiTietService.FindAllChungTuDuToan();

            if (BhYtChiTiet != null)
            {
                foreach (var item in BhYtChiTiet)
                {
                    if (!_lstIdBhxhChiTiet.Contains(item.Id))
                    {
                        _lstIdBhxhChiTiet.Add(item.Id);
                    }
                }
            }


            if (CbxQuarterSelected == null)
            {
                MessageBoxHelper.Error(Resources.AlertQuartyEmpty);
                return;
            }

            if (SelectedDonVi == null)
            {
                MessageBoxHelper.Error(Resources.MsgDonViEmpty);
                //OnResetData();
                return;
            }

            if (string.IsNullOrEmpty(SSoQuyetDinh))
            {
                System.Windows.Forms.MessageBox.Show(Resources.AlertSoKeHoachEmpty, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            _dicBhytChiTiet = new Dictionary<Guid, BhCpChungTuChiTiet>();

            try
            {
                if (_listErrChungTu != null && _listErrChungTu.Any())
                {
                    _listErrChungTu = new List<ImportErrorItem>();
                    foreach (var error in Items)
                    {
                        var rowIndex = Items.IndexOf(error);
                        var listError = _importService.ValidateItem(error, rowIndex);
                        if (listError.Count > 0)
                        {
                            _listErrChungTu.AddRange(listError);
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
                            _listErrChungTu = _listErrChungTu.Where(x => x.Row != rowIndex).ToList();
                        }
                        OnPropertyChanged(nameof(IsSaveData));
                    }
                    if (_listErrChungTu != null && _listErrChungTu.Any())
                    {
                        var messageOfRow = _listErrChungTu.Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).Distinct();
                        System.Windows.MessageBox.Show(string.Join(Environment.NewLine, messageOfRow), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    if (_listErrChungTu == null && !_listErrChungTu.Any() && !_isUpdateLoad)
                    {
                        OnPropertyChanged(nameof(IsSaveData));
                        return;
                    }

                    XlsFile xls = new XlsFile(false);
                    xls.Open(FilePath);
                    xls.ActiveSheet = 1;
                    _listErrChungTu = new List<ImportErrorItem>();
                    string sLNSImport = string.Empty;
                    var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                    predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                    _nsMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).OrderBy(x => x.SXauNoiMa).ToList();
                    var sLNS = listDanhMucLoaiChi.Where(x => x.Id == SelectedDanhMucLoaiChi.Id).Select(x => x.SLNS).FirstOrDefault();
                    if (sLNS == LNSValue.LNS_9010001_9010002 || sLNS == LNSValue.LNS_901_9010001_9010002)
                    {
                        sLNS = LNSValue.LNS_901_9010001_9010002;
                    }
                    var dataSLNS = sLNS.Split(',');
                    _nsMucLucs = _nsMucLucs.Where(x => dataSLNS.Contains(x.SLNS)).ToList();
                    var dataImport = _importService.ProcessData<BhCpChungTuDetailImportModel>(FilePath);
                    var lstChungTuImport = new ObservableCollection<BhCpChungTuDetailImportModel>(dataImport.Data);

                    List<string> lstError = new List<string>();

                    if (dataImport.ImportErrors.Count > 0)
                    {
                        _listErrChungTu.AddRange(dataImport.ImportErrors);
                    }

                    if (lstChungTuImport == null || lstChungTuImport.Count <= 0)
                    {
                        System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    if (string.IsNullOrEmpty(FilePath))
                    {
                        lstError.Add(Resources.ErrorFileEmpty);
                    }


                    List<BhCpChungTuDetailImportModel> listDetailImport = lstChungTuImport.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasData
                                                                            && _nsMucLucs.Any(y => y.SXauNoiMa == x.SXauNoiMa)).ToList();

                    if (listDetailImport.Count < 0)
                    {
                        System.Windows.MessageBox.Show(Resources.MsgCheckLuaChonLaiLoaiKP, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        OnResetData();
                        return;
                    }

                    Items = lstChungTuImport;

                    foreach (var item in Items)
                    {
                        item.PropertyChanged += (sender, args) =>
                        {
                            if (nameof(BhCpChungTuDetailImportModel.SXauNoiMa) == nameof(BhCpChungTuDetailImportModel.SXauNoiMa))
                            {
                                var entityDetail = (BhCpChungTuDetailImportModel)sender;
                                var rowIndex = _items.IndexOf(entityDetail);
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
                //FilePath = "";
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

        #region Dowload template
        private void OnDownloadTemplate()
        {
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_CP_IMPORT_TEMPLATE, ExportFileName.IMPORT_BH_CP_CHUNGTU_CHITIET);
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

        #region Check error and Reset data
        private void ShowError()
        {
            int rowIndex = Items.IndexOf(SelectedItem);
            List<string> errors = _listErrChungTu.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnResetData()
        {
            _mergeItems = new List<BhDmMucLucNganSachModel>();
            _filePath = string.Empty;

            SSoQuyetDinh = string.Empty;
            _items = new ObservableCollection<BhCpChungTuDetailImportModel>();
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);
            var lstLNS = SelectedDanhMucLoaiChi.HiddenValue.Split(',');
            _nsBhYtMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => lstLNS.Contains(x.SLNS)).ToList();
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
            var lstLNS = SelectedDanhMucLoaiChi.HiddenValue.Split(',');
            _nsBhYtMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => lstLNS.Contains(x.SLNS)).ToList();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_nsBhYtMucLucs.OrderBy(x => x.SXauNoiMa)));
        }
        #endregion

        #region On save data
        private void OnSaveData()
        {
            try
            {
                var namLamViec = _sessionInfo.YearOfWork;

                Guid? idLoaiDanhMucChi = Guid.Empty;
                string sLNS = SelectedDanhMucLoaiChi.HiddenValue;
                if (sLNS == LNSValue.LNS_9010001_9010002)
                {
                    sLNS = LNSValue.LNS_901_9010001_9010002;
                }
                var dataSLNS = sLNS.Split(',');

                if (Items.Any(x => !dataSLNS.Contains(x.SXauNoiMa)))
                {
                    System.Windows.MessageBox.Show(Resources.ErrorImport, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                List<BhDmMucLucNganSach> _nsBhYtMucLucs = _bhDmMucLucNganSachService.GetByLnsDieuChinhDuToan(namLamViec, sLNS);

                BhCpChungTu bhCpChungTu = new BhCpChungTu();
                bhCpChungTu.SSoChungTu = SSoChungTu;
                bhCpChungTu.DNgayQuyetDinh = DNgayQuyetDinh;
                bhCpChungTu.SSoQuyetDinh = SSoQuyetDinh;
                bhCpChungTu.BIsKhoa = false;
                bhCpChungTu.DNgayTao = DateTime.Now;
                bhCpChungTu.IQuy = int.Parse(CbxQuarterSelected.ValueItem);
                bhCpChungTu.ILoaiTongHop = AllocationTypeLoaiChungTu.ChungTu;
                bhCpChungTu.INamChungTu = namLamViec;
                bhCpChungTu.SNguoiTao = _sessionInfo.Principal;
                bhCpChungTu.SID_MaDonVi = _selectedDonVi.ValueItem;
                bhCpChungTu.DNgayChungTu = DNgayChungTu == null ? DateTime.Now : DNgayChungTu;
                bhCpChungTu.SLNS = SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002 ? LNSValue.LNS_9010001_9010002 : SelectedDanhMucLoaiChi.HiddenValue;
                bhCpChungTu.IID_LoaiCap = SelectedDanhMucLoaiChi.Id;
                bhCpChungTu.FTyLeThu = FTyLeThu;

                _bhCpChungTuService.Add(bhCpChungTu);

                List<BhCpChungTuChiTiet> lstBhCpChungTuChiTiets = new List<BhCpChungTuChiTiet>();
                List<BhCpChungTuDetailImportModel> listDetailImport = new List<BhCpChungTuDetailImportModel>();
                if (SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002)
                {
                    listDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasData
                                                                    && _nsBhYtMucLucs.Any(y => !y.BHangCha && y.SXauNoiMa == x.SXauNoiMa)).ToList();

                }
                else
                {
                    listDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasData
                                                                    && _nsBhYtMucLucs.Any(y => y.SXauNoiMa == x.SXauNoiMa)).ToList();
                }

                foreach (var item in listDetailImport)
                {
                    BhDmMucLucNganSach mucLuc = _nsBhYtMucLucs.FirstOrDefault(x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE
                    && item.SNoiDung == x.SMoTa && item.SXauNoiMa == x.SXauNoiMa);

                    if (mucLuc == null)
                    {
                        continue;
                    }

                    BhCpChungTuChiTiet chungTuChiTiet = new BhCpChungTuChiTiet();
                    chungTuChiTiet.IID_CP_ChungTu = bhCpChungTu.Id;
                    chungTuChiTiet.IID_MucLucNganSach = mucLuc.IIDMLNS;
                    chungTuChiTiet.SM = mucLuc.SM;
                    chungTuChiTiet.STM = mucLuc.STM;
                    chungTuChiTiet.SNoiDung = mucLuc.SMoTa;
                    chungTuChiTiet.IID_MaDonVi = _selectedDonVi.ValueItem;
                    chungTuChiTiet.FTienDaCap = item.FDaCap;
                    chungTuChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    chungTuChiTiet.INamLamViec = _sessionInfo.YearOfWork;
                    //chungTuChiTiet.FTienDuToan = item.FDuToan;
                    chungTuChiTiet.FTienKeHoachCap = item.FCapPhat;
                    chungTuChiTiet.SGhiChu = item.SGhiChu;
                    chungTuChiTiet.DNgayTao = DateTime.Now;
                    chungTuChiTiet.SNguoiTao = _sessionInfo.Principal;
                    chungTuChiTiet.SXauNoiMa = item.SXauNoiMa;

                    lstBhCpChungTuChiTiets.Add(chungTuChiTiet);
                }

                _bhCpChungTuChiTietService.AddRange(lstBhCpChungTuChiTiets);

                if (lstBhCpChungTuChiTiets.Count > 0)
                {
                    //bhCpChungTu.FTienDuToan = lstBhCpChungTuChiTiets.Sum(item => item.FTienDuToan);
                    bhCpChungTu.FTienDaCap = lstBhCpChungTuChiTiets.Sum(item => item.FTienDaCap);
                    bhCpChungTu.FTienKeHoachCap = lstBhCpChungTuChiTiets.Sum(item => item.FTienKeHoachCap);
                    _bhCpChungTuService.Update(bhCpChungTu);
                }

                BhCpChungTuModel bhCpChungTuModel = _mapper.Map<BhCpChungTuModel>(bhCpChungTu);
                DialogHost.CloseDialogCommand.Execute(bhCpChungTuModel, null);
                SavedAction?.Invoke(bhCpChungTuModel);
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
                _listErrChungTu.Clear();
                _isUpdateLoad = true;
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

        private List<ImportErrorItem> ValidateItem(BhCpChungTuDetailImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                var yearOfWork = _sessionService.Current.YearOfWork;
                var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);
                var lstLNS = SelectedDanhMucLoaiChi.HiddenValue.Split(',');
                _lsAllBhYtMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => lstLNS.Contains(x.SLNS)).ToList();
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
