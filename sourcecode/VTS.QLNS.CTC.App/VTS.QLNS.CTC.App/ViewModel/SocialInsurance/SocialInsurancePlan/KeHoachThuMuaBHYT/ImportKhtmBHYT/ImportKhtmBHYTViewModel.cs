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

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT.ImportKhtmBHYT
{
    public class ImportKhtmBHYTViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IKhtmBHYTService _khtmBHYTService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IKhtmBHYTChiTietService _khtmBHYTChiTietService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private List<ImportErrorItem> _lstErrKhtmBHYT = new List<ImportErrorItem>();
        public override string Name => "Kế hoạch thu BHYT thân nhân";
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT.ImportKhtmBHYT.ImportKhtmBHYT);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private Dictionary<Guid, BhKhtmBHYTChiTiet> _dicBhytChiTiet;
        public string SSoChungTu { get; set; }
        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        private ObservableCollection<BhDmMucLucNganSachModel> _existedMlns;
        public ObservableCollection<BhDmMucLucNganSachModel> ExistedMlns
        {
            get => _existedMlns;
            set => SetProperty(ref _existedMlns, value);
        }
        private List<BhDmMucLucNganSach> _nsBhytMucLucs;
        private KhtmBhytDetailImportModel _selectedItem;
        public KhtmBhytDetailImportModel SelectedItem
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
        private bool _isValidXNM;
        public bool IsValidXNM
        {
            get => _isValidXNM;
            set => SetProperty(ref _isValidXNM, value);
        }
        private bool _isSaveData;
        public bool IsSaveData
        {
            get
            {
                _isSaveData = (Items.Any() && !_lstErrKhtmBHYT.Any());
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
            set => SetProperty(ref _selectedDonVi, value);
        }
        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }
        private ObservableCollection<KhtmBhytDetailImportModel> _items;
        public ObservableCollection<KhtmBhytDetailImportModel> Items
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

        public ImportKhtmBHYTViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            IKhtmBHYTService khtmBHYTService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog logger,
            IKhtmBHYTChiTietService khtmBHYTChiTietService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _khtmBHYTService = khtmBHYTService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _khtmBHYTChiTietService = khtmBHYTChiTietService;
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
            LoadDonVi();
            OnResetData();
            NgayChungTu = DateTime.Now;
        }

        private void OnResetData()
        {
            _filePath = string.Empty;
            _selectedDonVi = null;
            _items = new ObservableCollection<KhtmBhytDetailImportModel>();
            _lstErrKhtmBHYT.Clear();
            _tabIndex = ImportTabIndex.Data;
            var predicateMLNS = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicateMLNS = predicateMLNS.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicateMLNS).Where(x => x.SLNS.StartsWith(BhxhMLNS.THU_MUA_BHYT)).OrderBy(x => x.SXauNoiMa).ToList();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));
            if (_existedMlns.Any())
                _existedMlns.RemoveAt(0);
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(IsSelectedFile));
        }
        private void OnDownloadTemplate()
        {
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_KHTM_IMPORT_TEMPLATE, ExportFileName.IMPORT_BH_KHTM_BHYT_CHUNGTU_CHITIET);
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
                FilePath = openFileDialog.FileName;
                _lstErrKhtmBHYT.Clear();
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

            List<Guid> _lstIdBhytChiTiet = new List<Guid>();
            var bhytChiTiet = _khtmBHYTChiTietService.FindAll();

            if (bhytChiTiet != null)
            {
                foreach (var item in bhytChiTiet)
                {
                    if (!_lstIdBhytChiTiet.Contains(item.Id))
                    {
                        _lstIdBhytChiTiet.Add(item.Id);
                    }
                }
                _lstErrKhtmBHYT.Clear();
            }
            _dicBhytChiTiet = new Dictionary<Guid, BhKhtmBHYTChiTiet>();
            try
            {
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;

                _nsBhytMucLucs = _bhDmMucLucNganSachService.GetListBhytMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.THU_MUA_BHYT);
                var dataImport = _importService.ProcessData<KhtmBhytDetailImportModel>(FilePath);
                var bhytImportModels = new ObservableCollection<KhtmBhytDetailImportModel>(dataImport.Data);

                if (!Items.Any())
                    Items = bhytImportModels;

                List<string> lstError = new List<string>();

                if (dataImport.ImportErrors.Count > 0)
                {
                    _lstErrKhtmBHYT.AddRange(dataImport.ImportErrors);
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
                    item.IsHangCha = _nsBhytMucLucs.FirstOrDefault(x => x.SXauNoiMa == item.SXauNoiMa)?.BHangCha ?? false;
                    ++i;
                    var listError = ValidateItem(item, i);

                    if (listError.Any())
                    {
                        _lstErrKhtmBHYT.AddRange(listError);
                        lstError.Add(Resources.MsgXauNoiMaKhongTonTai);
                        item.ImportStatus = false;
                        IsValidXNM = false;
                        if (listError.Any(x => x.IsErrorMLNS))
                        {
                            item.IsError = true;
                        }
                    }
                    else
                    {
                        if (!_lstErrKhtmBHYT.Any(x => x.Row == i - 1))
                            item.ImportStatus = true;
                    }

                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(KhtmBhytDetailImportModel.SXauNoiMa))
                        {
                            KhtmBhytDetailImportModel item = (KhtmBhytDetailImportModel)sender;
                            HandleData();
                        }
                    };
                }
                if (Items.Any(x => !x.ImportStatus) || lstError.Any())
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

        private List<ImportErrorItem> ValidateItem(KhtmBhytDetailImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                var lstXauNoiMaMlns = _bhDmMucLucNganSachService.GetListBhytMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.THU_MUA_BHYT)
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
            List<string> errors = _lstErrKhtmBHYT.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
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
            var NamLamViec = _sessionInfo.YearOfWork;

            _nsBhytMucLucs = _bhDmMucLucNganSachService.GetListBhytMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.THU_MUA_BHYT);
            BhKhtmBHYT chungTu = new BhKhtmBHYT();
            chungTu.SSoChungTu = SSoChungTu;
            chungTu.IIDMaDonVi = SelectedDonVi.ValueItem;
            chungTu.DNgayChungTu = NgayChungTu == null ? DateTime.Now : NgayChungTu;
            chungTu.SMoTa = MoTa;
            chungTu.INamLamViec = NamLamViec;
            chungTu.DNgayTao = DateTime.Now;
            chungTu.SNguoiTao = _sessionInfo.Principal;
            chungTu.ILoaiTongHop = 1;
            chungTu.BDaTongHop = false;
            _khtmBHYTService.Add(chungTu);
            var lstXauNoiMaMlns = _bhDmMucLucNganSachService.GetListBhytMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.THU_MUA_BHYT).ToList();
            List<BhKhtmBHYTChiTiet> chungTuChiTiets = new List<BhKhtmBHYTChiTiet>();
            List<KhtmBhytDetailImportModel> listDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning
            && !string.IsNullOrEmpty(x.FThanhTien) && _nsBhytMucLucs.Any(y => !y.BHangCha && y.SXauNoiMa == x.SXauNoiMa)).ToList();
            foreach (var item in listDetailImport)
            {
                BhDmMucLucNganSach mucLuc = lstXauNoiMaMlns.FirstOrDefault(x => x.INamLamViec == NamLamViec && x.ITrangThai == StatusType.ACTIVE
                && item.SXauNoiMa == x.SXauNoiMa);
                if (mucLuc == null)
                {
                    continue;
                }
                BhKhtmBHYTChiTiet bhKhtmChiTiet = new BhKhtmBHYTChiTiet();
                bhKhtmChiTiet.IdKhtmBHYT = chungTu.Id;
                bhKhtmChiTiet.IIDNoiDung = mucLuc.IIDMLNS;
                bhKhtmChiTiet.DNgayTao = DateTime.Now;
                bhKhtmChiTiet.SNguoiTao = _sessionInfo.Principal;
                bhKhtmChiTiet.ISoNguoi = ConvertStringToIntNull(item.ISoNguoi);
                bhKhtmChiTiet.ISoThang = ConvertStringToIntNull(item.ISoThang);
                bhKhtmChiTiet.FDinhMuc = ConvertStringToNumber<double>(item.FDinhMuc);
                bhKhtmChiTiet.FThanhTien = ConvertStringToNumber<double>(item.FThanhTien);
                bhKhtmChiTiet.SGhiChu = item.SGhiChu;
                bhKhtmChiTiet.INamLamViec = NamLamViec;
                bhKhtmChiTiet.SLNS = mucLuc.SLNS;
                bhKhtmChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                bhKhtmChiTiet.STenNoiDung = mucLuc.SMoTa;
                bhKhtmChiTiet.IIDMaDonVi = SelectedDonVi.ValueItem;
                bhKhtmChiTiet.STenDonVi = SelectedDonVi.DisplayItem;
                chungTuChiTiets.Add(bhKhtmChiTiet);
            }
            _khtmBHYTChiTietService.AddRange(chungTuChiTiets);

            if (chungTuChiTiets.Count > 0)
            {
                chungTu.ITongSoNguoi = chungTuChiTiets.Sum(item => item.ISoNguoi);
                chungTu.ITongSoThang = chungTuChiTiets.Sum(item => item.ISoThang);
                chungTu.FTongDinhMuc = chungTuChiTiets.Sum(item => item.FDinhMuc);
                chungTu.FTongThanhTien = chungTuChiTiets.Sum(item => item.FThanhTien);

                _khtmBHYTService.Update(chungTu);
            }

            BhKhtmBHYTModel khtmBhyt = _mapper.Map<BhKhtmBHYTModel>(chungTu);
            DialogHost.CloseDialogCommand.Execute(khtmBhyt, null);
            SavedAction?.Invoke(khtmBhyt);

        }
        private void LoadDonVi()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            ItemsDonVi = new ObservableCollection<ComboboxItem>();
            var lstCurrentUnitBh = _khtmBHYTService.FindCurrentUnits(yearOfWork);
            IEnumerable<DonVi> listDonVi = _donViService.FindByNamLamViec(yearOfWork).Where(x => x.Loai != LoaiDonVi.ROOT && !lstCurrentUnitBh.Contains(x.IIDMaDonVi));
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
                return (int)Convert.ChangeType(input.Replace(".", ""), typeof(int));
            }
        }

        private void LoadData()
        {
            var soChungTuIndex = _khtmBHYTService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
            SSoChungTu = "KHTM-" + soChungTuIndex.ToString("D3");
        }
    }
}
