using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
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
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexKhoiTaoCapPhat.ForexDanhSachKhoiTao
{
    public class ForexDanhSachKhoiTaoImportViewModel : GridViewModelBase<NhKtKhoiTaoCapPhatChiTietModel>
    {
        private readonly INsDonViService _nsDonViService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly INhKtKhoiTaoCapPhatService _nhKtKhoiTaoCapPhatService;
        private readonly INhKtKhoiTaoCapPhatChiTietService _nhKtKhoiTaoCapPhatChiTietService;
        private readonly INhThTongHopService _nhThTongHopService;
        private IImportExcelService _importService;
        private SessionInfo _sessionInfo;

        public override string Name => "Import thông tin khởi tạo cấp phát";
        public override string Title => "Import thông tin khởi tạo cấp phát";
        public override string Description => "Import thông tin khởi tạo cấp phát";
        public override Type ContentType => typeof(View.Forex.ForexKhoiTaoCapPhat.ForexDanhSachKhoiTao.ForexDanhSachKhoiTaoImport);
        public override PackIconKind IconKind => PackIconKind.Projector;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler OpenDetail;

        public List<NhDaDuAn> ListDuAnByDonVi;
        public List<NhDaHopDong> ListHopDongByDonVi;

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private ObservableCollection<ComboboxItem> _itemsNamKhoiTao;
        public ObservableCollection<ComboboxItem> ItemsNamKhoiTao
        {
            get => _itemsNamKhoiTao;
            set => SetProperty(ref _itemsNamKhoiTao, value);
        }

        private ComboboxItem _selectedNamKhoiTao;
        public ComboboxItem SelectedNamKhoiTao
        {
            get => _selectedNamKhoiTao;
            set => SetProperty(ref _selectedNamKhoiTao, value);
        }

        private DateTime? _ngayKhoiTao;
        public DateTime? NgayKhoiTao
        {
            get => _ngayKhoiTao;
            set => SetProperty(ref _ngayKhoiTao, value);
        }

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (Items != null && Items.Count > 0)
                    return !Items.Any(x => !x.ImportStatus);
                return false;
            }
        }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ResetDataCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand SelectionDoubleClickCommand { get; }
        public RelayCommand ShowErrorCommand { get; }

        public ForexDanhSachKhoiTaoImportViewModel(INsDonViService nsDonViService,
            ISessionService sessionService,
            IImportExcelService importService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDaDuAnService nhDaDuAnService,
            INhDaHopDongService nhDaHopDongService,
            INhKtKhoiTaoCapPhatService nhKtKhoiTaoCapPhatService,
            INhKtKhoiTaoCapPhatChiTietService nhKtKhoiTaoCapPhatChiTietService,
            INhThTongHopService nhThTongHopService,
            ILog logger,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _importService = importService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDaDuAnService = nhDaDuAnService;
            _nhDaHopDongService = nhDaHopDongService;
            _nhKtKhoiTaoCapPhatService = nhKtKhoiTaoCapPhatService;
            _nhKtKhoiTaoCapPhatChiTietService = nhKtKhoiTaoCapPhatChiTietService;
            _nhThTongHopService = nhThTongHopService;
            _logger = logger;
            _mapper = mapper;

            ShowErrorCommand = new RelayCommand(obj => ShowError());
            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ResetDataCommand = new RelayCommand(obj => OnResetConditon());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
        }

        public void OnProcessFile()
        {
            try
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckFileImport, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!Validate()) return;
                if (!CheckUnique()) return;

                _importErrors = new List<ImportErrorItem>();

                ListDuAnByDonVi = _nhDaDuAnService.FindAll(x => x.SMaDuAn != null).Where(x => x.IIdDonViQuanLyId == Guid.Parse(SelectedDonVi.HiddenValue)).ToList();
                ListHopDongByDonVi = _nhDaHopDongService.FindAll().Where(x => x.SSoHopDong != null && x.IIdDonViQuanLyId == Guid.Parse(SelectedDonVi.HiddenValue)).ToList();

                _importService.SetLastRowToRead(0);
                ImportResult<NhKtKhoiTaoCapPhatImportModel> _chungTuResult = _importService.ProcessData<NhKtKhoiTaoCapPhatImportModel>(FilePath);

                foreach (NhKtKhoiTaoCapPhatImportModel item in _chungTuResult.Data)
                {
                    int index = _chungTuResult.Data.IndexOf(item);
                    item.ImportStatus = true;
                    //Validate Du An
                    if (!string.IsNullOrEmpty(item.SMaDuAn) && !ValidateDuAnByDonVi(item))
                    {
                        item.ImportStatus = false;
                        if (_importErrors.Where(n => n.Row == index && n.Error == Resources.MsgImportErrorProjectCodeAgency).ToList().Count == 0)
                            _importErrors.Add(new ImportErrorItem { Row = index, Error = Resources.MsgImportErrorProjectCodeAgency });
                    }
                    //Validate Hop Dong
                    if (!string.IsNullOrEmpty(item.SMaHopDong) && !ValidateHopDongByDonVi(item))
                    {
                        item.ImportStatus = false;
                        if (_importErrors.Where(n => n.Row == index && n.Error == Resources.MsgImportErrorProjectCodeAgency).ToList().Count == 0)
                            _importErrors.Add(new ImportErrorItem { Row = index, Error = Resources.MsgImportErrorProjectCodeAgency });
                    }
                }
                Items = _mapper.Map<ObservableCollection<NhKtKhoiTaoCapPhatChiTietModel>>(ConvertData(_chungTuResult.Data));

                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));

                if (Items.Any(x => !x.ImportStatus))
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
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

        private void ShowError()
        {
            int rowIndex = Items.IndexOf(SelectedItem);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool ValidateDuAnByDonVi(NhKtKhoiTaoCapPhatImportModel item)
        {
            NhDaDuAn itemSelected = ListDuAnByDonVi.Where(n => n.SMaDuAn == item.SMaDuAn).FirstOrDefault();
            if (itemSelected != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateHopDongByDonVi(NhKtKhoiTaoCapPhatImportModel item)
        {
            NhDaHopDong itemSelected = ListHopDongByDonVi.Where(n => n.SSoHopDong == item.SMaHopDong).FirstOrDefault();
            if (itemSelected != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Validate()
        {
            if (SelectedNamKhoiTao == null || string.IsNullOrEmpty(SelectedNamKhoiTao.ValueItem))
            {
                System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgInputRequire, "Năm khởi tạo"), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (NgayKhoiTao == null)
            {
                System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgInputRequire, "Ngày khởi tạo"), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (SelectedDonVi == null || string.IsNullOrEmpty(SelectedDonVi.ValueItem))
            {
                System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgInputRequire, "Đơn vị quản lý"), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        public override void OnSave(object obj)
        {
            try
            {
                if (Items == null || Items.Count() == 0)
                {
                    return;
                }
                //Insert phần cấp phát
                NhKtKhoiTaoCapPhat entityKhoiTao = new NhKtKhoiTaoCapPhat();
                entityKhoiTao.IIdDonViID = Guid.Parse(SelectedDonVi.HiddenValue);
                entityKhoiTao.IIdMaDonVi = SelectedDonVi.ValueItem;
                entityKhoiTao.INamKhoiTao = Convert.ToInt32(SelectedNamKhoiTao.DisplayItem);
                entityKhoiTao.DNgayKhoiTao = NgayKhoiTao.Value;
                entityKhoiTao.BIsKhoa = false;
                entityKhoiTao.BIsXoa = false;
                entityKhoiTao.DNgayTao = DateTime.Now;
                entityKhoiTao.SNguoiTao = _sessionInfo.Principal;
                _nhKtKhoiTaoCapPhatService.Add(entityKhoiTao);

                //Insert phần cấp phát chi tiết
                IEnumerable<NhKtKhoiTaoCapPhatChiTietModel> listDataChiTiet = Items.ToList();
                foreach (var item in listDataChiTiet)
                {
                    item.IIdKhoiTaoCapPhatID = entityKhoiTao.Id;
                    item.IsAdded = true;
                }
                _nhKtKhoiTaoCapPhatChiTietService.AddOrUpdate(_mapper.Map<IEnumerable<NhKtKhoiTaoCapPhatChiTiet>>(listDataChiTiet));

                //Insert phần tổng hợp
                _nhThTongHopService.InsertNHTongHop_New("KHOI_TAO", 1, entityKhoiTao.Id);
                //_nhThTongHopService.InsertNHTongHop_Giam("KHOI_TAO", 1, entityKhoiTao.Id);

                //NhKtKhoiTaoCapPhat modelChiTiet = entityKhoiTao;
                //DataChangedEventHandler handler = OpenDetail;
                //if (handler != null)
                //{
                //    handler(modelChiTiet, new EventArgs());
                //}

                System.Windows.MessageBox.Show(Resources.MsgSaveDone, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                SavedAction?.Invoke(entityKhoiTao);
                //Đóng popup
                if (obj is Window window)
                {
                    window.Close();
                }
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
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = string.Format("Chọn file excel");
                openFileDialog.RestoreDirectory = true;
                openFileDialog.DefaultExt = ".xlsx";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                FilePath = openFileDialog.FileName;
                FileName = openFileDialog.SafeFileName;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnResetConditon()
        {
            FilePath = string.Empty;
            FileName = string.Empty;
            NgayKhoiTao = null;
            _importErrors = new List<ImportErrorItem>();
            Items = new ObservableCollection<NhKtKhoiTaoCapPhatChiTietModel>();
            if (ItemsNamKhoiTao != null && ItemsNamKhoiTao.Count > 0)
            {
                SelectedNamKhoiTao = ItemsNamKhoiTao.FirstOrDefault();
            }
            if (ItemsDonVi != null && ItemsDonVi.Count > 0)
            {
                SelectedDonVi = ItemsDonVi.FirstOrDefault();
            }
            OnPropertyChanged(nameof(ItemsNamKhoiTao));
            OnPropertyChanged(nameof(ItemsDonVi));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(NgayKhoiTao));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        public override void Init()
        {
            try
            {
                _sessionInfo = _sessionService.Current;
                OnResetConditon();
                LoadNamKhoiTao();
                LoadDonVi();
                NgayKhoiTao = DateTime.Now;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDonVi()
        {
            List<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();
            ItemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi);
            ItemsDonVi.Insert(0, new ComboboxItem { ValueItem = string.Empty, DisplayItem = "-- Chọn đơn vị --" });
            if (ItemsDonVi != null && ItemsDonVi.Count > 0)
            {
                SelectedDonVi = ItemsDonVi.FirstOrDefault();
            }
        }

        private void LoadNamKhoiTao()
        {
            ItemsNamKhoiTao = new ObservableCollection<ComboboxItem>();
            int year = DateTime.Now.Year;
            ItemsNamKhoiTao.Insert(0, new ComboboxItem { ValueItem = string.Empty, DisplayItem = "-- Chọn năm khởi tạo --" });
            for (int i = 0; i <= 10; i++)
            {
                ItemsNamKhoiTao.Add(new ComboboxItem() {ValueItem = year.ToString(), DisplayItem = year.ToString() });
                year--;
            }
            if (ItemsNamKhoiTao != null && ItemsNamKhoiTao.Count > 0)
            {
                SelectedNamKhoiTao = ItemsNamKhoiTao.FirstOrDefault();
            }
        }

        private List<NhKtKhoiTaoCapPhatChiTietModel> ConvertData(List<NhKtKhoiTaoCapPhatImportModel> importModels)
        {
            List<NhKtKhoiTaoCapPhatChiTietModel> results = new List<NhKtKhoiTaoCapPhatChiTietModel>();
            NhKtKhoiTaoCapPhatChiTietModel ktcp;
            int i = 0;
            foreach (var import in importModels)
            {
                ktcp = new NhKtKhoiTaoCapPhatChiTietModel();

                ktcp.SMaDuAn = import.SMaDuAn;
                if (!string.IsNullOrEmpty(import.SMaDuAn))
                {
                    var duAn = ListDuAnByDonVi.FirstOrDefault(x => x.SMaDuAn.ToUpper().Equals(import.SMaDuAn?.ToUpper()));
                    if (duAn != null)
                    {
                        ktcp.IIdDuAnID = duAn.Id;
                        ktcp.STenDuAn = duAn.STenDuAn;
                    }
                    else
                    {
                        ktcp.ImportStatus = false;
                        _importErrors.Add(new ImportErrorItem { Row = i, Error = Resources.MsgNotExistData, ColumnName = "Dự án" });
                    }
                }
                ktcp.SMaHopDong = import.SMaHopDong;
                if (!string.IsNullOrEmpty(import.SMaHopDong))
                {
                    var hopDong = ListHopDongByDonVi.FirstOrDefault(x => x.SSoHopDong.ToUpper().Equals(import.SMaHopDong?.ToUpper()));
                    if (hopDong != null)
                    {
                        ktcp.IIdHopDongID = hopDong.Id;
                        ktcp.STenHopDong = hopDong.STenHopDong;
                    }
                    else
                    {
                        ktcp.ImportStatus = false;
                        _importErrors.Add(new ImportErrorItem { Row = i, Error = Resources.MsgNotExistData, ColumnName = "Hợp đồng" });
                    }
                }
                ktcp.FDeNghiQTNamNayUSD = Convert.ToDouble(import.FDeNghiQTNamNayUSD);
                ktcp.FDeNghiQTNamNayVND = Convert.ToDouble(import.FDeNghiQTNamNayVND);
                ktcp.FQTKinhPhiDuyetCacNamTruocUSD = Convert.ToDouble(import.FQTKinhPhiDuyetCacNamTruocUSD);
                ktcp.FQTKinhPhiDuyetCacNamTruocVND = Convert.ToDouble(import.FQTKinhPhiDuyetCacNamTruocVND);
                ktcp.FLuyKeKinhPhiDuocCapUSD = Convert.ToDouble(import.FLuyKeKinhPhiDuocCapUSD);
                ktcp.FLuyKeKinhPhiDuocCapVND = Convert.ToDouble(import.FLuyKeKinhPhiDuocCapVND);
                ktcp.ImportStatus = import.ImportStatus;
                results.Add(ktcp);
                i++;
            }
            return results;
        }

        private bool CheckUnique()
        {
            //Check trùng đơn vị + năm
            var tempList = _nhKtKhoiTaoCapPhatService.FindAll();
            bool myCheck = true;
            foreach (var item in tempList)
            {
                if (item.IIdDonViID == SelectedDonVi.Id && item.INamKhoiTao == Convert.ToInt32(SelectedNamKhoiTao.ValueItem))
                {
                    myCheck = false;
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckUniqueYearAndUnit, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }
            return myCheck;
        }
    }
}
