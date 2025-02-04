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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.Import
{
    public class ImportThamDinhQuyetToanViewModel : ViewModelBase
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDmThamDinhQuyetToanService _bhDmThamDinhQuyetToanService;
        private readonly IBhThamDinhQuyetToanService _bhThamDinhQuyetToanService;
        private readonly IBhThamDinhQuyetToanChiTietService _bhThamDinhQuyetToanChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private ImpHistory _impHistory;
        #endregion

        #region Property
        IEnumerable<DonVi> listDonVi;
        private List<ImportErrorItem> _listErrChungTu = new List<ImportErrorItem>();
        public override string Name => "IMPORT DỮ LIỆU BÁO CÁO THẨM ĐỊNH QUYẾT TOÁN NĂM " + _sessionInfo.YearOfWork;
        public override Type ContentType => typeof(ImportThamDinhQuyetToan);
        public override PackIconKind IconKind => PackIconKind.Import;
        private Dictionary<Guid, BhQtcNamKinhPhiKhacChiTiet> _dicChiTiet;
        private List<BhDmMucLucNganSachModel> _mergeItems;
        private List<BhDmMucLucNganSach> _nsBhYtMucLucs;
        private List<BhDmMucLucNganSach> _lsAllBhYtMucLucs;
        private List<BhDmThamDinhQuyetToan> _nsDmTDQTMucLucs;
        public string SSoChungTu { get; set; }
        private BhThamDinhQuyetToanDetailImportModel _selectedItem;
        public BhThamDinhQuyetToanDetailImportModel SelectedItem
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
        private ObservableCollection<BhThamDinhQuyetToanDetailImportModel> _items;
        public ObservableCollection<BhThamDinhQuyetToanDetailImportModel> Items
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
            get => ImportedMlns.Any(i => i.IsSelected) && ExistedDMTDQTMuclucs.Where(i => i.IsSelected).Count() == 1;
        }

        private ObservableCollection<BhDmThamDinhQuyetToanModel> _existedDMTDQTMuclucs;
        public ObservableCollection<BhDmThamDinhQuyetToanModel> ExistedDMTDQTMuclucs
        {
            get => _existedDMTDQTMuclucs;
            set => SetProperty(ref _existedDMTDQTMuclucs, value);
        }

        //private ObservableCollection<BhDmMucLucNganSachModel> _existedMlns
        /*public ObservableCollection<BhDmMucLucNganSachModel> ExistedMlns
        {
            get => _existedMlns;
            set => SetProperty(ref _existedMlns, value);
        }*/
        private BhDmThamDinhQuyetToanModel _selectedParent;
        public BhDmThamDinhQuyetToanModel SelectedParent
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
        private string sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => sSoQuyetDinh;
            set => SetProperty(ref sSoQuyetDinh, value);
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
        public ImportThamDinhQuyetToanViewModel(
               ISessionService sessionService,
               INsDonViService donViService,
               IMapper mapper,
               ILog logger,
               IImportExcelService importService,
               IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
               IBhDmMucLucNganSachService bhDmMucLucNganSachService,
               IBhDmThamDinhQuyetToanService bhDmThamDinhQuyetToanService,
               IBhThamDinhQuyetToanService bhThamDinhQuyetToanService,
               IBhThamDinhQuyetToanChiTietService bhThamDinhQuyetToanChiTietService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _logger = logger;
            _importService = importService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _bhDmThamDinhQuyetToanService = bhDmThamDinhQuyetToanService;
            _bhThamDinhQuyetToanService = bhThamDinhQuyetToanService;
            _bhThamDinhQuyetToanChiTietService = bhThamDinhQuyetToanChiTietService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            DownloadTemplateCommand = new RelayCommand(obj => OnDownloadTemplate());
            HandleDataCommand = new RelayCommand(obj => OnHandleData());
        }
        #endregion

        #region Init
        public override void Init()
        {
            try
            {
                base.Init();
                _sessionInfo = _sessionService.Current;
                LoadData();
                OnResetData();
                LoadDonVi();
                NgayChungTu = DateTime.Now;
                DNgayQuyetDinh = DateTime.Now;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Load data

        private void LoadDonVi()
        {
            ItemsDonVi = new ObservableCollection<ComboboxItem>();
            listDonVi = _donViService.FindByNamLamViec(_sessionService.Current.YearOfWork).Where(x => x.Loai != LoaiDonVi.ROOT);
            if (listDonVi != null)
            {
                ItemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.TenDonVi,
                    ValueItem = n.IIDMaDonVi,
                    HiddenValue = n.Loai,
                    Id = n.Id
                }));
                ;
            }
            OnPropertyChanged(nameof(ItemsDonVi));
        }


        private void OnHandleData()
        {
            try
            {
                var fileExtension = Path.GetExtension(FilePath).ToLower();
                if (!(fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx")))
                {
                    System.Windows.MessageBox.Show(Resources.FileImportWrongExtensionExcel, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var chungTuThamDinh = _bhThamDinhQuyetToanService.FindAll(x => x.INamLamViec == _sessionInfo.YearOfWork).ToList();

                _dicChiTiet = new Dictionary<Guid, BhQtcNamKinhPhiKhacChiTiet>();
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;

                string sLNSImport = string.Empty;
                var dataImport = _importService.ProcessData<BhThamDinhQuyetToanDetailImportModel>(FilePath, false);
                var lstChungTuImport = new ObservableCollection<BhThamDinhQuyetToanDetailImportModel>(dataImport.Data);

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

                if (SelectedDonVi == null)
                {
                    MessageBoxHelper.Error(Resources.MsgDonViEmpty);
                    OnResetData();
                    return;
                }

                bool isExist = chungTuThamDinh.Any(x => x.IID_MaDonVi.Equals(SelectedDonVi.ValueItem) && x.INamLamViec == _sessionInfo.YearOfWork);

                if (isExist)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.AlertExistVoucher, SelectedDonVi.ValueItem, _sessionInfo.YearOfWork));
                    return;
                }

                var listDetailImport = lstChungTuImport.Where(x => x.ImportStatus && !x.IsWarning && x.HasData).ToList();
                if (listDetailImport.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.MsgCheckLuaChonLaiLoaiKP, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    OnResetData();
                    return;
                }

                var lstMa = ExistedDMTDQTMuclucs.Select(x => x.IMa.ToString());
                foreach (var item in listDetailImport )
                {
                    if (!lstMa.Contains(item.IMa))
                    {
                        item.ImportStatus = false;
                    }
                }

                if (listDetailImport.Any(x => !x.ImportStatus))
                {
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }


                Items = new ObservableCollection<BhThamDinhQuyetToanDetailImportModel>(listDetailImport);
                if (lstError.Any())
                {
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                OnPropertyChanged(nameof(IsSaveData));
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

            SSoQuyetDinh = string.Empty;
            Items = new ObservableCollection<BhThamDinhQuyetToanDetailImportModel>();
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_danhmuc = PredicateBuilder.True<BhDmThamDinhQuyetToan>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);

            //_nsBhYtMucLucs = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).ToList()
            _nsDmTDQTMucLucs = _bhDmThamDinhQuyetToanService.FindAll(predicate_danhmuc).ToList();
            _importedMlns = new ObservableCollection<BhDmMucLucNganSachModel>();
            _existedDMTDQTMuclucs = new ObservableCollection<BhDmThamDinhQuyetToanModel>(_mapper.Map<ObservableCollection<BhDmThamDinhQuyetToanModel>>(_nsDmTDQTMucLucs.OrderBy(x => x.ISTT)));
            _existedDMTDQTMuclucs.Where(x => x.IKieuChu == 1).Select(x => x.IsHangCha = true).ToList();
            //_existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_nsBhYtMucLucs.OrderBy(x => x.SXauNoiMa)))
            _impHistory = new ImpHistory();
            _listErrChungTu = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;
            LstFile = new ObservableCollection<FileFtpModel>();

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(TabIndex));
            //OnPropertyChanged(nameof(ExistedMlns))
            OnPropertyChanged(nameof(ExistedDMTDQTMuclucs));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(LstFile));
            OnPropertyChanged(nameof(IsSelectedFile));
        }
        #endregion


        #region On Save Data
        private void OnSaveData()
        {
            try
            {
                var entity = new BhThamDinhQuyetToan()
                {
                    SSoChungTu = SSoChungTu,
                    DNgayTao = DateTime.Now,
                    DNgaySua = DateTime.Now,
                    DNgayChungTu = NgayChungTu,
                    INamLamViec = _sessionInfo.YearOfWork,
                    IID_MaDonVi = SelectedDonVi.ValueItem,
                    SNguoiTao = _sessionInfo.Principal,
                    SNguoiSua = _sessionInfo.Principal,
                    SMoTa = "Thêm thông qua import"
                };

                var mucLucs = _bhDmThamDinhQuyetToanService.FindAll(x => x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(x => x.IMa).ToList();
                var mucLucModel = _mapper.Map<List<BhDmThamDinhQuyetToanModel>>(mucLucs);
                var lstIMas = mucLucModel.Select(x => x.IMaCha).Distinct();
                mucLucModel.Where(x => lstIMas.Contains(x.IMa)).Select(x => x.IsHangCha = true).ToList();

                _bhThamDinhQuyetToanService.Add(entity);

                List<BhThamDinhQuyetToanChiTiet> listDetails = new List<BhThamDinhQuyetToanChiTiet>();
                List<BhThamDinhQuyetToanDetailImportModel> lstDetailImports = Items.Where(x => x.ImportStatus && !x.IsWarning && x.HasData
                                                                                && mucLucModel.Any(y => y.IMa.ToString() == x.IMa)).ToList();

                foreach (var item in lstDetailImports)
                {
                    var detailEntity = new BhThamDinhQuyetToanChiTiet();
                    detailEntity.IID_BH_TDQT_ChungTu = entity.Id;
                    detailEntity.INamLamViec = _sessionInfo.YearOfWork;
                    detailEntity.IID_MaDonVi = SelectedDonVi.ValueItem;
                    detailEntity.IMa = ConvertStringToNumber<int>(item.IMa);
                    detailEntity.FSoBaoCao = ConvertStringToNumber<double>(item.SSoBaoCao);
                    detailEntity.FSoThamDinh = ConvertStringToNumber<double>(item.SSoThamDinh);
                    detailEntity.FQuanNhan = ConvertStringToNumber<double>(item.SQuanNhan);
                    detailEntity.FCNVLDHD = ConvertStringToNumber<double>(item.SSoCNVLDHD);
                    listDetails.Add(detailEntity);
                }

                _bhThamDinhQuyetToanChiTietService.AddRange(listDetails);

                if (listDetails.Any())
                {
                    entity.FSoBaoCao = listDetails.Sum(x => x.FSoBaoCao);
                    entity.FSoThamDinh = listDetails.Sum(x => x.FSoThamDinh);
                    entity.FQuanNhan = listDetails.Sum(x => x.FQuanNhan);
                    entity.FCNVLDHD = listDetails.Sum(x => x.FCNVLDHD);
                    _bhThamDinhQuyetToanService.Update(entity);
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<BhThamDinhQuyetToanModel>(entity));
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private T ConvertStringToNumber<T>(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return default(T);
            }
            else
            {
                return (T)Convert.ChangeType(input, typeof(T));
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

        private void LoadData()
        {
            SSoChungTu = _bhThamDinhQuyetToanService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork, _sessionInfo.Budget, _sessionInfo.YearOfBudget);
        }
    }
}
