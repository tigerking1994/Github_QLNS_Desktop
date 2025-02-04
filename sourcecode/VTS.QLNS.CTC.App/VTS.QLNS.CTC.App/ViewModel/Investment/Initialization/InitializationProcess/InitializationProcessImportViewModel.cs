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
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.RequestSettlement;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.Initialization.InitializationProcess
{
    public class InitializationProcessImportViewModel : GridViewModelBase<KhoiTaoImportModel>
    {
        private readonly INsDonViService _nsDonViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IVdtDaDuAnService _vdtDaDuAnService;
        private readonly IVdtKtKhoiTaoDuLieuService _vdtKtKhoiTaoService;
        private readonly IVdtKtKhoiTaoDuLieuChiTietService _chungTuChiTietService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly IVdtDmLoaiCongTrinhService _loaicongtrinhService;
        private readonly ILog _logger;
        private IImportExcelService _importService;
        private Dictionary<string, Guid> _dicLoaiCongTrinh;

        public override string Name => "Import thông tin khởi tạo dự án";
        public override string Title => "Import thông tin khởi tạo dự án";
        public override string Description => "Import thông tin khởi tạo dự án";
        public override Type ContentType => typeof(View.Investment.Initialization.InitializationProcess.InitializationProcessImport);
        public override PackIconKind IconKind => PackIconKind.Projector;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler OpenDetail;

        public List<VdtDaDuAn> ListDuAn;
        public List<VdtDaDuAn> ListDuAnByDonVi;

        private ObservableCollection<ComboboxItem> _itemsCoQuanTaiChinh;
        public ObservableCollection<ComboboxItem> ItemsCoQuanTaiChinh
        {
            get => _itemsCoQuanTaiChinh;
            set => SetProperty(ref _itemsCoQuanTaiChinh, value);
        }

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

        private ObservableCollection<ComboboxItem> _dataDonVi;
        public ObservableCollection<ComboboxItem> DataDonVi
        {
            get => _dataDonVi;
            set => SetProperty(ref _dataDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private int? _namKhoiTao;
        public int? NamKhoiTao
        {
            get => _namKhoiTao;
            set => SetProperty(ref _namKhoiTao, value);
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
                    return !Items.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ResetDataCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand SelectionDoubleClickCommand { get; }
        public RelayCommand ShowErrorCommand { get; }

        public InitializationProcessImportViewModel(INsDonViService nsDonViService,
            ISessionService sessionService,
            IVdtDaDuAnService vdtDaDuAnService,
            IImportExcelService importService,
            IVdtKtKhoiTaoDuLieuService vdtKtKhoiTaoService,
            IVdtKtKhoiTaoDuLieuChiTietService chungTuChiTietService,
            ITongHopNguonNSDauTuService tonghopService,
            IVdtDmLoaiCongTrinhService loaicongtrinhService,
            ILog logger,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _vdtDaDuAnService = vdtDaDuAnService;
            _importService = importService;
            _vdtKtKhoiTaoService = vdtKtKhoiTaoService;
            _chungTuChiTietService = chungTuChiTietService;
            _tonghopService = tonghopService;
            _loaicongtrinhService = loaicongtrinhService;

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
                    return;
                }

                if (!Validate())
                {
                    return;
                }

                _importErrors = new List<ImportErrorItem>();
                ListDuAn = _vdtDaDuAnService.FindAll().ToList();
                ListDuAnByDonVi = _vdtDaDuAnService.FindByIdDonViQuanLy(SelectedDonVi.ValueItem).ToList();
                _importService.SetLastRowToRead(0);
                ImportResult<KhoiTaoImportModel> _chungTuResult = _importService.ProcessData<KhoiTaoImportModel>(FilePath);

                //validate duan....
                foreach (KhoiTaoImportModel item in _chungTuResult.Data)
                {
                    int index = _chungTuResult.Data.IndexOf(item);
                    if (!ValidateDuAn(item))
                    {
                        item.ImportStatus = false;
                        if (_importErrors.Where(n => n.Row == index && n.Error == Resources.MsgImportErrorProjectCode).ToList().Count == 0)
                            _importErrors.Add(new ImportErrorItem { Row = index, Error = Resources.MsgImportErrorProjectCode });
                    }
                    else if (!ValidateDuAnByDonVi(item))
                    {
                        item.ImportStatus = false;
                        if (_importErrors.Where(n => n.Row == index && n.Error == Resources.MsgImportErrorProjectCodeAgency).ToList().Count == 0)
                            _importErrors.Add(new ImportErrorItem { Row = index, Error = Resources.MsgImportErrorProjectCodeAgency });
                    }
                    if (string.IsNullOrEmpty(item.SMaLoaiCongTrinh))
                    {
                        if (_importErrors.Where(n => n.Row == index && n.Error == string.Format(Resources.MsgErrorDataEmpty, "loại công trình")).ToList().Count == 0)
                            _importErrors.Add(new ImportErrorItem { Row = index, Error = string.Format(Resources.MsgErrorDataEmpty, "loại công trình") });
                    }
                    else if (!_dicLoaiCongTrinh.ContainsKey(item.SMaLoaiCongTrinh))
                    {
                        if (_importErrors.Where(n => n.Row == index && n.Error == string.Format(Resources.MsgErrorItemNotFound, "loại công trình")).ToList().Count == 0)
                            _importErrors.Add(new ImportErrorItem { Row = index, Error = string.Format(Resources.MsgErrorItemNotFound, "loại công trình") });
                    }
                }
                Items = new ObservableCollection<KhoiTaoImportModel>(_chungTuResult.Data);

                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));

                if (Items.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ShowError()
        {
            int rowIndex = Items.IndexOf(SelectedItem);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool ValidateDuAn(KhoiTaoImportModel item)
        {
            VdtDaDuAn itemSelected = ListDuAn.Where(n => n.SMaDuAn == item.MaDuAn).FirstOrDefault();
            if (itemSelected != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateDuAnByDonVi(KhoiTaoImportModel item)
        {
            VdtDaDuAn itemSelected = ListDuAnByDonVi.Where(n => n.SMaDuAn == item.MaDuAn).FirstOrDefault();
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
            if (SelectedDonVi == null || string.IsNullOrEmpty(SelectedDonVi.ValueItem))
            {
                System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgInputRequire, "Đơn vị quản lý"), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (NamKhoiTao == null)
            {
                System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgInputRequire, "Năm khởi tạo"), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (NgayKhoiTao == null)
            {
                System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgInputRequire, "Ngày khởi tạo"), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        public override void OnSave()
        {
            try
            {
                if (!Validate())
                {
                    return;
                }

                OnProcessFile();
                if (Items == null || Items.Count() == 0)
                {
                    return;
                }

                VdtKtKhoiTaoDuLieu entityKhoiTao = new VdtKtKhoiTaoDuLieu();

                DonVi donVi = _nsDonViService.FindByIdDonVi(SelectedDonVi.ValueItem, _sessionService.Current.YearOfWork);

                entityKhoiTao.INamKhoiTao = NamKhoiTao.Value;
                entityKhoiTao.DNgayKhoiTao = NgayKhoiTao.Value;
                entityKhoiTao.DDateCreate = DateTime.Now;
                entityKhoiTao.IIdDonViId = donVi.Id;
                entityKhoiTao.IIdMaDonVi = donVi.IIDMaDonVi;
                entityKhoiTao.SUserCreate = _sessionService.Current.Principal;
                _vdtKtKhoiTaoService.Add(entityKhoiTao);

                List<VdtKtKhoiTaoDuLieuChiTiet> listEntityChiTiet = new List<VdtKtKhoiTaoDuLieuChiTiet>();
                List<KhoiTaoImportModel> listDataChiTiet = Items.ToList();
                SetDuAnValue(ref listDataChiTiet);

                listEntityChiTiet = _mapper.Map<List<VdtKtKhoiTaoDuLieuChiTiet>>(listDataChiTiet);
                listEntityChiTiet.Select(n => { n.IIdKhoiTaoDuLieuId = entityKhoiTao.Id; return n; }).ToList();

                _chungTuChiTietService.AddRange(listEntityChiTiet);

                InitializationProcessModel modelChiTiet = _mapper.Map<VTS.QLNS.CTC.App.Model.InitializationProcessModel>(entityKhoiTao);
                System.Windows.MessageBox.Show(Resources.MsgSaveDone, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                SetupDataTongHop(entityKhoiTao.Id, listEntityChiTiet);
                DataChangedEventHandler handler = OpenDetail;
                if (handler != null)
                {
                    handler(modelChiTiet, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void SetDuAnValue(ref KhoiTaoImportModel item)
        {
            if (ListDuAn != null && ListDuAn.Count > 0)
            {
                string value = item.MaDuAn;
                VdtDaDuAn itemSelected = ListDuAn.Where(n => n.SMaDuAn == value).FirstOrDefault();
                if (itemSelected != null)
                {
                    item.DuAnId = itemSelected.Id;
                }
            }
        }

        private void SetDuAnValue(ref List<KhoiTaoImportModel> data)
        {
            foreach (KhoiTaoImportModel item in data)
            {
                KhoiTaoImportModel value = item;
                SetDuAnValue(ref value);
                if (_dicLoaiCongTrinh.ContainsKey(item.SMaLoaiCongTrinh))
                {
                    item.IIdLoaiCongTrinh = _dicLoaiCongTrinh[item.SMaLoaiCongTrinh];
                }
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
            NamKhoiTao = null;
            NgayKhoiTao = null;
            _importErrors = new List<ImportErrorItem>();
            Items = new ObservableCollection<KhoiTaoImportModel>();
            if (DataDonVi != null && DataDonVi.Count > 0)
            {
                SelectedDonVi = DataDonVi.FirstOrDefault();
            }
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(NamKhoiTao));
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
                OnResetConditon();
                LoadCombobxDonVi();
                LoadDropDownCoQuanThanhToan();
                _dicLoaiCongTrinh = new Dictionary<string, Guid>();

                var lstLoaiCongTrinh = _loaicongtrinhService.FindAll();
                if(lstLoaiCongTrinh != null)
                {
                    foreach(var item in lstLoaiCongTrinh)
                    {
                        if (!_dicLoaiCongTrinh.ContainsKey(item.SMaLoaiCongTrinh)) _dicLoaiCongTrinh.Add(item.SMaLoaiCongTrinh, item.IIdLoaiCongTrinh);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadCombobxDonVi()
        {
            List<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();
            DataDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi);
            DataDonVi.Insert(0, new ComboboxItem { ValueItem = string.Empty, DisplayItem = "-- Chọn đơn vị --" });
            if (DataDonVi != null && DataDonVi.Count > 0)
            {
                SelectedDonVi = DataDonVi.FirstOrDefault();
            }
        }

        private void LoadDropDownCoQuanThanhToan()
        {
            List<ComboboxItem> lstItem = new List<ComboboxItem>();
            lstItem.Add(new ComboboxItem() { DisplayItem = CoQuanThanhToanEnum.TypeName.KHO_BAC, ValueItem = ((int)CoQuanThanhToanEnum.Type.KHO_BAC).ToString() });
            lstItem.Add(new ComboboxItem() { DisplayItem = CoQuanThanhToanEnum.TypeName.CQTC, ValueItem = ((int)CoQuanThanhToanEnum.Type.CQTC).ToString() });
            ItemsCoQuanTaiChinh = new ObservableCollection<ComboboxItem>(lstItem);
            OnPropertyChanged(nameof(lstItem));
        }

        private void SetupDataTongHop(Guid iIdParentId, List<VdtKtKhoiTaoDuLieuChiTiet> lstItem)
        {
            _tonghopService.DeleteTongHopNguonDauTu(LOAI_CHUNG_TU.QUYET_TOAN, iIdParentId);
            List<TongHopNguonNSDauTuQuery> lstData = new List<TongHopNguonNSDauTuQuery>();
            foreach (var item in lstItem)
            {
                if ((item.FKhvnVonBoTriHetNamTruoc ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = iIdParentId,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVN_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVN_LENHCHI,
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        fGiaTri = item.FKhvnVonBoTriHetNamTruoc,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhvnLkvonDaThanhToanTuKhoiCongDenHetNamTruoc ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = iIdParentId,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                        sMaDich = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_TT_KHVN_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_TT_KHVN_LENHCHI,
                        sMaNguonCha = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVN_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVN_LENHCHI,
                        fGiaTri = item.FKhvnLkvonDaThanhToanTuKhoiCongDenHetNamTruoc,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhvnTrongDoVonTamUngTheoCheDoChuaThuHoi ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = iIdParentId,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                        sMaDich = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_TU_CHUATH_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_TU_CHUATH_LENHCHI,
                        sMaNguonCha = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVN_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVN_LENHCHI,
                        fGiaTri = item.FKhvnTrongDoVonTamUngTheoCheDoChuaThuHoi,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhvnKeHoachVonKeoDaiSangNam ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = iIdParentId,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_KHOBAC_CHUYENNAMTRUOC : LOAI_CHUNG_TU.QT_LENHCHI_CHUYENNAMTRUOC,
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        fGiaTri = item.FKhvnKeHoachVonKeoDaiSangNam,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhutVonBoTriHetNamTruoc ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = iIdParentId,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVU_LENHCHI,
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        fGiaTri = item.FKhutVonBoTriHetNamTruoc,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhutLkvonDaThanhToanTuKhoiCongDenHetNamTruoc ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = iIdParentId,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                        sMaDich = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_TT_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_TT_KHVU_LENHCHI,
                        sMaNguonCha = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVU_LENHCHI,
                        fGiaTri = item.FKhutLkvonDaThanhToanTuKhoiCongDenHetNamTruoc,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhutTrongDoVonTamUngTheoCheDoChuaThuHoi ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = iIdParentId,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                        sMaDich = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_TU_CHUATH_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_TU_CHUATH_LENHCHI,
                        sMaNguonCha = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVU_LENHCHI,
                        fGiaTri = item.FKhutTrongDoVonTamUngTheoCheDoChuaThuHoi,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhutKeHoachUngTruocKeoDaiSangNam ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = iIdParentId,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_UNG_KHOBAC_CHUYENNAMTRUOC : LOAI_CHUNG_TU.QT_UNG_LENHCHI_CHUYENNAMTRUOC,
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        fGiaTri = item.FKhutKeHoachUngTruocKeoDaiSangNam,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhutKeHoachUngTruocChuaThuHoi ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = iIdParentId,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVU_LENHCHI,
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        fGiaTri = item.FKhutKeHoachUngTruocChuaThuHoi,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }
            }
            _tonghopService.InsertTongHopNguonDauTuQuyetToan(iIdParentId, lstData);
        }
    }
}

