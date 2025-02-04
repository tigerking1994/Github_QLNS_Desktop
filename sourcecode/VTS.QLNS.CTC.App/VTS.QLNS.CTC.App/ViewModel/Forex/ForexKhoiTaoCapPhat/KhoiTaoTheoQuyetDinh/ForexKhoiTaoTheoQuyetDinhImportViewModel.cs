using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Helper;
using System.IO;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.Globalization;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexKhoiTaoCapPhat.KhoiTaoTheoQuyetDinh
{
    public class ForexKhoiTaoTheoQuyetDinhImportViewModel : GridViewModelBase<NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel>
    {
        private readonly INsDonViService _nsDonViService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly INhDaHopDongChiPhiService _nhDaHopDongChiPhiService;
        private readonly INhDaQdDauTuService _nhDaQdDauTuService;
        private readonly INhDaQuyetDinhKhacService _nhDaQuyetDinhKhacService;
        private readonly INhDaQuyetDinhKhacChiPhiService _nhDaQuyetDinhKhacChiPhiService;
        private readonly INhKtKhoiTaoCapPhatService _nhKtKhoiTaoCapPhatService;
        private readonly INhKtKhoiTaoCapPhatChiTietService _nhKtKhoiTaoCapPhatChiTietService;
        private readonly INhThTongHopService _nhThTongHopService;
        private IImportExcelService _importService;
        private readonly INhDmChiPhiService _nhDmChiPhiService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly IExportService _exportService;


        private SessionInfo _sessionInfo;

        public override string Name => "Import thông tin khởi tạo theo quyết định ";
        public override string Title => "Import thông tin khởi tạo theo quyết định";
        public override string Description => "Import thông tin khởi tạo theo quyết định";
        public override Type ContentType => typeof(View.Forex.ForexKhoiTaoCapPhat.KhoiTaoTheoQuyetDinh.ForexKhoiTaoTheoQuyetDinhImport);
        public override PackIconKind IconKind => PackIconKind.Projector;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler OpenDetail;

        public List<NhDaDuAn> ListDuAnImport;
        public List<NhDaHopDong> ListHopDongImport;
        public List<NhDaHopDongChiPhi> ListHopDongChiPhiImport = new List<NhDaHopDongChiPhi>();
        public List<NhDaQuyetDinhKhac> ListQuyetDinhChiPhiKhac;
        public List<NhDaQuyetDinhKhacChiPhi> ListQuyetDinhChiPhiKhacChiPhi = new List<NhDaQuyetDinhKhacChiPhi>();
        public List<NhDaQdDauTu> ListQdDauTu;
        public List<NhKtKhoiTaoCapPhatChiTiet> ListKtKhoiTaoChiTiet;
        public List<NhDaDuAn> ListDuAnByDonVi;
        public List<NhDaHopDong> ListHopDongByDonVi;
        public List<NhDmChiPhi> ListNhDmChiPhi;
        public List<NhDmNhiemVuChiQuery> ListNhDmNhiemVuChi;
        public List<NhKtKhoiTaoCapPhatChiTietImport> ListDataImport;

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
                    //return !Items.Any(x => !x.ImportStatus);
                    return true;
                return false;
            }
        }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ResetDataCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand SelectionDoubleClickCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand DownloadTemplateCommand { get; }


        public ForexKhoiTaoTheoQuyetDinhImportViewModel(INsDonViService nsDonViService,
            ISessionService sessionService,
            IImportExcelService importService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDaDuAnService nhDaDuAnService,
            INhDaHopDongService nhDaHopDongService,
            INhKtKhoiTaoCapPhatService nhKtKhoiTaoCapPhatService,
            INhKtKhoiTaoCapPhatChiTietService nhKtKhoiTaoCapPhatChiTietService,
            INhThTongHopService nhThTongHopService,
            ILog logger,
            IMapper mapper,
            INhDmChiPhiService nhDmChiPhiService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            INhDaQdDauTuService nhDaQdDauTuService,
            INhDaQuyetDinhKhacService nhDaQuyetDinhKhacService,
            IExportService exportService,
            INhDaHopDongChiPhiService nhDaHopDongChiPhiService,
            INhDaQuyetDinhKhacChiPhiService nhDaQuyetDinhKhacChiPhiService)
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
            _nhDmChiPhiService = nhDmChiPhiService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _nhDaQdDauTuService = nhDaQdDauTuService;
            _nhDaQuyetDinhKhacService = nhDaQuyetDinhKhacService;
            _exportService = exportService;
            _nhDaHopDongChiPhiService = nhDaHopDongChiPhiService;
            _nhDaQuyetDinhKhacChiPhiService = nhDaQuyetDinhKhacChiPhiService;
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ResetDataCommand = new RelayCommand(obj => OnResetConditon());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            DownloadTemplateCommand = new RelayCommand(obj => OnDownloadTemplate());

        }

        private void OnDownloadTemplate()
        {
            if (SelectedDonVi == null || string.IsNullOrEmpty(SelectedDonVi.ValueItem))
            {
                System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgInputRequire, "Đơn vị quản lý"), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                   var  iIdDonVi = Guid.Parse(SelectedDonVi.HiddenValue);
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNameWithoutExtension;
                    var data = new Dictionary<string, object>();
                    templateFileName = Path.Combine(ExportPrefix.PATH_NH_KTCP, ExportFileName.TEMPLATE_IMPORT_KHOITAO);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(Path.GetFileNameWithoutExtension(templateFileName));
                    //var listLoaiCongTrinh = _nhDMLoaiCongTrinh.FindAll().OrderBy(x => x.SMaLoaiCongTrinh);
                    var lstNhiemVuChi = _nhDmNhiemVuChiService.FindByDonViId(iIdDonVi).Where(x => !string.IsNullOrEmpty(x.SMaNhiemVuChi));
                    var lstChiPhi = _nhDmChiPhiService.FindAll().OrderBy(x => x.SMaChiPhi).ToList();
                    var dataChiPhi = _mapper.Map<List<NhDmChiPhiModel>>(lstChiPhi);
                    dataChiPhi = dataChiPhi.OrderBy(o => o.SMaChiPhi).ToList();
                    foreach (var item in dataChiPhi.Select((value, index) => new { index, value }))
                    {
                        item.value.STT = (item.index + 1).ToString();
                    }
                    data.Add("ItemsNhiemVuChi", lstNhiemVuChi);
                    data.Add("ItemsChiPhi", dataChiPhi);

                    var xlsFile = _exportService.Export<NhDmChiPhiModel, NhDmNhiemVuChiQuery>(templateFileName, data);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.EXCEL);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }

                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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
                _importService.SetLastRowToRead(0);
                ImportResult<NhKtKhoiTaoCapPhatChiTietImport, NhDaDuAnImport, NhDaHopDongImport, NhDaQuyetDinhKhacImport, NhDmNhiemVuChiImportKhoiTao> _chungTuResult = _importService.ProcessData<NhKtKhoiTaoCapPhatChiTietImport, NhDaDuAnImport, NhDaHopDongImport, NhDaQuyetDinhKhacImport, NhDmNhiemVuChiImportKhoiTao>(FilePath);
                ConvertData(_chungTuResult);
                //Items = _mapper.Map<ObservableCollection<NhKtKhoiTaoCapPhatChiTietModel>>(ConvertData(_chungTuResult.Data1));

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
                var iIdDonVi = Guid.Empty;
                var iIdMaDonVi = string.Empty;
                if (SelectedDonVi != null)
                {
                    iIdDonVi = Guid.Parse(SelectedDonVi.HiddenValue);
                    iIdMaDonVi = SelectedDonVi.ValueItem;
                }
                //Insert Du An
                ListDuAnImport = ListDuAnImport.Where(x => x.Id != Guid.Empty && !string.IsNullOrEmpty(x.STenDuAn) && Items.Where(w => !w.IIdDuAnID.IsNullOrEmpty()).Select(s => s.IIdDuAnID).Contains(x.Id)).ToList();
                ListDuAnImport.ForEach(x =>
                {
                    x.DNgayTao = DateTime.Now;
                    x.SNguoiTao = _sessionInfo.Principal;
                    x.IIdDonViQuanLyId = iIdDonVi;
                    x.IIdMaDonViQuanLy = iIdMaDonVi;
                    x.IIdKhttNhiemVuChiId = Items.Any(y => y.IIdDuAnID.Equals(x.Id) && y.ILoaiNoiDung == NHConstants.ILOAI_DU_AN) ? Items.FirstOrDefault(y => y.IIdDuAnID.Equals(x.Id) && y.ILoaiNoiDung == NHConstants.ILOAI_DU_AN).IIdKhttNhiemVuChiID : x.IIdKhttNhiemVuChiId;
                });
                _nhDaDuAnService.AddRange(ListDuAnImport);
                //Insert QDDauTu
                ListQdDauTu = ListQdDauTu.Where(x => !x.IIdDuAnId.IsNullOrEmpty() && !string.IsNullOrEmpty(x.SSoQuyetDinh) && ListDuAnImport.Select(s => s.Id).Contains(x.IIdDuAnId ?? Guid.Empty)).ToList();
                ListQdDauTu.ForEach(x =>
                {
                    x.BIsActive = true;
                    x.BIsGoc = true;
                    x.BIsKhoa = false;
                    x.BIsXoa = false;
                    x.DNgayTao = DateTime.Now;
                    x.SNguoiTao = _sessionInfo.Principal;
                    x.IIdDonViQuanLyId = iIdDonVi;
                    x.IIdMaDonViQuanLy = iIdMaDonVi;
                    x.IIdKhttNhiemVuChiId = Items.Any(y => y.IIdDuAnID.Equals(x.IIdDuAnId) && y.ILoaiNoiDung == NHConstants.ILOAI_DU_AN) ? Items.FirstOrDefault(y => y.IIdDuAnID.Equals(x.IIdDuAnId) && y.ILoaiNoiDung == NHConstants.ILOAI_DU_AN).IIdKhttNhiemVuChiID : Guid.Empty;
                });
                _nhDaQdDauTuService.AddRange(ListQdDauTu);

                //Insert Hop DOng
                ListHopDongImport = ListHopDongImport.Where(x => !string.IsNullOrEmpty(x.SSoHopDong) && !x.Id.IsNullOrEmpty() && Items.Where(w => !w.IIdHopDongID.IsNullOrEmpty()).Select(s => s.IIdHopDongID).Contains(x.Id)).ToList();
                ListHopDongImport.ForEach(x =>
                {
                    x.ILoai = x.ILanDieuChinh;
                    x.BIsActive = true;
                    x.BIsGoc = true;
                    x.BIsKhoa = false;
                    x.BIsXoa = false;
                    x.DNgayTao = DateTime.Now;
                    x.SNguoiTao = _sessionInfo.Principal;
                    x.IIdDonViQuanLyId = iIdDonVi;
                    x.IIdMaDonViQuanLy = iIdMaDonVi;
                    x.IIdKhTongTheNhiemVuChiId = Items.Any(y => y.IIdHopDongID.Equals(x.Id) && y.ILoaiNoiDung == NHConstants.ILOAI_HOP_DONG) ? Items.FirstOrDefault(y => y.IIdHopDongID.Equals(x.Id) && y.ILoaiNoiDung == NHConstants.ILOAI_HOP_DONG).IIdKhttNhiemVuChiID : Guid.Empty;
                    x.IIdDuAnId = Items.Any(y => y.IIdHopDongID.Equals(x.Id) && y.ILoaiNoiDung == NHConstants.ILOAI_HOP_DONG) ? Items.FirstOrDefault(y => y.IIdHopDongID.Equals(x.Id) && y.ILoaiNoiDung == NHConstants.ILOAI_HOP_DONG).IIdDuAnID : Guid.Empty;
                });
                ListHopDongImport.ForAll(x =>
                {
                    x.ILanDieuChinh = 0;
                });
                _nhDaHopDongService.AddRange(ListHopDongImport);
                //Insert QDKhac
                ListQuyetDinhChiPhiKhac = ListQuyetDinhChiPhiKhac.Where(x => !x.Id.IsNullOrEmpty() && !string.IsNullOrEmpty(x.SSoQuyetDinh) && Items.Where(w => w.ILoaiNoiDung.Equals(NHConstants.ILOAI_QD_KHAC)).Select(y => y.IIdQuyetDinhKhacID).Contains(x.Id)).ToList();
                ListQuyetDinhChiPhiKhac.ForEach(x =>
                {
                    x.BIsActive = true;
                    x.BIsGoc = true;
                    x.BIsKhoa = false;
                    x.BIsXoa = false;
                    x.DNgayTao = DateTime.Now;
                    x.SNguoiTao = _sessionInfo.Principal;
                    x.IIdDonViQuanLyId = iIdDonVi;
                    x.IIdMaDonViQuanLy = iIdMaDonVi;
                    x.IIdKHTTNhiemVuChiId = Items.Any(y => y.IIdQuyetDinhKhacID.Equals(x.Id) && y.ILoaiNoiDung == NHConstants.ILOAI_QD_KHAC) ? Items.FirstOrDefault(y => y.IIdQuyetDinhKhacID.Equals(x.Id) && y.ILoaiNoiDung == NHConstants.ILOAI_QD_KHAC).IIdKhttNhiemVuChiID : Guid.Empty;
                });
                _nhDaQuyetDinhKhacService.AddRange(ListQuyetDinhChiPhiKhac);
                //Insert chi phi chi tiet
                if (Items.Any(x => !x.IIdHopDongID.IsNullOrEmpty() && x.ILoaiNoiDung == NHConstants.ILOAI_CHI_PHI))
                {
                    ListHopDongChiPhiImport = new List<NhDaHopDongChiPhi>();
                    ListHopDongChiPhiImport.AddRange(_mapper.Map<List<NhDaHopDongChiPhi>>(Items.Where((x => !x.IIdHopDongID.IsNullOrEmpty() && x.ILoaiNoiDung == NHConstants.ILOAI_CHI_PHI))));
                    foreach (var item in ListHopDongChiPhiImport.Select((value, index) => (value, index)))
                    {
                        item.value.Id = Guid.NewGuid();
                        item.value.SMaOrder = (item.index + 1).ToString();
                        item.value.IIdParentId = Items.Any(y => y.Id.Equals(item.value.IIdParentId) && y.ILoaiNoiDung == NHConstants.ILOAI_HOP_DONG) ? null : item.value.IIdParentId;
                    }
                    _nhDaHopDongChiPhiService.AddRange(ListHopDongChiPhiImport);
                }

                if (Items.Any(x => !x.IIdQuyetDinhKhacID.IsNullOrEmpty() && x.ILoaiNoiDung == NHConstants.ILOAI_CHI_PHI))
                {
                    ListQuyetDinhChiPhiKhacChiPhi = new List<NhDaQuyetDinhKhacChiPhi>();
                    ListQuyetDinhChiPhiKhacChiPhi.AddRange(_mapper.Map<List<NhDaQuyetDinhKhacChiPhi>>(Items.Where((x => !x.IIdQuyetDinhKhacID.IsNullOrEmpty() && x.ILoaiNoiDung == NHConstants.ILOAI_CHI_PHI))));
                    foreach (var item in ListQuyetDinhChiPhiKhacChiPhi.Select((value, index) => (value, index)))
                    {
                        item.value.Id = Guid.NewGuid();
                        item.value.SMaOrder = (item.index + 1).ToString();
                        item.value.IIdParentId = Items.Any(y => y.Id.Equals(item.value.IIdParentId) && y.ILoaiNoiDung == NHConstants.ILOAI_QD_KHAC) ? null : item.value.IIdParentId;
                    }
                    _nhDaQuyetDinhKhacChiPhiService.AddRange(ListQuyetDinhChiPhiKhacChiPhi);

                }
                //Insert phần cấp phát
                NhKtKhoiTaoCapPhat entityKhoiTao = new NhKtKhoiTaoCapPhat
                {
                    IIdDonViID = Guid.Parse(SelectedDonVi.HiddenValue),
                    IIdMaDonVi = SelectedDonVi.ValueItem,
                    INamKhoiTao = Convert.ToInt32(SelectedNamKhoiTao.DisplayItem),
                    DNgayKhoiTao = NgayKhoiTao.Value,
                    BIsKhoa = false,
                    BIsXoa = false,
                    DNgayTao = DateTime.Now,
                    SNguoiTao = _sessionInfo.Principal
                };
                _nhKtKhoiTaoCapPhatService.Add(entityKhoiTao);

                //Insert phần cấp phát chi tiết
                IEnumerable<NhKtKhoiTaoCapPhatChiTiet> listDataChiTiet = _mapper.Map<IEnumerable<NhKtKhoiTaoCapPhatChiTiet>>(Items.ToList());
                foreach (var item in listDataChiTiet)
                {
                    item.IIdKhoiTaoCapPhatID = entityKhoiTao.Id;
                    item.IsAdded = true;
                }
                _nhKtKhoiTaoCapPhatChiTietService.AddOrUpdate(_mapper.Map<IEnumerable<NhKtKhoiTaoCapPhatChiTiet>>(listDataChiTiet));

                //Insert phần tổng hợp
                //_nhThTongHopService.InsertNHTongHop_Tang("KHOI_TAO", 1, entityKhoiTao.Id);
                //_nhThTongHopService.InsertNHTongHop_Giam("KHOI_TAO", 1, entityKhoiTao.Id);
                _nhThTongHopService.InsertNHTongHop_New(NHConstants.KHOI_TAO, (int)TypeExecute.Insert, entityKhoiTao.Id);

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
            //Items = new ObservableCollection<NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel>();
            Items = new ObservableCollection<NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel>();

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
                ListNhDmChiPhi = _nhDmChiPhiService.FindAll().ToList();
                ListNhDmNhiemVuChi = _nhDmNhiemVuChiService.FindByDonViId(Guid.Empty).ToList();
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
                ItemsNamKhoiTao.Add(new ComboboxItem() { ValueItem = year.ToString(), DisplayItem = year.ToString() });
                year--;
            }
            if (ItemsNamKhoiTao != null && ItemsNamKhoiTao.Count > 0)
            {
                SelectedNamKhoiTao = ItemsNamKhoiTao.FirstOrDefault();
            }
        }

        private void ConvertData(ImportResult<NhKtKhoiTaoCapPhatChiTietImport, NhDaDuAnImport, NhDaHopDongImport, NhDaQuyetDinhKhacImport, NhDmNhiemVuChiImportKhoiTao> dataImports)
        {
            List<NhKtKhoiTaoCapPhatChiTietModel> results = new List<NhKtKhoiTaoCapPhatChiTietModel>();
            //NhKtKhoiTaoCapPhatChiTietModel ktcp;
            if (dataImports != null)
            {

                ListNhDmNhiemVuChi = _nhDmNhiemVuChiService.FindByDonViId(Guid.Empty).ToList();
                ListDataImport = dataImports.Data1;
                ListQdDauTu = _mapper.Map<List<NhDaQdDauTu>>(dataImports.Data2);
                ListDuAnImport = _mapper.Map<List<NhDaDuAn>>(dataImports.Data2);
                ListHopDongImport = _mapper.Map<List<NhDaHopDong>>(dataImports.Data3);
                ListQuyetDinhChiPhiKhac = _mapper.Map<List<NhDaQuyetDinhKhac>>(dataImports.Data4);
                ListNhDmNhiemVuChi = ListNhDmNhiemVuChi.Where(x => dataImports.Data5.Select(y => y.MaNhiemVuChi).Contains(x.SMaNhiemVuChi)).ToList();
                CheckDataImportAndProcess();
                Items = new ObservableCollection<NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel>(_mapper.Map<List<NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel>>(ListDataImport));
                Items.ForAll(x =>
                {
                    if (x.ILoaiNoiDung == NHConstants.ILOAI_CHUONG_TRINH)
                    {
                        x.BHangCha = true;
                    }
                    else if (x.ILoaiNoiDung != NHConstants.ZERO)
                    {
                        x.BHangCha = Items.Any(y => y.IIdParentID == x.Id);
                    }
                    else
                    {
                        x.ImportStatus = false;
                        _importErrors.Add(new ImportErrorItem { Row = Items.IndexOf(x), Error = Resources.MsgNotExistData, ColumnName = NhTongHopConstants.SLOAI_NOI_DUNG });
                    }
                });
            }
        }

        private void CheckDataImportAndProcess()
        {
            foreach (NhKtKhoiTaoCapPhatChiTietImport item in ListDataImport)
            {
                //int index = Items.IndexOf(item);
                item.ImportStatus = true;
                ValidateImport(item);
            }
        }

        private void ValidateImport(NhKtKhoiTaoCapPhatChiTietImport item)
        {
            switch (item.ILoai)
            {
                case NHConstants.ILOAI_CHUONG_TRINH:
                    ValidateNhiemVuChi(item);
                    break;
                case NHConstants.ILOAI_DU_AN:
                    ValidateNhiemVuChi(item);
                    ValidateQdDauTu(item);
                    break;
                case NHConstants.ILOAI_HOP_DONG:
                    ValidateNhiemVuChi(item);
                    ValidateQdDauTu(item);
                    ValidateHopDong(item);
                    ValidateChiPhi(item);

                    break;
                case NHConstants.ILOAI_QD_KHAC:
                    ValidateNhiemVuChi(item);
                    ValidateQDKhac(item);
                    ValidateChiPhi(item);
                    break;
                case NHConstants.ILOAI_CHI_PHI:
                    ValidateQdDauTu(item);
                    ValidateHopDong(item);
                    ValidateQDKhac(item);
                    ValidateChiPhi(item);
                    break;
            }
        }

        private bool ValidateNhiemVuChi(NhKtKhoiTaoCapPhatChiTietImport item)
        { 
            if (ListNhDmNhiemVuChi.Any(x => x.SMaNhiemVuChi.Trim().Equals(item.SMaNhiemVuChi.Trim())))
            {
                if (item.ILoai == NHConstants.ILOAI_CHUONG_TRINH)
                {
                    var nhiemVuChi = ListNhDmNhiemVuChi.FirstOrDefault(x => x.SMaNhiemVuChi.Trim().Equals(item.SMaNhiemVuChi.Trim()));
                    item.IIdKhttNhiemVuChiID = nhiemVuChi.IIdKHTTNhiemVuChiId;
                    item.IIdParentID = nhiemVuChi.IIdParentId;
                    return true;

                }
                else
                {
                    var nhiemVuChi = ListNhDmNhiemVuChi.FirstOrDefault(x => x.SMaNhiemVuChi.Trim().Equals(item.SMaNhiemVuChi.Trim()));
                    item.IIdKhttNhiemVuChiID = nhiemVuChi.IIdKHTTNhiemVuChiId;
                    var IsCheck = ListDataImport.Any(x => x.IIdKhttNhiemVuChiID.Equals(nhiemVuChi.IIdKHTTNhiemVuChiId) && x.ILoai == NHConstants.ILOAI_CHUONG_TRINH);
                    if (IsCheck)
                    {
                        if(item.ILoai == NHConstants.ILOAI_DU_AN)
                        {
                            if (ListDuAnImport.Any(x => x.Id.Equals(item.IIdDuAnID)))
                            {
                                ListDuAnImport.ForEach(x =>
                                {
                                    if (x.Id.Equals(item.IIdDuAnID))
                                    {
                                        x.IIdKhttNhiemVuChiId = nhiemVuChi.IIdKHTTNhiemVuChiId;
                                    }

                                });

                            }
                        }
                        else if (item.ILoai == NHConstants.ILOAI_HOP_DONG)
                        {
                            if (ListHopDongImport.Any(x => x.Id.Equals(item.IIdHopDongID)))
                            {
                                ListHopDongImport.ForEach(x =>
                                {
                                    if (x.Id.Equals(item.IIdHopDongID))
                                    {
                                        x.IIdKhTongTheNhiemVuChiId = nhiemVuChi.IIdKHTTNhiemVuChiId;
                                    }

                                });
                            }
                        }
                        else
                        {
                            if (ListQuyetDinhChiPhiKhac.Any(x => x.Id.Equals(item.IIdQuyetDinhKhacID)))
                            {

                                ListQuyetDinhChiPhiKhac.ForEach(x =>
                                {
                                    if (x.Id.Equals(item.IIdQuyetDinhKhacID))
                                    {
                                        x.IIdKHTTNhiemVuChiId = nhiemVuChi.IIdKHTTNhiemVuChiId;
                                    }

                                });
                            }
                        }

                        item.IIdParentID = ListDataImport.FirstOrDefault(x => x.IIdKhttNhiemVuChiID.Equals(nhiemVuChi.IIdKHTTNhiemVuChiId) && x.ILoai == NHConstants.ILOAI_CHUONG_TRINH).Id;
                        return true;

                    }
                    else
                    {
                        if (item.ILoai == NHConstants.ILOAI_DU_AN)
                        {
                            _importErrors.Add(new ImportErrorItem { Row = ListDataImport.IndexOf(item), Error = Resources.MsgNotExistData, ColumnName = NhTongHopConstants.SLOAI_DU_AN });

                        }
                        else if(item.ILoai == NHConstants.ILOAI_HOP_DONG)
                        {
                            _importErrors.Add(new ImportErrorItem { Row = ListDataImport.IndexOf(item), Error = Resources.MsgNotExistData, ColumnName = NhTongHopConstants.SLOAI_HOP_DONG });
                        }
                        else
                        {
                            _importErrors.Add(new ImportErrorItem { Row = ListDataImport.IndexOf(item), Error = Resources.MsgNotExistData, ColumnName = NhTongHopConstants.SLOAI_QD_KHAC });
                        }
                        item.ImportStatus = false;
                        return false;
                    }

                }

            }
            else
            {
                if (string.IsNullOrEmpty(item.SMaNhiemVuChi))
                    return true;
                _importErrors.Add(new ImportErrorItem { Row = ListDataImport.IndexOf(item), Error = Resources.MsgNotExistData, ColumnName = NhTongHopConstants.SLOAI_CHUONG_TRINH });
                return false;
            }
        }

        private bool ValidateQdDauTu(NhKtKhoiTaoCapPhatChiTietImport item)
        {
            if (ListQdDauTu.Any(x => x.SSoQuyetDinh.Equals(item.SSoQuyetDinhDauTu) && !string.IsNullOrEmpty(x.SSoQuyetDinh)))
            {
                if (item.ILoai == NHConstants.ILOAI_HOP_DONG || item.ILoai == NHConstants.ILOAI_CHI_PHI)
                {
                    var qdDauTu = ListQdDauTu.FirstOrDefault(x => x.SSoQuyetDinh.Equals(item.SSoQuyetDinhDauTu));
                    var duAnKT = ListDataImport.FirstOrDefault(x => x.IIdDuAnID.Equals(qdDauTu.IIdDuAnId) && x.ILoai == NHConstants.ILOAI_DU_AN);
                    if (duAnKT != null)
                    {
                        if (item.ILoai == NHConstants.ILOAI_DU_AN)
                        {
                            if (ListHopDongImport.Any(x => x.Id.Equals(item.IIdHopDongID)))
                            {
                                ListHopDongImport.ForEach(y =>
                                {
                                    if (y.Id.Equals(item.IIdHopDongID))
                                    {
                                        y.IIdKhTongTheNhiemVuChiId = duAnKT.IIdKhttNhiemVuChiID;
                                        y.IIdDuAnId = duAnKT.IIdDuAnID;
                                    }

                                });
                            }
                        }
                        item.IIdKhttNhiemVuChiID = duAnKT.IIdKhttNhiemVuChiID;
                        item.IIdDuAnID = qdDauTu.IIdDuAnId;
                        item.IIdParentID = ListDataImport.FirstOrDefault(x => x.IIdDuAnID.Equals(qdDauTu.IIdDuAnId) && x.ILoai == NHConstants.ILOAI_DU_AN).Id;
                        return true;
                    }
                    else
                    {
                        if (item.ILoai == NHConstants.ILOAI_CHI_PHI)
                        {
                            _importErrors.Add(new ImportErrorItem { Row = ListDataImport.IndexOf(item), Error = Resources.MsgNotExistData, ColumnName = NhTongHopConstants.SLOAI_CHI_PHI });

                        }
                        else if (item.ILoai == NHConstants.ILOAI_HOP_DONG)
                        {
                            _importErrors.Add(new ImportErrorItem { Row = ListDataImport.IndexOf(item), Error = Resources.MsgNotExistData, ColumnName = NhTongHopConstants.SLOAI_HOP_DONG });
                        }
                        return false;
                    }
                }
                else
                {
                    item.IIdDuAnID = ListQdDauTu.FirstOrDefault(x => x.SSoQuyetDinh.Equals(item.SSoQuyetDinhDauTu)).IIdDuAnId;
                    ListDuAnImport.ForEach(x =>
                    {
                        if (x.Id.Equals(item.IIdDuAnID))
                        {
                            x.FUsd = NumberUtils.ConvertTextToDouble(item.FGiaTriNoiDungUSD);
                            x.FVnd = NumberUtils.ConvertTextToDouble(item.FGiaTriNoiDungVND);
                        }
                    });
                    return true;
                }

            }
            else
            {
                if (string.IsNullOrEmpty(item.SSoQuyetDinhDauTu))
                    return true;
                _importErrors.Add(new ImportErrorItem { Row = ListDataImport.IndexOf(item), Error = Resources.MsgNotExistData, ColumnName = NhTongHopConstants.SLOAI_DU_AN });
                item.ImportStatus = false;
                return false;
            }
        }

        private bool ValidateHopDong(NhKtKhoiTaoCapPhatChiTietImport item)
        {
            if (ListHopDongImport.Any(x => x.SSoHopDong.Equals(item.SSoHopDong) && !string.IsNullOrEmpty(x.SSoHopDong)))
            {
                if (item.ILoai == NHConstants.ILOAI_CHI_PHI)
                {
                    var hopDong = ListHopDongImport.FirstOrDefault(x => x.SSoHopDong.Equals(item.SSoHopDong) && !string.IsNullOrEmpty(x.SSoHopDong));
                    var hopDongKt = ListDataImport.FirstOrDefault(x => x.IIdHopDongID.Equals(hopDong.Id) && x.ILoai == NHConstants.ILOAI_HOP_DONG);
                    if (hopDongKt != null)
                    {
                        item.IIdHopDongID = hopDong.Id;
                        item.IIdParentID = ListDataImport.FirstOrDefault(x => x.IIdHopDongID.Equals(hopDong.Id) && x.ILoai == NHConstants.ILOAI_HOP_DONG).Id;
                        item.IIdKhttNhiemVuChiID = hopDongKt.IIdKhttNhiemVuChiID;
                        return true;
                    }
                    else
                    {

                        _importErrors.Add(new ImportErrorItem { Row = ListDataImport.IndexOf(item), Error = Resources.MsgNotExistData, ColumnName = NhTongHopConstants.SLOAI_CHI_PHI });
                        return false;
                    }
                }
                else
                {
                    item.IIdHopDongID = ListHopDongImport.FirstOrDefault(x => x.SSoHopDong.Equals(item.SSoHopDong)).Id;
                    ListHopDongImport.ForEach(x =>
                    {
                        if (x.Id.Equals(item.IIdHopDongID))
                        {
                            x.FGiaTriHopDongUSD = NumberUtils.ConvertTextToDouble(item.FGiaTriNoiDungUSD);
                            x.FGiaTriUsd = NumberUtils.ConvertTextToDouble(item.FGiaTriNoiDungUSD);
                            x.FGiaTriHopDongVND = NumberUtils.ConvertTextToDouble(item.FGiaTriNoiDungVND);
                            x.FGiaTriVnd = NumberUtils.ConvertTextToDouble(item.FGiaTriNoiDungVND);
                        }
                    });
                    return true;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(item.SSoHopDong))
                    return true;
                _importErrors.Add(new ImportErrorItem { Row = ListDataImport.IndexOf(item), Error = Resources.MsgNotExistData, ColumnName = NhTongHopConstants.SLOAI_HOP_DONG });
                return false;
            }
        }

        private bool ValidateQDKhac(NhKtKhoiTaoCapPhatChiTietImport item)
        {
            if (ListQuyetDinhChiPhiKhac.Any(x => x.SSoQuyetDinh.Equals(item.SSoQuyetDinhKhac) && !string.IsNullOrEmpty(x.SSoQuyetDinh)))
            {
                if (item.ILoai == NHConstants.ILOAI_CHI_PHI)
                {
                    var qdKhac = ListQuyetDinhChiPhiKhac.FirstOrDefault(x => x.SSoQuyetDinh.Equals(item.SSoQuyetDinhKhac) && !string.IsNullOrEmpty(x.SSoQuyetDinh));
                    var qdKhacKT = ListDataImport.FirstOrDefault(x => x.IIdQuyetDinhKhacID.Equals(qdKhac.Id) && x.ILoai == NHConstants.ILOAI_QD_KHAC);
                    if (qdKhacKT != null)
                    {
                        item.IIdQuyetDinhKhacID = qdKhac.Id;
                        item.IIdKhttNhiemVuChiID = qdKhacKT.IIdKhttNhiemVuChiID;
                        item.IIdParentID = ListDataImport.FirstOrDefault(x => x.IIdQuyetDinhKhacID.Equals(qdKhac.Id) && x.ILoai == NHConstants.ILOAI_QD_KHAC).Id;
                        return true;
                    }
                    else
                    {
                        _importErrors.Add(new ImportErrorItem { Row = ListDataImport.IndexOf(item), Error = Resources.MsgNotExistData, ColumnName = NhTongHopConstants.SLOAI_CHI_PHI });
                        return false;
                    }
                }
                else
                {
                    item.IIdQuyetDinhKhacID = ListQuyetDinhChiPhiKhac.FirstOrDefault(x => x.SSoQuyetDinh.Equals(item.SSoQuyetDinhKhac) && !string.IsNullOrEmpty(x.SSoQuyetDinh)).Id;
                    ListQuyetDinhChiPhiKhac.ForEach(x =>
                    {
                        if (x.Id.Equals(item.IIdQuyetDinhKhacID))
                        {
                            x.FGiaTriUsd = NumberUtils.ConvertTextToDouble(item.FGiaTriNoiDungUSD);
                            x.FGiaTriVnd = NumberUtils.ConvertTextToDouble(item.FGiaTriNoiDungVND);
                        }
                    });
                    return true;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(item.SSoQuyetDinhKhac))
                    return true;
                _importErrors.Add(new ImportErrorItem { Row = ListDataImport.IndexOf(item), Error = Resources.MsgNotExistData, ColumnName = NhTongHopConstants.SLOAI_QD_KHAC });
                return false;
            }
        }

        private bool ValidateChiPhi(NhKtKhoiTaoCapPhatChiTietImport item)
        {
            List<NhDaHopDongChiPhi> lstHdChiPhi = new List<NhDaHopDongChiPhi>();
            List<NhDaQuyetDinhKhacChiPhi> lstQdkChiPhi = new List<NhDaQuyetDinhKhacChiPhi>();
            if (ListNhDmChiPhi.Any(x => x.SMaChiPhi.Trim().Equals(item.SMaChiPhi.Trim())))
            {
                var chiPhi = ListNhDmChiPhi.FirstOrDefault(x => x.SMaChiPhi.Trim().Equals(item.SMaChiPhi.Trim()));
                if (item.ILoai.Equals(NHConstants.ILOAI_CHI_PHI))
                {
                    item.IIdChiPhiID = chiPhi.IIdChiPhi;
                    item.STenVietTatChiPhi = chiPhi.STenVietTat;
                    return true;
                }
                else if (item.ILoai.Equals(NHConstants.ILOAI_HOP_DONG))
                {
                    if (ListHopDongImport.Any(x => x.SSoHopDong.Equals(item.SSoHopDong)))
                    {
                        var itemHopDong = ListHopDongImport.FirstOrDefault(x => x.SSoHopDong.Equals(item.SSoHopDong));
                        lstHdChiPhi = itemHopDong.HopDongChiPhis.ToList() ?? new List<NhDaHopDongChiPhi>();
                        NhDaHopDongChiPhi hdchiphi = new NhDaHopDongChiPhi
                        {
                            IIdHopDongId = itemHopDong.Id,
                            IIdChiPhiId = chiPhi.IIdChiPhi
                        };
                        lstHdChiPhi.Add(hdchiphi);
                        itemHopDong.HopDongChiPhis = lstHdChiPhi;
                    }

                    return true;
                }
                else
                {
                    if (ListQuyetDinhChiPhiKhac.Any(x => x.SSoQuyetDinh.Equals(item.SSoQuyetDinhKhac)))
                    {
                        var itemQdKhac = ListQuyetDinhChiPhiKhac.FirstOrDefault(x => x.SSoQuyetDinh.Equals(item.SSoQuyetDinhKhac));
                        lstQdkChiPhi = itemQdKhac.LstChiTiet ?? new List<NhDaQuyetDinhKhacChiPhi>();
                        NhDaQuyetDinhKhacChiPhi hdchiphi = new NhDaQuyetDinhKhacChiPhi
                        {
                            IIdQuyetDinhKhacId = itemQdKhac.Id,
                            IIdDmChiPhiId = chiPhi.IIdChiPhi,
                            
                        };
                        lstQdkChiPhi.Add(hdchiphi);
                        itemQdKhac.LstChiTiet = lstQdkChiPhi;
                    }

                    return true;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(item.SMaChiPhi))
                    return true;
                _importErrors.Add(new ImportErrorItem { Row = ListDataImport.IndexOf(item), Error = Resources.MsgNotExistData, ColumnName = NhTongHopConstants.SLOAI_CHI_PHI });
                return false;
            }
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
