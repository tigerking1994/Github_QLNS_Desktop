using AutoMapper;
using FlexCel.XlsAdapter;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.KeHoachChiQuy;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.KeHoachVonUngDeXuat;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Utility.Enum;
using System.Globalization;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachChiQuy
{
    public class KeHoachChiQuyImportViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly IVdtNcNhuCauChiService _ncChiService;
        private readonly ILog _logger;
        private readonly IVdtDmDonViThucHienDuAnService _vdtDmDonViThucHienDuAnService;

        private string _fileName;
        private List<ImportErrorItem> _lstErrChungTuChiTiet;
        private List<VdtDmDonViThucHienDuAn> _lstDonViThucHienDuAnCreate;
        private Dictionary<string, Guid> _dicDonViThucHienDuAn;
        private Guid? _iIdDonViParent;
        private Dictionary<string, Guid> _dicDuAn;
        public override Type ContentType => typeof(KeHoachChiQuyImport);
        public override string Name => "IMPORT DỮ LIỆU KẾ HOẠCH CHI QUÝ";
        public override string Description => "Chọn file Excel, thực hiện kiểm tra và import dữ liệu kế hoạch chi quý";

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

        private ObservableCollection<VdtNcNhuCauChiChiTietModel> _keHoachChiQuyChiTiets;
        public ObservableCollection<VdtNcNhuCauChiChiTietModel> KeHoachChiQuyChiTiets
        {
            get => _keHoachChiQuyChiTiets;
            set => SetProperty(ref _keHoachChiQuyChiTiets, value);
        }


        private VdtNcNhuCauChiChiTietModel _keHoachChiQuyChiTietSelected;
        public VdtNcNhuCauChiChiTietModel KeHoachChiQuyChiTietSelected
        {
            get => _keHoachChiQuyChiTietSelected;
            set => SetProperty(ref _keHoachChiQuyChiTietSelected, value);
        }

        private VdtNcNhuCauChi _keHoachChiQuy = new VdtNcNhuCauChi();
        public VdtNcNhuCauChi KeHoachChiQuy
        {
            get => _keHoachChiQuy;
            set => SetProperty(ref _keHoachChiQuy, value);
        }

        private ObservableCollection<VdtNcNhuCauChiChiTietModel> _lstDuAn;
        public ObservableCollection<VdtNcNhuCauChiChiTietModel> LstDuAn
        {
            get => _lstDuAn;
            set => SetProperty(ref _lstDuAn, value);
        }

        public bool _isSaveData;
        public bool IsSaveData
        {
            get => KeHoachChiQuy != null && KeHoachChiQuyChiTiets != null && KeHoachChiQuyChiTiets.Count > 0 && !KeHoachChiQuyChiTiets.Any(x => !x.ImportStatus);
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
                    GetKinhPhiCucTaiChinhCap();
                }
            }
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
            set
            {
                if (SetProperty(ref _drpNguonVonSelected, value))
                {
                    GetKinhPhiCucTaiChinhCap();
                }
            }
        }

        private string _sSoDeNghi;
        public string SSoDeNghi
        {
            get => _sSoDeNghi;
            set => SetProperty(ref _sSoDeNghi, value);
        }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }

        private int? _iNamKeHoach;
        public int? INamKeHoach
        {
            get => _iNamKeHoach;
            set
            {
                if (SetProperty(ref _iNamKeHoach, value))
                {
                    GetKinhPhiCucTaiChinhCap();
                }
            }
        }

        private string _sNoiDung;
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }

        private string _sNguoiLap;
        public string SNguoiLap
        {
            get => _sNguoiLap;
            set => SetProperty(ref _sNguoiLap, value);
        }

        private ComboboxItem _cbxQuySelected;
        public ComboboxItem CbxQuySelected
        {
            get => _cbxQuySelected;
            set
            {
                if (SetProperty(ref _cbxQuySelected, value))
                {
                    GetKinhPhiCucTaiChinhCap();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxQuy;
        public ObservableCollection<ComboboxItem> CbxQuy
        {
            get => _cbxQuy;
            set => SetProperty(ref _cbxQuy, value);
        }

        private double? _fQuyTruocChuaGiaiNgan;
        public double? FQuyTruocChuaGiaiNgan
        {
            get => _fQuyTruocChuaGiaiNgan;
            set => SetProperty(ref _fQuyTruocChuaGiaiNgan, value);
        }

        private double? _fGiaiNganQuyNay;
        public double? FGiaiNganQuyNay
        {
            get => _fGiaiNganQuyNay;
            set => SetProperty(ref _fGiaiNganQuyNay, value);
        }

        private double? _fThucHienGiaiNgan;
        public double? FThucHienGiaiNgan
        {
            get => _fThucHienGiaiNgan;
            set => SetProperty(ref _fThucHienGiaiNgan, value);
        }

        private double? _fKinhPhiChuyenQuySau;
        public double? FKinhPhiChuyenQuySau
        {
            get => _fKinhPhiChuyenQuySau;
            set => SetProperty(ref _fKinhPhiChuyenQuySau, value);
        }

        private double? _fKinhPhiCapQuyToi;
        public double? FKinhPhiCapQuyToi
        {
            get => _fKinhPhiCapQuyToi;
            set => SetProperty(ref _fKinhPhiCapQuyToi, value);
        }
        #endregion

        public KeHoachChiQuyImportViewModel(ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonVonService,
            IVdtDmDonViThucHienDuAnService vdtDmDonViThucHienDuAnService,
            IVdtNcNhuCauChiService ncChiService,
            ILog logger)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _nsDonViService = nsDonViService;
            _nsNguonVonService = nsNguonVonService;
            _vdtDmDonViThucHienDuAnService = vdtDmDonViThucHienDuAnService;
            _ncChiService = ncChiService;
            _logger = logger;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
        }

        #region RelayCommand Event

        public override void Init()
        {
            try
            {
                LoadComboBoxLoaiDonVi();
                GetNguonVon();
                LoadData();
                LoadQuy();
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
            //LoadDuAn();
        }        

        private void SetValueDefault()
        {
            SSoDeNghi = null;
            DNgayQuyetDinh = null;
            INamKeHoach = null;
            CbxLoaiDonViSelected = null;
            DrpNguonVonSelected = null;
            FilePath = null;
            FQuyTruocChuaGiaiNgan = null;
            FGiaiNganQuyNay = null;
            FThucHienGiaiNgan = null;
            FKinhPhiChuyenQuySau = null;
            FKinhPhiCapQuyToi = null;

            //LstDuAn = new ObservableCollection<DuAnDenghiThanhToanModel>();
            //OnPropertyChanged(nameof(LstDuAn));
            OnPropertyChanged(nameof(SSoDeNghi));
            OnPropertyChanged(nameof(DNgayQuyetDinh));
            OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(DrpNguonVonSelected));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(FQuyTruocChuaGiaiNgan));
            OnPropertyChanged(nameof(FGiaiNganQuyNay));
            OnPropertyChanged(nameof(FThucHienGiaiNgan));
            OnPropertyChanged(nameof(FKinhPhiChuyenQuySau));
            OnPropertyChanged(nameof(FKinhPhiCapQuyToi));
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
                var data = _importService.ProcessData<KeHoachChiQuyImportModel>(FilePath);
                var listDataImport = data.Data.Select(x => new VdtNcNhuCauChiChiTietModel
                {
                    iStt = Convert.ToInt32(x.iStt),
                    sMaDuAn = x.SMaDuAn,
                    sTenDuAn = x.STenDuAn,
                    sLoaiThanhToan = x.SLoaiThanhToan,
                    fGiaTriDeNghi = x.FGiaTriDeNghi != "" ? Double.Parse(x.FGiaTriDeNghi) : 0,
                    fKeHoachVonNam = x.fKeHoachVonNam != "" ? Double.Parse(x.fKeHoachVonNam) : 0,
                    fThanhToanKLHTQuyNay = x.fThanhToanKLHTQuyNay != "" ? Double.Parse(x.fThanhToanKLHTQuyNay) : 0,
                    fThanhToanKLHTQuyTruoc = x.fThanhToanKLHTQuyTruoc != "" ? Double.Parse(x.fThanhToanKLHTQuyTruoc) : 0,
                    fThanhToanTamUngQuyNay = x.fThanhToanTamUngQuyNay != "" ? Double.Parse(x.fThanhToanTamUngQuyNay) : 0,
                    fThanhToanTamUngQuyTruoc = x.fThanhToanTamUngQuyTruoc != "" ? Double.Parse(x.fThanhToanTamUngQuyTruoc) : 0,
                    fThuHoiUng = x.fThuHoiUng != "" ? Double.Parse(x.fThuHoiUng) : 0,
                    fTongQuyNay = x.fTongQuyNay != "" ? Double.Parse(x.fTongQuyNay) : 0,
                    fTongQuyTruoc = x.fTongQuyTruoc != "" ? Double.Parse(x.fTongQuyTruoc) : 0,
                    sGhiChu = x.SGhiChu,
                    ImportStatus = x.ImportStatus,
                }).ToList();

                KeHoachChiQuyChiTiets = new ObservableCollection<VdtNcNhuCauChiChiTietModel>(listDataImport);
                _lstErrChungTuChiTiet = data.ImportErrors.ToList();

                int i = 1;
                foreach (var item in KeHoachChiQuyChiTiets)
                {
                    item.iStt = i;
                    i++;
                    foreach (var duan in LstDuAn)
                    {
                        if (duan.sMaDuAn == item.sMaDuAn)
                        {
                            item.sLoaiThanhToan = duan.sLoaiThanhToan;
                            item.iID_DuAnId = duan.iID_DuAnId;
                            item.fKeHoachVonNam = duan.fKeHoachVonNam;
                            item.fTongQuyTruoc = duan.fTongQuyTruoc;
                            item.fThanhToanKLHTQuyTruoc = duan.fThanhToanKLHTQuyTruoc;
                            item.fThanhToanTamUngQuyTruoc = duan.fThanhToanTamUngQuyTruoc;
                            item.fTongQuyNay = duan.fTongQuyNay;
                            item.fThanhToanKLHTQuyNay = duan.fThanhToanKLHTQuyNay;
                            item.fThuHoiUng = duan.fThuHoiUng;
                            item.fThanhToanTamUngQuyNay = duan.fThanhToanTamUngQuyNay;
                            item.fSoConGiaiNganNam = duan.fSoConGiaiNganNam;                            
                        }
                    }
                }

                string strErrorChungTu = string.Empty;
                if (!bIsImport)
                    //ValidateChungTu(ref strErrorChungTu);
                if (!string.IsNullOrEmpty(strErrorChungTu))
                {
                    System.Windows.MessageBox.Show(strErrorChungTu, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return strErrorChungTu;
                }
                if (!bIsImport)
                {

                    //ValidateChungTuChiTiet();
                }

                OnPropertyChanged(nameof(KeHoachChiQuyChiTiets));
                OnPropertyChanged(nameof(IsSaveData));
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return ex.Message;
            }
        }

        private void OnResetData()
        {
            _filePath = string.Empty;
            //_cbxLoaiDonViSelected = null;
            //_drpNguonVonSelected = null;
            //_iNamKeHoach = null;
            //_sSoQuyetDinh = null;
            //_dNgayQuyetDinh = null;            
            KeHoachChiQuy = new VdtNcNhuCauChi();
            KeHoachChiQuyChiTiets = new ObservableCollection<VdtNcNhuCauChiChiTietModel>();

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(DrpNguonVonSelected));
            //OnPropertyChanged(nameof(INamKeHoach));
            //OnPropertyChanged(nameof(SSoQuyetDinh));
            //OnPropertyChanged(nameof(DNgayQuyetDinh));            
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(KeHoachChiQuy));
            OnPropertyChanged(nameof(KeHoachChiQuyChiTiets));
        }

        #endregion

        #region Helper
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
        private void GetNguonVon(int? iIdNguonVon = null)
        {
            try
            {
                var cbxNguonVonData = _nsNguonVonService.FindNguonNganSach().OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
                _drpNguonVon = new ObservableCollection<ComboboxItem>(cbxNguonVonData);                
                OnPropertyChanged(nameof(DrpNguonVon));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadQuy()
        {
            List<ComboboxItem> data = new List<ComboboxItem>();
            data.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_1, ValueItem = ((int)LoaiQuyEnum.Type.QUY_1).ToString() });
            data.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_2, ValueItem = ((int)LoaiQuyEnum.Type.QUY_2).ToString() });
            data.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_3, ValueItem = ((int)LoaiQuyEnum.Type.QUY_3).ToString() });
            data.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_4, ValueItem = ((int)LoaiQuyEnum.Type.QUY_4).ToString() });
            CbxQuy = new ObservableCollection<ComboboxItem>(data);
            OnPropertyChanged(nameof(CbxQuy));
        }
        private void GetKinhPhiCucTaiChinhCap()
        {
            if (!INamKeHoach.HasValue || CbxLoaiDonViSelected == null || DrpNguonVonSelected == null || CbxQuySelected == null)
            {
                FQuyTruocChuaGiaiNgan = 0;
                FGiaiNganQuyNay = 0;
                FThucHienGiaiNgan = 0;
                FKinhPhiChuyenQuySau = 0;
            }
            else
            {
                var data = _ncChiService.GetKinhPhiCucTaiChinhCap(INamKeHoach.Value,
                    CbxLoaiDonViSelected.ValueItem,
                    int.Parse(DrpNguonVonSelected.ValueItem),
                    int.Parse(CbxQuySelected.ValueItem));
                if (data != null)
                {
                    FQuyTruocChuaGiaiNgan = data.fQuyTruocChuaGiaiNgan;
                    FGiaiNganQuyNay = data.fQuyNayDuocCap;
                    FThucHienGiaiNgan = data.fGiaiNganQuyNay;
                    FKinhPhiChuyenQuySau = data.fChuaGiaiNganChuyenQuySau;
                }

                _dicDuAn = new Dictionary<string, Guid>();
                var dataDetail = _ncChiService.GetNhuCauChiDetail(CbxLoaiDonViSelected.ValueItem, INamKeHoach.Value, int.Parse(DrpNguonVonSelected.ValueItem), int.Parse(CbxQuySelected.ValueItem));
                LstDuAn = _mapper.Map<ObservableCollection<VdtNcNhuCauChiChiTietModel>>(dataDetail);
                foreach (var item in LstDuAn)
                {
                    if (!string.IsNullOrEmpty(item.sMaDuAn) && !_dicDuAn.ContainsKey(item.sMaDuAn))
                    {
                        _dicDuAn.Add(item.sMaDuAn, item.Id);
                    }
                }
                OnPropertyChanged(nameof(LstDuAn));
            }
            OnPropertyChanged(nameof(FQuyTruocChuaGiaiNgan));
            OnPropertyChanged(nameof(FGiaiNganQuyNay));
            OnPropertyChanged(nameof(FThucHienGiaiNgan));
            OnPropertyChanged(nameof(FKinhPhiChuyenQuySau));
        }

        private bool ValidateForm(ref List<string> lstError)
        {
            bool bError = false;

            if (CbxLoaiDonViSelected == null)
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Đơn vị quản lý"));
                bError = true;
            }
            if (string.IsNullOrEmpty(SSoDeNghi))
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Số đề nghị"));
                bError = true;
            }
            if (DNgayQuyetDinh == null)
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Ngày đề nghị"));
                bError = true;
            }
            if (DrpNguonVonSelected == null)
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Nguồn vốn"));
                bError = true;
            }
            if (CbxQuySelected == null)
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Quý"));
                bError = true;
            }
            
            if (!INamKeHoach.HasValue || INamKeHoach.Value == 0)
            {
                lstError.Add(string.Format(Resources.MsgInputError, "Năm kế hoạch"));
                bError = true;
            }            
            if (_ncChiService.CheckExistSoKeHoach(SSoDeNghi, null))
            {
                lstError.Add("Số đề nghị đã bị trùng, vui lòng nhập số đề nghị hợp lệ");
                bError = true;
            }

            return !bError;
        }
        public override void OnSave(object obj)
        {
            if (!Validate()) return;
            List<VdtNcNhuCauChiChiTiet> lstData = new List<VdtNcNhuCauChiChiTiet>();
            Guid IdNCCQ = Guid.NewGuid();
            VdtNcNhuCauChi data = new VdtNcNhuCauChi();
            data.Id = IdNCCQ;
            data.SSoDeNghi = SSoDeNghi;
            data.DDateCreate = DNgayQuyetDinh;
            data.IIdNguonVonId = Convert.ToInt32(DrpNguonVonSelected.ValueItem);
            data.IQuy = Convert.ToInt32(CbxQuySelected.ValueItem);
            data.SNguoiLap = SNguoiLap;
            data.INamKeHoach = INamKeHoach;
            data.IIdMaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
            data.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
            data.SNoiDung = SNoiDung;
            data.SUserCreate = _sessionService.Current.Principal;
            _ncChiService.InsertKeHoachChiQuy(data);
            foreach (var item in KeHoachChiQuyChiTiets.Where(n => !n.IsDeleted && n.fGiaTriDeNghi != 0))
            {
                lstData.Add(ConvertData(item, IdNCCQ));
            }
            _ncChiService.InsertDetailDataImport(lstData);
            System.Windows.MessageBox.Show(Resources.MsgSaveDone);

            SavedAction?.Invoke(data);
            var view = obj as Window;
            view.Close();

        }

        private VdtNcNhuCauChiChiTiet ConvertData(VdtNcNhuCauChiChiTietModel item, Guid IdNCCQ)
        {
            VdtNcNhuCauChiChiTiet data = new VdtNcNhuCauChiChiTiet();
            data.Id = Guid.NewGuid();
            data.FGiaTriDeNghi = item.fGiaTriDeNghi;
            data.IIdDuAnId = item.iID_DuAnId;
            data.IIdLoaiCongTrinhId = item.iID_LoaiCongTrinhId;
            data.IIdNhuCauChiId = IdNCCQ;
            data.SGhiChu = item.sGhiChu;
            data.SLoaiThanhToan = item.sLoaiThanhToan;
            data.DDateCreate = DateTime.Now;
            data.SUserCreate = _sessionService.Current.Principal;
            return data;
        }

        private bool Validate()
        {
            List<string> lstError = new List<string>();

            if (KeHoachChiQuyChiTiets == null || !KeHoachChiQuyChiTiets.Any(n => !n.IsDeleted && n.fGiaTriDeNghi != 0))
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "chứng từ chi tiết"));
            }

            if (lstError != null && lstError.Count != 0)
            {
                System.Windows.MessageBox.Show(string.Join("\n", lstError));
                return false;
            }
            return true;
        }
        #endregion
    }
}
