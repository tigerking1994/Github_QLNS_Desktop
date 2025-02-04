using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.Estimate.DivisionEstimate;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate
{
    public class DivisionEstimateImportJsonViewModel : ViewModelBase
    {
        public override string Name => "PHÂN BỔ DỰ TOÁN";
        public override string Description => "Import phân bổ dự toán";
        public override Type ContentType => typeof(DivisionEstimateImportJson);

        #region Private
        private readonly ISessionService _sessionService;
        private readonly IImportExcelService _importService;
        private readonly INsDonViService _donviService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IConfiguration _configuration;
        private readonly ICauHinhCanCuService _cauhinhCanCuService;
        private readonly INsDtChungTuService _chungTuService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsDtNhanPhanBoMapService _dtChungTuMapService;
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

        private readonly List<NsDtChungTuChiTiet> _lstDetailInsert = new List<NsDtChungTuChiTiet>();
        private readonly List<NsDtChungTuChiTiet> _lstDetailNhanInsert = new List<NsDtChungTuChiTiet>();
        private readonly List<NsDtNhanPhanBoMap> _lstMapInsert = new List<NsDtNhanPhanBoMap>();
        private Guid _idNhan;
        private Guid _idPhanBo;
        private int _soChungTuIndex;
        private bool bIsError = false;
        #endregion

        #region ItemsValidate
        Dictionary<string, DonVi> _dicDonVi;
        Dictionary<int, string> _dicNganSachSuDung;
        Dictionary<string, NsMucLucNganSach> _dicMucLuc;
        Dictionary<string, NsCauHinhCanCu> _dicCauHinhCanCu;
        Dictionary<int, List<NsDtChungTuChiTiet>> _dicDetail;
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

        private ObservableCollection<NsDtChungTu> _items;
        public ObservableCollection<NsDtChungTu> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private NsDtChungTu _selectedItems;
        public NsDtChungTu SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }

        private ObservableCollection<NsDtChungTuChiTiet> _details;
        public ObservableCollection<NsDtChungTuChiTiet> Details
        {
            get => _details;
            set => SetProperty(ref _details, value);
        }

        private NsDtChungTuChiTiet _selectedDetail;
        public NsDtChungTuChiTiet SelectedDetail
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

        public DivisionEstimateImportJsonViewModel(
            ISessionService sessionService,
            IImportExcelService importService,
            INsDonViService donviService,
            INsMucLucNganSachService mucLucNganSachService,
            IConfiguration configuration,
            ICauHinhCanCuService cauhinhCanCuService,
            IImpHistoryService impHistoryService,
            ISktSoLieuChungTuService sktSoLieuChungTuService,
            ISktSoLieuService sktSoLieuService,
            ISktSoLieuChiTietCanCuDataService sktSoLieuChiTietCanCuDataService,
            ISoLieuChiTietPhanCapService soLieuChiTietPhanCapService,
            INsDtChungTuService estimationService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsDtNhanPhanBoMapService dtChungTuMapService,
            ILog logger,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _importService = importService;
            _donviService = donviService;
            _mucLucNganSachService = mucLucNganSachService;
            _configuration = configuration;
            _cauhinhCanCuService = cauhinhCanCuService;
            _impHistoryService = impHistoryService;
            _sktSoLieuChungTuService = sktSoLieuChungTuService;
            _sktSoLieuService = sktSoLieuService;
            _chungTuService = estimationService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _dtChungTuMapService = dtChungTuMapService;
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

                _dicDetail = new Dictionary<int, List<NsDtChungTuChiTiet>>();

                _lstDetailInsert.Clear();
                _lstDetailNhanInsert.Clear();
                _lstMapInsert.Clear();

                if (string.IsNullOrEmpty(FilePath) || !File.Exists(FilePath))
                    return;
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
            List<NsDtChungTu> lstData = _importService.GetDataJson<NsDtChungTu>(FilePath);
            if (lstData == null)
                return;
            SetupDataDtChungTuNew(lstData);
            if (bIsError)
            {
                MessageBoxHelper.Error(Resources.ImportError);
            }
        }

        private void OnShowDetail()
        {
            if (SelectedItems == null || Items.IndexOf(SelectedItems) == -1)
            {
                _details = new ObservableCollection<NsDtChungTuChiTiet>();
            }
            else
            {
                if (_dicDetail.ContainsKey(Items.IndexOf(SelectedItems)))
                    _details = new ObservableCollection<NsDtChungTuChiTiet>(_dicDetail[Items.IndexOf(SelectedItems)]);
                else
                    _details = new ObservableCollection<NsDtChungTuChiTiet>();
            }
            OnPropertyChanged(nameof(Details));
        }

        private int GetNextSoChungTuIndex(int loai)
        {
            System.Linq.Expressions.Expression<Func<NsDtChungTu, bool>> predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.ILoai == loai);

            int soChungTuIndex = _chungTuService.FindNextSoChungTuIndex(predicate);
            return soChungTuIndex;
        }

        private void BeforeSave()
        {
            foreach (NsDtChungTu item in Items)
            {
                item.FTongTuChi = _lstDetailInsert.Where(x => x.IIdDtchungTu == item.Id).Sum(x => x.FTuChi);
                item.FTongHienVat = _lstDetailInsert.Where(x => x.IIdDtchungTu == item.Id).Sum(x => x.FHienVat);
                item.FTongDuPhong = _lstDetailInsert.Where(x => x.IIdDtchungTu == item.Id).Sum(x => x.FDuPhong);
                item.FTongHangMua = _lstDetailInsert.Where(x => x.IIdDtchungTu == item.Id).Sum(x => x.FHangMua);
                item.FTongHangNhap = _lstDetailInsert.Where(x => x.IIdDtchungTu == item.Id).Sum(x => x.FHangNhap);
                item.FTongPhanCap = _lstDetailInsert.Where(x => x.IIdDtchungTu == item.Id).Sum(x => x.FPhanCap);
                item.FTongTonKho = _lstDetailInsert.Where(x => x.IIdDtchungTu == item.Id).Sum(x => x.FTonKho);
            }

            foreach (NsDtChungTu item in Items.SelectMany(x => x.ListVoucher).ToList())
            {
                item.FTongTuChi = _lstDetailNhanInsert.Where(x => x.IIdDtchungTu == item.Id).Sum(x => x.FTuChi);
                item.FTongHienVat = _lstDetailNhanInsert.Where(x => x.IIdDtchungTu == item.Id).Sum(x => x.FHienVat);
                item.FTongDuPhong = _lstDetailNhanInsert.Where(x => x.IIdDtchungTu == item.Id).Sum(x => x.FDuPhong);
                item.FTongHangMua = _lstDetailNhanInsert.Where(x => x.IIdDtchungTu == item.Id).Sum(x => x.FHangMua);
                item.FTongHangNhap = _lstDetailNhanInsert.Where(x => x.IIdDtchungTu == item.Id).Sum(x => x.FHangNhap);
                item.FTongPhanCap = _lstDetailNhanInsert.Where(x => x.IIdDtchungTu == item.Id).Sum(x => x.FPhanCap);
                item.FTongTonKho = _lstDetailNhanInsert.Where(x => x.IIdDtchungTu == item.Id).Sum(x => x.FTonKho);
            }

            _chungTuService.BulkInsert(Items.ToList());
            _chungTuService.BulkInsert(Items.SelectMany(x => x.ListVoucher).ToList());

            if (_lstMapInsert.Any())
            {
                _dtChungTuMapService.Save(_lstMapInsert);
                _dtChungTuMapService.RemoveDuplicate();
            }

            if (_lstDetailInsert.Any())
            {
                _dtChungTuChiTietService.BulkInsert(_lstDetailInsert);
            }

            if (_lstDetailNhanInsert.Any())
            {
                _dtChungTuChiTietService.BulkInsert(_lstDetailNhanInsert);
            }
        }

        public override void OnSave()
        {
            base.OnSave();
            if (bIsError)
            {
                MessageBoxHelper.Error(Resources.ImportError);
                return;
            }

            BackgroundWorkerHelper.Run((e, s) =>
            {
                IsLoading = true;
                BeforeSave();
            }, (e, s) =>
            {
                IsLoading = false;
                MessageBoxHelper.Info(Resources.MsgImportSuccess);
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(new object());
            });
        }

        private void OnResetData()
        {
            _fileName = string.Empty;
            _filePath = string.Empty;
            _impHistory = new ImpHistory();
            _items = new ObservableCollection<NsDtChungTu>();
            _details = new ObservableCollection<NsDtChungTuChiTiet>();
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
        //private void SetupDataDtChungTu(List<NsDtChungTu> lstItems)
        //{
        //    int i = 0;
        //    int soChungTuIndex = GetNextSoChungTuIndex(SoChungTuType.EstimateDivision);
        //    _soChungTuIndex = GetNextSoChungTuIndex(SoChungTuType.ReceiveEstimate);
        //    foreach (var item in lstItems)
        //    {
        //        item.Id = Guid.NewGuid();
        //        item.ISoChungTuIndex = soChungTuIndex;
        //        item.SSoChungTu = "CT-" + soChungTuIndex.ToString("D3");
        //        item.IIdDotNhan = "";

        //        bool rootStatus = true;
        //        if (item.ListDetailChiTiet != null && item.ListDetailChiTiet.Any())
        //        {
        //            SetupDataDtChungTuChiTiet(i, item, ref rootStatus);
        //        }

        //        item.ImportStatus = rootStatus;
        //        ValidateNsDtChungTu(item, i);
        //        ++i;
        //        soChungTuIndex++;
        //    }
        //    _items = new ObservableCollection<NsDtChungTu>(lstItems);
        //    _details = new ObservableCollection<NsDtChungTuChiTiet>();
        //    OnPropertyChanged(nameof(Items));
        //    OnPropertyChanged(nameof(Details));
        //}

        private void SetupDataDtChungTuNew(List<NsDtChungTu> lstItems)
        {
            int i = 0;
            int soChungTuIndex = GetNextSoChungTuIndex(SoChungTuType.EstimateDivision);
            _soChungTuIndex = GetNextSoChungTuIndex(SoChungTuType.ReceiveEstimate);
            foreach (NsDtChungTu item in lstItems)
            {
                item.Id = Guid.NewGuid();
                item.ISoChungTuIndex = soChungTuIndex;
                item.SSoChungTu = "CT-" + soChungTuIndex.ToString("D3");
                item.IIdDotNhan = "";
                item.ILoaiDuToan = item.ILoaiDuToan == 4 ? 3 : item.ILoaiDuToan;
                item.INamLamViec = _sessionService.Current.YearOfWork;

                bool rootStatus = true;
                if (item.ListDetailChiTiet != null && item.ListDetailChiTiet.Any())
                {
                    SetupDataDtChungTuChiTietNew(i, item, ref rootStatus);
                    //SetupDataDtChungTuChiTiet(i, item, ref rootStatus);
                }

                item.ImportStatus = rootStatus;
                ValidateNsDtChungTu(item, i);
                ++i;
                soChungTuIndex++;
            }
            _items = new ObservableCollection<NsDtChungTu>(lstItems);
            _details = new ObservableCollection<NsDtChungTuChiTiet>();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(Details));
        }

        private void ValidateNsDtChungTu(NsDtChungTu item, int index)
        {
            //if (!_dicDonVi.ContainsKey(item.IIdMaDonVi))
            //{
            //    _errorItems.Add(new ImportErrorItem()
            //    {
            //        ColumnName = "Mã đơn vị",
            //        Row = index,
            //        Error = String.Format(Resources.MsgErrorItemNotFound, "Mã đơn vị")
            //    });
            //    item.ImportStatus = false;
            //    bIsError = true;
            //}

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

        //private void SetupDataDtChungTuChiTiet(int pageIndex, NsDtChungTu objParent, ref bool rootStatus)
        //{
        //    int i = 0;
        //    Dictionary<string, string> dicErrorMucLuc = new Dictionary<string, string>();

        //    foreach (var z in objParent.ListDetailVoucher)
        //    {
        //        z.FTuChi = 0;
        //        z.FDuPhong = 0;
        //        z.FHangMua = 0;
        //        z.FHangNhap = 0;
        //        z.FHienVat = 0;
        //        z.FPhanCap = 0;
        //    }

        //    foreach (var item in objParent.ListVoucher)
        //    {
        //        var newId = Guid.NewGuid();
        //        foreach (var element in objParent.ListDetailVoucher)
        //        {
        //            if (element.IIdDtchungTu == item.Id)
        //            {
        //                element.IIdDtchungTu = newId;
        //            }
        //        }
        //        foreach (var element in objParent.ListDetailChiTiet)
        //        {
        //            if (element.IIdCtduToanNhan == item.Id)
        //            {
        //                element.IIdCtduToanNhan = newId;
        //            }
        //        }
        //        foreach (var element in objParent.ListVoucherMap)
        //        {
        //            if (element.IIdCtduToanNhan == item.Id)
        //            {
        //                element.IIdCtduToanNhan = newId;
        //            }
        //        }
        //        item.Id = newId;
        //        item.SSoChungTu = "DT-" + _soChungTuIndex.ToString("D3");
        //        item.ISoChungTuIndex = _soChungTuIndex;
        //        _soChungTuIndex++;
        //    }
        //    objParent.IIdDotNhan = string.Join(",", objParent.ListVoucher.Select(x => x.Id));

        //    foreach (var item in objParent.ListVoucherMap)
        //    {
        //        item.Id = Guid.NewGuid();
        //        item.IIdCtduToanPhanBo = objParent.Id;
        //        _lstMapInsert.Add(item);
        //    }

        //    foreach (var item in objParent.ListDetailChiTiet)
        //    {
        //        item.Id = Guid.NewGuid();
        //        item.IIdDtchungTu = objParent.Id;
        //        //item.IIdCtdtdauNam = objParent.Id;
        //        //item.STenDonVi = null;
        //        item.SMoTa = null;
        //        //item.IIdMaDonVi = objParent.IIdMaDonVi;
        //        item.IIdMaNguonNganSach = objParent.IIdMaNguonNganSach ?? 0;
        //        //item.ILoaiChungTu = (objParent.ILoaiChungTu ?? 0).ToString();
        //        item.INamLamViec = objParent.INamLamViec ?? 0;
        //        item.INamNganSach = objParent.INamNganSach ?? 0;
        //        item.ImportStatus = true;
        //        ValidateNsDtChungTuChiTiet(pageIndex, item, i);
        //        _lstDetailInsert.Add(item);
        //        ++i;
        //    }

        //    foreach (var y in objParent.ListDetailVoucher)
        //    {
        //        y.Id = Guid.NewGuid();
        //        foreach (var item in objParent.ListDetailChiTiet)
        //        {
        //            if (y.SXauNoiMa == item.SXauNoiMa && y.IIdDtchungTu == item.IIdCtduToanNhan)
        //            {
        //                y.FTuChi += item.FTuChi;
        //                y.FDuPhong += item.FDuPhong;
        //                y.FHangMua += item.FHangMua;
        //                y.FHangNhap += item.FHangNhap;
        //                y.FHienVat += item.FHienVat;
        //                y.FPhanCap += item.FPhanCap;
        //            }
        //        }
        //        _lstDetailNhanInsert.Add(y);
        //    }

        //    if (!_dicDetail.ContainsKey(pageIndex))
        //        _dicDetail.Add(pageIndex, objParent.ListDetailChiTiet);

        //    if (dicErrorMucLuc.Count != 0)
        //    {
        //        rootStatus = false;
        //        _errorItems.AddRange(dicErrorMucLuc.Select(n => new ImportErrorItem()
        //        {
        //            ColumnName = "Mục lục ngân sách",
        //            Row = pageIndex,
        //            Error = n.Value
        //        }));
        //    }
        //}

        private void SetupDataDtChungTuChiTietNew(int pageIndex, NsDtChungTu objParent, ref bool rootStatus)
        {
            int i = 0;
            Dictionary<string, string> dicErrorMucLuc = new Dictionary<string, string>();

            objParent.ListVoucherMap.Clear();
            objParent.ListDetailVoucher.Clear();
            var tempMap = objParent.ListDetailChiTiet.Select(t => new { t.IIdDtchungTu, t.IIdCtduToanNhan }).Distinct().ToList();

            foreach (var element in tempMap)
            {
                NsDtNhanPhanBoMap tempVoucherMap = new NsDtNhanPhanBoMap();
                tempVoucherMap.Id = Guid.NewGuid();
                tempVoucherMap.IIdCtduToanNhan = element.IIdCtduToanNhan ?? Guid.Empty;
                tempVoucherMap.IIdCtduToanPhanBo = objParent.Id;
                objParent.ListVoucherMap.Add(tempVoucherMap);
            }

            objParent.ListVoucher = objParent.ListVoucher.Where(t => tempMap.Any(x => x.IIdCtduToanNhan == t.Id)).Distinct().ToList();

            var tempDetailVoucher = objParent.ListDetailChiTiet.Select(t => new { t.SXauNoiMa, t.IIdCtduToanNhan }).Distinct().ToList();

            for (int index = 0; index < tempDetailVoucher.Count; index++)
            {
                var item = tempDetailVoucher[index];

                NsDtChungTuChiTiet addItem = new NsDtChungTuChiTiet();
                addItem = objParent.ListDetailChiTiet.Where(t => t.SXauNoiMa == item.SXauNoiMa && t.IIdCtduToanNhan == item.IIdCtduToanNhan).First().Clone();
                addItem.IIdDtchungTu = item.IIdCtduToanNhan;
                addItem.IIdCtduToanNhan = null;
                addItem.IPhanCap = 0;
                addItem.IIdMaDonVi = _sessionService.Current.IdDonVi;
                objParent.ListDetailVoucher.Add(addItem);
            }

            foreach (NsDtChungTuChiTiet z in objParent.ListDetailVoucher)
            {
                z.FTuChi = 0;
                z.FDuPhong = 0;
                z.FHangMua = 0;
                z.FHangNhap = 0;
                z.FHienVat = 0;
                z.FPhanCap = 0;
            }

            foreach (NsDtChungTu item in objParent.ListVoucher)
            {
                Guid newId = Guid.NewGuid();
                int nam = _sessionService.Current.YearOfWork;
                item.INamLamViec = nam;

                foreach (NsDtChungTuChiTiet element in objParent.ListDetailVoucher)
                {
                    if (element.IIdDtchungTu == item.Id)
                    {
                        element.IIdDtchungTu = newId;
                        element.INamLamViec = nam;
                    }
                }
                foreach (NsDtChungTuChiTiet element in objParent.ListDetailChiTiet)
                {
                    if (element.IIdCtduToanNhan == item.Id)
                    {
                        element.IIdCtduToanNhan = newId;
                        element.INamLamViec = nam;
                    }
                }
                foreach (NsDtNhanPhanBoMap element in objParent.ListVoucherMap)
                {
                    if (element.IIdCtduToanNhan == item.Id)
                    {
                        element.IIdCtduToanNhan = newId;
                    }
                }
                item.Id = newId;
                item.SSoChungTu = "DT-" + _soChungTuIndex.ToString("D3");
                item.ISoChungTuIndex = _soChungTuIndex;
                _soChungTuIndex++;
            }
            objParent.IIdDotNhan = string.Join(",", objParent.ListVoucher.Select(x => x.Id));
            objParent.SDsidMaDonVi = string.Join(",", objParent.ListDetailChiTiet.Select(x => x.IIdMaDonVi).Distinct());

            foreach (NsDtNhanPhanBoMap item in objParent.ListVoucherMap)
            {
                _lstMapInsert.Add(item);
            }

            foreach (NsDtChungTuChiTiet item in objParent.ListDetailChiTiet)
            {
                item.Id = Guid.NewGuid();
                item.IIdDtchungTu = objParent.Id;
                item.SMoTa = null;
                item.IIdMaNguonNganSach = objParent.IIdMaNguonNganSach ?? 0;
                item.INamLamViec = objParent.INamLamViec ?? 0;
                item.INamNganSach = objParent.INamNganSach ?? 0;
                item.ImportStatus = true;
                ValidateNsDtChungTuChiTiet(pageIndex, item, i);
                _lstDetailInsert.Add(item);
                ++i;
            }

            foreach (NsDtChungTuChiTiet y in objParent.ListDetailVoucher)
            {
                y.Id = Guid.NewGuid();
                foreach (NsDtChungTuChiTiet item in objParent.ListDetailChiTiet)
                {
                    if (y.SXauNoiMa == item.SXauNoiMa && y.IIdDtchungTu == item.IIdCtduToanNhan)
                    {
                        y.FTuChi += item.FTuChi;
                        y.FDuPhong += item.FDuPhong;
                        y.FHangMua += item.FHangMua;
                        y.FHangNhap += item.FHangNhap;
                        y.FHienVat += item.FHienVat;
                        y.FPhanCap += item.FPhanCap;
                    }
                }
                _lstDetailNhanInsert.Add(y);
            }

            if (!_dicDetail.ContainsKey(pageIndex))
                _dicDetail.Add(pageIndex, objParent.ListDetailChiTiet);

            if (dicErrorMucLuc.Count != 0)
            {
                rootStatus = false;
                _errorItems.AddRange(dicErrorMucLuc.Select(n => new ImportErrorItem()
                {
                    ColumnName = "Mục lục ngân sách",
                    Row = pageIndex,
                    Error = n.Value
                }));
            }
        }

        private void ValidateNsDtChungTuChiTiet(int pageIndex, NsDtChungTuChiTiet item, int index)
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

        //private void SetUpDataNsDtChungTuChiTietCanCu(int iPageIndex, NsDtChungTu objChungTu, ref bool rootStatus)
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
        //        ValidateNsDtChungTuChiTietCanCu(item, ref rootStatus, ref dicErrorCanCu, ref dicErrorXauNoiMa);
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

        //private void ValidateNsDtChungTuChiTietCanCu(JsonNsDtChungTuChiTietCanCuQuery item, ref bool rootStatus,
        //    ref Dictionary<string, string> dicErrorCanCu, ref Dictionary<string, string> dicErrorXauNoiMa)
        //{
        //    string sKeyCanCu = string.Format("{0}\t{1}\t{2}\t{3}", item.IIDMaChucNang, item.INamCanCu, item.INamLamViec, item.SModule);
        //    string sKeyError = string.Format("{0}\t{1}", item.IIDMaChucNang, item.INamCanCu);
        //    if (!_dicCauHinhCanCu.ContainsKey(sKeyCanCu))
        //    {
        //        rootStatus = false;
        //        bIsError = true;
        //        if (!dicErrorCanCu.ContainsKey(sKeyError))
        //            dicErrorCanCu.Add(sKeyError, string.Format("Chứng từ căn cứ có căn cứ {0} năm {1} không hợp lệ !",
        //                item.STenChucNang.ToLower(), item.INamCanCu));
        //    }
        //    else
        //    {
        //        item.IIdCanCu = _dicCauHinhCanCu[sKeyCanCu].Id;
        //    }

        //    if (_dicDonVi.ContainsKey(item.IIdMaDonVi))
        //    {
        //        item.STenDonVi = _dicDonVi[item.IIdMaDonVi].TenDonVi;
        //    }

        //    if (!_dicMucLuc.ContainsKey(item.SXauNoiMa))
        //    {
        //        rootStatus = false;
        //        bIsError = true;
        //        if (!dicErrorXauNoiMa.ContainsKey(item.SXauNoiMa))
        //        {
        //            dicErrorXauNoiMa.Add(item.SXauNoiMa, string.Format("Chứng từ căn cứ có mục lục ngân sách {0} không hợp lệ !", item.SXauNoiMa));
        //        }
        //    }
        //    else
        //    {
        //        item.SMoTa = _dicMucLuc[item.SXauNoiMa].MoTa;
        //    }
        //}

        //private void SetupDataPhanCap(NsDtChungTuChiTiet objChiTiet, ref bool rootStatus, ref Dictionary<string, string> dicErrorMucLuc)
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

        //private void ValidateNsDtDauNamPhanCap(NsDtdauNamPhanCap item, ref bool rootStatus, ref Dictionary<string, string> dicErrorMucLuc)
        //{
        //    if (_dicDonVi.ContainsKey(item.IIdMaDonVi))
        //    {
        //        item.STenDonVi = _dicDonVi[item.IIdMaDonVi].TenDonVi;
        //    }

        //    if (!_dicMucLuc.ContainsKey(item.SXauNoiMa))
        //    {
        //        item.ImportStatus = false;
        //        bIsError = true;
        //        if (!dicErrorMucLuc.ContainsKey(item.SXauNoiMa))
        //        {
        //            dicErrorMucLuc.Add(item.SXauNoiMa, string.Format("Chứng từ phân cấp có mục lục ngân sách {0} không hợp lệ !", item.SXauNoiMa));
        //        }
        //    }
        //    else
        //    {
        //        item.IIdMlns = _dicMucLuc[item.SXauNoiMa].MlnsId;
        //        item.INamLamViec = _sessionService.Current.YearOfWork;
        //    }
        //}

        private void GetDicDonVi()
        {
            IEnumerable<DonVi> lstData = _donviService.FindByNamLamViec(_sessionService.Current.YearOfWork);
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
            IEnumerable<NsMucLucNganSach> data = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork);
            if (data != null)
            {
                foreach (NsMucLucNganSach item in data)
                    if (!_dicMucLuc.ContainsKey(item.XauNoiMa))
                        _dicMucLuc.Add(item.XauNoiMa, item);
            }
        }

        private void GetDicCauHinhCanCu()
        {
            _dicCauHinhCanCu = new Dictionary<string, NsCauHinhCanCu>();
            IEnumerable<NsCauHinhCanCu> data = _cauhinhCanCuService.FindByCondition(n => n.INamLamViec == _sessionService.Current.YearOfWork);
            if (data != null)
            {
                foreach (NsCauHinhCanCu item in data)
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
