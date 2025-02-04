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
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.LevelBudget
{
    public class LevelBuggetImportViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private INsDonViService _donViService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private INsMucLucNganSachService _mucLucNganSachService;
        private ILbChungTuChiTietPhanCapService _soLieuChiTietPhanCapService;
        private List<NsMucLucNganSach> _mucLucNganSachs;
        private IConfiguration _configuration;
        private IImpHistoryService _impHistoryService;
        private ImpHistory _impHistory;
        private List<ImportErrorItem> _errors;
        private List<ImportErrorItem> _errorsChild;
        private List<NsMuclucNgansachModel> _mergeItems;
        private string _importFolder;
        private INsNguoiDungDonViService _nsNguoiDungDonViService;
        private ILbChungTuService _chungTuService;
        private ILbChungTuChiTietService _chungTuChiTietService;
        private IImpCpChungTuChiTietService _impChungTuChiTietService;
        private readonly ILog _logger;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler OpenDetail;
        private List<LevelBuggetImportModel> _importDataDetailProcess;
        private List<LevelBuggetChildImportModel> _importDataChildProcess;

        public override string Name => "Phân cấp ngân sách ngành";
        public override Type ContentType => typeof(View.Budget.DemandCheck.LevelBudget.LevelBuggetImport);
        public override string Description => "IMPORT PHÂN CẤP NGÂN SÁCH NGÀNH";
        public bool IsEnableSaveMLNS => _mergeItems.Count > 0;

        public bool IsEnabledMergeBtn
        {
            get => ImportedMlns.Where(i => i.IsSelected).Count() > 0 && ExistedMlns.Where(i => i.IsSelected).Count() == 1;
        }

        public bool IsEnabledUnmergeCommand
        {
            get => ExistedMlns.Where(i => i.IsModified && i.IsSelected).Count() > 0;
        }

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

        private ObservableCollection<ComboboxItem> _dataDonVi;
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

        private ObservableCollection<LevelBuggetImportModel> _dataImport;
        public ObservableCollection<LevelBuggetImportModel> DataImport
        {
            get => _dataImport;
            set => SetProperty(ref _dataImport, value);
        }

        private LevelBuggetImportModel _selectedItem;
        public LevelBuggetImportModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private ObservableCollection<LevelBuggetChildImportModel> _dataPhanCap;
        public ObservableCollection<LevelBuggetChildImportModel> DataPhanCap
        {
            get => _dataPhanCap;
            set => SetProperty(ref _dataPhanCap, value);
        }

        private LevelBuggetChildImportModel _selectedItemPhanCap;
        public LevelBuggetChildImportModel SelectedItemPhanCap
        {
            get => _selectedItemPhanCap;
            set => SetProperty(ref _selectedItemPhanCap, value);
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
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

        private string _soCongVan;
        public string SoCongVan
        {
            get => _soCongVan;
            set => SetProperty(ref _soCongVan, value);
        }

        private ObservableCollection<ComboboxItem> _dataNguonNganSach;
        public ObservableCollection<ComboboxItem> DataNguonNganSach
        {
            get => _dataNguonNganSach;
            set => SetProperty(ref _dataNguonNganSach, value);
        }

        private ComboboxItem _selectedNguonNganSach;
        public ComboboxItem SelectedNguonNganSach
        {
            get => _selectedNguonNganSach;
            set => SetProperty(ref _selectedNguonNganSach, value);
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

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand ShowErrorChildCommand { get; }
        public RelayCommand AddMLNSCommand { get; }
        public RelayCommand MergeCommand { get; }
        public RelayCommand UnmergeCommand { get; }
        public RelayCommand SaveMLNSCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand AddMLNSChildCommand { get; }

        public LevelBuggetImportViewModel(ISessionService sessionService,
           INsDonViService donViService,
           IMapper mapper,
           IImportExcelService importService,
           IConfiguration configuration,
           IImpHistoryService impHistoryService,
           ILog logger,
           INsMucLucNganSachService mucLucNganSachService,
           ILbChungTuChiTietPhanCapService soLieuChiTietPhanCapService,
           ILbChungTuService chungTuService,
           ILbChungTuChiTietService chungTuChiTietService,
           INsNguoiDungDonViService nsNguoiDungDonViService,
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
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _impChungTuChiTietService = impChungTuChiTietService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _soLieuChiTietPhanCapService = soLieuChiTietPhanCapService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            ShowErrorChildCommand = new RelayCommand(obj => ShowErrorChild());
            AddMLNSCommand = new RelayCommand(obj => OnAddMLNS());
            AddMLNSChildCommand = new RelayCommand(obj => OnAddMLNSChildCommand());
            MergeCommand = new RelayCommand(obj => OnMerge());
            UnmergeCommand = new RelayCommand(obj => OnUnMerge());
            SaveMLNSCommand = new RelayCommand(obj => OnSaveMLNS());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
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
                List<LevelBuggetImportModel> data = new List<LevelBuggetImportModel>();
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
                            TNG = item.TNG,
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
                    //_importedMlns.Remove(model);
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

        private void OnAddMLNSChildCommand()
        {
            TabIndex = ImportTabIndex.MLNS;
            NsMuclucNgansachModel importItem = new NsMuclucNgansachModel();
            if (ImportedMlns.Contains(importItem))
                return;

            bool isImportGroup = false;
            if (!_importedMlns.Any(x => x.XauNoiMa.Contains(SelectedItemPhanCap.ConcatenateCode))
                && !_existedMlns.Any(x => x.XauNoiMa.Contains(SelectedItemPhanCap.ConcatenateCode)))
            {
                List<LevelBuggetChildImportModel> data = new List<LevelBuggetChildImportModel>();
                data.Add(SelectedItemPhanCap);
                if (SelectedItemPhanCap.BHangCha)
                    data.AddRange(_importDataChildProcess.Where(x => SelectedItemPhanCap.ListConcatenateCodeChild.Contains(x.ConcatenateCode)).ToList());
                else
                    data.Add(_importDataChildProcess.Where(x => x.ConcatenateCode == SelectedItemPhanCap.ConcatenateCodeParent).FirstOrDefault());
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
                            TNG = item.TNG,
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
                    //_importedMlns.Remove(model);
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
                        MessageBoxHelper.Info(Resources.MsgSaveDone);

                        foreach (var item in listMLNS)
                        {
                            var importItem = _dataImport.Where(x => x.ConcatenateCode == item.XauNoiMa).FirstOrDefault();
                            if (importItem != null)
                            {
                                var listError = _importService.ValidateItem<LevelBuggetImportModel>(importItem, _dataImport.IndexOf(importItem));
                                if (listError.Count == 0)
                                {
                                    importItem.ImportStatus = true;
                                    importItem.IsErrorMLNS = false;
                                    TabIndex = ImportTabIndex.Data;
                                    OnPropertyChanged(nameof(IsSaveData));
                                }
                            }
                        }

                        foreach (var item in listMLNS)
                        {
                            var importItem = _dataPhanCap.Where(x => x.ConcatenateCode == item.XauNoiMa).FirstOrDefault();
                            if (importItem != null)
                            {
                                var listError = _importService.ValidateItem<LevelBuggetChildImportModel>(importItem, _dataPhanCap.IndexOf(importItem));
                                if (listError.Count == 0)
                                {
                                    importItem.ImportStatus = true;
                                    importItem.IsErrorMLNS = false;
                                    TabIndex = ImportTabIndex.Data;
                                    OnPropertyChanged(nameof(IsSaveData));
                                }
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
            foreach (var item in _importDataDetailProcess.Where(x => x.BHangCha))
            {
                if (!string.IsNullOrEmpty(item.TuChi))
                    item.TuChi = "0";
                if (!string.IsNullOrEmpty(item.PhanCap))
                    item.PhanCap = "0";
                if (!string.IsNullOrEmpty(item.ChuaPhanCap))
                    item.ChuaPhanCap = "0";
                if (!string.IsNullOrEmpty(item.HangNhap))
                    item.HangNhap = "0";
                if (!string.IsNullOrEmpty(item.HangMua))
                    item.HangMua = "0";
            }
            foreach (var item in _importDataDetailProcess.Where(x => !x.BHangCha))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(LevelBuggetImportModel currentItem, LevelBuggetImportModel selfItem)
        {
            List<LevelBuggetImportModel> parents = new List<LevelBuggetImportModel>();
            if (!currentItem.IsWarning)
                parents = _importDataDetailProcess.Where(x => currentItem.ConcatenateCode.Contains(x.ConcatenateCode)
                           && x.BHangCha && currentItem.ConcatenateCode != x.ConcatenateCode).OrderByDescending(x => x.LNS).ToList();
            else parents = _importDataDetailProcess.Where(x => currentItem.ConcatenateCode.Contains(x.ConcatenateCode) && currentItem.ConcatenateCode != x.ConcatenateCode).OrderByDescending(x => x.LNS).ToList();
            if (parents.Count > 0)
            {
                int columnIndexOrigin = 0;
                LevelBuggetImportModel parent = new LevelBuggetImportModel();
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
                if (Double.TryParse(parent.TuChi, out parentValue) && Double.TryParse(selfItem.TuChi, out childValue))
                    parent.TuChi = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.PhanCap, out parentValue) && Double.TryParse(selfItem.PhanCap, out childValue))
                    parent.PhanCap = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.ChuaPhanCap, out parentValue) && Double.TryParse(selfItem.ChuaPhanCap, out childValue))
                    parent.ChuaPhanCap = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.HangNhap, out parentValue) && Double.TryParse(selfItem.HangNhap, out childValue))
                    parent.HangNhap = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.HangMua, out parentValue) && Double.TryParse(selfItem.HangMua, out childValue))
                    parent.HangMua = (parentValue + childValue).ToString();
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

        private void ShowErrorChild()
        {
            int rowIndex = _dataPhanCap.IndexOf(SelectedItemPhanCap);
            List<string> errors = _errorsChild.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            MessageBoxHelper.Info(message);
        }

        private void LoadComboboxDonVi()
        {
            var predicate = CreatePredicate();
            List<DonVi> listDonVi = _donViService.FindByCondition(predicate).ToList();
            DataDonVi = new ObservableCollection<ComboboxItem>();
            DataDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi);
            if (DataDonVi != null && DataDonVi.Count > 0)
                SelectedDonVi = DataDonVi.FirstOrDefault();
        }

        private Expression<Func<DonVi, bool>> CreatePredicate()
        {
            var session = _sessionService.Current;
            var predicate = PredicateBuilder.True<DonVi>();
            List<string> nguoiDungDonVi = GetListNguoiDungDonVi();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT) && x.BCoNSNganh);
            predicate = predicate.And(x => nguoiDungDonVi != null && nguoiDungDonVi.Contains(x.IIDMaDonVi));
            return predicate;
        }

        private List<string> GetListNguoiDungDonVi()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => x.IIDMaNguoiDung == _sessionService.Current.Principal);
            List<NguoiDungDonVi> listNguoiDung = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return (listNguoiDung != null && listNguoiDung.Count > 0) ? listNguoiDung.Select(n => n.IIdMaDonVi).ToList() : new List<string>();
        }

        public override void Init()
        {
            try
            {
                base.Init();
                OnResetData();
                LoadComboboxDonVi();
                LoadNganSach();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadNganSach()
        {
            DataNguonNganSach = new ObservableCollection<ComboboxItem>();
            DataNguonNganSach.Add(new ComboboxItem { ValueItem = NguonNganSach.NSQP.ToString(), DisplayItem = NguonNganSach.TEN_NSQQ });
            DataNguonNganSach.Add(new ComboboxItem { ValueItem = NguonNganSach.NSK.ToString(), DisplayItem = NguonNganSach.TEN_NSK });
            SelectedNguonNganSach = DataNguonNganSach.FirstOrDefault();
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
            _soCongVan = string.Empty;
            NgayChungTu = null;
            if (DataDonVi != null && DataDonVi.Count > 0)
                _selectedDonVi = DataDonVi.FirstOrDefault();
            _mergeItems = new List<NsMuclucNgansachModel>();
            _filePath = string.Empty;
            _tabIndex = ImportTabIndex.Data;
            _impHistory = new ImpHistory();
            _dataImport = new ObservableCollection<LevelBuggetImportModel>();
            _dataPhanCap = new ObservableCollection<LevelBuggetChildImportModel>();
            _mucLucNganSachs = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
            _importedMlns = new ObservableCollection<NsMuclucNgansachModel>();
            _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(_mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(_mucLucNganSachs));
            _errors = new List<ImportErrorItem>();
            _errorsChild = new List<ImportErrorItem>();
            _importFolder = _configuration.GetSection("ImportFolder").Value;
            if (!Directory.Exists(_importFolder))
                Directory.CreateDirectory(_importFolder);

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(SoCongVan));
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(DataImport));
            OnPropertyChanged(nameof(DataPhanCap));
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
                List<NsNganhChungTuChiTiet> chungTuChiTiets = new List<NsNganhChungTuChiTiet>();
                foreach (LevelBuggetImportModel item in _dataImport.Where(x => x.ImportStatus && !x.IsWarning && _mucLucNganSachs.Any(y => y.XauNoiMa == x.ConcatenateCode && !y.BHangCha)))
                {
                    chungTuChiTiets.Add(new NsNganhChungTuChiTiet
                    {
                        SLns = item.LNS,
                        SL = item.L,
                        SK = item.K,
                        SM = item.M,
                        STm = item.TM,
                        STtm = item.TTM,
                        SNg = item.NG,
                        STng = item.TNG,
                        SMoTa = item.MoTa,
                        IIdMaDonVi = SelectedDonVi.ValueItem,
                        STenDonVi = !string.IsNullOrEmpty(SelectedDonVi.DisplayItem) ? SelectedDonVi.DisplayItem.Substring(6, SelectedDonVi.DisplayItem.Length - 6) : string.Empty,
                        FTuChi = !string.IsNullOrEmpty(item.TuChi) ? double.Parse(item.TuChi) : 0,
                        FPhanCap = !string.IsNullOrEmpty(item.PhanCap) ? double.Parse(item.PhanCap) : 0,
                        FChuaPhanCap = !string.IsNullOrEmpty(item.ChuaPhanCap) ? double.Parse(item.ChuaPhanCap) : 0,
                        SGhiChu = item.GhiChu,
                        FHangNhap = !string.IsNullOrEmpty(item.HangNhap) ? double.Parse(item.HangNhap) : 0,
                        FHangMua = !string.IsNullOrEmpty(item.HangMua) ? double.Parse(item.HangMua) : 0,
                        SXauNoiMa = item.XauNoiMa
                    });
                }

                NsNganhChungTu chungTu = new NsNganhChungTu();
                int soChungTuIndex = _chungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork, _sessionService.Current.Budget);
                chungTu.SSoChungTu = "PB-" + soChungTuIndex.ToString("D3");
                chungTu.ISoChungTuIndex = soChungTuIndex;
                chungTu.DNgayChungTu = NgayChungTu;
                //chungTu.SSoCongVan = SoCongVan;
                chungTu.IIdMaDonVi = SelectedDonVi.ValueItem;
                chungTu.SDslns = string.Join(",", _dataImport.Select(x => x.LNS).Distinct());
                chungTu.INamLamViec = _sessionService.Current.YearOfWork;
                chungTu.INamNganSach = _sessionService.Current.YearOfBudget;
                chungTu.ILoaiChungTu = int.Parse(SelectedNguonNganSach.ValueItem);
                chungTu.SNguoiTao = _sessionService.Current.Principal;
                chungTu.DNgayTao = DateTime.Now;
                chungTu.IIdMaNguonNganSach = _sessionService.Current.Budget;
                chungTu.BKhoa = false;
                _chungTuService.Add(chungTu);

                foreach (var item in chungTuChiTiets)
                {
                    NsMucLucNganSach mlns = _mucLucNganSachs.Where(x => x.XauNoiMa == item.SXauNoiMa).FirstOrDefault();
                    item.IIdCtnganh = chungTu.Id;
                    item.IIdMlns = mlns == null ? Guid.Empty : mlns.MlnsId;
                    item.IIdParentMlns = mlns == null ? Guid.Empty : mlns.MlnsIdParent;
                    item.BHangCha = false;
                    item.SXauNoiMa = mlns != null ? mlns.XauNoiMa : string.Empty;
                    item.INamLamViec = _sessionService.Current.YearOfWork;
                    item.INamNganSach = _sessionService.Current.YearOfBudget;
                    item.IIdMaNguonNganSach = _sessionService.Current.Budget;
                    item.SNguoiTao = _sessionService.Current.Principal;
                    item.DNgayTao = DateTime.Now;
                }

                _chungTuChiTietService.AddRange(chungTuChiTiets);

                // save phan cap
                List<DonVi> donViData = _donViService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();

                List<NsNganhChungTuChiTietPhanCap> listPhanCap = new List<NsNganhChungTuChiTietPhanCap>();
                foreach (LevelBuggetChildImportModel item in DataPhanCap.Where(x => x.ImportStatus && !x.IsWarning && _mucLucNganSachs.Any(y => y.XauNoiMa == x.ConcatenateCode && !y.BHangCha)))
                {
                    NsNganhChungTuChiTiet chiTiet = chungTuChiTiets.Where(n => n.SXauNoiMa == item.XauNoiMa).FirstOrDefault();
                    NsMucLucNganSach mlns = _mucLucNganSachs.Where(x => x.XauNoiMa == item.XauNoiMa).FirstOrDefault();
                    DonVi donVi = donViData.Where(n => n.IIDMaDonVi == item.MaDonVi).FirstOrDefault();
                    if (chiTiet != null && mlns != null)
                    {
                        listPhanCap.Add(new NsNganhChungTuChiTietPhanCap
                        {
                            IIdCtnganhChiTiet = chiTiet.Id,
                            IIdMaDonVi = item.MaDonVi,
                            STenDonVi = donVi != null ? donVi.TenDonVi : string.Empty,
                            IIdMlns = mlns.MlnsId,
                            FPhanCap = !string.IsNullOrEmpty(item.TuChi) ? double.Parse(item.TuChi) : 0,
                            SXauNoiMa = item.XauNoiMa,
                            INamLamViec = _sessionService.Current.YearOfWork,
                            DNgayTao = DateTime.Now,
                            SNguoiTao = _sessionService.Current.Principal
                        });
                    }
                }
                _soLieuChiTietPhanCapService.AddRange(listPhanCap);
                MessageBoxHelper.Info(Resources.MsgImportSuccess);
                LevelBuggetModel chungTuShow = _mapper.Map<LevelBuggetModel>(chungTu);

                //mở màn hình chứng từ chi tiết
                DataChangedEventHandler handler = OpenDetail;
                if (handler != null)
                {
                    handler(chungTuShow, new EventArgs());
                }
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
                message = Resources.ErrorAgencyEmpty;
                return false;
            }

            if (NgayChungTu == null)
            {
                message = Resources.ErrorNgayChungTuEmpty;
                return false;
            }

            //if (string.IsNullOrEmpty(SoCongVan.Trim()))
            //{
            //    message = string.Format(Resources.MsgInputRequire, "Số công văn");
            //    return false;
            //}
            return true;
        }

        private void OnProcessFile()
        {
            string message = string.Empty;
            bool checkValidate = Validate(ref message);
            if (!string.IsNullOrEmpty(message) || !checkValidate)
            {
                MessageBoxHelper.Warning(message);
                return;
            }
            _errors = new List<ImportErrorItem>();
            _errorsChild = new List<ImportErrorItem>();
            //save file to import folder
            string destFile = Path.Combine(_importFolder, string.Format("{0}_{1}", DateTime.Now.ToString("ddMMyyyyhhmmss"), _fileName));
            File.Copy(FilePath, destFile);
            _impHistory = new ImpHistory
            {
                FileName = _fileName,
                FilePath = _importFolder,
                ServiceCode = "Cấp phát",
                UserCreator = _sessionService.Current.Principal,
                DateCreated = DateTime.Now
            };
            _impHistoryService.Add(_impHistory);

            try
            {
                ImportResult<LevelBuggetImportModel> _voucherDetailResult = _importService.ProcessDataUnique<LevelBuggetImportModel>(FilePath);
                _importDataDetailProcess = new List<LevelBuggetImportModel>();
                //xác định cha con trong data import
                foreach (var item in _voucherDetailResult.Data)
                {
                    if (!item.IsErrorMLNS)
                    {
                        var mlns = _mucLucNganSachs.Where(x => x.XauNoiMa == item.ConcatenateCode).FirstOrDefault();
                        if (mlns != null)
                            item.BHangCha = mlns.BHangCha;
                    }
                    var childs = _voucherDetailResult.Data.Where(x => x.ConcatenateCode.Contains(item.ConcatenateCode) && x != item).ToList();
                    if (childs.Count > 0)
                        item.BHangCha = true;
                }

                //kiểm tra loại ngân sách con thỏa mãn điều kiện để import
                foreach (var item in _voucherDetailResult.Data)
                {
                    if (!item.ImportStatus && item.IsErrorMLNS)
                    {
                        var parents = _importDataDetailProcess.Where(x => item.ConcatenateCode.Contains(x.ConcatenateCode) && x != item).ToList();
                        if (parents.Count > 0)
                        {
                            int columnIndexOrigin = 0;
                            LevelBuggetImportModel parent = new LevelBuggetImportModel();
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
                                if (!string.IsNullOrEmpty(parent.TuChi))
                                    parent.TuChi = "0";
                                if (!string.IsNullOrEmpty(parent.PhanCap))
                                    parent.PhanCap = "0";
                                if (!string.IsNullOrEmpty(parent.ChuaPhanCap))
                                    parent.ChuaPhanCap = "0";
                                if (!string.IsNullOrEmpty(parent.HangNhap))
                                    parent.HangNhap = "0";
                                if (!string.IsNullOrEmpty(parent.HangMua))
                                    parent.HangMua = "0";
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
                DataImport = new ObservableCollection<LevelBuggetImportModel>(_importDataDetailProcess);
                foreach (var item in _dataImport)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName != nameof(LevelBuggetImportModel.ImportStatus)
                            && args.PropertyName != nameof(LevelBuggetImportModel.ConcatenateCode)
                            && args.PropertyName != nameof(LevelBuggetImportModel.IsErrorMLNS))
                        {
                            var voucherDetail = (LevelBuggetImportModel)sender;
                            int rowIndex = _dataImport.IndexOf(voucherDetail);
                            var listError = _importService.ValidateItem<LevelBuggetImportModel>(voucherDetail, rowIndex);
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

                //data phan cap LevelBuggetChildImportModel

                ImportResult<LevelBuggetChildImportModel> _voucherChildResult = _importService.ProcessDataUnique<LevelBuggetChildImportModel>(FilePath);
                _importDataChildProcess = new List<LevelBuggetChildImportModel>();
                //xác định cha con trong data import
                foreach (var item in _voucherChildResult.Data)
                {
                    if (!item.IsErrorMLNS)
                    {
                        var mlns = _mucLucNganSachs.Where(x => x.XauNoiMa == item.ConcatenateCode).FirstOrDefault();
                        if (mlns != null)
                            item.BHangCha = mlns.BHangCha;
                    }
                    var childs = _voucherChildResult.Data.Where(x => x.ConcatenateCode.Contains(item.ConcatenateCode) && x != item && x.XauNoiMa != item.XauNoiMa).ToList();
                    if (childs.Count > 0)
                        item.BHangCha = true;
                }

                //kiểm tra loại ngân sách con thỏa mãn điều kiện để import
                foreach (var item in _voucherChildResult.Data)
                {
                    if (!item.ImportStatus && item.IsErrorMLNS)
                    {
                        var parents = _importDataChildProcess.Where(x => item.ConcatenateCode.Contains(x.ConcatenateCode) && x != item).ToList();
                        if (parents.Count > 0)
                        {
                            int columnIndexOrigin = 0;
                            LevelBuggetChildImportModel parent = new LevelBuggetChildImportModel();
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
                                if (!string.IsNullOrEmpty(parent.TuChi))
                                    parent.TuChi = "0";

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
                    _importDataChildProcess.Add(item);
                }
                DataPhanCap = new ObservableCollection<LevelBuggetChildImportModel>(_importDataChildProcess);
                foreach (var item in _dataPhanCap)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName != nameof(LevelBuggetChildImportModel.ImportStatus)
                            && args.PropertyName != nameof(LevelBuggetChildImportModel.ConcatenateCode)
                            && args.PropertyName != nameof(LevelBuggetChildImportModel.IsErrorMLNS))
                        {
                            var voucherDetail = (LevelBuggetChildImportModel)sender;
                            int rowIndex = _dataPhanCap.IndexOf(voucherDetail);
                            var listError = _importService.ValidateItem<LevelBuggetChildImportModel>(voucherDetail, rowIndex);
                            if (listError.Count > 0)
                            {
                                List<string> errors = listError.Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                                string message = string.Join(Environment.NewLine, errors);
                                MessageBoxHelper.Info(message);
                                _errorsChild.AddRange(listError);
                                voucherDetail.ImportStatus = false;
                                if (listError.Any(x => x.IsErrorMLNS))
                                    voucherDetail.IsErrorMLNS = true;
                            }
                            else
                            {
                                voucherDetail.ImportStatus = true;
                                voucherDetail.IsErrorMLNS = false;
                                _errorsChild.RemoveAll(x => x.Row == rowIndex);
                                OnPropertyChanged(nameof(IsSaveData));
                            }
                        }
                    };
                }
                OnPropertyChanged(nameof(DataImport));
                OnPropertyChanged(nameof(DataPhanCap));
                if (_voucherDetailResult.ImportErrors.Count > 0)
                    _errors.AddRange(_voucherDetailResult.ImportErrors);
                if (_voucherChildResult.ImportErrors.Count > 0)
                    _errorsChild.AddRange(_voucherChildResult.ImportErrors);
                if (DataImport.Where(x => !x.IsWarning).Any(x => !x.ImportStatus) || DataPhanCap.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    MessageBoxHelper.Warning(Resources.AlertDataError);
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                //MessageBoxHelper.Warning(Resources.MsgCheckFormatFileImport);
                if (ex is Utility.Exceptions.WrongReportException)
                {
                    System.Windows.MessageBox.Show(Resources.WrongReportFormat, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}