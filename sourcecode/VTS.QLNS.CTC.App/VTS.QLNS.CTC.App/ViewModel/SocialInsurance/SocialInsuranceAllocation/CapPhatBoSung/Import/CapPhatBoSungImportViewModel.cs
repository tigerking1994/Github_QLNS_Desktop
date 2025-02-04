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

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung.Import
{
    public class CapPhatBoSungImportViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IBhCpBsChungTuService _chungTuService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhCpBsChungTuChiTietService _chungTuChiTietService;
        private readonly IBhDmCoSoYTeService _cSYTService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        private List<ImportErrorItem> _lstErrCpbsBHXH = new List<ImportErrorItem>();
        public override string Name => "CẤP PHÁT BỔ SUNG KCB BHYT";
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung.Import.CapPhatBoSungImport);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private Dictionary<Guid, BhCpBsChungTuChiTiet> _dicChungTuChiTiet;
        public string SSoChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime NgayChungTu { get; set; }
        public DateTime NgayQuyetDinh { get; set; }
        public int INamChungTu { get; set; }
        private CapPhatBoSungBHYTImportModel _selectedItem;
        public CapPhatBoSungBHYTImportModel SelectedItem
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
                _isSaveData = (Items.Any() && !_lstErrCpbsBHXH.Any());
                return _isSaveData;
            }
            set
            {
                SetProperty(ref _isSaveData, value);
            }
        }

        public string SoCapTamUng
        {
            get
            {
                int i = 1;
                if (CbxQuarterSelected is object)
                {
                    i = int.Parse(CbxQuarterSelected.ValueItem);
                } 
                return i switch
                {
                    1 => $"Số đã cấp, thanh toán quý 4/{_sessionService.Current.YearOfWork - 1} và tạm ứng quý 1/{_sessionService.Current.YearOfWork}",
                    _ => $"Số đã cấp, thanh toán quý {i - 1}/{_sessionService.Current.YearOfWork} và tạm ứng quý {i}/{_sessionService.Current.YearOfWork}"
                };
            }
        }

        public string SoCapBoSung => $"Số cấp bổ sung quý {CbxQuarterSelected.ValueItem}/{_sessionService.Current.YearOfWork}";

        public string DaQuyetToanQuyNay => $"Số quyết toán lũy kế đến quý {CbxQuarterSelected.ValueItem}/{_sessionService.Current.YearOfWork}";

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
        private ObservableCollection<CapPhatBoSungBHYTImportModel> _items;
        public ObservableCollection<CapPhatBoSungBHYTImportModel> Items
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
                OnPropertyChanged(nameof(SoCapTamUng));
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

        public CapPhatBoSungImportViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            IBhCpBsChungTuService bhCpBsChungTuService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog logger,
            IBhCpBsChungTuChiTietService bhCpBsChungTuChiTietService,
            IBhDmCoSoYTeService bhDmCoSoYTeService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _chungTuService = bhCpBsChungTuService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _chungTuChiTietService = bhCpBsChungTuChiTietService;
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
            OnResetData();
            INamChungTu = _sessionInfo.YearOfWork;
            NgayChungTu = DateTime.Now;
            NgayQuyetDinh = DateTime.Now;
        }

        private void OnResetData()
        {
            _filePath = string.Empty;
            _items = new ObservableCollection<CapPhatBoSungBHYTImportModel>();
            _lstErrCpbsBHXH.Clear();
            _tabIndex = ImportTabIndex.Data;
            var predicateMLNS = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicateMLNS = predicateMLNS.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            _mucLucNganSachs = _bhDmMucLucNganSachService.FindByCondition(predicateMLNS).Where(x => x.SLNS.StartsWith(BhxhMLNS.KCB_BHYT_CPBS)).OrderBy(x => x.SXauNoiMa).ToList();
            _existedMlns = new ObservableCollection<BhDmMucLucNganSachModel>(_mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(_mucLucNganSachs));

            OnPropertyChanged(nameof(TabIndex));
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsSelectedFile));
        }
        private void OnDownloadTemplate()
        {
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_IMPORT_CPBS, ExportFileName.RPT_BH_CPBS_CHUNG_TU_CHITIET);
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
                _lstErrCpbsBHXH.Clear();
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
                _lstErrCpbsBHXH.Clear();
            }
            _dicChungTuChiTiet = new Dictionary<Guid, BhCpBsChungTuChiTiet>();
            try
            {
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;

                var dataImport = _importService.ProcessData<CapPhatBoSungBHYTImportModel>(FilePath);
                var bhxhImportModels = new ObservableCollection<CapPhatBoSungBHYTImportModel>(dataImport.Data.Where(x => x.SXauNoiMa.ToUpper() != "TỔNG CỘNG" && x.STenCSYT != ""));

                if (!Items.Any())
                    Items = bhxhImportModels;

                List<string> lstError = new List<string>();

                if (CbxQuarterSelected == null)
                {
                    MessageBoxHelper.Error(Resources.AlertQuartyEmpty);
                    return;
                }

                if (string.IsNullOrEmpty(SSoQuyetDinh))
                {
                    System.Windows.Forms.MessageBox.Show(Resources.AlertSoKeHoachEmpty, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dataImport.ImportErrors.Count > 0)
                {
                    _lstErrCpbsBHXH.AddRange(dataImport.ImportErrors);
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
                        _lstErrCpbsBHXH.AddRange(listError);
                        item.ImportStatus = false;

                        if (listError.Any(x => x.IsErrorMLNS))
                        {
                            item.IsError = true;
                        }
                    }
                    else
                    {
                        if (!_lstErrCpbsBHXH.Any(x => x.Row == i - 1))
                            item.ImportStatus = true;
                    }

                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(CapPhatBoSungBHYTImportModel.SXauNoiMa))
                        {
                            CapPhatBoSungBHYTImportModel item = (CapPhatBoSungBHYTImportModel)sender;
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

        private List<ImportErrorItem> ValidateItem(CapPhatBoSungBHYTImportModel item, int rowIndex)
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


                if (!string.IsNullOrEmpty(item.IIDMaCoSoYTe))
                {
                    var existCSYT = _cSYTService.ExistCSYT(item.IIDMaCoSoYTe, _sessionInfo.YearOfWork);
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

                //if (!string.IsNullOrEmpty(item.STenCSYT))
                //{
                //    if (!item.STenCSYT.Contains("-"))
                //    {
                //        errors.Add(new ImportErrorItem()
                //        {
                //            ColumnName = "Tên cơ sở Y tế",
                //            Error = string.Format(Resources.ErrorFormatCSYT, "nội dung"),
                //            Row = rowIndex - 1
                //        });

                //    }
                //    else
                //    {
                //        var maCSYT = item.STenCSYT.Substring(0, item.STenCSYT.IndexOf('-')).Trim();
                //        var existCSYT = _cSYTService.ExistCSYT(maCSYT, _sessionInfo.YearOfWork);
                //        if (!existCSYT)
                //        {
                //            errors.Add(new ImportErrorItem()
                //            {
                //                ColumnName = "Tên cơ sở Y tế",
                //                Error = string.Format(Resources.ErrorCSYT, "nội dung"),
                //                Row = rowIndex - 1
                //            });
                //        }
                //    }    
                //}
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
                    List<string> errors = _lstErrCpbsBHXH.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                    string message = string.Join(Environment.NewLine, errors);
                    System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case ImportTabIndex.MLNS:
                    break;
            }    
            
        }


        private void OnSaveData()
        {
            if (string.IsNullOrEmpty(SSoQuyetDinh))
            {
                MessageBoxHelper.Error(Resources.AlertSoKeHoachEmpty);
                return;
            }

            if (CbxQuarterSelected == null)
            {
                MessageBoxHelper.Error(Resources.AlertQuartyEmpty);
                return;
            }
            
            string sLNSImport = string.Join(",", Items.Select(x =>x.SXauNoiMa.Length > 7 ? x.SXauNoiMa.Substring(0, 7) : x.SXauNoiMa).ToList().Distinct());
            BhCpBsChungTu chungTu = new BhCpBsChungTu();
            chungTu.SSoChungTu = SSoChungTu;
            chungTu.SSoQuyetDinh = SSoQuyetDinh;
            chungTu.IQuy = int.Parse(CbxQuarterSelected.ValueItem);
            chungTu.DNgayQuyetDinh = NgayQuyetDinh;
            chungTu.DNgayChungTu = NgayChungTu;
            chungTu.BDaTongHop = false;
            chungTu.SMoTa = MoTa;
            chungTu.INamLamViec = INamChungTu;
            chungTu.DNgayTao = NgayChungTu;
            chungTu.SNguoiTao = _sessionInfo.Principal;
            chungTu.ILoaiTongHop = 1;
            chungTu.SDslns = sLNSImport;
            chungTu.IIDMaDonVi = _sessionInfo.IdDonVi;

            _chungTuService.Add(chungTu);
            var lstXauNoiMaMlns = _bhDmMucLucNganSachService.GetListBhytMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.KCB_BHYT_CPBS).ToList();
            List<string> lstStrCSYT = new List<string>();
            List<BhCpBsChungTuChiTiet> chungTuChiTiets = new List<BhCpBsChungTuChiTiet>();
            List<CapPhatBoSungBHYTImportModel> listDetailImport = Items.Where(x => x.ImportStatus && !x.IsWarning && x.IsHasData
            && lstXauNoiMaMlns.Any(y => !y.BHangCha && y.SMoTa == x.STenMLNS)).ToList();

            if (listDetailImport.Count > 0)
            {
                foreach (var item in listDetailImport)
                {
                    BhDmMucLucNganSach mucLuc = lstXauNoiMaMlns.FirstOrDefault(x => x.INamLamViec == INamChungTu 
                                && x.ITrangThai == StatusType.ACTIVE
                                && item.SXauNoiMa == x.SXauNoiMa);
                    if (mucLuc == null)
                    {
                        continue;
                    }
                    //var itemMaCSYT = item.STenCSYT.Substring(0, item.STenCSYT.IndexOf('-')).Trim();
                    var itemCSYT = _cSYTService.GetCSYTByMa(item.IIDMaCoSoYTe, INamChungTu);
                    BhCpBsChungTuChiTiet bhDttChiTiet = new BhCpBsChungTuChiTiet();
                    bhDttChiTiet.IIDCTCapPhatBS = chungTu.Id;
                    bhDttChiTiet.IIdMlns = mucLuc.IIDMLNS;
                    bhDttChiTiet.IIdMlnsCha = mucLuc.IIDMLNSCha;
                    bhDttChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    bhDttChiTiet.DNgayTao = DateTime.Now;
                    bhDttChiTiet.SNguoiTao = _sessionInfo.Principal;
                    bhDttChiTiet.SLns = mucLuc.SLNS;
                    bhDttChiTiet.IIDCoSoYTe = itemCSYT.Id;
                    bhDttChiTiet.IIDMaCoSoYTe = itemCSYT.IIDMaCoSoYTe;
                    bhDttChiTiet.SGhiChu = item.SGhiChu;
                    lstStrCSYT.Add(itemCSYT.IIDMaCoSoYTe);
                    bhDttChiTiet.INamLamViec = INamChungTu;
                    bhDttChiTiet.FDaQuyetToan = ConvertStringToNumber<double>(item.FDaQuyetToan);
                    bhDttChiTiet.FDaCapUng = ConvertStringToNumber<double>(item.FDaCapUng);
                    bhDttChiTiet.FThuaThieu = bhDttChiTiet.FDaQuyetToan - bhDttChiTiet.FDaCapUng;
                    bhDttChiTiet.FSoCapBoSung = ConvertStringToNumber<double>(item.FSoCapBoSung);
                    bhDttChiTiet.IIdMaDonVi = _sessionInfo.IdDonVi;
                    chungTuChiTiets.Add(bhDttChiTiet);
                }
                string lstCoSoYTe = string.Join(",", lstStrCSYT.Distinct());
                _chungTuChiTietService.AddRange(chungTuChiTiets);
                if (chungTuChiTiets.Count > 0)
                {
                    chungTu.FTongDaQuyetToan = chungTuChiTiets.Sum(item => item.FDaQuyetToan);
                    chungTu.FTongDaCapUng = chungTuChiTiets.Sum(item => item.FDaCapUng);
                    chungTu.FTongThuaThieu = chungTuChiTiets.Sum(item => item.FThuaThieu);
                    chungTu.FTongSoCapBoSung = chungTuChiTiets.Sum(item => item.FSoCapBoSung);
                    chungTu.SCoSoYTe = lstCoSoYTe;
                    _chungTuService.Update(chungTu);
                }

                BhCpBsChungTuModel dttBhxh = _mapper.Map<BhCpBsChungTuModel>(chungTu);
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

        private void LoadData()
        {
            var soChungTuIndex = _chungTuService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
            SSoChungTu = "CP-" + soChungTuIndex.ToString("D3");
        }
    }
}
