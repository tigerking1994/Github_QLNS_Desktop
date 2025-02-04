using AutoMapper;
using FlexCel.Core;
using FlexCel.XlsAdapter;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.YearPlanImport
{
    public class YearPlanImportViewModel : ViewModelBase
    {
        private static string[] lstDonViExclude = new string[] { "0", "1" };
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly IVdtKhvPhanBoVonService _phanBoVonService;
        private readonly IVdtKhvPhanBoVonChiTietService _phanBoVonChiTietService;
        private readonly IVdtKhvPhanBoVonDonViService _vdtKhvPhanBoVonDonViService;
        private readonly FtpStorageService _ftpStorageService;

        private IConfiguration _configuration;
        private string _importFolder;
        private string _fileName;
        private static int _iLoai;
        private List<ImportErrorItem> _lstErrChungTuChiTiet;
        public override string Name { get; set; }
        public override string Description { get; set; }
        private Dictionary<string, NsMucLucNganSach> _dicMucLucNganSach;
        private Dictionary<string, Guid> _dicDuAn;

        #region Items
        public override Type ContentType => typeof(VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.YearPlanImport.YearPlanImport);
        public int? YearPlanTypes { get; set; }
        private VdtKhvPhanBoVon _phanBoVon = new VdtKhvPhanBoVon();
        public VdtKhvPhanBoVon PhanBoVon
        {
            get => _phanBoVon;
            set => SetProperty(ref _phanBoVon, value);
        }

        private ObservableCollection<PhanBoVonImportModel> _phanBoVonChiTiets;
        public ObservableCollection<PhanBoVonImportModel> PhanBoVonChiTiets
        {
            get => _phanBoVonChiTiets;
            set => SetProperty(ref _phanBoVonChiTiets, value);
        }

        private PhanBoVonImportModel _phanBoVonChiTietSelected;
        public PhanBoVonImportModel PhanBoVonChiTietSelected
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

        public bool _isSaveData;
        public bool IsSaveData
        {
            get => PhanBoVon != null && PhanBoVonChiTiets != null && PhanBoVonChiTiets.Count > 0 && !PhanBoVonChiTiets.Any(x => !x.ImportStatus);
            set => SetProperty(ref _isSaveData, value);
        }

        private string _iNamKeHoach;
        public string INamKeHoach
        {
            get => _iNamKeHoach;
            set
            {
                if (SetProperty(ref _iNamKeHoach, value))
                {
                    GetDonViQuanLy();
                }
            }
        }

        private string _sSoKeHoach;
        public string SSoKeHoach
        {
            get => _sSoKeHoach;
            set => SetProperty(ref _sSoKeHoach, value);
        }

        private DateTime? _dNgayLap;
        public DateTime? DNgayLap
        {
            get => _dNgayLap;
            set => SetProperty(ref _dNgayLap, value);
        }

        private ObservableCollection<ComboboxItem> _drpDonViQuanLy;
        public ObservableCollection<ComboboxItem> DrpDonViQuanLy
        {
            get => _drpDonViQuanLy;
            set => SetProperty(ref _drpDonViQuanLy, value);
        }

        private ComboboxItem _drpDonViQuanLySelected;
        public ComboboxItem DrpDonViQuanLySelected
        {
            get => _drpDonViQuanLySelected;
            set => SetProperty(ref _drpDonViQuanLySelected, value);
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
        public RelayCommand GetFileFtpCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand DownloadFileFtpServer { get; }

        #endregion

        public YearPlanImportViewModel(ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonVonService,
            IVdtKhvPhanBoVonService phanBoVonService,
            INsMucLucNganSachService nsMucLucNganSachService,
            IVdtKhvPhanBoVonChiTietService phanBoVonChiTietService,
            FtpStorageService ftpStorageService,
            IVdtKhvPhanBoVonDonViService vdtKhvPhanBoVonDonViService,
            IConfiguration configuration)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _nsDonViService = nsDonViService;
            _nsNguonVonService = nsNguonVonService;
            _configuration = configuration;
            _phanBoVonService = phanBoVonService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _phanBoVonChiTietService = phanBoVonChiTietService;
            _ftpStorageService = ftpStorageService;
            _vdtKhvPhanBoVonDonViService = vdtKhvPhanBoVonDonViService;

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
            _iLoai = YearPlanTypes.Value;
            switch (_iLoai)
            {
                case (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet:
                    Name = "IMPORT DỮ LIỆU KẾ HOẠCH VỐN NĂM ĐƯỢC DUYỆT";
                    Description = "Chọn file Excel, thực hiện kiểm tra và import dữ liệu kế hoạch vốn năm được duyệt";
                    break;
                case (int)LoaiKeHoachNam.KeHoachVonNam:
                    Name = "IMPORT DỮ LIỆU KẾ HOẠCH VỐN NĂM";
                    Description = "Chọn file Excel, thực hiện kiểm tra và import dữ liệu kế hoạch vốn năm";
                    break;
                case (int)LoaiKeHoachNam.TongHopKeHoachVonNam:
                    Name = "IMPORT DỮ LIỆU TỔNG HỢP KẾ HOẠCH VỐN NĂM";
                    Description = "Chọn file Excel, thực hiện kiểm tra và import dữ liệu tổng hợp kế hoạch vốn năm";
                    break;
            }
            _importFolder = _configuration.GetSection("ImportFolder").Value;
            Directory.CreateDirectory(_importFolder);
            GetNguonVon();
            GetVouchers();
            GetLoaiImport();
            OnResetData();
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
            var predicate = PredicateBuilder.True<VdtKhvPhanBoVon>();
            predicate = predicate.And(x => x.BActive.Value);
            List<VdtKhvPhanBoVon> lstQuery = _phanBoVonService.FindByCondition(predicate).ToList();
            DrpVouchers = _mapper.Map<ObservableCollection<ComboboxItem>>(lstQuery);
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
        private string OnProcessFile(bool bIsImport = false)
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

            GetMucLucNganSach();
            var data = GetDataImportByFileType();
            if (!bIsImport)
            {
                GetDuAn();
            }
            PhanBoVonChiTiets = new ObservableCollection<PhanBoVonImportModel>(data);
            ValidateChungTu(ref lstError);
            if (lstError.Any())
            {
                string sMessError = string.Join(Environment.NewLine, lstError);
                System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                PhanBoVon = null;
            }
            if (!bIsImport)
            {
                ValidateChungTuChiTiet();
            }
            OnPropertyChanged(nameof(PhanBoVon));
            OnPropertyChanged(nameof(PhanBoVonChiTiets));
            OnPropertyChanged(nameof(IsSaveData));
            return string.Empty;
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
            foreach (var item in LstFile)
            {
                if (item.BIsCheck)
                {
                    urlUrIDownLoad = item.SUrl;
                    fileName = item.SNameFile;
                    string filePath= _ftpStorageService.DowLoadFileFtpGiveLocal(urlUrIDownLoad, fileName);
                    FilePath = filePath;
                }
            }
            OnProcessFile(true);
        }
        /// <summary>
        /// Lấy dữ liệu từ ftp
        /// </summary>
        private void OnGetFileFtpCommand()
        {
            if (DrpDonViQuanLySelected == null || string.IsNullOrEmpty(INamKeHoach))
            {

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng chọn đúng từng giai đoạn");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                return;
            }
            else if (LstFile.Where(n => n.BIsCheck).Count() > 1)
            {
                System.Windows.MessageBox.Show("Chọn 1 file dữ liệu");
                return;
            }
            var btmTenDonVi = StringUtils.UCS2Convert(DrpDonViQuanLySelected.ValueItem);
            string sTime = string.Format("{0}", INamKeHoach);
            var strUrl = string.Format("{0}/{1}/{2}", btmTenDonVi, ConstantUrlPathPhanHe.UrlKhnddWinformSend, sTime);
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
        /// Save data
        /// </summary>
        public override void OnSave(object obj)
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

            if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && !lstDv.Contains(_drpDonViQuanLySelected.ValueItem))
            {
                System.Windows.MessageBox.Show(string.Format(Resources.UserManagerImportKHTHWarning, _sessionService.Current.Principal, _drpDonViQuanLySelected.DisplayItem), Resources.Alert);
                return;
            }

            if (DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("1"))
            {
                PhanBoVon.Id = Guid.NewGuid();
                PhanBoVon.SSoQuyetDinh = SSoKeHoach;
                PhanBoVon.DNgayQuyetDinh = DateTime.Now;
                PhanBoVon.INamKeHoach = _sessionService.Current.YearOfWork;
                PhanBoVon.IIdDonViQuanLyId = Guid.Parse(_drpDonViQuanLySelected.HiddenValue);
                PhanBoVon.IIDMaDonViQuanLy = _drpDonViQuanLySelected.ValueItem;
                PhanBoVon.BActive = true;
                PhanBoVon.BIsGoc = true;
                PhanBoVon.SUserCreate = _sessionService.Current.Principal;
                PhanBoVon.DDateCreate = DateTime.Now;
                PhanBoVon.IIdNguonVonId = Int32.Parse(_drpNguonVonSelected.ValueItem);
                PhanBoVon.IIdPhanBoGocId = PhanBoVon.Id;
                PhanBoVon.ILoai = (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet;

                string sError = string.Empty;
                _phanBoVonService.Insert(PhanBoVon, _sessionService.Current.Principal, ref sError);
                var lstData = PhanBoVonChiTiets.Select(
                n =>
                {
                    n.iID_DuAnID = _dicDuAn[n.sMaDuAn];
                    n.iID_LoaiNguonVonID = _dicMucLucNganSach.ContainsKey(n.sLns + "------") ? _dicMucLucNganSach[n.sLns + "------"].Id : Guid.Empty;
                    return n;
                }).ToList();
                var lstDataInsert = _mapper.Map<List<PhanBoVonChiTietInsertQuery>>(lstData);
                lstDataInsert = lstDataInsert.Select(n => { n.iID_PhanBoVonID = PhanBoVon.Id; return n; }).ToList();
                //bool isSucess = _phanBoVonChiTietService.CreatePhanBoVonChiTiet(PhanBoVon.Id, (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet, lstDataInsert, _sessionService.Current.Principal, true);
                bool isSucess = _phanBoVonChiTietService.CreatePhanBoVonChiTietClone(PhanBoVon.Id, (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet, lstDataInsert, _sessionService.Current.Principal, true);
                if (!isSucess)
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.MsgErrorFormat, "Mục lục ngân sách"));
                    _phanBoVonService.DeletePhanBoVon(PhanBoVon, _sessionService.Current.Principal, ref sError);
                    return;
                }
            }
            else if ((DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("2")))
            {
                if ((PhanBoVonChiTiets != null && PhanBoVonChiTiets.Count() > 0 && PhanBoVonChiTiets.FirstOrDefault().IdChungTu != Guid.Parse(_drpVoucherSelected.ValueItem)))
                {
                    System.Windows.MessageBox.Show(Resources.VoucherImportErrors, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                VdtKhvPhanBoVon itemParent = _phanBoVonService.FindById(Guid.Parse(_drpVoucherSelected.ValueItem));
                List<VdtKhvPhanBoVonChiTiet> itemChiTietParent = _phanBoVonService.GetPhanBoVonByIdPhanBoVon(Guid.Parse(_drpVoucherSelected.ValueItem)).ToList();
                List<VdtKhvPhanBoVonChiTiet> itemChiTietNew = _mapper.Map<List<VdtKhvPhanBoVonChiTiet>>(PhanBoVonChiTiets);

                VdtKhvPhanBoVon itemNew = new VdtKhvPhanBoVon();
                itemParent.CloneObj(itemNew);
                itemNew.DDateCreate = DateTime.Now;
                itemNew.SUserCreate = _sessionService.Current.Principal;
                itemNew.BActive = true;
                itemNew.BIsGoc = false;
                itemNew.Id = Guid.NewGuid();
                itemNew.IIdParentId = itemParent.Id;
                itemNew.SSoQuyetDinh = SSoKeHoach;

                var result = (from rs in itemChiTietNew
                              join pr in itemChiTietParent on rs.IIdPhanBoVonId equals pr.IIdPhanBoVonId
                              select new VdtKhvPhanBoVonChiTiet()
                              {
                                  Id = pr.Id,
                                  IIdPhanBoVonId = itemNew.Id,
                                  IIdDuAnId = pr.IIdDuAnId,
                                  IIdMucId = pr.IIdMucId,
                                  IIdTieuMucId = pr.IIdTieuMucId,
                                  IIdTietMucId = pr.IIdTietMucId,
                                  IIdNganhId = pr.IIdNganhId,
                                  STrangThaiDuAnDangKy = pr.STrangThaiDuAnDangKy,
                                  FGiaTrDeNghi = rs.FGiaTrDeNghi,
                                  FGiaTrPhanBo = rs.FGiaTrPhanBo,
                                  FGiaTriThuHoi = rs.FGiaTriThuHoi,
                                  SGhiChu = rs.SGhiChu,
                                  Lns = pr.Lns,
                                  L = pr.L,
                                  K = pr.K,
                                  M = pr.M,
                                  Tm = pr.Tm,
                                  Ttm = pr.Ttm,
                                  Ng = pr.Ng,
                                  FCapPhatTaiKhoBac = rs.FCapPhatTaiKhoBac,
                                  FCapPhatBangLenhChi = rs.FCapPhatBangLenhChi,
                                  FGiaTriThuHoiNamTruocKhoBac = rs.FGiaTriThuHoiNamTruocKhoBac,
                                  FGiaTriThuHoiNamTruocLenhChi = rs.FGiaTriThuHoiNamTruocLenhChi,
                                  IIdLoaiCongTrinh = rs.IIdLoaiCongTrinh,
                                  IIdParent = pr.IIdParent,
                                  BActive = true,
                                  ILoaiDuAn = pr.ILoaiDuAn
                              }).ToList();

                result = result.GroupBy(x => x.Id).Select(grp => grp.FirstOrDefault()).ToList();

                _phanBoVonService.CreateVoucherImports(itemNew, result);
            }
            System.Windows.MessageBox.Show(Resources.FileImportStatus);

            var window = obj as Window;
            window.Close();

            var entityModel = _mapper.Map<PhanBoVonModel>(PhanBoVon);
            SavedAction?.Invoke(entityModel);
        }

        private void ShowError(object param)
        {
            var importTabIndex = (ImportTabIndex)((int)param);
            int rowIndex;
            rowIndex = PhanBoVonChiTiets.IndexOf(PhanBoVonChiTietSelected);
            var errors = _lstErrChungTuChiTiet.Where(x => x.Row == rowIndex).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
            System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnResetData()
        {
            _filePath = string.Empty;
            _iNamKeHoach = null;
            _sSoKeHoach = null;
            _dNgayLap = null;
            _drpDonViQuanLySelected = null;
            _drpNguonVonSelected = null;
            _isSelectedFile = false;
            PhanBoVon = null;
            PhanBoVonChiTiets = new ObservableCollection<PhanBoVonImportModel>();
            LstFile = new ObservableCollection<FileFtpModel>();


            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(SSoKeHoach));
            OnPropertyChanged(nameof(DNgayLap));
            OnPropertyChanged(nameof(DrpDonViQuanLySelected));
            OnPropertyChanged(nameof(DrpNguonVonSelected));
            OnPropertyChanged(nameof(LstFile));

        }

        public override void OnClose(object obj)
        {
            var window = obj as Window;
            window.Close();
        }
        #endregion

        #region Helper
        private void GetNguonVon()
        {
            var cbxNguonVonData = _nsNguonVonService.FindNguonNganSach()
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
            _drpNguonVon = new ObservableCollection<ComboboxItem>(cbxNguonVonData);
        }

        private void GetDonViQuanLy()
        {
            int iNamKeHoach;
            if (int.TryParse(INamKeHoach, out iNamKeHoach))
            {
                var lstLoaiDonViData = _nsDonViService.FindByNamLamViec(iNamKeHoach);
                lstLoaiDonViData = lstLoaiDonViData.Where(x => x.Loai.Equals("0"));
                _drpDonViQuanLy = _mapper.Map<ObservableCollection<ComboboxItem>>(lstLoaiDonViData);
                OnPropertyChanged(nameof(DrpDonViQuanLy));
            }
            else
            {
                System.Windows.MessageBox.Show(string.Format(Resources.MsgErrorFormat, "Năm kế hoạch"), Resources.MsgErrorFormat, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool ValidateForm(ref List<string> lstError)
        {
            bool bError = false;
            if (DrpLoaiImportSelected == null)
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Loại imports"));
                bError = true;
            }
            if (string.IsNullOrEmpty(SSoKeHoach))
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Số kế hoạch"));
                bError = true;
            }

            if (DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("1"))
            {
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
                if (DrpNguonVonSelected == null)
                {
                    lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Nguồn vốn"));
                    bError = true;
                }
                if (DrpDonViQuanLySelected == null)
                {
                    lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Đơn vị quản lý"));
                    bError = true;
                }
                if (!DNgayLap.HasValue)
                {
                    lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Ngày lập"));
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

            return !bError;
        }

        private void ValidateChungTu(ref List<string> lstError)
        {
            if (_phanBoVonChiTiets.Where(n => !string.IsNullOrEmpty(n.sLns) && _dicMucLucNganSach.ContainsKey(n.sLns + "------")).Any())
            {
                PhanBoVon.IIdLoaiNguonVonId =
                    _dicMucLucNganSach[_phanBoVonChiTiets.Where(n => !string.IsNullOrEmpty(n.sLns) && _dicMucLucNganSach.ContainsKey(n.sLns + "------")).FirstOrDefault().sLns + "------"].Id;
            }
            if (_phanBoVonService.ExistPhanBoVonBySoQuyetDinhAndDonVi(PhanBoVon ?? new VdtKhvPhanBoVon(), _iLoai))
            {
                lstError.Add(string.Format(Resources.MsgErrorDuplicateSoQuyetDinh, DrpDonViQuanLySelected.DisplayItem, PhanBoVon.SSoQuyetDinh));
            }
        }

        private void ValidateChungTuChiTiet()
        {
            Dictionary<string, int> dicDuAnByMlns = new Dictionary<string, int>();
            for (int i = 0; i < _phanBoVonChiTiets.Count; ++i)
            {
                var item = _phanBoVonChiTiets[i];
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

                if (string.IsNullOrEmpty(item.sM) && string.IsNullOrEmpty(item.sTm) && string.IsNullOrEmpty(item.sTtm) && string.IsNullOrEmpty(item.sNg))
                {
                    _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                    {
                        ColumnName = "Mục lục ngân sách",
                        Error = Resources.MsgErrorDataEmpty,
                        IsErrorMLNS = false,
                        Row = i
                    });
                    item.ImportStatus &= false;
                }

                if (!_dicMucLucNganSach.ContainsKey(item.sXauNoiMa))
                {
                    _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                    {
                        ColumnName = "Mục lục ngân sách",
                        Error = Resources.MsgErrorMucLucNganSachNotExist,
                        IsErrorMLNS = true,
                        Row = i
                    });
                    item.IsErrorMLNS = true;
                    item.ImportStatus &= false;
                }

                if (item.ImportStatus)
                {
                    if (dicDuAnByMlns.ContainsKey(item.sMaDuAn + "\t" + item.sXauNoiMa))
                    {
                        _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                        {
                            ColumnName = string.Empty,
                            Error = Resources.MsgErrorDataExist,
                            IsErrorMLNS = false,
                            Row = i
                        });
                        item.ImportStatus &= false;
                    }
                    else
                    {
                        dicDuAnByMlns.Add(item.sMaDuAn + "\t" + item.sXauNoiMa, i);
                    }
                }
            }
            OnPropertyChanged(nameof(PhanBoVonChiTiets));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void GetMucLucNganSach()
        {
            int iNamLamViec;
            _dicMucLucNganSach = new Dictionary<string, NsMucLucNganSach>();
            if (!int.TryParse(INamKeHoach, out iNamLamViec))
            {
                return;
            }
            var data = _nsMucLucNganSachService.FindAll(iNamLamViec);
            if (data == null || !data.Any())
            {
                return;
            }
            data = data.Where(n => string.IsNullOrEmpty(n.Tng) && string.IsNullOrEmpty(n.Tng1) && string.IsNullOrEmpty(n.Tng2) && string.IsNullOrEmpty(n.Tng3)).ToList();
            foreach (var item in data)
            {
                if (_dicMucLucNganSach.ContainsKey(item.Lns + "-" + item.L + "-" + item.K + "-" + item.M + "-" + item.Tm + "-" + item.Ttm + "-" + item.Ng)) continue;
                _dicMucLucNganSach.Add(item.Lns + "-" + item.L + "-" + item.K + "-" + item.M + "-" + item.Tm + "-" + item.Ttm + "-" + item.Ng, item);
            }
        }

        private void GetDuAn(bool isCheckbl = false)
        {
            List<PhanBoVonChiTietQuery> lstDuAn = new List<PhanBoVonChiTietQuery>();
            string idMaDonViQuanLy = string.Empty;
            int idNguonVon = 0;

            if (_drpLoaiImportSelected.ValueItem.Equals("1"))
            {
                idMaDonViQuanLy = DrpDonViQuanLySelected.ValueItem;
                idNguonVon = Int32.Parse(DrpNguonVonSelected.ValueItem);
            }
            else if (_drpLoaiImportSelected.ValueItem.Equals("2"))
            {
                VdtKhvPhanBoVon itemParent = _phanBoVonService.FindById(Guid.Parse(_drpVoucherSelected.ValueItem));
                if (itemParent != null)
                {
                    idMaDonViQuanLy = itemParent.IIDMaDonViQuanLy;
                    idNguonVon = itemParent.IIdNguonVonId.Value;
                }
            }

            int iNamKeHoach;
            int.TryParse(INamKeHoach, out iNamKeHoach);

            var predicate = PredicateBuilder.True<VdtKhvPhanBoVonDonVi>();
            predicate = predicate.And(x => x.INamKeHoach == iNamKeHoach);
            predicate = predicate.And(x => x.IIdMaDonViQuanLy == _sessionService.Current.IdDonVi);
            predicate = predicate.And(x => x.IIdNguonVonId == idNguonVon);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.STongHop));

            var itemQuery = _vdtKhvPhanBoVonDonViService.FindByCondition(predicate);
            string lstId = Guid.Empty.ToString();
            if (itemQuery != null)
            {
                lstId = string.Join(",", itemQuery.Select(x => x.Id).ToList());
            }

            lstDuAn = _phanBoVonChiTietService.GetAllDuAnInPhanBoVon(lstId, idNguonVon).ToList();

            if (lstDuAn != null && lstDuAn.Any())
            {
                _dicDuAn = lstDuAn.GroupBy(n => n.sMaDuAn).ToDictionary(n => n.Key, n => n.First().iID_DuAnID);
            }
            else
            {
                _dicDuAn = new Dictionary<string, Guid>();
            }
        }

        private List<PhanBoVonImportModel> GetDataImportByFileType()
        {
            XlsFile xls = new XlsFile(false);
            xls.Open(FilePath);
            xls.ActiveSheet = 1;

            var dataImportMoMoi = _importService.ProcessData<PhanBoVon_MoMoiImportModel>(FilePath);
            if (dataImportMoMoi.ImportErrors.Any())
            {
                _lstErrChungTuChiTiet.AddRange(dataImportMoMoi.ImportErrors);
            }

            if (dataImportMoMoi.Data != null && dataImportMoMoi.Data.Count() > 0)
            {
                PhanBoVon = new VdtKhvPhanBoVon();
                var itemChungTu = dataImportMoMoi.Data.FirstOrDefault();
                if (itemChungTu != null)
                {
                    PhanBoVon.Id = Guid.Parse(itemChungTu.IdChungTu);
                    if (!string.IsNullOrEmpty(itemChungTu.IdChungTuParent))
                    {
                        PhanBoVon.IIdParentId = Guid.Parse(itemChungTu.IdChungTuParent);
                    }
                    PhanBoVon.BActive = bool.Parse(itemChungTu.IsActive);
                    PhanBoVon.BIsGoc = bool.Parse(itemChungTu.IsGoc);
                }
            }

            return _mapper.Map<List<PhanBoVonImportModel>>(dataImportMoMoi.Data);
        }
        #endregion
    }
}
