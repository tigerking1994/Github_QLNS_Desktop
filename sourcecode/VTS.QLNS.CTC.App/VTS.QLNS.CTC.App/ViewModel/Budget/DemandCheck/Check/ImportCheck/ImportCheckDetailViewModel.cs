using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check.ImportCheck
{
    public class ImportCheckDetailViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktMucLucService _sktMucLucService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly FtpStorageService _ftpStorageService;


        public override string Name => "Ngân sách thường xuyên";
        public override Type ContentType => typeof(View.Budget.DemandCheck.Check.ImportCheck.ImportCheckDetail);
        public override string Description => "Quyết toán Lương, Phụ cấp, Trợ cấp, Tiền ăn";
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private List<NsSktMucLuc> _nsSktMucLucs;
        private List<CheckVoucherDetailImportModel> _checkVoucherDetailProcess;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;

        private ObservableCollection<CheckVoucherDetailImportModel> _checkVoucherDetails;
        public ObservableCollection<CheckVoucherDetailImportModel> CheckVoucherDetails
        {
            get => _checkVoucherDetails;
            set => SetProperty(ref _checkVoucherDetails, value);
        }

        private CheckVoucherDetailImportModel _selectedItem;
        public CheckVoucherDetailImportModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }
        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }
        public bool IsSaveData
        {
            get
            {
                if (CheckVoucherDetails.Count > 0)
                    return !CheckVoucherDetails.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }

        private ObservableCollection<ComboboxItem> _listDonVi = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }
        private ObservableCollection<ComboboxItem> _voucherTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> VoucherTypes
        {
            get => _voucherTypes;
            set => SetProperty(ref _voucherTypes, value);
        }

        private ComboboxItem _voucherTypeSelected;

        public ComboboxItem VoucherTypeSelected
        {
            get => _voucherTypeSelected;
            set
            {
                SetProperty(ref _voucherTypeSelected, value);
                OnPropertyChanged(nameof(ShowColNSBD));
                OnPropertyChanged(nameof(ShowColNSSD));
            }
        }

        private ObservableCollection<SktMucLucModel> _existedMlskt;
        public ObservableCollection<SktMucLucModel> ExistedMlskt
        {
            get => _existedMlskt;
            set => SetProperty(ref _existedMlskt, value);
        }

        private ObservableCollection<SktMucLucModel> _importedMlskt;
        public ObservableCollection<SktMucLucModel> ImportedMlskt
        {
            get => _importedMlskt;
            set => SetProperty(ref _importedMlskt, value);
        }

        private List<SktMucLucModel> _mergeItems;
        public bool IsEnableSaveMLSKT => _mergeItems.Count > 0;

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }

        public bool IsEnabledMergeBtn
        {
            get => ImportedMlskt.Where(i => i.IsSelected).Count() > 0 && ExistedMlskt.Where(i => i.IsSelected).Count() == 1;
        }

        public bool IsEnabledUnmergeCommand
        {
            get => ExistedMlskt.Where(i => i.IsModified && i.IsSelected).Count() > 0;
        }

        private SktMucLucModel _selectedParent;
        public SktMucLucModel SelectedParent
        {
            get => _selectedParent;
            set
            {
                SetProperty(ref _selectedParent, value);
                OnPropertyChanged(nameof(IsEnabledMergeBtn));
            }
        }

        public Visibility ShowColNSBD => VoucherTypeSelected != null && VoucherType.NSBD_Key == VoucherTypeSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowColNSSD => VoucherTypeSelected != null && VoucherType.NSSD_Key == VoucherTypeSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;

        public DateTime NgayChungTu { get; set; }
        public string MoTa { get; set; }
        public NsSktChungTuModel Model { get; set; }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand AddMLSKTCommand { get; }
        public RelayCommand MergeCommand { get; }
        public RelayCommand UnmergeCommand { get; }
        public RelayCommand SaveMLSKTCommand { get; }
        public RelayCommand DownloadFileFtpServer { get; }
        public RelayCommand GetFileFtpCommand { get; }


        public ImportCheckDetailViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            ISktChungTuService sktChungTuService,
            ISktMucLucService sktMucLucService,
            FtpStorageService ftpStorageService,
            ISktChungTuChiTietService sktChungTuChiTietService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _sktChungTuService = sktChungTuService;
            _sktMucLucService = sktMucLucService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _ftpStorageService = ftpStorageService;
            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            AddMLSKTCommand = new RelayCommand(obj => OnAddMLSKT());
            MergeCommand = new RelayCommand(obj => OnMerge());
            UnmergeCommand = new RelayCommand(obj => OnUnMerge());
            SaveMLSKTCommand = new RelayCommand(obj => OnSaveMLSKT());
            GetFileFtpCommand = new RelayCommand(obj => OnGetFileFtpCommand());
            DownloadFileFtpServer = new RelayCommand(obj => OnDownloadFileFtpServer());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            OnResetData();
            LoadVoucherTypes();
            NgayChungTu = DateTime.Now;
        }

        private void OnResetData()
        {
            _fileName = string.Empty;
            _checkVoucherDetails = new ObservableCollection<CheckVoucherDetailImportModel>();
            _importErrors = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;
            _nsSktMucLucs = _sktMucLucService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(x => x.SKyHieu).ToList();
            _mergeItems = new List<SktMucLucModel>();
            _importedMlskt = new ObservableCollection<SktMucLucModel>();
            _existedMlskt = new ObservableCollection<SktMucLucModel>(_mapper.Map<ObservableCollection<SktMucLucModel>>(_nsSktMucLucs));
            LstFile = new ObservableCollection<FileFtpModel>();

            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(CheckVoucherDetails));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(LstFile));
        }

        private void LoadVoucherTypes()
        {
            var voucherTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key},
            };

            VoucherTypes = new ObservableCollection<ComboboxItem>(voucherTypes);
            VoucherTypeSelected = VoucherTypes.ElementAt(0);
        }
        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var currentIdDonVi = _sessionInfo.IdDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == currentIdDonVi);
            var nsDonViOfCurrentUser = _donViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private void OnUploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Chọn file excel");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            FileName = openFileDialog.FileName;
        }
        /// <summary>
        /// Lấy dữ liệu từ ftp
        /// </summary>
        private void OnGetFileFtpCommand()
        {
            if (VoucherTypeSelected == null)
            {

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng chọn đơn vị phù hợp");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                return;
            }
            var btmTenDonVi = _sessionService.Current.IdDonVi + "-" + StringUtils.UCS2Convert(_sessionService.Current.TenDonVi).Replace("---", "-");
            var sLicense = Convert.ToInt32(StringUtils.UCS2Convert(VoucherTypeSelected.ValueItem)) == 1 ? StringUtils.UCS2Convert(VoucherType.NSSD_Value) : StringUtils.UCS2Convert(VoucherType.NSBD_Value);
            string sLicenseChane = string.Format("{0}", sLicense);
            var strUrl = string.Format("{0}/{1}/{2}", btmTenDonVi, ConstantUrlPathPhanHe.UrlQlnsSktformSend, sLicenseChane);
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
        /// Download dữ liệu
        /// </summary>
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
                    string filePath = _ftpStorageService.DowLoadFileFtpGiveLocal(urlUrIDownLoad, fileName);
                    FileName = filePath;
                }
            }
            OnProcessFile();
        }

        private void ShowError()
        {
            int rowIndex = _checkVoucherDetails.IndexOf(SelectedItem);
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
                int loaiChungTu = 0;
                if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSSD_Key)
                {
                    loaiChungTu = int.Parse(VoucherType.NSSD_Key);
                }
                else if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSBD_Key)
                {
                    loaiChungTu = int.Parse(VoucherType.NSBD_Key);
                }
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                //xử lý chứng từ chi tiết
                ImportResult<CheckVoucherDetailImportModel> _voucherDetailResult = _importService.ProcessDataUnique<CheckVoucherDetailImportModel>(FileName);
                _checkVoucherDetailProcess = new List<CheckVoucherDetailImportModel>();

                //xác định cha con trong data import
                foreach (var item in _voucherDetailResult.Data)
                {
                    if (!item.IsErrorMLNS)
                    {
                        var mlns = _nsSktMucLucs.Where(x => x.SKyHieu == item.KyHieu).FirstOrDefault();
                        if (mlns != null)
                            item.BHangCha = mlns.BHangCha;
                    }
                    var childs = _voucherDetailResult.Data.Where(x => x.KyHieu.Contains(item.KyHieu) && x != item).ToList();
                    if (childs.Count > 0)
                        item.BHangCha = true;
                }

                //kiểm tra loại ngân sách con thỏa mãn điều kiện để import
                foreach (var item in _voucherDetailResult.Data)
                {
                    if (!item.ImportStatus && item.IsErrorMLNS)
                    {
                        var parents = _checkVoucherDetailProcess.Where(x => item.KyHieu.Contains(x.KyHieu) && x != item).ToList();
                        if (parents.Count > 0)
                        {
                            int columnIndexOrigin = 0;
                            CheckVoucherDetailImportModel parent = new CheckVoucherDetailImportModel();
                            foreach (var p in parents)
                            {
                                int maxColumn = p.KyHieu.Split("-").Count();
                                if (maxColumn > columnIndexOrigin)
                                {
                                    columnIndexOrigin = maxColumn;
                                    parent = p;
                                }
                            }
                            int columnIndexImport = item.KyHieu.Split("-").Count();
                            if (columnIndexOrigin < columnIndexImport)
                            {
                                if (parent.ListKyHieuChild == null)
                                    parent.ListKyHieuChild = new List<string>();
                                parent.ListKyHieuChild.Add(item.KyHieu);
                                item.KyHieuParent = parent.KyHieu;

                                var parentOrigin = _nsSktMucLucs.Where(x => x.SKyHieu == parent.KyHieu).FirstOrDefault();
                                if (parentOrigin != null && !parentOrigin.BHangCha)
                                {
                                    item.IsWarning = true;
                                }
                                if (parent.IsWarning)
                                    item.IsWarning = true;
                            }
                        }
                    }
                    _checkVoucherDetailProcess.Add(item);
                }

                if (loaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key)))
                {
                    foreach (var item in _checkVoucherDetailProcess)
                    {
                        item.MuaHangHienVat = "";
                        item.DacThu = "";
                        item.HuyDong = "";
                    }
                }
                else
                {
                    foreach (var item in _checkVoucherDetailProcess)
                    {
                        item.TuChi = "";
                    }
                }

                //CalculateData();
                _checkVoucherDetails = new ObservableCollection<CheckVoucherDetailImportModel>(_checkVoucherDetailProcess);
                foreach (var item in _checkVoucherDetails)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName != nameof(CheckVoucherDetailImportModel.ImportStatus)
                            && args.PropertyName != nameof(CheckVoucherDetailImportModel.KyHieu)
                            && args.PropertyName != nameof(CheckVoucherDetailImportModel.IsErrorMLNS))
                        {
                            var voucherDetail = (CheckVoucherDetailImportModel)sender;
                            int rowIndex = _checkVoucherDetails.IndexOf(voucherDetail);
                            var listError = _importService.ValidateItem<CheckVoucherDetailImportModel>(voucherDetail, rowIndex);
                            if (listError.Count > 0)
                            {
                                List<string> errors = listError.Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                                string message = string.Join(Environment.NewLine, errors);
                                System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
                                _importErrors.AddRange(listError);
                                voucherDetail.ImportStatus = false;
                                if (listError.Any(x => x.IsErrorMLNS))
                                    voucherDetail.IsErrorMLNS = true;
                            }
                            else
                            {
                                voucherDetail.ImportStatus = true;
                                voucherDetail.IsErrorMLNS = false;
                                _importErrors.RemoveAll(x => x.Row == rowIndex);
                                OnPropertyChanged(nameof(IsSaveData));
                            }
                        }
                    };
                }

                OnPropertyChanged(nameof(CheckVoucherDetails));
                if (_voucherDetailResult.ImportErrors.Count > 0)
                    errors.AddRange(_voucherDetailResult.ImportErrors);

                if (_checkVoucherDetails == null || _checkVoucherDetails.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (CheckVoucherDetails.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                if (errors.Count > 0)
                {
                    _importErrors = new ObservableCollection<ImportErrorItem>(errors).ToList();
                    OnPropertyChanged(nameof(ImportErrors));
                }
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                if (ex is Utility.Exceptions.WrongReportException)
                {
                    System.Windows.MessageBox.Show(Resources.WrongReportFormat, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void OnSaveData()
        {
            var NamLamViec = _sessionInfo.YearOfWork;
            var currentDV = GetNsDonViOfCurrentUser();
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            NsSktChungTu chungTu;
            chungTu = _sktChungTuService.FindByCondition(PredicateBuilder.True<NsSktChungTu>().And(i =>
                i.INamLamViec == NamLamViec && i.Id == Model.Id )).FirstOrDefault();
            if (chungTu == null)
            {
                System.Windows.MessageBox.Show(string.Format(Resources.VoucherIsExists, VoucherTypeSelected.DisplayItem, currentDV.TenDonVi), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _nsSktMucLucs = _sktMucLucService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork).ToList();

            List<NsSktChungTuChiTiet> chungTuChiTiets = new List<NsSktChungTuChiTiet>();
            List<CheckVoucherDetailImportModel> listDetailImport = _checkVoucherDetails.Where(x => x.ImportStatus && !x.IsWarning && _nsSktMucLucs.Any(y => y.SKyHieu == x.KyHieu && !y.BHangCha)).ToList();
            var predicate = PredicateBuilder.True<NsSktChungTuChiTiet>();
            predicate = predicate.And(x => x.IIdCtsoKiemTra == Model.Id);
            predicate = predicate.And(x => x.ILoai == Model.ILoai);
            var sktChungTuChiTiets = _sktChungTuChiTietService.FindByCondition(predicate);
            int countUpdate = 0; 
            foreach (var item in listDetailImport)
            {
                NsSktMucLuc mucLuc = _nsSktMucLucs.FirstOrDefault((x => x.SKyHieu.Equals(item.KyHieu) && x.INamLamViec == NamLamViec && x.ITrangThai == StatusType.ACTIVE));
                if (item.KyHieu.Equals("") || mucLuc == null)
                {
                    continue;
                }
                if (!sktChungTuChiTiets.Select(s => s.SKyHieu).Contains(item.KyHieu))
                {
                    NsSktChungTuChiTiet nsSktChiTiet = new NsSktChungTuChiTiet();
                    nsSktChiTiet.IIdCtsoKiemTra = chungTu.Id;
                    nsSktChiTiet.IIdMaDonVi = chungTu.IIdMaDonVi;
                    nsSktChiTiet.STenDonVi = chungTu.STenDonVi;
                    nsSktChiTiet.IIdMlskt = mucLuc.IIDMLSKT;
                    nsSktChiTiet.SMoTa = mucLuc.SMoTa;
                    nsSktChiTiet.SKyHieu = mucLuc.SKyHieu;
                    nsSktChiTiet.ILoai = DemandCheckType.CHECK;
                    nsSktChiTiet.INamNganSach = yearOfBudget;
                    nsSktChiTiet.IIdMaNguonNganSach = budgetSource;
                    nsSktChiTiet.INamLamViec = NamLamViec;
                    nsSktChiTiet.DNgayTao = DateTime.Now;
                    nsSktChiTiet.ILoaiChungTu = chungTu.ILoaiChungTu;
                    nsSktChiTiet.FTuChi = item.TuChi == "" ? 0 : Double.Parse(item.TuChi);
                    nsSktChiTiet.FMuaHangCapHienVat = item.MuaHangHienVat == "" ? 0 : Double.Parse(item.MuaHangHienVat);
                    nsSktChiTiet.FHuyDongTonKho = item.HuyDong == "" ? 0 : Double.Parse(item.HuyDong);
                    nsSktChiTiet.FPhanCap = item.DacThu == "" ? 0 : Double.Parse(item.DacThu);
                    nsSktChiTiet.SNguoiTao = _sessionInfo.Principal;
                    chungTuChiTiets.Add(nsSktChiTiet);
                }
                else
                {
                    sktChungTuChiTiets.ForAll(s =>
                    {
                        if (s.SKyHieu == item.KyHieu)
                        {                           
                            s.FTuChi = item.TuChi == "" ? 0 : Double.Parse(item.TuChi);
                            s.FMuaHangCapHienVat = item.MuaHangHienVat == "" ? 0 : Double.Parse(item.MuaHangHienVat);
                            s.FHuyDongTonKho = item.HuyDong == "" ? 0 : Double.Parse(item.HuyDong);
                            s.FPhanCap = item.DacThu == "" ? 0 : Double.Parse(item.DacThu);
                            s.DNgaySua = DateTime.Now;
                            s.SNguoiSua = _sessionInfo.Principal;
                            countUpdate++;
                        }
                    });
                }

            }
            _sktChungTuChiTietService.AddRange(chungTuChiTiets);
            if(countUpdate > 0)
                _sktChungTuChiTietService.UpdateRange(sktChungTuChiTiets);
            if (chungTuChiTiets.Count > 0 || sktChungTuChiTiets.Count() > 0)
            {
                chungTu.FTongTuChi = chungTuChiTiets.Sum(item => item.FTuChi) + sktChungTuChiTiets.Sum(item => item.FTuChi);
                chungTu.FTongPhanCap = chungTuChiTiets.Sum(item => item.FPhanCap) + sktChungTuChiTiets.Sum(item => item.FPhanCap);
                chungTu.FTongMuaHangCapHienVat = chungTuChiTiets.Sum(item => item.FMuaHangCapHienVat) + sktChungTuChiTiets.Sum(item => item.FMuaHangCapHienVat);
                _sktChungTuService.Update(chungTu);
            }

            NsSktChungTuModel demandVoucher = _mapper.Map<NsSktChungTuModel>(chungTu);
            DialogHost.CloseDialogCommand.Execute(demandVoucher, null);
            SavedAction?.Invoke(demandVoucher);
        }

        private void OnAddMLSKT()
        {
            TabIndex = ImportTabIndex.MLNS;
            SktMucLucModel importItem = new SktMucLucModel();
            if (ImportedMlskt.Contains(importItem))
                return;

            bool isImportGroup = false;
            if (!_importedMlskt.Any(x => x.SKyHieu.Contains(SelectedItem.KyHieu))
                || !_existedMlskt.Any(x => x.SKyHieu.Contains(SelectedItem.KyHieu)))
            {
                List<CheckVoucherDetailImportModel> data = new List<CheckVoucherDetailImportModel>();
                data.Add(SelectedItem);
                if (SelectedItem.BHangCha)
                    data.AddRange(_checkVoucherDetailProcess.Where(x => SelectedItem.ListKyHieuChild.Contains(x.KyHieu)).ToList());
                else
                {
                    var it = _checkVoucherDetailProcess.Where(x => x.KyHieu == SelectedItem.KyHieuParent)
                        .FirstOrDefault();
                    if (it != null)
                    {
                        data.Add(it);
                    }
                }
                if (data.Count > 1)
                    isImportGroup = true;
                data = data.OrderBy(x => x.KyHieu).ToList();
                foreach (var item in data)
                {
                    if (!_nsSktMucLucs.Any(x => x.SKyHieu == item.KyHieu))
                        _importedMlskt.Add(new SktMucLucModel()
                        {
                            IIDMLSKT = Guid.NewGuid(),
                            SKyHieu = item.KyHieu,
                            SNg = item.Nganh,
                            SNGCha = item.NganhCha,
                            SSTT = item.STT,
                            SMoTa = item.Description,
                            BHangCha = item.BHangCha,
                            ITrangThai = StatusType.ACTIVE,
                            INamLamViec = _sessionInfo.YearOfWork,
                            SM = "",
                            SLoaiNhap = "1,2",
                            IsModified = true
                        });
                }
            }
            foreach (SktMucLucModel model in _importedMlskt.ToList())
            {
                SktMucLucModel parent = null;
                if (isImportGroup && !model.BHangCha)
                    parent = _importedMlskt.Where(x => model.SKyHieu.Contains(x.SKyHieu) && model.SKyHieu != x.SKyHieu).FirstOrDefault();
                if (parent == null)
                    parent = FindParent(model, _existedMlskt);
                if (parent != null)
                {
                    int index = _existedMlskt.IndexOf(parent);
                    _existedMlskt.Insert(index + 1, model);
                    //_importedMlns.Remove(model);
                    model.IIDMLSKTCha = parent.IIDMLSKT;
                    model.BHangCha = model.BHangCha;
                    model.ITrangThai = 1;
                    model.DNguoiTao = _sessionInfo.Principal;
                    model.SM = "";
                    model.DNgayTao = DateTime.Now;
                    _mergeItems.Add(model);
                    OnPropertyChanged(nameof(IsEnableSaveMLSKT));
                }
            }
            _importedMlskt = new ObservableCollection<SktMucLucModel>();
            OnPropertyChanged(nameof(ExistedMlskt));
            OnPropertyChanged(nameof(ImportedMlskt));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnSelectionChanged();
        }

        private void OnSelectionChanged()
        {
            foreach (var i in _existedMlskt)
            {
                i.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(ModelBase.IsSelected))
                    {
                        OnPropertyChanged(nameof(IsEnabledMergeBtn));
                        OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                    }
                };
            }
            foreach (var i in _importedMlskt.Where(x => x.IsModified))
            {
                i.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(ModelBase.IsSelected))
                    {
                        OnPropertyChanged(nameof(IsEnabledMergeBtn));
                        OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                    }
                };
            }
        }

        public SktMucLucModel FindParent(SktMucLucModel model, IEnumerable<SktMucLucModel> ExistedMlns)
        {
            IEnumerable<SktMucLucModel> ancestors = _existedMlskt.Where(i => !Guid.Empty.Equals(i.Id) && !model.SKyHieu.Equals(i.SKyHieu) &&
                                                                            model.SKyHieu.StartsWith(i.SKyHieu + "-") && model.INamLamViec == i.INamLamViec)
                .OrderByDescending(i => i.SKyHieu.Length);
            return ancestors.FirstOrDefault();
        }

        private void OnMerge()
        {
            if (SelectedParent == null)
                return;
            int index = _existedMlskt.ToList().FindIndex(x => x.IsSelected);
            _mergeItems = _importedMlskt.Where(i => i.IsSelected && i.IsModified).ToList();
            foreach (var item in _mergeItems)
            {
                item.IIDMLSKTCha = SelectedParent.IIDMLSKT;
                item.BHangCha = false;
                item.ITrangThai = 1;
                item.DNguoiTao = _sessionInfo.Principal;
                item.DNgayTao = DateTime.Now;
            }

            List<SktMucLucModel> nsMuclucSktModels = _existedMlskt.ToList();
            nsMuclucSktModels.InsertRange(index + 1, _mergeItems);
            _existedMlskt = new ObservableCollection<SktMucLucModel>(nsMuclucSktModels);
            _importedMlskt = new ObservableCollection<SktMucLucModel>(ImportedMlskt.Where(i => !i.IsSelected || !i.IsModified));
            OnPropertyChanged(nameof(ExistedMlskt));
            OnPropertyChanged(nameof(ImportedMlskt));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnPropertyChanged(nameof(IsEnableSaveMLSKT));
            OnSelectionChanged();
        }

        private void OnUnMerge()
        {
            IEnumerable<SktMucLucModel> unmergeItems = _existedMlskt.Where(i => i.IsSelected && i.IsModified).ToList();
            foreach (var item in unmergeItems)
            {
                _mergeItems.Remove(item);
            }
            List<SktMucLucModel> nsMuclucSktModels = ImportedMlskt.ToList();
            nsMuclucSktModels.AddRange(unmergeItems);
            _importedMlskt = new ObservableCollection<SktMucLucModel>(nsMuclucSktModels);
            _existedMlskt = new ObservableCollection<SktMucLucModel>(_existedMlskt.Where(i => !i.IsSelected || !i.IsModified));
            OnPropertyChanged(nameof(ExistedMlskt));
            OnPropertyChanged(nameof(ImportedMlskt));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnPropertyChanged(nameof(IsEnableSaveMLSKT));
            OnSelectionChanged();
        }

        private void OnSaveMLSKT()
        {
            var result = System.Windows.MessageBox.Show(Resources.ConfirmAddMLNS, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    List<NsSktMucLuc> listMLSKT = _mapper.Map<List<NsSktMucLuc>>(_mergeItems);
                    _sktMucLucService.AddRange(listMLSKT);
                    _existedMlskt.Where(x => x.IsModified).Select(x => { x.IsModified = false; x.IsSelected = false; return x; }).ToList();
                    _mergeItems = new List<SktMucLucModel>();
                    OnPropertyChanged(nameof(ExistedMlskt));
                    OnPropertyChanged(nameof(IsEnableSaveMLSKT));
                    System.Windows.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);

                    foreach (var item in listMLSKT)
                    {
                        var importItem = _checkVoucherDetails.Where(x => x.KyHieu == item.SKyHieu).FirstOrDefault();
                        var listError = _importService.ValidateItem<CheckVoucherDetailImportModel>(importItem, _checkVoucherDetails.IndexOf(importItem));
                        if (listError.Count == 0)
                        {
                            importItem.ImportStatus = true;
                            importItem.IsErrorMLNS = false;
                            TabIndex = ImportTabIndex.Data;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(Resources.MsgSaveError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}
