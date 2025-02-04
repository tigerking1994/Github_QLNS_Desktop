using AutoMapper;
using log4net;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Allocation
{
    public class AllocationImportViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private INsDonViService _donViService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private INsMucLucNganSachService _mucLucNganSachService;
        private List<NsMucLucNganSach> _mucLucNganSachs;
        private IConfiguration _configuration;
        private readonly ICpDanhMucService _cpdanhMucService;
        private IImpHistoryService _impHistoryService;
        private ImpHistory _impHistory;
        private List<ImportErrorItem> _errors;
        private List<NsMuclucNgansachModel> _mergeItems;
        private string _importFolder;

        private ICpChungTuService _chungTuService;
        private ICpChungTuChiTietService _chungTuChiTietService;
        private IImpCpChungTuChiTietService _impChungTuChiTietService;
        private readonly ILog _logger;
        private readonly FtpStorageService _ftpStorageService;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        private List<AllocationImportModel> _importDataDetailProcess;

        public override string Name => "Cấp phát";
        public override Type ContentType => typeof(View.Budget.Allocation.AllocationImport);
        public override string Description => "IMPORT CẤP PHÁT";
        public bool IsEnableSaveMLNS => _mergeItems.Count > 0;
        public bool IsEnabledMergeBtn
        {
            get => ImportedMlns.Where(i => i.IsSelected).Count() > 0 && ExistedMlns.Where(i => i.IsSelected).Count() == 1;
        }

        public bool IsEnabledUnmergeCommand
        {
            get => ExistedMlns.Where(i => i.IsModified && i.IsSelected).Count() > 0;
        }
        public bool IsAllocationReceive { get; set; } = false;
        public bool IsSaveData
        {
            get
            {
                if (DataImport != null && DataImport.Count > 0)
                    return !DataImport.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private ObservableCollection<ComboboxItem> _dataDonVi = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> DataDonVi
        {
            get => _dataDonVi;
            set => SetProperty(ref _dataDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private ObservableCollection<AllocationImportModel> _dataImport;
        public ObservableCollection<AllocationImportModel> DataImport
        {
            get => _dataImport;
            set => SetProperty(ref _dataImport, value);
        }

        private AllocationImportModel _selectedItem;
        public AllocationImportModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        public bool IsEnableButtonDataShow => TabIndex != ImportTabIndex.Data;

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
            }
        }

        private ObservableCollection<ComboboxItem> _dataLoaiCap;
        public ObservableCollection<ComboboxItem> DataLoaiCap
        {
            get => _dataLoaiCap;
            set => SetProperty(ref _dataLoaiCap, value);
        }

        private ComboboxItem _selectedLoaiCap;
        public ComboboxItem SelectedLoaiCap
        {
            get => _selectedLoaiCap;
            set => SetProperty(ref _selectedLoaiCap, value);
        }

        private ObservableCollection<NsMuclucNgansachModel> _importedMlns;
        public ObservableCollection<NsMuclucNgansachModel> ImportedMlns
        {
            get => _importedMlns;
            set => SetProperty(ref _importedMlns, value);
        }

        private ObservableCollection<NsMuclucNgansachModel> _existedMlns;
        public ObservableCollection<NsMuclucNgansachModel> ExistedMlns
        {
            get => _existedMlns;
            set => SetProperty(ref _existedMlns, value);
        }

        private DateTime? _ngayChungTu;
        public DateTime? NgayChungTu
        {
            get => _ngayChungTu;
            set => SetProperty(ref _ngayChungTu, value);
        }

        private DateTime? _ngayQuyetDinh;
        public DateTime? NgayQuyetDinh
        {
            get => _ngayQuyetDinh;
            set => SetProperty(ref _ngayQuyetDinh, value);
        }

        private ObservableCollection<ComboboxItem> _listChiTietToi;
        public ObservableCollection<ComboboxItem> ListChiTietToi
        {
            get => _listChiTietToi;
            set => SetProperty(ref _listChiTietToi, value);
        }

        private ComboboxItem _selectedChiTietToi;
        public ComboboxItem SelectedChiTietToi
        {
            get => _selectedChiTietToi;
            set => SetProperty(ref _selectedChiTietToi, value);
        }

        private NsMuclucNgansachModel _selectedParent;
        public NsMuclucNgansachModel SelectedParent
        {
            get => _selectedParent;
            set
            {
                SetProperty(ref _selectedParent, value);
                OnPropertyChanged(nameof(IsEnabledMergeBtn));
            }
        }
        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand AddMLNSCommand { get; }
        public RelayCommand MergeCommand { get; }
        public RelayCommand UnmergeCommand { get; }
        public RelayCommand SaveMLNSCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand DownloadFileFtpServer { get; }
        public RelayCommand GetFileFtpCommand { get; }

        public AllocationImportViewModel(ISessionService sessionService,
           INsDonViService donViService,
           IMapper mapper,
           IImportExcelService importService,
           IConfiguration configuration,
           IImpHistoryService impHistoryService,
           ILog logger,
           INsMucLucNganSachService mucLucNganSachService,
           ICpChungTuService chungTuService,
           FtpStorageService ftpStorageService,
           ICpDanhMucService cpdanhMucService,
           ICpChungTuChiTietService chungTuChiTietService,
           IImpCpChungTuChiTietService impChungTuChiTietService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _mucLucNganSachService = mucLucNganSachService;
            _configuration = configuration;
            _impHistoryService = impHistoryService;
            _cpdanhMucService = cpdanhMucService;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _impChungTuChiTietService = impChungTuChiTietService;
            _ftpStorageService = ftpStorageService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            AddMLNSCommand = new RelayCommand(obj => OnAddMLNS());
            MergeCommand = new RelayCommand(obj => OnMerge());
            UnmergeCommand = new RelayCommand(obj => OnUnMerge());
            SaveMLNSCommand = new RelayCommand(obj => OnSaveMLNS());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            GetFileFtpCommand = new RelayCommand(obj => OnGetFileFtpCommand());
            DownloadFileFtpServer = new RelayCommand(obj => OnDownloadFileFtpServer());
        }

        private void OnMerge()
        {
            try
            {
                if (SelectedParent == null)
                    return;
                int index = _existedMlns.ToList().FindIndex(x => x.IsSelected);
                _mergeItems = _importedMlns.Where(i => i.IsSelected && i.IsModified).ToList();
                foreach (var item in _mergeItems)
                {
                    item.MlnsIdParent = SelectedParent.MlnsId;
                    item.BHangCha = false;
                    item.ITrangThai = 1;
                    item.SNguoiTao = _sessionService.Current.Principal;
                    item.DNgayTao = DateTime.Now;
                }
                List<NsMuclucNgansachModel> nsMuclucNgansachModels = _existedMlns.ToList();
                nsMuclucNgansachModels.InsertRange(index + 1, _mergeItems);
                _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(nsMuclucNgansachModels);
                _importedMlns = new ObservableCollection<NsMuclucNgansachModel>(ImportedMlns.Where(i => !i.IsSelected || !i.IsModified));
                OnPropertyChanged(nameof(ExistedMlns));
                OnPropertyChanged(nameof(ImportedMlns));
                OnPropertyChanged(nameof(IsEnabledMergeBtn));
                OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                OnPropertyChanged(nameof(IsEnableSaveMLNS));
                OnSelectionChanged();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnUnMerge()
        {
            try
            {
                IEnumerable<NsMuclucNgansachModel> unmergeItems = _existedMlns.Where(i => i.IsSelected && i.IsModified).ToList();
                foreach (var item in unmergeItems)
                {
                    _mergeItems.Remove(item);
                }
                List<NsMuclucNgansachModel> nsMuclucNgansachModels = ImportedMlns.ToList();
                nsMuclucNgansachModels.AddRange(unmergeItems);
                _importedMlns = new ObservableCollection<NsMuclucNgansachModel>(nsMuclucNgansachModels);
                _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(_existedMlns.Where(i => !i.IsSelected || !i.IsModified));
                OnPropertyChanged(nameof(ExistedMlns));
                OnPropertyChanged(nameof(ImportedMlns));
                OnPropertyChanged(nameof(IsEnabledMergeBtn));
                OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                OnPropertyChanged(nameof(IsEnableSaveMLNS));
                OnSelectionChanged();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnAddMLNS()
        {
            TabIndex = ImportTabIndex.MLNS;
            NsMuclucNgansachModel importItem = new NsMuclucNgansachModel();
            if (ImportedMlns.Contains(importItem))
                return;

            bool isImportGroup = false;
            if (!_importedMlns.Any(x => x.XauNoiMa.Contains(SelectedItem.ConcatenateCode))
                && !_existedMlns.Any(x => x.XauNoiMa.Contains(SelectedItem.ConcatenateCode)))
            {
                List<AllocationImportModel> data = new List<AllocationImportModel>();
                data.Add(SelectedItem);
                if (SelectedItem.BHangCha)
                    data.AddRange(_importDataDetailProcess.Where(x => SelectedItem.ListConcatenateCodeChild.Contains(x.ConcatenateCode)).ToList());
                else
                    data.Add(_importDataDetailProcess.Where(x => x.ConcatenateCode == SelectedItem.ConcatenateCodeParent).FirstOrDefault());
                if (data.Count > 1)
                    isImportGroup = true;
                data = data.OrderBy(x => x.ConcatenateCode).ToList();
                foreach (var item in data)
                {
                    if (!_mucLucNganSachs.Any(x => x.XauNoiMa == item.ConcatenateCode))
                        _importedMlns.Add(new NsMuclucNgansachModel
                        {
                            MlnsId = Guid.NewGuid(),
                            Lns = item.LNS,
                            L = item.L,
                            K = item.K,
                            M = item.M,
                            TM = item.TM,
                            TTM = item.TTM,
                            NG = item.NG,
                            XauNoiMa = item.ConcatenateCode,
                            MoTa = item.MoTa,
                            NamLamViec = _sessionService.Current.YearOfWork,
                            IsModified = true,
                            BHangCha = item.BHangCha
                        });
                }
            }
            foreach (NsMuclucNgansachModel model in _importedMlns.ToList())
            {
                NsMuclucNgansachModel parent = null;
                if (isImportGroup && !model.BHangCha)
                    parent = _importedMlns.Where(x => model.XauNoiMa.Contains(x.XauNoiMa) && model.XauNoiMa != x.XauNoiMa).FirstOrDefault();
                if (parent == null)
                    parent = FindParent(model, _existedMlns);
                if (parent != null)
                {
                    int index = _existedMlns.IndexOf(parent);
                    _existedMlns.Insert(index + 1, model);
                    model.MlnsIdParent = parent.MlnsId;
                    model.BHangCha = model.BHangCha;
                    model.ITrangThai = 1;
                    model.SNguoiTao = _sessionService.Current.Principal;
                    model.DNgayTao = DateTime.Now;
                    _mergeItems.Add(model);
                    OnPropertyChanged(nameof(IsEnableSaveMLNS));
                }
            }
            _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(_existedMlns.OrderBy(n => n.XauNoiMa).ToList());
            _importedMlns = new ObservableCollection<NsMuclucNgansachModel>();
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnSelectionChanged();
        }

        private void OnSaveMLNS()
        {
            try
            {
                var result = MessageBoxHelper.Confirm(Resources.ConfirmAddMLNS);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        List<NsMucLucNganSach> listMLNS = _mapper.Map<List<NsMucLucNganSach>>(_mergeItems);
                        _mucLucNganSachService.AddRange(listMLNS);
                        _existedMlns.Where(x => x.IsModified).Select(x => { x.IsModified = false; x.IsSelected = false; return x; }).ToList();
                        _mergeItems = new List<NsMuclucNgansachModel>();
                        OnPropertyChanged(nameof(ExistedMlns));
                        OnPropertyChanged(nameof(IsEnableSaveMLNS));
                        System.Windows.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                        foreach (var item in listMLNS)
                        {
                            var importItem = _dataImport.Where(x => x.ConcatenateCode == item.XauNoiMa).FirstOrDefault();
                            var listError = _importService.ValidateItem<AllocationImportModel>(importItem, _dataImport.IndexOf(importItem));
                            if (listError.Count == 0)
                            {
                                importItem.ImportStatus = true;
                                importItem.IsErrorMLNS = false;
                                TabIndex = ImportTabIndex.Data;
                                OnPropertyChanged(nameof(IsSaveData));
                            }
                        }
                    }
                    catch
                    {
                        MessageBoxHelper.Warning(Resources.MsgSaveError);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateData()
        {
            // Reset value parrent
            foreach (var item in _importDataDetailProcess.Where(x => x.BHangCha))
            {
                if (!string.IsNullOrEmpty(item.DuToan))
                    item.DuToan = "0";
                if (!string.IsNullOrEmpty(item.DaCap))
                    item.DaCap = "0";
                if (!string.IsNullOrEmpty(item.ConLai))
                    item.ConLai = "0";
                if (!string.IsNullOrEmpty(item.CapPhat))
                    item.CapPhat = "0";
            }
            foreach (var item in _importDataDetailProcess.Where(x => !x.BHangCha))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(AllocationImportModel currentItem, AllocationImportModel selfItem)
        {
            List<AllocationImportModel> parents = new List<AllocationImportModel>();
            if (!currentItem.IsWarning)
                parents = _importDataDetailProcess.Where(x => currentItem.ConcatenateCode.Contains(x.ConcatenateCode)
                           && x.BHangCha && currentItem.ConcatenateCode != x.ConcatenateCode).OrderByDescending(x => x.LNS).ToList();
            else parents = _importDataDetailProcess.Where(x => currentItem.ConcatenateCode.Contains(x.ConcatenateCode) && currentItem.ConcatenateCode != x.ConcatenateCode).OrderByDescending(x => x.LNS).ToList();
            if (parents.Count > 0)
            {
                int columnIndexOrigin = 0;
                AllocationImportModel parent = new AllocationImportModel();
                foreach (var p in parents)
                {
                    int maxColumn = p.ConcatenateCode.Split("-").Count();
                    if (maxColumn > columnIndexOrigin)
                    {
                        columnIndexOrigin = maxColumn;
                        parent = p;
                    }
                }
                double parentValue, childValue = 0;
                if (Double.TryParse(parent.DuToan, out parentValue) && Double.TryParse(selfItem.DuToan, out childValue))
                    parent.DuToan = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.DaCap, out parentValue) && Double.TryParse(selfItem.DaCap, out childValue))
                    parent.DaCap = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.ConLai, out parentValue) && Double.TryParse(selfItem.ConLai, out childValue))
                    parent.ConLai = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.CapPhat, out parentValue) && Double.TryParse(selfItem.CapPhat, out childValue))
                    parent.CapPhat = (parentValue + childValue).ToString();
                CalculateParent(parent, selfItem);
            }
            else return;
        }

        private void OnSelectionChanged()
        {
            foreach (var i in _existedMlns)
            {
                i.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(ModelBase.IsSelected))
                    {
                        OnPropertyChanged(nameof(IsEnabledMergeBtn));
                        OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                    }
                };
            }
            foreach (var i in _importedMlns.Where(x => x.IsModified))
            {
                i.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(ModelBase.IsSelected))
                    {
                        OnPropertyChanged(nameof(IsEnabledMergeBtn));
                        OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                    }
                };
            }
        }

        public NsMuclucNgansachModel FindParent(NsMuclucNgansachModel model, IEnumerable<NsMuclucNgansachModel> ExistedMlns)
        {
            IEnumerable<NsMuclucNgansachModel> ancestors = _existedMlns.Where(i => !Guid.Empty.Equals(i.Id) && !model.XauNoiMa.Equals(i.XauNoiMa) &&
                model.XauNoiMa.StartsWith(i.XauNoiMa + "-") && model.NamLamViec == i.NamLamViec)
                .OrderByDescending(i => i.XauNoiMa.Length);
            return ancestors.FirstOrDefault();
        }

        private void ShowError()
        {
            int rowIndex = _dataImport.IndexOf(SelectedItem);
            List<string> errors = _errors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            MessageBoxHelper.Info(message);
        }

        private Expression<Func<DonVi, bool>> CreatePredicateDonVi()
        {
            var session = _sessionService.Current;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(n => n.Loai == LoaiDonVi.NOI_BO);
            return predicate;
        }

        private void LoadComboboxDonVi()
        {
            if (IsAllocationReceive)
            {
                DataDonVi = new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem(_sessionService.Current.TenDonVi, _sessionService.Current.IdDonVi)
                };
                SelectedDonVi = DataDonVi.FirstOrDefault();
            }
            else
            {
                var predicate = CreatePredicateDonVi();
                List<DonVi> listDonVi = _donViService.FindByCondition(predicate).ToList();
                var listDonViByUser = _donViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.NOI_BO).Select(x => x.IIDMaDonVi).ToList();
                listDonVi = listDonVi.Where(n => listDonViByUser.Contains(n.IIDMaDonVi)).ToList();
                DataDonVi = new ObservableCollection<ComboboxItem>();
                DataDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi);
                if (DataDonVi != null && DataDonVi.Count > 0)
                {
                    SelectedDonVi = DataDonVi.FirstOrDefault();
                }
            }

        }

        public override void Init()
        {
            try
            {
                base.Init();
                OnResetData();
                LoadComboboxDonVi();
                LoadCombobox();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadCombobox()
        {
            ListChiTietToi = new ObservableCollection<ComboboxItem>();
            ListChiTietToi.Add(new ComboboxItem { DisplayItem = NSChiTietToi.MOTA_NGANH, ValueItem = NSChiTietToi.NGANH.ToString(), HiddenValue = NSChiTietToi.DB_VALUE_NGANH });
            ListChiTietToi.Add(new ComboboxItem { DisplayItem = NSChiTietToi.MOTA_TIEU_MUC, ValueItem = NSChiTietToi.TIEU_MUC.ToString(), HiddenValue = NSChiTietToi.DB_VALUE_TIEU_MUC });
            ListChiTietToi.Add(new ComboboxItem { DisplayItem = NSChiTietToi.MOTA_MUC, ValueItem = NSChiTietToi.MUC.ToString(), HiddenValue = NSChiTietToi.DB_VALUE_MUC });
            SelectedChiTietToi = ListChiTietToi.FirstOrDefault();

            try
            {
                DataLoaiCap = new ObservableCollection<ComboboxItem>();
                int namlamviec = _sessionService.Current.YearOfWork;
                List<CpDanhMuc> cbxCapPhatData = _cpdanhMucService.FindByNamLamViec(namlamviec);
                DataLoaiCap = _mapper.Map<ObservableCollection<ComboboxItem>>(cbxCapPhatData);
                SelectedLoaiCap = DataLoaiCap.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

            //DataLoaiCap = new ObservableCollection<ComboboxItem>();
            //DataLoaiCap.Add(new ComboboxItem { ValueItem = LoaiCap.CAP_UNG.ToString(), DisplayItem = LoaiCap.CAP_UNG_DISPLAY });
            //DataLoaiCap.Add(new ComboboxItem { ValueItem = LoaiCap.CAP_HOP_THUC.ToString(), DisplayItem = LoaiCap.CAP_HOP_THUC_DISPLAY });
            //DataLoaiCap.Add(new ComboboxItem { ValueItem = LoaiCap.CAP_THANH_KHOAN.ToString(), DisplayItem = LoaiCap.CAP_THANH_KHOAN_DISPLAY });
            //DataLoaiCap.Add(new ComboboxItem { ValueItem = LoaiCap.CAP_THU.ToString(), DisplayItem = LoaiCap.CAP_THU_DISPLAY });
            //SelectedLoaiCap = DataLoaiCap.FirstOrDefault();
        }

        private void OnUploadFile()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = string.Format("Chọn file excel");
                openFileDialog.RestoreDirectory = true;
                openFileDialog.DefaultExt = ".xlsx";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                FilePath = openFileDialog.FileName;
                _fileName = openFileDialog.SafeFileName;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnResetData()
        {
            _fileName = string.Empty;
            NgayChungTu = null;
            NgayQuyetDinh = null;
            if (DataDonVi != null && DataDonVi.Count > 0)
                _selectedDonVi = DataDonVi.FirstOrDefault();
            _mergeItems = new List<NsMuclucNgansachModel>();
            _filePath = string.Empty;
            _tabIndex = ImportTabIndex.Data;
            _impHistory = new ImpHistory();
            _dataImport = new ObservableCollection<AllocationImportModel>();
            _mucLucNganSachs = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
            _importedMlns = new ObservableCollection<NsMuclucNgansachModel>();
            _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(_mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(_mucLucNganSachs));
            _errors = new List<ImportErrorItem>();
            _importFolder = _configuration.GetSection("ImportFolder").Value;
            if (!Directory.Exists(_importFolder))
                Directory.CreateDirectory(_importFolder);

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(DataImport));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void OnSaveData()
        {
            try
            {
                string message = string.Empty;
                bool checkValidate = Validate(ref message);
                if (!string.IsNullOrEmpty(message) || !checkValidate)
                {
                    MessageBoxHelper.Warning(message);
                    return;
                }

                _mucLucNganSachs = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
                List<NsCpChungTuChiTiet> chungTuChiTiets = _mapper.Map<List<NsCpChungTuChiTiet>>(_dataImport.Where(x => x.ImportStatus
                && !x.IsWarning && !x.BHangCha));
                List<ImpCpChungTuChiTiet> impChungTuChiTiets = _mapper.Map<List<ImpCpChungTuChiTiet>>(_dataImport.Where(x => x.ImportStatus
                && !x.IsWarning && !x.BHangCha));

                NsCpChungTu chungTu = new NsCpChungTu();
                int soChungTuIndex = _chungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork, _sessionService.Current.Budget, _sessionService.Current.YearOfBudget);
                chungTu.SSoChungTu = "CP-" + soChungTuIndex.ToString("D3");
                chungTu.ISoChungTuIndex = soChungTuIndex;
                chungTu.DNgayChungTu = NgayChungTu;
                chungTu.DNgayQuyetDinh = NgayQuyetDinh;
                chungTu.SDsidMaDonVi = SelectedDonVi.ValueItem;
                chungTu.SDslns = string.Join(",", _dataImport.Select(x => x.LNS).Distinct());
                chungTu.NChiTietToi = SelectedChiTietToi.HiddenValue;
                chungTu.ITypeMoTa = SelectedChiTietToi.DisplayItem;
                chungTu.INamLamViec = _sessionService.Current.YearOfWork;
                chungTu.INamNganSach = _sessionService.Current.YearOfBudget;
                if (IsAllocationReceive) chungTu.ILoai = 0; else chungTu.ILoai = 1;
                chungTu.SNguoiTao = _sessionService.Current.Principal;
                chungTu.DNgayTao = DateTime.Now;
                chungTu.IIdMaNguonNganSach = _sessionService.Current.Budget;
                chungTu.BKhoa = false;
                chungTu.IIdMaDmcapPhat = SelectedLoaiCap.ValueItem;
                chungTu.FTongTuChi = _dataImport.Where(x => !string.IsNullOrEmpty(x.CapPhat)).Sum(x => double.Parse(x.CapPhat));
                _chungTuService.Add(chungTu);

                foreach (var item in chungTuChiTiets)
                {
                    NsMucLucNganSach mlns = _mucLucNganSachs.Where(x => x.XauNoiMa == item.SXauNoiMa).FirstOrDefault();
                    item.IIdCtcapPhat = chungTu.Id;
                    item.IIdMlns = mlns == null ? Guid.Empty : mlns.MlnsId;
                    item.IIdParentCha = mlns == null ? Guid.Empty : mlns.MlnsIdParent;
                    item.BHangCha = false;
                    item.INamLamViec = _sessionService.Current.YearOfWork;
                    item.IIdMaNguonNganSach = _sessionService.Current.Budget;
                    item.INamNganSach = _sessionService.Current.YearOfBudget;
                    item.IIdMaDonVi = _selectedDonVi.ValueItem;
                    item.STenDonVi = _selectedDonVi.DisplayItem;
                    item.SNguoiTao = _sessionService.Current.Principal;
                    item.DNgayTao = DateTime.Now;
                    item.FHienVat = 0;
                }

                foreach (var item in impChungTuChiTiets)
                {
                    NsMucLucNganSach mlns = _mucLucNganSachs.Where(x => x.XauNoiMa == item.XauNoiMa).FirstOrDefault();
                    item.HienVat = 0;
                    item.IdChungTu = chungTu.Id;
                    item.MlnsId = mlns == null ? Guid.Empty : mlns.MlnsId;
                    item.MlnsIdParent = mlns == null ? Guid.Empty : mlns.MlnsIdParent;
                    item.BHangCha = false;
                    item.NamLamViec = _sessionService.Current.YearOfWork;
                    item.NguonNganSach = _sessionService.Current.Budget;
                    item.IdDonVi = _selectedDonVi.ValueItem;
                    item.TenDonVi = _selectedDonVi.DisplayItem;
                    item.UserCreator = _sessionService.Current.Principal;
                    item.DateCreated = DateTime.Now;
                }
                _chungTuChiTietService.AddRange(chungTuChiTiets);
                _impChungTuChiTietService.AddRange(impChungTuChiTiets);

                MessageBoxHelper.Info(Resources.MsgImportSuccess);
                SavedAction?.Invoke(_mapper.Map<AllocationModel>(chungTu));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool Validate(ref string message)
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                message = Resources.MsgChooseFileImport;
                return false;
            }

            if (_selectedDonVi == null)
            {
                message = string.Format(Resources.MsgInputRequire, "Đơn vị");
                return false;
            }

            if (SelectedChiTietToi == null)
            {
                message = string.Format(Resources.MsgInputRequire, "Chi tiết tới");
                return false;
            }

            if (SelectedLoaiCap == null)
            {
                message = string.Format(Resources.MsgInputRequire, "Loại cấp");
                return false;
            }

            if (NgayChungTu == null)
            {
                message = string.Format(Resources.MsgInputRequire, "Ngày chứng từ");
                return false;
            }
            return true;
        }

        private List<NsMucLucNganSach> GetChiTietToiMLNS()
        {
            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.SCPChiTietToi != null && x.XauNoiMa.Length == 7);
            List<NsMucLucNganSach> data = _mucLucNganSachService.FindByCondition(predicate).ToList();
            return data;
        }

        private void ProcessSummaryDetailData(List<AllocationImportModel> input)
        {
            List<NsMucLucNganSach> dataMLNS = GetChiTietToiMLNS();
            foreach (var mlns in dataMLNS)
            {
                switch (mlns.SCPChiTietToi)
                {
                    case "NG":
                        input.Where(n => n.LNS.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length <= 25).Select(n => { n.BHangCha = true; return n; }).ToList();
                        input.Where(n => n.LNS.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length > 25).Select(n => { n.BHangCha = false; return n; }).ToList();
                        break;
                    case "TM":
                        input.Where(n => n.LNS.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length <= 20).Select(n => { n.BHangCha = true; return n; }).ToList();
                        input.Where(n => n.LNS.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length > 20).Select(n => { n.BHangCha = false; return n; }).ToList();
                        break;
                    case "M":
                        input.Where(n => n.LNS.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length <= 15).Select(n => { n.BHangCha = true; return n; }).ToList();
                        input.Where(n => n.LNS.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length > 15).Select(n => { n.BHangCha = false; return n; }).ToList();
                        break;
                    default:
                        break;
                }
            }
            input.Where(n => n.XauNoiMa.Trim().Length <= 7).Select(n => { n.BHangCha = true; return n; }).ToList();
        }

        private void OnProcessFile(bool isCheck = false)
        {
            if (!isCheck)
            {
                string message = string.Empty;
                bool checkValidate = Validate(ref message);
                if (!string.IsNullOrEmpty(message) || !checkValidate)
                {
                    MessageBoxHelper.Warning(message);
                    return;
                }
                _errors = new List<ImportErrorItem>();
            }
            //save file to import folder
            string destFile;
            ImpHistory impHistory = new ImpHistory();
            impHistory.FileName = _fileName;
            impHistory.ServiceCode = "Cấp phát";
            impHistory.UserCreator = _sessionService.Current.Principal;
            impHistory.DateCreated = DateTime.Now;
            if (isCheck)
            {
                FilePath = FileName;
                impHistory.FilePath = Path.Combine(IOExtensions.ApplicationPath, ConstantUrlPathPhanHe.UrlFolderFile);
            }
            else
            {
                destFile = Path.Combine(_importFolder, string.Format("{0}_{1}", DateTime.Now.ToString("ddMMyyyyhhmmss"), _fileName));
                File.Copy(FilePath, destFile);
                impHistory.FilePath = _importFolder;
            }
            _impHistoryService.Add(_impHistory);
            try
            {
                ImportResult<AllocationImportModel> _voucherDetailResult = _importService.ProcessData<AllocationImportModel>(FilePath);
                _importDataDetailProcess = new List<AllocationImportModel>();
                ProcessSummaryDetailData(_voucherDetailResult.Data);

                //kiểm tra loại ngân sách con thỏa mãn điều kiện để import
                foreach (var item in _voucherDetailResult.Data)
                {
                    if (!item.ImportStatus && item.IsErrorMLNS)
                    {
                        var parents = _importDataDetailProcess.Where(x => item.ConcatenateCode.Contains(x.ConcatenateCode) && x != item).ToList();
                        if (parents.Count > 0)
                        {
                            int columnIndexOrigin = 0;
                            AllocationImportModel parent = new AllocationImportModel();
                            foreach (var p in parents)
                            {
                                int maxColumn = p.ConcatenateCode.Split("-").Count();
                                if (maxColumn > columnIndexOrigin)
                                {
                                    columnIndexOrigin = maxColumn;
                                    parent = p;
                                }
                            }
                            int columnIndexImport = item.ConcatenateCode.Split("-").Count();
                            if (columnIndexOrigin < columnIndexImport)
                            {
                                if (!string.IsNullOrEmpty(parent.DuToan))
                                    parent.DuToan = "0";
                                if (!string.IsNullOrEmpty(parent.DaCap))
                                    parent.DaCap = "0";
                                if (!string.IsNullOrEmpty(parent.ConLai))
                                    parent.ConLai = "0";
                                if (!string.IsNullOrEmpty(parent.CapPhat))
                                    parent.CapPhat = "0";
                                if (parent.ListConcatenateCodeChild == null)
                                    parent.ListConcatenateCodeChild = new List<string>();
                                parent.ListConcatenateCodeChild.Add(item.ConcatenateCode);
                                item.ConcatenateCodeParent = parent.ConcatenateCode;

                                var parentOrigin = _mucLucNganSachs.Where(x => x.XauNoiMa == parent.ConcatenateCode).FirstOrDefault();
                                if (parentOrigin != null && !parentOrigin.BHangCha)
                                {
                                    item.IsWarning = true;
                                }
                                if (parent.IsWarning)
                                    item.IsWarning = true;
                            }
                        }
                    }
                    _importDataDetailProcess.Add(item);
                }
                CalculateData();
                DataImport = new ObservableCollection<AllocationImportModel>(_importDataDetailProcess);
                foreach (var item in _dataImport)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName != nameof(AllocationImportModel.ImportStatus)
                            && args.PropertyName != nameof(AllocationImportModel.ConcatenateCode)
                            && args.PropertyName != nameof(AllocationImportModel.IsErrorMLNS))
                        {
                            var voucherDetail = (AllocationImportModel)sender;
                            int rowIndex = _dataImport.IndexOf(voucherDetail);
                            var listError = _importService.ValidateItem<AllocationImportModel>(voucherDetail, rowIndex);
                            if (listError.Count > 0)
                            {
                                List<string> errors = listError.Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                                string message = string.Join(Environment.NewLine, errors);
                                MessageBoxHelper.Info(message);
                                _errors.AddRange(listError);
                                voucherDetail.ImportStatus = false;
                                if (listError.Any(x => x.IsErrorMLNS))
                                    voucherDetail.IsErrorMLNS = true;
                            }
                            else
                            {
                                voucherDetail.ImportStatus = true;
                                voucherDetail.IsErrorMLNS = false;
                                _errors.RemoveAll(x => x.Row == rowIndex);
                                OnPropertyChanged(nameof(IsSaveData));
                            }
                        }
                    };
                }
                OnPropertyChanged(nameof(DataImport));
                if (_voucherDetailResult.ImportErrors.Count > 0)
                    _errors.AddRange(_voucherDetailResult.ImportErrors);
                if (DataImport.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    MessageBoxHelper.Warning(Resources.AlertDataError);
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                MessageBoxHelper.Warning(Resources.MsgCheckFormatFileImport);
            }
        }

        /// <summary>
        /// Lấy dữ liệu từ ftp
        /// </summary>
        private void OnGetFileFtpCommand()
        {
            if (SelectedChiTietToi == null || SelectedLoaiCap == null || SelectedDonVi == null)
            {

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng chọn dữ liệu!");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                return;
            }
            var btmTenDonVi = StringUtils.UCS2Convert(SelectedDonVi.DisplayItem).Replace("---", "-");
            var sSelectCp = StringUtils.UCS2Convert(SelectedLoaiCap.DisplayItem);
            var sSelectN = StringUtils.UCS2Convert(SelectedChiTietToi.DisplayItem);
            string sLicenseChane = string.Format("{0}/{1}", sSelectCp, sSelectN);
            var strUrl = string.Format("{0}/{1}/{2}", btmTenDonVi, ConstantUrlPathPhanHe.UrlQlnsCpformSend, sLicenseChane);
            var lstData = _ftpStorageService.GetFileServerFtp(strUrl);
            if (lstData == null || lstData.Count == 0)
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Không tìm thấy dữ liệu");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                return;
            }
            LstFile = new ObservableCollection<FileFtpModel>(lstData);
        }
        /// <summary>
        /// Download dữ liệu
        /// </summary>
        private void OnDownloadFileFtpServer()
        {
            string urlUrIDownLoad = "";
            string fileName = "";
            if (LstFile == null || LstFile.Count == 0 || !LstFile.Any(e => e.BIsCheck))
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng lấy dữ liệu");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                return;
            }
            foreach (var item in LstFile)
            {
                if (item.BIsCheck)
                {
                    urlUrIDownLoad = item.SUrl;
                    fileName = item.SNameFile;
                    string filePath = _ftpStorageService.DowLoadFileFtpGiveLocal(urlUrIDownLoad, fileName);
                    FileName = filePath;
                }
            }
            OnProcessFile(true);
        }
        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}