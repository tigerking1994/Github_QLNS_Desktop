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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.ImportExcel;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.ImportExcel
{
    public class QuyetToanThuImportViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IQttBHXHService _chungTuService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IQttBHXHChiTietService _chungTuChiTietService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        private List<ImportErrorItem> _lstErrQttBHXH = new List<ImportErrorItem>();
        public override string Name => "QT thu BHXH, BHYT, BHTN";
        public override Type ContentType => typeof(QuyetToanThuImport);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private Dictionary<Guid, BhQttBHXHChiTiet> _dicChungTuChiTiet;
        public string SSoChungTu { get; set; }
        public DateTime NgayChungTu { get; set; }
        public int INamChungTu { get; set; }
        private QuyetToanThuImportModel _selectedItem;
        public QuyetToanThuImportModel SelectedItem
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
                _isSaveData = (Items.Any() && !_lstErrQttBHXH.Any());
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
            set
            {
                SetProperty(ref _quarterMonthSelected, value);
                LoadAgencies();
            }
                
        }
        private ObservableCollection<QuyetToanThuImportModel> _items;
        public ObservableCollection<QuyetToanThuImportModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
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

        public QuyetToanThuImportViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            IQttBHXHService qttBHXHService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog logger,
            IQttBHXHChiTietService qttBHXHChiTietService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _chungTuService = qttBHXHService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _chungTuChiTietService = qttBHXHChiTietService;
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
            LoadQuarterMonths();
            OnResetData();
            INamChungTu = _sessionInfo.YearOfWork;
            NgayChungTu = DateTime.Now;
        }

        private void OnResetData()
        {
            _filePath = string.Empty;
            _items = new ObservableCollection<Model.Import.QuyetToanThuImportModel>();
            _lstErrQttBHXH.Clear();
            _tabIndex = ImportTabIndex.Data;
            QuarterMonthSelected = null;
            _agencySelected = null;
            var predicateMLNS = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicateMLNS = predicateMLNS.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicateMLNS).Where(x => x.SLNS.StartsWith(BhxhMLNS.KHT_BHXH_BHYT_BHTN)).OrderBy(x => x.SXauNoiMa).ToList();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));
            _existedMlns.RemoveAt(0);
            OnPropertyChanged(nameof(AgencySelected));
            OnPropertyChanged(nameof(TabIndex));
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsSelectedFile));
        }

        private void LoadAgencies()
        {
            int selectedType = 0;
            if (QuarterMonthSelected != null)
            {
                selectedType = QuarterMonthSelected.DisplayItem.Contains("Tháng") ? 0 : QuarterMonthSelected.DisplayItem.Contains("Quý") ? 1 : 2;
            }    
            
            int yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            var lstCurrentUnit = _chungTuService.FindCurrentUnits(yearOfWork, Convert.ToInt32(QuarterMonthSelected?.ValueItem ?? "0"), selectedType, false);
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai != LoaiDonVi.ROOT && !lstCurrentUnit.Contains(x.IIDMaDonVi));
            List<DonVi> listDonVi = _donViService.FindByCondition(predicate).ToList();
            Agencies = new List<ComboboxItem>();
            Agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
        }

        private void LoadQuarterMonths()
        {
            _quarterMonths = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem("Tháng " + i, i.ToString());
                _quarterMonths.Add(month);
            }
            _quarterMonths.Add(new ComboboxItem("Quý I", "3"));
            _quarterMonths.Add(new ComboboxItem("Quý II", "6"));
            _quarterMonths.Add(new ComboboxItem("Quý III", "9"));
            _quarterMonths.Add(new ComboboxItem("Quý IV", "12"));
            _quarterMonths.Add(new ComboboxItem("Năm " + _sessionInfo.YearOfWork, _sessionInfo.YearOfWork.ToString()));
            QuarterMonthSelected = _quarterMonths.First();
        }

        private void OnDownloadTemplate()
        {
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTT_IMPORT_TEMPLATE, ExportFileName.RPT_BH_QTT_BHXH_CHUNGTU_CHITIET);
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
                _lstErrQttBHXH.Clear();
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
                _lstErrQttBHXH.Clear();
            }
            _dicChungTuChiTiet = new Dictionary<Guid, BhQttBHXHChiTiet>();
            try
            {
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;

                var predicateMLNS = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicateMLNS = predicateMLNS.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicateMLNS).Where(x => x.SLNS.StartsWith(BhxhMLNS.KHT_BHXH_BHYT_BHTN)).OrderBy(x => x.SXauNoiMa).ToList();
                var dataImport = _importService.ProcessData<QuyetToanThuImportModel>(FilePath);
                var qttImportModels = new ObservableCollection<QuyetToanThuImportModel>(dataImport.Data.Where(x => x.SXauNoiMa != "XNM"));

                if (!Items.Any())
                    Items = qttImportModels;

                List<string> lstError = new List<string>();

                if (dataImport.ImportErrors.Count > 0)
                {
                    _lstErrQttBHXH.AddRange(dataImport.ImportErrors);
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
                    if (!string.IsNullOrEmpty(item.STenMLNS) && item.STenMLNS.Trim().Equals("TỔNG CỘNG"))
                    {
                        item.IsHangCha = true;
                        continue;
                    }
                    item.IsHangCha = _mucLucNganSachs.FirstOrDefault(x => x.SXauNoiMa == item.SXauNoiMa)?.BHangCha ?? false;
                    item.FTyLeBHXHNSD = _mucLucNganSachs.FirstOrDefault(x => x.SXauNoiMa == item.SXauNoiMa)?.FTyLeBHXHNSD;
                    item.FTyLeBHXHNLD = _mucLucNganSachs.FirstOrDefault(x => x.SXauNoiMa == item.SXauNoiMa)?.FTyLeBHXHNLD;
                    item.FTyLeBHYTNSD = _mucLucNganSachs.FirstOrDefault(x => x.SXauNoiMa == item.SXauNoiMa)?.FTyLeBHYTNSD;
                    item.FTyLeBHYTNLD = _mucLucNganSachs.FirstOrDefault(x => x.SXauNoiMa == item.SXauNoiMa)?.FTyLeBHYTNLD;
                    item.FTyLeBHTNNSD = _mucLucNganSachs.FirstOrDefault(x => x.SXauNoiMa == item.SXauNoiMa)?.FTyLeBHTNNSD;
                    item.FTyLeBHTNNLD = _mucLucNganSachs.FirstOrDefault(x => x.SXauNoiMa == item.SXauNoiMa)?.FTyLeBHTNNLD;
                    ++i;
                    var listError = ValidateItem(item, i);
                    if (listError.Any())
                    {
                        _lstErrQttBHXH.AddRange(listError);
                        item.ImportStatus = false;

                        if (listError.Any(x => x.IsErrorMLNS))
                        {
                            item.IsError = true;
                        }
                    }
                    else
                    {
                        if (!_lstErrQttBHXH.Any(x => x.Row == i - 1))
                            item.ImportStatus = true;
                    }

                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(QuyetToanThuImportModel.SXauNoiMa))
                        {
                            QuyetToanThuImportModel item = (QuyetToanThuImportModel)sender;
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

        private List<ImportErrorItem> ValidateItem(QuyetToanThuImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                var lstXauNoiMaMlns = _bhDmMucLucNganSachService.GetListBhytMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.KHT_BHXH_BHYT_BHTN)
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
                    int rowIndex = Items.IndexOf(SelectedItem);
                    List<string> errors = _lstErrQttBHXH.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                    string message = string.Join(Environment.NewLine, errors);
                    System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case ImportTabIndex.MLNS:
                    break;
            }

        }


        private void OnSaveData()
        {
            if (_quarterMonthSelected == null)
            {
                MessageBoxHelper.Error(Resources.ErrorQuarterEmpty);
                return;
            }

            if (_agencySelected == null)
            {
                MessageBoxHelper.Error(Resources.ErrorAgencyEmpty);
                return;
            }

            var lstXauNoiMaMlns = _bhDmMucLucNganSachService.GetListBhytMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.KHT_BHXH_BHYT_BHTN).ToList();
            List<BhQttBHXHChiTiet> lstChungTuChiTiet = new List<BhQttBHXHChiTiet>();
            List<QuyetToanThuImportModel> listDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasData && !x.IsHangCha
            && lstXauNoiMaMlns.Any(y => !y.BHangCha && y.SXauNoiMa == x.SXauNoiMa)).ToList();
            //kiểm tra tồn tại chứng từ theo đơn vị, tháng, LNS
            if (QuarterMonthSelected.DisplayItem.StartsWith("Quý"))
            {
                List<string> listExistVoucher = _chungTuService.FindLNSExist(
                new BhQttBHXHChiTietCriteria
                {
                    INamLamViec = _sessionInfo.YearOfWork,
                    IQuyNam = Convert.ToInt32(QuarterMonthSelected.ValueItem),
                    IQuyNamLoai = 1,
                    IIDMaDonVi = _agencySelected.ValueItem
                }
                , Guid.Empty, listDetailImport.Select(x => x.SXauNoiMa.Substring(0, 7)).Distinct().ToList());
                if (listExistVoucher.Count > 0)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.AlertExistSettlementQuarterVoucher, _agencySelected.DisplayItem, QuarterMonthSelected.DisplayItem, string.Join(",", listExistVoucher)));
                    return;
                }
            }
            else if (QuarterMonthSelected.DisplayItem.StartsWith("Năm"))
            {
                List<string> listExistVoucher = _chungTuService.FindLNSExist(
                new BhQttBHXHChiTietCriteria
                {
                    INamLamViec = _sessionInfo.YearOfWork,
                    IQuyNam = Convert.ToInt32(QuarterMonthSelected.ValueItem),
                    IQuyNamLoai = 2,
                    IIDMaDonVi = _agencySelected.ValueItem
                }
                , Guid.Empty, listDetailImport.Select(x => x.SXauNoiMa.Substring(0, 7)).Distinct().ToList());
                if (listExistVoucher.Count > 0)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.AlertExistSettlementMonthVoucher, _agencySelected.DisplayItem, QuarterMonthSelected.DisplayItem, string.Join(",", listExistVoucher)));
                    return;
                }
            }
            else if (QuarterMonthSelected.DisplayItem.StartsWith("Tháng"))
            {
                List<string> listExistVoucher = _chungTuService.FindLNSExist(
                new BhQttBHXHChiTietCriteria
                {
                    INamLamViec = _sessionInfo.YearOfWork,
                    IQuyNam = Convert.ToInt32(QuarterMonthSelected.ValueItem),
                    IQuyNamLoai = 0,
                    IIDMaDonVi = _agencySelected.ValueItem
                }
                , Guid.Empty, listDetailImport.Select(x => x.SXauNoiMa.Substring(0, 7)).Distinct().ToList());
                if (listExistVoucher.Count > 0)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.AlertExistSettlementMonthVoucher, _agencySelected.DisplayItem, QuarterMonthSelected.DisplayItem, string.Join(",", listExistVoucher)));
                    return;
                }
            }
            var NamLamViec = _sessionInfo.YearOfWork;
            string sLNSImport = string.Join(",", Items.Select(x => x.SXauNoiMa.Length > 7 ? x.SXauNoiMa.Substring(0, 7) : x.SXauNoiMa).ToList().Distinct());
            BhQttBHXH chungTu = new BhQttBHXH();
            chungTu.SSoChungTu = SSoChungTu;
            chungTu.DNgayChungTu = NgayChungTu;
            chungTu.BDaTongHop = false;
            chungTu.SMoTa = MoTa;
            chungTu.INamLamViec = NamLamViec;
            chungTu.DNgayTao = NgayChungTu;
            chungTu.SNguoiTao = _sessionInfo.Principal;
            chungTu.ILoaiTongHop = 1;
            chungTu.SDsMlns = sLNSImport;
            chungTu.IQuyNam = int.Parse(QuarterMonthSelected.ValueItem);
            chungTu.IQuyNamLoai = QuarterMonthSelected.DisplayItem.Contains("Tháng") ? 0 : QuarterMonthSelected.DisplayItem.Contains("Quý") ? 1 : 2;
            chungTu.IIDMaDonVi = _agencySelected.ValueItem;
            chungTu.SQuyNamMoTa = QuarterMonthSelected == null ? string.Empty : QuarterMonthSelected.DisplayItem;

            _chungTuService.Add(chungTu);
            
            List<string> lstStrCSYT = new List<string>();

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
                    BhQttBHXHChiTiet chungTuChiTiet = new BhQttBHXHChiTiet();
                    chungTuChiTiet.QttBHXHId = chungTu.Id;
                    chungTuChiTiet.IIDMLNS = mucLuc.IIDMLNS;
                    chungTuChiTiet.IIDMLNSCha = mucLuc.IIDMLNSCha;
                    chungTuChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    chungTuChiTiet.SLns = mucLuc.SLNS;
                    chungTuChiTiet.INamLamViec = NamLamViec;
                    chungTuChiTiet.IQSBQNam = ConvertStringToNumber<int>(item.IQSBQNam.Replace(".", ""));
                    chungTuChiTiet.FLuongChinh = ConvertStringToNumber<double>(item.FLuongChinh);
                    chungTuChiTiet.FPhuCapChucVu = ConvertStringToNumber<double>(item.FPhuCapChucVu);
                    chungTuChiTiet.FPCTNNghe = ConvertStringToNumber<double>(item.FPCTNNghe);
                    chungTuChiTiet.FPCTNVuotKhung = ConvertStringToNumber<double>(item.FPCTNVuotKhung);
                    chungTuChiTiet.FNghiOm = ConvertStringToNumber<double>(item.FNghiOm);
                    chungTuChiTiet.FHSBL = ConvertStringToNumber<double>(item.FHSBL);
                    chungTuChiTiet.FTongQuyTienLuongNam = item.FTongQuyTienLuongNam;
                    //chungTuChiTiet.FDuToan = ConvertStringToNumber<double>(item.FDuToan);
                    //chungTuChiTiet.FDaQuyetToan = ConvertStringToNumber<double>(item.FDaQuyetToan);
                    //chungTuChiTiet.FConLai = chungTuChiTiet.FDuToan - chungTuChiTiet.FDaQuyetToan;
                    chungTuChiTiet.FThuBHXHNLD = item.FThuBHXHNLD;
                    chungTuChiTiet.FThuBHXHNSD = item.FThuBHXHNSD;
                    chungTuChiTiet.FTongSoPhaiThuBHXH = item.FTongSoPhaiThuBHXH;
                    chungTuChiTiet.FThuBHYTNLD = item.FThuBHYTNLD;
                    chungTuChiTiet.FThuBHYTNSD = item.FThuBHYTNSD;
                    chungTuChiTiet.FTongSoPhaiThuBHYT = item.FTongSoPhaiThuBHYT;
                    chungTuChiTiet.FThuBHTNNLD = (item.FThuBHTNNLD);
                    chungTuChiTiet.FThuBHTNNSD = (item.FThuBHTNNSD);
                    chungTuChiTiet.FTongSoPhaiThuBHTN = item.FTongSoPhaiThuBHTN;
                    chungTuChiTiet.FTongCong = item.FTongCong;
                    chungTuChiTiet.SGhiChu = item.SGhiChu;
                    chungTuChiTiet.IIDMaDonVi = _agencySelected.ValueItem;
                    lstChungTuChiTiet.Add(chungTuChiTiet);
                }
                _chungTuChiTietService.AddRange(lstChungTuChiTiet);
                if (lstChungTuChiTiet.Count > 0)
                {
                    chungTu.IQSBQNam = lstChungTuChiTiet.Sum(item => item.IQSBQNam);
                    chungTu.FLuongChinh = lstChungTuChiTiet.Sum(item => item.FLuongChinh);
                    chungTu.FPhuCapChucVu = lstChungTuChiTiet.Sum(item => item.FPhuCapChucVu);
                    chungTu.FPCTNNghe = lstChungTuChiTiet.Sum(item => item.FPCTNNghe);

                    chungTu.FPCTNVuotKhung = lstChungTuChiTiet.Sum(item => item.FPCTNVuotKhung);
                    chungTu.FNghiOm = lstChungTuChiTiet.Sum(item => item.FNghiOm);
                    chungTu.FHSBL = lstChungTuChiTiet.Sum(item => item.FHSBL);
                    chungTu.FTongQuyTienLuongNam = lstChungTuChiTiet.Sum(item => item.FTongQuyTienLuongNam);
                    chungTu.FDuToan = lstChungTuChiTiet.Sum(item => item.FDuToan);
                    chungTu.FDaQuyetToan = lstChungTuChiTiet.Sum(item => item.FDaQuyetToan);
                    chungTu.FConLai = lstChungTuChiTiet.Sum(item => item.FConLai);
                    chungTu.FThuBHXHNLD = lstChungTuChiTiet.Sum(item => item.FThuBHXHNLD);
                    chungTu.FThuBHXHNSD = lstChungTuChiTiet.Sum(item => item.FThuBHXHNSD);
                    chungTu.FTongSoPhaiThuBHXH = lstChungTuChiTiet.Sum(item => item.FTongSoPhaiThuBHXH);
                    chungTu.FThuBHYTNLD = lstChungTuChiTiet.Sum(item => item.FThuBHYTNLD);
                    chungTu.FThuBHYTNSD = lstChungTuChiTiet.Sum(item => item.FThuBHYTNSD);
                    chungTu.FTongSoPhaiThuBHYT = lstChungTuChiTiet.Sum(item => item.FTongSoPhaiThuBHYT);
                    chungTu.FThuBHTNNLD = lstChungTuChiTiet.Sum(item => item.FThuBHTNNLD);
                    chungTu.FThuBHTNNSD = lstChungTuChiTiet.Sum(item => item.FThuBHTNNSD);
                    chungTu.FTongSoPhaiThuBHTN = lstChungTuChiTiet.Sum(item => item.FTongSoPhaiThuBHTN);
                    chungTu.FTongCong = lstChungTuChiTiet.Sum(item => item.FTongCong);

                    _chungTuService.Update(chungTu);
                }

                BhQttBHXHModel dttBhxh = _mapper.Map<BhQttBHXHModel>(chungTu);
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

        private void LoadData()
        {
            var soChungTuIndex = _chungTuService.GetVoucherIndex(_sessionInfo.YearOfWork);
            SSoChungTu = "QTT-" + soChungTuIndex.ToString("D3");
        }
    }
}
