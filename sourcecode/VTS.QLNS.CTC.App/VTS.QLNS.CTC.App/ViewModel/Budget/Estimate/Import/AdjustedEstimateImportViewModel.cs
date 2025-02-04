using AutoMapper;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Import
{
    public class AdjustedEstimateImportViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IMapper _mapper;
        private readonly IImportExcelService _importService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IConfiguration _configuration;
        private readonly IImpHistoryService _impHistoryService;
        private readonly INsDcChungTuService _chungTuService;
        private readonly INsDcChungTuChiTietService _chungTuChiTietService;
        private readonly IDanhMucService _danhMucService;
        private SessionInfo _sessionInfo;
        private List<NsMucLucNganSach> _mucLucNganSachs;
        private ImpHistory _impHistory;
        private List<ImportErrorItem> _errors;
        private List<NsMuclucNgansachModel> _mergeItems;
        private string _importFolder;
        private string _fileName;
        private List<AdjustedEstimateDetailImportModel> _adjustedEstimateDetailProcess;
        private string _soChungTu;
        private int _soChungTuIndex;
        public AdjustedEstimateImportChosenViewModel AdjustedEstimateImportChosenViewModel { get; set; }

        public override string Name => "Điều chỉnh dự toán";
        public override Type ContentType => typeof(AdjustedEstimateImport);
        public override string Description => "Điều chỉnh số liệu dự toán";
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private ObservableCollection<AdjustedEstimateDetailImportModel> _adjustedEstimateDetails;
        public ObservableCollection<AdjustedEstimateDetailImportModel> AdjustedEstimateDetails
        {
            get => _adjustedEstimateDetails;
            set => SetProperty(ref _adjustedEstimateDetails, value);
        }

        private AdjustedEstimateDetailImportModel _selectedItem;
        public AdjustedEstimateDetailImportModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private ComboboxItem _agencySelected;
        public ComboboxItem AgencySelected
        {
            get => _agencySelected;
            set => SetProperty(ref _agencySelected, value);
        }

        private DateTime _voucherDate;
        public DateTime VoucherDate
        {
            get => _voucherDate;
            set => SetProperty(ref _voucherDate, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (AdjustedEstimateDetails.Count > 0)
                    return !AdjustedEstimateDetails.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }

        private ObservableCollection<NsMuclucNgansachModel> _existedMlns;
        public ObservableCollection<NsMuclucNgansachModel> ExistedMlns
        {
            get => _existedMlns;
            set => SetProperty(ref _existedMlns, value);
        }

        private ObservableCollection<NsMuclucNgansachModel> _importedMlns;
        public ObservableCollection<NsMuclucNgansachModel> ImportedMlns
        {
            get => _importedMlns;
            set => SetProperty(ref _importedMlns, value);
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

        private ObservableCollection<ComboboxItem> _voucherTypes;
        public ObservableCollection<ComboboxItem> VoucherTypes
        {
            get => _voucherTypes;
            set => SetProperty(ref _voucherTypes, value);
        }

        private ComboboxItem _voucherTypeSelected;
        public ComboboxItem VoucherTypeSelected
        {
            get => _voucherTypeSelected;
            set => SetProperty(ref _voucherTypeSelected, value);
        }

        private ComboboxItem _estimateSettlementTypeSelected;
        public ComboboxItem EstimateSettlementTypeSelected
        {
            get => _estimateSettlementTypeSelected;
            set
            {
                if (SetProperty(ref _estimateSettlementTypeSelected, value))
                {
                    if (Convert.ToInt32(_estimateSettlementTypeSelected.ValueItem) == (int)EstimateSettlementType.SIX_MONTH)
                    {
                        _duKienQuyetToanDauNamTitle = "Dự kiến QT 6 tháng đầu năm";
                        _duKienQuyetToanCuoiNamTitle = "Dự kiến QT 6 tháng cuối năm";
                    }
                    else
                    {
                        _duKienQuyetToanDauNamTitle = "Dự kiến QT 9 tháng đầu năm";
                        _duKienQuyetToanCuoiNamTitle = "Dự kiến QT 3 tháng cuối năm";
                    }
                    OnPropertyChanged(nameof(DuKienQuyetToanDauNamTitle));
                    OnPropertyChanged(nameof(DuKienQuyetToanCuoiNamTitle));
                }
            }
        }

        private ObservableCollection<ComboboxItem> _estimateSettlementTypes;
        public ObservableCollection<ComboboxItem> EstimateSettlementTypes
        {
            get => _estimateSettlementTypes;
            set => SetProperty(ref _estimateSettlementTypes, value);
        }

        public bool IsEnabledMergeBtn
        {
            get => ImportedMlns.Where(i => i.IsSelected).Count() > 0 && ExistedMlns.Where(i => i.IsSelected).Count() == 1;
        }

        public bool IsEnabledUnmergeCommand
        {
            get => ExistedMlns.Where(i => i.IsModified && i.IsSelected).Count() > 0;
        }

        public bool IsEnableSaveMLNS => _mergeItems.Count > 0;

        private string _duKienQuyetToanDauNamTitle;
        public string DuKienQuyetToanDauNamTitle
        {
            get => _duKienQuyetToanDauNamTitle;
            set => SetProperty(ref _duKienQuyetToanDauNamTitle, value);
        }

        private string _duKienQuyetToanCuoiNamTitle;
        public string DuKienQuyetToanCuoiNamTitle
        {
            get => _duKienQuyetToanCuoiNamTitle;
            set => SetProperty(ref _duKienQuyetToanCuoiNamTitle, value);
        }

        private ObservableCollection<ComboboxItem> _importTieuChiItems = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ImportTieuChiItems
        {
            get => _importTieuChiItems;
            set => SetProperty(ref _importTieuChiItems, value);
        }

        private ComboboxItem _importTieuChiSelected;
        public ComboboxItem ImportTieuChiSelected
        {
            get => _importTieuChiSelected;
            set
            {
                SetProperty(ref _importTieuChiSelected, value);
            }
        }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand AddMLNSCommand { get; }
        public RelayCommand MergeCommand { get; }
        public RelayCommand UnmergeCommand { get; }
        public RelayCommand SaveMLNSCommand { get; }
        public RelayCommand CloseCommand { get; }

        public AdjustedEstimateImportViewModel(
            ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            INsMucLucNganSachService mucLucNganSachService,
            IConfiguration configuration,
            IImpHistoryService impHistoryService,
            INsDcChungTuService chungTuService,
            INsDcChungTuChiTietService chungTuChiTietService,
            IDanhMucService danhMucService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _mucLucNganSachService = mucLucNganSachService;
            _configuration = configuration;
            _impHistoryService = impHistoryService;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _danhMucService = danhMucService;

            AdjustedEstimateImportChosenViewModel = new AdjustedEstimateImportChosenViewModel("Đ/c có muốn lấy dữ liệu căn cứ từ file excel?", "Xác nhận", NSMessageBoxButtons.OKCancel, ChosingHandler);

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
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _voucherDate = DateTime.Now;
            _importFolder = _configuration.GetSection("ImportFolder").Value;
            if (!Directory.Exists(_importFolder))
                Directory.CreateDirectory(_importFolder);
            OnResetData();
            LoadVoucherType();
            LoadEstimateSettlementType();
            LoadChungTuIndex();
            LoadAgencies();
            LoadTieuChis();
        }

        private void LoadChungTuIndex()
        {
            Expression<Func<NsDcChungTu, bool>> predicate = CreatePredicate();
            _soChungTuIndex = _chungTuService.FindNextSoChungTuIndex(predicate);
            _soChungTu = "DC-" + _soChungTuIndex.ToString("D3");
        }

        private Expression<Func<NsDcChungTu, bool>> CreatePredicate()
        {
            Expression<Func<NsDcChungTu, bool>> predicate = PredicateBuilder.True<NsDcChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            return predicate;
        }

        private void LoadVoucherType()
        {
            List<ComboboxItem> cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
            };

            Expression<Func<DonVi, bool>> predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(item => item.BCoNSNganh);
            predicate = predicate.And(item => item.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(item => item.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(item => !item.Loai.Equals(LoaiDonVi.TOAN_QUAN));
            IEnumerable<DonVi> listDonVi = _donViService.FindByCondition(predicate);
            if (listDonVi != null && listDonVi.Count() > 0)
            {
                cbxVoucher.Add(new ComboboxItem { DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key });
            }

            VoucherTypes = new ObservableCollection<ComboboxItem>(cbxVoucher);
            VoucherTypeSelected = VoucherTypes.ElementAt(0);
        }

        private void LoadEstimateSettlementType()
        {
            List<ComboboxItem> cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.EstimateSettlementTypeName[EstimateSettlementType.SIX_MONTH], ValueItem = ((int)EstimateSettlementType.SIX_MONTH).ToString()},
                new ComboboxItem {DisplayItem = VoucherType.EstimateSettlementTypeName[EstimateSettlementType.NINE_MONTH], ValueItem = ((int)EstimateSettlementType.NINE_MONTH).ToString()}
            };

            EstimateSettlementTypes = new ObservableCollection<ComboboxItem>(cbxVoucher);
            EstimateSettlementTypeSelected = EstimateSettlementTypes.ElementAt(0);
        }

        private void OnResetData()
        {
            _mergeItems = new List<NsMuclucNgansachModel>();
            _filePath = string.Empty;
            _agencySelected = null;
            _impHistory = new ImpHistory();
            _errors = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;
            _voucherTypeSelected = null;
            _estimateSettlementTypeSelected = null;

            _mucLucNganSachs = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
            _importedMlns = new ObservableCollection<NsMuclucNgansachModel>();
            _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(_mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(_mucLucNganSachs));
            _adjustedEstimateDetails = new ObservableCollection<AdjustedEstimateDetailImportModel>();

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(AgencySelected));
            OnPropertyChanged(nameof(AdjustedEstimateDetails));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(TabIndex));
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(VoucherTypeSelected));
            OnPropertyChanged(nameof(EstimateSettlementTypeSelected));
        }

        /// <summary>
        /// Tạo data cho combobox đơn vị
        /// </summary>
        private void LoadAgencies()
        {
            //int yearOfWork = _sessionInfo.YearOfWork;
            //Expression<Func<DonVi, bool>> predicate = PredicateBuilder.True<DonVi>();
            //predicate = predicate.And(x => x.NamLamViec == yearOfWork).And(x => x.Loai == AgencyType.LEVEL1.ToString("D"));
            //List<DonVi> listDonVi = _donViService.FindByCondition(predicate).ToList();

            var listDonVi = _donViService.GetDanhSachDonViByNguoiDung(_sessionInfo.Principal, _sessionInfo.YearOfWork);
            _agencies = new List<ComboboxItem>();
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
        }

        private void OnUploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Chọn file excel");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".xlsx";
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            FilePath = openFileDialog.FileName;
            _fileName = openFileDialog.SafeFileName;
        }

        private void OnProcessFile()
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(FilePath))
            {
                message = Resources.ErrorFileEmpty;
                goto ShowError;
            }
            if (_agencySelected == null)
            {
                message = Resources.ErrorAgencyEmpty;
                goto ShowError;
            }
            if (string.IsNullOrEmpty(_voucherDate.ToString()))
            {
                message = Resources.ErrorVoucherDateEmpty;
                goto ShowError;
            }
        ShowError:
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Warning(message);
                return;
            }

            _errors = new List<ImportErrorItem>();
            //save file to import folder
            string destFile = Path.Combine(_importFolder, string.Format("{0}_{1}", DateTime.Now.ToString("ddMMyyyyhhmmss"), _fileName));
            File.Copy(FilePath, destFile);
            _impHistory = new ImpHistory
            {
                FileName = _fileName,
                FilePath = _importFolder,
                ServiceCode = "Điều chỉnh dự toán",
                TableName = "DC_ChungTu",
                UserCreator = _sessionInfo.Principal,
                DateCreated = DateTime.Now
            };
            _impHistoryService.Add(_impHistory);

            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    try
                    {
                        e.Result = _importService.ProcessDataUnique<AdjustedEstimateDetailImportModel>(FilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.Error(Resources.ErrorImport);
                        throw ex;
                    }
                }, (s, e) =>
                {
                    IsLoading = false;
                    if (e.Error == null)
                    {
                        ImportResult<AdjustedEstimateDetailImportModel> _voucherDetailResult = e.Result as ImportResult<AdjustedEstimateDetailImportModel>;
                        _adjustedEstimateDetailProcess = new List<AdjustedEstimateDetailImportModel>();
                        //xác định cha con trong data import
                        foreach (AdjustedEstimateDetailImportModel item in _voucherDetailResult.Data)
                        {
                            if (!item.IsErrorMLNS)
                            {
                                NsMucLucNganSach mlns = _mucLucNganSachs.Where(x => x.XauNoiMa == item.ConcatenateCode).FirstOrDefault();
                                if (mlns != null)
                                    item.BHangCha = mlns.BHangChaDuToan.HasValue && mlns.BHangChaDuToan.Value;
                            }
                            List<AdjustedEstimateDetailImportModel> childs = _voucherDetailResult.Data.Where(x => x.ConcatenateCode.Contains(item.ConcatenateCode) && x != item).ToList();
                            if (childs.Count > 0)
                                item.BHangCha = true;
                        }

                        //kiểm tra loại ngân sách con thỏa mãn điều kiện để import
                        foreach (AdjustedEstimateDetailImportModel item in _voucherDetailResult.Data)
                        {
                            if (!item.ImportStatus && item.IsErrorMLNS)
                            {
                                List<AdjustedEstimateDetailImportModel> parents = _adjustedEstimateDetailProcess.Where(x => item.ConcatenateCode.Contains(x.ConcatenateCode) && x != item).ToList();
                                if (parents.Count > 0)
                                {
                                    int columnIndexOrigin = 0;
                                    AdjustedEstimateDetailImportModel parent = new AdjustedEstimateDetailImportModel();
                                    foreach (AdjustedEstimateDetailImportModel p in parents)
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
                                        if (!string.IsNullOrEmpty(parent.DuToanNganSachNam))
                                            parent.DuToanNganSachNam = "0";
                                        if (!string.IsNullOrEmpty(parent.DuToanChuyenNamSau))
                                            parent.DuToanChuyenNamSau = "0";
                                        if (!string.IsNullOrEmpty(parent.DuKienQtDauNam))
                                            parent.DuKienQtDauNam = "0";
                                        if (!string.IsNullOrEmpty(parent.DuKienQtCuoiNam))
                                            parent.DuKienQtCuoiNam = "0";
                                        if (parent.ListConcatenateCodeChild == null)
                                            parent.ListConcatenateCodeChild = new List<string>();
                                        parent.ListConcatenateCodeChild.Add(item.ConcatenateCode);
                                        item.ConcatenateCodeParent = parent.ConcatenateCode;

                                        NsMucLucNganSach parentOrigin = _mucLucNganSachs.FirstOrDefault(x => x.XauNoiMa == parent.ConcatenateCode);
                                        if (parentOrigin != null && !parentOrigin.BHangCha)
                                        {
                                            item.IsWarning = true;
                                        }
                                        if (parent.IsWarning)
                                            item.IsWarning = true;
                                    }
                                }
                            }
                            _adjustedEstimateDetailProcess.Add(item);
                        }

                        CalculateData();
                        _adjustedEstimateDetails = new ObservableCollection<AdjustedEstimateDetailImportModel>(_adjustedEstimateDetailProcess);
                        foreach (AdjustedEstimateDetailImportModel item in _adjustedEstimateDetails)
                        {
                            item.PropertyChanged += (sender, args) =>
                            {
                                if (args.PropertyName != nameof(AdjustedEstimateDetailImportModel.ImportStatus)
                                    && args.PropertyName != nameof(AdjustedEstimateDetailImportModel.ConcatenateCode)
                                    && args.PropertyName != nameof(AdjustedEstimateDetailImportModel.IsErrorMLNS))
                                {
                                    AdjustedEstimateDetailImportModel voucherDetail = (AdjustedEstimateDetailImportModel)sender;
                                    int rowIndex = _adjustedEstimateDetails.IndexOf(voucherDetail);
                                    List<ImportErrorItem> listError = _importService.ValidateItem<AdjustedEstimateDetailImportModel>(voucherDetail, rowIndex);
                                    if (listError.Count > 0)
                                    {
                                        List<string> errors = listError.Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                                        string message = string.Join(Environment.NewLine, errors);
                                        MessageBoxHelper.Info(message, "Thông tin lỗi");
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
                        OnPropertyChanged(nameof(AdjustedEstimateDetails));
                        if (_voucherDetailResult.ImportErrors.Count > 0)
                            _errors.AddRange(_voucherDetailResult.ImportErrors);
                        if (AdjustedEstimateDetails.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                            MessageBoxHelper.Warning(Resources.AlertDataError);
                        OnPropertyChanged(nameof(IsSaveData));
                    }
                });

            }
            catch (Exception)
            {
                MessageBoxHelper.Warning(Resources.ErrorImport);
            }
        }

        private void CalculateData()
        {
            // Reset value parrent
            //foreach (AdjustedEstimateDetailImportModel item in _adjustedEstimateDetailProcess.Where(x => x.BHangCha))
            //{
            //    if (!string.IsNullOrEmpty(item.DuToanNganSachNam))
            //        item.DuToanNganSachNam = "0";
            //    if (!string.IsNullOrEmpty(item.DuToanChuyenNamSau))
            //        item.DuToanChuyenNamSau = "0";
            //    if (!string.IsNullOrEmpty(item.DuKienQtDauNam))
            //        item.DuKienQtDauNam = "0";
            //    if (!string.IsNullOrEmpty(item.DuKienQtCuoiNam))
            //        item.DuKienQtCuoiNam = "0";
            //}
            //foreach (AdjustedEstimateDetailImportModel item in _adjustedEstimateDetailProcess.Where(x => !x.BHangCha))
            foreach (AdjustedEstimateDetailImportModel item in _adjustedEstimateDetailProcess)
            {
                item.DuToanNganSachNam = item.DuToanNganSachNam.Replace(",", ".");
                item.DuToanChuyenNamSau = item.DuToanChuyenNamSau.Replace(",", ".");
                item.DuKienQtDauNam = item.DuKienQtDauNam.Replace(",", ".");
                item.DuKienQtCuoiNam = item.DuKienQtCuoiNam.Replace(",", ".");
                //CalculateParent(item, item);
            }
        }

        private void CalculateParent(AdjustedEstimateDetailImportModel currentItem, AdjustedEstimateDetailImportModel selfItem)
        {
            List<AdjustedEstimateDetailImportModel> parents = new List<AdjustedEstimateDetailImportModel>();
            if (!currentItem.IsWarning)
                parents = _adjustedEstimateDetailProcess.Where(x => currentItem.ConcatenateCode.Contains(x.ConcatenateCode)
                           && x.BHangCha && currentItem.ConcatenateCode != x.ConcatenateCode).OrderByDescending(x => x.LNS).ToList();
            else parents = _adjustedEstimateDetailProcess.Where(x => currentItem.ConcatenateCode.Contains(x.ConcatenateCode) && currentItem.ConcatenateCode != x.ConcatenateCode).OrderByDescending(x => x.LNS).ToList();
            if (parents.Count > 0)
            {
                int columnIndexOrigin = 0;
                AdjustedEstimateDetailImportModel parent = new AdjustedEstimateDetailImportModel();
                foreach (AdjustedEstimateDetailImportModel p in parents)
                {
                    int maxColumn = p.ConcatenateCode.Split("-").Count();
                    if (maxColumn > columnIndexOrigin)
                    {
                        columnIndexOrigin = maxColumn;
                        parent = p;
                    }
                }

                double parentValue, childValue = 0;
                if (double.TryParse(parent.DuToanChuyenNamSau, out parentValue) && double.TryParse(selfItem.DuToanChuyenNamSau, out childValue))
                    parent.DuToanChuyenNamSau = (parentValue + childValue).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
                if (double.TryParse(parent.DuToanNganSachNam, out parentValue) && double.TryParse(selfItem.DuToanNganSachNam, out childValue))
                    parent.DuToanNganSachNam = (parentValue + childValue).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
                if (double.TryParse(parent.DuKienQtDauNam, out parentValue) && double.TryParse(selfItem.DuKienQtDauNam, out childValue))
                    parent.DuKienQtDauNam = (parentValue + childValue).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
                if (double.TryParse(parent.DuKienQtCuoiNam, out parentValue) && double.TryParse(selfItem.DuKienQtCuoiNam, out childValue))
                    parent.DuKienQtCuoiNam = (parentValue + childValue).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
                CalculateParent(parent, selfItem);
            }
            else return;
        }

        private void ChosingHandler(NSDialogResult result)
        {
            if (result == NSDialogResult.OK)
            {
                _mucLucNganSachs = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();

                List<NsDcChungTuChiTiet> chungTuChiTiets = _mapper.Map<List<NsDcChungTuChiTiet>>(_adjustedEstimateDetails.Select(x =>
                {
                    NsMucLucNganSach z = _mucLucNganSachs.FirstOrDefault(y => y.XauNoiMa == x.ConcatenateCode && y.BHangChaDuToan.HasValue && !y.BHangChaDuToan.Value);
                    if (z != null && x.ImportStatus && !x.IsWarning)
                    {
                        return x;
                    }
                    else return null;
                })).Where(x => x != null).ToList();


                if (!AdjustedEstimateImportChosenViewModel.ColumnChosens.ElementAt(0).IsDeleted)
                {
                    chungTuChiTiets.Select(x =>
                    {
                        x.FDuToanNganSachNam = null;
                        return x;
                    }).ToList();
                }
                if (!AdjustedEstimateImportChosenViewModel.ColumnChosens.ElementAt(1).IsDeleted)
                {
                    chungTuChiTiets.Select(x =>
                    {
                        x.FDuKienQtDauNam = null;
                        return x;
                    }).ToList();
                }

                //kiểm tra tồn tại chứng từ theo đơn vị, tháng, LNS
                List<string> listLNSExist = CheckExistLNS();
                if (listLNSExist.Count > 0)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.AlertExistAdjustedEstimate, _agencySelected.DisplayItem, _estimateSettlementTypeSelected.DisplayItem,
                                _voucherTypeSelected.DisplayItem, string.Join(",", listLNSExist)));
                    return;
                }
                var fileImport = _adjustedEstimateDetails.Where(x => !string.IsNullOrEmpty(x.TNG));
                //kiểm tra cấu hình mục lục ngân sách, dự toán chi tiết tới
                if ((ImportTieuChiSelected.ValueItem == nameof(MLNSFiled.TNG) || ImportTieuChiSelected.ValueItem == nameof(MLNSFiled.NG)) && _mucLucNganSachs.Where(x => x.SDuToanChiTietToi == nameof(MLNSFiled.TNG)).Any() && !fileImport.Any())
                {
                    MessageBoxHelper.Warning(string.Format(Resources.AlertImportAdjustedEstimate));
                    return;
                }

                NsDcChungTu chungTu = new NsDcChungTu();
                chungTu.ISoChungTuIndex = _soChungTuIndex;
                chungTu.SSoChungTu = _soChungTu;
                chungTu.DNgayChungTu = _voucherDate;
                chungTu.IIdMaDonVi = _agencySelected.ValueItem;
                chungTu.SDslns = string.Join(",", _adjustedEstimateDetails.Select(x => x.LNS).Distinct());
                chungTu.ILoaiChungTu = Convert.ToInt32(_voucherTypeSelected.ValueItem);
                chungTu.ILoaiDuKien = Convert.ToInt32(_estimateSettlementTypeSelected.ValueItem);
                chungTu.INamLamViec = _sessionInfo.YearOfWork;
                chungTu.INamNganSach = _sessionInfo.YearOfBudget;
                chungTu.IIdMaNguonNganSach = _sessionInfo.Budget;
                chungTu.SNguoiTao = _sessionInfo.Principal;
                chungTu.DNgayTao = DateTime.Now;
                chungTu.FDieuChinh = chungTuChiTiets.Where(x => x.FDuKienQtDauNam.HasValue).Select(x => x.FDuKienQtDauNam.Value).Sum() +
                                     chungTuChiTiets.Where(x => x.FDuKienQtCuoiNam.HasValue).Select(x => x.FDuKienQtCuoiNam.Value).Sum();
                _chungTuService.Add(chungTu);

                foreach (NsDcChungTuChiTiet item in chungTuChiTiets.Where(x => x.HasValue))
                {
                    NsMucLucNganSach mlns = _mucLucNganSachs.FirstOrDefault(x => x.XauNoiMa == item.SXauNoiMa);
                    item.IIdMlns = mlns == null ? Guid.Empty : mlns.MlnsId;
                    item.IIdMlnsCha = mlns == null ? Guid.Empty : mlns.MlnsIdParent;
                    item.IIdDcchungTu = chungTu.Id;
                    item.BHangCha = false;
                    item.INamLamViec = _sessionInfo.YearOfWork;
                    item.INamNganSach = _sessionInfo.YearOfBudget;
                    item.IIdMaNguonNganSach = _sessionInfo.Budget;
                    item.IIdMaDonVi = _agencySelected.ValueItem;
                    item.SNguoiTao = _sessionInfo.Principal;
                    item.DNgayTao = DateTime.Now;
                }
                _chungTuChiTietService.AddRange(chungTuChiTiets);

                DcChungTuModel model = _mapper.Map<DcChungTuModel>(chungTu);

                MessageBoxHelper.Info(Resources.MsgSaveDone);

                SavedAction?.Invoke(model);
            }
        }

        private void OnSaveData()
        {
            if (_voucherTypeSelected.ValueItem == VoucherType.NSBD_Key)
            {
                if (!_adjustedEstimateDetails.Select(x => x.LNS).Any(x => x.StartsWith("104")))
                {
                    MessageBoxHelper.Warning(Resources.AlertNSBDImport);
                    return;
                }
            }

            AdjustedEstimateImportChosenViewModel.Init();
            AdjustedEstimateImportChosenViewModel.ShowDialogHost("AdjustedEstimateImport");
        }

        /// <summary>
        /// kiểm tra trùng loại ngân sách, đơn vị, loại dự kiến quyết toán và loại chứng từ
        /// </summary>
        private List<string> CheckExistLNS()
        {
            Expression<Func<NsDcChungTu, bool>> predicate = PredicateBuilder.True<NsDcChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && x.INamNganSach == _sessionInfo.YearOfBudget && x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.IIdMaDonVi == _agencySelected.ValueItem);
            predicate = predicate.And(x => x.ILoaiChungTu == Convert.ToInt32(_voucherTypeSelected.ValueItem));
            predicate = predicate.And(x => x.ILoaiDuKien == Convert.ToInt32(_estimateSettlementTypeSelected.ValueItem));
            List<NsDcChungTu> chungTus = _chungTuService.FindByCondition(predicate).ToList();
            List<string> listLNSExist = new List<string>();
            chungTus.ForEach(x =>
            {
                listLNSExist.AddRange(x.SDslns.Split(','));
            });
            List<string> listLNSSelected = _adjustedEstimateDetails.Select(x => x.LNS).ToList();
            return listLNSSelected.Where(x => listLNSExist.Contains(x)).ToList();
        }

        private void ShowError()
        {
            int rowIndex = _adjustedEstimateDetails.IndexOf(SelectedItem);
            List<string> errors = _errors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            MessageBoxHelper.Warning(message, "Thông tin lỗi");
        }

        private void OnAddMLNS()
        {
            TabIndex = ImportTabIndex.MLNS;
            NsMuclucNgansachModel importItem = new NsMuclucNgansachModel();
            if (ImportedMlns.Contains(importItem))
                return;

            bool isImportGroup = false;
            if (!_importedMlns.Any(x => x.XauNoiMa.Contains(SelectedItem.ConcatenateCode))
                || !_existedMlns.Any(x => x.XauNoiMa.Contains(SelectedItem.ConcatenateCode)))
            {
                List<AdjustedEstimateDetailImportModel> data = new List<AdjustedEstimateDetailImportModel>();
                data.Add(SelectedItem);
                if (SelectedItem.BHangCha)
                    data.AddRange(_adjustedEstimateDetailProcess.Where(x => SelectedItem.ListConcatenateCodeChild.Contains(x.ConcatenateCode)).ToList());
                else
                    data.Add(_adjustedEstimateDetailProcess.Where(x => x.ConcatenateCode == SelectedItem.ConcatenateCodeParent).FirstOrDefault());
                if (data.Count > 1)
                    isImportGroup = true;
                data = data.Where(x => x != null).OrderBy(x => x.ConcatenateCode).ToList();
                foreach (AdjustedEstimateDetailImportModel item in data)
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
                            MoTa = item.Description,
                            NamLamViec = _sessionInfo.YearOfWork,
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
                    model.SNguoiTao = _sessionInfo.Principal;
                    model.DNgayTao = DateTime.Now;
                    _mergeItems.Add(model);
                    OnPropertyChanged(nameof(IsEnableSaveMLNS));
                }
            }
            _importedMlns = new ObservableCollection<NsMuclucNgansachModel>();
            OnPropertyChanged(nameof(ExistedMlns));
            OnPropertyChanged(nameof(ImportedMlns));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnSelectionChanged();
        }

        public NsMuclucNgansachModel FindParent(NsMuclucNgansachModel model, IEnumerable<NsMuclucNgansachModel> ExistedMlns)
        {
            IEnumerable<NsMuclucNgansachModel> ancestors = _existedMlns.Where(i => !Guid.Empty.Equals(i.Id) && !model.XauNoiMa.Equals(i.XauNoiMa) &&
                model.XauNoiMa.StartsWith(i.XauNoiMa + "-") && model.NamLamViec == i.NamLamViec)
                .OrderByDescending(i => i.XauNoiMa.Length);
            return ancestors.FirstOrDefault();
        }

        private void OnSelectionChanged()
        {
            foreach (NsMuclucNgansachModel i in _existedMlns)
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
            foreach (NsMuclucNgansachModel i in _importedMlns.Where(x => x.IsModified))
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

        private void OnMerge()
        {
            if (SelectedParent == null)
                return;
            int index = _existedMlns.ToList().FindIndex(x => x.IsSelected);
            _mergeItems = _importedMlns.Where(i => i.IsSelected && i.IsModified).ToList();
            foreach (NsMuclucNgansachModel item in _mergeItems)
            {
                item.MlnsIdParent = SelectedParent.MlnsId;
                item.BHangCha = false;
                item.ITrangThai = 1;
                item.SNguoiTao = _sessionInfo.Principal;
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

        private void OnUnMerge()
        {
            IEnumerable<NsMuclucNgansachModel> unmergeItems = _existedMlns.Where(i => i.IsSelected && i.IsModified).ToList();
            foreach (NsMuclucNgansachModel item in unmergeItems)
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

        private void OnSaveMLNS()
        {
            MessageBoxResult result = MessageBoxHelper.Confirm(Resources.ConfirmAddMLNS);
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

                    foreach (NsMucLucNganSach item in listMLNS)
                    {
                        AdjustedEstimateDetailImportModel importItem = _adjustedEstimateDetails.Where(x => x.ConcatenateCode == item.XauNoiMa).FirstOrDefault();
                        List<ImportErrorItem> listError = _importService.ValidateItem<AdjustedEstimateDetailImportModel>(importItem, _adjustedEstimateDetails.IndexOf(importItem));
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

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        private void LoadTieuChis()
        {
            List<DanhMuc> danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                DanhMuc danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                string chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                ImportTieuChiItems = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi, false));
                _importTieuChiSelected = ImportTieuChiItems != null ? ImportTieuChiItems[0] : null;
            }
        }
    }
}
