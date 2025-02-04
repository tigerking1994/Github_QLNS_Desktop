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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT.Import;
using VTS.QLNS.CTC.App.Model.Control;
using System.IO;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT.Import
{
    public class QuyetToanCapKinhPhiKcbImportViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IQtcCapKinhPhiKcbService _chungTuService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IQtcCapKinhPhiKcbChiTietService _chungTuChiTietService;
        private readonly IBhDmCoSoYTeService _cSYTService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        private List<ImportErrorItem> _lstErrQtkcbBHXH = new List<ImportErrorItem>();
        public override string Name => "QUYẾT TOÁN CẤP KP KCB BHYT";
        public override Type ContentType => typeof(QuyetToanCapKinhPhiKcbImport);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private Dictionary<Guid, BhQtCapKinhPhiKcbChiTiet> _dicChungTuChiTiet;
        public int INamChungTu { get; set; }
        public Visibility IsShowColumnKPKCBBHYTQuy => CbxQuarterSelected != null && _sessionInfo.YearOfWork.ToString() != CbxQuarterSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;
        public Visibility IsShowColumnKPKCBBHYTNam => CbxQuarterSelected != null && _sessionInfo.YearOfWork.ToString() == CbxQuarterSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;
        public string HeaderColum => CbxQuarterSelected != null && _sessionInfo.YearOfWork.ToString() == CbxQuarterSelected.ValueItem ? "Số quyết toán" : "Quyết toán quý này";
        private QtCapKinhPhiKcbImportModel _selectedItem;
        public QtCapKinhPhiKcbImportModel SelectedItem
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
                _isSaveData = (Items.Any() && !_lstErrQtkcbBHXH.Any());
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
        private ObservableCollection<ComboboxItem> _cbxQuarter;
        public ObservableCollection<ComboboxItem> CbxQuarter
        {
            get => _cbxQuarter;
            set => SetProperty(ref _cbxQuarter, value);
        }

        private List<BhQtCapKinhPhiKcb> _listChungTuAllQuy;
        public List<BhQtCapKinhPhiKcb> ListChungTuAllLQuy
        {
            get => _listChungTuAllQuy;
            set => SetProperty(ref _listChungTuAllQuy, value);
        }

        private ObservableCollection<QtCapKinhPhiKcbImportModel> _items;
        public ObservableCollection<QtCapKinhPhiKcbImportModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
        }
        private ComboboxItem _cbxQuarterSelected;
        public ComboboxItem CbxQuarterSelected
        {
            get => _cbxQuarterSelected;
            set
            {
                SetProperty(ref _cbxQuarterSelected, value);
                OnPropertyChanged(nameof(IsShowColumnKPKCBBHYTQuy));
                OnPropertyChanged(nameof(IsShowColumnKPKCBBHYTNam));
                OnPropertyChanged(nameof(HeaderColum));
                if (Items != null && Items.Count > 0)
                {
                    HandleDataAfterChangeQuater();
                }
            }
        }
        private ComboboxItem _cbxExpenseTypeSelected;
        public ComboboxItem CbxExpenseTypeSelected
        {
            get => _cbxExpenseTypeSelected;
            set
            {
                SetProperty(ref _cbxExpenseTypeSelected, value);
                if (value != null)
                {
                    ReloadMLNS();
                    OnPropertyChanged(nameof(ExistedMlns));
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxExpenseType;
        public ObservableCollection<ComboboxItem> CbxExpenseType
        {
            get => _cbxExpenseType;
            set => SetProperty(ref _cbxExpenseType, value);
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
        public DateTime NgayChungTu { get; set; }
        public string SSoChungTu { get; set; }
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

        public QuyetToanCapKinhPhiKcbImportViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            IQtcCapKinhPhiKcbService IiQtcCapKinhPhiKcbService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog logger,
            IQtcCapKinhPhiKcbChiTietService bhQtCapKinhPhiKcbChiTietService,
            IBhDmCoSoYTeService bhDmCoSoYTeService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _chungTuService = IiQtcCapKinhPhiKcbService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _chungTuChiTietService = bhQtCapKinhPhiKcbChiTietService;
            _cSYTService = bhDmCoSoYTeService;
            _logger = logger;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(ShowError);
            DownloadTemplateCommand = new RelayCommand(obj => OnDownloadTemplate());
            HandleDataCommand = new RelayCommand(obj => HandleData());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
            LoadQuarters();
            LoadExpenseTypes();
            OnResetData();
            INamChungTu = _sessionInfo.YearOfWork;
            NgayChungTu = DateTime.Now;
        }

        private void OnResetData()
        {
            _filePath = string.Empty;
            _items = new ObservableCollection<QtCapKinhPhiKcbImportModel>();
            _lstErrQtkcbBHXH.Clear();
            _tabIndex = ImportTabIndex.Data;
            var predicateMLNS = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicateMLNS = predicateMLNS.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicateMLNS).Where(x => x.SLNS == (int.Parse(CbxExpenseTypeSelected.ValueItem) == 1 ? BhxhMLNS.KinhPhi_KCB_BHYT_QN : BhxhMLNS.KinhPhi_KCB_BHYT_TNQN_NLD)).OrderBy(x => x.SXauNoiMa).ToList();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));

            OnPropertyChanged(nameof(TabIndex));
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsSelectedFile));
        }

        private void ReloadMLNS()
        {
            var predicateMLNS = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicateMLNS = predicateMLNS.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicateMLNS).Where(x => x.SLNS == (int.Parse(CbxExpenseTypeSelected.ValueItem) == 1 ? BhxhMLNS.KinhPhi_KCB_BHYT_QN : BhxhMLNS.KinhPhi_KCB_BHYT_TNQN_NLD)).OrderBy(x => x.SXauNoiMa).ToList();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));
        }
        private void OnDownloadTemplate()
        {
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTC_KP_KCB_BHYT_IMPORT, ExportFileName.RPT_BH_QTC_KPKCB_CHUNG_TU_CHITIET);
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
                _lstErrQtkcbBHXH.Clear();
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

        private void HandleDataAfterChangeQuater()
        {
            try
            {
                if (Items != null)
                {
                    var yearOfWork = _sessionInfo.YearOfWork;
                    foreach (var item in Items)
                    {
                        if (CbxQuarterSelected != null && CbxQuarterSelected.ValueItem == yearOfWork.ToString())
                        {
                            var maCoSoYTe = item.STenCSYT.Split("-").Length > 0 ? item.STenCSYT.Split("-").FirstOrDefault().Trim() : "";
                            var listChungTuMap = ListChungTuAllLQuy.Where(x => x.SCoSoYTe == maCoSoYTe && item.SLNS == x.SDslns).ToList();
                            item.FQuyetToan4Quy = listChungTuMap.Count > 0 ? listChungTuMap.Sum(x => x.FQuyetToanQuyNay).ToString() : "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FilePath = "";
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
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
                _lstErrQtkcbBHXH.Clear();
            }
            _dicChungTuChiTiet = new Dictionary<Guid, BhQtCapKinhPhiKcbChiTiet>();
            try
            {
                var yearOfWork = _sessionInfo.YearOfWork;
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;

                var dataImport = _importService.ProcessData<QtCapKinhPhiKcbImportModel>(FilePath);
                var bhxhImportModels = new ObservableCollection<QtCapKinhPhiKcbImportModel>(dataImport.Data);

                if (!Items.Any())
                    Items = bhxhImportModels;
                // Delete item Tông cộng
                Items.RemoveAt(Items.Count - 1);

                List<string> lstError = new List<string>();

                if (CbxQuarterSelected == null)
                {
                    MessageBoxHelper.Error(Resources.AlertQuartyEmpty);
                    return;
                }

                if (dataImport.ImportErrors.Count > 0)
                {
                    _lstErrQtkcbBHXH.AddRange(dataImport.ImportErrors);
                }

                if (!Items.Any())
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (string.IsNullOrEmpty(FilePath))
                {
                    lstError.Add(Resources.ErrorFileEmpty);
                }

                if (CbxQuarterSelected.ValueItem == yearOfWork.ToString())
                {
                    var lstQuy = "1,2,3,4";
                    var listChungTu = _chungTuService.FindByYear(yearOfWork).Where(x => lstQuy.Contains(x.IQuy.ToString()));
                    ListChungTuAllLQuy = _mapper.Map<List<BhQtCapKinhPhiKcb>>(listChungTu).ToList();
                }

                int i = 0;
                foreach (var item in Items)
                {
                    ++i;
                    var listError = ValidateItem(item, i);
                    if (listError.Any())
                    {
                        _lstErrQtkcbBHXH.AddRange(listError);
                        item.ImportStatus = false;

                        if (listError.Any(x => x.IsErrorMLNS))
                        {
                            item.IsError = true;
                        }
                    }
                    else
                    {
                        if (!_lstErrQtkcbBHXH.Any(x => x.Row == i - 1))
                            item.ImportStatus = true;
                    }

                    if (CbxQuarterSelected.ValueItem == yearOfWork.ToString())
                    {
                        var maCoSoYTe = item.STenCSYT.Split("-").Length > 0 ? item.STenCSYT.Split("-").FirstOrDefault().Trim() : "";
                        var listChungTuMap = ListChungTuAllLQuy.Where(x => x.SCoSoYTe == maCoSoYTe && item.SLNS == x.SDslns).ToList();
                        item.FQuyetToan4Quy = listChungTuMap.Count > 0 ? listChungTuMap.Sum(x => x.FQuyetToanQuyNay).ToString() : "";
                    }

                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(QtCapKinhPhiKcbImportModel.SXauNoiMa))
                        {
                            QtCapKinhPhiKcbImportModel item = (QtCapKinhPhiKcbImportModel)sender;
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

        private List<ImportErrorItem> ValidateItem(QtCapKinhPhiKcbImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                var lstXauNoiMaMlns = _bhDmMucLucNganSachService.GetListBhytMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.KCB_BHYT_CPBS)
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

                if (!string.IsNullOrEmpty(item.STenCSYT))
                {
                    if (!item.STenCSYT.Contains("-"))
                    {
                        errors.Add(new ImportErrorItem()
                        {
                            ColumnName = "Tên cơ sở Y tế",
                            Error = string.Format(Resources.ErrorFormatCSYT, "nội dung"),
                            Row = rowIndex - 1
                        });

                    }
                    else
                    {
                        var maCSYT = item.STenCSYT.Substring(0, item.STenCSYT.IndexOf('-')).Trim();
                        var existCSYT = _cSYTService.ExistCSYT(maCSYT, _sessionInfo.YearOfWork);
                        if (!existCSYT)
                        {
                            errors.Add(new ImportErrorItem()
                            {
                                ColumnName = "Tên cơ sở Y tế",
                                Error = string.Format(Resources.ErrorCSYT, "nội dung"),
                                Row = rowIndex - 1
                            });
                        }
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
        private void ShowError(object param)
        {
            var importTabIndex = (ImportTabIndex)((int)param);
            switch (importTabIndex)
            {
                case ImportTabIndex.Data:
                    int rowIndex = Items.IndexOf(SelectedItem);
                    List<string> errors = _lstErrQtkcbBHXH.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                    string message = string.Join(Environment.NewLine, errors);
                    System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case ImportTabIndex.MLNS:
                    break;
            }

        }

        private void OnSaveData()
        {
            if (CbxExpenseTypeSelected == null)
            {
                MessageBoxHelper.Error(Resources.AlertExpenseTypeEmpty);
                return;
            }

            if (CbxQuarterSelected == null)
            {
                MessageBoxHelper.Error(Resources.AlertQuartyEmpty);
                return;
            }

            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var NamLamViec = _sessionInfo.YearOfWork;
            string sLNSImport = string.Join(",", Items.Where(y => !string.IsNullOrEmpty(y.STenCSYT)).Select(x => x.SXauNoiMa).Distinct().ToList());
            BhQtCapKinhPhiKcb chungTu = new BhQtCapKinhPhiKcb();
            chungTu.SSoChungTu = SSoChungTu;
            chungTu.DNgayChungTu = NgayChungTu;
            chungTu.INamLamViec = NamLamViec;
            chungTu.IQuy = int.Parse(CbxQuarterSelected.ValueItem);
            chungTu.SQuyNamMoTa = CbxQuarterSelected == null ? string.Empty : CbxQuarterSelected.DisplayItem;
            chungTu.SMoTa = MoTa;
            chungTu.DNgayTao = NgayChungTu;
            chungTu.SNguoiTao = _sessionInfo.Principal;
            chungTu.SDslns = sLNSImport;
            chungTu.ILoaiKinhPhi = int.Parse(CbxExpenseTypeSelected.ValueItem);

            _chungTuService.Add(chungTu);
            var lstXauNoiMaMlns = _bhDmMucLucNganSachService.GetListBhytMucLucNs(_sessionInfo.YearOfWork, int.Parse(CbxExpenseTypeSelected.ValueItem) == 1 ?  BhxhMLNS.KinhPhi_KCB_BHYT_QN : BhxhMLNS.KinhPhi_KCB_BHYT_TNQN_NLD ).ToList();
            List<string> lstStrCSYT = new List<string>();
            List<BhQtCapKinhPhiKcbChiTiet> chungTuChiTiets = new List<BhQtCapKinhPhiKcbChiTiet>();
            List<QtCapKinhPhiKcbImportModel> listDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasData
            && lstXauNoiMaMlns.Any(y => !y.BHangCha && y.SMoTa == x.STenMLNS) && !string.IsNullOrEmpty(x.STenCSYT)).ToList();

            if (listDetailImport.Count > 0)
            {
                foreach (var item in listDetailImport)
                {
                    BhDmMucLucNganSach mucLuc = lstXauNoiMaMlns.FirstOrDefault(x => x.INamLamViec == NamLamViec && x.ITrangThai == StatusType.ACTIVE
                    && item.SXauNoiMa == x.SXauNoiMa);
                    if (mucLuc == null)
                    {
                        continue;
                    }
                    var itemMaCSYT = item.STenCSYT.Substring(0, item.STenCSYT.IndexOf('-')).Trim();
                    var itemCSYT = _cSYTService.GetCSYTByMa(itemMaCSYT, NamLamViec);
                    BhQtCapKinhPhiKcbChiTiet bhDttChiTiet = new BhQtCapKinhPhiKcbChiTiet();
                    bhDttChiTiet.IIDCTCapKinhPhiKCB = chungTu.Id;
                    bhDttChiTiet.IIdMlns = mucLuc.IIDMLNS;
                    bhDttChiTiet.IIdMlnsCha = mucLuc.IIDMLNSCha.GetValueOrDefault();
                    bhDttChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    bhDttChiTiet.SLns = item.SLNS;
                    bhDttChiTiet.IIDCoSoYTe = itemCSYT.Id;
                    bhDttChiTiet.sMaCoSoYTe = itemCSYT.IIDMaCoSoYTe;
                    bhDttChiTiet.STenCoSoYTe = itemCSYT.STenCoSoYTe;
                    bhDttChiTiet.SGhiChu = item.SGhiChu;
                    lstStrCSYT.Add(itemCSYT.IIDMaCoSoYTe);

                    bhDttChiTiet.FKeHoachCap = ConvertStringToNumber<double>(item.FKeHoachCap);
                    bhDttChiTiet.FDaQuyetToan = ConvertStringToNumber<double>(item.FDaQuyetToan);
                    bhDttChiTiet.FConLai = bhDttChiTiet.FKeHoachCap - bhDttChiTiet.FQuyetToanQuyNay;
                    bhDttChiTiet.FQuyetToanQuyNay = ConvertStringToNumber<double>(item.FQuyetToanQuyNay);

                    chungTuChiTiets.Add(bhDttChiTiet);
                }
                string lstCoSoYTe = string.Join(",", lstStrCSYT.Distinct());
                _chungTuChiTietService.AddRange(chungTuChiTiets);
                if (chungTuChiTiets.Count > 0)
                {
                    chungTu.FKeHoachCap = chungTuChiTiets.Sum(item => item.FKeHoachCap);
                    chungTu.FDaQuyetToan = chungTuChiTiets.Sum(item => item.FDaQuyetToan);
                    chungTu.FConLai = chungTuChiTiets.Sum(item => item.FConLai);
                    chungTu.FQuyetToanQuyNay = chungTuChiTiets.Sum(item => item.FQuyetToanQuyNay);
                    chungTu.SCoSoYTe = lstCoSoYTe;
                    _chungTuService.Update(chungTu);
                }

                BhQtCapKinhPhiKcbModel dttBhxh = _mapper.Map<BhQtCapKinhPhiKcbModel>(chungTu);
                DialogHost.CloseDialogCommand.Execute(dttBhxh, null);
                SavedAction?.Invoke(dttBhxh);
            }
            else
            {
                System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
        private void LoadQuarters()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q1], ValueItem = ((int)QuarterEnum.Q1).ToString()},
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q2], ValueItem = ((int)QuarterEnum.Q2).ToString()},
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q3], ValueItem = ((int)QuarterEnum.Q3).ToString()},
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q4], ValueItem = ((int)QuarterEnum.Q4).ToString()},
                new ComboboxItem {DisplayItem = "Năm " + yearOfWork.ToString(), ValueItem = yearOfWork.ToString()}
            };

            CbxQuarter = new ObservableCollection<ComboboxItem>(cbxVoucher);
            CbxQuarterSelected = CbxQuarter.First();
        }

        private void LoadExpenseTypes()
        {
            var cbxExpenseType = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = ExpenseTypes.ExpenseTypeName[ExpenseTypeEnum.KCB_QN], ValueItem = ((int)ExpenseTypeEnum.KCB_QN).ToString()},
                new ComboboxItem {DisplayItem = ExpenseTypes.ExpenseTypeName[ExpenseTypeEnum.KCB_TNQN_NLD], ValueItem = ((int)ExpenseTypeEnum.KCB_TNQN_NLD).ToString()}
            };

            CbxExpenseType = new ObservableCollection<ComboboxItem>(cbxExpenseType);
            CbxExpenseTypeSelected = CbxExpenseType.First();
        }

        private void LoadData()
        {
            var soChungTuIndex = _chungTuService.GetVoucherIndex(_sessionInfo.YearOfWork);
            SSoChungTu = "QTC-" + soChungTuIndex.ToString("D3");
        }
    }
}
