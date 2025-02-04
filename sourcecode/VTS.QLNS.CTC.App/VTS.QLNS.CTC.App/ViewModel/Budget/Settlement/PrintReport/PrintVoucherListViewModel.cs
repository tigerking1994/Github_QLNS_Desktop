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
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport
{
    public class PrintVoucherListViewModel : ViewModelBase
    {
        private IExportService _exportService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private INsMucLucNganSachService _mucLucNganSachService;
        private INsBkChungTuChiTietService _chungTuChiTietService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private readonly INsDonViService _iNsDonViService;
        private ILog _logger;
        private IMapper _mapper;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private ICollectionView _donViCollectionView;
        public override string Title => "In bảng kê chứng từ";
        public override string Description => "Chọn loại báo cáo cần in bảng kê chứng từ";
        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintVoucherList);

        private VoucherListModel _voucherList;
        public VoucherListModel VoucherList
        {
            get => _voucherList;
            set => SetProperty(ref _voucherList, value);
        }

        private string _title1;
        public string Title1
        {
            get => _title1;
            set => SetProperty(ref _title1, value);
        }

        private string _title2;
        public string Title2
        {
            get => _title2;
            set => SetProperty(ref _title2, value);
        }
        private string _title3;
        public string Title3
        {
            get => _title3;
            set => SetProperty(ref _title3, value);
        }

        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
        }

        private List<ComboboxItem> _units;
        public List<ComboboxItem> Units
        {
            get => _units;
            set => SetProperty(ref _units, value);
        }

        private ComboboxItem _selectedUnit;
        public ComboboxItem SelectedUnit
        {
            get => _selectedUnit;
            set => SetProperty(ref _selectedUnit, value);
        }

        private List<ComboboxItem> _loaiChis;
        public List<ComboboxItem> LoaiChis
        {
            get => _loaiChis;
            set => SetProperty(ref _loaiChis, value);
        }

        private ComboboxItem _selectedLoaiChi;
        public ComboboxItem SelectedLoaiChi
        {
            get => _selectedLoaiChi;
            set => SetProperty(ref _selectedLoaiChi, value);
        }

        private DateTime? _tuNgay;
        public DateTime? TuNgay
        {
            get => _tuNgay;
            set
            {
                SetProperty(ref _tuNgay, value);
                LoadDonVi();
            }
        }

        private DateTime? _denNgay;
        public DateTime? DenNgay
        {
            get => _denNgay;
            set
            {
                SetProperty(ref _denNgay, value);
                LoadDonVi();
            }
        }

        private ObservableCollection<CheckBoxItem> _listDonVi = new ObservableCollection<CheckBoxItem>();

        public ObservableCollection<CheckBoxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }

        private bool _selectAllDonVi;

        public bool SelectAllDonVi
        {
            get => ListDonVi.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDonVi, value);
                foreach (var item in ListDonVi) item.IsChecked = _selectAllDonVi;
            }
        }

        public string LabelSelectedCountDonVi
        {
            get => $"ĐƠN VỊ ({ListDonVi.Count(item => item.IsChecked)}/{ListDonVi.Count})";
        }

        private string _searchDonVi;

        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value))
                {
                    _donViCollectionView.Refresh();
                }
            }
        }

        public string SThangQuyMoTa { get; set; }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }


        public PrintVoucherListViewModel(ISessionService sessionService,
            INsMucLucNganSachService mucLucNganSachService,
            IExportService exportService,
            INsBkChungTuChiTietService chungTuChiTietService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            INsDonViService iNsDonViService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IMapper mapper,
            ILog logger)
        {
            _sessionService = sessionService;
            _mucLucNganSachService = mucLucNganSachService;
            _exportService = exportService;
            _chungTuChiTietService = chungTuChiTietService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _iNsDonViService = iNsDonViService;
            _logger = logger;
            _mapper = mapper;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ExportCommand = new RelayCommand(obj => IsOpenExportPopup = true);
            ExportExcelCommand = new RelayCommand(obj => OnExportFile((int)ExportType.EXCEL));
            ExportPDFCommand = new RelayCommand(obj =>
            {
                OnExportFile(ExportType.PDF);
            });
            PrintCommand = new RelayCommand(obj =>
            {
                OnExportFile(ExportType.PDF);
            });
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            LoadData();
            LoadLoaiChi();
            LoadDanhMuc();
            LoadDonVi();
        }

        private void LoadData()
        {
            _sessionInfo = _sessionService.Current;
            if (VoucherList != null)
            {
                SThangQuyMoTa = string.Format("{0} - Năm {1}", VoucherList.SThangQuyMoTa, _sessionInfo.YearOfWork);
            }

            LoadTieuDe();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_BANGKE_CHUNGTU) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }

        private void LoadLoaiChi()
        {
            LoaiChis = new List<ComboboxItem>()
            {
                new ComboboxItem { DisplayItem = "Chi TSCĐ", ValueItem = LoaiChiBangKe.ChiTSCD.ToString() },
                new ComboboxItem { DisplayItem = "Chi trực tiếp", ValueItem = LoaiChiBangKe.ChiTrucTiep.ToString() },
                new ComboboxItem { DisplayItem = "Nhập kho", ValueItem = LoaiChiBangKe.ChiNhapKho.ToString() }
            };

            SelectedLoaiChi = LoaiChis.ElementAt(0);
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(n => n.SGiaTri)
                .ToList();
            if (listDonViTinh.Count == 0)
                _units.Add(new ComboboxItem("Đồng", "1"));
            foreach (var dvt in listDonViTinh)
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = dvt.STen;
                cb.ValueItem = dvt.SGiaTri;
                cb.Type = dvt.SMoTa;
                _units.Add(new ComboboxItem(dvt.STen, dvt.SGiaTri));
            }
            OnPropertyChanged(nameof(Units));
            _selectedUnit = Units.ElementAt(0);

            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        public void LoadDonVi()
        {

            ListDonVi = LoadDonViBangKe();
            // Filter
            _donViCollectionView = CollectionViewSource.GetDefaultView(ListDonVi);
            _donViCollectionView.Filter = obj => string.IsNullOrWhiteSpace(_searchDonVi)
                                                 || (obj is CheckBoxItem item &&
                                                     item.DisplayItem.Contains(_searchDonVi, StringComparison.OrdinalIgnoreCase));

            foreach (var org in ListDonVi)
            {
                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountDonVi));
                    OnPropertyChanged(nameof(SelectAllDonVi));
                };
            }
        }

        public ObservableCollection<CheckBoxItem> LoadDonViBangKe()
        {
            var lstIdDonVi = GetListIdDonViBangKe();
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => lstIdDonVi.Contains(x.IIDMaDonVi));
            var lstDonVi = _iNsDonViService.FindByCondition(predicate).ToList();
            if (lstDonVi.Any())
            {
                var result = lstDonVi.Select(item => new CheckBoxItem
                {
                    ValueItem = item.IIDMaDonVi,
                    DisplayItem = string.Join(" - ", item.IIDMaDonVi, item.TenDonVi),
                    NameItem = item.TenDonVi
                }).OrderBy(item => item.ValueItem);
                return new ObservableCollection<CheckBoxItem>(result);
            }
            return new ObservableCollection<CheckBoxItem>();
        }

        public List<string> GetListIdDonViBangKe()
        {
            if (VoucherList != null)
            {
                var predicate = PredicateBuilder.True<NsBkChungTuChiTiet>();
                predicate = predicate.And(x => x.IIdBkchungTu.Equals(VoucherList.Id));
                if (TuNgay.HasValue)
                {
                    predicate = predicate.And(x => DateTime.Compare(TuNgay.Value.Date, x.DNgayChungTu.Date) <= 0);
                }
                if (DenNgay.HasValue)
                {
                    predicate = predicate.And(x => DateTime.Compare(DenNgay.Value.Date, x.DNgayChungTu.Date) >= 0);
                }
                var lstBkChiTiet = _chungTuChiTietService.FindByCondition(predicate).ToList();
                if (lstBkChiTiet.Any())
                {
                    return lstBkChiTiet.Select(x => x.IIdMaDonVi).Distinct().ToList();
                }
            }

            return new List<string>();
        }

        private void OnExportFile(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    int dvt = Convert.ToInt32(SelectedUnit.ValueItem);
                    var lstIdDonViCheck = ListDonVi.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList();
                    List<NsBkChungTuChiTietQuery> listChungTuChiTiet = new List<NsBkChungTuChiTietQuery>();
                    var predicate = PredicateBuilder.True<NsBkChungTuChiTiet>();
                    predicate = predicate.And(x => x.IIdBkchungTu.Equals(VoucherList.Id));
                    predicate = predicate.And(x => lstIdDonViCheck.Contains(x.IIdMaDonVi));
                    if (TuNgay.HasValue)
                    {
                        predicate = predicate.And(x => DateTime.Compare(TuNgay.Value.Date, x.DNgayChungTu.Date) <= 0);
                    }
                    if (DenNgay.HasValue)
                    {
                        predicate = predicate.And(x => DateTime.Compare(DenNgay.Value.Date, x.DNgayChungTu.Date) >= 0);
                    }

                    //predicate = predicate.And(x => SelectedLoaiChi.ValueItem == "0" || x.SLoai.Equals(SelectedLoaiChi.ValueItem));

                    var lstResult = _mapper.Map<List<VoucherListDetailModel>>(_chungTuChiTietService.FindByCondition(predicate).ToList());
                    listChungTuChiTiet = _mapper.Map<List<NsBkChungTuChiTietQuery>>(lstResult);
                    listChungTuChiTiet = listChungTuChiTiet.Select(x => { x.FTongTuChi = x.FTongTuChi / dvt; x.FTongHienVat = x.FTongHienVat / dvt; return x; }).OrderBy(n => n.DNgayChungTu).ThenBy(n => n.SSoChungTu).ToList();
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_BANGKE_CHUNGTU) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    int stt = 1;
                    int rowOffset = 18 - listChungTuChiTiet.Count % 18;
                    if (listChungTuChiTiet.Count < 18 || listChungTuChiTiet.Count > 28)
                    {
                        for (int i = 0; i < rowOffset; i++)
                        {
                            NsBkChungTuChiTietQuery itEmpty = new NsBkChungTuChiTietQuery();
                            itEmpty.DNgayChungTu = null;
                            listChungTuChiTiet.Add(itEmpty);
                        }
                    }

                    foreach (var it in listChungTuChiTiet)
                    {
                        it.Stt = stt++;
                    }

                    RptBangKeThongTri rptBangKe = new RptBangKeThongTri
                    {
                        Cap1 = _cap1,
                        Cap2 = _sessionInfo.TenDonVi,
                        SoChungTu = VoucherList.SSoChungTu,
                        TieuDe1 = Title1,
                        TieuDe2 = Title2,
                        TieuDe3 = Title3,
                        ThoiGian = SThangQuyMoTa,
                        //ThoiGian = VoucherList.DNgayQuyetDinh.HasValue ? string.Format("Tháng {0} năm {1}", VoucherList.DNgayQuyetDinh.Value.Month, VoucherList.DNgayQuyetDinh.Value.Year) : "",
                        h2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem),
                        TongTuChi = listChungTuChiTiet.Select(x => x.FTongTuChi).Sum(),
                        TongChiTSCD = listChungTuChiTiet.Where(x => x.ILoaiChi == LoaiChiBangKe.ChiTSCD).Select(x => x.FTongTuChi).Sum(),
                        TongChiTrucTiep = listChungTuChiTiet.Where(x => x.ILoaiChi == LoaiChiBangKe.ChiTrucTiep).Select(x => x.FTongTuChi).Sum(),
                        TongChiNhapKho = listChungTuChiTiet.Where(x => x.ILoaiChi == LoaiChiBangKe.ChiNhapKho).Select(x => x.FTongTuChi).Sum(),
                        TongHienVat = listChungTuChiTiet.Select(x => x.FTongHienVat).Sum(),
                        TienTuChi = StringUtils.NumberToText(listChungTuChiTiet.Select(x => x.FTongTuChi).Sum() * dvt),
                        TienHienVat = StringUtils.NumberToText(listChungTuChiTiet.Select(x => x.FTongHienVat).Sum() * dvt),
                        Items = listChungTuChiTiet,
                        LNS = VoucherList.SXauNoiMa,
                        Ngay = DateUtils.FormatDateReport(ReportDate),
                        DiaDiem = _diaDiem,
                        ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty,
                        ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty,
                        ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty,
                        ChucDanh4 = _dmChuKy != null ? _dmChuKy.ChucDanh4MoTa : string.Empty,
                        ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty,
                        ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty,
                        ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty,
                        ThuaLenh4 = _dmChuKy != null ? _dmChuKy.ThuaLenh4MoTa : string.Empty,
                        Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty,
                        Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty,
                        Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty,
                        Ten4 = _dmChuKy != null ? _dmChuKy.Ten4MoTa : string.Empty
                    };
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(dvt, exportType);
                    data.Add("FormatNumber", formatNumber);
                    foreach (var prop in rptBangKe.GetType().GetProperties())
                    {
                        data.Add(prop.Name, prop.GetValue(rptBangKe));
                    }
                    data.Add("KinhPhi", VoucherList != null ? VoucherList.SNoiDung : "");
                    var m = listChungTuChiTiet != null ? listChungTuChiTiet.First().SM : "";
                    var tm = listChungTuChiTiet != null ? listChungTuChiTiet.First().STm : "";
                    var ttm = listChungTuChiTiet != null ? listChungTuChiTiet.First().STtm : "";
                    var ng = listChungTuChiTiet != null ? listChungTuChiTiet.First().SNg : "";
                    var khoan = "Mục " + m + " Tiểu mục " + tm + " Tiết mục " + ttm + " Ngành " + ng;
                    data.Add("Khoan", khoan);
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_BANGKE_CHUNGTU);
                    string fileNamePrefix = ExportFileName.RPT_NS_BANGKE_CHUNGTU.Split(".").First();
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<NsBkChungTuChiTietQuery>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
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

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_BANGKE_CHUNGTU) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_BANGKE_CHUNGTU;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.HasAddedSign4 = true;
            DmChuKyDialogViewModel.HasAddedSign5 = false;
            DmChuKyDialogViewModel.HasAddedSign6 = false;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
