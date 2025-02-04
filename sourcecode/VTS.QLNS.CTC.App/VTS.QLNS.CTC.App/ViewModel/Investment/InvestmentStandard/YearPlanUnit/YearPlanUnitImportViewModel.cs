using AutoMapper;
using FlexCel.XlsAdapter;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.VonNamDonVi;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.VonNamDonVi
{
    public class YearPlanUnitImportViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly IVdtKhvPhanBoVonDonViService _phanBoVonService;
        private readonly IVdtDmDonViThucHienDuAnService _vdtDmDonViThucHienDuAnService;
        private readonly IVdtDaDuAnService _duAnService;
        private readonly FtpStorageService _ftpStorageService;

        private List<ImportErrorItem> _lstErrChungTuChiTiet;
        private string _fileName;
        private Dictionary<string, Guid> _dicDuAn;
        private Dictionary<string, Guid> _dicDonViThucHienDuAn;
        private List<VdtDaDuAn> _lstDuAnCreate;
        private List<VdtDmDonViThucHienDuAn> _lstDonViThucHienDuAnCreate;
        private Guid? _iIdDonViParent;
        private readonly ILog _logger;

        public override Type ContentType => typeof(VonNamDonViImport);
        public override string Name => "IMPORT DỮ LIỆU KẾ HOẠCH VỐN NĂM ĐỄ XUẤT";
        public override string Description => "Chọn file Excel, thực hiện kiểm tra và import dữ liệu kế hoạch vốn năm";

        #region Items
        private VdtKhvPhanBoVonDonVi _phanBoVon = new VdtKhvPhanBoVonDonVi();
        public VdtKhvPhanBoVonDonVi PhanBoVon
        {
            get => _phanBoVon;
            set => SetProperty(ref _phanBoVon, value);
        }

        private ObservableCollection<PhanBoVonDonViChiTietModel> _phanBoVonChiTiets;
        public ObservableCollection<PhanBoVonDonViChiTietModel> PhanBoVonChiTiets
        {
            get => _phanBoVonChiTiets;
            set => SetProperty(ref _phanBoVonChiTiets, value);
        }

        private PhanBoVonDonViChiTietModel _phanBoVonChiTietSelected;
        public PhanBoVonDonViChiTietModel PhanBoVonChiTietSelected
        {
            get => _phanBoVonChiTietSelected;
            set => SetProperty(ref _phanBoVonChiTietSelected, value);
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
            get => PhanBoVon != null && PhanBoVonChiTiets != null && PhanBoVonChiTiets.Count > 0 && !PhanBoVonChiTiets.Any(x => !x.ImportStatus);
            set => SetProperty(ref _isSaveData, value);
        }

        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set
            {
                SetProperty(ref _cbxLoaiDonViSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
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
                OnPropertyChanged(nameof(VouchersVisibility));
                OnPropertyChanged(nameof(VoucherModifiedVisibility));
            }
        }
        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
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
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }

        private string _sNguoiLap;
        public string SNguoiLap
        {
            get => _sNguoiLap;
            set => SetProperty(ref _sNguoiLap, value);
        }

        private string _sTruongPhong;
        public string STruongPhong
        {
            get => _sTruongPhong;
            set => SetProperty(ref _sTruongPhong, value);
        }

        public Visibility VouchersVisibility
        {
            get => (_drpLoaiImportSelected == null || _drpLoaiImportSelected != null && _drpLoaiImportSelected.ValueItem.Equals("1")) ? Visibility.Collapsed : Visibility.Visible;
        }

        public Visibility VoucherModifiedVisibility
        {
            get => (_drpLoaiImportSelected != null && _drpLoaiImportSelected.ValueItem.Equals("2")) ? Visibility.Collapsed : Visibility.Visible;
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

        public YearPlanUnitImportViewModel(ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonVonService,
            IVdtDaDuAnService duAnService,
            IVdtKhvPhanBoVonDonViService phanBoVonService,
            IVdtDmDonViThucHienDuAnService vdtDmDonViThucHien,
            FtpStorageService ftpStorageService,
            ILog logger)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _nsDonViService = nsDonViService;
            _nsNguonVonService = nsNguonVonService;
            _phanBoVonService = phanBoVonService;
            _duAnService = duAnService;
            _ftpStorageService = ftpStorageService;
            _vdtDmDonViThucHienDuAnService = vdtDmDonViThucHien;
            _logger = logger;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(ShowError);
            GetFileFtpCommand = new RelayCommand(obj => OnGetFileFtpCommand());
            DownloadFileFtpServer = new RelayCommand(obj => OnDownloadFileFtpServer());

        }

        #region RelayCommand
        public override void Init()
        {
            try
            {
                LoadComboBoxLoaiDonVi();
                GetNguonVon();
                GetDuAn();
                GetVouchers();
                GetLoaiImport();
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

            if (DrpLoaiImports != null && DrpLoaiImports.Count() > 0)
            {
                DrpLoaiImportSelected = DrpLoaiImports.FirstOrDefault();
            }
        }
        private void GetVouchers()
        {
            try
            {
                var predicate = PredicateBuilder.True<VdtKhvPhanBoVonDonVi>();
                predicate = predicate.And(x => x.BActive.Value);
                predicate = predicate.And(x => string.IsNullOrEmpty(x.STongHop));
                List<VdtKhvPhanBoVonDonVi> lstQuery = _phanBoVonService.FindByCondition(predicate).ToList();
                DrpVouchers = _mapper.Map<ObservableCollection<ComboboxItem>>(lstQuery);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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
                    return sMessError;
                }

                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;
                var data = _importService.ProcessData<PhanBoVonDonViImportModel>(FilePath);
                PhanBoVonChiTiets = _mapper.Map<ObservableCollection<PhanBoVonDonViChiTietModel>>(data.Data);
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

                OnPropertyChanged(nameof(PhanBoVonChiTiets));
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

                if (DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("1"))
                {
                    PhanBoVon.Id = Guid.NewGuid();
                    PhanBoVon.SSoQuyetDinh = SSoQuyetDinh;
                    PhanBoVon.DNgayQuyetDinh = DateTime.Now;
                    PhanBoVon.SNguoiLap = SNguoiLap;
                    PhanBoVon.STruongPhong = STruongPhong;
                    PhanBoVon.INamKeHoach = Int32.Parse(INamKeHoach);
                    PhanBoVon.IIdNguonVonId = Int32.Parse(_drpNguonVonSelected.ValueItem);
                    PhanBoVon.IIdDonViQuanLyId = Guid.Parse(_cbxLoaiDonViSelected.HiddenValue);
                    PhanBoVon.IIdMaDonViQuanLy = _cbxLoaiDonViSelected.ValueItem;
                    PhanBoVon.IIdParentId = null;
                    PhanBoVon.BActive = true;
                    PhanBoVon.BIsGoc = true;
                    PhanBoVon.SUserCreate = _sessionService.Current.Principal;
                    PhanBoVon.DDateCreate = DateTime.Now;

                    string sError = string.Empty;

                    _phanBoVonService.Insert(PhanBoVon, _sessionService.Current.Principal, ref sError);

                    var lstData = PhanBoVonChiTiets.Select(
                    n =>
                    {
                        n.iID_DuAnID = _dicDuAn[n.sMaDuAn];
                        return n;
                    }).ToList();
                    var lstDataInsert = _mapper.Map<List<VdtKhvPhanBoVonDonViChiTiet>>(lstData);
                    bool isSucess = _phanBoVonService.CreatePhanBoVonChiTiet(PhanBoVon.Id, lstDataInsert);
                    if (!isSucess)
                    {
                        System.Windows.MessageBox.Show(string.Format(Resources.MsgErrorFormat, "Mục lục ngân sách"));
                        _phanBoVonService.DeletePhanBoVonDonVi(PhanBoVon);
                        return;
                    }
                    System.Windows.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Question);
                }
                else if (DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("2"))
                {
                    if ((PhanBoVonChiTiets != null && PhanBoVonChiTiets.Count() > 0 && PhanBoVonChiTiets.FirstOrDefault().IdChungTu != Guid.Parse(_drpVoucherSelected.ValueItem)))
                    {
                        System.Windows.MessageBox.Show(Resources.VoucherImportErrors, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    VdtKhvPhanBoVonDonVi itemParent = _phanBoVonService.FindById(Guid.Parse(_drpVoucherSelected.ValueItem));
                    List<VdtKhvPhanBoVonDonViChiTiet> itemChiTietParent = _phanBoVonService.GetPhanBoVonDonViByIdPhanBoVon(Guid.Parse(_drpVoucherSelected.ValueItem)).ToList();
                    List<VdtKhvPhanBoVonDonViChiTiet> itemChiTietNew = _mapper.Map<List<VdtKhvPhanBoVonDonViChiTiet>>(PhanBoVonChiTiets);

                    List<PhanBoVonDonViQuery> lstQuery = _phanBoVonService.GetPhanBoVonDonViDieuChinh(itemParent != null ? itemParent.Id.ToString() : Guid.Empty.ToString()).ToList();
                    if (lstQuery != null && lstQuery.Count() == 0)
                    {
                        System.Windows.MessageBox.Show(Resources.VoucherAdjustKhvn, Resources.Alert);
                        return;
                    }

                    VdtKhvPhanBoVonDonVi itemNew = new VdtKhvPhanBoVonDonVi();
                    itemParent.CloneObj(itemNew);
                    itemNew.DDateCreate = DateTime.Now;
                    itemNew.SUserCreate = _sessionService.Current.Principal;
                    itemNew.BIsGoc = false;
                    itemNew.BActive = true;
                    itemNew.Id = Guid.NewGuid();
                    itemNew.IIdParentId = itemParent.Id;
                    itemNew.SSoQuyetDinh = SSoQuyetDinh;

                    var result = (from rs in itemChiTietNew
                                  join pr in itemChiTietParent on rs.IIdPhanBoVonDonVi equals pr.IIdPhanBoVonDonVi
                                  select new VdtKhvPhanBoVonDonViChiTiet()
                                  {
                                      Id = pr.Id,
                                      IIdPhanBoVonDonVi = itemNew.Id,
                                      IIdLoaiCongTrinhId = pr.IIdLoaiCongTrinhId,
                                      IIdDuAnId = pr.IIdDuAnId,
                                      SMaDuAn = pr.SMaDuAn,
                                      FTongMucDauTuDuocDuyet = rs.FTongMucDauTuDuocDuyet,
                                      FLuyKeVonNamTruoc = rs.FLuyKeVonNamTruoc,
                                      FKeHoachVonDuocDuyetNamNay = rs.FKeHoachVonDuocDuyetNamNay,
                                      FVonKeoDaiCacNamTruoc = rs.FVonKeoDaiCacNamTruoc,
                                      FUocThucHien = rs.FUocThucHien,
                                      FThuHoiVonUngTruoc = rs.FThuHoiVonUngTruoc,
                                      FThanhToan = rs.FThanhToan,
                                      STrangThaiDuAnDangKy = pr.STrangThaiDuAnDangKy,
                                      IIdParentId = pr.IIdParentId,
                                      BActive = true,
                                      ILoaiDuAn = pr.ILoaiDuAn
                                  }).ToList();

                    result = result.GroupBy(x => x.Id).Select(grp => grp.FirstOrDefault()).ToList();

                    _phanBoVonService.CreateVoucherImports(itemNew, result);
                }

                var entityModel = _mapper.Map<PhanBoVonDonViModel>(PhanBoVon);
                SavedAction?.Invoke(entityModel);

                var window = obj as Window;
                window.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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
            _iNamKeHoach = null;
            _sSoQuyetDinh = null;
            _dNgayQuyetDinh = null;
            _sNguoiLap = null;
            _sTruongPhong = null;
            _isSelectedFile = false;
            _drpLoaiImportSelected = null;
            LstFile = new ObservableCollection<FileFtpModel>();
            PhanBoVon = new VdtKhvPhanBoVonDonVi();
            PhanBoVonChiTiets = new ObservableCollection<PhanBoVonDonViChiTietModel>();

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(DrpNguonVonSelected));
            OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(SSoQuyetDinh));
            OnPropertyChanged(nameof(DNgayQuyetDinh));
            OnPropertyChanged(nameof(SNguoiLap));
            OnPropertyChanged(nameof(STruongPhong));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(DrpLoaiImportSelected));
            OnPropertyChanged(nameof(PhanBoVon));
            OnPropertyChanged(nameof(PhanBoVonChiTiets));
            OnPropertyChanged(nameof(LstFile));
        }

        private void ShowError(object param)
        {
            try
            {
                var importTabIndex = (ImportTabIndex)((int)param);
                int rowIndex;
                rowIndex = PhanBoVonChiTiets.IndexOf(PhanBoVonChiTietSelected);
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
            if (string.IsNullOrEmpty(SSoQuyetDinh))
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Số kế hoạch"));
                bError = true;
            }

            if (DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("1"))
            {
                int iNamKeHoach = 0;
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
            }
            else if (DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("2"))
            {
                if (DrpVoucherSelected == null)
                {
                    lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Chứng từ"));
                    bError = true;
                }
            }

            if (DrpLoaiImportSelected != null && CbxLoaiDonViSelected != null && DrpNguonVonSelected != null)
            {
                var predicate = PredicateBuilder.True<VdtKhvPhanBoVonDonVi>();
                predicate = predicate.And(x => x.INamKeHoach == int.Parse(INamKeHoach));
                predicate = predicate.And(x => x.IIdMaDonViQuanLy == CbxLoaiDonViSelected.ValueItem);
                predicate = predicate.And(x => x.IIdNguonVonId == int.Parse(DrpNguonVonSelected.ValueItem));
                List<VdtKhvPhanBoVonDonVi> lstQuery = _phanBoVonService.FindByCondition(predicate).ToList();

                if (lstQuery != null && lstQuery.Count() > 0)
                {
                    lstError.Add(string.Format(Resources.VoucherImportKhvnWrn, CbxLoaiDonViSelected.ValueItem, DrpNguonVonSelected.ValueItem, INamKeHoach));
                    bError = true;
                }
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
                CbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault();
                OnPropertyChanged(nameof(CbxLoaiDonVi));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void GetNguonVon(int? iIdNguonVon = null)
        {
            try
            {
                var cbxNguonVonData = _nsNguonVonService.FindNguonNganSach().OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
                _drpNguonVon = new ObservableCollection<ComboboxItem>(cbxNguonVonData);
                DrpNguonVonSelected = _drpNguonVon.FirstOrDefault();
                OnPropertyChanged(nameof(DrpNguonVon));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void GetDuAn()
        {
            try
            {
                _lstDuAnCreate = new List<VdtDaDuAn>();
                _dicDuAn = new Dictionary<string, Guid>();
                var lstData = _duAnService.FindAll();
                foreach (var item in lstData)
                {
                    if (!string.IsNullOrEmpty(item.SMaDuAn) && !_dicDuAn.ContainsKey(item.SMaDuAn))
                    {
                        _dicDuAn.Add(item.SMaDuAn, item.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ValidateChungTu(ref string strError)
        {
            PhanBoVon.SSoQuyetDinh = SSoQuyetDinh;
            PhanBoVon.DNgayQuyetDinh = DateTime.Now;
            PhanBoVon.SUserCreate = _sessionService.Current.Principal;
            PhanBoVon.SNguoiLap = _sessionService.Current.Principal;
            PhanBoVon.BIsCanBoDuyet = false;
            PhanBoVon.BIsDuyet = false;
            PhanBoVon.DDateCreate = DateTime.Now;

            if (DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("1"))
            {
                PhanBoVon.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
                PhanBoVon.IIdMaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
                PhanBoVon.INamKeHoach = int.Parse(INamKeHoach);
                PhanBoVon.IIdNguonVonId = int.Parse(DrpNguonVonSelected.ValueItem);
            }
            else
            {
                VdtKhvPhanBoVonDonVi itemQuery = _phanBoVonService.FindById(Guid.Parse(_drpVoucherSelected.ValueItem));
                if (itemQuery != null)
                {
                    PhanBoVon.IIdDonViQuanLyId = itemQuery.IIdDonViQuanLyId;
                    PhanBoVon.IIdMaDonViQuanLy = itemQuery.IIdMaDonViQuanLy;
                    PhanBoVon.INamKeHoach = itemQuery.INamKeHoach;
                    PhanBoVon.IIdNguonVonId = itemQuery.IIdNguonVonId;
                }
            }
        }
        private void ValidateChungTuChiTiet()
        {
            int iRow = 0;
            foreach (var item in _phanBoVonChiTiets)
            {
                iRow++;
                if (string.IsNullOrEmpty(item.sMaDuAn))
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
                    if (!_dicDuAn.ContainsKey(item.sMaDuAn))
                    {
                        _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                        {
                            ColumnName = "Mã dự án",
                            Error = Resources.MsgErrorProjectNotFound,
                            Row = iRow - 1
                        });
                        item.ImportStatus = false;
                    }
                    else
                    {
                        Guid iDDuAn = _dicDuAn[item.sMaDuAn];
                        VdtDaDuAn itemDuAn = _duAnService.FindById(iDDuAn);
                        VdtKhvPhanBoVonDonVi itemQuery = _phanBoVonService.FindById(DrpVoucherSelected != null ? Guid.Parse(DrpVoucherSelected.ValueItem) : Guid.Empty);
                        if (itemDuAn != null && ((DrpLoaiImportSelected.ValueItem.Equals("1") && CbxLoaiDonViSelected.ValueItem != itemDuAn.IIdMaDonViThucHienDuAn)
                            || (itemQuery != null && DrpLoaiImportSelected.ValueItem.Equals("2") && itemDuAn.IIdMaDonViThucHienDuAn != itemQuery.IIdMaDonViQuanLy)))
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
        }
        private void OnGetFileFtpCommand()
        {
            if (CbxLoaiDonViSelected == null || string.IsNullOrEmpty(INamKeHoach))
            {

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng nhập năm kế hoạch");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                return;
            }
            var btmTenDonVi = StringUtils.UCS2Convert(CbxLoaiDonViSelected.ValueItem);
            string sTime = string.Format("{0}", INamKeHoach);
            var strUrl = string.Format("{0}/{1}/{2}", btmTenDonVi, ConstantUrlPathPhanHe.UrlKhvndxWinformSend, sTime);
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
                    _ftpStorageService.DowLoadFileFtpGiveLocal(urlUrIDownLoad, fileName);
                    FilePath = Path.Combine(IOExtensions.ApplicationPath, ConstantUrlPathPhanHe.UrlFolderFile, item.SNameFile);
                }
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                OnProcessFile(true);

            }, (s, e) =>
            {
                IsLoading = false;
            });
        }

        private void CreateDonVi(DonViByPhanBoVonImportModel item, Dictionary<string, DonViByPhanBoVonImportModel> dicDonViImport)
        {
            try
            {
                VdtDmDonViThucHienDuAn dataInsert = new VdtDmDonViThucHienDuAn();
                dataInsert.IIdDonVi = Guid.NewGuid();
                if (!string.IsNullOrEmpty(item.Id_DonVi_Parent))
                {
                    if (!_dicDonViThucHienDuAn.ContainsKey(item.Id_DonVi_Parent))
                    {
                        CreateDonVi(dicDonViImport[item.Id_DonVi_Parent], dicDonViImport);
                    }
                    dataInsert.IIdDonViCha = _dicDonViThucHienDuAn[item.Id_DonVi_Parent];
                }
                else
                {
                    dataInsert.IIdDonViCha = _iIdDonViParent;
                }

                dataInsert.STenDonVi = item.TenDonVi;
                dataInsert.ICapDonVi = Int32.Parse(item.ICapDonVi);
                if (!_dicDonViThucHienDuAn.ContainsKey(item.Id_DonVi))
                {
                    _dicDonViThucHienDuAn.Add(item.Id_DonVi, dataInsert.IIdDonVi);
                    _lstDonViThucHienDuAnCreate.Add(dataInsert);
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
