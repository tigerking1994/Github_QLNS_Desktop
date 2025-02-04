using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Distribution
{
    public class DistributionImportJsonViewModel : ViewModelBase
    {
        public override string Name => "PHÂN BỔ SỐ KIỂM TRA";
        public override string Description => "Import phân bổ số kiểm tra Json";
        public override Type ContentType => typeof(View.Budget.DemandCheck.Distribution.DistributionImportJson);

        #region Private
        private readonly ISessionService _sessionService;
        private readonly IImportExcelService _importService;
        private readonly INsDonViService _donviService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly ISktMucLucService _muclucSktService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly INsSktNganhThamDinhChiTietSktService _sktChungTuThamDinhService;

        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private List<ImportErrorItem> _errorItems = new List<ImportErrorItem>();
        private Dictionary<int, List<ImportErrorItem>> _errorDetails = new Dictionary<int, List<ImportErrorItem>>();

        private List<NsSktChungTuChiTiet> _lstDetailInsert = new List<NsSktChungTuChiTiet>();
        private List<JsonNsSktNganhThamDinhChiTietSktQuery> _lstThamDinhInsert = new List<JsonNsSktNganhThamDinhChiTietSktQuery>();
        private bool bIsError = false;
        #endregion

        #region ItemsValidate
        Dictionary<string, DonVi> _dicDonVi;
        Dictionary<int, string> _dicNganSachSuDung;
        Dictionary<string, NsSktMucLuc> _dicMucLucSKT;
        Dictionary<int, List<NsSktChungTuChiTiet>> _dicDetail;
        #endregion

        #region Items
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

        private ObservableCollection<NsSktChungTu> _items;
        public ObservableCollection<NsSktChungTu> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private NsSktChungTu _selectedItems;
        public NsSktChungTu SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }

        private ObservableCollection<NsSktChungTuChiTiet> _details;
        public ObservableCollection<NsSktChungTuChiTiet> Details
        {
            get => _details;
            set => SetProperty(ref _details, value);
        }

        private NsSktChungTuChiTiet _selectedDetail;
        public NsSktChungTuChiTiet SelectedDetail
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

        public DistributionImportJsonViewModel(
            ISessionService sessionService,
            IImportExcelService importService,
            INsDonViService donviService,
            INsMucLucNganSachService mucLucNganSachService,
            ISktMucLucService muclucSktService,
            ISktChungTuService sktChungTuService,
            ISktChungTuChiTietService sktChungTuChiTietService,
            INsSktNganhThamDinhChiTietSktService sktChungTuThamDinhService,
            ILog logger,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _importService = importService;
            _donviService = donviService;
            _mucLucNganSachService = mucLucNganSachService;
            _muclucSktService = muclucSktService;
            _sktChungTuService = sktChungTuService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _sktChungTuThamDinhService = sktChungTuThamDinhService;
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
            GetDicNganSachSuDung();
            GetDicMucLucSKT();
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

                _dicDetail = new Dictionary<int, List<NsSktChungTuChiTiet>>();

                _lstDetailInsert = new List<NsSktChungTuChiTiet>();
                _lstThamDinhInsert = new List<JsonNsSktNganhThamDinhChiTietSktQuery>();

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
            List<NsSktChungTu> lstData = _importService.GetDataJson<NsSktChungTu>(FilePath);
            if (lstData == null) return;
            SetupDataNsSktChungTu(lstData);
            if (bIsError)
            {
                MessageBoxHelper.Error(Resources.ImportError);
            }
        }

        private void OnShowDetail()
        {
            if (SelectedItems == null || Items.IndexOf(SelectedItems) == -1)
            {
                _details = new ObservableCollection<NsSktChungTuChiTiet>();
            }
            else
            {
                if (_dicDetail.ContainsKey(Items.IndexOf(SelectedItems)))
                    _details = new ObservableCollection<NsSktChungTuChiTiet>(_dicDetail[Items.IndexOf(SelectedItems)]);
                else
                    _details = new ObservableCollection<NsSktChungTuChiTiet>();
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
            _sktChungTuService.BulkInsert(Items.ToList());
            if (_lstDetailInsert.Any())
            {
                _sktChungTuChiTietService.BulkInsert(_lstDetailInsert);
            }
            if (_lstThamDinhInsert.Any())
            {
                _sktChungTuThamDinhService.BulkInsert(_mapper.Map<List<NsSktNganhThamDinhChiTietSkt>>(_lstThamDinhInsert));
            }
            MessageBoxHelper.Info(Resources.MsgImportSuccess);
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(new object());
        }

        private void OnResetData()
        {
            _fileName = string.Empty;
            _filePath = string.Empty;
            _items = new ObservableCollection<NsSktChungTu>();
            _details = new ObservableCollection<NsSktChungTuChiTiet>();
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
        private void SetupDataNsSktChungTu(List<NsSktChungTu> lstItems)
        {
            int i = 0;
            foreach (var item in lstItems)
            {
                item.Id = Guid.NewGuid();
                bool status = true;

                if (item.ListDetail != null && item.ListDetail.Any())
                {
                    SetupDataNsSktChungTuChiTiet(i, item);
                }

                if (item.ListThamDinh != null && item.ListThamDinh.Any())
                {
                    SetUpDataNsSktNganhThamDinhChiTietSkt(i, item, ref status);
                }
                item.ImportStatus = status;
                ValidateNsSktChungTu(item, i);
                ++i;
            }
            _items = new ObservableCollection<NsSktChungTu>(lstItems);
            _details = new ObservableCollection<NsSktChungTuChiTiet>();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(Details));
        }

        private void ValidateNsSktChungTu(NsSktChungTu item, int index)
        {
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

            if (item.IIdMaNguonNganSach.Value != _sessionService.Current.Budget)
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

            if (!_dicNganSachSuDung.ContainsKey(item.ILoaiChungTu.Value))
            {
                _errorItems.Add(new ImportErrorItem()
                {
                    ColumnName = "Mã loại chứng từ",
                    Row = index,
                    Error = String.Format(Resources.MsgErrorItemNotFound, "Mã loại chứng từ")
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

            if (item.INamNganSach.Value != _sessionService.Current.YearOfBudget)
            {
                _errorItems.Add(new ImportErrorItem()
                {
                    ColumnName = "Mã năm ngân sách",
                    Row = index,
                    Error = String.Format(Resources.MsgErrorItemNotEqualSystem, "Mã năm ngân sách")
                });
                item.ImportStatus = false;
                bIsError = true;
            }
        }

        private void SetupDataNsSktChungTuChiTiet(int pageIndex, NsSktChungTu objParent)
        {
            int i = 0;
            foreach (var item in objParent.ListDetail)
            {
                item.Id = Guid.NewGuid();
                item.IIdCtsoKiemTra = objParent.Id;
                item.IIdMlskt = Guid.Empty;
                item.STenDonVi = null;
                item.SMoTa = null;
                item.ImportStatus = true;
                ValidateNsSktChungTuChiTiet(pageIndex, item, i);
                _lstDetailInsert.Add(item);
                ++i;
            }
            if (!_dicDetail.ContainsKey(pageIndex))
                _dicDetail.Add(pageIndex, objParent.ListDetail);
        }

        private void ValidateNsSktChungTuChiTiet(int pageIndex, NsSktChungTuChiTiet item, int index)
        {
            if (!_errorDetails.ContainsKey(pageIndex))
                _errorDetails.Add(pageIndex, new List<ImportErrorItem>());

            if (!_dicDonVi.ContainsKey(item.IIdMaDonVi))
            {
                _errorDetails[pageIndex].Add(new ImportErrorItem()
                {
                    ColumnName = "Mã đơn vị",
                    Row = index,
                    Error = String.Format(Resources.MsgErrorItemNotFound, "Mã đơn vị")
                });
                item.ImportStatus = false;
                bIsError = true;
            }
            else
            {
                item.STenDonVi = _dicDonVi[item.IIdMaDonVi].TenDonVi;
            }

            if (item.IIdMaNguonNganSach.Value != _sessionService.Current.Budget)
            {
                _errorDetails[pageIndex].Add(new ImportErrorItem()
                {
                    ColumnName = "Mã nguồn ngân sách",
                    Row = index,
                    Error = String.Format(Resources.MsgErrorItemNotEqualSystem, "Mã nguồn ngân sách")
                });
                item.ImportStatus = false;
                bIsError = true;
            }

            if (!_dicNganSachSuDung.ContainsKey(item.ILoaiChungTu.Value))
            {
                _errorDetails[pageIndex].Add(new ImportErrorItem()
                {
                    ColumnName = "Mã loại chứng từ",
                    Row = index,
                    Error = String.Format(Resources.MsgErrorItemNotFound, "Mã loại chứng từ")
                });
                item.ImportStatus = false;
                bIsError = true;
            }

            if (item.INamLamViec != _sessionService.Current.YearOfWork)
            {
                _errorDetails[pageIndex].Add(new ImportErrorItem()
                {
                    ColumnName = "Năm làm việc",
                    Row = index,
                    Error = String.Format(Resources.MsgErrorItemNotEqualSystem, "Năm làm việc")
                });
                item.ImportStatus = false;
                bIsError = true;
            }

            if (item.INamNganSach.Value != _sessionService.Current.YearOfBudget)
            {
                _errorDetails[pageIndex].Add(new ImportErrorItem()
                {
                    ColumnName = "Mã năm ngân sách",
                    Row = index,
                    Error = String.Format(Resources.MsgErrorItemNotEqualSystem, "Mã năm ngân sách")
                });
                item.ImportStatus = false;
                bIsError = true;
            }

            if (!_dicMucLucSKT.ContainsKey(item.SKyHieu))
            {
                _errorDetails[pageIndex].Add(new ImportErrorItem()
                {
                    ColumnName = "Ký hiệu",
                    Row = index,
                    Error = String.Format(Resources.MsgErrorItemNotFound, "Ký hiệu")
                });
                item.ImportStatus = false;
                bIsError = true;
            }
            else
            {
                item.SMoTa = _dicMucLucSKT[item.SKyHieu].SMoTa;
                item.IIdMlskt = _dicMucLucSKT[item.SKyHieu].IIDMLSKT;
            }
        }

        private void SetUpDataNsSktNganhThamDinhChiTietSkt(int pageIndex, NsSktChungTu objChungTu, ref bool rootStatus)
        {
            Dictionary<string, string> dicErrorMlns = new Dictionary<string, string>();
            foreach (var item in objChungTu.ListThamDinh)
            {
                item.Id = Guid.NewGuid();
                item.IIdCtsoKiemTra = objChungTu.Id;
                item.STenDonVi = null;
                item.SMoTa = null;
                item.IIdMucLuc = Guid.Empty;
                item.IIdMaDonVi = objChungTu.IIdMaDonVi;
                item.IIdMaNguonNganSach = objChungTu.IIdMaNguonNganSach ?? 0;
                item.INamLamViec = objChungTu.INamLamViec;
                item.INamNganSach = objChungTu.INamNganSach ?? 0;

                item.ImportStatus = true;
                ValidateNsSktNganhThamDinhChiTietSkt(item, ref rootStatus, ref dicErrorMlns);
                if (item.ImportStatus)
                    _lstThamDinhInsert.Add(item);
            }
            if (dicErrorMlns.Count != 0)
            {
                _errorItems.AddRange(dicErrorMlns.Select(n => new ImportErrorItem()
                {
                    ColumnName = "Mục lục SKT",
                    Row = pageIndex,
                    Error = n.Value
                }));
            }
        }

        private void ValidateNsSktNganhThamDinhChiTietSkt(JsonNsSktNganhThamDinhChiTietSktQuery item, ref bool rootStatus, ref Dictionary<string, string> dicErrorMlns)
        {
            if (_dicDonVi.ContainsKey(item.IIdMaDonVi))
            {
                item.STenDonVi = _dicDonVi[item.IIdMaDonVi].TenDonVi;
            }

            if (!_dicMucLucSKT.ContainsKey(item.SKyHieu))
            {
                rootStatus = false;
                bIsError = true;
                if (!dicErrorMlns.ContainsKey(item.SKyHieu))
                {
                    dicErrorMlns.Add(item.SKyHieu, string.Format("Chứng từ ngành thẩm định có mục lục SKT {0} không hợp lệ !", item.SKyHieu));
                }
            }
            else
            {
                item.SMoTa = _dicMucLucSKT[item.SKyHieu].SMoTa;
                item.IIdMucLuc = _dicMucLucSKT[item.SKyHieu].IIDMLSKT;
            }
        }

        private void GetDicDonVi()
        {
            var lstData = _donviService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            _dicDonVi = new Dictionary<string, DonVi>();
            if (lstData != null)
                _dicDonVi = lstData.ToDictionary(n => n.IIDMaDonVi, n => n);
        }

        private void GetDicNganSachSuDung()
        {
            _dicNganSachSuDung = new Dictionary<int, string>();
            _dicNganSachSuDung.Add(1, "Ngân sách sử dụng");
            _dicNganSachSuDung.Add(2, "Ngân sách đặc thù của ngành");
        }

        private void GetDicMucLucSKT()
        {
            _dicMucLucSKT = new Dictionary<string, NsSktMucLuc>();
            var data = _muclucSktService.FindByCondition(n => n.INamLamViec == _sessionService.Current.YearOfWork && n.ITrangThai == (int)Status.ACTIVE);
            if (data != null)
            {
                foreach (var item in data)
                    if (!_dicMucLucSKT.ContainsKey(item.SKyHieu))
                        _dicMucLucSKT.Add(item.SKyHieu, item);
            }
        }
        #endregion
    }
}
