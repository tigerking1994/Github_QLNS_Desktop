using AutoMapper;
using FlexCel.XlsAdapter;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Control;
using System.IO;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu.Import
{
    public class DieuChinhDuToanThuImportViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IBhDcDuToanThuService _dttBHXHService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDcDuToanThuChiTietService _dttBHXHChiTietService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private List<ImportErrorItem> _lstErrDttBHXH = new List<ImportErrorItem>();
        public override string Name => "Điều chỉnh DT thu BHXH, BHYT, BHTN";
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu.Import.DieuChinhDuToanThuImport);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private Dictionary<Guid, BhDcDuToanThuChiTiet> _dicBhxhChiTiet;
        public string SSoChungTu { get; set; }
        private List<BhDmMucLucNganSach> _nsBhxhMucLucs;
        private ObservableCollection<BhDmMucLucNganSachModel> _existedMlns;
        public ObservableCollection<BhDmMucLucNganSachModel> ExistedMlns
        {
            get => _existedMlns;
            set => SetProperty(ref _existedMlns, value);
        }
        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        private DieuChinhDTTImportModel _selectedItem;
        public DieuChinhDTTImportModel SelectedItem
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
                _isSaveData = (Items.Any() && !_lstErrDttBHXH.Any());
                return _isSaveData;
            }
            set
            {
                SetProperty(ref _isSaveData, value);
            }
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
        private ObservableCollection<DieuChinhDTTImportModel> _items;
        public ObservableCollection<DieuChinhDTTImportModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
                OnPropertyChanged(nameof(IsSaveData));
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
        public DateTime NgayChungTu { get; set; }
        public string MoTa { get; set; }
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand DownloadTemplateCommand { get; }
        public RelayCommand HandleDataCommand { get; }
        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        public DieuChinhDuToanThuImportViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            IBhDcDuToanThuService dttBHXHService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog logger,
            IBhDcDuToanThuChiTietService dttBHXHChiTietService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _dttBHXHService = dttBHXHService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _dttBHXHChiTietService = dttBHXHChiTietService;
            _logger = logger;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            DownloadTemplateCommand = new RelayCommand(obj => OnDownloadTemplate());
            HandleDataCommand = new RelayCommand(obj => HandleData());
        }


        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
            LoadEstimateType();
            LoadDonVi();
            OnResetData();
            NgayChungTu = DateTime.Now;
        }

        private void OnResetData()
        {
            _filePath = string.Empty;
            _items = new ObservableCollection<DieuChinhDTTImportModel>();
            _lstErrDttBHXH.Clear();
            _tabIndex = ImportTabIndex.Data;
            var predicateMLNS = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicateMLNS = predicateMLNS.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicateMLNS).Where(x => x.SLNS == BhxhMLNS.KHOI_DU_TOAN || x.SLNS == BhxhMLNS.KHOI_HACH_TOAN).OrderBy(x => x.SXauNoiMa).ToList();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));
            if (_existedMlns.Any())
                _existedMlns.RemoveAt(0);
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsSelectedFile));
        }
        private void OnDownloadTemplate()
        {
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_DC_DTT_IMPORT_TEMPLATE, ExportFileName.RPT_BH_DC_DTT_CHUNG_TU_CHITIET);
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
                    System.Windows.MessageBox.Show(Resources.MesDownloadSuccess);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }

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
                _lstErrDttBHXH.Clear();
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
        private void HandleData()
        {
            var fileExtension = Path.GetExtension(FilePath).ToLower();
            if (!(fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx")))
            {
                System.Windows.MessageBox.Show(Resources.FileImportWrongExtensionExcel, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            List<Guid> _lstIdBhxhChiTiet = new List<Guid>();
            var bhxhChiTiet = _dttBHXHChiTietService.FindAllChungTuDuToan();

            if (bhxhChiTiet != null)
            {
                foreach (var item in bhxhChiTiet)
                {
                    if (!_lstIdBhxhChiTiet.Contains(item.Id))
                    {
                        _lstIdBhxhChiTiet.Add(item.Id);
                    }
                }
                _lstErrDttBHXH.Clear();
            }
            _dicBhxhChiTiet = new Dictionary<Guid, BhDcDuToanThuChiTiet>();
            try
            {
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;

                _nsBhxhMucLucs = _bhDmMucLucNganSachService.GetListBhMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN);
                var dataImport = _importService.ProcessData<DieuChinhDTTImportModel>(FilePath);
                var bhxhImportModels = new ObservableCollection<DieuChinhDTTImportModel>(dataImport.Data);

                if (!Items.Any())
                    Items = bhxhImportModels;

                List<string> lstError = new List<string>();

                if (dataImport.ImportErrors.Count > 0)
                {
                    _lstErrDttBHXH.AddRange(dataImport.ImportErrors);
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
                    item.IsHangCha = _nsBhxhMucLucs.FirstOrDefault(x => x.SXauNoiMa == item.SXauNoiMa)?.BHangCha ?? false;
                    ++i;
                    var listError = ValidateItem(item, i);

                    if (listError.Any())
                    {
                        _lstErrDttBHXH.AddRange(listError);
                        item.ImportStatus = false;

                        if (listError.Any(x => x.IsErrorMLNS))
                        {
                            item.IsError = true;
                        }
                    }
                    else
                    {
                        if (!_lstErrDttBHXH.Any(x => x.Row == i - 1))
                            item.ImportStatus = true;
                    }

                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(DieuChinhDTTImportModel.SXauNoiMa))
                        {
                            DieuChinhDTTImportModel item = (DieuChinhDTTImportModel)sender;
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

        private List<ImportErrorItem> ValidateItem(DieuChinhDTTImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                var lstXauNoiMaMlns = _bhDmMucLucNganSachService.GetListBhMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN)
                    .Select(x => x.SXauNoiMa).ToList();
                if (!lstXauNoiMaMlns.Contains(item.SXauNoiMa))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Xâu nối mã",
                        Error = string.Format(Resources.MsgXauNoiMaKhongTonTai, "nội dung"),
                        Row = rowIndex - 1
                    });
                }
                return errors;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new List<ImportErrorItem>();
            }
        }
        private void ShowError()
        {
            int rowIndex = Items.IndexOf(SelectedItem);
            List<string> errors = _lstErrDttBHXH.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void OnSaveData()
        {
            if (SelectedDonVi == null)
            {
                MessageBoxHelper.Error(Resources.MsgDonViEmpty);
                return;
            }
            var yearOfWork = _sessionInfo.YearOfWork;
            string sLNSImport = string.Join(",", Items.Select(x => x.SXauNoiMa.Substring(0, 7)).ToList().Distinct());
            _nsBhxhMucLucs = _bhDmMucLucNganSachService.GetListBhMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN);
            BhDcDuToanThu chungTu = new BhDcDuToanThu();
            chungTu.SSoChungTu = SSoChungTu;
            chungTu.IIDMaDonVi = SelectedDonVi.ValueItem;
            chungTu.DNgayChungTu = NgayChungTu == null ? DateTime.Now : NgayChungTu;
            chungTu.SMoTa = MoTa;
            chungTu.INamLamViec = yearOfWork;
            chungTu.DNgayTao = DateTime.Now;
            chungTu.SNguoiTao = _sessionInfo.Principal;
            chungTu.ILoaiTongHop = 1;
            chungTu.IIdDonViId = new Guid(SelectedDonVi.HiddenValue);
            chungTu.SLNS = sLNSImport;
            chungTu.BDaTongHop = false;
            _dttBHXHService.Add(chungTu);
            var lstXauNoiMaMlns = _bhDmMucLucNganSachService.GetListBhMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN).ToList();
            List<BhDcDuToanThuChiTiet> chungTuChiTiets = new List<BhDcDuToanThuChiTiet>();
            List<DieuChinhDTTImportModel> listDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasDttData
            && _nsBhxhMucLucs.Any(y => !y.BHangCha && y.SMoTa == x.STenMLNS)).ToList();
            if (listDetailImport.Count > 0)
            {
                foreach (var item in listDetailImport)
                {
                    BhDmMucLucNganSach mucLuc = lstXauNoiMaMlns.FirstOrDefault(x => x.INamLamViec == yearOfWork && x.ITrangThai == StatusType.ACTIVE
                    && item.SXauNoiMa == x.SXauNoiMa);
                    if (mucLuc == null)
                    {
                        continue;
                    }
                    BhDcDuToanThuChiTiet bhDttChiTiet = new BhDcDuToanThuChiTiet();
                    bhDttChiTiet.IIDDttDieuChinh = chungTu.Id;
                    bhDttChiTiet.IIDMLNS = mucLuc.IIDMLNS;
                    bhDttChiTiet.IIDMLNSCha = mucLuc.IIDMLNSCha;
                    bhDttChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    bhDttChiTiet.DNgayTao = DateTime.Now;
                    bhDttChiTiet.SNguoiTao = _sessionInfo.Principal;
                    bhDttChiTiet.SLNS = mucLuc.SLNS;
                    bhDttChiTiet.INamLamViec = yearOfWork;
                    bhDttChiTiet.IIdMaDonVi = SelectedDonVi.ValueItem;
                    bhDttChiTiet.FThuBHXHNLD = ConvertStringToNumber<double>(item.FThuBHXHNLD);
                    bhDttChiTiet.FThuBHXHNSD = ConvertStringToNumber<double>(item.FThuBHXHNSD);
                    bhDttChiTiet.FThuBHYTNLD = ConvertStringToNumber<double>(item.FThuBHXHNSD);
                    bhDttChiTiet.FThuBHYTNSD = ConvertStringToNumber<double>(item.FThuBHYTNSD);
                    bhDttChiTiet.FThuBHTNNLD = ConvertStringToNumber<double>(item.FThuBHTNNLD);
                    bhDttChiTiet.FThuBHTNNSD = ConvertStringToNumber<double>(item.FThuBHTNNSD);
                    bhDttChiTiet.FThuBHXHNLDQTDauNam = ConvertStringToNumber<double>(item.FThuBHXHNLDQTDauNam);
                    bhDttChiTiet.FThuBHXHNSDQTDauNam = ConvertStringToNumber<double>(item.FThuBHXHNSDQTDauNam);
                    bhDttChiTiet.FThuBHYTNLDQTDauNam = ConvertStringToNumber<double>(item.FThuBHYTNLDQTDauNam);
                    bhDttChiTiet.FThuBHYTNSDQTDauNam = ConvertStringToNumber<double>(item.FThuBHYTNSDQTDauNam);
                    bhDttChiTiet.FThuBHTNNLDQTDauNam = ConvertStringToNumber<double>(item.FThuBHTNNLDQTDauNam);
                    bhDttChiTiet.FThuBHTNNSDQTDauNam = ConvertStringToNumber<double>(item.FThuBHTNNSDQTDauNam);
                    bhDttChiTiet.FThuBHXHNLDQTCuoiNam = ConvertStringToNumber<double>(item.FThuBHXHNLDQTCuoiNam);
                    bhDttChiTiet.FThuBHXHNSDQTCuoiNam = ConvertStringToNumber<double>(item.FThuBHXHNSDQTCuoiNam);
                    bhDttChiTiet.FThuBHYTNLDQTCuoiNam = ConvertStringToNumber<double>(item.FThuBHYTNLDQTCuoiNam);
                    bhDttChiTiet.FThuBHYTNSDQTCuoiNam = ConvertStringToNumber<double>(item.FThuBHYTNSDQTCuoiNam);
                    bhDttChiTiet.FThuBHTNNLDQTCuoiNam = ConvertStringToNumber<double>(item.FThuBHTNNLDQTCuoiNam);
                    bhDttChiTiet.FThuBHTNNSDQTCuoiNam = ConvertStringToNumber<double>(item.FThuBHTNNSDQTCuoiNam);

                    bhDttChiTiet.FThuBHXHNLDTang = ConvertStringToNumber<double>(item.FThuBHXHNLDTang);
                    bhDttChiTiet.FThuBHXHNSDTang = ConvertStringToNumber<double>(item.FThuBHXHNSDTang);
                    bhDttChiTiet.FThuBHXHNLDGiam = ConvertStringToNumber<double>(item.FThuBHXHNLDGiam);
                    bhDttChiTiet.FThuBHXHNSDGiam = ConvertStringToNumber<double>(item.FThuBHXHNSDGiam);

                    bhDttChiTiet.FThuBHYTNLDTang = ConvertStringToNumber<double>(item.FThuBHYTNLDTang);
                    bhDttChiTiet.FThuBHYTNSDTang = ConvertStringToNumber<double>(item.FThuBHYTNSDTang);
                    bhDttChiTiet.FThuBHYTNLDGiam = ConvertStringToNumber<double>(item.FThuBHYTNLDGiam);
                    bhDttChiTiet.FThuBHYTNSDGiam = ConvertStringToNumber<double>(item.FThuBHYTNSDGiam);

                    bhDttChiTiet.FThuBHTNNLDTang = ConvertStringToNumber<double>(item.FThuBHTNNLDTang);
                    bhDttChiTiet.FThuBHTNNSDTang = ConvertStringToNumber<double>(item.FThuBHTNNSDTang);
                    bhDttChiTiet.FThuBHTNNLDGiam = ConvertStringToNumber<double>(item.FThuBHTNNLDGiam);
                    bhDttChiTiet.FThuBHTNNSDGiam = ConvertStringToNumber<double>(item.FThuBHTNNSDGiam);

                    bhDttChiTiet.FTongCong = bhDttChiTiet.FThuBHXHNLDQTDauNam + bhDttChiTiet.FThuBHXHNSDQTDauNam + bhDttChiTiet.FThuBHYTNLDQTDauNam +
                    bhDttChiTiet.FThuBHYTNSDQTDauNam + bhDttChiTiet.FThuBHTNNLDQTDauNam + bhDttChiTiet.FThuBHYTNLDQTCuoiNam +
                    bhDttChiTiet.FThuBHTNNSDQTDauNam + bhDttChiTiet.FThuBHXHNLDQTCuoiNam + bhDttChiTiet.FThuBHXHNSDQTCuoiNam + 
                    bhDttChiTiet.FThuBHYTNSDQTCuoiNam + bhDttChiTiet.FThuBHTNNLDQTCuoiNam +bhDttChiTiet.FThuBHTNNSDQTCuoiNam;
                    chungTuChiTiets.Add(bhDttChiTiet);
                }
                _dttBHXHChiTietService.AddRange(chungTuChiTiets);
                if (chungTuChiTiets.Count > 0)
                {
                    chungTu.FThuBHXHNLD = chungTuChiTiets.Sum(item => item.FThuBHXHNLD);
                    chungTu.FThuBHXHNSD = chungTuChiTiets.Sum(item => item.FThuBHXHNSD);
                    chungTu.FThuBHYTNLD = chungTuChiTiets.Sum(item => item.FThuBHYTNLD);
                    chungTu.FThuBHYTNSD = chungTuChiTiets.Sum(item => item.FThuBHYTNSD);
                    chungTu.FThuBHTNNLD = chungTuChiTiets.Sum(item => item.FThuBHTNNLD);
                    chungTu.FThuBHTNNSD = chungTuChiTiets.Sum(item => item.FThuBHTNNSD);
                    chungTu.FThuBHXHNLDQTDauNam = chungTuChiTiets.Sum(item => item.FThuBHXHNLDQTDauNam);
                    chungTu.FThuBHXHNSDQTDauNam = chungTuChiTiets.Sum(item => item.FThuBHXHNSDQTDauNam);
                    chungTu.FThuBHYTNLDQTDauNam = chungTuChiTiets.Sum(item => item.FThuBHYTNLDQTDauNam);
                    chungTu.FThuBHYTNSDQTDauNam = chungTuChiTiets.Sum(item => item.FThuBHYTNSDQTDauNam);
                    chungTu.FThuBHTNNLDQTDauNam = chungTuChiTiets.Sum(item => item.FThuBHTNNLDQTDauNam);
                    chungTu.FThuBHTNNSDQTDauNam = chungTuChiTiets.Sum(item => item.FThuBHTNNSDQTDauNam);
                    chungTu.FThuBHXHNLDQTCuoiNam = chungTuChiTiets.Sum(item => item.FThuBHXHNLDQTCuoiNam);
                    chungTu.FThuBHXHNSDQTCuoiNam = chungTuChiTiets.Sum(item => item.FThuBHXHNSDQTCuoiNam);
                    chungTu.FThuBHYTNLDQTCuoiNam = chungTuChiTiets.Sum(item => item.FThuBHYTNLDQTCuoiNam);
                    chungTu.FThuBHYTNSDQTCuoiNam = chungTuChiTiets.Sum(item => item.FThuBHYTNSDQTCuoiNam);
                    chungTu.FThuBHTNNLDQTCuoiNam = chungTuChiTiets.Sum(item => item.FThuBHTNNLDQTCuoiNam);
                    chungTu.FThuBHTNNSDQTCuoiNam = chungTuChiTiets.Sum(item => item.FThuBHTNNSDQTCuoiNam);
                    chungTu.FTongThuBHXHNLD = chungTuChiTiets.Sum(item => item.FTongThuBHXHNLD);
                    chungTu.FTongThuBHXHNSD = chungTuChiTiets.Sum(item => item.FTongThuBHXHNSD);
                    chungTu.FTongThuBHYTNLD = chungTuChiTiets.Sum(item => item.FTongThuBHYTNLD);
                    chungTu.FTongThuBHYTNSD = chungTuChiTiets.Sum(item => item.FTongThuBHYTNSD);
                    chungTu.FTongThuBHTNNLD = chungTuChiTiets.Sum(item => item.FTongThuBHTNNLD);
                    chungTu.FTongThuBHTNNSD = chungTuChiTiets.Sum(item => item.FTongThuBHTNNSD);
                    chungTu.FThuBHYTNSDQTDauNam = chungTuChiTiets.Sum(item => item.FThuBHYTNSDQTDauNam);
                    chungTu.FThuBHTNNSDQTDauNam = chungTuChiTiets.Sum(item => item.FThuBHTNNSDQTDauNam);
                    chungTu.FThuBHYTNSDQTCuoiNam = chungTuChiTiets.Sum(item => item.FThuBHYTNSDQTCuoiNam);
                    chungTu.FThuBHXHTang = chungTuChiTiets.Sum(item => item.FThuBHXHTang);
                    chungTu.FThuBHYTTang = chungTuChiTiets.Sum(item => item.FThuBHYTTang);
                    chungTu.FThuBHTNTang = chungTuChiTiets.Sum(item => item.FThuBHTNTang);
                    chungTu.FThuBHXHGiam = chungTuChiTiets.Sum(item => item.FThuBHXHGiam);
                    chungTu.FThuBHYTGiam = chungTuChiTiets.Sum(item => item.FThuBHYTGiam);
                    chungTu.FThuBHTNGiam = chungTuChiTiets.Sum(item => item.FThuBHTNGiam);
                    chungTu.FTongCong = chungTuChiTiets.Sum(item => item.FTongCong);

                    _dttBHXHService.Update(chungTu);
                }

                BhDcDuToanThuModel dttBhxh = _mapper.Map<BhDcDuToanThuModel>(chungTu);
                DialogHost.CloseDialogCommand.Execute(dttBhxh, null);
                SavedAction?.Invoke(dttBhxh);
            }
            else
            {
                System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void LoadDonVi()
        {
            ItemsDonVi = new ObservableCollection<ComboboxItem>();
            var lstCurrentUnitBh = _dttBHXHService.FindCurrentUnits(_sessionService.Current.YearOfWork);
            IEnumerable<DonVi> listDonVi = _donViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(x => x.Loai != LoaiDonVi.ROOT && !lstCurrentUnitBh.Contains(x.IIDMaDonVi));
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
        private void LoadEstimateType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = EstimateType.EstimateTypeName[EstimateTypeNum.YEAR], ValueItem = ((int)EstimateTypeNum.YEAR).ToString()},
                new ComboboxItem {DisplayItem = EstimateType.EstimateTypeName[EstimateTypeNum.ADDITIONAL], ValueItem = ((int)EstimateTypeNum.ADDITIONAL).ToString()},
                new ComboboxItem {DisplayItem = EstimateType.EstimateTypeName[EstimateTypeNum.ADJUSTED], ValueItem = ((int)EstimateTypeNum.ADJUSTED).ToString()}
            };

            CbxEstimateType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            _cbxEstimateTypeSelected = CbxEstimateType.First();
        }

        private void LoadData()
        {
            var soChungTuIndex = _dttBHXHService.GetSoChungTuIndexByCondition();
            SSoChungTu = "DC-" + soChungTuIndex.ToString("D3");
        }
    }
}
