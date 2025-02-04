using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using AutoMapper;
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

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTNHHopDongTrongNuoc.MSNTImportHopDong

{
    public class MSNTImportHopDongViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ILog _logger;
        private readonly IImportExcelService _importService;
        private readonly INhDaHopDongService _iNhDaHopDongService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDmNhaThauService _nhDmNhaThauService;
        private readonly INhDmLoaiHopDongService _nhdmLoaiHopDongService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(View.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTNHHopDongTrongNuoc.ImportHopDong.ImportHopDong);
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private ObservableCollection<HopDongImportModel> _hopDongImportModels;
        public ObservableCollection<HopDongImportModel> HopDongImportModels
        {
            get => _hopDongImportModels;
            set => SetProperty(ref _hopDongImportModels, value);
        }
        public ObservableCollection<NhDaHopDongModel> _itemsHopDong;
        public ObservableCollection<NhDaHopDongModel> ItemsHopDong
        {
            get => _itemsHopDong;
            set => SetProperty(ref _itemsHopDong, value);
        }

        private NhDaHopDongModel _seletedHopDong;
        public NhDaHopDongModel SeletedHopDong
        {
            get => _seletedHopDong;
            set => SetProperty(ref _seletedHopDong, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private bool _isUploadFile;
        public bool IsUploadFile
        {
            get => _isUploadFile;
            set => SetProperty(ref _isUploadFile, value);
        }

        private bool _isEnabledCombobox;
        public bool IsEnabledCombobox
        {
            get => _isEnabledCombobox;
            set => SetProperty(ref _isEnabledCombobox, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (HopDongImportModels.Count > 0)
                    //return !HopDongImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                    return true;
                return false;
            }
        }

        public int ILoai { get; set; }
        public int IThuocMenu { get; set; }
        public MSNTNHHopDongTrongNuocDialogViewModel NHHopDongTrongNuocDialogViewModel { get; set;}
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorHopDongCommand { get; }
        //public RelayCommand HieuChinhCommand { get; }

        public MSNTImportHopDongViewModel(ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IImportExcelService importService,
            INhDaHopDongService iNhDmHopDongService,
            INsDonViService nsDonViService,
            INhDmNhaThauService nhDmNhaThauService,
            INhDmLoaiHopDongService nhdmLoaiHopDongService,
            MSNTNHHopDongTrongNuocDialogViewModel nhHopDongTrongNuocDialogViewModel)
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _importService = importService;
            _nsDonViService = nsDonViService;
            _nhdmLoaiHopDongService = nhdmLoaiHopDongService;
            _nhDmNhaThauService = nhDmNhaThauService;
            _iNhDaHopDongService = iNhDmHopDongService;

            NHHopDongTrongNuocDialogViewModel = nhHopDongTrongNuocDialogViewModel;
            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData(obj));
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorHopDongCommand = new RelayCommand(obj => ShowErrorHopDong());
            //HieuChinhCommand = new RelayCommand(obj => OnHieuChinh());
        }

        private void OnHieuChinh()
        {
            if (SeletedHopDong != null)
            {
                NHHopDongTrongNuocDialogViewModel.IsDetail = false;
                NHHopDongTrongNuocDialogViewModel.Model = SeletedHopDong;
                NHHopDongTrongNuocDialogViewModel.IsHieuChinhImport = true;
                NHHopDongTrongNuocDialogViewModel.SavedAction = obj =>
                {
                    SeletedHopDong.ImportStatus = true;
                    ItemsHopDong = new ObservableCollection<NhDaHopDongModel>(ItemsHopDong);
                };
                NHHopDongTrongNuocDialogViewModel.Init();
                NHHopDongTrongNuocDialogViewModel.ShowDialog();
            }
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            IsEnabledCombobox = true;
            LoadDonVi();
            OnResetData();
        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindByCondition(x => x.NamLamViec == _sessionInfo.YearOfWork).OrderBy(x => x.IIDMaDonVi);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void OnResetData()
        {
            _fileName = "Lựa chọn file Excel";
            HopDongImportModels = new ObservableCollection<HopDongImportModel>();
            ItemsHopDong = new ObservableCollection<NhDaHopDongModel>();
            _importErrors = new List<ImportErrorItem>();
            IsUploadFile = false;
            IsEnabledCombobox = true;
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void OnUploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Chọn file excel");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            FileName = openFileDialog.FileName;
            IsUploadFile = true;
        }

        private void ShowErrorHopDong()
        {
            int rowIndex = ItemsHopDong.IndexOf(SeletedHopDong);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OnProcessFile()
        {
            string message = string.Empty;
            if (!IsUploadFile || (IsUploadFile && string.IsNullOrEmpty(FileName)))
            {
                message = Resources.ErrorFileEmpty;
                goto ShowError;
            }
            if (SelectedDonVi == null)
            {
                message = Resources.MsgCheckDonVi;
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
                _importErrors.Clear();
                ImportResult<HopDongImportModel> dataImport = _importService.ProcessData<HopDongImportModel>(FileName);
                HopDongImportModels = new ObservableCollection<HopDongImportModel>(dataImport.Data);

                if (dataImport.ImportErrors.Count > 0) _importErrors.AddRange(dataImport.ImportErrors);

                if (HopDongImportModels == null || HopDongImportModels.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                //if (HopDongImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                //    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                ItemsHopDong = new ObservableCollection<NhDaHopDongModel>(ConvertData(HopDongImportModels.ToList()));
                IsEnabledCombobox = false;
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(ImportErrors));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnSaveData(object obj)
        {
            try
            {
                List<NhDaHopDongModel> lstModel = ConvertData(HopDongImportModels.ToList(), false);
                List<NhDaHopDongModel> importList = lstModel.Where(x => x.ImportStatus).ToList();
                if (importList.Any())
                {
                    List<NhDaHopDong> entities = _mapper.Map<List<NhDaHopDong>>(importList);
                    _iNhDaHopDongService.AddRange(entities);
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    SavedAction?.Invoke(null);
                    Window window = obj as Window;
                    window?.Close();
                }
                else
                {
                    MessageBoxHelper.Warning(Resources.MsgNotDataCorrectly);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        private List<NhDaHopDongModel> ConvertData(List<HopDongImportModel> importModels, bool isAddMessage = true)
        {
            List<NhDaHopDongModel> results = new List<NhDaHopDongModel>();
            NhDaHopDongModel it;
            IEnumerable<NhDmLoaiHopDong> nhDmLoaiHopDongs = _nhdmLoaiHopDongService.FindAll();
            IEnumerable<NhDmNhaThau> nhDmNhaThaus = _nhDmNhaThauService.FindAll();
            IEnumerable<NhDaHopDong> nhDaHopDongs = _iNhDaHopDongService.FindByCondition(x => x.BIsActive == true && x.IThuocMenu == IThuocMenu && x.ILoai == ILoai);
            int i = 0;
            foreach (HopDongImportModel import in importModels)
            {
                it = new NhDaHopDongModel();
                it.ImportStatus = import.ImportStatus;
                it.IsWarning = import.IsWarning;
                it.IThuocMenu = IThuocMenu;
                it.ILoai = ILoai;
                it.DNgayTao = DateTime.Now;
                it.SNguoiTao = _sessionInfo.Principal;
                it.BIsActive = true;
                it.BIsKhoa = false;
                it.BIsGoc = true;
                it.ILanDieuChinh = 0;
                it.SMaLoaiHopDong = import.MaLoaiHopDong;
                if (!string.IsNullOrEmpty(import.MaLoaiHopDong))
                {
                    var loaiHopDong = nhDmLoaiHopDongs.FirstOrDefault(x => x.SMaLoaiHopDong.ToUpper().Equals(import.MaLoaiHopDong?.ToUpper()));
                    if (loaiHopDong != null)
                    {
                        it.SMaLoaiHopDong = loaiHopDong.SMaLoaiHopDong;
                        it.SLoaiHopDong = loaiHopDong.STenLoaiHopDong;
                        it.IIdLoaiHopDongId = loaiHopDong.IIdLoaiHopDongId;
                    }
                    else
                    {
                        it.ImportStatus = false;
                        if (isAddMessage) _importErrors.Add(new ImportErrorItem { Row = i, Error = Resources.MsgNotExistData, ColumnName = "Loại hợp đồng" });
                    }
                }
                it.SMaNhaThauThucHien = import.MaNhaThauDaiDien;
                if (!string.IsNullOrEmpty(import.MaNhaThauDaiDien))
                {
                    var nhaThau = nhDmNhaThaus.FirstOrDefault(x => x.SMaNhaThau.ToUpper().Equals(import.MaNhaThauDaiDien?.ToUpper()));
                    if (nhaThau != null)
                    {
                        it.SMaNhaThauThucHien = nhaThau.SMaNhaThau;
                        it.GoiThau = nhaThau.STenNhaThau;
                        it.IIdNhaThauThucHienId = nhaThau.Id;
                    }
                    else
                    {
                        it.ImportStatus = false;
                        if (isAddMessage) _importErrors.Add(new ImportErrorItem { Row = i, Error = Resources.MsgNotExistData, ColumnName = "Nhà thầu đại diện" });
                    }
                }
                //if (!string.IsNullOrEmpty(import.MaDonVi))
                //{

                //    var DonVi = _nsDonViService.FindAll(x => x.MaTenDonVi.Equals(import.MaDonVi)).FirstOrDefault();
                //    if (DonVi != null)
                //    {
                //        it.DonVi = DonVi.TenDonVi;
                //        it.IIdDonViId = DonVi.Id;
                //    }
                //}
                it.SSoHopDong = import.SoHopDong;
                if (nhDaHopDongs.Any(x => x.SSoHopDong == import.SoHopDong))
                {
                    it.ImportStatus = false;
                    if (isAddMessage) _importErrors.Add(new ImportErrorItem { Row = i, Error = Resources.MsgErrorDataExist, ColumnName = "Số hợp đồng" });
                }
                else
                {
                    if (results.Any(x => x.SSoHopDong == import.SoHopDong))
                    {
                        it.ImportStatus = false;
                        if (isAddMessage) _importErrors.Add(new ImportErrorItem { Row = i, Error = Resources.MsgDuplicateData, ColumnName = "Số hợp đồng" });
                    }
                }
                it.IIdDonViQuanLyId = SelectedDonVi.Id;
                it.IIdMaDonViQuanLy = SelectedDonVi.IIDMaDonVi;
                it.STenHopDong = import.TenHopDong;
                it.DNgayHopDong = ParseUtils.TryParseDateTime(import.NgayKiHopDong);
                it.IThoiGianThucHien = ParseUtils.TryParseInt(import.IThoiGianThucHien);
                it.DKhoiCongDuKien = ParseUtils.TryParseDateTime(import.SKhoiCong);
                it.DKetThucDuKien = ParseUtils.TryParseDateTime(import.SKetThuc);
                it.SHinhThucHopDong = import.HinhThuchopDong;
                results.Add(it);
                i++;
            }
            return results;
        }

        public override void OnClose(object obj)
        {
            Window window = obj as Window;
            window?.Close();
        }
    }
}
