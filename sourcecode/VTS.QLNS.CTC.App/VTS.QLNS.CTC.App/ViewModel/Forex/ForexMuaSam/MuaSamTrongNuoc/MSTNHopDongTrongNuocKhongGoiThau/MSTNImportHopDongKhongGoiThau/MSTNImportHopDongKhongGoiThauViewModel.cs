using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using System.Windows.Forms;
using System.Windows;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Core.Domain;
using System.Globalization;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNHopDongTrongNuocKhongGoiThau.MSTNImportHopDongKhongGoiThau
{
    public class MSTNImportHopDongKhongGoiThauViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ILog _logger;
        private readonly IImportExcelService _importService;
        private readonly INhDaHopDongService _iNhDaHopDongService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDmLoaiHopDongService _nhdmLoaiHopDongService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INhMstnKeHoachDatHangService _nhKhDatHangService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNHopDongTrongNuocKhongGoiThau.MSTNImportHopDongKhongGoiThau.MSTNImportHopDongKhongGoiThau);
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private ObservableCollection<HopDongTrongNuocKhongGoiThauImportModel> _hopDongImportModels;
        public ObservableCollection<HopDongTrongNuocKhongGoiThauImportModel> HopDongImportModels
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
            set
            {
                if (SetProperty(ref _selectedDonVi, value) && value != null)
                {
                    LoadKeHoachTongThe();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsKeHoachTongThe;
        public ObservableCollection<ComboboxItem> ItemsKeHoachTongThe
        {
            get => _itemsKeHoachTongThe;
            set => SetProperty(ref _itemsKeHoachTongThe, value);
        }

        private ComboboxItem _selectedKeHoachTongThe;
        public ComboboxItem SelectedKeHoachTongThe
        {
            get => _selectedKeHoachTongThe;
            set
            {
                if (SetProperty(ref _selectedKeHoachTongThe, value) && value != null)
                {
                    LoadChuongTrinh();
                }
            }
        }

        private ObservableCollection<NhDmNhiemVuChiModel> _itemsChuongTrinh;
        public ObservableCollection<NhDmNhiemVuChiModel> ItemsChuongTrinh
        {
            get => _itemsChuongTrinh;
            set => SetProperty(ref _itemsChuongTrinh, value);
        }

        private NhDmNhiemVuChiModel _selectedChuongTrinh;
        public NhDmNhiemVuChiModel SelectedChuongTrinh
        {
            get => _selectedChuongTrinh;
            set => SetProperty(ref _selectedChuongTrinh, value);
        }

        //private ObservableCollection<NhMstnKeHoachDatHangModel> _itemsKeHoachDatHang;
        //public ObservableCollection<NhMstnKeHoachDatHangModel> ItemsKeHoachDatHang
        //{
        //    get => _itemsKeHoachDatHang;
        //    set => SetProperty(ref _itemsKeHoachDatHang, value);
        //}

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

        private bool _isUploadFile;
        public bool IsUploadFile
        {
            get => _isUploadFile;
            set => SetProperty(ref _isUploadFile, value);
        }
        public int ILoai { get; set; }
        public int IThuocMenu { get; set; }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorHopDongCommand { get; }

        public MSTNImportHopDongKhongGoiThauViewModel(ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IImportExcelService importService,
            INhDaHopDongService iNhDmHopDongService,
            INsDonViService nsDonViService,
            INhDmLoaiHopDongService nhdmLoaiHopDongService,
            INhMstnKeHoachDatHangService nhKhDatHangService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            INhKhTongTheService nhKhTongTheService
            )
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _importService = importService;
            _nsDonViService = nsDonViService;
            _nhdmLoaiHopDongService = nhdmLoaiHopDongService;
            _iNhDaHopDongService = iNhDmHopDongService;
            _nhKhDatHangService = nhKhDatHangService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _nhKhTongTheService = nhKhTongTheService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData(obj));
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorHopDongCommand = new RelayCommand(obj => ShowErrorHopDong());
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

        private void LoadKeHoachTongThe()
        {
            var lstKeHoachTongThe = _nhKhTongTheService.FindByDonViId(SelectedDonVi.Id).ToList();
            if (lstKeHoachTongThe.Any())
            {
                var result = lstKeHoachTongThe.Select(x =>
                {
                    ComboboxItem cb = new ComboboxItem();
                    if (x.INamKeHoach.HasValue)
                    {
                        cb.DisplayItem = "KHTT " + x.INamKeHoach.Value + "- Số KH: " + x.SSoKeHoachBqp;
                        cb.ValueItem = x.Id.ToString();
                    }
                    else
                    {
                        cb.DisplayItem = "KHTT " + x.IGiaiDoanTu_BQP.GetValueOrDefault() + "-" + x.IGiaiDoanDen_BQP.GetValueOrDefault() + " - Số KH: " + x.SSoKeHoachBqp;
                        cb.ValueItem = x.Id.ToString();
                    }
                    return cb;
                }).ToList();
                _itemsKeHoachTongThe = new ObservableCollection<ComboboxItem>(result);
            }
            else
            {
                _itemsKeHoachTongThe = new ObservableCollection<ComboboxItem>();
            }
            OnPropertyChanged(nameof(ItemsKeHoachTongThe));
        }

        private void LoadChuongTrinh()
        {
            ItemsChuongTrinh = new ObservableCollection<NhDmNhiemVuChiModel>();
            if (_selectedKeHoachTongThe != null && _selectedDonVi != null)
            {
                var data = _nhDmNhiemVuChiService.FindByKhTongTheIdAndDonViId(Guid.Parse(SelectedKeHoachTongThe.ValueItem), SelectedDonVi.Id);
                _itemsChuongTrinh = _mapper.Map<ObservableCollection<NhDmNhiemVuChiModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsChuongTrinh));
        }

        //private void LoadKeHoachDatHang()
        //{
        //    _itemsKeHoachDatHang = new ObservableCollection<NhMstnKeHoachDatHangModel>();
        //    if (SelectedDonVi != null && SelectedKeHoachTongThe != null && SelectedChuongTrinh != null)
        //    {
        //        IEnumerable<NhMstnKeHoachDatHangQuery> data = _nhKhDatHangService.FindMstnKeHoachDatHangByCondition(SelectedDonVi.Id, Guid.Parse(SelectedKeHoachTongThe.ValueItem), SelectedChuongTrinh.IIdKHTTNhiemVuChiId);
        //        _itemsKeHoachDatHang = _mapper.Map<ObservableCollection<NhMstnKeHoachDatHangModel>>(data);
        //    }
        //    OnPropertyChanged(nameof(ItemsKeHoachDatHang));
        //}

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
            if (SelectedKeHoachTongThe == null)
            {
                message = Resources.MsgCheckKeHoachTongThe;
                goto ShowError;
            }
            if (SelectedChuongTrinh == null)
            {
                message = Resources.MsgCheckChuongTrinh;
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
                ImportResult<HopDongTrongNuocKhongGoiThauImportModel> dataImport = _importService.ProcessData<HopDongTrongNuocKhongGoiThauImportModel>(FileName);
                HopDongImportModels = new ObservableCollection<HopDongTrongNuocKhongGoiThauImportModel>(dataImport.Data);

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

        private List<NhDaHopDongModel> ConvertData(List<HopDongTrongNuocKhongGoiThauImportModel> importModels, bool isAddMessage = true)
        {
            List<NhDaHopDongModel> results = new List<NhDaHopDongModel>();
            NhDaHopDongModel it;
            IEnumerable<NhDmLoaiHopDong> nhDmLoaiHopDongs = _nhdmLoaiHopDongService.FindAll();
            IEnumerable<NhMstnKeHoachDatHangQuery> nhMSTNKeHoachDatHangs = _nhKhDatHangService.FindMstnKeHoachDatHangByCondition(SelectedDonVi.Id, Guid.Parse(SelectedKeHoachTongThe.ValueItem), SelectedChuongTrinh.IIdKHTTNhiemVuChiId);
            IEnumerable<NhDaHopDong> nhDaHopDongs = _iNhDaHopDongService.FindByCondition(x => x.BIsActive == true && x.IThuocMenu == IThuocMenu && x.ILoai == ILoai);
            int i = 0;
            foreach (var import in importModels)
            {
                it = new NhDaHopDongModel();
                it.IThuocMenu = IThuocMenu;
                it.ILoai = ILoai;
                it.DNgayTao = DateTime.Now;
                it.SNguoiTao = _sessionInfo.Principal;
                it.BIsActive = true;
                it.BIsKhoa = false;
                it.BIsGoc = true;
                it.ILanDieuChinh = 0;
                it.ImportStatus = import.ImportStatus;
                it.IsWarning = import.IsWarning;
                it.IIdDonViQuanLyId = SelectedDonVi.Id;
                it.IIdMaDonViQuanLy = SelectedDonVi.IIDMaDonVi;
                it.IIdKhTongTheId = Guid.Parse(SelectedKeHoachTongThe.ValueItem);
                it.IIdKhTongTheNhiemVuChiId = SelectedChuongTrinh.IIdKHTTNhiemVuChiId;
                it.STT = import.STT;
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
                it.STenHopDong = import.TenHopDong;
                it.DNgayHopDong = ParseUtils.TryParseDateTime(import.NgayKiHopDong);
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
                it.SMaKeHoachDatHang = import.MaKeHoachDatHang;
                if (!string.IsNullOrEmpty(import.MaKeHoachDatHang))
                {
                    var khDatHang = nhMSTNKeHoachDatHangs.FirstOrDefault(x => x.SSoQuyetDinh.Equals(import.MaKeHoachDatHang));
                    if (khDatHang != null)
                    {
                        it.IIdKeHoachDatHangId = khDatHang.Id;
                        it.SMaKeHoachDatHang = khDatHang.SSoQuyetDinh;
                    }
                    else
                    {
                        it.ImportStatus = false;
                        if (isAddMessage) _importErrors.Add(new ImportErrorItem { Row = i, Error = Resources.MsgNotExistData, ColumnName = "Mã kế hoạch đặt hàng (Số quyết định)" });
                    }
                }
                it.IThoiGianThucHien = ParseUtils.TryParseInt(import.IThoiGianThucHien);
                it.DKhoiCongDuKien = ParseUtils.TryParseDateTime(import.SKhoiCong);
                it.DKetThucDuKien = ParseUtils.TryParseDateTime(import.SKetThuc);
                it.FGiaTriHopDongVND = ParseUtils.TryParseDouble(import.FGiaTriHopDongVND);
                it.FGiaTriHopDongUSD = ParseUtils.TryParseDouble(import.FGiaTriHopDongUSD);
                results.Add(it);
                i++;
            }
            return results;
        }

        private void OnResetData()
        {
            _fileName = "Lựa chọn file Excel";
            HopDongImportModels = new ObservableCollection<HopDongTrongNuocKhongGoiThauImportModel>();
            ItemsHopDong = new ObservableCollection<NhDaHopDongModel>();
            _importErrors = new List<ImportErrorItem>();
            IsUploadFile = false;
            SelectedDonVi = null;
            SelectedKeHoachTongThe = null;
            SelectedChuongTrinh = null;
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

        public override void OnClose(object obj)
        {
            Window window = obj as Window;
            window?.Close();
        }
    }
}
