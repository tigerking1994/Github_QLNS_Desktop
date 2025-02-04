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
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.Report
{
    public class AllocationReportUnitsCompareViewModel : ViewModelBase
    {
        private readonly INsDonViService _donViService;
        private readonly ICpChungTuService _chungTuService;
        private readonly ISessionService _sessionService;
        private readonly IExportService _exportService;
        private readonly IMucLucNganSachService _mucLucNganSachService;
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
        private List<ReportCapPhatSoSanhLnsQuery> _reportDataLns;
        private SessionInfo _sessionInfo;
        private string _diaDiem;
        private bool _isPDF;
        private bool _isCapPhatToanDonVi;
        private double _tongDuToan;
        private double _tongCapPhat;
        private double _tongConLai;

        public override string Title => "BÁO CÁO TỔNG HỢP SỐ CẤP PHÁT: MLNS DỌC - ĐƠN VỊ NGANG";
        public override string Description => "Chọn thông số in báo cáo";
        public override Type ContentType => typeof(View.Budget.Allocation.Report.AllocationReportCompare);
        public bool IsExportEnable => ListDonVi != null && ListDonVi.Any(x => x.IsChecked);
        public bool IsShowRadioLoaiChungTu => !_isCapPhatToanDonVi && _sessionService.Current.IsQuanLyDonViCha;

        private const string Header = "Trong đó";
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

        private ObservableCollection<ComboboxItem> _reportLevel = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ReportLevel
        {
            get => _reportLevel;
            set => SetProperty(ref _reportLevel, value);
        }

        private ComboboxItem _selectedReportLevel;
        public ComboboxItem SelectedReportLevel
        {
            get => _selectedReportLevel;
            set
            {
                SetProperty(ref _selectedReportLevel, value);
            }
        }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand ShowPopupPrintCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public AllocationReportUnitsCompareViewModel(
            INsDonViService donViService,
            ICpChungTuService chungTuService,
            ISessionService sessionService,
            ILog logger,
            IDanhMucService danhMucService,
            IMapper mapper,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            IMucLucNganSachService mucLucNganSachService,
            INsNguoiDungDonViService nguoiDungDonViService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _donViService = donViService;
            _chungTuService = chungTuService;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _mucLucNganSachService = mucLucNganSachService;
            _dmChuKyService = dmChuKyService;
            _mapper = mapper;
            _logger = logger;
            _exportService = exportService;
            _nguoiDungDonViService = nguoiDungDonViService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;


            PrintPDFCommand = new RelayCommand(o => PrintFile(true));
            PrintExcelCommand = new RelayCommand(o => PrintFile(false));
            ShowPopupPrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintBrowserCommand = new RelayCommand(o => PrintFile(true));
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
                LoadLoaiKinhPhi();
                LoadKieuGiay();
                LoadDonViTinh();
                LoadToiDotCap();
                LoadReportLevel();
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
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_CAPPHAT_MLNS_DONVINGANG) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
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

        private void LoadReportLevel()
        {
            var typeReport = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Ngành", ValueItem = NSChiTietToi.NGANH.ToString() },
                new ComboboxItem {DisplayItem = "Tiểu tiết mục", ValueItem = NSChiTietToi.TIEU_TIET_MUC.ToString() },
                new ComboboxItem {DisplayItem = "Tiểu mục", ValueItem = NSChiTietToi.TIEU_MUC.ToString() },
                new ComboboxItem {DisplayItem = "Mục", ValueItem = NSChiTietToi.MUC.ToString()},
            };

            ReportLevel = new ObservableCollection<ComboboxItem>(typeReport);
            SelectedReportLevel = ReportLevel.FirstOrDefault();
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

        public void PrintFile(bool isPDF)
        {
            _isPDF = isPDF;
            ExportCapPhatDonVi();
        }

        public void ExportCapPhatDonVi()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CP, GetTemplateName(ExportFileName.RPT_NS_CAPPHAT_DONVI_TO1));
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    List<ExportResult> results = new List<ExportResult>();

                    string lstdv = string.Join(",", ListDonVi.Where(n => n.IsChecked).Select(x => x.ValueItem).ToList());
                    DateTime ngayChungTu = _listChungTuSelected.Where(x => x.NgayChungTu.HasValue).Max(x => x.NgayChungTu.Value);
                    int loaiNganSach = string.IsNullOrEmpty(SelectedLoaiKinhPhi.ValueItem) ? LoaiKinhPhi.QUOC_PHONG_VALUE : int.Parse(SelectedLoaiKinhPhi.ValueItem);
                    int donViTinh = GetDonViTinh();
                    string ctID = string.Join(",", _listChungTuSelected.Select(x => x.Id.ToString()));
                    List<ReportCapPhatDonViQuery> dataExport = new List<ReportCapPhatDonViQuery>();
                    dataExport = _chungTuService.GetDataReportCapPhatDonVi(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, loaiNganSach, lstdv, ctID, ngayChungTu, _sessionInfo.Principal, donViTinh).ToList();

                    List<string> lstDonVi = dataExport.OrderBy(x => x.iID_MaDonVi).Select(x => x.iID_MaDonVi).Distinct().ToList();
                    List<string> lstXauNoiMa = dataExport.Select(x => x.sXauNoiMa).Distinct().ToList();
                    int pageSize = 3;
                    var listDonViSplit = new List<List<string>>();
                    if (_isPDF)
                    {
                        listDonViSplit = SplitList(lstDonVi, pageSize).ToList();
                    }
                    else
                    {
                        listDonViSplit.Add(lstDonVi);
                    }

                    if (dataExport.Count() > 0)
                    {
                        for (int i = 0; i < listDonViSplit.Count; i++)
                        {
                            //Header
                            List<DonVi> dvNguoiDung = _donViService.FindAll().Where(x => x.NamLamViec == _sessionInfo.YearOfWork).ToList();
                            List<HeaderReportCapPhatDonViQuery> lstHeader = new List<HeaderReportCapPhatDonViQuery>();
                            int stt = 1;
                            foreach (var dv in listDonViSplit[i])
                            {
                                HeaderReportCapPhatDonViQuery header = new HeaderReportCapPhatDonViQuery();
                                header.Stt = stt;
                                header.sMoTa = dvNguoiDung.Where(x => x.IIDMaDonVi == dv).First().TenDonVi;
                                header.TongSo = dataExport.Where(x => x.iID_MaDonVi == dv).Sum(x => x.fCapPhat);
                                lstHeader.Add(header);
                                stt++;
                            }

                            List<HeaderReportCapPhatDonViQuery> lstTongCong = new List<HeaderReportCapPhatDonViQuery>();
                            int stt_tc = 1;
                            foreach (var dv1 in listDonViSplit[i])
                            {
                                HeaderReportCapPhatDonViQuery tongcong = new HeaderReportCapPhatDonViQuery();
                                tongcong.Stt = stt_tc;
                                tongcong.TongSo = dataExport.Where(x => x.iID_MaDonVi == dv1).Sum(x => x.fCapPhat);
                                lstTongCong.Add(tongcong);
                                stt_tc++;
                            }

                            List<ReportCapPhatDonViQuery> dataPrint = new List<ReportCapPhatDonViQuery>();
                            foreach (var item in lstXauNoiMa)
                            {
                                ReportCapPhatDonViQuery report = new ReportCapPhatDonViQuery();
                                report.iID_MLNS = dataExport.Where(x => x.sXauNoiMa == item).First().iID_MLNS;
                                report.iID_MLNS_Cha = dataExport.Where(x => x.sXauNoiMa == item).First().iID_MLNS_Cha;
                                report.sLNS = dataExport.Where(x => x.sXauNoiMa == item).First().sLNS;
                                report.sL = dataExport.Where(x => x.sXauNoiMa == item).First().sL;
                                report.sK = dataExport.Where(x => x.sXauNoiMa == item).First().sK;
                                report.sM = dataExport.Where(x => x.sXauNoiMa == item).First().sM;
                                report.sTM = dataExport.Where(x => x.sXauNoiMa == item).First().sTM;
                                report.sTTM = dataExport.Where(x => x.sXauNoiMa == item).First().sTTM;
                                report.sNG = dataExport.Where(x => x.sXauNoiMa == item).First().sNG;
                                report.sMoTa = dataExport.Where(x => x.sXauNoiMa == item).First().sMoTa;
                                report.bHangCha = dataExport.Where(x => x.sXauNoiMa == item).First().bHangCha;
                                report.fCapPhat = dataExport.Where(x => x.sXauNoiMa == item).Sum(x => x.fCapPhat);
                                report.sXauNoiMa = item;
                                List<CapPhatDonViDynamicModel> lstCapPhat = new List<CapPhatDonViDynamicModel>();
                                foreach (var dv in listDonViSplit[i])
                                {
                                    CapPhatDonViDynamicModel capphat = new CapPhatDonViDynamicModel();
                                    capphat.iID_MaDonVi = dv; ;
                                    capphat.TongSo = dataExport.Where(x => x.sXauNoiMa == item && x.iID_MaDonVi == dv).Sum(x => x.fCapPhat);
                                    lstCapPhat.Add(capphat);
                                }
                                report.LstGiaTri = lstCapPhat;
                                dataPrint.Add(report);
                            }

                            //Get danh mục cha
                            List<ReportCapPhatDonViQuery> lstReport = new List<ReportCapPhatDonViQuery>();
                            GetListBaoCao(lstReport, dataPrint);
                            CaculataData(lstReport);

                            lstReport = lstReport.OrderBy(x => x.sXauNoiMa).ToList();
                            for (int index = lstReport.Count() - 1; index > 0; --index)
                            {
                                Guid? iIdparent = lstReport[index].iID_MLNS_Cha;
                                if (!iIdparent.HasValue) continue;
                                var objParent = lstReport.FirstOrDefault(n => n.iID_MLNS == iIdparent.Value);
                                if (objParent == null) continue;
                                objParent.fCapPhat += lstReport[index].fCapPhat;
                                var j = 0;
                                foreach (var item in lstReport[index].LstGiaTri)
                                {

                                    objParent.LstGiaTri.Add(new CapPhatDonViDynamicModel());
                                    objParent.LstGiaTri[j].TongSo += item.TongSo;
                                    ++j;
                                }
                            }

                            foreach (var item in lstReport)
                            {
                                var parent = lstReport.Where(x => x.iID_MLNS == item.iID_MLNS_Cha).LastOrDefault();
                                if (parent != null)
                                {

                                    if (!string.IsNullOrEmpty(item.sM))
                                    {
                                        item.sLNS = String.Empty;
                                    }
                                    if (!string.IsNullOrEmpty(item.sTM))
                                    {
                                        item.sM = String.Empty;
                                    }
                                    if (!string.IsNullOrEmpty(item.sTTM))
                                    {
                                        item.sTM = String.Empty;
                                        item.sM = String.Empty;
                                    }
                                    if (!string.IsNullOrEmpty(item.sNG))
                                    {
                                        item.sM = String.Empty;
                                        item.sTM = String.Empty;
                                    }
                                    if (String.IsNullOrEmpty(parent.sL) && String.IsNullOrEmpty(parent.sK))
                                    {
                                        if (!String.IsNullOrEmpty(item.sL) && !String.IsNullOrEmpty(item.sK))
                                            item.sLNS = item.sL + "-" + item.sK;
                                    }
                                }

                            }

                            double TongCong = lstReport.Where(x => !x.bHangCha).Sum(x => x.fCapPhat);

                            List<int> columnHidden = new List<int>();
                            if (SelectedReportLevel != null && Convert.ToInt16(SelectedReportLevel.ValueItem) == NSChiTietToi.TIEU_TIET_MUC)
                            {
                                lstReport = lstReport.Where(x => !string.IsNullOrEmpty(x.sLNS) || !string.IsNullOrEmpty(x.sM) || !string.IsNullOrEmpty(x.sTM) || !string.IsNullOrEmpty(x.sTTM)).ToList();
                                lstReport.Select(x => x.bHangCha = string.IsNullOrEmpty(x.sTTM)).ToList();
                                columnHidden.Add(6);
                            }
                            if (SelectedReportLevel != null && Convert.ToInt16(SelectedReportLevel.ValueItem) == NSChiTietToi.TIEU_MUC)
                            {
                                lstReport = lstReport.Where(x => !string.IsNullOrEmpty(x.sLNS) || !string.IsNullOrEmpty(x.sM) || !string.IsNullOrEmpty(x.sTM)).ToList();
                                lstReport.Select(x => x.bHangCha = string.IsNullOrEmpty(x.sTM)).ToList();
                                columnHidden.Add(6);
                                columnHidden.Add(5);
                            }
                            if (SelectedReportLevel != null && Convert.ToInt16(SelectedReportLevel.ValueItem) == NSChiTietToi.MUC)
                            {
                                lstReport = lstReport.Where(x => !string.IsNullOrEmpty(x.sLNS) || !string.IsNullOrEmpty(x.sM)).ToList();
                                lstReport.Select(x => x.bHangCha = string.IsNullOrEmpty(x.sM)).ToList();
                                columnHidden.Add(6);
                                columnHidden.Add(5);
                                columnHidden.Add(4);
                            }

                            int columnStart = 9;
                            var ColNameStart = GetExcelColumnName(columnStart);
                            int columnEnd = columnStart + (listDonViSplit[i].Any() ? listDonViSplit[i].Count - 1 : 0);
                            var ColNameEnd = GetExcelColumnName(columnEnd);
                            var mergeRange = string.Format("{0}8:{1}8", ColNameStart, ColNameEnd);
                            var lstColHeader = new List<HeaderReportDivisionCurrentBatch>();
                            if (lstReport.Any())
                            {
                                var iNumEmpty = lstReport.FirstOrDefault(x => !x.bHangCha).LstGiaTri.Count;
                                lstColHeader.Insert(0, new HeaderReportDivisionCurrentBatch { SSoQuyetDinh = Header, MergeRange = mergeRange, STT = 1 });
                                for (var iNum = 0; iNum < iNumEmpty - 1; iNum++)
                                {
                                    lstColHeader.Add(new HeaderReportDivisionCurrentBatch());
                                }
                                lstReport.Where(x => x.bHangCha).Select(x =>
                                {
                                    x.LstGiaTri.RemoveRange(iNumEmpty, x.LstGiaTri.Count - iNumEmpty);
                                    return x;
                                }).ToList();
                            }
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, _isPDF ? ExportType.PDF : ExportType.EXCEL);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                            data.Add("Cap2", GetHeader2Report());
                            data.Add("TieuDe1", TieuDe1);
                            data.Add("TieuDe2", TieuDe2);
                            data.Add("TieuDe3", TieuDe3);
                            data.Add("ListData", lstReport);
                            data.Add("LstNganhHeader", lstHeader);
                            data.Add("LstTongCong", lstTongCong);
                            data.Add("TongCong", TongCong);
                            data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                            data.Add("TongCongBangChu", TongCong != null ? StringUtils.NumberToText(TongCong * donViTinh) : "");
                            data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                            data.Add("MergeRange", mergeRange);
                            data.Add("ListHeader", lstColHeader);

                            AddChuKy(data, TypeChuKy.RPT_NS_CAPPHAT_MLNS_DONVINGANG);


                            if (i == 0)
                            {
                                if (_isPDF)
                                {
                                    fileNamePrefix = ExportFileName.RPT_NS_CAPPHAT_DONVI_TO1;
                                }
                                else
                                {
                                    fileNamePrefix = ExportFileName.RPT_NS_CAPPHAT_DONVI_EXCEL;
                                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_CP, GetTemplateName(ExportFileName.RPT_NS_CAPPHAT_DONVI_EXCEL));
                                }

                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                var xlsFile = _exportService.Export<ReportCapPhatDonViQuery, HeaderReportCapPhatDonViQuery, HeaderReportDivisionCurrentBatch>(templateFileName, data, columnHidden);
                                results.Add(new ExportResult(string.Format("rptNS_CapPhat_Mlns_DonVi_{0}", DateTime.Now.ToString("ddMMyyyyHHmmssss")), fileNameWithoutExtension, null, xlsFile));
                            }
                            else
                            {
                                fileNamePrefix = ExportFileName.RPT_NS_CAPPHAT_DONVI_TO2;
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_CP, GetTemplateName(ExportFileName.RPT_NS_CAPPHAT_DONVI_TO2));
                                var xlsFile = _exportService.Export<ReportCapPhatDonViQuery, HeaderReportCapPhatDonViQuery, HeaderReportDivisionCurrentBatch>(templateFileName, data, columnHidden);
                                results.Add(new ExportResult(string.Format("rptNS_CapPhat_Mlns_DonVi_{0}", DateTime.Now.ToString("ddMMyyyyHHmmssss")), fileNameWithoutExtension, null, xlsFile));
                            }


                        }
                    }
                    else
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("TieuDe1", TieuDe1);
                        data.Add("TieuDe2", TieuDe2);
                        data.Add("TieuDe3", TieuDe3);
                        fileNamePrefix = ExportFileName.RPT_NS_CAPPHAT_DONVI_EMPTY;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ReportCapPhatDonViQuery, HeaderReportCapPhatDonViQuery>(templateFileName, data);
                        results.Add(new ExportResult(string.Format("rptNS_DuToan_QdCongKhaiNganSach_{0}", lstdv), fileNameWithoutExtension, null, xlsFile));
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

        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }


        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 6)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        private void GetListBaoCao(List<ReportCapPhatDonViQuery> lstReport, List<ReportCapPhatDonViQuery> lstData)
        {
            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            predicate = predicate.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
            List<NsMucLucNganSach> lstMucLucNS = _mucLucNganSachService.FinByCondition(predicate).ToList();
            foreach (var item in lstData)
            {
                lstReport.Add(item);
                AddListParent(item, lstReport, lstMucLucNS);
            }
        }

        public void AddListParent(ReportCapPhatDonViQuery data, List<ReportCapPhatDonViQuery> lstReport, List<NsMucLucNganSach> lstMucLucNS)
        {
            var parent = lstMucLucNS.Where(x => x.MlnsId == data.iID_MLNS_Cha).FirstOrDefault();
            if (parent != null)
            {
                if (!lstReport.Any(x => x.iID_MLNS == parent.MlnsId))
                {
                    ReportCapPhatDonViQuery report = new ReportCapPhatDonViQuery
                    {
                        iID_MLNS = parent.MlnsId,
                        iID_MLNS_Cha = parent.MlnsIdParent,
                        sLNS = parent.Lns,
                        sL = parent.L,
                        sK = parent.K,
                        sM = parent.M,
                        sTM = parent.Tm,
                        sXauNoiMa = parent.XauNoiMa,
                        sTTM = parent.Ttm,
                        sNG = parent.Ng,
                        sMoTa = parent.MoTa,
                        bHangCha = parent.BHangCha,
                        fCapPhat = 0,
                        LstGiaTri = new List<CapPhatDonViDynamicModel>()
                    };
                    lstReport.Add(report);
                    AddListParent(report, lstReport, lstMucLucNS);
                }
            }

        }

        public void CaculataData(List<ReportCapPhatDonViQuery> lstReport)
        {
            foreach (var item in lstReport.Where(x => !x.bHangCha))
            {
                CalculateParentNew(item, lstReport);
            }
        }

        private double CalculateParentNew(ReportCapPhatDonViQuery currentItem, List<ReportCapPhatDonViQuery> lstData)
        {
            double fTong = currentItem.fCapPhat;
            if (lstData.Any(n => n.iID_MLNS_Cha == currentItem.iID_MLNS))
            {
                fTong = 0;
                foreach (var item in lstData.Where(n => n.iID_MLNS_Cha == currentItem.iID_MLNS))
                {
                    fTong += CalculateParentNew(item, lstData);

                }
            }
            currentItem.fCapPhat = fTong;
            return fTong;
        }

        public string GetTemplateName(string originName)
        {
            if (SelectedKieuGiay.ValueItem == LoaiGiay.NGANG && _isPDF)
            {
                originName = originName + "_landscape.xlsx";
            }
            else
            {
                originName = originName + ".xlsx";
            }
            return originName;
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        private void FormatDisplay()
        {
            foreach (var item in _reportDataLns.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
            {
                var parent = _reportDataLns.Where(x => x.MlnsId == item.MlnsIdCha).LastOrDefault();
                if (parent != null)
                {
                    item.L = string.Empty;
                    item.K = string.Empty;
                    item.LNS = string.Empty;
                    if (!string.IsNullOrEmpty(parent.M))
                        item.M = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TM))
                        item.TM = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TTM))
                        item.TTM = string.Empty;
                    if (!string.IsNullOrEmpty(parent.NG))
                        item.NG = string.Empty;
                }
            }
        }

        private string GetLevelCapPhat()
        {
            if (_listChungTuSelected == null || _listChungTuSelected.Count == 0)
                return string.Empty;
            if (_listChungTuSelected.Any(n => n.ChiTietToi == NSChiTietToi.DB_VALUE_NGANH))
                return NSChiTietToi.DB_VALUE_NGANH;
            if (_listChungTuSelected.Any(n => n.ChiTietToi == NSChiTietToi.DB_VALUE_TIEU_MUC))
                return NSChiTietToi.DB_VALUE_TIEU_MUC;
            if (_listChungTuSelected.Any(n => n.ChiTietToi == NSChiTietToi.DB_VALUE_MUC))
                return NSChiTietToi.DB_VALUE_MUC;
            return NSChiTietToi.DB_VALUE_NGANH;
        }


        //private void CalculateData()
        //{
        //    _reportDataLns.Where(x => x.BHangCha).Select(x => { x.DuToan = 0; x.CapPhat = 0; return x; }).ToList();
        //    foreach (var item in _reportDataLns.Where(x => (x.DuToan != 0 || x.CapPhat != 0)))
        //    {
        //        CalculateParent(item, item);
        //    }
        //}

        private void CalculateParent(ReportCapPhatSoSanhLnsQuery currentItem, ReportCapPhatSoSanhLnsQuery selfItem)
        {
            var parentItem = _reportDataLns.Where(x => x.MlnsId == currentItem.MlnsIdCha).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.DuToan += selfItem.DuToan;
            parentItem.CapPhat += selfItem.CapPhat;
            CalculateParent(parentItem, selfItem);
        }


        private void CalculateParentLns(ReportCapPhatSoSanhLnsQuery currentItem, ReportCapPhatSoSanhLnsQuery selfItem)
        {
            var parentItem = _reportDataLns.Where(x => x.MlnsId == currentItem.MlnsIdCha).FirstOrDefault();
            if (parentItem == null) return;
            if (string.IsNullOrEmpty(parentItem.MaDonVi))
            {
                parentItem.DuToan += selfItem.DuToan;
                parentItem.CapPhat += selfItem.CapPhat;
            }
            CalculateParentLns(parentItem, selfItem);
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

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_CAPPHAT_MLNS_DONVINGANG) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_CAPPHAT_MLNS_DONVINGANG;
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
    }
}
