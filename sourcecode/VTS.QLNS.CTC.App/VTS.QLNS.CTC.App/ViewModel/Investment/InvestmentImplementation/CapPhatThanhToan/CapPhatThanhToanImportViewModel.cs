using AutoMapper;
using FlexCel.XlsAdapter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.CapPhatThanhToan
{
    public class CapPhatThanhToanImportViewModel : ViewModelBase
    {
        public override Type ContentType => typeof(VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.CapPhatThanhToan.CapPhatThanhToanImport);

        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private IImportExcelService _importService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly IVdtDaDuAnService _duAnService;
        private readonly INsNguonNganSachService _nguonVonService;
        private readonly INsDonViService _nsDonViService;
        private readonly IDmChuDauTuService _chuDauTuService;
        private readonly ISessionService _sessionService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly IVdtDaTtHopDongService _vdtDaTtHopDongService;
        public readonly IVdtTtDeNghiThanhToanService _service;
        public readonly IVdtTtDeNghiThanhToanKhvService _khvService;
        private readonly IVdtDmChiPhiService _vdtDmChiPhiService;
        private readonly IVdtKhvPhanBoVonService _vdtKhvPhanBoVonService;
        private readonly IVdtKhvKeHoachVonUngService _vdtKhvKeHoachVonUngService;
        private readonly IVdtQtBcQuyetToanNienDoService _vdtQtBcQuyetToanNienDoService;
        private readonly IMapper _mapper;
        private List<ImportErrorItem> _lstErrChungTuChiTiet;
        private List<string> lstSoDeNghi = new List<string>();
        private List<VdtDaTtHopDong> _lstHopDong;
        private List<VdtDmChiPhi> _lstCP;
        private string _fileName;
        private Dictionary<string, Guid> _dicDuAn = new Dictionary<string, Guid>();
        #endregion

        #region Import item
        private ObservableCollection<VdtTtDeNghiThanhToanNSQPImportModel> _itemsThanhToan;
        public ObservableCollection<VdtTtDeNghiThanhToanNSQPImportModel> ItemsThanhToan
        {
            get => _itemsThanhToan;
            set => SetProperty(ref _itemsThanhToan, value);
        }

        private VdtTtDeNghiThanhToanNSQPImportModel _selectedThanhToan;
        public VdtTtDeNghiThanhToanNSQPImportModel SelectedThanhToan
        {
            get => _selectedThanhToan;
            set => SetProperty(ref _selectedThanhToan, value);
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
            get => ItemsThanhToan != null && ItemsThanhToan.Count > 0 && !ItemsThanhToan.Any(x => !x.ImportStatus);
            set => SetProperty(ref _isSaveData, value);
        }
        #endregion

        #region Items
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
            set
            {
                if (SetProperty(ref _selectedDonVi, value))
                {
                    LoadDropDownChuDauTu();
                }
            }
        }

        private int _namKeHoach;
        public int NamKeHoach
        {
            get => _namKeHoach;
            set => SetProperty(ref _namKeHoach, value);
        }

        private ObservableCollection<ComboboxItem> _itemsChuDauTu;
        public ObservableCollection<ComboboxItem> ItemsChuDauTu
        {
            get => _itemsChuDauTu;
            set => SetProperty(ref _itemsChuDauTu, value);
        }

        private ComboboxItem _selectedChuDauTu;
        public ComboboxItem SelectedChuDauTu
        {
            get => _selectedChuDauTu;
            set
            {
                if (SetProperty(ref _selectedChuDauTu, value))
                {
                    LoadDropDownDuAn();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsCoQuanThanhToan;
        public ObservableCollection<ComboboxItem> ItemsCoQuanThanhToan
        {
            get => _itemsCoQuanThanhToan;
            set => SetProperty(ref _itemsCoQuanThanhToan, value);
        }

        private ComboboxItem _selectedCoQuanThanhToan;
        public ComboboxItem SelectedCoQuanThanhToan
        {
            get => _selectedCoQuanThanhToan;
            set => SetProperty(ref _selectedCoQuanThanhToan, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiThanhToan;
        public ObservableCollection<ComboboxItem> ItemsLoaiThanhToan
        {
            get => _itemsLoaiThanhToan;
            set => SetProperty(ref _itemsLoaiThanhToan, value);
        }

        private ObservableCollection<ComboboxItem> _itemsNguonVon;
        public ObservableCollection<ComboboxItem> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private ObservableCollection<ComboboxItem> _itemsDuAn;
        public ObservableCollection<ComboboxItem> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }

        private ComboboxItem _selectedNguonVon;
        public ComboboxItem SelectedNguonVon
        {
            get => _selectedNguonVon;
            set => SetProperty(ref _selectedNguonVon, value);
        }
        #endregion

        #region RelayCommand
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorCommand { get; }
        #endregion

        public CapPhatThanhToanImportViewModel(
            IImportExcelService importService,
            ITongHopNguonNSDauTuService tonghopService,
            IVdtDaDuAnService duAnService,
            INsNguonNganSachService nguonVonService,
            INsDonViService nsDonViService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IVdtTtDeNghiThanhToanService service,
            IVdtDaTtHopDongService vdtDaTtHopDongService,
            IDmChuDauTuService chuDauTuService,
            ISessionService sessionService,
            IVdtDmChiPhiService vdtDmChiPhiService,
            IVdtKhvPhanBoVonService vdtKhvPhanBoVonService,
            IVdtKhvKeHoachVonUngService vdtKhvKeHoachVonUngService,
            IVdtQtBcQuyetToanNienDoService vdtQtBcQuyetToanNienDoService,
            IMapper mapper)
        {
            _importService = importService;
            _tonghopService = tonghopService;
            _duAnService = duAnService;
            _nguonVonService = nguonVonService;
            _nsDonViService = nsDonViService;
            _chuDauTuService = chuDauTuService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _vdtDaTtHopDongService = vdtDaTtHopDongService;
            _service = service;
            _sessionService = sessionService;
            _vdtDmChiPhiService = vdtDmChiPhiService;
            _vdtKhvPhanBoVonService = vdtKhvPhanBoVonService;
            _mapper = mapper;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(ShowError);
        }

        #region RelayCommnad
        public override void Init()
        {
            NamKeHoach = DateTime.Now.Year;
            FilePath = string.Empty;
            SelectedDonVi = null;
            SelectedChuDauTu = null;
            SelectedCoQuanThanhToan = null;
            ItemsThanhToan = new ObservableCollection<VdtTtDeNghiThanhToanNSQPImportModel>();
            LoadDropDownDonVi();
            LoadDropDownCoQuanThanhToan();
            LoadDropDownNguonVon();
            LoadAllListHopDong();
            LoadDropDownLoaiThanhToan();
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

        private void OnProcessFile()
        {
            _lstErrChungTuChiTiet = new List<ImportErrorItem>();
            List<string> lstError = new List<string>();

            if (string.IsNullOrEmpty(FilePath))
            {
                lstError.Add(Resources.ErrorFileEmpty);
            }

            ;
            if (!ValidateForm()) return;

            var data = GetDataImportByFileType();
            /*foreach (var item in data)
            {
                if (_dicDuAn.ContainsKey(item.SMaDuAn))
                    item.IIdDuAnId = _dicDuAn[item.SMaDuAn];
                var DNgayDeNghi = DateTime.ParseExact(item.SNgayDeNghi, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                List<KeHoachVonQuery> list = _vdtKhvPhanBoVonService.GetKeHoachVonCapPhatThanhToan(
                item.IIdDuAnId.ToString(),
                int.Parse(SelectedNguonVon.ValueItem),
                DNgayDeNghi,
                NamKeHoach,
                int.Parse(SelectedCoQuanThanhToan.ValueItem),
                Guid.Empty);
            }*/
            
            ItemsThanhToan = new ObservableCollection<VdtTtDeNghiThanhToanNSQPImportModel>(data);
            if (lstError.Any())
            {
                string sMessError = string.Join(Environment.NewLine, lstError);
                System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            ValidateChungTuChiTiet();
            OnPropertyChanged(nameof(ItemsThanhToan));
        }

        private void OnResetData()
        {
            FilePath = string.Empty;
            IsSelectedFile = false;
            ItemsThanhToan = null;
            SelectedThanhToan = null;
            SelectedDonVi = null;
            SelectedChuDauTu = null;
            SelectedCoQuanThanhToan = null;

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(ItemsThanhToan));
            OnPropertyChanged(nameof(SelectedThanhToan));
            OnPropertyChanged(nameof(SelectedChuDauTu));
            OnPropertyChanged(nameof(SelectedCoQuanThanhToan));
        }

        private void ShowError(object param)
        {
            var importTabIndex = (ImportTabIndex)((int)param);
            int rowIndex;
            rowIndex = ItemsThanhToan.IndexOf(SelectedThanhToan);
            var errors = _lstErrChungTuChiTiet.Where(x => x.Row == rowIndex).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
            System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public override void OnSave()
        {
            List<string> lstError = new List<string>();
            if (!ValidateForm()) return;
            List<VdtTtDeNghiThanhToan> lstData = new List<VdtTtDeNghiThanhToan>();
            List<VdtTtDeNghiThanhToanKhv> lstDataKHV = new List<VdtTtDeNghiThanhToanKhv>();
            foreach (var item in ItemsThanhToan.Where(n => n.ImportStatus))
            {
                VdtTtDeNghiThanhToan data = new VdtTtDeNghiThanhToan();
                data.Id = Guid.NewGuid();
                data.SSoDeNghi = item.SSoDeNghi;
                data.DNgayDeNghi = DateTime.ParseExact(item.SNgayDeNghi, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                data.IIdDonViQuanLyId = Guid.Parse(SelectedDonVi.HiddenValue);
                data.IIdMaDonViQuanLy = SelectedDonVi.ValueItem;
                data.SNguoiLap = _sessionService.Current.Principal;
                data.INamKeHoach = NamKeHoach;
                data.IIdNguonVonId = int.Parse(SelectedNguonVon.ValueItem);
                data.SGhiChu = item.SNoiDung;
                data.ILoaiThanhToan = int.Parse(item.SLoaiDeNghi);
                data.IIdDuAnId = item.IIdDuAnId;
                data.BThanhToanTheoHopDong = Convert.ToBoolean(item.BThanhToanTheoHD);
                //data.IIdHopDongId = item.IIdHopDong;
                if (data.BThanhToanTheoHopDong.HasValue && data.BThanhToanTheoHopDong.Value)
                    data.IIdHopDongId = _lstHopDong.FirstOrDefault(t => t.SSoHopDong.Equals(item.SSoHopDongChiPhi))?.Id;
                else
                    data.IIdChiPhiId = _lstCP.FirstOrDefault(t => t.SMaChiPhi.Equals(item.SSoHopDongChiPhi))?.IIdChiPhi;
                data.iCoQuanThanhToan = int.Parse(SelectedCoQuanThanhToan.ValueItem);
                data.BKhoa = false;
                //data.SSoBangKlht = item.SSoBangKlht;
                if (!string.IsNullOrEmpty(item.DNgayBangKlht))
                    data.DNgayBangKlht = DateTime.ParseExact(item.DNgayBangKlht, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                data.BHoanTraUngTruoc = Convert.ToBoolean(item.BHoanTraUngTruoc);
                if (!string.IsNullOrEmpty(item.FLuyKeGiaTriNghiemThuKlht))
                    data.FLuyKeGiaTriNghiemThuKlht = double.Parse(item.FLuyKeGiaTriNghiemThuKlht, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.SThanhToanTN))
                    data.FGiaTriThanhToanTN = double.Parse(item.SThanhToanTN, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.SThanhToanNN))
                    data.FGiaTriThanhToanNN = double.Parse(item.SThanhToanNN, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.SThuHoiTN))
                    data.FGiaTriThuHoiTN = double.Parse(item.SThuHoiTN, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.SThuHoiNN))
                    data.FGiaTriThuHoiNN = double.Parse(item.SThuHoiNN, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.SThuHoiUTTN))
                    data.FGiaTriThuHoiUngTruocTn = double.Parse(item.SThuHoiUTTN, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.SThuHoiUTNN))
                    data.FGiaTriThuHoiUngTruocNn = double.Parse(item.SThuHoiUTNN, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.SThueGTGT))
                    data.FThueGiaTriGiaTang = double.Parse(item.SThueGTGT, CultureInfo.GetCultureInfo("vi-VN"));
                if (!string.IsNullOrEmpty(item.SChuyenTienBH))
                    data.FChuyenTienBaoHanh = double.Parse(item.SChuyenTienBH, CultureInfo.GetCultureInfo("vi-VN"));
                data.STenDonViThuHuong = item.STenDVTH;
                data.SMaNganHang = item.SMaNH;
                data.SSoTaiKhoanNhaThau = item.SSTK;
                data.DDateCreate = DateTime.Now;
                data.SUserCreate = _sessionService.Current.Principal;
                lstData.Add(data);
                VdtTtDeNghiThanhToanKhv vdtTtDeNghiThanhToanKhv = new VdtTtDeNghiThanhToanKhv();
                vdtTtDeNghiThanhToanKhv.Id = Guid.NewGuid();
                vdtTtDeNghiThanhToanKhv.IIdDeNghiThanhToanId = data.Id;
                if (!string.IsNullOrEmpty(item.SLoaiKHV))
                {
                    vdtTtDeNghiThanhToanKhv.ILoai = int.Parse(item.SLoaiKHV);
                }
                vdtTtDeNghiThanhToanKhv.IIdKeHoachVonId = item.KHVId;
                lstDataKHV.Add(vdtTtDeNghiThanhToanKhv);
            }
            _service.InsertRange(lstData, _sessionService.Current.Principal);
            _vdtKhvPhanBoVonService.AddRange(lstDataKHV);
            System.Windows.MessageBox.Show(Resources.MsgSaveDone);
            SavedAction.Invoke(null);
        }
        #endregion

        #region Helper
        private bool ValidateForm()
        {
            List<string> lstError = new List<string>();
            if (SelectedDonVi == null)
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "đơn vị"));
            if (SelectedChuDauTu == null)
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "chủ đầu tư"));
            if (SelectedCoQuanThanhToan == null)
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "cơ quan thanh toán"));
            if (SelectedNguonVon == null)
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "nguồn vốn"));
            if (lstError.Count != 0)
            {
                System.Windows.MessageBox.Show(string.Join(Environment.NewLine, lstError), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void ValidateChungTuChiTiet()
        {
            DateTime dDateOut;
            lstSoDeNghi = new List<string>();
            for (int i = 0; i < ItemsThanhToan.Count; ++i)
            {
                var item = ItemsThanhToan[i];
                bool bIsHaveHopDong = true;

                if (string.IsNullOrEmpty(item.SSoDeNghi))
                {
                    _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                    {
                        ColumnName = "Số đề nghị",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "Số đề nghị"),
                        Row = i
                    });
                    item.ImportStatus &= false;
                }
                else
                {
                    if (lstSoDeNghi != null)
                    {
                        if (lstSoDeNghi.Where(n => n == item.SSoDeNghi).Count() <= 1)
                        {
                            lstSoDeNghi.Add(item.SSoDeNghi);
                        }
                        else
                        {
                            _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                            {
                                ColumnName = "Số đề nghị",
                                Error = string.Format(Resources.MsgDuplicateName, "Số đề nghị"),
                                Row = i
                            });
                            item.ImportStatus &= false;
                        }
                    }
                }

                if (string.IsNullOrEmpty(item.SNgayDeNghi))
                {
                    _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                    {
                        ColumnName = "Ngày đề nghị",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "Ngày đề nghị"),
                        Row = i
                    });
                    item.ImportStatus &= false;
                    bIsHaveHopDong = false;
                }
                if (!DateTime.TryParseExact(item.SNgayDeNghi, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"), DateTimeStyles.None, out dDateOut))
                {
                    _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                    {
                        ColumnName = "Ngày đề nghị",
                        Error = string.Format(Resources.MsgErrorFormat, "Ngày đề nghị"),
                        Row = i
                    });
                    item.ImportStatus &= false;
                    bIsHaveHopDong = false;
                }

                if (!_dicDuAn.ContainsKey(item.SMaDuAn))
                {
                    _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                    {
                        ColumnName = "Mã dự án",
                        Error = Resources.MsgErrorProjectNotFound,
                        Row = i
                    });
                    item.ImportStatus &= false;
                    bIsHaveHopDong = false;
                }
                else
                {
                    item.IIdDuAnId = _dicDuAn[item.SMaDuAn];
                }

                if (_dicDuAn.ContainsKey(item.SMaDuAn))
                {
                    item.IIdDuAnId = _dicDuAn[item.SMaDuAn];
                    var DNgayDeNghi = DateTime.ParseExact(item.SNgayDeNghi, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                    List<KeHoachVonQuery> list = _vdtKhvPhanBoVonService.GetKeHoachVonCapPhatThanhToan(
                        item.IIdDuAnId.ToString(),
                        int.Parse(SelectedNguonVon.ValueItem),
                        DNgayDeNghi,
                        NamKeHoach,
                        int.Parse(SelectedCoQuanThanhToan.ValueItem),
                        Guid.Empty);
                    if (!list.Select(t => t.sSoQuyetDinh).Contains(item.SKHV))
                    {
                        _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                        {
                            ColumnName = "Kế hoạch vốn",
                            Error = string.Format(Resources.MsgErrorDataEmpty, "Kế hoạch vốn"),
                            Row = i
                        });
                        item.ImportStatus &= false;
                    }
                    else
                    {
                        item.KHVId = list.FirstOrDefault(t => t.sSoQuyetDinh.Equals(item.SKHV))?.Id;
                    }
                }
                /*if (string.IsNullOrEmpty(item.SSoHopDongChiPhi))
                {
                    _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                    {
                        ColumnName = "Số hợp đồng",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "Số hợp đồng"),
                        Row = i
                    });
                    item.ImportStatus &= false;
                }
                else if (bIsHaveHopDong)
                {
                    if (_lstHopDong == null || (_lstHopDong != null && !_lstHopDong.Any(n => n.DNgayHopDong.Date <= dDateOut.Date && n.IIdDuAnId == item.IIdDuAnId && n.SSoHopDong == item.SSoHopDongChiPhi)))
                    {
                        _lstErrChungTuChiTiet.Add(new ImportErrorItem()
                        {
                            ColumnName = "số hợp đồng",
                            Error = string.Format(Resources.MsgErrorItemNotFound, "số hợp đồng"),
                            Row = i
                        });
                        item.ImportStatus &= false;
                    }
                    else
                    {
                        if (_lstHopDong.Any(n => n.DNgayHopDong <= dDateOut && n.IIdDuAnId == item.IIdDuAnId && n.SSoHopDong == item.SSoHopDongChiPhi))
                            item.IIdHopDong = _lstHopDong.FirstOrDefault(n => n.DNgayHopDong <= dDateOut && n.IIdDuAnId == item.IIdDuAnId && n.SSoHopDong == item.SSoHopDongChiPhi).Id;
                    }
                }*/
            }
            OnPropertyChanged(nameof(ItemsThanhToan));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private List<VdtTtDeNghiThanhToanNSQPImportModel> GetDataImportByFileType()
        {
            XlsFile xls = new XlsFile(false);
            xls.Open(FilePath);
            xls.ActiveSheet = 1;

            var lstResults = _importService.ProcessData<VdtTtDeNghiThanhToanNSQPImportModel>(FilePath);
            if (lstResults.ImportErrors.Any())
            {
                _lstErrChungTuChiTiet.AddRange(lstResults.ImportErrors);
            }
            return _mapper.Map<List<VdtTtDeNghiThanhToanNSQPImportModel>>(lstResults.Data);
        }

        private void LoadDropDownNguonVon()
        {
            var lstData = _nguonVonService.FindAll().Select(n => new ComboboxItem()
            {
                DisplayItem = n.STen,
                ValueItem = n.IIdMaNguonNganSach.ToString()
            });
            ItemsNguonVon = new ObservableCollection<ComboboxItem>(lstData);
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        private void LoadDropDownDonVi()
        {
            List<DonVi> donViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                    .Where(n => _lstDonViExclude.Contains(n.Loai)).ToList();
            List<NguoiDungDonVi> listNguoiDungDonVi = GetListNguoiDungDonVi();
            if (listNguoiDungDonVi != null && listNguoiDungDonVi.Count > 0)
            {
                donViData = donViData.Where(n => listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(n.IIDMaDonVi)).ToList();
            }
            else
            {
                donViData = new List<DonVi>();
            }
            var cbxLoaiDonViData = donViData.Select(n => new ComboboxItem()
            {
                ValueItem = n.IIDMaDonVi,
                DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi),
                HiddenValue = n.Id.ToString()
            });
            ItemsDonVi = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadDropDownChuDauTu()
        {
            if (SelectedDonVi == null)
            {
                ItemsChuDauTu = new ObservableCollection<ComboboxItem>();
                SelectedChuDauTu = null;
                return;
            }
            ItemsChuDauTu = new ObservableCollection<ComboboxItem>();
            DmChuDauTu parent = _chuDauTuService.FindByMaDonVi(SelectedDonVi.ValueItem, _sessionService.Current.YearOfWork);
            List<DmChuDauTu> result = new List<DmChuDauTu>();
            if (parent != null)
            {
                result.Add(parent);
                GetChildChuDauTu(parent.Id, ref result);
            }
            foreach (DmChuDauTu item in result)
            {
                ItemsChuDauTu.Add(new ComboboxItem { ValueItem = item.Id.ToString(), DisplayItem = string.Format("{0}-{1}", item.IIDMaDonVi, item.STenDonVi), HiddenValue = item.IIDMaDonVi });
            }
            if (ItemsChuDauTu != null && ItemsChuDauTu.Count > 0)
            {
                SelectedChuDauTu = ItemsChuDauTu.FirstOrDefault();
            }

            OnPropertyChanged(nameof(ItemsChuDauTu));
            OnPropertyChanged(nameof(SelectedChuDauTu));
        }

        private void LoadDropDownLoaiThanhToan()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem() { DisplayItem = PaymentTypeEnum.TypeName.THANH_TOAN, ValueItem = "1" });
            lstData.Add(new ComboboxItem() { DisplayItem = PaymentTypeEnum.TypeName.TAM_UNG, ValueItem = "2" });
            ItemsLoaiThanhToan = new ObservableCollection<ComboboxItem>(lstData);
            OnPropertyChanged(nameof(ItemsLoaiThanhToan));
        }

        private void LoadDropDownCoQuanThanhToan()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem() { DisplayItem = CoQuanThanhToanEnum.TypeName.CQTC, ValueItem = ((int)CoQuanThanhToanEnum.Type.CQTC).ToString() });
            lstData.Add(new ComboboxItem() { DisplayItem = CoQuanThanhToanEnum.TypeName.KHO_BAC, ValueItem = ((int)CoQuanThanhToanEnum.Type.KHO_BAC).ToString() });
            ItemsCoQuanThanhToan = new ObservableCollection<ComboboxItem>(lstData);
            OnPropertyChanged(nameof(ItemsCoQuanThanhToan));
        }

        private void GetChildChuDauTu(Guid id, ref List<DmChuDauTu> result)
        {
            DmChuDauTu parent = _chuDauTuService.FindByParentId(id, _sessionService.Current.YearOfWork);
            if (parent == null)
            {
                return;
            }
            else
            {
                if (result.Count == 0)
                {
                    result.Add(parent);
                    GetChildChuDauTu(parent.Id, ref result);
                }
                else
                {
                    DmChuDauTu check = result.Where(n => n.Id == parent.Id).FirstOrDefault();
                    if (check == null)
                    {
                        result.Add(parent);
                        GetChildChuDauTu(parent.Id, ref result);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private List<NguoiDungDonVi> GetListNguoiDungDonVi()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.IIDMaNguoiDung.Equals(_sessionService.Current.Principal));
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            List<NguoiDungDonVi> nsDungDonVis = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return nsDungDonVis;
        }

        private void LoadDropDownDuAn()
        {
            _dicDuAn = new Dictionary<string, Guid>();
            if (SelectedChuDauTu == null)
            {
                ItemsDuAn = new ObservableCollection<ComboboxItem>();
                return;
            }
            ItemsDuAn = new ObservableCollection<ComboboxItem>();
            List<VdtDaDuAn> list = _duAnService.FindByChuDauTuByMaChuDauTu(SelectedChuDauTu.HiddenValue);
            foreach (VdtDaDuAn item in list)
            {
                if (!_dicDuAn.ContainsKey(item.SMaDuAn))
                    _dicDuAn.Add(item.SMaDuAn, item.Id);
            }
        }

        private void LoadAllListHopDong()
        {
            var predicate = PredicateBuilder.True<VdtDaTtHopDong>();
            _lstHopDong = _vdtDaTtHopDongService.FindAll(predicate).ToList();
            _lstCP = _vdtDmChiPhiService.FindAll().ToList();
        }
        #endregion
    }
}
