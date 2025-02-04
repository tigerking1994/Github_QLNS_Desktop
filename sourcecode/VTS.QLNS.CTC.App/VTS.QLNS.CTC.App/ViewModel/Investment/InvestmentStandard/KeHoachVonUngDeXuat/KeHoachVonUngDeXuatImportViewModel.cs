using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Core.Service;
using log4net;
using VTS.QLNS.CTC.App.Command;
using System.Windows.Forms;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using System.Windows;
using FlexCel.XlsAdapter;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.VonNamDonVi;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.KeHoachVonUngDeXuat;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Helper;
using System.IO;
using System.Windows.Data;
using VTS.QLNS.CTC.Core.Domain.Query;
using MessageBox = System.Windows.Forms.MessageBox;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachVonUngDeXuat
{
    public class KeHoachVonUngDeXuatImportViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly IVdtKhvPhanBoVonDonViService _phanBoVonService;
        private readonly IVdtDmDonViThucHienDuAnService _vdtDmDonViThucHienDuAnService;
        private readonly IVdtDaDuAnService _duAnService;
        private readonly IVdtKhvKeHoachVonUngDxService _keHoachVonUngService;
        private readonly FtpStorageService _ftpStorageService;
        private Dictionary<string, Guid> _dicDuAn;
        private readonly ILog _logger;
        private string _fileName;
        private List<ImportErrorItem> _lstErrChungTuChiTiet;
        private List<VdtDmDonViThucHienDuAn> _lstDonViThucHienDuAnCreate;
        private Dictionary<string, Guid> _dicDonViThucHienDuAn;
        private Guid? _iIdDonViParent;
        public override Type ContentType => typeof(KeHoachVonUngDeXuatImport);
        public override string Name => "IMPORT DỮ LIỆU KẾ HOẠCH VỐN ỨNG ĐỄ XUẤT";
        public override string Description => "Chọn file Excel, thực hiện kiểm tra và import dữ liệu kế hoạch vốn ứng";        

        #region declare RelayCommand
        public RelayCommand UploadFileCommand { get; }                
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorCommand { get; }
        #endregion

        #region Componer
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

        private ObservableCollection<VdtKhvKeHoachVonUngDxChiTietModel> _keHoachVonUngDeXuatChiTiets;
        public ObservableCollection<VdtKhvKeHoachVonUngDxChiTietModel> KeHoachVonUngDeXuatChiTiets
        {
            get => _keHoachVonUngDeXuatChiTiets;
            set => SetProperty(ref _keHoachVonUngDeXuatChiTiets, value);
        }

        private VdtKhvKeHoachVonUngDxChiTietModel _keHoachVonUngDeXuatChiTietSelected;
        public VdtKhvKeHoachVonUngDxChiTietModel KeHoachVonUngDeXuatChiTietSelected
        {
            get => _keHoachVonUngDeXuatChiTietSelected;
            set => SetProperty(ref _keHoachVonUngDeXuatChiTietSelected, value);
        }

        private VdtKhvKeHoachVonUngDx _keHoachVonUngDeXuat = new VdtKhvKeHoachVonUngDx();
        public VdtKhvKeHoachVonUngDx KeHoachVonUngDeXuat
        {
            get => _keHoachVonUngDeXuat;
            set => SetProperty(ref _keHoachVonUngDeXuat, value);
        }

        public bool _isSaveData;
        public bool IsSaveData
        {
            get => KeHoachVonUngDeXuat != null && KeHoachVonUngDeXuatChiTiets != null && KeHoachVonUngDeXuatChiTiets.Count > 0 && !KeHoachVonUngDeXuatChiTiets.Any(x => !x.ImportStatus);
            set => SetProperty(ref _isSaveData, value);
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
        }

        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set
            {
                if (SetProperty(ref _cbxLoaiDonViSelected, value))
                {
                    LoadDuAn();
                }
            }
        }

        private ObservableCollection<DuAnDenghiThanhToanModel> _lstDuAn;
        public ObservableCollection<DuAnDenghiThanhToanModel> LstDuAn
        {
            get => _lstDuAn;
            set => SetProperty(ref _lstDuAn, value);
        }

        private ObservableCollection<ComboboxItem> _drpNguonVon;
        public ObservableCollection<ComboboxItem> DrpNguonVon
        {
            get => _drpNguonVon;
            set => SetProperty(ref _drpNguonVon, value);
        }

        private ComboboxItem _drpNguonVonSelected;
        public ComboboxItem DrpNguonVonSelected
        {
            get => _drpNguonVonSelected;
            set => SetProperty(ref _drpNguonVonSelected, value);
        }

        private string _iNamKeHoach;
        public string INamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }

        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set
            {
                if (SetProperty(ref _dNgayQuyetDinh, value))
                {
                    LoadDuAn();
                }
            }
        }

        #endregion

        public KeHoachVonUngDeXuatImportViewModel(ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonVonService,
            IVdtKhvPhanBoVonDonViService phanBoVonService,
            IVdtDmDonViThucHienDuAnService vdtDmDonViThucHienDuAnService,
            IVdtDaDuAnService duAnService,
            IVdtKhvKeHoachVonUngDxService keHoachVonUngService,
            FtpStorageService ftpStorageService,
            ILog logger)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _nsDonViService = nsDonViService;
            _nsNguonVonService = nsNguonVonService;
            _phanBoVonService = phanBoVonService;
            _vdtDmDonViThucHienDuAnService = vdtDmDonViThucHienDuAnService;
            _duAnService = duAnService;
            _keHoachVonUngService = keHoachVonUngService;
            _ftpStorageService = ftpStorageService;
            _logger = logger;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());                        
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(ShowError);
        }

        #region RelayCommand Event       
        public override void Init()
        {
            try
            {
                LoadComboBoxLoaiDonVi();
                GetNguonVon();
                LoadData();
                OnResetData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void LoadData(params object[] args)
        {
            SetValueDefault();
            LoadDuAn();
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

        private void OnResetData()
        {
            _filePath = string.Empty;
            _cbxLoaiDonViSelected = null;
            _drpNguonVonSelected = null;
            //_iNamKeHoach = null;
            _sSoQuyetDinh = null;
            //_dNgayQuyetDinh = null;            
            KeHoachVonUngDeXuat = new VdtKhvKeHoachVonUngDx();
            KeHoachVonUngDeXuatChiTiets = new ObservableCollection<VdtKhvKeHoachVonUngDxChiTietModel>();

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(DrpNguonVonSelected));
            //OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(SSoQuyetDinh));
            //OnPropertyChanged(nameof(DNgayQuyetDinh));            
            OnPropertyChanged(nameof(IsSelectedFile));            
            OnPropertyChanged(nameof(KeHoachVonUngDeXuat));
            OnPropertyChanged(nameof(KeHoachVonUngDeXuatChiTiets));            
        }

        private string OnProcessFile(bool bIsImport = false)
        {
            try
            {
                _lstErrChungTuChiTiet = new List<ImportErrorItem>();
                List<string> lstError = new List<string>();

                if (string.IsNullOrEmpty(FilePath))
                {
                    lstError.Add(Resources.ErrorFileEmpty);
                }
                if (!bIsImport)
                {
                    ValidateForm(ref lstError);
                }
                if (lstError.Any())
                {
                    string sMessError = string.Join(Environment.NewLine, lstError);
                    System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    FilePath = null;
                    OnPropertyChanged(nameof(FilePath));
                    return sMessError;
                }
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;
                var data = _importService.ProcessData<KeHoachVonUngImportModel>(FilePath);                
                var listDataImport = data.Data.Select(x => new VdtKhvKeHoachVonUngDxChiTietModel
                {
                    SMaDuAn = x.SMaDuAn,
                    STenDuAn = x.STenDuAn,
                    FGiaTriDeNghi = Convert.ToDouble(x.FGiaTriDeNghi),
                    SGhiChu = x.SGhiChu,
                    ImportStatus = x.ImportStatus,
                }).ToList();                
                KeHoachVonUngDeXuatChiTiets = new ObservableCollection<VdtKhvKeHoachVonUngDxChiTietModel>(listDataImport);
                _lstErrChungTuChiTiet = data.ImportErrors.ToList();

                int i = 1;
                foreach(var item in KeHoachVonUngDeXuatChiTiets)
                {
                    item.STT = i.ToString();
                    i++;
                    foreach(var duan in LstDuAn)
                    {
                        if(duan.sMaDuAn == item.SMaDuAn)
                        {
                            item.FTongMucDauTuPheDuyet = duan.fTongMucDauTuPheDuyet;
                            item.IIDDuAnID = duan.iID_DuAnID;
                            item.STrangThaiDuAnDangKy = duan.sTrangThaiDuAnDangKy;
                        }
                    }
                }

                string strErrorChungTu = string.Empty;
                if (!bIsImport)
                    ValidateChungTu(ref strErrorChungTu);
                if (!string.IsNullOrEmpty(strErrorChungTu))
                {
                    System.Windows.MessageBox.Show(strErrorChungTu, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return strErrorChungTu;
                }
                if (!bIsImport)
                {

                    ValidateChungTuChiTiet();
                }

                OnPropertyChanged(nameof(KeHoachVonUngDeXuatChiTiets));
                OnPropertyChanged(nameof(IsSaveData));
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return ex.Message;
            }
        }

        public override void OnSave(object obj)
        {
            try
            {
                List<string> lstError = new List<string>();
                ValidateForm(ref lstError);

                if (lstError.Any())
                {
                    string sMessError = string.Join(Environment.NewLine, lstError);
                    System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
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

                KeHoachVonUngDeXuat.Id = Guid.NewGuid();
                KeHoachVonUngDeXuat.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
                KeHoachVonUngDeXuat.IIdMaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
                KeHoachVonUngDeXuat.IIdNguonVonId = int.Parse(DrpNguonVonSelected.ValueItem);
                KeHoachVonUngDeXuat.SSoDeNghi = SSoQuyetDinh;
                KeHoachVonUngDeXuat.DNgayDeNghi = DNgayQuyetDinh;                
                KeHoachVonUngDeXuat.INamKeHoach = int.Parse(INamKeHoach);
                KeHoachVonUngDeXuat.BKhoa = false;
                KeHoachVonUngDeXuat.BActive = true;


                _keHoachVonUngService.Insert(KeHoachVonUngDeXuat, _sessionService.Current.Principal);

                //var lstData = KeHoachVonUngDeXuatChiTiets.Select(
                //    n =>
                //    {
                //        n.IIDDuAnID = _dicDuAn[n.SMaDuAn];
                //        return n;
                //    }).ToList();
                //var lstDataInsert = _mapper.Map<List<VdtKhvKeHoachVonUngDxChiTietModel>>(lstData);
                //bool isSucess = _phanBoVonService.CreatePhanBoVonChiTiet(KeHoachVonUngDeXuat.Id, lstDataInsert);

                List<string> messageBuilder = new List<string>();
                List<VdtKhvKeHoachVonUngDxChiTiet> lstDataChiTiet = new List<VdtKhvKeHoachVonUngDxChiTiet>();
                foreach (var item in KeHoachVonUngDeXuatChiTiets)
                {
                    VdtKhvKeHoachVonUngDxChiTiet itemData = ConvertDataInsert(item);
                    if (itemData == null)
                    {
                        messageBuilder.Add(Resources.MsgErrorMucLucNganSachNotExist);
                        break;
                    }
                    lstDataChiTiet.Add(itemData);
                }
                if (messageBuilder.Count != 0)
                {
                    MessageBox.Show(String.Join("\n", messageBuilder));
                    return;
                }
                bool isSucess = _keHoachVonUngService.InsertDetail(KeHoachVonUngDeXuat.Id, lstDataChiTiet);
                if (!isSucess)
                {
                    messageBuilder.Add(Resources.AlertDataError);
                    MessageBox.Show(String.Join("\n", messageBuilder));
                    return;
                }
                MessageBox.Show(Resources.MsgSaveDone);

                var entityModel = _mapper.Map<VdtKhvKeHoachVonUngDxModel>(KeHoachVonUngDeXuat);
                SavedAction?.Invoke(entityModel);

                var window = obj as Window;
                window.Close();
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

        private void ShowError(object param)
        {
            try
            {
                var importTabIndex = (ImportTabIndex)((int)param);
                int rowIndex;
                rowIndex = KeHoachVonUngDeXuatChiTiets.IndexOf(KeHoachVonUngDeXuatChiTietSelected);
                var errors = _lstErrChungTuChiTiet.Where(x => x.Row == rowIndex).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
                System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ValidateChungTuChiTiet()
        {
            int iRow = 0;
            foreach (var item in _keHoachVonUngDeXuatChiTiets)
            {
                iRow++;
                if (string.IsNullOrEmpty(item.SMaDuAn))
                {
                    _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                    {
                        ColumnName = "Mã dự án",
                        Error = Resources.MsgErrorDataEmpty,
                        Row = iRow - 1
                    });
                    item.ImportStatus = false;
                }
                else
                {
                    if (!_dicDuAn.ContainsKey(item.SMaDuAn))
                    {
                        _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                        {
                            ColumnName = "Mã dự án",
                            Error = Resources.MsgErrorProjectNotFound,
                            Row = iRow - 1
                        });
                        item.ImportStatus = false;
                    }                    
                }
            }
        }

        private void ValidateChungTu(ref string strError)
        {
            KeHoachVonUngDeXuat.SSoDeNghi = SSoQuyetDinh;
            KeHoachVonUngDeXuat.DNgayDeNghi = DateTime.Now;
            KeHoachVonUngDeXuat.SUserCreate = _sessionService.Current.Principal;
            KeHoachVonUngDeXuat.DDateCreate = DateTime.Now;
            KeHoachVonUngDeXuat.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
            KeHoachVonUngDeXuat.IIdMaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
            KeHoachVonUngDeXuat.INamKeHoach = int.Parse(INamKeHoach);
            KeHoachVonUngDeXuat.IIdNguonVonId = int.Parse(DrpNguonVonSelected.ValueItem);
        }

        private bool ValidateForm(ref List<string> lstError)
        {
            bool bError = false;

            if (string.IsNullOrEmpty(SSoQuyetDinh))
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Số kế hoạch"));
                bError = true;
            }
            if (DrpNguonVonSelected == null)
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Nguồn vốn"));
                bError = true;
            }
            if (CbxLoaiDonViSelected == null)
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Đơn vị quản lý"));
                bError = true;
            }

            int iNamKeHoach = 0;
            if (string.IsNullOrEmpty(INamKeHoach))
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Năm kế hoạch"));
                bError = true;
            }
            else if (!int.TryParse(INamKeHoach, out iNamKeHoach))
            {
                lstError.Add(string.Format(Resources.MsgErrorFormat, "Năm kế hoạch"));
                bError = true;
            }
            if (_keHoachVonUngService.CheckExistSoKeHoach(SSoQuyetDinh, _sessionService.Current.YearOfWork, null))
            {
                lstError.Add("Số quyết định đã bị trùng, vui lòng nhập số quyết định hợp lệ");
                bError = true;
            }

            return !bError;
        }

        private void LoadComboBoxLoaiDonVi(string iIdDonVi = null)
        {
            try
            {
                _lstDonViThucHienDuAnCreate = new List<VdtDmDonViThucHienDuAn>();
                _dicDonViThucHienDuAn = new Dictionary<string, Guid>();

                List<DonVi> lstDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();
                List<VdtDmDonViThucHienDuAn> lstDonViThucHienDuAn = _vdtDmDonViThucHienDuAnService.FindAll().ToList();
                foreach (var item in lstDonViThucHienDuAn)
                {
                    if (!string.IsNullOrEmpty(item.IIdMaDonVi) && !_dicDonViThucHienDuAn.ContainsKey(item.IIdMaDonVi))
                    {
                        _dicDonViThucHienDuAn.Add(item.IIdMaDonVi, item.IIdDonVi);
                    }
                }

                if (lstDonVi.Any(n => n.Loai == "0"))
                {
                    _iIdDonViParent = lstDonVi.FirstOrDefault(n => n.Loai == "0").Id;
                }

                var cbxLoaiDonViData = lstDonVi.Where(n => (string.IsNullOrEmpty(iIdDonVi) || n.IIDMaDonVi == iIdDonVi));
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
                cbxLoaiDonViData.Select(item =>
                {
                    if (lstDv.Contains(item.IIDMaDonVi))
                    {
                        lstUnitViaUser.Add(item);
                    }
                    return item;
                }).ToList();
                if (!lstUnitViaUser.Select(x => x.IIDMaDonVi).ToList().Contains(_sessionService.Current.IdDonVi))
                {
                    lstUnitViaUser.Add(lstDonVi.Where(x => x.IIDMaDonVi == _sessionService.Current.IdDonVi).FirstOrDefault());
                }

                _cbxLoaiDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(lstUnitViaUser);                
                OnPropertyChanged(nameof(CbxLoaiDonVi));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDuAn()
        {
            _dicDuAn = new Dictionary<string, Guid>();
            if (CbxLoaiDonViSelected == null || !DNgayQuyetDinh.HasValue) return;            
            var data = _keHoachVonUngService.GetDuAnInKeHoachVonUngDetail(CbxLoaiDonViSelected.ValueItem,
                DNgayQuyetDinh.Value, null);
            LstDuAn = _mapper.Map<ObservableCollection<DuAnDenghiThanhToanModel>>(data);

            foreach (var item in LstDuAn)
            {
                if (!string.IsNullOrEmpty(item.sMaDuAn) && !_dicDuAn.ContainsKey(item.sMaDuAn))
                {
                    _dicDuAn.Add(item.sMaDuAn, item.Id);
                }
            }
        }

        private void SetValueDefault()
        {
            SSoQuyetDinh = null;
            DNgayQuyetDinh = DateTime.Now;
            INamKeHoach = DateTime.Now.Year.ToString();
            CbxLoaiDonViSelected = null;
            DrpNguonVonSelected = null;
            FilePath = null;

            LstDuAn = new ObservableCollection<DuAnDenghiThanhToanModel>();
            OnPropertyChanged(nameof(LstDuAn));
            OnPropertyChanged(nameof(SSoQuyetDinh));
            OnPropertyChanged(nameof(DNgayQuyetDinh));
            OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(DrpNguonVonSelected));
            OnPropertyChanged(nameof(FilePath));
        }

        private void GetNguonVon(int? iIdNguonVon = null)
        {
            try
            {
                var cbxNguonVonData = _nsNguonVonService.FindNguonNganSach().OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
                _drpNguonVon = new ObservableCollection<ComboboxItem>(cbxNguonVonData);
                //DrpNguonVonSelected = _drpNguonVon.FirstOrDefault();
                OnPropertyChanged(nameof(DrpNguonVon));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private VdtKhvKeHoachVonUngDxChiTiet ConvertDataInsert(VdtKhvKeHoachVonUngDxChiTietModel data)
        {
            VdtKhvKeHoachVonUngDxChiTiet dataInsert = new VdtKhvKeHoachVonUngDxChiTiet();
            if (dataInsert == null) return null;
            dataInsert.Id = Guid.NewGuid();
            dataInsert.FGiaTriDeNghi = data.FGiaTriDeNghi;
            dataInsert.FTiGia = data.FTiGia;
            dataInsert.FTiGiaDonVi = data.FTiGiaDonVi;
            dataInsert.IIdDonViTienTeId = data.IIDDonViTienTeID;
            dataInsert.IIdDuAnId = data.IIDDuAnID;
            dataInsert.IIdKeHoachUngId = KeHoachVonUngDeXuat.Id;
            dataInsert.IIdTienTeId = data.IIDTienTeID;
            dataInsert.SGhiChu = data.SGhiChu;
            dataInsert.STrangThaiDuAnDangKy = data.STrangThaiDuAnDangKy;
            return dataInsert;
        }

        #endregion

    }
}
