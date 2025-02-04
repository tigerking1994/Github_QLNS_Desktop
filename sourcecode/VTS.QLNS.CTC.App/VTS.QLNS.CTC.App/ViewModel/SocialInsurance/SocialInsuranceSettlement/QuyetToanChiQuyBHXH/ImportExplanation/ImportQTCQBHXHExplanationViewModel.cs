using AutoMapper;
using FlexCel.XlsAdapter;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.ImportExplanation;
using System.Text;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.ImportExplanation
{
    public class ImportQTCQBHXHExplanationViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IQtcqBHXHService _chungTuService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IQtcqBHXHChiTietService _chungTuChiTietService;
        private readonly IBhQtcqCtctGtTroCapService _iBhQtcqCtctGtTroCapService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        private List<ImportErrorItem> _lstErrQtcqBHXH = new List<ImportErrorItem>();
        private List<ImportErrorItem> _lstErrExplainBHXH = new List<ImportErrorItem>();
        public override string Name => "QTC quý BHXH";
        public override Type ContentType => typeof(ImportQTCQBHXHExplanation);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private Dictionary<Guid, BhQttBHXHChiTiet> _dicChungTuChiTiet;
        private Guid _iDBhQtcqBHXH;
        public Guid IdBhQtcqBHXH 
        {
            get => _iDBhQtcqBHXH;
            set => SetProperty(ref _iDBhQtcqBHXH, value);
        }
        public string IIdMaDonVi { get; set; }
        public int IQuy { get; set; }
        private QtcqLuongDetailImportModel _selectedQTCQItem;
        public QtcqLuongDetailImportModel SelectedQTCQItem
        {
            get => _selectedQTCQItem;
            set => SetProperty(ref _selectedQTCQItem, value);
        }
        private QtcqExplainImportModel _selectedExplainItem;
        public QtcqExplainImportModel SelectedExplainItem
        {
            get => _selectedExplainItem;
            set => SetProperty(ref _selectedExplainItem, value);
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
                _isSaveData = (QTCQItems.Any() && ExplainItems.Any() && !_lstErrQtcqBHXH.Any() && !_lstErrExplainBHXH.Any());
                return _isSaveData;
            }
            set
            {
                SetProperty(ref _isSaveData, value);
            }
        }
        private ComboboxItem _agencySelected;
        public ComboboxItem AgencySelected
        {
            get => _agencySelected;
            set => SetProperty(ref _agencySelected, value);
        }
        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }
        private List<ComboboxItem> _quarterMonths;
        public List<ComboboxItem> QuarterMonths
        {
            get => _quarterMonths;
            set => SetProperty(ref _quarterMonths, value);
        }
        private ComboboxItem _quarterMonthSelected;
        public ComboboxItem QuarterMonthSelected
        {
            get => _quarterMonthSelected;
            set => SetProperty(ref _quarterMonthSelected, value);
        }
        private ObservableCollection<QtcqLuongDetailImportModel> _qTCQItems;
        public ObservableCollection<QtcqLuongDetailImportModel> QTCQItems
        {
            get => _qTCQItems;
            set
            {
                SetProperty(ref _qTCQItems, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
        }
        private ObservableCollection<QtcqExplainImportModel> _explainItems;
        public ObservableCollection<QtcqExplainImportModel> ExplainItems
        {
            get => _explainItems;
            set
            {
                SetProperty(ref _explainItems, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
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
        public string MoTa { get; set; }
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand DownloadTemplateCommand { get; }
        public RelayCommand HandleDataCommand { get; }
        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        public ImportQTCQBHXHExplanationViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            IQtcqBHXHService qttBHXHService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog logger,
            IQtcqBHXHChiTietService qttBHXHChiTietService,
            IBhQtcqCtctGtTroCapService iBhQtcqCtctGtTroCapService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _chungTuService = qttBHXHService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _chungTuChiTietService = qttBHXHChiTietService;
            _iBhQtcqCtctGtTroCapService = iBhQtcqCtctGtTroCapService;
            _logger = logger;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            SaveCommand = new RelayCommand(obj => OnSaveData(obj));
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(ShowError);
            HandleDataCommand = new RelayCommand(obj => HandleData());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            OnResetData();
        }

        private void OnResetData()
        {
            _filePath = string.Empty;
            _qTCQItems = new ObservableCollection<Model.Import.QtcqLuongDetailImportModel>();
            _explainItems = new ObservableCollection<Model.Import.QtcqExplainImportModel>();
            _lstErrQtcqBHXH.Clear();
            _lstErrExplainBHXH.Clear();
            _tabIndex = ImportTabIndex.Data;
            _agencySelected = null;
            var mucLucNganSachs = _bhDmMucLucNganSachService.GetMLNSCheDoBHXH(_sessionInfo.YearOfWork).ToList();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(mucLucNganSachs));

            OnPropertyChanged(nameof(AgencySelected));
            OnPropertyChanged(nameof(TabIndex));
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(QTCQItems));
            OnPropertyChanged(nameof(ExplainItems));
            OnPropertyChanged(nameof(IsSaveData));
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
                _lstErrQtcqBHXH.Clear();
                FilePath = openFileDialog.FileName;
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
            List<Guid> _lstIdBhxhChiTiet = new List<Guid>();
            var bhxhChiTiet = _chungTuChiTietService.FindAllVouchers();

            if (bhxhChiTiet != null)
            {
                foreach (var item in bhxhChiTiet)
                {
                    if (!_lstIdBhxhChiTiet.Contains(item.Id))
                    {
                        _lstIdBhxhChiTiet.Add(item.Id);
                    }
                }
            }
            _dicChungTuChiTiet = new Dictionary<Guid, BhQttBHXHChiTiet>();
            try
            {
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;

                var dataImport = _importService.ProcessData<QtcqLuongDetailImportModel>(FilePath);
                var qttImportModels = new ObservableCollection<QtcqLuongDetailImportModel>(dataImport.Data);
                var dataExplainImport = _importService.ProcessData<QtcqExplainImportModel>(FilePath);
                var explainImportModels = new ObservableCollection<QtcqExplainImportModel>(dataExplainImport.Data);

                List<string> lstError = new List<string>();

                if (dataImport.ImportErrors.Count > 0)
                {
                    _lstErrQtcqBHXH.AddRange(dataImport.ImportErrors);
                }

                if (dataExplainImport.ImportErrors.Count > 0)
                {
                    _lstErrExplainBHXH.AddRange(dataExplainImport.ImportErrors);
                }

                if (qttImportModels == null || qttImportModels.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.QTCQuyImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (explainImportModels == null || explainImportModels.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.CanBoCheDoImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (string.IsNullOrEmpty(FilePath))
                {
                    lstError.Add(Resources.ErrorFileEmpty);
                }

                int i = 0;
                foreach (var item in qttImportModels)
                {
                    ++i;
                    var listError = ValidateItem(item, i);
                    if (listError.Count > 0)
                    {
                        _lstErrQtcqBHXH.AddRange(listError);
                        item.ImportStatus = false;

                        if (listError.Any(x => x.IsErrorMLNS))
                        {
                            item.IsError = true;
                        }
                    }
                }

                int j = 0;
                foreach (var item in explainImportModels)
                {
                    ++j;
                    var listError = ValidateExplainItem(item, j);
                    if (listError.Count > 0)
                    {
                        _lstErrExplainBHXH.AddRange(listError);
                        item.ImportStatus = false;

                        if (listError.Any(x => x.IsErrorMLNS))
                        {
                            item.IsError = true;
                        }
                    }
                }
                QTCQItems = qttImportModels;
                if (lstError.Any() || qttImportModels.Any(x => !x.ImportStatus) || explainImportModels.Any(x => !x.ImportStatus))
                {
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                ExplainItems = explainImportModels;
            }
            catch (Exception ex)
            {
                FilePath = "";
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
            }

        }

        private List<ImportErrorItem> ValidateItem(QtcqLuongDetailImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                var lstXauNoiMaMlns = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(_sessionInfo.YearOfWork, LNSValue.LNS_9010001_9010002)
                    .Select(x => x.SXauNoiMa).ToList();
                if (!lstXauNoiMaMlns.Contains(item.SXauNoiMa))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Xâu nối mã",
                        Error = string.Format(Resources.MsgXauNoiMaKhongTonTai),
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
        private void ShowError(object param)
        {
            var importTabIndex = (ImportTabIndex)((int)param);
            switch (importTabIndex)
            {
                case ImportTabIndex.Data:
                    int rowIndex = QTCQItems.IndexOf(SelectedQTCQItem);
                    List<string> errors = _lstErrQtcqBHXH.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                    string message = string.Join(Environment.NewLine, errors);
                    System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case ImportTabIndex.Explain:
                    int rowExpIndex = ExplainItems.IndexOf(SelectedExplainItem);
                    List<string> expErrors = _lstErrExplainBHXH.Where(x => x.Row == rowExpIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                    string expMessage = string.Join(Environment.NewLine, expErrors);
                    System.Windows.MessageBox.Show(expMessage, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case ImportTabIndex.MLNS:
                    break;
            }

        }

        private void OnSaveData(object obj)
        {
            try
            {
                var existVoucherDetail = _chungTuChiTietService.ExistVoucherDetail(IdBhQtcqBHXH);
                var existExplanation = _iBhQtcqCtctGtTroCapService.IsExistExplain(IdBhQtcqBHXH);
                if (existVoucherDetail || existExplanation)
                {
                    var messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat(Resources.ConfirmImportQTCQ);
                    var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                         HandleSave);
                    DialogHost.Show(messageBox.Content, "ExplanationDetail");
                    return;
                }
                else
                {
                    OnImportNewItem();
                }
                OnClose(obj);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void HandleSave(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            var voucherDetails = _chungTuChiTietService.FindByVoucherID(IdBhQtcqBHXH);
            var existExplain = _iBhQtcqCtctGtTroCapService.FindByVoucherID(IdBhQtcqBHXH);
            _chungTuChiTietService.RemoveRange(voucherDetails);
            _iBhQtcqCtctGtTroCapService.RemoveRange(existExplain);
            OnImportNewItem();
        }

        private void OnImportNewItem()
        {
            var namLamViec = _sessionInfo.YearOfWork;
            var lstXauNoiMaMLNS = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(namLamViec, LNSValue.LNS_9010001_9010002).ToList();
            List<BhQtcqBHXHChiTiet> lstChungTuChiTiet = new List<BhQtcqBHXHChiTiet>();
            List<BhQtcqCtctGtTroCap> lstExplain = new List<BhQtcqCtctGtTroCap>();
            List<QtcqLuongDetailImportModel> listDetailImport = QTCQItems.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasData
            && lstXauNoiMaMLNS.Any(y => !y.BHangCha && y.SXauNoiMa == x.SXauNoiMa)).ToList();
            List<QtcqExplainImportModel> listExplainImport = ExplainItems.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasData
            && lstXauNoiMaMLNS.Any(y => !y.BHangCha && y.SXauNoiMa == x.SXauNoiMa)).ToList();

            var chungTu = _chungTuService.FindById(IdBhQtcqBHXH);
            if (listDetailImport.Count > 0)
            {
                foreach (var item in listDetailImport)
                {
                    BhDmMucLucNganSach mucLuc = lstXauNoiMaMLNS.FirstOrDefault(x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE
                    && item.SXauNoiMa == x.SXauNoiMa);
                    if (mucLuc == null)
                    {
                        continue;
                    }

                    BhQtcqBHXHChiTiet chungTuChiTiet = new BhQtcqBHXHChiTiet();
                    chungTuChiTiet.Id = Guid.NewGuid();
                    chungTuChiTiet.IdQTCQuyCheDoBHXH = IdBhQtcqBHXH;
                    chungTuChiTiet.DNgayTao = DateTime.Now;
                    chungTuChiTiet.INamLamViec = namLamViec;
                    chungTuChiTiet.IIDMaDonVi = IIdMaDonVi;
                    chungTuChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    chungTuChiTiet.SLNS = mucLuc.SLNS;
                    chungTuChiTiet.IIdMucLucNganSach = mucLuc.IIDMLNS;
                    chungTuChiTiet.SLoaiTroCap = mucLuc.SMoTa;

                    chungTuChiTiet.ISoSQDeNghi = listDetailImport.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => ConvertStringToNumber<int>(x.ISoSQDeNghi)).Sum();
                    chungTuChiTiet.FTienSQDeNghi = listDetailImport.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => ConvertStringToNumber<double>(x.FTienSQDeNghi)).Sum();
                    chungTuChiTiet.ISoQNCNDeNghi = listDetailImport.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => ConvertStringToNumber<int>(x.ISoQNCNDeNghi)).Sum();
                    chungTuChiTiet.FTienQNCNDeNghi = listDetailImport.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => ConvertStringToNumber<double>(x.FTienQNCNDeNghi)).Sum();
                    chungTuChiTiet.ISoCNVCQPDeNghi = listDetailImport.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => ConvertStringToNumber<int>(x.ISoCNVCQPDeNghi)).Sum();
                    chungTuChiTiet.FTienCNVCQPDeNghi = listDetailImport.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => ConvertStringToNumber<double>(x.FTienCNVCQPDeNghi)).Sum();
                    chungTuChiTiet.ISoHSQBSDeNghi = listDetailImport.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => ConvertStringToNumber<int>(x.ISoHSQBSDeNghi)).Sum();
                    chungTuChiTiet.FTienHSQBSDeNghi = listDetailImport.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => ConvertStringToNumber<double>(x.FTienHSQBSDeNghi)).Sum();
                    chungTuChiTiet.ISoLDHDDeNghi = listDetailImport.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => ConvertStringToNumber<int>(x.ISoLDHDDeNghi)).Sum();
                    chungTuChiTiet.FTienLDHDDeNghi = listDetailImport.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => ConvertStringToNumber<double>(x.FTienLDHDDeNghi)).Sum();

                    chungTuChiTiet.ITongSoDeNghi = chungTuChiTiet.ISoSQDeNghi + chungTuChiTiet.ISoQNCNDeNghi + chungTuChiTiet.ISoCNVCQPDeNghi
                        + chungTuChiTiet.ISoHSQBSDeNghi + chungTuChiTiet.ISoLDHDDeNghi;
                    chungTuChiTiet.FTongTienDeNghi = chungTuChiTiet.FTienSQDeNghi + chungTuChiTiet.FTienQNCNDeNghi + chungTuChiTiet.FTienCNVCQPDeNghi
                        + chungTuChiTiet.FTienHSQBSDeNghi + chungTuChiTiet.FTienLDHDDeNghi;
                    lstChungTuChiTiet.Add(chungTuChiTiet);
                }

                if (!listExplainImport.IsEmpty())
                {
                    lstChungTuChiTiet.Select(x =>
                    {
                        if (listExplainImport.Any(y => y.SXauNoiMa.Equals(x.SXauNoiMa)))
                        {
                            x.ISoSQDeNghi = listExplainImport.Any(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("1")) ? listExplainImport.Where(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("1")).Count() : 0;
                            x.FTienSQDeNghi = listExplainImport.Any(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("1")) ? listExplainImport.Where(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("1")).Select(x => ConvertStringToNumber<double>(x.FSoTien)).Sum() : 0;

                            x.ISoQNCNDeNghi = listExplainImport.Any(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("2")) ? listExplainImport.Where(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("2")).Count() : 0;
                            x.FTienQNCNDeNghi = listExplainImport.Any(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("2")) ? listExplainImport.Where(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("2")).Select(x => ConvertStringToNumber<double>(x.FSoTien)).Sum() : 0;

                            x.ISoCNVCQPDeNghi = listExplainImport.Any(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("3.")) ? listExplainImport.Where(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("3.")).Count() : 0;
                            x.FTienCNVCQPDeNghi = listExplainImport.Any(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("3.")) ? listExplainImport.Where(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("3.")).Select(x => ConvertStringToNumber<double>(x.FSoTien)).Sum() : 0;

                            x.ISoHSQBSDeNghi = listExplainImport.Any(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("43")) ? listExplainImport.Where(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("43")).Count() : 0;
                            x.FTienHSQBSDeNghi = listExplainImport.Any(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("43")) ? listExplainImport.Where(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("43")).Select(x => ConvertStringToNumber<double>(x.FSoTien)).Sum() : 0;

                            x.ISoLDHDDeNghi = listExplainImport.Any(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("0")) ? listExplainImport.Where(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("0")).Count() : 0;
                            x.FTienLDHDDeNghi = listExplainImport.Any(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("0")) ? listExplainImport.Where(y => y.SXauNoiMa == x.SXauNoiMa && y.SMaCapBac.StartsWith("0")).Select(x => ConvertStringToNumber<double>(x.FSoTien)).Sum() : 0;
                        }
                        return x;
                    }).ToList();

                }
                _chungTuChiTietService.AddRange(lstChungTuChiTiet);
                if (lstChungTuChiTiet.Count > 0)
                {
                    chungTu.ITongSoSQDeNghi = lstChungTuChiTiet.Sum(item => item.ISoSQDeNghi);
                    chungTu.FTongTienSQDeNghi = lstChungTuChiTiet.Sum(item => item.FTienSQDeNghi);
                    chungTu.ITongSoQNCNDeNghi = lstChungTuChiTiet.Sum(item => item.ISoQNCNDeNghi);
                    chungTu.FTongTienQNCNDeNghi = lstChungTuChiTiet.Sum(item => item.FTienQNCNDeNghi);
                    chungTu.ITongSoCNVCQPDeNghi = lstChungTuChiTiet.Sum(item => item.ISoCNVCQPDeNghi);
                    chungTu.FTongTienCNVCQPDeNghi = lstChungTuChiTiet.Sum(item => item.FTienCNVCQPDeNghi);
                    chungTu.ITongSoHSQBSDeNghi = lstChungTuChiTiet.Sum(item => item.ISoHSQBSDeNghi);
                    chungTu.FTongTienHSQBSDeNghi = lstChungTuChiTiet.Sum(item => item.FTienHSQBSDeNghi);
                    chungTu.ITongSoHDLDDeNghi = lstChungTuChiTiet.Sum(item => item.ISoLDHDDeNghi);
                    chungTu.FTongTienHDLDDeNghi = lstChungTuChiTiet.Sum(item => item.FTienLDHDDeNghi);
                    chungTu.ITongSoDeNghi = lstChungTuChiTiet.Sum(item => item.ITongSoDeNghi);
                    chungTu.FTongTienDeNghi = lstChungTuChiTiet.Sum(item => item.FTongTienDeNghi);
                    _chungTuService.Update(chungTu);
                }
                _chungTuService.DeleteDupItem(IdBhQtcqBHXH);
            }
            else
            {
                System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (listExplainImport.Count > 0)
            {
                foreach (var item in listExplainImport)
                {
                    BhDmMucLucNganSach mucLuc = lstXauNoiMaMLNS.FirstOrDefault(x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE
                    && item.SXauNoiMa == x.SXauNoiMa);
                    if (mucLuc == null)
                    {
                        continue;
                    }

                    BhQtcqCtctGtTroCap giaiThich = new BhQtcqCtctGtTroCap();
                    giaiThich.Id = Guid.NewGuid();
                    giaiThich.IID_QTC_Quy_ChungTu = IdBhQtcqBHXH;
                    giaiThich.DNgayTao = DateTime.Now;
                    giaiThich.INamLamViec = namLamViec;
                    giaiThich.IQuy = IQuy;
                    giaiThich.ID_MaDonVi = IIdMaDonVi;
                    giaiThich.SNguoiTao = _sessionService.Current.Principal;
                    giaiThich.SXauNoiMa = mucLuc.SXauNoiMa;
                    giaiThich.SMaHieuCanBo = item.SMaHieuCanBo;
                    giaiThich.STenCanBo = item.STenCanBo;
                    giaiThich.SMaCapBac = item.SMaCapBac;
                    giaiThich.STenCapBac = item.STenCapBac;
                    giaiThich.ISoNgayHuong = ConvertStringToNumber<int>(item.ISoNgayHuong);
                    giaiThich.SSoQuyetDinh = item.SSoQuyetDinh;
                    giaiThich.DNgayQuyetDinh = ConvertToDateTime(item.DNgayQuyetDinh);
                    giaiThich.FSoTien = listExplainImport.Where(x => x.SXauNoiMa == item.SXauNoiMa && x.SMaCapBac ==item.SMaCapBac).Select(x => ConvertStringToNumber<double>(x.FSoTien)).Sum();
                    giaiThich.ID_MaPhanHo = item.SMaDonVi;
                    giaiThich.STenPhanHo = item.STenDonVi;
                    lstExplain.Add(giaiThich);
                }
                _iBhQtcqCtctGtTroCapService.AddRange(lstExplain);
                _iBhQtcqCtctGtTroCapService.DeleteDupItem(IdBhQtcqBHXH);
            }
            else
            {
                System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            BhQtcqBHXHModel qTCQBHXH = _mapper.Map<BhQtcqBHXHModel>(chungTu);
            DialogHost.CloseDialogCommand.Execute(qTCQBHXH, null);
            SavedAction?.Invoke(qTCQBHXH);
            var message = Resources.MsgSaveDone;
            MessageBoxHelper.Info(message);
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

        private DateTime? ConvertToDateTime(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            if (DateTime.TryParse(input, out DateTime result))
            {
                return result;
            }

            return null;
        }

        private List<ImportErrorItem> ValidateExplainItem(QtcqExplainImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                var lstXauNoiMaMlns = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(_sessionInfo.YearOfWork, LNSValue.LNS_9010001_9010002)
                    .Select(x => x.SXauNoiMa).ToList();
                if (!lstXauNoiMaMlns.Contains(item.SXauNoiMa))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Xâu nối mã",
                        Error = string.Format(Resources.MsgXauNoiMaKhongTonTai),
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
    }
}
