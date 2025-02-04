using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.Budget.Estimate.AdjustedEstimate;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.AdjustedEstimate
{
    public class AdjustedEstimateImportJsonViewModel : ViewModelBase
    {
        public override string Name => "ĐIỀU CHỈNH DỰ TOÁN";
        public override string Description => "Import điều chỉnh dự toán";
        public override Type ContentType => typeof(AdjustedEstimateImportJson);

        #region Private
        private readonly ISessionService _sessionService;
        private readonly IImportExcelService _importService;
        private readonly INsDonViService _donviService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IConfiguration _configuration;
        private readonly ICauHinhCanCuService _cauhinhCanCuService;
        private readonly INsDcChungTuService _chungTuService;
        private readonly INsDcChungTuChiTietService _chungTuChiTietService;
        private readonly ISktSoLieuChungTuService _sktSoLieuChungTuService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly ISktSoLieuChiTietCanCuDataService _sktSoLieuChiTietCanCuDataService;
        private readonly ISoLieuChiTietPhanCapService _soLieuChiTietPhanCapService;
        private readonly IImpHistoryService _impHistoryService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private ImpHistory _impHistory;
        private List<ImportErrorItem> _errorItems = new List<ImportErrorItem>();
        private Dictionary<int, List<ImportErrorItem>> _errorDetails = new Dictionary<int, List<ImportErrorItem>>();

        private List<NsDcChungTuChiTiet> _lstDetailInsert = new List<NsDcChungTuChiTiet>();
        private bool bIsError = false;
        #endregion

        #region ItemsValidate
        Dictionary<string, DonVi> _dicDonVi;
        Dictionary<int, string> _dicNganSachSuDung;
        Dictionary<string, NsMucLucNganSach> _dicMucLuc;
        Dictionary<string, NsCauHinhCanCu> _dicCauHinhCanCu;
        Dictionary<int, List<NsDcChungTuChiTiet>> _dicDetail;
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

        private ObservableCollection<NsDcChungTu> _items;
        public ObservableCollection<NsDcChungTu> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private NsDcChungTu _selectedItems;
        public NsDcChungTu SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }

        private ObservableCollection<NsDcChungTuChiTiet> _details;
        public ObservableCollection<NsDcChungTuChiTiet> Details
        {
            get => _details;
            set => SetProperty(ref _details, value);
        }

        private NsDcChungTuChiTiet _selectedDetail;
        public NsDcChungTuChiTiet SelectedDetail
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

        public AdjustedEstimateImportJsonViewModel(
            ISessionService sessionService,
            IImportExcelService importService,
            INsDonViService donviService,
            INsMucLucNganSachService mucLucNganSachService,
            IConfiguration configuration,
            ICauHinhCanCuService cauhinhCanCuService,
            IImpHistoryService impHistoryService,
            ISktSoLieuChungTuService sktSoLieuChungTuService,
            ISktSoLieuService sktSoLieuService,
            INsDcChungTuChiTietService chungTuChiTietService,
            INsDcChungTuService chungTuService,
            ISktSoLieuChiTietCanCuDataService sktSoLieuChiTietCanCuDataService,
            ISoLieuChiTietPhanCapService soLieuChiTietPhanCapService,
            ILog logger,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _importService = importService;
            _donviService = donviService;
            _chungTuService = chungTuService;
            _mucLucNganSachService = mucLucNganSachService;
            _configuration = configuration;
            _cauhinhCanCuService = cauhinhCanCuService;
            _impHistoryService = impHistoryService;
            _sktSoLieuChungTuService = sktSoLieuChungTuService;
            _sktSoLieuService = sktSoLieuService;
            _chungTuChiTietService = chungTuChiTietService;
            _sktSoLieuChiTietCanCuDataService = sktSoLieuChiTietCanCuDataService;
            _soLieuChiTietPhanCapService = soLieuChiTietPhanCapService;
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
            GetDicMucLuc();
            GetDicCauHinhCanCu();
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

                _dicDetail = new Dictionary<int, List<NsDcChungTuChiTiet>>();

                _lstDetailInsert = new List<NsDcChungTuChiTiet>();

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
            List<NsDcChungTu> lstData = _importService.GetDataJson<NsDcChungTu>(FilePath);
            if (lstData == null) return;
            SetupDataNsDcChungTu(lstData);
            if (bIsError)
            {
                MessageBoxHelper.Error(Resources.ImportError);
            }
        }

        private void OnShowDetail()
        {
            if (SelectedItems == null || Items.IndexOf(SelectedItems) == -1)
            {
                _details = new ObservableCollection<NsDcChungTuChiTiet>();
            }
            else
            {
                if (_dicDetail.ContainsKey(Items.IndexOf(SelectedItems)))
                    _details = new ObservableCollection<NsDcChungTuChiTiet>(_dicDetail[Items.IndexOf(SelectedItems)]);
                else
                    _details = new ObservableCollection<NsDcChungTuChiTiet>();
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
            _chungTuService.BulkInsert(Items.ToList());
            if (_lstDetailInsert.Any())
            {
                _chungTuChiTietService.BulkInsert(_lstDetailInsert);
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
            _items = new ObservableCollection<NsDcChungTu>();
            _details = new ObservableCollection<NsDcChungTuChiTiet>();
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
        private void SetupDataNsDcChungTu(List<NsDcChungTu> lstItems)
        {
            int i = 0;
            foreach (var item in lstItems)
            {
                item.Id = Guid.NewGuid();
                bool rootStatus = true;
                if (item.ListDetail != null && item.ListDetail.Any())
                {
                    SetupDataNsDcChungTuChiTiet(i, item, ref rootStatus);
                }
               
                item.ImportStatus = rootStatus;
                ValidateNsDcChungTu(item, i);
                ++i;
            }
            _items = new ObservableCollection<NsDcChungTu>(lstItems);
            _details = new ObservableCollection<NsDcChungTuChiTiet>();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(Details));
        }

        private void ValidateNsDcChungTu(NsDcChungTu item, int index)
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

            if (!_dicNganSachSuDung.ContainsKey(item.ILoaiChungTu))
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

            if (item.INamLamViec.Value != _sessionService.Current.YearOfWork)
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

        private void SetupDataNsDcChungTuChiTiet(int pageIndex, NsDcChungTu objParent, ref bool rootStatus)
        {
            int i = 0;
            Dictionary<string, string> dicErrorMucLuc = new Dictionary<string, string>();
            foreach (var item in objParent.ListDetail)
            {
                item.Id = Guid.NewGuid();
                item.IIdDcchungTu = objParent.Id;
                item.SMoTa = null;
                item.IIdMaDonVi = objParent.IIdMaDonVi;
                item.IIdMaNguonNganSach = objParent.IIdMaNguonNganSach ?? 0;
                item.INamLamViec = objParent.INamLamViec ?? 0;
                item.INamNganSach = objParent.INamNganSach ?? 0;
                item.ImportStatus = true;
                ValidateNsDcChungTuChiTiet(pageIndex, item, i);
                _lstDetailInsert.Add(item);
                ++i;
            }
            if (!_dicDetail.ContainsKey(pageIndex))
                _dicDetail.Add(pageIndex, objParent.ListDetail);

            if (dicErrorMucLuc.Count != 0)
            {
                _errorItems.AddRange(dicErrorMucLuc.Select(n => new ImportErrorItem()
                {
                    ColumnName = "Mục lục ngân sách",
                    Row = pageIndex,
                    Error = n.Value
                }));
            }
        }

        private void ValidateNsDcChungTuChiTiet(int pageIndex, NsDcChungTuChiTiet item, int index)
        {
            if (!_errorDetails.ContainsKey(pageIndex))
                _errorDetails.Add(pageIndex, new List<ImportErrorItem>());

            //if (_dicDonVi.ContainsKey(item.IIdMaDonVi))
            //{
            //    item.STenDonVi = _dicDonVi[item.IIdMaDonVi].TenDonVi;
            //}

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

        //private void SetUpDataNsDtDauNamChungTuChiTietCanCu(int iPageIndex, NsDtdauNamChungTu objChungTu, ref bool rootStatus)
        //{
        //    Dictionary<string, string> dicErrorCanCu = new Dictionary<string, string>();
        //    Dictionary<string, string> dicErrorXauNoiMa = new Dictionary<string, string>();
        //    foreach (var item in objChungTu.ListDtDauNamCanCu)
        //    {
        //        item.Id = Guid.NewGuid();
        //        item.IID_CTDTDauNam = objChungTu.Id;
        //        item.STenDonVi = null;
        //        item.SMoTa = null;
        //        item.IIdCanCu = null;
        //        item.IIdMaDonVi = objChungTu.IIdMaDonVi;
        //        item.INamNganSach = objChungTu.INamNganSach ?? 0;
        //        item.ILoaiChungTu = objChungTu.ILoaiChungTu ?? 0;
        //        item.INamLamViec = objChungTu.INamLamViec ?? 0;
        //        item.IIdMaNguonNganSach = objChungTu.IIdMaNguonNganSach ?? 0;
        //        item.ImportStatus = true;
        //        ValidateNsDtDauNamChungTuChiTietCanCu(item, ref rootStatus, ref dicErrorCanCu, ref dicErrorXauNoiMa);
        //        if (item.ImportStatus)
        //            _lstCanCuInsert.Add(item);
        //    }
        //    if (dicErrorCanCu.Count != 0)
        //    {
        //        _errorItems.AddRange(dicErrorCanCu.Select(n => new ImportErrorItem()
        //        {
        //            ColumnName = "Cấu hình căn cứ",
        //            Row = iPageIndex,
        //            Error = n.Value
        //        }));
        //    }
        //    if (dicErrorXauNoiMa.Count != 0)
        //    {
        //        _errorItems.AddRange(dicErrorXauNoiMa.Select(n => new ImportErrorItem()
        //        {
        //            ColumnName = "Mục lục ngân sách",
        //            Row = iPageIndex,
        //            Error = n.Value
        //        }));
        //    }
        //}

        private void ValidateNsDtDauNamChungTuChiTietCanCu(JsonNsDtDauNamChungTuChiTietCanCuQuery item, ref bool rootStatus,
            ref Dictionary<string, string> dicErrorCanCu, ref Dictionary<string, string> dicErrorXauNoiMa)
        {
            string sKeyCanCu = string.Format("{0}\t{1}\t{2}\t{3}", item.IIDMaChucNang, item.INamCanCu, item.INamLamViec, item.SModule);
            string sKeyError = string.Format("{0}\t{1}", item.IIDMaChucNang, item.INamCanCu);
            if (!_dicCauHinhCanCu.ContainsKey(sKeyCanCu))
            {
                rootStatus = false;
                bIsError = true;
                if (!dicErrorCanCu.ContainsKey(sKeyError))
                    dicErrorCanCu.Add(sKeyError, string.Format("Chứng từ căn cứ có căn cứ {0} năm {1} không hợp lệ !",
                        item.STenChucNang.ToLower(), item.INamCanCu));
            }
            else
            {
                item.IIdCanCu = _dicCauHinhCanCu[sKeyCanCu].Id;
            }

            if (_dicDonVi.ContainsKey(item.IIdMaDonVi))
            {
                item.STenDonVi = _dicDonVi[item.IIdMaDonVi].TenDonVi;
            }

            if (!_dicMucLuc.ContainsKey(item.SXauNoiMa))
            {
                rootStatus = false;
                bIsError = true;
                if (!dicErrorXauNoiMa.ContainsKey(item.SXauNoiMa))
                {
                    dicErrorXauNoiMa.Add(item.SXauNoiMa, string.Format("Chứng từ căn cứ có mục lục ngân sách {0} không hợp lệ !", item.SXauNoiMa));
                }
            }
            else
            {
                item.SMoTa = _dicMucLuc[item.SXauNoiMa].MoTa;
            }
        }

        //private void SetupDataPhanCap(NsDtdauNamChungTuChiTiet objChiTiet, ref bool rootStatus, ref Dictionary<string, string> dicErrorMucLuc)
        //{
        //    foreach (var item in objChiTiet.ListDtDauNamPhanCap)
        //    {
        //        item.Id = Guid.NewGuid();
        //        item.IIdCtdtdauNamChiTiet = objChiTiet.Id;
        //        item.IIdCtdtDauNam = objChiTiet.IIdCtdtdauNam;
        //        item.IIdMaDonVi = objChiTiet.IIdMaDonVi;
        //        item.INamLamViec = 0;
        //        item.IIdMlns = Guid.Empty;
        //        item.ImportStatus = true;
        //        ValidateNsDtDauNamPhanCap(item, ref rootStatus, ref dicErrorMucLuc);
        //        if (item.ImportStatus)
        //            _lstPhanCapInsert.Add(item);
        //    }
        //}

        private void ValidateNsDtDauNamPhanCap(NsDtdauNamPhanCap item, ref bool rootStatus, ref Dictionary<string, string> dicErrorMucLuc)
        {
            if (_dicDonVi.ContainsKey(item.IIdMaDonVi))
            {
                item.STenDonVi = _dicDonVi[item.IIdMaDonVi].TenDonVi;
            }

            if (!_dicMucLuc.ContainsKey(item.SXauNoiMa))
            {
                item.ImportStatus = false;
                bIsError = true;
                if (!dicErrorMucLuc.ContainsKey(item.SXauNoiMa))
                {
                    dicErrorMucLuc.Add(item.SXauNoiMa, string.Format("Chứng từ phân cấp có mục lục ngân sách {0} không hợp lệ !", item.SXauNoiMa));
                }
            }
            else
            {
                item.IIdMlns = _dicMucLuc[item.SXauNoiMa].MlnsId;
                item.INamLamViec = _sessionService.Current.YearOfWork;
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

        private void GetDicCauHinhCanCu()
        {
            _dicCauHinhCanCu = new Dictionary<string, NsCauHinhCanCu>();
            var data = _cauhinhCanCuService.FindByCondition(n => n.INamLamViec == _sessionService.Current.YearOfWork);
            if (data != null)
            {
                foreach (var item in data)
                {
                    string key = string.Format("{0}\t{1}\t{2}\t{3}",
                        item.IIDMaChucNang, item.INamCanCu, item.INamLamViec, item.SModule);
                    if (!_dicCauHinhCanCu.ContainsKey(key))
                        _dicCauHinhCanCu.Add(key, item);
                }
            }
        }
        #endregion
    }
}
