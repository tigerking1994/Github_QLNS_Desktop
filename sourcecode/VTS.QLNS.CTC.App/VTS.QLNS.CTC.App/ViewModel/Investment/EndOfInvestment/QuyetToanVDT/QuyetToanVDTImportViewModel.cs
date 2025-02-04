using AutoMapper;
using FlexCel.XlsAdapter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
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

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.QuyetToanVDT
{
    public class QuyetToanVDTImportViewModel : ViewModelBase
    {
        #region Header
        public VdtQtBcquyetToanNienDoModel DataModel { get; set; }
        public string sVonTamUngChuaThuHoi => string.Format("Số vốn tạm ứng theo chế độ chưa thu hồi của các năm trước nộp điều chỉnh giảm trong năm {0}", DataModel.INamKeHoach);
        public string sKeHoachThanhToanVonNamNay => string.Format("Kế hoạch và thanh toán vốn đầu tư năm {0}", DataModel.INamKeHoach);
        public string sThanhToanVonTamUng => string.Format("Thanh toán KLHT của phần vốn tạm ứng theo chế độ từ KC đến hết niên độ năm trước năm {0}", DataModel.INamKeHoach);
        public string sKeHoachThanhToanVon => string.Format("Kế hoạch và thanh toán vốn đầu tư các năm trước được kéo dài thời gian thực hiện và thanh toán sang năm {0}", DataModel.INamKeHoach);
        public string sVonDaQuyetToanTrongNam => string.Format("Tổng cộng vốn đã thanh toán KLHT quyết toán trong năm {0}", DataModel.INamKeHoach);
        public string sLuyKeVonDaThanhToan => string.Format("Luỹ kế số vốn đã thanh toán từ K/C đến hết năm {0}", DataModel.INamKeHoach);
        public string sKeHoachVonNamNay => string.Format("Kế hoạch vốn đầu tư năm {0}", DataModel.INamKeHoach);
        #endregion

        public override Type ContentType => typeof(VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.QuyetToanVDT.QuyetToanVDTImport);
        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly INsDonViService _nsDonViService;
        private IImportExcelService _importService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly INsNguonNganSachService _nguonVonService;
        private readonly IMucLucNganSachService _mlNganSachService;
        private readonly IVdtQtBcQuyetToanNienDoService _service;
        private readonly IVdtDmLoaiCongTrinhService _loaicongtrinhService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private List<ImportErrorItem> _lstErrChungTuChiTiet;
        private string _fileName;
        private Dictionary<string, Guid> _dicDuAn = new Dictionary<string, Guid>();
        private Dictionary<string, Guid> _dicLoaiCongTrinh = new Dictionary<string, Guid>();
        private readonly FtpStorageService _ftpStorageService;
        #endregion

        #region Import item
        private VdtQtBcQuyetToanNienDo _objQuyetToan = new VdtQtBcQuyetToanNienDo();
        public VdtQtBcQuyetToanNienDo ObjQuyetToan
        {
            get => _objQuyetToan;
            set => SetProperty(ref _objQuyetToan, value);
        }

        private ObservableCollection<BcquyetToanNienDoVonNamChiTietImportModel> _quyetToanChiTiets;
        public ObservableCollection<BcquyetToanNienDoVonNamChiTietImportModel> QuyetToanChiTiets
        {
            get => _quyetToanChiTiets;
            set => SetProperty(ref _quyetToanChiTiets, value);
        }

        private BcquyetToanNienDoVonNamChiTietImportModel _quyetToanChiTietSelected;
        public BcquyetToanNienDoVonNamChiTietImportModel QuyetToanChiTietSelected
        {
            get => _quyetToanChiTietSelected;
            set => SetProperty(ref _quyetToanChiTietSelected, value);
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        public bool _isSelectedFile;
        public bool IsSelectedFile
        {
            get => !string.IsNullOrEmpty(_filePath);
            set => SetProperty(ref _isSelectedFile, value);
        }

        public bool _isSaveData;
        public bool IsSaveData
        {
            get => ObjQuyetToan != null && QuyetToanChiTiets != null && QuyetToanChiTiets.Count > 0 && !QuyetToanChiTiets.Any(x => !x.ImportStatus);
            set => SetProperty(ref _isSaveData, value);
        }
        #endregion

        #region Componer
        private string _sSoDeNghi;
        public string SSoDeNghi
        {
            get => _sSoDeNghi;
            set => SetProperty(ref _sSoDeNghi, value);
        }

        private DateTime? _dNgayDeNghi;
        public DateTime? DNgayDeNghi
        {
            get => _dNgayDeNghi;
            set => SetProperty(ref _dNgayDeNghi, value);
        }

        private string _iNamKeHoach;
        public string INamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }

        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set => SetProperty(ref _cbxLoaiDonViSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
        }

        private ComboboxItem _cbxNguonVonSelected;
        public ComboboxItem CbxNguonVonSelected
        {
            get => _cbxNguonVonSelected;
            set => SetProperty(ref _cbxNguonVonSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxNguonVon;
        public ObservableCollection<ComboboxItem> CbxNguonVon
        {
            get => _cbxNguonVon;
            set => SetProperty(ref _cbxNguonVon, value);
        }

        private ComboboxItem _cbxCoQuanThanhToanSelected;
        public ComboboxItem CbxCoQuanThanhToanSelected
        {
            get => _cbxCoQuanThanhToanSelected;
            set => SetProperty(ref _cbxCoQuanThanhToanSelected, value);
        }
        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }

        private ObservableCollection<ComboboxItem> _cbxCoQuanThanhToan;
        public ObservableCollection<ComboboxItem> CbxCoQuanThanhToan
        {
            get => _cbxCoQuanThanhToan;
            set => SetProperty(ref _cbxCoQuanThanhToan, value);
        }
        public bool _isBoolCheckFile;
        public bool IsBoolCheckFile
        {
            get => !string.IsNullOrEmpty(_filePath);
            set => SetProperty(ref _isBoolCheckFile, value);
        }
        #endregion

        #region RelayCommand
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand GetFileFtpCommand { get; }
        public RelayCommand DownloadFileFtpServer { get; }
        #endregion

        public QuyetToanVDTImportViewModel(
            INsDonViService nsDonViService,
            IImportExcelService importService,
            INsNguonNganSachService nguonVonService,
            IMucLucNganSachService mlNganSachService,
            IVdtQtBcQuyetToanNienDoService service,
            IVdtDmLoaiCongTrinhService loaicongtrinhService,
        ISessionService sessionService,
            FtpStorageService ftpStorageService,
            ITongHopNguonNSDauTuService tonghopService,
            IMapper mapper)
        {
            _tonghopService = tonghopService;
            _importService = importService;
            _nsDonViService = nsDonViService;
            _nguonVonService = nguonVonService;
            _mlNganSachService = mlNganSachService;
            _loaicongtrinhService = loaicongtrinhService;
            _service = service;
            _ftpStorageService = ftpStorageService;
            _sessionService = sessionService;
            _mapper = mapper;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(ShowError);
            GetFileFtpCommand = new RelayCommand(obj => OnGetFileFtpCommand());
            DownloadFileFtpServer = new RelayCommand(obj => OnDownloadFileFtpServer());
        }

        #region Event
        public override void Init()
        {
            base.Init();
            LoadComboBoxLoaiDonVi();
            LoadComboBoxNguonVon();
            LoadCoQuanThanhToan();
            IsBoolCheckFile = false;
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            _dicLoaiCongTrinh = new Dictionary<string, Guid>();
            FilePath = string.Empty;
            IsSelectedFile = false;
            CbxLoaiDonViSelected = null;
            INamKeHoach = null;
            SSoDeNghi = null;
            DNgayDeNghi = null;
            CbxNguonVonSelected = null;
            CbxCoQuanThanhToanSelected = null;
            QuyetToanChiTiets = new ObservableCollection<BcquyetToanNienDoVonNamChiTietImportModel>();
            _dicLoaiCongTrinh = new Dictionary<string, Guid>();
            var lstLoaiCongTrinh = _loaicongtrinhService.FindAll();
            if(lstLoaiCongTrinh != null)
            {
                foreach(var item in lstLoaiCongTrinh)
                {
                    if (!_dicLoaiCongTrinh.ContainsKey(item.SMaLoaiCongTrinh))
                        _dicLoaiCongTrinh.Add(item.SMaLoaiCongTrinh, item.IIdLoaiCongTrinh);
                }
            }

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(SSoDeNghi));
            OnPropertyChanged(nameof(DNgayDeNghi));
            OnPropertyChanged(nameof(CbxNguonVonSelected));
            OnPropertyChanged(nameof(CbxCoQuanThanhToanSelected));
            OnPropertyChanged(nameof(QuyetToanChiTiets));
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

            FilePath = openFileDialog.FileName;
            _fileName = openFileDialog.SafeFileName;
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnProcessFile();
        }

        /// <summary>
        /// Process
        /// </summary>
        /// <returns></returns>
        private string OnProcessFile()
        {
            _lstErrChungTuChiTiet = new List<ImportErrorItem>();
            List<string> lstError = new List<string>();

            if (string.IsNullOrEmpty(FilePath))
            {
                lstError.Add(Resources.ErrorFileEmpty);
            }

            ;
            if (!ValidateForm(ref lstError))
            {
                string sMessError = string.Join(Environment.NewLine, lstError);
                System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return sMessError;
            }

            var data = GetDataImportByFileType();
            QuyetToanChiTiets = new ObservableCollection<BcquyetToanNienDoVonNamChiTietImportModel>(data);
            if (lstError.Any())
            {
                string sMessError = string.Join(Environment.NewLine, lstError);
                System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                ObjQuyetToan = null;
            }
            ValidateChungTuChiTiet();
            IsBoolCheckFile = true;
            OnPropertyChanged(nameof(ObjQuyetToan));
            OnPropertyChanged(nameof(QuyetToanChiTiets));
            OnPropertyChanged(nameof(IsSaveData));
            return string.Empty;
        }

        private void OnResetData()
        {
            FilePath = string.Empty;
            IsSelectedFile = false;
            SSoDeNghi = null;
            INamKeHoach = null;
            DNgayDeNghi = null;
            CbxNguonVonSelected = null;
            CbxCoQuanThanhToanSelected = null;

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(SSoDeNghi));
            OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(DNgayDeNghi));
            OnPropertyChanged(nameof(CbxNguonVonSelected));
            OnPropertyChanged(nameof(CbxCoQuanThanhToanSelected));
        }

        private void ShowError(object param)
        {
            var importTabIndex = (ImportTabIndex)((int)param);
            int rowIndex;
            rowIndex = QuyetToanChiTiets.IndexOf(QuyetToanChiTietSelected);
            var errors = _lstErrChungTuChiTiet.Where(x => x.Row == rowIndex).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
            System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public override void OnSave()
        {
            List<string> lstError = new List<string>();
            ValidateForm(ref lstError);

            if (lstError.Any())
            {
                string sMessError = string.Join(Environment.NewLine, lstError);
                System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _service.Insert(ObjQuyetToan, _sessionService.Current.Principal);
            List<VdtQtBcQuyetToanNienDoChiTiet01> lstData = new List<VdtQtBcQuyetToanNienDoChiTiet01>();
            foreach (var item in QuyetToanChiTiets.Where(n => n.ImportStatus &&
             (!string.IsNullOrEmpty(n.fGiaTriTamUngDieuChinhGiam) || !string.IsNullOrEmpty(n.fGiaTriNamTruocChuyenNamSau) || !string.IsNullOrEmpty(n.fGiaTriNamNayChuyenNamSau))))
            {
                VdtQtBcQuyetToanNienDoChiTiet01 data = new VdtQtBcQuyetToanNienDoChiTiet01();
                data.DDateCreate = DateTime.Now;
                data.SUserCreate = _sessionService.Current.Principal;
                data.IIDDuAnID = _dicDuAn[item.sMaDuAn];
                data.IIdLoaiCongTrinh = _dicLoaiCongTrinh[item.SMaLoaiCongTrinh];
                if (!string.IsNullOrEmpty(item.fGiaTriNamNayChuyenNamSau))
                    data.FGiaTriNamNayChuyenNamSau = double.Parse(item.fGiaTriNamNayChuyenNamSau, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.fGiaTriNamTruocChuyenNamSau))
                    data.FGiaTriNamTruocChuyenNamSau = double.Parse(item.fGiaTriNamTruocChuyenNamSau, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.fGiaTriTamUngDieuChinhGiam))
                    data.FGiaTriTamUngDieuChinhGiam = double.Parse(item.fGiaTriTamUngDieuChinhGiam, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.fLuyKeThanhToanNamTruoc))
                    data.FLKThanhToanDenTrcNamQuyetToan = double.Parse(item.fLuyKeThanhToanNamTruoc, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.fTamUngTheoCheDoChuaThuHoiNamTruoc))
                    data.FTamUngChuaThuHoiTrcNamQuyetToan = double.Parse(item.fTamUngTheoCheDoChuaThuHoiNamTruoc, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.fTamUngNamTruocThuHoiNamNay))
                    data.FThuHoiUngNamTrc = double.Parse(item.fTamUngNamTruocThuHoiNamNay, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.fKHVNamTruocChuyenNamNay))
                    data.FChiTieuNamTrcChuyenSang = double.Parse(item.fKHVNamTruocChuyenNamNay, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.fTongThanhToanSuDungVonNamTruoc))
                    data.FThanhToanKLHTCTNamTrcChuyenSang = double.Parse(item.fTongThanhToanSuDungVonNamTruoc, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.fTamUngTheoCheDoChuaThuHoiKeoDaiNamNay))
                    data.FTamUngChuaThuHoiCTNamTrcChuyenSang = double.Parse(item.fTamUngTheoCheDoChuaThuHoiKeoDaiNamNay, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.fKHVNamNay))
                    data.FChiTieuNamNay = double.Parse(item.fKHVNamNay, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.fTongThanhToanSuDungVonNamNay))
                    data.FThanhToanKLHTCTNamNay = double.Parse(item.fTongThanhToanSuDungVonNamNay, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.fTamUngTheoCheDoChuaThuHoiNamNay))
                    data.FTamUngChuaThuHoiCTNamNay = double.Parse(item.fTamUngTheoCheDoChuaThuHoiNamNay, CultureInfo.GetCultureInfo("vi-VN"));
                data.Id = Guid.NewGuid();
                data.IIdBcquyetToanNienDo = ObjQuyetToan.Id;
                data.IIDDuAnID = item.iID_DuAnID;
                lstData.Add(data);
            }
            _service.InsertVdtQtBcquyetToanNienDoChiTiet01(ObjQuyetToan.Id, lstData);
            _tonghopService.InsertTongHopNguonDauTu_Tang(LOAI_CHUNG_TU.QUYET_TOAN, (int)TypeExecute.Update, ObjQuyetToan.Id);
            System.Windows.MessageBox.Show(Resources.MsgSaveDone);
            var entityModel = _mapper.Map<VdtQtBcQuyetToanNienDo>(ObjQuyetToan);
            SavedAction?.Invoke(entityModel);
        }

        private void OnGetFileFtpCommand()
        {
            if (CbxLoaiDonViSelected == null || string.IsNullOrEmpty(INamKeHoach))
            {

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng chọn đúng từng giai đoạn");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                return;
            }
            var btmTenDonVi = StringUtils.UCS2Convert(CbxLoaiDonViSelected.ValueItem);
            string sTime = string.Format("{0}", INamKeHoach);
            var strUrl = string.Format("{0}/{1}/{2}", btmTenDonVi, ConstantUrlPathPhanHe.UrlBcqtcnvdtWinformSendTt, sTime);
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
        private void OnDownloadFileFtpServer()
        {
            // bool checkBtn = LstFile.Any(e => e.BIsCheck);
            string urlUrIDownLoad = "";
            string fileName = "";
            if (LstFile == null || LstFile.Count == 0 || !LstFile.Any(e => e.BIsCheck))
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng lấy dữ liệu");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                return;
            }
            else if (LstFile.Where(n => n.BIsCheck).Count() > 1)
            {
                System.Windows.MessageBox.Show("Chọn 1 file dữ liệu");
                return;
            }
            foreach (var item in LstFile)
            {
                if (item.BIsCheck)
                {
                    urlUrIDownLoad = item.SUrl;
                    fileName = item.SNameFile;
                    string filePath=  _ftpStorageService.DowLoadFileFtpGiveLocal(urlUrIDownLoad, fileName);
                    FilePath = filePath;
                }
            }
            OnProcessFileFtpServer();
        }
        private string OnProcessFileFtpServer()
        {
            _lstErrChungTuChiTiet = new List<ImportErrorItem>();
            List<string> lstError = new List<string>();
            var data = GetDataImportByFileType();
            QuyetToanChiTiets = new ObservableCollection<BcquyetToanNienDoVonNamChiTietImportModel>(data);
            if (lstError.Any())
            {
                string sMessError = string.Join(Environment.NewLine, lstError);
                System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                ObjQuyetToan = null;
            }
            OnPropertyChanged(nameof(ObjQuyetToan));
            OnPropertyChanged(nameof(QuyetToanChiTiets));

            return string.Empty;
        }
        #endregion

        #region Helper
        private void ValidateChungTuChiTiet()
        {
            var data = _service.GetDeNghiQuyetToanNienDoDetail
                (ObjQuyetToan.IIdMaDonViQuanLy, ObjQuyetToan.INamKeHoach ?? 0, ObjQuyetToan.IIdNguonVonId ?? 0);
            _dicDuAn = new Dictionary<string, Guid>();
            foreach (var item in data)
            {
                if (_dicDuAn.ContainsKey(item.SMaDuAn)) continue;
                _dicDuAn.Add(item.SMaDuAn, item.IIDDuAnID);
            }

            for (int i = 0; i < QuyetToanChiTiets.Count; ++i)
            {
                var item = QuyetToanChiTiets[i];
                if (!_dicDuAn.ContainsKey(item.sMaDuAn))
                {
                    _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                    {
                        ColumnName = "Mã dự án",
                        Error = Resources.MsgErrorProjectNotFound,
                        IsErrorMLNS = false,
                        Row = i
                    });
                    item.ImportStatus &= false;
                }

                if (string.IsNullOrEmpty(item.SMaLoaiCongTrinh))
                {
                    _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                    {
                        ColumnName = "Loại công trình",
                        Error = string.Format(Resources.MsgErrorDataEmpty,"loại công trình"),
                        IsErrorMLNS = false,
                        Row = i
                    });
                    item.ImportStatus &= false;
                }
                else if (!_dicLoaiCongTrinh.ContainsKey(item.SMaLoaiCongTrinh))
                {
                    _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                    {
                        ColumnName = "Loại công trình",
                        Error = string.Format(Resources.MsgErrorItemNotFound, "loại công trình"),
                        IsErrorMLNS = false,
                        Row = i
                    });
                    item.ImportStatus &= false;
                }

                if (string.IsNullOrEmpty(item.fGiaTriTamUngDieuChinhGiam)
                    && string.IsNullOrEmpty(item.fGiaTriNamTruocChuyenNamSau)
                    && string.IsNullOrEmpty(item.fGiaTriNamNayChuyenNamSau))
                {
                    _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                    {
                        ColumnName = "Mã dự án",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "chứng từ chi tiết"),
                        IsErrorMLNS = false,
                        Row = i
                    });
                    item.ImportStatus &= false;
                }
            }
            OnPropertyChanged(nameof(QuyetToanChiTiets));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private List<BcquyetToanNienDoVonNamChiTietImportModel> GetDataImportByFileType()
        {
            XlsFile xls = new XlsFile(false);
            xls.Open(FilePath);
            xls.ActiveSheet = 1;

            var lstResults = _importService.ProcessData<BcquyetToanNienDoVonNamChiTietImportModel>(FilePath);
            if (lstResults.ImportErrors.Any())
            {
                _lstErrChungTuChiTiet.AddRange(lstResults.ImportErrors);
            }
            return _mapper.Map<List<BcquyetToanNienDoVonNamChiTietImportModel>>(lstResults.Data);
        }

        private bool ValidateForm(ref List<string> lstError)
        {
            int iNamKeHoach = 0;
            if (CbxLoaiDonViSelected == null)
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "đơn vị"));
            if (string.IsNullOrEmpty(INamKeHoach))
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "năm kế hoạch"));
            else if (!int.TryParse(INamKeHoach, out iNamKeHoach))
                lstError.Add(string.Format(Resources.MsgErrorFormat, "năm kế hoạch"));
            if (string.IsNullOrEmpty(SSoDeNghi))
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "số văn bản"));
            if (!DNgayDeNghi.HasValue)
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "ngày đề nghị"));
            if (CbxNguonVonSelected == null)
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "nguồn vốn"));
            if (CbxCoQuanThanhToanSelected == null)
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "cơ quan thanh toán"));
            if (lstError.Count != 0) return false;
            ObjQuyetToan = new VdtQtBcQuyetToanNienDo();
            ObjQuyetToan.DNgayDeNghi = DNgayDeNghi.Value;
            ObjQuyetToan.ICoQuanThanhToan = int.Parse(CbxCoQuanThanhToanSelected.ValueItem);
            ObjQuyetToan.Id = Guid.NewGuid();
            ObjQuyetToan.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
            ObjQuyetToan.IIdMaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
            ObjQuyetToan.IIdNguonVonId = int.Parse(CbxNguonVonSelected.ValueItem);
            ObjQuyetToan.ILoaiThanhToan = (int)PaymentTypeEnum.Type.THANH_TOAN;
            ObjQuyetToan.INamKeHoach = iNamKeHoach;
            ObjQuyetToan.SSoDeNghi = SSoDeNghi;
            return true;
        }

        private void LoadComboBoxLoaiDonVi()
        {
            var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => _lstDonViExclude.Contains(n.Loai))
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = n.TenDonVi, HiddenValue = n.Id.ToString() });
            _cbxLoaiDonVi = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
            OnPropertyChanged(nameof(CbxLoaiDonVi));
        }

        private void LoadComboBoxNguonVon()
        {
            var cbxNguonVon = _nguonVonService.FindAll().OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
            _cbxNguonVon = new ObservableCollection<ComboboxItem>(cbxNguonVon);
            OnPropertyChanged(nameof(CbxNguonVon));
        }

        public void LoadCoQuanThanhToan()
        {
            List<ComboboxItem> lstItem = new List<ComboboxItem>();
            lstItem.Add(new ComboboxItem()
            {
                ValueItem = ((int)CoQuanThanhToanEnum.Type.CQTC).ToString(),
                DisplayItem = CoQuanThanhToanEnum.TypeName.CQTC
            });
            lstItem.Add(new ComboboxItem()
            {
                ValueItem = ((int)CoQuanThanhToanEnum.Type.KHO_BAC).ToString(),
                DisplayItem = CoQuanThanhToanEnum.TypeName.KHO_BAC
            });
            CbxCoQuanThanhToan = new ObservableCollection<ComboboxItem>(lstItem);
            OnPropertyChanged(nameof(CbxCoQuanThanhToan));
        }
        #endregion
    }
}
