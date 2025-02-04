using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Plan.PrintPlanReport;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.RevenueExpenditureDivision;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using DanhMuc = VTS.QLNS.CTC.Core.Domain.DanhMuc;
namespace VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.RevenueExpenditureDivision
{
    public class RevenueExpenditureDivisionReportViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ICollectionView _donViCollectionView;
        private ICollectionView _nNganhCollectionView;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsPhongBanService _iNsPhongBanService;
        private readonly ISktMucLucService _iSktMucLucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsBaoCaoGhiChuService _ghiChuService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly ITnDtChungTuService _tnDtChungTuService;
        private readonly ITnDtChungTuChiTietService _tnDtChungTuChiTietService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private DmChuKy _dmChuKy;
        private string _typeChuky;
        private string _diaDiem;
        private List<TnDtdnChungTuChiTietModel> _listChungTuChiTiet;

        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand ExportSignatureActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand NoteCommand { get; }

        public NsBaoCaoGhiChuDialogViewModel NsBaoCaoGhiChuDialogViewModel { get; }
        public DmChuKyDialogViewModel DmChuKyDialogViewModel { get; }



        public override string Name
        {
            get => $"Báo cáo chi tiết dự toán đơn vị";
        }

        public override string Title
        {
            get => $"Báo cáo chi tiết dự toán đơn vị";
        }

        public override string Description
        {
            get => $"Báo cáo chi tiết dự toán đơn vị";
        }

        public override Type ContentType => typeof(RevenueExpenditureDivisionReport);
        public static DemandCheckPrintType DemandCheckPrintType { get; set; }

