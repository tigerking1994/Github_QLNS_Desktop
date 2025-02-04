using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using AutoMapper.Configuration;
using FlexCel.XlsAdapter;
using log4net;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Control;
using System.Runtime.Remoting.Messaging;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DuToanThuBHXH.Import
{
    public class DuToanThuBHXHImportViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IDttBHXHService _dttBHXHService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IDttBHXHChiTietService _dttBHXHChiTietService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private List<ImportErrorItem> _lstErrDttBHXH = new List<ImportErrorItem>();
        public override string Name => "Dự toán thu BHXH";
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsuranceEstimate.DuToanThuBHXH.Import.DuToanThuBHXHImport);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private Dictionary<Guid, BhDttBHXHChiTiet> _dicBhxhChiTiet;
        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        private List<BhDmMucLucNganSach> _nsBhxhMucLucs;
        private DttBhxhDetailImportModel _selectedItem;
        public DttBhxhDetailImportModel SelectedItem
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
        private ObservableCollection<DttBhxhDetailImportModel> _items;
        public ObservableCollection<DttBhxhDetailImportModel> Items
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

        private ObservableCollection<BhDmMucLucNganSachModel> _existedMlns;
        public ObservableCollection<BhDmMucLucNganSachModel> ExistedMlns
        {
            get => _existedMlns;
            set => SetProperty(ref _existedMlns, value);
        }

        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }
        public DateTime? DNgayQuyetDinh { get; set; }
        public DateTime NgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public string SSoChungTu { get; set; }
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

        public DuToanThuBHXHImportViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            IDttBHXHService dttBHXHService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog logger,
            IDttBHXHChiTietService dttBHXHChiTietService)
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
            DNgayQuyetDinh = DateTime.Now;
        }

        private void OnResetData()
        {
            _filePath = string.Empty;
            _items = new ObservableCollection<DttBhxhDetailImportModel>();
            _lstErrDttBHXH.Clear();
            _tabIndex = ImportTabIndex.Data;
            var predicateMLNS = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicateMLNS = predicateMLNS.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicateMLNS).Where(x => x.SLNS == BhxhMLNS.KHOI_DU_TOAN || x.SLNS == BhxhMLNS.KHOI_HACH_TOAN).OrderBy(x => x.SXauNoiMa).ToList();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));
            _existedMlns.RemoveAt(0);
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsSelectedFile));
        }
        private void OnDownloadTemplate()
        {
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_DTT_IMPORT_TEMPLATE, ExportFileName.IMPORT_BH_DTT_BHXH_CHUNGTU_CHITIET);
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
            var BhxhChiTiet = _dttBHXHChiTietService.FindAllChungTuDuToan();

            if (BhxhChiTiet != null)
            {
                foreach (var item in BhxhChiTiet)
                {
                    if (!_lstIdBhxhChiTiet.Contains(item.Id))
                    {
                        _lstIdBhxhChiTiet.Add(item.Id);
                    }
                }
                _lstErrDttBHXH.Clear();
            }
            _dicBhxhChiTiet = new Dictionary<Guid, BhDttBHXHChiTiet>();
            try
            {
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;

                _nsBhxhMucLucs = _bhDmMucLucNganSachService.GetListBhMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN);
                var dataImport = _importService.ProcessData<DttBhxhDetailImportModel>(FilePath);
                var bhxhImportModels = new ObservableCollection<DttBhxhDetailImportModel>(dataImport.Data);

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
                        if (args.PropertyName == nameof(DttBhxhDetailImportModel.SXauNoiMa))
                        {
                            DttBhxhDetailImportModel item = (DttBhxhDetailImportModel)sender;
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

        private List<ImportErrorItem> ValidateItem(DttBhxhDetailImportModel item, int rowIndex)
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
            var NamLamViec = _sessionInfo.YearOfWork;
            List<string> lstLNS = new List<string>();
            if (string.IsNullOrEmpty(SSoQuyetDinh))
            {
                System.Windows.Forms.MessageBox.Show(Resources.AlertSoQuyetDinhEmpty, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _nsBhxhMucLucs = _bhDmMucLucNganSachService.GetListBhMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN);
            BhDttBHXH chungTu = new BhDttBHXH();
            chungTu.SSoChungTu = SSoChungTu;
            chungTu.IIDMaDonVi = _sessionInfo.IdDonVi;
            chungTu.DNgayChungTu = NgayChungTu == null ? DateTime.Now : NgayChungTu;
            chungTu.SMoTa = MoTa;
            chungTu.INamLamViec = NamLamViec;
            chungTu.DNgayTao = DateTime.Now;
            chungTu.SNguoiTao = _sessionInfo.Principal;
            chungTu.SSoQuyetDinh = SSoQuyetDinh;
            chungTu.DNgayQuyetDinh = DNgayQuyetDinh;
            chungTu.ILoaiDuToan = int.Parse(_cbxEstimateTypeSelected.ValueItem);

            _dttBHXHService.Add(chungTu);
            var lstXauNoiMaMlns = _bhDmMucLucNganSachService.GetListBhMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN).ToList();
            List<BhDttBHXHChiTiet> chungTuChiTiets = new List<BhDttBHXHChiTiet>();
            List<DttBhxhDetailImportModel> listDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasDttData
            && _nsBhxhMucLucs.Any(y => !y.BHangCha && y.SXauNoiMa == x.SXauNoiMa)).ToList();
            if(listDetailImport.Count > 0)
            {
                foreach (var item in listDetailImport)
                {
                    BhDmMucLucNganSach mucLuc = lstXauNoiMaMlns.FirstOrDefault(x => x.INamLamViec == NamLamViec && x.ITrangThai == StatusType.ACTIVE
                    && item.SXauNoiMa == x.SXauNoiMa);
                    if (mucLuc == null)
                    {
                        continue;
                    }
                    BhDttBHXHChiTiet bhDttChiTiet = new BhDttBHXHChiTiet();
                    bhDttChiTiet.DttBHXHId = chungTu.Id;
                    bhDttChiTiet.IIdMlns = mucLuc.IIDMLNS;
                    bhDttChiTiet.IIdMlnsCha = mucLuc.IIDMLNSCha;
                    bhDttChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    bhDttChiTiet.SK = mucLuc.SK;
                    bhDttChiTiet.SL = mucLuc.SL;
                    bhDttChiTiet.SLns = mucLuc.SLNS;
                    bhDttChiTiet.SM = mucLuc.SM;
                    bhDttChiTiet.SNg = mucLuc.SNG;
                    bhDttChiTiet.STm = mucLuc.STM;
                    bhDttChiTiet.STng = mucLuc.STNG;
                    bhDttChiTiet.STng1 = mucLuc.STNG1;
                    bhDttChiTiet.STng2 = mucLuc.STNG2;
                    bhDttChiTiet.STng3 = mucLuc.STNG3;
                    bhDttChiTiet.STtm = mucLuc.STTM;
                    bhDttChiTiet.DNgayTao = DateTime.Now;
                    bhDttChiTiet.SNguoiTao = _sessionInfo.Principal;
                    bhDttChiTiet.FThuBHXHNguoiLaoDong = ConvertStringToNumber<double>(item.FNldBHXH);
                    bhDttChiTiet.FThuBHXHNguoiSuDungLaoDong = ConvertStringToNumber<double>(item.FNsdBHXH);
                    bhDttChiTiet.FTongThuBHXH = bhDttChiTiet.FThuBHXHNguoiLaoDong + bhDttChiTiet.FThuBHXHNguoiSuDungLaoDong;
                    bhDttChiTiet.FThuBHYTNguoiLaoDong = ConvertStringToNumber<double>(item.FNldBHYT);
                    bhDttChiTiet.FThuBHYTNguoiSuDungLaoDong = ConvertStringToNumber<double>(item.FNsdBHYT);
                    bhDttChiTiet.FTongThuBHYT = bhDttChiTiet.FThuBHYTNguoiLaoDong + bhDttChiTiet.FThuBHYTNguoiSuDungLaoDong;
                    bhDttChiTiet.FThuBHTNNguoiLaoDong = ConvertStringToNumber<double>(item.FNldBHTN);
                    bhDttChiTiet.FThuBHTNNguoiSuDungLaoDong = ConvertStringToNumber<double>(item.FNsdBHTN);
                    bhDttChiTiet.FTongThuBHTN = bhDttChiTiet.FThuBHTNNguoiLaoDong + bhDttChiTiet.FThuBHTNNguoiSuDungLaoDong;
                    bhDttChiTiet.SGhiChu = item.SGhiChu;
                    bhDttChiTiet.INamLamViec = NamLamViec;
                    chungTuChiTiets.Add(bhDttChiTiet);
                    lstLNS.Add(mucLuc.SLNS);
                }
                _dttBHXHChiTietService.AddRange(chungTuChiTiets);
                if (chungTuChiTiets.Count > 0)
                {
                    chungTu.FThuBHXHNLDDong = chungTuChiTiets.Sum(item => item.FThuBHXHNguoiLaoDong);
                    chungTu.FThuBHXHNSDDong = chungTuChiTiets.Sum(item => item.FThuBHXHNguoiSuDungLaoDong);
                    chungTu.FThuBHXH = chungTuChiTiets.Sum(item => item.FTongThuBHXH);
                    chungTu.FThuBHYTNLDDong = chungTuChiTiets.Sum(item => item.FThuBHYTNguoiLaoDong);
                    chungTu.FThuBHYTNSDDong = chungTuChiTiets.Sum(item => item.FThuBHYTNguoiSuDungLaoDong);
                    chungTu.FTongBHYT = chungTuChiTiets.Sum(item => item.FTongThuBHYT);
                    chungTu.FThuBHTNNLDDong = chungTuChiTiets.Sum(item => item.FThuBHTNNguoiLaoDong);
                    chungTu.FThuBHTNNSDDong = chungTuChiTiets.Sum(item => item.FThuBHTNNguoiSuDungLaoDong);
                    chungTu.FThuBHTN = chungTuChiTiets.Sum(item => item.FTongThuBHTN);
                    chungTu.FDuToan = chungTu.FThuBHXH + chungTu.FTongBHYT + chungTu.FThuBHTN;
                    chungTu.SDslns = GetValueSelected(lstLNS);
                    _dttBHXHService.Update(chungTu);
                }

                BhDttBHXHModel dttBhxh = _mapper.Map<BhDttBHXHModel>(chungTu);
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
        // Convert string sang int và cho phép return null
        private int? ConvertStringToIntNull(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            else
            {
                return (int)Convert.ChangeType(input, typeof(int));
            }
        }

        public static string GetValueSelected(List<string> data)
        {
            if (data.Count > 0)
            {
                return string.Join(",", data.Distinct().ToList());
            }
            return string.Empty;
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

        private void LoadData()
        {
            var soChungTuIndex = _dttBHXHService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
            SSoChungTu = "DTT-" + soChungTuIndex.ToString("D3");
        }
    }
}
