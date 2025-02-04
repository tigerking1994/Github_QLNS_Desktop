using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using AutoMapper;
using FlexCel.XlsAdapter;
using log4net;
using MaterialDesignThemes.Wpf;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKForexContractInfo.ImportHopDongNgoaiThuong
{
    public class ImportHopDongNgoaiThuongViewModel : ViewModelBase
    {
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly ISessionService _sessionService;
        private ILog _logger;
        private readonly IImportExcelService _importService;
        private readonly INhDaHopDongService _iNhDaHopDongService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(View.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTForexContractInfo.ImportHopDongNgoaiThuong.ImportHopDongNgoaiThuong);
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private ObservableCollection<HopDongNgoaiThuongImportModel> _HopDongNgoaiThuongImportModels;
        public ObservableCollection<HopDongNgoaiThuongImportModel> HopDongNgoaiThuongImportModels
        {
            get => _HopDongNgoaiThuongImportModels;
            set => SetProperty(ref _HopDongNgoaiThuongImportModels, value);
        }

        private NhDaHopDongModel _seletedHopDong;
        public NhDaHopDongModel SeletedHopDong
        {
            get => _seletedHopDong;
            set => SetProperty(ref _seletedHopDong, value);
        }
        public ObservableCollection<NhDaHopDongModel> _itemsHopDongNgoaiThuong;
        public ObservableCollection<NhDaHopDongModel> ItemsHopDongNgoaiThuong
        {
            get => _itemsHopDongNgoaiThuong;
            set => SetProperty(ref _itemsHopDongNgoaiThuong, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (HopDongNgoaiThuongImportModels.Count > 0)
                    return !HopDongNgoaiThuongImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }
        private List<ImportErrorItem> lstErHopDong = new List<ImportErrorItem>();

        private Dictionary<string, NhDmLoaiHopDong> _dicLoaiHopDong;
        private List<NhDaHopDongModel> lstHopDong;
        public ForexContractInfoDialogViewModel ForexContractInfoDialogViewModel { get; set; }
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorHopDongCommand { get; }
        public RelayCommand HieuChinhCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public ImportHopDongNgoaiThuongViewModel(ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IImportExcelService importService,
            INhDaHopDongService iNhDmHopDongService,
            INhDaHopDongService nhDaHopDongService,
            ForexContractInfoDialogViewModel forexContractInfoDialogViewModel)
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _importService = importService;
            _iNhDaHopDongService = iNhDmHopDongService;
            _nhDaHopDongService = nhDaHopDongService;

            ForexContractInfoDialogViewModel = forexContractInfoDialogViewModel;
            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            ShowErrorHopDongCommand = new RelayCommand(obj => ShowErrorHopDong());
            HieuChinhCommand = new RelayCommand(obj => OnHieuChinh());
            ShowErrorCommand = new RelayCommand(obj => ShowError());
        }
        private void OnHieuChinh()
        {
            if (SeletedHopDong != null)
            {
                ForexContractInfoDialogViewModel.IsDetail = false;
                ForexContractInfoDialogViewModel.Model = SeletedHopDong;
                ForexContractInfoDialogViewModel.IsHieuChinhImport = true;
                ForexContractInfoDialogViewModel.SavedAction = obj =>
                {
                    SeletedHopDong.ImportStatus = true;
                    ItemsHopDongNgoaiThuong = new ObservableCollection<NhDaHopDongModel>(ItemsHopDongNgoaiThuong);
                };
                ForexContractInfoDialogViewModel.Init();
                ForexContractInfoDialogViewModel.ShowDialog();
            }
        }
        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            OnLoadLoaiHopDong();
            OnResetData();
        }

        private void OnResetData()
        {
            _fileName = string.Empty;
            HopDongNgoaiThuongImportModels = new ObservableCollection<HopDongNgoaiThuongImportModel>();
            ItemsHopDongNgoaiThuong = new ObservableCollection<NhDaHopDongModel>();
            _importErrors = new List<ImportErrorItem>();
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void OnUploadFile()
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

            FileName = openFileDialog.FileName;

            XlsFile xls = new XlsFile(false);
            xls.Open(FileName);
            xls.ActiveSheet = 1;
        }

        private void ShowErrorHopDong()
        {
            int rowIndex = ItemsHopDongNgoaiThuong.IndexOf(SeletedHopDong);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnProcessFile()
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(FileName))
            {
                message = Resources.ErrorFileEmpty;
                goto ShowError;
            }
        ShowError:
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                //lấy thông tin thuế tncn
                ImportResult<HopDongNgoaiThuongImportModel> dataImport = _importService.ProcessData<HopDongNgoaiThuongImportModel>(FileName);
                HopDongNgoaiThuongImportModels = new ObservableCollection<HopDongNgoaiThuongImportModel>(dataImport.Data);

                lstErHopDong = new List<ImportErrorItem>();
                if (dataImport.ImportErrors.Count > 0)
                {
                    lstErHopDong.AddRange(dataImport.ImportErrors);
                }

                if (HopDongNgoaiThuongImportModels == null || HopDongNgoaiThuongImportModels.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (HopDongNgoaiThuongImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                ItemsHopDongNgoaiThuong = new ObservableCollection<NhDaHopDongModel>(ConvertData(HopDongNgoaiThuongImportModels.ToList()));
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnSaveData()
        {
            try
            {
                var lstModel = ItemsHopDongNgoaiThuong.Where(x => x.ImportStatus);
                var entities = _mapper.Map<List<NhDaHopDong>>(lstModel);
                foreach (var item in entities)
                {
                    item.BIsActive = true;
                    item.BIsGoc = true;
                    item.ILoai = 1;
                    item.IThuocMenu = 7;
                }
                _iNhDaHopDongService.AddRange(entities);
                MessageBoxHelper.Info("Lưu dữ liệu thành công.");
                SavedAction?.Invoke(_mapper.Map<IncomeTaxModel>(new IncomeTaxModel()));
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        private List<NhDaHopDongModel> ConvertData(List<HopDongNgoaiThuongImportModel> importModels)
        {
            List<NhDaHopDongModel> results = new List<NhDaHopDongModel>();
            int i = 0;
            foreach (var import in importModels)
            {
                NhDaHopDongModel it = new NhDaHopDongModel();
                DateTime dDate;
                it.SSoHopDong = import.SoHopDong;
                it.STenHopDong = import.TenHopDong;
                it.DNgayHopDong = !string.IsNullOrEmpty(import.NgayBanHanh) && DateTime.TryParse(import.NgayBanHanh, out dDate)
                   ? DateTime.Parse(import.NgayBanHanh, CultureInfo.CreateSpecificCulture("vi-VN"))
                   : (DateTime?)null;
                it.DKhoiCongDuKien = !string.IsNullOrEmpty(import.SKhoiCong) && DateTime.TryParse(import.SKhoiCong, out dDate)
                   ? DateTime.Parse(import.SKhoiCong, CultureInfo.CreateSpecificCulture("vi-VN"))
                   : (DateTime?)null;
                it.DKetThucDuKien = !string.IsNullOrEmpty(import.SKetThuc) && DateTime.TryParse(import.SKetThuc, out dDate)
                   ? DateTime.Parse(import.SKetThuc, CultureInfo.CreateSpecificCulture("vi-VN"))
                   : (DateTime?)null;

                it.FGiaTriUsd = import.SGiaTriUSD + "" != "" ? Convert.ToDouble(import.SGiaTriUSD) : 0;
                it.FGiaTriVnd = import.SGiaTriVND + "" != "" ? Convert.ToDouble(import.SGiaTriVND) : 0;
                it.FGiaTriNgoaiTeKhac = import.SGiaTriNgoaiTeKhac + "" != "" ? Convert.ToDouble(import.SGiaTriNgoaiTeKhac) : 0;
                it.FGiaTriHopDongUSD = import.SGiaTriUSD + "" != "" ? Convert.ToDouble(import.SGiaTriUSD) : 0;
                it.FGiaTriHopDongVND = import.SGiaTriVND + "" != "" ? Convert.ToDouble(import.SGiaTriVND) : 0;
                it.FGiaTriHopDongNgoaiTeKhac = import.SGiaTriNgoaiTeKhac + "" != "" ? Convert.ToDouble(import.SGiaTriNgoaiTeKhac) : 0;
                if (import.SMaLoaiHopDong != null && _dicLoaiHopDong.ContainsKey(import.SMaLoaiHopDong))
                {
                    it.IIdLoaiHopDongId = _dicLoaiHopDong[import.SMaLoaiHopDong].Id;
                    it.SMaLoaiHopDong = _dicLoaiHopDong[import.SMaLoaiHopDong].SMaLoaiHopDong;
                }

                ++i;
                var listError = ValidateItem(import, i);
                if (results.Any(x => x.SSoHopDong == import.SoHopDong))
                {
                    listError.Add(new ImportErrorItem()
                    {
                        ColumnName = "Số hợp đồng",
                        Error = string.Format(Resources.MsgErrorDataExist, "số hợp đồng"),
                        Row = i
                    });
                }
                if (listError.Count > 0)
                {
                    lstErHopDong.AddRange(listError);
                    import.ImportStatus = false;
                    import.IsWarning = true;
                }

                it.IsWarning = import.IsWarning;
                it.ImportStatus = import.ImportStatus;
                results.Add(it);
            }
            return results;
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
        private void OnLoadLoaiHopDong()
        {
            var lst = _nhDaHopDongService.FindAllHopDongNgoaiThuong(7);
            lstHopDong = _mapper.Map<List<NhDaHopDongModel>>(lst);
            _dicLoaiHopDong = new Dictionary<string, NhDmLoaiHopDong>();
            var datas = _nhDaHopDongService.GetAllLoaiHopDong();
            if (datas == null) return;
            foreach (var item in datas)
            {
                if (!_dicLoaiHopDong.ContainsKey(item.SMaLoaiHopDong))
                    _dicLoaiHopDong.Add(item.SMaLoaiHopDong, item);
            }
        }
        private void ShowError()
        {
            try
            {
                var errors = new HashSet<string>();
                int rowIndex = ItemsHopDongNgoaiThuong.IndexOf(SeletedHopDong);
                errors = lstErHopDong.Where(x => x.Row == (rowIndex + 1)).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
                System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private List<ImportErrorItem> ValidateItem(HopDongNgoaiThuongImportModel item, int rowIndex)
        {
            DateTime dDate;
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                if (string.IsNullOrEmpty(item.SoHopDong))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Số hợp đồng",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "số hợp đồng"),
                        Row = rowIndex
                    });
                }

                if (lstHopDong.Any(x => x.SSoHopDong == item.SoHopDong))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Số hợp đồng",
                        Error = string.Format(Resources.MsgErrorDataExist, "số hợp đồng"),
                        Row = rowIndex
                    });
                }

                if (string.IsNullOrEmpty(item.TenHopDong))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Tên hợp đồng",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "tên hợp đồng"),
                        Row = rowIndex
                    });
                }

                if (string.IsNullOrEmpty(item.NgayBanHanh) || !DateTime.TryParse(item.NgayBanHanh, out dDate))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Ngày ban hành",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "Ngày ban hành"),
                        Row = rowIndex
                    });
                }

                if (!_dicLoaiHopDong.ContainsKey(item.SMaLoaiHopDong))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Mã loại hợp đồng",
                        Error = string.Format(Resources.MsgErrorDataNotFound, "mã loại hợp đồng"),
                        Row = rowIndex
                    });
                }

                if (!string.IsNullOrEmpty(item.SKhoiCong) && DateTime.TryParse(item.SKhoiCong, out dDate) && !string.IsNullOrEmpty(item.SKetThuc) && DateTime.TryParse(item.SKetThuc, out dDate))
                {
                    var DKhoiCongDuKien = DateTime.Parse(item.SKhoiCong, CultureInfo.CreateSpecificCulture("vi-VN"));
                    var DKetThucDuKien = DateTime.Parse(item.SKetThuc, CultureInfo.CreateSpecificCulture("vi-VN"));
                    if (DKhoiCongDuKien > DKetThucDuKien)
                    {
                        errors.Add(new ImportErrorItem()
                        {
                            ColumnName = "Thời gian thực hiện",
                            Error = string.Format(Resources.MsgDateTimeFromSmall, ""),
                            Row = rowIndex
                        });
                    }
                }

                return errors;

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);

                return new List<ImportErrorItem>();
            }
        }
    }
}