        private string _txtTitleFirst;

        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set
            {
                SetProperty(ref _txtTitleFirst, value);
            }
        }

        private string _txtTitleSecond;

        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }
        private string _txtTitleThird;

        public string TxtTitleThird
        {
            get => _txtTitleThird;
            set
            {
                SetProperty(ref _txtTitleThird, value);
            }
        }

        private ObservableCollection<ComboboxItem> _paperPrintTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> PaperPrintTypes
        {
            get => _paperPrintTypes;
            set => SetProperty(ref _paperPrintTypes, value);
        }

        public bool InMotToChecked { get; set; }

        private bool _isInLuyKe;

        public bool IsInLuyKe
        {
            get => _isInLuyKe;
            set
            {
                SetProperty(ref _isInLuyKe, value);
                LoadDonVi();
                OnPropertyChanged(nameof(ListDonVi));
            }
        }

        private bool _isKBNN;

        public bool IsKBNN
        {
            get => _isKBNN;
            set
            {
                SetProperty(ref _isKBNN, value);
            }
        }


        private ObservableCollection<ComboboxItem> _itemsLoaiDuLieu = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ItemsLoaiDuLieu
        {
            get => _itemsLoaiDuLieu;
            set => SetProperty(ref _itemsLoaiDuLieu, value);
        }

        private ComboboxItem _selectedLoaiDuLieu;

        public ComboboxItem SelectedLoaiDuLieu
        {
            get => _selectedLoaiDuLieu;
            set
            {
                SetProperty(ref _selectedLoaiDuLieu, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsKieuGiayIn = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ItemsKieuGiayIn
        {
            get => _itemsKieuGiayIn;
            set => SetProperty(ref _itemsKieuGiayIn, value);
        }

        private ComboboxItem _selectedKieuGiayIn;

        public ComboboxItem SelectedKieuGiayIn
        {
            get => _selectedKieuGiayIn;
            set => SetProperty(ref _selectedKieuGiayIn, value);
        }

        private ObservableCollection<ComboboxItem> _catUnitTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> CatUnitTypes
        {
            get => _catUnitTypes;
            set => SetProperty(ref _catUnitTypes, value);
        }

        private ComboboxItem _catUnitTypeSelected;

        public ComboboxItem CatUnitTypeSelected
        {
            get => _catUnitTypeSelected;
            set => SetProperty(ref _catUnitTypeSelected, value);
        }

        public IEnumerable<DonVi> ListUnit { get; set; }

        private ObservableCollection<CheckBoxItem> _listDonVi = new ObservableCollection<CheckBoxItem>();

        public ObservableCollection<CheckBoxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }

        private List<CheckBoxItem> _lstIIdMaDonVi = new List<CheckBoxItem>();
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

        public List<Guid> ListIdChungTuBaoCaoSncNganh { get; set; }

        private List<DataReportDynamic> _lstDataDynamic = new List<DataReportDynamic>();
        private List<DataReportDynamic> _lstDataDynamicSummary = new List<DataReportDynamic>();

        private LoaiNSBD _loaiNSBD;
        public LoaiNSBD LoaiNSBD
        {
            get => _loaiNSBD;
            set
            {
                SetProperty(ref _loaiNSBD, value);
                LoadTypeChuKy();
                LoadTitleFirst();
            }
        }

        private ObservableCollection<ComboboxItem> _reportLevels = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ReportLevels
        {
            get => _reportLevels;
            set => SetProperty(ref _reportLevels, value);
        }

        private ComboboxItem _reportLevelSelected;

        public ComboboxItem ReportLevelSelected
        {
            get => _reportLevelSelected;
            set
            {
                SetProperty(ref _reportLevelSelected, value);
                OnPropertyChanged(nameof(IsEnableInMotTo));
            }
        }

        public bool IsEnableInMotTo => ReportLevelSelected != null && ReportLevelSelected.ValueItem.Equals("2");
        public bool IsShowLoaiDuLieu { get; set; }

        public bool IsInMotTo { get; set; }
        private TnDtdnChungTuModel Model;
        private DonVi DonViRoot;


        private ObservableCollection<ComboboxItem> _dataDot = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ComboboxItem _dataDotSelected;
        public ComboboxItem DataDotSelected
        {
            get => _dataDotSelected;
            set
            {
                SetProperty(ref _dataDotSelected, value);
                LoadDonVi();
            }
        }

        public RevenueExpenditureDivisionReportViewModel(INsDonViService nsDonViService, IExportService exportService,
            IDanhMucService danhMucService,
            ISktMucLucService iSktMucLucService,
            INsBaoCaoGhiChuService ghiChuService,
            IDmChuKyService dmChuKyService,
            ISessionService sessionService,
            INsPhongBanService iNsPhongBanService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            ITnDtChungTuChiTietService tnDtChungTuChiTietService,
            ITnDtChungTuService tnDtChungTuService,

            IMapper mapper,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            NsBaoCaoGhiChuDialogViewModel nsBaoCaoGhiChuDialogViewModel)
        {
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _danhMucService = danhMucService;
            _iSktMucLucService = iSktMucLucService;
            _dmChuKyService = dmChuKyService;
            _ghiChuService = ghiChuService;
            _iNsPhongBanService = iNsPhongBanService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _tnDtChungTuChiTietService = tnDtChungTuChiTietService;
            _tnDtChungTuService = tnDtChungTuService;

            NsBaoCaoGhiChuDialogViewModel = nsBaoCaoGhiChuDialogViewModel;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportPdfActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ExportSignatureActionCommand = new RelayCommand(ExportSignature);
            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
        }


        #region Note
        private void OnNoteCommand()
        {
            var typeChuKy = _typeChuky;
            //NsBaoCaoGhiChuDialogViewModel.IsBaoCaoSoNhuCauTongHop = false;
            //NsBaoCaoGhiChuDialogViewModel.IsBaoCaoSoKiemTra = IsBaoCaoSoKiemTra;
            NsBaoCaoGhiChuDialogViewModel.LoaiNSBD = LoaiNSBD;
            NsBaoCaoGhiChuDialogViewModel.SMaBaoCao = typeChuKy;
            NsBaoCaoGhiChuDialogViewModel.Init();

            NsBaoCaoGhiChuDialogViewModel.BQuanLySelected = NsBaoCaoGhiChuDialogViewModel.BQuanLyItems.FirstOrDefault();
            NsBaoCaoGhiChuDialogViewModel.KhoiSelected = NsBaoCaoGhiChuDialogViewModel.KhoiItems.FirstOrDefault();
            NsBaoCaoGhiChuDialogViewModel.VoucherTypeSelected = NsBaoCaoGhiChuDialogViewModel.VoucherTypes.FirstOrDefault(x => x.ValueItem == VoucherTypeSelected.ValueItem);

            NsBaoCaoGhiChuDialogViewModel.ShowDialogHost("RevenueExpenditureDivisionReport");
        }
        #endregion
        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            InMotToChecked = false;
            LoaiNSBD = LoaiNSBD.DAC_THU;
            Clear();
            InitReportDefaultDate();
            LoadDataDot();
            LoadDonVi();
            LoadTypeChuKy();
            LoadTitleFirst();
            LoadCatUnitTypes();
            LoadKieuGiayIn();
            LoadReportType();
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
            DonViRoot = _nsDonViService.FindByCondition(NSConstants.ZERO, StatusType.ACTIVE, _sessionInfo.YearOfWork).FirstOrDefault();
        }

        public void Clear()
        {
            _donViCollectionView = null;
        }

        private void LoadTypeChuKy()
        {
            _typeChuky = TypeChuKy.RPT_NS_THUNOP_NGANSACH_PHANBO_DUTOAN_DONVI;
        }

        public void LoadTitleFirst()
        {
            string first = $"GIAO DỰ TOÁN THU NGÂN SÁCH NHÀ NƯỚC NĂM {_sessionInfo.YearOfWork}";
            string second = $"(Kèm theo Quyết định số:.................../QĐ- ..............ngày ..............tháng............năm {_sessionInfo.YearOfWork} của .............................)";

            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            _dmChuKy ??= new DmChuKy();
            _dmChuKy.TieuDe1MoTa = string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa) ? first : _dmChuKy.TieuDe1MoTa;
            _dmChuKy.TieuDe2MoTa = string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa) ? second : _dmChuKy.TieuDe2MoTa;
            TxtTitleFirst = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : first;
            TxtTitleSecond = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : second;
            TxtTitleThird = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
        }

        public virtual void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(0);
        }

        public virtual void LoadReportType()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Ngành", ValueItem = "NG" },
                new ComboboxItem {DisplayItem = "Tiểu ngành", ValueItem = "TNG" },
            };

            ReportLevels = new ObservableCollection<ComboboxItem>(data);
            ReportLevelSelected = ReportLevels.ElementAt(0);
        }


        public void LoadCatUnitTypes()
        {
            _catUnitTypes = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH));
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            List<DanhMuc> data = _danhMucService.FindByCondition(predicate).OrderBy(x => x.SGiaTri).ToList();
            _catUnitTypes = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            if (data.Count == 0)
            {
                _catUnitTypes.Insert(0, new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            _catUnitTypeSelected = _catUnitTypes.Where(t => t.ValueItem == DonViTinh.NGHIN_DONG_VALUE).FirstOrDefault();
        }


        private ObservableCollection<CheckBoxItem> LoadNsDonVisTheoLoaiChungTu()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var iTrangThai = StatusType.ACTIVE;
            var predicateVoucher = PredicateBuilder.True<TnDtChungTu>();
            predicateVoucher = predicateVoucher.And(x => x.NamLamViec == yearOfWork);
            predicateVoucher = predicateVoucher.And(x => x.NamNganSach == _sessionInfo.YearOfBudget);
            predicateVoucher = predicateVoucher.And(x => x.NguonNganSach == _sessionInfo.Budget);
            predicateVoucher = predicateVoucher.And(x => x.ILoai == SoChungTuType.EstimateDivision);
            if (DataDotSelected is null) return new ObservableCollection<CheckBoxItem>();
            if (IsInLuyKe)
            {
                var luyKeDot = DataDot.Where(x => int.Parse(x.HiddenValue) >= int.Parse(DataDotSelected.HiddenValue)).ToList();
                var listIdDot = luyKeDot.Select(x => x.Id).ToList();
                predicateVoucher = predicateVoucher.And(x => listIdDot.Contains(x.Id));
            }
            else
            {
                predicateVoucher = predicateVoucher.And(x => x.Id == DataDotSelected.Id);
            }
            var lstMaDonVi = _tnDtChungTuService.GetAgencyCodeByVoucherDetail(predicateVoucher);
            if (lstMaDonVi.IsEmpty()) return new ObservableCollection<CheckBoxItem>();
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && lstMaDonVi.Contains(x.IIDMaDonVi));
            //bao cao so nhu cau tong hop
            ListUnit = _nsDonViService.FindByCondition(predicate);
            var result = ListUnit.Select(item => new CheckBoxItem
            {
                ValueItem = item.IIDMaDonVi,
                ValueItem2 = item.MaSoKBNN,
                DisplayItem = string.Join("-", item.IIDMaDonVi, item.TenDonVi),
                NameItem = item.TenDonVi,
            }).OrderBy(item => item.ValueItem);
            return new ObservableCollection<CheckBoxItem>(result);
        }


        private ObservableCollection<ComboboxItem> _voucherTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> VoucherTypes
        {
            get => _voucherTypes;
            set => SetProperty(ref _voucherTypes, value);
        }

        private ComboboxItem _voucherTypeSelected;

        public ComboboxItem VoucherTypeSelected
        {
            get => _voucherTypeSelected;
            set
            {
                SetProperty(ref _voucherTypeSelected, value);
                LoadDonVi();
                LoadTypeChuKy();
                LoadTitleFirst();
                OnPropertyChanged(nameof(LabelSelectedCountDonVi));
            }
        }

        private void LoadDataDot()
        {
            DataDot = new ObservableCollection<ComboboxItem>();
            DataDotSelected = null;
            var predicate = CreatePredicate();
            var chungTus = _tnDtChungTuService.FindByCondition(predicate);
            if (chungTus.IsEmpty()) return;
            List<string> lstSoQuyetDinh = chungTus.Where(x => !string.IsNullOrEmpty(x.SoQuyetDinh)).OrderByDescending(o => o.NgayQuyetDinh)
                .Select(x => x.SoQuyetDinh.Trim()).Distinct().ToList();
            int index = 1;
            foreach (var qd in lstSoQuyetDinh)
            {
                var ct = chungTus.FirstOrDefault(x => !string.IsNullOrEmpty(x.SoQuyetDinh) && x.SoQuyetDinh.Trim().Equals(qd));
                string mota = "";
                if (ct != null)
                {
                    mota += ct.NgayQuyetDinh.HasValue ? ct.NgayQuyetDinh.Value.ToString("dd/MM/yyyy") : "";

                    DataDot.Add(new ComboboxItem()
                    {
                        ValueItem = qd,
                        DisplayItem = string.Format("{0}-{1}\n{2}", qd, ct.SoChungTu, mota),
                        DisplayItemOption2 = ct.NgayQuyetDinh.HasValue ? ct.NgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty,
                        HiddenValue = index.ToString(),
                        Id = ct.Id
                    });
                    index++;
                }
            }
            //var ordered = DataDot.OrderByDescending(c => DateTime.Parse(c.DisplayItem.Split('\n')[1])).ToList();
            //DataDot = new ObservableCollection<ComboboxItem>(ordered);
            if (DataDot.IsEmpty()) return;
            DataDotSelected = DataDot.FirstOrDefault();

        }

        public void LoadDonVi()
        {
            ListDonVi = LoadNsDonVisTheoLoaiChungTu();
            ListDonVi = new ObservableCollection<CheckBoxItem>(ListDonVi.GroupBy(item => item.ValueItem).Select(item => item.First()));

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


        public void OnExport(ExportType exportType)
        {
            //if (ReportLevelSelected.ValueItem == "2")
            //    OnExportFileExcelOnePage(exportType);
            //else
            OnPrintRepportByAgency(exportType);

        }

        public Expression<Func<TnDtChungTu, bool>> CreatePredicateCT(List<CheckBoxItem> listIdDonVi)
        {
            var predicate = PredicateBuilder.True<TnDtChungTu>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.NamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.NguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => listIdDonVi.Select(s => s.ValueItem).Contains(x.IdDonVi));
            return predicate;
        }

        public Expression<Func<TnDtChungTu, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<TnDtChungTu>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.NamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.NguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
            return predicate;
        }

        private List<Guid> GetVoucherIds()
        {
            var luyKeDot = DataDot.Where(x => int.Parse(x.HiddenValue) >= int.Parse(DataDotSelected.HiddenValue)).ToList();
            return luyKeDot.Select(x => x.Id).ToList();
        }

        private void CalculateData(List<TnDtdnChungTuChiTietModel> items)
        {
            items.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.FDuToanNamKeHoach = 0;
                    x.FDuToanNamNay = 0;
                    x.FThucThuNamTruoc = 0;
                    x.FUocThucHienNamNay = 0;
                    return x;
                }).ToList();

            foreach (var item in items.Where(x => x.IsEditable && (x.FDuToanNamKeHoach != 0 || x.FDuToanNamNay != 0 || x.FThucThuNamTruoc != 0 || x.FUocThucHienNamNay != 0)))
            {
                CalculateParent(item, item, items);
            }

            CalculateTotal(items);
        }

        private void CalculateParent(TnDtdnChungTuChiTietModel currentItem, TnDtdnChungTuChiTietModel seftItem, List<TnDtdnChungTuChiTietModel> items)
        {
            var parrentItem = items.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FDuToanNamKeHoach += seftItem.FDuToanNamKeHoach;
            parrentItem.FDuToanNamNay += seftItem.FDuToanNamNay;
            parrentItem.FThucThuNamTruoc += seftItem.FThucThuNamTruoc;
            parrentItem.FUocThucHienNamNay += seftItem.FUocThucHienNamNay;
            CalculateParent(parrentItem, seftItem, items);
        }

        private void CalculateTotal(List<TnDtdnChungTuChiTietModel> items)

        {
            Model ??= new TnDtdnChungTuModel();
            Model.FTongDuToanNamKeHoach = 0;
            Model.FTongDuToanNamNay = 0;
            Model.FTongThucThuNamTruoc = 0;
            Model.FTongUocThucHienNamNay = 0;
            var listChildren = items.Where(x => x.IsEditable).ToList();
            foreach (var item in listChildren)
            {
                Model.FTongDuToanNamKeHoach += item.FDuToanNamKeHoach;
                Model.FTongDuToanNamNay += item.FDuToanNamNay;
                Model.FTongThucThuNamTruoc += item.FThucThuNamTruoc;
                Model.FTongUocThucHienNamNay += item.FUocThucHienNamNay;
            }
            OnPropertyChanged(nameof(Model));
        }




        public void OnPrintRepportByAgency(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var h1 = _catUnitTypeSelected != null ? $"{_catUnitTypeSelected.DisplayItem}" : "";
                    List<CheckBoxItem> listDonViChecked = ListDonVi.Where(item => item.IsChecked).ToList();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    _dmChuKy ??= new DmChuKy();
                    List<Guid> VoucherIds = GetVoucherIds();
                    EstimationVoucherDetailCriteria param = new EstimationVoucherDetailCriteria()
                    {
                        VoucherIds = string.Join(",", VoucherIds),
                        YearOfWork = yearOfWork,
                        YearOfBudget = yearOfBudget,
                        BudgetSource = budgetSource,
                        IIDMaDonVis = string.Join(",", listDonViChecked.Select(x => x.ValueItem)),
                        DonViTinh = donViTinh
                    };
                    IEnumerable<TnDtChungTuChiTietQuery> listData = _tnDtChungTuChiTietService.FindByRevenueExpendDivisionReport(param);
                    listData.Where(x => x.MlnsIdParent.IsNullOrEmpty()).Select(x => x.MlnsIdParent = Guid.Empty).ToList();
                    foreach (var item in listDonViChecked)
                    {
                        List<TnDtChungTuChiTietQuery> listDataDonVi = listData.Where(x => x.BHangCha || (!x.BHangCha && !string.IsNullOrEmpty(x.IdDonVi) && x.IdDonVi.Equals(item.ValueItem))).ToList();
                        HandlerData(ref listDataDonVi);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Items", listDataDonVi);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("TenDonVi", item.NameItem);
                        data.Add("Cap1", GetDonViBanHanh(1, _dmChuKy.LoaiDVBanHanh1, itemDanhMuc, _sessionInfo.TenDonVi));
                        data.Add("Cap2", GetDonViBanHanh(2, _dmChuKy.LoaiDVBanHanh2, itemDanhMuc, _sessionInfo.TenDonVi));
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("h1", h1);
                        data.Add("HEADER1", h1);
                        data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                        data.Add("h2", _sessionInfo.TenDonVi);
                        data.Add("IsPhuLuc", false);
                        data.Add("IsTrinhKy", true);
                        data.Add("year", yearOfWork);
                        data.Add("TongSoTien", listDataDonVi.Where(x => !x.BHangCha).Sum(s => s.TuChi) * donViTinh);
                        AddChuKy(data, _typeChuky);
                        data.Add("Count", 100000);
                        data.Add("MaSoKBNN", string.IsNullOrEmpty(item.ValueItem2) ? "......................................................." : item.ValueItem2);
                        data.Add("InMaKBNN", IsKBNN);
   

                        List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(ReportLevelSelected.ValueItem);
                        string templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_THUNOP_NGANSACH_PHANBO_DUTOAN_DONVI));
                        string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_" + _sessionInfo.TenDonVi;
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<TnDtChungTuChiTietQuery, GhiChu>(templateFileName, data, hideColumns.Select(x => x + 6).ToList());
                        results.Add(new ExportResult("Thông báo giao dự toán theo đơn vị " + _sessionInfo.TenDonVi, fileNameWithoutExtension, null, xlsFile));
                    }
                    
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (result != null)
                        {
                            if (InMotToChecked)
                            {
                                if (exportType == ExportType.EXCEL)
                                {
                                    exportType = ExportType.EXCEL_ONE_PAPER;
                                }
                                else
                                {
                                    exportType = ExportType.PDF_ONE_PAPER;
                                }
                            }
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

        private void HandlerData(ref List<TnDtChungTuChiTietQuery> items)
        {
            if (items.IsEmpty()) return;
            var itemParents = items.Where(x => x.BHangCha && !x.MlnsIdParent.IsNullOrEmpty());
            var lstIdParent = items.Where(x => !x.MlnsIdParent.IsNullOrEmpty()).Select(x => x.MlnsIdParent).ToList();
            var itemRemove = itemParents.Where(x => !lstIdParent.Contains(x.MlnsId));
            items = items.Where(x => !itemRemove.Select(y => y.MlnsId).Contains(x.MlnsId)).ToList();
        }

        private void OnExportFileExcelOnePage(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> exportResults = new List<ExportResult>();
                int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
                var yearOfWork = _sessionInfo.YearOfWork;
                var yearOfBudget = _sessionInfo.YearOfBudget;
                var budgetSource = _sessionInfo.Budget;
                var h1 = _catUnitTypeSelected != null ? $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}" : "";
                List<CheckBoxItem> listIdDonVi = ListDonVi.Where(item => item.IsChecked).ToList();
                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _dmChuKy ??= new DmChuKy();
                var predicateCt = CreatePredicateCT(listIdDonVi);
                List<TnDtdnChungTu> listChungTu = new List<TnDtdnChungTu>();//_tnDtChungTuService.FindByCondition(predicateCt).ToList();
                EstimationVoucherDetailCriteria param = new EstimationVoucherDetailCriteria()
                {
                    LNS = string.Join(",", listChungTu.Select(x => x.SDSLNS ?? string.Empty)),
                    YearOfWork = yearOfWork,
                    YearOfBudget = yearOfBudget,
                    BudgetSource = budgetSource,
                    IIDMaDonVis = string.Join(",", listIdDonVi.Select(x => x.ValueItem)),
                    LoaiChungTu = (int)SummaryPrintType.Detail,
                    DonViTinh = donViTinh
                };
                _listChungTuChiTiet = _mapper.Map<List<TnDtdnChungTuChiTietModel>>(new List<TnDtdnChungTuChiTietModel>());
                //_mapper.Map<List<TnDtdnChungTuChiTietModel>>(_tnDtChungTuChiTietService.FindDataReportAgencyDetailByCondition(param));
                List<CheckBoxItem> lstIdDonVi = ListDonVi.Where(item => item.IsChecked).ToList();
                int size = lstIdDonVi.Count() + 1;
                if (!InMotToChecked || exportType.Equals(ExportType.PDF))
                    size = 4;
                HandlerDataExportOnePage(lstIdDonVi, size, donViTinh, exportType, exportResults);
                e.Result = exportResults;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    List<ExportResult> result = (List<ExportResult>)e.Result;
                    if (result != null)
                    {
                        //if (InMotToChecked)
                        //    exportType = ExportType.PDF_ONE_PAPER;
                        if (InMotToChecked)
                        {
                            if (exportType == ExportType.EXCEL)
                            {
                                exportType = ExportType.EXCEL_ONE_PAPER;
                            }
                            else
                            {
                                exportType = ExportType.PDF_ONE_PAPER;
                            }
                        }
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


        private void HandlerDataExportOnePage(List<CheckBoxItem> lstIdDonVi, int size, int dvt, ExportType exportType, List<ExportResult> exportResults)
        {
            lstIdDonVi.Insert(0, new CheckBoxItem() { DisplayItem = "Bản thân " });
            List<List<CheckBoxItem>> listDonViSplits = SplitList(lstIdDonVi, size).ToList();
            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            int year = _sessionInfo.YearOfWork;
            int year1 = year - 1;
            List<int> hideColumns = new List<int>();
            var itemsHeaders2 = new List<HeaderReport>
                {
                    new HeaderReport()
                    {
                        Name = $"Ước thực hiện năm {year1}",
                    },
                     new HeaderReport()
                    {
                        Name = $"Dự toán năm {year}",
                    }
                };
            int donViTinh = 1;
            int rowHeader = 11;
            List<Tuple<int, int, int, int>> tuples = new List<Tuple<int, int, int, int>>();
            foreach (var donviPage in listDonViSplits.Select((value, index) => (value, index)))
            {
                int columnStartPage1 = 7;
                if (donviPage.index == NSConstants.ZERO) columnStartPage1 = 9;
                string templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DUTOANDAUNAM_ONEPAGE));
                List<HeaderReport> listHeader2 = new List<HeaderReport>();
                List<TnDtdnChungTuChiTietModel> results = new List<TnDtdnChungTuChiTietModel>();
                List<HeaderReport> listHeader = new List<HeaderReport>();

                tuples.Add(Tuple.Create(rowHeader, columnStartPage1, rowHeader, columnStartPage1 + 1));

                if (donviPage.index > 0) templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DUTOANDAUNAM_MULTIPAGE));

                var itemsClone = _listChungTuChiTiet.Clone();
                var itemsPage = itemsClone.Where(x => x.BHangCha || (!x.BHangCha && !string.IsNullOrEmpty(x.IIdMaDonVi) && donviPage.value.Select(s => s.ValueItem).Contains(x.IIdMaDonVi))).ToList();
                if (donviPage.value.Count < size)
                {
                    int countEmpty = size - donviPage.value.Count;
                    for (int j = 0; j < countEmpty; j++)
                    {
                        CheckBoxItem emptyCb = new CheckBoxItem();
                        donviPage.value.Add(emptyCb);
                    }
                }
                int columnEndPage1 = columnStartPage1 + (donviPage.value.Count > 1 ? donviPage.value.Count * 2 - 1 : 2);
                var ColNameStartPage1 = GetExcelColumnName(columnStartPage1);
                var ColNameEndPage1 = GetExcelColumnName(columnEndPage1);
                var mergeRangePage1 = string.Format("{0}10:{1}10", ColNameStartPage1, ColNameEndPage1);
                int colStart = columnStartPage1 + 2;

                foreach (var tenDv in donviPage.value.Select((value, index) => (value, index)))
                {
                    var itemHeaders = new List<HeaderReport>
                    {
                        new HeaderReport
                        {
                            Name = tenDv.value.DisplayItem,
                            Index = 1,
                            MergeRange = string.Format("{0}11:{1}11", GetExcelColumnName(colStart),GetExcelColumnName(colStart)),
                            NameCol = $"{GetExcelColumnName(colStart)}"
                        },
                        new HeaderReport()
                        {
                            NameCol = $"{GetExcelColumnName(colStart+1)}"
                        }
                    };
                    tuples.Add(Tuple.Create(rowHeader, colStart, rowHeader, colStart + 1));
                    listHeader.AddRange(itemHeaders);
                    colStart += 2;
                    if (string.IsNullOrEmpty(tenDv.value.DisplayItem))
                    {
                        listHeader2.Add(new HeaderReport());
                        listHeader2.Add(new HeaderReport());
                    }
                    else
                    {
                        listHeader2.AddRange(itemsHeaders2);
                    }
                }
                //List<string> lstCol1 = new List<string>();
                //List<string> lstCol2 = new List<string>();
                //foreach (var item in tuples.Select((value, index) => (value, index)))
                //{
                //    if (item.index > 0)
                //    {
                //        lstCol1.Add(GetExcelColumnName(item.value.Item2) + "{0}");
                //        lstCol2.Add(GetExcelColumnName(item.value.Item4) + "{0}");
                //    }

                //}
                var itemsResult = itemsPage.GroupBy(x => x.MlnsId).Select(s => new TnDtdnChungTuChiTietModel()
                {
                    MlnsId = s.Key,
                    MlnsIdParent = s.FirstOrDefault()?.MlnsIdParent ?? Guid.Empty,
                    XauNoiMa = s.FirstOrDefault()?.XauNoiMa ?? string.Empty,
                    M = s.FirstOrDefault()?.M ?? string.Empty,
                    Lns = s.FirstOrDefault()?.Lns ?? string.Empty,
                    L = s.FirstOrDefault()?.L ?? string.Empty,
                    K = s.FirstOrDefault()?.K ?? string.Empty,
                    Tm = s.FirstOrDefault()?.Tm ?? string.Empty,
                    Ttm = s.FirstOrDefault()?.Ttm ?? string.Empty,
                    Ng = s.FirstOrDefault()?.Ng ?? string.Empty,
                    Tng = s.FirstOrDefault()?.Tng ?? string.Empty,
                    Tng1 = s.FirstOrDefault()?.Tng1 ?? string.Empty,
                    Tng2 = s.FirstOrDefault()?.Tng2 ?? string.Empty,
                    Tng3 = s.FirstOrDefault()?.Tng3 ?? string.Empty,
                    MoTa = s.FirstOrDefault()?.MoTa ?? string.Empty,
                    BHangCha = s.FirstOrDefault()?.BHangCha ?? false,
                    LstGiaTri = ParseDataGroup(s.ToList(), donviPage.value, donviPage.index)
                }).ToList();
                var itemChild = new List<TnDtdnChungTuChiTietModel>();
                if (!itemsResult.IsEmpty())
                {
                    itemChild = itemsResult.Where(x => !x.BHangCha).ToList();
                    foreach (var item in itemChild)
                    {
                        FilterData(item, results, itemsResult);
                    }
                }
                results = results.OrderBy(o => o.XauNoiMa).ToList();
                //foreach (var item in results.Select((value, index) => (value, index)))
                //{
                //    item.value.NameCol1 = GetExpresstion(lstCol1, 13 + item.index);
                //    item.value.NameCol2 = GetExpresstion(lstCol2, 13 + item.index);
                //}
                var listTitle = listHeader2.Clone();
                listTitle.FirstOrDefault().Index = 1;
                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                data.Add("FormatNumber", formatNumber);
                data.Add("ListData", results);
                data.Add("TieuDe1", TxtTitleFirst);
                data.Add("TieuDe2", TxtTitleSecond);
                data.Add("TieuDe3", TxtTitleThird);
                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                data.Add("DonVi", _sessionInfo.TenDonVi);
                data.Add("Cap1", GetDonViBanHanh(1, _dmChuKy.LoaiDVBanHanh1, itemDanhMuc, _sessionInfo.TenDonVi));
                data.Add("Cap2", GetDonViBanHanh(2, _dmChuKy.LoaiDVBanHanh2, itemDanhMuc, _sessionInfo.TenDonVi));
                data.Add("DiaDiem", _diaDiem);
                data.Add("Header1", _catUnitTypeSelected != null ? $"{_catUnitTypeSelected.DisplayItem}" : "");
                data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                data.Add("h2", _sessionInfo.TenDonVi);
                data.Add("IsPhuLuc", false);
                data.Add("IsTrinhKy", true);
                data.Add("year", year);
                data.Add("year1", year - 1);
                data.Add("year2", year - 2);
                data.Add("IsTongHop", true);
                data.Add("TongSoTien", results.Where(x => !x.BHangCha).Sum(s => s.FTongDuToanNamKeHoach) * donViTinh);
                data.Add("Count", 1000000);
                AddChuKy(data, _typeChuky);
                data.Add("Headers", listHeader);
                data.Add("Headers2", listHeader2);
                data.Add("ListHeader1", listTitle);
                data.Add("Total", listTitle);
                data.Add("MergeRange", mergeRangePage1);


                string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_" + _sessionInfo.TenDonVi;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<TnDtdnChungTuChiTietModel, TnDtdnChungTuChiTietDynamicModel, GhiChu, HeaderReport>(templateFileName, data, hideColumns);
                foreach (var tuple in tuples)
                {
                    xlsFile.MergeCells(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
                }
                if (string.IsNullOrEmpty(TxtTitleSecond)) xlsFile.SetRowHidden(5, true);
                if (string.IsNullOrEmpty(TxtTitleThird)) xlsFile.SetRowHidden(6, true);
                exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
            }
        }

        private string GetExpresstion(List<string> cols, int index)
        {
            if (cols.IsEmpty()) return string.Empty;
            string nameJoin = string.Format(string.Join(",", cols), index);
            string nameExpress = $"=SUM({nameJoin})";
            return nameExpress;
        }

        private List<TnDtdnChungTuChiTietDynamicModel> ParseDataGroup(List<TnDtdnChungTuChiTietModel> items, List<CheckBoxItem> donvi, int index)
        {
            List<TnDtdnChungTuChiTietDynamicModel> outPut = new List<TnDtdnChungTuChiTietDynamicModel>();
            if (items.IsEmpty() && donvi.IsEmpty()) return outPut;
            foreach (var dv in donvi.Select((value, index) => (index, value)))
            {
                if (index == 0 && dv.index == 0)
                {
                    AddDataBanThan(outPut, items, donvi);
                }
                else
                {
                    TnDtdnChungTuChiTietDynamicModel giaTri = new TnDtdnChungTuChiTietDynamicModel();
                    TnDtdnChungTuChiTietDynamicModel giaTri2 = new TnDtdnChungTuChiTietDynamicModel();
                    if (!string.IsNullOrEmpty(dv.value.ValueItem))
                    {
                        TnDtdnChungTuChiTietModel gtDonVi = items.FirstOrDefault(x => dv.value.ValueItem.Equals(x.IIdMaDonVi));
                        if (gtDonVi != null)
                        {
                            giaTri.FVal = gtDonVi.FUocThucHienNamNay;
                            giaTri.FUocThucHienNamNay = gtDonVi.FUocThucHienNamNay;
                            giaTri2.FDuToanNamKeHoach = gtDonVi.FDuToanNamKeHoach;
                            giaTri2.FVal = gtDonVi.FDuToanNamKeHoach;
                        }
                        outPut.Add(giaTri);
                        outPut.Add(giaTri2);
                    }
                    else
                    {
                        outPut.Add(giaTri);
                        outPut.Add(giaTri2);
                    }
                }

            }
            return outPut;
        }

        private void AddDataBanThan(List<TnDtdnChungTuChiTietDynamicModel> output, List<TnDtdnChungTuChiTietModel> items, List<CheckBoxItem> donvi)
        {
            if (donvi.Any(x => x.ValueItem2 == DonViRoot.iCapDonVi.GetValueOrDefault(-1).ToString()))
            {
                var itemDetail = items.Where(x => donvi.Where(w => w.ValueItem2 == DonViRoot.iCapDonVi.GetValueOrDefault(-1).ToString()).Select(s => s.ValueItem).Contains(x.IIdMaDonVi));
                TnDtdnChungTuChiTietDynamicModel giaTri = new TnDtdnChungTuChiTietDynamicModel()
                {
                    FVal = itemDetail.Sum(x => x.FUocThucHienNamNay)
                };
                TnDtdnChungTuChiTietDynamicModel giaTri2 = new TnDtdnChungTuChiTietDynamicModel()
                {
                    FVal = itemDetail.Sum(x => x.FDuToanNamKeHoach)
                };
                output.Add(giaTri);
                output.Add(giaTri2);
            }
            else
            {
                output.Add(new TnDtdnChungTuChiTietDynamicModel());
                output.Add(new TnDtdnChungTuChiTietDynamicModel());
            }
        }


        private void FilterData(TnDtdnChungTuChiTietModel item, List<TnDtdnChungTuChiTietModel> results, List<TnDtdnChungTuChiTietModel> items)
        {
            if (item.IsDynamic)
            {
                results.Add(item);
                AddParent(item, results, items);
            }
        }

        private void AddParent(TnDtdnChungTuChiTietModel item, List<TnDtdnChungTuChiTietModel> results, List<TnDtdnChungTuChiTietModel> items)
        {
            if (item.MlnsIdParent.IsNullOrEmpty()) return;
            if (results.Any(x => item.MlnsIdParent.Equals(x.MlnsId))) return;
            TnDtdnChungTuChiTietModel parent = items.FirstOrDefault(x => x.MlnsId.Equals(item.MlnsIdParent));
            if (parent is null) return;
            results.Add(parent);
            AddParent(parent, results, items);
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize1, int nSize2)
        {
            for (int i = 0; i < bigList.Count; i += nSize1)
            {
                yield return bigList.GetRange(i, Math.Min(nSize1, bigList.Count - i));
                break;
            }
            for (int i = nSize2 - 1; i < bigList.Count; i += nSize2)
            {
                yield return bigList.GetRange(i, Math.Min(nSize2, bigList.Count - i));
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

        private bool CheckListValue(List<DataReportDynamic> listResult)
        {
            return listResult.Any(x => x.FVal != 0);
        }
        private List<DataReportDynamic> GetDataDefault()
        {
            List<DataReportDynamic> result = new List<DataReportDynamic>();
            foreach (var item in _lstDataDynamic)
            {
                result.Add(new DataReportDynamic());
            }

            return result;
        }
        public void ExportSignature(object param)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public List<NsSktMucLuc> FindListParentMucLucByChild(List<string> listIdMucLuc)
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var listMucLuc = _iSktMucLucService
                .FindByCondition(x => listIdMucLuc.Contains(x.SKyHieu) && x.INamLamViec == yearOfWork).ToList();
            List<Guid> listIdMlskt = new List<Guid>();
            List<NsSktMucLuc> sktMucLucs = new List<NsSktMucLuc>();
            if (listMucLuc.Count > 0)
            {
                listIdMlskt = listMucLuc.Select(item => item.IIDMLSKT).ToList();
                sktMucLucs = listMucLuc;
                while (true)
                {
                    var listIdParent = listMucLuc.Where(x => !listIdMlskt.Contains(x.IIDMLSKTCha.GetValueOrDefault())).Select(x => x.IIDMLSKTCha).ToList();
                    var listParent1 = _iSktMucLucService.FindByCondition(x => listIdParent.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).ToList();
                    if (listParent1.Count > 0)
                    {
                        var lstId = listParent1.Select(item => item.IIDMLSKT).ToList();
                        listIdMlskt.AddRange(lstId);
                        sktMucLucs.AddRange(listParent1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            sktMucLucs = sktMucLucs.GroupBy(x => x.IIDMLSKT).Select(x => x.First()).OrderBy(x => x.SKyHieu).ToList();
            return sktMucLucs;
        }

        public List<NsSktMucLuc> FindListParentMucLucByKyHieuCu(List<string> listIdMucLuc)
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var listMucLuc = _iSktMucLucService
                .FindByCondition(x => listIdMucLuc.Contains(x.SKyHieuCu) && x.INamLamViec == yearOfWork).ToList();
            List<Guid> listIdMlskt = new List<Guid>();
            List<NsSktMucLuc> sktMucLucs = new List<NsSktMucLuc>();
            if (listMucLuc.Count > 0)
            {
                listIdMlskt = listMucLuc.Select(item => item.IIDMLSKT).ToList();
                sktMucLucs = listMucLuc;
                while (true)
                {
                    var listIdParent = listMucLuc.Where(x => !listIdMlskt.Contains(x.IIDMLSKTCha.GetValueOrDefault())).Select(x => x.IIDMLSKTCha).ToList();
                    var listParent1 = _iSktMucLucService.FindByCondition(x => listIdParent.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).ToList();
                    if (listParent1.Count > 0)
                    {
                        var lstId = listParent1.Select(item => item.IIDMLSKT).ToList();
                        listIdMlskt.AddRange(lstId);
                        sktMucLucs.AddRange(listParent1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            sktMucLucs = sktMucLucs.GroupBy(x => x.IIDMLSKT).Select(x => x.First()).OrderBy(x => x.SKyHieuCu).ToList();
            return sktMucLucs;
        }

        public List<NsSktMucLuc> FindListParent2CapMucLucByChild(List<Guid> listIdMucLuc)
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var listMucLuc = _iSktMucLucService
                .FindByCondition(x => listIdMucLuc.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).ToList();
            List<Guid> listIdMlskt = new List<Guid>();
            List<NsSktMucLuc> sktMucLucs = new List<NsSktMucLuc>();
            int count = 0;
            if (listMucLuc.Count > 0)
            {
                listIdMlskt = listMucLuc.Select(item => item.IIDMLSKT).ToList();
                sktMucLucs = listMucLuc;
                while (true)
                {
                    var listIdParent = listMucLuc.Where(x => !listIdMlskt.Contains(x.IIDMLSKTCha.GetValueOrDefault())).Select(x => x.IIDMLSKTCha).ToList();
                    var listParent1 = _iSktMucLucService.FindByCondition(x => listIdParent.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).ToList();
                    if (listParent1.Count > 0)
                    {
                        var lstId = listParent1.Select(item => item.IIDMLSKT).ToList();
                        listIdMlskt.AddRange(lstId);
                        sktMucLucs.AddRange(listParent1.Select(x =>
                        {
                            x.Rank = count + 1;
                            return x;
                        }));
                    }
                    else
                    {
                        break;
                    }

                    count++;
                    // luc dau dang de la count > 1 nen lay 2 dong
                    if (count > 3) break;
                }
            }
            sktMucLucs = sktMucLucs.GroupBy(x => x.IIDMLSKT).Select(x => x.First()).OrderBy(x => x.SKyHieu).ToList();
            return sktMucLucs;
        }


        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
            {
                chuKyModel.IdType = _typeChuky;
                chuKyModel.TieuDe1MoTa = TxtTitleFirst;
                chuKyModel.TieuDe2MoTa = TxtTitleSecond;
                chuKyModel.TieuDe3MoTa = TxtTitleThird;
            }
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TxtTitleFirst = chuKy.TieuDe1MoTa;
                TxtTitleSecond = chuKy.TieuDe2MoTa;
                TxtTitleThird = chuKy.TieuDe3MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        public void AddNgayDiaDiem(Dictionary<string, object> data)
        {
            //add ngày địa điểm
            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
            data.Add("DiaDiem", _diaDiem);
        }

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh1MoTa);
            data.Add("ChucDanh1", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh1MoTa);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy == null ? string.Empty : dmChyKy.Ten1MoTa);
            data.Add("ThuaLenh2", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh2MoTa);
            data.Add("ChucDanh2", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh2MoTa);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy == null ? string.Empty : dmChyKy.Ten2MoTa);
            data.Add("ThuaLenh3", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh3MoTa);
            data.Add("ChucDanh3", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh3MoTa);
            data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten3", dmChyKy == null ? string.Empty : dmChyKy.Ten3MoTa);
        }

        public string RemoveIllegalCharacterFileName(string fileName)
        {
            return new Regex(@"[<>:""/\\|?*]").Replace(fileName, "-");
        }

        public string GetTemplate(string input)
        {
            return Path.Combine(ExportPrefix.PATH_TL_THUNOP_NGANSACH, input + FileExtensionFormats.Xlsx);
        }

        private class GhiChu
        {
            public string Content { get; set; }
            //public string SGhiChu => $"- {Content}";
            public string SGhiChu => Content;
        }

        private string GetDonViBanHanh(int iDonVi, string loaiDVBH, DanhMuc itemDanhMuc, string selectedUnit)
        {
            string dvBanHanh = "";
            if (itemDanhMuc != null)
            {
                switch (loaiDVBH)
                {
                    case "1":
                        dvBanHanh = itemDanhMuc.SGiaTri;
                        break;
                    case "2":
                        dvBanHanh = _sessionInfo.TenDonVi;
                        break;
                    case "3":
                        dvBanHanh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).FirstOrDefault(n => n.IIDMaDanhMuc == MaDanhMuc.DV_THONGTRI_BANHANH).SGiaTri;
                        break;
                    case "4":
                        dvBanHanh = selectedUnit;
                        break;
                    case "5":
                        dvBanHanh = iDonVi == 1 ? _dmChuKy.TenDVBanHanh1 : _dmChuKy.TenDVBanHanh2;
                        break;
                    default:
                        dvBanHanh = iDonVi == 1 ? itemDanhMuc.SGiaTri : _sessionInfo.TenDonVi;
                        break;
                }
            }
            return dvBanHanh;
        }
    }
}
