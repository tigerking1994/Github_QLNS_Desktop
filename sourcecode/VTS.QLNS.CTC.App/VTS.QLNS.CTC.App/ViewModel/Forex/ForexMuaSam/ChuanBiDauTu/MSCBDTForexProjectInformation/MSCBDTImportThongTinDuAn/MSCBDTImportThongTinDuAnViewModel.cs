using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using AutoMapper;
using FlexCel.XlsAdapter;
using log4net;
using MaterialDesignThemes.Wpf;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTForexProjectInformation.MSCBDTImportThongTinDuAn
{
    public class MSCBDTImportThongTinDuAnViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ILog _logger;
        private readonly IImportExcelService _importService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INhDaDuAnNguonVonService _nhDaDuAnNguonVonService;
        private readonly INhDaDuAnHangMucService _nhDaDuAnHangMucService;
        private readonly INsDonViService _nsDonViService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private readonly INhDmPhanCapPheDuyetService _nhDmPhanCapPheDuyetService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhDmLoaiCongTrinhService _nhDmLoaiCongTrinhService;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private static int indexSMaDuAn;
        private List<ImportErrorItem> _lstErrQlDuAn = new List<ImportErrorItem>();
        private Dictionary<string, NhDaDuAn> _dicDuAn;
        private Dictionary<string, Dictionary<string, NhDaDuAnNguonVon>> _dicNguonVon;
        private Dictionary<string, Dictionary<string, NhDaDuAnHangMuc>> _dicHangMuc;
        private Dictionary<string, NhDmLoaiCongTrinh> _dicLoaiCongTrinh;
        private Dictionary<string, NsNguonNganSach> _dicNsNguonVon;
        private Dictionary<string, DmChuDauTu> _dicChuDauTu;
        private Dictionary<string, NhDmPhanCapPheDuyet> _dicPhanCap;

        public override Type ContentType => typeof(View.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTForexProjectInformation.ImportThongTinDuAn.ImportThongTinDuAn);
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private ObservableCollection<ThongTinDuAnImportModel> _items;
        public ObservableCollection<ThongTinDuAnImportModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private ThongTinDuAnImportModel _selectedItem;
        public ThongTinDuAnImportModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private ObservableCollection<NhKhTongTheModel> _itemsSoKeHoachTongTheBQP;
        public ObservableCollection<NhKhTongTheModel> ItemsSoKeHoachTongTheBQP
        {
            get => _itemsSoKeHoachTongTheBQP;
            set => SetProperty(ref _itemsSoKeHoachTongTheBQP, value);
        }

        private NhKhTongTheModel _selectedSoKeHoachTongTheBQP;
        public NhKhTongTheModel SelectedSoKeHoachTongTheBQP
        {
            get => _selectedSoKeHoachTongTheBQP;
            set
            {
                if (SetProperty(ref _selectedSoKeHoachTongTheBQP, value))
                {
                    LoadDonVi();
                    LoadNhiemVuChi();
                }
            }
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
                if (SetProperty(ref _selectedDonVi, value))
                {
                    LoadNhiemVuChi();
                }
            }
        }
        private ObservableCollection<NhDmNhiemVuChiModel> _itemsNhiemVuChi;
        public ObservableCollection<NhDmNhiemVuChiModel> ItemsNhiemVuChi
        {
            get => _itemsNhiemVuChi;
            set => SetProperty(ref _itemsNhiemVuChi, value);
        }

        private NhDmNhiemVuChiModel _selectedNhiemVuChi;
        public NhDmNhiemVuChiModel SelectedNhiemVuChi
        {
            get => _selectedNhiemVuChi;
            set => SetProperty(ref _selectedNhiemVuChi, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (Items != null && Items.Count > 0 && SelectedSoKeHoachTongTheBQP != null && SelectedDonVi != null && SelectedNhiemVuChi != null)
                    return Items.Any(x => x.ImportStatus);
                return false;
            }
        }

        public MSCBDTForexProjectInformationDialogViewModel ForexProjectInformationDialogViewModel { get; set; }
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorCommand { get; }

        public MSCBDTImportThongTinDuAnViewModel(ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IImportExcelService importService,
            INhDaDuAnService nhDaDuAnService,
            INhDaDuAnNguonVonService nhDaDuAnNguonVonService,
            INhDaDuAnHangMucService nhDaDuAnHangMucService,
            INsDonViService nsDonViService,
            IDmChuDauTuService dmChuDauTuService,
            INhDmPhanCapPheDuyetService nhDmPhanCapPheDuyetService,
            INsNguonNganSachService nsNguonNganSachService,
            INhDmLoaiCongTrinhService nhDmLoaiCongTrinhService,
            INhKhTongTheService nhKhTongTheService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            MSCBDTForexProjectInformationDialogViewModel forexProjectInformationDialogViewModel)
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _importService = importService;
            _nhDaDuAnService = nhDaDuAnService;
            _nhDaDuAnNguonVonService = nhDaDuAnNguonVonService;
            _nhDaDuAnHangMucService = nhDaDuAnHangMucService;
            _nsDonViService = nsDonViService;
            _dmChuDauTuService = dmChuDauTuService;
            _nhDmPhanCapPheDuyetService = nhDmPhanCapPheDuyetService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDmLoaiCongTrinhService = nhDmLoaiCongTrinhService;
            _nhKhTongTheService = nhKhTongTheService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;

            ForexProjectInformationDialogViewModel = forexProjectInformationDialogViewModel;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData(obj));
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            ShowErrorCommand = new RelayCommand(obj => ShowError());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadSoKeHoachTongThe();
            LoadDonVi();
            OnLoadLoaiCongTrinh();
            OnLoadNguonVon();
            OnLoadChuDauTu();
            OnLoadPhanCap();
            OnResetData();
        }

        private void OnResetData()
        {
            _fileName = string.Empty;
            _selectedSoKeHoachTongTheBQP = null;
            _selectedDonVi = null;
            _selectedNhiemVuChi = null;
            _items = new ObservableCollection<ThongTinDuAnImportModel>();
            OnPropertyChanged(nameof(SelectedSoKeHoachTongTheBQP));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedNhiemVuChi));
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
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

        private void HandleData()
        {
            Dictionary<string, Guid> _dicMaDuAn = new Dictionary<string, Guid>();
            var lstDuAn = _nhDaDuAnService.FindAll(n => n.SMaDuAn != null && n.ILoai == 1);
            if (lstDuAn != null)
            {
                foreach (var item in lstDuAn)
                {
                    if (!_dicMaDuAn.ContainsKey(item.SMaDuAn))
                    {
                        _dicMaDuAn.Add(item.SMaDuAn, item.Id);
                    }
                }
            }
            _dicDuAn = new Dictionary<string, NhDaDuAn>();
            _dicNguonVon = new Dictionary<string, Dictionary<string, NhDaDuAnNguonVon>>();
            _dicHangMuc = new Dictionary<string, Dictionary<string, NhDaDuAnHangMuc>>();
            try
            {
                XlsFile xls = new XlsFile(false);
                xls.Open(FileName);
                xls.ActiveSheet = 1;

                var dataImport = _importService.ProcessData<ThongTinDuAnImportModel>(FileName);
                var QlDuAnImportModels = new ObservableCollection<ThongTinDuAnImportModel>(dataImport.Data);

                _lstErrQlDuAn = new List<ImportErrorItem>();
                List<string> lstError = new List<string>();

                if (dataImport.ImportErrors.Count > 0)
                {
                    _lstErrQlDuAn.AddRange(dataImport.ImportErrors);
                }

                if (QlDuAnImportModels == null || QlDuAnImportModels.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (QlDuAnImportModels.Any(x => !x.ImportStatus))
                {
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (string.IsNullOrEmpty(FileName))
                {
                    lstError.Add(Resources.ErrorFileEmpty);
                }

                int i = 0;
                foreach (var item in QlDuAnImportModels)
                {
                    ++i;
                    var listError = ValidateItem(item, i, _dicMaDuAn);

                    if (listError.Count > 0)
                    {
                        _lstErrQlDuAn.AddRange(listError);
                        item.ImportStatus = false;

                        if (listError.Any(x => x.IsErrorMLNS))
                        {
                            item.IsError = true;
                        }
                    }
                }

                Items = QlDuAnImportModels;

                if (lstError.Any())
                {
                    string sMessError = string.Join(Environment.NewLine, lstError);
                    System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                FileName = "";
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
            }
        }

        private List<ImportErrorItem> ValidateItem(ThongTinDuAnImportModel item, int rowIndex, Dictionary<string, Guid> _dicMaDuAn)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                if (string.IsNullOrEmpty(item.SMaDuAn))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Mã dự án",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "mã dự án"),
                        Row = rowIndex
                    });
                }
                else
                {
                    if (!_dicDuAn.ContainsKey(item.SMaDuAn))
                    {
                        _dicDuAn.Add(item.SMaDuAn, new NhDaDuAn());
                        _dicHangMuc.Add(item.SMaDuAn, new Dictionary<string, NhDaDuAnHangMuc>());
                        _dicNguonVon.Add(item.SMaDuAn, new Dictionary<string, NhDaDuAnNguonVon>());
                    }

                    if (_dicMaDuAn.ContainsKey(item.SMaDuAn))
                    {
                        errors.Add(new ImportErrorItem()
                        {
                            ColumnName = "Mã dự án",
                            Error = string.Format(Resources.MsgDuAnExisted, item.SMaDuAn),
                            Row = rowIndex
                        });
                    }

                    if (string.IsNullOrEmpty(item.STenDuAn))
                    {
                        errors.Add(new ImportErrorItem()
                        {
                            ColumnName = "Tên dự án",
                            Error = string.Format(Resources.MsgErrorDataEmpty, "tên dự án"),
                            Row = rowIndex
                        });
                    }

                    if (string.IsNullOrEmpty(item.ThoiGianThucHienTu))
                    {
                        errors.Add(new ImportErrorItem()
                        {
                            ColumnName = "Thời gian thực hiện từ",
                            Error = string.Format(Resources.MsgErrorDataEmpty, "thời gian thực hiện từ"),
                            Row = rowIndex
                        });
                    }

                    if (string.IsNullOrEmpty(item.ThoiGianThucHienDen))
                    {
                        errors.Add(new ImportErrorItem()
                        {
                            ColumnName = "Thời gian thực hiện đến",
                            Error = string.Format(Resources.MsgErrorDataEmpty, "thời gian thực hiện đến"),
                            Row = rowIndex
                        });
                    }

                    if (!string.IsNullOrEmpty(item.ThoiGianThucHienTu) && !string.IsNullOrEmpty(item.ThoiGianThucHienDen))
                    {
                        int countError = 0;
                        string[] tempKhoiCong = item.ThoiGianThucHienTu.Split('/');
                        string[] tempKetThuc = item.ThoiGianThucHienDen.Split('/');
                        string strKhoiCong = "", strKetThuc = "";
                        DateTime typeDate;

                        if (tempKhoiCong.Length > 3)
                        {
                            countError++;
                            errors.Add(new ImportErrorItem()
                            {
                                ColumnName = "Thời gian thực hiện từ",
                                Error = string.Format(Resources.MsgDateTimeFromInValid),
                                Row = rowIndex
                            });
                        }
                        if (tempKetThuc.Length > 3)
                        {
                            countError++;
                            errors.Add(new ImportErrorItem()
                            {
                                ColumnName = "Thời gian thực hiện đến",
                                Error = string.Format(Resources.MsgDateTimeToInValid),
                                Row = rowIndex
                            });
                        }
                        if (tempKhoiCong.Length != tempKetThuc.Length)
                        {
                            countError++;
                            errors.Add(new ImportErrorItem()
                            {
                                ColumnName = "Thời gian thực hiện từ",
                                Error = string.Format(Resources.MsgDateTimeFromToInValid),
                                Row = rowIndex
                            });
                        }
                        else
                        {
                            if (tempKhoiCong.Length == 1)
                            {
                                strKhoiCong = "01/01/" + tempKhoiCong[0];
                                strKetThuc = "01/01/" + tempKetThuc[0];
                            }
                            else if (tempKhoiCong.Length == 2)
                            {
                                strKhoiCong = "01/" + tempKhoiCong[0] + "/" + tempKhoiCong[1];
                                strKetThuc = "01/" + tempKetThuc[0] + "/" + tempKetThuc[1];


                            }
                            else if (tempKhoiCong.Length == 3)
                            {
                                strKhoiCong = tempKhoiCong[0] + "/" + tempKhoiCong[1] + "/" + tempKhoiCong[2];
                                strKetThuc = tempKetThuc[0] + "/" + tempKetThuc[1] + "/" + tempKetThuc[2];
                            }

                            if (!DateTime.TryParseExact(strKhoiCong, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out typeDate))
                            {
                                countError++;
                                errors.Add(new ImportErrorItem()
                                {
                                    ColumnName = "Thời gian thực hiện từ",
                                    Error = string.Format(Resources.MsgDateTimeFromInValid),
                                    Row = rowIndex
                                });
                            }
                            if (!DateTime.TryParseExact(strKetThuc, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out typeDate))
                            {
                                countError++;
                                errors.Add(new ImportErrorItem()
                                {
                                    ColumnName = "Thời gian thực hiện đến",
                                    Error = string.Format(Resources.MsgDateTimeToInValid),
                                    Row = rowIndex
                                });
                            }
                        }

                        if (countError == 0)
                        {
                            DateTime dateKhoiCong = Convert.ToDateTime(strKhoiCong);
                            DateTime dateKetThuc = Convert.ToDateTime(strKetThuc);

                            if (dateKhoiCong >= dateKetThuc)
                            {
                                errors.Add(new ImportErrorItem()
                                {
                                    ColumnName = "Thời gian thực hiện từ",
                                    Error = string.Format(Resources.MsgDateTimeFromSmall),
                                    Row = rowIndex
                                });
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(item.STenHangMuc))
                    {
                        errors.Add(new ImportErrorItem()
                        {
                            ColumnName = "Tên hạng mục",
                            Error = string.Format(Resources.MsgErrorDataEmpty, "tên hạng mục"),
                            Row = rowIndex
                        });
                    }
                    else if (!string.IsNullOrEmpty(item.SMaLoaiCongTrinh))
                    {
                        string sKeyHangMuc = string.Format("{0}\n{1}", item.STenHangMuc, item.SMaLoaiCongTrinh);
                        if (!_dicHangMuc[item.SMaDuAn].ContainsKey(sKeyHangMuc))
                        {
                            _dicHangMuc[item.SMaDuAn].Add(sKeyHangMuc, new NhDaDuAnHangMuc());
                        }
                        if (!_dicLoaiCongTrinh.ContainsKey(item.SMaLoaiCongTrinh))
                        {
                            errors.Add(new ImportErrorItem()
                            {
                                ColumnName = "Mã loại công trình",
                                Error = string.Format(Resources.MsgErrorItemNotFound, "Mã loại công trình"),
                                Row = rowIndex
                            });
                        }
                    }

                    if (!string.IsNullOrEmpty(item.IIdNguonVonId))
                    {
                        if (!_dicNguonVon[item.SMaDuAn].ContainsKey(item.IIdNguonVonId))
                        {
                            _dicNguonVon[item.SMaDuAn].Add(item.IIdNguonVonId, new NhDaDuAnNguonVon());
                        }
                        if (!_dicNsNguonVon.ContainsKey(item.IIdNguonVonId))
                        {
                            errors.Add(new ImportErrorItem()
                            {
                                ColumnName = "Mã nguồn vốn",
                                Error = string.Format(Resources.MsgErrorItemNotFound, "Mã nguồn vốn"),
                                Row = rowIndex
                            });
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

        private void ShowError()
        {
            try
            {
                var errors = new HashSet<string>();
                int rowIndex = Items.IndexOf(SelectedItem);
                errors = _lstErrQlDuAn.Where(x => x.Row == (rowIndex + 1)).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
                System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnProcessFile()
        {
            HandleData();  
        }

        private void OnSaveData(object obj)
        {
            try
            {
                List<string> lstError = new List<string>();
                HandleSaveData(Items.Where(x => x.ImportStatus).ToList());
                SavedAction?.Invoke(null);
                MessageBoxHelper.Info("Lưu dữ liệu thành công.");
                Window window = obj as Window;
                window.Close();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }
        private void HandleSaveData(List<ThongTinDuAnImportModel> items)
        {

            if (items.Count <= 0) throw new Exception("Không có bản ghi nào có thể lưu!");

            DonVi donviSuDung = _nsDonViService.FindByIdDonVi(_sessionService.Current.IdDonVi, _sessionService.Current.YearOfWork);
            _dicDuAn = new Dictionary<string, NhDaDuAn>();
            Dictionary<string, NhDaDuAnHangMuc> dicDuAnHangMuc = new Dictionary<string, NhDaDuAnHangMuc>();
            Dictionary<string, NhDaDuAnNguonVon> dicDuAnNguonVon = new Dictionary<string, NhDaDuAnNguonVon>();

            foreach (var item in items)
            {
                if (item.ImportStatus == true)
                {
                    if (!_dicDuAn.ContainsKey(item.SMaDuAn))
                    {
                        _dicDuAn.Add(item.SMaDuAn, new NhDaDuAn()
                        {
                            Id = Guid.NewGuid(),
                            STenDuAn = item.STenDuAn,
                            SMaDuAn = item.SMaDuAn,
                            SKhoiCong = item.ThoiGianThucHienTu,
                            SKetThuc = item.ThoiGianThucHienDen,
                            DNgayTao = DateTime.Now,
                            IIdKhttNhiemVuChiId = SelectedNhiemVuChi.IIdKHTTNhiemVuChiId,
                            IIdDonViQuanLyId = SelectedDonVi.Id,
                            IIdMaDonViQuanLy = SelectedDonVi.IIDMaDonVi,
                            IIdMaChuDauTu = item.MaChuDauTu,
                            IIdChuDauTuId = _dicChuDauTu[item.MaChuDauTu].Id,
                            IIdCapPheDuyetId = _dicPhanCap[item.MaPhanCapPheDuyet].Id,
                            ILoai = 1,
                            SNguoiTao = _sessionService.Current.Principal
                        });
                    }

                    if (!string.IsNullOrEmpty(item.STenHangMuc))
                    {
                        string sKeyHangMuc = string.Format("{0}\n{1}\n{2}", item.SMaDuAn, item.STenHangMuc, item.SMaLoaiCongTrinh);
                        if (!dicDuAnHangMuc.ContainsKey(sKeyHangMuc))
                        {
                            dicDuAnHangMuc.Add(sKeyHangMuc, new NhDaDuAnHangMuc()
                            {
                                Id = Guid.NewGuid(),
                                STenHangMuc = item.STenHangMuc,
                                IIdLoaiCongTrinhId = item.SMaLoaiCongTrinh == "" ? (Guid?)null : _dicLoaiCongTrinh[item.SMaLoaiCongTrinh].Id,
                                IIdDuAnId = _dicDuAn[item.SMaDuAn].Id
                            });
                        }
                    }

                    if (!string.IsNullOrEmpty(item.IIdNguonVonId))
                    {
                        string sKeyNguonVon = string.Format("{0}\n{1}", item.SMaDuAn, item.IIdNguonVonId);
                        if (!dicDuAnNguonVon.ContainsKey(sKeyNguonVon))
                        {
                            dicDuAnNguonVon.Add(sKeyNguonVon, new NhDaDuAnNguonVon()
                            {
                                Id = Guid.NewGuid(),
                                IIdNguonVonId = int.Parse(item.IIdNguonVonId),
                                IIdDuAnId = _dicDuAn[item.SMaDuAn].Id,
                                FGiaTriUsd = item.FGiaTriUsd == "" ? (double?)null : float.Parse(item.FGiaTriUsd),
                                FGiaTriVnd = item.FGiaTriVnd == "" ? (double?)null : float.Parse(item.FGiaTriVnd)
                            });
                        }
                    }
                }
            }
            _nhDaDuAnService.AddRange(_dicDuAn.Values);
            if (dicDuAnHangMuc.Count != 0)
            {
                _nhDaDuAnHangMucService.AddRange(dicDuAnHangMuc.Values);
            }
            if (dicDuAnNguonVon.Count != 0)
            {
                _nhDaDuAnNguonVonService.InsertDuAnNguonVon(dicDuAnNguonVon.Values);
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        private void LoadSoKeHoachTongThe()
        {
            IEnumerable<NhKhTongThe> data = _nhKhTongTheService.FindAll(s => s.BIsActive);
            _itemsSoKeHoachTongTheBQP = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data);
            _itemsSoKeHoachTongTheBQP.ForAll(s =>
            {
                if (s.ILoai == Loai_KHTT.GIAIDOAN)
                {
                    s.TenKeHoach = $"KHTT {s.IGiaiDoanTu_BQP} - {s.IGiaiDoanDen_BQP} - Số KH: {s.SSoKeHoachBqp}";
                }
                else
                {
                    s.TenKeHoach = $"KHTT {s.INamKeHoach} - Số KH: {s.SSoKeHoachBqp}";
                }
            });
            OnPropertyChanged(nameof(ItemsSoKeHoachTongTheBQP));
        }

        private void LoadDonVi()
        {
            _itemsDonVi = new ObservableCollection<DonViModel>();
            if (_selectedSoKeHoachTongTheBQP != null)
            {
                List<NhKhTongTheNhiemVuChiQuery> data = _nhKhTongTheNhiemVuChiService.FindAllDonViByKhTongTheId(_selectedSoKeHoachTongTheBQP.Id).Where(x => x.NamLamViec == _sessionInfo.YearOfWork).ToList();
                _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadNhiemVuChi()
        {
            _itemsNhiemVuChi = new ObservableCollection<NhDmNhiemVuChiModel>();
            if (SelectedDonVi != null && SelectedSoKeHoachTongTheBQP != null)
            {
                var data = _nhDmNhiemVuChiService.FindByKhTongTheIdAndDonViId(SelectedSoKeHoachTongTheBQP.Id, SelectedDonVi.Id);
                _itemsNhiemVuChi = _mapper.Map<ObservableCollection<NhDmNhiemVuChiModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsNhiemVuChi));
        }
        private void OnLoadLoaiCongTrinh()
        {
            _dicLoaiCongTrinh = new Dictionary<string, NhDmLoaiCongTrinh>();
            var data = _nhDmLoaiCongTrinhService.FindAll();
            if (data == null) return;
            foreach (var item in data)
            {
                if (!_dicLoaiCongTrinh.ContainsKey(item.SMaLoaiCongTrinh))
                {
                    _dicLoaiCongTrinh.Add(item.SMaLoaiCongTrinh, item);
                }
            }
        }
        private void OnLoadNguonVon()
        {
            _dicNsNguonVon = new Dictionary<string, NsNguonNganSach>();
            var data = _nsNguonNganSachService.FindAll();
            if (data == null) return;
            foreach (var item in data)
            {
                if (!_dicNsNguonVon.ContainsKey((item.IIdMaNguonNganSach ?? 0).ToString()))
                {
                    _dicNsNguonVon.Add((item.IIdMaNguonNganSach ?? 0).ToString(), item);
                }
            }
        }
        private void OnLoadChuDauTu()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            _dicChuDauTu = new Dictionary<string, DmChuDauTu>();
            var data = _dmChuDauTuService.FindByNamLamViec(yearOfWork);
            if (data == null) return;
            foreach (var item in data)
            {
                if (!_dicChuDauTu.ContainsKey(item.IIDMaDonVi))
                {
                    _dicChuDauTu.Add(item.IIDMaDonVi, item);
                }
            }
        }
        private void OnLoadPhanCap()
        {
            _dicPhanCap = new Dictionary<string, NhDmPhanCapPheDuyet>();
            var data = _nhDmPhanCapPheDuyetService.FindAll();
            if (data == null) return;
            foreach (var item in data)
            {
                if (!_dicPhanCap.ContainsKey(item.SMa))
                {
                    _dicPhanCap.Add(item.SMa, item);
                }
            }
        }
    }
}
