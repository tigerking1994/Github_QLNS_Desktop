using AutoMapper;
using FlexCel.XlsAdapter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using log4net;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using System.IO;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanSuggestions
{
    public class PlanSuggestionsImportViewModel : ViewModelBase
    {
        #region Private
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private readonly FtpStorageService _ftpStorageService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly IDmLoaiCongTrinhService _loaiCongTrinhService;
        private readonly IVdtKhvKeHoach5NamDeXuatService _vdtKhvKeHoach5NamDeXuatService;
        private readonly IVdtKhvKeHoach5NamDeXuatChiTietService _vdtKhvKeHoach5NamChiTietDexuatService;
        private readonly ILog _logger;
        private List<ImportErrorItem> _lstErrChungTuChiTiet;
        private Dictionary<int, Core.Domain.NsNguonNganSach> _dicNguonVon;
        private Dictionary<string, VdtDmLoaiCongTrinh> _dicLoaiCongTrinh;
        private Dictionary<string, Guid> _dicDonVi;
        private bool _isCheck;
        private string _localfilename;

        #endregion

        public override string Name => "IMPORT KẾ HOẠCH TRUNG HẠN ĐỀ XUẤT";
        public override string Description => "Chọn file Excel, thực hiện kiểm tra và import dữ liệu kế hoạch trung hạn đề xuất";

        #region Items
        private bool _bIsDownload;
        public bool BIsDownload
        {
            get => _bIsDownload;
            set => SetProperty(ref _bIsDownload, value);
        }

        private string _sTitleVonBoTriSauNam;
        public string STitleVonBoTriSauNam
        {
            get => _sTitleVonBoTriSauNam;
            set => SetProperty(ref _sTitleVonBoTriSauNam, value);
        }

        private string _sTitleNamThuNam;
        public string STitleNamThuNam
        {
            get => _sTitleNamThuNam;
            set => SetProperty(ref _sTitleNamThuNam, value);
        }

        private string _sTitleNamThuTu;
        public string STitleNamThuTu
        {
            get => _sTitleNamThuTu;
            set => SetProperty(ref _sTitleNamThuTu, value);
        }

        private string _sTitleNamThuBa;
        public string STitleNamThuBa
        {
            get => _sTitleNamThuBa;
            set => SetProperty(ref _sTitleNamThuBa, value);
        }

        private string _sTitleNamThuHai;
        public string STitleNamThuHai
        {
            get => _sTitleNamThuHai;
            set => SetProperty(ref _sTitleNamThuHai, value);
        }

        private string _sTitleNamThuNhat;
        public string STitleNamThuNhat
        {
            get => _sTitleNamThuNhat;
            set => SetProperty(ref _sTitleNamThuNhat, value);
        }

        private VdtKhvKeHoach5NamDeXuatModel _phanBoVon = new VdtKhvKeHoach5NamDeXuatModel();
        public VdtKhvKeHoach5NamDeXuatModel PhanBoVon
        {
            get => _phanBoVon;
            set => SetProperty(ref _phanBoVon, value);
        }

        private ObservableCollection<VdtKhvKeHoach5NamDeXuatChiTietModel> _phanBoVonChiTiets;
        public ObservableCollection<VdtKhvKeHoach5NamDeXuatChiTietModel> PhanBoVonChiTiets
        {
            get => _phanBoVonChiTiets;
            set => SetProperty(ref _phanBoVonChiTiets, value);
        }

        private VdtKhvKeHoach5NamDeXuatChiTietModel _phanBoVonChiTietSelected;
        public VdtKhvKeHoach5NamDeXuatChiTietModel PhanBoVonChiTietSelected
        {
            get => _phanBoVonChiTietSelected;
            set => SetProperty(ref _phanBoVonChiTietSelected, value);
        }

        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
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

        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set
            {
                SetProperty(ref _cbxLoaiDonViSelected, value);
                if (_cbxLoaiDonViSelected != null)
                {
                    PhanBoVon.IIdMaDonVi = _cbxLoaiDonViSelected.ValueItem;
                    PhanBoVon.IIdDonViId = Guid.Parse(_cbxLoaiDonViSelected.HiddenValue);
                    OnPropertyChanged(nameof(PhanBoVon));
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
        }

        private ObservableCollection<ComboboxItem> _drpVouchers;
        public ObservableCollection<ComboboxItem> DrpVouchers
        {
            get => _drpVouchers;
            set => SetProperty(ref _drpVouchers, value);
        }

        private ComboboxItem _drpVoucherSelected;
        public ComboboxItem DrpVoucherSelected
        {
            get => _drpVoucherSelected;
            set
            {
                SetProperty(ref _drpVoucherSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _drpLoaiDuAns;
        public ObservableCollection<ComboboxItem> DrpLoaiDuAns
        {
            get => _drpLoaiDuAns;
            set => SetProperty(ref _drpLoaiDuAns, value);
        }

        private ComboboxItem _drpLoaiDuAnSelected;
        public ComboboxItem DrpLoaiDuAnSelected
        {
            get => _drpLoaiDuAnSelected;
            set => SetProperty(ref _drpLoaiDuAnSelected, value);
        }

        private ObservableCollection<ComboboxItem> _drpLoaiImports;
        public ObservableCollection<ComboboxItem> DrpLoaiImports
        {
            get => _drpLoaiImports;
            set => SetProperty(ref _drpLoaiImports, value);
        }

        private ComboboxItem _drpLoaiImportSelected;
        public ComboboxItem DrpLoaiImportSelected
        {
            get => _drpLoaiImportSelected;
            set
            {
                SetProperty(ref _drpLoaiImportSelected, value);
                OnPropertyChanged(nameof(VoucherModifiedVisibility));
                OnPropertyChanged(nameof(VouchersSuggestionVisibility));
            }
        }

        private string _sSoKeHoach;
        public string sSoKeHoach
        {
            get => _sSoKeHoach;
            set
            {
                SetProperty(ref _sSoKeHoach, value);
                if (value != null)
                {
                    PhanBoVon.SSoQuyetDinh = _sSoKeHoach;
                }
            }
        }

        private int _iGiaiDoanTu;
        public int IGiaiDoanTu
        {
            get => _iGiaiDoanTu;
            set
            {
                SetProperty(ref _iGiaiDoanTu, value);
                if (value != 0)
                {
                    PhanBoVon.IGiaiDoanTu = IGiaiDoanTu;
                    LoadHeader();
                    OnPropertyChanged(nameof(IGiaiDoanDen));
                }
            }
        }

        private int _iGiaiDoanDen;
        public int IGiaiDoanDen
        {
            get
            {
                _iGiaiDoanDen = _iGiaiDoanTu + 4;
                PhanBoVon.IGiaiDoanDen = _iGiaiDoanDen;
                return _iGiaiDoanDen;
            }
            set => SetProperty(ref _iGiaiDoanDen, value);
        }

        private bool _isSaveData;
        public bool IsSaveData
        {
            get
            {
                _isSaveData = (PhanBoVonChiTiets != null && PhanBoVonChiTiets.Count > 0 && _isCheck) ? true : false;
                return _isSaveData;
            }
            set => SetProperty(ref _isSaveData, value);
        }

        private bool _isEdit = true;
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        public Visibility VouchersSuggestionVisibility
        {
            get => (_drpLoaiImportSelected == null || _drpLoaiImportSelected != null && _drpLoaiImportSelected.ValueItem.Equals("1")) ? Visibility.Collapsed : Visibility.Visible;
        }

        public Visibility VoucherModifiedVisibility
        {
            get => (_drpLoaiImportSelected != null && _drpLoaiImportSelected.ValueItem.Equals("2")) ? Visibility.Collapsed : Visibility.Visible;
        }

        #endregion

        #region RelayCommand
        public RelayCommand GetFileFtpCommand { get; }
        public RelayCommand DownloadFileFtpServer { get; }
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorCommand { get; }
        #endregion

        public PlanSuggestionsImportViewModel(ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            FtpStorageService ftpStorageService,
            ILog logger,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonVonService,
            IDmLoaiCongTrinhService loaiCongTrinhService,
            IVdtKhvKeHoach5NamDeXuatService vdtKhvKeHoach5NamDeXuatService,
            IVdtKhvKeHoach5NamDeXuatChiTietService vdtKhvKeHoach5NamChiTietDexuatService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _importService = importService;
            _nsDonViService = nsDonViService;
            _nsNguonVonService = nsNguonVonService;
            _vdtKhvKeHoach5NamDeXuatService = vdtKhvKeHoach5NamDeXuatService;
            _vdtKhvKeHoach5NamChiTietDexuatService = vdtKhvKeHoach5NamChiTietDexuatService;
            _loaiCongTrinhService = loaiCongTrinhService;
            _ftpStorageService = ftpStorageService;

            GetFileFtpCommand = new RelayCommand(obj => OnGetFileFtpCommand());
            DownloadFileFtpServer = new RelayCommand(obj => OnDownloadFileFtpServer());
            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(ShowError);
        }

        #region Event
        public override void Init()
        {
            try
            {
                IGiaiDoanTu = DateTime.Now.Year;
                IGiaiDoanDen = IGiaiDoanTu + 4;

                LoadComboBoxLoaiDonVi();
                GetNguonVon();
                GetLoaiCongTrinh();
                GetLoaiImport();
                GetLoaiDuAn();
                GetVouchers();
                OnResetData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void GetLoaiImport()
        {
            List<ComboboxItem> lstLoaiImport = new List<ComboboxItem>()
            {
                new ComboboxItem(){DisplayItem = "Mới", ValueItem = "1"},
                new ComboboxItem(){DisplayItem = "Điều chỉnh", ValueItem = "2"}
            };

            DrpLoaiImports = new ObservableCollection<ComboboxItem>(lstLoaiImport);
        }

        private void GetLoaiDuAn()
        {
            List<ComboboxItem> lstLoaiDuAn = new List<ComboboxItem>() {
                new ComboboxItem(){DisplayItem = "Mở mới", ValueItem = ((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI).ToString()},
                new ComboboxItem(){DisplayItem = "Chuyển tiếp", ValueItem = ((int)LoaiDuAnEnum.Type.CHUYEN_TIEP).ToString()}
            };

            DrpLoaiDuAns = new ObservableCollection<ComboboxItem>(lstLoaiDuAn);
        }

        private void GetVouchers()
        {
            var predicate = PredicateBuilder.True<VdtKhvKeHoach5NamDeXuat>();
            predicate = predicate.And(x => x.BActive);
            predicate = predicate.And(n => string.IsNullOrEmpty(n.STongHop));
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            List<VdtKhvKeHoach5NamDeXuat> lstQuery = _vdtKhvKeHoach5NamDeXuatService.FindByCondition(predicate).OrderByDescending(x => x.DDateCreate).ToList();
            DrpVouchers = _mapper.Map<ObservableCollection<ComboboxItem>>(lstQuery);
        }


        private void OnProcessFile()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                {
                    System.Windows.MessageBox.Show(Resources.ErrorFileEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            try
            {
                var dataImport = _importService.ProcessData<KeHoach5NamDeXuatImportModel>(FilePath);
                var KeHoach5NamDeXuatImportModels = new ObservableCollection<KeHoach5NamDeXuatImportModel>(dataImport.Data);
                _lstErrChungTuChiTiet = new List<ImportErrorItem>();
                List<string> lstError = new List<string>();

                if (dataImport.ImportErrors.Count > 0)
                {
                    _lstErrChungTuChiTiet.AddRange(dataImport.ImportErrors);
                }

                if (KeHoach5NamDeXuatImportModels == null || KeHoach5NamDeXuatImportModels.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (KeHoach5NamDeXuatImportModels.Any(x => !x.ImportStatus))
                {
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (string.IsNullOrEmpty(FilePath))
                {
                    lstError.Add(Resources.ErrorFileEmpty);
                }

                ValidateForm(ref lstError);
                if (lstError.Any())
                {
                    string sMessError = string.Join(Environment.NewLine, lstError);
                    System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _isCheck = true;

                OnPropertyChanged(nameof(PhanBoVon));
                OnPropertyChanged(nameof(PhanBoVonChiTiets));
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
                return;
            }
        }

        private void OnGetFileFtpCommand()
        {
            if (CbxLoaiDonViSelected == null || (IGiaiDoanDen == 0 && IGiaiDoanTu == 0))
            {

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng chọn đúng từng giai đoạn");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                return;
            }
            var btmTenDonVi = StringUtils.UCS2Convert(CbxLoaiDonViSelected.ValueItem);
            string sTime = string.Format("{0}-{1}", IGiaiDoanTu, IGiaiDoanDen);
            var strUrl = string.Format("{0}/{1}/{2}", btmTenDonVi, ConstantUrlPathPhanHe.UrlKhthdxWinformSend, sTime);
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
        /// Lấy dữ liệu từ ftp
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
            else if(LstFile.Where(n=>n.BIsCheck).Count() > 1){
                System.Windows.MessageBox.Show("Chọn 1 file dữ liệu");
                return;
            }
            foreach (var item in LstFile)
            {
                if (item.BIsCheck)
                {
                    urlUrIDownLoad = item.SUrl;
                    fileName = item.SNameFile;
                    string filePath = _ftpStorageService.DowLoadFileFtpGiveLocal(urlUrIDownLoad, fileName);
                    FilePath = filePath;
                }
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                LoadHeader();
                HandleData();

                if (CbxLoaiDonViSelected != null && !string.IsNullOrEmpty(sSoKeHoach) && !IGiaiDoanTu.Equals(0) && !IGiaiDoanDen.Equals(0))
                {
                    _isCheck = true;
                }
                else
                {
                    _isCheck = false;
                }

                if (PhanBoVon != null && PhanBoVon.Id != null)
                {
                    VdtKhvKeHoach5NamDeXuat itemChungTu = _vdtKhvKeHoach5NamDeXuatService.FindById(PhanBoVon.Id);

                    if (itemChungTu != null)
                    {
                        sSoKeHoach = PhanBoVon.SSoQuyetDinh;
                        IGiaiDoanTu = PhanBoVon.IGiaiDoanTu;
                        IGiaiDoanDen = PhanBoVon.IGiaiDoanDen;
                        IsEdit = false;
                    }
                    else
                    {
                        IsEdit = true;
                    }
                }
                OnPropertyChanged(nameof(FilePath));
                OnPropertyChanged(nameof(IsSelectedFile));
                OnPropertyChanged(nameof(PhanBoVon));
                OnPropertyChanged(nameof(PhanBoVonChiTiets));
                OnPropertyChanged(nameof(STitleNamThuNhat));
                OnPropertyChanged(nameof(STitleNamThuHai));
                OnPropertyChanged(nameof(STitleNamThuBa));
                OnPropertyChanged(nameof(STitleNamThuTu));
                OnPropertyChanged(nameof(STitleNamThuNam));
                OnPropertyChanged(nameof(STitleVonBoTriSauNam));
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IGiaiDoanTu));
                OnPropertyChanged(nameof(IGiaiDoanDen));
                OnPropertyChanged(nameof(sSoKeHoach));
                OnPropertyChanged(nameof(IsEdit));
            }, (s, e) =>
            {
                IsLoading = false;
            });

        }

        private void HandleData()
        {
            try
            {
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;
                var data = _importService.ProcessData<KeHoach5NamDeXuatImportModel>(FilePath);
                var dataExp = data.Data.Select(x =>
                {
                    if (string.IsNullOrEmpty(x.IIdLoaiCongTrinhId))
                    {
                        x.IIdLoaiCongTrinhId = Guid.Empty.ToString();
                    }
                    if (string.IsNullOrEmpty(x.IIdNguonVonId))
                    {
                        x.IIdNguonVonId = "0";
                    }
                    if (string.IsNullOrEmpty(x.FGiaTriBoTri))
                    {
                        x.FGiaTriBoTri = "0";
                    }
                    if (string.IsNullOrEmpty(x.FGiaTriNamThuNhat))
                    {
                        x.FGiaTriNamThuNhat = "0";
                    }
                    if (string.IsNullOrEmpty(x.FGiaTriNamThuHai))
                    {
                        x.FGiaTriNamThuHai = "0";
                    }
                    if (string.IsNullOrEmpty(x.FGiaTriNamThuBa))
                    {
                        x.FGiaTriNamThuBa = "0";
                    }
                    if (string.IsNullOrEmpty(x.FGiaTriNamThuTu))
                    {
                        x.FGiaTriNamThuTu = "0";
                    }
                    if (string.IsNullOrEmpty(x.FGiaTriNamThuNam))
                    {
                        x.FGiaTriNamThuNam = "0";
                    }
                    if (string.IsNullOrEmpty(x.IIdDuAnId))
                    {
                        x.IIdDuAnId = Guid.Empty.ToString();
                    }
                    if (string.IsNullOrEmpty(x.IdParentModified))
                    {
                        x.IdParentModified = Guid.Empty.ToString();
                    }
                    if (string.IsNullOrEmpty(x.IdParentOld))
                    {
                        x.IdParentOld = Guid.Empty.ToString();
                    }
                    if (string.IsNullOrEmpty(x.IdReference))
                    {
                        x.IdReference = Guid.Empty.ToString();
                    }
                    if (string.IsNullOrEmpty(x.IsStatus))
                    {
                        x.IsStatus = "0";
                    }
                    if (string.IsNullOrEmpty(x.Level))
                    {
                        x.Level = "1";
                    }
                    if (string.IsNullOrEmpty(x.IndexCode))
                    {
                        x.IndexCode = "0";
                    }
                    return x;
                }).ToList();
                if (dataExp.FirstOrDefault() != null)
                {
                    PhanBoVon.STongHop = dataExp.FirstOrDefault().STongHop;
                }
                List<VdtKhvKeHoach5NamDeXuatChiTietModel> dataImport = _mapper.Map<List<VdtKhvKeHoach5NamDeXuatChiTietModel>>(dataExp);
                PhanBoVonChiTiets = new ObservableCollection<VdtKhvKeHoach5NamDeXuatChiTietModel>(dataImport);
                PhanBoVonChiTiets.Select(x =>
                {
                    x.FGiaTriKeHoach = (x.FGiaTriNamThuNhat ?? 0) + (x.FGiaTriNamThuHai ?? 0) + (x.FGiaTriNamThuBa ?? 0) + (x.FGiaTriNamThuTu ?? 0) + (x.FGiaTriNamThuNam ?? 0);
                    return x;
                }).ToList();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadHeader()
        {
            try
            {
                STitleNamThuNhat = string.Format("Năm {0}", IGiaiDoanTu);
                STitleNamThuHai = string.Format("Năm {0}", IGiaiDoanTu + 1);
                STitleNamThuBa = string.Format("Năm {0}", IGiaiDoanTu + 2);
                STitleNamThuTu = string.Format("Năm {0}", IGiaiDoanTu + 3);
                STitleNamThuNam = string.Format("Năm {0}", IGiaiDoanTu + 4);
                STitleVonBoTriSauNam = string.Format("Vốn bố trí sau năm {0}", IGiaiDoanTu + 4);

                OnPropertyChanged(nameof(STitleNamThuNhat));
                OnPropertyChanged(nameof(STitleNamThuHai));
                OnPropertyChanged(nameof(STitleNamThuBa));
                OnPropertyChanged(nameof(STitleNamThuTu));
                OnPropertyChanged(nameof(STitleNamThuNam));
                OnPropertyChanged(nameof(STitleVonBoTriSauNam));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnSave()
        {
            try
            {
                List<string> lstError = new List<string>();
                if (ValidateForm(ref lstError))
                {
                    if (lstError.Any())
                    {
                        string sMessError = string.Join(Environment.NewLine, lstError);
                        System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                var lstUnitManager = _sessionService.Current.IdsDonViQuanLy;
                List<string> lstDv = new List<string>();
                if (lstUnitManager.Contains(","))
                {
                    lstDv = lstUnitManager.Split(",").ToList();
                }
                else
                {
                    lstDv.Add(lstUnitManager);
                }
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && !lstDv.Contains(_cbxLoaiDonViSelected.ValueItem))
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.UserManagerImportKHTHWarning, _sessionService.Current.Principal, _cbxLoaiDonViSelected.DisplayItem), Resources.Alert);
                    return;
                }

                if (DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("1"))
                {
                    PhanBoVon.Id = Guid.NewGuid();
                    VdtKhvKeHoach5NamDeXuat entity = _mapper.Map<VdtKhvKeHoach5NamDeXuat>(PhanBoVon);
                    entity.DNgayQuyetDinh = DateTime.Now;
                    entity.DDateCreate = DateTime.Now;
                    entity.SUserCreate = _sessionService.Current.Principal;
                    entity.IIdMaDonViQuanLy = _cbxLoaiDonViSelected.ValueItem;
                    entity.IIdDonViQuanLyId = Guid.Parse(_cbxLoaiDonViSelected.HiddenValue);
                    entity.NamLamViec = _sessionService.Current.YearOfWork;
                    entity.IGiaiDoanDen = IGiaiDoanDen;
                    entity.IGiaiDoanTu = IGiaiDoanTu;
                    entity.SSoQuyetDinh = sSoKeHoach;
                    entity.ILoai = Int32.Parse(_drpLoaiDuAnSelected.ValueItem);
                    entity.BActive = true;
                    entity.BIsGoc = true;

                    _vdtKhvKeHoach5NamDeXuatService.Add(entity);

                    var lstDataInsert = _mapper.Map<List<VdtKhvKeHoach5NamDeXuatChiTiet>>(PhanBoVonChiTiets);

                    lstDataInsert.Select(x =>
                    {
                        x.FGiaTriKeHoach = (x.FGiaTriNamThuNhat ?? 0) + (x.FGiaTriNamThuHai ?? 0) + (x.FGiaTriNamThuBa ?? 0) + (x.FGiaTriNamThuTu ?? 0) + (x.FGiaTriNamThuNam ?? 0);
                        x.FGiaTriBoTri = (x.FHanMucDauTu ?? 0) - x.FGiaTriKeHoach;
                        x.IdMParent = x.Id;
                        x.Id = Guid.NewGuid();
                        x.IIdKeHoach5NamId = entity.Id;
                        x.BActive = true;
                        return x;
                    }).OrderBy(x => x.SMaOrder).ToList();

                    var refDictionary = lstDataInsert.ToDictionary(x => x.IdMParent, x => x.Id);

                    foreach (var item in lstDataInsert)
                    {
                        item.IdReference = item.IdReference != Guid.Empty ? item.IdReference : null;
                        item.IdParent = item.IdParent != Guid.Empty ? item.IdParent : null;

                        if (item.IdReference != null)
                        {
                            item.IdReference = refDictionary[item.IdReference];
                        }
                        if (item.IdParent != null)
                        {
                            item.IdParent = refDictionary[item.IdParent];
                        }
                    }
                    lstDataInsert.Select(x =>
                    {
                        if (x.IIdDuAnId == Guid.Empty) x.IIdDuAnId = null;
                        if (x.IdParentModified == Guid.Empty) x.IdParentModified = null;
                        return x;
                    }).ToList();
                    if (lstDataInsert.Count() > 0)
                    {
                        var FGiaTriKeHoach = lstDataInsert.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriKeHoach);
                        var itemUpdate = _vdtKhvKeHoach5NamDeXuatService.FindById(lstDataInsert.FirstOrDefault().IIdKeHoach5NamId.Value);

                        if (itemUpdate != null)
                        {
                            itemUpdate.FGiaTriKeHoach = FGiaTriKeHoach;
                            _vdtKhvKeHoach5NamDeXuatService.Update(itemUpdate);
                        }
                    }

                    int isSuccess = _vdtKhvKeHoach5NamChiTietDexuatService.AddRange(lstDataInsert);
                    if (isSuccess == 0)
                    {
                        _vdtKhvKeHoach5NamDeXuatService.Delete(PhanBoVon.Id);
                        System.Windows.MessageBox.Show(Resources.ImportError);
                        return;
                    }
                    System.Windows.MessageBox.Show(Resources.FileImportStatus);
                }
                else if (DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("2"))
                {
                    //if ((PhanBoVonChiTiets != null && PhanBoVonChiTiets.Count() > 0 && PhanBoVonChiTiets.FirstOrDefault().IIdKeHoach5NamId != Guid.Parse(_drpVoucherSelected.ValueItem)))
                    //{
                    //    System.Windows.MessageBox.Show(Resources.VoucherImportErrors, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    //    return;
                    //}

                    VdtKhvKeHoach5NamDeXuat itemParent = _vdtKhvKeHoach5NamDeXuatService.FindById(Guid.Parse(_drpVoucherSelected.ValueItem));
                    var lstDetail = _vdtKhvKeHoach5NamChiTietDexuatService.FindListKH5NamDeXuatDieuChinhChiTiet(itemParent.Id).ToList();
                    var lstQuery = _vdtKhvKeHoach5NamChiTietDexuatService.FindListVoucherDetailsModified(itemParent.Id).ToList();
                    //List<VdtKhvKeHoach5NamDeXuatChiTiet> itemChiTietParent = _mapper.Map<List<VdtKhvKeHoach5NamDeXuatChiTiet>>(lstQuery);
                    List<VdtKhvKeHoach5NamDeXuatChiTiet> itemChiTietParent = _mapper.Map<List<VdtKhvKeHoach5NamDeXuatChiTiet>>(lstDetail);
                    List<VdtKhvKeHoach5NamDeXuatChiTiet> itemChiTietNew = _mapper.Map<List<VdtKhvKeHoach5NamDeXuatChiTiet>>(PhanBoVonChiTiets);
                    int count = 0;
                    if (PhanBoVonChiTiets.Count() > 0)
                    {
                        PhanBoVonChiTiets.ToList().ForEach(r =>
                        {
                            foreach (var i in itemChiTietParent)
                            {
                                if (r.STen == i.STen && r.SDiaDiem == i.SDiaDiem && r.IGiaiDoanTu == i.IGiaiDoanTu && r.IGiaiDoanDen == i.IGiaiDoanDen && r.IIdNguonVonId == i.IIdNguonVonId && r.FHanMucDauTu == i.FHanMucDauTu)
                                {
                                    count++;
                                    i.FGiaTriBoTri = r.FGiaTriBoTri;
                                    i.FGiaTriNamThuNhat = r.FGiaTriNamThuNhat;
                                    i.FGiaTriNamThuHai = r.FGiaTriNamThuHai;
                                    i.FGiaTriNamThuBa = r.FGiaTriNamThuBa;
                                    i.FGiaTriNamThuTu = r.FGiaTriNamThuTu;
                                    i.FGiaTriNamThuNam = r.FGiaTriNamThuNam;
                                    i.FGiaTriKeHoach = r.FGiaTriKeHoach;
                                }
                            }

                        });
                    }
                    if (count == 0)
                    {
                        System.Windows.MessageBox.Show(Resources.VoucherImportErrors, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    List<VdtKhvKeHoach5NamDeXuatChiTiet> result = new List<VdtKhvKeHoach5NamDeXuatChiTiet>();

                    if (itemChiTietParent.Count().Equals(0))
                    {
                        System.Windows.MessageBox.Show(Resources.MsgVoucherModified, Resources.Alert);
                        return;
                    }

                    VdtKhvKeHoach5NamDeXuat itemNew = new VdtKhvKeHoach5NamDeXuat();
                    itemParent.CloneObj(itemNew);

                    itemNew.DDateCreate = DateTime.Now;
                    itemNew.SUserCreate = _sessionService.Current.Principal;
                    itemNew.BIsGoc = false;
                    itemNew.Id = Guid.NewGuid();
                    itemNew.IIdParentId = itemParent.Id;
                    itemNew.SSoQuyetDinh = sSoKeHoach;
                    itemNew.BKhoa = false;
                    //result = itemChiTietNew;
                    result = itemChiTietParent;

                    result.Select(x =>
                    {
                        if (x.IdParent.HasValue)
                        {
                            bool parentInList = result.Select(x => x.Id).ToList().Contains(x.IdParent.Value);
                            if (!parentInList)
                            {
                                x.IdParent = null;
                            }
                        }
                        if (x.IIdDuAnId == Guid.Empty) x.IIdDuAnId = null;
                        if (x.IIdLoaiCongTrinhId == Guid.Empty) x.IIdLoaiCongTrinhId = null;
                        if (x.IdParentModified == Guid.Empty) x.IdParentModified = null;
                        if (x.IdReference == Guid.Empty) x.IdReference = null;
                        return x;
                    }).ToList();

                    int success = _vdtKhvKeHoach5NamDeXuatService.Adjust(itemNew, result);
                    if (success == DBContextSaveChangeState.ERROR)
                    {
                        System.Windows.MessageBox.Show(Resources.ImportError);
                        return;
                    }
                    System.Windows.MessageBox.Show(Resources.FileImportStatus);
                }

                var entityModel = _mapper.Map<VdtKhvKeHoach5NamDeXuatModel>(PhanBoVon);
                SavedAction?.Invoke(entityModel);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
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

                FilePath = openFileDialog.FileName;
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    LoadHeader();
                    HandleData();

                    if (CbxLoaiDonViSelected != null && !string.IsNullOrEmpty(sSoKeHoach) && !IGiaiDoanTu.Equals(0) && !IGiaiDoanDen.Equals(0))
                    {
                        _isCheck = true;
                    }
                    else
                    {
                        _isCheck = false;
                    }

                    if (PhanBoVon != null && PhanBoVon.Id != null)
                    {
                        VdtKhvKeHoach5NamDeXuat itemChungTu = _vdtKhvKeHoach5NamDeXuatService.FindById(PhanBoVon.Id);

                        if (itemChungTu != null)
                        {
                            sSoKeHoach = PhanBoVon.SSoQuyetDinh;
                            IGiaiDoanTu = PhanBoVon.IGiaiDoanTu;
                            IGiaiDoanDen = PhanBoVon.IGiaiDoanDen;
                            IsEdit = false;
                        }
                        else
                        {
                            IsEdit = true;
                        }
                    }

                    OnPropertyChanged(nameof(FilePath));
                    OnPropertyChanged(nameof(IsSelectedFile));
                    OnPropertyChanged(nameof(PhanBoVon));
                    OnPropertyChanged(nameof(PhanBoVonChiTiets));
                    OnPropertyChanged(nameof(STitleNamThuNhat));
                    OnPropertyChanged(nameof(STitleNamThuHai));
                    OnPropertyChanged(nameof(STitleNamThuBa));
                    OnPropertyChanged(nameof(STitleNamThuTu));
                    OnPropertyChanged(nameof(STitleNamThuNam));
                    OnPropertyChanged(nameof(STitleVonBoTriSauNam));
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IGiaiDoanTu));
                    OnPropertyChanged(nameof(IGiaiDoanDen));
                    OnPropertyChanged(nameof(sSoKeHoach));
                    OnPropertyChanged(nameof(IsEdit));
                }, (s, e) =>
                {
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.MsgErrorImport, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
                return;
            }
        }

        private void OnResetData()
        {
            _sSoKeHoach = null;
            IGiaiDoanTu = DateTime.Now.Year;
            _iGiaiDoanDen = DateTime.Now.Year + 4;
            _cbxLoaiDonViSelected = null;
            _isSelectedFile = false;
            _drpLoaiImportSelected = null;
            _drpVoucherSelected = null;
            _drpLoaiDuAnSelected = null;
            _cbxLoaiDonViSelected = null;
            sSoKeHoach = string.Empty;
            PhanBoVonChiTiets = new ObservableCollection<VdtKhvKeHoach5NamDeXuatChiTietModel>();
            LstFile = new ObservableCollection<FileFtpModel>();
            if (!BIsDownload)
            {
                _filePath = string.Empty;
                OnPropertyChanged(nameof(FilePath));
            }

            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(sSoKeHoach));
            OnPropertyChanged(nameof(IGiaiDoanTu));
            OnPropertyChanged(nameof(IGiaiDoanDen));
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(PhanBoVonChiTiets));
            OnPropertyChanged(nameof(DrpLoaiImportSelected));
            OnPropertyChanged(nameof(DrpLoaiDuAnSelected));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(LstFile));

            if (BIsDownload)
            {
                HandleData();
            }
        }

        private void ShowError(object param)
        {
            try
            {
                var importTabIndex = (ImportTabIndex)((int)param);
                int rowIndex;
                rowIndex = PhanBoVonChiTiets.IndexOf(PhanBoVonChiTietSelected) + 1;
                var errors = _lstErrChungTuChiTiet.Where(x => x.Row == rowIndex).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
                System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
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

        #endregion

        #region Helper
        private bool ValidateForm(ref List<string> lstError)
        {
            bool bError = false;
            if (DrpLoaiImportSelected == null)
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Loại imports"));
                bError = true;
            }

            if (string.IsNullOrEmpty(sSoKeHoach))
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Số kế hoạch"));
                bError = true;
            }

            if (DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("1"))
            {
                if (CbxLoaiDonViSelected == null)
                {
                    lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Đơn vị quản lý"));
                    bError = true;
                }
                if (IGiaiDoanTu == 0)
                {
                    lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Giai đoạn kế hoạch"));
                    bError = true;
                }
                if (DrpLoaiDuAnSelected == null)
                {
                    lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Loại dự án"));
                    bError = true;
                }
            }
            else if (DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("2"))
            {
                if (DrpVoucherSelected == null)
                {
                    lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Chứng từ"));
                    bError = true;
                }
            }

            if (CbxLoaiDonViSelected != null && DrpLoaiDuAnSelected != null)
            {
                if (!CheckGiaiDoan())
                {
                    lstError.Add(Resources.VoucherPeriodInValid);
                    bError = true;
                }
                //if (!CheckDataUnique())
                //{
                //    string message = string.Format(Resources.MsgErrorGiaiDoanExisted, CbxLoaiDonViSelected.DisplayItem, IGiaiDoanTu, IGiaiDoanDen);
                //    lstError.Add(message);
                //    bError = true;
                //}
            }

            return bError;
        }

        private bool CheckGiaiDoan()
        {
            var predicate = PredicateBuilder.True<VdtKhvKeHoach5NamDeXuat>();
            if (CbxLoaiDonViSelected != null)
            {
                predicate = predicate.And(x => x.IIdMaDonViQuanLy.Equals(CbxLoaiDonViSelected.ValueItem));
            }
            if (DrpLoaiDuAnSelected != null)
            {
                predicate = predicate.And(x => x.ILoai == int.Parse(DrpLoaiDuAnSelected.ValueItem));
            }
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => string.IsNullOrEmpty(x.STongHop));
            var rs = _vdtKhvKeHoach5NamDeXuatService.FindByCondition(predicate).ToList();

            //foreach (var item in rs)
            //{
            //    if (IGiaiDoanTu >= item.IGiaiDoanTu && IGiaiDoanTu <= item.IGiaiDoanDen)
            //    {
            //        return false;
            //    }
            //}

            return true;
        }

        private bool CheckDataUnique()
        {
            var predicate = PredicateBuilder.True<VdtKhvKeHoach5NamDeXuat>();
            if (CbxLoaiDonViSelected != null)
            {
                predicate = predicate.And(x => x.IIdMaDonViQuanLy.Equals(CbxLoaiDonViSelected.ValueItem));
            }
            if (DrpLoaiDuAnSelected != null)
            {
                predicate = predicate.And(x => x.ILoai == int.Parse(DrpLoaiDuAnSelected.ValueItem));
            }
            predicate = predicate.And(x => x.IGiaiDoanTu == IGiaiDoanTu);
            predicate = predicate.And(x => x.IGiaiDoanDen == IGiaiDoanDen);
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            var rs = _vdtKhvKeHoach5NamDeXuatService.FindByCondition(predicate).ToList();
            if (rs.Count > 0)
            {
                return false;
            }
            return true;
        }

        private void LoadComboBoxLoaiDonVi()
        {
            try
            {
                _dicDonVi = new Dictionary<string, Guid>();
                List<DonVi> lstDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();
                List<DonVi> lstUnitViaUser = new List<DonVi>();
                var lstUnitManager = _sessionService.Current.IdsDonViQuanLy;
                List<string> lstDv = new List<string>();
                if (lstUnitManager.Contains(","))
                {
                    lstDv = lstUnitManager.Split(",").ToList();
                }
                else
                {
                    lstDv.Add(lstUnitManager);
                }
                foreach (var item in lstDonVi)
                {
                    if (lstUnitManager.Contains(item.IIDMaDonVi))
                    {
                        lstUnitViaUser.Add(item);
                    }
                    if (!string.IsNullOrEmpty(item.IIDMaDonVi) && !_dicDonVi.ContainsKey(item.IIDMaDonVi))
                    {
                        _dicDonVi.Add(item.IIDMaDonVi, item.Id);
                    }
                }
                if (!lstUnitViaUser.Select(x => x.IIDMaDonVi).ToList().Contains(_sessionService.Current.IdDonVi))
                {
                    lstUnitViaUser.Add(lstDonVi.Where(x => x.IIDMaDonVi == _sessionService.Current.IdDonVi).FirstOrDefault());
                }
                _cbxLoaiDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(lstUnitViaUser);
                CbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault();
                OnPropertyChanged(nameof(CbxLoaiDonVi));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void GetNguonVon()
        {
            try
            {
                _dicNguonVon = new Dictionary<int, Core.Domain.NsNguonNganSach>();
                var data = _nsNguonVonService.FindNguonNganSach().ToList();
                _dicNguonVon = data.ToDictionary(n => n.IIdMaNguonNganSach ?? 0, n => n);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void GetLoaiCongTrinh()
        {
            try
            {
                _dicLoaiCongTrinh = new Dictionary<string, VdtDmLoaiCongTrinh>();
                var data = _loaiCongTrinhService.GetAll();
                foreach (var item in data)
                {
                    if (!string.IsNullOrEmpty(item.SMaLoaiCongTrinh) && !_dicLoaiCongTrinh.ContainsKey(item.SMaLoaiCongTrinh))
                    {
                        _dicLoaiCongTrinh.Add(item.SMaLoaiCongTrinh, item);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}
