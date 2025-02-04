using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Budget.Allocation.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.PrintReport
{
    public class PrintAllocationTypeViewModel : ViewModelBase
    {
        private readonly INsDonViService _donViService;
        private readonly ICpChungTuService _chungTuService;
        private readonly ISessionService _sessionService;
        private readonly IExportService _exportService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsNguoiDungDonViService _nguoiDungDonViService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private ICollectionView _listDonViView;
        private DmChuKy _dmChuKy;
        private List<CpChungTuQuery> _listChungTu;
        private List<CpChungTuQuery> _listChungTuToiDotCap;
        private List<CpChungTuQuery> _listChungTuSelected;
        private List<ReportCapPhatLoaiCapLnsQuery> _reportDataLns;
        private SessionInfo _sessionInfo;
        private string _diaDiem;
        private bool _isCapPhatToanDonVi;
        private bool _isPDF;
        private double _tongCapUng;
        private double _tongCapThanhKhoan;
        private double _tongCapHopThuc;
        private double _tongCapThu;

        public override string Title => "BÁO CÁO TỔNG HỢP CẤP PHÁT THEO LOẠI CẤP";
        public override string Description => "Chọn thông số in báo cáo";
        public override Type ContentType => typeof(PrintAllocationType);
        public bool IsExportEnable => ListDonVi != null && ListDonVi.Any(x => x.IsChecked);
        public bool IsShowRadioLoaiChungTu => !_isCapPhatToanDonVi && _sessionService.Current.IsQuanLyDonViCha;

        private LoaiChungTu _loaiChungTuValue;
        public LoaiChungTu LoaiChungTuValue
        {
            get => _loaiChungTuValue;
            set
            {
                if (SetProperty(ref _loaiChungTuValue, value))
                    LoadToiDotCap();
            }
        }

        private ObservableCollection<ComboboxItem> _dataToiDotCap;
        public ObservableCollection<ComboboxItem> DataToiDotCap
        {
            get => _dataToiDotCap;
            set => SetProperty(ref _dataToiDotCap, value);
        }

        private ComboboxItem _selectedToiDotCap;
        public ComboboxItem SelectedToiDotCap
        {
            get => _selectedToiDotCap;
            set
            {
                SetProperty(ref _selectedToiDotCap, value);
                TieuDe2 = string.Format("(Số liệu chỉ tiêu - cấp phát tới đợt ngày: {0})", _selectedToiDotCap != null ? _selectedToiDotCap.HiddenValue : string.Empty);
                LoadDonVi();
                GetMota();
            }
        }

        private string _moTa;
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private string _tieuDe1;
        public string TieuDe1
        {
            get => _tieuDe1;
            set => SetProperty(ref _tieuDe1, value);
        }

        private string _tieuDe2;
        public string TieuDe2
        {
            get => _tieuDe2;
            set => SetProperty(ref _tieuDe2, value);
        }

        private string _tieuDe3;
        public string TieuDe3
        {
            get => _tieuDe3;
            set => SetProperty(ref _tieuDe3, value);
        }

        private ObservableCollection<ComboboxItem> _dataLoaiBaoCao;
        public ObservableCollection<ComboboxItem> DataLoaiBaoCao
        {
            get => _dataLoaiBaoCao;
            set => SetProperty(ref _dataLoaiBaoCao, value);
        }

        private ComboboxItem _selectedLoaiBaoCao;
        public ComboboxItem SelectedLoaiBaoCao
        {
            get => _selectedLoaiBaoCao;
            set => SetProperty(ref _selectedLoaiBaoCao, value);
        }

        private ObservableCollection<ComboboxItem> _dataLoaiKinhPhi;
        public ObservableCollection<ComboboxItem> DataLoaiKinhPhi
        {
            get => _dataLoaiKinhPhi;
            set => SetProperty(ref _dataLoaiKinhPhi, value);
        }

        private ComboboxItem _selectedLoaiKinhPhi;
        public ComboboxItem SelectedLoaiKinhPhi
        {
            get => _selectedLoaiKinhPhi;
            set => SetProperty(ref _selectedLoaiKinhPhi, value);
        }

        private ObservableCollection<ComboboxItem> _dataKieuGiay;
        public ObservableCollection<ComboboxItem> DataKieuGiay
        {
            get => _dataKieuGiay;
            set => SetProperty(ref _dataKieuGiay, value);
        }

        private ComboboxItem _selectedKieuGiay;
        public ComboboxItem SelectedKieuGiay
        {
            get => _selectedKieuGiay;
            set => SetProperty(ref _selectedKieuGiay, value);
        }

        private ObservableCollection<CheckBoxItem> _listDonVi;
        public ObservableCollection<CheckBoxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }

        private bool? _selectAllDonVi;
        public bool? SelectAllDonVi
        {
            get
            {
                if (ListDonVi != null)
                {
                    var selected = ListDonVi.Where(x => x.IsFilter).Select(item => item.IsChecked).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, ListDonVi);
                    OnPropertyChanged();
                }
            }
        }

        private static void SelectAll(bool select, ObservableCollection<CheckBoxItem> models)
        {
            foreach (var model in models)
            {
                model.IsChecked = select;
            }
        }

        public string SelectedCountDonVi
        {
            get
            {
                int totalCount = ListDonVi != null ? ListDonVi.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = ListDonVi != null ? ListDonVi.Count(item => item.IsChecked) : 0;
                return string.Format("ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value))
                {
                    _listDonViView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountDonVi));
                    OnPropertyChanged(nameof(SelectAllDonVi));
                }
            }
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private ObservableCollection<ComboboxItem> _DataDonViTinh;
        public ObservableCollection<ComboboxItem> DataDonViTinh
        {
            get => _DataDonViTinh;
            set => SetProperty(ref _DataDonViTinh, value);
        }

        private ComboboxItem _selectedDonViTinh;
        public ComboboxItem SelectedDonViTinh
        {
            get => _selectedDonViTinh;
            set => SetProperty(ref _selectedDonViTinh, value);
        }

        public List<DonViNgayChungTuQuery> ListDonViByNgayChungTu { get; set; }

        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand ShowPopupPrintCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintAllocationTypeViewModel(
            INsDonViService donViService,
            ICpChungTuService chungTuService,
            ISessionService sessionService,
            ILog logger,
            IDanhMucService danhMucService,
            IMapper mapper,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            INsNguoiDungDonViService nguoiDungDonViService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _donViService = donViService;
            _chungTuService = chungTuService;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _mapper = mapper;
            _logger = logger;
            _exportService = exportService;
            _nguoiDungDonViService = nguoiDungDonViService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            PrintPDFCommand = new RelayCommand(o => PrintFile(true));
            PrintExcelCommand = new RelayCommand(o => PrintFile(false));
            ShowPopupPrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            try
            {
                _sessionInfo = _sessionService.Current;
                InitReportDefaultDate();
                LoadSettingCapPhat();
                if (!_isCapPhatToanDonVi)
                {
                    LoaiChungTuValue = LoaiChungTu.TONG_HOP;
                }
                LoadDanhMuc();
                LoadLoaiBaoCao();
                LoadLoaiKinhPhi();
                LoadKieuGiay();
                LoadDonViTinh();
                LoadToiDotCap();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadSettingCapPhat()
        {
            DanhMuc dmCapPhatToanDonVi = _danhMucService.FindByCode(MaDanhMuc.CAP_PHAT_TOAN_DON_VI);
            if (dmCapPhatToanDonVi != null)
                bool.TryParse(dmCapPhatToanDonVi.SGiaTri, out _isCapPhatToanDonVi);
            else _isCapPhatToanDonVi = false;
        }


        private void LoadDanhMuc()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_CAPPHAT_LOAICAP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TieuDe1 = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
            TieuDe2 = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
            TieuDe3 = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionInfo.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void LoadDonViTinh()
        {
            DataDonViTinh = new ObservableCollection<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh == null || listDonViTinh.Count <= 0)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            foreach (var dvt in listDonViTinh)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = dvt.SGiaTri.ToString(), DisplayItem = dvt.STen });
            }
            SelectedDonViTinh = DataDonViTinh.FirstOrDefault();
        }

        public int GetDonViTinh()
        {
            if (SelectedDonViTinh == null || string.IsNullOrEmpty(SelectedDonViTinh.ValueItem))
                return 1;
            return int.Parse(SelectedDonViTinh.ValueItem);
        }

        private void LoadKieuGiay()
        {
            DataKieuGiay = new ObservableCollection<ComboboxItem>();
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.DOC, DisplayItem = LoaiGiay.DOC });
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.NGANG, DisplayItem = LoaiGiay.NGANG });
            SelectedKieuGiay = DataKieuGiay.FirstOrDefault();
        }

        private void LoadLoaiKinhPhi()
        {
            DataLoaiKinhPhi = new ObservableCollection<ComboboxItem>();
            DataLoaiKinhPhi.Add(new ComboboxItem
            {
                ValueItem = LoaiKinhPhi.TAT_CA_VALUE.ToString(),
                DisplayItem = LoaiKinhPhi.TAT_CA_VALUE.ToString() + " - " + LoaiKinhPhi.TAT_CA_DISPLAY
            });
            DataLoaiKinhPhi.Add(new ComboboxItem
            {
                ValueItem = LoaiKinhPhi.QUOC_PHONG_VALUE.ToString(),
                DisplayItem = LoaiKinhPhi.QUOC_PHONG_VALUE.ToString() + " - " + LoaiKinhPhi.QUOC_PHONG_DISPLAY
            });
            DataLoaiKinhPhi.Add(new ComboboxItem
            {
                ValueItem = LoaiKinhPhi.NHA_NUOC_VALUE.ToString(),
                DisplayItem = LoaiKinhPhi.NHA_NUOC_VALUE.ToString() + " - " + LoaiKinhPhi.NHA_NUOC_DISPLAY
            });
            DataLoaiKinhPhi.Add(new ComboboxItem
            {
                ValueItem = LoaiKinhPhi.DAC_BIET_VALUE.ToString(),
                DisplayItem = LoaiKinhPhi.DAC_BIET_VALUE.ToString() + " - " + LoaiKinhPhi.DAC_BIET_DISPLAY
            });
            DataLoaiKinhPhi.Add(new ComboboxItem
            {
                ValueItem = LoaiKinhPhi.KHAC_VALUE.ToString(),
                DisplayItem = LoaiKinhPhi.KHAC_VALUE.ToString() + " - " + LoaiKinhPhi.KHAC_DISPLAY
            });
            SelectedLoaiKinhPhi = DataLoaiKinhPhi.FirstOrDefault();
        }

        private void LoadLoaiBaoCao()
        {
            DataLoaiBaoCao = new ObservableCollection<ComboboxItem>();
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.CHI_TIET_TUNG_DON_VI, DisplayItem = LoaiBaoCao.CHI_TIET_TUNG_DON_VI });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_DON_VI, DisplayItem = LoaiBaoCao.TONG_HOP_DON_VI });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_DON_VI_LNS1, DisplayItem = LoaiBaoCao.TONG_HOP_DON_VI_LNS1 });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_DON_VI_LNS3, DisplayItem = LoaiBaoCao.TONG_HOP_DON_VI_LNS3 });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_DON_VI_LNS, DisplayItem = LoaiBaoCao.TONG_HOP_DON_VI_LNS });
            SelectedLoaiBaoCao = DataLoaiBaoCao.FirstOrDefault();
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_CAPPHAT_LOAICAP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_CAPPHAT_LOAICAP;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TieuDe1 = chuKy.TieuDe1MoTa;
                TieuDe2 = chuKy.TieuDe2MoTa;
                TieuDe3 = chuKy.TieuDe3MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void LoadToiDotCap()
        {
            DataToiDotCap = new ObservableCollection<ComboboxItem>();
            _listChungTu = _chungTuService.FindByCondition(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget,
                                                    _sessionInfo.Principal, _isCapPhatToanDonVi, 1).ToList();
            if (_isCapPhatToanDonVi)
                _listChungTuToiDotCap = _listChungTu.Where(x => x.IsLocked).ToList();
            else
            {
                if (!_sessionService.Current.IsQuanLyDonViCha || (_sessionService.Current.IsQuanLyDonViCha && LoaiChungTuValue == LoaiChungTu.THUONG))
                    _listChungTuToiDotCap = _listChungTu.Where(x => x.IsLocked && string.IsNullOrEmpty(x.DSSoChungTuTongHop)).ToList();
                else _listChungTuToiDotCap = _listChungTu.Where(x => !string.IsNullOrEmpty(x.DSSoChungTuTongHop)).ToList();
            }
            DataToiDotCap = _mapper.Map<ObservableCollection<ComboboxItem>>(_listChungTuToiDotCap);
            if (DataToiDotCap != null && DataToiDotCap.Count > 0)
            {
                SelectedToiDotCap = DataToiDotCap.FirstOrDefault();
            }
            else SelectedToiDotCap = null;
        }

        public void GetMota()
        {
            MoTa = string.Empty;
            if (SelectedToiDotCap != null)
            {
                foreach (var item in _listChungTuSelected)
                {
                    MoTa += string.Format("- {0}({1}): {2}", item.SoChungTu, item.UserCreator, item.MoTa);
                    MoTa += Environment.NewLine;
                }
            }
            OnPropertyChanged(nameof(MoTa));
        }

        private bool ListDonViFilter(object obj)
        {
            bool result = true;
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(_searchDonVi))
                result = item.DisplayItem.ToLower().Contains(_searchDonVi.Trim()!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public void LoadDonVi()
        {
            ListDonVi = new ObservableCollection<CheckBoxItem>();
            if (SelectedToiDotCap == null)
            {
                OnPropertyChanged(nameof(ListDonVi));
                OnPropertyChanged(nameof(SelectedCountDonVi));
                OnPropertyChanged(nameof(IsExportEnable));
                OnPropertyChanged(nameof(SelectAllDonVi));
                return;
            }
            _listChungTuSelected = new List<CpChungTuQuery>();
            var chungTuSelected = _listChungTuToiDotCap.Where(n => n.SoChungTu == SelectedToiDotCap.ValueItem).First();
            var listChungTuLuyKeToiDot = _listChungTuToiDotCap.Where(n => n.SoChungTuIndex <= chungTuSelected.SoChungTuIndex && n.NgayChungTu.HasValue && n.NgayChungTu.Value.Date <= chungTuSelected.NgayChungTu.Value.Date).ToList();
            if (_isCapPhatToanDonVi)
                _listChungTuSelected = listChungTuLuyKeToiDot;
            else
            {
                if (!_sessionService.Current.IsQuanLyDonViCha || (_sessionService.Current.IsQuanLyDonViCha && LoaiChungTuValue == LoaiChungTu.THUONG))
                    _listChungTuSelected = listChungTuLuyKeToiDot;
                else
                {
                    foreach (var chungTu in listChungTuLuyKeToiDot)
                    {
                        _listChungTuSelected.AddRange(_listChungTu.Where(n => chungTu.DSSoChungTuTongHop.Split(",").ToList().Contains(n.SoChungTu)).ToList());
                    }
                }
            }
            List<DonVi> listDonVi = _donViService.FindByCapPhatId(_sessionInfo.YearOfWork, string.Join(",", _listChungTuSelected.Select(x => x.Id))).ToList();
            ListDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonVi);
            _listDonViView = CollectionViewSource.GetDefaultView(ListDonVi);
            _listDonViView.Filter = ListDonViFilter;
            foreach (var model in ListDonVi)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        OnPropertyChanged(nameof(SelectedCountDonVi));
                        OnPropertyChanged(nameof(SelectAllDonVi));
                        OnPropertyChanged(nameof(IsExportEnable));
                    }
                };
            }
            OnPropertyChanged(nameof(ListDonVi));
            OnPropertyChanged(nameof(SelectedCountDonVi));
            OnPropertyChanged(nameof(IsExportEnable));
            OnPropertyChanged(nameof(SelectAllDonVi));
        }

        private void PrintFile(bool isPDF)
        {
            _isPDF = isPDF;
            switch (SelectedLoaiBaoCao.ValueItem)
            {
                case LoaiBaoCao.CHI_TIET_TUNG_DON_VI:
                    ExportCapPhatChiTietDonVi();
                    break;
                case LoaiBaoCao.TONG_HOP_DON_VI:
                    ExportCapPhatTongDonVi();
                    break;
                case LoaiBaoCao.TONG_HOP_DON_VI_LNS:
                case LoaiBaoCao.TONG_HOP_DON_VI_LNS1:
                case LoaiBaoCao.TONG_HOP_DON_VI_LNS3:
                    ExportCapPhatLNS();
                    break;
                default:
                    break;
            }
        }

        public void ExportCapPhatChiTietDonVi()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    int loaiNganSach = string.IsNullOrEmpty(SelectedLoaiKinhPhi.ValueItem) ? LoaiKinhPhi.QUOC_PHONG_VALUE : int.Parse(SelectedLoaiKinhPhi.ValueItem);
                    var listSelectedDonVi = ListDonVi.Where(x => x.IsChecked).ToList();
                    int donViTinh = GetDonViTinh();

                    foreach (var donVi in listSelectedDonVi)
                    {
                        _reportDataLns = _chungTuService.GetDataReportLoaiCap<ReportCapPhatLoaiCapLnsQuery>(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget,
                            loaiNganSach, donVi.ValueItem, string.Join(",", _listChungTuSelected.Select(x => x.Id)), donViTinh, SelectedLoaiBaoCao.ValueItem).ToList();

                        _tongCapUng = _reportDataLns.Sum(x => x.CapUng);
                        _tongCapThanhKhoan = _reportDataLns.Sum(x => x.CapThanhKhoan);
                        _tongCapHopThuc = _reportDataLns.Sum(x => x.CapHopThuc);
                        _tongCapThu = _reportDataLns.Sum(x => x.CapThu);
                        CalculateDataLns();
                        _reportDataLns = _reportDataLns.Where(x => x.CapUng != 0 || x.CapHopThuc != 0 || x.CapThanhKhoan != 0 || x.CapThu != 0).ToList();
                        FormatDisplay();
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, _isPDF ? ExportType.PDF : ExportType.EXCEL);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("TieuDe1", TieuDe1);
                        data.Add("TieuDe2", TieuDe2);
                        data.Add("TieuDe3", TieuDe3);
                        data.Add("DonVi", donVi.DisplayItem);
                        data.Add("TongCapUng", _tongCapUng);
                        data.Add("TongCapThanhKhoan", _tongCapThanhKhoan);
                        data.Add("TongCapHopThuc", _tongCapHopThuc);
                        data.Add("TongCapThu", _tongCapThu);
                        data.Add("Items", _reportDataLns);
                        data.Add("Header1", SelectedDonViTinh.DisplayItem);
                        data.Add("TienCapUng", StringUtils.NumberToText(_tongCapUng * donViTinh, true));
                        data.Add("TienCapThanhKhoan", StringUtils.NumberToText(_tongCapThanhKhoan * donViTinh, true));
                        data.Add("TienCapHopThuc", StringUtils.NumberToText(_tongCapHopThuc * donViTinh, true));
                        data.Add("TienCapThu", StringUtils.NumberToText(_tongCapThu * donViTinh, true));
                        data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                        AddChuKy(data, TypeChuKy.RPT_NS_CAPPHAT_LOAICAP);

                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CP, GetTemplateName(ExportFileName.RPT_NS_CAPPHAT_LOAICAP_CHITIET_DONVI));
                        string fileNamePrefix = ExportFileName.RPT_NS_CAPPHAT_LOAICAP_CHITIET_DONVI;
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix, DateTime.Now.Millisecond.ToString());
                        var xlsFile = _exportService.Export<ReportCapPhatLoaiCapLnsQuery>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, _isPDF ? ExportType.PDF : ExportType.EXCEL);
                    }
                    else
                        _logger.Error(e.Error.Message);
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ExportCapPhatTongDonVi()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    int loaiNganSach = string.IsNullOrEmpty(SelectedLoaiKinhPhi.ValueItem) ? LoaiKinhPhi.QUOC_PHONG_VALUE : int.Parse(SelectedLoaiKinhPhi.ValueItem);
                    string listDonViSelected = CheckboxSelectedToStringConvert.GetValueSelected(ListDonVi);
                    int donViTinh = GetDonViTinh();

                    var reportData = _chungTuService.GetDataReportLoaiCap<ReportCapPhatLoaiCapTongDonViQuery>(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget,
                            loaiNganSach, listDonViSelected, string.Join(",", _listChungTuSelected.Select(x => x.Id)), donViTinh, SelectedLoaiBaoCao.ValueItem).ToList();

                    _tongCapUng = reportData.Sum(x => x.CapUng);
                    _tongCapThanhKhoan = reportData.Sum(x => x.CapThanhKhoan);
                    _tongCapHopThuc = reportData.Sum(x => x.CapHopThuc);
                    _tongCapThu = reportData.Sum(x => x.CapThu);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, _isPDF ? ExportType.PDF : ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("TieuDe1", TieuDe1);
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    data.Add("TongCapUng", _tongCapUng);
                    data.Add("TongCapThanhKhoan", _tongCapThanhKhoan);
                    data.Add("TongCapHopThuc", _tongCapHopThuc);
                    data.Add("TongCapThu", _tongCapThu);
                    data.Add("Items", reportData);
                    data.Add("Header1", SelectedDonViTinh.DisplayItem);
                    data.Add("TienCapUng", StringUtils.NumberToText(_tongCapUng * donViTinh, true));
                    data.Add("TienCapThanhKhoan", StringUtils.NumberToText(_tongCapThanhKhoan * donViTinh, true));
                    data.Add("TienCapHopThuc", StringUtils.NumberToText(_tongCapHopThuc * donViTinh, true));
                    data.Add("TienCapThu", StringUtils.NumberToText(_tongCapThu * donViTinh, true));
                    data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    AddChuKy(data, TypeChuKy.RPT_NS_CAPPHAT_LOAICAP);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CP, GetTemplateName(ExportFileName.RPT_NS_CAPPHAT_LOAICAP_TONGHOP_DONVI));
                    string fileNamePrefix = ExportFileName.RPT_NS_CAPPHAT_LOAICAP_TONGHOP_DONVI;
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportCapPhatLoaiCapTongDonViQuery>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                            _exportService.Open(result, _isPDF ? ExportType.PDF : ExportType.EXCEL);
                    }
                    else
                        _logger.Error(e.Error.Message);
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ExportCapPhatLNS()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    int loaiNganSach = string.IsNullOrEmpty(SelectedLoaiKinhPhi.ValueItem) ? LoaiKinhPhi.QUOC_PHONG_VALUE : int.Parse(SelectedLoaiKinhPhi.ValueItem);
                    string listDonViSelected = CheckboxSelectedToStringConvert.GetValueSelected(ListDonVi);
                    int donViTinh = GetDonViTinh();

                    _reportDataLns = _chungTuService.GetDataReportLoaiCap<ReportCapPhatLoaiCapLnsQuery>(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget,
                            loaiNganSach, listDonViSelected, string.Join(",", _listChungTuSelected.Select(x => x.Id)), donViTinh, SelectedLoaiBaoCao.ValueItem).ToList();

                    _tongCapUng = _reportDataLns.Sum(x => x.CapUng);
                    _tongCapThanhKhoan = _reportDataLns.Sum(x => x.CapThanhKhoan);
                    _tongCapHopThuc = _reportDataLns.Sum(x => x.CapHopThuc);
                    _tongCapThu = _reportDataLns.Sum(x => x.CapThu);
                    CalculateDataLns();
                    var reportData = ProcessDataLNS();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, _isPDF ? ExportType.PDF : ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("TieuDe1", TieuDe1);
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    data.Add("Items", reportData);
                    data.Add("Header1", SelectedDonViTinh.DisplayItem);
                    data.Add("TongCapUng", _tongCapUng);
                    data.Add("TongCapThanhKhoan", _tongCapThanhKhoan);
                    data.Add("TongCapHopThuc", _tongCapHopThuc);
                    data.Add("TongCapThu", _tongCapThu);
                    data.Add("TienCapUng", StringUtils.NumberToText(_tongCapUng * donViTinh, true));
                    data.Add("TienCapThanhKhoan", StringUtils.NumberToText(_tongCapThanhKhoan * donViTinh, true));
                    data.Add("TienCapHopThuc", StringUtils.NumberToText(_tongCapHopThuc * donViTinh, true));
                    data.Add("TienCapThu", StringUtils.NumberToText(_tongCapThu * donViTinh, true));
                    data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    AddChuKy(data, TypeChuKy.RPT_NS_CAPPHAT_SOSANH);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CP, GetTemplateName(ExportFileName.RPT_NS_CAPPHAT_LOAICAP_LNS));
                    string fileNamePrefix = ExportFileName.RPT_NS_CAPPHAT_LOAICAP_LNS;
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportCapPhatLoaiCapLnsQuery>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                            _exportService.Open(result, _isPDF ? ExportType.PDF : ExportType.EXCEL);
                    }
                    else
                        _logger.Error(e.Error.Message);
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateDataLns()
        {
            _reportDataLns.Where(x => string.IsNullOrEmpty(x.MaDonVi)).Select(x => { x.CapThu = 0; x.CapThanhKhoan = 0; x.CapHopThuc = 0; x.CapUng = 0; return x; }).ToList();
            foreach (var item in _reportDataLns.Where(x => x.CapThu != 0 || x.CapThanhKhoan != 0 || x.CapHopThuc != 0 || x.CapUng != 0))
            {
                CalculateParentLns(item, item);
            }
        }

        private void CalculateParentLns(ReportCapPhatLoaiCapLnsQuery currentItem, ReportCapPhatLoaiCapLnsQuery selfItem)
        {
            var parentItem = _reportDataLns.Where(x => x.MlnsId == currentItem.MlnsIdCha).FirstOrDefault();
            if (parentItem == null) return;
            if (string.IsNullOrEmpty(parentItem.MaDonVi))
            {
                parentItem.CapUng += selfItem.CapUng;
                parentItem.CapThanhKhoan += selfItem.CapThanhKhoan;
                parentItem.CapHopThuc += selfItem.CapHopThuc;
                parentItem.CapThu += selfItem.CapThu;
            }
            CalculateParentLns(parentItem, selfItem);
        }

        private List<ReportCapPhatLoaiCapLnsQuery> ProcessDataLNS()
        {
            List<ReportCapPhatLoaiCapLnsQuery> result = new List<ReportCapPhatLoaiCapLnsQuery>();
            _reportDataLns = _reportDataLns.Where(x => x.CapUng != 0 || x.CapThanhKhoan != 0 || x.CapHopThuc != 0 || x.CapThu != 0).ToList();
            foreach (var item in _reportDataLns)
            {
                if ((SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_DON_VI_LNS ||
                    SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_DON_VI_LNS3) && item.LNS.Length <= 3)
                    result.Add(item);
                else if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_DON_VI_LNS1 && item.LNS.Length == 1)
                    result.Add(item);
                else if (item.LNS.Length > 3)
                {
                    if (!result.Any(x => x.LNS == item.LNS))
                    {
                        // add hàng cha
                        if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_DON_VI_LNS)
                        {
                            var model = _reportDataLns.Where(x => x.LNS == item.LNS).FirstOrDefault();
                            if (model != null)
                            {
                                result.Add(new ReportCapPhatLoaiCapLnsQuery
                                {
                                    LNS = model.LNS,
                                    MoTa = model.MoTa,
                                    CapUng = model.CapUng,
                                    CapThanhKhoan = model.CapThanhKhoan,
                                    CapHopThuc = model.CapHopThuc,
                                    CapThu = model.CapThu,
                                    BHangChaLns = true
                                });
                            }
                        }
                        // add hàng đơn vị
                        var listDataByDonVi = _reportDataLns.Where(x => x.LNS == item.LNS && !string.IsNullOrEmpty(x.MaDonVi)).ToList();
                        foreach (var donVi in ListDonVi.Where(x => x.IsChecked))
                        {
                            var data = listDataByDonVi.Where(x => x.MaDonVi == donVi.ValueItem).ToList();
                            if (result.Any(x => x.MaDonVi == donVi.ValueItem) && SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_DON_VI_LNS1)
                            {
                                var row = result.Where(x => x.MaDonVi == donVi.ValueItem).First();
                                row.CapUng += data.Sum(x => x.CapUng);
                                row.CapThanhKhoan += data.Sum(x => x.CapThanhKhoan);
                                row.CapHopThuc += data.Sum(x => x.CapHopThuc);
                                row.CapThu += data.Sum(x => x.CapThu);
                            }
                            else
                                result.Add(new ReportCapPhatLoaiCapLnsQuery
                                {
                                    LNS = item.LNS,
                                    MaDonVi = donVi.ValueItem,
                                    MoTa = donVi.DisplayItem,
                                    CapUng = data.Sum(x => x.CapUng),
                                    CapThanhKhoan = data.Sum(x => x.CapThanhKhoan),
                                    CapHopThuc = data.Sum(x => x.CapHopThuc),
                                    CapThu = data.Sum(x => x.CapThu),
                                });
                        }
                    }
                }
            }
            return result;
        }

        private void FormatDisplay()
        {
            foreach (var item in _reportDataLns.Where(x => !string.IsNullOrEmpty(x.M)))
            {
                var parent = _reportDataLns.Where(x => x.MlnsId == item.MlnsIdCha).FirstOrDefault();
                if (parent != null && parent.LNS != string.Empty)
                {
                    if (item.M != string.Empty)
                    {
                        item.L = string.Empty;
                        item.K = string.Empty;
                        item.LNS = string.Empty;
                    }
                }
            }
            foreach (var item in _reportDataLns.Where(x => !string.IsNullOrEmpty(x.TM)))
            {
                var parent = _reportDataLns.Where(x => x.MlnsId == item.MlnsIdCha).FirstOrDefault();
                if (parent != null && parent.M != string.Empty)
                {
                    item.M = string.Empty;
                    item.L = string.Empty;
                    item.K = string.Empty;
                    item.LNS = string.Empty;
                }
            }
            foreach (var item in _reportDataLns.Where(x => !string.IsNullOrEmpty(x.TTM)))
            {
                var parent = _reportDataLns.Where(x => x.MlnsId == item.MlnsIdCha).FirstOrDefault();
                if (parent != null && parent.TM != string.Empty)
                {
                    item.TM = string.Empty;
                    item.M = string.Empty;
                    item.L = string.Empty;
                    item.K = string.Empty;
                    item.LNS = string.Empty;
                }
            }
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        public string GetTemplateName(string originName)
        {
            if (SelectedKieuGiay.ValueItem == LoaiGiay.NGANG)
            {
                originName = originName + "_landscape.xlsx";
            }
            else
            {
                originName = originName + ".xlsx";
            }
            return originName;
        }

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            // add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy != null ? dmChyKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ChucDanh1", dmChyKy != null ? dmChyKy.ChucDanh1MoTa : string.Empty);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy != null ? dmChyKy.Ten1MoTa : string.Empty);
            data.Add("ThuaLenh2", dmChyKy != null ? dmChyKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ChucDanh2", dmChyKy != null ? dmChyKy.ChucDanh2MoTa : string.Empty);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy != null ? dmChyKy.Ten2MoTa : string.Empty);
            data.Add("ThuaLenh3", dmChyKy != null ? dmChyKy.ThuaLenh3MoTa : string.Empty);
            data.Add("ChucDanh3", dmChyKy != null ? dmChyKy.ChucDanh3MoTa : string.Empty);
            data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten3", dmChyKy != null ? dmChyKy.Ten3MoTa : string.Empty);
        }
    }
}
