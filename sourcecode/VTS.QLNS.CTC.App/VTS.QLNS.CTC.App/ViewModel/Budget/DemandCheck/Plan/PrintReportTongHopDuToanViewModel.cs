using AutoMapper;
using FlexCel.Core;
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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan
{
    public class PrintReportTongHopDuToanViewModel : ViewModelBase
    {
        private ICpChungTuService _chungTuService;
        private IMapper _mapper;
        private ICollectionView _listDonViView;
        private ISessionService _sessionService;
        private INsPhongBanService _phongBanService;
        private IExportService _exportService;
        private ISktSoLieuService _sktSoLieuService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private INsDonViService _nsDonViService;
        private ISktSoLieuChungTuService _sktChungTuService;
        private INsNguoiDungDonViService _nsNguoiDungDonViService;
        private DanhMucCauHinhHeThongService _danhMucCauHinhHeThongService;
        private ICollectionView _listBudgetIndex;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private string _diaDiem;
        private readonly INsMucLucNganSachService _iNsMucLucNganSachService;
        public override string Name => "Báo cáo dự toán tổng hợp";
        public override string Title => "Báo cáo dự toán tổng hợp";
        public override Type ContentType => typeof(View.Budget.DemandCheck.Plan.PrintReportTongHopDuToan);
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        public ObservableCollection<CheckBoxItem> ListDonVi { get; set; }
        public List<ReportDuToanDauNamTongHopQuery> DataReportTongHop;
        public double TongQuyetToan;
        public double TongDuToan;
        public double TongTuChi;
        public double TongUocThucHien;
        public double TongDonVi1;
        public double TongDonVi2;
        public double TongDonVi3;
        public double TongDonVi1_UocThucHien;
        public double TongDonVi2_UocThucHien;
        public double TongDonVi3_UocThucHien;
        public double TongDonVi4;
        public double TongDonVi5;
        public double TongDonVi6;
        public string TypeReport;

        public bool IsCalculating { get; set; } = false;

        #region list LNS

        private ObservableCollection<NsMuclucNgansachModel> _budgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
        public ObservableCollection<NsMuclucNgansachModel> BudgetIndexes
        {
            get => _budgetIndexes;
            set => SetProperty(ref _budgetIndexes, value);
        }

        private string _searchBudgetIndexText;
        public string SearchBudgetIndexText
        {
            set
            {
                if (SetProperty(ref _searchBudgetIndexText, value))
                {
                    _listBudgetIndex.Refresh();
                }
            }
        }

        public string SelectedBudgetIndexCount
        {
            get
            {
                int totalCount = BudgetIndexes != null ? BudgetIndexes.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => BudgetIndexes != null && BudgetIndexes.Any() && BudgetIndexes.Where(x => x.IsFilter).All(item => item.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                if (BudgetIndexes != null)
                {
                    IsCalculating = true;
                    BudgetIndexes.Select(c => { c.IsSelected = _isSelectAllBudgetIndex; return c; }).ToList();
                    IsCalculating = false;
                    OnPropertyChanged(nameof(IsExportEnable));
                }
            }
        }
        #endregion
        public bool IsEnableListDonVi => (LoaiBaoCao != null && LoaiBaoCao.Where(n => n.IsChecked && n.DisplayItem == Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_DONVI0).Count() > 0)
            ? false : true;

        private ObservableCollection<CheckBoxItem> _loaiBaoCao;
        public ObservableCollection<CheckBoxItem> LoaiBaoCao
        {
            get => _loaiBaoCao;
            set => SetProperty(ref _loaiBaoCao, value);
        }

        private ObservableCollection<ComboboxItem> _budgetSourceTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BudgetSourceTypes
        {
            get => _budgetSourceTypes;
            set => SetProperty(ref _budgetSourceTypes, value);
        }

        private ComboboxItem _budgetSourceTypeSelected;

        public ComboboxItem BudgetSourceTypeSelected
        {
            get => _budgetSourceTypeSelected;
            set
            {
                SetProperty(ref _budgetSourceTypeSelected, value);
                LoadDonVi();
                BudgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
            }
        }

        private List<ComboboxItem> _bQuanLy;
        public List<ComboboxItem> BQuanLy
        {
            get => _bQuanLy;
            set => SetProperty(ref _bQuanLy, value);
        }

        private ComboboxItem _selectedBQuanLy;
        public ComboboxItem SelectedBQuanLy
        {
            get => _selectedBQuanLy;
            set {
                SetProperty(ref _selectedBQuanLy, value);
                LoadBudgetIndexes();
            }
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

        private ObservableCollection<ComboboxItem> _khoiItems = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> KhoiItems
        {
            get => _khoiItems;
            set => SetProperty(ref _khoiItems, value);
        }

        private ComboboxItem _khoiSelected;

        public ComboboxItem KhoiSelected
        {
            get => _khoiSelected;
            set
            {
                SetProperty(ref _khoiSelected, value);
                LoadDonVi();
            }
        }

        private bool _isBaoCaoDuToanTongHop = false;

        public bool IsBaoCaoDuToanTongHop
        {
            get => _isBaoCaoDuToanTongHop;
            set => SetProperty(ref _isBaoCaoDuToanTongHop, value);
        }

        private string _loaiChungTu;
        public string LoaiChungTu
        {
            get => _loaiChungTu;
            set => SetProperty(ref _loaiChungTu, value);
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

        public string SelectedCountDonVi
        {
            get
            {
                int totalCount = ListDonVi != null ? ListDonVi.Count : 0;
                int totalSelected = ListDonVi != null ? ListDonVi.Count(item => item.IsChecked) : 0;
                return string.Format("ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }

        public bool? SelectAllDonVi
        {
            get
            {
                if (ListDonVi != null)
                {
                    var selected = ListDonVi.Select(item => item.IsChecked).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    IsCalculating = true;
                    SelectAll(value.Value, ListDonVi);
                    IsCalculating = false;
                    OnPropertyChanged(nameof(SelectedCountDonVi));
                    OnPropertyChanged(nameof(IsEnableButtonPrint));
                    LoadBudgetIndexes();
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

        public bool IsEnableButtonPrint
        {
            get
            {
                if (ListDonVi != null && ListDonVi.Where(n => n.IsChecked).Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
                }
            }
        }

        public bool IsExportEnable
        {
            get
            {
                return ListDonVi != null && ListDonVi.Where(n => n.IsChecked).Any()
                    && _budgetIndexes != null && _budgetIndexes.Where(x => x.IsSelected).Any();
            }
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
                LoaiChungTu = string.Empty;
                if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSSD_Key)
                {
                    LoaiChungTu = VoucherType.NSSD_Key;
                }
                else
                {
                    LoaiChungTu = VoucherType.NSBD_Key;
                }

                LoadDonVi();
            }
        }

        private void LoadBQuanLy()
        {
            var predicate = PredicateBuilder.True<DmBQuanLy>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            List<DmBQuanLy> listPhongBan = _phongBanService.FindByCondition(predicate).OrderBy(x => x.IIDMaBQuanLy).ToList();
            _bQuanLy = _mapper.Map<List<ComboboxItem>>(listPhongBan);
            if (_bQuanLy.Count() > 0)
            {
                _bQuanLy.Insert(0, new ComboboxItem("Tất cả", string.Empty));
                SelectedBQuanLy = _bQuanLy.FirstOrDefault();
            }
        }

        private bool _isInTheoTongHop;
        public bool IsInTheoTongHop
        {
            get => _isInTheoTongHop;
            set
            {
                SetProperty(ref _isInTheoTongHop, value);
                LoadDonVi();
            }
        }

        private ObservableCollection<ComboboxItem> _dataInToiMuc;
        public ObservableCollection<ComboboxItem> DataInToiMuc
        {
            get => _dataInToiMuc;
            set => SetProperty(ref _dataInToiMuc, value);
        }

        private ComboboxItem _selectedInToiMuc;
        public ComboboxItem SelectedInToiMuc
        {
            get => _selectedInToiMuc;
            set => SetProperty(ref _selectedInToiMuc, value);
        }

        public bool IsShowInTheoTongHop => _sessionService.Current.IsQuanLyDonViCha && !Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI.Equals(TypeReport);
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintReportTongHopDuToanViewModel(
         ICpChungTuService chungTuService,
         INsDonViService donViService,
         IMapper mapper,
         INsDonViService nSDonViService,
         ISessionService sessionService,
         INsDonViService nsDonViService,
         ILog logger,
         ISktSoLieuService sktSoLieuService,
         IDanhMucService danhMucService,
         INsPhongBanService phongBanService,
         IExportService exportService,
         INsNguoiDungDonViService nsNguoiDungDonViService,
         IDmChuKyService dmChuKyService,
         ISktSoLieuChungTuService sktChungTuService,
         DanhMucCauHinhHeThongService danhMucCauHinhHeThongService,
         DmChuKyDialogViewModel dmChuKyDialogViewModel,
         INsMucLucNganSachService iNsMucLucNganSachService)
        {
            _chungTuService = chungTuService;
            _mapper = mapper;
            _sessionService = sessionService;
            _exportService = exportService;
            _sktSoLieuService = sktSoLieuService;
            _danhMucService = danhMucService;
            _phongBanService = phongBanService;
            _dmChuKyService = dmChuKyService;
            _nsDonViService = nsDonViService;
            _sktChungTuService = sktChungTuService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _danhMucCauHinhHeThongService = danhMucCauHinhHeThongService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;

            PrintPDFCommand = new RelayCommand(o => PrintPDF());
            PrintExcelCommand = new RelayCommand(o => PrintExcel());
            PrintBrowserCommand = new RelayCommand(o => OnPrintBrowser());
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            try
            {
                _isInTheoTongHop = false;
                InitReportDefaultDate();
                LoadCombobox();
                LoadKhois();
                LoadDonVi();
                LoadVoucherTypes();
                LoadChiTietToi();
                LoadBudgetSourceTypes();
                LoadBQuanLy();
                LoadBudgetIndexes();
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_DAUNAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                TieuDe1 = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
                TieuDe2 = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
                TieuDe3 = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
                //TieuDe1 = "Chi tiết lập dự toán ngân sách năm " + _sessionService.Current.YearOfWork;
                var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
                _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private List<NguoiDungDonVi> GetListDonViPhanQuyen()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => x.IIDMaNguoiDung == _sessionService.Current.Principal);
            List<NguoiDungDonVi> listNguoiDungDonVi = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return listNguoiDungDonVi;
        }

        public void LoadCombobox()
        {
            LoaiBaoCao = new ObservableCollection<CheckBoxItem>();
            switch (TypeReport)
            {
                case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI:
                    LoaiBaoCao.Add(new CheckBoxItem { IsChecked = true, DisplayItem = Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI });
                    IsBaoCaoDuToanTongHop = false;
                    break;
                case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN:
                    LoaiBaoCao.Add(new CheckBoxItem { IsChecked = true, DisplayItem = Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_DONVI0 });
                    IsBaoCaoDuToanTongHop = true;
                    break;
                case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_UOC_THUC_HIEN:
                    LoaiBaoCao.Add(new CheckBoxItem { IsChecked = true, DisplayItem = Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN });
                    LoaiBaoCao.Add(new CheckBoxItem { IsChecked = false, DisplayItem = Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_UOC_THUC_HIEN });
                    IsBaoCaoDuToanTongHop = false;
                    break;
                default:
                    IsBaoCaoDuToanTongHop = false;
                    break;
            }

            foreach (var item in LoaiBaoCao)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        LoadDonVi();
                        OnPropertyChanged(nameof(IsEnableListDonVi));
                    }
                };
            }

            DataKieuGiay = new ObservableCollection<ComboboxItem>();
            DataKieuGiay.Add(new ComboboxItem { ValueItem = KieuGiay.BIEU_TRINH_KY, DisplayItem = KieuGiay.BIEU_TRINH_KY });
            DataKieuGiay.Add(new ComboboxItem { ValueItem = KieuGiay.PHU_LUC, DisplayItem = KieuGiay.PHU_LUC });
            SelectedKieuGiay = DataKieuGiay.FirstOrDefault();

            DataDonViTinh = new ObservableCollection<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh == null || listDonViTinh.Count <= 0)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            foreach (var dvt in listDonViTinh)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = dvt.SGiaTri.ToString(), DisplayItem = dvt.STen });
            }
            SelectedDonViTinh = DataDonViTinh.ElementAt(0);
            OnPropertyChanged(nameof(IsEnableListDonVi));
        }


        public void LoadKhois()
        {
            var khoiItems = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Tất cả", ValueItem = TypeKhoi.TAT_CA.ToString()},
                new ComboboxItem {DisplayItem = "Doanh nghiệp", ValueItem = TypeKhoi.DOANH_NGHIEP.ToString()},
                new ComboboxItem {DisplayItem = "Dự toán", ValueItem = TypeKhoi.DU_TOAN.ToString()},
                new ComboboxItem {DisplayItem = "Bệnh viện tự chủ", ValueItem = TypeKhoi.BENH_VIEN.ToString()},
            };
            _khoiItems = new ObservableCollection<ComboboxItem>(khoiItems);
            _khoiSelected = _khoiItems.ElementAt(0);
        }

        private void LoadBudgetSourceTypes()
        {
            BudgetSourceTypes = new ObservableCollection<ComboboxItem> {
                new ComboboxItem() { DisplayItem = "Tất cả", ValueItem = TypeLoaiNNS.TAT_CA.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách dự toán", ValueItem = TypeLoaiNNS.DU_TOAN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách bệnh viện tự chủ", ValueItem = TypeLoaiNNS.BENH_VIEN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách doanh nghiệp", ValueItem = TypeLoaiNNS.DOANH_NGHIEP.ToString() }
            };

            BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(0);
        }

        private void LoadDonVi()
        {
            DonVi donvi0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            List<NguoiDungDonVi> listNguoiDungDonVi = GetListDonViPhanQuyen();
            int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");

            if (Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN.Equals(TypeReport) || Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_UOC_THUC_HIEN.Equals(TypeReport))
            {
                List<DonVi> listDonvi = new List<DonVi>();
                if (IsInTheoTongHop)
                {
                    listDonvi.Add(donvi0);
                }
                else
                {
                    var lstIdDonViDuocXem = ListIdDonViDuocXem();
                    var predicateDv = PredicateBuilder.True<DonVi>();
                    predicateDv = predicateDv.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                    predicateDv = predicateDv.And(x => !x.Loai.Equals(LoaiDonVi.ROOT));
                    predicateDv = predicateDv.And(x => lstIdDonViDuocXem.Contains(x.IIDMaDonVi));
                    if (_khoiSelected != null && !_khoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
                    {
                        predicateDv = predicateDv.And(x => !string.IsNullOrEmpty(x.Khoi) && x.Khoi.Equals(_khoiSelected.ValueItem));
                    }
                    listDonvi = _nsDonViService.FindByCondition(predicateDv).ToList();
                }
                ListDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonvi);
            }
            else
            {
                List<DonVi> listDonvi = _nsDonViService.FindDonViHasDataSktSoLieuChiTiet(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                _sessionService.Current.Budget, LoaiChungTu, loaiNNS).ToList();
                if (listNguoiDungDonVi != null && donvi0 != null && !listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(donvi0.IIDMaDonVi))
                {
                    listDonvi = listDonvi.Where(x => listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(x.IIDMaDonVi)).ToList();
                }
                ListDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonvi);
            }
            _listDonViView = CollectionViewSource.GetDefaultView(ListDonVi);
            _listDonViView.Filter = ListDonViFilter;
            foreach (var model in ListDonVi)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked) && !IsCalculating)
                    {
                        OnPropertyChanged(nameof(SelectedCountDonVi));
                        OnPropertyChanged(nameof(SelectAllDonVi));
                        OnPropertyChanged(nameof(IsEnableButtonPrint));
                        LoadBudgetIndexes();
                    }
                };
            }
            OnPropertyChanged(nameof(IsEnableButtonPrint));
            OnPropertyChanged(nameof(ListDonVi));
            OnPropertyChanged(nameof(SelectedCountDonVi));
        }

        private List<string> ListIdDonViDuocXem()
        {
            string loaiChungTu = string.Empty;
            loaiChungTu = (VoucherTypeSelected?.ValueItem == VoucherType.NSSD_Key) ? VoucherType.NSSD_Key : VoucherType.NSBD_Key;
            int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");

            List<DonViPlanBeginYearQuery> data = _nsDonViService.FindPlanBeginYearByConditon
                (_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, loaiChungTu, loaiNNS, _sessionService.Current.Principal).ToList();
            return data.Select(x => x.Id_DonVi).Distinct().ToList();
        }

        private void LoadVoucherTypes()
        {
            var voucherTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key},
            };

            VoucherTypes = new ObservableCollection<ComboboxItem>(voucherTypes);
            VoucherTypeSelected = VoucherTypes.ElementAt(0);
        }

        public int GetDonViTinh()
        {
            if (SelectedDonViTinh == null || string.IsNullOrEmpty(SelectedDonViTinh.ValueItem))
                return 1;
            return int.Parse(SelectedDonViTinh.ValueItem);
        }

        private bool ListDonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchDonVi))
            {
                return true;
            }
            return obj is CheckBoxItem item && item.DisplayItem.ToLower().Contains(_searchDonVi!.ToLower());
        }

        private void CalculateTotal()
        {
            TongQuyetToan = 0;
            TongDuToan = 0;
            TongTuChi = 0;
            TongUocThucHien = 0;
            TongDonVi1 = 0;
            TongDonVi2 = 0;
            TongDonVi3 = 0;
            TongDonVi4 = 0;
            TongDonVi5 = 0;
            TongDonVi6 = 0;
            TongDonVi1_UocThucHien = 0;
            TongDonVi2_UocThucHien = 0;
            TongDonVi3_UocThucHien = 0;
            List<ReportDuToanDauNamTongHopQuery> listChildren = DataReportTongHop.Where(x => !x.IsHangCha).ToList();
            foreach (ReportDuToanDauNamTongHopQuery item in listChildren)
            {
                TongQuyetToan += item.QuyetToan.HasValue ? item.QuyetToan.Value : 0;
                TongDuToan += item.DuToan.HasValue ? item.DuToan.Value : 0;
                TongTuChi += item.TuChi.HasValue ? item.TuChi.Value : 0;
                TongUocThucHien += item.UocThucHien.HasValue ? item.UocThucHien.Value : 0;
            };
        }

        private void CalculateDataLNS()
        {
            DataReportTongHop.Where(x => x.IsHangCha).Select(x => { x.QuyetToan = 0; x.DuToan = 0; x.TuChi = 0; x.UocThucHien = 0; return x; }).ToList();
            foreach (var item in DataReportTongHop.Where(x => !x.IsHangCha && (x.QuyetToan != 0 || x.DuToan != 0 || x.TuChi != 0 || x.UocThucHien != 0
                        || x.TuChiDonVi1 != 0 || x.TuChiDonVi2 != 0 || x.TuChiDonVi3 != 0 || x.TuChiDonVi4 != 0 || x.TuChiDonVi5 != 0 || x.TuChiDonVi6 != 0
                        || x.UocThucHienDonVi1 != 0 || x.UocThucHienDonVi2 != 0 || x.UocThucHienDonVi3 != 0)))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateDataLNSPage(ref List<ReportDuToanDauNamTongHopQuery> dataPage)
        {
            dataPage.Where(x => x.IsHangCha).Select(x => { x.QuyetToan = 0; x.DuToan = 0; x.TuChi = 0; x.UocThucHien = 0; return x; }).ToList();
            foreach (var item in dataPage.Where(x => (x.QuyetToan != 0 || x.DuToan != 0 || x.TuChi != 0 || x.UocThucHien != 0
                        || x.TuChiDonVi1 != 0 || x.TuChiDonVi2 != 0 || x.TuChiDonVi3 != 0 || x.TuChiDonVi4 != 0 || x.TuChiDonVi5 != 0 || x.TuChiDonVi6 != 0
                        || x.UocThucHienDonVi1 != 0 || x.UocThucHienDonVi2 != 0 || x.UocThucHienDonVi3 != 0)))
            {
                CalculateParentPage(ref dataPage, item, item);
            }
        }

        private void CalculateParentPage(ref List<ReportDuToanDauNamTongHopQuery> dataPage, ReportDuToanDauNamTongHopQuery currentItem, ReportDuToanDauNamTongHopQuery selfItem)
        {
            var parentItem = dataPage.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.QuyetToan += selfItem.QuyetToan;
            parentItem.DuToan += selfItem.DuToan;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.UocThucHien += selfItem.UocThucHien;

            parentItem.TuChiDonVi1 += selfItem.TuChiDonVi1;
            parentItem.TuChiDonVi2 += selfItem.TuChiDonVi2;
            parentItem.TuChiDonVi3 += selfItem.TuChiDonVi3;
            parentItem.TuChiDonVi4 += selfItem.TuChiDonVi4;
            parentItem.TuChiDonVi5 += selfItem.TuChiDonVi5;
            parentItem.TuChiDonVi6 += selfItem.TuChiDonVi6;
            parentItem.UocThucHienDonVi1 += selfItem.UocThucHienDonVi1;
            parentItem.UocThucHienDonVi2 += selfItem.UocThucHienDonVi2;
            parentItem.UocThucHienDonVi3 += selfItem.UocThucHienDonVi3;
            CalculateParentPage(ref dataPage, parentItem, selfItem);
        }

        private void CalculateParent(ReportDuToanDauNamTongHopQuery currentItem, ReportDuToanDauNamTongHopQuery selfItem)
        {
            var parentItem = DataReportTongHop.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.QuyetToan += selfItem.QuyetToan;
            parentItem.DuToan += selfItem.DuToan;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.UocThucHien += selfItem.UocThucHien;
            parentItem.TuChiDonVi1 += selfItem.TuChiDonVi1;
            parentItem.TuChiDonVi2 += selfItem.TuChiDonVi2;
            parentItem.TuChiDonVi3 += selfItem.TuChiDonVi3;
            parentItem.TuChiDonVi4 += selfItem.TuChiDonVi4;
            parentItem.TuChiDonVi5 += selfItem.TuChiDonVi5;
            parentItem.TuChiDonVi6 += selfItem.TuChiDonVi6;
            parentItem.UocThucHienDonVi1 += selfItem.UocThucHienDonVi1;
            parentItem.UocThucHienDonVi2 += selfItem.UocThucHienDonVi2;
            parentItem.UocThucHienDonVi3 += selfItem.UocThucHienDonVi3;
            CalculateParent(parentItem, selfItem);
        }

        private void PrintExcel()
        {
            try
            {
                string loaiBaoCao = LoaiBaoCao.Where(n => n.IsChecked).FirstOrDefault().DisplayItem;
                switch (loaiBaoCao)
                {
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI:
                        PrintReportDuToanChiTietDonVi(ExportType.EXCEL);
                        break;
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_DONVI0:
                        if (IsInTheoTongHop)
                        {
                            PrintReportDuToanInTheoChungTuTongHop(ExportType.EXCEL);
                        }
                        else
                        {
                            PrintReportDuToanTongHop(ExportType.EXCEL);
                        }
                        break;
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN:
                        PrintReportDuToan(ExportType.EXCEL);
                        break;
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_UOC_THUC_HIEN:
                        PrintReportDuToanUocThucHien(ExportType.EXCEL);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void PrintPDF()
        {
            try
            {
                string loaiBaoCao = LoaiBaoCao.Where(n => n.IsChecked).FirstOrDefault().DisplayItem;
                switch (loaiBaoCao)
                {
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI:
                        PrintReportDuToanChiTietDonVi(ExportType.PDF);
                        break;
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_DONVI0:
                        if (IsInTheoTongHop)
                        {
                            PrintReportDuToanInTheoChungTuTongHop(ExportType.PDF);
                        }
                        else
                        {
                            PrintReportDuToanTongHop(ExportType.PDF);
                        }
                        break;
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN:
                        PrintReportDuToan(ExportType.PDF);
                        break;
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_UOC_THUC_HIEN:
                        PrintReportDuToanUocThucHien(ExportType.PDF);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnPrintBrowser()
        {
            try
            {
                string loaiBaoCao = LoaiBaoCao.Where(n => n.IsChecked).FirstOrDefault().DisplayItem;
                switch (loaiBaoCao)
                {
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI:
                        PrintReportDuToanChiTietDonVi(ExportType.PDF);
                        break;
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_DONVI0:
                        if (IsInTheoTongHop)
                        {
                            PrintReportDuToanInTheoChungTuTongHop(ExportType.PDF);
                        }
                        else
                        {
                            PrintReportDuToanTongHop(ExportType.PDF);
                        }
                        break;
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN:
                        PrintReportDuToan(ExportType.PDF);
                        break;
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_UOC_THUC_HIEN:
                        PrintReportDuToanUocThucHien(ExportType.PDF);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadBudgetIndexes()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<string> listIdDonVi = ListDonVi.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList();
                string listDonViSelected = string.Join(",", listIdDonVi);
                int donViTinh = GetDonViTinh();
                bool isPrintPhuLuc = SelectedKieuGiay != null && SelectedKieuGiay.ValueItem == KieuGiay.PHU_LUC;
                int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");

                var data = _sktSoLieuService.GetDataReportDuToanDauNamTongHop(_sessionService.Current.YearOfWork, listDonViSelected, LoaiChungTu, loaiNNS, donViTinh).ToList();
                if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                {
                    var mucluc = _iNsMucLucNganSachService.FindByListLnsDonVi(data.Select(x => x.LNS).ToList(), _sessionService.Current.YearOfWork);
                    var listLNS = mucluc.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns).ToList();
                    data = data.Where(x => listLNS.Contains(x.LNS)).ToList();
                }
                var lns = StringUtils.GetXauNoiMaParentOptimize(data.Select(data => data.LNS)).ToHashSet();
                var listMucLuc = _iNsMucLucNganSachService.FindByCondition(x => x.NamLamViec == _sessionService.Current.YearOfWork
                                            && lns.Contains(x.Lns)
                                            && SelectedBQuanLy != null
                                            && string.IsNullOrEmpty(x.L)
                                            && (string.IsNullOrEmpty(SelectedBQuanLy.ValueItem) || SelectedBQuanLy.ValueItem == x.IdPhongBan)).Distinct().ToList();

                e.Result = listMucLuc;
            }, (s, e) =>
            {
                IsLoading = false;
                List<NsMucLucNganSach> listMLNS = e.Result as List<NsMucLucNganSach> ?? new List<NsMucLucNganSach>();
                BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMLNS);

                _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
                _listBudgetIndex.Filter = ListBudgetIndexFilter;
                foreach (var model in BudgetIndexes)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected) && !IsCalculating)
                        {
                            IsCalculating = true;
                            SetCheckChildren(BudgetIndexes, model);
                            SetCheckParent(BudgetIndexes, model);
                            IsCalculating = false;
                            OnPropertyChanged(nameof(IsExportEnable));
                            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                            OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                        }
                    };
                }
                OnPropertyChanged(nameof(IsExportEnable));
                OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
            });

        }

        private void SetCheckChildren(ObservableCollection<NsMuclucNgansachModel> items, NsMuclucNgansachModel item)
        {
            foreach (var e in items)
            {
                if (e.MlnsIdParent == item.MlnsId)
                {
                    e.IsSelected = item.IsSelected;
                    SetCheckChildren(items, e);
                }
            }
        }

        private void SetCheckParent(ObservableCollection<NsMuclucNgansachModel> items, NsMuclucNgansachModel item)
        {
            foreach (var e in items)
            {
                if (e.MlnsId == item.MlnsIdParent)
                {
                    e.IsSelected = items.Where(x => x.MlnsIdParent == item.MlnsIdParent).All(x => x.IsSelected);
                    SetCheckParent(items, e);
                }
            }
        }

        private bool ListBudgetIndexFilter(object obj)
        {
            bool result = true;
            var item = (NsMuclucNgansachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchBudgetIndexText))
                result = item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private string GetIdChungTuChild(string idDonVi)
        {
            var predicateSummary = PredicateBuilder.True<NsDtdauNamChungTu>();
            predicateSummary = predicateSummary.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicateSummary = predicateSummary.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicateSummary = predicateSummary.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicateSummary = predicateSummary.And(x => x.IIdMaDonVi == idDonVi);
            predicateSummary = predicateSummary.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
            NsDtdauNamChungTu chungTu = new NsDtdauNamChungTu();
            chungTu = _sktChungTuService.FindByCondition(predicateSummary).FirstOrDefault();
            List<string> listChungTu = new List<string>();
            if (chungTu != null && !string.IsNullOrEmpty(chungTu.SDSSoChungTuTongHop))
            {
                listChungTu = chungTu.SDSSoChungTuTongHop.Split(",").ToList();
            }
            if (chungTu != null)
            {
                if (listChungTu != null && listChungTu.Count > 0)
                {
                    var predicate = PredicateBuilder.True<NsDtdauNamChungTu>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                    predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
                    predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                    predicate = predicate.And(x => listChungTu.Contains(x.SSoChungTu));
                    predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
                    List<NsDtdauNamChungTu> chungTuChild = _sktChungTuService.FindByCondition(predicate).ToList();
                    if (chungTuChild != null && chungTuChild.Count > 0)
                    {
                        return string.Join(",", chungTuChild.Select(n => n.Id.ToString()).ToList());
                    }
                }
            }
            return string.Empty;
        }

        private void PrintReportDuToan(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_DAUNAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    ExcelFile xlsFile;

                    List<string> listIdDonVi = ListDonVi.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList();
                    string listDonViSelected = string.Join(",", listIdDonVi);
                    int donViTinh = GetDonViTinh();
                    bool isPrintPhuLuc = SelectedKieuGiay != null && SelectedKieuGiay.ValueItem == KieuGiay.PHU_LUC;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");

                    DataReportTongHop = _sktSoLieuService.GetDataReportDuToanDauNamTongHop(_sessionService.Current.YearOfWork, listDonViSelected, LoaiChungTu, loaiNNS, donViTinh).ToList();
                    if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                    {
                        var lns = _iNsMucLucNganSachService.FindByListLnsDonVi(DataReportTongHop.Select(x => x.LNS).ToList(), _sessionService.Current.YearOfWork);
                        var listLNS = lns.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns).ToList();
                        DataReportTongHop = DataReportTongHop.Where(x => listLNS.Contains(x.LNS)).ToList();
                    }
                    List<ReportDuToanDauNamTongHopQuery> listHeader = new List<ReportDuToanDauNamTongHopQuery>();
                    CalculateTotal();
                    IEnumerable<NsMucLucNganSach> lstmuclucngansach = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
                    var muclucngansach = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
                    if (DataReportTongHop != null && DataReportTongHop.Count > 0)
                    {
                        List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionService.Current.YearOfWork,
                            string.Join(",", DataReportTongHop.Select(n => n.XauNoiMa).ToList())).ToList();
                        if (listParent != null && listParent.Count > 0)
                        {
                            foreach (NsMucLucNganSach item in listParent)
                            {
                                listHeader.Add(new ReportDuToanDauNamTongHopQuery
                                {
                                    LNS = item.Lns,
                                    L = item.L,
                                    K = item.K,
                                    M = item.M,
                                    TM = item.Tm,
                                    TTM = item.Ttm,
                                    NG = item.Ng,
                                    TNG = item.Tng,
                                    TNG1 = item.Tng1,
                                    TNG2 = item.Tng2,
                                    TNG3 = item.Tng3,
                                    MoTa = item.MoTa,
                                    QuyetToan = 0,
                                    DuToan = 0,
                                    TuChi = 0,
                                    UocThucHien = 0,
                                    IsHangCha = true,
                                    XauNoiMa = item.XauNoiMa,
                                    MlnsId = item.MlnsId,
                                    MlnsIdParent = item.MlnsIdParent
                                });
                            }
                            foreach (ReportDuToanDauNamTongHopQuery item in DataReportTongHop)
                            {
                                var mlns = muclucngansach.Where(x => x.XauNoiMa == item.XauNoiMa).FirstOrDefault();
                                int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                                if (index >= 0)
                                {
                                    //item.MlnsIdParent = listHeader[index].MlnsId;
                                    if (item != null)
                                    {
                                        item.TNG = mlns.Tng == null ? String.Empty : mlns.Tng;
                                        item.TNG1 = mlns.Tng1 == null ? String.Empty : mlns.Tng1;
                                        item.TNG2 = mlns.Tng2 == null ? String.Empty : mlns.Tng2;
                                        item.TNG3 = mlns.Tng3 == null ? String.Empty : mlns.Tng3;
                                    }
                                    else
                                    {
                                        item.TNG = String.Empty;
                                        item.TNG1 = String.Empty;
                                        item.TNG2 = String.Empty;
                                        item.TNG3 = String.Empty;
                                    }

                                    listHeader.Insert(index + 1, item);
                                }
                            }
                        }

                        DataReportTongHop = new List<ReportDuToanDauNamTongHopQuery>(listHeader);
                        DataReportTongHop = DataReportTongHop.OrderBy(n => n.XauNoiMa).ToList();
                        DataReportTongHop.Where(n => !n.IsHangCha).Select(n => { n.LNS = string.Empty; n.M = string.Empty; n.TM = string.Empty; return n; }).ToList();
                        DataReportTongHop.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.M)).Select(n => { n.LNS = string.Empty; return n; }).ToList();
                        DataReportTongHop.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.TM)).Select(n => { n.M = string.Empty; return n; }).ToList();
                    }

                    //tinh tung don vi
                    //page1
                    string tenDonVi1 = string.Empty;
                    string tenDonVi2 = string.Empty;
                    string tenDonVi3 = string.Empty;
                    string tenDonVi4 = string.Empty;
                    string tenDonVi5 = string.Empty;
                    string tenDonVi6 = string.Empty;
                    for (int i = 0; i < listIdDonVi.Count; i++)
                    {
                        if (i > 5) break;
                        switch (i)
                        {
                            case 0:
                                tenDonVi1 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 1:
                                tenDonVi2 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 2:
                                tenDonVi3 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 3:
                                tenDonVi4 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 4:
                                tenDonVi5 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 5:
                                tenDonVi6 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            default:
                                break;
                        }
                        List<ReportDuToanDauNamTongHopQuery> dataDonvi1 = _sktSoLieuService.GetDataReportDuToanDauNamTongHop(_sessionService.Current.YearOfWork, listIdDonVi[i], LoaiChungTu, loaiNNS, donViTinh).ToList();
                        if (dataDonvi1 != null && dataDonvi1.Count > 0)
                        {
                            switch (i)
                            {
                                case 0:
                                    TongDonVi1 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    break;
                                case 1:
                                    TongDonVi2 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    break;
                                case 2:
                                    TongDonVi3 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    break;
                                case 3:
                                    TongDonVi4 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    break;
                                case 4:
                                    TongDonVi5 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    break;
                                case 5:
                                    TongDonVi6 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    break;
                                default:
                                    break;
                            }
                        }
                        foreach (ReportDuToanDauNamTongHopQuery item in dataDonvi1)
                        {
                            switch (i)
                            {
                                case 0:
                                    DataReportTongHop.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi1 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    break;
                                case 1:
                                    DataReportTongHop.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi2 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    break;
                                case 2:
                                    DataReportTongHop.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi3 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    break;
                                case 3:
                                    DataReportTongHop.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi4 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    break;
                                case 4:
                                    DataReportTongHop.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi5 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    break;
                                case 5:
                                    DataReportTongHop.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi6 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    CalculateDataLNS();
                    //Hien thi cot toi muc
                    if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "NG")
                    {
                        DataReportTongHop = DataReportTongHop.Where(x => x.TNG == "" && x.TNG1 == "" && x.TNG2 == "" && x.TNG3 == "").ToList();
                        DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG")
                    {
                        DataReportTongHop = DataReportTongHop.Where(x => x.TNG1 == "" && x.TNG2 == "" && x.TNG3 == "").ToList();
                        DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG1")
                    {
                        DataReportTongHop = DataReportTongHop.Where(x => x.TNG2 == "" && x.TNG3 == "").ToList();
                        DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG2")
                    {
                        DataReportTongHop = DataReportTongHop.Where(x => x.TNG3 == "").ToList();
                        DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                    }

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", GetLevelTitle(_dmChuKy, 1));
                    data.Add("Cap2", GetLevelTitle(_dmChuKy, 2));
                    data.Add("TieuDe1", TieuDe1.ToUpper());
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    data.Add("TongQuyetToan", TongQuyetToan);
                    data.Add("TongDuToan", TongDuToan);
                    data.Add("TongTuChi1", TongDonVi1);
                    data.Add("TongTuChi2", TongDonVi2);
                    data.Add("TongTuChi3", TongDonVi3);
                    data.Add("TongTuChi4", TongDonVi4);
                    data.Add("TongTuChi5", TongDonVi5);
                    data.Add("TongTuChi6", TongDonVi6);
                    data.Add("TongTuChi", TongTuChi);
                    data.Add("Items", DataReportTongHop);
                    data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                    data.Add("headercol1", _sessionService.Current.YearOfWork - 2);
                    data.Add("headercol2", _sessionService.Current.YearOfWork - 1);
                    data.Add("headercol3", _sessionService.Current.YearOfWork);
                    data.Add("TenDonVi1", tenDonVi1);
                    data.Add("TenDonVi2", tenDonVi2);
                    data.Add("TenDonVi3", tenDonVi3);
                    data.Add("TenDonVi4", tenDonVi4);
                    data.Add("TenDonVi5", tenDonVi5);
                    data.Add("TenDonVi6", tenDonVi6);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    data.Add("TongTienBangChu", StringUtils.NumberToText(TongTuChi * donViTinh, true));
                    data.Add("ThoiGian", string.Format("{0}, Ngày {1} tháng {2} năm {3}", _diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                    templateFileName = isPrintPhuLuc ? GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_TONGHOPDUTOAN_PHULUC_ALL) : GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_TONGHOPDUTOAN_ALL);
                    fileNamePrefix = "rptDuToanDauNamTongHop";
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                    List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(chiTietToi).Select(x => x + 1).ToList();
                    xlsFile = _exportService.Export<ReportDuToanDauNamTongHopQuery>(templateFileName, data, hideColumns);
                    exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));

                    //page2
                    int numberPage = listIdDonVi.Count() / 6;
                    if (listIdDonVi.Count() % 6 > 0)
                    {
                        numberPage++;
                    }

                    for (int p = 2; p < numberPage + 2; p++)
                    {
                        bool isExportFile = false;
                        TongDonVi1 = 0;
                        TongDonVi2 = 0;
                        TongDonVi3 = 0;
                        TongDonVi4 = 0;
                        TongDonVi5 = 0;
                        TongDonVi6 = 0;
                        tenDonVi1 = string.Empty;
                        tenDonVi2 = string.Empty;
                        tenDonVi3 = string.Empty;
                        tenDonVi4 = string.Empty;
                        tenDonVi5 = string.Empty;
                        tenDonVi6 = string.Empty;
                        int sttPage = p - 2;
                        List<ReportDuToanDauNamTongHopQuery> dataPage1 = new List<ReportDuToanDauNamTongHopQuery>();
                        foreach (ReportDuToanDauNamTongHopQuery item in DataReportTongHop)
                        {
                            var tng = string.Empty;
                            var tng1 = string.Empty;
                            var tng2 = string.Empty;
                            var tng3 = string.Empty;
                            var mlns = muclucngansach.Where(x => x.XauNoiMa == item.XauNoiMa).FirstOrDefault();
                            if (mlns != null)
                            {
                                tng = mlns.Tng;
                                tng1 = mlns.Tng1;
                                tng2 = mlns.Tng2;
                                tng3 = mlns.Tng3;
                            }

                            dataPage1.Add(new ReportDuToanDauNamTongHopQuery
                            {
                                LNS = item.LNS,
                                M = item.M,
                                TM = item.TM,
                                TTM = item.TTM,
                                NG = item.NG,
                                TNG = tng,
                                TNG1 = tng1,
                                TNG2 = tng2,
                                TNG3 = tng3,
                                MoTa = item.MoTa,
                                IsHangCha = item.IsHangCha,
                                MlnsId = item.MlnsId,
                                XauNoiMa = item.XauNoiMa,
                                MlnsIdParent = item.MlnsIdParent
                            });
                        }
                        dataPage1.Select(n =>
                        {
                            n.QuyetToan = 0; n.DuToan = 0; n.TuChi = 0; n.UocThucHien = 0;
                            n.TuChiDonVi1 = 0; n.TuChiDonVi2 = 0; n.TuChiDonVi3 = 0; n.TuChiDonVi4 = 0; n.TuChiDonVi5 = 0;
                            n.TuChiDonVi6 = 0; return n;
                        }).ToList();
                        int countDonVi = 0;

                        for (int i = sttPage * 6 + 6; i <= sttPage * 6 + 11; i++)
                        {
                            if (i >= listIdDonVi.Count()) break;
                            switch (countDonVi)
                            {
                                case 0:
                                    tenDonVi1 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 1:
                                    tenDonVi2 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 2:
                                    tenDonVi3 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 3:
                                    tenDonVi4 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 4:
                                    tenDonVi5 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 5:
                                    tenDonVi6 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                default:
                                    break;
                            }
                            List<ReportDuToanDauNamTongHopQuery> dataDonvi1 = _sktSoLieuService.GetDataReportDuToanDauNamTongHop(_sessionService.Current.YearOfWork, listIdDonVi[i], LoaiChungTu, loaiNNS, donViTinh).ToList();
                            if (dataDonvi1 != null && dataDonvi1.Count > 0)
                            {
                                isExportFile = true;
                                switch (countDonVi)
                                {
                                    case 0:
                                        TongDonVi1 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        break;
                                    case 1:
                                        TongDonVi2 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        break;
                                    case 2:
                                        TongDonVi3 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        break;
                                    case 3:
                                        TongDonVi4 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        break;
                                    case 4:
                                        TongDonVi5 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        break;
                                    case 5:
                                        TongDonVi6 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            foreach (ReportDuToanDauNamTongHopQuery item in dataDonvi1)
                            {
                                switch (countDonVi)
                                {
                                    case 0:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi1 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        break;
                                    case 1:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi2 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        break;
                                    case 2:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi3 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        break;
                                    case 3:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi4 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        break;
                                    case 4:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi5 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        break;
                                    case 5:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi6 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        break;
                                    default:
                                        break;
                                }

                            }
                            countDonVi++;
                        }
                        if (isExportFile)
                        {
                            //Hien thi cot toi muc
                            if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "NG")
                            {
                                dataPage1 = dataPage1.Where(x => x.TNG == "" && x.TNG1 == "" && x.TNG2 == "" && x.TNG3 == "").ToList();
                                dataPage1.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                            }
                            else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG")
                            {
                                dataPage1 = dataPage1.Where(x => x.TNG1 == "" && x.TNG2 == "" && x.TNG3 == "").ToList();
                                dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                            }
                            else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG1")
                            {
                                dataPage1 = dataPage1.Where(x => x.TNG2 == "" && x.TNG3 == "").ToList();
                                dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                            }
                            else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG2")
                            {
                                dataPage1 = dataPage1.Where(x => x.TNG3 == "").ToList();
                                dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                            }

                            CalculateDataLNSPage(ref dataPage1);
                            Dictionary<string, object> dataPage = new Dictionary<string, object>();
                            dataPage.Add("FormatNumber", formatNumber);
                            dataPage.Add("Items", dataPage1);
                            dataPage.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                            dataPage.Add("TongTuChi1", TongDonVi1);
                            dataPage.Add("TongTuChi2", TongDonVi2);
                            dataPage.Add("TongTuChi3", TongDonVi3);
                            dataPage.Add("TongTuChi4", TongDonVi4);
                            dataPage.Add("TongTuChi5", TongDonVi5);
                            dataPage.Add("TongTuChi6", TongDonVi6);
                            dataPage.Add("TenDonVi1", tenDonVi1);
                            dataPage.Add("TenDonVi2", tenDonVi2);
                            dataPage.Add("TenDonVi3", tenDonVi3);
                            dataPage.Add("TenDonVi4", tenDonVi4);
                            dataPage.Add("TenDonVi5", tenDonVi5);
                            dataPage.Add("TenDonVi6", tenDonVi6);

                            templateFileName = string.Format(GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_TONGHOPDUTOAN_TO2_ALL), p);
                            fileNamePrefix = string.Format("rptDuToanDauNamTongHop_To{0}", p);
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var chiTietToiTo2 = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                            List<int> hideColumnsTo2 = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(chiTietToi).Select(x => x + 1).ToList();
                            xlsFile = _exportService.Export<ReportDuToanDauNamTongHopQuery>(templateFileName, dataPage, hideColumnsTo2);
                            exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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



        private void PrintReportDuToanChiTietDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_DAUNAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<string> listIdDonVi = ListDonVi.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList();
                    if (listIdDonVi == null || listIdDonVi.Count <= 0)
                    {
                        MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                        return;
                    }
                    int donViTinh = GetDonViTinh();
                    bool isPrintPhuLuc = SelectedKieuGiay != null && SelectedKieuGiay.ValueItem == KieuGiay.PHU_LUC;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");

                    IEnumerable<NsMucLucNganSach> lstmuclucngansach = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();

                    foreach (string idDonVi in listIdDonVi)
                    {
                        DataReportTongHop = _sktSoLieuService.GetDataReportDuToanDauNamTongHop(_sessionService.Current.YearOfWork, idDonVi, LoaiChungTu, loaiNNS, donViTinh).ToList();
                        if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                        {
                            var lns = _iNsMucLucNganSachService.FindByListLnsDonVi(DataReportTongHop.Select(x => x.LNS).ToList(), _sessionService.Current.YearOfWork);
                            var listLNS = lns.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns).ToList();
                            DataReportTongHop = DataReportTongHop.Where(x => listLNS.Contains(x.LNS)).ToList();
                        }

                        List<ReportDuToanDauNamTongHopQuery> listHeader = new List<ReportDuToanDauNamTongHopQuery>();
                        CalculateTotal();

                        if (DataReportTongHop != null && DataReportTongHop.Count > 0)
                        {
                            List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionService.Current.YearOfWork,
                                                                        string.Join(",", DataReportTongHop.Select(n => n.XauNoiMa).ToList())).ToList();
                            listParent = listParent.Where(n => !DataReportTongHop.Select(x => x.XauNoiMa).ToList().Contains(n.XauNoiMa)).ToList();
                            if (listParent != null && listParent.Count > 0)
                            {
                                foreach (NsMucLucNganSach item in listParent)
                                {
                                    listHeader.Add(new ReportDuToanDauNamTongHopQuery
                                    {
                                        LNS = item.Lns,
                                        L = item.L,
                                        K = item.K,
                                        M = item.M,
                                        TM = item.Tm,
                                        TTM = item.Ttm,
                                        NG = item.Ng,
                                        TNG = item.Tng,
                                        TNG1 = item.Tng1,
                                        TNG2 = item.Tng2,
                                        TNG3 = item.Tng3,
                                        MoTa = item.MoTa,
                                        QuyetToan = 0,
                                        DuToan = 0,
                                        TuChi = 0,
                                        UocThucHien = 0,
                                        IsHangCha = true,
                                        MlnsId = item.MlnsId,
                                        MlnsIdParent = item.MlnsIdParent
                                    });
                                }
                                foreach (ReportDuToanDauNamTongHopQuery item in DataReportTongHop)
                                {
                                    var tng = string.Empty;
                                    var tng1 = string.Empty;
                                    var tng2 = string.Empty;
                                    var tng3 = string.Empty;
                                    var mlns = lstmuclucngansach.Where(x => x.XauNoiMa == item.XauNoiMa).FirstOrDefault();
                                    if (mlns != null)
                                    {
                                        tng = mlns.Tng;
                                        tng1 = mlns.Tng1;
                                        tng2 = mlns.Tng2;
                                        tng3 = mlns.Tng3;
                                    }

                                    //int index = listHeader.IndexOf(listHeader.Where(n => n.TM == item.TM && string.IsNullOrEmpty(n.TTM) && !string.IsNullOrEmpty(n.TM)).FirstOrDefault());
                                    int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                                    if (index >= 0)
                                    {
                                        //item.MlnsIdParent = listHeader[index].MlnsId;
                                        item.TNG = tng != null ? tng : String.Empty;
                                        item.TNG1 = tng1 != null ? tng1 : String.Empty;
                                        item.TNG2 = tng2 != null ? tng2 : String.Empty;
                                        item.TNG3 = tng3 != null ? tng3 : String.Empty;
                                        listHeader.Insert(index + 1, item);
                                    }
                                }
                            }

                            listHeader = listHeader.OrderBy(n => n.LNS).ThenBy(n => n.L).ThenBy(n => n.K).ThenBy(n => n.M).ThenBy(n => n.TM).ThenBy(n => n.TTM).ThenBy(n => n.NG).ThenBy(n => n.TNG).ThenBy(n => n.TNG1).ThenBy(n => n.TNG2).ThenBy(n => n.TNG3).ToList();
                            DataReportTongHop = new List<ReportDuToanDauNamTongHopQuery>(listHeader);
                            DataReportTongHop.Where(n => !n.IsHangCha).Select(n => { n.LNS = string.Empty; n.M = string.Empty; n.TM = string.Empty; return n; }).ToList();
                            DataReportTongHop.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.M)).Select(n => { n.LNS = string.Empty; return n; }).ToList();
                            DataReportTongHop.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.TM)).Select(n => { n.M = string.Empty; return n; }).ToList();


                        }
                        CalculateDataLNS();
                        //Hien thi cot toi muc
                        if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "NG")
                        {
                            DataReportTongHop = DataReportTongHop.Where(x => x.TNG == "" && x.TNG1 == "" && x.TNG2 == "" && x.TNG3 == "").ToList();
                            DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                        }
                        else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG")
                        {
                            DataReportTongHop = DataReportTongHop.Where(x => x.TNG1 == "" && x.TNG2 == "" && x.TNG3 == "").ToList();
                            DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                        }
                        else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG1")
                        {
                            DataReportTongHop = DataReportTongHop.Where(x => x.TNG2 == "" && x.TNG3 == "").ToList();
                            DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                        }
                        else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG2")
                        {
                            DataReportTongHop = DataReportTongHop.Where(x => x.TNG3 == "").ToList();
                            DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                        }


                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", GetLevelTitle(_dmChuKy, 1));
                        data.Add("Cap2", GetLevelTitle(_dmChuKy, 2));
                        data.Add("TieuDe1", TieuDe1.ToUpper());
                        data.Add("TieuDe2", TieuDe2);
                        data.Add("TieuDe3", TieuDe3);
                        data.Add("TongQuyetToan", TongQuyetToan);
                        data.Add("TongDuToan", TongDuToan);
                        data.Add("TongUocThucHien", TongUocThucHien);
                        data.Add("TongTuChi", TongTuChi);
                        data.Add("TongTienBangChu", StringUtils.NumberToText(TongTuChi * donViTinh, true));
                        // data.Add("ThoiGian", string.Format("{0}, Ngày {1} tháng {2} năm {3}",_diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                        data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                        data.Add("Items", DataReportTongHop);
                        data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                        data.Add("headercol1", _sessionService.Current.YearOfWork - 2);
                        data.Add("headercol2", _sessionService.Current.YearOfWork - 1);
                        data.Add("headercol3", _sessionService.Current.YearOfWork);
                        data.Add("TenDonVi", ListDonVi.Where(n => n.ValueItem == idDonVi).FirstOrDefault() != null ?
                            ListDonVi.Where(n => n.ValueItem == idDonVi).FirstOrDefault().NameItem : string.Empty);
                        data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                        data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                        data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                        data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                        data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                        data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                        data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                        data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                        data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                        templateFileName = isPrintPhuLuc ? GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_CHITIETDONVI_PHULUC_ALL) : GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_CHITIETDONVI_ALL);
                        fileNamePrefix = "rptDuToanDauNamChiTiet" + "_" + idDonVi;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                        List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(chiTietToi).Select(x => x + 1).ToList();
                        var xlsFile = _exportService.Export<ReportDuToanDauNamTongHopQuery>(templateFileName, data, hideColumns);
                        exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

        private void PrintReportDuToanTongHop(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_DAUNAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<string> listIdDonVi = ListDonVi.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList();
                    if (listIdDonVi == null || listIdDonVi.Count <= 0)
                    {
                        MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                        return;
                    }
                    int donViTinh = GetDonViTinh();
                    bool isPrintPhuLuc = SelectedKieuGiay != null && SelectedKieuGiay.ValueItem == KieuGiay.PHU_LUC;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");

                    string listStringIdDonVi = string.Join(",", listIdDonVi);

                    IEnumerable<NsMucLucNganSach> lstmuclucngansach = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();

                    DataReportTongHop = _sktSoLieuService.GetDataReportDuToanDauNamTongHop(_sessionService.Current.YearOfWork, listStringIdDonVi, LoaiChungTu, loaiNNS, donViTinh).ToList();
                    var lns = BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns).ToHashSet();
                    DataReportTongHop = DataReportTongHop.Where(d => lns.Contains(d.LNS)).ToList();


                    //if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                    //{
                    //    var lns = _iNsMucLucNganSachService.FindByListLnsDonVi(DataReportTongHop.Select(x => x.LNS).ToList(), _sessionService.Current.YearOfWork);
                    //    var listLNS = lns.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns).ToList();
                    //    DataReportTongHop = DataReportTongHop.Where(x => listLNS.Contains(x.LNS)).ToList();
                    //}

                    List<ReportDuToanDauNamTongHopQuery> listHeader = new List<ReportDuToanDauNamTongHopQuery>();
                    CalculateTotal();

                    if (DataReportTongHop != null && DataReportTongHop.Count > 0)
                    {
                        List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionService.Current.YearOfWork,
                                                                    string.Join(",", DataReportTongHop.Select(n => n.XauNoiMa).ToList())).ToList();
                        listParent = listParent.Where(n => !DataReportTongHop.Select(x => x.XauNoiMa).ToList().Contains(n.XauNoiMa)).ToList();
                        if (listParent != null && listParent.Count > 0)
                        {
                            foreach (NsMucLucNganSach item in listParent)
                            {
                                listHeader.Add(new ReportDuToanDauNamTongHopQuery
                                {
                                    LNS = item.Lns,
                                    L = item.L,
                                    K = item.K,
                                    M = item.M,
                                    TM = item.Tm,
                                    TTM = item.Ttm,
                                    NG = item.Ng,
                                    TNG = item.Tng,
                                    TNG1 = item.Tng1,
                                    TNG2 = item.Tng2,
                                    TNG3 = item.Tng3,
                                    MoTa = item.MoTa,
                                    QuyetToan = 0,
                                    DuToan = 0,
                                    TuChi = 0,
                                    UocThucHien = 0,
                                    IsHangCha = true,
                                    MlnsId = item.MlnsId,
                                    MlnsIdParent = item.MlnsIdParent
                                });
                            }
                            foreach (ReportDuToanDauNamTongHopQuery item in DataReportTongHop)
                            {
                                var tng = string.Empty;
                                var tng1 = string.Empty;
                                var tng2 = string.Empty;
                                var tng3 = string.Empty;
                                var mlns = lstmuclucngansach.Where(x => x.XauNoiMa == item.XauNoiMa).FirstOrDefault();
                                if (mlns != null)
                                {
                                    tng = mlns.Tng;
                                    tng1 = mlns.Tng1;
                                    tng2 = mlns.Tng2;
                                    tng3 = mlns.Tng3;
                                }

                                //int index = listHeader.IndexOf(listHeader.Where(n => n.TM == item.TM && string.IsNullOrEmpty(n.TTM) && !string.IsNullOrEmpty(n.TM)).FirstOrDefault());
                                int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                                if (index >= 0)
                                {
                                    //item.MlnsIdParent = listHeader[index].MlnsId;
                                    item.TNG = tng != null ? tng : String.Empty;
                                    item.TNG1 = tng1 != null ? tng1 : String.Empty;
                                    item.TNG2 = tng2 != null ? tng2 : String.Empty;
                                    item.TNG3 = tng3 != null ? tng3 : String.Empty;
                                    listHeader.Insert(index + 1, item);
                                }
                            }
                        }

                        listHeader = listHeader.OrderBy(n => n.LNS).ThenBy(n => n.L).ThenBy(n => n.K).ThenBy(n => n.M).ThenBy(n => n.TM).ThenBy(n => n.TTM).ThenBy(n => n.NG).ThenBy(n => n.TNG).ThenBy(n => n.TNG1).ThenBy(n => n.TNG2).ThenBy(n => n.TNG3).ToList();
                        DataReportTongHop = new List<ReportDuToanDauNamTongHopQuery>(listHeader);
                        DataReportTongHop.Where(n => !n.IsHangCha).Select(n =>
                        {
                            n.LNS = string.Empty;
                            n.L = string.Empty;
                            n.K = string.Empty;
                            n.M = string.Empty;
                            n.TM = string.Empty;
                            return n;
                        }).ToList();

                        DataReportTongHop.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.M)).ForAll(n =>
                        {
                            var parent = DataReportTongHop.FirstOrDefault(x => x.MlnsId == n.MlnsIdParent);
                            if (parent != null && !string.IsNullOrEmpty(parent.L) && !string.IsNullOrEmpty(parent.K))
                            {
                                n.LNS = string.Empty;
                                n.L = string.Empty;
                                n.K = string.Empty;
                            }
                        });

                        DataReportTongHop.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.TM)).Select(n =>
                        {
                            n.M = string.Empty;
                            n.K = string.Empty;
                            n.L = string.Empty;
                            n.LNS = string.Empty;
                            return n;
                        }).ToList();


                    }
                    CalculateDataLNS();
                    //Hien thi cot toi muc
                    //Hien thi cot toi muc
                    switch (SelectedInToiMuc.ValueItem)
                    {
                        case nameof(MLNSFiled.NG):
                            DataReportTongHop = DataReportTongHop.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                            DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG):
                            DataReportTongHop = DataReportTongHop.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                            DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG1):
                            DataReportTongHop = DataReportTongHop.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                            DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG2):
                            DataReportTongHop = DataReportTongHop.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                            DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                            break;
                    }

                    foreach (var item in DataReportTongHop)
                    {
                        if (!string.IsNullOrEmpty(item.NG))
                        {
                            item.M = String.Empty;
                            item.TM = String.Empty;
                        }
                        if (!string.IsNullOrEmpty(item.TNG))
                        {
                            item.TTM = String.Empty;
                            item.NG = String.Empty;
                        }
                        if (!string.IsNullOrEmpty(item.TNG1))
                        {
                            item.TNG = String.Empty;
                        }
                        if (!string.IsNullOrEmpty(item.TNG2))
                        {
                            item.TNG1 = String.Empty;
                        }
                        if (!string.IsNullOrEmpty(item.TNG3))
                        {
                            item.TNG2 = String.Empty;
                        }
                    }

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", GetLevelTitle(_dmChuKy, 1));
                    data.Add("Cap2", GetLevelTitle(_dmChuKy, 2));
                    data.Add("TieuDe1", TieuDe1.ToUpper());
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    data.Add("TongQuyetToan", TongQuyetToan);
                    data.Add("TongDuToan", TongDuToan);
                    data.Add("TongUocThucHien", TongUocThucHien);
                    data.Add("TongTuChi", TongTuChi);
                    data.Add("TongTienBangChu", StringUtils.NumberToText(TongTuChi * donViTinh, true));
                    // data.Add("ThoiGian", string.Format("{0}, Ngày {1} tháng {2} năm {3}",_diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                    data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    data.Add("Items", DataReportTongHop);
                    data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                    data.Add("headercol1", _sessionService.Current.YearOfWork - 2);
                    data.Add("headercol2", _sessionService.Current.YearOfWork - 1);
                    data.Add("headercol3", _sessionService.Current.YearOfWork);
                    //data.Add("TenDonVi", ListDonVi.Where(n => listStringIdDonVi.Contains(n.ValueItem)).FirstOrDefault() != null ?
                    //    string.Join(",", ListDonVi.Where(n => listStringIdDonVi.Contains(n.ValueItem)).Select(n => n.NameItem)) : string.Empty);
                    data.Add("TenDonVi", (BudgetSourceTypeSelected.ValueItem == "0") ? "Tổng hợp nguồn ngân sách" : BudgetSourceTypeSelected.DisplayItem);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    templateFileName = isPrintPhuLuc ? GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_CHITIETDONVI_PHULUC_ALL_TONGHOP) : GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_CHITIETDONVI_ALL_TONGHOP);
                    fileNamePrefix = "rptDuToanDauNamChiTietTongHopDonVi";
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                    List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(chiTietToi).Select(x => x + 1).ToList();
                    var xlsFile = _exportService.Export<ReportDuToanDauNamTongHopQuery>(templateFileName, data, hideColumns);
                    exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));

                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

        private void PrintReportDuToanInTheoChungTuTongHop(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_DAUNAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<string> listIdDonVi = ListDonVi.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList();
                    if (listIdDonVi == null || listIdDonVi.Count <= 0)
                    {
                        MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                        return;
                    }
                    int donViTinh = GetDonViTinh();
                    bool isPrintPhuLuc = SelectedKieuGiay != null && SelectedKieuGiay.ValueItem == KieuGiay.PHU_LUC;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");

                    IEnumerable<NsMucLucNganSach> lstmuclucngansach = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();

                    string listStringIdDonVi = "";
                    List<DonViPlanBeginYearQuery> dataPlanBegin = _nsDonViService.FindPlanBeginYearByConditon
                        (_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, LoaiChungTu, loaiNNS, _sessionService.Current.Principal).ToList();

                    DonVi donVi0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);

                    if (dataPlanBegin.Any(n => n.Id_DonVi == donVi0.IIDMaDonVi))
                    {
                        var dataPlanBeginSummary = dataPlanBegin.Where(n => n.Id_DonVi == donVi0.IIDMaDonVi);
                        var dataPlanBeginNormal = dataPlanBegin.Where(n => dataPlanBeginSummary.Any(m => !string.IsNullOrEmpty(m.DSSoChungTuTongHop) &&
                                                  m.DSSoChungTuTongHop.Split(",").Contains(n.SSoChungTu))).ToList();

                        if (KhoiSelected != null)
                        {
                            if (KhoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
                            {
                                listStringIdDonVi = donVi0.IIDMaDonVi;
                                DataReportTongHop = _sktSoLieuService.GetDataReportDuToanDauNamTongHop(_sessionService.Current.YearOfWork, listStringIdDonVi, LoaiChungTu, loaiNNS, donViTinh).ToList();
                            }
                            else if ((dataPlanBeginSummary != null && !string.IsNullOrEmpty(donVi0.Khoi)) || (dataPlanBeginSummary != null && string.IsNullOrEmpty(dataPlanBeginSummary.First().DSSoChungTuTongHop)))
                            {
                                listStringIdDonVi = !string.IsNullOrEmpty(donVi0.Khoi) && donVi0.Khoi.Equals(KhoiSelected.ValueItem) ? donVi0.IIDMaDonVi : "";
                                DataReportTongHop = _sktSoLieuService.GetDataReportDuToanDauNamTongHop(_sessionService.Current.YearOfWork, listStringIdDonVi, LoaiChungTu, loaiNNS, donViTinh).ToList();
                            }
                            else
                            {
                                var listDonViChildId = dataPlanBeginNormal.Select(n => n.Id_DonVi);
                                var listDonViChild = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).Where(n => listDonViChildId.Contains(n.IIDMaDonVi));
                                if (KhoiSelected != null && !KhoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
                                {
                                    listDonViChild = listDonViChild.Where(n => !string.IsNullOrEmpty(n.Khoi) && n.Khoi.Equals(KhoiSelected.ValueItem));
                                }
                                listStringIdDonVi = string.Join(",", listDonViChild.Select(n => n.IIDMaDonVi));
                                DataReportTongHop = _sktSoLieuService.GetDataReportDuToanDauNamTongHop_2(_sessionService.Current.YearOfWork, listStringIdDonVi, LoaiChungTu, loaiNNS, donViTinh).ToList();
                            }
                        }
                    }


                    if (string.IsNullOrEmpty(listStringIdDonVi))
                    {
                        MessageBoxHelper.Warning(Resources.EmptyAgency);
                        return;
                    }

                    List<ReportDuToanDauNamTongHopQuery> listHeader = new List<ReportDuToanDauNamTongHopQuery>();

                    if (DataReportTongHop != null && DataReportTongHop.Count > 0)
                    {
                        //if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                        //{
                        //    var lns = _iNsMucLucNganSachService.FindByListLnsDonVi(DataReportTongHop.Select(x => x.LNS).ToList(), _sessionService.Current.YearOfWork);
                        //    var listLNS = lns.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns).ToList();
                        //    DataReportTongHop = DataReportTongHop.Where(x => listLNS.Contains(x.LNS)).ToList();
                        //}
                        var lns = BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns).ToHashSet();
                        DataReportTongHop = DataReportTongHop.Where(d => lns.Contains(d.LNS)).ToList();
                        CalculateTotal();

                        List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionService.Current.YearOfWork,
                                                                    string.Join(",", DataReportTongHop.Select(n => n.XauNoiMa).ToList())).ToList();
                        listParent = listParent.Where(n => !DataReportTongHop.Select(x => x.XauNoiMa).ToList().Contains(n.XauNoiMa)).ToList();
                        if (listParent != null && listParent.Count > 0)
                        {
                            foreach (NsMucLucNganSach item in listParent)
                            {
                                listHeader.Add(new ReportDuToanDauNamTongHopQuery
                                {
                                    LNS = item.Lns,
                                    L = item.L,
                                    K = item.K,
                                    M = item.M,
                                    TM = item.Tm,
                                    TTM = item.Ttm,
                                    NG = item.Ng,
                                    TNG = item.Tng,
                                    TNG1 = item.Tng1,
                                    TNG2 = item.Tng2,
                                    TNG3 = item.Tng3,
                                    MoTa = item.MoTa,
                                    QuyetToan = 0,
                                    DuToan = 0,
                                    TuChi = 0,
                                    UocThucHien = 0,
                                    IsHangCha = true,
                                    MlnsId = item.MlnsId,
                                    MlnsIdParent = item.MlnsIdParent
                                });
                            }
                            foreach (ReportDuToanDauNamTongHopQuery item in DataReportTongHop)
                            {
                                var tng = string.Empty;
                                var tng1 = string.Empty;
                                var tng2 = string.Empty;
                                var tng3 = string.Empty;
                                var mlns = lstmuclucngansach.Where(x => x.XauNoiMa == item.XauNoiMa).FirstOrDefault();
                                if (mlns != null)
                                {
                                    tng = mlns.Tng;
                                    tng1 = mlns.Tng1;
                                    tng2 = mlns.Tng2;
                                    tng3 = mlns.Tng3;
                                }

                                //int index = listHeader.IndexOf(listHeader.Where(n => n.TM == item.TM && string.IsNullOrEmpty(n.TTM) && !string.IsNullOrEmpty(n.TM)).FirstOrDefault());
                                int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                                if (index >= 0)
                                {
                                    //item.MlnsIdParent = listHeader[index].MlnsId;
                                    item.TNG = tng != null ? tng : String.Empty;
                                    item.TNG1 = tng1 != null ? tng1 : String.Empty;
                                    item.TNG2 = tng2 != null ? tng2 : String.Empty;
                                    item.TNG3 = tng3 != null ? tng3 : String.Empty;
                                    listHeader.Insert(index + 1, item);
                                }
                            }
                        }

                        listHeader = listHeader.OrderBy(n => n.LNS).ThenBy(n => n.L).ThenBy(n => n.K).ThenBy(n => n.M).ThenBy(n => n.TM).ThenBy(n => n.TTM).ThenBy(n => n.NG).ThenBy(n => n.TNG).ThenBy(n => n.TNG1).ThenBy(n => n.TNG2).ThenBy(n => n.TNG3).ToList();
                        DataReportTongHop = new List<ReportDuToanDauNamTongHopQuery>(listHeader);

                        DataReportTongHop.Where(n => !n.IsHangCha).Select(n =>
                        {
                            n.LNS = string.Empty;
                            n.L = string.Empty;
                            n.K = string.Empty;
                            n.M = string.Empty;
                            n.TM = string.Empty;
                            return n;
                        }).ToList();

                        DataReportTongHop.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.M)).ForAll(n =>
                        {
                            var parent = DataReportTongHop.FirstOrDefault(x => x.MlnsId == n.MlnsIdParent);
                            if (parent != null && !string.IsNullOrEmpty(parent.L) && !string.IsNullOrEmpty(parent.K))
                            {
                                n.LNS = string.Empty;
                                n.L = string.Empty;
                                n.K = string.Empty;
                            }
                        });

                        DataReportTongHop.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.TM)).Select(n =>
                        {
                            n.M = string.Empty;
                            n.K = string.Empty;
                            n.L = string.Empty;
                            n.LNS = string.Empty;
                            return n;
                        }).ToList();

                        /*
                        DataReportTongHop.Where(n => !n.IsHangCha).Select(n => { n.LNS = string.Empty; n.M = string.Empty; n.TM = string.Empty; return n; }).ToList();
                        DataReportTongHop.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.M)).Select(n => { n.LNS = string.Empty; return n; }).ToList();
                        DataReportTongHop.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.TM)).Select(n => { n.M = string.Empty; return n; }).ToList();
                        */

                    }
                    CalculateDataLNS();
                    //Hien thi cot toi muc
                    switch (SelectedInToiMuc.ValueItem)
                    {
                        case nameof(MLNSFiled.NG):
                            DataReportTongHop = DataReportTongHop.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                            DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG):
                            DataReportTongHop = DataReportTongHop.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                            DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG1):
                            DataReportTongHop = DataReportTongHop.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                            DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG2):
                            DataReportTongHop = DataReportTongHop.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                            DataReportTongHop.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                            break;
                    }

                    foreach (var item in DataReportTongHop)
                    {
                        if (!string.IsNullOrEmpty(item.NG))
                        {
                            item.M = String.Empty;
                            item.TM = String.Empty;
                        }
                        if (!string.IsNullOrEmpty(item.TNG))
                        {
                            item.TTM = String.Empty;
                            item.NG = String.Empty;
                        }
                        if (!string.IsNullOrEmpty(item.TNG1))
                        {
                            item.TNG = String.Empty;
                        }
                        if (!string.IsNullOrEmpty(item.TNG2))
                        {
                            item.TNG1 = String.Empty;
                        }
                        if (!string.IsNullOrEmpty(item.TNG3))
                        {
                            item.TNG2 = String.Empty;
                        }
                    }

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", GetLevelTitle(_dmChuKy, 1));
                    data.Add("Cap2", GetLevelTitle(_dmChuKy, 2));
                    data.Add("TieuDe1", TieuDe1.ToUpper());
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    data.Add("TongQuyetToan", TongQuyetToan);
                    data.Add("TongDuToan", TongDuToan);
                    data.Add("TongUocThucHien", TongUocThucHien);
                    data.Add("TongTuChi", TongTuChi);
                    data.Add("TongTienBangChu", StringUtils.NumberToText(TongTuChi * donViTinh, true));
                    // data.Add("ThoiGian", string.Format("{0}, Ngày {1} tháng {2} năm {3}",_diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                    data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    data.Add("Items", DataReportTongHop);
                    data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                    data.Add("headercol1", _sessionService.Current.YearOfWork - 2);
                    data.Add("headercol2", _sessionService.Current.YearOfWork - 1);
                    data.Add("headercol3", _sessionService.Current.YearOfWork);
                    //data.Add("TenDonVi", ListDonVi.Where(n => listStringIdDonVi.Contains(n.ValueItem)).FirstOrDefault() != null ?
                    //    string.Join(",", ListDonVi.Where(n => listStringIdDonVi.Contains(n.ValueItem)).Select(n => n.NameItem)) : string.Empty); 
                    data.Add("TenDonVi", donVi0 != null ? donVi0.TenDonVi : string.Empty);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    templateFileName = isPrintPhuLuc ? GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_CHITIETDONVI_PHULUC_ALL) : GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_CHITIETDONVI_ALL);
                    fileNamePrefix = "rptDuToanDauNamChiTiet";
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                    List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(chiTietToi).Select(x => x + 1).ToList();
                    var xlsFile = _exportService.Export<ReportDuToanDauNamTongHopQuery>(templateFileName, data, hideColumns);
                    exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));

                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

        public string GetPath(string input)
        {
            return Path.Combine(ExportPrefix.PATH_TL_DTDN, input);
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        private void PrintReportDuToanUocThucHien(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_DAUNAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    ExcelFile xlsFile;

                    List<string> listIdDonVi = ListDonVi.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList();
                    string listDonViSelected = string.Join(",", listIdDonVi);
                    int donViTinh = GetDonViTinh();
                    bool isPrintPhuLuc = SelectedKieuGiay != null && SelectedKieuGiay.ValueItem == KieuGiay.PHU_LUC;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");

                    DataReportTongHop = _sktSoLieuService.GetDataReportDuToanDauNamTongHop(_sessionService.Current.YearOfWork, listDonViSelected, LoaiChungTu, loaiNNS, donViTinh).ToList();
                    if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                    {
                        var lns = _iNsMucLucNganSachService.FindByListLnsDonVi(DataReportTongHop.Select(x => x.LNS).ToList(), _sessionService.Current.YearOfWork);
                        var listLNS = lns.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns).ToList();
                        DataReportTongHop = DataReportTongHop.Where(x => listLNS.Contains(x.LNS)).ToList();
                    }
                    List<ReportDuToanDauNamTongHopQuery> listHeader = new List<ReportDuToanDauNamTongHopQuery>();
                    CalculateTotal();
                    if (DataReportTongHop != null && DataReportTongHop.Count > 0)
                    {
                        List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionService.Current.YearOfWork,
                                                                    string.Join(",", DataReportTongHop.Select(n => n.XauNoiMa).ToList())).ToList();
                        if (listParent != null && listParent.Count > 0)
                        {
                            foreach (NsMucLucNganSach item in listParent)
                            {
                                listHeader.Add(new ReportDuToanDauNamTongHopQuery
                                {
                                    LNS = item.Lns,
                                    L = item.L,
                                    K = item.K,
                                    M = item.M,
                                    TM = item.Tm,
                                    TTM = item.Ttm,
                                    NG = item.Ng,
                                    MoTa = item.MoTa,
                                    QuyetToan = 0,
                                    DuToan = 0,
                                    TuChi = 0,
                                    UocThucHien = 0,
                                    IsHangCha = true,
                                    MlnsId = item.MlnsId,
                                    MlnsIdParent = item.MlnsIdParent
                                });
                            }
                            foreach (ReportDuToanDauNamTongHopQuery item in DataReportTongHop)
                            {
                                //int index = listHeader.IndexOf(listHeader.Where(n => n.TM == item.TM && string.IsNullOrEmpty(n.TTM) && !string.IsNullOrEmpty(n.TM)).FirstOrDefault());
                                int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                                if (index >= 0)
                                {
                                    //item.MlnsIdParent = listHeader[index].MlnsId;
                                    listHeader.Insert(index + 1, item);
                                }
                            }
                        }
                        DataReportTongHop = new List<ReportDuToanDauNamTongHopQuery>(listHeader);
                        DataReportTongHop.Where(n => !n.IsHangCha).Select(n => { n.LNS = string.Empty; n.M = string.Empty; n.TM = string.Empty; return n; }).ToList();
                        DataReportTongHop.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.M)).Select(n => { n.LNS = string.Empty; return n; }).ToList();
                        DataReportTongHop.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.TM)).Select(n => { n.M = string.Empty; return n; }).ToList();
                    }

                    //tinh tung don vi
                    //page1
                    string tenDonVi1 = string.Empty;
                    string tenDonVi2 = string.Empty;
                    string tenDonVi3 = string.Empty;
                    string tenDonVi4 = string.Empty;
                    string tenDonVi5 = string.Empty;
                    string tenDonVi6 = string.Empty;
                    for (int i = 0; i < listIdDonVi.Count; i++)
                    {
                        if (i > 1) break;
                        switch (i)
                        {
                            case 0:
                                tenDonVi1 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 1:
                                tenDonVi2 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            default:
                                break;
                        }
                        List<ReportDuToanDauNamTongHopQuery> dataDonvi1 = _sktSoLieuService.GetDataReportDuToanDauNamTongHop(_sessionService.Current.YearOfWork, listIdDonVi[i], LoaiChungTu, loaiNNS, donViTinh).ToList();
                        if (dataDonvi1 != null && dataDonvi1.Count > 0)
                        {
                            switch (i)
                            {
                                case 0:
                                    TongDonVi1 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    TongDonVi1_UocThucHien = dataDonvi1.Select(n => n.UocThucHien.HasValue ? n.UocThucHien.Value : 0).Sum();
                                    break;
                                case 1:
                                    TongDonVi2 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    TongDonVi2_UocThucHien = dataDonvi1.Select(n => n.UocThucHien.HasValue ? n.UocThucHien.Value : 0).Sum();
                                    break;
                                default:
                                    break;
                            }
                        }
                        foreach (ReportDuToanDauNamTongHopQuery item in dataDonvi1)
                        {
                            switch (i)
                            {
                                case 0:
                                    DataReportTongHop.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi1 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    DataReportTongHop.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.UocThucHienDonVi1 = item.UocThucHien.HasValue ? item.UocThucHien.Value : 0; return n; }).ToList();
                                    break;
                                case 1:
                                    DataReportTongHop.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi2 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    DataReportTongHop.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.UocThucHienDonVi2 = item.UocThucHien.HasValue ? item.UocThucHien.Value : 0; return n; }).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    CalculateDataLNS();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", GetLevelTitle(_dmChuKy, 1));
                    data.Add("Cap2", GetLevelTitle(_dmChuKy, 2));
                    data.Add("TieuDe1", TieuDe1.ToUpper());
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    data.Add("TongUocThucHien", TongUocThucHien);
                    data.Add("TongTuChi", TongTuChi);
                    data.Add("TongTuChi1", TongDonVi1);
                    data.Add("TongUocThucHien1", TongDonVi1_UocThucHien);
                    data.Add("TongTuChi2", TongDonVi2);
                    data.Add("TongUocThucHien2", TongDonVi2_UocThucHien);
                    data.Add("Items", DataReportTongHop);
                    data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                    data.Add("headercol1", _sessionService.Current.YearOfWork - 1);
                    data.Add("headercol2", _sessionService.Current.YearOfWork);
                    data.Add("TenDonVi1", tenDonVi1);
                    data.Add("TenDonVi2", tenDonVi2);
                    data.Add("ThoiGian", string.Format("{0}, Ngày {1} tháng {2} năm {3}", _diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    data.Add("TongTienBangChu", StringUtils.NumberToText(TongTuChi * donViTinh, true));
                    templateFileName = isPrintPhuLuc ? GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_TONGHOPDUTOANUOCTHUCHIEN_PHULUC) : GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_TONGHOPDUTOANUOCTHUCHIEN);
                    fileNamePrefix = "rptDuToanDauNamDuToan_UocThucHien";
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    xlsFile = _exportService.Export<ReportDuToanDauNamTongHopQuery>(templateFileName, data);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));

                    //page2
                    int numberPage = (listIdDonVi.Count() - 2) / 3;
                    if ((listIdDonVi.Count() - 2) % 3 > 0)
                    {
                        numberPage++;
                    }

                    for (int p = 2; p < numberPage + 2; p++)
                    {
                        bool isExportFile = false;
                        TongDonVi1 = 0;
                        TongDonVi2 = 0;
                        TongDonVi3 = 0;
                        TongDonVi4 = 0;
                        TongDonVi5 = 0;
                        TongDonVi6 = 0;
                        TongDonVi1_UocThucHien = 0;
                        TongDonVi2_UocThucHien = 0;
                        TongDonVi3_UocThucHien = 0;
                        tenDonVi1 = string.Empty;
                        tenDonVi2 = string.Empty;
                        tenDonVi3 = string.Empty;
                        tenDonVi4 = string.Empty;
                        tenDonVi5 = string.Empty;
                        tenDonVi6 = string.Empty;
                        int sttPage = p - 2;
                        List<ReportDuToanDauNamTongHopQuery> dataPage1 = new List<ReportDuToanDauNamTongHopQuery>();
                        foreach (ReportDuToanDauNamTongHopQuery item in DataReportTongHop)
                        {
                            dataPage1.Add(new ReportDuToanDauNamTongHopQuery
                            {
                                LNS = item.LNS,
                                M = item.M,
                                TM = item.TM,
                                TTM = item.TTM,
                                NG = item.NG,
                                MoTa = item.MoTa,
                                IsHangCha = item.IsHangCha,
                                MlnsId = item.MlnsId,
                                XauNoiMa = item.XauNoiMa,
                                MlnsIdParent = item.MlnsIdParent
                            });
                        }
                        dataPage1.Select(n =>
                        {
                            n.QuyetToan = 0; n.DuToan = 0; n.TuChi = 0; n.UocThucHien = 0;
                            n.TuChiDonVi1 = 0; n.TuChiDonVi2 = 0; n.TuChiDonVi3 = 0; n.TuChiDonVi4 = 0; n.TuChiDonVi5 = 0;
                            n.UocThucHienDonVi1 = 0; n.UocThucHienDonVi2 = 0; n.UocThucHienDonVi3 = 0;
                            n.TuChiDonVi6 = 0; return n;
                        }).ToList();
                        int countDonVi = 0;

                        for (int i = sttPage * 3 + 2; i <= sttPage * 3 + 4; i++)
                        {
                            if (i >= listIdDonVi.Count()) break;
                            switch (countDonVi)
                            {
                                case 0:
                                    tenDonVi1 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 1:
                                    tenDonVi2 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 2:
                                    tenDonVi3 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                default:
                                    break;
                            }
                            List<ReportDuToanDauNamTongHopQuery> dataDonvi1 = _sktSoLieuService.GetDataReportDuToanDauNamTongHop(_sessionService.Current.YearOfWork, listIdDonVi[i], LoaiChungTu, loaiNNS, donViTinh).ToList();
                            if (dataDonvi1 != null && dataDonvi1.Count > 0)
                            {
                                isExportFile = true;
                                switch (countDonVi)
                                {
                                    case 0:
                                        TongDonVi1 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TongDonVi1_UocThucHien = dataDonvi1.Select(n => n.UocThucHien.HasValue ? n.UocThucHien.Value : 0).Sum();
                                        break;
                                    case 1:
                                        TongDonVi2 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TongDonVi2_UocThucHien = dataDonvi1.Select(n => n.UocThucHien.HasValue ? n.UocThucHien.Value : 0).Sum();
                                        break;
                                    case 2:
                                        TongDonVi3 = dataDonvi1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TongDonVi3_UocThucHien = dataDonvi1.Select(n => n.UocThucHien.HasValue ? n.UocThucHien.Value : 0).Sum();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            foreach (ReportDuToanDauNamTongHopQuery item in dataDonvi1)
                            {
                                switch (countDonVi)
                                {
                                    case 0:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi1 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.UocThucHienDonVi1 = item.UocThucHien.HasValue ? item.UocThucHien.Value : 0; return n; }).ToList();
                                        break;
                                    case 1:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi2 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.UocThucHienDonVi2 = item.UocThucHien.HasValue ? item.UocThucHien.Value : 0; return n; }).ToList();
                                        break;
                                    case 2:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.TuChiDonVi3 = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.UocThucHienDonVi3 = item.UocThucHien.HasValue ? item.UocThucHien.Value : 0; return n; }).ToList();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            countDonVi++;
                        }
                        if (isExportFile)
                        {
                            CalculateDataLNSPage(ref dataPage1);
                            Dictionary<string, object> dataPage = new Dictionary<string, object>();
                            dataPage.Add("FormatNumber", formatNumber);
                            dataPage.Add("Items", dataPage1);
                            dataPage.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                            dataPage.Add("TongTuChi1", TongDonVi1);
                            dataPage.Add("TongTuChi2", TongDonVi2);
                            dataPage.Add("TongTuChi3", TongDonVi3);
                            dataPage.Add("TongUocThucHien1", TongDonVi1_UocThucHien);
                            dataPage.Add("TongUocThucHien2", TongDonVi2_UocThucHien);
                            dataPage.Add("TongUocThucHien3", TongDonVi3_UocThucHien);
                            dataPage.Add("TenDonVi1", tenDonVi1);
                            dataPage.Add("TenDonVi2", tenDonVi2);
                            dataPage.Add("TenDonVi3", tenDonVi3);

                            templateFileName = GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_TONGHOPDUTOANUOCTHUCHIEN_TO2);
                            fileNamePrefix = string.Format("rptDuToanDauNamUocThucHien_To{0}", p);
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            xlsFile = _exportService.Export<ReportDuToanDauNamTongHopQuery>(templateFileName, dataPage);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_DAUNAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_DAUNAM;
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

        private void LoadChiTietToi()
        {
            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                string chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                DataInToiMuc = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi));
                _selectedInToiMuc = DataInToiMuc != null ? DataInToiMuc[0] : null;
            }
        }

        private string GetLevelTitle(DmChuKy dmChuKy, int level)
        {
            var loaiDVBanHanh = dmChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty;
            var danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

            return loaiDVBanHanh switch
            {
                LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => "TÊN ĐƠN VỊ ĐƯỢC CHỌN",
                LoaiDonViBanHanh.TUY_CHINH => dmChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
        }
    }
}
