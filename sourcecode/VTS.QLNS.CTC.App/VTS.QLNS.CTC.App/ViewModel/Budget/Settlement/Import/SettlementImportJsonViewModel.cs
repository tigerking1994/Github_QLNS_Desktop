using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using System.Windows;
using System.IO;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Import
{
    public class SettlementImportJsonViewModel : ViewModelBase
    {
        public override string Name
        {
            get
            {
                switch (SLoai)
                {
                    case SettlementType.REGULAR_BUDGET:
                        return "Import ngân sách thường xuyên";
                    case SettlementType.EXPENSE_BUDGET:
                        return "Import ngân sách kinh phí khác";
                    case SettlementType.FOREX_BUDGET:
                        return "Import ngân sách ngoại hối";
                    case SettlementType.STATE_BUDGET:
                        return "Import ngân sách nhà nước";
                    case SettlementType.DEFENSE_BUDGET:
                        return "Import ngân sách quốc phòng";
                    default:
                        return string.Empty;
                }
            }
        }

        public override string Description
        {
            get
            {
                switch (SLoai)
                {
                    case SettlementType.REGULAR_BUDGET:
                        return "Import quyết toán Lương, Phụ cấp, Trợ cấp, Tiền ăn Json";
                    case SettlementType.EXPENSE_BUDGET:
                        return "Import quyết toán ngân sách kinh phí khác Json";
                    case SettlementType.FOREX_BUDGET:
                        return "Import quyết toán ngân sách ngoại hối Json";
                    case SettlementType.STATE_BUDGET:
                        return "Import quyết toán ngân sách nhà nước Json";
                    case SettlementType.DEFENSE_BUDGET:
                        return "Import quyết toán ngân sách Quốc phòng (nghiệp vụ và NSK) Json";
                    default:
                        return string.Empty;
                }
            }
        }
        public override Type ContentType => typeof(View.Budget.DemandCheck.Plan.PlanBeginYearImportJson);

        #region Private
        private readonly ISessionService _sessionService;
        private readonly IImportExcelService _importService;
        private readonly INsDonViService _donviService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IImpHistoryService _impHistoryService;
        private readonly INsQtChungTuService _chungtuService;
        private readonly INsQtChungTuChiTietService _chungtuDetailService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private ImpHistory _impHistory;
        private List<ImportErrorItem> _errorItems = new List<ImportErrorItem>();
        private Dictionary<int, List<ImportErrorItem>> _errorDetails = new Dictionary<int, List<ImportErrorItem>>();

        private List<NsQtChungTuChiTiet> _lstDetailInsert = new List<NsQtChungTuChiTiet>();
        private bool bIsError = false;
        #endregion

        #region ItemsValidate
        Dictionary<string, DonVi> _dicDonVi;
        Dictionary<string, NsMucLucNganSach> _dicMucLuc;
        Dictionary<int, List<NsQtChungTuChiTiet>> _dicDetail;
        #endregion

        #region Items
        private string _sLoai;
        public string SLoai
        {
            get => _sLoai;
            set => SetProperty(ref _sLoai, value);
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

        private ObservableCollection<NsQtChungTu> _items;
        public ObservableCollection<NsQtChungTu> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private NsQtChungTu _selectedItems;
        public NsQtChungTu SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }

        private ObservableCollection<NsQtChungTuChiTiet> _details;
        public ObservableCollection<NsQtChungTuChiTiet> Details
        {
            get => _details;
            set => SetProperty(ref _details, value);
        }

        private NsQtChungTuChiTiet _selectedDetail;
        public NsQtChungTuChiTiet SelectedDetail
        {
            get => _selectedDetail;
            set => SetProperty(ref _selectedDetail, value);
        }
        #endregion

        #region Relay Command
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorItemsCommand { get; }
        public RelayCommand ShowErrorDetailCommand { get; }
        public RelayCommand ShowDetailCommand { get; }
        public RelayCommand CloseCommand { get; }
        #endregion

        public SettlementImportJsonViewModel(
            ISessionService sessionService,
            IImportExcelService importService,
            INsDonViService donviService,
            INsMucLucNganSachService mucLucNganSachService,
            IImpHistoryService impHistoryService,
            INsQtChungTuService chungtuService,
            INsQtChungTuChiTietService chungtuDetailService,
            ILog logger,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _importService = importService;
            _donviService = donviService;
            _mucLucNganSachService = mucLucNganSachService;
            _impHistoryService = impHistoryService;
            _chungtuService = chungtuService;
            _chungtuDetailService = chungtuDetailService;
            _logger = logger;
            _mapper = mapper;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorItemsCommand = new RelayCommand(obj => ShowItemsError());
            ShowErrorDetailCommand = new RelayCommand(obj => ShowDetailError());
            ShowDetailCommand = new RelayCommand(obj => OnShowDetail());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
        }

        #region Relay Command
        public override void Init()
        {
            base.Init();
            OnResetData();
            bIsError = false;
            GetDicDonVi();
            GetDicMucLuc();
        }

        private void OnUploadFile()
        {
            try
            {
                OnResetData();
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = string.Format("Chọn file Json");
                openFileDialog.RestoreDirectory = true;
                openFileDialog.DefaultExt = FileExtensionFormats.Json;
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                FilePath = openFileDialog.FileName;
                _fileName = openFileDialog.SafeFileName;

                _dicDetail = new Dictionary<int, List<NsQtChungTuChiTiet>>();

                _lstDetailInsert = new List<NsQtChungTuChiTiet>();

                if (string.IsNullOrEmpty(FilePath) || !File.Exists(FilePath)) return;
                bIsError = false;
                OnProcessFile();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnProcessFile()
        {
            List<NsQtChungTu> lstData = _importService.GetDataJson<NsQtChungTu>(FilePath);
            if (lstData == null) return;
            SetupDataChungTu(lstData);
            if (bIsError)
            {
                MessageBoxHelper.Error(Resources.ImportError);
            }
        }

        private void OnShowDetail()
        {
            if (SelectedItems == null || Items.IndexOf(SelectedItems) == -1)
            {
                _details = new ObservableCollection<NsQtChungTuChiTiet>();
            }
            else
            {
                if (_dicDetail.ContainsKey(Items.IndexOf(SelectedItems)))
                    _details = new ObservableCollection<NsQtChungTuChiTiet>(_dicDetail[Items.IndexOf(SelectedItems)]);
                else
                    _details = new ObservableCollection<NsQtChungTuChiTiet>();
            }
            OnPropertyChanged(nameof(Details));
        }

        public override void OnSave()
        {
            base.OnSave();
            if (bIsError)
            {
                MessageBoxHelper.Error(Resources.ImportError);
                return;
            }
            _chungtuService.BulkInsertNsQtChungTu(Items.ToList());
            if (_lstDetailInsert.Any())
            {
                _chungtuDetailService.BulkInsertNsQtChungTuChiTiet(_lstDetailInsert);
            }
            MessageBoxHelper.Info(Resources.MsgImportSuccess);
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(new object());
        }

        private void OnResetData()
        {
            _fileName = string.Empty;
            _filePath = string.Empty;
            _impHistory = new ImpHistory();
            _items = new ObservableCollection<NsQtChungTu>();
            _details = new ObservableCollection<NsQtChungTuChiTiet>();
            _errorItems = new List<ImportErrorItem>();
            _errorDetails = new Dictionary<int, List<ImportErrorItem>>();

            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(Details));
        }

        private void ShowItemsError()
        {
            int rowIndex = Items.IndexOf(SelectedItems);
            List<string> errors = _errorItems.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            MessageBoxHelper.Info(message);
        }

        private void ShowDetailError()
        {
            int pageIndex = Items.IndexOf(SelectedItems);
            int rowIndex = Details.IndexOf(SelectedDetail);
            if (_errorDetails.ContainsKey(pageIndex))
            {
                List<string> errors = _errorDetails[pageIndex].Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                string message = string.Join(Environment.NewLine, errors);
                MessageBoxHelper.Info(message);
            }

        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
        #endregion

        #region  Helper
        private void SetupDataChungTu(List<NsQtChungTu> lstItems)
        {
            int i = 0;
            foreach (var item in lstItems)
            {
                item.Id = Guid.NewGuid();
                bool rootStatus = true;
                if (item.Details != null && item.Details.Any())
                {
                    SetupDataChungTuChiTiet(i, item, ref rootStatus);
                }
                item.ImportStatus = rootStatus;
                ValidateChungTu(item, i);
                ++i;
            }
            _items = new ObservableCollection<NsQtChungTu>(lstItems);
            _details = new ObservableCollection<NsQtChungTuChiTiet>();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(Details));
        }

        private void ValidateChungTu(NsQtChungTu item, int index)
        {
            if (item.SLoai != SLoai)
            {
                _errorItems.Add(new ImportErrorItem()
                {
                    ColumnName = "Loại chứng từ",
                    Row = index,
                    Error = String.Format(Resources.MsgErrorItemNotEqualSystem, "Loại chứng từ")
                });
                item.ImportStatus = false;
                bIsError = true;
            }
            if (!_dicDonVi.ContainsKey(item.IIdMaDonVi))
            {
                _errorItems.Add(new ImportErrorItem()
                {
                    ColumnName = "Mã đơn vị",
                    Row = index,
                    Error = String.Format(Resources.MsgErrorItemNotFound, "Mã đơn vị")
                });
                item.ImportStatus = false;
                bIsError = true;
            }
            if (item.IIdMaNguonNganSach != _sessionService.Current.Budget)
            {
                _errorItems.Add(new ImportErrorItem()
                {
                    ColumnName = "Mã nguồn ngân sách",
                    Row = index,
                    Error = String.Format(Resources.MsgErrorItemNotEqualSystem, "Mã nguồn ngân sách")
                });
                item.ImportStatus = false;
                bIsError = true;
            }
            if (item.INamLamViec != _sessionService.Current.YearOfWork)
            {
                _errorItems.Add(new ImportErrorItem()
                {
                    ColumnName = "Năm làm việc",
                    Row = index,
                    Error = String.Format(Resources.MsgErrorItemNotEqualSystem, "Năm làm việc")
                });
                item.ImportStatus = false;
                bIsError = true;
            }
            if (item.INamNganSach != _sessionService.Current.YearOfBudget)
            {
                _errorItems.Add(new ImportErrorItem()
                {
                    ColumnName = "Năm ngân sách",
                    Row = index,
                    Error = String.Format(Resources.MsgErrorItemNotEqualSystem, "Năm ngân sách")
                });
                item.ImportStatus = false;
                bIsError = true;
            }
        }

        private void SetupDataChungTuChiTiet(int pageIndex, NsQtChungTu objParent, ref bool rootStatus)
        {
            int i = 0;
            foreach (var item in objParent.Details)
            {
                item.Id = Guid.NewGuid();
                item.IIdQtchungTu = objParent.Id;
                item.IIdMaDonVi = objParent.IIdMaDonVi;
                item.IIdMaNguonNganSach = objParent.IIdMaNguonNganSach;
                item.INamNganSach = objParent.INamNganSach;
                item.INamLamViec = objParent.INamLamViec;
                item.IThangQuy = objParent.IThangQuy;
                item.IThangQuyLoai = objParent.IThangQuyLoai;
                item.ImportStatus = true;
                ValidateChungTuChiTiet(pageIndex, item, i);
                _lstDetailInsert.Add(item);
                ++i;
            }
            if (!_dicDetail.ContainsKey(pageIndex))
                _dicDetail.Add(pageIndex, objParent.Details);

        }

        private void ValidateChungTuChiTiet(int pageIndex, NsQtChungTuChiTiet item, int index)
        {
            if (!_errorDetails.ContainsKey(pageIndex))
                _errorDetails.Add(pageIndex, new List<ImportErrorItem>());

            if (!_dicMucLuc.ContainsKey(item.SXauNoiMa))
            {
                _errorDetails[pageIndex].Add(new ImportErrorItem()
                {
                    ColumnName = "Xâu nối mã",
                    Row = index,
                    Error = String.Format(Resources.MsgErrorItemNotFound, "Xâu nối mã")
                });
                item.ImportStatus = false;
                bIsError = true;
            }
            else
            {
                item.SMoTa = _dicMucLuc[item.SXauNoiMa].MoTa;
            }
        }

        private void GetDicDonVi()
        {
            var lstData = _donviService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            _dicDonVi = new Dictionary<string, DonVi>();
            if (lstData != null)
            {
                List<string> exclude = new List<string>() { "0", "1" };
                if (lstData.Any(n => exclude.Contains(n.Loai)))
                    _dicDonVi = lstData.Where(n => exclude.Contains(n.Loai)).ToDictionary(n => n.IIDMaDonVi, n => n);
            }
        }

        private void GetDicMucLuc()
        {
            _dicMucLuc = new Dictionary<string, NsMucLucNganSach>();
            var data = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork);
            if (data != null)
            {
                foreach (var item in data)
                    if (!_dicMucLuc.ContainsKey(item.XauNoiMa))
                        _dicMucLuc.Add(item.XauNoiMa, item);
            }
        }

        #endregion
    }
}
