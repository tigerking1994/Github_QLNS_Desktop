using AutoMapper;
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
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanManagerApproved
{
    public class PlanManagerApprovedImportViewModel : ViewModelBase
    {
        private static string[] lstDonViExclude = new string[] {"0"};

        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly IVdtDmLoaiCongTrinhService _vdtDmLoaiCongTrinhService;
        private readonly IVdtDaNguonVonService _vdtDaNguonVonService;
        private readonly IVdtDuAnHangMucService _vdtDuAnHangMucService;
        private readonly IVdtDaDuAnService _duAnService;
        private readonly IVdtKhvKeHoach5NamService _vdtKhvKeHoach5NamService;
        private readonly IVdtKhvKeHoach5NamDeXuatService _vdtKhvKeHoach5NamDeXuatService;
        private readonly IVdtKhvKeHoach5NamChiTietService _vdtKhvKeHoach5NamChiTietService;
        private readonly INsDonViService _nsDonViService;
        private readonly IImportExcelService _importService;
        private readonly FtpStorageService _ftpStorageService;

        private List<ImportErrorItem> _listErrChungTuChiTiet = new List<ImportErrorItem>();
        private readonly ILog _logger;
        private MediumTermPlanIndexSearch _conditionSearch;
        private string _fileName;
        private bool _isCheck;

        private ObservableCollection<VdtKhvKeHoach5NamChiTietModel> _vdtKeHoach5NamChiTiets;
        public ObservableCollection<VdtKhvKeHoach5NamChiTietModel> VdtKeHoach5NamChiTiets
        {
            get => _vdtKeHoach5NamChiTiets;
            set => SetProperty(ref _vdtKeHoach5NamChiTiets, value);
        }

        private VdtKhvKeHoach5NamChiTietModel _vdtKeHoach5NamChiTietSelected;
        public VdtKhvKeHoach5NamChiTietModel VdtKeHoach5NamChiTietSelected
        {
            get => _vdtKeHoach5NamChiTietSelected;
            set => SetProperty(ref _vdtKeHoach5NamChiTietSelected, value);
        }

        private VdtKhvKeHoach5NamModel _keHoach5Nams = new VdtKhvKeHoach5NamModel();
        public VdtKhvKeHoach5NamModel KeHoach5Nams
        {
            get => _keHoach5Nams;
            set => SetProperty(ref _keHoach5Nams, value);
        }
        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }

        private ObservableCollection<DuAnKeHoachTrungHanModel> _duAn;
        public ObservableCollection<DuAnKeHoachTrungHanModel> DuAns
        {
            get => _duAn;
            set => SetProperty(ref _duAn, value);
        }

        private DuAnKeHoachTrungHanModel _duAnSelected;
        public DuAnKeHoachTrungHanModel DuAnSelected
        {
            get => _duAnSelected;
            set => SetProperty(ref _duAnSelected, value);
        }

        private ObservableCollection<ComboboxItem> _drpNguonNganSach;
        public ObservableCollection<ComboboxItem> DrpNguonNganSach
        {
            get => _drpNguonNganSach;
            set => SetProperty(ref _drpNguonNganSach, value);
        }

        private ComboboxItem _drpNguonNganSachSelected;
        public ComboboxItem DrpNguonNganSachSelected
        {
            get => _drpNguonNganSachSelected;
            set => SetProperty(ref _drpNguonNganSachSelected, value);
        }

        private ObservableCollection<ComboboxItem> _drpDonVi;
        public ObservableCollection<ComboboxItem> DrpDonVi
        {
            get => _drpDonVi;
            set => SetProperty(ref _drpDonVi, value);
        }

        private ComboboxItem _drpDonViSelected;
        public ComboboxItem DrpDonViSelected
        {
            get => _drpDonViSelected;
            set
            {
                SetProperty(ref _drpDonViSelected, value);
                if(value != null)
                {
                    GetVoucherSuggestion();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _drpLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> DrpLoaiCongTrinh
        {
            get => _drpLoaiCongTrinh;
            set => SetProperty(ref _drpLoaiCongTrinh, value);
        }

        private ComboboxItem _drpLoaiCongTrinhSelected;
        public ComboboxItem DrpLoaiCongTrinhSelected
        {
            get => _drpLoaiCongTrinhSelected;
            set => SetProperty(ref _drpLoaiCongTrinhSelected, value);
        }

        private ObservableCollection<ComboboxItem> _drpChuDauTu;
        public ObservableCollection<ComboboxItem> DrpChuDauTu
        {
            get => _drpChuDauTu;
            set => SetProperty(ref _drpChuDauTu, value);
        }

        private ComboboxItem _drpChuDauTuSelected;
        public ComboboxItem DrpChuDauTuSelected
        {
            get => _drpChuDauTuSelected;
            set => SetProperty(ref _drpChuDauTuSelected, value);
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

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }

        private string _sSoKeHoach;
        public string SSoKeHoach
        {
            get => _sSoKeHoach;
            set => SetProperty(ref _sSoKeHoach, value);
        }

        private int _iGiaiDoanTu;
        public int IGiaiDoanTu
        {
            get => _iGiaiDoanTu;
            set
            {
                SetProperty(ref _iGiaiDoanTu, value);

                OnPropertyChanged(nameof(VdtKeHoach5NamChiTiets));
                OnPropertyChanged(nameof(IGiaiDoanDen));
            }    
        }

        private int _iGiaiDoanDen;
        public int IGiaiDoanDen
        {
            get
            {
                _iGiaiDoanDen = IGiaiDoanTu + 4;
                return _iGiaiDoanDen;
            }
            set => SetProperty(ref _iGiaiDoanDen, value);
        }

        private bool _isSaveData;
        public bool IsSaveData
        {
            get
            {
                _isSaveData = (VdtKeHoach5NamChiTiets != null && VdtKeHoach5NamChiTiets.Count > 0 && _isCheck) ? true : false;
                return _isSaveData;
            }
            set => SetProperty(ref _isSaveData, value);
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
                OnPropertyChanged(nameof(VouchersSuggestionVisibility));
                OnPropertyChanged(nameof(VoucherModifiedVisibility));
                GetVouchers();
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

        private ObservableCollection<VdtKhvKeHoach5NamDeXuatModel> _itemsKhthDeXuat;
        public ObservableCollection<VdtKhvKeHoach5NamDeXuatModel> ItemsKhthDeXuat
        {
            get => _itemsKhthDeXuat;
            set => SetProperty(ref _itemsKhthDeXuat, value);
        }

        private VdtKhvKeHoach5NamDeXuatModel _selectedKhthDeXuat;
        public VdtKhvKeHoach5NamDeXuatModel SelectedKhthDeXuat
        {
            get => _selectedKhthDeXuat;
            set
            {
                if (SetProperty(ref _selectedKhthDeXuat, value))
                {
                    if (_selectedKhthDeXuat != null)
                    {
                        IGiaiDoanTu = _selectedKhthDeXuat.IGiaiDoanTu;
                        IGiaiDoanDen = _selectedKhthDeXuat.IGiaiDoanDen;

                        OnPropertyChanged(nameof(IGiaiDoanTu));
                        OnPropertyChanged(nameof(IGiaiDoanDen));
                    }
                }
            }
        }

        public Visibility VouchersSuggestionVisibility
        {
            get => (_drpLoaiImportSelected == null || _drpLoaiImportSelected != null && _drpLoaiImportSelected.ValueItem.Equals("1")) ? Visibility.Collapsed : Visibility.Visible;
        }

        public Visibility VoucherModifiedVisibility
        {
            get => (_drpLoaiImportSelected != null && _drpLoaiImportSelected.ValueItem.Equals("2")) ? Visibility.Collapsed : Visibility.Visible;
        }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand AddDuAnCommand { get; }
        public RelayCommand UnmergeCommand { get; }
        public RelayCommand SaveDuAnCommand { get; }
        public RelayCommand ResetDataCommand { get; }
        public RelayCommand DownloadFileFtpServer { get; }
        public RelayCommand GetFileFtpCommand { get; }

        public PlanManagerApprovedImportViewModel(
            IMapper mapper,
            ISessionService sessionService,
            INsNguonNganSachService nsNguonNganSachService,
            IVdtDmLoaiCongTrinhService vdtDmLoaiCongTrinhService,
            ILog logger,
            INsDonViService nsDonViService,
            IVdtDaDuAnService vdtDaDuAnService,
            IVdtDaNguonVonService vdtDaNguonVonService,
            IVdtDuAnHangMucService vdtDuAnHangMucService,
            IVdtKhvKeHoach5NamService vdtKhvKeHoach5NamService,
            IVdtKhvKeHoach5NamChiTietService vdtKhvKeHoach5NamChiTietService,
            IImportExcelService importExcelService,
            FtpStorageService ftpStorageService,
            IVdtKhvKeHoach5NamDeXuatService vdtKhvKeHoach5NamDeXuatService)
        {
            _mapper = mapper;
            _logger = logger;
            _nsDonViService = nsDonViService;
            _duAnService = vdtDaDuAnService;
            _importService = importExcelService;
            _vdtDaNguonVonService = vdtDaNguonVonService;
            _vdtDuAnHangMucService = vdtDuAnHangMucService;
            _sessionService = sessionService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _vdtDmLoaiCongTrinhService = vdtDmLoaiCongTrinhService;
            _vdtKhvKeHoach5NamService = vdtKhvKeHoach5NamService;
            _ftpStorageService = ftpStorageService;
            _vdtKhvKeHoach5NamChiTietService = vdtKhvKeHoach5NamChiTietService;
            _vdtKhvKeHoach5NamDeXuatService = vdtKhvKeHoach5NamDeXuatService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            GetFileFtpCommand = new RelayCommand(obj => OnGetFileFtpCommand());
            DownloadFileFtpServer = new RelayCommand(obj => OnDownloadFileFtpServer());

        }

        public override void Init()
        {
            try
            {
                IGiaiDoanTu = DateTime.Now.Year;
                IGiaiDoanDen = IGiaiDoanTu + 4;

                GetNguonNganSach();
                GetLoaiCongTrinh();
                GetDonViQuanLy();
                GetLoaiImport();
                GetLoaiDuAn();
                GetVouchers();
                OnResetData();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void GetVoucherSuggestion()
        {
            try
            {
                var predicate = PredicateBuilder.True<VdtKhvKeHoach5NamDeXuat>();
                if (_drpDonViSelected != null)
                {
                    predicate = predicate.And(x => x.IIdMaDonViQuanLy.Equals(_drpDonViSelected.ValueItem));
                }
                if (_drpLoaiDuAnSelected != null)
                {
                    predicate = predicate.And(x => x.ILoai == int.Parse(_drpLoaiDuAnSelected.ValueItem));
                }

                List<VdtKhvKeHoach5NamDeXuat> lstItem = _vdtKhvKeHoach5NamDeXuatService.FindByCondition(predicate).ToList();
                ItemsKhthDeXuat = _mapper.Map<ObservableCollection<VdtKhvKeHoach5NamDeXuatModel>>(lstItem);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void OnGetFileFtpCommand()
        {
            if (DrpDonViSelected == null || (IGiaiDoanDen == 0 && IGiaiDoanTu == 0))
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
            var btmTenDonVi = StringUtils.UCS2Convert(DrpDonViSelected.ValueItem);
            string sTime = string.Format("{0}-{1}", IGiaiDoanTu, IGiaiDoanDen);
            var strUrl = string.Format("{0}/{1}/{2}", btmTenDonVi, ConstantUrlPathPhanHe.UrlKhchddWinformSend, sTime);
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
            HandleData();
        }
        private void GetVouchers()
        {
            try
            {
                var predicate = PredicateBuilder.True<VdtKhvKeHoach5Nam>();
                predicate = predicate.And(x => x.BActive);
                predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                List<VdtKhvKeHoach5Nam> lstQuery = _vdtKhvKeHoach5NamService.FindByCondition(predicate).ToList();
                DrpVouchers = _mapper.Map<ObservableCollection<ComboboxItem>>(lstQuery);
            }
            catch(Exception ex)
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

            if(DrpLoaiImports != null && DrpLoaiImports.Count() > 0)
            {
                DrpLoaiImportSelected = DrpLoaiImports.FirstOrDefault();
            }
        }

        private void GetLoaiDuAn()
        {
            List<ComboboxItem> lstLoaiDuAn = new List<ComboboxItem>() { 
                new ComboboxItem(){DisplayItem = "Mở mới", ValueItem = ((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI).ToString()},
                new ComboboxItem(){DisplayItem = "Chuyển tiếp", ValueItem = ((int)LoaiDuAnEnum.Type.CHUYEN_TIEP).ToString()}
            };

            DrpLoaiDuAns = new ObservableCollection<ComboboxItem>(lstLoaiDuAn);

            if(DrpLoaiDuAns != null && DrpLoaiDuAns.Count() > 0)
            {
                DrpLoaiDuAnSelected = DrpLoaiDuAns.FirstOrDefault();
            }
        }

        private void OnResetData()
        {
            _filePath = string.Empty;
            _vdtKeHoach5NamChiTiets = new ObservableCollection<VdtKhvKeHoach5NamChiTietModel>();
            LstFile = new ObservableCollection<FileFtpModel>();
            _tabIndex = ImportTabIndex.Data;
            _drpLoaiCongTrinhSelected = null;
            _drpLoaiImportSelected = null;
            _drpDonViSelected = null;
            _drpLoaiDuAnSelected = null;
            _sSoKeHoach = string.Empty;

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(TabIndex));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(VdtKeHoach5NamChiTiets));
            OnPropertyChanged(nameof(LstFile));
        }

        private void ShowError()
        {
            try
            {
                var errors = new HashSet<string>();
                int rowIndex = VdtKeHoach5NamChiTiets.IndexOf(VdtKeHoach5NamChiTietSelected);
                errors = _listErrChungTuChiTiet.Where(x => x.Row == rowIndex).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
                System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ResetValidationSearch()
        {
            _conditionSearch = new MediumTermPlanIndexSearch()
            {
                idMaDonViQuanLy = _drpDonViSelected.ValueItem,
                iGiaiDoanTu = IGiaiDoanTu,
                iGiaiDoanDen = IGiaiDoanDen,
                iNamLamViec = _sessionService.Current.YearOfWork,
                iLoai = Int32.Parse(_drpLoaiDuAnSelected.ValueItem)
            };
        }

        public override void OnSave()
        {
            try
            {
                List<string> lstError = new List<string>();
                if (ValidateChungTu(ref lstError))
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

                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && !lstDv.Contains(_drpDonViSelected.ValueItem))
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.UserManagerImportKHTHWarning, _sessionService.Current.Principal, _drpDonViSelected.DisplayItem), Resources.Alert);
                    return;
                }

                if (DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("1"))
                {
                    KeHoach5Nams.Id = Guid.NewGuid();
                    VdtKhvKeHoach5Nam entity = _mapper.Map<VdtKhvKeHoach5Nam>(KeHoach5Nams);
                    if(_selectedKhthDeXuat != null)
                    {
                        entity.IIDKhthDeXuat = _selectedKhthDeXuat.Id;
                    }
                    entity.DNgayQuyetDinh = DateTime.Now;
                    entity.DDateCreate = DateTime.Now;
                    entity.SUserCreate = _sessionService.Current.Principal;
                    entity.IIdMaDonViQuanLy = _drpDonViSelected.ValueItem;
                    entity.IIdDonViQuanLyId = Guid.Parse(_drpDonViSelected.HiddenValue);
                    entity.NamLamViec = _sessionService.Current.YearOfWork;
                    entity.IGiaiDoanDen = IGiaiDoanDen;
                    entity.IGiaiDoanTu = IGiaiDoanTu;
                    entity.SSoQuyetDinh = SSoKeHoach;
                    entity.ILoai = Int32.Parse(_drpLoaiDuAnSelected.ValueItem);
                    entity.BActive = true;
                    entity.BIsGoc = true;

                    var lstData = VdtKeHoach5NamChiTiets.Select(x =>
                    {
                        x.Id = Guid.NewGuid();
                        x.IIdKeHoach5NamId = KeHoach5Nams.Id;
                        return x;
                    }).ToList();

                    var lstDataInsert = _mapper.Map<List<VdtKhvKeHoach5NamChiTiet>>(lstData);
                    entity.FGiaTriDuocDuyet = lstDataInsert.Sum(x => x.FVonBoTriTuNamDenNam ?? 0);
                    _vdtKhvKeHoach5NamService.Add(entity);

                    int isSuccess = _vdtKhvKeHoach5NamChiTietService.AddRange(lstDataInsert);
                    if (isSuccess == 0)
                    {
                        _vdtKhvKeHoach5NamService.Delete(KeHoach5Nams.Id);
                        System.Windows.MessageBox.Show(Resources.ImportError);
                        return;
                    }
                    System.Windows.MessageBox.Show(Resources.FileImportStatus);
                }
                else if(DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("2"))
                {
                    if((VdtKeHoach5NamChiTiets != null && VdtKeHoach5NamChiTiets.Count() > 0 && VdtKeHoach5NamChiTiets.FirstOrDefault().IIdKeHoach5NamId != Guid.Parse(_drpVoucherSelected.ValueItem)))
                    {
                        System.Windows.MessageBox.Show(Resources.VoucherImportErrors, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    VdtKhvKeHoach5Nam itemParent = _vdtKhvKeHoach5NamService.FindById(Guid.Parse(_drpVoucherSelected.ValueItem));

                    VdtKhvKeHoach5Nam itemNew = new VdtKhvKeHoach5Nam();
                    itemParent.CloneObj(itemNew);
                    itemNew.DDateCreate = DateTime.Now;
                    itemNew.SUserCreate = _sessionService.Current.Principal;
                    itemNew.BIsGoc = false;
                    itemNew.BActive = true;
                    itemNew.Id = Guid.NewGuid();
                    itemNew.IIdParentId = itemParent.Id;
                    itemNew.SSoQuyetDinh = SSoKeHoach;
                    itemNew.BKhoa = false;

                    int success = _vdtKhvKeHoach5NamService.Adjust(itemNew);
                    if(success == DBContextSaveChangeState.ERROR)
                    {
                        System.Windows.MessageBox.Show(Resources.ImportError);
                        return;
                    }
                    System.Windows.MessageBox.Show(Resources.FileImportStatus);
                }

                var entityModel = _mapper.Map<VdtKhvKeHoach5NamModel>(KeHoach5Nams);

                SavedAction?.Invoke(entityModel);
            }
            catch(Exception ex)
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
                _fileName = openFileDialog.SafeFileName;

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    if (DrpDonViSelected != null && !string.IsNullOrEmpty(SSoKeHoach) && !IGiaiDoanTu.Equals(0))
                    {
                        _isCheck = true;
                    }
                    else
                    {
                        _isCheck = false;
                    }

                    HandleData();

                    OnPropertyChanged(nameof(FilePath));
                    OnPropertyChanged(nameof(IsSelectedFile));
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(VdtKeHoach5NamChiTiets));
                    OnPropertyChanged(nameof(DuAns));
                }, (s, e) =>
                {
                    IsLoading = false;
                });
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.MsgErrorImport, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
                return;
            }
        }

        private bool ValidateChungTu(ref List<string> lstError)
        {
            bool isError = false;

            if(DrpLoaiImportSelected == null)
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Loại imports"));
                isError = true;
            }

            if (string.IsNullOrEmpty(SSoKeHoach))
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Số kế hoạch"));
                isError = true;
            }

            if (DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("1"))
            {
                if (DrpDonViSelected == null)
                {
                    lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Đơn vị quản lý"));
                    isError = true;
                }
                if (IGiaiDoanTu == 0)
                {
                    lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Giai đoạn kế hoạch"));
                    isError = true;
                }
                if (DrpLoaiDuAnSelected == null)
                {
                    lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Loại dự án"));
                    isError = true;
                }
                if (DrpDonViSelected != null && !string.IsNullOrEmpty(SSoKeHoach) && !IGiaiDoanTu.Equals(0) && DrpLoaiImportSelected.ValueItem.Equals("1"))
                {
                    ResetValidationSearch();

                    if(!CheckGiaiDoan())
                    {
                        lstError.Add(Resources.VoucherPeriodInValid);
                        isError = true;
                    }
                }
            }
            else if (DrpLoaiImportSelected != null && DrpLoaiImportSelected.ValueItem.Equals("2"))
            {
                if (DrpVoucherSelected == null)
                {
                    lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "Chứng từ"));
                    isError = true;
                }
            }

            return isError;
        }

        private bool CheckGiaiDoan()
        {
            var predicate = PredicateBuilder.True<VdtKhvKeHoach5Nam>();
            if (DrpDonViSelected != null)
            {
                predicate = predicate.And(x => x.IIdMaDonViQuanLy.Equals(DrpDonViSelected.ValueItem));
            }
            if (DuAnSelected != null)
            {
                predicate = predicate.And(x => x.ILoai == int.Parse(DuAnSelected.ValueItem));
            }
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            var rs = _vdtKhvKeHoach5NamService.FindByCondition(predicate).ToList();

            foreach (var item in rs)
            {
                if (IGiaiDoanTu >= item.IGiaiDoanTu && IGiaiDoanTu <= item.IGiaiDoanDen)
                {
                    return false;
                }
            }

            return true;
        }

        private void OnProcessFile(bool bIsImport = false)
        {
            try
            {
                List<string> lstError = new List<string>();
                if (!bIsImport)
                {
                    ValidateChungTu(ref lstError);
                }
                if (lstError.Any())
                {
                    string sMessError = string.Join(Environment.NewLine, lstError);
                    System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _isCheck = true;

                OnPropertyChanged(nameof(VdtKeHoach5NamChiTiets));
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return;
            }
        }

        private void HandleData()
        {
            try
            {
                //xử lý chứng từ chi tiết
                var sheetDetailAttribute = (SheetAttribute)Attribute.GetCustomAttribute(typeof(PlanManageApprovedImportModel), typeof(SheetAttribute));
               
                var importDivisionDetailResult = _importService.ProcessData<PlanManageApprovedImportModel>(FilePath);

                List<PlanManageApprovedImportModel> details = importDivisionDetailResult.Data;

                foreach (var item in details)
                {
                    if (string.IsNullOrEmpty(item.FHanMucDuToan))
                    {
                        item.FHanMucDuToan = "0";
                    }
                    if (string.IsNullOrEmpty(item.FVonDaGiao))
                    {
                        item.FVonDaGiao = "0";
                    }
                    if (string.IsNullOrEmpty(item.FVonBoTriGiaiDoan))
                    {
                        item.FVonBoTriGiaiDoan = "0";
                    }
                    if (string.IsNullOrEmpty(item.FVonBoTriSauNam))
                    {
                        item.FVonBoTriSauNam = "0";
                    }
                    if (string.IsNullOrEmpty(item.IIdDuAnId))
                    {
                        item.IIdDuAnId = Guid.Empty.ToString();
                    }
                    if (string.IsNullOrEmpty(item.IIdLoaiCongTrinhId))
                    {
                        item.IIdLoaiCongTrinhId = Guid.Empty.ToString();
                    }
                    if (string.IsNullOrEmpty(item.IMaNguonVon))
                    {
                        item.IMaNguonVon = "0";
                    }
                    if (string.IsNullOrEmpty(item.IIdParentId))
                    {
                        item.IIdParentId = Guid.Empty.ToString();
                    }
                    if (string.IsNullOrEmpty(item.IIdDonViId))
                    {
                        item.IIdDonViId = Guid.Empty.ToString();
                    }

                    var rowIndex = details.IndexOf(item);
                    var listError = ValidateItem(item, rowIndex);

                    if (listError.Count > 0)
                    {
                        _listErrChungTuChiTiet.AddRange(listError);
                        item.ImportStatus = false;

                        if (listError.Any(x => x.IsErrorMLNS))
                        {
                            item.IsError = true;
                        }
                    }
                }

                VdtKeHoach5NamChiTiets = _mapper.Map<ObservableCollection<VdtKhvKeHoach5NamChiTietModel>>(details);

                OnPropertyChanged(nameof(DuAns));
                OnPropertyChanged(nameof(VdtKeHoach5NamChiTiets));
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private List<ImportErrorItem> ValidateItem<T>(T item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();

                foreach (var prop in typeof(T).GetProperties())
                {
                    if(prop.GetCustomAttributes(true).Length > 0)
                    {
                        ColumnAttribute attribute = prop.GetCustomAttributes(true).First() as ColumnAttribute;
                        string val = prop.GetValue(item).ToString();

                        if (attribute.DataType == ValidateType.IsMaDuAn)
                        {
                            VdtDaDuAn lstDuAn = _duAnService.FindByMaDuAn(val);

                            if(lstDuAn == null)
                            {
                                errors.Add(new ImportErrorItem { Row = rowIndex, Error = "Dự án chưa tồn tại", ColumnName = "Danh mục dự án", IsErrorMLNS = true });
                            }    
                        }    
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

        private void GetNguonNganSach()
        {
            var lstData = _nsNguonNganSachService.FindNguonNganSach();
            _drpNguonNganSach = _mapper.Map<ObservableCollection<ComboboxItem>>(lstData);
        }

        private void GetLoaiCongTrinh()
        {
            List<VdtDmLoaiCongTrinh> listLoaiCongTrinh = _vdtDmLoaiCongTrinhService.FindAll().ToList();
            _drpLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(listLoaiCongTrinh);
        }

        private void GetDonViQuanLy()
        {
            var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
               .Where(n => lstDonViExclude.Contains(n.Loai)).ToList();

            _drpDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(cbxLoaiDonViData);
        }

        public override void OnClose(object obj)
        {
            try
            {
                var window = obj as Window;
                window.Close();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
