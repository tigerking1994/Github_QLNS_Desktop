using AutoMapper;
using FlexCel.XlsAdapter;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility.Exceptions;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu.ImportKhtBHXH
{
    public class ImportKhtBHXHViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IKhtBHXHService _khtBHXHService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IKhtBHXHChiTietService _khtBHXHChiTietService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private List<ImportErrorItem> _lstErrKhtBHXH = new List<ImportErrorItem>();
        public override string Name => "Kế hoạch thu BHXH, BHYT, BHTN";
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsurancePlan.KeHoachThu.ImportKhtBHXH.ImportKhtBHXH);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private Dictionary<Guid, BhKhtBHXHChiTiet> _dicBhxhChiTiet;
        public string SSoChungTu { get; set; }
        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        private ObservableCollection<BhDmMucLucNganSachModel> _existedMlns;
        public ObservableCollection<BhDmMucLucNganSachModel> ExistedMlns
        {
            get => _existedMlns;
            set => SetProperty(ref _existedMlns, value);
        }

        private List<BhDmMucLucNganSach> _nsBhxhMucLucs;
        private KhtBhxhDetailImportModel _selectedItem;
        public KhtBhxhDetailImportModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        public bool _isSelectedFile;
        public bool IsSelectedFile
        {
            get => !string.IsNullOrEmpty(_filePath);
            set => SetProperty(ref _isSelectedFile, value);
        }

        private bool _isSaveData;
        public bool IsSaveData
        {
            get
            {
                _isSaveData = (Items.Any() && !_lstErrKhtBHXH.Any());
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
            }
        }
        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }
        private ObservableCollection<KhtBhxhDetailImportModel> _items;
        public ObservableCollection<KhtBhxhDetailImportModel> Items
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

        public ImportKhtBHXHViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            IKhtBHXHService khtBHXHService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog logger,
            IKhtBHXHChiTietService khtBHXHChiTietService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _khtBHXHService = khtBHXHService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _khtBHXHChiTietService = khtBHXHChiTietService;
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
            _items = new ObservableCollection<KhtBhxhDetailImportModel>();
            _lstErrKhtBHXH.Clear();
            _tabIndex = ImportTabIndex.Data;
            var predicateMLNS = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicateMLNS = predicateMLNS.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicateMLNS).Where(x => x.SLNS.StartsWith(BhxhMLNS.KHT_BHXH_BHYT_BHTN)).OrderBy(x => x.SXauNoiMa).ToList();
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
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_KHT_IMPORT_TEMPLATE, ExportFileName.RPT_BH_KHT_CHUNGTU_CHITIET_BHXH);
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
                _lstErrKhtBHXH.Clear();
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
            var BhxhChiTiet = _khtBHXHChiTietService.FindAll(n => 1 == 1);
            
            if (BhxhChiTiet != null)
            {
                foreach (var item in BhxhChiTiet)
                {
                    if (!_lstIdBhxhChiTiet.Contains(item.Id))
                    {
                        _lstIdBhxhChiTiet.Add(item.Id);
                    }
                }
                _lstErrKhtBHXH.Clear();
            }
            _dicBhxhChiTiet = new Dictionary<Guid, BhKhtBHXHChiTiet>();
            try
            {
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;

                _nsBhxhMucLucs = _bhDmMucLucNganSachService.GetListBhMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN);
                var dataImport = _importService.ProcessData<KhtBhxhDetailImportModel>(FilePath);
                var bhxhImportModels = new ObservableCollection<KhtBhxhDetailImportModel>(dataImport.Data);

                if (!Items.Any())
                    Items = bhxhImportModels;

                List<string> lstError = new List<string>();

                if (dataImport.ImportErrors.Count > 0)
                {
                    _lstErrKhtBHXH.AddRange(dataImport.ImportErrors);
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
                        _lstErrKhtBHXH.AddRange(listError);
                        item.ImportStatus = false;

                        if (listError.Any(x => x.IsErrorMLNS))
                        {
                            item.IsError = true;
                        }
                    }
                    else
                    {
                        if (!_lstErrKhtBHXH.Any(x => x.Row == i - 1))
                            item.ImportStatus = true;
                    }

                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(KhtBhxhDetailImportModel.SXauNoiMa))
                        {
                            KhtBhxhDetailImportModel item = (KhtBhxhDetailImportModel)sender;
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
                } else
                {
                    System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    _logger.Error(ex.Message, ex);

                }
            }

        }

        private List<ImportErrorItem> ValidateItem(KhtBhxhDetailImportModel item, int rowIndex)
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
            List<string> errors = _lstErrKhtBHXH.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
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
            _nsBhxhMucLucs = _bhDmMucLucNganSachService.GetListBhMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN);
            BhKhtBHXH chungTu = new BhKhtBHXH();
            chungTu.SSoChungTu = SSoChungTu;
            chungTu.IID_MaDonVi = SelectedDonVi.ValueItem;
            chungTu.DNgayChungTu = NgayChungTu == null ? DateTime.Now : NgayChungTu;
            chungTu.SMoTa = MoTa;
            chungTu.INamLamViec = NamLamViec;
            chungTu.DNgayTao = DateTime.Now;
            chungTu.SNguoiTao = _sessionInfo.Principal;
            chungTu.ILoaiTongHop = 1;
            chungTu.BDaTongHop = false;
            _khtBHXHService.Add(chungTu);
            var lstXauNoiMaMlns = _bhDmMucLucNganSachService.GetListBhMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.KHOI_DU_TOAN, BhxhMLNS.KHOI_HACH_TOAN).ToList();
            List<BhKhtBHXHChiTiet> chungTuChiTiets = new List<BhKhtBHXHChiTiet>();
            List<KhtBhxhDetailImportModel> listDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning 
            && (x.FLuongChinh != "" || x.FPhuCapChucVu != "" || x.FPCTNNghe != "" || x.FPhuCapChucVu != "" || x.FNghiOm != "")
            && _nsBhxhMucLucs.Any(y => !y.BHangCha && y.SXauNoiMa == x.SXauNoiMa)).ToList();
            foreach (var item in listDetailImport)
            {
                BhDmMucLucNganSach mucLuc = lstXauNoiMaMlns.FirstOrDefault(x => x.INamLamViec == NamLamViec && x.ITrangThai == StatusType.ACTIVE 
                && item.SXauNoiMa == x.SXauNoiMa);
                if (mucLuc == null)
                {
                    continue;
                }
                BhKhtBHXHChiTiet bhKhtChiTiet = new BhKhtBHXHChiTiet();
                bhKhtChiTiet.KhtBHXHId = chungTu.Id;
                bhKhtChiTiet.IIDMucLucNganSach = mucLuc.IIDMLNS;
                bhKhtChiTiet.DNgayTao = DateTime.Now;
                bhKhtChiTiet.SNguoiTao = _sessionInfo.Principal;
                bhKhtChiTiet.IQSBQNam = ConvertStringToIntNull(item.IQSBQNam);
                bhKhtChiTiet.FLuongChinh = ConvertStringToNumber<double>(item.FLuongChinh);
                bhKhtChiTiet.FPhuCapChucVu = ConvertStringToNumber<double>(item.FPhuCapChucVu);
                bhKhtChiTiet.FPCTNNghe = ConvertStringToNumber<double>(item.FPCTNNghe);
                bhKhtChiTiet.FPCTNVuotKhung = ConvertStringToNumber<double>(item.FPCTNVuotKhung);
                bhKhtChiTiet.FNghiOm = ConvertStringToNumber<double>(item.FNghiOm);
                bhKhtChiTiet.FHSBL = ConvertStringToNumber<double>(item.FHSBL);
                bhKhtChiTiet.FTongQuyTienLuongNam = Math.Round(bhKhtChiTiet.FLuongChinh.GetValueOrDefault() + bhKhtChiTiet.FPhuCapChucVu.GetValueOrDefault() + bhKhtChiTiet.FPCTNNghe.GetValueOrDefault()
                    + bhKhtChiTiet.FPCTNVuotKhung.GetValueOrDefault() + bhKhtChiTiet.FNghiOm.GetValueOrDefault() + bhKhtChiTiet.FHSBL.GetValueOrDefault());
                bhKhtChiTiet.FThuBHXHNguoiLaoDong = Math.Round(bhKhtChiTiet.FTongQuyTienLuongNam.GetValueOrDefault() * mucLuc.FTyLeBHXHNLD.GetValueOrDefault());
                bhKhtChiTiet.FThuBHXHNguoiSuDungLaoDong = Math.Round(bhKhtChiTiet.FTongQuyTienLuongNam.GetValueOrDefault() * mucLuc.FTyLeBHXHNSD.GetValueOrDefault());
                bhKhtChiTiet.FTongThuBHXH = bhKhtChiTiet.FThuBHXHNguoiLaoDong.GetValueOrDefault() + bhKhtChiTiet.FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault();
                bhKhtChiTiet.FThuBHYTNguoiLaoDong = Math.Round(bhKhtChiTiet.FTongQuyTienLuongNam.GetValueOrDefault() * mucLuc.FTyLeBHYTNLD.GetValueOrDefault());
                bhKhtChiTiet.FThuBHYTNguoiSuDungLaoDong = Math.Round(bhKhtChiTiet.FTongQuyTienLuongNam.GetValueOrDefault() * mucLuc.FTyLeBHYTNSD.GetValueOrDefault());
                bhKhtChiTiet.FTongThuBHYT = bhKhtChiTiet.FThuBHYTNguoiLaoDong.GetValueOrDefault() + bhKhtChiTiet.FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault();
                bhKhtChiTiet.FThuBHTNNguoiLaoDong = Math.Round(bhKhtChiTiet.FTongQuyTienLuongNam.GetValueOrDefault() * mucLuc.FTyLeBHTNNLD.GetValueOrDefault());
                bhKhtChiTiet.FThuBHTNNguoiSuDungLaoDong = Math.Round(bhKhtChiTiet.FTongQuyTienLuongNam.GetValueOrDefault() * mucLuc.FTyLeBHTNNSD.GetValueOrDefault());
                bhKhtChiTiet.FTongThuBHTN = bhKhtChiTiet.FThuBHTNNguoiLaoDong.GetValueOrDefault() + bhKhtChiTiet.FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault();
                bhKhtChiTiet.FTongCong = bhKhtChiTiet.FTongThuBHXH.GetValueOrDefault() + bhKhtChiTiet.FTongThuBHYT.GetValueOrDefault() + bhKhtChiTiet.FTongThuBHTN.GetValueOrDefault();
                bhKhtChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                bhKhtChiTiet.SLNS = mucLuc.SLNS;
                bhKhtChiTiet.INamLamViec = NamLamViec;
                bhKhtChiTiet.IIdMaDonVi = SelectedDonVi.ValueItem;
                bhKhtChiTiet.STenDonVi = SelectedDonVi.DisplayItem;
                chungTuChiTiets.Add(bhKhtChiTiet);
            }
            _khtBHXHChiTietService.AddRange(chungTuChiTiets);
            
            if (chungTuChiTiets.Count > 0)
            {
                chungTu.IQSBQNam = chungTuChiTiets.Sum(item => item.IQSBQNam.GetValueOrDefault());
                chungTu.FLuongChinh = chungTuChiTiets.Sum(item => item.FLuongChinh.GetValueOrDefault());
                chungTu.FPhuCapChucVu = chungTuChiTiets.Sum(item => item.FPhuCapChucVu.GetValueOrDefault());
                chungTu.FPCTNNghe = chungTuChiTiets.Sum(item => item.FPCTNNghe.GetValueOrDefault());
                chungTu.FPCTNVuotKhung = chungTuChiTiets.Sum(item => item.FPCTNVuotKhung.GetValueOrDefault());
                chungTu.FNghiOm = chungTuChiTiets.Sum(item => item.FNghiOm.GetValueOrDefault());
                chungTu.FHSBL = chungTuChiTiets.Sum(item => item.FHSBL.GetValueOrDefault());
                chungTu.FTongQTLN = chungTuChiTiets.Sum(item => item.FTongQuyTienLuongNam.GetValueOrDefault());

                chungTu.FThuBHXHNLDDong = chungTuChiTiets.Sum(item => item.FThuBHXHNguoiLaoDong.GetValueOrDefault());
                chungTu.FThuBHXHNSDDong = chungTuChiTiets.Sum(item => item.FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault());
                chungTu.FThuBHXH = chungTuChiTiets.Sum(item => item.FTongThuBHXH.GetValueOrDefault());

                chungTu.FThuBHYTNLDDong = chungTuChiTiets.Sum(item => item.FThuBHYTNguoiLaoDong.GetValueOrDefault());
                chungTu.FThuBHYTNSDDong = chungTuChiTiets.Sum(item => item.FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault());
                chungTu.FTongBHYT = chungTuChiTiets.Sum(item => item.FTongThuBHYT.GetValueOrDefault());

                chungTu.FThuBHTNNLDDong = chungTuChiTiets.Sum(item => item.FThuBHTNNguoiLaoDong.GetValueOrDefault());
                chungTu.FThuBHTNNSDDong = chungTuChiTiets.Sum(item => item.FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault());
                chungTu.FThuBHTN = chungTuChiTiets.Sum(item => item.FTongThuBHTN.GetValueOrDefault());

                chungTu.FTong = chungTuChiTiets.Sum(item => item.FTongCong.GetValueOrDefault());

                _khtBHXHService.Update(chungTu);
            }

            BhKhtBHXHModel khtBhxh = _mapper.Map<BhKhtBHXHModel>(chungTu);
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            SavedAction?.Invoke(khtBhxh);

        }
        private void LoadDonVi()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            ItemsDonVi = new ObservableCollection<ComboboxItem>();
            var lstCurrentUnitBh = _khtBHXHService.FindCurrentUnits(yearOfWork);
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
            var soChungTuIndex = _khtBHXHService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
            SSoChungTu = "KHT-" + soChungTuIndex.ToString("D3");
        }
    }
}
