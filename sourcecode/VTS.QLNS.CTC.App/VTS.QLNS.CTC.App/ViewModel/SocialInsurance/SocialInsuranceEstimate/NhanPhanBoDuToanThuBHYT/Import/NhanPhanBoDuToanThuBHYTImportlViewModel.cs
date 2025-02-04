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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.NhanPhanBoDuToanThuBHYT.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanPhanBoDuToanThuBHYT.Import
{
    public class NhanPhanBoDuToanThuBHYTImportlViewModel : ViewModelBase
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IBhDtTmBHYTTNService _bhDtTmBHYTTNService;
        private readonly IBhDtTmBHYTTNChiTietService _bhDtTmBHYTTNChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        #endregion

        #region Property
        private SessionInfo _sessionInfo;
        private ImpHistory _impHistory;
        private List<ImportErrorItem> _lstErrDtTmBHYT = new List<ImportErrorItem>();
        private List<ImportErrorItem> _listErrChungTu = new List<ImportErrorItem>();
        public override string Name => "Import nhận phân bổ dự toán thu BHYT " + _sessionInfo.YearOfWork;
        public override Type ContentType => typeof(ImportNhanPhanBoDuToanThuBHYT);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private Dictionary<Guid, BhDtTmBHYTTNChiTiet> _dicBhytChiTiet;
        private List<BhDmMucLucNganSachModel> _mergeItems;
        private List<BhDmMucLucNganSach> _nsBhYtMucLucs;
        private List<BhDmMucLucNganSach> _lsAllBhYtMucLucs;
        private DtTmBhytTnDetailImportModel _selectedItem;
        public DtTmBhytTnDetailImportModel SelectedItem
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
                _isSaveData = (Items.Any() && !_lstErrDtTmBHYT.Any());
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
        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ObservableCollection<DtTmBhytTnDetailImportModel> _items;
        public ObservableCollection<DtTmBhytTnDetailImportModel> Items
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

        private ComboboxItem _cbxEstimateTypeSelected;
        public ComboboxItem CbxEstimateTypeSelected
        {
            get => _cbxEstimateTypeSelected;
            set
            {
                SetProperty(ref _cbxEstimateTypeSelected, value);
            }
        }
        private ObservableCollection<ComboboxItem> _cbxEstimateType;
        public ObservableCollection<ComboboxItem> CbxEstimateType
        {
            get => _cbxEstimateType;
            set => SetProperty(ref _cbxEstimateType, value);
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
        public DateTime NgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public string SSoChungTu { get; set; }
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
        public RelayCommand HandleDataCommand { get; }
        #endregion

        #region Constructor
        public NhanPhanBoDuToanThuBHYTImportlViewModel(
            ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            ILog logger,
            IImportExcelService importService,
            IBhDtTmBHYTTNService bhDtTmBHYTTNService,
            IBhDtTmBHYTTNChiTietService bhDtTmBHYTTNChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _logger = logger;
            _bhDtTmBHYTTNService = bhDtTmBHYTTNService;
            _bhDtTmBHYTTNChiTietService = bhDtTmBHYTTNChiTietService;
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
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
            LoadEstimateType();
            LoadDonVi();
            OnResetData();
            NgayChungTu = DateTime.Now;
            DNgayQuyetDinh = DateTime.Now;
        }
        #endregion

        #region Load data
        private void LoadDonVi()
        {
            ItemsDonVi = new ObservableCollection<ComboboxItem>();
            IEnumerable<DonVi> listDonVi = _donViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDonVi != null)
            {
                ItemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.TenDonVi,
                    ValueItem = n.IIDMaDonVi,
                    HiddenValue = n.Id.ToString()
                }));
            }
            OnPropertyChanged(nameof(ItemsDonVi));
        }
        private void LoadEstimateType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = EstimateType.EstimateTypeName[EstimateTypeNum.YEAR], ValueItem = ((int)EstimateTypeNum.YEAR).ToString()},
                new ComboboxItem {DisplayItem = EstimateType.EstimateTypeName[EstimateTypeNum.ADDITIONAL], ValueItem = ((int)EstimateTypeNum.ADDITIONAL).ToString()}
            };

            CbxEstimateType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            _cbxEstimateTypeSelected = CbxEstimateType.First();
        }

        private List<ImportErrorItem> ValidateItem(DtTmBhytTnDetailImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                _lsAllBhYtMucLucs = _bhDmMucLucNganSachService.GetListBhytMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.THU_MUA_BHYT).ToList();
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

        private void LoadData()
        {
            var soChungTuIndex = _bhDtTmBHYTTNService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
            SSoChungTu = "DTT-" + soChungTuIndex.ToString("D3");
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
            var BhYtChiTiet = _bhDtTmBHYTTNChiTietService.FindAllChungTuDuToan();

            if (BhYtChiTiet != null)
            {
                foreach (var item in BhYtChiTiet)
                {
                    if (!_lstIdBhxhChiTiet.Contains(item.Id))
                    {
                        _lstIdBhxhChiTiet.Add(item.Id);
                    }
                }
                _lstErrDtTmBHYT.Clear();
            }

            _dicBhytChiTiet = new Dictionary<Guid, BhDtTmBHYTTNChiTiet>();

            try
            {
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;

                var dataImport = _importService.ProcessData<DtTmBhytTnDetailImportModel>(FilePath);
                var bhytImportModels = new ObservableCollection<DtTmBhytTnDetailImportModel>(dataImport.Data);
                
                if (!Items.Any())
                    Items = bhytImportModels;

                List<string> lstError = new List<string>();

                if (dataImport.ImportErrors.Count > 0)
                {
                    _lstErrDtTmBHYT.AddRange(dataImport.ImportErrors);
                }

                if (!Items.Any())
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (string.IsNullOrEmpty(FilePath))
                {
                    lstError.Add(Resources.ErrorFileEmpty);
                }

                int i = 0;
                foreach (var item in Items)
                {
                    ++i;
                    var listError = ValidateItem(item, i);

                    if (listError.Any())
                    {
                        _lstErrDtTmBHYT.AddRange(listError);
                        item.ImportStatus = false;

                        if (listError.Any(x => x.IsErrorMLNS))
                        {
                            item.IsError = true;
                        }
                    }
                    else
                    {
                        if (!_lstErrDtTmBHYT.Any(x => x.Row == i - 1))
                            item.ImportStatus = true;
                    }

                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(DtTmBhytTnDetailImportModel.SXauNoiMa))
                        {
                            DtTmBhytTnDetailImportModel item = (DtTmBhytTnDetailImportModel)sender;
                            HandleData();
                        }
                    };
                }

                if (lstError.Any() || Items.Any(x => !x.ImportStatus))
                {
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
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
        #endregion

        #region Close
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

        #region Download template
        private void OnDownloadTemplate()
        {
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_DTTM_BHYT_IMPORT_TEMPLATE, ExportFileName.IMPORT_BH_DTTM_BHYT_CHUNGTU_CHITIET);
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

        #region Check error
        private void ShowError()
        {
            int rowIndex = Items.IndexOf(SelectedItem);
            List<string> errors = _lstErrDtTmBHYT.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnResetData()
        {
            _mergeItems = new List<BhDmMucLucNganSachModel>();
            _filePath = string.Empty;
            _items = new ObservableCollection<DtTmBhytTnDetailImportModel>();
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);

            _nsBhYtMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => x.SLNS.StartsWith(BhxhMLNS.THU_MUA_BHYT)).OrderBy(x => x.SXauNoiMa).ToList();
            _importedMlns = new ObservableCollection<BhDmMucLucNganSachModel>();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_nsBhYtMucLucs));
            _existedMlns.RemoveAt(0);
            _impHistory = new ImpHistory();
            _lstErrDtTmBHYT = new List<ImportErrorItem>();
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
        #endregion

        #region On save data
        private void OnSaveData()
        {
            try
            {
                var namLamViec = _sessionInfo.YearOfWork;
                List<string> lstLNS = new List<string>();
                string sLNSImport = string.Join(",", Items.Select(x => x.SXauNoiMa.Length >= 7 ? x.SXauNoiMa.Substring(0, 7) : x.SXauNoiMa).ToList().Distinct());

                if (string.IsNullOrEmpty(SSoQuyetDinh))
                {
                    System.Windows.Forms.MessageBox.Show(Resources.AlertSoQuyetDinhEmpty, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _lsAllBhYtMucLucs = _bhDmMucLucNganSachService.GetListBhytMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.THU_MUA_BHYT).ToList();
                BhDtTmBHYTTN chungTu = new BhDtTmBHYTTN();
                chungTu.Id = Guid.NewGuid();
                chungTu.SSoChungTu = SSoChungTu;
                chungTu.IIDMaDonVi = _sessionInfo.IdDonVi;
                chungTu.DNgayChungTu = NgayChungTu == null ? DateTime.Now : NgayChungTu;
                chungTu.SDSLNS = sLNSImport;
                chungTu.SMoTa = MoTa;
                chungTu.BIsKhoa = false;
                chungTu.INamLamViec = namLamViec;
                chungTu.DNgayTao = DateTime.Now;
                chungTu.SNguoiTao = _sessionInfo.Principal;
                chungTu.SSoQuyetDinh = SSoQuyetDinh;
                chungTu.DNgayQuyetDinh = DNgayQuyetDinh;
                chungTu.ILoaiDuToan = int.Parse(_cbxEstimateTypeSelected.ValueItem);

                _bhDtTmBHYTTNService.Add(chungTu);

                List<BhDtTmBHYTTNChiTiet> lstDtTmBHYTTNChiTiets = new List<BhDtTmBHYTTNChiTiet>();

                List<DtTmBhytTnDetailImportModel> listDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasData
                   && _lsAllBhYtMucLucs.Any(y => !y.BHangCha && y.SXauNoiMa == x.SXauNoiMa)).ToList();

                foreach (var item in listDetailImport)
                {
                    BhDmMucLucNganSach mucLuc = _lsAllBhYtMucLucs.FirstOrDefault(x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE
                        && item.SNoiDung == x.SMoTa && item.SXauNoiMa == x.SXauNoiMa);

                    if (mucLuc == null)
                    {
                        continue;
                    }

                    BhDtTmBHYTTNChiTiet bhDtTmBHYTTNChiTiet = new BhDtTmBHYTTNChiTiet();
                    bhDtTmBHYTTNChiTiet.Id = Guid.NewGuid();
                    bhDtTmBHYTTNChiTiet.IID_DTTM_BHYT_ThanNhan = chungTu.Id;
                    bhDtTmBHYTTNChiTiet.IID_MLNS = mucLuc.IIDMLNS;
                    bhDtTmBHYTTNChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    bhDtTmBHYTTNChiTiet.IIdMaDonVi = _sessionInfo.IdDonVi;
                    bhDtTmBHYTTNChiTiet.SNguoiTao = _sessionInfo.Principal;
                    bhDtTmBHYTTNChiTiet.DNgayTao = DateTime.Now;
                    bhDtTmBHYTTNChiTiet.INamLamViec = namLamViec;
                    bhDtTmBHYTTNChiTiet.SGhiChu = item.SGhiChu;
                    bhDtTmBHYTTNChiTiet.SNoiDung = mucLuc.SMoTa;
                    bhDtTmBHYTTNChiTiet.SLNS = mucLuc.SLNS;
                    bhDtTmBHYTTNChiTiet.FDuToan = item.FDuToan;
                    bhDtTmBHYTTNChiTiet.SXauNoiMa = item.SXauNoiMa;
                    bhDtTmBHYTTNChiTiet.IIdMaDonVi = _sessionInfo.IdDonVi;
                    lstDtTmBHYTTNChiTiets.Add(bhDtTmBHYTTNChiTiet);
                }

                _bhDtTmBHYTTNChiTietService.AddRange(lstDtTmBHYTTNChiTiets);

                if (lstDtTmBHYTTNChiTiets.Count > 0)
                {
                    chungTu.FDuToan = lstDtTmBHYTTNChiTiets.Sum(item => item.FDuToan);
                    _bhDtTmBHYTTNService.Update(chungTu);
                }

                BhDtTmBHYTTNModel bhDtTmBHYTTNModel = _mapper.Map<BhDtTmBHYTTNModel>(chungTu);
                DialogHost.CloseDialogCommand.Execute(bhDtTmBHYTTNModel, null);
                SavedAction?.Invoke(bhDtTmBHYTTNModel);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
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
                _lstErrDtTmBHYT.Clear();

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
